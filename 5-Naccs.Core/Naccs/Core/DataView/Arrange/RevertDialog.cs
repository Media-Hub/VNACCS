namespace Naccs.Core.DataView.Arrange
{
    using Naccs.Core.Classes;
    using Naccs.Core.Properties;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;

    public class RevertDialog : Form
    {
        private ToolStripMenuItem AllCheckOff_MouseClick;
        private ToolStripMenuItem AllCheckOn_MouseClick;
        private Button btnClose;
        private Button btnExract;
        private Button btnFind;
        private Button btnRevert;
        private DataGridViewTextBoxColumn clAirSea;
        private DataGridViewTextBoxColumn clFolder;
        private DataGridViewTextBoxColumn clInputNo;
        private DataGridViewTextBoxColumn clJobCode;
        private DataGridViewTextBoxColumn clJobInfo;
        private DataGridViewTextBoxColumn clmFileName;
        private DataGridViewTextBoxColumn clOutCode;
        private DataGridViewCheckBoxColumn clSelect;
        private DataGridViewTextBoxColumn clTimeStamp;
        private ComboBox cmbFindKind;
        private ComboBox cmbMonth;
        private ComboBox cmbYear;
        private const string cnFindKindColumn = "FIND_KIND_CLM";
        private const string cnFindKindName = "FIND_KIND_NAME";
        private const string cnMonth = "MONTH";
        private const int cnSelect_ColumnIndex = 9;
        private const string cnStorageFolder = "PastDataView";
        private const string cnYear = "YEAR";
        private IContainer components;
        private ContextMenuStrip contextMenuStrip1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridView dgvSelectList;
        private string dlMonth = "";
        private string dlYear = "";
        private int err;
        private DataTable findKind_table;
        private GroupBox grpDate;
        private GroupBox grpFind;
        private Label label1;
        private Label lblFindKind;
        private Label lblFindString;
        private Label lblMonth;
        private Label lblYear;
        private ToolStripMenuItem mnAllCheckOff;
        private ToolStripMenuItem mnAllCheckOn;
        private ToolStripMenuItem mnEdit;
        private ToolStripSeparator mnEditSep1;
        private ToolStripMenuItem mnExit;
        private ToolStripMenuItem mnFile;
        private ToolStripSeparator mnFileSep1;
        private ToolStripMenuItem mnOpen;
        private MenuStrip mnRevertDialog;
        private ToolStripMenuItem mnSelectCheckOff;
        private ToolStripMenuItem mnSelectCheckOn;
        private string pastDataView_YYYYMM = "";
        private string pastDataViewPath = "";
        private string path = "";
        private Panel pnlBottom;
        private Panel pnlTop;
        private ToolStripMenuItem SelectCheckOff_MouseClick;
        private ToolStripMenuItem SelectCheckOn_MouseClick;
        private TextBox txbFindString;
        private DataTable year_month_table;

        public event Naccs.Core.DataView.Arrange.EventHandler<RevertEventArgs> RevertEvent;

        public RevertDialog()
        {
            this.InitializeComponent();
            this.pastDataViewPath = ArrangeDataTable.PastDataViewPath;
            this.cmbYear.Items.Clear();
            this.cmbMonth.Items.Clear();
            this.year_month_table = new DataTable();
            this.year_month_table.Columns.Add("YEAR");
            this.year_month_table.Columns.Add("MONTH");
            this.makeYearCombo();
            this.findKind_table = new DataTable();
            this.findKind_table.Columns.Add("FIND_KIND_NAME");
            this.findKind_table.Columns.Add("FIND_KIND_CLM");
            this.makeFindKindCombo();
        }

        private void AllCheckOff_MouseClick_Click(object sender, EventArgs e)
        {
            this.mnAllCheckOff_Click(sender, e);
        }

        private void AllCheckOn_MouseClick_Click(object sender, EventArgs e)
        {
            this.mnAllCheckOn_Click(sender, e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnExract_Click(object sender, EventArgs e)
        {
            this.cmbCheck();
            if (this.err != 1)
            {
                this.pastDataView_YYYYMM = this.cmbYear.Text + this.cmbMonth.Text;
                this.dlYear = this.cmbYear.Text;
                this.dlMonth = this.cmbMonth.Text;
                this.path = Path.GetDirectoryName(ArrangeDataTable.IndexFilePath(this.pastDataView_YYYYMM));
                if (Directory.Exists(this.path))
                {
                    try
                    {
                        this.btnExract.Enabled = false;
                        Cursor.Current = Cursors.WaitCursor;
                        try
                        {
                            ArrangeDataTable.Load((DataTable) this.dgvSelectList.DataSource, this.pastDataView_YYYYMM);
                            this.dgvSelectList.Sort(this.dgvSelectList.Columns[7], ListSortDirection.Descending);
                        }
                        catch (Exception exception)
                        {
                            MessageDialog dialog = new MessageDialog();
                            dialog.ShowMessage("E301", this.path, null, exception);
                            dialog.Dispose();
                        }
                        return;
                    }
                    finally
                    {
                        this.btnExract.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        if (this.dgvSelectList.RowCount > 0)
                        {
                            this.dgvSelectList.CurrentCell = this.dgvSelectList[1, 0];
                            this.mnAllCheckOff_Click(sender, e);
                        }
                        else
                        {
                            base.ActiveControl = this.cmbYear;
                        }
                    }
                }
                this.RevertDialog_Load(sender, e);
                base.ActiveControl = this.cmbYear;
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cmbFindKind.Text))
            {
                SerchFromGridView.serch(this.dgvSelectList, this.cmbFindKind.SelectedValue.ToString(), this.txbFindString.Text);
            }
        }

        private void btnRevert_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            foreach (DataGridViewRow row in (IEnumerable) this.dgvSelectList.Rows)
            {
                if (!string.IsNullOrEmpty(row.Cells["clSelect"].Value.ToString()) && ((bool) row.Cells["clSelect"].Value))
                {
                    list.Add(row.Cells["clFileName"].Value.ToString());
                }
            }
            if (list.Count > 0)
            {
                this.path = Path.GetDirectoryName(ArrangeDataTable.IndexFilePath(this.pastDataView_YYYYMM));
                this.DoRevertEvent(this.path, list.ToArray());
            }
        }

        private int cmbCheck()
        {
            int length = this.cmbYear.Text.Length;
            int num2 = this.cmbMonth.Text.Length;
            if ((length != 4) || !Regex.IsMatch(this.cmbYear.Text, "^[0-9]+$"))
            {
                string message = Resources.ResourceManager.GetString("CORE21");
                using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                {
                    form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, message);
                }
                base.ActiveControl = this.cmbYear;
                this.err = 1;
                return this.err;
            }
            if ((num2 != 2) || !Regex.IsMatch(this.cmbMonth.Text, "^[0-9]+$"))
            {
                string str4 = Resources.ResourceManager.GetString("CORE22");
                using (MessageDialogSimpleForm form2 = new MessageDialogSimpleForm())
                {
                    form2.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, str4);
                }
                base.ActiveControl = this.cmbMonth;
                this.err = 1;
            }
            return this.err;
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.makeMonthCombo();
        }

        private void cmbYear_TextUpdate(object sender, EventArgs e)
        {
            this.makeMonthCombo();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DoRevertEvent(string pathroot, string[] filenames)
        {
            if (this.RevertEvent != null)
            {
                RevertEventArgs e = new RevertEventArgs {
                    PathRoot = pathroot,
                    FileNames = filenames
                };
                if (this.RevertEvent(this, e))
                {
                    this.dgvSelectList.CurrentCell = null;
                    for (int i = this.dgvSelectList.RowCount - 1; i > -1; i--)
                    {
                        DataGridViewRow row = this.dgvSelectList.Rows[i];
                        if (Convert.ToBoolean(row.Cells["clSelect"].Value))
                        {
                            this.dgvSelectList.Rows.RemoveAt(i);
                        }
                    }
                    this.dgvSelectList.Refresh();
                }
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnFind = new System.Windows.Forms.Button();
            this.lblFindString = new System.Windows.Forms.Label();
            this.txbFindString = new System.Windows.Forms.TextBox();
            this.lblFindKind = new System.Windows.Forms.Label();
            this.cmbFindKind = new System.Windows.Forms.ComboBox();
            this.mnRevertDialog = new System.Windows.Forms.MenuStrip();
            this.mnFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnAllCheckOn = new System.Windows.Forms.ToolStripMenuItem();
            this.mnAllCheckOff = new System.Windows.Forms.ToolStripMenuItem();
            this.mnEditSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnSelectCheckOn = new System.Windows.Forms.ToolStripMenuItem();
            this.mnSelectCheckOff = new System.Windows.Forms.ToolStripMenuItem();
            this.grpFind = new System.Windows.Forms.GroupBox();
            this.dgvSelectList = new System.Windows.Forms.DataGridView();
            this.clSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clFolder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clAirSea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clJobCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clOutCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clInputNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clJobInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clTimeStamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRevert = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblYear = new System.Windows.Forms.Label();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.lblMonth = new System.Windows.Forms.Label();
            this.grpDate = new System.Windows.Forms.GroupBox();
            this.btnExract = new System.Windows.Forms.Button();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AllCheckOn_MouseClick = new System.Windows.Forms.ToolStripMenuItem();
            this.AllCheckOff_MouseClick = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectCheckOn_MouseClick = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectCheckOff_MouseClick = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mnRevertDialog.SuspendLayout();
            this.grpFind.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectList)).BeginInit();
            this.grpDate.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(486, 14);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(70, 23);
            this.btnFind.TabIndex = 4;
            this.btnFind.Text = "Tìm kiếm";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // lblFindString
            // 
            this.lblFindString.AutoSize = true;
            this.lblFindString.Location = new System.Drawing.Point(6, 15);
            this.lblFindString.Name = "lblFindString";
            this.lblFindString.Size = new System.Drawing.Size(76, 26);
            this.lblFindString.TabIndex = 0;
            this.lblFindString.Text = "Tìm kiếm theo \r\nchuỗi";
            // 
            // txbFindString
            // 
            this.txbFindString.Location = new System.Drawing.Point(86, 17);
            this.txbFindString.Name = "txbFindString";
            this.txbFindString.Size = new System.Drawing.Size(100, 20);
            this.txbFindString.TabIndex = 1;
            // 
            // lblFindKind
            // 
            this.lblFindKind.AutoSize = true;
            this.lblFindKind.Location = new System.Drawing.Point(192, 20);
            this.lblFindKind.Name = "lblFindKind";
            this.lblFindKind.Size = new System.Drawing.Size(92, 13);
            this.lblFindKind.TabIndex = 2;
            this.lblFindKind.Text = "Phân loại tìm kiếm";
            // 
            // cmbFindKind
            // 
            this.cmbFindKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFindKind.FormattingEnabled = true;
            this.cmbFindKind.Location = new System.Drawing.Point(298, 16);
            this.cmbFindKind.Name = "cmbFindKind";
            this.cmbFindKind.Size = new System.Drawing.Size(182, 21);
            this.cmbFindKind.TabIndex = 3;
            // 
            // mnRevertDialog
            // 
            this.mnRevertDialog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnFile,
            this.mnEdit});
            this.mnRevertDialog.Location = new System.Drawing.Point(0, 0);
            this.mnRevertDialog.Name = "mnRevertDialog";
            this.mnRevertDialog.Size = new System.Drawing.Size(851, 24);
            this.mnRevertDialog.TabIndex = 0;
            this.mnRevertDialog.Text = "menuStrip1";
            this.mnRevertDialog.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mnRevertDialog_ItemClicked);
            // 
            // mnFile
            // 
            this.mnFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnOpen,
            this.mnFileSep1,
            this.mnExit});
            this.mnFile.Name = "mnFile";
            this.mnFile.Size = new System.Drawing.Size(73, 20);
            this.mnFile.Text = "Tệp tin (&F)";
            // 
            // mnOpen
            // 
            this.mnOpen.Name = "mnOpen";
            this.mnOpen.Size = new System.Drawing.Size(176, 22);
            this.mnOpen.Text = "Xóa tin nhắn cũ (&D)";
            this.mnOpen.Click += new System.EventHandler(this.mnDelete_Click);
            // 
            // mnFileSep1
            // 
            this.mnFileSep1.Name = "mnFileSep1";
            this.mnFileSep1.Size = new System.Drawing.Size(173, 6);
            // 
            // mnExit
            // 
            this.mnExit.Name = "mnExit";
            this.mnExit.Size = new System.Drawing.Size(176, 22);
            this.mnExit.Text = "Đóng (&X)";
            this.mnExit.Click += new System.EventHandler(this.mnExit_Click);
            // 
            // mnEdit
            // 
            this.mnEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnAllCheckOn,
            this.mnAllCheckOff,
            this.mnEditSep1,
            this.mnSelectCheckOn,
            this.mnSelectCheckOff});
            this.mnEdit.Name = "mnEdit";
            this.mnEdit.Size = new System.Drawing.Size(55, 20);
            this.mnEdit.Text = "Sửa (&E)";
            // 
            // mnAllCheckOn
            // 
            this.mnAllCheckOn.Name = "mnAllCheckOn";
            this.mnAllCheckOn.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.mnAllCheckOn.Size = new System.Drawing.Size(290, 22);
            this.mnAllCheckOn.Text = "Chọn tất cả (&A)";
            this.mnAllCheckOn.Click += new System.EventHandler(this.mnAllCheckOn_Click);
            // 
            // mnAllCheckOff
            // 
            this.mnAllCheckOff.Name = "mnAllCheckOff";
            this.mnAllCheckOff.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.A)));
            this.mnAllCheckOff.Size = new System.Drawing.Size(290, 22);
            this.mnAllCheckOff.Text = "Bỏ chọn tất cả (&C)";
            this.mnAllCheckOff.Click += new System.EventHandler(this.mnAllCheckOff_Click);
            // 
            // mnEditSep1
            // 
            this.mnEditSep1.Name = "mnEditSep1";
            this.mnEditSep1.Size = new System.Drawing.Size(287, 6);
            // 
            // mnSelectCheckOn
            // 
            this.mnSelectCheckOn.Name = "mnSelectCheckOn";
            this.mnSelectCheckOn.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.mnSelectCheckOn.Size = new System.Drawing.Size(290, 22);
            this.mnSelectCheckOn.Text = "Chọn vùng lựa chọn (&R)";
            this.mnSelectCheckOn.Click += new System.EventHandler(this.mnSelectCheckOn_Click);
            // 
            // mnSelectCheckOff
            // 
            this.mnSelectCheckOff.Name = "mnSelectCheckOff";
            this.mnSelectCheckOff.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.R)));
            this.mnSelectCheckOff.Size = new System.Drawing.Size(290, 22);
            this.mnSelectCheckOff.Text = "Bỏ chọn vùng lựa chọn (&Q)";
            this.mnSelectCheckOff.Click += new System.EventHandler(this.mnSelectCheckOff_Click);
            // 
            // grpFind
            // 
            this.grpFind.Controls.Add(this.lblFindString);
            this.grpFind.Controls.Add(this.btnFind);
            this.grpFind.Controls.Add(this.cmbFindKind);
            this.grpFind.Controls.Add(this.txbFindString);
            this.grpFind.Controls.Add(this.lblFindKind);
            this.grpFind.Location = new System.Drawing.Point(270, 3);
            this.grpFind.Name = "grpFind";
            this.grpFind.Size = new System.Drawing.Size(563, 45);
            this.grpFind.TabIndex = 2;
            this.grpFind.TabStop = false;
            this.grpFind.Text = "Tìm kiếm";
            // 
            // dgvSelectList
            // 
            this.dgvSelectList.AllowUserToAddRows = false;
            this.dgvSelectList.AllowUserToDeleteRows = false;
            this.dgvSelectList.AllowUserToResizeRows = false;
            this.dgvSelectList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSelectList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSelectList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSelectList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clSelect,
            this.clFolder,
            this.clAirSea,
            this.clJobCode,
            this.clOutCode,
            this.clInputNo,
            this.clJobInfo,
            this.clTimeStamp,
            this.clmFileName});
            this.dgvSelectList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSelectList.Location = new System.Drawing.Point(0, 80);
            this.dgvSelectList.Name = "dgvSelectList";
            this.dgvSelectList.RowTemplate.Height = 21;
            this.dgvSelectList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSelectList.Size = new System.Drawing.Size(851, 280);
            this.dgvSelectList.TabIndex = 2;
            // 
            // clSelect
            // 
            this.clSelect.DataPropertyName = "clSelect";
            this.clSelect.HeaderText = "Chọn";
            this.clSelect.Name = "clSelect";
            this.clSelect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clSelect.Width = 45;
            // 
            // clFolder
            // 
            this.clFolder.DataPropertyName = "clFolder";
            this.clFolder.HeaderText = "Thư mục";
            this.clFolder.Name = "clFolder";
            this.clFolder.ReadOnly = true;
            this.clFolder.Width = 120;
            // 
            // clAirSea
            // 
            this.clAirSea.DataPropertyName = "clAirSea";
            this.clAirSea.HeaderText = "A/S";
            this.clAirSea.Name = "clAirSea";
            this.clAirSea.ReadOnly = true;
            this.clAirSea.Visible = false;
            this.clAirSea.Width = 40;
            // 
            // clJobCode
            // 
            this.clJobCode.DataPropertyName = "clJobCode";
            this.clJobCode.HeaderText = "Mã nghiệp vụ";
            this.clJobCode.Name = "clJobCode";
            this.clJobCode.ReadOnly = true;
            // 
            // clOutCode
            // 
            this.clOutCode.DataPropertyName = "clOutCode";
            this.clOutCode.HeaderText = "Mã thông điệp đầu ra";
            this.clOutCode.Name = "clOutCode";
            this.clOutCode.ReadOnly = true;
            // 
            // clInputNo
            // 
            this.clInputNo.DataPropertyName = "clInputNo";
            this.clInputNo.HeaderText = "Mã thông điệp đầu vào";
            this.clInputNo.Name = "clInputNo";
            this.clInputNo.ReadOnly = true;
            this.clInputNo.Width = 160;
            // 
            // clJobInfo
            // 
            this.clJobInfo.DataPropertyName = "clJobInfo";
            this.clJobInfo.HeaderText = "Tiêu đề";
            this.clJobInfo.Name = "clJobInfo";
            this.clJobInfo.ReadOnly = true;
            this.clJobInfo.Width = 120;
            // 
            // clTimeStamp
            // 
            this.clTimeStamp.DataPropertyName = "clTimeStamp";
            this.clTimeStamp.HeaderText = "Thời gian gửi thông điệp";
            this.clTimeStamp.Name = "clTimeStamp";
            this.clTimeStamp.ReadOnly = true;
            this.clTimeStamp.Width = 160;
            // 
            // clmFileName
            // 
            this.clmFileName.DataPropertyName = "clmFileName";
            this.clmFileName.HeaderText = "Báo cáo tên";
            this.clmFileName.Name = "clmFileName";
            this.clmFileName.ReadOnly = true;
            this.clmFileName.Visible = false;
            // 
            // btnRevert
            // 
            this.btnRevert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRevert.Location = new System.Drawing.Point(571, 9);
            this.btnRevert.Name = "btnRevert";
            this.btnRevert.Size = new System.Drawing.Size(205, 23);
            this.btnRevert.TabIndex = 0;
            this.btnRevert.Text = "Hiển thị danh sách gửi nhận tin nhắn";
            this.btnRevert.UseVisualStyleBackColor = true;
            this.btnRevert.Click += new System.EventHandler(this.btnRevert_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(782, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(51, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(151, 21);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(29, 13);
            this.lblYear.TabIndex = 1;
            this.lblYear.Text = "Năm";
            // 
            // cmbYear
            // 
            this.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Items.AddRange(new object[] {
            "2009"});
            this.cmbYear.Location = new System.Drawing.Point(95, 17);
            this.cmbYear.MaxLength = 4;
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(50, 21);
            this.cmbYear.TabIndex = 2;
            this.cmbYear.SelectedIndexChanged += new System.EventHandler(this.cmbYear_SelectedIndexChanged);
            this.cmbYear.TextUpdate += new System.EventHandler(this.cmbYear_TextUpdate);
            // 
            // cmbMonth
            // 
            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Items.AddRange(new object[] {
            "12"});
            this.cmbMonth.Location = new System.Drawing.Point(8, 17);
            this.cmbMonth.MaxLength = 2;
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(39, 21);
            this.cmbMonth.TabIndex = 0;
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Location = new System.Drawing.Point(53, 21);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(38, 13);
            this.lblMonth.TabIndex = 3;
            this.lblMonth.Text = "Tháng";
            // 
            // grpDate
            // 
            this.grpDate.Controls.Add(this.btnExract);
            this.grpDate.Controls.Add(this.lblYear);
            this.grpDate.Controls.Add(this.cmbMonth);
            this.grpDate.Controls.Add(this.cmbYear);
            this.grpDate.Controls.Add(this.lblMonth);
            this.grpDate.Location = new System.Drawing.Point(14, 3);
            this.grpDate.Name = "grpDate";
            this.grpDate.Size = new System.Drawing.Size(249, 45);
            this.grpDate.TabIndex = 1;
            this.grpDate.TabStop = false;
            this.grpDate.Text = "Thời gian";
            // 
            // btnExract
            // 
            this.btnExract.Location = new System.Drawing.Point(181, 14);
            this.btnExract.Name = "btnExract";
            this.btnExract.Size = new System.Drawing.Size(62, 23);
            this.btnExract.TabIndex = 4;
            this.btnExract.Text = "Hiển thị";
            this.btnExract.UseVisualStyleBackColor = true;
            this.btnExract.Click += new System.EventHandler(this.btnExract_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pnlTop.Controls.Add(this.grpDate);
            this.pnlTop.Controls.Add(this.grpFind);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 24);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(851, 56);
            this.pnlTop.TabIndex = 1;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.label1);
            this.pnlBottom.Controls.Add(this.btnClose);
            this.pnlBottom.Controls.Add(this.btnRevert);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 360);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(851, 41);
            this.pnlBottom.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Số trường hợp";
            this.label1.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AllCheckOn_MouseClick,
            this.AllCheckOff_MouseClick,
            this.SelectCheckOn_MouseClick,
            this.SelectCheckOff_MouseClick});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(230, 92);
            // 
            // AllCheckOn_MouseClick
            // 
            this.AllCheckOn_MouseClick.Name = "AllCheckOn_MouseClick";
            this.AllCheckOn_MouseClick.Size = new System.Drawing.Size(229, 22);
            this.AllCheckOn_MouseClick.Text = "Chọn tất cả (&A)";
            this.AllCheckOn_MouseClick.Click += new System.EventHandler(this.AllCheckOn_MouseClick_Click);
            // 
            // AllCheckOff_MouseClick
            // 
            this.AllCheckOff_MouseClick.Name = "AllCheckOff_MouseClick";
            this.AllCheckOff_MouseClick.Size = new System.Drawing.Size(229, 22);
            this.AllCheckOff_MouseClick.Text = "Bỏ chọn tất cả (&C)";
            this.AllCheckOff_MouseClick.Click += new System.EventHandler(this.AllCheckOff_MouseClick_Click);
            // 
            // SelectCheckOn_MouseClick
            // 
            this.SelectCheckOn_MouseClick.Name = "SelectCheckOn_MouseClick";
            this.SelectCheckOn_MouseClick.Size = new System.Drawing.Size(229, 22);
            this.SelectCheckOn_MouseClick.Text = "Chọn vùng lựa chọn (&R)";
            this.SelectCheckOn_MouseClick.Click += new System.EventHandler(this.SelectCheckOn_MouseClick_Click);
            // 
            // SelectCheckOff_MouseClick
            // 
            this.SelectCheckOff_MouseClick.Name = "SelectCheckOff_MouseClick";
            this.SelectCheckOff_MouseClick.Size = new System.Drawing.Size(229, 22);
            this.SelectCheckOff_MouseClick.Text = "Bỏ chọn vùng lựa chọn (&Q)";
            this.SelectCheckOff_MouseClick.Click += new System.EventHandler(this.SelectCheckOff_MouseClick_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "clmFolder";
            this.dataGridViewTextBoxColumn1.HeaderText = "フォルダ名";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 130;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "clmSystem";
            this.dataGridViewTextBoxColumn2.HeaderText = "A/S";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 40;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "clmJobcode";
            this.dataGridViewTextBoxColumn3.HeaderText = "業務コード";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 90;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "clmOutcode";
            this.dataGridViewTextBoxColumn4.HeaderText = "出力コード";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "clmInputInfo";
            this.dataGridViewTextBoxColumn5.HeaderText = "入力No";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "clmSubject";
            this.dataGridViewTextBoxColumn6.HeaderText = "業務固有情報";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 130;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "clmTimestamp";
            this.dataGridViewTextBoxColumn7.HeaderText = "送受信時刻";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 120;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "clmFileName";
            this.dataGridViewTextBoxColumn8.HeaderText = "ファイル名";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Visible = false;
            // 
            // RevertDialog
            // 
            this.ClientSize = new System.Drawing.Size(851, 401);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.ControlBox = false;
            this.Controls.Add(this.dgvSelectList);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.mnRevertDialog);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.mnRevertDialog;
            this.Name = "RevertDialog";
            this.Text = "Quản lý danh sách tin nhắn cũ";
            this.Load += new System.EventHandler(this.RevertDialog_Load);
            this.mnRevertDialog.ResumeLayout(false);
            this.mnRevertDialog.PerformLayout();
            this.grpFind.ResumeLayout(false);
            this.grpFind.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectList)).EndInit();
            this.grpDate.ResumeLayout(false);
            this.grpDate.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void makeFindKindCombo()
        {
            this.findKind_table.Rows.Add(new string[] { "Thư mục", "clFolder" });
            this.findKind_table.Rows.Add(new string[] { "M\x00e3 nghiệp vụ", "clJobCode" });
            this.findKind_table.Rows.Add(new string[] { "M\x00e3 th\x00f4ng điệp đầu ra", "clOutCode" });
            this.findKind_table.Rows.Add(new string[] { "M\x00e3 th\x00f4ng điệp đầu v\x00e0o", "clInputNo" });
            this.findKind_table.Rows.Add(new string[] { "Ti\x00eau đề", "clJobInfo" });
            DataView view = new DataView(this.findKind_table);
            this.cmbFindKind.DisplayMember = "FIND_KIND_NAME";
            this.cmbFindKind.ValueMember = "FIND_KIND_CLM";
            this.cmbFindKind.DataSource = view;
            this.cmbFindKind.SelectedValue = "";
        }

        private void makeMonthCombo()
        {
            DataView view = new DataView(this.year_month_table);
            this.cmbMonth.DisplayMember = "MONTH";
            this.cmbMonth.ValueMember = "MONTH";
            view.RowFilter = string.Format("YEAR = '{0}'", this.cmbYear.Text);
            this.cmbMonth.DataSource = view;
        }

        private void makeYearCombo()
        {
            try
            {
                string[] array = Directory.GetDirectories(this.pastDataViewPath, "*", SearchOption.AllDirectories);
                Array.Reverse(array);
                List<string> list = new List<string>();
                new List<string>();
                string str = "";
                string str2 = "";
                string str3 = "";
                foreach (string str4 in array)
                {
                    string str5 = str4.Replace(this.pastDataViewPath + @"\", "");
                    str = str5.Substring(0, 4);
                    str3 = str5.Substring(4, 2);
                    if (!str2.Equals(str))
                    {
                        list.Add(str);
                        str2 = str;
                    }
                    this.year_month_table.Rows.Add(new string[] { str, str3 });
                }
                this.cmbYear.DataSource = list.ToArray();
            }
            catch (Exception)
            {
            }
        }

        private void mnAllCheckOff_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in (IEnumerable) this.dgvSelectList.Rows)
            {
                row.Cells["clSelect"].Value = false;
            }
            this.dgvSelectList.EndEdit();
        }

        private void mnAllCheckOn_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in (IEnumerable) this.dgvSelectList.Rows)
            {
                row.Cells["clSelect"].Value = true;
            }
            this.dgvSelectList.EndEdit();
        }

        private void mnDelete_Click(object sender, EventArgs e)
        {
            if (((this.dlYear != "") && (this.dlMonth != "")) && ((this.dlYear == this.cmbYear.Text) && (this.dlMonth == this.cmbMonth.Text)))
            {
                this.pastDataView_YYYYMM = this.cmbYear.Text + this.cmbMonth.Text;
                string path = Path.Combine(this.pastDataViewPath, this.pastDataView_YYYYMM);
                if (Directory.Exists(path))
                {
                    MessageDialog dialog = new MessageDialog();
                    DialogResult result = dialog.ShowMessage("C405", this.pastDataView_YYYYMM, "");
                    dialog.Close();
                    if (result == DialogResult.Yes)
                    {
                        if (ArrangeDataTable.IsLock(this.pastDataView_YYYYMM))
                        {
                            string message = Resources.ResourceManager.GetString("CORE23");
                            using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                            {
                                form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, message);
                            }
                            Environment.Exit(0);
                        }
                        Directory.Delete(path, true);
                        this.RevertDialog_Load(sender, e);
                    }
                }
            }
        }

        private void mnExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void mnRevertDialog_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void mnSelectCheckOff_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dgvSelectList.SelectedRows)
            {
                row.Cells["clSelect"].Value = false;
            }
            this.dgvSelectList.EndEdit();
        }

        private void mnSelectCheckOn_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dgvSelectList.SelectedRows)
            {
                row.Cells["clSelect"].Value = true;
            }
            this.dgvSelectList.EndEdit();
        }

        private void RevertDialog_Load(object sender, EventArgs e)
        {
            DataTable table = ArrangeDataTable.CreateDataTable();
            DataColumn column = new DataColumn {
                DataType = System.Type.GetType("System.Boolean"),
                ColumnName = "clSelect"
            };
            table.Columns.Add(column);
            this.dgvSelectList.DataSource = table;
            this.dgvSelectList.Columns["clFileName"].Visible = false;
            this.dgvSelectList.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;
        }

        private void SelectCheckOff_MouseClick_Click(object sender, EventArgs e)
        {
            this.mnSelectCheckOff_Click(sender, e);
        }

        private void SelectCheckOn_MouseClick_Click(object sender, EventArgs e)
        {
            this.mnSelectCheckOn_Click(sender, e);
        }

        public Icon FormIcon
        {
            set
            {
                base.Icon = value;
            }
        }
    }
}

