namespace Naccs.Net
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate void MailNetworkErrorEventHandler(IMailClient sender, string key, MailExceptionEx ex);
}

