using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DevComponents.DotNetBar.Metro.ColorTables
{
    /// <summary>
    /// Represents the MetroAppForm color table.
    /// </summary>
    public class MetroAppFormColorTable
    {
        /// <summary>
        /// Gets or sets the border thickness.
        /// </summary>
        public Thickness BorderThickness = new Thickness(0, 0, 1, 1);
        /// <summary>
        /// Gets or sets the border thickness for form when it is running on Windows without Glass effect enabled.
        /// </summary>
        public Thickness BorderPlainThickness = new Thickness(1);
        /// <summary>
        /// Gets or sets the border colors.
        /// </summary>
        public BorderColors BorderColors = new BorderColors(ColorScheme.GetColor("FF6B10"));
        /// <summary>
        /// Gets or sets the inactive form border colors.
        /// </summary>
        public BorderColors BorderColorsInactive = new BorderColors();
    }

    /// <summary>
    /// Represents the MetroForm color table.
    /// </summary>
    public class MetroFormColorTable
    {
        /// <summary>
        /// Gets or sets the border thickness.
        /// </summary>
        public Thickness BorderThickness = new Thickness(0, 0, 1, 1);
        /// <summary>
        /// Gets or sets the border thickness for form when it is running on Windows without Glass effect enabled.
        /// </summary>
        public Thickness BorderPlainThickness = new Thickness(1);
        /// <summary>
        /// Gets or sets the border colors.
        /// </summary>
        public BorderColors[] BorderColors = new BorderColors[1] { new BorderColors(ColorScheme.GetColor("FF6B10")) };
        /// <summary>
        /// Gets or sets the inactive form border colors.
        /// </summary>
        public BorderColors[] BorderColorsInactive = null;
    }
}
