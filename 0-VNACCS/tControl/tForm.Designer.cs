namespace DevComponents.DotNetBar
{
    partial class tForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tForm));
            this.TITLE = new DevComponents.DotNetBar.LabelX();
            this.CONTENT = new DevComponents.DotNetBar.PanelEx();
            this.pBOTTOM = new DevComponents.DotNetBar.PanelEx();
            this.SuspendLayout();
            // 
            // TITLE
            // 
            this.TITLE.BackColor = System.Drawing.Color.Green;
            // 
            // 
            // 
            this.TITLE.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.TITLE.Dock = System.Windows.Forms.DockStyle.Top;
            this.TITLE.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TITLE.ForeColor = System.Drawing.Color.White;
            this.TITLE.Location = new System.Drawing.Point(0, 0);
            this.TITLE.Name = "TITLE";
            this.TITLE.Size = new System.Drawing.Size(784, 30);
            this.TITLE.TabIndex = 0;
            this.TITLE.Text = "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM";
            this.TITLE.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // CONTENT
            // 
            this.CONTENT.CanvasColor = System.Drawing.SystemColors.Control;
            this.CONTENT.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.CONTENT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CONTENT.Location = new System.Drawing.Point(0, 30);
            this.CONTENT.Name = "CONTENT";
            this.CONTENT.Padding = new System.Windows.Forms.Padding(2);
            this.CONTENT.Size = new System.Drawing.Size(784, 503);
            this.CONTENT.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.CONTENT.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.CONTENT.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.CONTENT.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.CONTENT.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.CONTENT.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.CONTENT.Style.GradientAngle = 90;
            this.CONTENT.TabIndex = 2;
            // 
            // pBOTTOM
            // 
            this.pBOTTOM.CanvasColor = System.Drawing.SystemColors.Control;
            this.pBOTTOM.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pBOTTOM.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBOTTOM.Location = new System.Drawing.Point(0, 533);
            this.pBOTTOM.Name = "pBOTTOM";
            this.pBOTTOM.Padding = new System.Windows.Forms.Padding(2);
            this.pBOTTOM.Size = new System.Drawing.Size(784, 32);
            this.pBOTTOM.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pBOTTOM.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pBOTTOM.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pBOTTOM.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pBOTTOM.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pBOTTOM.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pBOTTOM.Style.GradientAngle = 90;
            this.pBOTTOM.TabIndex = 3;
            // 
            // tForm
            // 
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(784, 565);
            this.Controls.Add(this.CONTENT);
            this.Controls.Add(this.pBOTTOM);
            this.Controls.Add(this.TITLE);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "tForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.tForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        public LabelX TITLE;
        public PanelEx CONTENT;
        public PanelEx pBOTTOM;

    }
}
