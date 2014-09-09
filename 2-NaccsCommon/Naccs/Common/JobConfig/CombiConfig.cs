namespace Naccs.Common.JobConfig
{
    using Naccs.Common.Properties;
    using System;
    using System.Xml;

    public class CombiConfig : AbstractJobConfig
    {
        public SubConfig[] configs;
        public int count;
        public string dispcode;
        public string jobcode;
        public string title;

        public CombiConfig()
        {
            base.jobStyle = JobStyle.combi;
        }

        public override int setDocument(XmlDocument doc)
        {
            base.setDocument(doc);
            XmlNode node = doc.DocumentElement.SelectSingleNode("combination");
            if (node == null)
            {
                throw new Exception(Resources.ResourceManager.GetString("COM13"));
            }
            string s = base.getAttributeString(node, "count");
            if (s == "")
            {
                throw new Exception(Resources.ResourceManager.GetString("COM14"));
            }
            this.count = int.Parse(s);
            this.jobcode = base.getAttributeString(node, "jobcode");
            this.dispcode = base.getAttributeString(node, "dispcode");
            this.title = base.getAttributeString(node, "title");
            this.configs = new SubConfig[this.count];
            for (int i = 0; i < this.count; i++)
            {
                XmlNode node2 = node.ChildNodes[i];
                this.configs[i].order = int.Parse(base.getAttributeString(node2, "order"));
                this.configs[i].tabname = base.getAttributeString(node2, "tabname");
                this.configs[i].code = node2.InnerText;
                this.configs[i].config = null;
            }
            return 0;
        }
    }
}

