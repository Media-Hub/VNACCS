namespace Naccs.Common.CustomControls
{
    using Naccs.Common.Function;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [DisplayName("RadioButton"), ToolboxBitmap(typeof(RadioButton))]
    public class LBRadioButton : RadioButton, IItemAttributesEx, IItemAttributes
    {
        private string _attribute = "";
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
        private string checkAttribute = "";
        private string checkedText = "0";
        private IContainer components;
        private JobErrInfo jobErr;
        private string rep_ID = "";

        public LBRadioButton()
        {
            base.CheckedChanged += new EventHandler(this.CheckedChekgedEx);
            base.KeyDown += new KeyEventHandler(this.LBRadioButton_KeyDown);
            this.DoubleBuffered = true;
        }

        private void CheckedChekgedEx(object sender, EventArgs e)
        {
            if (base.Checked)
            {
                this.checkedText = "1";
            }
            else
            {
                this.checkedText = "0";
            }
            base.DataBindings["CheckedText"].WriteValue();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void LBRadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bool foward = e.Modifiers != Keys.Shift;
                ControlFunc.SelectNextControlEx(this, foward);
            }
        }

        public void SetBackColor()
        {
        }

        [Category("Service"), Description("Attribute"), Browsable(true), Localizable(true)]
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

        [Localizable(true), Description("Specify whether to check attribute or not."), Category("Service"), Browsable(true)]
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

        [Category("Service"), Browsable(true), Localizable(true), Description("Specify whether to check the validity of date or not")]
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

        [Localizable(true), Description("Specify whether to check full digit input or not"), Category("Service"), Browsable(true)]
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

        public string CheckedText
        {
            get
            {
                return this.checkedText;
            }
            set
            {
                if ((value == null) || (value == ""))
                {
                    this.checkedText = "0";
                }
                else
                {
                    this.checkedText = value;
                }
                if (this.checkedText == "0")
                {
                    base.Checked = false;
                }
                else
                {
                    base.Checked = true;
                }
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

        public int CurrentPage
        {
            get
            {
                if (base.DataBindings.Count <= 0)
                {
                    return -1;
                }
                if (this.rep_ID == "")
                {
                    return 0;
                }
                return (base.DataBindings[0].BindingManagerBase.Position + 1);
            }
        }

        [Category("Service"), Description("Digit number"), Browsable(true), Localizable(true)]
        public int figure
        {
            get
            {
                return this._figure;
            }
            set
            {
                this._figure = value;
            }
        }

        [Browsable(true), Category("Service"), Description("Specify an initial value of data"), Localizable(true)]
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

        public Color IBackColor
        {
            get
            {
                return this.BackColor;
            }
            set
            {
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

        [Browsable(true), Description("Specify whether an input or an output (not editable)"), Localizable(true), Category("Service")]
        public string input_output
        {
            get
            {
                return this._input_output;
            }
            set
            {
                this._input_output = value;
                base.Enabled = (this._input_output != null) && this._input_output.Equals("+");
            }
        }

        public JobErrInfo JobErr
        {
            get
            {
                return this.jobErr;
            }
            set
            {
                this.jobErr = value;
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
                if ((this.Text == null) || (this.Text.Length == 0))
                {
                    this.Text = value;
                }
            }
        }

        [Description("Order of message item"), Category("Service"), Browsable(true), Localizable(true)]
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

        public string Rep_ID
        {
            get
            {
                return this.rep_ID;
            }
            set
            {
                this.rep_ID = value;
            }
        }

        [Description("Specify whether the required input or not"), Browsable(true), Localizable(true), Category("Service")]
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

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                if ((value != null) && (value.Length != 0))
                {
                    base.Text = value;
                }
            }
        }
    }
}

