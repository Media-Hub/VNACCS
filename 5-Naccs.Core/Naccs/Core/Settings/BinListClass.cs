namespace Naccs.Core.Settings
{
    using System;
    using System.Collections.Generic;

    public class BinListClass : List<BinClass>
    {
        private Dictionary<int, BinClass> BinTable = new Dictionary<int, BinClass>();

        public void Add(BinClass bin)
        {
            BinClass item = (BinClass) bin.Clone();
            base.Add(item);
            try
            {
                this.BinTable.Add(bin.No, item);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Duplicated key." + exception.Message);
                throw exception;
            }
        }

        public BinClass getBin(int no)
        {
            try
            {
                return this.BinTable[no];
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

