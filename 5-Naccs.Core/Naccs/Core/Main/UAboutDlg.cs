namespace Naccs.Core.Main
{
    using Naccs.Core.Settings;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Xml;

    public class UAboutDlg : Form
    {
        private const string AirConfFile = "AirVersionInfo.conf";
        private const string AirSeaConfFile = "ComVersionInfo.conf";
        private Button btnOK;
        private const string C_Download = @"Download\Version.cfg";
        private IContainer components;
        private int iconflag;
        private ImageList imlIcon;
        private const string JetrasConfFile = "JetrasVersionInfo.conf";
        private Label lblComp;
        private Label lblExeDLVersion;
        private Label lblExp;
        private Label lblTemplateDLVersion;
        private Label lblVersion;
        private PictureBox pcbIcon;
        private const string ServerSelectNodeKey = "DownloadSettings/Server";
        private TextBox txbExeDLVersion;
        private TextBox txbExp;
        private TextBox txbName;
        private TextBox txbTemplateDLVersion;
        private TextBox txbVersion;
        private const string UserSelectNodeKey = "DownloadSettings/User";

        public UAboutDlg(int flag)
        {
            this.InitializeComponent();
            this.iconflag = flag;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private string GetUserType()
        {
            string str = "";
            try
            {
                string path = Path.Combine(PathInfo.CreateInstance().InstallPath, @"Download\Version.cfg");
                if (!File.Exists(path))
                {
                    return str;
                }
                XmlDocument document = new XmlDocument();
                document.Load(path);
                str = "Sea";
                XmlElement element = (XmlElement) document.SelectSingleNode("DownloadSettings/User");
                if (element != null)
                {
                    return element.GetAttribute("type");
                }
                XmlElement element2 = (XmlElement) document.SelectSingleNode("DownloadSettings/Server");
                if (element2 == null)
                {
                    return str;
                }
                string fileName = Path.GetFileName(element2.GetAttribute("uri"));
                if (fileName.Equals("ComVersionInfo.conf", StringComparison.CurrentCultureIgnoreCase) || fileName.Equals("JetrasVersionInfo.conf", StringComparison.CurrentCultureIgnoreCase))
                {
                    return "Air/Sea";
                }
                if (fileName.Equals("AirVersionInfo.conf", StringComparison.CurrentCultureIgnoreCase))
                {
                    str = "Air";
                }
            }
            catch
            {
            }
            return str;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(UAboutDlg));
            this.btnOK = new Button();
            this.lblComp = new Label();
            this.lblVersion = new Label();
            this.lblExp = new Label();
            this.pcbIcon = new PictureBox();
            this.txbName = new TextBox();
            this.txbVersion = new TextBox();
            this.txbExp = new TextBox();
            this.imlIcon = new ImageList(this.components);
            this.txbTemplateDLVersion = new TextBox();
            this.txbExeDLVersion = new TextBox();
            this.lblTemplateDLVersion = new Label();
            this.lblExeDLVersion = new Label();
            ((ISupportInitialize) this.pcbIcon).BeginInit();
            base.SuspendLayout();
            this.btnOK.DialogResult = DialogResult.Cancel;
            this.btnOK.Location = new Point(0xae, 0x8e);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.lblComp.AutoSize = true;
            this.lblComp.Location = new Point(0x45, 15);
            this.lblComp.Name = "lblComp";
            this.lblComp.Size = new Size(0x31, 12);
            this.lblComp.TabIndex = 1;
            this.lblComp.Text = "Hệ thống";
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new Point(0x45, 0x27);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new Size(0x36, 12);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "Phi\x00ean bản";
            this.lblExp.AutoSize = true;
            this.lblExp.Location = new Point(0x45, 0x71);
            this.lblExp.Name = "lblExp";
            this.lblExp.Size = new Size(0x2c, 12);
            this.lblExp.TabIndex = 4;
            this.lblExp.Text = "Ghi ch\x00fa";
            this.pcbIcon.ErrorImage = null;
            this.pcbIcon.Image = (Image) manager.GetObject("pcbIcon.Image");
            this.pcbIcon.Location = new Point(0x16, 0x10);
            this.pcbIcon.Name = "pcbIcon";
            this.pcbIcon.Size = new Size(0x20, 0x20);
            this.pcbIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pcbIcon.TabIndex = 5;
            this.pcbIcon.TabStop = false;
            this.txbName.Location = new Point(0xcd, 12);
            this.txbName.Name = "txbName";
            this.txbName.ReadOnly = true;
            this.txbName.Size = new Size(0xcc, 0x13);
            this.txbName.TabIndex = 6;
            this.txbVersion.Location = new Point(0xcd, 0x24);
            this.txbVersion.Name = "txbVersion";
            this.txbVersion.ReadOnly = true;
            this.txbVersion.Size = new Size(0xcc, 0x13);
            this.txbVersion.TabIndex = 8;
            this.txbExp.Location = new Point(0xcd, 110);
            this.txbExp.Name = "txbExp";
            this.txbExp.ReadOnly = true;
            this.txbExp.Size = new Size(0xcc, 0x13);
            this.txbExp.TabIndex = 10;
            this.imlIcon.ImageStream = (ImageListStreamer) manager.GetObject("imlIcon.ImageStream");
            this.imlIcon.TransparentColor = Color.Transparent;
            this.imlIcon.Images.SetKeyName(0, "NaccsInteractive.ico");
            this.imlIcon.Images.SetKeyName(1, "NaccsMail.ico");
            this.imlIcon.Images.SetKeyName(2, "netNaccs.ico");
            this.imlIcon.Images.SetKeyName(3, "Kiosk.ico");
            this.imlIcon.Images.SetKeyName(4, "NaccsTraining.ico");
            this.txbTemplateDLVersion.Location = new Point(0xcd, 0x55);
            this.txbTemplateDLVersion.Name = "txbTemplateDLVersion";
            this.txbTemplateDLVersion.ReadOnly = true;
            this.txbTemplateDLVersion.Size = new Size(0xcc, 0x13);
            this.txbTemplateDLVersion.TabIndex = 14;
            this.txbExeDLVersion.Location = new Point(0xcd, 0x3d);
            this.txbExeDLVersion.Name = "txbExeDLVersion";
            this.txbExeDLVersion.ReadOnly = true;
            this.txbExeDLVersion.Size = new Size(0xcc, 0x13);
            this.txbExeDLVersion.TabIndex = 13;
            this.lblTemplateDLVersion.AutoSize = true;
            this.lblTemplateDLVersion.Location = new Point(0x45, 0x58);
            this.lblTemplateDLVersion.Name = "lblTemplateDLVersion";
            this.lblTemplateDLVersion.Size = new Size(0x19, 12);
            this.lblTemplateDLVersion.TabIndex = 12;
            this.lblTemplateDLVersion.Text = "Mẫu";
            this.lblExeDLVersion.AutoSize = true;
            this.lblExeDLVersion.Location = new Point(0x45, 0x40);
            this.lblExeDLVersion.Name = "lblExeDLVersion";
            this.lblExeDLVersion.Size = new Size(0x38, 12);
            this.lblExeDLVersion.TabIndex = 11;
            this.lblExeDLVersion.Text = "Phần mềm";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnOK;
            base.ClientSize = new Size(0x1af, 0xb1);
            base.Controls.Add(this.txbTemplateDLVersion);
            base.Controls.Add(this.txbExeDLVersion);
            base.Controls.Add(this.lblTemplateDLVersion);
            base.Controls.Add(this.lblExeDLVersion);
            base.Controls.Add(this.txbExp);
            base.Controls.Add(this.txbVersion);
            base.Controls.Add(this.txbName);
            base.Controls.Add(this.pcbIcon);
            base.Controls.Add(this.lblExp);
            base.Controls.Add(this.lblVersion);
            base.Controls.Add(this.lblComp);
            base.Controls.Add(this.btnOK);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "UAboutDlg";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Th\x00f4ng tin phi\x00ean bản";
            base.Load += new EventHandler(this.UAboutDlg_Load);
            ((ISupportInitialize) this.pcbIcon).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void UAboutDlg_Load(object sender, EventArgs e)
        {
            string productVersion = Application.ProductVersion;
            string productName = Application.ProductName;
            string companyName = Application.CompanyName;
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            object[] customAttributes = entryAssembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            if ((customAttributes != null) && (customAttributes.Length > 0))
            {
                string copyright = ((AssemblyCopyrightAttribute) customAttributes[0]).Copyright;
            }
            string description = "-";
            object[] objArray2 = entryAssembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if ((objArray2 != null) && (objArray2.Length > 0))
            {
                description = ((AssemblyDescriptionAttribute) objArray2[0]).Description;
            }
            this.txbName.Text = productName;
            this.txbVersion.Text = productVersion;
            this.txbExp.Text = description;
            VersionSettings settings = new VersionSettings(false);
            this.txbExeDLVersion.Text = settings.Application.ToString().PadLeft(4, '0');
            this.txbTemplateDLVersion.Text = settings.Template.ToString().PadLeft(4, '0');
        }
    }
}

