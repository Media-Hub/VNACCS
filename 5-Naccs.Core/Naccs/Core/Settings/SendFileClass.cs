namespace Naccs.Core.Settings
{
    using System;
    using System.IO;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="SendFile")]
    public class SendFileClass : AbstractSettings
    {
        public const string default_senduser = @"Windowsのマイドキュメントからの相対パス\SendUser";
        private string senduser;

        [XmlElement(ElementName="SendUser")]
        public string SendUser
        {
            get
            {
                return this.senduser;
            }
            set
            {
                base.updateFlg = this.senduser != value;
                if (value.EndsWith(@"\"))
                {
                    if (string.IsNullOrEmpty(Path.GetDirectoryName(value)))
                    {
                        this.senduser = value;
                    }
                    else
                    {
                        this.senduser = Path.GetDirectoryName(value) + @"\";
                    }
                }
                else
                {
                    this.senduser = value;
                }
            }
        }
    }
}

