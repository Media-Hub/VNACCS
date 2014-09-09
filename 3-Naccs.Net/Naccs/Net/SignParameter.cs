namespace Naccs.Net
{
    using System;
    using System.Security.Cryptography.X509Certificates;

    public class SignParameter
    {
        private string mSignFlg = "";
        private string mSignFolder = "";
        private X509Certificate2 SignName;

        public X509Certificate2 SelectSign
        {
            get
            {
                return this.SignName;
            }
            set
            {
                this.SignName = value;
            }
        }

        public string Sign
        {
            get
            {
                return this.mSignFlg;
            }
            set
            {
                this.mSignFlg = value;
            }
        }

        public string SignFile
        {
            get
            {
                return this.mSignFolder;
            }
            set
            {
                this.mSignFolder = value;
            }
        }
    }
}

