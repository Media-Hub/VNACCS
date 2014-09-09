namespace Naccs.Core.Settings
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="LastLogon")]
    public class LastLogonClass : AbstractSettings
    {
        private string app;
        public const bool default_sso = false;
        public const bool default_uppercase = true;
        private string pop;
        private bool sso;
        private bool uppercase;

        [DefaultValue(""), XmlElement(ElementName="APP")]
        public string APP
        {
            get
            {
                return this.app;
            }
            set
            {
                base.updateFlg = this.app != value;
                this.app = value;
            }
        }

        [XmlElement(ElementName="POP"), DefaultValue("")]
        public string POP
        {
            get
            {
                return this.pop;
            }
            set
            {
                base.updateFlg = this.pop != value;
                this.pop = value;
            }
        }

        [DefaultValue(false), XmlElement(ElementName="SSO")]
        public bool SSO
        {
            get
            {
                return this.sso;
            }
            set
            {
                base.updateFlg = this.sso != value;
                this.sso = value;
            }
        }

        [DefaultValue(true), XmlElement(ElementName="UPPERCASE")]
        public bool UPPERCASE
        {
            get
            {
                return this.uppercase;
            }
            set
            {
                base.updateFlg = this.uppercase != value;
                this.uppercase = value;
            }
        }
    }
}

