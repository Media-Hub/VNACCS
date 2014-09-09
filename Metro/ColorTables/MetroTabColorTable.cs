using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DevComponents.DotNetBar.Metro.ColorTables
{
    public class MetroTabColorTable
    {
        /// <summary>
        /// Gets or sets the color table for MetroTabStrip.
        /// </summary>
        public MetroTabStripColorTable TabStrip = new MetroTabStripColorTable();

        /// <summary>
        /// Gets or sets tab panel background style.
        /// </summary>
        public ElementStyle TabPanelBackgroundStyle = new ElementStyle();

        /// <summary>
        /// Gets or sets the color table for MetroTabItem.
        /// </summary>
        public MetroTabItemColorTable MetroTabItem = new MetroTabItemColorTable();

        /// <summary>
        /// Gets or sets the color of the active form caption text displayed on metro strip.
        /// </summary>
        public Color ActiveCaptionText;
        /// <summary>
        /// Gets or sets the color of the inactive form caption text displayed on metro strip.
        /// </summary>
        public Color InactiveCaptionText;
        /// <summary>
        /// Gets or sets the text formatting for caption text.
        /// </summary>
        public eTextFormat CaptionTextFormat = eTextFormat.VerticalCenter | eTextFormat.Left | eTextFormat.EndEllipsis | eTextFormat.NoPrefix;
    }
}
