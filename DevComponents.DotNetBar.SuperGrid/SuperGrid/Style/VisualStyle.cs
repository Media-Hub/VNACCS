using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    /// <summary>
    /// Represents the visual style of a Row.
    /// </summary>
    [ToolboxItem(false), DesignTimeVisible(false)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class VisualStyle : BaseVisualStyle
    {
        #region Private variables

        private Background _Background;
        private BorderColor _BorderColor;
        private BorderPattern _BorderPattern;
        private Thickness _BorderThickness;

        private Color _TextColor = Color.Empty;

        private Padding _Margin;
        private Padding _Padding;

        private Font _Font;

        #endregion

        #region Public properties

        #region Background

        /// <summary>
        /// Gets or sets the style background.
        /// </summary>
        [Description("Indicates the style background")]
        public Background Background
        {
            get
            {
                if (_Background == null)
                {
                    _Background = Background.Empty;

                    UpdateChangeHandler(null, _Background);
                }

                return (_Background);
            }

            set
            {
                if (_Background != value)
                {
                    UpdateChangeHandler(_Background, value);

                    _Background = value;

                    OnPropertyChangedEx("Background", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeBackground()
        {
            return (_Background != null && _Background.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetBackground()
        {
            Background = null;
        }

        #endregion

        #region BorderColor

        /// <summary>
        /// Gets or sets the style border color.
        /// </summary>
        [Description("Indicates the style Border Color")]
        public BorderColor BorderColor
        {
            get
            {
                if (_BorderColor == null)
                {
                    _BorderColor = BorderColor.Empty;

                    UpdateChangeHandler(null, _BorderColor);
                }

                return (_BorderColor);
            }

            set
            {
                if (_BorderColor != value)
                {
                    UpdateChangeHandler(_BorderColor, value);

                    _BorderColor = value;

                    OnPropertyChangedEx("BorderColor", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeBorderColor()
        {
            return (_BorderColor != null && _BorderColor.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetBorderColor()
        {
            BorderColor = null;
        }

        #endregion

        #region BorderPattern

        /// <summary>
        /// Gets or sets the style border pattern (Solid, Dash, ...)
        /// </summary>
        [Description("Indicates the style border pattern (Solid, Dash, ...)")]
        public BorderPattern BorderPattern
        {
            get
            {
                if (_BorderPattern == null)
                {
                    _BorderPattern = BorderPattern.Empty;

                    UpdateChangeHandler(null, _BorderPattern);
                }

                return (_BorderPattern);
            }

            set
            {
                if (_BorderPattern != value)
                {
                    UpdateChangeHandler(_BorderPattern, value);

                    _BorderPattern = value;

                    OnPropertyChangedEx("BorderPattern", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeBorderPattern()
        {
            return (_BorderPattern != null && _BorderPattern.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetBorderPattern()
        {
            BorderPattern = null;
        }

        #endregion

        #region BorderThickness

        /// <summary>
        /// Gets or sets the style border thickness.
        /// </summary>
        [Description("Indicates the style border thickness")]
        public Thickness BorderThickness
        {
            get
            {
                if (_BorderThickness == null)
                {
                    _BorderThickness = Thickness.Empty;

                    UpdateChangeHandler(null, _BorderThickness);
                }

                return (_BorderThickness);
            }

            set
            {
                if (_BorderThickness != value)
                {
                    UpdateChangeHandler(_BorderThickness, value);
                    
                    _BorderThickness = value;

                    OnPropertyChangedEx("BorderThickness", VisualChangeType.Layout);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeBorderThickness()
        {
            return (_BorderThickness != null && _BorderThickness.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetBorderThickness()
        {
            BorderThickness = null;
        }

        #endregion

        #region Font

        /// <summary>
        /// Gets or sets the style Font
        /// </summary>
        [DefaultValue(null)]
        [Description("Indicates the style Font")]
        public Font Font
        {
            get { return (_Font); }

            set
            {
                if (_Font != value)
                {
                    _Font = value;

                    OnPropertyChangedEx("Font", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region IsEmpty

        /// <summary>
        /// Gets whether the style is logically Empty.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Gets whether the style is logically Empty.")]
        public override bool IsEmpty
        {
            get
            {
                return ((_Background == null || _Background.IsEmpty == true) &&
                        (_BorderColor == null || _BorderColor.IsEmpty == true) &&
                        (_BorderPattern == null || _BorderPattern.IsEmpty == true) &&
                        (_BorderThickness == null || _BorderThickness.IsEmpty == true) &&
                        (_Margin == null || _Margin.IsEmpty == true) &&
                        (_Padding == null || _Padding.IsEmpty == true) &&
                        (_Font == null) &&
                        (_TextColor.IsEmpty == true || _TextColor == Color.Black) &&

                        (base.IsEmpty == true)); }
        }

        #endregion

        #region Margin

        /// <summary>
        /// Gets or sets the spacing between the border and outside content.
        /// </summary>
        [Description("Indicates the spacing between the border and outside content")]
        public Padding Margin
        {
            get
            {
                if (_Margin == null)
                {
                    _Margin = Padding.Empty;

                    UpdateChangeHandler(null, _Margin);
                }

                return (_Margin);
            }

            set
            {
                if (_Margin != value)
                {
                    UpdateChangeHandler(_Margin, value);

                    _Margin = value;

                    OnPropertyChangedEx("Margin", VisualChangeType.Layout);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeMargin()
        {
            return (_Margin != null && _Margin.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetMargin()
        {
            Margin = null;
        }

        #endregion

        #region Padding

        /// <summary>
        /// Gets or sets spacing between the content and edges of the element.
        /// </summary>
        [Description("Indicates the spacing between the content and edges of the element")]
        public Padding Padding
        {
            get
            {
                if (_Padding == null)
                {
                    _Padding = Padding.Empty;

                    UpdateChangeHandler(null, _Padding);
                }

                return (_Padding);
            }

            set
            {
                if (_Padding != value)
                {
                    UpdateChangeHandler(_Padding, value);

                    _Padding = value;

                    OnPropertyChangedEx("Padding", VisualChangeType.Layout);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializePadding()
        {
            return (_Padding != null && _Padding.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetPadding()
        {
            Padding = null;
        }

        #endregion

        #region TextColor

        /// <summary>
        /// Gets or sets the Text color
        /// </summary>
        [Description("Indicates the Text color")]
        public Color TextColor
        {
            get { return (_TextColor); }

            set
            {
                if (_TextColor != value)
                {
                    _TextColor = value;

                    OnPropertyChangedEx("TextColor", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeTextColor()
        {
            return (_TextColor.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetTextColor()
        {
            _TextColor = Color.Empty;
        }

        #endregion

        #endregion

        #region ApplyStyle

        /// <summary>
        /// Applies the style to instance of this style.
        /// </summary>
        /// <param name="style">Style to apply.</param>
        public void ApplyStyle(VisualStyle style)
        {
            if (style != null)
            {
                base.ApplyStyle(style);

                if (style._Background != null && style._Background.IsEmpty == false)
                    Background = style._Background.Copy();

                if (style._BorderColor != null && style._BorderColor.IsEmpty == false)
                    BorderColor = style._BorderColor;

                if (style._BorderPattern != null && style._BorderPattern.IsEmpty == false)
                    BorderPattern.ApplyPattern(style._BorderPattern);

                if (style._BorderThickness != null && style._BorderThickness.IsZero == false)
                    BorderThickness = style._BorderThickness.Copy();

                if (style.Font != null)
                    Font = style.Font;

                if (style._Margin != null && style._Margin.IsEmpty == false)
                    Margin = style.Margin.Copy();

                if (style._Padding != null && style._Padding.IsEmpty == false)
                    Padding = style.Padding.Copy();

                if (style._TextColor.IsEmpty == false)
                    TextColor = style._TextColor;
            }
        }

        #endregion

        #region RenderBorder

        internal void RenderBorder(Graphics g, Rectangle r)
        {
            if (_BorderPattern == null || BorderThickness == null || BorderColor == null)
                return;

            if (BorderColor.IsUniform == true &&
                BorderThickness.IsUniform == true && BorderPattern.IsUniform == true)
            {
                if (_BorderPattern.Top != LinePattern.None  &&
                    (_BorderThickness.Top > 0 && _BorderColor.Top.IsEmpty == false))
                {
                    using (Pen pen = new
                        Pen(_BorderColor.Top, _BorderThickness.Top))
                    {
                        LinePattern pattern = (_BorderPattern.Top == LinePattern.NotSet)
                            ? LinePattern.Solid : _BorderPattern.Top;

                        if (_BorderThickness.Top == 1)
                        {
                            r.Width--;
                            r.Height--;
                        }
                        else
                        {
                            pen.Alignment = PenAlignment.Inset;
                        }

                        pen.DashStyle = (DashStyle)pattern;

                        g.DrawRectangle(pen, r);
                    }
                }
            }
            else
            {
                Pen[] pens = GetBorderPens();

                if (pens[(int) BorderSide.Right] != null)
                {
                    int right = r.Right - ((_BorderThickness.Right + 1) / 2);

                    g.DrawLine(pens[(int)BorderSide.Right], right, r.Top, right, r.Bottom);
                }

                if (pens[(int) BorderSide.Bottom] != null)
                {
                    int bottom = r.Bottom - ((_BorderThickness.Bottom + 1) / 2);

                    g.DrawLine(pens[(int)BorderSide.Bottom], r.X, bottom, r.Right, bottom);
                }

                if (pens[(int)BorderSide.Left] != null)
                {
                    int left = r.Left + (_BorderThickness.Left / 2);

                    g.DrawLine(pens[(int)BorderSide.Left], left, r.Top, left, r.Bottom);
                }

                if (pens[(int)BorderSide.Top] != null)
                {
                    int top = r.Top + (_BorderThickness.Top / 2);

                    g.DrawLine(pens[(int) BorderSide.Top], r.X, top, r.Right, top);
                }

                foreach (Pen pen in pens)
                {
                    if (pen != null)
                        pen.Dispose();
                }
            }
        }

        #endregion

        #region GetBorderSize

        internal Size GetBorderSize(bool inclPadding)
        {
            Size size = Size.Empty;

            size.Width += (BorderThickness.Horizontal + Margin.Horizontal);
            size.Height += (BorderThickness.Vertical + Margin.Vertical);

            if (inclPadding == true)
            {
                size.Width += Padding.Horizontal;
                size.Height += Padding.Vertical;
            }

            return (size);
        }

        #endregion

        #region GetBorderPens

        internal Pen[] GetBorderPens()
        {
            Pen[] pens = new Pen[4];

            pens[(int)BorderSide.Top] = GetBorderPen(pens,
                BorderColor.Top, BorderThickness.Top, BorderPattern.Top);

            pens[(int)BorderSide.Left] = GetBorderPen(pens,
                BorderColor.Left, BorderThickness.Left, BorderPattern.Left);

            pens[(int)BorderSide.Bottom] = GetBorderPen(pens,
                BorderColor.Bottom, BorderThickness.Bottom, BorderPattern.Bottom);

            pens[(int)BorderSide.Right] = GetBorderPen(pens,
                BorderColor.Right, BorderThickness.Right, BorderPattern.Right);

            return (pens);
        }

        #endregion

        #region GetBorderPen

        private Pen GetBorderPen(IEnumerable<Pen> pens,
            Color color, int width, LinePattern pattern)
        {
            if (color.IsEmpty == true ||
                pattern == LinePattern.None || width <= 0)
            {
                return (null);
            }

            foreach (Pen pen in pens)
            {
                if (pen != null)
                {
                    if (pen.Color == color && pen.Width == width &&
                        pen.DashStyle == (DashStyle)pattern)
                    {
                        return (pen);
                    }
                }
            }

            Pen npen = new Pen(color, width);

            if (pattern == LinePattern.NotSet)
                pattern = LinePattern.Solid;

            npen.DashStyle = (DashStyle)pattern;

            return (npen);
        }

        #endregion

        #region Copy

        /// <summary>
        /// Returns the copy of the style.
        /// </summary>
        /// <returns>Copy of the style.</returns>
        public new VisualStyle Copy()
        {
            VisualStyle style = new VisualStyle();

            CopyTo(style);

            return (style);
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// Returns the copy of the style.
        /// </summary>
        /// <returns>Copy of the style.</returns>
        public void CopyTo(VisualStyle copy)
        {
            base.CopyTo(copy);

            copy.TextColor = _TextColor;

            if (_Background != null)
                copy.Background = _Background.Copy();

            if (_BorderColor != null)
                copy.BorderColor = _BorderColor.Copy();

            if (_BorderPattern != null)
                copy.BorderPattern = _BorderPattern.Copy();

            if (_BorderPattern != null)
                copy.BorderPattern = _BorderPattern.Copy();

            if (_BorderThickness != null)
                copy.BorderThickness = _BorderThickness.Copy();

            if (_Font != null)
                copy.Font = (Font)_Font.Clone();

            if (_Margin != null)
                copy.Margin = _Margin.Copy();

            if (_Padding != null)
                copy.Padding = _Padding.Copy();
        }

        #endregion
    }
}
