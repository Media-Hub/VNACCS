namespace Naccs.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    public class InteractiveServerClass
    {
        private string connectname = "";
        private string displayname = "";
        private string objectname = "???";
        private string pingpoint = "";
        private string servername = "???.com";
        private int serverport = 0x270f;

        [XmlAttribute(AttributeName="display")]
        public string display
        {
            get
            {
                return this.displayname;
            }
            set
            {
                this.displayname = value;
            }
        }

        [XmlAttribute(AttributeName="name")]
        public string name
        {
            get
            {
                return this.connectname;
            }
            set
            {
                this.connectname = value;
            }
        }

        [XmlElement(ElementName="ObjectName")]
        public string ObjectName
        {
            get
            {
                return this.objectname;
            }
            set
            {
                this.objectname = value;
            }
        }

        [XmlElement(ElementName="PingPoint")]
        public string PingPoint
        {
            get
            {
                return this.pingpoint;
            }
            set
            {
                this.pingpoint = value;
            }
        }

        [XmlElement(ElementName="ServerName")]
        public string ServerName
        {
            get
            {
                return this.servername;
            }
            set
            {
                this.servername = value;
            }
        }

        [XmlElement(ElementName="ServerPort")]
        public int ServerPort
        {
            get
            {
                return this.serverport;
            }
            set
            {
                this.serverport = value;
            }
        }
    }
}

