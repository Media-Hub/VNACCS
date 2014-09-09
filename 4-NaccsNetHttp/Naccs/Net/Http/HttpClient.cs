namespace Naccs.Net.Http
{
    using Naccs.Net;
    using Naccs.Net.Http.Properties;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Net.Cache;
    using System.Net.Mime;
    using System.Net.Security;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading;
    using System.Timers;
    using System.Windows.Forms;

    public class HttpClient : IHttpClient, ICloneable
    {
        private string _DefUserAgent = "NACCS Terminal Software";
        private SMimeFormat cSMime = new SMimeFormat();
        private ComParamBuilder mBuilder;
        private bool mIsTimeout;
        private HttpWebRequest mRequest;
        private bool mRequestAfterAbort;
        private bool mRequestBeforAbort;
        private object mRequestSender;
        private ComParameter mSenddata;
        private string mSendkey;
        private MemoryStream mSendmem;
        private HttpSettings mSettings;
        private HttpStatus mStatus;
        protected System.Timers.Timer mTimer;
        private NetTrace mTrace;
        private NetTrace.Tracekind mTracekind = NetTrace.Tracekind.HTTP;
        private int RetryCount = 5;
        private int RetryWaitTimer = 100;

        public event NetworkErrorEventHandler OnNetworkError;

        public event ReceivedEventHandler OnReceived;

        public event SentEventHandler OnSent;

        public HttpClient()
        {
            this.mBuilder = new ComParamBuilder();
            this.mBuilder.BaseCharset = "UTF-8";
            this.mTrace = new NetTrace();
        }
        public HttpClient(HttpSettings settings)
        {
            this.InitSetting(settings);
            string path = Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), "HttpOption.csv");
            try
            {
                if (System.IO.File.Exists(path))
                {
                    using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
                    {
                        while (reader.Peek() >= 0)
                        {
                            string[] strArray = reader.ReadLine().Split(new char[] { ',' });
                            if (strArray.Length > 1)
                            {
                                if (strArray[0].Trim().Equals("RetryCount"))
                                {
                                    int.TryParse(strArray[1].Trim(), out this.RetryCount);
                                }
                                else if (strArray[0].Trim().Equals("RetryWaitTimer"))
                                {
                                    int.TryParse(strArray[1].Trim(), out this.RetryWaitTimer);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public void Abort()
        {
            this.mTrace.WriteString(this.mTracekind, string.Format("# {0,-16} : status[{1}]", "Abort Request", this.mStatus));
            this.mTimer.Enabled = false;
            if (this.mStatus == HttpStatus.Idle)
            {
                this.mRequestBeforAbort = true;
            }
            else
            {
                try
                {
                    this.mRequestAfterAbort = true;
                    this.mRequest.Abort();
                }
                catch (Exception exception)
                {
                    this.ErrorResponse(new WebException(exception.Message, exception), false);
                }
            }
        }

        public object Clone()
        {
            object obj2 = base.MemberwiseClone();
            try
            {
                ((HttpClient) obj2).mTimer.Elapsed -= new ElapsedEventHandler(this.TimeoutCallback);
            }
            catch
            {
            }
            ((HttpClient) obj2).InitSetting(this.mSettings);
            return obj2;
        }

        private void DoRequest()
        {
            if (((Control) this.mRequestSender).InvokeRequired)
            {
                RequestDelegate method = new RequestDelegate(this.DoRequest);
                ((Control) this.mRequestSender).Invoke(method, new object[0]);
            }
            else if (this.mRequestBeforAbort)
            {
                this.mRequest.Abort();
                this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "DoRequest", "Abort(Irregular)"));
            }
            else
            {
                try
                {
                    ComParameter mSenddata = this.mSenddata;
                    if (mSenddata == null)
                    {
                        this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "DoRequest", "GET"));
                        this.mRequest.Method = "GET";
                        this.Status = HttpStatus.ResponseWait;
                        this.mTimer.Enabled = true;
                        this.mRequest.BeginGetResponse(new AsyncCallback(this.GetCallback), this.mRequest);
                    }
                    else
                    {
                        this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "DoRequest", string.Format("Send Data Length={0} AttachCount={1}", MimeFormat.StringLength(mSenddata.DataString), mSenddata.AttachCount)));
                        this.mTrace.WriteDump(this.mTracekind, mSenddata.DataString, true);
                        this.mRequest.Method = "POST";
                        this.mSendmem = this.MakeSendData(this.mRequest, mSenddata);
                        this.Status = HttpStatus.RequestWait;
                        this.mTimer.Enabled = true;
                        this.mRequest.BeginGetRequestStream(new AsyncCallback(this.SendCallback), this.mRequest);
                    }
                }
                catch (WebException exception)
                {
                    this.ErrorResponse(exception, true);
                }
                catch (Exception exception2)
                {
                    this.ErrorResponse(new WebException(exception2.Message, exception2), false);
                }
            }
        }

        private void ErrorResponse(WebException wex, bool isWebException)
        {
            this.mTimer.Enabled = false;
            if (((Control) this.mRequestSender).InvokeRequired)
            {
                ErrorDelegate method = new ErrorDelegate(this.ErrorResponse);
                ((Control) this.mRequestSender).Invoke(method, new object[] { wex, isWebException });
            }
            else
            {
                try
                {
                    this.mTrace.WriteString(this.mTracekind, string.Format("# {0,-16} : status[{1}]", "ErrorResponse", this.mStatus));
                    if (isWebException)
                    {
                        if (wex.Status == WebExceptionStatus.RequestCanceled)
                        {
                            if (this.mIsTimeout)
                            {
                                this.mIsTimeout = false;
                                this.mTrace.WriteString(this.mTracekind, string.Format("- {0,-16} : error[{1}]", "ErrorResponse", HttpCause.Timeout));
                                HttpExceptionEx ex = new HttpExceptionEx(this.mStatus, HttpCause.Timeout, wex);
                                this.OnNetworkError(this, this.mSendkey, ex);
                            }
                            else
                            {
                                this.mTrace.WriteString(this.mTracekind, string.Format("- {0,-16} : error[{1}]", "ErrorResponse", HttpCause.UserCancel));
                                HttpExceptionEx ex2 = new HttpExceptionEx(this.mStatus, HttpCause.UserCancel, wex);
                                this.OnNetworkError(this, this.mSendkey, ex2);
                            }
                        }
                        else
                        {
                            this.mTrace.WriteString(this.mTracekind, string.Format("- {0,-16} : error[{1}]", "ErrorResponse", HttpCause.WebException));
                            HttpExceptionEx ex3 = new HttpExceptionEx(this.mStatus, HttpCause.WebException, wex);
                            this.OnNetworkError(this, this.mSendkey, ex3);
                        }
                    }
                    else
                    {
                        this.mTrace.WriteString(this.mTracekind, string.Format("- {0,-16} : error[{1}]", "ErrorResponse", HttpCause.Exception));
                        HttpExceptionEx ex4 = new HttpExceptionEx(this.mStatus, HttpCause.Exception, wex);
                        this.OnNetworkError(this, this.mSendkey, ex4);
                    }
                }
                finally
                {
                    this.Status = HttpStatus.Idle;
                }
            }
        }

        private void GetCallback(IAsyncResult asyncResult)
        {
            this.mTrace.WriteString(this.mTracekind, string.Format("# {0,-16} : status[{1}]", "GetCallback", this.mStatus));
            this.mTimer.Enabled = false;
            this.Status = HttpStatus.Response;
            WebResponse response = null;
            Stream readStream = null;
            MemoryStream mem = null;
            try
            {
                HttpWebRequest asyncState = (HttpWebRequest) asyncResult.AsyncState;
                try
                {
                    response = asyncState.EndGetResponse(asyncResult);
                    this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "GetCallback", string.Format("ContentType={0} ContentLength={1}", response.ContentType, response.ContentLength)));
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < response.Headers.Count; i++)
                    {
                        builder.Append(string.Format("\r\n{0}: {1}", response.Headers.Keys[i], response.Headers[i]));
                    }
                    this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "GetCallback", string.Format("<Header>\r\n{0}\r\n", builder.ToString())));
                    readStream = response.GetResponseStream();
                    mem = this.mBuilder.ReadToMemory(readStream);
                    this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "GetCallback", string.Format("Read Length={0}", mem.Length)));
                    string fileName = Path.GetFileName(asyncState.RequestUri.ToString());
                    if (string.IsNullOrEmpty(fileName))
                    {
                        fileName = "default";
                    }
                    string trans = response.Headers[HttpResponseHeader.TransferEncoding];
                    ComParameter data = this.mBuilder.ToAttachComParameter(mem, fileName, trans);
                    this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "ReceiveCallback", string.Format("Receive Data={0}", this.GetMaskedReceiveHeader(data.DataString, this.mSettings.IsDebug))));
                    this.ReceivedResponse(data);
                }
                catch (WebException exception)
                {
                    string message;
                    if (exception.Status == WebExceptionStatus.ProtocolError)
                    {
                        message = string.Format("Status Code : {0}({1})\r\n{2}", ((HttpWebResponse) exception.Response).StatusCode, ((HttpWebResponse) exception.Response).StatusDescription, exception.Message);
                    }
                    else
                    {
                        message = exception.Message;
                    }
                    this.mTrace.WriteString(this.mTracekind, string.Format("! {0,-16} : {1}({2})\r\n{3}", new object[] { "GetCallback", exception.GetType(), exception.Status, message }));
                    this.ErrorResponse(exception, true);
                }
                catch (Exception exception2)
                {
                    this.mTrace.WriteString(this.mTracekind, string.Format("! {0,-16} : {1} \r\n{2}", "GetCallback", exception2.GetType(), exception2.Message));
                    this.ErrorResponse(new WebException(exception2.Message, exception2), false);
                }
            }
            finally
            {
                if (mem != null)
                {
                    mem.Close();
                }
                this.Status = HttpStatus.Idle;
                if (readStream != null)
                {
                    readStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
            }
        }

        protected string GetMaskedReceiveHeader(string data, bool isDebug)
        {
            if (isDebug)
            {
                return data;
            }
            int num = 0x18e;
            char[] chArray = data.ToCharArray();
            if (chArray.Length < num)
            {
                return data;
            }
            for (int i = 0; i < 8; i++)
            {
                chArray[0x25 + i] = '*';
            }
            char[] chArray2 = new char[num];
            for (int j = 0; j < chArray2.Length; j++)
            {
                chArray2[j] = chArray[j];
            }
            return new string(chArray2);
        }

        protected HttpResult HttpRequest(object sender, string key, string uri, ComParameter senddata)
        {
            this.mTrace.WriteString(this.mTracekind, string.Format("# {0,-16} : status[{1}]", "HttpRequest", this.mStatus));
            if (this.mStatus != HttpStatus.Idle)
            {
                return HttpResult.StatusError;
            }
            this.mRequestAfterAbort = false;
            this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "HttpRequest", string.Format("key={0} uri={1}", key, uri)));
            this.mRequestSender = sender;
            this.mSendkey = key;
            this.mSenddata = senddata;
            try
            {
                if (string.IsNullOrEmpty(uri))
                {
                    this.mTrace.WriteString(this.mTracekind, string.Format("- {0,-16} : error[{1}]", "HttpRequest", HttpResult.UriError));
                    return HttpResult.UriError;
                }
                this.mRequest = (HttpWebRequest) WebRequest.Create(uri);
                try
                {
                    this.mRequest.Credentials = CredentialCache.DefaultCredentials;
                }
                catch (Exception exception)
                {
                    this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "HttpRequest", "Credentials Set Warning"));
                    this.mTrace.WriteString(this.mTracekind, string.Format("! {0,-16} : {1} \r\n{2}", "HttpRequest", exception.GetType(), exception.Message));
                }
                this.mRequest.KeepAlive = this.mSettings.KeepAlive;
                this.mRequest.ProtocolVersion = this.mSettings.ProtocolVersion;
                this.mRequest.UserAgent = this.DefUserAgent;
                this.mRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
                HttpResult result = this.SetProxy();
                if (result != HttpResult.None)
                {
                    return result;
                }
                result = this.SetClientCert();
                if (result != HttpResult.None)
                {
                    return result;
                }
                this.Status = HttpStatus.RequestWait;
                new Thread(new ThreadStart(this.DoRequest)) { Name = "通信スレッド（" + this.mSendkey + "）", IsBackground = true }.Start();
            }
            catch (Exception exception2)
            {
                this.mTrace.WriteString(this.mTracekind, string.Format("! {0,-16} : {1} \r\n{2}", "HttpRequest", exception2.GetType(), exception2.Message));
                return HttpResult.ProgramError;
            }
            return HttpResult.None;
        }

        protected void InitSetting(HttpSettings settings)
        {
            this.mBuilder = new ComParamBuilder();
            this.mBuilder.BaseCharset = settings.CharsetString;
            this.mSettings = settings;
            this.mTimer = new System.Timers.Timer();
            this.mTimer.Elapsed += new ElapsedEventHandler(this.TimeoutCallback);
            this.mTimer.Interval = this.mSettings.SendTimeout * 0x3e8;
            this.mTimer.AutoReset = false;
            this.Status = HttpStatus.Idle;
            this.TraceSet();
            if (this.mSettings.UseHttps)
            {
                this.mTracekind = NetTrace.Tracekind.HTTPS;
            }
        }

        public MemoryStream MakeSendData(HttpWebRequest request, ComParameter senddata)
        {
            MemoryStream message = null;
            string boundaryString = MimeFormat.BoundaryString;
            if (senddata.AttachCount > 0)
            {
                message = this.mBuilder.ToMemoryStream(senddata, boundaryString, this.mTrace);
                request.SendChunked = false;
                request.ContentType = MimeFormat.MultiContentType(boundaryString);
                request.ContentLength = message.ToArray().Length;
                this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "MakeSendData", string.Format("ContentType={0} Length={1}", request.ContentType, request.ContentLength)));
            }
            else if (!string.IsNullOrEmpty(senddata.DataString))
            {
                message = this.mBuilder.ToMemoryStream(senddata, boundaryString, this.mTrace);
                request.SendChunked = false;
                request.ContentType = MimeFormat.TextContentType(this.mBuilder.BaseCharset);
                request.ContentLength = message.ToArray().Length;
                this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "MakeSendData", string.Format("ContentType={0} Length={1}", request.ContentType, request.ContentLength)));
            }
            if (senddata.Sign == "1")
            {
                this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "ConvertSMIMEstr", string.Format("ContentType={0} Length={1}", request.ContentType, request.ContentLength)));
                if (this.cSMime == null)
                {
                    this.cSMime = new SMimeFormat();
                }
                string header = request.ContentType.ToString();
                message = this.cSMime.ConvertSMIME(message, header, senddata.SelectSign);
                request.ContentType = string.Format("multipart/signed; micalg=\"sha1\"; boundary=\"{0}\"; protocol=\"application/x-pkcs7-signature\"", this.cSMime.GetThisBoundary);
                request.ContentLength = message.ToArray().Length;
                this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "ConvertSMIMEend", string.Format("ContentType={0} Length={1}", request.ContentType, request.ContentLength)));
            }
            return message;
        }

        protected virtual bool OnRemoteCertValidation(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors errors)
        {
            bool flag = false;
            RemoteCertValidation validate = RemoteCertValidation.CreateInstance();
            flag = validate.Validation(cert, errors);
            if (!flag)
            {
                this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "RemoteCertValidation", string.Format("Errors:{0}", errors.ToString())));
                this.mTimer.Enabled = false;
                this.RemoteError((X509Certificate2) cert, errors, validate);
                if (validate.Validated)
                {
                    this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "RemoteCertValidation", "Request Retry"));
                    this.mTimer.Enabled = true;
                    return true;
                }
                ServicePointManager.ServerCertificateValidationCallback = null;
            }
            return flag;
        }

        private void ReceiveCallback(IAsyncResult asyncResult)
        {
            this.mTrace.WriteString(this.mTracekind, string.Format("# {0,-16} : status[{1}]", "ReceiveCallback", this.mStatus));
            this.mTimer.Enabled = false;
            this.Status = HttpStatus.Response;
            WebResponse response = null;
            Stream readStream = null;
            MemoryStream sMIMEmessage = null;
            string str = null;
            IntPtr handle = asyncResult.AsyncWaitHandle.SafeWaitHandle.DangerousGetHandle();
            this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "ReceiveCallback", string.Format("WaitHandle:{0} Start", handle)));
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                HttpWebRequest asyncState = (HttpWebRequest) asyncResult.AsyncState;
                try
                {
                    ComParameter parameter;
                    response = asyncState.EndGetResponse(asyncResult);
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < response.Headers.Count; i++)
                    {
                        builder.Append(string.Format("\r\n{0}: {1}", response.Headers.Keys[i], response.Headers[i]));
                    }
                    this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "ReceiveCallback", string.Format("<Header>\r\n{0}\r\n", builder.ToString())));
                    readStream = response.GetResponseStream();
                    sMIMEmessage = this.mBuilder.ReadToMemory(readStream);
                    if (this.mSettings.IsDebug)
                    {
                        this.WriteHttpMessage(false, response.Headers.ToByteArray(), sMIMEmessage.ToArray());
                    }
                    if (response.ContentType.ToString().Contains("multipart/signed"))
                    {
                        this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "ReceveSMIME", string.Format("ContentType={0} Length={1}", response.ContentType, response.ContentLength)));
                        str = response.ContentType.ToString() + "\r\n\r\n" + Encoding.UTF8.GetString(sMIMEmessage.ToArray());
                        string boundary = new ContentType(response.ContentType).Boundary;
                        sMIMEmessage = this.cSMime.SignParth(sMIMEmessage, boundary);
                        this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "SignParthSMIME", string.Format("S/MIMEmessage={0} Boudary={1}", Encoding.UTF8.GetString(sMIMEmessage.ToArray()), boundary)));
                    }
                    ContentDisposition cd = null;
                    if (response.Headers["Content-Disposition"] != null)
                    {
                        cd = new ContentDisposition(response.Headers["Content-Disposition"]);
                    }
                    if (MimeFormat.IsAttachment(cd))
                    {
                        string name = MimeFormat.DecodeAttachFile(cd.FileName);
                        string trans = response.Headers[HttpResponseHeader.TransferEncoding];
                        parameter = this.mBuilder.ToAttachComParameter(sMIMEmessage, name, trans);
                        this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "ReceiveCallback", string.Format("Receive Data={0}", this.GetMaskedReceiveHeader(parameter.DataString, this.mSettings.IsDebug))));
                        this.ReceivedResponse(parameter);
                        return;
                    }
                    ContentType ct = new ContentType(response.ContentType);
                    if (MimeFormat.IsText(ct))
                    {
                        parameter = this.mBuilder.ToTextComParameter(sMIMEmessage, ct.CharSet);
                        goto Label_0575;
                    }
                    if (MimeFormat.IsMulti(ct))
                    {
                        parameter = this.mBuilder.ToComParameter(sMIMEmessage, ct.Boundary, this.mTrace);
                        goto Label_0575;
                    }
                    if (!MimeFormat.IsSigned(ct))
                    {
                        goto Label_051D;
                    }
                    if (response.Headers.ToString().Contains("X-Backside-Transport") && response.Headers["X-Backside-Transport"].Contains("FAIL FAIL"))
                    {
                        this.mTrace.WriteString(this.mTracekind, string.Format("- {0,-16} : error[{1}]", "ReceiveCallback", string.Format("{0}({1})", HttpCause.NotSupport, ct.MediaType)));
                        this.ErrorResponse(new WebException("Signature form error."), false);
                        return;
                    }
                    if (Encoding.UTF8.GetString(sMIMEmessage.ToArray()).Contains("multipart/mixed"))
                    {
                        using (StringReader reader = new StringReader(Encoding.UTF8.GetString(sMIMEmessage.ToArray())))
                        {
                            string str5 = reader.ReadLine().ToString().Replace("Content-Type: multipart/mixed; boundary=", "").Replace("\"", "");
                            sMIMEmessage = new MemoryStream(Encoding.UTF8.GetBytes(reader.ReadToEnd()));
                            parameter = this.mBuilder.ToComParameter(sMIMEmessage, str5, this.mTrace);
                            reader.Close();
                            goto Label_0502;
                        }
                    }
                    using (StringReader reader2 = new StringReader(Encoding.UTF8.GetString(sMIMEmessage.ToArray())))
                    {
                        while (reader2.Peek() > -1)
                        {
                            if (string.IsNullOrEmpty(reader2.ReadLine()))
                            {
                                sMIMEmessage = new MemoryStream(Encoding.UTF8.GetBytes(reader2.ReadToEnd()));
                                break;
                            }
                        }
                        parameter = this.mBuilder.ToTextComParameter(sMIMEmessage, ct.CharSet);
                        reader2.Close();
                    }
                Label_0502:
                    parameter.Sign = "1";
                    parameter.SignFile = str.ToString();
                    goto Label_0575;
                Label_051D:
                    this.mTrace.WriteString(this.mTracekind, string.Format("- {0,-16} : error[{1}]", "ReceiveCallback", string.Format("{0}({1})", HttpCause.NotSupport, ct.MediaType)));
                    this.ErrorResponse(new WebException(Resources.ResourceManager.GetString("HTTP02")), false);
                    return;
                Label_0575:
                    this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "ReceiveCallback", string.Format("Receive Data Length={0} AttachCount={1}", MimeFormat.StringLength(parameter.DataString), parameter.AttachCount)));
                    this.mTrace.WriteDump(this.mTracekind, parameter.DataString, false);
                    this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "ReceiveCallback", string.Format("WaitHandle:{0} OnReceived Start Elapsed Time {1} ms", handle, stopwatch.ElapsedMilliseconds)));
                    this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "ReceiveCallback", string.Format("Receive Data=\r\n{0}", this.GetMaskedReceiveHeader(parameter.DataString, this.mSettings.IsDebug))));
                    this.ReceivedResponse(parameter);
                    this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "ReceiveCallback", string.Format("WaitHandle:{0} OnReceived Ended Elapsed Time {1} ms", handle, stopwatch.ElapsedMilliseconds)));
                }
                catch (WebException exception)
                {
                    string message;
                    if (exception.Status == WebExceptionStatus.ProtocolError)
                    {
                        message = string.Format("Status Code : {0}({1})\r\n{2}", ((HttpWebResponse) exception.Response).StatusCode, ((HttpWebResponse) exception.Response).StatusDescription, exception.Message);
                    }
                    else
                    {
                        message = exception.Message;
                    }
                    this.mTrace.WriteString(this.mTracekind, string.Format("! {0,-16} : {1}({2})\r\n{3}", new object[] { "ReceiveCallback", exception.GetType(), exception.Status, message }));
                    this.ErrorResponse(exception, true);
                }
                catch (Exception exception2)
                {
                    this.mTrace.WriteString(this.mTracekind, string.Format("! {0,-16} : {1} \r\n{2}", "ReceiveCallback", exception2.GetType(), exception2.Message));
                    this.ErrorResponse(new WebException(exception2.Message, exception2), false);
                }
            }
            finally
            {
                if (sMIMEmessage != null)
                {
                    sMIMEmessage.Close();
                }
                this.Status = HttpStatus.Idle;
                if (readStream != null)
                {
                    readStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                stopwatch.Stop();
                this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "ReceiveCallback", string.Format("WaitHandle:{0} Ended Elapsed Time {1} ms", handle, stopwatch.ElapsedMilliseconds)));
            }
        }

        private void ReceivedResponse(ComParameter data)
        {
            if (((Control) this.mRequestSender).InvokeRequired)
            {
                ReceivedDelegate method = new ReceivedDelegate(this.ReceivedResponse);
                ((Control) this.mRequestSender).Invoke(method, new object[] { data });
            }
            else
            {
                this.mTrace.WriteString(this.mTracekind, string.Format("# {0,-16} : status[{1}]", "ReceivedResponse", this.mStatus));
                if (this.mRequestAfterAbort)
                {
                    WebException wex = new WebException(Resources.ResourceManager.GetString("HTTP01"), WebExceptionStatus.RequestCanceled);
                    this.mIsTimeout = false;
                    this.ErrorResponse(wex, true);
                }
                else
                {
                    this.OnReceived(this, this.mSendkey, data);
                }
            }
        }

        private void RemoteError(X509Certificate2 cert, SslPolicyErrors errors, RemoteCertValidation validate)
        {
            if (((Control) this.mRequestSender).InvokeRequired)
            {
                RemoteErrorDelegate method = new RemoteErrorDelegate(this.RemoteError);
                ((Control) this.mRequestSender).Invoke(method, new object[] { cert, errors, validate });
            }
            else
            {
                RemoteCertWarning warning = new RemoteCertWarning(cert, errors);
                try
                {
                    if (warning.ShowDialog() == DialogResult.OK)
                    {
                        validate.Validated = true;
                    }
                }
                finally
                {
                    if (!warning.Disposing)
                    {
                        warning.Dispose();
                    }
                }
            }
        }

        private void RequestElapsed(object sender, ElapsedEventArgs e)
        {
            System.Timers.Timer timer = (System.Timers.Timer) sender;
            timer.Enabled = false;
            timer.Dispose();
            this.DoRequest();
        }

        public int Send(object sender, string key, ComParameter senddata)
        {
            this.TraceSet();
            this.mTrace.WriteString(NetTrace.Tracekind.START, null);
            this.mTrace.WriteString(this.mTracekind, string.Format("{0} key[{1}]", string.Format("# {0,-16} : status[{1}]", "Send Request", this.mStatus), key));
            if (this.mRequestBeforAbort)
            {
                this.Status = HttpStatus.Idle;
                return -1;
            }
            if (this.mStatus != HttpStatus.Idle)
            {
                for (int i = 0; i < this.RetryCount; i++)
                {
                    this.mTrace.WriteString(this.mTracekind, string.Format("{0} key[{1}]", string.Format("# {0,-16} : status[{1}]", "Send Wait", this.mStatus), key));
                    Thread.Sleep(this.RetryWaitTimer);
                    if (this.mStatus == HttpStatus.Idle)
                    {
                        this.mTrace.WriteString(this.mTracekind, string.Format("{0} key[{1}]", string.Format("# {0,-16} : status[{1}]", "Send Retry", this.mStatus), key));
                        break;
                    }
                }
            }
            HttpResult result = this.HttpRequest(sender, key, this.mSettings.UriString, senddata);
            if (result != HttpResult.None)
            {
                this.Status = HttpStatus.Idle;
                this.mTrace.WriteString(this.mTracekind, string.Format("{0} key[{1}]", string.Format("- {0,-16} : error[{1}]", "Send Request", result), key));
            }
            return (int) result;
        }

        private void SendCallback(IAsyncResult asyncResult)
        {
            this.mTrace.WriteString(this.mTracekind, string.Format("# {0,-16} : status[{1}]", "SendCallback", this.mStatus));
            this.Status = HttpStatus.Request;
            if (this.mSenddata.AttachCount > 0)
            {
                this.mTimer.Enabled = false;
            }
            try
            {
                try
                {
                    HttpWebRequest asyncState = (HttpWebRequest) asyncResult.AsyncState;
                    Stream stream = asyncState.EndGetRequestStream(asyncResult);
                    try
                    {
                        StringBuilder builder = new StringBuilder();
                        for (int i = 0; i < asyncState.Headers.Count; i++)
                        {
                            builder.Append(string.Format("\r\n{0}: {1}", asyncState.Headers.Keys[i], asyncState.Headers[i]));
                        }
                        this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "SendCallback", string.Format("<Header>\r\n{0}\r\n", builder.ToString())));
                        if (this.mSettings.IsDebug)
                        {
                            this.WriteHttpMessage(true, asyncState.Headers.ToByteArray(), this.mSendmem.ToArray());
                        }
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        stream.Write(this.mSendmem.ToArray(), 0, this.mSendmem.ToArray().Length);
                        stopwatch.Stop();
                        this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "SendCallback", string.Format("Stream Write Time {0} ms", stopwatch.ElapsedMilliseconds)));
                        this.SentResponse(this.mSendkey);
                        this.Status = HttpStatus.ResponseWait;
                        asyncState.BeginGetResponse(new AsyncCallback(this.ReceiveCallback), asyncState);
                    }
                    finally
                    {
                        stream.Close();
                    }
                }
                finally
                {
                    this.mSendmem.Close();
                }
            }
            catch (WebException exception)
            {
                string message;
                this.mTimer.Enabled = false;
                if (exception.Status == WebExceptionStatus.ProtocolError)
                {
                    message = string.Format("Status Code : {0}({1})\r\n{2}", ((HttpWebResponse) exception.Response).StatusCode, ((HttpWebResponse) exception.Response).StatusDescription, exception.Message);
                }
                else
                {
                    message = exception.Message;
                }
                this.mTrace.WriteString(this.mTracekind, string.Format("! {0,-16} : {1}({2})\r\n{3}", new object[] { "SendCallback", exception.GetType(), exception.Status, message }));
                this.ErrorResponse(exception, true);
            }
            catch (Exception exception2)
            {
                this.mTimer.Enabled = false;
                this.mTrace.WriteString(this.mTracekind, string.Format("! {0,-16} : {1} \r\n{2}", "SendCallback", exception2.GetType(), exception2.Message));
                this.ErrorResponse(new WebException(exception2.Message, exception2), false);
            }
        }

        private void SentResponse(string key)
        {
            if (((Control) this.mRequestSender).InvokeRequired)
            {
                SentDelegate method = new SentDelegate(this.SentResponse);
                ((Control) this.mRequestSender).Invoke(method, new object[] { key });
            }
            else
            {
                this.mTrace.WriteString(this.mTracekind, string.Format("# {0,-16} : status[{1}]", "SentResponse", this.mStatus));
                this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "SentResponse", string.Format("key={0}", key)));
                this.OnSent(this, key);
            }
        }

        private HttpResult SetClientCert()
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = null;
                if (this.mSettings.UseHttps)
                {
                    this.mTrace.WriteString(this.mTracekind, string.Format("# {0,-16} : status[{1}]", "SetClientCert", this.mStatus));
                    if (!string.IsNullOrEmpty(this.mSettings.ClientCertificateHash))
                    {
                        ClientCert cert = new ClientCert();
                        switch (cert.ValidDays(this.mSettings.ClientCertificateHash))
                        {
                            case -1:
                                return HttpResult.CertUnestablishedError;

                            case -2:
                                return HttpResult.CertSelectError;

                            case -3:
                                return HttpResult.CertValidError;
                        }
                        X509Certificate2 certificate = cert.Find(this.mSettings.ClientCertificateHash);
                        if (certificate == null)
                        {
                            return HttpResult.CertSelectError;
                        }
                        this.mRequest.ClientCertificates = new X509Certificate2Collection();
                        this.mRequest.ClientCertificates.Add(certificate);
                    }
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.OnRemoteCertValidation);
                }
            }
            catch (Exception exception)
            {
                this.mTrace.WriteString(this.mTracekind, string.Format("! {0,-16} : {1} \r\n{2}", "SetClientCert", exception.GetType(), exception.Message));
                return HttpResult.CertException;
            }
            return HttpResult.None;
        }

        private HttpResult SetProxy()
        {
            try
            {
                this.mRequest.Proxy = null;
                if (this.mSettings.UseProxy)
                {
                    this.mTrace.WriteString(this.mTracekind, string.Format("# {0,-16} : status[{1}]", "SetProxy", this.mStatus));
                    try
                    {
                        Dns.GetHostAddresses(this.mSettings.ProxyServer);
                    }
                    catch
                    {
                        return HttpResult.ProxyAddressError;
                    }
                    string proxyAddresString = this.mSettings.ProxyAddresString;
                    if (string.IsNullOrEmpty(proxyAddresString))
                    {
                        return HttpResult.ProxyAddressError;
                    }
                    WebProxy proxy = new WebProxy {
                        Address = new Uri(proxyAddresString)
                    };
                    if (this.mSettings.UseProxyAccount)
                    {
                        this.mTrace.WriteString(this.mTracekind, string.Format("+ {0,-16} : {1}", "SetProxy", "UseProxyAccount"));
                        proxy.Credentials = new NetworkCredential(this.mSettings.ProxyAccount, this.mSettings.ProxyPassword);
                    }
                    else
                    {
                        proxy.UseDefaultCredentials = true;
                    }
                    this.mRequest.Proxy = proxy;
                }
            }
            catch (Exception exception)
            {
                this.mTrace.WriteString(this.mTracekind, string.Format("! {0,-16} : {1} \r\n{2}", "SetProxy", exception.GetType(), exception.Message));
                return HttpResult.ProxyException;
            }
            return HttpResult.None;
        }

        private void TimeoutCallback(object sender, ElapsedEventArgs e)
        {
            this.mTrace.WriteString(this.mTracekind, string.Format("# {0,-16} : status[{1}]", "TimeoutCallback", this.mStatus));
            this.mTimer.Enabled = false;
            this.mIsTimeout = true;
            this.mRequest.Abort();
        }

        private void TraceSet()
        {
            if (this.mTrace == null)
            {
                this.mTrace = new NetTrace();
            }
            this.mTrace.UseTrace = this.mSettings.UseTrace;
            this.mTrace.TraceFile = this.mSettings.TraceFile;
            this.mTrace.TraceSize = this.mSettings.TraceSize;
            this.mTrace.IsDebug = this.mSettings.IsDebug;
        }

        protected virtual void WriteHttpMessage(bool request, byte[] hd, byte[] buff)
        {
            string str;
            if (request)
            {
                str = "HttpRequest.txt";
            }
            else
            {
                str = "HttpResponse.txt";
            }
            Stream stream = new FileStream(Path.Combine(Path.GetDirectoryName(this.mTrace.TraceFile), str), FileMode.Create, FileAccess.Write);
            try
            {
                stream.Write(hd, 0, hd.Length);
                stream.Write(buff, 0, buff.Length);
            }
            finally
            {
                stream.Close();
            }
        }

        protected virtual string DefUserAgent
        {
            get
            {
                return this._DefUserAgent;
            }
            set
            {
                this._DefUserAgent = value;
            }
        }

        protected HttpStatus Status
        {
            get
            {
                return this.mStatus;
            }
            set
            {
                this.mStatus = value;
                if (this.mStatus == HttpStatus.Idle)
                {
                    this.mRequestBeforAbort = false;
                }
            }
        }

        protected NetTrace Trace
        {
            get
            {
                return this.mTrace;
            }
        }

        private delegate void ErrorDelegate(WebException wex, bool isWebException);

        private delegate void ReceivedDelegate(ComParameter data);

        private delegate void RemoteErrorDelegate(X509Certificate2 cert, SslPolicyErrors errors, RemoteCertValidation validate);

        private delegate void RequestDelegate();

        private delegate void SentDelegate(string key);
    }
}

