namespace Naccs.Core.Settings
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="ColumnWidth")]
    public class ColumnWidthClass : AbstractSettings
    {
        private int airsea;
        private int datatype;
        public const int default_airsea = 30;
        public const int default_datatype = 40;
        public const int default_endflag = 40;
        public const int default_inputinfo = 80;
        public const int default_jobcode = 80;
        public const int default_jobinfo = 170;
        public const int default_joninfo1 = 0x91;
        public const int default_joninfo2 = 0x24;
        public const int default_joninfo3 = 0x72;
        public const int default_outcode = 60;
        public const int default_pattern = 40;
        public const int default_printname = 300;
        public const int default_rescode = 0x69;
        public const int default_timestamp = 0x80;
        public const int default_tvfolder = 0xaf;
        private int endflag;
        private int inputinfo;
        private int jobcode;
        private int jobinfo;
        private int joninfo1;
        private int joninfo2;
        private int joninfo3;
        private int outcode;
        private int pattern;
        private int printname;
        private int rescode;
        private int timestamp;
        private int tvfolder;

        [DefaultValue(30), XmlElement(ElementName="AirSea")]
        public int AirSea
        {
            get
            {
                return this.airsea;
            }
            set
            {
                base.updateFlg = this.airsea != value;
                this.airsea = value;
            }
        }

        [DefaultValue(40), XmlElement(ElementName="DataType")]
        public int DataType
        {
            get
            {
                return this.datatype;
            }
            set
            {
                base.updateFlg = this.datatype != value;
                this.datatype = value;
            }
        }

        [DefaultValue(40), XmlElement(ElementName="EndFlag")]
        public int EndFlag
        {
            get
            {
                return this.endflag;
            }
            set
            {
                base.updateFlg = this.endflag != value;
                this.endflag = value;
            }
        }

        [DefaultValue(80), XmlElement(ElementName="InputInfo")]
        public int InputInfo
        {
            get
            {
                return this.inputinfo;
            }
            set
            {
                base.updateFlg = this.inputinfo != value;
                this.inputinfo = value;
            }
        }

        [DefaultValue(80), XmlElement(ElementName="JobCode")]
        public int JobCode
        {
            get
            {
                return this.jobcode;
            }
            set
            {
                base.updateFlg = this.jobcode != value;
                this.jobcode = value;
            }
        }

        [XmlElement(ElementName="JobInfo"), DefaultValue(170)]
        public int JobInfo
        {
            get
            {
                return this.jobinfo;
            }
            set
            {
                base.updateFlg = this.jobinfo != value;
                this.jobinfo = value;
            }
        }

        [XmlElement(ElementName="JonInfo1"), DefaultValue(0x91)]
        public int JonInfo1
        {
            get
            {
                return this.joninfo1;
            }
            set
            {
                base.updateFlg = this.joninfo1 != value;
                this.joninfo1 = value;
            }
        }

        [DefaultValue(0x24), XmlElement(ElementName="JonInfo2")]
        public int JonInfo2
        {
            get
            {
                return this.joninfo2;
            }
            set
            {
                base.updateFlg = this.joninfo2 != value;
                this.joninfo2 = value;
            }
        }

        [DefaultValue(0x72), XmlElement(ElementName="JonInfo3")]
        public int JonInfo3
        {
            get
            {
                return this.joninfo3;
            }
            set
            {
                base.updateFlg = this.joninfo3 != value;
                this.joninfo3 = value;
            }
        }

        [DefaultValue(60), XmlElement(ElementName="OutCode")]
        public int OutCode
        {
            get
            {
                return this.outcode;
            }
            set
            {
                base.updateFlg = this.outcode != value;
                this.outcode = value;
            }
        }

        [DefaultValue(40), XmlElement(ElementName="Pattern")]
        public int Pattern
        {
            get
            {
                return this.pattern;
            }
            set
            {
                base.updateFlg = this.pattern != value;
                this.pattern = value;
            }
        }

        [DefaultValue(300), XmlElement(ElementName="PrintName")]
        public int PrintName
        {
            get
            {
                return this.printname;
            }
            set
            {
                base.updateFlg = this.printname != value;
                this.printname = value;
            }
        }

        [XmlElement(ElementName="ResCode"), DefaultValue(0x69)]
        public int ResCode
        {
            get
            {
                return this.rescode;
            }
            set
            {
                base.updateFlg = this.rescode != value;
                this.rescode = value;
            }
        }

        [XmlElement(ElementName="TimeStamp"), DefaultValue(0x80)]
        public int TimeStamp
        {
            get
            {
                return this.timestamp;
            }
            set
            {
                base.updateFlg = this.timestamp != value;
                this.timestamp = value;
            }
        }

        [DefaultValue(0xaf), XmlElement(ElementName="TVFolder")]
        public int TVFolder
        {
            get
            {
                return this.tvfolder;
            }
            set
            {
                base.updateFlg = this.tvfolder != value;
                this.tvfolder = value;
            }
        }
    }
}

