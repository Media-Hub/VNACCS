namespace Naccs.Common.CustomControls
{
    using System;

    [Serializable]
    public class ComboExItem
    {
        private string data;
        private string display;

        public ComboExItem(string value)
        {
            string[] strArray = value.Split(new char[] { ',' });
            if ((strArray != null) && (strArray.Length > 0))
            {
                this.data = strArray[0];
                this.display = strArray[1];
            }
        }

        public ComboExItem(string data, string display)
        {
            this.data = data;
            this.display = display;
        }

        public override string ToString()
        {
            return (this.data + "," + this.display);
        }

        public string Data
        {
            get
            {
                return this.data.TrimEnd(new char[0]);
            }
            set
            {
                this.data = value;
            }
        }

        public string Display
        {
            get
            {
                return this.display;
            }
            set
            {
                this.display = value;
            }
        }
    }
}

