namespace Naccs.Common.CustomControls
{
    using Naccs.Common.Function;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    [DisplayName("TextBox with Divided"), ToolboxBitmap(typeof(TextBox))]
    public class LBDivTextBox : UserControl, IItemAttributesEx, IItemAttributes
    {
        private string _attribute = "";
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
        private string bindText = "";
        private string checkAttribute = "";
        private bool CheckSelectNextCtl;
        private IContainer components;
        private int fFigure;
        private bool flgPos;
        private JobErrInfo jobErr;
        private Label lblSplit;
        private ExTextBox LTextBox;
        private string refName = "";
        private string rep_ID = "";
        private ExTextBox RTextBox;
        private StrFunc SF = StrFunc.CreateInstance();

        public event EventHandler OnOmitCtlInputSupport;

        public LBDivTextBox()
        {
            this.InitializeComponent();
            base.BindingContextChanged += new EventHandler(this.LBDivTextBox_BindingContextChanged);
            this.LTextBox.KeyPress += new KeyPressEventHandler(this.TextBox_KeyPress);
            this.RTextBox.KeyPress += new KeyPressEventHandler(this.TextBox_KeyPress);
            this.DoubleBuffered = true;
        }

        private void BS_PositionChanged(object sender, EventArgs e)
        {
            if (this.jobErr != null)
            {
                this.SetBackColor();
            }
        }

        public void Copy()
        {
            if (this.LTextBox.Focused)
            {
                this.LTextBox.Copy();
            }
            else if (this.RTextBox.Focused)
            {
                this.RTextBox.Copy();
            }
        }

        public void Cut()
        {
            if (this.LTextBox.Focused)
            {
                this.LTextBox.Cut();
            }
            else if (this.RTextBox.Focused)
            {
                this.RTextBox.Cut();
            }
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
            this.lblSplit = new Label();
            this.RTextBox = new ExTextBox();
            this.LTextBox = new ExTextBox();
            base.SuspendLayout();
            this.lblSplit.AutoSize = true;
            this.lblSplit.Dock = DockStyle.Left;
            this.lblSplit.Location = new Point(0x40, 0);
            this.lblSplit.Name = "lblSplit";
            this.lblSplit.Padding = new Padding(0, 4, 0, 0);
            this.lblSplit.Size = new System.Drawing.Size(11, 0x10);
            this.lblSplit.TabIndex = 1;
            this.lblSplit.Text = "-";
            this.RTextBox.attribute = "X";
            this.RTextBox.CharacterCasing = CharacterCasing.Upper;
            this.RTextBox.DisplayWidth = 0;
            this.RTextBox.Dock = DockStyle.Left;
            this.RTextBox.figure = 0;
            this.RTextBox.Location = new Point(0x4b, 0);
            this.RTextBox.MaxLength = 0;
            this.RTextBox.Name = "RTextBox";
            this.RTextBox.Size = new System.Drawing.Size(0x40, 0x13);
            this.RTextBox.TabIndex = 2;
            this.RTextBox.TextChanged += new EventHandler(this.RTextBox_TextChanged);
            this.LTextBox.attribute = "X";
            this.LTextBox.CharacterCasing = CharacterCasing.Upper;
            this.LTextBox.DisplayWidth = 0;
            this.LTextBox.Dock = DockStyle.Left;
            this.LTextBox.figure = 0;
            this.LTextBox.Location = new Point(0, 0);
            this.LTextBox.MaxLength = 0;
            this.LTextBox.Name = "LTextBox";
            this.LTextBox.Size = new System.Drawing.Size(0x40, 0x13);
            this.LTextBox.TabIndex = 0;
            this.LTextBox.TextChanged += new EventHandler(this.LTextBox_TextChanged);
            this.LTextBox.KeyDown += new KeyEventHandler(this.LTextBox_KeyDown);
            this.AutoSize = true;
            base.Controls.Add(this.RTextBox);
            base.Controls.Add(this.lblSplit);
            base.Controls.Add(this.LTextBox);
            this.MinimumSize = new System.Drawing.Size(0, 20);
            base.Name = "LBDivTextBox";
            base.Size = new System.Drawing.Size(0x8e, 20);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void LBDivTextBox_BindingContextChanged(object sender, EventArgs e)
        {
            if ((!this.flgPos && (base.DataBindings.Count > 0)) && (base.DataBindings[0].BindingManagerBase != null))
            {
                base.DataBindings[0].BindingManagerBase.PositionChanged += new EventHandler(this.BS_PositionChanged);
                this.flgPos = true;
            }
        }

        private void LTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (e.Modifiers == Keys.Shift))
            {
                bool foward = e.Modifiers != Keys.Shift;
                ControlFunc.SelectNextControlEx(this, foward);
            }
        }

