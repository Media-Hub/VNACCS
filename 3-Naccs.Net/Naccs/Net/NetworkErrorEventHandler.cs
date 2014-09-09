namespace Naccs.Net
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate void NetworkErrorEventHandler(IHttpClient sender, string key, HttpExceptionEx ex);
}

