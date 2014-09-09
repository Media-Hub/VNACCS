namespace Naccs.Common.Generator
{
    using System;

    public class ItemInfoRec
    {
        private string _attribute;
        private string _check_date = "";
        private string _check_full = "";
        private string _check_time = "";
        private string _choice_keyvalue = "";
        private string _form = "";
        private string _id = "";
        private string _in;
        private string _input_output = " ";
        private string _name = "";
        private long _order = -1L;
        private string _out;
        private string _rep_id = "";
        private string _required = "";
        private string _rightpacked;
        private string checkAttribute = "";
        private int fFigure;

        public string attribute
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

        public string check_attribute
        {
            get
            {
                return this.checkAttribute;
            }
            set
            {
                this.checkAttribute = value;
            }
        }

        public string check_date
        {
            get
            {
                return this._check_date;
            }
            set
            {
                this._check_date = value;
            }
        }

        public string check_full
        {
            get
            {
                return this._check_full;
            }
            set
            {
                this._check_full = value;
            }
        }

        public string check_time
        {
            get
            {
                return this._check_time;
            }
            set
            {
                this._check_time = value;
            }
        }

        public string choice_keyvalue
        {
            get
            {
                return this._choice_keyvalue;
            }
            set
            {
                this._choice_keyvalue = value;
            }
        }

        public int figure
        {
            get
            {
                return this.fFigure;
            }
            set
            {
                this.fFigure = value;
            }
        }

        public string form
        {
            get
            {
                return this._form;
            }
            set
            {
                this._form = value;
            }
        }

        public string id
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

        public string input_output
        {
            get
            {
                return this._input_output;
            }
            set
            {
                this._input_output = value;
            }
        }

        public string name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
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

        public string rep_id
        {
            get
            {
                return this._rep_id;
            }
            set
            {
                this._rep_id = value;
            }
        }

        public string required
        {
            get
            {
                return this._required;
            }
            set
            {
                this._required = value;
            }
        }

        public string Rightpacked
        {
            get
            {
                return this._rightpacked;
            }
            set
            {
                this._rightpacked = value;
            }
        }
    }
}

