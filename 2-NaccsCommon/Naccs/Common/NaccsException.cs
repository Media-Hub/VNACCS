namespace Naccs.Common
{
    using System;
    using System.Windows.Forms;

    public class NaccsException : ApplicationException
    {
        private int adint;
        private MessageBoxButtons button;
        private int code;
        private string detail;
        private DateTime exceptionTime;
        private MessageKind kind;
        private string machineName;

        public NaccsException()
        {
            this.machineName = "";
            this.exceptionTime = DateTime.Now;
            this.kind = MessageKind.Information;
            this.code = 0;
            this.detail = "";
            this.button = MessageBoxButtons.OK;
            this.adint = 0;
        }

        public NaccsException(MessageKind kind, int code, string detail) : this()
        {
            this.kind = kind;
            this.code = code;
            this.detail = detail;
        }

        public NaccsException(MessageKind kind, int code, string detail, MessageBoxButtons button) : this()
        {
            this.kind = kind;
            this.code = code;
            this.detail = detail;
            this.button = button;
        }

        public NaccsException(MessageKind kind, int code, string detail, MessageBoxButtons button, int adint)
        {
            this.machineName = "";
            this.exceptionTime = DateTime.Now;
            this.kind = kind;
            this.code = code;
            this.detail = detail;
            this.button = button;
            this.adint = adint;
        }

        public int Adint
        {
            get
            {
                return this.adint;
            }
            set
            {
                this.adint = value;
            }
        }

        public MessageBoxButtons Button
        {
            get
            {
                return this.button;
            }
            set
            {
                this.button = value;
            }
        }

        public int Code
        {
            get
            {
                return this.code;
            }
            set
            {
                this.code = value;
            }
        }

        public string Detail
        {
            get
            {
                return this.detail;
            }
            set
            {
                this.detail = value;
            }
        }

        public DateTime ExceptionTime
        {
            get
            {
                return this.exceptionTime;
            }
        }

        public MessageKind Kind
        {
            get
            {
                return this.kind;
            }
            set
            {
                this.kind = value;
            }
        }

        public string MachineName
        {
            get
            {
                return this.machineName;
            }
            set
            {
                this.machineName = value;
            }
        }
    }
}

