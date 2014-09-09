using System.ComponentModel;
using System.Drawing;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    ///<summary>
    /// ColumnHeaderRowVisualStyles
    ///</summary>
    [TypeConverter(typeof(VisualStylesConverter))]
    public class ColumnHeaderRowVisualStyles : VisualStyles<ColumnHeaderRowVisualStyle>
    {
    }

    ///<summary>
    /// ColumnHeaderRowVisualStyle
    ///</summary>
    [TypeConverter(typeof(VisualStylesConverter))]
    public class ColumnHeaderRowVisualStyle : BaseVisualStyle
    {
        #region Private variables

        private Color _FilterBorderColor = Color.Empty;
        private Color _IndicatorBorderColor = Color.Empty;
        private Color _SortIndicatorColor = Color.Empty;

        private Background _FilterBackground;
        private Background _IndicatorBackground;
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

        #region IndicatorBackground

        /// <summary>
        /// Gets or sets the RowHeader indicator background
        /// </summary>
        [Description("Indicates the RowHeader indicator background.")]
        public Background IndicatorBackground
        {
            get
            {
                if (_IndicatorBackground == null)
                {
                    _IndicatorBackground = Background.Empty;

                    UpdateChangeHandler(null, _IndicatorBackground);
                }

                return (_IndicatorBackground);
            }

            set
            {
                if (_IndicatorBackground != value)
                {
                    UpdateChangeHandler(_IndicatorBackground, value);

                    _IndicatorBackground = value;

                    OnPropertyChangedEx("IndicatorBackground", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeIndicatorBackground()
        {
            return (_IndicatorBackground != null && 
                _IndicatorBackground.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetIndicatorBackground()
        {
            IndicatorBackground = null;
        }

        #endregion

        #region IndicatorBorderColor

        /// <summary>
        /// Gets or sets the RowHeader indicator border color
        /// </summary>
        [Description("Indicates the RowHeader indicator border color")]
        public Color IndicatorBorderColor
        {
            get { return (_IndicatorBorderColor); }

            set
            {
                if (_IndicatorBorderColor != value)
                {
                    _IndicatorBorderColor = value;

                    OnPropertyChangedEx("IndicatorBorderColor", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeIndicatorBorderColor()
        {
            return (_IndicatorBorderColor.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetIndicatorBorderColor()
        {
            _IndicatorBorderColor = Color.Empty;
        }

        #endregion

        #region RowHeader

        /// <summary>
        /// Gets or sets the visual style of the ColumnHeader RowHeader
        /// </summary>
        [Description("Indicates the visual style of the ColumnHeader RowHeader.")]
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

        #region SortIndicatorColor

        /// <summary>
        /// Gets or sets the Sort indicator color
        /// </summary>
        [Description("Indicates the Sort indicator color")]
        public Color SortIndicatorColor
        {
            get { return (_SortIndicatorColor); }

            set
            {
                if (_SortIndicatorColor != value)
                {
                    _SortIndicatorColor = value;

                    OnPropertyChangedEx("SortIndicatorColor", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeSortIndicatorColor()
        {
            return (_SortIndicatorColor.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetSortIndicatorColor()
        {
            _SortIndicatorColor = Color.Empty;
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
        public void ApplyStyle(ColumnHeaderRowVisualStyle style)
        {
            if (style != null)
            {
                RowHeader.ApplyStyle(style.RowHeader);

                if (style._FilterBackground != null && style._FilterBackground.IsEmpty == false)
                    FilterBackground = style._FilterBackground.Copy();

                if (style._FilterBorderColor.IsEmpty == false)
                    FilterBorderColor = style._FilterBorderColor;

                if (style._IndicatorBackground != null && style.IndicatorBackground.IsEmpty == false)
                    IndicatorBackground = style._IndicatorBackground.Copy();

                if (style._IndicatorBorderColor.IsEmpty == false)
                    IndicatorBorderColor = style._IndicatorBorderColor;

                if (style._SortIndicatorColor.IsEmpty == false)
                    SortIndicatorColor = style._SortIndicatorColor;

                if (style._WhiteSpaceBackground != null && style._WhiteSpaceBackground.IsEmpty == false)
                    WhiteSpaceBackground = style._WhiteSpaceBackground.Copy();
            }
        }

        #endregion

        #region Copy

        /// <summary>
        /// Returns the copy of the style.
        /// </summary>
        /// <returns>Copy of the style.</returns>
        public new ColumnHeaderRowVisualStyle Copy()
        {
            ColumnHeaderRowVisualStyle copy = new ColumnHeaderRowVisualStyle();

            CopyTo(copy);

            return (copy);
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// Returns the copy of the style.
        /// </summary>
        /// <returns>Copy of the style.</returns>
        public void CopyTo(ColumnHeaderRowVisualStyle copy)
        {
            base.CopyTo(copy);

            if (_RowHeaderStyle != null)
                copy.RowHeader = _RowHeaderStyle.Copy();

            if (_FilterBackground != null)
                copy.FilterBackground = _FilterBackground.Copy();

            if (_IndicatorBackground != null)
                copy.IndicatorBackground = _IndicatorBackground.Copy();

            copy.FilterBorderColor = _FilterBorderColor;
            copy.IndicatorBorderColor = _IndicatorBorderColor;
            copy.SortIndicatorColor = _SortIndicatorColor;

            if (_WhiteSpaceBackground != null)
                copy.WhiteSpaceBackground = _WhiteSpaceBackground.Copy();
        }

        #endregion
    }
}
