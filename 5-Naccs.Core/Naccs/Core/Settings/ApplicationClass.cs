namespace Naccs.Core.Settings
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="Application")]
    public class ApplicationClass : AbstractSettings
    {
        private int bottom;
        private bool clock;
        private bool closeonappend;
        private int dataviewfontsize;
        private int dataviewsplitter;
        public const int default_bottom = -1;
        public const bool default_clock = false;
        public const bool default_closeonappend = false;
        public const int default_dataviewfontsize = 2;
        public const int default_dataviewsplitter = 150;
        public const int default_left = -1;
        public const bool default_listrestriction = true;
        public const bool default_logoffconfirm = true;
        public const int default_mainsplitter = 200;
        public const bool default_maximized = false;
        public const int default_maxretreivecnt = 0x270f;
        public const int default_right = -1;
        public const SettingDivType default_settingsdiv = SettingDivType.User;
        public const int default_top = -1;
        private int left;
        private bool listrestriction;
        private bool logoffconfirm;
        private int mainsplitter;
        private bool maximized;
        private int maxretreivecnt;
        private int right;
        private SettingDivType settingsdiv;
        private int top;

        public event EventHandler OnSettingDivChanged;

        [XmlElement(ElementName="Bottom"), DefaultValue(-1)]
        public int Bottom
        {
            get
            {
                return this.bottom;
            }
            set
            {
                base.updateFlg = this.bottom != value;
                this.bottom = value;
            }
        }

        [XmlElement(ElementName="Clock"), DefaultValue(false)]
        public bool Clock
        {
            get
            {
                return this.clock;
            }
            set
            {
                base.updateFlg = this.clock != value;
                this.clock = value;
            }
        }

        [XmlElement(ElementName="CloseOnAppend"), DefaultValue(false)]
        public bool CloseOnAppend
        {
            get
            {
                return this.closeonappend;
            }
            set
            {
                base.updateFlg = this.closeonappend != value;
                this.closeonappend = value;
            }
        }

        [DefaultValue(2), XmlElement(ElementName="DataViewFontSize")]
        public int DataViewFontSize
        {
            get
            {
                return this.dataviewfontsize;
            }
            set
            {
                base.updateFlg = this.dataviewfontsize != value;
                this.dataviewfontsize = value;
            }
        }

        [XmlElement(ElementName="DataViewSplitter"), DefaultValue(150)]
        public int DataViewSplitter
        {
            get
            {
                return this.dataviewsplitter;
            }
            set
            {
                base.updateFlg = this.dataviewsplitter != value;
                this.dataviewsplitter = value;
            }
        }

        [XmlElement(ElementName="Left"), DefaultValue(-1)]
        public int Left
        {
            get
            {
                return this.left;
            }
            set
            {
                base.updateFlg = this.left != value;
                this.left = value;
            }
        }

        [XmlIgnore]
        public bool ListRestriction
        {
            get
            {
                return this.listrestriction;
            }
            set
            {
                base.updateFlg = this.listrestriction != value;
                this.listrestriction = value;
            }
        }

        [DefaultValue(true), XmlElement(ElementName="LogoffConfirm")]
        public bool LogoffConfirm
        {
            get
            {
                return this.logoffconfirm;
            }
            set
            {
                base.updateFlg = this.logoffconfirm != value;
                this.logoffconfirm = value;
            }
        }

        [XmlElement(ElementName="MainSplitter"), DefaultValue(200)]
        public int MainSplitter
        {
            get
            {
                return this.mainsplitter;
            }
            set
            {
                base.updateFlg = this.mainsplitter != value;
                this.mainsplitter = value;
            }
        }

        [DefaultValue(false), XmlElement(ElementName="Maximized")]
        public bool Maximized
        {
            get
            {
                return this.maximized;
            }
            set
            {
                base.updateFlg = this.maximized != value;
                this.maximized = value;
            }
        }

        [XmlElement(ElementName="MaxRetreiveCnt"), DefaultValue(0x270f)]
        public int MaxRetreiveCnt
        {
            get
            {
                return this.maxretreivecnt;
            }
            set
            {
                base.updateFlg = this.maxretreivecnt != value;
                this.maxretreivecnt = value;
            }
        }

        [XmlElement(ElementName="Right"), DefaultValue(-1)]
        public int Right
        {
            get
            {
                return this.right;
            }
            set
            {
                base.updateFlg = this.right != value;
                this.right = value;
            }
        }

        [XmlElement(ElementName="SettingsDiv"), DefaultValue(0)]
        public SettingDivType SettingsDiv
        {
            get
            {
                return this.settingsdiv;
            }
            set
            {
                base.updateFlg = this.settingsdiv != value;
                this.settingsdiv = value;
                if (base.updateFlg && (this.OnSettingDivChanged != null))
                {
                    this.OnSettingDivChanged(this, new EventArgs());
                }
            }
        }

        [XmlElement(ElementName="Top"), DefaultValue(-1)]
        public int Top
        {
            get
            {
                return this.top;
            }
            set
            {
                base.updateFlg = this.top != value;
                this.top = value;
            }
        }
    }
}

