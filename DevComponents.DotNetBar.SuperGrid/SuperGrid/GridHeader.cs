using DevComponents.DotNetBar.SuperGrid.Style;

namespace DevComponents.DotNetBar.SuperGrid
{
    /// <summary>
    /// Defines the grid header
    /// </summary>
    public class GridHeader : GridTextRow
    {
        #region Constructors

        ///<summary>
        /// GridHeader
        ///</summary>
        public GridHeader()
            : this(null)
        {
        }

        ///<summary>
        /// GridHeader
        ///</summary>
        ///<param name="text"></param>
        public GridHeader(string text)
            : base(text)
        {
        }

        #endregion

        #region Style support

        protected override void ApplyStyleEx(TextRowVisualStyle style, StyleType[] css)
        {
            foreach (StyleType cs in css)
            {
                style.ApplyStyle(SuperGrid.BaseVisualStyles.HeaderStyles[cs]);
                style.ApplyStyle(SuperGrid.DefaultVisualStyles.HeaderStyles[cs]);
                style.ApplyStyle(GridPanel.DefaultVisualStyles.HeaderStyles[cs]);
            }
        }

        #endregion
    }
}
