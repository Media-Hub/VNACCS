namespace Naccs.Common.CustomControls
{
    using Naccs.Common.Function;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(TextBox)), DisplayName("TextBox")]
    public class LBTextBox : ExTextBox, IItemAttributesEx, IItemAttributes, Naccs.Common.CustomControls.IAutoComplete
    {
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
        private string bindText;
        private string checkAttribute = "";
        private IContainer components;
        private bool flgPos;
        private bool isCollectionOpen;
        private bool isTextSetFlag;
        private JobErrInfo jobErr;
        private string rep_ID = "";
        private StrFunc SF = StrFunc.CreateInstance();
        private int stat_flag = 1;
        private bool useAutoComplete;

        public event EventHandler OnSendBarcode;

        public LBTextBox()
        {
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.AutoCompleteMode = AutoCompleteMode.Suggest;
            base.AutoCompleteSource = AutoCompleteSource.CustomSource;
            base.CharacterCasing = CharacterCasing.Upper;
            base.TextChanged += new EventHandler(this.LBTextBox_TextChanged);
            base.BindingContextChanged += new EventHandler(this.LBTextBox_BindingContextChanged);
            base.KeyUp += new KeyEventHandler(this.LBTextBox_KeyUp);
            base.PreviewKeyDown += new PreviewKeyDownEventHandler(this.LBTextBox_PreviewKeyDown);
            this.DoubleBuffered = true;
            base.KeyDown += new KeyEventHandler(this.LBTextBox_KeyDown);
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
            base.Dispose(disposing);
        }

        protected override void ExTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = base.bSelectionAndKeyEventHandled;
            base.ExTextBox_KeyPress(sender, e);
        }

        private void LBTextBox_BindingContextChanged(object sender, EventArgs e)
        {
            if ((!this.flgPos && (base.DataBindings.Count > 0)) && (base.DataBindings[0].BindingManagerBase != null))
            {
                base.DataBindings[0].BindingManagerBase.PositionChanged += new EventHandler(this.BS_PositionChanged);
                this.flgPos = true;
            }
        }

        private void LBTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.input_output != "-")
            {
                int selectionStart;
                base.bSelectionAndKeyEventHandled = false;
                if (e.KeyCode == Keys.Back)
                {
                    selectionStart = base.SelectionStart;
                    if (selectionStart > 0)
                    {
                        int firstCharIndexOfCurrentLine = base.GetFirstCharIndexOfCurrentLine();
                        if (this.SelectionLength == 0)
                        {
                            if (selectionStart == firstCharIndexOfCurrentLine)
                            {
                                if ((selectionStart >= "\r\r\n".Length) && (this.Text.Substring(selectionStart - "\r\r\n".Length, "\r\r\n".Length) == "\r\r\n"))
                                {
                                    this.Text = this.Text.Remove(selectionStart - ("\r\r\n".Length + 1), 4);
                                    base.SelectionStart = selectionStart - ("\r\r\n".Length + 1);
                                }
                                else
                                {
                                    this.Text = this.Text.Remove(selectionStart - Environment.NewLine.Length, Environment.NewLine.Length);
                                    base.SelectionStart = selectionStart - Environment.NewLine.Length;
                                }
                            }
                            else if ((firstCharIndexOfCurrentLine > 1) && (selectionStart == (firstCharIndexOfCurrentLine + 1)))
                            {
                                int startIndex = selectionStart - 1;
                                string str = this.Text.Remove(startIndex, 1);
                                if ((startIndex >= "\r\r\n".Length) && (str.Substring(startIndex - "\r\r\n".Length, "\r\r\n".Length) == "\r\r\n"))
                                {
                                    startIndex -= "\r\r\n".Length;
                                }
                                this.Text = str;
                                base.SelectionStart = startIndex;
                            }
                            else
                            {
                                this.Text = this.Text.Remove(selectionStart - 1, 1);
                                base.SelectionStart = selectionStart - 1;
                            }
                            base.bSelectionAndKeyEventHandled = true;
                        }
                        else
                        {
                            this.Text = this.Text.Remove(selectionStart, this.SelectionLength);
                            base.SelectionStart = selectionStart;
                            base.bSelectionAndKeyEventHandled = true;
                        }
                    }
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    selectionStart = base.SelectionStart;
                    if (selectionStart < (this.Text.Length - 1))
                    {
                        if (this.SelectionLength == 0)
                        {
                            string str2 = this.Text.Substring(0, selectionStart);
                            string str3 = this.Text.Substring(selectionStart, this.Text.Length - selectionStart).Replace("\r\r\n", "");
                            if (str3 != "")
                            {
                                string str4 = "";
                                if (str3.StartsWith(Environment.NewLine))
                                {
                                    str4 = str3.Remove(0, Environment.NewLine.Length);
                                    this.Text = str2 + str4;
                                }
                                else
                                {
                                    str4 = str3.Remove(0, 1);
                                    this.Text = str2 + str4;
                                }
                                if (str2.EndsWith("\r\r\n") && str4.StartsWith(Environment.NewLine))
                                {
                                    selectionStart -= "\r\r\n".Length;
                                }
                            }
                        }
                        else
                        {
                            this.Text = this.Text.Remove(selectionStart, this.SelectionLength);
                        }
                        base.SelectionStart = selectionStart;
                        base.bSelectionAndKeyEventHandled = true;
                        e.Handled = true;
                    }
                }
            }
        }

        private void LBTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Down) || (e.KeyCode == Keys.Up))
            {
                this.isCollectionOpen = false;
            }
            else if (((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Escape)) && this.isCollectionOpen)
            {
                string text = this.Text;
                this.Text = "";
                this.isCollectionOpen = false;
                this.Text = text;
            }
        }

        private void LBTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if ((e.KeyCode == Keys.Down) || (e.KeyCode == Keys.Up))
            {
                this.isCollectionOpen = true;
            }
        }

        private void LBTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.Multiline)
            {
                string str = this.Text.Replace("\r\r\n", "");
                int selectionStart = base.SelectionStart;
                if (this.Text.Substring(0, selectionStart).EndsWith("\r\r\n" + Environment.NewLine))
                {
                    selectionStart -= "\r\r\n".Length;
                }
                StringBuilder builder = new StringBuilder();
                int num2 = 0;
                int startIndex = 0;
                bool flag = false;
                foreach (char ch in str)
                {
                    if (flag)
                    {
                        startIndex += num2;
                        num2 = 0;
                        flag = false;
                    }
                    else if (ComponentProperty.IsNewLineNeeded(str.Substring(startIndex, num2 + 1), this))
                    {
                        int length = builder.Length;
                        if (!base.bSelectionAndKeyEventHandled && ((((selectionStart - 2) == length) || ((selectionStart - 1) == length)) || (selectionStart == length)))
                        {
                            selectionStart += "\r\r\n".Length;
                        }
                        builder.Append("\r\r\n");
                        startIndex += num2;
                        num2 = 0;
                    }
                    num2++;
                    builder.Append(ch);
                    if (ch == '\n')
                    {
                        flag = true;
                    }
                }
                this.Text = builder.ToString();
                if (!base.bSelectionAndKeyEventHandled)
                {
                    base.SelectionStart = selectionStart;
                }
            }
            this.BindText = this.Text;
            if (!this.isTextSetFlag)
            {
                this.UndoTextSet();
                this.stat_flag = 1;
            }
            this.isTextSetFlag = false;
            if (!this.isCollectionOpen)
            {
                this.SetBackColor();
                int figure = base.figure;
                if ("W".Equals(base.attribute))
                {
                    figure = base.digit;
                }
                if ((this.BindText != null) && (base.GetLength(this.BindText) == figure))
                {
                    if (base.DoSelectNextControl)
                    {
                        if (!"W".Equals(base.attribute))
                        {
                            ControlFunc.SelectNextControlEx(this, true);
                        }
                        base.DoSelectNextControl = false;
                    }
                    if (base.DoBarCodeSend)
                    {
                        if (!"W".Equals(base.attribute) && (this.OnSendBarcode != null))
                        {
                            this.OnSendBarcode(this, null);
                        }
                        base.DoBarCodeSend = false;
                    }
                }
            }
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            if (this.Multiline)
            {
                base.OnTextChanged(new EventArgs());
            }
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (this.Multiline)
            {
                base.OnTextChanged(new EventArgs());
            }
        }

        public void SetBackColor()
        {
            if (((base.DataBindings.Count > 0) && (this.jobErr != null)) && this.jobErr.CheckErrInfo(this.id, this.CurrentPage, this.Text))
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
            this.isTextSetFlag = true;
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

        private void UndoTextSet()
        {
            this.AryUndotext[1] = this.AryUndotext[0];
            this.AryUndotext[0] = this.Text.ToString();
        }

        protected override void WndProc(ref Message m)
        {
            if ((m.Msg == DesignControls.WM_CUT) || (m.Msg == DesignControls.WM_PASTE))
            {
                this.UndoTextSet();
                this.stat_flag = 0;
                this.isTextSetFlag = true;
            }
            base.WndProc(ref m);
        }

        [Browsable(true), Category("Service"), Description("AutoComplete"), Localizable(true)]
        public bool AutoComplete
        {
            get
            {
                return this.useAutoComplete;
            }
            set
            {
                this.useAutoComplete = value;
            }
        }

        [DefaultValue(false), Category("Service"), Description("Set \"True\" if the data are sent when the value is entered to this field with the full digit in the service accompanied by bar code operation.")]
        public bool BarcodeSend
        {
            get
            {
                return base.barcodeSend;
            }
            set
            {
                base.barcodeSend = value;
            }
        }

        [Browsable(false), Description("Characters for Binding"), DefaultValue(""), Category("Service")]
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
                    if ((value != null) && (value.Length > 0))
                    {
                        string str = value.Replace("\r\r\n", "");
                        if (str == this.bindText)
                        {
                            return;
                        }
                        if (this.Text != value)
                        {
                            this.Text = value;
                        }
                        this.bindText = str;
                    }
                    else
                    {
                        this.Text = "";
                        this.bindText = "";
                    }
                    if (base.DataBindings["BindText"] != null)
                    {
                        base.DataBindings["BindText"].WriteValue();
                    }
                }
            }
        }

        [Localizable(true), Browsable(true), Category("Service"), Description("Specify whether to check attribute or not.")]
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

        [Description("Specify whether to check full digit input or not"), Browsable(true), Category("Service"), Localizable(true)]
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

        [Category("Service"), Localizable(true), Browsable(true), Description("Specify whether it checks date validity ")]
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

        [Description("Selections for radio button."), Category("Service"), Browsable(true), Localizable(true)]
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

        [Localizable(true), Category("Service"), Description("Specify an initial value of data"), Browsable(true)]
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

        [Description("Specify whether an input or an output (not editable)"), Localizable(true), Browsable(true), Category("Service")]
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

        AutoCompleteStringCollection Naccs.Common.CustomControls.IAutoComplete.AutoCompleteCustomSource
        {
            get
            {
                return base.AutoCompleteCustomSource;
            }
            set
            {
                base.AutoCompleteCustomSource = value;
            }
        }

        [Localizable(true), Category("Service"), Description("Item Name"), Browsable(true)]
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

        [Description("Specify whether the required input or not"), Localizable(true), Browsable(true), Category("Service")]
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

