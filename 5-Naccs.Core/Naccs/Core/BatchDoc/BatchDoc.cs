namespace Naccs.Core.BatchDoc
{
    using Naccs.Core.Classes;
    using Naccs.Core.Main;
    using Naccs.Core.Settings;
    using Naccs.Net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.IO.Compression;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    public class BatchDoc : Form
    {
        private ColumnHeader clmDateTime;
        private ColumnHeader clmDocName;
        private ColumnHeader clmFileName;
        private ColumnHeader clmFileSize;
        private ColumnHeader clmOutcode;
        private ColumnHeader clmStatus;
        private ToolStripMenuItem cmAllCheck;
        private ToolStripMenuItem cmAllCheckCancel;
        private ContextMenuStrip cmEdit;
        private ToolStripSeparator cmEditSep1;
        private ToolStripMenuItem cmRengeCheck;
        private ToolStripMenuItem cmRengeCheckCancel;
        private const string CnExtCSV = ".CSV";
        private const string CnExtEDI = ".EDI";
        private const string CnExtGZ = ".GZ";
        private const int CnFileNameLen = 0x80;
        private const string CnGuideDoing = "{0:D}/{1:D}の管理資料を取り出しています。";
        private const string CnGuideGet = "{0:D}の管理資料を取り出しました。";
        private const string CnGuideNoData = "取り出す管理資料はありません。";
        private const string CnGuideSelect = "取り出す管理資料をチェックしてください。 {0}/{1} の管理資料がチェックされています。";
        private const string CnListStatus = "取得済み";
        private const int CnOutcodeLen = 7;
        private IContainer components;
        private const string CRLF = "\r\n";
        private const string FmtRecvData = "BATCH_RECV";
        private const string FmtRecvFile = "BATCH_RECV File({0})";
        private const string FmtSendData = "BATCH_SEND Command({0})";
        private const string FmtSendError = "BATCH_SEND Error code({0})";
        private ImageList imReport;
        private ListView lvReport;
        private bool mGetting;
        private IHttpClient mHttpClient;
        private bool mIsCancel;
        private ToolStripMenuItem mnEdit;
        private ToolStripMenuItem mnEnd;
        private ToolStripMenuItem mnFile;
        private ToolStripSeparator mnFileSep1;
        private ToolStripMenuItem mnGet;
        private ToolStripMenuItem mnKeyChange;
        private ToolStripMenuItem mnNewReport;
        private MenuStrip mnReport;
        private ToolStripMenuItem mnView;
        private OutcodeTbl mOutcodeTbl = new OutcodeTbl();
        private object mOwnerControl;
        private string mPass;
        private bool mPost;
        private List<ListViewItem> mReportItems = new List<ListViewItem>();
        private string mSelectOutcode;
        private USendRecvDlg mSendDlg;
        private int mSendIndex;
        private string mSyskind = "2";
        private string mTermAccessKey;
        private string mTermLogicalName;
        private bool mUseDemo;
        private string mUser;
        private SortOrder reportSortOrder;
        private int sortColumnIndex = -1;
        private ToolStripStatusLabel stGuide;
        private StatusStrip stReport;
        private ToolStripStatusLabel stSpring1;
        private ToolStripButton tsGet;
        private ToolStripButton tsNewReport;
        private ToolStrip tsReport;

        public event BatchDocReceivedHandler OnReceived;

        public BatchDoc(object owner, bool demo, int systemkind)
        {
            this.InitializeComponent();
            this.mnEdit.DropDown = this.cmEdit;
            this.mOwnerControl = owner;
            this.mUseDemo = demo;
            this.mSyskind = systemkind.ToString();
            this.mGetting = false;
        }

        private void AllCheck()
        {
            foreach (ListViewItem item in this.mReportItems)
            {
                if (string.IsNullOrEmpty(item.SubItems[this.clmStatus.Index].Text))
                {
                    item.Checked = true;
                }
            }
            this.MenuStatusChange();
            this.lvReport.Refresh();
        }

        private void BatchDoc_Load(object sender, EventArgs e)
        {
            this.stGuide.Text = "取り出す管理資料はありません。";
            this.mnNewReport.Image = this.imReport.Images["view.bmp"];
            this.tsNewReport.Image = this.mnNewReport.Image;
            this.mnGet.Image = this.imReport.Images["get.bmp"];
            this.tsGet.Image = this.mnGet.Image;
            this.MenuStatusChange();
        }

        private void BatchDoc_Shown(object sender, EventArgs e)
        {
            this.lvReport.Items.Clear();
            if (this.mReportItems != null)
            {
                this.mReportItems.Clear();
            }
            this.stGuide.Text = "取り出す管理資料はありません。";
            base.Owner = (Form) this.mOwnerControl;
            this.mnKeyChange.Visible = this.mPost;
            base.Activate();
            if (this.mPost)
            {
                this.mnKeyChange_Click(sender, e);
            }
            else
            {
                this.mnNewReport_Click(sender, e);
            }
        }

        private void CallbackOnError(IHttpClient sender, string sendkey, HttpExceptionEx ex)
        {
            if (ex.Cause != HttpCause.UserCancel)
            {
                this.SendDlgClose();
                HttpErrorDlg dlg = new HttpErrorDlg();
                dlg.ShowHttpError(ex);
                dlg.Dispose();
            }
            this.mGetting = false;
            this.MenuStatusChange();
        }

        private void CallbackOnReceived(object sender, string sendkey, ComParameter recvdata)
        {
            if (sendkey.StartsWith(RequestCommand.LST.ToString()))
            {
                ULogClass.LogWrite("BATCH_RECV", recvdata.DataString.Substring(0, 400), true);
                this.SendDlgClose();
                this.View(recvdata);
                this.mGetting = false;
                this.MenuStatusChange();
            }
            else if (recvdata.AttachCount == 0)
            {
                ULogClass.LogWrite("BATCH_RECV", recvdata.DataString.Substring(0, 400), true);
                this.SendDlgClose();
                RecvData data = DataFactory.CreateRecvData(recvdata.DataString);
                if ((data.OtherData == null) && (data.ResultData.Items.Count > 0))
                {
                    HttpErrorDlg dlg = new HttpErrorDlg();
                    dlg.ShowJobError(data.ResultData.Header.JobCode, data.ResultData);
                    dlg.Dispose();
                }
                this.mGetting = false;
                this.MenuStatusChange();
            }
            else
            {
                List<ListViewItem> list = this.ReportCheckedItems();
                list[this.mSendIndex].SubItems[this.clmStatus.Index].Text = "取得済み";
                this.lvReport.Refresh();
                string text = list[this.mSendIndex].SubItems[this.clmFileName.Index].Text;
                string outcode = list[this.mSendIndex].SubItems[this.clmOutcode.Index].Text;
                ULogClass.LogWrite(string.Format("BATCH_RECV File({0})", text));
                try
                {
                    try
                    {
                        this.DataSave(recvdata, text, outcode);
                        if (!this.mIsCancel)
                        {
                            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer {
                                Interval = 10
                            };
                            timer.Tick += new EventHandler(this.GetNextFile);
                            timer.Enabled = true;
                        }
                    }
                    finally
                    {
                        this.SendDlgClose();
                    }
                }
                catch (Exception exception)
                {
                    MessageDialog dialog = new MessageDialog();
                    string internalCode = string.Format("{0}{1}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo), "\r\n") + string.Format("<Exception>{0}", "\r\n");
                    if (exception.Message != null)
                    {
                        internalCode = internalCode + string.Format("-ExceptionMessage{1} {0}{1}{1}", exception.Message, "\r\n");
                    }
                    dialog.ShowMessage("E303", text, internalCode);
                    this.mGetting = false;
                    this.MenuStatusChange();
                }
            }
        }

        private void CallbackOnSent(object sender, string key)
        {
        }

        private void cmAllCheck_Click(object sender, EventArgs e)
        {
            if (base.Enabled)
            {
                this.AllCheck();
            }
        }

        private void cmAllCheckCancel_Click(object sender, EventArgs e)
        {
            if (base.Enabled)
            {
                foreach (ListViewItem item in this.ReportCheckedItems())
                {
                    item.Checked = false;
                }
                this.MenuStatusChange();
                this.lvReport.Refresh();
            }
        }

        private void cmRengeCheck_Click(object sender, EventArgs e)
        {
            if (base.Enabled)
            {
                foreach (ListViewItem item in this.ReportSelectedItems())
                {
                    item.Checked = true;
                }
                this.MenuStatusChange();
                this.lvReport.Refresh();
            }
        }

        private void cmRengeCheckCancel_Click(object sender, EventArgs e)
        {
            if (base.Enabled)
            {
                foreach (ListViewItem item in this.ReportSelectedItems())
                {
                    item.Checked = false;
                }
                this.MenuStatusChange();
                this.lvReport.Refresh();
            }
        }

        private int CompareListViewItem(ListViewItem x, ListViewItem y)
        {
            int num = 0;
            if (this.sortColumnIndex == 3)
            {
                num = this.convertToLong(x.SubItems[this.sortColumnIndex].Text).CompareTo(this.convertToLong(y.SubItems[this.sortColumnIndex].Text));
            }
            else
            {
                num = x.SubItems[this.sortColumnIndex].Text.CompareTo(y.SubItems[this.sortColumnIndex].Text);
            }
            if (SortOrder.Descending.Equals(this.reportSortOrder))
            {
                num *= -1;
            }
            return num;
        }

        private long convertToLong(string inString)
        {
            long result = 0L;
            if (!long.TryParse(inString, out result))
            {
                result = -9223372036854775808L;
            }
            return result;
        }

        private void CreateBatchFolder(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }

        private string CreateRequestData(RequestCommand command, string body)
        {
            IData data = new NaccsData {
                Header = { JobCode = "?" + command.ToString(), UserId = this.mUser, Password = this.mPass },
                JobData = body
            };
            return data.GetDataString();
        }

        private void DataSave(ComParameter recvdata, string fname, string outcode)
        {
            string dir = FileSave.CreateInstance().TypeList.getType("F").Dir;
            try
            {
                if (fname.EndsWith(".GZ", StringComparison.CurrentCultureIgnoreCase))
                {
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fname);
                    MemoryStream stream = this.Decompress(Path.Combine(recvdata.AttachFolder, fname));
                    try
                    {
                        Encoding encoding = Encoding.GetEncoding("UTF-8");
                        if (fileNameWithoutExtension.EndsWith(".EDI", StringComparison.CurrentCultureIgnoreCase))
                        {
                            ComParameter parameter = new ComParameter {
                                DataString = encoding.GetString(stream.ToArray(), 0, stream.ToArray().Length)
                            };
                            this.OnReceived(parameter);
                        }
                        else
                        {
                            fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fname);
                            string str3 = this.mOutcodeTbl.Find(outcode);
                            if (!string.IsNullOrEmpty(str3))
                            {
                                fileNameWithoutExtension = string.Format("{0}{1}", str3, fileNameWithoutExtension.Substring(outcode.Length));
                            }
                            this.CreateBatchFolder(dir);
                            StreamWriter writer = new StreamWriter(Path.Combine(dir, fileNameWithoutExtension), false, Encoding.GetEncoding("UTF-8"));
                            try
                            {
                                writer.Write(encoding.GetString(stream.ToArray(), 0, stream.ToArray().Length));
                            }
                            finally
                            {
                                writer.Close();
                            }
                        }
                        return;
                    }
                    finally
                    {
                        stream.Close();
                    }
                }
                this.CreateBatchFolder(dir);
                File.Copy(Path.Combine(recvdata.AttachFolder, fname), Path.Combine(dir, fname), true);
            }
            finally
            {
                recvdata.DeleteAttachFolder();
            }
        }

        private MemoryStream Decompress(string path)
        {
            MemoryStream stream = new MemoryStream();
            FileStream stream2 = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            try
            {
                GZipStream stream3 = new GZipStream(stream2, CompressionMode.Decompress);
                try
                {
                    byte[] buffer = new byte[0x3e8];
                    long num = 0L;
                    while (true)
                    {
                        int count = stream3.Read(buffer, 0, 0x3e8);
                        if (count == 0)
                        {
                            return stream;
                        }
                        num += count;
                        stream.SetLength(num);
                        stream.Write(buffer, 0, count);
                    }
                }
                finally
                {
                    stream3.Close();
                }
            }
            finally
            {
                stream2.Close();
            }
            return stream;
        }

        private int DefaultCompareItem(ListViewItem x, ListViewItem y)
        {
            int num = 0;
            string text = x.SubItems[this.clmDateTime.Index].Text;
            string str2 = y.SubItems[this.clmDateTime.Index].Text;
            if (text.Length >= 10)
            {
                text = text.Substring(0, 10);
            }
            if (str2.Length >= 10)
            {
                str2 = str2.Substring(0, 10);
            }
            num = str2.CompareTo(text);
            if (num == 0)
            {
                num = x.SubItems[this.clmFileName.Index].Text.CompareTo(y.SubItems[this.clmFileName.Index].Text);
            }
            return num;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private string getDivFormConv(string str, string format, char ch)
        {
            int startIndex = 0;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < format.Length; i++)
            {
                if (format.Substring(i, 1) == "_")
                {
                    if (startIndex >= str.Length)
                    {
                        builder.Append(ch);
                    }
                    else
                    {
                        builder.Append(str.Substring(startIndex, 1));
                        startIndex++;
                    }
                }
                else
                {
                    builder.Append(format.Substring(i, 1));
                }
            }
            return builder.ToString();
        }

        private void GetNextFile(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = (System.Windows.Forms.Timer) sender;
            timer.Enabled = false;
            timer.Dispose();
            this.mSendIndex++;
            this.GetRequest(this.mSendIndex);
        }

        private void GetRequest(int index)
        {
            bool flag = false;
            List<ListViewItem> list = this.ReportCheckedItems();
            for (int i = index; i < list.Count; i++)
            {
                this.mSendIndex = i;
                this.stGuide.Text = string.Format("{0:D}/{1:D}の管理資料を取り出しています。", i + 1, list.Count);
                if (string.IsNullOrEmpty(list[i].SubItems[this.clmStatus.Index].Text))
                {
                    RequestCommand gTP;
                    flag = true;
                    string text = list[i].SubItems[this.clmFileName.Index].Text;
                    string body = string.Format("{0}{1}", text.PadRight(0x80, ' '), "\r\n");
                    if (this.mPost)
                    {
                        gTP = RequestCommand.GTP;
                    }
                    else
                    {
                        gTP = RequestCommand.GTN;
                    }
                    ComParameter data = new ComParameter {
                        DataString = this.CreateRequestData(gTP, body)
                    };
                    string sendkey = string.Format("{0}{1}", gTP.ToString(), i);
                    this.RequestSend(data, sendkey, string.Format("GET:{0}", list[i].SubItems[this.clmOutcode.Index].Text));
                    break;
                }
            }
            if (!flag)
            {
                this.mGetting = false;
                this.MenuStatusChange();
            }
        }

        private void HttpCancel()
        {
            this.mIsCancel = true;
            this.SendDlgClose();
            this.mHttpClient.Abort();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(Naccs.Core.BatchDoc.BatchDoc));
            this.mnReport = new MenuStrip();
            this.mnFile = new ToolStripMenuItem();
            this.mnGet = new ToolStripMenuItem();
            this.mnFileSep1 = new ToolStripSeparator();
            this.mnEnd = new ToolStripMenuItem();
            this.mnEdit = new ToolStripMenuItem();
            this.mnView = new ToolStripMenuItem();
            this.mnNewReport = new ToolStripMenuItem();
            this.mnKeyChange = new ToolStripMenuItem();
            this.cmEdit = new ContextMenuStrip(this.components);
            this.cmAllCheck = new ToolStripMenuItem();
            this.cmAllCheckCancel = new ToolStripMenuItem();
            this.cmEditSep1 = new ToolStripSeparator();
            this.cmRengeCheck = new ToolStripMenuItem();
            this.cmRengeCheckCancel = new ToolStripMenuItem();
            this.tsReport = new ToolStrip();
            this.tsNewReport = new ToolStripButton();
            this.tsGet = new ToolStripButton();
            this.stReport = new StatusStrip();
            this.stGuide = new ToolStripStatusLabel();
            this.stSpring1 = new ToolStripStatusLabel();
            this.lvReport = new ListView();
            this.clmDocName = new ColumnHeader();
            this.clmOutcode = new ColumnHeader();
            this.clmDateTime = new ColumnHeader();
            this.clmFileSize = new ColumnHeader();
            this.clmStatus = new ColumnHeader();
            this.clmFileName = new ColumnHeader();
            this.imReport = new ImageList(this.components);
            this.mnReport.SuspendLayout();
            this.cmEdit.SuspendLayout();
            this.tsReport.SuspendLayout();
            this.stReport.SuspendLayout();
            base.SuspendLayout();
            this.mnReport.Items.AddRange(new ToolStripItem[] { this.mnFile, this.mnEdit, this.mnView });
            this.mnReport.Location = new Point(0, 0);
            this.mnReport.Name = "mnReport";
            this.mnReport.Size = new Size(0x311, 0x18);
            this.mnReport.TabIndex = 0;
            this.mnReport.Text = "menuStrip1";
            this.mnFile.DropDownItems.AddRange(new ToolStripItem[] { this.mnGet, this.mnFileSep1, this.mnEnd });
            this.mnFile.Name = "mnFile";
            this.mnFile.Size = new Size(0x42, 20);
            this.mnFile.Text = "ファイル(&F)";
            this.mnGet.AutoToolTip = true;
            this.mnGet.Name = "mnGet";
            this.mnGet.ShortcutKeys = Keys.Control | Keys.G;
            this.mnGet.Size = new Size(0x94, 0x16);
            this.mnGet.Text = "取得(&G)";
            this.mnGet.ToolTipText = "選択した管理資料を取得する";
            this.mnGet.Click += new EventHandler(this.mnGet_Click);
            this.mnFileSep1.Name = "mnFileSep1";
            this.mnFileSep1.Size = new Size(0x91, 6);
            this.mnEnd.Name = "mnEnd";
            this.mnEnd.Size = new Size(0x94, 0x16);
            this.mnEnd.Text = "終了(&X)";
            this.mnEnd.Click += new EventHandler(this.mnEnd_Click);
            this.mnEdit.Name = "mnEdit";
            this.mnEdit.Size = new Size(0x38, 20);
            this.mnEdit.Text = "編集(&E)";
            this.mnView.DropDownItems.AddRange(new ToolStripItem[] { this.mnNewReport, this.mnKeyChange });
            this.mnView.Name = "mnView";
            this.mnView.Size = new Size(0x39, 20);
            this.mnView.Text = "表示(&V)";
            this.mnNewReport.Name = "mnNewReport";
            this.mnNewReport.ShortcutKeys = Keys.F5;
            this.mnNewReport.Size = new Size(0xd3, 0x16);
            this.mnNewReport.Text = "最新状態に更新(&R)";
            this.mnNewReport.Click += new EventHandler(this.mnNewReport_Click);
            this.mnKeyChange.Name = "mnKeyChange";
            this.mnKeyChange.ShortcutKeys = Keys.Control | Keys.K;
            this.mnKeyChange.Size = new Size(0xd3, 0x16);
            this.mnKeyChange.Text = "取り出し条件変更(&K)";
            this.mnKeyChange.Visible = false;
            this.mnKeyChange.Click += new EventHandler(this.mnKeyChange_Click);
            this.cmEdit.Items.AddRange(new ToolStripItem[] { this.cmAllCheck, this.cmAllCheckCancel, this.cmEditSep1, this.cmRengeCheck, this.cmRengeCheckCancel });
            this.cmEdit.Name = "cmReport";
            this.cmEdit.Size = new Size(0x102, 0x62);
            this.cmAllCheck.Name = "cmAllCheck";
            this.cmAllCheck.ShortcutKeys = Keys.Control | Keys.A;
            this.cmAllCheck.Size = new Size(0x101, 0x16);
            this.cmAllCheck.Text = "全てチェック(&A)";
            this.cmAllCheck.Click += new EventHandler(this.cmAllCheck_Click);
            this.cmAllCheckCancel.Name = "cmAllCheckCancel";
            this.cmAllCheckCancel.ShortcutKeys = Keys.Control | Keys.Shift | Keys.A;
            this.cmAllCheckCancel.Size = new Size(0x101, 0x16);
            this.cmAllCheckCancel.Text = "全てチェック解除(&C)";
            this.cmAllCheckCancel.Click += new EventHandler(this.cmAllCheckCancel_Click);
            this.cmEditSep1.Name = "cmEditSep1";
            this.cmEditSep1.Size = new Size(0xfe, 6);
            this.cmRengeCheck.Enabled = false;
            this.cmRengeCheck.Name = "cmRengeCheck";
            this.cmRengeCheck.ShortcutKeys = Keys.Control | Keys.R;
            this.cmRengeCheck.Size = new Size(0x101, 0x16);
            this.cmRengeCheck.Text = "選択範囲チェック(&R)";
            this.cmRengeCheck.Click += new EventHandler(this.cmRengeCheck_Click);
            this.cmRengeCheckCancel.Enabled = false;
            this.cmRengeCheckCancel.Name = "cmRengeCheckCancel";
            this.cmRengeCheckCancel.ShortcutKeys = Keys.Control | Keys.Shift | Keys.R;
            this.cmRengeCheckCancel.Size = new Size(0x101, 0x16);
            this.cmRengeCheckCancel.Text = "選択範囲チェック解除(&U)";
            this.cmRengeCheckCancel.Click += new EventHandler(this.cmRengeCheckCancel_Click);
            this.tsReport.Items.AddRange(new ToolStripItem[] { this.tsNewReport, this.tsGet });
            this.tsReport.Location = new Point(0, 0x18);
            this.tsReport.Name = "tsReport";
            this.tsReport.Size = new Size(0x311, 0x19);
            this.tsReport.TabIndex = 1;
            this.tsReport.Text = "toolStrip1";
//            this.tsNewReport.Image = (Image) manager.GetObject("tsNewReport.Image");
            this.tsNewReport.ImageTransparentColor = Color.Magenta;
            this.tsNewReport.Name = "tsNewReport";
            this.tsNewReport.Size = new Size(0x6a, 0x16);
            this.tsNewReport.Text = "最新状態に更新";
            this.tsNewReport.Click += new EventHandler(this.mnNewReport_Click);
//            this.tsGet.Image = (Image) manager.GetObject("tsGet.Image");
            this.tsGet.ImageTransparentColor = Color.Magenta;
            this.tsGet.Name = "tsGet";
            this.tsGet.Size = new Size(0x31, 0x16);
            this.tsGet.Text = "取得";
            this.tsGet.Click += new EventHandler(this.mnGet_Click);
            this.stReport.Items.AddRange(new ToolStripItem[] { this.stGuide, this.stSpring1 });
            this.stReport.Location = new Point(0, 0x135);
            this.stReport.Name = "stReport";
            this.stReport.Size = new Size(0x311, 0x16);
            this.stReport.TabIndex = 2;
            this.stReport.Text = "statusStrip1";
            this.stGuide.Name = "stGuide";
            this.stGuide.Size = new Size(0xa1, 0x11);
            this.stGuide.Text = "ｎ個の管理資料が取り出せます。";
            this.stSpring1.Name = "stSpring1";
            this.stSpring1.Size = new Size(0x261, 0x11);
            this.stSpring1.Spring = true;
            this.lvReport.CheckBoxes = true;
            this.lvReport.Columns.AddRange(new ColumnHeader[] { this.clmDocName, this.clmOutcode, this.clmDateTime, this.clmFileSize, this.clmStatus, this.clmFileName });
            this.lvReport.ContextMenuStrip = this.cmEdit;
            this.lvReport.Dock = DockStyle.Fill;
            this.lvReport.GridLines = true;
            this.lvReport.Location = new Point(0, 0x31);
            this.lvReport.Name = "lvReport";
            this.lvReport.Size = new Size(0x311, 260);
            this.lvReport.TabIndex = 3;
            this.lvReport.UseCompatibleStateImageBehavior = false;
            this.lvReport.View = System.Windows.Forms.View.Details;
            this.lvReport.VirtualMode = true;
            this.lvReport.PreviewKeyDown += new PreviewKeyDownEventHandler(this.lvReport_PreviewKeyDown);
            this.lvReport.MouseClick += new MouseEventHandler(this.lvReport_MouseClick);
            this.lvReport.VirtualItemsSelectionRangeChanged += new ListViewVirtualItemsSelectionRangeChangedEventHandler(this.lvReport_VirtualItemsSelectionRangeChanged);
            this.lvReport.SelectedIndexChanged += new EventHandler(this.lvReport_SelectedIndexChanged);
            this.lvReport.ColumnClick += new ColumnClickEventHandler(this.lvReport_ColumnClick);
            this.lvReport.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(this.lvReport_RetrieveVirtualItem);
            this.clmDocName.Text = "管理資料名";
            this.clmDocName.Width = 400;
            this.clmOutcode.Text = "出力情報コード";
            this.clmOutcode.Width = 0x5f;
            this.clmDateTime.Text = "作成日付時刻";
            this.clmDateTime.Width = 0x84;
            this.clmFileSize.Text = "サイズ(KB)";
            this.clmFileSize.TextAlign = HorizontalAlignment.Right;
            this.clmFileSize.Width = 0x49;
            this.clmStatus.Text = "状態";
            this.clmStatus.Width = 0x49;
            this.clmFileName.Text = "ファイル名";
            this.clmFileName.Width = 0;
//            this.imReport.ImageStream = (ImageListStreamer) manager.GetObject("imReport.ImageStream");
            this.imReport.TransparentColor = Color.Magenta;
            this.imReport.Images.SetKeyName(0, "view.bmp");
            this.imReport.Images.SetKeyName(1, "get.bmp");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x311, 0x14b);
            base.Controls.Add(this.lvReport);
            base.Controls.Add(this.stReport);
            base.Controls.Add(this.tsReport);
            base.Controls.Add(this.mnReport);
            base.MainMenuStrip = this.mnReport;
            base.Name = "BatchDoc";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "管理資料取り出し";
            base.Shown += new EventHandler(this.BatchDoc_Shown);
            base.Load += new EventHandler(this.BatchDoc_Load);
            this.mnReport.ResumeLayout(false);
            this.mnReport.PerformLayout();
            this.cmEdit.ResumeLayout(false);
            this.tsReport.ResumeLayout(false);
            this.tsReport.PerformLayout();
            this.stReport.ResumeLayout(false);
            this.stReport.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void ListRequest()
        {
            this.sortColumnIndex = -1;
            this.reportSortOrder = SortOrder.None;
            ComParameter data = new ComParameter();
            string mSelectOutcode = "";
            if (this.mPost)
            {
                mSelectOutcode = this.mSelectOutcode;
            }
            string body = string.Format("{0}{1}", mSelectOutcode.PadRight(7, ' '), "\r\n");
            data.DataString = this.CreateRequestData(RequestCommand.LST, body);
            string sendkey = RequestCommand.LST.ToString();
            this.RequestSend(data, sendkey, "LIST");
        }

        private void lvReport_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.sortColumnIndex.Equals(e.Column))
            {
                if (SortOrder.Descending.Equals(this.reportSortOrder))
                {
                    this.reportSortOrder = SortOrder.Ascending;
                }
                else
                {
                    this.reportSortOrder = SortOrder.Descending;
                }
            }
            else
            {
                this.reportSortOrder = SortOrder.Ascending;
            }
            this.sortColumnIndex = e.Column;
            List<ListViewItem> list = new List<ListViewItem>();
            foreach (ListViewItem item in this.mReportItems)
            {
                ListViewItem item2 = new ListViewItem(item.Text);
                for (int j = 1; j < item.SubItems.Count; j++)
                {
                    item2.SubItems.Add(item.SubItems[j].Text);
                }
                item2.SubItems.Add(item.Checked.ToString());
                list.Add(item2);
            }
            list.Sort(new Comparison<ListViewItem>(this.CompareListViewItem));
            this.mReportItems = new List<ListViewItem>();
            this.lvReport.VirtualListSize = 0;
            this.lvReport.Items.Clear();
            foreach (ListViewItem item3 in list)
            {
                ListViewItem item4 = new ListViewItem(item3.Text);
                for (int k = 1; k < (item3.SubItems.Count - 1); k++)
                {
                    item4.SubItems.Add(item3.SubItems[k].Text);
                }
                this.mReportItems.Add(item4);
            }
            this.lvReport.VirtualListSize = this.mReportItems.Count;
            Application.DoEvents();
            for (int i = 0; i < list.Count; i++)
            {
                this.mReportItems[i].Checked = true;
                if (list[i].SubItems[list[i].SubItems.Count - 1].Text.ToUpper() == "FALSE")
                {
                    this.mReportItems[i].Checked = false;
                }
            }
            this.lvReport.Refresh();
        }

        private void lvReport_MouseClick(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left) && (e.X < 20))
            {
                ListViewItem item = this.PointToReportItem(e.Y);
                if (item != null)
                {
                    item.Checked = !item.Checked;
                    this.MenuStatusChange();
                    this.lvReport.Refresh();
                }
            }
        }

        private void lvReport_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Space)
            {
                this.SelectedCheckChange();
            }
        }

        private void lvReport_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = this.mReportItems[e.ItemIndex];
        }

        private void lvReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmRengeCheck.Enabled = this.ReportSelectedItems().Count > 0;
            this.cmRengeCheckCancel.Enabled = this.cmRengeCheck.Enabled;
        }

        private void lvReport_VirtualItemsSelectionRangeChanged(object sender, ListViewVirtualItemsSelectionRangeChangedEventArgs e)
        {
            this.cmRengeCheck.Enabled = this.ReportSelectedItems().Count > 0;
            this.cmRengeCheckCancel.Enabled = this.cmRengeCheck.Enabled;
        }

        private void MenuStatusChange()
        {
            int num = 0;
            if (this.mReportItems.Count <= 0)
            {
                this.stGuide.Text = "取り出す管理資料はありません。";
            }
            else
            {
                List<ListViewItem> list = this.ReportCheckedItems();
                foreach (ListViewItem item in list)
                {
                    if (string.IsNullOrEmpty(item.SubItems[this.clmStatus.Index].Text))
                    {
                        num++;
                        break;
                    }
                }
                this.stGuide.Text = string.Format("取り出す管理資料をチェックしてください。 {0}/{1} の管理資料がチェックされています。", list.Count, this.mReportItems.Count);
            }
            this.cmAllCheck.Enabled = this.mReportItems.Count > 0;
            this.cmAllCheckCancel.Enabled = this.cmAllCheck.Enabled;
            this.mnGet.Enabled = num > 0;
            this.tsGet.Enabled = this.mnGet.Enabled;
            base.Enabled = !this.mGetting;
        }

        private void mnEnd_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void mnGet_Click(object sender, EventArgs e)
        {
            if (base.Enabled)
            {
                this.mGetting = true;
                this.MenuStatusChange();
                this.GetRequest(0);
            }
        }

        private void mnKeyChange_Click(object sender, EventArgs e)
        {
            if (base.Enabled)
            {
                ConditionChange change = new ConditionChange {
                    Outcode = this.mSelectOutcode
                };
                if (change.ShowDialog() == DialogResult.OK)
                {
                    this.mSelectOutcode = change.Outcode;
                    this.mGetting = true;
                    this.MenuStatusChange();
                    this.ListRequest();
                }
            }
        }

        private void mnNewReport_Click(object sender, EventArgs e)
        {
            if (base.Enabled)
            {
                if (this.mPost && string.IsNullOrEmpty(this.mSelectOutcode))
                {
                    this.mnKeyChange_Click(sender, e);
                }
                else
                {
                    this.tsNewReport.Enabled = this.mnNewReport.Enabled;
                    this.mGetting = true;
                    this.MenuStatusChange();
                    this.ListRequest();
                }
            }
        }

        private ListViewItem PointToReportItem(int y)
        {
            foreach (ListViewItem item2 in this.mReportItems)
            {
                if ((y >= item2.Bounds.Y) && (y <= (item2.Bounds.Y + item2.Bounds.Height)))
                {
                    return item2;
                }
            }
            return null;
        }

        private List<ListViewItem> ReportCheckedItems()
        {
            List<ListViewItem> list = new List<ListViewItem>();
            foreach (ListViewItem item in this.mReportItems)
            {
                if (item.Checked)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        private ListViewItem ReportFocusedItem(List<ListViewItem> items)
        {
            foreach (ListViewItem item2 in items)
            {
                if (item2.Focused)
                {
                    return item2;
                }
            }
            return null;
        }

        private List<ListViewItem> ReportSelectedItems()
        {
            List<ListViewItem> list = new List<ListViewItem>();
            foreach (ListViewItem item in this.mReportItems)
            {
                if (item.Selected)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        private void RequestSend(ComParameter data, string sendkey, string msg)
        {
            try
            {
                this.SendDlgShow(msg);
                ULogClass.LogWrite(string.Format("BATCH_SEND Command({0})", msg), data.DataString.Substring(0, 400), true);
                int num = this.mHttpClient.Send(this.mOwnerControl, sendkey, data);
                if (num > 0)
                {
                    ULogClass.LogWrite(string.Format("BATCH_SEND Error code({0})", num));
                    this.SendDlgClose();
                    HttpErrorDlg dlg = new HttpErrorDlg();
                    dlg.ShowSendError(num);
                    dlg.Dispose();
                    this.mGetting = false;
                    this.MenuStatusChange();
                }
            }
            catch
            {
                this.SendDlgClose();
                HttpErrorDlg dlg2 = new HttpErrorDlg();
                dlg2.ShowSendError(9);
                dlg2.Dispose();
                this.mGetting = false;
                this.MenuStatusChange();
            }
        }

        private void SelectedCheckChange()
        {
            List<ListViewItem> items = this.ReportSelectedItems();
            if (items.Count > 0)
            {
                bool flag = false;
                ListViewItem item = this.ReportFocusedItem(items);
                if (item == null)
                {
                    this.lvReport.FocusedItem = items[0];
                    flag = !items[0].Checked;
                }
                else
                {
                    flag = !item.Checked;
                }
                foreach (ListViewItem item2 in items)
                {
                    item2.Checked = flag;
                }
                this.MenuStatusChange();
                this.lvReport.Refresh();
            }
        }

        private void SendDlgClose()
        {
            base.Activate();
            if (this.mSendDlg != null)
            {
                this.mSendDlg.Close();
                this.mSendDlg.Dispose();
                this.mSendDlg = null;
            }
        }

        private void SendDlgShow(string msg)
        {
            this.mIsCancel = false;
            if (this.mSendDlg == null)
            {
                this.mSendDlg = new USendRecvDlg();
                this.mSendDlg.OnAbort += new OnAbortHandler(this.HttpCancel);
            }
            this.mSendDlg.lblSendJobCode.Text = msg;
            this.mSendDlg.TopMost = false;
            this.mSendDlg.Owner = this;
            this.mSendDlg.Show();
        }

        public DialogResult ShowDialog(IHttpClient client, string user, string password, bool post)
        {
            if (post)
            {
                this.Text = "管理資料再取り出し";
            }
            else
            {
                this.Text = "管理資料取り出し";
            }
            this.UserSetting();
            this.mHttpClient = (IHttpClient) client.Clone();
            this.mUser = user;
            this.mPass = password;
            this.mPost = post;
            this.mHttpClient.OnSent += new SentEventHandler(this.CallbackOnSent);
            this.mHttpClient.OnReceived += new ReceivedEventHandler(this.CallbackOnReceived);
            this.mHttpClient.OnNetworkError += new NetworkErrorEventHandler(this.CallbackOnError);
            return base.ShowDialog();
        }

        private void UserSetting()
        {
            UserEnvironment environment = UserEnvironment.CreateInstance();
            if (string.IsNullOrEmpty(environment.TerminalInfo.TermLogicalName))
            {
                this.mTermLogicalName = "";
            }
            else
            {
                this.mTermLogicalName = environment.TerminalInfo.TermLogicalName.Trim();
            }
            if (string.IsNullOrEmpty(environment.TerminalInfo.TermAccessKey))
            {
                this.mTermAccessKey = "";
            }
            else
            {
                this.mTermAccessKey = environment.TerminalInfo.TermAccessKey.Trim();
            }
        }

        private void View(ComParameter recvdata)
        {
            RecvData data = DataFactory.CreateRecvData(recvdata.DataString);
            if (data.OtherData == null)
            {
                if (data.ResultData.Items.Count > 0)
                {
                    HttpErrorDlg dlg = new HttpErrorDlg();
                    dlg.ShowJobError(data.ResultData.Header.JobCode, data.ResultData);
                    dlg.Dispose();
                }
            }
            else
            {
                if (this.mReportItems == null)
                {
                    this.mReportItems = new List<ListViewItem>();
                }
                else
                {
                    this.mReportItems.Clear();
                }
                this.lvReport.VirtualListSize = 0;
                this.lvReport.Items.Clear();
                for (int i = 0; i < data.OtherData.Items.Count; i += 2)
                {
                    string str2;
                    string str3;
                    string str4;
                    int index = data.OtherData.Items[i].IndexOf('.');
                    string path = data.OtherData.Items[i].Substring(0, index);
                    if (path.Length > 7)
                    {
                        str2 = path.Substring(0, 7);
                    }
                    else
                    {
                        str2 = path;
                    }
                    string[] strArray = path.Split(new char[] { '_' });
                    if (strArray.Length > 1)
                    {
                        try
                        {
                            DateTime time;
                            string str = strArray[strArray.Length - 1].ToString();
                            if (DateTime.TryParseExact(this.getDivFormConv(str, "____/__/__ __:__:__", ' '), "yyyy/MM/dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out time))
                            {
                                str3 = time.ToString();
                            }
                            else
                            {
                                str3 = "__/__/____ __:__:__";
                            }
                        }
                        catch
                        {
                            str3 = strArray[1];
                        }
                    }
                    else
                    {
                        str3 = "";
                    }
                    if ((i + 1) < data.OtherData.Items.Count)
                    {
                        try
                        {
                            long num3 = 0L;
                            num3 = long.Parse(data.OtherData.Items[i + 1].Trim());
                            str4 = string.Format("{0:n0}", Math.Ceiling((double) (((double) num3) / 1024.0)));
                        }
                        catch
                        {
                            str4 = data.OtherData.Items[i + 1].Trim();
                        }
                    }
                    else
                    {
                        str4 = "";
                    }
                    string fileName = this.mOutcodeTbl.Find(str2);
                    if (string.IsNullOrEmpty(fileName))
                    {
                        fileName = Path.GetFileName(path);
                    }
                    ListViewItem item = new ListViewItem(fileName);
                    item.SubItems.Add(str2);
                    item.SubItems.Add(str3);
                    item.SubItems.Add(str4);
                    item.SubItems.Add("");
                    item.SubItems.Add(data.OtherData.Items[i].Trim());
                    this.mReportItems.Add(item);
                }
                if (this.mReportItems.Count > 0)
                {
                    this.mReportItems.Sort(new Comparison<ListViewItem>(this.DefaultCompareItem));
                    this.lvReport.VirtualListSize = this.mReportItems.Count;
                    this.AllCheck();
                }
                else
                {
                    this.MenuStatusChange();
                }
            }
        }

        private enum RequestCommand
        {
            LST,
            GTN,
            GTP
        }
    }
}

