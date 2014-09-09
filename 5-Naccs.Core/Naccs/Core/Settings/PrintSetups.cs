namespace Naccs.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="Printer")]
    public class PrintSetups : AbstractConfig
    {
        private DefaultInfoClass defaultInfo = new DefaultInfoClass();
        public static Type[] ExtraTypes = new Type[] { typeof(PrinterInfoClass), typeof(BinClass), typeof(OutCodeClass) };
        private FormsClass forms = new FormsClass();
        private PrinterInfoListClass printerInfoList = new PrinterInfoListClass();
        private static PrintSetups singletonInstance = null;

        public static PrintSetups CreateInstance()
        {
            if (singletonInstance == null)
            {
                PathInfo info = PathInfo.CreateInstance();
                singletonInstance = AbstractConfig.Load(typeof(PrintSetups), ExtraTypes, info.PrinterSetup) as PrintSetups;
                if (singletonInstance == null)
                {
                    singletonInstance = new PrintSetups();
                    singletonInstance.Initialize();
                }
            }
            return singletonInstance;
        }

        public override void Initialize()
        {
            this.defaultInfo.Initialize();
            this.forms.Initialize();
            this.printerInfoList.Clear();
        }

        public static PrintSetups ResetInstance()
        {
            PathInfo info = PathInfo.CreateInstance();
            singletonInstance = AbstractConfig.Load(typeof(PrintSetups), ExtraTypes, info.PrinterSetup) as PrintSetups;
            if (singletonInstance == null)
            {
                singletonInstance = new PrintSetups();
                singletonInstance.Initialize();
            }
            return singletonInstance;
        }

        public DefaultInfoClass DefaultInfo
        {
            get
            {
                return this.defaultInfo;
            }
            set
            {
                this.defaultInfo = value;
            }
        }

        protected override Type[] extraTypes
        {
            get
            {
                return ExtraTypes;
            }
        }

        protected override string FileName
        {
            get
            {
                return PathInfo.CreateInstance().PrinterSetup;
            }
        }

        public FormsClass Forms
        {
            get
            {
                return this.forms;
            }
            set
            {
                this.forms = value;
            }
        }

        [XmlArrayItem(typeof(PrinterInfoClass), ElementName="PrinterInfo")]
        public PrinterInfoListClass PrinterInfoList
        {
            get
            {
                return this.printerInfoList;
            }
            set
            {
                this.printerInfoList = value;
            }
        }

        [XmlIgnore]
        public override bool UpdateFlg
        {
            get
            {
                if ((!base.UpdateFlg && !this.printerInfoList.UpdateFlg) && !this.defaultInfo.UpdateFlg)
                {
                    return this.forms.UpdateFlg;
                }
                return true;
            }
            set
            {
                base.UpdateFlg = value;
                if (!value)
                {
                    this.defaultInfo.UpdateFlg = value;
                    this.forms.UpdateFlg = value;
                    this.printerInfoList.UpdateFlg = value;
                }
            }
        }
    }
}

