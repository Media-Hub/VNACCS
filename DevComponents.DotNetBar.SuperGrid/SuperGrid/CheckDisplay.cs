using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// CheckDisplay
    ///</summary>
    static public class CheckDisplay
    {
        ///<summary>
        /// RenderCheckbox
        ///</summary>
        ///<param name="g"></param>
        ///<param name="bounds"></param>
        ///<param name="cstate"></param>
        ///<param name="bstate"></param>
        static public void RenderCheckbox(
            Graphics g, Rectangle bounds, CheckBoxState cstate, ButtonState bstate)
        {
            if (Application.RenderWithVisualStyles == true &&
                CheckBoxRenderer.GetGlyphSize(g, cstate) == bounds.Size)
            {
                CheckBoxRenderer.DrawCheckBox(g, bounds.Location, cstate);
            }
            else
            {
                ControlPaint.DrawCheckBox(g, bounds, bstate);
            }
        }
    }
}
