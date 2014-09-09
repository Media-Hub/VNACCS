using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid.Style;

namespace DevComponents.DotNetBar.SuperGrid
{
    /// <summary>
    /// Represents a Grid Filter row
    /// </summary>
    [TypeConverter(typeof(BlankExpandableObjectConverter))]
    public class GridFilter : GridElement
    {
        #region Constants

        private const int ImageCacheSize = 8;

        #endregion

        #region Private variables

        private int _RowHeight;

        private FilterRowVisualStyles _EffectiveRowHeaderStyles;
        private int _StyleUpdateCount;

        private HeaderArea _HitArea;
        private GridColumn _HitColumn;

        private HeaderArea _MouseDownHitArea;
        private GridColumn _MouseDownHitColumn;

        private Image _FilterImage;
        private int _FilterImageIndex = -1;
        private Image[] _FilterCacheImages = new Image[ImageCacheSize];

        private bool _Visible;
        private bool _ShowPanelFilterExpr;
        private bool _ShowToolTips = true;

        #endregion

        #region Internal properties

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

        #region FilterImage

        /// <summary>
        /// Gets or sets the default image to display
        /// in the filter row header.
        /// </summary>
        [DefaultValue(null), Category("Filtering")]
        [Description("Indicates the default image to display in the filter row header.")]
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

        internal Image GetFilterImage(GridPanel panel)
        {
            if (_FilterImage != null)
                return (_FilterImage);

            if (_FilterImageIndex >= 0)
            {
                ImageList imageList = GridPanel.ImageList;

                if (imageList != null && _FilterImageIndex < imageList.Images.Count)
                    return (imageList.Images[_FilterImageIndex]);
            }

            return (GetFilterImageEx(panel));
        }

        #region GetFilterImageEx

        private Image GetFilterImageEx(GridPanel panel)
        {
            StyleState state = GetFilterImageState(panel);

            Image cacheImage = _FilterCacheImages[(int)state];

            if (cacheImage == null)
            {
                Image image = new Bitmap(13, 12);

                using (Graphics g = Graphics.FromImage(image))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    using (GraphicsPath path = new GraphicsPath())
                    {
                        Point[] pts =
                        {
                            new Point(0, 0),
                            new Point(12, 0),
                            new Point(12, 1),
                            new Point(7, 6),
                            new Point(7, 11),
                            new Point(5, 9),
                            new Point(5, 6),
                            new Point(0, 1),
                            new Point(0, 0),
                        };

                        path.AddLines(pts);

                        FilterRowVisualStyle style = GetEffectiveRowStyle(state);

                        Rectangle r = new Rectangle(1, 1, 11, 10);

                        Background back = (style.FilterBackground != null && style.FilterBackground.IsEmpty == false)
                            ? style.FilterBackground : new Background(Color.White);

                        using (Brush br = back.GetBrush(r))
                        {
                            g.FillPath(br, path);
                        }

                        Color color = style.FilterBorderColor;

                        if (color.IsEmpty)
                            color = Color.DimGray;

                        using (Pen pen = new Pen(color))
                            g.DrawPath(pen, path);
                    }
                }

                _FilterCacheImages[(int)state] = image;

                cacheImage = image;
            }

            return (cacheImage);
        }

        #endregion

        #endregion

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

        #region ShowPanelFilterExpr

        ///<summary>
        /// Gets or sets whether filter
        /// expressions are displayed in the panel area
        ///</summary>
        [DefaultValue(false), Category("Appearance")]
        [Description("Indicates whether filter expressions are displayed in the panel area.")]
        public bool ShowPanelFilterExpr
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

        #region ShowToolTips

        /// <summary>
        /// Gets or sets whether tooltips are shown for FilterPanel expressions
        /// </summary>
        [DefaultValue(true), Category("Appearance")]
        [Description("Indicates whether tooltips are shown for FilterPanel expressions.")]
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

        #region Visible

