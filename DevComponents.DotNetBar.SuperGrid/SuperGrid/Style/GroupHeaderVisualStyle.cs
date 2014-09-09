using System.ComponentModel;
using System.Drawing;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    ///<summary>
    /// GroupHeaderVisualStyles
    ///</summary>
    public class GroupHeaderVisualStyles : VisualStyles<GroupHeaderVisualStyle>
    {
    }

    ///<summary>
    /// RowHeaderVisualStyle
    ///</summary>
    public class GroupHeaderVisualStyle : BaseVisualStyle
    {
        #region Static data

        ///<summary>
        /// Empty
        ///</summary>
        public static GroupHeaderVisualStyle Empty
        {
            get { return (new GroupHeaderVisualStyle()); }
        }

        #endregion

        #region Private variables

        private Font _Font;
        private Alignment _Alignment = Alignment.NotSet;
        private Tbool _AllowWrap = Tbool.NotSet;
        private Color _TextColor = Color.Empty;
        private Color _UnderlineColor = Color.Empty;
        private Background _Background;
        private Padding _Padding;

        private RowHeaderVisualStyle _RowHeaderStyle;

        #endregion

        #region Public properties

        #region Alignment

        /// <summary>
        /// Gets or sets the alignment of the content within the cell
        /// </summary>
        [DefaultValue(Alignment.NotSet), Category("Appearance")]
        [Description("Indicates the alignment of the content.")]
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
        [Description("Indicates whether text wrapping is permitted")]
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

        #region Font

        /// <summary>
        /// Gets or sets the style Font.
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

        #region Padding

        /// <summary>
        /// Gets or sets the spacing between the content and edges of the element
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

        #region RowHeaderStyle

        /// <summary>
        /// Gets or sets the GroupHeader RowHeader Style
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates the GroupHeader RowHeader Style")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public RowHeaderVisualStyle RowHeaderStyle
        {
            get
            {
                if (_RowHeaderStyle == null)
                {
                    _RowHeaderStyle = RowHeaderVisualStyle.Empty;

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

                    OnPropertyChangedEx("RowHeaderStyle", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region TextColor

        /// <summary>
        /// Gets or sets the Text color
        /// </summary>
        [Category("Appearance"), Description("Indicates the Text color")]
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

        #region UnderlineColor

        /// <summary>
        /// Gets or sets the text Underline Color
        /// </summary>
        [Description("Indicates the text Underline Color")]
        public Color UnderlineColor
        {
            get { return (_UnderlineColor); }

            set
            {
                if (_UnderlineColor != value)
                {
                    _UnderlineColor = value;

                    OnPropertyChangedEx("UnderlineColor", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeUnderlineColor()
        {
            return (_UnderlineColor.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetUnderlineColor()
        {
            _UnderlineColor = Color.Empty;
        }

        #endregion

        #endregion

        #region ApplyStyle

        /// <summary>
        /// Applies the style to instance of this style.
        /// </summary>
        /// <param name="style">Style to apply.</param>
        public void ApplyStyle(GroupHeaderVisualStyle style)
        {
            base.ApplyStyle(style);

            if (style.Alignment != Alignment.NotSet)
                Alignment = style.Alignment;

            if (style.AllowWrap != Tbool.NotSet)
                AllowWrap = style.AllowWrap;

            if (style._Background != null && style._Background.IsEmpty == false)
                Background = style._Background.Copy();

            if (style.Font != null)
                Font = style.Font;

            if (style._Padding != null && style._Padding.IsEmpty == false)
                Padding = style._Padding.Copy();

            if (style.TextColor.IsEmpty == false)
                TextColor = style.TextColor;

            if (style.UnderlineColor.IsEmpty == false)
                UnderlineColor = style.UnderlineColor;

            if (style._RowHeaderStyle != null)
                RowHeaderStyle.ApplyStyle(style._RowHeaderStyle);
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
        public new GroupHeaderVisualStyle Copy()
        {
            GroupHeaderVisualStyle copy = new GroupHeaderVisualStyle();

            CopyTo(copy);

            return (copy);
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// Returns the copy of the style.
        /// </summary>
        /// <returns>Copy of the style.</returns>
        public void CopyTo(GroupHeaderVisualStyle copy)
        {
            base.CopyTo(copy);

            copy.Alignment = _Alignment;
            copy.AllowWrap = _AllowWrap;

            if (_Background != null)
                copy.Background = _Background.Copy();

            if (_Font != null)
                copy.Font = (Font)_Font.Clone();

            if (_Padding != null)
                copy.Padding = _Padding.Copy();

            copy.TextColor = _TextColor;
            copy.UnderlineColor = _UnderlineColor;

            if (_RowHeaderStyle != null)
                copy.RowHeaderStyle = _RowHeaderStyle.Copy();
        }

        #endregion
    }
}
