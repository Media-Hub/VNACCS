using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid.Style;
using DevComponents.SuperGrid.TextMarkup;

namespace DevComponents.DotNetBar.SuperGrid
{
    /// <summary>
    /// Defines the grid GridGroupBy Row
    /// </summary>
    public class GridGroupByRow : GridTextRow
    {
        #region Constants

        const string DefaultWatermarkText =
            "Drag a column header here to group by that column";

        const int MarkerWidth = 13;
        const int MarkerHeight = 7;

        const int MarkerHPad = 10;
        const int MarkerVPad = 12;

        const int TextPad = 3;
        const int TextHPad = 6;
        const int TextVPad = 6;

        const int BoxSpacing = 8;
        const int DefaultMinRowHeight = 35;

        #endregion

        #region Private variables

        private bool _Visible;
        private bool _UseColumnHeaderColors = true;
        private bool _AllowUserSort = true;
        private bool _RemoveGroupOnDoubleClick = true;

        private GroupBoxLayout _GroupBoxLayout = GroupBoxLayout.Hierarchical;
        private GroupBoxStyle _GroupBoxStyle = GroupBoxStyle.Rectangular;

        private List<GridGroupBox> _GroupBoxes;
        private GroupBoxEffects _GroupBoxEffects = GroupBoxEffects.NotSet;

        private bool _Dragging;
        private Image _InsertMarker;

        private GridGroupBox _HitGroupBox;
        private GridGroupBox _LastHitGroupBox;
        private GridGroupBox _MouseDownHitBox;

        private HeaderArea _HitArea;
        private HeaderArea _LastHitArea;
        private GridColumn _HitColumn;

        private FloatWindow _GroupBoxWindow;
        private GridGroupBox _AfterGroupBox;
        private Rectangle _InsertRect;

        private FilterPopup _FilterMenu;
        private ImageVisibility _FilterImageVisibility = ImageVisibility.NotSet;

        private string _WatermarkText;

        private int _MaxGroupBoxWidth = 200;
        private int _CornerRadius = 5;

        #endregion

        #region Constructors

        ///<summary>
        /// GridHeader
        ///</summary>
        public GridGroupByRow()
            : this(null)
        {
        }

        ///<summary>
        /// GridHeader
        ///</summary>
        ///<param name="text"></param>
        public GridGroupByRow(string text)
            : base(text)
        {
        }

        #endregion

        #region Public properties

        #region AllowUserSort

        ///<summary>
        /// Gets or sets whether the user can change the group
        /// sort direction by clicking on the associated GroupBox
        ///</summary>
        [DefaultValue(true), Category("Behavior")]
        [Description("Indicates whether the user can change the group sort direction by clicking on the associated GroupBox.")]
        public bool AllowUserSort
        {
            get { return (_AllowUserSort); }

            set
            {
                if (_AllowUserSort != value)
                {
                    _AllowUserSort = value;

                    OnPropertyChangedEx("AllowUserSort");
                }
            }
        }

        #endregion

        #region CornerRadius

