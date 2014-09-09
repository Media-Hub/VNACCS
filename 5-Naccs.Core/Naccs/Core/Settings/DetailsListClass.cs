namespace Naccs.Core.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class DetailsListClass : List<DetailsClass>
    {
        private Dictionary<string, DetailsClass> DetailsTable = new Dictionary<string, DetailsClass>();
        private bool updateFlg;

        public void Add(DetailsClass details)
        {
            DetailsClass item = (DetailsClass) details.Clone();
            base.Add(item);
            try
            {
                this.DetailsTable.Add(details.OutCode, item);
                this.updateFlg = true;
                details.OnChange += new EventHandler(this.details_OnChange);
                item.OnChange += new EventHandler(this.details_OnChange);
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
            this.DetailsTable.Clear();
        }

        public void details_OnChange(object sender, EventArgs e)
        {
            this.updateFlg = true;
        }

        public DetailsClass getDetails(string outcode)
        {
            try
            {
                return this.DetailsTable[outcode];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Remove(DetailsClass item)
        {
            base.Remove(item);
            this.DetailsTable.Remove(item.OutCode);
            this.updateFlg = true;
        }

        public void RemoveAt(int index)
        {
            DetailsClass item = base[index];
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

