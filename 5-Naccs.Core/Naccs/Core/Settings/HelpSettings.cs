namespace Naccs.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="Help")]
    public class HelpSettings : AbstractConfig
    {
        public static Type[] ExtraTypes = new Type[] { typeof(HelpPathClass) };
        private HelpPathClass helpPath = new HelpPathClass();
        private static HelpSettings singletonInstance = null;

        public static HelpSettings CreateInstance()
        {
            if (singletonInstance == null)
            {
                PathInfo info = PathInfo.CreateInstance();
                singletonInstance = AbstractConfig.Load(typeof(HelpSettings), ExtraTypes, info.HelpSetup) as HelpSettings;
                if (singletonInstance == null)
                {
                    singletonInstance = new HelpSettings();
                    singletonInstance.Initialize();
                }
            }
            return singletonInstance;
        }

        public override void Initialize()
        {
            base.Initialize();
            this.helpPath.GymErrIndexHtmlUser = null;
            this.helpPath.TermMsgIndexHtmlUser = null;
            this.helpPath.GuideIndexHtmlUser = null;
        }

        public static HelpSettings ResetInstance()
        {
            PathInfo info = PathInfo.CreateInstance();
            singletonInstance = AbstractConfig.Load(typeof(HelpSettings), ExtraTypes, info.HelpSetup) as HelpSettings;
            if (singletonInstance == null)
            {
                singletonInstance = new HelpSettings();
                singletonInstance.Initialize();
            }
            return singletonInstance;
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
                return PathInfo.CreateInstance().HelpSetup;
            }
        }

        public HelpPathClass HelpPath
        {
            get
            {
                return this.helpPath;
            }
            set
            {
                this.helpPath = value;
            }
        }

        [XmlIgnore]
        public override bool UpdateFlg
        {
            get
            {
                if (!base.UpdateFlg)
                {
                    return this.helpPath.UpdateFlg;
                }
                return true;
            }
            set
            {
                base.UpdateFlg = value;
                if (!value)
                {
                    this.helpPath.UpdateFlg = value;
                }
            }
        }
    }
}

