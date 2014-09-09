using System.ComponentModel;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    ///<summary>
    /// RowVisualStyles
    ///</summary>
    public class RowVisualStyles : VisualStyles<RowVisualStyle>
    {
    }

    ///<summary>
    /// RowVisualStyle
    ///</summary>
    public class RowVisualStyle : BaseVisualStyle
    {
        #region Private variables

        private Background _Background;
        private RowHeaderVisualStyle _RowHeaderStyle;

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
            return (_Background != null && Background.IsEmpty == false);
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

        #region RowHeaderStyle

        /// <summary>
        /// Gets or sets the RowHeader Style
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates the RowHeader Style.")]
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

        #endregion

        #region ApplyStyle

        /// <summary>
        /// Applies the style to instance of this style.
        /// </summary>
        /// <param name="style">Style to apply.</param>
        public void ApplyStyle(RowVisualStyle style)
        {
            if (style != null)
            {
                base.ApplyStyle(style);

                if (style.Background != null && style.Background.IsEmpty == false)
                    Background = style.Background.Copy();

                if (style._RowHeaderStyle != null)
                    RowHeaderStyle.ApplyStyle(style._RowHeaderStyle);
            }
        }

        #endregion

        #region Copy

        /// <summary>
        /// Returns the copy of the style.
        /// </summary>
        /// <returns>Copy of the style.</returns>
        public new RowVisualStyle Copy()
        {
            RowVisualStyle style = new RowVisualStyle();

            CopyTo(style);

            return (style);
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// Returns the copy of the style.
        /// </summary>
        /// <returns>Copy of the style.</returns>
        public void CopyTo(RowVisualStyle copy)
        {
            base.CopyTo(copy);

            if (_Background != null)
                copy.Background = _Background.Copy();

            if (_RowHeaderStyle != null)
                copy.RowHeaderStyle = _RowHeaderStyle.Copy();
        }

        #endregion
    }
}
