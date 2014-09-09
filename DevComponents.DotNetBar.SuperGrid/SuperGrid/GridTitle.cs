using System.ComponentModel;
using DevComponents.DotNetBar.SuperGrid.Style;

namespace DevComponents.DotNetBar.SuperGrid
{
    /// <summary>
    /// Defines the grid title
    /// </summary>
    public class GridTitle : GridTextRow
    {
        #region Constructors

        ///<summary>
        /// GridHeader
        ///</summary>
        public GridTitle()
            : this(null)
        {
        }

        ///<summary>
        /// GridHeader
        ///</summary>
        ///<param name="text"></param>
        public GridTitle(string text)
            : base(text)
        {
            RowHeaderVisibility = RowHeaderVisibility.Never;
        }

        #endregion

        #region Public properties

        #region RowHeaderVisibility

        /// <summary>
        /// Gets or sets whether the RowHeader is displayed
        /// </summary>
        [DefaultValue(RowHeaderVisibility.Never), Category("Appearance")]
        [Description("Indicates whether the RowHeader is displayed")]
        public override RowHeaderVisibility RowHeaderVisibility
        {
            get { return (base.RowHeaderVisibility); }
            set { base.RowHeaderVisibility = value; }
        }

        #endregion

        #endregion

        #region Style support

        protected override void ApplyStyleEx(TextRowVisualStyle style, StyleType[] css)
        {
            foreach (StyleType cs in css)
            {
                style.ApplyStyle(SuperGrid.BaseVisualStyles.TitleStyles[cs]);
                style.ApplyStyle(SuperGrid.DefaultVisualStyles.TitleStyles[cs]);
                style.ApplyStyle(GridPanel.DefaultVisualStyles.TitleStyles[cs]);
            }
        }

        #endregion
    }
}
