using System;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid.Style;
using DevComponents.Editors;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// GridDoubleInputEditControl
    ///</summary>
    [ToolboxItem(false)]
    public class GridDoubleInputEditControl : DoubleInput, IGridCellEditControl, IGridCellEditorFocus
    {
        #region Private variables

        private GridCell _Cell;
        private EditorPanel _EditorPanel;
        private Bitmap _EditorCellBitmap;

        private bool _ValueChanged;
        private bool _SuspendUpdate;

        private StretchBehavior _StretchBehavior = StretchBehavior.None;

        #endregion

        ///<summary>
        /// GridIntegerInputEditControl
        ///</summary>
        public GridDoubleInputEditControl()
        {
            InitInputControl(false);
        }

        #region InitInputControl

        protected void InitInputControl(bool isIntegral)
        {
            ShowUpDown = true;

            if (isIntegral == true)
                DisplayFormat = "G";
        }

        #endregion

        #region OnValueChanged

        protected override void OnValueChanged(EventArgs e)
        {
            if (_SuspendUpdate == false)
                _Cell.EditorValueChanged(this);

            base.OnValueChanged(e);
        }

        #endregion

        #region GetValue

        ///<summary>
        /// GetValue
        ///</summary>
        ///<param name="value"></param>
        ///<returns></returns>
        public virtual double GetValue(object value)
        {
            GridPanel panel = _Cell.GridPanel;

            if (value == null ||
                (panel.NullValue == NullValue.DBNull && value == DBNull.Value))
            {
                return (0);
            }

            if (value is SqlSingle)
            {
                SqlSingle sdt = (SqlSingle)value;

                if (sdt.IsNull == true)
                    return (0);

                return (sdt.Value);
            }

            if (value is SqlDouble)
            {
                SqlDouble sdt = (SqlDouble)value;

                if (sdt.IsNull == true)
                    return (0);

                return (sdt.Value);
            }

            if (value is SqlDecimal)
            {
                SqlDecimal sdt = (SqlDecimal)value;

                if (sdt.IsNull == true)
                    return (0);

                return (Convert.ToDouble(sdt.Value));
            }

            if (value is SqlInt64)
            {
                SqlDecimal sdt = (SqlInt64)value;

                if (sdt.IsNull == true)
                    return (0);

                return (Convert.ToDouble(sdt.Value));
            }

            if (_Cell.IsValueExpression == true)
                value = _Cell.GetExpValue((string)value);

            return (Convert.ToDouble(value));
        }

        #endregion

        #region IGridCellEditControl Members

        #region Public properties

        #region CanInterrupt

        public bool CanInterrupt
        {
            get { return (true); }
        }

        #endregion

        #region CellEditMode

        public CellEditMode CellEditMode
        {
            get { return (CellEditMode.Modal); }
        }

        #endregion

        #region EditorCell

        public GridCell EditorCell
        {
            get { return (_Cell); }
            set { _Cell = value; }
        }

        #endregion

        #region EditorCellBitmap

        ///<summary>
        /// EditorCellBitmap
        ///</summary>
        public Bitmap EditorCellBitmap
        {
            get { return (_EditorCellBitmap); }

            set
            {
                if (_EditorCellBitmap != null)
                    _EditorCellBitmap.Dispose();

                _EditorCellBitmap = value;
            }
        }

        #endregion

        #region EditorFormattedValue

        ///<summary>
        /// EditorFormattedValue
        ///</summary>
        public virtual string EditorFormattedValue
        {
            get
            {
                if (_Cell != null && _Cell.IsValueNull == true)
                    return (_Cell.NullString);

                return (Text);
            }
        }

        #endregion

        #region EditorPanel

        public EditorPanel EditorPanel
        {
            get { return (_EditorPanel); }
            set { _EditorPanel = value; }
        }

        #endregion

        #region EditorValue

        public virtual object EditorValue
        {
            get { return (Value); }
            set { Value = GetValue(value); }
        }

        #endregion

        #region EditorValueChanged

        public virtual bool EditorValueChanged
        {
            get { return (_ValueChanged); }

            set
            {
                if (_ValueChanged != value)
                {
                    _ValueChanged = value;

                    if (value == true)
                        _Cell.SetEditorDirty(this);
                }
            }
        }

        #endregion

        #region EditorValueType

        ///<summary>
        /// EditorValueType
        ///</summary>
        public virtual Type EditorValueType
        {
            get { return (typeof(double)); }
        }

        #endregion

        #region StretchBehavior

        public virtual StretchBehavior StretchBehavior
        {
            get { return (_StretchBehavior); }
            set { _StretchBehavior = value; }
        }

        #endregion

        #region SuspendUpdate

        public bool SuspendUpdate
        {
            get { return (_SuspendUpdate); }
            set { _SuspendUpdate = value; }
        }

        #endregion

        #region ValueChangeBehavior

        public virtual ValueChangeBehavior ValueChangeBehavior
        {
            get { return (ValueChangeBehavior.InvalidateRender); }
        }

        #endregion

        #endregion

        #region InitializeContext

        ///<summary>
        /// InitializeContext
        ///</summary>
        ///<param name="cell"></param>
        ///<param name="style"></param>
        public virtual void InitializeContext(GridCell cell, CellVisualStyle style)
        {
            _Cell = cell;

            if (style != null)
            {
                Enabled = (_Cell.ReadOnly == false);
                Font = style.Font;
                ForeColor = style.TextColor;
            }

            Value = GetValue(cell.Value);

            _ValueChanged = false;
        }

        #endregion

        #region GetProposedSize

        ///<summary>
        /// GetProposedSize
        ///</summary>
        ///<param name="g"></param>
        ///<param name="cell"></param>
        ///<param name="style"></param>
        ///<param name="constraintSize"></param>
        ///<returns></returns>
        public virtual Size GetProposedSize(Graphics g,
            GridCell cell, CellVisualStyle style, Size constraintSize)
        {
            Size size = GetPreferredSize(constraintSize);

            return (size);
        }

        #endregion

        #region Edit support

        #region BeginEdit

        public virtual bool BeginEdit(bool selectAll)
        {
            if (FreeTextEntryMode == true)
            {
                TextBox box = GetFreeTextBox() as TextBox;

                if (box != null)
                {
                    if (selectAll == true)
                        box.SelectAll();
                }
            }

            return (false);
        }

        #endregion

        #region EndEdit

        public virtual bool EndEdit()
        {
            CloseDropDown();

            return (false);
        }

        #endregion

        #region CancelEdit

        public virtual bool CancelEdit()
        {
            CloseDropDown();

            return (false);
        }

        #endregion

        #endregion

        #region CellRender

        public virtual void CellRender(Graphics g)
        {
            _Cell.CellRender(this, g);
        }

        #endregion

        #region Keyboard support

        #region CellKeyDown

        ///<summary>
        /// CellKeyDown
        ///</summary>
        public virtual void CellKeyDown(KeyEventArgs e)
        {
        }

        #endregion

        #region WantsInputKey

        public virtual bool WantsInputKey(Keys key, bool gridWantsKey)
        {
            if ((key & Keys.KeyCode) == Keys.Tab)
            {
                ApplyFreeTextValue();

                return (false);
            }

            if (IsCalculatorDisplayed == true)
                return (true);

            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.Up:
                case Keys.Down:
                    return (true);

                case Keys.Enter:
                    ApplyFreeTextValue();
                    return (gridWantsKey == false);

                default:
                    return (gridWantsKey == false);
            }
        }

        #endregion

        #endregion

        #region Mouse support

        #region OnCellMouseMove

        ///<summary>
        /// OnCellMouseMove
        ///</summary>
        ///<param name="e"></param>
        public virtual void OnCellMouseMove(MouseEventArgs e)
        {
        }

        #endregion

        #region OnCellMouseEnter

        ///<summary>
        /// OnCellMouseEnter
        ///</summary>
        ///<param name="e"></param>
        public virtual void OnCellMouseEnter(EventArgs e)
        {
        }

        #endregion

        #region OnCellMouseLeave

        ///<summary>
        /// OnCellMouseLeave
        ///</summary>
        ///<param name="e"></param>
        public virtual void OnCellMouseLeave(EventArgs e)
        {
        }

        #endregion

        #region OnCellMouseUp

        ///<summary>
        /// OnCellMouseUp
        ///</summary>
        ///<param name="e"></param>
        public virtual void OnCellMouseUp(MouseEventArgs e)
        {
        }

        #endregion

        #region OnCellMouseDown

        ///<summary>
        /// OnCellMouseDown
        ///</summary>
        ///<param name="e"></param>
        public virtual void OnCellMouseDown(MouseEventArgs e)
        {
        }

        #endregion

        #endregion

        #endregion

        #region IGridCellEditorFocus members

        #region FocusEditor

        /// <summary>
        /// Gives focus to the editor
        /// </summary>
        public void FocusEditor()
        {
            if (IsEditorFocused == false)
            {
                if (FreeTextEntryMode == true)
                {
                    TextBox box = GetFreeTextBox() as TextBox;

                    if (box != null)
                        box.Focus();
                }
                else
                {
                    Focus();
                }
            }
        }

        #endregion

        #region IsEditorFocused

        /// <summary>
        /// Gets whether editor has the focus
        /// </summary>
        public bool IsEditorFocused
        {
            get
            {
                if (FreeTextEntryMode == true)
                {
                    TextBox box = GetFreeTextBox() as TextBox;

                    if (box != null)
                        return (box.Focused);
                }

                return (Focused);
            }
        }

        #endregion

        #endregion
    }

    ///<summary>
    /// GridDoubleIntInputEditControl
    ///</summary>
    [ToolboxItem(false)]
    public class GridDoubleIntInputEditControl : GridDoubleInputEditControl
    {
        ///<summary>
        /// GridDoubleIntInputEditControl
        ///</summary>
        public GridDoubleIntInputEditControl()
        {
            InitInputControl(true);
        }
    }
}
