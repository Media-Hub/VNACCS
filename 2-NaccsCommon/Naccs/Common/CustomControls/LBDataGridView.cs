namespace Naccs.Common.CustomControls
{
    using Naccs.Common.Function;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(DataGridView)), DisplayName("Grid")]
    public class LBDataGridView : DataGridView, IRepetition
    {
        private int _repetition_max = 1;
        private string aColumnID;
        private IContainer components;
        private EndEditCellData EndEditData = new EndEditCellData();
        private JobErrInfo jobErr;
        private string repetitionId = "";
        private StrFunc SF = StrFunc.CreateInstance();

        public event EventHandler OnColumnEnter;

        public LBDataGridView()
        {
            base.CellBeginEdit += new DataGridViewCellCancelEventHandler(this.LBDataGridView_CellBeginEdit);
            base.CellEndEdit += new DataGridViewCellEventHandler(this.LBDataGridView_CellEndEdit);
            base.CellEnter += new DataGridViewCellEventHandler(this.LBDataGridView_CellEnter);
            base.Leave += new EventHandler(this.LBDataGridView_Leave);
            base.EditMode = DataGridViewEditMode.EditOnEnter;
            base.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.LBDataGridView_EditingControlShowing);
            base.Paint += new PaintEventHandler(this.LBDataGridView_Paint);
            base.VisibleChanged += new EventHandler(this.LBDataGridView_VisibleChanged);
            this.DoubleBuffered = true;
        }

        public void ClearStyle()
        {
            for (int i = 0; i < base.Columns.Count; i++)
            {
                for (int j = 0; j < base.Rows.Count; j++)
                {
                    base[i, j].Style.BackColor = base[i, j].OwningColumn.DefaultCellStyle.BackColor;
                }
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

        private int GetColumnIdx(string stID)
        {
            for (int i = 0; i < base.Columns.Count; i++)
            {
                LBTextBoxColumn column = (LBTextBoxColumn) base.Columns[i];
                if (column.id == stID)
                {
                    return i;
                }
            }
            return -1;
        }

        protected void LBDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridViewCell cell = base[e.ColumnIndex, e.RowIndex];
            cell.Style.BackColor = DesignControls.NormalBackColor;
        }

        protected void LBDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            LBTextBoxCell cell = (LBTextBoxCell) base[e.ColumnIndex, e.RowIndex];
            DataGridViewColumn owningColumn = cell.OwningColumn;
            LBTextBoxColumn column2 = (LBTextBoxColumn) owningColumn;
            if (this.EndEditData.ID == cell.id)
            {
                if (cell.Value is DBNull)
                {
                    cell.Value = this.EndEditData.Data;
                }
                else if (this.EndEditData.Data != ((string) cell.Value))
                {
                    cell.Value = this.EndEditData.Data;
                }
            }
            this.EndEditData.ID = "";
            this.EndEditData.Data = "";
            if (this.jobErr != null)
            {
                string stData = "";
                if (!(cell.Value is DBNull))
                {
                    stData = (string) cell.Value;
                }
                if (this.jobErr.CheckErrInfo(column2.id, e.RowIndex + 1, stData))
                {
                    cell.Style.BackColor = DesignControls.ErrorColor;
                    return;
                }
            }
            if (column2.required == "M")
            {
                if (cell.Value is DBNull)
                {
                    cell.Style.BackColor = owningColumn.DefaultCellStyle.BackColor;
                }
                else if (string.IsNullOrEmpty((string) cell.Value))
                {
                    cell.Style.BackColor = owningColumn.DefaultCellStyle.BackColor;
                }
                else
                {
                    cell.Style.BackColor = DesignControls.NormalBackColor;
                }
            }
            else
            {
                cell.Style.BackColor = DesignControls.NormalBackColor;
            }
        }

        private void LBDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = base[e.ColumnIndex, e.RowIndex];
            LBTextBoxColumn owningColumn = (LBTextBoxColumn) cell.OwningColumn;
            if ((!base.CurrentCell.Displayed || (base.CurrentCell.ColumnIndex == (base.Columns.Count - 1))) && (base.FirstDisplayedScrollingColumnIndex >= 0))
            {
                base.FirstDisplayedScrollingColumnIndex = base.CurrentCell.ColumnIndex;
            }
            this.aColumnID = owningColumn.id;
            if (this.OnColumnEnter != null)
            {
                this.OnColumnEnter(this, e);
            }
        }

        private void LBDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is LBTextBoxEditingControl)
            {
                LBTextBoxEditingControl control = (LBTextBoxEditingControl) e.Control;
                control.TextChanged -= new EventHandler(this.LBDataGridViewTextBox_TextChanged);
                control.TextChanged += new EventHandler(this.LBDataGridViewTextBox_TextChanged);
            }
        }

        private void LBDataGridView_Leave(object sender, EventArgs e)
        {
            this.aColumnID = "";
        }

        private void LBDataGridView_Paint(object sender, PaintEventArgs e)
        {
            if (this.JobErr != null)
            {
                for (int i = 0; i < 6; i++)
                {
                    JobErrInfo.StructErr errInfo = this.JobErr.GetErrInfo(i);
                    if (errInfo.ErrPage > 0)
                    {
                        for (int j = 0; j < base.Columns.Count; j++)
                        {
                            LBTextBoxColumn column = (LBTextBoxColumn) base.Columns[j];
                            if (errInfo.ErrID == column.id)
                            {
                                DataGridViewCell cell = base[j, errInfo.ErrPage - 1];
                                string str = "";
                                if (!(cell.Value is DBNull))
                                {
                                    str = (string) cell.Value;
                                }
                                if (errInfo.ErrData == str)
                                {
                                    cell.Style.BackColor = DesignControls.ErrorColor;
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void LBDataGridView_VisibleChanged(object sender, EventArgs e)
        {
            if (base.Visible)
            {
                for (int i = 0; i < base.Columns.Count; i++)
                {
                    for (int j = 0; j < base.Rows.Count; j++)
                    {
                        LBTextBoxCell cell = (LBTextBoxCell) base[i, j];
                        DataGridViewColumn owningColumn = cell.OwningColumn;
                        LBTextBoxColumn column2 = (LBTextBoxColumn) owningColumn;
                        if (column2.input_output == "-")
                        {
                            cell.Style.BackColor = DesignControls.ReadOnlyBackColor;
                        }
                        else if (column2.required == "M")
                        {
                            if (cell.Value is DBNull)
                            {
                                cell.Style.BackColor = owningColumn.DefaultCellStyle.BackColor;
                            }
                            else if (string.IsNullOrEmpty((string) cell.Value))
                            {
                                cell.Style.BackColor = owningColumn.DefaultCellStyle.BackColor;
                            }
                            else
                            {
                                cell.Style.BackColor = DesignControls.NormalBackColor;
                            }
                        }
                        else
                        {
                            cell.Style.BackColor = DesignControls.NormalBackColor;
                        }
                    }
                }
            }
        }

        private void LBDataGridViewTextBox_TextChanged(object sender, EventArgs e)
        {
            LBTextBoxEditingControl control = (LBTextBoxEditingControl) sender;
            if (control.Modified && control.CheckSelectNextCtl)
            {
                int byteLength = this.SF.GetByteLength(control.Text);
                int maxByteLength = control.MaxByteLength;
                if ("W".Equals(control.attribute))
                {
                    byteLength = this.SF.GetStringLength(control.Text);
                    maxByteLength = control.Digit;
                }
                if (byteLength >= maxByteLength)
                {
                    this.EndEditData.ID = control.id;
                    this.EndEditData.Data = control.Text;
                    if (!this.TabToNextCell())
                    {
                        ControlFunc.SelectNextControlEx(this, true);
                    }
                }
                control.CheckSelectNextCtl = false;
            }
        }

        private int NextColIndex(int iStart)
        {
            for (int i = iStart; i < base.Columns.Count; i++)
            {
                LBTextBoxColumn column = (LBTextBoxColumn) base.Columns[i];
                if ((column.input_output == "+") && column.Visible)
                {
                    return i;
                }
            }
            for (int j = 0; j < iStart; j++)
            {
                LBTextBoxColumn column2 = (LBTextBoxColumn) base.Columns[j];
                if ((column2.input_output == "+") && column2.Visible)
                {
                    return j;
                }
            }
            return -1;
        }

        private int PreviousColIndex(int iStart)
        {
            for (int i = iStart; i > -1; i--)
            {
                LBTextBoxColumn column = (LBTextBoxColumn) base.Columns[i];
                if ((column.input_output == "+") && column.Visible)
                {
                    return i;
                }
            }
            for (int j = base.Columns.Count - 1; j > (iStart + 1); j--)
            {
                LBTextBoxColumn column2 = (LBTextBoxColumn) base.Columns[j];
                if ((column2.input_output == "+") && column2.Visible)
                {
                    return j;
                }
            }
            return -1;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys keys = keyData;
            if (keys != Keys.Tab)
            {
                if (keys != (Keys.Shift | Keys.Tab))
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
            }
            else
            {
                if (!this.TabToNextCell())
                {
                    ControlFunc.SelectNextControlEx(this, true);
                }
                return true;
            }
            if (!this.TabToPreviousCell())
            {
                ControlFunc.SelectNextControlEx(this, false);
            }
            return true;
        }

        public void SetFocusCell(string stID, int iPage)
        {
            int columnIdx = this.GetColumnIdx(stID);
            if (columnIdx >= 0)
            {
                base.CurrentCell = null;
                base.CurrentCell = base[columnIdx, iPage];
            }
        }

        public void SetLinkField(string stID)
        {
            for (int i = 0; i < base.Columns.Count; i++)
            {
                LBTextBoxColumn column = (LBTextBoxColumn) base.Columns[i];
                if (column.id == stID)
                {
                    column.DefaultCellStyle.ForeColor = DesignControls.LinkForeColor;
                }
            }
        }

        private bool TabToNextCell()
        {
            int num = -1;
            num = this.NextColIndex(base.CurrentCell.ColumnIndex + 1);
            if (num == -1)
            {
                return false;
            }
            if (num <= base.CurrentCell.ColumnIndex)
            {
                if (base.CurrentCell.RowIndex == (base.Rows.Count - 1))
                {
                    return false;
                }
                base.CurrentCell = base[num, base.CurrentCell.RowIndex + 1];
                return true;
            }
            base.CurrentCell = base[num, base.CurrentCell.RowIndex];
            return true;
        }

        private bool TabToPreviousCell()
        {
            int num = -1;
            num = this.PreviousColIndex(base.CurrentCell.ColumnIndex - 1);
            if (num == -1)
            {
                return false;
            }
            if (num >= base.CurrentCell.ColumnIndex)
            {
                if (base.CurrentCell.RowIndex == 0)
                {
                    return false;
                }
                base.CurrentCell = base[num, base.CurrentCell.RowIndex - 1];
                return true;
            }
            base.CurrentCell = base[num, base.CurrentCell.RowIndex];
            return true;
        }

        public string AColumnID
        {
            get
            {
                return this.aColumnID;
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
                for (int i = 0; i < base.Columns.Count; i++)
                {
                    base.Columns[i].ContextMenuStrip = value;
                }
                base.ContextMenuStrip = value;
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

        [Localizable(true), Category("Service"), Description("Iteration ID"), Browsable(true)]
        public string repetition_id
        {
            get
            {
                return this.repetitionId;
            }
            set
            {
                this.repetitionId = value;
            }
        }

        [Category("Service"), Localizable(true), Description("Max Number of Iteration")]
        public int repetition_max
        {
            get
            {
                return this._repetition_max;
            }
            set
            {
                this._repetition_max = value;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct EndEditCellData
        {
            public string ID;
            public string Data;
        }
    }
}

