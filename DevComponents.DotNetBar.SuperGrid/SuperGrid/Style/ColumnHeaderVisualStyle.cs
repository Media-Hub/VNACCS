using System.ComponentModel;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    ///<summary>
    /// ColumnHeaderVisualStyles
    ///</summary>
    public class ColumnHeaderVisualStyles : VisualStyles<ColumnHeaderVisualStyle>
    {
        #region Hidden properties

        #region Empty

        /// <summary>
        /// Empty
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ColumnHeaderVisualStyle Empty
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
        public new ColumnHeaderVisualStyle NotSelectable
        {
            get { return (base.NotSelectable); }
        }

        #endregion

        #endregion
    }

    ///<summary>
    /// ColumnHeaderVisualStyle
    ///</summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ColumnHeaderVisualStyle : CellVisualStyle
    {
        #region Copy

        /// <summary>
        /// Returns the copy of the style.
        /// </summary>
        /// <returns>Copy of the style.</returns>
        public new ColumnHeaderVisualStyle Copy()
        {
            ColumnHeaderVisualStyle style = new ColumnHeaderVisualStyle();

            CopyTo(style);

            return (style);
        }

        #endregion
    }
}
