namespace Naccs.Core.Classes
{
    using Naccs.Core.Properties;
    using Naccs.Net;
    using System;
    using System.Net;

    public class HttpErrorDlg : MessageDialog
    {
        private const string CRLF = "\r\n";

        public void ShowHttpError(HttpExceptionEx ex)
        {
            string str;
            bool flag = false;
            string applyStr = null;
            string str3 = null;
            string str4 = null;
            if (ex.Cause == HttpCause.UserCancel)
            {
                str = "E114";
            }
            else if (ex.Cause == HttpCause.Timeout)
            {
                str = "E113";
            }
            else if (ex.Cause == HttpCause.WebException)
            {
                str = "E111";
                str3 = ex.WebStatus.ToString();
                if (ex.WebStatus == WebExceptionStatus.ProtocolError)
                {
                    str4 = string.Format("{0}({1})", ex.HttpErrorStatusCode, ex.HttpErrorStatusDescription);
                }
                if (ex.Message != null)
                {
                    applyStr = ex.Message;
                }
                flag = true;
            }
            else if (ex.Cause == HttpCause.Exception)
            {
                str = "E112";
                if (ex.Message != null)
                {
                    applyStr = ex.Message;
                }
                flag = true;
            }
            else
            {
                str = "E119";
            }
            string internalCode = null;
            if (flag)
            {
                internalCode = string.Format("-ModuleStatus : {1}{0}", "\r\n", ex.Status);
                if (str3 != null)
                {
                    internalCode = internalCode + string.Format("-WebStatus : {1}{0}", "\r\n", str3);
                }
                if (str4 != null)
                {
                    internalCode = internalCode + string.Format("-HttpStatusCode : {1}{0}", "\r\n", str4);
                }
                if (ex.Message != null)
                {
                    internalCode = internalCode + string.Format("-ExceptionMessage{0} {1}{0}", "\r\n", ex.Message);
                }
                if (ex.InnerException != null)
                {
                    internalCode = internalCode + string.Format("-InnerException :{0} type={1}{0} {2}{0}", "\r\n", ex.InnerException.GetType(), ex.InnerException.ToString());
                }
                if (ex.TargetSite != null)
                {
                    internalCode = internalCode + string.Format("-TargetSite :{0} {1}{0}", "\r\n", ex.TargetSite.ToString());
                }
                if (ex.StackTrace != null)
                {
                    internalCode = internalCode + string.Format("-StackTrace :{0} {1}{0}", "\r\n", ex.StackTrace.ToString());
                }
            }
            if (applyStr == null)
            {
                base.ShowMessage(str, internalCode);
            }
            else
            {
                base.ShowMessage(str, applyStr, internalCode);
            }
        }

        public void ShowHttpError(int senderror, HttpExceptionEx ex)
        {
            if (senderror == 0)
            {
                this.ShowHttpError(ex);
            }
            else if (ex != null)
            {
                this.ShowHttpError(ex);
            }
            else
            {
                this.ShowSendError(senderror);
            }
        }

        public void ShowJobError(string job, IData resultData)
        {
            string jobCode = job.TrimEnd(new char[0]).ToUpper();
            if ((resultData != null) && (resultData.ItemCount > 0))
            {
                int length = 15;
                if (resultData.Items[0].Length < 15)
                {
                    length = resultData.Items[0].Length;
                }
                string resultCode = resultData.Items[0].Substring(0, length).ToUpper();
                //tuyenhm
                //jobCode = jobCode.Replace('?', 0xff1f);//0xff1f=dau ?
                jobCode = jobCode.Replace('?', '?');
                if (resultData.ItemCount > 1)
                {
                    string internalCode = null;
                    for (int i = 1; i < resultData.ItemCount; i++)
                    {
                        internalCode = internalCode + resultData.Items[i] + "\r\n";
                    }
                    base.ShowJobMessage(jobCode, resultCode, internalCode);
                }
                else
                {
                    base.ShowJobMessage(jobCode, resultCode, null);
                }
            }
        }

        public void ShowSendError(int errcode)
        {
            if (errcode != 0)
            {
                string str;
                string applyStr = null;
                string str3 = System.Enum.GetNames(typeof(HttpResult))[errcode].ToString();
                if (str3.StartsWith(HttpResult.UriError.ToString()))
                {
                    str = "E101";
                }
                else if (str3.StartsWith(HttpResult.ProxyAddressError.ToString()))
                {
                    str = "E102";
                }
                else if (str3.StartsWith(HttpResult.ProxyException.ToString()))
                {
                    str = "E102";
                }
                else if (str3.StartsWith(HttpResult.CertException.ToString()))
                {
                    str = "E103";
                }
                else if (str3.StartsWith(HttpResult.CertSelectError.ToString()))
                {
                    str = "E103";
                }
                else if (str3.StartsWith(HttpResult.CertUnestablishedError.ToString()))
                {
                    str = "E103";
                }
                else if (str3.StartsWith(HttpResult.CertValidError.ToString()))
                {
                    str = "E104";
                }
                else if (str3.StartsWith(HttpResult.StatusError.ToString()))
                {
                    str = "W124";
                }
                else
                {
                    str = "E109";
                }
                if (str == "E103")
                {
                    applyStr = Resources.ResourceManager.GetString("CORE101") + "{" + str3 + "}";
                }
                base.ShowMessage(str, applyStr, null);
            }
        }
    }
}

