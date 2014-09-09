namespace Naccs.Core.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="UserKey")]
    public class UserKey : AbstractConfig
    {
        public static Type[] ExtraTypes = new Type[] { typeof(JobKeyClass), typeof(KeyClass) };
        private JobKeyListClass jobKeyList = new JobKeyListClass();
        private static UserKey singletonInstance = null;

        public static UserKey ClearInstance()
        {
            singletonInstance = new UserKey();
            singletonInstance.Initialize();
            singletonInstance.SetDefaultUserKey(true);
            return singletonInstance;
        }

        public static UserKey CreateInstance()
        {
            if (singletonInstance == null)
            {
                MergeInstance();
            }
            return singletonInstance;
        }

        public static UserKey DefaultInstance()
        {
            singletonInstance = new UserKey();
            singletonInstance.Initialize();
            singletonInstance.SetDefaultUserKey(false);
            return singletonInstance;
        }

        public override void Initialize()
        {
            base.Initialize();
            this.jobKeyList.Clear();
        }

        private static void MergeInstance()
        {
            singletonInstance = new UserKey();
            singletonInstance.Initialize();
            singletonInstance.SetDefaultUserKey(false);
            PathInfo info = PathInfo.CreateInstance();
            UserKey key = AbstractConfig.Load(typeof(UserKey), ExtraTypes, info.UserKeySetup) as UserKey;
            if (key != null)
            {
                singletonInstance.UserKeyMerge(key.JobKeyList);
            }
        }

        public static string NoneCheck(string stk)
        {
            if (!(stk == "") && (stk != null))
            {
                return stk;
            }
            return "None";
        }

        public static UserKey ResetInstance()
        {
            MergeInstance();
            return singletonInstance;
        }

        private void SetDefaultUserKey(bool isreset)
        {
            JobKeyClass item = new JobKeyClass {
                Kind = 0
            };
            this.jobKeyList.Add(item);
            for (int i = 1; i < 4; i++)
            {
                KeyListClass class3 = new KeyListClass();
                item = new JobKeyClass();
                class3.GetJobKeyList(i, isreset);
                item.KeyList = class3.KeyInfoList.KeyList;
                item.Kind = i;
                this.jobKeyList.Add(item);
            }
        }

        private void UserKeyMerge(List<JobKeyClass> jkc)
        {
            for (int i = 0; i < jkc[0].KeyList.Count; i++)
            {
                singletonInstance.JobKeyList[0].KeyList.Add(jkc[0].KeyList[i]);
            }
            for (int j = 1; j < singletonInstance.JobKeyList.Count; j++)
            {
                for (int k = 0; k < singletonInstance.JobKeyList[j].KeyList.Count; k++)
                {
                    foreach (KeyClass class2 in jkc[j].KeyList)
                    {
                        if (singletonInstance.JobKeyList[j].KeyList[k].Name == class2.Name)
                        {
                            singletonInstance.JobKeyList[j].KeyList[k].Sckey = class2.Sckey;
                            break;
                        }
                    }
                }
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
                return PathInfo.CreateInstance().UserKeySetup;
            }
        }

        [XmlArrayItem(typeof(JobKeyClass), ElementName="JobKey")]
        public JobKeyListClass JobKeyList
        {
            get
            {
                return this.jobKeyList;
            }
            set
            {
                this.jobKeyList = value;
            }
        }

        [XmlIgnore]
        public override bool UpdateFlg
        {
            get
            {
                if (!base.UpdateFlg)
                {
                    return this.jobKeyList.UpdateFlg;
                }
                return true;
            }
            set
            {
                base.UpdateFlg = value;
                if (!value)
                {
                    this.jobKeyList.UpdateFlg = value;
                }
            }
        }
    }
}