        [DefaultValue(false), Category("Appearance")]
        [Description("Indicates whether the item is visible")]
        public override bool Visible
        {
            get { return (_Visible); }

            set
            {
                if (_Visible != value)
                {
                    _Visible = value;

                    if (_Visible == false)
                    {
                        if (SuperGrid.ActiveFilterPanel != null)
                            SuperGrid.ActiveFilterPanel.EndEdit();
                    }

                    OnPropertyChangedEx("Visible", VisualChangeType.Layout);
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

            sizeNeeded.Width = panel.ColumnHeader.Size.Width;

            sizeNeeded.Height = (_RowHeight > 0)
                ? _RowHeight : MeasureHeaderHeight(panel);

            Size = sizeNeeded;
        }

        #region MeasureHeader

        private int MeasureHeaderHeight(GridPanel panel)
        {
            int maxHeight = 0;

            foreach (GridColumn col in panel.Columns)
            {
                if (col.Visible == true)
                {
                    Size size = FilterPanel.GetPreferredSize(this, col);

                    FilterColumnHeaderVisualStyle style = GetEffectiveStyle(col);

                    int n = size.Height + style.Margin.Vertical;

                    if (n > maxHeight)
                        maxHeight = n;
                }
            }

            if (maxHeight > 0)
                maxHeight += 4;

            return (maxHeight);
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

                FilterRowVisualStyle style = GetEffectiveRowStyle();
                GridPanelVisualStyle pstyle = panel.GetEffectiveStyle();

                if (SuperGrid.DoPreRenderFilterRowEvent(g,
                    this, null, RenderParts.Background, bounds) == false)
                {
                    RenderRowBackground(g, style, bounds);

                    SuperGrid.DoPostRenderFilterRowEvent(g,
                        this, null, RenderParts.Background, bounds);
                }

                RenderColumnBorder(g, panel, bounds);

                if (panel.IsSubPanel == true ||
                    panel.FrozenColumnCount <= 0)
                {
                    RenderAllColumns(renderInfo, panel);
                }
                else
                {
                    RenderScrollableColumns(renderInfo, panel);
                    RenderFrozenColumns(renderInfo, panel);
                }

                RenderRowHeader(g, panel, style, pstyle);
            }
        }

        #region RenderRowBackground

        private void RenderRowBackground(
            Graphics g, FilterRowVisualStyle style, Rectangle bounds)
        {
            using (Brush br = style.WhiteSpaceBackground.GetBrush(bounds))
                g.FillRectangle(br, bounds);
        }

        #endregion

        #region RenderAllColumns

        private void RenderAllColumns(
            GridRenderInfo renderInfo, GridPanel panel)
        {
            Graphics g = renderInfo.Graphics;
            GridColumnCollection columns = panel.Columns;

            if (columns != null)
            {
                int[] map = columns.DisplayIndexMap;
                for (int i = 0; i < map.Length; i++)
                {
                    int index = map[i];

                    GridColumn column = columns[index];

                    if (column.Visible == true)
                    {
                        Rectangle r = GetBounds(panel, column);

                        if (r.IntersectsWith(renderInfo.ClipRectangle) == true)
                            RenderColumnHeader(g, panel, column, r);
                    }
                }
            }
        }

        #endregion

        #region RenderScrollableColumns

        private void RenderScrollableColumns(
            GridRenderInfo renderInfo, GridPanel panel)
        {
            Graphics g = renderInfo.Graphics;
            GridColumnCollection columns = panel.Columns;

            int[] map = columns.DisplayIndexMap;
            for (int i = 0; i < map.Length; i++)
            {
                int index = map[i];

                GridColumn column = columns[index];

                if (column.Visible == true && column.IsHFrozen == false)
                {
                    Rectangle r = GetBounds(panel, column);

                    if (r.IntersectsWith(renderInfo.ClipRectangle) == true)
                        RenderColumnHeader(g, panel, column, r);
                }
            }
        }

        #endregion

        #region RenderFrozenColumns

        private void RenderFrozenColumns(
            GridRenderInfo renderInfo, GridPanel panel)
        {
            Graphics g = renderInfo.Graphics;
            GridColumnCollection columns = panel.Columns;

            int[] map = columns.DisplayIndexMap;
            for (int i = 0; i < map.Length; i++)
            {
                int index = map[i];

                GridColumn column = columns[index];

                if (column.Visible == true)
                {
                    if (column.IsHFrozen == false)
                        break;
                
                    Rectangle r = GetBounds(panel, column);

                    if (r.IntersectsWith(renderInfo.ClipRectangle) == true)
                        RenderColumnHeader(g, panel, column, r);
                }
            }
        }

        #endregion

        #region RenderRowHeader

        private void RenderRowHeader(Graphics g,
            GridPanel panel, FilterRowVisualStyle style, GridPanelVisualStyle pstyle)
        {
            Rectangle r = GetRowHeaderBounds(panel);

            if (r.Width > 0 && r.Height > 0)
            {
                if (SuperGrid.DoPreRenderFilterRowEvent(g,
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

                    RenderFilterIndicator(g, panel, r);

                    SuperGrid.DoPostRenderFilterRowEvent(g,
                        this, null, RenderParts.RowHeader, r);
                }
            }
        }

        #region RenderFilterIndicator

        private void RenderFilterIndicator(Graphics g, GridPanel panel, Rectangle r)
        {
            Image image = GetFilterImage(panel);

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

        #endregion

        #region RenderColumnHeader

        private void RenderColumnHeader(Graphics g,
            GridPanel panel, GridColumn column, Rectangle bounds)
        {
            FilterColumnHeaderVisualStyle style = GetEffectiveStyle(column);

            if (bounds.Width > 0 && bounds.Height > 0)
            {
                if (SuperGrid.DoPreRenderFilterRowEvent(g,
                    this, column, RenderParts.Background, bounds) == false)
                {
                    RenderBackground(g, column, style, bounds);

                    SuperGrid.DoPostRenderFilterRowEvent(g,
                        this, column, RenderParts.Background, bounds);
                }

                RenderColumnBorder(g, panel, bounds);

                Rectangle r = bounds;

                r.X += (style.Margin.Left + 1);
                r.Width -= (style.Margin.Horizontal + 2);

                r.Y += style.Margin.Top;
                r.Height -= style.Margin.Vertical;

                if (r.Width > 0 && r.Height > 0)
                {
                    if (SuperGrid.ActiveFilterPanel == null ||
                        SuperGrid.ActiveFilterPanel.GridColumn != column)
                    {
                        if (SuperGrid.DoPreRenderFilterRowEvent(g,
                            this, column, RenderParts.Content, r) == false)
                        {
                            RenderFilterContent(g, column, style, r);

                            SuperGrid.DoPostRenderFilterRowEvent(g,
                                this, column, RenderParts.Content, r);
                        }
                    }
                }

                RenderFocusRect(g, column, bounds);
            }
        }

        #region RenderBackground

        internal void RenderBackground(Graphics g,
            GridColumn column, FilterColumnHeaderVisualStyle style, Rectangle r)
        {
            if (style == null)
                style = GetEffectiveStyle(column);

            if (column.FilterError == false || style.ErrorBackground == null)
            {
                using (Brush br = style.Background.GetBrush(r))
                    g.FillRectangle(br, r);
            }
            else
            {
                using (Brush br = style.ErrorBackground.GetBrush(r))
                    g.FillRectangle(br, r);
            }
        }

        #endregion

        #region RenderColumnBorder

        private void RenderColumnBorder(
            Graphics g, GridPanel panel, Rectangle bounds)
        {
            Rectangle r = bounds;
            Rectangle p = panel.PanelBounds;

            GridPanelVisualStyle pstyle = panel.GetEffectiveStyle();

            if (r.Y > 0 && r.Y <= p.Y)
            {
                r.Height -= (p.Y - r.Y + 1);
                r.Y = p.Y + 1;
            }

            using (Pen pen = new Pen(pstyle.HeaderLineColor))
                g.DrawRectangle(pen, r.X - 1, r.Top - 1, r.Width, r.Height);
        }

        #endregion

        #region RenderFilterContent

        private void RenderFilterContent(Graphics g,
            GridColumn column, FilterColumnHeaderVisualStyle style, Rectangle bounds)
        {
            switch (column.GetFilterPanelType())
            {
                case FilterEditType.CheckBox:
                    FilterPanel.RenderCheckBox(g, this, column, bounds);
                    break;

                default:
                    if (column.FilterValue != null || column.CanShowFilterExpr == true)
                    {
                        object value = column.FilterDisplayValue;

                        if (value != null)
                        {
                            string s = value is DateTime
                                           ? ((DateTime)value).ToString("d")
                                           : value.ToString();

                            RenderContent(g, column, s, style, bounds);
                        }
                    }
                    else if (column.FilterExpr != null)
                    {
                        string s = column.SuperGrid.FilterCustomString;

                        RenderContent(g, column, s, style, bounds);
                    }
                    break;
            }
        }

        #region RenderContent

        private void RenderContent(Graphics g, GridColumn column,
            string s, FilterColumnHeaderVisualStyle style, Rectangle bounds)
        {
            eTextFormat tf = style.GetTextFormatFlags();

            if (string.IsNullOrEmpty(s) == false)
            {
                Rectangle r = bounds;
                r.Width--;
                r.Height -= 2;

                Color color = column.FilterError == false
                                  ? style.TextColor
                                  : style.ErrorTextColor;

                TextDrawing.DrawString(g, s, style.Font, color, r, tf);
            }
        }

        #endregion

        #endregion

        #region RenderFocusRect

        private void RenderFocusRect(Graphics g, GridColumn column, Rectangle r)
        {
            if (SuperGrid.ActiveFilterPanel != null)
            {
                if (SuperGrid.ActiveFilterPanel.GridColumn == column)
                {
                    r.Width--;
                    r.Height--;
                    ControlPaint.DrawFocusRectangle(g, r);
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region GetRowHeaderBounds

        private Rectangle GetRowHeaderBounds(GridPanel panel)
        {
            if (panel.ShowRowHeaders == true)
            {
                Rectangle r = Bounds;
                r.Width = panel.RowHeaderWidth;

                Rectangle p = panel.PanelBounds;

                if (r.Y <= p.Y)
                {
                    r.Height -= (p.Y - r.Y + 1);
                    r.Y = p.Y + 1;
                }

                return (r);
            }

            return (Rectangle.Empty);
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
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                if (_HitArea == HeaderArea.InRowHeader || _HitArea == HeaderArea.InFilterMenu)
                    InvalidateRowHeader();

                _HitArea = HeaderArea.NoWhere;
                _HitColumn = null;

                SuperGrid.ToolTipText = "";
            }

            base.InternalMouseLeave(e);
        }

        #endregion

        #region InternalMouseMove

        private HeaderArea _LastHitArea;
        private GridColumn _LastHitColumn;

        internal override void InternalMouseMove(MouseEventArgs e)
        {
            GridPanel panel = Parent as GridPanel;

            if (panel != null)
            {
                _LastHitArea = _HitArea;
                _LastHitColumn = _HitColumn;

                _HitArea = GetHitArea(e.Location, ref _HitColumn, false);

                switch (_HitArea)
                {
                    case HeaderArea.InFilterMenu:
                        SuperGrid.GridCursor = Cursors.Hand;
                        UpdateToolTip(panel, panel.FilterExpr);
                        break;

                    case HeaderArea.InContent:
                        SuperGrid.GridCursor = Cursors.Default;

                        if (_HitColumn != null)
                        {
                            if (_HitColumn.IsFilteringEnabled == true)
                                UpdateToolTip(panel, _HitColumn.FilterExpr);
                        }
                        break;

                    default:
                        SuperGrid.GridCursor = Cursors.Default;
                        SuperGrid.ToolTipText = "";
                        break;
                }

                if (_LastHitArea != _HitArea)
                    InvalidateRowHeader();

                base.InternalMouseMove(e);
            }
        }

        #region UpdateToolTip

        private void UpdateToolTip(GridPanel panel, string toolTip)
        {
            if (_ShowToolTips == true)
            {
                if (_HitArea != _LastHitArea || _HitColumn != _LastHitColumn)
                    panel.SetHeaderTooltip(this, _HitColumn, _HitArea, toolTip);
            }
        }

        #endregion

        #endregion

        #region InternalMouseDown

        internal override void InternalMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            GridPanel panel = GridPanel;

            if (panel != null)
            {
                _MouseDownHitArea = _HitArea;
                _MouseDownHitColumn = _HitColumn;

                switch (_HitArea)
                {
                    case HeaderArea.InContent:
                        GridColumn column = _HitColumn;

                        if (column != null)
                        {
                            if (_HitColumn.IsFilteringEnabled == true)
                            {
                                if (panel.SetActiveRow(null, false) == true)
                                {
                                    FilterPanel fp = FilterPanel.GetFilterPanel(column);

                                    if (fp != null)
                                        fp.BeginEdit();
                                }
                            }
                        }
                        break;

                    case HeaderArea.InFilterMenu:
                        if (panel.SuperGrid.DoFilterBeginEditEvent(panel, null) == false)
                            LaunchCustomDialog(panel, panel.FilterExpr);
                        break;
                }
            }
        }

        #region LaunchCustomDialog

        private void LaunchCustomDialog(GridPanel panel, string filterExpr)
        {
            Form form = panel.SuperGrid.FindForm();

            if (panel.SuperGrid.FilterUseExtendedCustomDialog == true)
            {
                CustomFilterEx cf = new CustomFilterEx(panel, null, filterExpr);

                cf.Text = panel.Name;

                DialogResult dr = cf.ShowDialog(form);

                if (dr == DialogResult.OK)
                    panel.FilterExpr = cf.FilterExpr;
            }
            else
            {
                CustomFilter cf = new CustomFilter(panel, null, filterExpr);

                cf.Text = panel.Name;

                DialogResult dr = cf.ShowDialog(form);

                if (dr == DialogResult.OK)
                    panel.FilterExpr = cf.FilterExpr;
            }

            SuperGrid.Focus();
        }

        #endregion

        #endregion

        #region InternalMouseUp

        internal override void InternalMouseUp(MouseEventArgs e)
        {
            GridPanel panel = GridPanel;

            if (panel != null && _HitArea == _MouseDownHitArea)
            {
                switch (_HitArea)
                {
                    case HeaderArea.InContent:
                        if (_HitColumn != null && _MouseDownHitColumn == _HitColumn)
                            SuperGrid.DoFilterHeaderClickEvent(panel, _HitColumn, e);
                        break;

                    case HeaderArea.InRowHeader:
                        SuperGrid.DoFilterRowHeaderClickEvent(panel);
                        break;

                    case HeaderArea.InFilterMenu:
                        SuperGrid.DoFilterRowHeaderClickEvent(panel);
                        break;
                }
            }

            base.InternalMouseUp(e);
        }

        #endregion

        #endregion

        #region GetHitArea

        HeaderArea GetHitArea(Point pt, ref GridColumn column, bool isMouseDown)
        {
            column = null;

            if (AllowSelection == false)
                return (HeaderArea.NoWhere);

            GridPanel panel = Parent as GridPanel;

            if (panel != null)
            {
                Rectangle r = GetRowHeaderBounds(panel);

                r.Width += 2;

                if (r.Contains(pt) == true)
                {
                    if (panel.EnableFiltering == true && panel.EnableRowFiltering == true)
                        return (HeaderArea.InFilterMenu);

                    return (HeaderArea.InRowHeader);
                }

                column = GetHitColumn(panel, pt, isMouseDown);
            }

            return ((column != null) ?
                HeaderArea.InContent : HeaderArea.NoWhere);
        }

        #endregion

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

            GridColumnCollection columns = panel.Columns;
            foreach (GridColumn column in columns)
            {
                if (column.Visible == true)
                {
                    Rectangle r = GetBounds(panel, column);

                    if (pt.X >= r.Left && pt.X < r.Right)
                    {
                        if (pt.Y >= r.Top - dy && pt.Y < r.Bottom + dy)
                            return (column);
                    }
                }
            }

            return (null);
        }

        #endregion

        #region Style support routines

        #region GetStyleState

        private StyleState GetStyleState(GridColumn column, bool selected)
        {
            StyleState state = StyleState.Default;

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
        public FilterColumnHeaderVisualStyle GetEffectiveStyle(GridColumn column)
        {
            StyleState styleState = GetStyleState(column, column.IsSelected);
            FilterColumnHeaderVisualStyle style = GetEffectiveStyle(column, styleState);

            return (style);
        }

        internal FilterColumnHeaderVisualStyle
            GetEffectiveStyle(GridColumn column, StyleType sizingStyle)
        {
            ValidateStyle(column);

            return (GetStyle(column, sizingStyle));
        }

        private FilterColumnHeaderVisualStyle
            GetEffectiveStyle(GridColumn column, StyleState cellState)
        {
            ValidateStyle(column);

            switch (cellState)
            {
                case StyleState.MouseOver:
                    return (GetStyle(column, StyleType.MouseOver));

                case StyleState.Selected:
                    return (GetStyle(column, StyleType.Selected));

                case StyleState.Selected | StyleState.MouseOver:
                    return (GetStyle(column, StyleType.SelectedMouseOver));

                case StyleState.ReadOnly:
                    return (GetStyle(column, StyleType.ReadOnly));

                case StyleState.ReadOnly | StyleState.MouseOver:
                    return (GetStyle(column, StyleType.ReadOnlyMouseOver));

                case StyleState.ReadOnly | StyleState.Selected:
                    return (GetStyle(column, StyleType.ReadOnlySelected));

                case StyleState.ReadOnly | StyleState.MouseOver | StyleState.Selected:
                    return (GetStyle(column, StyleType.ReadOnlySelectedMouseOver));

                default:
                    return (GetStyle(column, StyleType.Default));
            }
        }

        #region ValidateStyle

        private void ValidateStyle(GridColumn column)
        {
            if (column.StyleUpdateCount != SuperGrid.StyleUpdateCount)
            {
                ClearEffectiveStyles(column);

                column.StyleUpdateCount = SuperGrid.StyleUpdateCount;
            }
        }

        #endregion

        #region ClearEffectiveStyles

        private void ClearEffectiveStyles(GridColumn column)
        {
            column.EffectiveFilterStyles = new FilterColumnHeaderVisualStyles();

            _EffectiveRowHeaderStyles = new FilterRowVisualStyles();
        }

        #endregion

        #region GetStyle

        private FilterColumnHeaderVisualStyle GetStyle(GridColumn column, StyleType e)
        {
            if (column.EffectiveFilterStyles == null)
                column.EffectiveFilterStyles = new FilterColumnHeaderVisualStyles();

            if (column.EffectiveFilterStyles.IsValid(e) == false)
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
                        style.ApplyStyle(column.FilterRowStyles[cs]);
                    }
                }

                SuperGrid.DoGetFilterColumnHeaderStyleEvent(column, e, ref style);

                if (style.Background == null || style.Background.IsEmpty == true)
                    style.Background = new Background(Color.White);

                if (style.Font == null)
                {
                    Font font = SystemFonts.DefaultFont;
                    style.Font = font;
                }

                column.EffectiveFilterStyles[e] = style;
            }

            return (column.EffectiveFilterStyles[e]);
        }

        #endregion

        #endregion

        #region GetEffectiveRowStyle

        internal FilterRowVisualStyle GetEffectiveRowStyle()
        {
            StyleState rowState = GetRowHeaderState();

            return (GetEffectiveRowStyle(rowState));
        }

        internal FilterRowVisualStyle GetEffectiveRowStyle(StyleState rowState)
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
            if (_EffectiveRowHeaderStyles == null ||
                (_StyleUpdateCount != SuperGrid.StyleUpdateCount))
            {
                _EffectiveRowHeaderStyles = new FilterRowVisualStyles();

                _StyleUpdateCount = SuperGrid.StyleUpdateCount;
            }
        }

