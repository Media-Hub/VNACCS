namespace Naccs.Common.Generator
{
    using System;

    public class LastPageInfo
    {
        private int _page;
        private string _tbl;

        public int Page
        {
            get
            {
                return this._page;
            }
            set
            {
                this._page = value;
            }
        }

        public string Tbl
        {
            get
            {
                return this._tbl;
            }
            set
            {
                this._tbl = value;
            }
        }
    }
}

