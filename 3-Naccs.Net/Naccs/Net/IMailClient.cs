namespace Naccs.Net
{
    using System;

    public interface IMailClient
    {
        event MailDeleteEventHandler OnDelete;

        event MailListEventHandler OnList;

        event MailNetworkErrorEventHandler OnNetworkError;

        event MailOpenEventHandler OnOpen;

        event MailReceivedEventHandler OnReceived;

        event MailSentEventHandler OnSent;

        void Abort();
        object Clone();
        void Close();
        int Delete(int id);
        bool IsRequestWait();
        int List();
        int Open(object sender, string account, string password, bool auth);
        int Receive(int id);
        int Send(string key, string account, string subject, ComParameter senddata);
    }
}

