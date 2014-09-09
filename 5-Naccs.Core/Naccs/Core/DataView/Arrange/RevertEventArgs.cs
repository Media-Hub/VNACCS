namespace Naccs.Core.DataView.Arrange
{
    using System;

    public class RevertEventArgs : EventArgs
    {
        private string[] _FileNames;
        private string _PathRoot = string.Empty;

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

