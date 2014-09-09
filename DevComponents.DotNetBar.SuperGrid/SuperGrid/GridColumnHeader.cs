using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid.Style;
using DevComponents.SuperGrid.TextMarkup;

namespace DevComponents.DotNetBar.SuperGrid
{
    /// <summary>
    /// Represents a layout of column headers
    /// </summary>
    [TypeConverter(typeof(BlankExpandableObjectConverter))]
    public class GridColumnHeader : GridElement
    {
        #region Constants

        private const int SeparatorWidth = 2;
        private const int ImageCacheSize = 8;

        #endregion

        #region Private variables

        private GridColumnCollection _Columns;

        private HeaderArea _HitArea;
        private GridColumn _HitColumn;
        private GridColumn _ResizeColumn;

        private int _MouseDownDelta;
        private HeaderArea _MouseDownHitArea;
        private GridColumn _MouseDownHitColumn;
        private Point _MouseDownPoint;
        private MouseButtons _MouseDownButtons;

        private bool _DragSelection;
        private bool _DragStarted;

        private HeaderArea _LastHitArea;
        private GridColumn _LastHitColumn;

        private bool _Reordering;
        private bool _Resizing;
        private int _ResizeWidth;

        private int _RowHeight;

        private GridColumn _SeparatorColumn;
        private bool _SeparatorIsRight;

        private FloatWindow _HeaderFw;
        private FloatWindow _SeparatorFw;

        private int _OrderTextOffset;
        private int _SelectAllCount;

        private ColumnHeaderRowVisualStyles _EffectiveRowHeaderStyles;
        private int _StyleUpdateCount;

        private bool _ShowHeaderImages = true;

        private Image _AscendingSortImage;
        private int _AscendingSortImageIndex = -1;
        private Image[] _AscendingSortCacheImages = new Image[ImageCacheSize];

        private Image _DescendingSortImage;
        private int _DescendingSortImageIndex = -1;
        private Image[] _DescendingSortCacheImages = new Image[ImageCacheSize];

        private Alignment _SortImageAlignment = Alignment.NotSet;

        private Image _FilterImage;
        private int _FilterImageIndex = -1;
        private Image[] _FilterCacheImages = new Image[ImageCacheSize * 2];

        private ImageVisibility _FilterImageVisibility = ImageVisibility.Auto;
        private Alignment _FilterImageAlignment = Alignment.NotSet;

        private FilterPopup _FilterMenu;

        private bool _LockedColumn;
        private bool _ShowFilterToolTips = true;
        private bool _ShowToolTips = true;

        private string _RowHeaderText;

        #endregion

        #region Internal properties

        #region Columns

        internal GridColumnCollection Columns
        {
            get { return (_Columns); }
            set { _Columns = value; }
        }

        #endregion

        #region RowHeaderBounds

        internal Rectangle RowHeaderBounds
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    Rectangle r = Bounds;
                    r.Width = panel.RowHeaderWidth;

                    return (r);
                }

