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

    public class JobForm : CommonJobForm
    {
        private List<Control> AuCtrlList;
        private IContainer components;
        private bool flgBar;
        private Naccs.Core.Job.JobPanel JP;
        private string stHistoryFile;

        public JobForm()
        {
            this.InitializeComponent();
            this.JP.OnShowGuide += new EventHandler(this.ShowGuide);
            this.JP.ContextMenuStrip = base.PopMenu;
            this.AuCtrlList = new List<Control>();
            base._style = "normal";
            this.JP.JobErrs = new JobErrInfo();
        }

        protected override void AcceptDataChange()
        {
            this.JP.Items.DsItems.AcceptChanges();
        }

        protected override void AddCsvData(List<string> stList, string tblName)
        {
            base.CmnClearJob();
            this.JP.LblNextPage.Text = "";
            this.JP.SetData(stList, Naccs.Common.Generator.ItemInfo.JobDataStatus.jdSend, tblName);
            this.InitCtlColor();
        }

        protected override void AddSendData(IData data, bool flgClear)
        {
            base.CmnClearJob();
            if (flgClear)
            {
                this.JP.ItemClearAll();
            }
            this.JP.LblNextPage.Text = "";
            this.JP.SetData(data.Items, Naccs.Common.Generator.ItemInfo.JobDataStatus.jdSend, "");
            base.txtInputInfo.Text = data.Header.InputInfo.Trim();
            this.InitCtlColor();
        }

        protected override bool CheckError(bool flgCheckReq)
        {
            int errorPage = 0;
            string[] idArray = null;
            bool flag = true;
            string msg = null;
            this.JP.JobErrs.ClearInputErrorInfo();
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
            if (this.JP.SearchErrorField(out idArray, out errorPage, out msg, flgCheckReq) == 0)
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

        protected override void ClearResErr()
        {
            this.JP.JobErrs.Clear();
            base.ClearResErr();
        }

        public override int CreateNewForm(string jobCode, string kind, bool flgNew, bool flgJobTitle)
        {
            try
            {
                int num = base.CreateNewForm(jobCode, kind, flgNew, flgJobTitle);
                if (num == 0)
                {
                    NormalConfig jobConfig;
                    if (base.JobConfig.jobStyle == JobStyle.normal)
                    {
                        jobConfig = (NormalConfig) base.JobConfig;
                    }
                    else
                    {
                        return (num = -99);
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
                    base.stCorrelationFileName = jobConfig.Correlation;
                    this.JP.SetCorrelation(base.stCorrelationFileName);
                    if (jobConfig.record.barcode)
                    {
                        this.JP.OnSendBarcode += new EventHandler(this.JP_OnSendBarcode);
                    }
                    this.flgBar = jobConfig.record.barcode;
                    num = this.JP.CreatePanel(jobConfig.Display);
                    if (num != 0)
                    {
                        return num;
                    }
                    string title = "";
                    if (flgJobTitle)
                    {
                        title = this.JP.Items.DisplayName;
                    }
                    else
                    {
                        title = jobConfig.record.titile1;
                    }
                    base.SetTitle(title, jobConfig.record.jobcode, jobConfig.record.dispcode);
                    base.ExNaccsData.Header.JobCode = jobConfig.record.jobcode;
                    base.ExNaccsData.Header.DispCode = jobConfig.record.dispcode;
                    if (jobConfig.Guide != "")
                    {
                        base.ReadGuideFile(jobConfig.Guide);
                    }
                    PathInfo info = PathInfo.CreateInstance();
                    Directory.CreateDirectory(info.InputHistoryRoot);
                    this.stHistoryFile = info.getInputHistory(jobConfig.record.jobcode);
                    this.ReadHistoryFile(this.stHistoryFile);
                    base.stLinkFileName = jobConfig.Link;
                    base.ReadLinkFile(base.stLinkFileName);
                    base.SetLinkField(0);
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
                    NormalConfig jobConfig;
                    num = base.IsValidJob(base.JobConfig);
                    if (num < 0)
                    {
                        return num;
                    }
                    if (base.JobConfig.jobStyle == JobStyle.normal)
                    {
                        jobConfig = (NormalConfig) base.JobConfig;
                    }
                    else
                    {
                        return (num = -99);
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
                    base.stCorrelationFileName = jobConfig.Correlation;
                    this.JP.SetCorrelation(base.stCorrelationFileName);
                    if (jobConfig.record.barcode)
                    {
                        this.JP.OnSendBarcode += new EventHandler(this.JP_OnSendBarcode);
                    }
                    this.flgBar = jobConfig.record.barcode;
                    num = this.JP.CreatePanel(jobConfig.Display);
                    if (num != 0)
                    {
                        return num;
                    }
                    string title = "";
                    if (flgJobTitle)
                    {
                        title = this.JP.Items.DisplayName;
                    }
                    else
                    {
                        title = jobConfig.record.titile1;
                    }
                    base.SetTitle(title, jobConfig.record.jobcode, jobConfig.record.dispcode);
                    base.ExNaccsData.Header.OutCode = jobConfig.record.jobcode;
                    base.ExNaccsData.Header.DispCode = jobConfig.record.dispcode;
                    if (jobConfig.Guide != "")
                    {
                        base.ReadGuideFile(jobConfig.Guide);
                    }
                    PathInfo info = PathInfo.CreateInstance();
                    Directory.CreateDirectory(info.InputHistoryRoot);
                    this.stHistoryFile = info.getInputHistory(jobConfig.record.jobcode);
                    this.ReadHistoryFile(this.stHistoryFile);
                    base.stLinkFileName = jobConfig.Link;
                    base.ReadLinkFile(base.stLinkFileName);
                    base.SetLinkField(0);
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
            this.JP.ChangeFontSize(mF);
            base.FMode = mF;
        }

        protected override CommonJobPanel GetCommonJobPanel()
        {
            return this.JP;
        }

        protected override void GetControlsFromJP(int idxJP, string iD, List<Control> lstCtrl)
        {
            this.JP.GetControlsFromID(this.JP, iD, lstCtrl);
        }

        protected override string GetIdData(string RepID, string stID, int No)
        {
            if (this.JP.Items.DsItems.Tables[RepID].Rows[No][stID] is DBNull)
            {
                return "";
            }
            return (string) this.JP.Items.DsItems.Tables[RepID].Rows[No][stID];
        }

        private TabControl GetTab(Control root)
        {
            foreach (Control control in root.Controls)
            {
                if (control is TabControl)
                {
                    return (TabControl) control;
                }
                Control tab = this.GetTab(control);
                if (tab != null)
                {
                    return (TabControl) tab;
                }
            }
            return null;
        }

        protected override void InitCtlColor()
        {
            if (base.Visible)
            {
                try
                {
                    this.JP.Visible = false;
                    this.JP.ChangeCtlColor(this.JP);
                }
                finally
                {
                    this.JP.Visible = true;
                }
            }
        }

        private void InitializeComponent()
        {
            Naccs.Common.Generator.ItemInfo info = new Naccs.Common.Generator.ItemInfo();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(JobForm));
            this.JP = new Naccs.Core.Job.JobPanel();
            base.gbInputInfo.SuspendLayout();
            base.stPnlUser.BeginInit();
            base.stPnlSendGuard.BeginInit();
            base.gbGuide.SuspendLayout();
            base.pnlGuiLayout.SuspendLayout();
            base.gbAttach.SuspendLayout();
            base.SuspendLayout();
            base.pnlGuiLayout.Controls.Add(this.JP);
            base.ResImageList.ImageStream = (ImageListStreamer) manager.GetObject("ResImageList.ImageStream");
            base.ResImageList.Images.SetKeyName(0, "Completion.ico");
            base.ResImageList.Images.SetKeyName(1, "Error.ico");
            base.ResImageList.Images.SetKeyName(2, "Warning.ico");
            this.JP.AutoScroll = true;
            this.JP.Dock = DockStyle.Fill;
            this.JP.Font = new Font("Courier New", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.JP.Items = info;
            this.JP.JobErrs = null;
            this.JP.Location = new Point(0, 0);
            this.JP.Name = "JP";
            this.JP.Size = new Size(0x2c5, 0x291);
            this.JP.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x3f8, 0x2de);
            base.Name = "JobForm";
            this.Text = "JobForm";
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

        private void JP_OnSendBarcode(object sender, EventArgs e)
        {
            base.ExecSend();
        }

        protected override IData MakeExNaccsData(Naccs.Common.Generator.ItemInfo.JobDataStatus jds)
        {
            IData data = new NaccsData {
                JobData = this.JP.GetData(jds)
            };
            NormalConfig jobConfig = (NormalConfig) base.JobConfig;
            if (base.JobConfig.jobStyle == JobStyle.normal)
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
                for (int i = 0; i < this.AuCtrlList.Count; i++)
                {
                    Naccs.Common.CustomControls.IAutoComplete complete = (Naccs.Common.CustomControls.IAutoComplete) this.AuCtrlList[i];
                    string str = complete.Text.Trim();
                    if (!string.IsNullOrEmpty(str) && !complete.AutoCompleteCustomSource.Contains(str))
                    {
                        complete.AutoCompleteCustomSource.Add(str);
                    }
                }
            }
            this.SaveHistoryFile(this.stHistoryFile);
            return data;
        }

        protected override void NextTab()
        {
            TabControl tab;
            if (this.JP.ActiveControl == null)
            {
                tab = this.GetTab(this);
            }
            else
            {
                tab = this.JP.GetTabControl();
            }
            if (tab != null)
            {
                if (tab.SelectedIndex == (tab.TabCount - 1))
                {
                    tab.SelectedIndex = 0;
                }
                else
                {
                    tab.SelectedIndex++;
                }
                base.CursorTop(tab.TabPages[tab.SelectedIndex]);
            }
        }

        private void ReadHistoryFile(string HistoryFile)
        {
            this.AuCtrlList.Clear();
            this.JP.GetAutoCompList(this.JP, this.AuCtrlList);
            if (File.Exists(HistoryFile))
            {
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(HistoryFile);
                    foreach (XmlElement element2 in document.DocumentElement.ChildNodes)
                    {
                        string attribute = element2.GetAttribute("id");
                        foreach (Control control in this.AuCtrlList)
                        {
                            Naccs.Common.CustomControls.IAutoComplete complete = (Naccs.Common.CustomControls.IAutoComplete) control;
                            if (attribute == complete.id)
                            {
                                AutoCompleteStringCollection strings = new AutoCompleteStringCollection();
                                foreach (XmlElement element3 in element2.ChildNodes)
                                {
                                    strings.Add(element3.InnerText);
                                }
                                complete.AutoCompleteCustomSource = strings;
                            }
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Failure in reading in the input history (auto complete) file.(JobForm.ReadHistoryFile)");
                    Console.WriteLine("(" + HistoryFile + ")");
                }
            }
        }

        private void SaveHistoryFile(string HistoryFile)
        {
            if (this.AuCtrlList.Count != 0)
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    XmlElement newChild = doc.CreateElement("History");
                    doc.AppendChild(newChild);
                    foreach (Control control in this.AuCtrlList)
                    {
                        Naccs.Common.CustomControls.IAutoComplete complete = (Naccs.Common.CustomControls.IAutoComplete) control;
                        XmlElement element2 = null;
                        bool flag = true;
                        foreach (XmlElement element3 in newChild.ChildNodes)
                        {
                            if (element3.GetAttribute("id") == complete.id)
                            {
                                element2 = element3;
                                flag = false;
                            }
                        }
                        if (element2 == null)
                        {
                            element2 = doc.CreateElement("Item");
                            element2.SetAttribute("id", complete.id);
                        }
                        int num = 0;
                        if (complete.AutoCompleteCustomSource.Count > 20)
                        {
                            num = complete.AutoCompleteCustomSource.Count - 20;
                        }
                        for (int i = num; i < complete.AutoCompleteCustomSource.Count; i++)
                        {
                            bool flag2 = true;
                            foreach (XmlElement element4 in element2)
                            {
                                if (element4.InnerText == complete.AutoCompleteCustomSource[i])
                                {
                                    flag2 = false;
                                    break;
                                }
                            }
                            if (flag2)
                            {
                                XmlElement element5 = doc.CreateElement("Data");
                                element5.InnerText = complete.AutoCompleteCustomSource[i];
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
                    Console.WriteLine("Failure in saving the input history (auto complete) file.");
                    Console.WriteLine("(" + HistoryFile + ")");
                }
            }
        }

        protected override void SetFieldError(string id, int errorPage, bool flgInputErr, int idx)
        {
            string idDataFromTbl = this.JP.GetIdDataFromTbl(id, errorPage);
            if (flgInputErr)
            {
                this.JP.JobErrs.SetInputErrInfo(id, errorPage, idDataFromTbl);
            }
            else
            {
                this.JP.JobErrs.SetErrInfo(id, errorPage, idDataFromTbl);
            }
            List<Control> lstCtrl = new List<Control>();
            this.JP.GetControlsFromID(this.JP, id, lstCtrl);
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

        protected override void SetFocusField(string id, int errorPage, int idx)
        {
            Control c = null;
            IItemAttributesEx ex = null;
            List<Control> lstCtrl = new List<Control>();
            this.JP.GetControlsFromID(this.JP, id, lstCtrl);
            if (lstCtrl.Count > 0)
            {
                if ((errorPage > 0) && ((this.JP.RepeatPos > errorPage) || (errorPage > (this.JP.RepeatPos + this.JP.BsCount))))
                {
                    int num;
                    if (this.JP.RepeatPos < errorPage)
                    {
                        num = 1;
                    }
                    else
                    {
                        num = -1;
                    }
                    int num2 = 0;
                    int repeatPos = this.JP.RepeatPos;
                    while (((num == 1) && ((repeatPos > this.JP.RepeatCnt) || (this.JP.RepeatCnt > (repeatPos + this.JP.BsCount)))) || ((num == -1) && (repeatPos != 1)))
                    {
                        num2++;
                        repeatPos += (this.JP.BsCount + 1) * num;
                        if ((repeatPos <= errorPage) && (errorPage <= (repeatPos + this.JP.BsCount)))
                        {
                            for (int j = 0; j < num2; j++)
                            {
                                if (num == 1)
                                {
                                    this.JP.bindingNavigatorMoveNextItem_Click(null, null);
                                }
                                else
                                {
                                    this.JP.bindingNavigatorMovePreviousItem_Click(null, null);
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
                        this.JP.OpenTabPage(c);
                        this.JP.Focus();
                        LBDataGridView view = (LBDataGridView) c;
                        view.Focus();
                        view.SetFocusCell(id, errorPage - 1);
                        return;
                    }
                    ex = (IItemAttributesEx) c;
                    if (ex.CurrentPage == errorPage)
                    {
                        this.JP.OpenTabPage(c);
                        this.JP.Focus();
                        c.Focus();
                        return;
                    }
                }
            }
            MessageDialog dialog = new MessageDialog();
            dialog.ShowMessage("E531", "", null);
            dialog.Dispose();
        }

        private void SetHistory(XmlDocument doc)
        {
            try
            {
                foreach (XmlElement element2 in doc.DocumentElement.ChildNodes)
                {
                    string attribute = element2.GetAttribute("id");
                    foreach (Control control in this.AuCtrlList)
                    {
                        Naccs.Common.CustomControls.IAutoComplete complete = (Naccs.Common.CustomControls.IAutoComplete) control;
                        if (attribute == complete.id)
                        {
                            AutoCompleteStringCollection strings = new AutoCompleteStringCollection();
                            foreach (XmlElement element3 in element2.ChildNodes)
                            {
                                strings.Add(element3.InnerText);
                            }
                            complete.AutoCompleteCustomSource = strings;
                        }
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
            this.JP.Items.DsItems.Tables[RepID].Rows[No][stID] = this.JP.Items.GetRevisionSetData(RepID, stID, stData);
        }

        public override void SetRecvData(IData data)
        {
            base.CmnClearJob();
            base.ExNaccsData = data;
            this.JP.SetData(base.ExNaccsData.Items, Naccs.Common.Generator.ItemInfo.JobDataStatus.jdRecv, "");
            base.txtInputInfo.Text = base.ExNaccsData.Header.InputInfo.Trim();
            base.ShowAttachInfo(base.ExNaccsData);
        }

        public override void SetRes(IData data, bool flgSetColor)
        {
            base.SetRes(data, flgSetColor);
            if (this.flgBar)
            {
                this.JP.ItemClearAll();
                base.CursorTop(base.pnlGuiLayout);
            }
        }

        public override void SetSendData(IData data)
        {
            base.CmnClearJob();
            this.JP.ItemClearAll();
            base.ExNaccsData = data;
            this.JP.SetData(base.ExNaccsData.Items, Naccs.Common.Generator.ItemInfo.JobDataStatus.jdSend, "");
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
    }
}

