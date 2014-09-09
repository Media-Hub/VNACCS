namespace Naccs.Core.Option
{
    using Naccs.Core.Settings;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class dlgFolderSetting : Form
    {
        private Button btnFsCncl;
        private Button btnFsOK;
        private IContainer components;
        private ImageList imlFolderSetting;
        private bool mBankWithdrawday;
        private string mFolderName;
        private const string mRootFolderName = "VNACCS";
        private TreeView trvFsFolderTree;
        private TextBox txbFolderName;

        public dlgFolderSetting()
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

        private void FolderAddSet(string FolderStr)
        {
            string str2;
            string str = FolderStr;
            TreeNode topNode = this.trvFsFolderTree.TopNode;
            int index = str.IndexOf(@"\");
            if (index > -1)
            {
                str2 = str.Substring(0, index);
                str = str.Remove(0, index + 1);
            }
            else
            {
                str2 = str;
                str = "";
            }
            while (str != "")
            {
                bool flag = false;
                index = str.IndexOf(@"\");
                if (index > -1)
                {
                    str2 = str.Substring(0, index);
                    str = str.Remove(0, index + 1);
                }
                else
                {
                    str2 = str;
                    str = "";
                }
                for (int i = 0; i < topNode.Nodes.Count; i++)
                {
                    if (topNode.Nodes[i].Text == str2)
                    {
                        topNode = topNode.Nodes[i];
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    topNode = topNode.Nodes.Add(str2);
                    topNode.ImageIndex = 0;
                    topNode.SelectedImageIndex = 0;
                }
            }
        }

        private void FolderSet(StringList FldList)
        {
            for (int i = 0; i < FldList.Count; i++)
            {
                this.FolderAddSet(FldList[i]);
            }
        }

        private void FolderSetting_Load(object sender, EventArgs e)
        {
            this.trvFsFolderTree.ImageList = this.imlFolderSetting;
            TreeNode node = new TreeNode("VNACCS", 0, 0);
            this.trvFsFolderTree.Nodes.Add(node);
            MessageClassify classify = MessageClassify.CreateInstance();
            this.FolderSet(classify.Folders);
            this.trvFsFolderTree.ExpandAll();
            this.trvFsFolderTree.Select();
            this.trvFsFolderTree.SelectedNode = this.trvFsFolderTree.Nodes[0];
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(dlgFolderSetting));
            this.btnFsOK = new Button();
            this.btnFsCncl = new Button();
            this.trvFsFolderTree = new TreeView();
            this.txbFolderName = new TextBox();
            this.imlFolderSetting = new ImageList(this.components);
            base.SuspendLayout();
            this.btnFsOK.DialogResult = DialogResult.OK;
            this.btnFsOK.Enabled = false;
            this.btnFsOK.Location = new Point(0x109, 30);
            this.btnFsOK.Name = "btnFsOK";
            this.btnFsOK.Size = new Size(0x4b, 0x17);
            this.btnFsOK.TabIndex = 0;
            this.btnFsOK.Text = "OK";
            this.btnFsOK.UseVisualStyleBackColor = true;
            this.btnFsCncl.DialogResult = DialogResult.Cancel;
            this.btnFsCncl.Location = new Point(0x109, 0x3b);
            this.btnFsCncl.Name = "btnFsCncl";
            this.btnFsCncl.Size = new Size(0x4b, 0x17);
            this.btnFsCncl.TabIndex = 1;
            this.btnFsCncl.Text = "Cancel";
            this.btnFsCncl.UseVisualStyleBackColor = true;
            this.trvFsFolderTree.HideSelection = false;
            this.trvFsFolderTree.Location = new Point(12, 30);
            this.trvFsFolderTree.Name = "trvFsFolderTree";
            this.trvFsFolderTree.Size = new Size(0xe3, 0xe7);
            this.trvFsFolderTree.TabIndex = 2;
            this.trvFsFolderTree.AfterSelect += new TreeViewEventHandler(this.trvFsFolderTree_AfterSelect);
            this.txbFolderName.BorderStyle = BorderStyle.None;
            this.txbFolderName.Location = new Point(12, 12);
            this.txbFolderName.MaxLength = 0x100;
            this.txbFolderName.Name = "txbFolderName";
            this.txbFolderName.ReadOnly = true;
            this.txbFolderName.Size = new Size(0x10c, 12);
            this.txbFolderName.TabIndex = 3;
            this.txbFolderName.TabStop = false;
            this.imlFolderSetting.ImageStream = (ImageListStreamer) manager.GetObject("imlFolderSetting.ImageStream");
            this.imlFolderSetting.TransparentColor = Color.Transparent;
            this.imlFolderSetting.Images.SetKeyName(0, "CLSDFOLD.ICO");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnFsCncl;
            base.ClientSize = new Size(0x16b, 0x111);
            base.Controls.Add(this.txbFolderName);
            base.Controls.Add(this.trvFsFolderTree);
            base.Controls.Add(this.btnFsCncl);
            base.Controls.Add(this.btnFsOK);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "dlgFolderSetting";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Chọn thư mục tin nhắn";
            base.TopMost = true;
            base.Load += new EventHandler(this.FolderSetting_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void trvFsFolderTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.txbFolderName.Text = this.trvFsFolderTree.SelectedNode.FullPath;
            this.mFolderName = this.txbFolderName.Text;
            this.btnFsOK.Enabled = false;
            if (this.BankWithdrawday)
            {
                if ((this.trvFsFolderTree.SelectedNode.Level > 1) && (this.trvFsFolderTree.SelectedNode.Level < 4))
                {
                    this.btnFsOK.Enabled = true;
                }
            }
            else if (this.trvFsFolderTree.SelectedNode.Level > 1)
            {
                this.btnFsOK.Enabled = true;
            }
        }

        public bool BankWithdrawday
        {
            get
            {
                return this.mBankWithdrawday;
            }
            set
            {
                this.mBankWithdrawday = value;
            }
        }

        public string FolderName
        {
            get
            {
                return this.mFolderName;
            }
            set
            {
                this.mFolderName = value;
            }
        }
    }
}

