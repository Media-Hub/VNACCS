using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using DevComponents.DotNetBar.SuperGrid.Primitives;
using DevComponents.DotNetBar.SuperGrid.Style;

namespace DevComponents.DotNetBar.SuperGrid
{
    /// <summary>
    /// Represents a grid row consisting of one or more grid cells.
    /// </summary>
    public class GridRow : GridContainer
    {
        #region Constants

        private const int SeparatorHeight = 2;

        #endregion

        #region Static data

        private static bool _dragSelection;
        private static bool _dragStarted;

        private static FloatWindow _separatorFw;
        private static FloatWindow _headerFw;

        #endregion

        #region Private variables

        private readonly GridCellCollection _Cells;

        private int _AnchorIndex;
        private int _RowHeight = -1;
        private int _PreDetailRowHeight = -1;
        private int _PostDetailRowHeight = -1;
        private int _EffectivePreDetailRowHeight;
        private int _EffectivePostDetailRowHeight;

        private Rs _States;

        private  RowArea _HitArea;
        private  GridContainer _HitItem;
        private  RowArea _LastArea;

        private RowArea _MouseDownHitArea;
        private GridContainer _MouseDownHitRow;
        private Point _MouseDownPoint;
        private int _MouseDownDelta;

        private GridContainer _SeparatorRow;

        private string _InfoText;
        private Rectangle _InfoImageBounds;
        private Rectangle _ExpandButtonBounds;

        private object _DataItem;
        private int _DataItemIndex;

        private GridCell _EmptyRenderCell;
        private GridCell _EmptySelectionCell;

        private int _ArrangeLayoutCount;
        private int _MeasureCount;
        private Size _RowHeaderSize;

        private int _StyleUpdateCount;
        private CellVisualStyles _EffectiveCellStyles;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new GridRow
        /// </summary>
        public GridRow()
        {
            SetState(Rs.AllowEdit, true);
            SetState(Rs.Visible, true);

            _Cells = new GridCellCollection();
            _Cells.CollectionChanged += CellsCollectionChanged;
        }

        /// <summary>
        /// Creates a new GridRow
        /// </summary>
        /// <param name="text"></param>
        public GridRow(string text)
            : this()
        {
            GridCell cell = new GridCell();
            cell.Value = text;

            _Cells.Add(cell);
        }

        /// <summary>
        /// Creates a new GridRow
        /// </summary>
        /// <param name="cells"></param>
        public GridRow(IEnumerable<GridCell> cells)
            : this()
        {
            foreach (GridCell cell in cells)
                _Cells.Add(cell);
        }

        /// <summary>
        /// Creates a new GridRow
        /// </summary>
        /// <param name="values"></param>
        public GridRow(IEnumerable<object> values)
            : this()
        {
            foreach (object o in values)
                _Cells.Add(new GridCell(o));
        }

        /// <summary>
        /// Creates a new GridRow
        /// </summary>
        /// <param name="values"></param>
        public GridRow(params object[] values)
            : this()
        {
            foreach (object o in values)
                _Cells.Add(new GridCell(o));
        }

        #endregion

        #region Public properties

        #region AllowEdit

        /// <summary>
        /// Gets or sets whether the row cells can be edited by the user. 
        /// </summary>
        [DefaultValue(true), Category("Behavior")]
        [Description("Indicates whether the row cells can be edited by the user.")]
        public bool AllowEdit
        {
            get { return (TestState(Rs.AllowEdit)); }

            set
            {
                if (value != AllowEdit)
                {
                    SetState(Rs.AllowEdit, value);

                    OnPropertyChangedEx("AllowEdit");
                }
            }
        }

        #endregion

        #region Bounds

        ///<summary>
        /// Gets the scroll adjusted bounds of the row
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

                    if (panel.IsSubPanel == true)
                        r.X -= HScrollOffset;

                    if (IsVFrozen == false)
                        r.Y -= VScrollOffset;

                    r.Height = FixedRowHeight;

                    return (r);
                }

