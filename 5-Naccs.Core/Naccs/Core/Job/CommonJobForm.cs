namespace Naccs.Core.Job
{
    using Naccs.Common.CustomControls;
    using Naccs.Common.Function;
    using Naccs.Common.Generator;
    using Naccs.Common.JobConfig;
    using Naccs.Core.Classes;
    using Naccs.Core.Print;
    using Naccs.Core.Properties;
    using Naccs.Core.Settings;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;
    using System.Deployment.Application;

    [DisplayName("共通業務画面"), ToolboxBitmap(typeof(Form))]
    public class CommonJobForm : Form
    {
        protected string _style;
        private bool boolAttach;
        private bool boolSendGuard;
        private ToolStripButton btnAppendJobData;
        private ToolStripButton btnAttachAppend;
        protected ToolStripButton btnCopy;
        protected ToolStripButton btnCut;
        protected ToolStripButton btnFileOpen;
        protected ToolStripButton btnFileSaveAs;
        private ToolStripButton btnGuidance;
        protected ToolStripButton btnPaste;
        protected ToolStripButton btnPrintJobData;
        private ToolStripButton btnSend;
        protected ToolStripButton btnUndo;
        private DataGridViewTextBoxColumn clCode;
        private DataGridViewTextBoxColumn clContents;
        private DataGridViewTextBoxColumn clDispose;
        private DataGridViewTextBoxColumn clID;
        private DataGridViewTextBoxColumn clJobCode;
        private DataGridViewTextBoxColumn clJobNo;
        private DataGridViewTextBoxColumn clNo;
        protected ColumnHeader colFile;
        private DataGridViewImageColumn colIcon;
        protected ColumnHeader colSize;
        private IContainer components;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private XmlDocument docGuide;
        protected IData ExNaccsData;
        protected bool flgDoCheckErr = true;
        protected bool flgFieldClearMsg;
        protected bool flgWarningBeep;
        private ModeFont fMode;
        protected GroupBox gbAttach;
        protected GroupBox gbGuide;
        protected GroupBox gbInputInfo;
        protected GroupBox gbResult;
        protected DataGridView gvResult;
        protected bool isDesigning = ((AppDomain.CurrentDomain.FriendlyName == "DefaultDomain") && !ApplicationDeployment.IsNetworkDeployed);
        protected AbstractJobConfig JobConfig;
        private ImageList JobImageList;
        protected XmlElement LinkElement;
        protected ListView lvAttach;
        public MenuStrip MainMenu;
        protected ToolStripMenuItem mnAllCheck;
        protected ToolStripMenuItem mnAllCheckOut;
        protected ToolStripMenuItem mnAllClear;
        protected ToolStripMenuItem mnAppendJobData;
        private ToolStripMenuItem mnAttach;
        private ToolStripMenuItem mnAttachAppend;
        private ToolStripMenuItem mnAttachDelete;
        private ToolStripMenuItem mnAttachOpen;
        private ToolStripMenuItem mnChangeActivePage;
        private ToolStripMenuItem mnClear;
        protected ToolStripMenuItem mnClearCurrentToEnd;
        protected ToolStripMenuItem mnCommonClear;
        protected ToolStripMenuItem mnCopy;
        protected ToolStripMenuItem mnCSV;
        protected ToolStripMenuItem mnCsvCommon;
        protected ToolStripMenuItem mnCsvRan;
        protected ToolStripMenuItem mnCut;
        protected ToolStripMenuItem mnDefaultSize;
        protected ToolStripMenuItem mnEdit;
        protected ToolStripMenuItem mnFile;
        protected ToolStripMenuItem mnFileOpen;
        protected ToolStripMenuItem mnFileSaveAs;
        protected ToolStripMenuItem mnFileSend;
        protected ToolStripMenuItem mnGridInsert;
        protected ToolStripMenuItem mnGridPaste;
        protected ToolStripMenuItem mnGridRemove;
        private ToolStripMenuItem mnGuidance;
        private ToolStripMenuItem mnHardCopy;
        private ToolStripMenuItem mnHardCopyPreview;
        private ToolStripMenuItem mnHardCpy;
        private ToolStripMenuItem mnHelp;
        protected ToolStripMenuItem mnJob;
        private ToolStripMenuItem mnJobClose;
        private ToolStripMenuItem mnJobNew;
        private ToolStripMenuItem mnOption;
        protected ToolStripMenuItem mnPaste;
        protected ToolStripMenuItem mnPrintJobData;
        protected ToolStripMenuItem mnPrintJobDataPreview;
        private ToolStripMenuItem mnRedo;
        protected ToolStripMenuItem mnRowCopy;
        protected ToolStripMenuItem mnRowPaste;
        protected ToolStripMenuItem mnSelectRanClear;
        protected ToolStripMenuItem mnSend;
        private ToolStripMenuItem mnSendGuardOFF;
        private ToolStripSeparator mnSep1;
        private ToolStripSeparator mnSep10;
        private ToolStripSeparator mnSep11;
        private ToolStripSeparator mnSep12;
        private ToolStripSeparator mnSep13;
        private ToolStripSeparator mnSep14;
        private ToolStripSeparator mnSep15;
        private ToolStripSeparator mnSep16;
        private ToolStripSeparator mnSep17;
        private ToolStripSeparator mnSep2;
        private ToolStripSeparator mnSep3;
        private ToolStripSeparator mnSep4;
        private ToolStripSeparator mnSep5;
        private ToolStripSeparator mnSep6;
        private ToolStripSeparator mnSep7;
        private ToolStripSeparator mnSep8;
        private ToolStripSeparator mnSep9;
        protected ToolStripMenuItem mnShowHint;
        private ToolStripMenuItem mnSystem;
        protected ToolStripMenuItem mnUndo;
        protected ToolStripMenuItem mnUpdateJobData;
        protected ToolStripMenuItem mnView;
        protected ToolStripMenuItem mnZoomIn;
        protected ToolStripMenuItem mnZoomOut;
        protected OpenFileDialog OpenFileDlg;
        protected ToolStripMenuItem pmnAllCheck;
        protected ToolStripMenuItem pmnAllCheckOut;
        protected ToolStripMenuItem pmnAllClear;
        protected ToolStripMenuItem pmnClear;
        protected ToolStripMenuItem pmnClearCurrentToEnd;
        protected ToolStripMenuItem pmnCommonClear;
        protected ToolStripMenuItem pmnCopy;
        protected ToolStripMenuItem pmnCut;
        protected ToolStripMenuItem pmnGridInsert;
        protected ToolStripMenuItem pmnGridPaste;
        protected ToolStripMenuItem pmnGridRemove;
        protected ToolStripMenuItem pmnJobLink;
        protected ToolStripMenuItem pmnPaste;
        protected ToolStripMenuItem pmnRowCopy;
        protected ToolStripMenuItem pmnRowPaste;
        protected ToolStripMenuItem pmnSelectRanClear;
        private ContextMenuStrip pmnSendGuardOFF;
        protected ToolStripMenuItem pmnShowHint;
        protected ToolStripMenuItem pmnUndo;
        private Panel pnlCommon;
        private Panel pnlGuide;
        protected Panel pnlGuiLayout;
        private Panel pnlHeaderInfo;
        private Panel pnlResultCode;
        protected ContextMenuStrip PopMenu;
        protected ImageList ResImageList;
        protected RichTextBox rtxtGuide;
        protected SaveFileDialog SaveFileDlg;
        private Icon SendGuardIcon;
        private ToolStripMenuItem smnSendGuardOFF;
        private Splitter spHorizon1;
        private Splitter spHorizon2;
        private Splitter spHorizon3;
        private Splitter spVertical;
        protected string stAttachPath;
        protected StatusBar statusBar;
        protected string stCorrelationFileName;
        protected string stLinkFileName;
        protected StatusBarPanel stPnlSendGuard;
        protected StatusBarPanel stPnlUser;
        protected char SystemMode = '2';
        private System.Windows.Forms.Timer tmSetResField;
        private ToolStripSeparator toolSep1;
        private ToolStripSeparator toolSep2;
        private ToolStripSeparator toolSep3;
        private ToolStripSeparator toolSep4;
        private ToolStripSeparator toolSep5;
        private ToolStripSeparator toolSep6;
        private System.Windows.Forms.ToolStrip ToolStrip;
        protected LBTextBox txtInputInfo;

        public event JobDemHandler OnAppend;

        public event JobLinkHandler OnCreateLinkForm;

        public event JobDemHandler OnCreateNewForm;

        public event JobHandler OnOpenJobForm;

        public event JobHandler OnOptionShow;

        public event JobHandler OnRepeatSend;

        public event JobHandler OnRepetInput;

        public event JobDemHandler OnSend;

        public event JobHandler OnSeqLoad;

        public CommonJobForm()
        {
            this.DoubleBuffered = true;
            base.SuspendLayout();
            this.InitializeComponent();
            this.txtInputInfo.Enter += new EventHandler(this.ShowGuide);
            this.lvAttach.Enter += new EventHandler(this.ShowGuide);
            this.gvResult.Enter += new EventHandler(this.ShowGuide);
            this.rtxtGuide.LanguageOption = RichTextBoxLanguageOptions.UIFonts;
            this.ExNaccsData = new NaccsData();
            this.LinkElement = null;
            this.SendGuardIcon = (Icon) this.statusBar.Panels[1].Icon.Clone();
            this.statusBar.Panels[1].Icon = null;
            if (!this.isDesigning)
            {
                User user = User.CreateInstance();
                this.flgFieldClearMsg = user.JobOption.QueryFieldClear;
                SystemEnvironment environment = SystemEnvironment.CreateInstance();
                UserEnvironment environment2 = UserEnvironment.CreateInstance();
                this.flgDoCheckErr = !environment.TerminalInfo.Debug || environment2.DebugFunction.Check_Off;
                this.mnHelp.Visible = environment.TerminalInfo.Demo;
                this.mnGuidance.Visible = environment.TerminalInfo.Demo;
                this.toolSep5.Visible = environment.TerminalInfo.Demo;
                this.btnGuidance.Visible = environment.TerminalInfo.Demo;
                new CustomizeMenu().VisibleJob(this.MainMenu);
                this.btnAppendJobData.Visible = environment.TerminalInfo.UserKind != 4;
                this.toolSep4.Visible = environment.TerminalInfo.UserKind != 4;
                ReceiveNotice notice = ReceiveNotice.CreateInstance();
                this.flgWarningBeep = notice.WarningInform.WaringMode;
                UserKeySet set = new UserKeySet();
                set.ShortCutSet(this.MainMenu.Items);
                set.ShortCutSet(this.PopMenu.Items);
                JobSettings settings = JobSettings.CreateInstance();
                this.SystemMode = settings.SysMode;
            }
            base.Disposed += new EventHandler(this.CommonJobForm_Disposed);
            base.ResumeLayout();
        }

        protected virtual void AcceptDataChange()
        {
        }

        protected virtual void AddCsvData(List<string> stList, string tblName)
        {
        }

        protected virtual void AddSendData(IData data, bool flgClear)
        {
        }

        private bool AppendAttachData(bool flgAppend)
        {
            string path = null;
            try
            {
                PathInfo info = PathInfo.CreateInstance();
                if (!flgAppend && (this.ExNaccsData.AttachFolder != null))
                {
                    path = this.ExNaccsData.AttachFolder;
                    Directory.Delete(path, true);
                    CopyDirectory(this.stAttachPath, path);
                }
                else
                {
                    path = info.AttachPath + this.ExNaccsData.JobCode.Trim() + this.ExNaccsData.DispCode.Trim() + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    CopyDirectory(this.stAttachPath, path);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Failure in deleting/copying the attachment folder.(CommonJobForm.AppendAttachData)");
                Console.WriteLine(this.stAttachPath);
                Console.WriteLine(path);
                string message = Resources.ResourceManager.GetString("CORE68") + MessageDialog.CreateExceptionMessage(exception);
                using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                {
                    form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, message);
                }
                return false;
            }
            this.ExNaccsData.AttachFolder = path;
            this.ExNaccsData.AttachFile = new string[this.lvAttach.Items.Count];
            for (int i = 0; i < this.lvAttach.Items.Count; i++)
            {
                this.ExNaccsData.AttachFile[i] = this.lvAttach.Items[i].Text;
            }
            this.ExNaccsData.AttachCount = this.lvAttach.Items.Count;
            return true;
        }

        public string AppendJobData()
        {
            IData data = null;
            string str = "";
            data = this.CmnAppendJob(true);
            if (data == null)
            {
                return "";
            }
            if (this.OnAppend != null)
            {
                str = this.OnAppend(this, data);
                this.ExNaccsData.ID = str;
                this.ExNaccsData.Status = DataStatus.Send;
            }
            return str;
        }

        protected virtual bool CheckError(bool flgCheckReq)
        {
            if (!StrFunc.CreateInstance().InputInfoCharCheck(this.txtInputInfo.Text))
            {
                MessageDialog dialog = new MessageDialog();
                dialog.ShowMessage("E516", Resources.ResourceManager.GetString("MSG_FORBIDDENCHAR_ERROR"), "");
                dialog.Dispose();
                this.gbInputInfo.Focus();
                this.txtInputInfo.Focus();
                return false;
            }
            return true;
        }

        protected bool ChoiceSetJobForm(IData Data)
        {
            DialogResult result;
            using (MessageDialog dialog = new MessageDialog())
            {
                result = dialog.ShowMessage("W504", null, null);
            }
            switch (result)
            {
                case DialogResult.Yes:
                    this.SendCreateNewFormMsg(Data);
                    return false;

                case DialogResult.No:
                    return true;
            }
            return false;
        }

        protected virtual void ClearResErr()
        {
            this.gvResult.Rows.Clear();
        }

        private IData CmnAppendJob(bool flgAppend)
        {
            IData data = null;
            DivideConfig config = null;
            NormalConfig jobConfig = null;
            CombiConfig config3 = null;
            XmlNode node;
            bool flag = true;
            if (this.JobConfig.jobStyle == JobStyle.normal)
            {
                jobConfig = (NormalConfig) this.JobConfig;
                if ((jobConfig.record.jobcode == "") || (jobConfig.record.jobcode == null))
                {
                    flag = false;
                }
            }
            else if (this.JobConfig.jobStyle == JobStyle.divide)
            {
                config = (DivideConfig) this.JobConfig;
                if ((config.record.jobcode == "") || (config.record.jobcode == null))
                {
                    flag = false;
                }
            }
            else
            {
                config3 = (CombiConfig) this.JobConfig;
                if ((config3.jobcode == "") || (config3.jobcode == null))
                {
                    flag = false;
                }
            }
            if (!flag)
            {
                MessageDialog dialog = new MessageDialog();
                dialog.ShowMessage("E517", "");
                dialog.Dispose();
                return null;
            }
            if (this.BoolSendGuard)
            {
                MessageDialog dialog2 = new MessageDialog();
                dialog2.ShowMessage("E515", "");
                dialog2.Dispose();
                return null;
            }
            if (!this.CheckError(true))
            {
                return null;
            }
            data = this.MakeExNaccsData(Naccs.Common.Generator.ItemInfo.JobDataStatus.jdSend);
            data.ID = this.ExNaccsData.ID;
            if ((this.ExNaccsData.Status == DataStatus.Recept) || (this.ExNaccsData.Status == DataStatus.Sent))
            {
                data.ID = null;
            }
            if (flgAppend && (this.ExNaccsData.Status == DataStatus.Send))
            {
                data.ID = null;
            }
            this.JobConfig.selectRecord(DateTime.Now, out node);
            if (node != null)
            {
                node = node.Attributes["sign-flg"];
                if (node != null)
                {
                    data.Signflg = int.Parse(node.Value);
                }
            }
            if (this.BoolAttach && this.IsAttach(data.JobCode.TrimEnd(new char[0]), data.DispCode.TrimEnd(new char[0])))
            {
                if (!this.AppendAttachData(data.ID == null))
                {
                    return null;
                }
                data.AttachFolder = this.ExNaccsData.AttachFolder;
                data.AttachFile = new string[this.lvAttach.Items.Count];
                for (int i = 0; i < this.lvAttach.Items.Count; i++)
                {
                    data.AttachFile[i] = this.lvAttach.Items[i].Text;
                }
                data.AttachCount = this.ExNaccsData.AttachCount;
            }
            else
            {
                this.BoolAttach = false;
            }
            if (!this.ConfirmDialog(data.JobCode.TrimEnd(new char[0]), data.DispCode.TrimEnd(new char[0])))
            {
                return null;
            }
            return data;
        }

        protected void CmnClearJob()
        {
            this.BoolSendGuard = false;
            this.ClearResErr();
        }

        private void CommitGridData()
        {
            CommonJobPanel commonJobPanel = this.GetCommonJobPanel();
            if ((commonJobPanel != null) && (commonJobPanel.ActiveControl is LBTextBoxEditingControl))
            {
                LBTextBoxEditingControl activeControl = (LBTextBoxEditingControl) commonJobPanel.ActiveControl;
                LBDataGridView editingControlDataGridView = (LBDataGridView) activeControl.EditingControlDataGridView;
                DataGridViewDataErrorContexts context = 0;
                editingControlDataGridView.CommitEdit(context);
            }
        }

        private void CommonJobForm_Disposed(object sender, EventArgs e)
        {
            try
            {
                string jobSetup = PathInfo.CreateInstance().JobSetup;
                this.SaveJobConfig(jobSetup);
                if (this.stAttachPath != "")
                {
                    Directory.Delete(this.stAttachPath, true);
                }
                this.SendGuardIcon.Dispose();
            }
            catch
            {
                Console.WriteLine("Failure in deleting the attachment folder.(CommonJobForm.CommonJobForm_FormClosing)");
                Console.WriteLine(this.stAttachPath);
            }
        }

        private void CommonJobForm_Load(object sender, EventArgs e)
        {
            if (!this.isDesigning)
            {
                if (this.ExNaccsData.Items.Count > 0)
                {
                    this.InitCtlColor();
                }
                string jobSetup = PathInfo.CreateInstance().JobSetup;
                this.ReadJobConfig(jobSetup);
                if (this.FMode != ModeFont.normal)
                {
                    this.ExecChangeFontSize(this.FMode);
                }
                this.CursorTop(this.pnlGuiLayout);
                this.tmSetResField.Enabled = true;
            }
        }

        private void CommonJobForm_SizeChanged(object sender, EventArgs e)
        {
            if ((base.WindowState != FormWindowState.Minimized) && (this.pnlGuiLayout.Width <= this.spVertical.MinSize))
            {
                this.pnlCommon.Width = (base.ClientSize.Width - this.spVertical.MinSize) - this.spVertical.Width;
            }
        }

        private bool ConfirmDialog(string jCode, string dCode)
        {
            this.GetConfirm(jCode, dCode);
            return true;
        }

        protected static void CopyDirectory(string sourceDirName, string destDirName)
        {
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
                File.SetAttributes(destDirName, File.GetAttributes(sourceDirName));
            }
            if (destDirName[destDirName.Length - 1] != Path.DirectorySeparatorChar)
            {
                destDirName = destDirName + Path.DirectorySeparatorChar;
            }
            foreach (string str in Directory.GetFiles(sourceDirName))
            {
                File.Copy(str, destDirName + Path.GetFileName(str), true);
            }
            foreach (string str2 in Directory.GetDirectories(sourceDirName))
            {
                CopyDirectory(str2, destDirName + Path.GetFileName(str2));
            }
        }

        private void CreateLinkMenu(XmlElement elm, ToolStripMenuItem menu)
        {
            foreach (XmlElement element in elm.ChildNodes)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                if (element.Name.Equals("destination"))
                {
                    item.Text = element.GetAttribute("text");
                    item.Name = element.GetAttribute("no");
                    item.Size = new System.Drawing.Size(0xda, 0x16);
                    item.Click += new EventHandler(this.Link_Click);
                }
                else if (element.Name.Equals("category"))
                {
                    item.Text = element.GetAttribute("text");
                    this.CreateLinkMenu(element, item);
                }
                menu.DropDownItems.Add(item);
            }
        }

        public virtual int CreateNewForm(string jobCode, string kind, bool flgNew, bool flgJobTitle)
        {
            int num = 0;
            if (kind != "")
            {
                num = JobConfigFactory.createJobConfig(jobCode, kind, DateTime.Now, out this.JobConfig);
            }
            else
            {
                num = JobConfigFactory.createJobConfig(jobCode, DateTime.Now, out this.JobConfig);
            }
            if (num < 0)
            {
                return num;
            }
            num = this.IsValidJob(this.JobConfig);
            if (num < 0)
            {
                return num;
            }
            if (flgNew && (this.JobConfig.templateSytle != TemplateStyle.Display))
            {
                return -9;
            }
            return 0;
        }

        public virtual int CreateRecvForm(IData Data, bool flgJobTitle)
        {
            return 0;
        }

        private void CsvFileOpen(string tblName)
        {
            string path = null;
            StreamReader reader = null;
            this.OpenFileDlg.InitialDirectory = this.GetInitialDirectory();
            this.OpenFileDlg.Filter = "CSV file(*.csv)|*.csv|All files(*.*)|*.*";
            if (this.OpenFileDlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    try
                    {
                        path = this.OpenFileDlg.FileName;
                        reader = new StreamReader(path, Encoding.UTF8);
                        List<string> stList = CommaText.CommaTextToItems(reader.ReadToEnd(), true);
                        this.AddCsvData(stList, tblName);
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
                catch (Exception exception)
                {
                    MessageDialog dialog = new MessageDialog();
                    dialog.ShowMessage("E302", path, null, exception);
                    dialog.Dispose();
                }
            }
        }

        protected void CursorBottom(Control C)
        {
            C.Focus();
            C.SelectNextControl(C, false, true, true, true);
        }

        protected void CursorTop(Control C)
        {
            C.Focus();
            C.SelectNextControl(C, true, true, true, true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DoEdtTextMenu(CommandID cmd)
        {
            CommonJobPanel commonJobPanel = this.GetCommonJobPanel();
            if (commonJobPanel != null)
            {
                Control activeControl = commonJobPanel.ActiveControl;
                if ((activeControl == null) && this.txtInputInfo.Focused)
                {
                    activeControl = this.txtInputInfo;
                }
                if (((activeControl is ExTextBox) || (activeControl is LBTextBoxEditingControl)) || ((activeControl is LBCommaTextBox) || (activeControl is LBMaskedTextBox)))
                {
                    TextBoxBase base2 = (TextBoxBase) activeControl;
                    if (cmd == StandardCommands.Undo)
                    {
                        if (activeControl is LBCommaTextBox)
                        {
                            ((LBCommaTextBox) activeControl).undo();
                        }
                        else if (activeControl is LBMaskedTextBox)
                        {
                            ((LBMaskedTextBox) activeControl).undo();
                        }
                        else if (activeControl is LBTextBox)
                        {
                            ((LBTextBox) activeControl).undo();
                        }
                        else
                        {
                            base2.Undo();
                        }
                    }
                    else if (cmd == StandardCommands.Cut)
                    {
                        base2.Cut();
                    }
                    else if (cmd == StandardCommands.Copy)
                    {
                        base2.Copy();
                    }
                    else if (cmd == StandardCommands.Paste)
                    {
                        base2.Paste();
                    }
                }
                if (activeControl is LBDivTextBox)
                {
                    LBDivTextBox box4 = (LBDivTextBox) activeControl;
                    if (cmd == StandardCommands.Undo)
                    {
                        box4.Undo();
                    }
                    else if (cmd == StandardCommands.Cut)
                    {
                        box4.Cut();
                    }
                    else if (cmd == StandardCommands.Copy)
                    {
                        box4.Copy();
                    }
                    else if (cmd == StandardCommands.Paste)
                    {
                        box4.Paste();
                    }
                }
                if (activeControl is LBInputComboBox)
                {
                    LBInputComboBox box5 = (LBInputComboBox) activeControl;
                    if (cmd == StandardCommands.Undo)
                    {
                        box5.undo();
                    }
                    else if (cmd == StandardCommands.Cut)
                    {
                        box5.Cut();
                    }
                    else if (cmd == StandardCommands.Copy)
                    {
                        box5.Copy();
                    }
                    else if (cmd == StandardCommands.Paste)
                    {
                        box5.Paste();
                    }
                }
            }
        }

        protected virtual void ExecChangeFontSize(ModeFont mF)
        {
        }

        protected IData ExecGetLinkData(XmlElement linkDst)
        {
            CommonJobPanel commonJobPanel = this.GetCommonJobPanel();
            if (commonJobPanel == null)
            {
                return null;
            }
            IData data = new NaccsData();
            int page = 0;
            string rep = null;
            commonJobPanel.GetRepPage(out rep, out page);
            data.Header.JobCode = linkDst.GetAttribute("jobcode");
            data.Header.DispCode = linkDst.GetAttribute("displaycode");
            for (int i = 0; i < linkDst.ChildNodes.Count; i++)
            {
                XmlElement element = (XmlElement) linkDst.ChildNodes[i];
                string attribute = element.GetAttribute("from");
                string str3 = element.GetAttribute("from_source");
                string item = null;
                if (!Naccs.Common.Generator.ItemInfo.tblComName.Equals(str3))
                {
                    if (!string.Equals(str3, rep))
                    {
                        using (MessageDialog dialog = new MessageDialog())
                        {
                            dialog.ShowMessage("W501", "");
                        }
                        return null;
                    }
                    item = this.GetIdData(str3, attribute, page);
                }
                else
                {
                    item = this.GetIdData(str3, attribute, 0);
                }
                data.Items.Add(item);
            }
            return data;
        }

        protected void ExecSend()
        {
            IData data = null;
            string str = null;
            data = this.CmnAppendJob(false);
            if (data != null)
            {
                if (this.OnSend != null)
                {
                    str = this.OnSend(this, data);
                }
                this.ExNaccsData.ID = str;
            }
        }

        protected bool ExecSetLinkData(IData data, string linkFileName, int No)
        {
            string attribute = null;
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(linkFileName);
                XmlElement documentElement = document.DocumentElement;
                XmlElement element2 = this.SearchLinkElement(documentElement, No.ToString());
                if (element2 != null)
                {
                    attribute = element2.GetAttribute("autosend");
                    for (int i = 0; i < element2.ChildNodes.Count; i++)
                    {
                        XmlElement element3 = (XmlElement) element2.ChildNodes[i];
                        string stID = element3.GetAttribute("to");
                        string repID = element3.GetAttribute("to_source");
                        this.SetIdData(repID, stID, data.Items[i], 0);
                    }
                    this.AcceptDataChange();
                }
                else
                {
                    attribute = string.Empty;
                }
            }
            catch
            {
                Console.WriteLine("Failure in reading in the service link information file node.(CommonJobForm.ExecSetLinkData)");
                Console.WriteLine("(" + linkFileName + ")");
                return false;
            }
            return (attribute == "true");
        }

        protected virtual CommonJobPanel GetCommonJobPanel()
        {
            return null;
        }

        private ConfirmType GetConfirm(string jCode, string dCode)
        {
            ConfirmType none = ConfirmType.None;
            AbstractJobConfig config = null;
            if (dCode != "")
            {
                if (JobConfigFactory.createJobConfig(jCode, dCode, DateTime.Now, out config) < 0)
                {
                    return none;
                }
            }
            else if (JobConfigFactory.createJobConfig(jCode, DateTime.Now, out config) < 0)
            {
                return none;
            }
            if (config.jobStyle == JobStyle.normal)
            {
                return ((NormalConfig) config).record.confirm;
            }
            if (config.jobStyle == JobStyle.divide)
            {
                none = ((DivideConfig) config).record.confirm;
            }
            return none;
        }

        protected virtual void GetControlsFromJP(int idxJP, string iD, List<Control> lstCtrl)
        {
        }

        private string getGuide(string id)
        {
            if (this.docGuide != null)
            {
                int length = id.Length;
                StringBuilder builder = new StringBuilder(id);
                while (length > 0)
                {
                    XmlNode node = this.docGuide.DocumentElement.SelectSingleNode("item[@id='" + builder.ToString() + "']");
                    if (node != null)
                    {
                        return node.InnerText;
                    }
                    builder[--length] = '_';
                }
            }
            return "";
        }

        protected virtual string GetIdData(string RepID, string stID, int No)
        {
            return null;
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

        protected string GetMsgFile(string stJobCode)
        {
            AbstractJobConfig config = null;
            DivideConfig config2 = null;
            NormalConfig config3 = null;
            CombiConfig config4 = null;
            if (JobConfigFactory.createJobConfig(stJobCode.Trim(), DateTime.Now, out config) != 0)
            {
                string path = PathInfo.CreateInstance().TemplateRoot + @"send\" + stJobCode.Trim();
                if (Directory.Exists(path))
                {
                    string[] directories = null;
                    directories = Directory.GetDirectories(path);
                    if (directories.Length > 0)
                    {
                        string dispcode = directories[0].Remove(0, path.Length + 1);
                        JobConfigFactory.createJobConfig(stJobCode.Trim(), dispcode, DateTime.Now, out config);
                    }
                }
            }
            if (config != null)
            {
                if (config.jobStyle == JobStyle.divide)
                {
                    config2 = (DivideConfig) config;
                    return config2.displayDivision.help;
                }
                if (config.jobStyle == JobStyle.normal)
                {
                    config3 = (NormalConfig) config;
                    return config3.Help;
                }
                if (config.jobStyle == JobStyle.combi)
                {
                    config4 = (CombiConfig) config;
                    return config4.configs[0].config.Help;
                }
            }
            return "";
        }

        private void gvResult_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string str = (string) this.gvResult[4, e.RowIndex].Value;
                string str2 = (string) this.gvResult[6, e.RowIndex].Value;
                if (((str != "0000") && !string.IsNullOrEmpty(str)) && !string.IsNullOrEmpty(str2))
                {
                    string str3 = (string) this.gvResult[7, e.RowIndex].Value;
                    if (string.IsNullOrEmpty(str3))
                    {
                        str3 = "0";
                    }
                    this.SetFocusField(str, int.Parse(str2), int.Parse(str3));
                }
            }
        }

        protected virtual void InitCtlColor()
        {
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(CommonJobForm));
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            this.pnlGuiLayout = new Panel();
            this.spVertical = new Splitter();
            this.pnlCommon = new Panel();
            this.pnlResultCode = new Panel();
            this.gbResult = new GroupBox();
            this.gvResult = new DataGridView();
            this.colIcon = new DataGridViewImageColumn();
            this.clCode = new DataGridViewTextBoxColumn();
            this.clContents = new DataGridViewTextBoxColumn();
            this.clDispose = new DataGridViewTextBoxColumn();
            this.clID = new DataGridViewTextBoxColumn();
            this.clJobCode = new DataGridViewTextBoxColumn();
            this.clNo = new DataGridViewTextBoxColumn();
            this.clJobNo = new DataGridViewTextBoxColumn();
            this.spHorizon2 = new Splitter();
            this.spHorizon3 = new Splitter();
            this.pnlGuide = new Panel();
            this.gbGuide = new GroupBox();
            this.rtxtGuide = new RichTextBox();
            this.spHorizon1 = new Splitter();
            this.pnlHeaderInfo = new Panel();
            this.gbAttach = new GroupBox();
            this.lvAttach = new ListView();
            this.colFile = new ColumnHeader();
            this.colSize = new ColumnHeader();
            this.gbInputInfo = new GroupBox();
            this.txtInputInfo = new LBTextBox();
            this.PopMenu = new ContextMenuStrip(this.components);
            this.pmnUndo = new ToolStripMenuItem();
            this.mnSep11 = new ToolStripSeparator();
            this.pmnCut = new ToolStripMenuItem();
            this.pmnCopy = new ToolStripMenuItem();
            this.pmnPaste = new ToolStripMenuItem();
            this.mnSep12 = new ToolStripSeparator();
            this.pmnRowCopy = new ToolStripMenuItem();
            this.pmnRowPaste = new ToolStripMenuItem();
            this.pmnGridPaste = new ToolStripMenuItem();
            this.pmnGridInsert = new ToolStripMenuItem();
            this.pmnGridRemove = new ToolStripMenuItem();
            this.mnSep13 = new ToolStripSeparator();
            this.pmnClear = new ToolStripMenuItem();
            this.pmnAllClear = new ToolStripMenuItem();
            this.pmnCommonClear = new ToolStripMenuItem();
            this.pmnClearCurrentToEnd = new ToolStripMenuItem();
            this.pmnSelectRanClear = new ToolStripMenuItem();
            this.mnSep14 = new ToolStripSeparator();
            this.pmnAllCheck = new ToolStripMenuItem();
            this.pmnAllCheckOut = new ToolStripMenuItem();
            this.mnSep15 = new ToolStripSeparator();
            this.pmnShowHint = new ToolStripMenuItem();
            this.mnSep16 = new ToolStripSeparator();
            this.pmnJobLink = new ToolStripMenuItem();
            this.statusBar = new StatusBar();
            this.stPnlUser = new StatusBarPanel();
            this.stPnlSendGuard = new StatusBarPanel();
            this.ToolStrip = new System.Windows.Forms.ToolStrip();
            this.btnFileOpen = new ToolStripButton();
            this.btnFileSaveAs = new ToolStripButton();
            this.toolSep1 = new ToolStripSeparator();
            this.btnAttachAppend = new ToolStripButton();
            this.toolSep2 = new ToolStripSeparator();
            this.btnPrintJobData = new ToolStripButton();
            this.toolSep3 = new ToolStripSeparator();
            this.btnCut = new ToolStripButton();
            this.btnCopy = new ToolStripButton();
            this.btnPaste = new ToolStripButton();
            this.btnUndo = new ToolStripButton();
            this.toolSep4 = new ToolStripSeparator();
            this.btnAppendJobData = new ToolStripButton();
            this.toolSep6 = new ToolStripSeparator();
            this.btnSend = new ToolStripButton();
            this.toolSep5 = new ToolStripSeparator();
            this.btnGuidance = new ToolStripButton();
            this.MainMenu = new MenuStrip();
            this.mnFile = new ToolStripMenuItem();
            this.mnJobNew = new ToolStripMenuItem();
            this.mnRedo = new ToolStripMenuItem();
            this.mnSep1 = new ToolStripSeparator();
            this.mnAppendJobData = new ToolStripMenuItem();
            this.mnUpdateJobData = new ToolStripMenuItem();
            this.mnSep2 = new ToolStripSeparator();
            this.mnFileOpen = new ToolStripMenuItem();
            this.mnFileSaveAs = new ToolStripMenuItem();
            this.mnFileSend = new ToolStripMenuItem();
            this.mnCSV = new ToolStripMenuItem();
            this.mnCsvCommon = new ToolStripMenuItem();
            this.mnCsvRan = new ToolStripMenuItem();
            this.mnSep3 = new ToolStripSeparator();
            this.mnAttach = new ToolStripMenuItem();
            this.mnAttachOpen = new ToolStripMenuItem();
            this.mnAttachDelete = new ToolStripMenuItem();
            this.mnAttachAppend = new ToolStripMenuItem();
            this.mnSep4 = new ToolStripSeparator();
            this.mnPrintJobData = new ToolStripMenuItem();
            this.mnPrintJobDataPreview = new ToolStripMenuItem();
            this.mnHardCpy = new ToolStripMenuItem();
            this.mnHardCopy = new ToolStripMenuItem();
            this.mnHardCopyPreview = new ToolStripMenuItem();
            this.mnSep5 = new ToolStripSeparator();
            this.mnJobClose = new ToolStripMenuItem();
            this.mnEdit = new ToolStripMenuItem();
            this.mnUndo = new ToolStripMenuItem();
            this.mnSep6 = new ToolStripSeparator();
            this.mnCut = new ToolStripMenuItem();
            this.mnCopy = new ToolStripMenuItem();
            this.mnPaste = new ToolStripMenuItem();
            this.mnSep7 = new ToolStripSeparator();
            this.mnRowCopy = new ToolStripMenuItem();
            this.mnRowPaste = new ToolStripMenuItem();
            this.mnGridPaste = new ToolStripMenuItem();
            this.mnGridInsert = new ToolStripMenuItem();
            this.mnGridRemove = new ToolStripMenuItem();
            this.mnSep8 = new ToolStripSeparator();
            this.mnClear = new ToolStripMenuItem();
            this.mnAllClear = new ToolStripMenuItem();
            this.mnCommonClear = new ToolStripMenuItem();
            this.mnClearCurrentToEnd = new ToolStripMenuItem();
            this.mnSelectRanClear = new ToolStripMenuItem();
            this.mnSendGuardOFF = new ToolStripMenuItem();
            this.mnSep9 = new ToolStripSeparator();
            this.mnAllCheck = new ToolStripMenuItem();
            this.mnAllCheckOut = new ToolStripMenuItem();
            this.mnView = new ToolStripMenuItem();
            this.mnChangeActivePage = new ToolStripMenuItem();
            this.mnSep17 = new ToolStripSeparator();
            this.mnShowHint = new ToolStripMenuItem();
            this.mnSep10 = new ToolStripSeparator();
            this.mnZoomIn = new ToolStripMenuItem();
            this.mnZoomOut = new ToolStripMenuItem();
            this.mnDefaultSize = new ToolStripMenuItem();
            this.mnJob = new ToolStripMenuItem();
            this.mnSend = new ToolStripMenuItem();
            this.mnOption = new ToolStripMenuItem();
            this.mnSystem = new ToolStripMenuItem();
            this.mnHelp = new ToolStripMenuItem();
            this.mnGuidance = new ToolStripMenuItem();
            this.JobImageList = new ImageList(this.components);
            this.pmnSendGuardOFF = new ContextMenuStrip(this.components);
            this.smnSendGuardOFF = new ToolStripMenuItem();
            this.OpenFileDlg = new OpenFileDialog();
            this.SaveFileDlg = new SaveFileDialog();
            this.ResImageList = new ImageList(this.components);
            this.tmSetResField = new System.Windows.Forms.Timer(this.components);
            this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            ToolStripContainer container = new ToolStripContainer();
            container.ContentPanel.SuspendLayout();
            container.TopToolStripPanel.SuspendLayout();
            container.SuspendLayout();
            this.pnlCommon.SuspendLayout();
            this.pnlResultCode.SuspendLayout();
            this.gbResult.SuspendLayout();
            ((ISupportInitialize) this.gvResult).BeginInit();
            this.pnlGuide.SuspendLayout();
            this.gbGuide.SuspendLayout();
            this.pnlHeaderInfo.SuspendLayout();
            this.gbAttach.SuspendLayout();
            this.gbInputInfo.SuspendLayout();
            this.PopMenu.SuspendLayout();
            this.stPnlUser.BeginInit();
            this.stPnlSendGuard.BeginInit();
            this.ToolStrip.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.pmnSendGuardOFF.SuspendLayout();
            base.SuspendLayout();
            container.ContentPanel.Controls.Add(this.pnlGuiLayout);
            container.ContentPanel.Controls.Add(this.spVertical);
            container.ContentPanel.Controls.Add(this.pnlCommon);
            container.ContentPanel.Controls.Add(this.statusBar);
            container.ContentPanel.Size = new System.Drawing.Size(0x3f8, 0x2ab);
            container.Dock = DockStyle.Fill;
            container.Location = new Point(0, 0x1a);
            container.Name = "TContainer";
            container.Size = new System.Drawing.Size(0x3f8, 0x2c4);
            container.TabIndex = 2;
            container.Text = "toolStripContainer1";
            container.TopToolStripPanel.Controls.Add(this.ToolStrip);
            this.pnlGuiLayout.BorderStyle = BorderStyle.Fixed3D;
            this.pnlGuiLayout.Dock = DockStyle.Fill;
            this.pnlGuiLayout.Location = new Point(0x12f, 0);
            this.pnlGuiLayout.Name = "pnlGuiLayout";
            this.pnlGuiLayout.Size = new System.Drawing.Size(0x2c9, 0x295);
            this.pnlGuiLayout.TabIndex = 1;
            this.spVertical.BackColor = SystemColors.Control;
            this.spVertical.Location = new Point(300, 0);
            this.spVertical.Name = "spVertical";
            this.spVertical.Size = new System.Drawing.Size(3, 0x295);
            this.spVertical.TabIndex = 4;
            this.spVertical.TabStop = false;
            this.pnlCommon.BorderStyle = BorderStyle.Fixed3D;
            this.pnlCommon.Controls.Add(this.pnlResultCode);
            this.pnlCommon.Controls.Add(this.spHorizon2);
            this.pnlCommon.Controls.Add(this.spHorizon3);
            this.pnlCommon.Controls.Add(this.pnlGuide);
            this.pnlCommon.Controls.Add(this.spHorizon1);
            this.pnlCommon.Controls.Add(this.pnlHeaderInfo);
            this.pnlCommon.Dock = DockStyle.Left;
            this.pnlCommon.Location = new Point(0, 0);
            this.pnlCommon.Name = "pnlCommon";
            this.pnlCommon.Size = new System.Drawing.Size(300, 0x295);
            this.pnlCommon.TabIndex = 0;
            this.pnlResultCode.BorderStyle = BorderStyle.Fixed3D;
            this.pnlResultCode.Controls.Add(this.gbResult);
            this.pnlResultCode.Dock = DockStyle.Fill;
            this.pnlResultCode.Location = new Point(0, 320);
            this.pnlResultCode.Name = "pnlResultCode";
            this.pnlResultCode.Size = new System.Drawing.Size(0x128, 0x14c);
            this.pnlResultCode.TabIndex = 2;
            this.gbResult.Controls.Add(this.gvResult);
            this.gbResult.Dock = DockStyle.Fill;
            this.gbResult.Location = new Point(0, 0);
            this.gbResult.Name = "gbResult";
            this.gbResult.Size = new System.Drawing.Size(0x124, 0x148);
            this.gbResult.TabIndex = 0;
            this.gbResult.TabStop = false;
            this.gbResult.Text = "Th\x00f4ng điệp nghiệp vụ";
            this.gvResult.AllowUserToAddRows = false;
            this.gvResult.AllowUserToDeleteRows = false;
            this.gvResult.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.gvResult.BackgroundColor = SystemColors.Control;
            this.gvResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvResult.Columns.AddRange(new DataGridViewColumn[] { this.colIcon, this.clCode, this.clContents, this.clDispose, this.clID, this.clJobCode, this.clNo, this.clJobNo });
            this.gvResult.Dock = DockStyle.Fill;
            this.gvResult.GridColor = SystemColors.InactiveCaptionText;
            this.gvResult.Location = new Point(3, 15);
            this.gvResult.Name = "gvResult";
            this.gvResult.ReadOnly = true;
            this.gvResult.RowHeadersVisible = false;
            style.WrapMode = DataGridViewTriState.True;
            this.gvResult.RowsDefaultCellStyle = style;
            this.gvResult.RowTemplate.Height = 0x15;
            this.gvResult.Size = new System.Drawing.Size(0x11e, 310);
            this.gvResult.TabIndex = 0;
            this.gvResult.TabStop = false;
            this.gvResult.CellDoubleClick += new DataGridViewCellEventHandler(this.gvResult_CellDoubleClick);
            this.colIcon.Frozen = true;
            this.colIcon.HeaderText = "";
            this.colIcon.Name = "colIcon";
            this.colIcon.ReadOnly = true;
            this.colIcon.Resizable = DataGridViewTriState.False;
            this.colIcon.Width = 30;
            style2.ForeColor = SystemColors.ControlText;
            this.clCode.DefaultCellStyle = style2;
            this.clCode.HeaderText = "M\x00e3";
            this.clCode.Name = "clCode";
            this.clCode.ReadOnly = true;
            this.clCode.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clCode.Width = 60;
            this.clContents.HeaderText = "Nội dung";
            this.clContents.Name = "clContents";
            this.clContents.ReadOnly = true;
            this.clContents.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clDispose.HeaderText = "Giải ph\x00e1p";
            this.clDispose.Name = "clDispose";
            this.clDispose.ReadOnly = true;
            this.clDispose.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clID.HeaderText = "M\x00e3 chỉ ti\x00eau";
            this.clID.Name = "clID";
            this.clID.ReadOnly = true;
            this.clID.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clID.Width = 80;
            this.clJobCode.HeaderText = "業務コード";
            this.clJobCode.Name = "clJobCode";
            this.clJobCode.ReadOnly = true;
            this.clJobCode.Resizable = DataGridViewTriState.True;
            this.clJobCode.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clJobCode.Visible = false;
            this.clJobCode.Width = 70;
            this.clNo.HeaderText = "行番号";
            this.clNo.Name = "clNo";
            this.clNo.ReadOnly = true;
            this.clNo.Visible = false;
            this.clJobNo.HeaderText = "業務項番";
            this.clJobNo.Name = "clJobNo";
            this.clJobNo.ReadOnly = true;
            this.clJobNo.Visible = false;
            this.spHorizon2.BackColor = SystemColors.Control;
            this.spHorizon2.Dock = DockStyle.Top;
            this.spHorizon2.Location = new Point(0, 0x13b);
            this.spHorizon2.Name = "spHorizon2";
            this.spHorizon2.Size = new System.Drawing.Size(0x128, 5);
            this.spHorizon2.TabIndex = 9;
            this.spHorizon2.TabStop = false;
            this.spHorizon3.BackColor = SystemColors.Control;
            this.spHorizon3.Dock = DockStyle.Bottom;
            this.spHorizon3.Location = new Point(0, 0x28c);
            this.spHorizon3.Name = "spHorizon3";
            this.spHorizon3.Size = new System.Drawing.Size(0x128, 5);
            this.spHorizon3.TabIndex = 6;
            this.spHorizon3.TabStop = false;
            this.pnlGuide.BorderStyle = BorderStyle.Fixed3D;
            this.pnlGuide.Controls.Add(this.gbGuide);
            this.pnlGuide.Dock = DockStyle.Top;
            this.pnlGuide.Location = new Point(0, 200);
            this.pnlGuide.Name = "pnlGuide";
            this.pnlGuide.Size = new System.Drawing.Size(0x128, 0x73);
            this.pnlGuide.TabIndex = 1;
            this.gbGuide.Controls.Add(this.rtxtGuide);
            this.gbGuide.Dock = DockStyle.Fill;
            this.gbGuide.Location = new Point(0, 0);
            this.gbGuide.Name = "gbGuide";
            this.gbGuide.Size = new System.Drawing.Size(0x124, 0x6f);
            this.gbGuide.TabIndex = 0;
            this.gbGuide.TabStop = false;
            this.gbGuide.Text = "Hướng dẫn nhập liệu";
            this.rtxtGuide.BackColor = SystemColors.Control;
            this.rtxtGuide.Dock = DockStyle.Fill;
            this.rtxtGuide.Location = new Point(3, 15);
            this.rtxtGuide.Name = "rtxtGuide";
            this.rtxtGuide.ReadOnly = true;
            this.rtxtGuide.Size = new System.Drawing.Size(0x11e, 0x5d);
            this.rtxtGuide.TabIndex = 0;
            this.rtxtGuide.TabStop = false;
            this.rtxtGuide.Text = "";
            this.spHorizon1.BackColor = SystemColors.Control;
            this.spHorizon1.Dock = DockStyle.Top;
            this.spHorizon1.Location = new Point(0, 0xc3);
            this.spHorizon1.Name = "spHorizon1";
            this.spHorizon1.Size = new System.Drawing.Size(0x128, 5);
            this.spHorizon1.TabIndex = 7;
            this.spHorizon1.TabStop = false;
            this.pnlHeaderInfo.Controls.Add(this.gbAttach);
            this.pnlHeaderInfo.Controls.Add(this.gbInputInfo);
            this.pnlHeaderInfo.Dock = DockStyle.Top;
            this.pnlHeaderInfo.Location = new Point(0, 0);
            this.pnlHeaderInfo.Name = "pnlHeaderInfo";
            this.pnlHeaderInfo.Size = new System.Drawing.Size(0x128, 0xc3);
            this.pnlHeaderInfo.TabIndex = 0;
            this.gbAttach.AutoSize = true;
            this.gbAttach.Controls.Add(this.lvAttach);
            this.gbAttach.Dock = DockStyle.Fill;
            this.gbAttach.Location = new Point(0, 0x2b);
            this.gbAttach.Name = "gbAttach";
            this.gbAttach.Size = new System.Drawing.Size(0x128, 0x98);
            this.gbAttach.TabIndex = 2;
            this.gbAttach.TabStop = false;
            this.gbAttach.Text = "Tệp tin đ\x00ednh k\x00e8m";
            this.lvAttach.Columns.AddRange(new ColumnHeader[] { this.colFile, this.colSize });
            this.lvAttach.Dock = DockStyle.Fill;
            this.lvAttach.FullRowSelect = true;
            this.lvAttach.HideSelection = false;
            this.lvAttach.Location = new Point(3, 15);
            this.lvAttach.Name = "lvAttach";
            this.lvAttach.Size = new System.Drawing.Size(290, 0x86);
            this.lvAttach.TabIndex = 0;
            this.lvAttach.TabStop = false;
            this.lvAttach.UseCompatibleStateImageBehavior = false;
            this.lvAttach.View = View.Details;
            this.colFile.Text = "T\x00ean b\x00e1o c\x00e1o";
            this.colFile.Width = 130;
            this.colSize.Text = "K\x00edch thước";
            this.colSize.Width = 0x4f;
            this.gbInputInfo.BackColor = SystemColors.Control;
            this.gbInputInfo.BackgroundImageLayout = ImageLayout.None;
            this.gbInputInfo.Controls.Add(this.txtInputInfo);
            this.gbInputInfo.Dock = DockStyle.Top;
            this.gbInputInfo.Location = new Point(0, 0);
            this.gbInputInfo.Name = "gbInputInfo";
            this.gbInputInfo.RightToLeft = RightToLeft.No;
            this.gbInputInfo.Size = new System.Drawing.Size(0x128, 0x2b);
            this.gbInputInfo.TabIndex = 1;
            this.gbInputInfo.TabStop = false;
            this.gbInputInfo.Text = "M\x00e3 th\x00f4ng điệp đầu v\x00e0o";
            this.txtInputInfo.attribute = "A";
            this.txtInputInfo.AutoComplete = false;
            this.txtInputInfo.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.txtInputInfo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.txtInputInfo.BackColor = SystemColors.Window;
            this.txtInputInfo.BindText = null;
            this.txtInputInfo.CharacterCasing = CharacterCasing.Upper;
            this.txtInputInfo.check_attribute = "";
            this.txtInputInfo.check_date = "";
            this.txtInputInfo.check_full = "";
            this.txtInputInfo.check_time = "";
            this.txtInputInfo.choice_keyvalue = "";
            this.txtInputInfo.ContextMenuStrip = this.PopMenu;
            this.txtInputInfo.DisplayWidth = 0;
            this.txtInputInfo.figure = 10;
            this.txtInputInfo.form = "";
            this.txtInputInfo.IBackColor = SystemColors.Window;
            this.txtInputInfo.id = "";
            this.txtInputInfo.ImeMode = ImeMode.Off;
            this.txtInputInfo.input_output = " ";
            this.txtInputInfo.JobErr = null;
            this.txtInputInfo.Location = new Point(6, 0x12);
            this.txtInputInfo.MaxLength = 10;
            this.txtInputInfo.name = "";
            this.txtInputInfo.Name = "txtInputInfo";
            this.txtInputInfo.order = -1;
            this.txtInputInfo.Rep_ID = "";
            this.txtInputInfo.required = "";
            this.txtInputInfo.Size = new System.Drawing.Size(0x92, 0x13);
            this.txtInputInfo.TabIndex = 0;
            this.PopMenu.Items.AddRange(new ToolStripItem[] { 
                this.pmnUndo, this.mnSep11, this.pmnCut, this.pmnCopy, this.pmnPaste, this.mnSep12, this.pmnRowCopy, this.pmnRowPaste, this.pmnGridPaste, this.pmnGridInsert, this.pmnGridRemove, this.mnSep13, this.pmnClear, this.mnSep14, this.pmnAllCheck, this.pmnAllCheckOut, 
                this.mnSep15, this.pmnShowHint, this.mnSep16, this.pmnJobLink
             });
            this.PopMenu.Name = "contextMenuStrip1";
            this.PopMenu.Size = new System.Drawing.Size(260, 0x15c);
            this.PopMenu.Opening += new CancelEventHandler(this.PopMenu_Opening);
            this.pmnUndo.Name = "pmnUndo";
            this.pmnUndo.ShortcutKeyDisplayString = "";
            this.pmnUndo.ShortcutKeys = Keys.Control | Keys.Z;
            this.pmnUndo.Size = new System.Drawing.Size(0x103, 0x16);
            this.pmnUndo.Tag = "mnJobUndo";
            this.pmnUndo.Text = "Ho\x00e0n t\x00e1c (&U)";
            this.pmnUndo.Click += new EventHandler(this.mnUndo_Click);
            this.mnSep11.Name = "mnSep11";
            this.mnSep11.Size = new System.Drawing.Size(0x100, 6);
            this.pmnCut.Name = "pmnCut";
            this.pmnCut.ShortcutKeyDisplayString = "";
            this.pmnCut.ShortcutKeys = Keys.Control | Keys.X;
            this.pmnCut.Size = new System.Drawing.Size(0x103, 0x16);
            this.pmnCut.Tag = "mnCut";
            this.pmnCut.Text = "Cắt (&T)";
            this.pmnCut.Click += new EventHandler(this.mnCut_Click);
            this.pmnCopy.Name = "pmnCopy";
            this.pmnCopy.ShortcutKeyDisplayString = "";
            this.pmnCopy.ShortcutKeys = Keys.Control | Keys.C;
            this.pmnCopy.Size = new System.Drawing.Size(0x103, 0x16);
            this.pmnCopy.Tag = "mnCopy";
            this.pmnCopy.Text = "Sao ch\x00e9p (&C)";
            this.pmnCopy.Click += new EventHandler(this.mnCopy_Click);
            this.pmnPaste.Name = "pmnPaste";
            this.pmnPaste.ShortcutKeyDisplayString = "";
            this.pmnPaste.ShortcutKeys = Keys.Control | Keys.V;
            this.pmnPaste.Size = new System.Drawing.Size(0x103, 0x16);
            this.pmnPaste.Tag = "mnPaste";
            this.pmnPaste.Text = "D\x00e1n (&P)";
            this.pmnPaste.Click += new EventHandler(this.mnPaste_Click);
            this.mnSep12.Name = "mnSep12";
            this.mnSep12.Size = new System.Drawing.Size(0x100, 6);
            this.pmnRowCopy.Name = "pmnRowCopy";
            this.pmnRowCopy.Size = new System.Drawing.Size(0x103, 0x16);
            this.pmnRowCopy.Tag = "mnRowCopy";
            this.pmnRowCopy.Text = "Sao ch\x00e9p d\x00f2ng (&K)";
            this.pmnRowCopy.Click += new EventHandler(this.mnRowCopy_Click);
            this.pmnRowPaste.Name = "pmnRowPaste";
            this.pmnRowPaste.Size = new System.Drawing.Size(0x103, 0x16);
            this.pmnRowPaste.Tag = "mnRowPaste";
            this.pmnRowPaste.Text = "D\x00e1n d\x00f2ng (&H)";
            this.pmnRowPaste.Click += new EventHandler(this.mnRowPaste_Click);
            this.pmnGridPaste.Name = "pmnGridPaste";
            this.pmnGridPaste.Size = new System.Drawing.Size(0x103, 0x16);
            this.pmnGridPaste.Tag = "mnGridPaste";
            this.pmnGridPaste.Text = "D\x00e1n bảng (&S)";
            this.pmnGridPaste.Click += new EventHandler(this.mnGridPaste_Click);
            this.pmnGridInsert.Name = "pmnGridInsert";
            this.pmnGridInsert.Size = new System.Drawing.Size(0x103, 0x16);
            this.pmnGridInsert.Tag = "mnGridInsert";
            this.pmnGridInsert.Text = "Ch\x00e8n d\x00f2ng (&I)";
            this.pmnGridInsert.Click += new EventHandler(this.mnGridInsert_Click);
            this.pmnGridRemove.Name = "pmnGridRemove";
            this.pmnGridRemove.Size = new System.Drawing.Size(0x103, 0x16);
            this.pmnGridRemove.Tag = "mnGridRemove";
            this.pmnGridRemove.Text = "X\x00f3a d\x00f2ng (&R)";
            this.pmnGridRemove.Click += new EventHandler(this.mnGridRemove_Click);
            this.mnSep13.Name = "mnSep13";
            this.mnSep13.Size = new System.Drawing.Size(0x100, 6);
            this.pmnClear.DropDownItems.AddRange(new ToolStripItem[] { this.pmnAllClear, this.pmnCommonClear, this.pmnClearCurrentToEnd, this.pmnSelectRanClear });
            this.pmnClear.Name = "pmnClear";
            this.pmnClear.Size = new System.Drawing.Size(0x103, 0x16);
            this.pmnClear.Text = "X\x00f3a m\x00e0n h\x00ecnh (&D)";
            this.pmnAllClear.Name = "pmnAllClear";
            this.pmnAllClear.Size = new System.Drawing.Size(0x13e, 0x16);
            this.pmnAllClear.Tag = "mnAllClear";
            this.pmnAllClear.Text = "X\x00f3a hết (&C)";
            this.pmnAllClear.Click += new EventHandler(this.mnAllClear_Click);
            this.pmnCommonClear.Name = "pmnCommonClear";
            this.pmnCommonClear.Size = new System.Drawing.Size(0x13e, 0x16);
            this.pmnCommonClear.Tag = "mnCommonClear";
            this.pmnCommonClear.Text = "X\x00f3a phần th\x00f4ng tin chung (&A)";
            this.pmnCommonClear.Click += new EventHandler(this.mnCommonClear_Click);
            this.pmnClearCurrentToEnd.Name = "pmnClearCurrentToEnd";
            this.pmnClearCurrentToEnd.ShortcutKeys = Keys.Control | Keys.D;
            this.pmnClearCurrentToEnd.Size = new System.Drawing.Size(0x13e, 0x16);
            this.pmnClearCurrentToEnd.Tag = "mnClearCurrentToEnd";
            this.pmnClearCurrentToEnd.Text = "X\x00f3a c\x00e1c d\x00f2ng lặp lại ph\x00eda dưới (&D)";
            this.pmnClearCurrentToEnd.Click += new EventHandler(this.mnClearCurrentToEnd_Click);
            this.pmnSelectRanClear.Name = "pmnSelectRanClear";
            this.pmnSelectRanClear.Size = new System.Drawing.Size(0x13e, 0x16);
            this.pmnSelectRanClear.Tag = "mnSelectRanClear";
            this.pmnSelectRanClear.Text = "X\x00f3a c\x00e1c d\x00f2ng lặp lại được lựa chọn (&L)";
            this.pmnSelectRanClear.Click += new EventHandler(this.mnSelectRanClear_Click);
            this.mnSep14.Name = "mnSep14";
            this.mnSep14.Size = new System.Drawing.Size(0x100, 6);
            this.pmnAllCheck.Name = "pmnAllCheck";
            this.pmnAllCheck.Size = new System.Drawing.Size(0x103, 0x16);
            this.pmnAllCheck.Text = "Chọn tất cả (&O)";
            this.pmnAllCheck.Click += new EventHandler(this.mnAllCheck_Click);
            this.pmnAllCheckOut.Name = "pmnAllCheckOut";
            this.pmnAllCheckOut.Size = new System.Drawing.Size(0x103, 0x16);
            this.pmnAllCheckOut.Text = "Bỏ chọn tất cả (&N)";
            this.pmnAllCheckOut.Click += new EventHandler(this.mnAllCheckOut_Click);
            this.mnSep15.Name = "mnSep15";
            this.mnSep15.Size = new System.Drawing.Size(0x100, 6);
            this.pmnShowHint.Name = "pmnShowHint";
            this.pmnShowHint.Size = new System.Drawing.Size(0x103, 0x16);
            this.pmnShowHint.Text = "Hiển thị đặc t\x00ednh của trường (&F)";
            this.pmnShowHint.Click += new EventHandler(this.mnShowHint_Click);
            this.mnSep16.Name = "mnSep16";
            this.mnSep16.Size = new System.Drawing.Size(0x100, 6);
            this.pmnJobLink.Enabled = false;
            this.pmnJobLink.Name = "pmnJobLink";
            this.pmnJobLink.Size = new System.Drawing.Size(0x103, 0x16);
            this.pmnJobLink.Text = "Link nghiệp vụ (&L)";
            this.statusBar.Location = new Point(0, 0x295);
            this.statusBar.Name = "statusBar";
            this.statusBar.Panels.AddRange(new StatusBarPanel[] { this.stPnlUser, this.stPnlSendGuard });
            this.statusBar.ShowPanels = true;
            this.statusBar.Size = new System.Drawing.Size(0x3f8, 0x16);
            this.statusBar.TabIndex = 3;
            this.statusBar.PanelClick += new StatusBarPanelClickEventHandler(this.statusBar_PanelClick);
            this.stPnlUser.AutoSize = StatusBarPanelAutoSize.Spring;
            this.stPnlUser.Name = "stPnlUser";
            this.stPnlUser.Width = 0x365;
            this.stPnlSendGuard.Alignment = HorizontalAlignment.Right;
            this.stPnlSendGuard.Icon = (Icon) manager.GetObject("stPnlSendGuard.Icon");
            this.stPnlSendGuard.Name = "stPnlSendGuard";
            this.stPnlSendGuard.Width = 130;
            this.ToolStrip.Dock = DockStyle.None;
            this.ToolStrip.Items.AddRange(new ToolStripItem[] { 
                this.btnFileOpen, this.btnFileSaveAs, this.toolSep1, this.btnAttachAppend, this.toolSep2, this.btnPrintJobData, this.toolSep3, this.btnCut, this.btnCopy, this.btnPaste, this.btnUndo, this.toolSep4, this.btnAppendJobData, this.toolSep6, this.btnSend, this.toolSep5, 
                this.btnGuidance
             });
            this.ToolStrip.Location = new Point(3, 0);
            this.ToolStrip.Name = "ToolStrip";
            this.ToolStrip.Size = new System.Drawing.Size(0x1b3, 0x19);
            this.ToolStrip.TabIndex = 5;
            this.btnFileOpen.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnFileOpen.Image = (Image) manager.GetObject("btnFileOpen.Image");
            this.btnFileOpen.ImageTransparentColor = Color.Magenta;
            this.btnFileOpen.Name = "btnFileOpen";
            this.btnFileOpen.Size = new System.Drawing.Size(0x17, 0x16);
            this.btnFileOpen.Text = "Mở tệp tin ngo\x00e0i";
            this.btnFileOpen.Click += new EventHandler(this.mnFileOpen_Click);
            this.btnFileSaveAs.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnFileSaveAs.Image = (Image) manager.GetObject("btnFileSaveAs.Image");
            this.btnFileSaveAs.ImageTransparentColor = Color.Magenta;
            this.btnFileSaveAs.Name = "btnFileSaveAs";
            this.btnFileSaveAs.Size = new System.Drawing.Size(0x17, 0x16);
            this.btnFileSaveAs.Text = "Lưu trữ";
            this.btnFileSaveAs.Click += new EventHandler(this.mnFileSaveAs_Click);
            this.toolSep1.Name = "toolSep1";
            this.toolSep1.Size = new System.Drawing.Size(6, 0x19);
            this.btnAttachAppend.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnAttachAppend.Image = (Image) manager.GetObject("btnAttachAppend.Image");
            this.btnAttachAppend.ImageTransparentColor = Color.Magenta;
            this.btnAttachAppend.Name = "btnAttachAppend";
            this.btnAttachAppend.Size = new System.Drawing.Size(0x17, 0x16);
            this.btnAttachAppend.Text = "Đ\x00ednh k\x00e8m tệp tin";
            this.btnAttachAppend.Click += new EventHandler(this.mnAttachAppend_Click);
            this.toolSep2.Name = "toolSep2";
            this.toolSep2.Size = new System.Drawing.Size(6, 0x19);
            this.btnPrintJobData.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnPrintJobData.Image = (Image) manager.GetObject("btnPrintJobData.Image");
            this.btnPrintJobData.ImageTransparentColor = Color.Magenta;
            this.btnPrintJobData.Name = "btnPrintJobData";
            this.btnPrintJobData.Size = new System.Drawing.Size(0x17, 0x16);
            this.btnPrintJobData.Text = "In ấn";
            this.btnPrintJobData.Click += new EventHandler(this.mnPrintJobData_Click);
            this.toolSep3.Name = "toolSep3";
            this.toolSep3.Size = new System.Drawing.Size(6, 0x19);
            this.btnCut.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnCut.Image = (Image) manager.GetObject("btnCut.Image");
            this.btnCut.ImageTransparentColor = Color.Magenta;
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(0x17, 0x16);
            this.btnCut.Text = "Cắt";
            this.btnCut.ToolTipText = "Cắt";
            this.btnCut.Click += new EventHandler(this.mnCut_Click);
            this.btnCopy.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnCopy.Image = (Image) manager.GetObject("btnCopy.Image");
            this.btnCopy.ImageTransparentColor = Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(0x17, 0x16);
            this.btnCopy.Text = "Sao ch\x00e9p";
            this.btnCopy.Click += new EventHandler(this.mnCopy_Click);
            this.btnPaste.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnPaste.Image = (Image) manager.GetObject("btnPaste.Image");
            this.btnPaste.ImageTransparentColor = Color.Magenta;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(0x17, 0x16);
            this.btnPaste.Text = "D\x00e1n";
            this.btnPaste.Click += new EventHandler(this.mnPaste_Click);
            this.btnUndo.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnUndo.Image = (Image) manager.GetObject("btnUndo.Image");
            this.btnUndo.ImageTransparentColor = Color.Magenta;
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(0x17, 0x16);
            this.btnUndo.Text = "Ho\x00e0n t\x00e1c";
            this.btnUndo.Click += new EventHandler(this.mnUndo_Click);
            this.toolSep4.Name = "toolSep4";
            this.toolSep4.Size = new System.Drawing.Size(6, 0x19);
            this.btnAppendJobData.Image = (Image) manager.GetObject("btnAppendJobData.Image");
            this.btnAppendJobData.ImageTransparentColor = Color.Magenta;
            this.btnAppendJobData.Name = "btnAppendJobData";
            this.btnAppendJobData.Size = new System.Drawing.Size(140, 0x16);
            this.btnAppendJobData.Text = "Đăng k\x00fd dữ liệu (&A)";
            this.btnAppendJobData.ToolTipText = "Đăng k\x00fd dữ liệu (A)";
            this.btnAppendJobData.Click += new EventHandler(this.mnAppendJobData_Click);
            this.toolSep6.Name = "toolSep6";
            this.toolSep6.Size = new System.Drawing.Size(6, 0x19);
            this.btnSend.Image = (Image) manager.GetObject("btnSend.Image");
            this.btnSend.ImageTransparentColor = Color.Magenta;
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(0x45, 0x16);
            this.btnSend.Text = "Gửi (&S)";
            this.btnSend.Click += new EventHandler(this.mnSend_Click);
            this.toolSep5.Name = "toolSep5";
            this.toolSep5.Size = new System.Drawing.Size(6, 0x19);
            this.toolSep5.Visible = false;
            this.btnGuidance.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnGuidance.Image = (Image) manager.GetObject("btnGuidance.Image");
            this.btnGuidance.ImageTransparentColor = Color.Magenta;
            this.btnGuidance.Name = "btnGuidance";
            this.btnGuidance.Size = new System.Drawing.Size(0x17, 0x16);
            this.btnGuidance.Text = "ガイダンス";
            this.btnGuidance.ToolTipText = "ガイダンス";
            this.btnGuidance.Visible = false;
            this.btnGuidance.Click += new EventHandler(this.mnGuidance_Click);
            this.MainMenu.Items.AddRange(new ToolStripItem[] { this.mnFile, this.mnEdit, this.mnView, this.mnJob, this.mnOption, this.mnHelp });
            this.MainMenu.Location = new Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(0x3f8, 0x1a);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "MainMenu";
            this.mnFile.DropDownItems.AddRange(new ToolStripItem[] { 
                this.mnJobNew, this.mnRedo, this.mnSep1, this.mnAppendJobData, this.mnUpdateJobData, this.mnSep2, this.mnFileOpen, this.mnFileSaveAs, this.mnFileSend, this.mnCSV, this.mnSep3, this.mnAttach, this.mnSep4, this.mnPrintJobData, this.mnPrintJobDataPreview, this.mnHardCpy, 
                this.mnSep5, this.mnJobClose
             });
            this.mnFile.Name = "mnFile";
            this.mnFile.Size = new System.Drawing.Size(0x52, 0x16);
            this.mnFile.Text = "Tệp tin (&F)";
            this.mnJobNew.Image = (Image) manager.GetObject("mnJobNew.Image");
            this.mnJobNew.ImageTransparentColor = Color.Fuchsia;
            this.mnJobNew.Name = "mnJobNew";
            this.mnJobNew.ShortcutKeys = Keys.F2;
            this.mnJobNew.Size = new System.Drawing.Size(0x10d, 0x16);
            this.mnJobNew.Tag = "mnJobNew";
            this.mnJobNew.Text = "Nghiệp vụ mới (&N)";
            this.mnJobNew.Click += new EventHandler(this.mnJobNew_Click);
            this.mnRedo.Name = "mnRedo";
            this.mnRedo.ShortcutKeys = Keys.F9;
            this.mnRedo.Size = new System.Drawing.Size(0x10d, 0x16);
            this.mnRedo.Tag = "mnRedo";
            this.mnRedo.Text = "Mở lại nghiệp vụ gần nhất (&R)";
            this.mnRedo.Click += new EventHandler(this.mnRedo_Click);
            this.mnSep1.Name = "mnSep1";
            this.mnSep1.Size = new System.Drawing.Size(0x10a, 6);
            this.mnAppendJobData.Image = (Image) manager.GetObject("mnAppendJobData.Image");
            this.mnAppendJobData.ImageTransparentColor = Color.Fuchsia;
            this.mnAppendJobData.Name = "mnAppendJobData";
            this.mnAppendJobData.ShortcutKeys = Keys.F6;
            this.mnAppendJobData.Size = new System.Drawing.Size(0x10d, 0x16);
            this.mnAppendJobData.Tag = "mnAppendJobData";
            this.mnAppendJobData.Text = "Đăng k\x00fd dữ liệu (&A)";
            this.mnAppendJobData.Click += new EventHandler(this.mnAppendJobData_Click);
            this.mnUpdateJobData.Name = "mnUpdateJobData";
            this.mnUpdateJobData.ShortcutKeys = Keys.F7;
            this.mnUpdateJobData.Size = new System.Drawing.Size(0x10d, 0x16);
            this.mnUpdateJobData.Tag = "mnUpdateJobData";
            this.mnUpdateJobData.Text = "Cập nhật dữ liệu (&U)";
            this.mnUpdateJobData.Click += new EventHandler(this.mnUpdateJobData_Click);
            this.mnSep2.Name = "mnSep2";
            this.mnSep2.Size = new System.Drawing.Size(0x10a, 6);
            this.mnFileOpen.Image = (Image) manager.GetObject("mnFileOpen.Image");
            this.mnFileOpen.ImageTransparentColor = Color.Fuchsia;
            this.mnFileOpen.Name = "mnFileOpen";
            this.mnFileOpen.ShortcutKeys = Keys.Control | Keys.O;
            this.mnFileOpen.Size = new System.Drawing.Size(0x10d, 0x16);
            this.mnFileOpen.Tag = "mnFileOpen";
            this.mnFileOpen.Text = "Mở tệp tin ngo\x00e0i (&O)";
            this.mnFileOpen.Click += new EventHandler(this.mnFileOpen_Click);
            this.mnFileSaveAs.Image = (Image) manager.GetObject("mnFileSaveAs.Image");
            this.mnFileSaveAs.ImageTransparentColor = Color.Fuchsia;
            this.mnFileSaveAs.Name = "mnFileSaveAs";
            this.mnFileSaveAs.ShortcutKeys = Keys.Control | Keys.S;
            this.mnFileSaveAs.Size = new System.Drawing.Size(0x10d, 0x16);
            this.mnFileSaveAs.Tag = "mnFileSaveAs";
            this.mnFileSaveAs.Text = "Lưu trữ (&S)";
            this.mnFileSaveAs.Click += new EventHandler(this.mnFileSaveAs_Click);
            this.mnFileSend.Name = "mnFileSend";
            this.mnFileSend.Size = new System.Drawing.Size(0x10d, 0x16);
            this.mnFileSend.Tag = "mnFileSend";
            this.mnFileSend.Text = "Nhập nhiều tệp tin (&L)";
            this.mnFileSend.Click += new EventHandler(this.mnFileSend_Click);
            this.mnCSV.DropDownItems.AddRange(new ToolStripItem[] { this.mnCsvCommon, this.mnCsvRan });
            this.mnCSV.Name = "mnCSV";
            this.mnCSV.Size = new System.Drawing.Size(0x10d, 0x16);
            this.mnCSV.Text = "Đọc tệp tin CSV (&C)";
            this.mnCsvCommon.Name = "mnCsvCommon";
            this.mnCsvCommon.Size = new System.Drawing.Size(0xf1, 0x16);
            this.mnCsvCommon.Tag = "mnCsvCommon";
            this.mnCsvCommon.Text = "Tất cả (&C)";
            this.mnCsvCommon.Click += new EventHandler(this.mnCsvCommon_Click);
            this.mnCsvRan.Name = "mnCsvRan";
            this.mnCsvRan.Size = new System.Drawing.Size(0xf1, 0x16);
            this.mnCsvRan.Tag = "mnCsvRan";
            this.mnCsvRan.Text = "Chỉ đọc th\x00f4ng tin chi tiết (&R)";
            this.mnCsvRan.Click += new EventHandler(this.mnCsvRan_Click);
            this.mnSep3.Name = "mnSep3";
            this.mnSep3.Size = new System.Drawing.Size(0x10a, 6);
            this.mnAttach.DropDownItems.AddRange(new ToolStripItem[] { this.mnAttachOpen, this.mnAttachDelete, this.mnAttachAppend });
            this.mnAttach.Name = "mnAttach";
            this.mnAttach.Size = new System.Drawing.Size(0x10d, 0x16);
            this.mnAttach.Text = "Đ\x00ednh k\x00e8m tệp tin (&T)";
            this.mnAttachOpen.Name = "mnAttachOpen";
            this.mnAttachOpen.Size = new System.Drawing.Size(0x84, 0x16);
            this.mnAttachOpen.Tag = "mnAttachOpen";
            this.mnAttachOpen.Text = "Mở (&O)";
            this.mnAttachOpen.Click += new EventHandler(this.mnAttachOpen_Click);
            this.mnAttachDelete.Name = "mnAttachDelete";
            this.mnAttachDelete.Size = new System.Drawing.Size(0x84, 0x16);
            this.mnAttachDelete.Tag = "mnAttachDelete";
            this.mnAttachDelete.Text = "X\x00f3a (&D)";
            this.mnAttachDelete.Click += new EventHandler(this.mnAttachDelete_Click);
            this.mnAttachAppend.Image = (Image) manager.GetObject("mnAttachAppend.Image");
            this.mnAttachAppend.ImageTransparentColor = Color.Fuchsia;
            this.mnAttachAppend.Name = "mnAttachAppend";
            this.mnAttachAppend.Size = new System.Drawing.Size(0x84, 0x16);
            this.mnAttachAppend.Tag = "mnAttachAppend";
            this.mnAttachAppend.Text = "Th\x00eam (&A)";
            this.mnAttachAppend.Click += new EventHandler(this.mnAttachAppend_Click);
            this.mnSep4.Name = "mnSep4";
            this.mnSep4.Size = new System.Drawing.Size(0x10a, 6);
            this.mnPrintJobData.Image = (Image) manager.GetObject("mnPrintJobData.Image");
            this.mnPrintJobData.ImageTransparentColor = Color.Fuchsia;
            this.mnPrintJobData.Name = "mnPrintJobData";
            this.mnPrintJobData.ShortcutKeys = Keys.Control | Keys.P;
            this.mnPrintJobData.Size = new System.Drawing.Size(0x10d, 0x16);
            this.mnPrintJobData.Tag = "mnPrintJobData";
            this.mnPrintJobData.Text = "In ấn (&P)";
            this.mnPrintJobData.Click += new EventHandler(this.mnPrintJobData_Click);
            this.mnPrintJobDataPreview.Name = "mnPrintJobDataPreview";
            this.mnPrintJobDataPreview.Size = new System.Drawing.Size(0x10d, 0x16);
            this.mnPrintJobDataPreview.Tag = "mnPrintJobDataPreview";
            this.mnPrintJobDataPreview.Text = "Xem trước khi in (&V)";
            this.mnPrintJobDataPreview.Click += new EventHandler(this.mnPrintJobDataPreview_Click);
            this.mnHardCpy.DropDownItems.AddRange(new ToolStripItem[] { this.mnHardCopy, this.mnHardCopyPreview });
            this.mnHardCpy.Name = "mnHardCpy";
            this.mnHardCpy.Size = new System.Drawing.Size(0x10d, 0x16);
            this.mnHardCpy.Text = "Bản sao cứng (&H)";
            this.mnHardCopy.Name = "mnHardCopy";
            this.mnHardCopy.ShortcutKeys = Keys.Control | Keys.H;
            this.mnHardCopy.Size = new System.Drawing.Size(0xc2, 0x16);
            this.mnHardCopy.Tag = "mnHardCopy";
            this.mnHardCopy.Text = "In ấn (&P)";
            this.mnHardCopy.Click += new EventHandler(this.mnHardCopy_Click);
            this.mnHardCopyPreview.Name = "mnHardCopyPreview";
            this.mnHardCopyPreview.Size = new System.Drawing.Size(0xc2, 0x16);
            this.mnHardCopyPreview.Tag = "mnHardCopyPreview";
            this.mnHardCopyPreview.Text = "Xem trước khi in (&V)";
            this.mnHardCopyPreview.Click += new EventHandler(this.mnHardCopyPreview_Click);
            this.mnSep5.Name = "mnSep5";
            this.mnSep5.Size = new System.Drawing.Size(0x10a, 6);
            this.mnJobClose.Name = "mnJobClose";
            this.mnJobClose.Size = new System.Drawing.Size(0x10d, 0x16);
            this.mnJobClose.Tag = "mnJobClose";
            this.mnJobClose.Text = "Đ\x00f3ng (&X)";
            this.mnJobClose.Click += new EventHandler(this.mnJobClose_Click);
            this.mnEdit.DropDownItems.AddRange(new ToolStripItem[] { 
                this.mnUndo, this.mnSep6, this.mnCut, this.mnCopy, this.mnPaste, this.mnSep7, this.mnRowCopy, this.mnRowPaste, this.mnGridPaste, this.mnGridInsert, this.mnGridRemove, this.mnSep8, this.mnClear, this.mnSendGuardOFF, this.mnSep9, this.mnAllCheck, 
                this.mnAllCheckOut
             });
            this.mnEdit.Name = "mnEdit";
            this.mnEdit.Size = new System.Drawing.Size(0x3f, 0x16);
            this.mnEdit.Text = "Sửa (&E)";
            this.mnEdit.DropDownOpening += new EventHandler(this.mnEdit_DropDownOpening);
            this.mnUndo.Image = (Image) manager.GetObject("mnUndo.Image");
            this.mnUndo.ImageTransparentColor = Color.Fuchsia;
            this.mnUndo.Name = "mnUndo";
            this.mnUndo.ShortcutKeys = Keys.Control | Keys.Z;
            this.mnUndo.Size = new System.Drawing.Size(0xd5, 0x16);
            this.mnUndo.Tag = "mnJobUndo";
            this.mnUndo.Text = "Ho\x00e0n t\x00e1c (&U)";
            this.mnUndo.Click += new EventHandler(this.mnUndo_Click);
            this.mnSep6.Name = "mnSep6";
            this.mnSep6.Size = new System.Drawing.Size(210, 6);
            this.mnCut.Image = (Image) manager.GetObject("mnCut.Image");
            this.mnCut.ImageTransparentColor = Color.Fuchsia;
            this.mnCut.Name = "mnCut";
            this.mnCut.ShortcutKeys = Keys.Control | Keys.X;
            this.mnCut.Size = new System.Drawing.Size(0xd5, 0x16);
            this.mnCut.Tag = "mnCut";
            this.mnCut.Text = "Cắt (&T)";
            this.mnCut.Click += new EventHandler(this.mnCut_Click);
            this.mnCopy.Image = (Image) manager.GetObject("mnCopy.Image");
            this.mnCopy.ImageTransparentColor = Color.Fuchsia;
            this.mnCopy.Name = "mnCopy";
            this.mnCopy.ShortcutKeys = Keys.Control | Keys.C;
            this.mnCopy.Size = new System.Drawing.Size(0xd5, 0x16);
            this.mnCopy.Tag = "mnCopy";
            this.mnCopy.Text = "Sao ch\x00e9p (&C)";
            this.mnCopy.Click += new EventHandler(this.mnCopy_Click);
            this.mnPaste.Image = (Image) manager.GetObject("mnPaste.Image");
            this.mnPaste.ImageTransparentColor = Color.Fuchsia;
            this.mnPaste.Name = "mnPaste";
            this.mnPaste.ShortcutKeys = Keys.Control | Keys.V;
            this.mnPaste.Size = new System.Drawing.Size(0xd5, 0x16);
            this.mnPaste.Tag = "mnPaste";
            this.mnPaste.Text = "D\x00e1n (&P)";
            this.mnPaste.Click += new EventHandler(this.mnPaste_Click);
            this.mnSep7.Name = "mnSep7";
            this.mnSep7.Size = new System.Drawing.Size(210, 6);
            this.mnRowCopy.Name = "mnRowCopy";
            this.mnRowCopy.Size = new System.Drawing.Size(0xd5, 0x16);
            this.mnRowCopy.Tag = "mnRowCopy";
            this.mnRowCopy.Text = "Sao ch\x00e9p d\x00f2ng (&K)";
            this.mnRowCopy.Click += new EventHandler(this.mnRowCopy_Click);
            this.mnRowPaste.Name = "mnRowPaste";
            this.mnRowPaste.Size = new System.Drawing.Size(0xd5, 0x16);
            this.mnRowPaste.Tag = "mnRowPaste";
            this.mnRowPaste.Text = "D\x00e1n d\x00f2ng (&H)";
            this.mnRowPaste.Click += new EventHandler(this.mnRowPaste_Click);
            this.mnGridPaste.Name = "mnGridPaste";
            this.mnGridPaste.Size = new System.Drawing.Size(0xd5, 0x16);
            this.mnGridPaste.Tag = "mnGridPaste";
            this.mnGridPaste.Text = "D\x00e1n bảng (&S)";
            this.mnGridPaste.Click += new EventHandler(this.mnGridPaste_Click);
            this.mnGridInsert.Name = "mnGridInsert";
            this.mnGridInsert.Size = new System.Drawing.Size(0xd5, 0x16);
            this.mnGridInsert.Tag = "mnGridInsert";
            this.mnGridInsert.Text = "Ch\x00e8n d\x00f2ng (&I)";
            this.mnGridInsert.Click += new EventHandler(this.mnGridInsert_Click);
            this.mnGridRemove.Name = "mnGridRemove";
            this.mnGridRemove.Size = new System.Drawing.Size(0xd5, 0x16);
            this.mnGridRemove.Tag = "mnGridRemove";
            this.mnGridRemove.Text = "X\x00f3a d\x00f2ng (&R)";
            this.mnGridRemove.Click += new EventHandler(this.mnGridRemove_Click);
            this.mnSep8.Name = "mnSep8";
            this.mnSep8.Size = new System.Drawing.Size(210, 6);
            this.mnClear.DropDownItems.AddRange(new ToolStripItem[] { this.mnAllClear, this.mnCommonClear, this.mnClearCurrentToEnd, this.mnSelectRanClear });
            this.mnClear.Name = "mnClear";
            this.mnClear.Size = new System.Drawing.Size(0xd5, 0x16);
            this.mnClear.Tag = "";
            this.mnClear.Text = "X\x00f3a m\x00e0n h\x00ecnh (&D)";
            this.mnAllClear.Name = "mnAllClear";
            this.mnAllClear.Size = new System.Drawing.Size(0x13e, 0x16);
            this.mnAllClear.Tag = "mnAllClear";
            this.mnAllClear.Text = "X\x00f3a hết (&C)";
            this.mnAllClear.Click += new EventHandler(this.mnAllClear_Click);
            this.mnCommonClear.Name = "mnCommonClear";
            this.mnCommonClear.Size = new System.Drawing.Size(0x13e, 0x16);
            this.mnCommonClear.Tag = "mnCommonClear";
            this.mnCommonClear.Text = "X\x00f3a phần th\x00f4ng tin chung (&A)";
            this.mnCommonClear.Click += new EventHandler(this.mnCommonClear_Click);
            this.mnClearCurrentToEnd.Name = "mnClearCurrentToEnd";
            this.mnClearCurrentToEnd.ShortcutKeys = Keys.Control | Keys.D;
            this.mnClearCurrentToEnd.Size = new System.Drawing.Size(0x13e, 0x16);
            this.mnClearCurrentToEnd.Tag = "mnClearCurrentToEnd";
            this.mnClearCurrentToEnd.Text = "X\x00f3a c\x00e1c d\x00f2ng lặp lại ph\x00eda dưới (&D)";
            this.mnClearCurrentToEnd.Click += new EventHandler(this.mnClearCurrentToEnd_Click);
            this.mnSelectRanClear.Name = "mnSelectRanClear";
            this.mnSelectRanClear.Size = new System.Drawing.Size(0x13e, 0x16);
            this.mnSelectRanClear.Tag = "mnSelectRanClear";
            this.mnSelectRanClear.Text = "X\x00f3a c\x00e1c d\x00f2ng lặp lại được lựa chọn (&L)";
            this.mnSelectRanClear.Click += new EventHandler(this.mnSelectRanClear_Click);
            this.mnSendGuardOFF.Enabled = false;
            this.mnSendGuardOFF.Name = "mnSendGuardOFF";
            this.mnSendGuardOFF.Size = new System.Drawing.Size(0xd5, 0x16);
            this.mnSendGuardOFF.Text = "Bật t\x00ednh năng gửi lại (&L)";
            this.mnSendGuardOFF.Click += new EventHandler(this.mnSendGuardOFF_Click);
            this.mnSep9.Name = "mnSep9";
            this.mnSep9.Size = new System.Drawing.Size(210, 6);
            this.mnAllCheck.Name = "mnAllCheck";
            this.mnAllCheck.Size = new System.Drawing.Size(0xd5, 0x16);
            this.mnAllCheck.Text = "Chọn tất cả (&O)";
            this.mnAllCheck.Click += new EventHandler(this.mnAllCheck_Click);
            this.mnAllCheckOut.Name = "mnAllCheckOut";
            this.mnAllCheckOut.Size = new System.Drawing.Size(0xd5, 0x16);
            this.mnAllCheckOut.Text = "Bỏ chọn tất cả (&N)";
            this.mnAllCheckOut.Click += new EventHandler(this.mnAllCheckOut_Click);
            this.mnView.DropDownItems.AddRange(new ToolStripItem[] { this.mnChangeActivePage, this.mnSep17, this.mnShowHint, this.mnSep10, this.mnZoomIn, this.mnZoomOut, this.mnDefaultSize });
            this.mnView.Name = "mnView";
            this.mnView.Size = new System.Drawing.Size(0x57, 0x16);
            this.mnView.Text = "Hiển thị (&V)";
            this.mnChangeActivePage.Name = "mnChangeActivePage";
            this.mnChangeActivePage.ShortcutKeys = Keys.F5;
            this.mnChangeActivePage.Size = new System.Drawing.Size(0x103, 0x16);
            this.mnChangeActivePage.Tag = "mnChangeActivePage";
            this.mnChangeActivePage.Text = "Chuyển trang Tab (&C)";
            this.mnChangeActivePage.Click += new EventHandler(this.mnChangeActivePage_Click);
            this.mnSep17.Name = "mnSep17";
            this.mnSep17.Size = new System.Drawing.Size(0x100, 6);
            this.mnShowHint.Name = "mnShowHint";
            this.mnShowHint.Size = new System.Drawing.Size(0x103, 0x16);
            this.mnShowHint.Text = "Hiển thị đặc t\x00ednh của trường (&F)";
            this.mnShowHint.Click += new EventHandler(this.mnShowHint_Click);
            this.mnSep10.Name = "mnSep10";
            this.mnSep10.Size = new System.Drawing.Size(0x100, 6);
            this.mnZoomIn.Name = "mnZoomIn";
            this.mnZoomIn.Size = new System.Drawing.Size(0x103, 0x16);
            this.mnZoomIn.Text = "Cỡ chữ lớn (&L)";
            this.mnZoomIn.Click += new EventHandler(this.mnZoomIn_Click);
            this.mnZoomOut.Name = "mnZoomOut";
            this.mnZoomOut.Size = new System.Drawing.Size(0x103, 0x16);
            this.mnZoomOut.Text = "Cỡ chữ nhỏ (&S)";
            this.mnZoomOut.Click += new EventHandler(this.mnZoomOut_Click);
            this.mnDefaultSize.Name = "mnDefaultSize";
            this.mnDefaultSize.Size = new System.Drawing.Size(0x103, 0x16);
            this.mnDefaultSize.Text = "Cỡ chữ mặc định (&P)";
            this.mnDefaultSize.Click += new EventHandler(this.mnDefaultSize_Click);
            this.mnJob.DropDownItems.AddRange(new ToolStripItem[] { this.mnSend });
            this.mnJob.Name = "mnJob";
            this.mnJob.Size = new System.Drawing.Size(0x52, 0x16);
            this.mnJob.Text = "Dịch vụ (&J)";
            this.mnSend.Image = (Image) manager.GetObject("mnSend.Image");
            this.mnSend.ImageTransparentColor = Color.Fuchsia;
            this.mnSend.Name = "mnSend";
            this.mnSend.ShortcutKeys = Keys.F12;
            this.mnSend.Size = new System.Drawing.Size(0x92, 0x16);
            this.mnSend.Tag = "mnSend";
            this.mnSend.Text = "Gửi (&S)";
            this.mnSend.Click += new EventHandler(this.mnSend_Click);
            this.mnOption.DropDownItems.AddRange(new ToolStripItem[] { this.mnSystem });
            this.mnOption.Name = "mnOption";
            this.mnOption.Size = new System.Drawing.Size(0x5f, 0x16);
            this.mnOption.Text = "Lựa chọn (&O)";
            this.mnSystem.Name = "mnSystem";
            this.mnSystem.Size = new System.Drawing.Size(0x95, 0x16);
            this.mnSystem.Tag = "mnSystem";
            this.mnSystem.Text = "Thiết lập (&S)";
            this.mnSystem.Click += new EventHandler(this.mnSystem_Click);
            this.mnHelp.DropDownItems.AddRange(new ToolStripItem[] { this.mnGuidance });
            this.mnHelp.Name = "mnHelp";
            this.mnHelp.Size = new System.Drawing.Size(0x4b, 0x16);
            this.mnHelp.Text = "ヘルプ(&H)";
            this.mnHelp.Visible = false;
            this.mnGuidance.Image = (Image) manager.GetObject("mnGuidance.Image");
            this.mnGuidance.ImageTransparentColor = Color.Fuchsia;
            this.mnGuidance.Name = "mnGuidance";
            this.mnGuidance.Size = new System.Drawing.Size(0x9b, 0x16);
            this.mnGuidance.Tag = "mnGuidance";
            this.mnGuidance.Text = "ガイダンス(&G)";
            this.mnGuidance.Visible = false;
            this.mnGuidance.Click += new EventHandler(this.mnGuidance_Click);
            this.JobImageList.ImageStream = (ImageListStreamer) manager.GetObject("JobImageList.ImageStream");
            this.JobImageList.TransparentColor = Color.Transparent;
            this.JobImageList.Images.SetKeyName(0, "eventlogError.ico");
            this.JobImageList.Images.SetKeyName(1, "eventlogWarn.ico");
            this.JobImageList.Images.SetKeyName(2, "eventlogInfo.ico");
            this.JobImageList.Images.SetKeyName(3, "印刷.ico");
            this.JobImageList.Images.SetKeyName(4, "開く.ico");
            this.JobImageList.Images.SetKeyName(5, "保存.ico");
            this.JobImageList.Images.SetKeyName(6, "切り取り.ico");
            this.JobImageList.Images.SetKeyName(7, "コピー.ico");
            this.JobImageList.Images.SetKeyName(8, "貼り付け.ico");
            this.JobImageList.Images.SetKeyName(9, "元に戻す.ico");
            this.JobImageList.Images.SetKeyName(10, "添付.ico");
            this.JobImageList.Images.SetKeyName(11, "ヘルプ.ico");
            this.pmnSendGuardOFF.Items.AddRange(new ToolStripItem[] { this.smnSendGuardOFF });
            this.pmnSendGuardOFF.Name = "contextMenuStrip2";
            this.pmnSendGuardOFF.Size = new System.Drawing.Size(0xc1, 0x1a);
            this.smnSendGuardOFF.Name = "smnSendGuardOFF";
            this.smnSendGuardOFF.Size = new System.Drawing.Size(0xc0, 0x16);
            this.smnSendGuardOFF.Text = "Bật t\x00ednh năng gửi lại";
            this.smnSendGuardOFF.Click += new EventHandler(this.mnSendGuardOFF_Click);
            this.OpenFileDlg.Filter = "Type(*.txt;*.csv;*.doc;*.xls;*.jpg;*.bmp;*.tif;*.pdf;*.xml;*.jet)|*.txt;*.csv;*.doc;*.xls;*.jpg;*.bmp;*.tif;*.pdf;*.xml;*.jet";
            this.OpenFileDlg.RestoreDirectory = true;
            this.SaveFileDlg.Filter = "Text file(*.txt)|*.txt|All files(*.*)|*.*";
            this.SaveFileDlg.RestoreDirectory = true;
            this.ResImageList.ImageStream = (ImageListStreamer) manager.GetObject("ResImageList.ImageStream");
            this.ResImageList.TransparentColor = Color.Transparent;
            this.ResImageList.Images.SetKeyName(0, "Completion.ico");
            this.ResImageList.Images.SetKeyName(1, "Error.ico");
            this.ResImageList.Images.SetKeyName(2, "Warning.ico");
            this.tmSetResField.Tick += new EventHandler(this.tmSetResField_Tick);
            style3.ForeColor = SystemColors.ControlText;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = style3;
            this.dataGridViewTextBoxColumn1.HeaderText = "コード";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 0x4a;
            this.dataGridViewTextBoxColumn2.HeaderText = "内容";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.HeaderText = "処置";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.HeaderText = "項目ID";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Width = 70;
            this.dataGridViewTextBoxColumn5.HeaderText = "";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Resizable = DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn5.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Visible = false;
            this.dataGridViewTextBoxColumn5.Width = 5;
            this.dataGridViewTextBoxColumn6.HeaderText = "行番号";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Visible = false;
            this.dataGridViewTextBoxColumn7.HeaderText = "業務項番";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Visible = false;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(0x3f8, 0x2de);
            base.Controls.Add(container);
            base.Controls.Add(this.MainMenu);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "CommonJobForm";
            base.Load += new EventHandler(this.CommonJobForm_Load);
            base.SizeChanged += new EventHandler(this.CommonJobForm_SizeChanged);
            container.ContentPanel.ResumeLayout(false);
            container.TopToolStripPanel.ResumeLayout(false);
            container.TopToolStripPanel.PerformLayout();
            container.ResumeLayout(false);
            container.PerformLayout();
            this.pnlCommon.ResumeLayout(false);
            this.pnlResultCode.ResumeLayout(false);
            this.gbResult.ResumeLayout(false);
            ((ISupportInitialize) this.gvResult).EndInit();
            this.pnlGuide.ResumeLayout(false);
            this.gbGuide.ResumeLayout(false);
            this.pnlHeaderInfo.ResumeLayout(false);
            this.pnlHeaderInfo.PerformLayout();
            this.gbAttach.ResumeLayout(false);
            this.gbInputInfo.ResumeLayout(false);
            this.gbInputInfo.PerformLayout();
            this.PopMenu.ResumeLayout(false);
            this.stPnlUser.EndInit();
            this.stPnlSendGuard.EndInit();
            this.ToolStrip.ResumeLayout(false);
            this.ToolStrip.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.pmnSendGuardOFF.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected bool IsAttach(string jCode, string dCode)
        {
            int num = 0;
            AbstractJobConfig config = null;
            DivideConfig config2 = null;
            NormalConfig config3 = null;
            if (dCode != "")
            {
                num = JobConfigFactory.createJobConfig(jCode, dCode, DateTime.Now, out config);
            }
            else
            {
                num = JobConfigFactory.createJobConfig(jCode, DateTime.Now, out config);
            }
            if (num >= 0)
            {
                if (config.jobStyle == JobStyle.normal)
                {
                    config3 = (NormalConfig) config;
                    return config3.record.attach;
                }
                if (config.jobStyle == JobStyle.divide)
                {
                    config2 = (DivideConfig) config;
                    return config2.record.attach;
                }
            }
            return false;
        }

        protected int IsValidJob(AbstractJobConfig config)
        {
            UserEnvironment environment = UserEnvironment.CreateInstance();
            SystemEnvironment environment2 = SystemEnvironment.CreateInstance();
            int num = 0;
            if (config.erase)
            {
                return -8;
            }
            if (!environment2.TerminalInfo.Debug || !environment.DebugFunction.UserKindFlag)
            {
                switch (environment2.TerminalInfo.UserKind)
                {
                    case 1:
                        if (config.general)
                        {
                            return num;
                        }
                        return -8;

                    case 2:
                        if (config.bank)
                        {
                            return num;
                        }
                        return -8;

                    case 3:
                        if (config.center)
                        {
                            return num;
                        }
                        return -8;

                    case 4:
                        if (config.kiosk)
                        {
                            return num;
                        }
                        return -8;
                }
            }
            return num;
        }

        private void Link_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem) sender;
            XmlElement linkDst = this.SearchLinkElement(this.LinkElement, item.Name);
            IData data = this.ExecGetLinkData(linkDst);
            if ((data != null) && (this.OnCreateLinkForm != null))
            {
                this.OnCreateLinkForm(this, data, this.stLinkFileName, int.Parse(item.Name));
            }
        }

        protected virtual IData MakeExNaccsData(Naccs.Common.Generator.ItemInfo.JobDataStatus jds)
        {
            return null;
        }

        private void mnAllCheck_Click(object sender, EventArgs e)
        {
            CommonJobPanel commonJobPanel = this.GetCommonJobPanel();
            if (commonJobPanel != null)
            {
                commonJobPanel.CheckAll("1", commonJobPanel);
            }
        }

        private void mnAllCheckOut_Click(object sender, EventArgs e)
        {
            CommonJobPanel commonJobPanel = this.GetCommonJobPanel();
            if (commonJobPanel != null)
            {
                commonJobPanel.CheckAll("0", commonJobPanel);
            }
        }

        private void mnAllClear_Click(object sender, EventArgs e)
        {
            CommonJobPanel commonJobPanel = this.GetCommonJobPanel();
            if (commonJobPanel != null)
            {
                Control activeControl = commonJobPanel.ActiveEditingDataGrid();
                if (activeControl == null)
                {
                    activeControl = commonJobPanel.ActiveControl;
                }
                if (this.flgFieldClearMsg)
                {
                    string text = Resources.ResourceManager.GetString("CORE27");
                    if (this.Style != "normal")
                    {
                        text = Resources.ResourceManager.GetString("CORE28") + "\r\n" + Resources.ResourceManager.GetString("CORE27");
                    }
                    if (MessageBox.Show(this, text, Resources.ResourceManager.GetString("TITLE"), MessageBoxButtons.OKCancel) != DialogResult.OK)
                    {
                        return;
                    }
                }
                this.CmnClearJob();
                commonJobPanel.ItemClearAll();
                this.InitCtlColor();
                if (activeControl != null)
                {
                    activeControl.Select();
                }
            }
        }

        private void mnAppendJobData_Click(object sender, EventArgs e)
        {
            IData data = null;
            string str = null;
            data = this.CmnAppendJob(true);
            if (data != null)
            {
                if (this.OnAppend != null)
                {
                    str = this.OnAppend(this, data);
                }
                this.ExNaccsData.ID = str;
                this.ExNaccsData.Status = DataStatus.Send;
            }
        }

        private void mnAttachAppend_Click(object sender, EventArgs e)
        {
            if (this.BoolAttach)
            {
                if (this.lvAttach.Items.Count >= 10)
                {
                    MessageDialog dialog = new MessageDialog();
                    dialog.ShowMessage("E522", "");
                    dialog.Dispose();
                }
                else
                {
                    this.OpenFileDlg.Filter = "Type(*.txt;*.csv;*.doc;*.xls;*.jpg;*.bmp;*.tif;*.pdf;*.xml;*.docx;*.xlsx;*.jpeg;*.html;*.htm;*.gif;*.tiff)|*.txt;*.csv;*.doc;*.xls;*.jpg;*.bmp;*.tif;*.pdf;*.xml;*.docx;*.xlsx;*.jpeg;*.html;*.htm;*.gif;*.tiff";
                    if (this.OpenFileDlg.ShowDialog() == DialogResult.OK)
                    {
                        FileInfo info = new FileInfo(this.OpenFileDlg.FileName);
                        double length = info.Length;
                        int num2 = this.SizeToKBSize(length);
                        for (int i = 0; i < this.lvAttach.Items.Count; i++)
                        {
                            string s = this.lvAttach.Items[i].SubItems[1].Text.Replace("KB", "");
                            num2 += int.Parse(s);
                        }
                        if (num2 > 0xbb8)
                        {
                            MessageDialog dialog2 = new MessageDialog();
                            dialog2.ShowMessage("E523", "");
                            dialog2.Dispose();
                        }
                        else
                        {
                            try
                            {
                                string fileName = Path.GetFileName(this.OpenFileDlg.FileName);
                                string[] strArray = this.OpenFileDlg.Filter.Split(new char[] { '|' });
                                if (!strArray[1].Contains(Path.GetExtension(fileName)) || (Path.GetExtension(this.OpenFileDlg.FileName) == ""))
                                {
                                    MessageDialog dialog3 = new MessageDialog();
                                    dialog3.ShowMessage("E525", strArray[1], "");
                                    dialog3.Dispose();
                                    return;
                                }
                                fileName = this.stAttachPath + Path.AltDirectorySeparatorChar + fileName;
                                File.Copy(this.OpenFileDlg.FileName, fileName, true);
                                FileAttributes fileAttributes = File.GetAttributes(fileName);
                                if ((fileAttributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                                {
                                    fileAttributes &= ~FileAttributes.ReadOnly;
                                }
                                File.SetAttributes(fileName, fileAttributes);
                            }
                            catch (Exception exception)
                            {
                                MessageDialog dialog4 = new MessageDialog();
                                dialog4.ShowMessage("E524", "", null, exception);
                                dialog4.Dispose();
                            }
                            this.ShowAttachInfo(this.stAttachPath);
                        }
                    }
                }
            }
        }

        private void mnAttachDelete_Click(object sender, EventArgs e)
        {
            if (this.BoolAttach)
            {
                try
                {
                    if (this.lvAttach.SelectedItems.Count > 0)
                    {
                        for (int i = this.lvAttach.SelectedItems.Count - 1; i > -1; i--)
                        {
                            File.Delete(this.stAttachPath + Path.AltDirectorySeparatorChar + this.lvAttach.SelectedItems[i].Text);
                        }
                        this.ShowAttachInfo(this.stAttachPath);
                    }
                }
                catch (Exception exception)
                {
                    MessageDialog dialog = new MessageDialog();
                    dialog.ShowMessage("E521", "", null, exception);
                    dialog.Dispose();
                }
            }
        }

        private void mnAttachOpen_Click(object sender, EventArgs e)
        {
            if (this.BoolAttach)
            {
                try
                {
                    if (this.lvAttach.SelectedItems.Count > 0)
                    {
                        Process.Start(this.stAttachPath + @"\" + this.lvAttach.SelectedItems[0].Text);
                    }
                }
                catch (Exception exception)
                {
                    MessageDialog dialog = new MessageDialog();
                    dialog.ShowMessage("E520", "", null, exception);
                    dialog.Dispose();
                }
            }
        }

        private void mnChangeActivePage_Click(object sender, EventArgs e)
        {
            this.NextTab();
        }

        private void mnClearCurrentToEnd_Click(object sender, EventArgs e)
        {
            CommonJobPanel commonJobPanel = this.GetCommonJobPanel();
            if (commonJobPanel != null)
            {
                Control activeControl = commonJobPanel.ActiveEditingDataGrid();
                if (activeControl == null)
                {
                    activeControl = commonJobPanel.ActiveControl;
                }
                if (this.flgFieldClearMsg)
                {
                    string text = Resources.ResourceManager.GetString("CORE27");
                    if (this.Style != "normal")
                    {
                        text = Resources.ResourceManager.GetString("CORE28") + "\r\n" + Resources.ResourceManager.GetString("CORE27");
                    }
                    if (MessageBox.Show(this, text, Resources.ResourceManager.GetString("TITLE"), MessageBoxButtons.OKCancel) != DialogResult.OK)
                    {
                        return;
                    }
                }
                this.CmnClearJob();
                commonJobPanel.ItemClearCurrentToEnd();
                this.InitCtlColor();
                if (activeControl != null)
                {
                    activeControl.Select();
                }
            }
        }

        private void mnCommonClear_Click(object sender, EventArgs e)
        {
            CommonJobPanel commonJobPanel = this.GetCommonJobPanel();
            if (commonJobPanel != null)
            {
                Control activeControl = commonJobPanel.ActiveEditingDataGrid();
                if (activeControl == null)
                {
                    activeControl = commonJobPanel.ActiveControl;
                }
                if (this.flgFieldClearMsg)
                {
                    string text = Resources.ResourceManager.GetString("CORE27");
                    if (this.Style != "normal")
                    {
                        text = Resources.ResourceManager.GetString("CORE28") + "\r\n" + Resources.ResourceManager.GetString("CORE27");
                    }
                    if (MessageBox.Show(this, text, Resources.ResourceManager.GetString("TITLE"), MessageBoxButtons.OKCancel) != DialogResult.OK)
                    {
                        return;
                    }
                }
                this.CmnClearJob();
                commonJobPanel.ItemClearCommon();
                this.InitCtlColor();
                if (activeControl != null)
                {
                    activeControl.Select();
                }
            }
        }

        private void mnCopy_Click(object sender, EventArgs e)
        {
            this.DoEdtTextMenu(StandardCommands.Copy);
        }

        private void mnCsvCommon_Click(object sender, EventArgs e)
        {
            this.CsvFileOpen("");
        }

        private void mnCsvRan_Click(object sender, EventArgs e)
        {
            this.CsvFileOpen(Naccs.Common.Generator.ItemInfo.tblRanName);
        }

        private void mnCut_Click(object sender, EventArgs e)
        {
            this.DoEdtTextMenu(StandardCommands.Cut);
        }

        private void mnDefaultSize_Click(object sender, EventArgs e)
        {
            this.ExecChangeFontSize(ModeFont.normal);
        }

        private void mnEdit_DropDownOpening(object sender, EventArgs e)
        {
            bool flag = false;
            CommonJobPanel commonJobPanel = this.GetCommonJobPanel();
            if (commonJobPanel != null)
            {
                flag = commonJobPanel.ActiveEditingDataGrid() != null;
            }
            this.mnRowPaste.Enabled = !flag;
            this.mnGridPaste.Enabled = flag;
            this.mnGridInsert.Enabled = flag;
            this.mnGridRemove.Enabled = flag;
        }

        private void mnFileOpen_Click(object sender, EventArgs e)
        {
            string fileName = null;
            this.OpenFileDlg.InitialDirectory = this.GetInitialDirectory();
            this.OpenFileDlg.Filter = "Text file(*.txt)|*.txt|All files(*.*)|*.*";
            if (this.OpenFileDlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fileName = this.OpenFileDlg.FileName;
                    IData data = DataFactory.LoadFromEdiFile(fileName);
                    if ((data.Header.JobCode == this.ExNaccsData.Header.JobCode) && (data.Header.DispCode == this.ExNaccsData.Header.DispCode))
                    {
                        this.ExNaccsData = data;
                    }
                    else
                    {
                        if (this._style == "combi")
                        {
                            this.SendCreateNewFormMsg(data);
                        }
                        else if (this.ChoiceSetJobForm(data))
                        {
                            this.ExNaccsData.JobData = data.JobData;
                            goto Label_00CD;
                        }
                        return;
                    }
                Label_00CD:
                    this.ExNaccsData.Header.IndexInfo = "";
                    this.AddSendData(this.ExNaccsData, false);
                }
                catch (Exception exception)
                {
                    MessageDialog dialog = new MessageDialog();
                    dialog.ShowMessage("E302", fileName, null, exception);
                    dialog.Dispose();
                }
            }
        }

        private void mnFileSaveAs_Click(object sender, EventArgs e)
        {
            string fileName = null;
            if (this.CheckError(false))
            {
                this.SaveFileDlg.InitialDirectory = this.GetInitialDirectory();
                if (this.SaveFileDlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        fileName = this.SaveFileDlg.FileName;
                        DataFactory.SaveToEdiFile(this.MakeExNaccsData(Naccs.Common.Generator.ItemInfo.JobDataStatus.jdSend), fileName);
                    }
                    catch (Exception exception)
                    {
                        MessageDialog dialog = new MessageDialog();
                        dialog.ShowMessage("E303", fileName, null, exception);
                        dialog.Dispose();
                    }
                }
            }
        }

        private void mnFileSend_Click(object sender, EventArgs e)
        {
            if (this.OnRepeatSend != null)
            {
                this.OnRepeatSend(this);
            }
        }

        private void mnGridInsert_Click(object sender, EventArgs e)
        {
            CommonJobPanel commonJobPanel = this.GetCommonJobPanel();
            if (commonJobPanel != null)
            {
                LBDataGridView view = commonJobPanel.ActiveEditingDataGrid();
                if ((view != null) && commonJobPanel.InsertLines(false))
                {
                    this.CmnClearJob();
                    this.InitCtlColor();
                    view.Select();
                }
            }
        }

        private void mnGridPaste_Click(object sender, EventArgs e)
        {
            CommonJobPanel commonJobPanel = this.GetCommonJobPanel();
            if (commonJobPanel != null)
            {
                Control activeControl = commonJobPanel.ActiveControl;
                if ((activeControl is LBDataGridView) || (activeControl is LBTextBoxEditingControl))
                {
                    commonJobPanel.GridPasteTsvData();
                    activeControl.Select();
                }
            }
        }

        private void mnGridRemove_Click(object sender, EventArgs e)
        {
            CommonJobPanel commonJobPanel = this.GetCommonJobPanel();
            if (commonJobPanel != null)
            {
                LBDataGridView view = commonJobPanel.ActiveEditingDataGrid();
                if ((view != null) && commonJobPanel.RemoveLines())
                {
                    this.CmnClearJob();
                    this.InitCtlColor();
                    view.Select();
                }
            }
        }

        private void mnGuidance_Click(object sender, EventArgs e)
        {
            string jobcode = "";
            string str2 = "";
            string path = "";
            PathInfo info = PathInfo.CreateInstance();
            DivideConfig config = null;
            NormalConfig jobConfig = null;
            CombiConfig config3 = null;
            if (this.JobConfig.jobStyle == JobStyle.normal)
            {
                jobConfig = (NormalConfig) this.JobConfig;
                if (jobConfig.record.jobcode != null)
                {
                    jobcode = jobConfig.record.jobcode;
                }
                if ((jobConfig.record.dispcode != null) && (jobConfig.record.dispcode != ""))
                {
                    str2 = "." + jobConfig.record.dispcode;
                }
            }
            else if (this.JobConfig.jobStyle == JobStyle.divide)
            {
                config = (DivideConfig) this.JobConfig;
                if (config.record.jobcode != null)
                {
                    jobcode = config.record.jobcode;
                }
                if ((config.record.dispcode != null) && (config.record.dispcode != ""))
                {
                    str2 = "." + config.record.dispcode;
                }
            }
            else
            {
                config3 = (CombiConfig) this.JobConfig;
                if (config3.jobcode != null)
                {
                    jobcode = config3.jobcode;
                }
                if ((config3.dispcode != null) && (config3.dispcode != ""))
                {
                    str2 = "." + config3.dispcode;
                }
            }
            path = info.GuidanceRoot + jobcode + str2 + "_guidance.htm";
            if (File.Exists(path))
            {
                Process.Start(path);
            }
            else
            {
                MessageDialog dialog = new MessageDialog();
                dialog.ShowMessage("W302", "");
                dialog.Close();
                dialog.Dispose();
            }
        }

        private void mnHardCopy_Click(object sender, EventArgs e)
        {
            new PrintManager().HardCopyPrint(this);
        }

        private void mnHardCopyPreview_Click(object sender, EventArgs e)
        {
            new PrintManager().HardCopyPrintPreview(this);
        }

        private void mnJobClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void mnJobNew_Click(object sender, EventArgs e)
        {
            if (this.OnOpenJobForm != null)
            {
                this.OnOpenJobForm(this);
            }
        }

        private void mnPaste_Click(object sender, EventArgs e)
        {
            this.DoEdtTextMenu(StandardCommands.Paste);
        }

        protected void mnPrintJobData_Click(object sender, EventArgs e)
        {
            try
            {
                this.CommitGridData();
                IData data = null;
                data = this.MakeExNaccsData(Naccs.Common.Generator.ItemInfo.JobDataStatus.jdNone);
                data.Contained = ContainedType.SendReceive;
                new PrintManager().Print(this, "", data, 0);
            }
            catch (Exception exception)
            {
                MessageDialog dialog = new MessageDialog();
                dialog.ShowMessage("E609", "", null, exception);
                dialog.Dispose();
            }
        }

        protected void mnPrintJobDataPreview_Click(object sender, EventArgs e)
        {
            try
            {
                this.CommitGridData();
                IData data = null;
                data = this.MakeExNaccsData(Naccs.Common.Generator.ItemInfo.JobDataStatus.jdNone);
                data.Contained = ContainedType.SendReceive;
                new PrintManager().PrintPreview(this, "", data);
            }
            catch (Exception exception)
            {
                MessageDialog dialog = new MessageDialog();
                dialog.ShowMessage("E609", "", null, exception);
                dialog.Dispose();
            }
        }

        private void mnRedo_Click(object sender, EventArgs e)
        {
            if (this.OnRepetInput != null)
            {
                this.OnRepetInput(this);
            }
        }

        private void mnRowCopy_Click(object sender, EventArgs e)
        {
            CommonJobPanel commonJobPanel = this.GetCommonJobPanel();
            if (commonJobPanel != null)
            {
                Control activeControl = commonJobPanel.ActiveControl;
                if ((activeControl is LBDataGridView) || (activeControl is LBTextBoxEditingControl))
                {
                    commonJobPanel.GridCopy();
                }
                else
                {
                    commonJobPanel.CopyTsvData();
                }
            }
        }

        private void mnRowPaste_Click(object sender, EventArgs e)
        {
            CommonJobPanel commonJobPanel = this.GetCommonJobPanel();
            if (commonJobPanel != null)
            {
                Control activeControl = commonJobPanel.ActiveControl;
                if (!(activeControl is LBDataGridView) && !(activeControl is LBTextBoxEditingControl))
                {
                    commonJobPanel.PasteTsvData();
                }
            }
        }

        private void mnSelectRanClear_Click(object sender, EventArgs e)
        {
            CommonJobPanel commonJobPanel = this.GetCommonJobPanel();
            if (commonJobPanel != null)
            {
                Control activeControl = commonJobPanel.ActiveEditingDataGrid();
                if (activeControl == null)
                {
                    activeControl = commonJobPanel.ActiveControl;
                }
                if (this.flgFieldClearMsg)
                {
                    string text = Resources.ResourceManager.GetString("CORE27");
                    if (this.Style != "normal")
                    {
                        text = Resources.ResourceManager.GetString("CORE28") + "\r\n" + Resources.ResourceManager.GetString("CORE27");
                    }
                    if (MessageBox.Show(this, text, Resources.ResourceManager.GetString("TITLE"), MessageBoxButtons.OKCancel) != DialogResult.OK)
                    {
                        return;
                    }
                }
                this.CmnClearJob();
                commonJobPanel.ItemClearCloseUp();
                this.InitCtlColor();
                if (activeControl != null)
                {
                    activeControl.Select();
                }
            }
        }

        private void mnSend_Click(object sender, EventArgs e)
        {
            this.ExecSend();
        }

        private void mnSendGuardOFF_Click(object sender, EventArgs e)
        {
            if (this.BoolSendGuard)
            {
                this.BoolSendGuard = !this.BoolSendGuard;
            }
        }

        private void mnSeqLoadDt_Click(object sender, EventArgs e)
        {
            if (this.OnSeqLoad != null)
            {
                this.OnSeqLoad(this);
            }
        }

        private void mnShowHint_Click(object sender, EventArgs e)
        {
            CommonJobPanel commonJobPanel = this.GetCommonJobPanel();
            if (commonJobPanel != null)
            {
                Control activeControl = commonJobPanel.ActiveControl;
                if (activeControl != null)
                {
                    string stID = null;
                    System.Type c = activeControl.GetType();
                    if (typeof(IItemAttributes).IsAssignableFrom(c))
                    {
                        IItemAttributes attributes = (IItemAttributes) activeControl;
                        stID = attributes.id;
                    }
                    else if (activeControl is LBDataGridView)
                    {
                        LBDataGridView view = (LBDataGridView) activeControl;
                        if (view.SelectedCells.Count == 0)
                        {
                            return;
                        }
                        int columnIndex = view.SelectedCells[0].ColumnIndex;
                        LBTextBoxColumn column = (LBTextBoxColumn) view.Columns[columnIndex];
                        stID = column.id;
                    }
                    else if (activeControl is LBTextBoxEditingControl)
                    {
                        LBTextBoxEditingControl control2 = (LBTextBoxEditingControl) activeControl;
                        stID = control2.id;
                    }
                    if (stID != null)
                    {
                        string hintText = commonJobPanel.GetHintText(stID);
                        if (hintText != null)
                        {
                            MessageBox.Show(this, hintText, "Field", MessageBoxButtons.OK);
                        }
                    }
                }
            }
        }

        private void mnSystem_Click(object sender, EventArgs e)
        {
            if (this.OnOptionShow != null)
            {
                this.OnOptionShow(this);
            }
        }

        private void mnUndo_Click(object sender, EventArgs e)
        {
            this.DoEdtTextMenu(StandardCommands.Undo);
        }

        private void mnUpdateJobData_Click(object sender, EventArgs e)
        {
            IData data = null;
            string str = null;
            data = this.CmnAppendJob(false);
            if (data != null)
            {
                if (this.OnAppend != null)
                {
                    str = this.OnAppend(this, data);
                }
                this.ExNaccsData.ID = str;
                this.ExNaccsData.Status = DataStatus.Send;
            }
        }

        private void mnZoomIn_Click(object sender, EventArgs e)
        {
            this.ExecChangeFontSize(ModeFont.large);
        }

        private void mnZoomOut_Click(object sender, EventArgs e)
        {
            this.ExecChangeFontSize(ModeFont.small);
        }

        protected virtual void NextTab()
        {
        }

        private void PopMenu_Opening(object sender, CancelEventArgs e)
        {
            bool flag = false;
            CommonJobPanel commonJobPanel = this.GetCommonJobPanel();
            if (commonJobPanel != null)
            {
                flag = commonJobPanel.ActiveEditingDataGrid() != null;
            }
            this.pmnRowPaste.Enabled = !flag;
            this.pmnGridPaste.Enabled = flag;
            this.pmnGridInsert.Enabled = flag;
            this.pmnGridRemove.Enabled = flag;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((keyData == Keys.PageUp) || (keyData == Keys.Next))
            {
                CommonJobPanel commonJobPanel = this.GetCommonJobPanel();
                if (commonJobPanel == null)
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                if (commonJobPanel.lbNavigator.Visible)
                {
                    if (keyData == Keys.PageUp)
                    {
                        commonJobPanel.bindingNavigatorMovePreviousItem_Click(null, null);
                    }
                    if (keyData == Keys.Next)
                    {
                        commonJobPanel.bindingNavigatorMoveNextItem_Click(null, null);
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected void ReadGuideFile(string GuideFile)
        {
            this.docGuide = new XmlDocument();
            try
            {
                this.docGuide.Load(GuideFile);
            }
            catch
            {
                Console.WriteLine("Failure in saving the input items guide file.(CommonJobForm.ReadGuideFile)");
                Console.WriteLine("(" + GuideFile + ")");
                this.docGuide = null;
            }
        }

        private void ReadJobConfig(string FileName)
        {
            JobSettings settings = JobSettings.CreateInstance();
            base.Width = settings.SizeW;
            base.Height = settings.SizeH;
            this.SystemMode = settings.SysMode;
            base.WindowState = settings.WindowState;
            this.FMode = settings.FontMode;
            this.CmnPnlHeight = settings.PnlRegularSizeH;
            this.pnlHeaderInfo.Height = settings.PnlHeaderSizeH;
            this.pnlGuide.Height = settings.PnlGuideSizeH;
            this.pnlResultCode.Height = settings.PnlResCodeSizeH;
            this.pnlCommon.Width = settings.PnlCommonSizeW;
        }

        protected void ReadLinkFile(string linkFileName)
        {
            this.LinkElement = null;
            XmlDocument document = null;
            document = new XmlDocument();
            try
            {
                if (!File.Exists(linkFileName))
                {
                    this.pmnJobLink.Enabled = false;
                }
                else
                {
                    document.Load(linkFileName);
                    this.LinkElement = document.DocumentElement;
                    this.CreateLinkMenu(this.LinkElement, this.pmnJobLink);
                    if (!string.IsNullOrEmpty(document.InnerXml) && (document.InnerXml.Contains("category") || document.InnerXml.Contains("destination")))
                    {
                        this.pmnJobLink.Enabled = true;
                    }
                    else
                    {
                        this.pmnJobLink.Enabled = false;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Failure in reading in the service link information file node.(CommonJobForm.ReadLinkFile)");
                Console.WriteLine("(" + linkFileName + ")");
            }
        }

        private void SaveJobConfig(string FileName)
        {
            JobSettings settings = JobSettings.CreateInstance();
            if (base.WindowState == FormWindowState.Normal)
            {
                settings.SizeW = base.Width;
                settings.SizeH = base.Height;
            }
            settings.SysMode = this.SystemMode;
            if (base.WindowState == FormWindowState.Maximized)
            {
                settings.WindowState = base.WindowState;
            }
            else
            {
                settings.WindowState = FormWindowState.Normal;
            }
            settings.FontMode = this.fMode;
            if (this.CmnPnlHeight > 0)
            {
                settings.PnlRegularSizeH = this.CmnPnlHeight;
            }
            settings.PnlHeaderSizeH = this.pnlHeaderInfo.Height;
            settings.PnlGuideSizeH = this.pnlGuide.Height;
            settings.PnlResCodeSizeH = this.pnlResultCode.Height;
            settings.PnlCommonSizeW = this.pnlCommon.Width;
            settings.SaveJobConfig();
        }

        private XmlElement SearchLinkElement(XmlElement elm, string linkNo)
        {
            foreach (XmlElement element in elm.ChildNodes)
            {
                if (element.Name.Equals("destination"))
                {
                    string attribute = element.GetAttribute("no");
                    if (linkNo.Equals(attribute))
                    {
                        return element;
                    }
                }
                else if (element.Name.Equals("category"))
                {
                    XmlElement element2 = this.SearchLinkElement(element, linkNo);
                    if (element2 != null)
                    {
                        return element2;
                    }
                }
            }
            return null;
        }

        private void SendCreateNewFormMsg(IData Data)
        {
            if (this.OnCreateNewForm != null)
            {
                this.OnCreateNewForm(this, Data);
            }
        }

        protected virtual void SetFieldError(string id, int errorPage, bool flgInputErr, int idx)
        {
        }

        protected virtual void SetFocusField(string id, int errorPage, int idx)
        {
        }

        protected virtual void SetIdData(string RepID, string stID, string stData, int No)
        {
        }

        public void SetLinkData(IData data, string linkFileName, int linkNo)
        {
            if ((data.Header.System == "1") || (data.Header.System == "2"))
            {
                this.SystemMode = data.Header.System[0];
                JobSettings.CreateInstance().SysMode = this.SystemMode;
            }
            if (this.ExecSetLinkData(data, linkFileName, linkNo))
            {
                this.ExecSend();
            }
        }

        protected void SetLinkField(int idxPanel)
        {
            try
            {
                foreach (XmlElement element in this.LinkElement.ChildNodes)
                {
                    for (int i = 0; i < element.ChildNodes.Count; i++)
                    {
                        string attribute = ((XmlElement) element.ChildNodes[i]).GetAttribute("from");
                        List<Control> lstCtrl = new List<Control>();
                        this.GetControlsFromJP(idxPanel, attribute, lstCtrl);
                        for (int j = 0; j < lstCtrl.Count; j++)
                        {
                            if (lstCtrl[j] is LBDataGridView)
                            {
                                ((LBDataGridView) lstCtrl[j]).SetLinkField(attribute);
                            }
                            else
                            {
                                lstCtrl[j].ForeColor = DesignControls.LinkForeColor;
                            }
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("Failure in reading in the service link information file node.(CommonJobForm.SetLinkField)");
            }
        }

        public virtual void SetRecvData(IData data)
        {
        }

        public virtual void SetRes(IData data, bool flgSetColor)
        {
            bool flag = false;
            XmlElement documentElement = null;
            XmlElement element2 = null;
            this.ClearResErr();
            string msgFile = this.GetMsgFile(data.JobCode);
            if (File.Exists(msgFile))
            {
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(msgFile);
                    documentElement = document.DocumentElement;
                }
                catch
                {
                    Console.WriteLine("Failure in reading in the service error message collection file.(CommonJobForm.SetRes)");
                    Console.WriteLine("(" + msgFile + ")");
                }
            }
            string systemErrorMessage = PathInfo.CreateInstance().SystemErrorMessage;
            if (File.Exists(systemErrorMessage))
            {
                XmlDocument document2 = new XmlDocument();
                try
                {
                    document2.Load(systemErrorMessage);
                    element2 = document2.DocumentElement;
                }
                catch
                {
                    Console.WriteLine("Failure in reading in the common error message collection file.(CommonJobForm.SetRes)");
                    Console.WriteLine("(" + systemErrorMessage + ")");
                }
            }
            for (int i = 0; i < data.Items[0].Length; i += 15)
            {
                string str3 = data.Items[0].Substring(i, 15);
                switch (str3)
                {
                    case "               ":
                        break;

                    case "00000-0000-0000":
                        this.gvResult.Rows.Add(new object[] { this.ResImageList.Images[0], "COMPLETION", "", "", "", "", "" });
                        break;

                    default:
                    {
                        string str4 = str3.Substring(0, 5);
                        string str5 = str3.Substring(6, 4).Trim();
                        string str6 = str3.Substring(11, 4).Trim();
                        string innerText = "";
                        string str8 = "";
                        try
                        {
                            XmlNode node = null;
                            if (str4[0] == 'A')
                            {
                                if (element2 != null)
                                {
                                    node = element2.SelectSingleNode("descendant::response[@code='" + str4 + "']");
                                }
                            }
                            else if (documentElement != null)
                            {
                                node = documentElement.SelectSingleNode("descendant::response[@code='" + str4 + "']");
                            }
                            if (node == null)
                            {
                                innerText = "";
                                str8 = "";
                            }
                            else
                            {
                                foreach (XmlElement element3 in node)
                                {
                                    if (element3.LocalName == "description")
                                    {
                                        innerText = element3.InnerText;
                                    }
                                    if (element3.LocalName == "disposition")
                                    {
                                        str8 = element3.InnerText;
                                    }
                                }
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Failure in reading in the service error message collection file node.(CommonJobForm.SetRes)");
                            Console.WriteLine("(" + msgFile + ")");
                        }
                        if (str4[0] == 'A')
                        {
                            this.gvResult.Rows.Add(new object[] { this.ResImageList.Images[1], str4, innerText, str8, str5, "", str6, "" });
                        }
                        else if (str4[0] == 'W')
                        {
                            this.gvResult.Rows.Add(new object[] { this.ResImageList.Images[2], str4, innerText, str8, str5, "", str6, "" });
                            flag = true;
                        }
                        else
                        {
                            this.gvResult.Rows.Add(new object[] { this.ResImageList.Images[1], str4, innerText, str8, str5, "", str6, "" });
                        }
                        for (int j = 0; j < this.gvResult.ColumnCount; j++)
                        {
                            DataGridViewCell cell = this.gvResult.Rows[this.gvResult.Rows.Count - 1].Cells[j];
                            if (str4[0] == 'A')
                            {
                                cell.Style.BackColor = DesignControls.SysErrorColor;
                            }
                            else if (str4[0] == 'W')
                            {
                                cell.Style.BackColor = DesignControls.WarningColor;
                            }
                            else
                            {
                                cell.Style.BackColor = DesignControls.ErrorColor;
                            }
                        }
                        break;
                    }
                }
            }
            this.BoolSendGuard = data.Header.SendGuard == "*";
            if (data.Header.IndexInfo.Trim() != "")
            {
                this.ExNaccsData.Header.IndexInfo = data.Header.IndexInfo;
            }
            if (flag && this.flgWarningBeep)
            {
                for (int k = 0; k < 2; k++)
                {
                    Console.Beep(0x106, 100);
                    Console.Beep(370, 100);
                }
            }
            if (flgSetColor)
            {
                this.InitCtlColor();
                this.CursorTop(this.pnlGuiLayout);
                this.SetResField();
            }
        }

        private void SetResField()
        {
            string id = "";
            int errorPage = -1;
            string s = "";
            for (int i = 0; i < this.gvResult.Rows.Count; i++)
            {
                string str3 = (string) this.gvResult[4, i].Value;
                string str4 = (string) this.gvResult[6, i].Value;
                string str5 = (string) this.gvResult[7, i].Value;
                if (((str3 != "0000") && !string.IsNullOrEmpty(str3)) && !string.IsNullOrEmpty(str4))
                {
                    if (string.IsNullOrEmpty(str5))
                    {
                        str5 = "0";
                    }
                    this.SetFieldError(str3, int.Parse(str4), false, int.Parse(str5));
                    if ((id == "") && (errorPage == -1))
                    {
                        id = str3;
                        errorPage = int.Parse(str4);
                        s = str5;
                    }
                }
            }
            if ((id != "") && (errorPage != -1))
            {
                this.SetFocusField(id, errorPage, int.Parse(s));
            }
        }

        public virtual void SetSendData(IData data)
        {
        }

        public void SetSeqSendData(IData data)
        {
            if ((data.Header.JobCode == this.ExNaccsData.Header.JobCode) && (data.Header.DispCode == this.ExNaccsData.Header.DispCode))
            {
                this.ExNaccsData.Header.IndexInfo = "";
                this.AddSendData(data, true);
                this.InitCtlColor();
            }
            else if (this.ChoiceSetJobForm(data))
            {
                this.ExNaccsData.JobData = data.JobData;
                this.ExNaccsData.Header.IndexInfo = "";
                this.AddSendData(data, true);
                this.InitCtlColor();
            }
        }

        protected void SetTitle(string Title, string JCode, string DCode)
        {
            if (SystemEnvironment.CreateInstance().TerminalInfo.UserKind == 4)
            {
                this.Text = Title;
            }
            else if (DCode != "")
            {
                this.Text = string.Concat(new object[] { JCode, '.', DCode, ' ', Title });
            }
            else
            {
                this.Text = JCode + ' ' + Title;
            }
        }

        protected void ShowAttachInfo(IData Data)
        {
            try
            {
                if (this.BoolAttach)
                {
                    string str = string.Format("{0}{1}{2}", Data.Header.JobCode.Trim(), Data.Header.DispCode.Trim(), DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    this.stAttachPath = Path.Combine(Path.GetTempPath(), str);
                }
                if (!string.IsNullOrEmpty(Data.AttachFolder))
                {
                    if (Directory.Exists(this.stAttachPath))
                    {
                        Directory.Delete(this.stAttachPath, true);
                    }
                    CopyDirectory(Data.AttachFolder, this.stAttachPath);
                }
                this.ShowAttachInfo(this.stAttachPath);
            }
            catch
            {
                Console.WriteLine("Failure in copying to the attachment folder form:{0} to{1}", Data.AttachFolder, this.stAttachPath);
            }
        }

        protected void ShowAttachInfo(string stPath)
        {
            if (this.BoolAttach)
            {
                if (!Directory.Exists(stPath))
                {
                    Directory.CreateDirectory(stPath);
                }
                this.lvAttach.Items.Clear();
                foreach (string str in Directory.GetFiles(stPath, "*", SearchOption.TopDirectoryOnly))
                {
                    string fileName = Path.GetFileName(str);
                    FileInfo info = new FileInfo(str);
                    double length = info.Length;
                    int num2 = this.SizeToKBSize(length);
                    ListViewItem item = new ListViewItem(new string[] { fileName, num2.ToString() + "KB" }, -1);
                    this.lvAttach.Items.AddRange(new ListViewItem[] { item });
                }
            }
        }

        protected void ShowGuide(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (control is IItemAttributes)
            {
                IItemAttributes attributes = (IItemAttributes) control;
                this.rtxtGuide.Text = this.getGuide(attributes.id);
            }
            else if (control is LBDataGridView)
            {
                LBDataGridView view = (LBDataGridView) control;
                string aColumnID = view.AColumnID;
                this.rtxtGuide.Text = this.getGuide(aColumnID);
            }
            else
            {
                this.rtxtGuide.Clear();
            }
        }

        private int SizeToKBSize(double size)
        {
            double a = size / 1024.0;
            return (int) Math.Ceiling(a);
        }

        private void statusBar_PanelClick(object sender, StatusBarPanelClickEventArgs e)
        {
            this.statusBar.ContextMenuStrip = null;
            if (this.BoolSendGuard && ((e.StatusBarPanel.Name == "stPnlSendGuard") && (e.Button == MouseButtons.Right)))
            {
                this.statusBar.ContextMenuStrip = this.pmnSendGuardOFF;
                this.pmnSendGuardOFF.Show();
            }
        }

        private void tmSetResField_Tick(object sender, EventArgs e)
        {
            this.tmSetResField.Enabled = false;
            this.SetResField();
        }

        protected bool BoolAttach
        {
            get
            {
                return this.boolAttach;
            }
            set
            {
                this.boolAttach = value;
                this.lvAttach.Enabled = value;
                this.mnAttach.Enabled = value;
                this.mnAttachAppend.Enabled = value;
                this.mnAttachDelete.Enabled = value;
                this.mnAttachOpen.Enabled = value;
                this.btnAttachAppend.Enabled = value;
            }
        }

        protected bool BoolSendGuard
        {
            get
            {
                return this.boolSendGuard;
            }
            set
            {
                this.boolSendGuard = value;
                if (value)
                {
                    this.statusBar.Panels[1].Text = "Resend prevention";
                    this.statusBar.Panels[1].Icon = (Icon) this.SendGuardIcon.Clone();
                    this.mnSendGuardOFF.Enabled = true;
                    this.pmnSendGuardOFF.Enabled = true;
                }
                else
                {
                    this.statusBar.Panels[1].Text = "";
                    this.statusBar.Panels[1].Icon = null;
                    this.mnSendGuardOFF.Enabled = false;
                    this.pmnSendGuardOFF.Enabled = false;
                }
            }
        }

        protected virtual int CmnPnlHeight
        {
            get
            {
                return 0;
            }
            set
            {
            }
        }

        protected ModeFont FMode
        {
            get
            {
                return this.fMode;
            }
            set
            {
                this.fMode = value;
                switch (value)
                {
                    case ModeFont.large:
                        this.mnZoomOut.Enabled = true;
                        this.mnDefaultSize.Enabled = true;
                        this.mnZoomIn.Enabled = false;
                        return;

                    case ModeFont.small:
                        this.mnZoomIn.Enabled = true;
                        this.mnDefaultSize.Enabled = true;
                        this.mnZoomOut.Enabled = false;
                        return;
                }
                this.mnZoomIn.Enabled = true;
                this.mnZoomOut.Enabled = true;
                this.mnDefaultSize.Enabled = false;
            }
        }

        protected bool RecvAttach
        {
            set
            {
                this.boolAttach = value;
                this.lvAttach.Enabled = value;
                this.mnAttach.Enabled = value;
                this.mnAttachAppend.Enabled = false;
                this.mnAttachDelete.Enabled = false;
                this.mnAttachOpen.Enabled = value;
                this.btnAttachAppend.Enabled = false;
            }
        }

        public DataStatus Status
        {
            get
            {
                return this.ExNaccsData.Status;
            }
            set
            {
                this.ExNaccsData.Status = value;
            }
        }

        public string Style
        {
            get
            {
                return this._style;
            }
        }

        public string UserCode
        {
            get
            {
                return this.statusBar.Panels[0].Text;
            }
            set
            {
                this.statusBar.Panels[0].Text = value;
            }
        }
    }
}

