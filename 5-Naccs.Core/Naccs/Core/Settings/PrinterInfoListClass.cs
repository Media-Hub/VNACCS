namespace Naccs.Core.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class PrinterInfoListClass : List<PrinterInfoClass>
    {
        private Dictionary<string, PrinterInfoClass> PrinterInfoTable = new Dictionary<string, PrinterInfoClass>();
        private bool updateFlg;

        public void Add(PrinterInfoClass printerinfo)
        {
            PrinterInfoClass item = (PrinterInfoClass) printerinfo.Clone();
            base.Add(item);
            try
            {
                this.PrinterInfoTable.Add(printerinfo.Name, item);
                this.updateFlg = true;
                printerinfo.OnChange += new EventHandler(this.printerinfo_OnChange);
                item.OnChange += new EventHandler(this.printerinfo_OnChange);
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
            this.PrinterInfoTable.Clear();
        }

        public PrinterInfoClass getPrinterInfo(string name)
        {
            try
            {
                return this.PrinterInfoTable[name];
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void printerinfo_OnChange(object sender, EventArgs e)
        {
            this.updateFlg = true;
        }

        public void Remove(PrinterInfoClass printerinfo)
        {
            base.Remove(printerinfo);
            this.PrinterInfoTable.Remove(printerinfo.Name);
            this.updateFlg = true;
        }

        public void RemoveAt(int index)
        {
            PrinterInfoClass printerinfo = base[index];
            this.Remove(printerinfo);
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

