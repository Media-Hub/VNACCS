using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DevComponents.DotNetBar.Metro.ColorTables
{
    /// <summary>
    /// Represents MetroTabItem color table.
    /// </summary>
    public class MetroTabItemColorTable
    {
        /// <summary>
        /// Gets or sets the default state tab colors.
        /// </summary>
        public MetroTabItemStateColorTable Default;
        /// <summary>
        /// Gets or sets the mouse over state tab colors.
        /// </summary>
        public MetroTabItemStateColorTable MouseOver;
        /// <summary>
        /// Gets or sets the selected state tab colors.
        /// </summary>
        public MetroTabItemStateColorTable Selected;
        /// <summary>
        /// Gets or sets the pressed state tab colors.
        /// </summary>
        public MetroTabItemStateColorTable Pressed;
        /// <summary>
        /// Gets or sets the disabled state tab colors.
        /// </summary>
        public MetroTabItemStateColorTable Disabled;
    }

    public class MetroTabItemStateColorTable
    {
        /// <summary>
        /// Initializes a new instance of the MetroTabItemStateColorTable class.
        /// </summary>
        /// <param name="textColor"></param>
        /// <param name="background"></param>
        public MetroTabItemStateColorTable(ElementStyle background)
        {
            Background = background;
        }

        /// <summary>
        /// Gets or sets the background style.
        /// </summary>
        public ElementStyle Background = null;
    }
}
