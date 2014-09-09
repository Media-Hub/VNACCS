namespace Naccs.Core.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="BatchdocInfo")]
    public class BatchdocInfoSysClass
    {
        private List<InteractiveServerClass> server = new List<InteractiveServerClass>();
        private string tracefilename = "Batchdoc.trc";
        private int tracefilesize = 0x3e8;

        public List<string> GetDisplayNames()
        {
            List<string> list = new List<string>();
            foreach (InteractiveServerClass class2 in this.Server)
            {
                list.Add(class2.display);
            }
            return list;
        }

        public InteractiveServerClass SelectServer(string name)
        {
            InteractiveServerClass class2 = null;
            foreach (InteractiveServerClass class3 in this.Server)
            {
                if (name.Equals(class3.name, StringComparison.CurrentCultureIgnoreCase))
                {
                    class2 = class3;
                    break;
                }
            }
            if (class2 != null)
            {
                return class2;
            }
            if (this.Server.Count > 0)
            {
                return this.Server[0];
            }
            return new InteractiveServerClass();
        }

        [XmlElement(ElementName="Server")]
        public List<InteractiveServerClass> Server
        {
            get
            {
                return this.server;
            }
            set
            {
                this.server = value;
            }
        }

        [XmlElement(ElementName="TraceFileName")]
        public string TraceFileName
        {
            get
            {
                return this.tracefilename;
            }
            set
            {
                this.tracefilename = value;
            }
        }

        [XmlElement(ElementName="TraceFileSize")]
        public int TraceFileSize
        {
            get
            {
                return this.tracefilesize;
            }
            set
            {
                this.tracefilesize = value;
            }
        }
    }
}

