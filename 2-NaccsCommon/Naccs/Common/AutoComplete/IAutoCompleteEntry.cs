namespace Naccs.Common.AutoComplete
{
    using System;

    public interface IAutoCompleteEntry
    {
        string[] MatchStrings { get; }
    }
}

