namespace Naccs.Net.Http
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Windows.Forms;

    public class RemoteCertWarning : Form
    {
        private Button btCancel;
        private Button btCert;
        private Button btOK;
        private IContainer components;
        private Label lbGuide;
        private Label lbMessage;
        private X509Certificate2 mCert;
        private SslPolicyErrors mErrors;
        private PictureBox pictureBox1;
        private TextBox txError;

        public RemoteCertWarning(X509Certificate2 cert, SslPolicyErrors errors)
        {
            this.mCert = cert;
            this.mErrors = errors;
            this.InitializeComponent();
        }

        private void btCert_Click(object sender, EventArgs e)
        {
            if (this.mCert != null)
            {
                X509Certificate2UI.DisplayCertificate(this.mCert, base.Handle);
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

        private void InitForm()
        {
            this.lbGuide.Text = "";
            this.txError.Text = "";
            this.btCert.Enabled = this.mCert != null;
            if (this.mErrors == SslPolicyErrors.None)
            {
                this.lbMessage.Text = "There is no problem in the server certificate.";
            }
            else
            {
                if ((this.mErrors & SslPolicyErrors.RemoteCertificateChainErrors) == SslPolicyErrors.RemoteCertificateChainErrors)
                {
                    this.txError.Text = this.txError.Text + "・Certificate is not trusted or expired.\r\n\r\n";
                }
                if ((this.mErrors & SslPolicyErrors.RemoteCertificateNameMismatch) == SslPolicyErrors.RemoteCertificateNameMismatch)
                {
                    this.txError.Text = this.txError.Text + "・The name of the certificate is a mismatch between the name of the site.";
                }
                if ((this.mErrors & SslPolicyErrors.RemoteCertificateNotAvailable) == SslPolicyErrors.RemoteCertificateNotAvailable)
                {
                    this.txError.Text = this.txError.Text + "・Failure in using certificate.\r\n\r\n";
                }
                if (!string.IsNullOrEmpty(this.txError.Text))
                {
                    this.lbMessage.Text = "Server certificate is invalid. \r\nThe problems of this server certificate  is following.";
                    this.lbGuide.Text = "Do you want a server certificate is valid from the next time?";
                }
                else
                {
                    this.lbMessage.Text = "Server certificate is invalid. \r\nThis server certificate has unexpected problem.";
                    this.lbGuide.Text = "Please click [No] button.";
                    this.btOK.Visible = false;
                    this.btOK.Enabled = false;
                }
            }
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(RemoteCertWarning));
            this.btOK = new Button();
            this.btCert = new Button();
            this.lbMessage = new Label();
            this.pictureBox1 = new PictureBox();
            this.lbGuide = new Label();
            this.txError = new TextBox();
            this.btCancel = new Button();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            base.SuspendLayout();
//            this.btOK.DialogResult = DialogResult.OK;
            this.btOK.Location = new Point(0x3e, 160);
            this.btOK.Name = "btOK";
            this.btOK.Size = new Size(0x68, 0x17);
            this.btOK.TabIndex = 0;
            this.btOK.Text = "Yes";
            this.btOK.UseVisualStyleBackColor = true;
            this.btCert.Location = new Point(0x148, 160);
            this.btCert.Name = "btCert";
            this.btCert.Size = new Size(0x7d, 0x17);
            this.btCert.TabIndex = 2;
            this.btCert.Text = "Hiển thị chứng thư số";
            this.btCert.UseVisualStyleBackColor = true;
            this.btCert.Click += new EventHandler(this.btCert_Click);
            this.lbMessage.AutoSize = true;
            this.lbMessage.ImageAlign = ContentAlignment.MiddleLeft;
            this.lbMessage.Location = new Point(0x3b, 12);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new Size(0xc3, 12);
            this.lbMessage.TabIndex = 3;
            this.lbMessage.Text = "M\x00e1y chủ chứng thư số chưa ch\x00ednh x\x00e1c";
            this.lbMessage.TextAlign = ContentAlignment.MiddleLeft;
//            this.pictureBox1.Image = (Image) manager.GetObject("pictureBox1.Image");
            this.pictureBox1.Location = new Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x20, 0x20);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.lbGuide.AutoSize = true;
            this.lbGuide.ImageAlign = ContentAlignment.MiddleLeft;
            this.lbGuide.Location = new Point(0x3b, 0x7d);
            this.lbGuide.Name = "lbGuide";
            this.lbGuide.Size = new Size(0x22, 12);
            this.lbGuide.TabIndex = 5;
            this.lbGuide.Text = "Guide";
            this.lbGuide.TextAlign = ContentAlignment.MiddleLeft;
            this.txError.ForeColor = SystemColors.WindowText;
            this.txError.Location = new Point(0x3e, 0x2c);
            this.txError.Multiline = true;
            this.txError.Name = "txError";
            this.txError.ReadOnly = true;
            this.txError.Size = new Size(0x187, 0x45);
            this.txError.TabIndex = 6;
//            this.btCancel.DialogResult = DialogResult.Cancel;
            this.btCancel.Location = new Point(0xc3, 160);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new Size(0x6a, 0x17);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "No";
            this.btCancel.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btCancel;
            base.ClientSize = new Size(0x1f0, 0xc6);
            base.Controls.Add(this.btCancel);
            base.Controls.Add(this.txError);
            base.Controls.Add(this.lbGuide);
            base.Controls.Add(this.pictureBox1);
            base.Controls.Add(this.lbMessage);
            base.Controls.Add(this.btCert);
            base.Controls.Add(this.btOK);
//            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "RemoteCertWarning";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Cảnh b\x00e1o bảo mật";
            base.TopMost = true;
            base.Load += new EventHandler(this.RemoteCertWarning_Load);
            base.Shown += new EventHandler(this.RemoteCertWarning_Shown);
            ((ISupportInitialize) this.pictureBox1).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void RemoteCertWarning_Load(object sender, EventArgs e)
        {
            this.InitForm();
        }

        private void RemoteCertWarning_Shown(object sender, EventArgs e)
        {
            this.btCancel.Focus();
        }
    }
}

