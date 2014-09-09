using System;
using System.Text;

#if AdvTree
namespace DevComponents.Tree.TextMarkup
#elif DOTNETBAR
namespace DevComponents.UI.ContentManager
#elif SUPERGRID
namespace DevComponents.SuperGrid.TextMarkup
#endif

{
    /// <summary>
    /// Represents a extended content block interface for advanced layout information.
    /// </summary>
    public interface IBlockExtended : IBlock
    {
        bool IsBlockElement { get;}
        bool IsNewLineAfterElement { get;}
        bool CanStartNewLine { get; }
        /// <summary>
        /// Returns whether element is an container so it receives full available size of parent control for layout.
        /// </summary>
        bool IsBlockContainer { get; }
    }
}
