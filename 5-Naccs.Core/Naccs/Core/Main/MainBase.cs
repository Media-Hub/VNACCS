namespace Naccs.Core.Main
{
    using Naccs.Common.JobConfig;
    using Naccs.Core.Classes;
    using Naccs.Core.DataView;
    using Naccs.Core.DataView.Arrange;
    using Naccs.Core.Job;
    using Naccs.Core.Option;
    using Naccs.Core.Print;
    using Naccs.Core.Properties;
    using Naccs.Core.Settings;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Deployment.Application;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Media;
    using System.Runtime.InteropServices;
    using System.Security.AccessControl;
    using System.Security.Principal;
    using System.Threading;
    using System.Windows.Forms;

    public class MainBase : Form
    {
        public bool AutoSendRecvFlag;
        public int AutoSendRecvKind;
        public int AutoSendRecvTimer;
        public bool BatchSendFlag;
        protected bool bMailActionFlag;
        protected bool bOptionShowingFlag;
        private const string C_ACLExeName = "CustomizeACL.exe";
        private const int C_JOBHISTORYCOUNT = 30;
        private const string C_PTYPE = "P";
        private const string C_UTYPE = "U";
        private const string C_VERSIONUPEXE = @"Download\NaccsLauncher.exe";
        private IContainer components;
        public int ComStatus;
        private int DataInfoCnt;
        private const string DefStr = "\\/:*?<>|\"";
        public bool DemoFlag;
        protected bool DisposingFlag;
        private ArrangeManager DsArrangeManager;
        private DispCodeList dscl;
        private StringList FldList;
        public USendRecvDlg frmSendRecv;
        public FileSave fsc;
        private StringList HJobList;
        public StringList HMailBoxList;
        public StringList HUserList;
        private const string IDENTITY = "everyone";
        public IDataView idv;
        protected bool isDesigning;
        protected bool isProxyError;
        public IUserCode iuc;
        public bool JobConnect;
        public string MailBoxID;
        public string MailBoxPassword;
        private List<ToolStripMenuItem> MiList;
        private ToolStripMenuItem mnAllSelect;
        protected ToolStripMenuItem mnBatchSend;
        private ToolStripMenuItem mnClearDelete;
        private ToolStripMenuItem mnCloseOnAppend;
        private ToolStripMenuItem mnControlChange;
        private ToolStripMenuItem mnControlGS;
        private ToolStripMenuItem mnControlMS;
        private ToolStripMenuItem mnControlSS;
        private ToolStripMenuItem mnControlWS;
        private ToolStripMenuItem mnCreateFolder;
        private ToolStripMenuItem mnDataDelete;
        protected ToolStripMenuItem mnDataViewExport;
        private ToolStripMenuItem mnDataViewFontSize;
        protected ToolStripMenuItem mnDataViewImport;
        protected ToolStripMenuItem mnDataViewPrint;
        protected ToolStripMenuItem mnDataViewRepair;
        protected ToolStripMenuItem mnDataViewRestore;
        private ToolStripMenuItem mnDataViewWindow;
        private ToolStripMenuItem mnDebug;
        private ToolStripMenuItem mnDeleteFolder;
        private ToolStripMenuItem mnDistribute;
        private ToolStripMenuItem mnEdit;
        private ToolStripSeparator mnESplitter1;
        private ToolStripSeparator mnESplitter2;
        private ToolStripSeparator mnESplitter3;
        private ToolStripSeparator mnESplitter4;
        private ToolStripSeparator mnESplitter5;
        private ToolStripMenuItem mnExit;
        private ToolStripMenuItem mnFile;
        private ToolStripMenuItem mnFileDataViewAppend;
        private ToolStripMenuItem mnFileSave;
        protected ToolStripMenuItem mnFileSend;
        private ToolStripMenuItem mnFolder;
        private ToolStripMenuItem mnFontDefault;
        private ToolStripMenuItem mnFontLarge;
        private ToolStripMenuItem mnFontSmall;
        private ToolStripSeparator mnFSplitter1;
        private ToolStripSeparator mnFSplitter2;
        private ToolStripSeparator mnFSplitter3;
        private ToolStripMenuItem mnFunctionBar;
        private ToolStripMenuItem mnHardCopy;
        private ToolStripMenuItem mnHardCopyPreview;
        private ToolStripMenuItem mnHardCopyPrint;
        private ToolStripMenuItem mnHelp;
        private ToolStripSeparator mnHSplitter1;
        private ToolStripSeparator mnHSplitter2;
        private ToolStripMenuItem mnInputCheck;
        private ToolStripMenuItem mnJob;
        private ToolStripMenuItem mnJobInputWindow;
        private ToolStripMenuItem mnJobKey;
        private ToolStripMenuItem mnJobKey1;
        private ToolStripMenuItem mnJobKey10;
        private ToolStripMenuItem mnJobKey11;
        private ToolStripMenuItem mnJobKey12;
        private ToolStripMenuItem mnJobKey13;
        private ToolStripMenuItem mnJobKey14;
        private ToolStripMenuItem mnJobKey15;
        private ToolStripMenuItem mnJobKey16;
        private ToolStripMenuItem mnJobKey17;
        private ToolStripMenuItem mnJobKey18;
        private ToolStripMenuItem mnJobKey19;
        private ToolStripMenuItem mnJobKey2;
        private ToolStripMenuItem mnJobKey20;
        private ToolStripMenuItem mnJobKey21;
        private ToolStripMenuItem mnJobKey22;
        private ToolStripMenuItem mnJobKey23;
        private ToolStripMenuItem mnJobKey24;
        private ToolStripMenuItem mnJobKey3;
        private ToolStripMenuItem mnJobKey4;
        private ToolStripMenuItem mnJobKey5;
        private ToolStripMenuItem mnJobKey6;
        private ToolStripMenuItem mnJobKey7;
        private ToolStripMenuItem mnJobKey8;
        private ToolStripMenuItem mnJobKey9;
        private ToolStripMenuItem mnJobMenuWindow;
        private ToolStripMenuItem mnJobMessage;
        private ToolStripMenuItem mnJobNew;
        private ToolStripMenuItem mnJobOpen;
        private ToolStripSeparator mnJSplitter1;
        protected ToolStripSeparator mnJSplitter3;
        protected ToolStripSeparator mnJSplitter4;
        protected ToolStripMenuItem mnListRestriction;
        private ToolStripMenuItem mnLogoff;
        public ToolStripMenuItem mnLogoffConf;
        public ToolStripMenuItem mnLogon;
        private ToolStripMenuItem mnLogonWindow;
        public MenuStrip mnMainMenu;
        private ToolStripMenuItem mnOpen;
        private ToolStripMenuItem mnOption;
        private ToolStripSeparator mnOSplitter1;
        private ToolStripSeparator mnOSplitter2;
        private ToolStripSeparator mnOSplitter3;
        protected ToolStripMenuItem mnOther;
        protected ToolStripMenuItem mnOtherTerm;
        private ToolStripMenuItem mnOutFile;
        private ToolStripMenuItem mnPackageMessage;
        private ToolStripMenuItem mnPrintDataView;
        private ToolStripMenuItem mnPrintDataViewPreview;
        private ToolStripMenuItem mnPrintJobData;
        private ToolStripMenuItem mnPrintJobDataPreview;
        private ToolStripMenuItem mnQueryFieldClear;
        private ToolStripMenuItem mnRecvSearch;
        private ToolStripMenuItem mnRecvText;
        private ToolStripMenuItem mnRedo;
        private ToolStripMenuItem mnRenameFolder;
        protected ToolStripMenuItem mnRep;
        private ToolStripMenuItem mnSelectedDistribute;
        private ToolStripMenuItem mnSendSearch;
        private ToolStripMenuItem mnSupport;
        private ToolStripMenuItem mnSystem;
        private ToolStripMenuItem mnToolbar;
        private ToolStripMenuItem mnToolbarJob;
        private ToolStripMenuItem mnToolbarStandard;
        private ToolStripMenuItem mnUndo;
        private ToolStripMenuItem mnVersionInfo;
        public ToolStripMenuItem mnVersionUp;
        protected ToolStripMenuItem mnViewRefresh;
        private ToolStripMenuItem mnVisible;
        protected ToolStripMenuItem mnVisibleCount;
        private ToolStripSeparator mnVSplitter1;
        private ToolStripSeparator mnVSplitter2;
        private ToolStripMenuItem mnWebSiteLink;
        private ToolStripMenuItem mnWindow;
        private MessageClassify msgc;
        public OpenFileDialog ofdMain;
        public PathInfo pi;
        public Panel pnlDataView;
        protected Panel pnlJobInput;
        protected Panel pnlJobMenu;
        protected Panel pnlSupportWindow;
        public Panel pnlUserInput;
        public IPrint pr;
        protected string ProtocolName;
        public bool RepFlag;
        public bool ReqFlag;
        private frmReceiveNoticeDlg rnd;
        public int SendRecvTimer;
        public bool SendRecvTimerFlg;
        public USendReportDlg SendReportDlg;
        private int SeqInt;
        private List<string> SeqList;
        private Splitter splMain;
        private ToolStripStatusLabel stbInfomation;
        private ToolStripStatusLabel stbMailAdress;
        public ToolStripStatusLabel stbProcess;
        public ToolStripStatusLabel stbRepStatus;
        public ToolStripStatusLabel stbReqStatus;
        private ToolStripStatusLabel stbStatus;
        public ToolStripStatusLabel stbTerm;
        private ToolStripStatusLabel stbUserCode;
        private StatusStrip stsMain;
        private SWCheckTbl swtbl;
        public SystemEnvironment sysenv;
        private ToolStripButton tbbBatchTake;
        private ToolStripButton tbbF1;
        private ToolStripButton tbbF10;
        private ToolStripButton tbbF11;
        private ToolStripButton tbbF12;
        private ToolStripButton tbbF2;
        private ToolStripButton tbbF3;
        private ToolStripButton tbbF4;
        private ToolStripButton tbbF5;
        private ToolStripButton tbbF6;
        private ToolStripButton tbbF7;
        private ToolStripButton tbbF8;
        private ToolStripButton tbbF9;
        private ToolStripButton tbbFileOpen;
        private ToolStripButton tbbFileSaveAs;
        private ToolStripSeparator tbbFSplitter1;
        private ToolStripSeparator tbbFSplitter2;
        public ToolStripButton tbbGuidance;
        private ToolStripButton tbbJetras;
        private ToolStripButton tbbJobNew;
        private ToolStripSeparator tbbJSplitter1;
        public ToolStripSeparator tbbJSplitter2;
        private ToolStripSeparator tbbJSplitter3;
        public ToolStripSeparator tbbJSplitter4;
        public ToolStripSeparator tbbJSplitter5;
        private ToolStripButton tbbLogoff;
        private ToolStripButton tbbLogon;
        public ToolStripButton tbbMailRecv;
        public ToolStripButton tbbMailSendRecv;
        private ToolStripButton tbbOpen;
        private ToolStripButton tbbPrintJobData;
        private ToolStripButton tbbPrintJobDataPreview;
        protected ToolStripButton tbbREP;
        protected ToolStripButton tbbREQ;
        public ToolStripButton tbbSend;
        private ToolStripSeparator tbbSSplitter1;
        public ToolStripContainer tlcMain;
        public System.Windows.Forms.Timer tmrJobConnect;
        protected System.Windows.Forms.Timer tmrSendRecv;
        private ToolStrip tslFunction;
        public ToolStrip tslJob;
        public ToolStrip tslStandard;
        protected UJobInput uJobInput1;
        protected UJobMenu uJobMenu1;
        private UOpeningDlg uopd;
        public string UserCode;
        public string UserPassword;
        public User usr;
        public ApplicationClass usr_apc;
        public ColumnWidthClass usr_cwc;
        public DataViewFormClass usr_dvf;
        public JobOptionClass usr_jpc;
        public ToolBarClass usr_tlb;
        public Naccs.Core.Settings.WindowClass usr_wnd;
        public UserEnvironment usrenv;
        private TerminalInfoClass usrenv_term;
        protected bool VersionUpFlag;

        public MainBase() : this(false)
        {
        }

        public MainBase(bool proxyError)
        {
            this.MiList = new List<ToolStripMenuItem>();
            this.ProtocolName = "";
            this.isDesigning = (AppDomain.CurrentDomain.FriendlyName == "DefaultDomain") && !ApplicationDeployment.IsNetworkDeployed;
            this.InitializeComponent();
            if (!this.isDesigning)
            {
                this.uopd = new UOpeningDlg();
                this.uopd.TopMost = true;
                this.uopd.StartPosition = FormStartPosition.CenterScreen;
                this.uopd.Show();
                Application.DoEvents();
                this.pi = PathInfo.CreateInstance();
                this.UserPathAuth();
                this.AuthorityChange();
                this.isProxyError = proxyError;
                this.OptionCheck();
                new ResourceManager().ResourceCopy();
                new VersionuptoolUpdater().Excute(this.VersioupToolPath);
                this.uJobInput1.OnJobOpen += new JobOpenHandler(this.JobWindow_OnJobOpen);
                this.uJobMenu1.OnJobOpen += new JobOpenMenuHandler(this.JobWindow_OnJobOpen);
                this.pr = new PrintManager();
                this.pr.OnPrintEnd += new OnPrintEndHandler(this.Print_OnPrintEnd);
                this.pr.OnPrintError += new OnPrintErrorHandler(this.Print_OnPrintError);
                Application.ThreadException += new ThreadExceptionEventHandler(this.Application_ThreadException);
                this.mnOption.DropDownItems.Remove(this.mnListRestriction);
            }
        }

        private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageDialog dialog = new MessageDialog();
            string message = e.Exception.Message + "\n" + e.Exception.Source + "\n\n" + e.Exception.StackTrace;
            ULogClass.WriteProcessInfo(message);
            dialog.ShowMessage("E002", "", message);
            dialog.Dispose();
        }

        private void AuthorityChange()
        {
            try
            {
                string directoryName = Path.GetDirectoryName(Path.GetDirectoryName(Application.ExecutablePath));
                string path = Path.GetDirectoryName(Path.GetDirectoryName(this.pi.CommonPath));
                if (Directory.Exists(directoryName))
                {
                    this.DirectorySecurityChange(directoryName);
                }
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    this.DirectorySecurityChange(path);
                }
                else
                {
                    this.DirectorySecurityChange(path);
                }
            }
            catch
            {
            }
        }

        private void BackupFileDelete()
        {
            if (Directory.Exists(this.pi.BackupPath))
            {
                string s = DateTime.Now.AddDays((double) -this.usrenv.TerminalInfo.SaveTerm).ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
                string[] strArray = Directory.GetFiles(this.pi.BackupPath, "*.*", SearchOption.TopDirectoryOnly);
                DateTime time2 = DateTime.ParseExact(s, "yyyyMMdd", null);
                for (int i = 0; i < strArray.Length; i++)
                {
                    DateTime time3 = new DateTime();
                    try
                    {
                        time3 = DateTime.ParseExact(Path.GetFileNameWithoutExtension(strArray[i]), "yyyyMMdd", null);
                    }
                    catch
                    {
                        try
                        {
                            File.Delete(strArray[i]);
                        }
                        catch
                        {
                        }
                        continue;
                    }
                    if (time2 > time3)
                    {
                        try
                        {
                            File.Delete(strArray[i]);
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        public IData ContainedSet(IData data)
        {
            if (data.Status == DataStatus.Recept)
            {
                data.Contained = ContainedType.ReceiveOnly;
                return data;
            }
            data.Contained = ContainedType.SendOnly;
            return data;
        }

        protected virtual object CreateOption()
        {
            return new OptionDialog();
        }

        public void DataVeiw_OnPrintJob(int prtflag)
        {
            switch (prtflag)
            {
                case 0:
                    this.mnPrintJobData_Click(this, null);
                    return;

                case 1:
                    this.mnPrintJobDataPreview_Click(this, null);
                    return;
            }
        }

        public void DataView_OnJobFormOpen(int key, bool JobFormFlag)
        {
            if (JobFormFlag)
            {
                this.JobFormOpen(key, JobFormFlag);
            }
            else
            {
                this.JobFormOpen(key);
            }
        }

        public void DataView_OnUpdateDataViewItems(object sender, bool FldCreate, bool FldChange, bool FldDelete, bool DDelete, bool DUndo, bool SSearch, bool RSearch, bool Export)
        {
            this.mnCreateFolder.Enabled = FldCreate;
            this.mnRenameFolder.Enabled = FldChange;
            this.mnDeleteFolder.Enabled = FldDelete;
            this.mnDataDelete.Enabled = DDelete;
            this.mnUndo.Enabled = DUndo;
            this.mnSendSearch.Enabled = SSearch;
            this.mnRecvSearch.Enabled = RSearch;
            this.mnDataViewExport.Enabled = Export;
            this.UpdateMenuItems();
        }

        private void DataViewFontSize(int sizeflag)
        {
            switch (sizeflag)
            {
                case 0:
                    this.mnFontDefault.Checked = false;
                    this.mnFontSmall.Checked = true;
                    this.mnFontLarge.Checked = false;
                    break;

                case 1:
                    this.mnFontDefault.Checked = true;
                    this.mnFontSmall.Checked = false;
                    this.mnFontLarge.Checked = false;
                    break;

                case 2:
                    this.mnFontDefault.Checked = false;
                    this.mnFontSmall.Checked = false;
                    this.mnFontLarge.Checked = true;
                    break;
            }
            this.idv.DataViewFontChange(sizeflag);
        }

        private void DirectorySecurityChange(string dirPath)
        {
            DriveInfo info = new DriveInfo(Application.ExecutablePath);
            if (info.DriveType != DriveType.CDRom)
            {
                DirectoryInfo info2 = new DirectoryInfo(dirPath);
                try
                {
                    DirectorySecurity accessControl = info2.GetAccessControl();
                    foreach (FileSystemAccessRule rule in accessControl.GetAccessRules(true, true, typeof(NTAccount)))
                    {
                        if ("everyone".ToUpper() == rule.IdentityReference.ToString().ToUpper())
                        {
                            return;
                        }
                    }
                    accessControl.AddAccessRule(new FileSystemAccessRule("everyone", FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow));
                    info2.SetAccessControl(accessControl);
                }
                catch (Exception exception)
                {
                    using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                    {
                        form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, MessageDialog.CreateExceptionMessage(exception));
                    }
                }
            }
        }

        private void DiskCheck()
        {
            DriveInfo info = new DriveInfo(Application.ExecutablePath);
            if (info.DriveType != DriveType.CDRom)
            {
                char ch = Application.StartupPath[0];
                string driveName = ch.ToString().ToUpper();
                if (('A' <= driveName[0]) && (driveName[0] <= 'Z'))
                {
                    DriveInfo info2 = new DriveInfo(driveName);
                    long num = (info2.TotalFreeSpace / 0x400L) / 0x400L;
                    if (this.usrenv.TerminalInfo.DiskWarning > num)
                    {
                        MessageDialog dialog = new MessageDialog();
                        dialog.ShowMessage("W301", num.ToString(), "");
                        dialog.Close();
                        dialog.Dispose();
                        Environment.Exit(0);
                    }
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

        protected string ErrCreate(string str)
        {
            string message = Naccs.Core.Properties.Resources.ResourceManager.GetString("CORE37");
            using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
            {
                form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, message);
            }
            IData data = DataFactory.CreateInstance();
            data.Header.OutCode = "*ERROR";
            data.Header.DataType = "E";
            data.Header.Div = "001";
            data.Header.EndFlag = "E";
            data.Header.Subject = "LENGTH ERROR";
            data.Header.DataInfo = "ERROR" + DateTime.Now.ToString("yyyyMMddHHmmss", DateTimeFormatInfo.InvariantInfo);
            data.JobData = str;
            return data.GetDataString();
        }

        public string FileNameCheck(string FStr, string rps)
        {
            string str = FStr;
            for (int i = 0; i < "\\/:*?<>|\"".Length; i++)
            {
                while (str.IndexOf("\\/:*?<>|\""[i]) > -1)
                {
                    if (rps == "")
                    {
                        str = str.Remove(str.IndexOf("\\/:*?<>|\""[i]), 1);
                    }
                    else
                    {
                        str = str.Replace(str[str.IndexOf("\\/:*?<>|\""[i])], rps.ToCharArray()[0]);
                    }
                }
            }
            if (rps == "")
            {
                while (str.IndexOf(' ') > -1)
                {
                    str = str.Remove(str.IndexOf(' '), 1);
                }
                return str;
            }
            bool flag = false;
            str = str.Trim();
            for (int j = str.Length - 1; j > -1; j--)
            {
                if (str[j] == ' ')
                {
                    if (!flag)
                    {
                        flag = true;
                    }
                    else
                    {
                        str = str.Remove(j, 1);
                    }
                }
                else
                {
                    flag = false;
                }
            }
            return str;
        }

        private void Function_Click(object sender, EventArgs e)
        {
            int num = int.Parse(((ToolStripButton) sender).Tag.ToString());
            if (this.MiList[num] != null)
            {
                this.MiList[num].PerformClick();
            }
        }

        private void FunctionEnabledCheck()
        {
            if (this.MiList.Count >= 12)
            {
                for (int i = 0; i < this.tslFunction.Items.Count; i++)
                {
                    if (((this.tslFunction.Items[i].Tag != null) && (int.Parse(this.tslFunction.Items[i].Tag.ToString()) >= 0)) && (int.Parse(this.tslFunction.Items[i].Tag.ToString()) <= 11))
                    {
                        if (this.MiList[int.Parse(this.tslFunction.Items[i].Tag.ToString())] != null)
                        {
                            this.tslFunction.Items[i].Enabled = this.MiList[int.Parse(this.tslFunction.Items[i].Tag.ToString())].Enabled;
                        }
                        else
                        {
                            this.tslFunction.Items[i].Enabled = false;
                        }
                    }
                }
            }
        }

        private void FunctionMenuSet(ToolStripMenuItem ts, int Flag)
        {
            for (int i = 0; i < ts.DropDownItems.Count; i++)
            {
                if (ts.DropDownItems[i].GetType() == typeof(ToolStripMenuItem))
                {
                    ToolStripMenuItem tsmi = (ToolStripMenuItem) ts.DropDownItems[i];
                    this.ShortCutCheck(tsmi, Flag);
                }
                if ((ts.DropDownItems[i].GetType() == typeof(ToolStripMenuItem)) && (((ToolStripMenuItem) ts.DropDownItems[i]).DropDownItems.Count > 0))
                {
                    this.FunctionMenuSet((ToolStripMenuItem) ts.DropDownItems[i], Flag);
                }
            }
        }

        private void FunctionSet(int Flag)
        {
            for (int i = 0; i < this.tslFunction.Items.Count; i++)
            {
                this.tslFunction.Items[i].Text = "          ";
            }
            this.MiList = new List<ToolStripMenuItem>();
            for (int j = 0; j < 12; j++)
            {
                this.MiList.Add(null);
            }
            for (int k = 0; k < this.mnMainMenu.Items.Count; k++)
            {
                if (this.mnMainMenu.Items[k].GetType() == typeof(ToolStripMenuItem))
                {
                    ToolStripMenuItem ts = (ToolStripMenuItem) this.mnMainMenu.Items[k];
                    if (ts.DropDownItems.Count > 0)
                    {
                        this.FunctionMenuSet(ts, Flag);
                    }
                }
            }
            this.FunctionEnabledCheck();
        }

        private string GetFunctionName(ToolStripMenuItem ts)
        {
            string text;
            if (ts.Tag != null)
            {
                text = ts.Tag.ToString();
                if (text.IndexOf(',') > -1)
                {
                    return text.Remove(0, text.IndexOf(',') + 1);
                }
            }
            text = ts.Text;
            if (text.IndexOf('(') > -1)
            {
                text = text.Remove(text.IndexOf('('));
            }
            return text;
        }

        protected string GetInitialDirectory()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string sendUser = FileSave.CreateInstance().SendFile.SendUser;
            if (sendUser != null)
            {
                sendUser = sendUser.TrimEnd(new char[] { '\\' });
                if (Directory.Exists(sendUser))
                {
                    return sendUser;
                }
            }
            return folderPath;
        }

        private void GetSWCode(string Code, out string JCode, out string OCode, int dtflag, string DataInf)
        {
            int num2;
            JCode = "";
            OCode = "";
            switch (dtflag)
            {
                case 0:
                    for (int i = 0; i < this.swtbl.SWList.Count; i++)
                    {
                        if (this.swtbl.SWList[i].ROutCode == Code)
                        {
                            if (this.idv.SWResCheck(this.swtbl.SWList[i].OOutCode, DataInf))
                            {
                                JCode = this.swtbl.SWList[i].JobCode;
                                OCode = this.swtbl.SWList[i].OOutCode;
                                return;
                            }
                            if ((JCode == "") && (OCode == ""))
                            {
                                JCode = this.swtbl.SWList[i].JobCode;
                                OCode = this.swtbl.SWList[i].OOutCode;
                            }
                        }
                    }
                    return;

                case 1:
                    num2 = 0;
                    break;

                default:
                    return;
            }
            while (num2 < this.swtbl.SWList.Count)
            {
                string str = Code;
                string oOutCode = this.swtbl.SWList[num2].OOutCode;
                if (str.Length > 6)
                {
                    str = str.Remove(6);
                }
                if (oOutCode.Length > 6)
                {
                    oOutCode = oOutCode.Remove(6);
                }
                if (oOutCode == str)
                {
                    JCode = this.swtbl.SWList[num2].JobCode;
                    OCode = this.swtbl.SWList[num2].ROutCode;
                    return;
                }
                num2++;
            }
        }

        private string GetSWJobCode(string OutCode, int Flag)
        {
            switch (Flag)
            {
                case 0:
                    for (int i = 0; i < this.swtbl.SWList.Count; i++)
                    {
                        if (this.swtbl.SWList[i].ROutCode == OutCode)
                        {
                            return this.swtbl.SWList[i].JobCode;
                        }
                    }
                    break;

                case 1:
                    for (int j = 0; j < this.swtbl.SWList.Count; j++)
                    {
                        if (this.swtbl.SWList[j].OOutCode == OutCode)
                        {
                            return this.swtbl.SWList[j].JobCode;
                        }
                    }
                    break;
            }
            return "";
        }

        public StringList HistoryListAdd(string str, StringList stl, int MaxInt)
        {
            string str2 = str;
            if (str2.IndexOf(" ") > -1)
            {
                str2 = str2.Remove(str2.IndexOf(" "));
            }
            for (int i = 0; i < stl.Count; i++)
            {
                string str3 = stl[i];
                if (str3.IndexOf(" ") > -1)
                {
                    str3 = str3.Remove(str3.IndexOf(" "));
                }
                if (str3 == str2)
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

        private void HtmlShow(string pathstr)
        {
            if (File.Exists(pathstr))
            {
                Process.Start(pathstr);
            }
            else
            {
                MessageDialog dialog = new MessageDialog();
                dialog.ShowMessage("W302", "");
                dialog.Close();
                dialog.Dispose();
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(MainBase));
            this.tlcMain = new ToolStripContainer();
            this.tslFunction = new ToolStrip();
            this.tbbF1 = new ToolStripButton();
            this.tbbF2 = new ToolStripButton();
            this.tbbF3 = new ToolStripButton();
            this.tbbF4 = new ToolStripButton();
            this.tbbFSplitter1 = new ToolStripSeparator();
            this.tbbF5 = new ToolStripButton();
            this.tbbF6 = new ToolStripButton();
            this.tbbF7 = new ToolStripButton();
            this.tbbF8 = new ToolStripButton();
            this.tbbFSplitter2 = new ToolStripSeparator();
            this.tbbF9 = new ToolStripButton();
            this.tbbF10 = new ToolStripButton();
            this.tbbF11 = new ToolStripButton();
            this.tbbF12 = new ToolStripButton();
            this.stsMain = new StatusStrip();
            this.stbStatus = new ToolStripStatusLabel();
            this.stbUserCode = new ToolStripStatusLabel();
            this.stbMailAdress = new ToolStripStatusLabel();
            this.stbRepStatus = new ToolStripStatusLabel();
            this.stbReqStatus = new ToolStripStatusLabel();
            this.stbInfomation = new ToolStripStatusLabel();
            this.stbProcess = new ToolStripStatusLabel();
            this.stbTerm = new ToolStripStatusLabel();
            this.pnlDataView = new Panel();
            this.splMain = new Splitter();
            this.pnlSupportWindow = new Panel();
            this.pnlJobMenu = new Panel();
            this.pnlJobInput = new Panel();
            this.pnlUserInput = new Panel();
            this.tslStandard = new ToolStrip();
            this.tbbJobNew = new ToolStripButton();
            this.tbbPrintJobData = new ToolStripButton();
            this.tbbPrintJobDataPreview = new ToolStripButton();
            this.tbbOpen = new ToolStripButton();
            this.tbbSSplitter1 = new ToolStripSeparator();
            this.tbbFileOpen = new ToolStripButton();
            this.tbbFileSaveAs = new ToolStripButton();
            this.tslJob = new ToolStrip();
            this.tbbLogon = new ToolStripButton();
            this.tbbLogoff = new ToolStripButton();
            this.tbbJSplitter1 = new ToolStripSeparator();
            this.tbbSend = new ToolStripButton();
            this.tbbMailRecv = new ToolStripButton();
            this.tbbMailSendRecv = new ToolStripButton();
            this.tbbJSplitter2 = new ToolStripSeparator();
            this.tbbREP = new ToolStripButton();
            this.tbbREQ = new ToolStripButton();
            this.tbbJSplitter3 = new ToolStripSeparator();
            this.tbbBatchTake = new ToolStripButton();
            this.tbbJSplitter4 = new ToolStripSeparator();
            this.tbbGuidance = new ToolStripButton();
            this.tbbJSplitter5 = new ToolStripSeparator();
            this.tbbJetras = new ToolStripButton();
            this.mnMainMenu = new MenuStrip();
            this.mnFile = new ToolStripMenuItem();
            this.mnJobNew = new ToolStripMenuItem();
            this.mnOpen = new ToolStripMenuItem();
            this.mnJobOpen = new ToolStripMenuItem();
            this.mnRedo = new ToolStripMenuItem();
            this.mnFSplitter1 = new ToolStripSeparator();
            this.mnOutFile = new ToolStripMenuItem();
            this.mnFileSave = new ToolStripMenuItem();
            this.mnFileSend = new ToolStripMenuItem();
            this.mnFileDataViewAppend = new ToolStripMenuItem();
            this.mnFSplitter2 = new ToolStripSeparator();
            this.mnPrintJobData = new ToolStripMenuItem();
            this.mnPrintJobDataPreview = new ToolStripMenuItem();
            this.mnHardCopy = new ToolStripMenuItem();
            this.mnHardCopyPrint = new ToolStripMenuItem();
            this.mnHardCopyPreview = new ToolStripMenuItem();
            this.mnDataViewPrint = new ToolStripMenuItem();
            this.mnPrintDataView = new ToolStripMenuItem();
            this.mnPrintDataViewPreview = new ToolStripMenuItem();
            this.mnFSplitter3 = new ToolStripSeparator();
            this.mnExit = new ToolStripMenuItem();
            this.mnEdit = new ToolStripMenuItem();
            this.mnUndo = new ToolStripMenuItem();
            this.mnDataDelete = new ToolStripMenuItem();
            this.mnESplitter1 = new ToolStripSeparator();
            this.mnAllSelect = new ToolStripMenuItem();
            this.mnESplitter2 = new ToolStripSeparator();
            this.mnClearDelete = new ToolStripMenuItem();
            this.mnESplitter3 = new ToolStripSeparator();
            this.mnFolder = new ToolStripMenuItem();
            this.mnCreateFolder = new ToolStripMenuItem();
            this.mnRenameFolder = new ToolStripMenuItem();
            this.mnDeleteFolder = new ToolStripMenuItem();
            this.mnESplitter4 = new ToolStripSeparator();
            this.mnDistribute = new ToolStripMenuItem();
            this.mnSelectedDistribute = new ToolStripMenuItem();
            this.mnESplitter5 = new ToolStripSeparator();
            this.mnSendSearch = new ToolStripMenuItem();
            this.mnRecvSearch = new ToolStripMenuItem();
            this.mnVisible = new ToolStripMenuItem();
            this.mnWindow = new ToolStripMenuItem();
            this.mnLogonWindow = new ToolStripMenuItem();
            this.mnJobInputWindow = new ToolStripMenuItem();
            this.mnJobMenuWindow = new ToolStripMenuItem();
            this.mnDataViewWindow = new ToolStripMenuItem();
            this.mnToolbar = new ToolStripMenuItem();
            this.mnToolbarStandard = new ToolStripMenuItem();
            this.mnToolbarJob = new ToolStripMenuItem();
            this.mnFunctionBar = new ToolStripMenuItem();
            this.mnVSplitter1 = new ToolStripSeparator();
            this.mnViewRefresh = new ToolStripMenuItem();
            this.mnVisibleCount = new ToolStripMenuItem();
            this.mnVSplitter2 = new ToolStripSeparator();
            this.mnDataViewFontSize = new ToolStripMenuItem();
            this.mnFontLarge = new ToolStripMenuItem();
            this.mnFontSmall = new ToolStripMenuItem();
            this.mnFontDefault = new ToolStripMenuItem();
            this.mnJob = new ToolStripMenuItem();
            this.mnLogon = new ToolStripMenuItem();
            this.mnLogoff = new ToolStripMenuItem();
            this.mnJSplitter1 = new ToolStripSeparator();
            this.mnBatchSend = new ToolStripMenuItem();
            this.mnJSplitter3 = new ToolStripSeparator();
            this.mnRep = new ToolStripMenuItem();
            this.mnJSplitter4 = new ToolStripSeparator();
            this.mnOther = new ToolStripMenuItem();
            this.mnOtherTerm = new ToolStripMenuItem();
            this.mnOption = new ToolStripMenuItem();
            this.mnSystem = new ToolStripMenuItem();
            this.mnOSplitter1 = new ToolStripSeparator();
            this.mnDataViewRestore = new ToolStripMenuItem();
            this.mnDataViewRepair = new ToolStripMenuItem();
            this.mnOSplitter2 = new ToolStripSeparator();
            this.mnDataViewImport = new ToolStripMenuItem();
            this.mnDataViewExport = new ToolStripMenuItem();
            this.mnOSplitter3 = new ToolStripSeparator();
            this.mnCloseOnAppend = new ToolStripMenuItem();
            this.mnListRestriction = new ToolStripMenuItem();
            this.mnQueryFieldClear = new ToolStripMenuItem();
            this.mnLogoffConf = new ToolStripMenuItem();
            this.mnHelp = new ToolStripMenuItem();
            this.mnJobMessage = new ToolStripMenuItem();
            this.mnPackageMessage = new ToolStripMenuItem();
            this.mnHSplitter1 = new ToolStripSeparator();
            this.mnVersionInfo = new ToolStripMenuItem();
            this.mnVersionUp = new ToolStripMenuItem();
            this.mnHSplitter2 = new ToolStripSeparator();
            this.mnSupport = new ToolStripMenuItem();
            this.mnWebSiteLink = new ToolStripMenuItem();
            this.mnJobKey = new ToolStripMenuItem();
            this.mnJobKey1 = new ToolStripMenuItem();
            this.mnJobKey2 = new ToolStripMenuItem();
            this.mnJobKey3 = new ToolStripMenuItem();
            this.mnJobKey4 = new ToolStripMenuItem();
            this.mnJobKey5 = new ToolStripMenuItem();
            this.mnJobKey6 = new ToolStripMenuItem();
            this.mnJobKey7 = new ToolStripMenuItem();
            this.mnJobKey8 = new ToolStripMenuItem();
            this.mnJobKey9 = new ToolStripMenuItem();
            this.mnJobKey10 = new ToolStripMenuItem();
            this.mnJobKey11 = new ToolStripMenuItem();
            this.mnJobKey12 = new ToolStripMenuItem();
            this.mnJobKey13 = new ToolStripMenuItem();
            this.mnJobKey14 = new ToolStripMenuItem();
            this.mnJobKey15 = new ToolStripMenuItem();
            this.mnJobKey16 = new ToolStripMenuItem();
            this.mnJobKey17 = new ToolStripMenuItem();
            this.mnJobKey18 = new ToolStripMenuItem();
            this.mnJobKey19 = new ToolStripMenuItem();
            this.mnJobKey20 = new ToolStripMenuItem();
            this.mnJobKey21 = new ToolStripMenuItem();
            this.mnJobKey22 = new ToolStripMenuItem();
            this.mnJobKey23 = new ToolStripMenuItem();
            this.mnJobKey24 = new ToolStripMenuItem();
            this.mnDebug = new ToolStripMenuItem();
            this.mnRecvText = new ToolStripMenuItem();
            this.mnInputCheck = new ToolStripMenuItem();
            this.mnControlChange = new ToolStripMenuItem();
            this.mnControlSS = new ToolStripMenuItem();
            this.mnControlMS = new ToolStripMenuItem();
            this.mnControlGS = new ToolStripMenuItem();
            this.mnControlWS = new ToolStripMenuItem();
            this.ofdMain = new OpenFileDialog();
            this.tmrJobConnect = new System.Windows.Forms.Timer(this.components);
            this.tmrSendRecv = new System.Windows.Forms.Timer(this.components);
            this.uJobMenu1 = new UJobMenu();
            this.uJobInput1 = new UJobInput();
            this.tlcMain.BottomToolStripPanel.SuspendLayout();
            this.tlcMain.ContentPanel.SuspendLayout();
            this.tlcMain.TopToolStripPanel.SuspendLayout();
            this.tlcMain.SuspendLayout();
            this.tslFunction.SuspendLayout();
            this.stsMain.SuspendLayout();
            this.pnlSupportWindow.SuspendLayout();
            this.pnlJobMenu.SuspendLayout();
            this.pnlJobInput.SuspendLayout();
            this.tslStandard.SuspendLayout();
            this.tslJob.SuspendLayout();
            this.mnMainMenu.SuspendLayout();
            base.SuspendLayout();
            this.tlcMain.BottomToolStripPanel.Controls.Add(this.tslFunction);
            this.tlcMain.BottomToolStripPanel.Controls.Add(this.stsMain);
            this.tlcMain.ContentPanel.Controls.Add(this.pnlDataView);
            this.tlcMain.ContentPanel.Controls.Add(this.splMain);
            this.tlcMain.ContentPanel.Controls.Add(this.pnlSupportWindow);
            manager.ApplyResources(this.tlcMain.ContentPanel, "tlcMain.ContentPanel");
            manager.ApplyResources(this.tlcMain, "tlcMain");
            this.tlcMain.Name = "tlcMain";
            this.tlcMain.TopToolStripPanel.Controls.Add(this.tslStandard);
            this.tlcMain.TopToolStripPanel.Controls.Add(this.tslJob);
            manager.ApplyResources(this.tslFunction, "tslFunction");
            this.tslFunction.Items.AddRange(new ToolStripItem[] { this.tbbF1, this.tbbF2, this.tbbF3, this.tbbF4, this.tbbFSplitter1, this.tbbF5, this.tbbF6, this.tbbF7, this.tbbF8, this.tbbFSplitter2, this.tbbF9, this.tbbF10, this.tbbF11, this.tbbF12 });
            this.tslFunction.Name = "tslFunction";
            manager.ApplyResources(this.tbbF1, "tbbF1");
            this.tbbF1.Name = "tbbF1";
            this.tbbF1.Tag = "0";
            this.tbbF1.Click += new EventHandler(this.Function_Click);
            manager.ApplyResources(this.tbbF2, "tbbF2");
            this.tbbF2.Name = "tbbF2";
            this.tbbF2.Tag = "1";
            this.tbbF2.Click += new EventHandler(this.Function_Click);
            manager.ApplyResources(this.tbbF3, "tbbF3");
            this.tbbF3.Name = "tbbF3";
            this.tbbF3.Tag = "2";
            this.tbbF3.Click += new EventHandler(this.Function_Click);
            manager.ApplyResources(this.tbbF4, "tbbF4");
            this.tbbF4.Name = "tbbF4";
            this.tbbF4.Tag = "3";
            this.tbbF4.Click += new EventHandler(this.Function_Click);
            this.tbbFSplitter1.Name = "tbbFSplitter1";
            manager.ApplyResources(this.tbbFSplitter1, "tbbFSplitter1");
            manager.ApplyResources(this.tbbF5, "tbbF5");
            this.tbbF5.Name = "tbbF5";
            this.tbbF5.Tag = "4";
            this.tbbF5.Click += new EventHandler(this.Function_Click);
            manager.ApplyResources(this.tbbF6, "tbbF6");
            this.tbbF6.Name = "tbbF6";
            this.tbbF6.Tag = "5";
            this.tbbF6.Click += new EventHandler(this.Function_Click);
            manager.ApplyResources(this.tbbF7, "tbbF7");
            this.tbbF7.Name = "tbbF7";
            this.tbbF7.Tag = "6";
            this.tbbF7.Click += new EventHandler(this.Function_Click);
            manager.ApplyResources(this.tbbF8, "tbbF8");
            this.tbbF8.Name = "tbbF8";
            this.tbbF8.Tag = "7";
            this.tbbF8.Click += new EventHandler(this.Function_Click);
            this.tbbFSplitter2.Name = "tbbFSplitter2";
            manager.ApplyResources(this.tbbFSplitter2, "tbbFSplitter2");
            manager.ApplyResources(this.tbbF9, "tbbF9");
            this.tbbF9.Name = "tbbF9";
            this.tbbF9.Tag = "8";
            this.tbbF9.Click += new EventHandler(this.Function_Click);
            manager.ApplyResources(this.tbbF10, "tbbF10");
            this.tbbF10.Name = "tbbF10";
            this.tbbF10.Tag = "9";
            this.tbbF10.Click += new EventHandler(this.Function_Click);
            manager.ApplyResources(this.tbbF11, "tbbF11");
            this.tbbF11.Name = "tbbF11";
            this.tbbF11.Tag = "10";
            this.tbbF11.Click += new EventHandler(this.Function_Click);
            manager.ApplyResources(this.tbbF12, "tbbF12");
            this.tbbF12.Name = "tbbF12";
            this.tbbF12.Tag = "11";
            this.tbbF12.Click += new EventHandler(this.Function_Click);
            manager.ApplyResources(this.stsMain, "stsMain");
            this.stsMain.Items.AddRange(new ToolStripItem[] { this.stbStatus, this.stbUserCode, this.stbMailAdress, this.stbRepStatus, this.stbReqStatus, this.stbInfomation, this.stbProcess, this.stbTerm });
            this.stsMain.Name = "stsMain";
            manager.ApplyResources(this.stbStatus, "stbStatus");
            this.stbStatus.BorderSides = ToolStripStatusLabelBorderSides.Right;
            this.stbStatus.Name = "stbStatus";
            manager.ApplyResources(this.stbUserCode, "stbUserCode");
            this.stbUserCode.BorderSides = ToolStripStatusLabelBorderSides.Right;
            this.stbUserCode.Name = "stbUserCode";
            manager.ApplyResources(this.stbMailAdress, "stbMailAdress");
            this.stbMailAdress.BorderSides = ToolStripStatusLabelBorderSides.Right;
            this.stbMailAdress.Name = "stbMailAdress";
            manager.ApplyResources(this.stbRepStatus, "stbRepStatus");
            this.stbRepStatus.BorderSides = ToolStripStatusLabelBorderSides.Right;
            this.stbRepStatus.Name = "stbRepStatus";
            manager.ApplyResources(this.stbReqStatus, "stbReqStatus");
            this.stbReqStatus.BorderSides = ToolStripStatusLabelBorderSides.Right;
            this.stbReqStatus.Name = "stbReqStatus";
            this.stbInfomation.Name = "stbInfomation";
            manager.ApplyResources(this.stbInfomation, "stbInfomation");
            this.stbInfomation.Spring = true;
            manager.ApplyResources(this.stbProcess, "stbProcess");
            this.stbProcess.BorderSides = ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Left;
            this.stbProcess.Name = "stbProcess";
            manager.ApplyResources(this.stbTerm, "stbTerm");
            this.stbTerm.BorderStyle = Border3DStyle.RaisedOuter;
            this.stbTerm.Name = "stbTerm";
            manager.ApplyResources(this.pnlDataView, "pnlDataView");
            this.pnlDataView.Name = "pnlDataView";
            manager.ApplyResources(this.splMain, "splMain");
            this.splMain.Name = "splMain";
            this.splMain.TabStop = false;
            this.pnlSupportWindow.Controls.Add(this.pnlJobMenu);
            this.pnlSupportWindow.Controls.Add(this.pnlJobInput);
            this.pnlSupportWindow.Controls.Add(this.pnlUserInput);
            manager.ApplyResources(this.pnlSupportWindow, "pnlSupportWindow");
            this.pnlSupportWindow.Name = "pnlSupportWindow";
            this.pnlJobMenu.Controls.Add(this.uJobMenu1);
            manager.ApplyResources(this.pnlJobMenu, "pnlJobMenu");
            this.pnlJobMenu.Name = "pnlJobMenu";
            manager.ApplyResources(this.pnlJobInput, "pnlJobInput");
            this.pnlJobInput.Controls.Add(this.uJobInput1);
            this.pnlJobInput.Name = "pnlJobInput";
            manager.ApplyResources(this.pnlUserInput, "pnlUserInput");
            this.pnlUserInput.Name = "pnlUserInput";
            manager.ApplyResources(this.tslStandard, "tslStandard");
            this.tslStandard.Items.AddRange(new ToolStripItem[] { this.tbbJobNew, this.tbbPrintJobData, this.tbbPrintJobDataPreview, this.tbbOpen, this.tbbSSplitter1, this.tbbFileOpen, this.tbbFileSaveAs });
            this.tslStandard.Name = "tslStandard";
            manager.ApplyResources(this.tbbJobNew, "tbbJobNew");
            this.tbbJobNew.Name = "tbbJobNew";
            this.tbbJobNew.Click += new EventHandler(this.mnJobNew_Click);
            manager.ApplyResources(this.tbbPrintJobData, "tbbPrintJobData");
            this.tbbPrintJobData.Name = "tbbPrintJobData";
            this.tbbPrintJobData.Click += new EventHandler(this.mnPrintJobData_Click);
            manager.ApplyResources(this.tbbPrintJobDataPreview, "tbbPrintJobDataPreview");
            this.tbbPrintJobDataPreview.Name = "tbbPrintJobDataPreview";
            this.tbbPrintJobDataPreview.Click += new EventHandler(this.mnPrintJobDataPreview_Click);
            manager.ApplyResources(this.tbbOpen, "tbbOpen");
            this.tbbOpen.Name = "tbbOpen";
            this.tbbOpen.Click += new EventHandler(this.mnOpen_Click);
            this.tbbSSplitter1.Name = "tbbSSplitter1";
            manager.ApplyResources(this.tbbSSplitter1, "tbbSSplitter1");
            manager.ApplyResources(this.tbbFileOpen, "tbbFileOpen");
            this.tbbFileOpen.Name = "tbbFileOpen";
            this.tbbFileOpen.Click += new EventHandler(this.mnFileOpen_Click);
            manager.ApplyResources(this.tbbFileSaveAs, "tbbFileSaveAs");
            this.tbbFileSaveAs.Name = "tbbFileSaveAs";
            this.tbbFileSaveAs.Click += new EventHandler(this.mnFileSave_Click);
            manager.ApplyResources(this.tslJob, "tslJob");
            this.tslJob.Items.AddRange(new ToolStripItem[] { this.tbbLogon, this.tbbLogoff, this.tbbJSplitter1, this.tbbSend, this.tbbMailRecv, this.tbbMailSendRecv, this.tbbJSplitter2, this.tbbREP, this.tbbREQ, this.tbbJSplitter3, this.tbbBatchTake, this.tbbJSplitter4, this.tbbGuidance, this.tbbJSplitter5, this.tbbJetras });
            this.tslJob.Name = "tslJob";
            manager.ApplyResources(this.tbbLogon, "tbbLogon");
            this.tbbLogon.Name = "tbbLogon";
            this.tbbLogon.Click += new EventHandler(this.mnLogon_Click);
            manager.ApplyResources(this.tbbLogoff, "tbbLogoff");
            this.tbbLogoff.Name = "tbbLogoff";
            this.tbbLogoff.Click += new EventHandler(this.mnLogoff_Click);
            this.tbbJSplitter1.Name = "tbbJSplitter1";
            manager.ApplyResources(this.tbbJSplitter1, "tbbJSplitter1");
            manager.ApplyResources(this.tbbSend, "tbbSend");
            this.tbbSend.Name = "tbbSend";
            this.tbbSend.Click += new EventHandler(this.Send_Click);
            manager.ApplyResources(this.tbbMailRecv, "tbbMailRecv");
            this.tbbMailRecv.Name = "tbbMailRecv";
            this.tbbMailRecv.Click += new EventHandler(this.MailRecv_Click);
            manager.ApplyResources(this.tbbMailSendRecv, "tbbMailSendRecv");
            this.tbbMailSendRecv.Name = "tbbMailSendRecv";
            this.tbbMailSendRecv.Click += new EventHandler(this.MailSendRecv_Click);
            this.tbbJSplitter2.Name = "tbbJSplitter2";
            manager.ApplyResources(this.tbbJSplitter2, "tbbJSplitter2");
            manager.ApplyResources(this.tbbREP, "tbbREP");
            this.tbbREP.Name = "tbbREP";
            this.tbbREP.Click += new EventHandler(this.mnRep_Click);
            manager.ApplyResources(this.tbbREQ, "tbbREQ");
            this.tbbREQ.Name = "tbbREQ";
            this.tbbREQ.Click += new EventHandler(this.mnREQ_Click);
            this.tbbJSplitter3.Name = "tbbJSplitter3";
            manager.ApplyResources(this.tbbJSplitter3, "tbbJSplitter3");
            manager.ApplyResources(this.tbbBatchTake, "tbbBatchTake");
            this.tbbBatchTake.Name = "tbbBatchTake";
            this.tbbBatchTake.Click += new EventHandler(this.mnBatchTake_Click);
            this.tbbJSplitter4.Name = "tbbJSplitter4";
            manager.ApplyResources(this.tbbJSplitter4, "tbbJSplitter4");
            manager.ApplyResources(this.tbbGuidance, "tbbGuidance");
            this.tbbGuidance.Name = "tbbGuidance";
            this.tbbGuidance.Click += new EventHandler(this.mnGuidance_Click);
            this.tbbJSplitter5.Name = "tbbJSplitter5";
            manager.ApplyResources(this.tbbJSplitter5, "tbbJSplitter5");
            this.tbbJetras.DisplayStyle = ToolStripItemDisplayStyle.Text;
            manager.ApplyResources(this.tbbJetras, "tbbJetras");
            this.tbbJetras.Name = "tbbJetras";
            this.tbbJetras.Click += new EventHandler(this.tbbJetras_Click);
            this.mnMainMenu.Items.AddRange(new ToolStripItem[] { this.mnFile, this.mnEdit, this.mnVisible, this.mnJob, this.mnOption, this.mnHelp, this.mnJobKey, this.mnDebug });
            manager.ApplyResources(this.mnMainMenu, "mnMainMenu");
            this.mnMainMenu.Name = "mnMainMenu";
            this.mnFile.DropDownItems.AddRange(new ToolStripItem[] { this.mnJobNew, this.mnOpen, this.mnJobOpen, this.mnRedo, this.mnFSplitter1, this.mnOutFile, this.mnFSplitter2, this.mnPrintJobData, this.mnPrintJobDataPreview, this.mnHardCopy, this.mnDataViewPrint, this.mnFSplitter3, this.mnExit });
            this.mnFile.Name = "mnFile";
            manager.ApplyResources(this.mnFile, "mnFile");
            manager.ApplyResources(this.mnJobNew, "mnJobNew");
            this.mnJobNew.Name = "mnJobNew";
            this.mnJobNew.Tag = "mnJobNew";
            this.mnJobNew.Click += new EventHandler(this.mnJobNew_Click);
            manager.ApplyResources(this.mnOpen, "mnOpen");
            this.mnOpen.Name = "mnOpen";
            this.mnOpen.Tag = "mnOpen";
            this.mnOpen.Click += new EventHandler(this.mnOpen_Click);
            this.mnJobOpen.Name = "mnJobOpen";
            manager.ApplyResources(this.mnJobOpen, "mnJobOpen");
            this.mnJobOpen.Tag = "mnJobOpen";
            this.mnJobOpen.Click += new EventHandler(this.mnJobOpen_Click);
            this.mnRedo.Name = "mnRedo";
            manager.ApplyResources(this.mnRedo, "mnRedo");
            this.mnRedo.Tag = "mnRedo";
            this.mnRedo.Click += new EventHandler(this.mnRedo_Click);
            this.mnFSplitter1.Name = "mnFSplitter1";
            manager.ApplyResources(this.mnFSplitter1, "mnFSplitter1");
            this.mnOutFile.DropDownItems.AddRange(new ToolStripItem[] { this.mnFileSave, this.mnFileSend, this.mnFileDataViewAppend });
            this.mnOutFile.Name = "mnOutFile";
            manager.ApplyResources(this.mnOutFile, "mnOutFile");
            manager.ApplyResources(this.mnFileSave, "mnFileSave");
            this.mnFileSave.Name = "mnFileSave";
            this.mnFileSave.Tag = "mnFileSaveAs";
            this.mnFileSave.Click += new EventHandler(this.mnFileSave_Click);
            this.mnFileSend.Name = "mnFileSend";
            manager.ApplyResources(this.mnFileSend, "mnFileSend");
            this.mnFileSend.Tag = "mnFileSend";
            this.mnFileSend.Click += new EventHandler(this.mnFileSend_Click);
            this.mnFileDataViewAppend.Name = "mnFileDataViewAppend";
            manager.ApplyResources(this.mnFileDataViewAppend, "mnFileDataViewAppend");
            this.mnFileDataViewAppend.Tag = "mnFileDataViewAppend";
            this.mnFileDataViewAppend.Click += new EventHandler(this.mnFileDataViewAppend_Click);
            this.mnFSplitter2.Name = "mnFSplitter2";
            manager.ApplyResources(this.mnFSplitter2, "mnFSplitter2");
            manager.ApplyResources(this.mnPrintJobData, "mnPrintJobData");
            this.mnPrintJobData.Name = "mnPrintJobData";
            this.mnPrintJobData.Tag = "mnPrintJobData";
            this.mnPrintJobData.Click += new EventHandler(this.mnPrintJobData_Click);
            manager.ApplyResources(this.mnPrintJobDataPreview, "mnPrintJobDataPreview");
            this.mnPrintJobDataPreview.Name = "mnPrintJobDataPreview";
            this.mnPrintJobDataPreview.Tag = "mnPrintJobDataPreview";
            this.mnPrintJobDataPreview.Click += new EventHandler(this.mnPrintJobDataPreview_Click);
            this.mnHardCopy.DropDownItems.AddRange(new ToolStripItem[] { this.mnHardCopyPrint, this.mnHardCopyPreview });
            this.mnHardCopy.Name = "mnHardCopy";
            manager.ApplyResources(this.mnHardCopy, "mnHardCopy");
            this.mnHardCopyPrint.Name = "mnHardCopyPrint";
            manager.ApplyResources(this.mnHardCopyPrint, "mnHardCopyPrint");
            this.mnHardCopyPrint.Tag = "mnHardCopy";
            this.mnHardCopyPrint.Click += new EventHandler(this.mnHardCopyPrint_Click);
            this.mnHardCopyPreview.Name = "mnHardCopyPreview";
            manager.ApplyResources(this.mnHardCopyPreview, "mnHardCopyPreview");
            this.mnHardCopyPreview.Tag = "mnHardCopyPreview";
            this.mnHardCopyPreview.Click += new EventHandler(this.mnHardCopyPreview_Click);
            this.mnDataViewPrint.DropDownItems.AddRange(new ToolStripItem[] { this.mnPrintDataView, this.mnPrintDataViewPreview });
            this.mnDataViewPrint.Name = "mnDataViewPrint";
            manager.ApplyResources(this.mnDataViewPrint, "mnDataViewPrint");
            this.mnDataViewPrint.Tag = "";
            this.mnPrintDataView.Name = "mnPrintDataView";
            manager.ApplyResources(this.mnPrintDataView, "mnPrintDataView");
            this.mnPrintDataView.Tag = "mnPrintDataView";
            this.mnPrintDataView.Click += new EventHandler(this.mnPrintDataView_Click);
            this.mnPrintDataViewPreview.Name = "mnPrintDataViewPreview";
            manager.ApplyResources(this.mnPrintDataViewPreview, "mnPrintDataViewPreview");
            this.mnPrintDataViewPreview.Tag = "mnPrintDataViewPreview";
            this.mnPrintDataViewPreview.Click += new EventHandler(this.mnPrintDataViewPreview_Click);
            this.mnFSplitter3.Name = "mnFSplitter3";
            manager.ApplyResources(this.mnFSplitter3, "mnFSplitter3");
            this.mnExit.Name = "mnExit";
            manager.ApplyResources(this.mnExit, "mnExit");
            this.mnExit.Click += new EventHandler(this.mnExit_Click);
            this.mnEdit.DropDownItems.AddRange(new ToolStripItem[] { this.mnUndo, this.mnDataDelete, this.mnESplitter1, this.mnAllSelect, this.mnESplitter2, this.mnClearDelete, this.mnESplitter3, this.mnFolder, this.mnESplitter4, this.mnDistribute, this.mnSelectedDistribute, this.mnESplitter5, this.mnSendSearch, this.mnRecvSearch });
            this.mnEdit.Name = "mnEdit";
            manager.ApplyResources(this.mnEdit, "mnEdit");
            this.mnUndo.Name = "mnUndo";
            manager.ApplyResources(this.mnUndo, "mnUndo");
            this.mnUndo.Tag = "mnUndo";
            this.mnUndo.Click += new EventHandler(this.mnUndo_Click);
            this.mnDataDelete.Name = "mnDataDelete";
            manager.ApplyResources(this.mnDataDelete, "mnDataDelete");
            this.mnDataDelete.Click += new EventHandler(this.mnDataDelete_Click);
            this.mnESplitter1.Name = "mnESplitter1";
            manager.ApplyResources(this.mnESplitter1, "mnESplitter1");
            this.mnAllSelect.Name = "mnAllSelect";
            manager.ApplyResources(this.mnAllSelect, "mnAllSelect");
            this.mnAllSelect.Tag = "mnSelectAll";
            this.mnAllSelect.Click += new EventHandler(this.mnAllSelect_Click);
            this.mnESplitter2.Name = "mnESplitter2";
            manager.ApplyResources(this.mnESplitter2, "mnESplitter2");
            this.mnClearDelete.Name = "mnClearDelete";
            manager.ApplyResources(this.mnClearDelete, "mnClearDelete");
            this.mnClearDelete.Tag = "mnCLearDelete";
            this.mnClearDelete.Click += new EventHandler(this.mnClearDelete_Click);
            this.mnESplitter3.Name = "mnESplitter3";
            manager.ApplyResources(this.mnESplitter3, "mnESplitter3");
            this.mnFolder.DropDownItems.AddRange(new ToolStripItem[] { this.mnCreateFolder, this.mnRenameFolder, this.mnDeleteFolder });
            this.mnFolder.Name = "mnFolder";
            manager.ApplyResources(this.mnFolder, "mnFolder");
            this.mnCreateFolder.Name = "mnCreateFolder";
            manager.ApplyResources(this.mnCreateFolder, "mnCreateFolder");
            this.mnCreateFolder.Tag = "mnCreateFolder";
            this.mnCreateFolder.Click += new EventHandler(this.mnCreateFolder_Click);
            this.mnRenameFolder.Name = "mnRenameFolder";
            manager.ApplyResources(this.mnRenameFolder, "mnRenameFolder");
            this.mnRenameFolder.Tag = "mnRenameFolder";
            this.mnRenameFolder.Click += new EventHandler(this.mnRenameFolder_Click);
            this.mnDeleteFolder.Name = "mnDeleteFolder";
            manager.ApplyResources(this.mnDeleteFolder, "mnDeleteFolder");
            this.mnDeleteFolder.Click += new EventHandler(this.mnDeleteFolde_Click);
            this.mnESplitter4.Name = "mnESplitter4";
            manager.ApplyResources(this.mnESplitter4, "mnESplitter4");
            this.mnDistribute.Name = "mnDistribute";
            manager.ApplyResources(this.mnDistribute, "mnDistribute");
            this.mnDistribute.Tag = "mnDistribute";
            this.mnDistribute.Click += new EventHandler(this.mnDistribute_Click);
            this.mnSelectedDistribute.Name = "mnSelectedDistribute";
            manager.ApplyResources(this.mnSelectedDistribute, "mnSelectedDistribute");
            this.mnSelectedDistribute.Tag = "mnSelectedDistribute";
            this.mnSelectedDistribute.Click += new EventHandler(this.mnSelectedDistribute_Click);
            this.mnESplitter5.Name = "mnESplitter5";
            manager.ApplyResources(this.mnESplitter5, "mnESplitter5");
            this.mnSendSearch.Name = "mnSendSearch";
            manager.ApplyResources(this.mnSendSearch, "mnSendSearch");
            this.mnSendSearch.Tag = "mnSendedSearch";
            this.mnSendSearch.Click += new EventHandler(this.mnSendSearch_Click);
            this.mnRecvSearch.Name = "mnRecvSearch";
            manager.ApplyResources(this.mnRecvSearch, "mnRecvSearch");
            this.mnRecvSearch.Tag = "mnReceivedSearch";
            this.mnRecvSearch.Click += new EventHandler(this.mnRecvSearch_Click);
            this.mnVisible.DropDownItems.AddRange(new ToolStripItem[] { this.mnWindow, this.mnToolbar, this.mnVSplitter1, this.mnViewRefresh, this.mnVisibleCount, this.mnVSplitter2, this.mnDataViewFontSize });
            this.mnVisible.Name = "mnVisible";
            manager.ApplyResources(this.mnVisible, "mnVisible");
            this.mnWindow.DropDownItems.AddRange(new ToolStripItem[] { this.mnLogonWindow, this.mnJobInputWindow, this.mnJobMenuWindow, this.mnDataViewWindow });
            this.mnWindow.Name = "mnWindow";
            manager.ApplyResources(this.mnWindow, "mnWindow");
            this.mnLogonWindow.Checked = true;
            this.mnLogonWindow.CheckOnClick = true;
            this.mnLogonWindow.CheckState = CheckState.Checked;
            this.mnLogonWindow.Name = "mnLogonWindow";
            manager.ApplyResources(this.mnLogonWindow, "mnLogonWindow");
            this.mnLogonWindow.Click += new EventHandler(this.mnLogonWindow_Click);
            this.mnJobInputWindow.Checked = true;
            this.mnJobInputWindow.CheckOnClick = true;
            this.mnJobInputWindow.CheckState = CheckState.Checked;
            this.mnJobInputWindow.Name = "mnJobInputWindow";
            manager.ApplyResources(this.mnJobInputWindow, "mnJobInputWindow");
            this.mnJobInputWindow.Click += new EventHandler(this.mnJobInputWindow_Click);
            this.mnJobMenuWindow.Checked = true;
            this.mnJobMenuWindow.CheckOnClick = true;
            this.mnJobMenuWindow.CheckState = CheckState.Checked;
            this.mnJobMenuWindow.Name = "mnJobMenuWindow";
            manager.ApplyResources(this.mnJobMenuWindow, "mnJobMenuWindow");
            this.mnJobMenuWindow.Click += new EventHandler(this.mnJobMenuWindow_Click);
            this.mnDataViewWindow.Checked = true;
            this.mnDataViewWindow.CheckOnClick = true;
            this.mnDataViewWindow.CheckState = CheckState.Checked;
            this.mnDataViewWindow.Name = "mnDataViewWindow";
            manager.ApplyResources(this.mnDataViewWindow, "mnDataViewWindow");
            this.mnDataViewWindow.Click += new EventHandler(this.mnDataViewWindow_Click);
            this.mnToolbar.DropDownItems.AddRange(new ToolStripItem[] { this.mnToolbarStandard, this.mnToolbarJob, this.mnFunctionBar });
            this.mnToolbar.Name = "mnToolbar";
            manager.ApplyResources(this.mnToolbar, "mnToolbar");
            this.mnToolbarStandard.Checked = true;
            this.mnToolbarStandard.CheckOnClick = true;
            this.mnToolbarStandard.CheckState = CheckState.Checked;
            this.mnToolbarStandard.Name = "mnToolbarStandard";
            manager.ApplyResources(this.mnToolbarStandard, "mnToolbarStandard");
            this.mnToolbarStandard.Click += new EventHandler(this.mnToolbarStandard_Click);
            this.mnToolbarJob.Checked = true;
            this.mnToolbarJob.CheckOnClick = true;
            this.mnToolbarJob.CheckState = CheckState.Checked;
            this.mnToolbarJob.Name = "mnToolbarJob";
            manager.ApplyResources(this.mnToolbarJob, "mnToolbarJob");
            this.mnToolbarJob.Click += new EventHandler(this.mnToolbarJob_Click);
            this.mnFunctionBar.Checked = true;
            this.mnFunctionBar.CheckOnClick = true;
            this.mnFunctionBar.CheckState = CheckState.Checked;
            this.mnFunctionBar.Name = "mnFunctionBar";
            manager.ApplyResources(this.mnFunctionBar, "mnFunctionBar");
            this.mnFunctionBar.Click += new EventHandler(this.mnFunctionBar_Click);
            this.mnVSplitter1.Name = "mnVSplitter1";
            manager.ApplyResources(this.mnVSplitter1, "mnVSplitter1");
            this.mnViewRefresh.Name = "mnViewRefresh";
            manager.ApplyResources(this.mnViewRefresh, "mnViewRefresh");
            this.mnViewRefresh.Tag = "mnViewRefresh";
            this.mnViewRefresh.Click += new EventHandler(this.mnViewRefresh_Click);
            this.mnVisibleCount.Checked = true;
            this.mnVisibleCount.CheckOnClick = true;
            this.mnVisibleCount.CheckState = CheckState.Checked;
            this.mnVisibleCount.Name = "mnVisibleCount";
            manager.ApplyResources(this.mnVisibleCount, "mnVisibleCount");
            this.mnVisibleCount.Tag = "";
            this.mnVisibleCount.Click += new EventHandler(this.mnVisibleCount_Click);
            this.mnVSplitter2.Name = "mnVSplitter2";
            manager.ApplyResources(this.mnVSplitter2, "mnVSplitter2");
            this.mnDataViewFontSize.DropDownItems.AddRange(new ToolStripItem[] { this.mnFontLarge, this.mnFontSmall, this.mnFontDefault });
            this.mnDataViewFontSize.Name = "mnDataViewFontSize";
            manager.ApplyResources(this.mnDataViewFontSize, "mnDataViewFontSize");
            this.mnFontLarge.CheckOnClick = true;
            this.mnFontLarge.Name = "mnFontLarge";
            manager.ApplyResources(this.mnFontLarge, "mnFontLarge");
            this.mnFontLarge.Click += new EventHandler(this.mnFontLarge_Click);
            this.mnFontSmall.CheckOnClick = true;
            this.mnFontSmall.Name = "mnFontSmall";
            manager.ApplyResources(this.mnFontSmall, "mnFontSmall");
            this.mnFontSmall.Click += new EventHandler(this.mnFontSmall_Click);
            this.mnFontDefault.Checked = true;
            this.mnFontDefault.CheckState = CheckState.Checked;
            this.mnFontDefault.Name = "mnFontDefault";
            manager.ApplyResources(this.mnFontDefault, "mnFontDefault");
            this.mnFontDefault.Click += new EventHandler(this.mnFontDefault_Click);
            this.mnJob.DropDownItems.AddRange(new ToolStripItem[] { this.mnLogon, this.mnLogoff, this.mnJSplitter1, this.mnBatchSend, this.mnJSplitter3, this.mnRep, this.mnJSplitter4, this.mnOther });
            this.mnJob.Name = "mnJob";
            manager.ApplyResources(this.mnJob, "mnJob");
            manager.ApplyResources(this.mnLogon, "mnLogon");
            this.mnLogon.Name = "mnLogon";
            this.mnLogon.Tag = "mnLogOn";
            this.mnLogon.Click += new EventHandler(this.mnLogon_Click);
            manager.ApplyResources(this.mnLogoff, "mnLogoff");
            this.mnLogoff.Name = "mnLogoff";
            this.mnLogoff.Tag = "mnLogOff";
            this.mnLogoff.Click += new EventHandler(this.mnLogoff_Click);
            this.mnJSplitter1.Name = "mnJSplitter1";
            manager.ApplyResources(this.mnJSplitter1, "mnJSplitter1");
            this.mnBatchSend.Name = "mnBatchSend";
            manager.ApplyResources(this.mnBatchSend, "mnBatchSend");
            this.mnBatchSend.Tag = "mnbatchSend";
            this.mnBatchSend.Click += new EventHandler(this.mnBatchSend_Click);
            this.mnJSplitter3.Name = "mnJSplitter3";
            manager.ApplyResources(this.mnJSplitter3, "mnJSplitter3");
            manager.ApplyResources(this.mnRep, "mnRep");
            this.mnRep.Name = "mnRep";
            this.mnRep.Tag = "mnREP1";
            this.mnRep.Click += new EventHandler(this.mnRep_Click);
            this.mnJSplitter4.Name = "mnJSplitter4";
            manager.ApplyResources(this.mnJSplitter4, "mnJSplitter4");
            this.mnOther.DropDownItems.AddRange(new ToolStripItem[] { this.mnOtherTerm });
            this.mnOther.Name = "mnOther";
            manager.ApplyResources(this.mnOther, "mnOther");
            this.mnOtherTerm.Name = "mnOtherTerm";
            manager.ApplyResources(this.mnOtherTerm, "mnOtherTerm");
            this.mnOtherTerm.Tag = "mnTTM";
            this.mnOtherTerm.Click += new EventHandler(this.mnOtherTerm_Click);
            this.mnOption.DropDownItems.AddRange(new ToolStripItem[] { this.mnSystem, this.mnOSplitter1, this.mnDataViewRestore, this.mnDataViewRepair, this.mnOSplitter2, this.mnDataViewImport, this.mnDataViewExport, this.mnOSplitter3, this.mnCloseOnAppend, this.mnListRestriction, this.mnQueryFieldClear, this.mnLogoffConf });
            this.mnOption.Name = "mnOption";
            manager.ApplyResources(this.mnOption, "mnOption");
            this.mnSystem.Name = "mnSystem";
            manager.ApplyResources(this.mnSystem, "mnSystem");
            this.mnSystem.Tag = "mnSystem";
            this.mnSystem.Click += new EventHandler(this.mnSystem_Click);
            this.mnOSplitter1.Name = "mnOSplitter1";
            manager.ApplyResources(this.mnOSplitter1, "mnOSplitter1");
            this.mnDataViewRestore.Name = "mnDataViewRestore";
            manager.ApplyResources(this.mnDataViewRestore, "mnDataViewRestore");
            this.mnDataViewRestore.Tag = "mnDataViewRestore";
            this.mnDataViewRestore.Click += new EventHandler(this.mnDataViewRestore_Click);
            this.mnDataViewRepair.Name = "mnDataViewRepair";
            manager.ApplyResources(this.mnDataViewRepair, "mnDataViewRepair");
            this.mnDataViewRepair.Tag = "mnDataViewRepair";
            this.mnDataViewRepair.Click += new EventHandler(this.mnDataViewRepair_Click);
            this.mnOSplitter2.Name = "mnOSplitter2";
            manager.ApplyResources(this.mnOSplitter2, "mnOSplitter2");
            this.mnDataViewImport.Name = "mnDataViewImport";
            manager.ApplyResources(this.mnDataViewImport, "mnDataViewImport");
            this.mnDataViewImport.Tag = "mnDataViewImport";
            this.mnDataViewImport.Click += new EventHandler(this.mnDataViewImport_Click);
            this.mnDataViewExport.Name = "mnDataViewExport";
            manager.ApplyResources(this.mnDataViewExport, "mnDataViewExport");
            this.mnDataViewExport.Tag = "mnDataViewExport";
            this.mnDataViewExport.Click += new EventHandler(this.mnDataViewExport_Click);
            this.mnOSplitter3.Name = "mnOSplitter3";
            manager.ApplyResources(this.mnOSplitter3, "mnOSplitter3");
            this.mnCloseOnAppend.CheckOnClick = true;
            this.mnCloseOnAppend.Name = "mnCloseOnAppend";
            manager.ApplyResources(this.mnCloseOnAppend, "mnCloseOnAppend");
            this.mnListRestriction.CheckOnClick = true;
            this.mnListRestriction.Name = "mnListRestriction";
            manager.ApplyResources(this.mnListRestriction, "mnListRestriction");
            this.mnQueryFieldClear.Checked = true;
            this.mnQueryFieldClear.CheckOnClick = true;
            this.mnQueryFieldClear.CheckState = CheckState.Checked;
            this.mnQueryFieldClear.Name = "mnQueryFieldClear";
            manager.ApplyResources(this.mnQueryFieldClear, "mnQueryFieldClear");
            this.mnQueryFieldClear.Click += new EventHandler(this.mnQueryFieldClear_Click);
            this.mnLogoffConf.Checked = true;
            this.mnLogoffConf.CheckOnClick = true;
            this.mnLogoffConf.CheckState = CheckState.Checked;
            this.mnLogoffConf.Name = "mnLogoffConf";
            manager.ApplyResources(this.mnLogoffConf, "mnLogoffConf");
            this.mnHelp.DropDownItems.AddRange(new ToolStripItem[] { this.mnJobMessage, this.mnPackageMessage, this.mnHSplitter1, this.mnVersionInfo, this.mnVersionUp, this.mnHSplitter2, this.mnSupport, this.mnWebSiteLink });
            this.mnHelp.Name = "mnHelp";
            manager.ApplyResources(this.mnHelp, "mnHelp");
            this.mnJobMessage.Name = "mnJobMessage";
            manager.ApplyResources(this.mnJobMessage, "mnJobMessage");
            this.mnJobMessage.Tag = "mnJobErrorHelp,Tham khảo gi\x00fap đỡ";
            this.mnJobMessage.Click += new EventHandler(this.mnJobMessage_Click);
            this.mnPackageMessage.Name = "mnPackageMessage";
            manager.ApplyResources(this.mnPackageMessage, "mnPackageMessage");
            this.mnPackageMessage.Tag = "mnPackageMessage";
            this.mnPackageMessage.Click += new EventHandler(this.mnPackageMessage_Click);
            this.mnHSplitter1.Name = "mnHSplitter1";
            manager.ApplyResources(this.mnHSplitter1, "mnHSplitter1");
            this.mnVersionInfo.Name = "mnVersionInfo";
            manager.ApplyResources(this.mnVersionInfo, "mnVersionInfo");
            this.mnVersionInfo.Click += new EventHandler(this.mnVersionInfo_Click);
            this.mnVersionUp.Name = "mnVersionUp";
            manager.ApplyResources(this.mnVersionUp, "mnVersionUp");
            this.mnVersionUp.Tag = "mnVersionUp";
            this.mnVersionUp.Click += new EventHandler(this.mnVersionUp_Click);
            this.mnHSplitter2.Name = "mnHSplitter2";
            manager.ApplyResources(this.mnHSplitter2, "mnHSplitter2");
            this.mnSupport.Name = "mnSupport";
            manager.ApplyResources(this.mnSupport, "mnSupport");
            this.mnSupport.Tag = "mnSupport";
            this.mnSupport.Click += new EventHandler(this.mnSupport_Click);
            this.mnWebSiteLink.Name = "mnWebSiteLink";
            manager.ApplyResources(this.mnWebSiteLink, "mnWebSiteLink");
            this.mnWebSiteLink.Tag = "mnWebLink";
            this.mnWebSiteLink.Click += new EventHandler(this.mnWebSiteLink_Click);
            this.mnJobKey.DropDownItems.AddRange(new ToolStripItem[] { 
                this.mnJobKey1, this.mnJobKey2, this.mnJobKey3, this.mnJobKey4, this.mnJobKey5, this.mnJobKey6, this.mnJobKey7, this.mnJobKey8, this.mnJobKey9, this.mnJobKey10, this.mnJobKey11, this.mnJobKey12, this.mnJobKey13, this.mnJobKey14, this.mnJobKey15, this.mnJobKey16, 
                this.mnJobKey17, this.mnJobKey18, this.mnJobKey19, this.mnJobKey20, this.mnJobKey21, this.mnJobKey22, this.mnJobKey23, this.mnJobKey24
             });
            this.mnJobKey.Name = "mnJobKey";
            manager.ApplyResources(this.mnJobKey, "mnJobKey");
            this.mnJobKey1.Name = "mnJobKey1";
            manager.ApplyResources(this.mnJobKey1, "mnJobKey1");
            this.mnJobKey1.Click += new EventHandler(this.mnJobKey_Click);
            this.mnJobKey2.Name = "mnJobKey2";
            manager.ApplyResources(this.mnJobKey2, "mnJobKey2");
            this.mnJobKey2.Click += new EventHandler(this.mnJobKey_Click);
            this.mnJobKey3.Name = "mnJobKey3";
            manager.ApplyResources(this.mnJobKey3, "mnJobKey3");
            this.mnJobKey3.Click += new EventHandler(this.mnJobKey_Click);
            this.mnJobKey4.Name = "mnJobKey4";
            manager.ApplyResources(this.mnJobKey4, "mnJobKey4");
            this.mnJobKey4.Click += new EventHandler(this.mnJobKey_Click);
            this.mnJobKey5.Name = "mnJobKey5";
            manager.ApplyResources(this.mnJobKey5, "mnJobKey5");
            this.mnJobKey5.Click += new EventHandler(this.mnJobKey_Click);
            this.mnJobKey6.Name = "mnJobKey6";
            manager.ApplyResources(this.mnJobKey6, "mnJobKey6");
            this.mnJobKey6.Click += new EventHandler(this.mnJobKey_Click);
            this.mnJobKey7.Name = "mnJobKey7";
            manager.ApplyResources(this.mnJobKey7, "mnJobKey7");
            this.mnJobKey7.Click += new EventHandler(this.mnJobKey_Click);
            this.mnJobKey8.Name = "mnJobKey8";
            manager.ApplyResources(this.mnJobKey8, "mnJobKey8");
            this.mnJobKey8.Click += new EventHandler(this.mnJobKey_Click);
            this.mnJobKey9.Name = "mnJobKey9";
            manager.ApplyResources(this.mnJobKey9, "mnJobKey9");
            this.mnJobKey9.Click += new EventHandler(this.mnJobKey_Click);
            this.mnJobKey10.Name = "mnJobKey10";
            manager.ApplyResources(this.mnJobKey10, "mnJobKey10");
            this.mnJobKey10.Click += new EventHandler(this.mnJobKey_Click);
            this.mnJobKey11.Name = "mnJobKey11";
            manager.ApplyResources(this.mnJobKey11, "mnJobKey11");
            this.mnJobKey11.Click += new EventHandler(this.mnJobKey_Click);
            this.mnJobKey12.Name = "mnJobKey12";
            manager.ApplyResources(this.mnJobKey12, "mnJobKey12");
            this.mnJobKey12.Click += new EventHandler(this.mnJobKey_Click);
            this.mnJobKey13.Name = "mnJobKey13";
            manager.ApplyResources(this.mnJobKey13, "mnJobKey13");
            this.mnJobKey13.Click += new EventHandler(this.mnJobKey_Click);
            this.mnJobKey14.Name = "mnJobKey14";
            manager.ApplyResources(this.mnJobKey14, "mnJobKey14");
            this.mnJobKey14.Click += new EventHandler(this.mnJobKey_Click);
            this.mnJobKey15.Name = "mnJobKey15";
            manager.ApplyResources(this.mnJobKey15, "mnJobKey15");
            this.mnJobKey15.Click += new EventHandler(this.mnJobKey_Click);
            this.mnJobKey16.Name = "mnJobKey16";
            manager.ApplyResources(this.mnJobKey16, "mnJobKey16");
            this.mnJobKey16.Click += new EventHandler(this.mnJobKey_Click);
            this.mnJobKey17.Name = "mnJobKey17";
            manager.ApplyResources(this.mnJobKey17, "mnJobKey17");
            this.mnJobKey17.Click += new EventHandler(this.mnJobKey_Click);
            this.mnJobKey18.Name = "mnJobKey18";
            manager.ApplyResources(this.mnJobKey18, "mnJobKey18");
            this.mnJobKey18.Click += new EventHandler(this.mnJobKey_Click);
            this.mnJobKey19.Name = "mnJobKey19";
            manager.ApplyResources(this.mnJobKey19, "mnJobKey19");
            this.mnJobKey19.Click += new EventHandler(this.mnJobKey_Click);
            this.mnJobKey20.Name = "mnJobKey20";
            manager.ApplyResources(this.mnJobKey20, "mnJobKey20");
            this.mnJobKey20.Click += new EventHandler(this.mnJobKey_Click);
            this.mnJobKey21.Name = "mnJobKey21";
            manager.ApplyResources(this.mnJobKey21, "mnJobKey21");
            this.mnJobKey21.Click += new EventHandler(this.mnJobKey_Click);
            this.mnJobKey22.Name = "mnJobKey22";
            manager.ApplyResources(this.mnJobKey22, "mnJobKey22");
            this.mnJobKey22.Click += new EventHandler(this.mnJobKey_Click);
            this.mnJobKey23.Name = "mnJobKey23";
            manager.ApplyResources(this.mnJobKey23, "mnJobKey23");
            this.mnJobKey23.Click += new EventHandler(this.mnJobKey_Click);
            this.mnJobKey24.Name = "mnJobKey24";
            manager.ApplyResources(this.mnJobKey24, "mnJobKey24");
            this.mnJobKey24.Click += new EventHandler(this.mnJobKey_Click);
            this.mnDebug.DropDownItems.AddRange(new ToolStripItem[] { this.mnRecvText, this.mnInputCheck, this.mnControlChange });
            this.mnDebug.Name = "mnDebug";
            manager.ApplyResources(this.mnDebug, "mnDebug");
            this.mnRecvText.Name = "mnRecvText";
            manager.ApplyResources(this.mnRecvText, "mnRecvText");
            this.mnRecvText.Click += new EventHandler(this.mnRecvText_Click);
            this.mnInputCheck.Checked = true;
            this.mnInputCheck.CheckOnClick = true;
            this.mnInputCheck.CheckState = CheckState.Checked;
            this.mnInputCheck.Name = "mnInputCheck";
            manager.ApplyResources(this.mnInputCheck, "mnInputCheck");
            this.mnInputCheck.Click += new EventHandler(this.mnInputCheck_Click);
            this.mnControlChange.DropDownItems.AddRange(new ToolStripItem[] { this.mnControlSS, this.mnControlMS, this.mnControlGS, this.mnControlWS });
            this.mnControlChange.Name = "mnControlChange";
            manager.ApplyResources(this.mnControlChange, "mnControlChange");
            this.mnControlSS.Checked = true;
            this.mnControlSS.CheckState = CheckState.Checked;
            this.mnControlSS.Name = "mnControlSS";
            manager.ApplyResources(this.mnControlSS, "mnControlSS");
            this.mnControlSS.Click += new EventHandler(this.mnControlSS_Click);
            this.mnControlMS.CheckOnClick = true;
            this.mnControlMS.Name = "mnControlMS";
            manager.ApplyResources(this.mnControlMS, "mnControlMS");
            this.mnControlMS.Click += new EventHandler(this.mnControlMS_Click);
            this.mnControlGS.CheckOnClick = true;
            this.mnControlGS.Name = "mnControlGS";
            manager.ApplyResources(this.mnControlGS, "mnControlGS");
            this.mnControlGS.Click += new EventHandler(this.mnControlGS_Click);
            this.mnControlWS.CheckOnClick = true;
            this.mnControlWS.Name = "mnControlWS";
            manager.ApplyResources(this.mnControlWS, "mnControlWS");
            this.mnControlWS.Click += new EventHandler(this.mnControlWS_Click);
            this.ofdMain.DefaultExt = "txt";
            manager.ApplyResources(this.ofdMain, "ofdMain");
            this.ofdMain.RestoreDirectory = true;
            this.tmrJobConnect.Tick += new EventHandler(this.tmrJobConnect_Tick);
            this.tmrSendRecv.Tick += new EventHandler(this.tmrSendRecv_Tick);
            manager.ApplyResources(this.uJobMenu1, "uJobMenu1");
            this.uJobMenu1.Name = "uJobMenu1";
            manager.ApplyResources(this.uJobInput1, "uJobInput1");
            this.uJobInput1.Name = "uJobInput1";
            manager.ApplyResources(this, "$this");
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.tlcMain);
            base.Controls.Add(this.mnMainMenu);
            base.KeyPreview = true;
            base.Name = "MainBase";
            base.Deactivate += new EventHandler(this.MainBase_Deactivate);
            base.FormClosing += new FormClosingEventHandler(this.MainBase_FormClosing);
            base.Load += new EventHandler(this.MainBase_Load);
            base.KeyDown += new KeyEventHandler(this.MainBase_KeyDown);
            base.KeyUp += new KeyEventHandler(this.MainBase_KeyUp);
            this.tlcMain.BottomToolStripPanel.ResumeLayout(false);
            this.tlcMain.BottomToolStripPanel.PerformLayout();
            this.tlcMain.ContentPanel.ResumeLayout(false);
            this.tlcMain.TopToolStripPanel.ResumeLayout(false);
            this.tlcMain.TopToolStripPanel.PerformLayout();
            this.tlcMain.ResumeLayout(false);
            this.tlcMain.PerformLayout();
            this.tslFunction.ResumeLayout(false);
            this.tslFunction.PerformLayout();
            this.stsMain.ResumeLayout(false);
            this.stsMain.PerformLayout();
            this.pnlSupportWindow.ResumeLayout(false);
            this.pnlSupportWindow.PerformLayout();
            this.pnlJobMenu.ResumeLayout(false);
            this.pnlJobInput.ResumeLayout(false);
            this.pnlJobInput.PerformLayout();
            this.tslStandard.ResumeLayout(false);
            this.tslStandard.PerformLayout();
            this.tslJob.ResumeLayout(false);
            this.tslJob.PerformLayout();
            this.mnMainMenu.ResumeLayout(false);
            this.mnMainMenu.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private string Job_OnAppend(object sender, IData Data)
        {
            if (this.mnCloseOnAppend.Checked && !this.BatchSendFlag)
            {
                ((CommonJobForm) sender).Close();
            }
            Data.Header.DataInfo = "";
            Data.Header.SendGuard = "";
            Data.Header.DataType = "";
            Data.TimeStamp = DateTime.Now;
            if (Data.ID == null)
            {
                return this.idv.AppendJobData(Data, 0, true, false, false);
            }
            return this.idv.UpdateJobData(Data);
        }

        private void Job_OnCreateLinkForm(object sender, IData data, string linkFileName, int linkNo)
        {
            this.JobFormOpen(data, linkFileName, linkNo);
        }

        public virtual string Job_OnCreateNewForm(object sender, IData data)
        {
            ((CommonJobForm) sender).Close();
            if (data.JobCode.Trim() == "")
            {
                this.JobError(-1);
                return data.ID;
            }
            if (!this.SearchDispCode(data))
            {
                UJobInputDlg dlg = new UJobInputDlg();
                dlg.SetCodeSelect(data.JobCode.Trim());
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    dlg.Close();
                    dlg.Dispose();
                    return data.ID;
                }
                data.Header.DispCode = dlg.DispCode;
                dlg.Close();
                dlg.Dispose();
            }
            this.JobFormOpen(data);
            return data.ID;
        }

        public void Job_OnOpenJobForm(object sender)
        {
            this.mnJobNew_Click(this, null);
        }

        public void Job_OnOptionShow(object sender)
        {
            if (!this.bOptionShowingFlag)
            {
                this.mnSystem_Click(this, null);
            }
        }

        public virtual void Job_OnRepeatSend(object sender)
        {
        }

        public void Job_OnRepetInput(object sender)
        {
            ((CommonJobForm) sender).Close();
            this.mnRedo_Click(this, null);
        }

        public virtual string Job_OnSend(object sender, IData Data)
        {
            return "";
        }

        private void Job_OnSeqLoad(object sender)
        {
            this.SeqLoad(sender);
        }

        public virtual void JobConnectSet()
        {
        }

        protected void JobError(int errint)
        {
            string messageCode = "";
            switch (errint)
            {
                case -12:
                    messageCode = "E512";
                    break;

                case -11:
                    messageCode = "E511";
                    break;

                case -9:
                    messageCode = "E509";
                    break;

                case -8:
                    messageCode = "E508";
                    break;

                case -7:
                    messageCode = "E507";
                    break;

                case -6:
                    messageCode = "E506";
                    break;

                case -5:
                    messageCode = "E505";
                    break;

                case -4:
                    messageCode = "E504";
                    break;

                case -3:
                    messageCode = "E503";
                    break;

                case -2:
                    messageCode = "E502";
                    break;

                case -1:
                    messageCode = "E501";
                    break;

                default:
                    messageCode = "E510";
                    break;
            }
            if (messageCode != "")
            {
                MessageDialog dialog = new MessageDialog();
                dialog.ShowMessage(messageCode, "");
                dialog.Dispose();
            }
        }

        public object JobFormOpen(IData data)
        {
            return this.JobFormOpen(-1, data.JobCode, data.DispCode, data, null, "", -1, true, false);
        }

        public object JobFormOpen(int key)
        {
            return this.JobFormOpen(key, "", "", null, null, "", -1, true, false);
        }

        public object JobFormOpen(IData data, bool ShowFlag)
        {
            return this.JobFormOpen(-1, data.JobCode, data.DispCode, data, null, "", -1, ShowFlag, false);
        }

        public object JobFormOpen(IData data, object sendjobsender)
        {
            return this.JobFormOpen(-1, data.JobCode, data.DispCode, data, sendjobsender, "", -1, true, false);
        }

        public object JobFormOpen(int key, bool JobFormFlag)
        {
            return this.JobFormOpen(key, "", "", null, null, "", -1, true, JobFormFlag);
        }

        public object JobFormOpen(string JobCode, string DispCode)
        {
            return this.JobFormOpen(-1, JobCode, DispCode, null, null, "", -1, true, false);
        }

        public object JobFormOpen(IData data, string linkFileName, int linkNo)
        {
            return this.JobFormOpen(-1, data.JobCode, data.DispCode, data, null, linkFileName, linkNo, true, false);
        }

        public object JobFormOpen(int key, IData data, object sendjobsender)
        {
            return this.JobFormOpen(key, data.JobCode, data.DispCode, data, sendjobsender, "", -1, true, false);
        }

        private object JobFormOpen(int key, string JobCode, string DispCode, IData JobOpenData, object sendjobsender, string linkFileName, int linkNo, bool ShowFlag, bool JobFormFlag)
        {
            object obj3;
            if (this.sysenv.TerminalInfo.Debug)
            {
                ULogClass.WriteProcessInfo("Before Screen Displaying JobCode:[" + JobCode + "]");
            }
            CommonJobForm form = null;
            IData odata = null;
            IData rdata = null;
            object obj2 = null;
            char[] anyOf = new char[] { 'R', 'C', 'M' };
            bool flgSetColor = false;
            Cursor cursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                IData data;
                if (key > -1)
                {
                    data = this.idv.GetData(key);
                    if (data == null)
                    {
                        return obj2;
                    }
                    if (data.Status != DataStatus.Recept)
                    {
                        form = JobFormFactory.CreateNewForm(data.Header.JobCode.Trim(), data.Header.DispCode.Trim(), false, JobFormFlag);
                    }
                    else
                    {
                        if ((!JobFormFlag && ShowFlag) && (data.Header.DataType.IndexOfAny(anyOf) < 0))
                        {
                            if (sendjobsender == null)
                            {
                                data = this.ContainedSet(data);
                                this.pr.PrintPreview(this, key.ToString(), data);
                            }
                            return obj2;
                        }
                        this.RecvDataSet(data, out odata, out rdata);
                        if (((odata != null) & (rdata != null)) && (((sendjobsender != null) & (rdata.Status == DataStatus.Recept)) & (odata.Status != DataStatus.Recept)))
                        {
                            flgSetColor = true;
                        }
                        if (sendjobsender != null)
                        {
                            if (odata != null)
                            {
                                if (odata.Status == DataStatus.Recept)
                                {
                                    form = JobFormFactory.CreateRecvForm(odata, JobFormFlag);
                                }
                                else if (flgSetColor)
                                {
                                    form = (CommonJobForm) sendjobsender;
                                }
                                else
                                {
                                    form = JobFormFactory.CreateNewForm(odata.JobCode.Trim(), odata.DispCode.Trim(), false, JobFormFlag);
                                }
                            }
                            else
                            {
                                form = JobFormFactory.CreateEmptyForm();
                            }
                        }
                        else if (odata != null)
                        {
                            if (odata.Status == DataStatus.Recept)
                            {
                                form = JobFormFactory.CreateRecvForm(odata, JobFormFlag);
                            }
                            else
                            {
                                form = JobFormFactory.CreateNewForm(odata.Header.JobCode.Trim(), odata.Header.DispCode.Trim(), false, JobFormFlag);
                            }
                        }
                        else
                        {
                            form = JobFormFactory.CreateEmptyForm();
                        }
                    }
                }
                else
                {
                    if (JobOpenData != null)
                    {
                        data = JobOpenData;
                    }
                    else
                    {
                        data = null;
                    }
                    if ((key < 0) && (sendjobsender != null))
                    {
                        try
                        {
                            ((CommonJobForm) sendjobsender).SetSeqSendData(data);
                            return sendjobsender;
                        }
                        catch
                        {
                            this.JobError(-12);
                            return sendjobsender;
                        }
                    }
                    form = JobFormFactory.CreateNewForm(JobCode.Trim(), DispCode.Trim(), true, JobFormFlag);
                }
                if ((form != null) && !flgSetColor)
                {
                    if (sendjobsender != null)
                    {
                        ((CommonJobForm) sendjobsender).Close();
                        ((CommonJobForm) sendjobsender).Dispose();
                    }
                    form.OnRepetInput += new JobHandler(this.Job_OnRepetInput);
                    form.OnAppend += new JobDemHandler(this.Job_OnAppend);
                    form.OnSend += new JobDemHandler(this.Job_OnSend);
                    form.OnCreateNewForm += new JobDemHandler(this.Job_OnCreateNewForm);
                    form.OnCreateLinkForm += new JobLinkHandler(this.Job_OnCreateLinkForm);
                    form.OnSeqLoad += new JobHandler(this.Job_OnSeqLoad);
                    form.OnRepeatSend += new JobHandler(this.Job_OnRepeatSend);
                    form.OnOpenJobForm += new JobHandler(this.Job_OnOpenJobForm);
                    form.OnOptionShow += new JobHandler(this.Job_OnOptionShow);
                }
                if (JobFormFactory.ErrorCode != 0)
                {
                    this.JobError(JobFormFactory.ErrorCode);
                    JobFormFactory.ErrorCode = 0;
                    goto Label_070F;
                }
                if (key < 0)
                {
                    AbstractJobConfig config;
                    int num;
                    string str;
                    if (DispCode.Trim() != "")
                    {
                        num = JobConfigFactory.createJobConfig(JobCode.Trim(), DispCode.Trim(), DateTime.Now, out config);
                        str = JobCode.Trim() + "." + DispCode.Trim();
                    }
                    else
                    {
                        num = JobConfigFactory.createJobConfig(JobCode.Trim(), DateTime.Now, out config);
                        str = JobCode.Trim();
                    }
                    if (num < 0)
                    {
                        this.JobError(num);
                    }
                    switch (config.jobStyle)
                    {
                        case JobStyle.normal:
                        {
                            NormalConfig config2 = (NormalConfig) config;
                            str = str + " " + config2.record.titile1;
                            this.HJobList = this.HistoryListAdd(str, this.HJobList, 30);
                            this.uJobInput1.HistoryJobListSet(this.HJobList);
                            this.uJobMenu1.HistoryJobListSet(this.HJobList);
                            goto Label_04D0;
                        }
                        case JobStyle.combi:
                        {
                            CombiConfig config4 = (CombiConfig) config;
                            str = str + " " + config4.title;
                            this.HJobList = this.HistoryListAdd(str, this.HJobList, 30);
                            this.uJobInput1.HistoryJobListSet(this.HJobList);
                            this.uJobMenu1.HistoryJobListSet(this.HJobList);
                            goto Label_04D0;
                        }
                        case JobStyle.divide:
                        {
                            DivideConfig config3 = (DivideConfig) config;
                            str = str + " " + config3.record.titile1;
                            this.HJobList = this.HistoryListAdd(str, this.HJobList, 30);
                            this.uJobInput1.HistoryJobListSet(this.HJobList);
                            this.uJobMenu1.HistoryJobListSet(this.HJobList);
                            goto Label_04D0;
                        }
                    }
                    this.JobError(-99);
                }
            Label_04D0:
                if (data == null)
                {
                    goto Label_05BF;
                }
                if (data.Status != DataStatus.Recept)
                {
                    goto Label_0569;
                }
                if (odata != null)
                {
                    if (odata.Status == DataStatus.Recept)
                    {
                        try
                        {
                            form.SetRecvData(odata);
                            goto Label_0542;
                        }
                        catch
                        {
                            this.JobError(-12);
                            return form;
                        }
                    }
                    if (!flgSetColor)
                    {
                        odata.Header.IndexInfo = "";
                        try
                        {
                            form.SetSendData(odata);
                            goto Label_0542;
                        }
                        catch
                        {
                            this.JobError(-12);
                            return form;
                        }
                    }
                    form.Status = odata.Status;
                }
            Label_0542:
                if ((rdata == null) || form.IsDisposed)
                {
                    goto Label_05BF;
                }
                try
                {
                    form.SetRes(rdata, flgSetColor);
                    goto Label_05BF;
                }
                catch
                {
                    this.JobError(-11);
                    return form;
                }
            Label_0569:
                if (linkFileName == "")
                {
                    data.Header.IndexInfo = "";
                    try
                    {
                        form.SetSendData(data);
                        goto Label_05BF;
                    }
                    catch
                    {
                        this.JobError(-12);
                        return form;
                    }
                }
                try
                {
                    form.SetLinkData(data, linkFileName, linkNo);
                }
                catch
                {
                    this.JobError(-12);
                    return form;
                }
            Label_05BF:
                if (ShowFlag)
                {
                    try
                    {
                        if (!form.IsDisposed)
                        {
                            if (this.sysenv.TerminalInfo.UserKind != 4)
                            {
                                form.UserCode = this.UserCode;
                            }
                            form.Show();
                            if (key > -1)
                            {
                                if (data.Header.DataType == "M")
                                {
                                    if (((data.Status == DataStatus.Recept) & !data.IsRead) && (data.Header.DataType.IndexOfAny(anyOf) > -1))
                                    {
                                        this.idv.OpenStatusChange(key.ToString());
                                    }
                                }
                                else
                                {
                                    if (((odata != null) && ((odata.Status == DataStatus.Recept) & !odata.IsRead)) && (odata.Header.DataType.IndexOfAny(anyOf) > -1))
                                    {
                                        this.idv.OpenStatusChange(odata.ID);
                                    }
                                    if (((rdata != null) && ((rdata.Status == DataStatus.Recept) & !rdata.IsRead)) && (rdata.Header.DataType.IndexOfAny(anyOf) > -1))
                                    {
                                        this.idv.OpenStatusChange(rdata.ID);
                                    }
                                }
                                if (((data.Status == DataStatus.Sent) || (data.Status == DataStatus.Send)) && !data.IsRead)
                                {
                                    this.idv.OpenStatusChange(key.ToString());
                                }
                            }
                        }
                    }
                    catch
                    {
                        this.JobError(-99);
                    }
                }
                obj2 = form;
            Label_070F:
                obj3 = obj2;
            }
            finally
            {
                this.Cursor = cursor;
                if (this.sysenv.TerminalInfo.Debug)
                {
                    ULogClass.WriteProcessInfo("After Screen Displaying");
                }
            }
            return obj3;
        }

        private void JobKeySet()
        {
            UserKey key = UserKey.CreateInstance();
            for (int i = 0; i < this.mnJobKey.DropDownItems.Count; i++)
            {
                ToolStripMenuItem item = (ToolStripMenuItem) this.mnJobKey.DropDownItems[i];
                item.Text = "";
                item.ShortcutKeys = Keys.None;
            }
            for (int j = 0; j < key.JobKeyList[0].KeyList.Count; j++)
            {
                ToolStripMenuItem item2 = (ToolStripMenuItem) this.mnJobKey.DropDownItems[j];
                if (key.JobKeyList[0].KeyList[j].Name != "")
                {
                    item2.Text = key.JobKeyList[0].KeyList[j].Name;
                    KeysConverter converter = new KeysConverter();
                    item2.ShortcutKeys = (Keys) converter.ConvertFromInvariantString(key.JobKeyList[0].KeyList[j].Sckey);
                }
                else
                {
                    item2.Text = "";
                    item2.ShortcutKeys = Keys.None;
                }
            }
            this.uJobMenu1.JobKeySet();
        }

        private void JobWindow_OnJobOpen(object sender, string JobCode, string DispCode)
        {
            this.JobFormOpen(JobCode, DispCode);
        }

        private void LoadUserApplication()
        {
            if ((this.usr_apc.Left > -1) && (this.usr_apc.Top > -1))
            {
                base.Location = new Point(this.usr_apc.Left, this.usr_apc.Top);
                base.Size = new Size(this.usr_apc.Right - this.usr_apc.Left, this.usr_apc.Bottom - this.usr_apc.Top);
            }
            this.pnlSupportWindow.Width = this.usr_apc.MainSplitter;
            this.idv.SetSplitter(this.usr_apc.DataViewSplitter);
            if (this.usr_apc.Maximized)
            {
                base.WindowState = FormWindowState.Maximized;
            }
            this.mnCloseOnAppend.Checked = this.usr_apc.CloseOnAppend;
            this.mnListRestriction.Checked = this.usr_apc.ListRestriction;
            switch (this.usr_apc.DataViewFontSize)
            {
                case 0:
                    this.DataViewFontSize(2);
                    break;

                case 1:
                    this.DataViewFontSize(0);
                    break;

                default:
                    this.DataViewFontSize(1);
                    break;
            }
            this.mnLogoffConf.Checked = this.usr_apc.LogoffConfirm;
            this.mnToolbarStandard.Checked = this.usr_tlb.Standard;
            this.mnToolbarJob.Checked = this.usr_tlb.Job;
            this.mnFunctionBar.Checked = this.usr_tlb.FunctionBar;
            this.mnLogonWindow.Checked = this.usr_wnd.UserInput;
            this.mnJobInputWindow.Checked = this.usr_wnd.JobInput;
            this.mnJobMenuWindow.Checked = this.usr_wnd.JobMenu;
            this.mnDataViewWindow.Checked = this.usr_wnd.DataView;
            this.idv.SetDataViewColum(this.usr_cwc);
            this.mnVisibleCount.Checked = this.usr_dvf.ReadVisible;
            this.idv.SetOpenViewFlag(this.mnVisibleCount.Checked);
            this.mnQueryFieldClear.Checked = this.usr_jpc.QueryFieldClear;
            this.mnInputCheck.Checked = this.usrenv.DebugFunction.Check_Off;
            this.WindowCheck();
            this.ToolbarCheck();
        }

        public virtual void MailRecv_Click(object sender, EventArgs e)
        {
        }

        public virtual void MailSendRecv_Click(object sender, EventArgs e)
        {
        }

        private void MainBase_Deactivate(object sender, EventArgs e)
        {
            this.FunctionSet(0);
        }

        private void MainBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.bMailActionFlag = true;
            if (!this.VersionUpFlag && (this.idv.GetCount(3) > 0))
            {
                MessageDialog dialog = new MessageDialog();
                DialogResult result = dialog.ShowMessage("C403", "");
                if (result == DialogResult.Yes)
                {
                    this.idv.ClearDelete();
                }
                else if (result != DialogResult.No)
                {
                    e.Cancel = true;
                    dialog.Dispose();
                    this.bMailActionFlag = false;
                    return;
                }
                dialog.Dispose();
            }
            try
            {
                this.DisposingFlag = true;
                if (this.DsArrangeManager != null)
                {
                    this.DsArrangeManager.Dispose();
                }
                this.BackupFileDelete();
                this.SaveUserApplication();
                this.usr.Save();
                this.msgc.Save();
                this.idv.DataViewListSave("");
            }
            catch (Exception exception)
            {
                MessageDialog dialog2 = new MessageDialog();
                dialog2.ShowMessage("E304", Naccs.Core.Properties.Resources.ResourceManager.GetString("CORE106"), null, exception);
                dialog2.Close();
                dialog2.Dispose();
            }
            ULogClass.LogWrite("=====< Log End >=====");
        }

        private void MainBase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                this.FunctionSet(1);
            }
            else if (e.Shift)
            {
                this.FunctionSet(2);
            }
        }

        private void MainBase_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                this.FunctionSet(1);
            }
            else if (e.Shift)
            {
                this.FunctionSet(2);
            }
            else
            {
                this.FunctionSet(0);
            }
        }

        private void MainBase_Load(object sender, EventArgs e)
        {
            if (!this.isDesigning)
            {
                this.DiskCheck();
                this.DataInfoCnt = 0;
                this.usr = User.CreateInstance();
                this.HJobList = this.usr.HistoryJobsList;
                this.HUserList = this.usr.HistoryUserList;
                this.HMailBoxList = this.usr.HistoryAddressList;
                this.usr_apc = this.usr.Application;
                this.usr_tlb = this.usr.ToolBar;
                this.usr_wnd = this.usr.Window;
                this.usr_cwc = this.usr.ColumnWidth;
                this.usr_dvf = this.usr.DataViewForm;
                this.usr_jpc = this.usr.JobOption;
                this.usrenv = UserEnvironment.CreateInstance();
                this.OptionSet(true);
                this.idv.RecvFolderSelect();
                this.FldList = this.msgc.Folders;
                if (this.idv.GetCount(4, this.usrenv.TerminalInfo.SaveTerm) > 0)
                {
                    MessageDialog dialog = new MessageDialog {
                        TopMost = true
                    };
                    if (dialog.ShowMessage("C402", this.usrenv.TerminalInfo.SaveTerm.ToString(), "") == DialogResult.Yes)
                    {
                        this.idv.SaveDateDataDelete(this.usrenv.TerminalInfo.SaveTerm);
                    }
                    dialog.Close();
                    dialog.Dispose();
                    this.uopd.Show();
                    Application.DoEvents();
                }
                this.uJobInput1.HistoryJobListSet(this.HJobList);
                this.uJobMenu1.HistoryJobListSet(this.HJobList);
                this.dscl = new DispCodeList();
                this.LoadUserApplication();
                this.mnJobKey.Visible = false;
                int userKind = this.sysenv.TerminalInfo.UserKind;
                if ((this.sysenv.TerminalInfo.UserKind != 4) && !this.sysenv.TerminalInfo.Receiver)
                {
                    this.DsArrangeManager = new ArrangeManager(this, this.idv);
                    this.DsArrangeManager.AppendArrangeMenu(this.mnOption);
                    this.DsArrangeManager.AutoRun();
                }
                this.tbbJetras.Visible = this.sysenv.TerminalInfo.Jetras;
                this.tbbJSplitter5.Visible = this.sysenv.TerminalInfo.Jetras;
                this.DisposingFlag = false;
                this.swtbl = new SWCheckTbl();
                ULogClass.LogWrite("=====< Log Start >=====");
                this.StatusChange(0);
                this.UpdateMenuItems();
                this.uopd.Close();
                this.uopd.Dispose();
            }
        }

        private void mnACLCustomize_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Path.Combine(this.pi.CustomizeRoot, "CustomizeACL.exe")))
                {
                    Process.Start(Path.Combine(this.pi.CustomizeRoot, "CustomizeACL.exe"), ((int) this.sysenv.TerminalInfo.Protocol).ToString());
                }
                else
                {
                    MessageDialog dialog = new MessageDialog();
                    dialog.ShowMessage("E701", "");
                    dialog.Close();
                    dialog.Dispose();
                }
            }
            catch (Exception exception)
            {
                MessageDialog dialog2 = new MessageDialog();
                dialog2.ShowMessage("E701", "", null, exception);
                dialog2.Close();
                dialog2.Dispose();
            }
        }

        private void mnAddRecord_Click(object sender, EventArgs e)
        {
            this.idv.AppendRecord();
        }

        private void mnAllSelect_Click(object sender, EventArgs e)
        {
            this.idv.AllSelect();
        }

        public virtual void mnBatchSend_Click(object sender, EventArgs e)
        {
        }

        public virtual void mnBatchTake_Click(object sender, EventArgs e)
        {
        }

        public virtual void mnBatchTakeAgain_Click(object sender, EventArgs e)
        {
        }

        private void mnClearDelete_Click(object sender, EventArgs e)
        {
            MessageDialog dialog = new MessageDialog();
            if (dialog.ShowMessage("C401", "") == DialogResult.Yes)
            {
                this.idv.ClearDelete();
            }
            dialog.Dispose();
        }

        private void mnControlGS_Click(object sender, EventArgs e)
        {
            this.SetControl(2);
        }

        private void mnControlMS_Click(object sender, EventArgs e)
        {
            this.SetControl(1);
        }

        private void mnControlSS_Click(object sender, EventArgs e)
        {
            this.SetControl(0);
        }

        private void mnControlWS_Click(object sender, EventArgs e)
        {
            this.SetControl(3);
        }

        private void mnCreateFolder_Click(object sender, EventArgs e)
        {
            this.idv.FolderCreate();
        }

        private void mnDataDelete_Click(object sender, EventArgs e)
        {
            this.idv.DataDelete(-1);
        }

        private void mnDataViewExport_Click(object sender, EventArgs e)
        {
            this.idv.DataExport();
        }

        private void mnDataViewImport_Click(object sender, EventArgs e)
        {
            this.idv.DataImport();
        }

        private void mnDataViewRepair_Click(object sender, EventArgs e)
        {
            this.idv.ViewRepair(false);
        }

        private void mnDataViewRestore_Click(object sender, EventArgs e)
        {
            this.idv.ViewRestore();
        }

        private void mnDataViewWindow_Click(object sender, EventArgs e)
        {
            this.pnlDataView.Visible = this.mnDataViewWindow.Checked;
            if (!this.pnlDataView.Visible)
            {
                this.pnlSupportWindow.Dock = DockStyle.Fill;
            }
            else
            {
                this.pnlSupportWindow.Dock = DockStyle.Left;
            }
        }

        private void mnDeleteFolde_Click(object sender, EventArgs e)
        {
            this.idv.FolderDelete();
        }

        private void mnDistribute_Click(object sender, EventArgs e)
        {
            this.idv.Distribute(false);
        }

        public virtual void mnDISYG_Click(object sender, EventArgs e)
        {
        }

        private void mnExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void mnFileDataViewAppend_Click(object sender, EventArgs e)
        {
            this.ofdMain.Multiselect = true;
            this.ofdMain.InitialDirectory = this.GetInitialDirectory();
            if (this.ofdMain.ShowDialog() == DialogResult.OK)
            {
                DeleteRestore restore = new DeleteRestore(3);
                restore.Show();
                restore.pgbRestore.Minimum = 0;
                restore.pgbRestore.Maximum = this.ofdMain.FileNames.Length;
                try
                {
                    int num = 0;
                    foreach (string str in this.ofdMain.FileNames)
                    {
                        IData data;
                        num++;
                        restore.pgbRestore.Value = num;
                        restore.Refresh();
                        try
                        {
                            data = DataFactory.LoadFromEdiFile(str);
                        }
                        catch (Exception exception)
                        {
                            MessageDialog dialog = new MessageDialog();
                            dialog.ShowMessage("E302", str, null, exception);
                            dialog.Dispose();
                            return;
                        }
                        if (data.Header.OutCode.Trim() != "")
                        {
                            this.idv.AppendJobData(data, 2, false, false, false);
                        }
                        else
                        {
                            using (MessageDialog dialog2 = new MessageDialog())
                            {
                                dialog2.ShowMessage("W406", null, null);
                            }
                        }
                    }
                }
                finally
                {
                    restore.Close();
                    restore.Dispose();
                    this.idv.DataViewSaveRepaint();
                }
            }
        }

        private void mnFileOpen_Click(object sender, EventArgs e)
        {
            this.ofdMain.Multiselect = false;
            this.ofdMain.InitialDirectory = this.GetInitialDirectory();
            if (this.ofdMain.ShowDialog() == DialogResult.OK)
            {
                IData data = null;
                try
                {
                    data = DataFactory.LoadFromEdiFile(this.ofdMain.FileName);
                }
                catch (Exception exception)
                {
                    MessageDialog dialog = new MessageDialog();
                    dialog.ShowMessage("E302", this.ofdMain.FileName, null, exception);
                    dialog.Close();
                    dialog.Dispose();
                    return;
                }
                if (!(data.JobCode.Trim() == ""))
                {
                    if (!this.SearchDispCode(data))
                    {
                        UJobInputDlg dlg = new UJobInputDlg();
                        dlg.SetCodeSelect(data.JobCode.Trim());
                        if (dlg.ShowDialog() != DialogResult.OK)
                        {
                            dlg.Close();
                            dlg.Dispose();
                            return;
                        }
                        data.Header.DispCode = dlg.DispCode;
                        dlg.Close();
                        dlg.Dispose();
                    }
                    this.JobFormOpen(data);
                }
                else
                {
                    this.JobError(-1);
                }
            }
        }

        private void mnFileSave_Click(object sender, EventArgs e)
        {
            this.idv.SaveAsFile();
        }

        public virtual void mnFileSend_Click(object sender, EventArgs e)
        {
        }

        private void mnFontDefault_Click(object sender, EventArgs e)
        {
            this.DataViewFontSize(1);
        }

        private void mnFontLarge_Click(object sender, EventArgs e)
        {
            this.DataViewFontSize(2);
        }

        private void mnFontSmall_Click(object sender, EventArgs e)
        {
            this.DataViewFontSize(0);
        }

        private void mnFunctionBar_Click(object sender, EventArgs e)
        {
            this.ToolbarCheck();
        }

        private void mnGuidance_Click(object sender, EventArgs e)
        {
            this.HtmlShow(this.pi.GymGuidelines);
        }

        private void mnHardCopyPreview_Click(object sender, EventArgs e)
        {
            this.mnHardCopyPreview.Enabled = false;
            try
            {
                this.pr.HardCopyPrintPreview(this);
            }
            finally
            {
                this.mnHardCopyPreview.Enabled = true;
            }
        }

        private void mnHardCopyPrint_Click(object sender, EventArgs e)
        {
            this.mnHardCopyPrint.Enabled = false;
            try
            {
                this.pr.HardCopyPrint(this);
            }
            finally
            {
                this.mnHardCopyPrint.Enabled = true;
            }
        }

        private void mnInputCheck_Click(object sender, EventArgs e)
        {
            this.usrenv.DebugFunction.Check_Off = this.mnInputCheck.Checked;
            this.usrenv.Save();
        }

        private void mnJobInputWindow_Click(object sender, EventArgs e)
        {
            this.pnlJobInput.Visible = this.mnJobInputWindow.Checked;
            this.SupportWindowCheck();
        }

        private void mnJobKey_Click(object sender, EventArgs e)
        {
            string text = ((ToolStripMenuItem) sender).Text;
            string dispCode = "";
            if (text.IndexOf(" ") > -1)
            {
                text = text.Remove(text.IndexOf(" "));
            }
            if (text.IndexOf(".") > -1)
            {
                string str3 = text;
                int index = str3.IndexOf(".");
                text = str3.Substring(0, index);
                dispCode = str3.Remove(0, index + 1);
            }
            this.JobFormOpen(text, dispCode);
        }

        private void mnJobMenuWindow_Click(object sender, EventArgs e)
        {
            this.pnlJobMenu.Visible = this.mnJobMenuWindow.Checked;
            this.SupportWindowCheck();
        }

        private void mnJobMessage_Click(object sender, EventArgs e)
        {
            string gymErrIndexHtml = HelpSettings.CreateInstance().HelpPath.GymErrIndexHtml;
            this.HtmlShow(gymErrIndexHtml);
        }

        private void mnJobNew_Click(object sender, EventArgs e)
        {
            string jobCode;
            string dispCode;
            UJobInputDlg dlg = new UJobInputDlg();
            dlg.HistoryJobListSet(this.HJobList);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                jobCode = dlg.JobCode;
                dispCode = dlg.DispCode;
                dlg.Close();
                dlg.Dispose();
            }
            else
            {
                dlg.Close();
                dlg.Dispose();
                return;
            }
            this.JobFormOpen(jobCode, dispCode);
        }

        private void mnJobOpen_Click(object sender, EventArgs e)
        {
            this.idv.JobOpen();
        }

        public virtual void mnLogoff_Click(object sender, EventArgs e)
        {
        }

        public virtual void mnLogon_Click(object sender, EventArgs e)
        {
        }

        private void mnLogonWindow_Click(object sender, EventArgs e)
        {
            this.pnlUserInput.Visible = this.mnLogonWindow.Checked;
            this.SupportWindowCheck();
        }

        private void mnNACCSBoard_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.sysenv.TerminalInfo.URL))
            {
                Process.Start(this.sysenv.TerminalInfo.URL);
            }
        }

        private void mnNetworkTest_Click(object sender, EventArgs e)
        {
            NetworkTestDialog dialog = new NetworkTestDialog();
            dialog.ShowDialog((TestServer) int.Parse(((ToolStripMenuItem) sender).Tag.ToString()));
            dialog.Close();
            dialog.Dispose();
        }

        private void mnOpen_Click(object sender, EventArgs e)
        {
            if (this.idv.GetKeyID() > -1)
            {
                this.JobFormOpen(this.idv.GetKeyID());
            }
        }

        public virtual void mnOtherTerm_Click(object sender, EventArgs e)
        {
        }

        private void mnPackageMessage_Click(object sender, EventArgs e)
        {
            string termMsgIndexHtml = HelpSettings.CreateInstance().HelpPath.TermMsgIndexHtml;
            this.HtmlShow(termMsgIndexHtml);
        }

        private void mnPortalBoard_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.sysenv.TerminalInfo.PortalURL))
            {
                Process.Start(this.sysenv.TerminalInfo.PortalURL);
            }
        }

        private void mnPrintDataView_Click(object sender, EventArgs e)
        {
            this.mnPrintDataView.Enabled = false;
            try
            {
                IData data = DataFactory.CreateInstance();
                string dataViewListData = "";
                dataViewListData = this.idv.GetDataViewListData();
                if (dataViewListData != "")
                {
                    data.JobData = dataViewListData;
                }
                this.pr.DataviewPrint(this, data);
            }
            finally
            {
                this.mnPrintDataView.Enabled = true;
            }
        }

        private void mnPrintDataViewPreview_Click(object sender, EventArgs e)
        {
            this.mnPrintDataViewPreview.Enabled = false;
            try
            {
                IData data = DataFactory.CreateInstance();
                string dataViewListData = "";
                dataViewListData = this.idv.GetDataViewListData();
                if (dataViewListData != "")
                {
                    data.JobData = dataViewListData;
                }
                this.pr.DataviewPrint(this, data, true);
            }
            finally
            {
                this.mnPrintDataViewPreview.Enabled = true;
            }
        }

        private void mnPrintJobData_Click(object sender, EventArgs e)
        {
            IData data;
            IData data2;
            IData data3;
            List<string> selectKeyList = this.idv.GetSelectKeyList();
            if (selectKeyList.Count == 1)
            {
                data = this.idv.GetData(int.Parse(selectKeyList[0]));
                if (data != null)
                {
                    if (data.Header.DataType == "M")
                    {
                        this.RecvDataSet(data, out data2, out data3);
                        data2 = this.ContainedSet(data2);
                        this.pr.Print(this, selectKeyList[0], data2, 0);
                    }
                    else
                    {
                        data = this.ContainedSet(data);
                        this.pr.Print(this, selectKeyList[0], data, 0);
                    }
                }
            }
            else if (selectKeyList.Count > 1)
            {
                for (int i = 0; i < selectKeyList.Count; i++)
                {
                    data = this.idv.GetData(int.Parse(selectKeyList[i]));
                    if (data != null)
                    {
                        if (data.Header.DataType == "M")
                        {
                            this.RecvDataSet(data, out data2, out data3);
                            data2 = this.ContainedSet(data2);
                            this.pr.Print(this, selectKeyList[i], data2, 1);
                        }
                        else
                        {
                            data = this.ContainedSet(data);
                            this.pr.Print(this, selectKeyList[i], data, 1);
                        }
                    }
                }
            }
        }

        private void mnPrintJobDataPreview_Click(object sender, EventArgs e)
        {
            this.mnPrintJobDataPreview.Enabled = false;
            try
            {
                IData data = this.idv.GetData(-1);
                IData odata = null;
                IData rdata = null;
                if (data != null)
                {
                    if (data.Header.DataType == "M")
                    {
                        this.RecvDataSet(data, out odata, out rdata);
                        odata = this.ContainedSet(odata);
                        this.pr.PrintPreview(this, this.idv.GetKeyID().ToString(), odata);
                    }
                    else
                    {
                        data = this.ContainedSet(data);
                        this.pr.PrintPreview(this, this.idv.GetKeyID().ToString(), data);
                    }
                }
            }
            finally
            {
                this.mnPrintJobDataPreview.Enabled = true;
            }
        }

        private void mnQueryFieldClear_Click(object sender, EventArgs e)
        {
            this.usr_jpc.QueryFieldClear = this.mnQueryFieldClear.Checked;
            this.usr.Save();
        }

        private void mnRecvSearch_Click(object sender, EventArgs e)
        {
            this.idv.RecvSearch();
        }

        private void mnRecvText_Click(object sender, EventArgs e)
        {
            int keyID = this.idv.GetKeyID();
            if (keyID > -1)
            {
                URecvTextDlg dlg = new URecvTextDlg {
                    txbRecvText = { Text = this.idv.GetData(keyID).GetDataString() }
                };
                dlg.ShowDialog();
                dlg.Close();
                dlg.Dispose();
            }
        }

        private void mnRedo_Click(object sender, EventArgs e)
        {
            if (this.HJobList.Count > 0)
            {
                string jobCode = this.HJobList[this.HJobList.Count - 1];
                string dispCode = "";
                if (jobCode.IndexOf(" ") > -1)
                {
                    jobCode = jobCode.Remove(jobCode.IndexOf(" "));
                }
                if (jobCode.IndexOf(".") > -1)
                {
                    string str3 = jobCode;
                    int index = str3.IndexOf(".");
                    jobCode = str3.Substring(0, index);
                    dispCode = str3.Remove(0, index + 1);
                }
                this.JobFormOpen(jobCode, dispCode);
            }
        }

        private void mnRenameFolder_Click(object sender, EventArgs e)
        {
            this.idv.FolderChangeName();
        }

        public virtual void mnRep_Click(object sender, EventArgs e)
        {
        }

        public virtual void mnREQ_Click(object sender, EventArgs e)
        {
        }

        private void mnSelectedDistribute_Click(object sender, EventArgs e)
        {
            this.idv.Distribute(true);
        }

        private void mnSendSearch_Click(object sender, EventArgs e)
        {
            this.idv.SendSearch();
        }

        private void mnSeqLoadDt_Click(object sender, EventArgs e)
        {
            this.SeqLoad(null);
        }

        private void mnSeqSend_Click(object sender, EventArgs e)
        {
            this.ofdMain.Multiselect = true;
            this.ofdMain.InitialDirectory = this.GetInitialDirectory();
            if (this.ofdMain.ShowDialog() == DialogResult.OK)
            {
                this.SeqInt = -1;
                this.SeqList = new List<string>();
                for (int i = 0; i < this.ofdMain.FileNames.Length; i++)
                {
                    this.SeqList.Add(this.ofdMain.FileNames[i]);
                }
                this.SeqLoad(null);
            }
        }

        private void mnSupport_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.sysenv.TerminalInfo.SupportURL))
            {
                Process.Start(this.sysenv.TerminalInfo.SupportURL);
            }
        }

        private void mnSystem_Click(object sender, EventArgs e)
        {
            this.TimerStop();
            this.bOptionShowingFlag = true;
            try
            {
                SettingDivType settingsDiv = this.usr.Application.SettingsDiv;
                string dataViewPath = this.pi.DataViewPath;
                if (this.DsArrangeManager != null)
                {
                    this.DsArrangeManager.Dispose();
                }
                using (OptionDialog dialog = (OptionDialog) this.CreateOption())
                {
                    dialog.IsLogOn = this.ComStatus > 0;
                    if (!dialog.IsLogOn)
                    {
                        this.idv.DataViewListSave(dataViewPath);
                    }
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        this.usr = User.CreateInstance();
                        this.pi = PathInfo.CreateInstance();
                        bool reloadFlag = settingsDiv != this.usr.Application.SettingsDiv;
                        if (!reloadFlag)
                        {
                            reloadFlag = this.pi.DataViewPath != dataViewPath;
                        }
                        this.OptionSet(reloadFlag);
                        this.OptionSetProc();
                        this.OptionSetInd();
                        this.idv.SetOpenViewFlag(this.mnVisibleCount.Checked);
                        this.idv.ViewCountCheck(true);
                        this.uJobMenu1.JobMenuSet();
                        this.UpdateMenuItems();
                        this.idv.DataViewRepaint(true);
                    }
                    else
                    {
                        this.usrenv = UserEnvironment.CreateInstance();
                        if (((this.usrenv.TerminalInfo.TermLogicalName == null) || (this.usrenv.TerminalInfo.TermLogicalName == "")) || ((this.sysenv.TerminalInfo.Protocol != ProtocolType.Mail) && ((this.usrenv.TerminalInfo.TermAccessKey == null) || (this.usrenv.TerminalInfo.TermAccessKey == ""))))
                        {
                            Environment.Exit(0);
                        }
                    }
                }
            }
            finally
            {
                this.bOptionShowingFlag = false;
            }
            this.TimerStart();
        }

        private void mnToolbarJob_Click(object sender, EventArgs e)
        {
            this.ToolbarCheck();
        }

        private void mnToolbarStandard_Click(object sender, EventArgs e)
        {
            this.ToolbarCheck();
        }

        private void mnUndo_Click(object sender, EventArgs e)
        {
            this.idv.DataUndo();
        }

        protected virtual void mnVersionInfo_Click(object sender, EventArgs e)
        {
            int flag = -1;
            if (this.sysenv.TerminalInfo.Demo)
            {
                flag = 4;
            }
            else
            {
                flag = (int) this.sysenv.TerminalInfo.Protocol;
            }
            UAboutDlg dlg = new UAboutDlg(flag);
            dlg.ShowDialog();
            dlg.Close();
            dlg.Dispose();
        }

        private void mnVersionUp_Click(object sender, EventArgs e)
        {
            DialogResult result;
            this.bMailActionFlag = true;
            using (MessageDialog dialog = new MessageDialog())
            {
                result = dialog.ShowMessage("W002", null, null);
            }
            if (result == DialogResult.Yes)
            {
                try
                {
                    string fileName = Path.Combine(Application.StartupPath, @"Download\NaccsLauncher.exe");
                    new Process();
                    Process.Start(fileName, "-U");
                    this.VersionUpFlag = true;
                    base.Close();
                    return;
                }
                catch (Exception exception)
                {
                    string message = Naccs.Core.Properties.Resources.ResourceManager.GetString("CORE70") + MessageDialog.CreateExceptionMessage(exception);
                    using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                    {
                        form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, message);
                    }
                    return;
                }
            }
            this.bMailActionFlag = false;
        }

        private void mnViewRefresh_Click(object sender, EventArgs e)
        {
            this.idv.ViewRefresh();
        }

        private void mnVisibleCount_Click(object sender, EventArgs e)
        {
            this.idv.SetOpenViewFlag(this.mnVisibleCount.Checked);
            this.idv.ViewCountCheck(false);
        }

        private void mnWebSiteLink_Click(object sender, EventArgs e)
        {
            string webSites = this.pi.WebSites;
            this.HtmlShow(webSites);
        }

        private void OptionCheck()
        {
            this.sysenv = SystemEnvironment.CreateInstance();
            this.DemoFlag = this.sysenv.TerminalInfo.Demo;
            this.usrenv = UserEnvironment.CreateInstance();
            if (((this.usrenv.TerminalInfo.TermLogicalName == null) || (this.usrenv.TerminalInfo.TermLogicalName == "")) || ((this.sysenv.TerminalInfo.Protocol != ProtocolType.Mail) && ((this.usrenv.TerminalInfo.TermAccessKey == null) || (this.usrenv.TerminalInfo.TermAccessKey == ""))))
            {
                OptionDialog dialog = (OptionDialog) this.CreateOption();
                this.uopd.Hide();
                dialog.ShowDialog();
                this.usrenv = UserEnvironment.CreateInstance();
                if (((this.usrenv.TerminalInfo.TermLogicalName == null) || (this.usrenv.TerminalInfo.TermLogicalName == "")) || ((this.sysenv.TerminalInfo.Protocol != ProtocolType.Mail) && ((this.usrenv.TerminalInfo.TermAccessKey == null) || (this.usrenv.TerminalInfo.TermAccessKey == ""))))
                {
                    dialog.Dispose();
                    Environment.Exit(0);
                }
                dialog.Dispose();
                this.uopd.Show();
                Application.DoEvents();
            }
            else if (this.isProxyError)
            {
                OptionDialog dialog2 = (OptionDialog) this.CreateOption();
                this.uopd.Hide();
                dialog2.DefaultTab = dialog2.tbcRoot.TabPages[1];
                DialogResult result = dialog2.ShowDialog();
                this.usrenv = UserEnvironment.CreateInstance();
                dialog2.Dispose();
                if (result == DialogResult.OK)
                {
                    string fileName = Path.Combine(Application.StartupPath, @"Download\NaccsLauncher.exe");
                    new Process();
                    Process.Start(fileName, "-U");
                    this.VersionUpFlag = true;
                }
                Environment.Exit(0);
                Application.DoEvents();
            }
        }

        private void OptionSet(bool ReloadFlag)
        {
            this.usrenv = UserEnvironment.CreateInstance();
            if (this.sysenv.TerminalInfo.Protocol == ProtocolType.Mail)
            {
                this.ProtocolName = this.sysenv.MailInfo.SelectServer(this.usrenv.MailInfo.ServerConnect).display;
            }
            else
            {
                this.ProtocolName = this.sysenv.InteractiveInfo.SelectServer(this.usrenv.InteractiveInfo.ServerConnect).display;
            }
            if (this.SeqList == null)
            {
                this.stbInfomation.Text = this.ProtocolName;
                this.stbInfomation.Font = new Font(this.stbInfomation.Font, FontStyle.Regular);
            }
            this.usrenv_term = this.usrenv.TerminalInfo;
            this.stbTerm.Text = this.usrenv_term.TermLogicalName;
            this.SendRecvTimer = this.usrenv.InteractiveInfo.AutoSendRecvTm;
            this.SendRecvTimerFlg = this.usrenv.InteractiveInfo.AutoSendRecvMode;
            this.JobConnectSet();
            this.JobConnect = this.usrenv.InteractiveInfo.JobConnect;
            this.AutoSendRecvFlag = this.usrenv.MailInfo.AutoMode;
            this.AutoSendRecvTimer = this.usrenv.MailInfo.AutoTm;
            this.AutoSendRecvKind = this.usrenv.MailInfo.AutoTmKind;
            if (this.AutoSendRecvTimer > 0)
            {
                this.tmrSendRecv.Interval = (this.AutoSendRecvTimer * 60) * 0x3e8;
            }
            else
            {
                this.AutoSendRecvFlag = false;
            }
            this.tmrSendRecv.Enabled = this.AutoSendRecvFlag;
            this.fsc = FileSave.CreateInstance();
            this.msgc = MessageClassify.CreateInstance();
            this.JobKeySet();
            new UserKeySet().ShortCutSet(this.mnMainMenu.Items);
            this.FunctionSet(0);
            this.idv.SetUserClass(this.sysenv.TerminalInfo.Debug, ReloadFlag);
        }

        public virtual void OptionSetInd()
        {
        }

        public virtual void OptionSetProc()
        {
        }

        private void Print_OnPrintEnd(string Key, int PrintFlag, int Status)
        {
            if (Key != "")
            {
                IData data = null;
                if (Status == 0)
                {
                    this.idv.PrintStatusChange(Key);
                }
                else if (Status == 1)
                {
                    data = this.idv.GetData(int.Parse(Key));
                    if ((data.Header.DataType == "U") || (data.Header.DataType == "P"))
                    {
                        this.idv.OpenStatusChange(Key);
                    }
                }
                if (PrintFlag == 2)
                {
                    if (Status != 1)
                    {
                        data = this.idv.GetData(int.Parse(Key));
                    }
                    if (data != null)
                    {
                        this.idv.Distribute(data);
                    }
                }
            }
        }

        private void Print_OnPrintError(object Sender, string Key, int PrintFlag)
        {
            if (PrintFlag == 0)
            {
                this.pr.PrintErrMsg(Sender);
            }
            if (PrintFlag == 2)
            {
                IData data = this.idv.GetData(int.Parse(Key));
                if (data != null)
                {
                    this.idv.Distribute(data);
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return (this.bOptionShowingFlag || base.ProcessCmdKey(ref msg, keyData));
        }

        protected int ReceiveNoticeMessage(IData data)
        {
            int num = -1;
            ReceiveNotice notice = ReceiveNotice.CreateInstance();
            try
            {
                if (notice.Inform.InfoMode)
                {
                    if ((this.rnd != null) && !this.rnd.IsDisposed)
                    {
                        return num;
                    }
                    for (int i = 0; i < notice.ReceiveInformList.Count; i++)
                    {
                        if (data.OutCode.StartsWith(notice.ReceiveInformList[i].OutCode))
                        {
                            if (this.sysenv.TerminalInfo.Protocol == ProtocolType.Mail)
                            {
                                if (notice.ReceiveInformList[i].Inform)
                                {
                                    if (notice.ReceiveInformList[i].InformSound)
                                    {
                                        num = 2;
                                    }
                                    else
                                    {
                                        num = 1;
                                    }
                                }
                                else if (notice.ReceiveInformList[i].InformSound)
                                {
                                    num = 0;
                                }
                            }
                            else
                            {
                                if (notice.ReceiveInformList[i].Inform)
                                {
                                    this.rnd = new frmReceiveNoticeDlg();
                                    this.rnd.Show(this);
                                }
                                if (notice.ReceiveInformList[i].InformSound && File.Exists(notice.Sound.SoundFile))
                                {
                                    new SoundPlayer(notice.Sound.SoundFile).Play();
                                }
                            }
                            break;
                        }
                    }
                }
                return num;
            }
            catch (Exception exception)
            {
                using (MessageDialog dialog = new MessageDialog())
                {
                    dialog.ShowMessage("E301", notice.Sound.SoundFile, null, exception);
                }
                return num;
            }
        }

        private void RecvDataSet(IData data, out IData Odata, out IData Rdata)
        {
            string dataInfo;
            Odata = null;
            Rdata = null;
            string jCode = "";
            string oCode = "";
            switch (data.Header.DataType)
            {
                case "M":
                {
                    RecvData data2 = DataFactory.CreateRecvData(data);
                    Odata = data2.OtherData;
                    Rdata = data2.ResultData;
                    return;
                }
                case "R":
                {
                    Rdata = data;
                    dataInfo = Rdata.Header.DataInfo;
                    char ch = dataInfo[dataInfo.Length - 1];
                    if (ch.ToString() == "S")
                    {
                        this.GetSWCode(Rdata.Header.OutCode.Trim(), out jCode, out oCode, 0, dataInfo);
                    }
                    Odata = this.idv.GetDivData(dataInfo, 1, jCode, oCode);
                    return;
                }
            }
            Odata = data;
            dataInfo = Odata.Header.DataInfo;
            char ch2 = dataInfo[dataInfo.Length - 1];
            if (ch2.ToString() == "S")
            {
                this.GetSWCode(Odata.Header.OutCode.Trim(), out jCode, out oCode, 1, dataInfo);
            }
            Rdata = this.idv.GetDivData(dataInfo, 0, jCode, oCode);
        }

        public string SaveFileCheck(IData data)
        {
            string path = "";
            if (!this.fsc.AllFileSave.Mode)
            {
                return path;
            }
            for (int i = 0; i < this.fsc.DetailsList.Count; i++)
            {
                if (this.fsc.DetailsList[i].OutCode == data.OutCode.Substring(0, this.fsc.DetailsList[i].OutCode.Length))
                {
                    if (!this.fsc.DetailsList[i].AutoSave)
                    {
                        return "";
                    }
                    path = this.fsc.DetailsList[i].Dir;
                    break;
                }
            }
            if (path == "")
            {
                for (int k = 0; k < this.fsc.TypeList.Count; k++)
                {
                    if (data.Header.DataType == this.fsc.TypeList[k].DataType)
                    {
                        if (!this.fsc.TypeList[k].FileSaveMode)
                        {
                            return path;
                        }
                        path = this.fsc.TypeList[k].Dir;
                        break;
                    }
                }
            }
            if (path == "")
            {
                return path;
            }
            string str2 = "";
            string str3 = "";
            for (int j = 0; j < this.fsc.FileNaming.FileNameRule.Length; j++)
            {
                if (str2 != "")
                {
                    str3 = "_";
                }
                char ch = this.fsc.FileNaming.FileNameRule[j];
                switch (int.Parse(ch.ToString()))
                {
                    case 1:
                        str2 = str2 + str3 + data.Header.UserId.Substring(0, 5).Trim();
                        break;

                    case 2:
                        str2 = str2 + str3 + this.FileNameCheck(data.Header.OutCode, "");
                        break;

                    case 3:
                        str2 = str2 + str3 + this.FileNameCheck(data.Header.Subject, "-");
                        break;

                    case 4:
                        str2 = str2 + str3 + DateTime.Now.ToString("yyyyMMddHHmmss", DateTimeFormatInfo.InvariantInfo);
                        break;
                }
            }
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (File.Exists(path + @"\" + str2 + ".txt"))
            {
                int num4 = 1;
                string str4 = path + @"\" + str2 + num4.ToString() + ".txt";
                while (File.Exists(str4))
                {
                    num4++;
                    str4 = path + @"\" + str2 + num4.ToString() + ".txt";
                }
                return str4;
            }
            return (path + @"\" + str2 + ".txt");
        }

        private void SaveUserApplication()
        {
            int num;
            if (base.WindowState == FormWindowState.Normal)
            {
                this.usr_apc.Left = base.Location.X;
                this.usr_apc.Top = base.Location.Y;
                this.usr_apc.Right = base.Size.Width + base.Location.X;
                this.usr_apc.Bottom = base.Size.Height + base.Location.Y;
            }
            if (this.mnDataViewWindow.Checked)
            {
                this.usr_apc.MainSplitter = this.pnlSupportWindow.Width;
            }
            this.usr_apc.DataViewSplitter = this.idv.GetSplitter();
            this.usr_apc.Maximized = base.WindowState == FormWindowState.Maximized;
            this.usr_apc.CloseOnAppend = this.mnCloseOnAppend.Checked;
            this.usr_apc.ListRestriction = this.mnListRestriction.Checked;
            if (this.mnFontLarge.Checked)
            {
                num = 0;
            }
            else if (this.mnFontSmall.Checked)
            {
                num = 1;
            }
            else
            {
                num = 2;
            }
            this.usr_apc.DataViewFontSize = num;
            this.usr_apc.LogoffConfirm = this.mnLogoffConf.Checked;
            this.usr_tlb.Standard = this.mnToolbarStandard.Checked;
            this.usr_tlb.Job = this.mnToolbarJob.Checked;
            this.usr_tlb.FunctionBar = this.mnFunctionBar.Checked;
            this.usr_wnd.UserInput = this.mnLogonWindow.Checked;
            this.usr_wnd.JobInput = this.mnJobInputWindow.Checked;
            this.usr_wnd.JobMenu = this.mnJobMenuWindow.Checked;
            this.usr_wnd.DataView = this.mnDataViewWindow.Checked;
            this.usr_cwc = this.idv.GetDataViewColum();
            this.usr_dvf.ReadVisible = this.mnVisibleCount.Checked;
            this.usr_jpc.QueryFieldClear = this.mnQueryFieldClear.Checked;
            this.FldList = this.idv.GetFldList();
        }

        public bool SearchDispCode(IData data)
        {
            if (this.sysenv.TerminalInfo.UserKind == 4)
            {
                return true;
            }
            List<string> list = new List<string>();
            list = this.dscl.DispCodeGet(data.Header.JobCode.Trim());
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (data.DispCode == list[i].Substring(0, 3))
                    {
                        return true;
                    }
                }
                return false;
            }
            return (data.DispCode.Trim() == "");
        }

        public virtual void Send_Click(object sender, EventArgs e)
        {
        }

        public IData SendDataCreate(IData Data, int DivFlag)
        {
            AbstractJobConfig config;
            Data.Header.Control = DataControl.GetControl();
            Data.TimeStamp = DateTime.Now;
            string termLogicalName = "";
            if (this.usrenv.TerminalInfo.TermLogicalName != null)
            {
                termLogicalName = this.usrenv.TerminalInfo.TermLogicalName;
            }
            if (this.DataInfoCnt < 0xf4240)
            {
                this.DataInfoCnt++;
            }
            else
            {
                this.DataInfoCnt = 0;
            }
            JobConfigFactory.createJobConfig(Data.Header.JobCode.Trim(), Data.Header.DispCode.Trim(), DateTime.Now, out config);
            if ((config != null) && (config.jobStyle == JobStyle.combi))
            {
                string str2 = this.DataInfoCnt.ToString().PadLeft(5, '0');
                if (str2.Length > 5)
                {
                    str2 = str2.Remove(0, str2.Length - 5);
                }
                Data.Header.DataInfo = termLogicalName.PadRight(6) + Data.TimeStamp.ToString("yyyyMMddHHmmss", DateTimeFormatInfo.InvariantInfo) + str2 + "S";
            }
            else if (this.sysenv.TerminalInfo.UserKind == 4)
            {
                string str3 = this.DataInfoCnt.ToString().PadLeft(5, '0');
                if (str3.Length > 5)
                {
                    str3 = str3.Remove(0, str3.Length - 5);
                }
                Data.Header.DataInfo = termLogicalName.PadRight(6) + Data.TimeStamp.ToString("yyyyMMddHHmmss", DateTimeFormatInfo.InvariantInfo) + str3 + "K";
            }
            else
            {
                Data.Header.DataInfo = termLogicalName.PadRight(6) + Data.TimeStamp.ToString("yyyyMMddHHmmss", DateTimeFormatInfo.InvariantInfo) + this.DataInfoCnt.ToString().PadLeft(6, '0');
            }
            Data.Header.UserId = this.UserCode;
            Data.Header.Password = this.UserPassword;
            Data.Header.Path = termLogicalName.PadRight(6);
            Data.Header.Div = "001";
            Data.Header.EndFlag = "E";
            Data.Header.SendGuard = "";
            Data.Header.DataType = "";
            return Data;
        }

        private void SeqLoad(object sender)
        {
            if (this.SeqList != null)
            {
                this.SeqInt++;
                if (this.SeqInt >= this.SeqList.Count)
                {
                    this.stbInfomation.Text = this.ProtocolName;
                    this.stbInfomation.Font = new Font(this.stbInfomation.Font, FontStyle.Regular);
                    this.SeqList = null;
                }
                else
                {
                    FileInfo info = new FileInfo(this.SeqList[this.SeqInt]);
                    string name = info.Name;
                    string[] strArray = new string[] { "順次ファイル選択中 ", (this.SeqInt + 1).ToString(), "/", this.SeqList.Count.ToString(), " <", name, "> " };
                    this.stbInfomation.Text = string.Concat(strArray);
                    this.stbInfomation.Font = new Font(this.stbInfomation.Font, FontStyle.Bold);
                    IData data = null;
                    try
                    {
                        data = DataFactory.LoadFromEdiFile(this.SeqList[this.SeqInt]);
                    }
                    catch (Exception exception)
                    {
                        MessageDialog dialog = new MessageDialog();
                        dialog.ShowMessage("E302", this.SeqList[this.SeqInt], null, exception);
                        dialog.Dispose();
                        return;
                    }
                    if ((sender != null) || !(data.JobCode.Trim() == ""))
                    {
                        if (data != null)
                        {
                            if ((sender == null) && !this.SearchDispCode(data))
                            {
                                UJobInputDlg dlg = new UJobInputDlg();
                                dlg.SetCodeSelect(data.JobCode.Trim());
                                if (dlg.ShowDialog() != DialogResult.OK)
                                {
                                    dlg.Close();
                                    dlg.Dispose();
                                    return;
                                }
                                data.Header.DispCode = dlg.DispCode;
                                dlg.Close();
                                dlg.Dispose();
                            }
                            this.JobFormOpen(data, sender);
                        }
                    }
                    else
                    {
                        this.JobError(-1);
                    }
                }
            }
        }

        private void SetControl(int flag)
        {
            this.SetControlMenu(flag);
            this.usrenv.DebugFunction.Control = flag;
            this.usrenv.Save();
        }

        private void SetControlMenu(int flag)
        {
            switch (flag)
            {
                case 0:
                    this.mnControlSS.Checked = true;
                    this.mnControlMS.Checked = false;
                    this.mnControlGS.Checked = false;
                    this.mnControlWS.Checked = false;
                    return;

                case 1:
                    this.mnControlSS.Checked = false;
                    this.mnControlMS.Checked = true;
                    this.mnControlGS.Checked = false;
                    this.mnControlWS.Checked = false;
                    return;

                case 2:
                    this.mnControlSS.Checked = false;
                    this.mnControlMS.Checked = false;
                    this.mnControlGS.Checked = true;
                    this.mnControlWS.Checked = false;
                    return;

                case 3:
                    this.mnControlSS.Checked = false;
                    this.mnControlMS.Checked = false;
                    this.mnControlGS.Checked = false;
                    this.mnControlWS.Checked = true;
                    return;
            }
        }

        private void ShortCutCheck(ToolStripMenuItem tsmi, int Flag)
        {
            int num = 0;
            if (tsmi.ShortcutKeys != Keys.None)
            {
                int num2 = 0;
                switch (Flag)
                {
                    case 1:
                        num2 = 0x20000;
                        break;

                    case 2:
                        num2 = 0x10000;
                        break;
                }
                for (int i = 0x70; i <= 0x7b; i++)
                {
                    //tuyenhm
                    if (tsmi.ShortcutKeys ==(Keys) (i + num2))
                    {
                        this.MiList[num] = tsmi;
                        for (int j = 0; j < this.tslFunction.Items.Count; j++)
                        {
                            if ((this.tslFunction.Items[j].Tag == null) || !(this.tslFunction.Items[j].Tag.ToString() == num.ToString()))
                            {
                                continue;
                            }
                            Graphics graphics = base.CreateGraphics();
                            string functionName = this.GetFunctionName(tsmi);
                            SizeF ef = graphics.MeasureString(functionName, this.tslFunction.Items[j].Font);
                            int num5 = (this.tslFunction.Items[j].Width - this.tslFunction.Items[j].Image.Width) - 15;
                            this.tslFunction.Items[j].ToolTipText = functionName;
                            while (true)
                            {
                                if (num5 >= ef.Width)
                                {
                                    break;
                                }
                                functionName = functionName.Substring(0, functionName.Length - 1);
                                ef = graphics.MeasureString(functionName, this.tslFunction.Items[j].Font);
                            }
                            this.tslFunction.Items[j].Text = functionName;
                            break;
                        }
                    }
                    num++;
                }
            }
        }

        public void StatusChange(int ist)
        {
            this.ComStatus = ist;
            this.stbStatus.Text = Enum.GetName(System.Type.GetType("Naccs.Core.Main.ComSt"), this.ComStatus);
            if (this.ComStatus != 0)
            {
                if (this.sysenv.TerminalInfo.UserKind != 4)
                {
                    this.stbUserCode.Text = this.UserCode;
                }
                this.stbMailAdress.Text = this.MailBoxID;
            }
            else
            {
                this.stbUserCode.Text = "";
                this.stbMailAdress.Text = "";
            }
            this.UpdateMenuItems();
            ULogClass.LogWrite("Status=" + this.stbStatus.Text);
        }

        private void SupportWindowCheck()
        {
            if ((!this.mnLogonWindow.Checked & !this.mnJobInputWindow.Checked) & !this.mnJobMenuWindow.Checked)
            {
                this.pnlSupportWindow.Visible = false;
            }
            else
            {
                this.pnlSupportWindow.Visible = true;
            }
        }

        protected IData SWCheck(IData data)
        {
            char ch = data.Header.DataInfo[data.Header.DataInfo.Length - 1];
            if (ch.ToString() == "S")
            {
                if (data.Header.OutCode.IndexOf("*") > -1)
                {
                    data.Header.DataType = "R";
                    string str = this.GetSWJobCode(data.OutCode.Trim(), 0);
                    if (str != "")
                    {
                        data.Header.JobCode = str;
                    }
                    return data;
                }
                data.Header.DataType = "C";
                string outCode = data.OutCode.Trim();
                if (outCode.Length > 6)
                {
                    outCode = outCode.Substring(0, 6);
                }
                string sWJobCode = this.GetSWJobCode(outCode, 1);
                if (sWJobCode != "")
                {
                    data.Header.JobCode = sWJobCode;
                }
            }
            return data;
        }

        private void tbbF11_Click(object sender, EventArgs e)
        {
            string text = ((ToolStripButton) sender).Text;
            string dispCode = "";
            int index = text.IndexOf(".");
            if (index > -1)
            {
                dispCode = text;
                text = text.Substring(0, index);
                dispCode = dispCode.Remove(0, index + 1);
            }
            this.JobFormOpen(text, dispCode);
        }

        private void tbbJetras_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(this.pi.Jetras);
            }
            catch (Exception exception)
            {
                string message = Naccs.Core.Properties.Resources.ResourceManager.GetString("CORE71") + Environment.NewLine + MessageDialog.CreateExceptionMessage(exception);
                using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                {
                    form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, message);
                }
            }
        }

        protected void TimerStart()
        {
            if (this.sysenv.TerminalInfo.Protocol == ProtocolType.Mail)
            {
                this.tmrSendRecv.Enabled = this.AutoSendRecvFlag;
            }
            else
            {
                this.tmrJobConnect.Enabled = this.SendRecvTimerFlg;
            }
        }

        protected void TimerStop()
        {
            if (this.sysenv.TerminalInfo.Protocol == ProtocolType.Mail)
            {
                this.tmrSendRecv.Enabled = false;
            }
            else
            {
                this.tmrJobConnect.Enabled = false;
            }
        }

        public virtual void tmrJobConnect_Tick(object sender, EventArgs e)
        {
        }

        public virtual void tmrSendRecv_Tick(object sender, EventArgs e)
        {
        }

        private void ToolbarCheck()
        {
            this.tslStandard.Visible = this.mnToolbarStandard.Checked;
            this.tslJob.Visible = this.mnToolbarJob.Checked;
            this.tslFunction.Visible = this.mnFunctionBar.Checked;
        }

        public void UpdateMenuItems()
        {
            this.mnOpen.Enabled = this.mnDataDelete.Enabled;
            this.tbbOpen.Enabled = this.mnOpen.Enabled;
            this.mnFileSave.Enabled = this.mnDataDelete.Enabled;
            this.tbbFileSaveAs.Enabled = this.mnFileSave.Enabled;
            this.mnPrintJobData.Enabled = this.mnDataDelete.Enabled;
            this.tbbPrintJobData.Enabled = this.mnPrintJobData.Enabled;
            this.mnPrintJobDataPreview.Enabled = this.mnDataDelete.Enabled;
            this.tbbPrintJobDataPreview.Enabled = this.mnPrintJobDataPreview.Enabled;
            this.mnLogon.Enabled = this.ComStatus == 0;
            this.tbbLogon.Enabled = this.mnLogon.Enabled;
            this.mnLogoff.Enabled = this.ComStatus == 1;
            this.tbbLogoff.Enabled = this.mnLogoff.Enabled;
            this.mnBatchSend.Enabled = this.ComStatus == 1;
            this.mnRep.Enabled = this.ComStatus == 1;
            this.tbbREP.Enabled = this.mnRep.Enabled;
            this.mnOtherTerm.Enabled = this.ComStatus == 1;
            this.mnJobOpen.Enabled = this.mnOpen.Enabled;
            this.mnDataViewRepair.Enabled = this.ComStatus == 0;
            this.mnDataViewRestore.Enabled = this.ComStatus == 0;
            this.mnDebug.Visible = this.sysenv.TerminalInfo.Debug;
            this.SetControlMenu(this.usrenv.DebugFunction.Control);
            this.UpdateMenuItemsProc();
            this.UpdateMenuItemsInd();
            this.idv.UpdDataViewMainItems(this.mnViewRefresh.Enabled, this.mnDataViewImport.Enabled, this.mnDataViewExport.Enabled, this.DemoFlag);
            if (this.DsArrangeManager != null)
            {
                this.DsArrangeManager.MenuEnabled = this.ComStatus == 0;
            }
            this.FunctionEnabledCheck();
        }

        public virtual void UpdateMenuItemsInd()
        {
        }

        public virtual void UpdateMenuItemsProc()
        {
        }

        protected void UserCodeJobSet(string usrcd)
        {
            if (this.sysenv.TerminalInfo.UserKind != 4)
            {
                for (int i = 0; i < Application.OpenForms.Count; i++)
                {
                    if (Application.OpenForms[i] is CommonJobForm)
                    {
                        ((CommonJobForm) Application.OpenForms[i]).UserCode = usrcd;
                    }
                }
            }
        }

        private void UserPathAuth()
        {
            UserPathNames names = UserPathNames.CreateInstance();
            if (names.CommonPath.Specify)
            {
                try
                {
                    string path = Path.Combine(names.CommonPath.Folder, Path.GetRandomFileName());
                    File.WriteAllText(path, Environment.UserName);
                    File.Delete(path);
                }
                catch (Exception exception)
                {
                    ULogClass.LogWrite(string.Format("{0} {1}", MessageKind.Error.ToString(), string.Format(Naccs.Core.Properties.Resources.ResourceManager.GetString("cnAuthMessage"), names.CommonPath.Folder, MessageDialog.CreateExceptionMessage(exception))));
                    MessageDialog dialog = new MessageDialog();
                    try
                    {
                        if (dialog.ShowMessage(ButtonPatern.YES_NO, MessageKind.Error, string.Format(Naccs.Core.Properties.Resources.ResourceManager.GetString("cnAuthMessage"), names.CommonPath.Folder, Naccs.Core.Properties.Resources.ResourceManager.GetString("cnAuthReply")), false, true, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            names.CommonPath.Specify = false;
                            names.Save();
                            UserPathNames.ResetInstance();
                            PathInfo.ResetInstance();
                            ULogClass.LogWrite("=====< Common Settings Reset>=====");
                        }
                        else
                        {
                            ULogClass.LogWrite("=====< Common Settings Error >=====");
                            Environment.Exit(0);
                        }
                    }
                    finally
                    {
                        dialog.Close();
                        dialog.Dispose();
                    }
                }
            }
        }

        private void WindowCheck()
        {
            this.pnlUserInput.Visible = this.mnLogonWindow.Checked;
            this.pnlJobInput.Visible = this.mnJobInputWindow.Checked;
            this.pnlJobMenu.Visible = this.mnJobMenuWindow.Checked;
            this.pnlDataView.Visible = this.mnDataViewWindow.Checked;
            if (!this.pnlDataView.Visible)
            {
                this.pnlSupportWindow.Dock = DockStyle.Fill;
            }
            else
            {
                this.pnlSupportWindow.Dock = DockStyle.Left;
            }
            this.SupportWindowCheck();
        }

        private string VersioupToolPath
        {
            get
            {
                return Path.Combine(Application.StartupPath, @"Download\NaccsLauncher.exe");
            }
        }
    }
}

