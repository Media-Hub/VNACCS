namespace Naccs.Core.Classes
{
    using Naccs.Common.Function;
    using Naccs.Core.Properties;
    using Naccs.Core.Settings;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class NetworkTestDialog : Form
    {
        private Button btClose;
        private Button btCopy;
        private IContainer components;
        private const string INTERACTIVE = "Interactive";
        private const string MAIL = "Mail";
        private string serverName;
        private TestServer testServer;
        private string trmKind;
        private TextBox txConsole;

        public NetworkTestDialog()
        {
            this.InitializeComponent();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            base.Close();
            base.Dispose();
        }

        private void btCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.txConsole.Text, true);
            string message = Resources.ResourceManager.GetString("CORE12");
            using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
            {
                form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Information, message);
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

        private void getTestServerInfo(TestServer testServer)
        {
            SystemEnvironment environment = SystemEnvironment.CreateInstance();
            switch (testServer)
            {
                case TestServer.Interactive_MainCenter:
                    this.trmKind = "Interactive";
                    this.serverName = environment.InteractiveInfo.SelectServer("MAIN").PingPoint;
                    return;

                case TestServer.Interactive_BackupCenter:
                    this.trmKind = "Interactive";
                    this.serverName = environment.InteractiveInfo.SelectServer("BACK").PingPoint;
                    return;

                case TestServer.Interactive_Test:
                    this.trmKind = "Interactive";
                    this.serverName = environment.InteractiveInfo.SelectServer("TEST").PingPoint;
                    return;

                case TestServer.netNACCS_MainNode_MainCenter:
                    this.trmKind = "Interactive";
                    this.serverName = environment.InteractiveInfo.SelectServer("MAIN-MAIN").ServerName;
                    return;

                case TestServer.netNACCS_MainNode_BackupCenter:
                    this.trmKind = "Interactive";
                    this.serverName = environment.InteractiveInfo.SelectServer("MAIN-BACK").ServerName;
                    return;

                case TestServer.netNACCS_MainNode_Test:
                    this.trmKind = "Interactive";
                    this.serverName = environment.InteractiveInfo.SelectServer("MAIN-TEST").ServerName;
                    return;

                case TestServer.netNACCS_BackupNode_MainCenter:
                    this.trmKind = "Interactive";
                    this.serverName = environment.InteractiveInfo.SelectServer("BACK-MAIN").ServerName;
                    return;

                case TestServer.netNACCS_BackupNode_BackupCenter:
                    this.trmKind = "Interactive";
                    this.serverName = environment.InteractiveInfo.SelectServer("BACK-BACK").ServerName;
                    return;

                case TestServer.netNACCS_BackupNode_Test:
                    this.trmKind = "Interactive";
                    this.serverName = environment.InteractiveInfo.SelectServer("BACK-TEST").ServerName;
                    return;

                case TestServer.Mail_MainCenter:
                    this.trmKind = "Mail";
                    this.serverName = environment.MailInfo.SelectServer("MAIN").PingPoint;
                    return;

                case TestServer.Mail_BackupCenter:
                    this.trmKind = "Mail";
                    this.serverName = environment.MailInfo.SelectServer("BACK").PingPoint;
                    return;

                case TestServer.Mail_Test:
                    this.trmKind = "Mail";
                    this.serverName = environment.MailInfo.SelectServer("TEST").PingPoint;
                    return;
            }
            using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
            {
                form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, string.Format(Resources.ResourceManager.GetString("CORE15"), testServer.ToString()));
            }
        }

        private void InitializeComponent()
        {
            this.txConsole = new TextBox();
            this.btCopy = new Button();
            this.btClose = new Button();
            base.SuspendLayout();
//            this.txConsole.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.txConsole.BackColor = SystemColors.Window;
            this.txConsole.Font = new Font("Tahoma", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x80);
            this.txConsole.Location = new Point(12, 12);
            this.txConsole.Multiline = true;
            this.txConsole.Name = "txConsole";
            this.txConsole.ReadOnly = true;
            this.txConsole.ScrollBars = ScrollBars.Both;
            this.txConsole.Size = new System.Drawing.Size(520, 0x12f);
            this.txConsole.TabIndex = 0;
            this.txConsole.TabStop = false;
            this.btCopy.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.btCopy.Enabled = false;
            this.btCopy.Location = new Point(0xa7, 0x141);
            this.btCopy.Name = "btCopy";
            this.btCopy.Size = new System.Drawing.Size(0x4b, 0x17);
            this.btCopy.TabIndex = 1;
            this.btCopy.Text = "結果をコピー";
            this.btCopy.UseVisualStyleBackColor = true;
            this.btCopy.Click += new EventHandler(this.btCopy_Click);
            this.btClose.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
//            this.btClose.DialogResult = DialogResult.OK;
            this.btClose.Enabled = false;
            this.btClose.Location = new Point(0x128, 0x141);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(0x4b, 0x17);
            this.btClose.TabIndex = 2;
            this.btClose.Text = "閉じる";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new EventHandler(this.btClose_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(0x21b, 0x161);
            base.ControlBox = false;
            base.Controls.Add(this.btClose);
            base.Controls.Add(this.btCopy);
            base.Controls.Add(this.txConsole);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "NetworkTestDialog";
            base.ShowIcon = false;
//            base.SizeGripStyle = SizeGripStyle.Show;
            this.Text = "ネットワーク接続確認";
            base.Shown += new EventHandler(this.NetworkTestDialog_Shown);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void NetworkTestControl()
        {
            string text = "";
            Cursor.Current = Cursors.WaitCursor;
            NetTestMode all = NetTestMode.All;
            switch (all)
            {
                case NetTestMode.All:
                case NetTestMode.ping:
                {
                    this.getTestServerInfo(this.testServer);
                    string trmKind = this.trmKind;
                    if (trmKind == null)
                    {
                        goto Label_00DC;
                    }
                    if (!(trmKind == "Interactive"))
                    {
                        if (trmKind == "Mail")
                        {
                            this.txConsole.AppendText("＃コマンド発行：");
                            text = DosCommand.doCommand("ping " + this.serverName);
                            this.txConsole.AppendText(text);
                            this.txConsole.AppendText("\n");
                            break;
                        }
                        goto Label_00DC;
                    }
                    this.txConsole.AppendText("＃コマンド発行：");
                    text = DosCommand.doCommand("ping " + this.serverName);
                    this.txConsole.AppendText(text);
                    this.txConsole.AppendText("\n");
                    break;
                }
            }
            goto Label_0111;
        Label_00DC:
            using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
            {
                form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, string.Format(Resources.ResourceManager.GetString("CORE16"), this.trmKind));
            }
        Label_0111:
            if ((all == NetTestMode.All) || (all == NetTestMode.ipconfig))
            {
                this.txConsole.AppendText("＃コマンド発行：");
                text = DosCommand.doCommand("ipconfig /all");
                this.txConsole.AppendText(text);
                this.txConsole.AppendText("\n");
            }
            Cursor.Current = Cursors.Default;
        }

        private void NetworkTestDialog_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            this.NetworkTestControl();
            this.btCopy.Enabled = true;
            this.btClose.Enabled = true;
            base.ControlBox = true;
        }

        public DialogResult ShowDialog(TestServer testServer)
        {
            this.testServer = testServer;
            return base.ShowDialog();
        }
    }
}

