using System.Drawing;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// CheckDisplay
    ///</summary>
    static public class ExpandDisplay
    {
        #region RenderButton

        ///<summary>
        /// RenderButton
        ///</summary>
        ///<param name="g"></param>
        ///<param name="image"></param>
        ///<param name="buttonBounds"></param>
        ///<param name="clipBounds"></param>
        static public Size RenderButton(Graphics g, Image image,
            Rectangle buttonBounds, Rectangle clipBounds)
        {
            if (image != null && buttonBounds.IsEmpty == false)
            {
                Rectangle r = buttonBounds;
                r.Width++;
                r.Height++;

                if (r.Width > image.Width)
                {
                    r.X += (r.Width - image.Width)/2;
                    r.Width = image.Width;
                }

                if (r.Height > image.Height)
                {
                    r.Y += (r.Height - image.Height)/2;
                    r.Height = image.Height;
                }

                r.Intersect(clipBounds);

                g.DrawImageUnscaledAndClipped(image, r);

                return (r.Size);
            }

            return (Size.Empty);
        }

        #endregion
    }
}
