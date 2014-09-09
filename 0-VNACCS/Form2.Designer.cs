namespace DevComponents.DotNetBar
{
    partial class Form2
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
            this.highlightBatBuoc = new DevComponents.DotNetBar.Validator.Highlighter();
            this.tTextBox1 = new DevComponents.DotNetBar.tTextBox();
            this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
            this.SuspendLayout();
            // 
            // highlightBatBuoc
            // 
            this.highlightBatBuoc.ContainerControl = this;
            this.highlightBatBuoc.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            // 
            // tTextBox1
            // 
            this.tTextBox1.BắtBuộc = false;
            // 
            // 
            // 
            this.tTextBox1.Border.Class = "TextBoxBorder";
            this.tTextBox1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.highlightBatBuoc.SetHighlightColor(this.tTextBox1, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
            this.tTextBox1.Location = new System.Drawing.Point(89, 49);
            this.tTextBox1.Name = "tTextBox1";
            this.tTextBox1.Size = new System.Drawing.Size(100, 20);
            this.superTooltip1.SetSuperTooltip(this.tTextBox1, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Dữ liệu bắt buộc", null, null, DevComponents.DotNetBar.eTooltipColor.Lemon, false, false, new System.Drawing.Size(0, 0)));
            this.tTextBox1.TabIndex = 0;
            // 
            // Form2
            // 
            this.ClientSize = new System.Drawing.Size(284, 265);
            this.Controls.Add(this.tTextBox1);
            this.DoubleBuffered = true;
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private Validator.Highlighter highlightBatBuoc;
        private tTextBox tTextBox1;
        private SuperTooltip superTooltip1;
    }
}