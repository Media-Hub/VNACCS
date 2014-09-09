namespace Naccs.Net
{
    using System;

    public enum HttpCause
    {
        None,
        WebException,
        Exception,
        Timeout,
        UserCancel,
        NotSupport,
        ProgramError
    }
}

