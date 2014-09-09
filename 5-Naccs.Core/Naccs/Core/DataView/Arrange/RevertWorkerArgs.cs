namespace Naccs.Core.DataView.Arrange
{
    using Naccs.Core.DataView;
    using System;

    public class RevertWorkerArgs
    {
        private IDataView _DataView;
        private string[] _FileNames;
        private string _PathRoot = string.Empty;

        public IDataView DataView
        {
            get
            {
                return this._DataView;
            }
            set
            {
                this._DataView = value;
            }
        }

        public string[] FileNames
        {
            get
            {
                return this._FileNames;
            }
            set
            {
                this._FileNames = value;
            }
        }

        public string PathRoot
        {
            get
            {
                return this._PathRoot;
            }
            set
            {
                this._PathRoot = value;
            }
        }
    }
}

