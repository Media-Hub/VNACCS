namespace Naccs.Core.Job
{
    using Naccs.Common.CustomControls;
    using Naccs.Common.Function;
    using Naccs.Common.Generator;
    using Naccs.Common.JobConfig;
    using Naccs.Core.Classes;
    using Naccs.Core.Settings;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    public class CombiJobForm : CommonJobForm
    {
        private IContainer components;
        private int defaultCmnHeight;
        private List<CmbJPRec> JPList;
        private Panel pnlRegular;
        private Panel pnlTab;
        private System.Windows.Forms.Splitter Splitter;
        private TabControl TC;

        public CombiJobForm()
        {
            this.InitializeComponent();
            base._style = "combi";
            base.gvResult.Columns[5].Visible = true;
            base.mnCSV.Enabled = false;
            base.mnCsvCommon.Enabled = false;
            base.mnCsvRan.Enabled = false;
        }

        protected override void AddSendData(IData data, bool flgClear)
        {
            base.CmnClearJob();
            if (flgClear)
            {
                for (int j = 0; j < this.JPList.Count; j++)
                {
                    this.JPList[j].JP.Items.ClearAll();
                }
            }
            this.ClearNavigator();
            data.Style = JobStyle.combi;
            for (int i = 0; i < data.JobCount; i++)
            {
                data.JobIndex = i;
                this.JPList[i].JP.SetData(data.SubItems, Naccs.Common.Generator.ItemInfo.JobDataStatus.jdSend, "");
            }
            base.txtInputInfo.Text = data.Header.InputInfo.Trim();
            this.InitCtlColor();
        }

        protected override bool CheckError(bool flgCheckReq)
        {
            int errorPage = 0;
            string[] idArray = null;
            bool flag = true;
            string msg = null;
            if (base.flgDoCheckErr)
            {
                flag = base.CheckError(flgCheckReq);
                if (!flag)
                {
                    return flag;
                }
                for (int i = 0; i < this.JPList.Count; i++)
                {
                    int num3;
                    this.JPList[i].JobErrs.ClearInputErrorInfo();
                    this.InitPnlCtlColor(this.JPList[i].JP);
                    if (i == 0)
                    {
                        num3 = this.JPList[i].JP.SearchErrorField(out idArray, out errorPage, out msg, flgCheckReq);
                    }
                    else
                    {
                        num3 = this.JPList[i].JP.SearchErrorField(out idArray, out errorPage, out msg, false, false);
                    }
                    if (num3 != 0)
                    {
                        for (int j = 0; j < idArray.Length; j++)
                        {
                            if (((j == 0) && !string.IsNullOrEmpty(idArray[j])) && (errorPage >= 0))
                            {
                                this.SetFocusField(idArray[j], errorPage, i);
                            }
                            this.SetFieldError(idArray[j], errorPage, true, i);
                        }
                        MessageDialog dialog = new MessageDialog();
                        dialog.ShowMessage("E516", msg, "");
                        dialog.Dispose();
                        return false;
                    }
                }
            }
            return flag;
        }

        private void ClearNavigator()
        {
            for (int i = 0; i < this.JPList.Count; i++)
            {
                if (this.JPList[i].JPStatus == JobPanelStatus.Open)
                {
                    this.JPList[i].JP.LblNextPage.Text = "";
                }
            }
        }

        protected override void ClearResErr()
        {
            for (int i = 0; i < this.JPList.Count; i++)
            {
                this.JPList[i].JobErrs.Clear();
            }
            base.ClearResErr();
        }

        private int CreateJobPanel(int idxPanel)
        {
            this.JPList[idxPanel].JP.JobErrs = this.JPList[idxPanel].JobErrs;
            int num = this.JPList[idxPanel].JP.CreateCmbPanel(this.JPList[idxPanel].FileName);
            if (num != 0)
            {
                this.JPList[idxPanel].JPStatus = JobPanelStatus.Error;
                return num;
            }
            this.JPList[idxPanel].JPStatus = JobPanelStatus.Open;
            if (idxPanel == 0)
            {
                this.defaultCmnHeight = this.JPList[idxPanel].JP.DefaultHeight;
                if (this.defaultCmnHeight > 0)
                {
                    this.pnlRegular.Height = this.defaultCmnHeight;
                }
            }
            else
            {
                TabPage page = this.TC.TabPages[idxPanel - 1];
                page.Controls.Add(this.JPList[idxPanel].JP);
                this.JPList[idxPanel].JP.Dock = DockStyle.Fill;
            }
            this.JPList[idxPanel].JP.SetCorrelation(this.JPList[idxPanel].CorrelationFileName);
            this.JPList[idxPanel].JP.Load += new EventHandler(this.JP_Load);
            this.ReadHistoryFile(idxPanel);
            this.SetAutoCompleteCtrl(idxPanel);
            this.ReadCmbGuideFile(idxPanel);
            if (base.FMode != ModeFont.normal)
            {
                this.JPList[idxPanel].JP.ChangeFontSize(base.FMode);
            }
            return 0;
        }

        private int CreateLayout(CombiConfig Cconfig)
        {
            int num = 0;
            PathInfo info = PathInfo.CreateInstance();
            Directory.CreateDirectory(info.InputHistoryRoot);
            this.JPList = new List<CmbJPRec>();
            this.JPList.Add(new CmbJPRec());
            this.JPList[0].JP = new Naccs.Core.Job.JobPanel();
            this.JPList[0].JPStatus = JobPanelStatus.Closed;
            this.JPList[0].AuCtrlInfoList = new List<AuCtrlInfo>();
            if (Cconfig.configs[0].config != null)
            {
                this.JPList[0].FileName = Cconfig.configs[0].config.Display;
                this.JPList[0].CorrelationFileName = Cconfig.configs[0].config.Correlation;
                this.JPList[0].HistoryFile = info.getInputHistory(Cconfig.configs[0].code);
                this.JPList[0].GuideFile = Cconfig.configs[0].config.Guide;
                this.JPList[0].JobErrs = new JobErrInfo();
                this.JPList[0].Code = Cconfig.configs[0].code;
                this.JPList[0].JCode = Cconfig.configs[0].config.record.jobcode.Trim();
                this.JPList[0].DCode = Cconfig.configs[0].config.record.dispcode.Trim();
            }
            this.InitJobPanel(this.JPList[0].JP);
            num = this.JPList[0].JP.Items.CreateDataSet(this.JPList[0].FileName);
            if (num != 0)
            {
                return num;
            }
            this.pnlRegular.Controls.Add(this.JPList[0].JP);
            this.JPList[0].JP.Dock = DockStyle.Fill;
            this.TC = new TabControl();
            this.TC.TabStop = false;
            this.TC.Name = "tbDefault";
            this.TC.Selected += new TabControlEventHandler(this.TC_Selected);
            this.pnlTab.Controls.Add(this.TC);
            this.TC.Dock = DockStyle.Fill;
            for (int i = 1; i < Cconfig.count; i++)
            {
                this.JPList.Add(new CmbJPRec());
                this.JPList[i].JP = new Naccs.Core.Job.JobPanel();
                this.JPList[i].JPStatus = JobPanelStatus.Closed;
                this.JPList[i].AuCtrlInfoList = new List<AuCtrlInfo>();
                if (Cconfig.configs[i].config != null)
                {
                    this.JPList[i].FileName = Cconfig.configs[i].config.Display;
                    this.JPList[i].CorrelationFileName = Cconfig.configs[i].config.Correlation;
                    this.JPList[i].HistoryFile = info.getInputHistory(Cconfig.configs[i].code);
                    this.JPList[i].GuideFile = Cconfig.configs[i].config.Guide;
                    this.JPList[i].JobErrs = new JobErrInfo();
                    this.JPList[i].Code = Cconfig.configs[i].code;
                    this.JPList[i].JCode = Cconfig.configs[i].config.record.jobcode;
                    this.JPList[i].DCode = Cconfig.configs[i].config.record.dispcode;
                }
                this.InitJobPanel(this.JPList[i].JP);
                num = this.JPList[i].JP.Items.CreateDataSet(this.JPList[i].FileName);
                if (num != 0)
                {
                    return num;
                }
                TabPage page = new TabPage {
                    Name = "Tab" + i.ToString(),
                    Text = Cconfig.configs[i].tabname
                };
                this.TC.Controls.Add(page);
            }
            return 0;
        }

        public override int CreateNewForm(string jobCode, string kind, bool flgNew, bool flgJobTitle)
        {
            try
            {
                int num = base.CreateNewForm(jobCode, kind, flgNew, flgJobTitle);
                if (num == 0)
                {
                    CombiConfig jobConfig;
                    if (base.JobConfig.jobStyle == JobStyle.combi)
                    {
                        jobConfig = (CombiConfig) base.JobConfig;
                    }
                    else
                    {
                        return (num = -99);
                    }
                    for (int i = 0; i < jobConfig.configs.Length; i++)
                    {
                        int num3 = 0;
                        AbstractJobConfig config = jobConfig.configs[i].config;
                        if (config == null)
                        {
                            return -2;
                        }
                        num3 = base.IsValidJob(config);
                        if (num3 < 0)
                        {
                            return num3;
                        }
                    }
                    num = this.CreateLayout(jobConfig);
                    if (num != 0)
                    {
                        return num;
                    }
                    num += this.CreateJobPanel(0);
                    num += this.CreateJobPanel(1);
                    if (num != 0)
                    {
                        return num;
                    }
                    base.BoolAttach = false;
                    base.stAttachPath = "";
                    string title = jobConfig.title;
                    base.SetTitle(title, jobConfig.jobcode, jobConfig.dispcode);
                    base.ExNaccsData.Header.JobCode = jobConfig.jobcode;
                    base.ExNaccsData.Header.DispCode = jobConfig.dispcode;
                }
                return num;
            }
            catch
            {
                return -99;
            }
        }

        public override int CreateRecvForm(IData Data, bool flgJobTitle)
        {
            try
            {
                int num = 0;
                Data.Style = JobStyle.combi;
                num = JobConfigFactory.createJobConfig(Data.OutCode, out this.JobConfig);
                if (num >= 0)
                {
                    CombiConfig jobConfig;
                    num = base.IsValidJob(base.JobConfig);
                    if (num < 0)
                    {
                        return num;
                    }
                    if (base.JobConfig.jobStyle == JobStyle.combi)
                    {
                        jobConfig = (CombiConfig) base.JobConfig;
                    }
                    else
                    {
                        return (num = -99);
                    }
                    for (int i = 0; i < jobConfig.configs.Length; i++)
                    {
                        Data.JobIndex = i;
                        int num3 = 0;
                        AbstractJobConfig config = jobConfig.configs[i].config;
                        if (!Data.SubCode.Contains(".") && (Data.SubCode.Length == 7))
                        {
                            num3 = JobConfigFactory.createJobConfig(Data.SubCode, out config);
                        }
                        else
                        {
                            num3 = JobConfigFactory.createJobConfig(Data.SubCode, DateTime.Now, out config);
                        }
                        if (num3 < 0)
                        {
                            return num3;
                        }
                        num3 = base.IsValidJob(config);
                        if (num3 != 0)
                        {
                            return num3;
                        }
                        jobConfig.configs[i].config = (NormalConfig) config;
                        jobConfig.configs[i].code = Data.SubCode;
                        jobConfig.configs[i].order = i;
                    }
                    num = this.CreateLayout(jobConfig);
                    if (num != 0)
                    {
                        return num;
                    }
                    num += this.CreateJobPanel(0);
                    num += this.CreateJobPanel(1);
                    if (num != 0)
                    {
                        return num;
                    }
                    base.BoolAttach = false;
                    base.stAttachPath = "";
                    string title = jobConfig.title;
                    base.SetTitle(title, jobConfig.jobcode, jobConfig.dispcode);
                    base.ExNaccsData.Header.OutCode = jobConfig.jobcode;
                    base.ExNaccsData.Header.DispCode = jobConfig.dispcode;
                }
                return num;
            }
            catch
            {
                return -99;
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

        protected override void ExecChangeFontSize(ModeFont mF)
        {
            for (int i = 0; i < this.JPList.Count; i++)
            {
                if (this.JPList[i].JPStatus == JobPanelStatus.Open)
                {
                    this.JPList[i].JP.ChangeFontSize(mF);
                }
            }
            base.FMode = mF;
        }

        private int GetActivePanel()
        {
            for (int i = 0; i < this.JPList.Count; i++)
            {
                if (this.JPList[i].JP.IsActive)
                {
                    return i;
                }
            }
            return -1;
        }

        private string getCmbGuide(string id, int pnlIdx)
        {
            if (this.JPList[pnlIdx].DocGuide != null)
            {
                int length = id.Length;
                StringBuilder builder = new StringBuilder(id);
                while (length > 0)
                {
                    XmlNode node = this.JPList[pnlIdx].DocGuide.DocumentElement.SelectSingleNode("item[@id='" + builder.ToString() + "']");
                    if (node != null)
                    {
                        return node.InnerText;
                    }
                    builder[--length] = '_';
                }
            }
            return "";
        }

        protected override CommonJobPanel GetCommonJobPanel()
        {
            for (int i = 0; i < this.JPList.Count; i++)
            {
                if (this.JPList[i].JP.IsActive)
                {
                    return this.JPList[i].JP;
                }
            }
            return this.JPList[0].JP;
        }

        protected override void GetControlsFromJP(int idxJP, string iD, List<Control> lstCtrl)
        {
            this.JPList[idxJP].JP.GetControlsFromID(this.JPList[idxJP].JP, iD, lstCtrl);
        }

        protected override void InitCtlColor()
        {
            if (base.Visible)
            {
                for (int i = 0; i < this.JPList.Count; i++)
                {
                    if (this.JPList[i].JPStatus == JobPanelStatus.Open)
                    {
                        this.InitPnlCtlColor(this.JPList[i].JP);
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(CombiJobForm));
            this.pnlTab = new Panel();
            this.Splitter = new System.Windows.Forms.Splitter();
            this.pnlRegular = new Panel();
            base.gbInputInfo.SuspendLayout();
            base.stPnlUser.BeginInit();
            base.stPnlSendGuard.BeginInit();
            base.gbGuide.SuspendLayout();
            base.pnlGuiLayout.SuspendLayout();
            base.gbAttach.SuspendLayout();
            base.SuspendLayout();
            base.pnlGuiLayout.Controls.Add(this.pnlTab);
            base.pnlGuiLayout.Controls.Add(this.Splitter);
            base.pnlGuiLayout.Controls.Add(this.pnlRegular);
            base.ResImageList.ImageStream = (ImageListStreamer) manager.GetObject("ResImageList.ImageStream");
            base.ResImageList.Images.SetKeyName(0, "Completion.ico");
            base.ResImageList.Images.SetKeyName(1, "Error.ico");
            base.ResImageList.Images.SetKeyName(2, "Warning.ico");
            this.pnlTab.Dock = DockStyle.Fill;
            this.pnlTab.Location = new Point(0, 0x14f);
            this.pnlTab.Name = "pnlTab";
            this.pnlTab.Size = new System.Drawing.Size(0x2c5, 0x142);
            this.pnlTab.TabIndex = 5;
            this.Splitter.BorderStyle = BorderStyle.Fixed3D;
            this.Splitter.Dock = DockStyle.Top;
            this.Splitter.Location = new Point(0, 0x145);
            this.Splitter.Name = "Splitter";
            this.Splitter.Size = new System.Drawing.Size(0x2c5, 10);
            this.Splitter.TabIndex = 4;
            this.Splitter.TabStop = false;
            this.pnlRegular.Dock = DockStyle.Top;
            this.pnlRegular.Location = new Point(0, 0);
            this.pnlRegular.Name = "pnlRegular";
            this.pnlRegular.Size = new System.Drawing.Size(0x2c5, 0x145);
            this.pnlRegular.TabIndex = 3;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new System.Drawing.Size(0x3f8, 0x2de);
            base.Name = "CombiJobForm";
            this.Text = "CombiJobForm";
            base.gbInputInfo.ResumeLayout(false);
            base.gbInputInfo.PerformLayout();
            base.stPnlUser.EndInit();
            base.stPnlSendGuard.EndInit();
            base.gbGuide.ResumeLayout(false);
            base.pnlGuiLayout.ResumeLayout(false);
            base.gbAttach.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InitJobPanel(Naccs.Core.Job.JobPanel JP)
        {
            JP.OnShowGuide += new EventHandler(this.ShowCmbGuide);
            JP.ContextMenuStrip = base.PopMenu;
        }

        private void InitPnlCtlColor(Naccs.Core.Job.JobPanel JP)
        {
            try
            {
                JP.Visible = false;
                JP.ChangeCtlColor(JP);
            }
            finally
            {
                JP.Visible = true;
            }
        }

        private void JP_Load(object sender, EventArgs e)
        {
            Naccs.Core.Job.JobPanel jP = (Naccs.Core.Job.JobPanel) sender;
            this.InitPnlCtlColor(jP);
        }

        protected override IData MakeExNaccsData(Naccs.Common.Generator.ItemInfo.JobDataStatus jds)
        {
            if (jds == Naccs.Common.Generator.ItemInfo.JobDataStatus.jdNone)
            {
                return this.MakePrtExNaccsData();
            }
            NaccsData data = new NaccsData {
                Style = JobStyle.combi
            };
            for (int i = 0; i < this.JPList.Count; i++)
            {
                data.JobIndex = i;
                data.SetSubJobData(this.JPList[i].JP.Items.GetDataList(jds), this.JPList[i].JCode);
            }
            CombiConfig jobConfig = (CombiConfig) base.JobConfig;
            if (base.JobConfig.jobStyle == JobStyle.combi)
            {
                data.Header.JobCode = jobConfig.jobcode;
                data.Header.DispCode = jobConfig.dispcode;
            }
            data.Header.System = jobConfig.system.ToString();
            data.Header.InputInfo = base.txtInputInfo.Text;
            data.Header.IndexInfo = base.ExNaccsData.Header.IndexInfo;
            if (jds == Naccs.Common.Generator.ItemInfo.JobDataStatus.jdSend)
            {
                for (int j = 0; j < this.JPList.Count; j++)
                {
                    for (int k = 0; k < this.JPList[j].AuCtrlInfoList.Count; k++)
                    {
                        List<string> idData = this.JPList[j].JP.Items.GetIdData(this.JPList[j].AuCtrlInfoList[k].ID);
                        for (int m = 0; m < idData.Count; m++)
                        {
                            string str = idData[m].Trim();
                            if (!string.IsNullOrEmpty(str) && !this.JPList[j].AuCtrlInfoList[k].AuList.Contains(str))
                            {
                                this.JPList[j].AuCtrlInfoList[k].AuList.Add(str);
                            }
                        }
                    }
                    this.SaveHistoryFile(this.JPList[j].HistoryFile, j);
                }
            }
            return data;
        }

        private IData MakePrtExNaccsData()
        {
            int activePanel = this.GetActivePanel();
            if (activePanel == -1)
            {
                activePanel = 0;
            }
            IData data = new NaccsData {
                JobData = this.JPList[activePanel].JP.GetData(Naccs.Common.Generator.ItemInfo.JobDataStatus.jdNone)
            };
            data.Header.JobCode = this.JPList[activePanel].JCode;
            data.Header.DispCode = this.JPList[activePanel].DCode;
            if ((!string.IsNullOrEmpty(base.ExNaccsData.Header.OutCode.Trim()) && !this.JPList[activePanel].Code.Contains(".")) && (this.JPList[activePanel].Code.Length == 7))
            {
                data.Header.OutCode = this.JPList[activePanel].Code;
            }
            data.Header.InputInfo = base.txtInputInfo.Text;
            data.Header.IndexInfo = base.ExNaccsData.Header.IndexInfo;
            return data;
        }

        protected override void NextTab()
        {
            TabControl tabControl = null;
            for (int i = 0; i < this.JPList.Count; i++)
            {
                if (this.JPList[i].JP.IsActive)
                {
                    tabControl = this.JPList[i].JP.GetTabControl();
                    if (tabControl != null)
                    {
                        break;
                    }
                }
            }
            if (tabControl == null)
            {
                if (!this.pnlTab.Visible)
                {
                    return;
                }
                tabControl = (TabControl) this.pnlTab.Controls["tbDefault"];
            }
            if (tabControl.SelectedIndex == (tabControl.TabCount - 1))
            {
                tabControl.SelectedIndex = 0;
            }
            else
            {
                tabControl.SelectedIndex++;
            }
            base.CursorTop(tabControl.TabPages[tabControl.SelectedIndex]);
        }

        private void OpenCombiTabPage(int idx)
        {
            if (idx != 0)
            {
                Control parent = this.JPList[idx].JP.Parent;
                TabPage page = null;
                while (!(parent is CommonJobForm))
                {
                    if (parent is TabControl)
                    {
                        TabControl control2 = (TabControl) parent;
                        control2.SelectedTab = page;
                        return;
                    }
                    if (parent is TabPage)
                    {
                        page = (TabPage) parent;
                    }
                    parent = parent.Parent;
                }
            }
        }

        protected void ReadCmbGuideFile(int idxPnl)
        {
            this.JPList[idxPnl].DocGuide = new XmlDocument();
            try
            {
                this.JPList[idxPnl].DocGuide.Load(this.JPList[idxPnl].GuideFile);
            }
            catch
            {
                Console.WriteLine("Failure in saving the input items guide file.(CombiJobForm.ReadCmbGuideFile)");
                Console.WriteLine("(" + this.JPList[idxPnl].GuideFile + ")");
                this.JPList[idxPnl].DocGuide = null;
            }
        }

        private void ReadHistoryFile(int idxPanel)
        {
            this.JPList[idxPanel].AuCtrlInfoList.Clear();
            if (File.Exists(this.JPList[idxPanel].HistoryFile))
            {
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(this.JPList[idxPanel].HistoryFile);
                    foreach (XmlElement element2 in document.DocumentElement.ChildNodes)
                    {
                        AuCtrlInfo item = new AuCtrlInfo {
                            AuList = new AutoCompleteStringCollection(),
                            ID = element2.GetAttribute("id")
                        };
                        foreach (XmlElement element3 in element2.ChildNodes)
                        {
                            item.AuList.Add(element3.InnerText);
                        }
                        this.JPList[idxPanel].AuCtrlInfoList.Add(item);
                    }
                }
                catch
                {
                    Console.WriteLine("Failure in reading in the input history (auto complete) file.(CombiJobForm.ReadHistoryFile)");
                    Console.WriteLine("(" + this.JPList[idxPanel].HistoryFile + ")");
                }
            }
        }

        private void SaveHistoryFile(string HistoryFile, int idxPnl)
        {
            if (this.JPList[idxPnl].AuCtrlInfoList.Count != 0)
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    XmlElement newChild = doc.CreateElement("History");
                    doc.AppendChild(newChild);
                    foreach (AuCtrlInfo info in this.JPList[idxPnl].AuCtrlInfoList)
                    {
                        XmlElement element2 = null;
                        bool flag = true;
                        foreach (XmlElement element3 in newChild.ChildNodes)
                        {
                            if (element3.GetAttribute("id") == info.ID)
                            {
                                element2 = element3;
                                flag = false;
                            }
                        }
                        if (element2 == null)
                        {
                            element2 = doc.CreateElement("Item");
                            element2.SetAttribute("id", info.ID);
                        }
                        int num = 0;
                        if (info.AuList.Count > 20)
                        {
                            num = info.AuList.Count - 20;
                        }
                        for (int i = num; i < info.AuList.Count; i++)
                        {
                            bool flag2 = true;
                            foreach (XmlElement element4 in element2)
                            {
                                if (element4.InnerText == info.AuList[i])
                                {
                                    flag2 = false;
                                    break;
                                }
                            }
                            if (flag2)
                            {
                                XmlElement element5 = doc.CreateElement("Data");
                                element5.InnerText = info.AuList[i];
                                element2.AppendChild(element5);
                            }
                        }
                        if (flag)
                        {
                            newChild.AppendChild(element2);
                        }
                    }
                    XmlTextWriter w = new XmlTextWriter(HistoryFile, Encoding.UTF8) {
                        Formatting = Formatting.Indented,
                        Indentation = 4
                    };
                    doc.Save(w);
                    this.SetHistory(doc, idxPnl);
                }
                catch
                {
                    Console.WriteLine("Failure in saving the input history (auto complete) file.(CombiJobForm.SaveHistoryFile)");
                    Console.WriteLine("(" + HistoryFile + ")");
                }
            }
        }

        private void SetAutoCompleteCtrl(int idxPanel)
        {
            bool flag = false;
            List<Control> auList = new List<Control>();
            this.JPList[idxPanel].JP.GetAutoCompList(this.JPList[idxPanel].JP, auList);
            foreach (Control control in auList)
            {
                flag = false;
                Naccs.Common.CustomControls.IAutoComplete complete = (Naccs.Common.CustomControls.IAutoComplete) control;
                foreach (AuCtrlInfo info in this.JPList[idxPanel].AuCtrlInfoList)
                {
                    if (info.ID == complete.id)
                    {
                        complete.AutoCompleteCustomSource = info.AuList;
                        flag = true;
                    }
                }
                if (!flag)
                {
                    AuCtrlInfo item = new AuCtrlInfo {
                        AuList = new AutoCompleteStringCollection(),
                        ID = complete.id
                    };
                    complete.AutoCompleteCustomSource = item.AuList;
                    this.JPList[idxPanel].AuCtrlInfoList.Add(item);
                }
            }
        }

        protected override void SetFieldError(string id, int errorPage, bool flgInputErr, int idx)
        {
            if (idx < this.JPList.Count)
            {
                Naccs.Core.Job.JobPanel jP = this.JPList[idx].JP;
                string idDataFromTbl = jP.GetIdDataFromTbl(id, errorPage);
                if (flgInputErr)
                {
                    this.JPList[idx].JobErrs.SetInputErrInfo(id, errorPage, idDataFromTbl);
                }
                else
                {
                    this.JPList[idx].JobErrs.SetErrInfo(id, errorPage, idDataFromTbl);
                }
                if (this.JPList[idx].JPStatus == JobPanelStatus.Open)
                {
                    List<Control> lstCtrl = new List<Control>();
                    jP.GetControlsFromID(jP, id, lstCtrl);
                    if (lstCtrl.Count > 0)
                    {
                        for (int i = 0; i < lstCtrl.Count; i++)
                        {
                            Control control = lstCtrl[i];
                            System.Type c = control.GetType();
                            if (typeof(IItemAttributesEx).IsAssignableFrom(c))
                            {
                                IItemAttributesEx ex = (IItemAttributesEx) control;
                                if (ex.CurrentPage == errorPage)
                                {
                                    ex.IBackColor = DesignControls.ErrorColor;
                                }
                            }
                        }
                    }
                }
            }
        }

        protected override void SetFocusField(string id, int errorPage, int idx)
        {
            if (idx < this.JPList.Count)
            {
                bool flag = true;
                if (this.JPList[idx].JPStatus == JobPanelStatus.Open)
                {
                    Control c = null;
                    IItemAttributesEx ex = null;
                    Naccs.Core.Job.JobPanel jP = this.JPList[idx].JP;
                    List<Control> lstCtrl = new List<Control>();
                    jP.GetControlsFromID(jP, id, lstCtrl);
                    if (lstCtrl.Count > 0)
                    {
                        if ((errorPage > 0) && ((jP.RepeatPos > errorPage) || (errorPage > (jP.RepeatPos + jP.BsCount))))
                        {
                            int num;
                            if (jP.RepeatPos < errorPage)
                            {
                                num = 1;
                            }
                            else
                            {
                                num = -1;
                            }
                            int num2 = 0;
                            int repeatPos = jP.RepeatPos;
                            while (((num == 1) && ((repeatPos > jP.RepeatCnt) || (jP.RepeatCnt > (repeatPos + jP.BsCount)))) || ((num == -1) && (repeatPos != 1)))
                            {
                                num2++;
                                repeatPos += (jP.BsCount + 1) * num;
                                if ((repeatPos <= errorPage) && (errorPage <= (repeatPos + jP.BsCount)))
                                {
                                    for (int j = 0; j < num2; j++)
                                    {
                                        if (num == 1)
                                        {
                                            jP.bindingNavigatorMoveNextItem_Click(null, null);
                                        }
                                        else
                                        {
                                            jP.bindingNavigatorMovePreviousItem_Click(null, null);
                                        }
                                    }
                                    this.SetFocusField(id, errorPage, idx);
                                    return;
                                }
                            }
                        }
                        for (int i = 0; i < lstCtrl.Count; i++)
                        {
                            c = lstCtrl[i];
                            if (c is LBDataGridView)
                            {
                                this.OpenCombiTabPage(idx);
                                jP.OpenTabPage(c);
                                jP.Focus();
                                LBDataGridView view = (LBDataGridView) c;
                                view.Focus();
                                view.SetFocusCell(id, errorPage - 1);
                                return;
                            }
                            ex = (IItemAttributesEx) c;
                            if (ex.CurrentPage == errorPage)
                            {
                                this.OpenCombiTabPage(idx);
                                jP.OpenTabPage(c);
                                jP.Focus();
                                c.Focus();
                                return;
                            }
                        }
                    }
                }
                else
                {
                    flag = false;
                }
                MessageDialog dialog = new MessageDialog();
                if (!flag)
                {
                    dialog.ShowMessage("E530", "", null);
                }
                else
                {
                    dialog.ShowMessage("E531", "", null);
                }
                dialog.Dispose();
            }
        }

        private void SetHistory(XmlDocument doc, int idxPnl)
        {
            this.JPList[idxPnl].AuCtrlInfoList.Clear();
            try
            {
                foreach (XmlElement element2 in doc.DocumentElement.ChildNodes)
                {
                    AuCtrlInfo item = new AuCtrlInfo {
                        AuList = new AutoCompleteStringCollection(),
                        ID = element2.GetAttribute("id")
                    };
                    foreach (XmlElement element3 in element2.ChildNodes)
                    {
                        item.AuList.Add(element3.InnerText);
                    }
                    this.JPList[idxPnl].AuCtrlInfoList.Add(item);
                }
                for (int i = 0; i < this.JPList.Count; i++)
                {
                    if (this.JPList[i].JPStatus == JobPanelStatus.Open)
                    {
                        this.SetAutoCompleteCtrl(i);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Failure in re-reading in the input history (auto complete) file.");
            }
        }

        public override void SetRecvData(IData data)
        {
            base.CmnClearJob();
            this.ClearNavigator();
            base.ExNaccsData = data;
            base.ExNaccsData.Style = JobStyle.combi;
            for (int i = 0; i < base.ExNaccsData.JobCount; i++)
            {
                if (i < this.JPList.Count)
                {
                    base.ExNaccsData.JobIndex = i;
                    this.JPList[i].JP.SetData(base.ExNaccsData.SubItems, Naccs.Common.Generator.ItemInfo.JobDataStatus.jdRecv, "");
                }
            }
            base.txtInputInfo.Text = base.ExNaccsData.Header.InputInfo.Trim();
        }

        public override void SetRes(IData data, bool flgSetColor)
        {
            bool flag = false;
            XmlElement rootCMSG = null;
            StrFunc func = StrFunc.CreateInstance();
            this.ClearResErr();
            string systemErrorMessage = PathInfo.CreateInstance().SystemErrorMessage;
            if (File.Exists(systemErrorMessage))
            {
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(systemErrorMessage);
                    rootCMSG = document.DocumentElement;
                }
                catch
                {
                    Console.WriteLine("Failure in reading in the common error message collection.(CommonJobForm.SetRes)");
                    Console.WriteLine("(" + systemErrorMessage + ")");
                }
            }
            int pnlidx = 0;
            for (int i = 0; i < data.Items.Count; i++)
            {
                if (i == 0)
                {
                    flag |= this.SetResCode(data.JobCode, data.Items[i], rootCMSG, pnlidx);
                }
                else if (i != 1)
                {
                    pnlidx++;
                    if (func.GetByteLength(data.Items[i]) == 0x66)
                    {
                        string local1 = data.Items[i];
                        string jobCode = func.SubByteString(data.Items[i], 0, 5).Trim();
                        flag |= this.SetResCode(jobCode, func.SubByteString(data.Items[i], 0x1b, 0x4b), rootCMSG, pnlidx);
                    }
                }
            }
            base.BoolSendGuard = data.Header.SendGuard == "*";
            if (data.Header.IndexInfo.Trim() != "")
            {
                base.ExNaccsData.Header.IndexInfo = data.Header.IndexInfo;
            }
            if (flag && base.flgWarningBeep)
            {
                Console.Beep(330, 100);
                Console.Beep(0x188, 100);
                Console.Beep(370, 100);
                Console.Beep(0x188, 100);
            }
            if (flgSetColor)
            {
                this.InitCtlColor();
                base.CursorTop(base.pnlGuiLayout);
                this.SetResField();
            }
        }

        private bool SetResCode(string JobCode, string stResLine, XmlElement rootCMSG, int Pnlidx)
        {
            bool flag = false;
            XmlElement documentElement = null;
            string msgFile = base.GetMsgFile(JobCode);
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
                    Console.WriteLine("Failure in reading in the service error message collection file.(CombiJobForm.SetResCode)");
                    Console.WriteLine("(" + msgFile + ")");
                }
            }
            for (int i = 0; i < stResLine.Length; i += 15)
            {
                string str2 = stResLine.Substring(i, 15);
                switch (str2)
                {
                    case "               ":
                        break;

                    case "00000-0000-0000":
                        base.gvResult.Rows.Add(new object[] { base.ResImageList.Images[0], "COMPLETION", "", "", "", JobCode, "", "" });
                        break;

                    default:
                    {
                        string str3 = str2.Substring(0, 5);
                        string str4 = str2.Substring(6, 4).Trim();
                        string str5 = str2.Substring(11, 4).Trim();
                        string innerText = "";
                        string str7 = "";
                        try
                        {
                            XmlNode node = null;
                            if (str3[0] == 'A')
                            {
                                if (rootCMSG != null)
                                {
                                    node = rootCMSG.SelectSingleNode("descendant::response[@code='" + str3 + "']");
                                }
                            }
                            else if (documentElement != null)
                            {
                                node = documentElement.SelectSingleNode("descendant::response[@code='" + str3 + "']");
                            }
                            if (node == null)
                            {
                                innerText = "";
                                str7 = "";
                            }
                            else
                            {
                                foreach (XmlElement element2 in node)
                                {
                                    if (element2.LocalName == "description")
                                    {
                                        innerText = element2.InnerText;
                                    }
                                    if (element2.LocalName == "disposition")
                                    {
                                        str7 = element2.InnerText;
                                    }
                                }
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Failure in reading in the service error message collection file node.(CombiJobForm.SetResCode)");
                            Console.WriteLine("(" + msgFile + ")");
                        }
                        if (str3[0] == 'A')
                        {
                            base.gvResult.Rows.Add(new object[] { base.ResImageList.Images[1], str3, innerText, str7, str4, JobCode, str5, Pnlidx.ToString() });
                        }
                        else if (str3[0] == 'W')
                        {
                            base.gvResult.Rows.Add(new object[] { base.ResImageList.Images[2], str3, innerText, str7, str4, JobCode, str5, Pnlidx.ToString() });
                            flag = true;
                        }
                        else
                        {
                            base.gvResult.Rows.Add(new object[] { base.ResImageList.Images[1], str3, innerText, str7, str4, JobCode, str5, Pnlidx.ToString() });
                        }
                        for (int j = 0; j < base.gvResult.ColumnCount; j++)
                        {
                            DataGridViewCell cell = base.gvResult.Rows[base.gvResult.Rows.Count - 1].Cells[j];
                            if (str3[0] == 'A')
                            {
                                cell.Style.BackColor = DesignControls.SysErrorColor;
                            }
                            else if (str3[0] == 'W')
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
            return flag;
        }

        private void SetResField()
        {
            string id = "";
            int errorPage = -1;
            string s = "";
            for (int i = 0; i < base.gvResult.Rows.Count; i++)
            {
                string str3 = (string) base.gvResult[4, i].Value;
                string str4 = (string) base.gvResult[6, i].Value;
                string str5 = (string) base.gvResult[7, i].Value;
                if ((!(str3 == "0000") && !string.IsNullOrEmpty(str3)) && !string.IsNullOrEmpty(str4))
                {
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

        public override void SetSendData(IData data)
        {
            base.CmnClearJob();
            this.ClearNavigator();
            for (int i = 0; i < this.JPList.Count; i++)
            {
                this.JPList[i].JP.Items.ClearAll();
            }
            base.ExNaccsData = data;
            base.ExNaccsData.Style = JobStyle.combi;
            for (int j = 0; j < base.ExNaccsData.JobCount; j++)
            {
                if (j < this.JPList.Count)
                {
                    base.ExNaccsData.JobIndex = j;
                    this.JPList[j].JP.SetData(base.ExNaccsData.SubItems, Naccs.Common.Generator.ItemInfo.JobDataStatus.jdSend, "");
                }
            }
            base.txtInputInfo.Text = base.ExNaccsData.Header.InputInfo.Trim();
        }

        private void ShowCmbGuide(object sender, EventArgs e)
        {
            int activePanel = this.GetActivePanel();
            if (activePanel < 0)
            {
                base.rtxtGuide.Clear();
            }
            else
            {
                Control control = sender as Control;
                if (control is IItemAttributes)
                {
                    IItemAttributes attributes = (IItemAttributes) control;
                    base.rtxtGuide.Text = this.getCmbGuide(attributes.id, activePanel);
                }
                else if (control is LBDataGridView)
                {
                    LBDataGridView view = (LBDataGridView) control;
                    string aColumnID = view.AColumnID;
                    base.rtxtGuide.Text = this.getCmbGuide(aColumnID, activePanel);
                }
                else
                {
                    base.rtxtGuide.Clear();
                }
            }
        }

        private void TC_Selected(object sender, TabControlEventArgs e)
        {
            if (this.JPList[e.TabPageIndex + 1].JPStatus == JobPanelStatus.Closed)
            {
                int errint = this.CreateJobPanel(e.TabPageIndex + 1);
                if (errint != 0)
                {
                    string msgCode = JobFormFactory.GetMsgCode(errint);
                    string text = e.TabPage.Text;
                    MessageDialog dialog = new MessageDialog();
                    dialog.ShowMessage(msgCode, text);
                    dialog.Dispose();
                }
            }
        }

        protected override int CmnPnlHeight
        {
            get
            {
                if (this.defaultCmnHeight == 0)
                {
                    return this.pnlRegular.Height;
                }
                return 0;
            }
            set
            {
                if (this.defaultCmnHeight == 0)
                {
                    this.pnlRegular.Height = value;
                }
            }
        }
    }
}

