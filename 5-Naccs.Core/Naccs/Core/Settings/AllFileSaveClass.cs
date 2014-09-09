namespace Naccs.Core.Settings
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="AllFileSave")]
    public class AllFileSaveClass : AbstractSettings
    {
        public const bool default_mode = false;
        private bool mode;

        [DefaultValue(false), XmlElement(ElementName="Mode")]
        public bool Mode
        {
            get
            {
                return this.mode;
            }
            set
            {
                base.updateFlg = this.mode != value;
                this.mode = value;
            }
        }
    }
}