                return (Rectangle.Empty);
            }
        }

        #endregion

        #region Cells

        /// <summary>
        /// Gets a reference to the Cells collection. Cells in the
        /// Cells member are not allocated by default, but are only
        /// done so if the cell Values are provided to the GridRow
        /// constructor at row allocation time. Row cells can be allocated
        /// later, and assigned (or added) to the current Cells collection.
        /// Cells are sequentially allocated, thus you can not allocate and
        /// assign Cell 10 without also having allocated and assigned cells 0 - 9.
        /// </summary>
        [DefaultValue(null), Category("Data")]
        [Description("Indicates a reference to the Cells collection.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GridCellCollection Cells
        {
            get { return (_Cells); }
        }

        #endregion

        #region DataItem

        ///<summary>
        /// Gets the object to which the row is bound.
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object DataItem
        {
            get { return (_DataItem); }
            internal set { _DataItem = value; }
        }

        #endregion

        #region EditorDirty

        ///<summary>
        /// Gets or sets whether the value being edited has changed.
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool EditorDirty
        {
            get { return (TestState(Rs.EditorDirty)); }

            set
            {
                if (EditorDirty != value)
                {
                    SetState(Rs.EditorDirty, value);

                    GridPanel panel = GridPanel;

                    if (panel != null)
                    {
                        if (value == false)
                        {
                            SetState(Rs.RowDirty, true);
                            SetState(Rs.RowNeedsStored, true);
                        }
                        else
                        {
                            ProcessNewRowChange(panel);
                        }

                        InvalidateRowHeader();
                    }
                }
            }
        }

        #endregion

        #region EffectivePreDetailRowHeight

        ///<summary>
        /// Gets the effective height of the "pre detail" portion of the
        /// row (the area at the bottom of the row).
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int EffectivePreDetailRowHeight
        {
            get { return (_EffectivePreDetailRowHeight); }
        }

        #endregion

        #region EffectivePostDetailRowHeight

        ///<summary>
        /// Gets the effective height of the "post detail" portion of the
        /// row (the area at the bottom of the row).
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int EffectivePostDetailRowHeight
        {
            get { return (_EffectivePostDetailRowHeight); }
        }

        #endregion

        #region FirstFrozenCell

        /// <summary>
        /// Gets a reference to the first Frozen cell
        /// or null if there is none.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridCell FirstFrozenCell
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    GridColumn column = panel.FirstFrozenColumn;

                    if (column != null)
                    {
                        int index = column.ColumnIndex;

                        if (index < _Cells.Count)
                            return (_Cells[index]);
                    }
                }

                return (null);
            }
        }

        #endregion

        #region FirstOnScreenCell

        /// <summary>
        /// Gets a reference to the first on-screen cell
        /// (after frozen columns) or null if there is none.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridCell FirstOnScreenCell
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    GridColumn column = panel.FirstOnScreenColumn;

                    if (column != null)
                    {
                        int index = column.ColumnIndex;

                        if (index < _Cells.Count)
                            return (_Cells[index]);
                    }
                }

                return (null);
            }
        }

        #endregion

        #region FirstSelectableCell

        /// <summary>
        /// Gets a reference to the first selectable cell
        /// or null if there is none defined.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridCell FirstSelectableCell
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    for (int i = 0; i < panel.Columns.Count; i++)
                    {
                        GridColumn column = panel.Columns[i];

                        if (column.Visible == true && column.AllowSelection == true)
                        {
                            GridCell cell =
                                GetCell(i, panel.AllowEmptyCellSelection);

                            if (cell != null)
                            {
                                if (cell.AllowSelection == true)
                                    return (cell);
                            }
                        }
                    }
                }

                return (null);
            }
        }

        #endregion

        #region FirstVisibleCell

        /// <summary>
        /// Gets a reference to the first visible cell
        /// or null if there is none defined.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridCell FirstVisibleCell
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    GridColumn column = panel.Columns.FirstVisibleColumn;

                    if (column != null)
                    {
                        int index = column.ColumnIndex;

                        GridCell cell =
                            GetCell(index, panel.AllowEmptyCellSelection);

                        if (cell != null)
                            return (cell);
                    }
                }

                return (null);
            }
        }

        #endregion

        #region Indexer (Column)

        /// <summary>
        /// Gets or sets Cell item at the given Column
        /// </summary>
        /// <param name="column">Column containing the cell</param>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridCell this[GridColumn column]
        {
            get { return (this[column.ColumnIndex]); }
        }

        #endregion

        #region Indexer (int)

        /// <summary>
        /// Gets or sets Cell item at the given Column
        /// </summary>
        /// <param name="columnIndex">Column containing the cell</param>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridCell this[int columnIndex]
        {
            get
            {
                if (columnIndex < Cells.Count)
                    return (Cells[columnIndex]);

                return (null);
            }
        }

        #endregion

        #region Indexer (string)

        /// <summary>
        /// Gets or sets Cell item at ColumnName index
        /// </summary>
        /// <param name="columnName">Name of Column containing the cell</param>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridCell this[string columnName]
        {
            get { return (FindCell(columnName)); }
        }

        #endregion

        #region InfoImageBounds

        ///<summary>
        /// Gets the bounds of the InfoImage
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle InfoImageBounds
        {
            get { return (_InfoImageBounds); }
            internal set { _InfoImageBounds = value; }
        }

        #endregion

        #region InfoText

        ///<summary>
        /// Gets or sets the informational text associated with
        /// the row. If the InfoText is non-null, then the rows
        /// associated InfoImage is displayed in the RowHeader, with
        /// the InfoText being displayed as the ToolTip for the InfoImage.
        ///</summary>
        [DefaultValue(null), Category("Appearance")]
        [Description("Gets or sets the informational text associated with the row. If the InfoText is non-null, then the rows associated InfoImage is displayed in the RowHeader, with the InfoText being displayed as the ToolTip for the InfoImage.")]
        public string InfoText
        {
            get { return (_InfoText); }

            set
            {
                if (_InfoText != value)
                {
                    _InfoText = value;

                    OnPropertyChangedEx("InfoText", VisualChangeType.Render);

                    InvalidateRowHeader();
                }
            }
        }

        #endregion

        #region IsDeleted

        ///<summary>
        /// Gets or sets whether the row has been marked as deleted.
        ///</summary>
        [DefaultValue(false), Category("Appearance")]
        [Description("Indicates whether the row has been marked as deleted.")]
        public override bool IsDeleted
        {
            get { return (base.IsDeleted); }

            set
            {
                if (base.IsDeleted != value)
                {
                    SetState(Rs.RowDirty, true);
                    SetState(Rs.RowNeedsStored, true);

                    base.IsDeleted = value;
                }
            }
        }

        #endregion

        #region IsDetailRow

        ///<summary>
        /// Gets whether the row is marked as a Group Detail row.
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDetailRow
        {
            get { return (TestState(Rs.DetailRow)); }
            internal set { SetState(Rs.DetailRow, value); }
        }

        #endregion

        #region IsInsertRow

        ///<summary>
        /// Gets whether the row is the Insert Row.
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsInsertRow
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    if (IsDesignerHosted == true)
                        return (false);

                    if (panel.ShowInsertRow == true)
                    {
                        if (panel.VirtualMode == true)
                        {
                            if (GridIndex == panel.VirtualRowCountEx - 1)
                                return (true);
                        }
                        else
                        {
                            if (RowIndex == panel.Rows.Count - 1)
                                return (true);
                        }
                    }
                }

                return (false);
            }
        }

        #endregion

        #region LastFrozenCell

        /// <summary>
        /// Gets a reference to the last Frozen cell
        /// or null if there is none.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridCell LastFrozenCell
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    GridColumn column = panel.LastFrozenColumn;

                    if (column != null)
                    {
                        int index = column.ColumnIndex;

                        if (index < _Cells.Count)
                            return (_Cells[index]);
                    }
                }

                return (null);
            }
        }

        #endregion

        #region LastOnScreenCell

        /// <summary>
        /// Gets a reference to the last on-screen cell
        /// or null if there is none.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridCell LastOnScreenCell
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    GridColumn column = panel.LastOnScreenColumn;
                    
                    if (column != null)
                    {
                        int index = column.ColumnIndex;

                        if (index < _Cells.Count)
                            return (_Cells[index]);
                    }
                }

                return (null);
            }
        }

        #endregion

        #region LastSelectableCell

        /// <summary>
        /// Gets a reference to the last selectable cell
        /// or null if there is none defined.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridCell LastSelectableCell
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    for (int i = panel.Columns.Count - 1; i >= 0; i--)
                    {
                        GridColumn column = panel.Columns[i];

                        if (column.Visible == true && column.AllowSelection == true)
                        {
                            GridCell cell = GetCell(i, panel.AllowEmptyCellSelection);

                            if (cell != null)
                            {
                                if (cell.AllowSelection == true)
                                    return (cell);
                            }
                        }
                    }
                }

                return (null);
            }
        }

        #endregion

        #region LastVisibleCell

        /// <summary>
        /// Gets a reference to the last visible cell
        /// or null if there is none defined.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridCell LastVisibleCell
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    GridColumn column = panel.Columns.LastVisibleColumn;

                    if (column != null)
                    {
                        int index = column.ColumnIndex;

                        if (index < _Cells.Count)
                            return (_Cells[index]);
                    }
                }

                return (null);
            }
        }

        #endregion

        #region RowDirty

        ///<summary>
        /// Gets or sets whether the row state has changed.
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool RowDirty
        {
            get { return (TestState(Rs.RowDirty)); }

            set
            {
                if (RowDirty != value)
                {
                    SetState(Rs.RowDirty, value); 

                    InvalidateRowHeader();
                }
            }
        }

        #endregion

        #region IsRowFilteredOut

        ///<summary>
        /// Gets whether the row has been filtered out.
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsRowFilteredOut
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    return (panel.IsFiltered == true &&
                        RowFilteredOut == true);
                }

                return (false);
            }
        }

        #endregion

        #region RowHeight

        ///<summary>
        /// Gets or sets the height of the row (0 denotes AutoSize, -1 denotes NotSet).
        ///</summary>
        [DefaultValue(-1), Category("Sizing")]
        [Description("Indicates the height of the row (0 denotes AutoSize, -1 denotes NotSet).")]
        public int RowHeight
        {
            get { return (_RowHeight); }

            set
            {
                if (_RowHeight != value)
                {
                    _RowHeight = (value < 0) ? -1 : value;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("RowHeight", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region PostDetailRowHeight

        ///<summary>
        /// Gets or sets the height of the "post detail" portion of the
        /// row (the area at the bottom of the row). -1 denotes NotSet.
        ///</summary>
        [DefaultValue(-1), Category("Sizing")]
        [Description("Indicates the height of the 'post detail' portion of the row (the area at the bottom of the row). -1 denotes NotSet.")]
        public int PostDetailRowHeight
        {
            get { return (_PostDetailRowHeight); }

            set
            {
                if (_PostDetailRowHeight != value)
                {
                    _PostDetailRowHeight = (value < 0) ? -1 : value;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("PostDetailRowHeight", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region RowPreDetailHeight

        ///<summary>
        /// Gets or sets the height of the "pre detail" portion of the
        /// row (the area at the bottom of the row). -1 denotes NotSet.
        ///</summary>
        [DefaultValue(-1), Category("Sizing")]
        [Description("Indicates the height of the 'pre detail' portion of the row (the area at the bottom of the row). -1 denotes NotSet.")]
        public int RowPreDetailHeight
        {
            get { return (_PreDetailRowHeight); }

            set
            {
                if (_PreDetailRowHeight != value)
                {
                    _PreDetailRowHeight = (value < 0) ? -1 : value;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("RowPreDetailHeight", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region SelectedCells

        ///<summary>
        /// Gets an array of the currently Selected Cells.
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridCell[] SelectedCells
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    if (IsSelected == true)
                    {
                        GridCell[] cells = new GridCell[Cells.Count];

                        for (int i = 0; i < Cells.Count; i++)
                        {
                            if (Cells[i].AllowSelection == true)
                                cells[i] = Cells[i];
                        }

                        return (cells);
                    }
                }

                return (null);
            }
        }

        #endregion

        #region Visible

        /// <summary>
        /// Gets or sets whether the row is visible.
        /// </summary>
        [DefaultValue(true), Category("Appearance")]
        [Description("Indicates whether the row is visible.")]
        public override bool Visible
        {
            get { return (base.Visible); }

            set
            {
                SetState(Rs.Visible, value);

                if (RowFilteredOut == true)
                    value = false;

                if (base.Visible != value)
                {
                    GridPanel panel = GridPanel;

                    if (panel != null)
                    {
                        int index = GridIndex;
                        int count = GetVisibleItemCount() + 1;

                        if (index < panel.Rows.Count)
                            panel.ExpandSelectedAtIndex(index, count, value);

                        if (value == false)
                        {
                            if (panel.ActiveRow == this)
                            {
                                GridContainer row =
                                    panel.GetRowFromIndex(GridIndex + 1) ??
                                    panel.GetRowFromIndex(GridIndex - 1);

                                panel.ActiveRow = row;
                                panel.LastProcessedItem = row;
                            }
                        }

                        panel.UpdateRowCountEx();
                    }

                    base.Visible = value;
                }
            }
        }

        #endregion

        #region WhiteSpaceBounds

        ///<summary>
        /// Gets the row WhiteSpace Bounds (the area
        /// past the end of the last defined column).
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle WhiteSpaceBounds
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    GridColumn lastcol = panel.Columns.LastVisibleColumn;

                    if (lastcol != null)
                    {
                        Rectangle colBounds = lastcol.Bounds;
                        Rectangle panelBounds = panel.Bounds;

                        if (panel.IsSubPanel == true)
                            panelBounds.Width -= 5;

                        if (colBounds.Right < panelBounds.Right)
                        {
                            int left = colBounds.Right;
                            int right = panelBounds.Right;

                            if (left < right)
                            {
                                Rectangle rowBounds = Bounds;

                                rowBounds.X = left;
                                rowBounds.Width = right - left;

                                //rowBounds.Intersect(SViewRect);

                                return (rowBounds);
                            }
                        }
                    }
                }

                return (Rectangle.Empty);
            }
        }

        #endregion

        #endregion

        #region Internal properties

        #region ArrangeLayoutCount

        internal int ArrangeLayoutCount
        {
            get { return (_ArrangeLayoutCount); }
            set { _ArrangeLayoutCount = value; }
        }

        #endregion

        #region DataItemIndex

        internal int DataItemIndex
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    if (panel.VirtualMode == true)
                        return (RowIndex);
                }

                return (_DataItemIndex);
            }

            set { _DataItemIndex = value; }
        }

        #endregion

        #region ExpandButtonBounds

        internal Rectangle ExpandButtonBounds
        {
            get { return (_ExpandButtonBounds); }
            set { _ExpandButtonBounds = value; }
        }

        #endregion

        #region IsReordering

        internal bool IsReordering
        {
            get { return (TestState(Rs.Reordering)); }
            set { SetState(Rs.Reordering, value); }
        }

        #endregion

        #region IsResizing

        internal bool IsResizing
        {
            get { return (TestState(Rs.Resizing)); }
            set { SetState(Rs.Resizing, value); }
        }

        #endregion

        #region IsControlDown

        internal bool IsControlDown
        {
            get { return (TestState(Rs.ControlDown)); }
            set { SetState(Rs.ControlDown, value); }
        }

        #endregion

        #region IsTempInsertRow

        internal bool IsTempInsertRow
        {
            get { return (TestState(Rs.TempInsertRow)); }
            set { SetState(Rs.TempInsertRow, value); }
        }

        #endregion

        #region Loading

        internal bool Loading
        {
            get { return (TestState(Rs.Loading)); }
            set { SetState(Rs.Loading, value); }
        }

        #endregion

        #region MeasureCount

        internal int MeasureCount
        {
            get { return (_MeasureCount); }
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

        #region RowFilteredOut

        internal bool RowFilteredOut
        {
            get { return (TestState(Rs.RowFilteredOut)); }

            set
            {
                SetState(Rs.RowFilteredOut, value);

                base.Visible = (value != true) && TestState(Rs.Visible);

                GridPanel panel = GridPanel;

                if (panel != null)
                    panel.InvalidateLayout();
            }
        }

        #endregion

        #region RowNeedsSorted

        ///<summary>
        /// RowNeedsSorted
        ///</summary>
        internal bool RowNeedsSorted
        {
            get { return (TestState(Rs.RowNeedsSorted)); }
            set { SetState(Rs.RowNeedsSorted, value); }
        }

        #endregion

        #region RowNeedsStored

        ///<summary>
        /// RowNeedsStored
        ///</summary>
        internal bool RowNeedsStored
        {
            get { return (TestState(Rs.RowNeedsStored)); }
            set { SetState(Rs.RowNeedsStored, value); }
        }

        #endregion

        #region IsSeparatorAtBottom

        internal bool IsSeparatorAtBottom
        {
            get { return (TestState(Rs.SeparatorAtBottom)); }
            set { SetState(Rs.SeparatorAtBottom, value); }
        }

        #endregion

        #endregion

        #region TestState

        private bool TestState(Rs state)
        {
            return ((_States & state) == state);
        }

        #endregion

        #region SetState

        private void SetState(Rs state, bool value)
        {
            if (value == true)
                _States |= state;
            else
                _States &= ~state;
        }

        #endregion

        #region MeasureOverride

        #region MeasureOverride

        /// <summary>
        /// Performs the layout of the item
        /// and sets the Size property to size that item will take.
        /// </summary>
        /// <param name="layoutInfo">Layout information.</param>
        /// <param name="stateInfo"></param>
        /// <param name="constraintSize"></param>
        protected override void MeasureOverride(
            GridLayoutInfo layoutInfo, GridLayoutStateInfo stateInfo, Size constraintSize)
        {
            Size sizeNeeded = Size.Empty;

            if (stateInfo.GridPanel.ShowRowHeaders == true)
                sizeNeeded = MeasureRowHeader(stateInfo.GridPanel);

            Size size = Size.Empty;

            if (stateInfo.GridPanel.VirtualMode == true)
            {
                size.Width = GetColumnsWidth(stateInfo);
                size.Height = stateInfo.GridPanel.VirtualRowHeight;
            }
            else
            {
                int rowHeight = GetRowHeight();

                if (rowHeight == 0)
                {
                    size = MeasureCells(layoutInfo, stateInfo);
                }
                else
                {
                    size.Width = GetColumnsWidth(stateInfo);
                    size.Height = rowHeight;
                }
            }

            sizeNeeded.Width += size.Width;
            sizeNeeded.Height = Math.Max(size.Height, sizeNeeded.Height);
            sizeNeeded.Height = Math.Max(sizeNeeded.Height, stateInfo.GridPanel.MinRowHeight);
            sizeNeeded.Height = Math.Min(sizeNeeded.Height, stateInfo.GridPanel.MaxRowHeight);

            _EffectivePreDetailRowHeight = GetRowPreDetailHeight();
            _EffectivePostDetailRowHeight = GetRowPostDetailHeight();

            SuperGrid.DoRowGetDetailHeightEvent(this, layoutInfo,
                sizeNeeded, ref _EffectivePreDetailRowHeight, ref _EffectivePostDetailRowHeight);

            sizeNeeded.Height += (_EffectivePreDetailRowHeight + _EffectivePostDetailRowHeight);

            FixedRowHeight = sizeNeeded.Height;

            HasVisibleItems = false;

            if (Rows != null && Rows.Count > 0)
            {
                if (Expanded == true)
                {
                    GridLayoutStateInfo itemStateInfo = new
                        GridLayoutStateInfo(stateInfo.GridPanel, stateInfo.IndentLevel + 1);

                    size = MeasureSubItems(layoutInfo, itemStateInfo, constraintSize);

                    sizeNeeded.Width = Math.Max(size.Width, sizeNeeded.Width);
                    sizeNeeded.Height += size.Height;
                }
                else
                {
                    HasVisibleItems = AnyVisibleItems();
                }
            }

            Size = sizeNeeded;
        }

        #region GetColumnsWidth

        private int GetColumnsWidth(GridLayoutStateInfo stateInfo)
        {
            int width = 0;

            GridColumnCollection columns = stateInfo.GridPanel.Columns;
            foreach (GridColumn column in columns)
            {
                if (column.Visible == true)
                    width += column.Size.Width;
            }

            return (width);
        }

        #endregion

        #region MeasureRowHeader

        private Size MeasureRowHeader(GridPanel panel)
        {
            Size size = panel.RowHeaderSize;

            if (size.IsEmpty == true)
            {
                if (NeedsMeasured == true)
                {
                    size.Width = panel.RowHeaderWidth;

                    if (GetRowHeight() == 0)
                    {
                        RowHeaderVisualStyle style = GetEffectiveRowStyle().RowHeaderStyle;

                        if (panel.ActiveRowIndicatorStyle == ActiveRowIndicatorStyle.Image ||
                            panel.ActiveRowIndicatorStyle == ActiveRowIndicatorStyle.Both)
                        {
                            MaxImageHeight(style.GetActiveRowImage(panel), ref size);
                        }

                        if (panel.ShowEditingImage == true)
                            MaxImageHeight(style.GetEditingRowImage(panel), ref size);

                        if (panel.ShowInsertRow == true)
                            MaxImageHeight(panel.GetInsertRowImage(), ref size);
                    }

                    _RowHeaderSize = size;
                }

                size = _RowHeaderSize;
            }

            return (size);
        }

        private void MaxImageHeight(Image image, ref Size size)
        {
            if (image != null)
                size.Height = Math.Max(size.Height, image.Height + 6);
        }

        #endregion

        #region AnyVisibleItems

        internal bool AnyVisibleItems()
        {
            GridItemsCollection items = Rows;
            foreach (GridElement item in items)
            {
                if (item.Visible == true)
                    return (true);
            }

            return (false);
        }

        #endregion

        #endregion

        #region MeasureCells

        private Size MeasureCells(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo)
        {
            GridPanel panel = stateInfo.GridPanel;
            Size sizeNeeded = Size.Empty;

            int rowHeight = GetRowHeight();

            GridCellCollection cells = _Cells;
            foreach (GridCell cell in cells)
            {
                if (cell.ColumnIndex < panel.Columns.Count)
                {
                    GridColumn column = panel.Columns[cell.ColumnIndex];

                    if (column.Visible == true)
                    {
                        ColumnAutoSizeMode mode = column.GetAutoSizeMode();
                        Size size = column.BoundsRelative.Size;

                        if ((rowHeight == 0 && mode == ColumnAutoSizeMode.Fill) ||
                            cell.NeedsMeasured == true || column.NeedsMeasured == true ||
                            NeedsMeasured == true || panel.NeedsMeasured == true)
                        {
                            cell.Measure(layoutInfo, stateInfo, size);
                        }

                        size.Height = cell.MeasuredSize.Height;

                        if (mode != ColumnAutoSizeMode.Fill)
                            size.Width = cell.MeasuredSize.Width;

                        if (rowHeight != 0)
                            size.Height = rowHeight;

                        cell.Size = size;

                        sizeNeeded.Width += cell.Size.Width;
                        sizeNeeded.Height = Math.Max(cell.Size.Height, sizeNeeded.Height);
                    }
                }
            }

            if (sizeNeeded.IsEmpty == true)
                sizeNeeded = new Size(100, rowHeight);

            return (sizeNeeded);
        }

        #endregion

        #endregion

        #region ArrangeOverride

        #region ArrangeOverride

        /// <summary>
        /// Performs the arrange pass layout of the
        /// item when final position and size of the item has been set.
        /// </summary>
        /// <param name="layoutInfo">Layout information.</param>
        /// <param name="stateInfo"></param>
        /// <param name="layoutBounds">Layout bounds</param>
        protected override void ArrangeOverride(
            GridLayoutInfo layoutInfo, GridLayoutStateInfo stateInfo, Rectangle layoutBounds)
        {
            ContainerBounds = layoutBounds;

            Rectangle bounds = layoutBounds;
            bounds.Height = FixedRowHeight;

            GridPanel panel = stateInfo.GridPanel;
            IndentLevel = stateInfo.IndentLevel;

            if (panel.ShowRowHeaders == true)
                bounds.X += panel.RowHeaderWidth;

            ArrangeCells(layoutInfo, stateInfo, bounds);

            if (Rows != null && Expanded == true)
            {
                bounds = layoutBounds;

                bounds.Y += FixedRowHeight;
                bounds.Height -= FixedRowHeight;

                GridLayoutStateInfo itemStateInfo =
                    new GridLayoutStateInfo(panel, IndentLevel + 1);

                ArrangeSubItems(layoutInfo, itemStateInfo, bounds);
            }

            ArrangeLayoutCount = SuperGrid.ArrangeLayoutCount;
        }

        #endregion

        #region ArrangeCells

        private void ArrangeCells(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, Rectangle bounds)
        {
            GridPanel panel = stateInfo.GridPanel;

            if (panel.Columns.Count > 0)
            {
                int indentLevel = stateInfo.IndentLevel;

                GridCellCollection cells = _Cells;
                GridColumnCollection columns = panel.Columns;

                int j = 0;

                int[] map = columns.DisplayIndexMap;
                for (int i = 0; i < map.Length; i++)
                {
                    int index = map[i];

                    GridColumn column = columns[index];

                    if (column.Visible == true)
                    {
                        if (index < cells.Count)
                        {
                            GridCell cell = cells[index];

                            if (panel.GroupColumns.Count > 0)
                            {
                                cell.IndentLevel = (j == 0 ? indentLevel : 0);
                            }
                            else
                            {
                                cell.IndentLevel =
                                    (panel.PrimaryColumnIndex == index) ? indentLevel : 0;
                            }

                            bounds.Width = column.Size.Width;

                            cell.Arrange(layoutInfo, stateInfo, bounds);
                        }

                        j++;

                        bounds.X += column.Size.Width;
                    }
                }
            }
        }

        #endregion

        #endregion

        #region RenderOverride

        /// <summary>
        /// Performs drawing of the item and its children
        /// </summary>
        /// <param name="renderInfo">Holds contextual rendering information.</param>
        protected override void RenderOverride(GridRenderInfo renderInfo)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                Rectangle bounds = Bounds;

                GridPanelVisualStyle pstyle = panel.GetEffectiveStyle();

                if (bounds.IntersectsWith(renderInfo.ClipRectangle))
                    RenderRow(renderInfo, panel, pstyle, bounds);

                if (Rows != null && Expanded == true)
                    RenderSubItems(renderInfo, panel, pstyle, panel.IsSubPanel, IsVFrozen);

                RenderDesignerElement(renderInfo, bounds);
            }
        }

        #region RenderRow

        private void RenderRow(GridRenderInfo renderInfo,
            GridPanel panel, GridPanelVisualStyle pstyle, Rectangle r)
        {
            Graphics g = renderInfo.Graphics;
            RenderParts parts = RenderParts.Background;

            SuperGrid.DoPreRenderRowEvent(g, this, parts, r);
            
            if (HScrollOffset == 0 ||
                panel.Parent != null || panel.FrozenColumnCount <= 0)
            {
                RenderAllColumns(renderInfo, panel);
            }
            else
            {
                RenderScrollableColumns(renderInfo, panel);
                RenderFrozenColumns(renderInfo, panel);
            }

            SuperGrid.DoPostRenderRowEvent(g, this, parts, r);

            parts = RenderParts.Border;

            if (panel.ShowRowHeaders == true)
                parts |= RenderParts.RowHeader;

            Rectangle w = WhiteSpaceBounds;

            if (w.Width > 0 && w.Height > 0)
                parts |= RenderParts.Whitespace;

            if (SuperGrid.DoPreRenderRowEvent(g, this, parts, r) == false)
            {
                if (panel.ShowRowHeaders == true)
                    RenderRowHeader(renderInfo, panel, this, pstyle, r);

                if (w.Width > 0 && w.Height > 0)
                    RenderRowWhitespace(renderInfo, panel, pstyle, w);

                SuperGrid.DoPostRenderRowEvent(g, this, parts, r);
            }

            if (panel.FocusCuesEnabled == true &&
                SuperGrid.ActiveElement == this)
            {
                RenderFocusRect(g, r);
            }
        }

        #region RenderAllColumns

        private void RenderAllColumns(
            GridRenderInfo renderInfo, GridPanel panel)
        {
            GridCellCollection cells = _Cells;
            GridColumnCollection columns = panel.Columns;

            bool isVFrozen = IsVFrozen;

            int[] map = columns.DisplayIndexMap;
            for (int i = 0; i < map.Length; i++)
            {
                int index = map[i];

                GridColumn column = columns[index];

                if (column.Bounds.IntersectsWith(renderInfo.ClipRectangle))
                {
                    if (index < cells.Count)
                    {
                        if (RenderCell(renderInfo, column, cells[index], false, isVFrozen) == false)
                            break;
                    }
                    else
                    {
                        RenderEmptyCell(renderInfo, column, false, isVFrozen);
                    }
                }
            }
        }

        #endregion

        #region RenderScrollableColumns

        private void RenderScrollableColumns(
            GridRenderInfo renderInfo, GridPanel panel)
        {
            GridCellCollection cells = _Cells;
            GridColumnCollection columns = panel.Columns;

            bool isVFrozen = IsVFrozen;

            int[] map = columns.DisplayIndexMap;
            for (int i = panel.FrozenColumnCount; i < map.Length; i++)
            {
                int index = map[i];

                if (index < cells.Count)
                {
                    RenderCell(renderInfo,
                        columns[index], cells[index], false, isVFrozen);
                }
                else
                {
                    RenderEmptyCell(renderInfo,
                        columns[index], false, isVFrozen);
                }
            }
        }

        #endregion

        #region RenderFrozenColumns

        private void RenderFrozenColumns(
            GridRenderInfo renderInfo, GridPanel panel)
        {
            GridCellCollection cells = _Cells;
            GridColumnCollection columns = panel.Columns;

            bool isVFrozen = IsVFrozen;

            int[] map = columns.DisplayIndexMap;
            int n = Math.Min(map.Length, panel.FrozenColumnCount);

            for (int i = 0; i < n; i++)
            {
                int index = map[i];

                if (index < cells.Count)
                {
                    RenderCell(renderInfo,
                        columns[index], cells[index], true, isVFrozen);
                }
                else
                {
                    RenderEmptyCell(renderInfo,
                        columns[index], true, isVFrozen);
                }
            }
        }

        #endregion

        #region RenderCell

        private bool RenderCell(GridRenderInfo renderInfo,
            GridColumn column, GridCell cell, bool isHFrozen, bool isVFrozen)
        {
            if (column.Visible == true)
            {
                Rectangle bounds = cell.BoundsRelative;

                if (isHFrozen == false)
                    bounds.X -= HScrollOffset;

                if (isVFrozen == false)
                    bounds.Y -= VScrollOffset;

                if (bounds.X > renderInfo.ClipRectangle.Right)
                    return (false);

                cell.Render(renderInfo);
            }

            return (true);
        }

        #endregion

        #region RenderEmptyCell

        private void RenderEmptyCell(GridRenderInfo renderInfo,
            GridColumn column, bool isHFrozen, bool isVFrozen)
        {
            if (column.Visible == true)
            {
                Rectangle r = column.BoundsRelative;
                r.Y = BoundsRelative.Y;
                r.Height = FixedRowHeight;

                Rectangle bounds = r;

                if (isHFrozen == false)
                    bounds.X -= HScrollOffset;

                if (isVFrozen == false)
                {
                    bounds.Y -= VScrollOffset;
                    bounds.Intersect(SViewRect);
                }

                if (bounds.IntersectsWith(renderInfo.ClipRectangle))
                {
                    GridCell cell = GetEmptyRenderCell(column);

                    cell.IsMouseOver =
                        bounds.Contains(SuperGrid.PointToClient(Control.MousePosition));

                    cell.Render(renderInfo);
                }
            }
        }

        #region GetEmptyRenderCell

        private GridCell GetEmptyRenderCell(GridColumn column)
        {
            if (_EmptyRenderCell == null)
                _EmptyRenderCell = new GridCell();

            int oldIndex = _EmptyRenderCell.ColumnIndex;

            _EmptyRenderCell = GetEmptyCell(_EmptyRenderCell, column.ColumnIndex);

            if (_EmptyRenderCell.ColumnIndex != oldIndex)
                _EmptyRenderCell.ClearEffectiveStyles();

            return (_EmptyRenderCell);
        }

        #endregion

        #endregion

        #region RenderFocusRect

        private void RenderFocusRect(Graphics g, Rectangle r)
        {
            if (SuperGrid.Focused == true && IsDesignerHosted == false)
            {
                GridCell cell = FirstVisibleCell;

                if (cell != null)
                {
                    Rectangle v = cell.HighLightBounds;

                    if (v.X > r.X)
                    {
                        int n = v.X - r.X;

                        r.X = v.X;
                        r.Width -= n;
                    }
                }
                    
                r.Height = FixedRowHeight;

                switch (GridPanel.GridLines)
                {
                    case GridLines.Both:
                        r.Width--;
                        r.Height--;
                        break;

                    case GridLines.Vertical:
                        r.Width--;
                        break;

                    case GridLines.Horizontal:
                        r.Height--;
                        break;
                }

                if (IsVFrozen == false)
                {
                    Rectangle t = SViewRect;
                    t.Width -= 1;
                    t.Height -= 1;

                    r.Intersect(t);
                }

                ControlPaint.DrawFocusRectangle(g, r);
            }
        }

        #endregion

        #endregion

        #region RenderRowHeader

        protected void RenderRowHeader(GridRenderInfo renderInfo,
            GridPanel panel, GridContainer item, GridPanelVisualStyle pstyle, Rectangle r)
        {
            Graphics g = renderInfo.Graphics;

            r.Width = panel.RowHeaderWidth;

            if (r.IntersectsWith(renderInfo.ClipRectangle))
            {
                if (r.Height > 100)
                    r = GetCenterBounds(r);

                RenderRowHeader(g, panel, item, pstyle, r, true);
            }
        }

        #region RenderRowHeader

        private void RenderRowHeader(Graphics g, GridPanel panel,
            GridContainer item, GridPanelVisualStyle pstyle, Rectangle r, bool highlight)
        {
            r.Width--;
            r.Height--;

            if (r.Width > 0 && r.Height > 0)
            {
                StyleState rowState = GetRowHeaderState(item, r);
                RowVisualStyle style = GetEffectiveRowStyle(item, rowState);

                if (SuperGrid.DoPreRenderRowEvent(g, this, RenderParts.RowHeader, r) == false)
                {
                    if (item.IsActiveRow == true &&
                        (panel.ActiveRowIndicatorStyle == ActiveRowIndicatorStyle.Highlight ||
                         panel.ActiveRowIndicatorStyle == ActiveRowIndicatorStyle.Both))
                    {
                        using (Brush br = style.RowHeaderStyle.ActiveRowBackground.GetBrush(r))
                            g.FillRectangle(br, r);
                    }
                    else
                    {
                        using (Brush br = style.RowHeaderStyle.Background.GetBrush(r))
                            g.FillRectangle(br, r);
                    }

                    if (highlight == true)
                    {
                        using (Pen pen = new Pen(style.RowHeaderStyle.BorderHighlightColor))
                        {
                            g.DrawLine(pen, r.X + 1, r.Top, r.Right - 1, r.Top);
                            g.DrawLine(pen, r.X + 1, r.Top, r.X + 1, r.Bottom);
                        }

                        using (Pen pen = new Pen(pstyle.HeaderLineColor))
                        {
                            g.DrawLine(pen, r.X, r.Top - 1, r.Right, r.Top - 1);
                            g.DrawLine(pen, r.X, r.Bottom, r.Right - 1, r.Bottom);
                            g.DrawLine(pen, r.Right, r.Top, r.Right, r.Bottom);
                        }
                    }

                    if (item is GridRow)
                    {
                        if (RowDirty == true && panel.ShowRowDirtyMarker == true)
                        {
                            Rectangle t = r;

                            t.X++;
                            t.Width = 3;


                            using (Brush br = style.RowHeaderStyle.DirtyMarkerBackground.GetBrush(t))
                                g.FillRectangle(br, t);
                        }
                    }

                    r.Width = panel.RowHeaderWidth;

                    RenderHeaderInfo(g, panel, item, style, r);

                    SuperGrid.DoPostRenderRowEvent(g, this, RenderParts.RowHeader, r);
                }
            }
        }

        #region GetRowHeaderState

        private StyleState GetRowHeaderState(
            GridContainer item, Rectangle r)
        {
            StyleState rowState = StyleState.Default;

            if (IsMouseOver == true)
            {
                r.Height++;

                if (IsResizing == true ||
                    r.Contains(SuperGrid.PointToClient(Control.MousePosition)) == true)
                {
                    rowState |= StyleState.MouseOver;
                }
            }

            if (item.IsSelected == true)
                rowState |= StyleState.Selected;

            return (rowState);
        }

        #endregion

        #endregion

        #region RenderHeaderInfo

        private void RenderHeaderInfo(Graphics g,
            GridPanel panel, GridContainer item, RowVisualStyle style, Rectangle r)
        {
            r.X += 4;
            r.Width -= 4;

            GridRow row = item as GridRow;

            int m = RenderInfoImage(g, panel, item, r);

            if (row != null && row.IsInsertRow == true)
            {
                RenderInsertIndicator(g, panel, r);
                RenderIndicatorImage(g, panel, item, r);
            }
            else
            {
                if (panel.ShowEditingImage == true &&
                    (row != null && row.EditorDirty == true))
                {
                    RenderEditingImage(g, panel, r);
                }
                else
                {
                    int n = RenderIndicatorImage(g, panel, item, r);

                    r.X += n;
                    r.Width -= n;

                    string text = item.RowHeaderText;

                    if (string.IsNullOrEmpty(text) == true)
                    {
                        text = (panel.ShowRowGridIndex == true)
                           ? (item.GridIndex + panel.RowHeaderIndexOffset).ToString() : "";
                    }

                    SuperGrid.DoGetRowHeaderTextEvent(item, ref text);

                    if (string.IsNullOrEmpty(text) == false)
                        RenderGridIndex(g, text, style, r, m);
                }
            }
        }

        #region RenderInfoImage

        private int RenderInfoImage(
            Graphics g, GridPanel panel, GridContainer item, Rectangle r)
        {
            GridRow row = item as GridRow;

            if (row != null)
            {
                row.InfoImageBounds = Rectangle.Empty;

                if (panel.ShowRowInfo == true &&
                    string.IsNullOrEmpty(row.InfoText) == false)
                {
                    StyleState rowState = GetRowHeaderState(this, r);
                    RowHeaderVisualStyle style = GetEffectiveRowStyle(this, rowState).RowHeaderStyle;

                    Image image = style.GetInfoRowImage(panel);

                    if (image != null)
                    {
                        Rectangle u = r;

                        u.Size = image.Size;
                        u.X = r.Right - u.Width - 4;
                        u.X = Math.Max(r.X, u.X);

                        if (r.Height > u.Height)
                            u.Y += (r.Height - u.Height)/2;

                        u.Intersect(r);

                        row.InfoImageBounds = u;

                        g.DrawImageUnscaledAndClipped(image, u);

                        return (image.Width);
                    }
                }
            }

            return (0);
        }

        #endregion

        #region RenderEditingImage

        private void RenderEditingImage(
            Graphics g, GridPanel panel, Rectangle r)
        {
            StyleState rowState = GetRowHeaderState(this, r);
            RowHeaderVisualStyle style = GetEffectiveRowStyle(this, rowState).RowHeaderStyle;

            Image image = style.GetEditingRowImage(panel);

            if (image != null)
            {
                Rectangle u = r;
                u.Size = image.Size;

                if (r.Width > u.Width)
                    u.X += (r.Width - u.Width) / 2;

                if (r.Height > u.Height)
                    u.Y += (r.Height - u.Height) / 2;

                u.Intersect(r);

                g.DrawImageUnscaledAndClipped(image, u);
            }
        }

        #endregion

        #region RenderInsertIndicator

        private void RenderInsertIndicator(
            Graphics g, GridPanel panel, Rectangle r)
        {
            Image image = panel.GetInsertRowImage();

            if (image != null)
            {
                Rectangle u = r;
                u.Size = image.Size;

                if (r.Width > u.Width)
                    u.X = r.X + (r.Width - u.Width) / 2;

                if (r.Height > u.Height)
                    u.Y += (r.Height - u.Height) / 2;

                u.Intersect(r);

                g.DrawImageUnscaledAndClipped(image, u);
            }
        }

        #endregion

        #region RenderIndicatorImage

        private int RenderIndicatorImage(Graphics g,
            GridPanel panel, GridContainer row, Rectangle r)
        {
            StyleState rowState = GetRowHeaderState(this, r);
            RowHeaderVisualStyle style = GetEffectiveRowStyle(this, rowState).RowHeaderStyle;

            Image image = null;
            
            GridRow grow = row as GridRow;
            
            if (grow != null)
            {
                if (grow.IsTempInsertRow == true && grow.IsInsertRow == false)
                    image = panel.GetInsertTempRowImage();
                else
                    image = style.GetActiveRowImage(panel);
            }

            if (image != null)
            {
                if (row.IsActiveRow == true)
                {
                    if (panel.ActiveRowIndicatorStyle == ActiveRowIndicatorStyle.Image ||
                        panel.ActiveRowIndicatorStyle == ActiveRowIndicatorStyle.Both)
                    {
                        Rectangle u = r;
                        u.Size = image.Size;

                        if (r.Height > u.Height)
                            u.Y += (r.Height - u.Height) / 2;

                        u.Intersect(r);

                        g.DrawImageUnscaledAndClipped(image, u);
                    }
                }

                return (image.Width);
            }

            return (0);
        }

        #endregion

        #region RenderGridIndex

        private void RenderGridIndex(Graphics g,
            string text, RowVisualStyle style, Rectangle r, int m)
        {
            Font font = style.RowHeaderStyle.Font ?? SystemFonts.DefaultFont;

            r.Inflate(-2, 0);
            r.Width -= m;

            eTextFormat tf = style.RowHeaderStyle.GetTextFormatFlags();

            TextDrawing.DrawString(g, text,
                font, style.RowHeaderStyle.TextColor, r, tf);
        }

        #endregion

        #endregion

        #endregion

        #region RenderRowWhitespace

        private void RenderRowWhitespace(GridRenderInfo renderInfo,
            GridPanel panel, GridPanelVisualStyle pstyle, Rectangle r)
        {
            Graphics g = renderInfo.Graphics;

            StyleState state = GetWhiteSpaceState(panel);
            RowVisualStyle style = GetEffectiveRowStyle(this, state);

            using (Brush br = style.Background.GetBrush(r))
                g.FillRectangle(br, r);

            if (panel.ShowWhitespaceRowLines == true)
            {
                switch (panel.GridLines)
                {
                    case GridLines.Both:
                    case GridLines.Horizontal:
                        if (pstyle.HorizontalLinePattern != LinePattern.None &&
                            pstyle.HorizontalLinePattern != LinePattern.NotSet)
                        {
                            using (Pen pen = new Pen(pstyle.HorizontalLineColor))
                            {
                                pen.DashStyle = (DashStyle) pstyle.HorizontalLinePattern;

                                g.DrawLine(pen, r.Left, r.Bottom - 1, r.Right - 1, r.Bottom - 1);
                                g.DrawLine(pen, r.Left, r.Top - 1, r.Right - 1, r.Top - 1);
                            }
                        }
                        break;
                }
            }
        }

        #region GetWhiteSpaceState

        private StyleState GetWhiteSpaceState(GridPanel panel)
        {
            StyleState rowState = StyleState.Default;

            if (IsMouseOver == true && _HitArea == RowArea.InWhiteSpace)
                rowState |= StyleState.MouseOver;

            if (IsSelected == true)
                rowState |= StyleState.Selected;

            if (panel.SelectionGranularity != SelectionGranularity.Cell &&
                panel.RowHighlightType == RowHighlightType.Full)
            {
                if (Bounds.Contains(SuperGrid.PointToClient(Control.MousePosition)))
                    rowState |= StyleState.MouseOver;
            }

            return (rowState);
        }

        #endregion

        #endregion

        #region RenderSubItems

        protected void RenderSubItems(GridRenderInfo renderInfo,
            GridPanel panel, GridPanelVisualStyle pstyle, bool isSubPanel, bool isFrozen)
        {
            Rectangle t = SViewRect;

            GridItemsCollection items = Rows;
            for (int i = FirstOnScreenRowIndex; i < items.Count; i++)
            {
                GridElement item = items[i];

                if (item.Visible == true)
                {
                    GridPanel ipanel = item as GridPanel;

                    if (ipanel != null)
                    {
                        Graphics g = renderInfo.Graphics;
                        Rectangle r = ipanel.ContainerBounds;

                        if (isFrozen == false)
                            r.Y -= VScrollOffset;

                        if (isSubPanel == true)
                            r.X -= HScrollOffset;

                        if (r.Y > t.Bottom)
                            break;

                        if (r.IntersectsWith(renderInfo.ClipRectangle))
                        {
                            Rectangle r2 = r;

                            if (panel.ShowRowHeaders == true)
                            {
                                r2.X += panel.RowHeaderWidth;
                                r2.Width -= panel.RowHeaderWidth;
                            }

                            if (SuperGrid.DoPreRenderPanelRowEvent(g,
                                ipanel, RenderParts.Background | RenderParts.Border, r2) == false)
                            {
                                RenderRowBackground(g, ipanel, r2);
                                RenderRowBorder(g, panel, r2);

                                SuperGrid.DoPostRenderPanelRowEvent(g,
                                    ipanel, RenderParts.Background | RenderParts.Border, r2);
                            }

                            RenderTreeLines(g, panel, ipanel, r, isSubPanel);
                            RenderRowCheckBox(g, panel, ipanel);

                            item.Render(renderInfo);

                            if (panel.ShowRowHeaders == true)
                            {
                                r2 = r;
                                r2.Width = panel.RowHeaderWidth;

                                if (SuperGrid.DoPreRenderPanelRowEvent(g,
                                    ipanel, RenderParts.RowHeader, r2) == false)
                                {
                                    RenderRowHeader(renderInfo, panel, ipanel, pstyle, r2);

                                    SuperGrid.DoPostRenderPanelRowEvent(g,
                                        ipanel, RenderParts.RowHeader, r2);
                                }
                            }
                        }
                    }
                    else
                    {
                        Rectangle r = item.BoundsRelative;

                        if (isFrozen == false)
                            r.Y -= VScrollOffset;

                        if (isSubPanel == true)
                            r.X -= HScrollOffset;

                        if (r.Y > t.Bottom)
                            break;

                        if (r.IntersectsWith(renderInfo.ClipRectangle))
                            item.Render(renderInfo);
                    }
                }
            }
        }

        #endregion

        #region RenderTreeLines

        private void RenderTreeLines(Graphics g,
            GridPanel panel, GridPanel ipanel, Rectangle r, bool isSubPanel)
        {
            if (panel.ShowTreeButtons == true && panel.ShowTreeLines == true)
            {
                if (panel.PrimaryColumn != null &&
                    panel.PrimaryColumn.Visible == true)
                {
                    int n = panel.PrimaryColumn.BoundsRelative.X -
                            panel.BoundsRelative.X;

                    r.X += n;
                    r.Width -= n;

                    r = GetCenterBounds(r);

                    if (panel.PrimaryColumn.IsHFrozen == false)
                    {
                        if (isSubPanel == false)
                            r.X -= HScrollOffset;
                    }

                    TreeDisplay.RenderLines(g, panel, r, ipanel, ipanel.IndentLevel,
                        panel.LevelIndentSize.Width, panel.TreeButtonIndent, false);
                }
            }
        }

        #endregion

        #region RenderRowBackground

        protected void RenderRowBackground(
            Graphics g, GridContainer item, Rectangle r)
        {
            if (item is GridPanel)
            {
                RowVisualStyle style = GetEffectiveRowStyle(item);

                if (style.Background != null && style.Background.IsEmpty == false)
                {
                    Rectangle t = item.ContainerBounds;

                    t.X -= HScrollOffset;

                    if (IsVFrozen == false)
                    {
                        t.Y -= VScrollOffset;

                        r.Intersect(SViewRect);
                        t.Intersect(SViewRect);
                        t.Y = r.Y;
                    }

                    using (Brush br = style.Background.GetBrush(t))
                        g.FillRectangle(br, r);
                }
            }
        }

        #endregion

        #region RenderRowCheckBox

        protected void RenderRowCheckBox(Graphics g,
            GridPanel panel, GridPanel ipanel)
        {
            if (ipanel.HasCheckBox == true)
            {
                Rectangle r = GetRowCheckBoxBounds(panel, ipanel);

                if (ipanel.Checked == true)
                {
                    CheckDisplay.RenderCheckbox(g, r,
                        CheckBoxState.CheckedNormal, ButtonState.Checked);
                }
                else
                {
                    CheckDisplay.RenderCheckbox(g, r,
                        CheckBoxState.UncheckedNormal, ButtonState.Normal);
                }
            }
        }

        #endregion

        #region GetRowCheckBoxBounds

        private Rectangle GetRowCheckBoxBounds(GridPanel panel, GridPanel ipanel)
        {
            Rectangle r = ipanel.CheckBoxBounds;

            if (ipanel.HasCheckBox == true)
            {
                Rectangle bounds = ipanel.ContainerBounds;
                bounds.Location = PointToScroll(ipanel, bounds.Location);

                Rectangle t = GetCenterBounds(bounds);

                r.Y = t.Y + (t.Height - panel.CheckBoxSize.Height) / 2;
                r.X -= HScrollOffset;

                if (r.Bottom > bounds.Bottom)
                    r.Y -= (r.Bottom - bounds.Bottom);

                if (r.Y < bounds.Y)
                    r.Y = bounds.Y;

                t = r;
                t.X += HScrollOffset;

                if (ipanel.IsVFrozen == false)
                    t.Y += VScrollOffset;

                ipanel.CheckBoxBounds = t;
            }
            else
            {
                r = Rectangle.Empty;
                ipanel.CheckBoxBounds = r;
            }

            return (r);
        }

        #endregion

        #region RenderRowBorder

        protected void RenderRowBorder(
            Graphics g, GridPanel panel, Rectangle r)
        {
            if (panel.GridLines == GridLines.Horizontal || panel.GridLines == GridLines.Both)
            {
                if (panel.ShowWhitespaceRowLines == true)
                {
                    GridPanelVisualStyle pstyle = panel.GetEffectiveStyle();

                    using (Pen pen = new Pen(pstyle.HorizontalLineColor))
                    {
                        pen.DashStyle = (DashStyle)pstyle.HorizontalLinePattern;
                        pen.DashOffset = r.X % 2;

                        g.DrawLine(pen, r.X, r.Top - 1, r.Right - 1, r.Top - 1);
                        g.DrawLine(pen, r.X, r.Bottom - 1, r.Right - 1, r.Bottom - 1);
                    }
                }
            }
        }

        #endregion

        #region RenderDesignerElement

        private void RenderDesignerElement(
            GridRenderInfo renderInfo, Rectangle r)
        {
            if (SuperGrid.DesignerElement == this)
            {
                r.X -= 2;
                r.Y++;
                r.Height -= 4;
                r.Width += 5;

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

        #region GetCenterBounds

        private Rectangle GetCenterBounds(Rectangle bounds)
        {
            Rectangle r = SuperGrid.ClientRectangle;

            int m = r.Y;

            if (IsVFrozen == false)
                m += SuperGrid.PrimaryGrid.FixedRowHeight;

            if (bounds.Y < m)
            {
                int n = m - bounds.Y;

                bounds.Y = m;
                bounds.Height -= n;
            }

            m = r.Bottom;

            if (SuperGrid.IsHScrollBarVisible == true)
                m -= SuperGrid.HScrollBarHeight;

            if (SuperGrid.PrimaryGrid.Footer != null &&
                SuperGrid.PrimaryGrid.Footer.Visible == true)
            {
                m -= SuperGrid.PrimaryGrid.Footer.Size.Height;
            }

            if (bounds.Bottom > m)
                bounds.Height -= (bounds.Bottom - m) - 1;

            return (bounds);
        }

        #endregion

        #region FindCell

        private GridCell FindCell(string columnName)
        {
            if (string.IsNullOrEmpty(columnName) == true)
                throw new Exception("Invalid Column Name.");

            foreach (GridCell cell in Cells)
            {
                if (columnName.Equals(cell.GridColumn.Name) == true)
                    return (cell);
            }

            return (null);
        }

        #endregion

        #region GetCell

        ///<summary>
        /// Gets the Row Cell for the given column index. If
        /// the cell does not exist,then null will be returned.
        ///</summary>
        ///<param name="columnIndex"></param>
        ///<returns></returns>
        public GridCell GetCell(int columnIndex)
        {
            return (GetCell(columnIndex, false));
        }

        ///<summary>
        /// Gets the Row Cell for the given column name. If
        /// the cell does not exist, null will be returned.
        ///</summary>
        ///<param name="name"></param>
        ///<returns></returns>
        public GridCell GetCell(string name)
        {
            return (FindCell(name));
        }

        ///<summary>
        /// Gets the Row Cell for the given column index. If
        /// "includeEmpty" is true, and the cell does not exist
        /// (i.e. is empty) then a dummy empty cell will be created
        /// and returned.
        ///</summary>
        ///<param name="columnIndex"></param>
        ///<param name="includeEmpty"></param>
        ///<returns></returns>
        public GridCell GetCell(int columnIndex, bool includeEmpty)
        {
            if (columnIndex < _Cells.Count)
                return (_Cells[columnIndex]);

            if (includeEmpty == true)
                return (GetEmptyCell(null, columnIndex));

            return (null);
        }

        #endregion

        #region GetEmptyCell

        internal GridCell GetEmptyCell(GridCell cell, int columnIndex)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                if (columnIndex < panel.Columns.Count)
                {
                    GridColumn column = panel.Columns[columnIndex];

                    if (cell == null)
                        cell = new GridCell();

                    Rectangle r = column.BoundsRelative;
                    r.Y = BoundsRelative.Y;
                    r.Height = FixedRowHeight;

                    cell.BoundsRelative = r;

                    cell.Parent = this;
                    cell.IsEmptyCell = true;
                    cell.ColumnIndex = columnIndex;
                    cell.SelectionUpdateCount = GridPanel.SelectionUpdateCount - 1;

                    return (cell);
                }
            }

            return (cell);
        }

        #endregion

        #region Mouse Handling

        #region InternalMouseEnter

        /// <summary>
        /// InternalMouseEnter
        /// </summary>
        /// <param name="e"></param>
        internal override void InternalMouseEnter(EventArgs e)
        {
            IsMouseOver = true;

            if (GridPanel.RowHighlightType == RowHighlightType.Full)
                InvalidateRender(Bounds);

            SuperGrid.DoRowMouseEnterEvent(this);

            base.InternalMouseEnter(e);
        }

        #region InvalidateWhitespace

        private void InvalidateWhitespace()
        {
            Rectangle r = WhiteSpaceBounds;

            if (r.IsEmpty == false)
                InvalidateRender(r);
        }

        #endregion

        #endregion

        #region InternalMouseLeave

        /// <summary>
        /// InternalMouseLeave
        /// </summary>
        /// <param name="e"></param>
        internal override void InternalMouseLeave(EventArgs e)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                IsMouseOver = false;

                if (GridPanel.RowHighlightType == RowHighlightType.Full)
                    InvalidateRender(Bounds);

                if (panel.ShowRowHeaders == true)
                {
                    panel.HotItem = null;

                    _HitArea = RowArea.NoWhere;
                    _HitItem = null;
                }

                //_MouseDownHitRow = null;
                //_MouseDownHitArea = RowArea.NoWhere;

                SuperGrid.ToolTip.Hide(SuperGrid);

                if (_LastArea == RowArea.InRowInfo)
                    ProcessRowInfoLeave();
            }

            CancelCapture();

            base.InternalMouseLeave(e);

            SuperGrid.DoRowMouseLeaveEvent(this);
        }

        #endregion

        #region InternalMouseMove

        /// <summary>
        /// InternalMouseMove
        /// </summary>
        /// <param name="e"></param>
        internal override void InternalMouseMove(MouseEventArgs e)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                if (IsMouseDown == true && _MouseDownHitRow != null)
                    ProcessMouseDownMove(e, panel);

                ProcessMouseMove(e, panel);
            }

            SuperGrid.DoRowMouseMoveEvent(this, e, _HitArea);

            base.InternalMouseMove(e);
        }

        #region ProcessMouseDownMove

        private void ProcessMouseDownMove(MouseEventArgs e, GridPanel panel)
        {
            if (_MouseDownHitRow != null)
            {
                Rectangle r = ViewRect;

                if (MouseDownPoint.Y < r.Y)
                {
                    int n = SuperGrid.PrimaryGrid.FixedRowHeight -
                            SuperGrid.PrimaryGrid.FixedHeaderHeight;

                    r.Y -= n;
                    r.Height += n;
                }

                VScrollBarAdv vsb = SuperGrid.VScrollBar;

                if ((e.Y >= r.Y) && 
                    (e.Y < r.Bottom || (vsb.Value >= vsb.Maximum - vsb.LargeChange)))
                {
                    SuperGrid.DisableAutoScrolling();

                    switch (_MouseDownHitArea)
                    {
                        case RowArea.InRowHeader:
                            panel.HotItem = _HitItem;
                            ProcessInRowHeader(panel, e);
                            break;

                        case RowArea.InRowResize:
                            ProcessInRowResize(panel, e.Location);
                            break;

                        case RowArea.InContent:
                        case RowArea.InWhiteSpace:
                            ProcessInContent(panel, e.Location);
                            break;
                    }
                }
                else
                {
                    SuperGrid.EnableAutoScrolling(AutoScrollEnable.Vertical, r);

                    StopResize();
                    StopReorder();
                }
            }
        }

        #region ProcessInContent

        private void ProcessInContent(GridPanel panel, Point pt)
        {
            GridContainer container = _MouseDownHitRow.Parent as GridContainer;

            if (container != null)
            {
                if (panel.SelectionGranularity != SelectionGranularity.Cell)
                {
                    _HitItem = GetRowAt(panel, pt);

                    if (_HitItem != null)
                    {
                        if (panel.MultiSelect == true)
                        {
                            if (_HitItem != panel.LastProcessedItem)
                                ProcessExtendSelection(panel, true);
                        }
                    }
                }
            }
        }

        #endregion

        #endregion

        #region ProcessMouseMove

        private void ProcessMouseMove(MouseEventArgs e, GridPanel panel)
        {
            _HitArea = GetHitArea(e.Location);
            
            if (IsMouseDown == false)
            {
                switch (_HitArea)
                {
                    case RowArea.InRowHeader:
                        panel.HotItem = _HitItem;
                        SuperGrid.GridCursor = Cursors.Default;
                        break;

                    case RowArea.InRowCheckBox:
                    case RowArea.InCellCheckBox:
                    case RowArea.InCellExpand:
                        panel.HotItem = null;
                        SuperGrid.GridCursor = Cursors.Hand;
                        break;

                    case RowArea.InRowResize:
                        panel.HotItem = _HitItem;
                        SuperGrid.GridCursor = Cursors.HSplit;
                        break;

                    default:
                        panel.HotItem = null;
                        SuperGrid.GridCursor = Cursors.Default;
                        break;
                }
            }
            else
            {
                if ((Control.MouseButtons & MouseButtons.Left) != MouseButtons.Left)
                    SuperGrid.GridCursor = Cursors.Default;
            }

            if (_HitArea != _LastArea)
            {
                if (_HitArea == RowArea.InRowInfo)
                    ProcessRowInfoEnter(e);

                else if (_LastArea == RowArea.InRowInfo)
                    ProcessRowInfoLeave();

                if (_HitArea == RowArea.InWhiteSpace || _LastArea == RowArea.InWhiteSpace)
                {
                    if (panel.RowHighlightType == RowHighlightType.Full)
                        InvalidateRender(Bounds);
                    else
                        InvalidateWhitespace();
                }

                _LastArea = _HitArea;
            }
        }

        #region ProcessRowInfoEnter

        private void ProcessRowInfoEnter(MouseEventArgs e)
        {
            if (SuperGrid.DoRowInfoEnterEvent(this, e.Location) == false)
                SuperGrid.ToolTipText = InfoText;
        }

        #endregion

        #region ProcessRowInfoLeave

        private void ProcessRowInfoLeave()
        {
            SuperGrid.ToolTipText = "";
            SuperGrid.DoRowInfoLeaveEvent(this);
        }

        #endregion

        #endregion

        #endregion

        #region InternalMouseDown

        /// <summary>
        /// InternalMouseDown
        /// </summary>
        /// <param name="e"></param>
        internal override void InternalMouseDown(MouseEventArgs e)
        {
            GridPanel panel = GridPanel;

            RowArea hitArea = _HitArea;
            GridContainer hitItem = _HitItem;

            if (panel != null && hitItem != null)
            {
                SuperGrid.DoRowMouseDownEvent(this, e, hitArea);

                if (_HitArea == RowArea.InRowResize)
                {
                    _MouseDownHitArea = _HitArea;
                    _MouseDownHitRow = _HitItem;
                    _MouseDownPoint = e.Location;

                    InitRowResize(e);
                    return;
                }

                if (CanSetActiveRow(panel, hitItem, true) == true)
                {
                    _HitItem = hitItem;
                    _HitArea = hitArea;

                    int rowIndex = RowIndex;

                    if (panel.SetActiveRow(_HitItem, true) == true)
                    {
                        if (_HitArea != RowArea.InContent)
                            panel.DeactivateEdit();

                        if (rowIndex > RowIndex)
                        {
                            SuperGrid.ArrangeGrid();

                            GridContainer row = panel.GetElementAt(e) as GridContainer;

                            if (row != null)
                            {
                                row.InternalMouseMove(e);
                                row.InternalMouseDown(e);
                                return;
                            }
                        }

                        _MouseDownHitArea = _HitArea;
                        _MouseDownHitRow = _HitItem;
                        _MouseDownPoint = e.Location;

                        _dragSelection = false;
                        _dragStarted = false;

                        switch (_HitArea)
                        {
                            case RowArea.InRowHeader:
                                panel.LastProcessedItem = _HitItem;

                                switch (panel.RowDragBehavior)
                                {
                                    case RowDragBehavior.Move:
                                    case RowDragBehavior.GroupMove:
                                        InitExtendSelection(panel);

                                        if ((Control.ModifierKeys & (Keys.Control | Keys.Shift)) == 0)
                                            InitRowMove(panel);
                                        break;

                                    case RowDragBehavior.ExtendSelection:
                                        if (IsSelected == false ||
                                            ((Control.ModifierKeys & (Keys.Control | Keys.Shift)) != Keys.None))
                                        {
                                            InitExtendSelection(panel);
                                        }
                                        else if (IsMouseDown == true)
                                        {
                                            Capture = true;

                                            _dragSelection = true;
                                        }
                                        break;
                                }
                                return;

                            case RowArea.InContent:
                                if (_HitItem is GridPanel ||
                                    panel.SelectionGranularity != SelectionGranularity.Cell)
                                {
                                    InitExtendSelection(panel);
                                }
                                break;

                            case RowArea.InWhiteSpace:
                                panel.LastProcessedItem = _HitItem;

                                switch (panel.RowWhitespaceClickBehavior)
                                {
                                    case RowWhitespaceClickBehavior.ClearSelection:
                                        panel.ClearAll();
                                        break;

                                    case RowWhitespaceClickBehavior.ExtendSelection:
                                        InitExtendSelection(panel);
                                        break;
                                }
                                return;

                            case RowArea.InCellExpand:
                                panel.LastProcessedItem = _HitItem;

                                ProcessInExpandButton();

                                if (panel.SelectionGranularity != SelectionGranularity.Cell)
                                    return;
                                break;
                        }
                    }
                    else
                    {
                        _MouseDownHitArea = RowArea.NoWhere;
                        _MouseDownHitRow = null;
                    }

                    base.InternalMouseDown(e);
                }
            }
        }

        #endregion

        #region InternalMouseUp

        /// <summary>
        /// InternalMouseUp
        /// </summary>
        /// <param name="e"></param>
        internal override void InternalMouseUp(MouseEventArgs e)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                SuperGrid.DisableAutoScrolling();
                
                if (SuperGrid.DoRowClickEvent(this, _MouseDownHitArea, e) == false)
                {
                    switch (_MouseDownHitArea)
                    {
                        case RowArea.InRowCheckBox:
                            ProcessInCheckBox(panel, _HitItem);
                            break;

                        case RowArea.InRowResize:
                            if (IsResizing == true)
                            {
                                StopResize();

                                if (panel.ImmediateResize == false)
                                {
                                    ResizeRow(panel, e.Location.Y);
                                    PostInternalMouseMove();
                                }
                            }
                            break;

                        case RowArea.InRowHeader:
                            if (InfoImageBounds.Contains(e.X, e.Y) == true)
                                SuperGrid.DoRowInfoClickEvent(this, e);

                            if (IsReordering == true)
                            {
                                StopReorder();
                                ReorderRow(panel);

                                PostInternalMouseMove();
                            }
                            else
                            {
                                if (_dragSelection == true)
                                    ExtendSelectionEx(panel);

                                panel.ColumnHeader.InvalidateRowHeader();
                            }

                            SuperGrid.DoRowHeaderClickEvent(panel, this, e);
                            break;

                        case RowArea.InCellCheckBox:
                            ProcessInCheckBox(panel, this);
                            break;
                    }
                }
            }

            _MouseDownHitRow = null;

            base.InternalMouseUp(e);

            SuperGrid.DoRowMouseUpEvent(this, e, _MouseDownHitArea);
        }

        #endregion

        #region InternalMouseDoubleClick

        internal override void InternalMouseDoubleClick(MouseEventArgs e)
        {
            if (SuperGrid.EditorCell == null)
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    if (SuperGrid.DoRowDoubleClickEvent(this, _MouseDownHitArea, e) == false)
                    {
                        switch (_MouseDownHitArea)
                        {
                            case RowArea.InRowHeader:
                                SuperGrid.DoRowHeaderDoubleClickEvent(this, e);
                                break;

                            case RowArea.InRowInfo:
                                SuperGrid.DoRowInfoDoubleClickEvent(this, e);
                                break;

                            case RowArea.InRowResize:
                                int n = GetMaximumRowHeight();

                                if (panel.VirtualMode == true)
                                    panel.VirtualRowHeight = n;
                                else
                                    RowHeight = n;

                                StopResize();
                                break;

                            case RowArea.InContent:
                                if (panel.RowDoubleClickBehavior == RowDoubleClickBehavior.Activate)
                                {
                                    if (MouseActivateCell(e, panel) == true)
                                        return;
                                }
                                else
                                {
                                    Expanded = !Expanded;
                                }
                                break;
                        }
                    }
                }

                base.InternalMouseDoubleClick(e);
            }
        }

        #region MouseActivateCell

        private bool MouseActivateCell(MouseEventArgs e, GridPanel panel)
        {
            if (panel.MouseEditMode == MouseEditMode.DoubleClick)
            {
                GridCell cell = null;

                if (panel.SelectionGranularity != SelectionGranularity.Cell)
                {
                    if (_HitArea == RowArea.InContent)
                    {
                        switch (panel.RowEditMode)
                        {
                            case RowEditMode.PrimaryCell:
                                if (panel.PrimaryColumn != null &&
                                    panel.PrimaryColumn.Visible == true)
                                {
                                    cell = _Cells[panel.PrimaryColumnIndex];
                                }
                                else
                                {
                                    cell = FirstVisibleCell;
                                }
                                break;

                            default:
                                cell = GetCellAt(panel, e.X, e.Y);
                                break;
                        }
                    }
                }
                else
                {
                    cell = GetCellAt(panel, e.X, e.Y);
                }

                if (cell != null)
                {
                    cell.InternalMouseDoubleClick(e);

                    return (true);
                }
            }

            return (false);
        }

        #endregion

        #endregion

        #region ProcessInExpandButton

        private void ProcessInExpandButton()
        {
            Expanded = !Expanded;
        }

        #endregion

        #region ProcessInCheckBox

        internal void ProcessInCheckBox(GridPanel panel, GridContainer item)
        {
            if (item != null && item.CanModify == true)
            {
                ProcessNewRowChange(panel);

                if (SuperGrid.DoBeforeCheckEvent(panel, item) == false)
                {
                    item.Checked = !item.Checked;

                    if (item is GridRow)
                        ((GridRow)item).RowNeedsStored = true;

                    SuperGrid.DoAfterCheckEvent(panel, item);
                }
            }
        }

        #endregion

        #region ProcessInRowHeader

        private void ProcessInRowHeader(GridPanel panel, MouseEventArgs e)
        {
            GridContainer container = _MouseDownHitRow.Parent as GridContainer;

            if (container != null)
            {
                Point pt = e.Location;

                _HitItem = GetRowAt(panel, pt);

                if (_HitItem != null)
                {
                    switch (panel.RowDragBehavior)
                    {
                        case RowDragBehavior.Move:
                        case RowDragBehavior.GroupMove:
                            if (IsInsertRow == false &&
                                panel.VirtualMode == false)
                            {
                                if (IsReordering == false)
                                    StartReorder(pt);
                                else
                                    ContinueReorder(panel, pt);
                            }
                            break;

                        case RowDragBehavior.ExtendSelection:
                            if (DragStarted(panel, e) == false)
                            {
                                if (panel.MultiSelect == true)
                                {
                                    if (_HitItem != panel.LastProcessedItem)
                                        ProcessExtendSelection(panel, true);
                                }
                            }
                            break;
                    }
                }
            }
        }

        #region DragStarted

        private bool DragStarted(GridPanel panel, MouseEventArgs e)
        {
            if (_dragSelection == true)
            {
                if (_dragStarted == false)
                {
                    if (DragDrop.DragStarted(_MouseDownPoint, e.Location) == true)
                    {
                        _dragStarted = true;

                        if (SuperGrid.DoItemDragEvent(this, e) == true)
                        {
                            _dragSelection = false;

                            InitExtendSelection(panel);
                        }
                    }
                }

                return (true);
            }

            return (false);
        }

        #endregion

        #endregion

        #region InitExtendSelection

        private void InitExtendSelection(GridPanel panel)
        {
            if (IsMouseSelectable() == true)
            {
                Capture = true;

                ExtendSelectionEx(panel);
            }
        }

        #region ExtendSelectionEx

        private void ExtendSelectionEx(GridPanel panel)
        {
            if (_MouseDownHitRow != null && _HitItem != null)
            {
                _AnchorIndex = _MouseDownHitRow.GridIndex;

                if (panel.LastProcessedItem != this && panel.LastProcessedItem != null)
                    panel.LastProcessedItem.InvalidateRender();

                bool ckey = panel.MultiSelect == true
                                ? ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                                : false;

                panel.LastProcessedItem = (ckey == true) ? _HitItem : this;

                ProcessExtendSelection(panel, false);
            }
        }

        #endregion

        #endregion

        #region ProcessExtendSelection

        internal void ProcessExtendSelection(GridPanel panel, bool extend)
        {
            bool ckey = panel.MultiSelect == true
                            ? ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                            : false;

            if (ckey == true)
                ProcessControlExtend(panel, extend);
            else
                ProcessNonControlExtend(panel, extend);

            panel.LastProcessedItem = _HitItem;
        }

        #region IsMouseSelectable

        private bool IsMouseSelectable()
        {
            if ((Control.MouseButtons & MouseButtons.Left) == MouseButtons.Left)
                return (true);

            return (IsSelected == false);
        }

        #endregion

        #region ProcessControlExtend

        private void ProcessControlExtend(GridPanel panel, bool extend)
        {
            if (panel.LastProcessedItem == null)
                panel.LastProcessedItem = _MouseDownHitRow;

            GridContainer lastRow = panel.LastProcessedItem as GridContainer;

            if (lastRow != null)
            {
                int startIndex = lastRow.GridIndex;
                int endIndex = _HitItem.GridIndex;

                panel.NormalizeIndices(extend,
                    _AnchorIndex, ref startIndex, ref endIndex);

                for (int i = startIndex; i <= endIndex; i++)
                {
                    if (extend == false || i != _AnchorIndex)
                        panel.SetSelectedRows(i, 1, !panel.IsRowSelected(i));
                }

                InvalidateRows(panel, startIndex, endIndex, true);
            }
        }

        #endregion

        #region ProcessNonControlExtend

        private void ProcessNonControlExtend(GridPanel panel, bool extend)
        {
            bool skey = (panel.MultiSelect == true) ?
                ((Control.ModifierKeys & Keys.Shift) == Keys.Shift) : false;

            if (skey == false || panel.SelectionRowAnchor == null)
                panel.SelectionRowAnchor = _MouseDownHitRow;

            ExtendSelection(panel, _HitItem, extend);
        }

        #endregion

        #endregion

        #region ResizeRow

        #region InitRowResize

        private void InitRowResize(MouseEventArgs e)
        {
            if ((Control.MouseButtons & MouseButtons.Left) == MouseButtons.Left)
            {
                Capture = true;

                _MouseDownDelta = e.Y - _MouseDownHitRow.BoundsRelative.Bottom;

                if (IsVFrozen == false)
                    _MouseDownDelta += VScrollOffset;

                IsResizing = false;
            }
            else
            {
                _MouseDownHitArea = RowArea.NoWhere;
            }
        }

        #endregion

        #region ProcessInRowResize

        private void ProcessInRowResize(GridPanel panel, Point pt)
        {
            if (IsResizing == false)
            {
                StartResize();
            }
            else
            {
                if (panel.ImmediateResize == false)
                    ContinueResize(panel, pt);
                else
                    ResizeRow(panel, pt.Y);
            }
        }

        #endregion

        #region StartResize

        private void StartResize()
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                IsResizing = true;

                if (panel.ImmediateResize == false)
                {
                    _separatorFw = new FloatWindow();
                    _separatorFw.Opacity = .5;
                    _separatorFw.BackColor = Color.Black;
                    _separatorFw.Owner = SuperGrid.FindForm();
                }
            }
        }

        #endregion

        #region StopResize

        private void StopResize()
        {
            IsResizing = false;

            if (_separatorFw != null)
            {
                _separatorFw.Close();
                _separatorFw.Dispose();

                _separatorFw = null;
            }
        }

        #endregion

        #region ContinueResize

        private void ContinueResize(GridPanel panel, Point pt)
        {
            Rectangle r = panel.BoundsRelative;

            if (panel.Parent != null)
                r.Y -= VScrollOffset;

            r.X -= HScrollOffset;

            if (pt.X > r.X)
            {
                Rectangle t = _MouseDownHitRow.BoundsRelative;

                if (panel.IsSubPanel == true)
                    t.X -= HScrollOffset;

                if (IsVFrozen == false)
                    t.Y -= VScrollOffset;

                int height = pt.Y - t.Y;

                if (height < panel.MinRowHeight)
                    pt.Y = t.Y + panel.MinRowHeight;

                if (pt.Y >= SuperGrid.PrimaryGrid.BoundsRelative.Bottom - 2)
                {
                    _separatorFw.Hide();
                }
                else
                {
                    if (pt.Y == panel.BoundsRelative.Bottom - 1)
                        pt.Y--;

                    r.Height = SeparatorHeight;
                    r.Y = pt.Y;

                    r.X = t.X;
                    r.Width = t.Width;

                    Rectangle v = SViewRect;

                    if (r.X < v.X)
                    {
                        r.Width -= (v.X - r.X);
                        r.X = v.X;
                    }

                    if (r.Right > v.Right)
                        r.Width -= (r.Right - v.Right);

                    r.Location = SuperGrid.PointToScreen(r.Location);

                    _separatorFw.Show();

                    _separatorFw.Bounds = r;
                    _separatorFw.Size = r.Size;
                }
            }
        }

        #endregion

        #region ResizeRow

        private void ResizeRow(GridPanel panel, int y)
        {
            int ry = _MouseDownHitRow.BoundsRelative.Bottom;
            y -= _MouseDownDelta;

            if (IsVFrozen == false)
                y += VScrollOffset;

            int dy = y - ry;

            if (dy != 0)
            {
                int n = Math.Max(Size.Height + dy + _MouseDownDelta, panel.MinRowHeight);
                n = Math.Min(n, panel.MaxRowHeight);

                if (panel.VirtualMode == true)
                {
                    panel.VirtualRowHeight = n;
                }
                else
                {
                    if (_MouseDownHitRow is GridRow)
                        ((GridRow)_MouseDownHitRow).RowHeight = n;
                }

                SuperGrid.DoRowResizedEvent(panel, _MouseDownHitRow);
            }
        }

        #endregion

        #endregion

        #region ReorderRow

        #region InitRowMove

        private void InitRowMove(GridPanel panel)
        {
            if (panel.CanMoveRow == true)
            {
                Capture = true;

                panel.ClearAll();
                IsSelected = true;

                IsReordering = false;

                panel.SelectionRowAnchor = _MouseDownHitRow;
            }
        }

        #endregion

        #region StartReorder

        private void StartReorder(Point pt)
        {
            if (Math.Abs(MouseDownPoint.Y - pt.Y) > 5)
            {
                IsReordering = true;
                _SeparatorRow = null;

                _headerFw = new FloatWindow();
                _headerFw.Opacity = .3;
                _headerFw.Owner = SuperGrid.FindForm();
                _headerFw.Paint += RowHeaderFwPaint;

                _separatorFw = new FloatWindow();
                _separatorFw.BackColor = Color.Black;
                _separatorFw.Owner = SuperGrid.FindForm();

                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddLines(new Point[]
                    {
                        new Point(3, 3),
                        new Point(0, 6),
                        new Point(0, 0),
                        new Point(3, 3),
                    });

                    path.CloseFigure();

                    path.AddLines(new Point[]
                    {
                        new Point(4, 3),
                        new Point(1000, 3),
                        new Point(1000, 4),
                        new Point(4, 4),
                        new Point(4, 3),
                    });

                    path.CloseFigure();

                    Region rgn = new Region(path);

                    _separatorFw.Region = rgn;
                }

            }
        }

        #endregion

        #region StopReorder

        private void StopReorder()
        {
            IsReordering = false;

            if (_headerFw != null)
            {
                _headerFw.Close();
                _headerFw.Paint -= RowHeaderFwPaint;
                _headerFw.Dispose();

                _headerFw = null;
            }

            if (_separatorFw != null)
            {
                _separatorFw.Close();
                _separatorFw.Dispose();

                _separatorFw = null;
            }
        }

        #endregion

        #region ContinueReorder

        private void ContinueReorder(GridPanel panel, Point pt)
        {
            GridContainer row = _HitItem;

            if (row != null)
            {
                ReorderHeader(pt);

                GridContainer mcont =
                    _MouseDownHitRow.Parent as GridContainer;

                GridContainer ncont =
                    row.Parent as GridContainer;

                if (mcont != null && ncont != null)
                {
                    if (mcont == ncont ||
                        (panel.RowDragBehavior == RowDragBehavior.GroupMove))
                    {
                        int n = ncont.Rows.IndexOf(row);
                        int m = mcont.Rows.IndexOf(_MouseDownHitRow);

                        if (m >= 0 && n >= 0)
                        {
                            if (_MouseDownHitRow.IsVFrozen == row.IsVFrozen)
                                ReorderSeparator(panel, pt);
                        }
                    }
                }
            }
        }

        #region ReorderHeader

        private void ReorderHeader(Point pt)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                Rectangle r = BoundsRelative;
                r.Location = PointToScroll(panel, r.Location);

                if (r.Height <= 0)
                {
                    _headerFw.Hide();
                }
                else
                {
                    Size size = _headerFw.Size;
                    Rectangle t = panel.IsSubPanel ? SViewRect : ViewRect;

                    if (r.X < t.X)
                        r.X = t.X;

                    if (r.Right > t.Right)
                        r.Width -= (r.Right - t.Right);

                    r.Height = _MouseDownHitRow.ContainerBounds.Height;
                    r.Height = Math.Min(r.Height, panel.MaxRowHeight);

                    r.Y = pt.Y - (r.Height / 2);

                    r.Location = SuperGrid.PointToScreen(r.Location);

                    _headerFw.Bounds = r;

                    if (_headerFw.Visible == false)
                        _headerFw.Show();

                    else if (r.Size.Equals(size) == false)
                        _headerFw.Refresh();

                }
            }
        }

        #endregion

        #region ReorderSeparator

        private void ReorderSeparator(GridPanel panel, Point pt)
        {
            GridContainer row = _HitItem;

            if (row != null && 
                (row.IsChildOf(_MouseDownHitRow) == false))
            {
                Rectangle bounds = row.ContainerBounds;
                bounds.Location = PointToScroll(panel, bounds.Location);

                bool isBottom = (pt.Y > bounds.Y + bounds.Height / 2);
                bool isControlDown = (Control.ModifierKeys & Keys.Control) == Keys.Control;

                if (IsControlDown != isControlDown ||
                    _SeparatorRow != row || IsSeparatorAtBottom != isBottom)
                {
                    if (isBottom == false || panel.ShowInsertRow == false)
                    {
                        _SeparatorRow = row;
                        IsSeparatorAtBottom = isBottom;

                        IsControlDown = isControlDown;

                        Rectangle r = bounds;
                        r.Y += isBottom ? row.FixedRowHeight - 4 : -4;

                        Rectangle t = SViewRect;

                        if (r.X < t.X)
                            r.X = t.X;

                        int level = row.IndentLevel;

                        if (IsSeparatorAtBottom == true && isControlDown == true)
                            level++;

                        int n = level * panel.LevelIndentSize.Width + 1;

                        r.X += n;
                        r.Width -= n;

                        if (r.Right > t.Right)
                            r.Width -= (r.Right - t.Right);

                        if (IsVFrozen == true || (r.Y + 4 >= t.Y && r.Y < t.Bottom))
                        {
                            r.Location = SuperGrid.PointToScreen(r.Location);

                            _separatorFw.Show();

                            _separatorFw.Location = r.Location;
                            _separatorFw.Width = r.Width;
                        }
                    }
                }
            }
            else
            {
                _SeparatorRow = null;

                if (_separatorFw != null)
                    _separatorFw.Hide();
            }
        }

        #endregion

        #endregion

        #region ReorderRow

        private void ReorderRow(GridPanel panel)
        {
            if ((_MouseDownHitRow != _SeparatorRow) &&
                (_MouseDownHitRow != null && _SeparatorRow != null))
            {
                if (_SeparatorRow.IsChildOf(_MouseDownHitRow) == false)
                {
                    GridContainer mcont = _MouseDownHitRow.Parent as GridContainer;
                    GridContainer ncont = _SeparatorRow.Parent as GridContainer;

                    if (mcont != null && ncont != null)
                    {
                        int n = ncont.Rows.IndexOf(_SeparatorRow);
                        int m = mcont.Rows.IndexOf(_MouseDownHitRow);

                        if (panel.RowDragBehavior == RowDragBehavior.GroupMove)
                        {
                            if (IsSeparatorAtBottom == true &&
                                ((Control.ModifierKeys & Keys.Control) == Keys.Control))
                            {
                                n = 0;
                                ncont = _SeparatorRow;

                                IsSeparatorAtBottom = false;
                            }
                        }

                        if (m >= 0 && n >= 0)
                        {
                            if (ncont == mcont)
                            {
                                if (n > m && IsSeparatorAtBottom == false)
                                    n--;

                                else if (m > n && IsSeparatorAtBottom == true)
                                    n++;

                                if (m == n)
                                    return;
                            }
                            else
                            {
                                if (IsSeparatorAtBottom == true)
                                    n++;
                            }

                            if (SuperGrid.DoRowMovingEvent(this, mcont, m, ref ncont, ref n) == false)
                            {
                                _MouseDownHitRow.IsSelected = false;

                                mcont.Rows.RemoveAt(m);
                                ncont.Rows.Insert(n, _MouseDownHitRow);

                                panel.UpdateIndicees(panel, panel.Rows, true);

                                if (_MouseDownHitRow.IsExpandedVisible == true)
                                    _MouseDownHitRow.IsSelected = true;

                                ncont.InvalidateRender();

                                SuperGrid.DoRowMovedEvent(
                                    mcont.GridPanel, mcont, _MouseDownHitRow);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #endregion

        #region RowHeaderFwPaint

        void RowHeaderFwPaint(object sender, PaintEventArgs e)
        {
            Rectangle r = _headerFw.ClientRectangle;

            if (_MouseDownHitRow != null)
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    GridPanelVisualStyle pstyle = panel.GetEffectiveStyle();

                    RenderRowHeader(e.Graphics, panel, _MouseDownHitRow, pstyle, r, false);
                }
            }

            r.Width--;
            r.Height--;
            e.Graphics.DrawRectangle(Pens.DimGray, r);
        }

        #endregion

        #endregion

        #region ExtendSelection

        internal void ExtendSelection(GridPanel panel)
        {
            _HitItem = this;

            _MouseDownHitRow = _HitItem;
            _MouseDownHitArea = RowArea.InContent;

            InvalidateRowHeader(_MouseDownHitRow);

            ProcessExtendSelection(panel, false);
        }

        #endregion

        #region GetHitArea

        ///<summary>
        /// This routine gets the RowArea 'HitArea' containing the
        /// given point (e.g. InCellCheckBox, InCellExpand, InRowHeader, etc).
        ///</summary>
        ///<param name="pt"></param>
        ///<returns></returns>
        public RowArea GetHitArea(Point pt)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                _HitItem = this;

                Rectangle r = BoundsRelative;
                r.Height = FixedRowHeight;

                if (panel.IsSubPanel == true)
                    r.X -= HScrollOffset;

                if (IsVFrozen == false)
                    r.Y -= VScrollOffset;

                if (r.Contains(pt))
                    return (GetRowHitArea(pt, panel, r));

                return (GetItemHitArea(pt, panel));
            }

            return (RowArea.NoWhere);
        }

        #region GetRowHitArea

        private RowArea GetRowHitArea(
            Point pt, GridPanel panel, Rectangle r)
        {
            if (panel.ShowRowHeaders == true)
            {
                if (pt.X >= r.X && pt.X < r.X + panel.RowHeaderWidth)
                    return (GetRowHeaderArea(pt, panel, r));
            }

            GridColumn column = panel.PrimaryColumn;

            if (column != null && column.Visible == true)
            {
                Rectangle t = _ExpandButtonBounds;

                if (IsVFrozen == false)
                    t.Y -= VScrollOffset;

                if (panel.IsSubPanel == true || column.IsHFrozen == false)
                    t.X -= HScrollOffset;

                if (t.Contains(pt) == true)
                    return (RowArea.InCellExpand);

                if (CanModify == true)
                {
                    t = CheckBoxBounds;

                    if (IsVFrozen == false)
                        t.Y -= VScrollOffset;

                    if (panel.IsSubPanel == true || column.IsHFrozen == false)
                        t.X -= HScrollOffset;

                    if (t.Contains(pt) == true)
                        return (RowArea.InCellCheckBox);
                }

                column = panel.Columns.LastVisibleColumn;

                t = column.BoundsRelative;
                t.Location = PointToScroll(panel, t.Location);

                if (pt.X > t.Right)
                    return (RowArea.InWhiteSpace);
            }

            return (RowArea.InContent);
        }

        #endregion

        #region GetRowHeaderArea

        private RowArea GetRowHeaderArea(
            Point pt, GridPanel panel, Rectangle r)
        {
            if (InfoImageBounds.Contains(pt) == true)
                return (RowArea.InRowInfo);

            if (panel.AllowRowResize == true)
            {
                if (pt.Y > r.Bottom - 6)
                    return (RowArea.InRowResize);
            }

            return (RowArea.InRowHeader);
        }

        #endregion

        #region GetItemHitArea

        private RowArea GetItemHitArea(Point pt, GridPanel panel)
        {
            _HitItem = GetRowElementAt(pt.X, pt.Y, false) as GridContainer;

            if (_HitItem != null)
            {
                Rectangle t = SViewRect;
                GridPanel ipanel = _HitItem as GridPanel;

                if (ipanel != null)
                {
                    Rectangle bounds = ipanel.BoundsRelative;
                    bounds.X -= HScrollOffset;

                    if (IsVFrozen == false)
                        bounds.Y -= VScrollOffset;

                    if (bounds.X < t.X)
                    {
                        bounds.Width -= (t.X - bounds.X);
                        bounds.X = t.X;
                    }

                    if (bounds.Contains(pt))
                        return (RowArea.InSubItem);

                    if (panel.ShowRowHeaders == true)
                    {
                        Rectangle r = ipanel.ContainerBounds;
                        r.Width = panel.RowHeaderWidth;

                        if (panel.IsSubPanel == true)
                            r.X -= HScrollOffset;

                        if (IsVFrozen == false)
                            r.Y -= VScrollOffset;

                        if (r.Contains(pt))
                            return (RowArea.InRowHeader);
                    }

                    if (ipanel.HasCheckBox == true && panel.ReadOnly == false)
                    {
                        Rectangle r = ipanel.CheckBoxBounds;
                        r.Location = PointToScroll(ipanel, r.Location);

                        if (r.Contains(pt) == true)
                            return (RowArea.InRowCheckBox);
                    }
                }
                else
                {
                    return (RowArea.InSubItem);
                }
            }

            return (RowArea.InContent);
        }

        #endregion

        #endregion

        #region GetElementAt

        /// <summary>
        /// Gets the row element containing the given coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Element or null</returns>
        public override GridElement GetElementAt(int x, int y)
        {
            return (GetRowElementAt(x, y, true));
        }

        #endregion

        #region GetRowElementAt

        internal GridElement GetRowElementAt(int x, int y, bool clip)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                Rectangle r = BoundsRelative;
                r.Height = FixedRowHeight;

                if (IsVFrozen == false)
                    r.Y -= VScrollOffset;

                if (panel.IsSubPanel == true)
                    r.X -= HScrollOffset;

                if (r.Contains(x, y))
                {
                    GridCell cell = GetCellAt(panel, x, y);

                    if (cell != null)
                        return (cell);
                }
                else
                {
                    return (base.GetElementAt(x, y));
                }
            }

            return (null);
        }

        #endregion

        #region GetCellAt

        /// <summary>
        /// Returns element at specified mouse coordinates
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="x">Horizontal position</param>
        /// <param name="y">Vertical position</param>
        /// <returns>Reference to child element or null
        /// if no element at specified coordinates</returns>
        protected virtual GridCell GetCellAt(GridPanel panel, int x, int y)
        {
            bool isFrozen = IsVFrozen;
            Rectangle t = SViewRect;

            int[] map = panel.Columns.DisplayIndexMap;
            for (int i = 0; i < map.Length; i++)
            {
                int index = map[i];

                GridColumn column = panel.Columns[index];

                if (column.Visible == true)
                {
                    if (index < _Cells.Count)
                    {
                        Rectangle bounds = _Cells[index].BoundsRelative;

                        if (panel.IsSubPanel == true || column.IsHFrozen == false)
                            bounds.X -= HScrollOffset;

                        if (isFrozen == false)
                        {
                            bounds.Y -= VScrollOffset;
                            bounds.Intersect(t);
                        }

                        if (bounds.Contains(x, y))
                            return (_Cells[index]);
                    }
                    else
                    {
                        Rectangle r = column.BoundsRelative;
                        r.Y = BoundsRelative.Y;
                        r.Height = BoundsRelative.Height;

                        Rectangle bounds = r;

                        if (panel.IsSubPanel == true || column.IsHFrozen == false)
                            bounds.X -= HScrollOffset;

                        if (isFrozen == false)
                        {
                            bounds.Y -= VScrollOffset;
                            bounds.Intersect(t);
                        }

                        if (bounds.Contains(x, y))
                            return (GetEmptySelectionCell(column));

                    }
                }
            }

            return (null);
        }

        #region GetEmptySelectionCell

        private GridCell GetEmptySelectionCell(GridColumn column)
        {
            if (_EmptySelectionCell == null ||
                _EmptySelectionCell.ColumnIndex != column.ColumnIndex)
            {
                _EmptySelectionCell = new GridCell();
            }

            _EmptySelectionCell = 
                GetEmptyCell(_EmptySelectionCell, column.ColumnIndex);

            return (_EmptySelectionCell);
        }

        #endregion

        #endregion

        #region GetRowAt

        private GridContainer GetRowAt(GridPanel panel, Point pt)
        {
            Rectangle r = panel.ContainerBounds;
            r.X -= HScrollOffset;

            if (pt.X < r.X)
                pt.X = r.X;

            GridContainer row = panel;
            GridElement item = row.InternalGetElementAt(pt.X, pt.Y);

            while (item != null)
            {
                row = item as GridContainer;

                if (row == null)
                    break;

                if (row is GridRow)
                    item = ((GridRow)row).GetRowElementAt(pt.X, pt.Y, false);
                else
                    item = row.GetElementAt(pt.X, pt.Y);

                if (item is GridContainer == false)
                    return (row);
            }

            return (null);
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
            GridPanel panel = GridPanel;

            bool dataRowChange = false;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Add:

                    foreach (GridElement item in e.NewItems)
                    {
                        if (panel != null && panel.VirtualMode == true)
                            throw new Exception("Virtual Grid can not have nested items.");

                        item.Parent = this;
                        item.NeedsMeasured = true;

                        if (panel != null)
                        {
                            GridRow row = item as GridRow;

                            if (row != null)
                            {
                                dataRowChange = true;

                                if (e.Action == NotifyCollectionChangedAction.Replace)
                                    panel.RemoveFromExpDictionary(row);

                                panel.AddToExpDictionary(row);
                            }
                            else
                            {
                                panel.NeedToUpdateIndicees = true;
                            }

                            panel.ExpandDeletedAtIndex(FullIndex + 1, 1);
                            panel.NeedToUpdateIndicees = true;
                        }
                    }

                    if (dataRowChange == true && panel.GroupColumns.Count > 0)
                        NeedsGrouped = true;
                    break;

                case NotifyCollectionChangedAction.Reset:
                    if (panel != null)
                    {
                        dataRowChange = true;

                        panel.ClearAll();
                        panel.ExpDictionary.Clear();
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    if (panel != null)
                    {
                        dataRowChange = true;

                        panel.ClearAll();

                        if (panel.ExpDictionary.Count > 0)
                        {
                            foreach (GridContainer item in e.OldItems)
                                panel.RemoveFromExpDictionary(item as GridRow);
                        }
                    }
                    break;
            }

            InvalidateLayout();

            if (dataRowChange == true)
            {
                panel.UpdateRowCount();

                SuperGrid.Invalidate();
            }
        }

        #endregion

        #region CellsCollectionChanged

        void CellsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Add:
                    GridPanel panel = GridPanel;

                    if (e.Action == NotifyCollectionChangedAction.Replace)
                    {
                        for (int i = 0; i < e.OldItems.Count; i++)
                        {
                            GridCell cell = e.OldItems[i] as GridCell;

                            if (cell != null)
                            {
                                if (panel.ExpDictionary.ContainsKey(cell))
                                    panel.ExpDictionary.Remove(cell);
                            }
                        }
                    }

                    for (int i = 0; i < e.NewItems.Count; i++)
                    {
                        GridCell cell = e.NewItems[i] as GridCell;

                        if (cell != null)
                        {
                            cell.Parent = this;
                            cell.ColumnIndex = e.NewStartingIndex + i;

                            if (panel != null)
                            {
                                string newExp = cell.Value as string;

                                if (newExp != null)
                                {
                                    newExp = newExp.Trim();

                                    if (newExp.StartsWith("="))
                                        cell.SetCellExp(panel, newExp);
                                }
                            }
                        }
                    }
                    break;

                default:
                    for (int i = 0; i < _Cells.Count; i++)
                    {
                        _Cells[i].Parent = this;
                        _Cells[i].ColumnIndex = i;
                    }
                    break;
            }

            if (SuperGrid != null)
            {
                GridCell cell = SuperGrid.ActiveElement as GridCell;

                if (cell != null)
                {
                    if (cell.GridRow == this && cell.ColumnIndex < Cells.Count)
                        SuperGrid.ActiveElement = Cells[cell.ColumnIndex];
                }
            }

            NeedsMeasured = true;

            if (GridPanel != null && GridPanel.VirtualMode == false)
                InvalidateLayout();
        }

        #endregion

        #region GetNextVisibleCell

        ///<summary>
        /// This routine returns the next visible cell in the
        /// row, after the given cell.  If the given cell is null,
        /// the first visible cell in the row will be returned.
        ///</summary>
        ///<param name="cell"></param>
        ///<returns>Next cell, or null</returns>
        ///<exception cref="Exception"></exception>
        public GridCell GetNextVisibleCell(GridCell cell)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                GridColumn column;

                if (cell != null)
                {
                    if (cell.Parent != this)
                        throw new Exception("Cell is not a member of this row.");

                    column = cell.GridColumn;

                    if (column != null)
                        column = panel.Columns.GetNextVisibleColumn(column);
                }
                else
                {
                    column = panel.FirstVisibleColumn;
                }

                if (column != null)
                    return (GetCell(column.ColumnIndex, panel.AllowEmptyCellSelection));
            }

            return (null);
        }

        #endregion

        #region GetPrevVisibleCell

        ///<summary>
        /// This routine returns the previous visible cell in the
        /// row, prior to the given cell.  If the given cell is null,
        /// the last visible cell in the row will be returned.
        ///</summary>
        ///<param name="cell"></param>
        ///<returns>Previous cell, or null</returns>
        ///<exception cref="Exception"></exception>
        public GridCell GetPrevVisibleCell(GridCell cell)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                GridColumn column;

                if (cell != null)
                {
                    if (cell.Parent != this)
                        throw new Exception("Cell is not a member of this row.");

                    column = cell.GridColumn;

                    if (column != null)
                        column = panel.Columns.GetPrevVisibleColumn(column);
                }
                else
                {
                    column = panel.LastVisibleColumn;
                }

                if (column != null)
                    return (GetCell(column.ColumnIndex, panel.AllowEmptyCellSelection));
            }

            return (null);
        }

        #endregion

        #region GetRowHeight

        internal int GetRowHeight()
        {
            if (_RowHeight >= 0)
                return (_RowHeight);

            GridPanel panel = GridPanel;

            if (panel != null)
                return (panel.DefaultRowHeight);

            return (20);
        }

        #endregion

        #region GetRowPreDetailHeight

        internal int GetRowPreDetailHeight()
        {
            if (_PreDetailRowHeight >= 0)
                return (_PreDetailRowHeight);

            GridPanel panel = GridPanel;

            if (panel != null)
                return (panel.DefaultPreDetailRowHeight);

            return (0);
        }

        #endregion

        #region GetRowPostDetailHeight

        internal int GetRowPostDetailHeight()
        {
            if (_PostDetailRowHeight >= 0)
                return (_PostDetailRowHeight);

            GridPanel panel = GridPanel;

            if (panel != null)
                return (panel.DefaultPostDetailRowHeight);

            return (0);
        }

        #endregion

        #region GetMaximumRowHeight

        ///<summary>
        /// This routine calculates and returns the maximum
        /// row height based upon the individual measured heights
        /// of each visible cell in the row.
        ///</summary>
        ///<returns></returns>
        public int GetMaximumRowHeight()
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                GridColumn[] columns = new GridColumn[panel.Columns.Count];

                int n = 0;
                for (int i = 0; i < panel.Columns.Count; i++)
                {
                    if (panel.Columns[i].Visible == true)
                        columns[n++] = panel.Columns[i];
                }

                return (GetMaximumRowHeight(columns));
            }

            return (GetRowHeight());
        }

        ///<summary>
        /// This routine calculates and returns the maximum
        /// row height based upon the individual measured heights
        /// of each cell in the row, based upon the given array of
        /// columns to included in the calculation process.
        ///</summary>
        ///<returns></returns>
        public int GetMaximumRowHeight(GridColumn[] columns)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                int maxHeight = 0;

                using (Graphics g = SuperGrid.CreateGraphics())
                {
                    GridLayoutInfo layoutInfo = new GridLayoutInfo(g, panel.BoundsRelative);
                    GridLayoutStateInfo layoutState = new GridLayoutStateInfo(panel, 0);

                    foreach (GridColumn column in columns)
                    {
                        if (column != null &&
                            column.ColumnIndex < Cells.Count)
                        {
                            GridCell cell = Cells[column.ColumnIndex];

                            if (cell != null)
                            {
                                Size oldSize = cell.Size;

                                cell.Measure(layoutInfo, layoutState, new Size(column.Width, 0));

                                if (cell.Size.Height > maxHeight)
                                    maxHeight = cell.Size.Height;

                                cell.Size = oldSize;
                            }
                        }
                    }
                }

                if (maxHeight > 0)
                    return (maxHeight);
            }

            return (GetRowHeight());
        }

        #endregion

        #region PointToScroll

        internal Point PointToScroll(GridPanel panel, Point pt)
        {
            pt.X -= HScrollOffset;

            if (IsVFrozen == false)
                pt.Y -= VScrollOffset;

            return (pt);
        }

        #endregion

        #region FlushRow

        internal override void FlushRow()
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                if (RowNeedsStored == true)
                {
                    RowNeedsStored = false;

                    panel.DataBinder.FlushRow();

                    if (panel.NeedToUpdateDataFilter == false)
                        DataFilter.UpdateRowFilterState(panel, this);

                    if (panel.VirtualMode == true)
                        SuperGrid.DoStoreVirtualRowEvent(panel, this);

                    if (RowNeedsSorted == true ||
                        IsTempInsertRow == true || IsInsertRow == true)
                    {
                        RowNeedsSorted = false;

                        if (panel.VirtualMode == true)
                        {
                            panel.VirtualRows.Clear();
                            panel.InvalidateLayout();

                            panel.DataBinder.UpdateDataSourceSort();
                        }
                        else
                        {
                            panel.UpdateRowPosition(this);
                        }
                    }
                }
            }

            base.FlushRow();
        }

        #endregion

        #region ProcessNewRowChange

        private void ProcessNewRowChange(GridPanel panel)
        {
            if (panel.ShowInsertRow == true)
            {
                if (IsInsertRow == true)
                {
                    panel.AddNewInsertRow();

                    SuperGrid.DoRowAddedEvent(panel, panel.Rows.Count - 2);

                    InvalidateLayout();
                }
            }
        }

        #endregion

        #region CancelCapture

        /// <summary>
        /// Cancels any inprogress operations (resize, reorder)
        /// that may have the mouse captured.
        /// </summary>
        public override void CancelCapture()
        {
            StopReorder();
            StopResize();

            base.CancelCapture();
        }

        #endregion

        #region Style support

        internal void ApplyCellStyle(CellVisualStyle style, StyleType cs)
        {
            ValidateStyle();

            if (_EffectiveCellStyles == null)
                _EffectiveCellStyles = new CellVisualStyles();

            if (_EffectiveCellStyles.IsValid(cs) == false)
            {
                GridPanel panel = GridPanel;

                int rowIndex = panel.UseAlternateRowStyle ? GridIndex : -1;

                if (rowIndex >= 0 && Parent is GridGroup)
                    rowIndex -= ((GridGroup)Parent).GridIndex + 1;

                CellVisualStyle cstyle = new CellVisualStyle();

                if ((rowIndex % 2) > 0)
                {
                    cstyle.ApplyStyle(SuperGrid.BaseVisualStyles.AlternateRowCellStyles[cs]);
                    cstyle.ApplyStyle(SuperGrid.DefaultVisualStyles.AlternateRowCellStyles[cs]);
                    cstyle.ApplyStyle(GridPanel.DefaultVisualStyles.AlternateRowCellStyles[cs]);
                }

                cstyle.ApplyStyle(CellStyles[cs]);

                _EffectiveCellStyles[cs] = cstyle;
            }

            style.ApplyStyle(_EffectiveCellStyles[cs]);
        }

        #region ValidateStyle

        private void ValidateStyle()
        {
            if (_StyleUpdateCount != SuperGrid.StyleUpdateCount)
            {
                _EffectiveCellStyles = null;

                _StyleUpdateCount = SuperGrid.StyleUpdateCount;
            }
        }

        #endregion

        #endregion

        #region ToString

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            GridPanel panel = GridPanel;

            if (panel != null)
            {
                sb.Append("GridRow");

                if (_Cells.Count > 0)
                {
                    sb.Append(" {");

                    foreach (GridCell cell in _Cells)
                    {
                        sb.Append(cell.Value == null ? "<null>" : cell.Value.ToString());
                        sb.Append(", ");
                    }

                    sb.Length -= 2;

                    sb.Append("}");
                }
            }

            return (sb.ToString());
        }

        #endregion

        #region IDisposable Members

        public override void Dispose()
        {
            if (_Cells != null)
                _Cells.CollectionChanged -= CellsCollectionChanged;

            DetachNestedRows(true);

            base.Dispose();
        }

        #endregion

        #region RowStates

        [Flags]
        private enum Rs
        {
            AllowEdit = (1<<0),
            EditorDirty = (1<<1),
            ControlDown = (1<<2), 
       
            DetailRow = (1<<3),
            TempInsertRow = (1<<4),

            Loading = (1<<5),
            Resizing = (1<<6),
            Reordering = (1<<7),

            RowDirty = (1<<8),
            RowFilteredOut = (1<<9),
            RowNeedsSorted = (1<<10),
            RowNeedsStored = (1<<11),

            SeparatorAtBottom = (1<<12),

            Visible = (1<<13),
        }

        #endregion
    }

    #region enums

    #region RowArea

    ///<summary>
    /// RowArea
    ///</summary>
    public enum RowArea
    {
        ///<summary>
        /// NoWhere
        ///</summary>
        NoWhere,

        ///<summary>
        /// InCellCheckBox
        ///</summary>
        InCellCheckBox,

        ///<summary>
        /// InCellExpand
        ///</summary>
        InCellExpand,

        ///<summary>
        /// InRowCheckBox
        ///</summary>
        InRowCheckBox,

        ///<summary>
        /// InRowHeader
        ///</summary>
        InRowHeader,

        ///<summary>
        /// InRowInfo
        ///</summary>
        InRowInfo,

        ///<summary>
        /// InRowResize
        ///</summary>
        InRowResize,

        ///<summary>
        /// InContent
        ///</summary>
        InContent,

        ///<summary>
        /// InSubItem
        ///</summary>
        InSubItem,

        ///<summary>
        /// InWhiteSpace
        ///</summary>
        InWhiteSpace,
    }

    #endregion

    #region RowDoubleClickBehavior

    ///<summary>
    /// Indicates what happens when a row is double clicked
    ///</summary>
    public enum RowDoubleClickBehavior
    {
        ///<summary>
        /// The row is activated
        ///</summary>
        Activate,

        ///<summary>
        /// The row is expanded or collapsed
        ///</summary>
        ExpandCollapse,
    }

    #endregion

    #endregion
}
