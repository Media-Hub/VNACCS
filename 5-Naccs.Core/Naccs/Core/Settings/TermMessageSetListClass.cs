namespace Naccs.Core.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class TermMessageSetListClass : List<TermMessageSetClass>
    {
        private Dictionary<string, TermMessageSetClass> TermMessageSetTable = new Dictionary<string, TermMessageSetClass>();
        private bool updateFlg;

        public void Add(TermMessageSetClass termmessageset)
        {
            TermMessageSetClass item = (TermMessageSetClass) termmessageset.Clone();
            base.Add(item);
            try
            {
                this.TermMessageSetTable.Add(termmessageset.Code, item);
                termmessageset.OnChange += new EventHandler(this.termmessageset_OnChange);
                item.OnChange += new EventHandler(this.termmessageset_OnChange);
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
            this.TermMessageSetTable.Clear();
        }

        public TermMessageSetClass GetTermMessageSet(string code)
        {
            try
            {
                return this.TermMessageSetTable[code];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Remove(string termmessageset)
        {
            TermMessageSetClass termMessageSet = this.GetTermMessageSet(termmessageset);
            if (termMessageSet != null)
            {
                this.TermMessageSetTable.Remove(termmessageset);
                base.Remove(termMessageSet);
            }
        }

        public void RemoveAt(int index)
        {
            TermMessageSetClass class2 = base[index];
            if (class2 != null)
            {
                this.TermMessageSetTable.Remove(class2.Code);
                this.RemoveAt(index);
            }
        }

        public void termmessageset_OnChange(object sender, EventArgs e)
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

