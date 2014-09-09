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
    /// Defines the grid column header
    /// </summary>
    [TypeConverter(typeof(BlankExpandableObjectConverter))]
    public abstract class GridTextRow : GridElement
    {
        #region Private variables

        private string _Text;
        private bool _EnableMarkup = true;
        private BodyElement _TextMarkup;

        private int _RowHeight;

        private HeaderArea _HitArea;
        private HeaderArea _MouseDownHitArea;

        private RowHeaderVisibility
            _RowHeaderVisibility = RowHeaderVisibility.PanelControlled;

        private TextRowVisualStyles _EffectiveRowStyles;
        private int _StyleUpdateCount;

        private Image _BackgroundImage;
        private GridBackgroundImageLayout _BackgroundImageLayout = GridBackgroundImageLayout.Tile;

        #endregion

        #region Constructors

        ///<summary>
        /// GridHeader
        ///</summary>
        protected GridTextRow()
        {
        }

        ///<summary>
        /// GridHeader
        ///</summary>
        ///<param name="text"></param>
        protected GridTextRow(string text)
        {
            _Text = text;
        }

        #endregion

        #region Public properties

        #region BackgroundImage

        /// <summary>
        /// Gets or sets the Background Image
        /// </summary>
        [DefaultValue(null), Category("Appearance")]
        [Description("Indicates the Background Image")]
        public Image BackgroundImage
        {
            get { return (_BackgroundImage); }

            set
            {
                if (_BackgroundImage != value)
                {
                    _BackgroundImage = value;

                    OnPropertyChangedEx("BackgroundImage", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region BackgroundImageLayout

        /// <summary>
        /// Gets or sets the layout of the Background Image
        /// </summary>
        [DefaultValue(GridBackgroundImageLayout.Tile), Category("Appearance")]
        [Description("Indicates the layout of the Background Image")]
        public GridBackgroundImageLayout BackgroundImageLayout
        {
            get { return (_BackgroundImageLayout); }

            set
            {
                if (_BackgroundImageLayout != value)
                {
                    _BackgroundImageLayout = value;

                    OnPropertyChangedEx("BackgroundImageLayout", VisualChangeType.Render);
                }
            }
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

        #region EnableMarkup

        /// <summary>
        /// Gets or sets whether text-markup support is enabled
        /// </summary>
        [DefaultValue(true), Category("Appearance")]
        [Description("Indicates whether text-markup support is enabled.")]
        public bool EnableMarkup
        {
            get { return (_EnableMarkup); }

            set
            {
                if (_EnableMarkup != value)
                {
                    _EnableMarkup = value;

                    MarkupTextChanged();

                    OnPropertyChangedEx("EnableMarkup", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region ImageBounds

        /// <summary>
        /// Gets whether the item is empty
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle ImageBounds
        {
            get
            {
                GridPanel panel = GridPanel;

                if (panel != null)
                {
                    TextRowVisualStyle style = GetEffectiveStyle();

                    Image image = style.GetImage(panel);

                    if (image != null)
                    {
                        Rectangle tb, ib;
                        GetItemBounds(panel, image, style, Bounds, out tb, out ib);

                        return (ib);
                    }
                }

                return (Rectangle.Empty);
            }
        }

        #endregion

        #region IsEmpty

        /// <summary>
        /// Gets whether the item is empty
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsEmpty
        {
            get { return (string.IsNullOrEmpty(_Text) == true); }
        }

        #endregion

        #region RowHeight

        /// <summary>
        /// Gets or sets the row height (0 to auto-size)
        /// </summary>
        [DefaultValue(0), Category("Appearance")]
        [Description("Indicates the row height (0 to auto-size)")]
        public int RowHeight
        {
            get { return (_RowHeight); }

            set
            {
                if (_RowHeight != value)
                {
                    _RowHeight = value;

                    OnPropertyChangedEx("RowHeight", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region RowHeaderVisibility

        /// <summary>
        /// Gets or sets whether the RowHeader is displayed
        /// </summary>
        [DefaultValue(RowHeaderVisibility.PanelControlled), Category("Appearance")]
        [Description("Indicates whether the RowHeader is displayed")]
        public virtual RowHeaderVisibility RowHeaderVisibility
        {
            get { return (_RowHeaderVisibility); }

            set
            {
                if (value != _RowHeaderVisibility)
                {
                    _RowHeaderVisibility = value;

                    OnPropertyChangedEx("RowHeaderVisibility", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region Text

        /// <summary>
        /// Gets or sets the Text
        /// </summary>
        [DefaultValue(null), Category("Appearance")]
        [Description("Indicates the Text.")]
        [Editor("DevComponents.DotNetBar.Design.TextMarkupUIEditor, DevComponents.DotNetBar.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=90f470f34c89ccaf", typeof(UITypeEditor))]
        public string Text
        {
            get { return (_Text); }

            set
            {
                if (_Text != value)
                {
                    _Text = value;

                    MarkupTextChanged();

                    OnPropertyChangedEx("Text", VisualChangeType.Layout);
                }
            }
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
            Size sizeNeeded = MeasureRow(layoutInfo, stateInfo, constraintSize);

            if (constraintSize.Width > 0)
                sizeNeeded.Width = constraintSize.Width;

            Size = sizeNeeded;
        }

        #region MeasureRow

        protected virtual Size MeasureRow(
            GridLayoutInfo layoutInfo, GridLayoutStateInfo stateInfo, Size constraintSize)
        {
            Size sizeNeeded = Size.Empty;

            if (IsEmpty == false)
            {
                GridPanel panel = stateInfo.GridPanel;
                TextRowVisualStyle style = GetSizingStyle(stateInfo.GridPanel);

                Alignment imageAlignment = style.ImageAlignment;
                Size imageSize = style.GetImageSize(style.Image);

                if (style.IsOverlayImage == true)
                    imageAlignment = Alignment.MiddleCenter;

                int width = constraintSize.Width;

                if (width > 0)
                {
                    if (panel.IsSubPanel == false)
                        width = ViewRect.Width;

                    if (CanShowRowHeader(panel) == true)
                        width -= panel.RowHeaderWidth;

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

                Size borderSize = style.GetBorderSize(true);

                if (constraintSize.Width > 0)
                    width -= borderSize.Width;

                Size size = GetTextSize(layoutInfo, style, width);

                switch (imageAlignment)
                {
                    case Alignment.MiddleCenter:
                        sizeNeeded.Height = Math.Max(imageSize.Height, size.Height) + borderSize.Height;
                        sizeNeeded.Width = Math.Max(size.Width, imageSize.Width) + borderSize.Width;
                        break;

                    case Alignment.TopCenter:
                    case Alignment.BottomCenter:
                        sizeNeeded.Width = Math.Max(size.Width, imageSize.Width) + borderSize.Width;
                        sizeNeeded.Height = size.Height + imageSize.Height + borderSize.Height;
                        break;

                    default:
                        sizeNeeded.Width = size.Width + imageSize.Width + borderSize.Width;
                        sizeNeeded.Height = Math.Max(imageSize.Height, size.Height) + borderSize.Height;
                        break;
                }

                if (RowHeight > 0)
                    sizeNeeded.Height = RowHeight;

                sizeNeeded.Height += 1;
            }

            return (sizeNeeded);
        }

        #endregion

        #region GetTextSize

        private Size GetTextSize(
            GridLayoutInfo layoutInfo, TextRowVisualStyle style, int width)
        {
            Size size = Size.Empty;

            if (string.IsNullOrEmpty(_Text) == false)
            {
                if (_TextMarkup != null)
                {
                    size = GetMarkupTextSize(layoutInfo, style, width);
                }
                else
                {
                    eTextFormat tf = style.GetTextFormatFlags();

                    if (width <= 0)
                    {
                        if (style.AllowWrap == Tbool.True)
                            tf &= ~eTextFormat.WordBreak;
                    }

                    size = TextHelper.MeasureText(
                        layoutInfo.Graphics, _Text, style.Font, new Size(width, 0), tf);
                }
            }

            return (size);
        }

        #region GetMarkupTextSize

        private Size GetMarkupTextSize(
            GridLayoutInfo layoutInfo, TextRowVisualStyle style, int width)
        {
            Graphics g = layoutInfo.Graphics;

            MarkupDrawContext d =
                new MarkupDrawContext(g, style.Font, style.TextColor, false);

            _TextMarkup.InvalidateElementsSize();
            _TextMarkup.Measure(new Size(width, 0), d);

            return (_TextMarkup.Bounds.Size);
        }

        #endregion

        #endregion

        #endregion

        #region GetSizingStyle

        protected TextRowVisualStyle GetSizingStyle(GridPanel panel)
        {
            StyleType style = panel.GetSizingStyle();

            if (style == StyleType.NotSet)
                style = StyleType.Default;

            return (GetEffectiveStyle(style));
        }

        #endregion

        #region ArrangeOverride

        /// <summary>
        /// Performs the arrange pass layout of the item
        /// when final position and size of the item has been set
        /// </summary>
        /// <param name="layoutInfo">Layout information.</param>
        /// <param name="stateInfo"></param>
        /// <param name="layoutBounds">Layout bounds</param>
        protected override void ArrangeOverride(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, Rectangle layoutBounds)
        {
            Size = ArrangeRow(layoutInfo, stateInfo, layoutBounds);
        }

        protected virtual Size ArrangeRow(GridLayoutInfo layoutInfo,
            GridLayoutStateInfo stateInfo, Rectangle layoutBounds)
        {
            Size size = Size;

            size.Width = stateInfo.GridPanel.ColumnHeader.BoundsRelative.Width;

            return (size);
        }

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
                Rectangle bounds = Bounds;

                if (bounds.Height > 0)
                {
                    Rectangle r = bounds;
                    r.Inflate(2, 2);

                    if (r.IntersectsWith(renderInfo.ClipRectangle))
                        RenderRow(renderInfo, panel, bounds);
                }
            }
        }

        #region RenderRow

        protected virtual void RenderRow(
            GridRenderInfo renderInfo, GridPanel panel, Rectangle r)
        {
            Graphics g = renderInfo.Graphics;

            TextRowVisualStyle style = GetEffectiveStyle();
            GridPanelVisualStyle pstyle = panel.GetEffectiveStyle();

            Image image = style.GetImage(panel);

            Rectangle tb, ib;
            r = GetItemBounds(panel, image, style, r, out tb, out ib);

            if (SuperGrid.DoPreRenderTextRowEvent(g, this, RenderParts.Background, r) == false)
            {
                RenderRowBackground(g, style, r);
                SuperGrid.DoPostRenderTextRowEvent(g, this, RenderParts.Background, r);
            }

            RenderBorder(g, panel, pstyle, r);

            if (SuperGrid.DoPreRenderTextRowEvent(g, this, RenderParts.Border, r) == false)
            {
                RenderRowBorder(g, style, r);
                SuperGrid.DoPostRenderTextRowEvent(g, this, RenderParts.Border, r);
            }

            if (style.ImageOverlay != ImageOverlay.Top)
                RenderRowImage(g, panel, style, ib);

            if (SuperGrid.DoPreRenderTextRowEvent(g, this, RenderParts.Content, r) == false)
            {
                RenderRowText(g, style, tb);
                SuperGrid.DoPostRenderTextRowEvent(g, this, RenderParts.Content, tb);
            }

            if (style.ImageOverlay == ImageOverlay.Top)
                RenderRowImage(g, panel, style, ib);

            if (SuperGrid.DoPreRenderTextRowEvent(g, this, RenderParts.RowHeader, r) == false)
            {
                RenderRowHeader(g, panel, pstyle, r);
                SuperGrid.DoPostRenderTextRowEvent(g, this, RenderParts.RowHeader, r);
            }
        }

        #region GetItemBounds

        private Rectangle GetItemBounds(GridPanel panel, Image image,
            TextRowVisualStyle style, Rectangle r, out Rectangle tb, out Rectangle ib)
        {
            Rectangle v = ViewRect;

            if (panel.IsSubPanel == false)
            {
                r.X = Math.Max(r.X, v.X);
                r.Width = v.Width;
            }

            if (CanShowRowHeader(panel) == true)
            {
                r.X += panel.RowHeaderWidth;
                r.Width -= panel.RowHeaderWidth;
            }

            tb = GetAdjustedBounds(style, style.GetNonImageBounds(image, r));
            ib = style.GetImageBounds(image, tb);

            return (r);
        }

        #endregion

        #region GetAdjustedBounds

        private Rectangle GetAdjustedBounds(TextRowVisualStyle style, Rectangle r)
        {
            r.X += (style.BorderThickness.Left + style.Margin.Left + style.Padding.Left);
            r.Width -= (style.BorderThickness.Horizontal + style.Margin.Horizontal + style.Padding.Horizontal + 1);

            r.Y += (style.BorderThickness.Top + style.Margin.Top + style.Padding.Top);
            r.Height -= (style.BorderThickness.Vertical + style.Margin.Vertical + style.Padding.Vertical + 1);

            return (r);
        }

        #endregion

        #region RenderRowBackground

        protected void RenderRowBackground(
            Graphics g, TextRowVisualStyle style, Rectangle r)
        {
            r.Width--;
            r.Height--;

            using (Brush br = style.Background.GetBrush(r))
                g.FillRectangle(br, r);

            if (_BackgroundImage != null)
            {
                Rectangle sr = r;
                sr.Location = Point.Empty;

                switch (_BackgroundImageLayout)
                {
                    case GridBackgroundImageLayout.TopRight:
                        OffsetRight(ref sr, ref r);
                        g.DrawImage(_BackgroundImage, r, sr, GraphicsUnit.Pixel);
                        break;

                    case GridBackgroundImageLayout.BottomLeft:
                        OffsetBottom(ref sr, ref r);
                        g.DrawImage(_BackgroundImage, r, sr, GraphicsUnit.Pixel);
                        break;

                    case GridBackgroundImageLayout.BottomRight:
                        OffsetRight(ref sr, ref r);
                        OffsetBottom(ref sr, ref r);
                        g.DrawImage(_BackgroundImage, r, sr, GraphicsUnit.Pixel);
                        break;

                    case GridBackgroundImageLayout.Center:
                        RenderImageCentered(g, _BackgroundImage, r);
                        break;

                    case GridBackgroundImageLayout.Stretch:
                        g.DrawImage(_BackgroundImage, r);
                        break;

                    case GridBackgroundImageLayout.Zoom:
                        RenderImageScaled(g, _BackgroundImage, r);
                        break;

                    case GridBackgroundImageLayout.Tile:
                        using (TextureBrush tbr = new TextureBrush(_BackgroundImage))
                        {
                            tbr.TranslateTransform(r.X, r.Y);
                            g.FillRectangle(tbr, r);
                        }
                        break;

                    default:
                        g.DrawImage(_BackgroundImage, r, sr, GraphicsUnit.Pixel);
                        break;
                }
            }
        }

        #region OffsetRight

        private void OffsetRight(ref Rectangle sr, ref Rectangle r)
        {
            if (_BackgroundImage.Width > r.Width)
                sr.X += (_BackgroundImage.Width - r.Width);
            else
                r.X += (r.Width - _BackgroundImage.Width);
        }

        #endregion

        #region OffsetBottom

        private void OffsetBottom(ref Rectangle sr, ref Rectangle r)
        {
            if (_BackgroundImage.Height > r.Height)
                sr.Y += (_BackgroundImage.Height - r.Height);
            else
                r.Y += (r.Height - _BackgroundImage.Height);
        }

        #endregion

        #region RenderImageCentered

        private void RenderImageCentered(Graphics g, Image image, Rectangle r)
        {
            Rectangle sr = r;
            sr.Location = Point.Empty;

            if (image.Width > r.Width)
                sr.X += (image.Width - r.Width) / 2;
            else
                r.X += (r.Width - image.Width) / 2;

            if (image.Height > r.Height)
                sr.Y += (image.Height - r.Height) / 2;
            else
                r.Y += (r.Height - image.Height) / 2;

            g.DrawImage(image, r, sr, GraphicsUnit.Pixel);
        }

        #endregion

        #region RenderImageScaled

        private void RenderImageScaled(Graphics g, Image image, Rectangle r)
        {
            SizeF size = new SizeF(image.Width / image.HorizontalResolution,
                image.Height / image.VerticalResolution);

            float scale = Math.Min(r.Width / size.Width, r.Height / size.Height);

            size.Width *= scale;
            size.Height *= scale;

            g.DrawImage(image, r.X + (r.Width - size.Width) / 2,
                r.Y + (r.Height - size.Height) / 2,
                size.Width, size.Height);
        }

        #endregion

        #endregion

        #region RenderBorder

        protected virtual void RenderBorder(Graphics g,
            GridPanel panel, GridPanelVisualStyle pstyle, Rectangle r)
        {
            using (Pen pen = new Pen(pstyle.HeaderLineColor))
            {
                g.DrawLine(pen, r.X, r.Y - 1, r.Right - 1, r.Y - 1);
                g.DrawLine(pen, r.X, r.Bottom - 1, r.Right - 1, r.Bottom - 1);
            }
        }

        #endregion

        #region RenderRowBorder

        protected void RenderRowBorder(Graphics g, TextRowVisualStyle style, Rectangle r)
        {
            if (style.BorderPattern == null ||
                style.BorderThickness == null || style.BorderColor == null)
            {
                return;
            }

            Rectangle t = r;
            t.X += style.Margin.Left;
            t.Width -= style.Margin.Horizontal;

            t.Y += style.Margin.Top;
            t.Height -= style.Margin.Vertical;

            if (style.BorderColor.IsUniform == true &&
                style.BorderThickness.IsUniform == true && style.BorderPattern.IsUniform == true)
            {
                if (style.BorderThickness.Top > 0 && style.BorderColor.Top.IsEmpty == false)
                {
                    t.Width -= style.BorderThickness.Top;
                    t.Height -= style.BorderThickness.Top;

                    using (Pen pen = new
                        Pen(style.BorderColor.Top, style.BorderThickness.Top))
                    {
                        LinePattern pattern = (style.BorderPattern.Top == LinePattern.NotSet)
                            ? LinePattern.Solid : style.BorderPattern.Top;

                        pen.DashStyle = (DashStyle)pattern;

                        g.DrawRectangle(pen, t);
                    }
                }
            }
            else
            {
                Pen[] pens = style.GetBorderPens();

                if (pens[(int)BorderSide.Right] != null)
                {
                    int right = t.Right - (style.BorderThickness.Right / 2);

                    g.DrawLine(pens[(int)BorderSide.Right], right, t.Top, right, t.Bottom);
                }

                if (pens[(int)BorderSide.Bottom] != null)
                {
                    int bottom = t.Bottom - ((style.BorderThickness.Bottom - 1) / 2);

                    g.DrawLine(pens[(int)BorderSide.Bottom], t.X, bottom, t.Right, bottom);
                }

                if (pens[(int)BorderSide.Left] != null)
                    g.DrawLine(pens[(int)BorderSide.Left], t.X, t.Top, t.X, t.Bottom);

                if (pens[(int)BorderSide.Top] != null)
                    g.DrawLine(pens[(int)BorderSide.Top], t.X, t.Top, t.Right, t.Top);

                foreach (Pen pen in pens)
                {
                    if (pen != null)
                        pen.Dispose();
                }
            }
        }

        #endregion

        #region RenderRowText

        protected void RenderRowText(Graphics g,
            TextRowVisualStyle style, Rectangle r)
        {
            string s = _Text;

            if (string.IsNullOrEmpty(s) == false)
            {
                if (r.Width > 0 && r.Height > 0)
                {
                    if (_TextMarkup != null)
                    {
                        RenderTextMarkup(g, style, r);
                    }
                    else
                    {
                        eTextFormat tf = style.GetTextFormatFlags();

                        TextDrawing.DrawString(g, s, style.Font, style.TextColor, r, tf);
                    }
                }
            }
        }

        #region RenderTextMarkup

        private void RenderTextMarkup(
            Graphics g, TextRowVisualStyle style, Rectangle r)
        {
            MarkupDrawContext d =
                new MarkupDrawContext(g, style.Font, style.TextColor, false);

            _TextMarkup.Arrange(new Rectangle(r.Location, r.Size), d);

            Size size = _TextMarkup.Bounds.Size;

            switch (style.Alignment)
            {
                case Alignment.MiddleLeft:
                case Alignment.MiddleCenter:
                case Alignment.MiddleRight:
                    if (r.Height > size.Height)
                        r.Y += (r.Height - size.Height)/2;
                    break;

                default:
                    if (r.Height > size.Height)
                        r.Y = r.Bottom - size.Height;
                    break;
            }

            _TextMarkup.Bounds = new Rectangle(r.Location, size);

            Region oldClip = g.Clip;

            try
            {
                g.SetClip(r, CombineMode.Intersect);

                _TextMarkup.Render(d);
            }
            finally
            {
                g.Clip = oldClip;
            }
        }

        #endregion

        #endregion

        #region RenderRowImage

        private void RenderRowImage(Graphics g,
            GridPanel panel, TextRowVisualStyle style, Rectangle r)
        {
            if (r.Width > 0 && r.Height > 0)
            {
                Image image = style.GetImage(panel);

                if (image != null)
                    g.DrawImageUnscaledAndClipped(image, r);
            }
        }

        #endregion

        #region RenderRowHeader

        protected virtual void RenderRowHeader(Graphics g,
            GridPanel panel, GridPanelVisualStyle pstyle, Rectangle r)
        {
            if (CanShowRowHeader(panel) == true)
            {
                r.X -= panel.RowHeaderWidth;
                r.Width = panel.RowHeaderWidth - 1;

                TextRowVisualStyle style = GetEffectiveRowHeaderStyle();

                using (Brush br = style.RowHeaderStyle.Background.GetBrush(r))
                    g.FillRectangle(br, r);

                using (Pen pen = new Pen(style.RowHeaderStyle.BorderHighlightColor))
                {
                    g.DrawLine(pen, r.X, r.Y, r.Right, r.Y);
                    g.DrawLine(pen, r.X, r.Y, r.X, r.Bottom);
                }

                using (Pen pen = new Pen(pstyle.HeaderLineColor))
                {
                    g.DrawLine(pen, r.X, r.Y - 1, r.Right, r.Y - 1);
                    g.DrawLine(pen, r.X, r.Bottom - 1, r.Right, r.Bottom - 1);
                    g.DrawLine(pen, r.Right, r.Y, r.Right, r.Bottom - 1);
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region Mouse events

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
            _HitArea = HeaderArea.NoWhere;

            InvalidateRender();

            base.InternalMouseLeave(e);
        }

        #endregion

        #region InternalMouseMove

        internal override void InternalMouseMove(MouseEventArgs e)
        {
            SuperGrid.GridCursor = Cursors.Default;

            HeaderArea area = GetHitArea(e.Location);

            if (_HitArea != area)
            {
                _HitArea = area;

                InvalidateRender();
            }

            if (AllowSelection == true)
            {
                if (_TextMarkup != null)
                    _TextMarkup.MouseMove(SuperGrid, e);
            }

            base.InternalMouseMove(e);
        }

        #endregion

        #region InternalMouseDown

        internal override void InternalMouseDown(MouseEventArgs e)
        {
            _MouseDownHitArea = _HitArea;

            base.InternalMouseDown(e);
        }

        #endregion

        #region InternalMouseUp

        internal override void InternalMouseUp(MouseEventArgs e)
        {
            if (AllowSelection == true)
            {
                if (_HitArea == _MouseDownHitArea)
                {
                    switch (_HitArea)
                    {
                        case HeaderArea.InRowHeader:
                            SuperGrid.DoTextRowHeaderClickEvent(this, e);
                            break;

                        case HeaderArea.InContent:
                            SuperGrid.DoTextRowClickEvent(this, e);
                            break;

                        case HeaderArea.InMarkup:
                            SuperGrid.DoTextRowClickEvent(this, e);

                            if (_TextMarkup != null)
                                _TextMarkup.Click(SuperGrid);

                            break;
                    }
                }
            }

            base.InternalMouseUp(e);
        }

        #endregion

        #endregion

        #region CanShowRowHeader

        protected virtual bool CanShowRowHeader(GridPanel panel)
        {
            switch (_RowHeaderVisibility)
            {
                case RowHeaderVisibility.Always:
                    return (true);

                case RowHeaderVisibility.PanelControlled:
                    return (panel.ShowRowHeaders);
            }

            return (false);
        }

        #endregion

        #region GetHitArea

        ///<summary>
        /// GetHitArea
        ///</summary>
        ///<param name="pt"></param>
        ///<returns></returns>
        public virtual HeaderArea GetHitArea(Point pt)
        {
            GridPanel panel = GridPanel;

            if (panel != null)
            {
                if (CanShowRowHeader(panel) == true)
                {
                    Rectangle r = GetRowHeaderBounds(panel);

                    if (r.Contains(pt) == true)
                        return (HeaderArea.InRowHeader);
                }
            }

            if (_TextMarkup != null)
            {
                if (_TextMarkup.MouseOverElement != null)
                    return (HeaderArea.InMarkup);
            }

            return (HeaderArea.InContent);
        }

        #endregion

        #region GetRowHeaderBounds

        private Rectangle GetRowHeaderBounds(GridPanel panel)
        {
            if (panel.ShowRowHeaders == true)
            {
                Rectangle r = BoundsRelative;
                r.Location = panel.PointToScroll(r.Location);
                r.Width = panel.RowHeaderWidth + 2;

                return (r);
            }

            return (Rectangle.Empty);
        }

        #endregion

        #region Style support routines

        #region GetEffectiveStyle

        internal TextRowVisualStyle GetEffectiveStyle()
        {
            ValidateRowStyle();

            StyleState rowState = GetRowState();

            switch (rowState)
            {
                case StyleState.MouseOver:
                    return (GetStyleEx(StyleType.MouseOver));

                case StyleState.Selected:
                    return (GetStyleEx(StyleType.Selected));

                case StyleState.Selected | StyleState.MouseOver:
                    return (GetStyleEx(StyleType.SelectedMouseOver));

                case StyleState.ReadOnly:
                    return (GetStyleEx(StyleType.ReadOnly));

                case StyleState.ReadOnly | StyleState.MouseOver:
                    return (GetStyleEx(StyleType.ReadOnlyMouseOver));

                case StyleState.ReadOnly | StyleState.Selected:
                    return (GetStyleEx(StyleType.ReadOnlySelected));

                case StyleState.ReadOnly | StyleState.MouseOver | StyleState.Selected:
                    return (GetStyleEx(StyleType.ReadOnlySelectedMouseOver));

                default:
                    return (GetStyleEx(StyleType.Default));
            }
        }

        internal TextRowVisualStyle GetEffectiveStyle(StyleType type)
        {
            ValidateRowStyle();

            return (GetStyleEx(type));
        }

        #region GetRowState

        private StyleState GetRowState()
        {
            StyleState rowState = StyleState.Default;

            if (IsMouseOver == true)
                rowState |= StyleState.MouseOver;

            GridPanel panel = GridPanel;

            if (panel != null)
            {
                if (panel == SuperGrid.ActiveGrid)
                    rowState |= StyleState.Selected;

                if (panel.ReadOnly == true)
                    rowState |= StyleState.ReadOnly;
            }

            return (rowState);
        }

        #endregion

        #endregion

        #region GetEffectiveRowHeaderStyle

        internal TextRowVisualStyle GetEffectiveRowHeaderStyle()
        {
            ValidateRowStyle();

            StyleState rowState = GetRowHeaderState();

            switch (rowState)
            {
                case StyleState.MouseOver:
                    return (GetStyleEx(StyleType.MouseOver));

                case StyleState.Selected:
                    return (GetStyleEx(StyleType.Selected));

                case StyleState.Selected | StyleState.MouseOver:
                    return (GetStyleEx(StyleType.SelectedMouseOver));

                case StyleState.ReadOnly:
                    return (GetStyleEx(StyleType.ReadOnly));

                case StyleState.ReadOnly | StyleState.MouseOver:
                    return (GetStyleEx(StyleType.ReadOnlyMouseOver));

                case StyleState.ReadOnly | StyleState.Selected:
                    return (GetStyleEx(StyleType.ReadOnlySelected));

                case StyleState.ReadOnly | StyleState.MouseOver | StyleState.Selected:
                    return (GetStyleEx(StyleType.ReadOnlySelectedMouseOver));

                default:
                    return (GetStyleEx(StyleType.Default));
            }
        }

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

        #endregion

        #region GetStyleEx

        private TextRowVisualStyle GetStyleEx(StyleType e)
        {
            if (_EffectiveRowStyles.IsValid(e) == false)
            {
                TextRowVisualStyle style = GetNewVisualStyle();

                StyleType[] css = style.GetApplyStyleTypes(e);

                if (css != null)
                    ApplyStyleEx(style, css);

                SuperGrid.DoGetTextRowStyleEvent(this, e, ref style);

                if (style.Background == null || style.Background.IsEmpty == true)
                    style.Background = new Background(Color.White);

                if (style.Font == null)
                    style.Font = SystemFonts.CaptionFont;

                _EffectiveRowStyles[e] = style;
            }

            return (_EffectiveRowStyles[e]);
        }

        #region GetNewVisualStyle

        protected virtual TextRowVisualStyle GetNewVisualStyle()
        {
            return (new TextRowVisualStyle());
        }

        #endregion

        #region ApplyStyleEx

        protected virtual void ApplyStyleEx(TextRowVisualStyle style, StyleType[] css)
        {
        }

        #endregion

        #region ValidateRowStyle

        private void ValidateRowStyle()
        {
            if (_EffectiveRowStyles == null ||
                (_StyleUpdateCount != SuperGrid.StyleUpdateCount))
            {
                _EffectiveRowStyles = new TextRowVisualStyles();

                _StyleUpdateCount = SuperGrid.StyleUpdateCount;
            }
        }

        #endregion

        #endregion

        #endregion

        #region InvalidateRender

        public override void InvalidateRender()
        {
            if (SuperGrid != null)
                InvalidateRender(Bounds);
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            string s = base.ToString();

            if (String.IsNullOrEmpty(_Text) == false)
                s += ": (\"" + _Text + "\")";

            return (s);
        }

        #endregion

        #region Markup support

        private void MarkupTextChanged()
        {
            _TextMarkup = null;

            if (_EnableMarkup == true)
            {
                if (MarkupParser.IsMarkup(_Text) == true)
                {
                    _TextMarkup = MarkupParser.Parse(_Text);

                    if (_TextMarkup != null)
                        _TextMarkup.HyperLinkClick += TextMarkupLinkClick;
                }
            }
        }

        /// <summary>
        /// Occurs when a text markup link is clicked
        /// </summary>
        protected virtual void TextMarkupLinkClick(object sender, EventArgs e)
        {
            HyperLink link = sender as HyperLink;

            SuperGrid.DoTextRowMarkupLinkClickEvent(this, link);
        }

        /// <summary>
        /// Gets plain text without text-markup (if text-markup is used in Text)
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string PlainText
        {
            get { return (_TextMarkup != null ? _TextMarkup.PlainText : Text); }
        }

        #endregion
    }

    #region enums

    #region GridBackgroundImageLayout

    ///<summary>
    /// Background Image Layout
    ///</summary>
    public enum GridBackgroundImageLayout
    {
        ///<summary>
        /// Image is Top Left aligned and unscaled
        ///</summary>
        TopLeft,

        ///<summary>
        /// Image is Top Right aligned and unscaled
        ///</summary>
        TopRight,

        ///<summary>
        /// Image is Bottom Left aligned and unscaled
        ///</summary>
        BottomLeft,

        ///<summary>
        /// Image is Bottom Right aligned and unscaled
        ///</summary>
        BottomRight,

        ///<summary>
        /// Image is Centered and unscaled
        ///</summary>
        Center,

        ///<summary>
        /// Image is Stretched to fill the area
        ///</summary>
        Stretch,

        ///<summary>
        /// Image is unscaled and tiled
        ///</summary>
        Tile,

        ///<summary>
        /// Image is proportionally scaled to fit
        ///</summary>
        Zoom,
    }

    #endregion

    #region RowHeaderVisibility

    ///<summary>
    /// RowHeaderVisibility
    ///</summary>
    public enum RowHeaderVisibility
    {
        ///<summary>
        /// Always visible
        ///</summary>
        Always,

        ///<summary>
        /// Never visible
        ///</summary>
        Never,

        ///<summary>
        /// Controlled via Panel.ShowRowHeaders
        ///</summary>
        PanelControlled,
    }

    #endregion

    #endregion
}
