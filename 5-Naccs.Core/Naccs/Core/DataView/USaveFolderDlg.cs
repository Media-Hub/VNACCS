namespace Naccs.Core.DataView
{
    using Naccs.Core.Classes;
    using Naccs.Core.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class USaveFolderDlg : Form
    {
        private Button btnCancel;
        private Button btnOK;
        private Button btnSelectDlg;
        private IContainer components;
        private FolderBrowserDialog fbdSaveFolder;
        public string FName;
        private Label lblFolder;
        private Label lblName;
        public string SelectPath;
        private TextBox txbFolder;
        private TextBox txbName;

        public USaveFolderDlg()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txbFolder.Text.Trim() == "")
            {
                string message = Resources.ResourceManager.GetString("CORE25");
                using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                {
                    form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, message);
                }
                this.txbFolder.Focus();
                base.DialogResult = DialogResult.None;
            }
            else if (this.txbName.Text.Trim() == "")
            {
                string str2 = Resources.ResourceManager.GetString("CORE26");
                using (MessageDialogSimpleForm form2 = new MessageDialogSimpleForm())
                {
                    form2.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, str2);
                }
                this.txbName.Focus();
                base.DialogResult = DialogResult.None;
            }
            else
            {
                this.SelectPath = this.txbFolder.Text;
                this.FName = this.txbName.Text;
            }
        }

        private void btnSelectDlg_Click(object sender, EventArgs e)
        {
            this.fbdSaveFolder.SelectedPath = this.txbFolder.Text;
            if (this.fbdSaveFolder.ShowDialog() == DialogResult.OK)
            {
                this.txbFolder.Text = this.fbdSaveFolder.SelectedPath;
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
            this.lblFolder = new Label();
            this.lblName = new Label();
            this.txbFolder = new TextBox();
            this.btnSelectDlg = new Button();
            this.txbName = new TextBox();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.fbdSaveFolder = new FolderBrowserDialog();
            base.SuspendLayout();
//            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new Point(0x13, 0x13);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new Size(0x2f, 12);
            this.lblFolder.TabIndex = 0;
            this.lblFolder.Text = "Thư mục";
            this.lblName.AutoSize = true;
            this.lblName.Location = new Point(0x13, 0x2c);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(60, 12);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "T\x00ean tệp tin";
            this.txbFolder.Location = new Point(0x59, 0x10);
            this.txbFolder.Name = "txbFolder";
            this.txbFolder.Size = new Size(280, 0x13);
            this.txbFolder.TabIndex = 0;
            this.btnSelectDlg.Location = new Point(0x177, 14);
            this.btnSelectDlg.Name = "btnSelectDlg";
            this.btnSelectDlg.Size = new Size(0x4b, 0x17);
            this.btnSelectDlg.TabIndex = 1;
            this.btnSelectDlg.Text = "Đường dẫn";
            this.btnSelectDlg.UseVisualStyleBackColor = true;
            this.btnSelectDlg.Click += new EventHandler(this.btnSelectDlg_Click);
            this.txbName.Location = new Point(0x59, 0x29);
            this.txbName.Name = "txbName";
            this.txbName.Size = new Size(280, 0x13);
            this.txbName.TabIndex = 2;
            this.btnOK.Location = new Point(0x88, 0x45);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
//            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x102, 0x45);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            base.AcceptButton = this.btnOK;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnCancel;
            base.ClientSize = new Size(470, 0x68);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnSelectDlg);
            base.Controls.Add(this.txbName);
            base.Controls.Add(this.txbFolder);
            base.Controls.Add(this.lblName);
            base.Controls.Add(this.lblFolder);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "USaveFolderDlg";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Chọn thư mục lưu trữ c\x00e1c tệp tin ngo\x00e0i";
            base.Shown += new EventHandler(this.USaveFolderDlg_Shown);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void USaveFolderDlg_Shown(object sender, EventArgs e)
        {
            this.txbFolder.Text = this.SelectPath;
            this.txbName.Focus();
        }
    }
}

