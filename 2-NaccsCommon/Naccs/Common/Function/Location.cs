namespace Naccs.Common.Function
{
    using Naccs.Common;
    using System;

    public class Location
    {
        private string developKind;
        private int x;
        private int y;

        public Location(string developKind)
        {
            this.developKind = "";
            this.developKind = developKind;
            switch (this.developKind)
            {
                case "s":
                    this.x = 20;
                    this.y = 20;
                    return;

                case "r":
                    this.x = 2;
                    this.y = 20;
                    return;
            }
            NaccsException exception = new NaccsException(MessageKind.Error, 0x3e7, "Print classification error");
            throw exception;
        }

        public Location(string location, string developKind)
        {
            this.developKind = "";
            this.developKind = developKind;
            this.x = int.Parse(location.Split(new char[] { ',' })[0]);
            this.y = int.Parse(location.Split(new char[] { ',' })[1]);
        }

        public string getLocation()
        {
            return (this.X.ToString() + "," + this.Y.ToString());
        }

        public void setNextLocation(int itemWedth)
        {
            switch (this.developKind)
            {
                case "s":
                    this.x = (this.x + itemWedth) + 6;
                    return;

                case "r":
                    this.x = (this.x + itemWedth) + 2;
                    return;
            }
            NaccsException exception = new NaccsException(MessageKind.Error, 0x3e7, "Print classification error");
            throw exception;
        }

        public void setNextRow(int x, int height)
        {
            this.x = x;
            this.y += height;
        }

        public int X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }
    }
}

