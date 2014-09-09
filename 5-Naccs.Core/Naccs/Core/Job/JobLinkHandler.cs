namespace Naccs.Core.Job
{
    using Naccs.Core.Classes;
    using System;
    using System.Runtime.CompilerServices;

    public delegate void JobLinkHandler(object sender, IData data, string linkFileName, int linkNo);
}

