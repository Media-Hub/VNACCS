namespace Naccs.Core.DataView.Arrange
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate bool EventHandler<TEventArgs>(object sender, TEventArgs e) where TEventArgs: EventArgs;
}

