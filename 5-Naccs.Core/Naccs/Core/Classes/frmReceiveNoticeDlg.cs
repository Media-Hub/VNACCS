namespace Naccs.Core.Classes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmReceiveNoticeDlg : Form
    {
        private Button btnOK;
        private IContainer components;
        private Label lblText;

        public frmReceiveNoticeDlg()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            base.Close();
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
            this.lblText = new Label();
            this.btnOK = new Button();
            base.SuspendLayout();
//            this.lblText.AutoSize = true;
            this.lblText.Location = new Point(0x22, 20);
            this.lblText.Name = "lblText";
            this.lblText.Size = new Size(0x92, 12);
            this.lblText.TabIndex = 0;
            this.lblText.Text = "The message was received.";
//            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x45, 0x30);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnOK;
            base.ClientSize = new Size(220, 0x53);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.lblText);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmReceiveNoticeDlg";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Private Terminal Software";
            base.TopMost = true;
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

