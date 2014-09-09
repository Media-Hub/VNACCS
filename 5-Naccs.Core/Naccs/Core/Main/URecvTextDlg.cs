namespace Naccs.Core.Main
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class URecvTextDlg : Form
    {
        private Button btnClose;
        private IContainer components;
        private Panel pnlbuttom;
        public TextBox txbRecvText;

        public URecvTextDlg()
        {
            this.InitializeComponent();
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
            this.pnlbuttom = new Panel();
            this.txbRecvText = new TextBox();
            this.btnClose = new Button();
            this.pnlbuttom.SuspendLayout();
            base.SuspendLayout();
            this.pnlbuttom.Controls.Add(this.btnClose);
            this.pnlbuttom.Dock = DockStyle.Bottom;
            this.pnlbuttom.Location = new Point(0, 0x187);
            this.pnlbuttom.Name = "pnlbuttom";
            this.pnlbuttom.Size = new Size(0x1fb, 0x2d);
            this.pnlbuttom.TabIndex = 0;
            this.txbRecvText.Dock = DockStyle.Fill;
            this.txbRecvText.Location = new Point(0, 0);
            this.txbRecvText.Multiline = true;
            this.txbRecvText.Name = "txbRecvText";
            this.txbRecvText.Size = new Size(0x1fb, 0x187);
            this.txbRecvText.TabIndex = 1;
            this.btnClose.DialogResult = DialogResult.OK;
            this.btnClose.Location = new Point(0xcb, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x4b, 0x17);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1fb, 0x1b4);
            base.Controls.Add(this.txbRecvText);
            base.Controls.Add(this.pnlbuttom);
            base.Name = "URecvTextDlg";
            base.ShowIcon = false;
            this.Text = "受信電文テキスト表示";
            this.pnlbuttom.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

