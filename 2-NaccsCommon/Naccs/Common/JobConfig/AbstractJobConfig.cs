namespace Naccs.Common.JobConfig
{
    using System;
    using System.Runtime.InteropServices;
    using System.Xml;

    public abstract class AbstractJobConfig
    {
        public bool bank;
        public bool center;
        public bool custom;
        public string directory;
        protected XmlDocument document;
        public bool erase;
        public bool general;
        public string guide_path;
        public string gym_err_path;
        public JobStyle jobStyle;
        public bool kiosk;
        public string Signflg;
        public char system;
        public TemplateStyle templateSytle;

        protected AbstractJobConfig()
        {
        }

        protected bool getAttributeBool(XmlNode node, string name, bool defaultReturn)
        {
            if ((node != null) && (node.Attributes[name] != null))
            {
                return bool.Parse(node.Attributes[name].Value);
            }
            return defaultReturn;
        }

        protected string getAttributeString(XmlNode node, string name)
        {
            if ((node != null) && (node.Attributes[name] != null))
            {
                return node.Attributes[name].Value;
            }
            return "";
        }

        protected string getNodeString(XmlNode parent, string name)
        {
            XmlNode node = parent.SelectSingleNode(name);
            if ((node != null) && (node.InnerText != null))
            {
                return node.InnerText;
            }
            return "";
        }

        protected DateTime GetValidDate(XmlNode rec)
        {
            string str;
            XmlAttribute attribute = rec.Attributes["valid-date"];
            if (attribute != null)
            {
                str = attribute.Value;
            }
            else
            {
                str = "";
            }
            if (str.Length == 14)
            {
                int year = int.Parse(str.Substring(0, 4));
                int month = int.Parse(str.Substring(4, 2));
                int day = int.Parse(str.Substring(6, 2));
                int hour = int.Parse(str.Substring(8, 2));
                int minute = int.Parse(str.Substring(10, 2));
                return new DateTime(year, month, day, hour, minute, int.Parse(str.Substring(12, 2)));
            }
            if (str.Length == 8)
            {
                int num7 = int.Parse(str.Substring(0, 4));
                int num8 = int.Parse(str.Substring(4, 2));
                return new DateTime(num7, num8, int.Parse(str.Substring(6, 2)));
            }
            return new DateTime(0L);
        }

        protected RecordAttribute loadRecord(XmlNode rec)
        {
            RecordAttribute attribute = new RecordAttribute();
            if (rec.Attributes["generation"] != null)
            {
                attribute.generation = int.Parse(rec.Attributes["generation"].Value);
            }
            attribute.jobcode = this.getAttributeString(rec, "jobcode");
            attribute.dispcode = this.getAttributeString(rec, "dispcode");
            attribute.titile1 = this.getAttributeString(rec, "title1");
            attribute.titile2 = this.getAttributeString(rec, "title2");
            attribute.attach = this.getAttributeBool(rec, "attach", false);
            attribute.validDate = this.GetValidDate(rec);
            attribute.barcode = this.getAttributeBool(rec, "barcode", false);
            string str = this.getAttributeString(rec, "confirm");
            int num = 1;
            if (str == num.ToString())
            {
                attribute.confirm = ConfirmType.TonnageTax;
            }
            else
            {
                int num2 = 2;
                if (str == num2.ToString())
                {
                    attribute.confirm = ConfirmType.ContainerNo;
                }
                else
                {
                    int num3 = 3;
                    if (str == num3.ToString())
                    {
                        attribute.confirm = ConfirmType.ShipInformation;
                    }
                    else
                    {
                        int num4 = 4;
                        if (str == num4.ToString())
                        {
                            attribute.confirm = ConfirmType.Document;
                        }
                        else
                        {
                            attribute.confirm = ConfirmType.None;
                        }
                    }
                }
            }
            if (this.getAttributeString(rec, "sign-flg") == "1")
            {
                attribute.signflg = 1;
                return attribute;
            }
            attribute.signflg = 0;
            return attribute;
        }

        public virtual int selectRecord(DateTime date, out XmlNode recordNode)
        {
            recordNode = null;
            XmlNodeList list = this.document.DocumentElement.SelectNodes("descendant::record");
            if ((list == null) || (list.Count == 0))
            {
                return -7;
            }
            long ticks = -1L;
            foreach (XmlNode node in list)
            {
                DateTime validDate = this.GetValidDate(node);
                if ((DateTime.Compare(date, validDate) >= 0) && (validDate.Ticks > ticks))
                {
                    recordNode = node;
                    ticks = validDate.Ticks;
                }
            }
            if (recordNode == null)
            {
                return -7;
            }
            return 0;
        }

        public virtual int selectRecord(int generation, out XmlNode recordNode)
        {
            recordNode = this.document.DocumentElement.SelectSingleNode("record[@generation=" + generation.ToString() + "]");
            if (recordNode == null)
            {
                return -7;
            }
            return 0;
        }

        public void setDirectories(string conf, string gym, string guide)
        {
            this.directory = conf;
            this.gym_err_path = gym;
            this.guide_path = guide;
        }

        public virtual int setDocument(XmlDocument doc)
        {
            this.document = doc;
            string str = this.getAttributeString(doc.DocumentElement, "system");
            if (str.Length < 1)
            {
                return -5;
            }
            this.system = str[0];
            string str2 = doc.DocumentElement.Attributes["type"].Value;
            if (str2 == "Display")
            {
                this.templateSytle = TemplateStyle.Display;
            }
            else if (str2 == "Print")
            {
                this.templateSytle = TemplateStyle.Print;
            }
            else if (str2 == "Preview")
            {
                this.templateSytle = TemplateStyle.Preview;
            }
            else if (str2 == "RecvOnly")
            {
                this.templateSytle = TemplateStyle.RecvOnly;
            }
            else
            {
                return -6;
            }
            this.erase = this.getAttributeBool(doc.DocumentElement, "erase", false);
            XmlNode node = doc.DocumentElement.SelectSingleNode("user-type");
            if (node != null)
            {
                this.general = this.getAttributeBool(node, "general", true);
                this.custom = this.getAttributeBool(node, "custom", true);
                this.center = this.getAttributeBool(node, "center", true);
                this.bank = this.getAttributeBool(node, "bank", true);
                this.kiosk = this.getAttributeBool(node, "private", false);
            }
            return 0;
        }
    }
}

