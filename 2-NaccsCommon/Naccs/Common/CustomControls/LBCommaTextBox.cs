namespace Naccs.Common.CustomControls
{
    using Naccs.Common.Function;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(TextBox)), DisplayName("TextBox with Thousand Separator")]
    public class LBCommaTextBox : TextBox, IItemAttributesEx, IItemAttributes
    {
        private string _attribute = "F";
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
        private string[] AryUndotext = new string[2];
        private string bindText = "";
        private string checkAttribute = "";
        private bool CheckSelectNextCtl;
        private string currencyMark = "";
        private int Figure;
        private bool flgPos;
        private bool iscomma = true;
        private JobErrInfo jobErr;
        private string rep_ID = "";
        private StrFunc SF = StrFunc.CreateInstance();
        private int stat_flag = 1;

        public LBCommaTextBox()
        {
            base.CausesValidation = false;
            base.TextAlign = HorizontalAlignment.Right;
            base.ImeMode = ImeMode.Disable;
            base.KeyPress += new KeyPressEventHandler(this.CommaTextBox_KeyPress);
            base.Click += new EventHandler(this.CommaTextBox_EventCusorOnRightEnd);
            base.Enter += new EventHandler(this.CommaTextBox_EventSelectAll);
            base.DoubleClick += new EventHandler(this.CommaTextBox_EventSelectAll);
            base.KeyDown += new KeyEventHandler(this.CommaTextBox_KeyDown);
            this.Figure = this.MaxLength;
            base.BindingContextChanged += new EventHandler(this.LBCommaTextBox_BindingContextChanged);
            this.DoubleBuffered = true;
        }

        private void BS_PositionChanged(object sender, EventArgs e)
        {
            if (this.jobErr != null)
            {
                this.SetBackColor();
            }
        }

        private void CommaTextBox_EventCusorOnRightEnd(object sender, EventArgs e)
        {
            base.SelectionStart = this.Text.Length;
        }

        private void CommaTextBox_EventSelectAll(object sender, EventArgs e)
        {
            base.SelectAll();
        }

        private void CommaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.End:
                case Keys.Home:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Delete:
                    e.Handled = true;
                    return;

                case Keys.Right:
                    break;

                case Keys.Enter:
                {
                    bool foward = e.Modifiers != Keys.Shift;
                    ControlFunc.SelectNextControlEx(this, foward);
                    break;
                }
                default:
                    return;
            }
        }

        private void CommaTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this._input_output != "-")
            {
                if (this.SelectionLength == this.Text.Length)
                {
                    if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (e.KeyChar == '-'))
                    {
                        this.CheckSelectNextCtl = true;
                        this.BindText = e.KeyChar.ToString();
                    }
                    else if (e.KeyChar == '\b')
                    {
                        this.BindText = "";
                    }
                }
                else if ((e.KeyChar == '\b') && (this.bindText.Length > 0))
                {
                    string str = this.bindText.Remove(this.bindText.Length - 1);
                    this.CheckSelectNextCtl = true;
                    this.BindText = str;
                }
                else if (this.InputCheck(e.KeyChar))
                {
                    this.CheckSelectNextCtl = true;
                    this.BindText = this.BindText + e.KeyChar;
                }
                e.Handled = true;
            }
        }

        private bool InputCheck(char InData)
        {
            if (InData != '\b')
            {
                if (this.bindText.Length >= this.figure)
                {
                    return false;
                }
                if ((InData >= '0') && (InData <= '9'))
                {
                    return true;
                }
                if (InData != ',')
                {
                    return false;
                }
                if (this.attribute != "F")
                {
                    return false;
                }
                if ("-".Equals(this.Text))
                {
                    char ch = ',';
                    if (ch.Equals(InData))
                    {
                        return false;
                    }
                }
                if (this.Text.IndexOf(',') != -1)
                {
                    return false;
                }
            }
            return true;
        }

        private void LBCommaTextBox_BindingContextChanged(object sender, EventArgs e)
        {
            if ((!this.flgPos && (base.DataBindings.Count > 0)) && (base.DataBindings[0].BindingManagerBase != null))
            {
                base.DataBindings[0].BindingManagerBase.PositionChanged += new EventHandler(this.BS_PositionChanged);
                this.flgPos = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) && (((e.X < 0) || (e.Y < 0)) || ((e.X >= base.ClientSize.Width) || (e.Y >= base.ClientSize.Height))))
            {
                base.Capture = false;
            }
            base.OnMouseMove(e);
        }

        public void SetBackColor()
        {
            if (((base.DataBindings.Count > 0) && (this.jobErr != null)) && this.jobErr.CheckErrInfo(this.id, this.CurrentPage, this.BindText))
            {
                this.BackColor = DesignControls.ErrorColor;
            }
            else if (base.ReadOnly)
            {
                this.BackColor = DesignControls.ReadOnlyBackColor;
            }
            else if ((this._required == "M") && (this.Text.Length == 0))
            {
                this.BackColor = DesignControls.MandatoryBackColor;
            }
            else
            {
                this.BackColor = DesignControls.NormalBackColor;
            }
        }

        public void undo()
        {
            if (this.input_output != "-")
            {
                if (this.stat_flag == 1)
                {
                    this.AryUndotext[0] = this.Text.ToString();
                    this.Text = this.AryUndotext[1];
                    this.stat_flag = 0;
                }
                else if (this.stat_flag == 0)
                {
                    this.AryUndotext[1] = this.Text.ToString();
                    this.Text = this.AryUndotext[0];
                    this.stat_flag = 1;
                }
                base.SelectAll();
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == DesignControls.WM_PASTE)
            {
                if (this._input_output == "-")
                {
                    return;
                }
                IDataObject dataObject = Clipboard.GetDataObject();
                if (dataObject.GetDataPresent(DataFormats.UnicodeText))
                {
                    string data = (string) dataObject.GetData(DataFormats.UnicodeText);
                    bool flag = false;
                    if (this.attribute == "F")
                    {
                        flag = this.SF.IsNumeric(data);
                    }
                    else
                    {
                        flag = this.SF.IsIntegralNum(data);
                    }
                    if (flag)
                    {
                        this.BindText = data;
                    }
                    return;
                }
            }
            if (m.Msg == DesignControls.WM_COPY)
            {
                Clipboard.SetDataObject(this.bindText);
            }
            else if (m.Msg == DesignControls.WM_CUT)
            {
                if (this._input_output != "-")
                {
                    Clipboard.SetDataObject(this.bindText);
                    this.BindText = "";
                }
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        [Description("Attribute"), Category("Service"), Browsable(true), Localizable(true)]
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

        [DefaultValue(""), Browsable(false), Category("Service"), Description("Characters for Binding")]
        public string BindText
        {
            get
            {
                return this.bindText;
            }
            set
            {
                if (value != this.bindText)
                {
                    if (((value != null) && (value.Length > 0)) && (value.Length <= this.figure))
                    {
                        if (this.attribute == "F")
                        {
                            if (!this.SF.IsNumeric(value))
                            {
                                value = "";
                            }
                        }
                        else if (!this.SF.IsIntegralNum(value))
                        {
                            value = "";
                        }
                        this.Text = this.SF.MakeYenCommaStr(value.ToString(), this.currencyMark, this.IsComma);
                        base.SelectionStart = this.Text.Length;
                        this.bindText = value.ToString();
                    }
                    else
                    {
                        this.Text = "";
                        this.bindText = "";
                    }
                    base.DataBindings["BindText"].WriteValue();
                    this.SetBackColor();
                    if (this.CheckSelectNextCtl)
                    {
                        if ((this.Focused && (this.bindText.Length == this.figure)) && (base.Parent != null))
                        {
                            ControlFunc.SelectNextControlEx(this, true);
                        }
                        this.CheckSelectNextCtl = false;
                    }
                }
            }
        }

        [Description("Specify whether to check attribute or not."), Browsable(true), Category("Service"), Localizable(true)]
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

        [Localizable(true), Browsable(true), Description("Specify whether to check the validity of date or not"), Category("Service")]
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

        [Localizable(true), Browsable(true), Category("Service"), Description("Specify whether to check full digit input or not")]
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

        [Browsable(true), Description("Specify whether it checks date validity "), Localizable(true), Category("Service")]
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

        [Description("Selections for radio button."), Browsable(true), Localizable(true), Category("Service")]
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
                this.currencyMark = value;
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

        [Localizable(true), Category("Service"), Description("Digit Number (except currency mark and thousand separator)"), Browsable(true), DefaultValue("MaxLength")]
        public int figure
        {
            get
            {
                return this.Figure;
            }
            set
            {
                this.Figure = value;
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

        [Description("Specify whether an input or an output (not editable)"), Browsable(true), Localizable(true), Category("Service")]
        public string input_output
        {
            get
            {
                return this._input_output;
            }
            set
            {
                this._input_output = value;
                base.ReadOnly = (this._input_output != null) && this._input_output.Equals("-");
                base.TabStop = (this._input_output == null) || !this._input_output.Equals("-");
                this.SetBackColor();
            }
        }

        [Description("Specify whether to use thousand separator"), DefaultValue("true"), Category("Service")]
        public bool IsComma
        {
            get
            {
                return this.iscomma;
            }
            set
            {
                this.iscomma = value;
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

        [Category("Service"), Description("Item Name"), Localizable(true), Browsable(true)]
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

        [Description("Order of message item"), Localizable(true), Category("Service"), Browsable(true)]
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

        [Description("Specify whether the required input or not"), Category("Service"), Browsable(true), Localizable(true)]
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

        public HorizontalAlignment TextAlign
        {
            get
            {
                return base.TextAlign;
            }
            set
            {
                if (value == HorizontalAlignment.Right)
                {
                    base.TextAlign = value;
                }
            }
        }
    }
}

