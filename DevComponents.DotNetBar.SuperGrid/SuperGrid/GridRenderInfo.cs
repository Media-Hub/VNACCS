using System.Drawing;

namespace DevComponents.DotNetBar.SuperGrid
{
    /// <summary>
    /// Provides the rendering information for grid render pass.
    /// </summary>
    public class GridRenderInfo
    {
        #region Public properties

        /// <summary>
        /// Gets or sets the reference to Graphics object used for rendering.
        /// </summary>
        public readonly Graphics Graphics;

        /// <summary>
        /// Gets or sets the clip rectangle.
        /// </summary>
        public readonly Rectangle ClipRectangle;

        /// <summary>
        /// Gets or sets whether right-to-left layout is in effect.
        /// </summary>
        public bool RightToLeft;

        #endregion

        /// <summary>
        /// Initializes a new instance of the GridRenderInfo class.
        /// </summary>
        /// <param name="graphics">Graphics object</param>
        /// <param name="clipRectangle">Control clip rectangle</param>
        public GridRenderInfo(Graphics graphics, Rectangle clipRectangle)
        {
            Graphics = graphics;
            ClipRectangle = clipRectangle;
        }
    }
}
