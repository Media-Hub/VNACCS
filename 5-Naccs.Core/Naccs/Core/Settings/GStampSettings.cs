namespace Naccs.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="GStompSettings")]
    public class GStampSettings : AbstractConfig
    {
        public static Type[] ExtraTypes = new Type[] { typeof(GStampInfoClass) };
        private GStampInfoClass gstampInfo = new GStampInfoClass();
        private static GStampSettings singletonInstance = null;

        public static GStampSettings CreateInstance()
        {
            if (singletonInstance == null)
            {
                PathInfo info = PathInfo.CreateInstance();
                singletonInstance = AbstractConfig.Load(typeof(GStampSettings), ExtraTypes, info.GStampIngo) as GStampSettings;
                if (singletonInstance == null)
                {
                    singletonInstance = new GStampSettings();
                    singletonInstance.Initialize();
                }
            }
            return singletonInstance;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public static GStampSettings ResetInstance()
        {
            PathInfo info = PathInfo.CreateInstance();
            singletonInstance = AbstractConfig.Load(typeof(GStampSettings), ExtraTypes, info.GStampIngo) as GStampSettings;
            if (singletonInstance == null)
            {
                singletonInstance = new GStampSettings();
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
                return PathInfo.CreateInstance().GStampIngo;
            }
        }

        public GStampInfoClass GStampInfo
        {
            get
            {
                return this.gstampInfo;
            }
            set
            {
                this.gstampInfo = value;
            }
        }

        [XmlIgnore]
        public override bool UpdateFlg
        {
            get
            {
                if (!base.UpdateFlg)
                {
                    return this.gstampInfo.UpdateFlg;
                }
                return true;
            }
            set
            {
                base.UpdateFlg = value;
                if (!value)
                {
                    this.gstampInfo.UpdateFlg = value;
                }
            }
        }
    }
}

