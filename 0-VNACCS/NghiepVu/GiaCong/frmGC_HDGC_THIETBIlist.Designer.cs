	namespace DevComponents.DotNetBar
	{																
	    partial class frmGC_HDGC_THIETBIlist
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
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn15 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn16 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn17 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gLocDuLieu = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.txtTB_MA = new DevComponents.DotNetBar.tTextBox();
            this.txtTB_TEN = new DevComponents.DotNetBar.tTextBox();
            this.txtTB_DVT = new DevComponents.DotNetBar.tTextBox();
            this.txtTB_HS = new DevComponents.DotNetBar.tTextBox();
            this.txtTB_NUOCXX = new DevComponents.DotNetBar.tTextBox();
            this.txtTB_TINHTRANG = new DevComponents.DotNetBar.tTextBox();
            this.txtTB_SOLUONG = new DevComponents.DotNetBar.tTextBox();
            this.txtTB_DONGIA = new DevComponents.DotNetBar.tTextBox();
            this.txtTB_MANGTE = new DevComponents.DotNetBar.tTextBox();
            this.txtTB_MABIEUTHUE_XNK = new DevComponents.DotNetBar.tTextBox();
            this.txtTB_MABIEUTHUE_TBDB = new DevComponents.DotNetBar.tTextBox();
            this.txtTB_MABIEUTHUE_MT = new DevComponents.DotNetBar.tTextBox();
            this.txtTB_MABIEUTHUE_VAT = new DevComponents.DotNetBar.tTextBox();
            this.txtTB_GHICHU = new DevComponents.DotNetBar.tTextBox();
            this.txtTB_TEN_EN = new DevComponents.DotNetBar.tTextBox();
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
            this.TITLE.Text = "DANH SÁCH GC_HDGC_THIETBI";
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
            this.gLocDuLieu.Controls.Add(this.txtTB_MA);
            this.gLocDuLieu.Controls.Add(this.txtTB_TEN);
            this.gLocDuLieu.Controls.Add(this.txtTB_DVT);
            this.gLocDuLieu.Controls.Add(this.txtTB_HS);
            this.gLocDuLieu.Controls.Add(this.txtTB_NUOCXX);
            this.gLocDuLieu.Controls.Add(this.txtTB_TINHTRANG);
            this.gLocDuLieu.Controls.Add(this.txtTB_SOLUONG);
            this.gLocDuLieu.Controls.Add(this.txtTB_DONGIA);
            this.gLocDuLieu.Controls.Add(this.txtTB_MANGTE);
            this.gLocDuLieu.Controls.Add(this.txtTB_MABIEUTHUE_XNK);
            this.gLocDuLieu.Controls.Add(this.txtTB_MABIEUTHUE_TBDB);
            this.gLocDuLieu.Controls.Add(this.txtTB_MABIEUTHUE_MT);
            this.gLocDuLieu.Controls.Add(this.txtTB_MABIEUTHUE_VAT);
            this.gLocDuLieu.Controls.Add(this.txtTB_GHICHU);
            this.gLocDuLieu.Controls.Add(this.txtTB_TEN_EN);
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
            // txtTB_MA
            // 
            this.txtTB_MA.BắtBuộc = false;
            // 
            // 
            // 
            this.txtTB_MA.Border.Class = "TextBoxBorder";
            this.txtTB_MA.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTB_MA.Location = new System.Drawing.Point(6, 5);
            this.txtTB_MA.Name = "txtTB_MA";
            this.txtTB_MA.Size = new System.Drawing.Size(100, 20);
            this.txtTB_MA.TabIndex = 1;
            this.txtTB_MA.Tag = "TB_MA";
            this.txtTB_MA.WatermarkText = "TB_MA";
            this.txtTB_MA.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtTB_TEN
            // 
            this.txtTB_TEN.BắtBuộc = false;
            // 
            // 
            // 
            this.txtTB_TEN.Border.Class = "TextBoxBorder";
            this.txtTB_TEN.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTB_TEN.Location = new System.Drawing.Point(116, 5);
            this.txtTB_TEN.Name = "txtTB_TEN";
            this.txtTB_TEN.Size = new System.Drawing.Size(100, 20);
            this.txtTB_TEN.TabIndex = 2;
            this.txtTB_TEN.Tag = "TB_TEN";
            this.txtTB_TEN.WatermarkText = "TB_TEN";
            this.txtTB_TEN.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtTB_DVT
            // 
            this.txtTB_DVT.BắtBuộc = false;
            // 
            // 
            // 
            this.txtTB_DVT.Border.Class = "TextBoxBorder";
            this.txtTB_DVT.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTB_DVT.Location = new System.Drawing.Point(226, 5);
            this.txtTB_DVT.Name = "txtTB_DVT";
            this.txtTB_DVT.Size = new System.Drawing.Size(100, 20);
            this.txtTB_DVT.TabIndex = 3;
            this.txtTB_DVT.Tag = "TB_DVT";
            this.txtTB_DVT.WatermarkText = "TB_DVT";
            this.txtTB_DVT.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtTB_HS
            // 
            this.txtTB_HS.BắtBuộc = false;
            // 
            // 
            // 
            this.txtTB_HS.Border.Class = "TextBoxBorder";
            this.txtTB_HS.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTB_HS.Location = new System.Drawing.Point(336, 5);
            this.txtTB_HS.Name = "txtTB_HS";
            this.txtTB_HS.Size = new System.Drawing.Size(100, 20);
            this.txtTB_HS.TabIndex = 4;
            this.txtTB_HS.Tag = "TB_HS";
            this.txtTB_HS.WatermarkText = "TB_HS";
            this.txtTB_HS.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtTB_NUOCXX
            // 
            this.txtTB_NUOCXX.BắtBuộc = false;
            // 
            // 
            // 
            this.txtTB_NUOCXX.Border.Class = "TextBoxBorder";
            this.txtTB_NUOCXX.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTB_NUOCXX.Location = new System.Drawing.Point(446, 5);
            this.txtTB_NUOCXX.Name = "txtTB_NUOCXX";
            this.txtTB_NUOCXX.Size = new System.Drawing.Size(100, 20);
            this.txtTB_NUOCXX.TabIndex = 5;
            this.txtTB_NUOCXX.Tag = "TB_NUOCXX";
            this.txtTB_NUOCXX.WatermarkText = "TB_NUOCXX";
            this.txtTB_NUOCXX.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtTB_TINHTRANG
            // 
            this.txtTB_TINHTRANG.BắtBuộc = false;
            // 
            // 
            // 
            this.txtTB_TINHTRANG.Border.Class = "TextBoxBorder";
            this.txtTB_TINHTRANG.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTB_TINHTRANG.Location = new System.Drawing.Point(556, 5);
            this.txtTB_TINHTRANG.Name = "txtTB_TINHTRANG";
            this.txtTB_TINHTRANG.Size = new System.Drawing.Size(100, 20);
            this.txtTB_TINHTRANG.TabIndex = 6;
            this.txtTB_TINHTRANG.Tag = "TB_TINHTRANG";
            this.txtTB_TINHTRANG.WatermarkText = "TB_TINHTRANG";
            this.txtTB_TINHTRANG.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtTB_SOLUONG
            // 
            this.txtTB_SOLUONG.BắtBuộc = false;
            // 
            // 
            // 
            this.txtTB_SOLUONG.Border.Class = "TextBoxBorder";
            this.txtTB_SOLUONG.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTB_SOLUONG.Location = new System.Drawing.Point(666, 5);
            this.txtTB_SOLUONG.Name = "txtTB_SOLUONG";
            this.txtTB_SOLUONG.Size = new System.Drawing.Size(100, 20);
            this.txtTB_SOLUONG.TabIndex = 7;
            this.txtTB_SOLUONG.Tag = "TB_SOLUONG";
            this.txtTB_SOLUONG.WatermarkText = "TB_SOLUONG";
            this.txtTB_SOLUONG.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtTB_DONGIA
            // 
            this.txtTB_DONGIA.BắtBuộc = false;
            // 
            // 
            // 
            this.txtTB_DONGIA.Border.Class = "TextBoxBorder";
            this.txtTB_DONGIA.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTB_DONGIA.Location = new System.Drawing.Point(776, 5);
            this.txtTB_DONGIA.Name = "txtTB_DONGIA";
            this.txtTB_DONGIA.Size = new System.Drawing.Size(100, 20);
            this.txtTB_DONGIA.TabIndex = 8;
            this.txtTB_DONGIA.Tag = "TB_DONGIA";
            this.txtTB_DONGIA.WatermarkText = "TB_DONGIA";
            this.txtTB_DONGIA.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtTB_MANGTE
            // 
            this.txtTB_MANGTE.BắtBuộc = false;
            // 
            // 
            // 
            this.txtTB_MANGTE.Border.Class = "TextBoxBorder";
            this.txtTB_MANGTE.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTB_MANGTE.Location = new System.Drawing.Point(886, 5);
            this.txtTB_MANGTE.Name = "txtTB_MANGTE";
            this.txtTB_MANGTE.Size = new System.Drawing.Size(100, 20);
            this.txtTB_MANGTE.TabIndex = 9;
            this.txtTB_MANGTE.Tag = "TB_MANGTE";
            this.txtTB_MANGTE.WatermarkText = "TB_MANGTE";
            this.txtTB_MANGTE.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtTB_MABIEUTHUE_XNK
            // 
            this.txtTB_MABIEUTHUE_XNK.BắtBuộc = false;
            // 
            // 
            // 
            this.txtTB_MABIEUTHUE_XNK.Border.Class = "TextBoxBorder";
            this.txtTB_MABIEUTHUE_XNK.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTB_MABIEUTHUE_XNK.Location = new System.Drawing.Point(996, 5);
            this.txtTB_MABIEUTHUE_XNK.Name = "txtTB_MABIEUTHUE_XNK";
            this.txtTB_MABIEUTHUE_XNK.Size = new System.Drawing.Size(100, 20);
            this.txtTB_MABIEUTHUE_XNK.TabIndex = 10;
            this.txtTB_MABIEUTHUE_XNK.Tag = "TB_MABIEUTHUE_XNK";
            this.txtTB_MABIEUTHUE_XNK.WatermarkText = "TB_MABIEUTHUE_XNK";
            this.txtTB_MABIEUTHUE_XNK.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtTB_MABIEUTHUE_TBDB
            // 
            this.txtTB_MABIEUTHUE_TBDB.BắtBuộc = false;
            // 
            // 
            // 
            this.txtTB_MABIEUTHUE_TBDB.Border.Class = "TextBoxBorder";
            this.txtTB_MABIEUTHUE_TBDB.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTB_MABIEUTHUE_TBDB.Location = new System.Drawing.Point(1106, 5);
            this.txtTB_MABIEUTHUE_TBDB.Name = "txtTB_MABIEUTHUE_TBDB";
            this.txtTB_MABIEUTHUE_TBDB.Size = new System.Drawing.Size(100, 20);
            this.txtTB_MABIEUTHUE_TBDB.TabIndex = 11;
            this.txtTB_MABIEUTHUE_TBDB.Tag = "TB_MABIEUTHUE_TBDB";
            this.txtTB_MABIEUTHUE_TBDB.WatermarkText = "TB_MABIEUTHUE_TBDB";
            this.txtTB_MABIEUTHUE_TBDB.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtTB_MABIEUTHUE_MT
            // 
            this.txtTB_MABIEUTHUE_MT.BắtBuộc = false;
            // 
            // 
            // 
            this.txtTB_MABIEUTHUE_MT.Border.Class = "TextBoxBorder";
            this.txtTB_MABIEUTHUE_MT.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTB_MABIEUTHUE_MT.Location = new System.Drawing.Point(1216, 5);
            this.txtTB_MABIEUTHUE_MT.Name = "txtTB_MABIEUTHUE_MT";
            this.txtTB_MABIEUTHUE_MT.Size = new System.Drawing.Size(100, 20);
            this.txtTB_MABIEUTHUE_MT.TabIndex = 12;
            this.txtTB_MABIEUTHUE_MT.Tag = "TB_MABIEUTHUE_MT";
            this.txtTB_MABIEUTHUE_MT.WatermarkText = "TB_MABIEUTHUE_MT";
            this.txtTB_MABIEUTHUE_MT.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtTB_MABIEUTHUE_VAT
            // 
            this.txtTB_MABIEUTHUE_VAT.BắtBuộc = false;
            // 
            // 
            // 
            this.txtTB_MABIEUTHUE_VAT.Border.Class = "TextBoxBorder";
            this.txtTB_MABIEUTHUE_VAT.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTB_MABIEUTHUE_VAT.Location = new System.Drawing.Point(1326, 5);
            this.txtTB_MABIEUTHUE_VAT.Name = "txtTB_MABIEUTHUE_VAT";
            this.txtTB_MABIEUTHUE_VAT.Size = new System.Drawing.Size(100, 20);
            this.txtTB_MABIEUTHUE_VAT.TabIndex = 13;
            this.txtTB_MABIEUTHUE_VAT.Tag = "TB_MABIEUTHUE_VAT";
            this.txtTB_MABIEUTHUE_VAT.WatermarkText = "TB_MABIEUTHUE_VAT";
            this.txtTB_MABIEUTHUE_VAT.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtTB_GHICHU
            // 
            this.txtTB_GHICHU.BắtBuộc = false;
            // 
            // 
            // 
            this.txtTB_GHICHU.Border.Class = "TextBoxBorder";
            this.txtTB_GHICHU.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTB_GHICHU.Location = new System.Drawing.Point(1436, 5);
            this.txtTB_GHICHU.Name = "txtTB_GHICHU";
            this.txtTB_GHICHU.Size = new System.Drawing.Size(100, 20);
            this.txtTB_GHICHU.TabIndex = 14;
            this.txtTB_GHICHU.Tag = "TB_GHICHU";
            this.txtTB_GHICHU.WatermarkText = "TB_GHICHU";
            this.txtTB_GHICHU.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtTB_TEN_EN
            // 
            this.txtTB_TEN_EN.BắtBuộc = false;
            // 
            // 
            // 
            this.txtTB_TEN_EN.Border.Class = "TextBoxBorder";
            this.txtTB_TEN_EN.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTB_TEN_EN.Location = new System.Drawing.Point(1546, 5);
            this.txtTB_TEN_EN.Name = "txtTB_TEN_EN";
            this.txtTB_TEN_EN.Size = new System.Drawing.Size(100, 20);
            this.txtTB_TEN_EN.TabIndex = 15;
            this.txtTB_TEN_EN.Tag = "TB_TEN_EN";
            this.txtTB_TEN_EN.WatermarkText = "TB_TEN_EN";
            this.txtTB_TEN_EN.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
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
            this.gList.PrimaryGrid.AutoGenerateColumns = false;
            gridColumn1.DataPropertyName = "TB_HDID";
            gridColumn1.HeaderText = "TB_HDID";
            gridColumn1.Name = "TB_HDID";
            gridColumn1.Visible = false;
            gridColumn2.DataPropertyName = "TB_ID";
            gridColumn2.HeaderText = "TB_ID";
            gridColumn2.Name = "TB_ID";
            gridColumn2.Visible = false;
            gridColumn3.DataPropertyName = "TB_MA";
            gridColumn3.HeaderText = "TB_MA";
            gridColumn3.Name = "TB_MA";
            gridColumn4.DataPropertyName = "TB_TEN";
            gridColumn4.HeaderText = "TB_TEN";
            gridColumn4.Name = "TB_TEN";
            gridColumn5.DataPropertyName = "TB_DVT";
            gridColumn5.HeaderText = "TB_DVT";
            gridColumn5.Name = "TB_DVT";
            gridColumn6.DataPropertyName = "TB_HS";
            gridColumn6.HeaderText = "TB_HS";
            gridColumn6.Name = "TB_HS";
            gridColumn7.DataPropertyName = "TB_NUOCXX";
            gridColumn7.HeaderText = "TB_NUOCXX";
            gridColumn7.Name = "TB_NUOCXX";
            gridColumn8.DataPropertyName = "TB_TINHTRANG";
            gridColumn8.HeaderText = "TB_TINHTRANG";
            gridColumn8.Name = "TB_TINHTRANG";
            gridColumn9.DataPropertyName = "TB_SOLUONG";
            gridColumn9.HeaderText = "TB_SOLUONG";
            gridColumn9.Name = "TB_SOLUONG";
            gridColumn10.DataPropertyName = "TB_DONGIA";
            gridColumn10.HeaderText = "TB_DONGIA";
            gridColumn10.Name = "TB_DONGIA";
            gridColumn11.DataPropertyName = "TB_MANGTE";
            gridColumn11.HeaderText = "TB_MANGTE";
            gridColumn11.Name = "TB_MANGTE";
            gridColumn12.DataPropertyName = "TB_MABIEUTHUE_XNK";
            gridColumn12.HeaderText = "TB_MABIEUTHUE_XNK";
            gridColumn12.Name = "TB_MABIEUTHUE_XNK";
            gridColumn13.DataPropertyName = "TB_MABIEUTHUE_TBDB";
            gridColumn13.HeaderText = "TB_MABIEUTHUE_TBDB";
            gridColumn13.Name = "TB_MABIEUTHUE_TBDB";
            gridColumn14.DataPropertyName = "TB_MABIEUTHUE_MT";
            gridColumn14.HeaderText = "TB_MABIEUTHUE_MT";
            gridColumn14.Name = "TB_MABIEUTHUE_MT";
            gridColumn15.DataPropertyName = "TB_MABIEUTHUE_VAT";
            gridColumn15.HeaderText = "TB_MABIEUTHUE_VAT";
            gridColumn15.Name = "TB_MABIEUTHUE_VAT";
            gridColumn16.DataPropertyName = "TB_GHICHU";
            gridColumn16.HeaderText = "TB_GHICHU";
            gridColumn16.Name = "TB_GHICHU";
            gridColumn17.DataPropertyName = "TB_TEN_EN";
            gridColumn17.HeaderText = "TB_TEN_EN";
            gridColumn17.Name = "TB_TEN_EN";
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
            this.gList.PrimaryGrid.Columns.Add(gridColumn15);
            this.gList.PrimaryGrid.Columns.Add(gridColumn16);
            this.gList.PrimaryGrid.Columns.Add(gridColumn17);
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
            this.chuotPhai_GhiDuLieu,
            this.chuotPhai_CopyDong,
            this.chuotPhai_XoaDong,
            this.chuotPhai_XoaHet,
            this.chuotPhai_XuatExcel});
            this.mnuChuotPhai.Name = "mnuChuotPhai";
            this.mnuChuotPhai.Size = new System.Drawing.Size(157, 136);
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
            // frmGC_HDGC_THIETBIlist
            // 
            this.ClientSize = new System.Drawing.Size(784, 565);
            this.Controls.Add(this.menuPhimTat);
            this.MainMenuStrip = this.menuPhimTat;
            this.Name = "frmGC_HDGC_THIETBIlist";
            this.Text = "Danh sách GC_HDGC_THIETBI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmGC_HDGC_THIETBIlist_Load);
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
	        private tTextBox txtTB_MA;
	        private tTextBox txtTB_TEN;
	        private tTextBox txtTB_DVT;
	        private tTextBox txtTB_HS;
	        private tTextBox txtTB_NUOCXX;
	        private tTextBox txtTB_TINHTRANG;
	        private tTextBox txtTB_SOLUONG;
	        private tTextBox txtTB_DONGIA;
	        private tTextBox txtTB_MANGTE;
	        private tTextBox txtTB_MABIEUTHUE_XNK;
	        private tTextBox txtTB_MABIEUTHUE_TBDB;
	        private tTextBox txtTB_MABIEUTHUE_MT;
	        private tTextBox txtTB_MABIEUTHUE_VAT;
	        private tTextBox txtTB_GHICHU;
	        private tTextBox txtTB_TEN_EN;
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
