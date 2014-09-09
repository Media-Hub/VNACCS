namespace Naccs.Common.CustomControls
{
    using Naccs.Common.AutoComplete;
    using Naccs.Common.Function;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    [DisplayName("TextBox with Format"), ToolboxBitmap(typeof(MaskedTextBox))]
    public class LBMaskedTextBox : MaskedTextBox, IItemAttributesEx, IItemAttributes, Naccs.Common.CustomControls.IAutoComplete
    {
        private string _attribute = "";
        private string _check_date = "";
        private string _check_full = "";
        private string _check_time = "";
        private string _choice_keyvalue = "";
        private int _figure = 1;
        private string _form = "";
        private string _id = "";
        private string _intput_output = " ";
        private string _name = "";
        private int _order = -1;
        private string _required = "";
        private string[] AryUndotext = new string[2];
        private bool autoComplete;
        private AutoCompleteStringCollection autoCompleteCustomSource;
        private string checkAttribute = "";
        private bool CheckSelectNextCtl;
        private IContainer components;
        private bool flgAutoCompShow;
        private bool flgPos;
        private WinHook hook;
        private int iSelectStart;
        private AutoCompleteEntryCollection items = new AutoCompleteEntryCollection();
        private JobErrInfo jobErr;
        private ListBox list;
        protected Form popup;
        private string rep_ID = "";
        private StrFunc SF = StrFunc.CreateInstance();
        private int stat_flag = 1;
        private AutoCompleteTriggerCollection triggers = new AutoCompleteTriggerCollection();
        private bool triggersEnabled = true;

        public LBMaskedTextBox()
        {
            base.TextChanged += new EventHandler(this.LBMaskedTextBox_TextChanged);
            base.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            base.CutCopyMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            base.KeyDown += new KeyEventHandler(this.LBMaskedTextBox_KeyDown);
            base.BindingContextChanged += new EventHandler(this.LBMaskedTextBox_BindingContextChanged);
            this.InitAutoComplete();
            base.KeyPress += new KeyPressEventHandler(this.LBMaskedTextBox_KeyPress);
            base.GotFocus += new EventHandler(this.LBMaskedTextBox_GotFocus);
            this.DoubleBuffered = true;
        }

        private void autoCompleteCustomSource_CollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            this.SetAuEntryCollection();
        }

        private void BS_PositionChanged(object sender, EventArgs e)
        {
            if (this.jobErr != null)
            {
                this.SetBackColor();
            }
        }

        private bool DefaultCmdKey(ref Message msg, Keys keyData)
        {
            bool flag = base.ProcessCmdKey(ref msg, keyData);
            if (this.triggersEnabled)
            {
                switch (this.triggers.OnCommandKey(keyData))
                {
                    case TriggerState.Show:
                        this.ShowList();
                        return flag;

                    case TriggerState.ShowAndConsume:
                        flag = true;
                        this.ShowList();
                        return flag;

                    case TriggerState.Hide:
                        this.HideList();
                        return flag;

                    case TriggerState.HideAndConsume:
                        flag = true;
                        this.HideList();
                        return flag;

                    case TriggerState.Select:
                        if (this.popup.Visible)
                        {
                            this.SelectCurrentItem();
                        }
                        return flag;

                    case TriggerState.SelectAndConsume:
                        if (this.popup.Visible)
                        {
                            flag = true;
                            this.SelectCurrentItem();
                        }
                        return flag;
                }
            }
            return flag;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private AutoCompleteEntryCollection FilterList(AutoCompleteEntryCollection list)
        {
            AutoCompleteEntryCollection entrys = new AutoCompleteEntryCollection();
            foreach (IAutoCompleteEntry entry in list)
            {
                foreach (string str in entry.MatchStrings)
                {
                    if (str.ToUpper().StartsWith(this.Text.ToUpper()))
                    {
                        entrys.Add(entry);
                        break;
                    }
                }
            }
            return entrys;
        }

        private void HideList()
        {
            this.flgAutoCompShow = false;
            if (this.hook != null)
            {
                this.hook.ReleaseHandle();
            }
            this.hook = null;
            this.popup.Hide();
        }

        private void InitAutoComplete()
        {
            this.autoCompleteCustomSource = new AutoCompleteStringCollection();
            this.popup = new Form();
            this.popup.StartPosition = FormStartPosition.Manual;
            this.popup.ShowInTaskbar = false;
            this.popup.FormBorderStyle = FormBorderStyle.None;
            this.popup.TopMost = true;
            this.popup.Deactivate += new EventHandler(this.Popup_Deactivate);
            this.popup.Load += new EventHandler(this.popup_Load);
            this.list = new ListBox();
            this.list.Cursor = Cursors.Hand;
            this.list.BorderStyle = BorderStyle.FixedSingle;
            this.list.MouseDown += new MouseEventHandler(this.List_MouseDown);
            this.list.ItemHeight = 14;
            this.list.Dock = DockStyle.Fill;
            this.popup.Controls.Add(this.list);
            this.triggers.Add(new TextLengthTrigger(2));
            this.triggers.Add(new ShortCutTrigger(Keys.Enter, TriggerState.SelectAndConsume));
            this.triggers.Add(new ShortCutTrigger(Keys.Tab, TriggerState.Select));
            this.triggers.Add(new ShortCutTrigger(Keys.Control | Keys.Space, TriggerState.ShowAndConsume));
            this.triggers.Add(new ShortCutTrigger(Keys.Escape, TriggerState.HideAndConsume));
        }

        private void LBMaskedTextBox_BindingContextChanged(object sender, EventArgs e)
        {
            if ((!this.flgPos && (base.DataBindings.Count > 0)) && (base.DataBindings[0].BindingManagerBase != null))
            {
                base.DataBindings[0].BindingManagerBase.PositionChanged += new EventHandler(this.BS_PositionChanged);
                this.flgPos = true;
            }
        }

        private void LBMaskedTextBox_GotFocus(object sender, EventArgs e)
        {
            base.SelectionStart = this.iSelectStart;
        }

        private void LBMaskedTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            this.iSelectStart = base.SelectionStart;
            if (e.KeyCode == Keys.Enter)
            {
                bool foward = e.Modifiers != Keys.Shift;
                ControlFunc.SelectNextControlEx(this, foward);
            }
        }

        private void LBMaskedTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((this._intput_output != "-") && (e.KeyChar != '\b'))
            {
                this.CheckSelectNextCtl = true;
            }
        }

        private void LBMaskedTextBox_TextChanged(object sender, EventArgs e)
        {
            this.SetBackColor();
            base.DataBindings["Text"].WriteValue();
            if (this.CheckSelectNextCtl)
            {
                if ((this.Text != null) && (this.SF.GetByteLength(this.Text) == this.figure))
                {
                    ControlFunc.SelectNextControlEx(this, true);
                }
                this.CheckSelectNextCtl = false;
            }
        }

        private void List_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < this.list.Items.Count; i++)
            {
                if (this.list.GetItemRectangle(i).Contains(e.X, e.Y))
                {
                    this.list.SelectedIndex = i;
                    this.SelectCurrentItem();
                }
            }
            this.HideList();
        }

        protected override void OnEnter(EventArgs e)
        {
            if (!base.DesignMode)
            {
                this.iSelectStart = 0;
                base.Focus();
                base.SelectAll();
                base.OnEnter(e);
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            if ((!this.Focused && !this.popup.Focused) && !this.list.Focused)
            {
                this.HideList();
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

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (this.triggersEnabled)
            {
                switch (this.triggers.OnTextChanged(this.Text))
                {
                    case TriggerState.Show:
                        this.ShowList();
                        return;

                    case TriggerState.Hide:
                        this.HideList();
                        return;
                }
                this.UpdateList();
            }
        }

        private void Popup_Deactivate(object sender, EventArgs e)
        {
            if ((!this.Focused && !this.popup.Focused) && !this.list.Focused)
            {
                this.HideList();
            }
        }

        private void popup_Load(object sender, EventArgs e)
        {
            this.SetPopUpSize();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    this.flgAutoCompShow = true;
                    if (this.list.SelectedIndex > 0)
                    {
                        this.list.SelectedIndex--;
                    }
                    return true;

                case Keys.Down:
                    this.flgAutoCompShow = true;
                    if (this.list.SelectedIndex < (this.list.Items.Count - 1))
                    {
                        this.list.SelectedIndex++;
                    }
                    return true;
            }
            return this.DefaultCmdKey(ref msg, keyData);
        }

        private void SelectCurrentItem()
        {
            if (this.list.SelectedIndex != -1)
            {
                string str = this.list.SelectedItem.ToString();
                base.Focus();
                this.Text = str;
                this.HideList();
            }
        }

        private void SetAuEntryCollection()
        {
            this.items.Clear();
            List<string> list = new List<string>();
            for (int i = 0; i < this.autoCompleteCustomSource.Count; i++)
            {
                list.Clear();
                string name = this.autoCompleteCustomSource[i];
                string str2 = this.Mask.Replace(">", "");
                string str3 = str2;
                str3 = str3.Replace('A', base.PromptChar).Replace('9', base.PromptChar).Replace('C', base.PromptChar);
                StringBuilder builder = new StringBuilder();
                builder.Append(str3.ToCharArray());
                int num2 = 0;
                for (int j = 0; j < builder.Length; j++)
                {
                    if (builder[j] == base.PromptChar)
                    {
                        if (num2 >= name.Length)
                        {
                            break;
                        }
                        builder[j] = name[num2];
                        num2++;
                    }
                }
                name = builder.ToString();
                for (int k = str2.Length - 1; k > 0; k--)
                {
                    if (((str2[k] == '9') || (str2[k] == 'A')) || (str2[k] == 'C'))
                    {
                        builder[k] = base.PromptChar;
                        list.Add(builder.ToString());
                    }
                }
                string[] matchList = new string[list.Count];
                for (int m = 0; m < list.Count; m++)
                {
                    matchList[m] = list[m];
                }
                this.items.Add(new AutoCompleteEntry(name, matchList));
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
            else if ((this._required == "M") && ((!base.MaskFull && (this.Mask != "")) || ((this.Mask == "") && (this.Text.Length == 0))))
            {
                this.BackColor = DesignControls.MandatoryBackColor;
            }
            else
            {
                this.BackColor = DesignControls.NormalBackColor;
            }
        }

        private void SetPopUpSize()
        {
            int count = this.list.Items.Count;
            if (count > 8)
            {
                count = 8;
            }
            this.popup.Font = this.Font;
            this.popup.Height = (count * this.list.ItemHeight) + 4;
            this.popup.Width = base.Width + SystemInformation.VerticalScrollBarWidth;
        }

        private void ShowList()
        {
            if (!this.popup.Visible)
            {
                this.list.SelectedIndex = -1;
                this.UpdateList();
                Point point = base.PointToScreen(new Point(0, 0));
                point.Y += base.Height;
                this.popup.Location = point;
                if (this.list.Items.Count > 0)
                {
                    this.popup.Show();
                    if (this.hook == null)
                    {
                        this.hook = new WinHook(this);
                        this.hook.AssignHandle(base.FindForm().Handle);
                    }
                    base.Focus();
                }
            }
            else
            {
                this.UpdateList();
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
                if (this.Text != "")
                {
                    base.SelectAll();
                }
            }
        }

        private void UpdateList()
        {
            object selectedItem = this.list.SelectedItem;
            this.list.Items.Clear();
            this.list.Items.AddRange(this.FilterList(this.items).ToObjectArray());
            if ((selectedItem != null) && this.list.Items.Contains(selectedItem))
            {
                bool flgAutoCompShow = this.flgAutoCompShow;
                this.flgAutoCompShow = true;
                this.list.SelectedItem = selectedItem;
                this.flgAutoCompShow = flgAutoCompShow;
            }
            if (this.list.Items.Count == 0)
            {
                this.HideList();
            }
            else
            {
                this.SetPopUpSize();
                if ((this.list.Items.Count > 0) && (this.list.SelectedIndex == -1))
                {
                    bool flag2 = this.flgAutoCompShow;
                    this.flgAutoCompShow = true;
                    this.list.SelectedIndex = 0;
                    this.flgAutoCompShow = flag2;
                }
            }
        }

        [Description("Attribute"), Browsable(true), Localizable(true), Category("Service")]
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

        [Browsable(true), ReadOnly(false), Localizable(true), Category("Service"), Description("AutoComplete")]
        public bool AutoComplete
        {
            get
            {
                return this.autoComplete;
            }
            set
            {
                this.autoComplete = value;
            }
        }

        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get
            {
                return this.autoCompleteCustomSource;
            }
            set
            {
                this.autoCompleteCustomSource = value;
                if (value != null)
                {
                    this.SetAuEntryCollection();
                    this.autoCompleteCustomSource.CollectionChanged += new CollectionChangeEventHandler(this.autoCompleteCustomSource_CollectionChanged);
                }
            }
        }

        [Browsable(true), Localizable(true), Category("Service"), Description("Specify whether to check attribute or not.")]
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

        [Browsable(true), Localizable(true), Category("Service"), Description("Selections for radio button.")]
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
                this.SetBackColor();
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

        [Browsable(true), Description("Specify whether an input or an output (not editable)"), Localizable(true), Category("Service")]
        public string input_output
        {
            get
            {
                return this._intput_output;
            }
            set
            {
                this._intput_output = value;
                base.ReadOnly = value != "+";
                base.TabStop = !(value != "+");
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

        [TypeConverter(typeof(MaskFormatConverter))]
        public string Mask
        {
            get
            {
                return base.Mask;
            }
            set
            {
                base.Mask = value;
            }
        }

        [Localizable(true), Browsable(true), Description("Item Name"), Category("Service")]
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

        [Browsable(true)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                this.triggersEnabled = false;
                base.Text = value;
                this.triggersEnabled = true;
            }
        }

        private class WinHook : NativeWindow
        {
            private LBMaskedTextBox tb;
            private const int WM_LBUTTONDBLCLK = 0x203;
            private const int WM_LBUTTONDOWN = 0x201;
            private const int WM_LBUTTONUP = 0x202;
            private const int WM_MBUTTONDBLCLK = 0x209;
            private const int WM_MBUTTONDOWN = 0x207;
            private const int WM_MBUTTONUP = 520;
            private const int WM_MOVE = 3;
            private const int WM_NCLBUTTONDBLCLK = 0xa3;
            private const int WM_NCLBUTTONDOWN = 0xa1;
            private const int WM_NCLBUTTONUP = 0xa2;
            private const int WM_NCMBUTTONDBLCLK = 0xa9;
            private const int WM_NCMBUTTONDOWN = 0xa7;
            private const int WM_NCMBUTTONUP = 0xa8;
            private const int WM_NCRBUTTONDBLCLK = 0xa6;
            private const int WM_NCRBUTTONDOWN = 0xa4;
            private const int WM_NCRBUTTONUP = 0xa5;
            private const int WM_PARENTNOTIFY = 0x210;
            private const int WM_RBUTTONDBLCLK = 0x206;
            private const int WM_RBUTTONDOWN = 0x204;
            private const int WM_RBUTTONUP = 0x205;
            private const int WM_SIZE = 5;

            public WinHook(LBMaskedTextBox tbox)
            {
                this.tb = tbox;
            }

            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case 0x201:
                    case 0x203:
                    case 0x204:
                    case 0x206:
                    case 0x207:
                    case 0x209:
                    case 0xa7:
                    case 0xa1:
                    case 0xa4:
                    {
                        Point pt = this.tb.FindForm().PointToScreen(new Point((int) m.LParam));
                        Point location = this.tb.PointToScreen(new Point(0, 0));
                        Rectangle rectangle = new Rectangle(location, this.tb.Size);
                        if (!rectangle.Contains(pt))
                        {
                            this.tb.HideList();
                        }
                        break;
                    }
                    case 0x210:
                        switch (((int) m.WParam))
                        {
                            case 0x201:
                            case 0x203:
                            case 0x204:
                            case 0x206:
                            case 0x207:
                            case 0x209:
                            case 0xa7:
                            case 0xa1:
                            case 0xa4:
                            {
                                Point point3 = this.tb.FindForm().PointToScreen(new Point((int) m.LParam));
                                Point point4 = this.tb.PointToScreen(new Point(0, 0));
                                Rectangle rectangle2 = new Rectangle(point4, this.tb.Size);
                                if (!rectangle2.Contains(point3))
                                {
                                    this.tb.HideList();
                                }
                                goto Label_01C8;
                            }
                        }
                        break;

                    case 3:
                    case 5:
                        this.tb.HideList();
                        break;
                }
            Label_01C8:
                base.WndProc(ref m);
            }
        }
    }
}

