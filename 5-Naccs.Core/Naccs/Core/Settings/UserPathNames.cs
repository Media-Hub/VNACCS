namespace Naccs.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="UserPathNames")]
    public class UserPathNames : AbstractConfig
    {
        private CommonPathClass commonPath = new CommonPathClass();
        public static Type[] ExtraTypes = new Type[] { typeof(CommonPathClass) };
        private static UserPathNames singletonInstance = null;

        public static UserPathNames CreateInstance()
        {
            return CreateInstance(null);
        }

        public static UserPathNames CreateInstance(PathInfo pathInfo)
        {
            if (singletonInstance == null)
            {
                if (pathInfo == null)
                {
                    pathInfo = PathInfo.CreateInstance();
                }
                singletonInstance = AbstractConfig.Load(typeof(UserPathNames), ExtraTypes, pathInfo.UserPathNamesPath) as UserPathNames;
                if (singletonInstance == null)
                {
                    singletonInstance = new UserPathNames();
                    singletonInstance.Initialize();
                }
            }
            return singletonInstance;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public static UserPathNames ResetInstance()
        {
            PathInfo info = PathInfo.CreateInstance();
            singletonInstance = AbstractConfig.Load(typeof(UserPathNames), ExtraTypes, info.UserPathNamesPath) as UserPathNames;
            if (singletonInstance == null)
            {
                singletonInstance = new UserPathNames();
                singletonInstance.Initialize();
            }
            return singletonInstance;
        }

        public CommonPathClass CommonPath
        {
            get
            {
                return this.commonPath;
            }
            set
            {
                this.commonPath = value;
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
                return PathInfo.CreateInstance().UserPathNamesPath;
            }
        }

        [XmlIgnore]
        public override bool UpdateFlg
        {
            get
            {
                if (!base.updateFlg)
                {
                    return this.commonPath.UpdateFlg;
                }
                return true;
            }
            set
            {
                base.UpdateFlg = value;
                if (!value)
                {
                    this.commonPath.UpdateFlg = false;
                }
            }
        }
    }
}

