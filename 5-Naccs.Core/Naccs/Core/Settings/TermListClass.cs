namespace Naccs.Core.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class TermListClass : List<TermClass>
    {
        private Dictionary<int, TermClass> TermTable = new Dictionary<int, TermClass>();
        private bool updateFlg;

        public void Add(TermClass item)
        {
            TermClass class2 = (TermClass) item.Clone();
            base.Add(class2);
            try
            {
                this.TermTable.Add(item.Id, class2);
                this.updateFlg = true;
                item.OnChange += new EventHandler(this.term_OnChange);
                class2.OnChange += new EventHandler(this.term_OnChange);
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
            this.TermTable.Clear();
        }

        public TermClass getTerm(int id)
        {
            try
            {
                return this.TermTable[id];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Remove(TermClass item)
        {
            base.Remove(item);
            this.TermTable.Remove(item.Id);
            this.updateFlg = true;
        }

        public void RemoveAt(int index)
        {
            TermClass item = this.getTerm(index);
            this.Remove(item);
        }

        public void term_OnChange(object sender, EventArgs e)
        {
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

