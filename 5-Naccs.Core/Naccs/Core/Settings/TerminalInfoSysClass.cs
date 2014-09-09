namespace Naccs.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    public class TerminalInfoSysClass
    {
        private int a2maximamdelay = 15;
        private int a2minimamdelay = 3;
        private int a2timeout = 180;
        private string backstatusstr = "";
        private int certificatedate = 0x1c;
        private bool debug;
        private bool demo;
        private bool jetras;
        private string portalurl;
        private string printstampmark;
        private ProtocolType protocol;
        private bool receiver;
        private int sendtimeout = 180;
        private string supporturl;
        private string teststatusstr = "";
        private string url;
        private bool useinternet;
        private int userkind = 1;

        [XmlElement(ElementName="A2MaximamDelay")]
        public int A2MaximamDelay
        {
            get
            {
                return this.a2maximamdelay;
            }
            set
            {
                this.a2maximamdelay = value;
            }
        }

        [XmlElement(ElementName="A2MinimamDelay")]
        public int A2MinimamDelay
        {
            get
            {
                return this.a2minimamdelay;
            }
            set
            {
                this.a2minimamdelay = value;
            }
        }

        [XmlElement(ElementName="A2TimeOut")]
        public int A2TimeOut
        {
            get
            {
                return this.a2timeout;
            }
            set
            {
                this.a2timeout = value;
            }
        }

        [XmlElement(ElementName="BackStatusStr")]
        public string BackStatusStr
        {
            get
            {
                return this.backstatusstr;
            }
            set
            {
                this.backstatusstr = value;
            }
        }

        [XmlElement(ElementName="CertificateDate")]
        public int CertificateDate
        {
            get
            {
                return this.certificatedate;
            }
            set
            {
                this.certificatedate = value;
            }
        }

        [XmlElement(ElementName="Debug")]
        public bool Debug
        {
            get
            {
                return this.debug;
            }
            set
            {
                this.debug = value;
            }
        }

        [XmlElement(ElementName="Demo")]
        public bool Demo
        {
            get
            {
                return this.demo;
            }
            set
            {
                this.demo = value;
            }
        }

        [XmlElement(ElementName="Jetras")]
        public bool Jetras
        {
            get
            {
                return this.jetras;
            }
            set
            {
                this.jetras = value;
            }
        }

        [XmlElement(ElementName="PortalURL")]
        public string PortalURL
        {
            get
            {
                return this.portalurl;
            }
            set
            {
                this.portalurl = value;
            }
        }

        [XmlElement(ElementName="PrintStampMark")]
        public string PrintStampMark
        {
            get
            {
                return this.printstampmark;
            }
            set
            {
                this.printstampmark = value;
            }
        }

        [XmlElement(ElementName="Protocol")]
        public ProtocolType Protocol
        {
            get
            {
                return this.protocol;
            }
            set
            {
                this.protocol = value;
            }
        }

        [XmlElement(ElementName="Receiver")]
        public bool Receiver
        {
            get
            {
                return this.receiver;
            }
            set
            {
                this.receiver = value;
            }
        }

        [XmlElement(ElementName="SendTimeOut")]
        public int SendTimeOut
        {
            get
            {
                return this.sendtimeout;
            }
            set
            {
                this.sendtimeout = value;
            }
        }

        [XmlElement(ElementName="SupportURL")]
        public string SupportURL
        {
            get
            {
                return this.supporturl;
            }
            set
            {
                this.supporturl = value;
            }
        }

        [XmlElement(ElementName="TestStatusStr")]
        public string TestStatusStr
        {
            get
            {
                return this.teststatusstr;
            }
            set
            {
                this.teststatusstr = value;
            }
        }

        [XmlElement(ElementName="URL")]
        public string URL
        {
            get
            {
                return this.url;
            }
            set
            {
                this.url = value;
            }
        }

        [XmlElement(ElementName="UseInternet")]
        public bool UseInternet
        {
            get
            {
                return this.useinternet;
            }
            set
            {
                this.useinternet = value;
            }
        }

        [XmlElement(ElementName="UserKind")]
        public int UserKind
        {
            get
            {
                return this.userkind;
            }
            set
            {
                this.userkind = value;
            }
        }
    }
}

