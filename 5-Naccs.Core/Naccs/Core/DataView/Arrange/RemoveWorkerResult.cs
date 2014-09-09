namespace Naccs.Core.DataView.Arrange
{
    using System;

    public class RemoveWorkerResult
    {
        private bool _IsCancel;
        private int _RemoveCount;

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

        public int RemoveCount
        {
            get
            {
                return this._RemoveCount;
            }
            set
            {
                this._RemoveCount = value;
            }
        }
    }
}

