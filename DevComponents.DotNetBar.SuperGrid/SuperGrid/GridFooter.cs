using DevComponents.DotNetBar.SuperGrid.Style;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// GridFooter
    ///</summary>
    public class GridFooter : GridTextRow
    {
        #region Constructors

        ///<summary>
        /// GridFooter
        ///</summary>
        public GridFooter()
            : this(null)
        {
        }

        ///<summary>
        /// GridFooter
        ///</summary>
        ///<param name="text"></param>
        public GridFooter(string text)
            : base(text)
        {
        }

        #endregion

        #region Style support

        protected override void ApplyStyleEx(TextRowVisualStyle style, StyleType[] css)
        {
            foreach (StyleType cs in css)
            {
                style.ApplyStyle(SuperGrid.BaseVisualStyles.FooterStyles[cs]);
                style.ApplyStyle(SuperGrid.DefaultVisualStyles.FooterStyles[cs]);
                style.ApplyStyle(GridPanel.DefaultVisualStyles.FooterStyles[cs]);
            }
        }

        #endregion
    }
}
