using System.ComponentModel;
using System.Drawing;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    /// <summary>
    /// Represents the visual style of a Row.
    /// </summary>
    [ToolboxItem(false), DesignTimeVisible(false)]
    [TypeConverter(typeof(BlankExpandableObjectConverter))]
    public class BaseRowHeaderVisualStyle : BaseVisualStyle
    {
        #region Static data

        ///<summary>
        /// Empty
        ///</summary>
        public static BaseRowHeaderVisualStyle Empty
        {
            get { return (new BaseRowHeaderVisualStyle()); }
        }

        #endregion

        #region Private variables

        private Background _Background;
        private Color _BorderHighlightColor = Color.Empty;

        private Font _Font;
        private Color _TextColor = Color.Empty;
        private Tbool _AllowWrap = Tbool.NotSet;
        private Alignment _TextAlignment = Alignment.NotSet;

        #endregion

        #region Public properties

        #region AllowWrap

        /// <summary>
        /// Gets or sets whether text wrapping is permitted
        /// </summary>
        [DefaultValue(Tbool.NotSet), Category("Appearance")]
        [Description("Indicates whether text wrapping is permitted.")]
        public Tbool AllowWrap
        {
            get { return (_AllowWrap); }

            set
            {
                if (_AllowWrap != value)
                {
                    _AllowWrap = value;

                    OnPropertyChangedEx("AllowWrap", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region Background

        /// <summary>
        /// Gets or sets the style background.
        /// </summary>
        [Description("Indicates whether the style background")]
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

        #region BorderHighlightColor

        /// <summary>
        /// Gets or sets the border Highlight color.
        /// </summary>
        [Description("Indicates whether the border Highlight color")]
        public Color BorderHighlightColor
        {
            get { return (_BorderHighlightColor); }

            set
            {
                if (_BorderHighlightColor != value)
                {
                    _BorderHighlightColor = value;

                    OnPropertyChangedEx("BorderHighlightColor", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeBorderHighlightColor()
        {
            return (_BorderHighlightColor.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetBorderHighlightColor()
        {
            _BorderHighlightColor = Color.Empty;
        }

        #endregion

        #region Font

        /// <summary>
        /// Gets or sets the style Font.
        /// </summary>
        [DefaultValue(null)]
        [Description("Indicates the RowHeader Font")]
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

        ///<summary>
        /// IsEmpty
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEmpty
        {
            get
            {
                return ((_Background == null || _Background.IsEmpty) &&
                        _BorderHighlightColor == Color.Empty &&
                        _Font == null && TextColor == Color.Empty);
            }
        }

        #endregion

        #region TextAlignment

        /// <summary>
        /// Gets or sets the alignment of the header text
        /// </summary>
        [DefaultValue(Alignment.NotSet), Category("Appearance")]
        [Description("Indicates the alignment of the header text.")]
        public Alignment TextAlignment
        {
            get { return (_TextAlignment); }

            set
            {
                if (_TextAlignment != value)
                {
                    _TextAlignment = value;

                    OnPropertyChangedEx("TextAlignment", VisualChangeType.Render);
                }
            }
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

        #region GetTextFormatFlags

        internal eTextFormat GetTextFormatFlags()
        {
            eTextFormat tf = eTextFormat.WordEllipsis |
                eTextFormat.NoPadding | eTextFormat.NoPrefix;

            if (AllowWrap == Tbool.True)
                tf |= eTextFormat.WordBreak;

            switch (TextAlignment)
            {
                case Alignment.TopCenter:
                    tf |= eTextFormat.HorizontalCenter;
                    break;

                case Alignment.TopRight:
                    tf |= eTextFormat.Right;
                    break;

                case Alignment.MiddleLeft:
                    tf |= eTextFormat.VerticalCenter;
                    break;

                case Alignment.NotSet:
                case Alignment.MiddleCenter:
                    tf |= eTextFormat.HorizontalCenter;
                    tf |= eTextFormat.VerticalCenter;
                    break;

                case Alignment.MiddleRight:
                    tf |= eTextFormat.Right;
                    tf |= eTextFormat.VerticalCenter;
                    break;

                case Alignment.BottomLeft:
                    tf |= eTextFormat.Bottom;
                    break;

                case Alignment.BottomCenter:
                    tf |= eTextFormat.Bottom;
                    tf |= eTextFormat.HorizontalCenter;
                    break;

                case Alignment.BottomRight:
                    tf |= eTextFormat.Bottom;
                    tf |= eTextFormat.Right;
                    break;
            }

            return (tf);
        }

        #endregion

        #region ApplyStyle

        /// <summary>
        /// Applies the style to instance of this style.
        /// </summary>
        /// <param name="style">Style to apply.</param>
        public void ApplyStyle(BaseRowHeaderVisualStyle style)
        {
            if (style != null)
            {
                base.ApplyStyle(style);

                if (style.Font != null)
                    _Font = style.Font;

                if (style.TextColor.IsEmpty == false)
                    _TextColor = style._TextColor;

                if (style.Background != null && style.Background.IsEmpty == false)
                    Background = style.Background.Copy();

                if (style.BorderHighlightColor.IsEmpty == false)
                    BorderHighlightColor = style.BorderHighlightColor;

                if (style.AllowWrap != Tbool.NotSet)
                    _AllowWrap = style.AllowWrap;

                if (style.TextAlignment != Alignment.NotSet)
                    _TextAlignment = style.TextAlignment;
            }
        }

        #endregion

        #region Copy

        /// <summary>
        /// Returns the copy of the style.
        /// </summary>
        /// <returns>Copy of the style.</returns>
        public new BaseRowHeaderVisualStyle Copy()
        {
            BaseRowHeaderVisualStyle style = new BaseRowHeaderVisualStyle();

            CopyTo(style);

            return (style);
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// Returns the copy of the style.
        /// </summary>
        /// <returns>Copy of the style.</returns>
        public void CopyTo(BaseRowHeaderVisualStyle copy)
        {
            base.CopyTo(copy);

            if (_Font != null)
                copy.Font = (Font)_Font.Clone();

            if (_TextColor.IsEmpty == false)
                copy.TextColor = _TextColor;

            copy.BorderHighlightColor = BorderHighlightColor;

            if (_Background != null)
                copy.Background = _Background.Copy();

            copy.TextAlignment = _TextAlignment;
            copy.AllowWrap = _AllowWrap;
        }

        #endregion
    }
}
