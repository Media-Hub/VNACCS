namespace Naccs.Net
{
    using System;
    using System.Net;
    using System.Reflection;

    public class HttpExceptionEx
    {
        private HttpCause mCause;
        private Exception mException;
        private HttpWebResponse mHttpErrorResponse;
        private HttpStatus mStatus;
        private WebExceptionStatus mWebStatus;

        public HttpExceptionEx(HttpStatus status, HttpCause cause, object baseException)
        {
            this.InitOption(status, cause);
            if (baseException.GetType() == typeof(WebException))
            {
                this.mException = (WebException) baseException;
                this.mWebStatus = ((WebException) baseException).Status;
                this.mHttpErrorResponse = (HttpWebResponse) ((WebException) baseException).Response;
            }
            else
            {
                this.mException = (Exception) baseException;
            }
        }

        public HttpExceptionEx(HttpStatus status, HttpCause cause, string message)
        {
            this.InitOption(status, cause);
            try
            {
                Exception exception = new Exception(message);
                throw exception;
            }
            catch (Exception exception2)
            {
                this.mException = exception2;
            }
        }

        public Exception GetBaseException()
        {
            return this.mException.GetBaseException();
        }

        private void InitOption(HttpStatus status, HttpCause cause)
        {
            this.mStatus = status;
            this.mCause = cause;
            this.mHttpErrorResponse = null;
            this.mWebStatus = WebExceptionStatus.Success;
        }

        public HttpCause Cause
        {
            get
            {
                return this.mCause;
            }
        }

        public HttpWebResponse HttpErrorResponse
        {
            get
            {
                return this.mHttpErrorResponse;
            }
        }

        public HttpStatusCode HttpErrorStatusCode
        {
            get
            {
                return this.mHttpErrorResponse.StatusCode;
            }
        }

        public string HttpErrorStatusDescription
        {
            get
            {
                return this.mHttpErrorResponse.StatusDescription;
            }
        }

        public Exception InnerException
        {
            get
            {
                return this.mException.InnerException;
            }
        }

        public string Message
        {
            get
            {
                return this.mException.Message;
            }
        }

        public string Source
        {
            get
            {
                return this.mException.Source;
            }
        }

        public string StackTrace
        {
            get
            {
                return this.mException.StackTrace;
            }
        }

        public HttpStatus Status
        {
            get
            {
                return this.mStatus;
            }
        }

        public MethodBase TargetSite
        {
            get
            {
                return this.mException.TargetSite;
            }
        }

        public WebExceptionStatus WebStatus
        {
            get
            {
                return this.mWebStatus;
            }
        }
    }
}

