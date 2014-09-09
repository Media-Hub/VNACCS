using System.Collections;
using System.Drawing;

namespace DevComponents.SuperGrid.TextMarkup
{
	/// <summary>
	/// Represents block layout manager responsible for sizing the content blocks.
	/// </summary>
	public abstract class BlockLayoutManager
	{
		private Graphics _Graphics;

	    /// <summary>
		/// Resizes the content block and sets it's Bounds property to reflect new size.
		/// </summary>
		/// <param name="block">Content block to resize.</param>
        /// <param name="availableSize">Content size available for the block in the given line.</param>
		public abstract void Layout(IBlock block, Size availableSize);

        /// <summary>
        /// Performs layout finalization
        /// </summary>
        /// <param name="containerBounds"></param>
        /// <param name="blocksBounds"></param>
        /// <param name="lines"></param>
        /// <returns></returns>
        public abstract Rectangle FinalizeLayout(Rectangle containerBounds, Rectangle blocksBounds, ArrayList lines);

		/// <summary>
		/// Gets or sets the graphics object used by layout manager.
		/// </summary>
		public Graphics Graphics
		{
			get {return _Graphics;}
			set {_Graphics = value;}
		}
	}
}
