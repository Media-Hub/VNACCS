namespace Naccs.Interactive.Classes
{
    using Naccs.Core.Classes;
    using Naccs.Core.Settings;
    using Naccs.Interactive.Properties;
    using Naccs.Net;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Threading;
    using System.Windows.Forms;

    public class AccumulateData : Form
    {
        private Button btCancel;
        private Button btClose;
        private Button btGet;
        private Button btList;
        private ColumnHeader clmCount;
        private ColumnHeader clmOutcode;
        private const int CnCommandLen = 7;
        private const string CnTotal = "Tổng hạng mục: {0:n0}";
        private IContainer components;
        private const string CRLF = "\r\n";
        private const string FmtNetworkError = "{0}_NetworkError";
        private const string FmtRecvData = "{0}_RECV";
        private const string FmtSendCommand = "{0}_SEND Command({1})";
        private const string FmtSendData = "{0}_SEND";
        private const string FmtSendError = "{0}_SEND Error code({1})";
        private Label lblTotal;
        private ListView lvList;
        private int mA2Delay = 3;
        private int mA2Timeout = 180;
        private System.Windows.Forms.Timer mA2tm;
        private IHttpClient mCmdClient;
        private bool mEndWait;
        private IData mFirstRequest;
        private IHttpClient mGetClient;
        private bool mIsExcute;
        private bool mIsSYG;
        private bool mNextStop;
        private object mOwnerControl;
        private string mPass;
        private bool mRecvOK;
        private bool mRefSending;
        private int mSendCount;
        private string mSyskind = "2";
        private string mTermAccessKey;
        private string mTermLogicalName;
        private string mTurnRtpinfo;
        private bool mUseDemo;
        private string mUser;

        public event AccumulateNetworkErrorHandler OnNetworkError;

        public event AccumulateReceivedHandler OnReceived;

        public AccumulateData(object owner, bool demo, int systemkind)
        {
            this.InitializeComponent();
            base.Disposed += new EventHandler(this.AccumulateData_Disposed);
            this.mOwnerControl = owner;
            base.Owner = (Form) owner;
            this.mUseDemo = demo;
            this.mSyskind = systemkind.ToString();
            SystemEnvironment environment = SystemEnvironment.CreateInstance();
            this.mA2Delay = environment.TerminalInfo.A2MinimamDelay;
            this.mA2Timeout = environment.TerminalInfo.A2TimeOut;
        }

        private void A2Send()
        {
            try
            {
                ComParameter senddata = new ComParameter {
                    DataString = this.CreateA2Data()
                };
                string str = this.JobCode();
                if (this.mNextStop)
                {
                    ULogClass.LogWrite(string.Format("{0}_SEND", str + "(Next Stop)"), senddata.DataString.Substring(0, 400), true);
                }
                else
                {
                    ULogClass.LogWrite(string.Format("{0}_SEND", str), senddata.DataString.Substring(0, 400), true);
                }
                int num = this.mGetClient.Send(this.mOwnerControl, str + this.mSendCount.ToString(), senddata);
                if (num > 0)
                {
                    ULogClass.LogWrite(string.Format("{0}_SEND Error code({1})", str, num));
                    this.OnNetworkError(num, null);
                    this.mIsExcute = false;
                    this.ButtonLock(false);
                }
            }
            catch (Exception exception)
            {
                ULogClass.LogWrite(string.Format("{0}_SEND Error code({1})", this.JobCode(), "Exception"));
                HttpExceptionEx ex = new HttpExceptionEx(HttpStatus.Idle, HttpCause.ProgramError, exception);
                this.OnNetworkError(9, ex);
                this.mIsExcute = false;
                this.ButtonLock(false);
            }
            this.mNextStop = false;
        }

        private void A2Send_Tick(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = (System.Windows.Forms.Timer) sender;
            timer.Enabled = false;
            timer.Dispose();
            this.mA2tm = null;
            this.A2Send();
        }

        private void AccumulateData_Disposed(object sender, EventArgs e)
        {
            if ((this.mIsExcute && (this.mA2tm != null)) && this.mA2tm.Enabled)
            {
                this.mA2tm.Enabled = false;
                this.mA2tm.Dispose();
                this.mOwnerControl = new Control();
                this.mNextStop = true;
                this.A2Send();
                this.mEndWait = true;
                for (int i = 0; i < 5; i++)
                {
                    if (!this.mEndWait)
                    {
                        return;
                    }
                    Thread.Sleep(100);
                }
            }
        }

        private void AccumulateData_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                base.Owner.Activate();
                base.Hide();
            }
        }

        private void AccumulateData_Load(object sender, EventArgs e)
        {
            base.Hide();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.NextStop();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            base.Owner.Activate();
            base.Hide();
        }

        private void btGet_Click(object sender, EventArgs e)
        {
            if (this.btGet.Enabled)
            {
                if (this.lvList.SelectedItems.Count > 0)
                {
                    this.ButtonLock(true);
                    string text = this.lvList.SelectedItems[0].Text;
                    this.RequestSend(RequestCommand.GET, text);
                }
                else
                {
                    string message = Resources.ResourceManager.GetString("INTR01");
                    using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                    {
                        form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Information, message);
                    }
                }
            }
        }

        private void btList_Click(object sender, EventArgs e)
        {
            this.ButtonLock(true);
            this.RequestSend(RequestCommand.REF, null);
        }

        private void ButtonLock(bool bLock)
        {
            if (bLock)
            {
                this.btList.Enabled = false;
                this.btGet.Enabled = false;
                this.btCancel.Enabled = false;
            }
            else
            {
                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                timer.Tick += new EventHandler(this.LockCancel);
                timer.Interval = 3;
                timer.Enabled = true;
            }
        }

        private void CallbackOnCmdReceived(object sender, string sendkey, ComParameter recvdata)
        {
            if (recvdata.DataString.Length < 400)
            {
                ULogClass.LogWrite(string.Format("{0}_RECV", this.JobCode()), recvdata.DataString, false);
                this.mIsExcute = false;
            }
            else
            {
                ULogClass.LogWrite(string.Format("{0}_RECV", this.JobCode()), recvdata.DataString.Substring(0, 400), true);
                if (sendkey.StartsWith(RequestCommand.REF.ToString()))
                {
                    this.RefResponce(recvdata);
                }
            }
            this.ButtonLock(false);
        }

        private void CallbackOnError(IHttpClient sender, string sendkey, HttpExceptionEx ex)
        {
            ULogClass.LogWrite(string.Format("{0}_NetworkError", this.JobCode()));
            this.mNextStop = false;
            this.OnNetworkError(0, ex);
            this.mIsExcute = false;
            this.ButtonLock(false);
        }

        private void CallbackOnGetReceived(object sender, string sendkey, ComParameter recvdata)
        {
            bool flag = false;
            bool flag2 = false;
            if (recvdata.DataString.Length < 400)
            {
                ULogClass.LogWrite(string.Format("{0}_RECV", this.JobCode()), recvdata.DataString, false);
                this.mTurnRtpinfo = "";
                this.mNextStop = false;
            }
            else
            {
                IData data = new NaccsData {
                    Header = { DataString = recvdata.DataString.Substring(0, 400) }
                };
                this.mTurnRtpinfo = data.Header.RtpInfo;
                flag2 = data.Header.Control.EndsWith("P");
                flag = !flag2 && (data.Header.DataType == "R");
            }
            Stopwatch stopwatch = new Stopwatch();
            try
            {
                stopwatch.Start();
                this.mRecvOK = this.OnReceived(recvdata);
                stopwatch.Stop();
                if (flag2)
                {
                    if (this.mRecvOK)
                    {
                        if ((this.mA2Timeout == 0) || (stopwatch.ElapsedMilliseconds < ((this.mA2Timeout - this.mA2Delay) * 0x3e8)))
                        {
                            this.mA2tm = new System.Windows.Forms.Timer();
                            this.mA2tm.Tick += new EventHandler(this.A2Send_Tick);
                            this.mA2tm.Interval = 1;
                            if (this.mA2Delay > 0)
                            {
                                this.mA2tm.Interval = this.mA2Delay * 0x3e8;
                            }
                            this.mA2tm.Enabled = true;
                        }
                        else
                        {
                            flag2 = false;
                            ComParameter parameter = new ComParameter {
                                DataString = "A2Timeout"
                            };
                            this.OnReceived(parameter);
                            new MessageDialog().ShowMessage("E121", null);
                        }
                    }
                    else
                    {
                        flag2 = false;
                        this.mNextStop = false;
                    }
                }
                else if (flag)
                {
                    flag2 = false;
                    this.mNextStop = false;
                    RecvData data2 = DataFactory.CreateRecvData(recvdata.DataString);
                    HttpErrorDlg dlg = new HttpErrorDlg();
                    dlg.ShowJobError(this.JobCode(), data2.ResultData);
                    dlg.Dispose();
                }
            }
            finally
            {
                stopwatch.Reset();
                this.mIsExcute = flag2;
                this.ButtonLock(false);
            }
        }

        private void CallbackOnSent(object sender, string key)
        {
            if (this.mEndWait)
            {
                this.mEndWait = false;
            }
        }

        private string CreateA2Data()
        {
            this.mSendCount++;
            IData data = new NaccsData();
            if (this.mRecvOK)
            {
                data.Header.JobCode = "?A2";
            }
            else
            {
                data.Header.JobCode = "?A3";
            }
            data.Header.Control = this.mFirstRequest.Header.Control;
            data.Header.UserId = this.mFirstRequest.Header.UserId;
            data.Header.Password = this.mFirstRequest.Header.Password;
            data.Header.Path = this.mFirstRequest.Header.Path;
            data.Header.DataInfo = string.Format("{0}{1}{2:D6}", data.Header.Path, this.DateTimeString(), this.mSendCount);
            data.Header.RtpInfo = this.mTurnRtpinfo;
            if (this.mNextStop)
            {
                data.Header.SendGuard = "*";
            }
            else
            {
                data.Header.SendGuard = " ";
            }
            return data.GetDataString();
        }

        private string CreateRequestData(RequestCommand command, string outcode)
        {
            string str2;
            this.mSendCount = 0;
            string str = this.JobCode();
            IData data = new NaccsData {
                Header = { Control = DataControl.GetControl(), JobCode = str, UserId = this.mUser, Password = this.mPass, Path = this.mTermLogicalName, DataInfo = string.Format("{0}{1}{2:D6}", str, this.DateTimeString(), this.mSendCount) }
            };
            if (command == RequestCommand.GET)
            {
                str2 = outcode;
            }
            else
            {
                str2 = command.ToString();
            }
            string str3 = string.Format("{0}{1}", str2.PadRight(7, ' '), "\r\n");
            data.JobData = str3;
            this.mFirstRequest = data;
            return data.GetDataString();
        }

        private string DateTimeString()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss", DateTimeFormatInfo.InvariantInfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lvList = new ListView();
            this.clmOutcode = new ColumnHeader();
            this.clmCount = new ColumnHeader();
            this.btList = new Button();
            this.btCancel = new Button();
            this.btGet = new Button();
            this.btClose = new Button();
            this.lblTotal = new Label();
            base.SuspendLayout();
