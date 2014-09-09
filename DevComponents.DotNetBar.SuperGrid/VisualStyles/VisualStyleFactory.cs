using System;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    /// <summary>
    /// Represents base class that each visual style factory for SuperGridControl inherits from.
    /// </summary>
    public abstract class VisualStyleFactory
    {
        /// <summary>
        /// Create the DefaultVisualStyle for SuperGridControl.
        /// </summary>
        /// <param name="factory">Color-Factory used to generate colors.</param>
        /// <returns>New instance of DefaultVisualStyles class.</returns>
        public abstract DefaultVisualStyles CreateStyle(Rendering.ColorFactory factory);

        /// <summary>
        /// Create the DefaultVisualStyle for SuperGridControl with empty color factory.
        /// </summary>
        /// <returns>New instance of DefaultVisualStyles class.</returns>
        public virtual DefaultVisualStyles CreateStyle()
        {
            if (StyleManager.ColorTint.IsEmpty)
                return CreateStyle(Rendering.ColorFactory.Empty);

            return CreateStyle(new Rendering.ColorBlendFactory(StyleManager.ColorTint));
        }

        /// <summary>
        /// Returns the style factory for specified visual style.
        /// </summary>
        /// <param name="style">Style to create factory for.</param>
        /// <returns>An instance of VisualStyleFactory.</returns>
        public static VisualStyleFactory GetStyleFactory(SuperGridStyle style)
        {
            if (style == SuperGridStyle.Office2010Blue)
                return new Office2010BlueStyleFactory();

            if (style == SuperGridStyle.Office2010Silver)
                return new Office2010SilverStyleFactory();

            if (style == SuperGridStyle.Office2010Black)
                return new Office2010BlackStyleFactory();

            if (style == SuperGridStyle.Metro)
                return new MetroStyleFactory();

            throw new ArgumentException(string.Format(
                "Specified style '{0}' factory has not been implemented.", style));
        }
    }
}
