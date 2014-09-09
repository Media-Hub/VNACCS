namespace Naccs.Core.Classes
{
    using Naccs.Core.Settings;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    internal class DispCodeList
    {
        private PathInfo mPathInfo = PathInfo.CreateInstance();
        private List<XmlNode> mTreeList;

        public DispCodeList()
        {
            this.CreateList();
        }

        private void CreateList()
        {
            string jobTreeRoot = this.mPathInfo.JobTreeRoot;
            this.mTreeList = new List<XmlNode>();
            if (Directory.Exists(jobTreeRoot))
            {
                XmlDocument document = new XmlDocument();
                string[] strArray = Directory.GetFiles(jobTreeRoot, "JobTree*.xml", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (File.Exists(strArray[i]))
                    {
                        try
                        {
                            document.Load(strArray[i]);
                            XmlNodeList elementsByTagName = document.DocumentElement.GetElementsByTagName("item");
                            for (int j = 0; j < elementsByTagName.Count; j++)
                            {
                                this.mTreeList.Add(elementsByTagName[j]);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        public List<string> DispCodeGet(string JobCode)
        {
            string str = Path.Combine(this.mPathInfo.TemplateRoot, "send");
            List<string> list = new List<string>();
            if (this.mTreeList.Count >= 1)
            {
                string str2 = "";
                for (int i = 0; i < this.mTreeList.Count; i++)
                {
                    int length = -1;
                    str2 = this.mTreeList[i].Attributes["code"].Value;
                    length = str2.IndexOf(".");
                    if (length != -1)
                    {
                        try
                        {
                            string str3 = string.Format(@"{0}\{1}.conf", str2.Replace(".", @"\"), str2.Replace(".", ""));
                            str2 = str2.Substring(0, length);
                            if (!File.Exists(Path.Combine(str, str3)))
                            {
                                continue;
                            }
                        }
                        catch
                        {
                            continue;
                        }
                        if (str2 == JobCode)
                        {
                            list.Add(this.mTreeList[i].Attributes["name"].Value);
                        }
                    }
                }
            }
            return list;
        }
    }
}

