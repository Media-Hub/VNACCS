using System.Drawing;

namespace DevComponents.SuperGrid.TextMarkup
{
	/// <summary>
	/// Represents a content block interface.
	/// </summary>
	public interface IBlock
	{
		/// <summary>
		/// Gets or sets the bounds of the content block.
		/// </summary>
		Rectangle Bounds {get;set;}

		/// <summary>
		/// Gets or sets whether content block is visible.
		/// </summary>
		bool Visible {get;set;}
	}
}