//            this.lvList.Alignment = ListViewAlignment.Left;
            this.lvList.Columns.AddRange(new ColumnHeader[] { this.clmOutcode, this.clmCount });
            this.lvList.Cursor = Cursors.Default;
            this.lvList.FullRowSelect = true;
            this.lvList.GridLines = true;
            this.lvList.HideSelection = false;
            this.lvList.Location = new Point(10, 8);
            this.lvList.MultiSelect = false;
            this.lvList.Name = "lvList";
//            this.lvList.RightToLeft = RightToLeft.No;
            this.lvList.Size = new Size(0xb9, 0xeb);
            this.lvList.Sorting = SortOrder.Ascending;
            this.lvList.TabIndex = 0;
            this.lvList.UseCompatibleStateImageBehavior = false;
            this.lvList.View = View.Details;
            this.lvList.DoubleClick += new EventHandler(this.lvList_DoubleClick);
            this.clmOutcode.Text = "M\x00e3 message đầu ra";
            this.clmOutcode.Width = 120;
            this.clmCount.Text = "số";
            this.clmCount.Width = 50;
            this.btList.Location = new Point(220, 12);
            this.btList.Name = "btList";
            this.btList.Size = new Size(120, 0x1a);
            this.btList.TabIndex = 1;
            this.btList.Text = "C\x00f3 được danh s\x00e1ch";
            this.btList.UseVisualStyleBackColor = true;
            this.btList.Click += new EventHandler(this.btList_Click);
            this.btCancel.Enabled = false;
            this.btCancel.Location = new Point(220, 0x67);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new Size(120, 0x1a);
            this.btCancel.TabIndex = 3;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new EventHandler(this.btCancel_Click);
            this.btGet.Location = new Point(220, 0x3a);
            this.btGet.Name = "btGet";
            this.btGet.Size = new Size(120, 0x1a);
            this.btGet.TabIndex = 2;
            this.btGet.Text = "Lấy dữ liệu";
            this.btGet.UseVisualStyleBackColor = true;
            this.btGet.Click += new EventHandler(this.btGet_Click);
