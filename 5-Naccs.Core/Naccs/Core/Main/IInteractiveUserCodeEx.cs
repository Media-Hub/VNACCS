namespace Naccs.Core.Main
{
    using System;

    public interface IInteractiveUserCodeEx : IInteractiveUserCode
    {
        event OnDeleteEventHandler OnDeleteList;
    }
}

