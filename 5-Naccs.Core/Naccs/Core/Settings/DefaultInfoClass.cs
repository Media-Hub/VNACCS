namespace Naccs.Core.Settings
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Printing;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="DefaultInfo")]
    public class DefaultInfoClass : AbstractSettings
    {
        private string binname = "";
        private int binno;
        public const string default_binname = "";
        public const int default_binno = 0;
        public const bool default_mode = false;
        public const string default_printer = "";
        public const bool default_sizewarning = true;
        private bool mode;
        private string printer = "";
        private bool sizewarning = true;

        private void getDefault()
        {
            PrintDocument document = new PrintDocument();
            if (document.PrinterSettings.IsValid)
            {
                this.printer = document.PrinterSettings.PrinterName;
                this.binno = document.PrinterSettings.DefaultPageSettings.PaperSource.RawKind;
                this.binname = document.PrinterSettings.DefaultPageSettings.PaperSource.SourceName;
            }
        }

        public override void Initialize()
        {
        }

        [DefaultValue(""), XmlElement(ElementName="BinName")]
        public string BinName
        {
            get
            {
                if (this.printer == "")
                {
                    this.getDefault();
                }
                return this.binname;
            }
            set
            {
                this.binname = value;
            }
        }

        [XmlElement(ElementName="BinNo"), DefaultValue(0)]
        public int BinNo
        {
            get
            {
                if (this.printer == "")
                {
                    this.getDefault();
                }
                return this.binno;
            }
            set
            {
                if (this.binno != value)
                {
                    base.updateFlg = this.binno != value;
                    this.binno = value;
                }
            }
        }

        [DefaultValue(false), XmlElement(ElementName="Mode")]
        public bool Mode
        {
            get
            {
                return this.mode;
            }
            set
            {
                if (this.mode != value)
                {
                    base.updateFlg = this.mode != value;
                    this.mode = value;
                }
            }
        }

        [XmlElement(ElementName="Printer"), DefaultValue("")]
        public string Printer
        {
            get
            {
                if (this.printer == "")
                {
                    this.getDefault();
                }
                return this.printer;
            }
            set
            {
                if (this.printer != value)
                {
                    base.updateFlg = this.printer != value;
                    this.printer = value;
                }
            }
        }

        [XmlElement(ElementName="SizeWarning"), DefaultValue(true)]
        public bool SizeWarning
        {
            get
            {
                return this.sizewarning;
            }
            set
            {
                if (this.sizewarning != value)
                {
                    base.updateFlg = this.sizewarning != value;
                    this.sizewarning = value;
                }
            }
        }
    }
}

