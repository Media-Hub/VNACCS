namespace Naccs.Common.Function
{
    using System;

    public class LogEventArgs : EventArgs
    {
        public string description;
        public object sender;
        public string title;

        public LogEventArgs(object sender, string title, string description)
        {
            this.sender = sender;
            this.title = title;
            this.description = description;
        }
    }
}

