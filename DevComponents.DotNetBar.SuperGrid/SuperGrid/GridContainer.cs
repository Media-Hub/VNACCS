using System;
using System.ComponentModel;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid.Style;

namespace DevComponents.DotNetBar.SuperGrid
{
    /// <summary>
    /// Defines Grid element with Items collection
    /// </summary>
    public abstract class GridContainer : GridElement, IDisposable
    {
        #region Private variables

        private GridItemsCollection _Rows;

        private Cs _States;

        private int _Index;
        private int _FullIndex;
        private int _RowIndex;
        private int _GridIndex;
        private int _IndentLevel;

        private Rectangle _CheckBoxBounds;
        private Rectangle _ContainerBounds;

        private GridElement _MouseOverElement;

        private int _FixedRowHeight;
        private int _FixedHeaderHeight;
        private int _FirstOnScreenRowIndex;

        private int _DeleteUpdateCount;
        private int _SelectionUpdateCount;
        private int _StyleUpdateCount;

        private RowVisualStyles _RowStyles;
        private RowVisualStyles _EffectiveRowStyles;
        private CellVisualStyles _CellVisualStyles;

        private string _RowHeaderText = "";

        #endregion

        #region Abstract methods

        /// <summary>
        /// Creates the GridItemsCollection that hosts the items.
        /// </summary>
        /// <returns>New instance of GridItemsCollection.</returns>
        protected abstract GridItemsCollection CreateItemsCollection();

        #endregion

        #region Public properties

        #region Bounds

        ///<summary>
        /// Gets the scroll adjusted bounds
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Rectangle Bounds
        {
            get
            {
                Rectangle r = BoundsRelative;

                r.X -= HScrollOffset;

                if (IsVFrozen == false)
                    r.Y -= VScrollOffset;

                return (r);
            }
        }

        #endregion

        #region CellStyles

        /// <summary>
        /// Gets or sets the default Cell visual styles
        /// </summary>
        [Category("Style")]
        [Description("Indicates the default Cell visual styles.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public CellVisualStyles CellStyles
        {
            get
            {
                if (_CellVisualStyles == null)
                {
                    _CellVisualStyles = new CellVisualStyles();

                    UpdateChangeHandler(null, _CellVisualStyles);
                }

                return (_CellVisualStyles);
            }

            set
            {
                if (_CellVisualStyles != value)
                {
                    CellVisualStyles oldValue = _CellVisualStyles;
                    _CellVisualStyles = value;

                    OnStyleChanged("CellVisualStyles", oldValue, value);
                }
            }
        }

        #endregion

        #region Checked

        ///<summary>
        /// Gets or sets the row CheckBox checked state
        ///</summary>
        [DefaultValue(false), Category("Appearance")]
        [Description("Indicates the row CheckBox checked state.")]
        public bool Checked
        {
            get { return (TestState(Cs.Checked)); }

            set
            {
                if (Checked != value)
                {
                    SetState(Cs.Checked, value);

                    OnCheckedChanged();
                }
            }
        }

        #region OnCheckedChanged

        private void OnCheckedChanged()
        {
            GridPanel panel = GridPanel;

            if (panel != null && HasCheckBox == true)
            {
                Rectangle r = _CheckBoxBounds;

                if (panel.IsSubPanel == true ||
                    (panel.PrimaryColumn != null && panel.PrimaryColumn.IsHFrozen == false))
                {
                    r.X -= HScrollOffset;
                }

                if (IsVFrozen == false)
                    r.Y -= VScrollOffset;

                InvalidateRender(r);
            }

            OnPropertyChangedEx("Checked");
        }

        #endregion

        #endregion

        #region Expanded

        ///<summary>
        /// Gets or sets whether the row is
        /// expanded, permitting its child rows to be visible
        ///</summary>
        [DefaultValue(false), Category("Appearance")]
        [Description("Indicates whether the row is expanded, permitting its child rows to be visible.")]
        public bool Expanded
        {
            get { return (TestState(Cs.Expanded)); }

            set
            {
                if (Expanded != value)
                {
                    SetExpanded(value, ExpandSource.Expand);

                    OnPropertyChangedEx("Expanded", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region FirstOnScreenRow

        ///<summary>
        /// Gets the first visible row on screen
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridContainer FirstOnScreenRow
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    int index = FirstOnScreenRowIndex;

                    if (panel.VirtualMode == true)
                    {
                        if (index < panel.VirtualRowCountEx)
                            return (panel.VirtualRows[index]);
                    }
                    else
                    {
                        if (index < Rows.Count)
                        {
                            GridContainer row = panel.Rows[index] as GridContainer;

                            while (row != null)
                            {
                                if (row.Expanded == true && row.Rows.Count > 0)
                                {
                                    GridContainer roe =
                                        GetFirstOnScreenRow(row.Rows, SuperGrid.ViewRect);

                                    if (roe != null)
                                        return (roe);
                                }

                                if (row is GridGroup == false)
                                    return (row);

                                if (++index >= Rows.Count)
                                    break;

                                row = panel.Rows[index] as GridContainer;
                            }
                        }
                    }
                }

                return (null);
            }
        }

        #endregion

        #region FirstOnScreenRowIndex

        ///<summary>
        /// Gets the first visible on screen row index
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int FirstOnScreenRowIndex
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    if ((VScrollOffset == 0 && panel.IsFiltered == false) || 
                        (Parent != null && IsVFrozen == true) ||
                        (panel.VirtualMode == false && panel.IsSubPanel == false && panel.FrozenRowCount > 0))
                    {
                        _FirstOnScreenRowIndex = 0;
                    }
                    else
                    {
                        Rectangle r = SuperGrid.ViewRect;

                        _FirstOnScreenRowIndex = (panel.VirtualMode == true)
                             ? GetFirstOnScreenVirtualRowIndex(panel)
                             : GetFirstOnScreenRealRowIndex(r);
                    }
                }

                return (_FirstOnScreenRowIndex);
            }

            internal set { _FirstOnScreenRowIndex = value; }
        }

        #region GetFirstOnScreenVirtualRow

        private int GetFirstOnScreenVirtualRowIndex(GridPanel panel)
        {
            Rectangle r = BoundsRelative;

            r.Y += (FixedRowHeight -
                panel.FrozenRowCount * panel.VirtualRowHeight);

            int y = VScrollOffset;

            if (Parent == null)
                y += (panel.FrozenRowCount * panel.VirtualRowHeight);
            else
                y = (y - r.Y) + SuperGrid.PrimaryGrid.FixedRowHeight;

            int n = y / panel.VirtualRowHeight;

            return (n > 0 ? n : 0);
        }

        #endregion

        #region GetFirstOnScreenRealRowIndex

        private int GetFirstOnScreenRealRowIndex(Rectangle r)
        {
            int lo = 0;
            int hi = Rows.Count - 1;

            while (lo < hi)
            {
                int mid = (lo + hi) / 2;

                GridElement item = Rows[mid];
                Rectangle t = item is GridPanel ? ((GridPanel)item).ContainerBounds : item.BoundsRelative;

                if (item.IsVFrozen == false)
                    t.Y -= VScrollOffset;

                if (t.Bottom > r.Y)
                {
                    if (t.Y <= r.Y)
                    {
                        if (item.Visible == true)
                            return (mid);
                    }

                    hi = mid - 1;
                }
                else
                {
                    lo = mid + 1;
                }
            }

            return (lo);
        }

        #endregion

        #endregion

        #region GetFirstOnScreenRow

        private GridContainer
            GetFirstOnScreenRow(GridItemsCollection items, Rectangle t)
        {
            for (int i = 0; i < items.Count; i++)
            {
                GridContainer item = items[i] as GridContainer;

                if (item != null)
                {
                    Rectangle r = item.BoundsRelative;

                    if (item.IsVFrozen == false)
                        r.Y -= VScrollOffset;

                    if (r.Bottom > t.Top)
                    {
                        if (item.Expanded == true && item.Rows.Count > 0)
                        {
                            GridContainer row = GetFirstOnScreenRow(item.Rows, t);

                            return (row ?? item);
                        }

                        return (item);
                    }
                }
            }

            return (null);
        }

        #endregion

        #region FirstSelectableRow

        ///<summary>
        /// Gets the first selectable row
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridContainer FirstSelectableRow
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    if (panel.VirtualMode == true)
                    {
                        if (panel.VirtualRowCountEx > 0)
                            return (panel.VirtualRows[0]);
                    }
                    else
                    {
                        foreach (GridContainer row in panel.Rows)
                        {
                            if (row.Visible == true && row.AllowSelection == true)
                                return (row);
                        }
                    }
                }

