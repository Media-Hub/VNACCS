namespace Naccs.Common.JobConfig
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Xml;

    public class DivideConfig : AbstractJobConfig
    {
        public Division displayDivision;
        public Division printDivision;
        public RecordAttribute record;

        public DivideConfig()
        {
            base.jobStyle = JobStyle.divide;
        }

        private int loadDivision(XmlNode divisionNode)
        {
            string str;
            Division division = new Division();
            if (divisionNode.Attributes["count"] == null)
            {
                return -40;
            }
            division.count = int.Parse(base.getAttributeString(divisionNode, "count"));
            division.templates = new TemplateItem[division.count];
            if (divisionNode.Name == "display-division")
            {
                this.displayDivision = division;
                str = "display";
            }
            else
            {
                this.printDivision = division;
                str = "print";
            }
            XmlNodeList list = divisionNode.SelectNodes(str);
            for (int i = 0; i < division.count; i++)
            {
                division.templates[i].order = int.Parse(base.getAttributeString(list[i], "order"));
                division.templates[i].tabname = base.getAttributeString(list[i], "tabname");
                division.templates[i].filename = Path.GetFullPath(base.directory + list[i].InnerText);
            }
            division.iteminfo = Path.GetFullPath(base.directory + base.getNodeString(divisionNode, "item-info"));
            division.link = Path.GetFullPath(base.directory + base.getNodeString(divisionNode, "link"));
            division.correlation = Path.GetFullPath(base.directory + base.getNodeString(divisionNode, "correlation"));
            division.guide = Path.GetFullPath(base.guide_path + base.getNodeString(divisionNode, "guide"));
            division.help = Path.GetFullPath(base.gym_err_path + base.getNodeString(divisionNode, "help"));
            return 0;
        }

        public override int selectRecord(DateTime date, out XmlNode recordNode)
        {
            int num = base.selectRecord(date, out recordNode);
            if (num == 0)
            {
                this.record = base.loadRecord(recordNode);
                foreach (XmlNode node in recordNode.ChildNodes)
                {
                    num = this.loadDivision(node);
                    if (num < 0)
                    {
                        return num;
                    }
                }
            }
            return num;
        }

        public override int selectRecord(int generation, out XmlNode recordNode)
        {
            int num = base.selectRecord(generation, out recordNode);
            if (num == 0)
            {
                this.record = base.loadRecord(recordNode);
                foreach (XmlNode node in recordNode.ChildNodes)
                {
                    this.loadDivision(node);
                }
            }
            return num;
        }
    }
}

