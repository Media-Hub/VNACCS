namespace DevComponents.DotNetBar
{
    partial class frmMEClist
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn7 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn8 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gLocDuLieu = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.txtEPC = new DevComponents.DotNetBar.tTextBox();
            this.txtTK_SO = new DevComponents.DotNetBar.tTextBox();
            this.btnThoat = new DevComponents.DotNetBar.ButtonX();
            this.btnXemChiTiet = new DevComponents.DotNetBar.ButtonX();
            this.btnThemMoi = new DevComponents.DotNetBar.ButtonX();
            this.gList = new DevComponents.DotNetBar.tGrid();
            this.mnuChuotPhai = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.chuotPhai_XemChiTiet = new System.Windows.Forms.ToolStripMenuItem();
            this.chuotPhai_CopyDong = new System.Windows.Forms.ToolStripMenuItem();
            this.chuotPhai_XoaDong = new System.Windows.Forms.ToolStripMenuItem();
            this.chuotPhai_XoaHet = new System.Windows.Forms.ToolStripMenuItem();
            this.chuotPhai_XuatExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.lblPhimTat = new DevComponents.DotNetBar.LabelX();
            this.bwLoadData = new System.ComponentModel.BackgroundWorker();
            this.menuPhimTat = new System.Windows.Forms.MenuStrip();
            this.phimTat = new System.Windows.Forms.ToolStripMenuItem();
            this.phimTat_XemChiTiet = new System.Windows.Forms.ToolStripMenuItem();
            this.phimTat_CopyDong = new System.Windows.Forms.ToolStripMenuItem();
            this.phimTat_XoaDong = new System.Windows.Forms.ToolStripMenuItem();
            this.phimTat_XoaHet = new System.Windows.Forms.ToolStripMenuItem();
            this.phimTat_Excel = new System.Windows.Forms.ToolStripMenuItem();
            this.proLoad = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.btnInAn = new DevComponents.DotNetBar.ButtonX();
            this.CONTENT.SuspendLayout();
            this.pBOTTOM.SuspendLayout();
            this.gLocDuLieu.SuspendLayout();
            this.mnuChuotPhai.SuspendLayout();
            this.menuPhimTat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            this.SuspendLayout();
            // 
            // TITLE
            // 
            // 
            // 
            // 
            this.TITLE.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.TITLE.Text = "DANH SÁCH TỜ KHAI XUẤT KHẨU TRỊ GIÁ THẤP (MEC)";
            // 
            // CONTENT
            // 
            this.CONTENT.Controls.Add(this.proLoad);
            this.CONTENT.Controls.Add(this.gList);
            this.CONTENT.Controls.Add(this.lblPhimTat);
            this.CONTENT.Controls.Add(this.gLocDuLieu);
            this.CONTENT.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.CONTENT.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.CONTENT.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.CONTENT.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.CONTENT.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.CONTENT.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.CONTENT.Style.GradientAngle = 90;
            // 
            // pBOTTOM
            // 
            this.pBOTTOM.Controls.Add(this.btnInAn);
            this.pBOTTOM.Controls.Add(this.btnThemMoi);
            this.pBOTTOM.Controls.Add(this.btnXemChiTiet);
            this.pBOTTOM.Controls.Add(this.btnThoat);
            this.pBOTTOM.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pBOTTOM.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pBOTTOM.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pBOTTOM.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pBOTTOM.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pBOTTOM.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pBOTTOM.Style.GradientAngle = 90;
            // 
            // gLocDuLieu
            // 
            this.gLocDuLieu.CanvasColor = System.Drawing.SystemColors.Control;
            this.gLocDuLieu.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gLocDuLieu.Controls.Add(this.txtEPC);
            this.gLocDuLieu.Controls.Add(this.txtTK_SO);
            this.gLocDuLieu.Dock = System.Windows.Forms.DockStyle.Top;
            this.gLocDuLieu.Location = new System.Drawing.Point(2, 2);
            this.gLocDuLieu.Name = "gLocDuLieu";
            this.gLocDuLieu.Size = new System.Drawing.Size(780, 47);
            // 
            // 
            // 
            this.gLocDuLieu.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gLocDuLieu.Style.BackColorGradientAngle = 90;
            this.gLocDuLieu.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gLocDuLieu.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gLocDuLieu.Style.BorderBottomWidth = 1;
            this.gLocDuLieu.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gLocDuLieu.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gLocDuLieu.Style.BorderLeftWidth = 1;
            this.gLocDuLieu.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gLocDuLieu.Style.BorderRightWidth = 1;
            this.gLocDuLieu.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gLocDuLieu.Style.BorderTopWidth = 1;
            this.gLocDuLieu.Style.CornerDiameter = 4;
            this.gLocDuLieu.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gLocDuLieu.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gLocDuLieu.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gLocDuLieu.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gLocDuLieu.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gLocDuLieu.TabIndex = 0;
            this.gLocDuLieu.Text = "Lọc dữ liệu";
            // 
            // txtEPC
            // 
            this.txtEPC.BắtBuộc = false;
            // 
            // 
            // 
            this.txtEPC.Border.Class = "TextBoxBorder";
            this.txtEPC.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtEPC.Location = new System.Drawing.Point(123, 5);
            this.txtEPC.Name = "txtEPC";
            this.txtEPC.Size = new System.Drawing.Size(127, 20);
            this.txtEPC.TabIndex = 3;
            this.txtEPC.Tag = "EPC";
            this.txtEPC.WatermarkText = "Mã người xuất khẩu";
            this.txtEPC.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtTK_SO
            // 
            this.txtTK_SO.BắtBuộc = false;
            // 
            // 
            // 
            this.txtTK_SO.Border.Class = "TextBoxBorder";
            this.txtTK_SO.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTK_SO.Location = new System.Drawing.Point(6, 5);
            this.txtTK_SO.Name = "txtTK_SO";
            this.txtTK_SO.Size = new System.Drawing.Size(100, 20);
            this.txtTK_SO.TabIndex = 1;
            this.txtTK_SO.Tag = "TK_SO";
            this.txtTK_SO.WatermarkText = "Số tờ khai";
            this.txtTK_SO.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // btnThoat
            // 
            this.btnThoat.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThoat.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnThoat.Location = new System.Drawing.Point(705, 4);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(75, 23);
            this.btnThoat.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnThoat.TabIndex = 0;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnXemChiTiet
            // 
            this.btnXemChiTiet.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnXemChiTiet.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnXemChiTiet.Location = new System.Drawing.Point(12, 4);
            this.btnXemChiTiet.Name = "btnXemChiTiet";
            this.btnXemChiTiet.Size = new System.Drawing.Size(97, 23);
            this.btnXemChiTiet.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnXemChiTiet.TabIndex = 1;
            this.btnXemChiTiet.Text = "Xem chi tiết (F1)";
            this.btnXemChiTiet.Click += new System.EventHandler(this.btnXemChiTiet_Click);
            // 
            // btnThemMoi
            // 
            this.btnThemMoi.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnThemMoi.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnThemMoi.Location = new System.Drawing.Point(129, 4);
            this.btnThemMoi.Name = "btnThemMoi";
            this.btnThemMoi.Size = new System.Drawing.Size(87, 23);
            this.btnThemMoi.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnThemMoi.TabIndex = 2;
            this.btnThemMoi.Text = "Thêm mới";
            this.btnThemMoi.Click += new System.EventHandler(this.btnThemMoi_Click);
            // 
            // gList
            // 
            this.gList.ContextMenuStrip = this.mnuChuotPhai;
            this.gList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gList.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.gList.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.gList.Location = new System.Drawing.Point(2, 69);
            this.gList.Name = "gList";
            this.gList.PrimaryGrid.AutoGenerateColumns = false;
            gridColumn1.DataPropertyName = "TK_ID";
            gridColumn1.HeaderText = "TK_ID";
            gridColumn1.Name = "TK_ID";
            gridColumn1.Visible = false;
            gridColumn2.DataPropertyName = "ECN";
            gridColumn2.HeaderText = "Số tờ khai";
            gridColumn2.Name = "ECN";
            gridColumn3.HeaderText = "Nhánh";
            gridColumn3.Name = "TK_NHANH";
            gridColumn3.Width = 50;
            gridColumn4.DataPropertyName = "TK_NGAYDK";
            gridColumn4.HeaderText = "Ngày đăng ký";
            gridColumn4.Name = "TK_NGAYDK";
            gridColumn5.DataPropertyName = "CH";
            gridColumn5.HeaderText = "Hải quan";
            gridColumn5.Name = "CH";
            gridColumn6.DataPropertyName = "CHB";
            gridColumn6.HeaderText = "Bộ phận HQ";
            gridColumn6.Name = "CHB";
            gridColumn7.DataPropertyName = "EPC";
            gridColumn7.HeaderText = "Mã người XK";
            gridColumn7.Name = "EPC";
            gridColumn8.DataPropertyName = "EPN";
            gridColumn8.HeaderText = "Tên người XK";
            gridColumn8.Name = "EPN";
            this.gList.PrimaryGrid.Columns.Add(gridColumn1);
            this.gList.PrimaryGrid.Columns.Add(gridColumn2);
            this.gList.PrimaryGrid.Columns.Add(gridColumn3);
            this.gList.PrimaryGrid.Columns.Add(gridColumn4);
            this.gList.PrimaryGrid.Columns.Add(gridColumn5);
            this.gList.PrimaryGrid.Columns.Add(gridColumn6);
            this.gList.PrimaryGrid.Columns.Add(gridColumn7);
            this.gList.PrimaryGrid.Columns.Add(gridColumn8);
            this.gList.PrimaryGrid.MultiSelect = false;
            this.gList.PrimaryGrid.ReadOnly = true;
            this.gList.PrimaryGrid.RowHeaderIndexOffset = 1;
            this.gList.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.RowWithCellHighlight;
            this.gList.PrimaryGrid.ShowRowGridIndex = true;
            this.gList.PrimaryGrid.UseAlternateRowStyle = true;
            this.gList.Size = new System.Drawing.Size(780, 432);
            this.gList.TabIndex = 1;
            this.gList.Text = "Danh sách";
            this.gList.CellDoubleClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellDoubleClickEventArgs>(this.gList_CellDoubleClick);
            this.gList.RowHeaderClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridRowHeaderClickEventArgs>(this.gList_RowHeaderClick);
            // 
            // mnuChuotPhai
            // 
            this.mnuChuotPhai.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chuotPhai_XemChiTiet,
            this.chuotPhai_CopyDong,
            this.chuotPhai_XoaDong,
            this.chuotPhai_XoaHet,
            this.chuotPhai_XuatExcel});
            this.mnuChuotPhai.Name = "mnuChuotPhai";
            this.mnuChuotPhai.Size = new System.Drawing.Size(155, 114);
            // 
            // chuotPhai_XemChiTiet
            // 
            this.chuotPhai_XemChiTiet.Name = "chuotPhai_XemChiTiet";
            this.chuotPhai_XemChiTiet.Size = new System.Drawing.Size(154, 22);
            this.chuotPhai_XemChiTiet.Text = "Xem chi tiết (F1)";
            this.chuotPhai_XemChiTiet.Click += new System.EventHandler(this.chuotPhai_XemChiTiet_Click);
            // 
            // chuotPhai_CopyDong
            // 
            this.chuotPhai_CopyDong.Name = "chuotPhai_CopyDong";
            this.chuotPhai_CopyDong.Size = new System.Drawing.Size(154, 22);
            this.chuotPhai_CopyDong.Text = "Copy dòng (F5)";
            this.chuotPhai_CopyDong.Click += new System.EventHandler(this.chuotPhai_CopyDong_Click);
            // 
            // chuotPhai_XoaDong
            // 
            this.chuotPhai_XoaDong.Name = "chuotPhai_XoaDong";
            this.chuotPhai_XoaDong.Size = new System.Drawing.Size(154, 22);
            this.chuotPhai_XoaDong.Text = "Xóa dòng (F8)";
            this.chuotPhai_XoaDong.Click += new System.EventHandler(this.chuotPhai_XoaDong_Click);
            // 
            // chuotPhai_XoaHet
            // 
            this.chuotPhai_XoaHet.Name = "chuotPhai_XoaHet";
            this.chuotPhai_XoaHet.Size = new System.Drawing.Size(154, 22);
            this.chuotPhai_XoaHet.Text = "Xóa hết (F11)";
            this.chuotPhai_XoaHet.Click += new System.EventHandler(this.chuotPhai_XoaHet_Click);
            // 
            // chuotPhai_XuatExcel
            // 
            this.chuotPhai_XuatExcel.Name = "chuotPhai_XuatExcel";
            this.chuotPhai_XuatExcel.Size = new System.Drawing.Size(154, 22);
            this.chuotPhai_XuatExcel.Text = "Xuất Excel (F12)";
            this.chuotPhai_XuatExcel.Click += new System.EventHandler(this.chuotPhai_XuatExcel_Click);
            // 
            // lblPhimTat
            // 
            // 
            // 
            // 
            this.lblPhimTat.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPhimTat.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPhimTat.Location = new System.Drawing.Point(2, 49);
            this.lblPhimTat.Name = "lblPhimTat";
            this.lblPhimTat.PaddingLeft = 5;
            this.lblPhimTat.Size = new System.Drawing.Size(780, 20);
            this.lblPhimTat.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.lblPhimTat.TabIndex = 6;
            this.lblPhimTat.Text = "F1: Xem chi tiết          F5: Copy dòng         F8: Xóa dòng           F11: Xóa h" +
    "ết            F12: Excel";
            // 
            // bwLoadData
            // 
            this.bwLoadData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadData_DoWork);
            this.bwLoadData.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadData_RunWorkerCompleted);
            // 
            // menuPhimTat
            // 
            this.menuPhimTat.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.phimTat});
            this.menuPhimTat.Location = new System.Drawing.Point(0, 0);
            this.menuPhimTat.Name = "menuPhimTat";
            this.menuPhimTat.Size = new System.Drawing.Size(784, 24);
            this.menuPhimTat.TabIndex = 4;
            this.menuPhimTat.Text = "menuStrip1";
            this.menuPhimTat.Visible = false;
            // 
            // phimTat
            // 
            this.phimTat.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.phimTat_XemChiTiet,
            this.phimTat_CopyDong,
            this.phimTat_XoaDong,
            this.phimTat_XoaHet,
            this.phimTat_Excel});
            this.phimTat.Name = "phimTat";
            this.phimTat.Size = new System.Drawing.Size(59, 20);
            this.phimTat.Text = "PhimTat";
            // 
            // phimTat_XemChiTiet
            // 
            this.phimTat_XemChiTiet.Name = "phimTat_XemChiTiet";
            this.phimTat_XemChiTiet.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.phimTat_XemChiTiet.Size = new System.Drawing.Size(149, 22);
            this.phimTat_XemChiTiet.Text = "XemChiTiet";
            this.phimTat_XemChiTiet.Click += new System.EventHandler(this.phimTat_XemChiTiet_Click);
            // 
            // phimTat_CopyDong
            // 
            this.phimTat_CopyDong.Name = "phimTat_CopyDong";
            this.phimTat_CopyDong.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.phimTat_CopyDong.Size = new System.Drawing.Size(149, 22);
            this.phimTat_CopyDong.Text = "CopyDong";
            this.phimTat_CopyDong.Click += new System.EventHandler(this.phimTat_CopyDong_Click);
            // 
            // phimTat_XoaDong
            // 
            this.phimTat_XoaDong.Name = "phimTat_XoaDong";
            this.phimTat_XoaDong.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.phimTat_XoaDong.Size = new System.Drawing.Size(149, 22);
            this.phimTat_XoaDong.Text = "XoaDong";
            this.phimTat_XoaDong.Click += new System.EventHandler(this.phimTat_XoaDong_Click);
            // 
            // phimTat_XoaHet
            // 
            this.phimTat_XoaHet.Name = "phimTat_XoaHet";
            this.phimTat_XoaHet.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.phimTat_XoaHet.Size = new System.Drawing.Size(149, 22);
            this.phimTat_XoaHet.Text = "XoaHet";
            this.phimTat_XoaHet.Click += new System.EventHandler(this.phimTat_XoaHet_Click);
            // 
            // phimTat_Excel
            // 
            this.phimTat_Excel.Name = "phimTat_Excel";
            this.phimTat_Excel.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.phimTat_Excel.Size = new System.Drawing.Size(149, 22);
            this.phimTat_Excel.Text = "Excel";
            this.phimTat_Excel.Click += new System.EventHandler(this.phimTat_Excel_Click);
            // 
            // proLoad
            // 
            // 
            // 
            // 
            this.proLoad.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.proLoad.Location = new System.Drawing.Point(37, 157);
            this.proLoad.Name = "proLoad";
            this.proLoad.ProgressType = DevComponents.DotNetBar.eProgressItemType.Marquee;
            this.proLoad.Size = new System.Drawing.Size(231, 23);
            this.proLoad.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.proLoad.TabIndex = 7;
            this.proLoad.Text = "Đang xử lý...";
            this.proLoad.TextVisible = true;
            // 
            // btnInAn
            // 
            this.btnInAn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnInAn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnInAn.Location = new System.Drawing.Point(236, 5);
            this.btnInAn.Name = "btnInAn";
            this.btnInAn.Size = new System.Drawing.Size(87, 23);
            this.btnInAn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnInAn.TabIndex = 3;
            this.btnInAn.Text = "In ấn";
            this.btnInAn.Click += new System.EventHandler(this.btnInAn_Click);
            // 
            // frmMEClist
            // 
            this.ClientSize = new System.Drawing.Size(784, 565);
            this.Controls.Add(this.menuPhimTat);
            this.MainMenuStrip = this.menuPhimTat;
            this.Name = "frmMEClist";
            this.Text = "Danh sách TKX trị giá thấp (MEC)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMEClist_Load);
            this.Controls.SetChildIndex(this.menuPhimTat, 0);
            this.Controls.SetChildIndex(this.TITLE, 0);
            this.Controls.SetChildIndex(this.pBOTTOM, 0);
            this.Controls.SetChildIndex(this.CONTENT, 0);
            this.CONTENT.ResumeLayout(false);
            this.pBOTTOM.ResumeLayout(false);
            this.gLocDuLieu.ResumeLayout(false);
            this.mnuChuotPhai.ResumeLayout(false);
            this.menuPhimTat.ResumeLayout(false);
            this.menuPhimTat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.GroupPanel gLocDuLieu;
        private tGrid gList;
        private ButtonX btnThemMoi;
        private ButtonX btnXemChiTiet;
        private ButtonX btnThoat;
        private LabelX lblPhimTat;
        private System.ComponentModel.BackgroundWorker bwLoadData;
        private System.Windows.Forms.MenuStrip menuPhimTat;
        private System.Windows.Forms.ToolStripMenuItem phimTat;
        private System.Windows.Forms.ToolStripMenuItem phimTat_XemChiTiet;
        private Controls.ProgressBarX proLoad;
        private tTextBox txtEPC;
        private tTextBox txtTK_SO;
        private System.Windows.Forms.BindingSource bsList;
        private System.Windows.Forms.ContextMenuStrip mnuChuotPhai;
        private System.Windows.Forms.ToolStripMenuItem chuotPhai_XemChiTiet;
        private System.Windows.Forms.ToolStripMenuItem chuotPhai_CopyDong;
        private System.Windows.Forms.ToolStripMenuItem chuotPhai_XoaDong;
        private System.Windows.Forms.ToolStripMenuItem chuotPhai_XoaHet;
        private System.Windows.Forms.ToolStripMenuItem chuotPhai_XuatExcel;
        private System.Windows.Forms.ToolStripMenuItem phimTat_CopyDong;
        private System.Windows.Forms.ToolStripMenuItem phimTat_XoaDong;
        private System.Windows.Forms.ToolStripMenuItem phimTat_XoaHet;
        private System.Windows.Forms.ToolStripMenuItem phimTat_Excel;
        private ButtonX btnInAn;
    }
}
