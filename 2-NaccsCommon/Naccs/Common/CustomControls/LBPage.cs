namespace Naccs.Common.CustomControls
{
    using Naccs.Common.Constant;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    [Description("Print Page"), Designer(typeof(PrintPageDesigner), typeof(IRootDesigner)), ToolboxBitmap(typeof(PrintDocument))]
    public class LBPage : UserControl
    {
        private bool bPaperSizeSetting;
        private IContainer components;
        private Padding hardwareMargin = new Padding(5);
        private bool includeHardwareMargin;
        private bool includePrintMargin;
        private bool landScape;
        private int maxOccurs = 1;
        private int minOccurs;
        private const float OnePoint = 0.264583f;
        private string paperSize;
        private Padding printMargin = new Padding(10);

        public LBPage()
        {
            this.InitializeComponent();
            this.PaperSize = "A4";
            this.setPaperSize();
            base.Resize += new EventHandler(this.LBPage_Resize);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            base.Name = "LBPage";
            base.Size = new Size(0x205, 0x209);
            base.ResumeLayout(false);
        }

        private void LBPage_Resize(object sender, EventArgs e)
        {
            if (!this.bPaperSizeSetting)
            {
                this.setPaperSize();
            }
        }

        private float MiliToPixel(float mili)
        {
            return (mili / 0.264583f);
        }

        private void setPaperSize()
        {
            this.bPaperSizeSetting = true;
            try
            {
                if (this.paperSize == "A5")
                {
                    if (this.landScape)
                    {
                        base.Width = SizeDef.LAYOUT_A5_HEIGHT;
                        base.Height = SizeDef.LAYOUT_A5_WIDTH;
                    }
                    else
                    {
                        base.Width = SizeDef.LAYOUT_A5_WIDTH;
                        base.Height = SizeDef.LAYOUT_A5_HEIGHT;
                    }
                }
                else if (this.paperSize == "A3")
                {
                    if (this.landScape)
                    {
                        base.Width = SizeDef.LAYOUT_A3_HEIGHT;
                        base.Height = SizeDef.LAYOUT_A3_WIDTH;
                    }
                    else
                    {
                        base.Width = SizeDef.LAYOUT_A3_WIDTH;
                        base.Height = SizeDef.LAYOUT_A3_HEIGHT;
                    }
                }
                else if (this.landScape)
                {
                    base.Width = SizeDef.LAYOUT_A4_HEIGHT;
                    base.Height = SizeDef.LAYOUT_A4_WIDTH;
                }
                else
                {
                    base.Width = SizeDef.LAYOUT_A4_WIDTH;
                    base.Height = SizeDef.LAYOUT_A4_HEIGHT;
                }
            }
            finally
            {
                this.bPaperSizeSetting = false;
            }
        }

        [Category("Service"), Localizable(false), Description("Hardware mergin (fixed 5mm)")]
        public Padding HardwareMargin
        {
            get
            {
                return this.hardwareMargin;
            }
        }

        [Description("Including the margin of hardware"), Category("Service"), DefaultValue(false)]
        public bool IncludeHardwareMargin
        {
            get
            {
                return this.includeHardwareMargin;
            }
            set
            {
                this.includeHardwareMargin = value;
                this.setPaperSize();
                this.Refresh();
            }
        }

        [Category("Service"), DefaultValue(false), Description("Including the margin of printing")]
        public bool IncludePrintMargin
        {
            get
            {
                return this.includePrintMargin;
            }
            set
            {
                this.includePrintMargin = value;
                this.setPaperSize();
                this.Refresh();
            }
        }

        [Description("Print Direction(Portrait：False, Landscape：True)"), DefaultValue(false), Category("Service")]
        public bool LandScape
        {
            get
            {
                return this.landScape;
            }
            set
            {
                this.landScape = value;
                this.setPaperSize();
            }
        }

        [DefaultValue(1), Description("The number of max print times"), Category("Service")]
        public int MaxOccurs
        {
            get
            {
                return this.maxOccurs;
            }
            set
            {
                this.maxOccurs = value;
            }
        }

        [Description("The number of minimum print times"), Category("Service"), DefaultValue(0)]
        public int MinOccurs
        {
            get
            {
                return this.minOccurs;
            }
            set
            {
                this.minOccurs = value;
            }
        }

        [Description("Paper Size（A3, A4, A5）"), TypeConverter(typeof(PaperSizeConverter)), Category("Service"), DefaultValue("A4")]
        public string PaperSize
        {
            get
            {
                if (((this.paperSize != "A3") && (this.paperSize != "A4")) && (this.paperSize != "A5"))
                {
                    this.paperSize = "A4";
                }
                return this.paperSize;
            }
            set
            {
                if (((value == "A3") || (value == "A4")) || (value == "A5"))
                {
                    this.paperSize = value;
                }
                else
                {
                    this.paperSize = "A4";
                }
                this.setPaperSize();
            }
        }

        [Category("Service"), Description("Print Mergin (unit : mm)")]
        public Padding PrintMargin
        {
            get
            {
                return this.printMargin;
            }
            set
            {
                this.printMargin = value;
                this.setPaperSize();
                this.Refresh();
            }
        }
    }
}

