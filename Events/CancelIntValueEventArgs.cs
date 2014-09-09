using System;
using System.Text;
using System.ComponentModel;

namespace DevComponents.DotNetBar
{
    /// <summary>
    /// Represents the cancelable event arguments with integer value.
    /// </summary>
    public class CancelIntValueEventArgs : CancelEventArgs
    {
        /// <summary>
        /// Gets or sets the new value that will be used if event is not canceled.
        /// </summary>
        public int NewValue = 0;
        /// <summary>
        /// Indicates the source of the event.
        /// </summary>
        public eEventSource EventSource = eEventSource.Code;
    }

    /// <summary>
    /// Defines delegate for cancelable events.
    /// </summary>
    public delegate void CancelIntValueEventHandler(object sender, CancelIntValueEventArgs e);
}
