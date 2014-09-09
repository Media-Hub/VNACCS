namespace Naccs.Core.Settings
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="MailInfo")]
    public class MailInfoClass : AbstractSettings
    {
        private bool adddomainname;
        private bool automode;
        private int autotm;
        private int autotmkind;
        public const bool default_adddomainname = true;
        public const bool default_automode = false;
        public const int default_autotm = 3;
        public const int default_autotmkind = 1;
        public const bool default_gatewayadddomainname = true;
        public const int default_gatewaypopserverport = 110;
        public const int default_gatewaysmtpserverport = 0x19;
        public const int default_maxretreivecnt = 0x270f;
        public const string default_serverconnect = "MAIN";
        public const bool default_traceoutput = false;
        public const bool default_usegateway = false;
        private bool gatewayadddomainname;
        private string gatewaymailbox;
        private string gatewaypopdomainname;
        private string gatewaypopservername;
        private int gatewaypopserverport;
        private string gatewaysmtpdomainname;
        private string gatewaysmtpservername;
        private int gatewaysmtpserverport;
        private int maxretreivecnt;
        private string serverconnect;
        private bool traceoutput;
        private bool usegateway;

        [XmlElement(ElementName="AddDomainName"), DefaultValue(true)]
        public bool AddDomainName
        {
            get
            {
                return this.adddomainname;
            }
            set
            {
                if (this.adddomainname != value)
                {
                    base.updateFlg = this.adddomainname != value;
                    this.adddomainname = value;
                }
            }
        }

        [XmlElement(ElementName="AutoMode"), DefaultValue(false)]
        public bool AutoMode
        {
            get
            {
                return this.automode;
            }
            set
            {
                if (this.automode != value)
                {
                    base.updateFlg = this.automode != value;
                    this.automode = value;
                }
            }
        }

        [DefaultValue(3), XmlElement(ElementName="AutoTm")]
        public int AutoTm
        {
            get
            {
                return this.autotm;
            }
            set
            {
                if (this.autotm != value)
                {
                    base.updateFlg = this.autotm != value;
                    this.autotm = value;
                }
            }
        }

        [DefaultValue(1), XmlElement(ElementName="AutoTmKind")]
        public int AutoTmKind
        {
            get
            {
                return this.autotmkind;
            }
            set
            {
                if (this.autotmkind != value)
                {
                    base.updateFlg = this.autotmkind != value;
                    this.autotmkind = value;
                }
            }
        }

        [DefaultValue(true), XmlElement(ElementName="GatewayAddDomainName")]
        public bool GatewayAddDomainName
        {
            get
            {
                return this.gatewayadddomainname;
            }
            set
            {
                if (this.gatewayadddomainname != value)
                {
                    base.updateFlg = this.gatewayadddomainname != value;
                    this.gatewayadddomainname = value;
                }
            }
        }

        [XmlElement(ElementName="GatewayMailBox")]
        public string GatewayMailBox
        {
            get
            {
                return this.gatewaymailbox;
            }
            set
            {
                if (this.gatewaymailbox != value)
                {
                    base.updateFlg = this.gatewaymailbox != value;
                    this.gatewaymailbox = value;
                }
            }
        }

        [XmlElement(ElementName="GatewayPOPDomainName")]
        public string GatewayPOPDomainName
        {
            get
            {
                return this.gatewaypopdomainname;
            }
            set
            {
                if (this.gatewaypopdomainname != value)
                {
                    base.updateFlg = this.gatewaypopdomainname != value;
                    this.gatewaypopdomainname = value;
                }
            }
        }

        [XmlElement(ElementName="GatewayPOPServerName")]
        public string GatewayPOPServerName
        {
            get
            {
                return this.gatewaypopservername;
            }
            set
            {
                if (this.gatewaypopservername != value)
                {
                    base.updateFlg = this.gatewaypopservername != value;
                    this.gatewaypopservername = value;
                }
            }
        }

        [DefaultValue(110), XmlElement(ElementName="GatewayPOPServerPort")]
        public int GatewayPOPServerPort
        {
            get
            {
                return this.gatewaypopserverport;
            }
            set
            {
                if (this.gatewaypopserverport != value)
                {
                    base.updateFlg = this.gatewaypopserverport != value;
                    this.gatewaypopserverport = value;
                }
            }
        }

        [XmlElement(ElementName="GatewaySMTPDomainName")]
        public string GatewaySMTPDomainName
        {
            get
            {
                return this.gatewaysmtpdomainname;
            }
            set
            {
                if (this.gatewaysmtpdomainname != value)
                {
                    base.updateFlg = this.gatewaysmtpdomainname != value;
                    this.gatewaysmtpdomainname = value;
                }
            }
        }

        [XmlElement(ElementName="GatewaySMTPServerName")]
        public string GatewaySMTPServerName
        {
            get
            {
                return this.gatewaysmtpservername;
            }
            set
            {
                if (this.gatewaysmtpservername != value)
                {
                    base.updateFlg = this.gatewaysmtpservername != value;
                    this.gatewaysmtpservername = value;
                }
            }
        }

        [XmlElement(ElementName="GatewaySMTPServerPort"), DefaultValue(0x19)]
        public int GatewaySMTPServerPort
        {
            get
            {
                return this.gatewaysmtpserverport;
            }
            set
            {
                if (this.gatewaysmtpserverport != value)
                {
                    base.updateFlg = this.gatewaysmtpserverport != value;
                    this.gatewaysmtpserverport = value;
                }
            }
        }

        [XmlElement(ElementName="MaxRetreiveCnt"), DefaultValue(0x270f)]
        public int MaxRetreiveCnt
        {
            get
            {
                return this.maxretreivecnt;
            }
            set
            {
                if (this.maxretreivecnt != value)
                {
                    base.updateFlg = this.maxretreivecnt != value;
                    this.maxretreivecnt = value;
                }
            }
        }

        [DefaultValue("MAIN"), XmlElement(ElementName="ServerConnect")]
        public string ServerConnect
        {
            get
            {
                return this.serverconnect;
            }
            set
            {
                if (this.serverconnect != value)
                {
                    base.updateFlg = this.serverconnect != value;
                    this.serverconnect = value;
                }
            }
        }

        [DefaultValue(false), XmlElement(ElementName="TraceOutput")]
        public bool TraceOutput
        {
            get
            {
                return this.traceoutput;
            }
            set
            {
                if (this.traceoutput != value)
                {
                    base.updateFlg = this.traceoutput != value;
                    this.traceoutput = value;
                }
            }
        }

        [XmlElement(ElementName="UseGateway"), DefaultValue(false)]
        public bool UseGateway
        {
            get
            {
                return this.usegateway;
            }
            set
            {
                if (this.usegateway != value)
                {
                    base.updateFlg = this.usegateway != value;
                    this.usegateway = value;
                }
            }
        }
    }
}

