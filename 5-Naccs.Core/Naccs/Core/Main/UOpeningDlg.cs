namespace Naccs.Core.Main
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;

    public class UOpeningDlg : Form
    {
        private IContainer components;
        private Label lblTitle;
        private Label lblTitle2;
        private Label lblTitle3;

        public UOpeningDlg()
        {
            this.InitializeComponent();
            object[] customAttributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if ((customAttributes != null) && (customAttributes.Length > 0))
            {
                string description = ((AssemblyDescriptionAttribute) customAttributes[0]).Description;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(UOpeningDlg));
            this.lblTitle = new Label();
            this.lblTitle2 = new Label();
            this.lblTitle3 = new Label();
            base.SuspendLayout();
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = Color.Transparent;
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 26.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblTitle.ForeColor = Color.FromArgb(0xc2, 0xc9, 0xe0);
            this.lblTitle.Location = new Point(0x39, 0x33);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(0x85, 0x27);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Private";
            this.lblTitle2.AutoSize = true;
            this.lblTitle2.BackColor = Color.Transparent;
            this.lblTitle2.Font = new Font("Microsoft Sans Serif", 26.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblTitle2.ForeColor = Color.FromArgb(0xc2, 0xc9, 0xe0);
            this.lblTitle2.Location = new Point(0x5c, 90);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new Size(0xa1, 0x27);
            this.lblTitle2.TabIndex = 3;
            this.lblTitle2.Text = "Terminal";
            this.lblTitle3.AutoSize = true;
            this.lblTitle3.BackColor = Color.Transparent;
            this.lblTitle3.Font = new Font("Microsoft Sans Serif", 26.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblTitle3.ForeColor = Color.FromArgb(0xc2, 0xc9, 0xe0);
            this.lblTitle3.Location = new Point(0x8a, 0x81);
            this.lblTitle3.Name = "lblTitle3";
            this.lblTitle3.Size = new Size(0xa2, 0x27);
            this.lblTitle3.TabIndex = 4;
            this.lblTitle3.Text = "Software";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackgroundImage = (Image) manager.GetObject("$this.BackgroundImage");
            base.ClientSize = new Size(0x19c, 0xe2);
            base.ControlBox = false;
            base.Controls.Add(this.lblTitle3);
            base.Controls.Add(this.lblTitle2);
            base.Controls.Add(this.lblTitle);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "UOpeningDlg";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

