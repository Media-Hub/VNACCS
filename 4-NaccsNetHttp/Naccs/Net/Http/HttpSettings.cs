namespace Naccs.Net.Http
{
    using System;
    using System.Net;

    public class HttpSettings
    {
        private const string CharsetSJIS = "Shift_JIS";
        private const string CharsetUTF8 = "UTF-8";
        private const string CnHttp = "HTTP://";
        private const string CnHttps = "HTTPS://";
        private bool isDebug;
        private int mCharset = 1;
        private string mClientCertificateHash;
        private bool mKeepAlive;
        private Version mProtocolVersion = HttpVersion.Version11;
        private string mProxyAccount;
        private string mProxyPassword;
        private int mProxyPort = 0x1f90;
        private string mProxyServer;
        private int mSendTimeout = 180;
        private string mServerName;
        private string mServerObject;
        private int mServerPort = 80;
        private string mTraceFile = "httptrace.log";
        private int mTraceSize = 0x3e8;
        private bool mUseHttps;
        private bool mUseProxy;
        private bool mUseProxyAccount;
        private bool mUseTrace;

        public int Charset
        {
            get
            {
                return this.mCharset;
            }
            set
            {
                this.mCharset = 0;
                if ((value > 0) && (value <= 2))
                {
                    this.mCharset = value;
                }
            }
        }

        public string CharsetString
        {
            get
            {
                if (this.mCharset == 2)
                {
                    return "Shift_JIS";
                }
                return "UTF-8";
            }
        }

        public string ClientCertificateHash
        {
            get
            {
                return this.mClientCertificateHash;
            }
            set
            {
                this.mClientCertificateHash = value;
            }
        }

        public string HttpProtocolVersion
        {
            get
            {
                return this.mProtocolVersion.ToString();
            }
            set
            {
                if (value == HttpVersion.Version10.ToString())
                {
                    this.mProtocolVersion = HttpVersion.Version10;
                }
                else
                {
                    this.mProtocolVersion = HttpVersion.Version11;
                }
            }
        }

        public bool IsDebug
        {
            get
            {
                return this.isDebug;
            }
            set
            {
                this.isDebug = value;
            }
        }

        public bool KeepAlive
        {
            get
            {
                return this.mKeepAlive;
            }
            set
            {
                this.mKeepAlive = value;
            }
        }

        public Version ProtocolVersion
        {
            get
            {
                if (!(this.mProtocolVersion == HttpVersion.Version10) && !(this.mProtocolVersion == HttpVersion.Version11))
                {
                    return HttpVersion.Version11;
                }
                return this.mProtocolVersion;
            }
        }

        public string ProxyAccount
        {
            get
            {
                return this.mProxyAccount;
            }
            set
            {
                this.mProxyAccount = value;
            }
        }

        public string ProxyAddresString
        {
            get
            {
                try
                {
                    string str = "HTTP://";
                    Uri uri = new Uri((str + this.ProxyServer) + ":" + this.ProxyPort.ToString());
                    return uri.ToString();
                }
                catch
                {
                    return null;
                }
            }
        }

        public string ProxyPassword
        {
            get
            {
                return this.mProxyPassword;
            }
            set
            {
                this.mProxyPassword = value;
            }
        }

        public int ProxyPort
        {
            get
            {
                return this.mProxyPort;
            }
            set
            {
                this.mProxyPort = value;
            }
        }

        public string ProxyServer
        {
            get
            {
                return this.mProxyServer;
            }
            set
            {
                this.mProxyServer = value;
            }
        }

        public int SendTimeout
        {
            get
            {
                return this.mSendTimeout;
            }
            set
            {
                this.mSendTimeout = value;
            }
        }

        public string ServerName
        {
            get
            {
                return this.mServerName;
            }
            set
            {
                this.mServerName = value;
            }
        }

        public string ServerObject
        {
            get
            {
                return this.mServerObject;
            }
            set
            {
                this.mServerObject = value;
            }
        }

        public int ServerPort
        {
            get
            {
                return this.mServerPort;
            }
            set
            {
                this.mServerPort = value;
            }
        }

        public string TraceFile
        {
            get
            {
                return this.mTraceFile;
            }
            set
            {
                this.mTraceFile = value;
            }
        }

        public int TraceSize
        {
            get
            {
                return this.mTraceSize;
            }
            set
            {
                this.mTraceSize = value;
            }
        }

        public string UriString
        {
            get
            {
                try
                {
                    string str = "HTTP://";
                    if (this.UseHttps)
                    {
                        str = "HTTPS://";
                    }
                    Uri baseUri = new Uri((str + this.ServerName) + ":" + this.ServerPort.ToString());
                    Uri uri2 = new Uri(baseUri, this.ServerObject);
                    return uri2.ToString();
                }
                catch
                {
                    return null;
                }
            }
        }

        public bool UseHttps
        {
            get
            {
                return this.mUseHttps;
            }
            set
            {
                this.mUseHttps = value;
            }
        }

        public bool UseProxy
        {
            get
            {
                return this.mUseProxy;
            }
            set
            {
                this.mUseProxy = value;
            }
        }

        public bool UseProxyAccount
        {
            get
            {
                return this.mUseProxyAccount;
            }
            set
            {
                this.mUseProxyAccount = value;
            }
        }

        public bool UseTrace
        {
            get
            {
                return this.mUseTrace;
            }
            set
            {
                this.mUseTrace = value;
            }
        }
    }
}

