using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Media;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using DevComponents.DotNetBar.SuperGrid.Style;

namespace DevComponents.DotNetBar.SuperGrid
{
    /// <summary>
    /// Represents grid cell.
    /// </summary>
    public class GridCell : GridElement, IComparable<GridCell>, IDisposable
    {
        #region Constants

        private const int CheckBoxSpacing = 3;

        #endregion

        #region Static variables

        private static CellInfoWindow _cellInfoWindow;

        private static CellArea _hitArea;
        private static CellArea _lastHitArea;
        private static GridCell _hitCell;

        private static CellArea _mouseDownHitArea;
        private static GridCell _mouseDownHitCell;
        private static Point _mouseDownPoint;
        private static IGridCellEditControl _mouseRenderer;

        private static bool _mouseInItem;
        private static bool _mouseDownInItem;
        private static bool _needsMouseEnter;
        private static bool _dragSelection;
        private static bool _dragStarted;

        private static int _anchorRowIndex;
        private static int _anchorColumnIndex;

        private static string _toolTipText;

        #endregion

        #region Private variables

        private int _ColumnIndex;
        private int _IndentLevel;

        private Size _CellSize;
        private Size _ContentSize;
        private Size _MeasuredSize;

        private Rectangle _Bounds;
        private Rectangle _BackBounds;

        private string _InfoText;
        private Image _InfoImage;

        private Cs _States;
        private EditorInfo _EditorInfo;

        private int _SelectionUpdateCount;
        private int _BoundsUpdateCount;
        private int _ColumnMeasureCount;
        private int _RowMeasureCount;
        private int _StyleUpdateCount;
        private int _DataResetCount;

        private object _Value;
        private object _ExpValue;

        private CellVisualStyles _Styles;
        private CellVisualStyles _EffectiveStyles;

        #endregion

        #region Constructors

        ///<summary>
        /// GridCell
        ///</summary>
        public GridCell()
        {
            SetState(Cs.AllowEdit, true);
        }

        ///<summary>
        /// GridCell
        ///</summary>
        ///<param name="value"></param>
        public GridCell(object value)
            : this()
        {
            _Value = value;
        }

        #endregion

        #region Internal properties

        #region CanModify

        internal bool CanModify
        {
            get
            {
                if (Visible == false ||
                    IsEmptyCell == true || IsReadOnly == true || GridRow.IsDeleted == true ||
                    AllowSelection == false || GridColumn.AllowSelection == false || GridRow.AllowSelection == false)
                {
                    return (false);
                }

                return (true);
            }
        }

        #endregion

        #region CellSize

        internal Size CellSize
        {
            get { return (_CellSize); }
            set { _CellSize = value; }
        }

        #endregion

        #region CellEditMode

        internal CellEditMode CellEditMode
        {
            get
            {
                IGridCellEditControl editor = GetInternalEditControl();

                if (editor != null)
                    return (editor.CellEditMode);

                return (CellEditMode.InPlace);
            }
        }

        #endregion

        #region CheckBoxBounds

        internal Rectangle CheckBoxBounds
        {
            get { return (GridRow.CheckBoxBounds); }
        }

        #endregion

        #region ContentSize

        internal Size ContentSize
        {
            get { return(_ContentSize); }
        }

        #endregion

        #region HitArea

        internal CellArea HitArea
        {
            get { return (_hitArea); }
            set { _hitArea = value; }
        }

        #endregion

        #region IndentLevel

        internal int IndentLevel
        {
            get { return (_IndentLevel); }
            set { _IndentLevel = value; }
        }

        #endregion

        #region IsDragSelection

        internal bool IsDragSelection
        {
            get { return (_dragSelection); }
            set { _dragSelection = value; }
        }

        #endregion

        #region IsDragStarted

        internal bool IsDragStarted
        {
            get { return (_dragStarted); }
            set { _dragStarted = value; }
        }

        #endregion

        #region MeasuredSize

        internal Size MeasuredSize
        {
            get { return (_MeasuredSize); }
        }

        #endregion

        #region NeedsMeasured

        internal override bool NeedsMeasured
        {
            get
            {
                if (_ColumnMeasureCount != GridColumn.MeasureCount)
                    return (true);

                if (_RowMeasureCount != GridRow.MeasureCount)
                    return (true);

                return (base.NeedsMeasured);
            }

            set
            {
                if (value == false)
                {
                    _ColumnMeasureCount = GridColumn.MeasureCount;
                    _RowMeasureCount = GridRow.MeasureCount;
                }

                base.NeedsMeasured = value;
            }
        }

        #endregion

        #region Selected

        internal bool Selected
        {
            get { return (TestState(Cs.Selected)); }
            set { SetState(Cs.Selected, value); }
        }

        #endregion

        #region SelectionUpdateCount

        internal int SelectionUpdateCount
        {
            get { return (_SelectionUpdateCount); }
            set { _SelectionUpdateCount = value; }
        }

        #endregion

        #region ValueEx

        internal object ValueEx
        {
            get { return (_Value); }

            set
            {
                if ((value != null && value.Equals(_Value) == false) ||
                    (value == null && _Value != null))
                {
                    object oldValue = _Value;
                    _Value = value;

                     OnValueChanged(oldValue, value, DataContext.CellValueLoad);
                }
            }
        }

        #endregion

        #region ValueExx

        internal object ValueExx
        {
            get { return (_Value); }
            set { _Value = value; }
        }

        #endregion

        #endregion

        #region Public properties

        #region AllowEdit

        /// <summary>
        /// Gets or sets whether the cell can be edited by the user. 
        /// </summary>
        [DefaultValue(true), Category("Behavior")]
        [Description("Indicates whether the cell can be edited by the user.")]
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

        #region BackBounds

        ///<summary>
        /// Gets the scroll adjusted background bounds of the cell
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle BackBounds
        {
            get
            {
                if (_BoundsUpdateCount != SuperGrid.BoundsUpdateCount)
                    UpdateBoundingRects();

                return (_BackBounds);
            }
        }

        #endregion

        #region Bounds

        ///<summary>
        /// Gets the scroll adjusted bounds of the cell
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Rectangle Bounds
        {
            get
            {
                if (_BoundsUpdateCount != SuperGrid.BoundsUpdateCount)
                    UpdateBoundingRects();

                return (_Bounds);
            }
        }

        #endregion

        #region BoundsRelative

        /// <summary>
        /// Gets or sets the relative bounds of the cell
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Rectangle BoundsRelative
        {
            get { return (base.BoundsRelative); }

            internal set
            {
                base.BoundsRelative = value;

                _BoundsUpdateCount = -1;
            }
        }

        #endregion

        #region CellBounds

        ///<summary>
        /// Gets the clipped, scroll adjusted, bounding rectangle of the cell 
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle CellBounds
        {
            get
            {
                if (_BoundsUpdateCount != SuperGrid.BoundsUpdateCount)
                    UpdateBoundingRects();

                GridPanel panel = GridPanel;

                if (panel != null)
                    return (GetCellBounds(panel));

                return (Bounds);
            }
        }

        #endregion

        #region CellStyles

        /// <summary>
        /// Gets or sets the visual styles assigned to the cell
        /// </summary>
        [Category("Style")]
        [Description("Indicates the visual styles assigned to the cell.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public CellVisualStyles CellStyles
        {
            get
            {
                if (_Styles == null)
                {
                    _Styles = new CellVisualStyles();

                    StyleVisualChangeHandler(null, _Styles);
                }

                return (_Styles);
            }

            set
            {
                if (_Styles != value)
                {
                    CellVisualStyles oldValue = _Styles;

                    _Styles = value;

                    OnCellStyleChanged("CellVisualStyles", oldValue, value);
                }
            }
        }

        private void OnCellStyleChanged(string property,
            CellVisualStyles oldValue, CellVisualStyles newValue)
        {
            StyleVisualChangeHandler(oldValue, newValue);

            OnPropertyChanged(new PropertyChangedEventArgs(property));
        }

        #endregion

        #region ColumnIndex

        /// <summary>
        /// Gets the associated Column index for the cell
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ColumnIndex
        {
            get { return (_ColumnIndex); }
            internal set { _ColumnIndex = value; }
        }

        #endregion

        #region ContentBounds

        ///<summary>
        /// Gets the scroll adjusted, content only, bounding rectangle of the cell 
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle ContentBounds
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                    return (GetContentBounds(panel));

                return (Rectangle.Empty);
            }
        }

        #endregion

        #region EditBounds

        ///<summary>
        /// Gets the clipped, scroll adjusted, edit bounding rectangle of the cell 
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle EditBounds
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                    return (GetEditBounds(panel));

