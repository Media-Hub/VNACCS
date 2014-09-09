using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DevComponents.DotNetBar.Rendering
{
    /// <summary>
    /// Specifies colors for StepIndicator control.
    /// </summary>
    public class StepIndicatorColorTable
    {
        /// <summary>
        /// Initializes a new instance of the StepIndicatorColorTable class.
        /// </summary>
        public StepIndicatorColorTable()
        {
            
        }
        /// <summary>
        /// Initializes a new instance of the StepIndicatorColorTable class.
        /// </summary>
        /// <param name="backgroundColor"></param>
        /// <param name="indicatorColor"></param>
        public StepIndicatorColorTable(Color backgroundColor, Color indicatorColor)
        {
            BackgroundColor = backgroundColor;
            IndicatorColor = indicatorColor;
        }
        /// <summary>
        /// Gets or sets Background color of the control.
        /// </summary>
        public Color BackgroundColor = Color.Empty;
        /// <summary>
        /// Gets or sets indicator color.
        /// </summary>
        public Color IndicatorColor = Color.Empty;
    }
}
