namespace Naccs.Common.CustomControls
{
    using System;

    public interface IRepContainerAttributes : IContainerAttributes, IRepetition
    {
        int CurrentPage { get; set; }
    }
}

