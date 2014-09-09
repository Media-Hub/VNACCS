using System.Collections.Generic;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    ///<summary>
    /// VisualStylesTable
    ///</summary>
    public static class VisualStylesTable
    {
        private static Dictionary<SuperGridStyle, DefaultVisualStyles>
            _DefaultStyles = new Dictionary<SuperGridStyle,DefaultVisualStyles>();

        /// <summary>
        /// Gets the DefaultVisualStyles for specified SuperGrid style.
        /// </summary>
        /// <param name="style">SuperGridStyle to return.</param>
        /// <returns>An instance of DefaultVisualStyle.</returns>
        public static DefaultVisualStyles GetStyle(SuperGridStyle style)
        {
            DefaultVisualStyles visualStyle;

            if (!_DefaultStyles.TryGetValue(style, out visualStyle))
            {
                VisualStyleFactory factory = VisualStyleFactory.GetStyleFactory(style);

                visualStyle = factory.CreateStyle();

                if (style != SuperGridStyle.Metro) // Do not cache Metro style
                    _DefaultStyles.Add(style, visualStyle);
            }

            return visualStyle;
        }

        /// <summary>
        /// Replaces an system style with the specified visual style.
        /// </summary>
        /// <param name="style">SuperGridStyle to replace.</param>
        /// <param name="visualStyle">DefaultVisualStyles to replace the system style with.</param>
        public static void SetStyleFactory(SuperGridStyle style, DefaultVisualStyles visualStyle)
        {
            _DefaultStyles[style] = visualStyle;
        }
    }

    /// <summary>
    /// Defines available pre-defined SuperGrid visual styles.
    /// </summary>
    public enum SuperGridStyle
    {
        /// <summary>
        /// Office 2010 Blue style.
        /// </summary>
        Office2010Blue,
        /// <summary>
        /// Office 2010 Silver style.
        /// </summary>
        Office2010Silver,
        /// <summary>
        /// Office 2010 Black style.
        /// </summary>
        Office2010Black,
        /// <summary>
        /// Metro Style.
        /// </summary>
        Metro
    }
}
