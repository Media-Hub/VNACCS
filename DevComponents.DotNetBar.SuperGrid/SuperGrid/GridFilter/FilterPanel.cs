using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.SuperGrid.Style;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// FilterPanel
    ///</summary>
    [ToolboxItem(false)]
    public class FilterPanel : Control
    {
        #region Static variables

        static FilterPanel _textBoxPanel;
        static FilterPanel _checkBoxPanel;
        static FilterPanel _comboBoxPanel;

        #endregion

        #region Private variables

        private FilterEditType _FilterType;

        private string _FilterExpr;
        private object _FilterValue;
        private object _FilterDisplayValue;

        private Size _SizeEx;
        private Point _OffsetEx;

        private GridColumn _GridColumn;
        private GridFilter _GridFilter;
        private Control _Control;

        private FilterColumnHeaderVisualStyle _Style;

        private bool _LockedSelection;

        #endregion

        ///<summary>
        /// Constructor
        ///</summary>
        public FilterPanel()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.Selectable, false);
        }

        #region Public properties

        #region Control

        ///<summary>
        /// Control
        ///</summary>
        public Control Control
        {
            get { return (_Control); }

            internal set
            {
                if (_Control != value)
                {
                    if (_Control is MyCheckBoxX)
                        FilterCheckBox = null;

                    else if (_Control is ComboBoxEx)
                        FilterComboBox = null;

                    else if (_Control is TextBoxX)
                        FilterTextBox = null;
                }

                if (value != null)
                {
                    Controls.Clear();
                    Controls.Add(value);
                }

                _Control = value;
            }
        }

        #endregion

        #region GridColumn

        ///<summary>
        /// GridColumn
        ///</summary>
        public GridColumn GridColumn
        {
            get { return (_GridColumn); }
            set { _GridColumn = value; }
        }

        #endregion

        #region GridFilter

        ///<summary>
        /// GridFilter
        ///</summary>
        public GridFilter GridFilter
        {
            get { return (_GridFilter); }
            set { _GridFilter = value; }
        }

        #endregion

        #region Style

        ///<summary>
        /// Style
        ///</summary>
        public FilterColumnHeaderVisualStyle Style
        {
            get { return (_Style); }
            set { _Style = value; }
        }

        #endregion

        #endregion

        #region Internal properties

        #region FilterCheckBox

        internal MyCheckBoxX FilterCheckBox
        {
            get
            {
                MyCheckBoxX mcb = Control as MyCheckBoxX;

                if (mcb == null)
                {
                    mcb = new MyCheckBoxX();

                    mcb.ThreeState = true;
                    mcb.FocusCuesEnabled = false;

                    mcb.CheckedChanged += FilterValueChanged;
                    mcb.LostFocus += ControlLostFocus;
                }

                return (mcb);
            }

            set
            {
                MyCheckBoxX mcb = Control as MyCheckBoxX;

                if (mcb != null)
                {
                    mcb.CheckedChanged -= FilterValueChanged;
                    mcb.LostFocus -= ControlLostFocus;
                }
            }
        }

        #endregion

        #region FilterComboBox

        internal ComboBoxEx FilterComboBox
        {
            get
            {
                ComboBoxEx cb = Control as ComboBoxEx;

                if (cb == null)
                {
                    cb = new ComboBoxEx();

                    cb.DrawMode = DrawMode.OwnerDrawFixed;
                    cb.DropDownStyle = ComboBoxStyle.DropDownList;

                    cb.SelectedValueChanged += FilterValueChanged;
                    cb.LostFocus += ControlLostFocus;
                }

                return (cb);
            }

            set
            {
                ComboBoxEx cb = Control as ComboBoxEx;

                if (cb != null)
                {
                    cb.SelectedValueChanged -= FilterValueChanged;
                    cb.LostFocus -= ControlLostFocus;
                }
            }
        }

        #endregion

        #region FilterTextBox

        internal TextBoxX FilterTextBox
        {
            get
            {
                TextBoxX tb = Control as TextBoxX;

                if (tb == null)
                {
                    tb = new TextBoxX();

                    tb.BorderStyle = BorderStyle.None;

                    tb.TextChanged += FilterValueChanged;
                    tb.LostFocus += ControlLostFocus;

                }

                return (tb);
            }

            set
            {
                TextBoxX tb = Control as TextBoxX;

                if (tb != null)
                {
                    tb.TextChanged -= FilterValueChanged;
                    tb.LostFocus -= ControlLostFocus;
                }
            }
        }

        #endregion

        #region FilterType

        internal FilterEditType FilterType
        {
            get { return (_FilterType); }
            set { _FilterType = value; }
        }

        #endregion

        #region OffsetEx

        internal Point OffsetEx
        {
            get { return (_OffsetEx); }
            set { _OffsetEx = value; }
        }

        #endregion

        #region SizeEx

        internal Size SizeEx
        {
            get { return (_SizeEx); }
            set { _SizeEx = value; }
        }

        #endregion

        #endregion

        #region OnPaintBackground

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (_Style != null && _GridColumn != null)
            {
                Rectangle r = ClientRectangle;

                if (r.IsEmpty == false)
                {
                    using (Brush br = _Style.Background.GetBrush(r))
                        g.FillRectangle(br, r);
                }
            }
            else
            {
                base.OnPaintBackground(e);
            }
        }

        #endregion

        #region GetPreferredSize

        static TextBoxX _sizeTextBox;
        static ComboBoxEx _sizeComboBox;
        static MyCheckBoxX _sizeCheckBox;

        static internal Size GetPreferredSize(
            GridFilter gridFilter, GridColumn gridColumn)
        {
            Size size = Size.Empty;

            FilterEditType type = gridColumn.GetFilterPanelType();

            switch (type)
            {
                case FilterEditType.ComboBox:
                    if (_sizeComboBox == null)
                        _sizeComboBox = new ComboBoxEx();

                    _sizeComboBox.Font = gridFilter.GetEffectiveStyle(gridColumn).Font;

                    size = _sizeComboBox.GetPreferredSize(Size.Empty);
                    break;

                case FilterEditType.TextBox:
                case FilterEditType.DateTime:
                    if (_sizeTextBox == null)
                        _sizeTextBox = new TextBoxX();

                    _sizeTextBox.BorderStyle = BorderStyle.None;
                    _sizeTextBox.Font = gridFilter.GetEffectiveStyle(gridColumn).Font;

                    size = _sizeTextBox.GetPreferredSize(Size.Empty);
                    break;

                case FilterEditType.CheckBox:
                    if (_sizeCheckBox == null)
                        _sizeCheckBox = new MyCheckBoxX();

                    size = new Size(20, 20);
                    break;
            }

            return (size);
        }

        #endregion

        #region RenderCheckBox

        static internal void RenderCheckBox(
            Graphics g, GridFilter gf, GridColumn column, Rectangle bounds)
        {
            object value = column.FilterValue;

            if (_sizeCheckBox == null)
            {
                _sizeCheckBox = new MyCheckBoxX();

                _sizeCheckBox.Text = "";
                _sizeCheckBox.BackColor = Color.Transparent;
            }

            _sizeCheckBox.Enabled = false;
            _sizeCheckBox.Size = new Size(20, 20);
            _sizeCheckBox.BackColor = GetSimpleColor(gf.GetEffectiveStyle(column));

            if (value is CheckState)
                _sizeCheckBox.CheckState = (CheckState)value;
            else
                _sizeCheckBox.CheckState = CheckState.Indeterminate;

            Rectangle r = new Rectangle(0, 0, 20, 20);

            using (Bitmap bm = new Bitmap(20, 20))
            {
                _sizeCheckBox.DrawToBitmap(bm, r);

                Rectangle t = bounds;

                if (bounds.Width > r.Width)
                {
                    t.X += (t.Width - r.Width - 1) / 2;
                    t.Width = r.Width;
                }

                if (bounds.Height > r.Height)
                {
                    t.Y += (t.Height - r.Height) / 2;
                    t.Height = r.Height;
                }

                g.DrawImage(bm, t);
            }
        }

        #endregion

        #region GetFilterPanel

        static internal FilterPanel GetFilterPanel(GridColumn gridColumn)
        {
            FilterEditType type = gridColumn.GetFilterPanelType();

            if (type != FilterEditType.None)
            {
                GridFilter gridFilter = gridColumn.GridPanel.Filter;

                FilterPanel fp = GetFilterPanel(gridFilter, type);

                fp.GridFilter = gridFilter;
                fp.GridColumn = gridColumn;

                fp.Style = gridFilter.GetEffectiveStyle(gridColumn);

                fp.InitEditControl(type);

                return (fp);
            }

            return (null);
        }

        #region InitEditControl

        private void InitEditControl(FilterEditType type)
        {
            FilterType = type;

            switch (type)
            {
                case FilterEditType.CheckBox:
                    InitCheckBoxEdit();
                    break;

                case FilterEditType.ComboBox:
                    InitComboBoxEdit();
                    break;

                case FilterEditType.DateTime:
                case FilterEditType.TextBox:
                    InitTextBoxEdit();
                    break;
            }
        }

        #region InitCheckBoxEdit

        private void InitCheckBoxEdit()
        {
            MyCheckBoxX cb = FilterCheckBox;

            cb.Text = "";
            cb.Enabled = true;

            cb.BackColor = GetSimpleColor(Style);

            Control = cb;
        }

        #endregion

        #region InitComboBoxEdit

        private void InitComboBoxEdit()
        {
            ComboBoxEx cb = FilterComboBox;

            cb.Items.Clear();

            cb.BackColor = GetSimpleColor(Style);
            cb.Font = Style.Font;
            cb.FocusCuesEnabled = false;

            Control = cb;
        }

        #endregion

        #region InitTextBoxEdit

        private void InitTextBoxEdit()
        {
            TextBoxX tb = FilterTextBox;

            tb.BackColor = GetSimpleColor(Style);
            tb.Font = Style.Font;

            switch (Style.Alignment)
            {
                case Alignment.TopRight:
                case Alignment.MiddleRight:
                case Alignment.BottomRight:
                    tb.TextAlign = HorizontalAlignment.Right;
                    break;

                case Alignment.TopCenter:
                case Alignment.MiddleCenter:
                case Alignment.BottomCenter:
                    tb.TextAlign = HorizontalAlignment.Center;
                    break;

                default:
                    tb.TextAlign = HorizontalAlignment.Left;
                    break;
            }

            Control = tb;
        }

        #endregion

        #endregion

        #region GetFilterPanel

        static private FilterPanel GetFilterPanel(
            GridFilter gridFilter, FilterEditType type)
        {
            switch (type)
            {
                case FilterEditType.CheckBox:
                    if (_checkBoxPanel == null || _checkBoxPanel.Parent != gridFilter.SuperGrid)
                        _checkBoxPanel = GetNewFilterPanel(gridFilter);

                    return (_checkBoxPanel);

                case FilterEditType.ComboBox:
                    if (_comboBoxPanel == null || _comboBoxPanel.Parent != gridFilter.SuperGrid)
                        _comboBoxPanel = GetNewFilterPanel(gridFilter);

                    return (_comboBoxPanel);

                case FilterEditType.DateTime:
                case FilterEditType.TextBox:
                    if (_textBoxPanel == null || _textBoxPanel.Parent != gridFilter.SuperGrid)
                        _textBoxPanel = GetNewFilterPanel(gridFilter);

                    return (_textBoxPanel);
            }

            return (null);
        }

        #region GetNewFilterPanel

        private static FilterPanel GetNewFilterPanel(GridFilter gridFilter)
        {
            FilterPanel fp = new FilterPanel();
            fp.Visible = false;

            gridFilter.SuperGrid.Controls.Add(fp);

            return (fp);
        }

        #endregion

        #endregion

        #region GetFilterType

        private static Type GetFilterType(FilterPanel fp)
        {
            IGridCellEditControl editor = fp.GridColumn.EditControl;

            if (editor != null)
                return (editor.EditorValueType);

            return (typeof(string));
        }

        #endregion

        #region GetSimpleColor

        static internal Color GetSimpleColor(FilterColumnHeaderVisualStyle style)
        {
            Color color1;
            Color color2;

            if (style.Background.BackColorBlend != null &&
                style.Background.BackColorBlend.IsEmpty == false)
            {
                IList<Color> colors = style.Background.BackColorBlend.Colors;

                color1 = colors[0];
                color2 = colors[colors.Count - 1];
            }
            else
            {
                color1 = style.Background.Color1;
                color2 = style.Background.Color2;
            }

            if (color2.IsEmpty == true)
                return (color1);

            return (Color.FromArgb((color1.R + color2.R) / 2,
                (color1.G + color2.G) / 2, (color1.B + color2.B) / 2));
        }

        #endregion

        #endregion

        #region FilterValueChanged

        void FilterValueChanged(object sender, EventArgs e)
        {
            if (_LockedSelection == false)
            {
                Control c = (Control) sender;

                FilterPanel fp = c.Parent as FilterPanel;

                if (fp != null)
                {
                    object filterValue;
                    object filterDisplayValue;
                    string filterExpr;

                    switch (fp.FilterType)
                    {
                        case FilterEditType.CheckBox:
                            filterExpr = GetCheckBoxFilterExpr(
                                c, fp, out filterValue, out filterDisplayValue);
                            break;

                        case FilterEditType.ComboBox:
                            filterExpr = GetComboBoxFilterExpr(
                                c, fp, out filterValue, out filterDisplayValue);
                            break;

                        default:
                            filterExpr = GetTextBoxFilterExpr(
                               c, fp, out filterValue, out filterDisplayValue);
                            break;
                    }

                    GridColumn column = fp.GridColumn;

                    if (column.SuperGrid.DoFilterEditValueChangedEvent(column.GridPanel, column, fp,
                        column.FilterValue, ref filterValue, ref filterDisplayValue, ref filterExpr) == false)
                    {
                        column.FilterValue = filterValue;
                        column.FilterDisplayValue = filterDisplayValue;
                        column.FilterExpr = filterExpr;
                    }
                }
            }
        }

        #region GetCheckBoxFilterExpr

        private string GetCheckBoxFilterExpr(Control c,
            FilterPanel fp, out object filterValue, out object filterDisplayValue)
        {
            filterValue = CheckState.Indeterminate;
            filterDisplayValue = CheckState.Indeterminate;

            MyCheckBoxX mcb = c as MyCheckBoxX;

            if (mcb != null)
            {
                CheckState cs = mcb.CheckState;

                filterValue = cs;
                filterDisplayValue = cs;

                if (cs != CheckState.Indeterminate)
                {
                    return ("[" + fp.GridColumn.Name + "] = " +
                            (cs == CheckState.Checked ? "true" : "false"));
                }
            }

            return (null);
        }

        #endregion

        #region GetComboBoxFilterExpr

        private string GetComboBoxFilterExpr(Control c,
            FilterPanel fp, out object filterValue, out object filterDisplayValue)
        {
            filterValue = null;
            filterDisplayValue = null;

            ComboBoxEx cb = c as ComboBoxEx;

            if (cb != null)
            {
                string s1 = c.Text;

                MyComboItem mci = cb.SelectedItem as MyComboItem;

                if (fp.GridColumn.FilterAutoScan == false)
                {
                    if (mci != null)
                    {
                        ComboBox cbx = fp.GridColumn.EditControl as ComboBox;

                        if (cbx != null)
                        {
                            cbx.SelectedIndex = (int) mci.Value;

                            s1 = cbx.Text;

                            if (cbx.DataSource != null)
                                s1 = cbx.SelectedValue.ToString();
                        }
                    }
                }
                else
                {
                    if (mci != null)
                    {
                        if (mci.Value is DateTime)
                        {
                            filterValue = s1;
                            filterDisplayValue = c.Text;

                            return ("[" + fp.GridColumn.Name + "] = #" + s1 + "#");
                        }
                    }
                }

                if (string.IsNullOrEmpty(s1) == false)
                {
                    filterValue = s1;
                    filterDisplayValue = c.Text;
                    
                    return ("[" + fp.GridColumn.Name + "] = '" + s1 + "'");
                }
            }

            return (null);
        }

        #endregion

        #region GetTextBoxFilterExpr

        private string GetTextBoxFilterExpr(Control c,
            FilterPanel fp, out object filterValue, out object filterDisplayValue)
        {
            filterValue = null;
            filterDisplayValue = null;

            object bds = fp.GridColumn.GridPanel.DataBinder.BaseDataSource;

            bool dsFilter = (fp.GridColumn.GridPanel.VirtualMode == true &&
                (bds is DataSet || bds is DataTable));

            string s = (c.Text ?? "").Trim();

            if (string.IsNullOrEmpty(s) == false)
            {
                filterValue = c.Text;
                filterDisplayValue = c.Text;

                if (dsFilter == true)
                {
                    s = "\'" + s + "*\'";

                    return ("Convert([" + fp.GridColumn.Name + "],System.String) Like " + s);
                }

                s = "\'" + s + "\'";

                if (GetFilterType(fp) == typeof(string))
                    return ("[" + fp.GridColumn.Name + "] Like " + s);

                return ("ToString([" + fp.GridColumn.Name + "]) Like " + s);
            }

            return (null);
        }

        #endregion

        #endregion

        #region ControlLostFocus

        static void ControlLostFocus(object sender, EventArgs e)
        {
            Control c = sender as Control;

            if (c != null)
            {
                FilterPanel fp = c.Parent as FilterPanel;

                if (fp != null)
                    fp.EndEdit();
            }
        }

        #endregion

        #region BeginEdit

        internal bool BeginEdit()
        {
            GridPanel panel = GridColumn.GridPanel;

            if (panel.SuperGrid.DoFilterBeginEditEvent(panel, GridColumn) == false)
            {
                panel.ClearAll();

                GridColumn.EnsureVisible();
                PositionEditPanel();

                GridColumn.SuperGrid.ActiveFilterPanel = this;

                _FilterExpr = GridColumn.FilterExpr;
                _FilterValue = GridColumn.FilterValue;
                _FilterDisplayValue = GridColumn.FilterDisplayValue;

                _LockedSelection = true;

                switch (FilterType)
                {
                    case FilterEditType.CheckBox:
                        BeginCheckBoxEdit();
                        break;

                    case FilterEditType.ComboBox:
                        BeginComboBoxEdit();
                        break;

                    default:
                        BeginTextBoxEdit();
                        break;
                }

                Show();
                BringToFront();
                Control.Focus();

                _LockedSelection = false;

                InvalidateFilterCell();

                return (false);
            }

            return (true);
        }

        #region BeginCheckBoxEdit

        private void BeginCheckBoxEdit()
        {
            MyCheckBoxX mcbx = Control as MyCheckBoxX;

            if (mcbx != null)
            {
                CheckState cs = CheckState.Indeterminate;

                if (_FilterValue != null)
                    cs = (CheckState)_FilterValue;

                mcbx.CheckState = cs;
            }
        }

        #endregion

        #region BeginComboBoxEdit

        private void BeginComboBoxEdit()
        {
            ComboBoxEx cb = Control as ComboBoxEx;

            if (cb != null)
            {
                if (_GridColumn.SuperGrid.DoFilterLoadItemsEvent(_GridColumn, cb) == false)
                {
                    if (_GridColumn.FilterAutoScan == true)
                        LoadScanItems(cb);
                    else
                        LoadComboItems(cb);

                    cb.Text = (_FilterValue != null) ? _FilterValue.ToString() : "";

                    int m = _GridColumn.SuperGrid.FilterMaxDropDownHeight;

                    cb.MaxDropDownItems = m / cb.ItemHeight;
                    cb.IntegralHeight = false;

                    _GridColumn.SuperGrid.DoFilterItemsLoadedEvent(_GridColumn, cb);
                }
            }
        }

        #region LoadScanItems

        private void LoadScanItems(ComboBoxEx cb)
        {
            List<object> list = _GridColumn.ScanItems;

            int n = Math.Min(list.Count, _GridColumn.FilterPopupMaxItems);

            MyComboItem[] items = new MyComboItem[n];

            bool blankSeen = false;

            for (int i = 0; i < n; i++)
            {
                object o = list[i];

                MyComboItem bi;

                if (o is DateTime)
                {
                    DateTime dt = (DateTime)o;
                    dt = dt.Date;

                    string s = dt.ToShortDateString();

                    bi = new MyComboItem("ScanDateEntry", s, dt);
                }
                else
                {
                    string s = o.ToString();

                    bi = new MyComboItem("ScanTextEntry", s, i);

                    if (blankSeen == false && string.IsNullOrEmpty(bi.Text) == true)
                        blankSeen = true;
                }

                items[i] = bi;
            }

            if (blankSeen == false)
                cb.Items.Add("");

            cb.Items.AddRange(items);
        }

        #endregion

        #region LoadComboItems

        private void LoadComboItems(ComboBoxEx cb)
        {
            bool blankSeen = false;

            ComboBox cbx = GridColumn.EditControl as ComboBox;

            if (cbx != null && cbx.Items.Count > 0)
            {
                MyComboItem[] items = new MyComboItem[cbx.Items.Count];

                for (int i = 0; i < cbx.Items.Count; i++)
                {
                    MyComboItem bi = new
                        MyComboItem("CbxEntry", cbx.GetItemText(cbx.Items[i]), i);

                    if (blankSeen == false && string.IsNullOrEmpty(bi.Text) == true)
                        blankSeen = true;

                    items[i] = bi;
                }

                if (blankSeen == false)
                    cb.Items.Add("");

                cb.Items.AddRange(items);
            }
        }

        #endregion

        #endregion

        #region BeginTextBoxEdit

        private void BeginTextBoxEdit()
        {
            TextBoxX tbx = Control as TextBoxX;

            if (tbx != null)
            {
                if (_FilterValue is DateTime)
                {
                    tbx.Text = ((DateTime)_FilterValue).ToString("d");
                }
                else
                {
                    tbx.Text = (_FilterValue != null)
                        ? _FilterValue.ToString() : "";
                }

                tbx.SelectAll();
            }
        }

        #endregion

        #endregion

        #region CancelEdit

        internal void CancelEdit()
        {
            GridColumn.FilterValue = _FilterValue;
            GridColumn.FilterDisplayValue = _FilterDisplayValue;
            GridColumn.FilterExpr = _FilterExpr;

            GridColumn.SuperGrid.DoFilterCancelEditEvent(GridColumn);

            CloseEdit();
        }

        #endregion

        #region EndEdit

        internal void EndEdit()
        {
            GridColumn.SuperGrid.DoFilterEndEditEvent(GridColumn);

            CloseEdit();
        }

        #endregion

        #region CloseEdit

        internal void CloseEdit()
        {
            bool wasFocused = GridColumn.SuperGrid.ContainsFocus;

            Hide();

            GridColumn.SuperGrid.ActiveFilterPanel = null;

            Control = null;

            InvalidateFilterCell();

            if (wasFocused == true)
                GridColumn.SuperGrid.Focus();
        }

        #endregion

        #region InvalidateFilterCell

        private void InvalidateFilterCell()
        {
            Rectangle r = GridFilter.GetBounds(GridColumn.GridPanel, GridColumn);

            GridColumn.SuperGrid.Invalidate(r);
        }

        #endregion

        #region PositionEditPanel

        internal void PositionEditPanel()
        {
            Rectangle r = GridFilter.GetBounds(GridColumn.GridPanel, GridColumn);

            r.X++;
            r.Y++;
            r.Width -= 3;
            r.Height -= 3;

            r.X += Style.Margin.Left;
            r.Y += Style.Margin.Top;
            r.Width -= Style.Margin.Horizontal;
            r.Height -= Style.Margin.Vertical;

            Rectangle e = GetClippedBounds(r);

            int xoff = 0;
            int yoff = 0;

            if (r.X < e.X)
                xoff = r.X - e.X;

            if (r.Y < e.Y)
                yoff = r.Y - e.Y;

            Size size = Control.GetPreferredSize(Size.Empty);

            if (Control is MyCheckBoxX == false)
                size.Width = Math.Max(20, r.Width);

            if (size.Height == 0)
                size.Height = 20;

            if (size.Width == 0)
                size.Width = 20;

            Rectangle t = new Rectangle(xoff, yoff, size.Width, size.Height);

            if (t.Height < e.Height)
                t.Y += (e.Height - t.Height) / 2;

            if (t.Width < e.Width)
                t.X += (e.Width - t.Width) / 2;

            Bounds = e;
            Control.Bounds = t;
        }

        #endregion

        #region GetClippedBounds

        private Rectangle GetClippedBounds(Rectangle r)
        {
            Rectangle v = GridColumn.SViewRect;

            if (GridColumn.GridPanel.IsVFrozen == false)
            {
                int n = GridColumn.GridPanel.FixedRowHeight -
                    (GridColumn.GridPanel.FixedHeaderHeight);

                v.Y -= n;
                v.Height += n;
            }
            else
            {
                if (r.Y < v.Y)
                {
                    v.Height += (v.Y - r.Y);
                    v.Y = r.Y;
                }
            }

            if (GridColumn.IsHFrozen == false)
            {
                GridColumn pcol = GridColumn.GridPanel.Columns.GetLastVisibleFrozenColumn();

                if (pcol != null)
                {
                    int n = pcol.BoundsRelative.Right - v.X;

                    v.X += n;
                    v.Width -= n;
                }
            }

            //r.Width -= 1;
            //r.Height -= 1;

            r.Intersect(v);

            return (r);
        }

        #endregion

        #region WantsInputKey

        ///<summary>
        ///</summary>
        ///<param name="key"></param>
        ///<param name="gridWantsKey"></param>
        ///<returns></returns>
        public bool WantsInputKey(Keys key, bool gridWantsKey)
        {
            switch (key)
            {
                case Keys.Down | Keys.Control:
                    return (false);
            }

            switch (FilterType)
            {
                case FilterEditType.ComboBox:
                    switch (key)
                    {
                        case Keys.Space:
                        case Keys.Up:
                        case Keys.Down:
                            return (true);
                    }
                    break;

                case FilterEditType.CheckBox:
                    switch (key)
                    {
                        case Keys.Space:
                            return (true);
                    }
                    break;

                case FilterEditType.DateTime:
                case FilterEditType.TextBox:
                    TextBoxX tb = Control as TextBoxX;

                    if (tb != null)
                    {
                        switch (key)
                        {
                            case Keys.Left:
                                return (tb.SelectionStart > 0);

                            case Keys.Right:
                                return (tb.SelectionStart < tb.TextLength);
                        }
                    }
                    break;
            }

            return (gridWantsKey == false);
        }

        #endregion

        #region Select

        ///<summary>
        /// Activates the control
        ///</summary>
        public new void Select()
        {
            base.Select();

            if (Control != null)
                Control.Select();
        }

        #endregion
    }

    #region MyCheckBoxX

    internal class MyCheckBoxX : CheckBoxX
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Space:
                    e.Handled = true;

                    switch (CheckState)
                    {
                        case CheckState.Unchecked:
                            CheckState = CheckState.Checked;
                            break;

                        case CheckState.Checked:
                            CheckState = CheckState.Indeterminate;
                            break;

                        default:
                            CheckState = CheckState.Unchecked;
                            break;
                    }
                    break;

                default:
                    base.OnKeyDown(e);
                    break;
            }
        }
    }

    #endregion

    #region MyComboItem

    internal class MyComboItem
    {
        private string _Name;
        private string _Text;

        private object _Value;

        public MyComboItem(string name)
            : this(name, null, -1)
        {
        }

        public MyComboItem(string name, string text)
            : this(name, text, -1)
        {
        }

        public MyComboItem(string name, string text, object value)
        {
            _Name = name;
            _Text = text;

            _Value = value;
        }

        public object Value
        {
            get { return (_Value); }
            set { _Value = value; }
        }

        public string Name
        {
            get { return (_Name); }
            set { _Name = value; }
        }

        public string Text
        {
            get { return (_Text); }
            set { _Text = value; }
        }

        public override string ToString()
        {
            return (_Text);
        }
    }

    #endregion

    #region enums

    /// <summary>
    /// FilterEditType
    /// </summary>
    public enum FilterEditType
    {
        /// <summary>
        /// Auto
        /// </summary>
        Auto,

        ///<summary>
        /// None
        ///</summary>
        None,

        ///<summary>
        /// CheckBox
        ///</summary>
        CheckBox,

        ///<summary>
        /// ComboBox
        ///</summary>
        ComboBox,

        ///<summary>
        /// DateTime
        ///</summary>
        DateTime,

        ///<summary>
        /// TextBox
        ///</summary>
        TextBox,
    }

    #endregion
}
