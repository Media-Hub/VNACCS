using System.ComponentModel;
using System.Drawing;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    ///<summary>
    /// FilterColumnHeaderVisualStyle
    ///</summary>
    public class FilterColumnHeaderVisualStyles : VisualStyles<FilterColumnHeaderVisualStyle>
    {
    }

    ///<summary>
    /// FilterRowVisualStyle
    ///</summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class FilterColumnHeaderVisualStyle : BaseVisualStyle
    {
        #region Private variables

        private Tbool _AllowWrap = Tbool.NotSet;
        private Alignment _Alignment = Alignment.NotSet;

        private Background _Background;
        private Background _ErrorBackground;
        private Background _GripBarBackground;

        private Color _TextColor = Color.Empty;
        private Color _ErrorTextColor = Color.Empty;

        private Font _Font;

        private Padding _Margin;

        #endregion

        #region Public properties

        #region Alignment

        /// <summary>
        /// Gets or sets the content alignment
        /// </summary>
        [DefaultValue(Alignment.NotSet), Category("Appearance")]
        [Description("Indicates the content alignment.")]
        public Alignment Alignment
        {
            get { return (_Alignment); }

            set
            {
                if (_Alignment != value)
                {
                    _Alignment = value;

                    OnPropertyChangedEx("Alignment", VisualChangeType.Layout);
                }
            }
        }

        #endregion

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

                    OnPropertyChangedEx("AllowWrap", VisualChangeType.Layout);
                }
            }
        }

        #endregion

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

        #region ErrorBackground

        /// <summary>
        /// Gets or sets the style error background.
        /// </summary>
        [Description("Indicates the style error background")]
        public Background ErrorBackground
        {
            get
            {
                if (_Background == null)
                {
                    _ErrorBackground = Background.Empty;

                    UpdateChangeHandler(null, _ErrorBackground);
                }

                return (_ErrorBackground);
            }

            set
            {
                if (_ErrorBackground != value)
                {
                    UpdateChangeHandler(_ErrorBackground, value);

                    _ErrorBackground = value;

                    OnPropertyChangedEx("ErrorBackground", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeErrorBackground()
        {
            return (_ErrorBackground != null && _ErrorBackground.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetErrorBackground()
        {
            ErrorBackground = null;
        }

        #endregion

        #region ErrorTextColor

        /// <summary>
        /// Gets or sets the error Text color
        /// </summary>
        [Description("Indicates the error Text color")]
        public Color ErrorTextColor
        {
            get { return (_ErrorTextColor); }

            set
            {
                if (_ErrorTextColor != value)
                {
                    _ErrorTextColor = value;

                    OnPropertyChangedEx("ErrorTextColor", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeErrorTextColor()
        {
            return (_ErrorTextColor.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetErrorTextColor()
        {
            _ErrorTextColor = Color.Empty;
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

        #region GripBarBackground

        /// <summary>
        /// Gets or sets the GripBar style background.
        /// </summary>
        [Description("Indicates the GripBar style background")]
        public Background GripBarBackground
        {
            get
            {
                if (_GripBarBackground == null)
                {
                    _GripBarBackground = Background.Empty;

                    UpdateChangeHandler(null, _GripBarBackground);
                }

                return (_GripBarBackground);
            }

            set
            {
                if (_GripBarBackground != value)
                {
                    UpdateChangeHandler(_Background, value);

                    _GripBarBackground = value;

                    OnPropertyChangedEx("GripBarBackground", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeGripBarBackground()
        {
            return (_GripBarBackground != null && _GripBarBackground.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetGripBarBackground()
        {
            GripBarBackground = null;
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

            switch (Alignment)
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
        /// Applies the style to the instance of this style.
        /// </summary>
        /// <param name="style">Style to apply.</param>
        public void ApplyStyle(FilterColumnHeaderVisualStyle style)
        {
            if (style != null)
            {
                base.ApplyStyle(style);

                if (style.Alignment != Alignment.NotSet)
                    Alignment = style.Alignment;

                if (style.AllowWrap != Tbool.NotSet)
                    AllowWrap = style.AllowWrap;

                if (style.Background != null && style.Background.IsEmpty == false)
                    Background = style.Background.Copy();

                if (style.GripBarBackground != null && style.GripBarBackground.IsEmpty == false)
                    GripBarBackground = style.GripBarBackground.Copy();

                if (style.ErrorBackground != null && style.ErrorBackground.IsEmpty == false)
                    ErrorBackground = style.ErrorBackground.Copy();

                if (style.Font != null)
                    Font = style.Font;

                if (style.ErrorTextColor.IsEmpty == false)
                    ErrorTextColor = style.ErrorTextColor;

                if (style.TextColor.IsEmpty == false)
                    TextColor = style.TextColor;

                if (style.Margin != null && style.Margin.IsEmpty == false)
                    Margin = style.Margin.Copy();
            }
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
        public void CopyTo(FilterColumnHeaderVisualStyle copy)
        {
            base.CopyTo(copy);

            copy.Alignment = _Alignment;
            copy.AllowWrap = _AllowWrap;
            copy.ErrorTextColor = _ErrorTextColor;
            copy.TextColor = _TextColor;

            if (_Background != null)
                copy.Background = _Background.Copy();

            if (_ErrorBackground != null)
                copy.ErrorBackground = _ErrorBackground.Copy();

            if (_GripBarBackground != null)
                copy.GripBarBackground = _GripBarBackground.Copy();

            if (_Font != null)
                copy.Font = _Font;

            if (_Margin != null)
                copy.Margin = _Margin.Copy();
        }

        #endregion
    }
}
