using System.ComponentModel;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    ///<summary>
    /// TextRowVisualStyles
    ///</summary>
    public class TextRowVisualStyles : VisualStyles<TextRowVisualStyle>
    {
        #region Hidden properties

        #region Empty

        /// <summary>
        /// Empty
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new TextRowVisualStyle Empty
        {
            get { return (base.Empty); }
        }

        #endregion

        #region NotSelectable

        /// <summary>
        /// NotSelectable
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new TextRowVisualStyle NotSelectable
        {
            get { return (base.NotSelectable); }
        }

        #endregion

        #endregion
    }

    ///<summary>
    /// TextRowVisualStyle
    ///</summary>
    public class TextRowVisualStyle : CellVisualStyle
    {
        #region Private variables

        private BaseRowHeaderVisualStyle _RowHeaderStyle;

        #endregion

        #region Public properties

        #region RowHeaderStyle

        /// <summary>
        /// Gets or sets the RowHeader Style
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates the RowHeader Style")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BaseRowHeaderVisualStyle RowHeaderStyle
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
        public void ApplyStyle(TextRowVisualStyle style)
        {
            if (style != null)
            {
                base.ApplyStyle(style);

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
        public new TextRowVisualStyle Copy()
        {
            TextRowVisualStyle copy = new TextRowVisualStyle();

            CopyTo(copy);

            return (copy);
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// Returns the copy of the style.
        /// </summary>
        /// <returns>Copy of the style.</returns>
        public void CopyTo(TextRowVisualStyle copy)
        {
            base.CopyTo(copy);

            if (_RowHeaderStyle != null)
                copy.RowHeaderStyle = _RowHeaderStyle.Copy();
        }

        #endregion
    }
}
