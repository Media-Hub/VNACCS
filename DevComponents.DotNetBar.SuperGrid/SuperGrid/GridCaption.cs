using System.ComponentModel;
using System.Drawing;
using DevComponents.DotNetBar.SuperGrid.Style;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// GridCaption
    ///</summary>
    public class GridCaption : GridTextRow
    {
        #region Constructors

        ///<summary>
        /// GridCaption
        ///</summary>
        public GridCaption()
            : this(null)
        {
        }

        ///<summary>
        /// GridCaption
        ///</summary>
        ///<param name="text"></param>
        public GridCaption(string text)
            : base(text)
        {
        }

        #endregion

        #region Hidden properties

        #region RowHeaderVisibility

        /// <summary>
        /// RowHeaderVisibility
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new RowHeaderVisibility RowHeaderVisibility
        {
            get { return (base.RowHeaderVisibility); }
        }

        #endregion

        #endregion

        #region RenderBorder

        protected override void RenderBorder(Graphics g,
            GridPanel panel, GridPanelVisualStyle pstyle, Rectangle r)
        {
            using (Pen pen = new Pen(pstyle.HeaderLineColor))
            {
                r.Height--;
                g.DrawLine(pen, r.X, r.Bottom, r.Right - 1, r.Bottom);
            }
        }

        #endregion

        #region CanShowRowHeader

        protected override bool CanShowRowHeader(GridPanel panel)
        {
            return (false);
        }

        #endregion

        #region Style support

        protected override void ApplyStyleEx(TextRowVisualStyle style, StyleType[] css)
        {
            foreach (StyleType cs in css)
            {
                style.ApplyStyle(SuperGrid.BaseVisualStyles.CaptionStyles[cs]);
                style.ApplyStyle(SuperGrid.DefaultVisualStyles.CaptionStyles[cs]);
                style.ApplyStyle(GridPanel.DefaultVisualStyles.CaptionStyles[cs]);
            }
        }

        #endregion
    }
}
