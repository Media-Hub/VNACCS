namespace Naccs.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="Term")]
    public class TermClass : ListItem, ICloneable
    {
        private string banknumber;
        private bool bankwithdrawday;
        public const string default_banknumber = "";
        public const bool default_bankwithdrawday = false;
        public const bool default_unopen = false;
        private string folder;
        private int id;
        private string jobcoode;
        private string outcode;
        private int priority;
        private bool unopen;
        private string usercode;

        public object Clone()
        {
            return base.MemberwiseClone();
        }

        [XmlElement(ElementName="BankNumber")]
        public string BankNumber
        {
            get
            {
                return this.banknumber;
            }
            set
            {
                if (this.banknumber != value)
                {
                    this.banknumber = value;
                    base.Change();
                }
            }
        }

        [XmlElement(ElementName="BankWithdrawday")]
        public bool BankWithdrawday
        {
            get
            {
                return this.bankwithdrawday;
            }
            set
            {
                if (this.bankwithdrawday != value)
                {
                    this.bankwithdrawday = value;
                    base.Change();
                }
            }
        }

        [XmlElement(ElementName="Folder")]
        public string Folder
        {
            get
            {
                return this.folder;
            }
            set
            {
                if (this.folder != value)
                {
                    this.folder = value;
                    base.Change();
                }
            }
        }

        [XmlAttribute(AttributeName="Id")]
        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                if (this.id != value)
                {
                    base.Change();
                }
                this.id = value;
            }
        }

        [XmlElement(ElementName="JobCoode")]
        public string JobCoode
        {
            get
            {
                return this.jobcoode;
            }
            set
            {
                if (this.jobcoode != value)
                {
                    this.jobcoode = value;
                    base.Change();
                }
            }
        }

        [XmlElement(ElementName="OutCode")]
        public string OutCode
        {
            get
            {
                return this.outcode;
            }
            set
            {
                if (this.outcode != value)
                {
                    this.outcode = value;
                    base.Change();
                }
            }
        }

        [XmlElement(ElementName="Priority")]
        public int Priority
        {
            get
            {
                return this.priority;
            }
            set
            {
                if (this.priority != value)
                {
                    base.Change();
                }
                this.priority = value;
            }
        }

        [XmlElement(ElementName="UnOpen")]
        public bool UnOpen
        {
            get
            {
                return this.unopen;
            }
            set
            {
                if (this.unopen != value)
                {
                    this.unopen = value;
                    base.Change();
                }
            }
        }

        [XmlElement(ElementName="UserCode")]
        public string UserCode
        {
            get
            {
                return this.usercode;
            }
            set
            {
                if (this.usercode != value)
                {
                    base.Change();
                }
                this.usercode = value;
            }
        }
    }
}

