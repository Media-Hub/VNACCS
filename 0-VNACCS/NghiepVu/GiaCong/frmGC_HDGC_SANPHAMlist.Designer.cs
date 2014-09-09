	namespace DevComponents.DotNetBar
	{																
	    partial class frmGC_HDGC_SANPHAMlist
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
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn9 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn10 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn11 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn12 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn13 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn14 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gLocDuLieu = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.txtSP_MA = new DevComponents.DotNetBar.tTextBox();
            this.txtSP_TEN = new DevComponents.DotNetBar.tTextBox();
            this.txtSP_DVT = new DevComponents.DotNetBar.tTextBox();
            this.txtSP_HS = new DevComponents.DotNetBar.tTextBox();
            this.txtSP_LOAISP_MA = new DevComponents.DotNetBar.tTextBox();
            this.txtSP_DONGIA = new DevComponents.DotNetBar.tTextBox();
            this.txtSP_MABIEUTHUE_XNK = new DevComponents.DotNetBar.tTextBox();
            this.txtSP_MABIEUTHUE_TBDB = new DevComponents.DotNetBar.tTextBox();
            this.txtSP_MABIEUTHUE_MT = new DevComponents.DotNetBar.tTextBox();
            this.txtSP_MABIEUTHUE_VAT = new DevComponents.DotNetBar.tTextBox();
            this.txtSP_GHICHU = new DevComponents.DotNetBar.tTextBox();
            this.txtSP_TEN_EN = new DevComponents.DotNetBar.tTextBox();
            this.btnThoat = new DevComponents.DotNetBar.ButtonX();
            this.btnGhiDuLieu = new DevComponents.DotNetBar.ButtonX();
            this.gList = new DevComponents.DotNetBar.tGrid();
            this.mnuChuotPhai = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.chuotPhai_GhiDuLieu = new System.Windows.Forms.ToolStripMenuItem();
            this.chuotPhai_CopyDong = new System.Windows.Forms.ToolStripMenuItem();
            this.chuotPhai_XoaDong = new System.Windows.Forms.ToolStripMenuItem();
            this.chuotPhai_XoaHet = new System.Windows.Forms.ToolStripMenuItem();
            this.chuotPhai_XuatExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.lblPhimTat = new DevComponents.DotNetBar.LabelX();
            this.bwLoadData = new System.ComponentModel.BackgroundWorker();
            this.menuPhimTat = new System.Windows.Forms.MenuStrip();
            this.phimTat = new System.Windows.Forms.ToolStripMenuItem();
            this.phimTat_GhiDuLieu = new System.Windows.Forms.ToolStripMenuItem();
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
            this.TITLE.Text = "DANH SÁCH GC_HDGC_SANPHAM";
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
            this.pBOTTOM.Controls.Add(this.btnGhiDuLieu);
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
            this.gLocDuLieu.Controls.Add(this.txtSP_MA);
            this.gLocDuLieu.Controls.Add(this.txtSP_TEN);
            this.gLocDuLieu.Controls.Add(this.txtSP_DVT);
            this.gLocDuLieu.Controls.Add(this.txtSP_HS);
            this.gLocDuLieu.Controls.Add(this.txtSP_LOAISP_MA);
            this.gLocDuLieu.Controls.Add(this.txtSP_DONGIA);
            this.gLocDuLieu.Controls.Add(this.txtSP_MABIEUTHUE_XNK);
            this.gLocDuLieu.Controls.Add(this.txtSP_MABIEUTHUE_TBDB);
            this.gLocDuLieu.Controls.Add(this.txtSP_MABIEUTHUE_MT);
            this.gLocDuLieu.Controls.Add(this.txtSP_MABIEUTHUE_VAT);
            this.gLocDuLieu.Controls.Add(this.txtSP_GHICHU);
            this.gLocDuLieu.Controls.Add(this.txtSP_TEN_EN);
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
            // txtSP_MA
            // 
            this.txtSP_MA.BắtBuộc = false;
            // 
            // 
            // 
            this.txtSP_MA.Border.Class = "TextBoxBorder";
            this.txtSP_MA.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSP_MA.Location = new System.Drawing.Point(6, 5);
            this.txtSP_MA.Name = "txtSP_MA";
            this.txtSP_MA.Size = new System.Drawing.Size(100, 20);
            this.txtSP_MA.TabIndex = 1;
            this.txtSP_MA.Tag = "SP_MA";
            this.txtSP_MA.WatermarkText = "SP_MA";
            this.txtSP_MA.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtSP_TEN
            // 
            this.txtSP_TEN.BắtBuộc = false;
            // 
            // 
            // 
            this.txtSP_TEN.Border.Class = "TextBoxBorder";
            this.txtSP_TEN.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSP_TEN.Location = new System.Drawing.Point(116, 5);
            this.txtSP_TEN.Name = "txtSP_TEN";
            this.txtSP_TEN.Size = new System.Drawing.Size(100, 20);
            this.txtSP_TEN.TabIndex = 2;
            this.txtSP_TEN.Tag = "SP_TEN";
            this.txtSP_TEN.WatermarkText = "SP_TEN";
            this.txtSP_TEN.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtSP_DVT
            // 
            this.txtSP_DVT.BắtBuộc = false;
            // 
            // 
            // 
            this.txtSP_DVT.Border.Class = "TextBoxBorder";
            this.txtSP_DVT.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSP_DVT.Location = new System.Drawing.Point(226, 5);
            this.txtSP_DVT.Name = "txtSP_DVT";
            this.txtSP_DVT.Size = new System.Drawing.Size(100, 20);
            this.txtSP_DVT.TabIndex = 3;
            this.txtSP_DVT.Tag = "SP_DVT";
            this.txtSP_DVT.WatermarkText = "SP_DVT";
            this.txtSP_DVT.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtSP_HS
            // 
            this.txtSP_HS.BắtBuộc = false;
            // 
            // 
            // 
            this.txtSP_HS.Border.Class = "TextBoxBorder";
            this.txtSP_HS.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSP_HS.Location = new System.Drawing.Point(336, 5);
            this.txtSP_HS.Name = "txtSP_HS";
            this.txtSP_HS.Size = new System.Drawing.Size(100, 20);
            this.txtSP_HS.TabIndex = 4;
            this.txtSP_HS.Tag = "SP_HS";
            this.txtSP_HS.WatermarkText = "SP_HS";
            this.txtSP_HS.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtSP_LOAISP_MA
            // 
            this.txtSP_LOAISP_MA.BắtBuộc = false;
            // 
            // 
            // 
            this.txtSP_LOAISP_MA.Border.Class = "TextBoxBorder";
            this.txtSP_LOAISP_MA.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSP_LOAISP_MA.Location = new System.Drawing.Point(446, 5);
            this.txtSP_LOAISP_MA.Name = "txtSP_LOAISP_MA";
            this.txtSP_LOAISP_MA.Size = new System.Drawing.Size(100, 20);
            this.txtSP_LOAISP_MA.TabIndex = 5;
            this.txtSP_LOAISP_MA.Tag = "SP_LOAISP_MA";
            this.txtSP_LOAISP_MA.WatermarkText = "SP_LOAISP_MA";
            this.txtSP_LOAISP_MA.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtSP_DONGIA
            // 
            this.txtSP_DONGIA.BắtBuộc = false;
            // 
            // 
            // 
            this.txtSP_DONGIA.Border.Class = "TextBoxBorder";
            this.txtSP_DONGIA.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSP_DONGIA.Location = new System.Drawing.Point(556, 5);
            this.txtSP_DONGIA.Name = "txtSP_DONGIA";
            this.txtSP_DONGIA.Size = new System.Drawing.Size(100, 20);
            this.txtSP_DONGIA.TabIndex = 6;
            this.txtSP_DONGIA.Tag = "SP_DONGIA";
            this.txtSP_DONGIA.WatermarkText = "SP_DONGIA";
            this.txtSP_DONGIA.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtSP_MABIEUTHUE_XNK
            // 
            this.txtSP_MABIEUTHUE_XNK.BắtBuộc = false;
            // 
            // 
            // 
            this.txtSP_MABIEUTHUE_XNK.Border.Class = "TextBoxBorder";
            this.txtSP_MABIEUTHUE_XNK.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSP_MABIEUTHUE_XNK.Location = new System.Drawing.Point(666, 5);
            this.txtSP_MABIEUTHUE_XNK.Name = "txtSP_MABIEUTHUE_XNK";
            this.txtSP_MABIEUTHUE_XNK.Size = new System.Drawing.Size(100, 20);
            this.txtSP_MABIEUTHUE_XNK.TabIndex = 7;
            this.txtSP_MABIEUTHUE_XNK.Tag = "SP_MABIEUTHUE_XNK";
            this.txtSP_MABIEUTHUE_XNK.WatermarkText = "SP_MABIEUTHUE_XNK";
            this.txtSP_MABIEUTHUE_XNK.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtSP_MABIEUTHUE_TBDB
            // 
            this.txtSP_MABIEUTHUE_TBDB.BắtBuộc = false;
            // 
            // 
            // 
            this.txtSP_MABIEUTHUE_TBDB.Border.Class = "TextBoxBorder";
            this.txtSP_MABIEUTHUE_TBDB.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSP_MABIEUTHUE_TBDB.Location = new System.Drawing.Point(776, 5);
            this.txtSP_MABIEUTHUE_TBDB.Name = "txtSP_MABIEUTHUE_TBDB";
            this.txtSP_MABIEUTHUE_TBDB.Size = new System.Drawing.Size(100, 20);
            this.txtSP_MABIEUTHUE_TBDB.TabIndex = 8;
            this.txtSP_MABIEUTHUE_TBDB.Tag = "SP_MABIEUTHUE_TBDB";
            this.txtSP_MABIEUTHUE_TBDB.WatermarkText = "SP_MABIEUTHUE_TBDB";
            this.txtSP_MABIEUTHUE_TBDB.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtSP_MABIEUTHUE_MT
            // 
            this.txtSP_MABIEUTHUE_MT.BắtBuộc = false;
            // 
            // 
            // 
            this.txtSP_MABIEUTHUE_MT.Border.Class = "TextBoxBorder";
            this.txtSP_MABIEUTHUE_MT.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSP_MABIEUTHUE_MT.Location = new System.Drawing.Point(886, 5);
            this.txtSP_MABIEUTHUE_MT.Name = "txtSP_MABIEUTHUE_MT";
            this.txtSP_MABIEUTHUE_MT.Size = new System.Drawing.Size(100, 20);
            this.txtSP_MABIEUTHUE_MT.TabIndex = 9;
            this.txtSP_MABIEUTHUE_MT.Tag = "SP_MABIEUTHUE_MT";
            this.txtSP_MABIEUTHUE_MT.WatermarkText = "SP_MABIEUTHUE_MT";
            this.txtSP_MABIEUTHUE_MT.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtSP_MABIEUTHUE_VAT
            // 
            this.txtSP_MABIEUTHUE_VAT.BắtBuộc = false;
            // 
            // 
            // 
            this.txtSP_MABIEUTHUE_VAT.Border.Class = "TextBoxBorder";
            this.txtSP_MABIEUTHUE_VAT.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSP_MABIEUTHUE_VAT.Location = new System.Drawing.Point(996, 5);
            this.txtSP_MABIEUTHUE_VAT.Name = "txtSP_MABIEUTHUE_VAT";
            this.txtSP_MABIEUTHUE_VAT.Size = new System.Drawing.Size(100, 20);
            this.txtSP_MABIEUTHUE_VAT.TabIndex = 10;
            this.txtSP_MABIEUTHUE_VAT.Tag = "SP_MABIEUTHUE_VAT";
            this.txtSP_MABIEUTHUE_VAT.WatermarkText = "SP_MABIEUTHUE_VAT";
            this.txtSP_MABIEUTHUE_VAT.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtSP_GHICHU
            // 
            this.txtSP_GHICHU.BắtBuộc = false;
            // 
            // 
            // 
            this.txtSP_GHICHU.Border.Class = "TextBoxBorder";
            this.txtSP_GHICHU.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSP_GHICHU.Location = new System.Drawing.Point(1106, 5);
            this.txtSP_GHICHU.Name = "txtSP_GHICHU";
            this.txtSP_GHICHU.Size = new System.Drawing.Size(100, 20);
            this.txtSP_GHICHU.TabIndex = 11;
            this.txtSP_GHICHU.Tag = "SP_GHICHU";
            this.txtSP_GHICHU.WatermarkText = "SP_GHICHU";
            this.txtSP_GHICHU.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtSP_TEN_EN
            // 
            this.txtSP_TEN_EN.BắtBuộc = false;
            // 
            // 
            // 
            this.txtSP_TEN_EN.Border.Class = "TextBoxBorder";
            this.txtSP_TEN_EN.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSP_TEN_EN.Location = new System.Drawing.Point(1216, 5);
            this.txtSP_TEN_EN.Name = "txtSP_TEN_EN";
            this.txtSP_TEN_EN.Size = new System.Drawing.Size(100, 20);
            this.txtSP_TEN_EN.TabIndex = 12;
            this.txtSP_TEN_EN.Tag = "SP_TEN_EN";
            this.txtSP_TEN_EN.WatermarkText = "SP_TEN_EN";
            this.txtSP_TEN_EN.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
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
            // btnGhiDuLieu
            // 
            this.btnGhiDuLieu.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnGhiDuLieu.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnGhiDuLieu.Location = new System.Drawing.Point(12, 4);
            this.btnGhiDuLieu.Name = "btnGhiDuLieu";
            this.btnGhiDuLieu.Size = new System.Drawing.Size(97, 23);
            this.btnGhiDuLieu.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnGhiDuLieu.TabIndex = 1;
            this.btnGhiDuLieu.Text = "Ghi dữ liệu (Ctrl+S)";
            this.btnGhiDuLieu.Click += new System.EventHandler(this.btnGhiDuLieu_Click);
            // 
            // gList
            // 
            this.gList.ContextMenuStrip = this.mnuChuotPhai;
            this.gList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gList.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.gList.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.gList.Location = new System.Drawing.Point(2, 69);
            this.gList.Name = "gList";
            this.gList.PrimaryGrid.AllowRowDelete = true;
            this.gList.PrimaryGrid.AllowRowInsert = true;
            this.gList.PrimaryGrid.AutoGenerateColumns = false;
            gridColumn1.DataPropertyName = "SP_HDID";
            gridColumn1.HeaderText = "SP_HDID";
            gridColumn1.Name = "SP_HDID";
            gridColumn1.Visible = false;
            gridColumn2.DataPropertyName = "SP_ID";
            gridColumn2.HeaderText = "SP_ID";
            gridColumn2.Name = "SP_ID";
            gridColumn2.Visible = false;
            gridColumn3.DataPropertyName = "SP_MA";
            gridColumn3.HeaderText = "SP_MA";
            gridColumn3.Name = "SP_MA";
            gridColumn4.DataPropertyName = "SP_TEN";
            gridColumn4.HeaderText = "SP_TEN";
            gridColumn4.Name = "SP_TEN";
            gridColumn5.DataPropertyName = "SP_DVT";
            gridColumn5.HeaderText = "SP_DVT";
            gridColumn5.Name = "SP_DVT";
            gridColumn6.DataPropertyName = "SP_HS";
            gridColumn6.HeaderText = "SP_HS";
            gridColumn6.Name = "SP_HS";
            gridColumn7.DataPropertyName = "SP_LOAISP_MA";
            gridColumn7.HeaderText = "SP_LOAISP_MA";
            gridColumn7.Name = "SP_LOAISP_MA";
            gridColumn8.DataPropertyName = "SP_DONGIA";
            gridColumn8.HeaderText = "SP_DONGIA";
            gridColumn8.Name = "SP_DONGIA";
            gridColumn9.DataPropertyName = "SP_MABIEUTHUE_XNK";
            gridColumn9.HeaderText = "SP_MABIEUTHUE_XNK";
            gridColumn9.Name = "SP_MABIEUTHUE_XNK";
            gridColumn10.DataPropertyName = "SP_MABIEUTHUE_TBDB";
            gridColumn10.HeaderText = "SP_MABIEUTHUE_TBDB";
            gridColumn10.Name = "SP_MABIEUTHUE_TBDB";
            gridColumn11.DataPropertyName = "SP_MABIEUTHUE_MT";
            gridColumn11.HeaderText = "SP_MABIEUTHUE_MT";
            gridColumn11.Name = "SP_MABIEUTHUE_MT";
            gridColumn12.DataPropertyName = "SP_MABIEUTHUE_VAT";
            gridColumn12.HeaderText = "SP_MABIEUTHUE_VAT";
            gridColumn12.Name = "SP_MABIEUTHUE_VAT";
            gridColumn13.DataPropertyName = "SP_GHICHU";
            gridColumn13.HeaderText = "SP_GHICHU";
            gridColumn13.Name = "SP_GHICHU";
            gridColumn14.DataPropertyName = "SP_TEN_EN";
            gridColumn14.HeaderText = "SP_TEN_EN";
            gridColumn14.Name = "SP_TEN_EN";
            this.gList.PrimaryGrid.Columns.Add(gridColumn1);
            this.gList.PrimaryGrid.Columns.Add(gridColumn2);
            this.gList.PrimaryGrid.Columns.Add(gridColumn3);
            this.gList.PrimaryGrid.Columns.Add(gridColumn4);
            this.gList.PrimaryGrid.Columns.Add(gridColumn5);
            this.gList.PrimaryGrid.Columns.Add(gridColumn6);
            this.gList.PrimaryGrid.Columns.Add(gridColumn7);
            this.gList.PrimaryGrid.Columns.Add(gridColumn8);
            this.gList.PrimaryGrid.Columns.Add(gridColumn9);
            this.gList.PrimaryGrid.Columns.Add(gridColumn10);
            this.gList.PrimaryGrid.Columns.Add(gridColumn11);
            this.gList.PrimaryGrid.Columns.Add(gridColumn12);
            this.gList.PrimaryGrid.Columns.Add(gridColumn13);
            this.gList.PrimaryGrid.Columns.Add(gridColumn14);
            this.gList.PrimaryGrid.KeyboardEditMode = DevComponents.DotNetBar.SuperGrid.KeyboardEditMode.EditOnEntry;
            this.gList.PrimaryGrid.MouseEditMode = DevComponents.DotNetBar.SuperGrid.MouseEditMode.SingleClick;
            this.gList.PrimaryGrid.MultiSelect = false;
            this.gList.PrimaryGrid.RowHeaderIndexOffset = 1;
            this.gList.PrimaryGrid.ShowInsertRow = true;
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
            this.chuotPhai_GhiDuLieu,
            this.chuotPhai_CopyDong,
            this.chuotPhai_XoaDong,
            this.chuotPhai_XoaHet,
            this.chuotPhai_XuatExcel});
            this.mnuChuotPhai.Name = "mnuChuotPhai";
            this.mnuChuotPhai.Size = new System.Drawing.Size(157, 114);
            // 
            // chuotPhai_GhiDuLieu
            // 
            this.chuotPhai_GhiDuLieu.Name = "chuotPhai_GhiDuLieu";
            this.chuotPhai_GhiDuLieu.Size = new System.Drawing.Size(156, 22);
            this.chuotPhai_GhiDuLieu.Text = "Ghi dữ liệu (F1)";
            this.chuotPhai_GhiDuLieu.Click += new System.EventHandler(this.chuotPhai_GhiDuLieu_Click);
            // 
            // chuotPhai_CopyDong
            // 
            this.chuotPhai_CopyDong.Name = "chuotPhai_CopyDong";
            this.chuotPhai_CopyDong.Size = new System.Drawing.Size(156, 22);
            this.chuotPhai_CopyDong.Text = "Copy dòng (F5)";
            this.chuotPhai_CopyDong.Click += new System.EventHandler(this.chuotPhai_CopyDong_Click);
            // 
            // chuotPhai_XoaDong
            // 
            this.chuotPhai_XoaDong.Name = "chuotPhai_XoaDong";
            this.chuotPhai_XoaDong.Size = new System.Drawing.Size(156, 22);
            this.chuotPhai_XoaDong.Text = "Xóa dòng (F8)";
            this.chuotPhai_XoaDong.Click += new System.EventHandler(this.chuotPhai_XoaDong_Click);
            // 
            // chuotPhai_XoaHet
            // 
            this.chuotPhai_XoaHet.Name = "chuotPhai_XoaHet";
            this.chuotPhai_XoaHet.Size = new System.Drawing.Size(156, 22);
            this.chuotPhai_XoaHet.Text = "Xóa hết (F11)";
            this.chuotPhai_XoaHet.Click += new System.EventHandler(this.chuotPhai_XoaHet_Click);
            // 
            // chuotPhai_XuatExcel
            // 
            this.chuotPhai_XuatExcel.Name = "chuotPhai_XuatExcel";
            this.chuotPhai_XuatExcel.Size = new System.Drawing.Size(156, 22);
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
            this.lblPhimTat.Text = "Ctrl+S: Ghi dữ liệu         F5: Copy dòng         F8: Xóa dòng           F11: Xóa" +
    " hết            F12: Excel";
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
            this.phimTat_GhiDuLieu,
            this.phimTat_CopyDong,
            this.phimTat_XoaDong,
            this.phimTat_XoaHet,
            this.phimTat_Excel});
            this.phimTat.Name = "phimTat";
            this.phimTat.Size = new System.Drawing.Size(64, 20);
            this.phimTat.Text = "PhimTat";
            // 
            // phimTat_GhiDuLieu
            // 
            this.phimTat_GhiDuLieu.Name = "phimTat_GhiDuLieu";
            this.phimTat_GhiDuLieu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.phimTat_GhiDuLieu.Size = new System.Drawing.Size(169, 22);
            this.phimTat_GhiDuLieu.Text = "GhiDuLieu";
            this.phimTat_GhiDuLieu.Click += new System.EventHandler(this.phimTat_GhiDuLieu_Click);
            // 
            // phimTat_CopyDong
            // 
            this.phimTat_CopyDong.Name = "phimTat_CopyDong";
            this.phimTat_CopyDong.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.phimTat_CopyDong.Size = new System.Drawing.Size(169, 22);
            this.phimTat_CopyDong.Text = "CopyDong";
            this.phimTat_CopyDong.Click += new System.EventHandler(this.phimTat_CopyDong_Click);
            // 
            // phimTat_XoaDong
            // 
            this.phimTat_XoaDong.Name = "phimTat_XoaDong";
            this.phimTat_XoaDong.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.phimTat_XoaDong.Size = new System.Drawing.Size(169, 22);
            this.phimTat_XoaDong.Text = "XoaDong";
            this.phimTat_XoaDong.Click += new System.EventHandler(this.phimTat_XoaDong_Click);
            // 
            // phimTat_XoaHet
            // 
            this.phimTat_XoaHet.Name = "phimTat_XoaHet";
            this.phimTat_XoaHet.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.phimTat_XoaHet.Size = new System.Drawing.Size(169, 22);
            this.phimTat_XoaHet.Text = "XoaHet";
            this.phimTat_XoaHet.Click += new System.EventHandler(this.phimTat_XoaHet_Click);
            // 
            // phimTat_Excel
            // 
            this.phimTat_Excel.Name = "phimTat_Excel";
            this.phimTat_Excel.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.phimTat_Excel.Size = new System.Drawing.Size(169, 22);
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
            // frmGC_HDGC_SANPHAMlist
            // 
            this.ClientSize = new System.Drawing.Size(784, 565);
            this.Controls.Add(this.menuPhimTat);
            this.MainMenuStrip = this.menuPhimTat;
            this.Name = "frmGC_HDGC_SANPHAMlist";
            this.Text = "Danh sách GC_HDGC_SANPHAM";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmGC_HDGC_SANPHAMlist_Load);
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
	        private ButtonX btnGhiDuLieu;																
	        private ButtonX btnThoat;																
	        private LabelX lblPhimTat;																
	        private System.ComponentModel.BackgroundWorker bwLoadData;																
	        private System.Windows.Forms.MenuStrip menuPhimTat;																
	        private System.Windows.Forms.ToolStripMenuItem phimTat;																
	        private System.Windows.Forms.ToolStripMenuItem phimTat_GhiDuLieu;																
	        private Controls.ProgressBarX proLoad;																
	        private tTextBox txtSP_MA;
	        private tTextBox txtSP_TEN;
	        private tTextBox txtSP_DVT;
	        private tTextBox txtSP_HS;
	        private tTextBox txtSP_LOAISP_MA;
	        private tTextBox txtSP_DONGIA;
	        private tTextBox txtSP_MABIEUTHUE_XNK;
	        private tTextBox txtSP_MABIEUTHUE_TBDB;
	        private tTextBox txtSP_MABIEUTHUE_MT;
	        private tTextBox txtSP_MABIEUTHUE_VAT;
	        private tTextBox txtSP_GHICHU;
	        private tTextBox txtSP_TEN_EN;
	        private System.Windows.Forms.BindingSource bsList;																
	        private System.Windows.Forms.ContextMenuStrip mnuChuotPhai;																
	        private System.Windows.Forms.ToolStripMenuItem chuotPhai_GhiDuLieu;																
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
