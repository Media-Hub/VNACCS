namespace Naccs.Core.Job
{
    using Naccs.Common.CustomControls;
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

    public class DivideJobForm : CommonJobForm
    {
        private List<AuCtrlInfo> AuCtrlInfoList;
        private IContainer components;
        private int defaultCmnHeight;
        private Naccs.Common.Generator.ItemInfo Items;
        private JobErrInfo JobErrs;
        private List<JPRec> JPList;
        private Panel pnlRegular;
        private Panel pnlTab;
        private System.Windows.Forms.Splitter Splitter;
        private string stHistoryFile;
        private TabControl TC;

        public DivideJobForm()
        {
            this.InitializeComponent();
            this.AuCtrlInfoList = new List<AuCtrlInfo>();
            base._style = "divide";
            this.Items = new Naccs.Common.Generator.ItemInfo();
            this.JobErrs = new JobErrInfo();
        }

        protected override void AcceptDataChange()
        {
            this.Items.DsItems.AcceptChanges();
        }

        protected override void AddCsvData(List<string> stList, string tblName)
        {
            base.CmnClearJob();
            this.ClearNavigator();
            this.Items.SetDataList(stList, Naccs.Common.Generator.ItemInfo.JobDataStatus.jdSend, tblName);
            this.InitCtlColor();
        }

        protected override void AddSendData(IData data, bool flgClear)
        {
            base.CmnClearJob();
            if (flgClear)
            {
                this.Items.ClearAll();
            }
            this.ClearNavigator();
            this.Items.SetDataList(data.Items, Naccs.Common.Generator.ItemInfo.JobDataStatus.jdSend, "");
            base.txtInputInfo.Text = data.Header.InputInfo.Trim();
            this.InitCtlColor();
        }

        protected override bool CheckError(bool flgCheckReq)
        {
            int errorPage = 0;
            string[] idArray = null;
            bool flag = true;
            string msg = null;
            this.JobErrs.ClearInputErrorInfo();
            this.InitCtlColor();
            if (!base.flgDoCheckErr)
            {
                return flag;
            }
            flag = base.CheckError(flgCheckReq);
            if (!flag)
            {
                return flag;
            }
            if (this.JPList[0].JP.SearchErrorField(out idArray, out errorPage, out msg, flgCheckReq) == 0)
            {
                return flag;
            }
            for (int i = 0; i < idArray.Length; i++)
            {
                if (((i == 0) && !string.IsNullOrEmpty(idArray[i])) && (errorPage >= 0))
                {
                    this.SetFocusField(idArray[i], errorPage, 0);
                }
                this.SetFieldError(idArray[i], errorPage, true, 0);
            }
            MessageDialog dialog = new MessageDialog();
            dialog.ShowMessage("E516", msg, "");
            dialog.Dispose();
            return false;
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
            this.JobErrs.Clear();
            base.ClearResErr();
        }

        private int CreateDivJobPanel(int idxPanel)
        {
            int num = this.JPList[idxPanel].JP.CreatePanel(this.JPList[idxPanel].FileName);
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
            this.JPList[idxPanel].JP.Load += new EventHandler(this.JP_Load);
            base.SetLinkField(idxPanel);
            this.SetAutoCompleteCtrl(idxPanel);
            if (base.FMode != ModeFont.normal)
            {
                this.JPList[idxPanel].JP.ChangeFontSize(base.FMode);
            }
            return 0;
        }

        private int CreateLayout(DivideConfig Dconfig)
        {
            int num = this.Items.CreateDataSet(Dconfig.displayDivision.iteminfo);
            if (num != 0)
            {
                return num;
            }
            this.JPList = new List<JPRec>();
            this.JPList.Add(new JPRec());
            this.JPList[0].JP = new DivJobPanel();
            this.JPList[0].FileName = Dconfig.displayDivision.templates[0].filename;
            this.JPList[0].JPStatus = JobPanelStatus.Closed;
            this.InitDivJobPanel(this.JPList[0].JP);
            this.pnlRegular.Controls.Add(this.JPList[0].JP);
            this.JPList[0].JP.Dock = DockStyle.Fill;
            this.TC = new TabControl();
            this.TC.TabStop = false;
            this.TC.Name = "tbDefault";
            this.TC.Selected += new TabControlEventHandler(this.TC_Selected);
            this.pnlTab.Controls.Add(this.TC);
            this.TC.Dock = DockStyle.Fill;
            this.TC.Font = new Font("Tahoma", this.TC.Font.Size);
            for (int i = 1; i < Dconfig.displayDivision.count; i++)
            {
                this.JPList.Add(new JPRec());
                this.JPList[i].JP = new DivJobPanel();
                this.JPList[i].FileName = Dconfig.displayDivision.templates[i].filename;
                this.JPList[i].JPStatus = JobPanelStatus.Closed;
                this.InitDivJobPanel(this.JPList[i].JP);
                TabPage page = new TabPage {
                    Name = "Tab" + i.ToString(),
                    Text = Dconfig.displayDivision.templates[i].tabname
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
                    DivideConfig jobConfig;
                    if (base.JobConfig.jobStyle == JobStyle.divide)
                    {
                        jobConfig = (DivideConfig) base.JobConfig;
                    }
                    else
                    {
                        return (num = -99);
                    }
                    PathInfo info = PathInfo.CreateInstance();
                    Directory.CreateDirectory(info.InputHistoryRoot);
                    this.stHistoryFile = info.getInputHistory(jobConfig.record.jobcode);
                    this.ReadHistoryFile(this.stHistoryFile);
                    base.stLinkFileName = jobConfig.displayDivision.link;
                    base.ReadLinkFile(base.stLinkFileName);
                    num = this.CreateLayout(jobConfig);
                    if (num != 0)
                    {
                        return num;
                    }
                    num += this.CreateDivJobPanel(0);
                    num += this.CreateDivJobPanel(1);
                    if (num != 0)
                    {
                        return num;
                    }
                    base.BoolAttach = jobConfig.record.attach;
                    if (base.BoolAttach)
                    {
                        base.stAttachPath = Path.GetTempPath() + jobCode.Trim() + kind.Trim() + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    }
                    else
                    {
                        base.stAttachPath = "";
                    }
                    base.stCorrelationFileName = jobConfig.displayDivision.correlation;
                    this.Items.ReadCorrelation(base.stCorrelationFileName);
                    string title = "";
                    if (flgJobTitle)
                    {
                        title = this.Items.DisplayName;
                    }
                    else
                    {
                        title = jobConfig.record.titile1;
                    }
                    base.SetTitle(title, jobConfig.record.jobcode, jobConfig.record.dispcode);
                    base.ExNaccsData.Header.JobCode = jobConfig.record.jobcode;
                    base.ExNaccsData.Header.DispCode = jobConfig.record.dispcode;
                    if (jobConfig.displayDivision.guide != "")
                    {
                        base.ReadGuideFile(jobConfig.displayDivision.guide);
                    }
                    if (jobConfig.record.attach)
                    {
                        base.ShowAttachInfo(base.stAttachPath);
                    }
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
                num = JobConfigFactory.createJobConfig(Data.OutCode, out this.JobConfig);
                if (num >= 0)
                {
                    DivideConfig jobConfig;
                    num = base.IsValidJob(base.JobConfig);
                    if (num < 0)
                    {
                        return num;
                    }
                    if (base.JobConfig.jobStyle == JobStyle.divide)
                    {
                        jobConfig = (DivideConfig) base.JobConfig;
                    }
                    else
                    {
                        return (num = -99);
                    }
                    PathInfo info = PathInfo.CreateInstance();
                    Directory.CreateDirectory(info.InputHistoryRoot);
                    this.stHistoryFile = info.getInputHistory(jobConfig.record.jobcode);
                    this.ReadHistoryFile(this.stHistoryFile);
                    base.stLinkFileName = jobConfig.displayDivision.link;
                    base.ReadLinkFile(base.stLinkFileName);
                    num = this.CreateLayout(jobConfig);
                    if (num != 0)
                    {
                        return num;
                    }
                    num += this.CreateDivJobPanel(0);
                    num += this.CreateDivJobPanel(1);
                    if (num != 0)
                    {
                        return num;
                    }
                    string jCode = jobConfig.record.jobcode.TrimEnd(new char[0]);
                    if (base.IsAttach(jCode, jobConfig.record.dispcode.TrimEnd(new char[0])))
                    {
                        base.BoolAttach = jobConfig.record.attach;
                    }
                    else
                    {
                        base.RecvAttach = jobConfig.record.attach;
                    }
                    base.stAttachPath = "";
                    base.stCorrelationFileName = jobConfig.displayDivision.correlation;
                    this.Items.ReadCorrelation(base.stCorrelationFileName);
                    string title = "";
                    if (flgJobTitle)
                    {
                        title = this.Items.DisplayName;
                    }
                    else
                    {
                        title = jobConfig.record.titile1;
                    }
                    base.SetTitle(title, jobConfig.record.jobcode, jobConfig.record.dispcode);
                    base.ExNaccsData.Header.OutCode = jobConfig.record.jobcode;
                    base.ExNaccsData.Header.DispCode = jobConfig.record.dispcode;
                    if (jobConfig.displayDivision.guide != "")
                    {
                        base.ReadGuideFile(jobConfig.displayDivision.guide);
                    }
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

        protected override string GetIdData(string RepID, string stID, int No)
        {
            if (this.Items.DsItems.Tables[RepID].Rows[No][stID] is DBNull)
            {
                return "";
            }
            return (string) this.Items.DsItems.Tables[RepID].Rows[No][stID];
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

        private void InitDivJobPanel(DivJobPanel DivJP)
        {
            DivJP.Items = this.Items;
            DivJP.OnShowGuide += new EventHandler(this.ShowGuide);
            DivJP.ContextMenuStrip = base.PopMenu;
            DivJP.JobErrs = this.JobErrs;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(DivideJobForm));
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
            base.SaveFileDlg.InitialDirectory = @"C:\Users\sawadary\Documents";
            base.ResImageList.ImageStream = (ImageListStreamer) manager.GetObject("ResImageList.ImageStream");
            base.ResImageList.Images.SetKeyName(0, "Completion.ico");
            base.ResImageList.Images.SetKeyName(1, "Error.ico");
            base.ResImageList.Images.SetKeyName(2, "Warning.ico");
            this.pnlTab.Dock = DockStyle.Fill;
            this.pnlTab.Location = new Point(0, 0x14f);
            this.pnlTab.Name = "pnlTab";
            this.pnlTab.Size = new Size(0x2c5, 0x142);
            this.pnlTab.TabIndex = 2;
            this.Splitter.BorderStyle = BorderStyle.Fixed3D;
            this.Splitter.Dock = DockStyle.Top;
            this.Splitter.Location = new Point(0, 0x145);
            this.Splitter.Name = "Splitter";
            this.Splitter.Size = new Size(0x2c5, 10);
            this.Splitter.TabIndex = 1;
            this.Splitter.TabStop = false;
            this.pnlRegular.Dock = DockStyle.Top;
            this.pnlRegular.Location = new Point(0, 0);
            this.pnlRegular.Name = "pnlRegular";
            this.pnlRegular.Size = new Size(0x2c5, 0x145);
            this.pnlRegular.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x3f8, 0x2de);
            base.Name = "DivideJobForm";
            this.Text = "DivideJobForm";
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

        private void InitPnlCtlColor(DivJobPanel DivJP)
        {
            try
            {
                DivJP.Visible = false;
                DivJP.ChangeCtlColor(DivJP);
            }
            finally
            {
                DivJP.Visible = true;
            }
        }

        private void JP_Load(object sender, EventArgs e)
        {
            DivJobPanel divJP = (DivJobPanel) sender;
            this.InitPnlCtlColor(divJP);
        }

        protected override IData MakeExNaccsData(Naccs.Common.Generator.ItemInfo.JobDataStatus jds)
        {
            IData data = new NaccsData {
                JobData = this.Items.GetDataList(jds)
            };
            DivideConfig jobConfig = (DivideConfig) base.JobConfig;
            if (base.JobConfig.jobStyle == JobStyle.divide)
            {
                data.Header.JobCode = jobConfig.record.jobcode;
                data.Header.DispCode = jobConfig.record.dispcode;
            }
            if (jds == Naccs.Common.Generator.ItemInfo.JobDataStatus.jdNone)
            {
                data.Header.OutCode = base.ExNaccsData.Header.OutCode;
            }
            data.Header.System = jobConfig.system.ToString();
            data.Header.InputInfo = base.txtInputInfo.Text;
            data.Header.IndexInfo = base.ExNaccsData.Header.IndexInfo;
            if (jds == Naccs.Common.Generator.ItemInfo.JobDataStatus.jdSend)
            {
                for (int i = 0; i < this.AuCtrlInfoList.Count; i++)
                {
                    List<string> idData = this.Items.GetIdData(this.AuCtrlInfoList[i].ID);
                    for (int j = 0; j < idData.Count; j++)
                    {
                        string str = idData[j].Trim();
                        if (!string.IsNullOrEmpty(str) && !this.AuCtrlInfoList[i].AuList.Contains(str))
                        {
                            this.AuCtrlInfoList[i].AuList.Add(str);
                        }
                    }
                }
            }
            this.SaveHistoryFile(this.stHistoryFile);
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

        private void OpenDivTabPage(int idx)
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

        private void ReadHistoryFile(string HistoryFile)
        {
            this.AuCtrlInfoList.Clear();
            if (File.Exists(HistoryFile))
            {
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(HistoryFile);
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
                        this.AuCtrlInfoList.Add(item);
                    }
                }
                catch
                {
                    Console.WriteLine("Failure in reading in the input history (auto complete) file.(DivJobForm.ReadHistoryFile)");
                    Console.WriteLine("(" + HistoryFile + ")");
                }
            }
        }

        private void ReadValue(Control root)
        {
            foreach (Control control in root.Controls)
            {
                if (control is IItemAttributesEx)
                {
                    if (control.DataBindings.Count > 0)
                    {
                        control.DataBindings[0].ReadValue();
                    }
                }
                else if (control.HasChildren)
                {
                    this.ReadValue(control);
                }
            }
        }

        private void SaveHistoryFile(string HistoryFile)
        {
            if (this.AuCtrlInfoList.Count != 0)
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    XmlElement newChild = doc.CreateElement("History");
                    doc.AppendChild(newChild);
                    foreach (AuCtrlInfo info in this.AuCtrlInfoList)
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
                    this.SetHistory(doc);
                }
                catch
                {
                    Console.WriteLine("Failure in saving the input history (auto complete) file.DivJobForm.SaveHistoryFile)");
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
                foreach (AuCtrlInfo info in this.AuCtrlInfoList)
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
                    this.AuCtrlInfoList.Add(item);
                }
            }
        }

        protected override void SetFieldError(string id, int errorPage, bool flgInputErr, int idx)
        {
            string idData = this.Items.GetIdData(id, errorPage);
            if (((idData == null) && !string.IsNullOrEmpty(id)) && (0 <= errorPage))
            {
                if (char.IsNumber(id[id.Length - 1]))
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append(id.ToCharArray());
                    StringBuilder builder2 = new StringBuilder();
                    for (int j = 0; j < builder.Length; j++)
                    {
                        if (char.IsNumber(builder[j]))
                        {
                            builder2.Append(builder[j]);
                            builder[j] = '_';
                        }
                    }
                    id = builder.ToString();
                    errorPage = int.Parse(builder2.ToString());
                    idData = this.Items.GetIdData(id, errorPage);
                }
                if (idData == null)
                {
                    idData = "";
                }
            }
            if (flgInputErr)
            {
                this.JobErrs.SetInputErrInfo(id, errorPage, idData);
            }
            else
            {
                this.JobErrs.SetErrInfo(id, errorPage, idData);
            }
            for (int i = 0; i < this.JPList.Count; i++)
            {
                if (this.JPList[i].JPStatus == JobPanelStatus.Open)
                {
                    List<Control> lstCtrl = new List<Control>();
                    DivJobPanel jP = this.JPList[i].JP;
                    jP.GetControlsFromID(jP, id, lstCtrl);
                    if (lstCtrl.Count > 0)
                    {
                        for (int k = 0; k < lstCtrl.Count; k++)
                        {
                            Control control = lstCtrl[k];
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
            bool flag = true;
            for (int i = 0; i < this.JPList.Count; i++)
            {
                if (this.JPList[i].JPStatus == JobPanelStatus.Open)
                {
                    Control c = null;
                    IItemAttributesEx ex = null;
                    DivJobPanel jP = this.JPList[i].JP;
                    List<Control> lstCtrl = new List<Control>();
                    jP.GetControlsFromID(jP, id, lstCtrl);
                    if (lstCtrl.Count > 0)
                    {
                        if ((errorPage > 0) && ((jP.RepeatPos > errorPage) || (errorPage > (jP.RepeatPos + jP.BsCount))))
                        {
                            int num2;
                            if (jP.RepeatPos < errorPage)
                            {
                                num2 = 1;
                            }
                            else
                            {
                                num2 = -1;
                            }
                            int num3 = 0;
                            int repeatPos = jP.RepeatPos;
                            while (((num2 == 1) && ((repeatPos > jP.RepeatCnt) || (jP.RepeatCnt > (repeatPos + jP.BsCount)))) || ((num2 == -1) && (repeatPos != 1)))
                            {
                                num3++;
                                repeatPos += (jP.BsCount + 1) * num2;
                                if ((repeatPos <= errorPage) && (errorPage <= (repeatPos + jP.BsCount)))
                                {
                                    for (int k = 0; k < num3; k++)
                                    {
                                        if (num2 == 1)
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
                        for (int j = 0; j < lstCtrl.Count; j++)
                        {
                            c = lstCtrl[j];
                            if (c is LBDataGridView)
                            {
                                this.OpenDivTabPage(i);
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
                                this.OpenDivTabPage(i);
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
            }
            if (((errorPage != 0) || (id[id.Length - 1] < '0')) || (id[id.Length - 1] > '9'))
            {
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
            else
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(id.ToCharArray());
                string s = "0";
                int num7 = 0;
                num7 = builder.Length - 1;
                while (num7 > -1)
                {
                    if ((builder[num7] < '0') || (builder[num7] > '9'))
                    {
                        break;
                    }
                    builder[num7] = '_';
                    num7--;
                }
                s = id.Substring(num7 + 1);
                this.SetFocusField(builder.ToString(), int.Parse(s), idx);
            }
        }

        private void SetHistory(XmlDocument doc)
        {
            this.AuCtrlInfoList.Clear();
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
                    this.AuCtrlInfoList.Add(item);
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

        protected override void SetIdData(string RepID, string stID, string stData, int No)
        {
            this.Items.DsItems.Tables[RepID].Rows[No][stID] = this.Items.GetRevisionSetData(RepID, stID, stData);
        }

        public override void SetRecvData(IData data)
        {
            base.CmnClearJob();
            this.ClearNavigator();
            base.ExNaccsData = data;
            this.Items.SetDataList(base.ExNaccsData.Items, Naccs.Common.Generator.ItemInfo.JobDataStatus.jdRecv, "");
            this.JPList[0].JP.SetlblNextpage();
            if (this.JPList.Count > 1)
            {
                this.JPList[1].JP.SetlblNextpage();
            }
            base.txtInputInfo.Text = base.ExNaccsData.Header.InputInfo.Trim();
            base.ShowAttachInfo(base.ExNaccsData);
        }

        public override void SetSendData(IData data)
        {
            base.CmnClearJob();
            this.ClearNavigator();
            this.Items.ClearAll();
            base.ExNaccsData = data;
            this.Items.SetDataList(base.ExNaccsData.Items, Naccs.Common.Generator.ItemInfo.JobDataStatus.jdSend, "");
            base.txtInputInfo.Text = base.ExNaccsData.Header.InputInfo.Trim();
            try
            {
                if (!string.IsNullOrEmpty(base.ExNaccsData.AttachFolder))
                {
                    PathInfo.CreateInstance();
                    Directory.Delete(base.stAttachPath, true);
                    CommonJobForm.CopyDirectory(base.ExNaccsData.AttachFolder, base.stAttachPath);
                }
                base.ShowAttachInfo(base.stAttachPath);
            }
            catch
            {
                Console.WriteLine("Failure in deleting/copying the attachment folder. (JobForm.SetSendData)");
                Console.WriteLine(base.stAttachPath);
                Console.WriteLine(base.ExNaccsData.AttachFolder);
            }
        }

        private void TC_Selected(object sender, TabControlEventArgs e)
        {
            if (this.JPList[e.TabPageIndex + 1].JPStatus == JobPanelStatus.Closed)
            {
                int errint = this.CreateDivJobPanel(e.TabPageIndex + 1);
                if (errint != 0)
                {
                    string msgCode = JobFormFactory.GetMsgCode(errint);
                    string text = e.TabPage.Text;
                    MessageDialog dialog = new MessageDialog();
                    dialog.ShowMessage(msgCode, text);
                    dialog.Dispose();
                }
            }
            else
            {
                this.ReadValue(this.JPList[e.TabPageIndex + 1].JP);
            }
            this.ReadValue(this.JPList[0].JP);
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

