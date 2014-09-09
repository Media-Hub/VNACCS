using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid.Style;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// GridCheckBoxEditControl
    ///</summary>
    [ToolboxItem(false)]
    public class GridCheckBoxEditControl : CheckBox, IGridCellEditControl
    {
        #region Private variables

        private GridCell _Cell;
        private EditorPanel _EditorPanel;
        private Bitmap _EditorCellBitmap;

        private bool _ValueChanged;
        private bool _SuspendUpdate;

        private object _CheckValueChecked;
        private object _CheckValueUnchecked;

        private StretchBehavior _StretchBehavior = StretchBehavior.None;

        #endregion

        ///<summary>
        /// GridCheckBoxEditControl
        ///</summary>
        public GridCheckBoxEditControl()
        {
            BackColor = Color.Transparent;

            CheckedChanged += ControlCheckedChanged;
        }

        #region Public properties

        #region CheckValueChecked

        ///<summary>
        /// CheckValueChecked
        ///</summary>
        public object CheckValueChecked
        {
            get { return (_CheckValueChecked); }
            set { _CheckValueChecked = value; }
        }

        #endregion

        #region CheckValueUnchecked

        ///<summary>
        /// CheckValueUnchecked
        ///</summary>
        public object CheckValueUnchecked
        {
            get { return (_CheckValueUnchecked); }
            set { _CheckValueUnchecked = value; }
        }

        #endregion

        #endregion

        #region ControlCheckedChanged

        private void ControlCheckedChanged(object sender, EventArgs e)
        {
            if (_SuspendUpdate == false)
            {
                if (Checked == true)
                    _Cell.Value = _CheckValueChecked ?? true;
                else
                    _Cell.Value = _CheckValueUnchecked ?? false;

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

        #region GetValue

        ///<summary>
        /// GetValue
        ///</summary>
        ///<param name="value"></param>
        ///<returns></returns>
        ///<exception cref="Exception"></exception>
        public virtual bool GetValue(object value)
        {
            GridPanel panel = _Cell.GridPanel;

            if (value == null ||
                (panel.NullValue == NullValue.DBNull && value == DBNull.Value))
            {
                return (false);
            }

            Type type = value.GetType();

            if (CheckValueChecked != null)
            {
                if (value.Equals(CheckValueChecked))
                    return (true);
            }
            else
            {
                if (type == typeof(bool))
                    return ((bool)value);

                if (type == typeof(string))
                {
                    string s = (string)value;

                    if (_Cell.IsValueExpression == true)
                        s = _Cell.GetExpValue(s);

                    switch (s.ToLower())
                    {
                        case "y":
                        case "yes":
                        case "t":
                        case "true":
                        case "1":
                            return (true);
                    }
                }
                else
                {
                    int n = Convert.ToInt32(value);

                    if (n == 1)
                        return (true);
                }
            }

            if (CheckValueUnchecked != null)
            {
                if (value.Equals(CheckValueUnchecked))
                    return (true);
            }
            else
            {
                if (type == typeof(bool))
                    return ((bool)value);

                if (type == typeof(string))
                {
                    string s = (string)value;

                    if (_Cell.IsValueExpression == true)
                        s = _Cell.GetExpValue(s);

                    switch (s.ToLower())
                    {
                        case "n":
                        case "no":
                        case "f":
                        case "false":
                        case "0":
                            return (false);
                    }
                }
                else
                {
                    int n = Convert.ToInt32(value);

                    if (n == 0)
                        return (false);
                }
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
            get { return (CellEditMode.NonModal); }
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
            get { return (Checked); }
            set { Checked = GetValue(value); }
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
            get { return (typeof(string)); }
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

            Checked = GetValue(_Cell.Value);

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
            Focus();

            switch (e.KeyData)
            {
                case Keys.T:
                    if (Checked == false)
                        Checked = true;
                    break;

                case Keys.F:
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
