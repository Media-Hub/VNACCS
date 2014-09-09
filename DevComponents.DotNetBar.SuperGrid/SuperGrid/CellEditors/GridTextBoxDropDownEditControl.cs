using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid.Style;
using DevComponents.DotNetBar.Controls;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// GridTextBoxDropDownControl
    ///</summary>
    [ToolboxItem(false)]
    public class GridTextBoxDropDownEditControl : TextBoxDropDown, IGridCellEditControl
    {
        #region DllImports / delegates

        private delegate bool EnumThreadWndProc(IntPtr hWnd, IntPtr lp);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool EnumThreadWindows(int tid, EnumThreadWndProc callback, IntPtr lp);

        [DllImport("user32.dll")]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder buffer, int buflen);

        [DllImport("kernel32.dll")]
        private static extern int GetCurrentThreadId();

        #endregion

        #region Private variables

        private GridCell _Cell;
        private EditorPanel _EditorPanel;
        private Bitmap _EditorCellBitmap;

        private bool _ValueChanged;
        private bool _SuspendUpdate;

        private StretchBehavior _StretchBehavior = StretchBehavior.None;

        private bool _AutoSuggestDropdownOpen;

        #endregion

        ///<summary>
        /// GridTextBoxDropDownEditControl
        ///</summary>
        public GridTextBoxDropDownEditControl()
        {
            BackgroundStyle.Class = "TextBoxBorder";
            Style = eDotNetBarStyle.StyleManagerControlled;
        }

        #region OnTextChanged

        protected override void OnTextChanged(EventArgs e)
        {
            if (_SuspendUpdate == false)
                _Cell.EditorValueChanged(this);

            base.OnTextChanged(e);
        }

        #endregion

        #region GetValue

        ///<summary>
        /// GetValue
        ///</summary>
        ///<param name="value"></param>
        ///<returns></returns>
        public virtual string GetValue(object value)
        {
            GridPanel panel = _Cell.GridPanel;

            if (value == null ||
                (panel.NullValue == NullValue.DBNull && value == DBNull.Value))
            {
                return ("");
            }

            if (_Cell.IsValueExpression == true)
                value = _Cell.GetExpValue((string)value);

            return (Convert.ToString(value));
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
            get { return (Text); }
            set { Text = GetValue(value); }
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
                Enabled = (_Cell.ReadOnly == false);
                ForeColor = style.TextColor;
                Font = style.Font;

                SetTextAlign(style);
            }

            Text = GetValue(_Cell.Value);

            _ValueChanged = false;
        }

        #region SetTextAlign

        private void SetTextAlign(CellVisualStyle style)
        {
            switch (style.Alignment)
            {
                case Alignment.TopLeft:
                case Alignment.MiddleLeft:
                case Alignment.BottomLeft:
                    TextAlign = HorizontalAlignment.Left;
                    break;

                case Alignment.TopCenter:
                case Alignment.MiddleCenter:
                case Alignment.BottomCenter:
                    TextAlign = HorizontalAlignment.Center;
                    break;

                case Alignment.TopRight:
                case Alignment.MiddleRight:
                case Alignment.BottomRight:
                    TextAlign = HorizontalAlignment.Right;
                    break;
            }
        }

        #endregion

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
            Show();
            Focus();
            
            if (selectAll == true)
                SelectAll();
            else
                Select(Text.Length, 1);

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
            // Let the TextBox handle the following keys

            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                    return (true);

                case Keys.Down:
                case Keys.Up:
                    if (IsAutoSuggestDropdownOpen())
                        return (true);

                    return (gridWantsKey == false);

                default:
                    return (gridWantsKey == false);
            }
        }

        #region IsAutoSuggestDropdownOpen

        private bool IsAutoSuggestDropdownOpen()
        {
            _AutoSuggestDropdownOpen = false;

            if (AutoCompleteMode == AutoCompleteMode.Suggest ||
                AutoCompleteMode == AutoCompleteMode.SuggestAppend)
            {
                EnumThreadWndProc callback = CheckWindow;
                EnumThreadWindows(GetCurrentThreadId(), callback, IntPtr.Zero);
            }

            return (_AutoSuggestDropdownOpen);
        }

        #region CheckWindow

        private bool CheckWindow(IntPtr hWnd, IntPtr lp)
        {
            StringBuilder sb = new StringBuilder(260);
            GetClassName(hWnd, sb, sb.Capacity);

            if (sb.ToString() == "Auto-Suggest Dropdown")
            {
                _AutoSuggestDropdownOpen = IsWindowVisible(hWnd);

                return (false);
            }

            return (true);
        }

        #endregion

        #endregion

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
    }
}
