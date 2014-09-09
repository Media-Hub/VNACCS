namespace Naccs.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="HistorySearch")]
    public class HistorySearchClass : ListItem, ICloneable
    {
        private int no;
        private string serchinfo;

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
                    base.Change();
                    this.no = value;
                }
            }
        }

        [XmlElement(ElementName="SerchInfo")]
        public string SerchInfo
        {
            get
            {
                return this.serchinfo;
            }
            set
            {
                if (this.serchinfo != value)
                {
                    this.serchinfo = value;
                    base.Change();
                }
            }
        }
    }
}

