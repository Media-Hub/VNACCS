using System.ComponentModel;
using System.Drawing;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    ///<summary>
    /// FilterRowVisualStyles
    ///</summary>
    [TypeConverter(typeof(VisualStylesConverter))]
    public class FilterRowVisualStyles : VisualStyles<FilterRowVisualStyle>
    {
    }

    ///<summary>
    /// FilterRowVisualStyle
    ///</summary>
    [TypeConverter(typeof(VisualStylesConverter))]
    public class FilterRowVisualStyle : BaseVisualStyle
    {
        #region Private variables

        private Color _FilterBorderColor = Color.Empty;

        private Background _FilterBackground;
        private Background _WhiteSpaceBackground;

        private BaseRowHeaderVisualStyle _RowHeaderStyle;

        #endregion

        #region Public properties

        #region FilterBackground

        /// <summary>
        /// Gets or sets the filter indicator background
        /// </summary>
        [Description("Indicates the RowHeader indicator background.")]
        public Background FilterBackground
        {
            get
            {
                if (_FilterBackground == null)
                {
                    _FilterBackground = Background.Empty;

                    UpdateChangeHandler(null, _FilterBackground);
                }

                return (_FilterBackground);
            }

            set
            {
                if (_FilterBackground != value)
                {
                    UpdateChangeHandler(_FilterBackground, value);

                    _FilterBackground = value;

                    OnPropertyChangedEx("FilterBackground", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeFilterBackground()
        {
            return (_FilterBackground != null &&
                _FilterBackground.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetFilterBackground()
        {
            FilterBackground = null;
        }

        #endregion

        #region FilterBorderColor

        /// <summary>
        /// Gets or sets the Filter border color
        /// </summary>
        [Description("Indicates the Filter border color")]
        public Color FilterBorderColor
        {
            get { return (_FilterBorderColor); }

            set
            {
                if (_FilterBorderColor != value)
                {
                    _FilterBorderColor = value;

                    OnPropertyChangedEx("FilterBorderColor", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeFilterBorderColor()
        {
            return (_FilterBorderColor.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetFilterBorderColor()
        {
            _FilterBorderColor = Color.Empty;
        }

        #endregion

        #region RowHeader

        /// <summary>
        /// Gets or sets the visual style of the RowHeader
        /// </summary>
        [Description("Indicates the visual style of the RowHeader.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BaseRowHeaderVisualStyle RowHeader
        {
            get
            {
                if (_RowHeaderStyle == null)
                {
                    _RowHeaderStyle = BaseRowHeaderVisualStyle.Empty;

                    UpdateChangeHandler(null, _RowHeaderStyle);
                }

                return (_RowHeaderStyle);
            }

            set
            {
                if (_RowHeaderStyle != value)
                {
                    UpdateChangeHandler(_RowHeaderStyle, value);

                    _RowHeaderStyle = value;

                    OnPropertyChangedEx("RowHeader", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region WhiteSpaceBackground

        /// <summary>
        /// Gets or sets the WhiteSpace Background
        /// </summary>
        [Description("Indicates the WhiteSpace Background")]
        public Background WhiteSpaceBackground
        {
            get
            {
                if (_WhiteSpaceBackground == null)
                {
                    _WhiteSpaceBackground = Background.Empty;

                    UpdateChangeHandler(null, _WhiteSpaceBackground);
                }

                return (_WhiteSpaceBackground);
            }

            set
            {
                if (_WhiteSpaceBackground != value)
                {
                    UpdateChangeHandler(_WhiteSpaceBackground, value);

                    _WhiteSpaceBackground = value;

                    OnPropertyChangedEx("WhiteSpaceBackground", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeWhiteSpaceBackground()
        {
            return (_WhiteSpaceBackground != null &&
                _WhiteSpaceBackground.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetWhiteSpaceBackground()
        {
            WhiteSpaceBackground = null;
        }

        #endregion

        #endregion

        #region ApplyStyle

        /// <summary>
        /// Applies the style to instance of this style.
        /// </summary>
        /// <param name="style">Style to apply.</param>
        public void ApplyStyle(FilterRowVisualStyle style)
        {
            if (style != null)
            {
                base.ApplyStyle(style);

                RowHeader.ApplyStyle(style.RowHeader);

                if (style.FilterBackground != null && style.FilterBackground.IsEmpty == false)
                    FilterBackground = style.FilterBackground.Copy();

                if (style.FilterBorderColor.IsEmpty == false)
                    FilterBorderColor = style.FilterBorderColor;

                if (style.WhiteSpaceBackground != null && style.WhiteSpaceBackground.IsEmpty == false)
                    WhiteSpaceBackground = style.WhiteSpaceBackground.Copy();
            }
        }

        #endregion

        #region Copy

        /// <summary>
        /// Returns the copy of the style.
        /// </summary>
        /// <returns>Copy of the style.</returns>
        public new FilterRowVisualStyle Copy()
        {
            FilterRowVisualStyle copy = new FilterRowVisualStyle();

            CopyTo(copy);

            return (copy);
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// Returns the copy of the style.
        /// </summary>
        /// <returns>Copy of the style.</returns>
        public void CopyTo(FilterRowVisualStyle copy)
        {
            base.CopyTo(copy);

            if (_RowHeaderStyle != null)
                copy.RowHeader = _RowHeaderStyle.Copy();

            copy.FilterBorderColor = _FilterBorderColor;

            if (_FilterBackground != null)
                copy.FilterBackground = _FilterBackground.Copy();

            if (_WhiteSpaceBackground != null)
                copy.WhiteSpaceBackground = _WhiteSpaceBackground.Copy();
        }

        #endregion
    }
}
