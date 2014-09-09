using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DevComponents.DotNetBar.SuperGrid
{
    internal partial class CellInfoWindow : FloatWindow
    {
        #region Private variables

        private SuperGridControl _SuperGrid;
        private GridCell _Cell;

        private System.Windows.Forms.ToolTip _ToolTip;

        #endregion

        public CellInfoWindow(SuperGridControl superGrid, GridCell cell)
        {
            InitializeComponent();

            TopLevel = false;

            _SuperGrid = superGrid;
            _Cell = cell;

            _ToolTip = new System.Windows.Forms.ToolTip();

            using (GraphicsPath path = GetImageGraphicsPath())
                Region = new Region(path);

            Paint += CellInfoWindowPaint;
            MouseClick += CellInfoWindowMouseClick;
            MouseDoubleClick += CellInfoWindowMouseDoubleClick;

            MouseEnter += CellInfoWindowMouseEnter;
            MouseLeave += CellInfoWindowMouseLeave;
        }

        #region CellInfoWindowMouseEnter

        void CellInfoWindowMouseEnter(object sender, System.EventArgs e)
        {
            if (_SuperGrid.DoCellInfoEnterEvent(_Cell, MousePosition) == false)
                _ToolTip.SetToolTip(this, _Cell.InfoText);
            else 
                _ToolTip.SetToolTip(this, "");
        }

        #endregion

        #region CellInfoWindowMouseLeave

        void CellInfoWindowMouseLeave(object sender, System.EventArgs e)
        {
            _SuperGrid.DoCellInfoLeaveEvent(_Cell);
        }

        #endregion

        #region GetImageGraphicsPath

        private GraphicsPath GetImageGraphicsPath()
        {
            Bitmap bitmap = new Bitmap(_Cell.GetInfoImage());
            GraphicsPath graphicsPath = new GraphicsPath();

            Color colorTransparent = bitmap.GetPixel(0, 0);

            for (int row = 0; row < bitmap.Height; row++)
            {
                for (int col = 0; col < bitmap.Width; col++)
                {
                    if (bitmap.GetPixel(col, row) != colorTransparent)
                    {
                        int colOpaquePixel = col;
                        int colNext;

                        for (colNext = colOpaquePixel; colNext < bitmap.Width; colNext++)
                        {
                            if (bitmap.GetPixel(colNext, row) == colorTransparent)
                                break;
                        }

                        graphicsPath.AddRectangle(new Rectangle(colOpaquePixel,
                                                   row, colNext - colOpaquePixel, 1));

                        col = colNext;
                    }
                }
            }

            return (graphicsPath);
        }

        #endregion

        #region CellInfoWindowPaint

        void CellInfoWindowPaint(object sender, PaintEventArgs e)
        {
            Image image = _Cell.GetInfoImage();

            if (image != null)
            {
                Rectangle r = Bounds;
                r.Location = Point.Empty;

                e.Graphics.DrawImageUnscaledAndClipped(image, r);
            }
        }

        #endregion

        #region CellInfoWindowMouseClick

        void CellInfoWindowMouseClick(object sender, MouseEventArgs e)
        {
            _SuperGrid.DoCellInfoClickEvent(_Cell, e);
        }

        #endregion

        #region CellInfoWindowMouseDoubleClick

        void CellInfoWindowMouseDoubleClick(object sender, MouseEventArgs e)
        {
            _SuperGrid.DoCellInfoDoubleClickEvent(_Cell, e);
        }

        #endregion
    }
}

