namespace Naccs.Common.CustomControls
{
    using System;

    public interface IItemAttributes
    {
        string attribute { get; set; }

        string check_attribute { get; set; }

        string check_date { get; set; }

        string check_full { get; set; }

        string check_time { get; set; }

        string choice_keyvalue { get; set; }

        int figure { get; set; }

        string form { get; set; }

        string id { get; set; }

        string input_output { get; set; }

        string name { get; set; }

        int order { get; set; }

        string required { get; set; }
    }
}

