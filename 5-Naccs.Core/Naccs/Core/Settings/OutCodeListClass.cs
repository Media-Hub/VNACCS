namespace Naccs.Core.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class OutCodeListClass : List<OutCodeClass>
    {
        private Dictionary<string, OutCodeClass> OutCodeTable = new Dictionary<string, OutCodeClass>();
        private bool updateFlg;

        public void Add(OutCodeClass outcode)
        {
            OutCodeClass item = (OutCodeClass) outcode.Clone();
            base.Add(item);
            try
            {
                this.OutCodeTable.Add(outcode.Name, item);
                this.updateFlg = true;
                outcode.OnChange += new EventHandler(this.outcode_OnChange);
                item.OnChange += new EventHandler(this.outcode_OnChange);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Duplicated key." + exception.Message);
                throw exception;
            }
        }

        public void Clear()
        {
            this.updateFlg = base.Count > 0;
            base.Clear();
            this.OutCodeTable.Clear();
        }

        public OutCodeClass getOutCode(string name)
        {
            try
            {
                return this.OutCodeTable[name];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void outcode_OnChange(object sender, EventArgs e)
        {
            this.updateFlg = true;
        }

        public void Remove(OutCodeClass outcode)
        {
            if (outcode != null)
            {
                base.Remove(outcode);
                this.OutCodeTable.Remove(outcode.Name);
                this.updateFlg = true;
            }
        }

        public void RemoveAt(int index)
        {
            OutCodeClass outcode = base[index];
            this.Remove(outcode);
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

