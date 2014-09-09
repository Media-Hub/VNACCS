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
    /// GridTextBoxEditControl
    ///</summary>
    [ToolboxItem(false)]
    public class GridTextBoxXEditControl : TextBoxX, IGridCellEditControl
    {
        ///<summary>
        /// GridTextBoxXEditControl
        ///</summary>
        public GridTextBoxXEditControl()
        {
            Multiline = true;
        }

        #region Private variables

        private GridCell _Cell;
        private EditorPanel _EditorPanel;
        private Bitmap _EditorCellBitmap;

        private bool _ValueChanged;
        private bool _SuspendUpdate;

        private StretchBehavior _StretchBehavior = StretchBehavior.None;

        #endregion

        #region OnTextChanged

        protected override void OnTextChanged(EventArgs e)
        {
            if (_SuspendUpdate == false)
                _Cell.EditorValueChanged(this);

            base.OnTextChanged(e);
        }

        #endregion

        #region OnPaintBackground

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            _Cell.PaintEditorBackground(e, this);
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

            if (value is SqlString)
            {
                SqlString sdt = (SqlString) value;

                if (sdt.IsNull == true)
                    return ("");

                return (sdt.Value);
            }

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

                return ((PasswordChar == '\0')
                    ? Text : new string(PasswordChar, TextLength));
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
            get
            {
                return ((Multiline == true)
                    ? StretchBehavior.VerticalOnly : StretchBehavior.None );
            }

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
            get { return (ValueChangeBehavior.InvalidateLayout); }
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
                WordWrap = (style.AllowWrap == Tbool.True);
                ForeColor = style.TextColor;
                Font = style.Font;
                Multiline = WordWrap;

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
            eTextFormat tf = eTextFormat.NoPadding |
                eTextFormat.WordEllipsis | eTextFormat.NoPrefix;

            if (style.AllowWrap == Tbool.True)
                tf |= eTextFormat.WordBreak;

            string s = String.IsNullOrEmpty(Text) ? " " : Text;

            Size size = (constraintSize.IsEmpty == true)
                ? TextHelper.MeasureText(g, s, style.Font, new Size(10000, 0), tf)
                : TextHelper.MeasureText(g, s, style.Font, constraintSize, tf);

            return (size);
        }

        #endregion

        #region Edit support

        #region BeginEdit

        public virtual bool BeginEdit(bool selectAll)
        {
            if (selectAll == true)
                SelectAll();
            else
                Select(Text.Length, 1);

            Show();
            Focus();

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

                case Keys.Tab:
                    if (AcceptsTab == true)
                        return (true);
                    break;

                case Keys.Enter:
                    if (Multiline == true && AcceptsReturn == true)
                        return (true);
                    break;
            }

            return (gridWantsKey == false);
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
    }
}
