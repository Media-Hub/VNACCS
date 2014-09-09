namespace Naccs.Common.CustomControls
{
    using Naccs.Common.Function;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    [DisplayName("Calender"), ToolboxBitmap(typeof(DateTimePicker))]
    public class LBDateTimePicker : DateTimePicker, IItemAttributesEx, IItemAttributes
    {
        private string _attribute = "";
        private string _check_date = "";
        private string _check_full = "";
        private string _check_time = "";
        private string _choice_keyvalue = "";
        private string _customFormat = "";
        private int _figure = 1;
        private string _form = "";
        private string _id = "";
        private string _input_output = " ";
        private string _name = "";
        private int _order = -1;
        private string _required = "";
        private string checkAttribute = "";
        private IContainer components;
        private bool flgChange;
        private bool flgPos;
        private JobErrInfo jobErr;
        private SolidBrush m_BackBrush;
        private string rep_ID = "";
        private static int WM_ERASEBKGND = 20;

        public LBDateTimePicker()
        {
            base.ValueChanged += new EventHandler(this.lbDateTimePicker_ValueChanged);
            base.PreviewKeyDown += new PreviewKeyDownEventHandler(this.lbDateTimePicker_PreviewKeyDown);
            base.CloseUp += new EventHandler(this.lbDateTimePicker_CloseUp);
            base.BindingContextChanged += new EventHandler(this.LBDateTimePicker_BindingContextChanged);
            this.DoubleBuffered = true;
        }

        private void BS_PositionChanged(object sender, EventArgs e)
        {
            if (this.jobErr != null)
            {
                this.SetBackColor();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            if (disposing && (this.m_BackBrush != null))
            {
                this.m_BackBrush.Dispose();
            }
            base.Dispose(disposing);
        }

        private void LBDateTimePicker_BindingContextChanged(object sender, EventArgs e)
        {
            if ((!this.flgPos && (base.DataBindings.Count > 0)) && (base.DataBindings[0].BindingManagerBase != null))
            {
                base.DataBindings[0].BindingManagerBase.PositionChanged += new EventHandler(this.BS_PositionChanged);
                this.flgPos = true;
            }
        }

        private void lbDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            this.flgChange = true;
            this.SetBackColor();
        }

        private void lbDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            this.flgChange = true;
            this.SetBackColor();
            if (e.KeyCode == Keys.Enter)
            {
                bool foward = e.Modifiers != Keys.Shift;
                ControlFunc.SelectNextControlEx(this, foward);
            }
        }

        private void lbDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            this.setCustomFormat(sender);
            base.DataBindings["BindText"].WriteValue();
        }

        public void SetBackColor()
        {
            if (((base.DataBindings.Count > 0) && (this.jobErr != null)) && this.jobErr.CheckErrInfo(this.id, this.CurrentPage, this.BindText))
            {
                this.BackColor = DesignControls.ErrorColor;
            }
            else if (this.flgChange)
            {
                this.BackColor = DesignControls.NormalBackColor;
            }
        }

        private void setCustomFormat(object sender)
        {
            if (this.Format == DateTimePickerFormat.Custom)
            {
                IFormatProvider provider = new CultureInfo("en-us");
                base.CustomFormat = "'" + base.Value.ToString(this._customFormat, provider) + "'";
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_ERASEBKGND)
            {
                Graphics graphics = Graphics.FromHdc(m.WParam);
                if (this.m_BackBrush == null)
                {
                    this.m_BackBrush = new SolidBrush(this.BackColor);
                }
                graphics.FillRectangle(this.m_BackBrush, base.ClientRectangle);
                graphics.Dispose();
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        [Localizable(true), Description("Attribute"), Browsable(true), Category("Service")]
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

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                if (this.m_BackBrush != null)
                {
                    this.m_BackBrush.Dispose();
                }
                base.BackColor = value;
                this.m_BackBrush = new SolidBrush(this.BackColor);
                base.Invalidate();
            }
        }

        [Description("Characters for Binding"), Browsable(true), Localizable(true), Category("Service")]
        public string BindText
        {
            get
            {
                return base.Value.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            }
            set
            {
                try
                {
                    string s = value.Substring(0, 4);
                    string str2 = value.Substring(4, 2);
                    string str3 = value.Substring(6, 2);
                    base.Value = new DateTime(int.Parse(s), int.Parse(str2), int.Parse(str3));
                }
                catch
                {
                }
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

        [Description("Specify whether to check the validity of date or not"), Browsable(true), Localizable(true), Category("Service")]
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

        [Description("Specify whether to check full digit input or not"), Category("Service"), Browsable(true), Localizable(true)]
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

        [Description("Specify whether it checks date validity "), Browsable(true), Localizable(true), Category("Service")]
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

        public string CustomFormat
        {
            get
            {
                return this._customFormat;
            }
            set
            {
                this._customFormat = value;
                this.setCustomFormat(this);
            }
        }

        [Localizable(true), Browsable(true), Description("Digit number"), Category("Service")]
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

        [Browsable(true), Description("Specify an initial value of data"), Localizable(true), Category("Service")]
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

        public DateTimePickerFormat Format
        {
            get
            {
                return base.Format;
            }
            set
            {
                base.Format = value;
                this.setCustomFormat(this);
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
                this.BackColor = value;
            }
        }

        [Browsable(true), Category("Service"), Description("Item ID"), Localizable(true)]
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

        [Localizable(true), Description("Specify whether an input or an output (not editable)"), Category("Service"), Browsable(true)]
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

        [Browsable(true), Localizable(true), Description("Item Name"), Category("Service")]
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
                this.SetBackColor();
            }
        }
    }
}

