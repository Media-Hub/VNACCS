namespace Naccs.Core.Settings
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="BatchSend")]
    public class BatchSendClass : AbstractSettings
    {
        public const int default_sleep = 0;
        private int sleep;

        [XmlElement(ElementName="Sleep"), DefaultValue(0)]
        public int Sleep
        {
            get
            {
                return this.sleep;
            }
            set
            {
                if (this.sleep != value)
                {
                    this.sleep = value;
                    base.updateFlg = true;
                }
            }
        }
    }
}