                return (Rectangle.Empty);
            }
        }

        #endregion

        #endregion

        #region Public properties

        #region AscendingSortImage

        /// <summary>
        /// Gets or sets the Ascending sort image
        /// </summary>
        [DefaultValue(null), Category("Appearance")]
        [Description("Indicates the Ascending sort image.")]
        public Image AscendingSortImage
        {
            get { return (_AscendingSortImage); }

            set
            {
                if (_AscendingSortImage != value)
                {
                    _AscendingSortImage = value;

                    OnPropertyChangedEx("AscendingSortImage", VisualChangeType.Render);
                }
            }
        }

        #region GetAscendingSortImage

        internal Image GetAscendingSortImage(GridColumn column)
        {
            if (_AscendingSortImage != null)
                return (_AscendingSortImage);

            if (_AscendingSortImageIndex >= 0)
            {
                ImageList imageList = GridPanel.ImageList;

                if (imageList != null && _AscendingSortImageIndex < imageList.Images.Count)
                    return (imageList.Images[_AscendingSortImageIndex]);
            }

            return (GetAscendingSortImageEx(column));
        }

        #region GetAscendingSortImageEx

        internal Image GetAscendingSortImageEx(GridColumn column)
        {
            StyleState state = GetStyleState(column, column.IsSelected);

            Image cacheImage = _AscendingSortCacheImages[(int)state];

            if (cacheImage == null)
            {
                Rectangle r = new Rectangle(0, 0, 7, 4);
                Image image = new Bitmap(7, 4);

                using (Graphics g = Graphics.FromImage(image))
                {
                    using (GraphicsPath path = new GraphicsPath())
                    {
                        Point pt = new Point(r.X + r.Width / 2, r.Top);

                        Point[] pts =
                        {
                            pt,
                            new Point(pt.X + 4, pt.Y + 4),
                            new Point(pt.X - 4, pt.Y + 4),
                            pt
                        };

                        path.AddLines(pts);

                        ColumnHeaderRowVisualStyle style = GetEffectiveRowHeaderStyleEx(state);

                        Color color = style.SortIndicatorColor;

                        if (color.IsEmpty)
                            color = Color.Black;

                        using (Brush br = new SolidBrush(color))
                            g.FillPath(br, path);
                    }
                }

                _AscendingSortCacheImages[(int)state] = image;

                cacheImage = image;
            }

            return (cacheImage);
        }

        #endregion

        #endregion

        #endregion

        #region AscendingSortImageIndex

        /// <summary>
        /// Gets or sets the Ascending Sort Image index
        /// </summary>
        [Browsable(true), DefaultValue(-1)]
        [Category("Appearance"), Description("Indicates the Ascending Sort Image index.")]
        [Editor("DevComponents.SuperGrid.Design.ImageIndexEditor, DevComponents.SuperGrid.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=26d81176cfa2b486", typeof(UITypeEditor))]
        [TypeConverter(typeof(ImageIndexConverter))]
        public int AscendingSortImageIndex
        {
            get { return (_AscendingSortImageIndex); }

            set
            {
                if (_AscendingSortImageIndex != value)
                {
                    _AscendingSortImageIndex = value;

                    OnPropertyChangedEx("AscendingSortImageIndex", VisualChangeType.Layout);
                }
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetAscendingSortImageIndex()
        {
            _AscendingSortImageIndex = -1;
        }

        #endregion

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
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    Rectangle r = BoundsRelative;

                    if (panel.IsSubPanel == true)
                    {
                        r.X -= HScrollOffset;

                        if (IsVFrozen == false)
                            r.Y -= VScrollOffset;
                    }

                    return (r);
                }

                return (Rectangle.Empty);
            }
        }

        #endregion

        #region DescendingSortImage

        /// <summary>
        /// Gets or sets the Descending sort image
        /// </summary>
        [DefaultValue(null), Category("Appearance")]
        [Description("Indicates the Descending sort image.")]
        public Image DescendingSortImage
        {
            get { return (_DescendingSortImage); }

            set
            {
                if (_DescendingSortImage != value)
                {
                    _DescendingSortImage = value;

                    OnPropertyChangedEx("DescendingSortImage", VisualChangeType.Render);
                }
            }
        }

        #region GetDescendingSortImage

        internal Image GetDescendingSortImage(GridColumn column)
        {
            if (_DescendingSortImage != null)
                return (_DescendingSortImage);

            if (_DescendingSortImageIndex >= 0)
            {
                ImageList imageList = GridPanel.ImageList;

                if (imageList != null && _DescendingSortImageIndex < imageList.Images.Count)
                    return (imageList.Images[_DescendingSortImageIndex]);
            }

            return (GetDescendingSortImageEx(column));
        }

        #region GetDescendingSortImageEx

        internal Image GetDescendingSortImageEx(GridColumn column)
        {
            StyleState state = GetStyleState(column, column.IsSelected);

            Image cacheImage = _DescendingSortCacheImages[(int)state];

            if (cacheImage == null)
            {
                Rectangle r = new Rectangle(0, 0, 7, 4);
                Image image = new Bitmap(7, 4);

                using (Graphics g = Graphics.FromImage(image))
                {
                    using (GraphicsPath path = new GraphicsPath())
                    {
                        Point pt = new Point(r.X + r.Width / 2, r.Bottom - 1);

                        Point[] pts =
                        {
                            pt,
                            new Point(pt.X + 4, pt.Y - 4),
                            new Point(pt.X - 4, pt.Y - 4),
                            pt
                        };

                        path.AddLines(pts);

                        ColumnHeaderRowVisualStyle style = GetEffectiveRowHeaderStyleEx(state);

                        Color color = style.SortIndicatorColor;

                        if (color.IsEmpty)
                            color = Color.Black;

                        using (Brush br = new SolidBrush(color))
                            g.FillPath(br, path);
                    }
                }

                _DescendingSortCacheImages[(int)state] = image;

                cacheImage = image;
            }

            return (cacheImage);
        }

        #endregion

        #endregion

        #endregion

        #region DescendingSortImageIndex

        /// <summary>
        /// Gets or sets the Descending Sort Image index
        /// </summary>
        [Browsable(true), DefaultValue(-1)]
        [Category("Appearance"), Description("Indicates the Descending Sort Image index.")]
        [Editor("DevComponents.SuperGrid.Design.ImageIndexEditor, DevComponents.SuperGrid.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=26d81176cfa2b486", typeof(UITypeEditor))]
        [TypeConverter(typeof(ImageIndexConverter))]
        public int DescendingSortImageIndex
        {
            get { return (_DescendingSortImageIndex); }

            set
            {
                if (_DescendingSortImageIndex != value)
                {
                    _DescendingSortImageIndex = value;

                    OnPropertyChangedEx("DescendingSortImageIndex", VisualChangeType.Layout);
                }
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetDescendingSortImageIndex()
        {
            _DescendingSortImageIndex = -1;
        }

        #endregion

        #region FilterImage

        /// <summary>
        /// Gets or sets the default
        /// image to display in the column header.
        /// </summary>
        [DefaultValue(null), Category("Filtering")]
        [Description("Indicates the default image to display in the column header.")]
        public Image FilterImage
        {
            get { return (_FilterImage); }

            set
            {
                if (_FilterImage != value)
                {
                    _FilterImage = value;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("FilterImage", VisualChangeType.Layout);
                }
            }
        }

        #region GetFilterImage

        internal Image GetFilterImage(GridColumn column)
        {
            bool inFilter;
            StyleState state = GetFilterImageState(column, out inFilter);

            return (GetFilterImage(column, state, inFilter));
        }

        internal Image GetFilterImage(
            GridColumn column, StyleState state, bool inFilter)
        {
            if (_FilterImage != null)
                return (_FilterImage);

            if (_FilterImageIndex >= 0)
            {
                ImageList imageList = GridPanel.ImageList;

                if (imageList != null && _FilterImageIndex < imageList.Images.Count)
                    return (imageList.Images[_FilterImageIndex]);
            }

            return (GetFilterImageEx(state, inFilter));
        }

        #region GetFilterImageEx

        private Image GetFilterImageEx(StyleState state, bool inFilter)
        {
            int n = (int)state;

            if (inFilter == true)
                n += ImageCacheSize;

            Image cacheImage = _FilterCacheImages[n];

            if (cacheImage == null)
            {
                Image image = new Bitmap(8, 8);

                using (Graphics g = Graphics.FromImage(image))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    using (GraphicsPath path = new GraphicsPath())
                    {
                        Point[] pts =
                        {
                            new Point(0, 0),
                            new Point(7, 0),
                            new Point(7, 1),
                            new Point(4, 4),
                            new Point(4, 7),
                            new Point(3, 6),
                            new Point(3, 4),
                            new Point(0, 1),
                            new Point(0, 0),
                        };

                        path.AddLines(pts);

                        Rectangle r = new Rectangle(1, 1, 6, 3);

                        ColumnHeaderRowVisualStyle style = GetEffectiveRowHeaderStyleEx(state);

                        Color color = style.FilterBorderColor;

                        if (color.IsEmpty)
                            color = Color.DimGray;

                        if (inFilter == false)
                            style = GetEffectiveRowHeaderStyleEx(state & ~StyleState.MouseOver);

                        Background back = (style.FilterBackground != null && style.FilterBackground.IsEmpty == false)
                            ? style.FilterBackground : new Background(Color.White);

                        using (Brush br = back.GetBrush(r))
                            g.FillPath(br, path);

                        using (Pen pen = new Pen(color))
                            g.DrawPath(pen, path);
                    }
                }

                _FilterCacheImages[n] = image;

                cacheImage = image;
            }

            return (cacheImage);
        }

        #region GetFilterImageState

        private StyleState GetFilterImageState(GridColumn column, out bool inFilter)
        {
            inFilter = false;

            StyleState state = GetRowHeaderState();

            state &= ~(StyleState.MouseOver | StyleState.Selected);

            if (_HitColumn == column)
            {
                state |= StyleState.MouseOver;

                if (_HitArea == HeaderArea.InFilterMenu)
                    inFilter = true;
            }

            if (string.IsNullOrEmpty(column.FilterExpr) == false)
                state |= StyleState.Selected;

            return (state);
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region FilterImageAlignment

        /// <summary>
        /// Gets or sets the alignment of the filter Image
        /// </summary>
        [DefaultValue(Alignment.NotSet), Category("Appearance")]
        [Description("Indicates the alignment of the filter Image.")]
        public Alignment FilterImageAlignment
        {
            get { return (_FilterImageAlignment); }

            set
            {
                if (_FilterImageAlignment != value)
                {
                    _FilterImageAlignment = value;

                    OnPropertyChangedEx("FilterImageAlignment", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region FilterImageIndex

        /// <summary>
        /// Gets or sets the Filter image index
        /// </summary>
        [Browsable(true), DefaultValue(-1)]
        [Category("Appearance"), Description("Indicates the Filter Row image index.")]
        [Editor("DevComponents.SuperGrid.Design.ImageIndexEditor, DevComponents.SuperGrid.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=26d81176cfa2b486", typeof(UITypeEditor))]
        [TypeConverter(typeof(ImageIndexConverter))]
        public int FilterImageIndex
        {
            get { return (_FilterImageIndex); }

            set
            {
                if (_FilterImageIndex != value)
                {
                    _FilterImageIndex = value;

                    OnPropertyChangedEx("FilterImageIndex", VisualChangeType.Layout);
                }
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetFilterImageIndex()
        {
            _FilterImageIndex = -1;
        }

        #endregion

        #region FilterImageVisibility

        /// <summary>
        /// Gets or sets the visibility of the header filter image
        /// </summary>
        [DefaultValue(ImageVisibility.Auto), Category("Filtering")]
        [Description("Indicates the visibility of the header filter image.")]
        public ImageVisibility FilterImageVisibility
        {
            get { return (_FilterImageVisibility); }

            set
            {
                if (_FilterImageVisibility != value)
                {
                    _FilterImageVisibility = value;

                    OnPropertyChangedEx("FilterImageVisibility", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region RowHeaderText

        ///<summary>
        /// Gets or sets the associated row header text.
        ///</summary>
        [DefaultValue(null), Category("Appearance")]
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

        #region RowHeight

        ///<summary>
        /// Gets or sets the height of the ColumnHeader row
        ///</summary>
        [DefaultValue(0), Category("Style")]
        [Description("Indicates the height of the ColumnHeader row")]
        public int RowHeight
        {
            get { return (_RowHeight); }

            set
            {
                if (_RowHeight != value)
                {
                    if (value < 0)
                        throw new Exception("RowHeight cannot be negative");

                    _RowHeight = value;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("RowHeight", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region ShowFilterToolTips

        /// <summary>
        /// Gets or sets whether tooltips are shown when over the filter image
        /// </summary>
        [DefaultValue(true), Category("Filtering")]
        [Description("Indicates whether tooltips are shown when over the filter image.")]
        public bool ShowFilterToolTips
        {
            get { return (_ShowFilterToolTips); }

            set
            {
                if (value != _ShowFilterToolTips)
                {
                    _ShowFilterToolTips = value;

                    OnPropertyChangedEx("ShowFilterToolTips");
                }
            }
        }

        #endregion

        #region ShowHeaderImages

        /// <summary>
        /// Gets or sets whether Column Header images are displayed
        /// </summary>
        [DefaultValue(true), Category("Appearance")]
        [Description("Indicates whether Column Header images are displayed.")]
        public bool ShowHeaderImages
        {
            get { return (_ShowHeaderImages); }

            set
            {
                if (_ShowHeaderImages != value)
                {
                    _ShowHeaderImages = value;

                    NeedsMeasured = true;

                    OnPropertyChangedEx("ShowHeaderImages", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region ShowToolTips

        /// <summary>
        /// Gets or sets whether tooltips are shown for the column header
        /// </summary>
        [DefaultValue(true), Category("Filtering")]
        [Description("Indicates whether tooltips are shown for the column header.")]
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

        #region SortImageAlignment

        /// <summary>
        /// Gets or sets the alignment of the Sort Images
        /// </summary>
        [DefaultValue(Alignment.NotSet), Category("Appearance")]
        [Description("Indicates the alignment of the Sort Images.")]
        public Alignment SortImageAlignment
        {
            get { return (_SortImageAlignment); }

            set
            {
                if (_SortImageAlignment != value)
                {
                    _SortImageAlignment = value;

                    OnPropertyChangedEx("SortImageAlignment", VisualChangeType.Layout);
                }
            }
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
            Size sizeNeeded = Size.Empty;

            GridPanel panel = stateInfo.GridPanel;

            if (panel.ShowRowHeaders == true)
                sizeNeeded.Width += panel.RowHeaderWidth;

            GridColumnCollection columns = _Columns;
            foreach (GridColumn column in columns)
            {
                Size size = MeasureHeader(layoutInfo, constraintSize, panel, column);

                if (column.Visible == true)
                {
                    sizeNeeded.Width += size.Width;
                    sizeNeeded.Height = Math.Max(size.Height, sizeNeeded.Height);
                }
            }

            if (_RowHeight > 0)
                sizeNeeded.Height = _RowHeight;

            Size = sizeNeeded;
        }

        #region MeasureHeader

        internal Size MeasureHeader(GridLayoutInfo layoutInfo,
            Size constraintSize, GridPanel panel, GridColumn column)
        {
            Size sizeNeeded = Size.Empty;

            ColumnHeaderVisualStyle style = GetSizingStyle(panel, column);

            Image image = (_ShowHeaderImages == true)
                ? style.GetImage(panel) : null;

            Alignment imageAlignment = style.ImageAlignment;
            Size imageSize = style.GetImageSize(image);

            if (style.IsOverlayImage == true)
                imageAlignment = Alignment.MiddleCenter;

            Size borderSize = style.GetBorderSize(true);

            int bwidth = (borderSize.Width * 2);
            int bheight = (borderSize.Height * 2);

            int width = constraintSize.Width > 0 ? column.Size.Width : 0;

            if (width > 0)
            {
                width -= bwidth;

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
            }

            column.HeaderContentSize =
                MeasureHeaderText(layoutInfo.Graphics, column, style, new Size(width, 0));

            if (width == 0)
                column.HeaderTextSize = column.HeaderContentSize;

            switch (imageAlignment)
            {
                case Alignment.MiddleCenter:
                    sizeNeeded.Height = Math.Max(imageSize.Height, column.HeaderContentSize.Height) + bheight;
                    sizeNeeded.Width = Math.Max(column.HeaderContentSize.Width, imageSize.Width) + bwidth;
                    break;

                case Alignment.TopCenter:
                case Alignment.BottomCenter:
                    sizeNeeded.Width = Math.Max(column.HeaderContentSize.Width, imageSize.Width) + bwidth;
                    sizeNeeded.Height = column.HeaderContentSize.Height + imageSize.Height + bheight;
                    break;

                default:
                    sizeNeeded.Width = column.HeaderContentSize.Width + imageSize.Width + bwidth;
                    sizeNeeded.Height = Math.Max(imageSize.Height, column.HeaderContentSize.Height) + bheight;
                    break;
            }

            Alignment sortAlignment = (_SortImageAlignment == Alignment.NotSet)
                ? Alignment.TopCenter : _SortImageAlignment;

            Alignment filterAlignment = (_FilterImageAlignment == Alignment.NotSet)
                ? Alignment.MiddleLeft : _FilterImageAlignment;

            int imsHeight = 0;

            if (panel.SortLevel != SortLevel.None)
            {
                imsHeight = GetPartSizeNeeded(
                    GetAscendingSortImage(column), style, sortAlignment, ref sizeNeeded);
            }

            int imfHeight = 0;

            if (FilterCanBeVisible(panel) == true)
            {
                imfHeight = GetPartSizeNeeded(
                    GetFilterImage(column), style, filterAlignment, ref sizeNeeded);
            }

            if (imsHeight > 0 && imfHeight > 0)
            {
                if (VAlignment(sortAlignment) == VAlignment(filterAlignment))
                {
                    int n = Math.Max(imsHeight, imfHeight);

                    sizeNeeded.Height += n;

                    if (VAlignment(style.Alignment) == 1)
                        sizeNeeded.Height += n;
                }
                else
                    sizeNeeded.Height += (imsHeight + imfHeight);
            }
            else
            {
                int n = imsHeight + imfHeight;

                sizeNeeded.Height += n;

                if (VAlignment(style.Alignment) == 1)
                    sizeNeeded.Height += n;
            }

            sizeNeeded.Width += 8;
            sizeNeeded.Height += 4;

            column.HeaderSize = sizeNeeded;

            return (sizeNeeded);
        }

        #region GetPartSizeNeeded

        private int GetPartSizeNeeded(Image image,
            ColumnHeaderVisualStyle style, Alignment alignment, ref Size sizeNeeded)
        {
            int height = 0;

            if (image != null)
            {
                if (alignment != Alignment.TopCenter &&
                    alignment != Alignment.MiddleCenter &&
                    alignment != Alignment.BottomCenter)
                {
                    sizeNeeded.Width += (image.Width + 4);
                }

                if (alignment == Alignment.MiddleLeft ||
                    alignment == Alignment.MiddleCenter ||
                    alignment == Alignment.MiddleRight ||
                    VAlignment(alignment) == VAlignment(style.Alignment))
                {
                    sizeNeeded.Height = Math.Max(sizeNeeded.Height, image.Height);
                }
                else
                {
                    height = (image.Height + 2);
                }
            }

            return (height);
        }

        #region FilterCanBeVisible

        private bool FilterCanBeVisible(GridPanel panel)
        {
            if (panel.EnableFiltering == true && panel.EnableColumnFiltering == true)
            {
                if (FilterImageVisibility != ImageVisibility.Never)
                    return (true);
            }

            return (false);
        }

        #endregion

        #region MeasureHeaderText

        private Size MeasureHeaderText(Graphics g,
            GridColumn column, CellVisualStyle style, Size constraintSize)
        {
            Size size = Size.Empty;

            string s = column.GetHeaderText();

            if (string.IsNullOrEmpty(s) == false)
            {
                if (column.HeaderTextMarkup != null)
                {
                    size = GetMarkupTextSize(g,
                        column.HeaderTextMarkup, style, constraintSize.Width);

                    column.HeaderTextSize = size;
                }
                else
                {
                    eTextFormat tf = style.GetTextFormatFlags();

                    size = (constraintSize.IsEmpty == true)
                               ? TextHelper.MeasureText(g, s, style.Font)
                               : TextHelper.MeasureText(g, s, style.Font, constraintSize, tf);
                }
            }

            return (size);
        }

        #region GetMarkupTextSize

        private Size GetMarkupTextSize(Graphics g, 
            BodyElement textMarkup, CellVisualStyle style, int width)
        {
            MarkupDrawContext d =
                new MarkupDrawContext(g, style.Font, style.TextColor, false);

            textMarkup.InvalidateElementsSize();
            textMarkup.Measure(new Size(width, 0), d);

            return (textMarkup.Bounds.Size);
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #endregion

        #region ArrangeOverride

        /// <summary>
        /// Performs the arrange pass layout of the item
        /// when final position and size of the item has been set.
        /// </summary>
        /// <param name="layoutInfo">Layout information.</param>
        /// <param name="stateInfo"></param>
        /// <param name="layoutBounds">Layout bounds</param>
        protected override void ArrangeOverride(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, Rectangle layoutBounds)
        {
        }

        #endregion

        #region RenderOverride

        /// <summary>
        /// Performs drawing of the item and its children.
        /// </summary>
        /// <param name="renderInfo">Holds contextual rendering information.</param>
        protected override void RenderOverride(GridRenderInfo renderInfo)
        {
            GridPanel panel = Parent as GridPanel;

            if (panel != null)
            {
                Graphics g = renderInfo.Graphics;
                Rectangle bounds = Bounds;

                ColumnHeaderRowVisualStyle style = GetEffectiveRowHeaderStyle();
                GridPanelVisualStyle pstyle = panel.GetEffectiveStyle();

                if (SuperGrid.DoPreRenderColumnHeaderEvent(g,
                    this, null, RenderParts.Background, bounds) == false)
                {
                    RenderRowBackground(g, style, bounds);

                    SuperGrid.DoPostRenderColumnHeaderEvent(g,
                        this, null, RenderParts.Background, bounds);
                }

                RenderColumnBorder(g, panel, bounds);

                if (panel.IsSubPanel == true ||
                    panel.FrozenColumnCount <= 0)
                {
                    RenderAllColumns(renderInfo, panel, pstyle, bounds);
                }
                else
                {
                    RenderScrollableColumns(renderInfo, panel);
                    RenderFrozenColumns(renderInfo, panel, pstyle, bounds);
                }

                RenderRowHeader(g, panel, style, pstyle);
            }
        }

        #region RenderRowBackground

        private void RenderRowBackground(
            Graphics g, ColumnHeaderRowVisualStyle style, Rectangle bounds)
        {
            using (Brush br = style.WhiteSpaceBackground.GetBrush(bounds))
                    g.FillRectangle(br, bounds);
        }

        #endregion

        #region RenderAllColumns

        private void RenderAllColumns(GridRenderInfo renderInfo,
            GridPanel panel, GridPanelVisualStyle pstyle, Rectangle bounds)
        {
            Graphics g = renderInfo.Graphics;
            GridColumnCollection columns = _Columns;

            GridColumn lastCol = null;

            int[] map = columns.DisplayIndexMap;
            for (int i = 0; i < map.Length; i++)
            {
                int index = map[i];

                GridColumn column = columns[index];

                if (column.Visible == true)
                {
                    Rectangle r = GetBounds(panel, column);

                    if (r.IntersectsWith(renderInfo.ClipRectangle) == true)
                        RenderColumnHeader(g, panel, column, r, true);

                    if (column.IsHFrozen == true)
                        lastCol = column;
                }
            }

            RenderFrozenColumnMarker(g, panel, pstyle, lastCol, bounds);
        }

        #endregion

        #region RenderScrollableColumns

        private void RenderScrollableColumns(
            GridRenderInfo renderInfo, GridPanel panel)
        {
            Graphics g = renderInfo.Graphics;
            GridColumnCollection columns = _Columns;

            int[] map = columns.DisplayIndexMap;
            for (int i = 0; i < map.Length; i++)
            {
                int index = map[i];

                GridColumn column = columns[index];

                if (column.Visible == true && column.IsHFrozen == false)
                {
                    Rectangle r = GetBounds(panel, column);

                    if (r.IntersectsWith(renderInfo.ClipRectangle) == true)
                        RenderColumnHeader(g, panel, column, r, true);
                }
            }
        }

        #endregion

        #region RenderFrozenColumns

        private void RenderFrozenColumns(GridRenderInfo renderInfo,
            GridPanel panel, GridPanelVisualStyle pstyle, Rectangle bounds)
        {
            Graphics g = renderInfo.Graphics;
            GridColumnCollection columns = _Columns;

            GridColumn lastCol = null;

            int[] map = columns.DisplayIndexMap;
            for (int i = 0; i < map.Length; i++)
            {
                int index = map[i];

                GridColumn column = columns[index];

                if (column.Visible == true && column.IsHFrozen == true)
                {
                    Rectangle r = GetBounds(panel, column);

                    if (r.IntersectsWith(renderInfo.ClipRectangle) == true)
                        RenderColumnHeader(g, panel, column, r, true);

                    lastCol = column;
                }
            }

            RenderFrozenColumnMarker(g, panel, pstyle, lastCol, bounds);
        }

        #endregion

        #region RenderColumnBorder

        private void RenderColumnBorder(
            Graphics g, GridPanel panel, Rectangle bounds)
        {
            Rectangle r = bounds;
            GridPanelVisualStyle pstyle = panel.GetEffectiveStyle();

            if (pstyle.HeaderHLinePattern != LinePattern.None &&
                pstyle.HeaderHLinePattern != LinePattern.NotSet)
            {
                using (Pen pen = new Pen(pstyle.HeaderLineColor))
                {
                    pen.DashStyle = (DashStyle)pstyle.HeaderHLinePattern;

                    g.DrawLine(pen, r.X, r.Bottom - 1, r.Right - 2, r.Bottom - 1);
                    g.DrawLine(pen, r.X, r.Top - 1, r.Right - 2, r.Top - 1);
                }
            }
            
            if (pstyle.HeaderVLinePattern != LinePattern.None &&
                pstyle.HeaderVLinePattern != LinePattern.NotSet)
            {
                using (Pen pen = new Pen(pstyle.HeaderLineColor))
                {
                    pen.DashStyle = (DashStyle)pstyle.HeaderVLinePattern;

                    g.DrawLine(pen, r.Right - 1, r.Top - 1, r.Right - 1, r.Bottom - 1);
                }
            }
        }

        #endregion

        #region RenderRowHeader

        private void RenderRowHeader(Graphics g,
            GridPanel panel, ColumnHeaderRowVisualStyle style, GridPanelVisualStyle pstyle)
        {
            Rectangle r = GetRowHeaderBounds(panel);

            if (r.Width > 0 && r.Height > 0)
            {
                if (SuperGrid.DoPreRenderColumnHeaderEvent(g,
                    this, null, RenderParts.RowHeader, r) == false)
                {
                    using (Brush br = style.RowHeader.Background.GetBrush(r))
                        g.FillRectangle(br, r);

                    using (Pen pen = new Pen(style.RowHeader.BorderHighlightColor))
                    {
                        g.DrawLine(pen, r.X, r.Top, r.Right - 2, r.Top);
                        g.DrawLine(pen, r.X + 1, r.Top, r.X + 1, r.Bottom - 1);

                        using (Pen pen2 = new Pen(pstyle.HeaderLineColor))
                        {
                            g.DrawLine(pen2, r.X, r.Top - 1, r.Right - 2, r.Top - 1);
                            g.DrawLine(pen2, r.X, r.Bottom - 1, r.Right - 2, r.Bottom - 1);
                            g.DrawLine(pen2, r.Right - 1, r.Top, r.Right - 1, r.Bottom - 1);
                        }
                    }

                    if (panel.TopLeftHeaderSelectBehavior != TopLeftHeaderSelectBehavior.NoSelection)
                    {
                        RenderRowHeaderIndicator(g, panel, style, r);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(_RowHeaderText) == false)
                            RenderRowHeaderText(g, _RowHeaderText, style, r);
                    }

                    SuperGrid.DoPostRenderColumnHeaderEvent(g,
                        this, null, RenderParts.RowHeader, r);
                }
            }
        }

        #region RenderRowHeaderIndicator

        private void RenderRowHeaderIndicator(Graphics g,
            GridPanel panel, ColumnHeaderRowVisualStyle style, Rectangle r)
        {
            r.Inflate(-3, -3);

            if (r.Width > 0 && r.Height > 0)
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    int n = Math.Min(r.Width, r.Height);

                    if (n > 10)
                        n = 10;

                    Point[] pts = new Point[3];

                    if (_SelectAllCount != panel.SelectionUpdateCount)
                    {
                        r.X--;
                        r.Y--;

                        pts[0] = new Point(r.Right, r.Bottom - n);
                        pts[1] = new Point(r.Right, r.Bottom);
                        pts[2] = new Point(r.Right - n, r.Bottom);
                    }
                    else
                    {
                        pts[0] = new Point(r.X, r.Y);
                        pts[1] = new Point(r.X + n, r.Y);
                        pts[2] = new Point(r.X, r.Y + n);

                        r.X++;
                        r.Y++;
                    }

                    path.AddPolygon(pts);
                    path.CloseFigure();

                    if (style.IndicatorBackground.GradientAngle % 45 == 0)
                        n /= 2;

                    r.Width = n - 1;
                    r.Height = n - 1;

                    if (n > 0)
                    {
                        g.SmoothingMode = SmoothingMode.AntiAlias;

                        using (Brush br = style.IndicatorBackground.GetBrush(r))
                        {
                            if (br is LinearGradientBrush)
                                ((LinearGradientBrush) br).WrapMode = WrapMode.Tile;

                            g.FillPath(br, path);
                        }

                        using (Pen pen = new Pen(style.IndicatorBorderColor))
                            g.DrawPath(pen, path);
                    }
                }
            }
        }

        #endregion

        #region RenderRowHeaderText

        private void RenderRowHeaderText(Graphics g,
            string text, ColumnHeaderRowVisualStyle style, Rectangle r)
        {
            Font font = style.RowHeader.Font ?? SystemFonts.DefaultFont;

            Size tSize = TextHelper.MeasureText(g, text, font);

            r.Inflate(-2, -2);

            eTextFormat tf = ((r.Width <= tSize.Width)
                  ? eTextFormat.Left
                  : eTextFormat.HorizontalCenter) | eTextFormat.NoPadding;

            tf |= eTextFormat.WordBreak | eTextFormat.VerticalCenter;

            TextDrawing.DrawString(g, text,
                font, style.RowHeader.TextColor, r, tf);
        }

        #endregion

        #endregion

        #region RenderColumnHeader

        private void RenderColumnHeader(Graphics g,
            GridPanel panel, GridColumn column, Rectangle bounds, bool fullRender)
        {
            ColumnHeaderVisualStyle style = GetEffectiveStyle(column);

            if (bounds.Width > 0 && bounds.Height > 0)
            {
                if (SuperGrid.DoPreRenderColumnHeaderEvent(g,
                    this, column, RenderParts.Background, bounds) == false)
                {
                    RenderBackground(g, style, bounds);

                    SuperGrid.DoPostRenderColumnHeaderEvent(g,
                        this, column, RenderParts.Background, bounds);
                }

                RenderColumnBorder(g, panel, bounds);

                Rectangle r = bounds;
                r.Height = BoundsRelative.Height;

                Rectangle t = GetContentBounds(panel, column, r);
                t = GetAdjustedBounds(column, t);

                if (style.ImageOverlay == ImageOverlay.None ||
                    style.ImageOverlay == ImageOverlay.Bottom)
                {
                    RenderHeaderImage(g, panel, style, bounds, t);
                }

                if (fullRender == true)
                    t = RenderSortAndFilterImage(g, column, style, t);

                if (SuperGrid.DoPreRenderColumnHeaderEvent(g,
                    this, column, RenderParts.Content, t) == false)
                {
                    RenderText(g, panel, column, style, t);

                    SuperGrid.DoPostRenderColumnHeaderEvent(g,
                        this, column, RenderParts.Content, t);
                }
                
                if (style.ImageOverlay == ImageOverlay.Top)
                    RenderHeaderImage(g, panel, style, bounds, t);

                r = GetBorderRect(style, r);

                if (SuperGrid.DoPreRenderColumnHeaderEvent(g,
                    this, column, RenderParts.Border, r) == false)
                {
                    if (r.Width > 0 && r.Height > 0)
                        style.RenderBorder(g, r);

                    SuperGrid.DoPostRenderColumnHeaderEvent(g,
                        this, column, RenderParts.Border, r);
                }
            }
        }

        #region GetBorderRect

        private Rectangle GetBorderRect(
            ColumnHeaderVisualStyle style, Rectangle r)
        {
            r.X += style.Margin.Left;
            r.Width -= style.Margin.Horizontal;

            r.Y += style.Margin.Top;
            r.Height -= style.Margin.Vertical;

            return (r);
        }

        #endregion

        #region RenderBackground

        private void RenderBackground(Graphics g,
            VisualStyle style, Rectangle r)
        {
            using (Brush br = style.Background.GetBrush(r))
                g.FillRectangle(br, r);
        }

        #endregion

        #region RenderSortAndFilterImage

        private Rectangle RenderSortAndFilterImage(
            Graphics g, GridColumn column, ColumnHeaderVisualStyle style, Rectangle t)
        {
            GridPanel panel = column.Parent as GridPanel;

            if (panel != null)
            {
                Alignment falign = (_FilterImageAlignment == Alignment.NotSet)
                    ? Alignment.MiddleLeft : _FilterImageAlignment;

                Alignment salign = (_SortImageAlignment == Alignment.NotSet)
                    ? Alignment.TopCenter : _SortImageAlignment;

                Rectangle rf = GetFilterImageBounds(panel, column);

                if (rf.IsEmpty == false)
                {
                    Image filterImage = GetColumnFilterImage(column);

                    if (filterImage != null)
                        g.DrawImageUnscaledAndClipped(filterImage, rf);
                }

                Rectangle rs = GetSortImageBounds(panel, column, salign, falign, rf);

                if (rs.IsEmpty == false)
                {
                    Image sortImage = GetColumnSortImage(panel, column);

                    if (sortImage != null)
                        g.DrawImageUnscaledAndClipped(sortImage, rs);
                }

                if (rf.IsEmpty == false)
                    t = UpdateContentRect(rf, t, falign, style, column);

                if (rs.IsEmpty == false)
                    t = UpdateContentRect(rs, t, salign, style, column);
            }

            t.X += 2;
            t.Width -= 4;

            return (t);
        }

        #region GetFilterImageBounds

        private Rectangle GetFilterImageBounds(GridPanel panel, GridColumn column)
        {
            if (_FilterImageVisibility == ImageVisibility.Always ||
                (column == _HitColumn) || (string.IsNullOrEmpty(column.FilterExpr) == false))
            {
                return (GetScrollBounds(panel, column, column.FilterImageBounds));
            }

            return (Rectangle.Empty);
        }

        #endregion

        #region GetSortImageBounds

        private Rectangle GetSortImageBounds(GridPanel panel,
            GridColumn column, Alignment salign, Alignment falign, Rectangle rf)
        {
            Rectangle r = GetScrollBounds(panel, column, column.SortImageBounds);

            if (r.IsEmpty == false)
            {
                if (falign == salign && r.IsEmpty == false)
                {
                    if (salign == Alignment.TopCenter ||
                        salign == Alignment.MiddleCenter ||
                        salign == Alignment.BottomCenter)
                    {
                        r.X = rf.X - (r.Width + 1);
                    }
                }
            }

            return (r);
        }

        #endregion

        #region UpdateContentRect

        private Rectangle UpdateContentRect(Rectangle r,
            Rectangle t, Alignment alignment, ColumnHeaderVisualStyle style, GridColumn column)
        {
            switch (GetStringAlignment(alignment))
            {
                case StringAlignment.Near:
                    int n1 = r.Right - t.Left;

                    switch (GetStringAlignment(style.Alignment))
                    {
                        case StringAlignment.Center:
                            if (n1 + 3 > (t.Width - column.HeaderTextSize.Width) / 2)
                            {
                                t.X = r.Right;
                                t.Width -= n1;
                            }
                            break;

                        default:
                            t.X = r.Right;
                            t.Width -= n1;
                            break;
                    }
                    break;

                case StringAlignment.Far:
                    int n2 = t.Right - r.Left;

                    switch (GetStringAlignment(style.Alignment))
                    {
                        case StringAlignment.Center:
                            if (n2 + 3 > (t.Width - column.HeaderTextSize.Width) / 2)
                                t.Width -= n2;
                            break;

                        default:
                            t.Width -= n2;
                            break;
                    }
                    break;
            }

            return (t);
        }

        #region GetStringAlignment

        private StringAlignment GetStringAlignment(Alignment alignment)
        {
            switch (alignment)
            {
                case Alignment.TopLeft:
                case Alignment.MiddleLeft:
                case Alignment.BottomLeft:
                    return (StringAlignment.Near);

                case Alignment.TopCenter:
                case Alignment.MiddleCenter:
                case Alignment.BottomCenter:
                    return (StringAlignment.Center);

                default:
                    return (StringAlignment.Far);
            }
        }

        #endregion

        #endregion

        #endregion

        #region VAlignment

        private int VAlignment(Alignment alignment)
        {
            switch (alignment)
            {
                case Alignment.TopLeft:
                case Alignment.TopCenter:
                case Alignment.TopRight:
                    return (0);

                case Alignment.MiddleLeft:
                case Alignment.MiddleCenter:
                case Alignment.MiddleRight:
                    return (1);

                default:
                    return (2);
            }
        }

        #endregion

        #region GetColumnSortImage

        internal Image GetColumnSortImage(GridPanel panel, GridColumn column)
        {
            SortDirection sortDirection = SortDirection.None;

            if (panel.GroupColumns != null &&
                panel.GroupColumns.Contains(column) == true)
            {
                sortDirection = column.GroupDirection;
            }
            else if (panel.SortLevel != SortLevel.None)
            {
                sortDirection = column.SortDirection;

                switch (column.SortIndicator)
                {
                    case SortIndicator.None:
                        sortDirection = SortDirection.None;
                        break;

                    case SortIndicator.Ascending:
                        sortDirection = SortDirection.Ascending;
                        break;

                    case SortIndicator.Descending:
                        sortDirection = SortDirection.Descending;
                        break;
                }
            }

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    return (GetAscendingSortImage(column));

                case SortDirection.Descending:
                    return (GetDescendingSortImage(column));
            }

            return (null);
        }

        #endregion

        #region GetFilterImage

        internal Image GetColumnFilterImage(GridColumn column)
        {
            if (column.IsFilteringEnabled == true)
            {
                if (_FilterImageVisibility != ImageVisibility.Never)
                    return (GetFilterImage(column));
            }

            return (null);
        }

        #endregion

        #region GetImageBounds

        private Rectangle GetImageBounds(
            Image image1, Alignment alignment, ref Rectangle r)
        {
            Rectangle u = r;
            Rectangle t = r;
            t.Size = image1.Size;

            switch (alignment)
            {
                case Alignment.TopLeft:
                    r.X += t.Width + 2;
                    r.Width -= (t.Width + 2);
                  break;

                case Alignment.NotSet:
                case Alignment.TopCenter:
                    t.X += (r.Width - t.Width) / 2;
                    break;

                case Alignment.TopRight:
                    t.X = r.Right - t.Width;
                    r.Width -= (t.Width + 2);
                    break;

                case Alignment.MiddleLeft:
                    t.Y += (r.Height - t.Height) / 2;

                    r.X += t.Width + 2;
                    r.Width -= (t.Width + 2);
                    break;

                case Alignment.MiddleCenter:
                    t.X += (r.Width - t.Width) / 2;
                    t.Y += (r.Height - t.Height) / 2;
                    break;

                case Alignment.MiddleRight:
                    t.X = r.Right - t.Width;
                    t.Y += (r.Height - t.Height) / 2;

                    r.Width -= (t.Width + 2);
                    break;

                case Alignment.BottomLeft:
                    r.X += t.Width + 2;
                    r.Width -= (t.Width + 2);
                    t.Y = r.Bottom - t.Height;
                    break;

                case Alignment.BottomCenter:
                    t.X += (r.Width - t.Width) / 2;
                    t.Y = r.Bottom - t.Height;
                    break;

                case Alignment.BottomRight:
                    t.X = r.Right - t.Width;
                    t.Y = r.Bottom - t.Height;

                    r.Width -= (t.Width + 2);
                    break;
            }

            t.Intersect(u);

            return (t);
        }

        #endregion

        #endregion

        #region RenderHeaderImage

        private void RenderHeaderImage(Graphics g, GridPanel panel,
            ColumnHeaderVisualStyle style, Rectangle bounds, Rectangle r)
        {
            if (_ShowHeaderImages == true)
            {
                Image image = style.GetImage(panel);

                if (image != null)
                {
                    Rectangle t = style.GetImageBounds(image, r);
                    bounds.Inflate(-1, -1);
                    t.Intersect(bounds);

                    if (t.Width > 0 && t.Height > 0)
                        g.DrawImageUnscaledAndClipped(image, t);
                }
            }
        }

        #endregion

        #region RenderText

        private void RenderText(Graphics g, GridPanel panel,
            GridColumn column, CellVisualStyle style, Rectangle bounds)
        {
            string s = column.GetHeaderText();

            if (s != null)
            {
                if (_MouseDownButtons == MouseButtons.Left)
                {
                    if (panel.ColumnHeaderClickBehavior == ColumnHeaderClickBehavior.SortAndReorder)
                    {
                        if (_Reordering == false && column == _MouseDownHitColumn &&
                            _MouseDownHitArea == HeaderArea.InContent)
                        {
                            bounds.X++;
                            bounds.Y += 2;
                            bounds.Height -= 2;
                        }
                    }
                }

                bounds.Width--;
                bounds.Height--;

                if (column.HeaderTextMarkup != null)
                {
                    RenderTextMarkup(g, column.HeaderTextMarkup, style, bounds);
                }
                else
                {
                    eTextFormat tf = style.GetTextFormatFlags();

                    TextDrawing.DrawString(g, s, style.Font, style.TextColor, bounds, tf);
                }
            }
        }

        #region RenderTextMarkup

        private void RenderTextMarkup(Graphics g,
            BodyElement textMarkup, CellVisualStyle style, Rectangle r)
        {
            MarkupDrawContext d =
                new MarkupDrawContext(g, style.Font, style.TextColor, false);

            textMarkup.Arrange(new Rectangle(r.Location, r.Size), d);

            Size size = textMarkup.Bounds.Size;

            switch (style.Alignment)
            {
                case Alignment.MiddleLeft:
                case Alignment.MiddleCenter:
                case Alignment.MiddleRight:
                    if (r.Height > size.Height)
                        r.Y += (r.Height - size.Height) / 2;
                    break;

                default:
                    if (r.Height > size.Height)
                        r.Y = r.Bottom - size.Height;
                    break;
            }

            textMarkup.Bounds = new Rectangle(r.Location, size);

            Region oldClip = g.Clip;

            try
            {
                g.SetClip(r, CombineMode.Intersect);

                textMarkup.Render(d);
            }
            finally
            {
                g.Clip = oldClip;
            }
        }

        #endregion

        #endregion

        #endregion

        #region RenderFrozenColumnMarker

        private void RenderFrozenColumnMarker(Graphics g,
            GridPanel panel, GridPanelVisualStyle pstyle, GridColumn lastCol, Rectangle bounds)
        {
            if (lastCol != null)
            {
                Rectangle r = lastCol.BoundsRelative;

                r.Y = bounds.Y + 3;
                r.Height = bounds.Height - 5;

                if (panel.IsSubPanel == true)
                    r.X -= HScrollOffset;

                if (r.Height > 0 && r.Width > 0)
                {
                    using (Pen pen = new Pen(pstyle.HeaderLineColor))
                    {
                        g.DrawLine(pen, r.Right - 3,
                                   r.Top - 1, r.Right - 3, r.Bottom - 1);
                    }
                }
            }
        }

        #endregion

        #region Mouse support

        #region InternalMouseEnter

        internal override void InternalMouseEnter(EventArgs e)
        {
            SuperGrid.ToolTipText = "";

            base.InternalMouseEnter(e);
        }

        #endregion

        #region InternalMouseLeave

        internal override void InternalMouseLeave(EventArgs e)
        {
            if (_LockedColumn == false)
            {
                ResetColumnState();

                _HitArea = HeaderArea.NoWhere;
                _HitColumn = null;
            }

            SuperGrid.ToolTipText = "";

            base.InternalMouseLeave(e);
        }

        #region ResetColumnState

        internal void ResetColumnState()
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                if (_MouseDownHitColumn != null)
                    InvalidateHeader(panel, _MouseDownHitColumn);

                if (_HitColumn != null && (_MouseDownHitColumn != _HitColumn))
                    InvalidateHeader(panel, _HitColumn);

                if (_HitArea == HeaderArea.InRowHeader)
                    InvalidateRowHeader();

                _MouseDownHitColumn = null;
            }

            _LockedColumn = false;
        }

        internal void ResetColumnStateEx()
        {
            ResetColumnState();

            _HitColumn = null;
            _HitArea = HeaderArea.NoWhere;
        }

        #endregion

        #endregion

        #region InternalMouseMove

        internal override void InternalMouseMove(MouseEventArgs e)
        {
            GridPanel panel = Parent as GridPanel;

            if (panel != null)
            {
                if (AllowSelection == true)
                {
                    _LastHitArea = _HitArea;
                    _LastHitColumn = _HitColumn;

                    if (IsMouseDown == true)
                    {
                        if (ProcessMouseDownMove(e) == false)
                        {
                            if (_MouseDownHitColumn != null)
                            {
                                SuperGrid.EnableAutoScrolling(AutoScrollEnable.Horizontal, ViewRect);

                                StopResize();
                                StopReorder();
                            }
                        }
                    }
                    else
                    {
                        _HitArea = GetHitArea(e.Location, ref _HitColumn, IsMouseDown);

                        switch (_HitArea)
                        {
                            case HeaderArea.InContent:
                                SuperGrid.GridCursor = Cursors.Default;

                                if (_HitColumn.HeaderTextMarkup != null)
                                    _HitColumn.HeaderTextMarkup.MouseMove(SuperGrid, e);

                                if (_ShowToolTips == true)
                                    UpdateToolTip(panel, _HitColumn.ToolTip);
                                break;

                            case HeaderArea.InResize:
                            case HeaderArea.InRowHeaderResize:
                                SuperGrid.GridCursor = Cursors.VSplit;
                                SuperGrid.ToolTipText = "";
                                break;

                            case HeaderArea.InRowHeader:
                                SuperGrid.GridCursor =
                                    (panel.TopLeftHeaderSelectBehavior != TopLeftHeaderSelectBehavior.NoSelection)
                                        ? Cursors.Hand
                                        : Cursors.Default;

                                UpdateToolTip(panel, "");
                                break;

                            case HeaderArea.InFilterMenu:
                                SuperGrid.GridCursor = Cursors.Hand;

                                if (_ShowFilterToolTips == true && _HitColumn.IsFilteringEnabled == true)
                                    UpdateToolTip(panel, _HitColumn.FilterExpr);
                                break;

                            default:
                                SuperGrid.GridCursor = Cursors.Default;
                                SuperGrid.ToolTipText = "";
                                break;
                        }

                        if (_HitArea != _LastHitArea || _LastHitColumn != _HitColumn)
                        {
                            if (_LastHitColumn != null)
                                InvalidateHeader(panel, _LastHitColumn);

                            if (_HitColumn != null)
                            {
                                if ((_LastHitColumn != _HitColumn) ||
                                    (_HitArea == HeaderArea.InFilterMenu || _LastHitArea == HeaderArea.InFilterMenu))
                                {
                                    InvalidateHeader(panel, _HitColumn);
                                }
                            }

                            if (_HitArea == HeaderArea.InRowHeader || _LastHitArea == HeaderArea.InRowHeader)
                                InvalidateRowHeader();
                        }
                    }
                }

                base.InternalMouseMove(e);
            }
        }

        #region UpdateToolTip

        private void UpdateToolTip(GridPanel panel, string toolTip)
        {
            if (_HitArea != _LastHitArea || _LastHitColumn != _HitColumn)
                panel.SetHeaderTooltip(this, _HitColumn, _HitArea, toolTip);
        }

        #endregion

        #region ProcessMouseDownMove

        private bool ProcessMouseDownMove(MouseEventArgs e)
        {
            GridPanel panel = Parent as GridPanel;

            if (panel != null)
            {
                if (_MouseDownHitArea == HeaderArea.InRowHeaderResize)
                {
                    ProcessInRowHeaderResize(e.Location, e);

                    return (true);
                }

                if (_MouseDownHitColumn != null)
                {
                    Rectangle r = ViewRect;

                    if ((_MouseDownHitArea == HeaderArea.InResize) ||
                        (_MouseDownHitArea == HeaderArea.InRowHeaderResize) ||
                        (e.X >= r.X && e.X < r.Right) ||
                        (_MouseDownHitColumn.IsHFrozen == true && panel.IsSubPanel == false))
                    {
                        SuperGrid.DisableAutoScrolling();

                        switch (_MouseDownHitArea)
                        {
                            case HeaderArea.InContent:
                                _HitArea = GetHitArea(e.Location, ref _HitColumn, IsMouseDown);

                                if (_HitArea == HeaderArea.InGroupBox &&
                                    _MouseDownHitColumn.GroupBoxEffectsEx != GroupBoxEffects.None)
                                {
                                    if (ProcessInGroupBox(panel, e) == true)
                                        return (true);
                                }

                                if (_HitColumn == null)
                                    return (false);

                                ProcessInContent(e);
                                break;

                            case HeaderArea.InResize:
                                ProcessInResize(e.Location, e);
                                break;
                        }

                        return (true);
                    }
                }
            }

            return (false);
        }

        #region ProcessInGroupBox

        private bool ProcessInGroupBox(GridPanel panel, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (panel.VisibleColumnCount > 1)
                {
                    if (panel.GroupByRow.DragStart(_MouseDownHitColumn) == true)
                    {
                        ResetColumnState();
                        StopReorder();

                        _MouseDownHitColumn = null;
                        _HitArea = HeaderArea.NoWhere;
                        _HitColumn = null;


                        return (true);
                    }
                }
            }

            return (false);
        }

        #endregion

        #endregion

        #endregion

        #region InternalMouseDown

        internal override void InternalMouseDown(MouseEventArgs e)
        {
            GridPanel panel = Parent as GridPanel;

            if (panel != null)
            {
                GridCell ecell = SuperGrid.EditorCell;

                if (ecell != null)
                {
                    if (ecell.EndEdit() == false)
                    {
                        _MouseDownHitArea = HeaderArea.NoWhere;
                        return;
                    }
                }

                if (_Reordering == true || _Resizing == true)
                {
                    StopResize();
                    StopReorder();

                    InvalidateRender();
                }

                _MouseDownPoint = e.Location;
                _MouseDownHitColumn = _HitColumn;
                _MouseDownHitArea = _HitArea;
                _MouseDownButtons = e.Button;

                _DragSelection = false;
                _DragStarted = false;

                if (_HitArea == HeaderArea.InContent)
                {
                    Capture = true;

                    ProcessInContentMouseDown(e, panel);
                }
                else
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        Capture = true;

                        switch (_HitArea)
                        {
                            case HeaderArea.InResize:
                                ProcessInResizeMouseDown(e, panel);
                                break;

                            case HeaderArea.InRowHeader:
                                ProcessInRowHeaderMouseDown(panel);
                                break;

                            case HeaderArea.InRowHeaderResize:
                                ProcessInRowHeaderResizeMouseDown();
                                break;

                            case HeaderArea.InFilterMenu:
                                ProcessInFilterMouseDown(panel, _HitColumn);
                                break;
                        }
                    }
                }
            }

            base.InternalMouseDown(e);
        }

        #region ProcessInContentMouseDown

        private void ProcessInContentMouseDown(MouseEventArgs e, GridPanel panel)
        {
            if (_HitColumn != null)
            {
                _MouseDownDelta = e.X - _HitColumn.BoundsRelative.X;

                if (panel.IsSubPanel == true || _HitColumn.IsHFrozen == false)
                    _MouseDownDelta += HScrollOffset;

                if (_HitColumn.AllowSelection == true)
                {
                    _LastHitColumn = null;
                    _Reordering = false;

                    if (panel.ColumnHeaderClickBehavior == ColumnHeaderClickBehavior.Select)
                    {
                        if (_HitColumn.IsSelected == false ||
                            ((Control.ModifierKeys & (Keys.Control | Keys.Shift)) != Keys.None))
                        {
                            ExtendSelection(panel, true);
                        }
                        else
                        {
                            _DragSelection = true;
                        }
                    }
                    else
                    {
                        InvalidateHeader(panel, _MouseDownHitColumn);
                    }
                }
            }
        }

        #region ExtendSelection

        private void ExtendSelection(GridPanel panel, bool capture)
        {
            if (capture == true)
                Capture = true;

            if (panel.SelectionColumnAnchor == null ||
                (Control.ModifierKeys & Keys.Shift) != Keys.Shift)
            {
                panel.SelectionColumnAnchor = _MouseDownHitColumn;
            }

            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                _MouseDownHitColumn.IsSelected = !_MouseDownHitColumn.IsSelected;
            else
                ProcessExtendSelection(panel, false);
        }

        #endregion

        #endregion

        #region ProcessInResizeMouseDown

        private void ProcessInResizeMouseDown(MouseEventArgs e, GridPanel panel)
        {
            if (_HitColumn != null)
            {
                Capture = true;

                _MouseDownDelta = e.X - _HitColumn.BoundsRelative.X;

                if (panel.IsSubPanel == true || _HitColumn.IsHFrozen == false)
                    _MouseDownDelta += HScrollOffset;

                _Resizing = false;
                _LastHitColumn = null;
            }
        }

        #endregion

        #region ProcessInRowHeaderMouseDown

        private void ProcessInRowHeaderMouseDown(GridPanel panel)
        {
            if (panel.TopLeftHeaderSelectBehavior != TopLeftHeaderSelectBehavior.NoSelection)
            {
                bool select = (_SelectAllCount != panel.SelectionUpdateCount);

                if (select == true)
                {
                    if (panel.MultiSelect == true)
                    {
                        panel.SelectAll();
                    }
                    else if (panel.ActiveRow != null)
                    {
                        panel.ClearAll();
                        panel.ActiveRow.IsSelected = true;
                    }
                }
                else
                {
                    panel.ClearAll();
                }

                if (select == true)
                    _SelectAllCount = panel.SelectionUpdateCount;
            }
        }

        #endregion

        #region ProcessInRowHeaderResizeMouseDown

        private void ProcessInRowHeaderResizeMouseDown()
        {
            Capture = true;

            _Resizing = false;
        }

        #endregion

        #region ProcessInFilterMouseDown

        private void ProcessInFilterMouseDown(GridPanel panel, GridColumn column)
        {
            ActivateFilterPopup(panel, column);
        }

        internal void ActivateFilterPopup(GridPanel panel, GridColumn column)
        {
            if (_FilterMenu != null)
                _FilterMenu.Dispose();

            _FilterMenu = new FilterPopup(panel);

            _LockedColumn = true;
            _HitArea = HeaderArea.NoWhere;

            _FilterMenu.ActivatePopup(column, ResetColumnState);
        }

        #endregion

        #endregion

        #region InternalMouseUp

        internal override void InternalMouseUp(MouseEventArgs e)
        {
            SuperGrid.DisableAutoScrolling();

            GridPanel panel = Parent as GridPanel;

            if (panel != null)
            {
                GridColumn column = GetHitColumn(panel, e.Location, false);
                
                switch (_MouseDownHitArea)
                {
                    case HeaderArea.InContent:
                        if (_Reordering == true)
                        {
                            StopReorder();

                            if (_SeparatorColumn != null && _HitColumn != _MouseDownHitColumn)
                                ReorderColumn(panel);
                        }
                        else
                        {
                            if (column != null && column == _MouseDownHitColumn)
                            {
                                if (SuperGrid.DoColumnHeaderClickEvent(panel, column, e) == false)
                                {
                                    if (e.Button == MouseButtons.Left)
                                    {
                                        if (_DragSelection == true)
                                            ExtendSelection(panel, false);

                                        if (panel.ColumnHeaderClickBehavior ==
                                            ColumnHeaderClickBehavior.SortAndReorder)
                                        {
                                            if (panel.IsSortable == true)
                                                SortColumn(panel, column);

                                            InvalidateHeader(panel, column);
                                        }

                                        if (column.HeaderTextMarkup != null)
                                            column.HeaderTextMarkup.Click(SuperGrid);
                                    }
                                }
                            }
                        }
                        break;

                    case HeaderArea.InResize:
                        if (_Resizing == true)
                        {
                            StopResize();

                            if (panel.ImmediateResize == false)
                                ResizeColumn(panel, e.Location.X - MouseDownPoint.X);
                        }
                        break;

                    case HeaderArea.InRowHeader:
                        SuperGrid.DoColumnRowHeaderClickEvent(panel);
                        break;

                    case HeaderArea.InRowHeaderResize:
                        if (_Resizing == true)
                        {
                            StopResize();

                            if (panel.ImmediateResize == false)
                                ResizeRowHeader(panel, e.Location.X - MouseDownPoint.X);
                        }
                        break;
                }
            }

            StopReorder();
            StopResize();

            _MouseDownHitColumn = null;

            base.InternalMouseUp(e);
        }

        #endregion

        #region InternalMouseDoubleClick

        internal override void InternalMouseDoubleClick(MouseEventArgs e)
        {
            if (_HitColumn != null)
            {
                if (SuperGrid.DoColumnHeaderDoubleClickEvent(_HitColumn, e) == false)
                {
                    switch (_HitArea)
                    {
                        case HeaderArea.InResize:
                            if (_HitColumn.GetAutoSizeMode() == ColumnAutoSizeMode.None)
                            {
                                RowScope scope = (GridPanel.VirtualMode == true || _HitColumn.GridPanel.Rows.Count > 1000)
                                                     ? RowScope.OnScreenRows
                                                     : RowScope.AllRows;

                                _HitColumn.Width = _HitColumn.GetMaximumCellSize(scope, true).Width + 2;
                            }
                            break;
                    }
                }
            }
            else
            {
                if (_HitArea == HeaderArea.InRowHeader)
                    SuperGrid.DoColumnRowHeaderDoubleClickEvent(GridPanel);
            }

            base.InternalMouseDoubleClick(e);
        }

        #endregion

        #endregion

        #region ProcessInContent

        #region ProcessInContent

        private void ProcessInContent(MouseEventArgs e)
        {
            if (IsDesignerHosted == false)
            {
                if (e.Button == MouseButtons.Left)
                {
                    GridPanel panel = Parent as GridPanel;

                    if (panel != null)
                    {
                        if (DragStarted(panel, e) == false)
                        {
                            if (panel.ColumnDragBehavior == ColumnDragBehavior.ExtendClickBehavior)
                            {
                                switch (panel.ColumnHeaderClickBehavior)
                                {
                                    case ColumnHeaderClickBehavior.SortAndReorder:
                                        ProcessMove(e.Location);
                                        break;

                                    case ColumnHeaderClickBehavior.Select:
                                        if (_HitColumn != _LastHitColumn)
                                            ProcessExtendSelection(panel, true);
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }

        #region DragStarted

        private bool DragStarted(GridPanel panel, MouseEventArgs e)
        {
            if (_DragSelection == true)
            {
                if (_DragStarted == false)
                {
                    if (DragDrop.DragStarted(_MouseDownPoint, e.Location) == true)
                    {
                        _DragStarted = true;

                        if (SuperGrid.DoItemDragEvent(_HitColumn, e) == true)
                        {
                            _DragSelection = false;

                            ExtendSelection(panel, true);
                        }
                    }
                }

                return (true);
            }

            return (false);
        }

        #endregion

        #endregion

        #region ProcessMove

        private void ProcessMove(Point pt)
        {
            if (_Reordering == false)
                StartReorder(pt);
            else
                ContinueReorder(pt);
        }

        #endregion

        #region ProcessExtendSelection

        private void ProcessExtendSelection(GridPanel panel, bool extend)
        {
            int startIndex = panel.Columns.GetDisplayIndex(panel.SelectionColumnAnchor);
            int endIndex = panel.Columns.GetDisplayIndex(_HitColumn);

            if (startIndex > endIndex)
            {
                int tempIndex = startIndex;
                startIndex = endIndex;
                endIndex = tempIndex;
            }

            if (panel.OnlyColumnsSelected(startIndex, endIndex) == false)
            {
                if (extend == false)
                    panel.ClearAll();

                int[] map = panel.Columns.DisplayIndexMap;

                for (int i = 0; i < map.Length; i++)
                {
                    GridColumn column = panel.Columns[map[i]];

                    if (column.Visible == true)
                    {
                        column.IsSelected =
                            (i >= startIndex && i <= endIndex);
                    }
                }
            }
        }

        #endregion

        #endregion

        #region ProcessInResize

        private void ProcessInResize(Point pt, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_Resizing == false)
                {
                    StartResize();
                }
                else
                {
                    GridPanel panel = Parent as GridPanel;

                    if (panel != null)
                    {
                        if (panel.ImmediateResize == false)
                            ContinueResize(panel, pt);
                        else
                            ResizeColumn(panel, pt.X - MouseDownPoint.X);
                    }
                }
            }
        }

        #endregion

        #region ProcessInRowHeaderResize

        private void ProcessInRowHeaderResize(Point pt, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridPanel panel = Parent as GridPanel;

                if (panel != null)
                {
                    if (_Resizing == false)
                    {
                        _Resizing = true;
                        _ResizeWidth = panel.RowHeaderWidth;

                        if (panel.ImmediateResize == false)
                        {
                            _SeparatorFw = new FloatWindow();
                            _SeparatorFw.Opacity = .5;
                            _SeparatorFw.BackColor = Color.Black;
                            _SeparatorFw.Owner = SuperGrid.FindForm();
                        }
                    }
                    else
                    {
                        if (panel.ImmediateResize == false)
                            ContinueRowHeaderResize(panel, pt);
                        else
                            ResizeRowHeader(panel, pt.X - MouseDownPoint.X);
                    }
                }
            }
        }

        #endregion

        #region Column Reorder

        #region StartReorder

        private void StartReorder(Point pt)
        {
            if (_MouseDownHitColumn != null && Math.Abs(MouseDownPoint.X - pt.X) > 5)
            {
                Capture = true;

                _Reordering = true;
                _SeparatorColumn = null;

                _HeaderFw = new FloatWindow();
                _HeaderFw.Opacity = .3;
                _HeaderFw.Owner = _MouseDownHitColumn.SuperGrid.FindForm();
                _HeaderFw.Paint += ColumnHeaderFwPaint;

                _SeparatorFw = new FloatWindow();
                _SeparatorFw.Opacity = .5;
                _SeparatorFw.BackColor = Color.Black;
                _SeparatorFw.Owner = _MouseDownHitColumn.SuperGrid.FindForm();
            }
        }

        #endregion

        #region StopReorder

        private void StopReorder()
        {
            _Reordering = false;

            if (_HeaderFw != null)
            {
                _HeaderFw.Close();
                _HeaderFw.Paint -= ColumnHeaderFwPaint;
                _HeaderFw.Dispose();

                _HeaderFw = null;
            }

            if (_SeparatorFw != null)
            {
                _SeparatorFw.Close();
                _SeparatorFw.Dispose();

                _SeparatorFw = null;
            }

            if (_MouseDownHitColumn != null)
                InvalidateHeader(GridPanel, _MouseDownHitColumn);

            if (_HitColumn != null)
                InvalidateHeader(GridPanel, _HitColumn);
        }

        #endregion

        #region ContinueReorder

        private void ContinueReorder(Point pt)
        {
            GridPanel panel = Parent as GridPanel;

            if (panel != null)
            {
                GridColumn column = _HitColumn;

                if (column != null)
                {
                    ReorderHeader(pt);

                    if (_MouseDownHitColumn.IsHFrozen == column.IsHFrozen)
                        ReorderSeparator(pt);
                }
            }
        }

        #region ReorderHeader

        private void ReorderHeader(Point pt)
        {
            GridPanel panel = Parent as GridPanel;

            if (panel != null)
            {
                Rectangle r = GetReorderHeaderBounds(panel, pt);

                if (r.Width <= 0 || r.Height <= 0)
                {
                    _HeaderFw.Hide();
                }
                else
                {
                    r.Location = SuperGrid.PointToScreen(r.Location);

                    Size size = _HeaderFw.Size;

                    _HeaderFw.Bounds = r;

                    if (_HeaderFw.Visible == false)
                        _HeaderFw.Show();

                    else if (r.Size.Equals(size) == false)
                        _HeaderFw.Refresh();
                }
            }
        }

        #region GetReorderHeaderBounds

        private Rectangle GetReorderHeaderBounds(GridPanel panel, Point pt)
        {
            Rectangle r = BoundsRelative;
            r.X = (pt.X - _MouseDownDelta);

            if (IsVFrozen == false)
                r.Y -= VScrollOffset;

            r.Width = _MouseDownHitColumn.BoundsRelative.Width;
            r.Height = (panel.BoundsRelative.Height - (BoundsRelative.Y - panel.BoundsRelative.Y));

            Rectangle t = SViewRect;

            int x = BoundsRelative.X;

            if (panel.IsSubPanel == true || _MouseDownHitColumn.IsHFrozen == false)
                x -= HScrollOffset;

            x = Math.Max(x, t.X);

            if (r.X < x)
            {
                _OrderTextOffset = r.X - x;

                r.X = x;
                r.Width += _OrderTextOffset;
            }
            else
            {
                _OrderTextOffset = 0;
            }

            int n = BoundsRelative.Right;

            if (panel.IsSubPanel == true || _MouseDownHitColumn.IsHFrozen == false)
                n -= HScrollOffset;

            x = Math.Min(n, t.Right) - 1;

            if (r.Right > x)
                r.Width -= (r.Right - x);

            if (r.Bottom > t.Bottom)
                r.Height -= (r.Bottom - t.Bottom);

            return (r);
        }

        #endregion

        #endregion

        #region ReorderSeparator

        private void ReorderSeparator(Point pt)
        {
            GridColumn column = _HitColumn;
            GridPanel panel = column.Parent as GridPanel;

            if (panel != null)
            {
                Rectangle bounds = column.BoundsRelative;

                if (column.IsHFrozen == false)
                    bounds.X -= HScrollOffset;

                bool isRight = (pt.X > bounds.X + bounds.Width / 2);

                if (_SeparatorColumn != column || _SeparatorIsRight != isRight)
                {
                    _SeparatorColumn = column;
                    _SeparatorIsRight = isRight;

                    Rectangle r = GetReorderSeparatorBounds(panel, column, isRight);

                    if (r.Width > 0 && r.Height > 0)
                    {
                        r.Location = column.SuperGrid.PointToScreen(r.Location);

                        _SeparatorFw.Show();

                        _SeparatorFw.Bounds = r;
                        _SeparatorFw.Size = r.Size;
                    }
                    else
                    {
                        if (_SeparatorFw != null)
                            _SeparatorFw.Hide();
                    }
                }
            }
        }

        #region GetReorderSeparatorBounds

        private Rectangle GetReorderSeparatorBounds(
            GridPanel panel, GridColumn column, bool isRight)
        {
            Rectangle r = panel.BoundsRelative;

            r.Y = BoundsRelative.Y;
            r.Height -= (BoundsRelative.Y - panel.BoundsRelative.Y);

            if (panel.IsSubPanel == true)
                r.Y -= VScrollOffset;

            r.Width = SeparatorWidth;

            r.X = ((isRight == true)
                       ? column.BoundsRelative.Right
                       : column.BoundsRelative.X) - 2;

            if (panel.IsSubPanel == true || column.IsHFrozen == false)
                r.X -= HScrollOffset;

            if (r.X >= panel.BoundsRelative.Right - 2)
                r.X = panel.BoundsRelative.Right - 3;

            Rectangle t = SViewRect;

            if (r.Bottom > t.Bottom)
                r.Height -= (r.Bottom - t.Bottom);

            if (r.X + 2 >= t.X && r.Right < t.Right)
                return (r);

            return (Rectangle.Empty);
        }

        #endregion

        #endregion

        #endregion

        #region ReorderColumn

        private void ReorderColumn(GridPanel panel)
        {
            GridColumnCollection columns = _Columns;

            int[] map = columns.DisplayIndexMap;

            int curIndex = columns.GetDisplayIndex(_MouseDownHitColumn);
            int sepIndex = columns.GetDisplayIndex(_SeparatorColumn);

            if (curIndex > sepIndex && _SeparatorIsRight == true && sepIndex < map.Length - 1)
                sepIndex++;

            else if (curIndex < sepIndex && _SeparatorIsRight == false && sepIndex > 0)
                sepIndex--;

            if (sepIndex != curIndex)
            {
                if (sepIndex < curIndex)
                {
                    for (int i = curIndex; i > sepIndex; i--)
                        map[i] = map[i - 1];
                }
                else
                {
                    for (int i = curIndex; i < sepIndex; i++)
                        map[i] = map[i + 1];
                }

                map[sepIndex] = _MouseDownHitColumn.ColumnIndex;

                for (int i = 0; i < map.Length; i++)
                    columns[map[i]].DisplayIndex = i;

                SuperGrid.UpdateStyleCount();
                SuperGrid.PrimaryGrid.InvalidateRender();

                SuperGrid.DoColumnMovedEvent(panel, _MouseDownHitColumn);
            }
        }

        #endregion

        #region ColumnHeaderFwPaint

        void ColumnHeaderFwPaint(object sender, PaintEventArgs e)
        {
            GridPanel panel = GridPanel;
            Rectangle r = _MouseDownHitColumn.BoundsRelative;

            r.Y = 0;
            r.X = _OrderTextOffset;
            r.Height = e.ClipRectangle.Height;

            RenderColumnHeader(e.Graphics, panel, _MouseDownHitColumn, r, false);

            r.Width--;
            r.Height--;
            e.Graphics.DrawRectangle(Pens.DimGray, r);
        }

        #endregion

        #region SortColumn

        internal void SortColumn(GridPanel panel, GridColumn column)
        {
            if (panel.IsSortable == true)
            {
                if (column.IsGroupColumn == true)
                {
                    column.GroupDirection =
                        ToggleSortDirection(column.GroupDirection);
                }
                else if (column.ColumnSortMode != ColumnSortMode.None)
                {
                    SortDirection sortDirection =
                        ToggleSortDirection(column.SortDirection);

                    if (column.ColumnSortMode == ColumnSortMode.Multiple &&
                         ((Control.ModifierKeys & Keys.Shift) == Keys.Shift))
                    {
                        panel.AddSort(column, sortDirection);
                    }
                    else
                    {
                        if (panel.SortColumns.Count > 1)
                            sortDirection = SortDirection.Ascending;

                        panel.SetSort(column, sortDirection);
                    }

                    SuperGrid.DoSortChangedEvent(panel);
                }
                else
                {
                    InvalidateHeader(panel, column);
                }
            }
            else
            {
                InvalidateHeader(panel, column);
            }
        }

        #region ToggleSortDirection

        private SortDirection ToggleSortDirection(SortDirection dir)
        {
            switch (dir)
            {
                case SortDirection.None:
                case SortDirection.Descending:
                    return (SortDirection.Ascending);

                default:
                    return (SortDirection.Descending);
            }
        }

        #endregion

        #endregion

        #endregion

        #region Column Resize

        #region StartResize

        private void StartResize()
        {
            GridPanel panel = Parent as GridPanel;

            if (panel != null)
            {
                _ResizeColumn = _MouseDownHitColumn;

                if (_ResizeColumn != null)
                {
                    _Resizing = true;
                    _ResizeWidth = _ResizeColumn.Size.Width;

                    if (panel.ImmediateResize == false)
                    {
                        _SeparatorFw = new FloatWindow();
                        _SeparatorFw.Opacity = .5;
                        _SeparatorFw.BackColor = Color.Black;
                        _SeparatorFw.Owner = SuperGrid.FindForm();
                    }
                }
            }
        }

        #endregion

        #region StopResize

        private void StopResize()
        {
            _Resizing = false;

            if (_SeparatorFw != null)
            {
                _SeparatorFw.Close();
                _SeparatorFw.Dispose();

                _SeparatorFw = null;
            }
        }

        #endregion

        #region ContinueResize

        private void ContinueResize(GridPanel panel, Point pt)
        {
            Rectangle r = panel.BoundsRelative;
            r.Y = BoundsRelative.Y;

            if (panel.IsSubPanel == true)
                r.Y -= VScrollOffset;

            r.X -= HScrollOffset;

            if (pt.X > r.X)
            {
                Rectangle t = _ResizeColumn.BoundsRelative;
                t.X -= HScrollOffset;

                int width = pt.X - t.X;

                if (width < _ResizeColumn.MinimumWidth)
                    pt.X = t.X + _ResizeColumn.MinimumWidth;

                if (pt.X >= SuperGrid.PrimaryGrid.BoundsRelative.Right - 2)
                {
                    _SeparatorFw.Hide();
                }
                else
                {
                    if (pt.X == panel.BoundsRelative.Right - 1)
                        pt.X--;

                    r.X = pt.X;

                    r.Width = SeparatorWidth;
                    r.Height -= (BoundsRelative.Y - panel.BoundsRelative.Y);

                    t = SViewRect;

                    if (r.Bottom > t.Bottom)
                        r.Height -= (r.Bottom - t.Bottom);

                    r.Location = SuperGrid.PointToScreen(r.Location);

                    _SeparatorFw.Show();

                    _SeparatorFw.Bounds = r;
                    _SeparatorFw.Size = r.Size;
                }
            }
        }

        #endregion

        #region ResizeColumn

        #region ResizeColumn

        private void ResizeColumn(GridPanel panel, int width)
        {
            ColumnAutoSizeMode autoSizeMode = _ResizeColumn.GetAutoSizeMode();

            if (autoSizeMode == ColumnAutoSizeMode.None)
                ResizeNoneColumn(panel, width);

            else if (autoSizeMode == ColumnAutoSizeMode.Fill)
                ResizeFillColumn(panel, width);
        }

        #endregion

        #region ResizeNoneColumn

        private void ResizeNoneColumn(GridPanel panel, int width)
        {
            int newWidth = _ResizeWidth + width;

            if (newWidth < _ResizeColumn.MinimumWidth)
                newWidth = _ResizeColumn.MinimumWidth;

            if (_ResizeColumn.ResizeMode == ColumnResizeMode.MoveFollowingElements)
            {
                _ResizeColumn.Width = newWidth;
            }
            else if (_ResizeColumn.ResizeMode == ColumnResizeMode.MaintainTotalWidth)
            {
                GridColumn ncolumn = panel.Columns.GetNextVisibleColumn(_ResizeColumn);

                if ((ncolumn == null) || (ncolumn.GetAutoSizeMode() != ColumnAutoSizeMode.None))
                {
                    _ResizeColumn.Width = newWidth;
                }
                else
                {
                    int twidth = _ResizeColumn.Size.Width + ncolumn.Size.Width;
                    int nwidth = twidth - newWidth;

                    if (nwidth < ncolumn.MinimumWidth)
                        nwidth = ncolumn.MinimumWidth;

                    newWidth = twidth - nwidth;

                    _ResizeColumn.Width = newWidth;
                    ncolumn.Width = nwidth;
                }
            }
        }

        #endregion

        #region ResizeFillColumn

        private void ResizeFillColumn(GridPanel panel, int width)
        {
            int fillWeight = 0;
            int fillWidth = 0;

            GridColumnCollection columns = _Columns;

            int colDisplayIndex = columns.GetDisplayIndex(_ResizeColumn);

            int[] map = columns.DisplayIndexMap;
            for (int i = 0; i < map.Length; i++)
            {
                int index = map[i];

                GridColumn column = columns[index];

                if (column.Visible == true)
                {
                    if (column.GetAutoSizeMode() == ColumnAutoSizeMode.Fill)
                    {
                        fillWidth += column.Size.Width;

                        if (i != colDisplayIndex)
                            fillWeight += column.FillWeight;
                    }
                }
            }

            if (fillWidth > 0 && fillWeight > 0)
            {
                int nw = _ResizeWidth + width;
                int newFillWidth = fillWidth - nw;

                for (int i = 0; i < map.Length; i++)
                {
                    int index = map[i];

                    GridColumn column = columns[index];

                    if (column.Visible == true)
                    {
                        if (column.GetAutoSizeMode() == ColumnAutoSizeMode.Fill)
                        {
                            float colWidth;

                            if (i != colDisplayIndex)
                                colWidth = (int)(newFillWidth * (float)column.FillWeight / fillWeight);
                            else
                                colWidth = nw;

                            if (colWidth < column.MinimumWidth)
                                colWidth = column.MinimumWidth;

                            column.FillWeight = (int)((colWidth * 1000) / fillWidth);
                        }
                    }
                }

                panel.SuperGrid.Invalidate();
            }
        }

        #endregion

        #endregion

        #endregion

        #region RowHeader Resize

        #region ContinueRowHeaderResize

        private void ContinueRowHeaderResize(GridPanel panel, Point pt)
        {
            Rectangle r = panel.BoundsRelative;
            r.Y = BoundsRelative.Y;

            if (panel.IsSubPanel == true)
                r.Y -= VScrollOffset;

            r.X -= HScrollOffset;

            if (pt.X > r.X)
            {
                int width = pt.X - r.X;

                if (width < 5)
                    pt.X = r.X + 5;

                if (pt.X >= SuperGrid.PrimaryGrid.BoundsRelative.Right - 2)
                {
                    _SeparatorFw.Hide();
                }
                else
                {
                    if (pt.X == panel.BoundsRelative.Right - 1)
                        pt.X--;

                    r.X = pt.X;

                    r.Width = SeparatorWidth;
                    r.Height -= (BoundsRelative.Y - panel.BoundsRelative.Y);

                    Rectangle t = SViewRect;

                    if (r.Bottom > t.Bottom)
                        r.Height -= (r.Bottom - t.Bottom);

                    r.Location = SuperGrid.PointToScreen(r.Location);

                    _SeparatorFw.Show();

                    _SeparatorFw.Bounds = r;
                    _SeparatorFw.Size = r.Size;
                }
            }
        }

        #endregion

        #region ResizeRowHeader

        private void ResizeRowHeader(GridPanel panel, int width)
        {
            Rectangle t = panel.ViewRect;

            int n = Math.Max(5, _ResizeWidth + width);

            if (t.Right - 10 < n)
                n = t.Right - 10;

            if (panel.RowHeaderWidth != n)
            {
                panel.RowHeaderWidth = n;

                panel.SuperGrid.Invalidate();

                SuperGrid.DoRowHeaderResizedEvent(panel);
            }
        }

        #endregion

        #endregion

        #region GetHitArea

        ///<summary>
        /// Returns the ColumnHeader hit area
        /// and column for the given Point
        ///</summary>
        ///<param name="pt"></param>
        ///<param name="column"></param>
        ///<returns>HeaderArea</returns>
        public HeaderArea GetHitArea(Point pt, ref GridColumn column)
        {
            return (GetHitArea(pt, ref column, false));
        }

        private HeaderArea GetHitArea(
            Point pt, ref GridColumn column, bool isMouseDown)
        {
            GridPanel panel = Parent as GridPanel;

            if (panel != null)
            {
                GridElement hitObject = GetHitObject(panel, pt, isMouseDown);

                if (hitObject == this)
                    return (GetHeaderHitArea(panel, pt, ref column, isMouseDown));

                if (hitObject == panel.GroupByRow)
                    return (HeaderArea.InGroupBox);
            }

            return (HeaderArea.NoWhere);
        }

        #region GetHitObject

        private GridElement GetHitObject(
            GridPanel panel, Point pt, bool isMouseDown)
        {
            if (isMouseDown == true && panel.GroupByRow.Visible == true)
            {
                if (panel.GroupByRow.Bounds.Contains(pt) == true)
                    return (panel.GroupByRow);
            }

            if (Capture == true || Bounds.Contains(pt) == true)
                return (this);

            return (null);
        }

        #endregion

        #region GetHeaderHitArea

        private HeaderArea GetHeaderHitArea(GridPanel panel,
            Point pt, ref GridColumn column, bool isMouseDown)
        {
            Rectangle r = GetRowHeaderBounds(panel);

            r.Width += 2;

            if (r.Contains(pt) == true)
            {
                if (Capture == true)
                    return (_HitArea);

                column = null;

                r.Width -= 2;

                if (panel.AllowRowHeaderResize == true)
                {
                    if (pt.X >= r.Right - 5)
                        return (HeaderArea.InRowHeaderResize);
                }

                return (HeaderArea.InRowHeader);
            }

            column = GetHitColumn(panel, pt, isMouseDown);

            return (GetHeaderArea(pt, ref column, panel));
        }

        #region GetHitColumn

        ///<summary>
        /// Gets the associated column at the given location
        ///</summary>
        ///<param name="x"></param>
        ///<param name="y"></param>
        ///<returns></returns>
        public GridColumn GetHitColumn(int x, int y)
        {
            return (GetHitColumn(new Point(x, y)));
        }

        ///<summary>
        /// Gets the associated column at the given point
        ///</summary>
        ///<param name="pt"></param>
        ///<returns></returns>
        public GridColumn GetHitColumn(Point pt)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
                return (GetHitColumn(panel, pt, false));

            return (null);
        }

        private GridColumn GetHitColumn(
            GridPanel panel, Point pt, bool isMouseDown)
        {
            int dy = (isMouseDown == true) ? 50 : 0;

            GridColumnCollection columns = _Columns;
            foreach (GridColumn column in columns)
            {
                if (column.Visible == true)
                {
                    Rectangle r = GetBounds(panel, column);

                    if (pt.X >= r.Left && pt.X < r.Right)
                    {
                        if (pt.Y >= r.Top && pt.Y < r.Bottom + dy)
                            return (column);
                    }
                }
            }

            return (null);
        }

        #endregion

        #region GetHeaderArea

        private HeaderArea GetHeaderArea(
            Point pt, ref GridColumn column, GridPanel panel)
        {
            GridColumn hcolumn = column;

            if (column == null)
                column = panel.Columns.LastVisibleColumn;

            if (column != null)
            {
                Rectangle r = column.BoundsRelative;

                if (IsVFrozen == false)
                    r.Y -= VScrollOffset;

                if (panel.IsSubPanel == true || column.IsHFrozen == false)
                    r.X -= HScrollOffset;

                if (pt.X < r.Left + 3 && pt.X >= r.Left)
                {
                    if (InLeftResizeArea(ref column, panel) == true)
                        return (HeaderArea.InResize);
                }

                if ((pt.X > r.Right - 5) && (pt.X < r.Right + 3))
                {
                    if (InRightResizeArea(ref column, panel) == true)
                        return (HeaderArea.InResize);
                }

                if (column.FilterImageBounds.IsEmpty == false)
                {
                    Rectangle t = GetScrollBounds(
                        panel, column, column.FilterImageBounds);

                    t.Inflate(2, 2);

                    if (t.Contains(pt) == true)
                        return (HeaderArea.InFilterMenu);
                }

                column = hcolumn;

                if (hcolumn != null)
                {
                    if (column.AllowSelection == true)
                        return (HeaderArea.InContent);

                    return (HeaderArea.NoWhere);
                }
            }

            return (HeaderArea.InWhitespace);
        }

        #region InLeftResizeArea

        private bool InLeftResizeArea(ref GridColumn column, GridPanel panel)
        {
            GridColumn pcolumn =
                panel.Columns.GetPrevVisibleColumn(column);

            if (pcolumn != null)
            {
                if (pcolumn.CanResize == true && pcolumn.ResizeMode != ColumnResizeMode.None)
                {
                    column = pcolumn;

                    return (true);
                }
            }

            return (false);
        }

        #endregion

        #region InRightResizeArea

        private bool InRightResizeArea(ref GridColumn column, GridPanel panel)
        {
            GridColumn nColumn =
                panel.Columns.GetNextVisibleColumn(column);

            if (nColumn != null && nColumn.Size.Width < 5)
            {
                if (nColumn.CanResize == true && nColumn.ResizeMode != ColumnResizeMode.None)
                    column = nColumn;
            }

            if (column.CanResize == true && column.ResizeMode != ColumnResizeMode.None)
                return (true);

            return (false);
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region UpdateImageBounds

        internal void UpdateImageBounds(GridPanel panel, GridColumn column)
        {
            Rectangle r = BoundsRelative;

            r.X = column.BoundsRelative.X;
            r.Width = column.BoundsRelative.Width;

            if (column.AdjustSortImagePositionByMargin == true)
            {
                VisualStyle style = GetEffectiveStyle(column);

                r.X += (style.BorderThickness.Left + style.Margin.Left);
                r.Width -= (style.BorderThickness.Horizontal + style.Margin.Horizontal);

                r.Y += (style.BorderThickness.Top + style.Margin.Top);
                r.Height -= (style.BorderThickness.Vertical + style.Margin.Vertical);
            }

            r.Inflate(-3, -3);
            r.Width--;

            Alignment sortAlignment = (_SortImageAlignment == Alignment.NotSet)
                ? Alignment.TopCenter : _SortImageAlignment;

            Alignment filterAlignment = (_FilterImageAlignment == Alignment.NotSet)
                ? Alignment.MiddleLeft : _FilterImageAlignment;

            Rectangle rt = r;

            column.SortImageBounds = Rectangle.Empty;
            column.FilterImageBounds = Rectangle.Empty;

            Image sortImage = GetColumnSortImage(panel, column);

            if (sortImage != null)
            {
                Rectangle t = GetImageBounds(sortImage, sortAlignment, ref r);

                column.SortImageBounds = t;
            }

            Image filterImage = GetColumnFilterImage(column);

            if (filterImage != null)
            {
                Rectangle t;

                if (sortImage != null && filterAlignment == sortAlignment)
                {
                    t = GetImageBounds(filterImage, filterAlignment, ref r);

                    switch (filterAlignment)
                    {
                        case Alignment.TopCenter:
                        case Alignment.MiddleCenter:
                        case Alignment.BottomCenter:
                            t.X = rt.X + (r.Width - (sortImage.Width +
                                filterImage.Width)) / 2 + sortImage.Width;
                            break;

                        case Alignment.TopLeft:
                        case Alignment.MiddleLeft:
                        case Alignment.BottomLeft:
                            break;
                    }
                }
                else
                {
                    t = GetImageBounds(filterImage, filterAlignment, ref rt);

                    r.Intersect(rt);
                }

                column.FilterImageBounds = t;
            }
        }

        #endregion

        #region GetRowHeaderBounds

        private Rectangle GetRowHeaderBounds(GridPanel panel)
        {
            if (panel.ShowRowHeaders == true)
            {
                Rectangle r = Bounds;
                r.Width = panel.RowHeaderWidth;

                Rectangle p = panel.PanelBounds;

                if (r.Y < p.Y)
                {
                    r.Height -= (p.Y - r.Y + 1);
                    r.Y = p.Y + 1;
                }

                return (r);
            }

            return (Rectangle.Empty);
        }

        #endregion

        #region Style support routines

        #region GetSizingStyle

        private ColumnHeaderVisualStyle GetSizingStyle(GridPanel panel, GridColumn column)
        {
            StyleType style = panel.GetSizingStyle();

            if (style == StyleType.NotSet)
                style = StyleType.Default;

            return (GetEffectiveStyle(column, style));
        }

        #endregion

        #region GetStyleState

        private StyleState GetStyleState(GridColumn column, bool selected)
        {
            StyleState state = StyleState.Default;

            if (_Reordering == true && column == _MouseDownHitColumn)
                state |= StyleState.MouseOver;

            else if (_HitColumn == column)
                state |= StyleState.MouseOver;

            if (selected == true)
                state |= StyleState.Selected;

            if (column.IsReadOnly == true)
                state |= StyleState.ReadOnly;

            return (state);
        }

        #endregion

        #region GetEffectiveStyle

        ///<summary>
        /// GetEffectiveStyle
        ///</summary>
        ///<param name="column"></param>
        ///<returns></returns>
        public ColumnHeaderVisualStyle GetEffectiveStyle(GridColumn column)
        {
            StyleState styleState = GetStyleState(column, column.IsSelected);
            ColumnHeaderVisualStyle style = GetEffectiveStyle(column, styleState);

            return (style);
        }

        internal ColumnHeaderVisualStyle
            GetEffectiveStyle(GridColumn column, StyleType sizingStyle)
        {
            return (column.GetHeaderStyle(sizingStyle));
        }

        ///<summary>
        ///Gets the EffectiveStyle for the given StyleState
        ///</summary>
        ///<param name="column"></param>
        ///<param name="cellState"></param>
        ///<returns></returns>
        public ColumnHeaderVisualStyle
            GetEffectiveStyle(GridColumn column, StyleState cellState)
        {
            StyleType type;

            switch (cellState)
            {
                case StyleState.MouseOver:
                    type = StyleType.MouseOver;
                    break;

                case StyleState.Selected:
                    type = StyleType.Selected;
                    break;

                case StyleState.Selected | StyleState.MouseOver:
                    type = StyleType.SelectedMouseOver;
                    break;

                case StyleState.ReadOnly:
                    type = StyleType.ReadOnly;
                    break;

                case StyleState.ReadOnly | StyleState.MouseOver:
                    type = StyleType.ReadOnlyMouseOver;
                    break;

                case StyleState.ReadOnly | StyleState.Selected:
                    type = StyleType.ReadOnlySelected;
                    break;

                case StyleState.ReadOnly | StyleState.MouseOver | StyleState.Selected:
                    type = StyleType.ReadOnlySelectedMouseOver;
                    break;

                default:
                    type = StyleType.Default;
                    break;
           }

            return (column.GetHeaderStyle(type));
        }

        #endregion

        #region GetEffectiveRowHeaderStyle

        internal ColumnHeaderRowVisualStyle GetEffectiveRowHeaderStyle()
        {
            return (GetEffectiveRowHeaderStyleEx(GetRowHeaderState()));
        }

        internal ColumnHeaderRowVisualStyle
            GetEffectiveRowHeaderStyleEx(StyleState rowState)
        {
            ValidateRowHeaderStyle();

            switch (rowState)
            {
                case StyleState.MouseOver:
                    return (GetRowHeaderStyle(StyleType.MouseOver));

                case StyleState.Selected:
                    return (GetRowHeaderStyle(StyleType.Selected));

                case StyleState.Selected | StyleState.MouseOver:
                    return (GetRowHeaderStyle(StyleType.SelectedMouseOver));

                case StyleState.ReadOnly:
                    return (GetRowHeaderStyle(StyleType.ReadOnly));

                case StyleState.ReadOnly | StyleState.MouseOver:
                    return (GetRowHeaderStyle(StyleType.ReadOnlyMouseOver));

                case StyleState.ReadOnly | StyleState.Selected:
                    return (GetRowHeaderStyle(StyleType.ReadOnlySelected));

                case StyleState.ReadOnly | StyleState.MouseOver | StyleState.Selected:
                    return (GetRowHeaderStyle(StyleType.ReadOnlySelectedMouseOver));

                default:
                    return (GetRowHeaderStyle(StyleType.Default));
            }
        }

        #region ValidateRowHeaderStyle

        private void ValidateRowHeaderStyle()
        {
            if (_StyleUpdateCount != SuperGrid.StyleUpdateCount)
            {
                _EffectiveRowHeaderStyles = null;

                _StyleUpdateCount = SuperGrid.StyleUpdateCount;
            }
        }

        #endregion

        #region GetRowHeaderState

        private StyleState GetRowHeaderState()
        {
            StyleState rowState = StyleState.Default;

            if (_HitArea == HeaderArea.InRowHeader)
                rowState |= StyleState.MouseOver;

            GridPanel panel = GridPanel;

            if (panel != null)
            {
                if (panel.ReadOnly == true)
                    rowState |= StyleState.ReadOnly;

                if (panel == SuperGrid.ActiveGrid)
                    rowState |= StyleState.Selected;
            }

            return (rowState);
        }

        #endregion

        #region GetRowHeaderStyle

        private ColumnHeaderRowVisualStyle GetRowHeaderStyle(StyleType e)
        {
            if (_EffectiveRowHeaderStyles == null)
                _EffectiveRowHeaderStyles = new ColumnHeaderRowVisualStyles();

            if (_EffectiveRowHeaderStyles.IsValid(e) == false)
            {
                ColumnHeaderRowVisualStyle style = new ColumnHeaderRowVisualStyle();

                StyleType[] css = style.GetApplyStyleTypes(e);

                if (css != null)
                {
                    foreach (StyleType cs in css)
                    {
                        style.ApplyStyle(SuperGrid.BaseVisualStyles.ColumnHeaderRowStyles[cs]);
                        style.ApplyStyle(SuperGrid.DefaultVisualStyles.ColumnHeaderRowStyles[cs]);
                        style.ApplyStyle(GridPanel.DefaultVisualStyles.ColumnHeaderRowStyles[cs]);
                    }
                }

                SuperGrid.DoGetColumnHeaderRowHeaderStyleEvent(this, e, ref style);

                if (style.RowHeader.Background == null || style.RowHeader.Background.IsEmpty == true)
                    style.RowHeader.Background = new Background(Color.WhiteSmoke);

                _EffectiveRowHeaderStyles[e] = style;
            }

            return (_EffectiveRowHeaderStyles[e]);
        }

        #endregion

        #endregion

        #endregion

        #region GetBounds

        internal Rectangle GetBounds(GridPanel panel, GridColumn column)
        {
            Rectangle r = BoundsRelative;

            r.X = column.BoundsRelative.X;
            r.Width = column.BoundsRelative.Width;

            return (GetScrollBounds(panel, column, r));
        }

        #endregion

        #region GetScrollBounds

        internal Rectangle GetScrollBounds(
            GridPanel panel, GridColumn column, Rectangle r)
        {
            if (r.IsEmpty == false)
            {
                if (IsVFrozen == false)
                    r.Y -= VScrollOffset;

                if (panel.IsSubPanel == true || column.IsHFrozen == false)
                    r.X -= HScrollOffset;
            }

            return (r);
        }

        #endregion

        #region GetContentBounds

        internal Rectangle GetContentBounds(
            GridPanel panel, GridColumn column, Rectangle r)
        {
            if (_ShowHeaderImages == true)
            {
                ColumnHeaderVisualStyle style = GetEffectiveStyle(column);

                Image image = style.GetImage(panel);

                if (image != null)
                {
                    Size size = style.GetImageSize(image);

                    if (style.IsOverlayImage != true)
                    {
                        switch (style.ImageAlignment)
                        {
                            case Alignment.TopCenter:
                                r.Y += size.Height;
                                r.Height -= size.Height;
                                break;

                            case Alignment.MiddleCenter:
                                break;

                            case Alignment.BottomCenter:
                                r.Height -= size.Height;
                                break;

                            case Alignment.TopRight:
                            case Alignment.MiddleRight:
                            case Alignment.BottomRight:
                                r.Width -= size.Width;
                                break;

                            default:
                                r.X += size.Width;
                                r.Width -= size.Width;
                                break;
                        }
                    }
                }
            }

            return (r);
        }

        #endregion

        #region GetAdjustedBounds

        private Rectangle GetAdjustedBounds(GridColumn column, Rectangle r)
        {
            VisualStyle style = GetEffectiveStyle(column);

            r.X += (style.BorderThickness.Left + style.Margin.Left + style.Padding.Left);
            r.Width -= (style.BorderThickness.Horizontal + style.Margin.Horizontal + style.Padding.Horizontal);

            r.Y += (style.BorderThickness.Top + style.Margin.Top + style.Padding.Top);
            r.Height -= (style.BorderThickness.Vertical + style.Margin.Vertical + style.Padding.Vertical);

            return (r);
        }

        #endregion

        #region GetImageBounds

        /// <summary>
        /// Gets the bounding rectangle for the
        /// given column header image
        /// </summary>
        /// <param name="column"></param>
        /// <returns>Bounding rectangle</returns>
        public Rectangle GetImageBounds(GridColumn column)
        {
            if (_ShowHeaderImages == true)
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    ColumnHeaderVisualStyle style = GetEffectiveStyle(column);

                    Image image = style.GetImage(panel);

                    if (image != null)
                    {
                        Rectangle bounds = GetBounds(panel, column);

                        Rectangle t = GetAdjustedBounds(column,
                            GetContentBounds(panel, column, bounds));

                        return (style.GetImageBounds(image, t));
                    }
                }
            }

            return (Rectangle.Empty);
        }

        #endregion

        #region InvalidateHeader

        internal void InvalidateHeader(GridPanel panel, GridColumn column)
        {
            Rectangle bounds = BoundsRelative;

            if (panel.IsVFrozen == false)
                bounds.Y -= VScrollOffset;

            bounds.X = column.BoundsRelative.X;
            bounds.Width = column.BoundsRelative.Width;

            if (panel.IsSubPanel == true || column.IsHFrozen == false)
                bounds.X -= HScrollOffset;

            InvalidateRender(bounds);
        }

        #endregion

        #region InvalidateRowHeader

        internal void InvalidateRowHeader()
        {
            InvalidateRender(RowHeaderBounds);
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

            _MouseDownHitColumn = null;

            base.CancelCapture();
        }

        #endregion
    }

    #region enums

    #region HeaderArea

    ///<summary>
    /// HeaderArea
    ///</summary>
    public enum HeaderArea
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
        /// InResize
        ///</summary>
        InResize,

        ///<summary>
        /// InRowHeader
        ///</summary>
        InRowHeader,

        ///<summary>
        /// InRowHeaderResize
        ///</summary>
        InRowHeaderResize,

        ///<summary>
        /// InWhitespace
        ///</summary>
        InWhitespace,

        ///<summary>
        /// InMarkup
        ///</summary>
        InMarkup,

        ///<summary>
        /// InFilterMenu
        ///</summary>
        InFilterMenu,

        ///<summary>
        /// InGroupBox
        ///</summary>
        InGroupBox,
    }

    #endregion

    #region ImageVisibility

    ///<summary>
    /// ImageVisibility
    ///</summary>
    public enum ImageVisibility
    {
        ///<summary>
        /// NotSet
        ///</summary>
        NotSet = -1,

        ///<summary>
        /// Auto
        ///</summary>
        Auto,

        ///<summary>
        /// Always
        ///</summary>
        Always,

        ///<summary>
        /// Never
        ///</summary>
        Never,
    }

    #endregion

    #endregion
}