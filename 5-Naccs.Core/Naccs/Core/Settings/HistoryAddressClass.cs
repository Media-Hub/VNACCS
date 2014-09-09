namespace Naccs.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="HistoryAddress")]
    public class HistoryAddressClass : ListItem, ICloneable
    {
        private string address;
        private int no;

        public object Clone()
        {
            return base.MemberwiseClone();
        }

        [XmlElement(ElementName="Address")]
        public string Address
        {
            get
            {
                return this.address;
            }
            set
            {
                if (this.address != value)
                {
                    this.address = value;
                    base.Change();
                }
            }
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
    }
}

