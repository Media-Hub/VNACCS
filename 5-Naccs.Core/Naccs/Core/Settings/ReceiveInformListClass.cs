namespace Naccs.Core.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class ReceiveInformListClass : List<ReceiveInformClass>
    {
        private Dictionary<string, ReceiveInformClass> ReceiveInformTable = new Dictionary<string, ReceiveInformClass>();
        private bool updateFlg;

        public void Add(ReceiveInformClass receiveinform)
        {
            ReceiveInformClass item = (ReceiveInformClass) receiveinform.Clone();
            base.Add(item);
            try
            {
                this.ReceiveInformTable.Add(receiveinform.OutCode, item);
                this.updateFlg = true;
                receiveinform.OnChange += new EventHandler(this.receiveinform_OnChange);
                item.OnChange += new EventHandler(this.receiveinform_OnChange);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Duplicated key." + exception.Message);
            }
        }

        public void Clear()
        {
            this.updateFlg = base.Count > 0;
            base.Clear();
            this.ReceiveInformTable.Clear();
        }

        public ReceiveInformClass getReceiveInform(string outcode)
        {
            try
            {
                return this.ReceiveInformTable[outcode];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void receiveinform_OnChange(object sender, EventArgs e)
        {
            this.updateFlg = true;
        }

        public void Remove(ReceiveInformClass item)
        {
            base.Remove(item);
            this.ReceiveInformTable.Remove(item.OutCode);
            this.updateFlg = true;
        }

        public void RemoveAt(int index)
        {
            ReceiveInformClass item = base[index];
            this.Remove(item);
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

