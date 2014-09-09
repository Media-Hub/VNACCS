namespace Naccs.Core.Settings
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="CommonPath")]
    public class CommonPathClass : AbstractSettings
    {
        public const bool default_specify = false;
        private string folder;
        private bool specify;

        [XmlElement(ElementName="Folder")]
        public string Folder
        {
            get
            {
                return this.folder;
            }
            set
            {
                base.updateFlg = this.folder != value;
                this.folder = value;
            }
        }

        [XmlElement(ElementName="Specify"), DefaultValue(false)]
        public bool Specify
        {
            get
            {
                return this.specify;
            }
            set
            {
                base.updateFlg = this.specify != value;
                this.specify = value;
            }
        }
    }
}

