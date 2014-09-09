namespace Naccs.Net
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography.X509Certificates;

    public class SignaturesCert
    {
        private X509Certificate2 signerCert;

        private X509Certificate2Collection GetCertificate2Collection(bool mode, IntPtr handle=default(IntPtr))
        {
            X509Store store = null;
            store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.OpenExistingOnly);
            X509Certificate2Collection certificates = store.Certificates;
            X509Certificate2Collection certificates2 = certificates.Find(X509FindType.FindByTimeValid, DateTime.Now, true);
            X509Certificate2Collection certificates3 = certificates.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, true);
            if (mode)
            {
                string certHashString = this.Cert.GetCertHashString();
                return certificates2.Find(X509FindType.FindByThumbprint, certHashString, true);
            }
            return X509Certificate2UI.SelectFromCollection(certificates3, "Certificate choose", "Choose a certificate for your signature.", X509SelectionFlag.SingleSelection, handle);
        }

        public void GetSigneture(IntPtr handle)
        {
            try
            {
                if (this.signerCert != null)
                {
                    X509Certificate2Collection certificates = this.GetCertificate2Collection(true, new IntPtr());
                    if (certificates.Count == 0)
                    {
                        certificates = new X509Certificate2Collection();
                        certificates = this.GetCertificate2Collection(false, handle);
                        if (certificates.Count == 0)
                        {
                            throw new Exception();
                        }
                        this.Cert = certificates[0];
                    }
                    else
                    {
                        this.Cert = certificates[0];
                    }
                }
                else
                {
                    X509Certificate2Collection certificates2 = this.GetCertificate2Collection(false, handle);
                    if (certificates2.Count == 0)
                    {
                        throw new Exception();
                    }
                    this.Cert = certificates2[0];
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public X509Certificate2 Cert
        {
            get
            {
                return this.signerCert;
            }
            set
            {
                this.signerCert = value;
            }
        }
    }
}

