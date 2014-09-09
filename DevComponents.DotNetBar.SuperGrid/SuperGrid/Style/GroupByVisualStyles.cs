using System.ComponentModel;
using System.Drawing;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    ///<summary>
    /// FilterRowVisualStyles
    ///</summary>
    [TypeConverter(typeof(VisualStylesConverter))]
    public class GroupByVisualStyles : VisualStyles<GroupByVisualStyle>
    {
    }

    ///<summary>
    /// FilterRowVisualStyle
    ///</summary>
    [TypeConverter(typeof(VisualStylesConverter))]
    public class GroupByVisualStyle : TextRowVisualStyle
    {
        #region Private variables

        private Color _GroupBoxBorderColor = Color.Empty;
        private Color _GroupBoxConnectorColor = Color.Empty;
        private Color _GroupBoxTextColor = Color.Empty;
        private Background _GroupBoxBackground;

        private Color _WatermarkTextColor = Color.Empty;
        private Font _WatermarkFont;

        private Background _InsertMarkerBackground;
        private Color _InsertMarkerBorderColor = Color.Empty;

        #endregion

        #region Public properties

        #region GroupBoxBorderColor

        /// <summary>
        /// Gets or sets the GroupBox border color
        /// </summary>
        [Description("Indicates the GroupBox border color")]
        public Color GroupBoxBorderColor
        {
            get { return (_GroupBoxBorderColor); }

            set
            {
                if (_GroupBoxBorderColor != value)
                {
                    _GroupBoxBorderColor = value;

                    OnPropertyChangedEx("GroupBoxBorderColor", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeGroupBoxBorderColor()
        {
            return (_GroupBoxBorderColor.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetGroupBoxBorderColor()
        {
            _GroupBoxBorderColor = Color.Empty;
        }

        #endregion

        #region GroupBoxConnectorColor

        /// <summary>
        /// Gets or sets the GroupBox connector color
        /// </summary>
        [Description("Indicates the GroupBox connector color")]
        public Color GroupBoxConnectorColor
        {
            get { return (_GroupBoxConnectorColor); }

            set
            {
                if (_GroupBoxConnectorColor != value)
                {
                    _GroupBoxConnectorColor = value;

                    OnPropertyChangedEx("GroupBoxConnectorColor", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeGroupBoxConnectorColor()
        {
            return (_GroupBoxBorderColor.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetGroupBoxConnectorColor()
        {
            _GroupBoxBorderColor = Color.Empty;
        }

        #endregion

        #region GroupBoxBackground

        /// <summary>
        /// Gets or sets the GroupBox indicator background
        /// </summary>
        [Description("Indicates the RowHeader indicator background.")]
        public Background GroupBoxBackground
        {
            get
            {
                if (_GroupBoxBackground == null)
                {
                    _GroupBoxBackground = Background.Empty;

                    UpdateChangeHandler(null, _GroupBoxBackground);
                }

                return (_GroupBoxBackground);
            }

            set
            {
                if (_GroupBoxBackground != value)
                {
                    UpdateChangeHandler(_GroupBoxBackground, value);

                    _GroupBoxBackground = value;

                    OnPropertyChangedEx("GroupBoxBackground", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeGroupBoxBackground()
        {
            return (_GroupBoxBackground != null &&
                _GroupBoxBackground.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetGroupBoxBackground()
        {
            GroupBoxBackground = null;
        }

        #endregion

        #region GroupBoxTextColor

        /// <summary>
        /// Gets or sets the GroupBox Text color
        /// </summary>
        [Description("Indicates the GroupBox Text color")]
        public Color GroupBoxTextColor
        {
            get { return (_GroupBoxTextColor); }

            set
            {
                if (_GroupBoxTextColor != value)
                {
                    _GroupBoxTextColor = value;

                    OnPropertyChangedEx("GroupBoxTextColor", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeGroupBoxTextColor()
        {
            return (_GroupBoxTextColor.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetGroupBoxTextColor()
        {
            _GroupBoxTextColor = Color.Empty;
        }

        #endregion

        #region InsertMarkerBackground

        /// <summary>
        /// Gets or sets the Insert Marker background color
        /// </summary>
        [Description("Indicates the Insert Marker background color")]
        public Background InsertMarkerBackground
        {
            get
            {
                if (_InsertMarkerBackground == null)
                {
                    _InsertMarkerBackground = Background.Empty;

                    UpdateChangeHandler(null, _InsertMarkerBackground);
                }

                return (_InsertMarkerBackground);
            }

            set
            {
                if (_InsertMarkerBackground != value)
                {
                    UpdateChangeHandler(_InsertMarkerBackground, value);

                    _InsertMarkerBackground = value;

                    OnPropertyChangedEx("InsertMarkerBackground", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeInsertMarkerBackground()
        {
            return (_InsertMarkerBackground != null &&
                _InsertMarkerBackground.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetInsertMarkerBackground()
        {
            InsertMarkerBackground = null;
        }

        #endregion

        #region InsertMarkerBorderColor

        /// <summary>
        /// Gets or sets the Insert Marker border color
        /// </summary>
        [Description("Indicates the Insert Marker border color")]
        public Color InsertMarkerBorderColor
        {
            get { return (_InsertMarkerBorderColor); }

            set
            {
                if (_InsertMarkerBorderColor != value)
                {
                    _InsertMarkerBorderColor = value;

                    OnPropertyChangedEx("InsertMarkerBorderColor", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeInsertMarkerBorderColor()
        {
            return (_InsertMarkerBorderColor.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetInsertMarkerBorderColor()
        {
            _InsertMarkerBorderColor = Color.Empty;
        }

        #endregion

        #region WatermarkFont

        /// <summary>
        /// Gets or sets the style Watermark Font
        /// </summary>
        [DefaultValue(null)]
        [Description("Indicates the style Watermark Font")]
        public Font WatermarkFont
        {
            get { return (_WatermarkFont); }

            set
            {
                if (_WatermarkFont != value)
                {
                    _WatermarkFont = value;

                    OnPropertyChangedEx("WatermarkFont", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region WatermarkTextColor

        /// <summary>
        /// Gets or sets the Watermark Text color
        /// </summary>
        [Description("Indicates the Watermark Text color")]
        public Color WatermarkTextColor
        {
            get { return (_WatermarkTextColor); }

            set
            {
                if (_WatermarkTextColor != value)
                {
                    _WatermarkTextColor = value;

                    OnPropertyChangedEx("WatermarkTextColor", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeWatermarkTextColor()
        {
            return (_WatermarkTextColor.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetWatermarkTextColor()
        {
            _WatermarkTextColor = Color.Empty;
        }

        #endregion

        #endregion

        #region ApplyStyle

        /// <summary>
        /// Applies the style to instance of this style.
        /// </summary>
        /// <param name="style">Style to apply.</param>
        public void ApplyStyle(GroupByVisualStyle style)
        {
            if (style != null)
            {
                base.ApplyStyle(style);

                if (style.GroupBoxBorderColor.IsEmpty == false)
                    GroupBoxBorderColor = style.GroupBoxBorderColor;

                if (style.GroupBoxConnectorColor.IsEmpty == false)
                    GroupBoxConnectorColor = style.GroupBoxConnectorColor;

                if (style.GroupBoxTextColor.IsEmpty == false)
                    GroupBoxTextColor = style.GroupBoxTextColor;

                if (style.GroupBoxBackground != null && style.GroupBoxBackground.IsEmpty == false)
                    GroupBoxBackground = style.GroupBoxBackground.Copy();

                if (style.InsertMarkerBorderColor.IsEmpty == false)
                    InsertMarkerBorderColor = style.InsertMarkerBorderColor;

                if (style.InsertMarkerBackground != null && style.InsertMarkerBackground.IsEmpty == false)
                    InsertMarkerBackground = style.InsertMarkerBackground.Copy();

                if (style.WatermarkTextColor.IsEmpty == false)
                    WatermarkTextColor = style.WatermarkTextColor;

                if (style.WatermarkFont != null)
                    WatermarkFont = style.WatermarkFont;
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
        public void CopyTo(GroupByVisualStyle copy)
        {
            base.CopyTo(copy);

            copy.GroupBoxBorderColor = _GroupBoxBorderColor;
            copy.GroupBoxTextColor = _GroupBoxTextColor;
            copy.GroupBoxConnectorColor = _GroupBoxConnectorColor;

            if (_GroupBoxBackground != null)
                copy.GroupBoxBackground = _GroupBoxBackground.Copy();

            copy.InsertMarkerBorderColor = _InsertMarkerBorderColor;

            if (_InsertMarkerBackground != null)
                copy.InsertMarkerBackground = _InsertMarkerBackground.Copy();

            copy.WatermarkTextColor = _WatermarkTextColor;

            if (_WatermarkFont != null)
                copy.Font = (Font)_WatermarkFont.Clone();
        }

        #endregion
    }
}
