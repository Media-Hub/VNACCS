using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.SuperGrid.Style;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// Constructor
    ///</summary>
    public class FilterPopup : IDisposable
    {
        #region Delegates

        ///<summary>
        /// PfnReset
        ///</summary>
        public delegate void PfnReset();

        #endregion

        #region Constants

        private readonly Size _DefaultGripSize = new Size(14, 14);

        private readonly System.Windows.Forms.Padding _DefaultPadding = System.Windows.Forms.Padding.Empty;
        private readonly System.Windows.Forms.Padding _DefaultMargin = new System.Windows.Forms.Padding(1);

        #endregion

        #region Private variables

        private GridPanel _Panel;
        private GridColumn _GridColumn;

        private PopupControl _PopupControl;

        private ItemPanel _ItemPanel;
        private ButtonItem _VisButtonItem;

        private FilterDateTimePicker _FilterDateTimePicker;

        private Control _Control;

        private string _FilterExpr;
        private object _FilterValue;
        private object _FilterDisplayValue;

        private IButtonControl _SaveButton;
        private PfnReset _PfnReset;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public FilterPopup(GridPanel panel)
        {
            _Panel = panel;

            _PopupControl = new PopupControl();

            _PopupControl.Opened += PopupControlOpened;
            _PopupControl.Closed += PopupControlClosed;

            _PopupControl.UserResize += PopupControlUserResize;

            _PopupControl.PreRenderGripBar += PopupControlPreRenderGripBar;
            _PopupControl.PostRenderGripBar += PopupControlPostRenderGripBar;
        }

        #region Private properties

        #region FilterDateTimePicker

        private FilterDateTimePicker FilterDateTimePicker
        {
            get
            {
                if (_FilterDateTimePicker == null)
                    FilterDateTimePicker = new FilterDateTimePicker(_GridColumn);

                return (_FilterDateTimePicker);
            }

            set
            {
                if (_FilterDateTimePicker != value)
                {
                    if (_FilterDateTimePicker != null)
                        _FilterDateTimePicker.ValueChanged -= FilterDateTimePickerValueChanged;

                    _FilterDateTimePicker = value;

                    if (_FilterDateTimePicker != null)
                        _FilterDateTimePicker.ValueChanged += FilterDateTimePickerValueChanged;
                }
            }
        }

        #endregion

        #region ItemPanel

        private ItemPanel ItemPanel
        {
            get
            {
                if (_ItemPanel == null)
                {
                    ItemPanel ip = new ItemPanel();
#if !TRIAL
                    ip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
#endif
                    ip.AutoScroll = true;
                    ip.FadeEffect = false;
                    ip.LayoutOrientation = eOrientation.Vertical;
                    ip.Style = eDotNetBarStyle.StyleManagerControlled;

                    ip.MinimumSize = new Size(80, 70);
                    ip.MaximumSize = new Size(1000, 1000);

                    ItemPanel = ip;
                }

                return (_ItemPanel);
            }

            set
            {
                if (_ItemPanel != value)
                {
                    if (_ItemPanel != null)
                        _ItemPanel.ItemClick -= ItemPanelClick;

                    _ItemPanel = value;

                    if (_ItemPanel != null)
                        _ItemPanel.ItemClick += ItemPanelClick;
                }
            }
        }

        #endregion

        #endregion

        #region Public properties

        #region Control

        /// <summary>
        /// Control
        /// </summary>
        public Control Control
        {
            get { return (_Control); }
            set { _Control = value; }
        }

        #endregion

        #region GripSize

        /// <summary>
        /// GripSize
        /// </summary>
        public Size GripSize
        {
            get { return (_PopupControl.GripSize); }
            set { _PopupControl.GripSize = value; }
        }

        #endregion

        #region Margin

        /// <summary>
        /// Margin
        /// </summary>
        public System.Windows.Forms.Padding Margin
        {
            get { return (_PopupControl.Margin); }
            set { _PopupControl.Margin = value; }
        }

        #endregion

        #region Padding

        /// <summary>
        /// Padding
        /// </summary>
        public System.Windows.Forms.Padding Padding
        {
            get { return (_PopupControl.Padding); }
            set { _PopupControl.Padding = value; }
        }

        #endregion

        #region ResizeMode

        /// <summary>
        /// Type of resize mode
        /// </summary>
        public PopupResizeMode ResizeMode
        {
            get { return (_PopupControl.ResizeMode); }
            set { _PopupControl.ResizeMode = value; }
        }

        #endregion

        #endregion

        #region Popup event processing

        #region PopupControlOpened

        private void PopupControlOpened(object sender, EventArgs e)
        {
            if (_Panel.SuperGrid.FindForm() is Office2007Form == false)
                _Panel.SuperGrid.PopupControl = _PopupControl;

            if (Control == _ItemPanel)
            {
                if (_VisButtonItem != null)
                    _ItemPanel.EnsureVisible(_VisButtonItem);
            }
            else
            {
                IButtonControl acceptButton = GetAcceptButton(Control);

                if (acceptButton != null)
                {
                    Form form1 = _Panel.SuperGrid.FindForm();

                    if (form1 != null)
                    {
                        _SaveButton = form1.AcceptButton;

                        form1.AcceptButton = acceptButton;
                    }
                }
            }
        }

        #endregion

        #region PopupControlClosed

        void PopupControlClosed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            FilterDateTimePicker ftp = Control as FilterDateTimePicker;

            if (ftp != null)
            {
                if (ftp.Cancel == true)
                {
                    _GridColumn.FilterExpr = _FilterExpr;
                    _GridColumn.FilterValue = _FilterValue;
                    _GridColumn.FilterDisplayValue = _FilterDisplayValue;
                }
            }

            if (_SaveButton != null)
            {
                Form form = _GridColumn.SuperGrid.FindForm();

                if (form != null)
                    form.AcceptButton = _SaveButton;

                _SaveButton = null;
            }

            _Panel.SuperGrid.PopupControl = null;
            _Panel.SuperGrid.GridCursor = Cursors.Default;

            if (_PfnReset != null)
                _PfnReset();

            _Panel.SuperGrid.PostInternalMouseMove();
            _Panel.SuperGrid.DoFilterPopupClosingEvent(_GridColumn, this);
        }

        #endregion

        #region PopupControlUserResize

        void PopupControlUserResize(object sender, EventArgs e)
        {
            _GridColumn.FilterPopupSize = Control.Size;
        }

        #endregion

        #endregion

        #region GetAcceptButton

        private IButtonControl GetAcceptButton(object control)
        {
            Type controlType = control.GetType();

            PropertyInfo property = controlType.GetProperty(
                "AcceptButton", BindingFlags.Instance | BindingFlags.Public);

            if (property != null)
            {
                if (property.PropertyType == typeof (IButtonControl))
                    return (IButtonControl) property.GetValue(control, null);
            }

            return (null);
        }

        #endregion

        #region PopupControlPreRenderGripBar

        private void PopupControlPreRenderGripBar(object sender, PreRenderGripBarEventArgs e)
        {
            e.Cancel = _GridColumn.SuperGrid.DoPreRenderFilterPopupGripBarEvent
                (e.Graphics, this, _GridColumn, e.Bounds);
        }

        #endregion

        #region PopupControlPostRenderGripBar

        private void PopupControlPostRenderGripBar(object sender, PostRenderGripBarEventArgs e)
        {
            _GridColumn.SuperGrid.DoPostRenderFilterPopupGripBarEvent
                (e.Graphics, this, _GridColumn, e.Bounds);
        }

        #endregion

        #region ActivatePopup

        internal void ActivatePopup(GridColumn column, PfnReset pfnReset)
        {
            Rectangle r = _Panel.ColumnHeader.GetScrollBounds(
                _Panel, column, column.FilterImageBounds);

            ActivatePopup(column, r, pfnReset);
        }

        internal void ActivatePopup(
            GridColumn column, Rectangle fiBounds, PfnReset pfnReset)
        {
            _GridColumn = column;
            _PfnReset = pfnReset;

            if (LoadFilterMenu() == false)
            {
                _FilterExpr = _GridColumn.FilterExpr;
                _FilterValue = _GridColumn.FilterValue;
                _FilterDisplayValue = _GridColumn.FilterDisplayValue;

                if (column.FilterPopupSize.IsEmpty == false)
                {
                    Control.Size = column.FilterPopupSize;
                }
                else
                {
                    if (Control.Size.IsEmpty == true)
                        Control.Size = Control.MinimumSize;
                }

                _Panel.SuperGrid.Cursor = Cursors.Default;

                PopupAnchor anchor;
                Point pt = GetPopupPoint(fiBounds, out anchor);

                _PopupControl.ResizeMode = anchor == PopupAnchor.Left
                    ? PopupResizeMode.BottomRight : PopupResizeMode.BottomLeft;

                FilterColumnHeaderVisualStyles styles = _GridColumn.EffectiveFilterStyles;

                _PopupControl.Background = styles.Default.GripBarBackground;

                _PopupControl.Show(Control, pt, anchor, _Panel.SuperGrid.FindForm());

                return;
            }
            
            if (_PfnReset != null)
                _PfnReset();

            _Panel.SuperGrid.PostInternalMouseMove();
            _Panel.SuperGrid.DoFilterPopupClosingEvent(_GridColumn, this);
        }

        #region LoadFilterMenu

        private bool LoadFilterMenu()
        {
            Control = null;

            Margin = _DefaultMargin;
            Padding = _DefaultPadding;
            GripSize = _DefaultGripSize;

            if (_Panel.SuperGrid.DoFilterPopupOpeningEvent(_GridColumn, this) == false)
            {
                if (Control == null)
                {
                    if (_Panel.SuperGrid.DoFilterPopupLoadEvent(_GridColumn, this) == false)
                    {
                        FilterEditType type = _GridColumn.GetFilterPanelType();

                        switch (type)
                        {
                            case FilterEditType.CheckBox:
                                LoadCheckBoxItems();
                                break;

                            case FilterEditType.ComboBox:
                                if (_GridColumn.FilterAutoScan == true)
                                    LoadDefaultItems();
                                else
                                    LoadComboItems();
                                break;

                            case FilterEditType.DateTime:
                                LoadDateTimeItems();
                                break;

                            default:
                                LoadDefaultItems();
                                break;
                        }

                        _Panel.SuperGrid.DoFilterPopupLoadedEvent(_GridColumn, this);
                    }

                    ItemPanel itemPanel = Control as ItemPanel;

                    if (itemPanel != null)
                        AdjustItemPanelSize(itemPanel);
                }

                return (false);
            }

            return (true);
        }

        #region LoadCustomFilterItems

        private void LoadCustomFilterItems(ItemPanel itemPanel)
        {
            bool firstItem = true;

            if (_GridColumn.SuperGrid.FilterUseExtendedCustomDialog == true)
            {
                List<UserFilterData> filterData =
                    FilterUserData.GetFilterData(_GridColumn.GridPanel);

                foreach (UserFilterData fd in filterData)
                {
                    if (fd.ReferNames.Contains(_GridColumn.Name) == true)
                    {
                        FilterPopupItem fpi = new
                            FilterPopupItem(fd.Name, fd.Name, FilterItemType.UserEntry, fd.Expression);

                        if (string.IsNullOrEmpty(fd.Description) == false)
                            fpi.Tooltip = fd.Description;

                        if (firstItem == true)
                        {
                            firstItem = false;

                            AddBaseItem(itemPanel, "Custom",
                                _GridColumn.SuperGrid.FilterCustomString, FilterItemType.Custom, "...");

                            int n = itemPanel.Items.Count - 1;

                            if (n > 0)
                                itemPanel.Items[n].BeginGroup = true;
                        }

                        if (fd.Expression.Equals(_GridColumn.FilterDisplayValue) == true)
                            fpi.FontBold = true;

                        itemPanel.Items.Add(fpi);
                    }
                }
            }

            if (firstItem == true)
            {
                AddBaseItem(itemPanel, "Custom",
                    _GridColumn.SuperGrid.FilterCustomString, FilterItemType.Custom, "...");
            }
        }

        #endregion

        #region LoadCheckBoxItems

        private void LoadCheckBoxItems()
        {
            FilterPopupItem[] items = null;

            CheckBoxX cb = _GridColumn.EditControl as CheckBoxX;

            if (cb != null)
            {
                items = new FilterPopupItem[(cb.ThreeState == true) ? 3 : 2];

                items[0] = new FilterPopupItem("CbEntry", "Checked",
                    FilterItemType.CheckBoxEntry, CheckState.Checked);

                items[1] = new FilterPopupItem("CbEntry", "Unchecked",
                    FilterItemType.CheckBoxEntry, CheckState.Unchecked);

                if (cb.ThreeState == true)
                {
                    items[2] = new FilterPopupItem("CbEntry", "Indeterminate",
                        FilterItemType.CheckBoxEntry, CheckState.Indeterminate);
                }
            }
            else if (_GridColumn.EditControl is GridSwitchButtonEditControl)
            {
                GridSwitchButtonEditControl sw =
                    (GridSwitchButtonEditControl)_GridColumn.EditControl;

                items = new FilterPopupItem[2];

                items[0] = new FilterPopupItem("SwEntry", sw.OnText,
                    FilterItemType.CheckBoxEntry, CheckState.Checked);

                items[1] = new FilterPopupItem("SwEntry", sw.OffText,
                    FilterItemType.CheckBoxEntry, CheckState.Unchecked);
            }

            ItemPanel itemPanel = GetBaseItemPanel();

            LoadCustomFilterItems(itemPanel);

            if (items != null)
            {
                if (itemPanel.Items.Count > 0)
                    items[0].BeginGroup = true;

                itemPanel.Items.AddRange(items);
            }

            Control = itemPanel;
        }

        #endregion

        #region LoadComboItems

        private void LoadComboItems()
        {
            _VisButtonItem = null;

            ComboBox cbx = _GridColumn.EditControl as ComboBox;

            if (cbx != null && cbx.Items.Count > 0)
            {
                FilterPopupItem[] items = new FilterPopupItem[cbx.Items.Count];

                for (int i = 0; i < cbx.Items.Count; i++)
                {
                    FilterPopupItem bi = new FilterPopupItem("CbxEntry",
                        cbx.GetItemText(cbx.Items[i]), FilterItemType.ComboBoxEntry,i);

                    items[i] = bi;

                    if (bi.Text.Equals(_GridColumn.FilterDisplayValue) == true)
                    {
                        _VisButtonItem = bi;

                        bi.FontBold = true;
                    }
                }

                ItemPanel itemPanel = GetBaseItemPanel();

                LoadCustomFilterItems(itemPanel);

                if (itemPanel.Items.Count > 0)
                {
                    items[0].BeginGroup = true;

                    itemPanel.Items.AddRange(items);
                }

                Control = itemPanel;
            }
        }

        #endregion

        #region LoadDateTimeItems

        private void LoadDateTimeItems()
        {
            IGridCellEditControl editor = _GridColumn.EditControl;

            if (editor != null && editor.EditorValueType == typeof (DateTime))
            {
                FilterDateTimePicker fdt = FilterDateTimePicker;

                fdt.Cancel = false;

                Control = fdt;
            }
        }

        #endregion

        #region LoadDefaultItems

        private void LoadDefaultItems()
        {
            _VisButtonItem = null;

            ItemPanel itemPanel = GetBaseItemPanel();

            LoadCustomFilterItems(itemPanel);

            if (_GridColumn.FilterAutoScan == true)
            {
                List<object> list = GetScanItems();

                if (list != null)
                {
                    int n = Math.Min(list.Count, _GridColumn.FilterPopupMaxItems);

                    for (int i = 0; i < n; i++)
                    {
                        object o = list[i];

                        FilterPopupItem fpi;

                        if (o is DateTime)
                        {
                            DateTime dt = (DateTime)o;
                            dt = dt.Date;

                            string s = dt.Date.ToShortDateString();

                            fpi = new FilterPopupItem(s, s, FilterItemType.ScanDateEntry, dt);
                        }
                        else
                        {
                            string s = o.ToString();

                            fpi = new FilterPopupItem(s, s, FilterItemType.ScanTextEntry, o);
                        }

                        if (i == 0)
                            fpi.BeginGroup = true;

                        if (fpi.Text.Equals(_GridColumn.FilterDisplayValue) == true)
                        {
                            fpi.FontBold = true;

                            _VisButtonItem = fpi;
                        }

                        itemPanel.Items.Add(fpi);
                    }
                }
            }

            Control = itemPanel;
        }

        #region GetScanItems

        private List<object> GetScanItems()
        {
            List<object> list = _GridColumn.ScanItems;

            if (list == null)
            {
                if (_GridColumn.NeedsFilterScan == true)
                {
                    _GridColumn.SuperGrid.Cursor = Cursors.AppStarting;

                    _GridColumn.NeedsFilterScan = false;
                    _GridColumn.FilterScan.BeginScan();

                    for (int i = 0; i < 500; i++)
                    {
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(20);

                        list = _GridColumn.ScanItems;

                        if (list != null)
                            break;
                    }

                    _GridColumn.SuperGrid.Cursor = Cursors.Default;
                }
            }

            return (list);
        }

        #endregion

        #endregion

        #region GetBaseItemPanel

        private ItemPanel GetBaseItemPanel()
        {
            ItemPanel itemPanel = ItemPanel;

            FilterColumnHeaderVisualStyle style = _GridColumn.GetFilterStyle(StyleType.Default);

            itemPanel.Font = style.Font;
            itemPanel.BackColor = style.Background.Color1;

            itemPanel.Items.Clear();

            AddBaseItem(itemPanel, "ShowAll",
                _GridColumn.SuperGrid.FilterShowAllString, FilterItemType.All, "");

            AddBaseItem(itemPanel, "ShowNull",
                _GridColumn.SuperGrid.FilterShowNullString, FilterItemType.Null, "");

            AddBaseItem(itemPanel, "ShowNotNull",
                _GridColumn.SuperGrid.FilterShowNotNullString, FilterItemType.NotNull, "");

            return (itemPanel);
        }

        #region AddBaseItem

        private void AddBaseItem(ItemPanel itemPanel,
            string name, string text, FilterItemType type, string ellipse)
        {
            if (string.IsNullOrEmpty(text) == false)
                itemPanel.Items.Add(new FilterPopupItem(name, text + ellipse, type));
        }

        #endregion

        #endregion

        #region AdjustItemPanelSize

        private void AdjustItemPanelSize(ItemPanel itemPanel)
        {
            if (_GridColumn.FilterPopupSize.IsEmpty == true)
            {
                int width = 0;
                int height = 0;

                if (itemPanel.Items.Count > 0)
                {
                    int w = 0;

                    foreach (BaseItem bi in itemPanel.Items)
                    {
                        bi.RecalcSize();

                        if (bi.WidthInternal > w)
                            w = bi.WidthInternal;
                    }

                    width = Math.Max(w + 
                        SystemInformation.VerticalScrollBarWidth * 2, _GridColumn.Size.Width);

                    height = itemPanel.Items.Count *
                        (itemPanel.Items[0].HeightInternal + itemPanel.ItemSpacing);
                }

                width = Math.Max(width, 90);
                height = Math.Max(Math.Min(height, _GridColumn.SuperGrid.FilterMaxDropDownHeight), 80);

                itemPanel.Size = new Size(width, height);
            }
        }

        #endregion

        #endregion

        #region GetPopupPoint

        private Point GetPopupPoint(Rectangle r, out PopupAnchor anchor)
        {
            Point pt;

            switch (_Panel.ColumnHeader.FilterImageAlignment)
            {
                case Alignment.TopRight:
                case Alignment.MiddleRight:
                case Alignment.BottomRight:
                    anchor = PopupAnchor.Right;
                    pt = new Point(r.Right + 2, r.Bottom + 4);
                    break;

                default:
                    anchor = PopupAnchor.Left;
                    pt = new Point(r.X - 2, r.Bottom + 4);
                    break;
            }

            pt = _Panel.SuperGrid.PointToScreen(pt);

            return (pt);
        }

        #endregion

        #endregion

        #region ItemPanelClick

        void ItemPanelClick(object sender, EventArgs e)
        {
            FilterPopupItem item = sender as FilterPopupItem;

            if (item != null)
            {
                object filterValue = null;
                object filterDisplayValue = null;
                string filterExpr = null;

                switch (item.FilterItemType)
                {
                    case FilterItemType.Null:
                        filterDisplayValue = item.Text;
                        filterExpr = "[" + _GridColumn.Name + "] = null";
                        break;

                    case FilterItemType.NotNull:
                        filterDisplayValue = item.Text;
                        filterExpr = "[" + _GridColumn.Name + "] != null";
                        break;

                    case FilterItemType.ComboBoxEntry:
                        filterExpr = ComboItemClick(item, ref filterValue, ref filterDisplayValue);
                        break;

                    case FilterItemType.CheckBoxEntry:
                        filterExpr = CheckBoxItemClick(item, ref filterValue, ref filterDisplayValue);
                        break;

                    case FilterItemType.Custom:
                        filterValue = _GridColumn.FilterValue;
                        filterDisplayValue = _GridColumn.FilterDisplayValue;
                        filterExpr = _GridColumn.FilterExpr;
                        break;

                    case FilterItemType.ScanTextEntry:
                        filterValue = item.Value;
                        filterDisplayValue = item.Value;
                        filterExpr = "= \"" + item.Value + "\"";
                        break;

                    case FilterItemType.ScanDateEntry:
                        filterValue = item.Value;
                        filterDisplayValue = ((DateTime)(item.Value)).ToShortDateString();
                        filterExpr = "Date([]) = #" + filterDisplayValue + "#";
                        break;

                    case FilterItemType.UserEntry:
                        filterDisplayValue = item.Value;
                        filterExpr = item.Value.ToString();
                        break;
                }

                if (_GridColumn.SuperGrid.DoFilterPopupValueChangedEvent(_GridColumn,
                    item.FilterItemType, ref filterValue, ref filterDisplayValue, ref filterExpr) == false)
                {
                    if (item.FilterItemType == FilterItemType.Custom)
                    {
                        LaunchCustomDialog(filterExpr);
                    }
                    else
                    {
                        _GridColumn.FilterExpr = filterExpr;
                        _GridColumn.FilterValue = filterValue;
                        _GridColumn.FilterDisplayValue = filterDisplayValue;
                    }
                }
            }

            _PopupControl.Hide();
        }

        #region ComboItemClick

        private string ComboItemClick(FilterPopupItem item,
            ref object filterValue, ref object filterDisplayValue)
        {
            ComboBox cbx = _GridColumn.EditControl as ComboBox;

            if (cbx != null)
            {
                cbx.SelectedIndex = (int)item.Value;

                string s1 = cbx.Text;

                if (cbx.DataSource != null)
                    s1 = cbx.SelectedValue.ToString();

                if (string.IsNullOrEmpty(s1) == false)
                {
                    filterValue = s1;
                    filterDisplayValue = item.Text;

                    return ("[" + _GridColumn.Name + "] = \"" + s1 + "\"");
                }
            }

            return (null);
        }

        #endregion

        #region CheckBoxItemClick

        private string CheckBoxItemClick(FilterPopupItem item,
            ref object filterValue, ref object filterDisplayValue)
        {
            CheckBoxX cb = _GridColumn.EditControl as CheckBoxX;

            if (cb != null)
            {
                string s1;

                switch ((CheckState)item.Value)
                {
                    case CheckState.Checked:
                        s1 = cb.CheckValueChecked != null
                            ? cb.CheckValueChecked.ToString() : "true";
                        break;

                    case CheckState.Unchecked:
                        s1 = cb.CheckValueUnchecked != null
                            ? cb.CheckValueUnchecked.ToString() : "false";
                        break;

                    default:
                        s1 = cb.CheckValueIndeterminate != null
                            ? cb.CheckValueIndeterminate.ToString() : "";
                        break;
                }

                filterValue = item.Value;
                filterDisplayValue = item.Text;

                return ("[" + _GridColumn.Name + "] = \"" + s1 + "\"");
            }
            
            if (_GridColumn.EditControl is GridSwitchButtonEditControl)
            {
                GridSwitchButtonEditControl sw =
                    (GridSwitchButtonEditControl)_GridColumn.EditControl;

                filterValue = item.Value;
                filterDisplayValue = item.Text;

                string s1;

                if ((CheckState)item.Value == CheckState.Checked)
                    s1 = sw.OnValue != null ? sw.OnValue.ToString() : "true";
                else
                    s1 = sw.OffValue != null ? sw.OffValue.ToString() : "false";

                return ("[" + _GridColumn.Name + "] = \"" + s1 + "\"");
            }

            return (null);
        }

        #endregion

        #region LaunchCustomDialog

        private void LaunchCustomDialog(string filterExpr)
        {
            Form form = _Panel.SuperGrid.FindForm();

            if (_Panel.SuperGrid.FilterUseExtendedCustomDialog == true)
            {
                CustomFilterEx cf = new CustomFilterEx(_Panel, _GridColumn, filterExpr);

                cf.Text = _GridColumn.Name;

                DialogResult dr = cf.ShowDialog(form);

                if (dr == DialogResult.OK)
                {
                    _GridColumn.FilterExpr = cf.FilterExpr;
                    _GridColumn.FilterValue = null;
                    _GridColumn.FilterDisplayValue = cf.FilterExpr;
                }
            }
            else
            {
                CustomFilter cf = new CustomFilter(_Panel, _GridColumn, filterExpr);

                cf.Text = _GridColumn.Name;

                DialogResult dr = cf.ShowDialog(form);

                if (dr == DialogResult.OK)
                {
                    _GridColumn.FilterExpr = cf.FilterExpr;
                    _GridColumn.FilterValue = null;
                    _GridColumn.FilterDisplayValue = cf.FilterExpr;
                }
            }

            _Panel.ColumnHeader.ResetColumnStateEx();

            _Panel.SuperGrid.Focus();
        }

        #endregion

        #endregion

        #region FilterDateTimePickerValueChanged

        void FilterDateTimePickerValueChanged(object sender, EventArgs e)
        {
            FilterDateTimePicker ftp = sender as FilterDateTimePicker;

            if (ftp != null)
            {
                string expr = ftp.GetFilterExpr();

                _GridColumn.FilterExpr = expr;
                _GridColumn.FilterValue = null;
                _GridColumn.FilterDisplayValue = expr;
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (_PopupControl != null)
            {
                Control = null;
                ItemPanel = null;

                _PopupControl.Opened -= PopupControlOpened;
                _PopupControl.Closed -= PopupControlClosed;

                _PopupControl.UserResize -= PopupControlUserResize;

                _PopupControl.PreRenderGripBar -= PopupControlPreRenderGripBar;
                _PopupControl.PostRenderGripBar -= PopupControlPostRenderGripBar;

                _PopupControl.Reset();
            }
        }

        #endregion
    }

    #region FilterPopupItem

    ///<summary>
    /// FilterPopupItem
    ///</summary>
    public class FilterPopupItem : ButtonItem
    {
        #region Private variables

        private object _Value;
        private FilterItemType _FilterItemType;

        #endregion

        #region Constructors

        ///<summary>
        /// FilterPopupItem
        ///</summary>
        ///<param name="name"></param>
        ///<param name="text"></param>
        ///<param name="filterItemType"></param>
        public FilterPopupItem(string name, string text, FilterItemType filterItemType)
            : this(name, text, filterItemType, null)
        {
        }

        ///<summary>
        /// FilterPopupItem
        ///</summary>
        ///<param name="name"></param>
        ///<param name="text"></param>
        ///<param name="filterItemType"></param>
        ///<param name="value"></param>
        public FilterPopupItem(string name,
            string text, FilterItemType filterItemType, object value)
            : base(name, text)
        {
            _Value = value;
            _FilterItemType = filterItemType;
        }

        #endregion

        #region Public properties

        ///<summary>
        /// Associated Value
        ///</summary>
        public object Value
        {
            get { return (_Value); }
            internal set { _Value = value; }
        }

        ///<summary>
        /// Associated FilterItem type
        ///</summary>
        public FilterItemType FilterItemType
        {
            get { return (_FilterItemType); }
            set { _FilterItemType = value; }
        }

        #endregion
    }

    #endregion

    #region Enums

    ///<summary>
    /// FilterItemType
    ///</summary>
    public enum FilterItemType
    {
        /// <summary>
        /// All
        /// </summary>
        All,

        ///<summary>
        /// Null
        ///</summary>
        Null,

        ///<summary>
        /// NotNull
        ///</summary>
        NotNull,

        ///<summary>
        /// Custom
        ///</summary>
        Custom,

        ///<summary>
        /// ComboBoxEntry
        ///</summary>
        ComboBoxEntry,

        ///<summary>
        /// ComboBoxEntry
        ///</summary>
        CheckBoxEntry,

        ///<summary>
        /// ScanTextEntry
        ///</summary>
        ScanTextEntry,

        ///<summary>
        /// ScanDateEntry
        ///</summary>
        ScanDateEntry,

        ///<summary>
        /// UserEntry
        ///</summary>
        UserEntry,
    }

    #endregion
}
