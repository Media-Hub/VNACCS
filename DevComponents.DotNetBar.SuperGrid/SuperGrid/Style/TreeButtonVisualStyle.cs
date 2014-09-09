using System.ComponentModel;
using System.Drawing;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    /// <summary>
    /// Represents the visual style of a Row.
    /// </summary>
    [ToolboxItem(false), DesignTimeVisible(false)]
    [TypeConverter(typeof(BlankExpandableObjectConverter))]
    public class BaseTreeButtonVisualStyle : BaseVisualStyle
    {
        #region Static data

        ///<summary>
        /// Empty
        ///</summary>
        public static BaseTreeButtonVisualStyle Empty
        {
            get { return (new BaseTreeButtonVisualStyle()); }
        }

        #endregion

        #region Private variables

        private Color _BorderColor = Color.Empty;
        private Color _HotBorderColor = Color.Empty;
        private Color _LineColor = Color.Empty;
        private Color _HotLineColor = Color.Empty;

        private Background _Background;
        private Background _HotBackground;

        #endregion

        #region Public properties

        #region Background

        /// <summary>
        /// Gets or sets the TreeButton background.
        /// </summary>
        [Description("Indicates the TreeButton background")]
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
            _Background = null;
        }

        #endregion

        #region HotBackground

        /// <summary>
        /// Gets or sets the  Hot background.
        /// </summary>
        [Description("Indicates the  Hot background")]
        public Background HotBackground
        {
            get
            {
                if (_Background == null)
                {
                    _HotBackground = Background.Empty;

                    UpdateChangeHandler(null, _HotBackground);
                }

                return (_HotBackground);
            }

            set
            {
                if (_Background != value)
                {
                    UpdateChangeHandler(_HotBackground, value);

                    _HotBackground = value;

                    OnPropertyChangedEx("HotBackground", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeHotBackground()
        {
            return (_HotBackground != null && _HotBackground.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetHotBackground()
        {
            HotBackground = null;
        }

        #endregion

        #region BorderColor

        /// <summary>
        /// Gets or sets the border Color
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates the border Color.")]
        public Color BorderColor
        {
            get { return (_BorderColor); }

            set
            {
                if (_BorderColor != value)
                {
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
            return (_BorderColor.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetBorderColor()
        {
            BorderColor = Color.Empty;
        }

        #endregion

        #region HotBorderColor

        /// <summary>
        /// Gets or sets the Hot border Color
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates the Hot border Color.")]
        public Color HotBorderColor
        {
            get { return (_HotBorderColor); }

            set
            {
                if (_HotBorderColor != value)
                {
                    _HotBorderColor = value;

                    OnPropertyChangedEx("HotBorderColor", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeHotBorderColor()
        {
            return (_HotBorderColor.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetHotBorderColor()
        {
            HotBorderColor = Color.Empty;
        }

        #endregion

        #region LineColor

        /// <summary>
        /// Gets or sets the button interior line Color
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates the button interior line Color.")]
        public Color LineColor
        {
            get { return (_LineColor); }

            set
            {
                if (_LineColor != value)
                {
                    _LineColor = value;

                    OnPropertyChangedEx("LineColor", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeLineColor()
        {
            return (_LineColor.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetLineColor()
        {
            LineColor = Color.Empty;
        }

        #endregion

        #region HotLineColor

        /// <summary>
        /// Gets or sets the Hot button interior line Color
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates the Hot button interior line Color.")]
        public Color HotLineColor
        {
            get { return (_HotLineColor); }

            set
            {
                if (_HotLineColor != value)
                {
                    _HotLineColor = value;

                    OnPropertyChangedEx("HotLineColor", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeHotLineColor()
        {
            return (_HotLineColor.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetHotLineColor()
        {
            HotLineColor = Color.Empty;
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
                return
                   ((_Background == null || _Background.IsEmpty) &&
                    (_HotBackground == null || _HotBackground.IsEmpty) &&
                    (_BorderColor == Color.Empty && _HotBorderColor == Color.Empty));
            }
        }

        #endregion

        #endregion

        #region ApplyStyle

        /// <summary>
        /// Applies the style to instance of this style.
        /// </summary>
        /// <param name="style">Style to apply.</param>
        public void ApplyStyle(BaseTreeButtonVisualStyle style)
        {
            if (style != null)
            {
                base.ApplyStyle(style);

                if (style.BorderColor.IsEmpty == false)
                    BorderColor = style.BorderColor;

                if (style.HotBorderColor.IsEmpty == false)
                    HotBorderColor = style.HotBorderColor;

                if (style.Background != null && style.Background.IsEmpty == false)
                    Background = style.Background.Copy();

                if (style.HotBackground != null && style.HotBackground.IsEmpty == false)
                    HotBackground = style.HotBackground.Copy();

                if (style.LineColor.IsEmpty == false)
                    LineColor = style.LineColor;

                if (style.HotLineColor.IsEmpty == false)
                    HotLineColor = style.HotLineColor;
            }
        }

        #endregion

        #region Copy

        /// <summary>
        /// Returns the copy of the style.
        /// </summary>
        /// <returns>Copy of the style.</returns>
        public new BaseTreeButtonVisualStyle Copy()
        {
            BaseTreeButtonVisualStyle style = new BaseTreeButtonVisualStyle();

            CopyTo(style);

            return (style);
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// Returns the copy of the style.
        /// </summary>
        /// <returns>Copy of the style.</returns>
        public void CopyTo(BaseTreeButtonVisualStyle copy)
        {
            base.CopyTo(copy);

            copy.BorderColor = _BorderColor;
            copy.HotBorderColor = _HotBorderColor;
            copy.LineColor = _LineColor;
            copy.HotLineColor = _HotLineColor;

            if (Background != null)
                copy.Background = _Background.Copy();

            if (HotBackground != null)
                copy.HotBackground = _HotBackground.Copy();
        }

        #endregion
    }

    /// <summary>
    /// Represents the visual style of a Row.
    /// </summary>
    [ToolboxItem(false), DesignTimeVisible(false)]
    [TypeConverter(typeof(BlankExpandableObjectConverter))]
    public class TreeButtonVisualStyle : BaseVisualStyle
    {
        #region Static data

        ///<summary>
        /// Empty
        ///</summary>
        public static TreeButtonVisualStyle Empty
        {
            get { return (new TreeButtonVisualStyle()); }
        }

        #endregion

        #region Private variables

        private BaseTreeButtonVisualStyle _ExpandButton;
        private BaseTreeButtonVisualStyle _CollapseButton;

        #endregion

        #region Public properties

        #region CollapseButton

        /// <summary>
        /// Gets or sets the CollapseButton.
        /// </summary>
        [Description("Indicates the CollapseButton")]
        public BaseTreeButtonVisualStyle CollapseButton
        {
            get
            {
                if (_CollapseButton == null)
                {
                    _CollapseButton = BaseTreeButtonVisualStyle.Empty;

                    UpdateChangeHandler(null, _CollapseButton);
                }

                return (_CollapseButton);
            }

            set
            {
                if (_CollapseButton != value)
                {
                    UpdateChangeHandler(_CollapseButton, value);

                    _CollapseButton = value;

                    OnPropertyChangedEx("CollapseButton", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeCollapseButton()
        {
            return (_CollapseButton != null && _CollapseButton.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetCollapseButton()
        {
            CollapseButton = null;
        }

        #endregion

        #region ExpandButton

        /// <summary>
        /// Gets or sets the ExpandButton.
        /// </summary>
        [Description("Indicates the ExpandButton")]
        public BaseTreeButtonVisualStyle ExpandButton
        {
            get
            {
                if (_ExpandButton == null)
                {
                    _ExpandButton = BaseTreeButtonVisualStyle.Empty;

                    UpdateChangeHandler(null, _ExpandButton);
                }

                return (_ExpandButton);
            }

            set
            {
                if (_ExpandButton != value)
                {
                    UpdateChangeHandler(_ExpandButton, value);

                    _ExpandButton = value;

                    OnPropertyChangedEx("ExpandButton", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeExpandButton()
        {
            return (_ExpandButton != null && _ExpandButton.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetExpandButton()
        {
            ExpandButton = null;
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
                return
                   ((_CollapseButton == null || _CollapseButton.IsEmpty) &&
                    (_ExpandButton == null || _ExpandButton.IsEmpty));
            }
        }

        #endregion

        #endregion

        #region ApplyStyle

        /// <summary>
        /// Applies the style to instance of this style.
        /// </summary>
        /// <param name="style">Style to apply.</param>
        public void ApplyStyle(TreeButtonVisualStyle style)
        {
            if (style != null)
            {
                base.ApplyStyle(style);

                if (style._CollapseButton != null)
                    CollapseButton.ApplyStyle(style._CollapseButton);

                if (style._ExpandButton != null)
                    ExpandButton.ApplyStyle(style._ExpandButton);
            }
        }

        #endregion

        #region Copy

        /// <summary>
        /// Returns the copy of the style.
        /// </summary>
        /// <returns>Copy of the style.</returns>
        public new TreeButtonVisualStyle Copy()
        {
            TreeButtonVisualStyle style = new TreeButtonVisualStyle();

            CopyTo(style);

            return (style);
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// Returns the copy of the style.
        /// </summary>
        /// <returns>Copy of the style.</returns>
        public void CopyTo(TreeButtonVisualStyle copy)
        {
            base.CopyTo(copy);

            if (_CollapseButton != null)
                copy.CollapseButton = _CollapseButton.Copy();

            if (_ExpandButton != null)
                copy.ExpandButton = _ExpandButton.Copy();
        }

        #endregion
    }
}
