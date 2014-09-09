namespace Naccs.Common.CustomControls
{
    using System;
    using System.Windows.Forms;

    public interface IAutoComplete
    {
        bool AutoComplete { get; set; }

        AutoCompleteStringCollection AutoCompleteCustomSource { get; set; }

        string id { get; set; }

        string Text { get; set; }
    }
}

