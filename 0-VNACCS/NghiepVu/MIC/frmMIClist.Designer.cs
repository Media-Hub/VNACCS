namespace DevComponents.DotNetBar
{
    partial class frmMIClist
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
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn7 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btnThoat = new DevComponents.DotNetBar.ButtonX();
            this.btnXemChiTiet = new DevComponents.DotNetBar.ButtonX();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX4 = new DevComponents.DotNetBar.ButtonX();
            this.gList = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.bwLoadData = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.phimTatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xemChiTietToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBarX1 = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.CONTENT.SuspendLayout();
            this.pBOTTOM.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TITLE
            // 
            // 
            // 
            // 
            this.TITLE.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.TITLE.Text = "DANH SÁCH TỜ KHAI NHẬP KHẨU TRỊ GIÁ THẤP (MIC)";
            // 
            // CONTENT
            // 
            this.CONTENT.Controls.Add(this.progressBarX1);
            this.CONTENT.Controls.Add(this.gList);
            this.CONTENT.Controls.Add(this.labelX1);
            this.CONTENT.Controls.Add(this.groupPanel1);
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
            this.pBOTTOM.Controls.Add(this.buttonX4);
            this.pBOTTOM.Controls.Add(this.buttonX3);
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
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupPanel1.Location = new System.Drawing.Point(2, 2);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(780, 100);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
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
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 0;
            this.groupPanel1.Text = "Lọc dữ liệu";
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
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX3.Location = new System.Drawing.Point(129, 4);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(87, 23);
            this.buttonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 2;
            this.buttonX3.Text = "In ấn";
            // 
            // buttonX4
            // 
            this.buttonX4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX4.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX4.Location = new System.Drawing.Point(297, 4);
            this.buttonX4.Name = "buttonX4";
            this.buttonX4.Size = new System.Drawing.Size(75, 23);
            this.buttonX4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX4.TabIndex = 3;
            this.buttonX4.Text = "Xóa tờ khai";
            // 
            // gList
            // 
            this.gList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gList.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.gList.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.gList.Location = new System.Drawing.Point(2, 122);
            this.gList.Name = "gList";
            this.gList.PrimaryGrid.AutoGenerateColumns = false;
            gridColumn1.DataPropertyName = "TK_ID";
            gridColumn1.HeaderText = "TK_ID";
            gridColumn1.Name = "TK_ID";
            gridColumn1.Visible = false;
            gridColumn2.DataPropertyName = "ICN";
            gridColumn2.HeaderText = "Số tờ khai";
            gridColumn2.Name = "ICN";
            gridColumn3.DataPropertyName = "TK_NGAYDK";
            gridColumn3.HeaderText = "Ngày đăng ký";
            gridColumn3.Name = "TK_NGAYDK";
            gridColumn4.DataPropertyName = "CH";
            gridColumn4.HeaderText = "Hải quan";
            gridColumn4.Name = "CH";
            gridColumn5.DataPropertyName = "CHB";
            gridColumn5.HeaderText = "Bộ phận HQ";
            gridColumn5.Name = "CHB";
            gridColumn6.DataPropertyName = "IMC";
            gridColumn6.HeaderText = "Mã người NK";
            gridColumn6.Name = "IMC";
            gridColumn7.DataPropertyName = "IMN";
            gridColumn7.HeaderText = "Tên người NK";
            gridColumn7.Name = "IMN";
            this.gList.PrimaryGrid.Columns.Add(gridColumn1);
            this.gList.PrimaryGrid.Columns.Add(gridColumn2);
            this.gList.PrimaryGrid.Columns.Add(gridColumn3);
            this.gList.PrimaryGrid.Columns.Add(gridColumn4);
            this.gList.PrimaryGrid.Columns.Add(gridColumn5);
            this.gList.PrimaryGrid.Columns.Add(gridColumn6);
            this.gList.PrimaryGrid.Columns.Add(gridColumn7);
            this.gList.PrimaryGrid.MultiSelect = false;
            this.gList.PrimaryGrid.ReadOnly = true;
            this.gList.PrimaryGrid.RowHeaderIndexOffset = 1;
            this.gList.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.RowWithCellHighlight;
            this.gList.PrimaryGrid.ShowRowGridIndex = true;
            this.gList.PrimaryGrid.UseAlternateRowStyle = true;
            this.gList.Size = new System.Drawing.Size(780, 379);
            this.gList.TabIndex = 1;
            this.gList.Text = "superGridControl1";
            
            this.gList.CellDoubleClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellDoubleClickEventArgs>(this.gList_CellDoubleClick);
            this.gList.RowHeaderClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridRowHeaderClickEventArgs>(this.gList_RowHeaderClick);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelX1.Location = new System.Drawing.Point(2, 102);
            this.labelX1.Name = "labelX1";
            this.labelX1.PaddingLeft = 5;
            this.labelX1.Size = new System.Drawing.Size(780, 20);
            this.labelX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.labelX1.TabIndex = 6;
            this.labelX1.Text = "F1: Xem chi tiết          F5: Copy dòng         F8: Xóa dòng           F11: Xóa h" +
    "ết            F12: Excel";
            // 
            // bwLoadData
            // 
            this.bwLoadData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadData_DoWork);
            this.bwLoadData.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadData_RunWorkerCompleted);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.phimTatToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // phimTatToolStripMenuItem
            // 
            this.phimTatToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xemChiTietToolStripMenuItem});
            this.phimTatToolStripMenuItem.Name = "phimTatToolStripMenuItem";
            this.phimTatToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.phimTatToolStripMenuItem.Text = "PhimTat";
            // 
            // xemChiTietToolStripMenuItem
            // 
            this.xemChiTietToolStripMenuItem.Name = "xemChiTietToolStripMenuItem";
            this.xemChiTietToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.xemChiTietToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.xemChiTietToolStripMenuItem.Text = "XemChiTiet";
            this.xemChiTietToolStripMenuItem.Click += new System.EventHandler(this.xemChiTietToolStripMenuItem_Click);
            // 
            // progressBarX1
            // 
            // 
            // 
            // 
            this.progressBarX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.progressBarX1.Location = new System.Drawing.Point(37, 157);
            this.progressBarX1.Name = "progressBarX1";
            this.progressBarX1.ProgressType = DevComponents.DotNetBar.eProgressItemType.Marquee;
            this.progressBarX1.Size = new System.Drawing.Size(231, 23);
            this.progressBarX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.progressBarX1.TabIndex = 7;
            this.progressBarX1.Text = "Đang xử lý...";
            this.progressBarX1.TextVisible = true;
            // 
            // frmMIClist
            // 
            this.ClientSize = new System.Drawing.Size(784, 565);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMIClist";
            this.Text = "Danh sách TKN trị giá thấp (MIC)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMIClist_Load);
            this.Controls.SetChildIndex(this.menuStrip1, 0);
            this.Controls.SetChildIndex(this.TITLE, 0);
            this.Controls.SetChildIndex(this.pBOTTOM, 0);
            this.Controls.SetChildIndex(this.CONTENT, 0);
            this.CONTENT.ResumeLayout(false);
            this.pBOTTOM.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.GroupPanel groupPanel1;
        private SuperGrid.SuperGridControl gList;
        private ButtonX buttonX4;
        private ButtonX buttonX3;
        private ButtonX btnXemChiTiet;
        private ButtonX btnThoat;
        private LabelX labelX1;
        private System.ComponentModel.BackgroundWorker bwLoadData;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem phimTatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xemChiTietToolStripMenuItem;
        private Controls.ProgressBarX progressBarX1;
    }
}
