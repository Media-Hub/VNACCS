namespace Naccs.Core.Job
{
    using Naccs.Common;
    using Naccs.Common.CustomControls;
    using Naccs.Common.Function;
    using Naccs.Common.Generator;
    using Naccs.Core.Properties;
    using Naccs.Core.Settings;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    [DisplayName("業務画面"), ToolboxBitmap(typeof(Form))]
    public class CommonJobPanel : UserControl
    {
        private ToolStripButton bindingNavigatorMoveFirstItem;
        private ToolStripButton bindingNavigatorMoveLastItem;
        private ToolStripButton bindingNavigatorMoveNextItem;
        private ToolStripButton bindingNavigatorMovePreviousItem;
        private ToolStripSeparator bindingNavigatorSeparator;
        private ToolStripSeparator bindingNavigatorSeparator1;
        private ToolStripSeparator bindingNavigatorSeparator2;
        private BindingSourceEx bsCom;
        protected BindingSourceEx bsRepeat;
        private IContainer components;
        private int defaultHeight;
        private List<int> DispIdxList;
        private bool flgPaint = true;
        private float fontZoom;
        private bool isActive;
        private Naccs.Common.Generator.ItemInfo items;
        private JobErrInfo jobErrs;
        protected ToolStripLabel lblNextpage;
        public LBNavigator lbNavigator;
        private List<Component> PList;
        protected List<BindingSourceEx> repBindingSources;
        private ToolStripLabel toolStripLabel1;
        private ToolStripTextBox toolStripTextBox1;
        private ToolTip toolTip;

        public event EventHandler OnSendBarcode;

        public event EventHandler OnShowGuide;

        public CommonJobPanel()
        {
            this.InitializeComponent();
            base.AutoScaleMode = AutoScaleMode.Font;
            this.fontZoom = 0f;
            this.repBindingSources = new List<BindingSourceEx>();
            this.PList = new List<Component>();
            this.DispIdxList = new List<int>();
            base.Enter += new EventHandler(this.CommonJobPanel_Enter);
            base.Leave += new EventHandler(this.CommonJobPanel_Leave);
            this.DoubleBuffered = true;
        }

        public LBDataGridView ActiveEditingDataGrid()
        {
            Control activeControl = base.ActiveControl;
            if (activeControl is LBDataGridView)
            {
                return (LBDataGridView) activeControl;
            }
            if (activeControl is LBTextBoxEditingControl)
            {
                return (LBDataGridView) ((LBTextBoxEditingControl) activeControl).EditingControlDataGridView;
            }
            return null;
        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            this.toolStripTextBox1.Focus();
            this.bsRepeat.MoveFirst();
        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            this.toolStripTextBox1.Focus();
            this.bsRepeat.Position = this.ViewLastPage();
        }

        public void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            if ((this.bsRepeat.Position + this.repBindingSources.Count) < (this.bsRepeat.Count - 1))
            {
                this.toolStripTextBox1.Focus();
                this.bsRepeat.MoveNext();
                this.bsRepeat.Position += this.repBindingSources.Count;
            }
        }

        public void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            if (this.bsRepeat.Position > 0)
            {
                this.toolStripTextBox1.Focus();
                this.bsRepeat.MovePrevious();
                this.bsRepeat.Position -= this.repBindingSources.Count;
            }
        }

        private void bsRepeat_PositionChanged(object sender, EventArgs e)
        {
            LBPanel panel = null;
            LBGroupBox box = null;
            if ((this.repBindingSources != null) && (this.bsRepeat.Position != -1))
            {
                this.ViewLastPage();
                this.toolStripTextBox1.Text = ((this.bsRepeat.Position / (this.repBindingSources.Count + 1)) + 1).ToString();
                if (this.Items.CheckDataExists(this.bsRepeat.DataMember, this.bsRepeat.Position + this.repBindingSources.Count))
                {
                    this.lblNextpage.Text = "*";
                }
                else
                {
                    this.lblNextpage.Text = "";
                }
                int num2 = this.bsRepeat.Position + 1;
                for (int i = 0; i < this.repBindingSources.Count; i++)
                {
                    if (num2 >= this.Items.DsItems.Tables[this.bsRepeat.DataMember].Rows.Count)
                    {
                        if (this.PList[i] is LBPanel)
                        {
                            panel = (LBPanel) this.PList[i];
                            panel.Visible = false;
                        }
                        if (this.PList[i] is LBGroupBox)
                        {
                            box = (LBGroupBox) this.PList[i];
                            box.Visible = false;
                        }
                    }
                    else
                    {
                        if (this.PList[i] is LBPanel)
                        {
                            panel = (LBPanel) this.PList[i];
                            panel.CurrentPage = num2;
                            panel.Visible = true;
                        }
                        if (this.PList[i] is LBGroupBox)
                        {
                            box = (LBGroupBox) this.PList[i];
                            box.CurrentPage = num2;
                            box.Visible = true;
                        }
                    }
                    this.repBindingSources[i].Position = num2++;
                }
                this.NavigatorItemChanged();
            }
        }

        public void ChangeCtlColor(Control root)
        {
            bool flag = true;
            foreach (Control control in root.Controls)
            {
                if (flag)
                {
                    flag = false;
                }
                System.Type c = control.GetType();
                if (typeof(IItemAttributesEx).IsAssignableFrom(c))
                {
                    ((IItemAttributesEx) control).SetBackColor();
                }
                else
                {
                    if (control is LBDataGridView)
                    {
                        LBDataGridView hC = (LBDataGridView) control;
                        bool visible = base.Visible;
                        try
                        {
                            if (!visible)
                            {
                                base.Visible = !visible;
                            }
                            this.ChangeGridColor(hC);
                        }
                        finally
                        {
                            if (base.Visible != visible)
                            {
                                base.Visible = visible;
                            }
                        }
                    }
                    if (control.HasChildren)
                    {
                        this.ChangeCtlColor(control);
                    }
                }
            }
        }

        public void ChangeFontSize(ModeFont mF)
        {
            switch (mF)
            {
                case ModeFont.normal:
                    this.FontZoom -= this.fontZoom;
                    return;

                case ModeFont.large:
                    this.FontZoom += (this.fontZoom < 0f) ? (2f - this.fontZoom) : 2f;
                    return;

                case ModeFont.small:
                    this.FontZoom -= (this.fontZoom > 0f) ? (1f + this.fontZoom) : 1f;
                    return;
            }
        }

        public void ChangeGridColor(Control hC)
        {
            LBDataGridView view = (LBDataGridView) hC;
            for (int i = 0; i < view.Columns.Count; i++)
            {
                for (int j = 0; j < view.Rows.Count; j++)
                {
                    LBTextBoxCell cell = (LBTextBoxCell) view[i, j];
                    DataGridViewColumn owningColumn = cell.OwningColumn;
                    LBTextBoxColumn column2 = (LBTextBoxColumn) owningColumn;
                    if (column2.input_output == "-")
                    {
                        cell.Style.BackColor = DesignControls.ReadOnlyBackColor;
                    }
                    else if (column2.input_output != "-")
                    {
                        if (column2.required == "M")
                        {
                            if (cell.Value is DBNull)
                            {
                                cell.Style.BackColor = owningColumn.DefaultCellStyle.BackColor;
                            }
                            else if (string.IsNullOrEmpty((string) cell.Value))
                            {
                                cell.Style.BackColor = owningColumn.DefaultCellStyle.BackColor;
                            }
                            else
                            {
                                cell.Style.BackColor = DesignControls.NormalBackColor;
                            }
                        }
                        else if (!(cell.Value is DBNull) && !string.IsNullOrEmpty((string) cell.Value))
                        {
                            cell.Style.BackColor = DesignControls.NormalBackColor;
                        }
                    }
                }
            }
        }

        public void CheckAll(string mode, Control Ctl)
        {
            foreach (Control control in Ctl.Controls)
            {
                if (control.HasChildren)
                {
                    this.CheckAll(mode, control);
                }
                else if (control is LBCheckBox)
                {
                    LBCheckBox box = (LBCheckBox) control;
                    if (box.CurrentPage == 0)
                    {
                        if (box.input_output == "+")
                        {
                            this.Items.DsItems.Tables[Naccs.Common.Generator.ItemInfo.tblComName].Rows[0][box.id] = mode;
                            this.Items.DsItems.Tables[Naccs.Common.Generator.ItemInfo.tblComName].AcceptChanges();
                        }
                    }
                    else if (box.input_output == "+")
                    {
                        for (int i = 0; i < this.Items.DsItems.Tables[box.Rep_ID].Rows.Count; i++)
                        {
                            this.Items.DsItems.Tables[box.Rep_ID].Rows[i][box.id] = mode;
                        }
                        this.Items.DsItems.Tables[box.Rep_ID].AcceptChanges();
                    }
                }
            }
        }

        private int ClipboardLineCount()
        {
            int num = 0;
            IDataObject dataObject = Clipboard.GetDataObject();
            if (dataObject.GetDataPresent(DataFormats.UnicodeText))
            {
                using (StringReader reader = new StringReader((string) dataObject.GetData(DataFormats.UnicodeText)))
                {
                    while (reader.ReadLine() != null)
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        private void CommonJobPanel_Enter(object sender, EventArgs e)
        {
            this.isActive = true;
        }

        private void CommonJobPanel_Leave(object sender, EventArgs e)
        {
            this.isActive = false;
        }

        private void CommonJobPanel_Paint(object sender, PaintEventArgs e)
        {
            if (this.flgPaint)
            {
                this.flgPaint = false;
                if (this.DispIdxList.Count != 0)
                {
                    foreach (Control control in base.Controls)
                    {
                        if (control is DataGridView)
                        {
                            DataGridView view = (DataGridView) control;
                            for (int i = 0; i < this.DispIdxList.Count; i++)
                            {
                                view.Columns[i].DisplayIndex = this.DispIdxList[i];
                            }
                        }
                    }
                }
            }
        }

        private string CopyStringCreation(ArrayList RowList, LBDataGridView LBDG)
        {
            StringBuilder builder = new StringBuilder();
            int num2 = 0;
            for (num2 = 0; num2 < RowList.Count; num2++)
            {
                for (int i = 0; i < LBDG.ColumnCount; i++)
                {
                    string str = LBDG.Rows[(int) RowList[num2]].Cells[i].Value.ToString();
                    if (i != (LBDG.ColumnCount - 1))
                    {
                        builder.Append(str + "\t");
                    }
                    else
                    {
                        builder.Append(str + "\r\n");
                    }
                }
            }
            return builder.ToString();
        }

        public void CopyTsvData()
        {
            string rep = null;
            int page = 0;
            string repPage = this.GetRepPage(out rep, out page);
            if ((rep != null) && (repPage != null))
            {
                this.Items.GetTsvData(rep, page, repPage);
            }
        }

        public virtual int CreatePanel(string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    return -2;
                }
                this.flgPaint = true;
                CreateComponentJob creator = new CreateComponentJob(this.lbNavigator) {
                    DS = this.Items.DsItems,
                    BsCom = this.bsCom,
                    BsRepeat = this.bsRepeat,
                    BsRList = this.repBindingSources,
                    PList = this.PList
                };
                this.defaultHeight = new LayoutGenerator(creator) { ListDispIdx = this.DispIdxList }.CreateLayout(fileName, this);
                if (this.bsRepeat.DataMember != "")
                {
                    this.bsRepeat.MoveLast();
                    this.bsRepeat.MoveFirst();
                    for (int i = 0; i < this.repBindingSources.Count; i++)
                    {
                        this.repBindingSources[i].Position = i + 1;
                    }
                    this.NavigatorItemChanged();
                    this.ViewLastPage();
                    this.toolStripTextBox1.Text = "1";
                }
                this.ResizeFont(this, 0f);
                this.SetEvent(this);
            }
            catch (NaccsException exception)
            {
                if (exception.Code == 0x1f8)
                {
                    return -4;
                }
                return -99;
            }
            catch (Exception)
            {
                return -99;
            }
            return 0;
        }

        protected override void Dispose(bool disposing)
        {
            this.toolStripTextBox1.KeyDown -= new KeyEventHandler(this.toolStripTextBox1_KeyDown);
            this.toolStripTextBox1.TextBox.MouseMove -= new MouseEventHandler(this.toolStripTextBox_MouseMove);
            this.bindingNavigatorMovePreviousItem.Click -= new EventHandler(this.bindingNavigatorMovePreviousItem_Click);
            this.bindingNavigatorMoveFirstItem.Click -= new EventHandler(this.bindingNavigatorMoveFirstItem_Click);
            this.bindingNavigatorMoveNextItem.Click -= new EventHandler(this.bindingNavigatorMoveNextItem_Click);
            this.bindingNavigatorMoveLastItem.Click -= new EventHandler(this.bindingNavigatorMoveLastItem_Click);
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void GetAutoCompList(Control root, List<Control> AuList)
        {
            foreach (Control control in root.Controls)
            {
                if (control is Naccs.Common.CustomControls.IAutoComplete)
                {
                    Naccs.Common.CustomControls.IAutoComplete complete = (Naccs.Common.CustomControls.IAutoComplete) control;
                    if (complete.AutoComplete)
                    {
                        AuList.Add(control);
                    }
                }
                else if (control.HasChildren)
                {
                    this.GetAutoCompList(control, AuList);
                }
            }
        }

        public Control GetControlFromID(Control root, string iD)
        {
            Control controlFromID = null;
            foreach (Control control2 in root.Controls)
            {
                System.Type c = control2.GetType();
                if (typeof(IItemAttributes).IsAssignableFrom(c))
                {
                    IItemAttributes attributes = (IItemAttributes) control2;
                    if (attributes.id == iD)
                    {
                        return control2;
                    }
                }
                else
                {
                    if (control2 is LBDataGridView)
                    {
                        LBDataGridView view = (LBDataGridView) control2;
                        for (int i = 0; i < view.Columns.Count; i++)
                        {
                            LBTextBoxColumn column = (LBTextBoxColumn) view.Columns[i];
                            if (column.id == iD)
                            {
                                return control2;
                            }
                        }
                    }
                    if (control2.HasChildren)
                    {
                        controlFromID = this.GetControlFromID(control2, iD);
                        if (controlFromID != null)
                        {
                            return controlFromID;
                        }
                    }
                }
            }
            return controlFromID;
        }

        public void GetControlsFromID(Control root, string iD, List<Control> lstCtrl)
        {
            foreach (Control control in root.Controls)
            {
                System.Type c = control.GetType();
                if (typeof(IItemAttributes).IsAssignableFrom(c))
                {
                    IItemAttributes attributes = (IItemAttributes) control;
                    if (attributes.id == iD)
                    {
                        lstCtrl.Add(control);
                    }
                }
                else
                {
                    if (control is LBDataGridView)
                    {
                        LBDataGridView view = (LBDataGridView) control;
                        for (int i = 0; i < view.Columns.Count; i++)
                        {
                            LBTextBoxColumn column = (LBTextBoxColumn) view.Columns[i];
                            if (column.id == iD)
                            {
                                lstCtrl.Add(control);
                            }
                        }
                    }
                    if (control.HasChildren)
                    {
                        this.GetControlsFromID(control, iD, lstCtrl);
                    }
                }
            }
        }

        public string GetHintText(string stID)
        {
            return this.Items.GetFieldInfo(stID);
        }

        public string GetRepPage(out string Rep, out int Page)
        {
            Rep = null;
            Page = 0;
            Control activeControl = base.ActiveControl;
            if (activeControl != null)
            {
                if (activeControl is IItemAttributesEx)
                {
                    IItemAttributesEx ex = (IItemAttributesEx) activeControl;
                    if (ex.Rep_ID != "")
                    {
                        Rep = ex.Rep_ID;
                        Page = ex.CurrentPage - 1;
                        return ex.id;
                    }
                }
                LBDataGridView editingControlDataGridView = null;
                if (activeControl is LBDataGridView)
                {
                    editingControlDataGridView = (LBDataGridView) activeControl;
                }
                else if (activeControl is LBTextBoxEditingControl)
                {
                    LBTextBoxEditingControl control2 = (LBTextBoxEditingControl) activeControl;
                    editingControlDataGridView = (LBDataGridView) control2.EditingControlDataGridView;
                }
                if (editingControlDataGridView != null)
                {
                    Rep = editingControlDataGridView.repetition_id;
                    Page = editingControlDataGridView.SelectedCells[0].RowIndex;
                    if (editingControlDataGridView.AColumnID == "")
                    {
                        Rep = null;
                        return null;
                    }
                    return editingControlDataGridView.AColumnID;
                }
            }
            return null;
        }

        public TabControl GetTabControl()
        {
            Control activeControl = base.ActiveControl;
            if (activeControl != null)
            {
                if (activeControl is TabControl)
                {
                    return (TabControl) activeControl;
                }
                for (Control control2 = activeControl.Parent; !(control2 is CommonJobPanel); control2 = control2.Parent)
                {
                    if (control2 is TabControl)
                    {
                        return (TabControl) control2;
                    }
                }
            }
            return null;
        }

        public void GridCopy()
        {
            LBDataGridView lBDG = null;
            Control activeControl = base.ActiveControl;
            if (activeControl is LBDataGridView)
            {
                lBDG = (LBDataGridView) activeControl;
            }
            if (activeControl is LBTextBoxEditingControl)
            {
                LBTextBoxEditingControl control2 = (LBTextBoxEditingControl) activeControl;
                lBDG = (LBDataGridView) control2.EditingControlDataGridView;
            }
            ArrayList rowList = new ArrayList();
            rowList.Add(lBDG.CurrentCell.RowIndex);
            Clipboard.SetDataObject(this.CopyStringCreation(rowList, lBDG));
        }

        public void GridPasteTsvData()
        {
            LBDataGridView hC = null;
            Control activeControl = base.ActiveControl;
            if (activeControl is LBDataGridView)
            {
                hC = (LBDataGridView) activeControl;
            }
            if (activeControl is LBTextBoxEditingControl)
            {
                LBTextBoxEditingControl control2 = (LBTextBoxEditingControl) activeControl;
                hC = (LBDataGridView) control2.EditingControlDataGridView;
            }
            string rep = null;
            int page = 0;
            this.GetRepPage(out rep, out page);
            string text = Clipboard.GetText();
            if (!string.IsNullOrEmpty(text))
            {
                string[] strArray = text.TrimEnd(new char[] { '\n' }).TrimEnd(new char[] { '\r' }).Split(new char[] { '\r' });
                ArrayList list = new ArrayList();
                ArrayList list2 = new ArrayList();
                foreach (DataGridViewCell cell in hC.SelectedCells)
                {
                    list.Add(cell.RowIndex);
                }
                foreach (DataGridViewCell cell2 in hC.SelectedCells)
                {
                    list2.Add(cell2.ColumnIndex);
                }
                list.Sort();
                list2.Sort();
                int num2 = (int) list[0];
                foreach (string str3 in strArray)
                {
                    if (num2 >= hC.RowCount)
                    {
                        break;
                    }
                    int num3 = (int) list2[0];
                    foreach (string str4 in str3.Split(new char[] { '\t' }))
                    {
                        if (num3 >= hC.ColumnCount)
                        {
                            break;
                        }
                        if (this.Items.PasteDecision(hC.Columns[num3].DataPropertyName))
                        {
                            hC.Rows[num2].Cells[num3].Value = this.Items.GetRevisionSetData(rep, hC.Columns[num3].DataPropertyName, str4);
                        }
                        num3++;
                    }
                    num2++;
                }
                if (hC != null)
                {
                    this.ChangeGridColor(hC);
                }
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(CommonJobPanel));
            this.toolTip = new ToolTip(this.components);
            this.lbNavigator = new LBNavigator();
            this.bindingNavigatorMoveFirstItem = new ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new ToolStripButton();
            this.bindingNavigatorSeparator = new ToolStripSeparator();
            this.toolStripTextBox1 = new ToolStripTextBox();
            this.toolStripLabel1 = new ToolStripLabel();
            this.bindingNavigatorSeparator1 = new ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new ToolStripButton();
            this.bindingNavigatorMoveLastItem = new ToolStripButton();
            this.bindingNavigatorSeparator2 = new ToolStripSeparator();
            this.lblNextpage = new ToolStripLabel();
            this.bsCom = new BindingSourceEx(this.components);
            this.bsRepeat = new BindingSourceEx(this.components);
            this.lbNavigator.BeginInit();
            this.lbNavigator.SuspendLayout();
            ((ISupportInitialize) this.bsCom).BeginInit();
            ((ISupportInitialize) this.bsRepeat).BeginInit();
            base.SuspendLayout();
            this.toolTip.IsBalloon = true;
            this.toolTip.ToolTipIcon = ToolTipIcon.Info;
            this.lbNavigator.AddNewItem = null;
            this.lbNavigator.CountItem = null;
            this.lbNavigator.DeleteItem = null;
            this.lbNavigator.Dock = DockStyle.None;
            this.lbNavigator.Font = new Font("Tahoma", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x80);
            this.lbNavigator.Items.AddRange(new ToolStripItem[] { this.bindingNavigatorMoveFirstItem, this.bindingNavigatorMovePreviousItem, this.bindingNavigatorSeparator, this.toolStripTextBox1, this.toolStripLabel1, this.bindingNavigatorSeparator1, this.bindingNavigatorMoveNextItem, this.bindingNavigatorMoveLastItem, this.bindingNavigatorSeparator2, this.lblNextpage });
            this.lbNavigator.Location = new Point(0x19, 0xce);
            this.lbNavigator.MoveFirstItem = null;
            this.lbNavigator.MoveLastItem = null;
            this.lbNavigator.MoveNextItem = null;
            this.lbNavigator.MovePreviousItem = null;
            this.lbNavigator.Name = "lbNavigator";
            this.lbNavigator.PositionItem = null;
            this.lbNavigator.repetition_id = "";
            this.lbNavigator.repetition_max = 1;
            this.lbNavigator.Size = new System.Drawing.Size(0xba, 0x19);
            this.lbNavigator.Stretch = true;
            this.lbNavigator.TabIndex = 12;
            this.lbNavigator.Text = "lbNavigator1";
            this.lbNavigator.Visible = false;
            this.bindingNavigatorMoveFirstItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
//            this.bindingNavigatorMoveFirstItem.Image = (Image) manager.GetObject("bindingNavigatorMoveFirstItem.Image");
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(0x17, 0x16);
            this.bindingNavigatorMoveFirstItem.Text = "Hạng mục đầu ti\x00ean";
            this.bindingNavigatorMoveFirstItem.Click += new EventHandler(this.bindingNavigatorMoveFirstItem_Click);
            this.bindingNavigatorMovePreviousItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
//            this.bindingNavigatorMovePreviousItem.Image = (Image) manager.GetObject("bindingNavigatorMovePreviousItem.Image");
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(0x17, 0x16);
            this.bindingNavigatorMovePreviousItem.Text = "Hạng mục trước";
            this.bindingNavigatorMovePreviousItem.Click += new EventHandler(this.bindingNavigatorMovePreviousItem_Click);
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 0x19);
            this.toolStripTextBox1.BackColor = SystemColors.Window;
            this.toolStripTextBox1.Font = new Font("Tahoma", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x80);
            this.toolStripTextBox1.MaxLength = 4;
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(0x2b, 0x19);
            this.toolStripTextBox1.TextBox.ContextMenuStrip = new ContextMenuStrip();
            this.toolStripTextBox1.KeyDown += new KeyEventHandler(this.toolStripTextBox1_KeyDown);
            this.toolStripTextBox1.TextBox.MouseMove += new MouseEventHandler(this.toolStripTextBox_MouseMove);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(0x13, 0x16);
            this.toolStripLabel1.Text = "/0";
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 0x19);
            this.bindingNavigatorMoveNextItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
//            this.bindingNavigatorMoveNextItem.Image = (Image) manager.GetObject("bindingNavigatorMoveNextItem.Image");
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(0x17, 0x16);
            this.bindingNavigatorMoveNextItem.Text = "Hạng mục tiếp theo";
            this.bindingNavigatorMoveNextItem.Click += new EventHandler(this.bindingNavigatorMoveNextItem_Click);
            this.bindingNavigatorMoveLastItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
//            this.bindingNavigatorMoveLastItem.Image = (Image) manager.GetObject("bindingNavigatorMoveLastItem.Image");
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(0x17, 0x16);
            this.bindingNavigatorMoveLastItem.Text = "Hạng mục cuối c\x00f9ng";
            this.bindingNavigatorMoveLastItem.Click += new EventHandler(this.bindingNavigatorMoveLastItem_Click);
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 0x19);
            this.lblNextpage.Name = "lblNextpage";
            this.lblNextpage.Size = new System.Drawing.Size(0, 0x16);
            this.bsCom.DataMember = "COM";
            this.bsCom.PositionItem = null;
            this.bsRepeat.PositionItem = null;
            this.bsRepeat.PositionChanged += new EventHandler(this.bsRepeat_PositionChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
///            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoScroll = true;
            base.Controls.Add(this.lbNavigator);
            base.Name = "CommonJobPanel";
            this.Font = new Font("Courier New", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.Size = new System.Drawing.Size(0x267, 0x381);
            base.Paint += new PaintEventHandler(this.CommonJobPanel_Paint);
            this.lbNavigator.EndInit();
            this.lbNavigator.ResumeLayout(false);
            this.lbNavigator.PerformLayout();
            ((ISupportInitialize) this.bsCom).EndInit();
            ((ISupportInitialize) this.bsRepeat).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public bool InsertLines(bool clipboard)
        {
            bool flag = false;
            LBDataGridView dataGrid = this.ActiveEditingDataGrid();
            if (dataGrid != null)
            {
                string rep = null;
                int page = 0;
                this.GetRepPage(out rep, out page);
                if (rep == null)
                {
                    return flag;
                }
                List<int> list = new List<int>();
                int insertLine = 0;
                if (clipboard)
                {
                    list.Add(page);
                    insertLine = this.ClipboardLineCount();
                }
                else
                {
                    list = this.SelectedLines(dataGrid);
                    insertLine = 1;
                }
                if (insertLine <= 0)
                {
                    return flag;
                }
                this.Items.InsertRows(rep, list.ToArray(), insertLine);
                if (clipboard)
                {
                    this.PasteTsvData();
                }
                flag = true;
                this.lblNextpage.Text = "";
            }
            return flag;
        }

        public virtual void ItemClearAll()
        {
        }

        public void ItemClearCloseUp()
        {
            string rep = null;
            int page = 0;
            this.GetRepPage(out rep, out page);
            if (rep != null)
            {
                this.Items.ClearCloseUp(rep, page);
                this.lblNextpage.Text = "";
            }
        }

        public virtual void ItemClearCommon()
        {
        }

        public void ItemClearCurrentToEnd()
        {
            string rep = null;
            int page = 0;
            this.GetRepPage(out rep, out page);
            if (rep != null)
            {
                this.Items.ClearCurrentToEnd(rep, page);
                this.lblNextpage.Text = "";
            }
        }

        private void LBTextBox_OnSendBarcode(object sender, EventArgs e)
        {
            if (this.OnSendBarcode != null)
            {
                this.OnSendBarcode(sender, e);
            }
        }

        private void NavigatorItemChanged()
        {
            bool flag = (this.bsRepeat.Position == 0) || (this.bsRepeat.Position < 0);
            this.bindingNavigatorMoveFirstItem.Enabled = !flag;
            this.bindingNavigatorMovePreviousItem.Enabled = !flag;
            if ((this.bsRepeat.Position + this.repBindingSources.Count) >= (this.bsRepeat.Count - 1))
            {
                this.bindingNavigatorMoveNextItem.Enabled = false;
            }
            else
            {
                this.bindingNavigatorMoveNextItem.Enabled = true;
            }
            this.bindingNavigatorMoveLastItem.Enabled = true;
        }

        private void OmitCtlInputSupport(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (control is LBDivTextBox)
            {
                Control controlFromID;
                Control root = null;
                LBDivTextBox box = (LBDivTextBox) control;
                Control parent = control.Parent;
                System.Type c = parent.GetType();
                int iNo = 1;
                while (!(parent is CommonJobPanel))
                {
                    if (typeof(IRepContainerAttributes).IsAssignableFrom(c))
                    {
                        IRepContainerAttributes attributes = (IRepContainerAttributes) parent;
                        if (attributes.repetition_id != "")
                        {
                            root = parent;
                            iNo = attributes.repetition_max;
                            break;
                        }
                    }
                    c = parent.Parent.GetType();
                }
                string refName = box.RefName;
                bool flag = false;
                if (root == null)
                {
                    controlFromID = this.GetControlFromID(this, refName);
                }
                else
                {
                    if (box.RefName.IndexOf("-") >= 0)
                    {
                        int index = box.RefName.IndexOf("-");
                        refName = box.RefName.Remove(index, 1);
                        flag = true;
                    }
                    controlFromID = this.GetControlFromID(root, refName);
                }
                if (controlFromID is LBDivTextBox)
                {
                    LBDivTextBox box2 = (LBDivTextBox) controlFromID;
                    if (box2.Rep_ID == box.Rep_ID)
                    {
                        string idData;
                        StrFunc func = StrFunc.CreateInstance();
                        if (flag)
                        {
                            if (box.CurrentPage == 1)
                            {
                                idData = this.Items.GetIdData(refName, iNo);
                            }
                            else
                            {
                                idData = this.Items.GetIdData(refName, box.CurrentPage - 1);
                            }
                            idData = func.CutByteLength(idData, box2.L_MaxLength);
                            idData = func.CutByteLength(idData, box.L_MaxLength);
                        }
                        else
                        {
                            idData = func.CutByteLength(box2.L_Text, box.L_MaxLength);
                        }
                        box.L_Text = idData.TrimEnd(null);
                    }
                }
            }
        }

        private void OnControlEnter(object sender, EventArgs e)
        {
            if (this.OnShowGuide != null)
            {
                this.OnShowGuide(sender, e);
            }
        }

        public void OpenTabPage(Control C)
        {
            Control parent = C.Parent;
            TabPage page = null;
            while (!(parent is CommonJobPanel))
            {
                if (parent is LBTabControl)
                {
                    LBTabControl control2 = (LBTabControl) parent;
                    control2.SelectedTab = page;
                }
                if (parent is TabPage)
                {
                    page = (TabPage) parent;
                }
                parent = parent.Parent;
            }
        }

        public void PasteTsvData()
        {
            LBDataGridView hC = null;
            Control activeControl = base.ActiveControl;
            if (activeControl is LBDataGridView)
            {
                hC = (LBDataGridView) activeControl;
            }
            if (activeControl is LBTextBoxEditingControl)
            {
                LBTextBoxEditingControl control2 = (LBTextBoxEditingControl) activeControl;
                hC = (LBDataGridView) control2.EditingControlDataGridView;
            }
            string rep = null;
            int page = 0;
            string repPage = this.GetRepPage(out rep, out page);
            if ((rep != null) && (repPage != null))
            {
                this.Items.SetTsvData(rep, page, repPage);
                if (hC != null)
                {
                    this.ChangeGridColor(hC);
                }
            }
        }

        public bool RemoveLines()
        {
            bool flag = false;
            LBDataGridView dataGrid = this.ActiveEditingDataGrid();
            if (dataGrid != null)
            {
                string rep = null;
                int page = 0;
                this.GetRepPage(out rep, out page);
                if (rep != null)
                {
                    List<int> list = this.SelectedLines(dataGrid);
                    if (list.Count > 0)
                    {
                        this.Items.RemoveRows(rep, list.ToArray());
                        flag = true;
                        this.lblNextpage.Text = "";
                    }
                }
            }
            return flag;
        }

        private void ResizeFont(Control control, float fsize)
        {
            float emSize = control.Font.Size + fsize;
            if (emSize > 0f)
            {
                control.Font = new Font(control.Font.Name, emSize, control.Font.Style, control.Font.Unit);
                if (control is LBDataGridView)
                {
                    LBDataGridView view = (LBDataGridView) control;
                    DataGridViewCellStyle columnHeadersDefaultCellStyle = view.ColumnHeadersDefaultCellStyle;
                    if ((columnHeadersDefaultCellStyle != null) && (columnHeadersDefaultCellStyle.Font != null))
                    {
                        float num2 = columnHeadersDefaultCellStyle.Font.Size + fsize;
                        view.ColumnHeadersDefaultCellStyle.Font = new Font(columnHeadersDefaultCellStyle.Font.Name, num2, columnHeadersDefaultCellStyle.Font.Style, GraphicsUnit.Point, 0x80);
                    }
                    for (int i = 0; i < view.Columns.Count; i++)
                    {
                        if (view.Columns[i].DefaultCellStyle.Font == null)
                        {
                            view.Columns[i].DefaultCellStyle.Font = new Font(view.Font.Name, emSize, view.Font.Style, view.Font.Unit);
                        }
                        else
                        {
                            float num4 = view.Columns[i].DefaultCellStyle.Font.Size + fsize;
                            if (num4 > 0f)
                            {
                                view.Columns[i].DefaultCellStyle.Font = new Font(view.Columns[i].DefaultCellStyle.Font.Name, num4, view.Columns[i].DefaultCellStyle.Font.Style, view.Columns[i].DefaultCellStyle.Font.Unit);
                            }
                        }
                    }
                }
                foreach (Control control2 in control.Controls)
                {
                    this.ResizeFont(control2, fsize);
                }
            }
        }

        public int SearchErrorField(out string[] idArray, out int errorPage, out string msg, bool flgCheckReq)
        {
            return this.SearchErrorField(out idArray, out errorPage, out msg, flgCheckReq, true);
        }

        public int SearchErrorField(out string[] idArray, out int errorPage, out string msg, bool flgCheckReq, bool flgCheckData)
        {
            int num = 0;
            string id = null;
            num = this.Items.SearchError(out id, out errorPage, flgCheckReq, flgCheckData);
            if (num == 0)
            {
                if (!this.Items.CheckCorrelation(out idArray, out errorPage, out msg))
                {
                    return -5;
                }
                return num;
            }
            idArray = new string[] { id };
            switch (num)
            {
                case -7:
                    msg = Resources.ResourceManager.GetString("MSG_TIME_ERROR");
                    return num;

                case -6:
                    msg = Resources.ResourceManager.GetString("MSG_DATE_ERROR");
                    return num;

                case -4:
                    msg = Resources.ResourceManager.GetString("MSG_EQUALLENGTH_ERROR");
                    return num;

                case -3:
                    msg = Resources.ResourceManager.GetString("MSG_FORBIDDENCHAR_ERROR");
                    return num;

                case -2:
                    msg = Resources.ResourceManager.GetString("MSG_NOTOMIT_ERROR");
                    return num;

                case -1:
                    msg = Resources.ResourceManager.GetString("MSG_NODATA_ERROR");
                    return num;
            }
            msg = "";
            return num;
        }

        private List<int> SelectedLines(LBDataGridView dataGrid)
        {
            List<int> list = new List<int>();
            foreach (DataGridViewRow row in dataGrid.SelectedRows)
            {
                list.Add(row.Index);
            }
            if (list.Count < 1)
            {
                foreach (DataGridViewCell cell in dataGrid.SelectedCells)
                {
                    if (!dataGrid.Rows[cell.RowIndex].Selected)
                    {
                        dataGrid.Rows[cell.RowIndex].Selected = true;
                        list.Add(cell.RowIndex);
                    }
                }
            }
            list.Sort();
            return list;
        }

        private void SetEvent(Control root)
        {
            foreach (Control control in root.Controls)
            {
                System.Type c = control.GetType();
                if (typeof(IItemAttributesEx).IsAssignableFrom(c))
                {
                    if (control is ExTextBox)
                    {
                        ExTextBox box = (ExTextBox) control;
                        if (box.Multiline)
                        {
                            box.SetNoWordWrap();
                        }
                    }
                    if ((control is LBTextBox) && (this.OnSendBarcode != null))
                    {
                        LBTextBox box2 = (LBTextBox) control;
                        if (box2.BarcodeSend)
                        {
                            box2.OnSendBarcode += new EventHandler(this.LBTextBox_OnSendBarcode);
                        }
                    }
                    if (control is LBInputComboBox)
                    {
                        LBInputComboBox box3 = (LBInputComboBox) control;
                        PathInfo info = PathInfo.CreateInstance();
                        if (box3.ReadTableFile(info.KioskTablesRoot))
                        {
                            this.Items.SetInitString(box3.id, box3.form);
                        }
                    }
                    control.ContextMenuStrip = this.ContextMenuStrip;
                    control.Enter += new EventHandler(this.OnControlEnter);
                    IItemAttributesEx ex = (IItemAttributesEx) control;
                    if (ex.required == "M")
                    {
                        this.toolTip.SetToolTip(control, DesignControls.stMsgRequired);
                    }
                    ex.JobErr = this.jobErrs;
                    this.Items.SetCheckInfo(ex.id, ex.check_date, ex.check_time);
                    if (c == DesignControls.DivTextBox)
                    {
                        LBDivTextBox box4 = (LBDivTextBox) control;
                        box4.OnOmitCtlInputSupport += new EventHandler(this.OmitCtlInputSupport);
                    }
                }
                else
                {
                    if (c == DesignControls.GridView)
                    {
                        LBDataGridView view = (LBDataGridView) control;
                        view.ContextMenuStrip = this.ContextMenuStrip;
                        view.OnColumnEnter += new EventHandler(this.OnControlEnter);
                        view.JobErr = this.jobErrs;
                        for (int i = 0; i < view.Columns.Count; i++)
                        {
                            LBTextBoxColumn column = (LBTextBoxColumn) view.Columns[i];
                            this.Items.SetCheckInfo(column.id, column.check_date, column.check_time);
                        }
                    }
                    if (control.HasChildren)
                    {
                        this.SetEvent(control);
                    }
                }
            }
        }

        private void toolStripTextBox_MouseMove(object sender, MouseEventArgs e)
        {
            if ((sender is TextBox) && (e.Button == MouseButtons.Right))
            {
                TextBox box = (TextBox) sender;
                if (((e.X < 0) || (e.Y < 0)) || ((e.X >= box.ClientSize.Width) || (e.Y >= box.ClientSize.Height)))
                {
                    box.Capture = false;
                }
            }
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bool flag = true;
                for (int i = 0; i < this.toolStripTextBox1.Text.Length; i++)
                {
                    char ch = this.toolStripTextBox1.Text[i];
                    if ((ch < '0') || (ch > '9'))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag && (this.toolStripTextBox1.Text != ""))
                {
                    int num2 = (int.Parse(this.toolStripTextBox1.Text) - 1) * (this.repBindingSources.Count + 1);
                    if (num2 < 0)
                    {
                        if (this.bsRepeat.Position != 0)
                        {
                            this.bindingNavigatorMoveFirstItem_Click(this, null);
                            return;
                        }
                    }
                    else if (num2 > (this.bsRepeat.Count - 1))
                    {
                        if (this.bsRepeat.Position != (this.bsRepeat.Count - 1))
                        {
                            this.bindingNavigatorMoveLastItem_Click(this, null);
                            return;
                        }
                    }
                    else
                    {
                        this.bsRepeat.Position = num2;
                    }
                }
                this.toolStripTextBox1.Text = ((this.bsRepeat.Position / (this.repBindingSources.Count + 1)) + 1).ToString();
                this.NavigatorItemChanged();
            }
        }

        private int ViewLastPage()
        {
            if (string.IsNullOrEmpty(this.bsRepeat.DataMember))
            {
                return -1;
            }
            int num = this.Items.ResetLastPage(this.bsRepeat.DataMember, this.repBindingSources.Count);
            int num2 = this.Items.DsItems.Tables[this.bsRepeat.DataMember].Rows.Count % (this.repBindingSources.Count + 1);
            int num3 = this.Items.DsItems.Tables[this.bsRepeat.DataMember].Rows.Count / (this.repBindingSources.Count + 1);
            if (num2 > 0)
            {
                num3++;
            }
            this.toolStripLabel1.Text = string.Format("/{0}", num3);
            return num;
        }

        public int BsCount
        {
            get
            {
                return this.repBindingSources.Count;
            }
        }

        public int DefaultHeight
        {
            get
            {
                return this.defaultHeight;
            }
        }

        private float FontZoom
        {
            get
            {
                return this.fontZoom;
            }
            set
            {
                if (!this.fontZoom.Equals(value))
                {
                    this.AutoScroll = false;
                    float fsize = value - this.fontZoom;
                    this.ResizeFont(this, fsize);
                    this.fontZoom = value;
                    this.Refresh();
                    this.AutoScroll = true;
                }
            }
        }

        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
        }

        public Naccs.Common.Generator.ItemInfo Items
        {
            get
            {
                return this.items;
            }
            set
            {
                this.items = value;
            }
        }

        public JobErrInfo JobErrs
        {
            get
            {
                return this.jobErrs;
            }
            set
            {
                this.jobErrs = value;
            }
        }

        public ToolStripLabel LblNextPage
        {
            get
            {
                return this.lblNextpage;
            }
            set
            {
                this.lblNextpage = value;
            }
        }

        public int RepeatCnt
        {
            get
            {
                return this.bsRepeat.Count;
            }
        }

        public int RepeatPos
        {
            get
            {
                return (this.bsRepeat.Position + 1);
            }
        }
    }
}

