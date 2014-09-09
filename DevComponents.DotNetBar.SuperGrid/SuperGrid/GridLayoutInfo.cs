using System.Drawing;
using DevComponents.DotNetBar.SuperGrid.Style;

namespace DevComponents.DotNetBar.SuperGrid
{
    /// <summary>
    /// Provides the layout information for grid layout pass
    /// </summary>
    public class GridLayoutInfo
    {
        #region Public variables

        /// <summary>
        /// Gets or sets the Graphics object of the control.
        /// </summary>
        public Graphics Graphics;

        /// <summary>
        /// Gets or sets whether right-to-left layout is in effect.
        /// </summary>
        public bool RightToLeft;

        /// <summary>
        /// Gets the client bounds for the layout i.e. client bounds of SuperGridControl.
        /// </summary>
        public Rectangle ClientBounds;

        /// <summary>
        /// Gets the default visual styles for grid elements.
        /// </summary>
        public DefaultVisualStyles DefaultVisualStyles;

        #endregion

        /// <summary>
        /// Initializes a new instance of the GridLayoutInfo class.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="clientBounds"></param>
        public GridLayoutInfo(Graphics graphics, Rectangle clientBounds)
        {
            Graphics = graphics;
            ClientBounds = clientBounds;
        }
    }

    /// <summary>
    /// Provides the layout state information for grid layout pass
    /// </summary>
    public class GridLayoutStateInfo
    {
        #region Public variables

        /// <summary>
        /// Gets or sets the layout GridPanel
        /// </summary>
        public GridPanel GridPanel;

        /// <summary>
        /// Gets or sets the IndentLevel
        /// </summary>
        public int IndentLevel;

        #endregion

        internal int PassCount;

        /// <summary>
        /// Initializes a new instance of the GridLayoutStateInfo class
        /// </summary>
        public GridLayoutStateInfo(GridPanel gridPanel, int indentLevel)
        {
            GridPanel = gridPanel;
            IndentLevel = indentLevel;
        }
    }
}
