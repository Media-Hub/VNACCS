namespace Naccs.Core.Settings
{
    using System;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="AllCertification")]
    public class AllCertificationClass : AbstractSettings
    {
        private string password;

        [XmlElement(ElementName="Password")]
        public byte[] EncryptPassword
        {
            get
            {
                if ((this.password != null) && !(this.password == ""))
                {
                    return Encoding.Unicode.GetBytes(this.password);
                }
                return null;
            }
            set
            {
                this.password = Encoding.Unicode.GetString(value);
            }
        }

        [XmlIgnore]
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                base.updateFlg = this.password != value;
                this.password = value;
            }
        }
    }
}

