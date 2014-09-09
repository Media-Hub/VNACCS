using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DevComponents.DotNetBar.Metro.ColorTables
{
    public class MetroStatusBarColorTable
    {
        /// <summary>
        /// Gets or sets the color array for the top-border lines.
        /// </summary>
        public Color[] TopBorders;
        /// <summary>
        /// Gets or sets the color array for the bottom-border lines.
        /// </summary>
        public Color[] BottomBorders;
        /// <summary>
        /// Gets or sets status bar background style.
        /// </summary>
        public ElementStyle BackgroundStyle = null;
        /// <summary>
        /// Gets or sets the resize handle marker light color.
        /// </summary>
        public Color ResizeMarkerLightColor;
        /// <summary>
        /// Gets or sets the resize handle marker color.
        /// </summary>
        public Color ResizeMarkerColor;
    }
}
