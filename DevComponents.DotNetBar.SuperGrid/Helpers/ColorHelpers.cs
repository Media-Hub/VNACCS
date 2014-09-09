using System;
using System.Drawing;

namespace DevComponents.DotNetBar.SuperGrid
{
    /// <summary>
    /// Provides helpers when working with colors.
    /// </summary>
    internal static class ColorHelpers
    {
        /// <summary>
        /// Converts hex string to Color type.
        /// </summary>
        /// <param name="rgbHex">Hexadecimal color representation.</param>
        /// <returns>Reference to Color object.</returns>
        public static Color GetColor(string rgbHex)
        {
            if (string.IsNullOrEmpty(rgbHex))
                return Color.Empty;

            if (rgbHex.Length == 8)
                return Color.FromArgb(Convert.ToInt32(rgbHex.Substring(0, 2), 16),
                                      Convert.ToInt32(rgbHex.Substring(2, 2), 16),
                                      Convert.ToInt32(rgbHex.Substring(4, 2), 16),
                                      Convert.ToInt32(rgbHex.Substring(6, 2), 16));

            return Color.FromArgb(Convert.ToInt32(rgbHex.Substring(0, 2), 16),
                                  Convert.ToInt32(rgbHex.Substring(2, 2), 16),
                                  Convert.ToInt32(rgbHex.Substring(4, 2), 16));
        }

        /// <summary>
        /// Converts hex string to Color type.
        /// </summary>
        /// <param name="rgb">Color representation as 32-bit RGB value.</param>
        /// <returns>Reference to Color object.</returns>
        public static Color GetColor(int rgb)
        {
            if (rgb == -1) 
                return Color.Empty;

            return Color.FromArgb((rgb & 0xFF0000) >> 16, (rgb & 0xFF00) >> 8, rgb & 0xFF);
        }

        /// <summary>
        /// Converts hex string to Color type.
        /// </summary>
        /// <param name="alpha">Alpha component for color.</param>
        /// <param name="rgb">Color representation as 32-bit RGB value.</param>
        /// <returns>Reference to Color object.</returns>
        public static Color GetColor(int alpha, int rgb)
        {
            if (rgb == -1) 
                return Color.Empty;

            return Color.FromArgb(alpha, (rgb & 0xFF0000) >> 16, (rgb & 0xFF00) >> 8, rgb & 0xFF);
        }
    }
}
