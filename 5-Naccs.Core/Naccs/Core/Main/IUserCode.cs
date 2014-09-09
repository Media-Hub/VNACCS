namespace Naccs.Core.Main
{
    using Naccs.Core.Settings;
    using System;

    public interface IUserCode
    {
        event OnButtonHandler OnButton;

        event OnDeleteEventHandler OnDeleteList;

        void HistoryUserListSet(StringList uc);
        void HistoryUserListSet(StringList uc, StringList mc);

        bool MailBoxCheck { get; set; }

        string MailBoxID { get; set; }

        string MailBoxPassword { get; set; }

        string Password { get; set; }

        bool UpperCheck { get; set; }

        string UserCode { get; set; }
    }
}

