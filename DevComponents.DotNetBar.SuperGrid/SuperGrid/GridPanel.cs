using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Media;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid.Primitives;
using DevComponents.DotNetBar.SuperGrid.Style;
using DevComponents.SuperGrid.TextMarkup;

namespace DevComponents.DotNetBar.SuperGrid
{
    /// <summary>
    /// Represents a grid panel which lays out the grid rows.
    /// </summary>
    [TypeConverter(typeof(BlankExpandableObjectConverter))]
    public class GridPanel : GridContainer
    {
        #region Private variables

        private string _Name = "";
        private ColumnAutoSizeMode _ColumnAutoSizeMode;

        private GridCaption _Caption;
        private GridColumnHeader _ColumnHeader;

        private GridHeader _Header;
        private GridFooter _Footer;
        private GridTitle _Title;
        private GridFilter _Filter;
        private GridGroupByRow _GroupByRow;

        private string _NoRowsText;

        private Size _LevelIndentSize = new Size(20, 10);
        private int _FrozenColumnCount;
        private int _FrozenRowCount;

        private int _PrimaryColumnIndex;
        private GridColumnCollection _Columns;
        private List<GridColumn> _GroupColumns;

        private List<GridColumn> _SortColumns;
        private SortLevel _SortLevel = SortLevel.Root;
        private bool _InRangeAdd;
        private bool _IsSorting;
        private bool _LoadInsertRow;
        private bool _IsRowMoving;
        private bool _PreActiveRowDirty;
        private bool _KeepRowsSorted = true;
        private bool _SortUsingHiddenColumns;

        private Image _ExpandImage;
        private Image _CollapseImage;
        private ImageList _ImageList;

        private bool _CheckBoxes;
        private Size _CheckBoxSize = new Size(13, 13);

        private GridLines _GridLines = GridLines.Both;

        private bool _FocusCuesEnabled = true;

        private bool _AllowRowResize;
        private bool _AllowRowDelete;
        private bool _AllowRowInsert;
        private bool _AllowRowHeaderResize;
        private bool _AllowEmptyCellSelection;
        private bool _AllowEdit = true;

        private bool _AutoGenerateColumns = true;
        private bool _AutoHideDeletedRows = true;
        private bool _AutoSelectNewBoundRows = true;
        private bool _AutoSelectDeleteBoundRows = true;
        private bool _AutoExpandSetGroup;

        private bool _ShowRowHeaders = true;
        private bool _ShowRowInfo = true;
        private bool _ShowRowGridIndex;
        private bool _ShowRowDirtyMarker = true;
        private bool _ShowInsertRow;

        private bool _ShowEditingImage = true;

        private bool _ShowTreeButtons;
        private bool _ShowTreeLines;

        private bool _ShowGroupExpand = true;
        private bool _ShowGroupUnderline = true;
        private bool _ShowWhitespaceRowLines = true;

        private bool _ShowCellInfo = true;
        private bool _ShowToolTips = true;
        private bool _ShowDropShadow = true;

        private bool _UseAlternateRowStyle;
        private bool _UseAlternateColumnStyle;

        private bool _IndentGroups = true;
        private bool _ImmediateResize;
        private bool _MultiSelect = true;
        private bool _EnterKeySelectsNextRow = true;
        private bool _EnableCellExpressions;
        private bool _EnsureVisibleAfterSort;
        private bool _EnsureVisibleAfterGrouping;
        private bool _EnsureRowVisible;
        private bool _SelectActiveRow;
        private bool _ReselectActiveItem;
        private bool _EnableSelectionBuffering = true;

        private KeyboardEditMode _KeyboardEditMode = KeyboardEditMode.EditOnKeystrokeOrF2;
        private MouseEditMode _MouseEditMode = MouseEditMode.DoubleClick;
        private RowEditMode _RowEditMode = RowEditMode.ClickedCell;
        private RowDoubleClickBehavior _RowDoubleClickBehavior = RowDoubleClickBehavior.Activate;

        private SelectionGranularity _SelectionGranularity = SelectionGranularity.Cell;
        private ColumnDragBehavior _ColumnDragBehavior = ColumnDragBehavior.ExtendClickBehavior;
        private RowDragBehavior _RowDragBehavior = RowDragBehavior.ExtendSelection;
        private CellDragBehavior _CellDragBehavior = CellDragBehavior.ExtendSelection;

        private RowWhitespaceClickBehavior _RowWhitespaceClickBehavior = RowWhitespaceClickBehavior.ClearSelection;
        private TopLeftHeaderSelectBehavior _TopLeftHeaderSelectBehavior = TopLeftHeaderSelectBehavior.Deterministic;

        private ColumnHeaderClickBehavior _ColumnHeaderClickBehavior = ColumnHeaderClickBehavior.SortAndReorder;
        private GroupHeaderKeyBehavior _GroupHeaderKeyBehavior = GroupHeaderKeyBehavior.Skip;
        private GroupHeaderClickBehavior _GroupHeaderClickBehavior = GroupHeaderClickBehavior.None;
        private WhitespaceClickBehavior _WhitespaceClickBehavior = WhitespaceClickBehavior.ClearSelection;

        private RowHighlightType _RowHighlightType = RowHighlightType.Full;
        private ActiveRowIndicatorStyle _ActiveRowIndicatorStyle = ActiveRowIndicatorStyle.Image;

        private RowHeaderVisibility _GroupRowHeaderVisibility = RowHeaderVisibility.PanelControlled;

        private int _RowHeaderWidth = 35;
        private int _GroupHeaderHeight = 30;

        private int _MaxRowHeight = 500;
        private int _MinRowHeight = 5;
        private int _DefaultRowHeight = 22;
        private int _DefaultPreDetailRowHeight;
        private int _DefaultPostDetailRowHeight;
        private Size _RowHeaderSize;
        private Size _SizeNeeded;

        private bool _VirtualMode;
        private int _VirtualRowCount;
        private int _VirtualRowHeight = 22;
        private GridVirtualRows _VirtualRows;
        private GridRow _VirtualInsertRow;
        private GridRow _VirtualTempInsertRow;

        private Rectangle _WhiteSpaceRect;
        private GridContainer _HotItem;

        private int _SelectionUpdateCount = 1;
        private int _LastSelectionUpdateCount = 1;
        private int _DeleteUpdateCount = 1;

        private SelectedElements _SelectedRows;
        private SelectedElements _SelectedColumns;

        private GridContainer _SelectionRowAnchor;
        private GridColumn _SelectionColumnAnchor;
        private GridElement _LastProcessedItem;

        private SelectedElements _DeletedRows;

        private object _DataSource;
        private string _DataMember;
        private DataBinder _DataBinder;
        private bool _NeedToUpdateBindings;

        private NullValue _NullValue = NullValue.DBNull;
        private string _NullString = String.Empty;

        private GridContainer _ActiveRow;
        private int _LatentActiveRowIndex = -1;
        private int _LatentActiveCellIndex = -1;

        private Image _InsertRowImage;
        private Image _InsertRowImageCache;
        private int _InsertRowImageIndex = -1;

        private Image _InsertTempRowImage;
        private Image _InsertTempRowImageCache;
        private int _InsertTempRowImageIndex = -1;

        private RelativeRow _InitialActiveRow = RelativeRow.FirstRow;
        private RelativeSelection _InitialSelection = RelativeSelection.FirstCell;

        private StyleType _SizingStyle = StyleType.NotSet;
        private DefaultVisualStyles _DefaultVisualStyles;
        private GridPanelVisualStyle _EffectiveStyle;
        private CellVisualStyles _EffectiveCellStyles;
        private int _StyleUpdateCount;

        private bool _EnableNoRowsMarkup;
        private BodyElement _NoRowsMarkup;

        private string _FilterExpr;
        private FilterEval _FilterEval;
        private FilterMatchType _FilterMatchType = FilterMatchType.NotSet;
        private FilterLevel _FilterLevel = FilterLevel.Root;

        private bool _EnableFiltering;
        private bool _EnableRowFiltering;
        private bool _EnableColumnFiltering;

        private bool _NeedToUpdateDataFilter;
        private bool _FilterIgnoreMatchCase = true;

        private int _NewIndex;
        private int _NewFullIndex;
        private int _NewGridIndex;
        private int _IndiceesUpdateCount;

        private GridContainer[] _GridIndexArray;
        private Dictionary<GridCell, EEval> _ExpDictionary;

        private int _VisibleRowCount;
        private int _RowHeaderIndexOffset;

        private ExpandButtonType _ExpandButtonType = ExpandButtonType.NotSet;
        private Image _ExpandButtonImage;
        private Image _ExpandButtonHotImage;
        private Image _CollapseButtonImage;
        private Image _CollapseButtonHotImage;

        private ProcessChildRelations _ProcessChildRelations = ProcessChildRelations.Always;

        #endregion

        /// <summary>
        /// GridPanel
        /// </summary>
        public GridPanel()
        {
            _ColumnAutoSizeMode = ColumnAutoSizeMode.None;

            _Columns = new GridColumnCollection();
            _Columns.ParentItem = this;

            _SortColumns = new List<GridColumn>();
            _GroupColumns = new List<GridColumn>();

            _ColumnHeader = new GridColumnHeader();
            _ColumnHeader.Parent = this;
            _ColumnHeader.Columns = _Columns;

            _SelectedRows = new SelectedElements();
            _SelectedColumns = new SelectedElements();
            _DeletedRows = new SelectedElements();

            _GridIndexArray = new GridContainer[64];
            _ExpDictionary = new Dictionary<GridCell, EEval>();
        }

        #region Hidden properties

        #region Expanded

        /// <summary>
        /// Expanded
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool Expanded
        {
            get { return (base.Expanded); }
        }

        #endregion

        #region Style

        /// <summary>
        /// Style
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new CellVisualStyles CellStyles
        {
            get { return (base.CellStyles); }
        }

        #endregion

        #region Style

        /// <summary>
        /// Style
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string RowHeaderText
        {
            get { return (base.RowHeaderText); }
        }

        #endregion

        #region RowStyles

        /// <summary>
        /// RowVisualStyles
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new RowVisualStyles RowStyles
        {
            get { return (base.RowStyles); }
        }

        #endregion

        #endregion

        #region Public properties

        #region ActiveRow

        /// <summary>
        /// Gets the current Active Row. This row will have the
        /// "Active Row" image presented in the row header (if
        /// ActiveRowImage is non-null).
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridContainer ActiveRow
        {
            get { return (_ActiveRow); }

            internal set
            {
                if (value != _ActiveRow)
                {
                    GridContainer oldActive = _ActiveRow;
                    _ActiveRow = value;

                    if (ActiveRowIndicatorStyle != ActiveRowIndicatorStyle.None)
                    {
                        if (oldActive != null)
                            InvalidateRowHeader(oldActive);

                        InvalidateRowHeader(_ActiveRow);
                    }
                }
            }
        }

        #endregion

        #region ActiveRowIndicatorStyle