                return (null);
            }
        }

        #endregion

        #region FirstVisibleRow

        ///<summary>
        /// Gets the first visible row
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridContainer FirstVisibleRow
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    if (panel.VirtualMode == true)
                    {
                        if (panel.VirtualRowCountEx > 0)
                            return (panel.VirtualRows[0]);
                    }
                    else
                    {
                        foreach (GridContainer row in panel.Rows)
                        {
                            if (row.Visible == true)
                                return (row);
                        }
                    }
                }

                return (null);
            }
        }

        #endregion

        #region FullIndex

        ///<summary>
        /// Gets the sequential index for the row
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int FullIndex
        {
            get
            {
                CheckIndicees();

                return (_FullIndex);
            }

            internal set { _FullIndex = value; }
        }

        #endregion

        #region GridIndex

        ///<summary>
        /// Gets the sequential index for the visible, expanded row
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int GridIndex
        {
            get
            {
                CheckIndicees();

                return (_GridIndex); 
            }

            internal set { _GridIndex = value; }
        }

        #endregion

        #region Index

        ///<summary>
        /// Gets the sequential, visible, local container index for the item
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Index
        {
            get
            {
                CheckIndicees();
                
                return (_Index);
            }

            internal set { _Index = value; }
        }

        #endregion

        #region IsActive

        ///<summary>
        /// Gets or sets whether the item is Active
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsActive
        {
            get { return (this == SuperGrid.ActiveRow); }
        }

        #endregion

        #region IsDeleted

        ///<summary>
        /// Gets whether the item is deleted
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsDeleted
        {
            get
            {
                GridPanel panel = GetParentPanel();

                if (panel != null)
                {
                    if (_DeleteUpdateCount != panel.DeleteUpdateCount)
                    {
                        SetState(Cs.Deleted, panel.IsRowDeleted(this));
                        _DeleteUpdateCount = panel.DeleteUpdateCount;
                    }
                }
                    
                return (TestState(Cs.Deleted));
            }

            set
            {
                SetState(Cs.Deleted, value);

                GridPanel panel = GetParentPanel();

                if (panel != null)
                {
                    if (panel.SetDeleted(this, value) == true)
                        _DeleteUpdateCount = panel._DeleteUpdateCount;

                }
            }
        }

        #endregion

        #region IsExpandedVisible

        ///<summary>
        /// Gets whether the item is visible
        /// and its parental hierarchy is expanded
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsExpandedVisible
        {
            get
            {
                if (Visible == false)
                    return (false);

                GridContainer row = Parent as GridContainer;

                while (row != null)
                {
                    if (row is GridRow)
                    {
                        if (row.Visible == false || row.Expanded == false)
                            return (false);
                    }

                    row = row.Parent as GridContainer;
                }

                return (true);
            }
        }

        #endregion

        #region IsOnScreen

        ///<summary>
        /// Gets whether the item is visible on screen
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsOnScreen
        {
            get
            {
                if (Visible == true)
                {
                    GridPanel panel = Parent as GridPanel;

                    if (panel != null)
                    {
                        Rectangle t = SViewRect;
                        Rectangle bounds = Bounds;

                        bounds.Width = panel.ColumnHeader.BoundsRelative.Width;

                        return (t.IntersectsWith(bounds));
                    }
                }

                return (false);
            }
        }

        #endregion

        #region IsSelectable

        ///<summary>
        /// Gets whether the row can be selected by the user.
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSelectable
        {
            get { return (AllowSelection == true); }
        }

        #endregion

        #region IsSelected

        ///<summary>
        /// Gets or sets whether the item is selected
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSelected
        {
            get
            {
                if (AllowSelection == true)
                {
                    GridPanel panel = GetParentPanel();

                    if (panel != null)
                    {
                        if (_SelectionUpdateCount != panel.SelectionUpdateCount)
                        {
                            SetState(Cs.Selected, panel.IsItemSelected(this));
                            _SelectionUpdateCount = panel.SelectionUpdateCount;
                        }

                        return (TestState(Cs.Selected));
                    }
                }

                return (false);
            }

            set
            {
                GridPanel panel = GetParentPanel();

                if (panel != null)
                {
                    if (panel.SetSelected(this, value) == true)
                    {
                        SetState(Cs.Selected, value);
                        _SelectionUpdateCount = panel.SelectionUpdateCount;

                        Rectangle r = ContainerBounds;

                        r.X -= HScrollOffset;

                        if (IsVFrozen == false)
                            r.Y -= VScrollOffset;

                        InvalidateRender(r);
                    }
                }
            }
        }

        #endregion

        #region LastOnScreenRow

        ///<summary>
        /// Gets the last visible on screen row
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridContainer LastOnScreenRow
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    if (panel.VirtualMode == true)
                    {
                        int index = LastOnScreenRowIndex;

                        if (index < panel.VirtualRowCountEx)
                            return (panel.VirtualRows[index]);
                    }
                    else
                    {
                        if (IsExpandedVisible == true)
                            return (GetLastOnScreenRow(panel.Rows, SuperGrid.ViewRect));
                    }
                }

                return (null);
            }
        }

        #region GetLastOnScreenRow

        private GridContainer
            GetLastOnScreenRow(GridItemsCollection items, Rectangle t)
        {
            for (int i = 0; i < items.Count; i++)
            {
                GridContainer item = items[i] as GridContainer;

                if (item != null)
                {
                    Rectangle r = item.BoundsRelative;

                    if (item.IsVFrozen == false)
                        r.Y -= VScrollOffset;

                    if (r.Bottom >= t.Bottom || i == items.Count - 1)
                    {
                        if (item.Expanded == true && item.Rows.Count > 0)
                        {
                            GridContainer row = GetLastOnScreenRow(item.Rows, t);

                            return (row ?? item);
                        }

                        return (item);
                    }
                }
            }

            return (null);
        }

        #endregion

        #endregion

        #region LastOnScreenRowIndex

        ///<summary>
        /// Gets the last visible on screen row index
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int LastOnScreenRowIndex
        {
            get
            {
                if (IsExpandedVisible == true)
                {
                    GridPanel panel = GridPanel;

                    if (panel != null)
                    {
                        Rectangle t = SuperGrid.ViewRect;

                        return (panel.VirtualMode == true)
                                   ? (GetLastOnScreenVirtualRowIndex(panel, t))
                                   : (GetLastOnScreenRealRowIndex(panel, t));
                    }
                }

                return (0);
            }
        }

        #region GetLastOnScreenVirtualRowIndex

        private int GetLastOnScreenVirtualRowIndex(
            GridPanel panel, Rectangle t)
        {
            int index = FirstOnScreenRowIndex;
            GridRow row = panel.VirtualRows[index];

            Rectangle r = row.BoundsRelative;
            r.Y -= VScrollOffset;

            int n = (t.Bottom - r.Top + panel.VirtualRowHeight - 1) / panel.VirtualRowHeight;

            return (Math.Min(panel.VisibleRowCount - 1, index + n - 1));
        }

        #endregion

        #region GetLastOnScreenRealRowIndex

        private int GetLastOnScreenRealRowIndex(GridPanel panel, Rectangle t)
        {
            int index = FirstOnScreenRowIndex;

            while (index + 1 < Rows.Count)
            {
                index++;

                GridContainer item = (GridContainer)panel.Rows[index];
                Rectangle r = item.BoundsRelative;

                if (item.IsVFrozen == false)
                    r.Y -= VScrollOffset;

                if (r.Bottom >= t.Bottom)
                    return (index);
            }

            return (index);
        }

        #endregion

        #endregion

        #region LastSelectableRow

        ///<summary>
        /// Gets the last user selectable row
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridContainer LastSelectableRow
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    if (panel.VirtualMode == true)
                    {
                        if (panel.VirtualRowCountEx > 0)
                            return (panel.VirtualRows[panel.VirtualRowCountEx - 1]);
                    }
                    else
                    {
                        for (int i = panel.Rows.Count - 1; i >= 0; i--)
                        {
                            GridContainer row = panel.Rows[i] as GridContainer;

                            if (row != null)
                            {
                                if (row.Visible == true && row.AllowSelection == true)
                                    return (row);
                            }
                        }
                    }
                }

                return (null);
            }
        }

        #endregion

        #region LastVisibleRow

        ///<summary>
        /// Gets the last visible row
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridContainer LastVisibleRow
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    if (panel.VirtualMode == true)
                    {
                        if (panel.VirtualRowCountEx > 0)
                            return (panel.VirtualRows[panel.VirtualRowCountEx - 1]);
                    }
                    else
                    {
                        for (int i = panel.Rows.Count - 1; i >= 0;  i--)
                        {
                            GridContainer row = panel.Rows[i] as GridContainer;

                            if (row != null)
                            {
                                if (row.Visible == true)
                                    return (row);
                            }
                        }
                    }
                }

                return (null);
            }
        }

        #endregion

        #region NextVisibleRow

        ///<summary>
        /// Gets the next visible row, or null if no
        /// subsequent rows are defined
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridContainer NextVisibleRow
        {
            get
            {
                GridPanel panel = GetParentPanel();

                if (panel != null)
                {
                    int index = GridIndex + 1;

                    GridElement item = panel.GetRowFromIndex(index);

                    while (item != null)
                    {
                        if (ItemIsSelectable(panel, item) == true)
                            return ((GridContainer) item);

                        item = panel.GetRowFromIndex(++index);
                    }
                }

                return (null);
            }
        }

        private bool ItemIsSelectable(GridPanel panel, GridElement item)
        {
            if (item.Visible == false)
                return (false);
            
            if (item is GridGroup && panel.GroupHeaderKeyBehavior != GroupHeaderKeyBehavior.Select)
                return (false);

            return (item is GridContainer);
        }

        #endregion

        #region PrevVisibleRow

        ///<summary>
        /// Gets the previous visible row, or null if
        /// no previous rows are defined
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridContainer PrevVisibleRow
        {
            get
            {
                GridPanel panel = GetParentPanel();

                if (panel != null)
                {
                    int index = GridIndex;

                    while (--index >= 0)
                    {
                        GridElement item = panel.GetRowFromIndex(index);

                        if (item == null)
                            break;

                        if (ItemIsSelectable(panel, item) == true)
                            return ((GridContainer)item);
                    }
                }

                return (null);
            }
        }

        #endregion

        #region ReadOnly

        /// <summary>
        /// Gets or sets whether the user can change the row contents
        /// </summary>
        [DefaultValue(false), Category("Behavior")]
        [Description("Indicates whether the user can change the row contents.")]
        public bool ReadOnly
        {
            get { return (TestState(Cs.ReadOnly)); }

            set
            {
                if (value != ReadOnly)
                {
                    SetState(Cs.ReadOnly, value);

                    OnPropertyChangedEx("ReadOnly", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region RowHeaderText

        ///<summary>
        /// Gets or sets the associated row header text.
        ///</summary>
        [DefaultValue(""), Category("Appearance")]
        [Description("Indicates the associated row header text.).")]
        public string RowHeaderText
        {
            get { return (_RowHeaderText); }

            set
            {
                if (_RowHeaderText != value)
                {
                    _RowHeaderText = value;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("RowHeaderText", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region RowIndex

        ///<summary>
        /// Gets the sequential, parental index of the item
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int RowIndex
        {
            get
            {
                CheckIndicees();

                return (_RowIndex);
            }

            internal set { _RowIndex = value; }
        }

        #region CheckIndicees

        private void CheckIndicees()
        {
            GridPanel panel = GridPanel;

            if (panel != null)
                panel.UpdateIndicees(panel, panel.Rows, true);
        }

        #endregion

        #endregion

        #region Rows

        /// <summary>
        /// Gets the reference to the Rows collection
        /// </summary>
        [DefaultValue(null), Category("Data")]
        [Description("Indicates the reference to the Rows collection.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual GridItemsCollection Rows
        {
            get
            {
                if (_Rows == null)
                    _Rows = CreateItemsCollection();

                return (_Rows);
            }
        }

        #endregion

        #region RowsUnresolved

        ///<summary>
        /// Gets or sets whether the rows collection is unknown and has not
        /// been resolved (presumes there are rows until set to false)
        ///</summary>
        [DefaultValue(false), Category("Data")]
        [Description("Indicates whether the rows collection is unresolved (presumes there are rows until set to false).")]
        public bool RowsUnresolved
        {
            get { return (TestState(Cs.RowsUnresolved)); }

            set
            {
                if (RowsUnresolved != value)
                {
                    SetState(Cs.RowsUnresolved, value);

                    OnPropertyChangedEx("RowsUnresolved", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region RowStyles

        /// <summary>
        /// Gets or sets the visual styles assigned to the row elements
        /// </summary>
        [Category("Style")]
        [Description("Indicates visual style assigned to the row elements")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public RowVisualStyles RowStyles
        {
            get
            {
                if (_RowStyles == null)
                {
                    _RowStyles = new RowVisualStyles();

                    UpdateChangeHandler(null, _RowStyles);
                }

                return (_RowStyles);
            }

            set
            {
                if (_RowStyles != value)
                {
                    RowVisualStyles oldValue = _RowStyles;
                    _RowStyles = value;

                    OnStyleChanged("RowStyles", oldValue, value);
                }
            }
        }

        #endregion

        #region ShowCheckBox

        /// <summary>
        /// Get or sets whether the row CheckBox is shown (Panel.CheckBoxes must also be true)
        /// </summary>
        [DefaultValue(true), Category("Appearance")]
        [Description("Indicates whether the row CheckBox is shown (Panel.CheckBoxes must also be true).")]
        public bool ShowCheckBox
        {
            get { return (TestState(Cs.ShowCheckBox)); }

            set
            {
                if (ShowCheckBox != value)
                {
                    SetState(Cs.ShowCheckBox, value);

                    NeedsMeasured = true;

                    OnPropertyChangedEx("ShowCheckBox", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region ShowTreeButton

        /// <summary>
        /// Gets or sets whether expand / collapse
        /// buttons are shown if the row contains nested items 
        /// </summary>
        [DefaultValue(true), Category("Appearance")]
        [Description("Indicates whether Tree expand / collapse buttons are shown if the row contains nested items.")]
        public bool ShowTreeButton
        {
            get { return (TestState(Cs.ShowTreeButton)); }

            set
            {
                if (ShowTreeButton != value)
                {
                    SetState(Cs.ShowTreeButton, value);

                    OnPropertyChangedEx("ShowTreeButton", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #endregion

        #region Constructors

        protected GridContainer()
        {
            SetState(Cs.ShowCheckBox, true);
            SetState(Cs.ShowTreeButton, true);
        }

        #endregion

        #region Internal properties

        #region CanModify

        internal bool CanModify
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    if (panel.ReadOnly == true)
                        return (false);
                }

                return (ReadOnly == false && IsDeleted == false);
            }
        }

        #endregion

        #region CheckBoxBounds

        internal Rectangle CheckBoxBounds
        {
            get { return (_CheckBoxBounds); }
            set { _CheckBoxBounds = value; }
        }

        #endregion

        #region ContainerBounds

        internal Rectangle ContainerBounds
        {
            get { return (_ContainerBounds); }
            set { _ContainerBounds = value; }
        }

        #endregion

        #region FixedHeaderHeight

        /// <summary>
        /// Get or sets the FixedHeaderHeight
        /// </summary>
        internal int FixedHeaderHeight
        {
            get { return (_FixedHeaderHeight); }
            set { _FixedHeaderHeight = value; }
        }

        #endregion

        #region FixedRowHeight

        /// <summary>
        /// Get or sets the FixedRowHeight
        /// </summary>
        internal int FixedRowHeight
        {
            get { return (_FixedRowHeight); }
            set { _FixedRowHeight = value; }
        }

        #endregion

        #region HasCheckBox

        internal bool HasCheckBox
        {
            get
            {
                GridPanel ppanel = GetParentPanel();

                if (ppanel != null)
                {
                    if (ppanel.CheckBoxes != true)
                        return (false);
                }

                return (ShowCheckBox);
            }
        }

        #endregion

        #region HasVisibleItems

        internal bool HasVisibleItems
        {
            get { return (TestState(Cs.HasVisibleItems)); }
            set { SetState(Cs.HasVisibleItems, value); }
        }

        #endregion

        #region IndentLevel

        /// <summary>
        /// Get or sets the panel indent level
        /// </summary>
        internal int IndentLevel
        {
            get { return (_IndentLevel); }
            set { _IndentLevel = value; }
        }

        #endregion

        #region IsActiveRow

        internal bool IsActiveRow
        {
            get
            {
                GridPanel panel = GridPanel;

                return (panel.ActiveRow != null &&
                    panel.ActiveRow.GridIndex == GridIndex);
            }
        }

        #endregion

        #region MaxRowIndex

        internal int MaxRowIndex
        {
            get
            {
                GridPanel panel = this as GridPanel;

                if (panel != null && panel.VirtualMode == true)
                    return (panel.VirtualRowCountEx - 1);

                return (Rows.Count - 1);
            }
        }

        #endregion

        #region NeedsFilterScan

        /// <summary>
        /// Get or sets whether the container needs filter scanned
        /// </summary>
        internal bool NeedsFilterScan
        {
            get { return (TestState(Cs.NeedsFilterScan)); }
            set { SetState(Cs.NeedsFilterScan, value); }
        }

        #endregion

        #region NeedsGrouped

        /// <summary>
        /// Get or sets whether the container needs grouped
        /// </summary>
        internal bool NeedsGrouped
        {
            get { return (TestState(Cs.NeedsGrouped)); }
            set { SetState(Cs.NeedsGrouped, value); }
        }

        #endregion

        #region NeedsGroupSorted

        /// <summary>
        /// Get or sets whether the container groups need sorted
        /// </summary>
        internal bool NeedsGroupSorted
        {
            get { return (TestState(Cs.NeedsGroupSorted)); }
            set { SetState(Cs.NeedsGroupSorted, value); }
        }

        #endregion

        #region NeedsSorted

        /// <summary>
        /// Get or sets whether the container sorted
        /// </summary>
        internal bool NeedsSorted
        {
            get { return (TestState(Cs.NeedsSorted)); }
            set { SetState(Cs.NeedsSorted, value); }
        }

        #endregion

        #endregion

        #region TestState

        private bool TestState(Cs state)
        {
            return ((_States & state) == state);
        }

        #endregion

        #region SetState

        private void SetState(Cs state, bool value)
        {
            if (value == true)
                _States |= state;
            else
                _States &= ~state;
        }

        #endregion

        #region GetVisibleItemCount

        internal int GetVisibleItemCount()
        {
            int n = 0;

            if (Expanded == true)
            {
                foreach (GridElement item in Rows)
                {
                    if (item.Visible == true)
                    {
                        n++;

                        GridRow row = item as GridRow;

                        if (row != null)
                        {
                            if (row.Rows.Count > 0 && row.Expanded == true)
                                n += row.GetVisibleItemCount();
                        }
                    }
                }
            }

            return (n);
        }

        #endregion

        #region GetNextItem

        internal GridContainer GetNextItem()
        {
            GridContainer container = Parent as GridContainer;

            if (container != null)
            {
                GridPanel panel = container as GridPanel;

                if (panel != null && panel.VirtualMode == true)
                {
                    GridRow row = this as GridRow;

                    if (row != null)
                    {
                        int n = row.GridIndex + 1;

                        if (n < panel.VirtualRowCountEx)
                            return (panel.VirtualRows[n]);
                    }
                }
                else
                {
                    int index = RowIndex;

                    if (index >= 0)
                    {
                        for (int i = index + 1; i < container.Rows.Count; i++)
                        {
                            GridContainer item = container.Rows[i] as GridContainer;

                            if (item != null && item.Visible == true)
                                return (item);
                        }
                    }
                }
            }

            return (null);
        }

        #endregion

        #region GetPrevItem

        internal GridContainer GetPrevItem()
        {
            GridContainer container = Parent as GridContainer;

            if (container != null)
            {
                GridPanel panel = container as GridPanel;

                if (panel != null && panel.VirtualMode == true)
                {
                    GridRow row = this as GridRow;
                    if (row != null)
                    {
                        int n = row.GridIndex - 1;

                        if (n >= 0)
                            return (panel.VirtualRows[n]);
                    }
                }
                else
                {
                    for (int i = Index - 1; i >= 0; i--)
                    {
                        GridContainer item = container.Rows[i] as GridContainer;

                        if (item != null && item.Visible == true)
                            return (item);

                    }
                }
            }

            return (null);
        }

        #endregion

        #region FindGridPanel

        ///<summary>
        /// Finds the GridPanel with the given Name.
        /// Only the root Rows collection is searched.
        ///</summary>
        ///<returns>GridPanel, or null if not found.</returns>
        public GridPanel FindGridPanel(string name)
        {
            return (FindGridPanel(name, false));
        }

        ///<summary>
        /// Finds the GridPanel with the given Name.
        /// Nested Rows are searched if 'includeNested' is true.
        ///</summary>
        ///<param name="name">Name to search</param>
        ///<param name="includeNested">Whether to include nested rows in the search.</param>
        ///<returns>GridPanel, or null if not found.</returns>
        public GridPanel FindGridPanel(string name, bool includeNested)
        {
            GridItemsCollection items = Rows;

            foreach (GridElement item in items)
            {
                GridPanel panel = item as GridPanel;

                if (panel != null)
                {
                    if (panel.Name != null && panel.Name.Equals(name))
                        return (panel);
                }
            }

            if (includeNested == true)
            {
                foreach (GridElement item in items)
                {
                    GridContainer container = item as GridContainer;

                    if (container != null)
                    {
                        GridPanel panel = container.FindGridPanel(name, true);

                        if (panel != null)
                            return (panel);
                    }
                }
            }

            return (null);
        }

        #endregion

        #region SetExpanded

        private void SetExpanded(bool value, ExpandSource source)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                panel.FlushActiveRow();

                if (Expanded == false)
                {
                    bool dynanic = RowsUnresolved;

                    if (SuperGrid == null ||
                        SuperGrid.DoBeforeExpandEvent(panel, this, source) == false)
                    {
                        ProcessExpandChange(panel, value);

                        if (dynanic == true && RowsUnresolved == false)
                            NeedsSorted = true;

                        if (SuperGrid != null)
                            SuperGrid.DoAfterExpandEvent(panel, this, source);
                    }
                }
                else
                {
                    if (SuperGrid == null ||
                        SuperGrid.DoBeforeCollapseEvent(panel, this, source) == false)
                    {
                        ProcessExpandChange(panel, value);

                        if (SuperGrid != null)
                            SuperGrid.DoAfterCollapseEvent(panel, this, source);
                    }
                }
            }
            else
            {
                SetState(Cs.Expanded, value);
            }
        }

        #region ProcessExpandChange

        private void ProcessExpandChange(GridPanel panel, bool value)
        {
            if (value == true)
                SetState(Cs.Expanded, true);

            int index = GridIndex;
            int count = GetVisibleItemCount();

            panel.ExpandSelectedAtIndex(index + 1, count, value);

            if (value == false)
                SetState(Cs.Expanded, false);
        }

        #endregion

        #endregion

        #region ExpandItemTree

        /// <summary>
        /// Expands the entire tree, as necessary, to display the given row.
        /// </summary>
        /// <returns>'true' if any expanding was actually performed</returns>
        public bool ExpandItemTree()
        {
            GridContainer row = Parent as GridContainer;

            bool expandChanged = false;

            while (row != null)
            {
                if (row is GridRow || row is GridGroup)
                {
                    if (row.Expanded == false)
                    {
                        if (row.Rows.Count > 0 || row.RowsUnresolved == true)
                        {
                            row.Expanded = true;

                            expandChanged = true;
                        }
                    }
                }

                row = row.Parent as GridContainer;
            }

            return (expandChanged);
        }

        #endregion

        #region ExpandAll

        ///<summary>
        /// Expands all collapsed (or unexpanded) rows.
        ///</summary>
        public void ExpandAll()
        {
            ExpandAll(-1, true);

            InvalidateLayout();
        }

        ///<summary>
        /// Expands all collapsed (or unexpanded) rows. If a depth
        /// is provided, then rows will be expanded only to the
        /// given nested depth. In other words, to expand only
        /// the first level of rows in the container, you would
        /// call ExpandAll(0). To collapse the first level – as
        /// well as 2 levels under it, you would call CollapseAll(2).
        ///</summary>
        public void ExpandAll(int depth)
        {
            ExpandAll(depth, true);

            InvalidateLayout();
        }

        private void ExpandAll(int depth, bool expand)
        {
            if (Rows.Count > 0)
            {
                SuperGrid.BeginUpdate();

                foreach (GridElement item in Rows)
                {
                    GridContainer row = item as GridContainer;

                    if (row != null)
                    {
                        if (row.Expanded != expand)
                            row.SetExpanded(expand, ExpandSource.ExpandAll);

                        if (depth != 0)
                            row.ExpandAll(depth - 1, expand);
                    }
                }

                SuperGrid.EndUpdate();
            }
        }

        #endregion

        #region CollapseAll

        ///<summary>
        /// Collapses all expanded rows.
        ///</summary>
        public void CollapseAll()
        {
            ExpandAll(-1, false);

            InvalidateLayout();
        }

        ///<summary>
        /// Collapses all expanded rows. If a depth is provided,
        /// then rows will be collapsed only to the given nested
        /// depth.  In other words, to collapse only the first
        /// level of rows in the container, you would call CollapseAll(0).
        /// To collapse the first level – as well as 2 levels under
        /// it, you would call CollapseAll(2).

        ///</summary>
        public void CollapseAll(int depth)
        {
            ExpandAll(depth, false);

            InvalidateLayout();
        }

        #endregion

        #region SetActive

        ///<summary>
        /// Makes the given row active
        ///</summary>
        ///<returns>true, if successful</returns>
        public bool SetActive()
        {
            return (SetActive(false));
        }

        ///<summary>
        /// Makes the given row active and
        /// optionally selects the given row
        ///</summary>
        ///<returns>true, if successful</returns>
        public bool SetActive(bool select)
        {
            if (GridPanel.SetActiveRow(this) == true)
            {
                if (select == true)
                {
                    GridPanel.ClearAll();

                    IsSelected = true;
                }

                return (true);
            }

            return (false);
        }

        #endregion

        #region EnsureVisible

        ///<summary>
        /// This routine will ensure that the row is visible on
        /// the screen.  If the row is not visible on screen, the
        /// view window will be scrolled to make it visible.  
        ///</summary>
        ///<param name="center">If 'true', the row will be centered on screen</param>
        public override void EnsureVisible(bool center)
        {
            if (Visible == true)
            {
                GridPanel panel = GridPanel;

                if (ExpandItemTree() == true || FixedRowHeight <= 0 || panel.IsLayoutValid == false)
                    SuperGrid.ArrangeGrid();

                Rectangle t = SViewRect;
                Rectangle bounds = BoundsRelative;

                int fixedHeight = FixedRowHeight;

                if (panel.VirtualMode == true)
                {
                    bounds = panel.BoundsRelative;

                    bounds.Y += (panel.FixedRowHeight - (panel.FrozenRowCount*panel.VirtualRowHeight));
                    bounds.Y += (GridIndex * panel.VirtualRowHeight);

                    if (this is GridRow)
                        fixedHeight = panel.VirtualRowHeight;
                    else
                        fixedHeight = 0;
                }

                if (fixedHeight >= t.Height || bounds.Y - VScrollOffset < t.Y)
                {
                    int n = bounds.Y - t.Y;

                    if (center == false || fixedHeight >= t.Height)
                        SuperGrid.SetVScrollValue(n);
                    else
                        SuperGrid.SetVScrollValue(n - (t.Height - fixedHeight) / 2);
                }
                else if (bounds.Y - VScrollOffset > t.Bottom - fixedHeight)
                {
                    int n = bounds.Y - (t.Bottom - fixedHeight);

                    if (center == false)
                        SuperGrid.SetVScrollValue(n);
                    else
                        SuperGrid.SetVScrollValue(n + (t.Height - fixedHeight) / 2);
                }
            }
        }

        #endregion

        #region ScrollToTop

        internal void ScrollToTop()
        {
            if (Visible == true)
            {
                if (IsVFrozen == false)
                {
                    if (ExpandItemTree() == true)
                        SuperGrid.ArrangeGrid();

                    Rectangle t = SViewRect;
                    Rectangle bounds = BoundsRelative;

                    GridPanel panel = GridPanel;

                    if (panel.VirtualMode == true)
                    {
                        bounds = panel.BoundsRelative;

                        bounds.Y += (panel.FixedRowHeight - (panel.FrozenRowCount * panel.VirtualRowHeight));
                        bounds.Y += (GridIndex * panel.VirtualRowHeight);
                    }

                    int n = bounds.Y - t.Y;

                    SuperGrid.SetVScrollValue(n);
                }
            }
        }

        #endregion

        #region ScrollToBottom

        internal void ScrollToBottom()
        {
            if (Visible == true)
            {
                if (IsVFrozen == false)
                {
                    ExpandItemTree();
                    SuperGrid.ArrangeGrid();

                    Rectangle t = SViewRect;
                    Rectangle bounds = BoundsRelative;

                    GridPanel panel = GridPanel;

                    if (panel.VirtualMode == true)
                    {
                        bounds = panel.BoundsRelative;

                        bounds.Y += (panel.FixedRowHeight - (panel.FrozenRowCount * panel.VirtualRowHeight));
                        bounds.Y += (GridIndex * panel.VirtualRowHeight);
                    }

                    int n = bounds.Y - (t.Bottom);

                    SuperGrid.SetVScrollValue(n);
                }
            }
        }

        #endregion

        #region ExtendSelection

        internal void ExtendSelection(
            GridPanel panel, GridContainer endItem, bool extend)
        {
            int startIndex = panel.SelectionRowAnchor.GridIndex;
            int endIndex = endItem.GridIndex;

            panel.NormalizeIndices(false, 0, ref startIndex, ref endIndex);

            if (panel.OnlyRowsSelected(startIndex, endIndex) == false)
            {
                if (extend == false)
                    panel.ClearAllSelected();
                else
                    panel.ClearAll(false);

                panel.SetSelectedRows(startIndex, endIndex - startIndex + 1, true);

                GridContainer row = GetLastProcessedRow(panel);

                if (extend == true && row != null)
                    startIndex = row.GridIndex;

                InvalidateRows(panel, startIndex, endIndex, extend);
            }
        }

        #region GetLastProcessedRow

        private GridContainer GetLastProcessedRow(GridPanel panel)
        {
            if (panel.LastProcessedItem is GridContainer)
                return ((GridContainer)panel.LastProcessedItem);

            if (panel.LastProcessedItem is GridCell)
                return (((GridCell)panel.LastProcessedItem).GridRow);

            return (null);
        }

        #endregion

        #endregion

        #region InvalidateRows

        internal void InvalidateRows(GridPanel panel, int start, int end, bool extend)
        {
            if (start > end)
            {
                int temp = start;
                start = end;
                end = temp;
            }

            start = Math.Max(0, start);

            if (extend == false && panel.FrozenRowCount > 0)
                InvalidateRowRange(panel, 0, panel.FirstOnScreenRowIndex);

            InvalidateRowRange(panel, start, end);
        }

        #region InvalidateRowRange

        private void InvalidateRowRange(GridPanel panel, int start, int end)
        {
            Rectangle t = SuperGrid.ClientRectangle;

            GridContainer rs = panel.GetRowFromIndex(start);
            GridContainer re = panel.GetRowFromIndex(end);

            if (rs != null && re != null)
            {
                Rectangle rsRect = rs.Bounds;
                Rectangle reRect = re.Bounds;

                if (rs is GridPanel)
                    rsRect = GetScrollBounds(rs.ContainerBounds);

                if (re is GridPanel)
                    reRect = GetScrollBounds(re.ContainerBounds);

                Rectangle r = new Rectangle(rsRect.X, rsRect.Y,
                                            reRect.Width, reRect.Bottom - rsRect.Y);

                r.IntersectsWith(t);

                panel.InvalidateRender(r);
            }
        }

        #region GetScrollBounds

        private Rectangle GetScrollBounds(Rectangle r)
        {
            r.X -= HScrollOffset;

            if (IsVFrozen == false)
                r.Y -= VScrollOffset;

            return (r);
        }

        #endregion

        #endregion

        #endregion

        #region MeasureSubItems

        protected Size MeasureSubItems(
            GridLayoutInfo layoutInfo, GridLayoutStateInfo stateInfo, Size constraintSize)
        {
            Size sizeNeeded = Size.Empty;
            GridPanel panel = stateInfo.GridPanel;

            HasVisibleItems = false;

            GridItemsCollection items = Rows;
            foreach (GridElement item in items)
            {
                if (item.Visible == true)
                {
                    Size size;
                    GridPanel ipanel = item as GridPanel;

                    if (ipanel != null)
                    {
                        int n = GetRowIndent(panel, ipanel);

                        Rectangle r = layoutInfo.ClientBounds;
                        r.Inflate((panel.ShowDropShadow ? -5 : 0) - n, 0);

                        GridLayoutInfo itemLayoutInfo = new GridLayoutInfo(layoutInfo.Graphics, r);
                        GridLayoutStateInfo itemStateInfo = new GridLayoutStateInfo(ipanel, 0);

                        item.Measure(itemLayoutInfo, itemStateInfo, Size.Empty);
                        size = item.Size;

                        size.Width += n;
                        size.Height += (panel.LevelIndentSize.Height * 2);
                    }
                    else
                    {
                        item.Measure(layoutInfo, stateInfo, constraintSize);
                        size = item.Size;
                    }

                    sizeNeeded.Width = Math.Max(sizeNeeded.Width, size.Width);
                    sizeNeeded.Height += size.Height;

                    HasVisibleItems = true;
                }
            }

            return (sizeNeeded);
        }

        #endregion

        #region ArrangeSubItems

        protected void ArrangeSubItems(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, Rectangle layoutBounds)
        {
            Rectangle bounds = layoutBounds;

            GridPanel panel = stateInfo.GridPanel;
            int indentLevel = stateInfo.IndentLevel;

            GridItemsCollection items = Rows;
            foreach (GridElement item in items)
            {
                if (item.Visible == true)
                {
                    bounds.Height = item.Size.Height;

                    GridPanel ipanel = item as GridPanel;

                    if (ipanel != null)
                    {
                        ipanel.IndentLevel = indentLevel;

                        Rectangle r = layoutBounds;
                        r.Y = bounds.Y;
                        r.Height = bounds.Height + panel.LevelIndentSize.Height * 2;

                        ipanel.ContainerBounds = r;

                        int n = GetRowIndent(panel, ipanel);

                        r.X = layoutBounds.X + n;
                        r.Y += panel.LevelIndentSize.Height;

                        if (ipanel.ShowDropShadow == true)
                            r.Y--;

                        r.Width = ipanel.BoundsRelative.Width;
                        r.Height -= (panel.LevelIndentSize.Height * 2);

                        ArrangeCheckBox(panel, ipanel, r);

                        stateInfo.GridPanel = ipanel;
                        stateInfo.IndentLevel = 0;

                        item.Arrange(layoutInfo, stateInfo, r);

                        bounds.Y += (panel.LevelIndentSize.Height * 2);
                    }
                    else
                    {
                        stateInfo.GridPanel = panel;
                        stateInfo.IndentLevel = indentLevel;

                        item.Arrange(layoutInfo, stateInfo, bounds);
                    }

                    bounds.Y += item.Size.Height;
                }
                else
                {
                    item.BoundsRelative = bounds;
                }
            }
        }

        #region ArrangeCheckBox

        private void ArrangeCheckBox(
            GridPanel panel, GridPanel ipanel, Rectangle r)
        {
            if (ipanel.HasCheckBox == true)
            {
                int k = Math.Max(panel.CheckBoxSize.Width, panel.CheckBoxSize.Height) + 3;

                r.X -= k;
                r.Width = panel.CheckBoxSize.Width;

                r.Y += (r.Height - panel.CheckBoxSize.Height) / 2;
                r.Height = panel.CheckBoxSize.Height;

                ipanel.CheckBoxBounds = r;
            }
            else
            {
                ipanel.CheckBoxBounds = Rectangle.Empty;
            }
        }

        #endregion

        #endregion

        #region GetRowIndent

        protected int GetRowIndent(GridPanel panel, GridPanel ipanel)
        {
            int indentLevel = 0;

            GridContainer container = ipanel.Parent as GridContainer;

            if (container != null)
                indentLevel = container.IndentLevel + 1;

            int n = 0;

            if (panel.ShowRowHeaders == true)
                n += panel.RowHeaderWidth;

            if (panel.CheckBoxes == true)
                n += Math.Max(ipanel.CheckBoxSize.Width, ipanel.CheckBoxSize.Height) + 3;

            if (panel.PrimaryColumn != null &&
                panel.PrimaryColumn.Visible == true)
            {
                if (panel.ShowTreeButtons == true || panel.ShowTreeLines == true)
                    n += panel.TreeButtonIndent;

                n += (panel.LevelIndentSize.Width * indentLevel);

                GridColumnCollection columns = panel.Columns;

                int[] map = columns.DisplayIndexMap;
                for (int i = 0; i < map.Length; i++)
                {
                    int index = map[i];

                    GridColumn column = columns[index];

                    if (index == panel.PrimaryColumnIndex)
                        break;

                    if (column.Visible == true)
                        n += column.Size.Width;
                }
            }
            else
            {
                n += 3;
            }

            return (n);
        }

        #endregion

        #region GetParentPanel

        ///<summary>
        /// Gets the Parent GridPanel for the item.
        ///</summary>
        ///<returns>GridPanel or null</returns>
        public GridPanel GetParentPanel()
        {
            GridElement parent = Parent;

            while (parent != null)
            {
                if (parent is GridPanel)
                    return ((GridPanel)parent);

                parent = parent.Parent;
            }

            return (null);
        }

        #endregion

        #region InvalidateRowHeader

        internal void InvalidateRowHeader()
        {
            InvalidateRowHeader(this);
        }

        internal void InvalidateRowHeader(GridContainer row)
        {
            if (row != null)
            {
                GridPanel panel = row.GetParentPanel();

                if (panel != null)
                {
                    if (panel.ShowRowHeaders == true)
                    {
                        Rectangle bounds = panel.BoundsRelative;

                        if (panel.IsSubPanel == true)
                            bounds.X -= HScrollOffset;

                        bounds.Y = row.ContainerBounds.Y;

                        if (row.IsVFrozen == false)
                            bounds.Y -= VScrollOffset;

                        bounds.Width = panel.RowHeaderWidth;
                        bounds.Height = row.ContainerBounds.Height;

                        InvalidateRender(bounds);
                    }
                }
            }
        }

        #endregion

        #region Mouse Handling

        #region InternalMouseLeave

        /// <summary>
        /// Called by top-level control to pass message into the grid
        /// element. To handle it override corresponding On - virtual method.
        /// </summary>
        /// <param name="e"></param>
        internal override void InternalMouseLeave(EventArgs e)
        {
            GridElement mouseOverElement = _MouseOverElement;

            if (mouseOverElement != null)
            {
                mouseOverElement.InternalMouseLeave(e);
                _MouseOverElement = null;
            }

            base.InternalMouseLeave(e);
        }

        #endregion

        #region InternalMouseMove

        /// <summary>
        /// Called by top-level control to pass message into the grid
        /// element. To handle it override corresponding On - virtual method.
        /// </summary>
        /// <param name="e"></param>
        internal override void InternalMouseMove(MouseEventArgs e)
        {
            GridElement element = GetElementAt(e);

            if (element != _MouseOverElement)
            {
                GridElement mouseOverElement = _MouseOverElement;

                if (mouseOverElement != null)
                    mouseOverElement.InternalMouseLeave(e);

                if (element != null)
                    element.InternalMouseEnter(e);

                _MouseOverElement = element;
            }

            if (element != null)
                element.InternalMouseMove(e);

            base.InternalMouseMove(e);
        }

        #endregion

        #region InternalMouseHover

        /// <summary>
        /// Called by top-level control to pass message into the grid
        /// element. To handle it override corresponding On - virtual method.
        /// </summary>
        /// <param name="e"></param>
        internal override void InternalMouseHover(EventArgs e)
        {
            GridElement mouseOverElement = _MouseOverElement;
            
            if (mouseOverElement != null)
                mouseOverElement.InternalMouseHover(e);

            base.InternalMouseHover(e);
        }

        #endregion

        #region InternalMouseClick

        /// <summary>
        /// Called by top-level control to pass message into the grid
        /// element. To handle it override corresponding On - virtual method.
        /// </summary>
        /// <param name="e"></param>
        internal override void InternalMouseClick(MouseEventArgs e)
        {
            GridElement mouseOverElement = _MouseOverElement;

            if (mouseOverElement == null)
                InternalMouseMove(e);

            mouseOverElement = _MouseOverElement;

            if (mouseOverElement != null)
                mouseOverElement.InternalMouseClick(e);

            base.InternalMouseClick(e);
        }

        #endregion

        #region InternalMouseDoubleClick

        /// <summary>
        /// Called by top-level control to pass message into the grid
        /// element. To handle it override corresponding On - virtual method.
        /// </summary>
        /// <param name="e"></param>
        internal override void InternalMouseDoubleClick(MouseEventArgs e)
        {
            GridElement mouseOverElement = _MouseOverElement;

            if (mouseOverElement != null)
                mouseOverElement.InternalMouseDoubleClick(e);

            base.InternalMouseDoubleClick(e);
        }

        #endregion

        #region InternalMouseDown

        /// <summary>
        /// Called by top-level control to pass message into the grid
        /// element. To handle it override corresponding On - virtual method.
        /// </summary>
        /// <param name="e"></param>
        internal override void InternalMouseDown(MouseEventArgs e)
        {
            GridElement mouseOverElement = _MouseOverElement;

            if (mouseOverElement == null)
            {
                MouseEventArgs e2 = new
                    MouseEventArgs(MouseButtons.None, 0, e.X, e.Y, e.Delta);

                InternalMouseMove(e2);

                mouseOverElement = _MouseOverElement;
            }

            if (mouseOverElement != null)
                mouseOverElement.InternalMouseDown(e);

            base.InternalMouseDown(e);
        }

        #endregion

        #region InternalMouseUp

        /// <summary>
        /// Called by top-level control to pass message into the grid
        /// element. To handle it override corresponding On - virtual method.
        /// </summary>
        /// <param name="e"></param>
        internal override void InternalMouseUp(MouseEventArgs e)
        {
            GridElement mouseOverElement = _MouseOverElement;

            if (mouseOverElement != null)
                mouseOverElement.InternalMouseUp(e);

            base.InternalMouseUp(e);
        }

        #endregion

        #endregion

        #region GetElementAt

        /// <summary>
        /// Returns element at specified mouse coordinates.
        /// </summary>
        /// <param name="e">Mouse event arguments</param>
        /// <returns>Reference to child element or null if no element at specified coordinates</returns>
        public virtual GridElement GetElementAt(MouseEventArgs e)
        {
            return GetElementAt(e.X, e.Y);
        }

        /// <summary>
        /// Returns element at specified mouse coordinates.
        /// </summary>
        /// <param name="x">Horizontal position</param>
        /// <param name="y">Vertical position</param>
        /// <returns>Reference to child element or null if no element at specified coordinates</returns>
        public virtual GridElement GetElementAt(int x, int y)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                Rectangle t = CViewRect;
                Rectangle bounds = Rectangle.Empty;

                if (t.Contains(x, y) == true)
                {
                    if (this is GridPanel || Expanded == true)
                    {
                        GridItemsCollection items = Rows;
                        for (int i = 0; i < items.Count; i++)
                        {
                            GridElement item = items[i];

                            if (item.IsVFrozen == false)
                                break;

                            if (IsElementAt(item, x, y, ref bounds) == true)
                                return (item);

                            if (bounds.Y > t.Bottom)
                                break;
                        }

                        for (int i = FirstOnScreenRowIndex; i < items.Count; i++)
                        {
                            GridElement item = items[i];

                            if (IsElementAt(item, x, y, ref bounds) == true)
                            {
                                GridGroup grp = item as GridGroup;

                                if (grp != null)
                                {
                                    item = grp.GetElementAt(x, y);

                                    if (item == null)
                                        return (grp);
                                }

                                return (item);
                            }

                            if (bounds.Y > y)
                                break;
                        }
                    }
                }
            }

            return (null);
        }

        private bool IsElementAt(
            GridElement item, int x, int y, ref Rectangle bounds)
        {
            if (item.Visible == true)
            {
                bounds = item.BoundsRelative;

                if (item.Visible == true)
                {
                    if (item is GridPanel)
                        bounds = ((GridPanel)item).ContainerBounds;

                    bounds.X -= HScrollOffset;

                    if (item.IsVFrozen == false)
                        bounds.Y -= VScrollOffset;

                    if (bounds.Contains(x, y))
                        return (true);
                }
            }

            return (false);
        }

        #endregion

        #region InternalGetElementAt

        internal GridElement InternalGetElementAt(MouseEventArgs e)
        {
            return (GetElementAt(e.X, e.Y));
        }

        internal GridElement InternalGetElementAt(int x, int y)
        {
            return (GetElementAt(x, y));
        }

        #endregion

        #region CanSetActiveRow

        internal bool CanSetActiveRow(bool beep)
        {
            return (CanSetActiveRow(GridPanel, this, beep));
        }

        internal bool CanSetActiveRow(
            GridPanel panel, GridContainer row, bool beep)
        {
            if (row == null || row.AllowSelection == false)
            {
                if (beep == true)
                    SystemSounds.Beep.Play();

                return (false);
            }

            if (row.IsActive == false)
            {
                GridContainer parent = row.Parent as GridContainer;

                if (parent != null)
                {
                    if ((uint)row.RowIndex > parent.MaxRowIndex)
                        return (false);
                }

                return (SuperGrid.DoRowActivatingEvent(panel, SuperGrid.ActiveRow, row) != true);
            }

            return (row.IsActive);
        }

        #endregion

        #region FlushRow

        internal virtual void FlushRow()
        {
        }

        #endregion

        #region PurgeDeletedRows

        ///<summary>
        /// Purges all rows marked as 'deleted'. Once rows are purged
        /// they cannot be restored.
        ///</summary>
        public void PurgeDeletedRows()
        {
            PurgeDeletedRows(true);
        }

        ///<summary>
        /// Purges all rows marked as 'deleted'. Once rows are purged
        /// they cannot be restored.
        ///</summary>
        ///<param name="includeNestedRows">Determines whether nested rows are also purged.</param>
        public void PurgeDeletedRows(bool includeNestedRows)
        {
            GridPanel panel = GridPanel;

            if (panel.DeletedRowCount > 0)
            {
                if (SuperGrid.DoRowsPurgingEvent(this) == false)
                {
                    if (panel.ActiveRow != null)
                        panel.SelectActiveRow = true;

                    if (panel.VirtualMode == true)
                        PurgeVirtualDeletedRows(panel);
                    else
                        PurgeRealDeletedRows(panel, includeNestedRows);

                    SuperGrid.DoRowsPurgedEvent(this);
                }
            }
        }

        #region PurgeVirtualDeletedRows

        private void PurgeVirtualDeletedRows(GridPanel panel)
        {
            int count = 0;
            int topIndex = 0;
            int offset = 0;

            int activeIndex =
                (panel.ActiveRow != null) ? panel.ActiveRow.RowIndex : -1;

            try
            {
                SelectedElementCollection sec = panel.GetDeletedRows();

                for (int i = sec.Count - 1; i >= 0; i--)
                {
                    GridRow row = sec[i] as GridRow;

                    if (row != null)
                    {
                        count++;
                        topIndex = row.RowIndex - 1;

                        if (row.RowIndex < activeIndex)
                            offset++;

                        panel.SetDeleted(row, false);
                        panel.DataBinder.RemoveRow(row);
                    }
                }
            }
            finally
            {
                panel.VirtualRowCount -= count;
                panel.VirtualRows.MaxRowIndex = topIndex;
            }

            if (offset > 0)
                panel.LatentActiveRowIndex = activeIndex - offset;
        }

        #endregion

        #region PurgeRealDeletedRows

        private void PurgeRealDeletedRows(GridPanel panel, bool includeNestedRows)
        {
            if (panel.ActiveRow != null)
            {
                if (panel.ActiveRow.IsDeleted == true)
                {
                    if (panel.VirtualMode == false)
                        panel.ActiveRow = GetNextLocalItem(panel.ActiveRow.RowIndex);
                }
            }

            PurgeRealDeletedRowsEx(panel.Rows, includeNestedRows);
        }

        #region PurgeRealDeletedRowsEx

        private void PurgeRealDeletedRowsEx(
            GridItemsCollection rows, bool includeNestedRows)
        {
            for (int i = rows.Count - 1; i >= 0; i--)
            {
                GridContainer row = rows[i] as GridContainer;

                if (row != null)
                {
                    if (row.IsDeleted == true)
                    {
                        row.IsDeleted = false;
                        rows.RemoveAt(i);
                    }
                    else
                    {
                        if (includeNestedRows == true)
                            row.PurgeRealDeletedRowsEx(row.Rows, true);
                    }
                }
            }
        }

        #endregion

        #endregion

        #region GetNextLocalItem

        internal GridContainer GetNextLocalItem(int index)
        {
            GridPanel panel = GridPanel;

            index = GetNextLocalItemIndex(index);

            if (panel.VirtualMode == true)
            {
                if ((uint)index < panel.VirtualRowCount)
                    return (panel.VirtualRows[index]);
            }
            else
            {
                if ((uint) index < Rows.Count)
                    return (Rows[index] as GridContainer);
            }

            return (null);
        }

        #endregion

        #region GetNextLocalItemIndex

        private int GetNextLocalItemIndex(int index)
        {
            if (GridPanel.VirtualMode == true)
                return (GetNextLocalVirtualItemIndex(index));

            return (GetNextLocalRealItemIndex(index));
        }

        #region GetNextLocalRealItemIndex

        internal int GetNextLocalRealItemIndex(int index)
        {
            if (Rows.Count > 0 && index >= 0)
            {
                if (index >= Rows.Count)
                    index = Rows.Count - 1;

                for (int i = index + 1; i < Rows.Count; i++)
                {
                    GridContainer item = Rows[i] as GridContainer;

                    if (item != null && item.Visible == true && item.IsDeleted == false)
                        return (i);
                }

                for (int i = index - 1; i >= 0; i--)
                {
                    GridContainer item = Rows[i] as GridContainer;

                    if (item != null && item.Visible == true && item.IsDeleted == false)
                        return (i);
                }
            }

            return (-1);
        }

        #endregion

        #region GetNextLocalVirtualItemIndex

        internal int GetNextLocalVirtualItemIndex(int index)
        {
            GridPanel panel = GridPanel;

            if (panel.VirtualRowCount > 0 && index >= 0)
            {
                if (index >= panel.VirtualRowCount)
                    index = panel.VirtualRowCount - 1;

                for (int i = index + 1; i < panel.VirtualRowCount; i++)
                {
                    GridContainer item = panel.VirtualRows[i];

                    if (item != null && item.IsDeleted == false)
                        return (i);
                }

                for (int i = index - 1; i >= 0; i--)
                {
                    GridContainer item = panel.VirtualRows[i];

                    if (item != null && item.IsDeleted == false)
                        return (i);
                }
            }

            return (-1);
        }

        #endregion

        #endregion

        #endregion

        #region DetachNestedRows

        internal void DetachNestedRows(bool dispose)
        {
            DetachNestedRows(this, dispose);
        }

        private void DetachNestedRows(GridContainer cont, bool dispose)
        {
            if (dispose == true)
            {
                if (cont is GridPanel)
                    ((GridPanel) cont).DataBinder = null;
            }

            if (cont is GridRow)
                DetachGridRow((GridRow)cont);

            if (cont.Rows.Count > 0)
            {
                foreach (GridContainer row in cont.Rows)
                {
                    if (row != null)
                        DetachNestedRows(row, dispose);
                }
            }
        }

        private void DetachGridRow(GridRow row)
        {
            foreach (GridCell cell in row.Cells)
            {
                cell.EditControl = null;
                cell.RenderControl = null;
            }

            if (SuperGrid != null)
            {
                if (SuperGrid.ActiveRow == row)
                    SuperGrid.ActiveRow = null;
            }
        }

        #endregion

        #region Style support routines

        #region OnStyleChanged

        private void OnStyleChanged(string property,
            INotifyPropertyChanged oldValue, INotifyPropertyChanged newValue)
        {
            UpdateChangeHandler(oldValue, newValue);

            OnPropertyChanged(new PropertyChangedEventArgs(property));
        }

        #endregion

        #region UpdateChangeHandler

        private void UpdateChangeHandler(
            INotifyPropertyChanged oldValue, INotifyPropertyChanged newValue)
        {
            NeedsMeasured = true;

            if (oldValue != null)
                oldValue.PropertyChanged -= StyleChanged;

            if (newValue != null)
                newValue.PropertyChanged += StyleChanged;
        }

        #endregion

        #region StyleChanged

        protected virtual void StyleChanged(object sender, PropertyChangedEventArgs e)
        {
            if (SuperGrid != null)
                SuperGrid.UpdateStyleCount();

            VisualChangeType changeType = ((VisualPropertyChangedEventArgs)e).ChangeType;

            if (changeType == VisualChangeType.Layout)
            {
                NeedsMeasured = true;

                InvalidateLayout();
            }
            else
            {
                if (SuperGrid != null)
                    InvalidateRender();
            }
        }

        #endregion

        #region GetEffectiveRowStyle

        ///<summary>
        /// Gets the 'Effective' row style for the row. The
        /// 'Effective' Style is the cached summation of SuperGrid,
        /// Panel, Row, and Alternate Row styles.  It is the Style
        /// that is used to render each row element for the row.
        ///</summary>
        ///<returns>RowVisualStyle</returns>
        public RowVisualStyle GetEffectiveRowStyle()
        {
            return (GetEffectiveRowStyle(this));
        }

        internal RowVisualStyle GetEffectiveRowStyle(GridContainer item)
        {
            StyleState rowState = GetRowState(item);

            return (GetEffectiveRowStyle(item, rowState));
        }

        internal RowVisualStyle GetEffectiveRowStyle(
            GridContainer item, StyleState rowState)
        {
            return (GetRowStateStyle(item, rowState));
        }

        #region GetRowState

        internal StyleState GetRowState(GridContainer item)
        {
            StyleState rowState = StyleState.Default;

            if (IsMouseOver == true)
            {
                Point pt = SuperGrid.PointToClient(Control.MousePosition);

                Rectangle r = (item is GridPanel)
                                  ? item.ContainerBounds
                                  : item.BoundsRelative;

                r.X -= HScrollOffset;

                if (item.IsVFrozen == false)
                    r.Y -= VScrollOffset;

                Rectangle t = ViewRect;

                r.Intersect(t);

                if (r.Contains(pt) == true)
                    rowState |= StyleState.MouseOver;
            }

            if (item.IsSelected == true)
                rowState |= StyleState.Selected;

            return (rowState);
        }

        #endregion

        #region GetRowStateStyle

        private RowVisualStyle GetRowStateStyle(GridContainer item, StyleState rowState)
        {
            ValidateRowStyle();

            switch (rowState)
            {
                case StyleState.MouseOver:
                    return (GetRowStyle(item, StyleType.MouseOver));

                case StyleState.Selected:
                    return (GetRowStyle(item, StyleType.Selected));

                case StyleState.Selected | StyleState.MouseOver:
                    return (GetRowStyle(item, StyleType.SelectedMouseOver));

                case StyleState.ReadOnly:
                    return (GetRowStyle(item, StyleType.ReadOnly));

                case StyleState.ReadOnly | StyleState.MouseOver:
                    return (GetRowStyle(item, StyleType.ReadOnlyMouseOver));

                case StyleState.ReadOnly | StyleState.Selected:
                    return (GetRowStyle(item, StyleType.ReadOnlySelected));

                case StyleState.ReadOnly | StyleState.MouseOver | StyleState.Selected:
                    return (GetRowStyle(item, StyleType.ReadOnlySelectedMouseOver));

                default:
                    return (GetRowStyle(item, StyleType.Default));
            }
        }

        #endregion

        #region GetRowStyle

        private RowVisualStyle GetRowStyle(GridContainer item, StyleType e)
        {
            if (_EffectiveRowStyles.IsValid(e) == false)
            {
                RowVisualStyle style = new RowVisualStyle();

                StyleType[] css = style.GetApplyStyleTypes(e);

                if (css != null)
                {
                    GridPanel panel = GridPanel;

                    foreach (StyleType cs in css)
                    {
                        style.ApplyStyle(SuperGrid.BaseVisualStyles.RowStyles[cs]);
                        style.ApplyStyle(SuperGrid.DefaultVisualStyles.RowStyles[cs]);
                        style.ApplyStyle(GridPanel.DefaultVisualStyles.RowStyles[cs]);
                        style.ApplyStyle(item.RowStyles[cs]);

                        if (panel != null && panel.UseAlternateRowStyle == true)
                        {
                            if ((GridIndex % 2) > 0)
                            {
                                style.ApplyStyle(SuperGrid.BaseVisualStyles.AlternateRowCellStyles[cs]);
                                style.ApplyStyle(SuperGrid.DefaultVisualStyles.AlternateRowCellStyles[cs]);
                                style.ApplyStyle(GridPanel.DefaultVisualStyles.AlternateRowCellStyles[cs]);
                            }
                        }

                        style.ApplyStyle(item.CellStyles[cs]);
                        style.ApplyStyle(item.CellStyles[cs]);
                    }
                }

                SuperGrid.DoGetRowStyleEvent(this, e, ref style);

                RowHeaderVisualStyle style2 = style.RowHeaderStyle;
                SuperGrid.DoGetRowHeaderStyleEvent(this, e, ref style2);

                if (style.Background == null || style.Background.IsEmpty == true)
                    style.Background = new Background(Color.White);

                _EffectiveRowStyles[e] = style;
            }

            return (_EffectiveRowStyles[e]);
        }

        #endregion

        #endregion

        #region ValidateRowStyle

        private void ValidateRowStyle()
        {
            if (_EffectiveRowStyles == null ||
                (_StyleUpdateCount != SuperGrid.StyleUpdateCount))
            {
                _EffectiveRowStyles = new RowVisualStyles();

                _StyleUpdateCount = SuperGrid.StyleUpdateCount;
            }
        }

        #endregion

        #endregion

        #region IsParentOf

        ///<summary>
        /// Determines whether the row is
        /// a parent row of the given container row.
        ///</summary>
        ///<param name="row"></param>
        ///<returns>true if the row is a parent of the given row.</returns>
        public bool IsParentOf(GridContainer row)
        {
            if (row != null)
                return (row.IsChildOf(this));

            return (false);
        }

        #endregion

        #region IsChildOf

        ///<summary>
        /// Determines whether the row is
        /// a child row of the given container row.
        ///</summary>
        ///<param name="row"></param>
        ///<returns>true if the row is a child of the given row.</returns>
        public bool IsChildOf(GridContainer row)
        {
            if (row != null)
            {
                GridContainer parent = Parent as GridContainer;

                while (parent != null)
                {
                    if (parent == row)
                        return (true);

                    parent = parent.Parent as GridContainer;
                }
            }

            return (false);
        }

        #endregion

        #region GetCell

        ///<summary>
        /// Gets the GridCell for the given row and column index
        ///</summary>
        ///<param name="rowIndex"></param>
        ///<param name="columnIndex"></param>
        ///<returns>GridCell, or null if not a valid cell</returns>
        public GridCell GetCell(int rowIndex, int columnIndex)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                GridRow row = null;

                if (panel.VirtualMode == true)
                {
                    if ((uint)rowIndex < panel.VirtualRowCount)
                        row = panel.VirtualRows[rowIndex];
                }
                else
                {
                    if ((uint) rowIndex < Rows.Count)
                        row = Rows[rowIndex] as GridRow;
                }

                if (row != null && columnIndex < row.Cells.Count)
                    return (row.Cells[columnIndex]);
            }

            return (null);
        }

        #endregion

        #region Dispose

        public virtual void Dispose()
        {
            if (Rows != null && Rows.Count > 0)
            {
                foreach (GridElement item in Rows)
                {
                    GridContainer row = item as GridContainer;

                    if (row != null)
                        row.Dispose();
                }
            }
        }

        #endregion

        #region ContainerStates

        [Flags]
        private enum Cs
        {
            Checked = (1 << 0),
            Deleted = (1 << 1),
            Expanded = (1 << 2),
            HasVisibleItems = (1 << 3),
            NeedsFilterScan = (1 << 4),
            NeedsGrouped = (1 << 5),
            NeedsGroupSorted = (1 << 6),
            NeedsSorted = (1 << 7),
            ReadOnly = (1 << 8),
            RowsUnresolved = (1 << 9),
            Selected = (1 << 10),
            ShowCheckBox = (1 << 11),
            ShowTreeButton = (1 << 12),
        }

        #endregion
    }

    #region enums

    #region ExpandSource

    ///<summary>
    /// Operation source resulting in the Expand call
    ///</summary>
    public enum ExpandSource
    {
        ///<summary>
        /// Expand operation
        ///</summary>
        Expand,

        ///<summary>
        /// ExpandAll operation
        ///</summary>
        ExpandAll,
    }

    #endregion

    #endregion
}
