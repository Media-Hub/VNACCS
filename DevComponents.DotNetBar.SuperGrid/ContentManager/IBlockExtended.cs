namespace DevComponents.SuperGrid.TextMarkup
{
    /// <summary>
    /// Represents a extended content block interface for advanced layout information.
    /// </summary>
    public interface IBlockExtended : IBlock
    {
        /// <summary>
        /// Gets whether element is block level element.
        /// </summary>
        bool IsBlockElement { get;}

        /// <summary>
        /// Gets whether new line is required after the element.
        /// </summary>
        bool IsNewLineAfterElement { get;}

        /// <summary>
        /// Gets whether element can be on new line.
        /// </summary>
        bool CanStartNewLine { get; }
    }
}
