namespace Naccs.Interactive.Classes
{
    using Naccs.Core.Classes;
    using Naccs.Core.Settings;
    using System;
    using System.IO;
    using System.Xml;

    public class KioskLink
    {
        private const string C_COMPLITION = "00000-0000-0000";
        public string LinkFile;
        private RecvData mRecvData;

        public KioskLink(RecvData recvdata)
        {
            this.mRecvData = recvdata;
            PathInfo info = PathInfo.CreateInstance();
            string str = this.mRecvData.ResultData.OutCode.Replace("*", "").Trim() + ".xml";
            this.LinkFile = Path.Combine(info.KioskLinkRoot, str);
        }

        public IData CreateLinkData()
        {
            IData data = null;
            try
            {
                if (!this.IsLinkData())
                {
                    return data;
                }
                XmlDocument document = new XmlDocument();
                document.Load(this.LinkFile);
                XmlElement element = (XmlElement) document.SelectSingleNode("link/destination");
                if (element == null)
                {
                    return data;
                }
                data = new NaccsData {
                    Header = { JobCode = element.GetAttribute("jobcode"), DispCode = element.GetAttribute("displaycode") }
                };
                foreach (XmlElement element2 in element.ChildNodes)
                {
                    int num;
                    if (int.TryParse(element2.GetAttribute("from"), out num))
                    {
                        if (num < this.mRecvData.ResultData.Items.Count)
                        {
                            data.Items.Add(this.mRecvData.ResultData.Items[num]);
                        }
                        else
                        {
                            data.Items.Add("");
                        }
                    }
                    else
                    {
                        data.Items.Add("");
                    }
                }
            }
            catch
            {
                data = null;
            }
            return data;
        }

        private bool IsLinkData()
        {
            bool flag = false;
            if (((this.mRecvData.ResultData != null) && (this.mRecvData.MData == null)) && ((this.mRecvData.ResultData.JobData.Substring(0, 15) == "00000-0000-0000") && File.Exists(this.LinkFile)))
            {
                flag = true;
            }
            return flag;
        }
    }
}

