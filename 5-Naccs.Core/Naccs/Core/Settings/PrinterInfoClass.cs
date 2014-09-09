namespace Naccs.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="PrinterInfo")]
    public class PrinterInfoClass : ListItem, ICloneable
    {
        private BinListClass binList = new BinListClass();
        public const bool default_doubleprint = false;
        private bool doubleprint;
        private string name;

        public object Clone()
        {
            return base.MemberwiseClone();
        }

        [XmlArrayItem(typeof(BinClass), ElementName="Bin")]
        public BinListClass BinList
        {
            get
            {
                return this.binList;
            }
            set
            {
                this.binList = value;
            }
        }

        [XmlElement(ElementName="DoublePrint")]
        public bool DoublePrint
        {
            get
            {
                return this.doubleprint;
            }
            set
            {
                if (this.doubleprint != value)
                {
                    this.doubleprint = value;
                    base.Change();
                }
            }
        }

        [XmlAttribute(AttributeName="Name")]
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    base.Change();
                }
            }
        }
    }
}

