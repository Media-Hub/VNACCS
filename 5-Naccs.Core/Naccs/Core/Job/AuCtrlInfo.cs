namespace Naccs.Core.Job
{
    using System;
    using System.Windows.Forms;

    internal class AuCtrlInfo
    {
        private AutoCompleteStringCollection auList;
        private string id;

        public AutoCompleteStringCollection AuList
        {
            get
            {
                return this.auList;
            }
            set
            {
                this.auList = value;
            }
        }

        public string ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
    }
}

