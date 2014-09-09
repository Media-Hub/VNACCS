namespace Naccs.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="OptionCertification")]
    public class OptionCertification : AbstractConfig
    {
        private AllCertificationClass allCertification = new AllCertificationClass();
        public static Type[] ExtraTypes = new Type[] { typeof(AllCertificationClass) };
        private static OptionCertification singletonInstance = null;

        public static OptionCertification CreateInstance()
        {
            if (singletonInstance == null)
            {
                PathInfo info = PathInfo.CreateInstance();
                singletonInstance = AbstractConfig.Load(typeof(OptionCertification), ExtraTypes, info.OptioncertificationSetup) as OptionCertification;
                if (singletonInstance == null)
                {
                    singletonInstance = new OptionCertification();
                    singletonInstance.Initialize();
                }
            }
            return singletonInstance;
        }

        public static bool IsCertificate(int DivInt)
        {
            PathInfo info = PathInfo.CreateInstance();
            OptionCertification certification = AbstractConfig.Load(typeof(OptionCertification), ExtraTypes, info.getOptionCertification(DivInt)) as OptionCertification;
            if (certification == null)
            {
                return false;
            }
            return !string.IsNullOrEmpty(certification.AllCertification.Password);
        }

        public static OptionCertification ResetInstance()
        {
            PathInfo info = PathInfo.CreateInstance();
            singletonInstance = AbstractConfig.Load(typeof(OptionCertification), ExtraTypes, info.OptioncertificationSetup) as OptionCertification;
            if (singletonInstance == null)
            {
                singletonInstance = new OptionCertification();
                singletonInstance.Initialize();
            }
            return singletonInstance;
        }

        public AllCertificationClass AllCertification
        {
            get
            {
                return this.allCertification;
            }
            set
            {
                this.allCertification = value;
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
                return PathInfo.CreateInstance().OptioncertificationSetup;
            }
        }

        [XmlIgnore]
        public override bool UpdateFlg
        {
            get
            {
                if (!base.UpdateFlg)
                {
                    return this.allCertification.UpdateFlg;
                }
                return true;
            }
            set
            {
                base.UpdateFlg = value;
                if (!value)
                {
                    this.allCertification.UpdateFlg = value;
                }
            }
        }
    }
}

