namespace DevComponents.DotNetBar
{
    partial class frmThongDiep
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
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn8 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn9 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn10 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn11 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn12 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn13 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn14 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn15 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn16 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.gList = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.bwLoadData = new System.ComponentModel.BackgroundWorker();
            this.CONTENT.SuspendLayout();
            this.SuspendLayout();
            // 
            // TITLE
            // 
            // 
            // 
            // 
            this.TITLE.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.TITLE.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.TITLE.Text = "LỊCH SỬ GIAO DỊCH";
            // 
            // CONTENT
            // 
            this.CONTENT.Controls.Add(this.gList);
            this.CONTENT.Controls.Add(this.groupPanel1);
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(784, 118);
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
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
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
            // gList
            // 
            this.gList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gList.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.gList.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.gList.Location = new System.Drawing.Point(0, 118);
            this.gList.Name = "gList";
            gridColumn1.DataPropertyName = "TD_ID";
            gridColumn1.Name = "TD_ID";
            gridColumn2.DataPropertyName = "TD_MANV";
            gridColumn2.Name = "TD_MANV";
            gridColumn3.DataPropertyName = "TD_TENNV";
            gridColumn3.Name = "TD_TENNV";
            gridColumn4.DataPropertyName = "TD_MATD";
            gridColumn4.Name = "TD_MATD";
            gridColumn5.DataPropertyName = "TD_DINHDANG";
            gridColumn5.Name = "TD_DINHDANG";
            gridColumn6.DataPropertyName = "TD_RETURNCODE";
            gridColumn6.Name = "TD_RETURNCODE";
            gridColumn7.DataPropertyName = "TD_TIEUDE";
            gridColumn7.Name = "TD_TIEUDE";
            gridColumn8.DataPropertyName = "TD_THOIGIAN";
            gridColumn8.Name = "TD_THOIGIAN";
            gridColumn9.DataPropertyName = "TD_LOAITD";
            gridColumn9.Name = "TD_LOAITD";
            gridColumn10.DataPropertyName = "TD_COKETTHUC";
            gridColumn10.Name = "TD_COKETTHUC";
            gridColumn11.DataPropertyName = "TD_MESSSAGECODE";
            gridColumn11.Name = "TD_MESSSAGECODE";
            gridColumn12.DataPropertyName = "TD_TENCHITIEU";
            gridColumn12.Name = "TD_TENCHITIEU";
            gridColumn13.DataPropertyName = "TD_MOTALOI";
            gridColumn13.Name = "TD_MOTALOI";
            gridColumn14.DataPropertyName = "TD_CACHKHACPHUC";
            gridColumn14.Name = "TD_CACHKHACPHUC";
            gridColumn15.DataPropertyName = "TD_TRANGTHAI";
            gridColumn15.Name = "TD_TRANGTHAI";
            gridColumn16.DataPropertyName = "TD_CONTENT";
            gridColumn16.Name = "TD_CONTENT";
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
            this.gList.Size = new System.Drawing.Size(784, 390);
            this.gList.TabIndex = 1;
            this.gList.Text = "superGridControl1";
            // 
            // bwLoadData
            // 
            this.bwLoadData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadData_DoWork);
            this.bwLoadData.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadData_RunWorkerCompleted);
            // 
            // frmThongDiep
            // 
            this.ClientSize = new System.Drawing.Size(784, 565);
            this.Name = "frmThongDiep";
            this.Text = "Thông điệp nghiệp vụ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmThongDiep_Load);
            this.CONTENT.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.GroupPanel groupPanel1;
        private SuperGrid.SuperGridControl gList;
        private System.ComponentModel.BackgroundWorker bwLoadData;
    }
}
