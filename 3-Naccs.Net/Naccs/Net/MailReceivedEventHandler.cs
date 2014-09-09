namespace Naccs.Net
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate void MailReceivedEventHandler(IMailClient sender, int id, ComParameter receivedata);
}