//            this.btClose.DialogResult = DialogResult.Cancel;
            this.btClose.Location = new Point(220, 0xd9);
            this.btClose.Name = "btClose";
            this.btClose.Size = new Size(120, 0x1a);
            this.btClose.TabIndex = 4;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new EventHandler(this.btClose_Click);
            this.lblTotal.AutoSize = true;
            this.lblTotal.ForeColor = SystemColors.ActiveCaption;
            this.lblTotal.Location = new Point(80, 0xf6);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Padding = new Padding(0, 3, 0, 0);
            this.lblTotal.Size = new Size(0x5c, 15);
            this.lblTotal.TabIndex = 6;
            this.lblTotal.Text = "Tổng hạng mục: 0";
            this.lblTotal.TextAlign = ContentAlignment.MiddleRight;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btClose;
            base.ClientSize = new Size(0x178, 0x10b);
            base.Controls.Add(this.lblTotal);
            base.Controls.Add(this.btClose);
            base.Controls.Add(this.btGet);
            base.Controls.Add(this.btCancel);
            base.Controls.Add(this.btList);
            base.Controls.Add(this.lvList);
//            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "AccumulateData";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Th\x00f4ng điệp lưu trữ";
            base.FormClosing += new FormClosingEventHandler(this.AccumulateData_FormClosing);
            base.Load += new EventHandler(this.AccumulateData_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private string JobCode()
        {
            if (this.mIsSYG)
            {
                return "SYG";
            }
            return "REQ";
        }

        private void LockCancel(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = (System.Windows.Forms.Timer) sender;
            timer.Enabled = false;
            timer.Dispose();
            this.btList.Enabled = true;
            this.btCancel.Enabled = true;
            if (this.lvList.Items.Count > 0)
            {
                this.btGet.Enabled = !this.mIsExcute;
                this.btCancel.Enabled = this.mIsExcute;
            }
            else
            {
                this.btGet.Enabled = false;
                this.btCancel.Enabled = false;
            }
            this.lvList.Select();
        }

        private void lvList_DoubleClick(object sender, EventArgs e)
        {
            this.btGet_Click(sender, e);
        }

        public void NextStop()
        {
            this.ButtonLock(true);
            if (this.mIsExcute)
            {
                this.mNextStop = true;
            }
        }

        private void RefResponce(ComParameter recvdata)
        {
            RecvData data = DataFactory.CreateRecvData(recvdata.DataString);
            if (data.OtherData == null)
            {
                HttpErrorDlg dlg = new HttpErrorDlg();
                dlg.ShowJobError(this.JobCode(), data.ResultData);
                dlg.Dispose();
            }
            else
            {
                string text = null;
                if (this.lvList.SelectedItems.Count > 0)
                {
                    text = this.lvList.SelectedItems[0].Text;
                }
                this.lvList.Items.Clear();
                int num = 0;
                for (int i = 1; i < data.OtherData.ItemCount; i += 2)
                {
                    int num2;
                    ListViewItem item = new ListViewItem {
                        Text = data.OtherData.Items[i]
                    };
                    if (!string.IsNullOrEmpty(text) && (text == item.Text))
                    {
                        item.Selected = true;
                    }
                    try
                    {
                        num2 = int.Parse(data.OtherData.Items[i + 1].Trim());
                    }
                    catch
                    {
                        num2 = 0;
                    }
                    num += num2;
                    item.SubItems.Add(string.Format("{0:n0}", num2));
                    this.lvList.Items.Add(item);
                }
                this.lblTotal.Text = string.Format("Tổng hạng mục: {0:n0}", num);
            }
        }

        private void RefSend(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = (System.Windows.Forms.Timer) sender;
            timer.Enabled = false;
            timer.Dispose();
            this.RequestSend(RequestCommand.REF, null);
            base.ShowDialog();
            this.mRefSending = false;
        }

        private void RefSendTimerStart()
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(this.RefSend);
            timer.Interval = 3;
            timer.Enabled = true;
        }

        private void RequestSend(RequestCommand command, string outcode)
        {
            try
            {
                int num;
                if (command == RequestCommand.GET)
                {
                    this.mIsExcute = true;
                }
                ComParameter senddata = new ComParameter {
                    DataString = this.CreateRequestData(command, outcode)
                };
                ULogClass.LogWrite(string.Format("{0}_SEND Command({1})", this.JobCode(), command.ToString()), senddata.DataString.Substring(0, 400), true);
                if (command == RequestCommand.GET)
                {
                    num = this.mGetClient.Send(this.mOwnerControl, command.ToString() + this.mSendCount.ToString(), senddata);
                }
                else
                {
                    num = this.mCmdClient.Send(this.mOwnerControl, command.ToString() + this.mSendCount.ToString(), senddata);
                }
                if (num > 0)
                {
                    ULogClass.LogWrite(string.Format("{0}_SEND Error code({1})", this.JobCode(), num));
                    this.OnNetworkError(num, null);
                    this.mIsExcute = false;
                    this.ButtonLock(false);
                }
            }
            catch (Exception exception)
            {
                ULogClass.LogWrite(string.Format("{0}_SEND Error code({1})", this.JobCode(), "Exception"));
                HttpExceptionEx ex = new HttpExceptionEx(HttpStatus.Idle, HttpCause.ProgramError, exception);
                this.OnNetworkError(9, ex);
                this.mIsExcute = false;
                this.ButtonLock(false);
            }
        }

        public bool SendREQ(IHttpClient client, string user, string password)
        {
            if (this.mIsExcute && this.mIsSYG)
            {
                return false;
            }
            if (!this.mRefSending)
            {
                this.mIsSYG = false;
                this.mRefSending = true;
                this.Text = "Th\x00f4ng điệp lưu trữ";
                if (!this.mIsExcute)
                {
                    this.lvList.Items.Clear();
                    this.Usersetting();
                    this.SetClient(client);
                    this.mUser = user;
                    this.mPass = password;
                }
                this.ButtonLock(true);
                this.RefSendTimerStart();
            }
            return true;
        }

        public bool SendSYG(IHttpClient client, string user, string password)
        {
            if (this.mIsExcute && !this.mIsSYG)
            {
                return false;
            }
            if (!this.mRefSending)
            {
                this.mIsSYG = true;
                this.mRefSending = true;
                this.Text = Resources.ResourceManager.GetString("INTR08");
                if (!this.mIsExcute)
                {
                    this.lvList.Items.Clear();
                    this.Usersetting();
                    this.SetClient(client);
                    this.mUser = user;
                    this.mPass = password;
                }
                this.ButtonLock(true);
                this.RefSendTimerStart();
            }
            return true;
        }

        private void SetClient(IHttpClient client)
        {
            this.mGetClient = (IHttpClient) client.Clone();
            this.mCmdClient = (IHttpClient) client.Clone();
            this.mGetClient.OnSent += new SentEventHandler(this.CallbackOnSent);
            this.mGetClient.OnReceived += new ReceivedEventHandler(this.CallbackOnGetReceived);
            this.mGetClient.OnNetworkError += new NetworkErrorEventHandler(this.CallbackOnError);
            this.mCmdClient.OnSent += new SentEventHandler(this.CallbackOnSent);
            this.mCmdClient.OnReceived += new ReceivedEventHandler(this.CallbackOnCmdReceived);
            this.mCmdClient.OnNetworkError += new NetworkErrorEventHandler(this.CallbackOnError);
        }

        private void Usersetting()
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

        public bool IsExcute
        {
            get
            {
                return this.mIsExcute;
            }
        }

        public bool IsREQ
        {
            get
            {
                return !this.mIsSYG;
            }
        }

        public bool IsSYG
        {
            get
            {
                return this.mIsSYG;
            }
        }

        private enum RequestCommand
        {
            REF,
            GET
        }
    }
}

