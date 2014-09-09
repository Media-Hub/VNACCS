namespace Naccs.Core.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class MailInfoSysClass
    {
        private int popservercnctretry = 1;
        private int reconnecttime = 0x3e8;
        private int recvtimeout = 60;
        private int sendtimeout = 60;
        private List<MailServerClass> server = new List<MailServerClass>();
        private string tracefilename = "Mail.trc";
        private int tracefilesize = 0x3e8;

        public List<string> GetDisplayNames()
        {
            List<string> list = new List<string>();
            foreach (MailServerClass class2 in this.Server)
            {
                list.Add(class2.display);
            }
            return list;
        }

        public List<string> GetNames()
        {
            List<string> list = new List<string>();
            foreach (MailServerClass class2 in this.Server)
            {
                list.Add(class2.name);
            }
            return list;
        }

        public MailServerClass SelectServer(string name)
        {
            MailServerClass class2 = null;
            foreach (MailServerClass class3 in this.Server)
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
            return new MailServerClass();
        }

        [XmlElement(ElementName="POPServerCnctRetry")]
        public int POPServerCnctRetry
        {
            get
            {
                return this.popservercnctretry;
            }
            set
            {
                this.popservercnctretry = value;
            }
        }

        [XmlElement(ElementName="ReConnectTime")]
        public int ReConnectTime
        {
            get
            {
                return this.reconnecttime;
            }
            set
            {
                this.reconnecttime = value;
            }
        }

        [XmlElement(ElementName="RecvTimeOut")]
        public int RecvTimeOut
        {
            get
            {
                return this.recvtimeout;
            }
            set
            {
                this.recvtimeout = value;
            }
        }

        [XmlElement(ElementName="SendTimeOut")]
        public int SendTimeOut
        {
            get
            {
                return this.sendtimeout;
            }
            set
            {
                this.sendtimeout = value;
            }
        }

        [XmlElement(ElementName="Server")]
        public List<MailServerClass> Server
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

