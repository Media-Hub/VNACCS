namespace Naccs.Net
{
    using System;

    public interface IHttpClient
    {
        event NetworkErrorEventHandler OnNetworkError;

        event ReceivedEventHandler OnReceived;

        event SentEventHandler OnSent;

        void Abort();
        object Clone();
        int Send(object sender, string key, ComParameter senddata);
    }
}

