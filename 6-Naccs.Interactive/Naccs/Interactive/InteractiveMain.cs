namespace Naccs.Interactive
{
    using Naccs.Core.BatchDoc;
    using Naccs.Core.Classes;
    using Naccs.Core.DataView;
    using Naccs.Core.Job;
    using Naccs.Core.Main;
    using Naccs.Interactive.Classes;
    using Naccs.Net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class InteractiveMain : MainBase
    {
        public AccumulateData acm;
        public IHttpClient acm_ihc;
        public AtOnceData ato;
        public IHttpClient ato_ihc;
        public IHttpClient bat_ihc;
        private bool bSendFlag;
        private const string C_APPENDERROR = "Failure in Registering.";
        private const string C_COMPLITION = "00000-0000-0000";
        private const string C_SENDERROR = "Failure in Sending.";
        private const int C_USERCOUNT = 30;
        private SignaturesCert CA;
        private bool cancelflag;
        private IContainer components;
        private bool ConnectFlag;
        private bool DateCheckFlag;
        private FolderBrowserDialog folderBrowserDialog1;
        public IHttpClient ihc;
        private _KioskLinkData KioskLinkData;
        private string KioskLinkSystem;
        public AtOnceData logon_ato;
        public IHttpClient logon_ihc;
        private IData LogonData;
        private object Logonsender;
        private bool LogonSendFlag;
        private int SendAllCount;
        private int SendCount;
        private object sendjobsender;
        private bool SignSendFlg;
        private UUserCode uUserCodeI;

        public InteractiveMain() : this(false)
        {
        }

        public InteractiveMain(bool proxyError) : base(proxyError)
        {
            this.SignSendFlg = true;
            this.KioskLinkSystem = " ";
            this.InitializeComponent();
        }

        public void Accumulate_OnNetworkError(int senderror, HttpExceptionEx ex)
        {
            string str;
            HttpErrorDlg dlg = new HttpErrorDlg();
            base.stbReqStatus.Text = "";
            dlg.ShowHttpError(senderror, ex);
            if (ex == null)
            {
                str = System.Enum.GetNames(typeof(HttpResult))[senderror].ToString();
            }
            else
            {
                str = System.Enum.GetNames(typeof(HttpCause))[(int) ex.Cause].ToString();
            }
            ULogClass.LogWrite("Accumulate_NetworkError Status=" + str);
            dlg.Dispose();
        }

        public bool Accumulate_OnReceived(ComParameter recvdata)
        {
            if (base.DisposingFlag)
            {
                base.stbReqStatus.Text = "";
                ULogClass.LogWrite("Logoff Staus = Abolished the messages and ended extraction due to logoff status.");
                return false;
            }
            if (this.acm.IsREQ)
            {
                base.stbReqStatus.Text = "RECEIVING";
            }
            else
            {
                base.stbReqStatus.Text = "RECEIVING";
            }
            if (recvdata.DataString.Length < 400)
            {
                base.stbReqStatus.Text = "";
                return true;
            }
            string str = "";
            RecvData data = new RecvData();
            try
            {
                data = DataFactory.CreateRecvData(recvdata);
            }
            catch
            {
                str = "-1";
            }
            IData mData = null;
            if (data.MData != null)
            {
                mData = data.MData;
                str = base.idv.AppendJobData(mData, 2, true, true, false);
            }
            else
            {
                if (data.ResultData != null)
                {
                    base.stbReqStatus.Text = "";
                    mData = data.ResultData;
                    if (!mData.Header.Control.EndsWith("P"))
                    {
                        ULogClass.LogWrite("INET_RECV_OTHER ", mData.GetDataString().Substring(0, 400), true);
                        return true;
                    }
                    str = base.idv.AppendJobData(mData, 2, true, true, false);
                }
                if (data.OtherData != null)
                {
                    mData = data.OtherData;
                    str = base.idv.AppendJobData(mData, 2, true, true, false);
                }
            }
            if (str == "-1")
            {
                MessageDialog dialog = new MessageDialog();
                dialog.ShowMessage("E404", "", "");
                dialog.Dispose();
                return false;
            }
            base.ReceiveNoticeMessage(mData);
            if (mData != null)
            {
                ULogClass.LogWrite("INET_RECV_OTHER ", mData.GetDataString().Substring(0, 400), true);
                try
                {
                    string path = base.SaveFileCheck(mData);
                    if (path != "")
                    {
                        if (mData.Header.DataType == "A")
                        {
                            base.idv.AppendRecord(mData, Path.GetDirectoryName(path));
                        }
                        else
                        {
                            DataFactory.SaveToEdiFile(mData, path);
                            base.idv.SaveStatusChange(mData.ID, true);
                        }
                    }
                }
                catch
                {
                }
                mData = base.ContainedSet(mData);
                base.pr.Print(this, mData.ID, mData, 2);
            }
            if (mData != null)
            {
                base.idv.Distribute(mData);
            }
            return true;
        }

        public void AtOnce_OnNetworkError(int senderror, HttpExceptionEx ex)
        {
            string str;
            HttpErrorDlg dlg = new HttpErrorDlg();
            base.stbRepStatus.Text = "";
            dlg.ShowHttpError(senderror, ex);
            if (ex == null)
            {
                str = System.Enum.GetNames(typeof(HttpResult))[senderror].ToString();
            }
            else
            {
                str = System.Enum.GetNames(typeof(HttpCause))[(int) ex.Cause].ToString();
            }
            ULogClass.LogWrite("AtOnce_NetworkError Status=" + str);
            dlg.Dispose();
        }

        public bool AtOnce_OnReceived(ComParameter recvdata)
        {
            if (base.DisposingFlag)
            {
                base.stbRepStatus.Text = "";
                ULogClass.LogWrite("Logoff Staus = Abolished the messages and ended extraction due to logoff status.");
                return false;
            }
            if (recvdata.DataString.Length < 400)
            {
                base.stbRepStatus.Text = "";
                return true;
            }
            string str = "";
            RecvData data = new RecvData();
            try
            {
                data = DataFactory.CreateRecvData(recvdata);
            }
            catch
            {
                str = "-1";
            }
            IData mData = null;
            if (data.MData != null)
            {
                mData = data.MData;
                str = base.idv.AppendJobData(mData, 2, true, true, false);
            }
            else
            {
                if (data.ResultData != null)
                {
                    base.stbRepStatus.Text = "";
                    mData = data.ResultData;
                    if (!mData.Header.Control.EndsWith("P"))
                    {
                        ULogClass.LogWrite("INET_RECV_OTHER ", mData.GetDataString().Substring(0, 400), true);
                        return true;
                    }
                    str = base.idv.AppendJobData(mData, 2, true, true, false);
                }
                if (data.OtherData != null)
                {
                    mData = data.OtherData;
                    if (mData.Header.DataType == "U")
                    {
                        mData = base.SWCheck(mData);
                    }
                    str = base.idv.AppendJobData(mData, 2, true, true, false);
                }
            }
            base.ReceiveNoticeMessage(mData);
            if (mData != null)
            {
                ULogClass.LogWrite("INET_RECV_OTHER ", mData.GetDataString().Substring(0, 400), true);
                try
                {
                    string path = base.SaveFileCheck(mData);
                    if (path != "")
                    {
                        if (mData.Header.DataType == "A")
                        {
                            base.idv.AppendRecord(mData, Path.GetDirectoryName(path));
                        }
                        else
                        {
                            DataFactory.SaveToEdiFile(mData, path);
                            base.idv.SaveStatusChange(mData.ID, true);
                        }
                    }
                }
                catch
                {
                }
                mData = base.ContainedSet(mData);
                base.pr.Print(this, mData.ID, mData, 2);
            }
            if (mData != null)
            {
                base.idv.Distribute(mData);
            }
            if (str == "-1")
            {
                MessageDialog dialog = new MessageDialog();
                dialog.ShowMessage("E404", "", "");
                dialog.Dispose();
                return false;
            }
            return true;
        }

        private bool BatchDoc_OnReceived(ComParameter recvdata)
        {
            if (recvdata.DataString.Length >= 400)
            {
                string str = "";
                RecvData data = new RecvData();
                try
                {
                    data = DataFactory.CreateRecvData(recvdata);
                }
                catch
                {
                    str = "-1";
                }
                IData mData = null;
                if (data.MData != null)
                {
                    mData = data.MData;
                    str = base.idv.AppendJobData(mData, 2, true, true, false);
                }
                else
                {
                    if (data.ResultData != null)
                    {
                        mData = data.ResultData;
                        str = base.idv.AppendJobData(mData, 2, true, true, false);
                    }
                    if (data.OtherData != null)
                    {
                        mData = data.OtherData;
                        str = base.idv.AppendJobData(mData, 2, true, true, false);
                    }
                }
                if (mData != null)
                {
                    ULogClass.LogWrite("INET_RECV_OTHER ", mData.GetDataString().Substring(0, 400), true);
                    try
                    {
                        string fileName = base.SaveFileCheck(mData);
                        if (fileName != "")
                        {
                            DataFactory.SaveToEdiFile(mData, fileName);
                            base.idv.SaveStatusChange(mData.ID, true);
                        }
                    }
                    catch
                    {
                    }
                    mData = base.ContainedSet(mData);
                    base.pr.Print(this, mData.ID, mData, 2);
                }
                if (mData != null)
                {
                    base.idv.Distribute(mData);
                }
                if (str == "-1")
                {
                    MessageDialog dialog = new MessageDialog();
                    dialog.ShowMessage("E404", "", "");
                    dialog.Dispose();
                }
            }
            return true;
        }

        private void BatchSend(bool flg = true)
        {
            int sendKey = base.idv.GetSendKey(flg);
            this.SignSendFlg = flg;
            if ((sendKey > -1) && !this.cancelflag)
            {
                this.SendCount++;
                IData data = base.idv.GetData(sendKey);
                if (data == null)
                {
                    base.idv.RecordDelete(sendKey);
                    System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer {
                        Interval = 10
                    };
                    timer.Tick += new EventHandler(this.tm_Tick);
                    timer.Enabled = true;
                }
                else
                {
                    this.OnSend(null, data);
                }
            }
            else
            {
                base.Enabled = true;
                this.SendRecvDlgHide();
                this.bSendFlag = false;
                if (base.BatchSendFlag)
                {
                    base.BatchSendFlag = false;
                    MessageDialog dialog = new MessageDialog();
                    if (dialog.ShowMessage("I104", base.SendReportDlg.StatusCount(USendReportDlg.ReportStatus.Sent).ToString(), "") == DialogResult.Yes)
                    {
                        base.SendReportDlg.Show();
                    }
                    dialog.Dispose();
                }
                else
                {
                    MessageDialog dialog2 = new MessageDialog();
                    dialog2.ShowMessage("I103", this.SendCount.ToString(), "");
                    dialog2.Dispose();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FileSend(object sender)
        {
            this.cancelflag = false;
            if (base.ComStatus == 0)
            {
                MessageDialog dialog = new MessageDialog();
                if (dialog.ShowMessage("C102", "") != DialogResult.Yes)
                {
                    dialog.Dispose();
                    return;
                }
                dialog.Dispose();
            }
            base.ofdMain.Multiselect = true;
            base.ofdMain.InitialDirectory = base.GetInitialDirectory();
            if (base.ofdMain.ShowDialog() == DialogResult.OK)
            {
                base.SendReportDlg.Dispose();
                base.SendReportDlg = new USendReportDlg();
                base.BatchSendFlag = true;
                DeleteRestore restore = new DeleteRestore(1) {
                    pgbRestore = { Minimum = 0, Maximum = base.ofdMain.FileNames.Length }
                };
                restore.Show();
                base.Enabled = false;
                base.SendReportDlg.Clear();
                for (int i = 0; i < base.ofdMain.FileNames.Length; i++)
                {
                    restore.pgbRestore.Value = i + 1;
                    restore.Refresh();
                    base.SendReportDlg.Add(base.ofdMain.FileNames[i]);
                    Application.DoEvents();
                    IData data = null;
                    try
                    {
                        data = DataFactory.LoadFromEdiFile(base.ofdMain.FileNames[i]);
                    }
                    catch (Exception exception)
                    {
                        base.SendReportDlg.SetStatus(i, USendReportDlg.ReportStatus.Error, null, "Failure in Registering.");
                        MessageDialog dialog2 = new MessageDialog();
                        dialog2.ShowMessage("E302", base.ofdMain.FileNames[i], null, exception);
                        dialog2.Dispose();
                        continue;
                    }
                    if ((sender == null) && (data.JobCode.Trim() == ""))
                    {
                        base.SendReportDlg.SetStatus(i, USendReportDlg.ReportStatus.Error, null, "Failure in Registering.");
                        base.JobError(-1);
                    }
                    else if (data != null)
                    {
                        object obj2;
                        if (sender == null)
                        {
                            if (!base.SearchDispCode(data))
                            {
                                UJobInputDlg dlg = new UJobInputDlg();
                                dlg.SetCodeSelect(data.JobCode.Trim());
                                if (dlg.ShowDialog() == DialogResult.OK)
                                {
                                    data.Header.DispCode = dlg.DispCode;
                                    dlg.Close();
                                    dlg.Dispose();
                                }
                                else
                                {
                                    dlg.Close();
                                    dlg.Dispose();
                                    base.SendReportDlg.SetStatus(i, USendReportDlg.ReportStatus.Error, null, "Failure in Registering.");
                                    continue;
                                }
                            }
                            obj2 = base.JobFormOpen(data, false);
                        }
                        else
                        {
                            try
                            {
                                ((CommonJobForm) sender).SetSendData(data);
                                obj2 = sender;
                            }
                            catch
                            {
                                base.JobError(-11);
                                continue;
                            }
                        }
                        if (obj2 == null)
                        {
                            base.SendReportDlg.SetStatus(i, USendReportDlg.ReportStatus.Error, null, "Failure in Registering.");
                        }
                        else
                        {
                            string str;
                            try
                            {
                                str = ((CommonJobForm) obj2).AppendJobData();
                            }
                            catch
                            {
                                str = "";
                            }
                            if (str == "")
                            {
                                base.SendReportDlg.SetStatus(i, USendReportDlg.ReportStatus.Error, null, "Failure in Registering.");
                            }
                            else
                            {
                                if (sender == null)
                                {
                                    ((CommonJobForm) obj2).Close();
                                    ((CommonJobForm) obj2).Dispose();
                                }
                                base.SendReportDlg.SetStatus(i, USendReportDlg.ReportStatus.Wait, str, "");
                            }
                        }
                    }
                    else
                    {
                        base.SendReportDlg.SetStatus(i, USendReportDlg.ReportStatus.Error, null, "Failure in Registering.");
                    }
                }
                base.Enabled = true;
                this.SendRecvDlgHide();
                restore.Close();
                restore.Dispose();
                if (base.ComStatus == 1)
                {
                    base.StatusChange(1);
                    this.bSendFlag = true;
                    bool flg = true;
                    if (base.idv.sExistenceCheck())
                    {
                        MessageDialog dialog3 = new MessageDialog();
                        if (dialog3.ShowMessage("I105", "") == DialogResult.Yes)
                        {
                            this.SendAllCount = base.idv.GetCount(1);
                            flg = true;
                        }
                        else
                        {
                            this.SendAllCount = base.idv.GetCount(5);
                            flg = false;
                        }
                    }
                    else
                    {
                        this.SendAllCount = base.idv.GetCount(1);
                    }
                    this.SendCount = 0;
                    this.BatchSend(flg);
                }
                else
                {
                    base.BatchSendFlag = false;
                    MessageDialog dialog4 = new MessageDialog();
                    if (dialog4.ShowMessage("I002", base.SendReportDlg.StatusCount(USendReportDlg.ReportStatus.Wait).ToString(), "") == DialogResult.Yes)
                    {
                        base.SendReportDlg.Show();
                    }
                    dialog4.Dispose();
                }
            }
        }

        private void Http_OnError(IHttpClient sender, string sendkey, HttpExceptionEx ex)
        {
            this.ConnectFlag = false;
            base.Enabled = true;
            if ((base.frmSendRecv != null) && base.frmSendRecv.Visible)
            {
                this.SendRecvDlgHide();
            }
            base.StatusChange(1);
            if (ex.Cause != HttpCause.UserCancel)
            {
                HttpErrorDlg dlg = new HttpErrorDlg();
                dlg.ShowHttpError(ex);
                if (ex.WebStatus == WebExceptionStatus.UnknownError)
                {
                    this.CA = null;
                }
                ULogClass.LogWrite("NetworkError Status=" + System.Enum.GetNames(typeof(HttpCause))[(int) ex.Cause].ToString());
                dlg.Dispose();
            }
            if (this.bSendFlag)
            {
                if (ex.Cause != HttpCause.UserCancel)
                {
                    base.SendReportDlg.SetStatus(sendkey, USendReportDlg.ReportStatus.Wait, "Failure in Sending.");
                }
                if (base.BatchSendFlag)
                {
                    base.BatchSendFlag = false;
                    MessageDialog dialog = new MessageDialog();
                    if (dialog.ShowMessage("I104", base.SendReportDlg.StatusCount(USendReportDlg.ReportStatus.Sent).ToString(), "") == DialogResult.Yes)
                    {
                        base.SendReportDlg.Show();
                    }
                    dialog.Dispose();
                }
                this.bSendFlag = false;
            }
        }

        private void Http_OnReceived(object sender, string sendkey, ComParameter dt)
        {
            string[,] strArray = new string[,] { { "High security alert", "E123" }, { "Verification GW Error", "E134" }, { "Message Format GW Error", "E135" } };
            ULogClass.LogWrite(string.Format("INET_RECV_START Length={0} AttachCount={1}", dt.DataString.Length, dt.AttachCount), null, false);
            this.ConnectFlag = false;
            base.idv.SendStatusChange(sendkey);
            for (int i = 0; i < strArray.GetLength(0); i++)
            {
                if ((dt.DataString.IndexOf(Environment.NewLine) != 0x18e) && dt.DataString.Contains(strArray[i, 0]))
                {
                    base.StatusChange(1);
                    if (!this.bSendFlag)
                    {
                        base.Enabled = true;
                        this.SendRecvDlgHide();
                    }
                    MessageDialog dialog = new MessageDialog();
                    dialog.ShowMessage(strArray[i, 1].ToString(), "", dt.DataString);
                    dialog.Dispose();
                    if (strArray[i, 0] == "Verification GW Error")
                    {
                        this.CA = null;
                    }
                    return;
                }
            }
            if (dt.DataString.Length < 400)
            {
                dt.DataString = base.ErrCreate(dt.DataString);
            }
            RecvData recvdata = DataFactory.CreateRecvData(dt);
            IData data = null;
            IData data3 = null;
            IData data4 = null;
            string str = "-1";
            if (recvdata.MData != null)
            {
                data = recvdata.ResultData;
                if (base.usrenv.InteractiveInfo.JobConnect && (data.JobData.Substring(0, 15) == "00000-0000-0000"))
                {
                    this.RepSend(1);
                }
                data = recvdata.MData;
                str = base.idv.AppendJobData(data, 2, false, true, false);
            }
            else
            {
                if (recvdata.ResultData != null)
                {
                    data = recvdata.ResultData;
                    if (base.usrenv.InteractiveInfo.JobConnect && (data.JobData.Substring(0, 15) == "00000-0000-0000"))
                    {
                        this.RepSend(1);
                    }
                    str = base.idv.AppendJobData(data, 2, false, true, false);
                    data3 = data;
                }
                if (recvdata.OtherData != null)
                {
                    data = recvdata.OtherData;
                    str = base.idv.AppendJobData(data, 2, false, true, false);
                    data4 = data;
                }
            }
            ULogClass.LogWrite(string.Format("INET_RECV_DB_APPEND Key={0}", str), null, false);
            base.StatusChange(1);
            if (!this.bSendFlag)
            {
                base.Enabled = true;
                this.SendRecvDlgHide();
            }
            if (this.DateCheckFlag)
            {
                try
                {
                    string s = data.Header.ServerRecvTime.ToString().Trim().PadRight(14, '0');
                    string format = "yyyyMMddHHmmss";
                    DateTime time = DateTime.ParseExact(s, format, null);
                    DateTime now = DateTime.Now;
                    TimeSpan span = (TimeSpan) (now - time);
                    if ((-5.0 > span.TotalMinutes) || (5.0 < span.TotalMinutes))
                    {
                        MessageDialog dialog2 = new MessageDialog();
                        dialog2.ShowMessage("W001", "Server's Time : " + time.ToString("dd/MM/yyyy HH:mm:ss", DateTimeFormatInfo.InvariantInfo) + Environment.NewLine + "PC's time : " + now.ToString("dd/MM/yyyy HH:mm:ss", DateTimeFormatInfo.InvariantInfo) + Environment.NewLine + Environment.NewLine, "");
                        dialog2.Dispose();
                    }
                }
                catch
                {
                }
                this.DateCheckFlag = false;
            }
            if (!this.bSendFlag)
            {
                bool flag = false;
                if (base.sysenv.TerminalInfo.UserKind == 4)
                {
                    KioskLink link = new KioskLink(recvdata);
                    IData data5 = link.CreateLinkData();
                    if (data5 != null)
                    {
                        flag = true;
                        if (this.sendjobsender != null)
                        {
                            ((JobForm) this.sendjobsender).Close();
                        }
                        this.KioskLinkData = new _KioskLinkData();
                        this.KioskLinkData.Data = data5;
                        this.KioskLinkData.Data.Header.System = this.KioskLinkSystem;
                        this.KioskLinkData.File = link.LinkFile;
                        this.KioskLinkData.No = 1;
                        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer {
                            Interval = 10
                        };
                        timer.Tick += new EventHandler(this.KioskLink_Tick);
                        timer.Enabled = true;
                    }
                }
                if (!flag)
                {
                    base.JobFormOpen(int.Parse(str), data, this.sendjobsender);
                }
            }
            if (data4 != null)
            {
                ULogClass.LogWrite("INET_RECV_OTHER ", data4.GetDataString().Substring(0, 400), true);
                try
                {
                    string path = base.SaveFileCheck(data4);
                    if (path != "")
                    {
                        if (data4.Header.DataType == "A")
                        {
                            base.idv.AppendRecord(data4, Path.GetDirectoryName(path));
                        }
                        else
                        {
                            DataFactory.SaveToEdiFile(data4, path);
                            base.idv.SaveStatusChange(data4.ID, false);
                        }
                    }
                }
                catch
                {
                }
                data4 = base.ContainedSet(data4);
                base.pr.Print(this, data4.ID, data4, 2);
            }
            if (data3 != null)
            {
                ULogClass.LogWrite("INET_RECV_RESULT ", data3.GetDataString().Substring(0, 400), true);
                try
                {
                    string fileName = base.SaveFileCheck(data3);
                    if (fileName != "")
                    {
                        DataFactory.SaveToEdiFile(data3, fileName);
                        base.idv.SaveStatusChange(data3.ID, false);
                    }
                }
                catch
                {
                }
            }
            if (((data3 == null) && (data4 == null)) && (data != null))
            {
                ULogClass.LogWrite("INET_RECV_MDATA ", data.GetDataString().Substring(0, 400), true);
                try
                {
                    string str6 = base.SaveFileCheck(data);
                    if (str6 != "")
                    {
                        DataFactory.SaveToEdiFile(data, str6);
                        base.idv.SaveStatusChange(data.ID, false);
                    }
                }
                catch
                {
                }
                RecvData data6 = DataFactory.CreateRecvData(data);
                data6.OtherData = base.ContainedSet(data6.OtherData);
                base.pr.Print(this, data.ID, data6.OtherData, 2);
            }
            if (data4 != null)
            {
                base.idv.Distribute(data4);
            }
            if (data3 != null)
            {
                base.idv.Distribute(data3);
            }
            if (((data3 == null) && (data4 == null)) && (data != null))
            {
                base.idv.Distribute(data);
            }
            base.idv.DataViewSaveRepaint();
            if (this.bSendFlag && !this.cancelflag)
            {
                ULogClass.LogWrite("INET_RECV_END NextSend", null, false);
                base.SendReportDlg.SetStatus(sendkey, USendReportDlg.ReportStatus.Sent, "");
                System.Windows.Forms.Timer timer2 = new System.Windows.Forms.Timer {
                    Interval = 10
                };
                timer2.Tick += new EventHandler(this.tm_Tick);
                timer2.Enabled = true;
            }
            else
            {
                ULogClass.LogWrite("INET_RECV_END", null, false);
            }
        }

        private void Http_OnSent(object sender, string key)
        {
            base.StatusChange(3);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(InteractiveMain));
            this.folderBrowserDialog1 = new FolderBrowserDialog();
            this.uUserCodeI = new UUserCode();
            base.tlcMain.ContentPanel.SuspendLayout();
            base.tlcMain.SuspendLayout();
            base.pnlUserInput.SuspendLayout();
            base.pnlJobMenu.SuspendLayout();
            base.pnlJobInput.SuspendLayout();
            base.pnlSupportWindow.SuspendLayout();
            base.SuspendLayout();
            base.tlcMain.BottomToolStripPanelVisible = true;
            base.tlcMain.ContentPanel.Size = new Size(0x3f8, 0x24a);
            base.tlcMain.LeftToolStripPanelVisible = true;
            base.tlcMain.RightToolStripPanelVisible = true;
            base.tlcMain.Size = new Size(0x3f8, 0x2c4);
            base.tlcMain.TopToolStripPanelVisible = true;
            base.pnlUserInput.Controls.Add(this.uUserCodeI);
            base.pnlUserInput.Size = new Size(200, 0x67);
            base.pnlDataView.Size = new Size(0x32d, 0x24a);
            base.pnlJobMenu.Location = new Point(0, 0xcc);
            base.pnlJobMenu.Size = new Size(200, 0x17e);
            base.pnlJobInput.Location = new Point(0, 0x67);
            base.pnlSupportWindow.Size = new Size(200, 0x24a);
            base.uJobMenu1.Size = new Size(200, 0x17e);
            this.uUserCodeI.AutoSize = true;
            this.uUserCodeI.Dock = DockStyle.Fill;
            this.uUserCodeI.Location = new Point(0, 0);
            this.uUserCodeI.MailBoxCheck = false;
            this.uUserCodeI.MailBoxID = null;
            this.uUserCodeI.MailBoxPassword = null;
            this.uUserCodeI.Name = "uUserCodeI";
            this.uUserCodeI.Password = "";
            this.uUserCodeI.Size = new Size(200, 0x67);
            this.uUserCodeI.TabIndex = 0;
            this.uUserCodeI.UpperCheck = false;
            this.uUserCodeI.UserCode = "";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x3f8, 0x2de);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "InteractiveMain";
            base.Load += new EventHandler(this.InteractiveMain_Load);
            base.tlcMain.ContentPanel.ResumeLayout(false);
            base.tlcMain.ResumeLayout(false);
            base.tlcMain.PerformLayout();
            base.pnlUserInput.ResumeLayout(false);
            base.pnlUserInput.PerformLayout();
            base.pnlJobMenu.ResumeLayout(false);
            base.pnlJobInput.ResumeLayout(false);
            base.pnlJobInput.PerformLayout();
            base.pnlSupportWindow.ResumeLayout(false);
            base.pnlSupportWindow.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void Interactive_DeleteList(bool ListUpdateFlg, ListType DelList, string DelKey)
        {
            if (DelList == ListType.UserList)
            {
                base.HUserList.Remove(DelKey);
            }
            else
            {
                base.HMailBoxList.Remove(DelKey);
            }
            if (ListUpdateFlg)
            {
                base.iuc.HistoryUserListSet(base.HUserList, base.HMailBoxList);
            }
        }

        private void InteractiveMain_Load(object sender, EventArgs e)
        {
            if (!base.isDesigning)
            {
                base.iuc = this.uUserCodeI;
                base.iuc.OnButton += new OnButtonHandler(this.UserCode_OnButton);
                base.iuc.OnDeleteList += new OnDeleteEventHandler(this.Interactive_DeleteList);
                base.iuc.UserCode = base.UserCode;
                base.iuc.Password = base.UserPassword;
                base.iuc.HistoryUserListSet(base.HUserList);
                this.ihc.OnSent += new SentEventHandler(this.Http_OnSent);
                this.ihc.OnReceived += new ReceivedEventHandler(this.Http_OnReceived);
                this.ihc.OnNetworkError += new NetworkErrorEventHandler(this.Http_OnError);
                this.ato = new AtOnceData(this, base.DemoFlag, 2);
                this.ato.OnReceived += new AtonceReceivedHandler(this.AtOnce_OnReceived);
                this.ato.OnNetworkError += new AtonceNetworkErrorHandler(this.AtOnce_OnNetworkError);
                this.acm = new AccumulateData(this, base.DemoFlag, 2);
                this.acm.OnReceived += new AccumulateReceivedHandler(this.Accumulate_OnReceived);
                this.acm.OnNetworkError += new AccumulateNetworkErrorHandler(this.Accumulate_OnNetworkError);
                this.logon_ato = new AtOnceData(this, base.DemoFlag, 2);
                this.logon_ato.OnReceived += new AtonceReceivedHandler(this.Logon_AtOnce_OnReceived);
                this.logon_ato.OnNetworkError += new AtonceNetworkErrorHandler(this.Logon_AtOnce_OnNetworkError);
                base.SendReportDlg = new USendReportDlg();
                base.BatchSendFlag = false;
                this.JobConnectSet();
                this.DateCheckFlag = true;
            }
        }

        public override void Job_OnRepeatSend(object sender)
        {
            this.FileSend(sender);
        }

        public override string Job_OnSend(object sender, IData Data)
        {
            this.KioskLinkSystem = Data.Header.System;
            this.cancelflag = false;
            if (base.ComStatus == 0)
            {
                if (!this.LogonCheck())
                {
                    return Data.ID;
                }
                if (sender != null)
                {
                    ((CommonJobForm) sender).Enabled = false;
                }
                this.LogonSendFlag = true;
                this.Logonsender = sender;
                this.LogonData = Data;
                if (!this.LogonStart())
                {
                    this.LogonSendFlag = false;
                    return Data.ID;
                }
            }
            else if (Data.Signflg == 1)
            {
                MessageDialog dialog = new MessageDialog();
                if (dialog.ShowMessage("I105", "") == DialogResult.Yes)
                {
                    this.OnSend(sender, Data);
                }
                else
                {
                    base.idv.AppendJobData(Data, 0, true, false, false);
                }
            }
            else
            {
                this.OnSend(sender, Data);
            }
            return Data.ID;
        }

        public override void JobConnectSet()
        {
            if (base.SendRecvTimer > 0)
            {
                base.tmrJobConnect.Interval = (base.SendRecvTimer * 0x3e8) * 60;
            }
            base.tmrJobConnect.Enabled = base.SendRecvTimerFlg;
        }

        private void KioskLink_Tick(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = (System.Windows.Forms.Timer) sender;
            timer.Enabled = false;
            timer.Dispose();
            base.JobFormOpen(this.KioskLinkData.Data, this.KioskLinkData.File, this.KioskLinkData.No);
        }

        private void LogoffStart()
        {
            base.TimerStop();
            if (base.mnLogoffConf.Checked)
            {
                MessageDialog dialog = new MessageDialog();
                if (dialog.ShowMessage("C101", "") != DialogResult.Yes)
                {
                    dialog.Dispose();
                    base.TimerStart();
                    return;
                }
                dialog.Dispose();
            }
            this.ato.NextStop();
            this.acm.NextStop();
            base.UserCode = "";
            base.UserPassword = "";
            base.iuc.Password = "";
            this.CA = null;
            base.StatusChange(0);
            base.UserCodeJobSet("");
        }

        public void Logon_AtOnce_OnNetworkError(int senderror, HttpExceptionEx ex)
        {
            try
            {
                string str;
                HttpErrorDlg dlg = new HttpErrorDlg();
                base.stbRepStatus.Text = "";
                dlg.ShowHttpError(senderror, ex);
                if (ex == null)
                {
                    str = System.Enum.GetNames(typeof(HttpResult))[senderror].ToString();
                }
                else
                {
                    str = System.Enum.GetNames(typeof(HttpCause))[(int) ex.Cause].ToString();
                }
                ULogClass.LogWrite("Logon_AtOnce_NetworkError Status=" + str);
                dlg.Dispose();
                base.UserCode = "";
                base.UserPassword = "";
                base.StatusChange(0);
                base.UserCodeJobSet("");
            }
            finally
            {
                base.Enabled = true;
                if (this.Logonsender != null)
                {
                    ((CommonJobForm) this.Logonsender).Enabled = true;
                }
                this.SendRecvDlgHide();
                this.LogonSendFlag = false;
            }
        }

        public bool Logon_AtOnce_OnReceived(ComParameter recvdata)
        {
            bool flag;
            try
            {
                if (recvdata.DataString.Length < 400)
                {
                    base.stbRepStatus.Text = "";
                    this.LogonEnd();
                    if (this.LogonSendFlag)
                    {
                        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer {
                            Interval = 10
                        };
                        timer.Tick += new EventHandler(this.tm_TickSend);
                        timer.Enabled = true;
                    }
                    return true;
                }
                base.UserCode = "";
                base.UserPassword = "";
                base.StatusChange(0);
                base.UserCodeJobSet("");
                flag = false;
            }
            finally
            {
                base.Enabled = true;
                if (this.Logonsender != null)
                {
                    ((CommonJobForm) this.Logonsender).Enabled = true;
                }
                this.SendRecvDlgHide();
                this.LogonSendFlag = false;
            }
            return flag;
        }

        private bool LogonCheck()
        {
            if (!this.ato.IsExcute && !this.acm.IsExcute)
            {
                return true;
            }
            MessageDialog dialog = new MessageDialog();
            dialog.ShowMessage("W123", "", "");
            dialog.Dispose();
            return false;
        }

        private void LogonEnd()
        {
            base.StatusChange(1);
            base.HUserList = base.HistoryListAdd(base.UserCode, base.HUserList, 30);
            base.iuc.HistoryUserListSet(base.HUserList);
            base.iuc.Password = base.UserPassword;
            base.UserCodeJobSet(base.UserCode);
            base.TimerStart();
        }

        private bool LogonStart()
        {
            IInteractiveUserCode code;
            if (base.sysenv.TerminalInfo.UserKind == 4)
            {
                code = new KioskUserCode();
            }
            else
            {
                code = new UUserCodeDlg();
                ((IInteractiveUserCodeEx) code).OnDeleteList += new OnDeleteEventHandler(this.Interactive_DeleteList);
            }
            code.VisibleCheck(0);
            code.HistoryUserListSet(base.HUserList);
            if (code.ShowDialog() == DialogResult.OK)
            {
                this.SendRecvDlgShow(0);
                base.Enabled = false;
                base.UserCode = code.UserCode;
                base.UserPassword = code.Password;
                code.Close();
                code.Dispose();
            }
            else
            {
                if (this.Logonsender != null)
                {
                    ((CommonJobForm) this.Logonsender).Enabled = true;
                }
                code.Close();
                code.Dispose();
                return false;
            }
            if (!this.logon_ato.SendLogon(this.logon_ihc, base.UserCode, base.UserPassword))
            {
                base.Enabled = true;
                this.SendRecvDlgHide();
                MessageDialog dialog = new MessageDialog();
                dialog.ShowMessage("E122", "", "");
                dialog.Dispose();
            }
            return true;
        }

        public override void mnBatchSend_Click(object sender, EventArgs e)
        {
            this.cancelflag = false;
            bool flg = true;
            if (base.idv.sExistenceCheck())
            {
                MessageDialog dialog = new MessageDialog();
                if (dialog.ShowMessage("I105", "") == DialogResult.Yes)
                {
                    this.SendAllCount = base.idv.GetCount(1);
                    flg = true;
                }
                else
                {
                    this.SendAllCount = base.idv.GetCount(5);
                    flg = false;
                }
            }
            else
            {
                this.SendAllCount = base.idv.GetCount(1);
                flg = true;
            }
            if (this.SendAllCount < 1)
            {
                MessageDialog dialog2 = new MessageDialog();
                dialog2.ShowMessage("W401", "");
                dialog2.Close();
                dialog2.Dispose();
            }
            else
            {
                this.bSendFlag = true;
                this.SendCount = 0;
                this.BatchSend(flg);
            }
        }

        public override void mnBatchTake_Click(object sender, EventArgs e)
        {
            if (base.mnListRestriction.Checked)
            {
                base.idv.DataViewNoRepaint(true);
            }
            Naccs.Core.BatchDoc.BatchDoc doc = new Naccs.Core.BatchDoc.BatchDoc(this, base.DemoFlag, 2);
            doc.OnReceived += new BatchDocReceivedHandler(this.BatchDoc_OnReceived);
            doc.ShowDialog(this.bat_ihc, base.UserCode, base.UserPassword, false);
            doc.Close();
            doc.Dispose();
            if (base.mnListRestriction.Checked)
            {
                base.idv.DataViewNoRepaint(false);
            }
        }

        public override void mnBatchTakeAgain_Click(object sender, EventArgs e)
        {
            if (base.mnListRestriction.Checked)
            {
                base.idv.DataViewNoRepaint(true);
            }
            Naccs.Core.BatchDoc.BatchDoc doc = new Naccs.Core.BatchDoc.BatchDoc(this, base.DemoFlag, 2);
            doc.OnReceived += new BatchDocReceivedHandler(this.BatchDoc_OnReceived);
            doc.ShowDialog(this.bat_ihc, base.UserCode, base.UserPassword, true);
            doc.Close();
            doc.Dispose();
            if (base.mnListRestriction.Checked)
            {
                base.idv.DataViewNoRepaint(false);
            }
        }

        public override void mnDISYG_Click(object sender, EventArgs e)
        {
            if (!this.acm.SendSYG(this.acm_ihc, base.UserCode, base.UserPassword))
            {
                string applyStr = "";
                if (this.acm.IsREQ)
                {
                    applyStr = "蓄積";
                }
                else
                {
                    applyStr = "障害";
                }
                base.stbReqStatus.Text = "";
                MessageDialog dialog = new MessageDialog();
                dialog.ShowMessage("W122", applyStr, "");
                dialog.Dispose();
            }
        }

        public override void mnFileSend_Click(object sender, EventArgs e)
        {
            this.FileSend(null);
        }

        public override void mnLogoff_Click(object sender, EventArgs e)
        {
            this.LogoffStart();
        }

        public override void mnLogon_Click(object sender, EventArgs e)
        {
            if (this.LogonCheck())
            {
                this.LogonStart();
            }
        }

        public override void mnOtherTerm_Click(object sender, EventArgs e)
        {
            base.mnOtherTerm.Enabled = false;
            try
            {
                if ((base.ComStatus > 0) && !this.TTMSend())
                {
                    string applyStr = "";
                    if (this.ato.IsREP1)
                    {
                        applyStr = "Instant";
                    }
                    else
                    {
                        applyStr = "other terminal";
                    }
                    MessageDialog dialog = new MessageDialog();
                    dialog.ShowMessage("W122", applyStr, "");
                    dialog.Dispose();
                }
            }
            finally
            {
                base.mnOtherTerm.Enabled = true;
            }
        }

        public override void mnRep_Click(object sender, EventArgs e)
        {
            if ((base.ComStatus > 0) && !this.RepSend(0))
            {
                string applyStr = "";
                if (this.ato.IsREP1)
                {
                    applyStr = "Instant";
                }
                else
                {
                    applyStr = "other terminal";
                }
                MessageDialog dialog = new MessageDialog();
                dialog.ShowMessage("W122", applyStr, "");
                dialog.Dispose();
            }
        }

        public override void mnREQ_Click(object sender, EventArgs e)
        {
            if (!this.acm.SendREQ(this.acm_ihc, base.UserCode, base.UserPassword))
            {
                string applyStr = "";
                if (this.acm.IsREQ)
                {
                    applyStr = "accumulated";
                }
                else
                {
                    applyStr = "Instant";
                }
                base.stbReqStatus.Text = "";
                MessageDialog dialog = new MessageDialog();
                dialog.ShowMessage("W122", applyStr, "");
                dialog.Dispose();
            }
        }

        private void OnSend(object sender, IData Data)
        {
            if (base.ComStatus == 1)
            {
                this.sendjobsender = sender;
                if (Data.Signflg == 1)
                {
                    if (this.CA == null)
                    {
                        this.CA = new SignaturesCert();
                    }
                    try
                    {
                        IntPtr handle;
                        if (sender == null)
                        {
                            handle = base.Handle;
                        }
                        else
                        {
                            handle = ((Form) sender).Handle;
                        }
                        this.CA.GetSigneture(handle);
                    }
                    catch (Exception exception)
                    {
                        new MessageDialog().ShowMessage("E124", MessageDialog.CreateExceptionMessage(exception));
                        base.StatusChange(1);
                        base.Enabled = true;
                        this.SendRecvDlgHide();
                        return;
                    }
                }
                this.SendRecvDlgShow(1);
                if (this.bSendFlag)
                {
                    base.frmSendRecv.lblSendJobCode.Text = Data.JobCode + " " + this.SendCount.ToString() + "/" + this.SendAllCount.ToString();
                }
                else
                {
                    base.frmSendRecv.lblSendJobCode.Text = Data.JobCode;
                }
                base.Enabled = false;
                ComParameter senddata = new ComParameter();
                Data = base.SendDataCreate(Data, 0);
                senddata.DataString = Data.GetDataString();
                senddata.AttachFolder = Data.AttachFolder;
                if (Data.Signflg == 1)
                {
                    senddata.SelectSign = this.CA.Cert;
                    senddata.Sign = Data.Signflg.ToString();
                }
                base.StatusChange(2);
                string iD = Data.ID;
                if (!this.bSendFlag)
                {
                    if (Data.ID == null)
                    {
                        iD = base.idv.AppendJobData(Data, 0, false, false, false);
                    }
                    else
                    {
                        iD = base.idv.UpdateJobData(Data);
                    }
                }
                else
                {
                    iD = base.idv.UpdateJobData(Data);
                }
                if (base.sysenv.TerminalInfo.Debug)
                {
                    ULogClass.LogWrite("INET_SEND ", Data.GetDataString(), false);
                }
                else
                {
                    ULogClass.LogWrite("INET_SEND ", Data.GetDataString().Substring(0, 400), true);
                }
                this.ConnectFlag = true;
                int errcode = this.ihc.Send(this, iD, senddata);
                if (errcode > 0)
                {
                    HttpErrorDlg dlg = new HttpErrorDlg();
                    base.StatusChange(1);
                    base.Enabled = true;
                    this.SendRecvDlgHide();
                    dlg.ShowSendError(errcode);
                    ULogClass.LogWrite("SendError Status=" + System.Enum.GetNames(typeof(HttpResult))[errcode].ToString());
                    dlg.Dispose();
                }
                else if (errcode < 0)
                {
                    base.StatusChange(1);
                    base.Enabled = true;
                    this.SendRecvDlgHide();
                    ULogClass.LogWrite("User Cancel StatusCode = " + errcode.ToString());
                    if (this.bSendFlag)
                    {
                        if (base.BatchSendFlag)
                        {
                            base.BatchSendFlag = false;
                            MessageDialog dialog2 = new MessageDialog();
                            if (dialog2.ShowMessage("I104", base.SendReportDlg.StatusCount(USendReportDlg.ReportStatus.Sent).ToString(), "") == DialogResult.Yes)
                            {
                                base.SendReportDlg.Show();
                            }
                            dialog2.Dispose();
                        }
                        this.bSendFlag = false;
                    }
                }
            }
        }

        private bool RepSend(int flg)
        {
            if (!this.ato.IsExcute)
            {
                base.stbRepStatus.Text = "RECEIVING";
                this.ato.SendREP(this.ato_ihc, flg, base.UserCode, base.UserPassword);
                return true;
            }
            return false;
        }

        private void SendRecv_OnAbort()
        {
            if (this.ConnectFlag)
            {
                this.ihc.Abort();
                base.Enabled = true;
                this.SendRecvDlgHide();
                base.StatusChange(1);
                base.UpdateMenuItems();
                this.ConnectFlag = false;
            }
            else
            {
                this.cancelflag = true;
            }
        }

        private void SendRecvDlgHide()
        {
            if (base.frmSendRecv != null)
            {
                base.frmSendRecv.Close();
                base.frmSendRecv.Dispose();
                base.frmSendRecv = null;
            }
        }

        private void SendRecvDlgShow(int StatusFlag)
        {
            if (base.frmSendRecv == null)
            {
                base.frmSendRecv = new USendRecvDlg();
                base.frmSendRecv.OnAbort += new OnAbortHandler(this.SendRecv_OnAbort);
                if (StatusFlag == 0)
                {
                    base.frmSendRecv.btnCancel.Enabled = false;
                }
                base.frmSendRecv.lblSendJobCode.Text = "";
                base.frmSendRecv.Show();
                Application.DoEvents();
            }
        }

        private void tm_Tick(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = (System.Windows.Forms.Timer) sender;
            timer.Enabled = false;
            timer.Dispose();
            this.BatchSend(this.SignSendFlg);
        }

        private void tm_TickSend(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = (System.Windows.Forms.Timer) sender;
            timer.Enabled = false;
            timer.Dispose();
            this.OnSend(this.Logonsender, this.LogonData);
        }

        public override void tmrJobConnect_Tick(object sender, EventArgs e)
        {
            if (base.ComStatus > 0)
            {
                this.RepSend(1);
            }
        }

        private bool TTMSend()
        {
            if (!this.ato.IsExcute)
            {
                base.stbRepStatus.Text = "RECEIVING";
                this.ato.SendTTM(this.ato_ihc, base.UserCode, base.UserPassword);
                return true;
            }
            return false;
        }

        public override void UpdateMenuItemsProc()
        {
            base.tbbJSplitter2.Visible = false;
            base.tbbSend.Enabled = false;
            base.tbbSend.Visible = false;
            base.tbbMailRecv.Enabled = false;
            base.tbbMailRecv.Visible = false;
            base.tbbMailSendRecv.Enabled = false;
            base.tbbMailSendRecv.Visible = false;
            this.uUserCodeI.ChangeEnabled(base.mnLogon.Enabled);
        }

        private void UserCode_OnButton(bool Flag)
        {
            if (Flag)
            {
                if (this.LogonCheck())
                {
                    base.UserCode = base.iuc.UserCode;
                    base.UserPassword = base.iuc.Password;
                    this.SendRecvDlgShow(0);
                    base.Enabled = false;
                    if (!this.logon_ato.SendLogon(this.logon_ihc, base.UserCode, base.UserPassword))
                    {
                        base.Enabled = true;
                        this.SendRecvDlgHide();
                        MessageDialog dialog = new MessageDialog();
                        dialog.ShowMessage("E122", "", "");
                        dialog.Dispose();
                    }
                }
            }
            else
            {
                this.LogoffStart();
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct _KioskLinkData
        {
            public IData Data;
            public string File;
            public int No;
        }
    }
}

