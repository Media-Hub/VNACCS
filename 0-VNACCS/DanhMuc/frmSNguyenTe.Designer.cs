namespace DevComponents.DotNetBar
{
    partial class frmSNguyenTe
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
            this.menuPhimTat = new System.Windows.Forms.MenuStrip();
            this.phimTatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ghiDuLieuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hoaHangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xoaHetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyHangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnThoat = new DevComponents.DotNetBar.ButtonX();
            this.btnGhi = new DevComponents.DotNetBar.ButtonX();
            this.gList = new DevComponents.DotNetBar.tGrid();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.txtNT_TENNT = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtNT_MANT = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.bwLoadData = new System.ComponentModel.BackgroundWorker();
            this.progressBarX1 = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnDongBo = new DevComponents.DotNetBar.ButtonX();
            this.pBOTTOM.SuspendLayout();
            this.CONTENT.SuspendLayout();
            this.menuPhimTat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TITLE
            // 
            // 
            // 
            // 
            this.TITLE.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.TITLE.Text = "DANH MỤC NGUYÊN TỆ";
            // 
            // pBOTTOM
            // 
            this.pBOTTOM.Controls.Add(this.btnDongBo);
            this.pBOTTOM.Controls.Add(this.btnGhi);
            this.pBOTTOM.Controls.Add(this.btnThoat);
            // 
            // CONTENT
            // 
            this.CONTENT.Controls.Add(this.progressBarX1);
            this.CONTENT.Controls.Add(this.gList);
            this.CONTENT.Controls.Add(this.labelX1);
            this.CONTENT.Controls.Add(this.groupPanel1);
            // 
            // menuPhimTat
            // 
            this.menuPhimTat.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.phimTatToolStripMenuItem});
            this.menuPhimTat.Location = new System.Drawing.Point(0, 0);
            this.menuPhimTat.Name = "menuPhimTat";
            this.menuPhimTat.Size = new System.Drawing.Size(784, 24);
            this.menuPhimTat.TabIndex = 3;
            this.menuPhimTat.Text = "menuStrip1";
            this.menuPhimTat.Visible = false;
            // 
            // phimTatToolStripMenuItem
            // 
            this.phimTatToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ghiDuLieuToolStripMenuItem,
            this.hoaHangToolStripMenuItem,
            this.xoaHetToolStripMenuItem,
            this.copyHangToolStripMenuItem,
            this.autoExcelToolStripMenuItem});
            this.phimTatToolStripMenuItem.Name = "phimTatToolStripMenuItem";
            this.phimTatToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.phimTatToolStripMenuItem.Text = "PhimTat";
            // 
            // ghiDuLieuToolStripMenuItem
            // 
            this.ghiDuLieuToolStripMenuItem.Name = "ghiDuLieuToolStripMenuItem";
            this.ghiDuLieuToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.ghiDuLieuToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.ghiDuLieuToolStripMenuItem.Text = "GhiDuLieu";
            this.ghiDuLieuToolStripMenuItem.Click += new System.EventHandler(this.ghiDuLieuToolStripMenuItem_Click);
            // 
            // hoaHangToolStripMenuItem
            // 
            this.hoaHangToolStripMenuItem.Name = "hoaHangToolStripMenuItem";
            this.hoaHangToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.hoaHangToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.hoaHangToolStripMenuItem.Text = "XoaHang";
            this.hoaHangToolStripMenuItem.Click += new System.EventHandler(this.hoaHangToolStripMenuItem_Click);
            // 
            // xoaHetToolStripMenuItem
            // 
            this.xoaHetToolStripMenuItem.Name = "xoaHetToolStripMenuItem";
            this.xoaHetToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.xoaHetToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.xoaHetToolStripMenuItem.Text = "XoaHet";
            this.xoaHetToolStripMenuItem.Click += new System.EventHandler(this.xoaHetToolStripMenuItem_Click);
            // 
            // copyHangToolStripMenuItem
            // 
            this.copyHangToolStripMenuItem.Name = "copyHangToolStripMenuItem";
            this.copyHangToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.copyHangToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.copyHangToolStripMenuItem.Text = "CopyHang";
            this.copyHangToolStripMenuItem.Click += new System.EventHandler(this.copyHangToolStripMenuItem_Click);
            // 
            // autoExcelToolStripMenuItem
            // 
            this.autoExcelToolStripMenuItem.Name = "autoExcelToolStripMenuItem";
            this.autoExcelToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.autoExcelToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.autoExcelToolStripMenuItem.Text = "AutoExcel";
            this.autoExcelToolStripMenuItem.Click += new System.EventHandler(this.autoExcelToolStripMenuItem_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThoat.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnThoat.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnThoat.Location = new System.Drawing.Point(707, 2);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(75, 23);
            this.btnThoat.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnThoat.TabIndex = 0;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnGhi
            // 
            this.btnGhi.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnGhi.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnGhi.Location = new System.Drawing.Point(4, 2);
            this.btnGhi.Name = "btnGhi";
            this.btnGhi.Size = new System.Drawing.Size(75, 23);
            this.btnGhi.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnGhi.TabIndex = 1;
            this.btnGhi.Text = "Ghi (Ctrl+S)";
            this.btnGhi.Click += new System.EventHandler(this.btnGhi_Click);
            // 
            // gList
            // 
            this.gList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gList.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.gList.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.gList.Location = new System.Drawing.Point(0, 64);
            this.gList.Name = "gList";
            this.gList.PrimaryGrid.AllowRowDelete = true;
            this.gList.PrimaryGrid.AllowRowInsert = true;
            this.gList.PrimaryGrid.AutoGenerateColumns = false;
            gridColumn1.DataPropertyName = "NT_MANT";
            gridColumn1.HeaderText = "Mã nguyên tệ";
            gridColumn1.Name = "NT_MANT";
            gridColumn2.DataPropertyName = "NT_TENNT";
            gridColumn2.HeaderText = "Tên nguyên tệ";
            gridColumn2.Name = "NT_TENNT";
            gridColumn2.Width = 200;
            gridColumn3.DataPropertyName = "NT_TYGIAVND";
            gridColumn3.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleInputEditControl);
            gridColumn3.HeaderText = "Tỷ giá VNĐ";
            gridColumn3.Name = "NT_TYGIAVND";
            this.gList.PrimaryGrid.Columns.Add(gridColumn1);
            this.gList.PrimaryGrid.Columns.Add(gridColumn2);
            this.gList.PrimaryGrid.Columns.Add(gridColumn3);
            this.gList.PrimaryGrid.DataSource = this.bsList;
            this.gList.PrimaryGrid.EnableRowFiltering = true;
            this.gList.PrimaryGrid.KeyboardEditMode = DevComponents.DotNetBar.SuperGrid.KeyboardEditMode.EditOnEntry;
            this.gList.PrimaryGrid.MouseEditMode = DevComponents.DotNetBar.SuperGrid.MouseEditMode.SingleClick;
            this.gList.PrimaryGrid.MultiSelect = false;
            this.gList.PrimaryGrid.RowHeaderIndexOffset = 1;
            this.gList.PrimaryGrid.ShowInsertRow = true;
            this.gList.PrimaryGrid.ShowRowGridIndex = true;
            this.gList.PrimaryGrid.UseAlternateRowStyle = true;
            this.gList.Size = new System.Drawing.Size(784, 444);
            this.gList.TabIndex = 0;
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.groupPanel1.Controls.Add(this.txtNT_TENNT);
            this.groupPanel1.Controls.Add(this.txtNT_MANT);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(784, 44);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 1;
            this.groupPanel1.Text = "Lọc dữ liệu";
            // 
            // txtNT_TENNT
            // 
            // 
            // 
            // 
            this.txtNT_TENNT.Border.Class = "TextBoxBorder";
            this.txtNT_TENNT.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtNT_TENNT.FocusHighlightEnabled = true;
            this.txtNT_TENNT.Location = new System.Drawing.Point(153, 3);
            this.txtNT_TENNT.Name = "txtNT_TENNT";
            this.txtNT_TENNT.Size = new System.Drawing.Size(186, 20);
            this.txtNT_TENNT.TabIndex = 1;
            this.txtNT_TENNT.Tag = "NT_TENNT";
            this.txtNT_TENNT.WatermarkText = "Tên nguyên tệ";
            this.txtNT_TENNT.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // txtNT_MANT
            // 
            // 
            // 
            // 
            this.txtNT_MANT.Border.Class = "TextBoxBorder";
            this.txtNT_MANT.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtNT_MANT.FocusHighlightEnabled = true;
            this.txtNT_MANT.Location = new System.Drawing.Point(38, 3);
            this.txtNT_MANT.Name = "txtNT_MANT";
            this.txtNT_MANT.Size = new System.Drawing.Size(100, 20);
            this.txtNT_MANT.TabIndex = 0;
            this.txtNT_MANT.Tag = "NT_MANT";
            this.txtNT_MANT.WatermarkText = "Mã nguyên tệ";
            this.txtNT_MANT.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // bwLoadData
            // 
            this.bwLoadData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadData_DoWork);
            this.bwLoadData.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadData_RunWorkerCompleted);
            // 
            // progressBarX1
            // 
            this.progressBarX1.Anchor = System.Windows.Forms.AnchorStyles.None;
            // 
            // 
            // 
            this.progressBarX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.progressBarX1.Location = new System.Drawing.Point(300, 243);
            this.progressBarX1.Name = "progressBarX1";
            this.progressBarX1.ProgressType = DevComponents.DotNetBar.eProgressItemType.Marquee;
            this.progressBarX1.Size = new System.Drawing.Size(184, 23);
            this.progressBarX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.progressBarX1.TabIndex = 4;
            this.progressBarX1.Text = "Đang xử lý...";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelX1.Location = new System.Drawing.Point(0, 44);
            this.labelX1.Name = "labelX1";
            this.labelX1.PaddingLeft = 5;
            this.labelX1.Size = new System.Drawing.Size(784, 20);
            this.labelX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.labelX1.TabIndex = 5;
            this.labelX1.Text = "F5: Copy dòng         F8: Xóa dòng           F11: Xóa hết            F12: Excel";
            // 
            // btnDongBo
            // 
            this.btnDongBo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDongBo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDongBo.Location = new System.Drawing.Point(103, 2);
            this.btnDongBo.Name = "btnDongBo";
            this.btnDongBo.Size = new System.Drawing.Size(110, 23);
            this.btnDongBo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDongBo.TabIndex = 2;
            this.btnDongBo.Text = "Đồng bộ từ Internet";
            this.btnDongBo.Click += new System.EventHandler(this.btnDongBo_Click);
            // 
            // frmSNguyenTe
            // 
            this.CancelButton = this.btnThoat;
            this.ClientSize = new System.Drawing.Size(784, 565);
            this.Controls.Add(this.menuPhimTat);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuPhimTat;
            this.Name = "frmSNguyenTe";
            this.Load += new System.EventHandler(this.frmSNguyenTe_Load);
            this.Controls.SetChildIndex(this.menuPhimTat, 0);
            this.Controls.SetChildIndex(this.TITLE, 0);
            this.Controls.SetChildIndex(this.pBOTTOM, 0);
            this.Controls.SetChildIndex(this.CONTENT, 0);
            this.pBOTTOM.ResumeLayout(false);
            this.CONTENT.ResumeLayout(false);
            this.menuPhimTat.ResumeLayout(false);
            this.menuPhimTat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuPhimTat;
        private System.Windows.Forms.ToolStripMenuItem phimTatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ghiDuLieuToolStripMenuItem;
        private ButtonX btnGhi;
        private ButtonX btnThoat;
        private tGrid gList;
        private Controls.GroupPanel groupPanel1;
        private Controls.TextBoxX txtNT_TENNT;
        private Controls.TextBoxX txtNT_MANT;
        private System.Windows.Forms.BindingSource bsList;
        private System.ComponentModel.BackgroundWorker bwLoadData;
        private Controls.ProgressBarX progressBarX1;
        private LabelX labelX1;
        private System.Windows.Forms.ToolStripMenuItem hoaHangToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xoaHetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyHangToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoExcelToolStripMenuItem;
        private ButtonX btnDongBo;
    }
}
