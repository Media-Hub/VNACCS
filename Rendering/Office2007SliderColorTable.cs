using System;
using System.Text;

namespace DevComponents.DotNetBar.Rendering
{
    /// <summary>
    /// Defines the color table for the slider item.
    /// </summary>
    public class Office2007SliderColorTable
    {
        /// <summary>
        /// Gets or sets the default state colors.
        /// </summary>
        public Office2007SliderStateColorTable Default = new Office2007SliderStateColorTable();

        /// <summary>
        /// Gets or sets the mouse over state colors.
        /// </summary>
        public Office2007SliderStateColorTable MouseOver = new Office2007SliderStateColorTable();

        /// <summary>
        /// Gets or sets the mouse pressed colors.
        /// </summary>
        public Office2007SliderStateColorTable Pressed = new Office2007SliderStateColorTable();

        /// <summary>
        /// Gets or sets the disabled colors.
        /// </summary>
        public Office2007SliderStateColorTable Disabled = new Office2007SliderStateColorTable();

        /// <summary>
        /// Gets or sets the optional color table for Thumb part of the slider. When set to null default values from Office2007SliderColorTable are used.
        /// </summary>
        public Office2007SliderPartColorTable TrackPart = null;
        /// <summary>
        /// Gets or sets the optional color table for Increase button part of the slider. When set to null default values from Office2007SliderColorTable are used.
        /// </summary>
        public Office2007SliderPartColorTable IncreaseButtonPart = null;
        /// <summary>
        /// Gets or sets the optional color table for Decrease button part of the slider. When set to null default values from Office2007SliderColorTable are used.
        /// </summary>
        public Office2007SliderPartColorTable DecreaseButtonPart = null;
    }

    /// <summary>
    /// Defines set of color tables for single slider part.
    /// </summary>
    public class Office2007SliderPartColorTable
    {
        /// <summary>
        /// Gets or sets the default state colors.
        /// </summary>
        public Office2007SliderStateColorTable Default = new Office2007SliderStateColorTable();

        /// <summary>
        /// Gets or sets the mouse over state colors.
        /// </summary>
        public Office2007SliderStateColorTable MouseOver = new Office2007SliderStateColorTable();

        /// <summary>
        /// Gets or sets the mouse pressed colors.
        /// </summary>
        public Office2007SliderStateColorTable Pressed = new Office2007SliderStateColorTable();
    }
}
