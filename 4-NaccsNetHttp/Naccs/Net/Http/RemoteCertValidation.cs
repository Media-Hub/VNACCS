namespace Naccs.Net.Http
{
    using System;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;

    public class RemoteCertValidation
    {
        private static RemoteCertValidation Instance;
        private string mCertHashString;
        private SslPolicyErrors mErrors;
        private bool mValidated;

        public static RemoteCertValidation CreateInstance()
        {
            if (Instance == null)
            {
                Instance = new RemoteCertValidation();
            }
            return Instance;
        }

        public bool Validation(X509Certificate cert, SslPolicyErrors errors)
        {
            if (errors == SslPolicyErrors.None)
            {
                this.Validated = true;
            }
            else if (string.IsNullOrEmpty(this.mCertHashString))
            {
                this.Validated = false;
            }
            else if ((this.mCertHashString != cert.GetCertHashString()) || (this.mErrors != errors))
            {
                this.Validated = false;
            }
            this.mCertHashString = cert.GetCertHashString();
            this.mErrors = errors;
            return this.Validated;
        }

        public bool Validated
        {
            get
            {
                return this.mValidated;
            }
            set
            {
                this.mValidated = value;
            }
        }
    }
}

