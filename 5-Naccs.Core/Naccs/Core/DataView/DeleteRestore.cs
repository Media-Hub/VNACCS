namespace Naccs.Core.DataView
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class DeleteRestore : Form
    {
        private IContainer components;
        private Label lblRestoreMessage;
        public ProgressBar pgbRestore;
        private Timer tmrRestore;

        public DeleteRestore(int flag)
        {
            this.InitializeComponent();
            switch (flag)
            {
                case 0:
                    this.lblRestoreMessage.Text = "Emptying the Recycle Bin.";
                    break;

                case 1:
                    this.lblRestoreMessage.Text = "Registering the data.";
                    break;

                case 2:
                    this.lblRestoreMessage.Text = "Saving the message.";
                    break;

                case 3:
                    this.lblRestoreMessage.Text = "Registering the message list.";
                    break;

                case 4:
                    this.lblRestoreMessage.Text = "Importing the messages.";
                    break;

                case 5:
                    this.lblRestoreMessage.Text = "Obtaining the latest templates.";
                    this.pgbRestore.Visible = false;
                    break;

                case 6:
                    this.lblRestoreMessage.Text = "Recovering message list.";
                    break;

                case 7:
                    this.lblRestoreMessage.Text = "Deleting the message.";
                    break;

                case 8:
                    this.lblRestoreMessage.Text = "Undoing the message.";
                    break;

                case 9:
                    this.lblRestoreMessage.Text = "Moving the message.";
                    break;

                case 10:
                    this.lblRestoreMessage.Text = "Restoring message list.";
                    break;

                case 11:
                    this.lblRestoreMessage.Text = "Exporting the messages.";
                    break;

                case 12:
                    this.lblRestoreMessage.Text = "Assigning the message.";
                    break;
            }
            this.lblRestoreMessage.Refresh();
            this.pgbRestore.Refresh();
            Application.DoEvents();
        }

        private void CountUp()
        {
            if (this.pgbRestore.Value == this.pgbRestore.Maximum)
            {
                this.pgbRestore.Value = 0;
            }
            else
            {
                this.pgbRestore.Value++;
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
            this.components = new Container();
            this.pgbRestore = new ProgressBar();
            this.tmrRestore = new Timer(this.components);
            this.lblRestoreMessage = new Label();
            base.SuspendLayout();
            this.pgbRestore.Location = new Point(12, 0x2c);
            this.pgbRestore.Maximum = 10;
            this.pgbRestore.Name = "pgbRestore";
            this.pgbRestore.Size = new Size(0x123, 0x17);
            this.pgbRestore.TabIndex = 0;
            this.tmrRestore.Interval = 500;
            this.tmrRestore.Tick += new EventHandler(this.tmrRestore_Tick);
            this.lblRestoreMessage.Location = new Point(12, 0x12);
            this.lblRestoreMessage.Name = "lblRestoreMessage";
            this.lblRestoreMessage.Size = new Size(0x123, 12);
            this.lblRestoreMessage.TabIndex = 1;
            this.lblRestoreMessage.Text = "Emptying the Recycle Bin.";
            this.lblRestoreMessage.TextAlign = ContentAlignment.MiddleCenter;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x13b, 0x4f);
            base.ControlBox = false;
            base.Controls.Add(this.lblRestoreMessage);
            base.Controls.Add(this.pgbRestore);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "DeleteRestore";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Private Terminal Software";
            base.ResumeLayout(false);
        }

        private void tmrRestore_Tick(object sender, EventArgs e)
        {
            this.CountUp();
            this.lblRestoreMessage.Refresh();
            this.pgbRestore.Refresh();
            Application.DoEvents();
        }
    }
}

