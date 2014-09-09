using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid.Style;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// EditorPanel
    ///</summary>
    [ToolboxItem(false)]
    public class EditorPanel : Control
    {
        #region Private variables

        private Size _SizeEx;
        private Point _OffsetEx;
        private GridCell _GridCell;

        #endregion

        ///<summary>
        /// Constructor
        ///</summary>
        public EditorPanel()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        #region Public properties

        #region GridCell

        ///<summary>
        /// GridCell
        ///</summary>
        public GridCell GridCell
        {
            get { return (_GridCell); }
            internal set { _GridCell = value; }
        }

        #endregion

        #endregion

        #region Internal properties

        #region OffsetEx

        internal Point OffsetEx
        {
            get { return (_OffsetEx); }
            set { _OffsetEx = value; }
        }

        #endregion

        #region SizeEx

        internal Size SizeEx
        {
            get { return (_SizeEx); }
            set { _SizeEx = value; }
        }

        #endregion

        #endregion

        #region OnPaintBackground

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (_GridCell != null)
            {
                Rectangle r = new Rectangle(_OffsetEx, _SizeEx);
                VisualStyle style = _GridCell.GetEffectiveStyle();

                if (r.IsEmpty == false)
                {
                    if (_GridCell.SuperGrid.DoPreRenderCellEvent(g, _GridCell, RenderParts.Background, r) == false)
                    {
                        using (Brush br = style.Background.GetBrush(r))
                            g.FillRectangle(br, r);

                        _GridCell.SuperGrid.DoPostRenderCellEvent(g, _GridCell, RenderParts.Background, r);
                    }
                }
            }
            else
            {
                base.OnPaintBackground(e);
            }
        }

        #endregion
    }
}
