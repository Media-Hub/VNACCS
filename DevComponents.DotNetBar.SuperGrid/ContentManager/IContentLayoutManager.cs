using System.Drawing;

namespace DevComponents.SuperGrid.TextMarkup
{
	/// <summary>
	/// Represents interface for block layout.
	/// </summary>
	public interface IContentLayout
	{
		/// <summary>
		/// Performs layout of the content block.
		/// </summary>
		/// <param name="containerBounds">Container bounds to layout content blocks in.</param>
		/// <param name="contentBlocks">Content blocks to layout.</param>
		/// <param name="blockLayout">Block layout manager that resizes the content blocks.</param>
		/// <returns>The bounds of the content blocks within the container bounds.</returns>
		Rectangle Layout(Rectangle containerBounds, IBlock[] contentBlocks, BlockLayoutManager blockLayout);
	}
}
