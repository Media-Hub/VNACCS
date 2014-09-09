namespace Naccs.Common.CustomControls
{
    using Naccs.Common.Function;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [DisplayName("Barcode")]
    public class LBBarcode : Control, IItemAttributes
    {
        private string _attribute = "X";
        private float _byteWidth;
        private string _check_date = "";
        private string _check_full = "";
        private string _check_time = "";
        private string _choice_keyvalue = "";
        private string _form = "";
        private string _id = "";
        private string _input_output = " ";
        private string _name = "";
        private int _order = -1;
        private string _required = "";
        private string BarcodeText = " ";
        private static DotNetBarcode bc1;
        private string checkAttribute = "";
        private int fFigure;
        private StrFunc SF = StrFunc.CreateInstance();

        public LBBarcode()
        {
            base.DoubleBuffered = true;
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.DoubleBuffered = true;
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            bc1 = new DotNetBarcode(DotNetBarcode.Types.Code39);
            this._byteWidth = 12f;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            float x = 0f;
            float y = 0f;
            float num3 = this.ByteWidth * (this.fFigure + 2);
            float height = base.Size.Height;
            base.Width = (int) num3;
            base.Height = (int) height;
            bc1.FontName = this.Font.Name;
            bc1.FontSize = this.Font.Size;
            bc1.FontBold = this.Font.Bold;
            bc1.FontItalic = this.Font.Italic;
            bc1.FontColor = this.ForeColor;
            bc1.FontBackGroundColor = this.BackColor;
            this.BarcodeText = this.SF.MakeCycleStr(this.fFigure, "X");
            bc1.BarColor = this.ForeColor;
            bc1.BackGroundColor = this.BackColor;
            bc1.WriteBar(this.BarcodeText, x, y, num3, height, e.Graphics);
        }

        public void SetBackColor()
        {
            this.BackColor = SystemColors.Control;
        }

        [Browsable(true), Description("Attribute"), Category("Service"), Localizable(true)]
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

        [Description("The barcode width of one character (in pixel unit). \n［For one dimension barcode］"), Browsable(true), Localizable(true), Category("Layout")]
        public float ByteWidth
        {
            get
            {
                return this._byteWidth;
            }
            set
            {
                this._byteWidth = value;
                this.Refresh();
            }
        }

        [Category("Service"), Description("Specify whether to check attribute or not."), Localizable(true), Browsable(true)]
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

        [Localizable(true), Browsable(true), Category("Service"), Description("Specify whether to check the validity of date or not")]
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

        [Localizable(true), Category("Service"), Browsable(true), Description("Specify whether to check full digit input or not")]
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

        [Description("Specify whether it checks date validity "), Localizable(true), Category("Service"), Browsable(true)]
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

        [Localizable(true), Description("Selections for radio button."), Browsable(true), Category("Service")]
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

        [Category("Service"), Description("Digit number"), Localizable(true), Browsable(true)]
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

        [Description("Specify an initial value of data"), Localizable(true), Category("Service"), Browsable(true)]
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

        [Localizable(true), Category("Service"), Description("Item ID"), Browsable(true)]
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

        [Category("Service"), Localizable(true), Description("Specify whether an input or an output (not editable)"), Browsable(true)]
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

        [Description("Item Name"), Browsable(true), Localizable(true), Category("Service")]
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

        [Localizable(true), Browsable(true), Category("Service"), Description("Order of message item")]
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

        [Description("Specify whether the required input or not"), Category("Service"), Localizable(true), Browsable(true)]
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

