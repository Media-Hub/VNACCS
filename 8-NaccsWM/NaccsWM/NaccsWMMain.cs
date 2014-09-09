namespace NaccsWM
{
    using Naccs.Core.DataView;
    using Naccs.Core.Settings;
    using Naccs.Interactive;
    using Naccs.Net.Http;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class NaccsWMMain : InteractiveMain
    {
        private IContainer components;
        private HttpSettings hts_bat;
        private HttpSettings hts_cls;
        private SystemEnvironment sys_env;
        private UDataView ucDataView;

        public NaccsWMMain() : this(false)
        {
        }

        public NaccsWMMain(bool proxyError) : base(proxyError)
        {
            this.InitializeComponent();
            if (!base.isDesigning)
            {
                base.idv = this.ucDataView;
                base.idv.OnUpdateDataViewItems += new OnUpdateDataViewItemsHandler(this.DataView_OnUpdateDataViewItems);
                base.idv.OnJobFormOpen += new OnJobFormOpenHandler(this.DataView_OnJobFormOpen);
                base.idv.OnPrintJob += new OnPrintJobHandler(this.DataVeiw_OnPrintJob);
                this.HttpSet();
                base.DemoFlag = false;
                base.ihc = new HttpClient(this.hts_cls);
                base.ato_ihc = new HttpClient(this.hts_cls);
                base.acm_ihc = new HttpClient(this.hts_cls);
                base.bat_ihc = new HttpClient(this.hts_bat);
                base.logon_ihc = new HttpClient(this.hts_cls);
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

        private void HttpSet()
        {
            this.hts_cls = new HttpSettings();
            this.hts_bat = new HttpSettings();
            this.SetMinStatus();
        }

        private void InitializeComponent()
        {
            this.ucDataView = new Naccs.Core.DataView.UDataView();
            this.pnlDataView.SuspendLayout();
            this.pnlJobInput.SuspendLayout();
            this.pnlJobMenu.SuspendLayout();
            this.pnlSupportWindow.SuspendLayout();
            this.tlcMain.ContentPanel.SuspendLayout();
            this.tlcMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDataView
            // 
            this.pnlDataView.Controls.Add(this.ucDataView);
            this.pnlDataView.Size = new System.Drawing.Size(813, 649);
            // 
            // pnlJobMenu
            // 
            this.pnlJobMenu.Size = new System.Drawing.Size(200, 427);
            // 
            // pnlSupportWindow
            // 
            this.pnlSupportWindow.Size = new System.Drawing.Size(200, 649);
            // 
            // tlcMain
            // 
            this.tlcMain.BottomToolStripPanelVisible = true;
            // 
            // tlcMain.ContentPanel
            // 
            this.tlcMain.ContentPanel.Size = new System.Drawing.Size(1016, 649);
            this.tlcMain.LeftToolStripPanelVisible = true;
            this.tlcMain.RightToolStripPanelVisible = true;
            this.tlcMain.TopToolStripPanelVisible = true;
            // 
            // uJobMenu1
            // 
            this.uJobMenu1.Size = new System.Drawing.Size(200, 427);
            // 
            // ucDataView
            // 
            this.ucDataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDataView.Location = new System.Drawing.Point(0, 0);
            this.ucDataView.Name = "ucDataView";
            this.ucDataView.Size = new System.Drawing.Size(813, 649);
            this.ucDataView.TabIndex = 0;
            // 
            // NaccsWMMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1016, 795);
            this.Name = "NaccsWMMain";
            this.Text = "Private Terminal Software";
            this.Load += new System.EventHandler(this.NaccsWMMain_Load);
            this.pnlDataView.ResumeLayout(false);
            this.pnlJobInput.ResumeLayout(false);
            this.pnlJobInput.PerformLayout();
            this.pnlJobMenu.ResumeLayout(false);
            this.pnlSupportWindow.ResumeLayout(false);
            this.pnlSupportWindow.PerformLayout();
            this.tlcMain.ContentPanel.ResumeLayout(false);
            this.tlcMain.ResumeLayout(false);
            this.tlcMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void NaccsWMMain_Load(object sender, EventArgs e)
        {
        }

        public override void OptionSetInd()
        {
            this.SetMinStatus();
        }

        private void ServerSet(string SrvCnt)
        {
            this.hts_cls.ServerName = this.sys_env.InteractiveInfo.SelectServer(base.usrenv.InteractiveInfo.ServerConnect).ServerName;
            this.hts_cls.ServerObject = this.sys_env.InteractiveInfo.SelectServer(base.usrenv.InteractiveInfo.ServerConnect).ObjectName;
            this.hts_cls.ServerPort = this.sys_env.InteractiveInfo.SelectServer(base.usrenv.InteractiveInfo.ServerConnect).ServerPort;
            this.hts_bat.ServerName = this.sys_env.BatchdocInfo.SelectServer(base.usrenv.InteractiveInfo.ServerConnect).ServerName;
            this.hts_bat.ServerObject = this.sys_env.BatchdocInfo.SelectServer(base.usrenv.InteractiveInfo.ServerConnect).ObjectName;
            this.hts_bat.ServerPort = this.sys_env.BatchdocInfo.SelectServer(base.usrenv.InteractiveInfo.ServerConnect).ServerPort;
        }

        private void SetMinStatus()
        {
            this.sys_env = SystemEnvironment.CreateInstance();
            base.usrenv = UserEnvironment.CreateInstance();
            this.ServerSet(base.usrenv.InteractiveInfo.ServerConnect);
            if (!Directory.Exists(base.pi.LogPath))
            {
                Directory.CreateDirectory(base.pi.LogPath);
            }
            this.hts_cls.UseHttps = this.sys_env.TerminalInfo.UseInternet;
            this.hts_cls.UseProxy = base.usrenv.HttpOption.UseProxy;
            this.hts_cls.ProxyServer = base.usrenv.HttpOption.ProxyName;
            this.hts_cls.ProxyPort = base.usrenv.HttpOption.ProxyPort;
            this.hts_cls.UseProxyAccount = base.usrenv.HttpOption.UseProxyAccount;
            this.hts_cls.ProxyAccount = base.usrenv.HttpOption.ProxyAccount;
            this.hts_cls.ProxyPassword = base.usrenv.HttpOption.ProxyPassword;
            this.hts_cls.ClientCertificateHash = base.usrenv.HttpOption.CertificateHash;
            this.hts_cls.SendTimeout = this.sys_env.TerminalInfo.SendTimeOut;
            this.hts_cls.UseTrace = base.usrenv.InteractiveInfo.TraceOutput;
            this.hts_cls.TraceFile = base.pi.LogPath + this.sys_env.InteractiveInfo.TraceFileName;
            this.hts_cls.TraceSize = this.sys_env.InteractiveInfo.TraceFileSize;
            this.hts_cls.IsDebug = this.sys_env.TerminalInfo.Debug;
            this.hts_bat.UseHttps = this.sys_env.TerminalInfo.UseInternet;
            this.hts_bat.UseProxy = base.usrenv.HttpOption.UseProxy;
            this.hts_bat.ProxyServer = base.usrenv.HttpOption.ProxyName;
            this.hts_bat.ProxyPort = base.usrenv.HttpOption.ProxyPort;
            this.hts_bat.UseProxyAccount = base.usrenv.HttpOption.UseProxyAccount;
            this.hts_bat.ProxyAccount = base.usrenv.HttpOption.ProxyAccount;
            this.hts_bat.ProxyPassword = base.usrenv.HttpOption.ProxyPassword;
            this.hts_bat.ClientCertificateHash = base.usrenv.HttpOption.CertificateHash;
            this.hts_bat.SendTimeout = this.sys_env.TerminalInfo.SendTimeOut;
            this.hts_bat.UseTrace = base.usrenv.InteractiveInfo.TraceOutput;
            this.hts_bat.TraceFile = base.pi.LogPath + this.sys_env.BatchdocInfo.TraceFileName;
            this.hts_bat.TraceSize = this.sys_env.BatchdocInfo.TraceFileSize;
            this.hts_bat.IsDebug = this.sys_env.TerminalInfo.Debug;
        }

        public override void UpdateMenuItemsInd()
        {
        }
    }
}

