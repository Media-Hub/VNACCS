namespace Naccs.Core.DataView
{
    using Naccs.Common.Function;
    using Naccs.Common.JobConfig;
    using Naccs.Core.Classes;
    using Naccs.Core.Properties;
    using Naccs.Core.Settings;
    using Naccs.Net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;
    using System.Deployment.Application;

    public class UDataView : UserControl, IDataView
    {
        private Button btnSearch;
        private const int C_FOLDERLENGTH = 0x40;
        private const int C_HISTORYSEARCH = 20;
        private DataColumn clAttach;
        private DataColumn cldAirSea;
        private DataColumn clDataInfo;
        private DataColumn cldDelete;
        private DataColumn cldEnd;
        private DataColumn cldFolder;
        private DataColumn cldID;
        private DataColumn cldInputNo;
        private DataColumn cldJobCode;
        private DataColumn cldJobInfo;
        private DataColumn cldNum;
        private DataColumn cldOpen;
        private DataColumn cldOutCode;
        private DataColumn cldPattern;
        private DataColumn cldPrint;
        private DataColumn cldResCode;
        private DataColumn cldSave;
        private DataColumn cldStatus;
        private DataColumn cldSyu;
        private DataColumn cldTimeStamp;
        private DataColumn clFileName;
        private DataGridViewTextBoxColumn clmAirSea;
        private DataGridViewTextBoxColumn clmAttach;
        private DataGridViewTextBoxColumn clmDataInfo;
        private DataGridViewTextBoxColumn clmDelete;
        private DataGridViewTextBoxColumn clmEnd;
        private DataGridViewTextBoxColumn clmFileName;
        private DataGridViewTextBoxColumn clmFolder;
        private DataGridViewTextBoxColumn clmID;
        private DataGridViewTextBoxColumn clmInputNo;
        private DataGridViewTextBoxColumn clmJobCode;
        private DataGridViewTextBoxColumn clmJobInfo;
        private DataGridViewTextBoxColumn clmNum;
        private DataGridViewImageColumn clmOpen;
        private DataGridViewTextBoxColumn clmOpenFlag;
        private DataGridViewTextBoxColumn clmOutCode;
        private DataGridViewTextBoxColumn clmPattern;
        private DataGridViewImageColumn clmPrint;
        private DataGridViewTextBoxColumn clmPrintFlag;
        private DataGridViewTextBoxColumn clmResCode;
        private DataGridViewImageColumn clmSave;
        private DataGridViewTextBoxColumn clmSaveFlag;
        private DataGridViewImageColumn clmSign;
        private DataGridViewTextBoxColumn clmSignFilePath;
        private DataGridViewTextBoxColumn clmSignflg;
        private DataGridViewTextBoxColumn clmStatus;
        private DataGridViewTextBoxColumn clmSyu;
        private DataGridViewTextBoxColumn clmTimeStamp;
        private DataGridViewTextBoxColumn clmUserCode;
        private DataColumn clSign;
        private DataColumn clUserCode;
        private ComboBox cmbSearchDiv;
        private ComboBox cmbSearchText;
        private IContainer components;
        private DataColumn dataColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataTable dataTable1;
        private List<DBFix> DBFixList = new List<DBFix>();
        private List<DBSort> DBSortList = new List<DBSort>();
        private const string DefStr = "\\/:*?<>|\"'()";
        private DataGridView dgvDataView;
        private DataGridViewRow dgvr;
        private bool DragFlag;
        private DeleteRestore drProgress;
        private TreeNode dtn;
        private DataSet dtsDataView;
        private StringList FldList;
        private bool FOpenViewFlag;
        private FileSave fs;
        private StringList HSearchList;
        private ImageList imgDataview;
        private ImageList imlFolder;
        protected bool isDesigning = ((AppDomain.CurrentDomain.FriendlyName == "DefaultDomain") && !ApplicationDeployment.IsNetworkDeployed);
        private Label lblSearchDiv;
        private Label lblSearchText;
        private int MaxID;
        private MessageClassify msgc;
        private const string NOMAL_RECIVE_CODE = "00000";
        private const int NOMAL_RECIVE_CODE_FIGURES = 5;
        private TreeNode NoRepaintTreeNode;
        private OpenFileDialog ofdDataView;
        public PathInfo pi;
        private Panel pnlData;
        private Panel pnlDVSearch;
        private Panel pnlSearch;
        private Panel pnlSendRecvFolder;
        private DateTime prev_time = DateTime.Now;
        private readonly string RECIVE_FOLDER_PATH_STRING = (@"VNACCS\" + Resources.ResourceManager.GetString("CORE108"));
        private ToolStripMenuItem rmnAllSelect;
        private ToolStripMenuItem rmnChangeName;
        private ContextMenuStrip rmnDataView;
        private ToolStripMenuItem rmnDataViewRefresh;
        private ToolStripMenuItem rmnDelete;
        private ToolStripMenuItem rmnExport;
        private ToolStripMenuItem rmnFile;
        private ToolStripMenuItem rmnFileSave;
        private ToolStripMenuItem rmnFolderCreate;
        private ToolStripMenuItem rmnFolderDelete;
        private ToolStripMenuItem rmnImport;
        private ToolStripMenuItem rmnJobOpen;
        private ToolStripMenuItem rmnOpen;
        private ToolStripMenuItem rmnPrint;
        private ToolStripMenuItem rmnPrintPreview;
        private ToolStripMenuItem rmnRecvSearch;
        private ContextMenuStrip rmnSendRecvFolder;
        private ToolStripMenuItem rmnSendSearch;
        private ToolStripMenuItem rmnsignatureVerification;
        private ToolStripSeparator rmnSplitter1;
        private ToolStripSeparator rmnSplitter2;
        private ToolStripSeparator rmnSplitter3;
        private ToolStripSeparator rmnSplitter4;
        private ToolStripSeparator rmnSplitter5;
        private ToolStripSeparator rmnSplitter6;
        private ToolStripSeparator rmnSplitter7;
        private ToolStripMenuItem rmnUndo;
        private const string ROOT_FOLDER_PATH_STRING = @"VNACCS\";
        private StrFunc SF = StrFunc.CreateInstance();
        private SaveFileDialog sfdDataView;
        private Splitter splDataView;
        private ToolStripStatusLabel stpDataCount;
        private ToolStripStatusLabel stpSelectCount;
        private StatusStrip stSendRecvFolder;
        private TermListClass trmlist;
        private TreeView tvSendRecvFolder;
        private const string UseCharset = "UTF-8";
        private User usr;
        private UserEnvironment usrenv;

        public event OnJobFormOpenHandler OnJobFormOpen;

        public event OnPrintJobHandler OnPrintJob;

        public event OnUpdateDataViewItemsHandler OnUpdateDataViewItems;

        public UDataView()
        {
            this.InitializeComponent();
            this.dgvDataView.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dgvDataView_CellFormatting);
        }

        public void AllSelect()
        {
            this.dgvDataView.Focus();
            this.dgvDataView.SelectAll();
        }

        public string AppendJobData(IData Data, int Status, bool Flag, bool bkFlag, bool TMFlag)
        {
            if (!TMFlag)
            {
                Data.TimeStamp = DateTime.Now;
            }
            Data.Status = (DataStatus) Status;
            if (Data.UserFolder == null)
            {
                Data.UserFolder = "";
            }
            Data.ID = this.MaxID.ToString();
            this.MaxID++;
            DirectoryInfo info = new DirectoryInfo(this.pi.DataViewPath);
            if (!info.Exists)
            {
                info.Create();
            }
            string fName = Data.ID + Data.TimeStamp.ToString("yyyyMMddHHmmss", DateTimeFormatInfo.InvariantInfo) + ".txt";
            if (bkFlag && (Data.Status == DataStatus.Recept))
            {
                ULogClass.LogWrite("Recv Data File Save Start Name=[" + fName + "]");
            }
            try
            {
                if (!this.DataViewDataSave(Data, fName))
                {
                    return "-1";
                }
                if (bkFlag && (Data.Status == DataStatus.Recept))
                {
                    string str2 = "Failure in obtaining.";
                    try
                    {
                        FileInfo info2 = new FileInfo(this.pi.DataViewPath + fName);
                        str2 = info2.Length.ToString();
                    }
                    catch
                    {
                    }
                    ULogClass.LogWrite("Recv Data File Save End Name=[" + fName + "] Size=[" + str2 + "]");
                }
                if (!this.DataViewDataAdd(Data, fName, Flag))
                {
                    return "-1";
                }
                if ((bkFlag && this.msgc.DataView.AutoBackup) && (Data.Status == DataStatus.Recept))
                {
                    string backupPath = this.pi.BackupPath;
                    if (!Directory.Exists(backupPath))
                    {
                        Directory.CreateDirectory(backupPath);
                    }
                    string name = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo) + ".dat";
                    new DataCompress(backupPath, name).AddData(this.GetExportData(Data));
                }
            }
            finally
            {
                if (this.FOpenViewFlag && (Data.Status == DataStatus.Recept))
                {
                    if (Data.UserFolder != "")
                    {
                        this.ViewCountChange(Data.UserFolder);
                    }
                    else
                    {
                        this.ViewCountChange(this.GetFullPath(this.RECIVE_FOLDER_PATH_STRING));
                    }
                }
                this.stpCount();
            }
            return Data.ID;
        }

        public void AppendRecord()
        {
            string dir = this.fs.TypeList.getType("A").Dir;
            try
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                int num = 0;
                IData data = null;
                List<string[]> list = new List<string[]>();
                for (int i = 0; i < this.dgvDataView.Rows.Count; i++)
                {
                    if (this.dgvDataView.Rows[i].Selected && (this.dgvDataView.Rows[i].Cells["clmSyu"].Value.ToString() == "A"))
                    {
                        string[] item = new string[] { this.dgvDataView.Rows[i].Cells["clmID"].Value.ToString(), Path.Combine(this.pi.DataViewPath, this.dgvDataView.Rows[i].Cells["clmFileName"].Value.ToString()) };
                        list.Add(item);
                    }
                }
                string appendRecord = "";
                for (int j = 0; j < list.Count; j++)
                {
                    data = DataFactory.LoadFromDataView(list[j][0], list[j][1]);
                    appendRecord = this.GetAppendRecord(data);
                    string str2 = data.OutCode.Trim() + DateTime.Now.ToString("ddMMyyyy", DateTimeFormatInfo.InvariantInfo) + ".csv";
                    if (File.Exists(Path.Combine(dir, str2)))
                    {
                        StreamWriter writer = new StreamWriter(Path.Combine(dir, str2), true, Encoding.GetEncoding("UTF-8"));
                        writer.WriteLine(appendRecord);
                        writer.Close();
                    }
                    else
                    {
                        StreamWriter writer2 = new StreamWriter(Path.Combine(dir, str2), false, Encoding.GetEncoding("UTF-8"));
                        writer2.WriteLine(appendRecord);
                        writer2.Close();
                    }
                    this.SaveStatusChange(data.ID, true);
                    num++;
                }
                string message = num.ToString() + Resources.ResourceManager.GetString("CORE63");
                using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                {
                    form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Information, message);
                }
            }
            catch (Exception exception)
            {
                MessageDialog dialog = new MessageDialog();
                dialog.ShowMessage("E303", this.sfdDataView.FileName, null, exception);
                dialog.Dispose();
            }
        }

        public void AppendRecord(IData data, string FolderStr)
        {
            string path = FolderStr;
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string appendRecord = "";
                appendRecord = this.GetAppendRecord(data);
                string str2 = data.OutCode.Trim() + DateTime.Now.ToString("ddMMyyyy", DateTimeFormatInfo.InvariantInfo) + ".csv";
                if (File.Exists(Path.Combine(path, str2)))
                {
                    StreamWriter writer = new StreamWriter(Path.Combine(path, str2), true, Encoding.GetEncoding("UTF-8"));
                    writer.WriteLine(appendRecord);
                    writer.Close();
                }
                else
                {
                    StreamWriter writer2 = new StreamWriter(Path.Combine(path, str2), false, Encoding.GetEncoding("UTF-8"));
                    writer2.WriteLine(appendRecord);
                    writer2.Close();
                }
                this.SaveStatusChange(data.ID, true);
            }
            catch
            {
            }
        }

        private void AttachDelete(string AttachFolder)
        {
            if (AttachFolder.Trim() != "")
            {
                try
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(AttachFolder);
                    RemoveAttribute(dirInfo);
                    dirInfo.Delete(true);
                }
                catch
                {
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.dgvDataView.RowCount >= 1)
            {
                string name = "";
                if (this.cmbSearchDiv.SelectedIndex >= 0)
                {
                    this.HSearchList = this.HistoryListAdd(this.cmbSearchText.Text, this.HSearchList, 20);
                    this.SearchListSet();
                    for (int i = 0; i < this.dgvDataView.Columns.Count; i++)
                    {
                        if (this.dgvDataView.Columns[i].HeaderText == this.cmbSearchDiv.Text)
                        {
                            name = this.dgvDataView.Columns[i].Name;
                            break;
                        }
                    }
                    if (!(name == ""))
                    {
                        int num7;
                        if (this.dgvDataView.SelectedRows.Count <= 0)
                        {
                            num7 = -1;
                        }
                        else
                        {
                            int num8 = -1;
                            for (int j = 0; j < this.dgvDataView.RowCount; j++)
                            {
                                if (this.dgvDataView.Rows[j].Selected)
                                {
                                    num8 = j;
                                    break;
                                }
                            }
                            num7 = num8;
                            this.dgvDataView.ClearSelection();
                        }
                        bool flag3 = true;
                        for (bool flag4 = true; flag3; flag4 = false)
                        {
                            int num10;
                            if (flag4)
                            {
                                num10 = num7 + 1;
                            }
                            else
                            {
                                num10 = 0;
                            }
                            for (int k = num10; k < this.dgvDataView.RowCount; k++)
                            {
                                if (!flag4 && (k > num7))
                                {
                                    using (MessageDialog dialog4 = new MessageDialog())
                                    {
                                        dialog4.ShowMessage("I501", null, null);
                                    }
                                    return;
                                }
                                if (this.dgvDataView.Rows[k].Cells[name].Value.ToString().ToUpper().IndexOf(this.cmbSearchText.Text.ToUpper()) > -1)
                                {
                                    this.SelectClear();
                                    this.dgvDataView.Rows[k].Selected = true;
                                    this.dgvDataView.CurrentCell = this.dgvDataView.Rows[k].Cells[0];
                                    if (!this.dgvDataView.Rows[k].Displayed)
                                    {
                                        this.dgvDataView.FirstDisplayedScrollingRowIndex = this.dgvDataView.Rows[k].Index;
                                    }
                                    flag3 = false;
                                    return;
                                }
                                if (!flag4 && (k == num7))
                                {
                                    using (MessageDialog dialog5 = new MessageDialog())
                                    {
                                        dialog5.ShowMessage("I501", null, null);
                                    }
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        int num2;
                        if (this.dgvDataView.SelectedRows.Count <= 0)
                        {
                            num2 = 0;
                        }
                        else
                        {
                            int num3 = -1;
                            for (int m = 0; m < this.dgvDataView.RowCount; m++)
                            {
                                if (this.dgvDataView.Rows[m].Selected)
                                {
                                    num3 = m;
                                    break;
                                }
                            }
                            num2 = num3;
                            this.dgvDataView.ClearSelection();
                        }
                        bool flag = true;
                        for (bool flag2 = true; flag; flag2 = false)
                        {
                            int num5;
                            if (flag2)
                            {
                                num5 = num2 + 1;
                            }
                            else
                            {
                                num5 = 0;
                            }
                            for (int n = num5; n < this.dgvDataView.RowCount; n++)
                            {
                                if (!flag2 && (n > num2))
                                {
                                    using (MessageDialog dialog = new MessageDialog())
                                    {
                                        dialog.ShowMessage("I501", null, null);
                                    }
                                    return;
                                }
                                IData data = null;
                                try
                                {
                                    data = DataFactory.LoadFromDataView(this.dgvDataView.Rows[n].Cells["clmID"].Value.ToString(), Path.Combine(this.pi.DataViewPath, this.dgvDataView.Rows[n].Cells["clmFileName"].Value.ToString()));
                                }
                                catch (Exception exception)
                                {
                                    MessageDialog dialog2 = new MessageDialog();
                                    dialog2.ShowMessage("E402", this.dgvDataView.Rows[n].Cells["clmFileName"].Value.ToString(), null, exception);
                                    dialog2.Dispose();
                                    return;
                                }
                                if (data.GetDataString().ToUpper().IndexOf(this.cmbSearchText.Text.ToUpper()) > -1)
                                {
                                    this.SelectClear();
                                    this.dgvDataView.Rows[n].Selected = true;
                                    this.dgvDataView.CurrentCell = this.dgvDataView.Rows[n].Cells[0];
                                    if (!this.dgvDataView.Rows[n].Displayed)
                                    {
                                        this.dgvDataView.FirstDisplayedScrollingRowIndex = this.dgvDataView.Rows[n].Index;
                                    }
                                    flag = false;
                                    return;
                                }
                                if (!flag2 && (n == num2))
                                {
                                    using (MessageDialog dialog3 = new MessageDialog())
                                    {
                                        dialog3.ShowMessage("I501", null, null);
                                    }
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        public bool CheckFile(IData data)
        {
            return (this.dtsDataView.Tables["NACCS"].Select("clDataInfo = '" + data.Header.DataInfo + "' AND clSyu = '" + data.Header.DataType + "'").Length > 0);
        }

        public void ClearDelete()
        {
            this.drProgress = new DeleteRestore(0);
            this.drProgress.Show();
            try
            {
                DataRow[] rowArray = this.dtsDataView.Tables["NACCS"].Select("clDelete = 1");
                this.drProgress.pgbRestore.Minimum = 0;
                this.drProgress.pgbRestore.Maximum = rowArray.Length;
                for (int i = 0; i < rowArray.Length; i++)
                {
                    this.drProgress.pgbRestore.Value = i + 1;
                    this.drProgress.Refresh();
                    try
                    {
                        if (File.Exists(this.pi.DataViewPath + rowArray[i]["clFileName"].ToString()))
                        {
                            File.Delete(this.pi.DataViewPath + rowArray[i]["clFileName"].ToString());
                        }
                        if ((rowArray[i]["clSign"].ToString() != "0") && File.Exists(rowArray[i]["clSignPath"].ToString()))
                        {
                            File.Delete(rowArray[i]["clSignPath"].ToString());
                        }
                    }
                    catch (Exception exception)
                    {
                        string message = Resources.ResourceManager.GetString("CORE61") + Environment.NewLine + MessageDialog.CreateExceptionMessage(exception);
                        using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                        {
                            form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, message);
                        }
                    }
                    this.AttachDelete(rowArray[i]["clAttach"].ToString());
                    rowArray[i].Delete();
                }
                this.dtsDataView.AcceptChanges();
                this.dgvDataView.Refresh();
                this.stpCount();
            }
            finally
            {
                this.drProgress.Close();
                this.drProgress.Dispose();
            }
        }

        private void cmbSearchText_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && !((ComboBox) sender).DroppedDown)
            {
                this.btnSearch_Click(sender, null);
            }
        }

        private void cmbSearchText_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.IsInputKey = true;
            }
        }

        public virtual void DataDelete(int DNInt)
        {
            if (DNInt == -1)
            {
                int index = this.tvSendRecvFolder.SelectedNode.Index;
            }
            if ((this.tvSendRecvFolder.SelectedNode.Level != 0) && ((this.tvSendRecvFolder.SelectedNode.Level > 0) && (this.dgvDataView.SelectedRows != null)))
            {
                this.drProgress = new DeleteRestore(7);
                this.drProgress.Show();
                try
                {
                    this.drProgress.pgbRestore.Minimum = 0;
                    this.drProgress.pgbRestore.Maximum = this.dgvDataView.SelectedRows.Count;
                    IData data = null;
                    for (int i = this.dgvDataView.SelectedRows.Count - 1; i > -1; i--)
                    {
                        this.drProgress.pgbRestore.Value++;
                        this.drProgress.Refresh();
                        DataRow[] rowArray = this.dtsDataView.Tables["NACCS"].Select("clID = " + this.dgvDataView.SelectedRows[i].Cells["clmID"].Value);
                        rowArray[0]["clDelete"] = 1;
                        data = this.GetData(int.Parse(rowArray[0]["clID"].ToString()));
                        if (data == null)
                        {
                            rowArray[0].Delete();
                        }
                        else
                        {
                            data.IsDeleted = true;
                            this.DataViewDataSave(data, rowArray[0]["clFileName"].ToString());
                        }
                    }
                }
                catch (Exception exception)
                {
                    string message = Resources.ResourceManager.GetString("CORE58") + Environment.NewLine + MessageDialog.CreateExceptionMessage(exception);
                    using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                    {
                        form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, message);
                    }
                }
                finally
                {
                    this.dtsDataView.AcceptChanges();
                    this.dgvDataView.Refresh();
                    this.stpCount();
                    if (this.FOpenViewFlag)
                    {
                        this.ViewCountChange(this.GetFullPath(this.tvSendRecvFolder.SelectedNode.FullPath));
                    }
                    this.drProgress.Close();
                    this.drProgress.Dispose();
                }
            }
        }

        public void DataExport()
        {
            this.sfdDataView.Filter = "Export file（*.dat）|*.dat";
            if (this.sfdDataView.ShowDialog() == DialogResult.OK)
            {
                List<int> list = new List<int>();
                this.drProgress = new DeleteRestore(11);
                this.drProgress.Show();
                try
                {
                    this.drProgress.pgbRestore.Minimum = 0;
                    this.drProgress.pgbRestore.Maximum = this.dgvDataView.SelectedRows.Count;
                    this.drProgress.pgbRestore.Value = 0;
                    string directoryName = Path.GetDirectoryName(this.sfdDataView.FileName);
                    string fileName = Path.GetFileName(this.sfdDataView.FileName);
                    DataCompress compress = new DataCompress(directoryName, fileName);
                    compress.Clear();
                    IData data = null;
                    for (int i = 0; i < this.dgvDataView.RowCount; i++)
                    {
                        if (this.dgvDataView.Rows[i].Selected)
                        {
                            this.drProgress.pgbRestore.Value++;
                            this.drProgress.Refresh();
                            data = this.GetData(int.Parse(this.dgvDataView.Rows[i].Cells["clmID"].Value.ToString()));
                            if (data == null)
                            {
                                list.Add(int.Parse(this.dgvDataView.Rows[i].Cells["clmID"].Value.ToString()));
                            }
                            else
                            {
                                compress.AddData(this.GetExportData(data));
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                    {
                        form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, MessageDialog.CreateExceptionMessage(exception));
                    }
                }
                finally
                {
                    list.Sort();
                    for (int j = list.Count - 1; j > -1; j--)
                    {
                        this.RecordDelete(list[j]);
                    }
                    this.dtsDataView.AcceptChanges();
                    this.dgvDataView.Refresh();
                    this.stpCount();
                    this.drProgress.Close();
                    this.drProgress.Dispose();
                }
            }
        }

        public void DataImport()
        {
            this.ofdDataView.Filter = "Import file（*.dat）|*.dat";
            if (this.ofdDataView.ShowDialog() == DialogResult.OK)
            {
                string directoryName = Path.GetDirectoryName(this.ofdDataView.FileName);
                string fileName = Path.GetFileName(this.ofdDataView.FileName);
                this.drProgress = new DeleteRestore(4);
                this.drProgress.Show();
                try
                {
                    DataCompress compress = new DataCompress(directoryName, fileName);
                    int count = compress.IndexList.Count;
                    IData data = null;
                    this.drProgress.pgbRestore.Minimum = 0;
                    this.drProgress.pgbRestore.Maximum = count;
                    for (int i = 0; i < count; i++)
                    {
                        bool flag;
                        this.drProgress.pgbRestore.Value = i + 1;
                        this.drProgress.Refresh();
                        string s = compress.GetData(i);
                        data = this.SetExportData(s, out flag, true);
                        if (data != null)
                        {
                            data.UserFolder = this.FolderAddSet(data.UserFolder);
                            this.AppendJobData(data, (int) data.Status, false, false, flag);
                        }
                    }
                }
                catch (Exception exception)
                {
                    using (MessageDialog dialog = new MessageDialog())
                    {
                        dialog.ShowMessage("E406", null, null, exception);
                    }
                }
                finally
                {
                    this.drProgress.Close();
                    this.drProgress.Dispose();
                    this.SaveFolder();
                    this.dtsDataView.AcceptChanges();
                    this.dgvDataView.Refresh();
                    this.stpCount();
                    if (this.FOpenViewFlag)
                    {
                        this.ViewCountCheck(true);
                    }
                }
            }
        }

        public virtual void DataUndo()
        {
            if ((this.tvSendRecvFolder.SelectedNode.Index == 3) && (this.dgvDataView.SelectedRows != null))
            {
                this.drProgress = new DeleteRestore(8);
                this.drProgress.Show();
                StringList sl = new StringList();
                try
                {
                    this.drProgress.pgbRestore.Minimum = 0;
                    this.drProgress.pgbRestore.Maximum = this.dgvDataView.SelectedRows.Count;
                    IData data = null;
                    for (int i = this.dgvDataView.SelectedRows.Count - 1; i > -1; i--)
                    {
                        this.drProgress.pgbRestore.Value++;
                        this.drProgress.Refresh();
                        DataRow[] rowArray = this.dtsDataView.Tables["NACCS"].Select("clID = " + this.dgvDataView.SelectedRows[i].Cells["clmID"].Value);
                        rowArray[0]["clDelete"] = 0;
                        rowArray[0]["clFolder"] = this.FolderAddSet(rowArray[0]["clFolder"].ToString());
                        if (((rowArray[0]["clStatus"].ToString() == "r") && (rowArray[0]["clFolder"].ToString() != "")) && !this.ExistCheck(sl, rowArray[0]["clFolder"].ToString()))
                        {
                            sl.Add(rowArray[0]["clFolder"].ToString());
                        }
                        data = this.GetData(int.Parse(rowArray[0]["clID"].ToString()));
                        if (data == null)
                        {
                            rowArray[0].Delete();
                        }
                        else
                        {
                            data.IsDeleted = false;
                            data.UserFolder = rowArray[0]["clFolder"].ToString();
                            this.DataViewDataSave(data, rowArray[0]["clFileName"].ToString());
                        }
                    }
                }
                catch (Exception exception)
                {
                    string message = Resources.ResourceManager.GetString("CORE60") + Environment.NewLine + MessageDialog.CreateExceptionMessage(exception);
                    using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                    {
                        form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, message);
                    }
                }
                finally
                {
                    this.SaveFolder();
                    this.dtsDataView.AcceptChanges();
                    this.dgvDataView.Refresh();
                    this.stpCount();
                    if (this.FOpenViewFlag)
                    {
                        for (int j = 0; j < sl.Count; j++)
                        {
                            this.ViewCountChange(sl[j]);
                        }
                    }
                    this.drProgress.Close();
                    this.drProgress.Dispose();
                }
            }
        }

        public bool DataViewDataAdd(IData Data, string FName, bool Flag)
        {
            DataRow row = this.dtsDataView.Tables[0].NewRow();
            row["clID"] = Data.ID;
            row["clOpen"] = this.FlagCheck(Data.IsRead);
            row["clPrint"] = this.FlagCheck(Data.IsPrinted);
            row["clSave"] = this.FlagCheck(Data.IsSaved);
            row["clJobCode"] = Data.JobCode.Trim();
            if (Data.DispCode.Trim() != "")
            {
                row["clJobCode"] = row["clJobCode"] + "." + Data.DispCode;
            }
            row["clOutCode"] = Data.OutCode;
            row["clSyu"] = Data.Header.DataType;
            row["clNum"] = Data.Header.Div;
            row["clInputNo"] = Data.Header.InputInfo;
            row["clPattern"] = Data.Header.Pattern;
            string subject = Data.Header.Subject;
            row["clAirSea"] = GetASType(Data);
            if (Data.Status == DataStatus.Recept)
            {
                if (((Data.Header.DataType == "R") || (Data.Header.DataType == "M")) || (Data.Header.DataType == "U"))
                {
                    row["clResCode"] = subject.Substring(0, 15);
                    subject = subject.Remove(0, 0x10);
                }
                else
                {
                    row["clResCode"] = "";
                }
            }
            else
            {
                row["clResCode"] = "";
            }
            if (Data.Status == DataStatus.Recept)
            {
                row["clJobInfo"] = subject;
            }
            else
            {
                row["clJobInfo"] = "";
            }
            row["clTimeStamp"] = Data.TimeStamp.ToString("yyyy/MM/dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
            row["clEnd"] = Data.Header.EndFlag;
            row["clFolder"] = Data.UserFolder;
            if (Data.Status == DataStatus.Send)
            {
                row["clStatus"] = "s";
            }
            else if (Data.Status == DataStatus.Sent)
            {
                row["clStatus"] = "a";
            }
            else
            {
                row["clStatus"] = "r";
            }
            row["clDelete"] = this.FlagCheck(Data.IsDeleted);
            row["clFileName"] = FName;
            row["clDataInfo"] = Data.Header.DataInfo;
            row["clUserCode"] = Data.Header.UserId;
            row["clAttach"] = Data.AttachFolder;
            row["clSign"] = Data.Signflg;
            row["clSignPath"] = Data.SignFileFolder;
            this.dtsDataView.Tables[0].Rows.Add(row);
            if (Flag)
            {
                this.dtsDataView.AcceptChanges();
                this.dgvDataView.Refresh();
                return true;
            }
            return true;
        }

        public bool DataViewDataSave(IData Data, string FName)
        {
            try
            {
                DataFactory.SaveToDataView(Data, this.pi.DataViewPath + FName);
            }
            catch (Exception exception)
            {
                MessageDialog dialog = new MessageDialog();
                dialog.ShowMessage("E404", FName, null, exception);
                dialog.Dispose();
                return false;
            }
            return true;
        }

        public void DataViewDataUpdate(IData Data)
        {
            DataRow[] rowArray = this.dtsDataView.Tables["NACCS"].Select("clID = " + Data.ID);
            rowArray[0]["clID"] = Data.ID;
            if (Data.IsRead)
            {
                rowArray[0]["clOpen"] = "1";
            }
            else
            {
                rowArray[0]["clOpen"] = "0";
            }
            if (Data.IsPrinted)
            {
                rowArray[0]["clPrint"] = "1";
            }
            else
            {
                rowArray[0]["clPrint"] = "0";
            }
            if (Data.IsSaved)
            {
                rowArray[0]["clSave"] = "1";
            }
            else
            {
                rowArray[0]["clSave"] = "0";
            }
            rowArray[0]["clJobCode"] = Data.JobCode.Trim();
            if (Data.DispCode.Trim() != "")
            {
                rowArray[0]["clJobCode"] = rowArray[0]["clJobCode"] + "." + Data.DispCode;
            }
            rowArray[0]["clOutCode"] = Data.OutCode;
            rowArray[0]["clSyu"] = Data.Header.DataType;
            rowArray[0]["clNum"] = Data.Header.Div;
            rowArray[0]["clInputNo"] = Data.Header.InputInfo;
            rowArray[0]["clPattern"] = Data.Header.Pattern;
            string subject = Data.Header.Subject;
            rowArray[0]["clAirSea"] = GetASType(Data);
            if (Data.Status == DataStatus.Recept)
            {
                if (((Data.Header.DataType == "R") || (Data.Header.DataType == "M")) || (Data.Header.DataType == "U"))
                {
                    rowArray[0]["clResCode"] = subject.Substring(0, 15);
                    subject = subject.Remove(0, 0x10);
                }
                else
                {
                    rowArray[0]["clResCode"] = "";
                }
            }
            else
            {
                rowArray[0]["clResCode"] = "";
            }
            if (Data.Status == DataStatus.Recept)
            {
                rowArray[0]["clJobInfo"] = subject;
            }
            else
            {
                rowArray[0]["clJobInfo"] = "";
            }
            rowArray[0]["clTimeStamp"] = Data.TimeStamp.ToString("yyyy/MM/dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
            rowArray[0]["clEnd"] = Data.Header.EndFlag;
            rowArray[0]["clFolder"] = Data.UserFolder;
            if (Data.Status == DataStatus.Send)
            {
                rowArray[0]["clStatus"] = "s";
            }
            else if (Data.Status == DataStatus.Sent)
            {
                rowArray[0]["clStatus"] = "a";
            }
            else
            {
                rowArray[0]["clStatus"] = "r";
            }
            rowArray[0]["clDelete"] = Data.IsDeleted;
            rowArray[0]["clDataInfo"] = Data.Header.DataInfo;
            rowArray[0]["clUserCode"] = Data.Header.UserId;
            rowArray[0]["clAttach"] = Data.AttachFolder;
            this.dtsDataView.AcceptChanges();
            this.dgvDataView.Refresh();
        }

        public void DataViewFontChange(int sizeflag)
        {
            switch (sizeflag)
            {
                case 0:
                    this.dgvDataView.Font = new Font(this.dgvDataView.Font.Name, 7f);
                    break;

                case 2:
                    this.dgvDataView.Font = new Font(this.dgvDataView.Font.Name, 13f);
                    break;

                default:
                    this.dgvDataView.Font = new Font(this.dgvDataView.Font.Name, 9f);
                    break;
            }
            this.dgvDataView.AutoResizeRows();
        }

        public bool DataViewListSave(string strPath)
        {
            DirectoryInfo info;
            string fileName = "";
            if (strPath != "")
            {
                fileName = strPath + "Data.xml";
                info = new DirectoryInfo(strPath);
            }
            else
            {
                fileName = this.pi.DataViewPath + "Data.xml";
                info = new DirectoryInfo(this.pi.DataViewPath);
            }
            if (!info.Exists)
            {
                info.Create();
            }
            try
            {
                this.dtsDataView.WriteXml(fileName);
                File.SetLastWriteTime(fileName, DateTime.Now);
            }
            catch (Exception exception)
            {
                MessageDialog dialog = new MessageDialog();
                dialog.ShowMessage("E405", fileName, null, exception);
                dialog.Dispose();
                return false;
            }
            return true;
        }

        public void DataViewNoRepaint(bool ViewFlag)
        {
            if (ViewFlag)
            {
                this.NoRepaintTreeNode = this.tvSendRecvFolder.SelectedNode;
                this.tvSendRecvFolder.SelectedNode = this.tvSendRecvFolder.Nodes[0];
            }
            else
            {
                this.tvSendRecvFolder.SelectedNode = this.NoRepaintTreeNode;
            }
        }

        public virtual void DataViewRepaint(bool SelectFlag)
        {
            if (!this.DragFlag)
            {
                this.UpdMenuItems();
                if (this.tvSendRecvFolder.SelectedNode != null)
                {
                    DataTable table = this.dtsDataView.Tables[0];
                    int index = this.tvSendRecvFolder.SelectedNode.Index;
                    if ((index == 0) || (this.tvSendRecvFolder.SelectedNode.Level != 1))
                    {
                        this.rmnDataView.Items["rmnSplitter7"].Visible = true;
                        this.rmnDataView.Items["rmnsignatureVerification"].Visible = true;
                    }
                    else
                    {
                        this.rmnDataView.Items["rmnSplitter7"].Visible = false;
                        this.rmnDataView.Items["rmnsignatureVerification"].Visible = false;
                    }
                    string fullPath = this.GetFullPath(this.tvSendRecvFolder.SelectedNode.FullPath);
                    if ((index == 0) && (this.tvSendRecvFolder.SelectedNode.Text == "VNACCS"))
                    {
                        table.DefaultView.RowFilter = "clFolder = 'NACCS'";
                        this.dgvDataView.DataSource = table;
                        this.stpDataCount.Text = "0";
                    }
                    else
                    {
                        if (this.tvSendRecvFolder.SelectedNode.Level > 1)
                        {
                            index = 0;
                        }
                        string str2 = "";
                        string str3 = "";
                        for (int i = 0; i < this.DBSortList.Count; i++)
                        {
                            string name = this.DBSortList[i].Name;
                            if (name == null)
                            {
                                goto Label_01AF;
                            }
                            if (!(name == "clmOpenFlag"))
                            {
                                if (name == "clmPrintFlag")
                                {
                                    goto Label_019D;
                                }
                                if (name == "clmSaveFlag")
                                {
                                    goto Label_01A6;
                                }
                                goto Label_01AF;
                            }
                            str3 = "clmOpen";
                            goto Label_01C3;
                        Label_019D:
                            str3 = "clmPrint";
                            goto Label_01C3;
                        Label_01A6:
                            str3 = "clmSave";
                            goto Label_01C3;
                        Label_01AF:
                            str3 = this.DBSortList[i].Name;
                        Label_01C3:
                            str3 = "cl" + str3.Remove(0, 3);
                            if (str2 == "")
                            {
                                str2 = str3 + " ";
                            }
                            else
                            {
                                str2 = str2 + "," + str3 + " ";
                            }
                            if (this.DBSortList[i].Flag)
                            {
                                str2 = str2 + "ASC";
                            }
                            else
                            {
                                str2 = str2 + "DESC";
                            }
                        }
                        table.DefaultView.Sort = str2;
                        switch (index)
                        {
                            case 0:
                                if (this.tvSendRecvFolder.SelectedNode.Level >= 2)
                                {
                                    table.DefaultView.RowFilter = "clFolder = '" + fullPath + "' AND clDelete = 0";
                                    break;
                                }
                                table.DefaultView.RowFilter = "clFolder = '' AND clStatus = 'r' AND clDelete = 0";
                                break;

                            case 1:
                                table.DefaultView.RowFilter = "clStatus = 's' AND clDelete = 0";
                                break;

                            case 2:
                                table.DefaultView.RowFilter = "clStatus = 'a' AND clDelete = 0";
                                break;

                            case 3:
                                table.DefaultView.RowFilter = "clDelete = 1";
                                break;

                            default:
                                table.DefaultView.RowFilter = "";
                                break;
                        }
                        this.dgvDataView.DataSource = table;
                        this.stpCount();
                        if (SelectFlag && (this.dgvDataView.RowCount > 0))
                        {
                            this.dgvDataView.ClearSelection();
                            this.dgvDataView.Rows[0].Selected = true;
                            this.dgvDataView.CurrentCell = this.dgvDataView.Rows[0].Cells[0];
                            this.dgvDataView.FirstDisplayedScrollingRowIndex = 0;
                            this.dgvDataView.FirstDisplayedScrollingColumnIndex = 0;
                        }
                        this.dgvDataView.Refresh();
                    }
                }
            }
        }

        public void DataViewSaveRepaint()
        {
            this.dtsDataView.AcceptChanges();
            this.dgvDataView.Refresh();
        }

        private void DBFixAdd()
        {
            this.DBFixList.Clear();
            for (int i = 0; i < this.trmlist.Count; i++)
            {
                DBFix item = new DBFix {
                    ID = this.trmlist[i].Id,
                    Priority = this.trmlist[i].Priority,
                    UserCode = this.trmlist[i].UserCode,
                    JobCode = this.trmlist[i].JobCoode,
                    OutCode = this.trmlist[i].OutCode,
                    Folder = this.trmlist[i].Folder
                };
                if (this.trmlist[i].UnOpen)
                {
                    item.Open = "1";
                }
                else
                {
                    item.Open = "0";
                }
                this.DBFixList.Add(item);
            }
        }

        private void dgvDataView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewColumn column = this.dgvDataView.Columns[e.ColumnIndex];
            if ("clmTimeStamp".Equals(column.Name))
            {
                DataGridViewCell cell = this.dgvDataView[e.ColumnIndex, e.RowIndex];
                if ((cell != null) && (cell.Value != null))
                {
                    string str = DateTime.ParseExact(cell.Value.ToString(), "yyyy/MM/dd HH:mm:ss", Thread.CurrentThread.CurrentCulture).ToString("dd/MM/yyyy HH:mm:ss", Thread.CurrentThread.CurrentCulture);
                    e.Value = str;
                }
            }
        }

        protected virtual void dgvDataView_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                string str;
                string str4;
                if (e != null)
                {
                    switch (e.ColumnIndex)
                    {
                        case 0:
                            str = this.dgvDataView.Rows[e.RowIndex].Cells["clmOpenFlag"].Value.ToString();
                            if (!(str == "0"))
                            {
                                goto Label_0083;
                            }
                            e.Value = this.imgDataview.Images[5];
                            break;

                        case 1:
                            if (!(this.dgvDataView.Rows[e.RowIndex].Cells["clmPrintFlag"].Value.ToString() == "1"))
                            {
                                goto Label_0121;
                            }
                            e.Value = this.imgDataview.Images[3];
                            break;

                        case 2:
                            if (!(this.dgvDataView.Rows[e.RowIndex].Cells["clmSaveFlag"].Value.ToString() == "1"))
                            {
                                goto Label_0196;
                            }
                            e.Value = this.imgDataview.Images[2];
                            break;

                        case 3:
                            str4 = this.dgvDataView.Rows[e.RowIndex].Cells["clmSignFlg"].Value.ToString();
                            if (!(str4 == "1"))
                            {
                                goto Label_0209;
                            }
                            e.Value = this.imgDataview.Images[9];
                            break;
                    }
                }
                return;
            Label_0083:
                if (str == "1")
                {
                    e.Value = this.imgDataview.Images[6];
                }
                else
                {
                    e.Value = this.imgDataview.Images[4];
                }
                return;
            Label_0121:
                e.Value = this.imgDataview.Images[4];
                return;
            Label_0196:
                e.Value = this.imgDataview.Images[4];
                return;
            Label_0209:
                if (str4 == "2")
                {
                    e.Value = this.imgDataview.Images[10];
                }
                else if (str4 == "9")
                {
                    e.Value = this.imgDataview.Images[11];
                }
                else
                {
                    e.Value = this.imgDataview.Images[4];
                }
            }
            catch
            {
            }
        }

        private void dgvDataView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.DragFlag = false;
            if (this.DBSortList.Count == 0)
            {
                DBSort item = new DBSort {
                    ID = 1,
                    Name = this.dgvDataView.Columns[e.ColumnIndex].Name,
                    Flag = true
                };
                this.DBSortList.Add(item);
            }
            else if (this.DBSortList.Count == 1)
            {
                if (this.DBSortList[0].Name == this.dgvDataView.Columns[e.ColumnIndex].Name)
                {
                    DBSort sort2 = this.DBSortList[0];
                    sort2.Flag = !sort2.Flag;
                    this.DBSortList[0] = sort2;
                }
                else
                {
                    DBSort sort3 = new DBSort {
                        ID = 2,
                        Name = this.dgvDataView.Columns[e.ColumnIndex].Name,
                        Flag = true
                    };
                    this.DBSortList.Insert(0, sort3);
                }
            }
            else if (this.DBSortList.Count == 2)
            {
                if (this.DBSortList[0].Name == this.dgvDataView.Columns[e.ColumnIndex].Name)
                {
                    DBSort sort4 = this.DBSortList[0];
                    sort4.Flag = !sort4.Flag;
                    this.DBSortList[0] = sort4;
                }
                else if (this.DBSortList[1].Name == this.dgvDataView.Columns[e.ColumnIndex].Name)
                {
                    DBSort sort5 = new DBSort();
                    sort5 = this.DBSortList[0];
                    this.DBSortList.RemoveAt(0);
                    this.DBSortList.Add(sort5);
                }
                else
                {
                    this.DBSortList.RemoveAt(1);
                    DBSort sort6 = new DBSort {
                        ID = 1,
                        Name = this.dgvDataView.Columns[e.ColumnIndex].Name,
                        Flag = true
                    };
                    this.DBSortList.Insert(0, sort6);
                }
            }
            this.DataViewRepaint(false);
        }

        private void dgvDataView_DragDrop(object sender, DragEventArgs e)
        {
            this.DragFlag = false;
        }

        private void dgvDataView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    e.Handled = true;
                    if ((this.tvSendRecvFolder.SelectedNode.Index != 3) || (this.tvSendRecvFolder.SelectedNode.Level != 1))
                    {
                        this.rmnOpen_Click(sender, e);
                    }
                    break;

                case Keys.Delete:
                    this.DataDelete(-1);
                    break;
            }
        }

        private void dgvDataView_Leave(object sender, EventArgs e)
        {
            this.DragFlag = false;
        }

        protected virtual void dgvDataView_MouseDown(object sender, MouseEventArgs e)
        {
            if ((((e.Button & MouseButtons.Left) == MouseButtons.Left) && (this.dgvDataView.ColumnHeadersHeight < e.Y)) && (this.dgvDataView.HitTest(e.X, e.Y).RowIndex >= 0))
            {
                DateTime now = DateTime.Now;
                TimeSpan span = (TimeSpan) (now - this.prev_time);
                this.prev_time = now;
                if (((span.TotalMilliseconds <= SystemInformation.DoubleClickTime) && (this.dgvDataView.SelectedRows.Count > 0)) && (this.dgvr.Index == this.dgvDataView.HitTest(e.X, e.Y).RowIndex))
                {
                    if ((this.tvSendRecvFolder.SelectedNode.Index != 3) || (this.tvSendRecvFolder.SelectedNode.Level != 1))
                    {
                        this.DragFlag = false;
                        this.OnJobFormOpen(this.GetKeyID(), false);
                    }
                }
                else
                {
                    this.DragFlag = true;
                    this.dtn = this.tvSendRecvFolder.SelectedNode;
                    if (this.dgvDataView.SelectedRows.Count > 0)
                    {
                        this.dgvr = this.dgvDataView.Rows[this.dgvDataView.HitTest(e.X, e.Y).RowIndex];
                    }
                    base.DoDragDrop(this.dgvDataView.SelectedRows, DragDropEffects.Move);
                    SendMessage(this.dgvDataView.Handle, 0x2a3, 0, 0);
                }
            }
        }

        private void dgvDataView_MouseLeave(object sender, EventArgs e)
        {
            this.DragFlag = false;
        }

        private void dgvDataView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            string str = this.dgvDataView.Rows[e.RowIndex].Cells["clmOpenFlag"].Value.ToString();
            Font prototype = this.dgvDataView.DefaultCellStyle.Font;
            switch (str)
            {
                case "0":
                    if (this.GetCellValue(e.RowIndex, "clmStatus").ToLower().Equals("r") && this.GetCellValue(e.RowIndex, "clmDelete").ToLower().Equals("false"))
                    {
                        Font font2 = new Font(prototype, prototype.Style | FontStyle.Bold);
                        this.dgvDataView.Rows[e.RowIndex].DefaultCellStyle.Font = font2;
                    }
                    break;

                case "1":
                {
                    Font font3 = new Font(prototype, prototype.Style);
                    this.dgvDataView.Rows[e.RowIndex].DefaultCellStyle.Font = font3;
                    break;
                }
            }
            string cellValue = this.GetCellValue(e.RowIndex, "clmResCode");
            if ((this.GetCellValue(e.RowIndex, "clmSyu").ToLower().Equals("r") || this.GetCellValue(e.RowIndex, "clmSyu").ToLower().Equals("m")) && ((cellValue.Length < 5) || (cellValue.Substring(0, 5) != "00000")))
            {
                this.dgvDataView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
            }
            else
            {
                this.dgvDataView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = new Color();
            }
        }

        private void dgvDataView_SelectionChanged(object sender, EventArgs e)
        {
            this.stpSelectCount.Text = " selected : " + this.dgvDataView.SelectedRows.Count.ToString();
            this.UpdMenuItems();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void Distribute(IData data)
        {
            this.Distribute(false, data);
        }

        public void Distribute(bool SelFlag)
        {
            this.Distribute(SelFlag, null);
        }

        public void Distribute(bool SelFlag, IData data)
        {
            if ((!SelFlag || (this.dgvDataView.SelectedRows.Count > 0)) || (data != null))
            {
                StringList sl = new StringList();
                IData data2 = null;
                List<string> lst = new List<string>();
                if (SelFlag)
                {
                    for (int i = 0; this.dgvDataView.SelectedRows.Count > i; i++)
                    {
                        lst.Add(this.dgvDataView.SelectedRows[i].Cells["clmID"].Value.ToString());
                    }
                }
                bool flag = data == null;
                if (flag)
                {
                    this.drProgress = new DeleteRestore(12);
                    this.drProgress.Show();
                    this.drProgress.Refresh();
                }
                try
                {
                    string str;
                    DataRow[] rowArray;
                    IData data3 = null;
                    if (data == null)
                    {
                        DBFix dBFixStr = new DBFix {
                            JobCode = "",
                            OutCode = "",
                            UserCode = "",
                            Open = "1"
                        };
                        str = this.FixListString(dBFixStr, SelFlag, data, lst);
                        rowArray = this.dtsDataView.Tables["NACCS"].Select(str);
                        if (rowArray.Length == 0)
                        {
                            return;
                        }
                        this.drProgress.pgbRestore.Minimum = 0;
                        this.drProgress.pgbRestore.Maximum = rowArray.Length;
                        for (int k = 0; k < rowArray.Length; k++)
                        {
                            this.drProgress.pgbRestore.Value = k + 1;
                            this.drProgress.Refresh();
                            if (rowArray[k]["clFolder"].ToString() != "")
                            {
                                if (!this.ExistCheck(sl, rowArray[k]["clFolder"].ToString()))
                                {
                                    sl.Add(rowArray[k]["clFolder"].ToString());
                                }
                                rowArray[k]["clFolder"] = "";
                                data3 = this.GetData(int.Parse(rowArray[k]["clID"].ToString()));
                                if (data3 == null)
                                {
                                    rowArray[k].Delete();
                                }
                                else
                                {
                                    data3.UserFolder = "";
                                    this.DataViewDataSave(data3, rowArray[k]["clFileName"].ToString());
                                }
                            }
                        }
                        this.dtsDataView.AcceptChanges();
                    }
                    for (int j = this.DBFixList.Count - 1; j > -1; j--)
                    {
                        for (int m = 0; m < this.DBFixList.Count; m++)
                        {
                            if (this.DBFixList[m].Priority == (j + 1))
                            {
                                str = this.FixListString(this.DBFixList[m], SelFlag, data, lst);
                                rowArray = this.dtsDataView.Tables["NACCS"].Select(str);
                                if (rowArray.Length != 0)
                                {
                                    if (flag)
                                    {
                                        this.drProgress.pgbRestore.Minimum = 0;
                                        this.drProgress.pgbRestore.Maximum = rowArray.Length;
                                    }
                                    for (int n = 0; n < rowArray.Length; n++)
                                    {
                                        if (flag)
                                        {
                                            this.drProgress.pgbRestore.Value = n + 1;
                                            this.drProgress.Refresh();
                                        }
                                        if ((this.FOpenViewFlag && (rowArray[n]["clOpen"].ToString() == "0")) && (rowArray[n]["clFolder"].ToString() != this.DBFixList[m].Folder))
                                        {
                                            if ((rowArray[n]["clFolder"].ToString() != "") && !this.ExistCheck(sl, rowArray[n]["clFolder"].ToString()))
                                            {
                                                sl.Add(rowArray[n]["clFolder"].ToString());
                                            }
                                            if (!this.ExistCheck(sl, this.DBFixList[m].Folder))
                                            {
                                                sl.Add(this.DBFixList[m].Folder);
                                            }
                                        }
                                        rowArray[n]["clFolder"] = this.DBFixList[m].Folder;
                                        data2 = this.GetData(int.Parse(rowArray[n]["clID"].ToString()));
                                        if (data2 == null)
                                        {
                                            rowArray[n].Delete();
                                        }
                                        else
                                        {
                                            data2.UserFolder = this.DBFixList[m].Folder;
                                            this.DataViewDataSave(data2, rowArray[n]["clFileName"].ToString());
                                        }
                                    }
                                    this.dtsDataView.AcceptChanges();
                                }
                            }
                        }
                    }
                }
                finally
                {
                    this.dgvDataView.Refresh();
                    this.stpCount();
                    if (this.FOpenViewFlag)
                    {
                        this.ViewCountCheck(true);
                    }
                    if (flag)
                    {
                        this.drProgress.Close();
                        this.drProgress.Dispose();
                    }
                }
            }
        }

        private bool ExistCheck(StringList sl, string str)
        {
            for (int i = 0; i < sl.Count; i++)
            {
                string str2 = this.FolderStringCut(sl[i]);
                string str3 = this.FolderStringCut(str);
                if (Normalize.NormalizeComp(str2, str3, true))
                {
                    return true;
                }
            }
            return false;
        }

        private string FixListString(DBFix DBFixStr, bool SelFlag, IData data, List<string> lst)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("clStatus = 'r' AND clDelete = 0");
            if (DBFixStr.JobCode != "")
            {
                builder.Append(" AND clJobCode = '").Append(DBFixStr.JobCode).Append("'");
            }
            if (DBFixStr.OutCode != "")
            {
                builder.Append(" AND clOutCode Like '%").Append(DBFixStr.OutCode).Append("%'");
            }
            if (DBFixStr.UserCode != "")
            {
                builder.Append(" AND clUserCode Like '%").Append(DBFixStr.UserCode).Append("%'");
            }
            if (DBFixStr.Open == "0")
            {
                builder.Append(" AND clOpen = '1'");
            }
            if (SelFlag)
            {
                builder.Append(" AND clID IN (");
                for (int i = 0; lst.Count > i; i++)
                {
                    builder.Append(lst[i]).Append(',');
                }
                if (builder[builder.Length - 1] == ',')
                {
                    builder.Remove(builder.Length - 1, 1);
                }
                builder.Append(")");
            }
            else if (data != null)
            {
                builder.Append(" AND clID IN (").Append(data.ID).Append(")");
            }
            return builder.ToString();
        }

        private int FlagCheck(bool flag)
        {
            if (flag)
            {
                return 1;
            }
            return 0;
        }

        private bool FlagCheck(int i)
        {
            if (i == 0)
            {
                return false;
            }
            return true;
        }

        private string FolderAddSet(string FolderStr)
        {
            string str2;
            string str = FolderStr;
            TreeNode node = this.tvSendRecvFolder.Nodes[0];
            string text = "";
            int index = str.IndexOf(@"\");
            if (index > -1)
            {
                str2 = str.Substring(0, index);
                str = str.Remove(0, index + 1);
                text = node.Text;
            }
            else
            {
                str2 = str;
                str = "";
            }
            while (str != "")
            {
                bool flag = false;
                index = str.IndexOf(@"\");
                if (index > -1)
                {
                    str2 = str.Substring(0, index);
                    str = str.Remove(0, index + 1);
                }
                else
                {
                    str2 = str;
                    str = "";
                }
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    string str4 = this.FolderStringCut(this.GetFullPath(node.Nodes[i].Text));
                    string str5 = this.FolderStringCut(str2);
                    if (Normalize.NormalizeComp(str4, str5, true))
                    {
                        node = node.Nodes[i];
                        flag = true;
                        text = Path.Combine(text, this.GetFullPath(node.Text));
                        break;
                    }
                }
                if (!flag)
                {
                    node = node.Nodes.Add(str2);
                    node.Tag = 0;
                    node.ImageIndex = 5;
                    node.SelectedImageIndex = 6;
                    text = Path.Combine(text, this.GetFullPath(node.Text));
                }
            }
            return text;
        }

        public void FolderChangeName()
        {
            if (this.FOpenViewFlag)
            {
                this.tvSendRecvFolder.SelectedNode.Text = this.tvSendRecvFolder.SelectedNode.Text.Remove(this.tvSendRecvFolder.SelectedNode.Text.LastIndexOf('('));
            }
            this.tvSendRecvFolder.LabelEdit = true;
            this.tvSendRecvFolder.SelectedNode.BeginEdit();
        }

        public void FolderCreate()
        {
            int num = 0;
            bool flag = true;
            TreeNode tn = null;
            while (flag)
            {
                string str;
                if (num == 0)
                {
                    str = Resources.ResourceManager.GetString("C_DEFFOLDERNAME");
                }
                else
                {
                    str = Resources.ResourceManager.GetString("C_DEFFOLDERNAME") + num.ToString();
                }
                flag = false;
                for (int i = 0; i < this.tvSendRecvFolder.SelectedNode.Nodes.Count; i++)
                {
                    if (str == this.GetFullPath(this.tvSendRecvFolder.SelectedNode.Nodes[i].Text))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    tn = this.tvSendRecvFolder.SelectedNode.Nodes.Add(str);
                    tn.Tag = 0;
                    tn.ImageIndex = 5;
                    tn.SelectedImageIndex = 6;
                    if (this.FOpenViewFlag)
                    {
                        this.NoOpenSet(tn);
                    }
                }
                num++;
            }
            if (tn != null)
            {
                tn.Expand();
            }
            this.UpdMenuItems();
        }

        public void FolderDelete()
        {
            if (this.tvSendRecvFolder.SelectedNode.Nodes.Count > 0)
            {
                using (MessageDialog dialog = new MessageDialog())
                {
                    dialog.ShowMessage("W405", null, null);
                    return;
                }
            }
            if (this.FolderTermCheck(this.GetFullPath(this.tvSendRecvFolder.SelectedNode.FullPath)))
            {
                DialogResult result;
                using (MessageDialog dialog2 = new MessageDialog())
                {
                    result = dialog2.ShowMessage("C406", null, null);
                }
                if (result != DialogResult.Yes)
                {
                    return;
                }
                this.FolderTermDelete(this.GetFullPath(this.tvSendRecvFolder.SelectedNode.FullPath));
            }
            try
            {
                DataRow[] rowArray = this.dtsDataView.Tables["NACCS"].Select("clFolder = '" + this.GetFullPath(this.tvSendRecvFolder.SelectedNode.FullPath) + "'");
                IData data = null;
                for (int i = 0; i < rowArray.Length; i++)
                {
                    rowArray[i]["clDelete"] = 1;
                    data = this.GetData(int.Parse(rowArray[i]["clID"].ToString()));
                    if (data == null)
                    {
                        rowArray[i].Delete();
                    }
                    else
                    {
                        data.IsDeleted = true;
                        this.DataViewDataSave(data, rowArray[i]["clFileName"].ToString());
                    }
                }
                this.tvSendRecvFolder.SelectedNode.Remove();
            }
            catch (Exception exception)
            {
                string message = Resources.ResourceManager.GetString("CORE59") + Environment.NewLine + MessageDialog.CreateExceptionMessage(exception);
                using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                {
                    form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, message);
                }
            }
            finally
            {
                this.dtsDataView.AcceptChanges();
                this.dgvDataView.Refresh();
                this.stpCount();
            }
        }

        private bool FolderNameCheck(string Str)
        {
            for (int i = 0; i < "\\/:*?<>|\"'()".Length; i++)
            {
                if (Str.IndexOf("\\/:*?<>|\"'()"[i]) > -1)
                {
                    return false;
                }
            }
            return true;
        }

        private void FolderSet()
        {
            this.tvSendRecvFolder.Nodes[0].Nodes[0].Nodes.Clear();
            for (int i = this.trmlist.Count - 1; i > -1; i--)
            {
                bool flag = false;
                for (int k = 0; k < this.FldList.Count; k++)
                {
                    if (this.trmlist[i].Folder == this.FldList[k])
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    this.trmlist.RemoveAt(this.trmlist[i].Id);
                }
            }
            for (int j = 0; j < this.FldList.Count; j++)
            {
                this.FolderAddSet(this.FldList[j]);
            }
        }

        private string FolderStringCut(string fldstr)
        {
            string str = fldstr;
            return str.Replace("　", " ").Trim();
        }

        private bool FolderTermCheck(string FPath)
        {
            for (int i = 0; i < this.trmlist.Count; i++)
            {
                if (this.trmlist[i].Folder == FPath)
                {
                    return true;
                }
            }
            return false;
        }

        private void FolderTermDelete(string FPath)
        {
            for (int i = this.trmlist.Count - 1; i > -1; i--)
            {
                if (this.trmlist[i].Folder == FPath)
                {
                    this.trmlist.RemoveAt(this.trmlist[i].Id);
                }
            }
        }

        private string GetAppendRecord(IData data)
        {
            string str3;
            string jobData = data.JobData;
            string str2 = "";
            StringReader reader = new StringReader(jobData);
            while ((str3 = reader.ReadLine()) != null)
            {
                if (str2 == "")
                {
                    str2 = str3;
                }
                else
                {
                    str2 = str2 + "," + str3;
                }
            }
            return str2;
        }

        public static string GetASType(IData Data)
        {
            string str = "";
            try
            {
                if (Data.Status == DataStatus.Recept)
                {
                    if (Data.Header.OutCode.StartsWith("*"))
                    {
                        if (Data.Header.OutCode.Length > 1)
                        {
                            str = Data.Header.OutCode.Substring(1, 1);
                        }
                        return str;
                    }
                    if (Data.Header.OutCode.Length > 0)
                    {
                        str = Data.Header.OutCode.Substring(0, 1);
                    }
                    return str;
                }
                if (Data.Header.System == "1")
                {
                    return "A";
                }
                if (Data.Header.System == "2")
                {
                    return "S";
                }
                return Data.Header.System;
            }
            catch
            {
                return "";
            }
        }

        private string GetCellValue(int rowIndex, string columName)
        {
            object obj2 = this.dgvDataView.Rows[rowIndex].Cells[columName].Value;
            string str = "";
            if (obj2 != null)
            {
                str = obj2.ToString();
            }
            return str;
        }

        public int GetCount(int Status)
        {
            return this.GetCount(Status, -1);
        }

        public int GetCount(int Status, int SaveDate)
        {
            DataRow[] rowArray = null;
            DateTime time = DateTime.Now.AddDays((double) -SaveDate);
            switch (Status)
            {
                case 0:
                    rowArray = this.dtsDataView.Tables["NACCS"].Select("clStatus = 'r' AND clDelete = 0");
                    break;

                case 1:
                    rowArray = this.dtsDataView.Tables["NACCS"].Select("clStatus = 's' AND clDelete = 0");
                    break;

                case 2:
                    rowArray = this.dtsDataView.Tables["NACCS"].Select("clStatus = 'a' AND clDelete = 0");
                    break;

                case 3:
                    rowArray = this.dtsDataView.Tables["NACCS"].Select("clDelete = 1");
                    break;

                case 4:
                    rowArray = this.dtsDataView.Tables["NACCS"].Select("clDelete = 0 AND clTimeStamp < '" + time.ToString("yyyy/MM/dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo) + "' AND (clStatus = 'a' OR (clStatus = 'r' AND clOpen = '1'))");
                    break;

                case 5:
                    rowArray = this.dtsDataView.Tables["NACCS"].Select("clStatus = 's' AND clDelete = 0 AND clSign <> 1", "clID ASC");
                    break;
            }
            return rowArray.Length;
        }

        public IData GetData(int key)
        {
            if (key > -1)
            {
                IData data = null;
                DataRow[] rowArray = this.dtsDataView.Tables["NACCS"].Select("clID = " + key.ToString());
                try
                {
                    data = DataFactory.LoadFromDataView(key.ToString(), Path.Combine(this.pi.DataViewPath, rowArray[0]["clFileName"].ToString()));
                }
                catch (Exception exception)
                {
                    MessageDialog dialog = new MessageDialog();
                    dialog.ShowMessage("E402", rowArray[0]["clFileName"].ToString(), null, exception);
                    dialog.Dispose();
                }
                return data;
            }
            if (this.dgvDataView.SelectedRows.Count <= 0)
            {
                return null;
            }
            IData data2 = null;
            int num = -1;
            for (int i = 0; i < this.dgvDataView.RowCount; i++)
            {
                if (this.dgvDataView.Rows[i].Selected)
                {
                    num = i;
                    break;
                }
            }
            try
            {
                data2 = DataFactory.LoadFromDataView(this.dgvDataView.Rows[num].Cells["clmID"].Value.ToString(), Path.Combine(this.pi.DataViewPath, this.dgvDataView.Rows[num].Cells["clmFileName"].Value.ToString()));
            }
            catch (Exception exception2)
            {
                MessageDialog dialog2 = new MessageDialog();
                dialog2.ShowMessage("E402", this.dgvDataView.Rows[num].Cells["clmFileName"].Value.ToString(), null, exception2);
                dialog2.Dispose();
            }
            return data2;
        }

        public virtual ColumnWidthClass GetDataViewColum()
        {
            ColumnWidthClass columnWidth = this.usr.ColumnWidth;
            columnWidth.JobCode = this.dgvDataView.Columns["clmJobCode"].Width;
            columnWidth.OutCode = this.dgvDataView.Columns["clmOutCode"].Width;
            columnWidth.InputInfo = this.dgvDataView.Columns["clmInputNo"].Width;
            columnWidth.ResCode = this.dgvDataView.Columns["clmResCode"].Width;
            columnWidth.Pattern = this.dgvDataView.Columns["clmPattern"].Width;
            columnWidth.JobInfo = this.dgvDataView.Columns["clmJobInfo"].Width;
            columnWidth.TimeStamp = this.dgvDataView.Columns["clmTimeStamp"].Width;
            columnWidth.DataType = this.dgvDataView.Columns["clmSyu"].Width;
            columnWidth.EndFlag = this.dgvDataView.Columns["clmEnd"].Width;
            columnWidth.AirSea = this.dgvDataView.Columns["clmAirSea"].Width;
            return columnWidth;
        }

        public virtual string GetDataViewListData()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < this.dgvDataView.RowCount; i++)
            {
                int num2;
                if (this.dgvDataView.Rows[i].Cells["clmOpenFlag"].Value.ToString() == "1")
                {
                    builder.Append("*").Append(Environment.NewLine);
                }
                else
                {
                    builder.Append(" ").Append(Environment.NewLine);
                }
                if (this.dgvDataView.Rows[i].Cells["clmPrintFlag"].Value.ToString() == "1")
                {
                    builder.Append("*").Append(Environment.NewLine);
                }
                else
                {
                    builder.Append(" ").Append(Environment.NewLine);
                }
                if (this.dgvDataView.Rows[i].Cells["clmSaveFlag"].Value.ToString() == "1")
                {
                    builder.Append("*").Append(Environment.NewLine);
                }
                else
                {
                    builder.Append(" ").Append(Environment.NewLine);
                }
                string str3 = this.dgvDataView.Rows[i].Cells["clmSignflg"].Value.ToString();
                if (str3 == null)
                {
                    goto Label_01DC;
                }
                if (!(str3 == "1"))
                {
                    if (str3 == "2")
                    {
                        goto Label_01AC;
                    }
                    if (str3 == "9")
                    {
                        goto Label_01C4;
                    }
                    goto Label_01DC;
                }
                builder.Append("1").Append(Environment.NewLine);
                goto Label_01F2;
            Label_01AC:
                builder.Append("2").Append(Environment.NewLine);
                goto Label_01F2;
            Label_01C4:
                builder.Append("E").Append(Environment.NewLine);
                goto Label_01F2;
            Label_01DC:
                builder.Append(" ").Append(Environment.NewLine);
            Label_01F2:
                num2 = this.dgvDataView.Columns["clmJobCode"].Index;
                while (num2 <= this.dgvDataView.Columns["clmEnd"].Index)
                {
                    string name = this.dgvDataView.Columns[num2].Name;
                    try
                    {
                        if ((this.dgvDataView.Rows[i].Cells[name].Value != null) && !(this.dgvDataView.Rows[i].Cells[name].Value is DBNull))
                        {
                            if ("clmTimeStamp".Equals(name))
                            {
                                string s = (string) this.dgvDataView.Rows[i].Cells[name].Value;
                                s = DateTime.ParseExact(s, "yyyy/MM/dd HH:mm:ss", Thread.CurrentThread.CurrentCulture).ToString("dd/MM/yyyy HH:mm:ss", Thread.CurrentThread.CurrentCulture);
                                builder.Append(s).Append(Environment.NewLine);
                            }
                            else
                            {
                                builder.Append((string) this.dgvDataView.Rows[i].Cells[name].Value).Append(Environment.NewLine);
                            }
                        }
                        else
                        {
                            builder.Append("").Append(Environment.NewLine);
                        }
                    }
                    catch
                    {
                        builder.Append("").Append(Environment.NewLine);
                    }
                    num2++;
                }
            }
            return builder.ToString();
        }

        public IData GetDivData(string DataInf, int Flag, string JCode, string OCode)
        {
            DataRow[] rowArray = this.dtsDataView.Tables["NACCS"].Select("clStatus = 'r' AND clDataInfo = '" + DataInf + "'");
            for (int i = 0; i < rowArray.Length; i++)
            {
                if ((OCode == "") || rowArray[i]["clOutCode"].ToString().Trim().StartsWith(OCode))
                {
                    switch (Flag)
                    {
                        case 0:
                            if (!(rowArray[i]["clSyu"].ToString() == "R"))
                            {
                                break;
                            }
                            return this.GetData(int.Parse(rowArray[i]["clID"].ToString()));

                        case 1:
                            if (!(rowArray[i]["clSyu"].ToString() == "C"))
                            {
                                break;
                            }
                            return this.GetData(int.Parse(rowArray[i]["clID"].ToString()));
                    }
                }
            }
            if (Flag == 1)
            {
                rowArray = this.dtsDataView.Tables["NACCS"].Select("clStatus = 'a' AND clDataInfo = '" + DataInf + "'");
                if (rowArray.Length > 0)
                {
                    string str = JCode;
                    string str2 = "";
                    if (str.IndexOf('.') > -1)
                    {
                        str2 = str;
                        str = str.Remove(str.IndexOf('.'));
                        str2 = str2.Remove(0, str2.IndexOf('.') + 1);
                    }
                    if (JCode == "")
                    {
                        return this.GetData(int.Parse(rowArray[0]["clID"].ToString()));
                    }
                    IData data = new NaccsData();
                    IData data2 = this.GetData(int.Parse(rowArray[0]["clID"].ToString()));
                    data2.Style = JobStyle.combi;
                    if (data2.JobCode.Trim() == str)
                    {
                        return data2;
                    }
                    for (int j = 0; j < data2.JobCount; j++)
                    {
                        data2.JobIndex = j;
                        if (data2.SubCode.Trim() == str)
                        {
                            data.Header.JobCode = str;
                            data.Header.DispCode = str2;
                            data.Header.InputInfo = data2.Header.InputInfo;
                            StringBuilder builder = new StringBuilder();
                            for (int k = 0; k < data2.SubItems.Count; k++)
                            {
                                builder.Append(data2.SubItems[k]).Append(Environment.NewLine);
                            }
                            string str3 = builder.ToString();
                            if (str3.LastIndexOf(Environment.NewLine) > 0)
                            {
                                str3 = str3.Remove(str3.LastIndexOf(Environment.NewLine));
                            }
                            data.JobData = str3;
                            return data;
                        }
                    }
                }
            }
            return null;
        }

        private string GetExportData(IData data)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(data.TimeStamp.ToString("yyyyMMddHHmmss", DateTimeFormatInfo.InvariantInfo));
            builder.AppendLine(((int) data.Status).ToString());
            builder.AppendLine(data.UserFolder);
            builder.AppendLine(this.FlagCheck(data.IsRead).ToString());
            builder.AppendLine(this.FlagCheck(data.IsPrinted).ToString());
            builder.AppendLine(this.FlagCheck(data.IsSaved).ToString());
            builder.AppendLine(this.FlagCheck(data.IsDeleted).ToString());
            builder.AppendLine(data.SignFileFolder).ToString();
            builder.Append(data.GetDataString());
            return builder.ToString();
        }

        public StringList GetFldList()
        {
            this.FldList.Clear();
            TreeNode tn = this.tvSendRecvFolder.Nodes[0].Nodes[0];
            this.SetFldNode(tn);
            return this.FldList;
        }

        private TreeNode GetFolderPathNode(string FolderStr)
        {
            string str2;
            string str = FolderStr;
            TreeNode node = null;
            int index = str.IndexOf(@"\");
            if (index > -1)
            {
                node = this.tvSendRecvFolder.Nodes[0];
                str2 = str.Substring(0, index);
                str = str.Remove(0, index + 1);
            }
            else
            {
                str2 = str;
                str = "";
            }
            while (str != "")
            {
                index = str.IndexOf(@"\");
                if (index > -1)
                {
                    str2 = str.Substring(0, index);
                    str = str.Remove(0, index + 1);
                }
                else
                {
                    str2 = str;
                    str = "";
                }
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    if (this.GetFullPath(node.Nodes[i].Text) == str2)
                    {
                        node = node.Nodes[i];
                        continue;
                    }
                }
            }
            return node;
        }

        private string GetFullPath(string pathstr)
        {
            try
            {
                if (!this.FOpenViewFlag)
                {
                    return pathstr;
                }
                string str = pathstr;
                string str2 = "";
                while (str.IndexOf(@"\") > -1)
                {
                    str2 = str2 + str.Substring(0, str.IndexOf(@"\"));
                    if (str2.LastIndexOf('(') > -1)
                    {
                        str2 = str2.Remove(str2.LastIndexOf('(')) + @"\";
                    }
                    else
                    {
                        str2 = str2 + @"\";
                    }
                    str = str.Remove(0, str.IndexOf(@"\") + 1);
                }
                if (str.LastIndexOf('(') > -1)
                {
                    str2 = str2 + str.Remove(str.LastIndexOf('('));
                }
                else
                {
                    str2 = str2 + str;
                }
                return str2;
            }
            catch
            {
                return pathstr;
            }
        }

        public int GetID()
        {
            if (this.dtsDataView.Tables[0].Rows.Count > 0)
            {
                return (int.Parse(this.dtsDataView.Tables[0].Compute("Max(clID)", null).ToString()) + 1);
            }
            return 0;
        }

        public int GetKeyID()
        {
            if (this.dgvDataView.SelectedRows.Count > 0)
            {
                for (int i = 0; i < this.dgvDataView.RowCount; i++)
                {
                    if (this.dgvDataView.Rows[i].Selected)
                    {
                        return int.Parse(this.dgvDataView.Rows[i].Cells["clmID"].Value.ToString());
                    }
                }
            }
            return -1;
        }

        private string GetSaveFolder(IData data)
        {
            if (data != null)
            {
                if (data.Status != DataStatus.Recept)
                {
                    return this.fs.SendFile.SendUser;
                }
                for (int i = 0; i < this.fs.TypeList.Count; i++)
                {
                    if (data.Header.DataType == this.fs.TypeList[i].DataType)
                    {
                        return this.fs.TypeList[i].Dir;
                    }
                }
            }
            return "";
        }

        public List<string> GetSelectKeyList()
        {
            List<string> list = new List<string>();
            if (this.dgvDataView.SelectedRows.Count > 0)
            {
                for (int i = 0; i < this.dgvDataView.RowCount; i++)
                {
                    if (this.dgvDataView.Rows[i].Selected)
                    {
                        list.Add(this.dgvDataView.Rows[i].Cells["clmID"].Value.ToString());
                    }
                }
            }
            return list;
        }

        public int GetSendKey(bool flg = true)
        {
            int num = -1;
            DataRow[] rowArray = null;
            if (flg)
            {
                rowArray = this.dtsDataView.Tables["NACCS"].Select("clStatus = 's' AND clDelete = 0", "clSign DESC, clID ASC");
            }
            else
            {
                rowArray = this.dtsDataView.Tables["NACCS"].Select("clStatus = 's' AND clDelete = 0 AND clSign <> 1", "clID ASC");
            }
            if (rowArray.Length > 0)
            {
                num = int.Parse(rowArray[0]["clID"].ToString());
            }
            return num;
        }

        public int GetSplitter()
        {
            return this.tvSendRecvFolder.Width;
        }

        private StringList HistoryListAdd(string str, StringList stl, int MaxInt)
        {
            for (int i = 0; i < stl.Count; i++)
            {
                if (stl[i] == str)
                {
                    stl.RemoveAt(i);
                    break;
                }
            }
            if (stl.Count >= MaxInt)
            {
                stl.RemoveAt(0);
            }
            stl.Add(str);
            return stl;
        }

        protected void InhDoDragDrop()
        {
            base.DoDragDrop(this.dgvDataView.SelectedRows, DragDropEffects.Move);
            SendMessage(this.dgvDataView.Handle, 0x2a3, 0, 0);
        }

        protected bool InhExistCheck(StringList sl, string str)
        {
            return this.ExistCheck(sl, str);
        }

        protected string InhFolderAdd(string folder)
        {
            return this.FolderAddSet(folder);
        }

        protected string InhGetFullPath(string path)
        {
            return this.GetFullPath(path);
        }

        protected void InhJobFormOpen()
        {
            if (this.OnJobFormOpen != null)
            {
                this.OnJobFormOpen(this.GetKeyID(), false);
            }
        }

        protected void InhResetCount()
        {
            this.stpDataCount.Text = "0";
        }

        protected void InhSaveFolder()
        {
            this.SaveFolder();
        }

        protected void InhSetCount()
        {
            this.stpCount();
        }

        protected void InhUpdateDataViewItems()
        {
            if (this.OnUpdateDataViewItems != null)
            {
                this.OnUpdateDataViewItems(this, this.rmnFolderCreate.Enabled, this.rmnChangeName.Enabled, this.rmnFolderDelete.Enabled, this.rmnDelete.Enabled, this.rmnUndo.Enabled, this.rmnSendSearch.Enabled, this.rmnRecvSearch.Enabled, this.rmnExport.Enabled);
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(UDataView));
            this.pnlData = new Panel();
            this.pnlDVSearch = new Panel();
            this.dgvDataView = new DataGridView();
            this.clmOpen = new DataGridViewImageColumn();
            this.clmPrint = new DataGridViewImageColumn();
            this.clmSave = new DataGridViewImageColumn();
            this.clmSign = new DataGridViewImageColumn();
            this.clmJobCode = new DataGridViewTextBoxColumn();
            this.clmOutCode = new DataGridViewTextBoxColumn();
            this.clmInputNo = new DataGridViewTextBoxColumn();
            this.clmPattern = new DataGridViewTextBoxColumn();
            this.clmResCode = new DataGridViewTextBoxColumn();
            this.clmJobInfo = new DataGridViewTextBoxColumn();
            this.clmTimeStamp = new DataGridViewTextBoxColumn();
            this.clmSyu = new DataGridViewTextBoxColumn();
            this.clmEnd = new DataGridViewTextBoxColumn();
            this.clmID = new DataGridViewTextBoxColumn();
            this.clmFolder = new DataGridViewTextBoxColumn();
            this.clmNum = new DataGridViewTextBoxColumn();
            this.clmStatus = new DataGridViewTextBoxColumn();
            this.clmOpenFlag = new DataGridViewTextBoxColumn();
            this.clmSaveFlag = new DataGridViewTextBoxColumn();
            this.clmPrintFlag = new DataGridViewTextBoxColumn();
            this.clmDelete = new DataGridViewTextBoxColumn();
            this.clmFileName = new DataGridViewTextBoxColumn();
            this.clmDataInfo = new DataGridViewTextBoxColumn();
            this.clmUserCode = new DataGridViewTextBoxColumn();
            this.clmAttach = new DataGridViewTextBoxColumn();
            this.clmSignFilePath = new DataGridViewTextBoxColumn();
            this.clmSignflg = new DataGridViewTextBoxColumn();
            this.clmAirSea = new DataGridViewTextBoxColumn();
            this.rmnDataView = new ContextMenuStrip(this.components);
            this.rmnOpen = new ToolStripMenuItem();
            this.rmnJobOpen = new ToolStripMenuItem();
            this.rmnSplitter1 = new ToolStripSeparator();
            this.rmnPrint = new ToolStripMenuItem();
            this.rmnPrintPreview = new ToolStripMenuItem();
            this.rmnSplitter2 = new ToolStripSeparator();
            this.rmnFile = new ToolStripMenuItem();
            this.rmnFileSave = new ToolStripMenuItem();
            this.rmnSplitter4 = new ToolStripSeparator();
            this.rmnUndo = new ToolStripMenuItem();
            this.rmnDelete = new ToolStripMenuItem();
            this.rmnAllSelect = new ToolStripMenuItem();
            this.rmnSplitter3 = new ToolStripSeparator();
            this.rmnDataViewRefresh = new ToolStripMenuItem();
            this.rmnSplitter5 = new ToolStripSeparator();
            this.rmnImport = new ToolStripMenuItem();
            this.rmnExport = new ToolStripMenuItem();
            this.rmnSplitter6 = new ToolStripSeparator();
            this.rmnSendSearch = new ToolStripMenuItem();
            this.rmnRecvSearch = new ToolStripMenuItem();
            this.rmnSplitter7 = new ToolStripSeparator();
            this.rmnsignatureVerification = new ToolStripMenuItem();
            this.pnlSearch = new Panel();
            this.btnSearch = new Button();
            this.cmbSearchDiv = new ComboBox();
            this.lblSearchDiv = new Label();
            this.cmbSearchText = new ComboBox();
            this.lblSearchText = new Label();
            this.splDataView = new Splitter();
            this.pnlSendRecvFolder = new Panel();
            this.tvSendRecvFolder = new TreeView();
            this.rmnSendRecvFolder = new ContextMenuStrip(this.components);
            this.rmnFolderCreate = new ToolStripMenuItem();
            this.rmnChangeName = new ToolStripMenuItem();
            this.rmnFolderDelete = new ToolStripMenuItem();
            this.imlFolder = new ImageList(this.components);
            this.stSendRecvFolder = new StatusStrip();
            this.stpDataCount = new ToolStripStatusLabel();
            this.stpSelectCount = new ToolStripStatusLabel();
            this.dtsDataView = new DataSet();
            this.dataTable1 = new DataTable();
            this.cldAirSea = new DataColumn();
            this.cldJobCode = new DataColumn();
            this.cldOutCode = new DataColumn();
            this.cldInputNo = new DataColumn();
            this.cldPattern = new DataColumn();
            this.cldResCode = new DataColumn();
            this.cldJobInfo = new DataColumn();
            this.cldTimeStamp = new DataColumn();
            this.cldSyu = new DataColumn();
            this.cldEnd = new DataColumn();
            this.cldID = new DataColumn();
            this.cldFolder = new DataColumn();
            this.cldNum = new DataColumn();
            this.cldStatus = new DataColumn();
            this.cldOpen = new DataColumn();
            this.cldSave = new DataColumn();
            this.cldPrint = new DataColumn();
            this.cldDelete = new DataColumn();
            this.clFileName = new DataColumn();
            this.clDataInfo = new DataColumn();
            this.clUserCode = new DataColumn();
            this.clAttach = new DataColumn();
            this.clSign = new DataColumn();
            this.dataColumn1 = new DataColumn();
            this.imgDataview = new ImageList(this.components);
            this.sfdDataView = new SaveFileDialog();
            this.ofdDataView = new OpenFileDialog();
            this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn18 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn19 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn21 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn22 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn23 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new DataGridViewTextBoxColumn();
            this.pnlData.SuspendLayout();
            this.pnlDVSearch.SuspendLayout();
            ((ISupportInitialize) this.dgvDataView).BeginInit();
            this.rmnDataView.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlSendRecvFolder.SuspendLayout();
            this.rmnSendRecvFolder.SuspendLayout();
            this.stSendRecvFolder.SuspendLayout();
            this.dtsDataView.BeginInit();
            this.dataTable1.BeginInit();
            base.SuspendLayout();
            this.pnlData.Controls.Add(this.pnlDVSearch);
            this.pnlData.Controls.Add(this.splDataView);
            this.pnlData.Controls.Add(this.pnlSendRecvFolder);
//            manager.ApplyResources(this.pnlData, "pnlData");
            this.pnlData.Name = "pnlData";
            this.pnlDVSearch.Controls.Add(this.dgvDataView);
            this.pnlDVSearch.Controls.Add(this.pnlSearch);
//            manager.ApplyResources(this.pnlDVSearch, "pnlDVSearch");
            this.pnlDVSearch.Name = "pnlDVSearch";
            this.dgvDataView.AllowUserToAddRows = false;
            this.dgvDataView.AllowUserToDeleteRows = false;
            this.dgvDataView.AllowUserToResizeRows = false;
            this.dgvDataView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style.BackColor = SystemColors.Control;
            style.ForeColor = SystemColors.WindowText;
            style.SelectionBackColor = SystemColors.Highlight;
            style.SelectionForeColor = SystemColors.HighlightText;
            style.WrapMode = DataGridViewTriState.False;
            this.dgvDataView.ColumnHeadersDefaultCellStyle = style;
            this.dgvDataView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDataView.Columns.AddRange(new DataGridViewColumn[] { 
                this.clmOpen, this.clmPrint, this.clmSave, this.clmSign, this.clmJobCode, this.clmOutCode, this.clmInputNo, this.clmPattern, this.clmResCode, this.clmJobInfo, this.clmTimeStamp, this.clmSyu, this.clmEnd, this.clmID, this.clmFolder, this.clmNum, 
                this.clmStatus, this.clmOpenFlag, this.clmSaveFlag, this.clmPrintFlag, this.clmDelete, this.clmFileName, this.clmDataInfo, this.clmUserCode, this.clmAttach, this.clmSignFilePath, this.clmSignflg, this.clmAirSea
             });
            this.dgvDataView.ContextMenuStrip = this.rmnDataView;
//            manager.ApplyResources(this.dgvDataView, "dgvDataView");
            this.dgvDataView.Name = "dgvDataView";
            this.dgvDataView.ReadOnly = true;
            style2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style2.BackColor = SystemColors.Control;
            style2.ForeColor = SystemColors.WindowText;
            style2.SelectionBackColor = SystemColors.Highlight;
            style2.SelectionForeColor = SystemColors.HighlightText;
            style2.WrapMode = DataGridViewTriState.True;
            this.dgvDataView.RowHeadersDefaultCellStyle = style2;
            this.dgvDataView.RowHeadersVisible = false;
            this.dgvDataView.RowTemplate.Height = 0x15;
            this.dgvDataView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvDataView.VirtualMode = true;
            this.dgvDataView.CellValueNeeded += new DataGridViewCellValueEventHandler(this.dgvDataView_CellValueNeeded);
            this.dgvDataView.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.dgvDataView_ColumnHeaderMouseClick);
            this.dgvDataView.RowPrePaint += new DataGridViewRowPrePaintEventHandler(this.dgvDataView_RowPrePaint);
            this.dgvDataView.SelectionChanged += new EventHandler(this.dgvDataView_SelectionChanged);
            this.dgvDataView.DragDrop += new DragEventHandler(this.dgvDataView_DragDrop);
            this.dgvDataView.KeyDown += new KeyEventHandler(this.dgvDataView_KeyDown);
            this.dgvDataView.Leave += new EventHandler(this.dgvDataView_Leave);
            this.dgvDataView.MouseDown += new MouseEventHandler(this.dgvDataView_MouseDown);
            this.dgvDataView.MouseLeave += new EventHandler(this.dgvDataView_MouseLeave);
//            manager.ApplyResources(this.clmOpen, "clmOpen");
            this.clmOpen.Name = "clmOpen";
            this.clmOpen.ReadOnly = true;
            this.clmOpen.Resizable = DataGridViewTriState.True;
         //   manager.ApplyResources(this.clmPrint, "clmPrint");
            this.clmPrint.Name = "clmPrint";
            this.clmPrint.ReadOnly = true;
            this.clmPrint.Resizable = DataGridViewTriState.True;
          //  manager.ApplyResources(this.clmSave, "clmSave");
            this.clmSave.Name = "clmSave";
            this.clmSave.ReadOnly = true;
            this.clmSave.Resizable = DataGridViewTriState.True;
           // manager.ApplyResources(this.clmSign, "clmSign");
            this.clmSign.Name = "clmSign";
            this.clmSign.ReadOnly = true;
            this.clmJobCode.DataPropertyName = "clJobCode";
            this.clmJobCode.FillWeight = 80f;
           // manager.ApplyResources(this.clmJobCode, "clmJobCode");
            this.clmJobCode.Name = "clmJobCode";
            this.clmJobCode.ReadOnly = true;
            this.clmJobCode.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clmOutCode.DataPropertyName = "clOutCode";
            this.clmOutCode.FillWeight = 70f;
           // manager.ApplyResources(this.clmOutCode, "clmOutCode");
            this.clmOutCode.Name = "clmOutCode";
            this.clmOutCode.ReadOnly = true;
            this.clmOutCode.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clmInputNo.DataPropertyName = "clInputNo";
            this.clmInputNo.FillWeight = 80f;
//            manager.ApplyResources(this.clmInputNo, "clmInputNo");
            this.clmInputNo.Name = "clmInputNo";
            this.clmInputNo.ReadOnly = true;
            this.clmInputNo.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clmPattern.DataPropertyName = "clPattern";
           // manager.ApplyResources(this.clmPattern, "clmPattern");
            this.clmPattern.Name = "clmPattern";
            this.clmPattern.ReadOnly = true;
            this.clmPattern.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clmResCode.DataPropertyName = "clResCode";
          //  manager.ApplyResources(this.clmResCode, "clmResCode");
            this.clmResCode.Name = "clmResCode";
            this.clmResCode.ReadOnly = true;
            this.clmResCode.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clmJobInfo.DataPropertyName = "clJobInfo";
            this.clmJobInfo.FillWeight = 120f;
         //   manager.ApplyResources(this.clmJobInfo, "clmJobInfo");
            this.clmJobInfo.Name = "clmJobInfo";
            this.clmJobInfo.ReadOnly = true;
            this.clmJobInfo.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clmTimeStamp.DataPropertyName = "clTimeStamp";
          //  manager.ApplyResources(this.clmTimeStamp, "clmTimeStamp");
            this.clmTimeStamp.Name = "clmTimeStamp";
            this.clmTimeStamp.ReadOnly = true;
            this.clmTimeStamp.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clmSyu.DataPropertyName = "clSyu";
            this.clmSyu.FillWeight = 50f;
          //  manager.ApplyResources(this.clmSyu, "clmSyu");
            this.clmSyu.Name = "clmSyu";
            this.clmSyu.ReadOnly = true;
            this.clmSyu.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clmEnd.DataPropertyName = "clEnd";
            this.clmEnd.FillWeight = 50f;
          //  manager.ApplyResources(this.clmEnd, "clmEnd");
            this.clmEnd.Name = "clmEnd";
            this.clmEnd.ReadOnly = true;
            this.clmEnd.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clmID.DataPropertyName = "clID";
            //manager.ApplyResources(this.clmID, "clmID");
            this.clmID.Name = "clmID";
            this.clmID.ReadOnly = true;
            this.clmID.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clmFolder.DataPropertyName = "clFolder";
         //   manager.ApplyResources(this.clmFolder, "clmFolder");
            this.clmFolder.Name = "clmFolder";
            this.clmFolder.ReadOnly = true;
            this.clmFolder.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clmNum.DataPropertyName = "clNum";
         //   manager.ApplyResources(this.clmNum, "clmNum");
            this.clmNum.Name = "clmNum";
            this.clmNum.ReadOnly = true;
            this.clmNum.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clmStatus.DataPropertyName = "clStatus";
           // manager.ApplyResources(this.clmStatus, "clmStatus");
            this.clmStatus.Name = "clmStatus";
            this.clmStatus.ReadOnly = true;
            this.clmStatus.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clmOpenFlag.DataPropertyName = "clOpen";
           // manager.ApplyResources(this.clmOpenFlag, "clmOpenFlag");
            this.clmOpenFlag.Name = "clmOpenFlag";
            this.clmOpenFlag.ReadOnly = true;
            this.clmOpenFlag.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clmSaveFlag.DataPropertyName = "clSave";
           // manager.ApplyResources(this.clmSaveFlag, "clmSaveFlag");
            this.clmSaveFlag.Name = "clmSaveFlag";
            this.clmSaveFlag.ReadOnly = true;
            this.clmSaveFlag.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clmPrintFlag.DataPropertyName = "clPrint";
           // manager.ApplyResources(this.clmPrintFlag, "clmPrintFlag");
            this.clmPrintFlag.Name = "clmPrintFlag";
            this.clmPrintFlag.ReadOnly = true;
            this.clmPrintFlag.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clmDelete.DataPropertyName = "clDelete";
         //   manager.ApplyResources(this.clmDelete, "clmDelete");
            this.clmDelete.Name = "clmDelete";
            this.clmDelete.ReadOnly = true;
            this.clmDelete.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clmFileName.DataPropertyName = "clFileName";
           // manager.ApplyResources(this.clmFileName, "clmFileName");
            this.clmFileName.Name = "clmFileName";
            this.clmFileName.ReadOnly = true;
            this.clmFileName.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clmDataInfo.DataPropertyName = "clDataInfo";
          //  manager.ApplyResources(this.clmDataInfo, "clmDataInfo");
            this.clmDataInfo.Name = "clmDataInfo";
            this.clmDataInfo.ReadOnly = true;
            this.clmDataInfo.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clmUserCode.DataPropertyName = "clUserCode";
          //  manager.ApplyResources(this.clmUserCode, "clmUserCode");
            this.clmUserCode.Name = "clmUserCode";
            this.clmUserCode.ReadOnly = true;
            this.clmUserCode.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clmAttach.DataPropertyName = "clAttach";
         //   manager.ApplyResources(this.clmAttach, "clmAttach");
            this.clmAttach.Name = "clmAttach";
            this.clmAttach.ReadOnly = true;
            this.clmAttach.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clmSignFilePath.DataPropertyName = "clSignPath";
          //  manager.ApplyResources(this.clmSignFilePath, "clmSignFilePath");
            this.clmSignFilePath.Name = "clmSignFilePath";
            this.clmSignFilePath.ReadOnly = true;
            this.clmSignflg.DataPropertyName = "clSign";
          //  manager.ApplyResources(this.clmSignflg, "clmSignflg");
            this.clmSignflg.Name = "clmSignflg";
            this.clmSignflg.ReadOnly = true;
            this.clmAirSea.DataPropertyName = "clAirSea";
         //   manager.ApplyResources(this.clmAirSea, "clmAirSea");
            this.clmAirSea.Name = "clmAirSea";
            this.clmAirSea.ReadOnly = true;
            this.clmAirSea.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.rmnDataView.Items.AddRange(new ToolStripItem[] { 
                this.rmnOpen, this.rmnJobOpen, this.rmnSplitter1, this.rmnPrint, this.rmnPrintPreview, this.rmnSplitter2, this.rmnFile, this.rmnSplitter4, this.rmnUndo, this.rmnDelete, this.rmnAllSelect, this.rmnSplitter3, this.rmnDataViewRefresh, this.rmnSplitter5, this.rmnImport, this.rmnExport, 
                this.rmnSplitter6, this.rmnSendSearch, this.rmnRecvSearch, this.rmnSplitter7, this.rmnsignatureVerification
             });
            this.rmnDataView.Name = "rmnDataView";
           // manager.ApplyResources(this.rmnDataView, "rmnDataView");
            this.rmnDataView.Opened += new EventHandler(this.rmnDataView_Opened);
            this.rmnOpen.Name = "rmnOpen";
          //  manager.ApplyResources(this.rmnOpen, "rmnOpen");
            this.rmnOpen.Tag = "mnOpen";
            this.rmnOpen.Click += new EventHandler(this.rmnOpen_Click);
            this.rmnJobOpen.Name = "rmnJobOpen";
          //  manager.ApplyResources(this.rmnJobOpen, "rmnJobOpen");
            this.rmnJobOpen.Tag = "mnJobOpen";
            this.rmnJobOpen.Click += new EventHandler(this.rmnJobOpen_Click);
            this.rmnSplitter1.Name = "rmnSplitter1";
          //  manager.ApplyResources(this.rmnSplitter1, "rmnSplitter1");
            this.rmnPrint.Name = "rmnPrint";
           // manager.ApplyResources(this.rmnPrint, "rmnPrint");
            this.rmnPrint.Tag = "mnPrintJobData";
            this.rmnPrint.Click += new EventHandler(this.rmnPrint_Click);
            this.rmnPrintPreview.Name = "rmnPrintPreview";
          //  manager.ApplyResources(this.rmnPrintPreview, "rmnPrintPreview");
            this.rmnPrintPreview.Tag = "mnPrintJobDataPreview";
            this.rmnPrintPreview.Click += new EventHandler(this.rmnPrintPreview_Click);
            this.rmnSplitter2.Name = "rmnSplitter2";
         //   manager.ApplyResources(this.rmnSplitter2, "rmnSplitter2");
            this.rmnFile.DropDownItems.AddRange(new ToolStripItem[] { this.rmnFileSave });
            this.rmnFile.Name = "rmnFile";
          //  manager.ApplyResources(this.rmnFile, "rmnFile");
            this.rmnFileSave.Name = "rmnFileSave";
         //   manager.ApplyResources(this.rmnFileSave, "rmnFileSave");
            this.rmnFileSave.Tag = "mnFileSaveAs";
            this.rmnFileSave.Click += new EventHandler(this.rmnFileSave_Click);
            this.rmnSplitter4.Name = "rmnSplitter4";
        //    manager.ApplyResources(this.rmnSplitter4, "rmnSplitter4");
            this.rmnUndo.Name = "rmnUndo";
          //  manager.ApplyResources(this.rmnUndo, "rmnUndo");
            this.rmnUndo.Tag = "mnUndo";
            this.rmnUndo.Click += new EventHandler(this.rmnUndo_Click);
            this.rmnDelete.Name = "rmnDelete";
         //   manager.ApplyResources(this.rmnDelete, "rmnDelete");
            this.rmnDelete.Click += new EventHandler(this.rmnDelete_Click);
            this.rmnAllSelect.Name = "rmnAllSelect";
         //   manager.ApplyResources(this.rmnAllSelect, "rmnAllSelect");
            this.rmnAllSelect.Tag = "mnSelectAll";
            this.rmnAllSelect.Click += new EventHandler(this.rmnAllSelect_Click);
            this.rmnSplitter3.Name = "rmnSplitter3";
         //   manager.ApplyResources(this.rmnSplitter3, "rmnSplitter3");
            this.rmnDataViewRefresh.Name = "rmnDataViewRefresh";
         //   manager.ApplyResources(this.rmnDataViewRefresh, "rmnDataViewRefresh");
            this.rmnDataViewRefresh.Tag = "mnViewRefresh";
            this.rmnDataViewRefresh.Click += new EventHandler(this.rmnDataViewRefresh_Click);
            this.rmnSplitter5.Name = "rmnSplitter5";
         //   manager.ApplyResources(this.rmnSplitter5, "rmnSplitter5");
            this.rmnImport.Name = "rmnImport";
         //   manager.ApplyResources(this.rmnImport, "rmnImport");
            this.rmnImport.Tag = "mnDataViewImport";
            this.rmnImport.Click += new EventHandler(this.rmnImport_Click);
            this.rmnExport.Name = "rmnExport";
         //   manager.ApplyResources(this.rmnExport, "rmnExport");
            this.rmnExport.Tag = "mnDataViewExport";
            this.rmnExport.Click += new EventHandler(this.rmnExport_Click);
            this.rmnSplitter6.Name = "rmnSplitter6";
         //   manager.ApplyResources(this.rmnSplitter6, "rmnSplitter6");
            this.rmnSendSearch.Name = "rmnSendSearch";
        //    manager.ApplyResources(this.rmnSendSearch, "rmnSendSearch");
            this.rmnSendSearch.Tag = "mnSendedSearch";
            this.rmnSendSearch.Click += new EventHandler(this.rmnSendSearch_Click);
            this.rmnRecvSearch.Name = "rmnRecvSearch";
          //  manager.ApplyResources(this.rmnRecvSearch, "rmnRecvSearch");
            this.rmnRecvSearch.Tag = "mnReceivedSearch";
            this.rmnRecvSearch.Click += new EventHandler(this.rmnRecvSearch_Click);
            this.rmnSplitter7.Name = "rmnSplitter7";
          //  manager.ApplyResources(this.rmnSplitter7, "rmnSplitter7");
          //  manager.ApplyResources(this.rmnsignatureVerification, "rmnsignatureVerification");
            this.rmnsignatureVerification.Name = "rmnsignatureVerification";
            this.rmnsignatureVerification.Click += new EventHandler(this.rmnsignatureVerification_Click);
            this.pnlSearch.BackColor = SystemColors.GradientInactiveCaption;
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.cmbSearchDiv);
            this.pnlSearch.Controls.Add(this.lblSearchDiv);
            this.pnlSearch.Controls.Add(this.cmbSearchText);
            this.pnlSearch.Controls.Add(this.lblSearchText);
          //  manager.ApplyResources(this.pnlSearch, "pnlSearch");
            this.pnlSearch.Name = "pnlSearch";
          //  manager.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new EventHandler(this.btnSearch_Click);
            this.cmbSearchDiv.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbSearchDiv.FormattingEnabled = true;
//            this.cmbSearchDiv.Items.AddRange(new object[] { manager.GetString("cmbSearchDiv.Items"), manager.GetString("cmbSearchDiv.Items1"), manager.GetString("cmbSearchDiv.Items2"), manager.GetString("cmbSearchDiv.Items3"), manager.GetString("cmbSearchDiv.Items4"), manager.GetString("cmbSearchDiv.Items5"), manager.GetString("cmbSearchDiv.Items6"), manager.GetString("cmbSearchDiv.Items7"), manager.GetString("cmbSearchDiv.Items8") });
          //  manager.ApplyResources(this.cmbSearchDiv, "cmbSearchDiv");
            this.cmbSearchDiv.Name = "cmbSearchDiv";
          //  manager.ApplyResources(this.lblSearchDiv, "lblSearchDiv");
            this.lblSearchDiv.Name = "lblSearchDiv";
            this.cmbSearchText.FormattingEnabled = true;
         //   manager.ApplyResources(this.cmbSearchText, "cmbSearchText");
            this.cmbSearchText.Name = "cmbSearchText";
            this.cmbSearchText.KeyDown += new KeyEventHandler(this.cmbSearchText_KeyDown);
            this.cmbSearchText.PreviewKeyDown += new PreviewKeyDownEventHandler(this.cmbSearchText_PreviewKeyDown);
          //  manager.ApplyResources(this.lblSearchText, "lblSearchText");
            this.lblSearchText.Name = "lblSearchText";
        //    manager.ApplyResources(this.splDataView, "splDataView");
            this.splDataView.Name = "splDataView";
            this.splDataView.TabStop = false;
            this.pnlSendRecvFolder.Controls.Add(this.tvSendRecvFolder);
            this.pnlSendRecvFolder.Controls.Add(this.stSendRecvFolder);
          //  manager.ApplyResources(this.pnlSendRecvFolder, "pnlSendRecvFolder");
            this.pnlSendRecvFolder.Name = "pnlSendRecvFolder";
            this.tvSendRecvFolder.AllowDrop = true;
//            this.tvSendRecvFolder.BorderStyle = BorderStyle.FixedSingle;
            this.tvSendRecvFolder.ContextMenuStrip = this.rmnSendRecvFolder;
          //  manager.ApplyResources(this.tvSendRecvFolder, "tvSendRecvFolder");
            this.tvSendRecvFolder.HideSelection = false;
            this.tvSendRecvFolder.ImageList = this.imlFolder;
            this.tvSendRecvFolder.Name = "tvSendRecvFolder";
//            this.tvSendRecvFolder.Nodes.AddRange(new TreeNode[] { (TreeNode) manager.GetObject("tvSendRecvFolder.Nodes") });
            this.tvSendRecvFolder.AfterLabelEdit += new NodeLabelEditEventHandler(this.tvSendRecvFolder_AfterLabelEdit);
            this.tvSendRecvFolder.AfterSelect += new TreeViewEventHandler(this.tvSendRecvFolder_AfterSelect);
            this.tvSendRecvFolder.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.tvSendRecvFolder_NodeMouseClick);
            this.tvSendRecvFolder.DragDrop += new DragEventHandler(this.tvSendRecvFolder_DragDrop);
            this.tvSendRecvFolder.DragEnter += new DragEventHandler(this.tvSendRecvFolder_DragEnter);
            this.tvSendRecvFolder.DragOver += new DragEventHandler(this.tvSendRecvFolder_DragOver);
            this.tvSendRecvFolder.PreviewKeyDown += new PreviewKeyDownEventHandler(this.tvSendRecvFolder_PreviewKeyDown);
            this.rmnSendRecvFolder.Items.AddRange(new ToolStripItem[] { this.rmnFolderCreate, this.rmnChangeName, this.rmnFolderDelete });
            this.rmnSendRecvFolder.Name = "rmnSendRecvFolder";
         //   manager.ApplyResources(this.rmnSendRecvFolder, "rmnSendRecvFolder");
            this.rmnFolderCreate.Name = "rmnFolderCreate";
         //   manager.ApplyResources(this.rmnFolderCreate, "rmnFolderCreate");
            this.rmnFolderCreate.Tag = "mnCreateFolder";
            this.rmnFolderCreate.Click += new EventHandler(this.rmnFolderCreate_Click);
            this.rmnChangeName.Name = "rmnChangeName";
          //  manager.ApplyResources(this.rmnChangeName, "rmnChangeName");
            this.rmnChangeName.Tag = "mnRenameFolder";
            this.rmnChangeName.Click += new EventHandler(this.rmnChangeName_Click);
            this.rmnFolderDelete.Name = "rmnFolderDelete";
        //    manager.ApplyResources(this.rmnFolderDelete, "rmnFolderDelete");
            this.rmnFolderDelete.Click += new EventHandler(this.rmnFolderDelete_Click);
            this.imlFolder.ImageStream = (ImageListStreamer) manager.GetObject("imlFolder.ImageStream");
            this.imlFolder.TransparentColor = Color.Fuchsia;
            this.imlFolder.Images.SetKeyName(0, "トップ.bmp");
            this.imlFolder.Images.SetKeyName(1, "受信.bmp");
            this.imlFolder.Images.SetKeyName(2, "送信.bmp");
            this.imlFolder.Images.SetKeyName(3, "送信済み.bmp");
            this.imlFolder.Images.SetKeyName(4, "ごみ箱.bmp");
            this.imlFolder.Images.SetKeyName(5, "FolderC2.bmp");
            this.imlFolder.Images.SetKeyName(6, "FolderO2.bmp");
            this.stSendRecvFolder.Items.AddRange(new ToolStripItem[] { this.stpDataCount, this.stpSelectCount });
           // manager.ApplyResources(this.stSendRecvFolder, "stSendRecvFolder");
            this.stSendRecvFolder.Name = "stSendRecvFolder";
            this.stpDataCount.Name = "stpDataCount";
          //  manager.ApplyResources(this.stpDataCount, "stpDataCount");
            this.stpSelectCount.BorderSides = ToolStripStatusLabelBorderSides.Left;
            this.stpSelectCount.BorderStyle = Border3DStyle.Bump;
            this.stpSelectCount.Name = "stpSelectCount";
          //  manager.ApplyResources(this.stpSelectCount, "stpSelectCount");
            this.stpSelectCount.Spring = true;
            this.dtsDataView.DataSetName = "NewDataSet";
            this.dtsDataView.Tables.AddRange(new DataTable[] { this.dataTable1 });
            this.dataTable1.Columns.AddRange(new DataColumn[] { 
                this.cldAirSea, this.cldJobCode, this.cldOutCode, this.cldInputNo, this.cldPattern, this.cldResCode, this.cldJobInfo, this.cldTimeStamp, this.cldSyu, this.cldEnd, this.cldID, this.cldFolder, this.cldNum, this.cldStatus, this.cldOpen, this.cldSave, 
                this.cldPrint, this.cldDelete, this.clFileName, this.clDataInfo, this.clUserCode, this.clAttach, this.clSign, this.dataColumn1
             });
            this.dataTable1.TableName = "Naccs";
            this.cldAirSea.ColumnName = "clAirSea";
            this.cldJobCode.Caption = "業務ｺｰﾄﾞ";
            this.cldJobCode.ColumnName = "clJobCode";
            this.cldOutCode.ColumnName = "clOutCode";
            this.cldOutCode.MaxLength = 7;
            this.cldInputNo.ColumnName = "clInputNo";
            this.cldInputNo.MaxLength = 10;
            this.cldPattern.ColumnName = "clPattern";
            this.cldPattern.MaxLength = 1;
            this.cldResCode.ColumnName = "clResCode";
            this.cldResCode.MaxLength = 15;
            this.cldJobInfo.ColumnName = "clJobInfo";
            this.cldJobInfo.MaxLength = 0x40;
            this.cldTimeStamp.ColumnName = "clTimeStamp";
            this.cldTimeStamp.MaxLength = 0x13;
            this.cldSyu.ColumnName = "clSyu";
            this.cldSyu.MaxLength = 1;
            this.cldEnd.ColumnName = "clEnd";
            this.cldEnd.MaxLength = 1;
            this.cldID.ColumnName = "clID";
            this.cldID.DataType = typeof(ulong);
            this.cldFolder.ColumnName = "clFolder";
            this.cldFolder.MaxLength = 0xff;
            this.cldNum.ColumnName = "clNum";
            this.cldNum.MaxLength = 3;
            this.cldStatus.ColumnName = "clStatus";
            this.cldStatus.MaxLength = 1;
            this.cldOpen.ColumnName = "clOpen";
            this.cldOpen.MaxLength = 1;
            this.cldSave.ColumnName = "clSave";
            this.cldSave.MaxLength = 1;
            this.cldPrint.ColumnName = "clPrint";
            this.cldPrint.MaxLength = 1;
            this.cldDelete.ColumnName = "clDelete";
            this.cldDelete.DataType = typeof(bool);
            this.clFileName.ColumnName = "clFileName";
            this.clDataInfo.Caption = "clDataInfo";
            this.clDataInfo.ColumnName = "clDataInfo";
            this.clUserCode.Caption = "clUserCode";
            this.clUserCode.ColumnName = "clUserCode";
            this.clAttach.Caption = "clAttach";
            this.clAttach.ColumnName = "clAttach";
            this.clSign.Caption = "clSign";
            this.clSign.ColumnName = "clSign";
            this.dataColumn1.Caption = "clSignPath";
            this.dataColumn1.ColumnName = "clSignPath";
//            this.imgDataview.ImageStream = (ImageListStreamer) manager.GetObject("imgDataview.ImageStream");
            this.imgDataview.TransparentColor = Color.Fuchsia;
            this.imgDataview.Images.SetKeyName(0, "FolderC2.bmp");
            this.imgDataview.Images.SetKeyName(1, "FolderO2.bmp");
            this.imgDataview.Images.SetKeyName(2, "保存済み２.ico");
            this.imgDataview.Images.SetKeyName(3, "印刷済み２.ico");
            this.imgDataview.Images.SetKeyName(4, "kara.bmp");
            this.imgDataview.Images.SetKeyName(5, "未開封.bmp");
            this.imgDataview.Images.SetKeyName(6, "開封.bmp");
            this.imgDataview.Images.SetKeyName(7, "Save.bmp");
            this.imgDataview.Images.SetKeyName(8, "printer2.bmp");
            this.imgDataview.Images.SetKeyName(9, "透過テスト.ico");
            this.imgDataview.Images.SetKeyName(10, "Sign2-2.ico");
            this.imgDataview.Images.SetKeyName(11, "Sign3.ico");
          //  manager.ApplyResources(this.sfdDataView, "sfdDataView");
            this.sfdDataView.RestoreDirectory = true;
           // manager.ApplyResources(this.ofdDataView, "ofdDataView");
            this.ofdDataView.RestoreDirectory = true;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "clJobCode";
            this.dataGridViewTextBoxColumn1.FillWeight = 80f;
          //  manager.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "clOutCode";
            this.dataGridViewTextBoxColumn2.FillWeight = 70f;
           // manager.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "clInputNo";
            this.dataGridViewTextBoxColumn3.FillWeight = 80f;
           // manager.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "clPattern";
            this.dataGridViewTextBoxColumn4.FillWeight = 80f;
           // manager.ApplyResources(this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "clResCode";
           // manager.ApplyResources(this.dataGridViewTextBoxColumn5, "dataGridViewTextBoxColumn5");
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "clJobInfo";
            this.dataGridViewTextBoxColumn6.FillWeight = 120f;
           // manager.ApplyResources(this.dataGridViewTextBoxColumn6, "dataGridViewTextBoxColumn6");
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn7.DataPropertyName = "clTimeStamp";
            this.dataGridViewTextBoxColumn7.FillWeight = 120f;
           // manager.ApplyResources(this.dataGridViewTextBoxColumn7, "dataGridViewTextBoxColumn7");
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn8.DataPropertyName = "clSyu";
            this.dataGridViewTextBoxColumn8.FillWeight = 50f;
           // manager.ApplyResources(this.dataGridViewTextBoxColumn8, "dataGridViewTextBoxColumn8");
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn9.DataPropertyName = "clEnd";
            this.dataGridViewTextBoxColumn9.FillWeight = 50f;
          //  manager.ApplyResources(this.dataGridViewTextBoxColumn9, "dataGridViewTextBoxColumn9");
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn10.DataPropertyName = "clID";
            this.dataGridViewTextBoxColumn10.FillWeight = 50f;
           // manager.ApplyResources(this.dataGridViewTextBoxColumn10, "dataGridViewTextBoxColumn10");
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn11.DataPropertyName = "clFolder";
          //  manager.ApplyResources(this.dataGridViewTextBoxColumn11, "dataGridViewTextBoxColumn11");
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn12.DataPropertyName = "clNum";
          //  manager.ApplyResources(this.dataGridViewTextBoxColumn12, "dataGridViewTextBoxColumn12");
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn13.DataPropertyName = "clStatus";
          //  manager.ApplyResources(this.dataGridViewTextBoxColumn13, "dataGridViewTextBoxColumn13");
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn14.DataPropertyName = "clOpen";
           // manager.ApplyResources(this.dataGridViewTextBoxColumn14, "dataGridViewTextBoxColumn14");
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            this.dataGridViewTextBoxColumn14.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn15.DataPropertyName = "clSave";
           // manager.ApplyResources(this.dataGridViewTextBoxColumn15, "dataGridViewTextBoxColumn15");
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            this.dataGridViewTextBoxColumn15.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn16.DataPropertyName = "clPrint";
           // manager.ApplyResources(this.dataGridViewTextBoxColumn16, "dataGridViewTextBoxColumn16");
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.ReadOnly = true;
            this.dataGridViewTextBoxColumn16.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn17.DataPropertyName = "clDelete";
          //  manager.ApplyResources(this.dataGridViewTextBoxColumn17, "dataGridViewTextBoxColumn17");
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.ReadOnly = true;
            this.dataGridViewTextBoxColumn17.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn18.DataPropertyName = "clFileName";
          //  manager.ApplyResources(this.dataGridViewTextBoxColumn18, "dataGridViewTextBoxColumn18");
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.ReadOnly = true;
            this.dataGridViewTextBoxColumn18.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn19.DataPropertyName = "clDataInfo";
           // manager.ApplyResources(this.dataGridViewTextBoxColumn19, "dataGridViewTextBoxColumn19");
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.ReadOnly = true;
            this.dataGridViewTextBoxColumn19.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn20.DataPropertyName = "clUserCode";
          //  manager.ApplyResources(this.dataGridViewTextBoxColumn20, "dataGridViewTextBoxColumn20");
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn20.ReadOnly = true;
            this.dataGridViewTextBoxColumn20.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn21.DataPropertyName = "clAttach";
           // manager.ApplyResources(this.dataGridViewTextBoxColumn21, "dataGridViewTextBoxColumn21");
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn21.ReadOnly = true;
            this.dataGridViewTextBoxColumn21.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn22.DataPropertyName = "clAttach";
//            manager.ApplyResources(this.dataGridViewTextBoxColumn22, "dataGridViewTextBoxColumn22");
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            this.dataGridViewTextBoxColumn22.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn23.DataPropertyName = "clSignPath";
         //   manager.ApplyResources(this.dataGridViewTextBoxColumn23, "dataGridViewTextBoxColumn23");
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            this.dataGridViewTextBoxColumn24.DataPropertyName = "clSign";
         //   manager.ApplyResources(this.dataGridViewTextBoxColumn24, "dataGridViewTextBoxColumn24");
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            this.dataGridViewTextBoxColumn24.SortMode = DataGridViewColumnSortMode.NotSortable;
          //  manager.ApplyResources(this, "$this");
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.pnlData);
            base.Name = "UDataView";
            base.Load += new EventHandler(this.UDataViewForm_Load);
            this.pnlData.ResumeLayout(false);
            this.pnlDVSearch.ResumeLayout(false);
            ((ISupportInitialize) this.dgvDataView).EndInit();
            this.rmnDataView.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlSendRecvFolder.ResumeLayout(false);
            this.pnlSendRecvFolder.PerformLayout();
            this.rmnSendRecvFolder.ResumeLayout(false);
            this.stSendRecvFolder.ResumeLayout(false);
            this.stSendRecvFolder.PerformLayout();
            this.dtsDataView.EndInit();
            this.dataTable1.EndInit();
            base.ResumeLayout(false);
        }

        protected virtual bool IsMoveDataCheck(TreeNode target)
        {
            if ((target.Level <= 1) && ((target.Level != 1) || ((target.Index != 0) && (target.Index != 3))))
            {
                return false;
            }
            return true;
        }

        public void JobOpen()
        {
            int keyID = this.GetKeyID();
            if (keyID > -1)
            {
                this.OnJobFormOpen(keyID, true);
            }
        }

        private int LenCount(string str)
        {
            return Encoding.GetEncoding("UTF-8").GetByteCount(str);
        }

        private void MultSave()
        {
            USaveFolderDlg dlg = new USaveFolderDlg {
                SelectPath = this.GetSaveFolder(this.GetData(this.GetKeyID()))
            };
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                dlg.Close();
                dlg.Dispose();
            }
            else
            {
                string selectPath = dlg.SelectPath;
                string fName = dlg.FName;
                dlg.Close();
                dlg.Dispose();
                this.drProgress = new DeleteRestore(2);
                this.drProgress.Show();
                string path = "";
                try
                {
                    this.drProgress.Refresh();
                    if (!Directory.Exists(selectPath))
                    {
                        Directory.CreateDirectory(selectPath);
                    }
                    int length = this.dgvDataView.SelectedRows.Count.ToString().Length;
                    int num2 = 1;
                    IData data = null;
                    List<string[]> list = new List<string[]>();
                    for (int i = 0; i < this.dgvDataView.Rows.Count; i++)
                    {
                        if (this.dgvDataView.Rows[i].Selected)
                        {
                            string[] item = new string[] { this.dgvDataView.Rows[i].Cells["clmID"].Value.ToString(), Path.Combine(this.pi.DataViewPath, this.dgvDataView.Rows[i].Cells["clmFileName"].Value.ToString()) };
                            list.Add(item);
                        }
                    }
                    this.drProgress.pgbRestore.Minimum = 0;
                    this.drProgress.pgbRestore.Maximum = list.Count;
                    for (int j = 0; j < list.Count; j++)
                    {
                        this.drProgress.pgbRestore.Value = num2;
                        this.drProgress.Refresh();
                        data = DataFactory.LoadFromDataView(list[j][0], list[j][1]);
                        path = Path.Combine(selectPath, fName + num2.ToString().PadLeft(length, '0') + ".txt");
                        if (File.Exists(path))
                        {
                            MessageDialog dialog = new MessageDialog();
                            if (dialog.ShowMessage("C301", path, "") != DialogResult.Yes)
                            {
                                dialog.Dispose();
                                num2++;
                                continue;
                            }
                        }
                        DataFactory.SaveToEdiFile(data, path);
                        this.SaveStatusChange(data.ID, false);
                        num2++;
                    }
                }
                catch (Exception exception)
                {
                    MessageDialog dialog2 = new MessageDialog();
                    dialog2.ShowMessage("E303", path, null, exception);
                    dialog2.Dispose();
                }
                finally
                {
                    this.dtsDataView.AcceptChanges();
                    this.dgvDataView.Refresh();
                    this.stpCount();
                    this.drProgress.Close();
                    this.drProgress.Dispose();
                }
            }
        }

        private void NodeNameChange(TreeNode tn, string fulfld)
        {
            try
            {
                DataRow[] rowArray = this.dtsDataView.Tables["NACCS"].Select("clFolder = '" + this.GetFullPath(tn.FullPath) + "'");
                for (int k = 0; k < rowArray.Length; k++)
                {
                    rowArray[k]["clFolder"] = fulfld;
                    IData data = this.GetData(int.Parse(rowArray[k]["clID"].ToString()));
                    if (data == null)
                    {
                        rowArray[k].Delete();
                    }
                    else
                    {
                        data.UserFolder = fulfld;
                        this.DataViewDataSave(data, rowArray[k]["clFileName"].ToString());
                    }
                }
            }
            finally
            {
                this.dtsDataView.AcceptChanges();
            }
            for (int i = 0; i < this.trmlist.Count; i++)
            {
                if (this.trmlist[i].Folder == this.GetFullPath(tn.FullPath))
                {
                    this.trmlist[i].Folder = fulfld;
                }
            }
            for (int j = 0; j < tn.Nodes.Count; j++)
            {
                this.NodeNameChange(tn.Nodes[j], fulfld + @"\" + this.GetFullPath(tn.Nodes[j].Text));
            }
        }

        private void NoOpenDelete(TreeNode tn)
        {
            for (int i = 0; i < tn.Nodes.Count; i++)
            {
                this.NoOpenDelete(tn.Nodes[i]);
            }
            if ((((tn.Level == 1) && (tn.Index == 0)) || (tn.Level >= 2)) && (tn.Text.LastIndexOf('(') > -1))
            {
                tn.Text = tn.Text.Remove(tn.Text.LastIndexOf('('));
            }
        }

        private void NoOpenSet(TreeNode tn)
        {
            for (int i = 0; i < tn.Nodes.Count; i++)
            {
                this.NoOpenSet(tn.Nodes[i]);
            }
            if ((((tn.Level == 1) && (tn.Index == 0)) || (tn.Level >= 2)) && !tn.IsEditing)
            {
                if (tn.Text.LastIndexOf('(') > -1)
                {
                    tn.Text = tn.Text.Remove(tn.Text.LastIndexOf('('));
                }
                string fullPath = "";
                if ((tn.Level != 1) || (tn.Index != 0))
                {
                    fullPath = this.GetFullPath(tn.FullPath);
                }
                string filter = "clStatus = 'r' AND clDelete = 0 AND clOpen = '0' AND clFolder = '" + fullPath + "'";
                string str3 = this.dtsDataView.Tables["NACCS"].Compute("Count(clID)", filter).ToString();
                tn.Text = tn.Text + "(" + str3 + ")";
            }
        }

        public void OpenStatusChange(string key)
        {
            DataRow[] rowArray = this.dtsDataView.Tables["NACCS"].Select("clID = " + key);
            if (File.Exists(this.pi.DataViewPath + rowArray[0]["clFileName"].ToString()))
            {
                IData data = null;
                try
                {
                    data = DataFactory.LoadFromDataView(key, Path.Combine(this.pi.DataViewPath, rowArray[0]["clFileName"].ToString()));
                }
                catch (Exception exception)
                {
                    MessageDialog dialog = new MessageDialog();
                    dialog.ShowMessage("E402", rowArray[0]["clFileName"].ToString(), null, exception);
                    dialog.Dispose();
                }
                data.IsRead = true;
                string fName = rowArray[0]["clFileName"].ToString();
                rowArray[0]["clOpen"] = "1";
                this.DataViewDataSave(data, fName);
                this.dtsDataView.AcceptChanges();
                if (this.FOpenViewFlag)
                {
                    this.ViewCountCheck(true);
                }
                this.stpCount();
            }
        }

        public void PrintStatusChange(string key)
        {
            DataRow[] rowArray = this.dtsDataView.Tables["NACCS"].Select("clID = " + key);
            if (File.Exists(this.pi.DataViewPath + rowArray[0]["clFileName"].ToString()))
            {
                IData data = null;
                try
                {
                    data = DataFactory.LoadFromDataView(key, Path.Combine(this.pi.DataViewPath, rowArray[0]["clFileName"].ToString()));
                }
                catch (Exception exception)
                {
                    MessageDialog dialog = new MessageDialog();
                    dialog.ShowMessage("E402", rowArray[0]["clFileName"].ToString(), null, exception);
                    dialog.Dispose();
                }
                data.IsPrinted = true;
                string fName = rowArray[0]["clFileName"].ToString();
                rowArray[0]["clPrint"] = "1";
                rowArray[0]["clOpen"] = "1";
                data.IsRead = true;
                if (this.FOpenViewFlag)
                {
                    string fullPath = rowArray[0]["clFolder"].ToString();
                    if (string.IsNullOrEmpty(fullPath))
                    {
                        fullPath = this.GetFullPath(this.RECIVE_FOLDER_PATH_STRING);
                    }
                    this.ViewCountChange(fullPath);
                }
                this.DataViewDataSave(data, fName);
                this.dtsDataView.AcceptChanges();
                this.dgvDataView.Refresh();
                this.stpCount();
            }
        }

        public void RecordDelete(int key)
        {
            DataRow[] rowArray = this.dtsDataView.Tables["NACCS"].Select("clID = " + key.ToString());
            if (rowArray.Length > 0)
            {
                rowArray[0].Delete();
            }
        }

        public void RecvFolderSelect()
        {
            this.tvSendRecvFolder.SelectedNode = this.tvSendRecvFolder.Nodes[0].Nodes[0];
        }

        public void RecvSearch()
        {
            if (this.GetKeyID() > -1)
            {
                IData data = this.GetData(this.GetKeyID());
                DataRow[] rowArray = this.dtsDataView.Tables["NACCS"].Select("clDataInfo = '" + data.Header.DataInfo + "' AND clStatus = 'r' AND clDelete = 0");
                if (rowArray.Length > 0)
                {
                    TreeNode folderPathNode = this.GetFolderPathNode(rowArray[0]["clFolder"].ToString());
                    if (folderPathNode != null)
                    {
                        this.tvSendRecvFolder.SelectedNode = folderPathNode;
                    }
                    else
                    {
                        this.tvSendRecvFolder.SelectedNode = this.tvSendRecvFolder.Nodes[0].Nodes[0];
                    }
                    this.dgvDataView.ClearSelection();
                    for (int i = 0; i < this.dgvDataView.RowCount; i++)
                    {
                        if (rowArray[0]["clID"].ToString() == this.dgvDataView.Rows[i].Cells["clmID"].Value.ToString())
                        {
                            this.dgvDataView.Rows[i].Selected = true;
                            this.dgvDataView.CurrentCell = this.dgvDataView.Rows[i].Cells[0];
                            if (!this.dgvDataView.Rows[i].Displayed)
                            {
                                this.dgvDataView.FirstDisplayedScrollingRowIndex = this.dgvDataView.Rows[i].Index;
                            }
                        }
                    }
                }
                else
                {
                    using (MessageDialog dialog = new MessageDialog())
                    {
                        dialog.ShowMessage("I501", null, null);
                    }
                }
            }
        }

        public static void RemoveAttribute(DirectoryInfo dirInfo)
        {
            dirInfo.Attributes = FileAttributes.Normal;
            foreach (FileInfo info in dirInfo.GetFiles())
            {
                info.Attributes = FileAttributes.Normal;
            }
            foreach (DirectoryInfo info2 in dirInfo.GetDirectories())
            {
                RemoveAttribute(info2);
            }
        }

        private void rmnAllSelect_Click(object sender, EventArgs e)
        {
            this.AllSelect();
        }

        private void rmnAppendSave_Click(object sender, EventArgs e)
        {
            this.AppendRecord();
        }

        private void rmnChangeName_Click(object sender, EventArgs e)
        {
            if (!this.tvSendRecvFolder.SelectedNode.IsEditing)
            {
                this.FolderChangeName();
            }
        }

        private void rmnDataView_Opened(object sender, EventArgs e)
        {
            if (!this.dgvDataView.Focused)
            {
                this.dgvDataView.Focus();
            }
        }

        private void rmnDataViewRefresh_Click(object sender, EventArgs e)
        {
            this.ViewRefresh();
        }

        private void rmnDelete_Click(object sender, EventArgs e)
        {
            this.DataDelete(-1);
        }

        private void rmnExport_Click(object sender, EventArgs e)
        {
            this.DataExport();
        }

        private void rmnFileSave_Click(object sender, EventArgs e)
        {
            this.SaveAsFile();
        }

        private void rmnFolderCreate_Click(object sender, EventArgs e)
        {
            this.FolderCreate();
            this.SaveFolder();
        }

        private void rmnFolderDelete_Click(object sender, EventArgs e)
        {
            if (this.tvSendRecvFolder.Focused)
            {
                this.FolderDelete();
                this.SaveFolder();
                this.DBFixAdd();
            }
        }

        private void rmnImport_Click(object sender, EventArgs e)
        {
            this.DataImport();
        }

        private void rmnJobOpen_Click(object sender, EventArgs e)
        {
            this.JobOpen();
        }

        private void rmnOpen_Click(object sender, EventArgs e)
        {
            int keyID = this.GetKeyID();
            if (keyID > -1)
            {
                this.OnJobFormOpen(keyID, false);
            }
        }

        private void rmnPrint_Click(object sender, EventArgs e)
        {
            this.OnPrintJob(0);
        }

        private void rmnPrintPreview_Click(object sender, EventArgs e)
        {
            this.OnPrintJob(1);
        }

        private void rmnRecvSearch_Click(object sender, EventArgs e)
        {
            this.RecvSearch();
        }

        private void rmnSendSearch_Click(object sender, EventArgs e)
        {
            this.SendSearch();
        }

        private void rmnsignatureVerification_Click(object sender, EventArgs e)
        {
            StreamReader reader = null;
            int num = 0;
            new Hashtable();
            SMimeFormat format = null;
            IData data = null;
            int num2 = -2146889721;
            int num3 = -2146893818;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            int num10 = 0;
            int num11 = 0;
            StringBuilder builder = new StringBuilder();
            if (this.dgvDataView.SelectedRows.Count > 0)
            {
                for (int i = 0; i < this.dgvDataView.RowCount; i++)
                {
                    string str = this.dgvDataView.Rows[i].Cells["clmSignflg"].Value.ToString();
                    if (this.dgvDataView.Rows[i].Selected && (str != "0"))
                    {
                        num++;
                        data = this.GetData(int.Parse(this.dgvDataView.Rows[i].Cells["clmID"].Value.ToString()));
                        string path = this.dgvDataView.Rows[i].Cells["clmSignFilePath"].Value.ToString();
                        if (File.Exists(path))
                        {
                            ULogClass.LogWrite(string.Format("SignatureVerification FilePath={0}", path), null, false);
                            try
                            {
                                try
                                {
                                    reader = new StreamReader(path);
                                    string sMIMEmessage = reader.ReadToEnd();
                                    format = new SMimeFormat();
                                    if (format.SignCheck(sMIMEmessage))
                                    {
                                        this.dgvDataView.Rows[i].Cells["clmSignflg"].Value = "2";
                                        data.Signflg = 2;
                                        this.DataViewDataSave(data, this.dgvDataView.Rows[i].Cells["clmFileName"].Value.ToString());
                                    }
                                    else
                                    {
                                        this.dgvDataView.Rows[i].Cells["clmSignflg"].Value = "9";
                                        data.Signflg = 9;
                                        this.DataViewDataSave(data, this.dgvDataView.Rows[i].Cells["clmFileName"].Value.ToString());
                                        if (format.GetThisCertificateErr == "NotTimeValid")
                                        {
                                            num4++;
                                        }
                                        else if (format.GetThisCertificateErr == "UntrustedRoot")
                                        {
                                            num5++;
                                        }
                                        else if (format.GetThisCertificateErr == "Revoked")
                                        {
                                            num6++;
                                        }
                                        else
                                        {
                                            num7++;
                                        }
                                        if (!builder.ToString().Contains(format.GetThisCertificateErr))
                                        {
                                            builder.Append(format.GetThisCertificateErr + "\r\n");
                                        }
                                    }
                                }
                                catch (Exception exception)
                                {
                                    this.dgvDataView.Rows[i].Cells["clmSignflg"].Value = "9";
                                    data.Signflg = 9;
                                    this.DataViewDataSave(data, this.dgvDataView.Rows[i].Cells["clmFileName"].Value.ToString());
                                    int num13 = (int) exception.GetType().GetProperty("HResult", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(exception, new object[0]);
                                    if (num13 == num2)
                                    {
                                        num9++;
                                    }
                                    else if (num13 == num3)
                                    {
                                        num8++;
                                    }
                                    else
                                    {
                                        num10++;
                                    }
                                    if (!builder.ToString().Contains(exception.Message))
                                    {
                                        builder.Append(exception.Message + "\r\n");
                                    }
                                }
                                continue;
                            }
                            finally
                            {
                                reader.Close();
                            }
                        }
                        num11++;
                        if (!builder.ToString().Contains("CheckFileExistError"))
                        {
                            builder.Append("CheckFileExistError\r\n");
                        }
                        this.dgvDataView.Rows[i].Cells["clmSignflg"].Value = "9";
                        data.Signflg = 9;
                        this.DataViewDataSave(data, this.dgvDataView.Rows[i].Cells["clmFileName"].Value.ToString());
                    }
                }
                MessageDialog dialog = new MessageDialog();
                int num14 = ((num4 + num5) + num6) + num7;
                int num15 = (num8 + num9) + num10;
                int num16 = (num14 + num15) + num11;
                string messageCode = "";
                ULogClass.LogWrite(string.Format("SignatureVerificationResult ErrCount={0}", num16.ToString()), null, false);
                if (num != 0)
                {
                    if (num16 == 0)
                    {
                        messageCode = "I106";
                        dialog.ShowMessage(messageCode, "");
                    }
                    else
                    {
                        if (((num15 != 0) && (num14 == 0)) && (num11 == 0))
                        {
                            if (((num9 != 0) && (num8 == 0)) && (num10 == 0))
                            {
                                messageCode = "E126";
                            }
                            else if (((num9 == 0) && (num8 != 0)) && (num10 == 0))
                            {
                                messageCode = "E128";
                            }
                            else
                            {
                                messageCode = "E129";
                            }
                        }
                        else if (((num15 == 0) && (num14 != 0)) && (num11 == 0))
                        {
                            if (((num4 != 0) && (num5 == 0)) && ((num6 == 0) && (num7 == 0)))
                            {
                                messageCode = "E130";
                            }
                            else if (((num4 == 0) && (num5 != 0)) && ((num6 == 0) && (num7 == 0)))
                            {
                                messageCode = "E131";
                            }
                            else if (((num4 == 0) && (num5 == 0)) && ((num6 != 0) && (num7 == 0)))
                            {
                                messageCode = "E136";
                            }
                            else
                            {
                                messageCode = "E127";
                            }
                        }
                        else if (((num15 == 0) && (num14 == 0)) && (num11 != 0))
                        {
                            messageCode = "E132";
                        }
                        else
                        {
                            messageCode = "E125";
                        }
                        dialog.ShowMessage(messageCode, num16.ToString(), builder.ToString());
                    }
                }
                else
                {
                    dialog.ShowMessage("E133", "");
                }
                this.DataViewRepaint(false);
            }
        }

        private void rmnUndo_Click(object sender, EventArgs e)
        {
            this.DataUndo();
        }

        public void SaveAsFile()
        {
            if (this.dgvDataView.SelectedRows.Count >= 1)
            {
                if (this.dgvDataView.SelectedRows.Count == 1)
                {
                    this.SingleSave();
                }
                else
                {
                    this.MultSave();
                }
            }
        }

        public void SaveDateDataDelete(int SaveDate)
        {
            DateTime time = DateTime.Now.AddDays((double) -SaveDate);
            DataRow[] rowArray = this.dtsDataView.Tables["NACCS"].Select("clDelete = 0 AND clTimeStamp < '" + time.ToString("yyyy/MM/dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo) + "' AND (clStatus = 'a' OR (clStatus = 'r' AND clOpen = '1'))");
            IData data = null;
            for (int i = 0; i < rowArray.Length; i++)
            {
                rowArray[i]["clDelete"] = 1;
                data = this.GetData(int.Parse(rowArray[i]["clID"].ToString()));
                if (data == null)
                {
                    rowArray[i].Delete();
                }
                else
                {
                    data.IsDeleted = true;
                    this.DataViewDataSave(data, rowArray[i]["clFileName"].ToString());
                }
            }
            this.dtsDataView.AcceptChanges();
            this.dgvDataView.Refresh();
            this.stpCount();
        }

        private void SaveFolder()
        {
            this.FldList = this.GetFldList();
            this.msgc.Save();
        }

        public void SaveStatusChange(string key, bool Flag)
        {
            DataRow[] rowArray = this.dtsDataView.Tables["NACCS"].Select("clID = " + key);
            if (File.Exists(this.pi.DataViewPath + rowArray[0]["clFileName"].ToString()))
            {
                IData data = null;
                try
                {
                    data = DataFactory.LoadFromDataView(key, Path.Combine(this.pi.DataViewPath, rowArray[0]["clFileName"].ToString()));
                }
                catch (Exception exception)
                {
                    MessageDialog dialog = new MessageDialog();
                    dialog.ShowMessage("E402", rowArray[0]["clFileName"].ToString(), null, exception);
                    dialog.Dispose();
                }
                data.IsSaved = true;
                string fName = rowArray[0]["clFileName"].ToString();
                rowArray[0]["clSave"] = "1";
                rowArray[0]["clOpen"] = "1";
                data.IsRead = true;
                if (this.FOpenViewFlag)
                {
                    string fullPath = rowArray[0]["clFolder"].ToString();
                    if (string.IsNullOrEmpty(fullPath))
                    {
                        fullPath = this.GetFullPath(this.RECIVE_FOLDER_PATH_STRING);
                    }
                    this.ViewCountChange(fullPath);
                }
                this.DataViewDataSave(data, fName);
                if (Flag)
                {
                    this.dtsDataView.AcceptChanges();
                    this.dgvDataView.Refresh();
                    this.stpCount();
                }
            }
        }

        private void SearchListSet()
        {
            this.cmbSearchText.Items.Clear();
            for (int i = this.HSearchList.Count - 1; i > -1; i--)
            {
                this.cmbSearchText.Items.Add(this.HSearchList[i]);
            }
        }

        private void SelectClear()
        {
            this.dgvDataView.ClearSelection();
        }

        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        public void SendSearch()
        {
            if (this.GetKeyID() > -1)
            {
                IData data = this.GetData(this.GetKeyID());
                DataRow[] rowArray = this.dtsDataView.Tables["NACCS"].Select("clDataInfo = '" + data.Header.DataInfo + "' AND clStatus = 'a' AND clDelete = 0");
                if (rowArray.Length > 0)
                {
                    this.tvSendRecvFolder.SelectedNode = this.tvSendRecvFolder.Nodes[0].Nodes[2];
                    this.dgvDataView.ClearSelection();
                    for (int i = 0; i < this.dgvDataView.RowCount; i++)
                    {
                        if (rowArray[0]["clID"].ToString() == this.dgvDataView.Rows[i].Cells["clmID"].Value.ToString())
                        {
                            this.dgvDataView.Rows[i].Selected = true;
                            this.dgvDataView.CurrentCell = this.dgvDataView.Rows[i].Cells[0];
                            if (!this.dgvDataView.Rows[i].Displayed)
                            {
                                this.dgvDataView.FirstDisplayedScrollingRowIndex = this.dgvDataView.Rows[i].Index;
                            }
                        }
                    }
                }
                else
                {
                    using (MessageDialog dialog = new MessageDialog())
                    {
                        dialog.ShowMessage("I501", null, null);
                    }
                }
            }
        }

        public void SendStatusChange(string sendkey)
        {
            DataRow[] rowArray = this.dtsDataView.Tables["NACCS"].Select("clID = " + sendkey);
            if (File.Exists(this.pi.DataViewPath + rowArray[0]["clFileName"].ToString()))
            {
                IData data = null;
                try
                {
                    AbstractJobConfig config;
                    XmlNode node;
                    data = DataFactory.LoadFromDataView(sendkey, Path.Combine(this.pi.DataViewPath, rowArray[0]["clFileName"].ToString()));
                    JobConfigFactory.createJobConfig(data.JobCode.Trim(), DateTime.Now, out config);
                    config.selectRecord(DateTime.Now, out node);
                    node = node.Attributes["sign-flg"];
                    if (node != null)
                    {
                        data.Signflg = int.Parse(node.Value);
                    }
                }
                catch (Exception exception)
                {
                    MessageDialog dialog = new MessageDialog();
                    dialog.ShowMessage("E402", rowArray[0]["clFileName"].ToString(), null, exception);
                    dialog.Dispose();
                }
                data.Status = DataStatus.Sent;
                string fName = rowArray[0]["clFileName"].ToString();
                rowArray[0]["clStatus"] = "a";
                this.DataViewDataSave(data, fName);
                this.dtsDataView.AcceptChanges();
                if (this.msgc.DataView.AutoBackup)
                {
                    string backupPath = this.pi.BackupPath;
                    if (!Directory.Exists(backupPath))
                    {
                        Directory.CreateDirectory(backupPath);
                    }
                    string name = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo) + ".dat";
                    new DataCompress(backupPath, name).AddData(this.GetExportData(data));
                }
                this.stpCount();
            }
        }

        public virtual void SetDataViewColum(ColumnWidthClass usr_cwc)
        {
            this.dgvDataView.Columns["clmJobCode"].Width = usr_cwc.JobCode;
            this.dgvDataView.Columns["clmOutCode"].Width = usr_cwc.OutCode;
            this.dgvDataView.Columns["clmInputNo"].Width = usr_cwc.InputInfo;
            this.dgvDataView.Columns["clmResCode"].Width = usr_cwc.ResCode;
            this.dgvDataView.Columns["clmPattern"].Width = usr_cwc.Pattern;
            this.dgvDataView.Columns["clmJobInfo"].Width = usr_cwc.JobInfo;
            this.dgvDataView.Columns["clmTimeStamp"].Width = usr_cwc.TimeStamp;
            this.dgvDataView.Columns["clmSyu"].Width = usr_cwc.DataType;
            this.dgvDataView.Columns["clmEnd"].Width = usr_cwc.EndFlag;
            this.dgvDataView.Columns["clmAirSea"].Width = usr_cwc.AirSea;
        }

        private IData SetExportData(string s, out bool dateflag, bool flg)
        {
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            string str6 = "";
            string str7 = "";
            StringBuilder builder = new StringBuilder();
            string str8 = "";
            IData mData = null;
            int num = 0;
            using (StringReader reader = new StringReader(s))
            {
                while (reader.Peek() > -1)
                {
                    string str9;
                    switch (num)
                    {
                        case 0:
                            str9 = reader.ReadLine();
                            if (str9.Length <= 1)
                            {
                                break;
                            }
                            str8 = str9;
                            str = reader.ReadLine();
                            goto Label_00F2;

                        case 1:
                            str2 = reader.ReadLine();
                            goto Label_00F2;

                        case 2:
                            str3 = reader.ReadLine();
                            goto Label_00F2;

                        case 3:
                            str4 = reader.ReadLine();
                            goto Label_00F2;

                        case 4:
                            str5 = reader.ReadLine();
                            goto Label_00F2;

                        case 5:
                            str6 = reader.ReadLine();
                            goto Label_00F2;

                        case 6:
                            str7 = reader.ReadLine();
                            goto Label_00F2;

                        default:
                            builder.AppendLine(reader.ReadLine());
                            goto Label_00F2;
                    }
                    str8 = "";
                    str = str9;
                Label_00F2:
                    num++;
                }
            }
            RecvData data2 = DataFactory.CreateRecvData(builder.ToString());
            if (data2.MData != null)
            {
                mData = data2.MData;
            }
            else if (data2.OtherData != null)
            {
                mData = data2.OtherData;
            }
            else if (data2.ResultData != null)
            {
                mData = data2.ResultData;
            }
            if (str8 != "")
            {
                try
                {
                    mData.TimeStamp = DateTime.ParseExact(str8, "yyyyMMddHHmmss", null);
                }
                catch
                {
                    mData.TimeStamp = DateTime.Now;
                }
                dateflag = true;
            }
            else
            {
                dateflag = false;
            }
            mData.Status = (DataStatus) int.Parse(str);
            mData.UserFolder = str2;
            mData.IsRead = this.FlagCheck(int.Parse(str3));
            mData.IsPrinted = this.FlagCheck(int.Parse(str4));
            mData.IsSaved = this.FlagCheck(int.Parse(str5));
            mData.IsDeleted = this.FlagCheck(int.Parse(str6));
            if (!flg && (str7 != ""))
            {
                mData.SignFileFolder = str7;
                mData.Signflg = 1;
            }
            return mData;
        }

        private void SetFldNode(TreeNode tn)
        {
            for (int i = 0; i < tn.Nodes.Count; i++)
            {
                this.FldList.Add(this.GetFullPath(tn.Nodes[i].FullPath));
                this.SetFldNode(tn.Nodes[i]);
            }
        }

        public void SetMsgc()
        {
            this.msgc = MessageClassify.CreateInstance();
            this.trmlist = this.msgc.TermList;
        }

        public void SetOpenViewFlag(bool Flag)
        {
            this.FOpenViewFlag = Flag;
        }

        public void SetSplitter(int SplInt)
        {
            this.pnlSendRecvFolder.Width = SplInt;
        }

        public void SetUserClass(bool DebugFlag, bool ReloadFlag)
        {
            this.usr = User.CreateInstance();
            this.usrenv = UserEnvironment.CreateInstance();
            this.HSearchList = this.usr.HistorySearchList;
            this.SearchListSet();
            this.cmbSearchDiv.SelectedIndex = 0;
            this.msgc = MessageClassify.CreateInstance();
            this.trmlist = this.msgc.TermList;
            this.FldList = this.msgc.Folders;
            this.FolderSet();
            this.DBFixAdd();
            this.pi = PathInfo.CreateInstance();
            if (ReloadFlag)
            {
                string path = this.pi.DataViewPath + "Data.xml";
                string str2 = "";
                if (File.Exists(path))
                {
                    str2 = File.GetLastWriteTime(path).ToString("yyyy/MM/dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
                }
                string upTime = this.msgc.DataView.upTime;
                bool flag = (upTime == null) || (str2.CompareTo(upTime) > 0);
                bool flag2 = true;
                if (!flag)
                {
                    if (DebugFlag)
                    {
                        DialogResult result;
                        using (MessageDialog dialog = new MessageDialog())
                        {
                            result = dialog.ShowMessage("W403", null, null);
                        }
                        if (result == DialogResult.No)
                        {
                            flag2 = false;
                        }
                    }
                    else
                    {
                        using (MessageDialog dialog2 = new MessageDialog())
                        {
                            dialog2.ShowMessage("W404", null, null);
                        }
                    }
                }
                try
                {
                    this.msgc.DataView.upTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
                    this.msgc.Save();
                }
                catch (Exception exception)
                {
                    MessageDialog dialog3 = new MessageDialog();
                    dialog3.ShowMessage("E304", Resources.ResourceManager.GetString("CORE106"), null, exception);
                    dialog3.Close();
                    dialog3.Dispose();
                }
                if (flag || (!flag && !flag2))
                {
                    TreeNode selectedNode = this.tvSendRecvFolder.SelectedNode;
                    this.tvSendRecvFolder.SelectedNode = this.tvSendRecvFolder.Nodes[0];
                    this.dtsDataView.Clear();
                    if (str2 != "")
                    {
                        try
                        {
                            this.dtsDataView.Tables[0].BeginLoadData();
                            try
                            {
                                this.dtsDataView.ReadXml(path);
                            }
                            finally
                            {
                                this.dtsDataView.Tables[0].EndLoadData();
                            }
                            this.MaxID = this.GetID();
                        }
                        catch (Exception exception2)
                        {
                            MessageDialog dialog4 = new MessageDialog();
                            dialog4.ShowMessage("E403", path, null, exception2);
                            dialog4.Dispose();
                            this.ViewRepair(true);
                        }
                    }
                    this.tvSendRecvFolder.SelectedNode = selectedNode;
                }
                else
                {
                    this.ViewRepair(true);
                }
            }
            this.fs = FileSave.CreateInstance();
            UserKeySet set = new UserKeySet();
            set.ShortCutSet(this.rmnDataView.Items);
            set.ShortCutSet(this.rmnSendRecvFolder.Items);
        }

        public bool sExistenceCheck()
        {
            return (this.dtsDataView.Tables["NACCS"].Select("clStatus = 's'  AND clSign = 1 AND clDelete = 0").Length != 0);
        }

        private void SingleSave()
        {
            this.sfdDataView.Filter = "Text file（*.txt）|*.txt|All files (*.*)|*.*";
            this.sfdDataView.InitialDirectory = this.GetSaveFolder(this.GetData(this.GetKeyID()));
            if (this.sfdDataView.ShowDialog() == DialogResult.OK)
            {
                IData data = null;
                try
                {
                    int num = -1;
                    for (int i = 0; i < this.dgvDataView.RowCount; i++)
                    {
                        if (this.dgvDataView.Rows[i].Selected)
                        {
                            num = i;
                            break;
                        }
                    }
                    data = DataFactory.LoadFromDataView(this.dgvDataView.Rows[num].Cells["clmID"].Value.ToString(), Path.Combine(this.pi.DataViewPath, this.dgvDataView.Rows[num].Cells["clmFileName"].Value.ToString()));
                    DataFactory.SaveToEdiFile(data, this.sfdDataView.FileName);
                    this.SaveStatusChange(data.ID, true);
                }
                catch (Exception exception)
                {
                    MessageDialog dialog = new MessageDialog();
                    dialog.ShowMessage("E303", this.sfdDataView.FileName, null, exception);
                    dialog.Dispose();
                }
            }
        }

        private void stpCount()
        {
            string filter = "";
            string str2 = "";
            if (this.tvSendRecvFolder.SelectedNode.Level < 2)
            {
                if ((this.tvSendRecvFolder.SelectedNode.Index == 0) && (this.tvSendRecvFolder.SelectedNode.Level == 1))
                {
                    filter = "clStatus = 'r' AND clDelete = 0 AND clOpen = '0' AND clFolder = ''";
                    str2 = this.dtsDataView.Tables["NACCS"].Compute("Count(clID)", filter).ToString();
                    this.stpDataCount.Text = this.dgvDataView.RowCount.ToString() + "(" + str2 + ")";
                }
                else
                {
                    this.stpDataCount.Text = this.dgvDataView.RowCount.ToString();
                }
            }
            else
            {
                filter = "clStatus = 'r' AND clDelete = 0 AND clOpen = '0' AND clFolder = '" + this.GetFullPath(this.tvSendRecvFolder.SelectedNode.FullPath) + "'";
                str2 = this.dtsDataView.Tables["NACCS"].Compute("Count(clID)", filter).ToString();
                this.stpDataCount.Text = this.dgvDataView.RowCount.ToString() + "(" + str2 + ")";
            }
            this.UpdMenuItems();
        }

        public bool SWResCheck(string OutCode, string DataInf)
        {
            DataRow[] rowArray = this.dtsDataView.Tables["NACCS"].Select("clStatus = 'r' AND clDataInfo = '" + DataInf + "'");
            for (int i = 0; i < rowArray.Length; i++)
            {
                string str = rowArray[i]["clOutCode"].ToString().Trim();
                if (str.Length > 6)
                {
                    str = str.Substring(0, 6);
                }
                if (OutCode == str)
                {
                    return true;
                }
            }
            return false;
        }

        private void tvSendRecvFolder_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            try
            {
                string str = "";
                string label = "";
                if ((e.Label != null) && (this.tvSendRecvFolder.SelectedNode.Parent != null))
                {
                    for (int i = 0; i < this.tvSendRecvFolder.SelectedNode.Parent.Nodes.Count; i++)
                    {
                        label = e.Label;
                        if (this.LenCount(label) > 0x40)
                        {
                            label = this.SF.CutByteLength(e.Label, 0x40);
                        }
                        string str3 = this.FolderStringCut(label.ToUpper());
                        string str4 = this.FolderStringCut(this.GetFullPath(this.tvSendRecvFolder.SelectedNode.Parent.Nodes[i].Text).ToUpper());
                        if ((str3.Normalize(NormalizationForm.FormKC) == str4.Normalize(NormalizationForm.FormKC)) && (e.Node.Text.Normalize(NormalizationForm.FormKC).ToUpper() != this.GetFullPath(this.tvSendRecvFolder.SelectedNode.Parent.Nodes[i].Text).Normalize(NormalizationForm.FormKC).ToUpper()))
                        {
                            using (MessageDialog dialog = new MessageDialog())
                            {
                                dialog.ShowMessage("W505", null, null);
                            }
                            if (this.FOpenViewFlag)
                            {
                                string filterExpression = "clStatus = 'r' AND clDelete = 0 AND clOpen = '0' AND clFolder = '" + this.GetFullPath(this.tvSendRecvFolder.SelectedNode.FullPath) + "'";
                                DataRow[] rowArray = this.dtsDataView.Tables["NACCS"].Select(filterExpression);
                                str = "(" + rowArray.Length.ToString() + ")";
                            }
                            e.Node.Text = e.Node.Text + str;
                            e.CancelEdit = true;
                            return;
                        }
                    }
                }
                else
                {
                    if (this.FOpenViewFlag)
                    {
                        string str6 = "clStatus = 'r' AND clDelete = 0 AND clOpen = '0' AND clFolder = '" + this.GetFullPath(this.tvSendRecvFolder.SelectedNode.FullPath) + "'";
                        DataRow[] rowArray2 = this.dtsDataView.Tables["NACCS"].Select(str6);
                        str = "(" + rowArray2.Length.ToString() + ")";
                    }
                    e.Node.Text = e.Node.Text + str;
                    e.CancelEdit = true;
                    return;
                }
                if (e.Label.Trim() == "")
                {
                    string message = Resources.ResourceManager.GetString("CORE55");
                    using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                    {
                        form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Information, message);
                    }
                    e.Node.Text = e.Node.Text;
                    e.CancelEdit = true;
                    e.Node.BeginEdit();
                }
                else if (!this.FolderNameCheck(e.Label))
                {
                    if (this.FOpenViewFlag)
                    {
                        string str8 = "clStatus = 'r' AND clDelete = 0 AND clOpen = '0' AND clFolder = '" + this.GetFullPath(this.tvSendRecvFolder.SelectedNode.FullPath) + "'";
                        DataRow[] rowArray3 = this.dtsDataView.Tables["NACCS"].Select(str8);
                        str = "(" + rowArray3.Length.ToString() + ")";
                    }
                    e.Node.Text = e.Node.Text + str;
                    e.CancelEdit = true;
                }
                else
                {
                    label = e.Label;
                    if (this.LenCount(e.Label) > 0x40)
                    {
                        label = this.SF.CutByteLength(e.Label, 0x40);
                    }
                    string fulfld = this.GetFullPath(e.Node.Parent.FullPath) + @"\" + label;
                    try
                    {
                        this.NodeNameChange(e.Node, fulfld);
                    }
                    catch (Exception exception)
                    {
                        string str10 = Resources.ResourceManager.GetString("CORE57") + Environment.NewLine + MessageDialog.CreateExceptionMessage(exception);
                        using (MessageDialogSimpleForm form2 = new MessageDialogSimpleForm())
                        {
                            form2.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, str10);
                        }
                    }
                    if (this.FOpenViewFlag)
                    {
                        string str11 = "clStatus = 'r' AND clDelete = 0 AND clOpen = '0' AND clFolder = '" + this.GetFullPath(fulfld) + "'";
                        DataRow[] rowArray4 = this.dtsDataView.Tables["NACCS"].Select(str11);
                        str = "(" + rowArray4.Length.ToString() + ")";
                    }
                    e.Node.Text = label + str;
                    this.SaveFolder();
                    this.DBFixAdd();
                    this.DataViewRepaint(true);
                    e.CancelEdit = true;
                }
            }
            finally
            {
                this.tvSendRecvFolder.LabelEdit = false;
            }
        }

        private void tvSendRecvFolder_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.DataViewRepaint(true);
        }

        protected virtual void tvSendRecvFolder_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DataGridViewSelectedRowCollection)))
            {
                try
                {
                    TreeView view = (TreeView) sender;
                    TreeNode nodeAt = view.GetNodeAt(view.PointToClient(new Point(e.X, e.Y)));
                    if ((nodeAt != null) && this.IsMoveDataCheck(nodeAt))
                    {
                        if ((nodeAt.Level == 1) && (nodeAt.Index == 3))
                        {
                            this.DataDelete(this.dtn.Index);
                        }
                        else
                        {
                            this.drProgress = new DeleteRestore(9);
                            this.drProgress.Show();
                            try
                            {
                                this.drProgress.pgbRestore.Minimum = 0;
                                this.drProgress.pgbRestore.Maximum = this.dgvDataView.SelectedRows.Count;
                                IData data = null;
                                for (int i = this.dgvDataView.SelectedRows.Count - 1; i > -1; i--)
                                {
                                    this.drProgress.pgbRestore.Value++;
                                    this.drProgress.Refresh();
                                    DataRow[] rowArray = this.dtsDataView.Tables["NACCS"].Select("clID = " + this.dgvDataView.SelectedRows[this.dgvDataView.SelectedRows.Count - 1].Cells["clmID"].Value);
                                    data = this.GetData(int.Parse(rowArray[0]["clID"].ToString()));
                                    if (data == null)
                                    {
                                        rowArray[0].Delete();
                                    }
                                    else
                                    {
                                        if (nodeAt.Level == 1)
                                        {
                                            rowArray[0]["clFolder"] = "";
                                            data.UserFolder = "";
                                        }
                                        else
                                        {
                                            rowArray[0]["clFolder"] = this.GetFullPath(nodeAt.FullPath);
                                            data.UserFolder = this.GetFullPath(nodeAt.FullPath);
                                        }
                                        this.DataViewDataSave(data, rowArray[0]["clFileName"].ToString());
                                    }
                                }
                            }
                            catch (Exception exception)
                            {
                                string message = Resources.ResourceManager.GetString("CORE53") + Environment.NewLine + MessageDialog.CreateExceptionMessage(exception);
                                using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                                {
                                    form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, message);
                                }
                            }
                            finally
                            {
                                this.dtsDataView.AcceptChanges();
                                this.dgvDataView.Refresh();
                                this.drProgress.Close();
                                this.drProgress.Dispose();
                            }
                        }
                        this.tvSendRecvFolder.SelectedNode = this.dtn;
                        if (this.FOpenViewFlag)
                        {
                            if (((nodeAt.Level == 1) && (nodeAt.Index == 0)) || (nodeAt.Level > 1))
                            {
                                this.ViewCountChange(this.GetFullPath(nodeAt.FullPath));
                            }
                            if (((this.dtn.Level == 1) && (this.dtn.Index == 0)) || (this.dtn.Level > 1))
                            {
                                this.ViewCountChange(this.GetFullPath(this.dtn.FullPath));
                            }
                        }
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
                finally
                {
                    this.DragFlag = false;
                    this.stpCount();
                }
            }
        }

        private void tvSendRecvFolder_DragEnter(object sender, DragEventArgs e)
        {
            this.DragFlag = true;
        }

        protected virtual void tvSendRecvFolder_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DataGridViewSelectedRowCollection)))
            {
                TreeView view = (TreeView) sender;
                TreeNode nodeAt = view.GetNodeAt(view.PointToClient(new Point(e.X, e.Y)));
                if (nodeAt != null)
                {
                    if (((this.dtn.Level == 1) && (this.dtn.Index == 1)) && ((nodeAt.Level != 1) || (nodeAt.Index != 3)))
                    {
                        e.Effect = DragDropEffects.None;
                        return;
                    }
                    if (((this.dtn.Level == 1) && (this.dtn.Index == 2)) && ((nodeAt.Level != 1) || (nodeAt.Index != 3)))
                    {
                        e.Effect = DragDropEffects.None;
                        return;
                    }
                }
                if ((this.dtn.Index == 3) && (this.dtn.Level == 1))
                {
                    e.Effect = DragDropEffects.None;
                }
                else
                {
                    e.Effect = DragDropEffects.Move;
                    if ((nodeAt == null) || !this.IsMoveDataCheck(nodeAt))
                    {
                        e.Effect = DragDropEffects.None;
                    }
                    else if (!nodeAt.IsSelected)
                    {
                        view.SelectedNode = nodeAt;
                    }
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void tvSendRecvFolder_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.tvSendRecvFolder.SelectedNode = e.Node;
        }

        private void tvSendRecvFolder_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (!this.tvSendRecvFolder.SelectedNode.IsEditing)
                {
                    e.IsInputKey = true;
                    if (this.rmnFolderDelete.Enabled)
                    {
                        this.rmnFolderDelete_Click(sender, null);
                    }
                }
                else
                {
                    e.IsInputKey = true;
                }
            }
        }

        private void UDataViewForm_Load(object sender, EventArgs e)
        {
            if (!this.isDesigning)
            {
                this.DoubleBuffered = true;
                SystemEnvironment.CreateInstance();
                this.dgvDataView.Columns["clmNum"].Visible = false;
                this.dgvDataView.Columns["clmStatus"].Visible = false;
                this.dgvDataView.Columns["clmID"].Visible = false;
                this.dgvDataView.Columns["clmOpenFlag"].Visible = false;
                this.dgvDataView.Columns["clmPrintFlag"].Visible = false;
                this.dgvDataView.Columns["clmFolder"].Visible = false;
                this.dgvDataView.Columns["clmSaveFlag"].Visible = false;
                this.dgvDataView.Columns["clmDelete"].Visible = false;
                this.dgvDataView.Columns["clmFileName"].Visible = false;
                this.dgvDataView.Columns["clmDataInfo"].Visible = false;
                this.dgvDataView.Columns["clmUserCode"].Visible = false;
                this.dgvDataView.Columns["clmAttach"].Visible = false;
                this.dgvDataView.Columns["clmAirSea"].Visible = false;
                this.dgvDataView.Columns["clmSignflg"].Visible = false;
                this.dgvDataView.Columns["clmSignFilePath"].Visible = false;
                DBSort item = new DBSort {
                    ID = 1,
                    Name = "clmTimeStamp",
                    Flag = false
                };
                this.DBSortList.Add(item);
            }
        }

        public void UpdateCount()
        {
            this.stpCount();
            if (this.FOpenViewFlag)
            {
                this.ViewCountCheck(true);
            }
            this.MaxID = this.GetID();
        }

        public string UpdateJobData(IData Data)
        {
            string filter = string.Format("clStatus = 's' AND clDelete = false", Data.ID);
            if (((int) this.dtsDataView.Tables["NACCS"].Compute("Count(clID)", filter)) < 1)
            {
                Data.TimeStamp = DateTime.Now;
                return this.AppendJobData(Data, 0, false, false, false);
            }
            this.DataViewDataUpdate(Data);
            DataRow[] rowArray = this.dtsDataView.Tables["NACCS"].Select("clID = " + Data.ID);
            this.DataViewDataSave(Data, rowArray[0]["clFileName"].ToString());
            this.stpCount();
            return Data.ID;
        }

        public void UpdDataViewMainItems(bool ViewRefresh, bool Import, bool Export, bool DemoFlag)
        {
            this.rmnDataViewRefresh.Enabled = ViewRefresh;
            this.rmnImport.Enabled = Import;
            this.rmnExport.Enabled = Export;
        }

        protected virtual void UpdMenuItems()
        {
            if ((this.tvSendRecvFolder.SelectedNode.Level > 3) || ((this.tvSendRecvFolder.SelectedNode.Level < 2) && (this.tvSendRecvFolder.SelectedNode.Tag.ToString() != "1")))
            {
                this.rmnFolderCreate.Enabled = false;
            }
            else
            {
                this.rmnFolderCreate.Enabled = true;
            }
            if (this.tvSendRecvFolder.SelectedNode.Level < 2)
            {
                this.rmnChangeName.Enabled = false;
            }
            else
            {
                this.rmnChangeName.Enabled = true;
            }
            if ((this.tvSendRecvFolder.SelectedNode.Level < 2) || (this.tvSendRecvFolder.SelectedNode.GetNodeCount(true) > 0))
            {
                this.rmnFolderDelete.Enabled = false;
            }
            else
            {
                this.rmnFolderDelete.Enabled = true;
            }
            if ((this.dgvDataView.SelectedRows.Count > 0) && ((this.tvSendRecvFolder.SelectedNode.Level != 1) || (this.tvSendRecvFolder.SelectedNode.Index != 3)))
            {
                this.rmnDelete.Enabled = true;
            }
            else
            {
                this.rmnDelete.Enabled = false;
            }
            if (((this.tvSendRecvFolder.SelectedNode.Index == 3) && (this.tvSendRecvFolder.SelectedNode.Level == 1)) && (this.dgvDataView.SelectedRows.Count > 0))
            {
                this.rmnUndo.Enabled = true;
            }
            else
            {
                this.rmnUndo.Enabled = false;
            }
            if ((this.dgvDataView.SelectedRows.Count > 0) && ((this.tvSendRecvFolder.SelectedNode.Level != 1) || ((this.tvSendRecvFolder.SelectedNode.Index != 3) && (this.tvSendRecvFolder.SelectedNode.Index != 1))))
            {
                this.rmnExport.Enabled = true;
            }
            else
            {
                this.rmnExport.Enabled = false;
            }
            this.rmnOpen.Enabled = this.rmnDelete.Enabled;
            this.rmnJobOpen.Enabled = this.rmnDelete.Enabled;
            this.rmnPrint.Enabled = this.rmnDelete.Enabled;
            this.rmnPrintPreview.Enabled = this.rmnDelete.Enabled;
            this.rmnFileSave.Enabled = this.rmnDelete.Enabled;
            this.rmnSendSearch.Enabled = (this.dgvDataView.SelectedRows.Count > 0) && (((this.tvSendRecvFolder.SelectedNode.Index == 0) && (this.tvSendRecvFolder.SelectedNode.Level == 1)) || (this.tvSendRecvFolder.SelectedNode.Level > 1));
            this.rmnRecvSearch.Enabled = ((this.dgvDataView.SelectedRows.Count > 0) && (this.tvSendRecvFolder.SelectedNode.Index == 2)) && (this.tvSendRecvFolder.SelectedNode.Level == 1);
            this.OnUpdateDataViewItems(this, this.rmnFolderCreate.Enabled, this.rmnChangeName.Enabled, this.rmnFolderDelete.Enabled, this.rmnDelete.Enabled, this.rmnUndo.Enabled, this.rmnSendSearch.Enabled, this.rmnRecvSearch.Enabled, this.rmnExport.Enabled);
        }

        public void ViewCountChange(string pathstr)
        {
            if (pathstr != "")
            {
                TreeNode folderPathNode = this.GetFolderPathNode(pathstr);
                if (((folderPathNode.Level == 1) && (folderPathNode.Index == 0)) || (folderPathNode.Level >= 2))
                {
                    if (folderPathNode.Text.LastIndexOf('(') > -1)
                    {
                        folderPathNode.Text = folderPathNode.Text.Remove(folderPathNode.Text.LastIndexOf('('));
                    }
                    string fullPath = "";
                    if ((folderPathNode.Level != 1) || (folderPathNode.Index != 0))
                    {
                        fullPath = this.GetFullPath(folderPathNode.FullPath);
                    }
                    string filter = "clStatus = 'r' AND clDelete = 0 AND clOpen = '0' AND clFolder = '" + fullPath + "'";
                    string str3 = this.dtsDataView.Tables["NACCS"].Compute("Count(clID)", filter).ToString();
                    folderPathNode.Text = folderPathNode.Text + "(" + str3 + ")";
                }
            }
        }

        public void ViewCountCheck(bool flag)
        {
            if (this.FOpenViewFlag)
            {
                if (flag)
                {
                    for (int j = 0; j < this.tvSendRecvFolder.Nodes.Count; j++)
                    {
                        this.NoOpenDelete(this.tvSendRecvFolder.Nodes[j]);
                    }
                }
                for (int i = 0; i < this.tvSendRecvFolder.Nodes.Count; i++)
                {
                    this.NoOpenSet(this.tvSendRecvFolder.Nodes[i]);
                }
            }
            else
            {
                for (int k = 0; k < this.tvSendRecvFolder.Nodes.Count; k++)
                {
                    this.NoOpenDelete(this.tvSendRecvFolder.Nodes[k]);
                }
            }
        }

        public void ViewRefresh()
        {
            TreeNode selectedNode = this.tvSendRecvFolder.SelectedNode;
            this.tvSendRecvFolder.TreeViewNodeSorter = new NodeSorter();
            this.tvSendRecvFolder.SelectedNode = selectedNode;
        }

        public void ViewRepair(bool StartFlag)
        {
            if (!StartFlag)
            {
                DialogResult result;
                using (MessageDialog dialog = new MessageDialog())
                {
                    result = dialog.ShowMessage("C407", null, null);
                }
                if (result != DialogResult.Yes)
                {
                    return;
                }
                this.DataViewNoRepaint(true);
            }
            ULogClass.LogWrite(string.Format("ViewRepair Start", new object[0]));
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            this.drProgress = new DeleteRestore(6);
            if (StartFlag)
            {
                this.drProgress.TopMost = true;
            }
            this.drProgress.Show();
            int num = 0;
            string path = Path.Combine(this.pi.LogPath, "Repair" + DateTime.Now.ToString("yyyyMMddHHmmssff"));
            try
            {
                try
                {
                    this.dtsDataView.Clear();
                    string[] strArray = new string[0];
                    if (Directory.Exists(this.pi.DataViewPath))
                    {
                        strArray = Directory.GetFiles(this.pi.DataViewPath, "*.txt", SearchOption.TopDirectoryOnly);
                    }
                    ULogClass.LogWrite(string.Format("ViewRepair GetFiles Count={0} [timer {1}]", strArray.Length, stopwatch.ElapsedMilliseconds));
                    IData data = null;
                    this.drProgress.pgbRestore.Minimum = 0;
                    this.drProgress.pgbRestore.Maximum = strArray.Length;
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        this.drProgress.pgbRestore.Value = i + 1;
                        this.drProgress.Refresh();
                        try
                        {
                            data = DataFactory.LoadFromDataView(i.ToString(), strArray[i]);
                        }
                        catch
                        {
                            if ((num == 0) && !Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            string destFileName = Path.Combine(path, Path.GetFileName(strArray[i].ToString()));
                            try
                            {
                                File.Copy(strArray[i], destFileName, true);
                                File.Delete(strArray[i]);
                            }
                            catch
                            {
                            }
                            num++;
                            continue;
                        }
                        if ((!data.IsDeleted && (data.Status == DataStatus.Recept)) && !string.IsNullOrEmpty(data.UserFolder))
                        {
                            data.UserFolder = this.FolderAddSet(data.UserFolder);
                        }
                        data.ID = i.ToString();
                        string fileName = Path.GetFileName(strArray[i].ToString());
                        this.DataViewDataAdd(data, fileName, false);
                        this.DataViewDataSave(data, fileName.ToString());
                    }
                }
                catch (Exception exception)
                {
                    this.dgvDataView.Refresh();
                    if (!StartFlag)
                    {
                        this.stpCount();
                        if (this.FOpenViewFlag)
                        {
                            this.ViewCountCheck(true);
                        }
                    }
                    MessageDialog dialog2 = new MessageDialog();
                    dialog2.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, Resources.ResourceManager.GetString("CORE107") + Environment.NewLine + MessageDialog.CreateExceptionMessage(exception));
                    dialog2.Dispose();
                    return;
                }
                this.dgvDataView.Refresh();
                if (!StartFlag)
                {
                    this.stpCount();
                    if (this.FOpenViewFlag)
                    {
                        this.ViewCountCheck(true);
                    }
                }
            }
            finally
            {
                stopwatch.Stop();
                ULogClass.LogWrite(string.Format("ViewRepair Ended [Timer {0}]", stopwatch.ElapsedMilliseconds));
                this.MaxID = this.GetID();
                if (!StartFlag)
                {
                    this.DataViewNoRepaint(false);
                }
                this.drProgress.Close();
                this.drProgress.Dispose();
                if (num > 0)
                {
                    using (MessageDialog dialog3 = new MessageDialog())
                    {
                        dialog3.ShowMessage("W402", num.ToString(), "");
                    }
                }
            }
        }

        public void ViewRestore()
        {
            DialogResult result;
            using (MessageDialog dialog = new MessageDialog())
            {
                result = dialog.ShowMessage("C408", null, null);
            }
            if (result == DialogResult.Yes)
            {
                string str = "";
                string[] strArray = new string[0];
                if (Directory.Exists(this.pi.BackupPath))
                {
                    strArray = Directory.GetFiles(this.pi.BackupPath, "*.dat", SearchOption.TopDirectoryOnly);
                }
                if (strArray.Length <= 0)
                {
                    DialogResult result2;
                    using (MessageDialog dialog2 = new MessageDialog())
                    {
                        result2 = dialog2.ShowMessage("C409", null, null);
                    }
                    if (result2 != DialogResult.Yes)
                    {
                        return;
                    }
                }
                if (Directory.Exists(this.pi.DataViewPath))
                {
                    Directory.Delete(this.pi.DataViewPath, true);
                }
                if (Directory.Exists(this.pi.AttachPath))
                {
                    string[] directories = Directory.GetDirectories(this.pi.AttachPath);
                    for (int i = 0; i < directories.Length; i++)
                    {
                        this.AttachDelete(directories[i]);
                    }
                }
                this.dtsDataView.Clear();
                this.drProgress = new DeleteRestore(10);
                this.drProgress.Show();
                try
                {
                    this.drProgress.pgbRestore.Minimum = 0;
                    this.drProgress.pgbRestore.Maximum = strArray.Length;
                    for (int j = 0; j < strArray.Length; j++)
                    {
                        this.drProgress.pgbRestore.Value = j + 1;
                        this.drProgress.Refresh();
                        str = strArray[j].ToString();
                        DataCompress compress = new DataCompress(Path.GetDirectoryName(strArray[j].ToString()), Path.GetFileName(strArray[j].ToString()));
                        int count = compress.IndexList.Count;
                        IData data = null;
                        for (int k = 0; k < count; k++)
                        {
                            bool flag;
                            string s = compress.GetData(k);
                            data = this.SetExportData(s, out flag, false);
                            if (data != null)
                            {
                                data.UserFolder = this.FolderAddSet(data.UserFolder);
                                this.AppendJobData(data, (int) data.Status, false, false, flag);
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    string message = Resources.ResourceManager.GetString("CORE65") + Environment.NewLine + Resources.ResourceManager.GetString("CORE66") + str + Environment.NewLine + Resources.ResourceManager.GetString("CORE67") + Environment.NewLine + MessageDialog.CreateExceptionMessage(exception);
                    using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                    {
                        form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, message);
                    }
                    return;
                }
                finally
                {
                    this.MaxID = this.GetID();
                    this.SaveFolder();
                    this.dtsDataView.AcceptChanges();
                    this.dgvDataView.Refresh();
                    this.stpCount();
                    if (this.FOpenViewFlag)
                    {
                        this.ViewCountCheck(true);
                    }
                    this.drProgress.Close();
                    this.drProgress.Dispose();
                }
                using (MessageDialog dialog3 = new MessageDialog())
                {
                    dialog3.ShowMessage("I502", null);
                }
            }
        }

        public bool ViewRevert(string path)
        {
            bool flag;
            if (base.InvokeRequired)
            {
                ViewRevertDelegate method = new ViewRevertDelegate(this.ViewRevert);
                return (bool) base.Invoke(method, new object[] { path });
            }
            try
            {
                IData data = DataFactory.LoadFromDataView(this.MaxID.ToString(), path);
                if ((!data.IsDeleted && (data.Status == DataStatus.Recept)) && !string.IsNullOrEmpty(data.UserFolder))
                {
                    data.UserFolder = this.FolderAddSet(data.UserFolder);
                }
                this.DataViewDataAdd(data, Path.GetFileName(path), false);
                this.MaxID = this.GetID();
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        protected DataSet InhDataSet
        {
            get
            {
                return this.dtsDataView;
            }
        }

        protected DataGridView InhDataView
        {
            get
            {
                return this.dgvDataView;
            }
        }

        protected ContextMenuStrip InhDataViewMenu
        {
            get
            {
                return this.rmnDataView;
            }
        }

        protected bool InhDragFlag
        {
            get
            {
                return this.DragFlag;
            }
            set
            {
                this.DragFlag = value;
            }
        }

        protected ContextMenuStrip InhFolderMenu
        {
            get
            {
                return this.rmnSendRecvFolder;
            }
        }

        protected TreeView InhFolderTree
        {
            get
            {
                return this.tvSendRecvFolder;
            }
        }

        protected bool InhOpenViewFlag
        {
            get
            {
                return this.FOpenViewFlag;
            }
        }

        protected ComboBox InhSearchCombo
        {
            get
            {
                return this.cmbSearchDiv;
            }
        }

        protected List<DBSort> InhSortList
        {
            get
            {
                return this.DBSortList;
            }
        }

        public DataGridView InternalDataGrid
        {
            get
            {
                return this.dgvDataView;
            }
        }

        public DataSet InternalDataSet
        {
            get
            {
                return this.dtsDataView;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct DBFix
        {
            public int ID;
            public int Priority;
            public string UserCode;
            public string JobCode;
            public string OutCode;
            public string Folder;
            public string Open;
        }

        [StructLayout(LayoutKind.Sequential)]
        protected struct DBSort
        {
            public int ID;
            public string Name;
            public bool Flag;
        }

        public class NodeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                TreeNode node = x as TreeNode;
                TreeNode node2 = y as TreeNode;
                if (node.Level < 2)
                {
                    return 1;
                }
                return string.Compare(node.Text, node2.Text);
            }
        }

        private delegate bool ViewRevertDelegate(string path);
    }
}

