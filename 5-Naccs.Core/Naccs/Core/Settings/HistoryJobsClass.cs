namespace Naccs.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="HistoryJobs")]
    public class HistoryJobsClass : ListItem, ICloneable
    {
        public const int default_fontsize = 9;
        private int fontsize;
        private string jobcode;
        private int no;

        public object Clone()
        {
            return base.MemberwiseClone();
        }

        [XmlElement(ElementName="FontSize")]
        public int FontSize
        {
            get
            {
                return this.fontsize;
            }
            set
            {
                if (this.fontsize != value)
                {
                    this.fontsize = value;
                    base.Change();
                }
            }
        }

        [XmlElement(ElementName="JobCode")]
        public string JobCode
        {
            get
            {
                return this.jobcode;
            }
            set
            {
                if (this.jobcode != value)
                {
                    this.jobcode = value;
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

