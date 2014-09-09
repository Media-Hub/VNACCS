using System.ComponentModel;
using System.Drawing;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    ///<summary>
    /// GridPanelVisualStyle
    ///</summary>
    [TypeConverter(typeof(VisualStylesConverter))]
    public class GridPanelVisualStyle : VisualStyle
    {
        #region Private variables

        private Tbool _AllowWrap = Tbool.NotSet;
        private Alignment _Alignment = Alignment.NotSet;

        private Color _HeaderLineColor = Color.Empty;
        private Color _HorizontalLineColor = Color.Empty;
        private Color _VerticalLineColor = Color.Empty;
        private Color _TreeLineColor = Color.Empty;

        private TreeButtonVisualStyle _CircleTreeButtonStyle;
        private TreeButtonVisualStyle _SquareTreeButtonStyle;
        private TreeButtonVisualStyle _TriangleTreeButtonStyle;

        private LinePattern _HeaderHLinePattern = LinePattern.NotSet;
        private LinePattern _HeaderVLinePattern = LinePattern.NotSet;
        private LinePattern _HorizontalLinePattern = LinePattern.NotSet;
        private LinePattern _VerticalLinePattern = LinePattern.NotSet;
        private LinePattern _TreeLinePattern = LinePattern.NotSet;
        
        #endregion

        #region Hidden properties

        #region BorderThickness

        ///<summary>
        /// Margin
        ///</summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Thickness BorderThickness
        {
            get { return (base.BorderThickness); }
            set { base.BorderThickness = value; }
        }

        #endregion

        #region Margin

        ///<summary>
        /// Margin
        ///</summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Margin
        {
            get { return (base.Margin); }
            set { base.Margin = value; }
        }

        #endregion

        #region Padding

        ///<summary>
        /// Padding
        ///</summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Padding
        {
            get { return (base.Padding); }
            set { base.Padding = value; }
        }

        #endregion

        #endregion

        #region Public properties

        #region Alignment

        /// <summary>
        /// Gets or sets the alignment of the NoRowText within the panel
        /// </summary>
        [DefaultValue(Alignment.NotSet), Category("Appearance")]
        [Description("Indicates the alignment of the NoRowText within the panel.")]
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

        #region CircleTreeButtonStyle

        /// <summary>
        /// Gets or sets the Circle TreeButton Style
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates the Circle TreeButton Style.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TreeButtonVisualStyle CircleTreeButtonStyle
        {
            get
            {
                if (_CircleTreeButtonStyle == null)
                {
                    _CircleTreeButtonStyle = TreeButtonVisualStyle.Empty;

                    UpdateChangeHandler(null, _CircleTreeButtonStyle);
                }

                return (_CircleTreeButtonStyle);
            }

            set
            {
                if (_CircleTreeButtonStyle != value)
                {
                    UpdateChangeHandler(_CircleTreeButtonStyle, value);

                    _CircleTreeButtonStyle = value;

                    OnPropertyChangedEx("CircleTreeButtonStyle", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region HeaderLineColor

        /// <summary>
        /// Gets or sets the Header Line Color
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates the Header Line Color.")]
        public Color HeaderLineColor
        {
            get { return (_HeaderLineColor); }

            set
            {
                if (_HeaderLineColor != value)
                {
                    _HeaderLineColor = value;

                    OnPropertyChangedEx("HeaderLineColor", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeHeaderLineColor()
        {
            return (_HeaderLineColor.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetHeaderLineColor()
        {
            _HeaderLineColor = Color.Empty;
        }

        #endregion

        #region HeaderHLinePattern

        /// <summary>
        /// Gets or sets the Header Horizontal Line pattern
        /// </summary>
        [Category("Appearance"), DefaultValue(LinePattern.NotSet)]
        [Description("Indicates the Header Line pattern.")]
        public LinePattern HeaderHLinePattern
        {
            get { return (_HeaderHLinePattern); }

            set
            {
                if (_HeaderHLinePattern != value)
                {
                    _HeaderHLinePattern = value;

                    OnPropertyChangedEx("HeaderHLinePattern", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region HeaderVLinePattern

        /// <summary>
        /// Gets or sets the Header Vertical Line pattern
        /// </summary>
        [Category("Appearance"), DefaultValue(LinePattern.NotSet)]
        [Description("Indicates the Header Line pattern.")]
        public LinePattern HeaderVLinePattern
        {
            get { return (_HeaderVLinePattern); }

            set
            {
                if (_HeaderVLinePattern != value)
                {
                    _HeaderVLinePattern = value;

                    OnPropertyChangedEx("HeaderVLinePattern", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region HorizontalLineColor

        /// <summary>
        /// Gets or sets the Horizontal Line Color
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates the Horizontal Line Color.")]
        public Color HorizontalLineColor
        {
            get { return (_HorizontalLineColor); }

            set
            {
                if (_HorizontalLineColor != value)
                {
                    _HorizontalLineColor = value;

                    OnPropertyChangedEx("HorizontalLineColor", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeHorizontalLineColor()
        {
            return (_HorizontalLineColor.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetHorizontalLineColor()
        {
            _HorizontalLineColor = Color.Empty;
        }

        #endregion

        #region HorizontalLinePattern

        /// <summary>
        /// Gets or sets the Horizontal Line pattern
        /// </summary>
        [Category("Appearance"), DefaultValue(LinePattern.NotSet)]
        [Description("Indicates the Horizontal Line pattern.")]
        public LinePattern HorizontalLinePattern
        {
            get { return (_HorizontalLinePattern); }

            set
            {
                if (_HorizontalLinePattern != value)
                {
                    _HorizontalLinePattern = value;

                    OnPropertyChangedEx("HorizontalLinePattern", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region SquareTreeButtonStyle

        /// <summary>
        /// Gets or sets the Square TreeButton Style
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates the Square TreeButton Style.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TreeButtonVisualStyle SquareTreeButtonStyle
        {
            get
            {
                if (_SquareTreeButtonStyle == null)
                {
                    _SquareTreeButtonStyle = TreeButtonVisualStyle.Empty;

                    UpdateChangeHandler(null, _SquareTreeButtonStyle);
                }

                return (_SquareTreeButtonStyle);
            }

            set
            {
                if (_SquareTreeButtonStyle != value)
                {
                    UpdateChangeHandler(_SquareTreeButtonStyle, value);

                    _SquareTreeButtonStyle = value;

                    OnPropertyChangedEx("SquareTreeButtonStyle", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region TreeLineColor

        /// <summary>
        /// Gets or sets the Tree Line Color
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates the Tree Line Color.")]
        public Color TreeLineColor
        {
            get { return (_TreeLineColor); }

            set
            {
                if (_TreeLineColor != value)
                {
                    _TreeLineColor = value;

                    OnPropertyChangedEx("TreeLineColor", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeTreeLineColor()
        {
            return (_TreeLineColor.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetTreeLineColor()
        {
            _TreeLineColor = Color.Empty;
        }

        #endregion

        #region TreeLinePattern

        /// <summary>
        /// Gets or sets the Tree Line pattern
        /// </summary>
        [Category("Appearance"), DefaultValue(LinePattern.NotSet)]
        [Description("Indicates the Tree Line pattern.")]
        public LinePattern TreeLinePattern
        {
            get { return (_TreeLinePattern); }

            set
            {
                if (_TreeLinePattern != value)
                {
                    _TreeLinePattern = value;

                    OnPropertyChangedEx("TreeLinePattern", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region TriangleTreeButtonStyle

        /// <summary>
        /// Gets or sets the Triangle TreeButton Style
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates the Triangle TreeButton Style.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TreeButtonVisualStyle TriangleTreeButtonStyle
        {
            get
            {
                if (_TriangleTreeButtonStyle == null)
                {
                    _TriangleTreeButtonStyle = TreeButtonVisualStyle.Empty;

                    UpdateChangeHandler(null, _TriangleTreeButtonStyle);
                }

                return (_TriangleTreeButtonStyle);
            }

            set
            {
                if (_TriangleTreeButtonStyle != value)
                {
                    UpdateChangeHandler(_TriangleTreeButtonStyle, value);

                    _TriangleTreeButtonStyle = value;

                    OnPropertyChangedEx("TriangleTreeButtonStyle", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region VerticalLineColor

        /// <summary>
        /// Gets or sets the Vertical Line color
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates the Vertical Line Color.")]
        public Color VerticalLineColor
        {
            get { return (_VerticalLineColor); }

            set
            {
                if (_VerticalLineColor != value)
                {
                    _VerticalLineColor = value;

                    OnPropertyChangedEx("VerticalLineColor", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeVerticalLineColor()
        {
            return (_VerticalLineColor.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetVerticalLineColor()
        {
            _VerticalLineColor = Color.Empty;
        }

        #endregion

        #region VerticalLinePattern

        /// <summary>
        /// Gets or sets the Vertical Line pattern
        /// </summary>
        [Category("Appearance"), DefaultValue(LinePattern.NotSet)]
        [Description("Indicates the Vertical Line pattern.")]
        public LinePattern VerticalLinePattern
        {
            get { return (_VerticalLinePattern); }

            set
            {
                if (_VerticalLinePattern != value)
                {
                    _VerticalLinePattern = value;

                    OnPropertyChangedEx("VerticalLinePattern", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #endregion

        #region ApplyStyle

        /// <summary>
        /// Applies the style to instance of this style.
        /// </summary>
        /// <param name="style">Style to apply.</param>
        public void ApplyStyle(GridPanelVisualStyle style)
        {
            if (style != null)
            {
                base.ApplyStyle(style);

                if (style.Alignment != Alignment.NotSet)
                    _Alignment = style.Alignment;

                if (style.AllowWrap != Tbool.NotSet)
                    _AllowWrap = style.AllowWrap;

                if (style._CircleTreeButtonStyle != null)
                    CircleTreeButtonStyle.ApplyStyle(style._CircleTreeButtonStyle);

                if (style.HeaderLineColor.IsEmpty == false)
                    _HeaderLineColor = style.HeaderLineColor;

                if (style.TreeLineColor.IsEmpty == false)
                    _TreeLineColor = style.TreeLineColor;

                if (style.HorizontalLineColor.IsEmpty == false)
                    _HorizontalLineColor = style.HorizontalLineColor;

                if (style.VerticalLineColor.IsEmpty == false)
                    _VerticalLineColor = style.VerticalLineColor;

                if (style.TreeLinePattern != LinePattern.NotSet)
                    TreeLinePattern = style.TreeLinePattern;

                if (style.HorizontalLinePattern != LinePattern.NotSet)
                    HorizontalLinePattern = style.HorizontalLinePattern;

                if (style._SquareTreeButtonStyle != null)
                    SquareTreeButtonStyle.ApplyStyle(style._SquareTreeButtonStyle);

                if (style._TriangleTreeButtonStyle != null)
                    TriangleTreeButtonStyle.ApplyStyle(style._TriangleTreeButtonStyle);
                
                if (style.VerticalLinePattern != LinePattern.NotSet)
                    VerticalLinePattern = style.VerticalLinePattern;

                if (style.HeaderHLinePattern != LinePattern.NotSet)
                    HeaderHLinePattern = style.HeaderHLinePattern;

                if (style.HeaderVLinePattern != LinePattern.NotSet)
                    HeaderVLinePattern = style.HeaderVLinePattern;
            }
        }

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

        #region Copy

        /// <summary>
        /// Returns the copy of the style.
        /// </summary>
        /// <returns>Copy of the style.</returns>
        public new GridPanelVisualStyle Copy()
        {
            GridPanelVisualStyle style = new GridPanelVisualStyle();

            CopyTo(style);

            return (style);
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// Returns the copy of the style.
        /// </summary>
        /// <returns>Copy of the style.</returns>
        public void CopyTo(GridPanelVisualStyle copy)
        {
            base.CopyTo(copy);

            copy.Alignment = _Alignment;
            copy.AllowWrap = _AllowWrap;

            copy.HeaderLineColor = _HeaderLineColor;
            copy.TreeLineColor = _TreeLineColor;
            copy.HorizontalLineColor = _HorizontalLineColor;
            copy.VerticalLineColor = _VerticalLineColor;

            copy.TreeLinePattern = _TreeLinePattern;
            copy.HorizontalLinePattern = _HorizontalLinePattern;
            copy.VerticalLinePattern = _VerticalLinePattern;
            copy.HeaderHLinePattern = _HeaderHLinePattern;
            copy.HeaderVLinePattern = _HeaderVLinePattern;

            if (_CircleTreeButtonStyle != null)
                copy.CircleTreeButtonStyle = _CircleTreeButtonStyle.Copy();

            if (_SquareTreeButtonStyle != null)
                copy.SquareTreeButtonStyle = _SquareTreeButtonStyle.Copy();

            if (_TriangleTreeButtonStyle != null)
                copy.TriangleTreeButtonStyle = _TriangleTreeButtonStyle.Copy();
        }

        #endregion
    }
}
