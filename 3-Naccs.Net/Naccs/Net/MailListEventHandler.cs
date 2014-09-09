namespace Naccs.Net
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public delegate void MailListEventHandler(IMailClient sender, List<string> list);
}

