namespace Naccs.Common.CustomControls
{
    using System;
    using System.Drawing;

    public interface IItemAttributesEx : IItemAttributes
    {
        void SetBackColor();

        int CurrentPage { get; }

        Color IBackColor { get; set; }

        JobErrInfo JobErr { get; set; }

        string Rep_ID { get; set; }
    }
}

