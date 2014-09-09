using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid.Style;
using DevComponents.SuperGrid.TextMarkup;

namespace DevComponents.DotNetBar.SuperGrid
{
    /// <summary>
    /// Defines the grid column header
    /// </summary>
    public class GridColumn : GridElement
    {
        #region Events

        /// <summary>
        /// Occurs after column header DisplayIndex has changed.
        /// </summary>
        public event EventHandler DisplayIndexChanged;

        #endregion

        #region Private variables

        private ColumnAutoSizeMode _AutoSizeMode = ColumnAutoSizeMode.NotSet;
        private ColumnSortMode _ColumnSortMode = ColumnSortMode.Single;
        private ColumnResizeMode _ResizeMode = ColumnResizeMode.MoveFollowingElements;

        private CellHighlightMode _CellHighlightMode = CellHighlightMode.Full;

        private SortDirection _SortDirection = SortDirection.None;
        private SortDirection _GroupDirection = SortDirection.None;
        private SortIndicator _SortIndicator = SortIndicator.Auto;

        private int _ColumnIndex;
        private int _DisplayIndex = -1;

        private int _MinimumWidth = 5;
        private int _FillWeight = 100;

        private string _NullString;
        private string _HeaderText;

        private Size _HeaderSize;
        private Size _HeaderContentSize;
        private Size _HeaderTextSize;

        private Cs _States;

        private BodyElement _HeaderTextMarkup;

        private string _Name;
        private int _Width = 100;

        private int _SelectionUpdateCount;
        private SelectedElements _SelectedCells;

        private Type _EditorType = typeof(GridTextBoxXEditControl);
        private Type _RenderType;

        private IGridCellEditControl _EditControl;
        private IGridCellEditControl _RenderControl;

        private object[] _EditorParams;
        private object[] _RenderParams;

        private int _MeasureCount;

        private Image _InfoImage;
        private Image _InfoImageCache;
        private Alignment _InfoImageAlignment = Alignment.MiddleRight;

        private string _DataPropertyName;
        private object _DefaultNewRowCellValue;
        private Type _DataType;

        private CellVisualStyles _CellStyles;
        private ColumnHeaderVisualStyles _HeaderStyles;
        private FilterColumnHeaderVisualStyles _FilterRowStyles;

        private ColumnHeaderVisualStyles _EffectiveStyles;
        private FilterColumnHeaderVisualStyles _EffectiveFilterStyles;
        private CellVisualStyles _EffectiveCellStyles;

        private StyleState _LastStyleState;
        private int _StyleUpdateCount;

        private Rectangle _SortImageBounds;

        private string _FilterExpr;
        private FilterEval _FilterEval;

        private object _FilterValue;
        private object _FilterDisplayValue;

        private Tbool _EnableFiltering = Tbool.NotSet;
        private Tbool _ShowPanelFilterExpr = Tbool.NotSet;

        private Rectangle _FilterImageBounds;
        private Size _FilterPopupSize = Size.Empty;
        private int _FilterPopupMaxItems = 100;
        private FilterScan _FilterScan;

        private FilterEditType _FilterEditType = FilterEditType.Auto;
        private FilterMatchType _FilterMatchType = FilterMatchType.NotSet;
        private GroupBoxEffects _GroupBoxEffects = GroupBoxEffects.NotSet;

        private string _ToolTip;

        #endregion

        #region Constructors

        ///<summary>
        /// GridColumn
        ///</summary>
        public GridColumn()
        {
            SetState(Cs.AllowEdit, true);
            SetState(Cs.MarkRowDirtyOnCellValueChange, true);

            _SelectedCells = new SelectedElements();
        }

        ///<summary>
        /// GridColumn
        ///</summary>
        public GridColumn(string name)
            : this()
        {
            Name = name;
        }

        #endregion

        #region Internal properties

        #region CanResize

        internal bool CanResize
        {
            get
            {
                ColumnAutoSizeMode mode = GetAutoSizeMode();

                switch (mode)
                {
                    case ColumnAutoSizeMode.None:
                    case ColumnAutoSizeMode.Fill:
                        return (true);

                    default:
                        return (false);
                }
            }
        }

        #endregion

        #region CanShowFilterExpr

        internal bool CanShowFilterExpr
        {
            get
            {
                if (_ShowPanelFilterExpr != Tbool.NotSet)
                    return (_ShowPanelFilterExpr == Tbool.True);

                if (GridPanel != null)
                    return (GridPanel.Filter.ShowPanelFilterExpr);

                return (false);
            }
        }

        #endregion

        #region EffectiveCellStyles

        internal CellVisualStyles EffectiveCellStyles
        {
            get { return (_EffectiveCellStyles); }
            set { _EffectiveCellStyles = value; }
        }

        #endregion

        #region EffectiveFilterStyles

        internal FilterColumnHeaderVisualStyles EffectiveFilterStyles
        {
            get { return (_EffectiveFilterStyles); }
            set { _EffectiveFilterStyles = value; }
        }

        #endregion

        #region EffectiveStyles

        internal ColumnHeaderVisualStyles EffectiveStyles
        {
            get { return (_EffectiveStyles); }
            set { _EffectiveStyles = value; }
        }

        #endregion

        #region FilterError

        internal bool FilterError
        {
            get { return (TestState(Cs.FilterError)); }
            set { SetState(Cs.FilterError, value); }
        }

        #endregion

        #region FilterExprUsesName

        internal bool FilterExprUsesName
        {
            get { return (TestState(Cs.FilterExprUsesName)); }
            set { SetState(Cs.FilterExprUsesName, value); }
        }

        #endregion

        #region FilterEval

        internal FilterEval FilterEval
        {
            get { return (_FilterEval); }
            set { _FilterEval = value; }
        }

        #endregion

        #region FilterImageBounds

        internal Rectangle FilterImageBounds
        {
            get { return (_FilterImageBounds); }
            set { _FilterImageBounds = value; }
        }

        #endregion

        #region FilterScan

        internal FilterScan FilterScan
        {
            get
            {
                if (_FilterScan == null)
                    FilterScan = new FilterScan(this);

                return (_FilterScan);
            }

            set
            {
                if (_FilterScan != value)
                {
                    if (_FilterScan != null)
                        _FilterScan.EndScan();

                    _FilterScan = value;
                }
            }
        }

        #endregion

        #region GroupBoxEffectsEx

        internal GroupBoxEffects GroupBoxEffectsEx
        {
            get
            {
                if (_GroupBoxEffects != GroupBoxEffects.NotSet)
                    return (_GroupBoxEffects);
                
                GridPanel panel = GridPanel;

                if (panel != null && panel.GroupByRow != null)
                {
                    if (panel.GroupByRow.GroupBoxEffects != GroupBoxEffects.NotSet)
                        return (panel.GroupByRow.GroupBoxEffects);
                }

                return (GroupBoxEffects.Move);
            }
        }

        #endregion

        #region HeaderContentSize

        internal Size HeaderContentSize
        {
            get { return (_HeaderContentSize); }
            set { _HeaderContentSize = value; }
        }

        #endregion

        #region HeaderSize

        internal Size HeaderSize
        {
            get { return (_HeaderSize); }
            set { _HeaderSize = value; }
        }

        #endregion

        #region HeaderTextMarkup

        internal BodyElement HeaderTextMarkup
        {
            get { return (_HeaderTextMarkup); }
        }

        #endregion

        #region HeaderTextSize

        internal Size HeaderTextSize
        {
            get { return (_HeaderTextSize); }
            set { _HeaderTextSize = value; }
        }

        #endregion

        #region IsFiltered

        internal bool IsFiltered
        {
            get { return (IsFilteringEnabled == true &&
                String.IsNullOrEmpty(FilterExpr) == false); }
        }

        #endregion

        #region IsReadOnly

        internal bool IsReadOnly
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    if (panel.ReadOnly || ReadOnly)
                        return (true);
                }

