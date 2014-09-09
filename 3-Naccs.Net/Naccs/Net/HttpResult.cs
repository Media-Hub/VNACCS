namespace Naccs.Net
{
    using System;

    public enum HttpResult
    {
        None,
        UriError,
        ProxyException,
        ProxyAddressError,
        CertException,
        CertUnestablishedError,
        CertValidError,
        CertSelectError,
        StatusError,
        ProgramError
    }
}

