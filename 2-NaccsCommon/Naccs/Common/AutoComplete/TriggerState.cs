namespace Naccs.Common.AutoComplete
{
    using System;

    [Serializable]
    public enum TriggerState
    {
        None,
        Show,
        ShowAndConsume,
        Hide,
        HideAndConsume,
        Select,
        SelectAndConsume
    }
}