        private void LTextBox_TextChanged(object sender, EventArgs e)
        {
            this.SetBackColor();
            base.DataBindings["BindText"].WriteValue();
            if (this.CheckSelectNextCtl)
            {
                if ((this.LTextBox.Text != null) && (this.SF.GetByteLength(this.LTextBox.Text) == this.L_MaxLength))
                {
                    base.SelectNextControl(this.LTextBox, true, true, true, true);
                }
                this.CheckSelectNextCtl = false;
            }
        }

        public void Paste()
        {
            if (this.LTextBox.Focused)
            {
                this.LTextBox.Paste();
            }
            else if (this.RTextBox.Focused)
            {
                this.RTextBox.Paste();
                if (((this.LTextBox.Text == "") && !(this.refName == "")) && (this.OnOmitCtlInputSupport != null))
                {
                    this.OnOmitCtlInputSupport(this, null);
                }
            }
        }

        private void RTextBox_TextChanged(object sender, EventArgs e)
        {
            this.SetBackColor();
            base.DataBindings["BindText"].WriteValue();
            if (this.CheckSelectNextCtl)
            {
                if ((this.RTextBox.Text != null) && (this.SF.GetByteLength(this.RTextBox.Text) == this.R_MaxLength))
                {
                    ControlFunc.SelectNextControlEx(this, true);
                }
                this.CheckSelectNextCtl = false;
            }
        }

