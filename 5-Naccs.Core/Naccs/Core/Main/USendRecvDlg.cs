namespace Naccs.Core.Main
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class USendRecvDlg : Form
    {
        public Button btnCancel;
        private IContainer components;
        public Label lblSendJobCode;
        public Label lblSendStatus;
        private ProgressBar pgbSendRecv;
        private Panel pnlProgress;
        private System.Windows.Forms.Timer tmrSendRecv;

        public event OnAbortHandler OnAbort;

        public USendRecvDlg()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.OnAbort();
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
            this.components = new Container();
            this.pnlProgress = new Panel();
            this.pgbSendRecv = new ProgressBar();
            this.btnCancel = new Button();
            this.lblSendJobCode = new Label();
            this.lblSendStatus = new Label();
            this.tmrSendRecv = new System.Windows.Forms.Timer(this.components);
            this.pnlProgress.SuspendLayout();
            base.SuspendLayout();
            this.pnlProgress.Controls.Add(this.pgbSendRecv);
            this.pnlProgress.Dock = DockStyle.Bottom;
            this.pnlProgress.Location = new Point(0, 0x70);
            this.pnlProgress.Name = "pnlProgress";
            this.pnlProgress.Size = new Size(0x106, 0x15);
            this.pnlProgress.TabIndex = 1;
            this.pgbSendRecv.Dock = DockStyle.Fill;
            this.pgbSendRecv.Location = new Point(0, 0);
            this.pgbSendRecv.Maximum = 20;
            this.pgbSendRecv.Name = "pgbSendRecv";
            this.pgbSendRecv.Size = new Size(0x106, 0x15);
            this.pgbSendRecv.TabIndex = 1;
            this.btnCancel.Location = new Point(0x5d, 0x3f);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.lblSendJobCode.Location = new Point(12, 0x25);
            this.lblSendJobCode.Name = "lblSendJobCode";
            this.lblSendJobCode.Size = new Size(0xee, 0x17);
            this.lblSendJobCode.TabIndex = 3;
            this.lblSendJobCode.Text = "IDA01.S01";
            this.lblSendJobCode.TextAlign = ContentAlignment.MiddleCenter;
            this.lblSendStatus.Location = new Point(0x52, 14);
            this.lblSendStatus.Name = "lblSendStatus";
            this.lblSendStatus.Size = new Size(100, 0x17);
            this.lblSendStatus.TabIndex = 4;
            this.lblSendStatus.Text = "Connecting...";
            this.lblSendStatus.TextAlign = ContentAlignment.MiddleCenter;
            this.tmrSendRecv.Interval = 300;
            this.tmrSendRecv.Tick += new EventHandler(this.tmrSendRecv_Tick);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x106, 0x85);
            base.ControlBox = false;
            base.Controls.Add(this.lblSendStatus);
            base.Controls.Add(this.lblSendJobCode);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.pnlProgress);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "USendRecvDlg";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Private Terminal Software";
            base.TopMost = true;
            base.Shown += new EventHandler(this.USendRecvDlg_Shown);
            base.VisibleChanged += new EventHandler(this.USendRecvDlg_VisibleChanged);
            this.pnlProgress.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void tmrSendRecv_Tick(object sender, EventArgs e)
        {
            if (this.pgbSendRecv.Value == this.pgbSendRecv.Maximum)
            {
                this.pgbSendRecv.Value = 0;
            }
            else
            {
                this.pgbSendRecv.Value++;
            }
        }

        private void USendRecvDlg_Shown(object sender, EventArgs e)
        {
            this.lblSendStatus.Focus();
        }

        private void USendRecvDlg_VisibleChanged(object sender, EventArgs e)
        {
            if (base.Visible)
            {
                this.pgbSendRecv.Value = 0;
                this.tmrSendRecv.Enabled = true;
            }
            else
            {
                this.tmrSendRecv.Enabled = false;
            }
        }
    }
}

