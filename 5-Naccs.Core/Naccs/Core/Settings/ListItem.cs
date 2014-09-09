namespace Naccs.Core.Settings
{
    using System;
    using System.Threading;

    public class ListItem
    {
        public event EventHandler OnChange;

        public void Change()
        {
            if (this.OnChange != null)
            {
                this.OnChange(this, new EventArgs());
            }
        }
    }
}

