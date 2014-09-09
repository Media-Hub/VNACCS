namespace Naccs.Core.Settings
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="ResultCodeColor")]
    public class ResultCodeColorClass : AbstractSettings
    {
        private Color cmessage = Color.FromName("Lime");
        private const string default_cmessage = "Lime";
        private const string default_error = "Yellow";
        private const string default_normal = "White";
        private const string default_warning = "Fuchsia";
        private Color error = Color.FromName("Yellow");
        private Color normal = Color.FromName("White");
        private Color warning = Color.FromName("Fuchsia");

        [XmlIgnore]
        public Color Cmessage
        {
            get
            {
                return this.cmessage;
            }
            set
            {
                base.updateFlg = this.cmessage != value;
                this.cmessage = value;
            }
        }

        [XmlElement(ElementName="Cmessage"), DefaultValue("Lime")]
        public string CMessageColorName
        {
            get
            {
                return this.cmessage.Name;
            }
            set
            {
                Color color = Color.FromName(value);
                if (color != this.cmessage)
                {
                    this.cmessage = color;
                    base.updateFlg = true;
                }
            }
        }

        [XmlIgnore]
        public Color Error
        {
            get
            {
                return this.error;
            }
            set
            {
                base.updateFlg = this.error != value;
                this.error = value;
            }
        }

        [XmlElement(ElementName="Error"), DefaultValue("Yellow")]
        public string ErrorColorName
        {
            get
            {
                return this.error.Name;
            }
            set
            {
                Color color = Color.FromName(value);
                if (color != this.error)
                {
                    this.error = color;
                    base.updateFlg = true;
                }
            }
        }

        [XmlIgnore]
        public Color Normal
        {
            get
            {
                return this.normal;
            }
            set
            {
                base.updateFlg = this.normal != value;
                this.normal = value;
            }
        }

        [DefaultValue("White"), XmlElement(ElementName="Normal")]
        public string NormalColorName
        {
            get
            {
                return this.normal.Name;
            }
            set
            {
                Color color = Color.FromName(value);
                if (this.normal != color)
                {
                    this.normal = color;
                    base.updateFlg = true;
                }
            }
        }

        [XmlIgnore]
        public Color Warning
        {
            get
            {
                return this.warning;
            }
            set
            {
                base.updateFlg = this.warning != value;
                this.warning = value;
            }
        }

        [XmlElement(ElementName="Warning"), DefaultValue("Fuchsia")]
        public string WarningColorName
        {
            get
            {
                return this.warning.Name;
            }
            set
            {
                Color color = Color.FromName(value);
                if (color != this.warning)
                {
                    this.warning = color;
                    base.updateFlg = true;
                }
            }
        }
    }
}

