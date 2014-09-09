namespace Naccs.Common.CustomControls
{
    using Naccs.Common.Constant;
    using Naccs.Common.Function;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [DisplayName("Computerization OCR")]
    public class LBCompOcr : Control, IItemAttributes
    {
        private string _attribute = "X";
        private bool _boxLine;
        private SizeF _boxSize;
        private float _bytePitch;
        private string _check_date = "";
        private string _check_full = "";
        private string _check_time = "";
        private string _choice_keyvalue = "";
        public static float _defaultEmSize = 12f;
        public static string _defaultFontFile = "NACCS_OCR.TTF";
        public static string _defaultFontName = "NACCS_OCR";
        public static string _defaultFontStr = "NACCS_OCR, 12pt";
        private Point _fontLocate;
        private string _form = "";
        private string _id = "";
        private string _input_output = " ";
        private string _name = "";
        private int _order = -1;
        private string _required = "";
        private const float Byte_PitchM = 2.54f;
        private string checkAttribute = "";
        private string currencyMark = "";
        private int fFigure;
        private StrFunc SF = StrFunc.CreateInstance();

        public LBCompOcr()
        {
            base.DoubleBuffered = true;
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.DoubleBuffered = true;
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this._fontLocate = new Point(0, 0);
            this._boxSize = new SizeF((float) Math.Round((double) (2.54f * SizeDef.DisplayPixelPerMiliMeter), 2), 19f);
            this._bytePitch = (float) Math.Round((double) (2.54f * SizeDef.DisplayPixelPerMiliMeter), 2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            float num3;
            this._fontLocate.X = 0;
            this._fontLocate.Y = 0;
            this._boxSize.Width = (float) Math.Round((double) (2.54f * SizeDef.DisplayPixelPerMiliMeter), 2);
            this._bytePitch = (float) Math.Round((double) (2.54f * SizeDef.DisplayPixelPerMiliMeter), 2);
            this._boxLine = false;
            float x = this.FontLocate.X;
            float y = this.FontLocate.Y;
            PointF point = new PointF(x, y);
            if (this.CurrencyMark.Length == 0)
            {
                num3 = this.BytePitch * this.fFigure;
            }
            else
            {
                num3 = this.BytePitch * (this.fFigure + 1f);
            }
            float height = this.Font.GetHeight();
            base.Width = ((int) num3) + 2;
            this._boxSize.Height = (int) height;
            base.Height = (int) height;
            string str = this.SF.MakeCycleStr(this.fFigure, "9");
            if (this.CurrencyMark.Length > 0)
            {
                str = this.CurrencyMark + str;
            }
            using (System.Drawing.Font font = new System.Drawing.Font(this.Font, this.Font.Style))
            {
                using (SolidBrush brush = new SolidBrush(this.ForeColor))
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        char ch = str[i];
                        e.Graphics.DrawString(ch.ToString(), font, brush, point);
                        x += this.BytePitch;
                        point.X = x;
                    }
                }
            }
        }

        public void SetBackColor()
        {
            this.BackColor = SystemColors.Control;
        }

        [Localizable(true), Browsable(true), Description("Attribute"), Category("Service")]
        public string attribute
        {
            get
            {
                return this._attribute;
            }
            set
            {
                this._attribute = value;
            }
        }

        [DefaultValue(false), Description("The existence or non-existence of the frame border."), Category("Appearance"), Localizable(true), Browsable(true)]
        public bool BoxLine
        {
            get
            {
                return this._boxLine;
            }
            set
            {
                this._boxLine = value;
                this.Refresh();
            }
        }

        [Description("The frame size (in pixel unit)."), Browsable(true), Localizable(true), Category("Layout")]
        public SizeF BoxSize
        {
            get
            {
                return this._boxSize;
            }
            set
            {
                this._boxSize = value;
            }
        }

        [Category("Layout"), Description("The pitch width of one word (in pixel unit)."), Browsable(true), Localizable(true)]
        public float BytePitch
        {
            get
            {
                return this._bytePitch;
            }
            set
            {
                this._bytePitch = value;
                this.Refresh();
            }
        }

        [Browsable(true), Description("Specify whether to check attribute or not."), Localizable(true), Category("Service")]
        public string check_attribute
        {
            get
            {
                return this.checkAttribute;
            }
            set
            {
                this.checkAttribute = value;
            }
        }

        [Browsable(true), Category("Service"), Description("Specify whether to check the validity of date or not"), Localizable(true)]
        public string check_date
        {
            get
            {
                return this._check_date;
            }
            set
            {
                this._check_date = value;
            }
        }

        [Category("Service"), Description("Specify whether to check full digit input or not"), Browsable(true), Localizable(true)]
        public string check_full
        {
            get
            {
                return this._check_full;
            }
            set
            {
                this._check_full = value;
            }
        }

        [Description("Specify whether it checks date validity "), Category("Service"), Browsable(true), Localizable(true)]
        public string check_time
        {
            get
            {
                return this._check_time;
            }
            set
            {
                this._check_time = value;
            }
        }

        [Browsable(true), Description("Selections for radio button."), Localizable(true), Category("Service")]
        public string choice_keyvalue
        {
            get
            {
                return this._choice_keyvalue;
            }
            set
            {
                this._choice_keyvalue = value;
            }
        }

        [Description("Currency Mark"), DefaultValue(""), Category("Service")]
        public string CurrencyMark
        {
            get
            {
                return this.currencyMark;
            }
            set
            {
                if (value.Length > 1)
                {
                    this.currencyMark = value.Substring(0, 1);
                }
                else
                {
                    this.currencyMark = value;
                }
                this.Refresh();
            }
        }

        [Browsable(true), Description("Digit number"), Localizable(true), Category("Service")]
        public int figure
        {
            get
            {
                return this.fFigure;
            }
            set
            {
                this.fFigure = value;
            }
        }

        [Description("This is the font to display a text with the control. \n\n [This is not reflected to the printing, instead prescribed OCR font is used upon printing.]")]
        public override System.Drawing.Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }

        [Browsable(true), Description("The display position (in pixel unit) of the font."), Localizable(true), Category("Layout")]
        public Point FontLocate
        {
            get
            {
                return this._fontLocate;
            }
            set
            {
                this._fontLocate = value;
            }
        }

        [Category("Service"), Description("Specify an initial value of data"), Browsable(true), Localizable(true)]
        public string form
        {
            get
            {
                return this._form;
            }
            set
            {
                this._form = value;
            }
        }

        [Description("Item ID"), Category("Service"), Browsable(true), Localizable(true)]
        public string id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        [Localizable(true), Description("Specify whether an input or an output (not editable)"), Browsable(true), Category("Service")]
        public string input_output
        {
            get
            {
                return this._input_output;
            }
            set
            {
                this._input_output = value;
                this.SetBackColor();
            }
        }

        [Browsable(true), Category("Service"), Description("Item Name"), Localizable(true)]
        public string name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        [Localizable(true), Browsable(true), Description("Order of message item"), Category("Service")]
        public int order
        {
            get
            {
                return this._order;
            }
            set
            {
                this._order = value;
            }
        }

        [Localizable(true), Browsable(true), Category("Service"), Description("Specify whether the required input or not")]
        public string required
        {
            get
            {
                return this._required;
            }
            set
            {
                this._required = value;
                this.SetBackColor();
            }
        }
    }
}

