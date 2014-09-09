namespace Naccs.Common.Function
{
    using System;

    public class Size
    {
        private int height;
        private int width;

        public Size(string size)
        {
            this.width = int.Parse(size.Split(new char[] { ',' })[0]);
            this.height = int.Parse(size.Split(new char[] { ',' })[1]);
        }

        public Size(int wedth, int height)
        {
            this.width = wedth;
            this.height = height;
        }

        public string getSize()
        {
            return (this.width.ToString() + "," + this.height.ToString());
        }

        public int Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }

        public int Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
            }
        }
    }
}

