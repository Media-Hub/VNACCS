namespace Naccs.Interactive.Classes
{
    using Naccs.Core.Classes;
    using Naccs.Core.Settings;
    using Naccs.Net;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    public class AtOnceData : Form
    {
        private Button btCancel;
        private Button btOK;
        private const int CnAccesskeyLen = 0x10;
        private const int CnJobLen = 5;
        private const int CnTerminalLen = 6;
        private IContainer components;
        private const string CRLF = "\r\n";
        private const string FmtNetworkError = "{0}_NetworkError";
        private const string FmtRecvData = "{0}_RECV";
        private const string FmtSendData = "{0}_SEND";
        private const string FmtSendError = "{0}_SEND Error code({1})";
        private Label lblKey;
        private Label lblTerminal;
        private int mA2Delay = 3;
        private int mA2Timeout = 180;
        private System.Windows.Forms.Timer mA2tm;
        private bool mEndWait;
        private IData mFirstRequest;
        private IHttpClient mHttpClient;
        private bool mIsExcute;
        private int mIsExcuteMode;
        private bool mIsTTM;
        private bool mLogon;
        private bool mNextStop;
        private object mOwnerControl;
        private string mPass;
        private bool mRecvOK;
        private int mSendCount;
        private string mTermAccessKey;
        private string mTermLogicalName;
        private string mTurnRtpinfo;
        private string mTxAccessKey = "";
        private string mTxTerminal = "";
        private bool mUseDemo;
        private string mUser;
        private TextBox txAccessKey;
        private TextBox txTerminal;
        private const string UseCharset = "UTF-8";

        public event AtonceNetworkErrorHandler OnNetworkError;

        public event AtonceReceivedHandler OnReceived;

        public AtOnceData(object owner, bool demo, int systemkind)
        {
            this.InitializeComponent();
            base.Disposed += new EventHandler(this.AtOnceData_Disposed);
            this.mOwnerControl = owner;
            this.mUseDemo = demo;
            base.Owner = (Form) owner;
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
                int num = this.mHttpClient.Send(this.mOwnerControl, str + this.mSendCount.ToString(), senddata);
                if (num > 0)
                {
                    ULogClass.LogWrite(string.Format("{0}_SEND Error code({1})", str, num));
                    this.OnNetworkError(num, null);
                    this.mIsExcute = false;
                }
            }
            catch (Exception exception)
            {
                ULogClass.LogWrite(string.Format("{0}_SEND Error code({1})", this.JobCode(), "Exception"));
                HttpExceptionEx ex = new HttpExceptionEx(HttpStatus.Idle, HttpCause.ProgramError, exception);
                this.OnNetworkError(9, ex);
                this.mIsExcute = false;
            }
            this.mNextStop = false;
        }

        private void A2Send_Tick(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = (System.Windows.Forms.Timer) sender;
            timer.Enabled = false;
            timer.Dispose();
            this.A2Send();
        }

        private void AtOnceData_Disposed(object sender, EventArgs e)
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

        private void AtOnceData_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.NoticeDlgCancel();
            }
        }

        private void AtOnceData_Shown(object sender, EventArgs e)
        {
            this.txTerminal.Focus();
        }

        private void AtOnceDlg_FormClosed(object sender, FormClosedEventArgs e)
        {
            base.Owner.Activate();
            base.Hide();
        }

        private void AtOnceDlg_Load(object sender, EventArgs e)
        {
            base.Hide();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.NoticeDlgCancel();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            string s = this.txTerminal.Text.Trim();
            int byteCount = Encoding.GetEncoding("UTF-8").GetByteCount(s);
            string str2 = null;
            if (byteCount < this.txTerminal.MaxLength)
            {
                str2 = "W304";
            }
            else if (!this.IsNumOrAlfaData(s))
            {
                str2 = "W305";
            }
            if (!string.IsNullOrEmpty(str2))
            {
                using (MessageDialog dialog = new MessageDialog())
                {
                    dialog.ShowMessage(str2, null, null);
                }
                this.txTerminal.Focus();
            }
            else
            {
                s = this.txAccessKey.Text.Trim();
                byteCount = Encoding.GetEncoding("UTF-8").GetByteCount(s);
                if ((byteCount <= 0) || (byteCount > this.txAccessKey.MaxLength))
                {
                    str2 = "W306";
                    this.txAccessKey.Text = s;
                    this.txAccessKey.Focus();
                }
                else if (!this.IsNumOrAlfaData(s))
                {
                    str2 = "W307";
                }
                if (!string.IsNullOrEmpty(str2))
                {
                    using (MessageDialog dialog2 = new MessageDialog())
                    {
                        dialog2.ShowMessage(str2, null, null);
                    }
                    this.txAccessKey.Focus();
                }
                else
                {
                    this.mTxTerminal = this.txTerminal.Text;
                    this.mTxAccessKey = this.txAccessKey.Text;
                    base.Owner.Activate();
                    base.Hide();
                    this.SendTimerStart();
                }
            }
        }

        private void CallbackOnError(IHttpClient sender, string sendkey, HttpExceptionEx ex)
        {
            ULogClass.LogWrite(string.Format("{0}_NetworkError", this.JobCode()));
            this.mNextStop = false;
            this.OnNetworkError(0, ex);
            this.mIsExcute = false;
        }

        private void CallbackOnReceived(object sender, string sendkey, ComParameter recvdata)
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
                flag = data.Header.Control.EndsWith("P");
                flag2 = ((this.IsExcuteMode == 0) && !flag) && (data.Header.DataType == "R");
            }
            Stopwatch stopwatch = new Stopwatch();
            try
            {
                stopwatch.Start();
                this.mRecvOK = this.OnReceived(recvdata);
                stopwatch.Stop();
                if (flag)
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
                            flag = false;
                            ComParameter parameter = new ComParameter {
                                DataString = "A2Timeout"
                            };
                            this.OnReceived(parameter);
                            new MessageDialog().ShowMessage("E121", null);
                        }
                    }
                    else
                    {
                        flag = false;
                        this.mNextStop = false;
                    }
                }
                else if (flag2)
                {
                    flag = false;
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
                this.mIsExcute = flag;
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

        private string CreateRequestData()
        {
            string mTermLogicalName;
            string mTermAccessKey;
            this.mSendCount = 0;
            string str = this.JobCode();
            if (this.mIsTTM)
            {
                mTermLogicalName = this.txTerminal.Text.Trim();
                mTermAccessKey = this.txAccessKey.Text.Trim();
            }
            else
            {
                mTermLogicalName = this.mTermLogicalName;
                mTermAccessKey = this.mTermAccessKey;
            }
            mTermLogicalName = mTermLogicalName.PadRight(6, ' ');
            mTermAccessKey = mTermAccessKey.PadRight(0x10, ' ');
            IData data = new NaccsData {
                Header = { Control = DataControl.GetControl(), JobCode = str, UserId = this.mUser, Password = this.mPass, Path = this.mTermLogicalName, DataInfo = string.Format("{0}{1}{2:D6}", str.PadRight(5, ' '), this.DateTimeString(), this.mSendCount) }
            };
            if (this.mLogon)
            {
                data.Header.SendGuard = "*";
            }
            string str4 = "0";
            if (this.mIsExcuteMode != 0)
            {
                str4 = "1";
            }
            string str5 = string.Format("{0}{3}{1}{3}{2}{3}", new object[] { mTermLogicalName, mTermAccessKey, str4, "\r\n" });
            data.JobData = str5;
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
            this.lblTerminal = new Label();
            this.txTerminal = new TextBox();
            this.btOK = new Button();
            this.lblKey = new Label();
            this.txAccessKey = new TextBox();
            this.btCancel = new Button();
            base.SuspendLayout();
//            this.lblTerminal.AutoSize = true;
            this.lblTerminal.Location = new Point(8, 0x13);
            this.lblTerminal.Name = "lblTerminal";
            this.lblTerminal.Size = new Size(0x56, 12);
            this.lblTerminal.TabIndex = 1;
            this.lblTerminal.Text = "M\x00e3 số m\x00e1y trạm";
            this.txTerminal.CharacterCasing = CharacterCasing.Upper;
//            this.txTerminal.ImeMode = ImeMode.Disable;
            this.txTerminal.Location = new Point(0x70, 0x10);
            this.txTerminal.MaxLength = 6;
            this.txTerminal.Name = "txTerminal";
            this.txTerminal.Size = new Size(0x44, 0x13);
            this.txTerminal.TabIndex = 0;
            this.txTerminal.Tag = "";
            this.txTerminal.KeyPress += new KeyPressEventHandler(this.TextBox_KeyPress);
            this.btOK.Location = new Point(0x30, 80);
            this.btOK.Name = "btOK";
            this.btOK.Size = new Size(0x4b, 0x17);
            this.btOK.TabIndex = 4;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new EventHandler(this.btOK_Click);
            this.lblKey.AutoSize = true;
            this.lblKey.Location = new Point(8, 0x2e);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new Size(0x65, 12);
            this.lblKey.TabIndex = 3;
            this.lblKey.Text = "Kh\x00f3a truy cập";
            this.txAccessKey.CharacterCasing = CharacterCasing.Upper;
//            this.txAccessKey.ImeMode = ImeMode.Disable;
            this.txAccessKey.Location = new Point(0x70, 0x2b);
            this.txAccessKey.MaxLength = 0x10;
            this.txAccessKey.Name = "txAccessKey";
            this.txAccessKey.Size = new Size(0x9b, 0x13);
            this.txAccessKey.TabIndex = 2;
            this.txAccessKey.KeyPress += new KeyPressEventHandler(this.TextBox_KeyPress);
//            this.btCancel.DialogResult = DialogResult.Cancel;
            this.btCancel.Location = new Point(0xa2, 80);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new Size(0x4b, 0x17);
            this.btCancel.TabIndex = 5;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new EventHandler(this.btCancel_Click);
            base.AcceptButton = this.btOK;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btCancel;
            base.ClientSize = new Size(0x117, 0x77);
            base.Controls.Add(this.btCancel);
            base.Controls.Add(this.txAccessKey);
            base.Controls.Add(this.lblKey);
            base.Controls.Add(this.btOK);
            base.Controls.Add(this.txTerminal);
            base.Controls.Add(this.lblTerminal);
            this.DoubleBuffered = true;
//            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "AtOnceData";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Thay v\x00ec nhận được một tin nhắn";
            base.FormClosing += new FormClosingEventHandler(this.AtOnceData_FormClosing);
            base.FormClosed += new FormClosedEventHandler(this.AtOnceDlg_FormClosed);
            base.Load += new EventHandler(this.AtOnceDlg_Load);
            base.Shown += new EventHandler(this.AtOnceData_Shown);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private bool IsNumOrAlfa(char ch)
        {
            if (((ch < '0') || (ch > '9')) && ((ch < 'A') || (ch > 'Z')))
            {
                return false;
            }
            return true;
        }

        private bool IsNumOrAlfaData(string data)
        {
            char[] chArray = data.ToCharArray();
            for (int i = 0; i < chArray.Length; i++)
            {
                if (!this.IsNumOrAlfa(chArray[i]))
                {
                    return false;
                }
            }
            return true;
        }

        private string JobCode()
        {
            if (this.mIsTTM)
            {
                return "TTM";
            }
            return "#REP1";
        }

        public void NextStop()
        {
            if (this.mIsExcute)
            {
                this.mNextStop = true;
            }
        }

        private void NoticeDlgCancel()
        {
            this.mIsExcute = false;
            ComParameter recvdata = new ComParameter {
                DataString = "DialogCancel"
            };
            this.OnReceived(recvdata);
        }

        private void RequestSend()
        {
            try
            {
                ComParameter senddata = new ComParameter {
                    DataString = this.CreateRequestData()
                };
                string str = this.JobCode();
                if (this.mLogon)
                {
                    ULogClass.LogWrite(string.Format("{0}_SEND", "LOGON"), senddata.DataString.Substring(0, 400), true);
                }
                else
                {
                    ULogClass.LogWrite(string.Format("{0}_SEND", str), senddata.DataString.Substring(0, 400), true);
                }
                int num = this.mHttpClient.Send(this.mOwnerControl, str + this.mSendCount.ToString(), senddata);
                if (num > 0)
                {
                    ULogClass.LogWrite(string.Format("{0}_SEND Error code({1})", str, num));
                    this.OnNetworkError(num, null);
                    this.mIsExcute = false;
                }
            }
            catch (Exception exception)
            {
                ULogClass.LogWrite(string.Format("{0}_SEND Error code({1})", this.JobCode(), "Exception"));
                HttpExceptionEx ex = new HttpExceptionEx(HttpStatus.Idle, HttpCause.ProgramError, exception);
                this.OnNetworkError(9, ex);
                this.mIsExcute = false;
            }
        }

        private void RequestStart(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = (System.Windows.Forms.Timer) sender;
            timer.Enabled = false;
            timer.Dispose();
            this.RequestSend();
        }

        public bool SendLogon(IHttpClient client, string user, string password)
        {
            if (this.IsExcute)
            {
                return false;
            }
            this.mLogon = true;
            return this.SendREP(client, 0, user, password);
        }

        public bool SendREP(IHttpClient client, int excutemode, string user, string password)
        {
            if (this.IsExcute)
            {
                return false;
            }
            this.mIsExcute = true;
            this.mIsExcuteMode = excutemode;
            this.mIsTTM = false;
            this.Usersetting();
            this.mHttpClient = (IHttpClient) client.Clone();
            this.mUser = user;
            this.mPass = password;
            this.mHttpClient.OnSent += new SentEventHandler(this.CallbackOnSent);
            this.mHttpClient.OnReceived += new ReceivedEventHandler(this.CallbackOnReceived);
            this.mHttpClient.OnNetworkError += new NetworkErrorEventHandler(this.CallbackOnError);
            this.SendTimerStart();
            return true;
        }

        private void SendTimerStart()
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer {
                Interval = 3
            };
            timer.Tick += new EventHandler(this.RequestStart);
            timer.Enabled = true;
        }

        public bool SendTTM(IHttpClient client, string user, string password)
        {
            if (this.IsExcute)
            {
                return false;
            }
            this.mIsExcute = true;
            this.mIsExcuteMode = 0;
            this.mIsTTM = true;
            this.Usersetting();
            this.mHttpClient = (IHttpClient) client.Clone();
            this.mUser = user;
            this.mPass = password;
            this.mHttpClient.OnSent += new SentEventHandler(this.CallbackOnSent);
            this.mHttpClient.OnReceived += new ReceivedEventHandler(this.CallbackOnReceived);
            this.mHttpClient.OnNetworkError += new NetworkErrorEventHandler(this.CallbackOnError);
            this.txTerminal.Text = this.mTxTerminal;
            this.txAccessKey.Text = this.mTxAccessKey;
            this.ShowTimerStart();
            return true;
        }

        private void ShowStart(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = (System.Windows.Forms.Timer) sender;
            timer.Enabled = false;
            timer.Dispose();
            base.ShowDialog();
        }

        private void ShowTimerStart()
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer {
                Interval = 3
            };
            timer.Tick += new EventHandler(this.ShowStart);
            timer.Enabled = true;
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == '\x0016')
                {
                    if (Clipboard.GetDataObject().GetDataPresent(DataFormats.UnicodeText))
                    {
                        string data = (string) Clipboard.GetDataObject().GetData(DataFormats.UnicodeText);
                        if (this.IsNumOrAlfaData(data))
                        {
                            data = data.ToUpper();
                            TextBox box = (TextBox) sender;
                            int selectionStart = box.SelectionStart;
                            string text = box.Text;
                            if (box.SelectionLength > 0)
                            {
                                text = text.Remove(selectionStart, box.SelectionLength);
                            }
                            int num2 = (text.Length + data.Length) - box.MaxLength;
                            if (num2 > 0)
                            {
                                data = data.Remove(data.Length - num2);
                            }
                            text = text.Insert(box.SelectionStart, data);
                            box.Text = text;
                            box.SelectionStart = selectionStart + data.Length;
                            box.SelectionLength = 0;
                        }
                    }
                    e.Handled = true;
                }
            }
            else
            {
                char ch = char.ToUpper(e.KeyChar);
                if (this.IsNumOrAlfa(ch))
                {
                    e.KeyChar = ch;
                }
                else
                {
                    e.Handled = true;
                }
            }
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

        public int IsExcuteMode
        {
            get
            {
                return this.mIsExcuteMode;
            }
        }

        public bool IsREP1
        {
            get
            {
                return !this.mIsTTM;
            }
        }

        public bool IsTTM
        {
            get
            {
                return this.mIsTTM;
            }
        }
    }
}

