namespace Naccs.Core.Job
{
    using Naccs.Core.Settings;
    using System;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    internal class JobSettings
    {
        private ModeFont fontMode;
        private int pnlCommonSizeW = 300;
        private int pnlGuideSizeH = 0x73;
        private int pnlHeaderSizeH = 0xc3;
        private int pnlRegularSizeH = 0x145;
        private int pnlResCodeSizeH = 0xfe;
        private int pnlResMsgSizeH = 80;
        private static JobSettings singletonInstance;
        private int sizeH = 0x300;
        private int sizeW = 0x400;
        private char sysMode = '2';
        private FormWindowState windowState;

        public JobSettings()
        {
            string jobSetup = PathInfo.CreateInstance().JobSetup;
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(jobSetup);
                foreach (XmlElement element2 in document.DocumentElement.ChildNodes)
                {
                    if (element2.LocalName == "SizeW")
                    {
                        this.sizeW = int.Parse(element2.InnerText);
                    }
                    if (element2.LocalName == "SizeH")
                    {
                        this.sizeH = int.Parse(element2.InnerText);
                    }
                    if (element2.LocalName == "System")
                    {
                        this.sysMode = element2.InnerText[0];
                    }
                    if ((element2.LocalName == "Maximized") && (element2.InnerText == "1"))
                    {
                        this.windowState = FormWindowState.Maximized;
                    }
                    if (element2.LocalName == "FontMode")
                    {
                        if (element2.InnerText == "1")
                        {
                            this.fontMode = ModeFont.large;
                        }
                        if (element2.InnerText == "2")
                        {
                            this.fontMode = ModeFont.small;
                        }
                    }
                    if (element2.LocalName == "PnlRegularSizeH")
                    {
                        this.pnlRegularSizeH = int.Parse(element2.InnerText);
                    }
                    if (element2.LocalName == "PnlHeaderSizeH")
                    {
                        this.pnlHeaderSizeH = int.Parse(element2.InnerText);
                    }
                    if (element2.LocalName == "PnlGuideSizeH")
                    {
                        this.pnlGuideSizeH = int.Parse(element2.InnerText);
                    }
                    if (element2.LocalName == "PnlResCodeSizeH")
                    {
                        this.pnlResCodeSizeH = int.Parse(element2.InnerText);
                    }
                    if (element2.LocalName == "PnlResMsgSizeH")
                    {
                        this.pnlResMsgSizeH = int.Parse(element2.InnerText);
                    }
                    if (element2.LocalName == "PnlCommonSizeW")
                    {
                        this.pnlCommonSizeW = int.Parse(element2.InnerText);
                    }
                }
            }
            catch
            {
            }
        }

        public static JobSettings CreateInstance()
        {
            if (singletonInstance == null)
            {
                singletonInstance = new JobSettings();
            }
            return singletonInstance;
        }

        public void SaveJobConfig()
        {
            string jobSetup = PathInfo.CreateInstance().JobSetup;
            try
            {
                XmlDocument document = new XmlDocument();
                XmlElement newChild = document.CreateElement("JobConfig");
                document.AppendChild(newChild);
                XmlElement element2 = document.CreateElement("SizeW");
                element2.InnerText = this.sizeW.ToString();
                newChild.AppendChild(element2);
                XmlElement element3 = document.CreateElement("SizeH");
                element3.InnerText = this.sizeH.ToString();
                newChild.AppendChild(element3);
                XmlElement element4 = document.CreateElement("System");
                element4.InnerText = this.sysMode.ToString();
                newChild.AppendChild(element4);
                XmlElement element5 = document.CreateElement("Maximized");
                if (this.windowState == FormWindowState.Maximized)
                {
                    element5.InnerText = "1";
                }
                else
                {
                    element5.InnerText = "0";
                }
                newChild.AppendChild(element5);
                XmlElement element6 = document.CreateElement("FontMode");
                if (this.fontMode == ModeFont.normal)
                {
                    element6.InnerText = "0";
                }
                else if (this.fontMode == ModeFont.large)
                {
                    element6.InnerText = "1";
                }
                else if (this.fontMode == ModeFont.small)
                {
                    element6.InnerText = "2";
                }
                newChild.AppendChild(element6);
                XmlElement element7 = document.CreateElement("PnlRegularSizeH");
                element7.InnerText = this.pnlRegularSizeH.ToString();
                newChild.AppendChild(element7);
                XmlElement element8 = document.CreateElement("PnlHeaderSizeH");
                element8.InnerText = this.pnlHeaderSizeH.ToString();
                newChild.AppendChild(element8);
                XmlElement element9 = document.CreateElement("PnlGuideSizeH");
                element9.InnerText = this.pnlGuideSizeH.ToString();
                newChild.AppendChild(element9);
                XmlElement element10 = document.CreateElement("PnlResCodeSizeH");
                element10.InnerText = this.pnlResCodeSizeH.ToString();
                newChild.AppendChild(element10);
                XmlElement element11 = document.CreateElement("PnlResMsgSizeH");
                element11.InnerText = this.PnlResMsgSizeH.ToString();
                newChild.AppendChild(element11);
                XmlElement element12 = document.CreateElement("PnlCommonSizeW");
                element12.InnerText = this.pnlCommonSizeW.ToString();
                newChild.AppendChild(element12);
                XmlTextWriter w = new XmlTextWriter(jobSetup, Encoding.UTF8) {
                    Formatting = Formatting.Indented,
                    Indentation = 4
                };
                document.Save(w);
            }
            catch
            {
            }
        }

        public ModeFont FontMode
        {
            get
            {
                return this.fontMode;
            }
            set
            {
                this.fontMode = value;
            }
        }

        public int PnlCommonSizeW
        {
            get
            {
                return this.pnlCommonSizeW;
            }
            set
            {
                this.pnlCommonSizeW = value;
            }
        }

        public int PnlGuideSizeH
        {
            get
            {
                return this.pnlGuideSizeH;
            }
            set
            {
                this.pnlGuideSizeH = value;
            }
        }

        public int PnlHeaderSizeH
        {
            get
            {
                return this.pnlHeaderSizeH;
            }
            set
            {
                this.pnlHeaderSizeH = value;
            }
        }

        public int PnlRegularSizeH
        {
            get
            {
                return this.pnlRegularSizeH;
            }
            set
            {
                this.pnlRegularSizeH = value;
            }
        }

        public int PnlResCodeSizeH
        {
            get
            {
                return this.pnlResCodeSizeH;
            }
            set
            {
                this.pnlResCodeSizeH = value;
            }
        }

        public int PnlResMsgSizeH
        {
            get
            {
                return this.pnlResMsgSizeH;
            }
            set
            {
                this.pnlResMsgSizeH = value;
            }
        }

        public int SizeH
        {
            get
            {
                return this.sizeH;
            }
            set
            {
                this.sizeH = value;
            }
        }

        public int SizeW
        {
            get
            {
                return this.sizeW;
            }
            set
            {
                this.sizeW = value;
            }
        }

        public char SysMode
        {
            get
            {
                return this.sysMode;
            }
            set
            {
                this.sysMode = value;
            }
        }

        public FormWindowState WindowState
        {
            get
            {
                return this.windowState;
            }
            set
            {
                this.windowState = value;
            }
        }
    }
}

