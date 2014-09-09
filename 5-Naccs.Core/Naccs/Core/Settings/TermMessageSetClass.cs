namespace Naccs.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="TermMessageSet")]
    public class TermMessageSetClass : ListItem, ICloneable
    {
        private string button;
        private string code;
        private string description;
        private string disposition;
        private string message;

        public object Clone()
        {
            return base.MemberwiseClone();
        }

        [XmlElement(ElementName="Button")]
        public string Button
        {
            get
            {
                return this.button;
            }
            set
            {
                if (this.button != value)
                {
                    this.button = value;
                    base.Change();
                }
            }
        }

        [XmlAttribute(AttributeName="Code")]
        public string Code
        {
            get
            {
                return this.code;
            }
            set
            {
                if (this.code != value)
                {
                    this.code = value;
                    base.Change();
                }
            }
        }

        [XmlElement(ElementName="Description")]
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                if (this.description != value)
                {
                    this.description = value;
                    base.Change();
                }
            }
        }

        [XmlElement(ElementName="Disposition")]
        public string Disposition
        {
            get
            {
                return this.disposition;
            }
            set
            {
                if (this.disposition != value)
                {
                    this.disposition = value;
                    base.Change();
                }
            }
        }

        [XmlElement(ElementName="Message")]
        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                if (this.message != value)
                {
                    this.message = value;
                    base.Change();
                }
            }
        }
    }
}