        ///<summary>
        /// Gets or sets the corner radius to
        /// use when GroupBoxStyle is RoundedRectangular
        ///</summary>
        [DefaultValue(5), Category("Behavior")]
        [Description("Indicates the corner radius to use when GroupBoxStyle is RoundedRectangular.")]
        public int CornerRadius
        {
            get { return (_CornerRadius); }

            set
            {
                if (_CornerRadius != value)
                {
                    _CornerRadius = value;

                    OnPropertyChangedEx("CornerRadius", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region FilterImageVisibility

        /// <summary>
        /// Gets or sets the visibility of the header filter image
        /// </summary>
        [DefaultValue(ImageVisibility.NotSet), Category("Appearance")]
        [Description("Indicates the visibility of the filter image.")]
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

        #region GroupBoxEffects

        /// <summary>
        /// Gets or sets the default for how columns interact with the GroupBox
        /// </summary>
        [DefaultValue(GroupBoxEffects.NotSet), Category("Grouping")]
        [Description("Indicates the default for how columns interact with the GroupBox.")]
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

        #region GroupBoxLayout

        /// <summary>
        /// Gets or sets the group box layout (Flat, Hierarchical) 
        /// </summary>
        [DefaultValue(GroupBoxLayout.Hierarchical), Category("Appearance")]
        [Description("Indicates the group box layout (Flat, Hierarchical)")]
        public GroupBoxLayout GroupBoxLayout
        {
            get { return (_GroupBoxLayout); }

            set
            {
                if (_GroupBoxLayout != value)
                {
                    _GroupBoxLayout = value;

                    OnPropertyChangedEx("GroupBoxLayout", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region GroupBoxStyle

        /// <summary>
        /// Gets or sets the group box Style (Rectangular, RoundedRect) 
        /// </summary>
        [DefaultValue(GroupBoxStyle.Rectangular), Category("Appearance")]
        [Description("Indicates the group box Style (Rectangular, RoundedRect)")]
        public GroupBoxStyle GroupBoxStyle
        {
            get { return (_GroupBoxStyle); }

            set
            {
                if (_GroupBoxStyle != value)
                {
                    _GroupBoxStyle = value;

                    OnPropertyChangedEx("GroupBoxStyle", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region IsEmpty

        /// <summary>
        /// Gets whether the item is empty
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool IsEmpty
        {
            get { return (false); }
        }

        #endregion

        #region MaxGroupBoxWidth

        /// <summary>
        /// Gets or sets the maximum GroupBox width, in pixels 
        /// </summary>
        [DefaultValue(200), Category("Appearance")]
        [Description("Indicates the maximum GroupBox width, in pixels.")]
        public int MaxGroupBoxWidth
        {
            get { return (_MaxGroupBoxWidth); }

            set
            {
                if (_MaxGroupBoxWidth != value)
                {
                    _MaxGroupBoxWidth = value;

                    OnPropertyChangedEx("MaxGroupBoxWidth", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region RemoveGroupOnDoubleClick

        ///<summary>
        /// Gets or sets whether the grouping column is removed
        /// when the user double clicks on the associated GroupBox
        ///</summary>
        [DefaultValue(true), Category("Behavior")]
        [Description("Indicates whether the grouping column is removed when the user double clicks on the associated GroupBox.")]
        public bool RemoveGroupOnDoubleClick
        {
            get { return (_RemoveGroupOnDoubleClick); }

            set
            {
                if (_RemoveGroupOnDoubleClick != value)
                {
                    _RemoveGroupOnDoubleClick = value;

                    OnPropertyChangedEx("RemoveGroupOnDoubleClick");
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

                    OnPropertyChangedEx("Visible", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region UseColumnHeaderColors

        ///<summary>
        /// Gets or sets whether Column Header color definitions are used
        ///</summary>
        [DefaultValue(true), Category("Appearance")]
        [Description("Indicates whether Column Header color definitions are used")]
        public bool UseColumnHeaderColors
        {
            get { return (_UseColumnHeaderColors); }

            set
            {
                if (_UseColumnHeaderColors != value)
                {
                    _UseColumnHeaderColors = value;

                    OnPropertyChangedEx("UseColumnHeaderColors", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region WatermarkText

        /// <summary>
        /// Gets or sets the Watermark text to use when no Grouping is active 
        /// </summary>
        [DefaultValue(null), Category("Appearance")]
        [Description("Indicates the Watermark text to use when no Grouping is active")]
        public string WatermarkText
        {
            get { return (_WatermarkText); }

            set
            {
                if (_WatermarkText != value)
                {
                    _WatermarkText = value;

                    OnPropertyChangedEx("WatermarkText", VisualChangeType.Render);
                }
            }
        }

        #region GetWatermarkText

        private string GetWatermarkText()
        {
            return (_WatermarkText ?? DefaultWatermarkText);
        }

        #endregion

        #endregion

        #endregion

        #region Private properties

        #region InsertMarker

        private Image InsertMarker
        {
            get
            {
                if (_InsertMarker == null)
                    _InsertMarker = CreateInsertMarker();

                return (_InsertMarker);
            }

            set
            {
                if (_InsertMarker != null)
                    _InsertMarker.Dispose();

                _InsertMarker = value;
            }
        }

        #region CreateInsertMarker

        private Image CreateInsertMarker()
        {
            GroupByVisualStyle style = (GroupByVisualStyle)GetEffectiveStyle();

            Image image = new Bitmap(MarkerWidth, MarkerHeight);

            using (Graphics g = Graphics.FromImage(image))
            {
                Point[] pts =
                    {
                        new Point(0, 0),
                        new Point(MarkerWidth - 1, 0),
                        new Point(MarkerHeight - 1, MarkerHeight - 1),
                        new Point(0, 0),
                    };

                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddLines(pts);

                    Rectangle r = new Rectangle(0, 0, MarkerWidth, MarkerHeight);

                    using (Brush br = style.InsertMarkerBackground.GetBrush(r))
                        g.FillPath(br, path);

                    using (Pen pen = new Pen(style.InsertMarkerBorderColor))
                        g.DrawPath(pen, path);
                }
            }

            return (image);
        }

        #endregion

        #endregion

        #endregion

        #region MeasureRow

        protected override Size MeasureRow(
            GridLayoutInfo layoutInfo, GridLayoutStateInfo stateInfo, Size constraintSize)
        {
            Size sizeNeeded = base.MeasureRow(layoutInfo, stateInfo, constraintSize);

            GridPanel panel = stateInfo.GridPanel;

            int height = GetGroupByHeight(panel);

            if (RowHeight == 0)
            {
                if (height < DefaultMinRowHeight)
                    height = DefaultMinRowHeight;
            }

            if (height > sizeNeeded.Height)
                sizeNeeded.Height = height;

            return (sizeNeeded);
        }

        #region GetGroupByHeight

        private int GetGroupByHeight(GridPanel panel)
        {
            if (_GroupBoxes == null)
                _GroupBoxes = new List<GridGroupBox>();
            else
                _GroupBoxes.Clear();

            GridGroupBox box = new GridGroupBox(this, null);
            _GroupBoxes.Add(box);

            int rowHeight = (GroupBoxLayout == GroupBoxLayout.Hierarchical)
                                ? GetHierarchicalHeight(panel)
                                : GetFlatHeight(panel);

            rowHeight += (MarkerVPad * 2);

            TextRowVisualStyle tstyle = GetEffectiveStyle();

            rowHeight += tstyle.BorderThickness.Vertical +
                tstyle.Padding.Vertical + tstyle.Margin.Vertical;

            return (rowHeight);
        }

        #region GetHierarchicalHeight

        private int GetHierarchicalHeight(GridPanel panel)
        {
            int rowHeight = 0;
            int lastHeight = 0;

            foreach (GridColumn column in panel.GroupColumns)
            {
                GridGroupBox box = new GridGroupBox(this, column);

                box.GroupBoxStyle = GroupBoxStyle;
                box.CornerRadius = CornerRadius;

                _GroupBoxes.Add(box);

                Size size = GetGroupBoxSize(box);

                if (rowHeight == 0)
                {
                    rowHeight = size.Height;
                }
                else
                {
                    if (size.Height > lastHeight)
                        rowHeight += (size.Height - lastHeight / 2);
                    else
                        rowHeight += (size.Height / 2);
                }

                lastHeight = size.Height;
            }

            return (rowHeight);
        }

        #endregion

        #region GetFlatHeight

        private int GetFlatHeight(GridPanel panel)
        {
            int rowHeight = 0;

            foreach (GridColumn column in panel.GroupColumns)
            {
                GridGroupBox box = new GridGroupBox(this, column);

                box.GroupBoxStyle = GroupBoxStyle;
                box.CornerRadius = CornerRadius;

                _GroupBoxes.Add(box);

                Size size = GetGroupBoxSize(box);

                if (size.Height > rowHeight)
                    rowHeight = size.Height;
            }

            return (rowHeight);
        }

        #endregion

        #region GetGroupBoxSize

        private Size GetGroupBoxSize(GridGroupBox box)
        {
            Size size = new Size();

            size.Width = box.Column.HeaderTextSize.Width + TextHPad;
            size.Height = box.Column.HeaderTextSize.Height + TextVPad;

            box.ContentSize = size;

            SuperGrid.DoConfigureGroupBoxEvent(this, box);

            size = box.ContentSize;

            size.Width += box.Padding.Horizontal;
            size.Height += box.Padding.Vertical;

            if (size.Width > _MaxGroupBoxWidth)
                size.Width = _MaxGroupBoxWidth;

            size.Width += MarkerHPad + TextHPad;

            if (size.Width < 20)
                size.Width = 20;

            if (size.Height < 15)
                size.Height = 15;

            box.RelativeBounds = new Rectangle(Point.Empty, size);

            return (size);
        }

        #endregion

        #endregion

        #endregion

        #region ArrangeRow

        protected override Size ArrangeRow(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, Rectangle layoutBounds)
        {
            UpdateGroupBoxes(stateInfo.GridPanel, layoutBounds);

            return (base.ArrangeRow(layoutInfo, stateInfo, layoutBounds));
        }

        #region UpdateGroupBoxes

        private void UpdateGroupBoxes(GridPanel panel, Rectangle r)
        {
            TextRowVisualStyle tstyle = GetEffectiveStyle();

            r = GetAdjustedBounds(tstyle, r);
            r.Size = Size.Empty;

            if (CanShowRowHeader(panel) == true)
                r.X += panel.RowHeaderWidth;

            r.X += (MarkerWidth - MarkerHPad);
            r.Y += (MarkerVPad - MarkerHeight + TextPad);

            GridGroupBox box = _GroupBoxes[0];
            box.RelativeBounds = r;

            int lastHeight = MarkerHeight;
            int midline = Bounds.Y + Bounds.Height / 2 - 1;

            for (int i = 1; i < _GroupBoxes.Count; i++)
            {
                box = _GroupBoxes[i];

                r.X += (r.Width + BoxSpacing);
                r.Size = box.RelativeBounds.Size;

                if (GroupBoxLayout == GroupBoxLayout.Hierarchical)
                {
                    r.Y += (r.Height > lastHeight)
                        ? lastHeight / 2 : lastHeight - r.Height / 2;
                }
                else
                {
                    r.Y = midline - (r.Height / 2);
                }

                UpdateGroupBoxRects(panel, box, ref r);

                lastHeight = r.Height;
            }

            _AfterGroupBox = null;
        }

        #region UpdateGroupBoxRects

        private void UpdateGroupBoxRects(
            GridPanel panel, GridGroupBox box, ref Rectangle r)
        {
            Rectangle t = r;

            Rectangle ts = GetSortImageBounds(panel, box.Column, t);
            Rectangle tf = GetFilterImageBounds(panel, box.Column, t);

            if (ts.Width > 0)
                r.Width += (ts.Width + TextPad);

            if (tf.Width > 0)
                r.Width += (tf.Width + TextPad);

            r.Width += 3;

            box.RelativeBounds = r;

            if (tf.IsEmpty == false && ts.IsEmpty == false)
            {
                if (ts.X == tf.X)
                {
                    if (tf.X < r.X + r.Width / 2)
                        tf.X += ts.Width + TextPad;
                    else
                        ts.X += tf.Width + TextPad;
                }
                else
                {
                    if (tf.X < r.X + r.Width / 2)
                        ts.X = r.Right - (ts.Width + TextPad);
                    else
                        tf.X = r.Right - (tf.Width + TextPad);
                }
            }
            else
            {
                if (ts.Width > 0 && ts.X > r.X + r.Width / 2)
                    ts.X = r.Right - (ts.Width + TextPad);

                if (tf.Width > 0 && tf.X > r.X + r.Width / 2)
                    tf.X = r.Right - (tf.Width + TextPad);
            }

            box.FilterImageRelBounds = tf;
            box.SortImageRelBounds = ts;
        }

        #region GetSortImageBounds

        private Rectangle GetSortImageBounds(
            GridPanel panel, GridColumn column, Rectangle r)
        {
            Image sortImage = panel.ColumnHeader.GetColumnSortImage(panel, column);

            if (sortImage != null)
            {
                Alignment alignment = (panel.ColumnHeader.SortImageAlignment == Alignment.NotSet)
                    ? Alignment.MiddleLeft : panel.ColumnHeader.SortImageAlignment;

                return (GetImageBoundsEx(sortImage, alignment, r));
            }

            return (Rectangle.Empty);
        }

        #endregion

        #region GetFilterImageBounds

        private Rectangle GetFilterImageBounds(
            GridPanel panel, GridColumn column, Rectangle r)
        {
            Image filterImage = null;

            switch (_FilterImageVisibility)
            {
                case ImageVisibility.Auto:
                case ImageVisibility.Always:
                    if (column.IsFilteringEnabled == true)
                        filterImage = panel.ColumnHeader.GetFilterImage(column);
                    break;

                case ImageVisibility.NotSet:
                    filterImage = panel.ColumnHeader.GetColumnFilterImage(column);
                    break;
            }

            if (filterImage != null)
            {
                Alignment alignment = panel.ColumnHeader.FilterImageAlignment;

                if (alignment == Alignment.NotSet)
                    alignment = Alignment.MiddleLeft;

                return (GetImageBoundsEx(filterImage, alignment, r));
            }

            return (Rectangle.Empty);
        }

        #endregion

        #region GetImageBoundsEx

        private Rectangle GetImageBoundsEx(
            Image image, Alignment alignment, Rectangle r)
        {
            Rectangle t = r;
            t.Size = image.Size;

            t.X += 6;
            t.Y += (r.Height - t.Height) / 2;

            switch (alignment)
            {
                case Alignment.TopLeft:
                case Alignment.MiddleLeft:
                case Alignment.BottomLeft:
                    break;

                default:
                    t.X = r.Right + 3;
                    break;
            }

            return (t);
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region RenderRow

        protected override void RenderRow(
            GridRenderInfo renderInfo, GridPanel panel, Rectangle r)
        {
            base.RenderRow(renderInfo, panel, r);

            Graphics g = renderInfo.Graphics;

            if (CanShowRowHeader(panel) == true)
            {
                r.X += panel.RowHeaderWidth;
                r.Width -= panel.RowHeaderWidth;
            }

            Region oldClip = g.Clip;

            try
            {
                g.SetClip(r, CombineMode.Intersect);

                if (_GroupBoxes.Count > 1)
                {
                    GridPanelVisualStyle pstyle = panel.GetEffectiveStyle();
                    GridGroupBox lastBox = null;

                    foreach (GridGroupBox box in _GroupBoxes)
                    {
                        if (box.Column != null)
                        {
                            RenderGroupBox(panel, box, lastBox, g, pstyle);
                            lastBox = box;
                        }
                    }
                }
                else
                {
                    RenderWaterMarkText(panel, g);
                }
            }
            finally
            {
                g.Clip = oldClip;
            }

            if (_InsertRect.IsEmpty == false)
                g.DrawImageUnscaled(InsertMarker, _InsertRect);
        }

        #region RenderGroupBox

        private void RenderGroupBox(GridPanel panel,
            GridGroupBox box, GridGroupBox lastBox, Graphics g, GridPanelVisualStyle pstyle)
        {
            StyleType type = GetGroupByStyleType(box);

            ColumnHeaderVisualStyle cstyle = panel.ColumnHeader.GetEffectiveStyle(box.Column, type);
            GroupByVisualStyle style = GetGroupByEffectiveStyle(box);

            Rectangle r = box.Bounds;

            if (r.Width > 0 && r.Height > 0)
            {
                if (SuperGrid.DoPreRenderGroupBoxEvent(g,
                    this, box, RenderParts.Background, r) == false)
                {
                    int radius = GetCornerRadius(box);

                    if (radius > 0 && box.GroupBoxStyle == GroupBoxStyle.RoundedRectangular)
                        RenderRoundedBox(g, style, cstyle, pstyle, lastBox, box, radius);
                    else
                        RenderRectBox(g, style, cstyle, pstyle, lastBox, box);

                    SuperGrid.DoPostRenderGroupBoxEvent(g,
                        this, box, RenderParts.Background, r);
                }

                Rectangle t = r;

                RenderSortImage(g, panel, box, ref t);
                RenderFilterImage(g, panel, box, ref t);

                int n = box.Padding.Left + TextHPad;

                t.X += n;
                t.Width -= n;

                if (t.Width - box.ContentSize.Width > 0)
                    t.Width = box.ContentSize.Width;

                if (SuperGrid.DoPreRenderGroupBoxEvent(g,
                    this, box, RenderParts.Content, t) == false)
                {
                    Rectangle t2 = t;

                    t.Y += box.Padding.Top;
                    t.Height = box.Column.HeaderTextSize.Height + 6;

                    RenderText(g, box.Column, style, cstyle, t);

                    SuperGrid.DoPostRenderGroupBoxEvent(g,
                        this, box, RenderParts.Content, t2);
                }
            }
        }

        #region GetCornerRadius

        internal int GetCornerRadius(GridGroupBox box)
        {
            int maxRadius = Math.Min(
                box.RelativeBounds.Width, box.RelativeBounds.Height) / 2;

            int radius = Math.Min(box.CornerRadius, maxRadius);

            return (radius > 0 ? radius : 0);
        }

        #endregion

        #region RenderRoundedBox

        private void RenderRoundedBox(Graphics g, GroupByVisualStyle style,
            ColumnHeaderVisualStyle cstyle, GridPanelVisualStyle pstyle,
            GridGroupBox leftBox, GridGroupBox box, int radius)
        {
            Rectangle r = box.Bounds;

            using (GraphicsPath path = GetRoundedPath(r, radius))
            {
                SmoothingMode sm = g.SmoothingMode;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                if (_UseColumnHeaderColors == true)
                {
                    using (Brush br = cstyle.Background.GetBrush(r))
                        g.FillPath(br, path);

                    using (Pen pen = new Pen(pstyle.HeaderLineColor))
                        g.DrawPath(pen, path);
                }
                else
                {
                    using (Brush br = style.GroupBoxBackground.GetBrush(r))
                        g.FillPath(br, path);

                    using (Pen pen = new Pen(style.GroupBoxBorderColor))
                        g.DrawPath(pen, path);
                }

                if (leftBox != null)
                {
                    Region oldClip = g.Clip;
                    Region newClip = new Region(Bounds);

                    newClip.Exclude(path);

                    g.SetClip(newClip, CombineMode.Intersect);

                    try
                    {
                        if (SuperGrid.DoPreRenderGroupBoxConnectorEvent(g, this, leftBox, box) == false)
                        {
                            RenderBoxConnector(g, style, leftBox.Bounds, box.Bounds, true);

                            SuperGrid.DoPostRenderGroupBoxConnectorEvent(g, this, leftBox, box);
                        }
                    }
                    finally
                    {
                        g.Clip = oldClip;
                    }
                }

                g.SmoothingMode = sm;
            }
        }

        #region GetRoundedPath

        private GraphicsPath GetRoundedPath(Rectangle r, int radius)
        {
            int diameter = radius << 1;

            Rectangle t = r;
            t.Size = new Size(diameter, diameter);

            GraphicsPath path = new GraphicsPath();
            path.AddArc(t, 180, 90);

            t.X = r.Right - diameter;
            path.AddArc(t, 270, 90);

            t.Y = r.Bottom - diameter;
            path.AddArc(t, 0, 90);

            t.X = r.X;
            path.AddArc(t, 90, 90);

            path.CloseAllFigures();

            return (path);
        }

        #endregion

        #endregion

        #region RenderRectBox

        private void RenderRectBox(Graphics g, GroupByVisualStyle style,
            ColumnHeaderVisualStyle cstyle, GridPanelVisualStyle pstyle,
            GridGroupBox leftBox, GridGroupBox box)
        {
            Rectangle r = box.Bounds;

            if (_UseColumnHeaderColors == true)
            {
                using (Brush br = cstyle.Background.GetBrush(r))
                    g.FillRectangle(br, r);

                using (Pen pen = new Pen(pstyle.HeaderLineColor))
                    g.DrawRectangle(pen, r);
            }
            else
            {
                using (Brush br = style.GroupBoxBackground.GetBrush(r))
                    g.FillRectangle(br, r);

                using (Pen pen = new Pen(style.GroupBoxBorderColor))
                    g.DrawRectangle(pen, r);
            }

            if (leftBox != null)
            {
                if (SuperGrid.DoPreRenderGroupBoxConnectorEvent(g, this, leftBox, box) == false)
                {
                    RenderBoxConnector(g, style, leftBox.Bounds, r, false);

                    SuperGrid.DoPostRenderGroupBoxConnectorEvent(g, this, leftBox, box);
                }
            }
        }

        #endregion

        #region RenderBoxConnector

        private void RenderBoxConnector(Graphics g,
            GroupByVisualStyle style, Rectangle l, Rectangle r, bool extend)
        {
            using (Pen pen = new Pen(style.GroupBoxConnectorColor))
            {
                if (GroupBoxLayout == GroupBoxLayout.Hierarchical)
                {
                    Point pt1 = new Point(l.X + l.Width / 2, l.Bottom + 1);
                    Point pt2 = new Point(pt1.X, l.Bottom + (r.Bottom - l.Bottom) / 2);
                    Point pt3 = new Point(r.X + (extend ? 10 : -1), pt2.Y);

                    g.DrawLines(pen, new Point[] { pt1, pt2, pt3 });
                }
                else
                {
                    Point pt1 = new Point(l.Right + 1, l.Y + l.Height / 2 + 1);
                    Point pt2 = new Point(r.X - 1, pt1.Y);

                    g.DrawLine(pen, pt1, pt2);
                }
            }
        }

        #endregion

        #endregion

        #region Render Filter/Sort Image

        #region RenderSortImage

        private void RenderSortImage(
            Graphics g, GridPanel panel, GridGroupBox box, ref Rectangle t)
        {
            GridColumn column = box.Column;
            Rectangle bounds = box.SortImageBounds;

            if (bounds.IsEmpty == false)
            {                
                Image sortImage =
                    panel.ColumnHeader.GetColumnSortImage(panel, column);

                RenderImage(g, sortImage, bounds, ref t);
            }
        }

        #endregion

        #region RenderFilterImage

        private void RenderFilterImage(
            Graphics g, GridPanel panel, GridGroupBox box, ref Rectangle t)
        {
            GridColumn column = box.Column;
            Rectangle bounds = box.FilterImageBounds;

            if (bounds.IsEmpty == false)
            {
                if (CanShowFilterImage(panel, box) == true)
                {
                    bool inFilter;
                    StyleState state = GetFilterImageState(box, out inFilter);

                    Image filterImage =
                        panel.ColumnHeader.GetFilterImage(column, state, inFilter);

                    RenderImage(g, filterImage, bounds, ref t);
                }
            }
        }

        #region CanShowFilterImage

        private bool CanShowFilterImage(GridPanel panel, GridGroupBox box)
        {
            switch (_FilterImageVisibility)
            {
                case ImageVisibility.Auto:
                    if (box.Column.IsFilteringEnabled == true)
                    {
                        if (string.IsNullOrEmpty(box.Column.FilterExpr) == false)
                            return (true);

                        return (_Dragging == false && box.IsEqualTo(_HitGroupBox) == true);
                    }
                    break;

                case ImageVisibility.Always:
                    if (box.Column.IsFilteringEnabled == true)
                        return (true);
                    break;

                case ImageVisibility.NotSet:

                    switch (panel.ColumnHeader.FilterImageVisibility)
                    {
                        case ImageVisibility.Always:
                            return (true);

                        case ImageVisibility.Never:
                            return (false);

                        case ImageVisibility.Auto:
                            if (string.IsNullOrEmpty(box.Column.FilterExpr) == false)
                                return (true);

                            return (_Dragging == false && box.IsEqualTo(_HitGroupBox) == true);
                    }
                    break;
            }

            return (false);
        }

        #endregion

        #region GetFilterImageState

        private StyleState GetFilterImageState(GridGroupBox box, out bool inFilter)
        {
            inFilter = false;

            StyleState state = StyleState.Default;

            if (box.IsEqualTo(_HitGroupBox) == true)
            {
                state = StyleState.MouseOver;

                if (_HitArea == HeaderArea.InFilterMenu)
                    inFilter = true;
            }

            if (string.IsNullOrEmpty(box.Column.FilterExpr) == false)
                state |= StyleState.Selected;

            return (state);
        }

        #endregion

        #endregion

        #region RenderImage

        private void RenderImage(Graphics g,
            Image image, Rectangle r, ref Rectangle t)
        {
            if (image != null)
            {
                g.DrawImageUnscaledAndClipped(image, r);

                if (r.X < t.X + t.Width / 2)
                {
                    int n = t.Right - r.Right;

                    t.X = r.Right;
                    t.Width = n;
                }
                else
                {
                    int n = t.Right - r.Left;

                    t.Width -= n;
                }
            }
        }

        #endregion

        #endregion

        #region RenderText

        private void RenderText(Graphics g, GridColumn column,
            GroupByVisualStyle style, ColumnHeaderVisualStyle cstyle, Rectangle bounds)
        {
            string s = column.GetHeaderText();

            if (s != null)
            {
                if (column.HeaderTextMarkup != null)
                {
                    RenderTextMarkup(g, column.HeaderTextMarkup, cstyle, bounds);
                }
                else
                {
                    if (_UseColumnHeaderColors == true)
                    {
                        eTextFormat tf = cstyle.GetTextFormatFlags();

                        TextDrawing.DrawString(g, s,
                            cstyle.Font, cstyle.TextColor, bounds, tf);
                    }
                    else
                    {
                        eTextFormat tf = style.GetTextFormatFlags();

                        TextDrawing.DrawString(g, s,
                            cstyle.Font, style.GroupBoxTextColor, bounds, tf);
                    }
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

        #region RenderWaterMarkText

        private void RenderWaterMarkText(GridPanel panel, Graphics g)
        {
            if (AllowSelection == true)
            {
                string s = GetWatermarkText();

                if (string.IsNullOrEmpty(s) == false)
                {
                    GroupByVisualStyle style = (GroupByVisualStyle)GetEffectiveStyle();
                    Rectangle t = GetAdjustedBounds(style, Bounds);

                    if (CanShowRowHeader(panel) == true)
                    {
                        t.X += panel.RowHeaderWidth;
                        t.Width -= panel.RowHeaderWidth;
                    }

                    TextDrawing.DrawString(g, s, style.WatermarkFont, style.WatermarkTextColor,
                        t, eTextFormat.VerticalCenter | eTextFormat.Left);
                }
            }
        }

        #endregion

        #endregion

        #region GroupBoxWindowPaint

        void GroupBoxWindowPaint(object sender, PaintEventArgs e)
        {
            GridPanel panel = GridPanel;
            GridPanelVisualStyle pstyle = panel.GetEffectiveStyle();

            RenderGroupBox(panel,
                (GridGroupBox)_GroupBoxWindow.Tag, null, e.Graphics, pstyle);
        }

        #endregion

        #region Mouse support

        #region InternalMouseLeave

        internal override void InternalMouseLeave(EventArgs e)
        {
            base.InternalMouseLeave(e);

            if (_LockedColumn == false)
                _HitGroupBox = null;
        }

        #endregion

        #region InternalMouseMove

        internal override void InternalMouseMove(MouseEventArgs e)
        {
            base.InternalMouseMove(e);

            SuperGrid.GridCursor = Cursors.Default;

            if (AllowSelection == true)
            {
                Point pt = e.Location;

                _LastHitArea = _HitArea;
                _LastHitGroupBox = _HitGroupBox;

                _HitGroupBox = GetGroupBoxAt(pt);
                _HitArea = GetHitArea(pt);

                if (IsMouseDown == true)
                {
                    if (_Dragging == true)
                    {
                        DragContinue(pt);
                    }
                    else if (_MouseDownHitBox != null)
                    {
                        if (Math.Abs(MouseDownPoint.X - pt.X) > 5 ||
                            Math.Abs(MouseDownPoint.Y - pt.Y) > 5)
                        {
                            DragStart(_MouseDownHitBox.Column);
                        }
                    }
                }
                else
                {
                    switch (_HitArea)
                    {
                        case HeaderArea.InFilterMenu:
                            SuperGrid.GridCursor = Cursors.Hand;
                            break;

                        default:
                            SuperGrid.GridCursor = Cursors.Default;
                            break;
                    }
                }

                if (_HitArea != _LastHitArea || _LastHitGroupBox != _HitGroupBox)
                {
                    if (_LastHitGroupBox != null)
                        InvalidateRender(_LastHitGroupBox.Bounds);

                    if (_HitGroupBox != null)
                    {
                        if ((_LastHitGroupBox != _HitGroupBox) ||
                            (_HitArea == HeaderArea.InFilterMenu || _LastHitArea == HeaderArea.InFilterMenu))
                        {
                            InvalidateRender(_HitGroupBox.Bounds);
                        }
                    }
                }
            }
        }

        #endregion

        #region InternalMouseDown

        internal override void InternalMouseDown(MouseEventArgs e)
        {
            base.InternalMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                _MouseDownHitBox = _HitGroupBox;

                if (_MouseDownHitBox != null)
                {
                    Capture = true;

                    if (_HitGroupBox.FilterImageRelBounds.Contains(e.Location) == true)
                        ProcessFilterHit(_MouseDownHitBox);
                    else
                        ProcessContentHit();

                    InvalidateRender(_MouseDownHitBox.Bounds);
                }
            }
        }

        #region ProcessContentHit

        private void ProcessContentHit()
        {
            _Dragging = false;
        }

        #endregion

        #region ProcessFilterHit

        private bool _LockedColumn = true;

        private void ProcessFilterHit(GridGroupBox box)
        {
            if (_FilterMenu != null)
                _FilterMenu.Dispose();

            _LockedColumn = true;

            _FilterMenu = new FilterPopup(GridPanel);

            _FilterMenu.ActivatePopup(box.Column, box.FilterImageRelBounds, ResetState);
        }

        #endregion

        #region ResetState

        internal void ResetState()
        {
            _HitGroupBox = null;
            _LockedColumn = false;
        }

        #endregion

        #endregion

        #region InternalMouseUp

        internal override void InternalMouseUp(MouseEventArgs e)
        {
            base.InternalMouseUp(e);

            GridPanel panel = GridPanel;

            if (_GroupBoxWindow != null)
            {
                GridGroupBox box = (GridGroupBox)_GroupBoxWindow.Tag;

                if (_AfterGroupBox != null)
                {
                    int index = _GroupBoxes.IndexOf(_AfterGroupBox);

                    if (box.Column.GroupBoxEffectsEx == GroupBoxEffects.Move)
                        box.Column.Visible = false;

                    panel.InsertGroup(box.Column, index);
                }
                else if (box.Column.GroupBoxEffectsEx != GroupBoxEffects.None)
                {
                    if (_HitColumn != null)
                    {
                        box.Column.Visible = true;

                        if (_HitColumn != box.Column)
                            ReorderColumn(panel, box.Column);

                        panel.RemoveGroup(box.Column);

                        panel.NeedsSorted = true;
                    }
                }

                DragEnd();
            }
            else
            {
                if (_HitGroupBox != null && _HitGroupBox == _MouseDownHitBox)
                {
                    if (_HitArea == HeaderArea.InContent)
                    {
                        if (panel.IsSortable == true && _AllowUserSort == true)
                        {
                            if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
                            {
                                panel.ColumnHeader.SortColumn(panel, _HitGroupBox.Column);

                                SuperGrid.PostInternalMouseMove();
                            }
                        }
                    }
                }
            }

            if (_MouseDownHitBox != null)
            {
                InvalidateRender(_MouseDownHitBox.Bounds);

                _MouseDownHitBox = null;
            }
        }

        #region ReorderColumn

        private void ReorderColumn(GridPanel panel, GridColumn column)
        {
            GridColumnCollection columns = panel.Columns;

            int[] map = columns.DisplayIndexMap;

            int curIndex = columns.GetDisplayIndex(column);
            int sepIndex = columns.GetDisplayIndex(_HitColumn);

            bool isRight = (_InsertRect.X >
                _HitColumn.Bounds.X + _HitColumn.Bounds.Width / 2);

            if (curIndex > sepIndex && isRight == true && sepIndex < map.Length - 1)
                sepIndex++;

            else if (curIndex < sepIndex && isRight == false && sepIndex > 0)
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

                map[sepIndex] = column.ColumnIndex;

                for (int i = 0; i < map.Length; i++)
                    columns[map[i]].DisplayIndex = i;

                SuperGrid.UpdateStyleCount();
                SuperGrid.PrimaryGrid.InvalidateRender();

                SuperGrid.DoColumnMovedEvent(panel, column);
            }
        }

        #endregion

        #endregion

        #region InternalMouseDoubleClick

        internal override void InternalMouseDoubleClick(MouseEventArgs e)
        {
            base.InternalMouseDoubleClick(e);

            if (_RemoveGroupOnDoubleClick == true)
            {
                GridGroupBox box = _HitGroupBox;

                if (box != null && box == _MouseDownHitBox)
                {
                    GridPanel panel = GridPanel;

                    if (panel.IsSortable == false || _AllowUserSort == false ||
                        (Control.ModifierKeys & Keys.Control) == Keys.Control)
                    {
                        panel.RemoveGroup(box.Column);

                        box.Column.Visible = true;
                    }
                }
            }
        }

        #endregion

        #endregion

        #region GetHitArea

        public override HeaderArea GetHitArea(Point pt)
        {
            if (_HitGroupBox != null)
            {
                if (_HitGroupBox.FilterImageRelBounds.Contains(pt))
                    return (HeaderArea.InFilterMenu);

                return (HeaderArea.InContent);
            }

            HeaderArea area = base.GetHitArea(pt);

            if (area != HeaderArea.InContent)
                return (area);

            return (HeaderArea.InWhitespace);
        }

        #endregion

        #region GetAdjustedBounds

        private Rectangle GetAdjustedBounds(TextRowVisualStyle style, Rectangle r)
        {
            r.X += (style.BorderThickness.Left + style.Margin.Left + style.Padding.Left);
            r.Width -= (style.BorderThickness.Horizontal + style.Margin.Horizontal + style.Padding.Horizontal);

            r.Y += (style.BorderThickness.Top + style.Margin.Top + style.Padding.Top);
            r.Height -= (style.BorderThickness.Vertical + style.Margin.Vertical + style.Padding.Vertical);

            return (r);
        }

        #endregion

        #region GetScrollBounds

        internal Rectangle GetScrollBounds(Rectangle r)
        {
            if (IsVFrozen == false)
                r.Y -= VScrollOffset;

            r.X -= HScrollOffset;

            return (r);
        }

        #endregion

        #region GetGroupBoxAt

        ///<summary>
        /// Gets the GroupBox containing the given point
        ///</summary>
        ///<param name="pt"></param>
        ///<returns>GroupBox, or null</returns>
        public GridGroupBox GetGroupBoxAt(Point pt)
        {
            foreach (GridGroupBox box in _GroupBoxes)
            {
                if (box.Bounds.Contains(pt) == true)
                    return (box);
            }

            return (null);
        }

        #endregion

        #region FindGroupPartition

        private GridGroupBox FindGroupPartition(Point pt)
        {
            for (int i = _GroupBoxes.Count - 1; i >= 0; i--)
            {
                GridGroupBox box = _GroupBoxes[i];

                if (pt.X > box.Bounds.X + box.Bounds.Width / 2)
                    return (box);
            }

            return (_GroupBoxes[0]);
        }

        #endregion

        #region Drag support

        #region DragStart

        internal bool DragStart(GridColumn column)
        {
            GridPanel panel = GridPanel;

            if (panel.VirtualMode == true)
                return (false);

            if (AllowSelection == true && panel.ColumnHeader.Visible == true &&
                column.GroupBoxEffectsEx != GroupBoxEffects.None)
            {
                Capture = true;

                _Dragging = true;

                _GroupBoxWindow = new FloatWindow();
                _GroupBoxWindow.Opacity = .5;
                _GroupBoxWindow.Owner = SuperGrid.FindForm();
                _GroupBoxWindow.Paint += GroupBoxWindowPaint;

                GridGroupBox box = new GridGroupBox(this, column);

                box.GroupBoxStyle = GroupBoxStyle;
                box.CornerRadius = CornerRadius;

                Size size = GetGroupBoxSize(box);
                Rectangle r = new Rectangle(Point.Empty, size);

                UpdateGroupBoxRects(GridPanel, box, ref r);

                box.RelativeBounds = r;
                box.IsDragBox = true;

                _GroupBoxWindow.Size = r.Size;

                int radius = GetCornerRadius(box);

                if (radius > 0 && box.GroupBoxStyle == GroupBoxStyle.RoundedRectangular)
                {
                    using (GraphicsPath path = GetRoundedPath(r, radius))
                    {
                        GraphicsPath cpath = (GraphicsPath)path.Clone();

                        using (Pen pen = new Pen(Color.Black, 2))
                            cpath.Widen(pen);

                        Region rgn = new Region(path);
                        rgn.Union(cpath);

                        _GroupBoxWindow.Region = rgn;
                    }
                }

                _GroupBoxWindow.Tag = box;

                return (true);
            }

            return (false);
        }

        #endregion

        #region DragContinue

        private void DragContinue(Point pt)
        {
            GridGroupBox box = _GroupBoxWindow.Tag as GridGroupBox;

            if (box != null)
            {
                Point ptStart = pt;

                Size size = box.RelativeBounds.Size;
                size.Width++;
                size.Height++;

                pt.X -= (size.Width / 2);
                pt.Y -= (size.Height / 2);

                pt = SuperGrid.PointToScreen(pt);

                Rectangle r = new Rectangle(pt, size);

                _GroupBoxWindow.Bounds = r;

                if (_GroupBoxWindow.Visible == false)
                    _GroupBoxWindow.Show();

                Rectangle t = GetInsertRect(ptStart);

                if (t.Equals(_InsertRect) == false)
                {
                    InvalidateRender(_InsertRect);

                    _InsertRect = t;

                    InvalidateRender(_InsertRect);
                }
            }
        }

        #region GetInsertRect

        private Rectangle GetInsertRect(Point pt)
        {
            _HitColumn = null;
            _AfterGroupBox = null;

            if (Bounds.Contains(pt) == true)
                return (GetGroupBoxInsertRect(pt));

            return (GetColumnInsertRect(pt));
        }

        #region GetColumnInsertRect

        private Rectangle GetColumnInsertRect(Point pt)
        {
            GridPanel panel = GridPanel;

            _HitColumn = GetHitColumn(panel, pt);

            if (_HitColumn != null)
            {
                Rectangle t = Bounds;
                Rectangle s = SViewRect;

                if (panel.ShowRowHeaders == true &&
                    RowHeaderVisibility != RowHeaderVisibility.Never)
                {
                    t.X += panel.RowHeaderWidth;
                }

                t.X += 2;
                t.Width = s.Right - t.X - 2;

                Rectangle r = _HitColumn.Bounds;

                if (pt.X > r.X + r.Width / 2)
                    r.X = r.Right;

                r.X -= (MarkerWidth / 2 + 1);
                r.Y = t.Bottom - MarkerHeight - 2;

                r.Width = MarkerWidth;
                r.Height = MarkerHeight;

                if (r.X < t.X)
                    r.X = t.X;

                if (r.X > t.Right - MarkerWidth)
                    r.X = t.Right - MarkerWidth;

                return (r);
            }

            return (Rectangle.Empty);
        }

        #region GetHitColumn

        private GridColumn GetHitColumn(GridPanel panel, Point pt)
        {
            if (panel.Bounds.Contains(pt) == true)
            {
                if (pt.Y >= Bounds.Bottom && pt.Y < Bounds.Bottom + 50)
                {
                    if (pt.X < panel.FirstVisibleColumn.Bounds.X)
                        return (panel.FirstVisibleColumn);

                    foreach (GridColumn col in panel.Columns)
                    {
                        if (col.Visible == true)
                        {
                            if (pt.X >= col.Bounds.X && pt.X <= col.Bounds.Right)
                                return (col);
                        }
                    }

                    return (panel.LastVisibleColumn);
                }
            }

            return (null);
        }

        #endregion

        #endregion

        #region GetGroupBoxInsertRect

        private Rectangle GetGroupBoxInsertRect(Point pt)
        {
            Rectangle t = Bounds;

            _AfterGroupBox = FindGroupPartition(pt);

            pt = new Point(_AfterGroupBox.Bounds.Right, _AfterGroupBox.Bounds.Y);

            if (GroupBoxLayout == GroupBoxLayout.Flat)
            {
                int index = _GroupBoxes.IndexOf(_AfterGroupBox);

                if (index + 1 < _GroupBoxes.Count)
                {
                    GridGroupBox beforeBox = _GroupBoxes[index + 1];

                    if (index == 0 || beforeBox.Bounds.Y < pt.Y)
                        pt.Y = beforeBox.Bounds.Y;
                }

                pt.X -= 2;
                pt.Y -= 9;

                if (pt.Y < t.Y + 6)
                    pt.Y = t.Y + 6;
            }
            else
            {
                pt.X += 2;
                pt.Y -= 6;

                if (pt.Y < t.Y + 2)
                    pt.Y = t.Y + 2;
            }

            pt.X = Math.Max(pt.X, t.X + 2);

            if (pt.X + MarkerWidth + 2 > ViewRect.Right)
                pt.X = ViewRect.Right - MarkerWidth - 2;

            return (new Rectangle(pt.X, pt.Y, MarkerWidth, MarkerHeight));
        }

        #endregion

        #endregion

        #endregion

        #region DragEnd

        private void DragEnd()
        {
            _Dragging = false;

            if (_GroupBoxWindow != null)
            {
                _GroupBoxWindow.Close();

                _GroupBoxWindow = null;
                _MouseDownHitBox = null;

                InvalidateRender(_InsertRect);
                _InsertRect = Rectangle.Empty;

                SuperGrid.PostInternalMouseMove();
            }
        }

        #endregion

        #endregion

        #region CancelCapture

        public override void CancelCapture()
        {
            if (CapturedItem == this)
            {
                Capture = false;

                DragEnd();
            }
        }

        #endregion

        #region Style support

        #region GetNewVisualStyle

        protected override TextRowVisualStyle GetNewVisualStyle()
        {
            return (new GroupByVisualStyle());
        }

        #endregion

        #region ApplyStyleEx

        protected override void ApplyStyleEx(TextRowVisualStyle style, StyleType[] css)
        {
            GroupByVisualStyle gstyle = style as GroupByVisualStyle;

            if (gstyle != null)
            {
                foreach (StyleType cs in css)
                {
                    gstyle.ApplyStyle(SuperGrid.BaseVisualStyles.GroupByStyles[cs]);
                    gstyle.ApplyStyle(SuperGrid.DefaultVisualStyles.GroupByStyles[cs]);
                    gstyle.ApplyStyle(GridPanel.DefaultVisualStyles.GroupByStyles[cs]);
                }

                if (gstyle.GroupBoxBorderColor == Color.Empty)
                    gstyle.GroupBoxBorderColor = Color.Blue;

                if (gstyle.WatermarkTextColor == Color.Empty)
                    gstyle.WatermarkTextColor = Color.Gray;

                if (gstyle.WatermarkFont == null)
                    gstyle.WatermarkFont = SystemFonts.DefaultFont;
            }

            InsertMarker = null;
        }

        #endregion

        #region GetGroupByEffectiveStyle

        private GroupByVisualStyle GetGroupByEffectiveStyle(GridGroupBox box)
        {
            return ((GroupByVisualStyle)
                GetEffectiveStyle(GetGroupByStyleType(box)));
        }

        #endregion

        #region GetGroupByStyleType

        private StyleType GetGroupByStyleType(GridGroupBox box)
        {
            if (IsMouseDown == true)
            {
                Point pt = Control.MousePosition;
                pt = SuperGrid.PointToClient(pt);

                if (box.FilterImageBounds.Contains(pt) == false)
                {
                    if (box.IsDragBox == true || box.IsEqualTo(_MouseDownHitBox))
                    {
                        if (box.IsEqualTo(_HitGroupBox) == true)
                            return (StyleType.SelectedMouseOver);

                        return (StyleType.Selected);
                    }

                    return (StyleType.Default);
                }
            }

            if (box.IsEqualTo(_HitGroupBox) == true)
                return (StyleType.MouseOver);

            return (StyleType.Default);
        }

        #endregion

        #endregion
    }

    #region GridGroupBox

    ///<summary>
    /// GroupBox definition
    ///</summary>
    public class GridGroupBox
    {
        #region Private properties

        private GridGroupByRow _GridGroupBy;
        private GridColumn _Column;

        private Rectangle _RelativeBounds;
        private Rectangle _SortImageRelBounds;
        private Rectangle _FilterImageRelBounds;

        private Padding _Padding = new Padding(0);
        private GroupBoxStyle _GroupBoxStyle = GroupBoxStyle.Rectangular;

        private bool _IsDragBox;

        private Size _ContentSize;
        private int _CornerRadius;

        #endregion

        #region Public properties

        #region Bounds

        ///<summary>
        /// Gets the associated Bounds
        ///</summary>
        public Rectangle Bounds
        {
            get
            {
                if (_IsDragBox == true)
                    return (_RelativeBounds);

                return (_GridGroupBy.GetScrollBounds(_RelativeBounds));
            }
        }

        #endregion

        #region Column

        ///<summary>
        /// Gets the associated GridColumn
        ///</summary>
        public GridColumn Column
        {
            get { return (_Column); }
            internal set { _Column = value; }
        }

        #endregion

        #region ContentSize

        ///<summary>
        /// Gets the associated Content Size
        ///</summary>
        public Size ContentSize
        {
            get { return (_ContentSize); }
            set { _ContentSize = value; }
        }

        #endregion

        #region CornerRadius

        ///<summary>
        /// Gets or sets the corner radius to
        /// use when GroupBoxStyle is RoundedRectangular
        ///</summary>
        public int CornerRadius
        {
            get { return (_CornerRadius); }
            set { _CornerRadius = value; }
        }

            #endregion

        #region GridGroupBy

        ///<summary>
        /// Gets the associated GridGroupBy object
        ///</summary>
        public GridGroupByRow GridGroupBy
        {
            get { return (_GridGroupBy); }
            internal set { _GridGroupBy = value; }
        }

        #endregion

        #region GroupBoxStyle

        ///<summary>
        /// GroupBox Style
        ///</summary>
        public GroupBoxStyle GroupBoxStyle
        {
            get { return (_GroupBoxStyle); }
            set { _GroupBoxStyle = value; }
        }

        #endregion

        #region Padding

        ///<summary>
        /// Content padding
        ///</summary>
        public Padding Padding
        {
            get { return (_Padding); }
            set { _Padding = value; }
        }

        #endregion

        #endregion

        #region Internal properties

        #region FilterImageBounds

        internal Rectangle FilterImageBounds
        {
            get
            {
                if (_IsDragBox == true || _FilterImageRelBounds.IsEmpty)
                    return (_FilterImageRelBounds);

                return (_GridGroupBy.GetScrollBounds(_FilterImageRelBounds));
            }
        }

        #endregion

        #region FilterImageRelBounds

        internal Rectangle FilterImageRelBounds
        {
            get { return (_FilterImageRelBounds); }
            set { _FilterImageRelBounds = value; }
        }

        #endregion

        #region IsDragBox

        internal bool IsDragBox
        {
            get { return (_IsDragBox); }
            set { _IsDragBox = value; }
        }

        #endregion

        #region RelativeBounds

        internal Rectangle RelativeBounds
        {
            get { return (_RelativeBounds); }
            set { _RelativeBounds = value; }
        }

        #endregion

        #region SortImageBounds

        internal Rectangle SortImageBounds
        {
            get
            {
                if (_IsDragBox == true || _SortImageRelBounds.IsEmpty)
                    return (_SortImageRelBounds);

                return (_GridGroupBy.GetScrollBounds(_SortImageRelBounds));
            }
        }

        #endregion

        #region SortImageRelBounds

        internal Rectangle SortImageRelBounds
        {
            get { return (_SortImageRelBounds); }
            set { _SortImageRelBounds = value; }
        }

        #endregion

        #endregion

        internal GridGroupBox(GridGroupByRow groupBy, GridColumn column)
        {
            _GridGroupBy = groupBy;
            _Column = column;

            _GroupBoxStyle = groupBy.GroupBoxStyle;
        }

        #region IsEqualTo

        internal bool IsEqualTo(GridGroupBox box)
        {
            return (box != null && box.Column == Column);
        }

        #endregion
    }

    #endregion

    #region enums

    #region GroupBoxEffects

    ///<summary>
    /// GroupBoxEffects
    ///</summary>
    public enum GroupBoxEffects
    {
        ///<summary>
        /// Not set.
        ///</summary>
        NotSet = -1,

        ///<summary>
        /// The Column will not interact with the GroupBox.
        ///</summary>
        None,

        ///<summary>
        /// The Column presentation will be copied to and
        /// from the grid and associated GroupBox.
        ///</summary>
        Copy,

        ///<summary>
        /// The Column presentation will be moved to and
        /// from the grid and associated GroupBox.
        ///</summary>
        Move,
    }

    #endregion

    #region GroupBoxLayout

    ///<summary>
    /// GroupBox Layout
    ///</summary>
    public enum GroupBoxLayout
    {
        ///<summary>
        /// Group boxes are Flat, all on the same layout row
        ///</summary>
        Flat,

        ///<summary>
        /// Group boxes are oriented Hierarchically
        ///</summary>
        Hierarchical,
    }

    #endregion

    #region GroupBoxStyle

    ///<summary>
    /// GroupBox Style
    ///</summary>
    public enum GroupBoxStyle
    {
        ///<summary>
        /// Group boxes are drawn Rectangular
        ///</summary>
        Rectangular,

        ///<summary>
        /// Group boxes are drawn Rounded Rectangular
        ///</summary>
        RoundedRectangular,
    }

    #endregion

    #endregion
}
