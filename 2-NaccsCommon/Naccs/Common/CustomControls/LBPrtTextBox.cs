namespace Naccs.Common.CustomControls
{
    using Naccs.Common.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(TextBox)), DisplayName("TextBox")]
    public class LBPrtTextBox : TextBox, IItemAttributes
    {
        private string _attribute = "X";
        private string _check_date = "";
        private string _check_full = "";
        private string _check_time = "";
        private string _choice_keyvalue = "";
        private int _figure = 1;
        private string _form = "";
        private string _id = "";
        private string _input_output = " ";
        private string _name = "";
        private int _order = -1;
        private string _required = "";
        private char[] chDateFinds = new char[] { 'y', 'Y', 'd', 'D' };
        private string checkAttribute = "";
        private char[] chOtherFinds = new char[] { '#', '\\', '$', ',', 'A', 'C', '9', '_' };
        private char[] chTimeFinds = new char[] { 'h', 'H', 's', 'S' };
        private string format = "";

        public LBPrtTextBox()
        {
            base.ReadOnly = true;
            base.TextChanged += new EventHandler(this.LBPrtTextBox_TextChanged);
            this.DoubleBuffered = true;
        }

        private void LBPrtTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.Text))
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < this._figure; i++)
                {
                    builder.Append("X");
                }
                this.Text = builder.ToString();
            }
        }

        [Localizable(true), Browsable(true), Category("Service"), Description("Attribute")]
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

        [Category("Service"), Localizable(true), Description("Specify whether to check attribute or not."), Browsable(true)]
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

        [Browsable(true), Localizable(true), Category("Service"), Description("Specify whether to check the validity of date or not")]
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

        [Description("Specify whether to check full digit input or not"), Localizable(true), Category("Service"), Browsable(true)]
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

        [Localizable(true), Description("Specify whether it checks date validity "), Browsable(true), Category("Service")]
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

        [Localizable(true), Category("Service"), Description("Selections for radio button."), Browsable(true)]
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

        [Localizable(true), Description("Digit number"), Browsable(true), Category("Service")]
        public int figure
        {
            get
            {
                return this._figure;
            }
            set
            {
                this._figure = value;
                string str = "";
                int num = this._figure;
                if (this._attribute == "W")
                {
                    num /= 3;
                }
                for (int i = 0; i < num; i++)
                {
                    str = str + "X";
                }
                this.Text = str;
            }
        }

        [Browsable(true), Localizable(true), Category("Service"), Description("Specify an initial value of data")]
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

        [Description("This is the format setting character string  for TextBox with Format/Thousand Separator/Date edit.\n\n* Month is expressed by \"MM\" and  minutes by \"mm\". Be careful not to make a mistake in specifying the format.")]
        public string Format
        {
            get
            {
                return this.format;
            }
            set
            {
                this.format = value;
                if ((!string.IsNullOrEmpty(this._id) && (value.IndexOfAny(this.chOtherFinds) < 0)) && (((value.IndexOfAny(this.chDateFinds) >= 0) && (value.IndexOf('M') < 0)) || ((value.IndexOfAny(this.chTimeFinds) >= 0) && (value.IndexOf('m') < 0))))
                {
                    MessageBox.Show(Resources.ResourceManager.GetString("COM01") + value + Resources.ResourceManager.GetString("COM02") + Resources.ResourceManager.GetString("COM03") + Resources.ResourceManager.GetString("COM04"));
                }
            }
        }

        [Description("Item ID"), Browsable(true), Localizable(true), Category("Service")]
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

        [Category("Service"), Browsable(true), Localizable(true), Description("Specify whether an input or an output (not editable)")]
        public string input_output
        {
            get
            {
                return this._input_output;
            }
            set
            {
                this._input_output = value;
            }
        }

        [Localizable(true), Description("Item Name"), Category("Service"), Browsable(true)]
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

        [Browsable(true), Localizable(true), Category("Service"), Description("Order of message item")]
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

        [Description("Specify whether the required input or not"), Localizable(true), Category("Service"), Browsable(true)]
        public string required
        {
            get
            {
                return this._required;
            }
            set
            {
                this._required = value;
            }
        }
    }
}