                return (TestState(Cs.ReadOnly));
            }
        }

        #endregion

        #region LastStyleState

        internal StyleState LastStyleState
        {
            get { return (_LastStyleState); }
            set { _LastStyleState = value; }
        }

        #endregion

        #region MeasureCount

        internal int MeasureCount
        {
            get { return (_MeasureCount); }
        }

        #endregion

        #region NeedsFilterScan

        internal bool NeedsFilterScan
        {
            get { return (TestState(Cs.NeedsFilterScan)); }
            set { SetState(Cs.NeedsFilterScan, value); }
        }

        #endregion

        #region NeedsMeasured

        internal override bool NeedsMeasured
        {
            get { return (base.NeedsMeasured); }

            set
            {
                if (value == true)
                    _MeasureCount++;

                base.NeedsMeasured = value;
            }
        }

        #endregion

        #region NeedsResized

        internal bool NeedsResized
        {
            get { return (TestState(Cs.NeedsResized)); }
            set { SetState(Cs.NeedsResized, value); }
        }

        #endregion

        #region ScanItems

        internal List<object> ScanItems
        {
            get
            {
                if (_FilterScan != null)
                    return (_FilterScan.ScanItems);

                return (null);
            }
        }

        #endregion

        #region SelectedCells

        internal SelectedElements SelectedCells
        {
            get { return (_SelectedCells); }
            set { _SelectedCells = value; }
        }

        #endregion

        #region SortImageBounds

        internal Rectangle SortImageBounds
        {
            get { return (_SortImageBounds); }
            set { _SortImageBounds = value; }
        }

        #endregion

        #region StyleUpdateCount

        internal int StyleUpdateCount
        {
            get { return (_StyleUpdateCount); }
            set { _StyleUpdateCount = value; }
        }

        #endregion

        #endregion

        #region Public properties

        #region AdjustSortImagePositionByMargin

        ///<summary>
        /// Gets or sets whether the Column Header sort image
        /// position is adjusted by the header margin
        ///</summary>
        [DefaultValue(false), Category("Appearance")]
        [Description("Indicates whether the Column Header sort image position is adjusted by the header margin.")]
        public bool AdjustSortImagePositionByMargin
        {
            get { return (TestState(Cs.AdjustSortImagePositionByMargin)); }

            set
            {
                if (AdjustSortImagePositionByMargin != value)
                {
                    SetState(Cs.AdjustSortImagePositionByMargin, value);

                    OnPropertyChangedEx("AdjustSortImagePositionByMargin", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region AllowEdit

        /// <summary>
        /// Gets or sets whether column cells can be edited by the user. 
        /// </summary>
        [DefaultValue(true), Category("Behavior")]
        [Description("Indicates whether column cells can be edited by the user.")]
        public bool AllowEdit
        {
            get { return (TestState(Cs.AllowEdit)); }

            set
            {
                if (value != AllowEdit)
                {
                    SetState(Cs.AllowEdit, value);

                    OnPropertyChangedEx("AllowEdit");
                }
            }
        }

        #endregion

        #region AutoSizeMode

        /// <summary>
        /// Gets or sets how column auto-sizing is performed
        /// </summary>
        [DefaultValue(ColumnAutoSizeMode.NotSet), Category("Sizing")]
        [Description("Indicates how column auto-sizing is performed.")]
        public ColumnAutoSizeMode AutoSizeMode
        {
            get { return (_AutoSizeMode); }

            set
            {
                if (value != _AutoSizeMode)
                {
                    _AutoSizeMode = value;

                    OnPropertyChangedEx("AutoSizeMode", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region Bounds

        ///<summary>
        /// Gets the scroll adjusted bounds of the Column
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Rectangle Bounds
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    Rectangle r = BoundsRelative;

                    if (IsHFrozen == false)
                        r.X -= HScrollOffset;

                    if (panel.IsSubPanel == true)
                        r.Y -= VScrollOffset;

                    return (r);
                }

                return (Rectangle.Empty);
            }
        }

        #endregion

        #region CellHighlightMode

        /// <summary>
        /// Gets or sets the type of highlighting
        /// to perform when a cell is selected
        /// </summary>
        [DefaultValue(CellHighlightMode.Full), Category("Behavior")]
        [Description("Indicates the type of highlighting to perform when a cell is selected.")]
        public CellHighlightMode CellHighlightMode
        {
            get { return (_CellHighlightMode); }

            set
            {
                if (value != _CellHighlightMode)
                {
                    _CellHighlightMode = value;

                    OnPropertyChangedEx("CellHighlightType");
                }
            }
        }

        #endregion

        #region CellStyles

        /// <summary>
        /// Gets or sets the default visual styles assigned to the column cells
        /// </summary>
        [Category("Style")]
        [Description("Indicates default visual style assigned to the column cells.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public CellVisualStyles CellStyles
        {
            get
            {
                if (_CellStyles == null)
                {
                    _CellStyles = new CellVisualStyles();

                    StyleVisualChangeHandler(null, _CellStyles);
                }

                return (_CellStyles);
            }

            set
            {
                if (_CellStyles != value)
                {
                    StyleVisualChangeHandler(_CellStyles, value);

                    _CellStyles = value;

                    OnPropertyChangedEx("CellStyles", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region ColumnIndex

        /// <summary>
        /// Gets the panel level, Columns collection index of the column
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ColumnIndex
        {
            get { return (_ColumnIndex); }
            internal set { _ColumnIndex = value; }
        }

        #endregion

        #region ColumnSortMode

        /// <summary>
        /// Gets or sets how column sorting is performed
        /// </summary>
        [DefaultValue(ColumnSortMode.Single), Category("Sorting")]
        [Description("Indicates how column sorting is performed.")]
        public ColumnSortMode ColumnSortMode
        {
            get { return (_ColumnSortMode); }

            set
            {
                if (value != _ColumnSortMode)
                {
                    _ColumnSortMode = value;

                    OnPropertyChangedEx("ColumnSortMode");
                }
            }
        }

        #endregion

        #region DataPropertyName

        ///<summary>
        /// Gets or sets the name of the data source property
        /// or database column to which the column is bound
        ///</summary>
        [DefaultValue(null), Category("Data")]
        [Description("Indicates the name of the data source property or database column to which the column is bound.")]
        public string DataPropertyName
        {
            get { return (_DataPropertyName); }

            set
            {
                if (value != _DataPropertyName)
                {
                    _DataPropertyName = value;

                    if (GridPanel != null)
                        GridPanel.NeedToUpdateBindings = true;

                    OnPropertyChangedEx("DataPropertyName", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region DataType

        ///<summary>
        ///Gets or sets the underlying data type
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type DataType
        {
            get { return (_DataType); }
            set { _DataType = value; }
        }

        #endregion

        #region DefaultNewRowCellValue

        /// <summary>
        /// Gets or sets the default cell value used when a new row is added
        /// </summary>
        [DefaultValue(null), Category("Data")]
        [Description("Indicates the default cell value used when a new row is added.")]
        [TypeConverter(typeof(ValueTypeConverter))]
        [Editor("DevComponents.SuperGrid.Design.ValueTypeEditor, DevComponents.SuperGrid.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=26d81176cfa2b486", typeof(UITypeEditor))]
        public object DefaultNewRowCellValue
        {
            get 
            {
                if (_DefaultNewRowCellValue is DBNull)
                    return null;
                return (_DefaultNewRowCellValue); 
            }

            set
            {
                if (value != _DefaultNewRowCellValue)
                {
                    _DefaultNewRowCellValue = value;

                    OnPropertyChangedEx("DefaultNewRowCellValue");
                }
            }
        }

        #endregion

        #region DisplayIndex

        /// <summary>
        /// Gets or sets display index of the column.
        /// </summary>
        /// <remarks>
        /// A lower display index means a column will appear first (to the left) of columns with a higher display index. 
        /// Allowable values are from 0 to num columns - 1. (-1 is legal only as the default value). SuperGrid enforces
        /// that no two columns have the same display index, ie. changing the display index of a column will cause the index
        /// of other columns to adjust as well.
        /// </remarks>
        [DefaultValue(-1), Category("Appearance")]
        [Description("Indicates the display index of the column. -1 indicates default value.")]
        public int DisplayIndex
        {
            get { return (_DisplayIndex); }

            set
            {
                if (value != _DisplayIndex)
                {
                    _DisplayIndex = value;

                    OnPropertyChangedEx("DisplayIndex", VisualChangeType.Layout);
                    OnDisplayIndexChanged(EventArgs.Empty);
                }
            }
        }

        #endregion

        #region EditControl

        ///<summary>
        /// Gets the default Edit Control used for each cell in the column.
        /// The column level edit control is a shared control, created and
        /// based upon the column level EditorType and EditorParams properties.
        /// Each cell in the column, that does not define its own EditorType,
        /// will utilize the column level EditorControl.
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IGridCellEditControl EditControl
        {
            get
            {
                if (_EditControl == null)
                    EditControl = GetEditControl(_EditorType, _EditorParams);

                if (_EditControl != null)
                {
                    if (_EditControl.EditorPanel.Parent == null && SuperGrid != null)
                        SuperGrid.Controls.Add(_EditControl.EditorPanel);
                }

                return (_EditControl);
            }

            internal set
            {
                if (_EditControl != value)
                {
                    if (_EditControl != null)
                    {
                        if (_EditControl.EditorPanel != null)
                        {
                            if (_EditControl.EditorPanel.Parent != null)
                                SuperGrid.Controls.Remove(_EditControl.EditorPanel);

                            _EditControl.EditorPanel.Visible = false;

                            if (_EditControl.EditorPanel.Controls.Count > 0)
                                _EditControl.EditorPanel.Controls[0].Visible = false;

                            _EditControl.EditorPanel.Dispose();
                        }

                        ((Control)_EditControl).Dispose();

                        InvalidateLayout();
                    }

                    _EditControl = value;
                }
            }
        }

        #endregion

        #region EditorParams

        /// <summary>
        /// Gets or sets an array of arguments that match in number,
        /// order, and type the parameters of the EditControl constructor
        /// to invoke. If empty or null, the default constructor is invoked.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object[] EditorParams
        {
            get { return (_EditorParams); }

            set
            {
                if (_EditorParams != value)
                {
                    _EditorParams = value;

                    EditControl = null;
                    RenderControl = null;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("EditorParams", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region EditorType

        ///<summary>
        /// Gets or sets the default cell editor type. This is the default
        /// control type used to perform the actual modification of the cell value
        ///</summary>
        [DefaultValue(typeof(GridTextBoxXEditControl)), Category("Data")]
        [Description("Indicates the default cell editor type. This is the default control type used to perform the actual modification of the cell value.")]
        [TypeConverter(typeof(EditTypeConverter))]
        [Editor("DevComponents.SuperGrid.Design.EditTypeEditor, DevComponents.SuperGrid.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=26d81176cfa2b486", typeof(UITypeEditor))]
        public Type EditorType
        {
            get { return (_EditorType); }

            set
            {
                if (value != _EditorType)
                {
                    _EditorType = value;

                    EditControl = null;
                    RenderControl = null;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("EditorType", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region EnableFiltering

        /// <summary>
        /// Gets or sets whether filtering is enabled for the column
        /// </summary>
        [DefaultValue(Tbool.NotSet), Category("Filtering")]
        [Description("Indicates whether filtering is enabled for the column.")]
        public Tbool EnableFiltering
        {
            get { return (_EnableFiltering); }

            set
            {
                if (_EnableFiltering != value)
                {
                    _EnableFiltering = value;

                    OnPropertyChangedEx("EnableFiltering", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region EnableGroupHeaderMarkup

        /// <summary>
        /// Gets or sets whether text-markup support is enabled for the Group Header Text
        /// </summary>
        [DefaultValue(false), Category("Grouping")]
        [Description("Indicates whether text-markup support is enabled for the Group Header Text.")]
        public bool EnableGroupHeaderMarkup
        {
            get { return (TestState(Cs.EnableGroupHeaderMarkup)); }

            set
            {
                if (EnableGroupHeaderMarkup != value)
                {
                    SetState(Cs.EnableGroupHeaderMarkup, value);

                    if (GridPanel != null)
                        GridPanel.NeedsGrouped = true;

                    OnPropertyChangedEx("EnableGroupHeaderMarkup", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region EnableHeaderMarkup

        /// <summary>
        /// Gets or sets whether text-markup support is enabled for the HeaderText
        /// </summary>
        [DefaultValue(false), Category("Appearance")]
        [Description("Indicates whether text-markup support is enabled for the HeaderText.")]
        public bool EnableHeaderMarkup
        {
            get { return (TestState(Cs.EnableHeaderMarkup)); }

            set
            {
                if (EnableHeaderMarkup != value)
                {
                    SetState(Cs.EnableHeaderMarkup, value);

                    MarkupHeaderTextChanged();

                    OnPropertyChangedEx("EnableHeaderMarkup", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region FilterAutoScan

        /// <summary>
        /// Gets or sets whether the column's FilterPopup
        /// is filled by auto-scanning the column contents.
        /// </summary>
        [DefaultValue(false), Category("Filtering")]
        [Description("Indicates whether the column's FilterPopup is filled by auto-scanning the column contents.")]
        public bool FilterAutoScan
        {
            get { return (TestState(Cs.FilterAutoScan)); }

            set
            {
                if (FilterAutoScan != value)
                {
                    SetState(Cs.FilterAutoScan, value);
                    SetState(Cs.NeedsFilterScan, true);

                    if (value == false)
                        FilterScan = null;

                    OnPropertyChangedEx("FilterAutoScan", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region FilterDisplayValue

        /// <summary>
        /// Gets or sets the value used to filter grid rows.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object FilterDisplayValue
        {
            get { return (_FilterDisplayValue); }

            set
            {
                if (_FilterDisplayValue != value)
                {
                    _FilterDisplayValue = value;

                    OnPropertyChangedEx("FilterDisplayValue");

                    if (GridPanel != null)
                    {
                        if (GridPanel.ColumnHeader.Visible == true)
                            GridPanel.ColumnHeader.InvalidateRender();
                    }
                }
            }
        }

        #endregion

        #region FilterEditType

        /// <summary>
        /// Gets or sets the filter edit type
        /// </summary>
        [DefaultValue(FilterEditType.Auto), Category("Filtering")]
        [Description("Indicates the filter edit type).")]
        public FilterEditType FilterEditType
        {
            get { return (_FilterEditType); }

            set
            {
                if (_FilterEditType != value)
                {
                    _FilterEditType = value;

                    OnPropertyChangedEx("FilterEditType", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region FilterExpr

        /// <summary>
        /// Gets or sets the value used to filter grid rows.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string FilterExpr
        {
            get { return (_FilterExpr); }

            set
            {
                if ("".Equals(value))
                    value = null;

                if ((_FilterExpr == null && _FilterExpr != value) ||
                    (_FilterExpr != null && _FilterExpr.Equals(value) == false))
                {
                    _FilterExpr = value;

                    _FilterEval = null;

                    if (_FilterExpr == null)
                    {
                        _FilterValue = null;
                        _FilterDisplayValue = null;
                    }

                    if (GridPanel != null)
                        GridPanel.NeedToUpdateDataFilter = true;

                    OnPropertyChangedEx("FilterExpr", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region FilterMatchType

        /// <summary>
        /// Gets or sets the filter data match type (how data elements are matched)
        /// </summary>
        [DefaultValue(FilterMatchType.NotSet), Category("Filtering")]
        [Description("Indicates the filter data match style (how data elements are matched).")]
        public FilterMatchType FilterMatchType
        {
            get { return (_FilterMatchType); }

            set
            {
                if (_FilterMatchType != value)
                {
                    _FilterMatchType = value;

                    FilterEval = null;

                    if (GridPanel != null)
                        GridPanel.NeedToUpdateDataFilter = true;

                    OnPropertyChangedEx("FilterMatchType", VisualChangeType.Layout);
                }
            }
        }

        #region GetFilterMatchType

        internal FilterMatchType GetFilterMatchType()
        {
            if (_FilterMatchType != FilterMatchType.NotSet)
                return (_FilterMatchType);

            if (GridPanel != null)
                return (GridPanel.GetFilterMatchType());

            return (FilterMatchType.None);
        }

        #endregion

        #endregion

        #region FilterPopupMaxItems

        /// <summary>
        /// Gets or sets the filter popup maximum number of drop-down items
        /// </summary>
        [DefaultValue(100), Category("Filtering")]
        [Description("Indicates the filter popup maximum number of drop-down items.")]
        public int FilterPopupMaxItems
        {
            get { return (_FilterPopupMaxItems); }
            set { _FilterPopupMaxItems = value; }
        }

        #endregion

        #region FilterPopupSize

        /// <summary>
        /// Gets or sets the filter popup size
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size FilterPopupSize
        {
            get { return (_FilterPopupSize); }
            set { _FilterPopupSize = value; }
        }

        #endregion

        #region FilterRowStyles

        /// <summary>
        /// Gets or sets the visual styles assigned to the Filter Row
        /// </summary>
        [DefaultValue(null), Category("Style")]
        [Description("Indicates visual style assigned to the Filter Row.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public FilterColumnHeaderVisualStyles FilterRowStyles
        {
            get
            {
                if (_FilterRowStyles == null)
                {
                    _FilterRowStyles = new FilterColumnHeaderVisualStyles();

                    StyleVisualChangeHandler(null, _FilterRowStyles);
                }

                return (_FilterRowStyles);
            }

            set
            {
                if (_FilterRowStyles != value)
                {
                    StyleVisualChangeHandler(_FilterRowStyles, value);

                    _FilterRowStyles = value;

                    OnPropertyChangedEx("FilterRowStyles", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region FilterValue

        /// <summary>
        /// Gets or sets the current filter value (the value that is
        /// loaded into, or retrieved from, the editing filter control
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object FilterValue
        {
            get { return (_FilterValue); }
            set { _FilterValue = value; }
        }

        #endregion

        #region FillWeight

        /// <summary>
        /// Gets or sets a value which, when AutoSizeMode is Fill,
        /// represents the width of the column relative to the widths
        /// of other fill-mode columns (default value is 100).
        /// </summary>
        [DefaultValue(100), Category("Sizing")]
        [Description("Indicates a value which, when AutoSizeMode is Fill, represents the width of the column relative to the widths of other fill-mode columns (default value is 100).")]
        public int FillWeight
        {
            get { return (_FillWeight); }

            set
            {
                if (value != _FillWeight)
                {
                    _FillWeight = value;

                    GridPanel panel = GridPanel;

                    if (panel != null)
                        panel.NeedsMeasured = true;

                    OnPropertyChangedEx("FillWeight", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region GroupBoxEffects

        /// <summary>
        /// Gets or sets how the column interacts with the GroupBox
        /// </summary>
        [DefaultValue(GroupBoxEffects.NotSet), Category("Grouping")]
        [Description("Indicates how the column interacts with the GroupBox.")]
        public GroupBoxEffects GroupBoxEffects
        {
            get { return (_GroupBoxEffects); }

            set
            {
                if (_GroupBoxEffects != value)
                {
                    _GroupBoxEffects = value;

                    OnPropertyChangedEx("GroupBoxEffects");
                }
            }
        }

        #endregion

        #region GroupDirection

        /// <summary>
        /// Gets or sets the grouping sort direction (ie. ascending or descending).
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SortDirection GroupDirection
        {
            get { return (_GroupDirection); }

            set
            {
                if (_GroupDirection != value)
                {
                    _GroupDirection = value;

                    OnGroupDirectionChanged();
                }
            }
        }

        #region OnGroupDirectionChanged

        private void OnGroupDirectionChanged()
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                panel.NeedsGroupSorted = true;

                if (panel.VirtualMode == true)
                    panel.VirtualRows.Clear();

                OnPropertyChangedEx("GroupDirection", VisualChangeType.Layout);
            }
        }

        #endregion

        #endregion

        #region HeaderText

        /// <summary>
        /// Gets or sets the column header text. If the HeaderText is null
        /// then the Name of the columns is used in its place.
        /// </summary>
        [DefaultValue(null), Category("Appearance"), Localizable(true)]
        [Description("Indicates the column header text. If the HeaderText is null then the Name of the columns is used in its place.")]
        public string HeaderText
        {
            get { return (_HeaderText); }

            set
            {
                if (_HeaderText != value)
                {
                    _HeaderText = value;

                    MarkupHeaderTextChanged();

                    NeedsMeasured = true;

                    OnPropertyChangedEx("HeaderText", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region HeaderStyles

        /// <summary>
        /// Gets or sets the visual styles assigned to the Column Header
        /// </summary>
        [DefaultValue(null), Category("Style")]
        [Description("Indicates visual style assigned to the Column Header.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ColumnHeaderVisualStyles HeaderStyles
        {
            get
            {
                if (_HeaderStyles == null)
                {
                    _HeaderStyles = new ColumnHeaderVisualStyles();

                    StyleVisualChangeHandler(null, _HeaderStyles);
                }

                return (_HeaderStyles);
            }

            set
            {
                if (_HeaderStyles != value)
                {
                    StyleVisualChangeHandler(_HeaderStyles, value);

                    _HeaderStyles = value;

                    OnPropertyChangedEx("HeaderStyles", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region InfoImage

        /// <summary>
        /// Gets or sets the default cell informational Image
        /// (the image to display when InfoText is non-empty).
        /// </summary>
        [DefaultValue(null), Category("Appearance")]
        [Description("Indicates the default cell informational Image (the image to display when InfoText is non-empty).")]
        public Image InfoImage
        {
            get { return (_InfoImage); }

            set
            {
                if (_InfoImage != value)
                {
                    if (_InfoImageCache != null)
                    {
                        _InfoImageCache.Dispose();
                        _InfoImageCache = null;
                    }

                    _InfoImage = value;

                    OnPropertyChangedEx("InfoImage", VisualChangeType.Layout);
                }
            }
        }

        #region GetInfoImage

        internal Image GetInfoImage()
        {
            if (_InfoImage != null)
                return (_InfoImage);

            if (_InfoImageCache == null)
            {
                _InfoImageCache = _InfoImage;

                if (_InfoImageCache == null)
                {
                    Assembly myAssembly = Assembly.GetExecutingAssembly();

                    using (Stream myStream =
                        myAssembly.GetManifestResourceStream("DevComponents.DotNetBar.SuperGrid.InfoImage.png"))
                    {
                        if (myStream != null)
                            _InfoImageCache = new Bitmap(myStream);
                    }
                }
            }

            return (_InfoImageCache);
        }

        #endregion

        #endregion

        #region InfoImageAlignment

        ///<summary>
        /// Gets or sets how the InfoImage is aligned within the cell
        ///</summary>
        [DefaultValue(Alignment.MiddleRight), Category("Appearance")]
        [Description("Indicates how the InfoImage is aligned within the cell.")]
        public Alignment InfoImageAlignment
        {
            get { return (_InfoImageAlignment); }

            set
            {
                if (_InfoImageAlignment != value)
                {
                    _InfoImageAlignment = value;

                    OnPropertyChangedEx("InfoImageAlignment");
                }
            }
        }

        #endregion

        #region IsFilteringEnabled

        ///<summary>
        /// Gets whether filtering is enabled for the column
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsFilteringEnabled
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    if (panel.EnableFiltering == false)
                        return (false);

                    if (_EnableFiltering != Tbool.NotSet)
                        return (_EnableFiltering == Tbool.True);

                    return (panel.EnableColumnFiltering == true);
                }

                return (false);
            }
        }

        #endregion

        #region IsGroupColumn

        ///<summary>
        /// Gets whether the column is a Grouping column
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsGroupColumn
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                    return (panel.GroupColumns.Contains(this));

                return (false);
            }
        }

        #endregion

        #region IsHFrozen

        ///<summary>
        /// Gets whether the column is horizontally frozen
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsHFrozen
        {
            get
            {
                GridPanel panel = Parent as GridPanel;

                if (panel != null && panel.IsSubPanel == false)
                {
                    int n = panel.Columns.GetDisplayIndex(this);

                    return (n < panel.FrozenColumnCount);
                }

                return (false);
            }
        }

        #endregion

        #region IsOnScreen

        ///<summary>
        /// Gets whether the column is visibly on screen.
        ///</summary>
        [Browsable (false)]
        [DesignerSerializationVisibility (DesignerSerializationVisibility.Hidden)]
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
                        Rectangle bounds = BoundsRelative;

                        if (IsHFrozen == false)
                        {
                            bounds.X -= HScrollOffset;

                            GridColumn fcol = panel.Columns.GetLastVisibleFrozenColumn();

                            if (fcol != null)
                            {
                                int n = fcol.BoundsRelative.Right - t.X;

                                t.X = fcol.BoundsRelative.Right;
                                t.Width -= n;
                            }
                        }

                        if (panel.IsVFrozen == false)
                            bounds.Y -= VScrollOffset;

                        return (t.IntersectsWith(bounds));
                    }
                }

                return (false);
            }
        }

        #endregion

        #region IsPrimaryColumn

        ///<summary>
        /// Gets whether the column is the Primary column
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPrimaryColumn
        {
            get
            {
                GridPanel panel = Parent as GridPanel;

                if (panel != null)
                    return (panel.PrimaryColumn == this);

                return (false);
            }
        }

        #endregion

        #region IsSelectable

        ///<summary>
        /// Gets whether the column can be selected.
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
        /// Gets or sets whether the column is selected
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSelected
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    if (_SelectionUpdateCount != panel.SelectionUpdateCount)
                    {
                        SetState(Cs.Selected, panel.IsItemSelected(this));
                        _SelectionUpdateCount = panel.SelectionUpdateCount;
                    }

                    return (TestState(Cs.Selected));
                }

                return (false);
            }

            set
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    if (panel.SetSelected(this, value) == true)
                    {
                        SetState(Cs.Selected, value);
                        _SelectionUpdateCount = panel.SelectionUpdateCount;

                        InvalidateRender();

                        panel.ColumnHeader.InvalidateRender();
                    }
                }
            }
        }

        #endregion

        #region IsSortColumn

        ///<summary>
        /// Gets whether the column is a Sort column
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSortColumn
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                    return (panel.SortColumns.Contains(this));

                return (false);
            }
        }

        #endregion

        #region MarkRowDirtyOnCellValueChange

        /// <summary>
        /// Gets or sets whether cell Value changes within this column
        /// will result in the associated Row being marked as 'dirty'
        /// </summary>
        [DefaultValue(true), Category("Data")]
        [Description("Indicates whether cell Value changes within this column will result in the associated Row being marked as 'dirty'.")]
        public bool MarkRowDirtyOnCellValueChange
        {
            get { return (TestState(Cs.MarkRowDirtyOnCellValueChange)); }

            set
            {
                if (value != MarkRowDirtyOnCellValueChange)
                {
                    SetState(Cs.MarkRowDirtyOnCellValueChange, value);

                    OnPropertyChangedEx("MarkRowDirtyOnCellValueChange");
                }
            }
        }

        #endregion

        #region MinimumWidth

        /// <summary>
        /// Gets or sets the minimum column width (default value is 5)
        /// </summary>
        [DefaultValue(5), Category("Sizing")]
        [Description("Indicates the minimum column width (default value is 5).")]
        public int MinimumWidth
        {
            get { return _MinimumWidth; }

            set
            {
                if (_MinimumWidth != value)
                {
                    if (value < 0)
                        throw new ArgumentException("Value must be 0 or greater.");
 
                    _MinimumWidth = value;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("MinimumWidth", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region Name

        /// <summary>
        /// Gets or sets the Column name.
        /// </summary>
        [DefaultValue(null), ParenthesizePropertyName(true)]
        [Description("Indicates the Column name.")]
        public string Name
        {
            get { return (_Name); }

            set
            {
                if (_Name != value)
                {
                    _Name = value;

                    if (String.IsNullOrEmpty(_HeaderText) == true)
                        OnPropertyChangedEx("Name", VisualChangeType.Layout);
                    else
                        OnPropertyChangedEx("Name");
                }
            }
        }

        #endregion

        #region NextVisibleColumn

        ///<summary>
        /// Gets the next visible column
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridColumn NextVisibleColumn
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                    return (panel.Columns.GetNextVisibleColumn(this));

                return (null);
            }
        }

        #endregion

        #region NullString

        /// <summary>
        /// Gets or sets the default text to display for null values. 
        /// </summary>
        [DefaultValue(null), Category("Data")]
        [Description("Indicates the default text to display for null values.")]
        public string NullString
        {
            get { return (_NullString); }

            set
            {
                if (_NullString != value)
                {
                    _NullString = value;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("NullString", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region PrevVisibleColumn

        ///<summary>
        /// Gets the previous visible column
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridColumn PrevVisibleColumn
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                    return (panel.Columns.GetPrevVisibleColumn(this));

                return (null);
            }
        }

        #endregion

        #region ReadOnly

        /// <summary>
        /// Gets or sets whether the user can change column cell contents
        /// </summary>
        [DefaultValue(false), Category("Behavior")]
        [Description("Indicates whether the user can change column cell contents.")]
        public bool ReadOnly
        {
            get { return (TestState(Cs.ReadOnly)); }

            set
            {
                if (ReadOnly != value)
                {
                    SetState(Cs.ReadOnly, value);

                    NeedsMeasured = true;

                    OnPropertyChangedEx("ReadOnly", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region RenderControl

        ///<summary>
        /// Gets the default render control for the column cells
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IGridCellEditControl RenderControl
        {
            get
            {
                if (_RenderControl == null)
                {
                    RenderControl = (_RenderType != null)
                                        ? GetEditControl(_RenderType, _RenderParams)
                                        : GetEditControl(_EditorType, _EditorParams);

                    if (_RenderControl != null)
                    {
                        if (_RenderControl.EditorPanel.Parent == null && SuperGrid != null)
                            SuperGrid.Controls.Add(_RenderControl.EditorPanel);
                    }
                }

                return (_RenderControl);
            }

            internal set
            {
                if (_RenderControl != value)
                {
                    if (_RenderControl != null)
                    {
                        if (_RenderControl.EditorPanel.Parent != null)
                        {
                            SuperGrid.Controls.Remove(_RenderControl.EditorPanel);

                            _RenderControl.EditorPanel.Visible = false;

                            if (_RenderControl.EditorPanel.Controls.Count > 0)
                                _RenderControl.EditorPanel.Controls[0].Visible = false;

                            _RenderControl.EditorPanel.Dispose();
                        }

                        ((Control)_RenderControl).Dispose();

                        InvalidateLayout();
                    }

                    _RenderControl = value;
                }
            }
        }

        #endregion

        #region RenderParams

        /// <summary>
        /// Gets or sets an array of arguments that match in number,
        /// order, and type the parameters of the RenderControl constructor
        /// to invoke. If empty or null, the default constructor is invoked.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object[] RenderParams
        {
            get { return (_RenderParams); }

            set
            {
                if (_RenderParams != value)
                {
                    _RenderParams = value;

                    RenderControl = null;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("RenderParams", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region RenderType

        ///<summary>
        /// Gets or sets the cell render control type (defaults to EditorType, when not set)
        ///</summary>
        [DefaultValue(null), Category("Data")]
        [Description("Indicates cell render control type (defaults to EditorType, when not set)")]
        [TypeConverter(typeof(EditTypeConverter))]
        [Editor("DevComponents.SuperGrid.Design.EditTypeEditor, DevComponents.SuperGrid.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=26d81176cfa2b486", typeof(UITypeEditor))]
        public Type RenderType
        {
            get { return (_RenderType); }

            set
            {
                if (value != _RenderType)
                {
                    _RenderType = value;

                    RenderControl = null;
                    NeedsMeasured = true;

                    OnPropertyChangedEx("RenderType", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region ResizeMode

        /// <summary>
        /// Gets or sets how the column is resized by the user
        /// </summary>
        [DefaultValue(ColumnResizeMode.MoveFollowingElements), Category("Sizing")]
        [Description("Indicates how the column is resized by the user.")]
        public ColumnResizeMode ResizeMode
        {
            get { return (_ResizeMode); }

            set
            {
                if (_ResizeMode != value)
                {
                    _ResizeMode = value;

                    OnPropertyChangedEx("ResizeMode");
                }
            }
        }

        #endregion

        #region SelectedCellCount

        ///<summary>
        /// Gets the current count of selected cells in the column.
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedCellCount
        {
            get
            {
                int n = 0;

                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    if (panel.VirtualMode == true)
                    {
                        foreach (SelectedRange range in _SelectedCells.Ranges)
                            n += range.Count;
                    }
                    else
                    {
                        foreach (SelectedRange range in _SelectedCells.Ranges)
                        {
                            for (int i = range.StartIndex; i <= range.EndIndex; i++)
                            {
                                GridRow row = panel.GetRowFromIndex(i) as GridRow;

                                if (row != null)
                                {
                                    if (row.Cells.Count > _ColumnIndex)
                                    {
                                        if (row.Cells[_ColumnIndex].AllowSelection == true)
                                            n++;
                                    }
                                    else
                                    {
                                        n++;
                                    }
                                }
                            }
                        }
                    }
                }

                return (n);
            }
        }

        #endregion

        #region ShowCustomFilterExpr

        ///<summary>
        /// Gets or sets whether the filter expression is displayed in the panel area
        ///</summary>
        [DefaultValue(Tbool.NotSet), Category("Filtering")]
        [Description("Indicates whether the filter expression is displayed in the panel area.")]
        public Tbool ShowPanelFilterExpr
        {
            get { return (_ShowPanelFilterExpr); }

            set
            {
                if (_ShowPanelFilterExpr != value)
                {
                    _ShowPanelFilterExpr = value;

                    OnPropertyChangedEx("ShowPanelFilterExpr", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region SortDirection

        /// <summary>
        /// Gets or sets the Sorting direction (ascending or descending)
        /// </summary>
        [Browsable(false)]
        [DefaultValue(SortDirection.None), Category("Sorting")]
        [Description("Indicates the Sorting direction (ascending or descending).")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SortDirection SortDirection
        {
            get { return (_SortDirection); }

            set
            {
                if (_SortDirection != value)
                {
                    _SortDirection = value;

                    OnSortDirectionChanged();
                    OnPropertyChangedEx("SortDirection", VisualChangeType.Render);
                }
            }
        }

        #region OnSortDirectionChanged

        private void OnSortDirectionChanged()
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                panel.NeedsSorted = true;
                panel.ColumnHeader.InvalidateHeader(panel, this);

                if (panel.VirtualMode == true)
                    panel.VirtualRows.Clear();
            }
        }

        #endregion

        #endregion

        #region SortIndicator

        /// <summary>
        /// Gets or sets the sort indicator
        /// </summary>
        [DefaultValue(SortIndicator.Auto), Category("Sorting")]
        [Description("Indicates the Sorting indicator.")]
        public SortIndicator SortIndicator
        {
            get { return (_SortIndicator); }

            set
            {
                if (_SortIndicator != value)
                {
                    _SortIndicator = value;

                    NeedsMeasured = true;

                    OnSortIndicatorChanged();
                }
            }
        }

        #region OnSortIndicatorChanged

        private void OnSortIndicatorChanged()
        {
            GridPanel panel = GridPanel;

            if (panel != null)
                panel.ColumnHeader.InvalidateRender();

            OnPropertyChangedEx("SortIndicator");
        }

        #endregion

        #endregion

        #region ToolTip

        ///<summary>
        /// Gets or sets the ToolTip text for the column header.
        ///</summary>
        [DefaultValue(null), Category("Appearance")]
        [Description("Indicates ththe ToolTip text for the column header.")]
        public string ToolTip
        {
            get { return (_ToolTip); }

            set
            {
                if (_ToolTip != value)
                {
                    _ToolTip = value;

                    OnPropertyChangedEx("ToolTip");
                }
            }
        }

        #endregion

        #region Width

        /// <summary>
        /// Gets or sets the column width.
        /// </summary>
        [DefaultValue(100), Category("Sizing")]
        [Description("Indicates the column width.")]
        public int Width
        {
            get { return _Width; }

            set
            {
                if (_Width != value)
                {
                    _Width = value;

                    OnWidthChanged();
                }
            }
        }

        #region OnWidthChanged

        private void OnWidthChanged()
        {
            GridPanel panel = GridPanel;

            if (panel != null)
                SuperGrid.DoColumnResizedEvent(panel, this);

            NeedsMeasured = true;

            OnPropertyChangedEx("Width", VisualChangeType.Layout);
        }

        #endregion

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

        #region OnEvent processing

        #region OnDisplayIndexChanged

        /// <summary>
        /// Raises DisplayIndexChanged event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnDisplayIndexChanged(EventArgs e)
        {
            EventHandler handler = DisplayIndexChanged;

            if (handler != null)
                handler(this, e);
        }

        #endregion

        #endregion

        #region MeasureOverride

        /// <summary>
        /// Performs the layout of the item and sets the Size property to size that item will take.
        /// </summary>
        /// <param name="layoutInfo">Layout information.</param>
        /// <param name="stateInfo"></param>
        /// <param name="constraintSize"></param>
        protected override void MeasureOverride(
            GridLayoutInfo layoutInfo, GridLayoutStateInfo stateInfo, Size constraintSize)
        {
            if (NeedsResized == true || NeedsMeasured == true || stateInfo.GridPanel.NeedsMeasured == true)
            {
                Size sizeNeeded = Size.Empty;

                ColumnAutoSizeMode autoSizeMode = GetAutoSizeMode();

                int baseWidth = MinimumWidth;

                switch (autoSizeMode)
                {
                    case ColumnAutoSizeMode.AllCells:
                    case ColumnAutoSizeMode.DisplayedCells:
                    case ColumnAutoSizeMode.ColumnHeader:
                        baseWidth = Math.Max(baseWidth, _HeaderSize.Width);
                        break;
                }

                int height = 0;

                switch (autoSizeMode)
                {
                    case ColumnAutoSizeMode.None:
                        sizeNeeded.Width = Math.Max(baseWidth, Width);
                        break;

                    case ColumnAutoSizeMode.AllCells:
                    case ColumnAutoSizeMode.AllCellsExceptHeader:
                        sizeNeeded.Width = GetColumnWidth(layoutInfo, stateInfo,
                            autoSizeMode, stateInfo.GridPanel.Rows, baseWidth, 0, ref height);
                        break;

                    case ColumnAutoSizeMode.ColumnHeader:
                        sizeNeeded = new Size(baseWidth, 0);
                        MarkRowsToMeasure(stateInfo.GridPanel.Rows);
                        break;

                    default:
                        sizeNeeded.Width = GetColumnWidth(layoutInfo, stateInfo, autoSizeMode,
                            stateInfo.GridPanel.Rows, baseWidth, layoutInfo.ClientBounds.Height, ref height);
                        break;
                }

                Size = sizeNeeded;

                NeedsResized = false;
            }
        }

        #region GetColumnWidth

        private int GetColumnWidth(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, ColumnAutoSizeMode autoSizeMode,
            IEnumerable<GridElement> items, int width, int maxHeight, ref int height)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                if (panel.VirtualMode == true)
                {
                    if (panel.VirtualRowCount > 0)
                    {
                        int n = GetVirtualColumnWidthEx(layoutInfo,
                            stateInfo, autoSizeMode, ref height);

                        width = Math.Max(width, n);
                    }
                }
                else
                {
                    int n = GetColumnWidthEx(layoutInfo,
                            stateInfo, autoSizeMode, items, maxHeight, ref height);

                    width = Math.Max(width, n);
                }
            }

            return (width);
        }

        #region GetVirtualColumnWidthEx

        private int GetVirtualColumnWidthEx(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, ColumnAutoSizeMode autoSizeMode, ref int height)
        {
            GridPanel panel = GridPanel;

            int start = panel.FirstOnScreenRowIndex;
            int end = panel.LastOnScreenRowIndex;

            int width = 0;

            for (int i = start; i <= end; i++)
            {
                GridRow row = panel.VirtualRows[i];

                if (ColumnIndex < row.GridPanel.Columns.Count)
                {
                    if (ColumnIndex < row.Cells.Count)
                    {
                        GridCell cell = row.Cells[ColumnIndex];

                        MeasureCell(layoutInfo, stateInfo, autoSizeMode, row, cell);

                        width = Math.Max(width, cell.CellSize.Width);
                        height += cell.Size.Height;
                    }
                    else
                    {
                        width = Math.Max(width, Width);
                    }
                }
            }

            return (width);
        }

        #endregion

        #region GetColumnWidthEx

        private int GetColumnWidthEx(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, ColumnAutoSizeMode autoSizeMode,
            IEnumerable<GridElement> items, int maxHeight, ref int height)
        {
            int width = 0;

            foreach (GridElement item in items)
            {
                GridRow row = item as GridRow;

                if (row != null && row.Visible == true)
                {
                    if (ColumnIndex < row.GridPanel.Columns.Count)
                    {
                        if (ColumnIndex < row.Cells.Count)
                        {
                            GridCell cell = row.Cells[ColumnIndex];

                            if (maxHeight <= 0 || height < maxHeight)
                            {
                                MeasureCell(layoutInfo, stateInfo, autoSizeMode, row, cell);

                                width = Math.Max(width, cell.CellSize.Width);
                                height += cell.Size.Height;
                            }
                            else
                            {
                                cell.NeedsMeasured = true;
                            }
                        }
                        else
                        {
                            width = Math.Max(width, Width);
                        }
                    }

                    if (row.Rows != null && row.Expanded == true)
                    {
                        GridLayoutStateInfo itemStateInfo = new
                            GridLayoutStateInfo(stateInfo.GridPanel, stateInfo.IndentLevel + 1);

                        width = GetColumnWidth(layoutInfo, itemStateInfo,
                            autoSizeMode, row.Rows, width, maxHeight, ref height);
                    }
                }
                else
                {
                    if (item is GridGroup)
                    {
                        int n = GetColumnWidthEx(layoutInfo, stateInfo,
                            autoSizeMode, ((GridGroup) item).Rows, maxHeight, ref height);

                        width = Math.Max(width, n);
                    }
                }
            }

            return (width);
        }

        #endregion

        #endregion

        #region MeasureCell

        private void MeasureCell(GridLayoutInfo layoutInfo, GridLayoutStateInfo stateInfo,
            ColumnAutoSizeMode autoSizeMode, GridRow row, GridCell cell)
        {
            if (cell.NeedsMeasured || NeedsMeasured || row.NeedsMeasured)
            {
                int rowHeight = row.GetRowHeight();

                if (rowHeight > 0)
                {
                    Size size = new Size(Width, rowHeight);

                    if (autoSizeMode != ColumnAutoSizeMode.None)
                    {
                        cell.Measure(layoutInfo, stateInfo, Size.Empty);

                        size.Width = cell.Size.Width;
                        size.Width += GetColumnIndent(stateInfo.IndentLevel);
                    }
                    else
                    {
                        cell.Measure(layoutInfo, stateInfo, size);
                    }

                    cell.Size = size;
                }
                else
                {
                    Size size = Size.Empty;

                    if (autoSizeMode == ColumnAutoSizeMode.None)
                    {
                        size.Width = Width;
                        size.Width -= GetColumnIndent(stateInfo.IndentLevel);
                        size.Width = Math.Max(1, size.Width);
                    }

                    cell.Measure(layoutInfo, stateInfo, new Size(size.Width, 0));

                    if (autoSizeMode != ColumnAutoSizeMode.None)
                    {
                        size = cell.Size;
                        size.Width += GetColumnIndent(stateInfo.IndentLevel);
                        cell.Size = size;
                    }
                    else
                    {
                        size = cell.Size;
                        size.Width = Width;
                        cell.Size = size;
                    }
                }

                cell.CellSize = cell.Size;
            }
        }

        #endregion

        #region MarkRowsToMeasure

        private void MarkRowsToMeasure(IEnumerable<GridElement> items)
        {
            foreach (GridElement item in items)
            {
                GridRow row = item as GridRow;

                if (row != null && row.Visible == true)
                {
                    if (row.GetRowHeight() <= 0)
                        row.NeedsMeasured = true;

                    if (row.Rows != null && row.Expanded == true)
                        MarkRowsToMeasure(row.Rows);
                }
            }
        }

        #endregion

        #endregion

        #region ArrangeOverride

        /// <summary>
        /// Performs the arrange pass layout of the item when final position and size of the item has been set.
        /// </summary>
        /// <param name="layoutInfo">Layout information.</param>
        /// <param name="stateInfo"></param>
        /// <param name="layoutBounds">Layout bounds</param>
        protected override void ArrangeOverride(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, Rectangle layoutBounds)
        {
            GridPanel panel = stateInfo.GridPanel;

            if (panel.ColumnHeader.Visible == true)
                panel.ColumnHeader.UpdateImageBounds(panel, this);

            if (NeedsFilterScan == true || panel.NeedsFilterScan == true)
            {
                NeedsFilterScan = false;

                if (FilterAutoScan == true)
                    FilterScan.BeginScan();
            }
        }

        #endregion

        #region RenderOverride

        /// <summary>
        /// Performs drawing of the item and its children.
        /// </summary>
        /// <param name="renderInfo">Holds contextual rendering information.</param>
        protected override void RenderOverride(GridRenderInfo renderInfo)
        {
            RenderDesignerElement(renderInfo, Bounds);
        }

        #region RenderDesignerElement

        private void RenderDesignerElement(
            GridRenderInfo renderInfo, Rectangle r)
        {
            if (SuperGrid.DesignerElement == this)
            {
                int n = r.Y - (GridPanel.ColumnHeader.Bounds.Y + 4);

                r.X += 2;
                r.Y = GridPanel.ColumnHeader.Bounds.Y - 4;
                r.Width -= 6;
                r.Height += n + 4;

                if (r.Width > 0 && r.Height > 0)
                {
                    using (Pen pen = new Pen(Color.Purple))
                    {
                        pen.DashStyle = DashStyle.Dash;

                        renderInfo.Graphics.DrawRectangle(pen, r);
                    }
                }
            }
        }

        #endregion

        #endregion

        #region GetHeaderText

        internal string GetHeaderText()
        {
            return (_HeaderText ?? _Name);
        }

        #endregion

        #region GetColumnIndent

        internal int GetColumnIndent(int indentLevel)
        {
            int n = 0;

            GridPanel panel = GridPanel;

            if (panel != null)
            {
                if (panel.PrimaryColumn == this)
                {
                    if (panel.ShowTreeButtons == true || panel.ShowTreeLines == true)
                        n += panel.TreeButtonIndent;

                    if (panel.CheckBoxes == true)
                    {
                        if (panel.ShowTreeButtons == true || panel.ShowTreeLines == true)
                            n += 3;

                        n += panel.CheckBoxSize.Width + 2;
                    }

                    n += (panel.LevelIndentSize.Width * indentLevel);
                }
            }

            return (n);
        }

        #endregion

        #region GetAutoSizeMode

        internal ColumnAutoSizeMode GetAutoSizeMode()
        {
            GridPanel panel = Parent as GridPanel;

            if (panel != null)
            {
                ColumnAutoSizeMode mode = AutoSizeMode;

                if (mode == ColumnAutoSizeMode.NotSet)
                {
                    mode = panel.ColumnAutoSizeMode;

                    if (mode != ColumnAutoSizeMode.NotSet)
                        return (mode);
                }

                return (mode);
            }

            return (ColumnAutoSizeMode.None);
        }

        #endregion

        #region GetMaximumCellSize

        ///<summary>
        /// This routine calculates and returns the maximum cell
        /// size from each cell in the column, as limited by the
        /// given row scope ('AllRows' or 'OnScreenRows').
        /// If 'includeHeader' is true, then the width of the header
        /// is included in the max calculation.
        ///</summary>
        ///<returns>Maximum cell size.</returns>
        public Size GetMaximumCellSize(RowScope scope, bool includeHeader)
        {
            Size size = Size.Empty;
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                using (Graphics g = SuperGrid.CreateGraphics())
                {
                    GridLayoutInfo layoutInfo = new GridLayoutInfo(g, panel.BoundsRelative);
                    GridLayoutStateInfo layoutState = new GridLayoutStateInfo(panel, 0);

                    if (includeHeader == true)
                    {
                        Size oldSize = panel.ColumnHeader.Size;

                        panel.ColumnHeader.MeasureHeader(layoutInfo, Size.Empty, panel, this);

                        size.Width = _HeaderSize.Width;

                        panel.ColumnHeader.Size = oldSize;
                    }

                    if (panel.VirtualMode == true)
                    {
                        int firstRow = 0;
                        int lastRow = panel.VirtualRowCountEx;

                        if (scope == RowScope.OnScreenRows)
                        {
                            firstRow = panel.FirstOnScreenRowIndex;
                            lastRow = panel.LastOnScreenRowIndex + 1;
                        }

                        for (int i = firstRow; i < lastRow; i++)
                        {
                            GridRow row = panel.VirtualRows[i];
                            GridCell cell = row.Cells[ColumnIndex];

                            Size oldSize = cell.Size;

                            cell.Measure(layoutInfo, layoutState, Size.Empty);

                            int n = GetColumnIndent(layoutState.IndentLevel);

                            size.Width = Math.Max(size.Width, cell.Size.Width + n);
                            size.Height = Math.Max(size.Height, cell.Size.Height);

                            cell.Size = oldSize;
                        }
                    }
                    else
                    {
                        if (panel.Rows.Count > 0)
                        {
                            int firstRow = 0;
                            int lastRow = panel.Rows.Count;

                            if (scope == RowScope.OnScreenRows)
                            {
                                firstRow = panel.FirstOnScreenRowIndex;
                                lastRow = firstRow + 100;
                            }

                            Size cellSize = GetMaximumCellSize(
                                panel.Rows, firstRow, lastRow, layoutInfo, layoutState);

                            size.Width = Math.Max(size.Width, cellSize.Width);
                            size.Height = Math.Max(size.Height, cellSize.Height);
                        }
                    }
                }
            }

            return (size);
        }

        private Size GetMaximumCellSize(GridItemsCollection rows,
            int firstRow, int lastRow, GridLayoutInfo layoutInfo, GridLayoutStateInfo layoutState)
        {
            Size size = Size.Empty;

            for (int i = firstRow; i < lastRow; i++)
            {
                if (i >= rows.Count)
                    break;

                GridContainer item = rows[i] as GridContainer;

                if (item != null)
                {
                    Size cellSize = Size.Empty;

                    if (item is GridRow)
                    {
                        cellSize = GetMaxRowCellSize(
                            (GridRow) item, layoutInfo, layoutState);
                    }
                    else if (item is GridGroup)
                    {
                        cellSize = GetMaximumCellSize(
                            item.Rows, 0, lastRow - i, layoutInfo, layoutState);
                    }

                    size.Width = Math.Max(size.Width, cellSize.Width);
                    size.Height = Math.Max(size.Height, cellSize.Height);
                }
            }

            return (size);
        }

        #region GetMaxRowCellSize

        private Size GetMaxRowCellSize(GridRow row,
            GridLayoutInfo layoutInfo, GridLayoutStateInfo layoutState)
        {
            if (row.Cells.Count <= ColumnIndex)
                return (Size.Empty);

            GridCell cell = row.Cells[ColumnIndex];

            Size oldSize = cell.Size;

            cell.Measure(layoutInfo, layoutState, Size.Empty);

            Size size = cell.Size;
            size.Width += GetColumnIndent(layoutState.IndentLevel);

            cell.Size = oldSize;

            if (row.Rows.Count > 0 && row.Expanded == true)
            {
                layoutState.IndentLevel++;

                foreach (GridContainer item in row.Rows)
                {
                    if (item is GridRow)
                    {
                        Size cellSize =
                            GetMaxRowCellSize((GridRow)item, layoutInfo, layoutState);

                        size.Width = Math.Max(size.Width, cellSize.Width);
                        size.Height = Math.Max(size.Height, cellSize.Height);
                    }
                }

                layoutState.IndentLevel--;
            }

            return (size);
        }

        #endregion

        #endregion

        #region GetSelectedCells

        ///<summary>
        /// GetSelectedCells
        ///</summary>
        public SelectedElementCollection GetSelectedCells()
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                SelectedElementCollection cells = new
                    SelectedElementCollection(SelectedCells.Count);

                panel.GetSelectedCells(cells, this);

                return (cells);
            }

            return (null);
        }

        #endregion

        #region EnsureVisible

        ///<summary>
        /// EnsureVisible
        ///</summary>
        ///<param name="center"></param>
        public override void EnsureVisible(bool center)
        {
            if (Visible == true)
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    if (IsHFrozen == false)
                    {
                        Rectangle t = SViewRect;

                        GridColumn pcol = panel.Columns.GetLastVisibleFrozenColumn();

                        if (pcol != null)
                        {
                            t.Width -= (pcol.BoundsRelative.Right - t.X);
                            t.X = pcol.BoundsRelative.Right;
                        }

                        Rectangle bounds = BoundsRelative;

                        if (bounds.Width >= t.Width || bounds.X - HScrollOffset < t.X)
                        {
                            int n = bounds.X - t.X;

                            if (center == false || bounds.Width >= t.Width)
                                SuperGrid.SetHScrollValue(n - 1);
                            else
                                SuperGrid.SetHScrollValue(n - (t.Width - bounds.Width)/2);
                        }
                        else if (bounds.X - HScrollOffset > t.Right - bounds.Width)
                        {
                            int n = bounds.X - (t.Right - bounds.Width);

                            if (center == false)
                                SuperGrid.SetHScrollValue(n + 1);
                            else
                                SuperGrid.SetHScrollValue(n + (t.Width - bounds.Width)/2);
                        }
                    }
                }
            }
        }

        #endregion

        #region ToggleSort

        ///<summary>
        /// ToggleSort
        ///</summary>
        public void ToggleSort()
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                SortDirection sortDirection;

                switch (_SortDirection)
                {
                    case SortDirection.None:
                    case SortDirection.Descending:
                        sortDirection = SortDirection.Ascending;
                        break;

                    default:
                        sortDirection = SortDirection.Descending;
                        break;
                }

                panel.AddSort(this, sortDirection);

                SuperGrid.DoSortChangedEvent(panel);
            }
        }

        #endregion

        #region Edit support

        #region GetEditControl

        internal IGridCellEditControl GetEditControl(Type type, object[] args)
        {
            if (type != null)
            {
                IGridCellEditControl editor =
                    Activator.CreateInstance(type, args) as IGridCellEditControl;

                if (editor != null)
                {
                    if (editor is Control == false)
                        throw new Exception("Edit/Render Type must be based on 'Control'.");

                    editor.EditorPanel = new EditorPanel();
                    editor.EditorPanel.Visible = false;
                    editor.EditorPanel.Controls.Add((Control) editor);
                }

                return (editor);
            }

            return (null);
        }

        #endregion

        #endregion

        #region Style support routines

        #region StylePropertyChanged

        internal void StylePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            EffectiveStyles = new ColumnHeaderVisualStyles();
            EffectiveFilterStyles = new FilterColumnHeaderVisualStyles();
        }

        #endregion

        #region StyleVisualChangeHandler

        private void StyleVisualChangeHandler(
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

        /// <summary>
        /// Occurs when one of element visual styles has property changes.
        /// Default implementation invalidates visual appearance of element.
        /// </summary>
        /// <param name="sender">VisualStyle that changed.</param>
        /// <param name="e">Event arguments.</param>
        protected virtual void StyleChanged(object sender, PropertyChangedEventArgs e)
        {
            if (SuperGrid != null)
            {
                SuperGrid.UpdateStyleCount();

                VisualChangeType changeType = ((VisualPropertyChangedEventArgs) e).ChangeType;

                if (changeType == VisualChangeType.Layout)
                {
                    NeedsMeasured = true;

                    InvalidateLayout();
                }
                else
                {
                    GridPanel.InvalidateRender();
                }
            }
        }

        #endregion

        #region GetFilterStyle

        internal FilterColumnHeaderVisualStyle GetFilterStyle(StyleType e)
        {
            ValidateStyle();

            if (EffectiveFilterStyles == null)
                EffectiveFilterStyles = new FilterColumnHeaderVisualStyles();

            if (EffectiveFilterStyles.IsValid(e) == false)
            {
                FilterColumnHeaderVisualStyle style = new FilterColumnHeaderVisualStyle();

                StyleType[] css = style.GetApplyStyleTypes(e);

                if (css != null)
                {
                    foreach (StyleType cs in css)
                    {
                        style.ApplyStyle(SuperGrid.BaseVisualStyles.FilterColumnHeaderStyles[cs]);
                        style.ApplyStyle(SuperGrid.DefaultVisualStyles.FilterColumnHeaderStyles[cs]);
                        style.ApplyStyle(GridPanel.DefaultVisualStyles.FilterColumnHeaderStyles[cs]);
                        style.ApplyStyle(FilterRowStyles[cs]);
                    }
                }

                SuperGrid.DoGetFilterColumnHeaderStyleEvent(this, e, ref style);

                if (style.Background == null || style.Background.IsEmpty == true)
                    style.Background = new Background(Color.White);

                if (style.Font == null)
                {
                    Font font = SystemFonts.DefaultFont;
                    style.Font = font;
                }

                EffectiveFilterStyles[e] = style;
            }

            return (EffectiveFilterStyles[e]);
        }

        #endregion

        #region GetHeaderStyle

        internal ColumnHeaderVisualStyle GetHeaderStyle(StyleType e)
        {
            ValidateStyle();

            if (EffectiveStyles == null)
                EffectiveStyles = new ColumnHeaderVisualStyles();

            if (EffectiveStyles.IsValid(e) == false)
            {
                ColumnHeaderVisualStyle style = new ColumnHeaderVisualStyle();

                StyleType[] css = style.GetApplyStyleTypes(e);

                if (css != null)
                {
                    foreach (StyleType cs in css)
                    {
                        style.ApplyStyle(SuperGrid.BaseVisualStyles.ColumnHeaderStyles[cs]);
                        style.ApplyStyle(SuperGrid.DefaultVisualStyles.ColumnHeaderStyles[cs]);
                        style.ApplyStyle(GridPanel.DefaultVisualStyles.ColumnHeaderStyles[cs]);
                        style.ApplyStyle(HeaderStyles[cs]);
                    }
                }

                SuperGrid.DoGetColumnHeaderStyleEvent(this, e, ref style);

                if (style.Background == null || style.Background.IsEmpty == true)
                    style.Background = new Background(Color.White);

                if (style.Font == null)
                    style.Font = SystemFonts.DefaultFont;

                EffectiveStyles[e] = style;
            }

            return (EffectiveStyles[e]);
        }

        #endregion

        #region InvalidateStyle

        ///<summary>
        ///Invalidates the cached Style
        ///definition for all defined StyleTypes
        ///</summary>
        public void InvalidateStyle()
        {
            EffectiveStyles = null;
            EffectiveFilterStyles = null;

            InvalidateLayout();
        }

        ///<summary>
        ///Invalidate the cached Style
        ///definition for the given StyleType
        ///</summary>
        ///<param name="type"></param>
        public void InvalidateStyle(StyleType type)
        {
            if (_EffectiveStyles != null)
            {
                _EffectiveStyles[type] = null;
                _EffectiveFilterStyles[type] = null;

                InvalidateLayout();
            }
        }

        #endregion

        #region ApplyCellStyle

        internal void ApplyCellStyle(CellVisualStyle style, StyleType cs)
        {
            ValidateStyle();

            if (_EffectiveCellStyles == null)
                _EffectiveCellStyles = new CellVisualStyles();

            if (_EffectiveCellStyles.IsValid(cs) == false)
            {
                GridPanel panel = GridPanel;

                int colIndex = panel.UseAlternateColumnStyle 
                    ? panel.Columns.GetDisplayIndex(this) : -1;

                CellVisualStyle cstyle = new CellVisualStyle();

                if ((colIndex % 2) > 0)
                {
                    cstyle.ApplyStyle(SuperGrid.BaseVisualStyles.AlternateColumnCellStyles[cs]);
                    cstyle.ApplyStyle(SuperGrid.DefaultVisualStyles.AlternateColumnCellStyles[cs]);
                    cstyle.ApplyStyle(GridPanel.DefaultVisualStyles.AlternateColumnCellStyles[cs]);
                }

                cstyle.ApplyStyle(CellStyles[cs]);

                _EffectiveCellStyles[cs] = cstyle;
            }

            style.ApplyStyle(_EffectiveCellStyles[cs]);
        }

        #endregion

        #region ValidateStyle

        private void ValidateStyle()
        {
            if (_StyleUpdateCount != SuperGrid.StyleUpdateCount)
            {
                EffectiveStyles = null;
                EffectiveFilterStyles = null;
                EffectiveCellStyles = null;

                _StyleUpdateCount = SuperGrid.StyleUpdateCount;
            }
        }

        #endregion

        #endregion

        #region GetFilterPanelType

        internal FilterEditType GetFilterPanelType()
        {
            FilterEditType filterType = FilterEditType;
            
            if (filterType == FilterEditType.Auto)
            {
                IGridCellEditControl editor = EditControl;

                if (editor != null)
                {
                    if (editor is ComboBox)
                    {
                        filterType = FilterEditType.ComboBox;
                    }
                    else
                    {
                        if (editor.EditorValueType == typeof(bool))
                            filterType = FilterEditType.CheckBox;

                        else if (editor.EditorValueType == typeof(DateTime))
                            filterType = FilterEditType.DateTime;

                        else
                            filterType = FilterEditType.TextBox;
                    }
                }
                else
                {
                    filterType = FilterEditType.TextBox;
                }
            }

            SuperGrid.DoGetFilterEditTypeEvent(this, ref filterType);

            return (filterType);
        }

        #endregion

        #region Markup support

        private void MarkupHeaderTextChanged()
        {
            _HeaderTextMarkup = null;

            if (EnableHeaderMarkup == true)
            {
                if (MarkupParser.IsMarkup(_HeaderText) == true)
                {
                    _HeaderTextMarkup = MarkupParser.Parse(_HeaderText);

                    if (_HeaderTextMarkup != null)
                        _HeaderTextMarkup.HyperLinkClick += HeaderTextMarkupLinkClick;
                }
            }
        }

        /// <summary>
        /// Occurs when a header text markup link is clicked
        /// </summary>
        protected virtual void HeaderTextMarkupLinkClick(object sender, EventArgs e)
        {
            HyperLink link = sender as HyperLink;

            SuperGrid.DoColumnHeaderMarkupLinkClickEvent(this, link);

            GridPanel.ColumnHeader.InvalidateHeader(GridPanel, this);
        }

        /// <summary>
        /// Gets plain Header text without text-markup (if text-markup is used in Text)
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string PlainHeaderText
        {
            get { return (_HeaderTextMarkup != null ? _HeaderTextMarkup.PlainText : _HeaderText); }
        }

        #endregion

        #region ActivateFilterPopup

        ///<summary>
        ///Activates the column's FilterPopup
        ///</summary>
        public void ActivateFilterPopup()
        {
            GridPanel panel = GridPanel;

            if (panel != null)
                panel.ColumnHeader.ActivateFilterPopup(panel, this);
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            string s = base.ToString();

            string t = GetHeaderText();

            if (String.IsNullOrEmpty(t) == false)
                s += ": (\"" + t + "\")";

            return (s);
        }

        #endregion

        #region ColumnStates

        [Flags]
        private enum Cs
        {
            AdjustSortImagePositionByMargin = (1 << 0),
            AllowEdit = (1 << 1),
            EnableGroupHeaderMarkup = (1 << 2),
            EnableHeaderMarkup = (1 << 3),
            FilterAutoScan = (1 << 4),
            FilterError = (1 << 5),
            FilterExprUsesName = (1 << 6),
            MarkRowDirtyOnCellValueChange = (1 << 7),
            NeedsFilterScan = (1 << 8),
            NeedsResized = (1 << 9),
            ReadOnly = (1 << 10),
            Selected = (1 << 11),
        }

        #endregion
    }

    #region enums

    #region ColumnAutoSizeMode

    /// <summary>
    /// Defines columns auto-sizing mode.
    /// </summary>
    public enum ColumnAutoSizeMode
    {
        /// <summary>
        /// The sizing behavior of the column is inherited
        /// from the GridPanel.AutoSizeColumnsMode property.
        /// </summary>
        NotSet,

        /// <summary>
        /// Auto-sizing does not take place.
        /// </summary>
        None,

        /// <summary>
        /// The column width adjusts so that the widths of all columns
        /// exactly fills the display area of the control, requiring
        /// horizontal scrolling only to keep column widths above the
        /// ColumnHeader.MinimumWidth property values. Relative column widths
        /// are determined by the relative Column.FillWeight property values.
        /// </summary>
        Fill,

        /// <summary>
        /// The column width adjusts to fit the contents of all cells in the column
        /// that are in rows currently displayed on screen, excluding the header cell. 
        /// </summary>
        DisplayedCellsExceptHeader,

        /// <summary>
        /// The column width adjusts to fit the contents of all cells in the column
        /// that are in rows currently displayed on screen, including the header cell. 
        /// </summary>
        DisplayedCells,

        /// <summary>
        /// The column width adjusts to fit the contents of the column header cell. 
        /// </summary>
        ColumnHeader,

        /// <summary>
        /// The column width adjusts to fit the contents of all
        /// cells in the column, excluding the header cell. 
        /// </summary>
        AllCellsExceptHeader,

        /// <summary>
        /// The column width adjusts to fit the contents of all
        /// cells in the column, including the header cell. 
        /// </summary>
        AllCells
    }

    #endregion

    #region ColumnAutoMode

    ///<summary>
    /// Indicates the column sorting mode
    ///</summary>
    public enum ColumnSortMode
    {
        ///<summary>
        /// No sorting permitted
        ///</summary>
        None,

        ///<summary>
        /// Can participate in Single column sorts only
        ///</summary>
        Single,

        ///<summary>
        /// Can participate in Single and Multiple column sorts
        ///</summary>
        Multiple,
    }

    #endregion

    #region ColumnResizeMode

    ///<summary>
    /// ColumnResizeMode
    ///</summary>
    public enum ColumnResizeMode
    {
        ///<summary>
        /// The element cannot be resized by the user
        ///</summary>
        None,

        ///<summary>
        /// The size of the element plus that of its adjacent element
        /// is changed so that the total size of the elements is maintained
        ///</summary>
        MaintainTotalWidth,

        ///<summary>
        /// When the element size is changed, all following
        /// elements are offset accordingly
        ///</summary>
        MoveFollowingElements,
    }

    #endregion

    #region SortIndicator

    ///<summary>
    /// SortIndicator
    ///</summary>
    public enum SortIndicator
    {
        ///<summary>
        /// No indicator is displayed
        ///</summary>
        None,

        ///<summary>
        /// Ascending indicator is displayed
        ///</summary>
        Ascending,

        ///<summary>
        /// Descending indicator is displayed
        ///</summary>
        Descending,

        ///<summary>
        /// Set automatically based on current
        /// column sort criteria
        ///</summary>
        Auto,
    }

    #endregion

    #region RowScope

    ///<summary>
    /// RowScope
    ///</summary>
    public enum RowScope
    {
        ///<summary>
        /// All Rows
        ///</summary>
        AllRows,

        ///<summary>
        /// Only On Screen rows
        ///</summary>
        OnScreenRows,
    }

    #endregion

    #endregion

    #region EditTypeConverter

    ///<summary>
    /// EditTypeConverter
    ///</summary>
    public class EditTypeConverter : TypeConverter
    {
        ///<summary>
        /// ConvertTo
        ///</summary>
        ///<param name="context"></param>
        ///<param name="culture"></param>
        ///<param name="value"></param>
        ///<param name="destinationType"></param>
        ///<returns></returns>
        public override object ConvertTo(
            ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                Type type = value as Type;

                if (type != null)
                    return (type.Name);
            }

            return (base.ConvertTo(context, culture, value, destinationType));
        }
    }

    #endregion

}
