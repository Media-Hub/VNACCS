namespace Naccs.Common.JobConfig
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Xml;

    public class NormalConfig : AbstractJobConfig
    {
        private string correlation;
        private string display;
        private string guide;
        private string help;
        private string link;
        private string print;
        public RecordAttribute record;

        public NormalConfig()
        {
            base.jobStyle = JobStyle.normal;
        }

        public override int selectRecord(DateTime date, out XmlNode record)
        {
            int num = base.selectRecord(date, out record);
            if (num == 0)
            {
                this.setProperties(record);
            }
            return num;
        }

        public override int selectRecord(int generation, out XmlNode record)
        {
            int num = base.selectRecord(generation, out record);
            if (num == 0)
            {
                this.setProperties(record);
            }
            return num;
        }

        private void setProperties(XmlNode rec)
        {
            this.record = base.loadRecord(rec);
            this.display = base.getNodeString(rec, "display");
            this.print = base.getNodeString(rec, "print");
            this.link = base.getNodeString(rec, "link");
            this.correlation = base.getNodeString(rec, "correlation");
            this.guide = base.getNodeString(rec, "guide");
            this.help = base.getNodeString(rec, "help");
        }

        public string Correlation
        {
            get
            {
                if ((this.correlation != null) && (this.correlation != ""))
                {
                    return Path.GetFullPath(base.directory + this.correlation);
                }
                return "";
            }
        }

        public string Display
        {
            get
            {
                if ((this.display != null) && (this.display != ""))
                {
                    return Path.GetFullPath(base.directory + this.display);
                }
                return "";
            }
        }

        public string Guide
        {
            get
            {
                if ((this.guide != null) && (this.guide != ""))
                {
                    return Path.GetFullPath(base.guide_path + this.guide);
                }
                return "";
            }
        }

        public string Help
        {
            get
            {
                if ((this.help != null) && (this.help != ""))
                {
                    return Path.GetFullPath(base.gym_err_path + this.help);
                }
                return "";
            }
        }

        public string Link
        {
            get
            {
                if ((this.link != null) && (this.link != ""))
                {
                    return Path.GetFullPath(base.directory + this.link);
                }
                return "";
            }
        }

        public string Print
        {
            get
            {
                if ((this.print != null) && (this.print != ""))
                {
                    return Path.GetFullPath(base.directory + this.print);
                }
                return "";
            }
        }
    }
}

