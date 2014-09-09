namespace Naccs.Core.Option
{
    using Naccs.Core.Classes;
    using Naccs.Core.Properties;
    using Naccs.Core.Settings;
    using Naccs.Net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security.AccessControl;
    using System.Security.Cryptography.X509Certificates;
    using System.Windows.Forms;
    using System.Deployment.Application;

    public class OptionDialog : Form
    {
        private ToolStripButton bdnAPAddNewItem;
        private ToolStripLabel bdnAPCountItem;
        private ToolStripButton bdnAPDeleteItem;
        private ToolStripButton bdnAPMoveFirstItem;
        private ToolStripButton bdnAPMoveLastItem;
        private ToolStripButton bdnAPMoveNextItem;
        private ToolStripButton bdnAPMovePreviousItem;
        private ToolStripTextBox bdnAPPositionItem;
        private ToolStripSeparator bdnAPSeparator;
        private ToolStripSeparator bdnAPSeparator1;
        private ToolStripSeparator bdnAPSeparator2;
        private ToolStripButton bdnASCAddNewItem;
        private ToolStripLabel bdnASCCountItem;
        private ToolStripButton bdnASCDeleteItem;
        private ToolStripButton bdnASCMoveFirstItem;
        private ToolStripButton bdnASCMoveLastItem;
        private ToolStripButton bdnASCMoveNextItem;
        private ToolStripButton bdnASCMovePreviousItem;
        private ToolStripTextBox bdnASCPositionItem;
        private ToolStripSeparator bdnASCSeparator1;
        private ToolStripSeparator bdnASCSeparator2;
        private ToolStripSeparator bdnASCSeparator3;
        private BindingNavigator bdnAutoPrint;
        private BindingNavigator bdnFileSaveC;
        private ToolStripLabel bdnKGCountItem;
        private ToolStripButton bdnKGMoveFirstItem;
        private ToolStripButton bdnKGMoveLastItem;
        private ToolStripButton bdnKGMoveNextItem;
        private ToolStripButton bdnKGMovePreviousItem;
        private ToolStripTextBox bdnKGPositionItem;
        private ToolStripSeparator bdnKGSeparator1;
        private ToolStripSeparator bdnKGSeparator2;
        private ToolStripLabel bdnKJCountItem;
        private ToolStripButton bdnKJMoveFirstItem;
        private ToolStripButton bdnKJMoveLastItem;
        private ToolStripButton bdnKJMoveNextItem;
        private ToolStripButton bdnKJMovePreviousItem;
        private ToolStripTextBox bdnKJPositionItem;
        private ToolStripSeparator bdnKJSeparator1;
        private ToolStripSeparator bdnKJSeparator2;
        private ToolStripLabel bdnKRCountItem;
        private ToolStripButton bdnKRMoveFirstItem;
        private ToolStripButton bdnKRMoveLastItem;
        private ToolStripButton bdnKRMoveNextItem;
        private ToolStripButton bdnKRMovePreviousItem;
        private ToolStripTextBox bdnKRPositionItem;
        private ToolStripSeparator bdnKRSeparator1;
        private ToolStripSeparator bdnKRSeparator2;
        private ToolStripLabel bdnKSCountItem;
        private ToolStripButton bdnKSMoveFirstItem;
        private ToolStripButton bdnKSMoveLastItem;
        private ToolStripButton bdnKSMoveNextItem;
        private ToolStripButton bdnKSMovePreviousItem;
        private ToolStripTextBox bdnKSPositionItem;
        private ToolStripSeparator bdnKSSeparator1;
        private ToolStripSeparator bdnKSSeparator2;
        private ToolStripButton bdnMCAddNewItem;
        private ToolStripLabel bdnMCCountItem;
        private ToolStripButton bdnMCDeleteItem;
        private ToolStripButton bdnMCMoveFirstItem;
        private ToolStripButton bdnMCMoveLastItem;
        private ToolStripButton bdnMCMoveNextItem;
        private ToolStripButton bdnMCMovePreviousItem;
        private ToolStripTextBox bdnMCPositionItem;
        private ToolStripSeparator bdnMCSeparator1;
        private ToolStripSeparator bdnMCSeparator2;
        private ToolStripSeparator bdnMCSeparator3;
        private BindingNavigator bdnMsgCls;
        private BindingNavigator bdnRcvNotice;
        private ToolStripButton bdnRNAddNewItem;
        private ToolStripLabel bdnRNCountItem;
        private ToolStripButton bdnRNDeleteItem;
        private ToolStripButton bdnRNMoveFirstItem;
        private ToolStripButton bdnRNMoveLastItem;
        private ToolStripButton bdnRNMoveNextItem;
        private ToolStripButton bdnRNMovePreviousItem;
        private ToolStripTextBox bdnRNPositionItem;
        private ToolStripSeparator bdnRNSeparator1;
        private ToolStripSeparator bdnRNSeparator2;
        private ToolStripSeparator bdnRNSeparator3;
        private BindingNavigator bdnUserKeyG;
        private BindingNavigator bdnUserKeyJ;
        private BindingNavigator bdnUserKeyR;
        private BindingNavigator bdnUserKeyS;
        private BindingSource bdsAutoPrint;
        private BindingSource bdsFileSaveC;
        private BindingSource bdsFileSaveK;
        private BindingSource bdsMsgCls;
        private BindingSource bdsRcvNotice;
        private BindingSource bdsUserKeyG;
        private BindingSource bdsUserKeyJ;
        private BindingSource bdsUserKeyR;
        private BindingSource bdsUserKeyS;
        private Button btnAcptCncl;
        private Button btnAcptOK;
        private Button btnCertificateSelect;
        private Button btnDefaultPrinter;
        private Button btnEntryPrtAdd;
        private Button btnEntryPrtDel;
        private Button btnGateway;
        private Button btnKanriFile;
        private Button btnPrinterSet;
        private Button btnPwdChngCncl;
        private Button btnPwdChngOK;
        private Button btnRootAcpt;
        private Button btnRootCncl;
        private Button btnRootOK;
        private Button btnSendFile;
        private Button btnSoundFile;
        private Button btnUkeyDflt;
        private Button btnUkeyReset;
        private const string C_IDENTITY = "everyone";
        private const string C_NO_SETTING = "-";
        private DataGridViewTextBoxColumn cAPBinName;
        private DataGridViewTextBoxColumn cAPBinNo;
        private DataGridViewButtonColumn cAPBtn;
        private DataGridViewTextBoxColumn cAPCount;
        private DataGridViewCheckBoxColumn cAPMode;
        private DataGridViewTextBoxColumn cAPOutCode;
        private DataGridViewTextBoxColumn cAPPrinter;
        private DataGridView cdgv;
        private string CertHash;
        private DataGridViewCheckBoxColumn cFSCAutoSave;
        private DataGridViewButtonColumn cFSCBtn;
        private DataGridViewTextBoxColumn cFSCDir;
        private DataGridViewTextBoxColumn cFSCOutCode;
        private DataGridViewButtonColumn cFSKBtn;
        private DataGridViewTextBoxColumn cFSKDataName;
        private DataGridViewTextBoxColumn cFSKDataType;
        private DataGridViewTextBoxColumn cFSKDir;
        private DataGridViewCheckBoxColumn cFSKFileSaveMode;
        private CheckBox chbSizeWarning;
        private CheckBox ckbAutoPrt;
        private CheckBox ckbAutoSendRecvMode;
        private CheckBox ckbCommonFolder;
        private CheckBox ckbDoublePrt;
        private CheckBox ckbFileSave;
        private CheckBox ckbJobConnect;
        private CheckBox ckbRcvInform;
        private CheckBox ckbRcvInformSnd;
        private CheckBox ckbTraceOutput;
        private CheckBox ckbTraceOutputM;
        private CheckBox ckbUseAutoBackup;
        private CheckBox ckbUseAutoSndRcvTm;
        private CheckBox ckbUseGateway;
        private CheckBox ckbUseProxy;
        private CheckBox ckbUseProxyAccount;
        private CheckBox ckbUseProxyAccountM;
        private CheckBox ckbUseProxyM;
        private DataGridViewTextBoxColumn cKGNameKj;
        private DataGridViewComboBoxColumn cKGSckey;
        private DataGridViewTextBoxColumn cKJGymCode;
        private DataGridViewTextBoxColumn cKJName;
        private DataGridViewTextBoxColumn cKJNameKj;
        private DataGridViewComboBoxColumn cKJSckey;
        private DataGridViewTextBoxColumn cKJWindowCode;
        private DataGridViewTextBoxColumn cKRNameKj;
        private DataGridViewComboBoxColumn cKRSckey;
        private DataGridViewTextBoxColumn cKSNameKj;
        private DataGridViewComboBoxColumn cKSSckey;
        private ComboBox cmbFileNameRule1;
        private ComboBox cmbFileNameRule2;
        private ComboBox cmbFileNameRule3;
        private ComboBox cmbFileNameRule4;
        private ComboBox cmbServerConnect;
        private ComboBox cmbServerConnectM;
        private DataGridViewTextBoxColumn cMCBankNumber;
        private DataGridViewCheckBoxColumn cMCBankWithdrawday;
        private DataGridViewButtonColumn cMCBtn;
        private DataGridViewTextBoxColumn cMCFolder;
        private DataGridViewTextBoxColumn cMCId;
        private DataGridViewTextBoxColumn cMCJobCode;
        private DataGridViewTextBoxColumn cMCOutCode;
        private DataGridViewTextBoxColumn cMCPriority;
        private DataGridViewCheckBoxColumn cMCUnOpen;
        private DataGridViewTextBoxColumn cMCUserCode;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private IContainer components;
        private DataGridViewCheckBoxColumn cRNInform;
        private DataGridViewCheckBoxColumn cRNInformSound;
        private DataGridViewTextBoxColumn cRNOutCode;
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
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private int default_binno;
        private TabPage defaultTab;
        private DataGridView dgvAutoPrint;
        private DataGridView dgvFileSaveC;
        private DataGridView dgvFileSaveK;
        private DataGridView dgvMsgCls;
        private DataGridView dgvReceiveInform;
        private DataGridView dgvUserKeyG;
        private DataGridView dgvUserKeyJ;
        private DataGridView dgvUserKeyR;
        private DataGridView dgvUserKeyS;
        private SettingDivType DivType;
        private DataColumn dtclmAPBinName;
        private DataColumn dtclmAPBinNo;
        private DataColumn dtclmAPBtn;
        private DataColumn dtclmAPCount;
        private DataColumn dtclmAPMode;
        private DataColumn dtclmAPOutCode;
        private DataColumn dtclmAPPrinter;
        private DataColumn dtclmFSCAutoSave;
        private DataColumn dtclmFSCBtn;
        private DataColumn dtclmFSCDir;
        private DataColumn dtclmFSCOutCode;
        private DataColumn dtclmFSKBtn;
        private DataColumn dtclmFSKDataName;
        private DataColumn dtclmFSKDataType;
        private DataColumn dtclmFSKDir;
        private DataColumn dtclmFSKFileSaveMode;
        private DataColumn dtclmKGName;
        private DataColumn dtclmKGNameKj;
        private DataColumn dtclmKGSckey;
        private DataColumn dtclmKJGymCode;
        private DataColumn dtclmKJName;
        private DataColumn dtclmKJNameKj;
        private DataColumn dtclmKJSckey;
        private DataColumn dtclmKJWindowCode;
        private DataColumn dtclmKRName;
        private DataColumn dtclmKRNameKj;
        private DataColumn dtclmKRSckey;
        private DataColumn dtclmKSName;
        private DataColumn dtclmKSNameKj;
        private DataColumn dtclmKSSckey;
        private DataColumn dtclmMCBankNumber;
        private DataColumn dtclmMCBankWithdrawday;
        private DataColumn dtclmMCBtn;
        private DataColumn dtclmMCFolder;
        private DataColumn dtclmMCId;
        private DataColumn dtclmMCJobCode;
        private DataColumn dtclmMCOutCode;
        private DataColumn dtclmMCPriority;
        private DataColumn dtclmMCUnOpen;
        private DataColumn dtclmMCUserCode;
        private DataColumn dtclmRNInform;
        private DataColumn dtclmRNInformSound;
        private DataColumn dtclmRNOutCode;
        private DataSet dtsOption;
        private DataSet dtsUserKey;
        private DataTable dttAutoPrint;
        private DataTable dttFileSaveC;
        private DataTable dttFileSaveK;
        private DataTable dttMsgCls;
        private DataTable dttRcvNotice;
        private DataTable dttUserKeyG;
        private DataTable dttUserKeyJ;
        private DataTable dttUserKeyR;
        private DataTable dttUserKeyS;
        private ErrInfo ei;
        private static FileSave fileSave;
        private GridErrDialog Gdlg;
        private GroupBox gpbAutoSndRcvTm;
        private GroupBox gpbAutoTmKind;
        private GroupBox gpbCertificate;
        private GroupBox gpbComLog;
        private GroupBox gpbCommonFolder;
        private GroupBox gpbDefaultPrinter;
        private GroupBox gpbEntryPrinter;
        private GroupBox gpbFileNameRule;
        private GroupBox gpbFileSaveC;
        private GroupBox gpbFileSaveK;
        private GroupBox gpbJobConnect;
        private GroupBox gpbKanriFile;
        private GroupBox gpbMargin;
        private GroupBox gpbMsgCls;
        private GroupBox gpbOptionShare;
        private GroupBox gpbPrintout;
        private GroupBox gpbProxy;
        private GroupBox gpbProxyM;
        private GroupBox gpbPwdCert;
        private GroupBox gpbPwdKind;
        private GroupBox gpbReceiveInform;
        private GroupBox gpbReceiveNotice;
        private GroupBox gpbSendFile;
        private GroupBox gpbTerminalInfo;
        private GroupBox gpbTrace;
        private GroupBox gpbTraceM;
        private GatewaySettingClass gsc;
        private static GStampSettings gStamp;
        private static HelpSettings helpSettings;
        private bool IsBank;
        protected bool isDesigning = ((AppDomain.CurrentDomain.FriendlyName == "DefaultDomain") && !ApplicationDeployment.IsNetworkDeployed);
        private bool isLogon;
        private bool IsUpdateFlg;
        private Label lblAutoSendRecvTm;
        private Label lblAutoSndRcvTm;
        private Label lblBin;
        private Label lblCertificateDate;
        private Label lblComLog1;
        private Label lblComLog2;
        private Label lblComLogSize;
        private Label lblDefaultBin;
        private Label lblDefaultPrinter;
        private Label lblDiskWarning;
        private Label lblFileNameRule;
        private Label lblHelpPath;
        private Label lblHi;
        private Label lblIssuerName;
        private Label lblJobErrCodeHtml;
        private Label lblKb1;
        private Label lblKen;
        private Label lblMarginL;
        private Label lblMarginT;
        private Label lblMaxRetreiveCnt;
        private Label lblMb;
        private Label lblMin;
        private Label lblMinM;
        private Label lblNewPwd;
        private Label lblNewPwdChk;
        private Label lblNewPwdRe;
        private Label lblOldPwd;
        private Label lblPassword;
        private Label lblPlus1;
        private Label lblPlus2;
        private Label lblPlus3;
        private Label lblPlus4;
        private Label lblPrinterName;
        private Label lblProxyAccount;
        private Label lblProxyAccountM;
        private Label lblProxyName;
        private Label lblProxyNameM;
        private Label lblProxyPassword;
        private Label lblProxyPasswordM;
        private Label lblProxyPort;
        private Label lblProxyPortM;
        private Label lblSaveTerm;
        private Label lblServerConnect;
        private Label lblServerConnectM;
        private Label lblSoundFile;
        private Label lblTermAccessKey;
        private Label lblTermLogicalName;
        private Label lblTermType;
        private Label lblTraceFile;
        private Label lblTraceFileM;
        private Label lblTraceOutput1;
        private Label lblTraceOutput1M;
        private Label lblTraceOutput2;
        private Label lblTraceOutput2M;
        private Label lblTrmErrCodeHtml;
        private Label lblUserKind;
        private ListView lsvBin;
        private int lsvBinIdx;
        private ListView lsvPrt;
        private int lsvPrtIdx;
        private static MessageClassify messageClassify;
        private MessageDialog Msdg;
        private NumericUpDown nudAutoSendRecvTm;
        private NumericUpDown nudAutoSndRcvTm;
        private NumericUpDown nudComLogSize;
        private NumericUpDown nudDiskWarning;
        private NumericUpDown nudMarginL;
        private NumericUpDown nudMarginT;
        private NumericUpDown nudMaxRetreiveCnt;
        private NumericUpDown nudSaveTerm;
        private static OptionCertification optionCertification;
        private List<string> outcodelist_ap;
        private List<string> outcodelist_asc;
        private List<string> outcodelist_mc;
        private List<string> outcodelist_rn;
        private string PassWord = "";
        private static PathInfo pathInfo;
        private Panel pnlMail;
        private Panel pnlServer;
        private static PrintSetups printSetups;
        private ProtocolType Protocol;
        private List<PrinterInfoClass> prtInfoList;
        private RadioButton rdbAutoTmKindR;
        private RadioButton rdbAutoTmKindS;
        private RadioButton rdbAutoTmKindSR;
        private RadioButton rdbNoUseShare;
        private RadioButton rdbPwdChng;
        private RadioButton rdbPwdCncl;
        private RadioButton rdbPwdSet;
        private RadioButton rdbUseShare;
        private static ReceiveNotice receiveNotice;
        private UserKey saveUserKey;
        private static SystemEnvironment systemEnvironment;
        private TabControl tbcPassword;
        public TabControl tbcRoot;
        private TabControl tbcUserKey;
        private TabPage tbpAutoPrint;
        private TabPage tbpFileSaveC;
        private TabPage tbpFileSaveK;
        private TabPage tbpHelp;
        private TabPage tbpLog;
        private TabPage tbpMsgCls;
        private TabPage tbpPassword;
        private TabPage tbpPrinter;
        private TabPage tbpPwdChk;
        private TabPage tbpPwdChng;
        private TabPage tbpReceiveInform;
        private TabPage tbpServer;
        private TabPage tbpTerm;
        private TabPage tbpUserKey;
        private TabPage tbpUserKeyG;
        private TabPage tbpUserKeyJ;
        private TabPage tbpUserKeyR;
        private TabPage tbpUserKeyS;
        private ToolStripButton tlsMCbtnDown;
        private ToolStripButton tlsMCbtnUp;
        private ToolStripSeparator tlsMCSeparator1;
        private TextBox txbCertificateDate;
        private TextBox txbComLog1;
        private TextBox txbComLog2;
        private TextBox txbCommonFolder;
        private TextBox txbDefaultBin;
        private TextBox txbDefaultPrinter;
        private TextBox txbIssuerName;
        private TextBox txbJobErrCodeHtml;
        private TextBox txbKanriFile;
        private TextBox txbNewPwd;
        private TextBox txbNewPwdRe;
        private TextBox txbOldPwd;
        private TextBox txbPassword;
        private TextBox txbProxyAccount;
        private TextBox txbProxyAccountM;
        private TextBox txbProxyName;
        private TextBox txbProxyNameM;
        private TextBox txbProxyPassword;
        private TextBox txbProxyPasswordM;
        private TextBox txbProxyPort;
        private TextBox txbProxyPortM;
        private TextBox txbSendFile;
        private TextBox txbSoundFile;
        private TextBox txbTermAccessKey;
        private TextBox txbTermLogicalName;
        private TextBox txbTermType;
        private TextBox txbTraceOutput1;
        private TextBox txbTraceOutput1M;
        private TextBox txbTraceOutput2;
        private TextBox txbTraceOutput2M;
        private TextBox txbTrmErrCodeHtml;
        private TextBox txbUserKind;
        private static User user;
        private static UserEnvironment userEnvironment;
        private static UserKey userKey;

        public OptionDialog()
        {
            this.InitializeComponent();
        }

        private void bdnAddNewItem_Click(object sender, EventArgs e)
        {
            List<string> list;
            ToolStripButton button = (ToolStripButton) sender;
            BindingNavigator currentParent = (BindingNavigator) button.GetCurrentParent();
            string name = currentParent.Name;
            if (name == null)
            {
                return;
            }
            if (!(name == "bdnAutoPrint"))
            {
                if (!(name == "bdnMsgCls"))
                {
                    if (!(name == "bdnFileSaveC"))
                    {
                        if (!(name == "bdnRcvNotice"))
                        {
                            return;
                        }
                        list = this.outcodelist_rn;
                    }
                    else
                    {
                        list = this.outcodelist_asc;
                    }
                    goto Label_0077;
                }
            }
            else
            {
                list = this.outcodelist_ap;
                goto Label_0077;
            }
            list = this.outcodelist_mc;
        Label_0077:
            list.Add("");
        }

        private void bdnDeleteItem_Click(object sender, EventArgs e)
        {
            List<string> list;
            DataGridView dgvMsgCls;
            int index;
            ToolStripButton button = (ToolStripButton) sender;
            BindingNavigator currentParent = (BindingNavigator) button.GetCurrentParent();
            string name = currentParent.Name;
            if (name == null)
            {
                return;
            }
            if (!(name == "bdnAutoPrint"))
            {
                if (!(name == "bdnMsgCls"))
                {
                    if (!(name == "bdnFileSaveC"))
                    {
                        if (!(name == "bdnRcvNotice"))
                        {
                            return;
                        }
                        list = this.outcodelist_rn;
                        dgvMsgCls = this.dgvReceiveInform;
                        index = this.cRNOutCode.Index;
                    }
                    else
                    {
                        list = this.outcodelist_asc;
                        dgvMsgCls = this.dgvFileSaveC;
                        index = this.cFSCOutCode.Index;
                    }
                    goto Label_00CF;
                }
            }
            else
            {
                list = this.outcodelist_ap;
                dgvMsgCls = this.dgvAutoPrint;
                index = this.cAPOutCode.Index;
                goto Label_00CF;
            }
            list = this.outcodelist_mc;
            dgvMsgCls = this.dgvMsgCls;
            index = this.cMCOutCode.Index;
        Label_00CF:
            list.Clear();
            foreach (DataGridViewRow row in (IEnumerable) dgvMsgCls.Rows)
            {
                list.Add(row.Cells[index].Value.ToString());
            }
        }

        private void btnAcptCncl_Click(object sender, EventArgs e)
        {
            this.txbPassword.Clear();
        }

        private void btnAcptOK_Click(object sender, EventArgs e)
        {
            if (!this.Password_Cert())
            {
                this.ShowModelessMessage("E570", null, null);
            }
        }

        private void btnCertificateSelect_Click(object sender, EventArgs e)
        {
            ClientCert cert = new ClientCert();
            X509Certificate2 certificate = cert.Select(this, "クライアント証明書選択", "インターネット接続に使用するクライアント証明書を選択してください。", true);
            if (certificate != null)
            {
                string thumbprint = certificate.Thumbprint;
                cert.ValidDays(thumbprint);
                this.txbIssuerName.Text = cert.DisplayIssuerName(certificate);
                this.txbCertificateDate.Text = certificate.NotAfter.ToString("yyyy/MM/dd", DateTimeFormatInfo.InvariantInfo);
                this.CertHash = certificate.GetCertHashString();
            }
        }

        private void btnChoiceDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            Button button = (Button) sender;
            if (button == this.btnSendFile)
            {
                dialog.SelectedPath = this.txbSendFile.Text;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.txbSendFile.Text = dialog.SelectedPath;
                }
            }
            else
            {
                dialog.SelectedPath = this.txbKanriFile.Text;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.txbKanriFile.Text = dialog.SelectedPath;
                }
            }
        }

        private void btnDefaultPrinter_Click(object sender, EventArgs e)
        {
            PrinterSettings settings = new PrinterSettings();
            if (!settings.IsValid)
            {
                MessageDialog dialog = new MessageDialog();
                dialog.ShowMessage("E607", "");
                dialog.Close();
                dialog.Dispose();
            }
            else
            {
                PrinterTrayDlg dlg = null;
                try
                {
                    dlg = new PrinterTrayDlg();
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        this.txbDefaultPrinter.Text = dlg.PrinterSettings.PrinterName;
                        this.txbDefaultBin.Text = dlg.PrinterSettings.DefaultPageSettings.PaperSource.SourceName;
                        this.default_binno = dlg.PrinterSettings.DefaultPageSettings.PaperSource.RawKind;
                        if (!this.IsEntryPrinter(dlg.PrinterSettings.PrinterName))
                        {
                            this.GetPrinterInfo(dlg.PrinterSettings);
                            this.tbpPrinter_Refresh();
                        }
                    }
                }
                catch (Exception exception)
                {
                    MessageDialog dialog2 = new MessageDialog();
                    dialog2.ShowMessage("E611", dlg.PrinterSettings.PrinterName, null, exception);
                    dialog2.Close();
                    dialog2.Dispose();
                }
            }
        }

        private void btnEntryPrtAdd_Click(object sender, EventArgs e)
        {
            if (this.prtInfoList.Count >= 0x63)
            {
                this.ShowModelessMessage("E548", null, null);
            }
            else
            {
                PrinterSettings settings = new PrinterSettings();
                if (!settings.IsValid)
                {
                    MessageDialog dialog = new MessageDialog();
                    dialog.ShowMessage("E607", "");
                    dialog.Close();
                    dialog.Dispose();
                }
                else
                {
                    PrinterTrayDlg dlg = null;
                    try
                    {
                        dlg = new PrinterTrayDlg();
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            if (this.IsEntryPrinter(dlg.PrinterSettings.PrinterName))
                            {
                                this.ShowModelessMessage("E549", null, null);
                            }
                            else
                            {
                                this.GetPrinterInfo(dlg.PrinterSettings);
                                if (string.IsNullOrEmpty(this.txbDefaultPrinter.Text))
                                {
                                    this.txbDefaultPrinter.Text = dlg.PrinterSettings.PrinterName;
                                    this.txbDefaultBin.Text = dlg.PrinterSettings.DefaultPageSettings.PaperSource.SourceName;
                                    this.default_binno = dlg.PrinterSettings.DefaultPageSettings.PaperSource.RawKind;
                                }
                                this.tbpPrinter_Refresh();
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageDialog dialog2 = new MessageDialog();
                        dialog2.ShowMessage("E611", dlg.PrinterSettings.PrinterName, null, exception);
                        dialog2.Close();
                        dialog2.Dispose();
                    }
                }
            }
        }

        private void btnEntryPrtDel_Click(object sender, EventArgs e)
        {
            if (this.lsvPrt.Items.Count > 0)
            {
                if (this.lsvPrt.Items[this.lsvPrtIdx].Text == this.txbDefaultPrinter.Text)
                {
                    this.ShowModelessMessage("E551", null, null);
                }
                else
                {
                    foreach (DataRow row in this.dttAutoPrint.Rows)
                    {
                        if ((row.ItemArray[this.dttAutoPrint.Columns.IndexOf(this.dtclmAPPrinter)].ToString() == this.lsvPrt.Items[this.lsvPrtIdx].Text) && ((bool) row.ItemArray[this.dttAutoPrint.Columns.IndexOf(this.dtclmAPMode)]))
                        {
                            this.ShowModelessMessage("E552", row.ItemArray[this.dttAutoPrint.Columns.IndexOf(this.dtclmAPOutCode)].ToString(), null);
                            return;
                        }
                    }
                    this.prtInfoList.RemoveAt(this.lsvPrtIdx);
                    this.tbpPrinter_Refresh();
                }
            }
        }

        private void btnGateway_Click(object sender, EventArgs e)
        {
            GatewaySetting setting = new GatewaySetting {
                GatewayInfo = this.gsc
            };
            if (setting.ShowDialog() == DialogResult.OK)
            {
                this.gsc = setting.GatewayInfo;
            }
            setting.Close();
            setting.Dispose();
        }

        private void btnPrinterSet_Click(object sender, EventArgs e)
        {
            if ((this.lsvPrt.Items.Count > 0) && (this.lsvBin.Items[this.lsvBinIdx].Text == this.prtInfoList[this.lsvPrtIdx].BinList[this.lsvBinIdx].Name))
            {
                if (!new PrintDocument { PrinterSettings = { PrinterName = this.prtInfoList[this.lsvPrtIdx].Name } }.PrinterSettings.IsValid)
                {
                    this.Msdg.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, Resources.ResourceManager.GetString("OptionMsg14"));
                }
                else
                {
                    dlgPrinterSetting setting = new dlgPrinterSetting {
                        PrinterName = this.prtInfoList[this.lsvPrtIdx].Name,
                        ListSize = this.prtInfoList[this.lsvPrtIdx].BinList[this.lsvBinIdx].Size,
                        ListOrientation = this.prtInfoList[this.lsvPrtIdx].BinList[this.lsvBinIdx].Orientation
                    };
                    if (setting.ShowDialog() == DialogResult.OK)
                    {
                        this.prtInfoList[this.lsvPrtIdx].BinList[this.lsvBinIdx].Size = setting.ListSize;
                        this.prtInfoList[this.lsvPrtIdx].BinList[this.lsvBinIdx].Orientation = setting.ListOrientation;
                        this.SetFontBinList(this.prtInfoList[this.lsvPrtIdx].BinList);
                    }
                    setting.Close();
                    setting.Dispose();
                }
            }
        }

        private void btnPwdChngCncl_Click(object sender, EventArgs e)
        {
            this.txbOldPwd.Clear();
            this.txbNewPwd.Clear();
            this.txbNewPwdRe.Clear();
        }

        private void btnPwdChngOK_Click(object sender, EventArgs e)
        {
            if (this.rdbPwdSet.Checked)
            {
                if (!this.Str_Decision(this.txbNewPwd.Text, this.txbNewPwd.MaxLength, StrType.IsLetterOrDigit, OpeType.EQ))
                {
                    this.ShowModelessMessage("E580", this.txbNewPwd.MaxLength.ToString(), null);
                    this.txbNewPwd.Focus();
                }
                else if (!this.Str_Decision(this.txbNewPwdRe.Text, this.txbNewPwdRe.MaxLength, StrType.IsLetterOrDigit, OpeType.EQ))
                {
                    this.ShowModelessMessage("E580", this.txbNewPwd.MaxLength.ToString(), null);
                    this.txbNewPwdRe.Focus();
                }
                else if (this.txbNewPwd.Text != this.txbNewPwdRe.Text)
                {
                    this.ShowModelessMessage("E562", null, null);
                    this.txbNewPwdRe.Focus();
                }
                else
                {
                    this.PassWord = this.txbNewPwd.Text;
                    this.UsePassword();
                    this.btnRootOK.Enabled = true;
                    this.btnRootAcpt.Enabled = true;
                    this.gpbPwdCert.Enabled = false;
                }
            }
            else if (this.rdbPwdChng.Checked)
            {
                if (this.txbOldPwd.Text != this.PassWord)
                {
                    this.ShowModelessMessage("E570", null, null);
                    this.txbOldPwd.Focus();
                }
                else if (!this.Str_Decision(this.txbNewPwd.Text, this.txbNewPwd.MaxLength, StrType.IsLetterOrDigit, OpeType.EQ))
                {
                    this.ShowModelessMessage("E580", this.txbNewPwd.MaxLength.ToString(), null);
                    this.txbNewPwd.Focus();
                }
                else if (!this.Str_Decision(this.txbNewPwdRe.Text, this.txbNewPwdRe.MaxLength, StrType.IsLetterOrDigit, OpeType.EQ))
                {
                    this.ShowModelessMessage("E580", this.txbNewPwd.MaxLength.ToString(), null);
                    this.txbNewPwdRe.Focus();
                }
                else if (this.txbNewPwd.Text != this.txbNewPwdRe.Text)
                {
                    this.ShowModelessMessage("E562", null, null);
                    this.txbNewPwdRe.Focus();
                }
                else
                {
                    this.PassWord = this.txbNewPwd.Text;
                    this.UsePassword();
                    this.btnRootOK.Enabled = true;
                    this.btnRootAcpt.Enabled = true;
                    this.gpbPwdCert.Enabled = false;
                }
            }
            else if (this.rdbPwdCncl.Checked)
            {
                if (this.txbOldPwd.Text != this.PassWord)
                {
                    this.ShowModelessMessage("E570", null, null);
                    this.txbOldPwd.Focus();
                }
                else
                {
                    this.PassWord = "";
                    this.NoUsePassword();
                    this.gpbPwdCert.Enabled = false;
                }
            }
        }

        private void btnRootAcpt_Click(object sender, EventArgs e)
        {
            if (this.FormClosing_Check())
            {
                if (!this.TabPage_Save())
                {
                    this.ShowModelessMessage("E573", null, null);
                }
                else
                {
                    this.saveUserKey = userKey;
                }
            }
        }

        private void btnRootCncl_Click(object sender, EventArgs e)
        {
            userKey.JobKeyList = this.saveUserKey.JobKeyList;
            this.Cursor = Cursors.Default;
            if (this.IsUpdateFlg)
            {
                base.DialogResult = DialogResult.OK;
            }
            else
            {
                base.DialogResult = DialogResult.Cancel;
            }
        }

        private void btnRootOK_Click(object sender, EventArgs e)
        {
            this.btnRootAcpt_Click(sender, e);
            if (this.IsUpdateFlg)
            {
                base.DialogResult = DialogResult.OK;
            }
        }

        private void btnSoundFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "wav file (*.wav)|*.wav",
                RestoreDirectory = true
            };
            try
            {
                dialog.InitialDirectory = Path.GetDirectoryName(this.txbSoundFile.Text);
            }
            catch
            {
                Console.WriteLine("btnSoundFile_Click");
            }
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txbSoundFile.Text = dialog.FileName;
            }
        }

        private void btnUkeyDflt_Click(object sender, EventArgs e)
        {
            if (this.ShowModalMessage("C503", null, null) == DialogResult.Yes)
            {
                userKey = UserKey.DefaultInstance();
                this.LoadUserKey();
            }
        }

        private void btnUkeyReset_Click(object sender, EventArgs e)
        {
            if (this.ShowModalMessage("C502", null, null) == DialogResult.Yes)
            {
                userKey = UserKey.ClearInstance();
                this.LoadUserKey();
            }
        }

        private void ckbCommonFolder_Click(object sender, EventArgs e)
        {
            if (!this.btnRootOK.Enabled)
            {
                this.ShowModelessMessage("E544", null, null);
                this.ckbCommonFolder.Checked = !this.ckbCommonFolder.Checked;
            }
            else if (!this.FormClosing_Check())
            {
                this.ckbCommonFolder.Checked = !this.ckbCommonFolder.Checked;
            }
            else
            {
                if (!this.ckbCommonFolder.Checked)
                {
                    if (!this.CommonFolderSwitch(this.ckbCommonFolder.Checked, this.txbCommonFolder.Text))
                    {
                        this.ckbCommonFolder.Checked = !this.ckbCommonFolder.Checked;
                    }
                }
                else
                {
                    string str = this.CommonFolderSelect(this.txbCommonFolder.Text, true);
                    if (str != null)
                    {
                        this.txbCommonFolder.Text = str;
                        if (!this.CommonFolderSwitch(this.ckbCommonFolder.Checked, this.txbCommonFolder.Text))
                        {
                            this.ckbCommonFolder.Checked = !this.ckbCommonFolder.Checked;
                        }
                    }
                    else
                    {
                        this.ckbCommonFolder.Checked = !this.ckbCommonFolder.Checked;
                    }
                }
                if (!this.btnRootOK.Enabled)
                {
                    this.txbPassword.Text = this.PassWord;
                    this.Password_Cert();
                }
            }
        }

        private void ckbDoublePrt_CheckedChanged(object sender, EventArgs e)
        {
            if (this.lsvPrt.Items.Count > 0)
            {
                this.prtInfoList[this.lsvPrtIdx].DoublePrint = this.ckbDoublePrt.Checked;
            }
        }

        private void ckbFileSave_CheckedChanged(object sender, EventArgs e)
        {
            this.gpbFileSaveK.Enabled = this.ckbFileSave.Checked;
        }

        private void ckbRcvInform_CheckedChanged(object sender, EventArgs e)
        {
            this.gpbReceiveNotice.Enabled = this.ckbRcvInform.Checked;
            this.btnSoundFile.Enabled = this.ckbRcvInform.Checked;
        }

        private void ckbServer_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox) sender;
            switch (box.Name)
            {
                case "ckbUseProxy":
                    this.txbProxyName.Enabled = this.ckbUseProxy.Checked;
                    this.txbProxyPort.Enabled = this.ckbUseProxy.Checked;
                    this.ckbUseProxyAccount.Enabled = this.ckbUseProxy.Checked;
                    this.ckbServer_CheckedChanged(this.ckbUseProxyAccount, e);
                    return;

                case "ckbUseProxyAccount":
                    if (!this.ckbUseProxyAccount.Enabled)
                    {
                        this.txbProxyAccount.Enabled = false;
                        this.txbProxyPassword.Enabled = false;
                        return;
                    }
                    this.txbProxyAccount.Enabled = this.ckbUseProxyAccount.Checked;
                    this.txbProxyPassword.Enabled = this.ckbUseProxyAccount.Checked;
                    return;

                case "ckbAutoSendRecvMode":
                    this.nudAutoSendRecvTm.Enabled = this.ckbAutoSendRecvMode.Checked;
                    return;

                case "ckbTraceOutput":
                    this.txbTraceOutput1.Enabled = this.ckbTraceOutput.Checked;
                    this.txbTraceOutput2.Enabled = this.ckbTraceOutput.Checked;
                    return;

                case "ckbUseProxyM":
                    this.txbProxyNameM.Enabled = this.ckbUseProxyM.Checked;
                    this.txbProxyPortM.Enabled = this.ckbUseProxyM.Checked;
                    this.ckbUseProxyAccountM.Enabled = this.ckbUseProxyM.Checked;
                    this.ckbServer_CheckedChanged(this.ckbUseProxyAccountM, e);
                    return;

                case "ckbUseProxyAccountM":
                    if (!this.ckbUseProxyAccountM.Enabled)
                    {
                        this.txbProxyAccountM.Enabled = false;
                        this.txbProxyPasswordM.Enabled = false;
                        return;
                    }
                    this.txbProxyAccountM.Enabled = this.ckbUseProxyAccountM.Checked;
                    this.txbProxyPasswordM.Enabled = this.ckbUseProxyAccountM.Checked;
                    return;

                case "ckbUseAutoSndRcvTm":
                    this.nudAutoSndRcvTm.Enabled = this.ckbUseAutoSndRcvTm.Checked;
                    this.gpbAutoTmKind.Enabled = this.ckbUseAutoSndRcvTm.Checked;
                    return;

                case "ckbTraceOutputM":
                    this.txbTraceOutput1M.Enabled = this.ckbTraceOutputM.Checked;
                    this.txbTraceOutput2M.Enabled = this.ckbTraceOutputM.Checked;
                    return;

                case "ckbUseGateway":
                    this.btnGateway.Enabled = this.ckbUseGateway.Checked;
                    return;
            }
        }

        private bool cmbFileNameRule_Check()
        {
            int[] numArray = new int[5];
            numArray[this.cmbFileNameRule1.SelectedIndex + 1] = this.cmbFileNameRule1.SelectedIndex + 1;
            if (this.cmbFileNameRule2.SelectedIndex > 0)
            {
                if (numArray[this.cmbFileNameRule2.SelectedIndex] != 0)
                {
                    this.ShowModelessMessage("E569", null, null);
                    this.tbcRoot.SelectTab(this.tbpFileSaveK);
                    this.cmbFileNameRule2.Focus();
                    return false;
                }
                numArray[this.cmbFileNameRule2.SelectedIndex] = this.cmbFileNameRule2.SelectedIndex;
            }
            if (this.cmbFileNameRule3.SelectedIndex > 0)
            {
                if (numArray[this.cmbFileNameRule3.SelectedIndex] != 0)
                {
                    this.ShowModelessMessage("E569", null, null);
                    this.tbcRoot.SelectTab(this.tbpFileSaveK);
                    this.cmbFileNameRule3.Focus();
                    return false;
                }
                numArray[this.cmbFileNameRule3.SelectedIndex] = this.cmbFileNameRule3.SelectedIndex;
            }
            if (this.cmbFileNameRule4.SelectedIndex > 0)
            {
                if (numArray[this.cmbFileNameRule4.SelectedIndex] != 0)
                {
                    this.ShowModelessMessage("E546", null, null);
                    this.tbcRoot.SelectTab(this.tbpFileSaveK);
                    this.cmbFileNameRule4.Focus();
                    return false;
                }
                numArray[this.cmbFileNameRule4.SelectedIndex] = this.cmbFileNameRule4.SelectedIndex;
            }
            return true;
        }

        private void CommonFolderEnable()
        {
            this.gpbCommonFolder.Enabled = !this.IsLogOn;
            if (this.gpbCommonFolder.Enabled)
            {
                this.gpbCommonFolder.Enabled = this.rdbNoUseShare.Checked;
                this.gpbOptionShare.Enabled = !this.ckbCommonFolder.Checked;
            }
        }

        private string CommonFolderSelect(string folder, bool isCommon)
        {
            string str = null;
            if (this.ShowModalMessage("W503", null, null) != DialogResult.OK)
            {
                return str;
            }
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = Resources.ResourceManager.GetString("OptionMsg51");
                if (!string.IsNullOrEmpty(folder))
                {
                    dialog.SelectedPath = folder;
                }
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return str;
                }
                if (isCommon)
                {
                    if (Directory.Exists(pathInfo.UserCommonPath(true, dialog.SelectedPath)))
                    {
                        this.ShowModelessMessage("E575", null, null);
                        return str;
                    }
                    return dialog.SelectedPath;
                }
                return dialog.SelectedPath;
            }
        }

        private bool CommonFolderSwitch(bool specify, string folder)
        {
            if (this.ShowModalMessage("C505", null, null) == DialogResult.Cancel)
            {
                return false;
            }
            string commonPath = pathInfo.CommonPath;
            string dest = pathInfo.UserCommonPath(specify, folder);
            if (!specify && !Directory.Exists(commonPath))
            {
                this.ShowModelessMessage("E577", commonPath, null);
                return false;
            }
            if (!this.TabPage_Save())
            {
                this.ShowModelessMessage("E576", commonPath, null);
                return false;
            }
            if (Directory.Exists(commonPath))
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    this.CopyFolder(commonPath, dest);
                    try
                    {
                        Directory.Delete(commonPath, true);
                    }
                    catch (Exception exception)
                    {
                        this.ShowModelessMessage("E579", commonPath, MessageDialog.CreateExceptionMessage(exception));
                    }
                }
                catch (Exception exception2)
                {
                    this.ShowModelessMessage("E578", commonPath, MessageDialog.CreateExceptionMessage(exception2));
                    return false;
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
            UserPathNames names = UserPathNames.CreateInstance();
            names.CommonPath.Specify = specify;
            names.CommonPath.Folder = folder;
            names.Save();
            names = UserPathNames.ResetInstance();
            pathInfo = PathInfo.ResetInstance();
            ConfigFiles.AllReset();
            this.TabPage_Load();
            return true;
        }

        private void CopyFolder(string src, string dest)
        {
            if (Directory.Exists(dest))
            {
                Directory.Delete(dest, true);
            }
            DirectoryInfo info = new DirectoryInfo(src);
            foreach (FileInfo info2 in info.GetFiles("*.*", SearchOption.AllDirectories))
            {
                string path = info2.FullName.Replace(src, dest);
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                }
                info2.CopyTo(path, true);
                FileAttributes fileAttributes = File.GetAttributes(path);
                if ((fileAttributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    fileAttributes &= ~FileAttributes.ReadOnly;
                }
                File.SetAttributes(path, fileAttributes);
                Application.DoEvents();
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridView dgv = (DataGridView) sender;
                if (!(dgv.CurrentCell is DataGridViewButtonCell))
                {
                    if ((dgv.CurrentCell is DataGridViewCheckBoxCell) && (dgv.Name == "dgvMsgCls"))
                    {
                        this.dgvMsgCls_CellClick(dgv);
                    }
                }
                else
                {
                    string name = dgv.Name;
                    if (name != null)
                    {
                        if (!(name == "dgvAutoPrint"))
                        {
                            if (!(name == "dgvMsgCls"))
                            {
                                if (!(name == "dgvFileSaveK"))
                                {
                                    if (name == "dgvFileSaveC")
                                    {
                                        this.dgvFileSaveC_CellClick(dgv);
                                    }
                                }
                                else
                                {
                                    this.dgvFileSaveK_CellClick(dgv);
                                }
                            }
                            else
                            {
                                this.dgvMsgCls_CellClick(dgv);
                            }
                        }
                        else
                        {
                            this.dgvAutoPrint_CellClick(dgv);
                        }
                    }
                }
            }
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView view = (DataGridView) sender;
            if ((view.CurrentCell is DataGridViewTextBoxCell) && (view.CurrentCell.Value != null))
            {
                view.CurrentCell.Value = view.CurrentCell.Value.ToString().ToUpper();
                if (((view == this.dgvAutoPrint) && (view.CurrentCellAddress.X == this.cAPCount.Index)) && ((view.CurrentCell.Value.ToString() != "0") && (view.CurrentCell.Value.ToString() != "00")))
                {
                    view.CurrentCell.Value = view.CurrentCell.Value.ToString().TrimStart(new char[] { '0' });
                }
            }
        }

        private void dgv_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            bool flag;
            if (!this.IsRowDelete())
            {
                DataGridView dgv = (DataGridView) sender;
                this.cdgv = dgv;
                flag = true;
                switch (dgv.Name)
                {
                    case "dgvAutoPrint":
                        flag = this.dgvAutoPrint_CellValidating(dgv);
                        goto Label_0107;

                    case "dgvMsgCls":
                        flag = this.dgvMsgCls_CellValidating(dgv);
                        goto Label_0107;

                    case "dgvFileSaveC":
                        flag = this.dgvFileSaveC_CellValidating(dgv);
                        goto Label_0107;

                    case "dgvReceiveInform":
                        flag = this.dgvReceiveInform_CellValidating(dgv);
                        goto Label_0107;

                    case "dgvUserKeyJ":
                    case "dgvUserKeyS":
                    case "dgvUserKeyR":
                    case "dgvUserKeyG":
                        flag = this.dgvUserKey_CellValidating(dgv);
                        goto Label_0107;
                }
            }
            return;
        Label_0107:
            if (!flag)
            {
                e.Cancel = true;
            }
        }

        private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void dgv_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridView dgv = (DataGridView) sender;
            string name = dgv.Name;
            if (name != null)
            {
                if (!(name == "dgvAutoPrint"))
                {
                    if (name == "dgvFileSaveC")
                    {
                        this.dgvFileSaveC_RowsAdded(dgv, e.RowIndex);
                    }
                }
                else
                {
                    this.dgvAutoPrint_RowsAdded(dgv, e.RowIndex);
                }
            }
            dgv.Focus();
            try
            {
                dgv.CurrentRow.Cells[0].Selected = true;
            }
            catch
            {
            }
        }

        private void dgv_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            this.bdnAPDeleteItem.Checked = false;
            this.bdnMCDeleteItem.Checked = false;
            this.bdnASCDeleteItem.Checked = false;
            this.bdnRNDeleteItem.Checked = false;
        }

        private void dgv_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (this.IsRowDelete())
            {
                return;
            }
            DataGridView dgv = (DataGridView) sender;
            this.cdgv = dgv;
            ErrInfo errInfo = ErrInfo.errInfo;
            string name = dgv.Name;
            if (name == null)
            {
                return;
            }
            if (!(name == "dgvAutoPrint"))
            {
                if (!(name == "dgvMsgCls"))
                {
                    if (!(name == "dgvFileSaveC"))
                    {
                        if (!(name == "dgvReceiveInform"))
                        {
                            if (!(name == "dgvUserKeyJ"))
                            {
                                return;
                            }
                            errInfo = this.dgvUserKey_RowValidating(dgv);
                        }
                        else
                        {
                            errInfo = this.dgvReceiveInform_RowValidating(dgv);
                        }
                    }
                    else
                    {
                        errInfo = this.dgvFileSaveC_RowValidating(dgv);
                    }
                    goto Label_009C;
                }
            }
            else
            {
                errInfo = this.dgvAutoPrint_RowValidating(dgv);
                goto Label_009C;
            }
            errInfo = this.dgvMsgCls_RowValidating(dgv);
        Label_009C:
            if (!errInfo.err_result)
            {
                dgv.CurrentCell = dgv.CurrentRow.Cells[errInfo.err_colidx];
                e.Cancel = true;
            }
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView) sender;
            if ((view.Focused && (view.CurrentCell != null)) && (view.CurrentCell is DataGridViewTextBoxCell))
            {
                view.BeginEdit(true);
            }
        }

        private void dgvAutoPrint_CellClick(DataGridView dgv)
        {
            PrinterSettings settings = new PrinterSettings();
            if (!settings.IsValid)
            {
                MessageDialog dialog = new MessageDialog();
                dialog.ShowMessage("E607", "");
                dialog.Close();
                dialog.Dispose();
            }
            else
            {
                DataGridViewRow currentRow = dgv.CurrentRow;
                PrinterTrayDlg dlg = null;
                try
                {
                    short num;
                    dlg = new PrinterTrayDlg();
                    if (short.TryParse(currentRow.Cells[this.cAPCount.Index].Value.ToString(), out num))
                    {
                        settings.Copies = num;
                    }
                    dlg.PrinterSettings = settings;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (this.IsEntryPrinter(dlg.PrinterSettings.PrinterName))
                        {
                            currentRow.Cells[this.cAPPrinter.Index].Value = dlg.PrinterSettings.PrinterName;
                            currentRow.Cells[this.cAPBinName.Index].Value = dlg.PrinterSettings.DefaultPageSettings.PaperSource.SourceName;
                            currentRow.Cells[this.cAPBinNo.Index].Value = dlg.PrinterSettings.DefaultPageSettings.PaperSource.RawKind;
                            if (dlg.PrinterSettings.Copies > 0x63)
                            {
                                currentRow.Cells[this.cAPCount.Index].Value = 0x63;
                            }
                            else
                            {
                                currentRow.Cells[this.cAPCount.Index].Value = dlg.PrinterSettings.Copies;
                            }
                        }
                        else
                        {
                            this.ShowModelessMessage("E553", null, null);
                        }
                    }
                }
                catch (Exception exception)
                {
                    MessageDialog dialog2 = new MessageDialog();
                    dialog2.ShowMessage("E611", dlg.PrinterSettings.PrinterName, null, exception);
                    dialog2.Close();
                    dialog2.Dispose();
                }
            }
        }

        private bool dgvAutoPrint_CellValidating(DataGridView dgv)
        {
            DataGridViewRow currentRow = dgv.CurrentRow;
            int x = dgv.CurrentCellAddress.X;
            if (x == this.cAPCount.Index)
            {
                if (string.IsNullOrEmpty(currentRow.Cells[this.cAPCount.Index].EditedFormattedValue.ToString().TrimStart(new char[] { '0' })))
                {
                    this.ShowModelessMessage("E554", null, null);
                    return false;
                }
                if (!this.Str_Decision(currentRow.Cells[this.cAPCount.Index].EditedFormattedValue.ToString(), this.cAPCount.MaxInputLength, StrType.IsDigit, OpeType.GE))
                {
                    this.ShowModelessMessage("E554", null, null);
                    return false;
                }
            }
            else if ((x == this.cAPOutCode.Index) && !this.OutCodeCheck(dgv, x, this.outcodelist_ap))
            {
                return false;
            }
            return true;
        }

        private void dgvAutoPrint_RowsAdded(DataGridView dgv, int rowidx)
        {
            DataGridViewRow row = dgv.Rows[rowidx];
            if (string.IsNullOrEmpty(row.Cells[this.cAPOutCode.Index].Value.ToString()))
            {
                row.Cells[this.cAPPrinter.Index].Value = this.txbDefaultPrinter.Text;
                row.Cells[this.cAPBinName.Index].Value = this.txbDefaultBin.Text;
                row.Cells[this.cAPBinNo.Index].Value = this.default_binno;
            }
        }

        private ErrInfo dgvAutoPrint_RowValidating(DataGridView dgv)
        {
            DataGridViewRow currentRow = dgv.CurrentRow;
            ErrInfo errInfo = ErrInfo.errInfo;
            if (currentRow.Cells[0].Value != null)
            {
                if (string.IsNullOrEmpty(currentRow.Cells[this.cAPPrinter.Index].EditedFormattedValue.ToString()))
                {
                    errInfo.err_colidx = this.cAPPrinter.Index;
                    errInfo.err_result = false;
                    this.ShowModelessMessage("E555", null, null);
                    return errInfo;
                }
                if (string.IsNullOrEmpty(currentRow.Cells[this.cAPOutCode.Index].EditedFormattedValue.ToString()))
                {
                    errInfo.err_colidx = this.cAPOutCode.Index;
                    errInfo.err_result = false;
                    this.ShowModelessMessage("E567", null, null);
                    return errInfo;
                }
            }
            return errInfo;
        }

        private void dgvFileSaveC_CellClick(DataGridView dgv)
        {
            DataGridViewRow currentRow = dgv.CurrentRow;
            FolderBrowserDialog dialog = new FolderBrowserDialog {
                SelectedPath = currentRow.Cells[this.cFSCDir.Index].Value.ToString()
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                currentRow.Cells[this.cFSCDir.Index].Value = dialog.SelectedPath;
            }
        }

        private bool dgvFileSaveC_CellValidating(DataGridView dgv)
        {
            DataGridViewRow currentRow = dgv.CurrentRow;
            int x = dgv.CurrentCellAddress.X;
            if ((x == this.cFSCOutCode.Index) && !this.OutCodeCheck(dgv, x, this.outcodelist_asc))
            {
                return false;
            }
            return true;
        }

        private void dgvFileSaveC_RowsAdded(DataGridView dgv, int rowidx)
        {
            DataGridViewRow row = dgv.Rows[rowidx];
            if (string.IsNullOrEmpty(row.Cells[this.cFSCOutCode.Index].Value.ToString()))
            {
                this.dgvFileSaveC.Select();
                row.Cells[this.cFSCAutoSave.Index].Value = true;
                row.Cells[this.cFSCDir.Index].Value = Path.Combine(pathInfo.FileSaveRoot, "RecvUser");
            }
        }

        private ErrInfo dgvFileSaveC_RowValidating(DataGridView dgv)
        {
            DataGridViewRow currentRow = dgv.CurrentRow;
            ErrInfo errInfo = ErrInfo.errInfo;
            if ((currentRow.Cells[0].Value != null) && string.IsNullOrEmpty(currentRow.Cells[this.cFSCOutCode.Index].EditedFormattedValue.ToString()))
            {
                errInfo.err_colidx = this.cFSCOutCode.Index;
                errInfo.err_result = false;
                this.ShowModelessMessage("E567", null, null);
                return errInfo;
            }
            return errInfo;
        }

        private void dgvFileSaveK_CellClick(DataGridView dgv)
        {
            DataGridViewRow currentRow = dgv.CurrentRow;
            FolderBrowserDialog dialog = new FolderBrowserDialog {
                SelectedPath = currentRow.Cells[this.cFSKDir.Index].Value.ToString()
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                currentRow.Cells[this.cFSKDir.Index].Value = dialog.SelectedPath;
            }
        }

        private void dgvMsgCls_CellClick(DataGridView dgv)
        {
            DataGridViewRow currentRow = dgv.CurrentRow;
            if (this.dgvMsgCls.CurrentCell.ColumnIndex == this.cMCBtn.Index)
            {
                dlgFolderSetting setting = new dlgFolderSetting {
                    BankWithdrawday = (bool) currentRow.Cells[this.cMCBankWithdrawday.Index].Value
                };
                if (setting.ShowDialog() == DialogResult.OK)
                {
                    currentRow.Cells[this.cMCFolder.Index].Value = setting.FolderName;
                    currentRow.Cells[this.cMCFolder.Index].Selected = true;
                    currentRow.Cells[this.cMCBtn.Index].Selected = true;
                }
                setting.Close();
                setting.Dispose();
            }
            if (((this.dgvMsgCls.CurrentCell.ColumnIndex == this.cMCBankWithdrawday.Index) && this.IsBank) && (((bool) currentRow.Cells[this.cMCBankWithdrawday.Index].EditedFormattedValue) && (currentRow.Cells[this.cMCFolder.Index].EditedFormattedValue.ToString().Split(new char[] { '\\' }).Length > 4)))
            {
                this.ShowModelessMessage("E564", null, null);
                this.dgvMsgCls.CancelEdit();
            }
        }

        private bool dgvMsgCls_CellValidating(DataGridView dgv)
        {
            DataGridViewRow currentRow = dgv.CurrentRow;
            int x = dgv.CurrentCellAddress.X;
            if (x == this.cMCOutCode.Index)
            {
                if (!this.OutCodeCheck(dgv, x, this.outcodelist_mc))
                {
                    return false;
                }
            }
            else if (x == this.cMCUserCode.Index)
            {
                if (!string.IsNullOrEmpty(currentRow.Cells[this.cMCUserCode.Index].EditedFormattedValue.ToString()) && !this.Str_Decision(currentRow.Cells[this.cMCUserCode.Index].EditedFormattedValue.ToString(), this.cMCUserCode.MaxInputLength, StrType.IsLetterOrDigit, OpeType.EQ))
                {
                    this.ShowModelessMessage("E574", null, null);
                    return false;
                }
            }
            else if (x == this.cMCJobCode.Index)
            {
                if ((!string.IsNullOrEmpty(currentRow.Cells[this.cMCJobCode.Index].EditedFormattedValue.ToString()) && !this.Str_Decision(currentRow.Cells[this.cMCJobCode.Index].EditedFormattedValue.ToString(), 5, StrType.IsLetterOrDigit, OpeType.EQ)) && !this.Str_Decision(currentRow.Cells[this.cMCJobCode.Index].EditedFormattedValue.ToString(), 3, StrType.IsLetterOrDigit, OpeType.EQ))
                {
                    this.ShowModelessMessage("E558", null, null);
                    return false;
                }
            }
            else if (this.IsBank)
            {
                if ((x == this.cMCBankNumber.Index) && !string.IsNullOrEmpty(currentRow.Cells[this.cMCBankNumber.Index].EditedFormattedValue.ToString()))
                {
                    if (string.IsNullOrEmpty(currentRow.Cells[this.cMCBankNumber.Index].EditedFormattedValue.ToString().TrimEnd(new char[0])))
                    {
                        this.ShowModelessMessage("E566", null, null);
                        return false;
                    }
                    if (!this.Str_Decision(currentRow.Cells[this.cMCBankNumber.Index].EditedFormattedValue.ToString(), this.cMCBankNumber.MaxInputLength, StrType.IsNACCS, OpeType.GE))
                    {
                        this.ShowModelessMessage("E561", null, null);
                        return false;
                    }
                }
                if (((x == this.cMCBankWithdrawday.Index) && ((bool) currentRow.Cells[this.cMCBankWithdrawday.Index].EditedFormattedValue)) && (currentRow.Cells[this.cMCFolder.Index].EditedFormattedValue.ToString().Split(new char[] { '\\' }).Length > 4))
                {
                    this.ShowModelessMessage("E564", null, null);
                    return false;
                }
            }
            return true;
        }

        private void dgvMsgCls_RowDataChange(int PreviousRow, int DestRow)
        {
            object[] objArray = new object[this.dgvMsgCls.ColumnCount];
            foreach (DataGridViewColumn column in this.dgvMsgCls.Columns)
            {
                objArray[column.Index] = this.dgvMsgCls.Rows[PreviousRow].Cells[column.Index].Value;
                this.dgvMsgCls.Rows[PreviousRow].Cells[column.Index].Value = this.dgvMsgCls.Rows[DestRow].Cells[column.Index].Value;
                this.dgvMsgCls.Rows[DestRow].Cells[column.Index].Value = objArray[column.Index];
            }
            string str = this.outcodelist_mc[PreviousRow];
            this.outcodelist_mc[PreviousRow] = this.outcodelist_mc[DestRow];
            this.outcodelist_mc[DestRow] = str;
        }

        private ErrInfo dgvMsgCls_RowValidating(DataGridView dgv)
        {
            DataGridViewRow currentRow = dgv.CurrentRow;
            ErrInfo errInfo = ErrInfo.errInfo;
            if (currentRow != null)
            {
                string str;
                if (currentRow.Cells[0].Value == null)
                {
                    return errInfo;
                }
                if (this.IsBank)
                {
                    str = currentRow.Cells[this.cMCUserCode.Index].EditedFormattedValue.ToString() + currentRow.Cells[this.cMCJobCode.Index].EditedFormattedValue.ToString() + currentRow.Cells[this.cMCOutCode.Index].EditedFormattedValue.ToString() + currentRow.Cells[this.cMCBankNumber.Index].EditedFormattedValue.ToString();
                }
                else
                {
                    str = currentRow.Cells[this.cMCUserCode.Index].EditedFormattedValue.ToString() + currentRow.Cells[this.cMCJobCode.Index].EditedFormattedValue.ToString() + currentRow.Cells[this.cMCOutCode.Index].EditedFormattedValue.ToString();
                }
                if (str.Length == 0)
                {
                    errInfo.err_colidx = 0;
                    errInfo.err_result = false;
                    this.ShowModelessMessage("E556", null, null);
                    return errInfo;
                }
                string[] checkStr = new string[] { currentRow.Cells[this.cMCUserCode.Index].EditedFormattedValue.ToString().ToUpper(), currentRow.Cells[this.cMCJobCode.Index].EditedFormattedValue.ToString().ToUpper(), currentRow.Cells[this.cMCOutCode.Index].EditedFormattedValue.ToString().ToUpper(), currentRow.Cells[this.cMCBankNumber.Index].EditedFormattedValue.ToString().ToUpper() };
                int num = this.FuriwakeCheck(checkStr);
                if (num > -1)
                {
                    errInfo.err_colidx = num;
                    errInfo.err_result = false;
                    this.ShowModelessMessage("E563", null, null);
                    return errInfo;
                }
                if (!string.IsNullOrEmpty(currentRow.Cells[this.cMCOutCode.Index].EditedFormattedValue.ToString()))
                {
                    if (!this.OutCodeCheck(dgv, this.cMCOutCode.Index, this.outcodelist_mc))
                    {
                        errInfo.err_colidx = this.cMCOutCode.Index;
                        errInfo.err_result = false;
                        return errInfo;
                    }
                }
                else if (!string.IsNullOrEmpty(currentRow.Cells[this.cMCUserCode.Index].EditedFormattedValue.ToString()))
                {
                    if (!this.Str_Decision(currentRow.Cells[this.cMCUserCode.Index].EditedFormattedValue.ToString(), this.cMCUserCode.MaxInputLength, StrType.IsLetterOrDigit, OpeType.EQ))
                    {
                        errInfo.err_colidx = this.cMCUserCode.Index;
                        errInfo.err_result = false;
                        this.ShowModelessMessage("E574", null, null);
                        return errInfo;
                    }
                }
                else if ((!string.IsNullOrEmpty(currentRow.Cells[this.cMCJobCode.Index].EditedFormattedValue.ToString()) && !this.Str_Decision(currentRow.Cells[this.cMCJobCode.Index].EditedFormattedValue.ToString(), 5, StrType.IsLetterOrDigit, OpeType.EQ)) && !this.Str_Decision(currentRow.Cells[this.cMCJobCode.Index].EditedFormattedValue.ToString(), 3, StrType.IsLetterOrDigit, OpeType.EQ))
                {
                    errInfo.err_colidx = this.cMCJobCode.Index;
                    errInfo.err_result = false;
                    return errInfo;
                }
                if (this.IsBank && !string.IsNullOrEmpty(currentRow.Cells[this.cMCBankNumber.Index].EditedFormattedValue.ToString()))
                {
                    if (string.IsNullOrEmpty(currentRow.Cells[this.cMCBankNumber.Index].EditedFormattedValue.ToString().TrimEnd(new char[0])))
                    {
                        errInfo.err_colidx = this.cMCBankNumber.Index;
                        errInfo.err_result = false;
                        return errInfo;
                    }
                    if (!this.Str_Decision(currentRow.Cells[this.cMCBankNumber.Index].EditedFormattedValue.ToString(), this.cMCBankNumber.MaxInputLength, StrType.IsNACCS, OpeType.GE))
                    {
                        errInfo.err_colidx = this.cMCBankNumber.Index;
                        errInfo.err_result = false;
                        this.ShowModelessMessage("E561", null, null);
                        return errInfo;
                    }
                }
                if (string.IsNullOrEmpty(currentRow.Cells[this.cMCFolder.Index].EditedFormattedValue.ToString()))
                {
                    errInfo.err_colidx = this.cMCBtn.Index;
                    errInfo.err_result = false;
                    this.ShowModelessMessage("E557", null, null);
                    return errInfo;
                }
            }
            return errInfo;
        }

        private bool dgvMsgCls_UpDownCheck()
        {
            if (!this.dgvMsgCls_CellValidating(this.dgvMsgCls))
            {
                return false;
            }
            ErrInfo info = this.dgvMsgCls_RowValidating(this.dgvMsgCls);
            if (!info.err_result)
            {
                this.dgvMsgCls.CurrentCell = this.dgvMsgCls.CurrentRow.Cells[info.err_colidx];
                return false;
            }
            return true;
        }

        private bool dgvReceiveInform_CellValidating(DataGridView dgv)
        {
            DataGridViewRow currentRow = dgv.CurrentRow;
            int x = dgv.CurrentCellAddress.X;
            if ((x == this.cRNOutCode.Index) && !this.OutCodeCheck(dgv, x, this.outcodelist_rn))
            {
                return false;
            }
            return true;
        }

        private ErrInfo dgvReceiveInform_RowValidating(DataGridView dgv)
        {
            DataGridViewRow currentRow = dgv.CurrentRow;
            ErrInfo errInfo = ErrInfo.errInfo;
            if ((currentRow.Cells[0].Value != null) && string.IsNullOrEmpty(currentRow.Cells[this.cRNOutCode.Index].EditedFormattedValue.ToString()))
            {
                errInfo.err_colidx = this.cRNOutCode.Index;
                errInfo.err_result = false;
                this.ShowModelessMessage("E567", null, null);
                return errInfo;
            }
            return errInfo;
        }

        private bool dgvUserKey_CellValidating(DataGridView dgv)
        {
            DataGridViewRow currentRow = dgv.CurrentRow;
            int x = dgv.CurrentCellAddress.X;
            if (dgv.Name == "dgvUserKeyJ")
            {
                if (((x == this.cKJGymCode.Index) && !string.IsNullOrEmpty(currentRow.Cells[x].EditedFormattedValue.ToString())) && (!this.Str_Decision(currentRow.Cells[x].EditedFormattedValue.ToString(), 3, StrType.IsLetterOrDigit, OpeType.EQ) && !this.Str_Decision(currentRow.Cells[x].EditedFormattedValue.ToString(), 5, StrType.IsLetterOrDigit, OpeType.EQ)))
                {
                    this.ShowModelessMessage("E558", null, null);
                    return false;
                }
                if (((x == this.cKJWindowCode.Index) && !string.IsNullOrEmpty(currentRow.Cells[x].EditedFormattedValue.ToString())) && !this.Str_Decision(currentRow.Cells[x].EditedFormattedValue.ToString(), 3, StrType.IsLetterOrDigit, OpeType.EQ))
                {
                    this.ShowModelessMessage("E568", null, null);
                    return false;
                }
                if (((x == this.cKJSckey.Index) && !string.IsNullOrEmpty(currentRow.Cells[x].EditedFormattedValue.ToString())) && !this.ShortcutKeyRepeatCheck(dgv, x))
                {
                    return false;
                }
            }
            else if ((x == this.cKSSckey.Index) && !this.ShortcutKeyRepeatCheck(dgv, x))
            {
                return false;
            }
            return true;
        }

        private bool dgvUserKey_GetKey(DataGridView dgv, string key)
        {
            int num = 1;
            if (dgv.Name == "dgvUserKeyJ")
            {
                num = 2;
            }
            foreach (DataGridViewRow row in (IEnumerable) dgv.Rows)
            {
                if (row.Cells[num].Value.ToString() == key)
                {
                    this.ShowModelessMessage("E559", "[" + dgv.Tag.ToString() + "]", null);
                    return false;
                }
            }
            return true;
        }

        private ErrInfo dgvUserKey_RowValidating(DataGridView dgv)
        {
            DataGridViewRow currentRow = dgv.CurrentRow;
            ErrInfo errInfo = ErrInfo.errInfo;
            if ((string.IsNullOrEmpty(currentRow.Cells[this.cKJGymCode.Index].EditedFormattedValue.ToString()) && string.IsNullOrEmpty(currentRow.Cells[this.cKJWindowCode.Index].EditedFormattedValue.ToString())) && string.IsNullOrEmpty(currentRow.Cells[this.cKJSckey.Index].EditedFormattedValue.ToString()))
            {
                errInfo.err_result = true;
                return errInfo;
            }
            if (!string.IsNullOrEmpty(currentRow.Cells[this.cKJGymCode.Index].EditedFormattedValue.ToString()) && !string.IsNullOrEmpty(currentRow.Cells[this.cKJSckey.Index].EditedFormattedValue.ToString()))
            {
                errInfo.err_result = true;
                return errInfo;
            }
            if ((!string.IsNullOrEmpty(currentRow.Cells[this.cKJGymCode.Index].EditedFormattedValue.ToString()) && !string.IsNullOrEmpty(currentRow.Cells[this.cKJWindowCode.Index].EditedFormattedValue.ToString())) && !string.IsNullOrEmpty(currentRow.Cells[this.cKJSckey.Index].EditedFormattedValue.ToString()))
            {
                errInfo.err_result = true;
                return errInfo;
            }
            if (!string.IsNullOrEmpty(currentRow.Cells[this.cKJGymCode.Index].EditedFormattedValue.ToString()))
            {
                errInfo.err_colidx = this.cKJSckey.Index;
                errInfo.err_result = false;
                this.ShowModelessMessage("E550", null, null);
                return errInfo;
            }
            if (!string.IsNullOrEmpty(currentRow.Cells[this.cKJWindowCode.Index].EditedFormattedValue.ToString()))
            {
                errInfo.err_colidx = this.cKJGymCode.Index;
                errInfo.err_result = false;
                return errInfo;
            }
            if (!string.IsNullOrEmpty(currentRow.Cells[this.cKJSckey.Index].EditedFormattedValue.ToString()))
            {
                errInfo.err_colidx = this.cKJGymCode.Index;
                errInfo.err_result = false;
                return errInfo;
            }
            return errInfo;
        }

        private void dialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.Enabled = true;
            if ((this.cdgv != null) && (this.cdgv.CurrentCell != null))
            {
                this.cdgv.BeginEdit(true);
            }
        }

        private bool DirectorySecurityChange(string dirPath)
        {
            try
            {
                DirectoryInfo info = new DirectoryInfo(dirPath);
                if (!info.Exists)
                {
                    info.Create();
                }
                DirectorySecurity accessControl = info.GetAccessControl();
                accessControl.AddAccessRule(new FileSystemAccessRule("everyone", FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow));
                info.SetAccessControl(accessControl);
            }
            catch
            {
                this.ShowModelessMessage("E572", null, null);
                this.rdbUseShareChange();
                return false;
            }
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DlgRes_No()
        {
            this.Cursor = Cursors.WaitCursor;
            user.Application.SettingsDiv = this.DivType;
            user.Save();
            ConfigFiles.AllReset();
            this.TabPage_Load();
            this.Cursor = Cursors.Default;
        }

        private void DlgRes_Yes()
        {
            messageClassify.Save();
            user.Application.SettingsDiv = this.DivType;
            user.Save();
            pathInfo = PathInfo.ResetInstance();
            messageClassify = MessageClassify.ResetInstance();
            this.LoadMsgCls();
            optionCertification = OptionCertification.ResetInstance();
            this.LoadPassword();
            this.btnRootAcpt_Click(null, new EventArgs());
        }

        private bool FormClosing_Check()
        {
            if (!this.cmbFileNameRule_Check())
            {
                return false;
            }
            if (!this.Str_Decision(this.txbTermLogicalName.Text, this.txbTermLogicalName.MaxLength, StrType.IsLetterOrDigit, OpeType.EQ))
            {
                this.tbcRoot.SelectTab(this.tbpTerm);
                this.ShowModelessMessage("E532", this.txbTermLogicalName.MaxLength.ToString(), null);
                this.txbTermLogicalName.Select();
                return false;
            }
            if (this.Protocol == ProtocolType.Mail)
            {
                if (this.ckbUseProxyM.Enabled && this.ckbUseProxyM.Checked)
                {
                    if (!this.Str_Decision(this.txbProxyNameM.Text, this.txbProxyNameM.MaxLength, StrType.IsSymbol, OpeType.GE))
                    {
                        this.tbcRoot.SelectTab(this.tbpServer);
                        this.ShowModelessMessage("E533", this.txbProxyNameM.MaxLength.ToString(), null);
                        this.txbProxyNameM.Focus();
                        return false;
                    }
                    if (!this.Str_Decision(this.txbProxyPortM.Text, this.txbProxyPortM.MaxLength, StrType.IsDigit, OpeType.GE))
                    {
                        this.tbcRoot.SelectTab(this.tbpServer);
                        this.ShowModelessMessage("E546", null, null);
                        this.txbProxyPortM.Focus();
                        return false;
                    }
                    if (int.Parse(this.txbProxyPortM.Text) == 0)
                    {
                        this.tbcRoot.SelectTab(this.tbpServer);
                        this.ShowModelessMessage("E546", null, null);
                        this.txbProxyPortM.Focus();
                        return false;
                    }
                }
                if (this.ckbUseProxyAccountM.Enabled && this.ckbUseProxyAccountM.Checked)
                {
                    if (!this.Str_Decision(this.txbProxyAccountM.Text, this.txbProxyAccountM.MaxLength, StrType.IsSymbol, OpeType.GE))
                    {
                        this.tbcRoot.SelectTab(this.tbpServer);
                        this.ShowModelessMessage("E533", this.txbProxyAccountM.MaxLength.ToString(), null);
                        this.txbProxyAccountM.Focus();
                        return false;
                    }
                    if (!this.Str_Decision(this.txbProxyPasswordM.Text, this.txbProxyPasswordM.MaxLength, StrType.IsSymbol, OpeType.GE))
                    {
                        this.tbcRoot.SelectTab(this.tbpServer);
                        this.ShowModelessMessage("E533", this.txbProxyAccountM.MaxLength.ToString(), null);
                        this.txbProxyPasswordM.Focus();
                        return false;
                    }
                }
            }
            else
            {
                if (!this.Str_Decision(this.txbTermAccessKey.Text, this.txbTermAccessKey.MaxLength, StrType.IsLetterOrDigit, OpeType.GE))
                {
                    this.tbcRoot.SelectTab(this.tbpTerm);
                    this.ShowModelessMessage("E533", this.txbTermAccessKey.MaxLength.ToString(), null);
                    this.txbTermAccessKey.Select();
                    return false;
                }
                if (this.ckbUseProxy.Enabled && this.ckbUseProxy.Checked)
                {
                    if (!this.Str_Decision(this.txbProxyName.Text, this.txbProxyName.MaxLength, StrType.IsSymbol, OpeType.GE))
                    {
                        this.tbcRoot.SelectTab(this.tbpServer);
                        this.ShowModelessMessage("E533", this.txbProxyName.MaxLength.ToString(), null);
                        this.txbProxyName.Focus();
                        return false;
                    }
                    if (!this.Str_Decision(this.txbProxyPort.Text, this.txbProxyPort.MaxLength, StrType.IsDigit, OpeType.GE))
                    {
                        this.tbcRoot.SelectTab(this.tbpServer);
                        this.ShowModelessMessage("E546", null, null);
                        this.txbProxyPort.Focus();
                        return false;
                    }
                    if (int.Parse(this.txbProxyPort.Text) == 0)
                    {
                        this.tbcRoot.SelectTab(this.tbpServer);
                        this.ShowModelessMessage("E546", null, null);
                        this.txbProxyPort.Focus();
                        return false;
                    }
                }
                if (this.ckbUseProxyAccount.Enabled && this.ckbUseProxyAccount.Checked)
                {
                    if (!this.Str_Decision(this.txbProxyAccount.Text, this.txbProxyAccount.MaxLength, StrType.IsSymbol, OpeType.GE))
                    {
                        this.tbcRoot.SelectTab(this.tbpServer);
                        this.ShowModelessMessage("E533", this.txbProxyAccount.MaxLength.ToString(), null);
                        this.txbProxyAccount.Focus();
                        return false;
                    }
                    if (!this.Str_Decision(this.txbProxyPassword.Text, this.txbProxyPassword.MaxLength, StrType.IsSymbol, OpeType.GE))
                    {
                        this.tbcRoot.SelectTab(this.tbpServer);
                        this.ShowModelessMessage("E533", this.txbProxyPassword.MaxLength.ToString(), null);
                        this.txbProxyPassword.Focus();
                        return false;
                    }
                }
            }
            return true;
        }

        private int FuriwakeCheck(string[] checkStr)
        {
            int num = -1;
            int num2 = 0;
            foreach (DataGridViewRow row in (IEnumerable) this.dgvMsgCls.Rows)
            {
                string[] strArray;
                if (this.IsBank)
                {
                    strArray = new string[] { row.Cells[this.cMCUserCode.Index].EditedFormattedValue.ToString().ToUpper(), row.Cells[this.cMCJobCode.Index].EditedFormattedValue.ToString().ToUpper(), row.Cells[this.cMCOutCode.Index].EditedFormattedValue.ToString().ToUpper(), row.Cells[this.cMCBankNumber.Index].EditedFormattedValue.ToString().ToUpper() };
                    if (((strArray[0] == checkStr[0]) && (strArray[1] == checkStr[1])) && ((strArray[2] == checkStr[2]) && (strArray[3] == checkStr[3])))
                    {
                        num2++;
                        if ((strArray[3] == checkStr[3]) && (strArray[3] != ""))
                        {
                            num = 3;
                        }
                        if ((strArray[2] == checkStr[2]) && (strArray[2] != ""))
                        {
                            num = 2;
                        }
                        if ((strArray[1] == checkStr[1]) && (strArray[1] != ""))
                        {
                            num = 1;
                        }
                        if ((strArray[0] == checkStr[0]) && (strArray[0] != ""))
                        {
                            num = 0;
                        }
                    }
                }
                else
                {
                    strArray = new string[] { row.Cells[this.cMCUserCode.Index].EditedFormattedValue.ToString().ToUpper(), row.Cells[this.cMCJobCode.Index].EditedFormattedValue.ToString().ToUpper(), row.Cells[this.cMCOutCode.Index].EditedFormattedValue.ToString().ToUpper() };
                    if (((strArray[0] == checkStr[0]) && (strArray[1] == checkStr[1])) && (strArray[2] == checkStr[2]))
                    {
                        num2++;
                        if ((strArray[2] == checkStr[2]) && (strArray[2] != ""))
                        {
                            num = 2;
                        }
                        if ((strArray[1] == checkStr[1]) && (strArray[1] != ""))
                        {
                            num = 1;
                        }
                        if ((strArray[0] == checkStr[0]) && (strArray[0] != ""))
                        {
                            num = 0;
                        }
                    }
                }
                if (num2 > 1)
                {
                    return num;
                }
            }
            return -1;
        }

        private void GetPrinterInfo(PrinterSettings ps)
        {
            BinClass class3;
            PrinterInfoClass item = new PrinterInfoClass {
                Name = ps.PrinterName,
                DoublePrint = false,
                BinList = new BinListClass()
            };
            if (ps.PaperSources.Count > 0)
            {
                foreach (PaperSource source in ps.PaperSources)
                {
                    class3 = new BinClass {
                        No = source.RawKind,
                        Name = source.SourceName
                    };
                    if (!this.IsErrorBinNo(item.BinList, class3.No))
                    {
                        item.BinList.Add(class3);
                    }
                }
            }
            else
            {
                PaperSource paperSource = ps.DefaultPageSettings.PaperSource;
                class3 = new BinClass {
                    No = paperSource.RawKind,
                    Name = paperSource.SourceName
                };
                item.BinList.Add(class3);
            }
            this.prtInfoList.Add(item);
        }

        private void GridError()
        {
            base.Enabled = true;
            this.Gdlg.Dispose();
            this.cdgv.EndEdit();
            if (this.cdgv.CurrentRow != null)
            {
                this.cdgv.BeginEdit(true);
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(OptionDialog));
            DataGridViewCellStyle style4 = new DataGridViewCellStyle();
            DataGridViewCellStyle style5 = new DataGridViewCellStyle();
            DataGridViewCellStyle style6 = new DataGridViewCellStyle();
            DataGridViewCellStyle style7 = new DataGridViewCellStyle();
            DataGridViewCellStyle style8 = new DataGridViewCellStyle();
            DataGridViewCellStyle style9 = new DataGridViewCellStyle();
            DataGridViewCellStyle style10 = new DataGridViewCellStyle();
            DataGridViewCellStyle style11 = new DataGridViewCellStyle();
            DataGridViewCellStyle style12 = new DataGridViewCellStyle();
            DataGridViewCellStyle style13 = new DataGridViewCellStyle();
            DataGridViewCellStyle style14 = new DataGridViewCellStyle();
            DataGridViewCellStyle style15 = new DataGridViewCellStyle();
            DataGridViewCellStyle style16 = new DataGridViewCellStyle();
            DataGridViewCellStyle style17 = new DataGridViewCellStyle();
            DataGridViewCellStyle style18 = new DataGridViewCellStyle();
            DataGridViewCellStyle style19 = new DataGridViewCellStyle();
            DataGridViewCellStyle style20 = new DataGridViewCellStyle();
            DataGridViewCellStyle style21 = new DataGridViewCellStyle();
            DataGridViewCellStyle style22 = new DataGridViewCellStyle();
            DataGridViewCellStyle style23 = new DataGridViewCellStyle();
            DataGridViewCellStyle style24 = new DataGridViewCellStyle();
            DataGridViewCellStyle style25 = new DataGridViewCellStyle();
            DataGridViewCellStyle style26 = new DataGridViewCellStyle();
            DataGridViewCellStyle style27 = new DataGridViewCellStyle();
            DataGridViewCellStyle style28 = new DataGridViewCellStyle();
            DataGridViewCellStyle style29 = new DataGridViewCellStyle();
            DataGridViewCellStyle style30 = new DataGridViewCellStyle();
            DataGridViewCellStyle style31 = new DataGridViewCellStyle();
            DataGridViewCellStyle style32 = new DataGridViewCellStyle();
            DataGridViewCellStyle style33 = new DataGridViewCellStyle();
            DataGridViewCellStyle style34 = new DataGridViewCellStyle();
            DataGridViewCellStyle style35 = new DataGridViewCellStyle();
            DataGridViewCellStyle style36 = new DataGridViewCellStyle();
            DataGridViewCellStyle style37 = new DataGridViewCellStyle();
            DataGridViewCellStyle style38 = new DataGridViewCellStyle();
            DataGridViewCellStyle style39 = new DataGridViewCellStyle();
            this.dtclmFSKDataName = new DataColumn();
            this.dtclmFSKDataType = new DataColumn();
            this.dtclmFSKFileSaveMode = new DataColumn();
            this.dtclmFSKDir = new DataColumn();
            this.dtclmFSKBtn = new DataColumn();
            this.bdsAutoPrint = new BindingSource(this.components);
            this.dtsOption = new DataSet();
            this.dttAutoPrint = new DataTable();
            this.dtclmAPOutCode = new DataColumn();
            this.dtclmAPMode = new DataColumn();
            this.dtclmAPCount = new DataColumn();
            this.dtclmAPPrinter = new DataColumn();
            this.dtclmAPBinName = new DataColumn();
            this.dtclmAPBtn = new DataColumn();
            this.dtclmAPBinNo = new DataColumn();
            this.dttMsgCls = new DataTable();
            this.dtclmMCId = new DataColumn();
            this.dtclmMCPriority = new DataColumn();
            this.dtclmMCUserCode = new DataColumn();
            this.dtclmMCJobCode = new DataColumn();
            this.dtclmMCOutCode = new DataColumn();
            this.dtclmMCUnOpen = new DataColumn();
            this.dtclmMCBankNumber = new DataColumn();
            this.dtclmMCBankWithdrawday = new DataColumn();
            this.dtclmMCFolder = new DataColumn();
            this.dtclmMCBtn = new DataColumn();
            this.dttFileSaveK = new DataTable();
            this.dttFileSaveC = new DataTable();
            this.dtclmFSCOutCode = new DataColumn();
            this.dtclmFSCAutoSave = new DataColumn();
            this.dtclmFSCDir = new DataColumn();
            this.dtclmFSCBtn = new DataColumn();
            this.dttRcvNotice = new DataTable();
            this.dtclmRNOutCode = new DataColumn();
            this.dtclmRNInform = new DataColumn();
            this.dtclmRNInformSound = new DataColumn();
            this.bdsFileSaveK = new BindingSource(this.components);
            this.bdsFileSaveC = new BindingSource(this.components);
            this.bdsRcvNotice = new BindingSource(this.components);
            this.btnRootCncl = new Button();
            this.btnRootOK = new Button();
            this.bdsMsgCls = new BindingSource(this.components);
            this.tbpHelp = new TabPage();
            this.txbJobErrCodeHtml = new TextBox();
            this.txbTrmErrCodeHtml = new TextBox();
            this.lblJobErrCodeHtml = new Label();
            this.lblTrmErrCodeHtml = new Label();
            this.lblHelpPath = new Label();
            this.tbpLog = new TabPage();
            this.gpbComLog = new GroupBox();
            this.nudComLogSize = new NumericUpDown();
            this.txbComLog2 = new TextBox();
            this.lblComLog2 = new Label();
            this.txbComLog1 = new TextBox();
            this.lblComLog1 = new Label();
            this.lblComLogSize = new Label();
            this.lblKb1 = new Label();
            this.tbpTerm = new TabPage();
            this.gpbCommonFolder = new GroupBox();
            this.ckbCommonFolder = new CheckBox();
            this.txbCommonFolder = new TextBox();
            this.gpbOptionShare = new GroupBox();
            this.rdbNoUseShare = new RadioButton();
            this.rdbUseShare = new RadioButton();
            this.nudDiskWarning = new NumericUpDown();
            this.nudSaveTerm = new NumericUpDown();
            this.ckbUseAutoBackup = new CheckBox();
            this.lblTermAccessKey = new Label();
            this.txbTermAccessKey = new TextBox();
            this.lblMb = new Label();
            this.gpbTerminalInfo = new GroupBox();
            this.txbUserKind = new TextBox();
            this.lblTermType = new Label();
            this.lblUserKind = new Label();
            this.txbTermType = new TextBox();
            this.lblHi = new Label();
            this.lblDiskWarning = new Label();
            this.lblTermLogicalName = new Label();
            this.txbTermLogicalName = new TextBox();
            this.lblSaveTerm = new Label();
            this.tbpUserKey = new TabPage();
            this.btnUkeyDflt = new Button();
            this.btnUkeyReset = new Button();
            this.tbcUserKey = new TabControl();
            this.tbpUserKeyJ = new TabPage();
            this.dgvUserKeyJ = new DataGridView();
            this.cKJGymCode = new DataGridViewTextBoxColumn();
            this.cKJWindowCode = new DataGridViewTextBoxColumn();
            this.cKJSckey = new DataGridViewComboBoxColumn();
            this.cKJNameKj = new DataGridViewTextBoxColumn();
            this.cKJName = new DataGridViewTextBoxColumn();
            this.bdsUserKeyJ = new BindingSource(this.components);
            this.dtsUserKey = new DataSet();
            this.dttUserKeyJ = new DataTable();
            this.dtclmKJNameKj = new DataColumn();
            this.dtclmKJSckey = new DataColumn();
            this.dtclmKJName = new DataColumn();
            this.dtclmKJGymCode = new DataColumn();
            this.dtclmKJWindowCode = new DataColumn();
            this.dttUserKeyS = new DataTable();
            this.dtclmKSNameKj = new DataColumn();
            this.dtclmKSSckey = new DataColumn();
            this.dtclmKSName = new DataColumn();
            this.dttUserKeyR = new DataTable();
            this.dtclmKRNameKj = new DataColumn();
            this.dtclmKRSckey = new DataColumn();
            this.dtclmKRName = new DataColumn();
            this.dttUserKeyG = new DataTable();
            this.dtclmKGNameKj = new DataColumn();
            this.dtclmKGSckey = new DataColumn();
            this.dtclmKGName = new DataColumn();
            this.bdnUserKeyJ = new BindingNavigator(this.components);
            this.bdnKJCountItem = new ToolStripLabel();
            this.bdnKJMoveFirstItem = new ToolStripButton();
            this.bdnKJMovePreviousItem = new ToolStripButton();
            this.bdnKJSeparator1 = new ToolStripSeparator();
            this.bdnKJPositionItem = new ToolStripTextBox();
            this.bdnKJSeparator2 = new ToolStripSeparator();
            this.bdnKJMoveNextItem = new ToolStripButton();
            this.bdnKJMoveLastItem = new ToolStripButton();
            this.tbpUserKeyS = new TabPage();
            this.dgvUserKeyS = new DataGridView();
            this.cKSNameKj = new DataGridViewTextBoxColumn();
            this.cKSSckey = new DataGridViewComboBoxColumn();
            this.bdsUserKeyS = new BindingSource(this.components);
            this.bdnUserKeyS = new BindingNavigator(this.components);
            this.bdnKSCountItem = new ToolStripLabel();
            this.bdnKSMoveFirstItem = new ToolStripButton();
            this.bdnKSMovePreviousItem = new ToolStripButton();
            this.bdnKSSeparator1 = new ToolStripSeparator();
            this.bdnKSPositionItem = new ToolStripTextBox();
            this.bdnKSSeparator2 = new ToolStripSeparator();
            this.bdnKSMoveNextItem = new ToolStripButton();
            this.bdnKSMoveLastItem = new ToolStripButton();
            this.tbpUserKeyR = new TabPage();
            this.dgvUserKeyR = new DataGridView();
            this.cKRNameKj = new DataGridViewTextBoxColumn();
            this.cKRSckey = new DataGridViewComboBoxColumn();
            this.bdsUserKeyR = new BindingSource(this.components);
            this.bdnUserKeyR = new BindingNavigator(this.components);
            this.bdnKRCountItem = new ToolStripLabel();
            this.bdnKRMoveFirstItem = new ToolStripButton();
            this.bdnKRMovePreviousItem = new ToolStripButton();
            this.bdnKRSeparator1 = new ToolStripSeparator();
            this.bdnKRPositionItem = new ToolStripTextBox();
            this.bdnKRSeparator2 = new ToolStripSeparator();
            this.bdnKRMoveNextItem = new ToolStripButton();
            this.bdnKRMoveLastItem = new ToolStripButton();
            this.tbpUserKeyG = new TabPage();
            this.dgvUserKeyG = new DataGridView();
            this.cKGNameKj = new DataGridViewTextBoxColumn();
            this.cKGSckey = new DataGridViewComboBoxColumn();
            this.bdsUserKeyG = new BindingSource(this.components);
            this.bdnUserKeyG = new BindingNavigator(this.components);
            this.bdnKGCountItem = new ToolStripLabel();
            this.bdnKGMoveFirstItem = new ToolStripButton();
            this.bdnKGMovePreviousItem = new ToolStripButton();
            this.bdnKGSeparator1 = new ToolStripSeparator();
            this.bdnKGPositionItem = new ToolStripTextBox();
            this.bdnKGSeparator2 = new ToolStripSeparator();
            this.bdnKGMoveNextItem = new ToolStripButton();
            this.bdnKGMoveLastItem = new ToolStripButton();
            this.tbpReceiveInform = new TabPage();
            this.ckbRcvInformSnd = new CheckBox();
            this.gpbReceiveInform = new GroupBox();
            this.gpbReceiveNotice = new GroupBox();
            this.dgvReceiveInform = new DataGridView();
            this.cRNOutCode = new DataGridViewTextBoxColumn();
            this.cRNInform = new DataGridViewCheckBoxColumn();
            this.cRNInformSound = new DataGridViewCheckBoxColumn();
            this.bdnRcvNotice = new BindingNavigator(this.components);
            this.bdnRNAddNewItem = new ToolStripButton();
            this.bdnRNCountItem = new ToolStripLabel();
            this.bdnRNDeleteItem = new ToolStripButton();
            this.bdnRNMoveFirstItem = new ToolStripButton();
            this.bdnRNMovePreviousItem = new ToolStripButton();
            this.bdnRNSeparator1 = new ToolStripSeparator();
            this.bdnRNPositionItem = new ToolStripTextBox();
            this.bdnRNSeparator2 = new ToolStripSeparator();
            this.bdnRNMoveNextItem = new ToolStripButton();
            this.bdnRNMoveLastItem = new ToolStripButton();
            this.bdnRNSeparator3 = new ToolStripSeparator();
            this.btnSoundFile = new Button();
            this.lblSoundFile = new Label();
            this.txbSoundFile = new TextBox();
            this.ckbRcvInform = new CheckBox();
            this.tbpFileSaveC = new TabPage();
            this.gpbFileSaveC = new GroupBox();
            this.dgvFileSaveC = new DataGridView();
            this.cFSCOutCode = new DataGridViewTextBoxColumn();
            this.cFSCAutoSave = new DataGridViewCheckBoxColumn();
            this.cFSCDir = new DataGridViewTextBoxColumn();
            this.cFSCBtn = new DataGridViewButtonColumn();
            this.bdnFileSaveC = new BindingNavigator(this.components);
            this.bdnASCAddNewItem = new ToolStripButton();
            this.bdnASCCountItem = new ToolStripLabel();
            this.bdnASCDeleteItem = new ToolStripButton();
            this.bdnASCMoveFirstItem = new ToolStripButton();
            this.bdnASCMovePreviousItem = new ToolStripButton();
            this.bdnASCSeparator1 = new ToolStripSeparator();
            this.bdnASCPositionItem = new ToolStripTextBox();
            this.bdnASCSeparator2 = new ToolStripSeparator();
            this.bdnASCMoveNextItem = new ToolStripButton();
            this.bdnASCMoveLastItem = new ToolStripButton();
            this.bdnASCSeparator3 = new ToolStripSeparator();
            this.tbpFileSaveK = new TabPage();
            this.gpbKanriFile = new GroupBox();
            this.btnKanriFile = new Button();
            this.txbKanriFile = new TextBox();
            this.ckbFileSave = new CheckBox();
            this.gpbFileSaveK = new GroupBox();
            this.dgvFileSaveK = new DataGridView();
            this.cFSKDataName = new DataGridViewTextBoxColumn();
            this.cFSKFileSaveMode = new DataGridViewCheckBoxColumn();
            this.cFSKDir = new DataGridViewTextBoxColumn();
            this.cFSKBtn = new DataGridViewButtonColumn();
            this.cFSKDataType = new DataGridViewTextBoxColumn();
            this.gpbSendFile = new GroupBox();
            this.btnSendFile = new Button();
            this.txbSendFile = new TextBox();
            this.gpbFileNameRule = new GroupBox();
            this.lblFileNameRule = new Label();
            this.lblPlus4 = new Label();
            this.cmbFileNameRule4 = new ComboBox();
            this.lblPlus3 = new Label();
            this.lblPlus2 = new Label();
            this.cmbFileNameRule3 = new ComboBox();
            this.lblPlus1 = new Label();
            this.cmbFileNameRule2 = new ComboBox();
            this.cmbFileNameRule1 = new ComboBox();
            this.tbpMsgCls = new TabPage();
            this.gpbMsgCls = new GroupBox();
            this.dgvMsgCls = new DataGridView();
            this.cMCUserCode = new DataGridViewTextBoxColumn();
            this.cMCJobCode = new DataGridViewTextBoxColumn();
            this.cMCOutCode = new DataGridViewTextBoxColumn();
            this.cMCBankNumber = new DataGridViewTextBoxColumn();
            this.cMCUnOpen = new DataGridViewCheckBoxColumn();
            this.cMCBankWithdrawday = new DataGridViewCheckBoxColumn();
            this.cMCFolder = new DataGridViewTextBoxColumn();
            this.cMCBtn = new DataGridViewButtonColumn();
            this.cMCId = new DataGridViewTextBoxColumn();
            this.cMCPriority = new DataGridViewTextBoxColumn();
            this.bdnMsgCls = new BindingNavigator(this.components);
            this.bdnMCAddNewItem = new ToolStripButton();
            this.bdnMCCountItem = new ToolStripLabel();
            this.bdnMCDeleteItem = new ToolStripButton();
            this.bdnMCMoveFirstItem = new ToolStripButton();
            this.bdnMCMovePreviousItem = new ToolStripButton();
            this.bdnMCSeparator1 = new ToolStripSeparator();
            this.bdnMCPositionItem = new ToolStripTextBox();
            this.bdnMCSeparator2 = new ToolStripSeparator();
            this.bdnMCMoveNextItem = new ToolStripButton();
            this.bdnMCMoveLastItem = new ToolStripButton();
            this.bdnMCSeparator3 = new ToolStripSeparator();
            this.tlsMCSeparator1 = new ToolStripSeparator();
            this.tlsMCbtnUp = new ToolStripButton();
            this.tlsMCbtnDown = new ToolStripButton();
            this.tbpAutoPrint = new TabPage();
            this.ckbAutoPrt = new CheckBox();
            this.gpbPrintout = new GroupBox();
            this.dgvAutoPrint = new DataGridView();
            this.cAPOutCode = new DataGridViewTextBoxColumn();
            this.cAPMode = new DataGridViewCheckBoxColumn();
            this.cAPCount = new DataGridViewTextBoxColumn();
            this.cAPPrinter = new DataGridViewTextBoxColumn();
            this.cAPBinName = new DataGridViewTextBoxColumn();
            this.cAPBinNo = new DataGridViewTextBoxColumn();
            this.cAPBtn = new DataGridViewButtonColumn();
            this.bdnAutoPrint = new BindingNavigator(this.components);
            this.bdnAPAddNewItem = new ToolStripButton();
            this.bdnAPCountItem = new ToolStripLabel();
            this.bdnAPDeleteItem = new ToolStripButton();
            this.bdnAPMoveFirstItem = new ToolStripButton();
            this.bdnAPMovePreviousItem = new ToolStripButton();
            this.bdnAPSeparator = new ToolStripSeparator();
            this.bdnAPPositionItem = new ToolStripTextBox();
            this.bdnAPSeparator1 = new ToolStripSeparator();
            this.bdnAPMoveNextItem = new ToolStripButton();
            this.bdnAPMoveLastItem = new ToolStripButton();
            this.bdnAPSeparator2 = new ToolStripSeparator();
            this.tbpPrinter = new TabPage();
            this.chbSizeWarning = new CheckBox();
            this.gpbDefaultPrinter = new GroupBox();
            this.btnDefaultPrinter = new Button();
            this.txbDefaultBin = new TextBox();
            this.lblDefaultBin = new Label();
            this.txbDefaultPrinter = new TextBox();
            this.lblDefaultPrinter = new Label();
            this.gpbEntryPrinter = new GroupBox();
            this.lsvPrt = new ListView();
            this.columnHeader2 = new ColumnHeader();
            this.lsvBin = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.ckbDoublePrt = new CheckBox();
            this.btnPrinterSet = new Button();
            this.btnEntryPrtDel = new Button();
            this.btnEntryPrtAdd = new Button();
            this.gpbMargin = new GroupBox();
            this.nudMarginL = new NumericUpDown();
            this.nudMarginT = new NumericUpDown();
            this.lblMarginL = new Label();
            this.lblMarginT = new Label();
            this.lblBin = new Label();
            this.lblPrinterName = new Label();
            this.tbpPassword = new TabPage();
            this.tbcPassword = new TabControl();
            this.tbpPwdChk = new TabPage();
            this.gpbPwdCert = new GroupBox();
            this.btnAcptCncl = new Button();
            this.btnAcptOK = new Button();
            this.txbPassword = new TextBox();
            this.lblPassword = new Label();
            this.tbpPwdChng = new TabPage();
            this.gpbPwdKind = new GroupBox();
            this.rdbPwdCncl = new RadioButton();
            this.rdbPwdSet = new RadioButton();
            this.rdbPwdChng = new RadioButton();
            this.lblNewPwdChk = new Label();
            this.txbNewPwdRe = new TextBox();
            this.lblNewPwdRe = new Label();
            this.txbNewPwd = new TextBox();
            this.lblNewPwd = new Label();
            this.btnPwdChngCncl = new Button();
            this.btnPwdChngOK = new Button();
            this.txbOldPwd = new TextBox();
            this.lblOldPwd = new Label();
            this.tbcRoot = new TabControl();
            this.tbpServer = new TabPage();
            this.pnlMail = new Panel();
            this.btnGateway = new Button();
            this.ckbUseGateway = new CheckBox();
            this.nudMaxRetreiveCnt = new NumericUpDown();
            this.gpbTraceM = new GroupBox();
            this.lblTraceFileM = new Label();
            this.txbTraceOutput2M = new TextBox();
            this.lblTraceOutput2M = new Label();
            this.txbTraceOutput1M = new TextBox();
            this.lblTraceOutput1M = new Label();
            this.ckbTraceOutputM = new CheckBox();
            this.gpbProxyM = new GroupBox();
            this.txbProxyPasswordM = new TextBox();
            this.lblProxyPasswordM = new Label();
            this.txbProxyAccountM = new TextBox();
            this.lblProxyAccountM = new Label();
            this.ckbUseProxyAccountM = new CheckBox();
            this.txbProxyPortM = new TextBox();
            this.lblProxyPortM = new Label();
            this.txbProxyNameM = new TextBox();
            this.lblProxyNameM = new Label();
            this.ckbUseProxyM = new CheckBox();
            this.lblKen = new Label();
            this.lblMaxRetreiveCnt = new Label();
            this.gpbAutoSndRcvTm = new GroupBox();
            this.nudAutoSndRcvTm = new NumericUpDown();
            this.ckbUseAutoSndRcvTm = new CheckBox();
            this.gpbAutoTmKind = new GroupBox();
            this.rdbAutoTmKindSR = new RadioButton();
            this.rdbAutoTmKindR = new RadioButton();
            this.rdbAutoTmKindS = new RadioButton();
            this.lblAutoSndRcvTm = new Label();
            this.lblMinM = new Label();
            this.cmbServerConnectM = new ComboBox();
            this.lblServerConnectM = new Label();
            this.pnlServer = new Panel();
            this.gpbProxy = new GroupBox();
            this.txbProxyPassword = new TextBox();
            this.lblProxyPassword = new Label();
            this.txbProxyAccount = new TextBox();
            this.lblProxyAccount = new Label();
            this.ckbUseProxyAccount = new CheckBox();
            this.txbProxyPort = new TextBox();
            this.lblProxyPort = new Label();
            this.txbProxyName = new TextBox();
            this.lblProxyName = new Label();
            this.ckbUseProxy = new CheckBox();
            this.gpbTrace = new GroupBox();
            this.lblTraceFile = new Label();
            this.txbTraceOutput2 = new TextBox();
            this.lblTraceOutput2 = new Label();
            this.txbTraceOutput1 = new TextBox();
            this.lblTraceOutput1 = new Label();
            this.ckbTraceOutput = new CheckBox();
            this.gpbCertificate = new GroupBox();
            this.btnCertificateSelect = new Button();
            this.txbCertificateDate = new TextBox();
            this.lblCertificateDate = new Label();
            this.txbIssuerName = new TextBox();
            this.lblIssuerName = new Label();
            this.cmbServerConnect = new ComboBox();
            this.lblServerConnect = new Label();
            this.gpbJobConnect = new GroupBox();
            this.nudAutoSendRecvTm = new NumericUpDown();
            this.ckbAutoSendRecvMode = new CheckBox();
            this.ckbJobConnect = new CheckBox();
            this.lblAutoSendRecvTm = new Label();
            this.lblMin = new Label();
            this.btnRootAcpt = new Button();
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
            this.dataGridViewTextBoxColumn25 = new DataGridViewTextBoxColumn();
            ((ISupportInitialize) this.bdsAutoPrint).BeginInit();
            this.dtsOption.BeginInit();
            this.dttAutoPrint.BeginInit();
            this.dttMsgCls.BeginInit();
            this.dttFileSaveK.BeginInit();
            this.dttFileSaveC.BeginInit();
            this.dttRcvNotice.BeginInit();
            ((ISupportInitialize) this.bdsFileSaveK).BeginInit();
            ((ISupportInitialize) this.bdsFileSaveC).BeginInit();
            ((ISupportInitialize) this.bdsRcvNotice).BeginInit();
            ((ISupportInitialize) this.bdsMsgCls).BeginInit();
            this.tbpHelp.SuspendLayout();
            this.tbpLog.SuspendLayout();
            this.gpbComLog.SuspendLayout();
            this.nudComLogSize.BeginInit();
            this.tbpTerm.SuspendLayout();
            this.gpbCommonFolder.SuspendLayout();
            this.gpbOptionShare.SuspendLayout();
            this.nudDiskWarning.BeginInit();
            this.nudSaveTerm.BeginInit();
            this.gpbTerminalInfo.SuspendLayout();
            this.tbpUserKey.SuspendLayout();
            this.tbcUserKey.SuspendLayout();
            this.tbpUserKeyJ.SuspendLayout();
            ((ISupportInitialize) this.dgvUserKeyJ).BeginInit();
            ((ISupportInitialize) this.bdsUserKeyJ).BeginInit();
            this.dtsUserKey.BeginInit();
            this.dttUserKeyJ.BeginInit();
            this.dttUserKeyS.BeginInit();
            this.dttUserKeyR.BeginInit();
            this.dttUserKeyG.BeginInit();
            this.bdnUserKeyJ.BeginInit();
            this.bdnUserKeyJ.SuspendLayout();
            this.tbpUserKeyS.SuspendLayout();
            ((ISupportInitialize) this.dgvUserKeyS).BeginInit();
            ((ISupportInitialize) this.bdsUserKeyS).BeginInit();
            this.bdnUserKeyS.BeginInit();
            this.bdnUserKeyS.SuspendLayout();
            this.tbpUserKeyR.SuspendLayout();
            ((ISupportInitialize) this.dgvUserKeyR).BeginInit();
            ((ISupportInitialize) this.bdsUserKeyR).BeginInit();
            this.bdnUserKeyR.BeginInit();
            this.bdnUserKeyR.SuspendLayout();
            this.tbpUserKeyG.SuspendLayout();
            ((ISupportInitialize) this.dgvUserKeyG).BeginInit();
            ((ISupportInitialize) this.bdsUserKeyG).BeginInit();
            this.bdnUserKeyG.BeginInit();
            this.bdnUserKeyG.SuspendLayout();
            this.tbpReceiveInform.SuspendLayout();
            this.gpbReceiveInform.SuspendLayout();
            this.gpbReceiveNotice.SuspendLayout();
            ((ISupportInitialize) this.dgvReceiveInform).BeginInit();
            this.bdnRcvNotice.BeginInit();
            this.bdnRcvNotice.SuspendLayout();
            this.tbpFileSaveC.SuspendLayout();
            this.gpbFileSaveC.SuspendLayout();
            ((ISupportInitialize) this.dgvFileSaveC).BeginInit();
            this.bdnFileSaveC.BeginInit();
            this.bdnFileSaveC.SuspendLayout();
            this.tbpFileSaveK.SuspendLayout();
            this.gpbKanriFile.SuspendLayout();
            this.gpbFileSaveK.SuspendLayout();
            ((ISupportInitialize) this.dgvFileSaveK).BeginInit();
            this.gpbSendFile.SuspendLayout();
            this.gpbFileNameRule.SuspendLayout();
            this.tbpMsgCls.SuspendLayout();
            this.gpbMsgCls.SuspendLayout();
            ((ISupportInitialize) this.dgvMsgCls).BeginInit();
            this.bdnMsgCls.BeginInit();
            this.bdnMsgCls.SuspendLayout();
            this.tbpAutoPrint.SuspendLayout();
            this.gpbPrintout.SuspendLayout();
            ((ISupportInitialize) this.dgvAutoPrint).BeginInit();
            this.bdnAutoPrint.BeginInit();
            this.bdnAutoPrint.SuspendLayout();
            this.tbpPrinter.SuspendLayout();
            this.gpbDefaultPrinter.SuspendLayout();
            this.gpbEntryPrinter.SuspendLayout();
            this.gpbMargin.SuspendLayout();
            this.nudMarginL.BeginInit();
            this.nudMarginT.BeginInit();
            this.tbpPassword.SuspendLayout();
            this.tbcPassword.SuspendLayout();
            this.tbpPwdChk.SuspendLayout();
            this.gpbPwdCert.SuspendLayout();
            this.tbpPwdChng.SuspendLayout();
            this.gpbPwdKind.SuspendLayout();
            this.tbcRoot.SuspendLayout();
            this.tbpServer.SuspendLayout();
            this.pnlMail.SuspendLayout();
            this.nudMaxRetreiveCnt.BeginInit();
            this.gpbTraceM.SuspendLayout();
            this.gpbProxyM.SuspendLayout();
            this.gpbAutoSndRcvTm.SuspendLayout();
            this.nudAutoSndRcvTm.BeginInit();
            this.gpbAutoTmKind.SuspendLayout();
            this.pnlServer.SuspendLayout();
            this.gpbProxy.SuspendLayout();
            this.gpbTrace.SuspendLayout();
            this.gpbCertificate.SuspendLayout();
            this.gpbJobConnect.SuspendLayout();
            this.nudAutoSendRecvTm.BeginInit();
            base.SuspendLayout();
            this.dtclmFSKDataName.Caption = "";
            this.dtclmFSKDataName.ColumnName = "clmFSKDataName";
            this.dtclmFSKDataName.DefaultValue = "";
            this.dtclmFSKDataType.Caption = "";
            this.dtclmFSKDataType.ColumnName = "clmFSKDataType";
            this.dtclmFSKDataType.DefaultValue = "";
            this.dtclmFSKFileSaveMode.Caption = "";
            this.dtclmFSKFileSaveMode.ColumnName = "clmFSKFileSaveMode";
            this.dtclmFSKFileSaveMode.DataType = typeof(bool);
            this.dtclmFSKFileSaveMode.DefaultValue = true;
            this.dtclmFSKDir.Caption = "";
            this.dtclmFSKDir.ColumnName = "clmFSKDir";
            this.dtclmFSKDir.DefaultValue = "";
            this.dtclmFSKBtn.Caption = "";
            this.dtclmFSKBtn.ColumnName = "clmFSKBtn";
            this.dtclmFSKBtn.DefaultValue = "Đường dẫn";
            this.bdsAutoPrint.DataMember = "dtAutoPrint";
            this.bdsAutoPrint.DataSource = this.dtsOption;
            this.dtsOption.DataSetName = "NewDataSet";
            this.dtsOption.Tables.AddRange(new DataTable[] { this.dttAutoPrint, this.dttMsgCls, this.dttFileSaveK, this.dttFileSaveC, this.dttRcvNotice });
            this.dttAutoPrint.Columns.AddRange(new DataColumn[] { this.dtclmAPOutCode, this.dtclmAPMode, this.dtclmAPCount, this.dtclmAPPrinter, this.dtclmAPBinName, this.dtclmAPBtn, this.dtclmAPBinNo });
            this.dttAutoPrint.TableName = "dtAutoPrint";
            this.dtclmAPOutCode.Caption = "";
            this.dtclmAPOutCode.ColumnName = "clmAPOutCode";
            this.dtclmAPOutCode.DefaultValue = "";
            this.dtclmAPMode.Caption = "";
            this.dtclmAPMode.ColumnName = "clmAPMode";
            this.dtclmAPMode.DataType = typeof(bool);
            this.dtclmAPMode.DefaultValue = true;
            this.dtclmAPCount.Caption = "";
            this.dtclmAPCount.ColumnName = "clmAPCount";
            this.dtclmAPCount.DefaultValue = "1";
            this.dtclmAPPrinter.Caption = "";
            this.dtclmAPPrinter.ColumnName = "clmAPPrinter";
            this.dtclmAPPrinter.DefaultValue = "";
            this.dtclmAPBinName.Caption = "";
            this.dtclmAPBinName.ColumnName = "clmAPBinName";
            this.dtclmAPBinName.DefaultValue = "";
            this.dtclmAPBtn.Caption = "";
            this.dtclmAPBtn.ColumnName = "clmAPBtn";
            this.dtclmAPBtn.DefaultValue = "Đường dẫn";
            this.dtclmAPBinNo.Caption = "";
            this.dtclmAPBinNo.ColumnName = "clmAPBinNo";
            this.dtclmAPBinNo.DefaultValue = "";
            this.dttMsgCls.Columns.AddRange(new DataColumn[] { this.dtclmMCId, this.dtclmMCPriority, this.dtclmMCUserCode, this.dtclmMCJobCode, this.dtclmMCOutCode, this.dtclmMCUnOpen, this.dtclmMCBankNumber, this.dtclmMCBankWithdrawday, this.dtclmMCFolder, this.dtclmMCBtn });
            this.dttMsgCls.TableName = "dtMsgCls";
            this.dtclmMCId.Caption = "";
            this.dtclmMCId.ColumnName = "clmMCId";
            this.dtclmMCId.DefaultValue = "";
            this.dtclmMCPriority.Caption = "";
            this.dtclmMCPriority.ColumnName = "clmMCPriority";
            this.dtclmMCPriority.DefaultValue = "";
            this.dtclmMCUserCode.Caption = "";
            this.dtclmMCUserCode.ColumnName = "clmMCUserCode";
            this.dtclmMCUserCode.DefaultValue = "";
            this.dtclmMCJobCode.Caption = "";
            this.dtclmMCJobCode.ColumnName = "clmMCJobCode";
            this.dtclmMCJobCode.DefaultValue = "";
            this.dtclmMCOutCode.Caption = "";
            this.dtclmMCOutCode.ColumnName = "clmMCOutCode";
            this.dtclmMCOutCode.DefaultValue = "";
            this.dtclmMCUnOpen.Caption = "";
            this.dtclmMCUnOpen.ColumnName = "clmMCUnOpen";
            this.dtclmMCUnOpen.DataType = typeof(bool);
            this.dtclmMCUnOpen.DefaultValue = false;
            this.dtclmMCBankNumber.Caption = "";
            this.dtclmMCBankNumber.ColumnName = "clmMCBankNumber";
            this.dtclmMCBankNumber.DefaultValue = "";
            this.dtclmMCBankWithdrawday.Caption = "";
            this.dtclmMCBankWithdrawday.ColumnName = "clmMCBankWithdrawday";
            this.dtclmMCBankWithdrawday.DataType = typeof(bool);
            this.dtclmMCBankWithdrawday.DefaultValue = false;
            this.dtclmMCFolder.Caption = "";
            this.dtclmMCFolder.ColumnName = "clmMCFolder";
            this.dtclmMCFolder.DefaultValue = "";
            this.dtclmMCBtn.Caption = "";
            this.dtclmMCBtn.ColumnName = "clmMCBtn";
            this.dtclmMCBtn.DefaultValue = "Đường dẫn";
            this.dttFileSaveK.Columns.AddRange(new DataColumn[] { this.dtclmFSKDataType, this.dtclmFSKFileSaveMode, this.dtclmFSKDir, this.dtclmFSKBtn, this.dtclmFSKDataName });
            this.dttFileSaveK.TableName = "dtFileSaveK";
            this.dttFileSaveC.Columns.AddRange(new DataColumn[] { this.dtclmFSCOutCode, this.dtclmFSCAutoSave, this.dtclmFSCDir, this.dtclmFSCBtn });
            this.dttFileSaveC.TableName = "dtFileSaveC";
            this.dtclmFSCOutCode.Caption = "";
            this.dtclmFSCOutCode.ColumnName = "clmFSCOutCode";
            this.dtclmFSCOutCode.DefaultValue = "";
            this.dtclmFSCAutoSave.Caption = "";
            this.dtclmFSCAutoSave.ColumnName = "clmFSCAutoSave";
            this.dtclmFSCAutoSave.DataType = typeof(bool);
            this.dtclmFSCAutoSave.DefaultValue = true;
            this.dtclmFSCDir.Caption = "";
            this.dtclmFSCDir.ColumnName = "clmFSCDir";
            this.dtclmFSCDir.DefaultValue = "";
            this.dtclmFSCBtn.Caption = "";
            this.dtclmFSCBtn.ColumnName = "clmFSCBtn";
            this.dtclmFSCBtn.DefaultValue = "Đường dẫn";
            this.dttRcvNotice.Columns.AddRange(new DataColumn[] { this.dtclmRNOutCode, this.dtclmRNInform, this.dtclmRNInformSound });
            this.dttRcvNotice.TableName = "dtRcvNotice";
            this.dtclmRNOutCode.ColumnName = "clmRNOutCode";
            this.dtclmRNOutCode.DefaultValue = "";
            this.dtclmRNInform.ColumnName = "clmRNInform";
            this.dtclmRNInform.DataType = typeof(bool);
            this.dtclmRNInform.DefaultValue = true;
            this.dtclmRNInformSound.ColumnName = "clmRNInformSound";
            this.dtclmRNInformSound.DataType = typeof(bool);
            this.dtclmRNInformSound.DefaultValue = false;
            this.bdsFileSaveK.DataMember = "dtFileSaveK";
            this.bdsFileSaveK.DataSource = this.dtsOption;
            this.bdsFileSaveC.DataMember = "dtFileSaveC";
            this.bdsFileSaveC.DataSource = this.dtsOption;
            this.bdsRcvNotice.DataMember = "dtRcvNotice";
            this.bdsRcvNotice.DataSource = this.dtsOption;
            this.btnRootCncl.CausesValidation = false;
            this.btnRootCncl.Location = new Point(0x341, 0x20a);
            this.btnRootCncl.Name = "btnRootCncl";
            this.btnRootCncl.Size = new Size(70, 0x17);
            this.btnRootCncl.TabIndex = 0x1a;
            this.btnRootCncl.Text = "Cancel";
            this.btnRootCncl.UseVisualStyleBackColor = true;
            this.btnRootCncl.Click += new EventHandler(this.btnRootCncl_Click);
            this.btnRootOK.Location = new Point(0x2a1, 0x20a);
            this.btnRootOK.Name = "btnRootOK";
            this.btnRootOK.Size = new Size(70, 0x17);
            this.btnRootOK.TabIndex = 0x18;
            this.btnRootOK.Text = "OK";
            this.btnRootOK.UseVisualStyleBackColor = true;
            this.btnRootOK.Click += new EventHandler(this.btnRootOK_Click);
            this.bdsMsgCls.DataMember = "dtMsgCls";
            this.bdsMsgCls.DataSource = this.dtsOption;
            this.tbpHelp.Controls.Add(this.txbJobErrCodeHtml);
            this.tbpHelp.Controls.Add(this.txbTrmErrCodeHtml);
            this.tbpHelp.Controls.Add(this.lblJobErrCodeHtml);
            this.tbpHelp.Controls.Add(this.lblTrmErrCodeHtml);
            this.tbpHelp.Controls.Add(this.lblHelpPath);
            this.tbpHelp.Location = new Point(4, 40);
            this.tbpHelp.Name = "tbpHelp";
            this.tbpHelp.Size = new Size(0x389, 0x1d4);
            this.tbpHelp.TabIndex = 11;
            this.tbpHelp.Text = "ヘルプ設定";
            this.tbpHelp.UseVisualStyleBackColor = true;
            this.txbJobErrCodeHtml.Location = new Point(0x9a, 0x39);
            this.txbJobErrCodeHtml.Name = "txbJobErrCodeHtml";
            this.txbJobErrCodeHtml.ReadOnly = true;
            this.txbJobErrCodeHtml.Size = new Size(0x1fa, 0x13);
            this.txbJobErrCodeHtml.TabIndex = 2;
            this.txbJobErrCodeHtml.TabStop = false;
            this.txbTrmErrCodeHtml.Location = new Point(0x99, 0x56);
            this.txbTrmErrCodeHtml.Name = "txbTrmErrCodeHtml";
            this.txbTrmErrCodeHtml.ReadOnly = true;
            this.txbTrmErrCodeHtml.Size = new Size(0x1fa, 0x13);
            this.txbTrmErrCodeHtml.TabIndex = 5;
            this.txbTrmErrCodeHtml.TabStop = false;
            this.lblJobErrCodeHtml.AutoSize = true;
            this.lblJobErrCodeHtml.Location = new Point(8, 60);
            this.lblJobErrCodeHtml.Name = "lblJobErrCodeHtml";
            this.lblJobErrCodeHtml.Size = new Size(0x56, 12);
            this.lblJobErrCodeHtml.TabIndex = 1;
            this.lblJobErrCodeHtml.Text = "業務メッセージ集";
            this.lblTrmErrCodeHtml.AutoSize = true;
            this.lblTrmErrCodeHtml.Location = new Point(8, 0x59);
            this.lblTrmErrCodeHtml.Name = "lblTrmErrCodeHtml";
            this.lblTrmErrCodeHtml.Size = new Size(0x85, 12);
            this.lblTrmErrCodeHtml.TabIndex = 4;
            this.lblTrmErrCodeHtml.Text = "パッケージソフトメッセージ集";
            this.lblHelpPath.AutoSize = true;
            this.lblHelpPath.Location = new Point(0x9e, 14);
            this.lblHelpPath.Name = "lblHelpPath";
            this.lblHelpPath.Size = new Size(0x6f, 12);
            this.lblHelpPath.TabIndex = 0;
            this.lblHelpPath.Text = "ヘルプファイルパス情報";
            this.tbpLog.Controls.Add(this.gpbComLog);
            this.tbpLog.Location = new Point(4, 40);
            this.tbpLog.Name = "tbpLog";
            this.tbpLog.Size = new Size(0x389, 0x1d4);
            this.tbpLog.TabIndex = 2;
            this.tbpLog.Text = "Nhật k\x00fd m\x00e1y";
            this.tbpLog.UseVisualStyleBackColor = true;
            this.gpbComLog.Controls.Add(this.nudComLogSize);
            this.gpbComLog.Controls.Add(this.txbComLog2);
            this.gpbComLog.Controls.Add(this.lblComLog2);
            this.gpbComLog.Controls.Add(this.txbComLog1);
            this.gpbComLog.Controls.Add(this.lblComLog1);
            this.gpbComLog.Controls.Add(this.lblComLogSize);
            this.gpbComLog.Controls.Add(this.lblKb1);
            this.gpbComLog.Location = new Point(15, 15);
            this.gpbComLog.Name = "gpbComLog";
            this.gpbComLog.Size = new Size(0x1ad, 0x6d);
            this.gpbComLog.TabIndex = 0;
            this.gpbComLog.TabStop = false;
            this.gpbComLog.Text = "Nhật k\x00fd truyền tin";
            this.nudComLogSize.Location = new Point(0x4c, 0x16);
            int[] bits = new int[4];
            bits[0] = 0x270f;
            this.nudComLogSize.Maximum = new decimal(bits);
            int[] numArray2 = new int[4];
            numArray2[0] = 0x10;
            this.nudComLogSize.Minimum = new decimal(numArray2);
            this.nudComLogSize.Name = "nudComLogSize";
            this.nudComLogSize.Size = new Size(0x3a, 0x13);
            this.nudComLogSize.TabIndex = 1;
            this.nudComLogSize.Tag = "";
            int[] numArray3 = new int[4];
            numArray3[0] = 0x3e8;
            this.nudComLogSize.Value = new decimal(numArray3);
            this.nudComLogSize.Validating += new CancelEventHandler(this.nud_Validating);
            this.txbComLog2.Location = new Point(0x73, 0x4d);
            this.txbComLog2.Name = "txbComLog2";
            this.txbComLog2.ReadOnly = true;
            this.txbComLog2.Size = new Size(0x132, 0x13);
            this.txbComLog2.TabIndex = 6;
            this.txbComLog2.TabStop = false;
            this.lblComLog2.Location = new Point(7, 80);
            this.lblComLog2.Name = "lblComLog2";
            this.lblComLog2.Size = new Size(0x66, 12);
            this.lblComLog2.TabIndex = 5;
            this.lblComLog2.Text = "T\x00ean tệp tin thứ hai";
            this.txbComLog1.Location = new Point(0x73, 0x33);
            this.txbComLog1.Name = "txbComLog1";
            this.txbComLog1.ReadOnly = true;
            this.txbComLog1.Size = new Size(0x132, 0x13);
            this.txbComLog1.TabIndex = 4;
            this.txbComLog1.TabStop = false;
            this.lblComLog1.Location = new Point(7, 0x36);
            this.lblComLog1.Name = "lblComLog1";
            this.lblComLog1.Size = new Size(0x6c, 12);
            this.lblComLog1.TabIndex = 3;
            this.lblComLog1.Text = "T\x00ean tệp tin thứ nhất";
            this.lblComLogSize.Location = new Point(7, 0x19);
            this.lblComLogSize.Name = "lblComLogSize";
            this.lblComLogSize.Size = new Size(0x40, 12);
            this.lblComLogSize.TabIndex = 0;
            this.lblComLogSize.Text = "K\x00edch thước";
            this.lblKb1.AutoSize = true;
            this.lblKb1.Location = new Point(140, 0x19);
            this.lblKb1.Name = "lblKb1";
            this.lblKb1.Size = new Size(20, 12);
            this.lblKb1.TabIndex = 2;
            this.lblKb1.Text = "KB";
            this.tbpTerm.Controls.Add(this.gpbCommonFolder);
            this.tbpTerm.Controls.Add(this.gpbOptionShare);
            this.tbpTerm.Controls.Add(this.nudDiskWarning);
            this.tbpTerm.Controls.Add(this.nudSaveTerm);
            this.tbpTerm.Controls.Add(this.ckbUseAutoBackup);
            this.tbpTerm.Controls.Add(this.lblTermAccessKey);
            this.tbpTerm.Controls.Add(this.txbTermAccessKey);
            this.tbpTerm.Controls.Add(this.lblMb);
            this.tbpTerm.Controls.Add(this.gpbTerminalInfo);
            this.tbpTerm.Controls.Add(this.lblHi);
            this.tbpTerm.Controls.Add(this.lblDiskWarning);
            this.tbpTerm.Controls.Add(this.lblTermLogicalName);
            this.tbpTerm.Controls.Add(this.txbTermLogicalName);
            this.tbpTerm.Controls.Add(this.lblSaveTerm);
            this.tbpTerm.Location = new Point(4, 40);
            this.tbpTerm.Name = "tbpTerm";
            this.tbpTerm.Padding = new Padding(3);
            this.tbpTerm.Size = new Size(0x389, 0x1d4);
            this.tbpTerm.TabIndex = 0;
            this.tbpTerm.Text = "M\x00e1y trạm";
            this.tbpTerm.UseVisualStyleBackColor = true;
            this.gpbCommonFolder.Controls.Add(this.ckbCommonFolder);
            this.gpbCommonFolder.Controls.Add(this.txbCommonFolder);
            this.gpbCommonFolder.Enabled = false;
            this.gpbCommonFolder.Location = new Point(0x11, 0xe4);
            this.gpbCommonFolder.Name = "gpbCommonFolder";
            this.gpbCommonFolder.Size = new Size(0x1e3, 0x47);
            this.gpbCommonFolder.TabIndex = 14;
            this.gpbCommonFolder.TabStop = false;
            this.gpbCommonFolder.Text = "đường dẫn của tập tin t\x00f9y chọn tiết kiệm";
            this.gpbCommonFolder.Visible = false;
            this.ckbCommonFolder.AutoSize = true;
            this.ckbCommonFolder.Enabled = false;
            this.ckbCommonFolder.Location = new Point(20, 0x16);
            this.ckbCommonFolder.Name = "ckbCommonFolder";
            this.ckbCommonFolder.Size = new Size(0xba, 0x10);
            this.ckbCommonFolder.TabIndex = 0;
            this.ckbCommonFolder.Text = "Lưu tập tin v\x00e0o thư mục sau đ\x00e2y";
            this.ckbCommonFolder.UseVisualStyleBackColor = true;
            this.ckbCommonFolder.Visible = false;
            this.txbCommonFolder.Enabled = false;
            this.txbCommonFolder.Location = new Point(20, 0x2a);
            this.txbCommonFolder.Name = "txbCommonFolder";
            this.txbCommonFolder.ReadOnly = true;
            this.txbCommonFolder.Size = new Size(0x1c0, 0x13);
            this.txbCommonFolder.TabIndex = 1;
            this.txbCommonFolder.TabStop = false;
            this.txbCommonFolder.Visible = false;
            this.gpbOptionShare.Controls.Add(this.rdbNoUseShare);
            this.gpbOptionShare.Controls.Add(this.rdbUseShare);
            this.gpbOptionShare.Location = new Point(0x11, 0x55);
            this.gpbOptionShare.Name = "gpbOptionShare";
            this.gpbOptionShare.Size = new Size(180, 50);
            this.gpbOptionShare.TabIndex = 5;
            this.gpbOptionShare.TabStop = false;
            this.gpbOptionShare.Text = "Sử dụng c\x00e1c c\x00e0i đặt chung";
            this.rdbNoUseShare.AutoSize = true;
            this.rdbNoUseShare.Checked = true;
            this.rdbNoUseShare.Location = new Point(100, 0x15);
            this.rdbNoUseShare.Name = "rdbNoUseShare";
            this.rdbNoUseShare.Size = new Size(0x25, 0x10);
            this.rdbNoUseShare.TabIndex = 0;
            this.rdbNoUseShare.TabStop = true;
            this.rdbNoUseShare.Text = "No";
            this.rdbNoUseShare.UseVisualStyleBackColor = true;
            this.rdbNoUseShare.Click += new EventHandler(this.rdbShare_Click);
            this.rdbUseShare.AutoSize = true;
            this.rdbUseShare.Location = new Point(20, 0x15);
            this.rdbUseShare.Name = "rdbUseShare";
            this.rdbUseShare.Size = new Size(0x2a, 0x10);
            this.rdbUseShare.TabIndex = 1;
            this.rdbUseShare.TabStop = true;
            this.rdbUseShare.Text = "Yes";
            this.rdbUseShare.UseVisualStyleBackColor = true;
            this.rdbUseShare.Click += new EventHandler(this.rdbShare_Click);
            this.nudDiskWarning.Location = new Point(0x173, 0x9c);
            int[] numArray4 = new int[4];
            numArray4[0] = 0x3e7;
            this.nudDiskWarning.Maximum = new decimal(numArray4);
            int[] numArray5 = new int[4];
            numArray5[0] = 60;
            this.nudDiskWarning.Minimum = new decimal(numArray5);
            this.nudDiskWarning.Name = "nudDiskWarning";
            this.nudDiskWarning.Size = new Size(0x48, 0x13);
            this.nudDiskWarning.TabIndex = 10;
            this.nudDiskWarning.Tag = "";
            int[] numArray6 = new int[4];
            numArray6[0] = 100;
            this.nudDiskWarning.Value = new decimal(numArray6);
            this.nudDiskWarning.Validating += new CancelEventHandler(this.nud_Validating);
            this.nudSaveTerm.Location = new Point(0x9b, 0x9c);
            int[] numArray7 = new int[4];
            numArray7[0] = 0x63;
            this.nudSaveTerm.Maximum = new decimal(numArray7);
            int[] numArray8 = new int[4];
            numArray8[0] = 1;
            this.nudSaveTerm.Minimum = new decimal(numArray8);
            this.nudSaveTerm.Name = "nudSaveTerm";
            this.nudSaveTerm.Size = new Size(40, 0x13);
            this.nudSaveTerm.TabIndex = 7;
            this.nudSaveTerm.Tag = "";
            int[] numArray9 = new int[4];
            numArray9[0] = 1;
            this.nudSaveTerm.Value = new decimal(numArray9);
            this.nudSaveTerm.Validating += new CancelEventHandler(this.nud_Validating);
            this.ckbUseAutoBackup.Location = new Point(0x11, 0xbf);
            this.ckbUseAutoBackup.Name = "ckbUseAutoBackup";
            this.ckbUseAutoBackup.Size = new Size(120, 0x10);
            this.ckbUseAutoBackup.TabIndex = 12;
            this.ckbUseAutoBackup.Text = "Tự động lưu trữ";
            this.ckbUseAutoBackup.UseVisualStyleBackColor = true;
            this.lblTermAccessKey.Location = new Point(15, 0x37);
            this.lblTermAccessKey.Name = "lblTermAccessKey";
            this.lblTermAccessKey.Size = new Size(0x5f, 12);
            this.lblTermAccessKey.TabIndex = 2;
            this.lblTermAccessKey.Text = "Kh\x00f3a truy cập";
            this.txbTermAccessKey.CharacterCasing = CharacterCasing.Upper;
//            this.txbTermAccessKey.ImeMode = ImeMode.Disable;
            this.txbTermAccessKey.Location = new Point(0x73, 0x34);
            this.txbTermAccessKey.MaxLength = 0x10;
            this.txbTermAccessKey.Name = "txbTermAccessKey";
            this.txbTermAccessKey.Size = new Size(0xb9, 0x13);
            this.txbTermAccessKey.TabIndex = 3;
            this.txbTermAccessKey.Tag = "Access key";
            this.lblMb.AutoSize = true;
            this.lblMb.Location = new Point(0x1c1, 0x9e);
            this.lblMb.Name = "lblMb";
            this.lblMb.Size = new Size(0x16, 12);
            this.lblMb.TabIndex = 11;
            this.lblMb.Text = "MB";
            this.gpbTerminalInfo.Controls.Add(this.txbUserKind);
            this.gpbTerminalInfo.Controls.Add(this.lblTermType);
            this.gpbTerminalInfo.Controls.Add(this.lblUserKind);
            this.gpbTerminalInfo.Controls.Add(this.txbTermType);
            this.gpbTerminalInfo.Location = new Point(0x113, 6);
            this.gpbTerminalInfo.Name = "gpbTerminalInfo";
            this.gpbTerminalInfo.Size = new Size(0xe1, 0x49);
            this.gpbTerminalInfo.TabIndex = 4;
            this.gpbTerminalInfo.TabStop = false;
            this.gpbTerminalInfo.Text = "端末定義";
            this.gpbTerminalInfo.Visible = false;
            this.txbUserKind.Location = new Point(0x5e, 0x2e);
            this.txbUserKind.Name = "txbUserKind";
            this.txbUserKind.ReadOnly = true;
            this.txbUserKind.Size = new Size(0x3b, 0x13);
            this.txbUserKind.TabIndex = 3;
            this.txbUserKind.TabStop = false;
            this.lblTermType.AutoSize = true;
            this.lblTermType.Location = new Point(0x17, 0x18);
            this.lblTermType.Name = "lblTermType";
            this.lblTermType.Size = new Size(0x35, 12);
            this.lblTermType.TabIndex = 0;
            this.lblTermType.Text = "端末種別";
            this.lblUserKind.AutoSize = true;
            this.lblUserKind.Location = new Point(0x17, 0x31);
            this.lblUserKind.Name = "lblUserKind";
            this.lblUserKind.Size = new Size(0x41, 12);
            this.lblUserKind.TabIndex = 2;
            this.lblUserKind.Text = "利用者区分";
            this.txbTermType.Location = new Point(0x5e, 0x15);
            this.txbTermType.Name = "txbTermType";
            this.txbTermType.ReadOnly = true;
            this.txbTermType.Size = new Size(0x4a, 0x13);
            this.txbTermType.TabIndex = 1;
            this.txbTermType.TabStop = false;
            this.lblHi.AutoSize = true;
            this.lblHi.Location = new Point(0xc9, 0x9e);
            this.lblHi.Name = "lblHi";
            this.lblHi.Size = new Size(0x1f, 12);
            this.lblHi.TabIndex = 8;
            this.lblHi.Text = "Ng\x00e0y";
            this.lblDiskWarning.Location = new Point(0x10a, 0x9e);
            this.lblDiskWarning.Name = "lblDiskWarning";
            this.lblDiskWarning.Size = new Size(0x5f, 12);
            this.lblDiskWarning.TabIndex = 9;
            this.lblDiskWarning.Text = "Dung lượng lưu trữ";
            this.lblTermLogicalName.Location = new Point(15, 30);
            this.lblTermLogicalName.Name = "lblTermLogicalName";
            this.lblTermLogicalName.Size = new Size(0x5f, 12);
            this.lblTermLogicalName.TabIndex = 0;
            this.lblTermLogicalName.Text = "M\x00e3 số m\x00e1y trạm";
            this.txbTermLogicalName.CharacterCasing = CharacterCasing.Upper;
//            this.txbTermLogicalName.ImeMode = ImeMode.Disable;
            this.txbTermLogicalName.Location = new Point(0x73, 0x1b);
            this.txbTermLogicalName.MaxLength = 6;
            this.txbTermLogicalName.Name = "txbTermLogicalName";
            this.txbTermLogicalName.Size = new Size(0x4d, 0x13);
            this.txbTermLogicalName.TabIndex = 1;
            this.txbTermLogicalName.Tag = "Logical terminal name";
            this.lblSaveTerm.Location = new Point(15, 0x9e);
            this.lblSaveTerm.Name = "lblSaveTerm";
            this.lblSaveTerm.Size = new Size(0x86, 12);
            this.lblSaveTerm.TabIndex = 6;
            this.lblSaveTerm.Text = "Thời gian lưu trữ tin nhắn";
            this.tbpUserKey.Controls.Add(this.btnUkeyDflt);
            this.tbpUserKey.Controls.Add(this.btnUkeyReset);
            this.tbpUserKey.Controls.Add(this.tbcUserKey);
            this.tbpUserKey.Location = new Point(4, 40);
            this.tbpUserKey.Name = "tbpUserKey";
            this.tbpUserKey.Size = new Size(0x389, 0x1d4);
            this.tbpUserKey.TabIndex = 9;
            this.tbpUserKey.Text = "Ph\x00edm tắt";
            this.tbpUserKey.UseVisualStyleBackColor = true;
            this.btnUkeyDflt.CausesValidation = false;
            this.btnUkeyDflt.Location = new Point(0x237, 0x3e);
            this.btnUkeyDflt.Name = "btnUkeyDflt";
            this.btnUkeyDflt.Size = new Size(0x65, 0x17);
            this.btnUkeyDflt.TabIndex = 3;
            this.btnUkeyDflt.Text = "Mặc định";
            this.btnUkeyDflt.UseVisualStyleBackColor = true;
            this.btnUkeyDflt.Click += new EventHandler(this.btnUkeyDflt_Click);
            this.btnUkeyReset.CausesValidation = false;
            this.btnUkeyReset.Location = new Point(0x237, 0x6b);
            this.btnUkeyReset.Name = "btnUkeyReset";
            this.btnUkeyReset.Size = new Size(0x65, 0x17);
            this.btnUkeyReset.TabIndex = 4;
            this.btnUkeyReset.Text = "X\x00f3a to\x00e0n bộ";
            this.btnUkeyReset.UseVisualStyleBackColor = true;
            this.btnUkeyReset.Click += new EventHandler(this.btnUkeyReset_Click);
            this.tbcUserKey.Controls.Add(this.tbpUserKeyJ);
            this.tbcUserKey.Controls.Add(this.tbpUserKeyS);
            this.tbcUserKey.Controls.Add(this.tbpUserKeyR);
            this.tbcUserKey.Controls.Add(this.tbpUserKeyG);
            this.tbcUserKey.Location = new Point(8, 7);
            this.tbcUserKey.Name = "tbcUserKey";
            this.tbcUserKey.SelectedIndex = 0;
            this.tbcUserKey.Size = new Size(0x1fc, 0x1b9);
            this.tbcUserKey.TabIndex = 0;
            this.tbcUserKey.TabStop = false;
            this.tbpUserKeyJ.Controls.Add(this.dgvUserKeyJ);
            this.tbpUserKeyJ.Controls.Add(this.bdnUserKeyJ);
            this.tbpUserKeyJ.Location = new Point(4, 0x16);
            this.tbpUserKeyJ.Name = "tbpUserKeyJ";
            this.tbpUserKeyJ.Size = new Size(500, 0x19f);
            this.tbpUserKeyJ.TabIndex = 0;
            this.tbpUserKeyJ.Text = "Nghiệp vụ";
            this.tbpUserKeyJ.UseVisualStyleBackColor = true;
            this.dgvUserKeyJ.AllowUserToAddRows = false;
            this.dgvUserKeyJ.AllowUserToDeleteRows = false;
            style.BackColor = SystemColors.GradientInactiveCaption;
            this.dgvUserKeyJ.AlternatingRowsDefaultCellStyle = style;
            this.dgvUserKeyJ.AutoGenerateColumns = false;
            this.dgvUserKeyJ.BackgroundColor = SystemColors.Control;
            style2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style2.BackColor = SystemColors.Control;
            style2.ForeColor = SystemColors.WindowText;
            style2.SelectionBackColor = SystemColors.Highlight;
            style2.SelectionForeColor = SystemColors.HighlightText;
            style2.WrapMode = DataGridViewTriState.True;
            this.dgvUserKeyJ.ColumnHeadersDefaultCellStyle = style2;
            this.dgvUserKeyJ.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserKeyJ.Columns.AddRange(new DataGridViewColumn[] { this.cKJGymCode, this.cKJWindowCode, this.cKJSckey, this.cKJNameKj, this.cKJName });
            this.dgvUserKeyJ.DataSource = this.bdsUserKeyJ;
            this.dgvUserKeyJ.Dock = DockStyle.Fill;
            this.dgvUserKeyJ.Location = new Point(0, 0x19);
            this.dgvUserKeyJ.Name = "dgvUserKeyJ";
            style3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style3.BackColor = SystemColors.Control;
            style3.ForeColor = SystemColors.WindowText;
            style3.SelectionBackColor = SystemColors.Highlight;
            style3.SelectionForeColor = SystemColors.HighlightText;
            style3.WrapMode = DataGridViewTriState.True;
            this.dgvUserKeyJ.RowHeadersDefaultCellStyle = style3;
            this.dgvUserKeyJ.RowTemplate.Height = 0x15;
            this.dgvUserKeyJ.Size = new Size(500, 390);
            this.dgvUserKeyJ.TabIndex = 2;
            this.dgvUserKeyJ.Tag = "JOB key";
            this.dgvUserKeyJ.CellEndEdit += new DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            this.dgvUserKeyJ.CellValidating += new DataGridViewCellValidatingEventHandler(this.dgv_CellValidating);
            this.dgvUserKeyJ.RowValidating += new DataGridViewCellCancelEventHandler(this.dgv_RowValidating);
            this.dgvUserKeyJ.SelectionChanged += new EventHandler(this.dgv_SelectionChanged);
            this.cKJGymCode.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.cKJGymCode.DataPropertyName = "clmKJGymCode";
            this.cKJGymCode.FillWeight = 347f;
            this.cKJGymCode.HeaderText = "M\x00e3 nghiệp vụ";
            this.cKJGymCode.MaxInputLength = 5;
            this.cKJGymCode.Name = "cKJGymCode";
            this.cKJGymCode.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.cKJWindowCode.DataPropertyName = "clmKJWindowCode";
            this.cKJWindowCode.HeaderText = "M\x00e3 m\x00e0n h\x00ecnh";
            this.cKJWindowCode.MaxInputLength = 3;
            this.cKJWindowCode.Name = "cKJWindowCode";
            this.cKJWindowCode.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.cKJWindowCode.Visible = false;
            this.cKJSckey.DataPropertyName = "clmKJSckey";
            this.cKJSckey.HeaderText = "Ph\x00edm tắt";
            this.cKJSckey.Items.AddRange(new object[] { 
                "", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12", "Shift+F1", "Shift+F2", "Shift+F3", 
                "Shift+F4", "Shift+F5", "Shift+F6", "Shift+F7", "Shift+F8", "Shift+F9", "Shift+F10", "Shift+F11", "Shift+F12", "Ctrl+F1", "Ctrl+F2", "Ctrl+F3", "Ctrl+F4", "Ctrl+F5", "Ctrl+F6", "Ctrl+F7", 
                "Ctrl+F8", "Ctrl+F9", "Ctrl+F10", "Ctrl+F11", "Ctrl+F12", "Ctrl+A", "Ctrl+B", "Ctrl+C", "Ctrl+D", "Ctrl+E", "Ctrl+F", "Ctrl+G", "Ctrl+H", "Ctrl+I", "Ctrl+J", "Ctrl+K", 
                "Ctrl+L", "Ctrl+M", "Ctrl+N", "Ctrl+O", "Ctrl+P", "Ctrl+Q", "Ctrl+R", "Ctrl+S", "Ctrl+T", "Ctrl+U", "Ctrl+V", "Ctrl+W", "Ctrl+X", "Ctrl+Y", "Ctrl+Z"
             });
            this.cKJSckey.Name = "cKJSckey";
            this.cKJSckey.Width = 110;
            this.cKJNameKj.DataPropertyName = "clmKJNameKj";
            this.cKJNameKj.HeaderText = "clmKJNameKj";
            this.cKJNameKj.Name = "cKJNameKj";
            this.cKJNameKj.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.cKJNameKj.Visible = false;
            this.cKJName.DataPropertyName = "clmKJName";
            this.cKJName.HeaderText = "clmKJName";
            this.cKJName.Name = "cKJName";
            this.cKJName.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.cKJName.Visible = false;
            this.bdsUserKeyJ.DataMember = "dtUserKeyJ";
            this.bdsUserKeyJ.DataSource = this.dtsUserKey;
            this.dtsUserKey.DataSetName = "NewDataSet";
            this.dtsUserKey.Tables.AddRange(new DataTable[] { this.dttUserKeyJ, this.dttUserKeyS, this.dttUserKeyR, this.dttUserKeyG });
            this.dttUserKeyJ.Columns.AddRange(new DataColumn[] { this.dtclmKJNameKj, this.dtclmKJSckey, this.dtclmKJName, this.dtclmKJGymCode, this.dtclmKJWindowCode });
            this.dttUserKeyJ.TableName = "dtUserKeyJ";
            this.dtclmKJNameKj.Caption = "";
            this.dtclmKJNameKj.ColumnName = "clmKJNameKj";
            this.dtclmKJNameKj.DefaultValue = "";
            this.dtclmKJSckey.Caption = "";
            this.dtclmKJSckey.ColumnName = "clmKJSckey";
            this.dtclmKJSckey.DefaultValue = "";
            this.dtclmKJName.Caption = "";
            this.dtclmKJName.ColumnName = "clmKJName";
            this.dtclmKJName.DefaultValue = "";
            this.dtclmKJGymCode.Caption = "";
            this.dtclmKJGymCode.ColumnName = "clmKJGymCode";
            this.dtclmKJGymCode.DefaultValue = "";
            this.dtclmKJWindowCode.Caption = "";
            this.dtclmKJWindowCode.ColumnName = "clmKJWindowCode";
            this.dtclmKJWindowCode.DefaultValue = "";
            this.dttUserKeyS.Columns.AddRange(new DataColumn[] { this.dtclmKSNameKj, this.dtclmKSSckey, this.dtclmKSName });
            this.dttUserKeyS.TableName = "dtUserKeyS";
            this.dtclmKSNameKj.Caption = "";
            this.dtclmKSNameKj.ColumnName = "clmKSNameKj";
            this.dtclmKSNameKj.DefaultValue = "";
            this.dtclmKSSckey.Caption = "";
            this.dtclmKSSckey.ColumnName = "clmKSSckey";
            this.dtclmKSSckey.DefaultValue = "";
            this.dtclmKSName.Caption = "";
            this.dtclmKSName.ColumnName = "clmKSName";
            this.dtclmKSName.DefaultValue = "";
            this.dttUserKeyR.Columns.AddRange(new DataColumn[] { this.dtclmKRNameKj, this.dtclmKRSckey, this.dtclmKRName });
            this.dttUserKeyR.TableName = "dtUserKeyR";
            this.dtclmKRNameKj.Caption = "";
            this.dtclmKRNameKj.ColumnName = "clmKRNameKj";
            this.dtclmKRNameKj.DefaultValue = "";
            this.dtclmKRSckey.Caption = "";
            this.dtclmKRSckey.ColumnName = "clmKRSckey";
            this.dtclmKRSckey.DefaultValue = "";
            this.dtclmKRName.Caption = "";
            this.dtclmKRName.ColumnName = "clmKRName";
            this.dtclmKRName.DefaultValue = "";
            this.dttUserKeyG.Columns.AddRange(new DataColumn[] { this.dtclmKGNameKj, this.dtclmKGSckey, this.dtclmKGName });
            this.dttUserKeyG.TableName = "dtUserKeyG";
            this.dtclmKGNameKj.Caption = "";
            this.dtclmKGNameKj.ColumnName = "clmKGNameKj";
            this.dtclmKGNameKj.DefaultValue = "";
            this.dtclmKGSckey.Caption = "";
            this.dtclmKGSckey.ColumnName = "clmKGSckey";
            this.dtclmKGSckey.DefaultValue = "";
            this.dtclmKGName.Caption = "";
            this.dtclmKGName.ColumnName = "clmKGName";
            this.dtclmKGName.DefaultValue = "";
            this.bdnUserKeyJ.AddNewItem = null;
            this.bdnUserKeyJ.BindingSource = this.bdsUserKeyJ;
            this.bdnUserKeyJ.CountItem = this.bdnKJCountItem;
            this.bdnUserKeyJ.DeleteItem = null;
            this.bdnUserKeyJ.Items.AddRange(new ToolStripItem[] { this.bdnKJMoveFirstItem, this.bdnKJMovePreviousItem, this.bdnKJSeparator1, this.bdnKJPositionItem, this.bdnKJCountItem, this.bdnKJSeparator2, this.bdnKJMoveNextItem, this.bdnKJMoveLastItem });
            this.bdnUserKeyJ.Location = new Point(0, 0);
            this.bdnUserKeyJ.MoveFirstItem = this.bdnKJMoveFirstItem;
            this.bdnUserKeyJ.MoveLastItem = this.bdnKJMoveLastItem;
            this.bdnUserKeyJ.MoveNextItem = this.bdnKJMoveNextItem;
            this.bdnUserKeyJ.MovePreviousItem = this.bdnKJMovePreviousItem;
            this.bdnUserKeyJ.Name = "bdnUserKeyJ";
            this.bdnUserKeyJ.PositionItem = this.bdnKJPositionItem;
            this.bdnUserKeyJ.Size = new Size(500, 0x19);
            this.bdnUserKeyJ.TabIndex = 1;
            this.bdnKJCountItem.Name = "bdnKJCountItem";
            this.bdnKJCountItem.Size = new Size(0x26, 0x16);
            this.bdnKJCountItem.Text = "/ {0}";
            this.bdnKJCountItem.ToolTipText = "Total items";
            this.bdnKJMoveFirstItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnKJMoveFirstItem.Image = (Image) manager.GetObject("bdnKJMoveFirstItem.Image");
            this.bdnKJMoveFirstItem.Name = "bdnKJMoveFirstItem";
            this.bdnKJMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bdnKJMoveFirstItem.Size = new Size(0x17, 0x16);
            this.bdnKJMoveFirstItem.Text = "Hạng mục đầu ti\x00ean";
            this.bdnKJMoveFirstItem.ToolTipText = "First item";
            this.bdnKJMovePreviousItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnKJMovePreviousItem.Image = (Image) manager.GetObject("bdnKJMovePreviousItem.Image");
            this.bdnKJMovePreviousItem.Name = "bdnKJMovePreviousItem";
            this.bdnKJMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bdnKJMovePreviousItem.Size = new Size(0x17, 0x16);
            this.bdnKJMovePreviousItem.Text = "Hạng mục trước";
            this.bdnKJMovePreviousItem.ToolTipText = "Previous item";
            this.bdnKJSeparator1.Name = "bdnKJSeparator1";
            this.bdnKJSeparator1.Size = new Size(6, 0x19);
            this.bdnKJPositionItem.AccessibleName = "位置";
            this.bdnKJPositionItem.AutoSize = false;
            this.bdnKJPositionItem.Name = "bdnKJPositionItem";
            this.bdnKJPositionItem.Size = new Size(50, 0x13);
            this.bdnKJPositionItem.Text = "0";
            this.bdnKJPositionItem.ToolTipText = "Present location";
            this.bdnKJSeparator2.Name = "bdnKJSeparator2";
            this.bdnKJSeparator2.Size = new Size(6, 0x19);
            this.bdnKJMoveNextItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnKJMoveNextItem.Image = (Image) manager.GetObject("bdnKJMoveNextItem.Image");
            this.bdnKJMoveNextItem.Name = "bdnKJMoveNextItem";
            this.bdnKJMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bdnKJMoveNextItem.Size = new Size(0x17, 0x16);
            this.bdnKJMoveNextItem.Text = "Hạng mục tiếp theo";
            this.bdnKJMoveNextItem.ToolTipText = "Next item";
            this.bdnKJMoveLastItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnKJMoveLastItem.Image = (Image) manager.GetObject("bdnKJMoveLastItem.Image");
            this.bdnKJMoveLastItem.Name = "bdnKJMoveLastItem";
            this.bdnKJMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bdnKJMoveLastItem.Size = new Size(0x17, 0x16);
            this.bdnKJMoveLastItem.Text = "hạng mục cuối c\x00f9ng";
            this.tbpUserKeyS.Controls.Add(this.dgvUserKeyS);
            this.tbpUserKeyS.Controls.Add(this.bdnUserKeyS);
            this.tbpUserKeyS.Location = new Point(4, 0x16);
            this.tbpUserKeyS.Name = "tbpUserKeyS";
            this.tbpUserKeyS.Size = new Size(500, 0x19f);
            this.tbpUserKeyS.TabIndex = 1;
            this.tbpUserKeyS.Text = "Chức năng chung";
            this.tbpUserKeyS.UseVisualStyleBackColor = true;
            this.dgvUserKeyS.AllowUserToAddRows = false;
            this.dgvUserKeyS.AllowUserToDeleteRows = false;
            style4.BackColor = SystemColors.GradientInactiveCaption;
            this.dgvUserKeyS.AlternatingRowsDefaultCellStyle = style4;
            this.dgvUserKeyS.AutoGenerateColumns = false;
            this.dgvUserKeyS.BackgroundColor = SystemColors.Control;
            style5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style5.BackColor = SystemColors.Control;
            style5.ForeColor = SystemColors.WindowText;
            style5.SelectionBackColor = SystemColors.Highlight;
            style5.SelectionForeColor = SystemColors.HighlightText;
            style5.WrapMode = DataGridViewTriState.True;
            this.dgvUserKeyS.ColumnHeadersDefaultCellStyle = style5;
            this.dgvUserKeyS.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserKeyS.Columns.AddRange(new DataGridViewColumn[] { this.cKSNameKj, this.cKSSckey });
            this.dgvUserKeyS.DataSource = this.bdsUserKeyS;
            this.dgvUserKeyS.Dock = DockStyle.Fill;
            this.dgvUserKeyS.Location = new Point(0, 0x19);
            this.dgvUserKeyS.Name = "dgvUserKeyS";
            style6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style6.BackColor = SystemColors.Control;
            style6.ForeColor = SystemColors.WindowText;
            style6.SelectionBackColor = SystemColors.Highlight;
            style6.SelectionForeColor = SystemColors.HighlightText;
            style6.WrapMode = DataGridViewTriState.True;
            this.dgvUserKeyS.RowHeadersDefaultCellStyle = style6;
            this.dgvUserKeyS.RowTemplate.Height = 0x15;
            this.dgvUserKeyS.Size = new Size(500, 390);
            this.dgvUserKeyS.TabIndex = 2;
            this.dgvUserKeyS.Tag = "Common";
            this.dgvUserKeyS.CellEndEdit += new DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            this.dgvUserKeyS.CellValidating += new DataGridViewCellValidatingEventHandler(this.dgv_CellValidating);
            this.dgvUserKeyS.RowValidating += new DataGridViewCellCancelEventHandler(this.dgv_RowValidating);
            this.dgvUserKeyS.SelectionChanged += new EventHandler(this.dgv_SelectionChanged);
            this.cKSNameKj.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.cKSNameKj.DataPropertyName = "clmKSNameKj";
            this.cKSNameKj.HeaderText = "T\x00ean chức năng";
            this.cKSNameKj.Name = "cKSNameKj";
            this.cKSNameKj.ReadOnly = true;
            this.cKSNameKj.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.cKSSckey.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.cKSSckey.DataPropertyName = "clmKSSckey";
            this.cKSSckey.HeaderText = "Ph\x00edm tắt";
            this.cKSSckey.Items.AddRange(new object[] { 
                "-", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12", "Shift+F1", "Shift+F2", "Shift+F3", 
                "Shift+F4", "Shift+F5", "Shift+F6", "Shift+F7", "Shift+F8", "Shift+F9", "Shift+F10", "Shift+F11", "Shift+F12", "Ctrl+F1", "Ctrl+F2", "Ctrl+F3", "Ctrl+F4", "Ctrl+F5", "Ctrl+F6", "Ctrl+F7", 
                "Ctrl+F8", "Ctrl+F9", "Ctrl+F10", "Ctrl+F11", "Ctrl+F12", "Ctrl+A", "Ctrl+B", "Ctrl+C", "Ctrl+D", "Ctrl+E", "Ctrl+F", "Ctrl+G", "Ctrl+H", "Ctrl+I", "Ctrl+J", "Ctrl+K", 
                "Ctrl+L", "Ctrl+M", "Ctrl+N", "Ctrl+O", "Ctrl+P", "Ctrl+Q", "Ctrl+R", "Ctrl+S", "Ctrl+T", "Ctrl+U", "Ctrl+V", "Ctrl+W", "Ctrl+X", "Ctrl+Y", "Ctrl+Z"
             });
            this.cKSSckey.Name = "cKSSckey";
            this.cKSSckey.Resizable = DataGridViewTriState.True;
            this.cKSSckey.Width = 110;
            this.bdsUserKeyS.DataMember = "dtUserKeyS";
            this.bdsUserKeyS.DataSource = this.dtsUserKey;
            this.bdnUserKeyS.AddNewItem = null;
            this.bdnUserKeyS.BindingSource = this.bdsUserKeyS;
            this.bdnUserKeyS.CountItem = this.bdnKSCountItem;
            this.bdnUserKeyS.DeleteItem = null;
            this.bdnUserKeyS.Items.AddRange(new ToolStripItem[] { this.bdnKSMoveFirstItem, this.bdnKSMovePreviousItem, this.bdnKSSeparator1, this.bdnKSPositionItem, this.bdnKSCountItem, this.bdnKSSeparator2, this.bdnKSMoveNextItem, this.bdnKSMoveLastItem });
            this.bdnUserKeyS.Location = new Point(0, 0);
            this.bdnUserKeyS.MoveFirstItem = this.bdnKSMoveFirstItem;
            this.bdnUserKeyS.MoveLastItem = this.bdnKSMoveLastItem;
            this.bdnUserKeyS.MoveNextItem = this.bdnKSMoveNextItem;
            this.bdnUserKeyS.MovePreviousItem = this.bdnKSMovePreviousItem;
            this.bdnUserKeyS.Name = "bdnUserKeyS";
            this.bdnUserKeyS.PositionItem = this.bdnKSPositionItem;
            this.bdnUserKeyS.Size = new Size(500, 0x19);
            this.bdnUserKeyS.TabIndex = 1;
            this.bdnKSCountItem.Name = "bdnKSCountItem";
            this.bdnKSCountItem.Size = new Size(0x26, 0x16);
            this.bdnKSCountItem.Text = "/ {0}";
            this.bdnKSCountItem.ToolTipText = "Total items";
            this.bdnKSMoveFirstItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnKSMoveFirstItem.Image = (Image) manager.GetObject("bdnKSMoveFirstItem.Image");
            this.bdnKSMoveFirstItem.Name = "bdnKSMoveFirstItem";
            this.bdnKSMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bdnKSMoveFirstItem.Size = new Size(0x17, 0x16);
            this.bdnKSMoveFirstItem.Text = "Hạng mục đầu ti\x00ean";
            this.bdnKSMoveFirstItem.ToolTipText = "First item";
            this.bdnKSMovePreviousItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnKSMovePreviousItem.Image = (Image) manager.GetObject("bdnKSMovePreviousItem.Image");
            this.bdnKSMovePreviousItem.Name = "bdnKSMovePreviousItem";
            this.bdnKSMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bdnKSMovePreviousItem.Size = new Size(0x17, 0x16);
            this.bdnKSMovePreviousItem.Text = "Hạng mục trước";
            this.bdnKSMovePreviousItem.ToolTipText = "Previous item";
            this.bdnKSSeparator1.Name = "bdnKSSeparator1";
            this.bdnKSSeparator1.Size = new Size(6, 0x19);
            this.bdnKSPositionItem.AccessibleName = "位置";
            this.bdnKSPositionItem.AutoSize = false;
            this.bdnKSPositionItem.Name = "bdnKSPositionItem";
            this.bdnKSPositionItem.Size = new Size(50, 0x13);
            this.bdnKSPositionItem.Text = "0";
            this.bdnKSPositionItem.ToolTipText = "Present location";
            this.bdnKSSeparator2.Name = "bdnKSSeparator2";
            this.bdnKSSeparator2.Size = new Size(6, 0x19);
            this.bdnKSMoveNextItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnKSMoveNextItem.Image = (Image) manager.GetObject("bdnKSMoveNextItem.Image");
            this.bdnKSMoveNextItem.Name = "bdnKSMoveNextItem";
            this.bdnKSMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bdnKSMoveNextItem.Size = new Size(0x17, 0x16);
            this.bdnKSMoveNextItem.Text = "Hạng mục tiếp theo";
            this.bdnKSMoveNextItem.ToolTipText = "Next item";
            this.bdnKSMoveLastItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnKSMoveLastItem.Image = (Image) manager.GetObject("bdnKSMoveLastItem.Image");
            this.bdnKSMoveLastItem.Name = "bdnKSMoveLastItem";
            this.bdnKSMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bdnKSMoveLastItem.Size = new Size(0x17, 0x16);
            this.bdnKSMoveLastItem.Text = "hạng mục cuối c\x00f9ng";
            this.bdnKSMoveLastItem.ToolTipText = "Last item";
            this.tbpUserKeyR.Controls.Add(this.dgvUserKeyR);
            this.tbpUserKeyR.Controls.Add(this.bdnUserKeyR);
            this.tbpUserKeyR.Location = new Point(4, 0x16);
            this.tbpUserKeyR.Name = "tbpUserKeyR";
            this.tbpUserKeyR.Size = new Size(500, 0x19f);
            this.tbpUserKeyR.TabIndex = 3;
            this.tbpUserKeyR.Text = "M\x00e0n h\x00ecnh ch\x00ednh";
            this.tbpUserKeyR.UseVisualStyleBackColor = true;
            this.dgvUserKeyR.AllowUserToAddRows = false;
            this.dgvUserKeyR.AllowUserToDeleteRows = false;
            this.dgvUserKeyR.AllowUserToResizeRows = false;
            style7.BackColor = SystemColors.GradientInactiveCaption;
            this.dgvUserKeyR.AlternatingRowsDefaultCellStyle = style7;
            this.dgvUserKeyR.AutoGenerateColumns = false;
            this.dgvUserKeyR.BackgroundColor = SystemColors.Control;
            style8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style8.BackColor = SystemColors.Control;
            style8.ForeColor = SystemColors.WindowText;
            style8.SelectionBackColor = SystemColors.Highlight;
            style8.SelectionForeColor = SystemColors.HighlightText;
            style8.WrapMode = DataGridViewTriState.True;
            this.dgvUserKeyR.ColumnHeadersDefaultCellStyle = style8;
            this.dgvUserKeyR.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserKeyR.Columns.AddRange(new DataGridViewColumn[] { this.cKRNameKj, this.cKRSckey });
            this.dgvUserKeyR.DataSource = this.bdsUserKeyR;
            this.dgvUserKeyR.Dock = DockStyle.Fill;
            this.dgvUserKeyR.Location = new Point(0, 0x19);
            this.dgvUserKeyR.Name = "dgvUserKeyR";
            style9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style9.BackColor = SystemColors.Control;
            style9.ForeColor = SystemColors.WindowText;
            style9.SelectionBackColor = SystemColors.Highlight;
            style9.SelectionForeColor = SystemColors.HighlightText;
            style9.WrapMode = DataGridViewTriState.True;
            this.dgvUserKeyR.RowHeadersDefaultCellStyle = style9;
            this.dgvUserKeyR.RowTemplate.Height = 0x15;
            this.dgvUserKeyR.Size = new Size(500, 390);
            this.dgvUserKeyR.TabIndex = 2;
            this.dgvUserKeyR.Tag = "Message list";
            this.dgvUserKeyR.CellEndEdit += new DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            this.dgvUserKeyR.CellValidating += new DataGridViewCellValidatingEventHandler(this.dgv_CellValidating);
            this.dgvUserKeyR.RowValidating += new DataGridViewCellCancelEventHandler(this.dgv_RowValidating);
            this.dgvUserKeyR.SelectionChanged += new EventHandler(this.dgv_SelectionChanged);
            this.cKRNameKj.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.cKRNameKj.DataPropertyName = "clmKRNameKj";
            this.cKRNameKj.HeaderText = "T\x00ean chức năng";
            this.cKRNameKj.Name = "cKRNameKj";
            this.cKRNameKj.ReadOnly = true;
            this.cKRNameKj.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.cKRSckey.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.cKRSckey.DataPropertyName = "clmKRSckey";
            this.cKRSckey.HeaderText = "Ph\x00edm tắt";
            this.cKRSckey.Items.AddRange(new object[] { 
                "-", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12", "Shift+F1", "Shift+F2", "Shift+F3", 
                "Shift+F4", "Shift+F5", "Shift+F6", "Shift+F7", "Shift+F8", "Shift+F9", "Shift+F10", "Shift+F11", "Shift+F12", "Ctrl+F1", "Ctrl+F2", "Ctrl+F3", "Ctrl+F4", "Ctrl+F5", "Ctrl+F6", "Ctrl+F7", 
                "Ctrl+F8", "Ctrl+F9", "Ctrl+F10", "Ctrl+F11", "Ctrl+F12", "Ctrl+A", "Ctrl+B", "Ctrl+C", "Ctrl+D", "Ctrl+E", "Ctrl+F", "Ctrl+G", "Ctrl+H", "Ctrl+I", "Ctrl+J", "Ctrl+K", 
                "Ctrl+L", "Ctrl+M", "Ctrl+N", "Ctrl+O", "Ctrl+P", "Ctrl+Q", "Ctrl+R", "Ctrl+S", "Ctrl+T", "Ctrl+U", "Ctrl+V", "Ctrl+W", "Ctrl+X", "Ctrl+Y", "Ctrl+Z"
             });
            this.cKRSckey.Name = "cKRSckey";
            this.cKRSckey.Resizable = DataGridViewTriState.True;
            this.cKRSckey.Width = 110;
            this.bdsUserKeyR.DataMember = "dtUserKeyR";
            this.bdsUserKeyR.DataSource = this.dtsUserKey;
            this.bdnUserKeyR.AddNewItem = null;
            this.bdnUserKeyR.BindingSource = this.bdsUserKeyR;
            this.bdnUserKeyR.CountItem = this.bdnKRCountItem;
            this.bdnUserKeyR.DeleteItem = null;
            this.bdnUserKeyR.Items.AddRange(new ToolStripItem[] { this.bdnKRMoveFirstItem, this.bdnKRMovePreviousItem, this.bdnKRSeparator1, this.bdnKRPositionItem, this.bdnKRCountItem, this.bdnKRSeparator2, this.bdnKRMoveNextItem, this.bdnKRMoveLastItem });
            this.bdnUserKeyR.Location = new Point(0, 0);
            this.bdnUserKeyR.MoveFirstItem = this.bdnKRMoveFirstItem;
            this.bdnUserKeyR.MoveLastItem = this.bdnKRMoveLastItem;
            this.bdnUserKeyR.MoveNextItem = this.bdnKRMoveNextItem;
            this.bdnUserKeyR.MovePreviousItem = this.bdnKRMovePreviousItem;
            this.bdnUserKeyR.Name = "bdnUserKeyR";
            this.bdnUserKeyR.PositionItem = this.bdnKRPositionItem;
            this.bdnUserKeyR.Size = new Size(500, 0x19);
            this.bdnUserKeyR.TabIndex = 1;
            this.bdnKRCountItem.Name = "bdnKRCountItem";
            this.bdnKRCountItem.Size = new Size(0x26, 0x16);
            this.bdnKRCountItem.Text = "/ {0}";
            this.bdnKRCountItem.ToolTipText = "Total items";
            this.bdnKRMoveFirstItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnKRMoveFirstItem.Image = (Image) manager.GetObject("bdnKRMoveFirstItem.Image");
            this.bdnKRMoveFirstItem.Name = "bdnKRMoveFirstItem";
            this.bdnKRMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bdnKRMoveFirstItem.Size = new Size(0x17, 0x16);
            this.bdnKRMoveFirstItem.Text = "Hạng mục đầu ti\x00ean";
            this.bdnKRMoveFirstItem.ToolTipText = "First item";
            this.bdnKRMovePreviousItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnKRMovePreviousItem.Image = (Image) manager.GetObject("bdnKRMovePreviousItem.Image");
            this.bdnKRMovePreviousItem.Name = "bdnKRMovePreviousItem";
            this.bdnKRMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bdnKRMovePreviousItem.Size = new Size(0x17, 0x16);
            this.bdnKRMovePreviousItem.Text = "Hạng mục trước";
            this.bdnKRMovePreviousItem.ToolTipText = "Previous item";
            this.bdnKRSeparator1.Name = "bdnKRSeparator1";
            this.bdnKRSeparator1.Size = new Size(6, 0x19);
            this.bdnKRPositionItem.AccessibleName = "位置";
            this.bdnKRPositionItem.AutoSize = false;
            this.bdnKRPositionItem.Name = "bdnKRPositionItem";
            this.bdnKRPositionItem.Size = new Size(50, 0x13);
            this.bdnKRPositionItem.Text = "0";
            this.bdnKRPositionItem.ToolTipText = "Present location";
            this.bdnKRSeparator2.Name = "bdnKRSeparator2";
            this.bdnKRSeparator2.Size = new Size(6, 0x19);
            this.bdnKRMoveNextItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnKRMoveNextItem.Image = (Image) manager.GetObject("bdnKRMoveNextItem.Image");
            this.bdnKRMoveNextItem.Name = "bdnKRMoveNextItem";
            this.bdnKRMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bdnKRMoveNextItem.Size = new Size(0x17, 0x16);
            this.bdnKRMoveNextItem.Text = "Hạng mục tiếp theo";
            this.bdnKRMoveNextItem.ToolTipText = "Next item";
            this.bdnKRMoveLastItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnKRMoveLastItem.Image = (Image) manager.GetObject("bdnKRMoveLastItem.Image");
            this.bdnKRMoveLastItem.Name = "bdnKRMoveLastItem";
            this.bdnKRMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bdnKRMoveLastItem.Size = new Size(0x17, 0x16);
            this.bdnKRMoveLastItem.Text = "hạng mục cuối c\x00f9ng";
            this.bdnKRMoveLastItem.ToolTipText = "Last item";
            this.tbpUserKeyG.Controls.Add(this.dgvUserKeyG);
            this.tbpUserKeyG.Controls.Add(this.bdnUserKeyG);
            this.tbpUserKeyG.Location = new Point(4, 0x16);
            this.tbpUserKeyG.Name = "tbpUserKeyG";
            this.tbpUserKeyG.Size = new Size(500, 0x19f);
            this.tbpUserKeyG.TabIndex = 2;
            this.tbpUserKeyG.Text = "M\x00e0n h\x00ecnh nghiệp vụ";
            this.tbpUserKeyG.UseVisualStyleBackColor = true;
            this.dgvUserKeyG.AllowUserToAddRows = false;
            this.dgvUserKeyG.AllowUserToDeleteRows = false;
            style10.BackColor = SystemColors.GradientInactiveCaption;
            this.dgvUserKeyG.AlternatingRowsDefaultCellStyle = style10;
            this.dgvUserKeyG.AutoGenerateColumns = false;
            this.dgvUserKeyG.BackgroundColor = SystemColors.Control;
            style11.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style11.BackColor = SystemColors.Control;
            style11.ForeColor = SystemColors.WindowText;
            style11.SelectionBackColor = SystemColors.Highlight;
            style11.SelectionForeColor = SystemColors.HighlightText;
            style11.WrapMode = DataGridViewTriState.True;
            this.dgvUserKeyG.ColumnHeadersDefaultCellStyle = style11;
            this.dgvUserKeyG.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserKeyG.Columns.AddRange(new DataGridViewColumn[] { this.cKGNameKj, this.cKGSckey });
            this.dgvUserKeyG.DataSource = this.bdsUserKeyG;
            this.dgvUserKeyG.Dock = DockStyle.Fill;
            this.dgvUserKeyG.Location = new Point(0, 0x19);
            this.dgvUserKeyG.Name = "dgvUserKeyG";
            style12.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style12.BackColor = SystemColors.Control;
            style12.ForeColor = SystemColors.WindowText;
            style12.SelectionBackColor = SystemColors.Highlight;
            style12.SelectionForeColor = SystemColors.HighlightText;
            style12.WrapMode = DataGridViewTriState.True;
            this.dgvUserKeyG.RowHeadersDefaultCellStyle = style12;
            this.dgvUserKeyG.RowTemplate.Height = 0x15;
            this.dgvUserKeyG.Size = new Size(500, 390);
            this.dgvUserKeyG.TabIndex = 2;
            this.dgvUserKeyG.Tag = "Business service screen";
            this.dgvUserKeyG.CellEndEdit += new DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            this.dgvUserKeyG.CellValidating += new DataGridViewCellValidatingEventHandler(this.dgv_CellValidating);
            this.dgvUserKeyG.RowValidating += new DataGridViewCellCancelEventHandler(this.dgv_RowValidating);
            this.dgvUserKeyG.SelectionChanged += new EventHandler(this.dgv_SelectionChanged);
            this.cKGNameKj.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.cKGNameKj.DataPropertyName = "clmKGNameKj";
            this.cKGNameKj.HeaderText = "T\x00ean chức năng";
            this.cKGNameKj.Name = "cKGNameKj";
            this.cKGNameKj.ReadOnly = true;
            this.cKGNameKj.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.cKGSckey.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.cKGSckey.DataPropertyName = "clmKGSckey";
            this.cKGSckey.HeaderText = "Ph\x00edm tắt";
            this.cKGSckey.Items.AddRange(new object[] { 
                "-", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12", "Shift+F1", "Shift+F2", "Shift+F3", 
                "Shift+F4", "Shift+F5", "Shift+F6", "Shift+F7", "Shift+F8", "Shift+F9", "Shift+F10", "Shift+F11", "Shift+F12", "Ctrl+F1", "Ctrl+F2", "Ctrl+F3", "Ctrl+F4", "Ctrl+F5", "Ctrl+F6", "Ctrl+F7", 
                "Ctrl+F8", "Ctrl+F9", "Ctrl+F10", "Ctrl+F11", "Ctrl+F12", "Ctrl+A", "Ctrl+B", "Ctrl+C", "Ctrl+D", "Ctrl+E", "Ctrl+F", "Ctrl+G", "Ctrl+H", "Ctrl+I", "Ctrl+J", "Ctrl+K", 
                "Ctrl+L", "Ctrl+M", "Ctrl+N", "Ctrl+O", "Ctrl+P", "Ctrl+Q", "Ctrl+R", "Ctrl+S", "Ctrl+T", "Ctrl+U", "Ctrl+V", "Ctrl+W", "Ctrl+X", "Ctrl+Y", "Ctrl+Z"
             });
            this.cKGSckey.Name = "cKGSckey";
            this.cKGSckey.Resizable = DataGridViewTriState.True;
            this.cKGSckey.Width = 110;
            this.bdsUserKeyG.DataMember = "dtUserKeyG";
            this.bdsUserKeyG.DataSource = this.dtsUserKey;
            this.bdnUserKeyG.AddNewItem = null;
            this.bdnUserKeyG.BindingSource = this.bdsUserKeyG;
            this.bdnUserKeyG.CountItem = this.bdnKGCountItem;
            this.bdnUserKeyG.DeleteItem = null;
            this.bdnUserKeyG.Items.AddRange(new ToolStripItem[] { this.bdnKGMoveFirstItem, this.bdnKGMovePreviousItem, this.bdnKGSeparator1, this.bdnKGPositionItem, this.bdnKGCountItem, this.bdnKGSeparator2, this.bdnKGMoveNextItem, this.bdnKGMoveLastItem });
            this.bdnUserKeyG.Location = new Point(0, 0);
            this.bdnUserKeyG.MoveFirstItem = this.bdnKGMoveFirstItem;
            this.bdnUserKeyG.MoveLastItem = this.bdnKGMoveLastItem;
            this.bdnUserKeyG.MoveNextItem = this.bdnKGMoveNextItem;
            this.bdnUserKeyG.MovePreviousItem = this.bdnKGMovePreviousItem;
            this.bdnUserKeyG.Name = "bdnUserKeyG";
            this.bdnUserKeyG.PositionItem = this.bdnKGPositionItem;
            this.bdnUserKeyG.Size = new Size(500, 0x19);
            this.bdnUserKeyG.TabIndex = 1;
            this.bdnKGCountItem.Name = "bdnKGCountItem";
            this.bdnKGCountItem.Size = new Size(0x26, 0x16);
            this.bdnKGCountItem.Text = "/ {0}";
            this.bdnKGCountItem.ToolTipText = "Total items";
            this.bdnKGMoveFirstItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnKGMoveFirstItem.Image = (Image) manager.GetObject("bdnKGMoveFirstItem.Image");
            this.bdnKGMoveFirstItem.Name = "bdnKGMoveFirstItem";
            this.bdnKGMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bdnKGMoveFirstItem.Size = new Size(0x17, 0x16);
            this.bdnKGMoveFirstItem.Text = "Hạng mục đầu ti\x00ean";
            this.bdnKGMoveFirstItem.ToolTipText = "First item";
            this.bdnKGMovePreviousItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnKGMovePreviousItem.Image = (Image) manager.GetObject("bdnKGMovePreviousItem.Image");
            this.bdnKGMovePreviousItem.Name = "bdnKGMovePreviousItem";
            this.bdnKGMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bdnKGMovePreviousItem.Size = new Size(0x17, 0x16);
            this.bdnKGMovePreviousItem.Text = "Hạng mục trước";
            this.bdnKGMovePreviousItem.ToolTipText = "Previous item";
            this.bdnKGSeparator1.Name = "bdnKGSeparator1";
            this.bdnKGSeparator1.Size = new Size(6, 0x19);
            this.bdnKGPositionItem.AccessibleName = "位置";
            this.bdnKGPositionItem.AutoSize = false;
            this.bdnKGPositionItem.Name = "bdnKGPositionItem";
            this.bdnKGPositionItem.Size = new Size(50, 0x13);
            this.bdnKGPositionItem.Text = "0";
            this.bdnKGPositionItem.ToolTipText = "Present location";
            this.bdnKGSeparator2.Name = "bdnKGSeparator2";
            this.bdnKGSeparator2.Size = new Size(6, 0x19);
            this.bdnKGMoveNextItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnKGMoveNextItem.Image = (Image) manager.GetObject("bdnKGMoveNextItem.Image");
            this.bdnKGMoveNextItem.Name = "bdnKGMoveNextItem";
            this.bdnKGMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bdnKGMoveNextItem.Size = new Size(0x17, 0x16);
            this.bdnKGMoveNextItem.Text = "Hạng mục tiếp theo";
            this.bdnKGMoveLastItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnKGMoveLastItem.Image = (Image) manager.GetObject("bdnKGMoveLastItem.Image");
            this.bdnKGMoveLastItem.Name = "bdnKGMoveLastItem";
            this.bdnKGMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bdnKGMoveLastItem.Size = new Size(0x17, 0x16);
            this.bdnKGMoveLastItem.Text = "hạng mục cuối c\x00f9ng";
            this.bdnKGMoveLastItem.ToolTipText = "Last item";
            this.tbpReceiveInform.Controls.Add(this.ckbRcvInformSnd);
            this.tbpReceiveInform.Controls.Add(this.gpbReceiveInform);
            this.tbpReceiveInform.Location = new Point(4, 40);
            this.tbpReceiveInform.Name = "tbpReceiveInform";
            this.tbpReceiveInform.Size = new Size(0x389, 0x1d4);
            this.tbpReceiveInform.TabIndex = 7;
            this.tbpReceiveInform.Text = "Th\x00f4ng b\x00e1o";
            this.tbpReceiveInform.UseVisualStyleBackColor = true;
            this.ckbRcvInformSnd.AutoSize = true;
            this.ckbRcvInformSnd.Checked = true;
            this.ckbRcvInformSnd.CheckState = CheckState.Checked;
            this.ckbRcvInformSnd.Location = new Point(0x221, 0x25);
            this.ckbRcvInformSnd.Name = "ckbRcvInformSnd";
            this.ckbRcvInformSnd.Size = new Size(0x148, 0x10);
            this.ckbRcvInformSnd.TabIndex = 1;
            this.ckbRcvInformSnd.Text = "Sử dụng \x00e2m b\x00e1o nếu kết quả xử l\x00fd c\x00f3 th\x00f4ng tin cảnh b\x00e1o (W)";
            this.ckbRcvInformSnd.UseVisualStyleBackColor = true;
            this.gpbReceiveInform.Controls.Add(this.gpbReceiveNotice);
            this.gpbReceiveInform.Controls.Add(this.btnSoundFile);
            this.gpbReceiveInform.Controls.Add(this.lblSoundFile);
            this.gpbReceiveInform.Controls.Add(this.txbSoundFile);
            this.gpbReceiveInform.Controls.Add(this.ckbRcvInform);
            this.gpbReceiveInform.Location = new Point(0x11, 13);
            this.gpbReceiveInform.Name = "gpbReceiveInform";
            this.gpbReceiveInform.Size = new Size(0x201, 0x1be);
            this.gpbReceiveInform.TabIndex = 0;
            this.gpbReceiveInform.TabStop = false;
            this.gpbReceiveInform.Text = "Th\x00f4ng b\x00e1o khi nhận th\x00f4ng điệp từ VNACCS";
            this.gpbReceiveNotice.Controls.Add(this.dgvReceiveInform);
            this.gpbReceiveNotice.Controls.Add(this.bdnRcvNotice);
            this.gpbReceiveNotice.Location = new Point(0x13, 0x3b);
            this.gpbReceiveNotice.Name = "gpbReceiveNotice";
            this.gpbReceiveNotice.Size = new Size(0x12a, 0x17d);
            this.gpbReceiveNotice.TabIndex = 4;
            this.gpbReceiveNotice.TabStop = false;
            this.gpbReceiveNotice.Text = "Th\x00f4ng điệp th\x00f4ng b\x00e1o";
            this.dgvReceiveInform.AllowUserToAddRows = false;
            style13.BackColor = SystemColors.GradientInactiveCaption;
            this.dgvReceiveInform.AlternatingRowsDefaultCellStyle = style13;
            this.dgvReceiveInform.AutoGenerateColumns = false;
            this.dgvReceiveInform.BackgroundColor = SystemColors.Control;
            style14.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style14.BackColor = SystemColors.Control;
            style14.ForeColor = SystemColors.WindowText;
            style14.SelectionBackColor = SystemColors.Highlight;
            style14.SelectionForeColor = SystemColors.HighlightText;
            style14.WrapMode = DataGridViewTriState.True;
            this.dgvReceiveInform.ColumnHeadersDefaultCellStyle = style14;
            this.dgvReceiveInform.ColumnHeadersHeight = 0x20;
            this.dgvReceiveInform.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvReceiveInform.Columns.AddRange(new DataGridViewColumn[] { this.cRNOutCode, this.cRNInform, this.cRNInformSound });
            this.dgvReceiveInform.DataSource = this.bdsRcvNotice;
            this.dgvReceiveInform.Dock = DockStyle.Fill;
            this.dgvReceiveInform.Location = new Point(3, 40);
            this.dgvReceiveInform.MultiSelect = false;
            this.dgvReceiveInform.Name = "dgvReceiveInform";
            style15.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style15.BackColor = SystemColors.Control;
            style15.ForeColor = SystemColors.WindowText;
            style15.SelectionBackColor = SystemColors.Highlight;
            style15.SelectionForeColor = SystemColors.HighlightText;
            style15.WrapMode = DataGridViewTriState.True;
            this.dgvReceiveInform.RowHeadersDefaultCellStyle = style15;
            this.dgvReceiveInform.RowTemplate.Height = 0x15;
            this.dgvReceiveInform.Size = new Size(0x124, 0x152);
            this.dgvReceiveInform.TabIndex = 1;
            this.dgvReceiveInform.Tag = "Notification";
            this.dgvReceiveInform.CellContentClick += new DataGridViewCellEventHandler(this.dgv_CellContentClick);
            this.dgvReceiveInform.CellEndEdit += new DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            this.dgvReceiveInform.CellValidating += new DataGridViewCellValidatingEventHandler(this.dgv_CellValidating);
            this.dgvReceiveInform.DataError += new DataGridViewDataErrorEventHandler(this.dgv_DataError);
            this.dgvReceiveInform.RowsAdded += new DataGridViewRowsAddedEventHandler(this.dgv_RowsAdded);
            this.dgvReceiveInform.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.dgv_RowsRemoved);
            this.dgvReceiveInform.RowValidating += new DataGridViewCellCancelEventHandler(this.dgv_RowValidating);
            this.dgvReceiveInform.SelectionChanged += new EventHandler(this.dgv_SelectionChanged);
            this.cRNOutCode.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.cRNOutCode.DataPropertyName = "clmRNOutCode";
            this.cRNOutCode.HeaderText = "M\x00e3 th\x00f4ng điệp đầu ra";
            this.cRNOutCode.MaxInputLength = 6;
            this.cRNOutCode.Name = "cRNOutCode";
            this.cRNOutCode.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.cRNOutCode.Width = 120;
            this.cRNInform.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.cRNInform.DataPropertyName = "clmRNInform";
            this.cRNInform.HeaderText = "Th\x00f4ng b\x00e1o";
            this.cRNInform.Name = "cRNInform";
            this.cRNInform.Width = 70;
            this.cRNInformSound.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.cRNInformSound.DataPropertyName = "clmRNInformSound";
            this.cRNInformSound.HeaderText = "\x00c2m b\x00e1o";
            this.cRNInformSound.Name = "cRNInformSound";
            this.cRNInformSound.Width = 60;
            this.bdnRcvNotice.AddNewItem = this.bdnRNAddNewItem;
            this.bdnRcvNotice.BindingSource = this.bdsRcvNotice;
            this.bdnRcvNotice.CountItem = this.bdnRNCountItem;
            this.bdnRcvNotice.DeleteItem = this.bdnRNDeleteItem;
            this.bdnRcvNotice.Items.AddRange(new ToolStripItem[] { this.bdnRNMoveFirstItem, this.bdnRNMovePreviousItem, this.bdnRNSeparator1, this.bdnRNPositionItem, this.bdnRNCountItem, this.bdnRNSeparator2, this.bdnRNMoveNextItem, this.bdnRNMoveLastItem, this.bdnRNSeparator3, this.bdnRNAddNewItem, this.bdnRNDeleteItem });
            this.bdnRcvNotice.Location = new Point(3, 15);
            this.bdnRcvNotice.MoveFirstItem = this.bdnRNMoveFirstItem;
            this.bdnRcvNotice.MoveLastItem = this.bdnRNMoveLastItem;
            this.bdnRcvNotice.MoveNextItem = this.bdnRNMoveNextItem;
            this.bdnRcvNotice.MovePreviousItem = this.bdnRNMovePreviousItem;
            this.bdnRcvNotice.Name = "bdnRcvNotice";
            this.bdnRcvNotice.PositionItem = this.bdnRNPositionItem;
            this.bdnRcvNotice.Size = new Size(0x124, 0x19);
            this.bdnRcvNotice.TabIndex = 0;
            this.bdnRcvNotice.Text = "bdnRcvNotice";
            this.bdnRNAddNewItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnRNAddNewItem.Image = (Image) manager.GetObject("bdnRNAddNewItem.Image");
            this.bdnRNAddNewItem.Name = "bdnRNAddNewItem";
            this.bdnRNAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bdnRNAddNewItem.Size = new Size(0x17, 0x16);
            this.bdnRNAddNewItem.Tag = "Add";
            this.bdnRNAddNewItem.Text = "Th\x00eam";
            this.bdnRNAddNewItem.ToolTipText = "Add";
            this.bdnRNAddNewItem.Click += new EventHandler(this.bdnAddNewItem_Click);
            this.bdnRNCountItem.Name = "bdnRNCountItem";
            this.bdnRNCountItem.Size = new Size(0x26, 0x16);
            this.bdnRNCountItem.Text = "/ {0}";
            this.bdnRNCountItem.ToolTipText = "Total items";
            this.bdnRNDeleteItem.CheckOnClick = true;
            this.bdnRNDeleteItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnRNDeleteItem.Image = (Image) manager.GetObject("bdnRNDeleteItem.Image");
            this.bdnRNDeleteItem.Name = "bdnRNDeleteItem";
            this.bdnRNDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bdnRNDeleteItem.Size = new Size(0x17, 0x16);
            this.bdnRNDeleteItem.Tag = "Del";
            this.bdnRNDeleteItem.Text = "X\x00f3a";
            this.bdnRNDeleteItem.ToolTipText = "Delete";
            this.bdnRNDeleteItem.Click += new EventHandler(this.bdnDeleteItem_Click);
            this.bdnRNMoveFirstItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnRNMoveFirstItem.Image = (Image) manager.GetObject("bdnRNMoveFirstItem.Image");
            this.bdnRNMoveFirstItem.Name = "bdnRNMoveFirstItem";
            this.bdnRNMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bdnRNMoveFirstItem.Size = new Size(0x17, 0x16);
            this.bdnRNMoveFirstItem.Text = "Hạng mục đầu ti\x00ean";
            this.bdnRNMoveFirstItem.ToolTipText = "First item";
            this.bdnRNMovePreviousItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnRNMovePreviousItem.Image = (Image) manager.GetObject("bdnRNMovePreviousItem.Image");
            this.bdnRNMovePreviousItem.Name = "bdnRNMovePreviousItem";
            this.bdnRNMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bdnRNMovePreviousItem.Size = new Size(0x17, 0x16);
            this.bdnRNMovePreviousItem.Text = "Hạng mục trước";
            this.bdnRNMovePreviousItem.ToolTipText = "Previous item";
            this.bdnRNSeparator1.Name = "bdnRNSeparator1";
            this.bdnRNSeparator1.Size = new Size(6, 0x19);
            this.bdnRNPositionItem.AccessibleName = "位置";
            this.bdnRNPositionItem.AutoSize = false;
            this.bdnRNPositionItem.Name = "bdnRNPositionItem";
            this.bdnRNPositionItem.Size = new Size(50, 0x13);
            this.bdnRNPositionItem.Text = "0";
            this.bdnRNPositionItem.ToolTipText = "Present location";
            this.bdnRNSeparator2.Name = "bdnRNSeparator2";
            this.bdnRNSeparator2.Size = new Size(6, 0x19);
            this.bdnRNMoveNextItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnRNMoveNextItem.Image = (Image) manager.GetObject("bdnRNMoveNextItem.Image");
            this.bdnRNMoveNextItem.Name = "bdnRNMoveNextItem";
            this.bdnRNMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bdnRNMoveNextItem.Size = new Size(0x17, 0x16);
            this.bdnRNMoveNextItem.Text = "Hạng mục tiếp theo";
            this.bdnRNMoveNextItem.ToolTipText = "Next item";
            this.bdnRNMoveLastItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnRNMoveLastItem.Image = (Image) manager.GetObject("bdnRNMoveLastItem.Image");
            this.bdnRNMoveLastItem.Name = "bdnRNMoveLastItem";
            this.bdnRNMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bdnRNMoveLastItem.Size = new Size(0x17, 0x16);
            this.bdnRNMoveLastItem.Text = "hạng mục cuối c\x00f9ng";
            this.bdnRNMoveLastItem.ToolTipText = "Last item";
            this.bdnRNSeparator3.Name = "bdnRNSeparator3";
            this.bdnRNSeparator3.Size = new Size(6, 0x19);
            this.btnSoundFile.Location = new Point(0x1b6, 20);
            this.btnSoundFile.Name = "btnSoundFile";
            this.btnSoundFile.Size = new Size(70, 0x17);
            this.btnSoundFile.TabIndex = 3;
            this.btnSoundFile.Text = "Đường dẫn";
            this.btnSoundFile.UseVisualStyleBackColor = true;
            this.btnSoundFile.Click += new EventHandler(this.btnSoundFile_Click);
            this.lblSoundFile.AutoSize = true;
            this.lblSoundFile.Location = new Point(0xb1, 0x19);
            this.lblSoundFile.Name = "lblSoundFile";
            this.lblSoundFile.Size = new Size(0x2c, 12);
            this.lblSoundFile.TabIndex = 1;
            this.lblSoundFile.Text = "\x00c2m b\x00e1o";
            this.txbSoundFile.Location = new Point(0xe0, 0x16);
            this.txbSoundFile.Name = "txbSoundFile";
            this.txbSoundFile.ReadOnly = true;
            this.txbSoundFile.Size = new Size(0xd0, 0x13);
            this.txbSoundFile.TabIndex = 2;
            this.txbSoundFile.TabStop = false;
            this.ckbRcvInform.AutoSize = true;
            this.ckbRcvInform.Location = new Point(0x13, 0x18);
            this.ckbRcvInform.Name = "ckbRcvInform";
            this.ckbRcvInform.Size = new Size(0x75, 0x10);
            this.ckbRcvInform.TabIndex = 0;
            this.ckbRcvInform.Text = "Sử dụng th\x00f4ng b\x00e1o";
            this.ckbRcvInform.UseVisualStyleBackColor = true;
            this.ckbRcvInform.CheckedChanged += new EventHandler(this.ckbRcvInform_CheckedChanged);
            this.tbpFileSaveC.Controls.Add(this.gpbFileSaveC);
            this.tbpFileSaveC.Location = new Point(4, 40);
            this.tbpFileSaveC.Name = "tbpFileSaveC";
            this.tbpFileSaveC.Size = new Size(0x389, 0x1d4);
            this.tbpFileSaveC.TabIndex = 6;
            this.tbpFileSaveC.Text = "Tự động lưu tệp tin (M\x00e3 th\x00f4ng điệp đầu ra)";
            this.tbpFileSaveC.UseVisualStyleBackColor = true;
            this.gpbFileSaveC.Controls.Add(this.dgvFileSaveC);
            this.gpbFileSaveC.Controls.Add(this.bdnFileSaveC);
            this.gpbFileSaveC.Location = new Point(3, 15);
            this.gpbFileSaveC.Name = "gpbFileSaveC";
            this.gpbFileSaveC.Size = new Size(710, 0x1c1);
            this.gpbFileSaveC.TabIndex = 0;
            this.gpbFileSaveC.TabStop = false;
            this.gpbFileSaveC.Text = "Tự động lưu tệp tin (M\x00e3 th\x00f4ng điệp đầu ra)";
            this.dgvFileSaveC.AllowUserToAddRows = false;
            style16.BackColor = SystemColors.GradientInactiveCaption;
            this.dgvFileSaveC.AlternatingRowsDefaultCellStyle = style16;
            this.dgvFileSaveC.AutoGenerateColumns = false;
            this.dgvFileSaveC.BackgroundColor = SystemColors.Control;
            style17.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style17.BackColor = SystemColors.Control;
            style17.ForeColor = SystemColors.WindowText;
            style17.SelectionBackColor = SystemColors.Highlight;
            style17.SelectionForeColor = SystemColors.HighlightText;
            style17.WrapMode = DataGridViewTriState.True;
            this.dgvFileSaveC.ColumnHeadersDefaultCellStyle = style17;
            this.dgvFileSaveC.ColumnHeadersHeight = 0x20;
            this.dgvFileSaveC.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvFileSaveC.Columns.AddRange(new DataGridViewColumn[] { this.cFSCOutCode, this.cFSCAutoSave, this.cFSCDir, this.cFSCBtn });
            this.dgvFileSaveC.DataSource = this.bdsFileSaveC;
            this.dgvFileSaveC.Dock = DockStyle.Fill;
            this.dgvFileSaveC.Location = new Point(3, 40);
            this.dgvFileSaveC.MultiSelect = false;
            this.dgvFileSaveC.Name = "dgvFileSaveC";
            style18.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style18.BackColor = SystemColors.Control;
            style18.ForeColor = SystemColors.WindowText;
            style18.SelectionBackColor = SystemColors.Highlight;
            style18.SelectionForeColor = SystemColors.HighlightText;
            style18.WrapMode = DataGridViewTriState.True;
            this.dgvFileSaveC.RowHeadersDefaultCellStyle = style18;
            this.dgvFileSaveC.RowHeadersWidth = 30;
            this.dgvFileSaveC.RowTemplate.Height = 0x15;
            this.dgvFileSaveC.Size = new Size(0x2c0, 0x196);
            this.dgvFileSaveC.TabIndex = 1;
            this.dgvFileSaveC.Tag = "Auto save(Output information code)";
            this.dgvFileSaveC.CellContentClick += new DataGridViewCellEventHandler(this.dgv_CellContentClick);
            this.dgvFileSaveC.CellEndEdit += new DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            this.dgvFileSaveC.CellValidating += new DataGridViewCellValidatingEventHandler(this.dgv_CellValidating);
            this.dgvFileSaveC.DataError += new DataGridViewDataErrorEventHandler(this.dgv_DataError);
            this.dgvFileSaveC.RowsAdded += new DataGridViewRowsAddedEventHandler(this.dgv_RowsAdded);
            this.dgvFileSaveC.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.dgv_RowsRemoved);
            this.dgvFileSaveC.RowValidating += new DataGridViewCellCancelEventHandler(this.dgv_RowValidating);
            this.dgvFileSaveC.SelectionChanged += new EventHandler(this.dgv_SelectionChanged);
            this.cFSCOutCode.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.cFSCOutCode.DataPropertyName = "clmFSCOutCode";
            this.cFSCOutCode.HeaderText = "M\x00e3 th\x00f4ng điệp đầu ra";
            this.cFSCOutCode.MaxInputLength = 6;
            this.cFSCOutCode.Name = "cFSCOutCode";
            this.cFSCOutCode.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.cFSCOutCode.Width = 120;
            this.cFSCAutoSave.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.cFSCAutoSave.DataPropertyName = "clmFSCAutoSave";
            this.cFSCAutoSave.HeaderText = "Tự động";
            this.cFSCAutoSave.Name = "cFSCAutoSave";
            this.cFSCAutoSave.Width = 0x41;
            this.cFSCDir.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.cFSCDir.DataPropertyName = "clmFSCDir";
            style19.BackColor = SystemColors.Control;
            this.cFSCDir.DefaultCellStyle = style19;
            this.cFSCDir.HeaderText = "Lưu tại";
            this.cFSCDir.Name = "cFSCDir";
            this.cFSCDir.ReadOnly = true;
            this.cFSCDir.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.cFSCBtn.DataPropertyName = "clmFSCBtn";
            this.cFSCBtn.HeaderText = "";
            this.cFSCBtn.Name = "cFSCBtn";
            this.cFSCBtn.Resizable = DataGridViewTriState.True;
            this.cFSCBtn.Width = 0x41;
            this.bdnFileSaveC.AddNewItem = this.bdnASCAddNewItem;
            this.bdnFileSaveC.BindingSource = this.bdsFileSaveC;
            this.bdnFileSaveC.CountItem = this.bdnASCCountItem;
            this.bdnFileSaveC.DeleteItem = this.bdnASCDeleteItem;
            this.bdnFileSaveC.Items.AddRange(new ToolStripItem[] { this.bdnASCMoveFirstItem, this.bdnASCMovePreviousItem, this.bdnASCSeparator1, this.bdnASCPositionItem, this.bdnASCCountItem, this.bdnASCSeparator2, this.bdnASCMoveNextItem, this.bdnASCMoveLastItem, this.bdnASCSeparator3, this.bdnASCAddNewItem, this.bdnASCDeleteItem });
            this.bdnFileSaveC.Location = new Point(3, 15);
            this.bdnFileSaveC.MoveFirstItem = this.bdnASCMoveFirstItem;
            this.bdnFileSaveC.MoveLastItem = this.bdnASCMoveLastItem;
            this.bdnFileSaveC.MoveNextItem = this.bdnASCMoveNextItem;
            this.bdnFileSaveC.MovePreviousItem = this.bdnASCMovePreviousItem;
            this.bdnFileSaveC.Name = "bdnFileSaveC";
            this.bdnFileSaveC.PositionItem = this.bdnASCPositionItem;
            this.bdnFileSaveC.Size = new Size(0x2c0, 0x19);
            this.bdnFileSaveC.TabIndex = 0;
            this.bdnFileSaveC.Text = "bindingNavigator3";
            this.bdnASCAddNewItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnASCAddNewItem.Image = (Image) manager.GetObject("bdnASCAddNewItem.Image");
            this.bdnASCAddNewItem.Name = "bdnASCAddNewItem";
            this.bdnASCAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bdnASCAddNewItem.Size = new Size(0x17, 0x16);
            this.bdnASCAddNewItem.Tag = "Add";
            this.bdnASCAddNewItem.Text = "Th\x00eam";
            this.bdnASCAddNewItem.ToolTipText = "Add";
            this.bdnASCAddNewItem.Click += new EventHandler(this.bdnAddNewItem_Click);
            this.bdnASCCountItem.Name = "bdnASCCountItem";
            this.bdnASCCountItem.Size = new Size(0x26, 0x16);
            this.bdnASCCountItem.Text = "/ {0}";
            this.bdnASCCountItem.ToolTipText = "Total items";
            this.bdnASCDeleteItem.CheckOnClick = true;
            this.bdnASCDeleteItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnASCDeleteItem.Image = (Image) manager.GetObject("bdnASCDeleteItem.Image");
            this.bdnASCDeleteItem.Name = "bdnASCDeleteItem";
            this.bdnASCDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bdnASCDeleteItem.Size = new Size(0x17, 0x16);
            this.bdnASCDeleteItem.Tag = "Del";
            this.bdnASCDeleteItem.Text = "X\x00f3a";
            this.bdnASCDeleteItem.ToolTipText = "Delete";
            this.bdnASCDeleteItem.Click += new EventHandler(this.bdnDeleteItem_Click);
            this.bdnASCMoveFirstItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnASCMoveFirstItem.Image = (Image) manager.GetObject("bdnASCMoveFirstItem.Image");
            this.bdnASCMoveFirstItem.Name = "bdnASCMoveFirstItem";
            this.bdnASCMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bdnASCMoveFirstItem.Size = new Size(0x17, 0x16);
            this.bdnASCMoveFirstItem.Text = "Hạng mục đầu ti\x00ean";
            this.bdnASCMoveFirstItem.ToolTipText = "First item";
            this.bdnASCMovePreviousItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnASCMovePreviousItem.Image = (Image) manager.GetObject("bdnASCMovePreviousItem.Image");
            this.bdnASCMovePreviousItem.Name = "bdnASCMovePreviousItem";
            this.bdnASCMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bdnASCMovePreviousItem.Size = new Size(0x17, 0x16);
            this.bdnASCMovePreviousItem.Text = "Hạng mục trước";
            this.bdnASCMovePreviousItem.ToolTipText = "Previous item";
            this.bdnASCSeparator1.Name = "bdnASCSeparator1";
            this.bdnASCSeparator1.Size = new Size(6, 0x19);
            this.bdnASCPositionItem.AccessibleName = "位置";
            this.bdnASCPositionItem.AutoSize = false;
            this.bdnASCPositionItem.Name = "bdnASCPositionItem";
            this.bdnASCPositionItem.Size = new Size(50, 0x13);
            this.bdnASCPositionItem.Text = "0";
            this.bdnASCPositionItem.ToolTipText = "Present location";
            this.bdnASCSeparator2.Name = "bdnASCSeparator2";
            this.bdnASCSeparator2.Size = new Size(6, 0x19);
            this.bdnASCMoveNextItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnASCMoveNextItem.Image = (Image) manager.GetObject("bdnASCMoveNextItem.Image");
            this.bdnASCMoveNextItem.Name = "bdnASCMoveNextItem";
            this.bdnASCMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bdnASCMoveNextItem.Size = new Size(0x17, 0x16);
            this.bdnASCMoveNextItem.Text = "Hạng mục tiếp theo";
            this.bdnASCMoveNextItem.ToolTipText = "Next item";
            this.bdnASCMoveLastItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnASCMoveLastItem.Image = (Image) manager.GetObject("bdnASCMoveLastItem.Image");
            this.bdnASCMoveLastItem.Name = "bdnASCMoveLastItem";
            this.bdnASCMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bdnASCMoveLastItem.Size = new Size(0x17, 0x16);
            this.bdnASCMoveLastItem.Text = "hạng mục cuối c\x00f9ng";
            this.bdnASCMoveLastItem.ToolTipText = "Last item";
            this.bdnASCSeparator3.Name = "bdnASCSeparator3";
            this.bdnASCSeparator3.Size = new Size(6, 0x19);
            this.tbpFileSaveK.Controls.Add(this.gpbKanriFile);
            this.tbpFileSaveK.Controls.Add(this.ckbFileSave);
            this.tbpFileSaveK.Controls.Add(this.gpbFileSaveK);
            this.tbpFileSaveK.Controls.Add(this.gpbSendFile);
            this.tbpFileSaveK.Controls.Add(this.gpbFileNameRule);
            this.tbpFileSaveK.Location = new Point(4, 40);
            this.tbpFileSaveK.Name = "tbpFileSaveK";
            this.tbpFileSaveK.Size = new Size(0x389, 0x1d4);
            this.tbpFileSaveK.TabIndex = 5;
            this.tbpFileSaveK.Text = "Tự động lưu tệp tin (theo loại th\x00f4ng điệp)";
            this.tbpFileSaveK.UseVisualStyleBackColor = true;
            this.gpbKanriFile.Controls.Add(this.btnKanriFile);
            this.gpbKanriFile.Controls.Add(this.txbKanriFile);
            this.gpbKanriFile.Location = new Point(4, 0x197);
            this.gpbKanriFile.Name = "gpbKanriFile";
            this.gpbKanriFile.Size = new Size(0x1c7, 0x3d);
            this.gpbKanriFile.TabIndex = 4;
            this.gpbKanriFile.TabStop = false;
            this.gpbKanriFile.Text = "管理資料電文ファイル既定保存先";
            this.gpbKanriFile.Visible = false;
            this.btnKanriFile.Location = new Point(0x192, 0x16);
            this.btnKanriFile.Name = "btnKanriFile";
            this.btnKanriFile.Size = new Size(0x2e, 0x17);
            this.btnKanriFile.TabIndex = 1;
            this.btnKanriFile.Text = "参照";
            this.btnKanriFile.UseVisualStyleBackColor = true;
            this.btnKanriFile.Click += new EventHandler(this.btnChoiceDir_Click);
            this.txbKanriFile.Location = new Point(10, 0x17);
            this.txbKanriFile.Name = "txbKanriFile";
            this.txbKanriFile.ReadOnly = true;
            this.txbKanriFile.Size = new Size(0x180, 0x13);
            this.txbKanriFile.TabIndex = 0;
            this.txbKanriFile.TabStop = false;
            this.ckbFileSave.AutoSize = true;
            this.ckbFileSave.Checked = true;
            this.ckbFileSave.CheckState = CheckState.Checked;
            this.ckbFileSave.Location = new Point(14, 15);
            this.ckbFileSave.Name = "ckbFileSave";
            this.ckbFileSave.Size = new Size(0x75, 0x10);
            this.ckbFileSave.TabIndex = 0;
            this.ckbFileSave.Text = "Tự động lưu tệp tin";
            this.ckbFileSave.UseVisualStyleBackColor = true;
            this.ckbFileSave.CheckedChanged += new EventHandler(this.ckbFileSave_CheckedChanged);
            this.gpbFileSaveK.Controls.Add(this.dgvFileSaveK);
            this.gpbFileSaveK.Location = new Point(3, 0x2f);
            this.gpbFileSaveK.Name = "gpbFileSaveK";
            this.gpbFileSaveK.Size = new Size(0x308, 0xdb);
            this.gpbFileSaveK.TabIndex = 1;
            this.gpbFileSaveK.TabStop = false;
            this.gpbFileSaveK.Text = "Tự động lưu tệp tin (Ph\x00e2n loại)";
            this.dgvFileSaveK.AllowUserToAddRows = false;
            this.dgvFileSaveK.AllowUserToDeleteRows = false;
            style20.BackColor = SystemColors.GradientInactiveCaption;
            this.dgvFileSaveK.AlternatingRowsDefaultCellStyle = style20;
            this.dgvFileSaveK.AutoGenerateColumns = false;
            this.dgvFileSaveK.BackgroundColor = SystemColors.Control;
            style21.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style21.BackColor = SystemColors.Control;
            style21.ForeColor = SystemColors.WindowText;
            style21.SelectionBackColor = SystemColors.Highlight;
            style21.SelectionForeColor = SystemColors.HighlightText;
            style21.WrapMode = DataGridViewTriState.True;
            this.dgvFileSaveK.ColumnHeadersDefaultCellStyle = style21;
            this.dgvFileSaveK.ColumnHeadersHeight = 0x20;
            this.dgvFileSaveK.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvFileSaveK.Columns.AddRange(new DataGridViewColumn[] { this.cFSKDataName, this.cFSKFileSaveMode, this.cFSKDir, this.cFSKBtn, this.cFSKDataType });
            this.dgvFileSaveK.DataSource = this.bdsFileSaveK;
            this.dgvFileSaveK.Dock = DockStyle.Fill;
            this.dgvFileSaveK.Location = new Point(3, 15);
            this.dgvFileSaveK.Name = "dgvFileSaveK";
            style22.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style22.BackColor = SystemColors.Control;
            style22.ForeColor = SystemColors.WindowText;
            style22.SelectionBackColor = SystemColors.Highlight;
            style22.SelectionForeColor = SystemColors.HighlightText;
            style22.WrapMode = DataGridViewTriState.True;
            this.dgvFileSaveK.RowHeadersDefaultCellStyle = style22;
            this.dgvFileSaveK.RowHeadersWidth = 30;
            this.dgvFileSaveK.RowTemplate.Height = 0x15;
            this.dgvFileSaveK.Size = new Size(770, 0xc9);
            this.dgvFileSaveK.TabIndex = 1;
            this.dgvFileSaveK.Tag = "Auto save (Message type)";
            this.dgvFileSaveK.CellContentClick += new DataGridViewCellEventHandler(this.dgv_CellContentClick);
            this.dgvFileSaveK.RowValidating += new DataGridViewCellCancelEventHandler(this.dgv_RowValidating);
            this.cFSKDataName.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.cFSKDataName.DataPropertyName = "clmFSKDataName";
            this.cFSKDataName.HeaderText = "Loại th\x00f4ng điệp (M\x00e3 ph\x00e2n loại)";
            this.cFSKDataName.Name = "cFSKDataName";
            this.cFSKDataName.ReadOnly = true;
            this.cFSKDataName.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.cFSKDataName.Width = 230;
            this.cFSKFileSaveMode.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.cFSKFileSaveMode.DataPropertyName = "clmFSKFileSaveMode";
            this.cFSKFileSaveMode.HeaderText = "Tự động lưu";
            this.cFSKFileSaveMode.Name = "cFSKFileSaveMode";
            this.cFSKFileSaveMode.Width = 70;
            this.cFSKDir.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.cFSKDir.DataPropertyName = "clmFSKDir";
            style23.BackColor = SystemColors.Control;
            this.cFSKDir.DefaultCellStyle = style23;
            this.cFSKDir.HeaderText = "Lưu tại";
            this.cFSKDir.MaxInputLength = 0xff;
            this.cFSKDir.Name = "cFSKDir";
            this.cFSKDir.ReadOnly = true;
            this.cFSKDir.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.cFSKBtn.DataPropertyName = "clmFSKBtn";
            this.cFSKBtn.HeaderText = "";
            this.cFSKBtn.Name = "cFSKBtn";
            this.cFSKBtn.Resizable = DataGridViewTriState.True;
            this.cFSKBtn.Width = 0x41;
            this.cFSKDataType.DataPropertyName = "clmFSKDataType";
            this.cFSKDataType.HeaderText = "clmFSKDataType";
            this.cFSKDataType.Name = "cFSKDataType";
            this.cFSKDataType.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.cFSKDataType.Visible = false;
            this.gpbSendFile.Controls.Add(this.btnSendFile);
            this.gpbSendFile.Controls.Add(this.txbSendFile);
            this.gpbSendFile.Location = new Point(4, 340);
            this.gpbSendFile.Name = "gpbSendFile";
            this.gpbSendFile.Size = new Size(480, 0x3d);
            this.gpbSendFile.TabIndex = 3;
            this.gpbSendFile.TabStop = false;
            this.gpbSendFile.Text = "Thư mục lưu trữ mặc định cho c\x00e1c tệp tin gửi đi";
            this.btnSendFile.Location = new Point(0x192, 0x16);
            this.btnSendFile.Name = "btnSendFile";
            this.btnSendFile.Size = new Size(0x48, 0x17);
            this.btnSendFile.TabIndex = 1;
            this.btnSendFile.Text = "Đường dẫn";
            this.btnSendFile.UseVisualStyleBackColor = true;
            this.btnSendFile.Click += new EventHandler(this.btnChoiceDir_Click);
            this.txbSendFile.Location = new Point(10, 0x17);
            this.txbSendFile.Name = "txbSendFile";
            this.txbSendFile.ReadOnly = true;
            this.txbSendFile.Size = new Size(0x180, 0x13);
            this.txbSendFile.TabIndex = 0;
            this.txbSendFile.TabStop = false;
            this.gpbFileNameRule.Controls.Add(this.lblFileNameRule);
            this.gpbFileNameRule.Controls.Add(this.lblPlus4);
            this.gpbFileNameRule.Controls.Add(this.cmbFileNameRule4);
            this.gpbFileNameRule.Controls.Add(this.lblPlus3);
            this.gpbFileNameRule.Controls.Add(this.lblPlus2);
            this.gpbFileNameRule.Controls.Add(this.cmbFileNameRule3);
            this.gpbFileNameRule.Controls.Add(this.lblPlus1);
            this.gpbFileNameRule.Controls.Add(this.cmbFileNameRule2);
            this.gpbFileNameRule.Controls.Add(this.cmbFileNameRule1);
            this.gpbFileNameRule.Location = new Point(4, 0x110);
            this.gpbFileNameRule.Name = "gpbFileNameRule";
            this.gpbFileNameRule.Size = new Size(0x2af, 0x35);
            this.gpbFileNameRule.TabIndex = 2;
            this.gpbFileNameRule.TabStop = false;
            this.gpbFileNameRule.Text = "Quy tắc đặt t\x00ean tệp tin";
            this.lblFileNameRule.AutoSize = true;
            this.lblFileNameRule.Location = new Point(0x233, 0x15);
            this.lblFileNameRule.Name = "lblFileNameRule";
            this.lblFileNameRule.Size = new Size(0x15, 12);
            this.lblFileNameRule.TabIndex = 8;
            this.lblFileNameRule.Text = ".txt";
            this.lblPlus4.AutoSize = true;
            this.lblPlus4.Location = new Point(540, 0x15);
            this.lblPlus4.Name = "lblPlus4";
            this.lblPlus4.Size = new Size(0x11, 12);
            this.lblPlus4.TabIndex = 7;
            this.lblPlus4.Text = "＋";
            this.cmbFileNameRule4.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbFileNameRule4.FormattingEnabled = true;
            this.cmbFileNameRule4.Items.AddRange(new object[] { "", "M\x00e3 người sử dụng", "M\x00e3 th\x00f4ng điệp đầu ra", "Ti\x00eau đề", "Ng\x00e0y th\x00e1ng" });
            this.cmbFileNameRule4.Location = new Point(0x1a9, 0x12);
            this.cmbFileNameRule4.Name = "cmbFileNameRule4";
            this.cmbFileNameRule4.Size = new Size(0x6d, 20);
            this.cmbFileNameRule4.TabIndex = 6;
            this.lblPlus3.AutoSize = true;
            this.lblPlus3.Location = new Point(0x192, 0x15);
            this.lblPlus3.Name = "lblPlus3";
            this.lblPlus3.Size = new Size(0x11, 12);
            this.lblPlus3.TabIndex = 5;
            this.lblPlus3.Text = "＋";
            this.lblPlus2.AutoSize = true;
            this.lblPlus2.Location = new Point(0x108, 0x15);
            this.lblPlus2.Name = "lblPlus2";
            this.lblPlus2.Size = new Size(0x11, 12);
            this.lblPlus2.TabIndex = 3;
            this.lblPlus2.Text = "＋";
            this.cmbFileNameRule3.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbFileNameRule3.FormattingEnabled = true;
            this.cmbFileNameRule3.Items.AddRange(new object[] { "", "M\x00e3 người sử dụng", "M\x00e3 th\x00f4ng điệp đầu ra", "Ti\x00eau đề", "Ng\x00e0y th\x00e1ng" });
            this.cmbFileNameRule3.Location = new Point(0x11f, 0x12);
            this.cmbFileNameRule3.Name = "cmbFileNameRule3";
            this.cmbFileNameRule3.Size = new Size(0x6d, 20);
            this.cmbFileNameRule3.TabIndex = 4;
            this.lblPlus1.AutoSize = true;
            this.lblPlus1.Location = new Point(0x7e, 0x15);
            this.lblPlus1.Name = "lblPlus1";
            this.lblPlus1.Size = new Size(0x11, 12);
            this.lblPlus1.TabIndex = 1;
            this.lblPlus1.Text = "＋";
            this.cmbFileNameRule2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbFileNameRule2.FormattingEnabled = true;
            this.cmbFileNameRule2.Items.AddRange(new object[] { "", "M\x00e3 người sử dụng", "M\x00e3 th\x00f4ng điệp đầu ra", "Ti\x00eau đề", "Ng\x00e0y th\x00e1ng" });
            this.cmbFileNameRule2.Location = new Point(0x95, 0x12);
            this.cmbFileNameRule2.Name = "cmbFileNameRule2";
            this.cmbFileNameRule2.Size = new Size(0x6d, 20);
            this.cmbFileNameRule2.TabIndex = 2;
            this.cmbFileNameRule1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbFileNameRule1.FormattingEnabled = true;
            this.cmbFileNameRule1.Items.AddRange(new object[] { "M\x00e3 người sử dụng", "M\x00e3 th\x00f4ng điệp đầu ra", "Ti\x00eau đề", "Ng\x00e0y th\x00e1ng" });
            this.cmbFileNameRule1.Location = new Point(11, 0x12);
            this.cmbFileNameRule1.Name = "cmbFileNameRule1";
            this.cmbFileNameRule1.Size = new Size(0x6d, 20);
            this.cmbFileNameRule1.TabIndex = 0;
            this.tbpMsgCls.Controls.Add(this.gpbMsgCls);
            this.tbpMsgCls.Location = new Point(4, 40);
            this.tbpMsgCls.Name = "tbpMsgCls";
            this.tbpMsgCls.Size = new Size(0x389, 0x1d4);
            this.tbpMsgCls.TabIndex = 10;
            this.tbpMsgCls.Text = "Ph\x00e2n loại th\x00f4ng điệp";
            this.tbpMsgCls.UseVisualStyleBackColor = true;
            this.gpbMsgCls.Controls.Add(this.dgvMsgCls);
            this.gpbMsgCls.Controls.Add(this.bdnMsgCls);
            this.gpbMsgCls.Dock = DockStyle.Top;
            this.gpbMsgCls.Location = new Point(0, 0);
            this.gpbMsgCls.Name = "gpbMsgCls";
            this.gpbMsgCls.Size = new Size(0x389, 0x1d5);
            this.gpbMsgCls.TabIndex = 0;
            this.gpbMsgCls.TabStop = false;
            this.gpbMsgCls.Text = "Ph\x00e2n loại th\x00f4ng điệp nhận ";
            this.dgvMsgCls.AllowUserToAddRows = false;
            style24.BackColor = SystemColors.GradientInactiveCaption;
            this.dgvMsgCls.AlternatingRowsDefaultCellStyle = style24;
            this.dgvMsgCls.AutoGenerateColumns = false;
            this.dgvMsgCls.BackgroundColor = SystemColors.Control;
            style25.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style25.BackColor = SystemColors.Control;
            style25.ForeColor = SystemColors.WindowText;
            style25.SelectionBackColor = SystemColors.Highlight;
            style25.SelectionForeColor = SystemColors.HighlightText;
            style25.WrapMode = DataGridViewTriState.True;
            this.dgvMsgCls.ColumnHeadersDefaultCellStyle = style25;
            this.dgvMsgCls.ColumnHeadersHeight = 0x20;
            this.dgvMsgCls.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMsgCls.Columns.AddRange(new DataGridViewColumn[] { this.cMCUserCode, this.cMCJobCode, this.cMCOutCode, this.cMCBankNumber, this.cMCUnOpen, this.cMCBankWithdrawday, this.cMCFolder, this.cMCBtn, this.cMCId, this.cMCPriority });
            this.dgvMsgCls.DataSource = this.bdsMsgCls;
            this.dgvMsgCls.Dock = DockStyle.Fill;
            this.dgvMsgCls.Location = new Point(3, 40);
            this.dgvMsgCls.MultiSelect = false;
            this.dgvMsgCls.Name = "dgvMsgCls";
            style26.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style26.BackColor = SystemColors.Control;
            style26.ForeColor = SystemColors.WindowText;
            style26.SelectionBackColor = SystemColors.Highlight;
            style26.SelectionForeColor = SystemColors.HighlightText;
            style26.WrapMode = DataGridViewTriState.True;
            this.dgvMsgCls.RowHeadersDefaultCellStyle = style26;
            this.dgvMsgCls.RowHeadersWidth = 0x23;
            this.dgvMsgCls.RowTemplate.Height = 0x15;
            this.dgvMsgCls.Size = new Size(0x383, 0x1aa);
            this.dgvMsgCls.TabIndex = 1;
            this.dgvMsgCls.Tag = "Assort message";
            this.dgvMsgCls.CellContentClick += new DataGridViewCellEventHandler(this.dgv_CellContentClick);
            this.dgvMsgCls.CellEndEdit += new DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            this.dgvMsgCls.CellValidating += new DataGridViewCellValidatingEventHandler(this.dgv_CellValidating);
            this.dgvMsgCls.DataError += new DataGridViewDataErrorEventHandler(this.dgv_DataError);
            this.dgvMsgCls.RowsAdded += new DataGridViewRowsAddedEventHandler(this.dgv_RowsAdded);
            this.dgvMsgCls.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.dgv_RowsRemoved);
            this.dgvMsgCls.RowValidating += new DataGridViewCellCancelEventHandler(this.dgv_RowValidating);
            this.dgvMsgCls.SelectionChanged += new EventHandler(this.dgv_SelectionChanged);
            this.cMCUserCode.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.cMCUserCode.DataPropertyName = "clmMCUserCode";
            this.cMCUserCode.HeaderText = "M\x00e3 người sử dụng";
            this.cMCUserCode.MaxInputLength = 5;
            this.cMCUserCode.Name = "cMCUserCode";
            this.cMCUserCode.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.cMCJobCode.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.cMCJobCode.DataPropertyName = "clmMCJobCode";
            this.cMCJobCode.HeaderText = "M\x00e3 nghiệp vụ";
            this.cMCJobCode.MaxInputLength = 5;
            this.cMCJobCode.Name = "cMCJobCode";
            this.cMCJobCode.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.cMCOutCode.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.cMCOutCode.DataPropertyName = "clmMCOutCode";
            this.cMCOutCode.HeaderText = "M\x00e3 th\x00f4ng điệp đầu ra";
            this.cMCOutCode.MaxInputLength = 6;
            this.cMCOutCode.Name = "cMCOutCode";
            this.cMCOutCode.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.cMCOutCode.Width = 120;
            this.cMCBankNumber.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.cMCBankNumber.DataPropertyName = "clmMCBankNumber";
            this.cMCBankNumber.HeaderText = "口座番号";
            this.cMCBankNumber.MaxInputLength = 0x10;
            this.cMCBankNumber.Name = "cMCBankNumber";
            this.cMCBankNumber.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.cMCBankNumber.Width = 140;
            this.cMCUnOpen.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.cMCUnOpen.DataPropertyName = "clmMCUnOpen";
            this.cMCUnOpen.HeaderText = "ph\x00e2n loại chưa đọc";
            this.cMCUnOpen.Name = "cMCUnOpen";
            this.cMCUnOpen.Width = 110;
            this.cMCBankWithdrawday.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.cMCBankWithdrawday.DataPropertyName = "clmMCBankWithdrawday";
            this.cMCBankWithdrawday.HeaderText = "引落日振り分け";
            this.cMCBankWithdrawday.Name = "cMCBankWithdrawday";
            this.cMCBankWithdrawday.Width = 0x37;
            this.cMCFolder.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.cMCFolder.DataPropertyName = "clmMCFolder";
            style27.BackColor = SystemColors.Control;
            this.cMCFolder.DefaultCellStyle = style27;
            this.cMCFolder.HeaderText = "Thư mục sử dụng";
            this.cMCFolder.Name = "cMCFolder";
            this.cMCFolder.ReadOnly = true;
            this.cMCFolder.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.cMCBtn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.cMCBtn.DataPropertyName = "clmMCBtn";
            this.cMCBtn.HeaderText = "";
            this.cMCBtn.Name = "cMCBtn";
            this.cMCBtn.Resizable = DataGridViewTriState.True;
            this.cMCBtn.Width = 70;
            this.cMCId.DataPropertyName = "clmMCId";
            this.cMCId.HeaderText = "clmMCId";
            this.cMCId.Name = "cMCId";
            this.cMCId.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.cMCId.Visible = false;
            this.cMCPriority.DataPropertyName = "clmMCPriority";
            this.cMCPriority.HeaderText = "clmMCPriority";
            this.cMCPriority.Name = "cMCPriority";
            this.cMCPriority.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.cMCPriority.Visible = false;
            this.bdnMsgCls.AddNewItem = this.bdnMCAddNewItem;
            this.bdnMsgCls.BindingSource = this.bdsMsgCls;
            this.bdnMsgCls.CountItem = this.bdnMCCountItem;
            this.bdnMsgCls.DeleteItem = this.bdnMCDeleteItem;
            this.bdnMsgCls.Items.AddRange(new ToolStripItem[] { this.bdnMCMoveFirstItem, this.bdnMCMovePreviousItem, this.bdnMCSeparator1, this.bdnMCPositionItem, this.bdnMCCountItem, this.bdnMCSeparator2, this.bdnMCMoveNextItem, this.bdnMCMoveLastItem, this.bdnMCSeparator3, this.bdnMCAddNewItem, this.bdnMCDeleteItem, this.tlsMCSeparator1, this.tlsMCbtnUp, this.tlsMCbtnDown });
            this.bdnMsgCls.Location = new Point(3, 15);
            this.bdnMsgCls.MoveFirstItem = this.bdnMCMoveFirstItem;
            this.bdnMsgCls.MoveLastItem = this.bdnMCMoveLastItem;
            this.bdnMsgCls.MoveNextItem = this.bdnMCMoveNextItem;
            this.bdnMsgCls.MovePreviousItem = this.bdnMCMovePreviousItem;
            this.bdnMsgCls.Name = "bdnMsgCls";
            this.bdnMsgCls.PositionItem = this.bdnMCPositionItem;
            this.bdnMsgCls.Size = new Size(0x383, 0x19);
            this.bdnMsgCls.TabIndex = 0;
            this.bdnMsgCls.Text = "bdnMsgCls";
            this.bdnMCAddNewItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnMCAddNewItem.Image = (Image) manager.GetObject("bdnMCAddNewItem.Image");
            this.bdnMCAddNewItem.Name = "bdnMCAddNewItem";
            this.bdnMCAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bdnMCAddNewItem.Size = new Size(0x17, 0x16);
            this.bdnMCAddNewItem.Tag = "Add";
            this.bdnMCAddNewItem.Text = "Th\x00eam";
            this.bdnMCAddNewItem.ToolTipText = "Add";
            this.bdnMCAddNewItem.Click += new EventHandler(this.bdnAddNewItem_Click);
            this.bdnMCCountItem.Name = "bdnMCCountItem";
            this.bdnMCCountItem.Size = new Size(0x26, 0x16);
            this.bdnMCCountItem.Text = "/ {0}";
            this.bdnMCCountItem.ToolTipText = "Total items";
            this.bdnMCDeleteItem.CheckOnClick = true;
            this.bdnMCDeleteItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnMCDeleteItem.Image = (Image) manager.GetObject("bdnMCDeleteItem.Image");
            this.bdnMCDeleteItem.Name = "bdnMCDeleteItem";
            this.bdnMCDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bdnMCDeleteItem.Size = new Size(0x17, 0x16);
            this.bdnMCDeleteItem.Tag = "Del";
            this.bdnMCDeleteItem.Text = "X\x00f3a";
            this.bdnMCDeleteItem.ToolTipText = "Delete";
            this.bdnMCDeleteItem.Click += new EventHandler(this.bdnDeleteItem_Click);
            this.bdnMCMoveFirstItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnMCMoveFirstItem.Image = (Image) manager.GetObject("bdnMCMoveFirstItem.Image");
            this.bdnMCMoveFirstItem.Name = "bdnMCMoveFirstItem";
            this.bdnMCMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bdnMCMoveFirstItem.Size = new Size(0x17, 0x16);
            this.bdnMCMoveFirstItem.Text = "Hạng mục đầu ti\x00ean";
            this.bdnMCMoveFirstItem.ToolTipText = "First item";
            this.bdnMCMovePreviousItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnMCMovePreviousItem.Image = (Image) manager.GetObject("bdnMCMovePreviousItem.Image");
            this.bdnMCMovePreviousItem.Name = "bdnMCMovePreviousItem";
            this.bdnMCMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bdnMCMovePreviousItem.Size = new Size(0x17, 0x16);
            this.bdnMCMovePreviousItem.Text = "Hạng mục trước";
            this.bdnMCMovePreviousItem.ToolTipText = "Previous item";
            this.bdnMCSeparator1.Name = "bdnMCSeparator1";
            this.bdnMCSeparator1.Size = new Size(6, 0x19);
            this.bdnMCPositionItem.AccessibleName = "位置";
            this.bdnMCPositionItem.AutoSize = false;
            this.bdnMCPositionItem.Name = "bdnMCPositionItem";
            this.bdnMCPositionItem.Size = new Size(50, 0x13);
            this.bdnMCPositionItem.Text = "0";
            this.bdnMCPositionItem.ToolTipText = "Present location";
            this.bdnMCSeparator2.Name = "bdnMCSeparator2";
            this.bdnMCSeparator2.Size = new Size(6, 0x19);
            this.bdnMCMoveNextItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnMCMoveNextItem.Image = (Image) manager.GetObject("bdnMCMoveNextItem.Image");
            this.bdnMCMoveNextItem.Name = "bdnMCMoveNextItem";
            this.bdnMCMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bdnMCMoveNextItem.Size = new Size(0x17, 0x16);
            this.bdnMCMoveNextItem.Text = "Hạng mục tiếp theo";
            this.bdnMCMoveNextItem.ToolTipText = "Next item";
            this.bdnMCMoveLastItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnMCMoveLastItem.Image = (Image) manager.GetObject("bdnMCMoveLastItem.Image");
            this.bdnMCMoveLastItem.Name = "bdnMCMoveLastItem";
            this.bdnMCMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bdnMCMoveLastItem.Size = new Size(0x17, 0x16);
            this.bdnMCMoveLastItem.Text = "hạng mục cuối c\x00f9ng";
            this.bdnMCMoveLastItem.ToolTipText = "Last item";
            this.bdnMCSeparator3.Name = "bdnMCSeparator3";
            this.bdnMCSeparator3.Size = new Size(6, 0x19);
            this.tlsMCSeparator1.Name = "tlsMCSeparator1";
            this.tlsMCSeparator1.Size = new Size(6, 0x19);
            this.tlsMCbtnUp.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tlsMCbtnUp.Image = (Image) manager.GetObject("tlsMCbtnUp.Image");
            this.tlsMCbtnUp.Name = "tlsMCbtnUp";
            this.tlsMCbtnUp.Size = new Size(0x17, 0x16);
            this.tlsMCbtnUp.Text = "Di chuyển l\x00ean tr\x00ean";
            this.tlsMCbtnUp.ToolTipText = "Move Up";
            this.tlsMCbtnUp.Click += new EventHandler(this.tlsMCbtnUp_Click);
            this.tlsMCbtnDown.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tlsMCbtnDown.Image = (Image) manager.GetObject("tlsMCbtnDown.Image");
            this.tlsMCbtnDown.Name = "tlsMCbtnDown";
            this.tlsMCbtnDown.Size = new Size(0x17, 0x16);
            this.tlsMCbtnDown.Text = "Di chuyển xuống dưới";
            this.tlsMCbtnDown.ToolTipText = "Move Down";
            this.tlsMCbtnDown.Click += new EventHandler(this.tlsMCbtnDown_Click);
            this.tbpAutoPrint.Controls.Add(this.ckbAutoPrt);
            this.tbpAutoPrint.Controls.Add(this.gpbPrintout);
            this.tbpAutoPrint.Location = new Point(4, 40);
            this.tbpAutoPrint.Name = "tbpAutoPrint";
            this.tbpAutoPrint.Size = new Size(0x389, 0x1d4);
            this.tbpAutoPrint.TabIndex = 4;
            this.tbpAutoPrint.Text = "In tự động";
            this.tbpAutoPrint.UseVisualStyleBackColor = true;
            this.ckbAutoPrt.Checked = true;
            this.ckbAutoPrt.CheckState = CheckState.Checked;
            this.ckbAutoPrt.Location = new Point(0x12, 13);
            this.ckbAutoPrt.Name = "ckbAutoPrt";
            this.ckbAutoPrt.Size = new Size(100, 0x10);
            this.ckbAutoPrt.TabIndex = 0;
            this.ckbAutoPrt.Text = "In tự động";
            this.ckbAutoPrt.UseVisualStyleBackColor = true;
            this.gpbPrintout.Controls.Add(this.dgvAutoPrint);
            this.gpbPrintout.Controls.Add(this.bdnAutoPrint);
            this.gpbPrintout.Location = new Point(0x12, 0x2d);
            this.gpbPrintout.Name = "gpbPrintout";
            this.gpbPrintout.Size = new Size(600, 0x19d);
            this.gpbPrintout.TabIndex = 1;
            this.gpbPrintout.TabStop = false;
            this.gpbPrintout.Text = "Loại b\x00e1o c\x00e1o";
            this.dgvAutoPrint.AllowUserToAddRows = false;
            style28.BackColor = SystemColors.GradientInactiveCaption;
            this.dgvAutoPrint.AlternatingRowsDefaultCellStyle = style28;
            this.dgvAutoPrint.AutoGenerateColumns = false;
            this.dgvAutoPrint.BackgroundColor = SystemColors.Control;
            style29.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style29.BackColor = SystemColors.Control;
            style29.ForeColor = SystemColors.WindowText;
            style29.SelectionBackColor = SystemColors.Highlight;
            style29.SelectionForeColor = SystemColors.HighlightText;
            style29.WrapMode = DataGridViewTriState.True;
            this.dgvAutoPrint.ColumnHeadersDefaultCellStyle = style29;
            this.dgvAutoPrint.ColumnHeadersHeight = 0x20;
            this.dgvAutoPrint.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvAutoPrint.Columns.AddRange(new DataGridViewColumn[] { this.cAPOutCode, this.cAPMode, this.cAPCount, this.cAPPrinter, this.cAPBinName, this.cAPBinNo, this.cAPBtn });
            this.dgvAutoPrint.DataSource = this.bdsAutoPrint;
            this.dgvAutoPrint.Dock = DockStyle.Fill;
            this.dgvAutoPrint.Location = new Point(3, 40);
            this.dgvAutoPrint.MultiSelect = false;
            this.dgvAutoPrint.Name = "dgvAutoPrint";
            style30.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style30.BackColor = SystemColors.Control;
            style30.ForeColor = SystemColors.WindowText;
            style30.SelectionBackColor = SystemColors.Highlight;
            style30.SelectionForeColor = SystemColors.HighlightText;
            style30.WrapMode = DataGridViewTriState.True;
            this.dgvAutoPrint.RowHeadersDefaultCellStyle = style30;
            this.dgvAutoPrint.RowHeadersWidth = 30;
            this.dgvAutoPrint.RowTemplate.Height = 0x15;
            this.dgvAutoPrint.Size = new Size(0x252, 370);
            this.dgvAutoPrint.TabIndex = 1;
            this.dgvAutoPrint.Tag = "Auto print";
            this.dgvAutoPrint.CellContentClick += new DataGridViewCellEventHandler(this.dgv_CellContentClick);
            this.dgvAutoPrint.CellEndEdit += new DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            this.dgvAutoPrint.CellValidating += new DataGridViewCellValidatingEventHandler(this.dgv_CellValidating);
            this.dgvAutoPrint.DataError += new DataGridViewDataErrorEventHandler(this.dgv_DataError);
            this.dgvAutoPrint.RowsAdded += new DataGridViewRowsAddedEventHandler(this.dgv_RowsAdded);
            this.dgvAutoPrint.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.dgv_RowsRemoved);
            this.dgvAutoPrint.RowValidating += new DataGridViewCellCancelEventHandler(this.dgv_RowValidating);
            this.dgvAutoPrint.SelectionChanged += new EventHandler(this.dgv_SelectionChanged);
            this.cAPOutCode.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.cAPOutCode.DataPropertyName = "clmAPOutCode";
            this.cAPOutCode.HeaderText = "M\x00e3 th\x00f4ng điệp đầu ra";
            this.cAPOutCode.MaxInputLength = 6;
            this.cAPOutCode.Name = "cAPOutCode";
            this.cAPOutCode.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.cAPOutCode.Width = 120;
            this.cAPMode.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.cAPMode.DataPropertyName = "clmAPMode";
            this.cAPMode.HeaderText = "Tự động in";
            this.cAPMode.Name = "cAPMode";
            this.cAPMode.Width = 0x41;
            this.cAPCount.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.cAPCount.DataPropertyName = "clmAPCount";
            style31.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.cAPCount.DefaultCellStyle = style31;
            this.cAPCount.HeaderText = "Số bản in";
            this.cAPCount.MaxInputLength = 2;
            this.cAPCount.Name = "cAPCount";
            this.cAPCount.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.cAPCount.Width = 60;
            this.cAPPrinter.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.cAPPrinter.DataPropertyName = "clmAPPrinter";
            style32.BackColor = SystemColors.Control;
            this.cAPPrinter.DefaultCellStyle = style32;
            this.cAPPrinter.HeaderText = "M\x00e1y in";
            this.cAPPrinter.Name = "cAPPrinter";
            this.cAPPrinter.ReadOnly = true;
            this.cAPPrinter.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.cAPPrinter.Width = 0xa8;
            this.cAPBinName.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.cAPBinName.DataPropertyName = "clmAPBinName";
            style33.BackColor = SystemColors.Control;
            this.cAPBinName.DefaultCellStyle = style33;
            this.cAPBinName.HeaderText = "Khay giấy";
            this.cAPBinName.Name = "cAPBinName";
            this.cAPBinName.ReadOnly = true;
            this.cAPBinName.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.cAPBinName.Width = 80;
            this.cAPBinNo.DataPropertyName = "clmAPBinNo";
            this.cAPBinNo.HeaderText = "clmAPBinNo";
            this.cAPBinNo.Name = "cAPBinNo";
            this.cAPBinNo.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.cAPBinNo.Visible = false;
            this.cAPBinNo.Width = 40;
            this.cAPBtn.DataPropertyName = "clmAPBtn";
            this.cAPBtn.HeaderText = "";
            this.cAPBtn.Name = "cAPBtn";
            this.cAPBtn.Resizable = DataGridViewTriState.True;
            this.cAPBtn.Width = 70;
            this.bdnAutoPrint.AddNewItem = this.bdnAPAddNewItem;
            this.bdnAutoPrint.BindingSource = this.bdsAutoPrint;
            this.bdnAutoPrint.CountItem = this.bdnAPCountItem;
            this.bdnAutoPrint.DeleteItem = this.bdnAPDeleteItem;
            this.bdnAutoPrint.Items.AddRange(new ToolStripItem[] { this.bdnAPMoveFirstItem, this.bdnAPMovePreviousItem, this.bdnAPSeparator, this.bdnAPPositionItem, this.bdnAPCountItem, this.bdnAPSeparator1, this.bdnAPMoveNextItem, this.bdnAPMoveLastItem, this.bdnAPSeparator2, this.bdnAPAddNewItem, this.bdnAPDeleteItem });
            this.bdnAutoPrint.Location = new Point(3, 15);
            this.bdnAutoPrint.MoveFirstItem = this.bdnAPMoveFirstItem;
            this.bdnAutoPrint.MoveLastItem = this.bdnAPMoveLastItem;
            this.bdnAutoPrint.MoveNextItem = this.bdnAPMoveNextItem;
            this.bdnAutoPrint.MovePreviousItem = this.bdnAPMovePreviousItem;
            this.bdnAutoPrint.Name = "bdnAutoPrint";
            this.bdnAutoPrint.PositionItem = this.bdnAPPositionItem;
            this.bdnAutoPrint.Size = new Size(0x252, 0x19);
            this.bdnAutoPrint.TabIndex = 0;
            this.bdnAutoPrint.Text = "bdnAutoPrint";
            this.bdnAPAddNewItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnAPAddNewItem.Image = (Image) manager.GetObject("bdnAPAddNewItem.Image");
            this.bdnAPAddNewItem.Name = "bdnAPAddNewItem";
            this.bdnAPAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bdnAPAddNewItem.Size = new Size(0x17, 0x16);
            this.bdnAPAddNewItem.Tag = "Add";
            this.bdnAPAddNewItem.Text = "Th\x00eam";
            this.bdnAPAddNewItem.ToolTipText = "Add";
            this.bdnAPAddNewItem.Click += new EventHandler(this.bdnAddNewItem_Click);
            this.bdnAPCountItem.Name = "bdnAPCountItem";
            this.bdnAPCountItem.Size = new Size(0x26, 0x16);
            this.bdnAPCountItem.Text = "/ {0}";
            this.bdnAPCountItem.ToolTipText = "Total items";
            this.bdnAPDeleteItem.CheckOnClick = true;
            this.bdnAPDeleteItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnAPDeleteItem.Image = (Image) manager.GetObject("bdnAPDeleteItem.Image");
            this.bdnAPDeleteItem.Name = "bdnAPDeleteItem";
            this.bdnAPDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bdnAPDeleteItem.Size = new Size(0x17, 0x16);
            this.bdnAPDeleteItem.Tag = "Del";
            this.bdnAPDeleteItem.Text = "X\x00f3a";
            this.bdnAPDeleteItem.ToolTipText = "Delete";
            this.bdnAPDeleteItem.Click += new EventHandler(this.bdnDeleteItem_Click);
            this.bdnAPMoveFirstItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnAPMoveFirstItem.Image = (Image) manager.GetObject("bdnAPMoveFirstItem.Image");
            this.bdnAPMoveFirstItem.Name = "bdnAPMoveFirstItem";
            this.bdnAPMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bdnAPMoveFirstItem.Size = new Size(0x17, 0x16);
            this.bdnAPMoveFirstItem.Text = "Hạng mục đầu ti\x00ean";
            this.bdnAPMoveFirstItem.ToolTipText = "First item";
            this.bdnAPMovePreviousItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnAPMovePreviousItem.Image = (Image) manager.GetObject("bdnAPMovePreviousItem.Image");
            this.bdnAPMovePreviousItem.Name = "bdnAPMovePreviousItem";
            this.bdnAPMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bdnAPMovePreviousItem.Size = new Size(0x17, 0x16);
            this.bdnAPMovePreviousItem.Text = "Hạng mục trước";
            this.bdnAPMovePreviousItem.ToolTipText = "Previous item";
            this.bdnAPSeparator.Name = "bdnAPSeparator";
            this.bdnAPSeparator.Size = new Size(6, 0x19);
            this.bdnAPPositionItem.AccessibleName = "位置";
            this.bdnAPPositionItem.AutoSize = false;
            this.bdnAPPositionItem.Name = "bdnAPPositionItem";
            this.bdnAPPositionItem.Size = new Size(50, 0x13);
            this.bdnAPPositionItem.Text = "0";
            this.bdnAPPositionItem.ToolTipText = "Present location";
            this.bdnAPSeparator1.Name = "bdnAPSeparator1";
            this.bdnAPSeparator1.Size = new Size(6, 0x19);
            this.bdnAPMoveNextItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnAPMoveNextItem.Image = (Image) manager.GetObject("bdnAPMoveNextItem.Image");
            this.bdnAPMoveNextItem.Name = "bdnAPMoveNextItem";
            this.bdnAPMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bdnAPMoveNextItem.Size = new Size(0x17, 0x16);
            this.bdnAPMoveNextItem.Text = "Hạng mục tiếp theo";
            this.bdnAPMoveNextItem.ToolTipText = "Next item";
            this.bdnAPMoveLastItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.bdnAPMoveLastItem.Image = (Image) manager.GetObject("bdnAPMoveLastItem.Image");
            this.bdnAPMoveLastItem.Name = "bdnAPMoveLastItem";
            this.bdnAPMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bdnAPMoveLastItem.Size = new Size(0x17, 0x16);
            this.bdnAPMoveLastItem.Text = "hạng mục cuối c\x00f9ng";
            this.bdnAPMoveLastItem.ToolTipText = "Last item";
            this.bdnAPSeparator2.Name = "bdnAPSeparator2";
            this.bdnAPSeparator2.Size = new Size(6, 0x19);
            this.tbpPrinter.AutoScroll = true;
            this.tbpPrinter.Controls.Add(this.chbSizeWarning);
            this.tbpPrinter.Controls.Add(this.gpbDefaultPrinter);
            this.tbpPrinter.Controls.Add(this.gpbEntryPrinter);
            this.tbpPrinter.Location = new Point(4, 40);
            this.tbpPrinter.Name = "tbpPrinter";
            this.tbpPrinter.Size = new Size(0x389, 0x1d4);
            this.tbpPrinter.TabIndex = 3;
            this.tbpPrinter.Text = "M\x00e1y in";
            this.tbpPrinter.UseVisualStyleBackColor = true;
            this.chbSizeWarning.AutoSize = true;
            this.chbSizeWarning.Location = new Point(15, 0x12d);
            this.chbSizeWarning.Name = "chbSizeWarning";
            this.chbSizeWarning.Size = new Size(0xe7, 0x10);
            this.chbSizeWarning.TabIndex = 3;
            this.chbSizeWarning.Text = "K\x00edch thước dữ liệu in b\x00e1o động (hơn 1MB)";
            this.chbSizeWarning.UseVisualStyleBackColor = true;
            this.gpbDefaultPrinter.Controls.Add(this.btnDefaultPrinter);
            this.gpbDefaultPrinter.Controls.Add(this.txbDefaultBin);
            this.gpbDefaultPrinter.Controls.Add(this.lblDefaultBin);
            this.gpbDefaultPrinter.Controls.Add(this.txbDefaultPrinter);
            this.gpbDefaultPrinter.Controls.Add(this.lblDefaultPrinter);
            this.gpbDefaultPrinter.Location = new Point(15, 0xcb);
            this.gpbDefaultPrinter.Name = "gpbDefaultPrinter";
            this.gpbDefaultPrinter.Size = new Size(330, 0x4b);
            this.gpbDefaultPrinter.TabIndex = 1;
            this.gpbDefaultPrinter.TabStop = false;
            this.gpbDefaultPrinter.Text = "M\x00e1y in ngầm định";
            this.btnDefaultPrinter.Location = new Point(0x109, 0x2e);
            this.btnDefaultPrinter.Name = "btnDefaultPrinter";
            this.btnDefaultPrinter.Size = new Size(60, 0x17);
            this.btnDefaultPrinter.TabIndex = 4;
            this.btnDefaultPrinter.Text = "Thay đổi";
            this.btnDefaultPrinter.UseVisualStyleBackColor = true;
            this.btnDefaultPrinter.Click += new EventHandler(this.btnDefaultPrinter_Click);
            this.txbDefaultBin.Location = new Point(0x48, 0x30);
            this.txbDefaultBin.Name = "txbDefaultBin";
            this.txbDefaultBin.ReadOnly = true;
            this.txbDefaultBin.Size = new Size(0xb3, 0x13);
            this.txbDefaultBin.TabIndex = 3;
            this.txbDefaultBin.TabStop = false;
            this.lblDefaultBin.AutoSize = true;
            this.lblDefaultBin.Location = new Point(7, 0x33);
            this.lblDefaultBin.Name = "lblDefaultBin";
            this.lblDefaultBin.Size = new Size(0x36, 12);
            this.lblDefaultBin.TabIndex = 2;
            this.lblDefaultBin.Text = "Khay giấy";
            this.txbDefaultPrinter.Location = new Point(0x48, 20);
            this.txbDefaultPrinter.Name = "txbDefaultPrinter";
            this.txbDefaultPrinter.ReadOnly = true;
            this.txbDefaultPrinter.Size = new Size(0xb3, 0x13);
            this.txbDefaultPrinter.TabIndex = 1;
            this.txbDefaultPrinter.TabStop = false;
            this.lblDefaultPrinter.AutoSize = true;
            this.lblDefaultPrinter.Location = new Point(7, 0x17);
            this.lblDefaultPrinter.Name = "lblDefaultPrinter";
            this.lblDefaultPrinter.Size = new Size(0x3e, 12);
            this.lblDefaultPrinter.TabIndex = 0;
            this.lblDefaultPrinter.Text = "T\x00ean m\x00e1y in";
            this.gpbEntryPrinter.Controls.Add(this.lsvPrt);
            this.gpbEntryPrinter.Controls.Add(this.lsvBin);
            this.gpbEntryPrinter.Controls.Add(this.ckbDoublePrt);
            this.gpbEntryPrinter.Controls.Add(this.btnPrinterSet);
            this.gpbEntryPrinter.Controls.Add(this.btnEntryPrtDel);
            this.gpbEntryPrinter.Controls.Add(this.btnEntryPrtAdd);
            this.gpbEntryPrinter.Controls.Add(this.gpbMargin);
            this.gpbEntryPrinter.Controls.Add(this.lblBin);
            this.gpbEntryPrinter.Controls.Add(this.lblPrinterName);
            this.gpbEntryPrinter.Location = new Point(15, 15);
            this.gpbEntryPrinter.Name = "gpbEntryPrinter";
            this.gpbEntryPrinter.Size = new Size(0x265, 0xb6);
            this.gpbEntryPrinter.TabIndex = 0;
            this.gpbEntryPrinter.TabStop = false;
            this.gpbEntryPrinter.Text = "M\x00e1y in";
            this.lsvPrt.Columns.AddRange(new ColumnHeader[] { this.columnHeader2 });
            this.lsvPrt.HeaderStyle = ColumnHeaderStyle.None;
            this.lsvPrt.Location = new Point(6, 0x2a);
            this.lsvPrt.MultiSelect = false;
            this.lsvPrt.Name = "lsvPrt";
            this.lsvPrt.Size = new Size(0x9d, 100);
            this.lsvPrt.TabIndex = 1;
            this.lsvPrt.UseCompatibleStateImageBehavior = false;
            this.lsvPrt.View = View.Details;
            this.lsvPrt.SelectedIndexChanged += new EventHandler(this.lsvPrt_SelectedIndexChanged);
            this.lsvBin.Columns.AddRange(new ColumnHeader[] { this.columnHeader1 });
            this.lsvBin.HeaderStyle = ColumnHeaderStyle.None;
            this.lsvBin.Location = new Point(0xac, 0x2a);
            this.lsvBin.MultiSelect = false;
            this.lsvBin.Name = "lsvBin";
            this.lsvBin.Size = new Size(0x91, 100);
            this.lsvBin.TabIndex = 3;
            this.lsvBin.UseCompatibleStateImageBehavior = false;
            this.lsvBin.View = View.Details;
            this.lsvBin.SelectedIndexChanged += new EventHandler(this.lsvBin_SelectedIndexChanged);
            this.columnHeader1.Text = "";
            this.ckbDoublePrt.AutoSize = true;
            this.ckbDoublePrt.Location = new Point(0x152, 0x5d);
            this.ckbDoublePrt.Name = "ckbDoublePrt";
            this.ckbDoublePrt.Size = new Size(0x41, 0x10);
            this.ckbDoublePrt.TabIndex = 7;
            this.ckbDoublePrt.Text = "In 2 mặt";
            this.ckbDoublePrt.UseVisualStyleBackColor = true;
            this.ckbDoublePrt.CheckedChanged += new EventHandler(this.ckbDoublePrt_CheckedChanged);
            this.btnPrinterSet.Location = new Point(0x1ab, 0x59);
            this.btnPrinterSet.Name = "btnPrinterSet";
            this.btnPrinterSet.Size = new Size(0x9b, 0x17);
            this.btnPrinterSet.TabIndex = 8;
            this.btnPrinterSet.Text = "ドットインパクトプリンタ設定";
            this.btnPrinterSet.UseVisualStyleBackColor = true;
            this.btnPrinterSet.Visible = false;
            this.btnPrinterSet.Click += new EventHandler(this.btnPrinterSet_Click);
            this.btnEntryPrtDel.Location = new Point(0x3e, 0x94);
            this.btnEntryPrtDel.Name = "btnEntryPrtDel";
            this.btnEntryPrtDel.Size = new Size(0x2e, 0x17);
            this.btnEntryPrtDel.TabIndex = 5;
            this.btnEntryPrtDel.Text = "X\x00f3a";
            this.btnEntryPrtDel.UseVisualStyleBackColor = true;
            this.btnEntryPrtDel.Click += new EventHandler(this.btnEntryPrtDel_Click);
            this.btnEntryPrtAdd.Location = new Point(9, 0x94);
            this.btnEntryPrtAdd.Name = "btnEntryPrtAdd";
            this.btnEntryPrtAdd.Size = new Size(0x2e, 0x17);
            this.btnEntryPrtAdd.TabIndex = 4;
            this.btnEntryPrtAdd.Text = "Th\x00eam";
            this.btnEntryPrtAdd.UseVisualStyleBackColor = true;
            this.btnEntryPrtAdd.Click += new EventHandler(this.btnEntryPrtAdd_Click);
            this.gpbMargin.Controls.Add(this.nudMarginL);
            this.gpbMargin.Controls.Add(this.nudMarginT);
            this.gpbMargin.Controls.Add(this.lblMarginL);
            this.gpbMargin.Controls.Add(this.lblMarginT);
            this.gpbMargin.Location = new Point(0x152, 0x1b);
            this.gpbMargin.Name = "gpbMargin";
            this.gpbMargin.Size = new Size(0xb8, 0x2f);
            this.gpbMargin.TabIndex = 6;
            this.gpbMargin.TabStop = false;
            this.gpbMargin.Text = "Lề giấy (1/10mm)";
            this.nudMarginL.Location = new Point(130, 0x12);
            bits = new int[4];
            bits[0] = 100;
            bits[3] = -2147483648;
            this.nudMarginL.Minimum = new decimal(bits);
            this.nudMarginL.Name = "nudMarginL";
            this.nudMarginL.Size = new Size(0x34, 0x13);
            this.nudMarginL.TabIndex = 3;
            this.nudMarginL.Tag = "";
            this.nudMarginL.Validating += new CancelEventHandler(this.nud_Validating);
            this.nudMarginT.Location = new Point(0x23, 0x12);
            bits = new int[4];
            bits[0] = 100;
            bits[3] = -2147483648;
            this.nudMarginT.Minimum = new decimal(bits);
            this.nudMarginT.Name = "nudMarginT";
            this.nudMarginT.Size = new Size(0x34, 0x13);
            this.nudMarginT.TabIndex = 1;
            this.nudMarginT.Tag = "";
            this.nudMarginT.Validating += new CancelEventHandler(this.nud_Validating);
            this.lblMarginL.AutoSize = true;
            this.lblMarginL.Location = new Point(0x62, 20);
            this.lblMarginL.Name = "lblMarginL";
            this.lblMarginL.Size = new Size(30, 12);
            this.lblMarginL.TabIndex = 2;
            this.lblMarginL.Text = "Rộng";
            this.lblMarginT.AutoSize = true;
            this.lblMarginT.Location = new Point(12, 20);
            this.lblMarginT.Name = "lblMarginT";
            this.lblMarginT.Size = new Size(0x16, 12);
            this.lblMarginT.TabIndex = 0;
            this.lblMarginT.Text = "D\x00e0i";
            this.lblBin.AutoSize = true;
            this.lblBin.Location = new Point(170, 0x1b);
            this.lblBin.Name = "lblBin";
            this.lblBin.Size = new Size(0x36, 12);
            this.lblBin.TabIndex = 2;
            this.lblBin.Text = "Khay giấy";
            this.lblPrinterName.AutoSize = true;
            this.lblPrinterName.Location = new Point(7, 0x1b);
            this.lblPrinterName.Name = "lblPrinterName";
            this.lblPrinterName.Size = new Size(0x3e, 12);
            this.lblPrinterName.TabIndex = 0;
            this.lblPrinterName.Text = "T\x00ean m\x00e1y in";
            this.tbpPassword.Controls.Add(this.tbcPassword);
            this.tbpPassword.Location = new Point(4, 40);
            this.tbpPassword.Name = "tbpPassword";
            this.tbpPassword.Size = new Size(0x389, 0x1d4);
            this.tbpPassword.TabIndex = 8;
            this.tbpPassword.Text = "Mật khẩu";
            this.tbpPassword.UseVisualStyleBackColor = true;
            this.tbcPassword.Controls.Add(this.tbpPwdChk);
            this.tbcPassword.Controls.Add(this.tbpPwdChng);
//            this.tbcPassword.ImeMode = ImeMode.Disable;
            this.tbcPassword.Location = new Point(8, 8);
            this.tbcPassword.Name = "tbcPassword";
            this.tbcPassword.SelectedIndex = 0;
            this.tbcPassword.Size = new Size(0x108, 200);
            this.tbcPassword.TabIndex = 0;
            this.tbpPwdChk.Controls.Add(this.gpbPwdCert);
            this.tbpPwdChk.Location = new Point(4, 0x16);
            this.tbpPwdChk.Name = "tbpPwdChk";
            this.tbpPwdChk.Padding = new Padding(3);
            this.tbpPwdChk.Size = new Size(0x100, 0xae);
            this.tbpPwdChk.TabIndex = 0;
            this.tbpPwdChk.Text = "X\x00e1c thực";
            this.tbpPwdChk.UseVisualStyleBackColor = true;
            this.gpbPwdCert.Controls.Add(this.btnAcptCncl);
            this.gpbPwdCert.Controls.Add(this.btnAcptOK);
            this.gpbPwdCert.Controls.Add(this.txbPassword);
            this.gpbPwdCert.Controls.Add(this.lblPassword);
            this.gpbPwdCert.Location = new Point(0x10, 20);
            this.gpbPwdCert.Name = "gpbPwdCert";
            this.gpbPwdCert.Size = new Size(0xaf, 0x5d);
            this.gpbPwdCert.TabIndex = 1;
            this.gpbPwdCert.TabStop = false;
            this.gpbPwdCert.Text = "Mật khẩu x\x00e1c thực";
            this.btnAcptCncl.Location = new Point(0x51, 0x39);
            this.btnAcptCncl.Name = "btnAcptCncl";
            this.btnAcptCncl.Size = new Size(0x47, 0x17);
            this.btnAcptCncl.TabIndex = 3;
            this.btnAcptCncl.Text = "Cancel";
            this.btnAcptCncl.UseVisualStyleBackColor = true;
            this.btnAcptCncl.Click += new EventHandler(this.btnAcptCncl_Click);
            this.btnAcptOK.Location = new Point(0x17, 0x39);
            this.btnAcptOK.Name = "btnAcptOK";
            this.btnAcptOK.Size = new Size(0x2e, 0x17);
            this.btnAcptOK.TabIndex = 2;
            this.btnAcptOK.Text = "OK";
            this.btnAcptOK.UseVisualStyleBackColor = true;
            this.btnAcptOK.Click += new EventHandler(this.btnAcptOK_Click);
            this.txbPassword.CharacterCasing = CharacterCasing.Upper;
            this.txbPassword.Location = new Point(0x51, 0x18);
            this.txbPassword.MaxLength = 4;
            this.txbPassword.Name = "txbPassword";
            this.txbPassword.PasswordChar = '*';
            this.txbPassword.Size = new Size(0x2b, 0x13);
            this.txbPassword.TabIndex = 1;
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new Point(14, 0x1b);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new Size(50, 12);
            this.lblPassword.TabIndex = 0;
            this.lblPassword.Text = "Mật khẩu";
            this.tbpPwdChng.Controls.Add(this.gpbPwdKind);
            this.tbpPwdChng.Controls.Add(this.lblNewPwdChk);
            this.tbpPwdChng.Controls.Add(this.txbNewPwdRe);
            this.tbpPwdChng.Controls.Add(this.lblNewPwdRe);
            this.tbpPwdChng.Controls.Add(this.txbNewPwd);
            this.tbpPwdChng.Controls.Add(this.lblNewPwd);
            this.tbpPwdChng.Controls.Add(this.btnPwdChngCncl);
            this.tbpPwdChng.Controls.Add(this.btnPwdChngOK);
            this.tbpPwdChng.Controls.Add(this.txbOldPwd);
            this.tbpPwdChng.Controls.Add(this.lblOldPwd);
            this.tbpPwdChng.Location = new Point(4, 0x16);
            this.tbpPwdChng.Name = "tbpPwdChng";
            this.tbpPwdChng.Padding = new Padding(3);
            this.tbpPwdChng.Size = new Size(0x100, 0xae);
            this.tbpPwdChng.TabIndex = 1;
            this.tbpPwdChng.Text = "Thay đổi mật khẩu";
            this.tbpPwdChng.UseVisualStyleBackColor = true;
            this.gpbPwdKind.Controls.Add(this.rdbPwdCncl);
            this.gpbPwdKind.Controls.Add(this.rdbPwdSet);
            this.gpbPwdKind.Controls.Add(this.rdbPwdChng);
            this.gpbPwdKind.Location = new Point(8, 8);
            this.gpbPwdKind.Name = "gpbPwdKind";
            this.gpbPwdKind.Size = new Size(0xf2, 0x21);
            this.gpbPwdKind.TabIndex = 0;
            this.gpbPwdKind.TabStop = false;
            this.rdbPwdCncl.AutoSize = true;
            this.rdbPwdCncl.Location = new Point(0xa5, 11);
            this.rdbPwdCncl.Name = "rdbPwdCncl";
            this.rdbPwdCncl.Size = new Size(0x2a, 0x10);
            this.rdbPwdCncl.TabIndex = 2;
            this.rdbPwdCncl.TabStop = true;
            this.rdbPwdCncl.Text = "X\x00f3a";
            this.rdbPwdCncl.UseVisualStyleBackColor = true;
            this.rdbPwdCncl.CheckedChanged += new EventHandler(this.rdbPwdCncl_CheckedChanged);
            this.rdbPwdSet.AutoSize = true;
            this.rdbPwdSet.Location = new Point(9, 11);
            this.rdbPwdSet.Name = "rdbPwdSet";
            this.rdbPwdSet.Size = new Size(0x44, 0x10);
            this.rdbPwdSet.TabIndex = 0;
            this.rdbPwdSet.TabStop = true;
            this.rdbPwdSet.Text = "Thiếp lập";
            this.rdbPwdSet.UseVisualStyleBackColor = true;
            this.rdbPwdSet.CheckedChanged += new EventHandler(this.rdbPwdSet_CheckedChanged);
            this.rdbPwdChng.AutoSize = true;
            this.rdbPwdChng.Location = new Point(0x58, 11);
            this.rdbPwdChng.Name = "rdbPwdChng";
            this.rdbPwdChng.Size = new Size(0x42, 0x10);
            this.rdbPwdChng.TabIndex = 1;
            this.rdbPwdChng.TabStop = true;
            this.rdbPwdChng.Text = "Thay đổi";
            this.rdbPwdChng.UseVisualStyleBackColor = true;
            this.rdbPwdChng.CheckedChanged += new EventHandler(this.rdbPwdChng_CheckedChanged);
            this.lblNewPwdChk.Location = new Point(0x91, 0x6d);
            this.lblNewPwdChk.Name = "lblNewPwdChk";
            this.lblNewPwdChk.Size = new Size(0x4a, 12);
            this.lblNewPwdChk.TabIndex = 7;
            this.lblNewPwdChk.Text = "(X\x00e1c nhận lại)";
            this.txbNewPwdRe.CharacterCasing = CharacterCasing.Upper;
//            this.txbNewPwdRe.ImeMode = ImeMode.Disable;
            this.txbNewPwdRe.Location = new Point(0x60, 0x6a);
            this.txbNewPwdRe.MaxLength = 4;
            this.txbNewPwdRe.Name = "txbNewPwdRe";
            this.txbNewPwdRe.PasswordChar = '*';
            this.txbNewPwdRe.Size = new Size(0x2b, 0x13);
            this.txbNewPwdRe.TabIndex = 6;
            this.lblNewPwdRe.Location = new Point(0x10, 0x6d);
            this.lblNewPwdRe.Name = "lblNewPwdRe";
            this.lblNewPwdRe.Size = new Size(0x47, 12);
            this.lblNewPwdRe.TabIndex = 5;
            this.lblNewPwdRe.Text = "Mật khẩu mới";
            this.txbNewPwd.CharacterCasing = CharacterCasing.Upper;
//            this.txbNewPwd.ImeMode = ImeMode.Disable;
            this.txbNewPwd.Location = new Point(0x60, 0x4e);
            this.txbNewPwd.MaxLength = 4;
            this.txbNewPwd.Name = "txbNewPwd";
            this.txbNewPwd.PasswordChar = '*';
            this.txbNewPwd.Size = new Size(0x2b, 0x13);
            this.txbNewPwd.TabIndex = 4;
            this.lblNewPwd.Location = new Point(0x10, 0x51);
            this.lblNewPwd.Name = "lblNewPwd";
            this.lblNewPwd.Size = new Size(0x47, 12);
            this.lblNewPwd.TabIndex = 3;
            this.lblNewPwd.Text = "Mật khẩu mới";
            this.btnPwdChngCncl.Location = new Point(0x60, 140);
            this.btnPwdChngCncl.Name = "btnPwdChngCncl";
            this.btnPwdChngCncl.Size = new Size(0x47, 0x17);
            this.btnPwdChngCncl.TabIndex = 9;
            this.btnPwdChngCncl.Text = "Cancel";
            this.btnPwdChngCncl.UseVisualStyleBackColor = true;
            this.btnPwdChngCncl.Click += new EventHandler(this.btnPwdChngCncl_Click);
            this.btnPwdChngOK.Location = new Point(0x29, 140);
            this.btnPwdChngOK.Name = "btnPwdChngOK";
            this.btnPwdChngOK.Size = new Size(0x27, 0x17);
            this.btnPwdChngOK.TabIndex = 8;
            this.btnPwdChngOK.Text = "OK";
            this.btnPwdChngOK.UseVisualStyleBackColor = true;
            this.btnPwdChngOK.Click += new EventHandler(this.btnPwdChngOK_Click);
            this.txbOldPwd.CharacterCasing = CharacterCasing.Upper;
//            this.txbOldPwd.ImeMode = ImeMode.Disable;
            this.txbOldPwd.Location = new Point(0x60, 0x30);
            this.txbOldPwd.MaxLength = 4;
            this.txbOldPwd.Name = "txbOldPwd";
            this.txbOldPwd.PasswordChar = '*';
            this.txbOldPwd.Size = new Size(0x2b, 0x13);
            this.txbOldPwd.TabIndex = 2;
            this.lblOldPwd.Location = new Point(0x10, 0x33);
            this.lblOldPwd.Name = "lblOldPwd";
            this.lblOldPwd.Size = new Size(0x47, 12);
            this.lblOldPwd.TabIndex = 1;
            this.lblOldPwd.Text = "Mật khẩu cũ";
            this.tbcRoot.Controls.Add(this.tbpTerm);
            this.tbcRoot.Controls.Add(this.tbpServer);
            this.tbcRoot.Controls.Add(this.tbpPrinter);
            this.tbcRoot.Controls.Add(this.tbpAutoPrint);
            this.tbcRoot.Controls.Add(this.tbpMsgCls);
            this.tbcRoot.Controls.Add(this.tbpFileSaveK);
            this.tbcRoot.Controls.Add(this.tbpFileSaveC);
            this.tbcRoot.Controls.Add(this.tbpReceiveInform);
            this.tbcRoot.Controls.Add(this.tbpUserKey);
            this.tbcRoot.Controls.Add(this.tbpLog);
            this.tbcRoot.Controls.Add(this.tbpHelp);
            this.tbcRoot.Controls.Add(this.tbpPassword);
            this.tbcRoot.Location = new Point(0, 4);
            this.tbcRoot.Multiline = true;
            this.tbcRoot.Name = "tbcRoot";
            this.tbcRoot.SelectedIndex = 0;
            this.tbcRoot.Size = new Size(0x391, 0x200);
            this.tbcRoot.TabIndex = 0;
            this.tbcRoot.TabStop = false;
            this.tbpServer.AutoScroll = true;
            this.tbpServer.Controls.Add(this.pnlMail);
            this.tbpServer.Controls.Add(this.pnlServer);
            this.tbpServer.Location = new Point(4, 40);
            this.tbpServer.Name = "tbpServer";
            this.tbpServer.Padding = new Padding(3);
            this.tbpServer.Size = new Size(0x389, 0x1d4);
            this.tbpServer.TabIndex = 1;
            this.tbpServer.Text = "M\x00e1y chủ";
            this.tbpServer.UseVisualStyleBackColor = true;
            this.pnlMail.AutoSize = true;
            this.pnlMail.Controls.Add(this.btnGateway);
            this.pnlMail.Controls.Add(this.ckbUseGateway);
            this.pnlMail.Controls.Add(this.nudMaxRetreiveCnt);
            this.pnlMail.Controls.Add(this.gpbTraceM);
            this.pnlMail.Controls.Add(this.gpbProxyM);
            this.pnlMail.Controls.Add(this.lblKen);
            this.pnlMail.Controls.Add(this.lblMaxRetreiveCnt);
            this.pnlMail.Controls.Add(this.gpbAutoSndRcvTm);
            this.pnlMail.Controls.Add(this.cmbServerConnectM);
            this.pnlMail.Controls.Add(this.lblServerConnectM);
            this.pnlMail.Location = new Point(0x1c9, 0x11);
            this.pnlMail.Name = "pnlMail";
            this.pnlMail.Size = new Size(440, 0x1d1);
            this.pnlMail.TabIndex = 1;
            this.btnGateway.Location = new Point(0xea, 0x2a);
            this.btnGateway.Name = "btnGateway";
            this.btnGateway.Size = new Size(0x98, 0x17);
            this.btnGateway.TabIndex = 3;
            this.btnGateway.Text = "ゲートウェイサーバの設定";
            this.btnGateway.UseVisualStyleBackColor = true;
            this.btnGateway.Click += new EventHandler(this.btnGateway_Click);
            this.ckbUseGateway.AutoSize = true;
            this.ckbUseGateway.Location = new Point(0x1c, 0x2e);
            this.ckbUseGateway.Name = "ckbUseGateway";
            this.ckbUseGateway.Size = new Size(0x9f, 0x10);
            this.ckbUseGateway.TabIndex = 2;
            this.ckbUseGateway.Text = "ゲートウェイサーバを経由する";
            this.ckbUseGateway.UseVisualStyleBackColor = true;
            this.ckbUseGateway.CheckedChanged += new EventHandler(this.ckbServer_CheckedChanged);
            this.nudMaxRetreiveCnt.Location = new Point(0xab, 0x1aa);
            bits = new int[4];
            bits[0] = 0x270f;
            this.nudMaxRetreiveCnt.Maximum = new decimal(bits);
            bits = new int[4];
            bits[0] = 1;
            this.nudMaxRetreiveCnt.Minimum = new decimal(bits);
            this.nudMaxRetreiveCnt.Name = "nudMaxRetreiveCnt";
            this.nudMaxRetreiveCnt.Size = new Size(0x36, 0x13);
            this.nudMaxRetreiveCnt.TabIndex = 8;
            this.nudMaxRetreiveCnt.Tag = "";
            bits = new int[4];
            bits[0] = 0x270f;
            this.nudMaxRetreiveCnt.Value = new decimal(bits);
            this.nudMaxRetreiveCnt.Validating += new CancelEventHandler(this.nud_Validating);
            this.gpbTraceM.Controls.Add(this.lblTraceFileM);
            this.gpbTraceM.Controls.Add(this.txbTraceOutput2M);
            this.gpbTraceM.Controls.Add(this.lblTraceOutput2M);
            this.gpbTraceM.Controls.Add(this.txbTraceOutput1M);
            this.gpbTraceM.Controls.Add(this.lblTraceOutput1M);
            this.gpbTraceM.Controls.Add(this.ckbTraceOutputM);
            this.gpbTraceM.Location = new Point(11, 0x4e);
            this.gpbTraceM.Name = "gpbTraceM";
            this.gpbTraceM.Size = new Size(0x19b, 0x61);
            this.gpbTraceM.TabIndex = 4;
            this.gpbTraceM.TabStop = false;
            this.gpbTraceM.Text = "トレース";
            this.lblTraceFileM.AutoSize = true;
            this.lblTraceFileM.Location = new Point(0xa2, 0x13);
            this.lblTraceFileM.Name = "lblTraceFileM";
            this.lblTraceFileM.Size = new Size(0x4b, 12);
            this.lblTraceFileM.TabIndex = 1;
            this.lblTraceFileM.Text = "トレースファイル";
            this.txbTraceOutput2M.Location = new Point(0xa4, 0x41);
            this.txbTraceOutput2M.Name = "txbTraceOutput2M";
            this.txbTraceOutput2M.ReadOnly = true;
            this.txbTraceOutput2M.Size = new Size(0xd3, 0x13);
            this.txbTraceOutput2M.TabIndex = 5;
            this.txbTraceOutput2M.TabStop = false;
            this.lblTraceOutput2M.AutoSize = true;
            this.lblTraceOutput2M.Location = new Point(15, 0x44);
            this.lblTraceOutput2M.Name = "lblTraceOutput2M";
            this.lblTraceOutput2M.Size = new Size(0x8f, 12);
            this.lblTraceOutput2M.TabIndex = 4;
            this.lblTraceOutput2M.Text = "管理資料情報取出サーバ用";
            this.txbTraceOutput1M.Location = new Point(0xa4, 0x27);
            this.txbTraceOutput1M.Name = "txbTraceOutput1M";
            this.txbTraceOutput1M.ReadOnly = true;
            this.txbTraceOutput1M.Size = new Size(0xd3, 0x13);
            this.txbTraceOutput1M.TabIndex = 3;
            this.txbTraceOutput1M.TabStop = false;
            this.lblTraceOutput1M.AutoSize = true;
            this.lblTraceOutput1M.Location = new Point(15, 0x2a);
            this.lblTraceOutput1M.Name = "lblTraceOutput1M";
            this.lblTraceOutput1M.Size = new Size(0x4b, 12);
            this.lblTraceOutput1M.TabIndex = 2;
            this.lblTraceOutput1M.Text = "メールサーバ用";
            this.ckbTraceOutputM.AutoSize = true;
            this.ckbTraceOutputM.Location = new Point(0x10, 0x12);
            this.ckbTraceOutputM.Name = "ckbTraceOutputM";
            this.ckbTraceOutputM.Size = new Size(0x54, 0x10);
            this.ckbTraceOutputM.TabIndex = 0;
            this.ckbTraceOutputM.Text = "トレース出力";
            this.ckbTraceOutputM.UseVisualStyleBackColor = true;
            this.ckbTraceOutputM.CheckedChanged += new EventHandler(this.ckbServer_CheckedChanged);
            this.gpbProxyM.Controls.Add(this.txbProxyPasswordM);
            this.gpbProxyM.Controls.Add(this.lblProxyPasswordM);
            this.gpbProxyM.Controls.Add(this.txbProxyAccountM);
            this.gpbProxyM.Controls.Add(this.lblProxyAccountM);
            this.gpbProxyM.Controls.Add(this.ckbUseProxyAccountM);
            this.gpbProxyM.Controls.Add(this.txbProxyPortM);
            this.gpbProxyM.Controls.Add(this.lblProxyPortM);
            this.gpbProxyM.Controls.Add(this.txbProxyNameM);
            this.gpbProxyM.Controls.Add(this.lblProxyNameM);
            this.gpbProxyM.Controls.Add(this.ckbUseProxyM);
            this.gpbProxyM.Location = new Point(11, 0xb9);
            this.gpbProxyM.Name = "gpbProxyM";
            this.gpbProxyM.Size = new Size(0x19b, 0x88);
            this.gpbProxyM.TabIndex = 5;
            this.gpbProxyM.TabStop = false;
            this.gpbProxyM.Text = "プロキシサーバ（管理資料情報取出サーバ接続用）";
            //this.txbProxyPasswordM.ImeMode = ImeMode.Disable;
            this.txbProxyPasswordM.Location = new Point(0xff, 0x67);
            this.txbProxyPasswordM.MaxLength = 0x20;
            this.txbProxyPasswordM.Name = "txbProxyPasswordM";
            this.txbProxyPasswordM.Size = new Size(120, 0x13);
            this.txbProxyPasswordM.TabIndex = 9;
            this.txbProxyPasswordM.Tag = "Password";
            this.txbProxyPasswordM.Text = "*";
            this.txbProxyPasswordM.UseSystemPasswordChar = true;
            this.lblProxyPasswordM.AutoSize = true;
            this.lblProxyPasswordM.Location = new Point(0xc5, 0x6a);
            this.lblProxyPasswordM.Name = "lblProxyPasswordM";
            this.lblProxyPasswordM.Size = new Size(0x34, 12);
            this.lblProxyPasswordM.TabIndex = 8;
            this.lblProxyPasswordM.Text = "パスワード";
            //this.txbProxyAccountM.ImeMode = ImeMode.Disable;
            this.txbProxyAccountM.Location = new Point(0x45, 0x67);
            this.txbProxyAccountM.MaxLength = 0x40;
            this.txbProxyAccountM.Name = "txbProxyAccountM";
            this.txbProxyAccountM.Size = new Size(120, 0x13);
            this.txbProxyAccountM.TabIndex = 7;
            this.txbProxyAccountM.Tag = "Account";
            this.lblProxyAccountM.AutoSize = true;
            this.lblProxyAccountM.Location = new Point(14, 0x6a);
            this.lblProxyAccountM.Name = "lblProxyAccountM";
            this.lblProxyAccountM.Size = new Size(0x31, 12);
            this.lblProxyAccountM.TabIndex = 6;
            this.lblProxyAccountM.Text = "アカウント";
            this.ckbUseProxyAccountM.AutoSize = true;
            this.ckbUseProxyAccountM.Location = new Point(0x10, 0x51);
            this.ckbUseProxyAccountM.Name = "ckbUseProxyAccountM";
            this.ckbUseProxyAccountM.Size = new Size(0x90, 0x10);
            this.ckbUseProxyAccountM.TabIndex = 5;
            this.ckbUseProxyAccountM.Text = "プロキシサーバ認証を行う";
            this.ckbUseProxyAccountM.UseVisualStyleBackColor = true;
            this.ckbUseProxyAccountM.CheckedChanged += new EventHandler(this.ckbServer_CheckedChanged);
            //this.txbProxyPortM.ImeMode = ImeMode.Disable;
            this.txbProxyPortM.Location = new Point(0x14d, 0x2e);
            this.txbProxyPortM.MaxLength = 5;
            this.txbProxyPortM.Name = "txbProxyPortM";
            this.txbProxyPortM.Size = new Size(0x2a, 0x13);
            this.txbProxyPortM.TabIndex = 4;
            this.lblProxyPortM.AutoSize = true;
            this.lblProxyPortM.Location = new Point(0x126, 0x31);
            this.lblProxyPortM.Name = "lblProxyPortM";
            this.lblProxyPortM.Size = new Size(0x21, 12);
            this.lblProxyPortM.TabIndex = 3;
            this.lblProxyPortM.Text = "ポート";
            //this.txbProxyNameM.ImeMode = ImeMode.Disable;
            this.txbProxyNameM.Location = new Point(0x70, 0x2e);
            this.txbProxyNameM.MaxLength = 0x40;
            this.txbProxyNameM.Name = "txbProxyNameM";
            this.txbProxyNameM.Size = new Size(0xa4, 0x13);
            this.txbProxyNameM.TabIndex = 2;
            this.txbProxyNameM.Tag = "Proxy name";
            this.lblProxyNameM.AutoSize = true;
            this.lblProxyNameM.Location = new Point(14, 0x31);
            this.lblProxyNameM.Name = "lblProxyNameM";
            this.lblProxyNameM.Size = new Size(0x55, 12);
            this.lblProxyNameM.TabIndex = 1;
            this.lblProxyNameM.Text = "プロキシサーバ名";
            this.ckbUseProxyM.AutoSize = true;
            this.ckbUseProxyM.Location = new Point(0x10, 0x18);
            this.ckbUseProxyM.Name = "ckbUseProxyM";
            this.ckbUseProxyM.Size = new Size(0x90, 0x10);
            this.ckbUseProxyM.TabIndex = 0;
            this.ckbUseProxyM.Text = "プロキシサーバを使用する";
            this.ckbUseProxyM.UseVisualStyleBackColor = true;
            this.ckbUseProxyM.CheckedChanged += new EventHandler(this.ckbServer_CheckedChanged);
            this.lblKen.AutoSize = true;
            this.lblKen.Location = new Point(0xe7, 0x1ad);
            this.lblKen.Name = "lblKen";
            this.lblKen.Size = new Size(0x11, 12);
            this.lblKen.TabIndex = 9;
            this.lblKen.Text = "件";
            this.lblMaxRetreiveCnt.AutoSize = true;
            this.lblMaxRetreiveCnt.Location = new Point(0x18, 0x1ad);
            this.lblMaxRetreiveCnt.Name = "lblMaxRetreiveCnt";
            this.lblMaxRetreiveCnt.Size = new Size(0x8b, 12);
            this.lblMaxRetreiveCnt.TabIndex = 7;
            this.lblMaxRetreiveCnt.Text = "一度に受信する電文の件数";
            this.gpbAutoSndRcvTm.Controls.Add(this.nudAutoSndRcvTm);
            this.gpbAutoSndRcvTm.Controls.Add(this.ckbUseAutoSndRcvTm);
            this.gpbAutoSndRcvTm.Controls.Add(this.gpbAutoTmKind);
            this.gpbAutoSndRcvTm.Controls.Add(this.lblAutoSndRcvTm);
            this.gpbAutoSndRcvTm.Controls.Add(this.lblMinM);
            this.gpbAutoSndRcvTm.Location = new Point(11, 0x147);
            this.gpbAutoSndRcvTm.Name = "gpbAutoSndRcvTm";
            this.gpbAutoSndRcvTm.Size = new Size(0x19b, 90);
            this.gpbAutoSndRcvTm.TabIndex = 6;
            this.gpbAutoSndRcvTm.TabStop = false;
            this.gpbAutoSndRcvTm.Text = "自動送受信タイマ";
            this.nudAutoSndRcvTm.Location = new Point(0x3d, 0x38);
            bits = new int[4];
            bits[0] = 0x63;
            this.nudAutoSndRcvTm.Maximum = new decimal(bits);
            bits = new int[4];
            bits[0] = 3;
            this.nudAutoSndRcvTm.Minimum = new decimal(bits);
            this.nudAutoSndRcvTm.Name = "nudAutoSndRcvTm";
            this.nudAutoSndRcvTm.Size = new Size(0x2a, 0x13);
            this.nudAutoSndRcvTm.TabIndex = 2;
            this.nudAutoSndRcvTm.Tag = "";
            bits = new int[4];
            bits[0] = 3;
            this.nudAutoSndRcvTm.Value = new decimal(bits);
            this.nudAutoSndRcvTm.Validating += new CancelEventHandler(this.nud_Validating);
            this.ckbUseAutoSndRcvTm.AutoSize = true;
            this.ckbUseAutoSndRcvTm.Location = new Point(14, 0x1c);
            this.ckbUseAutoSndRcvTm.Name = "ckbUseAutoSndRcvTm";
            this.ckbUseAutoSndRcvTm.Size = new Size(0x75, 0x10);
            this.ckbUseAutoSndRcvTm.TabIndex = 0;
            this.ckbUseAutoSndRcvTm.Text = "自動タイマ起動する";
            this.ckbUseAutoSndRcvTm.UseVisualStyleBackColor = true;
            this.ckbUseAutoSndRcvTm.CheckedChanged += new EventHandler(this.ckbServer_CheckedChanged);
            this.gpbAutoTmKind.Controls.Add(this.rdbAutoTmKindSR);
            this.gpbAutoTmKind.Controls.Add(this.rdbAutoTmKindR);
            this.gpbAutoTmKind.Controls.Add(this.rdbAutoTmKindS);
            this.gpbAutoTmKind.Location = new Point(0x89, 0x2a);
            this.gpbAutoTmKind.Name = "gpbAutoTmKind";
            this.gpbAutoTmKind.Size = new Size(200, 0x2b);
            this.gpbAutoTmKind.TabIndex = 4;
            this.gpbAutoTmKind.TabStop = false;
            this.gpbAutoTmKind.Text = "自動タイマ起動種別";
            this.rdbAutoTmKindSR.AutoSize = true;
            this.rdbAutoTmKindSR.Checked = true;
            this.rdbAutoTmKindSR.Location = new Point(12, 0x10);
            this.rdbAutoTmKindSR.Name = "rdbAutoTmKindSR";
            this.rdbAutoTmKindSR.Size = new Size(0x3b, 0x10);
            this.rdbAutoTmKindSR.TabIndex = 0;
            this.rdbAutoTmKindSR.TabStop = true;
            this.rdbAutoTmKindSR.Text = "送受信";
            this.rdbAutoTmKindSR.UseVisualStyleBackColor = true;
            this.rdbAutoTmKindR.AutoSize = true;
            this.rdbAutoTmKindR.Location = new Point(0x90, 0x10);
            this.rdbAutoTmKindR.Name = "rdbAutoTmKindR";
            this.rdbAutoTmKindR.Size = new Size(0x2f, 0x10);
            this.rdbAutoTmKindR.TabIndex = 2;
            this.rdbAutoTmKindR.Text = "受信";
            this.rdbAutoTmKindR.UseVisualStyleBackColor = true;
            this.rdbAutoTmKindS.AutoSize = true;
            this.rdbAutoTmKindS.Location = new Point(0x54, 0x10);
            this.rdbAutoTmKindS.Name = "rdbAutoTmKindS";
            this.rdbAutoTmKindS.Size = new Size(0x2f, 0x10);
            this.rdbAutoTmKindS.TabIndex = 1;
            this.rdbAutoTmKindS.Text = "送信";
            this.rdbAutoTmKindS.UseVisualStyleBackColor = true;
            this.lblAutoSndRcvTm.AutoSize = true;
            this.lblAutoSndRcvTm.Location = new Point(14, 60);
            this.lblAutoSndRcvTm.Name = "lblAutoSndRcvTm";
            this.lblAutoSndRcvTm.Size = new Size(0x2b, 12);
            this.lblAutoSndRcvTm.TabIndex = 1;
            this.lblAutoSndRcvTm.Text = "タイマ値";
            this.lblMinM.AutoSize = true;
            this.lblMinM.Location = new Point(0x6b, 60);
            this.lblMinM.Name = "lblMinM";
            this.lblMinM.Size = new Size(0x11, 12);
            this.lblMinM.TabIndex = 3;
            this.lblMinM.Text = "分";
            this.cmbServerConnectM.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbServerConnectM.FormattingEnabled = true;
            this.cmbServerConnectM.Location = new Point(0x60, 12);
            this.cmbServerConnectM.Name = "cmbServerConnectM";
            this.cmbServerConnectM.Size = new Size(0x98, 20);
            this.cmbServerConnectM.TabIndex = 1;
            this.lblServerConnectM.AutoSize = true;
            this.lblServerConnectM.Location = new Point(9, 15);
            this.lblServerConnectM.Name = "lblServerConnectM";
            this.lblServerConnectM.Size = new Size(0x47, 12);
            this.lblServerConnectM.TabIndex = 0;
            this.lblServerConnectM.Text = "接続先サーバ";
            this.pnlServer.AutoSize = true;
            this.pnlServer.Controls.Add(this.gpbProxy);
            this.pnlServer.Controls.Add(this.gpbTrace);
            this.pnlServer.Controls.Add(this.gpbCertificate);
            this.pnlServer.Controls.Add(this.cmbServerConnect);
            this.pnlServer.Controls.Add(this.lblServerConnect);
            this.pnlServer.Controls.Add(this.gpbJobConnect);
            this.pnlServer.Location = new Point(8, 0x11);
            this.pnlServer.Name = "pnlServer";
            this.pnlServer.Size = new Size(0x1bb, 0x1d4);
            this.pnlServer.TabIndex = 0;
            this.gpbProxy.Controls.Add(this.txbProxyPassword);
            this.gpbProxy.Controls.Add(this.lblProxyPassword);
            this.gpbProxy.Controls.Add(this.txbProxyAccount);
            this.gpbProxy.Controls.Add(this.lblProxyAccount);
            this.gpbProxy.Controls.Add(this.ckbUseProxyAccount);
            this.gpbProxy.Controls.Add(this.txbProxyPort);
            this.gpbProxy.Controls.Add(this.lblProxyPort);
            this.gpbProxy.Controls.Add(this.txbProxyName);
            this.gpbProxy.Controls.Add(this.lblProxyName);
            this.gpbProxy.Controls.Add(this.ckbUseProxy);
            this.gpbProxy.Location = new Point(12, 0x24);
            this.gpbProxy.Name = "gpbProxy";
            this.gpbProxy.Size = new Size(0x19b, 0x88);
            this.gpbProxy.TabIndex = 3;
            this.gpbProxy.TabStop = false;
            this.gpbProxy.Text = " M\x00e1y chủ Proxy";
            //this.txbProxyPassword.ImeMode = ImeMode.Disable;
            this.txbProxyPassword.Location = new Point(0x11c, 0x67);
            this.txbProxyPassword.MaxLength = 0x20;
            this.txbProxyPassword.Name = "txbProxyPassword";
            this.txbProxyPassword.Size = new Size(120, 0x13);
            this.txbProxyPassword.TabIndex = 9;
            this.txbProxyPassword.Tag = "Password";
            this.txbProxyPassword.Text = "*";
            this.txbProxyPassword.UseSystemPasswordChar = true;
            this.lblProxyPassword.AutoSize = true;
            this.lblProxyPassword.Location = new Point(0xe7, 0x6a);
            this.lblProxyPassword.Name = "lblProxyPassword";
            this.lblProxyPassword.Size = new Size(50, 12);
            this.lblProxyPassword.TabIndex = 8;
            this.lblProxyPassword.Text = "Mật khẩu";
            //this.txbProxyAccount.ImeMode = ImeMode.Disable;
            this.txbProxyAccount.Location = new Point(0x69, 0x67);
            this.txbProxyAccount.MaxLength = 0x40;
            this.txbProxyAccount.Name = "txbProxyAccount";
            this.txbProxyAccount.Size = new Size(120, 0x13);
            this.txbProxyAccount.TabIndex = 7;
            this.txbProxyAccount.Tag = "Account";
            this.lblProxyAccount.AutoSize = true;
            this.lblProxyAccount.Location = new Point(14, 0x6a);
            this.lblProxyAccount.Name = "lblProxyAccount";
            this.lblProxyAccount.Size = new Size(0x5b, 12);
            this.lblProxyAccount.TabIndex = 6;
            this.lblProxyAccount.Text = "M\x00e3 người sử dụng";
            this.ckbUseProxyAccount.AutoSize = true;
            this.ckbUseProxyAccount.Location = new Point(0x10, 0x51);
            this.ckbUseProxyAccount.Name = "ckbUseProxyAccount";
            this.ckbUseProxyAccount.Size = new Size(0x93, 0x10);
            this.ckbUseProxyAccount.TabIndex = 5;
            this.ckbUseProxyAccount.Text = "X\x00e1c thực m\x00e1y chủ Proxy";
            this.ckbUseProxyAccount.UseVisualStyleBackColor = true;
            this.ckbUseProxyAccount.CheckedChanged += new EventHandler(this.ckbServer_CheckedChanged);
            //this.txbProxyPort.ImeMode = ImeMode.Disable;
            this.txbProxyPort.Location = new Point(0x158, 0x2e);
            this.txbProxyPort.MaxLength = 5;
            this.txbProxyPort.Name = "txbProxyPort";
            this.txbProxyPort.Size = new Size(0x2a, 0x13);
            this.txbProxyPort.TabIndex = 4;
            this.lblProxyPort.AutoSize = true;
            this.lblProxyPort.Location = new Point(0x131, 0x31);
            this.lblProxyPort.Name = "lblProxyPort";
            this.lblProxyPort.Size = new Size(30, 12);
            this.lblProxyPort.TabIndex = 3;
            this.lblProxyPort.Text = "Cổng";
            //this.txbProxyName.ImeMode = ImeMode.Disable;
            this.txbProxyName.Location = new Point(0x7b, 0x2e);
            this.txbProxyName.MaxLength = 0x40;
            this.txbProxyName.Name = "txbProxyName";
            this.txbProxyName.Size = new Size(0xa4, 0x13);
            this.txbProxyName.TabIndex = 2;
            this.txbProxyName.Tag = "Proxy name";
            this.lblProxyName.Location = new Point(14, 0x31);
            this.lblProxyName.Name = "lblProxyName";
            this.lblProxyName.Size = new Size(0x67, 12);
            this.lblProxyName.TabIndex = 1;
            this.lblProxyName.Text = "T\x00ean m\x00e1y chủ Proxy";
            this.ckbUseProxy.AutoSize = true;
            this.ckbUseProxy.Location = new Point(0x10, 0x18);
            this.ckbUseProxy.Name = "ckbUseProxy";
            this.ckbUseProxy.Size = new Size(0x8d, 0x10);
            this.ckbUseProxy.TabIndex = 0;
            this.ckbUseProxy.Text = "Sử dụng m\x00e1y chủ proxy";
            this.ckbUseProxy.UseVisualStyleBackColor = true;
            this.ckbUseProxy.CheckedChanged += new EventHandler(this.ckbServer_CheckedChanged);
            this.gpbTrace.Controls.Add(this.lblTraceFile);
            this.gpbTrace.Controls.Add(this.txbTraceOutput2);
            this.gpbTrace.Controls.Add(this.lblTraceOutput2);
            this.gpbTrace.Controls.Add(this.txbTraceOutput1);
            this.gpbTrace.Controls.Add(this.lblTraceOutput1);
            this.gpbTrace.Controls.Add(this.ckbTraceOutput);
            this.gpbTrace.Location = new Point(12, 0xb9);
            this.gpbTrace.Name = "gpbTrace";
            this.gpbTrace.Size = new Size(0x19b, 0x2e);
            this.gpbTrace.TabIndex = 4;
            this.gpbTrace.TabStop = false;
            this.gpbTrace.Text = "Nhật k\x00fd truyền tin";
            this.lblTraceFile.AutoSize = true;
            this.lblTraceFile.Location = new Point(0xaf, 0x13);
            this.lblTraceFile.Name = "lblTraceFile";
            this.lblTraceFile.Size = new Size(60, 12);
            this.lblTraceFile.TabIndex = 1;
            this.lblTraceFile.Text = "T\x00ean tệp tin";
            this.txbTraceOutput2.Location = new Point(0xa4, 0x41);
            this.txbTraceOutput2.Name = "txbTraceOutput2";
            this.txbTraceOutput2.ReadOnly = true;
            this.txbTraceOutput2.Size = new Size(0xd3, 0x13);
            this.txbTraceOutput2.TabIndex = 5;
            this.txbTraceOutput2.TabStop = false;
            this.txbTraceOutput2.Visible = false;
            this.lblTraceOutput2.AutoSize = true;
            this.lblTraceOutput2.Location = new Point(15, 0x44);
            this.lblTraceOutput2.Name = "lblTraceOutput2";
            this.lblTraceOutput2.Size = new Size(0x8f, 12);
            this.lblTraceOutput2.TabIndex = 4;
            this.lblTraceOutput2.Text = "管理資料情報取出サーバ用";
            this.lblTraceOutput2.Visible = false;
            this.txbTraceOutput1.Location = new Point(0xee, 0x10);
            this.txbTraceOutput1.Name = "txbTraceOutput1";
            this.txbTraceOutput1.ReadOnly = true;
            this.txbTraceOutput1.Size = new Size(0xa5, 0x13);
            this.txbTraceOutput1.TabIndex = 3;
            this.txbTraceOutput1.TabStop = false;
            this.lblTraceOutput1.AutoSize = true;
            this.lblTraceOutput1.Location = new Point(15, 0x2a);
            this.lblTraceOutput1.Name = "lblTraceOutput1";
            this.lblTraceOutput1.Size = new Size(0x47, 12);
            this.lblTraceOutput1.TabIndex = 2;
            this.lblTraceOutput1.Text = "会話サーバ用";
            this.lblTraceOutput1.Visible = false;
            this.ckbTraceOutput.Location = new Point(0x10, 0x12);
            this.ckbTraceOutput.Name = "ckbTraceOutput";
            this.ckbTraceOutput.Size = new Size(0x98, 0x10);
            this.ckbTraceOutput.TabIndex = 0;
            this.ckbTraceOutput.Text = "Tệp tin nhật k\x00fd truyền tin";
            this.ckbTraceOutput.UseVisualStyleBackColor = true;
            this.ckbTraceOutput.CheckedChanged += new EventHandler(this.ckbServer_CheckedChanged);
            this.gpbCertificate.Controls.Add(this.btnCertificateSelect);
            this.gpbCertificate.Controls.Add(this.txbCertificateDate);
            this.gpbCertificate.Controls.Add(this.lblCertificateDate);
            this.gpbCertificate.Controls.Add(this.txbIssuerName);
            this.gpbCertificate.Controls.Add(this.lblIssuerName);
            this.gpbCertificate.Location = new Point(12, 0x25);
            this.gpbCertificate.Name = "gpbCertificate";
            this.gpbCertificate.Size = new Size(0x19b, 0x52);
            this.gpbCertificate.TabIndex = 2;
            this.gpbCertificate.TabStop = false;
            this.gpbCertificate.Text = "クライアント証明書";
            this.gpbCertificate.Visible = false;
            this.btnCertificateSelect.Location = new Point(0x10, 0x12);
            this.btnCertificateSelect.Name = "btnCertificateSelect";
            this.btnCertificateSelect.Size = new Size(0x98, 0x17);
            this.btnCertificateSelect.TabIndex = 0;
            this.btnCertificateSelect.Text = "クライアント証明書の選択";
            this.btnCertificateSelect.UseVisualStyleBackColor = true;
            this.btnCertificateSelect.Click += new EventHandler(this.btnCertificateSelect_Click);
            this.txbCertificateDate.Location = new Point(0xee, 0x36);
            this.txbCertificateDate.Name = "txbCertificateDate";
            this.txbCertificateDate.ReadOnly = true;
            this.txbCertificateDate.Size = new Size(70, 0x13);
            this.txbCertificateDate.TabIndex = 4;
            this.txbCertificateDate.TabStop = false;
            this.lblCertificateDate.AutoSize = true;
            this.lblCertificateDate.Location = new Point(0xb3, 0x39);
            this.lblCertificateDate.Name = "lblCertificateDate";
            this.lblCertificateDate.Size = new Size(0x35, 12);
            this.lblCertificateDate.TabIndex = 3;
            this.lblCertificateDate.Text = "有効期限";
            this.txbIssuerName.Location = new Point(0x3e, 0x36);
            this.txbIssuerName.Name = "txbIssuerName";
            this.txbIssuerName.ReadOnly = true;
            this.txbIssuerName.Size = new Size(0x6a, 0x13);
            this.txbIssuerName.TabIndex = 2;
            this.txbIssuerName.TabStop = false;
            this.lblIssuerName.AutoSize = true;
            this.lblIssuerName.Location = new Point(15, 0x39);
            this.lblIssuerName.Name = "lblIssuerName";
            this.lblIssuerName.Size = new Size(0x29, 12);
            this.lblIssuerName.TabIndex = 1;
            this.lblIssuerName.Text = "発行者";
            this.cmbServerConnect.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbServerConnect.FormattingEnabled = true;
            this.cmbServerConnect.Location = new Point(0x7c, 10);
            this.cmbServerConnect.Name = "cmbServerConnect";
            this.cmbServerConnect.Size = new Size(0xdf, 20);
            this.cmbServerConnect.TabIndex = 1;
//            this.lblServerConnect.ImeMode = ImeMode.NoControl;
            this.lblServerConnect.Location = new Point(10, 13);
            this.lblServerConnect.Name = "lblServerConnect";
            this.lblServerConnect.Size = new Size(0x77, 12);
            this.lblServerConnect.TabIndex = 0;
            this.lblServerConnect.Text = "Điểm kết nối m\x00e1y chủ";
            this.gpbJobConnect.Controls.Add(this.nudAutoSendRecvTm);
            this.gpbJobConnect.Controls.Add(this.ckbAutoSendRecvMode);
            this.gpbJobConnect.Controls.Add(this.ckbJobConnect);
            this.gpbJobConnect.Controls.Add(this.lblAutoSendRecvTm);
            this.gpbJobConnect.Controls.Add(this.lblMin);
            this.gpbJobConnect.Location = new Point(12, 250);
            this.gpbJobConnect.Name = "gpbJobConnect";
            this.gpbJobConnect.Size = new Size(0x19b, 0x53);
            this.gpbJobConnect.TabIndex = 5;
            this.gpbJobConnect.TabStop = false;
            this.gpbJobConnect.Text = "Tự động nhận th\x00f4ng b\x00e1o từ VNACCS";
            this.nudAutoSendRecvTm.Location = new Point(0x6b, 0x33);
            bits = new int[4];
            bits[0] = 0x63;
            this.nudAutoSendRecvTm.Maximum = new decimal(bits);
            bits = new int[4];
            bits[0] = 5;
            this.nudAutoSendRecvTm.Minimum = new decimal(bits);
            this.nudAutoSendRecvTm.Name = "nudAutoSendRecvTm";
            this.nudAutoSendRecvTm.Size = new Size(0x29, 0x13);
            this.nudAutoSendRecvTm.TabIndex = 2;
            this.nudAutoSendRecvTm.Tag = "";
            bits = new int[4];
            bits[0] = 10;
            this.nudAutoSendRecvTm.Value = new decimal(bits);
            this.nudAutoSendRecvTm.Validating += new CancelEventHandler(this.nud_Validating);
            this.ckbAutoSendRecvMode.AutoSize = true;
            this.ckbAutoSendRecvMode.Location = new Point(13, 0x18);
            this.ckbAutoSendRecvMode.Name = "ckbAutoSendRecvMode";
            this.ckbAutoSendRecvMode.Size = new Size(90, 0x10);
            this.ckbAutoSendRecvMode.TabIndex = 0;
            this.ckbAutoSendRecvMode.Text = "Tự động nhận";
            this.ckbAutoSendRecvMode.UseVisualStyleBackColor = true;
            this.ckbAutoSendRecvMode.CheckedChanged += new EventHandler(this.ckbServer_CheckedChanged);
            this.ckbJobConnect.Location = new Point(0xc7, 0x34);
            this.ckbJobConnect.Name = "ckbJobConnect";
            this.ckbJobConnect.Size = new Size(120, 0x10);
            this.ckbJobConnect.TabIndex = 4;
            this.ckbJobConnect.Text = "Li\x00ean kết nghiệp vụ";
            this.ckbJobConnect.UseVisualStyleBackColor = true;
            this.lblAutoSendRecvTm.Location = new Point(11, 0x35);
            this.lblAutoSendRecvTm.Name = "lblAutoSendRecvTm";
            this.lblAutoSendRecvTm.Size = new Size(0x5b, 12);
            this.lblAutoSendRecvTm.TabIndex = 1;
            this.lblAutoSendRecvTm.Text = "Khoảng thời gian";
            this.lblMin.AutoSize = true;
            this.lblMin.Location = new Point(0x9a, 0x35);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new Size(0x1c, 12);
            this.lblMin.TabIndex = 3;
            this.lblMin.Text = "Ph\x00fat";
            this.btnRootAcpt.Location = new Point(0x2f1, 0x20a);
            this.btnRootAcpt.Name = "btnRootAcpt";
            this.btnRootAcpt.Size = new Size(70, 0x17);
            this.btnRootAcpt.TabIndex = 0x19;
            this.btnRootAcpt.Text = "Apply";
            this.btnRootAcpt.UseVisualStyleBackColor = true;
            this.btnRootAcpt.Click += new EventHandler(this.btnRootAcpt_Click);
            this.dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "clmAPOutCode";
            this.dataGridViewTextBoxColumn1.HeaderText = "出力情報コード";
            this.dataGridViewTextBoxColumn1.MaxInputLength = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 110;
            this.dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "clmAPCount";
            style34.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = style34;
            this.dataGridViewTextBoxColumn2.HeaderText = "部数";
            this.dataGridViewTextBoxColumn2.MaxInputLength = 2;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 0x37;
            this.dataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "clmAPPrinter";
            style35.BackColor = SystemColors.Control;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = style35;
            this.dataGridViewTextBoxColumn3.HeaderText = "プリンタ";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "clmAPBinName";
            style36.BackColor = SystemColors.Control;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = style36;
            this.dataGridViewTextBoxColumn4.HeaderText = "給紙装置名";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "clmAPBinNo";
            this.dataGridViewTextBoxColumn5.HeaderText = "clmAPBinNo";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Visible = false;
            this.dataGridViewTextBoxColumn5.Width = 40;
            this.dataGridViewTextBoxColumn6.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "clmMCUserCode";
            this.dataGridViewTextBoxColumn6.HeaderText = "利用者コード";
            this.dataGridViewTextBoxColumn6.MaxInputLength = 5;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn7.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn7.DataPropertyName = "clmMCJobCode";
            this.dataGridViewTextBoxColumn7.HeaderText = "業務コード";
            this.dataGridViewTextBoxColumn7.MaxInputLength = 5;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn8.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn8.DataPropertyName = "clmMCOutCode";
            this.dataGridViewTextBoxColumn8.HeaderText = "出力情報コード";
            this.dataGridViewTextBoxColumn8.MaxInputLength = 6;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn8.Width = 110;
            this.dataGridViewTextBoxColumn9.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn9.DataPropertyName = "clmMCBankNumber";
            this.dataGridViewTextBoxColumn9.HeaderText = "口座番号";
            this.dataGridViewTextBoxColumn9.MaxInputLength = 0x10;
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn9.Width = 150;
            this.dataGridViewTextBoxColumn10.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn10.DataPropertyName = "clmMCFolder";
            style37.BackColor = SystemColors.Control;
            this.dataGridViewTextBoxColumn10.DefaultCellStyle = style37;
            this.dataGridViewTextBoxColumn10.HeaderText = "振り分け先フォルダ";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn11.DataPropertyName = "clmMCId";
            this.dataGridViewTextBoxColumn11.HeaderText = "clmMCId";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn11.Visible = false;
            this.dataGridViewTextBoxColumn12.DataPropertyName = "clmMCPriority";
            this.dataGridViewTextBoxColumn12.HeaderText = "clmMCPriority";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn12.Visible = false;
            this.dataGridViewTextBoxColumn13.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn13.DataPropertyName = "clmFSKDataName";
            this.dataGridViewTextBoxColumn13.HeaderText = "電文種別(種別コード)";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn14.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn14.DataPropertyName = "clmFSKDir";
            style38.BackColor = SystemColors.Control;
            this.dataGridViewTextBoxColumn14.DefaultCellStyle = style38;
            this.dataGridViewTextBoxColumn14.HeaderText = "保存先";
            this.dataGridViewTextBoxColumn14.MaxInputLength = 0xff;
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            this.dataGridViewTextBoxColumn14.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn15.DataPropertyName = "clmFSKDataType";
            this.dataGridViewTextBoxColumn15.HeaderText = "clmFSKDataType";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn15.Visible = false;
            this.dataGridViewTextBoxColumn16.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn16.DataPropertyName = "clmFSCOutCode";
            this.dataGridViewTextBoxColumn16.HeaderText = "出力情報コード";
            this.dataGridViewTextBoxColumn16.MaxInputLength = 6;
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn16.Width = 110;
            this.dataGridViewTextBoxColumn17.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn17.DataPropertyName = "clmFSCDir";
            style39.BackColor = SystemColors.Control;
            this.dataGridViewTextBoxColumn17.DefaultCellStyle = style39;
            this.dataGridViewTextBoxColumn17.HeaderText = "保存先";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.ReadOnly = true;
            this.dataGridViewTextBoxColumn17.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn18.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn18.DataPropertyName = "clmRNOutCode";
            this.dataGridViewTextBoxColumn18.HeaderText = "出力情報コード";
            this.dataGridViewTextBoxColumn18.MaxInputLength = 6;
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn18.Width = 110;
            this.dataGridViewTextBoxColumn19.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn19.DataPropertyName = "clmKJGymCode";
            this.dataGridViewTextBoxColumn19.FillWeight = 347f;
            this.dataGridViewTextBoxColumn19.HeaderText = "業務コード";
            this.dataGridViewTextBoxColumn19.MaxInputLength = 5;
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn20.DataPropertyName = "clmKJWindowCode";
            this.dataGridViewTextBoxColumn20.HeaderText = "画面コード";
            this.dataGridViewTextBoxColumn20.MaxInputLength = 3;
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn20.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn20.Visible = false;
            this.dataGridViewTextBoxColumn21.DataPropertyName = "clmKJNameKj";
            this.dataGridViewTextBoxColumn21.HeaderText = "clmKJNameKj";
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn21.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn21.Visible = false;
            this.dataGridViewTextBoxColumn22.DataPropertyName = "clmKJName";
            this.dataGridViewTextBoxColumn22.HeaderText = "clmKJName";
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            this.dataGridViewTextBoxColumn22.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn22.Visible = false;
            this.dataGridViewTextBoxColumn23.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn23.DataPropertyName = "clmKSNameKj";
            this.dataGridViewTextBoxColumn23.HeaderText = "機能名";
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            this.dataGridViewTextBoxColumn23.ReadOnly = true;
            this.dataGridViewTextBoxColumn23.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn24.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn24.DataPropertyName = "clmKRNameKj";
            this.dataGridViewTextBoxColumn24.HeaderText = "機能名";
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            this.dataGridViewTextBoxColumn24.ReadOnly = true;
            this.dataGridViewTextBoxColumn24.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn25.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn25.DataPropertyName = "clmKGNameKj";
            this.dataGridViewTextBoxColumn25.HeaderText = "機能名";
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            this.dataGridViewTextBoxColumn25.ReadOnly = true;
            this.dataGridViewTextBoxColumn25.SortMode = DataGridViewColumnSortMode.NotSortable;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.ClientSize = new Size(0x394, 0x224);
            base.Controls.Add(this.tbcRoot);
            base.Controls.Add(this.btnRootAcpt);
            base.Controls.Add(this.btnRootOK);
            base.Controls.Add(this.btnRootCncl);
            this.DoubleBuffered = true;
//            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.KeyPreview = true;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "OptionDialog";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Lựa chọn";
            base.Load += new EventHandler(this.OptionDialog_Load);
            base.KeyDown += new KeyEventHandler(this.OptionDialog_KeyDown);
            ((ISupportInitialize) this.bdsAutoPrint).EndInit();
            this.dtsOption.EndInit();
            this.dttAutoPrint.EndInit();
            this.dttMsgCls.EndInit();
            this.dttFileSaveK.EndInit();
            this.dttFileSaveC.EndInit();
            this.dttRcvNotice.EndInit();
            ((ISupportInitialize) this.bdsFileSaveK).EndInit();
            ((ISupportInitialize) this.bdsFileSaveC).EndInit();
            ((ISupportInitialize) this.bdsRcvNotice).EndInit();
            ((ISupportInitialize) this.bdsMsgCls).EndInit();
            this.tbpHelp.ResumeLayout(false);
            this.tbpHelp.PerformLayout();
            this.tbpLog.ResumeLayout(false);
            this.gpbComLog.ResumeLayout(false);
            this.gpbComLog.PerformLayout();
            this.nudComLogSize.EndInit();
            this.tbpTerm.ResumeLayout(false);
            this.tbpTerm.PerformLayout();
            this.gpbCommonFolder.ResumeLayout(false);
            this.gpbCommonFolder.PerformLayout();
            this.gpbOptionShare.ResumeLayout(false);
            this.gpbOptionShare.PerformLayout();
            this.nudDiskWarning.EndInit();
            this.nudSaveTerm.EndInit();
            this.gpbTerminalInfo.ResumeLayout(false);
            this.gpbTerminalInfo.PerformLayout();
            this.tbpUserKey.ResumeLayout(false);
            this.tbcUserKey.ResumeLayout(false);
            this.tbpUserKeyJ.ResumeLayout(false);
            this.tbpUserKeyJ.PerformLayout();
            ((ISupportInitialize) this.dgvUserKeyJ).EndInit();
            ((ISupportInitialize) this.bdsUserKeyJ).EndInit();
            this.dtsUserKey.EndInit();
            this.dttUserKeyJ.EndInit();
            this.dttUserKeyS.EndInit();
            this.dttUserKeyR.EndInit();
            this.dttUserKeyG.EndInit();
            this.bdnUserKeyJ.EndInit();
            this.bdnUserKeyJ.ResumeLayout(false);
            this.bdnUserKeyJ.PerformLayout();
            this.tbpUserKeyS.ResumeLayout(false);
            this.tbpUserKeyS.PerformLayout();
            ((ISupportInitialize) this.dgvUserKeyS).EndInit();
            ((ISupportInitialize) this.bdsUserKeyS).EndInit();
            this.bdnUserKeyS.EndInit();
            this.bdnUserKeyS.ResumeLayout(false);
            this.bdnUserKeyS.PerformLayout();
            this.tbpUserKeyR.ResumeLayout(false);
            this.tbpUserKeyR.PerformLayout();
            ((ISupportInitialize) this.dgvUserKeyR).EndInit();
            ((ISupportInitialize) this.bdsUserKeyR).EndInit();
            this.bdnUserKeyR.EndInit();
            this.bdnUserKeyR.ResumeLayout(false);
            this.bdnUserKeyR.PerformLayout();
            this.tbpUserKeyG.ResumeLayout(false);
            this.tbpUserKeyG.PerformLayout();
            ((ISupportInitialize) this.dgvUserKeyG).EndInit();
            ((ISupportInitialize) this.bdsUserKeyG).EndInit();
            this.bdnUserKeyG.EndInit();
            this.bdnUserKeyG.ResumeLayout(false);
            this.bdnUserKeyG.PerformLayout();
            this.tbpReceiveInform.ResumeLayout(false);
            this.tbpReceiveInform.PerformLayout();
            this.gpbReceiveInform.ResumeLayout(false);
            this.gpbReceiveInform.PerformLayout();
            this.gpbReceiveNotice.ResumeLayout(false);
            this.gpbReceiveNotice.PerformLayout();
            ((ISupportInitialize) this.dgvReceiveInform).EndInit();
            this.bdnRcvNotice.EndInit();
            this.bdnRcvNotice.ResumeLayout(false);
            this.bdnRcvNotice.PerformLayout();
            this.tbpFileSaveC.ResumeLayout(false);
            this.gpbFileSaveC.ResumeLayout(false);
            this.gpbFileSaveC.PerformLayout();
            ((ISupportInitialize) this.dgvFileSaveC).EndInit();
            this.bdnFileSaveC.EndInit();
            this.bdnFileSaveC.ResumeLayout(false);
            this.bdnFileSaveC.PerformLayout();
            this.tbpFileSaveK.ResumeLayout(false);
            this.tbpFileSaveK.PerformLayout();
            this.gpbKanriFile.ResumeLayout(false);
            this.gpbKanriFile.PerformLayout();
            this.gpbFileSaveK.ResumeLayout(false);
            ((ISupportInitialize) this.dgvFileSaveK).EndInit();
            this.gpbSendFile.ResumeLayout(false);
            this.gpbSendFile.PerformLayout();
            this.gpbFileNameRule.ResumeLayout(false);
            this.gpbFileNameRule.PerformLayout();
            this.tbpMsgCls.ResumeLayout(false);
            this.gpbMsgCls.ResumeLayout(false);
            this.gpbMsgCls.PerformLayout();
            ((ISupportInitialize) this.dgvMsgCls).EndInit();
            this.bdnMsgCls.EndInit();
            this.bdnMsgCls.ResumeLayout(false);
            this.bdnMsgCls.PerformLayout();
            this.tbpAutoPrint.ResumeLayout(false);
            this.gpbPrintout.ResumeLayout(false);
            this.gpbPrintout.PerformLayout();
            ((ISupportInitialize) this.dgvAutoPrint).EndInit();
            this.bdnAutoPrint.EndInit();
            this.bdnAutoPrint.ResumeLayout(false);
            this.bdnAutoPrint.PerformLayout();
            this.tbpPrinter.ResumeLayout(false);
            this.tbpPrinter.PerformLayout();
            this.gpbDefaultPrinter.ResumeLayout(false);
            this.gpbDefaultPrinter.PerformLayout();
            this.gpbEntryPrinter.ResumeLayout(false);
            this.gpbEntryPrinter.PerformLayout();
            this.gpbMargin.ResumeLayout(false);
            this.gpbMargin.PerformLayout();
            this.nudMarginL.EndInit();
            this.nudMarginT.EndInit();
            this.tbpPassword.ResumeLayout(false);
            this.tbcPassword.ResumeLayout(false);
            this.tbpPwdChk.ResumeLayout(false);
            this.gpbPwdCert.ResumeLayout(false);
            this.gpbPwdCert.PerformLayout();
            this.tbpPwdChng.ResumeLayout(false);
            this.tbpPwdChng.PerformLayout();
            this.gpbPwdKind.ResumeLayout(false);
            this.gpbPwdKind.PerformLayout();
            this.tbcRoot.ResumeLayout(false);
            this.tbpServer.ResumeLayout(false);
            this.tbpServer.PerformLayout();
            this.pnlMail.ResumeLayout(false);
            this.pnlMail.PerformLayout();
            this.nudMaxRetreiveCnt.EndInit();
            this.gpbTraceM.ResumeLayout(false);
            this.gpbTraceM.PerformLayout();
            this.gpbProxyM.ResumeLayout(false);
            this.gpbProxyM.PerformLayout();
            this.gpbAutoSndRcvTm.ResumeLayout(false);
            this.gpbAutoSndRcvTm.PerformLayout();
            this.nudAutoSndRcvTm.EndInit();
            this.gpbAutoTmKind.ResumeLayout(false);
            this.gpbAutoTmKind.PerformLayout();
            this.pnlServer.ResumeLayout(false);
            this.gpbProxy.ResumeLayout(false);
            this.gpbProxy.PerformLayout();
            this.gpbTrace.ResumeLayout(false);
            this.gpbTrace.PerformLayout();
            this.gpbCertificate.ResumeLayout(false);
            this.gpbCertificate.PerformLayout();
            this.gpbJobConnect.ResumeLayout(false);
            this.gpbJobConnect.PerformLayout();
            this.nudAutoSendRecvTm.EndInit();
            base.ResumeLayout(false);
        }

        private bool IsEntryPrinter(string PrinterName)
        {
            foreach (ListViewItem item in this.lsvPrt.Items)
            {
                if (PrinterName == item.Text)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsErrorBinNo(BinListClass blc, int binno)
        {
            foreach (BinClass class2 in blc)
            {
                if (class2.No == binno)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsRowDelete()
        {
            if ((!this.bdnAPDeleteItem.Checked && !this.bdnMCDeleteItem.Checked) && (!this.bdnASCDeleteItem.Checked && !this.bdnRNDeleteItem.Checked))
            {
                return false;
            }
            return true;
        }

        private void LoadAutoPrint()
        {
            this.dttAutoPrint.Clear();
            this.outcodelist_ap = new List<string>();
            foreach (OutCodeClass class2 in printSetups.Forms.OutCodeList)
            {
                DataRow row = this.dttAutoPrint.NewRow();
                row[this.dtclmAPOutCode] = class2.Name;
                row[this.dtclmAPMode] = class2.Mode;
                row[this.dtclmAPCount] = class2.Count;
                row[this.dtclmAPPrinter] = class2.Printer;
                row[this.dtclmAPBinName] = class2.BinName;
                row[this.dtclmAPBinNo] = class2.BinNo;
                this.dttAutoPrint.Rows.Add(row);
                this.outcodelist_ap.Add(class2.Name);
            }
            this.ckbAutoPrt.Checked = printSetups.DefaultInfo.Mode;
        }

        private void LoadFileSaveC()
        {
            this.dttFileSaveC.Clear();
            this.outcodelist_asc = new List<string>();
            foreach (DetailsClass class2 in fileSave.DetailsList)
            {
                DataRow row = this.dttFileSaveC.NewRow();
                row[this.dtclmFSCOutCode] = class2.OutCode;
                row[this.dtclmFSCAutoSave] = class2.AutoSave;
                row[this.dtclmFSCDir] = class2.Dir;
                this.dttFileSaveC.Rows.Add(row);
                this.outcodelist_asc.Add(class2.OutCode);
            }
        }

        private void LoadFileSaveK()
        {
            this.dttFileSaveK.Clear();
            foreach (TypeClass class2 in fileSave.TypeList)
            {
                DataRow row = this.dttFileSaveK.NewRow();
                string dataType = class2.DataType;
                if (dataType != null)
                {
                    if (!(dataType == "F"))
                    {
                        if (dataType == "C")
                        {
                            goto Label_009D;
                        }
                        if (dataType == "M")
                        {
                            goto Label_00B0;
                        }
                        if (dataType == "P")
                        {
                            goto Label_00C3;
                        }
                        if (dataType == "R")
                        {
                            goto Label_00D6;
                        }
                    }
                    else
                    {
                        this.txbKanriFile.Text = class2.Dir;
                    }
                }
                continue;
            Label_009D:
                row[this.dtclmFSKDataName] = "Th\x00f4ng điệp th\x00f4ng b\x00e1o d\x00e0nh cho m\x00e0n h\x00ecnh (C)";
                goto Label_00E7;
            Label_00B0:
                row[this.dtclmFSKDataName] = "Th\x00f4ng điệp th\x00f4ng b\x00e1o d\x00e0nh cho m\x00e0n h\x00ecnh chứa th\x00f4ng tin kết quả xử l\x00fd (M)";
                goto Label_00E7;
            Label_00C3:
                row[this.dtclmFSKDataName] = "Th\x00f4ng điệp d\x00e0nh cho b\x00e1o c\x00e1o (P)";
                goto Label_00E7;
            Label_00D6:
                row[this.dtclmFSKDataName] = "Th\x00f4ng điệp th\x00f4ng b\x00e1o kết quả xử l\x00fd (R)";
            Label_00E7:
                row[this.dtclmFSKDataType] = class2.DataType;
                row[this.dtclmFSKFileSaveMode] = class2.FileSaveMode;
                row[this.dtclmFSKDir] = class2.Dir;
                this.dttFileSaveK.Rows.Add(row);
            }
            this.cmbFileNameRule1.SelectedIndex = int.Parse(fileSave.FileNaming.FileNameRule.Substring(0, 1)) - 1;
            if (fileSave.FileNaming.FileNameRule.Substring(1, 1) == "9")
            {
                this.cmbFileNameRule2.SelectedIndex = 0;
            }
            else
            {
                this.cmbFileNameRule2.SelectedIndex = int.Parse(fileSave.FileNaming.FileNameRule.Substring(1, 1));
            }
            if (fileSave.FileNaming.FileNameRule.Substring(2, 1) == "9")
            {
                this.cmbFileNameRule3.SelectedIndex = 0;
            }
            else
            {
                this.cmbFileNameRule3.SelectedIndex = int.Parse(fileSave.FileNaming.FileNameRule.Substring(2, 1));
            }
            if (fileSave.FileNaming.FileNameRule.Substring(3, 1) == "9")
            {
                this.cmbFileNameRule4.SelectedIndex = 0;
            }
            else
            {
                this.cmbFileNameRule4.SelectedIndex = int.Parse(fileSave.FileNaming.FileNameRule.Substring(3, 1));
            }
            this.ckbFileSave.Checked = fileSave.AllFileSave.Mode;
            this.txbSendFile.Text = fileSave.SendFile.SendUser;
        }

        private void LoadHelp()
        {
            this.txbJobErrCodeHtml.Text = helpSettings.HelpPath.GymErrIndexHtml;
            this.txbTrmErrCodeHtml.Text = helpSettings.HelpPath.TermMsgIndexHtml;
        }

        private void LoadLog()
        {
            this.nudComLogSize.Value = userEnvironment.LogFileLength.ComLog;
            this.txbComLog1.Text = systemEnvironment.LogFileLength.NACCS_ComLog1;
            this.txbComLog2.Text = systemEnvironment.LogFileLength.NACCS_ComLog2;
        }

        private void LoadMsgCls()
        {
            DataRow row;
            this.dttMsgCls.Rows.Clear();
            this.outcodelist_mc = new List<string>();
            if (this.IsBank)
            {
                foreach (TermClass class2 in messageClassify.TermList)
                {
                    row = this.dttMsgCls.NewRow();
                    row[this.dtclmMCUserCode] = class2.UserCode;
                    row[this.dtclmMCJobCode] = class2.JobCoode;
                    row[this.dtclmMCOutCode] = class2.OutCode;
                    row[this.dtclmMCBankNumber] = class2.BankNumber;
                    row[this.dtclmMCUnOpen] = class2.UnOpen;
                    row[this.dtclmMCBankWithdrawday] = class2.BankWithdrawday;
                    row[this.dtclmMCFolder] = class2.Folder;
                    this.dttMsgCls.Rows.Add(row);
                    this.outcodelist_mc.Add(class2.OutCode);
                }
            }
            else
            {
                foreach (TermClass class3 in messageClassify.TermList)
                {
                    if (((class3.UserCode != "") || (class3.JobCoode != "")) || (class3.OutCode != ""))
                    {
                        row = this.dttMsgCls.NewRow();
                        row[this.dtclmMCUserCode] = class3.UserCode;
                        row[this.dtclmMCJobCode] = class3.JobCoode;
                        row[this.dtclmMCOutCode] = class3.OutCode;
                        row[this.dtclmMCBankNumber] = "";
                        row[this.dtclmMCUnOpen] = class3.UnOpen;
                        row[this.dtclmMCBankWithdrawday] = false;
                        row[this.dtclmMCFolder] = class3.Folder;
                        this.dttMsgCls.Rows.Add(row);
                        this.outcodelist_mc.Add(class3.OutCode);
                    }
                }
                this.dgvMsgCls.Columns[this.cMCBankNumber.Index].Visible = false;
                this.dgvMsgCls.Columns[this.cMCBankWithdrawday.Index].Visible = false;
            }
            this.ckbUseAutoBackup.Checked = messageClassify.DataView.AutoBackup;
        }

        private void LoadPassword()
        {
            this.PassWord = optionCertification.AllCertification.Password;
            if (string.IsNullOrEmpty(optionCertification.AllCertification.Password))
            {
                this.NoUsePassword();
                this.tbcRoot.SelectTab(this.tbpTerm.Name);
            }
            else
            {
                this.UsePassword();
                this.btnRootOK.Enabled = false;
                this.btnRootAcpt.Enabled = false;
                this.tbcRoot.SelectTab(this.tbpPassword.Name);
            }
        }

        private void LoadPrinter()
        {
            this.txbDefaultPrinter.Text = printSetups.DefaultInfo.Printer;
            this.txbDefaultBin.Text = printSetups.DefaultInfo.BinName;
            this.default_binno = printSetups.DefaultInfo.BinNo;
            this.chbSizeWarning.Checked = printSetups.DefaultInfo.SizeWarning;
            this.prtInfoList = new List<PrinterInfoClass>();
            if (printSetups.PrinterInfoList.Count != 0)
            {
                foreach (PrinterInfoClass class4 in printSetups.PrinterInfoList)
                {
                    PrinterInfoClass item = new PrinterInfoClass {
                        Name = class4.Name,
                        DoublePrint = class4.DoublePrint
                    };
                    foreach (BinClass class5 in class4.BinList)
                    {
                        BinClass bin = new BinClass {
                            L = class5.L,
                            Name = class5.Name,
                            No = class5.No,
                            Orientation = class5.Orientation,
                            Size = class5.Size,
                            T = class5.T
                        };
                        item.BinList.Add(bin);
                    }
                    this.prtInfoList.Add(item);
                }
            }
            else
            {
                PrintDocument document = new PrintDocument();
                if (document.PrinterSettings.IsValid)
                {
                    this.GetPrinterInfo(document.PrinterSettings);
                }
            }
            this.tbpPrinter_Refresh();
        }

        private void LoadReceiveInform()
        {
            this.dttRcvNotice.Clear();
            this.outcodelist_rn = new List<string>();
            foreach (ReceiveInformClass class2 in receiveNotice.ReceiveInformList)
            {
                DataRow row = this.dttRcvNotice.NewRow();
                row[this.dtclmRNOutCode] = class2.OutCode;
                row[this.dtclmRNInform] = class2.Inform;
                row[this.dtclmRNInformSound] = class2.InformSound;
                this.dttRcvNotice.Rows.Add(row);
                this.outcodelist_rn.Add(class2.OutCode);
            }
            this.ckbRcvInformSnd.Checked = receiveNotice.WarningInform.WaringMode;
            this.ckbRcvInform.Checked = receiveNotice.Inform.InfoMode;
            this.gpbReceiveNotice.Enabled = this.ckbRcvInform.Checked;
            this.btnSoundFile.Enabled = this.ckbRcvInform.Checked;
            this.txbSoundFile.Text = receiveNotice.Sound.SoundFile;
        }

        private void LoadServer()
        {
            if (this.Protocol != ProtocolType.Mail)
            {
                this.pnlServer.Visible = true;
                this.pnlMail.Visible = false;
                this.cmbServerConnect.Items.Clear();
                this.cmbServerConnect.Items.AddRange(systemEnvironment.InteractiveInfo.GetDisplayNames().ToArray());
                if (this.cmbServerConnect.Items.Count > 0)
                {
                    this.cmbServerConnect.SelectedIndex = 0;
                }
                int num2 = 0;
                foreach (string str2 in systemEnvironment.InteractiveInfo.GetNames())
                {
                    if (str2 == userEnvironment.InteractiveInfo.ServerConnect)
                    {
                        if (this.cmbServerConnect.Items.Count > num2)
                        {
                            this.cmbServerConnect.SelectedIndex = num2;
                        }
                        break;
                    }
                    num2++;
                }
            }
            else
            {
                this.pnlServer.Visible = false;
                this.pnlMail.Visible = true;
                this.pnlMail.Location = new Point(8, 0x11);
                this.cmbServerConnectM.Items.Clear();
                this.cmbServerConnectM.Items.AddRange(systemEnvironment.MailInfo.GetDisplayNames().ToArray());
                if (this.cmbServerConnectM.Items.Count > 0)
                {
                    this.cmbServerConnectM.SelectedIndex = 0;
                }
                int num = 0;
                foreach (string str in systemEnvironment.MailInfo.GetNames())
                {
                    if (str == userEnvironment.MailInfo.ServerConnect)
                    {
                        if (this.cmbServerConnectM.Items.Count > num)
                        {
                            this.cmbServerConnectM.SelectedIndex = num;
                        }
                        break;
                    }
                    num++;
                }
                this.ckbUseGateway.Checked = userEnvironment.MailInfo.UseGateway;
                this.btnGateway.Enabled = this.ckbUseGateway.Checked;
                this.gsc = new GatewaySettingClass();
                this.gsc.GatewaySMTPServerName = userEnvironment.MailInfo.GatewaySMTPServerName;
                this.gsc.GatewaySMTPServerPort = userEnvironment.MailInfo.GatewaySMTPServerPort;
                this.gsc.GatewayPOPServerName = userEnvironment.MailInfo.GatewayPOPServerName;
                this.gsc.GatewayPOPServerPort = userEnvironment.MailInfo.GatewayPOPServerPort;
                this.gsc.GatewaySMTPDomainName = userEnvironment.MailInfo.GatewaySMTPDomainName;
                this.gsc.GatewayPOPDomainName = userEnvironment.MailInfo.GatewayPOPDomainName;
                this.gsc.GatewayMailBox = userEnvironment.MailInfo.GatewayMailBox;
                this.gsc.GatewayAddDomainName = userEnvironment.MailInfo.GatewayAddDomainName;
                this.ckbUseProxyM.Checked = userEnvironment.HttpOption.UseProxy;
                this.txbProxyNameM.Text = userEnvironment.HttpOption.ProxyName;
                this.txbProxyPortM.Text = userEnvironment.HttpOption.ProxyPort.ToString();
                this.ckbUseProxyAccountM.Checked = userEnvironment.HttpOption.UseProxyAccount;
                this.txbProxyAccountM.Text = userEnvironment.HttpOption.ProxyAccount;
                this.txbProxyPasswordM.Text = userEnvironment.HttpOption.ProxyPassword;
                this.ckbTraceOutputM.Checked = userEnvironment.MailInfo.TraceOutput;
                this.txbTraceOutput1M.Text = systemEnvironment.MailInfo.TraceFileName;
                this.txbTraceOutput1M.Enabled = this.ckbTraceOutputM.Checked;
                this.txbTraceOutput2M.Text = systemEnvironment.BatchdocInfo.TraceFileName;
                this.txbTraceOutput2M.Enabled = this.ckbTraceOutputM.Checked;
                this.ckbUseAutoSndRcvTm.Checked = userEnvironment.MailInfo.AutoMode;
                this.nudAutoSndRcvTm.Enabled = this.ckbUseAutoSndRcvTm.Checked;
                if (userEnvironment.MailInfo.AutoTm < this.nudAutoSndRcvTm.Minimum)
                {
                    this.nudAutoSndRcvTm.Value = this.nudAutoSndRcvTm.Minimum;
                }
                else
                {
                    this.nudAutoSndRcvTm.Value = userEnvironment.MailInfo.AutoTm;
                }
                switch (userEnvironment.MailInfo.AutoTmKind)
                {
                    case 2:
                        this.rdbAutoTmKindS.Checked = true;
                        break;

                    case 3:
                        this.rdbAutoTmKindR.Checked = true;
                        break;

                    default:
                        this.rdbAutoTmKindSR.Checked = true;
                        break;
                }
                this.nudMaxRetreiveCnt.Value = userEnvironment.MailInfo.MaxRetreiveCnt;
                this.ckbServer_CheckedChanged(this.ckbUseProxyM, new EventArgs());
                this.ckbServer_CheckedChanged(this.ckbUseProxyAccountM, new EventArgs());
                this.ckbServer_CheckedChanged(this.ckbUseAutoSndRcvTm, new EventArgs());
                this.ckbServer_CheckedChanged(this.ckbTraceOutputM, new EventArgs());
                this.ckbServer_CheckedChanged(this.ckbUseGateway, new EventArgs());
                return;
            }
            this.ckbUseProxy.Checked = userEnvironment.HttpOption.UseProxy;
            this.txbProxyName.Text = userEnvironment.HttpOption.ProxyName;
            this.txbProxyPort.Text = userEnvironment.HttpOption.ProxyPort.ToString();
            this.ckbUseProxyAccount.Checked = userEnvironment.HttpOption.UseProxyAccount;
            this.txbProxyAccount.Text = userEnvironment.HttpOption.ProxyAccount;
            this.txbProxyPassword.Text = userEnvironment.HttpOption.ProxyPassword;
            this.ckbTraceOutput.Checked = userEnvironment.InteractiveInfo.TraceOutput;
            this.txbTraceOutput1.Text = systemEnvironment.InteractiveInfo.TraceFileName;
            this.txbTraceOutput1.Enabled = this.ckbTraceOutput.Checked;
            this.txbTraceOutput2.Text = systemEnvironment.BatchdocInfo.TraceFileName;
            this.txbTraceOutput2.Enabled = this.ckbTraceOutput.Checked;
            this.ckbAutoSendRecvMode.Checked = userEnvironment.InteractiveInfo.AutoSendRecvMode;
            this.nudAutoSendRecvTm.Enabled = this.ckbAutoSendRecvMode.Checked;
            if (userEnvironment.InteractiveInfo.AutoSendRecvTm < this.nudAutoSendRecvTm.Minimum)
            {
                this.nudAutoSendRecvTm.Value = this.nudAutoSendRecvTm.Minimum;
            }
            else
            {
                this.nudAutoSendRecvTm.Value = userEnvironment.InteractiveInfo.AutoSendRecvTm;
            }
            this.ckbJobConnect.Checked = userEnvironment.InteractiveInfo.JobConnect;
            this.gpbCertificate.Enabled = false;
            this.txbIssuerName.Clear();
            this.txbCertificateDate.Clear();
            this.ckbServer_CheckedChanged(this.ckbUseProxy, new EventArgs());
            this.ckbServer_CheckedChanged(this.ckbUseProxyAccount, new EventArgs());
            this.ckbServer_CheckedChanged(this.ckbTraceOutput, new EventArgs());
            this.ckbServer_CheckedChanged(this.ckbAutoSendRecvMode, new EventArgs());
        }

        private void LoadTerm()
        {
            this.txbTermLogicalName.Text = userEnvironment.TerminalInfo.TermLogicalName;
            this.txbTermLogicalName.Enabled = !this.IsLogOn;
            if (this.Protocol == ProtocolType.Mail)
            {
                this.lblTermAccessKey.Visible = false;
                this.txbTermAccessKey.Visible = false;
            }
            else
            {
                this.txbTermAccessKey.Text = userEnvironment.TerminalInfo.TermAccessKey;
                this.txbTermAccessKey.Enabled = !this.IsLogOn;
            }
            this.txbTermType.Text = systemEnvironment.TerminalInfo.Protocol.ToString();
            this.gpbOptionShare.Enabled = !this.IsLogOn;
            if (this.DivType == SettingDivType.User)
            {
                this.rdbNoUseShare.Checked = true;
                this.rdbUseShare.Checked = false;
                this.DivType = SettingDivType.User;
            }
            else
            {
                this.rdbNoUseShare.Checked = false;
                this.rdbUseShare.Checked = true;
                this.DivType = SettingDivType.AllUsers;
            }
            this.nudSaveTerm.Value = userEnvironment.TerminalInfo.SaveTerm;
            this.nudDiskWarning.Value = userEnvironment.TerminalInfo.DiskWarning;
            UserPathNames names = UserPathNames.CreateInstance();
            this.ckbCommonFolder.Checked = names.CommonPath.Specify;
            this.txbCommonFolder.Text = names.CommonPath.Folder;
        }

        private void LoadUserKey()
        {
            DataRow row;
            this.dtsUserKey.Clear();
            JobKeyClass class2 = userKey.JobKeyList[0];
            for (int i = 0; i < 0x18; i++)
            {
                row = this.dttUserKeyJ.NewRow();
                if (i < class2.KeyList.Count)
                {
                    KeyClass class3 = class2.KeyList[i];
                    if (class3.Sckey != "None")
                    {
                        row[2] = class3.Name;
                        row[1] = class3.Sckey;
                        int index = class3.Name.IndexOf(".");
                        if (index > -1)
                        {
                            row[3] = class3.Name.Substring(0, index);
                            row[4] = class3.Name.Substring(index + 1);
                        }
                        else
                        {
                            row[3] = class3.Name;
                        }
                    }
                }
                this.dtsUserKey.Tables[0].Rows.Add(row);
            }
            for (int j = 1; j < 4; j++)
            {
                foreach (KeyClass class4 in userKey.JobKeyList[j].KeyList)
                {
                    row = this.dtsUserKey.Tables[j].NewRow();
                    row[0] = class4.NameKJ;
                    row[2] = class4.Name;
                    if (class4.Sckey == "None")
                    {
                        row[1] = "-";
                    }
                    else
                    {
                        row[1] = class4.Sckey;
                    }
                    this.dtsUserKey.Tables[j].Rows.Add(row);
                }
            }
        }

        private void lsvBin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lsvBin.SelectedItems.Count > 0)
            {
                this.lsvBinIdx = this.lsvBin.SelectedItems[0].Index;
                this.nudMarginL.Value = this.prtInfoList[this.lsvPrtIdx].BinList[this.lsvBinIdx].L;
                this.nudMarginT.Value = this.prtInfoList[this.lsvPrtIdx].BinList[this.lsvBinIdx].T;
                foreach (ListViewItem item in this.lsvBin.Items)
                {
                    item.BackColor = SystemColors.Window;
                    item.ForeColor = SystemColors.WindowText;
                }
                this.lsvBin.Items[this.lsvBinIdx].BackColor = SystemColors.Highlight;
                this.lsvBin.Items[this.lsvBinIdx].ForeColor = SystemColors.Window;
            }
        }

        private void lsvPrt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lsvPrt.SelectedItems.Count > 0)
            {
                this.lsvPrtIdx = this.lsvPrt.SelectedItems[0].Index;
                this.lsvBinIdx = 0;
                this.ckbDoublePrt.Checked = this.prtInfoList[this.lsvPrtIdx].DoublePrint;
                this.SetFontBinList(this.prtInfoList[this.lsvPrtIdx].BinList);
                foreach (ListViewItem item in this.lsvPrt.Items)
                {
                    item.BackColor = SystemColors.Window;
                    item.ForeColor = SystemColors.WindowText;
                }
                this.lsvPrt.Items[this.lsvPrtIdx].BackColor = SystemColors.Highlight;
                this.lsvPrt.Items[this.lsvPrtIdx].ForeColor = SystemColors.Window;
                this.lsvBin.TopItem.Selected = true;
            }
        }

        private void NoUsePassword()
        {
            this.Text = Resources.ResourceManager.GetString("OptionMsg59");
            this.gpbPwdCert.Enabled = false;
            this.btnRootOK.Enabled = true;
            this.btnRootAcpt.Enabled = true;
            this.rdbPwdSet.Enabled = true;
            this.rdbPwdSet.Checked = true;
            this.rdbPwdChng.Enabled = false;
            this.rdbPwdCncl.Enabled = false;
            this.txbOldPwd.Enabled = false;
            this.txbNewPwd.Enabled = true;
            this.txbNewPwdRe.Enabled = true;
            this.tbcPassword.SelectedTab = this.tbpPwdChk;
            this.txbOldPwd.Clear();
            this.txbNewPwd.Clear();
            this.txbNewPwdRe.Clear();
            this.txbPassword.Clear();
        }

        private void nud_Validating(object sender, CancelEventArgs e)
        {
            NumericUpDown down = (NumericUpDown) sender;
            if (down.Text.Length == 0)
            {
                this.ShowModelessMessage("E540", down.Minimum.ToString() + "~" + down.Maximum.ToString(), null);
                e.Cancel = true;
            }
            if (down.Value < down.Minimum)
            {
                this.ShowModelessMessage("E540", down.Minimum.ToString() + "~" + down.Maximum.ToString(), null);
                down.Value = down.Minimum;
            }
            if (down.Value > down.Maximum)
            {
                this.ShowModelessMessage("E540", down.Minimum.ToString() + "~" + down.Maximum.ToString(), null);
                down.Value = down.Maximum;
            }
            if (((down.Name == "nudMarginL") || (down.Name == "nudMarginT")) && (this.lsvBin.Items.Count > 0))
            {
                this.prtInfoList[this.lsvPrtIdx].BinList[this.lsvBinIdx].T = (int) this.nudMarginT.Value;
                this.prtInfoList[this.lsvPrtIdx].BinList[this.lsvBinIdx].L = (int) this.nudMarginL.Value;
            }
        }

        private void OptionDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.btnRootCncl.Enabled && (e.KeyCode == Keys.Escape))
            {
                this.btnRootCncl_Click(sender, e);
            }
        }

        private void OptionDialog_Load(object sender, EventArgs e)
        {
            if (!this.isDesigning)
            {
                systemEnvironment = SystemEnvironment.CreateInstance();
                if (systemEnvironment.TerminalInfo.UserKind == 2)
                {
                    this.IsBank = true;
                }
                if (systemEnvironment.TerminalInfo.UserKind == 4)
                {
                    this.Protocol = ProtocolType.netNACCS;
                    this.tbcRoot.TabPages.Remove(this.tbpMsgCls);
                    this.tbcRoot.TabPages.Remove(this.tbpReceiveInform);
                    this.tbcRoot.TabPages.Remove(this.tbpUserKey);
                }
                else
                {
                    this.Protocol = systemEnvironment.TerminalInfo.Protocol;
                    this.tbcRoot.TabPages.Remove(this.tbpHelp);
                }
                user = User.CreateInstance();
                this.DivType = user.Application.SettingsDiv;
                this.Msdg = new MessageDialog();
                this.TabPage_Load();
                if (this.defaultTab != null)
                {
                    this.tbcRoot.SelectTab(this.defaultTab);
                }
            }
        }

        private bool OutCodeCheck(DataGridView dgv, int colidx, List<string> outcodelist)
        {
            DataGridViewRow currentRow = dgv.CurrentRow;
            if (!string.IsNullOrEmpty(currentRow.Cells[colidx].EditedFormattedValue.ToString()))
            {
                return (this.OutCodeLengthCheck(dgv, colidx) && this.OutCodeDirExistCheck(currentRow.Cells[colidx].EditedFormattedValue.ToString(), outcodelist, currentRow.Index));
            }
            outcodelist[currentRow.Index] = "";
            this.ShowModelessMessage("E567", null, null);
            return false;
        }

        private bool OutCodeDirExistCheck(string outcode, List<string> outcodelist, int rowidx)
        {
            DirectoryInfo info = new DirectoryInfo(Path.Combine(pathInfo.TemplateRoot, "Recv"));
            if (!info.Exists)
            {
                this.ShowModelessMessage("E547", null, null);
            }
            if (outcode.Length == 4)
            {
                if (info.GetDirectories(outcode.Substring(0, 4) + "*").Length > 0)
                {
                    return true;
                }
            }
            else if (outcode.Length == 6)
            {
                if (info.GetDirectories(outcode.Substring(0, 4)).Length > 0)
                {
                    return true;
                }
                if (info.GetDirectories(outcode.Substring(0, 6)).Length > 0)
                {
                    return true;
                }
            }
            else
            {
                this.ShowModelessMessage("E567", null, null);
                return false;
            }
            if (outcodelist.Contains(outcode.ToUpper()))
            {
                return true;
            }
            if (this.ShowModalMessage("W502", null, null) == DialogResult.Yes)
            {
                outcodelist[rowidx] = outcode.ToUpper();
                return true;
            }
            return false;
        }

        private bool OutCodeLengthCheck(DataGridView dgv, int colidx)
        {
            string str = "";
            str = dgv.CurrentRow.Cells[colidx].EditedFormattedValue.ToString().ToUpper();
            if (!this.Str_Decision(str, 4, StrType.IsLetterOrDigit, OpeType.EQ) && !this.Str_Decision(str, 6, StrType.IsLetterOrDigit, OpeType.EQ))
            {
                this.ShowModelessMessage("E567", null, null);
                return false;
            }
            if ((dgv != this.dgvMsgCls) && !this.OutCodeRepeatCheck(dgv, colidx))
            {
                return false;
            }
            return true;
        }

        private bool OutCodeRepeatCheck(DataGridView dgv, int colidx)
        {
            string str = dgv.CurrentRow.Cells[colidx].EditedFormattedValue.ToString().ToUpper();
            string str2 = str.Substring(0, 4);
            foreach (DataGridViewRow row in (IEnumerable) dgv.Rows)
            {
                if (row.Index != dgv.CurrentRow.Index)
                {
                    if (row.Cells[colidx].Value.ToString() == str)
                    {
                        this.ShowModelessMessage("E565", null, null);
                        return false;
                    }
                    if (str.Length == 4)
                    {
                        if (row.Cells[this.cAPOutCode.Index].Value.ToString().StartsWith(str2))
                        {
                            this.ShowModelessMessage("E571", null, null);
                            return false;
                        }
                    }
                    else if (row.Cells[this.cAPOutCode.Index].Value.ToString().StartsWith(str2) && (row.Cells[this.cAPOutCode.Index].Value.ToString().Length == 4))
                    {
                        this.ShowModelessMessage("E571", null, null);
                        return false;
                    }
                }
            }
            return true;
        }

        private bool Password_Cert()
        {
            if (this.PassWord == this.txbPassword.Text)
            {
                this.btnRootOK.Enabled = true;
                this.btnRootAcpt.Enabled = true;
                this.gpbPwdCert.Enabled = false;
                return true;
            }
            this.btnRootOK.Enabled = false;
            this.btnRootAcpt.Enabled = false;
            return false;
        }

        private void rdbPwdChng_CheckedChanged(object sender, EventArgs e)
        {
            this.txbOldPwd.Enabled = true;
            this.txbNewPwd.Enabled = true;
            this.txbNewPwdRe.Enabled = true;
        }

        private void rdbPwdCncl_CheckedChanged(object sender, EventArgs e)
        {
            this.txbOldPwd.Enabled = true;
            this.txbNewPwd.Enabled = false;
            this.txbNewPwdRe.Enabled = false;
        }

        private void rdbPwdSet_CheckedChanged(object sender, EventArgs e)
        {
            this.txbOldPwd.Enabled = false;
            this.txbNewPwd.Enabled = true;
            this.txbNewPwdRe.Enabled = true;
        }

        private void rdbShare_Click(object sender, EventArgs e)
        {
            if ((!this.rdbNoUseShare.Checked || (this.DivType != SettingDivType.User)) && (!this.rdbUseShare.Checked || (this.DivType != SettingDivType.AllUsers)))
            {
                this.btnRootCncl.Enabled = false;
                bool flag = false;
                try
                {
                    if (!this.btnRootOK.Enabled)
                    {
                        this.ShowModelessMessage("E544", null, null);
                        this.rdbUseShareChange();
                    }
                    else
                    {
                        if (this.DivType == SettingDivType.AllUsers)
                        {
                            flag = OptionCertification.IsCertificate(0);
                        }
                        else
                        {
                            flag = OptionCertification.IsCertificate(1);
                            string commonPath = PathInfo.CreateInstance().CommonPath;
                            if (commonPath.EndsWith(@"\"))
                            {
                                commonPath = Path.GetDirectoryName(commonPath);
                            }
                            commonPath = Path.GetFileName(Path.GetDirectoryName(commonPath));
                            string dirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), commonPath);
                            if (!this.DirectorySecurityChange(dirPath))
                            {
                                return;
                            }
                        }
                        if (flag)
                        {
                            this.SettingEnvChanged2();
                        }
                        else
                        {
                            this.SettingEnvChanged();
                        }
                    }
                }
                finally
                {
                    this.btnRootCncl.Enabled = true;
                }
            }
        }

        private void rdbUseShareChange()
        {
            if (this.rdbUseShare.Checked)
            {
                this.rdbUseShare.Checked = false;
                this.rdbNoUseShare.Checked = true;
                this.rdbNoUseShare.Focus();
            }
            else
            {
                this.rdbUseShare.Checked = true;
                this.rdbNoUseShare.Checked = false;
                this.rdbUseShare.Focus();
            }
        }

        private void SaveAutoPrint()
        {
            printSetups.Forms.OutCodeList.Clear();
            foreach (DataGridViewRow row in (IEnumerable) this.dgvAutoPrint.Rows)
            {
                OutCodeClass outcode = new OutCodeClass {
                    Name = row.Cells[this.cAPOutCode.Index].Value.ToString(),
                    Mode = (bool) row.Cells[this.cAPMode.Index].Value,
                    Count = int.Parse(row.Cells[this.cAPCount.Index].Value.ToString()),
                    Printer = row.Cells[this.cAPPrinter.Index].Value.ToString(),
                    BinName = row.Cells[this.cAPBinName.Index].Value.ToString(),
                    BinNo = int.Parse(row.Cells[this.cAPBinNo.Index].Value.ToString())
                };
                printSetups.Forms.OutCodeList.Add(outcode);
            }
            printSetups.DefaultInfo.Mode = this.ckbAutoPrt.Checked;
        }

        private void SaveFileSaveC()
        {
            fileSave.DetailsList.Clear();
            foreach (DataGridViewRow row in (IEnumerable) this.dgvFileSaveC.Rows)
            {
                DetailsClass details = new DetailsClass {
                    OutCode = row.Cells[this.cFSCOutCode.Index].Value.ToString(),
                    AutoSave = (bool) row.Cells[this.cFSCAutoSave.Index].Value,
                    Dir = row.Cells[this.cFSCDir.Index].Value.ToString()
                };
                fileSave.DetailsList.Add(details);
            }
        }

        private void SaveFileSaveK()
        {
            fileSave.TypeList.Clear();
            TypeClass type = new TypeClass {
                DataType = "F",
                Dir = this.txbKanriFile.Text
            };
            fileSave.TypeList.Add(type);
            foreach (DataGridViewRow row in (IEnumerable) this.dgvFileSaveK.Rows)
            {
                type = new TypeClass {
                    DataType = row.Cells[this.cFSKDataType.Index].Value.ToString(),
                    Dir = row.Cells[this.cFSKDir.Index].Value.ToString(),
                    FileSaveMode = (bool) row.Cells[this.cFSKFileSaveMode.Index].Value
                };
                fileSave.TypeList.Add(type);
            }
            fileSave.AllFileSave.Mode = this.ckbFileSave.Checked;
            string[] strArray = new string[4];
            strArray[0] = (this.cmbFileNameRule1.SelectedIndex + 1).ToString();
            if (this.cmbFileNameRule2.SelectedIndex <= 0)
            {
                strArray[1] = "9";
            }
            else
            {
                strArray[1] = this.cmbFileNameRule2.SelectedIndex.ToString();
            }
            if (this.cmbFileNameRule3.SelectedIndex <= 0)
            {
                strArray[2] = "9";
            }
            else
            {
                strArray[2] = this.cmbFileNameRule3.SelectedIndex.ToString();
            }
            if (this.cmbFileNameRule4.SelectedIndex <= 0)
            {
                strArray[3] = "9";
            }
            else
            {
                strArray[3] = this.cmbFileNameRule4.SelectedIndex.ToString();
            }
            fileSave.FileNaming.FileNameRule = string.Concat(strArray);
            fileSave.SendFile.SendUser = this.txbSendFile.Text;
        }

        private void SaveLog()
        {
            userEnvironment.LogFileLength.ComLog = (int) this.nudComLogSize.Value;
        }

        private void SaveMsgCls()
        {
            messageClassify.TermList.Clear();
            foreach (DataGridViewRow row in (IEnumerable) this.dgvMsgCls.Rows)
            {
                TermClass class2=new TermClass();
                class2 = new TermClass {
                    Id = row.Index + 1,
                    Priority = class2.Id,
                    UserCode = row.Cells[this.cMCUserCode.Index].Value.ToString(),
                    JobCoode = row.Cells[this.cMCJobCode.Index].Value.ToString(),
                    OutCode = row.Cells[this.cMCOutCode.Index].Value.ToString(),
                    BankNumber = row.Cells[this.cMCBankNumber.Index].Value.ToString(),
                    UnOpen = (bool) row.Cells[this.cMCUnOpen.Index].Value,
                    BankWithdrawday = (bool) row.Cells[this.cMCBankWithdrawday.Index].Value,
                    Folder = row.Cells[this.cMCFolder.Index].Value.ToString()
                };
                messageClassify.TermList.Add(class2);
            }
        }

        private void SavePassword()
        {
            optionCertification.AllCertification.Password = this.PassWord;
        }

        private void SavePrinter()
        {
            printSetups.DefaultInfo.Mode = this.ckbAutoPrt.Checked;
            printSetups.DefaultInfo.Printer = this.txbDefaultPrinter.Text;
            printSetups.DefaultInfo.BinNo = this.default_binno;
            printSetups.DefaultInfo.BinName = this.txbDefaultBin.Text;
            printSetups.DefaultInfo.SizeWarning = this.chbSizeWarning.Checked;
            printSetups.PrinterInfoList.Clear();
            foreach (PrinterInfoClass class2 in this.prtInfoList)
            {
                printSetups.PrinterInfoList.Add(class2);
            }
        }

        private void SaveReceiveInform()
        {
            receiveNotice.ReceiveInformList.Clear();
            foreach (DataGridViewRow row in (IEnumerable) this.dgvReceiveInform.Rows)
            {
                ReceiveInformClass receiveinform = new ReceiveInformClass {
                    OutCode = row.Cells[this.cRNOutCode.Index].Value.ToString(),
                    Inform = (bool) row.Cells[this.cRNInform.Index].Value,
                    InformSound = (bool) row.Cells[this.cRNInformSound.Index].Value
                };
                receiveNotice.ReceiveInformList.Add(receiveinform);
            }
            receiveNotice.Inform.InfoMode = this.ckbRcvInform.Checked;
            this.gpbReceiveNotice.Enabled = this.ckbRcvInform.Checked;
            this.btnSoundFile.Enabled = this.ckbRcvInform.Checked;
            receiveNotice.Sound.SoundFile = this.txbSoundFile.Text;
            receiveNotice.WarningInform.WaringMode = this.ckbRcvInformSnd.Checked;
        }

        private void SaveServer()
        {
            if (this.Protocol != ProtocolType.Mail)
            {
                userEnvironment.InteractiveInfo.ServerConnect = systemEnvironment.InteractiveInfo.Server[this.cmbServerConnect.SelectedIndex].name;
                if ((this.Protocol == ProtocolType.netNACCS) && systemEnvironment.TerminalInfo.UseInternet)
                {
                    userEnvironment.HttpOption.CertificateHash = this.CertHash;
                }
                userEnvironment.HttpOption.UseProxy = this.ckbUseProxy.Checked;
                userEnvironment.HttpOption.ProxyName = this.txbProxyName.Text;
                if (this.Str_Decision(this.txbProxyPort.Text, this.txbProxyPort.MaxLength, StrType.IsDigit, OpeType.GE))
                {
                    userEnvironment.HttpOption.ProxyPort = int.Parse(this.txbProxyPort.Text);
                }
                else
                {
                    userEnvironment.HttpOption.ProxyPort = 0;
                }
                userEnvironment.HttpOption.UseProxyAccount = this.ckbUseProxyAccount.Checked;
                userEnvironment.HttpOption.ProxyAccount = this.txbProxyAccount.Text;
                userEnvironment.HttpOption.ProxyPassword = this.txbProxyPassword.Text;
                userEnvironment.InteractiveInfo.TraceOutput = this.ckbTraceOutput.Checked;
                userEnvironment.InteractiveInfo.AutoSendRecvMode = this.ckbAutoSendRecvMode.Checked;
                userEnvironment.InteractiveInfo.AutoSendRecvTm = (int) this.nudAutoSendRecvTm.Value;
                userEnvironment.InteractiveInfo.JobConnect = this.ckbJobConnect.Checked;
            }
            else
            {
                userEnvironment.MailInfo.ServerConnect = systemEnvironment.MailInfo.Server[this.cmbServerConnectM.SelectedIndex].name;
                userEnvironment.MailInfo.TraceOutput = this.ckbTraceOutputM.Checked;
                userEnvironment.HttpOption.UseProxy = this.ckbUseProxyM.Checked;
                userEnvironment.HttpOption.ProxyName = this.txbProxyNameM.Text;
                if (this.Str_Decision(this.txbProxyPortM.Text, this.txbProxyPortM.MaxLength, StrType.IsDigit, OpeType.GE))
                {
                    userEnvironment.HttpOption.ProxyPort = int.Parse(this.txbProxyPortM.Text);
                }
                else
                {
                    userEnvironment.HttpOption.ProxyPort = 0;
                }
                userEnvironment.HttpOption.UseProxyAccount = this.ckbUseProxyAccountM.Checked;
                userEnvironment.HttpOption.ProxyAccount = this.txbProxyAccountM.Text;
                userEnvironment.HttpOption.ProxyPassword = this.txbProxyPasswordM.Text;
                userEnvironment.MailInfo.AutoMode = this.ckbUseAutoSndRcvTm.Checked;
                userEnvironment.MailInfo.AutoTm = (int) this.nudAutoSndRcvTm.Value;
                if (this.rdbAutoTmKindSR.Checked)
                {
                    userEnvironment.MailInfo.AutoTmKind = 1;
                }
                if (this.rdbAutoTmKindS.Checked)
                {
                    userEnvironment.MailInfo.AutoTmKind = 2;
                }
                if (this.rdbAutoTmKindR.Checked)
                {
                    userEnvironment.MailInfo.AutoTmKind = 3;
                }
                userEnvironment.MailInfo.MaxRetreiveCnt = (int) this.nudMaxRetreiveCnt.Value;
                userEnvironment.MailInfo.UseGateway = this.ckbUseGateway.Checked;
                userEnvironment.MailInfo.GatewaySMTPServerName = this.gsc.GatewaySMTPServerName;
                userEnvironment.MailInfo.GatewaySMTPServerPort = this.gsc.GatewaySMTPServerPort;
                userEnvironment.MailInfo.GatewayPOPServerName = this.gsc.GatewayPOPServerName;
                userEnvironment.MailInfo.GatewayPOPServerPort = this.gsc.GatewayPOPServerPort;
                userEnvironment.MailInfo.GatewaySMTPDomainName = this.gsc.GatewaySMTPDomainName;
                userEnvironment.MailInfo.GatewayPOPDomainName = this.gsc.GatewayPOPDomainName;
                userEnvironment.MailInfo.GatewayMailBox = this.gsc.GatewayMailBox;
                userEnvironment.MailInfo.GatewayAddDomainName = this.gsc.GatewayAddDomainName;
            }
        }

        private void SaveTerm()
        {
            userEnvironment.TerminalInfo.TermLogicalName = this.txbTermLogicalName.Text;
            if (this.Protocol == ProtocolType.Mail)
            {
                userEnvironment.TerminalInfo.TermAccessKey = null;
            }
            else
            {
                userEnvironment.TerminalInfo.TermAccessKey = this.txbTermAccessKey.Text;
            }
            userEnvironment.TerminalInfo.SaveTerm = (int) this.nudSaveTerm.Value;
            userEnvironment.TerminalInfo.DiskWarning = (int) this.nudDiskWarning.Value;
            messageClassify.DataView.AutoBackup = this.ckbUseAutoBackup.Checked;
            user.Application.SettingsDiv = this.DivType;
        }

        private void SaveUserKey()
        {
            KeyClass class3;
            JobKeyClass item = new JobKeyClass();
            userKey.JobKeyList.Clear();
            foreach (DataGridViewRow row in (IEnumerable) this.dgvUserKeyJ.Rows)
            {
                class3 = new KeyClass();
                if (string.IsNullOrEmpty(row.Cells[this.cKJWindowCode.Index].Value.ToString()))
                {
                    row.Cells[this.cKJName.Index].Value = row.Cells[this.cKJGymCode.Index].Value.ToString();
                }
                else
                {
                    row.Cells[this.cKJName.Index].Value = row.Cells[this.cKJGymCode.Index].Value.ToString() + "." + row.Cells[this.cKJWindowCode.Index].Value.ToString();
                }
                if (!string.IsNullOrEmpty(row.Cells[this.cKJName.Index].Value.ToString()))
                {
                    class3.Name = row.Cells[this.cKJName.Index].Value.ToString();
                    if ((row.Cells[this.cKJSckey.Index].Value.ToString() != "-") && (row.Cells[this.cKJSckey.Index].Value.ToString() != ""))
                    {
                        class3.Sckey = row.Cells[this.cKJSckey.Index].Value.ToString();
                    }
                    else
                    {
                        class3.Sckey = "";
                    }
                    item.KeyList.Add(class3);
                }
            }
            item.Kind = 0;
            userKey.JobKeyList.Add(item);
            for (int i = 1; i < 4; i++)
            {
                item = new JobKeyClass();
                foreach (DataRow row2 in this.dtsUserKey.Tables[i].Rows)
                {
                    class3 = new KeyClass();
                    if (!string.IsNullOrEmpty(row2.ItemArray[2].ToString()))
                    {
                        class3.Name = row2.ItemArray[2].ToString();
                        class3.NameKJ = row2.ItemArray[0].ToString();
                        if ((row2.ItemArray[1].ToString() != "-") && (row2.ItemArray[1].ToString() != ""))
                        {
                            class3.Sckey = row2.ItemArray[1].ToString();
                        }
                        else
                        {
                            class3.Sckey = "";
                        }
                        item.KeyList.Add(class3);
                    }
                }
                item.Kind = i;
                userKey.JobKeyList.Add(item);
            }
        }

        private void SetFontBinList(BinListClass blc)
        {
            this.lsvBin.Items.Clear();
            for (int i = 0; i < blc.Count; i++)
            {
                if (blc[i].Name != null)
                {
                    this.lsvBin.Items.Add(blc[i].Name);
                    if (string.IsNullOrEmpty(blc[i].Size) && string.IsNullOrEmpty(blc[i].Orientation))
                    {
                        this.lsvBin.Items[i].Font = new Font(this.lsvBin.Items[i].Font, this.lsvBin.Items[i].Font.Style);
                    }
                    else
                    {
                        this.lsvBin.Items[i].Font = new Font(this.lsvBin.Items[i].Font, this.lsvBin.Items[i].Font.Style | FontStyle.Bold);
                    }
                }
            }
            this.lsvBin.Items[this.lsvBinIdx].Selected = true;
            this.lsvBin.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void SettingEnvChanged()
        {
            DialogResult result = this.ShowModalMessage("C501", null, null);
            if (result == DialogResult.Cancel)
            {
                this.rdbUseShareChange();
            }
            else if (!this.FormClosing_Check())
            {
                this.rdbUseShareChange();
            }
            else if (!this.TabPage_Save())
            {
                this.ShowModelessMessage("E545", null, null);
                this.rdbUseShareChange();
            }
            else
            {
                if (this.rdbUseShare.Checked)
                {
                    this.DivType = SettingDivType.AllUsers;
                }
                else
                {
                    this.DivType = SettingDivType.User;
                }
                if (result == DialogResult.Yes)
                {
                    this.DlgRes_Yes();
                }
                else
                {
                    this.DlgRes_No();
                }
            }
        }

        private void SettingEnvChanged2()
        {
            if (this.ShowModalMessage("C504", null, null) == DialogResult.No)
            {
                this.rdbUseShareChange();
            }
            else if (!this.FormClosing_Check())
            {
                this.rdbUseShareChange();
            }
            else if (!this.TabPage_Save())
            {
                this.ShowModelessMessage("E581", null, null);
                this.rdbUseShareChange();
            }
            else
            {
                if (this.rdbUseShare.Checked)
                {
                    this.DivType = SettingDivType.AllUsers;
                }
                else
                {
                    this.DivType = SettingDivType.User;
                }
                this.DlgRes_No();
            }
        }

        private bool ShortcutKeyRepeatCheck(DataGridView dgv, int colidx)
        {
            string str = dgv.CurrentRow.Cells[colidx].EditedFormattedValue.ToString();
            if ((str != "-") && !string.IsNullOrEmpty(str))
            {
                foreach (DataGridViewRow row2 in (IEnumerable) dgv.Rows)
                {
                    if ((row2.Index != dgv.CurrentRow.Index) && (row2.Cells[colidx].Value.ToString() == str))
                    {
                        this.ShowModelessMessage("E560", null, null);
                        return false;
                    }
                }
                switch (dgv.Name)
                {
                    case "dgvUserKeyJ":
                        if (!this.dgvUserKey_GetKey(this.dgvUserKeyS, str) || !this.dgvUserKey_GetKey(this.dgvUserKeyR, str))
                        {
                            return false;
                        }
                        break;

                    case "dgvUserKeyS":
                        if (this.dgvUserKey_GetKey(this.dgvUserKeyJ, str))
                        {
                            if (!this.dgvUserKey_GetKey(this.dgvUserKeyR, str))
                            {
                                return false;
                            }
                            if (this.dgvUserKey_GetKey(this.dgvUserKeyG, str))
                            {
                                break;
                            }
                        }
                        return false;

                    case "dgvUserKeyR":
                        if (!this.dgvUserKey_GetKey(this.dgvUserKeyS, str) || !this.dgvUserKey_GetKey(this.dgvUserKeyJ, str))
                        {
                            return false;
                        }
                        break;

                    default:
                        if (!this.dgvUserKey_GetKey(this.dgvUserKeyS, str))
                        {
                            return false;
                        }
                        break;
                }
            }
            return true;
        }

        private void ShowGridErrDialog(string msg)
        {
            if (this.Gdlg != null)
            {
                this.Gdlg.Dispose();
            }
            this.Gdlg = new GridErrDialog();
            this.Gdlg.Message = msg;
            base.Enabled = false;
            this.Gdlg.OnCellError += new OnCellErrorHandler(this.GridError);
            this.Gdlg.Show();
        }

        private DialogResult ShowModalMessage(string msgCode, string applyStr, string message)
        {
            DialogResult result;
            try
            {
                base.Enabled = false;
                MessageDialog dialog = new MessageDialog();
                dialog.FormClosing += new FormClosingEventHandler(this.dialog_FormClosing);
                result = dialog.ShowMessage(msgCode, applyStr, message);
            }
            catch (Exception exception)
            {
                base.Enabled = true;
                throw exception;
            }
            return result;
        }

        private void ShowModelessMessage(string msgCode, string applyStr, string message)
        {
            try
            {
                base.Enabled = false;
                MessageDialog dialog = new MessageDialog();
                dialog.FormClosing += new FormClosingEventHandler(this.dialog_FormClosing);
                dialog.ShowModelessMessageDialog(msgCode, applyStr, message);
            }
            catch (Exception exception)
            {
                base.Enabled = true;
                throw exception;
            }
        }

        private bool Str_Decision(string str, int len, StrType st, OpeType ot)
        {
            switch (ot)
            {
                case OpeType.EQ:
                    if (str.Length == len)
                    {
                        break;
                    }
                    return false;

                case OpeType.GE:
                    if ((str.Length != 0) && (str.Length <= len))
                    {
                        break;
                    }
                    return false;

                case OpeType.GT:
                    if (str.Length >= len)
                    {
                        break;
                    }
                    return false;

                case OpeType.LE:
                    if ((str.Length != 0) && (str.Length < len))
                    {
                        break;
                    }
                    return false;

                case OpeType.LT:
                    if (str.Length > len)
                    {
                        break;
                    }
                    return false;
            }
            char[] chArray = str.ToUpper().ToCharArray();
            switch (st)
            {
                case StrType.IsDigit:
                    foreach (char ch in chArray)
                    {
                        if ((ch < '0') || (ch > '9'))
                        {
                            return false;
                        }
                    }
                    break;

                case StrType.IsLetter:
                    foreach (char ch2 in chArray)
                    {
                        if ((ch2 < 'A') || (ch2 > 'Z'))
                        {
                            return false;
                        }
                    }
                    break;

                case StrType.IsLetterOrDigit:
                    foreach (char ch3 in chArray)
                    {
                        if (((ch3 < '0') || (ch3 > '9')) && ((ch3 < 'A') || (ch3 > 'Z')))
                        {
                            return false;
                        }
                    }
                    break;

                case StrType.IsSymbol:
                    foreach (char ch4 in chArray)
                    {
                        if ((ch4 < ' ') || (ch4 > '\x007f'))
                        {
                            return false;
                        }
                    }
                    break;

                case StrType.IsNACCS:
                    foreach (char ch5 in chArray)
                    {
                        if ((ch5 < ' ') || (ch5 > ']'))
                        {
                            return false;
                        }
                        if (((ch5 == '$') || (ch5 == '%')) || ((ch5 == '\'') || (ch5 == '*')))
                        {
                            return false;
                        }
                    }
                    break;
            }
            return true;
        }

        protected virtual void TabPage_Load()
        {
            ConfigFiles.AllLoad();
            pathInfo = ConfigFiles.pathInfo;
            user = ConfigFiles.user;
            userKey = ConfigFiles.userKey;
            this.saveUserKey = ConfigFiles.userKey;
            printSetups = ConfigFiles.printerSettings;
            userEnvironment = ConfigFiles.userEnvironment;
            messageClassify = ConfigFiles.messageClassify;
            fileSave = ConfigFiles.fileSave;
            receiveNotice = ConfigFiles.receiveNotice;
            helpSettings = ConfigFiles.helpSettings;
            optionCertification = ConfigFiles.optionCertification;
            gStamp = ConfigFiles.gStamp;
            this.LoadTerm();
            this.LoadServer();
            this.LoadPrinter();
            this.LoadAutoPrint();
            this.LoadMsgCls();
            this.LoadFileSaveK();
            this.LoadFileSaveC();
            this.LoadReceiveInform();
            this.LoadUserKey();
            this.LoadLog();
            this.LoadHelp();
            this.LoadPassword();
        }

        private bool TabPage_Save()
        {
            this.Cursor = Cursors.WaitCursor;
            this.SaveTerm();
            this.SaveServer();
            this.SavePrinter();
            this.SaveAutoPrint();
            this.SaveMsgCls();
            this.SaveFileSaveK();
            this.SaveFileSaveC();
            this.SaveReceiveInform();
            this.SaveUserKey();
            this.SaveLog();
            this.SavePassword();
            optionCertification.UpdateFlg = true;
            if (ConfigFiles.AllSave())
            {
                this.Cursor = Cursors.Default;
                return false;
            }
            this.IsUpdateFlg = true;
            this.Cursor = Cursors.Default;
            return true;
        }

        private void tbpPrinter_Refresh()
        {
            if (this.prtInfoList.Count > 0)
            {
                this.lsvPrt.Items.Clear();
                foreach (PrinterInfoClass class2 in this.prtInfoList)
                {
                    this.lsvPrt.Items.Add(class2.Name);
                }
                this.lsvPrtIdx = 0;
                this.lsvBinIdx = 0;
                this.SetFontBinList(this.prtInfoList[this.lsvPrtIdx].BinList);
                this.lsvPrt.TopItem.Selected = true;
                this.lsvBin.TopItem.Selected = true;
                this.lsvPrt.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
        }

        private void tlsMCbtnDown_Click(object sender, EventArgs e)
        {
            if (this.dgvMsgCls_UpDownCheck())
            {
                this.dgvMsgCls.EndEdit();
                if ((this.dgvMsgCls.CurrentCellAddress.Y + 1) < this.dgvMsgCls.Rows.Count)
                {
                    this.dgvMsgCls_RowDataChange(this.dgvMsgCls.CurrentCellAddress.Y, this.dgvMsgCls.CurrentCellAddress.Y + 1);
                    this.dgvMsgCls.Rows[this.dgvMsgCls.CurrentCellAddress.Y + 1].Cells[this.dgvMsgCls.CurrentCellAddress.X].Selected = true;
                }
            }
        }

        private void tlsMCbtnUp_Click(object sender, EventArgs e)
        {
            if (this.dgvMsgCls_UpDownCheck())
            {
                this.dgvMsgCls.EndEdit();
                if (this.dgvMsgCls.CurrentCellAddress.Y > 0)
                {
                    this.dgvMsgCls_RowDataChange(this.dgvMsgCls.CurrentCellAddress.Y, this.dgvMsgCls.CurrentCellAddress.Y - 1);
                    this.dgvMsgCls.Rows[this.dgvMsgCls.CurrentCellAddress.Y - 1].Cells[this.dgvMsgCls.CurrentCellAddress.X].Selected = true;
                }
            }
        }

        private void UsePassword()
        {
            this.Text = Resources.ResourceManager.GetString("OptionMsg58");
            this.gpbPwdCert.Enabled = true;
            this.btnRootOK.Enabled = false;
            this.btnRootAcpt.Enabled = false;
            this.rdbPwdSet.Enabled = false;
            this.rdbPwdChng.Enabled = true;
            this.rdbPwdCncl.Enabled = true;
            this.rdbPwdCncl.Checked = true;
            this.tbcPassword.SelectedTab = this.tbpPwdChk;
            this.txbOldPwd.Clear();
            this.txbNewPwd.Clear();
            this.txbNewPwdRe.Clear();
            this.txbPassword.Clear();
        }

        public TabPage DefaultTab
        {
            get
            {
                return this.defaultTab;
            }
            set
            {
                this.defaultTab = value;
            }
        }

        public bool IsLogOn
        {
            get
            {
                return this.isLogon;
            }
            set
            {
                this.isLogon = value;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct ErrInfo
        {
            public int err_colidx;
            public bool err_result;
            public static readonly OptionDialog.ErrInfo errInfo;
            public ErrInfo(int e_cidx, bool e_result)
            {
                this.err_colidx = e_cidx;
                this.err_result = e_result;
            }

            static ErrInfo()
            {
                errInfo = new OptionDialog.ErrInfo(0, true);
            }
        }

        private enum OpeType
        {
            EQ,
            GE,
            GT,
            LE,
            LT
        }

        private enum StrType
        {
            IsDigit,
            IsLetter,
            IsLetterOrDigit,
            IsSymbol,
            IsNACCS
        }

        private enum UserKeyIndex
        {
            NameKj,
            Sckey,
            Name,
            GymCode,
            WindowCode
        }
    }
}