        public void SetBackColor()
        {
            if (((base.DataBindings.Count > 0) && (this.jobErr != null)) && this.jobErr.CheckTrimErrInfo(this.id, this.CurrentPage, this.BindText))
            {
                this.LTextBox.BackColor = DesignControls.ErrorColor;
                this.RTextBox.BackColor = DesignControls.ErrorColor;
            }
            else if (this._input_output.Equals("-"))
            {
                this.LTextBox.BackColor = DesignControls.ReadOnlyBackColor;
                this.RTextBox.BackColor = DesignControls.ReadOnlyBackColor;
            }
            else if (((this._required == "M") && (this.L_Text.Length == 0)) && (this.R_Text.Length == 0))
            {
                this.LTextBox.BackColor = DesignControls.MandatoryBackColor;
                this.RTextBox.BackColor = DesignControls.MandatoryBackColor;
            }
            else
            {
                this.LTextBox.BackColor = DesignControls.NormalBackColor;
                this.RTextBox.BackColor = DesignControls.NormalBackColor;
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((this._input_output != "-") && (e.KeyChar != '\b'))
            {
                Control control = (Control) sender;
                if ((((control.Name == this.RTextBox.Name) && (this.LTextBox.Text == "")) && (!(this.refName == "") && (this.OnOmitCtlInputSupport != null))) && ((e.KeyChar != '\r') && (e.KeyChar != '\x001b')))
                {
                    this.OnOmitCtlInputSupport(this, e);
                }
                this.CheckSelectNextCtl = true;
            }
        }

        public void Undo()
        {
            if (this.LTextBox.Focused)
            {
                this.LTextBox.Undo();
            }
            else if (this.RTextBox.Focused)
            {
                this.RTextBox.Undo();
            }
        }

        [Category("Service"), Browsable(true), Description("Attribute"), Localizable(true)]
        public string attribute
        {
            get
            {
                return this._attribute;
            }
            set
            {
                this._attribute = value;
                this.LTextBox.attribute = value;
                this.RTextBox.attribute = value;
            }
        }

        [Description("Characters for Binding"), Browsable(false), Category("Service"), DefaultValue("")]
        public string BindText
        {
            get
            {
                string text = this.LTextBox.Text;
                string s = this.RTextBox.Text;
                if ((this.attribute == "I") || (this.attribute == "F"))
                {
                    this.bindText = this.SF.FillBlankLeft(text, this.L_MaxLength) + this.SF.FillBlankLeft(s, this.R_MaxLength);
                }
                else
                {
                    this.bindText = this.SF.FillBlankRight(text, this.L_MaxLength) + this.SF.FillBlankRight(s, this.R_MaxLength);
                }
                return this.bindText;
            }
            set
            {
                if ((value != null) && (value.Length > 0))
                {
                    int iStart = this.L_MaxLength;
                    string st = null;
                    if ((this.attribute == "I") || (this.attribute == "F"))
                    {
                        st = this.SF.FillBlankLeft(value, this.figure);
                    }
                    else
                    {
                        st = this.SF.FillBlankRight(value, this.figure);
                    }
                    if (this.L_MaxLength > 0)
                    {
                        string str2 = st;
                        bool flag = true;
                        if (this.SF.GetByteLength(str2) > this.L_MaxLength)
                        {
                            str2 = this.SF.CutByteLength(str2, this.L_MaxLength);
                            iStart = this.SF.GetByteLength(str2);
                            flag = false;
                        }
                        if ((this.attribute == "I") || (this.attribute == "F"))
                        {
                            this.LTextBox.Text = str2.TrimStart(null);
                        }
                        else
                        {
                            this.LTextBox.Text = str2.TrimEnd(null);
                        }
                        if (flag)
                        {
                            this.RTextBox.Text = "";
                            return;
                        }
                    }
                    if (this.R_MaxLength > 0)
                    {
                        string str3 = this.SF.SubByteString(st, iStart, this.R_MaxLength);
                        if (this.SF.GetByteLength(str3) > this.R_MaxLength)
                        {
                            str3 = this.SF.CutByteLength(str3, this.R_MaxLength);
                        }
                        if ((this.attribute == "I") || (this.attribute == "F"))
                        {
                            this.RTextBox.Text = str3.TrimStart(null);
                        }
                        else
                        {
                            this.RTextBox.Text = str3.TrimEnd(null);
                        }
                    }
                }
                else
                {
                    this.LTextBox.Text = "";
                    this.RTextBox.Text = "";
                }
            }
        }

        [Browsable(true), Description("Specify whether to check attribute or not."), Category("Service"), Localizable(true)]
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

        [Browsable(true), Localizable(true), Category("Service"), Description("Specify whether to check full digit input or not")]
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

        [Category("Service"), Localizable(true), Browsable(true), Description("Selections for radio button.")]
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

        public override System.Windows.Forms.ContextMenuStrip ContextMenuStrip
        {
            get
            {
                return base.ContextMenuStrip;
            }
            set
            {
                base.ContextMenuStrip = value;
                this.LTextBox.ContextMenuStrip = value;
                this.RTextBox.ContextMenuStrip = value;
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

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                this.LTextBox.ForeColor = value;
                this.RTextBox.ForeColor = value;
            }
        }

        [Localizable(true), Browsable(true), Description("Specify an initial value of data"), Category("Service")]
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
                return this.LTextBox.BackColor;
            }
            set
            {
                this.LTextBox.BackColor = value;
                this.RTextBox.BackColor = value;
            }
        }

        [Description("Item ID"), Localizable(true), Category("Service"), Browsable(true)]
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
                bool flag = (this._input_output != null) && this._input_output.Equals("-");
                this.LTextBox.ReadOnly = flag;
                this.RTextBox.ReadOnly = flag;
                this.LTextBox.TabStop = !flag;
                this.RTextBox.TabStop = !flag;
                base.TabStop = !flag;
                this.SetBackColor();
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

        [DefaultValue(""), Description("Length of Left TextBox"), Category("Service")]
        public int L_MaxLength
        {
            get
            {
                return this.LTextBox.figure;
            }
            set
            {
                this.LTextBox.figure = value;
            }
        }

        [Description("Text of Left TextBox"), Category("Service"), DefaultValue("")]
        public string L_Text
        {
            get
            {
                return this.LTextBox.Text;
            }
            set
            {
                this.LTextBox.Text = value;
            }
        }

        [Description("Width of Left TextBox"), DefaultValue(""), Category("Service")]
        public int L_Width
        {
            get
            {
                return this.LTextBox.Width;
            }
            set
            {
                this.LTextBox.Width = value;
            }
        }

        [Localizable(true), Browsable(true), Category("Service"), Description("Item Name")]
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

        [Browsable(true), Description("Order of message item"), Localizable(true), Category("Service")]
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

        [Description("Length of Right TextBox"), Category("Service"), DefaultValue("")]
        public int R_MaxLength
        {
            get
            {
                return this.RTextBox.figure;
            }
            set
            {
                this.RTextBox.figure = value;
            }
        }

        [Category("Service"), DefaultValue(""), Description("Text of Right TextBox")]
        public string R_Text
        {
            get
            {
                return this.RTextBox.Text;
            }
            set
            {
                this.RTextBox.Text = value;
            }
        }

        [Description("Width of Right TextBox"), Category("Service"), DefaultValue("")]
        public int R_Width
        {
            get
            {
                return this.RTextBox.Width;
            }
            set
            {
                this.RTextBox.Width = value;
            }
        }

        [DefaultValue(""), Category("Service"), Description("Set up each field of the copy source with the item ID.")]
        public string RefName
        {
            get
            {
                return this.refName;
            }
            set
            {
                this.refName = value;
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

        [Browsable(true), Category("Service"), Description("Specify whether the required input or not"), Localizable(true)]
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

