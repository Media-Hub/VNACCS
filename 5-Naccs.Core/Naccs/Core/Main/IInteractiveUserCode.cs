namespace Naccs.Core.Main
{
    using Naccs.Core.Settings;
    using System;
    using System.Windows.Forms;

    public interface IInteractiveUserCode
    {
        void Close();
        void Dispose();
        void HistoryUserListSet(StringList uc);
        DialogResult ShowDialog();
        void VisibleCheck(int Flag);

        string Password { get; set; }

        string UserCode { get; set; }
    }
}

