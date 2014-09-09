namespace Naccs.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="Sound")]
    public class SoundClass : AbstractSettings
    {
        private string soundfile;

        [XmlElement(ElementName="SoundFile")]
        public string SoundFile
        {
            get
            {
                return this.soundfile;
            }
            set
            {
                base.updateFlg = this.soundfile != value;
                this.soundfile = value;
            }
        }
    }
}

