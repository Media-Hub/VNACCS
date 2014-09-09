namespace Naccs.Core.Settings
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="DataView")]
    public class DataViewClass : AbstractSettings
    {
        private bool autobackup;
        public const bool default_autobackup = false;
        private string uptime;

        [DefaultValue(false), XmlElement(ElementName="AutoBackup")]
        public bool AutoBackup
        {
            get
            {
                return this.autobackup;
            }
            set
            {
                if (this.autobackup != value)
                {
                    base.updateFlg = this.autobackup != value;
                    this.autobackup = value;
                }
            }
        }

        [XmlElement(ElementName="upTime")]
        public string upTime
        {
            get
            {
                return this.uptime;
            }
            set
            {
                if (this.uptime != value)
                {
                    base.updateFlg = this.uptime != value;
                    this.uptime = value;
                }
            }
        }
    }
}

