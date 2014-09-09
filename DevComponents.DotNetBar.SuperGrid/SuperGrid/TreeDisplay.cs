using System.Drawing;
using System.Drawing.Drawing2D;
using DevComponents.DotNetBar.SuperGrid.Style;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// TreeDisplay
    ///</summary>
    public class TreeDisplay
    {
        #region RenderLines

        ///<summary>
        /// RenderTreeLines
        ///</summary>
        ///<param name="g"></param>
        ///<param name="panel"></param>
        ///<param name="bounds"></param>
        ///<param name="item"></param>
        ///<param name="indentLevel"></param>
        ///<param name="indentAmount"></param>
        ///<param name="width"></param>
        ///<param name="hasButton"></param>
        static public void RenderLines(Graphics g, GridPanel panel, Rectangle bounds,
            GridContainer item, int indentLevel, int indentAmount, int width, bool hasButton)
        {
            Rectangle r = bounds;

            GridPanelVisualStyle style = panel.GetEffectiveStyle();

            if (style.TreeLinePattern == LinePattern.Dot)
            {
                if (r.Y % 2 == 0)
                {
                    r.Y++;

                    if (r.Height % 2 != 0)
                        r.Height--;
                }
            }

            GetLineBounds(bounds, ref r, indentLevel, indentAmount, width);

            using (Pen pen = new Pen(style.TreeLineColor))
            {
                pen.DashStyle = (DashStyle)style.TreeLinePattern;

                Rectangle t = Rectangle.Empty;
                int n = 0;

                if (item is GridPanel)
                {
                    t = item.BoundsRelative;

                    t.X -= item.HScrollOffset;
                    t.Y -= item.VScrollOffset;

                    if (item.HasCheckBox == true)
                        n = panel.CheckBoxSize.Width;
                }

                if (r.Width > 0)
                    DrawDash(g, item, style, pen, r, t, n, hasButton);

                int rowIndex = item.GridIndex;

                GridContainer tRow = item;
                GridContainer nRow = tRow.GetNextItem();

                while (tRow != null)
                {
                    int tIndex = (nRow != null) ? nRow.GridIndex : -1;

                    if (r.Width > 0)
                    {
                        if (t.IsEmpty || r.Right <= t.Left)
                        {
                            if (tIndex > rowIndex)
                            {
                                if (rowIndex == 0 && item.Parent is GridPanel)
                                    DrawLower(g, style, pen, r);
                                else
                                    DrawFull(g, pen, r);
                            }
                            else
                            {
                                if (item == tRow && (indentLevel > 0 || rowIndex > 0))
                                    DrawUpper(g, pen, r);
                            }
                        }
                    }

                    tRow = tRow.Parent as GridRow;

                    if (tRow != null)
                        nRow = tRow.GetNextItem();

                    GetLineBounds(bounds, ref r, --indentLevel, indentAmount, width);
                }
            }
        }

        #endregion

        #region GetLineBounds

        static private void GetLineBounds(
            Rectangle bounds, ref Rectangle r, int indentLevel, int indentAmount, int width)
        {
            r.X = bounds.X + (indentAmount * indentLevel);
            r.Width = width;
        }

        #endregion

        #region DrawDash

        static void DrawDash(Graphics g, GridContainer item, GridPanelVisualStyle style,
            Pen pen, Rectangle r, Rectangle t, int n, bool hasButton)
        {
            int x = r.X + r.Width / 2 + 1;
            int y = r.Y + r.Height / 2;

            GridPanel ppanel = item.GetParentPanel();

            if (ppanel.CheckBoxes == true && item.ShowCheckBox == false)
            {
                r.Width += ppanel.CheckBoxSize.Width;

                if (item is GridPanel)
                    r.Width += 4;
            }

            if (t.Width > 0 && r.Right > t.Left - n)
                r.Width -= r.Right - (t.Left - n);
            
            if (r.Width > 0)
            {
                if (style.TreeLinePattern == LinePattern.Dot)
                {
                    x++;

                    if (hasButton == false)
                    {
                        if (y % 2 == 0)
                            y--;
                    }
                    else
                    {
                        y -= 2;
                    }
                }

                g.DrawLine(pen, x, y, r.Right, y);
            }
        }

        #endregion

        #region DrawUpper

        static void DrawUpper(Graphics g, Pen pen, Rectangle r)
        {
            int x = r.X + r.Width / 2;

            g.DrawLine(pen, x, r.Y, x, r.Y + r.Height / 2);
        }

        #endregion

        #region DrawLower

        static void DrawLower(Graphics g,
            GridPanelVisualStyle style, Pen pen, Rectangle r)
        {
            int x = r.X + r.Width / 2;
            int y = r.Y + r.Height / 2;

            if (style.TreeLinePattern == LinePattern.Dot)
            {
                if (y % 2 == 0)
                    y--;
            }

            g.DrawLine(pen, x, y, x, r.Bottom);
        }

        #endregion

        #region DrawFull

        static void DrawFull(Graphics g, Pen pen, Rectangle r)
        {
            g.DrawLine(pen,
                       r.X + r.Width / 2, r.Y,
                       r.X + r.Width / 2, r.Bottom);
        }

        #endregion
    }
}
