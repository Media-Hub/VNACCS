namespace Naccs.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="Forms")]
    public class FormsClass : AbstractSettings
    {
        private OutCodeListClass outCodeList = new OutCodeListClass();

        public override void Initialize()
        {
            base.Initialize();
            this.outCodeList.Clear();
        }

        [XmlArrayItem(typeof(OutCodeClass), ElementName="OutCode")]
        public OutCodeListClass OutCodeList
        {
            get
            {
                return this.outCodeList;
            }
            set
            {
                this.outCodeList = value;
            }
        }

        [XmlIgnore]
        public override bool UpdateFlg
        {
            get
            {
                if (!base.UpdateFlg)
                {
                    return this.outCodeList.UpdateFlg;
                }
                return true;
            }
            set
            {
                base.UpdateFlg = value;
                if (!value)
                {
                    this.outCodeList.UpdateFlg = value;
                }
            }
        }
    }
}

