namespace Naccs.Core.Classes
{
    using Naccs.Net;
    using System;

    public class MailErrorDlg : MessageDialog
    {
        private const string CRLF = "\r\n";

        public void ShowMailError(MailExceptionEx ex)
        {
            string str;
            bool flag = false;
            string applyStr = null;
            if (ex.Cause == MailCause.UserCancel)
            {
                if (ex.Status == MailStatus.Send)
                {
                    str = "E215";
                }
                else
                {
                    str = "E225";
                }
            }
            else if (ex.Cause == MailCause.Timeout)
            {
                if (ex.Status == MailStatus.Send)
                {
                    str = "E214";
                }
                else
                {
                    str = "E224";
                }
            }
            else if (ex.Cause == MailCause.SmtpDnsException)
            {
                str = "E211";
                if (ex.Message != null)
                {
                    applyStr = ex.Message;
                }
                flag = true;
            }
            else if (ex.Cause == MailCause.SmtpException)
            {
                str = "E212";
                if (ex.Message != null)
                {
                    applyStr = ex.Message;
                }
                flag = true;
            }
            else if (ex.Cause == MailCause.SmtpErrorResponse)
            {
                str = "E213";
                if (ex.Message != null)
                {
                    applyStr = ex.Message;
                }
                flag = true;
            }
            else if (ex.Cause == MailCause.PopDnsException)
            {
                str = "E221";
                if (ex.Message != null)
                {
                    applyStr = ex.Message;
                }
                flag = true;
            }
            else if (ex.Cause == MailCause.PopException)
            {
                str = "E222";
                if (ex.Message != null)
                {
                    applyStr = ex.Message;
                }
                flag = true;
            }
            else if (ex.Cause == MailCause.PopErrorResponse)
            {
                if (ex.Status == MailStatus.Open)
                {
                    str = "E231";
                }
                else
                {
                    str = "E223";
                }
                if (ex.Message != null)
                {
                    applyStr = ex.Message;
                }
                flag = true;
            }
            else if (ex.Status == MailStatus.Send)
            {
                str = "E219";
            }
            else
            {
                str = "E229";
            }
            string internalCode = null;
            if (flag)
            {
                internalCode = string.Format("-ModuleStatus : {1}{0}", "\r\n", ex.Status);
                if (ex.SocketErrorCode != 0)
                {
                    internalCode = internalCode + string.Format("-SoketErrorCode : {0} {1}({2}){0}", "\r\n", ex.SocketErrorCode, ex.SocketErrorCodeType.ToString());
                }
                if (ex.Message != null)
                {
                    if (ex.ErrorResponse)
                    {
                        internalCode = internalCode + string.Format("-ServerErrorMessage{0} {1}{0}", "\r\n", ex.Message);
                    }
                    else
                    {
                        internalCode = internalCode + string.Format("-ExceptionMessage{0} {1}{0}", "\r\n", ex.Message);
                    }
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
                    internalCode = internalCode + string.Format("-StackTrace :{0} {1}{0}", "\r\n", ex.StackTrace);
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

        public void ShowRequestError(MailStatus status, int errcode)
        {
            if (errcode != 0)
            {
                string str;
                if (Enum.GetNames(typeof(MailResult))[errcode].ToString().StartsWith(MailResult.StatusError.ToString()))
                {
                    if (status == MailStatus.Open)
                    {
                        str = "E201";
                    }
                    else if (status == MailStatus.Send)
                    {
                        str = "E202";
                    }
                    else if (status == MailStatus.Receive)
                    {
                        str = "E203";
                    }
                    else if (status == MailStatus.List)
                    {
                        str = "E204";
                    }
                    else if (status == MailStatus.Delete)
                    {
                        str = "E205";
                    }
                    else
                    {
                        str = "E209";
                    }
                }
                else
                {
                    str = "E209";
                }
                base.ShowMessage(str, null, null);
            }
        }
    }
}

