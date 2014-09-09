namespace Naccs.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    public class LogFileLengthSysClass
    {
        public const string default_naccs_comlog1 = "NacCom1.log";
        public const string default_naccs_comlog2 = "NacCom2.log";
        private string naccs_comlog1 = "NacCom1.log";
        private string naccs_comlog2 = "NacCom2.log";

        [XmlElement(ElementName="NACCS_ComLog1")]
        public string NACCS_ComLog1
        {
            get
            {
                return this.naccs_comlog1;
            }
            set
            {
                this.naccs_comlog1 = value;
            }
        }

        [XmlElement(ElementName="NACCS_ComLog2")]
        public string NACCS_ComLog2
        {
            get
            {
                return this.naccs_comlog2;
            }
            set
            {
                this.naccs_comlog2 = value;
            }
        }
    }
}

