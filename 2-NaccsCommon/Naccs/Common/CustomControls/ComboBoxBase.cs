namespace Naccs.Common.CustomControls
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    [DisplayName("ComboBox"), ToolboxBitmap(typeof(ComboBox))]
    public class ComboBoxBase : ComboBox, IItemAttributesEx, IItemAttributes
    {
        private string _attribute = "";
        private string _check_date = "";
        private string _check_full = "";
        private string _check_time = "";
        private string _choice_keyvalue;
        private int _figure = 1;
        private string _form = "";
        private string _id = "";
        private string _input_output = " ";
        private string _name = "";
        private int _order = -1;
        private string _required = "";
        private string checkAttribute = "";
        private IContainer components;
        protected ArrayList dropDownSource = new ArrayList();
        private bool flgPos;
        private JobErrInfo jobErr;
        private string rep_ID = "";

        public ComboBoxBase()
        {
            this.SetDataSource(this.dropDownSource, "Display", "Data");
            base.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            base.DrawItem += new DrawItemEventHandler(this.ComboBoxBase_DrawItem);
            base.BindingContextChanged += new EventHandler(this.LBComboBox_BindingContextChanged);
        }

        private void BS_PositionChanged(object sender, EventArgs e)
        {
            if (this.JobErr != null)
            {
                this.SetBackColor();
            }
        }

        private void ComboBoxBase_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index >= 0)
            {
                string data;
                Color highlightText;
                if ((e.State & DrawItemState.ComboBoxEdit) == DrawItemState.ComboBoxEdit)
                {
                    data = ((ComboExItem) this.dropDownSource[e.Index]).Data;
                }
                else
                {
                    ComboExItem item = (ComboExItem) this.dropDownSource[e.Index];
                    if ((item.Display != null) && (item.Display.Length > 0))
                    {
                        data = item.Data + ":" + item.Display;
                    }
                    else
                    {
                        data = item.Data;
                    }
                }
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    highlightText = SystemColors.HighlightText;
                }
                else
                {
                    highlightText = e.ForeColor;
                }
                TextRenderer.DrawText(e.Graphics, data, this.Font, new Point(e.Bounds.X, e.Bounds.Y), highlightText);
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

        private void LBComboBox_BindingContextChanged(object sender, EventArgs e)
        {
            if ((!this.flgPos && (base.DataBindings.Count > 0)) && (base.DataBindings[0].BindingManagerBase != null))
            {
                base.DataBindings[0].BindingManagerBase.PositionChanged += new EventHandler(this.BS_PositionChanged);
                this.flgPos = true;
            }
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWndControl, int msgId, IntPtr wParam, IntPtr lParam);
        public virtual void SetBackColor()
        {
        }

        protected void SetDataSource(object DSource, string DMember, string VMember)
        {
            base.DataSource = DSource;
            base.DisplayMember = DMember;
            base.ValueMember = VMember;
        }

        protected void setDropDownWidth()
        {
            Graphics graphics = base.CreateGraphics();
            float num = 0f;
            foreach (ComboExItem item in this.dropDownSource)
            {
                string text = item.Data + ":" + item.Display;
                num = Math.Max(num, graphics.MeasureString(text, this.Font).Width);
            }
            int num2 = (int) decimal.Round((decimal) num, 0);
            num2 += 30;
            base.DropDownWidth = Math.Max(base.Width, num2);
            graphics.Dispose();
        }

        protected override void WndProc(ref Message m)
        {
            if ((m.Msg == 0x20a) && !base.DroppedDown)
            {
                SendMessage(base.Parent.Handle, m.Msg, m.WParam, m.LParam);
            }
            else
            {
                base.WndProc(ref m);
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
            }
        }

        [Description("Specify whether to check attribute or not."), Browsable(true), Localizable(true), Category("Service")]
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

        [Category("Service"), Localizable(true), Description("Specify whether to check the validity of date or not"), Browsable(true)]
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

        [Description("Specify whether to check full digit input or not"), Localizable(true), Browsable(true), Category("Service")]
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

        [Browsable(true), Localizable(true), Category("Service"), Description("Specify whether it checks date validity ")]
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

        [Category("Service"), Description("Input the data of message or the contents of pull-down display delimited by thousand separator. \nFormat：\n\t[Code],[Displayed contents]"), Localizable(true), Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public object DataSource
        {
            get
            {
                return this.dropDownSource;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public object DisplayMember
        {
            get
            {
                return this.dropDownSource;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object DrawMode
        {
            get
            {
                return this.dropDownSource;
            }
        }

        [Description("Digit number"), Category("Service"), Browsable(true), Localizable(true)]
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

        public override System.Drawing.Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                if (this.dropDownSource.Count > 0)
                {
                    this.setDropDownWidth();
                }
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

        [Localizable(true), Browsable(true), Description("Item ID"), Category("Service")]
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

        [Description("Specify whether an input or an output (not editable)"), Category("Service"), Browsable(true), Localizable(true)]
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

        [Description("Order of message item"), Browsable(true), Localizable(true), Category("Service")]
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

        [Localizable(true), Category("Service"), Description("Specify whether the required input or not"), Browsable(true)]
        public string required
        {
            get
            {
                return this._required;
            }
            set
            {
                this._required = value;
                if ((this._required != null) && this._required.Equals("M"))
                {
                    this.BackColor = DesignControls.MandatoryBackColor;
                }
                else
                {
                    this.BackColor = DesignControls.NormalBackColor;
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object ValueMember
        {
            get
            {
                return this.dropDownSource;
            }
        }
    }
}

