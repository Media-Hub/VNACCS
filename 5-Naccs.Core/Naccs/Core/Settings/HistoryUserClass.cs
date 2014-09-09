namespace Naccs.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="HistoryUser")]
    public class HistoryUserClass : ListItem, ICloneable
    {
        private int no;
        private string usercode;

        public object Clone()
        {
            return base.MemberwiseClone();
        }

        [XmlAttribute(AttributeName="No")]
        public int No
        {
            get
            {
                return this.no;
            }
            set
            {
                if (this.no != value)
                {
                    this.no = value;
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
                    this.usercode = value;
                    base.Change();
                }
            }
        }
    }
}

