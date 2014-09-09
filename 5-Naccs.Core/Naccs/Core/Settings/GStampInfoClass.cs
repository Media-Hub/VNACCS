namespace Naccs.Core.Settings
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="GStompInfo")]
    public class GStampInfoClass : AbstractSettings
    {
        private string date_alignment;
        private string date_item;
        public const string default_date_alignment = "Left";
        public const string default_gstamp_on = "False";
        public const string default_top_item = "日本銀行歳入代理店";
        private string gstamp_on;
        private string top_item;
        private string under_item1;
        private string under_item2;
        private string under_item3;

        [XmlElement(ElementName="DateAlignment"), DefaultValue("Left")]
        public string DateAlignment
        {
            get
            {
                return this.date_alignment;
            }
            set
            {
                this.date_alignment = value;
            }
        }

        [DefaultValue(""), XmlElement(ElementName="DateItem")]
        public string DateItem
        {
            get
            {
                return this.date_item;
            }
            set
            {
                this.date_item = value;
            }
        }

        [DefaultValue(false), XmlElement(ElementName="GStompOn")]
        public bool GStampOn
        {
            get
            {
                return (string.Compare(this.gstamp_on, "true", true) == 0);
            }
            set
            {
                if (value)
                {
                    this.gstamp_on = "True";
                }
                else
                {
                    this.gstamp_on = "False";
                }
            }
        }

        [XmlElement(ElementName="TopItem"), DefaultValue("日本銀行歳入代理店")]
        public string TopItem
        {
            get
            {
                return this.top_item;
            }
            set
            {
                this.top_item = value;
            }
        }

        [DefaultValue(""), XmlElement(ElementName="UnderItem1")]
        public string UnderItem1
        {
            get
            {
                return this.under_item1;
            }
            set
            {
                this.under_item1 = value;
            }
        }

        [DefaultValue(""), XmlElement(ElementName="UnderItem2")]
        public string UnderItem2
        {
            get
            {
                return this.under_item2;
            }
            set
            {
                this.under_item2 = value;
            }
        }

        [DefaultValue(""), XmlElement(ElementName="UnderItem3")]
        public string UnderItem3
        {
            get
            {
                return this.under_item3;
            }
            set
            {
                this.under_item3 = value;
            }
        }
    }
}

