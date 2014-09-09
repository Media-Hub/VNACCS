using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid.Primitives;
using DevComponents.DotNetBar.SuperGrid.Style;
using DevComponents.SuperGrid.TextMarkup;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// GridGroup
    ///</summary>
    public class GridGroup : GridContainer
    {
        #region Private variables

        private GridCell _Cell;

        private string _Text;
        private Size _TextSize;

        private Rectangle _ExpandButtonBounds;

        private GroupArea _HitArea;
        private GroupArea _MouseDownHitArea;
        private GroupArea _LastHitArea;

        private GroupHeaderVisualStyles _GroupHeaderVisualStyles;
        private GroupHeaderVisualStyles _EffectiveVisualStyles;

        private int _StyleUpdateCount;

        private BodyElement _GroupTextMarkup;

        #endregion

        #region Public properties

        #region Bounds

        ///<summary>
        /// Bounds
        ///</summary>
        public override Rectangle Bounds
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    Rectangle r = BoundsRelative;

                    if (IsVFrozen == false)
                        r.Y -= VScrollOffset;

                    if (panel.IsSubPanel == true)
                        r.X -= HScrollOffset;

                    r.Height = FixedRowHeight;

                    Rectangle t = ViewRect;

                    if (r.Right > t.Right)
                        r.Width -= (r.Right - t.Right);

                    return (r);
                }

                return (Rectangle.Empty);
            }
        }

        #endregion

        #region Cell

        ///<summary>
        /// Gets the Group's associated grid cell.
        ///</summary>
        public GridCell Cell
        {
            get { return (_Cell); }
            internal set { _Cell = value; }
        }

        #endregion

        #region Column

        ///<summary>
        /// Gets the Group's associated grid Column.
        ///</summary>
        public GridColumn Column
        {
            get { return (_Cell != null ? _Cell.GridColumn : null); }
        }

        #endregion

        #region GroupHeaderVisualStyles

        /// <summary>
        /// Gets or sets the visual styles
        /// assigned to the element. Default value is null.
        /// </summary>
        [DefaultValue(null), Category("Style")]
        [Description("Indicates visual style assigned to the element")]
        public GroupHeaderVisualStyles GroupHeaderVisualStyles
        {
            get
            {
                if (_GroupHeaderVisualStyles == null)
                {
                    _GroupHeaderVisualStyles = new GroupHeaderVisualStyles();

                    GroupHeaderStyleChangeHandler(null, _GroupHeaderVisualStyles);
                }

                return (_GroupHeaderVisualStyles);
            }

            set
            {
                if (_GroupHeaderVisualStyles != value)
                {
                    GroupHeaderVisualStyles oldValue = _GroupHeaderVisualStyles;
                    _GroupHeaderVisualStyles = value;

                    OnCellStyleChanged("GroupHeaderVisualStyle", oldValue, value);
                }
            }
        }

        private void OnCellStyleChanged(string property,
            GroupHeaderVisualStyles oldValue, GroupHeaderVisualStyles newValue)
        {
            GroupHeaderStyleChangeHandler(oldValue, newValue);

            OnPropertyChanged(new PropertyChangedEventArgs(property));
        }

        #endregion

        #region GroupValue

        ///<summary>
        /// GroupValue
        ///</summary>
        public object GroupValue
        {
            get { return (_Cell != null ? _Cell.Value : 0); }
        }

        #endregion

        #region PlainText

        ///<summary>
        /// Gets the "Plain" header Text (MiniMarkup stripped)
        ///</summary>
        public string PlainText
        {
            get
            {
                if (_GroupTextMarkup != null)
                    return (_GroupTextMarkup.PlainText);

                return (_Text);
            }
        }

        #endregion

        #region Text

        ///<summary>
        /// Text
        ///</summary>
        public string Text
        {
            get { return (_Text); }

            set
            {
                if (_Text != value)
                {
                    _Text = value;

                    MarkupGroupTextChanged();

                    OnPropertyChangedEx("Text", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #endregion

        #region Internal properties

        #region GroupTextMarkup

        internal BodyElement GroupTextMarkup
        {
            get { return (_GroupTextMarkup); }
            set { _GroupTextMarkup = value; }
        }

        #endregion

        #endregion

        #region MeasureOverride

        #region MeasureOverride

        protected override void MeasureOverride(
            GridLayoutInfo layoutInfo, GridLayoutStateInfo stateInfo, Size constraintSize)
        {
            Size sizeNeeded = Size.Empty;
            GridPanel panel = stateInfo.GridPanel;
            Graphics g = layoutInfo.Graphics;

            if (CanShowRowHeader(panel) == true)
                sizeNeeded.Width += panel.RowHeaderWidth;

            GroupHeaderVisualStyle style = GetSizingStyle(panel);

            Size size = MeasureGroupText(g, style, constraintSize);

            if (size.Height > 0)
            {
                size.Height += 8;

                size.Width += style.Padding.Horizontal;
                size.Height += style.Padding.Vertical;
            }

            size.Height = Math.Max(size.Height, panel.GroupHeaderHeight);

            sizeNeeded.Width += size.Width;
            sizeNeeded.Height = Math.Max(size.Height, sizeNeeded.Height);

            if (panel.ShowGroupUnderline == true && size.Height > 0)
                sizeNeeded.Height += 4;

            FixedRowHeight = sizeNeeded.Height;

            if (Rows != null && Expanded == true)
            {
                if (panel.IndentGroups == true)
                {
                    GridLayoutStateInfo itemStateInfo = new
                        GridLayoutStateInfo(panel, stateInfo.IndentLevel + 1);

                    size = MeasureSubItems(layoutInfo, itemStateInfo, constraintSize);
                }
                else
                {
                    size = MeasureSubItems(layoutInfo, stateInfo, constraintSize);
                }

                sizeNeeded.Width = Math.Max(size.Width, sizeNeeded.Width);
                sizeNeeded.Height += size.Height;
            }

            Size = sizeNeeded;
        }

        #endregion

        #region MeasureGroupText

        private Size MeasureGroupText(Graphics g,
            GroupHeaderVisualStyle style, Size constraintSize)
        {
            _TextSize = Size.Empty;

            if (String.IsNullOrEmpty(Text) == false)
            {
                GridPanel panel = GridPanel;

                if (panel.IsSubPanel == false)
                {
                    if (panel.ShowGroupExpand == true)
                    {
                        constraintSize.Width = SViewRect.Width;

                        int n = panel.TreeButtonIndent +
                                (panel.LevelIndentSize.Width * IndentLevel);

                        constraintSize.Width -= n;
                    }
                }

                _TextSize = MeasureHeaderText(g, style, constraintSize);
            }

            return (_TextSize);
        }

        #region MeasureHeaderText

        private Size MeasureHeaderText(Graphics g,
            GroupHeaderVisualStyle style, Size constraintSize)
        {
            Size size = Size.Empty;

            string s = Text;

            if (string.IsNullOrEmpty(s) == false)
            {
                if (_GroupTextMarkup != null)
                {
                    size = GetMarkupTextSize(g,
                        _GroupTextMarkup, style, constraintSize.Width);
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
            BodyElement textMarkup, GroupHeaderVisualStyle style, int width)
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

        #region ArrangeOverride

        protected override void ArrangeOverride(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, Rectangle layoutBounds)
        {
            Rectangle bounds = layoutBounds;
            bounds.Height = FixedRowHeight;

            GridPanel panel = stateInfo.GridPanel;
            IndentLevel = stateInfo.IndentLevel;

            if (panel.ShowGroupExpand == true)
            {
                GroupHeaderVisualStyle style = GetSizingStyle(panel);

                Rectangle r = bounds;
                r.Width = panel.LevelIndentSize.Width;
                r.Height -= style.Padding.Vertical;

                if (r.Height < panel.GroupHeaderHeight)
                    r.Height = panel.GroupHeaderHeight;

                if (CanShowRowHeader(panel) == true)
                    r.X += panel.RowHeaderWidth;

                int n = panel.TreeButtonIndent +
                    (panel.LevelIndentSize.Width * IndentLevel) - panel.LevelIndentSize.Width;

                r.X += n;

                if (r.Right > BoundsRelative.Right)
                    r.Width -= r.Right - BoundsRelative.Right;

                r.X += style.Padding.Left;
                r.Y += style.Padding.Top;

                Size size = panel.GetTreeButtonSize();

                r.X += (r.Width - size.Width) / 2;
                r.Y += (r.Height - size.Height) / 2 - 1;

                r.Size = size;

                _ExpandButtonBounds = r;
            }
            else
            {
                _ExpandButtonBounds = Rectangle.Empty;
            }

            if (Rows != null && Expanded == true)
            {
                bounds = layoutBounds;

                bounds.Y += FixedRowHeight;
                bounds.Height -= FixedRowHeight;

                if (panel.IndentGroups == true)
                {
                    GridLayoutStateInfo itemStateInfo =
                        new GridLayoutStateInfo(panel, stateInfo.IndentLevel + 1);

                    ArrangeSubItems(layoutInfo, itemStateInfo, bounds);
                }
                else
                {
                    ArrangeSubItems(layoutInfo, stateInfo, bounds);
                }
            }
        }

        #endregion

        #region RenderOverride

        protected override void RenderOverride(GridRenderInfo renderInfo)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                Rectangle r = Bounds;

                if (r.IntersectsWith(renderInfo.ClipRectangle))
                {
                    Graphics g = renderInfo.Graphics;
                    GroupHeaderVisualStyle style = GetEffectiveStyle();

                    if (SuperGrid.DoPreRenderGroupHeaderEvent(g,
                        this, RenderParts.Background | RenderParts.RowHeader, r) == false)
                    {
                        RenderGroupBackground(g, style, r);
                        RenderRowHeader(g, panel, style, r);

                        SuperGrid.DoPostRenderGroupHeaderEvent(g,
                            this, RenderParts.Background | RenderParts.RowHeader, r);
                    }

                    if (CanShowRowHeader(panel) == true)
                    {
                        r.X += panel.RowHeaderWidth;
                        r.Width -= panel.RowHeaderWidth;
                    }
                    
                    RenderHeaderExpand(g, panel, ref r);

                    r = GetAdjustedBounds(style, r);

                    if (r.Height < panel.GroupHeaderHeight)
                        r.Height = panel.GroupHeaderHeight;

                    if (SuperGrid.DoPreRenderGroupHeaderEvent(g,
                        this, RenderParts.Content, r) == false)
                    {
                        RenderHeaderText(g, style, r);
                        RenderHeaderUnderline(g, panel, style, r);

                        SuperGrid.DoPostRenderGroupHeaderEvent(g,
                            this, RenderParts.Content, r);
                    }
                }

                if (Rows != null && Expanded == true)
                {
                    foreach (GridElement item in Rows)
                    {
                        if (item.Visible == true)
                            item.Render(renderInfo);
                    }
                }
            }
        }

        #region GetAdjustedBounds

        private Rectangle GetAdjustedBounds(GroupHeaderVisualStyle style, Rectangle r)
        {
            r.X += (style.Padding.Left);
            r.Width -= (style.Padding.Horizontal);

            r.Y += (style.Padding.Top + 2);
            r.Height -= (style.Padding.Vertical + 2);

            return (r);
        }

        #endregion

        #region RenderRowHeader

        private void RenderRowHeader(Graphics g,
            GridPanel panel, GroupHeaderVisualStyle style, Rectangle r)
        {
            if (CanShowRowHeader(panel) == true)
            {
                r.Width = panel.RowHeaderWidth;

                using (Brush br = style.RowHeaderStyle.Background.GetBrush(r))
                    g.FillRectangle(br, r);

                using (Pen pen = new Pen(style.RowHeaderStyle.BorderHighlightColor))
                {
                    g.DrawLine(pen, r.X + 1, r.Top, r.Right - 2, r.Top);
                    g.DrawLine(pen, r.X + 1, r.Top, r.X + 1, r.Bottom - 1);
                }

                GridPanelVisualStyle pstyle = panel.GetEffectiveStyle();

                using (Pen pen = new Pen(pstyle.HeaderLineColor))
                {
                    g.DrawLine(pen, r.X, r.Top - 1, r.Right - 1, r.Top - 1);
                    g.DrawLine(pen, r.X, r.Bottom - 1, r.Right - 2, r.Bottom - 1);
                    g.DrawLine(pen, r.Right - 1, r.Top, r.Right - 1, r.Bottom - 1);
                }

                r.X += 4;
                r.Width -= 4;

                int n = RenderIndicatorImage(g, panel, this, r);

                r.X += n;
                r.Width -= n;

                if (panel.ShowRowGridIndex == true)
                    RenderGridIndex(g, this, style.RowHeaderStyle, r);
            }
        }

        #region RenderIndicatorImage

        private int RenderIndicatorImage(Graphics g,
            GridPanel panel, GridContainer row, Rectangle r)
        {
            RowHeaderVisualStyle style = GetEffectiveRowStyle().RowHeaderStyle;

            Image image = style.GetActiveRowImage(panel);

            if (image != null)
            {
                if (panel.ActiveRow == row)
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
            GridContainer row, RowHeaderVisualStyle style, Rectangle r)
        {
            string text = (row.GridIndex + row.GridPanel.RowHeaderIndexOffset).ToString();

            Font font = style.Font ?? SystemFonts.DefaultFont;

            r.Inflate(-2, 0);

            TextDrawing.DrawString(g, text,
                font, style.TextColor, r, style.GetTextFormatFlags());

            return;
        }

        #endregion

        #endregion

        #region CanShowRowHeader

        protected virtual bool CanShowRowHeader(GridPanel panel)
        {
            switch (panel.GroupRowHeaderVisibility)
            {
                case RowHeaderVisibility.Always:
                    return (true);

                case RowHeaderVisibility.PanelControlled:
                    return (panel.ShowRowHeaders);
            }

            return (false);
        }

        #endregion

        #region RenderGroupBackground

        private void RenderGroupBackground(
            Graphics g, GroupHeaderVisualStyle style, Rectangle r)
        {
            if (style.Background != null && style.Background.IsEmpty == false)
            {
                using (Brush br = style.Background.GetBrush(r))
                    g.FillRectangle(br, r);
            }
        }

        #endregion

        #region RenderHeaderExpand

        private void RenderHeaderExpand(
            Graphics g, GridPanel panel, ref Rectangle r)
        {
            if (panel.ShowGroupExpand == true)
            {
                Rectangle bounds = _ExpandButtonBounds;

                if (IsVFrozen == false)
                    bounds.Y -= VScrollOffset;

                if (panel.IsSubPanel == true)
                    bounds.X -= HScrollOffset;

                bool hot = (_HitArea == GroupArea.ExpandButton);

                Image image = (Expanded == true)
                    ? panel.GetCollapseButton(g, hot) : panel.GetExpandButton(g, hot);

                ExpandDisplay.RenderButton(g, image, bounds, r);

                int n = panel.TreeButtonIndent +
                    (panel.LevelIndentSize.Width * IndentLevel);

                r.X += n;
                r.Width -= n;
            }
            else
            {
                r.X += 3;
                r.Width -= 3;
            }
        }

        #endregion

        #region RenderHeaderText

        private void RenderHeaderText(Graphics g,
            GroupHeaderVisualStyle style, Rectangle r)
        {
            eTextFormat tf = style.GetTextFormatFlags();

            if (style.Alignment == Alignment.BottomLeft ||
                style.Alignment == Alignment.BottomCenter ||
                style.Alignment == Alignment.BottomRight)
            {
                r.Height -= 4;
            }

            if (r.Width > 0 && r.Height > 0)
            {
                if (_GroupTextMarkup != null)
                    RenderTextMarkup(g, _GroupTextMarkup, style, r);
                else
                    TextDrawing.DrawString(g, Text, style.Font, style.TextColor, r, tf);
            }
        }

        #region RenderTextMarkup

        private void RenderTextMarkup(Graphics g,
            BodyElement textMarkup, GroupHeaderVisualStyle style, Rectangle r)
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

        #region RenderHeaderUnderline

        private void RenderHeaderUnderline(Graphics g,
            GridPanel panel, GroupHeaderVisualStyle style, Rectangle r)
        {
            if (panel.ShowGroupUnderline == true && style.UnderlineColor.IsEmpty == false)
            {
                switch (style.Alignment)
                {
                    case Alignment.MiddleLeft:
                    case Alignment.MiddleCenter:
                    case Alignment.MiddleRight:
                        r.Y += (r.Height + _TextSize.Height) / 2 + 4;
                        break;

                    case Alignment.TopLeft:
                    case Alignment.TopCenter:
                    case Alignment.TopRight:
                        r.Y += _TextSize.Height + 6;
                        break;

                    default:
                        r.Y = r.Bottom - _TextSize.Height / 2;
                        break;
                }

                if (r.Width > SViewRect.Width)
                    r.Width = SViewRect.Width;

                int n = r.Width;

                r.Height = 1;
                r.Width = (int)(r.Width * .75);

                Rectangle t = r;

                Color color1 = style.UnderlineColor;
                Color color2 = Color.Transparent;

                switch (style.Alignment)
                {
                    case Alignment.TopCenter:
                    case Alignment.MiddleCenter:
                    case Alignment.BottomCenter:
                        r.X += (n - r.Width) / 2;
                        r.Width /= 2;

                        color1 = Color.Transparent;
                        color2 = style.UnderlineColor;
                        break;

                    case Alignment.TopRight:
                    case Alignment.MiddleRight:
                    case Alignment.BottomRight:
                        r.X += n - r.Width;

                        color1 = Color.Transparent;
                        color2 = style.UnderlineColor;
                        break;
                }

                t.X = r.X;

                using (LinearGradientBrush br = new
                    LinearGradientBrush(r, color1, color2, 0f))
                {
                    br.WrapMode = WrapMode.TileFlipXY;

                    g.FillRectangle(br, t);
                }
            }
        }

        #endregion

        #endregion

        #region Mouse support

        #region InternalMouseEnter

        internal override void InternalMouseEnter(EventArgs e)
        {
            base.InternalMouseEnter(e);

            InvalidateRender();
        }

        #endregion

        #region InternalMouseLeave

        internal override void InternalMouseLeave(EventArgs e)
        {
            base.InternalMouseLeave(e);

            InvalidateRender();
        }

        #endregion

        #region InternalMouseMove

        internal override void InternalMouseMove(MouseEventArgs e)
        {
            _HitArea = GetGroupAreaAt(e);

            switch (_HitArea)
            {
                case GroupArea.ExpandButton:
                    SuperGrid.GridCursor = Cursors.Hand;
                    break;

                case GroupArea.Content:
                    SuperGrid.GridCursor = Cursors.Default;
                    break;
            }

            if (_LastHitArea != _HitArea)
            {
                _LastHitArea = _HitArea;

                InvalidateRender();
            }

            base.InternalMouseMove(e);
        }

        #endregion

        #region InternalMouseDown

        internal override void InternalMouseDown(MouseEventArgs e)
        {
            _MouseDownHitArea = _HitArea;

            GridPanel panel = GridPanel;

            if (panel != null)
            {
                switch (_MouseDownHitArea)
                {
                    case GroupArea.Content:
                        bool ckey = ((Control.ModifierKeys & Keys.Control) == Keys.Control);
                        bool skey = ((Control.ModifierKeys & Keys.Shift) == Keys.Shift);

                        if (skey == false && ckey == false)
                            panel.ClearAll();

                        switch (panel.GroupHeaderClickBehavior)
                        {
                            case GroupHeaderClickBehavior.ExpandCollapse:
                                Expanded = !Expanded;
                                break;

                            case GroupHeaderClickBehavior.Select:
                                ProcessSelect(panel, ckey, skey);
                                break;

                            case GroupHeaderClickBehavior.SelectAll:
                                ProcessSelectAll(panel, this, ckey);
                                panel.InvalidateRender();
                                break;
                        }
                        break;

                    case GroupArea.ExpandButton:
                        Expanded = !Expanded;
                        return;
                }
            }

            base.InternalMouseDown(e);
        }

        #region ProcessSelect

        private void ProcessSelect(GridPanel panel, bool ckey, bool skey)
        {
            int index = GridIndex;

            if (ckey == true)
                panel.SetSelectedRows(index, 1, !panel.IsRowSelected(index));
            else
                panel.SetSelectedRows(index, 1, true);

            panel.SetActiveRow(this, true); 

            if (skey == false && ckey == false)
                panel.InvalidateRender();
            else
                InvalidateRender();
        }

        #endregion

        #region ProcessSelectAll

        private void ProcessSelectAll(GridPanel panel, 
            GridContainer item, bool ckey)
        {
            if (item.Rows != null && item.Rows.Count > 0)
            {
                item.Expanded = true;

                foreach (GridContainer row in item.Rows)
                {
                    if (row is GridGroup)
                    {
                        ProcessSelectAll(panel, row, ckey);
                    }
                    else
                    {
                        int startIndex = ((GridContainer)item.Rows[0]).GridIndex;
                        int endIndex = ((GridContainer)item.Rows[item.Rows.Count - 1]).GridIndex;

                        if (ckey == true)
                        {
                            for (int i = startIndex; i <= endIndex; i++)
                                panel.SetSelectedRows(i, 1, !panel.IsRowSelected(i));
                        }
                        else
                        {
                            panel.SetSelectedRows(startIndex, endIndex - startIndex + 1, true);
                        }

                        break;
                    }
                }
            }
        }

        #endregion

        #endregion

        #region InternalMouseUp

        /// <summary>
        /// InternalMouseUp
        /// </summary>
        /// <param name="e"></param>
        internal override void InternalMouseUp(MouseEventArgs e)
        {
            if (_HitArea == _MouseDownHitArea)
                SuperGrid.DoGroupHeaderClickEvent(this, _MouseDownHitArea, e);
        }

        #endregion

        #region InternalMouseDoubleClick

        internal override void InternalMouseDoubleClick(MouseEventArgs e)
        {
            if (_HitArea == _MouseDownHitArea)
                SuperGrid.DoGroupHeaderDoubleClickEvent(this, _MouseDownHitArea, e);
        }

        #endregion

        #endregion

        #region GetGroupAreaAt

        /// <summary>
        /// Returns element at specified mouse coordinates
        /// </summary>
        /// <param name="e">Mouse event arguments</param>
        /// <returns>Reference to child Group element or null
        /// if no element at specified coordinates</returns>
        protected virtual GroupArea GetGroupAreaAt(MouseEventArgs e)
        {
            return GetGroupAreaAt(e.X, e.Y);
        }

        /// <summary>
        /// Returns element at specified mouse coordinates
        /// </summary>
        /// <param name="x">Horizontal position</param>
        /// <param name="y">Vertical position</param>
        /// <returns>Reference to child element or null
        /// if no element at specified coordinates</returns>
        protected virtual GroupArea GetGroupAreaAt(int x, int y)
        {
            Rectangle r = BoundsRelative;
            r.Height = FixedRowHeight;

            if (IsVFrozen == false)
                r.Y -= VScrollOffset;

            if (GridPanel.IsSubPanel == true)
                r.X -= HScrollOffset;

            if (r.Contains(x, y) == true)
            {
                r = _ExpandButtonBounds;

                if (IsVFrozen == false)
                    r.Y -= VScrollOffset;

                if (GridPanel.IsSubPanel == true)
                    r.X -= HScrollOffset;

                if (r.Contains(x, y) == true)
                    return (GroupArea.ExpandButton);

                return (GroupArea.Content);
            }

            return (GroupArea.NoWhere);
        }

        #endregion

        #region InvalidateRender

        public override void InvalidateRender()
        {
            Rectangle bounds = BoundsRelative;

            if (IsVFrozen == false)
                bounds.Y -= VScrollOffset;

            SuperGridControl grid = SuperGrid;

            if (grid != null)
                grid.InvalidateRender(bounds);

            OnRenderInvalid(EventArgs.Empty);
        }

        #endregion

        #region Style support routines

        #region GroupHeaderStyleChangeHandler

        private void GroupHeaderStyleChangeHandler(
            GroupHeaderVisualStyles oldValue, GroupHeaderVisualStyles newValue)
        {
            if (oldValue != null)
                oldValue.PropertyChanged -= StyleChanged;

            if (newValue != null)
                newValue.PropertyChanged += StyleChanged;
        }

        #endregion

        #region GetEffectiveStyle

        ///<summary>
        /// GetEffectiveStyle
        ///</summary>
        ///<returns></returns>
        public GroupHeaderVisualStyle GetEffectiveStyle()
        {
            StyleState state = GetState();

            return (GetEffectiveStyle(state));
        }

        internal GroupHeaderVisualStyle GetEffectiveStyle(StyleType styleType)
        {
            ValidateStyle();

            return (GetStyle(styleType));
        }

        internal GroupHeaderVisualStyle GetEffectiveStyle(StyleState state)
        {
            ValidateStyle();

            switch (state)
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

        #region GetStyle

        private GroupHeaderVisualStyle GetStyle(StyleType e)
        {
            if (_EffectiveVisualStyles.IsValid(e) == false)
            {
                GroupHeaderVisualStyle style = new GroupHeaderVisualStyle();

                StyleType[] css = style.GetApplyStyleTypes(e);

                if (css != null)
                {
                    foreach (StyleType cs in css)
                    {
                        style.ApplyStyle(SuperGrid.BaseVisualStyles.GroupHeaderStyles[cs]);
                        style.ApplyStyle(SuperGrid.DefaultVisualStyles.GroupHeaderStyles[cs]);
                        style.ApplyStyle(GridPanel.DefaultVisualStyles.GroupHeaderStyles[cs]);

                        style.ApplyStyle(GroupHeaderVisualStyles[cs]);
                    }
                }

                SuperGrid.DoGetGroupHeaderStyleEvent(this, e, ref style);

                if (style.Background == null || style.Background.IsEmpty == true)
                    style.Background = new Background(Color.White);

                if (style.Font == null)
                    style.Font = SystemFonts.DefaultFont;

                if (style.TextColor.IsEmpty)
                    style.TextColor = Color.Black;

                _EffectiveVisualStyles[e] = style;
            }

            return (_EffectiveVisualStyles[e]);
        }

        #endregion

        #region ValidateStyle

        private void ValidateStyle()
        {
            if (_EffectiveVisualStyles == null ||
                (_StyleUpdateCount != SuperGrid.StyleUpdateCount))
            {
                _EffectiveVisualStyles = new GroupHeaderVisualStyles();

                _StyleUpdateCount = SuperGrid.StyleUpdateCount;
            }
        }

        #endregion

        #region GetState

        internal StyleState GetState()
        {
            StyleState rowState = StyleState.Default;

            if (IsMouseOver == true)
            {
                Point pt = SuperGrid.PointToClient(Control.MousePosition);

                if (Bounds.Contains(pt) == true)
                    rowState |= StyleState.MouseOver;
            }

            if (IsSelected == true)
                rowState |= StyleState.Selected;

            return (rowState);
        }

        #endregion

        #region GetSizingStyle

        private GroupHeaderVisualStyle GetSizingStyle(GridPanel panel)
        {
            StyleType styleType = panel.GetSizingStyle();

            if (styleType == StyleType.NotSet)
                styleType = StyleType.Default;

            return (GetEffectiveStyle(styleType));
        }

        #endregion

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

        void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                panel.NeedToUpdateIndicees = true;
                panel.InvalidateLayout();
            }
        }

        #endregion

        #region Markup support

        private void MarkupGroupTextChanged()
        {
            _GroupTextMarkup = null;

            if (Column != null && Column.EnableGroupHeaderMarkup == true)
            {
                if (MarkupParser.IsMarkup(_Text) == true)
                    _GroupTextMarkup = MarkupParser.Parse(_Text);
            }
        }

        #endregion
    }

    #region enums

    #region GroupArea

    ///<summary>
    /// GroupArea
    ///</summary>
    public enum GroupArea
    {
        ///<summary>
        /// Content
        ///</summary>
        Content,

        ///<summary>
        /// ExpandButton
        ///</summary>
        ExpandButton,

        ///<summary>
        /// NoWhere
        ///</summary>
        NoWhere,
    }

    #endregion

    #endregion

}
