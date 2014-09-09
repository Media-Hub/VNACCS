namespace Naccs.Common.CustomControls
{
    using System;

    public interface IContainerAttributes : IRepetition
    {
        int Max { get; set; }

        int RepX { get; set; }

        int RepY { get; set; }

        int SpaceX { get; set; }

        int SpaceY { get; set; }
    }
}

