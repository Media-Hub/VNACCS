namespace DevComponents.DotNetBar
{
    partial class frmHutDanhMucECUS5
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
            this.txtAccessFile = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnTimDBAccess = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.rAccess = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.rSQL = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtServerName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtDBName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtSQLUser = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txtSQLPass = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnSNUOC = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.bwSieuNhan = new System.ComponentModel.BackgroundWorker();
            this.pDongBo = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btnSNGHTE = new DevComponents.DotNetBar.ButtonX();
            this.btnSCUAKHAUNN = new DevComponents.DotNetBar.ButtonX();
            this.btnSCUAKHAU = new DevComponents.DotNetBar.ButtonX();
            this.btnSDDLK = new DevComponents.DotNetBar.ButtonX();
            this.btnSBOPHAN = new DevComponents.DotNetBar.ButtonX();
            this.btnSHAIQUAN = new DevComponents.DotNetBar.ButtonX();
            this.btnSDVT = new DevComponents.DotNetBar.ButtonX();
            this.progressBarX1 = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.btnSDKGH = new DevComponents.DotNetBar.ButtonX();
            this.CONTENT.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.pDongBo.SuspendLayout();
            this.SuspendLayout();
            // 
            // TITLE
            // 
            // 
            // 
            // 
            this.TITLE.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.TITLE.Text = "HÚT DANH MỤC CỦA ECUS5";
            // 
            // CONTENT
            // 
            this.CONTENT.Controls.Add(this.progressBarX1);
            this.CONTENT.Controls.Add(this.pDongBo);
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
            this.pBOTTOM.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pBOTTOM.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pBOTTOM.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pBOTTOM.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pBOTTOM.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pBOTTOM.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pBOTTOM.Style.GradientAngle = 90;
            // 
            // txtAccessFile
            // 
            // 
            // 
            // 
            this.txtAccessFile.Border.Class = "TextBoxBorder";
            this.txtAccessFile.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtAccessFile.Location = new System.Drawing.Point(94, 35);
            this.txtAccessFile.Name = "txtAccessFile";
            this.txtAccessFile.Size = new System.Drawing.Size(564, 20);
            this.txtAccessFile.TabIndex = 1;
            this.txtAccessFile.Text = "E:\\ECUSDATA-VNACCS\\ECUS5VNACCS.mdb";
            // 
            // btnTimDBAccess
            // 
            this.btnTimDBAccess.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTimDBAccess.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnTimDBAccess.Location = new System.Drawing.Point(664, 32);
            this.btnTimDBAccess.Name = "btnTimDBAccess";
            this.btnTimDBAccess.Size = new System.Drawing.Size(75, 23);
            this.btnTimDBAccess.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnTimDBAccess.TabIndex = 2;
            this.btnTimDBAccess.Text = "...";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(23, 34);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(70, 23);
            this.labelX1.TabIndex = 3;
            this.labelX1.Text = "Đường dẫn:";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // rAccess
            // 
            // 
            // 
            // 
            this.rAccess.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rAccess.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.rAccess.Location = new System.Drawing.Point(24, 3);
            this.rAccess.Name = "rAccess";
            this.rAccess.Size = new System.Drawing.Size(391, 23);
            this.rAccess.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.rAccess.TabIndex = 4;
            this.rAccess.Text = "Hút từ database access ECUS5VNACCS.mdb";
            // 
            // rSQL
            // 
            // 
            // 
            // 
            this.rSQL.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rSQL.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.rSQL.Checked = true;
            this.rSQL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rSQL.CheckValue = "Y";
            this.rSQL.Location = new System.Drawing.Point(24, 74);
            this.rSQL.Name = "rSQL";
            this.rSQL.Size = new System.Drawing.Size(174, 23);
            this.rSQL.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.rSQL.TabIndex = 5;
            this.rSQL.Text = "Hút từ database SQL";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(0, 102);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(93, 23);
            this.labelX2.TabIndex = 7;
            this.labelX2.Text = "Server name:";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtServerName
            // 
            // 
            // 
            // 
            this.txtServerName.Border.Class = "TextBoxBorder";
            this.txtServerName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtServerName.Location = new System.Drawing.Point(94, 103);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(167, 20);
            this.txtServerName.TabIndex = 6;
            this.txtServerName.Text = "(local)";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(0, 130);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(93, 23);
            this.labelX3.TabIndex = 9;
            this.labelX3.Text = "Database name:";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtDBName
            // 
            // 
            // 
            // 
            this.txtDBName.Border.Class = "TextBoxBorder";
            this.txtDBName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDBName.Location = new System.Drawing.Point(94, 131);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Size = new System.Drawing.Size(167, 20);
            this.txtDBName.TabIndex = 8;
            this.txtDBName.Text = "ECUS5VNACCS";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(284, 102);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(93, 23);
            this.labelX4.TabIndex = 11;
            this.labelX4.Text = "DB User:";
            this.labelX4.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtSQLUser
            // 
            // 
            // 
            // 
            this.txtSQLUser.Border.Class = "TextBoxBorder";
            this.txtSQLUser.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSQLUser.Location = new System.Drawing.Point(378, 103);
            this.txtSQLUser.Name = "txtSQLUser";
            this.txtSQLUser.Size = new System.Drawing.Size(167, 20);
            this.txtSQLUser.TabIndex = 10;
            this.txtSQLUser.Text = "sa";
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(284, 128);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(93, 23);
            this.labelX5.TabIndex = 13;
            this.labelX5.Text = "DB Pass:";
            this.labelX5.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtSQLPass
            // 
            // 
            // 
            // 
            this.txtSQLPass.Border.Class = "TextBoxBorder";
            this.txtSQLPass.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSQLPass.Location = new System.Drawing.Point(378, 129);
            this.txtSQLPass.Name = "txtSQLPass";
            this.txtSQLPass.Size = new System.Drawing.Size(167, 20);
            this.txtSQLPass.TabIndex = 12;
            this.txtSQLPass.Text = "1";
            // 
            // btnSNUOC
            // 
            this.btnSNUOC.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSNUOC.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSNUOC.Location = new System.Drawing.Point(9, 3);
            this.btnSNUOC.Name = "btnSNUOC";
            this.btnSNUOC.Size = new System.Drawing.Size(154, 23);
            this.btnSNUOC.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSNUOC.TabIndex = 14;
            this.btnSNUOC.Text = "1 - Hút Nước";
            this.btnSNUOC.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.btnSNUOC.Click += new System.EventHandler(this.btnHUT_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.rSQL);
            this.groupPanel1.Controls.Add(this.rAccess);
            this.groupPanel1.Controls.Add(this.txtAccessFile);
            this.groupPanel1.Controls.Add(this.labelX5);
            this.groupPanel1.Controls.Add(this.btnTimDBAccess);
            this.groupPanel1.Controls.Add(this.txtSQLPass);
            this.groupPanel1.Controls.Add(this.labelX1);
            this.groupPanel1.Controls.Add(this.labelX4);
            this.groupPanel1.Controls.Add(this.txtSQLUser);
            this.groupPanel1.Controls.Add(this.txtServerName);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.txtDBName);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupPanel1.Location = new System.Drawing.Point(2, 2);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(780, 180);
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
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
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
            this.groupPanel1.TabIndex = 15;
            this.groupPanel1.Text = "Thông tin ECUS5 DB";
            // 
            // bwSieuNhan
            // 
            this.bwSieuNhan.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwSieuNhan_DoWork);
            this.bwSieuNhan.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwSieuNhan_RunWorkerCompleted);
            // 
            // pDongBo
            // 
            this.pDongBo.CanvasColor = System.Drawing.SystemColors.Control;
            this.pDongBo.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.pDongBo.Controls.Add(this.btnSDKGH);
            this.pDongBo.Controls.Add(this.btnSNGHTE);
            this.pDongBo.Controls.Add(this.btnSCUAKHAUNN);
            this.pDongBo.Controls.Add(this.btnSCUAKHAU);
            this.pDongBo.Controls.Add(this.btnSDDLK);
            this.pDongBo.Controls.Add(this.btnSBOPHAN);
            this.pDongBo.Controls.Add(this.btnSHAIQUAN);
            this.pDongBo.Controls.Add(this.btnSDVT);
            this.pDongBo.Controls.Add(this.btnSNUOC);
            this.pDongBo.Location = new System.Drawing.Point(0, 180);
            this.pDongBo.Name = "pDongBo";
            this.pDongBo.Size = new System.Drawing.Size(784, 325);
            // 
            // 
            // 
            this.pDongBo.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.pDongBo.Style.BackColorGradientAngle = 90;
            this.pDongBo.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pDongBo.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.pDongBo.Style.BorderBottomWidth = 1;
            this.pDongBo.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pDongBo.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.pDongBo.Style.BorderLeftWidth = 1;
            this.pDongBo.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.pDongBo.Style.BorderRightWidth = 1;
            this.pDongBo.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.pDongBo.Style.BorderTopWidth = 1;
            this.pDongBo.Style.CornerDiameter = 4;
            this.pDongBo.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.pDongBo.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.pDongBo.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pDongBo.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.pDongBo.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.pDongBo.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.pDongBo.TabIndex = 16;
            this.pDongBo.Text = "Hút danh mục";
            this.pDongBo.Click += new System.EventHandler(this.pDongBo_Click);
            // 
            // btnSNGHTE
            // 
            this.btnSNGHTE.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSNGHTE.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSNGHTE.Location = new System.Drawing.Point(187, 3);
            this.btnSNGHTE.Name = "btnSNGHTE";
            this.btnSNGHTE.Size = new System.Drawing.Size(154, 23);
            this.btnSNGHTE.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSNGHTE.TabIndex = 21;
            this.btnSNGHTE.Text = "8 - Hút nguyên tệ";
            this.btnSNGHTE.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.btnSNGHTE.Click += new System.EventHandler(this.btnHUT_Click);
            // 
            // btnSCUAKHAUNN
            // 
            this.btnSCUAKHAUNN.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSCUAKHAUNN.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSCUAKHAUNN.Location = new System.Drawing.Point(9, 243);
            this.btnSCUAKHAUNN.Name = "btnSCUAKHAUNN";
            this.btnSCUAKHAUNN.Size = new System.Drawing.Size(154, 23);
            this.btnSCUAKHAUNN.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSCUAKHAUNN.TabIndex = 20;
            this.btnSCUAKHAUNN.Text = "7 - Hút cửa khẩu nước ngoài";
            this.btnSCUAKHAUNN.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.btnSCUAKHAUNN.Click += new System.EventHandler(this.btnHUT_Click);
            // 
            // btnSCUAKHAU
            // 
            this.btnSCUAKHAU.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSCUAKHAU.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSCUAKHAU.Location = new System.Drawing.Point(9, 204);
            this.btnSCUAKHAU.Name = "btnSCUAKHAU";
            this.btnSCUAKHAU.Size = new System.Drawing.Size(154, 23);
            this.btnSCUAKHAU.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSCUAKHAU.TabIndex = 19;
            this.btnSCUAKHAU.Text = "6 - Hút cửa khẩu trong nước";
            this.btnSCUAKHAU.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.btnSCUAKHAU.Click += new System.EventHandler(this.btnHUT_Click);
            // 
            // btnSDDLK
            // 
            this.btnSDDLK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSDDLK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSDDLK.Location = new System.Drawing.Point(9, 163);
            this.btnSDDLK.Name = "btnSDDLK";
            this.btnSDDLK.Size = new System.Drawing.Size(154, 23);
            this.btnSDDLK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSDDLK.TabIndex = 18;
            this.btnSDDLK.Text = "5 - Hút địa điểm lưu kho";
            this.btnSDDLK.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.btnSDDLK.Click += new System.EventHandler(this.btnHUT_Click);
            // 
            // btnSBOPHAN
            // 
            this.btnSBOPHAN.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSBOPHAN.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSBOPHAN.Location = new System.Drawing.Point(9, 123);
            this.btnSBOPHAN.Name = "btnSBOPHAN";
            this.btnSBOPHAN.Size = new System.Drawing.Size(154, 23);
            this.btnSBOPHAN.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSBOPHAN.TabIndex = 17;
            this.btnSBOPHAN.Text = "4 - Hút Bộ phận";
            this.btnSBOPHAN.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.btnSBOPHAN.Click += new System.EventHandler(this.btnHUT_Click);
            // 
            // btnSHAIQUAN
            // 
            this.btnSHAIQUAN.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSHAIQUAN.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSHAIQUAN.Location = new System.Drawing.Point(9, 83);
            this.btnSHAIQUAN.Name = "btnSHAIQUAN";
            this.btnSHAIQUAN.Size = new System.Drawing.Size(154, 23);
            this.btnSHAIQUAN.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSHAIQUAN.TabIndex = 16;
            this.btnSHAIQUAN.Text = "3 - Hút Hải quan";
            this.btnSHAIQUAN.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.btnSHAIQUAN.Click += new System.EventHandler(this.btnHUT_Click);
            // 
            // btnSDVT
            // 
            this.btnSDVT.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSDVT.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSDVT.Location = new System.Drawing.Point(9, 43);
            this.btnSDVT.Name = "btnSDVT";
            this.btnSDVT.Size = new System.Drawing.Size(154, 23);
            this.btnSDVT.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSDVT.TabIndex = 15;
            this.btnSDVT.Text = "2 - Hút ĐVT";
            this.btnSDVT.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.btnSDVT.Click += new System.EventHandler(this.btnHUT_Click);
            // 
            // progressBarX1
            // 
            this.progressBarX1.Anchor = System.Windows.Forms.AnchorStyles.None;
            // 
            // 
            // 
            this.progressBarX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.progressBarX1.Location = new System.Drawing.Point(300, 478);
            this.progressBarX1.Name = "progressBarX1";
            this.progressBarX1.ProgressType = DevComponents.DotNetBar.eProgressItemType.Marquee;
            this.progressBarX1.Size = new System.Drawing.Size(184, 23);
            this.progressBarX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.progressBarX1.TabIndex = 5;
            this.progressBarX1.Text = "Đang xử lý...";
            // 
            // btnSDKGH
            // 
            this.btnSDKGH.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSDKGH.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSDKGH.Location = new System.Drawing.Point(187, 43);
            this.btnSDKGH.Name = "btnSDKGH";
            this.btnSDKGH.Size = new System.Drawing.Size(154, 23);
            this.btnSDKGH.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSDKGH.TabIndex = 22;
            this.btnSDKGH.Text = "9 - Hút điều kiện giao hàng";
            this.btnSDKGH.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.btnSDKGH.Click += new System.EventHandler(this.btnHUT_Click);
            // 
            // frmHutDanhMucECUS5
            // 
            this.ClientSize = new System.Drawing.Size(784, 565);
            this.Name = "frmHutDanhMucECUS5";
            this.CONTENT.ResumeLayout(false);
            this.groupPanel1.ResumeLayout(false);
            this.pDongBo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.TextBoxX txtAccessFile;
        private LabelX labelX1;
        private ButtonX btnTimDBAccess;
        private Controls.CheckBoxX rSQL;
        private Controls.CheckBoxX rAccess;
        private LabelX labelX5;
        private Controls.TextBoxX txtSQLPass;
        private LabelX labelX4;
        private Controls.TextBoxX txtSQLUser;
        private LabelX labelX3;
        private Controls.TextBoxX txtDBName;
        private LabelX labelX2;
        private Controls.TextBoxX txtServerName;
        private ButtonX btnSNUOC;
        private Controls.GroupPanel groupPanel1;
        private System.ComponentModel.BackgroundWorker bwSieuNhan;
        private Controls.GroupPanel pDongBo;
        private ButtonX btnSDVT;
        private Controls.ProgressBarX progressBarX1;
        private ButtonX btnSHAIQUAN;
        private ButtonX btnSBOPHAN;
        private ButtonX btnSDDLK;
        private ButtonX btnSCUAKHAU;
        private ButtonX btnSCUAKHAUNN;
        private ButtonX btnSNGHTE;
        private ButtonX btnSDKGH;
    }
}
