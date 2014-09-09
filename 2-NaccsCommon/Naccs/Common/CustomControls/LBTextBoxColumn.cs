namespace Naccs.Common.CustomControls
{
    using Naccs.Common.Properties;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public class LBTextBoxColumn : DataGridViewColumn, IItemAttributes
    {
        private string _attribute;
        private string _check_date;
        private string _check_full;
        private string _check_time;
        private string _choice_keyvalue;
        private string _form;
        private string _id;
        private string _input_output;
        private string _name;
        private int _order;
        private string _required;
        private string checkAttribute;
        private int fFigure;

        public LBTextBoxColumn() : base(new LBTextBoxCell())
        {
            this._id = "";
            this._name = "";
            this._input_output = " ";
            this._required = "";
            this._attribute = "";
            this._form = "";
            this._order = -1;
            this.checkAttribute = "";
            this._choice_keyvalue = "";
            this._check_full = "";
            this._check_date = "";
            this._check_time = "";
        }

        public override object Clone()
        {
            LBTextBoxColumn column = base.Clone() as LBTextBoxColumn;
            column.id = this.id;
            column.name = this.name;
            column.input_output = this.input_output;
            column.required = this.required;
            column.attribute = this.attribute;
            column.figure = this.figure;
            column.form = this.form;
            column.order = this.order;
            column.check_attribute = this.check_attribute;
            column.choice_keyvalue = this.choice_keyvalue;
            column.check_full = this.check_full;
            column.check_date = this.check_date;
            column.check_time = this.check_time;
            return column;
        }

        [Category("Service"), Description("Attribute"), Localizable(true), Browsable(true)]
        public string attribute
        {
            get
            {
                return this._attribute;
            }
            set
            {
                this._attribute = value;
                LBTextBoxCell cellTemplate = (LBTextBoxCell) this.CellTemplate;
                cellTemplate.attribute = value;
            }
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if ((value != null) && !value.GetType().IsAssignableFrom(typeof(LBTextBoxCell)))
                {
                    throw new InvalidCastException(Resources.ResourceManager.GetString("COM10"));
                }
                base.CellTemplate = value;
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

        [Category("Service"), Localizable(true), Browsable(true), Description("Specify whether to check full digit input or not")]
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

        [Description("Specify whether it checks date validity "), Browsable(true), Category("Service"), Localizable(true)]
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
                LBTextBoxCell cellTemplate = (LBTextBoxCell) this.CellTemplate;
                cellTemplate.MaxByteLength = value;
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

        [Category("Service"), Browsable(true), Description("Item ID"), Localizable(true)]
        public string id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
                LBTextBoxCell cellTemplate = (LBTextBoxCell) this.CellTemplate;
                cellTemplate.id = value;
            }
        }

        [Category("Service"), Description("Specify whether an input or an output (not editable)"), Localizable(true), Browsable(true)]
        public string input_output
        {
            get
            {
                return this._input_output;
            }
            set
            {
                this._input_output = value;
                if ((this._input_output != null) && this._input_output.Equals("-"))
                {
                    this.DefaultCellStyle.BackColor = DesignControls.ReadOnlyBackColor;
                    this.ReadOnly = true;
                }
                else
                {
                    this.DefaultCellStyle.BackColor = DesignControls.NormalBackColor;
                    this.ReadOnly = false;
                }
            }
        }

        [Category("Service"), Localizable(true), Description("Item Name"), Browsable(true)]
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

        [Category("Service"), Localizable(true), Browsable(true), Description("Order of message item")]
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

        [Browsable(true), Localizable(true), Category("Service"), Description("Specify whether the required input or not")]
        public string required
        {
            get
            {
                return this._required;
            }
            set
            {
                this._required = value;
                if (value == "M")
                {
                    base.ToolTipText = DesignControls.stMsgRequired;
                    this.DefaultCellStyle.BackColor = DesignControls.MandatoryBackColor;
                    this.CellTemplate.ToolTipText = DesignControls.stMsgRequired;
                }
                else
                {
                    if ((this._input_output != null) && this._input_output.Equals("-"))
                    {
                        this.DefaultCellStyle.BackColor = DesignControls.ReadOnlyBackColor;
                    }
                    else
                    {
                        this.DefaultCellStyle.BackColor = DesignControls.NormalBackColor;
                    }
                    base.ToolTipText = null;
                    this.CellTemplate.ToolTipText = null;
                }
            }
        }
    }
}

