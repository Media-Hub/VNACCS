namespace Naccs.Net
{
    using System;
    using System.Security.Cryptography.X509Certificates;
    using System.Windows.Forms;

    public class ClientCert
    {
        public const int CERT_NOT_SELECT = -2;
        public const int CERT_NOT_YET_VALID = -3;
        public const int CERT_UNESTABLISHED = -1;
        private X509Certificate2Collection mStoreCollection;

        public ClientCert()
        {
            this.SetStore(null);
        }

        public ClientCert(string issuer)
        {
            this.SetStore(issuer);
        }

        public bool DisplayCert(object sender, string thumbprint)
        {
            if (string.IsNullOrEmpty(thumbprint))
            {
                return false;
            }
            X509Certificate2 certificate = this.Find(thumbprint);
            if (certificate == null)
            {
                return false;
            }
            X509Certificate2UI.DisplayCertificate(certificate, ((Control) sender).Handle);
            return true;
        }

        public string DisplayIssuerName(X509Certificate2 cert)
        {
            string str = "";
            if (cert == null)
            {
                return str;
            }
            return cert.GetNameInfo(X509NameType.SimpleName, true);
        }

        public X509Certificate2 Find(string thumbprint)
        {
            X509Certificate2Enumerator enumerator = this.mStoreCollection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                X509Certificate2 current = enumerator.Current;
                if (current.Thumbprint == thumbprint)
                {
                    return current;
                }
            }
            return null;
        }

        public X509Certificate2 Select(object sender, string title, string msg, bool validOnly)
        {
            DateTime now = DateTime.Now;
            X509Certificate2Collection certificates2 = X509Certificate2UI.SelectFromCollection(this.mStoreCollection.Find(X509FindType.FindByTimeValid, now, validOnly), title, msg, X509SelectionFlag.SingleSelection, ((Control) sender).Handle);
            if (certificates2.Count > 0)
            {
                return certificates2[0];
            }
            return null;
        }

        private void SetStore(string issuer)
        {
            X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.OpenExistingOnly);
            if (string.IsNullOrEmpty(issuer))
            {
                this.mStoreCollection = store.Certificates;
            }
            else
            {
                this.mStoreCollection = store.Certificates.Find(X509FindType.FindByIssuerName, issuer, false);
            }
        }

        public int ValidDays(string thumbprint)
        {
            if (string.IsNullOrEmpty(thumbprint))
            {
                return -1;
            }
            DateTime now = DateTime.Now;
            X509Certificate2 certificate = this.Find(thumbprint);
            if (certificate == null)
            {
                return -2;
            }
            TimeSpan span = certificate.NotAfter.Subtract(now);
            if (span.Days < 0)
            {
                return -3;
            }
            return span.Days;
        }
    }
}

