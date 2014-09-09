using System.Drawing;
using System.Windows.Forms;

namespace DevComponents.DotNetBar.SuperGrid
{
    internal static class DragDrop
    {
        public static bool DragStarted(Point pt1, Point pt2)
        {
            Rectangle r = new Rectangle(pt1.X, pt1.Y, 0, 0);

            r.Inflate(SystemInformation.DragSize.Width,
                SystemInformation.DragSize.Height);

            return (r.Contains(pt2.X, pt2.Y) == false);
        }
    }
}