        #endregion

        #region GetRowHeaderState

        private StyleState GetRowHeaderState()
        {
            StyleState rowState = StyleState.Default;

            GridPanel panel = GridPanel;

            if (panel != null)
            {
                if (panel.ReadOnly == true)
                    rowState |= StyleState.ReadOnly;

                if (panel == SuperGrid.ActiveGrid)
                    rowState |= StyleState.Selected;

                Rectangle r = GetRowHeaderBounds(panel);

                if (r.Contains(Control.MousePosition) == true)
                    rowState |= StyleState.MouseOver;
            }

            return (rowState);
        }

        #endregion

        #region GetRowHeaderStyle

        private FilterRowVisualStyle GetRowHeaderStyle(StyleType e)
        {
            if (_EffectiveRowHeaderStyles.IsValid(e) == false)
            {
                FilterRowVisualStyle style = new FilterRowVisualStyle();

                StyleType[] css = style.GetApplyStyleTypes(e);

                if (css != null)
                {
                    foreach (StyleType cs in css)
                    {
                        style.ApplyStyle(SuperGrid.BaseVisualStyles.FilterRowStyles[cs]);
                        style.ApplyStyle(SuperGrid.DefaultVisualStyles.FilterRowStyles[cs]);
                        style.ApplyStyle(GridPanel.DefaultVisualStyles.FilterRowStyles[cs]);
                    }
                }

                SuperGrid.DoGetFilterRowStyleEvent(this, e, ref style);

                if (style.RowHeader.Background == null || style.RowHeader.Background.IsEmpty == true)
                    style.RowHeader.Background = new Background(Color.WhiteSmoke);

                _EffectiveRowHeaderStyles[e] = style;
            }

            return (_EffectiveRowHeaderStyles[e]);
        }

