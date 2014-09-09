namespace Naccs.Common.Generator
{
    using System;

    public class NaccsDataItem
    {
        private string _attribute;
        private string _data;
        private int _figure;
        private string _id;
        private string _in;
        private long _order;
        private string _out;
        private int _page;
        private string _rpacked;
        private string _tbl;

        public string Attr
        {
            get
            {
                return this._attribute;
            }
            set
            {
                this._attribute = value;
            }
        }

        public string Data
        {
            get
            {
                return this._data;
            }
            set
            {
                this._data = value;
            }
        }

        public int Figure
        {
            get
            {
                return this._figure;
            }
            set
            {
                this._figure = value;
            }
        }

        public string ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public string In
        {
            get
            {
                return this._in;
            }
            set
            {
                this._in = value;
            }
        }

        public long order
        {
            get
            {
                return this._order;
            }
            set
            {
                this._order = value;
            }
        }

        public string Out
        {
            get
            {
                return this._out;
            }
            set
            {
                this._out = value;
            }
        }

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

        public string Rpacked
        {
            get
            {
                return this._rpacked;
            }
            set
            {
                this._rpacked = value;
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

