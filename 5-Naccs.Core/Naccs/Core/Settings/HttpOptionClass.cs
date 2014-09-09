namespace Naccs.Core.Settings
{
    using System;
    using System.ComponentModel;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="HttpOption")]
    public class HttpOptionClass : AbstractSettings
    {
        private string certificatehash;
        public const int default_proxyport = 0x1f90;
        public const bool default_useproxy = false;
        public const bool default_useproxyaccount = false;
        private string proxyaccount;
        private string proxyname;
        private string proxypassword;
        private int proxyport;
        private bool useproxy;
        private bool useproxyaccount;

        [XmlElement(ElementName="CertificateHash")]
        public string CertificateHash
        {
            get
            {
                return this.certificatehash;
            }
            set
            {
                if (this.certificatehash != value)
                {
                    base.updateFlg = this.certificatehash != value;
                    this.certificatehash = value;
                }
            }
        }

        [XmlElement(ElementName="ProxyPassword")]
        public byte[] EncryptProxyPassword
        {
            get
            {
                if ((this.proxypassword != null) && !(this.proxypassword == ""))
                {
                    return Encoding.Unicode.GetBytes(this.proxypassword);
                }
                return null;
            }
            set
            {
                this.proxypassword = Encoding.Unicode.GetString(value);
            }
        }

        [XmlElement(ElementName="ProxyAccount")]
        public string ProxyAccount
        {
            get
            {
                return this.proxyaccount;
            }
            set
            {
                if (this.proxyaccount != value)
                {
                    base.updateFlg = this.proxyaccount != value;
                    this.proxyaccount = value;
                }
            }
        }

        [XmlElement(ElementName="ProxyName")]
        public string ProxyName
        {
            get
            {
                return this.proxyname;
            }
            set
            {
                if (this.proxyname != value)
                {
                    base.updateFlg = this.proxyname != value;
                    this.proxyname = value;
                }
            }
        }

        [XmlIgnore]
        public string ProxyPassword
        {
            get
            {
                return this.proxypassword;
            }
            set
            {
                if (this.proxypassword != value)
                {
                    base.updateFlg = this.proxypassword != value;
                    this.proxypassword = value;
                }
            }
        }

        [DefaultValue(0x1f90), XmlElement(ElementName="ProxyPort")]
        public int ProxyPort
        {
            get
            {
                return this.proxyport;
            }
            set
            {
                if (this.proxyport != value)
                {
                    base.updateFlg = this.proxyport != value;
                    this.proxyport = value;
                }
            }
        }

        [DefaultValue(false), XmlElement(ElementName="UseProxy")]
        public bool UseProxy
        {
            get
            {
                return this.useproxy;
            }
            set
            {
                if (this.useproxy != value)
                {
                    base.updateFlg = this.useproxy != value;
                    this.useproxy = value;
                }
            }
        }

        [XmlElement(ElementName="UseProxyAccount"), DefaultValue(false)]
        public bool UseProxyAccount
        {
            get
            {
                return this.useproxyaccount;
            }
            set
            {
                if (this.useproxyaccount != value)
                {
                    base.updateFlg = this.useproxyaccount != value;
                    this.useproxyaccount = value;
                }
            }
        }
    }
}

