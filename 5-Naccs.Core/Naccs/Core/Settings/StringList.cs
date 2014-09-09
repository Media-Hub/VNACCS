namespace Naccs.Core.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class StringList : List<string>
    {
        private bool updateFlg;

        public void Add(string item)
        {
            base.Add(item);
            this.updateFlg = true;
        }

        public void Clear()
        {
            base.Clear();
            this.updateFlg = true;
        }

        public void Insert(int index, string item)
        {
            base.Insert(index, item);
            this.updateFlg = true;
        }

        public void Remove(string item)
        {
            base.Remove(item);
            this.updateFlg = true;
        }

        public void RemoveAt(int index)
        {
            base.RemoveAt(index);
            this.updateFlg = true;
        }

        [XmlIgnore]
        public bool UpdateFlg
        {
            get
            {
                return this.updateFlg;
            }
            set
            {
                this.updateFlg = value;
            }
        }
    }
}

