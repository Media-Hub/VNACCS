using System;
using System.Text;
using System.Drawing;

namespace DevComponents.DotNetBar
{
    /// <summary>
    /// Provides data for ButtonItem rendering.
    /// </summary>
    public class ButtonItemRendererEventArgs
    {
        /// <summary>
        /// Gets or sets Graphics object group is rendered on.
        /// </summary>
        public Graphics Graphics = null;
        /// <summary>
        /// Gets the reference to ButtonItem instance being rendered.
        /// </summary>
        public ButtonItem ButtonItem = null;

        /// <summary>
        /// Reference to internal data.
        /// </summary>
        internal ItemPaintArgs ItemPaintArgs = null;

        /// <summary>
        /// Creates new instance of the object.
        /// </summary>
        public ButtonItemRendererEventArgs() { }

        /// <summary>
        /// Creates new instance of the object and initializes it with default values
        /// </summary>
        /// <param name="g">Reference to Graphics object.</param>
        /// <param name="button">Reference to ButtonItem object.</param>
        public ButtonItemRendererEventArgs(Graphics g, ButtonItem button)
        {
            this.Graphics = g;
            this.ButtonItem = button;
        }

        /// <summary>
        /// Creates new instance of the object and initializes it with default values
        /// </summary>
        /// <param name="g">Reference to Graphics object.</param>
        /// <param name="button">Reference to ButtonItem object.</param>
        internal ButtonItemRendererEventArgs(Graphics g, ButtonItem button, ItemPaintArgs pa)
        {
            this.Graphics = g;
            this.ButtonItem = button;
            this.ItemPaintArgs = pa;
        }
    }
}
