namespace Naccs.Core.Settings
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="TerminalInfo")]
    public class TerminalInfoClass : AbstractSettings
    {
        private bool autobackup;
        public const bool default_autobackup = false;
        public const int default_diskwarning = 100;
        public const int default_saveterm = 30;
        public const bool default_versionup = false;
        private int diskwarning;
        private int saveterm;
        private string termaccesskey;
        private string termlogicalname;
        private bool versionup;

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

        [DefaultValue(100), XmlElement(ElementName="DiskWarning")]
        public int DiskWarning
        {
            get
            {
                return this.diskwarning;
            }
            set
            {
                if (this.diskwarning != value)
                {
                    base.updateFlg = this.diskwarning != value;
                    this.diskwarning = value;
                }
            }
        }

        [DefaultValue(30), XmlElement(ElementName="SaveTerm")]
        public int SaveTerm
        {
            get
            {
                return this.saveterm;
            }
            set
            {
                if (this.saveterm != value)
                {
                    base.updateFlg = this.saveterm != value;
                    this.saveterm = value;
                }
            }
        }

        [XmlElement(ElementName="TermAccessKey")]
        public string TermAccessKey
        {
            get
            {
                return this.termaccesskey;
            }
            set
            {
                if (this.termaccesskey != value)
                {
                    base.updateFlg = this.termaccesskey != value;
                    this.termaccesskey = value;
                }
            }
        }

        [XmlElement(ElementName="TermLogicalName")]
        public string TermLogicalName
        {
            get
            {
                return this.termlogicalname;
            }
            set
            {
                if (this.termlogicalname != value)
                {
                    base.updateFlg = this.termlogicalname != value;
                    this.termlogicalname = value;
                }
            }
        }

        [XmlElement(ElementName="Versionup"), DefaultValue(false)]
        public bool Versionup
        {
            get
            {
                return this.versionup;
            }
            set
            {
                if (this.versionup != value)
                {
                    base.updateFlg = this.versionup != value;
                    this.versionup = value;
                }
            }
        }
    }
}

