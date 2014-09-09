namespace Naccs.Core.Settings
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="Inform")]
    public class InformClass : AbstractSettings
    {
        public const bool default_infomode = false;
        private bool infomode;

        [DefaultValue(false), XmlElement(ElementName="InfoMode")]
        public bool InfoMode
        {
            get
            {
                return this.infomode;
            }
            set
            {
                base.updateFlg = this.infomode != value;
                this.infomode = value;
            }
        }
    }
}

