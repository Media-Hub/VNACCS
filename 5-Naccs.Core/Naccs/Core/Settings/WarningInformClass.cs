namespace Naccs.Core.Settings
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="WarningInform")]
    public class WarningInformClass : AbstractSettings
    {
        public const bool default_waringmode = true;
        private bool waringmode;

        [XmlElement(ElementName="WaringMode"), DefaultValue(true)]
        public bool WaringMode
        {
            get
            {
                return this.waringmode;
            }
            set
            {
                base.updateFlg = this.waringmode != value;
                this.waringmode = value;
            }
        }
    }
}