                return (Rectangle.Empty);
            }
        }

        #endregion

        #region EditControl

        ///<summary>
        /// Gets the Edit Control used for the cell. The cell level
        /// edit control is a non-shared control, created and based
        /// upon the cell level EditorType and EditorParams properties.
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IGridCellEditControl EditControl
        {
            get
            {
                if (_EditorInfo == null || _EditorInfo.EditorType == null)
                    return (null);

                if (_EditorInfo.EditControl == null)
                    EditControl = GetEditControl(_EditorInfo.EditorType, _EditorInfo.EditorParams);

                return (_EditorInfo.EditControl);
            }

            internal set
            {
                if (_EditorInfo == null)
                    _EditorInfo = new EditorInfo();

                if (_EditorInfo.EditControl != value)
                {
                    if (_EditorInfo.EditControl != null)
                    {
                        if (_EditorInfo.EditControl.EditorPanel.Parent != null)
                            SuperGrid.Controls.Remove(_EditorInfo.EditControl.EditorPanel);

                        if (_EditorInfo.EditorType != null)
                            ((Control)_EditorInfo.EditControl).Dispose();
                    }

                    _EditorInfo.EditControl = value;

                    if (_EditorInfo.EditControl != null)
                    {
                        if (_EditorInfo.EditControl.EditorPanel.Parent == null)
                            SuperGrid.Controls.Add(_EditorInfo.EditControl.EditorPanel);
                    }
                }
            }
        }

        #endregion

        #region EditorDirty

        ///<summary>
        /// Gets or sets whether the value
        /// being edited has been changed in the editor 
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool EditorDirty
        {
            get { return (GridRow.EditorDirty); }
            set { GridRow.EditorDirty = value; }
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
            get
            {
                if (_EditorInfo == null)
                    return (null);

                return (_EditorInfo.EditorParams);
            }

            set
            {
                if (_EditorInfo == null)
                    _EditorInfo = new EditorInfo();

                if (_EditorInfo.EditorParams != value)
                {
                    _EditorInfo.EditorParams = value;

                    _EditorInfo.EditControl = null;
                    _EditorInfo.RenderControl = null;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("EditorParams", VisualChangeType.Layout);
                }
                                    
                if (_EditorInfo.IsEmpty == true)
                    _EditorInfo = null;
            }
        }

        #endregion

        #region EditorType

        ///<summary>
        /// Indicates the cell editor type. This is the control type
        /// used to perform the actual modification of the cell value
        ///</summary>
        [DefaultValue(null), Category("Data")]
        [Description("Indicates the cell editor type. This is the control type used to perform the actual modification of the cell value.")]
        [TypeConverter(typeof(EditTypeConverter))]
        [Editor("DevComponents.SuperGrid.Design.EditTypeEditor, DevComponents.SuperGrid.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=26d81176cfa2b486", typeof(UITypeEditor))]
        public Type EditorType
        {
            get
            {
                if (_EditorInfo == null)
                    return (null);

                return (_EditorInfo.EditorType);
            }

            set
            {
                if (value != null || _EditorInfo != null)
                {
                    if (_EditorInfo == null)
                        _EditorInfo = new EditorInfo();

                    if (_EditorInfo.EditorType != value)
                    {
                        _EditorInfo.EditorType = value;
                        _EditorInfo.EditControl = null;
                        _EditorInfo.RenderControl = null;

                        NeedsMeasured = true;

                        OnPropertyChangedEx("EditorType", VisualChangeType.Layout);
                    }

                    if (_EditorInfo.IsEmpty == true)
                        _EditorInfo = null;
                }
            }
        }

        #endregion

        #region ExpValue

        ///<summary>
        /// Gets the last evaluated expression value for the cell.
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object ExpValue
        {
            get { return (_ExpValue); }
        }

        #endregion

        #region GridColumn

        /// <summary>
        /// Gets the GridColumn associated with the cell
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridColumn GridColumn
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    if (_ColumnIndex < panel.Columns.Count)
                        return (panel.Columns[_ColumnIndex]);
                }

                return (null);
            }
        }

        #endregion

        #region GridRow

        ///<summary>
        /// Gets the GridRow associated with the cell.
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridRow GridRow
        {
            get { return (Parent as GridRow); }
        }

        #endregion

        #region HighLightBounds

        ///<summary>
        /// Gets the scroll adjusted, bounding rectangle used
        /// to highlight the cell contents when selected.
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle HighLightBounds
        {
            get
            {
                Rectangle r = Rectangle.Empty;
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    switch (GetCellHighlightMode(panel))
                    {
                        case CellHighlightMode.Content:
                            IGridCellEditControl editor = GetInternalRenderControl();

                            r = GetAdjustedBounds(ContentBounds);
                            r = GetEditPanelBounds(editor, r);

                            r.Inflate(1, 1);
                            break;

                        case CellHighlightMode.Partial:
                            r = GetAdjustedBounds(ContentBounds);
                            r.Inflate(1, 1);
                            break;

                        case CellHighlightMode.Full:
                            r = GetAdjustedBounds();
                            break;
                    }
                }

                return (r);
            }
        }

        #endregion

        #region InfoImage

        /// <summary>
        /// Gets or sets the cell informational Image (the image
        /// to display when InfoText is non-empty)
        /// </summary>
        [DefaultValue(null), Category("Appearance")]
        [Description("Indicates the cell informational Image (the image to display when InfoText is non-empty).")]
        public Image InfoImage
        {
            get { return (_InfoImage); }

            set
            {
                if (_InfoImage != value)
                {
                    _InfoImage = value;

                    OnPropertyChangedEx("InfoImage", VisualChangeType.Render);
                }
            }
        }

        #region GetInfoImage

        internal Image GetInfoImage()
        {
            return (_InfoImage ?? GridColumn.GetInfoImage());
        }

        #endregion

        #endregion

        #region InfoImageBounds

        ///<summary>
        /// Gets the scroll adjusted, bounding
        /// rectangle for the current set info image
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle InfoImageBounds
        {
            get
            {
                Image image = GetInfoImage();

                if (image != null && string.IsNullOrEmpty(_InfoText) == false)
                {
                    Rectangle r = GetAdjustedBounds(ContentBounds);
                    r.Inflate(-2, -1);

                    Rectangle t = r;

                    switch (GridColumn.InfoImageAlignment)
                    {
                        case Alignment.TopCenter:
                            r.X += (r.Width - image.Width) / 2;
                            break;

                        case Alignment.TopRight:
                            r.X = (r.Right - image.Width);
                            break;

                        case Alignment.MiddleLeft:
                            r.Y += (r.Height - image.Height) / 2;
                            break;

                        case Alignment.MiddleCenter:
                            r.X += (r.Width - image.Width) / 2;
                            r.Y += (r.Height - image.Height) / 2;
                            break;

                        case Alignment.MiddleRight:
                            r.X = (r.Right - image.Width);
                            r.Y += (r.Height - image.Height) / 2;
                            break;

                        case Alignment.BottomLeft:
                            r.Y = (r.Bottom - image.Height);
                            break;

                        case Alignment.BottomCenter:
                            r.X += (r.Width - image.Width) / 2;
                            r.Y = (r.Bottom - image.Height);
                            break;

                        case Alignment.BottomRight:
                            r.X = (r.Right - image.Width);
                            r.Y = (r.Bottom - image.Height);
                            break;
                    }

                    if (r.X < t.X)
                        r.X = t.X;

                    if (r.Y < t.Y)
                        r.Y = t.Y;

                    r.Width = image.Width;
                    r.Height = image.Height;

                    return (r);
                }

                return (Rectangle.Empty);
            }
        }

        #endregion

        #region InfoText

        ///<summary>
        /// Gets or sets the informational text associated with
        /// the cell.  If the InfoText is non-null, then the cells
        /// associated InfoImage is displayed in the cell, with the
        /// InfoText being displayed as the ToolTip for the InfoImage.
        ///</summary>
        [DefaultValue(null), Category("Appearance")]
        [Description("Indicates the informational text associated with the cell. If the InfoText is non-null, then the cells associated InfoImage is displayed in the cell, with the InfoText being displayed as the ToolTip for the InfoImage.")]
        public string InfoText
        {
            get { return (_InfoText); }

            set
            {
                if (_InfoText != value)
                {
                    _InfoText = value;

                    UpdateInfoWindow();

                    OnPropertyChangedEx("InfoText");
                }
            }
        }

        #endregion

        #region IsActiveCell

        ///<summary>
        /// Gets whether the cell is the Active cell.
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsActiveCell
        {
            get
            {
                if (SuperGrid != null)
                    return (SuperGrid.ActiveCell == this);

                return(false);
            }
        }

        #endregion

        #region IsCellVisible

        ///<summary>
        /// Gets whether the cell is visible (taking row visibility,
        /// column visibility, and expanded row state into account).
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsCellVisible
        {
            get
            {
                if (BoundsRelative.Width > 0)
                {
                    GridRow row = GridRow;

                    if (row != null)
                    {
                        if (GridColumn.Visible == false)
                            return (false);

                        return (row.IsExpandedVisible);
                    }
                }

                return (false);
            }
        }

        #endregion

        #region IsDataError

        ///<summary>
        /// Gets whether setting the cell Value has caused a Data Error
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDataError
        {
            get { return (TestState(Cs.DataError)); }
            internal set { SetState(Cs.DataError, value); }
        }

        #endregion

        #region IsEmptyCell

        ///<summary>
        /// Gets whether the cell is an Empty cell.
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEmptyCell
        {
            get { return (TestState(Cs.EmptyCell)); }
            internal set { SetState(Cs.EmptyCell, value); }
        }

        #endregion

        #region IsPrimaryCell

        ///<summary>
        /// Gets whether the cell is the defined PrimaryCell
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPrimaryCell
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                    return (panel.PrimaryColumnIndex == _ColumnIndex);

                return (false);
            }
        }

        #endregion

        #region IsReadOnly

        ///<summary>
        /// Gets whether the cell is ReadOnly due to 
        /// row, column, or cell ReadOnly property status.
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsReadOnly
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    if (panel.ReadOnly || GridColumn.ReadOnly || GridRow.ReadOnly)
                        return (true);
                }

                return (TestState(Cs.ReadOnly));
            }
        }

        #endregion

        #region IsSelectable

        ///<summary>
        /// Gets whether the cell is selectable due to
        /// row, column, or cell AllowSelection property status.
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSelectable
        {
            get
            {
                return (AllowSelection == true &&
                    GridRow.AllowSelection == true &&
                    GridColumn.AllowSelection == true);
            }
        }

        #endregion

        #region IsSelected

        ///<summary>
        /// Gets or sets whether the cell is selected.
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSelected
        {
            get
            {
                if (AllowSelection == true)
                {
                    GridPanel panel = GridPanel;

                    if (panel != null)
                    {
                        if (panel.AllowSelection == true)
                        {
                            if (_SelectionUpdateCount != panel.SelectionUpdateCount)
                            {
                                Selected = false;
                                _SelectionUpdateCount = panel.SelectionUpdateCount;

                                if (GridRow.IsSelected == true)
                                    Selected = true;

                                else if (GridColumn.IsSelected == true)
                                    Selected = true;

                                else
                                    Selected = panel.IsItemSelected(this);
                            }

                            return (Selected);
                        }
                    }
                }

                return (false);
            }

            set
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    if (panel.SetSelected(this, value) == true)
                        InvalidateRender();
                }
            }
        }

        #endregion

        #region IsValueExpression

        ///<summary>
        /// Gets whether the cell Value is an expression.
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsValueExpression
        {
            get { return (TestState(Cs.ValueExpression)); }
            internal set { SetState(Cs.ValueExpression, value); }
        }

        #endregion

        #region IsValueNull

        ///<summary>
        /// Gets whether the cell Value is null.
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsValueNull
        {
            get
            {
                object value = Value;

                return (value == null ||
                    (GridPanel.NullValue == NullValue.DBNull && value == DBNull.Value));
            }
        }

        #endregion

        #region NullString

        /// <summary>
        /// Gets how null values are displayed 
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string NullString
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                    return (GridColumn.NullString ?? panel.NullString);

                return (String.Empty);
            }
        }

        #endregion

        #region ReadOnly

        /// <summary>
        /// Gets or sets whether the user can change cell contents
        /// </summary>
        [DefaultValue(false), Category("Behavior")]
        [Description("Indicates whether the user can change cell contents.")]
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

        #region RenderControl

        ///<summary>
        /// Gets the current set Render Control for the cell
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IGridCellEditControl RenderControl
        {
            get
            {                    
                if (_EditorInfo == null ||
                    (_EditorInfo.RenderType == null && _EditorInfo.EditorType == null))
                {
                    return (null);
                }

                if (_EditorInfo.RenderControl == null)
                {
                    RenderControl = (_EditorInfo.RenderType != null)
                                        ? GetEditControl(_EditorInfo.RenderType, _EditorInfo.RenderParams)
                                        : GetEditControl(_EditorInfo.EditorType, _EditorInfo.EditorParams);
                }

                return (_EditorInfo.RenderControl);
            }

            internal set
            {
                if (_EditorInfo == null)
                    _EditorInfo = new EditorInfo();

                if (_EditorInfo.RenderControl != value)
                {
                    if (_EditorInfo.RenderControl != null)
                    {
                        if (_EditorInfo.RenderControl.EditorPanel.Parent != null)
                            SuperGrid.Controls.Remove(_EditorInfo.RenderControl.EditorPanel);

                        if (_EditorInfo.RenderType != null || _EditorInfo.EditorType != null)
                            ((Control)_EditorInfo.RenderControl).Dispose();
                    }

                    _EditorInfo.RenderControl = value;

                    if (_EditorInfo.RenderControl != null)
                    {
                        if (_EditorInfo.RenderControl.EditorPanel.Parent == null)
                            SuperGrid.Controls.Add(_EditorInfo.RenderControl.EditorPanel);
                    }
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
            get
            {
                if (_EditorInfo == null)
                    return (null);

                return (_EditorInfo.RenderParams);
            }

            set
            {
                if (_EditorInfo == null)
                    _EditorInfo = new EditorInfo();

                if (_EditorInfo.RenderParams != value)
                {
                    _EditorInfo.RenderParams = value;

                    _EditorInfo.RenderControl = null;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("RenderParams", VisualChangeType.Layout);
                }

                if (_EditorInfo.IsEmpty == true)
                    _EditorInfo = null;
            }
        }

        #endregion

        #region RenderType

        ///<summary>
        /// Gets or sets the cell render type. This is the control
        /// type used to perform the default rendering of the cell value
        ///</summary>
        [DefaultValue(null), Category("Data")]
        [Description("Indicates the cell render type. This is the control type used to perform the default rendering of the cell value.")]
        [TypeConverter(typeof(EditTypeConverter))]
        [Editor("DevComponents.SuperGrid.Design.EditTypeEditor, DevComponents.SuperGrid.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=26d81176cfa2b486", typeof(UITypeEditor))]
        public Type RenderType
        {
            get
            {
                if (_EditorInfo == null)
                    return (null);

                return (_EditorInfo.RenderType);
            }

            set
            {
                if (value != null || _EditorInfo != null)
                {
                    if (_EditorInfo == null)
                        _EditorInfo = new EditorInfo();

                    if (_EditorInfo.RenderType != value)
                    {
                        _EditorInfo.RenderType = value;
                        _EditorInfo.RenderControl = null;

                        NeedsMeasured = true;

                        OnPropertyChangedEx("RenderType", VisualChangeType.Layout);
                    }

                    if (_EditorInfo.IsEmpty == true)
                        _EditorInfo = null;
                }
            }
        }

        #endregion

        #region RowIndex

        /// <summary>
        /// Gets the associated Row index for the cell
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int RowIndex
        {
            get
            {
                GridRow row = GridRow;

                return (row != null ? row.RowIndex : -1);
            }
        }

        #endregion

        #region Value

        /// <summary>
        /// Gets or sets the cell value
        /// </summary>
        [DefaultValue(null), Category("Data")]
        [Description("Indicates the cell value.")]
        [TypeConverter(typeof(ValueTypeConverter))]
        [Editor("DevComponents.SuperGrid.Design.ValueTypeEditor, DevComponents.SuperGrid.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=26d81176cfa2b486", typeof(UITypeEditor))]
        public object Value
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    object o = GetValue(panel);

                    if (SuperGrid != null)
                        SuperGrid.DoGetCellValueEvent(this, ref o);

                    return (o);
                }

                return (_Value);
            }

            set
            {
                if ((value != null && value.Equals(_Value) == false) ||
                    (value == null && _Value != null))
                {
                    object oldValue = _Value;

                    SetValue(value);

                    GridRow row = GridRow;

                    if (row != null && row.Loading == false)
                        OnValueChanged(oldValue, value, DataContext.CellEdit);
                }
            }
        }

        #region GetValue

        protected virtual object GetValue(GridPanel panel)
        {
            if (panel.DataSource != null)
            {
                if (ActiveIList(panel) == true ||
                    _DataResetCount != panel.DataBinder.DataResetCount)
                {
                    _DataResetCount = panel.DataBinder.DataResetCount;

                    object o = panel.DataBinder.GetValue(this, _Value);

                    if ((o == null && o != _Value) ||
                        (o != null && o.Equals(_Value) == false))
                    {
                        _Value = o;

                        GridColumn.NeedsFilterScan = true;

                        InvalidateRender();

                        return (o);
                    }
                }
            }

            return (_Value);
        }

        #region ActiveIList

        private bool ActiveIList(GridPanel panel)
        {
            if (panel.DataSource is IList &&
                (panel.DataSource is IListSource == false &&
                 panel.DataSource is IBindingList == false))
            {
                GridRow row = GridRow;

                if (row != null)
                {
                    if (panel.LatentActiveRowIndex < 0 && row.IsTempInsertRow == false)
                        return (true);
                }
            }

            return (false);
        }

        #endregion

        #endregion

        #region SetValue

        protected virtual void SetValue(object value)
        {
            _Value = value;

            GridPanel panel = GridPanel;

            if (panel != null)
            {
                GridRow row = GridRow;

                if (row.Loading == false)
                {
                    panel.DataBinder.SetValue(this, value);

                    row.RowNeedsStored = true;
                    
                    GridColumn.NeedsFilterScan = true;

                    if (panel.KeepRowsSorted == true)
                    {
                        if (GridColumn.IsSortColumn == true)
                        {
                            if (row == panel.ActiveRow)
                                row.RowNeedsSorted = true;
                            else
                                panel.NeedsSorted = true;
                        }
                    }
                }
            }
        }

        #endregion

        #region OnValueChanged

        private void OnValueChanged(object oldValue, object newValue, DataContext context)
        {
            NeedsMeasured = true;

            IsDataError = false;

            ClearEffectiveStyles();
            RefreshCellEditor();

            OnPropertyChanged(new PropertyChangedEventArgs("Value"));

            GridPanel panel = GridPanel;

            if (panel != null)
            {
                SuperGrid.DoCellValueChangedEvent(this, oldValue, newValue, context);

                if (panel.EnableCellExpressions == true)
                {
                    if (GridRow.Loading == false)
                        UpdateCellExp(panel, oldValue, newValue);
                }
            }
        }

        #region RefreshCellEditor

        private void RefreshCellEditor()
        {
            IGridCellEditControl editor = GetInternalEditControl();

            if (editor != null)
            {
                if (SuperGrid.NonModalEditorCell == this)
                    RefreshNonModalEditor();

                else if (SuperGrid.EditorCell == this)
                    RefreshEditor(editor);

                switch (editor.ValueChangeBehavior)
                {
                    case ValueChangeBehavior.InvalidateLayout:
                        GridColumn.NeedsResized = true;
                        GridRow.NeedsMeasured = true;

                        InvalidateLayout();
                        break;

                    default:
                        InvalidateRender();
                        break;
                }
            }
        }

        #region RefreshEditor

        private void RefreshEditor(IGridCellEditControl editor)
        {
            editor.SuspendUpdate = true;

            try
            {
                CellVisualStyle style = GetEffectiveStyle();

                editor.InitializeContext(this, style);
            }
            finally
            {
                editor.SuspendUpdate = false;
            }
        }

        #endregion

        #region RefreshNonModalEditor

        private void RefreshNonModalEditor()
        {
            if (SuperGrid.ActiveNonModalEditor != null)
            {
                SuperGrid.ActiveNonModalEditor.EditorValue = _Value;

                CellVisualStyle style = GetEffectiveStyle();

                InitializeContext(DataContext.CellValueLoad,
                    SuperGrid.ActiveNonModalEditor, style);
            }
        }

        #endregion

        #endregion

        #endregion

        #region UpdateCellExp

        internal void UpdateCellExp(
            GridPanel panel, object oldValue, object value)
        {
            string oldExp = oldValue as string;
            string newExp = value as string;

            if (oldExp != null)
            {
                oldExp = oldExp.Trim();

                if (oldExp.StartsWith("="))
                {
                    panel.ExpDictionary.Remove(this);

                    InfoText = null;
                }
            }

            IsValueExpression = false;

            if (newExp != null)
            {
                newExp = newExp.Trim();

                if (newExp.StartsWith("="))
                    SetCellExp(panel, newExp);
            }

            RefreshExpressions(panel, this, new List<GridCell>());
        }

        #region RefreshExpressions

        private void RefreshExpressions(
            GridPanel panel, GridCell gridCell, List<GridCell> usedCells)
        {
            foreach (KeyValuePair<GridCell, EEval> item in panel.ExpDictionary)
            {
                List<GridCell> cells = item.Value.GetCellReferences();

                if (cells != null)
                {
                    if (cells.Contains(gridCell) == true)
                    {
                        item.Key.InvalidateRender();

                        if (usedCells.Contains(item.Key) == false)
                        {
                            usedCells.Add(item.Key);

                            RefreshExpressions(panel, item.Key, usedCells);

                            usedCells.Remove(item.Key);
                        }
                    }
                }
            }
        }

        #endregion

        #region SetCellExp

        internal void SetCellExp(GridPanel panel, string newExp)
        {
            IsValueExpression = false;

            while (true)
            {
                try
                {
                    EEval eval = new EEval(this, newExp);

                    panel.ExpDictionary[this] = eval;

                    InfoText = null;
                    IsValueExpression = true;

                    break;
                }
                catch (Exception exp)
                {
                    bool retry = false;
                    bool throwException = false;

                    if (SuperGrid.HasDataErrorHandler == true)
                    {
                        object value = newExp;

                        if (SuperGrid.DoDataErrorEvent(GridPanel, this, exp,
                            DataContext.CellExpressionParse, ref value, ref throwException, ref retry) == true)
                        {
                            return;
                        }
                    }

                    if (throwException == true)
                        throw;

                    InfoText = exp.Message;

                    if (retry == false)
                        break;
                }
            }
        }

        #endregion

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

        #region MeasureOverride

        private int _MeasureCount;

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
            _MeasureCount++;

            MeasureCell(layoutInfo.Graphics,
                stateInfo.GridPanel, stateInfo.IndentLevel, constraintSize);
        }

        #region MeasureCell

        private void MeasureCell(Graphics g,
            GridPanel panel, int indentLevel, Size constraintSize)
        {
            Size sizeNeeded = Size.Empty;

            if (ColumnIndex < panel.Columns.Count)
            {
                CellVisualStyle style = GetSizingStyle(panel);

                Image image = style.GetImage(panel);
                Alignment imageAlignment = style.ImageAlignment;
                Size imageSize = style.GetImageSize(image);

                if (style.IsOverlayImage == true)
                    imageAlignment = Alignment.MiddleCenter;

                int width = constraintSize.Width;

                Size borderSize = style.GetBorderSize(true);

                int bwidth = borderSize.Width + 2;
                int bheight = borderSize.Height + 2;

                if (constraintSize.Width > 1)
                {
                    switch (imageAlignment)
                    {
                        case Alignment.TopCenter:
                        case Alignment.MiddleCenter:
                        case Alignment.BottomCenter:
                            break;

                        default:
                            width -= imageSize.Width;
                            break;
                    }

                    width -= (bwidth + 3);

                    if (BoundsRelative.X == GridPanel.Bounds.X)
                        width--;
                }

                _ContentSize =
                    GetProposedSize(g, style, new Size(width, 0));

                switch (imageAlignment)
                {
                    case Alignment.MiddleCenter:
                        sizeNeeded.Height = Math.Max(imageSize.Height, _ContentSize.Height) + bheight;
                        sizeNeeded.Width = Math.Max(_ContentSize.Width, imageSize.Width) + bwidth;
                        break;

                    case Alignment.TopCenter:
                    case Alignment.BottomCenter:
                        sizeNeeded.Width = Math.Max(_ContentSize.Width, imageSize.Width) + bwidth;
                        sizeNeeded.Height = _ContentSize.Height + imageSize.Height + bheight;
                        break;

                    default:
                        sizeNeeded.Width = _ContentSize.Width + imageSize.Width + bwidth;
                        sizeNeeded.Height = Math.Max(imageSize.Height, _ContentSize.Height) + bheight;
                        break;
                }

                if (panel.GroupColumns.Count > 0)
                {
                    if (panel.Columns.FirstVisibleColumn == GridColumn)
                        sizeNeeded.Width += panel.LevelIndentSize.Width * (indentLevel + 1);
                }

                if (GridRow != null &&
                    (GridRow.Rows.Count > 0 || GridRow.RowsUnresolved == true))
                {
                    if (panel.ExpandImage != null)
                    {
                        sizeNeeded.Height = Math.Max(sizeNeeded.Height,
                            panel.ExpandImage.Height + 4);
                    }

                    if (panel.CollapseImage != null)
                    {
                        sizeNeeded.Height = Math.Max(sizeNeeded.Height,
                            panel.CollapseImage.Height + 4);
                    }
                }

                if (panel.CheckBoxes == true)
                {
                    sizeNeeded.Height = Math.Max(sizeNeeded.Height,
                        panel.CheckBoxSize.Height);

                    sizeNeeded.Width += (CheckBoxSpacing * 2);
                }
            }

            sizeNeeded.Width += 6;
            sizeNeeded.Height += 3;

            if (constraintSize.Width > 0)
                sizeNeeded.Width = constraintSize.Width;

            _MeasuredSize = sizeNeeded;

            Size = sizeNeeded;
        }

        #endregion

        #endregion

        #region ArrangeOverride

        /// <summary>
        /// Performs the arrange pass layout of the item
        /// when final position and size of the item has been set.
        /// </summary>
        /// <param name="layoutInfo">Layout information.</param>
        /// <param name="stateInfo"></param>
        /// <param name="layoutBounds"></param>
        protected override void ArrangeOverride(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, Rectangle layoutBounds)
        {
            GridPanel panel = stateInfo.GridPanel;
            Rectangle r = layoutBounds;

            r.X += _IndentLevel * (panel.LevelIndentSize.Width);
            r.Width = panel.TreeButtonIndent;

            ArrangeExpandButton(panel, ref r);
            ArrangeCheckBox(panel, r);

            _toolTipText = null;
        }

        #region ArrangeExpandButton

        private void ArrangeExpandButton(GridPanel panel, ref Rectangle r)
        {
            if (IsPrimaryCell == true)
            {
                Rectangle t = Rectangle.Empty;

                if (panel.ShowTreeButtons == true)
                {
                    if (NeedsExpandButton() == true)
                    {
                        t = r;

                        Size size = panel.GetTreeButtonSize();

                        t.X += (r.Width - size.Width) / 2;
                        t.Y += (r.Height - size.Height) / 2 - 1;

                        t.Size = size;
                    }

                    r.X += panel.TreeButtonIndent;
                    r.Width -= panel.TreeButtonIndent;
                }
                else
                {
                    if (panel.CheckBoxes == true)
                    {
                        r.X += CheckBoxSpacing;
                        r.Width -= CheckBoxSpacing;
                    }
                }

                GridRow.ExpandButtonBounds = t;
            }
        }

        #region NeedsExpandButton

        private bool NeedsExpandButton()
        {
            GridRow row = GridRow;

            if (row != null)
            {
                if (row.ShowTreeButton == true)
                {
                    if (row.RowsUnresolved || row.HasVisibleItems)
                        return (true);
                }
            }

            return (false);
        }

        #endregion

        #endregion

        #region ArrangeCheckBox

        private void ArrangeCheckBox(GridPanel panel, Rectangle r)
        {
            if (IsPrimaryCell == true)
            {
                if (GridRow.HasCheckBox == true)
                {
                    r.Width = panel.CheckBoxSize.Width;
                    r.Y += (r.Height - panel.CheckBoxSize.Height) / 2;
                    r.Height = panel.CheckBoxSize.Height;

                    GridRow.CheckBoxBounds = r;
                }
                else
                {
                    GridRow.CheckBoxBounds = Rectangle.Empty;
                }
            }
        }

        #endregion

        #endregion

        #region RenderOverride

        /// <summary>
        /// Performs drawing of the item and its children.
        /// </summary>
        /// <param name="renderInfo">Holds contextual rendering information.</param>
        protected override void RenderOverride(GridRenderInfo renderInfo)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                CellVisualStyle style = GetEffectiveStyle();

                if (NeedsMeasured == true)
                {
                    Size size = new Size(GridColumn.Size.Width, GridRow.Size.Height);

                    if (panel.VirtualMode == false)
                        MeasureCell(renderInfo.Graphics, panel, IndentLevel, size);

                    size.Height = GridRow.FixedRowHeight;
                    Size = size;

                    UpdateBoundingRects();

                    NeedsMeasured = false;
                }

                Rectangle r = BackBounds;

                if (r.Width > 0 && r.Height > 0)
                {
                    Graphics g = renderInfo.Graphics;
                    CellHighlightMode mode = GetCellHighlightMode(panel);

                    if (SuperGrid.DoPreRenderCellEvent(g, this, RenderParts.Background, r) == false)
                    {
                        RenderCellBackground(panel, g, r, mode);
                        SuperGrid.DoPostRenderCellEvent(g, this, RenderParts.Background, r);
                    }

                    RenderGridBorder(g, panel, r);

                    int n = 0;

                    if (IsPrimaryCell == true)
                    {
                        int x = r.X;

                        RenderCellTreeData(g, panel, ref r);
                        RenderCellCheckBox(g, panel, ref r);

                        n = r.X - x;
                    }
                    else
                    {
                        if (panel.GroupColumns.Count > 0)
                        {
                            if (panel.Columns.FirstVisibleColumn == GridColumn)
                                n = panel.LevelIndentSize.Width * _IndentLevel;
                        }
                    }

                    r = Bounds;
                    r.X += n;
                    r.Width -= n;

                    if (SuperGrid.DoPreRenderCellEvent(g, this, RenderParts.Border, r) == false)
                    {
                        RenderCellBorder(g, panel, style, r);
                        SuperGrid.DoPostRenderCellEvent(g, this, RenderParts.Border, r);
                    }

                    r = GetAdjustedBounds(ContentBounds);

                    if (style.ImageOverlay != ImageOverlay.Top)
                        RenderCellImage(g, panel, style, r, mode);

                    if (CanRenderCellValue() == true)
                    {
                        if (SuperGrid.DoPreRenderCellEvent(g, this, RenderParts.Content, r) == false)
                        {
                            RenderCellValue(g, style, r);
                            SuperGrid.DoPostRenderCellEvent(g, this, RenderParts.Content, r);
                        }
                    }

                    if (style.ImageOverlay == ImageOverlay.Top)
                        RenderCellImage(g, panel, style, r, mode);

                    if (IsDataError == true)
                    {
                        Rectangle t = Bounds;

                        if (t.Width > 5 && t.Height > 5)
                            t.Inflate(-5, -5);

                        using (SolidBrush br = new SolidBrush(Color.FromArgb(64, Color.Crimson)))
                            renderInfo.Graphics.FillRectangle(br, t);
                    }
                    else if (GridRow.IsDeleted == true)
                    {
                        using (SolidBrush br = new SolidBrush(Color.FromArgb(128, Color.LightGray)))
                            renderInfo.Graphics.FillRectangle(br, Bounds);
                    }

                    if (panel.FocusCuesEnabled == true)
                    {
                        GridCell cell = SuperGrid.ActiveElement as GridCell;

                        if (IsSameCell(cell) == true)
                            RenderFocusRect(g);
                    }
                }
            }
        }

        #region IsSameCell

        private bool IsSameCell(GridCell cell)
        {
            if (cell != null)
            {
                return (ColumnIndex == cell.ColumnIndex &&
                    GridRow == cell.GridRow);
            }

            return (false);
        }

        #endregion

        #region CanRenderCellValue

        private bool CanRenderCellValue()
        {
            if (Visible == false || IsEmptyCell == true || SuperGrid.EditorCell == this)
                return (false);

            if (SuperGrid.NonModalEditorCell == this)
                return (false);

            return (true);
        }

        #endregion

        #region RenderCellBackground

        private void RenderCellBackground(GridPanel panel,
            Graphics g, Rectangle r, CellHighlightMode type)
        {
            if (SuperGrid.EditorCell == this)
            {
                TextBox tb = GetInternalEditControl() as TextBox;

                if (tb != null)
                {
                    using (Brush br = new SolidBrush(tb.BackColor))
                        RenderBackground(g, r, type, br);

                    return;
                }
            }
            
            using (Brush br = GetContentBrush(panel, r, type))
                RenderBackground(g, r, type, br);
        }

        #region RenderBackground

        private void RenderBackground(
            Graphics g, Rectangle r, CellHighlightMode type, Brush br)
        {
            if (type == CellHighlightMode.Partial ||
                type == CellHighlightMode.Content)
            {
                r = RenderPartialBackground(g, r, type);
            }

            if (r.Width > 0 && r.Height > 0)
                g.FillRectangle(br, r);
        }

        #endregion

        #region RenderPartialBackground

        private Rectangle RenderPartialBackground(
            Graphics g, Rectangle r, CellHighlightMode type)
        {
            CellVisualStyle defaultStyle =
                GetEffectiveStyle(StyleType.Default);

            using (Brush br = defaultStyle.Background.GetBrush(r))
                g.FillRectangle(br, r);

            r = GetAdjustedBounds(ContentBounds);

            if (type == CellHighlightMode.Content)
            {
                IGridCellEditControl editor = GetInternalRenderControl();

                r = GetEditPanelBounds(editor, r);
            }

            r.Inflate(1, 1);

            return (r);
        }

        #endregion

        #region GetContentBrush

        private Brush GetContentBrush(
            GridPanel panel, Rectangle r, CellHighlightMode type)
        {
            if (IsEmptyCell == true && panel.AllowEmptyCellSelection == false)
            {
                CellVisualStyle style =
                    GetEffectiveStyle(StyleType.Empty);

                return (style.Background.GetBrush(r));
            }

            if (IsSelectable == false)
            {
                CellVisualStyle style =
                    GetEffectiveStyle(StyleType.NotSelectable);

                return (style.Background.GetBrush(r));
            }

            if (type == CellHighlightMode.None)
            {
                CellVisualStyle style =
                    GetEffectiveStyle(false);

                return (style.Background.GetBrush(r));
            }
         
            CellVisualStyle contentStyle = GetContentStyle();

            return (contentStyle.Background.GetBrush(r));
        }

        #endregion

        #region GetContentStyle

        private CellVisualStyle GetContentStyle()
        {
            if (GridPanel.SelectionGranularity == SelectionGranularity.Row)
            {
                StyleState cellState = StyleState.Default;

                Rectangle t = ViewRect;
                Rectangle r = GridRow.Bounds;
                r.Intersect(t);

                if (r.Contains(SuperGrid.PointToClient(Control.MousePosition)))
                    cellState |= StyleState.MouseOver;

                if (IsSelected == true)
                    cellState |= StyleState.Selected;

                return (GetEffectiveStyle(cellState));
            }

            return (GetEffectiveStyle());
        }

        #endregion

        #region GetCellHighlightMode

        private CellHighlightMode GetCellHighlightMode(GridPanel panel)
        {
            if (panel.SelectionGranularity != SelectionGranularity.Cell)
            {
                switch (panel.RowHighlightType)
                {
                    case RowHighlightType.None:
                        return (CellHighlightMode.None);

                    case RowHighlightType.PrimaryColumnOnly:
                        return (IsPrimaryCell == true 
                            ? CellHighlightMode.Full : CellHighlightMode.None);

                    default:
                        return (CellHighlightMode.Full);
                }
            }

            if (GridRow.IsSelected == true)
                return (CellHighlightMode.Full);

            return (GridColumn.CellHighlightMode);
        }

        #endregion

        #endregion

        #region RenderCellBorder

        private void RenderCellBorder(
            Graphics g, GridPanel panel, CellVisualStyle style, Rectangle r)
        {
            Rectangle t = GetBorderRect(panel, style, r);

            style.RenderBorder(g, t);
        }

        #region GetBorderRect

        private Rectangle GetBorderRect(
            GridPanel panel, CellVisualStyle style, Rectangle r)
        {
            r.X += style.Margin.Left;
            r.Width -= style.Margin.Horizontal;

            r.Y += style.Margin.Top;
            r.Height -= style.Margin.Vertical;

            if (panel.GridLines == GridLines.Horizontal || panel.GridLines == GridLines.Both)
                r.Height--;

            if (panel.GridLines == GridLines.Vertical || panel.GridLines == GridLines.Both)
                r.Width--;

            return (r);
        }

        #endregion

        #endregion

        #region RenderGridBorder

        private void RenderGridBorder(Graphics g,
            GridPanel panel, Rectangle r)
        {
            if (panel.GridLines != GridLines.None)
            {
                GridPanelVisualStyle pstyle = panel.GetEffectiveStyle();

                if (panel.GridLines == GridLines.Both)
                    RenderBothGridBorders(g, pstyle, r);

                else if (panel.GridLines == GridLines.Vertical)
                    RenderVGridBorder(g, pstyle, r);

                else if (panel.GridLines == GridLines.Horizontal)
                    RenderHGridBorder(g, pstyle, r);
            }
        }

        #region RenderBothGridBorders

        private void RenderBothGridBorders(
            Graphics g, GridPanelVisualStyle pstyle, Rectangle r)
        {
            if (pstyle.VerticalLineColor == pstyle.HorizontalLineColor &&
                pstyle.VerticalLinePattern == pstyle.HorizontalLinePattern)
            {
                if (pstyle.VerticalLinePattern != LinePattern.None &&
                    pstyle.VerticalLinePattern != LinePattern.NotSet)
                {
                    using (Pen pen = new Pen(pstyle.VerticalLineColor))
                    {
                        pen.DashStyle = (DashStyle)pstyle.VerticalLinePattern;
                            pen.DashStyle = (DashStyle)pstyle.VerticalLinePattern;

                        pen.DashOffset = r.Top % 2;
                        g.DrawLine(pen, r.Right - 1, r.Top, r.Right - 1, r.Bottom);

                        pen.DashOffset = r.X % 2;
                        g.DrawLine(pen, r.X, r.Top - 1, r.Right - 1, r.Top - 1);
                        g.DrawLine(pen, r.X, r.Bottom - 1, r.Right - 1, r.Bottom - 1);
                    }
                }
            }
            else
            {
                RenderHGridBorder(g, pstyle, r);
                RenderVGridBorder(g, pstyle, r);
            }
        }

        #endregion

        #region RenderHGridBorder

        private void RenderHGridBorder(Graphics g, GridPanelVisualStyle pstyle, Rectangle r)
        {
            if (pstyle.HorizontalLinePattern != LinePattern.None &&
                pstyle.HorizontalLinePattern != LinePattern.NotSet)
            {
                using (Pen pen = new Pen(pstyle.HorizontalLineColor))
                {
                    pen.DashStyle = (DashStyle) pstyle.HorizontalLinePattern;
                    pen.DashOffset = r.X % 2;

                    g.DrawLine(pen, r.X, r.Top - 1, r.Right - 1, r.Top - 1);
                    g.DrawLine(pen, r.X, r.Bottom - 1, r.Right - 1, r.Bottom - 1);
                }
            }
        }

        #endregion

        #region RendervGridBorder

        private void RenderVGridBorder(Graphics g, GridPanelVisualStyle pstyle, Rectangle r)
        {
            if (pstyle.VerticalLinePattern != LinePattern.None &&
                pstyle.VerticalLinePattern != LinePattern.NotSet)
            {
                using (Pen pen = new Pen(pstyle.VerticalLineColor))
                {
                    pen.DashStyle = (DashStyle)pstyle.VerticalLinePattern;
                    pen.DashOffset = r.Top % 2;

                    g.DrawLine(pen, r.Right - 1, r.Top, r.Right - 1, r.Bottom);
                }
            }
        }

        #endregion

        #endregion

        #region RenderCellTreeData

        private void RenderCellTreeData(
            Graphics g, GridPanel panel, ref Rectangle r)
        {
            if (panel.ShowTreeButtons == true || panel.ShowTreeLines == true)
            {
                GridRow row = GridRow;
                Rectangle t = row.ExpandButtonBounds;

                if (row.IsVFrozen == false)
                    t.Y -= VScrollOffset;

                if (panel.IsSubPanel == true || GridColumn.IsHFrozen == false)
                    t.X -= HScrollOffset;

                bool hasExpandButton = (panel.ShowTreeButtons == true &&
                    row.ExpandButtonBounds.IsEmpty == false);

                if (panel.ShowTreeLines == true)
                {
                    Rectangle p = r;

                    p.Y--;
                    p.Height++;

                    TreeDisplay.RenderLines(g, panel, p, row, _IndentLevel,
                        panel.LevelIndentSize.Width, panel.TreeButtonIndent, hasExpandButton);
                }

                if (hasExpandButton == true)
                {
                    Point pt = Control.MousePosition;
                    pt = SuperGrid.PointToClient(pt);

                    bool hot = t.Contains(pt);

                    Image image = (row.Expanded == true)
                        ? panel.GetCollapseButton(g, hot) : panel.GetExpandButton(g, hot);

                    ExpandDisplay.RenderButton(g, image, t, r);
                }

                r.X += panel.TreeButtonIndent;
                r.Width -= panel.TreeButtonIndent;
            }

            int n = panel.LevelIndentSize.Width * _IndentLevel;

            r.X += n;
            r.Width -= n;
        }

        #endregion

        #region RenderCellCheckBox

        private void RenderCellCheckBox(
            Graphics g, GridPanel panel, ref Rectangle r)
        {
            if (GridRow.HasCheckBox == true)
            {
                Rectangle t = CheckBoxBounds;

                if (GridRow.IsVFrozen == false)
                    t.Y -= VScrollOffset;

                if (panel.IsSubPanel == true || GridColumn.IsHFrozen == false)
                    t.X -= HScrollOffset;

                Region clip = null;

                if (t.Right >= r.Right || t.Height >= r.Height)
                {
                    Rectangle z = r;
                    z.Width--;
                    z.Height--;

                    clip = g.Clip;
                    g.SetClip(z, CombineMode.Intersect);
                }

                if (GridRow.Checked == true)
                {
                    CheckDisplay.RenderCheckbox(g, t,
                        CheckBoxState.CheckedNormal, ButtonState.Checked);
                }
                else
                {
                    CheckDisplay.RenderCheckbox(g, t,
                        CheckBoxState.UncheckedNormal, ButtonState.Normal);
                }

                if (clip != null)
                    g.Clip = clip;

                r.X += panel.CheckBoxSize.Width + (CheckBoxSpacing * 2);
                r.Width -= (panel.CheckBoxSize.Width + (CheckBoxSpacing * 2));
            }
        }

        #endregion

        #region RenderCellImage

        private void RenderCellImage(Graphics g,
            GridPanel panel, CellVisualStyle style, Rectangle r, CellHighlightMode type)
        {
            Image image = style.GetImage(panel);

            if (image != null)
            {
                r = style.GetImageBounds(image, r);

                r.Intersect(Bounds);

                if (r.Width > 0 && r.Height > 0)
                {
                    if (CanHighlightCell(style, type) == true )
                        DrawHighlightImage(g, image, style, r);
                    else
                        g.DrawImageUnscaledAndClipped(image, r);
                }
            }
        }

        #region CanHighlightCell

        private bool CanHighlightCell(
            CellVisualStyle style, CellHighlightMode type)
        {
            switch (style.ImageHighlightMode)
            {
                case ImageHighlightMode.Always:
                    return (true);

                case ImageHighlightMode.Never:
                    return (false);

                default:
                    if (IsSelected == false)
                        return (false);

                    return (type == CellHighlightMode.Content ||
                            type == CellHighlightMode.Partial);
            }
        }

        #endregion

        #region DrawHighlightImage

        private void DrawHighlightImage(Graphics g, 
            Image image, CellVisualStyle style, Rectangle r)
        {
            Color color = GetSimpleColor(style);

            float[][] ptsArray =
            {
                new float[] {(float)color.R / 255, 0, 0, 0, 0},
                new float[] {0, (float)color.G / 255, 0, 0, 0},
                new float[] {0, 0, (float)color.B / 255, 0, 0},
                new float[] {0f, 0, 0, 1, 0},
                new float[] {.15f, .15f, .15f, 0, 1}
            };

            ColorMatrix clrMatrix = new ColorMatrix(ptsArray);
            ImageAttributes imgAttribs = new ImageAttributes();

            imgAttribs.SetColorMatrix(clrMatrix,
                ColorMatrixFlag.Default, ColorAdjustType.Default);

            g.DrawImage(image, r,
                0, 0, style.Image.Width, image.Height,
                GraphicsUnit.Pixel, imgAttribs);
        }

        #endregion

        #region GetSimpleColor

        private Color GetSimpleColor(CellVisualStyle style)
        {
            Color color1;
            Color color2;

            if (style.Background.BackColorBlend.Colors != null &&
                style.Background.BackColorBlend.Colors.Length > 0)
            {
                Color[] colors = style.Background.BackColorBlend.Colors;

                color1 = colors[0];
                color2 = colors[colors.Length - 1];
            }
            else
            {
                color1 = style.Background.Color1;
                color2 = style.Background.Color2;
            }

            if (color2.IsEmpty == true)
                return (color1);

            switch (style.Alignment)
            {
                case Alignment.TopLeft:
                case Alignment.TopCenter:
                case Alignment.TopRight:
                    return (color1);

                case Alignment.MiddleLeft:
                case Alignment.MiddleCenter:
                case Alignment.MiddleRight:
                    return (Color.FromArgb((color1.R + color2.R) / 2, (color1.G + color2.G) / 2, (color1.B + color2.B) / 2));

                default:
                    return (color2);
            }
        }

        #endregion

        #endregion

        #region RenderCellValue

        //private int _RenderCount;

        private void RenderCellValue(
            Graphics g, CellVisualStyle style, Rectangle r)
        {
            if (r.Width > 0)
            {
                GridColumn column = GridColumn;

                if (column != null)
                {
                    IGridCellEditControl renderer;

                    if (SuperGrid.ActiveEditor != null ||
                        (_mouseRenderer == null || _mouseRenderer.EditorCell != this))
                    {
                        renderer = GetInternalRenderControl();
                    }
                    else
                    {
                        renderer = _mouseRenderer;
                    }

                    if (renderer != null && ((Control)renderer).IsDisposed == false)
                    {
                        CellRender(g, style, renderer);

                        //using (StringFormat sf = new StringFormat())
                        //{
                        //    sf.Alignment = StringAlignment.Far;
                        //    sf.LineAlignment = StringAlignment.Far;
                        //    sf.Trimming = StringTrimming.EllipsisCharacter;

                        //    g.DrawString(GridRow.DataItemIndex.ToString(),
                        //                 SystemFonts.DefaultFont, Brushes.Red, r, sf);

                        //    g.DrawString((++_RenderCount).ToString(),
                        //                 SystemFonts.DefaultFont, Brushes.Red, r, sf);

                        //    sf.LineAlignment = StringAlignment.Near;

                        //    g.DrawString((_MeasureCount).ToString(),
                        //                 SystemFonts.DefaultFont, Brushes.Blue, r, sf);
                        //}
                    }
                }
            }
        }

        #endregion

        #region RenderFocusRect

        private void RenderFocusRect(Graphics g)
        {
            if (IsFocused() == true)
                ControlPaint.DrawFocusRectangle(g, HighLightBounds);
        }

        #region IsFocused

        private bool IsFocused()
        {
            if (IsDesignerHosted == true)
                return (false);

            return (SuperGrid.ContainsFocus == true);
        }

        #endregion

        #endregion

        #endregion

        #region GetProposedSize

        private Size GetProposedSize(Graphics g, CellVisualStyle style, Size constraintSize)
        {
            return (GetProposedSize(g, style, constraintSize, null, false));
        }

        private Size GetProposedSize(Graphics g,
            CellVisualStyle style, Size constraintSize, IGridCellEditControl editor, bool actual)
        {
            Size size = Size.Empty;

            if (IsValueExpression == true && ExpValue != null)
            {
                size = GetModalSize(g, ExpValue.ToString(), style, constraintSize);
            }
            else
            {
                if (editor == null)
                {
                    editor = SuperGrid.EditorCell == this
                        ? GetInternalEditControl() : GetInternalRenderControl();
                }

                if (editor != null)
                {
                    bool suspended = editor.SuspendUpdate;

                    try
                    {
                        editor.SuspendUpdate = true;

                        if (SuperGrid.EditorCell != this)
                            InitializeContext(DataContext.CellProposedSize, editor, style);

                        if (actual == true && editor.CellEditMode == CellEditMode.Modal)
                            size = GetModalSize(g, editor.EditorFormattedValue, style, constraintSize);
                        else
                            size = editor.GetProposedSize(g, this, style, constraintSize);
                    }
                    finally
                    {
                        editor.SuspendUpdate = suspended;
                    }
                }
            }

            string s = null;

            SuperGrid.DoGetCellFormattedValueEvent(this, ref s);

            if (s != null)
            {
                Size size2 = GetModalSize(g, s, style, constraintSize);

                size.Width = Math.Max(size.Width, size2.Width);
                size.Height = Math.Max(size.Height, size2.Height);
            }

            return (size);
        }

        #region GetModalSize

        private Size GetModalSize(Graphics g,
            string text, CellVisualStyle style, Size constraintSize)
        {
            eTextFormat tf = eTextFormat.NoClipping |
                eTextFormat.WordEllipsis | eTextFormat.NoPrefix;

            if (style.AllowWrap == Tbool.True)
                tf |= eTextFormat.WordBreak;

            string s = String.IsNullOrEmpty(text) ? " " : text;

            Size size = (constraintSize.IsEmpty == true)
                            ? TextHelper.MeasureText(g, s, style.Font)
                            : TextHelper.MeasureText(g, s, style.Font, constraintSize, tf);

            return (size);
        }

        #endregion

        #endregion

        #region Mouse support

        #region InternalMouseEnter

        internal override void InternalMouseEnter(EventArgs e)
        {
            base.InternalMouseEnter(e);

            if (GridColumn != null)
            {
                DoRendererMouseEnter();

                SuperGrid.ToolTipText = "";
                _toolTipText = null;

                if (GridPanel.SelectionGranularity != SelectionGranularity.Cell)
                    GridRow.InvalidateRender(GridRow.Bounds);

                SuperGrid.DoCellMouseEnterEvent(this);
            }
        }

        #region DoRendererMouseEnter

        private void DoRendererMouseEnter()
        {
            _needsMouseEnter = true;

            if (SuperGrid.ActiveEditor == null)
            {
                _needsMouseEnter = false;

                if (IsEmptyCell == false)
                {
                    CellVisualStyle style = GetEffectiveStyle();

                    _mouseRenderer = (CellEditMode != CellEditMode.Modal)
                                         ? GetInternalEditControl()
                                         : GetInternalRenderControl();

                    if (_mouseRenderer != null)
                    {
                        switch (CellEditMode)
                        {
                            case CellEditMode.InPlace:
                                InitializeContext(DataContext.CellMouseEvent, _mouseRenderer, style);
                                break;

                            case CellEditMode.NonModal:
                                if (CanModify == true)
                                {
                                    if (SuperGrid.ActiveNonModalEditor == null ||
                                        SuperGrid.ActiveNonModalEditor.CanInterrupt == true)
                                    {
                                        if (SuperGrid.ActiveNonModalEditor != null)
                                            SuperGrid.DeactivateNonModalEditor();

                                        InitializeContext(DataContext.CellMouseEvent, _mouseRenderer, style);

                                        PositionEditControl(_mouseRenderer);

                                        _mouseRenderer.BeginEdit(false);
                                        _mouseRenderer.OnCellMouseEnter(EventArgs.Empty);

                                        SuperGrid.ActivateNonModalEditor(_mouseRenderer, this);
                                    }
                                    else
                                    {
                                        _mouseRenderer = null;
                                        _needsMouseEnter = true;
                                    }
                                }
                                break;
                        }

                        if (_mouseRenderer != null)
                            InvalidateRender();
                    }
                }
                else
                {
                    InvalidateRender();
                }
            }
        }

        #endregion

        #endregion

        #region InternalMouseLeave

        internal override void InternalMouseLeave(EventArgs e)
        {
            if (GridColumn != null)
            {
                DoRendererMouseLeave(e);

                if (_lastHitArea == CellArea.InCellInfo)
                    ProcessCellInfoLeave();

                if (GridPanel.SelectionGranularity != SelectionGranularity.Cell)
                    GridRow.InvalidateRender(GridRow.Bounds);

                SuperGrid.DoCellMouseLeaveEvent(this);

                SuperGrid.ToolTipText = "";
                _toolTipText = null;

                _lastHitArea = CellArea.NoWhere;
            }
                
            base.InternalMouseLeave(e);
        }

        #region DoRendererMouseLeave

        internal void DoRendererMouseLeave(EventArgs e)
        {
            if (_mouseRenderer != null &&
                ((Control) _mouseRenderer).IsDisposed == false)
            {
                if (CellEditMode != CellEditMode.InPlace || _mouseInItem == true)
                    _mouseRenderer.OnCellMouseLeave(e);
            }
                
            _mouseInItem = false;
            _mouseRenderer = null;
            _needsMouseEnter = false;

            InvalidateRender();
        }

        #endregion

        #endregion

        #region InternalMouseMove

        internal override void InternalMouseMove(MouseEventArgs e)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                if (e.Button != MouseButtons.None)
                    ProcessMouseDownMove(e, panel);

                ProcessMouseMove(e);

                base.InternalMouseMove(e);

                DoRendererMouseMove(e);

                SuperGrid.DoCellMouseMoveEvent(this, e);
            }
        }

        #region DoRendererMouseMove

        private void DoRendererMouseMove(MouseEventArgs e)
        {
            if (_needsMouseEnter == true)
                DoRendererMouseEnter();

            if (_mouseRenderer != null &&
                ((Control) _mouseRenderer).IsDisposed == false)
            {
                if (CellEditMode == CellEditMode.InPlace)
                {
                    DoInPlaceMouseMove(e);
                }
                else
                {
                    Rectangle r = GetAdjustedBounds(EditBounds);
                    Point pt = SuperGrid.PointToClient(Control.MousePosition);

                    if (r.Contains(pt) == true)
                        _mouseRenderer.OnCellMouseMove(GetControlMouseArgs(e));
                }
            }
        }

        #region DoInPlaceMouseMove

        private void DoInPlaceMouseMove(MouseEventArgs e)
        {
            if (IsMouseDown == false || _mouseDownInItem == true)
            {
                Rectangle r = GetAdjustedBounds(ContentBounds);
                r = GetEditPanelBounds(_mouseRenderer, r);

                bool inItem = r.Contains(e.Location);

                if (_mouseInItem != inItem)
                {
                    if (inItem == true)
                        _mouseRenderer.OnCellMouseEnter(EventArgs.Empty);
                    else
                        _mouseRenderer.OnCellMouseLeave(EventArgs.Empty);

                    _mouseInItem = inItem;
                }

                if (_mouseInItem == true)
                    _mouseRenderer.OnCellMouseMove(GetControlMouseArgs(e, r));
            }
        }

        #endregion

        #endregion

        #region ProcessMouseDownMove

        private void ProcessMouseDownMove(MouseEventArgs e, GridPanel panel)
        {
            if (_mouseDownHitCell != null)
            {
                if (_mouseDownHitArea == CellArea.InContent)
                {
                    Rectangle r = ViewRect;

                    if (MouseDownPoint.Y < r.Y)
                    {
                        int n = SuperGrid.PrimaryGrid.FixedRowHeight -
                                SuperGrid.PrimaryGrid.FixedHeaderHeight;

                        r.Y -= n;
                        r.Height += n;
                    }

                    if ((e.Y >= r.Y && e.Y < r.Bottom) && (e.X >= r.X && e.X < r.Right))
                    {
                        SuperGrid.DisableAutoScrolling();

                        ProcessInContent(panel, e);
                    }
                    else
                    {
                        SuperGrid.EnableAutoScrolling(
                            AutoScrollEnable.Horizontal | AutoScrollEnable.Vertical, r);
                    }
                }
            }
        }

        #region ProcessInContent

        private void ProcessInContent(GridPanel panel, MouseEventArgs e)
        {
            Point pt = e.Location;

            Rectangle t = SViewRect;
            pt.X = Math.Min(t.Right - 1, pt.X);
            pt.X = Math.Max(t.X, pt.X);

            Rectangle v = panel.BoundsRelative;
            v.Location = panel.PointToScroll(v.Location);
            pt.X = Math.Min(v.Right - 1, pt.X);
            pt.X = Math.Max(v.X, pt.X);

            GridCell lastCell = _hitCell;

            _hitCell = GetCellAt(panel, pt);

            if (DragStarted(panel, pt, e) == false)
            {
                if (_hitCell != null && _hitArea == CellArea.InContent)
                {
                    if (panel.MultiSelect == true)
                    {
                        if (lastCell != null)
                            lastCell.IsMouseOver = false;

                        _hitCell.IsMouseOver = true;

                        if (panel.CellDragBehavior == CellDragBehavior.ExtendSelection)
                        {
                            if (panel.LastProcessedItem is GridCell &&
                                _hitCell != panel.LastProcessedItem)
                            {
                                ProcessExtendSelection(panel, true);
                            }
                        }
                    }
                }
            }
        }

        #region DragStarted

        private bool DragStarted(GridPanel panel, Point pt, MouseEventArgs e)
        {
            if (IsDragSelection == true)
            {
                if (IsDragStarted == false)
                {
                    if (DragDrop.DragStarted(_mouseDownPoint, pt) == true)
                    {
                        IsDragStarted = true;

                        if (SuperGrid.DoItemDragEvent(this, e) == true)
                        {
                            IsDragSelection = false;

                            ExtendSelection(panel);
                        }
                    }
                }

                return (true);
            }

            return (false);
        }

        #endregion

        #region GetCellAt

        private GridCell GetCellAt(GridPanel panel, Point pt)
        {
            Rectangle t = ViewRect;
            pt.X = Math.Min(t.Right - 1, pt.X);
            pt.X = Math.Max(t.X, pt.X);

            Rectangle v = panel.BoundsRelative;
            v.Location = panel.PointToScroll(v.Location);
            pt.X = Math.Min(v.Right - 1, pt.X);
            pt.X = Math.Max(v.X, pt.X);

            GridRow row = panel.InternalGetElementAt(pt.X, pt.Y) as GridRow;

            while (row != null)
            {
                GridElement item = row.InternalGetElementAt(pt.X, pt.Y);

                if (item is GridCell)
                    return (item as GridCell);

                row = item as GridRow;
            }

            return (null);
        }

        #endregion

        #endregion

        #endregion

        #region ProcessMouseMove

        private void ProcessMouseMove(MouseEventArgs e)
        {
            _hitArea = GetCellAreaAt(e);
            _hitCell = this;

            SuperGrid.GridCursor = Cursors.Default;

            if (_hitArea != _lastHitArea)
            {
                ProcessToolTipText(e);

                _lastHitArea = _hitArea;

                InvalidateRender();
            }

            base.InternalMouseMove(e);
        }

        #region ProcessToolTipText

        private void ProcessToolTipText(MouseEventArgs e)
        {
            if (_hitArea == CellArea.InContent)
                ProcessCellToolTip();
            
            else if (_hitArea == CellArea.InCellInfo)
                ProcessCellInfoEnter(e);

            if (_lastHitArea == CellArea.InCellInfo)
                ProcessCellInfoLeave();
        }

        #region ProcessCellToolTip

        private void ProcessCellToolTip()
        {
            SetToolTip(GridPanel.ShowToolTips ? GetToolTipText() : string.Empty);
        }

        #region GetToolTipText

        private string GetToolTipText()
        {
            if (IsEmptyCell == false)
            {
                if (_toolTipText == null)
                {
                    _toolTipText = "";

                    CellVisualStyle style = GetEffectiveStyle();

                    Rectangle r = GetAdjustedBounds(ContentBounds);

                    if (_mouseRenderer != null &&
                        ((Control)_mouseRenderer).IsDisposed == false)
                    {
                        Size size = new
                            Size(style.AllowWrap == Tbool.True ? r.Width : 0, 0);

                        using (Graphics g = SuperGrid.CreateGraphics())
                            size = GetProposedSize(g, style, size, _mouseRenderer, true);

                        if (size.Width > r.Width || size.Height > r.Height)
                            _toolTipText = _mouseRenderer.EditorFormattedValue;
                    }
                }
            }

            return (_toolTipText);
        }

        #endregion

        #endregion

        #region ProcessCellInfoEnter

        private void ProcessCellInfoEnter(MouseEventArgs e)
        {
            SetToolTip(SuperGrid.DoCellInfoEnterEvent(this, e.Location) ? "" : InfoText);
        }

        #endregion

        #region ProcessCellInfoLeave

        private void ProcessCellInfoLeave()
        {
            SuperGrid.DoCellInfoLeaveEvent(this);
        }

        #endregion

        #region SetToolTip

        private void SetToolTip(string toolTipText)
        {
            if (SuperGrid.ToolTipText != _toolTipText)
                SuperGrid.ToolTip.Hide(SuperGrid);

            SuperGrid.ToolTipText = toolTipText;
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region InternalMouseDown

        internal override void InternalMouseDown(MouseEventArgs e)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                SuperGrid.DoCellMouseDownEvent(this, e);

                _mouseDownPoint = e.Location;

                IsDragSelection = false;
                IsDragStarted = false;

                if (_hitArea == CellArea.InContent || _hitArea == CellArea.InCheckBox)
                {
                    if (CanSetActiveCell(panel, _hitCell) == true)
                    {
                        if (panel.SelectionGranularity == SelectionGranularity.Cell)
                        {
                            _mouseDownHitArea = _hitArea;
                            _mouseDownHitCell = _hitCell;

                            panel.LastProcessedItem = _hitCell;

                            if (panel.IsItemSelected(this) == false ||
                                ((Control.ModifierKeys & (Keys.Control | Keys.Shift)) != Keys.None))
                            {
                                ExtendSelection(panel);
                            }
                            else
                            {
                                IsDragSelection = true;

                                Capture = true;
                            }
                        }
                        else
                        {
                            _mouseDownHitArea = CellArea.NoWhere;
                        }
                    }
                    else
                    {
                        _mouseDownHitArea = CellArea.NoWhere;
                    }
                }

                DoRendererMouseDown(e);
            }

            base.InternalMouseDown(e);
        }

        #region DoRendererMouseDown

        private void DoRendererMouseDown(MouseEventArgs e)
        {
            if (CanModify == true)
            {
                if (_needsMouseEnter == true)
                    DoRendererMouseEnter();

                if (_mouseRenderer != null)
                {
                    if (_mouseInItem == true)
                    {
                        _mouseDownInItem = true;

                        _mouseRenderer.OnCellMouseDown(GetControlMouseArgs(e));
                    }
                }
            }
        }

        #endregion

        #region CanSetActiveCell

        internal bool CanSetActiveCell(GridPanel panel, GridCell cell)
        {
            if (SuperGrid.ActiveCell != cell)
            {
                if (cell.IsSelectable == false ||
                    (cell.IsEmptyCell == true && panel.AllowEmptyCellSelection == false))
                {
                    SystemSounds.Beep.Play();

                    return (false);
                }

                GridCell ecell = SuperGrid.EditorCell;

                if (ecell != null)
                {
                    if (ecell.EndEdit() == false)
                        return (false);
                }

                if (cell.GridRow.IsActive == false)
                {
                    if (cell.GridRow.CanSetActiveRow(false) == false)
                        return (false);

                    if (panel.SetActiveRow(cell.GridRow, true) == false)
                        return (false);
                }

                if (SuperGrid.DoCellActivatingEvent(panel, SuperGrid.ActiveCell, cell) == true)
                    return (false);

                if (panel.SelectionGranularity == SelectionGranularity.Cell)
                {
                    SuperGrid.ActiveCell = cell;
                    SuperGrid.ActiveElement = cell;

                    panel.LastProcessedItem = cell;
                }
            }

            return (true);
        }

        #endregion

        #region ProcessExtendSelection

        private void ProcessExtendSelection(GridPanel panel, bool extend)
        {
            bool ckey = (panel.MultiSelect == true) ?
                ((Control.ModifierKeys & Keys.Control) == Keys.Control) : false;

            if (ckey == true)
                ProcessControlExtend(panel, extend);
            else
                ProcessNonControlExtend(panel, extend);

            panel.LastProcessedItem = _hitCell;
        }

        #region ProcessControlExtend

        private void ProcessControlExtend(GridPanel panel, bool extend)
        {
            if (!(panel.LastProcessedItem is GridCell)) return;
           GridCell cell = (GridCell)panel.LastProcessedItem;

            int startRowIndex = cell.GridRow.GridIndex;
            int startColumnIndex = panel.Columns.GetDisplayIndex(cell.GridColumn);

            int[] map = panel.Columns.DisplayIndexMap;

            if (extend == false)
            {
                int columnIndex = map[startColumnIndex];

                panel.SetSelectedCells(startRowIndex, columnIndex,
                    1, 1, !panel.IsCellSelected(startRowIndex, columnIndex));

                cell.InvalidateRender();

                panel.ColumnHeader.InvalidateRowHeader();
            }
            else
            {
                int endRowIndex = _hitCell.GridRow.GridIndex;
                int endColumnIndex = panel.Columns.GetDisplayIndex(_hitCell.GridColumn);

                if (endColumnIndex - startColumnIndex != 0)
                {
                    SelectControlColumn(panel, map,
                        startColumnIndex, endColumnIndex, startRowIndex);
                }

                if (endRowIndex - startRowIndex != 0)
                {
                    SelectControlRow(panel, map,
                        startRowIndex, endRowIndex, endColumnIndex);
                }
            }
        }

        #region SelectControlColumn

        private void SelectControlColumn(GridPanel panel, int[] map,
            int startColumnIndex, int endColumnIndex, int startRowIndex)
        {
            int endRowIndex = startRowIndex;

            panel.NormalizeIndices(true,
                _anchorColumnIndex, ref startColumnIndex, ref endColumnIndex);

            panel.NormalizeIndices(false,
                _anchorRowIndex, ref startRowIndex, ref endRowIndex);

            if (endRowIndex > _anchorRowIndex)
                startRowIndex = _anchorRowIndex;

            else if (startRowIndex < _anchorRowIndex)
                endRowIndex = _anchorRowIndex;

            for (int i = startRowIndex; i <= endRowIndex; i++)
            {
                for (int j = startColumnIndex; j <= endColumnIndex; j++)
                {
                    if (j != _anchorColumnIndex)
                    {
                        int columnIndex = map[j];

                        panel.SetSelectedCells(i, columnIndex, 1, 1,
                            !panel.IsCellSelected(i, columnIndex));
                    }
                }
            }

            InvalidateCells(panel,
                startRowIndex, endRowIndex, startColumnIndex, endColumnIndex);
        }

        #endregion

        #region SelectControlRow

        private void SelectControlRow(GridPanel panel, int[] map,
            int startRowIndex, int endRowIndex, int endColumnIndex)
        {
            int startColumnIndex = endColumnIndex;

            panel.NormalizeIndices(false,
                _anchorColumnIndex, ref startColumnIndex, ref endColumnIndex);

            panel.NormalizeIndices(true,
                _anchorRowIndex, ref startRowIndex, ref endRowIndex);

            if (endColumnIndex > _anchorColumnIndex)
                startColumnIndex = _anchorColumnIndex;

            else if (startColumnIndex < _anchorColumnIndex)
                endColumnIndex = _anchorColumnIndex;

            for (int i = startRowIndex; i <= endRowIndex; i++)
            {
                for (int j = startColumnIndex; j <= endColumnIndex; j++)
                {
                    if (i != _anchorRowIndex)
                    {
                        int columnIndex = map[j];

                        panel.SetSelectedCells(i, columnIndex, 1, 1,
                            !panel.IsCellSelected(i, columnIndex));
                    }
                }
            }

            InvalidateCells(panel,
                startRowIndex, endRowIndex, startColumnIndex, endColumnIndex);
        }

        #endregion

        #endregion

        #region ProcessNonControlExtend

        private void ProcessNonControlExtend(GridPanel panel, bool extend)
        {
            bool skey = (panel.MultiSelect == true) ?
                ((Control.ModifierKeys & Keys.Shift) == Keys.Shift) : false;

            if (skey == false ||
                panel.SelectionRowAnchor == null ||
                panel.SelectionColumnAnchor == null)
            {
                panel.SelectionRowAnchor = _mouseDownHitCell.GridRow;
                panel.SelectionColumnAnchor = _mouseDownHitCell.GridColumn;
            }

            ExtendSelection(panel, _hitCell, extend);
        }

        #endregion

        #region InvalidateCells

        private void InvalidateCells(GridPanel panel,
            int startRowIndex, int endRowIndex, int startColumnIndex, int endColumnIndex)
        {
            Rectangle r = Rectangle.Empty;

            int n = panel.Columns.Count;

            if ((uint)startColumnIndex < n && (uint)endColumnIndex < n)
            {
                int sci = panel.Columns.DisplayIndexMap[startColumnIndex];
                int eci = panel.Columns.DisplayIndexMap[endColumnIndex];

                Rectangle u = panel.Columns[sci].BoundsRelative;
                u = Rectangle.Union(u, panel.Columns[eci].BoundsRelative);

                if (startColumnIndex <= endColumnIndex)
                    InvalidateCellArea(panel, startRowIndex, endRowIndex, u, ref r);

                if (startRowIndex != endRowIndex)
                {
                    int start = startRowIndex;
                    int end = endRowIndex;

                    if (start > end)
                    {
                        int temp = start;
                        start = end;
                        end = temp;
                    }

                    InvalidateCellArea(panel, start, end, u, ref r);
                }

                if (Parent != null)
                    r.X -= HScrollOffset;

                if (IsVFrozen == false)
                    r.Y -= VScrollOffset;
            }

            panel.InvalidateRender(r);
        }

        #region InvalidateCellArea

        private void InvalidateCellArea(GridPanel panel,
            int sri, int eri, Rectangle u, ref Rectangle r)
        {
            GridElement start = panel.GetRowFromIndex(sri);
            GridElement end = panel.GetRowFromIndex(eri);

            if (start != null && end != null)
            {
                GridContainer gcr = panel.GetRowFromIndex(sri);
                GridContainer gce = panel.GetRowFromIndex(eri);

                Rectangle t = (gcr != null) ? gcr.BoundsRelative : Rectangle.Empty;
                Rectangle t2 = (gce != null) ? gce.BoundsRelative : Rectangle.Empty;

                t.Height = t2.Bottom - t.Top;

                t.X = u.X;
                t.Width = u.Width;

                r = (r.IsEmpty == true) ? t : Rectangle.Union(r, t);
            }
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region InternalMouseUp

        internal override void InternalMouseUp(MouseEventArgs e)
        {
            if (CanModify == true)
                DoRendererMouseUp(e);

            _mouseDownInItem = false;

            SuperGrid.DoCellMouseUpEvent(this, e);

            if (InfoImageBounds.Contains(e.X, e.Y) == true)
            {
                SuperGrid.DoCellInfoClickEvent(this, e);
            }
            else
            {
                if (_mouseDownHitCell == this)
                {
                    if (SuperGrid.DoCellClickEvent(this, e) == false)
                    {
                        GridPanel panel = GridPanel;

                        if (panel != null)
                        {
                            if (IsDragSelection == true)
                            {
                                if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                                    ExtendSelectionEx(panel);
                            }

                            if (_hitArea == CellArea.InCheckBox)
                            {
                                ProcessInCheckBox(panel);
                            }
                            else
                            {
                                if (panel.MouseEditMode == MouseEditMode.SingleClick)
                                {
                                    if (_hitArea == CellArea.InContent &&
                                        CanSetActiveCell(GridPanel, this) == true)
                                    {
                                        BeginEdit(true);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            IsDragSelection = false;

            base.InternalMouseUp(e);
        }

        #region ProcessInCheckBox

        private void ProcessInCheckBox(GridPanel panel)
        {
            GridRow.ProcessInCheckBox(panel, GridRow);
        }

        #endregion

        #region DoRendererMouseUp

        private void DoRendererMouseUp(MouseEventArgs e)
        {
            if (_mouseRenderer != null)
            {
                if (_mouseDownInItem == true)
                {
                    _mouseRenderer.OnCellMouseUp(GetControlMouseArgs(e));

                    SuperGrid.Focus();
                }
            }
        }

        #endregion

        #endregion

        #region InternalMouseDoubleClick

        internal override void InternalMouseDoubleClick(MouseEventArgs e)
        {
            if (InfoImageBounds.Contains(e.X, e.Y) == true)
            {
                SuperGrid.DoCellInfoDoubleClickEvent(this, e);
            }
            else
            {
                if (SuperGrid.DoCellDoubleClickEvent(this, e) == false)
                {
                    if (_hitArea == CellArea.InContent &&
                        CanSetActiveCell(GridPanel, this) == true)
                    {
                        BeginEdit(true);
                    }
                }

                DoRendererMouseDoubleClick(e);
            }
        }

        #endregion

        #region DoRendererMouseDoubleClick

        private void DoRendererMouseDoubleClick(MouseEventArgs e)
        {
            if (CanModify == true)
            {
                if (_mouseRenderer != null)
                {
                    if (_mouseDownInItem == true)
                    {
                        _mouseRenderer.OnCellMouseDown(GetControlMouseArgs(e));
                    }
                }
            }
        }

        #endregion

        #endregion

        #region GetControlMouseArgs

        private MouseEventArgs GetControlMouseArgs(MouseEventArgs e)
        {
            Rectangle r = GetAdjustedBounds(ContentBounds);
            r = GetEditPanelBounds(_mouseRenderer, r);

            return (GetControlMouseArgs(e, r)); 
        }

        private MouseEventArgs
            GetControlMouseArgs(MouseEventArgs e, Rectangle r)
        {
            int x = e.X - r.X;
            int y = e.Y - r.Y;

            MouseEventArgs args = new
                MouseEventArgs(e.Button, e.Clicks, x, y, e.Delta);

            return (args);
        }

        #endregion

        #region ExtendSelection

        internal void ExtendSelection(GridPanel panel, GridCell endCell, bool extend)
        {
            int startRowIndex = panel.SelectionRowAnchor.GridIndex;
            int startColumnIndex = panel.Columns.GetDisplayIndex(panel.SelectionColumnAnchor);

            int endRowIndex = endCell.GridRow.GridIndex;
            int endColumnIndex = panel.Columns.GetDisplayIndex(endCell.GridColumn);

            if (startRowIndex < 0)
                startRowIndex = endRowIndex;

            panel.NormalizeIndices(false, 0, ref startRowIndex, ref endRowIndex);
            panel.NormalizeIndices(false, 0, ref startColumnIndex, ref endColumnIndex);

            if (startColumnIndex < 0)
                startColumnIndex = 0;

            if (endColumnIndex < 0)
                endColumnIndex = 0;

            if (panel.OnlyCellsSelected(startRowIndex, startColumnIndex, endRowIndex, endColumnIndex) == false)
            {
                if (panel.IsGrouped == false &&
                    (panel.SelectedCellCount < 10 && panel.SelectedRowCount < 10 && panel.SelectedColumnCount < 10))
                {
                    foreach (GridCell cell in panel.SelectedCells)
                        cell.IsSelected = false;

                    foreach (GridColumn col in panel.SelectedColumns)
                        col.IsSelected = false;

                    foreach (GridContainer row in panel.SelectedRows)
                    {
                        if (row != null)
                            row.IsSelected = false;
                    }
                }
                else
                {
                    panel.ClearAll(extend == false);
                }

                if (startRowIndex >= 0 && endRowIndex >= 0)
                {
                    int[] map = panel.Columns.DisplayIndexMap;

                    for (int i = startColumnIndex; i <= endColumnIndex; i++)
                    {
                        int columnIndex = map[i];

                        panel.SetSelectedCells(startRowIndex, columnIndex,
                                               endRowIndex - startRowIndex + 1, 1, true);
                    }

                    GridCell lastCell = (Control.MouseButtons == MouseButtons.None)
                        ? SuperGrid.LastActiveCell : panel.LastProcessedItem as GridCell;

                    if (lastCell != null)
                    {
                        if (lastCell.ColumnIndex < startColumnIndex)
                            startColumnIndex = lastCell.ColumnIndex;

                        else if (lastCell.ColumnIndex > endColumnIndex)
                            endColumnIndex = lastCell.ColumnIndex;

                        if (lastCell.GridRow.GridIndex < startRowIndex)
                            startRowIndex = lastCell.GridRow.GridIndex;

                        else if (lastCell.GridRow.GridIndex > endRowIndex)
                            endRowIndex = lastCell.GridRow.GridIndex;
                    }

                    InvalidateCells(panel, startRowIndex,
                                    endRowIndex, startColumnIndex, endColumnIndex);
                }

                panel.ColumnHeader.InvalidateRowHeader();
            }
        }

        #endregion

        #region GetNextCell

        internal GridCell GetNextCell(bool canSelect)
        {
            return (GetNextCell(canSelect, true));
        }

        internal GridCell GetNextCell(bool canSelect, bool multiRow)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                int[] map = panel.Columns.DisplayIndexMap;
                int index = panel.Columns.GetDisplayIndex(GridColumn);

                int anchor = index;

                GridRow row = GridRow;

                while (row != null)
                {
                    if (row.AllowSelection == true)
                    {
                        while (++index < map.Length)
                        {
                            if (multiRow == false && index == anchor)
                                return (null);

                            GridColumn column = panel.Columns[map[index]];

                            if (column.Visible == true &&
                                (canSelect == false || column.AllowSelection == true))
                            {
                                GridCell cell = row.GetCell(
                                    column.ColumnIndex, panel.AllowEmptyCellSelection);

                                if (cell != null)
                                {
                                    if (canSelect == false || cell.AllowSelection == true)
                                        return (cell);
                                }
                            }
                        }
                    }

                    if (multiRow == true)
                        row = row.NextVisibleRow as GridRow;

                    index = -1;
                }
            }

            return (null);
        }

        #endregion

        #region GetPreviousCell

        internal GridCell GetPreviousCell(bool canSelect)
        {
            return (GetPreviousCell(canSelect, true));
        }

        internal GridCell GetPreviousCell(bool canSelect, bool multiRow)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                int[] map = panel.Columns.DisplayIndexMap;
                int index = panel.Columns.GetDisplayIndex(GridColumn);

                int anchor = index;

                GridRow row = GridRow;

                while (row != null)
                {
                    if (row.AllowSelection == true)
                    {
                        while (--index >= 0)
                        {
                            if (multiRow == false && index == anchor)
                                return (null);

                            GridColumn column = panel.Columns[map[index]];

                            if (column.Visible == true &&
                                (canSelect == false || column.AllowSelection == true))
                            {
                                GridCell cell = row.GetCell(
                                    column.ColumnIndex, panel.AllowEmptyCellSelection);

                                if (cell != null)
                                {
                                    if (canSelect == false || cell.AllowSelection == true)
                                        return (cell);
                                }
                            }
                        }
                    }

                    if (multiRow == true)
                        row = row.PrevVisibleRow as GridRow;

                    index = map.Length;
                }
            }

            return (null);
        }

        #endregion

        #region GetCellAreaAt

        /// <summary>
        /// Returns element at specified mouse coordinates
        /// </summary>
        /// <param name="e">Mouse event arguments</param>
        /// <returns>Reference to child Cell element or null
        /// if no element at specified coordinates</returns>
        protected virtual CellArea GetCellAreaAt(MouseEventArgs e)
        {
            return GetCellAreaAt(e.X, e.Y);
        }

        /// <summary>
        /// Returns element at specified mouse coordinates
        /// </summary>
        /// <param name="x">Horizontal position</param>
        /// <param name="y">Vertical position</param>
        /// <returns>Reference to child element or null
        /// if no element at specified coordinates</returns>
        protected virtual CellArea GetCellAreaAt(int x, int y)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                if (InfoImageBounds.Contains(x, y) == true)
                    return (CellArea.InCellInfo);

                if (IsPrimaryCell == true)
                {
                    GridRow cellRow = GridRow;

                    if (cellRow != null)
                    {
                        Rectangle r = cellRow.ExpandButtonBounds;

                        if (cellRow.IsVFrozen == false)
                            r.Y -= VScrollOffset;

                        if (panel.IsSubPanel == true || GridColumn.IsHFrozen == false)
                            r.X -= HScrollOffset;

                        if (r.Contains(x, y) == true)
                            return (CellArea.InExpandButton);

                        if (CanModify == true)
                        {
                            r = cellRow.CheckBoxBounds;

                            if (cellRow.IsVFrozen == false)
                                r.Y -= VScrollOffset;

                            if (panel.IsSubPanel == true || GridColumn.IsHFrozen == false)
                                r.X -= HScrollOffset;

                            if (r.Contains(x, y) == true)
                                return (CellArea.InCheckBox);
                        }
                    }
                }
            }

            return (CellArea.InContent);
        }

        #endregion

        #region EnsureVisible

        ///<summary>
        /// Ensures that the given cell is visibly centered
        /// (as much as possible) in the grid display.
        ///</summary>
        ///<param name="center"></param>
        public override void EnsureVisible(bool center)
        {
            if (GridRow.Visible == true && GridColumn.Visible == true)
            {
                GridRow.EnsureVisible(center);
                GridColumn.EnsureVisible(center);
            }
        }

        #endregion

        #region SetActive

        ///<summary>
        /// Sets the given cell as Active
        ///</summary>
        ///<returns>true - if successful</returns>
        public bool SetActive()
        {
            return (SetActive(false));
        }

        ///<summary>
        /// Makes the given cell active and
        /// optionally selects the given cell
        ///</summary>
        ///<returns>true, if successful</returns>
        public bool SetActive(bool select)
        {
            if (GridPanel.SetActiveCell(this) == true)
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

        #region Editor support

        #region GetEditControl

        internal IGridCellEditControl GetEditControl(Type type, object[] args)
        {
            if (type != null)
            {
                IGridCellEditControl editor =
                    Activator.CreateInstance(type, args) as IGridCellEditControl;

                if (editor is Control == false)
                    throw new Exception("Edit/Render Type must be based on 'Control'.");

                editor.EditorPanel = new EditorPanel();
                editor.EditorPanel.Visible = false;
                editor.EditorPanel.Controls.Add((Control) editor);

                return (editor);
            }

            return (null);
        }

        #endregion

        #region GetInternalEditControl

        internal IGridCellEditControl GetInternalEditControl()
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                if (AllowEdit == true && GridColumn.AllowEdit == true &&
                    GridRow.AllowEdit == true && panel.AllowEdit== true)
                {
                    Type editorType = _EditorInfo != null ? _EditorInfo.EditorType : null;
                    object[] editorParams = _EditorInfo != null ? _EditorInfo.EditorParams : null;

                    SuperGrid.DoGetEditorEvent(panel, this, ref editorType, ref editorParams);

                    EditorType = editorType;
                    EditorParams = editorParams;

                    return (EditControl ?? (GridColumn != null ? GridColumn.EditControl : null));
                }
            }

            return (null);
        }

        #endregion

        #region GetInternalRenderControl

        private IGridCellEditControl GetInternalRenderControl()
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                Type renderType = _EditorInfo != null ? _EditorInfo.RenderType : null;
                object[] renderParams = _EditorInfo != null ? _EditorInfo.RenderParams : null;

                SuperGrid.DoGetRendererEvent(panel, this, ref renderType, ref renderParams);

                RenderType = renderType;
                RenderParams = renderParams;
                
                return (RenderControl ?? (GridColumn != null ? GridColumn.RenderControl : null));
            }

            return (null);
        }

        #endregion

        #region BeginEdit

        ///<summary>
        /// Begins a Modal cell edit operation using
        /// the defined column or cell Modal EditControl.
        ///</summary>
        ///<param name="selectAll"></param>
        ///<returns></returns>
        public IGridCellEditControl BeginEdit(bool selectAll)
        {
            return (BeginEdit(selectAll, null));
        }

        ///<summary>
        /// Begins a Modal cell edit operation using the defined column
        /// or cell Modal EditControl.  If a KeyEvent is provided then
        /// the event will be presented to the editor control. 
        ///</summary>
        ///<returns>Cell edit control</returns>
        public IGridCellEditControl BeginEdit(bool selectAll, KeyEventArgs e)
        {
            GridCell ecell = SuperGrid.EditorCell;

            if (ecell == this)
                return (GetInternalEditControl());

            if (ecell != null)
                throw new Exception("Modal Cell edit is already in process.");

            if (Visible == true && CanModify == true)
            {
                GridPanel.FlushSelected();

                IGridCellEditControl editor = GetInternalEditControl();

                if (editor != null && editor.CellEditMode == CellEditMode.Modal)
                {
                    CellVisualStyle style = GetEffectiveStyle(StyleState.Default);

                    DoRendererMouseLeave(EventArgs.Empty);
                    EnsureVisible();

                    GetCurrentContentSize(style, editor);

                    InitializeContext(DataContext.CellEdit, editor, style);

                    if (SuperGrid.DoBeginEditEvent(GridPanel, this, editor) == false)
                    {
                        SuperGrid.ActiveEditor = editor;
                        SuperGrid.EditorCell = this;

                        PositionEditPanel(editor);

                        editor.EditorPanel.Show();
                        editor.EditorPanel.BringToFront();

                        if (editor.BeginEdit(selectAll) == false)
                        {
                            UpdateInfoWindow();

                            FocusEditor(editor);

                            if (e != null)
                                SuperGrid.PressKeyDown((ushort)e.KeyCode);

                            return (editor);
                        }

                        CloseEdit(editor);
                    }
                }
            }

            return (null);
        }

        #endregion

        #region EndEdit

        ///<summary>
        /// This routine ends an in-progress Modal cell edit operation.
        ///</summary>
        ///<returns>true if ended</returns>
        public bool EndEdit()
        {
            IGridCellEditControl editor = SuperGrid.ActiveEditor;

            if (editor != null && CellEditMode == CellEditMode.Modal)
            {
                if (GridRow.EditorDirty == true)
                {
                    if (ValidateCellEdit(editor) == false)
                        return (false);

                    GridRow.EditorDirty = false;

                    object value = Value;

                    if (UpdateCellValue(editor) == true)
                        Value = value;
                }

                if (SuperGrid.DoEndEditEvent(GridPanel, this, editor) == false)
                {
                    if (editor.EndEdit() == false)
                    {
                        CloseEdit(editor);

                        return (true);
                    }
                }

                return (false);
            }

            return (true);
        }

        #region UpdateCellValue

        private bool UpdateCellValue(IGridCellEditControl editor)
        {
            object value = editor.EditorValue;

            while (true)
            {
                try
                {
                    ConvertValueToType(editor, value);

                    return (false);
                }
                catch (Exception exp)
                {
                    bool throwException = true;
                    bool retry = false;

                    if (SuperGrid.HasDataErrorHandler == true)
                    {
                        if (SuperGrid.DoDataErrorEvent(GridPanel, this, exp,
                            DataContext.CellValueStore, ref value, ref throwException, ref retry) == true)
                        {
                            return (true);
                        }
                    }

                    if (throwException == true)
                    {
                        MessageBoxEx.Show(exp.ToString());
                        throw;
                    }

                    if (retry == false)
                        break;
                }
            }

            return (true);
        }

        #region ConvertValueToType

        private void ConvertValueToType(IGridCellEditControl editor, object value)
        {
            Type dataType = GridColumn.DataType;

            if (editor is IGridCellConvertTo)
            {
                IGridCellConvertTo cvtEditor = (IGridCellConvertTo)editor;

                object result;
                if (cvtEditor.TryConvertTo(value, dataType, out result) == true)
                {
                    Value = result;

                    return;
                }
            }

            if (dataType == null && Value != null)
                dataType = Value.GetType();

            if (dataType == typeof (SqlDateTime))
                Value = SetSqlDateTime(value);

            else if (dataType == typeof (SqlBoolean))
                Value = SetSqlBoolean(value);

            else if (dataType == typeof (SqlString))
                Value = SetSqlString(value);

            else if (dataType == typeof (SqlSingle))
                Value = SetSqlSingle(value);

            else if (dataType == typeof (SqlDouble))
                Value = SetSqlDouble(value);

            else if (dataType == typeof(SqlDecimal))
                Value = SetSqlDecimal(value);

            else if (dataType == typeof (SqlInt16))
                Value = SetSqlInt16(value);

            else if (dataType == typeof (SqlInt32))
                Value = SetSqlInt32(value);

            else if (dataType == typeof (SqlInt64))
                Value = SetSqlInt64(value);

            else if (dataType != null)
                Value = ConvertValue.ConvertTo(value, dataType);

            else
                Value = value;
        }

        #region SetSqlDateTime

        private object SetSqlDateTime(object value)
        {
            if (value == null)
                return (new SqlDateTime());

            if (value is DateTime)
                return (new SqlDateTime((DateTime)value));

            return (new SqlDateTime(Convert.ToDateTime(value)));
        }

        #endregion

        #region SetSqlBoolean

        private object SetSqlBoolean(object value)
        {
            if (value == null)
                return (new SqlBoolean());

            if (value is CheckState)
            {
                CheckState cs = (CheckState)value;

                if (cs == CheckState.Checked)
                    return (new SqlBoolean(true));

                if (cs == CheckState.Unchecked)
                    return (new SqlBoolean(false));

                return (new SqlBoolean());
            }

            return (new SqlBoolean(Convert.ToBoolean(value)));
        }

        #endregion

        #region SetSqlBoolean

        private object SetSqlString(object value)
        {
            if (value == null)
                return (new SqlString());

            return (new SqlString(Convert.ToString(value)));
        }

        #endregion

        #region SetSqlSingle

        private object SetSqlSingle(object value)
        {
            if (value == null)
                return (new SqlSingle());

            return (new SqlSingle(Convert.ToSingle(value)));
        }

        #endregion

        #region SetSqlDouble

        private object SetSqlDouble(object value)
        {
            if (value == null)
                return (new SqlDouble());

            return (new SqlSingle(Convert.ToDouble(value)));
        }

        #endregion

        #region SetSqlDecimal

        private object SetSqlDecimal(object value)
        {
            if (value == null)
                return (new SqlDecimal());

            return (new SqlDecimal(Convert.ToDecimal(value)));
        }

        #endregion

        #region SetSqlInt16

        private object SetSqlInt16(object value)
        {
            if (value == null)
                return (new SqlInt16());

            return (new SqlInt16(Convert.ToInt16(value)));
        }

        #endregion

        #region SetSqlInt32

        private object SetSqlInt32(object value)
        {
            if (value == null)
                return (new SqlInt32());

            return (new SqlInt32(Convert.ToInt32(value)));
        }

        #endregion

        #region SetSqlInt64

        private object SetSqlInt64(object value)
        {
            if (value == null)
                return (new SqlInt64());

            return (new SqlInt64(Convert.ToInt64(value)));
        }

        #endregion

        #endregion

        #endregion

        #region ValidateCellEdit

        private bool ValidateCellEdit(IGridCellEditControl editor)
        {
            try
            {
                object value = editor.EditorValue;
                object formattedValue = editor.EditorFormattedValue;

                if (SuperGrid.DoCellValidatingEvent(this, value, formattedValue) == true)
                {
                    SystemSounds.Beep.Play();

                    return (false);
                }

                SuperGrid.DoCellValidatedEvent(this);
            }
            catch
            {
                SystemSounds.Beep.Play();

                return (false);
            }

            return (true);
        }

        #endregion

        #endregion

        #region CancelEdit

        ///<summary>
        /// Cancels the current in-progress Modal cell edit operation.
        ///</summary>
        ///<returns>true if canceled</returns>
        public bool CancelEdit()
        {
            IGridCellEditControl editor = SuperGrid.ActiveEditor;

            if (editor == null || CellEditMode != CellEditMode.Modal)
                throw new Exception("Bad CancelEdit - Modal edit not in progress.");

            if (SuperGrid.DoCancelEditEvent(GridPanel, this, editor) == false)
            {
                if (editor.CancelEdit() == false)
                {
                    bool rowDirty = GridRow.RowDirty;
                    bool needsStored = GridRow.RowNeedsStored;

                    CloseEdit(editor);

                    GridRow.RowDirty = rowDirty;
                    GridRow.RowNeedsStored = needsStored;

                    return (true);
                }
            }

            return (true);
        }

        #endregion

        #region CloseEdit

        private void CloseEdit(IGridCellEditControl editor)
        {
            CloseCellInfoWindow();

            IsMouseOver = false;

            bool wasFocused = ((Control)editor).ContainsFocus;

            editor.EditorPanel.Hide();
            editor.EditorPanel.GridCell = null;

            GridRow.EditorDirty = false;

            SuperGrid.ActiveEditor = null;
            SuperGrid.EditorCell = null;

            if (wasFocused == true)
                SuperGrid.Focus();

            SuperGrid.DoCloseEditEvent(GridPanel, this);

            if (GridPanel.NeedToUpdateDataFilter == true)
                InvalidateLayout();
            else
                InvalidateRender();
        }

        #endregion

        #region PositionEditPanel

        internal void PositionEditPanel(IGridCellEditControl editor)
        {
            PositionEditControl(editor);

            UpdateCellInfoWindowPos();

            ((Control)editor).Update();
            editor.EditorPanel.Update();
        }

        #endregion

        #region PostCellKeyDown

        ///<summary>
        /// PostCellKeyDown
        ///</summary>
        ///<returns></returns>
        internal void PostCellKeyDown(Keys keyData)
        {
            if (CanModify == true)
            {
                SuperGrid.DeactivateNonModalEditor();

                IGridCellEditControl editor = GetInternalEditControl();

                if (editor != null)
                {
                    KeyEventArgs e = new KeyEventArgs(keyData);

                    CellVisualStyle style = GetEffectiveStyle();

                    InitializeContext(DataContext.CellKeyEvent, editor, style);

                    editor.CellKeyDown(e);
                }

                PostInternalMouseMove();
            }
        }

        #endregion

        #region PositionEditControl

        ///<summary>
        /// PositionEditControl
        ///</summary>
        ///<param name="editor"></param>
        private void PositionEditControl(IGridCellEditControl editor)
        {
            if (GridColumn != null)
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    Control c = (Control) editor;

                    Rectangle r = GetAdjustedBounds(ContentBounds);
                    Rectangle e = GetEditPanelBounds(editor, r);
                    e = GetClippedBounds(panel, e);

                    switch (editor.CellEditMode)
                    {
                        case CellEditMode.Modal:
                        case CellEditMode.NonModal:
                            if (GetCellHighlightMode(panel) == CellHighlightMode.Content)
                            {
                                r = GetEditPanelBounds(editor, r);
                                e = GetClippedBounds(panel, r);
                            }

                            editor.EditorPanel.Bounds = e;

                            c.Bounds = GetEditorBounds(editor, r, e);

                            editor.EditorPanel.SizeEx = Bounds.Size;
                            editor.EditorPanel.OffsetEx = new Point(Bounds.X - e.X, Bounds.Y - e.Y);

                            if (editor.CellEditMode == CellEditMode.NonModal)
                            {
                                editor.EditorPanel.BringToFront();
                                editor.EditorPanel.Show();
                            }
                            break;

                        case CellEditMode.InPlace:
                            r.Intersect(SuperGrid.SViewRect);
                            r.Location = Point.Empty;

                            c.Bounds = r;
                            break;
                    }
                }
            }
        }

        #region GetEditPanelBounds

        private Rectangle GetEditPanelBounds(IGridCellEditControl editor, Rectangle r)
        {
            CellVisualStyle style = GetEffectiveStyle();

            int n = r.Right;

            if (SuperGrid.EditorCell != this)
            {
                AlignHorizontally(editor, style, ref r);
            }
            else
            {
                if (GetCellHighlightMode(GridPanel) == CellHighlightMode.Content)
                    n -= 2;
            }

            if (r.Right > n)
                r.Width -= (r.Right - n);

            n = r.Bottom;

            AlignVertically(editor, style, ref r);

            if (r.Bottom > n)
                r.Height -= (r.Bottom - n + 1);

            return (r);
        }

        #endregion

        #region AlignHorizontally

        private void AlignHorizontally(
            IGridCellEditControl editor, CellVisualStyle style, ref Rectangle r)
        {
            Size size = _ContentSize;

            switch (editor.StretchBehavior)
            {
                case StretchBehavior.Both:
                case StretchBehavior.HorizontalOnly:
                    size.Width = r.Width;
                    break;

                default:
                    if (size.Width <= 0)
                    {
                        GetCurrentContentSize(style, editor);

                        size = _ContentSize;
                    }

                    if (size.Width <= 0)
                    {
                        size.Width = r.Width;
                    }
                    else if (r.Width > size.Width)
                    {
                        switch (style.Alignment)
                        {
                            case Alignment.TopCenter:
                            case Alignment.MiddleCenter:
                            case Alignment.BottomCenter:
                                r.X += ((r.Width - size.Width)/2);
                                break;

                            case Alignment.TopRight:
                            case Alignment.MiddleRight:
                            case Alignment.BottomRight:
                                r.X += (r.Width - size.Width - 1);
                                break;
                        }
                    }
                    break;
            }

            r.Width = size.Width;
        }

        #endregion

        #region AlignVertically

        private void AlignVertically(
            IGridCellEditControl editor, CellVisualStyle style, ref Rectangle r)
        {
            Size size = _ContentSize;

            switch (editor.StretchBehavior)
            {
                case StretchBehavior.Both:
                case StretchBehavior.VerticalOnly:
                    size.Height = r.Height - 1;
                    break;

                default:
                    if (size.Height <= 0)
                    {
                        size.Height = r.Height;
                    }
                    else if (r.Height > size.Height)
                    {
                        switch (style.Alignment)
                        {
                            case Alignment.MiddleLeft:
                            case Alignment.MiddleCenter:
                            case Alignment.MiddleRight:
                                r.Y += ((r.Height - size.Height)/2);
                                break;

                            case Alignment.BottomLeft:
                            case Alignment.BottomCenter:
                            case Alignment.BottomRight:
                                r.Y += r.Height - size.Height;
                                break;
                        }
                    }
                    break;
            }

            r.Height = size.Height;
        }

        #endregion

        #region GetEditorBounds

        private Rectangle GetEditorBounds(
            IGridCellEditControl editor, Rectangle r, Rectangle e)
        {
            r.Width = e.Right - r.X;

            if (GetCellHighlightMode(GridPanel) == CellHighlightMode.Content)
            {
                r.X = 0;
                r.Y = 0;
            }
            else
            {
                r.X = (r.X - e.X);
                r.Y = (r.Y - e.Y);

                r = GetEditPanelBounds(editor, r);
            }

            return (r);
        }

        #endregion

        #endregion

        #region FocusEditor

        internal void FocusEditor(IGridCellEditControl editor)
        {
            if (((Control)editor).Focused == false)
                ((Control)editor).Focus();

            if (editor is IGridCellEditorFocus)
                ((IGridCellEditorFocus)editor).FocusEditor();
        }

        #endregion

        #region CellRender

        ///<summary>
        /// This routine can be called by a cell editor / renderer to
        /// perform default cell rendering provided by the grid.
        ///</summary>
        ///<param name="editor"></param>
        ///<param name="g"></param>
        public void CellRender(IGridCellEditControl editor, Graphics g)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                CellVisualStyle style = GetEffectiveStyle();

                Rectangle rc = GetAdjustedBounds(ContentBounds);

                if (rc.Height > 0 && rc.Width > 0)
                {
                    switch (editor.CellEditMode)
                    {
                        case CellEditMode.Modal:
                            string s = IsDataError ? _Value.ToString() : editor.EditorFormattedValue;
                            string s2 = s;
                            
                            if (s != null && s.TrimStart().StartsWith("=") == true)
                                s2 = GetExpValue(s);

                            if (style.Font.Italic == true)
                                s2 += " ";

                            SuperGrid.DoGetCellFormattedValueEvent(this, ref s2);

                            eTextFormat tf = style.GetTextFormatFlags();

                            if (string.IsNullOrEmpty(s2) == false)
                                TextDrawing.DrawString(g, s2, style.Font, style.TextColor, rc, tf);
                            break;

                        case CellEditMode.NonModal:
                        case CellEditMode.InPlace:
                            Rectangle re = GetEditPanelBounds(editor, rc);

                            if (re.Width > 0 && re.Height > 0)
                            {
                                Bitmap bm = GetCellBitmap(editor, rc);

                                if (bm != null)
                                {
                                    PaintButtonContent(editor, bm, rc, re);

                                    g.DrawImageUnscaledAndClipped(bm, re);
                                }
                            }
                            break;
                    }

                    if (panel.ShowCellInfo == true)
                    {
                        if (String.IsNullOrEmpty(_InfoText) == false)
                        {
                            Image image = GetInfoImage();

                            if (image != null)
                            {
                                rc = InfoImageBounds;

                                if (rc.Width > 0 && rc.Height > 0)
                                    g.DrawImageUnscaledAndClipped(image, rc);
                            }
                        }
                    }
                }
            }
        }

        #region GetExpValue

        ///<summary>
        /// Gets the resultant value of the evaluated Cell Expression, if
        /// applicable. Otherwise returns the given value.
        ///</summary>
        ///<param name="source"></param>
        ///<returns></returns>
        public string GetExpValue(string source)
        {
            GridPanel panel = GridPanel;

            if (IsValueExpression == true &&
                panel.EnableCellExpressions == true)
            {
                EEval eval = panel.ExpDictionary[this];

                if (eval != null)
                {
                    while (true)
                    {
                        try
                        {
                            _ExpValue = eval.Evaluate();

                            string s = (_ExpValue != null)
                                           ? _ExpValue.ToString()
                                           : "";

                            InfoText = null;

                            return (s);
                        }
                        catch (Exception exp)
                        {
                            bool retry = false;
                            bool throwException = false;

                            if (SuperGrid.HasDataErrorHandler == true)
                            {
                                object value = _Value;

                                if (SuperGrid.DoDataErrorEvent(panel, this, exp,
                                   DataContext.CellExpressionEval, ref value, ref throwException, ref retry) == true)
                                {
                                    return (source);
                                }
                            }

                            if (throwException == true)
                                throw;

                            InfoText = exp.Message;

                            if (retry == false)
                                return ("####!");
                        }
                    }
                }
            }

            return (source);
        }

        #endregion

        #region GetCellBitmap

        /// <summary>
        /// Gets the cell paint bitmap
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        private Bitmap GetCellBitmap(IGridCellEditControl editor, Rectangle r)
        {
            Bitmap bm = editor.EditorCellBitmap;

            if (bm == null || bm.Size != r.Size)
            {
                bm = new Bitmap(r.Width, r.Height);

                editor.EditorCellBitmap = bm;
            }

            return (bm);
        }

        #endregion

        #region PaintButtonContent

        /// <summary>
        /// Paints the button background and content
        /// </summary>
        private void PaintButtonContent(IGridCellEditControl editor,
            Bitmap bm, Rectangle rc, Rectangle re)
        {
            Control c = (Control)editor;

            // SizeEx is the size of the Adjusted Content size of the cell
            // OffsetEx is the offset from the Editor panel to the cell ContentBounds

            editor.EditorPanel.SizeEx = rc.Size;
            editor.EditorPanel.OffsetEx = new Point(rc.X - re.X, rc.Y - re.Y);

            editor.SuspendUpdate = true;

            // Force a resize (LabelX needed this)

            re.Location = Point.Empty;
            c.Bounds = new Rectangle(0, 0, re.Width + 1, re.Height + 1);
            c.Bounds = re;

            editor.EditorPanel.Show();

            c.DrawToBitmap(bm, re);

            editor.SuspendUpdate = false;
        }

        #endregion

        #endregion

        #region PaintEditorBackground

        ///<summary>
        /// Performs default cell background painting for the cell.
        ///</summary>
        ///<param name="e"></param>
        ///<param name="editor"></param>
        public void PaintEditorBackground(PaintEventArgs e, IGridCellEditControl editor)
        {
            Rectangle r = new Rectangle();

            r.Size = editor.EditorPanel.SizeEx;
            r.Location = editor.EditorPanel.OffsetEx;

            if (r.Width > 0 && r.Height > 0)
            {
                Graphics g = e.Graphics;

                if (SuperGrid.DoPreRenderCellEvent(g, this, RenderParts.Background, r) == false)
                {
                    GridPanel panel = GridPanel;
                    CellHighlightMode mode = GetCellHighlightMode(panel);

                    using (Brush br = GetContentBrush(panel, r, mode))
                        g.FillRectangle(br, r);

                    SuperGrid.DoPostRenderCellEvent(g, this, RenderParts.Background, r);
                }
            }
        }

        #endregion

        #region ExtendSelection

        ///<summary>
        /// ExtendSelection
        ///</summary>
        internal void ExtendSelection(GridPanel panel)
        {
            if (IsMouseSelectable() == true)
            {
                Capture = true;

                ExtendSelectionEx(panel);
            }
        }

        private void ExtendSelectionEx(GridPanel panel)
        {
            _mouseDownHitCell = this;
            _mouseDownHitArea = CellArea.InContent;

            if (panel.SelectionRowAnchor == null)
                panel.SelectionRowAnchor = GridRow;

            if (_hitCell != null)
            {
                _anchorRowIndex = _hitCell.GridRow.GridIndex;
                _anchorColumnIndex = panel.Columns.GetDisplayIndex(GridColumn);

                ProcessExtendSelection(panel, false);

                if (panel.LastProcessedItem != _hitCell)
                    GridRow.EditorDirty = false;

                if (SuperGrid.ActiveNonModalEditor != null)
                    SuperGrid.ActiveNonModalEditor.EditorPanel.Invalidate(true);
            }
        }

        #region IsMouseSelectable

        private bool IsMouseSelectable()
        {
            if ((Control.MouseButtons & MouseButtons.Left) == MouseButtons.Left)
                return (true);

            return (IsSelected == false);
        }

        #endregion

        #endregion

        #region InitializeContext

        private void InitializeContext(DataContext dataContext,
            IGridCellEditControl editor, CellVisualStyle style)
        {
            if (((Control) editor).Handle != null)
            {
                object value = Value;

                if (InitializeContextEx(dataContext, editor, style) == true)
                    Value = value;
            }
        }

        private bool InitializeContextEx(DataContext dataContext,
            IGridCellEditControl editor, CellVisualStyle style)
        {
            object value = Value;

            while (true)
            {
                bool suspended = editor.SuspendUpdate;

                try
                {
                    IsDataError = false;

                    editor.SuspendUpdate = true;
                    editor.EditorPanel.GridCell = this;

                    editor.InitializeContext(this, style);

                    SuperGrid.DoInitEditContextEvent(this, editor);

                    return (false);
                }
                catch (Exception exp)
                {
                    bool retry = false;
                    bool throwException = false;

                    if (SuperGrid.HasDataErrorHandler == true)
                    {
                        if (SuperGrid.DoDataErrorEvent(GridPanel, this, exp,
                            dataContext, ref value, ref throwException, ref retry) == true)
                        {
                            return (true);
                        }
                    }
                    else
                    {
                        IsDataError = true;

                        return (false);
                    }

                    if (throwException == true)
                        throw;

                    if (retry == false)
                        break;

                    Value = value;
                }
                finally
                {
                    editor.SuspendUpdate = suspended;
                }
            }

            return (true);
        }

        #endregion

        #region CellRender

        private void CellRender(Graphics g,
            CellVisualStyle style, IGridCellEditControl renderer)
        {
            if (renderer.CellEditMode != CellEditMode.Modal)
            {
                try
                {
                    renderer.SuspendUpdate = true;

                    InitializeContext(DataContext.CellRender, renderer, style);
                    renderer.CellRender(g);
                }
                finally
                {
                    renderer.SuspendUpdate = false;
                }
            }
            else
            {
                InitializeContext(DataContext.CellRender, renderer, style);
                renderer.CellRender(g);
            }
        }

        #endregion

        #region SetEditorDirty

        ///<summary>
        /// Called by the cell editor to conditionally set
        /// the cells row level EditorDirty state.
        ///</summary>
        ///<param name="editor"></param>
        public void SetEditorDirty(IGridCellEditControl editor)
        {
            if (GridColumn.MarkRowDirtyOnCellValueChange == true)
            {
                if (SuperGrid.DoRowMarkedDirtyEvent(this, editor) == false)
                    EditorDirty = true;
            }
        }

        #endregion

        #region EditorValueChanged

        ///<summary>
        /// This routine is called by cell editors to signal to the grid
        /// that the editor has changed the cell Value.
        ///</summary>
        ///<param name="editor"></param>
        public void EditorValueChanged(IGridCellEditControl editor)
        {
            if (editor.SuspendUpdate == false)
            {
                SuperGrid.DoEditorValueChangedEvent(this, editor);

                editor.EditorValueChanged = true;
            }
        }

        #endregion

        #endregion

        #region UpdateBoundingRects

        private void UpdateBoundingRects()
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                _BoundsUpdateCount = SuperGrid.BoundsUpdateCount;

                _BackBounds = GetBackBounds(panel);
                _Bounds = GetBounds(panel);
            }
        }

        #region GetBackBounds

        /// <summary>
        /// Gets the scroll adjusted background bounding rectangle
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        internal Rectangle GetBackBounds(GridPanel panel)
        {
            Rectangle r = BoundsRelative;

            if (panel.IsSubPanel == false)
            {
                GridColumn column = GridColumn;

                if (column != null)
                {
                    if (column.IsHFrozen == false)
                        r.X -= HScrollOffset;
                }
            }
            else
            {
                r.X -= HScrollOffset;
            }

            if (GridRow.IsVFrozen == false)
                r.Y -= VScrollOffset;

            return (r);
        }

        #endregion

        #region GetBounds

        /// <summary>
        /// Gets the scroll adjusted bounding rectangle
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        internal Rectangle GetBounds(GridPanel panel)
        {
            Rectangle r = _BackBounds;

            r.Y += GridRow.EffectivePreDetailRowHeight;
            r.Height -= (GridRow.EffectivePreDetailRowHeight + GridRow.EffectivePostDetailRowHeight);

            return (r);
        }

        #endregion

        #region GetCellBounds

        /// <summary>
        /// Gets the SViewRect clipped Bounds
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        private Rectangle GetCellBounds(GridPanel panel)
        {
            return (GetClippedBounds(panel, Bounds));
        }

        #endregion

        #region GetContentBounds

        /// <summary>
        /// Gets the cell content (excludes image) bounding rectangle
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        internal Rectangle GetContentBounds(GridPanel panel)
        {
            Rectangle r = Bounds;

            int n = panel.LevelIndentSize.Width * _IndentLevel;

            if (IsPrimaryCell == true)
            {
                if (panel.ShowTreeButtons == true || panel.ShowTreeLines == true)
                    n += panel.TreeButtonIndent;

                if (panel.CheckBoxes == true)
                    n += panel.CheckBoxSize.Width + (CheckBoxSpacing * 2);

                r.X += n;
                r.Width -= n;
            }
            else if (panel.GroupColumns.Count > 0)
            {
                if (panel.Columns.FirstVisibleColumn == GridColumn)
                {
                    r.X += n;
                    r.Width -= n;
                }
            }

            CellVisualStyle style = GetEffectiveStyle();

            Image image = style.GetImage(panel);

            if (image != null)
                r = style.GetNonImageBounds(image, r);

            r.Inflate(-1, -1);

            return (r);
        }

        #endregion

        #region GetEditBounds

        /// <summary>
        /// Gets 
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        private Rectangle GetEditBounds(GridPanel panel)
        {
            Rectangle r = GetClippedBounds(panel, ContentBounds);

            r.Inflate(-1, -1);

            return (r);
        }

        #endregion

        #region GetClippedBounds

        private Rectangle GetClippedBounds(GridPanel panel, Rectangle r)
        {
            Rectangle v = SViewRect;

            if (IsVFrozen == true)
            {
                int n = panel.FixedRowHeight - panel.FixedHeaderHeight;

                v.Y -= n;
                v.Height += n;
            }

            if (GridColumn.IsHFrozen == false)
            {
                GridColumn pcol = panel.Columns.GetLastVisibleFrozenColumn();

                if (pcol != null)
                {
                    int n = pcol.BoundsRelative.Right - v.X;

                    v.X += n;
                    v.Width -= n;
                }
            }

            r.Intersect(v);

            return (r);
        }

        #endregion

        #endregion

        #region GetAdjustedBounds

        #region GetAdjustedBounds()

        private Rectangle GetAdjustedBounds()
        {
            Rectangle r = Bounds;

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

            if (r.X == GridPanel.Bounds.X)
            {
                r.X++;
                r.Width--;
            }

            return (r);
        }

        #endregion

        #region GetAdjustedBounds(r)

        private Rectangle GetAdjustedBounds(Rectangle r)
        {
            CellVisualStyle style = GetEffectiveStyle();

            r.X += (style.BorderThickness.Left + style.Margin.Left + style.Padding.Left);
            r.Width -= (style.BorderThickness.Horizontal + style.Margin.Horizontal + style.Padding.Horizontal);

            r.Y += (style.BorderThickness.Top + style.Margin.Top + style.Padding.Top);
            r.Height -= (style.BorderThickness.Vertical + style.Margin.Vertical + style.Padding.Vertical);

            if (Bounds.X == GridPanel.Bounds.X)
            {
                r.X++;
                r.Width--;
            }

            r.Width--;

            return (r);
        }

        #endregion

        #endregion

        #region GetCurrentContentSize

        private void GetCurrentContentSize(
            CellVisualStyle style, IGridCellEditControl editor)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                Size oldSize = _ContentSize;
                int width = Bounds.Size.Width - 2;

                Image image = style.GetImage(panel);

                if (image != null)
                {
                    Alignment alignment = style.ImageAlignment;

                    if (style.IsOverlayImage == true)
                        alignment = Alignment.MiddleCenter;

                    switch (alignment)
                    {
                        case Alignment.TopLeft:
                        case Alignment.TopRight:
                        case Alignment.MiddleLeft:
                        case Alignment.MiddleRight:
                        case Alignment.BottomLeft:
                        case Alignment.BottomRight:
                            width -= style.GetImageSize(image).Width;
                            break;
                    }
                }

                width -= style.GetBorderSize(true).Width;

                Size constraintSize = new Size(width, 0);

                using (Graphics g = SuperGrid.CreateGraphics())
                    _ContentSize = GetProposedSize(g, style, constraintSize, editor, false);

                if (_ContentSize != oldSize)
                    _BoundsUpdateCount = SuperGrid.BoundsUpdateCount - 1;
            }
        }

        #endregion

        #region GetCellEditBounds

        ///<summary>
        /// Get the CellEditBounds for the cell.
        ///</summary>
        ///<param name="editor"></param>
        ///<returns></returns>
        public Rectangle GetCellEditBounds(IGridCellEditControl editor)
        {
            Rectangle r = GetAdjustedBounds(ContentBounds);

            r = GetEditPanelBounds(editor, r);

            return (r);
        }

        #endregion

        #region CellInfoWindow support

        #region UpdateInfoWindow

        private void UpdateInfoWindow()
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                if (SuperGrid.EditorCell != this)
                {
                    CloseCellInfoWindow();
                }
                else
                {
                    if (CanShowCellInfo(panel) == true)
                    {
                        if (_cellInfoWindow == null)
                            OpenCellInfoWindow();

                        UpdateCellInfoWindowPos();

                        _cellInfoWindow.Show();
                        _cellInfoWindow.BringToFront();
                    }
                    else
                    {
                        CloseCellInfoWindow();
                    }
                }
            }
        }

        #endregion

        #region CanShowCellInfo

        private bool CanShowCellInfo(GridPanel panel)
        {
            return (panel.ShowCellInfo == true  &&
                string.IsNullOrEmpty(InfoText) == false &&
                GetInfoImage() != null);
        }

        #endregion

        #region UpdateCellInfoWindowPos

        private void UpdateCellInfoWindowPos()
        {
            if (_cellInfoWindow != null)
            {
                Point pt = InfoImageBounds.Location;

                pt = SuperGrid.PointToScreen(pt);
                pt = SuperGrid.ActiveEditor.EditorPanel.PointToClient(pt);

                _cellInfoWindow.Location = pt;
            }
        }

        #endregion

        #region OpenCellInfoWindow

        private void OpenCellInfoWindow()
        {
            _cellInfoWindow = new CellInfoWindow(SuperGrid, this);
            _cellInfoWindow.Parent = SuperGrid.ActiveEditor.EditorPanel;

            SuperGrid.ActiveEditor.EditorPanel.Controls.Add(_cellInfoWindow);
        }

        #endregion

        #region CloseCellInfoWindow

        private void CloseCellInfoWindow()
        {
            if (_cellInfoWindow != null)
            {
                SuperGrid.ToolTipText = "";

                _cellInfoWindow.Dispose();
                _cellInfoWindow = null;
            }
        }

        #endregion

        #endregion

        #region Style support

        #region GetEffectiveStyle

        ///<summary>
        /// Gets the EffectiveStyle for the cell. The effective
        /// style is the cached, composite style definition for the
        /// given cell, composed from panel, row, column, and cell styles.
        ///</summary>
        ///<returns></returns>
        public CellVisualStyle GetEffectiveStyle()
        {
            StyleState cellState = GetCellState(IsSelected);
            CellVisualStyle style = GetEffectiveStyle(cellState);

            return (style);
        }

        ///<summary>
        /// Gets the EffectiveStyle for the cell- for the given StyleType.
        /// The effective style is the cached, composite style definition for the
        /// given cell, composed from panel, row, column, and cell styles.
        ///</summary>
        ///<param name="type"></param>
        ///<returns></returns>
        public CellVisualStyle GetEffectiveStyle(StyleType type)
        {
            ValidateStyle();

            return (GetStyle(type));
        }

        internal CellVisualStyle GetEffectiveStyle(bool selected)
        {
            StyleState cellState = GetCellState(selected);

            return (GetEffectiveStyle(cellState));
        }

        private CellVisualStyle GetEffectiveStyle(StyleState cellState)
        {
            ValidateStyle();

            if (IsDesignerHosted == true)
                cellState &= ~(StyleState.MouseOver | StyleState.Selected);

            if (IsEmptyCell == true &&
                (GridPanel.AllowEmptyCellSelection == false || cellState == StyleState.Default))
            {
                return (GetStyle(StyleType.Empty));
            }

            if (IsSelectable == false)
                return (GetStyle(StyleType.NotSelectable));

            switch (cellState)
            {
                case StyleState.MouseOver:
                    return (GetStyle(StyleType.MouseOver));

                case StyleState.Selected:
                    return (GetStyle(StyleType.Selected));

                case StyleState.Selected | StyleState.MouseOver:
                    return (GetStyle(StyleType.SelectedMouseOver));

                case StyleState.ReadOnly:
                    return (GetStyle(StyleType.ReadOnly));

                case StyleState.ReadOnly | StyleState.MouseOver:
                    return (GetStyle(StyleType.ReadOnlyMouseOver));

                case StyleState.ReadOnly | StyleState.Selected:
                    return (GetStyle(StyleType.ReadOnlySelected));

                case StyleState.ReadOnly | StyleState.MouseOver | StyleState.Selected:
                    return (GetStyle(StyleType.ReadOnlySelectedMouseOver));

                default:
                    return (GetStyle(StyleType.Default));
            }
        }

        #endregion

        #region GetCellState

        private StyleState GetCellState(bool selected)
        {
            StyleState cellState = StyleState.Default;

            if (IsReadOnly == true)
                cellState |= StyleState.ReadOnly;

            if (SuperGrid.EditorCell != null || SuperGrid.NonModalEditorCell != null)
            {
                if (SuperGrid.EditorCell == this || SuperGrid.NonModalEditorCell == this)
                    cellState |= StyleState.MouseOver;
            }
            else
            {
                Point pt = SuperGrid.PointToClient(Control.MousePosition);
                Rectangle r = BoundsRelative;

                r.X -= HScrollOffset;

                if (IsVFrozen == false)
                    r.Y -= VScrollOffset;

                Rectangle t = ViewRect;

                r.Intersect(t);

                if (r.Contains(pt) == true)
                    cellState |= StyleState.MouseOver;
            }

            if (selected == true)
                cellState |= StyleState.Selected;

            return (cellState);
        }

        #endregion

        #region GetStyle

        private CellVisualStyle GetStyle(StyleType e)
        {
            if (_EffectiveStyles == null)
                _EffectiveStyles = new CellVisualStyles();

            if (_EffectiveStyles.IsValid(e) == false)
            {
                CellVisualStyle style = new CellVisualStyle();

                StyleType[] css = style.GetApplyStyleTypes(e);

                if (css != null)
                {
                    GridPanel panel = GridPanel;
                    GridColumn column = GridColumn;
                    GridRow row = GridRow;

                    foreach (StyleType cs in css)
                    {
                        panel.ApplyCellStyle(style, cs);
                        column.ApplyCellStyle(style, cs);
                        row.ApplyCellStyle(style, cs);

                        style.ApplyStyle(CellStyles[cs]);
                    }
                }

                SuperGrid.DoGetCellStyleEvent(this, e, ref style);

                if (_EffectiveStyles == null)
                    _EffectiveStyles = new CellVisualStyles();

                if (style.Background == null || style.Background.IsEmpty == true)
                    style.Background = new Background(Color.White);

                if (style.Font == null)
                    style.Font = SystemFonts.DefaultFont;

                _EffectiveStyles[e] = style;
            }

            return (_EffectiveStyles[e]);
        }

        #endregion

        #region GetSizingStyle

        private CellVisualStyle GetSizingStyle(GridPanel panel)
        {
            StyleType style = panel.GetSizingStyle();

            if (style == StyleType.NotSet)
            {
                style = (IsReadOnly == true)
                    ? StyleType.ReadOnly : StyleType.Default;
            }

            return (GetEffectiveStyle(style));
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
            ClearEffectiveStyles();

            VisualChangeType changeType = ((VisualPropertyChangedEventArgs)e).ChangeType;

            if (changeType == VisualChangeType.Layout)
            {
                NeedsMeasured = true;

                if (GridColumn != null)
                    GridColumn.NeedsMeasured = true;

                InvalidateLayout();
            }
            else
            {
                InvalidateRender();
            }
        }

        #endregion

        #region ValidateStyle

        private void ValidateStyle()
        {
            if (_StyleUpdateCount != SuperGrid.StyleUpdateCount)
                ClearEffectiveStyles();

            _StyleUpdateCount = SuperGrid.StyleUpdateCount;
        }

        #endregion

        #region InvalidateStyle

        ///<summary>
        ///Invalidates the cached Style
        ///definition for all defined StyleTypes
        ///</summary>
        public void InvalidateStyle()
        {
            ClearEffectiveStyles();

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

                InvalidateLayout();
            }
        }

        #endregion

        #region ClearEffectiveStyles

        internal void ClearEffectiveStyles()
        {
            _EffectiveStyles = null;
        }

        #endregion

        #endregion

        #region InvalidateLayout

        /// <summary>
        /// Invalidates the layout for the item.
        /// </summary>
        public override void InvalidateLayout()
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                if (panel.VirtualMode == false)
                    base.InvalidateLayout();
                else
                    InvalidateRender();
            }
        }

        #endregion

        #region InvalidateRender

        public override void InvalidateRender()
        {
            if (SuperGrid != null && GridColumn != null)
                InvalidateRender(Bounds);
        }

        #endregion

        #region IComparable<GridCell> Members

        public int CompareTo(GridCell other)
        {
            int sval = 0;
            if (SuperGrid.DoCompareElementsEvent(GridPanel, this, other, ref sval) == true)
                return (sval);

            if (GridPanel.SortUsingHiddenColumns == false)
            {
                if (Visible == false)
                    return (other.Visible == false ? 0 : -1);

                if (other.Visible == false)
                    return (1);
            }

            object val1 = IsValueExpression ? ExpValue : Value;
            object val2 = other.IsValueExpression ? other.ExpValue : other.Value;

            if (val1 != null && val1 != DBNull.Value)
            {
                Type type = val1.GetType();

                if (val2 != null && val2 != DBNull.Value)
                {
                    if (type != val2.GetType())
                        return (0);

                    if (type == typeof (string))
                        return (String.Compare((string)val1, (string)val2));

                    if (type == typeof (int))
                        return ((int)val1 - (int)val2);

                    if (type == typeof(Int16))
                        return ((Int16)val1 - (Int16)val2);

                    if (type == typeof (double))
                    {
                        double d = (double)val1 - (double)val2;
                        return (d < 0 ? -1 : d > 0 ? 1 : 0);
                    }

                    if (type == typeof (decimal))
                    {
                        decimal d = (decimal)val1 - (decimal)other.Value;
                        return (d < 0 ? -1 : d > 0 ? 1 : 0);
                    }

                    if (type == typeof (long))
                    {
                        long d = (long)val1 - (long)val2;
                        return (d < 0 ? -1 : d > 0 ? 1 : 0);
                    }

                    if (type == typeof (float))
                    {
                        float f = (float)val1 - (float)val2;
                        return (f < 0 ? -1 : f > 0 ? 1 : 0);
                    }

                    if (type == typeof (bool))
                    {
                        if (val1 == val2)
                            return (0);

                        return ((bool)val1 == true ? 1 : -1);
                    }

                    if (type == typeof (DateTime))
                    {
                        return (((DateTime)val1).CompareTo((DateTime)val2));
                    }

                    return (0);
                }

                return (1);
            }

            if (val2 != null && val2 != DBNull.Value)
                return (-1);

            return (0);
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            string s = "GridCell " + ColumnIndex +
                ": {" + (Value ?? "<null>") + "}";

            return (s);
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            _mouseRenderer = null;

            EditControl = null;
            RenderControl = null;
        }

        #endregion

        #region EditorInfo

        private class EditorInfo
        {
            #region Public variables

            public Type EditorType;
            public Type RenderType;

            public IGridCellEditControl EditControl;
            public IGridCellEditControl RenderControl;

            public object[] EditorParams;
            public object[] RenderParams;

            #endregion

            #region Public properties

            public bool IsEmpty
            {
                get
                {
                    return (EditorType == null && RenderType == null &&
                            (EditorParams == null && RenderParams == null));
                }
            }

            #endregion
        }

        #endregion

        #region CellStates

        [Flags]
        private enum Cs
        {
            AllowEdit = (1 << 0),
            DataError = (1 << 1),
            EmptyCell = (1 << 2),
            ReadOnly = (1 << 3),
            Selected = (1 << 4),
            ValueExpression = (1 << 5),
        }

        #endregion
    }

    #region enums

    #region CellArea

    ///<summary>
    /// CellArea
    ///</summary>
    public enum CellArea
    {
        ///<summary>
        /// NoWhere
        ///</summary>
        NoWhere,

        ///<summary>
        /// InContent
        ///</summary>
        InContent,

        ///<summary>
        /// InExpandButton
        ///</summary>
        InExpandButton,

        ///<summary>
        /// InCheckBox
        ///</summary>
        InCheckBox,

        ///<summary>
        /// InCellInfo
        ///</summary>
        InCellInfo,
    }

    #endregion

    #endregion

    #region ValueTypeConverter

    ///<summary>
    /// ValueTypeConverter
    ///</summary>
    public class ValueTypeConverter : TypeConverter
    {
        #region CanConvertTo

        public override bool CanConvertTo(
            ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return (true);

            return (base.CanConvertTo(context, destinationType));
        }

        #endregion

        #region ConvertTo

        public override object ConvertTo(
            ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                string s = value as string;

                if (s != null)
                    return (s);
            }

            return (base.ConvertTo(context, culture, value, destinationType));
        }

        #endregion

        #region CanConvertFrom

        public override bool CanConvertFrom(
            ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return (true);

            return (base.CanConvertFrom(context, sourceType));
        }

        #endregion

        #region ConvertFrom

        public override object ConvertFrom(
            ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                GridCell cell = context.Instance as GridCell;

                if (cell != null)
                {
                    Type type = GetEditType(cell);

                    if (type == typeof (GridDoubleInputEditControl))
                        return (ConvertFromToDouble((string)value));

                    if (type == typeof (GridIntegerInputEditControl) ||
                        type == typeof (GridProgressBarXEditControl) ||
                        type == typeof (GridSliderEditControl) ||
                        type == typeof (GridTrackBarEditControl))
                    {
                        return (ConvertFromToInteger((string) value));
                    }

                    if (type == typeof (GridNumericUpDownEditControl))
                        return (ConvertFromToDecimal((string)value));
                }

                return (value);
            }

            return base.ConvertFrom(context, culture, value);
        }

        #region ConvertFromToDouble

        private double ConvertFromToDouble(string value)
        {
            try
            {
                return (double.Parse(value));
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid value to convert to 'double'.");
            }
        }

        #endregion

        #region ConvertFromToInteger

        private int ConvertFromToInteger(string value)
        {
            try
            {
                return (int.Parse(value));
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid value to convert to 'int'.");
            }
        }

        #endregion

        #region ConvertFromToDecimal

        private decimal ConvertFromToDecimal(string value)
        {
            try
            {
                return (decimal.Parse(value));
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid value to convert to 'decimal'.");
            }
        }

        #endregion

        #endregion

        #region GetEditType

        private Type GetEditType(GridCell cell)
        {
            if (cell.EditorType != null)
                return (cell.EditorType);

            if (cell.GridColumn != null)
                return (cell.GridColumn.EditorType);

            return (null);
        }

        #endregion
    }

    #endregion

}
