namespace Naccs.Core.DataView.Arrange
{
    using System;

    public class RevertWorkerResult
    {
        private string[] _ErrorFiles;
        private Exception _FirstError;
        private bool _IsCancel;
        private int _RevertCount;
        private string[] _SkipFiles;

        public string[] ErrorFiles
        {
            get
            {
                return this._ErrorFiles;
            }
            set
            {
                this._ErrorFiles = value;
            }
        }

        public Exception FirstError
        {
            get
            {
                return this._FirstError;
            }
            set
            {
                this._FirstError = value;
            }
        }

        public bool IsCancel
        {
            get
            {
                return this._IsCancel;
            }
            set
            {
                this._IsCancel = value;
            }
        }

        public int RevertCount
        {
            get
            {
                return this._RevertCount;
            }
            set
            {
                this._RevertCount = value;
            }
        }

        public string[] SkipFiles
        {
            get
            {
                return this._SkipFiles;
            }
            set
            {
                this._SkipFiles = value;
            }
        }
    }
}

