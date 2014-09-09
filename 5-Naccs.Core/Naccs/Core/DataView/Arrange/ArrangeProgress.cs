namespace Naccs.Core.DataView.Arrange
{
    using Naccs.Core.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class ArrangeProgress : Form
    {
        private Button btnCancel;
        private const int cnDisplayPacent = 5;
        private IContainer components;
        private Label lbMessage;
        private ProgressBar pgbArrenge;

        public event EventHandler CancelEvent;

        public ArrangeProgress(ProgresType type)
        {
            this.InitializeComponent();
            this.SetMessage(type);
        }

        private void ArrangeProgress_Shown(object sender, EventArgs e)
        {
            this.lbMessage.Focus();
            this.lbMessage.Update();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.CancelEvent != null)
            {
                this.btnCancel.Enabled = false;
                this.CancelEvent(sender, e);
                this.btnCancel.Enabled = true;
            }
        }

        public static int DisplayPercentage(int maxCount, int nowCount)
        {
            int num = (nowCount * 100) / maxCount;
            return ((num / 5) * 5);
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
            this.pgbArrenge = new ProgressBar();
            this.lbMessage = new Label();
            this.btnCancel = new Button();
            base.SuspendLayout();
//            this.pgbArrenge.Dock = DockStyle.Bottom;
            this.pgbArrenge.Location = new Point(0, 110);
            this.pgbArrenge.Name = "pgbArrenge";
            this.pgbArrenge.Size = new Size(0x144, 0x17);
            this.pgbArrenge.Step = 1;
            this.pgbArrenge.TabIndex = 0;
            this.lbMessage.Location = new Point(2, 14);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new Size(0x13e, 0x17);
            this.lbMessage.TabIndex = 1;
            this.lbMessage.Text = "Please wait…";
            this.lbMessage.TextAlign = ContentAlignment.MiddleCenter;
            this.btnCancel.Location = new Point(0x87, 0x3f);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(60, 0x17);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x144, 0x85);
            base.ControlBox = false;
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.lbMessage);
            base.Controls.Add(this.pgbArrenge);
//            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ArrangeProgress";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Private Terminal Software";
            base.TopMost = true;
            base.Shown += new EventHandler(this.ArrangeProgress_Shown);
            base.ResumeLayout(false);
        }

        private void SetMessage(ProgresType type)
        {
            this.SetMessage(Resources.ResourceManager.GetString("CORE82"));
        }

        public void SetMessage(string msg)
        {
            this.lbMessage.Text = msg;
        }

        public bool CanCancel
        {
            set
            {
                this.btnCancel.Visible = value;
            }
        }

        public int Percentage
        {
            get
            {
                return this.pgbArrenge.Value;
            }
            set
            {
                this.pgbArrenge.Value = value;
                this.pgbArrenge.Update();
            }
        }

        public enum ProgresType
        {
            Remove,
            Revert,
            NoType
        }
    }
}