        #endregion

        #endregion

        #region GetFilterImageState

        private StyleState GetFilterImageState(GridPanel panel)
        {
            StyleState state = GetRowHeaderState();

            state &= ~(StyleState.MouseOver | StyleState.Selected);

            if (_HitArea == HeaderArea.InFilterMenu)
                state |= StyleState.MouseOver;

            if (string.IsNullOrEmpty(panel.FilterExpr) == false)
                state |= StyleState.Selected;

            return (state);
        }

        #endregion

        #endregion

        #region GetBounds

        internal Rectangle GetBounds(GridPanel panel, GridColumn column)
        {
            Rectangle r = BoundsRelative;

            r.X = column.BoundsRelative.X;
            r.Width = column.BoundsRelative.Width;

            if (r.X == 0)
            {
                r.X++;
                r.Width--;
            }

            if (IsVFrozen == false)
                r.Y -= VScrollOffset;

            if (panel.IsSubPanel == true || column.IsHFrozen == false)
                r.X -= HScrollOffset;

            return (r);
        }

        #endregion

        #region InvalidateRowHeader

        internal void InvalidateRowHeader()
        {
            InvalidateRender(RowHeaderBounds);
        }

        #endregion

        #region ActivateFilterEdit

        ///<summary>
        ///Activates the filter edit control for the given column
        ///</summary>
        ///<param name="column"></param>
        public void ActivateFilterEdit(GridColumn column)
        {
            GridPanel panel = GridPanel;

            if (panel.SetActiveRow(null, false) == true)
            {
                FilterPanel fp = FilterPanel.GetFilterPanel(column);

                if (fp != null)
                    fp.BeginEdit();
            }
        }

        #endregion
    }
}
