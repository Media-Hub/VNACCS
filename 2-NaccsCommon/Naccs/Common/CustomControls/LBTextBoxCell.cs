namespace Naccs.Common.CustomControls
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public class LBTextBoxCell : DataGridViewTextBoxCell
    {
        private string _attribute = "";
        private string _id = "";
        private int fMaxByteLength;

        public override object Clone()
        {
            LBTextBoxCell cell = base.Clone() as LBTextBoxCell;
            cell.MaxByteLength = this.MaxByteLength;
            cell.attribute = this.attribute;
            cell.id = this.id;
            return cell;
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            LBTextBoxEditingControl editingControl = base.DataGridView.EditingControl as LBTextBoxEditingControl;
            editingControl.MaxByteLength = this.MaxByteLength;
            editingControl.attribute = this.attribute;
            if (!(base.Value is DBNull))
            {
                editingControl.Text = (string) base.Value;
            }
            editingControl.ContextMenuStrip = base.DataGridView.ContextMenuStrip;
            editingControl.id = this.id;
        }

        [Localizable(true), Category("Service"), Description("Attribute"), Browsable(true)]
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

        public override System.Type EditType
        {
            get
            {
                return typeof(LBTextBoxEditingControl);
            }
        }

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

        [Description("Digit Number"), Localizable(true), Browsable(true), Category("Service")]
        public int MaxByteLength
        {
            get
            {
                return this.fMaxByteLength;
            }
            set
            {
                this.fMaxByteLength = value;
            }
        }
    }
}

