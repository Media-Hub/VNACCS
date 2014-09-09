namespace Naccs.Net
{
    using System;
    using System.Net.Sockets;
    using System.Reflection;

    public class MailExceptionEx
    {
        private MailCause mCause;
        private Exception mException;
        private int mSocketErrorCode;
        private SocketError mSocketErrorCodeType;
        private MailStatus mStatus;

        public MailExceptionEx(MailStatus status, MailCause cause, object baseException)
        {
            this.InitOption(status, cause);
            if (baseException.GetType() == typeof(TimeoutException))
            {
                this.mException = ((TimeoutException) baseException).GetBaseException();
            }
            else if (baseException.GetType() == typeof(SocketException))
            {
                this.mException = (SocketException) baseException;
                this.mSocketErrorCode = ((SocketException) baseException).ErrorCode;
                this.mSocketErrorCodeType = ((SocketException) baseException).SocketErrorCode;
            }
            else
            {
                this.mException = (Exception) baseException;
            }
        }

        public MailExceptionEx(MailStatus status, MailCause cause, string message)
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

        private void InitOption(MailStatus status, MailCause cause)
        {
            this.mStatus = status;
            this.mCause = cause;
            this.mSocketErrorCode = 0;
            this.mSocketErrorCodeType = SocketError.Success;
        }

        public MailCause Cause
        {
            get
            {
                return this.mCause;
            }
        }

        public bool ErrorResponse
        {
            get
            {
                if (this.mCause != MailCause.PopErrorResponse)
                {
                    return (this.mCause == MailCause.SmtpErrorResponse);
                }
                return true;
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

        public int SocketErrorCode
        {
            get
            {
                return this.mSocketErrorCode;
            }
        }

        public SocketError SocketErrorCodeType
        {
            get
            {
                return this.mSocketErrorCodeType;
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

        public MailStatus Status
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
    }
}