        ///<summary>
        /// Gets or sets how the Active Row
        /// is visually indicated in the RowHeader.
        ///</summary>
        [DefaultValue(ActiveRowIndicatorStyle.Image), Category("Rows")]
        [Description("Indicates how the Active Row is visually indicated in the RowHeader.")]
        public ActiveRowIndicatorStyle ActiveRowIndicatorStyle
        {
            get { return (_ActiveRowIndicatorStyle); }

            set
            {
                if (_ActiveRowIndicatorStyle != value)
                {
                    _ActiveRowIndicatorStyle = value;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("ActiveRowIndicatorStyle", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region AllowEdit

        /// <summary>
        /// Gets or sets whether grid cells can be edited by the user. 
        /// </summary>
        [DefaultValue(true), Category("Behavior")]
        [Description("Indicates whether grid cells can be edited by the user.")]
        public bool AllowEdit
        {
            get { return (_AllowEdit); }

            set
            {
                if (value != _AllowEdit)
                {
                    _AllowEdit = value;

                    OnPropertyChangedEx("AllowEdit");
                }
            }
        }

        #endregion

        #region AllowEmptyCellSelection

        /// <summary>
        /// Gets or sets whether empty cells can be
        /// selected and interacted with by the user
        /// </summary>
        [DefaultValue(false), Category("Behavior")]
        [Description("Indicates whether empty cells can be selected and interacted with by the user.")]
        public bool AllowEmptyCellSelection
        {
            get { return (_AllowEmptyCellSelection); }

            set
            {
                if (value != _AllowEmptyCellSelection)
                {
                    _AllowEmptyCellSelection = value;

                    OnPropertyChangedEx("AllowEmptyCellSelection");
                }
            }
        }

        #endregion

        #region AllowRowDelete

        /// <summary>
        /// Gets or sets whether grid rows can be deleted by the user. 
        /// Selected rows are deleted by pressing the 'delete' key, and can
        /// be restored by pressing the 'shift-delete' key.
        /// </summary>
        [DefaultValue(false), Category("Behavior")]
        [Description("Indicates whether grid rows can be deleted by the user. Selected rows are deleted by pressing the 'delete' key, and can be restored by pressing the 'shift-delete' key.")]
        public bool AllowRowDelete
        {
            get { return (_AllowRowDelete); }

            set
            {
                if (value != _AllowRowDelete)
                {
                    _AllowRowDelete = value;

                    OnPropertyChangedEx("AllowRowDelete");
                }
            }
        }

        #endregion

        #region AllowRowHeaderResize

        /// <summary>
        /// Gets or sets whether grid row headers can be resized by the user.
        /// </summary>
        [DefaultValue(false), Category("Behavior")]
        [Description("Indicates whether grid row headers can be resized by the user.")]
        public bool AllowRowHeaderResize
        {
            get { return (_AllowRowHeaderResize); }

            set
            {
                if (value != _AllowRowHeaderResize)
                {
                    _AllowRowHeaderResize = value;

                    OnPropertyChangedEx("AllowRowHeaderResize");
                }
            }
        }

        #endregion

        #region AllowRowInsert

        /// <summary>
        /// Gets or sets whether grid rows can be inserted
        /// by the user. Rows can be inserted by selecting
        /// a row and pressing the 'insert' key.
        /// </summary>
        [DefaultValue(false), Category("Behavior")]
        [Description("Indicates whether grid rows can be inserted by the user. Rows can be inserted by selecting a row and pressing the 'insert' key.")]
        public bool AllowRowInsert
        {
            get { return (_AllowRowInsert); }

            set
            {
                if (value != _AllowRowInsert)
                {
                    _AllowRowInsert = value;

                    OnPropertyChangedEx("AllowRowInsert");
                }
            }
        }

        #endregion

        #region AllowRowResize

        /// <summary>
        /// Gets or sets whether grid rows can be resized by the user
        /// </summary>
        [DefaultValue(false), Category("Behavior")]
        [Description("Indicates whether grid rows can be resized by the user.")]
        public bool AllowRowResize
        {
            get { return (_AllowRowResize); }

            set
            {
                if (value != _AllowRowResize)
                {
                    _AllowRowResize = value;

                    OnPropertyChangedEx("AllowRowResize");
                }
            }
        }

        #endregion

        #region AutoExpandSetGroup

        ///<summary>
        /// Gets or sets whether new SetGroups are automatically Expanded
        ///</summary>
        [DefaultValue(false), Category("Group")]
        [Description("Indicates whether new SetGroups are automatically Expanded)")]
        public bool AutoExpandSetGroup
        {
            get { return (_AutoExpandSetGroup); }

            set
            {
                if (_AutoExpandSetGroup != value)
                {
                    _AutoExpandSetGroup = value;

                    OnPropertyChangedEx("AutoExpandSetGroup");
                }
            }
        }

        #endregion

        #region AutoGenerateColumns

        /// <summary>
        /// Gets or sets a whether columns are created
        /// automatically when the DataSource property is set
        /// </summary>
        [DefaultValue(true), Category("Data")]
        [Description("Indicates whether columns are created automatically when the DataSource property is set.")]
        public bool AutoGenerateColumns
        {
            get { return (_AutoGenerateColumns); }

            set
            {
                if (_AutoGenerateColumns != value)
                {
                    _AutoGenerateColumns = value;

                    if (value == true)
                        _NeedToUpdateBindings = true;

                    OnPropertyChangedEx("AutoGenerateColumns", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region AutoHideDeletedRows

        /// <summary>
        /// Gets or sets whether newly deleted rows are automatically hidden.
        /// </summary>
        [DefaultValue(true), Category("Behavior")]
        [Description("Indicates whether newly deleted rows are automatically hidden.")]
        public bool AutoHideDeletedRows
        {
            get { return (_AutoHideDeletedRows); }

            set
            {
                if (_AutoHideDeletedRows != value)
                {
                    _AutoHideDeletedRows = value;

                    OnPropertyChangedEx("AutoHideDeletedRows");
                }
            }
        }

        #endregion

        #region AutoSelectDeleteBoundRows

        /// <summary>
        /// Gets or sets whether the grid will automatically select the
        /// next positionally available row following a bound data row deletion.
        /// </summary>
        [DefaultValue(true), Category("Behavior")]
        [Description("Indicates whether the grid will automatically select the next positionally available row following a bound data row deletion.")]
        public bool AutoSelectDeleteBoundRows
        {
            get { return (_AutoSelectDeleteBoundRows); }

            set
            {
                if (_AutoSelectDeleteBoundRows != value)
                {
                    _AutoSelectDeleteBoundRows = value;

                    OnPropertyChangedEx("AutoSelectDeleteBoundRows");
                }
            }
        }

        #endregion

        #region AutoSelectNewBoundRows

        /// <summary>
        /// Gets or sets whether newly added rows
        /// (when bound to a data source) are automatically selected.
        /// </summary>
        [DefaultValue(true), Category("Behavior")]
        [Description("Indicates whether newly added rows (when bound to a data source) are automatically selected.")]
        public bool AutoSelectNewBoundRows
        {
            get { return (_AutoSelectNewBoundRows); }

            set
            {
                if (_AutoSelectNewBoundRows != value)
                {
                    _AutoSelectNewBoundRows = value;

                    OnPropertyChangedEx("AutoSelectNewBoundRows");
                }
            }
        }

        #endregion

        #region Bounds

        ///<summary>
        /// Gets the scroll adjusted bounds of the Grid Panel
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Rectangle Bounds
        {
            get
            {
                Rectangle r = BoundsRelative;

                if (IsVFrozen == false)
                {
                    if (IsSubPanel == true)
                        r.X -= HScrollOffset;

                    r.Y -= VScrollOffset;
                }

                return (r);
            }
        }

        #endregion

        #region CanDeleteRow

        ///<summary>
        /// Gets whether row deletes are permitted
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanDeleteRow
        {
            get
            {
                if (AllowRowDelete == false)
                    return (false);

                if (GroupColumns != null && GroupColumns.Count > 0)
                    return (false);

                return (DataBinder.CanDeleteRow);
            }
        }

        #endregion

        #region CanInsertRow

        ///<summary>
        /// Gets whether row inserts are permitted
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanInsertRow
        {
            get
            {
                if (AllowRowInsert == false)
                    return (false);

                if (GroupColumns != null && GroupColumns.Count > 0)
                    return (false);

                return (DataBinder.CanInsertRow);
            }
        }

        #endregion

        #region CanMoveRow

        ///<summary>
        /// Gets whether row moves are permitted
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanMoveRow
        {
            get
            {
                if (VirtualMode == false ||
                    (RowDragBehavior != RowDragBehavior.Move && RowDragBehavior != RowDragBehavior.GroupMove))
                {
                    return (false);
                }

                return (DataBinder.CurrencyManager == null);
            }
        }
        #endregion

        #region Caption

        ///<summary>
        /// Gets or sets the Caption, which is displayed at the
        /// very top of the panel.  The Caption is fixed
        /// and it does not move as grid rows are scrolled.
        ///</summary>
        ///<exception cref="ArgumentException"></exception>
        [Category("Header,Footer...")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("Indicates the Caption item, which is displayed at the very top of the panel. The Caption is fixed and it does not move as grid rows are scrolled.")]
        public GridCaption Caption
        {
            get
            {
                if (_Caption == null)
                    Caption = new GridCaption();

                return (_Caption);
            }

            set
            {
                if (_Caption != value)
                {
                    if (value != null)
                    {
                        if (value.Parent != null)
                            throw new ArgumentException("Item assigned as Caption already has a parent.");

                    }

                    _Caption = value;

                    if (_Caption != null)
                        _Caption.Parent = this;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("Caption", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region CellDragBehavior

        /// <summary>
        /// Gets or sets the behavior when a cell is clicked and dragged
        /// </summary>
        [DefaultValue(CellDragBehavior.ExtendSelection), Category("Behavior")]
        [Description("Indicates the behavior when a cell is clicked and dragged.")]
        public CellDragBehavior CellDragBehavior
        {
            get { return (_CellDragBehavior); }

            set
            {
                if (value != _CellDragBehavior)
                {
                    _CellDragBehavior = value;

                    OnPropertyChangedEx("CellDragBehavior");
                }
            }
        }

        #endregion

        #region CheckBoxes

        /// <summary>
        /// Get or sets whether CheckBoxes are present in the grid rows.
        /// </summary>
        [DefaultValue(false), Category("Appearance")]
        [Description("Indicates whether CheckBoxes are present in the grid rows.")]
        public bool CheckBoxes
        {
            get { return (_CheckBoxes); }

            set
            {
                if (_CheckBoxes != value)
                {
                    _CheckBoxes = value;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("CheckBoxes", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region CheckBoxSize

        ///<summary>
        /// Gets or sets the CheckBox size
        ///</summary>
        [Category("Appearance")]
        [Description("Indicates the CheckBox size.")]
        public Size CheckBoxSize
        {
            get { return (_CheckBoxSize); }

            set
            {
                if (value != _CheckBoxSize)
                {
                    _CheckBoxSize = value;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("CheckBoxSize", VisualChangeType.Layout);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal bool ShouldSerializeCheckBoxSize()
        {
            return (_CheckBoxSize.Height != 13 || _CheckBoxSize.Width != 13);
        }

        /// <summary>
        /// Resets property to its default value
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal void ResetCheckBoxSize()
        {
            CheckBoxSize = new Size(13, 13);
        }

        #endregion

        #region CollapseImage

        /// <summary>
        /// Gets or sets the default TreeButton collapse image
        /// </summary>
        [DefaultValue(null), Category("Appearance")]
        [Description("Indicates the default TreeButton collapse image.")]
        public Image CollapseImage
        {
            get { return (_CollapseImage); }

            set
            {
                if (value != _CollapseImage)
                {
                    _CollapseImage = value;

                    CollapseButtonImage = null;
                    CollapseButtonHotImage = null;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("CollapseImage", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region ColumnAutoSizeMode

        /// <summary>
        /// Gets or sets a value indicating the default mode for determining column widths
        /// </summary>
        [DefaultValue(ColumnAutoSizeMode.None), Category("Columns")]
        [Description("Indicates the default mode for determining column widths.")]
        public ColumnAutoSizeMode ColumnAutoSizeMode
        {
            get { return (_ColumnAutoSizeMode); }

            set
            {
                if (value != _ColumnAutoSizeMode)
                {
                    _ColumnAutoSizeMode = value;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("ColumnAutoSizeMode", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region ColumnHeader

        /// <summary>
        /// Gets the column header for the grid. The Column Header
        /// is the container object responsible for the definition
        /// and control of the Column Header row (container each
        /// column header text, etc).
        /// </summary>
        [Category("Columns")]
        [Description("Indicates the column header for the grid")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GridColumnHeader ColumnHeader
        {
            get { return (_ColumnHeader); }
        }

        #endregion

        #region ColumnHeaderClickBehavior

        /// <summary>
        /// Gets or sets the behavior when a column header is clicked.
        /// </summary>
        [Category("Columns")]
        [DefaultValue(ColumnHeaderClickBehavior.SortAndReorder)]
        [Description("Indicates the behavior when a column header is clicked.")]
        public ColumnHeaderClickBehavior ColumnHeaderClickBehavior
        {
            get { return (_ColumnHeaderClickBehavior); }

            set
            {
                if (value != _ColumnHeaderClickBehavior)
                {
                    _ColumnHeaderClickBehavior = value;

                    OnPropertyChangedEx("ColumnHeaderClickBehavior");
                }
            }
        }

        #endregion

        #region ColumnDragBehavior

        /// <summary>
        /// Gets or sets the behavior when a column header is dragged
        /// </summary>
        [DefaultValue(ColumnDragBehavior.ExtendClickBehavior), Category("Columns")]
        [Description("Indicates the behavior when a column header is dragged.")]
        public ColumnDragBehavior ColumnDragBehavior
        {
            get { return (_ColumnDragBehavior); }

            set
            {
                if (value != _ColumnDragBehavior)
                {
                    _ColumnDragBehavior = value;

                    OnPropertyChangedEx("ColumnDragBehavior");
                }
            }
        }

        #endregion

        #region Columns

        /// <summary>
        /// Gets a reference to the collection of grid columns
        /// </summary>
        [Category("Columns")]
        [Description("Indicates the collection of grid columns.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GridColumnCollection Columns
        {
            get { return (_Columns); }
        }

        #endregion

        #region DataMember

        ///<summary>
        /// Gets or sets the name of the list or table
        /// in the data source that the grid is bound to.
        ///</summary>
        [DefaultValue(null), Category("Data")]
        [Description("Indicates the name of the list or table in the data source that the grid is bound to.")]
        public string DataMember
        {
            get { return (_DataMember); }

            set
            {
                if (_DataBinder != null)
                {
                    _DataBinder.Clear();

                    if (VirtualMode == false)
                        Rows.Clear();
                }

                if (_DataMember != value)
                {
                    ClearSort();
                    ClearGroup();
                }

                _DataMember = value;

                NeedToUpdateBindings = true;
                NeedToUpdateDataFilter = true;

                NeedsMeasured = true;

                OnPropertyChangedEx("DataMember", VisualChangeType.Layout);
            }
        }

        #region SetDataMember

        internal void SetDataMember(string dataMember)
        {
            _DataMember = dataMember;
        }

        #endregion

        #endregion

        #region DataSource

        ///<summary>
        /// Gets or sets the data source that the grid is bound to
        ///</summary>
        [DefaultValue(null), AttributeProvider(typeof(IListSource)), Category("Data")]
        [Description("Indicates the data source that the grid is bound to.")]
        public object DataSource
        {
            get { return (_DataSource); }

            set
            {
                if (_DataBinder != null)
                    _DataBinder.Clear();

                if (_AutoGenerateColumns == true)
                {
                    ClearSort();
                    ClearGroup();
                }

                _DataSource = value;

                NeedToUpdateBindings = true;
                NeedToUpdateDataFilter = true;

                NeedsSorted = true;
                NeedsMeasured = true;

                OnPropertyChangedEx("DataSource", VisualChangeType.Layout);
            }
        }

        #region SetDataSource

        internal void SetDataSource(object dataSource)
        {
            _DataSource = dataSource;
        }

        #endregion

        #endregion

        #region DefaultPostDetailRowHeight

        ///<summary>
        /// Gets or sets the default post-detail row height (default is 0)
        ///</summary>
        [DefaultValue(0), Category("Rows")]
        [Description("Indicates the default post-detail row height (default is 0)")]
        public int DefaultPostDetailRowHeight
        {
            get { return (_DefaultPostDetailRowHeight); }

            set
            {
                if (_DefaultPostDetailRowHeight != value)
                {
                    if (value < 0)
                        throw new ArgumentException("Value must be 0 or greater.");

                    _DefaultPostDetailRowHeight = value;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("DefaultPostDetailRowHeight", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region DefaultPreDetailRowHeight

        ///<summary>
        /// Gets or sets the default pre-detail row height (default is 0)
        ///</summary>
        [DefaultValue(0), Category("Rows")]
        [Description("Indicates the default pre-detail row height (default is 0)")]
        public int DefaultPreDetailRowHeight
        {
            get { return (_DefaultPreDetailRowHeight); }

            set
            {
                if (_DefaultPreDetailRowHeight != value)
                {
                    if (value < 0)
                        throw new ArgumentException("Value must be 0 or greater.");

                    _DefaultPreDetailRowHeight = value;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("DefaultPreDetailRowHeight", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region DefaultRowHeight

        ///<summary>
        /// Gets or sets the default row height (default is 20, 0 to autoSize)
        ///</summary>
        [DefaultValue(22), Category("Rows")]
        [Description("Indicates the default row height (default is 20, 0 for autoSize)")]
        public int DefaultRowHeight
        {
            get { return (_DefaultRowHeight); }

            set
            {
                if (_DefaultRowHeight != value)
                {
                    if (value < 0)
                        throw new ArgumentException("Value must be 0 or greater.");

                    _DefaultRowHeight = value;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("DefaultRowHeight", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region DefaultVisualStyles

        ///<summary>
        /// Gets or sets the Default Visual Styles for each grid element
        ///</summary>
        [Browsable(true), Category("Style")]
        [Description("Gets or sets the Default Visual Styles for the each grid element.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DefaultVisualStyles DefaultVisualStyles
        {
            get
            {
                if (_DefaultVisualStyles == null)
                {
                    _DefaultVisualStyles = new DefaultVisualStyles();

                    DefaultVisualChangeHandler(null, _DefaultVisualStyles);
                }

                return (_DefaultVisualStyles);
            }

            set
            {
                if (_DefaultVisualStyles != value)
                {
                    DefaultVisualStyles oldValue = _DefaultVisualStyles;
                    _DefaultVisualStyles = value;

                    OnDefaultVisualStyleChanged(oldValue, value);

                    DefaultVisualChangeHandler(oldValue, value);
                }
            }
        }

        #region OnDefaultVisualStyleChanged

        private void OnDefaultVisualStyleChanged(
            DefaultVisualStyles oldValue, DefaultVisualStyles newValue)
        {
            DefaultVisualChangeHandler(oldValue, newValue);

            OnPropertyChanged(new PropertyChangedEventArgs("DefaultVisualStyles"));
        }

        #endregion

        #region DefaultVisualChangeHandler

        private void DefaultVisualChangeHandler(
            DefaultVisualStyles oldValue, DefaultVisualStyles newValue)
        {
            NeedsMeasured = true;

            if (oldValue != null)
                oldValue.PropertyChanged -= DefaultVisualStylesPropertyChanged;

            if (newValue != null)
                newValue.PropertyChanged += DefaultVisualStylesPropertyChanged;
        }

        #endregion

        #endregion

        #region DeletedRowCount

        ///<summary>
        /// Gets the current number of deleted rows.
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int DeletedRowCount
        {
            get { return (_DeletedRows.Count); }
        }

        #endregion

        #region DeletedRows

        ///<summary>
        /// IEnumerable that can be used to enumerate through
        /// all root level deleted rows
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable DeletedRows
        {
            get { return (new SelectedRowsEnumeration(this, _DeletedRows)); }
        }

        #endregion

        #region EnableCellExpressions

        /// <summary>
        /// Gets or sets whether cell expression evaluation is enabled.
        /// </summary>
        [DefaultValue(false), Category("Behavior")]
        [Description("Indicates whether cell expression evaluation is enabled. Cell expressions begin with an equals sign '='.")]
        public bool EnableCellExpressions
        {
            get { return (_EnableCellExpressions); }

            set
            {
                if (_EnableCellExpressions != value)
                {
                    _EnableCellExpressions = value;

                    OnPropertyChangedEx("EnableCellExpressions", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region EnableColumnFiltering

        /// <summary>
        /// Gets or sets whether column level filtering is enabled.
        /// </summary>
        [DefaultValue(false), Category("Filtering")]
        [Description("Indicates whether column level filtering is enabled.")]
        public bool EnableColumnFiltering
        {
            get { return (_EnableColumnFiltering); }

            set
            {
                if (_EnableColumnFiltering != value)
                {
                    _EnableColumnFiltering = value;

                    NeedToUpdateDataFilter = true;

                    OnPropertyChangedEx("EnableColumnFiltering", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region EnableFiltering

        /// <summary>
        /// Gets or sets whether filtering in general is enabled.
        /// </summary>
        [DefaultValue(false), Category("Filtering")]
        [Description("Indicates whether filtering in general is enabled.")]
        public bool EnableFiltering
        {
            get { return (_EnableFiltering); }

            set
            {
                if (_EnableFiltering != value)
                {
                    _EnableFiltering = value;

                    _NeedToUpdateDataFilter = true;

                    OnPropertyChangedEx("EnableFiltering", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region EnableNoRowsMarkup

        /// <summary>
        /// Gets or sets whether text-markup support is enabled for the NoRowsText
        /// </summary>
        [DefaultValue(false), Category("Appearance")]
        [Description("Indicates whether text-markup support is enabled for the NoRowsText.")]
        public bool EnableNoRowsMarkup
        {
            get { return (_EnableNoRowsMarkup); }

            set
            {
                if (_EnableNoRowsMarkup != value)
                {
                    _EnableNoRowsMarkup = value;

                    NoRowsMarkupTextChanged();

                    OnPropertyChangedEx("EnableNoRowsMarkup", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region EnableRowFiltering

        /// <summary>
        /// Gets or sets whether row level filtering is enabled.
        /// </summary>
        [DefaultValue(false), Category("Filtering")]
        [Description("Indicates whether row level filtering is enabled.")]
        public bool EnableRowFiltering
        {
            get { return (_EnableRowFiltering); }

            set
            {
                if (_EnableRowFiltering != value)
                {
                    _EnableRowFiltering = value;

                    NeedToUpdateDataFilter = true;

                    OnPropertyChangedEx("EnableRowFiltering", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region EnableSelectionBuffering

        /// <summary>
        /// Gets or sets whether selection buffering is enabled, such
        /// that only one SelectionChange event is raised per output refresh.
        /// </summary>
        [DefaultValue(true), Category("Behavior")]
        [Description("Indicates whether selection buffering is enabled, such that only one SelectionChange event is raised per output refresh..")]
        public bool EnableSelectionBuffering
        {
            get { return (_EnableSelectionBuffering); }

            set
            {
                if (_EnableSelectionBuffering != value)
                {
                    _EnableSelectionBuffering = value;

                    OnPropertyChangedEx("EnableSelectionBuffering");
                }
            }
        }

        #endregion

        #region EnsureVisibleAfterGrouping

        /// <summary>
        /// Gets or sets whether EnsureVisible will be called
        /// for the current ActiveRow following a grouping operation.
        /// </summary>
        [DefaultValue(false), Category("Behavior")]
        [Description("Indicates whether EnsureVisible will be called for the current ActiveRow following a grouping operation.")]
        public bool EnsureVisibleAfterGrouping
        {
            get { return (_EnsureVisibleAfterGrouping); }

            set
            {
                if (value != _EnsureVisibleAfterGrouping)
                {
                    _EnsureVisibleAfterGrouping = value;

                    OnPropertyChangedEx("EnsureVisibleAfterGrouping");
                }
            }
        }

        #endregion

        #region EnsureVisibleAfterSort

        /// <summary>
        /// Gets or sets whether EnsureVisible will be called
        /// for the current ActiveRow following a sort operation.
        /// </summary>
        [DefaultValue(false), Category("Behavior")]
        [Description("Indicates whether EnsureVisible will be called for the current ActiveRow following a sort operation.")]
        public bool EnsureVisibleAfterSort
        {
            get { return (_EnsureVisibleAfterSort); }

            set
            {
                if (value != _EnsureVisibleAfterSort)
                {
                    _EnsureVisibleAfterSort = value;

                    OnPropertyChangedEx("EnsureVisibleAfterSort");
                }
            }
        }

        #endregion

        #region EnterKeySelectsNextRow

        /// <summary>
        /// Gets or sets whether the EnterKey selects the
        /// next available row, or stays on the current row
        /// </summary>
        [DefaultValue(true), Category("Behavior")]
        [Description("Indicates whether the EnterKey selects the next available row, or stays on the current row.")]
        public bool EnterKeySelectsNextRow
        {
            get { return (_EnterKeySelectsNextRow); }

            set
            {
                if (_EnterKeySelectsNextRow != value)
                {
                    _EnterKeySelectsNextRow = value;

                    OnPropertyChangedEx("EnterKeySelectsNextRow");
                }
            }
        }

        #endregion

        #region ExpandButtonType

        /// <summary>
        /// Gets or sets the ExpandButton Type
        /// </summary>
        [DefaultValue(ExpandButtonType.NotSet), Category("Appearance")]
        [Description("Indicates the ExpandButton Type.")]
        public ExpandButtonType ExpandButtonType
        {
            get { return (_ExpandButtonType); }

            set
            {
                if (_ExpandButtonType != value)
                {
                    _ExpandButtonType = value;

                    ExpandButtonImage = null;
                    ExpandButtonHotImage = null;
                    CollapseButtonImage = null;
                    CollapseButtonHotImage = null;

                    OnPropertyChangedEx("ExpandButtonType", VisualChangeType.Layout);
                }
            }
        }

        #region GetExpandButtonType

        internal ExpandButtonType GetExpandButtonType()
        {
            ExpandButtonType type = _ExpandButtonType;

            if (type == ExpandButtonType.NotSet)
                type = SuperGrid.ExpandButtonType;

            return (type != ExpandButtonType.NotSet ? type : ExpandButtonType.Square);
        }

        #endregion

        #endregion

        #region ExpandImage

        /// <summary>
        /// Gets or sets the default TreeButton expand image
        /// </summary>
        [DefaultValue(null), Category("Appearance")]
        [Description("Indicates the default TreeButton expand image.")]
        public Image ExpandImage
        {
            get { return (_ExpandImage); }

            set
            {
                if (value != _ExpandImage)
                {
                    _ExpandImage = value;

                    ExpandButtonImage = null;
                    ExpandButtonHotImage = null;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("ExpandImage", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region Filter

        /// <summary>
        /// Gets or sets the Filter item which is displayed just below
        /// the Column Header. The Filter area is fixed and it does
        /// not move as grid rows are scrolled.
        /// </summary>
        [Category("Filtering")]
        [Description("Indicates the Filter item, which is displayed just below the Column Header. The Filter area is fixed and it does not move as grid rows are scrolled.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GridFilter Filter
        {
            get
            {
                if (_Filter == null)
                    Filter = new GridFilter();

                return (_Filter);
            }

            set
            {
                if (_Filter != value)
                {
                    if (value != null)
                    {
                        if (value.Parent != null)
                            throw new ArgumentException("Item assigned as Filter already has a parent.");
                    }

                    _Filter = value;

                    if (_Filter != null)
                        _Filter.Parent = this;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("Filter", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region FilterExpr

        /// <summary>
        /// Gets or sets the Filter expression.
        /// </summary>
        [DefaultValue(null), Category("Filtering")]
        [Description("Indicates the Filter expression.")]
        public string FilterExpr
        {
            get
            {
                return (_FilterExpr);
            }

            set
            {
                if (_FilterExpr != value)
                {
                    _FilterExpr = value;
                    _FilterEval = null;

                    NeedToUpdateDataFilter = true;

                    OnPropertyChangedEx("FilterExpr", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region FilterIgnoreMatchCase

        /// <summary>
        /// Gets or sets the filter data match ignores string case
        /// </summary>
        [DefaultValue(true), Category("Filtering")]
        [Description("Indicates the filter data match ignores string case).")]
        public bool FilterIgnoreMatchCase
        {
            get { return (_FilterIgnoreMatchCase); }

            set
            {
                if (_FilterIgnoreMatchCase != value)
                {
                    _FilterIgnoreMatchCase = value;

                    _FilterEval = null;
                    ClearColumnFilterEvals();

                    NeedToUpdateDataFilter = true;

                    OnPropertyChangedEx("FilterIgnoreCase", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region FilterLevel

        /// <summary>
        /// Gets or sets the column Filtering level. 
        /// </summary>
        [DefaultValue(FilterLevel.Root), Category("Filtering")]
        [Description("Indicates the column filtering level.")]
        public FilterLevel FilterLevel
        {
            get { return (_FilterLevel); }

            set
            {
                if (_FilterLevel != value)
                {
                    _FilterLevel = value;

                    NeedToUpdateDataFilter = true;

                    OnPropertyChangedEx("FilterLevel", VisualChangeType.Layout);
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

                    ClearColumnFilterEvals();

                    NeedToUpdateDataFilter = true;

                    OnPropertyChangedEx("FilterMatchType", VisualChangeType.Layout);
                }
            }
        }

        #region GetFilterMatchType

        internal FilterMatchType GetFilterMatchType()
        {
            if (_FilterMatchType != FilterMatchType.NotSet)
                return (_FilterMatchType);
            
            return (FilterMatchType.None);
        }

        #endregion

        #endregion

        #region FirstFrozenColumn

        /// <summary>
        /// Gets a reference to the first Frozen column
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridColumn FirstFrozenColumn
        {
            get
            {
                int[] displayMap = Columns.DisplayIndexMap;

                for (int i = 0; i < displayMap.Length; i++)
                {
                    GridColumn column = Columns[displayMap[i]];

                    if (column.Visible == true && column.IsHFrozen == true)
                        return (column);
                }

                return (null);
            }
        }

        #endregion

        #region FirstOnScreenColumn

        /// <summary>
        /// Gets a reference to the first on-screen column
        /// (after frozen columns)
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridColumn FirstOnScreenColumn
        {
            get
            {
                int[] displayMap = Columns.DisplayIndexMap;

                for (int i = 0; i < displayMap.Length; i++)
                {
                    GridColumn column = Columns[displayMap[i]];

                    if (column.IsHFrozen == false && column.IsOnScreen == true)
                        return (column);
                }

                return (null);
            }
        }

        #endregion

        #region FirstVisibleColumn

        /// <summary>
        /// Gets a reference to the first visible column
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridColumn FirstVisibleColumn
        {
            get { return (_Columns.FirstVisibleColumn); }
        }

        #endregion

        #region FlatCheckedRows

        ///<summary>
        /// IEnumerable that can be used to enumerate through
        /// all levels of checked rows as though they were at
        /// the root level
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable FlatCheckedRows
        {
            get { return (new FlatCheckedRowsEnumeration(this, false)); }
        }

        #endregion

        #region FlatCheckedExpandedRows

        ///<summary>
        /// IEnumerable that can be used to enumerate through
        /// all levels of checked, expanded rows as though they
        /// were at the root level
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable FlatCheckedExpandedRows
        {
            get { return (new FlatCheckedRowsEnumeration(this, true)); }
        }

        #endregion

        #region FlatDeletedRows

        ///<summary>
        /// IEnumerable that can be used to enumerate through
        /// all levels of deleted rows as though they were at
        /// the root level
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable FlatDeletedRows
        {
            get { return (new FlatDeletedRowsEnumeration(this)); }
        }

        #endregion

        #region FlatExpandedRows

        ///<summary>
        /// IEnumerable that can be used to enumerate through
        /// all levels of expanded rows as though they were at
        /// the root level
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable FlatExpandedRows
        {
            get { return (new FlatRowsEnumeration(this, true, false)); }
        }

        #endregion

        #region FlatRows

        ///<summary>
        /// IEnumerable that can be used to enumerate through
        /// all levels of rows as though they were at the root level
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable FlatRows
        {
            get { return (new FlatRowsEnumeration(this, false, false)); }
        }

        #endregion

        #region FlatVisibleExpandedRows

        ///<summary>
        /// IEnumerable that can be used to enumerate through
        /// all levels of visible, expanded rows as though they
        /// were at the root level
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable FlatVisibleExpandedRows
        {
            get { return (new FlatRowsEnumeration(this, true, true)); }
        }

        #endregion

        #region FlatVisibleRows

        ///<summary>
        /// IEnumerable that can be used to enumerate through
        /// all levels of visible rows as though they
        /// were at the root level
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable FlatVisibleRows
        {
            get { return (new FlatRowsEnumeration(this, false, true)); }
        }

        #endregion

        #region FocusCuesEnabled

        /// <summary>
        /// Gets or sets whether focus cues are displayed on the focused element
        /// </summary>
        [DefaultValue(true), Category("Appearance")]
        [Description("Indicates whether focus cues are displayed on the focused element.")]
        public bool FocusCuesEnabled
        {
            get { return (_FocusCuesEnabled); }

            set
            {
                if (_FocusCuesEnabled != value)
                {
                    _FocusCuesEnabled = value;

                    OnPropertyChangedEx("FocusCuesEnabled", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region Footer

        /// <summary>
        /// Gets or sets the Footer, which is displayed at the
        /// very bottom of the panel.  The Footer is fixed
        /// and it does not move as grid rows are scrolled.
        /// </summary>
        [Category("Header,Footer...")]
        [Description("Indicates the Footer item, which is displayed at the very bottom of the panel. The Footer is fixed and does not move as grid rows are scrolled.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GridFooter Footer
        {
            get
            {
                if (_Footer == null)
                    Footer = new GridFooter();

                return (_Footer);
            }

            set
            {
                if (_Footer != value)
                {
                    if (value != null)
                    {
                        if (value.Parent != null)
                            throw new ArgumentException("Item assigned as Footer already has a parent.");
                    }

                    _Footer = value;

                    NeedsMeasured = true;

                    if (_Footer != null)
                        _Footer.Parent = this;

                    OnPropertyChangedEx("Footer", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region FrozenColumnCount

        /// <summary>
        /// Gets or sets the number of columns that are frozen, counted
        /// from the left. Frozen columns are always visible regardless
        /// of the horizontal scroll position.
        /// </summary>
        [DefaultValue(0), Category("Columns")]
        [Description("Indicates the number of columns that are frozen, counted from the left. Frozen columns are always visible regardless of the horizontal scroll position.")]
        public int FrozenColumnCount
        {
            get { return (_FrozenColumnCount); }

            set
            {
                if (value != _FrozenColumnCount)
                {
                    _FrozenColumnCount = value;

                    OnPropertyChangedEx("FrozenColumnCount", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region FrozenRowCount

        /// <summary>
        /// Gets or sets the number of rows that are frozen, counted
        /// from the top. Frozen rows are always visible regardless
        /// of the vertical scroll position.
        /// </summary>
        [DefaultValue(0), Category("Rows")]
        [Description("Indicates the number of rows that are frozen, counted from the top. Frozen rows are always visible regardless of the vertical scroll position.")]
        public int FrozenRowCount
        {
            get { return (_FrozenRowCount); }

            set
            {
                if (value != _FrozenRowCount)
                {
                    _FrozenRowCount = value;

                    OnPropertyChangedEx("FrozenRowCount", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region GridLines

        /// <summary>
        /// Gets or sets which grid lines (horizontal and vertical) are displayed in the grid
        /// </summary>
        [DefaultValue(GridLines.Both), Category("Appearance")]
        [Description("Indicates which grid lines (horizontal and vertical) are displayed in the grid.")]
        public GridLines GridLines
        {
            get { return (_GridLines); }

            set
            {
                if (_GridLines != value)
                {
                    _GridLines = value;

                    OnPropertyChangedEx("FrozenRowCount", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region GroupBy

        /// <summary>
        /// Gets or sets the row which is displayed just above
        /// the Column Header, and is used to permit display and user control
        /// of column grouping. The GroupByRow is fixed and it does
        /// not move as grid rows are scrolled.
        /// </summary>
        [Category("Header,Footer...")]
        [Description("Indicates the row, which is displayed just above the Column Header, and is used to display and permit user control of column grouping. The GroupByRow is fixed and it does not move as grid rows are scrolled.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GridGroupByRow GroupByRow
        {
            get
            {
                if (_GroupByRow == null)
                    GroupByRow = new GridGroupByRow();

                return (_GroupByRow);
            }

            set
            {
                if (_GroupByRow != value)
                {
                    if (value != null)
                    {
                        if (value.Parent != null)
                            throw new ArgumentException("Item assigned as GroupByRow already has a parent.");
                    }

                    _GroupByRow = value;

                    if (_GroupByRow != null)
                        _GroupByRow.Parent = this;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("GroupByRow", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region GroupColumns

        /// <summary>
        /// Gets or sets the panel Group columns
        /// </summary>
        public List<GridColumn> GroupColumns
        {
            get { return (_GroupColumns); }
        }

        #endregion

        #region GroupHeaderClickBehavior

        /// <summary>
        /// Gets or sets the behavior when a Group header is clicked
        /// </summary>
        [DefaultValue(GroupHeaderClickBehavior.None), Category("Group")]
        [Description("Indicates the behavior when a Group header is clicked.")]
        public GroupHeaderClickBehavior GroupHeaderClickBehavior
        {
            get { return (_GroupHeaderClickBehavior); }

            set
            {
                if (_GroupHeaderClickBehavior != value)
                {
                    _GroupHeaderClickBehavior = value;

                    OnPropertyChangedEx("GroupHeaderClickBehavior");
                }
            }
        }

        #endregion

        #region GroupHeaderKeyBehavior

        /// <summary>
        /// Gets or sets the behavior when a Group header
        /// is encountered during keyboard navigation
        /// </summary>
        [DefaultValue(GroupHeaderKeyBehavior.Skip), Category("Group")]
        [Description("Indicates the behavior when a Group header is encountered during keyboard navigation.")]
        public GroupHeaderKeyBehavior GroupHeaderKeyBehavior
        {
            get { return (_GroupHeaderKeyBehavior); }

            set
            {
                if (_GroupHeaderKeyBehavior != value)
                {
                    _GroupHeaderKeyBehavior = value;

                    OnPropertyChangedEx("GroupHeaderKeyBehavior");
                }
            }
        }

        #endregion

        #region GroupHeaderHeight

        /// <summary>
        /// Gets or sets the height of the Group header 
        /// </summary>
        [DefaultValue(30), Category("Group")]
        [Description("Indicates the height of the Group header.")]
        public int GroupHeaderHeight
        {
            get { return (_GroupHeaderHeight); }

            set
            {
                if (_GroupHeaderHeight != value)
                {
                    _GroupHeaderHeight = value;

                    OnPropertyChangedEx("GroupHeaderHeight", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region GroupRowHeaderVisibility

        /// <summary>
        /// Gets or sets whether the Group RowHeader is displayed
        /// </summary>
        [DefaultValue(RowHeaderVisibility.PanelControlled), Category("Group")]
        [Description("Indicates whether the Group RowHeader is displayed.")]
        public virtual RowHeaderVisibility GroupRowHeaderVisibility
        {
            get { return (_GroupRowHeaderVisibility); }

            set
            {
                if (value != _GroupRowHeaderVisibility)
                {
                    _GroupRowHeaderVisibility = value;

                    OnPropertyChangedEx("GroupRowHeaderVisibility", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region Header

        /// <summary>
        /// Gets or sets the Header item, which is displayed 
        /// just below the column header. The Header item is fixed
        /// and does not move as grid rows are scrolled.
        /// </summary>
        [Category("Header,Footer...")]
        [Description("Indicates the Header item, which is displayed just below the column header. The Header is fixed and does not move as grid rows are scrolled.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GridHeader Header
        {
            get
            {
                if (_Header == null)
                    Header = new GridHeader();

                return (_Header);
            }

            set
            {
                if (_Header != value)
                {
                    if (value != null)
                    {
                        if (value.Parent != null)
                            throw new ArgumentException("Item assigned as Header already has a parent.");
                    }

                    _Header = value;

                    NeedsMeasured = true;

                    if (_Header != null)
                        _Header.Parent = this;

                    OnPropertyChangedEx("Header", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region ImageList

        /// <summary>
        /// Gets or sets the ImageList used by the Grid elements
        /// </summary>
        [Browsable(true), Category("Appearance"), DefaultValue(null)]
        [Description("Indicates the ImageList used by the Grid elements.")]
        public ImageList ImageList
        {
            get { return (_ImageList); }

            set
            {
                if (_ImageList != value)
                {
                    _ImageList = value;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("ImageList", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region ImmediateResize

         ///<summary>
         /// Gets or sets whether resizing panel columns
         /// and rows occurs immediately
         ///</summary>
        [DefaultValue(false), Category("Behavior")]
        [Description("Indicates whether resizing panel columns and rows occurs immediately.")]
        public bool ImmediateResize
        {
            get { return (_ImmediateResize); }

            set
            {
                if (_ImmediateResize != value)
                {
                    _ImmediateResize = value;

                    OnPropertyChangedEx("ImmediateResize");
                }
            }
        }

        #endregion

        #region SortUsingHiddenColumns

        ///<summary>
        /// Gets or sets whether hidden Columns
        /// are used when sorting the grid data
        ///</summary>
        [DefaultValue(false), Category("Behavior")]
        [Description("Indicates whether hidden Columns are used when sorting the grid data.")]
        public bool SortUsingHiddenColumns
        {
            get { return (_SortUsingHiddenColumns); }

            set
            {
                if (_SortUsingHiddenColumns != value)
                {
                    _SortUsingHiddenColumns = value;

                    if (SortColumns != null && SortColumns.Count > 0)
                        NeedsSorted = true;

                    OnPropertyChangedEx("SortUsingHiddenColumns", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region IndentGroups

        /// <summary>
        /// Gets or sets whether Nested Groups are indented
        /// </summary>
        [DefaultValue(true), Category("Group")]
        [Description("Indicates whether Nested Groups are indented.")]
        public bool IndentGroups
        {
            get { return (_IndentGroups); }

            set
            {
                if (value != _IndentGroups)
                {
                    _IndentGroups = value;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("IndentGroups", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region InitialActiveRow

        /// <summary>
        /// Gets or sets the initial Active Row
        /// </summary>
        [DefaultValue(RelativeRow.FirstRow), Category("Selection")]
        [Description("Indicates the initial Active Row")]
        public RelativeRow InitialActiveRow
        {
            get { return (_InitialActiveRow); }

            set
            {
                if (value != _InitialActiveRow)
                {
                    _InitialActiveRow = value;

                    OnPropertyChangedEx("InitialActiveRow");
                }
            }
        }

        #endregion

        #region InitialSelection

        /// <summary>
        /// Gets or sets the initial relative row / cell selection
        /// </summary>
        [DefaultValue(RelativeSelection.FirstCell), Category("Selection")]
        [Description("Indicates the initial relative row / cell selection.")]
        public RelativeSelection InitialSelection
        {
            get { return (_InitialSelection); }

            set
            {
                if (value != _InitialSelection)
                {
                    _InitialSelection = value;

                    OnPropertyChangedEx("InitialSelection");
                }
            }
        }

        #endregion

        #region InsertRowImage

        /// <summary>
        /// Gets or sets the default image to display
        /// in the row header to denote the Insertion Row.
        /// </summary>
        [DefaultValue(null), Category("Rows")]
        [Description("Indicates the image to display in the row header to denote the Insertion Row.")]
        public Image InsertRowImage
        {
            get { return (_InsertRowImage); }

            set
            {
                if (_InsertRowImage != value)
                {
                    if (_InsertRowImageCache != null)
                    {
                        _InsertRowImageCache.Dispose();
                        _InsertRowImageCache = null;
                    }

                    _InsertRowImage = value;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("InsertRowImage", VisualChangeType.Layout);
                }
            }
        }

        #region GetInsertRowImage

        internal Image GetInsertRowImage()
        {
            if (_InsertRowImage != null)
                return (_InsertRowImage);

            if (_InsertRowImageIndex >= 0)
            {
                ImageList imageList = _ImageList;

                if (imageList != null && _InsertRowImageIndex < imageList.Images.Count)
                    return (imageList.Images[_InsertRowImageIndex]);
            }

            return (GetInsertRowImageEx());
        }

        #region GetInsertRowImageEx

        private Image GetInsertRowImageEx()
        {
            if (_InsertRowImageCache == null)
            {
                Assembly myAssembly = Assembly.GetExecutingAssembly();

                using (Stream myStream =
                    myAssembly.GetManifestResourceStream("DevComponents.DotNetBar.SuperGrid.InsertRow.png"))
                {
                    if (myStream != null)
                        _InsertRowImageCache = new Bitmap(myStream);
                }
            }

            return (_InsertRowImageCache);
        }

        #endregion

        #endregion

        #endregion

        #region InsertRowImageIndex

        /// <summary>
        /// Gets or sets the Insert Row image index
        /// </summary>
        [Browsable(true), DefaultValue(-1)]
        [Category("Appearance"), Description("Indicates the Insert Row image index.")]
        [Editor("DevComponents.SuperGrid.Design.ImageIndexEditor, DevComponents.SuperGrid.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=26d81176cfa2b486", typeof(UITypeEditor))]
        [TypeConverter(typeof(ImageIndexConverter))]
        public int InsertRowImageIndex
        {
            get { return (_InsertRowImageIndex); }

            set
            {
                if (_InsertRowImageIndex != value)
                {
                    _InsertRowImageIndex = value;

                    OnPropertyChangedEx("InsertRowImageIndex", VisualChangeType.Layout);
                }
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetInsertRowImageIndex()
        {
            _InsertRowImageIndex = -1;
        }

        #endregion

        #region InsertTempRowImage

        /// <summary>
        /// Gets or sets the default image to display
        /// in the row header to denote a temporary insert Row.
        /// </summary>
        [DefaultValue(null), Category("Rows")]
        [Description("Indicates the image to display in the row header to denote a temporary Insertion Row.")]
        public Image InsertTempRowImage
        {
            get { return (_InsertTempRowImage); }

            set
            {
                if (_InsertTempRowImage != value)
                {
                    if (_InsertTempRowImageCache != null)
                    {
                        _InsertTempRowImageCache.Dispose();
                        _InsertTempRowImageCache = null;
                    }

                    _InsertTempRowImage = value;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("InsertTempRowImage", VisualChangeType.Layout);
                }
            }
        }

        #region GetInsertTempRowImage

        internal Image GetInsertTempRowImage()
        {
            if (_InsertTempRowImage != null)
                return (_InsertTempRowImage);

            if (_InsertTempRowImageIndex >= 0)
            {
                ImageList imageList = _ImageList;

                if (imageList != null && _InsertTempRowImageIndex < imageList.Images.Count)
                    return (imageList.Images[_InsertTempRowImageIndex]);
            }

            return (GetInsertTempRowImageEx());
        }

        #region GetInsertTempRowImageEx

        private Image GetInsertTempRowImageEx()
        {
            if (_InsertTempRowImageCache == null)
            {
                RowVisualStyle style = GetEffectiveRowStyle();

                Rectangle r = new Rectangle(0, 0, 4, 7);
                Image image = new Bitmap(4, 7);

                using (Graphics g = Graphics.FromImage(image))
                {
                    Point pt = new Point(r.X, r.Y + r.Height / 2);

                    Point[] pts =
                            {
                                pt,
                                new Point(pt.X + 4, pt.Y + 4),
                                new Point(pt.X + 4, pt.Y - 4),
                                pt
                            };

                    using (GraphicsPath path = new GraphicsPath())
                    {
                        path.AddLines(pts);

                        Color color = style.RowHeaderStyle.ActiveRowIndicatorColor;

                        if (color.IsEmpty)
                            color = Color.Black;

                        using (Brush br = new SolidBrush(color))
                            g.FillPath(br, path);
                    }
                }

                _InsertTempRowImageCache = image;
            }

            return (_InsertTempRowImageCache);
        }

        #endregion

        #endregion

        #endregion

        #region InsertTempRowImageIndex

        /// <summary>
        /// Gets or sets the Insert Temp Row image index
        /// </summary>
        [Browsable(true), DefaultValue(-1)]
        [Category("Appearance"), Description("Indicates the Insert Temp Row image index.")]
        [Editor("DevComponents.SuperGrid.Design.ImageIndexEditor, DevComponents.SuperGrid.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=26d81176cfa2b486", typeof(UITypeEditor))]
        [TypeConverter(typeof(ImageIndexConverter))]
        public int InsertTempRowImageIndex
        {
            get { return (_InsertTempRowImageIndex); }

            set
            {
                if (_InsertTempRowImageIndex != value)
                {
                    _InsertTempRowImageIndex = value;

                    OnPropertyChangedEx("InsertTempRowImageIndex", VisualChangeType.Layout);
                }
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetInsertTempRowImageIndex()
        {
            _InsertTempRowImageIndex = -1;
        }

        #endregion

        #region IsColumnFiltered

        /// <summary>
        /// Gets whether the GridPanel has column-level filtering
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsColumnFiltered
        {
            get { return (DataFilter.IsColumnFiltered(this)); }
        }

        #endregion

        #region IsFiltered

        /// <summary>
        /// Gets whether the GridPanel is being filtered
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsFiltered
        {
            get { return (DataFilter.IsFiltered(this)); }
        }

        #endregion

        #region IsGrouped

        /// <summary>
        /// Gets whether the GridPanel is currently Grouped
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsGrouped
        {
            get { return (_GroupColumns != null && _GroupColumns.Count > 0); }
        }

        #endregion

        #region IsRowFiltered

        /// <summary>
        /// Gets whether the GridPanel has row-level filtering
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsRowFiltered
        {
            get { return (DataFilter.IsRowFiltered(this)); }
        }

        #endregion

        #region IsSortable

        /// <summary>
        /// Gets whether the GridPanel is able to be sorted
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSortable
        {
            get
            {
                if (VirtualMode == true)
                {
                    if (DataBinder.CurrencyManager == null ||
                        DataBinder.CurrencyManager.List.Count <= 0)
                    {
                        return (true);
                    }

                    return (DataBinder.IsSortable);
                }

                return (true);
            }
        }

        #endregion

        #region IsSubPanel

        /// <summary>
        /// Gets whether the GridPanel is a subordinate / nested grid.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSubPanel
        {
            get { return (Parent != null); }
        }

        #endregion

        #region KeepRowsSorted

        /// <summary>
        /// Gets or sets whether grid rows are kept sorted
        /// on row deactivation, following a cell's Value change
        /// </summary>
        [DefaultValue(true), Category("Behavior")]
        [Description("Indicates whether grid rows are kept sorted on row deactivation, following a cell's Value change.")]
        public bool KeepRowsSorted
        {
            get { return (_KeepRowsSorted); }

            set
            {
                if (value != _KeepRowsSorted)
                {
                    _KeepRowsSorted = value;

                    OnPropertyChangedEx("KeepRowsSorted");
                }
            }
        }

        #endregion

        #region KeyboardEditMode

        /// <summary>
        /// Gets or sets how cell editing is initiated by the keyboard
        /// </summary>
        [DefaultValue(KeyboardEditMode.EditOnKeystrokeOrF2), Category("Behavior")]
        [Description("Indicates how cell editing is initiated by the keyboard.")]
        public KeyboardEditMode KeyboardEditMode
        {
            get { return (_KeyboardEditMode); }

            set
            {
                if (value != _KeyboardEditMode)
                {
                    _KeyboardEditMode = value;

                    OnPropertyChangedEx("KeyboardEditMode");
                }
            }
        }

        #endregion

        #region LastFrozenColumn

        /// <summary>
        /// Gets a reference to the last Frozen column
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridColumn LastFrozenColumn
        {
            get
            {
                int[] displayMap = Columns.DisplayIndexMap;

                for (int i = displayMap.Length - 1; i >= 0; i--)
                {
                    GridColumn column = Columns[displayMap[i]];

                    if (column.Visible == true && column.IsHFrozen == true)
                        return (column);
                }

                return (null);
            }
        }

        #endregion

        #region LastOnScreenColumn

        /// <summary>
        /// Gets a reference to the last on-screen column
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridColumn LastOnScreenColumn
        {
            get
            {
                int[] displayMap = Columns.DisplayIndexMap;

                for (int i = displayMap.Length - 1; i >= 0; i--)
                {
                    GridColumn column = Columns[displayMap[i]];

                    if (column.IsOnScreen == true)
                        return (column);
                }

                return (null);
            }
        }

        #endregion

        #region LastVisibleColumn

        /// <summary>
        /// Gets a reference to the last visible column
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridColumn LastVisibleColumn
        {
            get { return (Columns.LastVisibleColumn); }
        }

        #endregion

        #region LevelIndentSize

        /// <summary>
        /// Gets or sets the amount to indent the grid data when
        /// a new grid level is encountered
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates the amount to indent the grid data when a new grid level is encountered.")]
        public Size LevelIndentSize
        {
            get { return (_LevelIndentSize); }

            set
            {
                if (_LevelIndentSize != value)
                {
                    _LevelIndentSize = value;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("LevelIndentSize", VisualChangeType.Layout);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal bool ShouldSerializeLevelIndentSize()
        {
            return (_LevelIndentSize.Height != 10 || _LevelIndentSize.Width != 20);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal void ResetLevelIndentSize()
        {
            LevelIndentSize = new Size(20, 10);
        }

        #endregion

        #region MaxRowHeight

        /// <summary>
        /// Gets or sets the maximum row height
        /// </summary>
        [DefaultValue(500), Category("Rows")]
        [Description("Indicates the maximum row height.")]
        public int MaxRowHeight
        {
            get { return (_MaxRowHeight); }

            set
            {
                if (value != _MaxRowHeight)
                {
                    if (value < 0)
                        throw new ArgumentException("Value must be 0 or greater.");

                    _MaxRowHeight = value;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("MaxRowHeight", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region MinRowHeight

        /// <summary>
        /// Gets or sets the minimum row height
        /// </summary>
        [DefaultValue(5), Category("Rows")]
        [Description("Indicates the minimum row height.")]
        public int MinRowHeight
        {
            get { return (_MinRowHeight); }

            set
            {
                if (value != _MinRowHeight)
                {
                    if (value < 0)
                        throw new ArgumentException("Value must be 0 or greater.");

                    _MinRowHeight = value;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("MinRowHeight", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region MouseEditMode

        /// <summary>
        /// Gets or sets how cell editing is initiated by the mouse
        /// </summary>
        [DefaultValue(MouseEditMode.DoubleClick), Category("Behavior")]
        [Description("Indicates how cell editing is initiated by the mouse.")]
        public MouseEditMode MouseEditMode
        {
            get { return (_MouseEditMode); }

            set
            {
                if (value != _MouseEditMode)
                {
                    _MouseEditMode = value;

                    OnPropertyChangedEx("MinRowHeight");
                }
            }
        }

        #endregion

        #region MultiSelect

        ///<summary>
        /// Gets or sets whether multiple items can be selected by the user
        ///</summary>
        [DefaultValue(true), Category("Selection")]
        [Description("Indicates whether multiple items can be selected by the user.")]
        public bool MultiSelect
        {
            get { return (_MultiSelect); }

            set
            {
                if (value != _MultiSelect)
                {
                    _MultiSelect = value;

                    OnPropertyChangedEx("MultiSelect");
                }
            }
        }

        #endregion

        #region Name

        /// <summary>
        /// Gets or sets the Grid Panel name
        /// </summary>
        [DefaultValue("")]
        [Description("Indicates the Grid Panel name.")]
        public string Name
        {
            get { return (_Name); }

            set
            {
                if (_Name != value)
                {
                    _Name = value;

                    OnPropertyChangedEx("Name");
                }
            }
        }

        #endregion

        #region NoRowsText

        /// <summary>
        /// Gets or sets the text to show when there are no visible data Rows in the grid
        /// </summary>
        [DefaultValue(null), Category("Appearance")]
        [Description("Indicates the text to show when there are no visible data Rows in the grid.")]
        public string NoRowsText
        {
            get { return (_NoRowsText); }

            set
            {
                if (_NoRowsText != value)
                {
                    _NoRowsText = value;

                    NoRowsMarkupTextChanged();

                    OnPropertyChangedEx("NoRowsText", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region NullString

        /// <summary>
        /// Gets or sets how null values are displayed 
        /// </summary>
        [DefaultValue(""), Category("Data")]
        [Description("Indicates how null values are displayed.")]
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

        #region NullValue

        /// <summary>
        /// Gets or sets how null values are interpreted 
        /// </summary>
        [DefaultValue(NullValue.DBNull), Category("Data")]
        [Description("Indicates how null values are interpreted.")]
        public NullValue NullValue
        {
            get { return (_NullValue); }

            set
            {
                if (_NullValue != value)
                {
                    _NullValue = value;

                    OnPropertyChangedEx("NullValue", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region OnScreenColumns

        ///<summary>
        /// IEnumerable that can be used to enumerate through
        /// the current on-screen columns
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable OnScreenColumns
        {
            get { return (new OnScreenColumnsEnumeration(this)); }
        }

        #endregion

        #region OnScreenRows

        ///<summary>
        /// IEnumerable that can be used to enumerate through
        /// the current on-screen rows
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable OnScreenRows
        {
            get { return (new OnScreenRowsEnumeration(this)); }
        }

        #endregion

        #region PrimaryColumn

        /// <summary>
        /// Gets the primary column. The Primary Columns will
        /// contain (if enabled) the row checkbox and Tree lines
        /// and expand / collapse button.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridColumn PrimaryColumn
        {
            get
            {
                if ((uint)_PrimaryColumnIndex < _Columns.Count)
                    return (_Columns[_PrimaryColumnIndex]);

                return (null);
            }
        }

        #endregion

        #region PrimaryColumnIndex

        /// <summary>
        /// Gets or sets the index of the primary column
        /// </summary>
        [DefaultValue(0), Category("Columns")]
        [Description("Indicates the index of the primary column.")]
        [Editor("DevComponents.SuperGrid.Design.ColumnListTypeEditor, DevComponents.SuperGrid.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=26d81176cfa2b486", typeof(UITypeEditor))]
        public int PrimaryColumnIndex
        {
            get { return (_PrimaryColumnIndex); }

            set
            {
                if (_PrimaryColumnIndex != value)
                {
                    _PrimaryColumnIndex = value;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("PrimaryColumnIndex", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region ProcessChildRelations

        /// <summary>
        /// Gets or sets how the SuperGrid processes child relations
        /// </summary>
        [DefaultValue(ProcessChildRelations.Always), Category("Data")]
        [Description("Indicates how the SuperGrid processes child relations.")]
        public ProcessChildRelations ProcessChildRelations
        {
            get { return (_ProcessChildRelations); }

            set
            {
                if (_ProcessChildRelations != value)
                {
                    _ProcessChildRelations = value;

                    OnPropertyChangedEx("ProcessChildRelations", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region RowDoubleClickBehavior

        /// <summary>
        /// Gets or sets the behavior when a row is double clicked
        /// </summary>
        [DefaultValue(RowDoubleClickBehavior.Activate), Category("Behavior")]
        [Description("Indicates the behavior when a row is double clicked.")]
        public RowDoubleClickBehavior RowDoubleClickBehavior
        {
            get { return (_RowDoubleClickBehavior); }

            set
            {
                if (_RowDoubleClickBehavior != value)
                {
                    _RowDoubleClickBehavior = value;

                    OnPropertyChangedEx("RowDoubleClickBehavior");
                }
            }
        }

        #endregion

        #region RowDragBehavior

        /// <summary>
        /// Gets or sets the behavior when a row header is clicked and dragged
        /// </summary>
        [DefaultValue(RowDragBehavior.ExtendSelection), Category("Behavior")]
        [Description("Indicates the behavior when a row header is clicked and dragged.")]
        public RowDragBehavior RowDragBehavior
        {
            get { return (_RowDragBehavior); }

            set
            {
                if (value != _RowDragBehavior)
                {
                    _RowDragBehavior = value;

                    OnPropertyChangedEx("RowDragBehavior");
                }
            }
        }

        #endregion

        #region RowEditMode

        /// <summary>
        /// Gets or sets whether editing is initiated on the primary cell
        /// or the cell under the mouse when row level editing is enabled.
        /// </summary>
        [DefaultValue(RowEditMode.ClickedCell), Category("Behavior")]
        [Description("Indicates whether editing is initiated on the primary cell or the cell under the mouse when row level editing is enabled")]
        public RowEditMode RowEditMode
        {
            get { return (_RowEditMode); }

            set
            {
                if (value != _RowEditMode)
                {
                    _RowEditMode = value;

                    OnPropertyChangedEx("RowEditMode");
                }
            }
        }

        #endregion

        #region RowHeaderIndexOffset

        /// <summary>
        /// Gets or sets the offset value used
        /// to adjust the Index displayed in the row header.
        /// </summary>
        [DefaultValue(0), Category("Appearance")]
        [Description("Indicates the offset value used to adjust the Index displayed in the row header")]
        public int RowHeaderIndexOffset
        {
            get { return (_RowHeaderIndexOffset); }

            set
            {
                if (_RowHeaderIndexOffset != value)
                {
                    _RowHeaderIndexOffset = value;

                    OnPropertyChangedEx("RowHeaderIndexOffset", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region RowHeaderWidth

        /// <summary>
        /// Gets or sets the width of the row header
        /// </summary>
        [DefaultValue(35), Category("Rows")]
        [Description("Indicates the width of the row header.")]
        public int RowHeaderWidth
        {
            get { return (_RowHeaderWidth); }

            set
            {
                if (_RowHeaderWidth != value)
                {
                    _RowHeaderWidth = value;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("RowHeaderWidth", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region RowHighlightType

        /// <summary>
        /// Gets or sets the type of highlighting
        /// to use  when a row is selected
        /// </summary>
        [DefaultValue(RowHighlightType.Full), Category("Selection")]
        [Description("Indicates the type of highlighting to use  when a row is selected.")]
        public RowHighlightType RowHighlightType
        {
            get { return (_RowHighlightType); }

            set
            {
                if (value != _RowHighlightType)
                {
                    _RowHighlightType = value;

                    OnPropertyChangedEx("RowHeaderWidth");
                }
            }
        }

        #endregion

        #region RowWhitespaceClickBehavior

        /// <summary>
        /// Gets or sets the behavior when the user
        /// clicks the mouse in the row whitespace
        /// </summary>
        [DefaultValue(RowWhitespaceClickBehavior.ClearSelection), Category("Rows")]
        [Description("Indicates the behavior when the user clicks the mouse in the row whitespace.")]
        public RowWhitespaceClickBehavior RowWhitespaceClickBehavior
        {
            get { return (_RowWhitespaceClickBehavior); }

            set
            {
                if (value != _RowWhitespaceClickBehavior)
                {
                    _RowWhitespaceClickBehavior = value;

                    OnPropertyChangedEx("RowWhitespaceClickBehavior");
                }
            }
        }

        #endregion

        #region SelectedCellCount

        ///<summary>
        /// Gets the current selected cell count
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedCellCount
        {
            get
            {
                int n = 0;

                foreach (GridColumn column in Columns)
                    n += column.SelectedCellCount;

                return (n);
            }
        }

        #endregion

        #region SelectedCells

        ///<summary>
        /// IEnumerable that can be used
        /// to enumerate through all currently selected cells
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable SelectedCells
        {
            get { return (new SelectedCellsEnumeration(this)); }
        }

        #endregion

        #region SelectedColumnCount

        ///<summary>
        /// Gets the current selected column count
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedColumnCount
        {
            get { return (_SelectedColumns.Count); }
        }

        #endregion

        #region SelectedColumns

        ///<summary>
        /// IEnumerable that can be used
        /// to enumerate through all currently selected columns
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable SelectedColumns
        {
            get { return (new SelectedColumnsEnumeration(this, _SelectedColumns)); }
        }

        #endregion

        #region SelectedRowCount

        ///<summary>
        /// Gets the current selected row count
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedRowCount
        {
            get { return (_SelectedRows.Count); }
        }

        #endregion

        #region SelectedRows

        ///<summary>
        /// IEnumerable that can be used
        /// to enumerate through all currently selected rows
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable SelectedRows
        {
            get { return (new SelectedRowsEnumeration(this, _SelectedRows)); }
        }

        #endregion

        #region SelectionGranularity

        /// <summary>
        /// Gets or sets whether element selection
        /// is at the row or cell level
        /// </summary>
        [DefaultValue(SelectionGranularity.Cell), Category("Selection")]
        [Description("Indicates whether element selection is at the row or cell level.")]
        public SelectionGranularity SelectionGranularity
        {
            get { return (_SelectionGranularity); }

            set
            {
                if (value != _SelectionGranularity)
                {
                    _SelectionGranularity = value;

                    OnPropertyChangedEx("SelectionGranularity");
                }
            }
        }

        #endregion

        #region ShowCellInfo

        /// <summary>
        /// Gets or sets whether to show  Cell Info Image and Text (if defined).
        /// </summary>
        [DefaultValue(true), Category("Appearance")]
        [Description("Indicates whether to show Cell Info Image and Text (if defined).")]
        public bool ShowCellInfo
        {
            get { return (_ShowCellInfo); }

            set
            {
                if (_ShowCellInfo != value)
                {
                    _ShowCellInfo = value;

                    OnPropertyChangedEx("ShowCellInfo", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region ShowColumnHeader

        /// <summary>
        /// Gets or sets whether the Column Header is displayed
        /// </summary>
        [DefaultValue(true), Category("Appearance")]
        [Description("Indicates whether the Column Header is displayed.")]
        public bool ShowColumnHeader
        {
            get { return (_ColumnHeader.Visible); }

            set
            {
                if (value != _ColumnHeader.Visible)
                {
                    _ColumnHeader.Visible = value;

                    OnPropertyChangedEx("ShowColumnHeader", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region ShowDropShadow

        /// <summary>
        /// Gets or sets whether nested Grid Panels display a drop shadow
        /// </summary>
        [DefaultValue(true), Category("Appearance")]
        [Description("Indicates whether nested Grid Panels display a drop shadow.")]
        public bool ShowDropShadow
        {
            get { return (_ShowDropShadow); }

            set
            {
                if (value != _ShowDropShadow)
                {
                    _ShowDropShadow = value;

                    OnPropertyChangedEx("ShowDropShadow", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region ShowEditingImage

        /// <summary>
        /// Gets or sets whether the EditingRowImage is
        /// displayed in the row header of the cell being edited. 
        /// </summary>
        [DefaultValue(true), Category("Appearance")]
        [Description("Indicates whether the EditingRowImage is displayed in the row header of the cell being edited.")]
        public bool ShowEditingImage
        {
            get { return (_ShowEditingImage); }

            set
            {
                if (value != _ShowEditingImage)
                {
                    _ShowEditingImage = value;

                    OnPropertyChangedEx("ShowEditingImage");
                }
            }
        }

        #endregion

        #region ShowGroupExpand

        /// <summary>
        /// Gets or sets whether the expand button is shown in group headers 
        /// </summary>
        [DefaultValue(true), Category("Group")]
        [Description("Indicates whether the expand button is shown in group headers.")]
        public bool ShowGroupExpand
        {
            get { return (_ShowGroupExpand); }

            set
            {
                if (value != _ShowGroupExpand)
                {
                    _ShowGroupExpand = value;

                    OnPropertyChangedEx("ShowGroupExpand", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region ShowGroupUnderline

        /// <summary>
        /// Gets or sets whether the Group header text is underlined 
        /// </summary>
        [DefaultValue(true), Category("Group")]
        [Description("Indicates whether the Group header text is underlined.")]
        public bool ShowGroupUnderline
        {
            get { return (_ShowGroupUnderline); }

            set
            {
                if (value != _ShowGroupUnderline)
                {
                    _ShowGroupUnderline = value;

                    OnPropertyChangedEx("ShowGroupExpand", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region ShowInsertRow

        /// <summary>
        /// Gets or sets whether the insert row is displayed
        /// at the bottom of the panel for users to add new rows
        /// </summary>
        [DefaultValue(false), Category("Appearance")]
        [Description("Indicates whether the insert row is displayed at the bottom of the panel for users to add new rows.")]
        public bool ShowInsertRow
        {
            get { return (_ShowInsertRow); }

            set
            {
                if (value != _ShowInsertRow)
                {
                    _ShowInsertRow = value;

                    if (IsDesignerHosted == false)
                    {
                        if (value == true)
                            AddNewInsertRow();
                        else
                            RemoveNewInsertRow();
                    }

                    OnPropertyChangedEx("ShowInsertionRow", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region ShowRowInfo

        /// <summary>
        /// Gets or sets whether to show Row Info Image and  (if defined)
        /// </summary>
        [DefaultValue(true), Category("Appearance")]
        [Description("Indicates whether to show Row Info Image and  (if defined).")]
        public bool ShowRowInfo
        {
            get { return (_ShowRowInfo); }

            set
            {
                if (_ShowRowInfo != value)
                {
                    _ShowRowInfo = value;

                    OnPropertyChangedEx("ShowInsertionRow", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region ShowRowHeaders

        /// <summary>
        /// Gets or sets whether row headers are displayed
        /// </summary>
        [DefaultValue(true), Category("Appearance")]
        [Description("Indicates whether row headers are displayed.")]
        public bool ShowRowHeaders
        {
            get { return (_ShowRowHeaders); }

            set
            {
                if (value != _ShowRowHeaders)
                {
                    _ShowRowHeaders = value;

                    OnPropertyChangedEx("ShowRowHeaders", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region ShowRowGridIndex

        /// <summary>
        /// Gets or sets whether row
        /// GridIndex values are displayed in the row headers.
        /// </summary>
        [DefaultValue(false), Category("Appearance")]
        [Description("Indicates whether row GridIndex values are displayed in the row headers. A GridIndex is the 'logical' index for the current visible row.")]
        public bool ShowRowGridIndex
        {
            get { return (_ShowRowGridIndex); }

            set
            {
                if (_ShowRowGridIndex != value)
                {
                    _ShowRowGridIndex = value;

                    OnPropertyChangedEx("ShowRowGridIndex");

                    if (_ShowRowHeaders == true)
                        InvalidateRender();
                }
            }
        }

        #endregion

        #region ShowRowDirtyMarker

        /// <summary>
        /// Gets or sets whether the DirtyRow marker is displayed,
        /// signifying that the row data has been changed by the user
        /// </summary>
        [DefaultValue(true), Category("Appearance")]
        [Description("Indicates whether the DirtyRow marker is displayed, signifying that the row data has been changed by the user.")]
        public bool ShowRowDirtyMarker
        {
            get { return (_ShowRowDirtyMarker); }

            set
            {
                if (_ShowRowDirtyMarker != value)
                {
                    _ShowRowDirtyMarker = value;

                    OnPropertyChangedEx("ShowRowDirtyMarker", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region ShowWhitespaceRowLines

        /// <summary>
        /// Gets or sets whether the row lines are displayed in the Whitespace
        /// area following the last visible column in the grid
        /// </summary>
        [DefaultValue(true), Category("Appearance")]
        [Description("Indicates whether the row lines are displayed in the Whitespace area following the last visible column in the grid.")]
        public bool ShowWhitespaceRowLines
        {
            get { return (_ShowWhitespaceRowLines); }

            set
            {
                if (value != _ShowWhitespaceRowLines)
                {
                    _ShowWhitespaceRowLines = value;

                    OnPropertyChangedEx("ShowWhitespaceRowLines", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region ShowToolTips

        /// <summary>
        /// Gets or sets whether tooltips are shown
        /// for cell data that is truncated by the display
        /// </summary>
        [DefaultValue(true), Category("Appearance")]
        [Description("Indicates whether tooltips are shown for cell data that is truncated by the display.")]
        public bool ShowToolTips
        {
            get { return (_ShowToolTips); }

            set
            {
                if (value != _ShowToolTips)
                {
                    _ShowToolTips = value;

                    OnPropertyChangedEx("ShowToolTips");
                }
            }
        }

        #endregion

        #region ShowTreeButtons

        /// <summary>
        /// Gets or sets whether expand / collapse
        /// buttons are shown for rows containing nested rows 
        /// </summary>
        [DefaultValue(false), Category("Appearance")]
        [Description("Indicates whether Tree expand / collapse buttons are shown for rows containing nested rows.")]
        public bool ShowTreeButtons
        {
            get { return (_ShowTreeButtons); }

            set
            {
                if (_ShowTreeButtons != value)
                {
                    _ShowTreeButtons = value;

                    OnPropertyChangedEx("ShowTreeButtons", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region ShowTreeLines

        /// <summary>
        /// Gets or sets whether hierarchical tree-lines
        /// are shown connecting grid rows 
        /// </summary>
        [DefaultValue(false), Category("Appearance")]
        [Description("Indicates whether hierarchical tree-lines are shown connecting grid rows.")]
        public bool ShowTreeLines
        {
            get { return (_ShowTreeLines); }

            set
            {
                if (_ShowTreeLines != value)
                {
                    _ShowTreeLines = value;

                    OnPropertyChangedEx("ShowTreeButtons", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region SizingStyle

        /// <summary>
        /// Gets or sets which Visual Display Style to use for element sizing
        /// </summary>
        [DefaultValue(StyleType.NotSet), Category("Style")]
        [Description("Indicates which Visual Display Style to use for element sizing.")]
        public StyleType SizingStyle
        {
            get { return (_SizingStyle); }

            set
            {
                if (_SizingStyle != value)
                {
                    _SizingStyle = value;

                    OnPropertyChangedEx("SizingStyle", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region SortColumns

        /// <summary>
        /// Gets a reference to the list of columns to use for sorting the grid data.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<GridColumn> SortColumns
        {
            get { return (_SortColumns); }
        }

        #endregion

        #region SortLevel

        /// <summary>
        /// Gets or sets the column sorting level. 
        /// </summary>
        [DefaultValue(SortLevel.Root), Category("Appearance")]
        [Description("Indicates the column sorting level.")]
        public SortLevel SortLevel
        {
            get { return (_SortLevel); }

            set
            {
                if (value != _SortLevel)
                {
                    _SortLevel = value;

                    OnPropertyChangedEx("SortLevel", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region Title

        /// <summary>
        /// Gets or sets the item which is displayed just above
        /// the Column Header. The Title area is fixed and it does
        /// not move as grid rows are scrolled.
        /// </summary>
        [Category("Header,Footer...")]
        [Description("Indicates the Title item, which is displayed just above the Column Header. The Title area is fixed and it does not move as grid rows are scrolled.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GridTitle Title
        {
            get
            {
                if (_Title == null)
                    Title = new GridTitle();

                return (_Title);
            }

            set
            {
                if (_Title != value)
                {
                    if (value != null)
                    {
                        if (value.Parent != null)
                            throw new ArgumentException("Item assigned as Title already has a parent.");
                    }

                    _Title = value;

                    if (_Title != null)
                        _Title.Parent = this;

                    NeedsMeasured = true;
                    
                    OnPropertyChangedEx("Title", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region TopLeftHeaderSelectBehavior

        /// <summary>
        /// Gets or sets the behavior that occurs when
        /// the top left header is selected by the user
        /// </summary>
        [DefaultValue(TopLeftHeaderSelectBehavior.Deterministic), Category("Behavior")]
        [Description("Indicates the behavior that occurs when the top left header is selected by the user.")]
        public TopLeftHeaderSelectBehavior TopLeftHeaderSelectBehavior
        {
            get { return (_TopLeftHeaderSelectBehavior); }

            set
            {
                if (value != _TopLeftHeaderSelectBehavior)
                {
                    _TopLeftHeaderSelectBehavior = value;

                    OnPropertyChangedEx("TopLeftHeaderSelectBehavior", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region UseAlternateColumnStyle

        /// <summary>
        /// Gets or sets whether the AlternateColumn Style is used
        /// </summary>
        [DefaultValue(false), Category("Style")]
        [Description("Indicates whether the AlternateColumn Style is used.")]
        public bool UseAlternateColumnStyle
        {
            get { return (_UseAlternateColumnStyle); }

            set
            {
                if (value != _UseAlternateColumnStyle)
                {
                    _UseAlternateColumnStyle = value;

                    if (SuperGrid != null)
                        SuperGrid.UpdateStyleCount();

                    OnPropertyChangedEx("UseAlternateColumnStyle", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region UseAlternateRowStyle

        /// <summary>
        /// Gets or sets whether the AlternateRow Style is used
        /// </summary>
        [DefaultValue(false), Category("Style")]
        [Description("Indicates whether the AlternateRow Style is used")]
        public bool UseAlternateRowStyle
        {
            get { return (_UseAlternateRowStyle); }

            set
            {
                if (value != _UseAlternateRowStyle)
                {
                    _UseAlternateRowStyle = value;

                    if (SuperGrid != null)
                        SuperGrid.UpdateStyleCount();

                    OnPropertyChangedEx("UseAlternateRowStyle", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region VirtualMode

        ///<summary>
        /// Gets or sets whether the grid data is virtualized (i.e. external to the grid)
        ///</summary>
        [DefaultValue(false), Category("Virtual Mode")]
        [Description("Indicates whether the grid data is virtualized (ie. external to the grid).")]
        public bool VirtualMode
        {
            get { return (_VirtualMode); }

            set
            {
                if (_VirtualMode != value)
                {
                    _VirtualMode = value;
                    _VirtualRowCount = DataBinder.RowCount;

                    NeedToUpdateBindings = true;
                    NeedsSorted = true;

                    if (value == true)
                    {
                        _VirtualRows = new GridVirtualRows(this);

                        if (_ShowInsertRow == true)
                        {
                            Rows.FloatLastItem = false;

                            AddNewInsertRow();
                        }

                        Rows.Clear();
                    }
                    else
                    {
                        _VirtualRowCount = 0;

                        _VirtualRows.Clear();
                        _VirtualRows = null;

                        if (_ShowInsertRow == true)
                            AddNewInsertRow();
                    }

                    UpdateVisibleRowCount();

                    OnPropertyChangedEx("VirtualMode", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region VirtualRowCount

        ///<summary>
        /// Gets or sets the number of virtual grid rows
        ///</summary>
        [DefaultValue(0), Category("Virtual Mode")]
        [Description("Indicates the number of virtual grid rows.")]
        public int VirtualRowCount
        {
            get
            {
                if (_VirtualMode == true && _ShowInsertRow == true)
                    return (_VirtualRowCount - 1);

                return (_VirtualRowCount);
            }

            set
            {
                if (VirtualMode == true)
                {
                    if (value < 0)
                        value = 0;

                    int n = (_ShowInsertRow == true) ? 1 : 0;

                    if (_VirtualRowCount != value + n)
                    {
                        if (SuperGrid != null)
                        {
                            if (SuperGrid.ActiveRow != null)
                                SuperGrid.ActiveRow.FlushRow();
                        }

                        if (_ShowInsertRow == true)
                            RemoveNewInsertRow();

                        if (value == 0)
                        {
                            _VirtualRows.Clear();

                            ClearAll();
                        }
                        else
                        {
                            int count = _VirtualRowCount - value;

                            if (count > 0)
                            {
                                SetSelectedCells(value, 0, count, Columns.Count, false);
                                SetSelectedRows(value, count, false);
                            }

                            _VirtualRows.MaxRowIndex = value;
                        }

                        _VirtualRowCount = value;

                        if (_ShowInsertRow == true)
                            AddNewInsertRow();

                        _VisibleRowCount = _VirtualRowCount;

                        if (ActiveRow != null && ActiveRow.RowIndex >= _VirtualRowCount)
                            ActiveRow = null;

                        OnPropertyChangedEx("VirtualRowCount", VisualChangeType.Layout);
                    }
                }
                else
                {
                    if (_VirtualRowCount != value)
                    {
                        _VirtualRowCount = value;

                        OnPropertyChangedEx("VirtualRowCount", VisualChangeType.Layout);
                    }
                }
            }
        }

        #endregion

        #region VirtualRowHeight

        ///<summary>
        /// Gets or sets the height of every virtual row
        ///</summary>
        [DefaultValue(22), Category("Virtual Mode")]
        [Description("Indicates the height of every virtual row.")]
        public int VirtualRowHeight
        {
            get { return (_VirtualRowHeight); }

            set
            {
                if (_VirtualRowHeight != value)
                {
                    if (value < 0)
                        throw new Exception("Row height must be non-negative");

                    _VirtualRowHeight = value;

                    OnPropertyChangedEx("VirtualRowHeight", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region VirtualRows

        ///<summary>
        /// Gets the Virtual Mode rows collection object
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridVirtualRows VirtualRows
        {
            get { return (_VirtualRows); }
        }

        #endregion

        #region VisibleColumnCount

        ///<summary>
        /// Gets the current visible column count
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int VisibleColumnCount
        {
            get
            {
                int count = 0;

                GridColumnCollection columns = Columns;
                foreach (GridColumn column in columns)
                {
                    if (column.Visible == true)
                        count++;
                }

                return (count);
            }
        }

        #endregion

        #region VisibleRowCount

        ///<summary>
        /// Gets the current visible row count
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int VisibleRowCount
        {
            get { return (_VisibleRowCount); }
        }

        #endregion

        #region WhitespaceClickBehavior

        /// <summary>
        /// Gets or sets the behavior when the user
        /// clicks the mouse in the grid panel whitespace
        /// </summary>
        [DefaultValue(WhitespaceClickBehavior.ClearSelection), Category("Behavior")]
        [Description("Indicates the behavior when the user clicks the mouse in the grid panel whitespace.")]
        public WhitespaceClickBehavior WhitespaceClickBehavior
        {
            get { return (_WhitespaceClickBehavior); }

            set
            {
                if (value != _WhitespaceClickBehavior)
                {
                    _WhitespaceClickBehavior = value;

                    OnPropertyChangedEx("VirtualRowHeight");
                }
            }
        }

        #endregion

        #endregion

        #region Internal properties

        #region CollapseButtonHotImage

        internal Image CollapseButtonHotImage
        {
            get { return (_CollapseButtonHotImage); }

            set
            {
                if (_CollapseButtonHotImage != null)
                    _CollapseButtonHotImage.Dispose();

                _CollapseButtonHotImage = value;
            }
        }

        #endregion

        #region CollapseButtonImage

        internal Image CollapseButtonImage
        {
            get { return (_CollapseButtonImage); }

            set
            {
                if (_CollapseButtonImage != null)
                    _CollapseButtonImage.Dispose();

                _CollapseButtonImage = value;
            }
        }

        #endregion

        #region DataBinder

        internal DataBinder DataBinder
        {
            get
            {
                if (_DataBinder == null)
                    _DataBinder = new DataBinder(this);

                return (_DataBinder);
            }

            set
            {
                if (_DataBinder != null)
                    _DataBinder.Clear();

                _DataBinder = value;
            }
        }

        #endregion

        #region DeleteUpdateCount

        internal int DeleteUpdateCount
        {
            get { return (_DeleteUpdateCount); }
            set { _DeleteUpdateCount = value; }
        }

        #endregion

        #region EnsureRowVisible

        internal bool EnsureRowVisible
        {
            get { return (_EnsureRowVisible); }
            set { _EnsureRowVisible = value; }
        }

        #endregion

        #region ExpandButtonHotImage

        internal Image ExpandButtonHotImage
        {
            get { return (_ExpandButtonHotImage); }

            set
            {
                if (_ExpandButtonHotImage != null)
                    _ExpandButtonHotImage.Dispose();

                _ExpandButtonHotImage = value;
            }
        }

        #endregion

        #region ExpandButtonImage

        internal Image ExpandButtonImage
        {
            get { return (_ExpandButtonImage); }

            set
            {
                if (_ExpandButtonImage != null)
                    _ExpandButtonImage.Dispose();

                _ExpandButtonImage = value;
            }
        }

        #endregion

        #region ExpDictionary

        internal Dictionary<GridCell, EEval> ExpDictionary
        {
            get { return (_ExpDictionary); }
        }

        #endregion

        #region FilterEval

        internal FilterEval FilterEval
        {
            get { return (_FilterEval); }
            set { _FilterEval = value; }
        }

        #endregion

        #region HasDeletedRows

        internal bool HasDeletedRows
        {
            get { return (_DeletedRows.FirstIndex >= 0); }
        }

        #endregion

        #region HotItem

        internal GridContainer HotItem
        {
            get { return (_HotItem); }

            set
            {
                if (_HotItem != value)
                {
                    GridContainer oldValue = _HotItem;
                    _HotItem = value;

                    if (ShowRowHeaders == true)
                    {
                        if (oldValue != null)
                            InvalidateRowHeader(oldValue);

                        if (_HotItem != null)
                            InvalidateRowHeader(_HotItem);
                    }
                }
            }
        }

        #endregion

        #region InternalDeletedRows

        ///<summary>
        /// InternalDeletedRows
        ///</summary>
        internal SelectedElements InternalDeletedRows
        {
            get { return (_DeletedRows); }
        }

        #endregion

        #region InternalSelectedRows

        internal SelectedElements InternalSelectedRows
        {
            get { return (_SelectedRows); }
        }

        #endregion

        #region LastProcessedItem

        internal GridElement LastProcessedItem
        {
            get { return (_LastProcessedItem); }

            set
            {
                _LastProcessedItem = value;

                SuperGrid.ActiveElement = value;
            }
        }

        #endregion

        #region LatentActiveCellIndex

        internal int LatentActiveCellIndex
        {
            get { return (_LatentActiveCellIndex); }
            set { _LatentActiveCellIndex = value; }
        }

        #endregion

        #region LatentActiveRowIndex

        internal int LatentActiveRowIndex
        {
            get { return (_LatentActiveRowIndex); }
            set { _LatentActiveRowIndex = value; }
        }

        #endregion

        #region NeedToUpdateBindings

        internal bool NeedToUpdateBindings
        {
            get { return (_NeedToUpdateBindings); }
            set { _NeedToUpdateBindings = value; }
        }

        #endregion

        #region NeedToUpdateDataFilter

        internal bool NeedToUpdateDataFilter
        {
            get { return (_NeedToUpdateDataFilter); }
            
            set
            {
                if (_NeedToUpdateDataFilter != value)
                {
                    if (value == true)
                    {
                        if (EnableFiltering == true)
                        {
                            _NeedToUpdateDataFilter = true;

                            if (SuperGrid != null)
                                SuperGrid.UpdateStyleCount();

                            InvalidateLayout();
                        }
                    }
                    else
                    {
                        _NeedToUpdateDataFilter = false;
                    }
                }
            }
        }

        #endregion

        #region NeedToUpdateIndicees

        internal bool NeedToUpdateIndicees
        {
            get
            {
                if (SuperGrid != null)
                    return (SuperGrid.IndiceesUpdateCount != _IndiceesUpdateCount);

                return (false);
            }

            set
            {
                if (SuperGrid != null)
                {
                    if (value == true)
                        _IndiceesUpdateCount = SuperGrid.IndiceesUpdateCount - 1;
                    else
                        _IndiceesUpdateCount = SuperGrid.IndiceesUpdateCount;
                }
            }
        }

        #endregion

        #region NewFullIndex

        internal int NewFullIndex
        {
            get { return (_NewFullIndex); }
            set { _NewFullIndex = value; }
        }

        #endregion

        #region NewIndex

        internal int NewIndex
        {
            get { return (_NewIndex); }
            set { _NewIndex = value; }
        }

        #endregion

        #region PanelBounds

        internal Rectangle PanelBounds
        {
            get
            {
                Rectangle r = BoundsRelative;

                if (IsVFrozen == false)
                {
                    if (IsSubPanel == true)
                        r.X -= HScrollOffset;

                    r.Y -= VScrollOffset;
                }

                if (_Caption != null && _Caption.Visible == true)
                {
                    r.Y += _Caption.Size.Height;
                    r.Height -= _Caption.Size.Height;
                }

                if (IsSubPanel == true)
                {
                    r.Y--;
                    r.Height++;
                }

                return (r);
            }
        }

        #endregion

        #region RowHeaderSize

        internal Size RowHeaderSize
        {
            get { return (_RowHeaderSize); }
            set { _RowHeaderSize = value; }
        }

        #endregion

        #region SelectActiveRow

        internal bool SelectActiveRow
        {
            get { return (_SelectActiveRow); }
            set { _SelectActiveRow = value; }
        }

        #endregion

        #region SelectionColumnAnchor

        internal GridColumn SelectionColumnAnchor
        {
            get { return (_SelectionColumnAnchor); }
            set { _SelectionColumnAnchor = value; }
        }

        #endregion

        #region SelectionRowAnchor

        internal GridContainer SelectionRowAnchor
        {
            get { return (_SelectionRowAnchor); }
            set { _SelectionRowAnchor = value; }
        }

        #endregion

        #region SelectionUpdateCount

        internal int SelectionUpdateCount
        {
            get { return (_SelectionUpdateCount); }
            set { _SelectionUpdateCount = value; }
        }

        #endregion

        #region SizeNeeded

        internal Size SizeNeeded
        {
            get { return (_SizeNeeded); }
            set { _SizeNeeded = value; }
        }

        #endregion

        #region TreeButtonIndent

        internal int TreeButtonIndent
        {
            get
            {
                int n = 20;

                if (_ExpandImage != null)
                    n = Math.Max(n, _ExpandImage.Width + 4);

                if (_CollapseImage != null)
                    n = Math.Max(n, _CollapseImage.Width + 4);

                return (n);
            }
        }

        #endregion

        #region VirtualInsertRow

        internal GridRow VirtualInsertRow
        {
            get { return (_VirtualInsertRow); }
            set { _VirtualInsertRow = value; }
        }

        #endregion

        #region VirtualTempInsertRow

        internal GridRow VirtualTempInsertRow
        {
            get { return (_VirtualTempInsertRow); }
            set { _VirtualTempInsertRow = value; }
        }

        #endregion

        #region VirtualRowCountEx

        internal int VirtualRowCountEx
        {
            get { return (_VirtualRowCount); }
            set { _VirtualRowCount = value; }
        }

        #endregion

        #endregion

        #region MeasureOverride

        /// <summary>
        /// Performs the layout of the item and sets
        /// the Size property to size that item will take.
        /// </summary>
        /// <param name="layoutInfo">Layout information.</param>
        /// <param name="stateInfo"></param>
        /// <param name="constraintSize"></param>
        protected override void MeasureOverride(
            GridLayoutInfo layoutInfo, GridLayoutStateInfo stateInfo, Size constraintSize)
        {
            Size sizeNeeded = new Size();

            if (_NeedToUpdateBindings == true)
            {
                if (_LatentActiveRowIndex < 0 && _ActiveRow != null)
                {
                    _LatentActiveCellIndex = -1;
                    _LatentActiveRowIndex = _ActiveRow.RowIndex;

                    if (_LastProcessedItem is GridCell)
                        _LatentActiveCellIndex = ((GridCell)_LastProcessedItem).ColumnIndex;
                }
            }

            if (stateInfo.PassCount == 0)
            {
                UpdateDataBindings();

                if (VirtualMode == true)
                    UpdateOrderLayout();

                UpdateDataFiltering();

                if (VirtualMode == false)
                    UpdateOrderLayout();

                LoadLatentInsertRow();
            }

            GridPanel panel = stateInfo.GridPanel;

            if (panel.VirtualMode == false)
                UpdateIndicees(panel, panel.Rows, true);

            if (_ReselectActiveItem == true)
            {
                _ReselectActiveItem = false;

                if (SuperGrid.ActiveElement != null)
                {
                    if (SuperGrid.ActiveElement is GridCell)
                        ((GridCell) SuperGrid.ActiveElement).IsSelected = true;

                    else if (SuperGrid.ActiveElement is GridContainer)
                        ((GridContainer) SuperGrid.ActiveElement).IsSelected = true;
                }
            }

            MeasureColumnHeader(layoutInfo, stateInfo, constraintSize);
            MeasureColumns(layoutInfo, stateInfo, ref sizeNeeded);
            MeasureColumnHeader(layoutInfo, stateInfo, ref sizeNeeded);
            MeasureRows(layoutInfo, stateInfo, constraintSize, ref sizeNeeded);

            SizeColumns(ref sizeNeeded);
            SizeColumnHeader(layoutInfo, ref sizeNeeded);

            MeasureElement(_Caption, layoutInfo, stateInfo, ref sizeNeeded);
            MeasureElement(_Title, layoutInfo, stateInfo, ref sizeNeeded);
            MeasureElement(_Filter, layoutInfo, stateInfo, ref sizeNeeded);
            MeasureElement(_Header, layoutInfo, stateInfo, ref sizeNeeded);
            MeasureElement(_GroupByRow, layoutInfo, stateInfo, ref sizeNeeded);
            MeasureElement(_Footer, layoutInfo, stateInfo, ref sizeNeeded);

            if (Parent != null)
                sizeNeeded.Width += 5;

            Size = sizeNeeded;
            SizeNeeded = sizeNeeded;
        }

        #region LoadLatentInsertRow

        private void LoadLatentInsertRow()
        {
            if (_LoadInsertRow == true)
            {
                _LoadInsertRow = false;

                if (_VirtualMode == false && Rows.FloatLastItem == true)
                {
                    GridRow row = Rows[Rows.Count - 1] as GridRow;

                    if (row != null)
                    {
                        row.RowIndex = Rows.Count - 1;

                        for (int i = row.Cells.Count; i < _Columns.Count; i++)
                        {
                            row.Cells.Add(new
                                GridCell(_Columns[i].DefaultNewRowCellValue));
                        }

                        SuperGrid.DoRowSetDefaultValuesEvent(this, row, NewRowContext.RowInit);
                    }
                }
            }
        }

        #endregion

        #region UpdateOrderLayout

        internal void UpdateOrderLayout()
        {
            if (VirtualMode == false)
            {
                if (NeedsGrouped || NeedsGroupSorted || NeedsSorted)
                {
                    if (CanReselectActiveItem() == true)
                        _ReselectActiveItem = true;

                    ClearAll(false);

                    if (NeedsGrouped || NeedsGroupSorted)
                    {
                        bool groupChanged = false;

                        if (NeedsGrouped == true)
                            groupChanged |= GroupItems();

                        if (NeedsGroupSorted == true)
                            groupChanged |= GroupSort();

                        NeedsGrouped = NeedsGroupSorted = false;

                        if (groupChanged == true)
                        {
                            if (_EnsureVisibleAfterGrouping == true && ActiveRow != null)
                                EnsureRowVisible = true;

                            SuperGrid.NeedToUpdateIndicees = true;

                            SuperGrid.DoRowsGroupedEvent(this);
                        }
                    }

                    if (NeedsSorted == true)
                    {
                        if (_EnsureVisibleAfterSort == true && ActiveRow != null)
                            EnsureRowVisible = true;

                        SortItems();

                        SuperGrid.NeedToUpdateIndicees = true;
                    }
                }
            }
            else
            {
                if (NeedsSorted == true)
                {
                    NeedsSorted = false;

                    DataBinder.UpdateDataSourceSort();

                    ClearAll(false);
                }
            }
        }

        #region CanReselectActiveItem

        private bool CanReselectActiveItem()
        {
            if (SuperGrid.ActiveElement != null)
            {
                if (SuperGrid.ActiveElement is GridCell)
                    return ((GridCell) SuperGrid.ActiveElement).IsSelected;

                if (SuperGrid.ActiveElement is GridContainer)
                    return ((GridContainer) SuperGrid.ActiveElement).IsSelected;
            }

            return (false);
        }

        #endregion

        #endregion

        #region UpdateIndicees

        internal void UpdateIndicees(
            GridPanel panel, GridItemsCollection items, bool vis)
        {
            if (NeedToUpdateIndicees == true)
            {
                NeedToUpdateIndicees = false;

                _NewFullIndex = 0;
                _NewGridIndex = 0;

                UpdateIndiceesEx(panel, items, vis);

                panel.TruncateGridIndexArray();
                panel.UpdateVisibleRowCount();
            }
        }

        private void UpdateIndiceesEx(
            GridPanel panel, GridItemsCollection items, bool vis)
        {
            int index = 0;

            for (int i = 0; i < items.Count; i++)
            {
                GridContainer row = items[i] as GridContainer;

                if (row != null)
                {
                    row.RowIndex = i;
                    row.FullIndex = _NewFullIndex++;

                    bool rowVisible = (vis == true && row.Visible);

                    if (rowVisible == true)
                    {
                        row.Index = index++;
                        row.GridIndex = panel.GetNewGridIndex(row);
                    }

                    if (row is GridPanel == false)
                        UpdateIndiceesEx(panel, row.Rows, rowVisible && row.Expanded);
                }
            }
        }

        #endregion

        #region MeasureColumnHeader

        private void MeasureColumnHeader(
            GridLayoutInfo layoutInfo, GridLayoutStateInfo stateInfo, Size constraintSize)
        {
            GridColumnHeader columnHeader = _ColumnHeader;

            if (columnHeader != null)
                columnHeader.Measure(layoutInfo, stateInfo, constraintSize);
        }
        
        private void MeasureColumnHeader(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, ref Size sizeNeeded)
        {
            GridColumnHeader columnHeader = _ColumnHeader;

            if (columnHeader != null)
            {
                columnHeader.Measure(layoutInfo, stateInfo, new Size(1, 0));

                if (columnHeader.Visible == true)
                    sizeNeeded.Height += columnHeader.Size.Height;
                else
                    sizeNeeded.Height++;
            }
        }

        #endregion

        #region MeasureColumns

        private void MeasureColumns(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, ref Size sizeNeeded)
        {
            int fillBase;
            int noFillWidth;

            CalculateFillData(layoutInfo,
                stateInfo, out fillBase, out noFillWidth);

            int width = CalculateColumnWidths(
                layoutInfo, fillBase, noFillWidth);

            if (_ShowRowHeaders == true)
                width += _RowHeaderWidth;

            sizeNeeded.Width = Math.Max(width, sizeNeeded.Width);
        }

        #region CalculateFillData

        private void CalculateFillData(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, out int fillBase, out int noFillWidth)
        {
            fillBase = 0;
            noFillWidth = 0;

            if (_ShowRowHeaders == true)
                noFillWidth += _RowHeaderWidth;

            GridColumnCollection columns = _Columns;
            foreach (GridColumn column in columns)
            {
                if (column.Visible == true)
                {
                    if (_VirtualMode == true)
                    {
                        switch (GetAutoSizeMode(column))
                        {
                            case ColumnAutoSizeMode.None:
                            case ColumnAutoSizeMode.ColumnHeader:
                                break;

                            default:
                                column.NeedsMeasured = true;
                               break;
                        }
                    }

                    column.Measure(layoutInfo, stateInfo, Size.Empty);

                    if (GetAutoSizeMode(column) == ColumnAutoSizeMode.Fill)
                        fillBase += column.FillWeight;
                    else
                        noFillWidth += column.Size.Width;
                }
            }
        }

        #endregion

        #region CalculateColumnWidths

        private int CalculateColumnWidths(
            GridLayoutInfo layoutInfo, int fillBase, int noFillWidth)
        {
            int width = 0;
            int n = layoutInfo.ClientBounds.Width - noFillWidth;

            GridColumn lastColumn = _Columns.LastVisibleColumn;

            GridColumnCollection columns = _Columns;

            int[] map = columns.DisplayIndexMap;
            for (int i = 0; i < map.Length; i++)
            {
                int index = map[i];

                GridColumn column = columns[index];

                if (column.Visible == true)
                {
                    Size size = column.Size;
                    ColumnAutoSizeMode autoSizeMode = GetAutoSizeMode(column);

                    if (fillBase > 0 && autoSizeMode == ColumnAutoSizeMode.Fill)
                    {
                        if (column.FillWeight == 0)
                        {
                            size.Width = column.MinimumWidth;
                        }
                        else
                        {
                            if (column == lastColumn)
                            {
                                size.Width = layoutInfo.ClientBounds.Right - width;

                                if (_ShowRowHeaders == true)
                                    size.Width -= _RowHeaderWidth;
                            }
                            else
                            {
                                size.Width = (int) (n*(float) column.FillWeight/fillBase);
                            }
                        }
                    }
                    else if (autoSizeMode == ColumnAutoSizeMode.None)
                    {
                        size.Width = column.Width;
                    }

                    size.Width = Math.Max(size.Width, column.MinimumWidth);

                    if (column.Size.Width != size.Width)
                    {
                        column.Size = size;

                        SuperGrid.DoColumnResizedEvent(this, column);
                    }

                    width += size.Width;
                }
            }

            return (width);
        }

        #endregion

        #endregion

        #region MeasureRows

        private void MeasureRows(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, Size constraintSize, ref Size sizeNeeded)
        {
            if (VirtualMode == true)
                MeasureVirtualRows(layoutInfo, stateInfo, constraintSize, ref sizeNeeded);
            else
                MeasureRealRows(layoutInfo, stateInfo, constraintSize, ref sizeNeeded);
        }

        #region MeasureVirtualRows

        private void MeasureVirtualRows(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, Size constraintSize, ref Size sizeNeeded)
        {
            if (VirtualRowCountEx > 0)
            {
                sizeNeeded.Height +=
                    (VirtualRowCountEx * stateInfo.GridPanel.VirtualRowHeight);
            }
            else
            {
                Size size = MeasureWhiteSpaceText(layoutInfo, constraintSize);

                sizeNeeded.Height += size.Height;
            }
        }

        #endregion

        #region MeasureRealRows

        private void MeasureRealRows(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, Size constraintSize, ref Size sizeNeeded)
        {
            RowHeaderSize = Size.Empty;
            Rectangle r = Rectangle.Empty;

            constraintSize.Width = sizeNeeded.Width;

            GridItemsCollection items = Rows;
            foreach (GridElement item in items)
            {
                if (item is GridTextRow == false)
                {
                    if (item.Visible == true)
                    {
                        item.Measure(layoutInfo, stateInfo, constraintSize);

                        r.Y += item.Size.Height;

                        sizeNeeded.Height += item.Size.Height;
                        sizeNeeded.Width = Math.Max(item.Size.Width, sizeNeeded.Width);
                    }
                    else
                    {
                        item.BoundsRelative = r;
                    }
                }
            }

            if (_VisibleRowCount == 0)
            {
                Size size = MeasureWhiteSpaceText(layoutInfo, constraintSize);

                sizeNeeded.Height += size.Height;
            }
        }

        #region MeasureWhiteSpaceText

        private Size MeasureWhiteSpaceText(
            GridLayoutInfo layoutInfo, Size constraintSize)
        {
            Size size = Size.Empty;

            if (String.IsNullOrEmpty(_NoRowsText) == false)
            {
                GridPanelVisualStyle style = GetEffectiveStyle();

                constraintSize.Width = BoundsRelative.Width - (8 + 5);

                if (_NoRowsMarkup != null)
                {
                    size = GetMarkupTextSize(layoutInfo, style, constraintSize.Width);
                }
                else
                {
                    eTextFormat tf = style.GetTextFormatFlags();

                    size = TextHelper.MeasureText(
                        layoutInfo.Graphics, _NoRowsText, style.Font, constraintSize, tf);
                }

                size.Height += 10;
            }

            return (size);
        }

        #region GetMarkupTextSize

        private Size GetMarkupTextSize(
            GridLayoutInfo layoutInfo, GridPanelVisualStyle style, int width)
        {
            Graphics g = layoutInfo.Graphics;

            MarkupDrawContext d =
                new MarkupDrawContext(g, style.Font, style.TextColor, false);

            _NoRowsMarkup.InvalidateElementsSize();
            _NoRowsMarkup.Measure(new Size(width, 0), d);

            return (_NoRowsMarkup.Bounds.Size);
        }

        #endregion

        #endregion

        #endregion

        #region SizeColumns

        private void SizeColumns(ref Size sizeNeeded)
        {
            GridColumnCollection columns = _Columns;
            foreach (GridColumn column in columns)
            {
                if (column.Visible == true)
                {
                    Size size = column.Size;
                    size.Height = sizeNeeded.Height;

                    if (ColumnHeader.Visible == true)
                        size.Height -= ColumnHeader.Size.Height;

                    column.Size = size;
                }
            }
        }

        #endregion

        #region SizeColumnHeader

        private void SizeColumnHeader(
            GridLayoutInfo layoutInfo, ref Size sizeNeeded)
        {
            GridColumnHeader columnHeader = _ColumnHeader;

            if (columnHeader != null)
            {
                Size size = columnHeader.Size;

                size.Width = (Parent == null)
                     ? Math.Max(sizeNeeded.Width, layoutInfo.ClientBounds.Width)
                     : sizeNeeded.Width;

                columnHeader.Size = size;
            }
        }

        #endregion

        #region MeasureElement

        private void MeasureElement(GridElement item,
            GridLayoutInfo layoutInfo, GridLayoutStateInfo stateInfo, ref Size sizeNeeded)
        {
            if (item != null && item.Visible == true)
            {
                item.Measure(layoutInfo, stateInfo, new Size(sizeNeeded.Width, 0));

                sizeNeeded.Height += item.Size.Height;
            }
        }

        #endregion

        #endregion

        #endregion

        #region ArrangeOverride

        /// <summary>
        /// Performs the arrange pass layout of the item when
        /// final position and size of the item has been set.
        /// </summary>
        /// <param name="layoutInfo">Layout information.</param>
        /// <param name="stateInfo"></param>
        /// <param name="layoutBounds"></param>
        protected override void ArrangeOverride(
            GridLayoutInfo layoutInfo, GridLayoutStateInfo stateInfo, Rectangle layoutBounds)
        {
            Rectangle bounds = layoutBounds;

            ArrangeCaption(layoutInfo, stateInfo, ref bounds);
            ArrangeTitle(layoutInfo, stateInfo, ref bounds);
            ArrangeGroupBox(layoutInfo, stateInfo, ref bounds);
            ArrangeColumnHeader(layoutInfo, stateInfo, ref bounds);
            ArrangeFilter(layoutInfo, stateInfo, ref bounds);
            ArrangeHeader(layoutInfo, stateInfo, ref bounds);
            ArrangeFooter(layoutInfo, stateInfo, ref bounds);

            FixedRowHeight = bounds.Y - layoutBounds.Y;
            FixedHeaderHeight = FixedRowHeight;

            ArrangeColumns(layoutInfo, stateInfo, bounds);
            ArrangeRows(layoutInfo, stateInfo, bounds);

            NeedsFilterScan = false;

            UpdateActivateRow();
        }

        #region ArrangeCaption

        private void ArrangeCaption(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, ref Rectangle bounds)
        {
            GridElement caption = _Caption;

            if (caption != null && caption.Visible == true)
            {
                caption.Arrange(layoutInfo, stateInfo,
                    new Rectangle(bounds.Location, caption.Size));

                bounds.Y += caption.Size.Height;
                bounds.Height -= caption.Size.Height;
            }
        }

        #endregion

        #region ArrangeTitle

        private void ArrangeTitle(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, ref Rectangle bounds)
        {
            GridElement title = _Title;

            if (title != null && title.Visible == true)
            {
                title.Arrange(layoutInfo, stateInfo,
                    new Rectangle(bounds.Location, title.Size));

                bounds.Y += title.Size.Height;
                bounds.Height -= title.Size.Height;
            }
        }

        #endregion
        
        #region ArrangeColumnHeader

        private void ArrangeColumnHeader(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, ref Rectangle bounds)
        {
            GridElement columnHeader = _ColumnHeader;

            if (columnHeader.Visible == true)
            {
                columnHeader.Arrange(layoutInfo, stateInfo,
                    new Rectangle(bounds.Location, columnHeader.Size));
                
                bounds.Y += columnHeader.Size.Height;
                bounds.Height -= columnHeader.Size.Height;
            }
        }

        #endregion

        #region ArrangeHeader

        private void ArrangeHeader(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, ref Rectangle bounds)
        {
            GridElement header = _Header;

            if (header != null && header.Visible == true)
            {
                header.Arrange(layoutInfo, stateInfo,
                    new Rectangle(bounds.Location, header.Size));

                bounds.Y += header.Size.Height;
                bounds.Height -= header.Size.Height;
            }
        }

        #endregion

        #region ArrangeFilter

        private void ArrangeFilter(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, ref Rectangle bounds)
        {
            GridElement filter = _Filter;

            if (filter != null && filter.Visible == true)
            {
                filter.Arrange(layoutInfo, stateInfo,
                    new Rectangle(bounds.Location, filter.Size));

                bounds.Y += filter.Size.Height;
                bounds.Height -= filter.Size.Height;
            }
        }

        #endregion

        #region ArrangeGroupBox

        private void ArrangeGroupBox(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, ref Rectangle bounds)
        {
            GridElement groupBox = _GroupByRow;

            if (groupBox != null && groupBox.Visible == true)
            {
                groupBox.Arrange(layoutInfo, stateInfo,
                    new Rectangle(bounds.Location, groupBox.Size));

                bounds.Y += groupBox.Size.Height;
                bounds.Height -= groupBox.Size.Height;
            }
        }

        #endregion

        #region ArrangeFooter

        private void ArrangeFooter(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, ref Rectangle bounds)
        {
            GridElement footer = _Footer;

            if (footer != null && footer.Visible == true)
            {
                Rectangle itemBounds =
                    new Rectangle(bounds.X, bounds.Bottom - footer.Size.Height,
                                  footer.Size.Width, footer.Size.Height);

                footer.Arrange(layoutInfo, stateInfo, itemBounds);
                bounds.Height -= itemBounds.Height;
            }
        }

        #endregion

        #region ArrangeColumns

        private void ArrangeColumns(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, Rectangle bounds)
        {
            if (ShowRowHeaders == true)
            {
                bounds.X += _RowHeaderWidth;
                bounds.Width -= _RowHeaderWidth;
            }

            GridColumnCollection columns = _Columns;

            int[] map = columns.DisplayIndexMap;
            for (int i = 0; i < map.Length; i++)
            {
                int index = map[i];

                GridColumn column = columns[index];

                if (column.Visible == true)
                {
                    Size size = column.Size;

                    column.Arrange(layoutInfo, stateInfo,
                        new Rectangle(bounds.Location, size));

                    bounds.X += size.Width;
                    bounds.Width -= size.Width;
                }
            }
        }

        #endregion

        #region ArrangeRows

        private void ArrangeRows(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, Rectangle bounds)
        {
            if (VirtualMode == true)
                ArrangeVirtualRows(layoutInfo, stateInfo, bounds);
            else
                ArrangeRealRows(layoutInfo, stateInfo, bounds);
        }

        #region ArrangeVirtualRows

        private void ArrangeVirtualRows(
            GridLayoutInfo layoutInfo, GridLayoutStateInfo stateInfo, Rectangle bounds)
        {
            GridPanel panel = stateInfo.GridPanel;

            _VisibleRowCount = VirtualRowCountEx;

            FixedRowHeight += (panel.VirtualRowHeight * panel.FrozenRowCount);

            Point currentLocation = bounds.Location;

            if (panel.VirtualRowCount > 0)
            {
                int start = panel.FirstOnScreenRowIndex;
                int end = panel.LastOnScreenRowIndex;

                currentLocation.Y += (start * _VirtualRowHeight);

                for (int i = start; i <= end; i++)
                {
                    GridRow row = panel.VirtualRows[i];

                    Size size = new Size(_ColumnHeader.Size.Width, row.Size.Height);
                    Rectangle itemBounds = new Rectangle(currentLocation, size);

                    row.Arrange(layoutInfo, stateInfo, itemBounds);

                    currentLocation.Y += itemBounds.Height;
                }
            }

            _WhiteSpaceRect = bounds;

            _WhiteSpaceRect.Y += (panel.VisibleRowCount * panel.VirtualRowHeight) - 1;
            _WhiteSpaceRect.Height = bounds.Bottom - _WhiteSpaceRect.Y;
        }

        #endregion

        #region ArrangeRealRows

        private void ArrangeRealRows(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, Rectangle bounds)
        {
            Point currentLocation = bounds.Location;
            Rectangle r = Rectangle.Empty;
            r.Location = currentLocation;

            int n = 0;

            GridItemsCollection items = Rows;
            foreach (GridElement item in items)
            {
                if (item.Visible == true)
                {
                    Size size = new Size(_ColumnHeader.Size.Width, item.Size.Height);
                    Rectangle itemBounds = new Rectangle(currentLocation, size);

                    item.Arrange(layoutInfo, stateInfo, itemBounds);

                    if (n++ < FrozenRowCount)
                        FixedRowHeight += itemBounds.Height;

                    currentLocation.Y += itemBounds.Height;
                }
                else
                {
                    r.Y = currentLocation.Y;
                    item.BoundsRelative = r;
                }
            }

            _WhiteSpaceRect = bounds;

            _WhiteSpaceRect.Y = currentLocation.Y - 1;
            _WhiteSpaceRect.Height = bounds.Bottom - _WhiteSpaceRect.Y;
        }

        #endregion

        #endregion

        #region UpdateActivateRow

        private void UpdateActivateRow()
        {
            GridContainer row = null;

            if (VirtualMode == true)
            {
                if (LatentActiveRowIndex >= 0 && LatentActiveRowIndex < VirtualRowCountEx)
                    row = VirtualRows[LatentActiveRowIndex];
                else
                    SetInitialVirtualSelection();
            }
            else
            {
                if (LatentActiveRowIndex >= 0 && LatentActiveRowIndex < Rows.Count)
                    row = Rows[LatentActiveRowIndex] as GridContainer;
                else
                    SetInitialRealSelection();
            }

            LatentActiveRowIndex = -1;

            if (row != null)
            {
                ClearAll();

                SetActiveRow(row, true);

                if (_LatentActiveCellIndex >= 0)
                {
                    GridCell cell = GetCell(row.RowIndex, _LatentActiveCellIndex);

                    if (cell != null)
                    {
                        SetActiveCell(cell);
                        SelectCell(cell);
                    }
                }
                else
                {
                    row.IsSelected = true;
                    SelectionRowAnchor = row;
                }

                EnsureRowVisible = true;
            }

            _LatentActiveCellIndex = -1;

            if (EnsureRowVisible == true)
            {
                EnsureRowVisible = false;

                if (ActiveRow != null)
                    ActiveRow.EnsureVisible(false);
            }

            if (_SelectActiveRow == true)
            {
                _SelectActiveRow = false;

                ClearAll();

                if (ActiveRow != null)
                    ActiveRow.IsSelected = true;
            }
        }

        #region SetInitialVirtualSelection

        private void SetInitialVirtualSelection()
        {
            if (ActiveRow == null && VirtualRowCountEx > 0)
            {
                int n = 0;

                switch (_InitialActiveRow)
                {
                    case RelativeRow.InsertionRow:
                        n = VirtualRowCountEx - 1;
                        break;

                    case RelativeRow.LastRow:
                        n = VirtualRowCountEx - 1;

                        if (ShowInsertRow == true && n > 0)
                            n--;
                        break;

                    case RelativeRow.None:
                        return;
                }

                SetInitialSelection(VirtualRows[n]);
            }
        }

        #endregion

        #region SetInitialRealSelection

        private void SetInitialRealSelection()
        {
            if (ActiveRow == null && Rows.Count > 0)
            {
                int n = 0;

                switch (_InitialActiveRow)
                {
                    case RelativeRow.InsertionRow:
                        n = Rows.Count - 1;
                        break;

                    case RelativeRow.LastRow:
                        n = Rows.Count - 1;

                        if (ShowInsertRow == true && n > 0)
                            n--;
                        break;

                    case RelativeRow.None:
                        return;
                }

                if (Rows[n] is GridRow)
                    SetInitialSelection((GridRow)Rows[n]);
            }
        }

        #endregion

        #region SetInitialSelection

        private void SetInitialSelection(GridRow row)
        {
            if (_InitialActiveRow != RelativeRow.None)
                SetActiveRow(row, false);

            switch (_InitialSelection)
            {
                case RelativeSelection.FirstCell:
                    SetFirstAvailableCell(Columns.FirstVisibleColumn);
                    break;

                case RelativeSelection.LastCell:
                    SetLastAvailableCell(Columns.LastVisibleColumn);
                    break;

                case RelativeSelection.PrimaryCell:
                    SetFirstAvailableCell(PrimaryColumn);
                    break;

                case RelativeSelection.Row:
                    SuperGrid.KeySelectRow(this, row, true, false);
                    break;

                default:
                    if (row != null)
                        row.EnsureVisible();
                    break;
            }
        }

        #endregion

        #region SetFirstAvailableCell

        private void SetFirstAvailableCell(GridColumn column)
        {
            if (column != null)
            {
                GridRow row = _ActiveRow as GridRow;

                if (row != null)
                {
                    GridCell ecell = row.GetCell(
                        column.ColumnIndex, AllowEmptyCellSelection);

                    while (ecell != null && ecell.AllowSelection == false)
                        ecell = ecell.GetNextCell(true, false);

                    if (ecell != null)
                        SelectCell(ecell);
                }
            }
        }

        #endregion

        #region SetLastAvailableCell

        private void SetLastAvailableCell(GridColumn column)
        {
            if (column != null)
            {
                GridRow row = _ActiveRow as GridRow;

                if (row != null)
                {
                    GridCell ecell = row.GetCell(
                        column.ColumnIndex, AllowEmptyCellSelection);

                    while (ecell != null && ecell.AllowSelection == false)
                        ecell = ecell.GetPreviousCell(true, false);

                    if (ecell != null)
                        SelectCell(ecell);
                }
            }
        }

        #endregion

        #region SelectCell

        private void SelectCell(GridCell cell)
        {
            if (cell != null)
            {
                if (SetActiveRow(cell.GridRow, true) == true)
                {
                    SelectionRowAnchor = cell.GridRow;
                    SelectionColumnAnchor = cell.GridColumn;
                }

                cell.ExtendSelection(this, cell, false);

                LastProcessedItem = cell;

                if (IsSubPanel == false)
                    cell.EnsureVisible();
            }
        }

        #endregion

        #endregion

        #endregion

        #region RenderOverride

        /// <summary>
        /// Performs drawing of the item and its children.
        /// </summary>
        /// <param name="renderInfo">Holds contextual rendering information.</param>
        protected override void RenderOverride(GridRenderInfo renderInfo)
        {
            Rectangle r = (Parent == null)
                ? SuperGrid.ClientRectangle : BoundsRelative;

            if (BoundsRelative.IntersectsWith(r))
            {
                RenderRows(renderInfo);
                RenderFooter(renderInfo);
                RenderColumnHeader(renderInfo);
                RenderGroupBox(renderInfo);
                RenderFilter(renderInfo);
                RenderHeader(renderInfo);
                RenderTitle(renderInfo);
                RenderCaption(renderInfo);
                RenderWhitespace(renderInfo);
                RenderBorder(renderInfo);

                if (IsDesignerHosted == true)
                {
                    foreach (GridColumn column in _Columns)
                        column.Render(renderInfo);

                    RenderDesignerElement(renderInfo, Bounds);
                }
            }

            FlushSelected();
        }

        #region RenderRows

        private void RenderRows(GridRenderInfo renderInfo)
        {
            Graphics g = renderInfo.Graphics;
            Rectangle t = SuperGrid.ViewRect;
            Rectangle k = SuperGrid.ViewRectEx;

            GridRenderInfo renderInfo2 = renderInfo;

            Region save = g.Clip;
            g.SetClip(k, CombineMode.Intersect);

            if (renderInfo.ClipRectangle.Y < t.Y)
            {
                Rectangle r = renderInfo.ClipRectangle;

                if (IsVFrozen == false)
                {
                    r.Y = t.Y;
                    r.Height -= (t.Y - r.Y);
                }

                renderInfo2 = new GridRenderInfo(g, r);
            }

            if (VirtualMode == true)
            {
                RenderVirtualRows(renderInfo2);
                RenderFrozenVirtualRows(renderInfo);
            }
            else
            {
                RenderRealRows(renderInfo2);
                RenderFrozenRealRows(renderInfo);
            }

            g.Clip = save;
        }

        #region RenderVirtualRows

        private void RenderVirtualRows(GridRenderInfo renderInfo)
        {
            Rectangle r = BoundsRelative;
            r.Size = new Size(_ColumnHeader.Size.Width, VirtualRowHeight);

            int frow = FirstOnScreenRowIndex;

            GridVirtualRows vrows = VirtualRows;
            for (int i = frow; i < VirtualRowCountEx; i++)
            {
                GridRow row = vrows[i];

                Rectangle bounds = row.VScrollBounds;

                if (bounds.Size != r.Size)
                {
                    row.FlushRow();
                    vrows.FreeRow(i);

                    row = vrows[i];

                    bounds = row.VScrollBounds;
                }

                if (IsSubPanel == true)
                    bounds.X -= HScrollOffset;

                if (bounds.IntersectsWith(renderInfo.ClipRectangle))
                    row.Render(renderInfo);

                if (bounds.Y > renderInfo.ClipRectangle.Bottom)
                    break;
            }
        }

        #endregion

        #region RenderFrozenVirtualRows

        private void RenderFrozenVirtualRows(GridRenderInfo renderInfo)
        {
            if (FrozenRowCount > 0 && IsSubPanel == false)
            {
                Rectangle r = BoundsRelative;
                r.Y += (FixedRowHeight - (FrozenRowCount * VirtualRowHeight));
                r.Size = new Size(_ColumnHeader.Size.Width, VirtualRowHeight);

                int n = Math.Min(FrozenRowCount, VirtualRowCountEx);

                GridVirtualRows vrows = VirtualRows;
                for (int i = 0; i < n; i++)
                {
                    GridRow row = vrows[i];

                    r.Y += VirtualRowHeight;

                    Rectangle bounds = row.BoundsRelative;

                    if (IsSubPanel == true)
                        bounds.X -= HScrollOffset;

                    if (bounds.IntersectsWith(renderInfo.ClipRectangle))
                        row.Render(renderInfo);

                    if (i + 1 == FrozenRowCount)
                    {
                        bounds.X -= 3;
                        bounds.Width += 3;

                        AddDropShadow(renderInfo, bounds, true, false);
                        break;
                    }

                    if (bounds.Y > renderInfo.ClipRectangle.Bottom)
                        break;
                }
            }
        }

        #endregion

        #region RenderRealRows

        private void RenderRealRows(GridRenderInfo renderInfo)
        {
            GridItemsCollection items = Rows;
            for (int i = FirstOnScreenRowIndex; i < items.Count; i++)
            {
                GridElement item = items[i];

                if (item.Visible == true)
                {
                    Rectangle r = item.BoundsRelative;

                    if (IsSubPanel == true)
                        r.X -= HScrollOffset;

                    if (item.IsVFrozen == false)
                        r.Y -= VScrollOffset;

                    if (r.Y > renderInfo.ClipRectangle.Bottom)
                        break;

                    if (r.IntersectsWith(renderInfo.ClipRectangle))
                        item.Render(renderInfo);
                }
            }
        }

        #endregion

        #region RenderFrozenRealRows

        private void RenderFrozenRealRows(GridRenderInfo renderInfo)
        {
            if ( FrozenRowCount > 0 && IsSubPanel == false)
            {
                int n = 0;

                GridItemsCollection items = Rows;
                foreach (GridElement item in items)
                {
                    if (item.Visible == true)
                    {
                        Rectangle r = item.BoundsRelative;

                        if (IsSubPanel == true)
                            r.X -= HScrollOffset;

                        if (r.IntersectsWith(renderInfo.ClipRectangle))
                            item.Render(renderInfo);

                        if (++n >= FrozenRowCount)
                        {
                            r.X -= 3;
                            r.Width += 3;
                            
                            AddDropShadow(renderInfo, r, true, false);
                            break;
                        }
                    }
                }
            }
        }

        #endregion

        #endregion

        #region RenderFooter

        private void RenderFooter(GridRenderInfo renderInfo)
        {
            GridElement footer = _Footer;

            if (footer != null && footer.Visible == true)
                footer.Render(renderInfo);
        }

        #endregion

        #region RenderColumnHeader

        private void RenderColumnHeader(GridRenderInfo renderInfo)
        {
            GridElement columnHeader = _ColumnHeader;

            if (columnHeader.Visible == true)
            {
                Rectangle bounds = columnHeader.BoundsRelative;

                if (IsSubPanel == true)
                    bounds.X -= HScrollOffset;

                if (IsVFrozen == false)
                    bounds.Y -= VScrollOffset;

                if (bounds.IntersectsWith(renderInfo.ClipRectangle))
                    columnHeader.Render(renderInfo);
            }
        }

        #endregion

        #region RenderGroupBox

        private void RenderGroupBox(GridRenderInfo renderInfo)
        {
            GridElement groupBox = _GroupByRow;

            if (groupBox != null && groupBox.Visible == true)
                groupBox.Render(renderInfo);
        }

        #endregion

        #region RenderFilter

        private void RenderFilter(GridRenderInfo renderInfo)
        {
            GridElement filter = _Filter;

            if (filter != null && filter.Visible == true)
                filter.Render(renderInfo);
        }

        #endregion

        #region RenderHeader

        private void RenderHeader(GridRenderInfo renderInfo)
        {
            GridElement header = _Header;

            if (header != null && header.Visible == true)
                header.Render(renderInfo);
        }

        #endregion

        #region RenderTitle

        private void RenderTitle(GridRenderInfo renderInfo)
        {
            GridElement title = _Title;

            if (title != null && title.Visible == true)
                title.Render(renderInfo);
        }

        #endregion

        #region RenderCaption

        private void RenderCaption(GridRenderInfo renderInfo)
        {
            GridElement caption = _Caption;

            if (caption != null && caption.Visible == true)
                caption.Render(renderInfo);
        }

        #endregion

        #region RenderBorder

        private void RenderBorder(GridRenderInfo renderInfo)
        {
            Graphics g = renderInfo.Graphics;

            Rectangle t = ViewRect;
            Rectangle r = PanelBounds;

            if (Parent != null)
            {
                r.Width = BoundsRelative.Width - 5;

                if (IsVFrozen == false)
                {
                    r.Intersect(t);

                    if (r.Y < t.Y)
                    {
                        r.Height -= (t.Y - r.Y);
                        r.Y = t.Y;
                    }
                }

                if (_ShowDropShadow == true)
                    AddDropShadow(renderInfo, r, true, true);

                t.Inflate(4, 4);

                if (r.Bottom > t.Bottom)
                    r.Height -= (r.Bottom - t.Bottom);
            }
            else
            {
                r = SuperGrid.ClientRectangle;
            }

            if (r.Width > 0 && r.Height > 0)
            {
                GridPanelVisualStyle style = GetEffectiveStyle();

                style.RenderBorder(g, r);
            }
        }

        #region AddDropShadow

        internal void AddDropShadow(
            GridRenderInfo renderInfo, Rectangle r, bool drawBot, bool drawRight)
        {
            Graphics g = renderInfo.Graphics;
            Color color = Color.Black;

            using (Pen pen1 = new Pen(Color.FromArgb(80, color)))
            {
                using (Pen pen2 = new Pen(Color.FromArgb(48, color)))
                {
                    using (Pen pen3 = new Pen(Color.FromArgb(16, color)))
                    {
                        Point pt1 = new Point(r.X + 3, r.Bottom);
                        Point pt2 = new Point(r.Right - 1, r.Bottom);
                        Point pt3 = new Point(r.Right, r.Y + 3);
                        Point pt4 = new Point(r.Right, r.Bottom - 1);

                        pt3.Y = Math.Max(pt3.Y, renderInfo.ClipRectangle.Y);
                        pt4.Y = Math.Min(pt4.Y, renderInfo.ClipRectangle.Bottom);

                        if (drawBot == true)
                        {
                            g.DrawLine(pen1, pt1, pt2);

                            pt1.X++; pt1.Y++;
                            pt2.X--; pt2.Y++;

                            g.DrawLine(pen2, pt1, pt2);

                            pt1.X++; pt1.Y++;
                            pt2.X--; pt2.Y++;

                            g.DrawLine(pen3, pt1, pt2);
                        }

                        if (drawRight == true)
                        {
                            g.DrawLine(pen1, pt3, pt4);

                            pt3.X++; pt3.Y++;
                            pt4.X++; pt4.Y--;

                            g.DrawLine(pen2, pt3, pt4);

                            pt3.X++; pt3.Y++;
                            pt4.X++; pt4.Y -= 2;

                            g.DrawLine(pen3, pt3, pt4);
                        }
                    }
                }
            }
        }

        #endregion

        #endregion

        #region RenderWhitespace

        private void RenderWhitespace(GridRenderInfo renderInfo)
        {
            Rectangle r = _WhiteSpaceRect;

            if (IsSubPanel == true)
                r.X -= HScrollOffset;

            if (IsVFrozen == false)
                r.Y -= VScrollOffset;

            r.Inflate(-1, -1);

            if (r.Width > 0 && r.Height > 0)
            {
                Graphics g = renderInfo.Graphics;

                if (r.IntersectsWith(renderInfo.ClipRectangle) == true)
                {
                    GridPanelVisualStyle pstyle = GetEffectiveStyle();

                    using (Brush br = pstyle.Background.GetBrush(r))
                        g.FillRectangle(br, r);

                    if (GridLines == GridLines.Vertical)
                    {
                        if (Rows.Count > 0 ||
                            (VirtualMode == true && VirtualRowCount > 0))
                        {
                            using (Pen pen = new Pen(pstyle.HorizontalLineColor))
                            {
                                pen.DashStyle = (DashStyle) pstyle.HorizontalLinePattern;

                                g.DrawLine(pen, r.X, r.Y, r.Right, r.Y);
                                g.DrawLine(pen, r.X, r.Bottom, r.Right, r.Bottom);
                            }
                        }
                    }

                    if (VisibleRowCount == 0)
                    {
                        r.Inflate(-2, -2);

                        if (r.Width > 0 && r.Height > 0)
                        {
                            if (String.IsNullOrEmpty(_NoRowsText) == false)
                            {
                                GridPanelVisualStyle style = GetEffectiveStyle();

                                if (_NoRowsMarkup != null)
                                {
                                    RenderTextMarkup(g, style, r);
                                }
                                else
                                {
                                    eTextFormat tf = style.GetTextFormatFlags();

                                    TextDrawing.DrawString(g,
                                        _NoRowsText, style.Font, style.TextColor, r, tf);
                                }
                            }
                        }
                    }
                }
            }
        }

        #region RenderTextMarkup

        private void RenderTextMarkup(
            Graphics g, GridPanelVisualStyle style, Rectangle r)
        {
            MarkupDrawContext d =
                new MarkupDrawContext(g, style.Font, style.TextColor, false);

            _NoRowsMarkup.Arrange(new Rectangle(Point.Empty, r.Size), d);

            Size size = _NoRowsMarkup.Bounds.Size;

            switch (style.Alignment)
            {
                case Alignment.NotSet:
                case Alignment.MiddleLeft:
                case Alignment.MiddleCenter:
                case Alignment.MiddleRight:
                    if (r.Height > size.Height)
                        r.Y += (r.Height - size.Height) / 2;
                    break;

                case Alignment.BottomLeft:
                case Alignment.BottomCenter:
                case Alignment.BottomRight:
                    if (r.Height > size.Height)
                        r.Y = r.Bottom - size.Height;
                    break;
            }

            _NoRowsMarkup.Bounds = new Rectangle(r.Location, size);

            Region oldClip = g.Clip;

            try
            {
                g.SetClip(r, CombineMode.Intersect);

                _NoRowsMarkup.Render(d);
            }
            finally
            {
                g.Clip = oldClip;
            }
        }

        #endregion

        #endregion

        #region RenderDesignerElement

        private void RenderDesignerElement(
            GridRenderInfo renderInfo, Rectangle r)
        {
            if (IsDesignerHosted == true)
            {
                if (SuperGrid.DesignerElement == this)
                {
                    r.X -= 2;
                    r.Y -= 3;
                    r.Height += 3;
                    r.Width += 3;

                    if (ShowDropShadow == true)
                    {
                        r.Width += 2;
                        r.Height += 2;
                    }

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
        }

        #endregion

        #region GetTreeButtonSize

        internal Size GetTreeButtonSize()
        {
            Size size = (ExpandImage != null)
                ? ExpandImage.Size : TreeButtonSize;

            Size cSize = (CollapseImage != null)
                ? CollapseImage.Size : TreeButtonSize;

            size.Width = Math.Max(size.Width, cSize.Width);
            size.Height = Math.Max(size.Height, cSize.Height);

            return (size);
        }

        #region TreeButtonSize

        private Size TreeButtonSize
        {
            get
            {
                switch (GetExpandButtonType())
                {
                    case ExpandButtonType.Circle:
                        return (new Size(12, 12));

                    case ExpandButtonType.Square:
                        return (new Size(12, 12));

                    case ExpandButtonType.Triangle:
                        return (new Size(16, 16));
                }

                return (Size.Empty);
            }
        }

        #endregion

        #endregion

        #region GetExpandButton

        internal Image GetExpandButton(Graphics g, bool hot)
        {
            if (_ExpandImage != null)
                return (_ExpandImage);

            if (hot == true)
            {
                if (_ExpandButtonHotImage == null)
                {
                    switch (GetExpandButtonType())
                    {
                        case ExpandButtonType.Circle:

                            ExpandButtonHotImage = GetCircleExpandButton(g, true);
                            break;

                        case ExpandButtonType.Square:
                            ExpandButtonHotImage = GetSquareExpandButton(g, true);
                            break;

                        case ExpandButtonType.Triangle:
                            ExpandButtonHotImage = GetTriangleExpandButton(g, true);
                            break;
                    }
                }

                return (_ExpandButtonHotImage);
            }

            if (_ExpandButtonImage == null)
            {
                switch (GetExpandButtonType())
                {
                    case ExpandButtonType.Circle:
                        ExpandButtonImage = GetCircleExpandButton(g, false);
                        break;

                    case ExpandButtonType.Square:
                        ExpandButtonImage = GetSquareExpandButton(g, false);
                        break;

                    case ExpandButtonType.Triangle:
                        ExpandButtonImage = GetTriangleExpandButton(g, false);
                        break;
                }
            }

            return (_ExpandButtonImage);

        }

        #endregion

        #region GetCollapseButton

        internal Image GetCollapseButton(Graphics g, bool hot)
        {
            if (_CollapseImage != null)
                return (_CollapseImage);

            if (hot == true)
            {
                if (_CollapseButtonHotImage == null)
                {
                    switch (GetExpandButtonType())
                    {
                        case ExpandButtonType.Circle:
                            CollapseButtonHotImage = GetCircleCollapseButton(g, true);
                            break;

                        case ExpandButtonType.Square:
                            CollapseButtonHotImage = GetSquareCollapseButton(g, true);
                            break;

                        case ExpandButtonType.Triangle:
                            CollapseButtonHotImage = GetTriangleCollapseButton(g, true);
                            break;
                    }
                }

                return (_CollapseButtonHotImage);
            }

            if (_CollapseButtonImage == null)
            {
                switch (GetExpandButtonType())
                {
                    case ExpandButtonType.Circle:
                        CollapseButtonImage = GetCircleCollapseButton(g, false);
                        break;

                    case ExpandButtonType.Square:
                        CollapseButtonImage = GetSquareCollapseButton(g, false);
                        break;

                    case ExpandButtonType.Triangle:
                        CollapseButtonImage = GetTriangleCollapseButton(g, false);
                        break;
                }
            }

            return (_CollapseButtonImage);

        }

        #endregion

        #region Circle button support

        #region GetCircleExpandButton

        private Bitmap GetCircleExpandButton(Graphics g, bool hot)
        {
            Bitmap bmp = new Bitmap(13, 13, g);
            Rectangle r = new Rectangle(0, 0, 12, 12);

            r.Inflate(-2, -2);

            GridPanelVisualStyle style = GetEffectiveStyle();

            Color borderColor = (hot == true)
                ? style.CircleTreeButtonStyle.ExpandButton.HotBorderColor
                : style.CircleTreeButtonStyle.ExpandButton.BorderColor;

            Color lineColor = (hot == true)
                ? style.CircleTreeButtonStyle.ExpandButton.HotLineColor
                : style.CircleTreeButtonStyle.ExpandButton.LineColor;

            Background background = (hot == true)
                ? style.CircleTreeButtonStyle.ExpandButton.HotBackground
                : style.CircleTreeButtonStyle.ExpandButton.Background;

            using (Graphics gBmp = Graphics.FromImage(bmp))
            {
                gBmp.CompositingQuality = CompositingQuality.HighQuality;
                gBmp.SmoothingMode = SmoothingMode.AntiAlias;

                using (Pen pen = new Pen(lineColor))
                {
                    DrawCircleButtonBase(gBmp, pen, borderColor, background, r);

                    int x = r.X + r.Width / 2;

                    gBmp.DrawLine(pen, x, r.Y + 2, x, r.Bottom - 2);
                }
            }

            return (bmp);
        }

        #endregion

        #region GetCircleCollapseButton

        private Bitmap GetCircleCollapseButton(Graphics g, bool hot)
        {
            Bitmap bmp = new Bitmap(13, 13, g);
            Rectangle r = new Rectangle(0, 0, 12, 12);

            r.Inflate(-2, -2);

            GridPanelVisualStyle style = GetEffectiveStyle();

            Color borderColor = (hot == true)
                ? style.CircleTreeButtonStyle.CollapseButton.HotBorderColor
                : style.CircleTreeButtonStyle.CollapseButton.BorderColor;

            Color lineColor = (hot == true)
                ? style.CircleTreeButtonStyle.CollapseButton.HotLineColor
                : style.CircleTreeButtonStyle.CollapseButton.LineColor;

            Background background = (hot == true)
                ? style.CircleTreeButtonStyle.CollapseButton.HotBackground
                : style.CircleTreeButtonStyle.CollapseButton.Background;

            using (Graphics gBmp = Graphics.FromImage(bmp))
            {
                gBmp.CompositingQuality = CompositingQuality.HighQuality;
                gBmp.SmoothingMode = SmoothingMode.AntiAlias;

                using (Pen pen = new Pen(lineColor))
                    DrawCircleButtonBase(gBmp, pen, borderColor, background, r);
            }

            return (bmp);
        }

        #endregion

        #region DrawCircleButtonBase

        private void DrawCircleButtonBase(Graphics g,
            Pen pen, Color borderColor, Background background, Rectangle r)
        {
            Rectangle t = r;
            t.X++;
            t.Y++;
            t.Width--;
            t.Height--;

            using (Brush br = background.GetBrush(r))
                g.FillRectangle(br, t);

            using (Pen penBorder = new Pen(borderColor))
                g.DrawEllipse(penBorder, r);

            int y = r.Y + r.Height / 2;

            g.DrawLine(pen, r.X + 2, y, r.Right - 2, y);
        }

        #endregion

        #endregion

        #region Square button support

        #region GetSquareExpandButton

        private Bitmap GetSquareExpandButton(Graphics g, bool hot)
        {
            Bitmap bmp = new Bitmap(13, 13, g);
            Rectangle r = new Rectangle(0, 0, 12, 12);

            r.Inflate(-2, -2);

            GridPanelVisualStyle style = GetEffectiveStyle();

            Color borderColor = (hot == true)
                ? style.SquareTreeButtonStyle.ExpandButton.HotBorderColor
                : style.SquareTreeButtonStyle.ExpandButton.BorderColor;

            Color lineColor = (hot == true)
                ? style.SquareTreeButtonStyle.ExpandButton.HotLineColor
                : style.SquareTreeButtonStyle.ExpandButton.LineColor;

            Background background = (hot == true)
                ? style.SquareTreeButtonStyle.ExpandButton.HotBackground
                : style.SquareTreeButtonStyle.ExpandButton.Background;

            using (Graphics gBmp = Graphics.FromImage(bmp))
            {
                using (Pen pen = new Pen(lineColor))
                {
                    DrawSquareButtonBase(gBmp, pen, borderColor, background, r);

                    int x = r.X + r.Width/2;

                    gBmp.DrawLine(pen, x, r.Y + 2, x, r.Bottom - 2);
                }
            }

            return (bmp);
        }

        #endregion

        #region GetSquareCollapseButton

        private Bitmap GetSquareCollapseButton(Graphics g, bool hot)
        {
            Bitmap bmp = new Bitmap(13, 13, g);
            Rectangle r = new Rectangle(0, 0, 12, 12);

            r.Inflate(-2, -2);

            GridPanelVisualStyle style = GetEffectiveStyle();

            Color borderColor = (hot == true)
                ? style.SquareTreeButtonStyle.CollapseButton.HotBorderColor
                : style.SquareTreeButtonStyle.CollapseButton.BorderColor;

            Color lineColor = (hot == true)
                ? style.SquareTreeButtonStyle.CollapseButton.HotLineColor
                : style.SquareTreeButtonStyle.CollapseButton.LineColor;

            Background background = (hot == true)
                ? style.SquareTreeButtonStyle.CollapseButton.HotBackground
                : style.SquareTreeButtonStyle.CollapseButton.Background;

            using (Graphics gBmp = Graphics.FromImage(bmp))
            {
                using (Pen pen = new Pen(lineColor))
                    DrawSquareButtonBase(gBmp, pen, borderColor, background, r);
            }

            return (bmp);
        }

        #endregion

        #region DrawSquareButtonBase

        private void DrawSquareButtonBase(Graphics g,
            Pen pen, Color borderColor, Background background, Rectangle r)
        {
            Rectangle t = r;
            t.X++;
            t.Y++;
            t.Width--;
            t.Height--;

            using (Brush br = background.GetBrush(r))
                g.FillRectangle(br, t);

            Color foo = Color.FromArgb(140, borderColor);

            using (Pen cpen = new Pen(foo))
                g.DrawRectangle(cpen, r);

            using (Pen borderPen = new Pen(borderColor))
            {
                g.DrawLine(borderPen, r.X + 1, r.Y, r.Right - 1, r.Y);
                g.DrawLine(borderPen, r.Right, r.Y + 1, r.Right, r.Bottom - 1);
                g.DrawLine(borderPen, r.X + 1, r.Bottom, r.Right - 1, r.Bottom);
                g.DrawLine(borderPen, r.X, r.Y + 1, r.X, r.Bottom - 1);
            }

            int y = r.Y + r.Height / 2;

            g.DrawLine(pen, r.X + 2, y, r.Right - 2, y);
        }

        #endregion

        #endregion

        #region Triangle button support

        #region GetTriangleExpandButton

        private Image GetTriangleExpandButton(Graphics g, bool hot)
        {
            Bitmap bmp = new Bitmap(11, 15, g);
            Rectangle r = new Rectangle(0, 0, 11, 15);

            GridPanelVisualStyle style = GetEffectiveStyle();

            Color borderColor = (hot == true)
                ? style.TriangleTreeButtonStyle.ExpandButton.HotBorderColor
                : style.TriangleTreeButtonStyle.ExpandButton.BorderColor;

            Background background = (hot == true)
                ? style.TriangleTreeButtonStyle.ExpandButton.HotBackground
                : style.TriangleTreeButtonStyle.ExpandButton.Background;

            using (Graphics gBmp = Graphics.FromImage(bmp))
            {
                gBmp.CompositingQuality = CompositingQuality.HighQuality;
                gBmp.SmoothingMode = SmoothingMode.AntiAlias;

                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddLines(new Point[]
                    {
                        new Point(3, 3),
                        new Point(7, 7),
                        new Point(3, 11),
                    });

                    path.CloseAllFigures();

                    using (Brush br = background.GetBrush(r))
                        gBmp.FillPath(br, path);

                    using (Pen pen = new Pen(borderColor))
                    {
                        gBmp.DrawPath(pen, path);
                        gBmp.DrawPath(pen, path);
                    }
                }
            }

            return (bmp);
        }

        #endregion

        #region GetTriangleCollapseButton

        private Bitmap GetTriangleCollapseButton(Graphics g, bool hot)
        {
            Bitmap bmp = new Bitmap(12, 12, g);
            Rectangle r = new Rectangle(0, 0, 12, 12);

            GridPanelVisualStyle style = GetEffectiveStyle();

            Color borderColor = (hot == true)
                ? style.TriangleTreeButtonStyle.CollapseButton.HotBorderColor
                : style.TriangleTreeButtonStyle.CollapseButton.BorderColor;

            Background background = (hot == true)
                ? style.TriangleTreeButtonStyle.CollapseButton.HotBackground
                : style.TriangleTreeButtonStyle.CollapseButton.Background;

            using (Graphics gBmp = Graphics.FromImage(bmp))
            {
                gBmp.CompositingQuality = CompositingQuality.HighQuality;
                gBmp.SmoothingMode = SmoothingMode.AntiAlias;

                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddLines(new Point[]
                    {
                        new Point(8, 3),
                        new Point(8, 8),
                        new Point(3, 8),
                   });

                    path.CloseAllFigures();

                    using (Brush br = background.GetBrush(r))
                        gBmp.FillPath(br, path);

                    using (Pen pen = new Pen(borderColor))
                    {
                        gBmp.DrawPath(pen, path);
                        gBmp.DrawPath(pen, path);
                    }
                }
            }

            return (bmp);
        }

        #endregion

        #endregion

        #endregion

        #region Mouse handling

        #region InternalMouseEnter

        internal override void InternalMouseEnter(EventArgs e)
        {
            base.InternalMouseEnter(e);

            StyleState rowState = GetRowState(this);
            RowVisualStyle style = GetEffectiveRowStyle(this, rowState);

            rowState &= ~StyleState.MouseOver;

            if (GetEffectiveRowStyle(this, rowState).Background != style.Background)
                RefreshContainer();
        }

        #endregion

        #region InternalMouseLeave

        internal override void InternalMouseLeave(EventArgs e)
        {
            HotItem = null;

            base.InternalMouseLeave(e);

            StyleState rowState = GetRowState(this);
            RowVisualStyle style = GetEffectiveRowStyle(this, rowState);

            rowState |= StyleState.MouseOver;

            if (GetEffectiveRowStyle(this, rowState).Background != style.Background)
                RefreshContainer();
        }

        #endregion

        #region InternalMouseMove

        /// <summary>
        /// InternalMouseMove
        /// </summary>
        /// <param name="e"></param>
        internal override void InternalMouseMove(MouseEventArgs e)
        {
            if (_WhiteSpaceRect.Contains(e.Location) == true)
            {
                SuperGrid.GridCursor = Cursors.Default;

                if (_NoRowsMarkup != null)
                    _NoRowsMarkup.MouseMove(SuperGrid, e);
            }

            base.InternalMouseMove(e);
        }

        #endregion

        #region InternalMouseDown

        private bool _DownInWhitespace;

        internal override void InternalMouseDown(MouseEventArgs e)
        {
            if (_WhitespaceClickBehavior == WhitespaceClickBehavior.ClearSelection)
            {
                _DownInWhitespace = _WhiteSpaceRect.Contains(e.Location);

                if (_DownInWhitespace == true)
                {
                    GridCell ecell = SuperGrid.EditorCell;

                    if ((ecell == null) || ecell.EndEdit())
                        ClearAll();
                }
            }

            base.InternalMouseDown(e);
        }

        #endregion

        #region InternalMouseUp

        internal override void InternalMouseUp(MouseEventArgs e)
        {
            if (_WhiteSpaceRect.Contains(e.Location) == true)
            {
                if (_DownInWhitespace == true)
                {
                    if (_NoRowsMarkup != null)
                        _NoRowsMarkup.Click(SuperGrid);
                }
            }

            base.InternalMouseUp(e);
        }

        #endregion

        #region RefreshContainer

        private void RefreshContainer()
        {
            if (Parent != null)
            {
                Rectangle r = ContainerBounds;

                if (IsSubPanel == true)
                    r.X -= HScrollOffset;

                if (IsVFrozen == false)
                    r.Y -= VScrollOffset;

                Parent.InvalidateRender(r);
            }
        }

        #endregion

        #endregion

        #region GetElementAt

        /// <summary>
        /// Gets the current panel element at the given location.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public override GridElement GetElementAt(MouseEventArgs e)
        {
            return (GetElementAt(e.X, e.Y));
        }

        /// <summary>
        /// Gets the current panel element at the given location.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public override GridElement GetElementAt(int x, int y)
        {
            if (SubElementAt(_ColumnHeader, x, y) == true)
                return (_ColumnHeader);

            if (SubElementAt(_Caption, x, y) == true)
                return (_Caption);

            if (SubElementAt(_GroupByRow, x, y) == true)
                return (_GroupByRow);

            if (SubElementAt(_Title, x, y) == true)
                return (_Title);

            if (SubElementAt(_Header, x, y) == true)
                return (_Header);

            if (SubElementAt(_Filter, x, y) == true)
                return (_Filter);

            if (SubElementAt(_Footer, x, y) == true)
                return (_Footer);

            if (VirtualMode == true)
                return (GetVirtualElementAt(x, y));

            return (base.GetElementAt(x, y));
        }

        #region SubElementAt

        private bool SubElementAt(GridElement item, int x, int y)
        {
            if (item != null && item.Visible == true)
            {
                Rectangle r = item.BoundsRelative;
                r.X -= HScrollOffset;

                if (IsVFrozen == false)
                    r.Y -= VScrollOffset;

                return (r.Contains(x, y));
            }

            return (false);
        }

        #endregion

        #endregion

        #region GetVirtualElementAt

        private GridElement GetVirtualElementAt(int x, int y)
        {
            GridPanel panel = GridPanel;
            if (panel != null && panel.IsSubPanel == false)
            {
                if (panel.FrozenRowCount > 0)
                {
                    int n = Math.Min(panel.FrozenRowCount, VirtualRowCountEx);

                    for (int i = 0; i < n; i++)
                    {
                        GridElement item = VirtualRows[i];

                        if (item != null)
                        {
                            Rectangle bounds = item.BoundsRelative;
                            bounds.X -= HScrollOffset;

                            if (bounds.Height <= 0 ||
                                bounds.Y > item.SuperGrid.ClientRectangle.Bottom)
                                break;

                            if (bounds.Contains(x, y))
                                return (item);
                        }
                    }
                }
            }

            for (int i = FirstOnScreenRowIndex; i < VirtualRowCountEx; i++)
            {
                GridElement item = VirtualRows[i];

                if (item != null)
                {
                    Rectangle bounds = item.BoundsRelative;
                    bounds.X -= HScrollOffset;

                    if (item.IsVFrozen == false)
                        bounds.Y -= VScrollOffset;

                    if (bounds.Height <= 0 ||
                        bounds.Y > item.SuperGrid.ClientRectangle.Bottom)
                        break;

                    if (bounds.Contains(x, y))
                        return (item);
                }
            }

            return (null);
        }

        #endregion

        #region GetColumnAt

        ///<summary>
        /// Gets the GridColumn containing the specified location.
        ///</summary>
        ///<param name="pt">Point to test</param>
        ///<returns>Column or null</returns>
        public GridColumn GetColumnAt(Point pt)
        {
            Rectangle t = SuperGrid.SViewRect;

            foreach (GridColumn column in _Columns)
            {
                Rectangle r = column.BoundsRelative;

                if (IsSubPanel == true || column.IsHFrozen == false)
                    r.X -= HScrollOffset;

                r.Y -= VScrollOffset;

                r.Intersect(t);

                if (r.Contains(pt) == true)
                    return (column);
            }

            return (null);
        }

        #endregion

        #region GetNewGridIndex

        internal int GetNewGridIndex(GridContainer item)
        {
            int gridIndex = _NewGridIndex++;

            if (gridIndex >= _GridIndexArray.Length)
            {
                int n = _GridIndexArray.Length;
                n = Math.Min(n * 2, n + 2048);

                GridContainer[] array = new GridContainer[n];

                _GridIndexArray.CopyTo(array, 0);
                _GridIndexArray = array;
            }

            _GridIndexArray[gridIndex] = item;

            return (gridIndex);
        }

        #endregion

        #region TruncateGridIndexArray

        internal void TruncateGridIndexArray()
        {
            if (_GridIndexArray != null)
            {
                for (int i = _NewGridIndex; i < _GridIndexArray.Length; i++)
                    _GridIndexArray[i] = null;
            }
        }

        #endregion

        #region InvalidateRender

        /// <summary>
        /// Invalidates the display state of the panel.
        /// </summary>
        public override void InvalidateRender()
        {
            if (ShowDropShadow == true && IsSubPanel == true)
            {
                Rectangle r = BoundsRelative;

                r.X -= HScrollOffset;

                if (IsVFrozen == false)
                    r.Y -= VScrollOffset;

                r.Width += 3;
                r.Height += 3;

                InvalidateRender(r);
            }
            else
            {
                base.InvalidateRender();
            }
        }

        #endregion

        #region SetActiveRow

        ///<summary>
        /// Sets the current Active Row
        ///</summary>
        ///<param name="row">Row to make active.</param>
        ///<returns>true is successful</returns>
        public bool SetActiveRow(GridContainer row)
        {
            if (row != null)
            {
                if (row.GridPanel != this)
                    throw new Exception("GridPanel does not contain the given row!");
            }

            return (SetActiveRow(row, true));
        }

        internal bool SetActiveRow(GridContainer ncont, bool activate)
        {
            GridContainer ocont = SuperGrid.ActiveRow;

            if (ocont != ncont && ncont is GridPanel == false)
            {
                if (DeactivateEdit() == false)
                    return (false);

                if (DeactivateRow(ocont, ncont) == false)
                    return (false);

                if (ActivateRow(ocont, ncont, activate) == false)
                    return (false);
            }

            if (ocont != ncont && ncont != null)
                LastProcessedItem = ncont;

            return (true);
        }

        #region DeactivateEdit

        internal bool DeactivateEdit()
        {
            GridCell ecell = SuperGrid.EditorCell;

            if (ecell != null)
            {
                if (ecell.EndEdit() == false)
                    return (false);
            }

            return (true);
        }

        #endregion

        #region DeactivateRow

        private bool DeactivateRow(GridContainer ocont, GridContainer ncont)
        {
            GridRow orow = ocont as GridRow;

            if (orow != null)
            {
                if (orow.RowNeedsStored == true)
                {
                    if (SuperGrid.DoRowValidatingEvent(orow) == true)
                    {
                        if (ncont is GridRow || ncont == null)
                            SystemSounds.Beep.Play();

                        return (false);
                    }

                    SuperGrid.DoRowValidatedEvent(orow);
                }

                orow.EditorDirty = false;

                if (orow.IsTempInsertRow == true && orow.IsInsertRow == false)
                {
                    orow.IsTempInsertRow = false;

                    if (InsertNewRow(orow, orow.RowIndex) == false)
                        return (false);

                    orow.RowNeedsStored = true;
                    orow.RowNeedsSorted = true;

                    if (VirtualMode == true)
                    {
                        VirtualTempInsertRow = null;
                        VirtualRows.MaxRowIndex = orow.RowIndex;
                    }
                }

                if (orow.RowNeedsStored == true)
                    orow.FlushRow();

                if (orow.RowNeedsSorted == true)
                {
                    orow.RowNeedsSorted = false;

                    if (VirtualMode == false)
                        UpdateRowPosition(orow);
                    else
                    {
                        VirtualRows.Clear();
                        InvalidateLayout();
                    }
                }

                if (orow.IsInsertRow == true)
                    SuperGrid.DoRowSetDefaultValuesEvent(this, orow, NewRowContext.RowDeactivate);
            }

            return (true);
        }

        #endregion

        #region ActivateRow

        private bool ActivateRow(
            GridContainer ocont, GridContainer ncont, bool activate)
        {
            if (ncont != null)
            {
                GridRow nrow = ncont as GridRow;

                if (nrow != null)
                {
                    ActiveRow = ncont;

                    if (DataBinder.SetPosition(nrow) == false)
                    {
                        if (ocont is GridRow)
                            ((GridRow)ocont).RowDirty = _PreActiveRowDirty;

                        ActiveRow = ocont;

                        return (false);
                    }

                    _PreActiveRowDirty = nrow.RowDirty;
                }

                ActiveRow = ncont;

                if (activate == true || SuperGrid.ActiveRow == null)
                {
                    SuperGrid.ActiveRow = ncont;
                    SuperGrid.ActiveElement = ncont;

                    if (nrow != null && nrow.IsInsertRow == true)
                    {
                        for (int i = nrow.Cells.Count; i < _Columns.Count; i++)
                            nrow.Cells.Add(new GridCell(_Columns[i].DefaultNewRowCellValue));

                        SuperGrid.DoRowSetDefaultValuesEvent(this, nrow, NewRowContext.RowActivate);
                    }
                }
            }
            else
            {
                if (activate == true)
                    ActiveRow = ncont;
            }

            return (true);
        }

        #endregion

        #endregion

        #region FlushActiveRow

        ///<summary>
        ///Should be called when bound rows have been inserted, to make sure
        ///they have been flushed to the data source prior to issuing an Update
        ///</summary>
        ///<returns>true if successful</returns>
        public bool FlushActiveRow()
        {
            if (ActiveRow != null)
            {
                if (DeactivateEdit() == true)
                    return (DeactivateRow(ActiveRow, null));
            }

            return (true);
        }

        #endregion

        #region SetActiveCell

        ///<summary>
        /// Sets the given cell as Active
        ///</summary>
        ///<param name="cell">Cell to make active</param>
        ///<returns>true - if successful</returns>
        public bool SetActiveCell(GridCell cell)
        {
            if (cell != null)
            {
                if (cell.GridPanel != this)
                    throw new Exception("GridPanel does not contain the given cell!");

                return (SuperGrid.KeySelectCell(this, cell, false, false));
            }
            
            GridCell ecell = SuperGrid.EditorCell;

            if (ecell != null)
            {
                if (ecell.EndEdit() == false)
                    return (false);
            }

            if (SuperGrid.DoCellActivatingEvent(this, SuperGrid.ActiveCell, null) == true)
                return (false);

            if (SelectionGranularity == SelectionGranularity.Cell)
            {
                SuperGrid.ActiveCell = null;
                SuperGrid.ActiveElement = null;

                LastProcessedItem = null;
            }

            return (true);
        }

        #endregion

        #region ActivateFilterPopup

        ///<summary>
        ///Activates the given column's FilterPopup
        ///</summary>
        ///<param name="column"></param>
        public void ActivateFilterPopup(GridColumn column)
        {
            if (column != null)
                column.ActivateFilterPopup();
        }

        #endregion

        #region ClearDirtyRowMarkers

        ///<summary>
        /// Clears all dirty row markers.
        ///</summary>
        public void ClearDirtyRowMarkers()
        {
            if (VirtualMode == true)
            {
                VirtualRows.Clear();

                InvalidateRender();
            }
            else
            {
                foreach (GridRow row in Rows)
                    row.RowDirty = false;
            }
        }

        #endregion

        #region Sort support

        #region AddSort

        #region AddSort(col)

        ///<summary>
        /// This routine is used to add a single column to the
        /// list of columns used to sort grid data rows.
        ///</summary>
        public void AddSort(GridColumn column)
        {
            AddSort(column, SortDirection.Ascending);
        }

        #endregion

        #region AddSort(col, dir)

        ///<summary>
        /// This routine is used to add a single column, and
        /// its associated sorting direction, to the list of
        /// columns used to sort grid data rows.
        ///</summary>
        public void AddSort(GridColumn column, SortDirection sortDirection)
        {
            column.SortDirection = sortDirection;

            AddSortToList(column);
        }

        #endregion

        #region AddSort(col[])

        ///<summary>
        /// This routine is used to add an array of column to the
        /// list of columns used to sort grid data rows.
        ///</summary>
        public void AddSort(GridColumn[] columns)
        {
            AddSort(columns, SortDirection.Ascending);
        }

        #endregion

        #region AddSort(col[], dir)

        ///<summary>
        /// This routine is used to add an array of columns and
        /// a specified sort direction to the list of columns
        /// used to sort grid data rows.
        ///</summary>
        public void AddSort(GridColumn[] columns, SortDirection sortDirection)
        {
            try
            {
                _InRangeAdd = true;

                foreach (GridColumn column in columns)
                    AddSort(column, sortDirection);
            }
            finally
            {
                _InRangeAdd = false;
            }

            InvalidateLayout();
        }

        #endregion

        #region AddSort(col[], dir[])

        ///<summary>
        /// This routine is used to add an array of columns, and
        /// their respective sort directions, to the list of columns
        /// used to sort grid data rows.
        ///</summary>
        public void AddSort(
            GridColumn[] columns, SortDirection[] sortDirection)
        {
            try
            {
                _InRangeAdd = true;

                for (int i=0; i<columns.Length; i++)
                    AddSort(columns[i], sortDirection[i]);
            }
            finally
            {
                _InRangeAdd = false;
            }

            InvalidateLayout();
        }

        #endregion

        #region AddSortToList

        private void AddSortToList(GridColumn column)
        {
            NeedsSorted = true;

            if (_SortColumns.Contains(column) == false)
                _SortColumns.Add(column);

            if (_InRangeAdd == false)
                InvalidateLayout();
        }

        #endregion

        #endregion

        #region SetSort

        #region SetSort()

        ///<summary>
        /// Sets the current list of sorting columns to empty.
        ///</summary>
        public void SetSort()
        {
            if (IsCurrentSort(null, SortDirection.Ascending) == false)
                ClearSort();
        }

        #endregion

        #region SetSort(col)

        ///<summary>
        /// Sets a single column to use for sorting the grid data.
        ///</summary>
        public void SetSort(GridColumn column)
        {
            if (IsCurrentSort(column, SortDirection.Ascending) == false)
            {
                ClearSort(false);

                AddSort(column);
            }
        }

        #endregion

        #region SetSort(col, dir)

        ///<summary>
        /// Sets a single column, and its associated sort
        /// direction (ascending or descending) to use for sorting the grid data.
        ///</summary>
        public void SetSort(GridColumn column, SortDirection sortDirection)
        {
            if (IsCurrentSort(column, sortDirection) == false)
            {
                ClearSort(false);

                AddSort(column, sortDirection);
            }
        }

        #endregion

        #region IsCurrentSort

        private bool IsCurrentSort(GridColumn column, SortDirection sortDirection)
        {
            if (column == null)
                return (_SortColumns.Count == 0);

            if (_SortColumns.Count == 1 && _SortColumns.Contains(column) == true)
                return (column.SortDirection == sortDirection);

            return (false);
        }

        #endregion

        #region SetSort(col[])

        ///<summary>
        /// Sets an array of columns to use for sorting the grid data.
        ///</summary>
        public void SetSort(GridColumn[] columns)
        {
            ClearSort(false);

            AddSort(columns);
        }

        #endregion

        #region SetSort(col[], dir)

        ///<summary>
        /// Sets an array of columns, and an associated sort
        /// direction (ascending or descending) to use for sorting the grid data.
        ///</summary>
        public void SetSort(GridColumn[] columns, SortDirection sortDirection)
        {
            ClearSort(false);

            AddSort(columns, sortDirection);
        }

        #endregion

        #region SetSort(col[], dir[])

        ///<summary>
        /// Sets an array of columns, and their respective sort
        /// direction (ascending or descending) to use for sorting the grid data.
        ///</summary>
        public void SetSort(GridColumn[] columns, SortDirection[] sortDirection)
        {
            ClearSort(false);

            AddSort(columns, sortDirection);
        }

        #endregion

        #endregion

        #region ClearSort

        ///<summary>
        /// Clears the currently set column sorting list.
        ///</summary>
        public void ClearSort()
        {
            ClearSort(true);
        }

        internal void ClearSort(bool doSort)
        {
            GridColumnCollection columns = _Columns;

            foreach (GridColumn column in columns)
                column.SortDirection = SortDirection.None;

            _SortColumns.Clear();

            InvalidateRender(); 
            
            if (doSort == true)
                InvalidateLayout();
        }

        #endregion

        #region SortItems

        internal void SortItems()
        {
            if (_SortColumns != null && _SortColumns.Count > 0 && Rows.Count > 0)
            {
                if (SuperGrid.DoRowsSortingEvent(this) == false)
                {
                    try
                    {
                        _IsSorting = true;

                        SortItems(this, _SortLevel);

                        if (UseAlternateRowStyle == true)
                            SuperGrid.UpdateStyleCount();

                        InvalidateLayout();
                    }
                    finally
                    {
                        _IsSorting = false;
                    }

                    SuperGrid.DoRowsSortedEvent(this);
                }
            }

            NeedsSorted = false;
        }

        internal void SortItems(GridContainer container, SortLevel sortLevel)
        {
            GridItemsCollection items = container.Rows;

            bool isGroup = (items[0] is GridGroup);

            if (items.Count > 0)
            {
                if (isGroup == true ||
                    (sortLevel & SortLevel.Expanded) == SortLevel.Expanded)
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (items[i] is GridRow || items[i] is GridGroup)
                        {
                            GridContainer row = items[i] as GridContainer;

                            if (row != null && row.Rows.Count > 0)
                                SortItems(row, (isGroup ? sortLevel : sortLevel | SortLevel.Root));
                        }
                    }
                }

                if (isGroup == false)
                {
                    if ((sortLevel & SortLevel.Root) == SortLevel.Root)
                    {
                        if (_SortColumns.Count > 1 ||
                            _GroupColumns.Contains(_SortColumns[0]) == false)
                        {
                            int n = GetGroupSortCount(items);

                            if (_ShowInsertRow == true)
                                n--;

                            if (n > 1)
                            {
                                if (HasDeletedRows == false)
                                    SortItemsWithOutDeleted(items, n);
                                else
                                    SortItemsWithDeleted(items, n);
                            }
                        }
                    }
                }
            }

            container.NeedsSorted = false;
        }

        #region SortItemsWithOutDeleted

        private void SortItemsWithOutDeleted(GridItemsCollection items, int n)
        {
            GridElement[] array = new GridElement[n];

            for (int i = 0; i < n; i++)
                array[i] = items[i];

            Array.Sort(array, 0, n, new RowComparer(_SortColumns));

            for (int i = 0; i < n; i++)
                items[i] = array[i];
        }

        #endregion

        #region SortItemsWithDeleted

        private void SortItemsWithDeleted(GridItemsCollection items, int n)
        {
            GridElement[] array = new GridElement[n];
            bool[] darray = new bool[n];

            for (int i = 0; i < n; i++)
            {
                array[i] = items[i];

                GridRow row = items[i] as GridRow;

                if (row != null)
                    darray[i] = row.IsDeleted;
            }

            Array.Sort(array, darray, 0, n, new RowComparer(_SortColumns));

            for (int i = 0; i < n; i++)
                items[i] = array[i];

            UnDeleteAll();

            for (int i = 0; i < n; i++)
            {
                if (darray[i] == true)
                    SetDeletedRows(i, 1, true);
            }
        }

        #endregion

        #region RowComparer

        private class RowComparer : IComparer<GridElement>
        {
            private List<GridColumn> _SortColumns;

            public RowComparer(List<GridColumn> sortColumns)
            {
                _SortColumns = sortColumns;
            }

            public int Compare(GridElement x, GridElement y)
            {
                GridRow rowx = x as GridRow;
                GridRow rowy = y as GridRow;

                if (rowx == null || rowy == null)
                {
                    if (x is GridTextRow && y is GridTextRow)
                        return ((GridTextRow)x).Text.CompareTo(((GridTextRow)y).Text);

                    if (x is GridTextRow || x is GridGroup)
                        return (1);

                    return (0);
                }

                foreach (GridColumn column in _SortColumns)
                {
                    int index = column.ColumnIndex;

                    int sval;

                    if (index >= rowx.Cells.Count)
                        sval = (index >= rowy.Cells.Count ? 0 : -1);

                    else if (index >= rowy.Cells.Count)
                        sval = 1;

                    else
                        sval = rowx.Cells[index].CompareTo(rowy.Cells[index]);

                    if (sval != 0)
                    {
                        return (column.SortDirection ==
                                SortDirection.Ascending) ? sval : -sval;
                    }
                }

                return (0);
            }
        }

        #endregion

        #endregion

        #region ToggleMultiColumnSort

        ///<summary>
        /// This routine toggles the current sort direction for the current
        /// list of sort columns. 
        ///</summary>
        public void ToggleMultiColumnSort()
        {
            if (_SortColumns.Count > 0)
            {
                foreach (GridColumn column in _SortColumns)
                {
                    SortDirection sortDirection;

                    switch (column.SortDirection)
                    {
                        case SortDirection.None:
                        case SortDirection.Descending:
                            sortDirection = SortDirection.Ascending;
                            break;

                        default:
                            sortDirection = SortDirection.Descending;
                            break;
                    }

                    AddSort(column, sortDirection);
                }

                SuperGrid.DoSortChangedEvent(this);
            }
        }

        #endregion

        #endregion

        #region Group support

        #region AddGroup

        #region AddGroup(col)

        ///<summary>
        /// This routine is used to add a single column to the
        /// list of columns used to group grid data rows together.
        ///</summary>
        public void AddGroup(GridColumn column)
        {
            AddGroup(column, SortDirection.Ascending);
        }

        #endregion

        #region AddGroup(col, dir)

        ///<summary>
        /// This routine is used to add a single column, and
        /// its associated sorting direction, to the list of
        /// columns used to group grid data rows together.
        ///</summary>
        public void AddGroup(GridColumn column, SortDirection groupDirection)
        {
            column.GroupDirection = groupDirection;

            AddGroupToList(column);
        }

        #endregion

        #region AddGroup(col[])

        ///<summary>
        /// This routine is used to add an array of column to the
        /// list of columns used to group grid data rows together.
        ///</summary>
        public void AddGroup(GridColumn[] columns)
        {
            AddGroup(columns, SortDirection.Ascending);
        }

        #endregion

        #region AddGroup(col[], dir)

        ///<summary>
        /// This routine is used to add an array of columns and
        /// a specified sort direction to the list of columns
        /// used to group grid data rows together.
        ///</summary>
        public void AddGroup(
            GridColumn[] columns, SortDirection groupDirection)
        {
            try
            {
                _InRangeAdd = true;

                foreach (GridColumn column in columns)
                    AddGroup(column, groupDirection);
            }
            finally
            {
                _InRangeAdd = false;
            }

            InvalidateLayout();
        }

        #endregion

        #region AddGroup(col[], dir[])

        ///<summary>
        /// This routine is used to add an array of columns, and
        /// their respective sort directions, to the list of columns
        /// used to group grid data rows together.
        ///</summary>
        public void AddGroup(
            GridColumn[] columns, SortDirection[] groupDirection)
        {
            try
            {
                _InRangeAdd = true;

                for (int i = 0; i < columns.Length; i++)
                    AddGroup(columns[i], groupDirection[i]);
            }
            finally
            {
                _InRangeAdd = false;
            }

            InvalidateLayout();
        }

        #endregion

        #region AddGroupToList

        private void AddGroupToList(GridColumn column)
        {
            NeedsGrouped = true;

            if (_GroupColumns.Contains(column) == false)
                _GroupColumns.Add(column);

            if (_InRangeAdd == false)
                InvalidateLayout();
        }

        #endregion

        #endregion

        #region InsertGroup(col)

        ///<summary>
        /// This routine is used to insert a single column to the
        /// list of columns used to group grid data rows together.
        ///</summary>
        public void InsertGroup(GridColumn column, int index)
        {
            InsertGroup(column, index, SortDirection.Ascending);
        }

        #endregion

        #region InsertGroup(col, dir)

        ///<summary>
        /// This routine is used to insert a single column, and
        /// its associated sorting direction, to the list of
        /// columns used to group grid data rows together.
        ///</summary>
        public void InsertGroup(
            GridColumn column, int index, SortDirection groupDirection)
        {
            column.GroupDirection = groupDirection;

            InsertGroupToList(column, index);
        }

        #endregion

        #region InsertGroupToList

        private void InsertGroupToList(GridColumn column, int index)
        {
            NeedsGrouped = true;

            int n = _GroupColumns.IndexOf(column);

            if (n == index)
                return;

            if (n >= 0)
            {
                _GroupColumns.RemoveAt(n);

                if (n < index)
                    index--;
            }

            _GroupColumns.Insert(index, column);

            InvalidateLayout();
        }

        #endregion

        #region SetGroup

        #region SetGroup()

        ///<summary>
        /// Resets the Group list to its default empty state.
        ///</summary>
        public void SetGroup()
        {
            ClearGroup();
        }

        #endregion

        #region SetGroup(col)

        ///<summary>
        /// Sets the Group list to the given Column.
        ///</summary>
        public void SetGroup(GridColumn column)
        {
            ClearGroup();

            AddGroup(column, SortDirection.Ascending);
        }

        #endregion

        #region SetGroup(col, dir)

        ///<summary>
        /// Sets the Group list to the given Column and sort direction.
        ///</summary>
        public void SetGroup(
            GridColumn column, SortDirection groupDirection)
        {
            ClearGroup();

            AddGroup(column, groupDirection);
        }

        #endregion

        #region SetGroup(col[])

        ///<summary>
        /// Sets the Group list to the given Column list.
        ///</summary>
        public void SetGroup(GridColumn[] columns)
        {
            ClearGroup();

            AddGroup(columns);
        }

        #endregion

        #region SetGroup(col[], dir)

        ///<summary>
        /// Sets the Group list to the
        /// given Column list and sort direction.
        ///</summary>
        public void SetGroup(
            GridColumn[] columns, SortDirection groupDirection)
        {
            ClearGroup();

            AddGroup(columns, groupDirection);
        }

        #endregion

        #region SetGroup(col[], dir[])

        ///<summary>
        /// Sets the Group list to the
        /// given Column list and sort direction.
        ///</summary>
        public void SetGroup(
            GridColumn[] columns, SortDirection[] groupDirection)
        {
            ClearSort(false);

            AddGroup(columns, groupDirection);
        }

        #endregion

        #endregion

        #region ClearGroup

        ///<summary>
        /// Clears all currently set Column Groups.
        ///</summary>
        public void ClearGroup()
        {
            if (GroupColumns.Count > 0)
            {
                NeedsGrouped = true;

                GridColumnCollection columns = _Columns;

                foreach (GridColumn column in columns)
                    column.GroupDirection = SortDirection.None;

                _GroupColumns.Clear();

                InvalidateLayout();
            }
        }

        #endregion

        #region RemoveGroup

        ///<summary>
        /// Removes an individual set Column Group.
        ///</summary>
        public void RemoveGroup(GridColumn column)
        {
            if (_GroupColumns.Contains(column) == true)
            {
                NeedsGrouped = true;

                column.GroupDirection = SortDirection.None;

                _GroupColumns.Remove(column);

                InvalidateLayout();
            }
        }

        #endregion

        #region GroupItems

        #region GroupItems (main)

        private bool GroupItems()
        {
            bool changed;

            _IsSorting = true;

            try
            {
                changed = RemoveGroupRows();
                changed |= AddGroupRows();
            }
            finally
            {
                _IsSorting = false;
            }

            NeedsGrouped = false;

            return (changed);
        }

        #region AddGroupRows

        private bool AddGroupRows()
        {
            if (_GroupColumns != null && _GroupColumns.Count > 0)
            {
                if (_ShowInsertRow == true)
                    ShowInsertRow = false;

                GroupItems(Rows, 0);

                NeedsGroupSorted = true;

                if (_AutoExpandSetGroup == true)
                    ExpandAll();

                return (true);
            }

            return (false);
        }

        #endregion

        #region RemoveGroupRows

        private bool RemoveGroupRows()
        {
            if (Rows.Count > 0 && Rows[0] is GridGroup)
            {
                List<GridElement> list = new List<GridElement>();

                RemoveGroups(Rows, list);
                Rows.Clear();

                foreach (GridElement item in list)
                {
                    item.Parent = null;

                    if ((item is GridRow) &&
                        ((GridRow) item).IsDetailRow == true)
                    {
                        continue;
                    }

                    Rows.Add(item);
                }

                return (true);
            }

            return (false);
        }

        #endregion

        #endregion

        #region GroupItems (sub)

        private void GroupItems(ICollection<GridElement> items, int index)
        {
            if (index < _GroupColumns.Count)
            {
                Hashtable groupTable = new Hashtable();
                List<GridGroup> groupOrder = new List<GridGroup>();

                ProcessGroup(items, _GroupColumns[index], groupTable, groupOrder);

                IGridCellEditControl editor = _GroupColumns[index].RenderControl;

                if (editor != null)
                {
                    ComboBox cbx = editor as ComboBox;

                    if (cbx == null || string.IsNullOrEmpty(cbx.DisplayMember))
                        editor = null;
                }

                try
                {
                    if (editor != null)
                        editor.SuspendUpdate = true;

                    foreach (GridGroup group in groupOrder)
                    {
                        if (editor != null)
                        {
                            editor.InitializeContext(group.Cell, null);

                            group.Text = editor.EditorFormattedValue;
                        }

                        SuperGrid.DoColumnGroupedEvent(this, _GroupColumns[index], group);

                        GroupItems(group.Rows, index + 1);

                        GetGroupDetailRows(index, group);
                    }
                }
                finally
                {
                    if (editor != null)
                        editor.SuspendUpdate = false;
                }
            }
        }

        #region GetGroupDetailRows

        private void GetGroupDetailRows(int index, GridGroup group)
        {
            List<GridRow> preRows;
            List<GridRow> postRows;

            if (SuperGrid.DoGetGroupedDetailRowsEvent(this,
                _GroupColumns[index], group, out preRows, out postRows) == true)
            {
                if (preRows != null)
                {
                    for (int i = preRows.Count - 1; i >= 0; i--)
                    {
                        preRows[i].IsDetailRow = true;

                        group.Rows.Insert(0, preRows[i]);
                    }
                }

                if (postRows != null)
                {
                    for (int i = 0; i < postRows.Count;  i++)
                    {
                        postRows[i].IsDetailRow = true;

                        group.Rows.Add(postRows[i]);
                    }
                }
            }
        }

        #endregion

        #endregion

        #region GroupSort

        internal bool GroupSort()
        {
            bool changed;

            try
            {
                _IsSorting = true;

                changed = GroupSort(this);
            }
            finally
            {
                _IsSorting = false;
            }

            return (changed);
        }

        internal bool GroupSort(GridContainer container)
        {
            bool changed = false;

            GridItemsCollection items = container.Rows;

            if (items.Count > 0)
            {
                int groupRowIndex = -1;

                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i] is GridGroup)
                    {
                        groupRowIndex = i;

                        GridContainer row = items[i] as GridContainer;

                        if (row != null && row.Rows.Count > 0)
                            changed |= GroupSort(row);
                    }
                }

                if (groupRowIndex >= 0)
                {
                    if (items.Count > 1)
                    {
                        GridColumn column = ((GridGroup)items[groupRowIndex]).Cell.GridColumn;

                        if (column != null &&
                            column.GroupDirection != SortDirection.None)
                        {
                            int n = GetGroupSortCount(items);

                            GridElement[] array = new GridElement[items.Count];

                            items.CopyTo(array, 0);
                            Array.Sort(array, 0, n, new GroupComparer(column.GroupDirection));

                            for (int i = 0; i < n; i++)
                                items[i] = array[i];

                            changed = true;
                        }
                    }
                }
            }

            NeedsGroupSorted = false;

            return (changed);
        }

        #region GetGroupSortCount

        private int GetGroupSortCount(GridItemsCollection items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                GridRow row = items[i] as GridRow;

                if (row != null && row.IsDetailRow == true)
                    return (i);
            }

            return (items.Count);
        }

        #endregion

        #region GroupComparer

        private class GroupComparer : IComparer<GridElement>
        {
            #region Private variables

            private SortDirection _GroupDirection;

            #endregion

            public GroupComparer(SortDirection groupDirection)
            {
                _GroupDirection = groupDirection;
            }

            #region Compare

            public int Compare(GridElement x, GridElement y)
            {
                GridGroup rowx = x as GridGroup;
                GridGroup rowy = y as GridGroup;

                if (rowx == null || rowy == null)
                    return (-1);

                int sval = rowx.Cell.CompareTo(rowy.Cell);

                return (_GroupDirection == 
                    SortDirection.Ascending) ? sval : -sval;
            }

            #endregion
        }

        #endregion

        #endregion

        #region ProcessGroup

        private void ProcessGroup(ICollection<GridElement> items,
            GridColumn gridColumn, Hashtable groupTable, List<GridGroup> groupOrder)
        {
            GridCell cell = null;

            foreach (GridElement item in items)
            {
                GridRow row = item as GridRow;

                if (row != null)
                {
                    if (gridColumn.ColumnIndex < row.Cells.Count)
                    {
                        cell = row.Cells[gridColumn.ColumnIndex];

                        AddItemToGroup(groupTable, groupOrder, cell, row);
                    }
                }
                else
                {
                    GridGroup group = item as GridGroup;

                    if (group != null)
                        ProcessGroup(group.Rows, gridColumn, groupTable, groupOrder);
                    else
                        AddItemToGroup(groupTable, groupOrder, cell, item);
                }
            }

            items.Clear();

            foreach (GridGroup group in groupOrder)
            {
                group.Parent = null;

                items.Add(group);

                bool hasVis = false;

                foreach (GridElement item in group.Rows)
                {
                    if (item.Visible == true)
                    {
                        hasVis = true;
                        break;
                    }
                }

                if (hasVis == false)
                    group.Visible = false;
            }
        }

        #endregion

        #region AddItemToGroup

        private void AddItemToGroup(IDictionary groupTable,
            List<GridGroup> groupOrder, GridCell cell, GridElement row)
        {
            object groupId = cell.Value ?? "<null>";

            SuperGrid.DoGetGroupIdEvent(row, cell.GridColumn, ref groupId);

            GridGroup group = groupTable[groupId] as GridGroup;

            if (group == null)
            {
                group = new GridGroup();

                group.Cell = cell;
                group.Text = groupId.ToString();

                groupTable[groupId] = group;
                groupOrder.Add(group);
            }

            group.Rows.Add(row);
        }

        #endregion

        #region RemoveGroups

        private void RemoveGroups(
            IEnumerable<GridElement> items, ICollection<GridElement> list)
        {
            foreach (GridElement item in items)
            {
                GridGroup group = item as GridGroup;

                if (group != null)
                    RemoveGroups(group.Rows, list);
                else
                    list.Add(item);
            }
        }

        #endregion

        #endregion

        #endregion

        #region Selection Support

        #region GetSelected

        #region GetSelectedElements

        ///<summary>
        /// Gets a SelectedElementCollection containing
        /// a list of currently selected rows, columns, and cells.
        ///</summary>
        public SelectedElementCollection GetSelectedElements()
        {
            SelectedElementCollection items =
                new SelectedElementCollection();

            GetSelectedElements(items);

            return (items);
        }

        internal void GetSelectedElements(SelectedElementCollection items)
        {
            GetSelectedRows(_SelectedRows, items);
            GetSelectedColumns(items);
            GetSelectedCells(items);
        }

        #endregion

        #region GetSelectedRows

        ///<summary>
        /// Gets a SelectedElementCollection
        /// containing a list of currently selected rows.
        ///</summary>
        public SelectedElementCollection GetSelectedRows()
        {
            SelectedElementCollection items =
                new SelectedElementCollection(SelectedRowCount);

            GetSelectedRows(_SelectedRows, items);

            return (items);
        }

        internal void GetSelectedRows(ICollection<GridElement> items)
        {
            GetSelectedRows(_SelectedRows, items);
        }

        internal void GetSelectedRows(SelectedElements se, ICollection<GridElement> items)
        {
            foreach (SelectedRange range in se.Ranges)
            {
                for (int i = range.StartIndex; i <= range.EndIndex; i++)
                {
                    GridContainer item = GetRowFromIndex(i);

                    if (item != null)
                        items.Add(item);
                }
            }
        }

        #endregion

        #region GetSelectedColumns

        ///<summary>
        /// Gets a SelectedElementCollection
        /// containing a list of currently selected columns.
        ///</summary>
        public SelectedElementCollection GetSelectedColumns()
        {
            SelectedElementCollection items =
                new SelectedElementCollection(SelectedColumnCount);

            GetSelectedColumns(items);

            return (items);
        }

        internal void GetSelectedColumns(ICollection<GridElement> items)
        {
            foreach (SelectedRange range in _SelectedColumns.Ranges)
            {
                for (int i = range.StartIndex; i <= range.EndIndex; i++)
                    items.Add(Columns[i]);
            }
        }

        #endregion

        #region GetSelectedCells

        ///<summary>
        /// Gets a SelectedElementCollection
        /// containing a list of currently selected cells.
        ///</summary>
        public SelectedElementCollection GetSelectedCells()
        {
            SelectedElementCollection items =
                new SelectedElementCollection(SelectedCellCount);

            GetSelectedCells(items);

            return (items);
        }

        internal void GetSelectedCells(SelectedElementCollection items)
        {
            foreach (GridColumn column in Columns)
                GetSelectedCells(items, column);
        }

        internal void GetSelectedCells(
            SelectedElementCollection items, GridColumn column)
        {
            foreach (SelectedRange range in column.SelectedCells.Ranges)
            {
                for (int i = range.StartIndex; i <= range.EndIndex; i++)
                {
                    GridRow row = GetRowFromIndex(i) as GridRow;

                    if (row != null)
                    {
                        GridCell cell = row.GetCell(column.ColumnIndex,
                            _AllowEmptyCellSelection);

                        if (cell != null && cell.AllowSelection == true)
                            items.Add(cell);
                    }
                }
            }
        }

        #endregion

        #endregion

        #region Select

        ///<summary>
        /// Selects the given Row.
        ///</summary>
        ///<param name="row"></param>
        public void Select(GridContainer row)
        {
            row.IsSelected = true;
        }

        ///<summary>
        /// Selects the given Column.
        ///</summary>
        ///<param name="column"></param>
        public void Select(GridColumn column)
        {
            column.IsSelected = true;
        }

        ///<summary>
        /// Selects the given Cell.
        ///</summary>
        ///<param name="cell"></param>
        public void Select(GridCell cell)
        {
            cell.IsSelected = true;
        }

        #endregion

        #region SelectAll

        ///<summary>
        /// Selects all cells, columns, or rows based upon
        /// the current TopLeftHeaderSelectBehavior property setting.
        ///</summary>
        public void SelectAll()
        {
            int count = (VirtualMode == true)
                ? VirtualRowCountEx : _NewGridIndex;

            if (count > 0)
            {
                TopLeftHeaderSelectBehavior tsb = _TopLeftHeaderSelectBehavior;

                if (_TopLeftHeaderSelectBehavior == TopLeftHeaderSelectBehavior.Deterministic)
                {
                    tsb = (_SelectionGranularity == SelectionGranularity.Cell)
                              ? TopLeftHeaderSelectBehavior.SelectAllCells
                              : TopLeftHeaderSelectBehavior.SelectAllRows;
                }

                ClearAll();

                switch (tsb)
                {
                    case TopLeftHeaderSelectBehavior.SelectAllCells:
                        SetSelectedCells(0, 0, count, Columns.Count, true);
                        break;

                    case TopLeftHeaderSelectBehavior.SelectAllColumns:
                        SetSelectedColumns(0, Columns.Count, true);
                        break;

                    case TopLeftHeaderSelectBehavior.SelectAllRows:
                        SetSelectedRows(0, count, true);
                        break;
                }
            }
            else
            {
                ColumnHeader.InvalidateRowHeader();
            }
        }

        #endregion

        #region SetSelected

        #region SetSelected (row)

        ///<summary>
        /// This routine is used to set an individual row as either
        /// selected or unselected, based upon the provided 'selected' value.
        ///</summary>
        ///<param name="row"></param>
        ///<param name="selected"></param>
        ///<returns></returns>
        public bool SetSelected(GridContainer row, bool selected)
        {
            int index = row.GridIndex;

            return (SetSelected(_SelectedRows,
                index, selected, ref _SelectionUpdateCount));
        }

        ///<summary>
        /// This routine is used to set a range of rows as either
        /// selected or unselected, based upon the provided 'selected' value.
        ///</summary>
        ///<param name="row"></param>
        ///<param name="count"></param>
        ///<param name="selected"></param>
        public void SetSelected(GridContainer row, int count, bool selected)
        {
            int index = row.GridIndex;

            SetSelected(_SelectedRows,
                index, count, selected, ref _SelectionUpdateCount);
        }

        ///<summary>
        /// This routine is used to set a range of rows as either
        /// selected or unselected, based upon the provided 'selected' value.
        ///</summary>
        ///<param name="startIndex"></param>
        ///<param name="count"></param>
        ///<param name="selected"></param>
        public void SetSelectedRows(int startIndex, int count, bool selected)
        {
            if (count > 0)
            {
                SetSelected(_SelectedRows,
                    startIndex, count, selected, ref _SelectionUpdateCount);

                InvalidateRows(this, startIndex, startIndex + count, false);
            }
        }

        #endregion

        #region SetSelected (column)

        ///<summary>
        /// This routine is used to set an individual column as either
        /// selected or unselected, based upon the provided 'selected' value.
        ///</summary>
        ///<param name="column"></param>
        ///<param name="selected"></param>
        ///<returns></returns>
        public bool SetSelected(GridColumn column, bool selected)
        {
            int index = column.ColumnIndex;

            return (SetSelected(_SelectedColumns,
                index, selected, ref _SelectionUpdateCount));
        }

        ///<summary>
        /// This routine is used to set a range of columns as either
        /// selected or unselected, based upon the provided 'selected' value.
        ///</summary>
        ///<param name="column"></param>
        ///<param name="count"></param>
        ///<param name="selected"></param>
        public void SetSelected(GridColumn column, int count, bool selected)
        {
            int index = column.ColumnIndex;

            SetSelected(_SelectedColumns,
                index, count, selected, ref _SelectionUpdateCount);
        }

        ///<summary>
        /// This routine sets a range of columns as selected
        /// or unselected, based upon the provided 'selected' value.
        ///</summary>
        ///<param name="startIndex"></param>
        ///<param name="count"></param>
        ///<param name="selected"></param>
        public void SetSelectedColumns(int startIndex, int count, bool selected)
        {
            if (count > 0)
            {
                SetSelected(_SelectedColumns,
                    startIndex, count, selected, ref _SelectionUpdateCount);
            }
        }

        #endregion

        #region SetSelected (cell)

        internal bool SetSelected(GridCell cell, bool selected)
        {
            GridColumn column = cell.GridColumn;
            int index = cell.GridRow.GridIndex;

            return (SetSelected(column.SelectedCells,
                index, selected, ref _SelectionUpdateCount));
        }

        ///<summary>
        /// This routine is used to set a range of cells as either
        /// selected or unselected, based upon the provided 'selected' value.
        ///</summary>
        ///<param name="startRowIndex"></param>
        ///<param name="startColumnName"></param>
        ///<param name="rowCount"></param>
        ///<param name="columnCount"></param>
        ///<param name="selected"></param>
        public void SetSelectedCells(int startRowIndex, string startColumnName,
            int rowCount, int columnCount, bool selected)
        {
            SetSelectedCells(startRowIndex,
                Columns[startColumnName].ColumnIndex, rowCount, columnCount, selected);
        }

        ///<summary>
        /// This routine is used to set a range of cells as either
        /// selected or unselected, based upon the provided 'selected' value.
        ///</summary>
        ///<param name="startRowIndex"></param>
        ///<param name="startColumnIndex"></param>
        ///<param name="rowCount"></param>
        ///<param name="columnCount"></param>
        ///<param name="selected"></param>
        public void SetSelectedCells(int startRowIndex, int startColumnIndex,
            int rowCount, int columnCount, bool selected)
        {
            if (rowCount > 0 && columnCount > 0)
            {
                for (int i = 0; i < columnCount; i++)
                {
                    SetSelected(Columns[startColumnIndex + i].SelectedCells,
                        startRowIndex, rowCount, selected, ref _SelectionUpdateCount);
                }
            }
        }

        #endregion

        #region SetSelected (single)

        internal bool SetSelected(
            SelectedElements selectedElements, int index, bool selected, ref int uCount)
        {
            SelectedRange range;
            bool found = selectedElements.FindRange(index, out range);

            if (selected == true)
            {
                if (found == false)
                {
                    selectedElements.AddItem(index);

                    uCount++;

                    if (EnableSelectionBuffering == false)
                        FlushSelected();

                    return (true);
                }
            }
            else
            {
                if (found == true)
                {
                    selectedElements.RemoveItem(index, range);

                    uCount++;

                    if (EnableSelectionBuffering == false)
                        FlushSelected();
                    
                    return (true);
                }
            }

            return (false);
        }

        #endregion

        #region SetSelected (multiple)

        internal void SetSelected(SelectedElements selectedElements,
            int index, int count, bool selected, ref int uCount)
        {
            if (count > 0)
            {
                if (selected == true)
                {
                    selectedElements.AddRange(index, index + count - 1);
                    uCount++;
                }
                else
                {
                    if (selectedElements.RemoveRange(index, index + count - 1) == true)
                        uCount++;
                }
            }
        }

        #endregion

        #endregion

        #region IsItemSelected

        internal bool IsItemSelected(
            SelectedElements selectedElements, int index)
        {
            SelectedRange range;

            return (selectedElements.FindRange(index, out range));
        }

        ///<summary>
        /// Retrieves whether the given item is selected.
        ///</summary>
        ///<param name="row"></param>
        ///<returns></returns>
        public bool IsItemSelected(GridContainer row)
        {
            if (row != null)
            {
                int index = row.GridIndex;

                return (IsItemSelected(_SelectedRows, index));
            }

            return (false);
        }

        ///<summary>
        /// Retrieves whether the given item is selected.
        ///</summary>
        ///<param name="column"></param>
        ///<returns></returns>
        public bool IsItemSelected(GridColumn column)
        {
            if (column != null)
            {
                int index = column.ColumnIndex;

                return (IsItemSelected(_SelectedColumns, index));
            }
            return (false);
        }

        ///<summary>
        /// Retrieves whether the given item is selected.
        ///</summary>
        ///<param name="cell"></param>
        ///<returns></returns>
        public bool IsItemSelected(GridCell cell)
        {
            if (cell != null)
            {
                GridColumn column = cell.GridColumn;
                int index = cell.GridRow.GridIndex;

                return (IsItemSelected(column.SelectedCells, index));
            }

            return (false);
        }

        #endregion

        #region Is(Row/Column/Cell)Selected

        ///<summary>
        /// IsRowSelected
        ///</summary>
        ///<param name="rowIndex"></param>
        ///<returns></returns>
        public bool IsRowSelected(int rowIndex)
        {
            return (IsItemSelected(_SelectedRows, rowIndex));
        }

        ///<summary>
        /// Retrieves whether the given column (as
        /// specified by the given column index) is selected.
        ///</summary>
        ///<param name="colIndex"></param>
        ///<returns></returns>
        public bool IsColumnSelected(int colIndex)
        {
            return (IsItemSelected(_SelectedColumns, colIndex));
        }

        ///<summary>
        /// Retrieves whether the given cell (as specified by the
        /// given row and column index) is selected.
        ///</summary>
        ///<param name="rowIndex"></param>
        ///<param name="colIndex"></param>
        ///<returns></returns>
        public bool IsCellSelected(int rowIndex, int colIndex)
        {
            SelectedElements selectedElements =
                Columns[colIndex].SelectedCells;

            return (IsItemSelected(selectedElements, rowIndex));
        }

        #endregion

        #region OnlyRowsSelected

        internal bool OnlyRowsSelected(int startIndex, int endIndex)
        {
            if (_SelectedRows.Ranges.Count != 1)
                return (false);

            if (_SelectedRows.Ranges[0].StartIndex != startIndex ||
                _SelectedRows.Ranges[0].EndIndex != endIndex)
            {
                return (false);
            }

            if (_SelectedColumns.Count > 0)
                return (false);

            foreach (GridColumn column in Columns)
            {
                if (column.SelectedCells.Count > 0)
                    return (false);
            }

            return (true);
        }

        #endregion

        #region OnlyColumnsSelected

        internal bool OnlyColumnsSelected(int startIndex, int endIndex)
        {
            if (_SelectedColumns.Ranges.Count != 1)
                return (false);

            if (_SelectedColumns.Ranges[0].StartIndex != startIndex ||
                _SelectedColumns.Ranges[0].EndIndex != endIndex)
            {
                return (false);
            }

            if (_SelectedRows.Count > 0)
                return (false);

            foreach (GridColumn column in Columns)
            {
                if (column.SelectedCells.Count > 0)
                    return (false);
            }

            return (true);
        }

        #endregion

        #region OnlyCellsSelected

        internal bool OnlyCellsSelected(int startRowIndex,
            int startColIndex, int endRowIndex, int endColIndex)
        {
            if (_SelectedRows.Ranges.Count > 0)
                return (false);

            if (_SelectedColumns.Ranges.Count > 0)
                return (false);

            foreach (GridColumn column in Columns)
            {
                if (column.ColumnIndex >= startColIndex && column.ColumnIndex <= endColIndex)
                {
                    if (column.SelectedCells.Ranges.Count != 1)
                        return (false);

                    if (column.SelectedCells.Ranges[0].StartIndex != startRowIndex ||
                        column.SelectedCells.Ranges[0].EndIndex != endRowIndex)
                    {
                        return (false);
                    }
                }
                else
                {
                    if (column.SelectedCells.Ranges.Count > 0)
                        return (false);
                }
            }

            return (true);
        }

        #endregion

        #region ClearAll

        ///<summary>
        /// Clears all cells, rows, and columns
        /// from their associated selection lists.
        ///</summary>
        public void ClearAll()
        {
            ClearAll(true);
        }

        internal void ClearAll(bool invalidate)
        {
            ClearSelected(_SelectedRows);
            ClearSelected(_SelectedColumns);

            foreach (GridColumn column in Columns)
                ClearSelected(column.SelectedCells);

            if (invalidate == true)
                InvalidateRender();
        }

        #endregion

        #region ClearAllSelected

        internal void ClearAllSelected()
        {
            Rectangle t = CViewRect;

            int[] map = Columns.DisplayIndexMap;

            for (int i = 0; i < map.Length; i++)
            {
                GridColumn col = Columns[map[i]];

                if (col.Visible && col.Bounds.IntersectsWith(t))
                {
                    if (col.IsSelected == true)
                    {
                        col.InvalidateRender();
                        ColumnHeader.InvalidateHeader(this, col);
                    }
                    else if (col.SelectedCells.Ranges.Count > 0)
                    {
                        col.InvalidateRender();
                    }
                }
            }

            int first = GetFirstOnScreenGridIndex();
            int last = GetLastOnScreenGridIndex(first);

            InvalidateSelectedRows(first, last, t);

            ClearAll(false);
        }

        #region InvalidateSelectedRows

        private void InvalidateSelectedRows(int startRow, int endRow, Rectangle t)
        {
            for (int i = startRow; i <= endRow; i++)
            {
                GridContainer row = GetRowFromIndex(i);

                if (row != null && row.Bounds.IntersectsWith(t))
                {
                    if (row.IsSelected == true)
                    {
                        if (row is GridPanel)
                            row.InvalidateRender(row.ContainerBounds);
                        else
                            row.InvalidateRender();
                    }
                }
            }
        }

        #endregion

        #region GetFirstOnScreenGridIndex

        private int GetFirstOnScreenGridIndex()
        {
            if ((VScrollOffset == 0 && IsFiltered == false) ||
                (Parent != null && IsVFrozen == true) ||
                (VirtualMode == false && IsSubPanel == false && FrozenRowCount > 0))
            {
                return (0);
            }

            if (_VirtualMode == true)
                return (GetFirstVirtualOnScreenGridIndex());

            return (GetFirstRealOnScreenGridIndex());
        }

        #region GetFirstVirtualOnScreenGridIndex

        private int GetFirstVirtualOnScreenGridIndex()
        {
            Rectangle r = BoundsRelative;

            r.Y += (FixedRowHeight -
                FrozenRowCount * VirtualRowHeight);

            int y = VScrollOffset;

            if (Parent == null)
                y += (FrozenRowCount * VirtualRowHeight);
            else
                y = (y - r.Y) + SuperGrid.PrimaryGrid.FixedRowHeight;

            int n = y / VirtualRowHeight;

            return (n > 0 ? n : 0);
        }

        #endregion

        #region GetFirstRealOnScreenGridIndex

        private int GetFirstRealOnScreenGridIndex()
        {
            if (_NewGridIndex <= 0)
                return (-1);
                
            int lo = 0;
            int hi = _NewGridIndex - 1;

            Rectangle r = SuperGrid.ViewRect;

            while (lo < hi)
            {
                int mid = (lo + hi)/2;

                GridElement item = _GridIndexArray[mid];
                Rectangle t = item is GridPanel ? ((GridPanel) item).ContainerBounds : item.BoundsRelative;

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

        #region GetLastOnScreenGridndex

        private int GetLastOnScreenGridIndex(int index)
        {
            if (_VirtualMode == true)
                return (GetLastVirtualOnScreenGridIndex());

            return (GetLastRealOnScreenGridIndex(index));
        }

        #region GetLastVirtualOnScreenGridIndex

        private int GetLastVirtualOnScreenGridIndex()
        {
            Rectangle r = BoundsRelative;

            int h = FixedRowHeight - (FrozenRowCount * VirtualRowHeight);
            r.Y += h;
            r.Height -= h;

            int y = VScrollOffset;

            if (Parent == null)
            {
                y += (FrozenRowCount * VirtualRowHeight);
                r.Height -= FrozenRowCount * VirtualRowHeight;
            }
            else
            {
                y = (y - r.Y) + SuperGrid.PrimaryGrid.FixedRowHeight;
            }

            int n = (y +  r.Height) / VirtualRowHeight;

            return (n > 0 ? n : 0);
        }

        #endregion

        #region GetLastRealOnScreenGridIndex

        private int GetLastRealOnScreenGridIndex(int index)
        {
            Rectangle r = SuperGrid.ViewRect;

            while (index + 1 < _NewGridIndex)
            {
                index++;

                GridContainer item = _GridIndexArray[index];
                Rectangle t = item.BoundsRelative;

                if (item.IsVFrozen == false)
                    t.Y -= VScrollOffset;

                if (t.Bottom >= r.Bottom)
                    return (index);
            }

            return (index);
        }

        #endregion

        #endregion

        #endregion

        #region ClearSelected(rows/columns/cells)

        ///<summary>
        /// Clears all currently selected Rows.
        ///</summary>
        public void ClearSelectedRows()
        {
            ClearSelected(_SelectedRows);

            InvalidateRender();
        }

        ///<summary>
        /// Clears all currently selected Columns.
        ///</summary>
        public void ClearSelectedColumns()
        {
            ClearSelected(_SelectedColumns);

            InvalidateRender();
        }

        ///<summary>
        /// Clears all currently selected Cells.
        ///</summary>
        public void ClearSelectedCells()
        {
            foreach (GridColumn column in Columns)
                ClearSelected(column.SelectedCells);

            InvalidateRender();
        }

        private void ClearSelected(SelectedElements selectedElements)
        {
            if (selectedElements.Count > 0)
            {
                selectedElements.Clear();

                UpdateSelectionCount();
            }
        }

        #endregion

        #region ExpandSelectedAtIndex

        internal void ExpandSelectedAtIndex(
            int index, int count, bool expand)
        {
            if (expand == false)
            {
                SetSelectedRows(index, count, false);
                SetSelectedCells(index, 0, count, Columns.Count, false);

                count = -count;
            }

            _SelectedRows.OffsetIndices(index, count);

            foreach (GridColumn column in Columns)
                column.SelectedCells.OffsetIndices(index, count);

            UpdateRowCountEx();
        }

        #endregion

        #region UpdateSelectionCount

        internal void UpdateSelectionCount()
        {
            _SelectionUpdateCount++;

            if (EnableSelectionBuffering == false)
                FlushSelected();
        }

        #endregion

        #region OnSelectionChanged

        internal void OnSelectionChanged()
        {
            _LastSelectionUpdateCount = _SelectionUpdateCount;

            SuperGrid.DoSelectionChangedEvent(this);
        }

        #endregion

        #region NormalizeIndices

        internal void NormalizeIndices(
            bool extend, int anchor, ref int startIndex, ref int endIndex)
        {
            if (startIndex > endIndex)
            {
                int tempIndex = startIndex;
                startIndex = endIndex;
                endIndex = tempIndex;

                if (extend == true)
                {
                    if (startIndex > anchor)
                        startIndex++;

                    else if (endIndex < anchor)
                        endIndex--;
                }
            }
            else
            {
                if (extend == true)
                {
                    if (endIndex < anchor)
                        endIndex--;

                    else if (startIndex > anchor)
                        startIndex++;
                }
            }
        }

        #endregion

        #region FlushSelected

        ///<summary>
        /// Flushes any pending Selections (raises the SelectionChanged event
        /// if selections have changed and the event has not been previously raised).
        ///</summary>
        ///<returns>true, if event raised.</returns>
        public bool FlushSelected()
        {
            if (_SelectionUpdateCount != _LastSelectionUpdateCount)
            {
                OnSelectionChanged();

                return (true);
            }

            return (false);
        }

        #endregion

        #endregion

        #region Delete row support

        #region GetDeletedRows

        ///<summary>
        /// Gets a SelectedElementCollection of the
        /// current list of Deleted Rows.
        ///</summary>
        public SelectedElementCollection GetDeletedRows()
        {
            SelectedElementCollection items =
                new SelectedElementCollection(DeletedRowCount);

            GetSelectedRows(_DeletedRows, items);

            return (items);
        }

        #endregion

        #region DeleteAll

        ///<summary>
        /// This routine marks each data rows as Deleted.
        ///</summary>
        public void DeleteAll()
        {
            if (VirtualMode == true)
            {
                if (VirtualRowCountEx > 0)
                {
                    ClearSelected(_DeletedRows);

                    SetSelected(_DeletedRows, 0,
                        VirtualRowCountEx, true, ref _DeleteUpdateCount);
                }
            }
            else
            {
                if (Rows.Count > 0)
                {
                    int count = ((GridContainer)Rows[Rows.Count - 1]).FullIndex;

                    ClearSelected(_DeletedRows);

                    SetSelected(_DeletedRows, 0,
                        count + 1, true, ref _SelectionUpdateCount);
                }
            }
        }

        #endregion

        #region UnDeleteAll

        ///<summary>
        /// Undeletes (or restores) all previously deleted grid rows.
        ///</summary>
        public void UnDeleteAll()
        {
            ClearSelected(_DeletedRows);

            UpdateDeleteCount();
        }

        #endregion

        #region SetDeleted

        ///<summary>
        /// Sets the deleted state for the given row.
        ///</summary>
        ///<param name="row"></param>
        ///<param name="deleted"></param>
        ///<returns></returns>
        public bool SetDeleted(GridContainer row, bool deleted)
        {
            int index = row.FullIndex;

            if (SetSelected(_DeletedRows, index, deleted, ref _DeleteUpdateCount) == true)
            {
                if (_VirtualMode == false)
                {
                    if (deleted == true && _AutoHideDeletedRows == true)
                    {
                        row.Visible = false;

                        _SelectActiveRow = true;
                    }
                    else
                    {
                        InvalidateRender();
                    }
                }
                else
                {
                    InvalidateRender();
                }

                return (true);
            }

            return (false);
        }

        ///<summary>
        /// Sets the deleted state, beginning at the given row, and
        /// continuing for the specified count.
        ///</summary>
        ///<param name="row"></param>
        ///<param name="count"></param>
        ///<param name="deleted"></param>
        public void SetDeleted(GridContainer row, int count, bool deleted)
        {
            SetDeletedRows(row.FullIndex, count, deleted);
        }

        ///<summary>
        /// Sets the deleted state, beginning at the given row
        /// index, and continuing for the specified count.
        ///</summary>
        ///<param name="startIndex"></param>
        ///<param name="count"></param>
        ///<param name="deleted"></param>
        public void SetDeletedRows(int startIndex, int count, bool deleted)
        {
            if (count > 0)
            {
                SetSelected(_DeletedRows,
                    startIndex, count, deleted, ref _DeleteUpdateCount);

                if (_VirtualMode == false)
                {
                    if (deleted == true && _AutoHideDeletedRows == true)
                    {
                        for (int i = 0; i < count; i++)
                            Rows[startIndex + i].Visible = false;

                        _SelectActiveRow = true;
                    }
                    else
                    {
                        for (int i = 0; i < count; i++)
                            Rows[startIndex + i].InvalidateRender();
                    }
                }
                else
                {
                    InvalidateRender();
                }
            }
        }

        #endregion

        #region IsRowDeleted

        ///<summary>
        /// IsRowDeleted
        ///</summary>
        ///<param name="row"></param>
        ///<returns></returns>
        public bool IsRowDeleted(GridContainer row)
        {
            int index = row.FullIndex;

            return (IsRowDeleted(index));
        }

        ///<summary>
        /// IsRowDeleted
        ///</summary>
        ///<param name="index"></param>
        ///<returns></returns>
        public bool IsRowDeleted(int index)
        {
            return (IsItemSelected(_DeletedRows, index));
        }

        #endregion

        #region UpdateDeleteCount

        internal void UpdateDeleteCount()
        {
            _DeleteUpdateCount++;
        }

        #endregion

        #region ExpandDeletedAtIndex

        internal void ExpandDeletedAtIndex(int index, int count)
        {
            _DeletedRows.OffsetIndices(index, count);

            UpdateRowCountEx();
        }

        #endregion

        #endregion

        #region InsertRow

        ///<summary>
        /// Inserts a new row at the given index.
        ///</summary>
        ///<param name="index"></param>
        ///<exception cref="Exception"></exception>
        public GridRow InsertRow(int index)
        {
            if (DeactivateEdit() == false)
                return (null);

            if (DeactivateRow(SuperGrid.ActiveRow, null) == false)
                return (null);
            
            if (SuperGrid.DoRowAddingEvent(this, ref index) == false)
            {
                GridRow row = new GridRow();

                for (int i = 0; i < Columns.Count; i++)
                {
                    GridCell cell = new
                        GridCell(Columns[i].DefaultNewRowCellValue);

                    row.Cells.Add(cell);
                }

                if (VirtualMode == true)
                    InsertVirtualTempRow(row, index);
                else
                    InsertRealTempRow(row, index);

                return (row);
            }

            return (null);
        }

        #region InsertVirtualTempRow

        private void InsertVirtualTempRow(GridRow row, int index)
        {
            if ((uint)index > VirtualRowCount)
                throw new Exception("Invalid VirtualRow collection index");

            row.RowIndex = index;

            row.IsTempInsertRow = true;
            row.RowNeedsStored = true;

            VirtualRows.MaxRowIndex = index - 1;
            VirtualRowCount++;

            _VirtualTempInsertRow = row;

            LatentActiveRowIndex = index;
            EnsureRowVisible = true;

            HotItem = null;
            ExpandDeletedAtIndex(row.FullIndex + 1, 1);
            PostInternalMouseMove();

            SuperGrid.DoRowSetDefaultValuesEvent(this, row, NewRowContext.RowInit);
            SuperGrid.DoRowAddedEvent(this, index);
            SuperGrid.PrimaryGrid.InvalidateLayout();
        }

        #endregion

        #region InsertRealTempRow

        private void InsertRealTempRow(GridRow row, int index)
        {
            if ((uint)index > Rows.Count)
                throw new Exception("Invalid Rows collection index");

            if (index == Rows.Count && ShowInsertRow == true)
                index--;

            row.IsTempInsertRow = true;
            row.RowNeedsStored = true;

            Rows.Insert(index, row);
            UpdateIndicees(this, Rows, true);

            LatentActiveRowIndex = index;
            EnsureRowVisible = true;

            HotItem = null;
            ExpandDeletedAtIndex(row.FullIndex + 1, 1);
            PostInternalMouseMove();

            SuperGrid.DoRowSetDefaultValuesEvent(this, row, NewRowContext.RowInit);
            SuperGrid.DoRowAddedEvent(this, index);
            SuperGrid.PrimaryGrid.InvalidateLayout();
        }

        #endregion

        #endregion

        #region InsertNewRow

        private bool InsertNewRow(GridRow row, int index)
        {
            while (true)
            {
                try
                {
                    DataBinder.UpdatePosInProgress = true;

                    DataBinder.InsertRow(index, row);
                    break;
                }
                catch (Exception exp)
                {
                    bool retry = false;
                    bool throwException = false;

                    if (SuperGrid.HasDataErrorHandler == true)
                    {
                        object value = row;

                        if (SuperGrid.DoDataErrorEvent(this, null, exp,
                            DataContext.InsertRow, ref value, ref throwException, ref retry) == true)
                        {
                            return (false);
                        }
                    }

                    if (throwException == true)
                        throw;

                    if (retry == false)
                        return (false);
                }
                finally
                {
                    DataBinder.UpdatePosInProgress = false;
                }
            }

            return (true);
        }

        #endregion

        #region GetRowFromIndex

        ///<summary>
        /// Gets the Row from the given GridIndex
        ///</summary>
        ///<param name="gridIndex"></param>
        ///<returns></returns>
        public GridContainer GetRowFromIndex(int gridIndex)
        {
            if (gridIndex >= 0)
            {
                if (VirtualMode == true)
                {
                    if (gridIndex >= VirtualRowCountEx)
                        return (null);

                    return (VirtualRows[gridIndex]);
                }

                if (gridIndex < _NewGridIndex && gridIndex < _GridIndexArray.Length)
                    return (_GridIndexArray[gridIndex]);
            }

            return (null);
        }

        #endregion

        #region UpdateRowCount

        internal void UpdateRowCount()
        {
            if (DataBinder.IsUpdateSuspended == false)
            {
                if (EnableFiltering == true && FilterLevel != FilterLevel.None)
                {
                    bool rowFiltered = DataFilter.IsRowFiltered(this);
                    bool colFiltered = DataFilter.IsColumnFiltered(this);

                    if (rowFiltered == true || colFiltered == true)
                        NeedToUpdateDataFilter = true;
                }

                UpdateRowCountEx();
            }
        }

        #endregion

        #region UpdateRowCountEx

        internal void UpdateRowCountEx()
        {
            if (SuperGrid != null)
            {
                SuperGrid.NeedToUpdateIndicees = true;
                SuperGrid.UpdateStyleCount();
            }
        }

        #endregion

        #region CreateItemsCollection

        /// <summary>
        /// Creates the GridItemsCollection that hosts the items.
        /// </summary>
        /// <returns>New instance of GridItemsCollection.</returns>
        protected override GridItemsCollection CreateItemsCollection()
        {
            GridItemsCollection items = new GridItemsCollection();

            items.CollectionChanged += ItemsCollectionChanged;

            return (items);
        }

        #endregion

        #region ItemsCollectionChanged

        void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_IsRowMoving == false)
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                    case NotifyCollectionChangedAction.Replace:
                        int n = 0;

                        foreach (GridElement item in e.NewItems)
                        {
                            if (item is GridPanel)
                                throw new Exception("GridPanels can not be nested.");

                            if (VirtualMode == true)
                                throw new Exception("Virtual Panels can not have nested rows.");

                            if (item.Parent != null && item.Parent != this)
                                throw new Exception("Row is already a member of another Rows collection.");

                            item.Parent = this;

                            if (_IsSorting == false)
                                item.NeedsMeasured = true;

                            GridContainer row = item as GridContainer;

                            if (row != null)
                            {
                                row.RowIndex = e.NewStartingIndex + n;

                                if (row is GridRow)
                                {
                                    if (e.Action == NotifyCollectionChangedAction.Replace)
                                        RemoveFromExpDictionary(row);

                                    AddToExpDictionary(row);
                                }
                            }

                            n++;
                        }
                        break;

                    case NotifyCollectionChangedAction.Reset:
                        ClearAll();
                        UnDeleteAll();

                        DetachNestedRows(false);

                        _ExpDictionary.Clear();
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        ClearAll();

                        if (_ExpDictionary.Count > 0)
                        {
                            foreach (GridContainer item in e.OldItems)
                                RemoveFromExpDictionary(item as GridRow);
                        }

                        foreach (GridContainer item in e.OldItems)
                        {
                            GridRow row = item as GridRow;

                            if (row != null)
                            {
                                if (row.DataItem != null)
                                {
                                    if (row.IsTempInsertRow == false)
                                        DataBinder.RemoveRow(row);

                                    row.DataItem = null;
                                }

                                if (row == ActiveRow)
                                    ActiveRow = null;

                                row.Dispose();
                            }
                        }
                        break;
                }

                if (_IsSorting == false)
                {
                    NeedsMeasured = true;
                    NeedsFilterScan = true;

                    if (GroupColumns.Count > 0)
                        NeedsGrouped = true;

                    UpdateRowCount();

                    InvalidateLayout();

                    if (SuperGrid != null && SuperGrid.NonModalEditorCell != null)
                    {
                        SuperGrid.DeactivateNonModalEditor();
                        SuperGrid.PostInternalMouseMove();
                    }
                }
            }
        }

        #endregion

        #region AddToExpDictionary

        internal void AddToExpDictionary(GridContainer cont)
        {
            if (EnableCellExpressions == true)
            {
                GridRow row = cont as GridRow;

                if (row != null)
                {
                    foreach (GridCell cell in row.Cells)
                    {
                        string newExp = cell.Value as string;

                        if (newExp != null)
                        {
                            newExp = newExp.Trim();

                            if (newExp.StartsWith("="))
                                cell.SetCellExp(this, newExp);
                        }
                    }
                }

                if (cont.Rows != null)
                {
                    foreach (GridContainer item in cont.Rows)
                        AddToExpDictionary(item);
                }
            }
        }

        #endregion

        #region RemoveFromExpDictionary

        internal void RemoveFromExpDictionary(GridContainer cont)
        {
            if (EnableCellExpressions == true)
            {
                GridRow row = cont as GridRow;

                if (row != null)
                {
                    foreach (GridCell cell in row.Cells)
                    {
                        if (cell.IsValueExpression == true)
                            _ExpDictionary.Remove(cell);
                    }
                }

                if (cont.Rows != null)
                {
                    foreach (GridContainer item in cont.Rows)
                        RemoveFromExpDictionary(item);
                }
            }
        }

        #endregion

        #region AddNewInsertRow

        internal void AddNewInsertRow()
        {
            if (_VirtualMode == true)
            {
                if (_VirtualRowCount > 0)
                {
                    _VirtualInsertRow = VirtualRows[_VirtualRowCount];

                    for (int i = 0; i < _Columns.Count; i++)
                        _VirtualInsertRow.Cells[i].ValueEx = _Columns[i].DefaultNewRowCellValue;

                    if (SuperGrid != null)
                        SuperGrid.DoRowSetDefaultValuesEvent(this, _VirtualInsertRow, NewRowContext.RowInit);

                    _VirtualInsertRow.IsTempInsertRow = true;
                }

                _VirtualRowCount++;
            }
            else
            {
                GridItemsCollection items = Rows;

                GridRow row = new GridRow();

                for (int i = 0; i < _Columns.Count; i++)
                    row.Cells.Add(new GridCell(_Columns[i].DefaultNewRowCellValue));

                row.Parent = this;
                row.IsTempInsertRow = true;

                if (SuperGrid != null)
                    SuperGrid.DoRowSetDefaultValuesEvent(this, row, NewRowContext.RowInit);

                items.FloatLastItem = false;
                items.Add(row);
                items.FloatLastItem = true;

                if (SuperGrid == null)
                    _LoadInsertRow = true;
            }
        }

        #endregion

        #region RemoveNewInsertRow

        internal void RemoveNewInsertRow()
        {
            if (_VirtualMode == true)
            {
                _VirtualRowCount--;

                _VirtualInsertRow = null;

                VirtualRows.FreeRow(_VirtualRowCount);
            }
            else
            {
                GridItemsCollection items = Rows;

                if (items.Count == 0 || items.FloatLastItem == false)
                    throw new Exception();

                GridRow row = items[items.Count - 1] as GridRow;

                if (row == null)
                    throw new Exception();

                if (_ActiveRow == row)
                    SetActiveRow(row.PrevVisibleRow, true);

                items.Remove(row);

                items.FloatLastItem = false;
            }
        }

        #endregion

        #region GetAutoSizeMode

        private ColumnAutoSizeMode GetAutoSizeMode(GridColumn column)
        {
            return (column.AutoSizeMode == ColumnAutoSizeMode.NotSet)
                ? _ColumnAutoSizeMode : column.AutoSizeMode;
        }

        #endregion

        #region PointToScroll

        internal Point PointToScroll(Point pt)
        {
            if (IsSubPanel == true)
                pt.X -= HScrollOffset;

            if (IsVFrozen == false)
                pt.Y -= VScrollOffset;

            return (pt);
        }

        #endregion

        #region PointToGrid

        ///<summary>
        /// Converts the given Point
        /// into a Grid relative Point.
        ///</summary>
        public Point PointToGrid(Point pt)
        {
            if (IsSubPanel == true)
            {
                pt.X += HScrollOffset;
                pt.Y += VScrollOffset;
            }
            else
            {
                if (FrozenColumnCount == 0)
                {
                    Rectangle t = SuperGrid.SViewRect;

                    if (t.Contains(pt) == true)
                    {
                        pt.X += HScrollOffset;
                        pt.Y += VScrollOffset;
                    }
                }
                else
                {
                    GridElement item = GetElementAt(pt.X, pt.Y);

                    if (item is GridColumnHeader)
                        pt = PointByColumn(pt);

                    else if (item is GridFooter)
                        pt.Y += VScrollOffset;

                    else if (item is GridRow)
                    {
                        GridElement sub = ((GridRow)item).GetElementAt(pt.X, pt.Y);

                        if (item.IsVFrozen == false)
                            pt.Y += VScrollOffset;

                        if (sub is GridPanel == false)
                            pt = PointByColumn(pt);
                        else
                            pt.X += HScrollOffset;
                    }
                }
            }

            return (pt);
        }

        #endregion

        #region PointByColumn

        private Point PointByColumn(Point pt)
        {
            int[] map = Columns.DisplayIndexMap;

            foreach (int index in map)
            {
                GridColumn column = _Columns[index];

                if (column.Visible == true)
                {
                    int x = column.BoundsRelative.X;

                    if (column.IsHFrozen == false && pt.X > x)
                    {
                        pt.X += HScrollOffset;
                        break;
                    }
                }
            }

            return (pt);
        }

        #endregion

        #region CopySelectedCellsToClipboard

        ///<summary>
        /// Copies the currently selected list of tab delimited cells to the
        /// system ClipBoard.  This routine takes all cell, column, and row
        /// selections into account when determining what cell data to write
        /// to the clipboard. 
        ///</summary>
        ///<param name="includeColumnHeaders">If true, the column header text is written.</param>
        ///<param name="includeGridIndex">If true, the row GridIndex is written.</param>
        public void CopySelectedCellsToClipboard(
            bool includeColumnHeaders, bool includeGridIndex)
        {
            CopySelectedCellsToClipboard(
                includeColumnHeaders, includeGridIndex, '\t', true); 
        }

        ///<summary>
        /// Copies the currently selected list of tab delimited cells to the system
        /// ClipBoard.  This routine takes all cell, column, and row selections into
        /// account when determining what cell data to write to the clipboard. 
        ///</summary>
        ///<param name="includeColumnHeaders">If true, the column header text is written.</param>
        ///<param name="includeGridIndex">If true, the row GridIndex is written.</param>
        ///<param name="delimiter">Field delimiter</param>
        public void CopySelectedCellsToClipboard(
            bool includeColumnHeaders, bool includeGridIndex, char delimiter)
        {
            CopySelectedCellsToClipboard(
                includeColumnHeaders, includeGridIndex, '\t', true);
        }

        ///<summary>
        /// Copies the currently selected list of tab delimited cells to the system
        /// ClipBoard.  This routine takes all cell, column, and row selections into
        /// account when determining what cell data to write to the clipboard. 
        ///</summary>
        ///<param name="includeColumnHeaders">If true, the column header text is written.</param>
        ///<param name="includeGridIndex">If true, the row GridIndex is written.</param>
        ///<param name="delimiter">Field delimiter</param>
        ///<param name="fullCopy">Whether to copy all leading columns</param>
        public void CopySelectedCellsToClipboard(
            bool includeColumnHeaders, bool includeGridIndex, char delimiter, bool fullCopy)
        {
            SelectedElementCollection items = GetSelectedElements();
            List<GridCell> cells = items.GetCells();

            if (cells.Count > 0)
            {
                cells.Sort(new CellComparer());

                BitArray usedColumns = GetUsedColumns(cells);

                if (usedColumns.Count > 0)
                {
                    int fIndex = (fullCopy == true)
                        ? 0 : GetFirstCellIndex(usedColumns);

                    StringBuilder sb = new StringBuilder();

                    if (includeColumnHeaders == true)
                    {
                        foreach (GridColumn column in _Columns)
                        {
                            if (fullCopy == true || usedColumns[column.ColumnIndex] == true)
                            {
                                if (column.ColumnIndex >= fIndex)
                                {
                                    sb.Append(column.GetHeaderText());
                                    sb.Append(delimiter);
                                }
                            }
                        }

                        if (sb.Length > 0)
                        {
                            sb.Length--;
                            sb.AppendLine();
                        }
                    }

                    int cIndex = 0;
                    int rIndex = -1;

                    foreach (GridCell cell in cells)
                    {
                        if (fullCopy == true || usedColumns[cell.ColumnIndex] == true)
                        {
                            int rowIndex = cell.GridRow.GridIndex;

                            if (rowIndex != rIndex)
                            {
                                if (rIndex >= 0)
                                    sb.AppendLine();

                                rIndex = rowIndex;
                                cIndex = fIndex;

                                if (includeGridIndex == true)
                                {
                                    sb.Append(rowIndex);
                                    sb.Append(delimiter);
                                }
                            }

                            while (cIndex < cell.ColumnIndex)
                            {
                                cIndex++;

                                if (fullCopy == true || usedColumns[cIndex] == true)
                                    sb.Append(delimiter);
                            }

                            sb.Append(cell.Value ?? NullString ?? "<null>");
                        }
                        else
                        {
                            cIndex = cell.ColumnIndex;
                        }
                    }

                    sb.AppendLine();

                    DataObject mData = new DataObject();
                    mData.SetData(DataFormats.Text, true, sb.ToString());

                    Clipboard.SetDataObject(mData, true);
                }
            }
        }

        #region CellComparer

        private class CellComparer : IComparer<GridCell>
        {
            public int Compare(GridCell x, GridCell y)
            {
                int xcol = x.ColumnIndex;
                int xrow = x.GridRow.GridIndex;

                int ycol = y.ColumnIndex;
                int yrow = y.GridRow.GridIndex;

                if (xrow < yrow)
                    return (-1);

                if (xrow > yrow)
                    return (1);

                if (xcol < ycol)
                    return (-1);

                if (xcol > ycol)
                    return (1);

                return (0);
            }
        }

        #endregion

        #region GetUsedColumns

        private BitArray GetUsedColumns(IEnumerable<GridCell> cells)
        {
            int visColumns = 0;

            foreach (GridColumn column in Columns)
            {
                if (column.Visible == true)
                    visColumns++;
            }

            BitArray usedCells = new BitArray(Columns.Count);

            if (visColumns > 0)
            {
                foreach (GridCell cell in cells)
                {
                    if (cell.GridColumn.Visible == true)
                    {
                        usedCells[cell.ColumnIndex] = true;

                        if (--visColumns <= 0)
                            break;
                    }
                }
            }

            return (usedCells);
        }

        #endregion

        #region GetFirstCellIndex

        private int GetFirstCellIndex(BitArray usedColumns)
        {
            for (int i = 0; i < usedColumns.Count; i++)
            {
                if (usedColumns[i] == true)
                    return (i);
            }

            return (0);
        }

        #endregion

        #endregion

        #region DataUpdate support

        ///<summary>
        /// Calling the BeginDataUpdate routine informs the grid
        /// that an extended data update phase has begun. The SuperGrid
        /// will suspend all data notification and updates
        /// until the corresponding EndDataUpdate routine is called.
        /// 
        /// BeginDataUpdate / EndDataUpdate can be nested and must be
        /// called in pairs – every BeginDataUpdate must have a
        /// matching EndDataUpdate call.
        ///</summary>
        public void BeginDataUpdate()
        {
            DataBinder.BeginUpdate();
        }

        ///<summary>
        /// Calling the EndDataUpdate routine informs the grid
        /// that an extended data update phase has ended.
        /// 
        /// BeginDataUpdate / EndDataUpdate can be nested and must be
        /// called in pairs – every BeginDataUpdate must have a
        /// matching EndDataUpdate call.
        ///</summary>
        public void EndDataUpdate()
        {
            DataBinder.EndUpdate();
        }

        #endregion

        #region UpdateDataBindings

        private void UpdateDataBindings()
        {
            if (_NeedToUpdateBindings == true)
            {
                if (DataBinder.ConnectionInProgress == false)
                {
                    if (VirtualMode == false)
                        Rows.Clear();
                    else
                        VirtualRowCount = 0;

                    ClearAll();
                    UnDeleteAll();

                    DataBinder.Clear();

                    if (DataSource != null)
                    {
                        DataBinder.DataConnect();

                        _FilterEval = null;

                        if (DataBinder.CurrencyManager != null)
                            _NeedToUpdateBindings = false;
                    }
                    else
                    {
                        _NeedToUpdateBindings = false;
                    }
                }
            }
        }

        #endregion

        #region UpdateDataFiltering

        private void UpdateDataFiltering()
        {
            if (NeedToUpdateDataFilter == true)
            {
                if (DeactivateEdit() == true)
                {
                    ClearAll();

                    DataFilter.FilterData(this);

                    SuperGrid.NeedToUpdateIndicees = true;

                    NeedToUpdateDataFilter = false;
                    NeedsGrouped = true;
                }
            }
        }

        #endregion

        #region UpdateVisibleRowCount

        internal void UpdateVisibleRowCount()
        {
            if (_VirtualMode == true)
            {
                _VisibleRowCount = _VirtualRowCount;
            }
            else
            {
                _VisibleRowCount = 0;

                for (int i = 0; i < Rows.Count; i++)
                {
                    if (Rows[i].Visible == true)
                        _VisibleRowCount++;
                }
            }
        }

        #endregion

        #region ClearAllFilters

        ///<summary>
        /// Clears all panel and column level filters
        ///</summary>
        public void ClearAllFilters()
        {
            ClearAllFilters(true, true);
        }

        ///<summary>
        /// Clears all panel and column level filters, as specified.
        ///</summary>
        public void ClearAllFilters(
            bool clearPanelFilter, bool clearColumnFilters)
        {
            if (clearPanelFilter == true)
                FilterExpr = null;

            if (clearColumnFilters == true)
            {
                if (Columns != null)
                {
                    foreach (GridColumn col in Columns)
                        col.FilterExpr = null;
                }
            }
        }

        #endregion

        #region ClearColumnFilterEvals

        internal void ClearColumnFilterEvals()
        {
            _FilterEval = null;

            if (_Columns != null)
            {
                foreach (GridColumn col in _Columns)
                    col.FilterEval = null;
            }
        }

        #endregion

        #region DetachDataSource

        ///<summary>
        /// Detaches the grid from the bound DataSource.
        /// If 'keepData' is true, then the data is kept (but no longer bound) 
        ///</summary>
        ///<param name="keepData"></param>
        public void DetachDataSource(bool keepData)
        {
            if (DataSource != null)
            {
                if (keepData == true)
                {
                    if (IsLayoutValid == false)
                        SuperGrid.ArrangeGrid();

                    if (VirtualMode == true)
                    {
                        VirtualRowCount = 0;
                    }
                    else
                    {
                        foreach (GridElement item in Rows)
                        {
                            GridRow row = item as GridRow;

                            if (row != null)
                            {
                                foreach (GridCell cell in row.Cells)
                                    cell.ValueExx = DataBinder.GetValue(cell, cell.ValueEx);
                            }
                        }
                    }
                }

                DataSource = null;

                if (keepData == true)
                {
                    _NeedToUpdateBindings = false;
                }
                else
                {
                    if (VirtualMode == false)
                        Rows.Clear();
                }
            }
        }

        #endregion

        #region UpdateRowPosition

        internal void UpdateRowPosition(GridRow row)
        {
            if (KeepRowsSorted == true)
            {
                if (_SortColumns != null && _SortColumns.Count > 0 && Rows.Count > 1)
                {
                    RowComparer rc = new RowComparer(_SortColumns);

                    GridContainer cont = (GridContainer) row.Parent;

                    if (RowPositionOk(cont, row, rc) == false)
                        SortRow(cont, row, rc);
                }
            }
        }

        #region RowPositionOk

        private bool RowPositionOk(
            GridContainer cont, GridRow row, RowComparer rc)
        {
            GridRow prow = null;

            for (int i = row.RowIndex - 1; i >= 0; --i)
            {
                prow = cont.Rows[i] as GridRow;

                if (prow != null)
                    break;
            }

            if (prow != null)
            {
                if (rc.Compare(prow, row) > 0)
                    return (false);
            }

            GridRow nrow = null;

            for (int i = row.RowIndex + 1; i < cont.Rows.Count; i++)
            {
                nrow = cont.Rows[i] as GridRow;

                if (nrow != null)
                    break;
            }

            if (nrow != null)
            {
                if (rc.Compare(nrow, row) < 0)
                    return (false);
            }

            return (true);
        }

        #endregion

        #region SortRow

        private void SortRow(
            GridContainer cont, GridRow row, RowComparer rc)
        {
            int rowIndex = row.RowIndex;

            _IsRowMoving = true;

            cont.Rows.RemoveAt(rowIndex);

            int newRowIndex = GetRowPosition(cont, row, rc);

            cont.Rows.Insert(newRowIndex, row);

            if (newRowIndex < rowIndex)
            {
                int n = rowIndex;
                rowIndex = newRowIndex;
                newRowIndex = n;
            }

            for (int i = rowIndex; i <= newRowIndex; i++)
                ((GridContainer)cont.Rows[i]).RowIndex = i;

            if (UseAlternateRowStyle == true)
                SuperGrid.UpdateStyleCount();

            _IsRowMoving = false;

            SuperGrid.NeedToUpdateIndicees = true;

            InvalidateLayout();
        }

        #endregion

        #region GetRowPosition

        private int GetRowPosition(
            GridContainer cont, GridRow row, RowComparer rc)
        {
            int lo = 0;
            int hi = cont.Rows.Count;

            while (lo < hi)
            {
                int mid = (lo + hi)/2;

                GridRow roe = cont.Rows[mid] as GridRow;

                if (roe != null)
                {
                    int n = rc.Compare(row, roe);

                    if (n == 0)
                        return (mid);

                    if (n < 0)
                        hi = mid - 1;
                    else
                        lo = mid + 1;
                }
            }

            if ((uint)lo < cont.Rows.Count)
            {
                GridRow roe2 = cont.Rows[lo] as GridRow;

                if (roe2 != null)
                {
                    if (rc.Compare(row, roe2) > 0)
                        lo++;
                }
            }

            return (lo);
        }

        #endregion

        #endregion

        #region Style support

        #region GetSizingStyle

        internal StyleType GetSizingStyle()
        {
            return ((_SizingStyle == StyleType.NotSet)
                ? SuperGrid.SizingStyle : _SizingStyle);
        }

        #endregion

        #region DefaultVisualStylesPropertyChanged

        void DefaultVisualStylesPropertyChanged(object sender, PropertyChangedEventArgs e)
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
                InvalidateRender();
            }
        }

        #endregion

        #region GetEffectiveStyle

        internal GridPanelVisualStyle GetEffectiveStyle()
        {
            ValidateStyle();

            if (_EffectiveStyle == null)
            {
                GridPanelVisualStyle style = new GridPanelVisualStyle();

                style.ApplyStyle(SuperGrid.BaseVisualStyles.GridPanelStyle);
                style.ApplyStyle(SuperGrid.DefaultVisualStyles.GridPanelStyle);
                style.ApplyStyle(DefaultVisualStyles.GridPanelStyle);

                SuperGrid.DoGetPanelStyleEvent(this, ref style);

                if (style.Background == null || style.Background.IsEmpty == true)
                    style.Background = new Background(Color.White);

                if (style.Font == null)
                    style.Font = SystemFonts.DefaultFont;

                _EffectiveStyle = style;
            }

            return (_EffectiveStyle);
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
                CellVisualStyle cstyle = new CellVisualStyle();

                cstyle.ApplyStyle(SuperGrid.BaseVisualStyles.CellStyles[cs]);
                cstyle.ApplyStyle(SuperGrid.DefaultVisualStyles.CellStyles[cs]);
                cstyle.ApplyStyle(GridPanel.DefaultVisualStyles.CellStyles[cs]);

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
                _EffectiveStyle = null;
                _EffectiveCellStyles = null;

                ExpandButtonImage = null;
                ExpandButtonHotImage = null;
                CollapseButtonImage = null;
                CollapseButtonHotImage = null;

                _StyleUpdateCount = SuperGrid.StyleUpdateCount;
            }
        }

        #endregion

        #region InvalidateStyle

        ///<summary>
        ///Invalidates the cached Style definition
        ///</summary>
        public void InvalidateStyle()
        {
            if (SuperGrid != null)
                _StyleUpdateCount = SuperGrid.StyleUpdateCount - 1;

            InvalidateLayout();
        }

        #endregion

        #endregion

        #region Markup support

        private void NoRowsMarkupTextChanged()
        {
            _NoRowsMarkup = null;

            if (_EnableNoRowsMarkup == true)
            {
                if (MarkupParser.IsMarkup(_NoRowsText) == true)
                {
                    _NoRowsMarkup = MarkupParser.Parse(_NoRowsText);

                    if (_NoRowsMarkup != null)
                        _NoRowsMarkup.HyperLinkClick += NoRowsMarkupLinkClick;
                }
            }
        }

        /// <summary>
        /// Occurs when a text markup link is clicked
        /// </summary>
        protected virtual void NoRowsMarkupLinkClick(object sender, EventArgs e)
        {
            HyperLink link = sender as HyperLink;

            SuperGrid.DoNoRowsMarkupLinkClickEvent(this, link);
        }

        /// <summary>
        /// Gets plain text without text-markup (if text-markup is used in NoRowsText)
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string NoRowsPlainText
        {
            get { return (_NoRowsMarkup != null ? _NoRowsMarkup.PlainText : _NoRowsText); }
        }

        #endregion

        #region SetHeaderTooltip

        internal void SetHeaderTooltip(GridElement item,
            GridColumn column, HeaderArea hitArea, string toolTip)
        {
            if (SuperGrid.DoGetColumnHeaderToolTipEvent(this, column, hitArea, ref toolTip) == false)
            {
                if (SuperGrid.ToolTipText != toolTip)
                    SuperGrid.ToolTip.Hide(SuperGrid);

                SuperGrid.ToolTipText = toolTip;
            }
            else
            {
                SuperGrid.ToolTipText = "";
            }
        }

        #endregion

        #region ToString

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string s = base.ToString();

            if (String.IsNullOrEmpty(_Name) == false)
                s += ": (\"" + _Name + "\")";

            return (s);
        }

        #endregion

        #region Dispose

        public override void Dispose()
        {
            DataBinder = null;

            ExpandButtonImage = null;
            ExpandButtonHotImage = null;
            CollapseButtonImage = null;
            CollapseButtonHotImage = null;

            base.Dispose();
        }

        #endregion
    }

    #region enums

    #region ActiveRowIndicatorStyle

    ///<summary>
    /// ActiveRowIndicatorStyle
    ///</summary>
    public enum ActiveRowIndicatorStyle
    {
        ///<summary>
        /// No indicator will be displayed.
        ///</summary>
        None,

        ///<summary>
        /// An image will be displayed.
        ///</summary>
        Image,

        ///<summary>
        /// The Row Header will be highlighted.
        ///</summary>
        Highlight,

        ///<summary>
        /// An image will be displayed and the
        /// Row Header will be highlighted.
        ///</summary>
        Both,
    }

    #endregion

    #region CellDragBehavior

    ///<summary>
    /// Specifies the resultant behavior
    /// of clicking and dragging on a grid cell
    ///</summary>
    public enum CellDragBehavior
    {
        ///<summary>
        /// Dragging will have no effect
        ///</summary>
        None,

        ///<summary>
        /// Dragging will extend the selection
        ///</summary>
        ExtendSelection,
    }

    #endregion

    #region ProcessChildRelations

    ///<summary>
    /// Specifies the conditions under which
    /// encountered child relations will be processed 
    ///</summary>
    public enum ProcessChildRelations
    {
        ///<summary>
        /// Always
        ///</summary>
        Always,

        ///<summary>
        /// Never
        ///</summary>
        Never,

        ///<summary>
        /// Only when data present
        ///</summary>
        WhenDataPresent,
    }

    #endregion

    #region ColumnHeaderClickBehavior

    ///<summary>
    /// Specifies the resultant behavior
    /// of clicking on a column header
    ///</summary>
    public enum ColumnHeaderClickBehavior
    {
        ///<summary>
        /// Clicking on a column header will have no effect
        ///</summary>
        None,

        ///<summary>
        /// Clicking on a column header will select the
        /// column, and dragging will extend the selection
        /// if ColumnDragBehavior is set to ExtendClickBehavior
        ///</summary>
        Select,

        ///<summary>
        /// Clicking on a column header will sort the grid using
        /// that column, or will reorder the column if ColumnDragBehavior
        /// is set to ExtendClickBehavior
        ///</summary>
        SortAndReorder,
    }

    #endregion

    #region ColumnDragBehavior

    ///<summary>
    /// Specifies the resultant behavior
    /// of dragging a column header
    ///</summary>
    public enum ColumnDragBehavior
    {
        ///<summary>
        /// Dragging on a column header will have no effect
        ///</summary>
        None,

        ///<summary>
        /// Dragging a column header will
        /// extend the set ColumnHeaderClickBehavior
        ///</summary>
        ExtendClickBehavior,
    }

    #endregion

    #region FilterLevel

    /// <summary>
    /// FilterLevel
    /// </summary>
    [Flags]
    public enum FilterLevel
    {
        /// <summary>
        /// No data filtered
        /// </summary>
        None = 0,

        /// <summary>
        /// Root level data filtered
        /// </summary>
        Root = (1 << 0),

        /// <summary>
        /// Root level data filtered
        /// conditional upon Rows collection being empty
        /// </summary>
        RootConditional = (1 << 1),

        /// <summary>
        /// Expanded level data filtered
        /// </summary>
        Expanded = (1 << 2),

        /// <summary>
        /// All data filtered
        /// </summary>
        All = (Root | Expanded),

        /// <summary>
        /// All data filtered, Root Level data filtered
		/// conditional upon Rows collection being empty
        /// </summary>
        AllConditional = (RootConditional | Expanded),
    }

    #endregion

    #region FilterMatchType

    ///<summary>
    /// Specifies what match style to use when filtering data elements
    ///</summary>
    public enum FilterMatchType
    {
        ///<summary>
        /// Not set
        ///</summary>
        NotSet,

        ///<summary>
        /// No special matching
        ///</summary>
        None,

        ///<summary>
        /// Regular Expression matching
        ///</summary>
        RegularExpressions,

        ///<summary>
        /// Wildcard matching
        ///</summary>
        Wildcards,
    }

    #endregion

    #region GridLines

    ///<summary>
    /// Specifies which grid lines are displayed
    ///</summary>
    public enum GridLines
    {
        ///<summary>
        /// No grid lines are displayed
        ///</summary>
        None,  
        
        ///<summary>
        /// Only horizontal grid lines are displayed
        ///</summary>
        Horizontal,

        ///<summary>
        /// Only vertical grid lines are displayed
        ///</summary>
        Vertical,

        ///<summary>
        /// Both horizontal and vertical grid lines are displayed
        ///</summary>
        Both,
    }

    #endregion

    #region GroupHeaderClickBehavior

    ///<summary>
    /// Specifies the behavior when a user
    /// clicks on a group header
    ///</summary>
    public enum GroupHeaderClickBehavior
    {
        ///<summary>
        /// Clicking on a group header will have no effect
        ///</summary>
        None,

        ///<summary>
        /// Clicking on a header will
        /// expand or collapse the group
        ///</summary>
        ExpandCollapse,

        ///<summary>
        /// Clicking on a group heading will select the header
        ///</summary>
        Select,

        ///<summary>
        /// Clicking on a group heading will
        /// select all the first-level rows contained in the group
        ///</summary>
        SelectAll,
    }

    #endregion

    #region GroupHeaderKeyBehavior

    ///<summary>
    /// Specifies the behavior when a user
    /// moves via the keyboard onto a group header
    ///</summary>
    public enum GroupHeaderKeyBehavior
    {
        ///<summary>
        /// Header will be skipped
        ///</summary>
        Skip,

        ///<summary>
        /// Header will be selected
        ///</summary>
        Select,
    }

    #endregion

    #region KeyboardEditMode

    ///<summary>
    /// Which keyboard actions will start a cell edit operation
    ///</summary>
    public enum KeyboardEditMode
    {
        ///<summary>
        /// No keyboard action will initiate an edit
        ///</summary>
        None,

        ///<summary>
        /// Edit initiated when any alphanumeric key
        /// is pressed while the cell has focus
        ///</summary>
        EditOnKeystroke,

        ///<summary>
        /// Edit initiated when F2 is pressed while the cell
        /// has focus. This mode places the selection point at
        /// the end of the cell contents
        ///</summary>
        EditOnF2,

        ///<summary>
        /// Editing begins when any alphanumeric key
        /// or F2 is pressed while the cell has focus
        ///</summary>
        EditOnKeystrokeOrF2,

        ///<summary>
        /// Editing begins on entry to a cell
        ///</summary>
        EditOnEntry
    }

    #endregion

    #region NullValue

    ///<summary>
    /// Specifies how null values are interpreted 
    ///</summary>
    public enum NullValue
    {
        ///<summary>
        /// A null is interpreted to be a null object reference
        ///</summary>
        NullReference,

        ///<summary>
        /// A null is interpreted to be an instance of DBNull.Value
        ///</summary>
        DBNull,
    }

    #endregion

    #region RelativeSelection

    ///<summary>
    /// RelativeSelection
    ///</summary>
    public enum RelativeSelection
    {
        ///<summary>
        /// None
        ///</summary>
        None,

        ///<summary>
        /// FirstCell
        ///</summary>
        FirstCell,

        ///<summary>
        /// LastCell
        ///</summary>
        LastCell,

        ///<summary>
        /// PrimaryCell
        ///</summary>
        PrimaryCell,

        ///<summary>
        /// Row
        ///</summary>
        Row,
    }

    #endregion

    #region MouseEditMode

    ///<summary>
    /// Which mouse actions will start a cell edit operation
    ///</summary>
    public enum MouseEditMode
    {
        ///<summary>
        /// No mouse action will initiate an edit
        ///</summary>
        None,

        ///<summary>
        /// Single clicking a cell will initiate an edit
        ///</summary>
        SingleClick,
        
        ///<summary>
        /// Double clicking a cell will initiate an edit
        ///</summary>
        DoubleClick,
    }

    #endregion

    #region PanelArea

    ///<summary>
    /// PanelArea
    ///</summary>
    public enum PanelArea
    {
        ///<summary>
        /// In Content
        ///</summary>
        InContent,

        ///<summary>
        /// In WhiteSpace
        ///</summary>
        InWhiteSpace,
    }

    #endregion

    #region RelativeRow

    ///<summary>
    /// Relative row (cardinality)
    ///</summary>
    public enum RelativeRow
    {
        ///<summary>
        /// No Row
        ///</summary>
        None,

        ///<summary>
        /// First Row
        ///</summary>
        FirstRow,

        ///<summary>
        /// Insertion Row
        ///</summary>
        InsertionRow,

        ///<summary>
        /// Last Row
        ///</summary>
        LastRow,
    }

    #endregion

    #region RowDragBehavior

    ///<summary>
    /// Specifies the resultant behavior
    /// of clicking and dragging on a row header
    ///</summary>
    public enum RowDragBehavior
    {
        ///<summary>
        /// Dragging on a row header will have no effect
        ///</summary>
        None,

        ///<summary>
        /// Dragging a row header will extend the selection
        ///</summary>
        ExtendSelection,

        ///<summary>
        /// Dragging a row header will initiate a row move that
        /// will be constrained to stay within the current group.
        ///</summary>
        Move,

        ///<summary>
        /// Dragging a row header will initiate a row move that
        /// can cross group row boundaries
        ///</summary>
        GroupMove,
    }

    #endregion

    #region RowEditMode

    ///<summary>
    /// Defines whether editing is initiated on the primary cell
    /// or the cell under the mouse when row level editing is enabled
    ///</summary>
    public enum RowEditMode
    {
        ///<summary>
        /// Initiate edit on PrimaryCell
        ///</summary>
        PrimaryCell,

        ///<summary>
        /// Initiate edit on clicked cell
        ///</summary>
        ClickedCell,
    }

    #endregion

    #region RowHighlightType

    ///<summary>
    /// Specifies the type of highlighting
    /// to employ when a grid row is selected
    ///</summary>
    public enum RowHighlightType
    {
        ///<summary>
        /// No highlighting
        ///</summary>
        None,

        ///<summary>
        /// Entire row will be Highlighted
        ///</summary>
        Full,

        ///<summary>
        /// Only the PrimaryColumn
        /// cell contents will be Highlighted
        ///</summary>
        PrimaryColumnOnly,
    }

    #endregion

    #region RowWhitespaceClickBehavior

    ///<summary>
    /// Specifies the resultant behavior
    /// of clicking in the row whitespace area
    ///</summary>
    public enum RowWhitespaceClickBehavior
    {
        ///<summary>
        /// No effect
        ///</summary>
        None,

        ///<summary>
        /// Current selection is cleared
        ///</summary>
        ClearSelection,

        ///<summary>
        /// Extends the row selection
        ///</summary>
        ExtendSelection,
    }

    #endregion

    #region SelectionGranularity

    ///<summary>
    /// Determines whether row or cell selection is permitted with in the grid
    ///</summary>
    public enum SelectionGranularity
    {
        ///<summary>
        /// Individual cells may be selected
        ///</summary>
        Cell,

        ///<summary>
        /// Only entire rows may be selected
        ///</summary>
        Row,

        ///<summary>
        /// Only entire rows may be selected, but individual
        /// cells will be highlighted when mouse is over cell.
        ///</summary>
        RowWithCellHighlight
    }

    #endregion

    #region SortDirection

    /// <summary>
    /// SortDirection
    /// </summary>
    public enum SortDirection
    {
        /// <summary>
        /// No sorting
        /// </summary>
        None,

        /// <summary>
        /// Ascending
        /// </summary>
        Ascending,

        /// <summary>
        /// Descending
        /// </summary>
        Descending
    }

    #endregion

    #region SortLevel

    /// <summary>
    /// SortLevel
    /// </summary>
    [Flags]
    public enum SortLevel
    {
        /// <summary>
        /// No data sorting
        /// </summary>
        None = 0,

        /// <summary>
        /// Root level data
        /// </summary>
        Root = (1 << 0),

        /// <summary>
        /// Expanded level data
        /// </summary>
        Expanded = (1 << 1),

        /// <summary>
        /// All data
        /// </summary>
        All = (Root | Expanded),
    }

    #endregion

    #region TopLeftHeaderSelectBehavior

    ///<summary>
    /// TopLeftHeaderSelectBehavior
    ///</summary>
    public enum TopLeftHeaderSelectBehavior
    {
        ///<summary>
        /// No selection
        ///</summary>
        NoSelection,

        ///<summary>
        /// No selection
        ///</summary>
        SelectAllCells,

        ///<summary>
        /// All columns will be selected
        ///</summary>
        SelectAllColumns,

        ///<summary>
        /// All rows will be selected
        ///</summary>
        SelectAllRows,

        ///<summary>
        /// Element selection will be determined by
        /// the current set SelectionGranularity
        ///</summary>
        Deterministic,
    }

    #endregion

    #region WhitespaceClickBehavior

    ///<summary>
    /// Specifies the resultant behavior
    /// of clicking in the grid panel whitespace area
    ///</summary>
    public enum WhitespaceClickBehavior
    {
        ///<summary>
        /// No effect
        ///</summary>
        None,

        ///<summary>
        /// Current selection is cleared
        ///</summary>
        ClearSelection,
    }

    #endregion

    #endregion

    #region IEnumeration

    #region FlatCheckedRowsEnumeration

    class FlatCheckedRowsEnumeration : IEnumerator, IEnumerable
    {
        #region Private variables

        private GridPanel _Panel;
        private bool _InclExpanded;

        private int _CurIndex;
        private GridContainer _CurItem;
        private GridContainer _CurContainer;

        private Stack<int> _IndexStack;
        private Stack<GridContainer> _ContStack;

        #endregion

        public FlatCheckedRowsEnumeration(GridPanel panel, bool inclExpanded)
        {
            _Panel = panel;
            _InclExpanded = inclExpanded;

            Reset();
        }

        #region IEnumerable support

        #region MoveNext

        public bool MoveNext()
        {
            PushCurItem();

            while (GetNextItem() == false)
            {
                if (_ContStack.Count <= 0)
                    return (false);
                
                PopCurItem();
            }

            return (true);
        }

        #region GetNextItem

        private bool GetNextItem()
        {
            while (++_CurIndex < _CurContainer.Rows.Count)
            {
                _CurItem = _CurContainer.Rows[_CurIndex] as GridContainer;

                if (_CurItem != null && _CurItem.IsDeleted == false)
                {
                    if (_CurItem.Checked == true)
                        return (true);

                    PushCurItem();
                }
            }

            return (false);
        }

        #endregion

        #region PushCurItem

        private void PushCurItem()
        {
            if (_CurItem != null)
            {
                if (_CurItem.Rows.Count > 0 && _InclExpanded == true)
                {
                    _ContStack.Push(_CurContainer);
                    _IndexStack.Push(_CurIndex);

                    _CurIndex = -1;
                    _CurContainer = _CurItem;
                }
            }
        }

        #endregion

        #region PopCurItem

        private void PopCurItem()
        {
            _CurContainer = _ContStack.Pop();
            _CurIndex = _IndexStack.Pop();
        }

        #endregion

        #endregion

        #region Reset

        public void Reset()
        {
            _CurIndex = -1;
            _CurItem = null;
            _CurContainer = _Panel;

            _IndexStack = new Stack<int>();
            _ContStack = new Stack<GridContainer>();
        }

        #endregion

        #region Current

        public object Current
        {
            get { return (_CurItem); }
        }

        #endregion

        #region GetEnumerator

        public IEnumerator GetEnumerator()
        {
            return (this);
        }

        #endregion

        #endregion
    }

    #endregion

    #region FlatDeletedRowsEnumeration

    class FlatDeletedRowsEnumeration : IEnumerator, IEnumerable
    {
        #region Private variables

        private GridPanel _Panel;

        private int _CurIndex;
        private GridContainer _CurItem;
        private GridContainer _CurContainer;

        private Stack<int> _IndexStack;
        private Stack<GridContainer> _ContStack;

        #endregion

        public FlatDeletedRowsEnumeration(GridPanel panel)
        {
            _Panel = panel;

            Reset();
        }

        #region IEnumerable support

        public bool MoveNext()
        {
            if (_CurContainer == null)
            {
                _CurIndex = -1;
                _CurContainer = _Panel;
            }

            while (GetNextItem() == false)
            {
                if (_ContStack.Count > 0)
                {
                    _CurContainer = _ContStack.Pop();
                    _CurIndex = _IndexStack.Pop();
                }
                else
                {
                    return (false);
                }
            }

            _CurItem = (GridContainer)_CurContainer.Rows[_CurIndex];

            return (true);
        }

        private bool GetNextItem()
        {
            while (++_CurIndex < _CurContainer.Rows.Count)
            {
                GridContainer row =
                    _CurContainer.Rows[_CurIndex] as GridContainer;

                if (row == null)
                    return (false);
                
                if (row.IsDeleted == true)
                    return (true);

                if (row.Rows.Count > 0 && row.RowsUnresolved == false)
                {
                    _ContStack.Push(_CurContainer);
                    _IndexStack.Push(_CurIndex);

                    _CurIndex = -1;
                    _CurContainer = row;
                }
            }

            return (false);
        }

        public void Reset()
        {
            _CurIndex = -1;
            _CurItem = null;
            _CurContainer = null;

            _IndexStack = new Stack<int>();
            _ContStack = new Stack<GridContainer>();
        }

        public object Current
        {
            get { return (_CurItem); }
        }

        public IEnumerator GetEnumerator()
        {
            return (this);
        }

        #endregion
    }

    #endregion

    #region FlatRowsEnumeration

    class FlatRowsEnumeration : IEnumerator, IEnumerable
    {
        #region Private variables

        private GridPanel _Panel;
        private bool _OnlyExpanded;
        private bool _OnlyVisible;

        private int _CurIndex;
        private GridContainer _CurItem;
        private GridContainer _CurContainer;

        private Stack<int> _IndexStack;
        private Stack<GridContainer> _ContStack;

        #endregion

        public FlatRowsEnumeration(
            GridPanel panel, bool onlyExpanded, bool onlyVisible)
        {
            _Panel = panel;

            _OnlyExpanded = onlyExpanded;
            _OnlyVisible = onlyVisible;

            Reset();
        }

        #region IEnumerable support

        public bool MoveNext()
        {
            if (_CurContainer == null)
            {
                _CurIndex = -1;
                _CurContainer = _Panel;

                if (GetNextItem() == false)
                    return (false);
            }
            else
            {
                if (_CurItem.Rows.Count > 0 &&
                    (_OnlyExpanded == false || _CurItem.Expanded == true))
                {
                    _ContStack.Push(_CurContainer);
                    _IndexStack.Push(_CurIndex);

                    _CurIndex = -1;
                    _CurContainer = _CurItem;
                }

                while (GetNextItem() == false)
                {
                    if (_ContStack.Count > 0)
                    {
                        _CurContainer = _ContStack.Pop();
                        _CurIndex = _IndexStack.Pop();
                    }
                    else
                    {
                        return (false);
                    }
                }
            }

            _CurItem = (GridContainer)_CurContainer.Rows[_CurIndex];

            return (true);
        }

        private bool GetNextItem()
        {
            while (++_CurIndex < _CurContainer.Rows.Count)
            {
                GridContainer row =
                    _CurContainer.Rows[_CurIndex] as GridContainer;

                if (row != null && row.IsDeleted == false)
                {
                    if (_OnlyVisible == false || row.Visible == true)
                        return (true);
                }
            }

            return (false);
        }

        public void Reset()
        {
            _CurIndex = -1;
            _CurItem = null;
            _CurContainer = null;

            _IndexStack = new Stack<int>();
            _ContStack = new Stack<GridContainer>();
        }

        public object Current
        {
            get { return (_CurItem); }
        }

        public IEnumerator GetEnumerator()
        {
            return (this);
        }

        #endregion
    }

    #endregion

    #region OnScreenColumnsEnumeration

    class OnScreenColumnsEnumeration : IEnumerator, IEnumerable
    {
        #region Private variables

        private GridPanel _Panel;

        private int _CurIndex;
        private GridColumn _CurItem;

        #endregion

        public OnScreenColumnsEnumeration(GridPanel panel)
        {
            _Panel = panel;

            Reset();
        }

        #region IEnumerable support

        public bool MoveNext()
        {
            int[] map = _Panel.Columns.DisplayIndexMap;

            while (++_CurIndex < map.Length)
            {
                _CurItem = _Panel.Columns[map[_CurIndex]];

                if (_CurItem.IsOnScreen == true)
                    return (true);
            }

            return (false);
        }

        public void Reset()
        {
            _CurIndex = -1;
        }

        public object Current
        {
            get { return (_CurItem); }
        }

        public IEnumerator GetEnumerator()
        {
            return (this);
        }

        #endregion
    }

    #endregion

    #region OnScreenRowsEnumeration

    class OnScreenRowsEnumeration : IEnumerator, IEnumerable
    {
        #region Private variables

        private GridPanel _Panel;

        private int _CurIndex;
        private GridContainer _CurItem;

        private int _FirstVisibleRow;
        private int _LastVisibleRow;

        #endregion

        public OnScreenRowsEnumeration(GridPanel panel)
        {
            _Panel = panel;

            _FirstVisibleRow = panel.FirstOnScreenRowIndex;
            _LastVisibleRow = panel.LastOnScreenRowIndex;

            Reset();
        }

        #region IEnumerable support

        public bool MoveNext()
        {
            _CurIndex = (_CurIndex == -1) ?
                _FirstVisibleRow : _CurIndex + 1;

            if (_CurIndex <= _LastVisibleRow)
            {
                if (_Panel.VirtualMode == true)
                    _CurItem = _Panel.VirtualRows[_CurIndex];
                else
                    _CurItem = (GridContainer) _Panel.Rows[_CurIndex];

                return (true);
            }

            return (false);
        }

        public void Reset()
        {
            _CurIndex = -1;
        }

        public object Current
        {
            get { return (_CurItem); }
        }

        public IEnumerator GetEnumerator()
        {
            return (this);
        }

        #endregion
    }

    #endregion

    #region SelectedEnumeration

    class SelectedEnumeration : IEnumerator, IEnumerable
    {
        #region Private variables

        private GridPanel _Panel;
        private SelectedElements _SelectedElements;

        private int _CurIndex;

        #endregion

        protected SelectedEnumeration(
            GridPanel panel, SelectedElements selectedElements)
	    {
            _Panel = panel;
            _SelectedElements = selectedElements;

            Reset();
        }

        #region Protected properties

        protected GridPanel Panel
        {
            get { return (_Panel); }
        }

        protected int CurIndex
        {
            get { return (_CurIndex); }
        }

        #endregion

        #region IEnumerable support

        public bool MoveNext()
        {
            return (_SelectedElements.GetNextIndex(ref _CurIndex));
        }

        public void Reset()
        {
            _CurIndex = -1;
        }

        public object Current
        {
            get { return (GetCurrent); }
        }

        protected virtual object GetCurrent
        {
            get { return (null); }
        }

        public IEnumerator GetEnumerator()
        {
            return (this);
        }

        #endregion
    }

    #endregion

    #region SelectedRowsEnumeration

    class SelectedRowsEnumeration : SelectedEnumeration
    {
        public SelectedRowsEnumeration(GridPanel panel, SelectedElements selectedElements)
            : base(panel, selectedElements)
        {
        }

        protected override object GetCurrent
        {
            get { return (Panel.GetRowFromIndex(CurIndex)); }
        }
    }

    #endregion

    #region SelectedColumnsEnumeration

    class SelectedColumnsEnumeration : SelectedEnumeration
    {
        public SelectedColumnsEnumeration(GridPanel panel, SelectedElements selectedElements)
            : base(panel, selectedElements)
        {
        }

        protected override object GetCurrent
        {
            get { return (Panel.Columns[CurIndex]); }
        }
    }

    #endregion

    #region SelectedCellsEnumeration

    class SelectedCellsEnumeration : IEnumerator, IEnumerable
    {
        #region Private variables

        private GridPanel _Panel;

        private int _CurIndex;
        private int _CurColumnIndex;

        private GridCell _CurGridCell;
        private GridCell _EmptyCell;

        #endregion

        public SelectedCellsEnumeration(GridPanel panel)
        {
            _Panel = panel;

            Reset();
        }

        #region IEnumerable support

        public bool MoveNext()
        {
            if (_CurColumnIndex == -1)
            {
                _CurColumnIndex++;
                _CurIndex = -1;
            }

            while (_CurColumnIndex < _Panel.Columns.Count)
            {
                GridColumn column = _Panel.Columns[_CurColumnIndex];

                while (column.SelectedCells.GetNextIndex(ref _CurIndex) == true)
                {
                    GridRow row = _Panel.GetRowFromIndex(_CurIndex) as GridRow;

                    if (row != null)
                    {
                        _CurGridCell = row.GetCell(_CurColumnIndex);

                        if (_CurGridCell == null)
                        {
                            _EmptyCell = row.GetEmptyCell(_EmptyCell, _CurColumnIndex);

                            if (_EmptyCell != null)
                            {
                                _CurGridCell = _EmptyCell;

                                return (true);
                            }
                        }
                        else
                        {
                            if (_CurGridCell.AllowSelection == true)
                                return (true);
                        }
                    }
                }

                _CurColumnIndex++;
                _CurIndex = -1;
            }

            return (false);
        }

        public void Reset()
        {
            _CurIndex = -1;
            _CurColumnIndex = -1;
        }

        public object Current
        {
            get { return (_CurGridCell); }
        }

        public IEnumerator GetEnumerator()
        {
            return (this);
        }

        #endregion
    }

    #endregion

    #endregion

    #region BlankExpandableObjectConverter

    ///<summary>
    /// BlankExpandableObjectConverter
    ///</summary>
    public class BlankExpandableObjectConverter : ExpandableObjectConverter
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
                return (" ");

            return (base.ConvertTo(context, culture, value, destinationType));
        }
    }

    #endregion
}
