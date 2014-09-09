namespace Naccs.Net
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Security.Cryptography.Pkcs;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    public class SMimeFormat
    {
        private int bcount = 3;
        private string CertificateErr = "";
        private string Disposition = "attachment";
        private string fileName = "smime.p7s";
        private string signdSystem = "application/x-pkcs7-signature";
        private int size = 0x20;
        private string ThisBO = "";
        private string tranceferEncording = "base64";

        public MemoryStream ConvertSMIME(MemoryStream message, string Header, X509Certificate2 SingerCert)
        {
            MemoryStream stream2;
            string str = Encoding.UTF8.GetString(message.ToArray());
            if (Header != "")
            {
                str = ("Content-Type: " + Header + "\r\n\r\n") + str;
            }
            string sign = "";
            try
            {
                sign = this.CreateDigitalSignature(str, SingerCert);
                if (sign != null)
                {
                    string s = this.CreateMIMEMessage(str, sign);
                    return new MemoryStream(Encoding.UTF8.GetBytes(s));
                }
                stream2 = null;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return stream2;
        }

        private string CreateDigitalSignature(string message, X509Certificate2 Sign)
        {
            string str;
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                str = Convert.ToBase64String(this.SignMsg(bytes, Sign), Base64FormattingOptions.InsertLineBreaks);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str;
        }

        private string CreateMIMEMessage(string msg, string sign)
        {
            StringBuilder builder = new StringBuilder();
            this.ThisBO = this.CreateBoundaryString;
            string str = "This is an S/MIME signed message";
            string str2 = "Content-Type: " + this.signdSystem + "; name=\"" + this.fileName + "\"";
            string str3 = "Content-Transfer-Encoding: " + this.tranceferEncording;
            string str4 = "Content-Disposition: " + this.Disposition + "; filename=\"" + this.fileName + "\"";
            builder.AppendLine(str);
            builder.AppendLine("");
            builder.AppendLine("--" + this.ThisBO);
            builder.AppendLine(msg);
            builder.AppendLine("--" + this.ThisBO);
            builder.AppendLine(str2);
            builder.AppendLine(str3);
            builder.AppendLine(str4);
            builder.AppendLine("");
            builder.AppendLine(sign);
            builder.AppendLine("");
            builder.AppendLine("--" + this.ThisBO + "--");
            builder.AppendLine("");
            return builder.ToString();
        }

        public bool SignCheck(string SMIMEmessage)
        {
            bool flag;
            string[] strArray = this.SMimeParther(SMIMEmessage, "");
            try
            {
                flag = this.VerifySig(strArray[1], strArray[0]);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public byte[] SignMsg(byte[] msg, X509Certificate2 signerCert)
        {
            byte[] buffer;
            try
            {
                ContentInfo contentInfo = new ContentInfo(msg);
                SignedCms cms = null;
                cms = new SignedCms(contentInfo, true);
                CmsSigner signer = new CmsSigner(signerCert);
                signer.IncludeOption = X509IncludeOption.EndCertOnly;//TuyenHM
                cms.ComputeSignature(signer, false);
                buffer = cms.Encode();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return buffer;
        }

        public MemoryStream SignParth(MemoryStream SMIMEmessage, string boudary)
        {
            MemoryStream stream;
            string message = Encoding.UTF8.GetString(SMIMEmessage.ToArray());
            string[] strArray = this.SMimeParther(message, boudary);
            try
            {
                stream = new MemoryStream(Encoding.UTF8.GetBytes(strArray[0]));
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return stream;
        }

        private string[] SMimeParther(string message, string boundary = "")
        {
            string[] strArray = message.Split(new char[] { '\n' });
            ArrayList list = new ArrayList();
            string[] strArray2 = null;
            int index = 0;
            if (boundary == "")
            {
                strArray2 = strArray[0].Split(new char[] { ';' });
                for (int k = 0; k <= (strArray2.Length - 1); k++)
                {
                    if (strArray2[k].Contains("boundary"))
                    {
                        index = k;
                        break;
                    }
                }
                boundary = strArray2[index].Replace(" boundary=", "").Replace("\"", "");
                if (boundary.Contains("\r"))
                {
                    boundary = boundary.Replace("\r", "");
                }
            }
            for (int i = 0; i <= (strArray.Length - 1); i++)
            {
                if (strArray[i].StartsWith("--" + boundary))
                {
                    list.Add(i);
                }
            }
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            if (list.Count != this.bcount)
            {
                throw new Exception("Decoding error telegram signed");
            }
            int num4 = (int) list[0];
            num4++;
            int num5 = (int) list[1];
            num5 -= 2;
            int num6 = (int) list[1];
            num6 += 4;
            int num7 = (int) list[2];
            num7--;
            if (num4 <= num5)
            {
                for (int m = num4; m <= num5; m++)
                {
                    builder.Append(strArray[m] + "\n");
                }
            }
            else
            {
                builder.Append(strArray[num4] + "\n");
            }
            for (int j = num6; j <= num7; j++)
            {
                if (strArray[j] != "")
                {
                    builder2.Append(strArray[j]);
                }
            }
            return new string[] { builder.ToString(), builder2.ToString().Trim() };
        }

        private string statuschk(X509Chain ch)
        {
            string str = "";
            ArrayList list = new ArrayList();
            for (int i = 0; i <= (ch.ChainStatus.Length - 1); i++)
            {
                list.Add(ch.ChainStatus[i].Status);
            }
            if (list.Contains(X509ChainStatusFlags.UntrustedRoot))
            {
                str = "UntrustedRoot";
            }
            else if (list.Contains(X509ChainStatusFlags.NotTimeValid))
            {
                str = "NotTimeValid";
            }
            else if (list.Contains(X509ChainStatusFlags.Revoked))
            {
                str = "Revoked";
            }
            else if (((list.Count == 2) && list.Contains(X509ChainStatusFlags.OfflineRevocation)) && list.Contains(X509ChainStatusFlags.RevocationStatusUnknown))
            {
                str = "CRL does not exist";
            }
            else if ((list.Count == 1) && list.Contains(X509ChainStatusFlags.RevocationStatusUnknown))
            {
                str = "CRL does not exist";
            }
            else
            {
                str = "Other";
            }
            return str.ToString();
        }

        private bool VerifySig(string signeture, string message)
        {
            SignedCms cms = null;
            bool flag;
            this.CertificateErr = "";
            byte[] encodedMessage = Convert.FromBase64String(signeture);
            ContentInfo contentInfo = new ContentInfo(Encoding.UTF8.GetBytes(message));
            cms = new SignedCms(contentInfo, true);
            X509Chain ch = new X509Chain();
            try
            {
                cms.Decode(encodedMessage);
                ch.ChainPolicy.RevocationMode = X509RevocationMode.Offline;
                ch.ChainPolicy.VerificationFlags = X509VerificationFlags.IgnoreRootRevocationUnknown;
                if (ch.Build(cms.Certificates[0]))
                {
                    cms.CheckSignature(true);
                    return true;
                }
                string str = this.statuschk(ch);
                if (str == "CRL does not exist")
                {
                    cms.CheckSignature(true);
                    return true;
                }
                this.CertificateErr = str;
                flag = false;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        private string CreateBoundaryString
        {
            get
            {
                string str = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                byte[] data = new byte[this.size];
                new RNGCryptoServiceProvider().GetNonZeroBytes(data);
                string str2 = string.Empty;
                byte[] buffer2 = data;
                for (int i = 0; i < buffer2.Length; i++)
                {
                    int seed = buffer2[i];
                    int num2 = new Random(seed).Next(str.Length);
                    str2 = str2 + str[num2];
                }
                return string.Format("{0}{1}", "----", str2.ToString().Replace("-", ""));
            }
        }

        public string GetThisBoundary
        {
            get
            {
                return this.ThisBO;
            }
        }

        public string GetThisCertificateErr
        {
            get
            {
                return this.CertificateErr;
            }
        }
    }
}

