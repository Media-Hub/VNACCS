namespace Naccs.Core.DataView.Arrange
{
    using System;
    using System.Data;

    public class RemoveWorkerArgs
    {
        private string _RemoveDate;
        private bool _ResetID;
        private DataTable _Table;

        public string RemoveDate
        {
            get
            {
                return this._RemoveDate;
            }
            set
            {
                this._RemoveDate = value;
            }
        }

        public bool ResetID
        {
            get
            {
                return this._ResetID;
            }
            set
            {
                this._ResetID = value;
            }
        }

        public DataTable Table
        {
            get
            {
                return this._Table;
            }
            set
            {
                this._Table = value;
            }
        }
    }
}

