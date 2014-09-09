using System;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid.Style;
using DevComponents.DotNetBar.Controls;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// GridCheckBoxEditControl
    ///</summary>
    [ToolboxItem(false)]
    public class GridCheckBoxXEditControl : CheckBoxX, IGridCellEditControl
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
        /// GridCheckBoxXEditControl
        ///</summary>
        public GridCheckBoxXEditControl()
        {
            CheckValueChecked = null;
            CheckValueUnchecked = null;

            Text = "";

            CheckedChanged += ControlCheckedChanged;
        }

        #region ControlCheckedChanged

        private void ControlCheckedChanged(object sender, EventArgs e)
        {
            if (_SuspendUpdate == false)
            {
                if (CheckState == CheckState.Checked)
                {
                    _Cell.Value = (CheckValueChecked ?? true);
                }
                else if (CheckState == CheckState.Unchecked)
                {
                    _Cell.Value = (CheckValueUnchecked ?? false);
                }
                else
                {
                    _Cell.Value = CheckValueIndeterminate;
                }

                _Cell.EditorValueChanged(this);
            }
        }

        #endregion

        #region OnInvalidated

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);

            if (_Cell != null && _SuspendUpdate == false)
                _Cell.InvalidateRender();
        }

        #endregion

        #region OnPaint

        protected override void OnPaint(PaintEventArgs e)
        {
            _Cell.PaintEditorBackground(e, this);

            PaintControl(e);
        }

        #endregion

        #region Text

        public override string Text
        {
            get { return (base.Text); }

            set
            {
                if (string.IsNullOrEmpty(value))
                    value = " ";

                base.Text = value;
            }
        }

        #endregion

        #region GetValue

        ///<summary>
        /// GetValue
        ///</summary>
        ///<param name="value"></param>
        ///<returns></returns>
        ///<exception cref="Exception"></exception>
        public virtual CheckState GetValue(object value)
        {
            GridPanel panel = _Cell.GridPanel;

            if (value == null ||
                (panel.NullValue == NullValue.DBNull && value == DBNull.Value))
            {
                return (ThreeState ? CheckState.Indeterminate : CheckState.Unchecked);
            }

            Type type = value.GetType();

            if (CheckValueChecked != null)
            {
                if (value.Equals(CheckValueChecked))
                    return (CheckState.Checked);
            }

            if (CheckValueUnchecked != null)
            {
                if (value.Equals(CheckValueUnchecked))
                    return (CheckState.Unchecked);
            }

            if (type == typeof (bool))
                return ((bool) value ? CheckState.Checked : CheckState.Unchecked);

            if (type == typeof (string))
            {
                string s = (string) value;

                if (_Cell.IsValueExpression == true)
                    s = _Cell.GetExpValue(s);

                switch (s.ToLower())
                {
                    case "y":
                    case "yes":
                    case "t":
                    case "true":
                    case "1":
                        return (CheckState.Checked);

                    case "":
                    case "0":
                    case "n":
                    case "no":
                    case "f":
                    case "false":
                        return (CheckState.Unchecked);
                }
            }
            else if (type == typeof (SqlBoolean))
            {
                SqlBoolean sb = (SqlBoolean) value;

                if (sb.IsNull == true)
                    return (ThreeState ? CheckState.Indeterminate : CheckState.Unchecked);

                return (sb.IsTrue ? CheckState.Checked : CheckState.Unchecked);
            }
            else if (type == typeof(char))
            {
                char c = (char)value;

                switch (Char.ToLower(c))
                {
                    case 't':
                    case 'y':
                    case '1':
                        return (CheckState.Checked);

                    case 'f':
                    case 'n':
                    case '0':
                        return (CheckState.Unchecked);
                }
            }
            else
            {
                int n = Convert.ToInt32(value);

                return (n == 0 ? CheckState.Unchecked : CheckState.Checked);
            }

            throw new Exception("Invalid checkBox cell value (" + value + ").");
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
            get { return (CellEditMode.InPlace); }
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
            get { return (CheckState); }
            set { CheckState = GetValue(value); }
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
            get { return (typeof(bool)); }
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
                Enabled = (_Cell.IsReadOnly == false);

                Font = style.Font;
                ForeColor = style.TextColor;
            }

            CheckState = GetValue(_Cell.Value);

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

            if (size.Width < size.Height)
                size.Width = size.Height + 5;

            return (size);
        }

        #endregion

        #region Edit support

        #region BeginEdit

        public virtual bool BeginEdit(bool selectAll)
        {
            return (false);
        }

        #endregion

        #region EndEdit

        public virtual bool EndEdit()
        {
            return (false);
        }

        #endregion

        #region CancelEdit

        public virtual bool CancelEdit()
        {
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
            switch (e.KeyData)
            {
                case Keys.T:
                case Keys.Y:
                    if (Checked == false)
                        Checked = true;
                    break;

                case Keys.F:
                case Keys.N:
                    if (Checked == true)
                        Checked = false;
                    break;

                default:
                    OnKeyDown(e);
                    break;
            }
        }

        #endregion

        #region WantsInputKey

        public virtual bool WantsInputKey(Keys key, bool gridWantsKey)
        {
            switch (key & Keys.KeyCode)
            {
                case Keys.T:
                case Keys.F:
                case Keys.Y:
                case Keys.N:
                case Keys.Space:
                    return (true);

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
            OnMouseMove(e);
        }

        #endregion

        #region OnCellMouseEnter

        ///<summary>
        /// OnCellMouseEnter
        ///</summary>
        ///<param name="e"></param>
        public virtual void OnCellMouseEnter(EventArgs e)
        {
            OnMouseEnter(e);
        }

        #endregion

        #region OnCellMouseLeave

        ///<summary>
        /// OnCellMouseLeave
        ///</summary>
        ///<param name="e"></param>
        public virtual void OnCellMouseLeave(EventArgs e)
        {
            OnMouseLeave(e);
        }

        #endregion

        #region OnCellMouseUp

        ///<summary>
        /// OnCellMouseUp
        ///</summary>
        ///<param name="e"></param>
        public virtual void OnCellMouseUp(MouseEventArgs e)
        {
            OnMouseUp(e);
        }

        #endregion

        #region OnCellMouseDown

        ///<summary>
        /// OnCellMouseDown
        ///</summary>
        ///<param name="e"></param>
        public virtual void OnCellMouseDown(MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        #endregion

        #endregion

        #endregion
    }
}
