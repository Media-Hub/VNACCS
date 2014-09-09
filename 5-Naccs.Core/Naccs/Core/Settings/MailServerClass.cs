namespace Naccs.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    public class MailServerClass
    {
        private string connectname = "";
        private string displayname = "";
        private string domainname = "";
        private string mailbox = "";
        private string pingpoint = "";
        private string popservername = "";
        private int popserverport = 110;
        private string smtpservername = "";
        private int smtpserverport = 0x19;

        [XmlAttribute(AttributeName="display")]
        public string display
        {
            get
            {
                return this.displayname;
            }
            set
            {
                this.displayname = value;
            }
        }

        [XmlElement(ElementName="DomainName")]
        public string DomainName
        {
            get
            {
                return this.domainname;
            }
            set
            {
                this.domainname = value;
            }
        }

        [XmlElement(ElementName="MailBox")]
        public string MailBox
        {
            get
            {
                return this.mailbox;
            }
            set
            {
                this.mailbox = value;
            }
        }

        [XmlAttribute(AttributeName="name")]
        public string name
        {
            get
            {
                return this.connectname;
            }
            set
            {
                this.connectname = value;
            }
        }

        [XmlElement(ElementName="PingPoint")]
        public string PingPoint
        {
            get
            {
                return this.pingpoint;
            }
            set
            {
                this.pingpoint = value;
            }
        }

        [XmlElement(ElementName="POPServerName")]
        public string POPServerName
        {
            get
            {
                return this.popservername;
            }
            set
            {
                this.popservername = value;
            }
        }

        [XmlElement(ElementName="POPServerPort")]
        public int POPServerPort
        {
            get
            {
                return this.popserverport;
            }
            set
            {
                this.popserverport = value;
            }
        }

        [XmlElement(ElementName="SMTPServerName")]
        public string SMTPServerName
        {
            get
            {
                return this.smtpservername;
            }
            set
            {
                this.smtpservername = value;
            }
        }

        [XmlElement(ElementName="SMTPServerPort")]
        public int SMTPServerPort
        {
            get
            {
                return this.smtpserverport;
            }
            set
            {
                this.smtpserverport = value;
            }
        }
    }
}

