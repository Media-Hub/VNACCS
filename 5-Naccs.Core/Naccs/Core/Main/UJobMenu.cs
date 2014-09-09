namespace Naccs.Core.Main
{
    using Naccs.Common.Function;
    using Naccs.Core.Classes;
    using Naccs.Core.Properties;
    using Naccs.Core.Settings;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;
    using System.Deployment.Application;

    public class UJobMenu : UserControl
    {
        private ColumnHeader clhJobCode;
        private ColumnHeader clhJobKey;
        public List<ListRec> CodeList;
        private IContainer components;
        private string DCode;
        private FileSave fs;
        public List<ListRec> GeneralAppList;
        private ImageList imlJobMenu;
        private bool isDesigning = ((AppDomain.CurrentDomain.FriendlyName == "DefaultDomain") && !ApplicationDeployment.IsNetworkDeployed);
        private string JCode;
        private Label lblCode;
        private Label lblFolderOpen;
        private Label lblGeneralApp;
        private Label lblHistoryJob;
        private Label lblJobKey;
        private Label lblJobMenu;
        private ListBox lsbCode;
        private ListBox lsbFolderOpen;
        private ListBox lsbGeneralApp;
        private ListBox lsbHistoryJob;
        private ListView lsvJobKey;
        private PathInfo pi;
        private Panel pnlCode;
        private Panel pnlFolderOpen;
        private Panel pnlGeneralApp;
        private Panel pnlHistoryJob;
        private Panel pnlJobKey;
        public Panel pnlJobMenu;
        private TabPage tbpCode;
        private TabPage tbpFolderOpen;
        private TabPage tbpGeneralApp;
        private TabPage tbpHistoryJob;
        private TabPage tbpJobKey;
        private TabPage tbpJobMenu;
        public TabControl tcJobOpenWindow;
        public TreeView trvJobMenu;

        public event JobOpenMenuHandler OnJobOpen;

        public UJobMenu()
        {
            this.InitializeComponent();
        }

        public void CodeSet()
        {
            this.lsbCode.Items.Clear();
            this.CodeList = new List<ListRec>();
            try
            {
                StreamReader reader = new StreamReader(this.pi.CodeList, Encoding.UTF8);
                try
                {
                    while (!reader.EndOfStream)
                    {
                        List<string> list = CommaText.CommaTextToItems(reader.ReadLine());
                        if (list.Count > 0)
                        {
                            ListRec item = new ListRec {
                                Name = list[0]
                            };
                            if (list.Count > 1)
                            {
                                item.URL = list[1];
                            }
                            else
                            {
                                item.URL = "";
                            }
                            this.CodeList.Add(item);
                        }
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            catch
            {
            }
            for (int i = 0; i < this.CodeList.Count; i++)
            {
                this.lsbCode.Items.Add(this.CodeList[i].Name);
            }
        }

        public void DebugSet(bool DebugFlag)
        {
            if (!DebugFlag)
            {
                this.lsbFolderOpen.Items.RemoveAt(this.lsbFolderOpen.Items.Count - 1);
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

        public string Find(string GeneralApp)
        {
            for (int i = 0; i < this.GeneralAppList.Count; i++)
            {
                if (this.GeneralAppList[i].Name == GeneralApp)
                {
                    return this.GeneralAppList[i].URL;
                }
            }
            return null;
        }

        public void GeneralAppSet()
        {
            this.lsbGeneralApp.Items.Clear();
            this.GeneralAppList = new List<ListRec>();
            try
            {
                StreamReader reader = new StreamReader(this.pi.GeneralApp, Encoding.UTF8);
                try
                {
                    while (!reader.EndOfStream)
                    {
                        List<string> list = CommaText.CommaTextToItems(reader.ReadLine());
                        if (list.Count > 0)
                        {
                            ListRec item = new ListRec {
                                Name = list[0]
                            };
                            if (list.Count > 1)
                            {
                                item.URL = list[1];
                            }
                            else
                            {
                                item.URL = "";
                            }
                            this.GeneralAppList.Add(item);
                        }
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            catch
            {
            }
            for (int i = 0; i < this.GeneralAppList.Count; i++)
            {
                this.lsbGeneralApp.Items.Add(this.GeneralAppList[i].Name);
            }
        }

        public void HistoryJobListSet(StringList jl)
        {
            this.lsbHistoryJob.Items.Clear();
            for (int i = jl.Count - 1; i > -1; i--)
            {
                this.lsbHistoryJob.Items.Add(jl[i]);
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ListViewItem item = new ListViewItem(new string[] { "ECR", "F11" }, -1);
            ComponentResourceManager manager = new ComponentResourceManager(typeof(UJobMenu));
            this.tbpFolderOpen = new TabPage();
            this.lsbFolderOpen = new ListBox();
            this.pnlFolderOpen = new Panel();
            this.lblFolderOpen = new Label();
            this.tbpJobKey = new TabPage();
            this.lsvJobKey = new ListView();
            this.clhJobCode = new ColumnHeader();
            this.clhJobKey = new ColumnHeader();
            this.pnlJobKey = new Panel();
            this.lblJobKey = new Label();
            this.tbpHistoryJob = new TabPage();
            this.lsbHistoryJob = new ListBox();
            this.pnlHistoryJob = new Panel();
            this.lblHistoryJob = new Label();
            this.tbpJobMenu = new TabPage();
            this.trvJobMenu = new TreeView();
            this.pnlJobMenu = new Panel();
            this.lblJobMenu = new Label();
            this.tcJobOpenWindow = new TabControl();
            this.tbpGeneralApp = new TabPage();
            this.lsbGeneralApp = new ListBox();
            this.pnlGeneralApp = new Panel();
            this.lblGeneralApp = new Label();
            this.tbpCode = new TabPage();
            this.lsbCode = new ListBox();
            this.pnlCode = new Panel();
            this.lblCode = new Label();
            this.imlJobMenu = new ImageList(this.components);
            this.tbpFolderOpen.SuspendLayout();
            this.pnlFolderOpen.SuspendLayout();
            this.tbpJobKey.SuspendLayout();
            this.pnlJobKey.SuspendLayout();
            this.tbpHistoryJob.SuspendLayout();
            this.pnlHistoryJob.SuspendLayout();
            this.tbpJobMenu.SuspendLayout();
            this.pnlJobMenu.SuspendLayout();
            this.tcJobOpenWindow.SuspendLayout();
            this.tbpGeneralApp.SuspendLayout();
            this.pnlGeneralApp.SuspendLayout();
            this.tbpCode.SuspendLayout();
            this.pnlCode.SuspendLayout();
            base.SuspendLayout();
            this.tbpFolderOpen.Controls.Add(this.lsbFolderOpen);
            this.tbpFolderOpen.Controls.Add(this.pnlFolderOpen);
            this.tbpFolderOpen.ImageIndex = 1;
            this.tbpFolderOpen.Location = new Point(4, 0x76);
            this.tbpFolderOpen.Name = "tbpFolderOpen";
            this.tbpFolderOpen.Padding = new Padding(3);
            this.tbpFolderOpen.Size = new System.Drawing.Size(0xd7, 0x126);
            this.tbpFolderOpen.TabIndex = 4;
            this.tbpFolderOpen.Text = "Thư mục t\x00e0i liệu tham khảo";
            this.tbpFolderOpen.UseVisualStyleBackColor = true;
            this.lsbFolderOpen.Cursor = Cursors.Hand;
            this.lsbFolderOpen.Dock = DockStyle.Fill;
            this.lsbFolderOpen.DrawMode = DrawMode.OwnerDrawFixed;
            this.lsbFolderOpen.ForeColor = Color.RoyalBlue;
            this.lsbFolderOpen.FormattingEnabled = true;
            this.lsbFolderOpen.HorizontalScrollbar = true;
            this.lsbFolderOpen.IntegralHeight = false;
            this.lsbFolderOpen.ItemHeight = 20;
            this.lsbFolderOpen.Items.AddRange(new object[] { "Th\x00f4ng tin Log", "Th\x00f4ng điệp th\x00f4ng b\x00e1o d\x00e0nh cho m\x00e0n h\x00ecnh", "Th\x00f4ng điệp th\x00f4ng b\x00e1o d\x00e0nh cho m\x00e0n h\x00ecnh chứa th\x00f4ng tin kết quả xử l\x00fd", "Th\x00f4ng điệp d\x00e0nh cho b\x00e1o c\x00e1o", "Th\x00f4ng điệp th\x00f4ng b\x00e1o kết quả xử l\x00fd", "Thư mục lưu trữ mặc định cho c\x00e1c tệp tin gửi đi" });
            this.lsbFolderOpen.Location = new Point(3, 0x1d);
            this.lsbFolderOpen.Name = "lsbFolderOpen";
            this.lsbFolderOpen.Size = new System.Drawing.Size(0xd1, 0x106);
            this.lsbFolderOpen.TabIndex = 4;
            this.lsbFolderOpen.MouseClick += new MouseEventHandler(this.lsbFolderOpen_MouseClick);
            this.lsbFolderOpen.DrawItem += new DrawItemEventHandler(this.lsb_DrawItem);
            this.lsbFolderOpen.KeyDown += new KeyEventHandler(this.lsbFolderOpen_KeyDown);
            this.pnlFolderOpen.BackColor = SystemColors.GradientInactiveCaption;
            this.pnlFolderOpen.Controls.Add(this.lblFolderOpen);
            this.pnlFolderOpen.Dock = DockStyle.Top;
            this.pnlFolderOpen.Location = new Point(3, 3);
            this.pnlFolderOpen.Name = "pnlFolderOpen";
            this.pnlFolderOpen.Size = new System.Drawing.Size(0xd1, 0x1a);
            this.pnlFolderOpen.TabIndex = 3;
            this.lblFolderOpen.BackColor = SystemColors.GradientInactiveCaption;
            this.lblFolderOpen.Dock = DockStyle.Fill;
            this.lblFolderOpen.Location = new Point(0, 0);
            this.lblFolderOpen.Name = "lblFolderOpen";
            this.lblFolderOpen.Size = new System.Drawing.Size(0xd1, 0x1a);
            this.lblFolderOpen.TabIndex = 0;
            this.lblFolderOpen.Text = "Thư mục t\x00e0i liệu tham khảo";
            this.lblFolderOpen.TextAlign = ContentAlignment.MiddleCenter;
            this.tbpJobKey.Controls.Add(this.lsvJobKey);
            this.tbpJobKey.Controls.Add(this.pnlJobKey);
            this.tbpJobKey.ImageIndex = 0;
            this.tbpJobKey.Location = new Point(4, 0x76);
            this.tbpJobKey.Name = "tbpJobKey";
            this.tbpJobKey.Padding = new Padding(3);
            this.tbpJobKey.Size = new System.Drawing.Size(0xd7, 0x126);
            this.tbpJobKey.TabIndex = 2;
            this.tbpJobKey.Text = "Ph\x00edm tắt nghiệp vụ";
            this.tbpJobKey.UseVisualStyleBackColor = true;
            this.lsvJobKey.Columns.AddRange(new ColumnHeader[] { this.clhJobCode, this.clhJobKey });
            this.lsvJobKey.Cursor = Cursors.Hand;
            this.lsvJobKey.Dock = DockStyle.Fill;
            this.lsvJobKey.ForeColor = Color.RoyalBlue;
            this.lsvJobKey.FullRowSelect = true;
            this.lsvJobKey.HideSelection = false;
            this.lsvJobKey.Items.AddRange(new ListViewItem[] { item });
            this.lsvJobKey.Location = new Point(3, 0x1d);
            this.lsvJobKey.MultiSelect = false;
            this.lsvJobKey.Name = "lsvJobKey";
            this.lsvJobKey.Size = new System.Drawing.Size(0xd1, 0x106);
            this.lsvJobKey.TabIndex = 4;
            this.lsvJobKey.UseCompatibleStateImageBehavior = false;
            this.lsvJobKey.View = View.Details;
            this.lsvJobKey.KeyDown += new KeyEventHandler(this.lsvJobKey_KeyDown);
            this.lsvJobKey.MouseClick += new MouseEventHandler(this.lsvJobKey_MouseClick);
            this.clhJobCode.Text = "M\x00e3 nghiệp vụ";
            this.clhJobCode.Width = 80;
            this.clhJobKey.Text = "Ph\x00edm tắt";
            this.clhJobKey.TextAlign = HorizontalAlignment.Right;
            this.clhJobKey.Width = 80;
            this.pnlJobKey.BackColor = SystemColors.GradientInactiveCaption;
            this.pnlJobKey.Controls.Add(this.lblJobKey);
            this.pnlJobKey.Dock = DockStyle.Top;
            this.pnlJobKey.Location = new Point(3, 3);
            this.pnlJobKey.Name = "pnlJobKey";
            this.pnlJobKey.Size = new System.Drawing.Size(0xd1, 0x1a);
            this.pnlJobKey.TabIndex = 3;
            this.lblJobKey.Dock = DockStyle.Fill;
            this.lblJobKey.Location = new Point(0, 0);
            this.lblJobKey.Name = "lblJobKey";
            this.lblJobKey.Size = new System.Drawing.Size(0xd1, 0x1a);
            this.lblJobKey.TabIndex = 0;
            this.lblJobKey.Text = "Ph\x00edm tắt nghiệp vụ";
            this.lblJobKey.TextAlign = ContentAlignment.MiddleCenter;
            this.tbpHistoryJob.Controls.Add(this.lsbHistoryJob);
            this.tbpHistoryJob.Controls.Add(this.pnlHistoryJob);
            this.tbpHistoryJob.ImageIndex = 0;
            this.tbpHistoryJob.Location = new Point(4, 0x76);
            this.tbpHistoryJob.Name = "tbpHistoryJob";
            this.tbpHistoryJob.Padding = new Padding(3);
            this.tbpHistoryJob.Size = new System.Drawing.Size(0xd7, 0x126);
            this.tbpHistoryJob.TabIndex = 1;
            this.tbpHistoryJob.Text = "Nhật k\x00fd nghiệp vụ";
            this.tbpHistoryJob.UseVisualStyleBackColor = true;
            this.lsbHistoryJob.Cursor = Cursors.Hand;
            this.lsbHistoryJob.Dock = DockStyle.Fill;
            this.lsbHistoryJob.DrawMode = DrawMode.OwnerDrawFixed;
            this.lsbHistoryJob.ForeColor = Color.RoyalBlue;
            this.lsbHistoryJob.FormattingEnabled = true;
            this.lsbHistoryJob.HorizontalScrollbar = true;
            this.lsbHistoryJob.IntegralHeight = false;
            this.lsbHistoryJob.ItemHeight = 20;
            this.lsbHistoryJob.Items.AddRange(new object[] { "IDA.S02 輸入申告事項登録：海上：大額", "BIA 搬入確認登録（保税運送貨物）" });
            this.lsbHistoryJob.Location = new Point(3, 0x1d);
            this.lsbHistoryJob.Name = "lsbHistoryJob";
            this.lsbHistoryJob.Size = new System.Drawing.Size(0xd1, 0x106);
            this.lsbHistoryJob.TabIndex = 2;
            this.lsbHistoryJob.MouseClick += new MouseEventHandler(this.lsbHistoryJob_MouseClick);
            this.lsbHistoryJob.DrawItem += new DrawItemEventHandler(this.lsb_DrawItem);
            this.lsbHistoryJob.KeyDown += new KeyEventHandler(this.lsbHistoryJob_KeyDown);
            this.pnlHistoryJob.BackColor = SystemColors.GradientInactiveCaption;
            this.pnlHistoryJob.Controls.Add(this.lblHistoryJob);
            this.pnlHistoryJob.Dock = DockStyle.Top;
            this.pnlHistoryJob.Location = new Point(3, 3);
            this.pnlHistoryJob.Name = "pnlHistoryJob";
            this.pnlHistoryJob.Size = new System.Drawing.Size(0xd1, 0x1a);
            this.pnlHistoryJob.TabIndex = 1;
            this.lblHistoryJob.Dock = DockStyle.Fill;
            this.lblHistoryJob.Location = new Point(0, 0);
            this.lblHistoryJob.Name = "lblHistoryJob";
            this.lblHistoryJob.Size = new System.Drawing.Size(0xd1, 0x1a);
            this.lblHistoryJob.TabIndex = 0;
            this.lblHistoryJob.Text = "Nhật k\x00fd nghiệp vụ";
            this.lblHistoryJob.TextAlign = ContentAlignment.MiddleCenter;
            this.tbpJobMenu.Controls.Add(this.trvJobMenu);
            this.tbpJobMenu.Controls.Add(this.pnlJobMenu);
            this.tbpJobMenu.ImageIndex = 0;
            this.tbpJobMenu.Location = new Point(4, 0x76);
            this.tbpJobMenu.Name = "tbpJobMenu";
            this.tbpJobMenu.Padding = new Padding(3);
            this.tbpJobMenu.Size = new System.Drawing.Size(0xd7, 0x126);
            this.tbpJobMenu.TabIndex = 0;
            this.tbpJobMenu.Text = "Danh s\x00e1ch nghiệp vụ";
            this.tbpJobMenu.UseVisualStyleBackColor = true;
            this.trvJobMenu.Cursor = Cursors.Hand;
            this.trvJobMenu.Dock = DockStyle.Fill;
            this.trvJobMenu.Location = new Point(3, 0x1d);
            this.trvJobMenu.Name = "trvJobMenu";
            this.trvJobMenu.Size = new System.Drawing.Size(0xd1, 0x106);
            this.trvJobMenu.TabIndex = 6;
            this.trvJobMenu.Tag = "";
            this.trvJobMenu.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.trvJobMenu_NodeMouseClick);
            this.trvJobMenu.KeyDown += new KeyEventHandler(this.trvJobMenu_KeyDown);
            this.trvJobMenu.MouseMove += new MouseEventHandler(this.trvJobMenu_MouseMove);
            this.pnlJobMenu.BackColor = SystemColors.GradientInactiveCaption;
            this.pnlJobMenu.Controls.Add(this.lblJobMenu);
            this.pnlJobMenu.Dock = DockStyle.Top;
            this.pnlJobMenu.Location = new Point(3, 3);
            this.pnlJobMenu.Name = "pnlJobMenu";
            this.pnlJobMenu.Size = new System.Drawing.Size(0xd1, 0x1a);
            this.pnlJobMenu.TabIndex = 5;
            this.lblJobMenu.Dock = DockStyle.Fill;
            this.lblJobMenu.Location = new Point(0, 0);
            this.lblJobMenu.Name = "lblJobMenu";
            this.lblJobMenu.Size = new System.Drawing.Size(0xd1, 0x1a);
            this.lblJobMenu.TabIndex = 0;
            this.lblJobMenu.Text = "Danh s\x00e1ch nghiệp vụ";
            this.lblJobMenu.TextAlign = ContentAlignment.MiddleCenter;
            this.tcJobOpenWindow.Controls.Add(this.tbpJobMenu);
            this.tcJobOpenWindow.Controls.Add(this.tbpHistoryJob);
            this.tcJobOpenWindow.Controls.Add(this.tbpJobKey);
            this.tcJobOpenWindow.Controls.Add(this.tbpFolderOpen);
            this.tcJobOpenWindow.Controls.Add(this.tbpGeneralApp);
            this.tcJobOpenWindow.Controls.Add(this.tbpCode);
            this.tcJobOpenWindow.Dock = DockStyle.Fill;
            this.tcJobOpenWindow.ImageList = this.imlJobMenu;
            this.tcJobOpenWindow.Location = new Point(0, 0);
            this.tcJobOpenWindow.Multiline = true;
            this.tcJobOpenWindow.Name = "tcJobOpenWindow";
            this.tcJobOpenWindow.SelectedIndex = 0;
            this.tcJobOpenWindow.Size = new System.Drawing.Size(0xdf, 0x1a0);
            this.tcJobOpenWindow.TabIndex = 0x19;
            this.tbpGeneralApp.Controls.Add(this.lsbGeneralApp);
            this.tbpGeneralApp.Controls.Add(this.pnlGeneralApp);
            this.tbpGeneralApp.ImageIndex = 2;
            this.tbpGeneralApp.Location = new Point(4, 0x76);
            this.tbpGeneralApp.Name = "tbpGeneralApp";
            this.tbpGeneralApp.Padding = new Padding(3);
            this.tbpGeneralApp.Size = new System.Drawing.Size(0xd7, 0x126);
            this.tbpGeneralApp.TabIndex = 5;
            this.tbpGeneralApp.Text = "Danh s\x00e1ch biểu mẫu khai b\x00e1o";
            this.tbpGeneralApp.UseVisualStyleBackColor = true;
            this.lsbGeneralApp.Cursor = Cursors.Hand;
            this.lsbGeneralApp.Dock = DockStyle.Fill;
            this.lsbGeneralApp.DrawMode = DrawMode.OwnerDrawFixed;
            this.lsbGeneralApp.ForeColor = Color.RoyalBlue;
            this.lsbGeneralApp.FormattingEnabled = true;
            this.lsbGeneralApp.HorizontalScrollbar = true;
            this.lsbGeneralApp.IntegralHeight = false;
            this.lsbGeneralApp.ItemHeight = 20;
            this.lsbGeneralApp.Items.AddRange(new object[] { 
                "K01 不開港入港届出（遭難／特殊船舶）", "K02 不開港入港届出（遭難／特殊船舶）", "K03 不開港入港届出（遭難／特殊航空機）", "K04 不開港入港届出（遭難／特殊航空機）", "K05 沿海通航船等外国寄港届出", "K06 船舶／航空機資格変更届出", "K07 船舶／航空機資格変更届出", "K08 不開港在港期間等変更願", "K09 船移届出", "K10 指定地外積卸許可申請", "K11 船陸交通一括許可申請変更届出", "K12 指定地外／船陸交通許可申請（包括）", "K13 指定地外／船陸交通許可申請（包括）", "K14 仮陸揚届出（船用品等）", "K15 仮陸揚復路運送申告（船用品等）", "K16 仮陸揚期間延長願（船用品等）", 
                "K17 外貨船機用品積込承認申告（包括）", "K18 外貨船機用品積込（包括）訂正願", "K19 内貨船機用品積込承認申告（包括）", "K20 内貨船機用品積込（包括）訂正願", "K21 船機用燃料油振替積込承認申請", "K22 とん税非課税理由証明申請", "K23 手数料予納承認申請（臨時開庁（監視））", "K99 ＮＡＣＣＳ登録情報変更願（監視）", "G01 違約品等廃棄関税払戻申請", "G02 国産困難航空機素材等の確認申請", "G03 再輸出・再輸入・輸入期間延長承認申請", "G04 再輸出・再輸入・輸入期間延長承認申請", "G05 再輸出・再輸入・輸入期間延長承認申請", "G06 再輸出・再輸入・輸入期間延長承認申請", "G07 違約品等保税地域搬入期間延長承認申請", "G08 外国貨物古包装材料引取免税願", 
                "G09 外国貨物古包装材料引取免税願", "G10 輸入原料品等関税額証明願", "G11 加工修繕輸出貨物確認申告", "G12 加工組立輸出貨物確認申告", "G13 再輸出減免税貨物輸出届出", "G14 再輸出減免税貨物輸出届出", "G15 再輸出貨物に係る輸入確認申請", "G16 再輸出貨物に係る輸入確認申請", "G17 包括事前審査申出", "G18 滅却（廃棄）承認申請（違約品等）", "G19 滅却（廃棄）承認申請（違約品等）", "G20 滅却（廃棄）承認申請（違約品等）", "G21 疑義貨物点検申請", "G22 手数料予納承認申請（臨時開庁（通関））", "G99 ＮＡＣＣＳ登録情報変更願（通関）", "S01 担保物／保証人変更承認申請", 
                "S02 担保物／保証人変更承認申請", "S03 担保保証期間非更新届出", "S04 担保解除申請", "S05 過誤納金充当申出", "S06 手数料予納承認申請（臨時開庁（収納））", "S99 ＮＡＣＣＳ登録情報変更願（収納）", "Y01 輸入貨物評価（包括）申告Ｉ", "Y02 輸入貨物評価（包括）申告ＩＩ", "Y03 輸入貨物評価（包括）一部変更届出", "Y04 輸入貨物評価（個別）申告Ｉ（事前審査）", "Y05 輸入貨物評価（個別）申告ＩＩ（事前審査）", "Y06 関税評価に係る事前教示Ｉ", "Y07 関税評価に係る事前教示ＩＩ", "Y99 ＮＡＣＣＳ登録情報変更願（評価）", "Z01 事前教示照会（分類）", "Z02 事前教示照会（原産地）", 
                "Z03 事前教示回答書（変更通知書）異議申出", "Z99 ＮＡＣＣＳ登録情報変更願（関税鑑査官）", "T01 通関業許可申請事項変更届出", "T02 通関士その他通関業務従業者氏名等届出", "T03 件数・料金その他通関業務関連事項報告", "T99 ＮＡＣＣＳ登録情報変更願（通関業監督官）", "H01 保税地域収容能力等変更届出", "H02 保税地域収容能力等変更届出", "H03 保税地域収容能力等変更届出", "H04 保税地域収容能力等変更届出", "H05 保税地域休廃業届出", "H06 保税地域休廃業届出", "H07 保税地域休廃業届出", "H08 保税地域休廃業届出", "H09 保税地域業務再開届出", "H10 保税地域業務再開届出", 
                "H11 保税地域業務再開届出", "H12 保税地域業務再開届出", "H13 同時蔵置特例届出", "H14 同時蔵置特例変更届出", "H15 保税地域許可内容変更届出", "H16 保税台帳電磁的記録保存届出", "H17 外国貨物蔵置期間延長承認申請", "H18 外国貨物蔵置期間延長承認申請", "H19 外国貨物蔵置期間延長承認申請", "H20 未承認貨物蔵置期間延長申請", "H21 船機用品戻入届出", "H22 滅却（廃棄）承認申請（保税蔵置貨物等）", "H23 滅却（廃棄）承認申請（保税蔵置貨物等）", "H24 滅却（廃棄）承認申請（保税蔵置貨物等）", "H25 滅却（廃棄）承認申請（保税蔵置貨物等）", "H26 滅却（廃棄）承認申請（保税蔵置貨物等）", 
                "H27 滅却（廃棄）承認申請（保税蔵置貨物等）", "H28 滅却（廃棄）承認申請（保税蔵置貨物等）", "H29 外国貨物包括滅却承認申請", "H30 減免税外貨等亡失届出（免税コンテナ等）", "H31 外国貨物亡失届出", "H32 外国貨物亡失届出", "H33 外国貨物亡失届出", "H34 外国貨物亡失届出", "H35 外国貨物亡失届出", "H36 外国貨物亡失届出", "H37 外国貨物亡失届出", "H38 違約品等保税地域搬入届", "H39 違約品等保税地域搬入届", "H40 違約品等保税地域搬入届", "H41 違約品等保税地域搬入届", "H42 違約品等保税地域搬入届", 
                "H43 違約品等保税地域搬入届", "H44 見本一時持出（包括）許可申請", "H45 外国貨物廃棄届出", "H46 免税コンテナー国内運送届出", "H47 免税コンテナー再輸出期間延長承認申請", "H48 国産コンテナー等確認申請", "H49 国産コンテナー等確認証紙貼付事績報告", "H50 免税コンテナー等滅却承認申請", "H51 免税コンテナー記帳事務所報告", "H52 免税コンテナー等変質損傷減税申請", "H53 手数料予納承認申請（臨時開庁（保税））", "H99 ＮＡＣＣＳ登録情報変更願（保税）"
             });
            this.lsbGeneralApp.Location = new Point(3, 0x1d);
            this.lsbGeneralApp.Name = "lsbGeneralApp";
            this.lsbGeneralApp.Size = new System.Drawing.Size(0xd1, 0x106);
            this.lsbGeneralApp.TabIndex = 6;
            this.lsbGeneralApp.DrawItem += new DrawItemEventHandler(this.lsb_DrawItem);
            this.lsbGeneralApp.KeyDown += new KeyEventHandler(this.lsbGeneralApp_KeyDown);
            this.lsbGeneralApp.MouseDown += new MouseEventHandler(this.lsbGeneralApp_MouseDown);
            this.pnlGeneralApp.BackColor = SystemColors.GradientInactiveCaption;
            this.pnlGeneralApp.Controls.Add(this.lblGeneralApp);
            this.pnlGeneralApp.Dock = DockStyle.Top;
            this.pnlGeneralApp.Location = new Point(3, 3);
            this.pnlGeneralApp.Name = "pnlGeneralApp";
            this.pnlGeneralApp.Size = new System.Drawing.Size(0xd1, 0x1a);
            this.pnlGeneralApp.TabIndex = 5;
            this.lblGeneralApp.BackColor = SystemColors.GradientInactiveCaption;
            this.lblGeneralApp.Dock = DockStyle.Fill;
            this.lblGeneralApp.Location = new Point(0, 0);
            this.lblGeneralApp.Name = "lblGeneralApp";
            this.lblGeneralApp.Size = new System.Drawing.Size(0xd1, 0x1a);
            this.lblGeneralApp.TabIndex = 0;
            this.lblGeneralApp.Text = "Danh s\x00e1ch biểu mẫu khai b\x00e1o";
            this.lblGeneralApp.TextAlign = ContentAlignment.MiddleCenter;
            this.tbpCode.Controls.Add(this.lsbCode);
            this.tbpCode.Controls.Add(this.pnlCode);
            this.tbpCode.ImageIndex = 2;
            this.tbpCode.Location = new Point(4, 0x76);
            this.tbpCode.Name = "tbpCode";
            this.tbpCode.Padding = new Padding(3);
            this.tbpCode.Size = new System.Drawing.Size(0xd7, 0x126);
            this.tbpCode.TabIndex = 6;
            this.tbpCode.Text = "Danh s\x00e1ch m\x00e3 chuẩn";
            this.tbpCode.UseVisualStyleBackColor = true;
            this.lsbCode.Cursor = Cursors.Hand;
            this.lsbCode.Dock = DockStyle.Fill;
            this.lsbCode.DrawMode = DrawMode.OwnerDrawFixed;
            this.lsbCode.ForeColor = Color.RoyalBlue;
            this.lsbCode.FormattingEnabled = true;
            this.lsbCode.HorizontalScrollbar = true;
            this.lsbCode.IntegralHeight = false;
            this.lsbCode.ItemHeight = 20;
            this.lsbCode.Items.AddRange(new object[] { 
                "K01 不開港入港届出（遭難／特殊船舶）", "K02 不開港入港届出（遭難／特殊船舶）", "K03 不開港入港届出（遭難／特殊航空機）", "K04 不開港入港届出（遭難／特殊航空機）", "K05 沿海通航船等外国寄港届出", "K06 船舶／航空機資格変更届出", "K07 船舶／航空機資格変更届出", "K08 不開港在港期間等変更願", "K09 船移届出", "K10 指定地外積卸許可申請", "K11 船陸交通一括許可申請変更届出", "K12 指定地外／船陸交通許可申請（包括）", "K13 指定地外／船陸交通許可申請（包括）", "K14 仮陸揚届出（船用品等）", "K15 仮陸揚復路運送申告（船用品等）", "K16 仮陸揚期間延長願（船用品等）", 
                "K17 外貨船機用品積込承認申告（包括）", "K18 外貨船機用品積込（包括）訂正願", "K19 内貨船機用品積込承認申告（包括）", "K20 内貨船機用品積込（包括）訂正願", "K21 船機用燃料油振替積込承認申請", "K22 とん税非課税理由証明申請", "K23 手数料予納承認申請（臨時開庁（監視））", "K99 ＮＡＣＣＳ登録情報変更願（監視）", "G01 違約品等廃棄関税払戻申請", "G02 国産困難航空機素材等の確認申請", "G03 再輸出・再輸入・輸入期間延長承認申請", "G04 再輸出・再輸入・輸入期間延長承認申請", "G05 再輸出・再輸入・輸入期間延長承認申請", "G06 再輸出・再輸入・輸入期間延長承認申請", "G07 違約品等保税地域搬入期間延長承認申請", "G08 外国貨物古包装材料引取免税願", 
                "G09 外国貨物古包装材料引取免税願", "G10 輸入原料品等関税額証明願", "G11 加工修繕輸出貨物確認申告", "G12 加工組立輸出貨物確認申告", "G13 再輸出減免税貨物輸出届出", "G14 再輸出減免税貨物輸出届出", "G15 再輸出貨物に係る輸入確認申請", "G16 再輸出貨物に係る輸入確認申請", "G17 包括事前審査申出", "G18 滅却（廃棄）承認申請（違約品等）", "G19 滅却（廃棄）承認申請（違約品等）", "G20 滅却（廃棄）承認申請（違約品等）", "G21 疑義貨物点検申請", "G22 手数料予納承認申請（臨時開庁（通関））", "G99 ＮＡＣＣＳ登録情報変更願（通関）", "S01 担保物／保証人変更承認申請", 
                "S02 担保物／保証人変更承認申請", "S03 担保保証期間非更新届出", "S04 担保解除申請", "S05 過誤納金充当申出", "S06 手数料予納承認申請（臨時開庁（収納））", "S99 ＮＡＣＣＳ登録情報変更願（収納）", "Y01 輸入貨物評価（包括）申告Ｉ", "Y02 輸入貨物評価（包括）申告ＩＩ", "Y03 輸入貨物評価（包括）一部変更届出", "Y04 輸入貨物評価（個別）申告Ｉ（事前審査）", "Y05 輸入貨物評価（個別）申告ＩＩ（事前審査）", "Y06 関税評価に係る事前教示Ｉ", "Y07 関税評価に係る事前教示ＩＩ", "Y99 ＮＡＣＣＳ登録情報変更願（評価）", "Z01 事前教示照会（分類）", "Z02 事前教示照会（原産地）", 
                "Z03 事前教示回答書（変更通知書）異議申出", "Z99 ＮＡＣＣＳ登録情報変更願（関税鑑査官）", "T01 通関業許可申請事項変更届出", "T02 通関士その他通関業務従業者氏名等届出", "T03 件数・料金その他通関業務関連事項報告", "T99 ＮＡＣＣＳ登録情報変更願（通関業監督官）", "H01 保税地域収容能力等変更届出", "H02 保税地域収容能力等変更届出", "H03 保税地域収容能力等変更届出", "H04 保税地域収容能力等変更届出", "H05 保税地域休廃業届出", "H06 保税地域休廃業届出", "H07 保税地域休廃業届出", "H08 保税地域休廃業届出", "H09 保税地域業務再開届出", "H10 保税地域業務再開届出", 
                "H11 保税地域業務再開届出", "H12 保税地域業務再開届出", "H13 同時蔵置特例届出", "H14 同時蔵置特例変更届出", "H15 保税地域許可内容変更届出", "H16 保税台帳電磁的記録保存届出", "H17 外国貨物蔵置期間延長承認申請", "H18 外国貨物蔵置期間延長承認申請", "H19 外国貨物蔵置期間延長承認申請", "H20 未承認貨物蔵置期間延長申請", "H21 船機用品戻入届出", "H22 滅却（廃棄）承認申請（保税蔵置貨物等）", "H23 滅却（廃棄）承認申請（保税蔵置貨物等）", "H24 滅却（廃棄）承認申請（保税蔵置貨物等）", "H25 滅却（廃棄）承認申請（保税蔵置貨物等）", "H26 滅却（廃棄）承認申請（保税蔵置貨物等）", 
                "H27 滅却（廃棄）承認申請（保税蔵置貨物等）", "H28 滅却（廃棄）承認申請（保税蔵置貨物等）", "H29 外国貨物包括滅却承認申請", "H30 減免税外貨等亡失届出（免税コンテナ等）", "H31 外国貨物亡失届出", "H32 外国貨物亡失届出", "H33 外国貨物亡失届出", "H34 外国貨物亡失届出", "H35 外国貨物亡失届出", "H36 外国貨物亡失届出", "H37 外国貨物亡失届出", "H38 違約品等保税地域搬入届", "H39 違約品等保税地域搬入届", "H40 違約品等保税地域搬入届", "H41 違約品等保税地域搬入届", "H42 違約品等保税地域搬入届", 
                "H43 違約品等保税地域搬入届", "H44 見本一時持出（包括）許可申請", "H45 外国貨物廃棄届出", "H46 免税コンテナー国内運送届出", "H47 免税コンテナー再輸出期間延長承認申請", "H48 国産コンテナー等確認申請", "H49 国産コンテナー等確認証紙貼付事績報告", "H50 免税コンテナー等滅却承認申請", "H51 免税コンテナー記帳事務所報告", "H52 免税コンテナー等変質損傷減税申請", "H53 手数料予納承認申請（臨時開庁（保税））", "H99 ＮＡＣＣＳ登録情報変更願（保税）"
             });
            this.lsbCode.Location = new Point(3, 0x1d);
            this.lsbCode.Name = "lsbCode";
            this.lsbCode.Size = new System.Drawing.Size(0xd1, 0x106);
            this.lsbCode.TabIndex = 7;
            this.lsbCode.DrawItem += new DrawItemEventHandler(this.lsb_DrawItem);
            this.lsbCode.KeyDown += new KeyEventHandler(this.lsbCode_KeyDown);
            this.lsbCode.MouseDown += new MouseEventHandler(this.lsbCode_MouseDown);
            this.pnlCode.BackColor = SystemColors.GradientInactiveCaption;
            this.pnlCode.Controls.Add(this.lblCode);
            this.pnlCode.Dock = DockStyle.Top;
            this.pnlCode.Location = new Point(3, 3);
            this.pnlCode.Name = "pnlCode";
            this.pnlCode.Size = new System.Drawing.Size(0xd1, 0x1a);
            this.pnlCode.TabIndex = 6;
            this.lblCode.BackColor = SystemColors.GradientInactiveCaption;
            this.lblCode.Dock = DockStyle.Fill;
            this.lblCode.Location = new Point(0, 0);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(0xd1, 0x1a);
            this.lblCode.TabIndex = 0;
            this.lblCode.Text = "Danh s\x00e1ch m\x00e3 chuẩn";
            this.lblCode.TextAlign = ContentAlignment.MiddleCenter;
            this.imlJobMenu.ImageStream = (ImageListStreamer) manager.GetObject("imlJobMenu.ImageStream");
            this.imlJobMenu.TransparentColor = Color.Fuchsia;
            this.imlJobMenu.Images.SetKeyName(0, "job2.ico");
            this.imlJobMenu.Images.SetKeyName(1, "OpenFolder.bmp");
            this.imlJobMenu.Images.SetKeyName(2, "XMLFile.bmp");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.tcJobOpenWindow);
            base.Name = "UJobMenu";
            base.Size = new System.Drawing.Size(0xdf, 0x1a0);
            base.Load += new EventHandler(this.UJobMenu_Load);
            this.tbpFolderOpen.ResumeLayout(false);
            this.pnlFolderOpen.ResumeLayout(false);
            this.tbpJobKey.ResumeLayout(false);
            this.pnlJobKey.ResumeLayout(false);
            this.tbpHistoryJob.ResumeLayout(false);
            this.pnlHistoryJob.ResumeLayout(false);
            this.tbpJobMenu.ResumeLayout(false);
            this.pnlJobMenu.ResumeLayout(false);
            this.tcJobOpenWindow.ResumeLayout(false);
            this.tbpGeneralApp.ResumeLayout(false);
            this.pnlGeneralApp.ResumeLayout(false);
            this.tbpCode.ResumeLayout(false);
            this.pnlCode.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public void JobKeySet()
        {
            this.lsvJobKey.Items.Clear();
            UserKey key = UserKey.CreateInstance();
            for (int i = 0; i < key.JobKeyList[0].KeyList.Count; i++)
            {
                if (key.JobKeyList[0].KeyList[i].Name != "")
                {
                    this.lsvJobKey.Items.Add(key.JobKeyList[0].KeyList[i].Name);
                    this.lsvJobKey.Items[this.lsvJobKey.Items.Count - 1].SubItems.Add(key.JobKeyList[0].KeyList[i].Sckey);
                    this.lsvJobKey.Items[this.lsvJobKey.Items.Count - 1].Font = new Font(this.lsvJobKey.Items[this.lsvJobKey.Items.Count - 1].Font, FontStyle.Underline);
                }
            }
        }

        public void JobMenuSet()
        {
            this.pi = PathInfo.CreateInstance();
            this.fs = FileSave.CreateInstance();
        }

        private void JobTreeSet()
        {
            if (Directory.Exists(this.pi.JobTreeRoot))
            {
                XmlDocument document = new XmlDocument();
                foreach (string str in Directory.GetFiles(this.pi.JobTreeRoot, "JobTree*.xml", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(str))
                    {
                        try
                        {
                            document.Load(str);
                            XmlNode documentElement = document.DocumentElement;
                            TreeNode tn = null;
                            this.repeatNode(documentElement, tn, true);
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        private void lsb_DrawItem(object sender, DrawItemEventArgs e)
        {
            Color foreColor;
            e.DrawBackground();
            ListBox box = (ListBox) sender;
            string text = (e.Index > -1) ? box.Items[e.Index].ToString() : box.Text;
            Font font = new Font(box.Font, box.Font.Style);
            if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
            {
                foreColor = box.ForeColor;
            }
            else
            {
                foreColor = box.BackColor;
            }
            float num = (e.Bounds.Height - e.Graphics.MeasureString(text, font).Height) / 2f;
            TextRenderer.DrawText(e.Graphics, text, this.Font, new Point(e.Bounds.X, e.Bounds.Y + ((int) num)), foreColor);
            font.Dispose();
            e.DrawFocusRectangle();
        }

        private void lsbCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.lsbCode_MouseDown(this, null);
            }
        }

        private void lsbCode_MouseDown(object sender, MouseEventArgs e)
        {
            if ((this.lsbCode.Items.Count >= 1) && (this.lsbCode.SelectedItem.ToString() != ""))
            {
                try
                {
                    Process.Start(this.CodeList[this.lsbCode.SelectedIndex].URL);
                }
                catch (Exception exception)
                {
                    string message = Resources.ResourceManager.GetString("CORE41") + Environment.NewLine + MessageDialog.CreateExceptionMessage(exception);
                    using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                    {
                        form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, message);
                    }
                }
            }
        }

        private void lsbFolderOpen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.lsbFolderOpen_MouseClick(this, null);
            }
        }

        private void lsbFolderOpen_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.lsbFolderOpen.SelectedIndex > -1)
            {
                string path = "";
                switch (this.lsbFolderOpen.SelectedIndex)
                {
                    case 0:
                        path = this.pi.LogPath;
                        break;

                    case 1:
                        path = this.fs.TypeList.getType("C").Dir;
                        break;

                    case 2:
                        path = this.fs.TypeList.getType("M").Dir;
                        break;

                    case 3:
                        path = this.fs.TypeList.getType("P").Dir;
                        break;

                    case 4:
                        path = this.fs.TypeList.getType("R").Dir;
                        break;

                    case 5:
                        path = this.fs.SendFile.SendUser;
                        break;
                }
                if (Directory.Exists(path))
                {
                    Process.Start("EXPLORER.EXE", "/n," + path);
                }
            }
        }

        private void lsbGeneralApp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.lsbGeneralApp_MouseDown(this, null);
            }
        }

        private void lsbGeneralApp_MouseDown(object sender, MouseEventArgs e)
        {
            if ((this.lsbGeneralApp.Items.Count >= 1) && (this.lsbGeneralApp.SelectedItem.ToString() != ""))
            {
                try
                {
                    Process.Start(this.GeneralAppList[this.lsbGeneralApp.SelectedIndex].URL);
                }
                catch (Exception exception)
                {
                    string message = Resources.ResourceManager.GetString("CORE41") + Environment.NewLine + MessageDialog.CreateExceptionMessage(exception);
                    using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                    {
                        form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, message);
                    }
                }
            }
        }

        private void lsbHistoryJob_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.lsbHistoryJob_MouseClick(this, null);
            }
        }

        private void lsbHistoryJob_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.lsbHistoryJob.SelectedIndex > -1)
            {
                string jobCode = this.lsbHistoryJob.SelectedItem.ToString();
                string dispCode = "";
                int index = jobCode.IndexOf(" ");
                jobCode = jobCode.Remove(index);
                if (jobCode.IndexOf(".") > -1)
                {
                    string str3 = jobCode;
                    int length = str3.IndexOf(".");
                    jobCode = str3.Substring(0, length);
                    dispCode = str3.Remove(0, length + 1);
                }
                this.OnJobOpen(this, jobCode, dispCode);
            }
        }

        private void lsvJobKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.lsvJobKey_MouseClick(this, null);
            }
        }

        private void lsvJobKey_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.lsvJobKey.SelectedItems.Count > 0)
            {
                string text = this.lsvJobKey.SelectedItems[0].Text;
                string dispCode = "";
                int index = text.IndexOf(".");
                if (index > -1)
                {
                    dispCode = text;
                    text = text.Substring(0, index);
                    dispCode = dispCode.Remove(0, index + 1);
                }
                this.OnJobOpen(this, text, dispCode);
            }
        }

        private void repeatNode(XmlNode xn, TreeNode tn, bool FirstFlag)
        {
            for (int i = 0; i < xn.ChildNodes.Count; i++)
            {
                TreeNode node;
                if (FirstFlag)
                {
                    node = this.trvJobMenu.Nodes.Add(xn.ChildNodes[i].Attributes["name"].InnerText);
                }
                else
                {
                    node = tn.Nodes.Add(xn.ChildNodes[i].Attributes["name"].InnerText);
                }
                if (xn.ChildNodes[i].Name == "item")
                {
                    node.Tag = xn.ChildNodes[i].Attributes["code"].InnerText;
                    node.ForeColor = Color.RoyalBlue;
                }
                else if (xn.ChildNodes[i].Name == "category")
                {
                    this.repeatNode(xn.ChildNodes[i], node, false);
                }
            }
        }

        private void tm_Tick(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Timer) sender).Enabled = false;
            ((System.Windows.Forms.Timer) sender).Dispose();
            this.OnJobOpen(this, this.JCode, this.DCode);
        }

        private void TreeOpen()
        {
            for (int i = 0; i < this.trvJobMenu.Nodes.Count; i++)
            {
                this.trvJobMenu.Nodes[i].Expand();
            }
            if (this.trvJobMenu.Nodes.Count > 0)
            {
                this.trvJobMenu.TopNode = this.trvJobMenu.Nodes[0];
            }
        }

        private void trvJobMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TreeNode selectedNode = this.trvJobMenu.SelectedNode;
                if ((selectedNode != null) && (selectedNode.GetNodeCount(true) == 0))
                {
                    string tag = (string) selectedNode.Tag;
                    string dispCode = "";
                    if (tag != null)
                    {
                        if (tag.IndexOf(".") > -1)
                        {
                            dispCode = tag;
                            tag = tag.Substring(0, tag.IndexOf("."));
                            dispCode = dispCode.Remove(0, dispCode.IndexOf(".") + 1);
                        }
                        this.OnJobOpen(this, tag, dispCode);
                    }
                }
            }
        }

        private void trvJobMenu_MouseMove(object sender, MouseEventArgs e)
        {
            TreeNode nodeAt = this.trvJobMenu.GetNodeAt(e.X, e.Y);
            if (((nodeAt != null) && (nodeAt.GetNodeCount(true) == 0)) && ((nodeAt.Bounds.X <= e.X) && (e.X <= (nodeAt.Bounds.X + nodeAt.Bounds.Width))))
            {
                this.trvJobMenu.Cursor = Cursors.Hand;
            }
            else
            {
                this.trvJobMenu.Cursor = Cursors.Default;
            }
        }

        private void trvJobMenu_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode nodeAt = this.trvJobMenu.GetNodeAt(e.X, e.Y);
            if (((nodeAt != null) && (nodeAt.GetNodeCount(true) == 0)) && ((nodeAt.Bounds.X <= e.X) && (e.X <= (nodeAt.Bounds.X + nodeAt.Bounds.Width))))
            {
                this.trvJobMenu.SelectedNode = nodeAt;
                this.JCode = (string) nodeAt.Tag;
                this.DCode = "";
                if (this.JCode != null)
                {
                    if (this.JCode.IndexOf(".") > -1)
                    {
                        this.DCode = this.JCode;
                        this.JCode = this.JCode.Substring(0, this.JCode.IndexOf("."));
                        this.DCode = this.DCode.Remove(0, this.DCode.IndexOf(".") + 1);
                    }
                    System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                    timer.Tick += new EventHandler(this.tm_Tick);
                    timer.Interval = 100;
                    timer.Enabled = true;
                }
            }
        }

        private void UJobMenu_Load(object sender, EventArgs e)
        {
            if (!this.isDesigning)
            {
                this.trvJobMenu.Nodes.Clear();
                this.JobMenuSet();
                this.JobTreeSet();
                this.GeneralAppSet();
                this.CodeSet();
                Graphics graphics = Graphics.FromHwnd(this.lsbHistoryJob.Handle);
                float num = 0f;
                for (int i = 0; i < this.lsbHistoryJob.Items.Count; i++)
                {
                    num = Math.Max(graphics.MeasureString((string) this.lsbHistoryJob.Items[i], this.lsbHistoryJob.Font).Width, num);
                }
                this.lsbHistoryJob.HorizontalExtent = ((int) num) + 8;
                graphics = Graphics.FromHwnd(this.lsbFolderOpen.Handle);
                num = 0f;
                for (int j = 0; j < this.lsbFolderOpen.Items.Count; j++)
                {
                    num = Math.Max(graphics.MeasureString((string) this.lsbFolderOpen.Items[j], this.lsbFolderOpen.Font).Width, num);
                }
                this.lsbFolderOpen.HorizontalExtent = ((int) num) + 8;
                graphics = Graphics.FromHwnd(this.lsbGeneralApp.Handle);
                num = 0f;
                for (int k = 0; k < this.lsbGeneralApp.Items.Count; k++)
                {
                    num = Math.Max(graphics.MeasureString((string) this.lsbGeneralApp.Items[k], this.lsbGeneralApp.Font).Width, num);
                }
                this.lsbGeneralApp.HorizontalExtent = ((int) num) + 8;
                graphics = Graphics.FromHwnd(this.lsbCode.Handle);
                num = 0f;
                for (int m = 0; m < this.lsbCode.Items.Count; m++)
                {
                    num = Math.Max(graphics.MeasureString((string) this.lsbCode.Items[m], this.lsbCode.Font).Width, num);
                }
                this.lsbCode.HorizontalExtent = ((int) num) + 8;
                this.TreeOpen();
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ListRec
        {
            public string Name;
            public string URL;
        }
    }
}

