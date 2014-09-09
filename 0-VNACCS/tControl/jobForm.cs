using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Validator;
using Naccs.Common.JobConfig;
using Naccs.Core.Classes;
using Naccs.Net;
using Naccs.Net.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace DevComponents.DotNetBar
{
    public partial class jobForm : DevComponents.DotNetBar.OfficeForm
    {
        private static ManualResetEvent allDone = new ManualResetEvent(false);

        public string jobCode = "";
        public string jobTitle = "";
        public string jobCodeTitle = "";
        public DataTable dtGuide = new DataTable();

        string strJobConfigPathInit = @"App_LocalResources\Template\Send\{0}";
        public string strTemplatePath = "";
        public string strGuidePath = "";
        public string strHelpPath = "";

        public string strDisplayFile = "";
        public string strGuideFile = "";
        public string strHelpFile = "";

        public bool KySo = false;//Xác định nghiệp vụ sử dụng chữ ký số

        public jobForm()
        {
            InitializeComponent();
        }

       

        public jobForm(string JobCode)
        {
            InitializeComponent();

            #region Xử lý nghiệp vụ
            //Xác định jobcode để load config
            this.jobCode = JobCode;

            //Template path
            strTemplatePath = Path.Combine(Application.StartupPath, @"App_LocalResources\Template"); if (Directory.Exists(strTemplatePath) == false) Directory.CreateDirectory(strTemplatePath);
            strGuidePath = Path.Combine(Application.StartupPath, @"App_LocalResources\Help\guide\"); if (Directory.Exists(strGuidePath) == false) Directory.CreateDirectory(strGuidePath);
            strHelpPath = Path.Combine(Application.StartupPath, @"App_LocalResources\Help\gym_err\"); if (Directory.Exists(strHelpPath) == false) Directory.CreateDirectory(strHelpPath);

            //Đường dẫn file config cho job này
            string strJobConfigPath = Path.Combine(Application.StartupPath, string.Format(strJobConfigPathInit, jobCode));
            if (Directory.Exists(strJobConfigPath) == false) Directory.CreateDirectory(strJobConfigPath);
            string strJobConfigFileName=string.Format("{0}.conf",jobCode);
            string strJobConfigFilePath = Path.Combine(strJobConfigPath, strJobConfigFileName);
            clsAll.ExtractJobFile(strJobConfigPath, strJobConfigFileName);
            //Load file config
            AbstractJobConfig c1 = new NormalConfig();
            JobConfigFactory.TemplatePath = strTemplatePath;
            JobConfigFactory.GuidePath = strGuidePath;
            JobConfigFactory.GymErrPath = strHelpPath;
            JobConfigFactory.createJobConfig(jobCode, new DateTime(), out c1);
            NormalConfig jobConfig = (c1 as NormalConfig);
            jobTitle = jobConfig.record.titile1;
            this.KySo = jobConfig.record.signflg.Equals(1);

            //Title
            jobCodeTitle = string.Format("{0} - {1}", jobCode, jobTitle);
            this.Text = jobCodeTitle;
            this.TITLE.Text = jobCodeTitle;

            //Diplay file
            strDisplayFile = jobConfig.Display;
            clsAll.ExtractJobFile(strJobConfigPath, Path.GetFileName(strDisplayFile));
            strGuideFile = jobConfig.Guide;
            clsAll.ExtractJobFile(strGuidePath, Path.GetFileName(strGuideFile));
            strHelpFile = jobConfig.Help;
            clsAll.ExtractJobFile(strHelpPath, Path.GetFileName(strHelpFile));

            #region Load guide file hướng dẫn
            XmlDocument docGuide = new XmlDocument();
            docGuide.Load(strGuideFile);
            DataColumn cID = new DataColumn("ID");
            dtGuide.Columns.Add(cID);
            DataColumn cGuideText = new DataColumn("GuideText");
            dtGuide.Columns.Add(cGuideText);
            foreach (XmlNode n in docGuide.SelectNodes("guide/item"))
            {
                DataRow r = dtGuide.NewRow();
                r[0] = n.Attributes["id"].Value;
                r[1] = n.InnerText;
                dtGuide.Rows.Add(r);
            }
            #endregion

           
            #endregion
        }

        private void jobForm_Load(object sender, EventArgs e)
        {
             #region Add help kiểu cổ
            AddHelpCalssicalType(this.CONTENT);
            #endregion
        }

        private void AddHelpCalssicalType(Control c)
        {
            if (c is LabelX || c is Label)
            {
                return;
            }

            if (c is TextBoxX)
            {
                TextBoxX txt = c as TextBoxX;
                txt.Enter += txt_Enter;
                return;
            }

            if (c is tCodeList)
            {
                tCodeList cl = c as tCodeList;
                cl.Enter += cl_Enter;
                return;
            }

            if (c is ComboBoxEx)
            {
                ComboBoxEx cb = c as ComboBoxEx;
                cb.Enter += cb_Enter;
                return;
            }

            if (c is SuperTabControl)
            {
                SuperTabControl tab = c as SuperTabControl;
                foreach (Control c1 in tab.Controls)
                {
                    AddHelpCalssicalType(c1);
                }
                return;
            }
            
            if (c is SuperTabControlPanel)
            {
                SuperTabControlPanel tab = c as SuperTabControlPanel;
                foreach (Control c1 in tab.Controls)
                {
                    AddHelpCalssicalType(c1);
                }
                return;
            }

            if (c is PanelEx)
            {
                PanelEx panel = c as PanelEx;
                foreach (Control c1 in panel.Controls)
                {
                    AddHelpCalssicalType(c1);
                }
                return;
            }

            if (c is GroupPanel)
            {
                GroupPanel panel = c as GroupPanel;
                foreach (Control c1 in panel.Controls)
                {
                    AddHelpCalssicalType(c1);
                }
                return;
            }           
        }

        void cb_Enter(object sender, EventArgs e)
        {
            ComboBoxEx cl = sender as ComboBoxEx;
            DataRow[] rr = dtGuide.Select(string.Format("ID='{0}'", cl.Name));
            if (rr == null) return;
            if (rr.Length < 1) return;
            lblHelp.Text = string.Format("{0}", rr[0]["GuideText"]);
        }

        void cl_Enter(object sender, EventArgs e)
        {
            tCodeList cl = sender as tCodeList;
            DataRow[] rr = dtGuide.Select(string.Format("ID='{0}'", cl.Name));
            if (rr == null) return;
            if (rr.Length < 1) return;
            lblHelp.Text = string.Format("{0}", rr[0]["GuideText"]);
        }

        void txt_Enter(object sender, EventArgs e)
        {
            TextBoxX txt = sender as TextBoxX;
            DataRow[] rr = dtGuide.Select(string.Format("ID='{0}'", txt.Name));
            if (rr == null) return;
            if (rr.Length < 1) return;
            lblHelp.Text = string.Format("{0}", rr[0]["GuideText"]);
        }

        public string DateTimeString()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss", DateTimeFormatInfo.InvariantInfo);
        }

        private string SendMSG = "";
        private ComParameter sendData;
        private byte[] byteArray;
        public ComParameter SendData(string strSendMSG)
        {
            try
            {
                this.SendMSG = strSendMSG;
                sendData = new ComParameter();
                sendData.DataString = SendMSG;
                sendData.Sign = (this.KySo == true) ? "1" : "";
                if (this.KySo == true)//set cert để ký
                {
                    X509Certificate2 storedCert =
                    CryptographyHelper.GetStoreX509Certificate2BySerial("5404287E6CC0BC4DA008B43C710C0D5A");
                    sendData.SelectSign = storedCert;

                    //test
                    RSACryptography rsaC = new RSACryptography(storedCert);

                    string strKetQuaKy = rsaC.SignHash("sdfdf");
                }

                returnParameter = null;
                allDone.Reset();

                //chống lỗi ssl
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                // Create a new HttpWebRequest object.
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(st.fIPHQ);

                // Set the Method property to 'POST' to post data to the URI.
                request.Method = "POST";

                request.Credentials = CredentialCache.DefaultCredentials;
                request.KeepAlive = false;
                request.ProtocolVersion = HttpVersion.Version11;
                request.UserAgent = "NACCS Terminal Software";
                request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
                request.Method = "POST";

                request.SendChunked = false;
                request.ContentType = "text/plain; charset=UTF-8";

                //Tao send data
                HttpClient h = new HttpClient();
                byteArray = h.MakeSendData(request, sendData).ToArray();

                // start the asynchronous operation
                request.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), request);

                // Keep the main thread from continuing while the asynchronous 
                // operation completes. A real world application 
                // could do something useful such as updating its user interface. 
                allDone.WaitOne();

                string checkPoint = "";

                return returnParameter;
            }
            catch (Exception ex)
            {
                this.Show1(ex.Message, "Vui lòng liên hệ nhà cung cấp phần mềm. Hotline: 0917 863 688  (Mr. Tuyển)");
                return null;
            }
        }

        private void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            //chống lỗi ssl
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

            // End the operation
            Stream postStream = request.EndGetRequestStream(asynchronousResult);

            //Console.WriteLine("Please enter the input data to be posted:");
            //string postData = Console.ReadLine();

          //  // Convert the string into a byte array. 
          //  byte[] byteArray = Encoding.UTF8.GetBytes(SendMSG);           

            // Write to the request stream.
            postStream.Write(byteArray, 0, byteArray.Length);
            postStream.Close();

            // Start the asynchronous operation to get the response
            request.BeginGetResponse(new AsyncCallback(GetResponseCallback), request);
        }

        ComParameter returnParameter;
        private void GetResponseCallback(IAsyncResult asynchronousResult)
        {
            //chống lỗi ssl
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

            // End the operation
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
            Stream streamResponse = response.GetResponseStream();

            StreamReader streamRead = new StreamReader(streamResponse);

            ComParamBuilder mBuilder = new ComParamBuilder();
            mBuilder.BaseCharset = "UTF-8";

            MemoryStream sMIMEmessage = mBuilder.ReadToMemory(streamResponse);
            returnParameter = mBuilder.ToTextComParameter(sMIMEmessage, "UTF-8");

            string responseString = streamRead.ReadToEnd();
            Console.WriteLine(responseString);
            //lblMSG.Text = responseString;
            //Literal1.Text = responseString;

            // Close the stream object
            streamResponse.Close();
            streamRead.Close();

            // Release the HttpWebResponse
            response.Close();
            allDone.Set();
        }

        public IData CreateRequestData()
        {
            string mTermLogicalName = st.fTerminalID;
            string mTermAccessKey = st.fTerminalAccessKey;
            string mUser = st.fUserIDVNACCS;
            string mPass = st.fPasswordVNACCS;
            int mSendCount = 0;
            string str = this.jobCode;

            mTermLogicalName = mTermLogicalName.PadRight(6, ' ');
            mTermAccessKey = mTermAccessKey.PadRight(0x10, ' ');
            IData data = new NaccsData
            {
                Header =
                {
                    Control = DataControl.GetControl(),
                    JobCode = str,
                    UserId = mUser,
                    Password = mPass,
                    Path = mTermLogicalName,
                    DataInfo = string.Format("{0}{1}{2:D6}", str.PadRight(5, ' '), this.DateTimeString(), mSendCount)
                }
            };


            bool mLogon = false;//mLogon để chỉ ra rằng header này dùng để logon chứ ko phải là header của của nghiệp vụ khác
            if (mLogon)
            {
                data.Header.SendGuard = "*";
            }


            Naccs.Core.Job.JobPanel JP = new Naccs.Core.Job.JobPanel();
            JP.CreatePanel(this.strDisplayFile);
            //JP.Items.DsItems.Tables[0].Rows[0][ICN.ID] = ICN.Text;
            //JP.Items.DsItems.Tables[0].Rows[0][HAB.ID] = HAB.Text;
            //JP.Items.DsItems.Tables[0].Rows[0][BLN.ID] = BLN.Text;
            //JP.Items.DsItems.Tables[0].Rows[0][IVU.ID] = IVU.Text;
            foreach (Control c in this.CONTENT.Controls)
            {
                if (c is TextBoxX)
                {
                    TextBoxX txt = c as TextBoxX;
                    JP.Items.DsItems.Tables[0].Rows[0][txt.Name] = txt.Text;
                }
            }
            string strJobData = JP.GetData(Naccs.Common.Generator.ItemInfo.JobDataStatus.jdSend);
            data.JobData = strJobData;

            return data;
        }
        public string CreateRequestString()
        {
            IData data = this.CreateRequestData();
            return data.GetDataString();
        }

       public delegate void DelegateShow1(string strNoiDungLoi,string strCachKhacPhuc);
       public void Show1(string strNoiDungLoi, string strCachKhacPhuc)
        {
            if (this.InvokeRequired == true)
            {
                DelegateShow1 show1 = new DelegateShow1(Show1);
                this.Invoke(show1, strNoiDungLoi, strCachKhacPhuc);
            }
            else
            {
                tDialog dlg = new tDialog();
                dlg.txtNoiDungLoi.Text = strNoiDungLoi;
                dlg.txtKhacPhuc.Text = strCachKhacPhuc;
                dlg.ShowDialog();
            }
        }

       public void ShowLoiHeThong(string strNoiDungLoi)
       {
           //Tìm cách khắc phục trong db học lỗi
           //nếu ko có thì bảo liên hệ với tuyển

           //chưa code

           //else
           this.Show1(strNoiDungLoi, "Vui lòng liên hệ nhà cung cấp phần mềm. Hotline: 0917 863 688  (Mr. Tuyển)");
       }

       private void jobForm_FormClosing(object sender, FormClosingEventArgs e)
       {
           e.Cancel = false;
       }

        bool isValid = true;
        public bool CheckControls(Control cRoot)
        {
            isValid = true;
            return ValidateControls(cRoot);
        }
        private bool ValidateControls(Control cRoot)
        {
            if (cRoot is PanelEx || cRoot is GroupPanel)
            {
                foreach (Control c in cRoot.Controls)
                {
                    isValid = ValidateControls(c);
                }
            }

            //Xử lý cho tTextBox
            if (cRoot is tTextBox)
            {
                tTextBox c = cRoot as tTextBox;
                if (c.Enabled == false || c.Visible == false )
                {
                    //do nothing
                }
                else
                {
                    if (c.BắtBuộc == true)
                    {
                        if (string.IsNullOrEmpty(c.Text.Trim()))
                        {
                            hlBatBuoc.SetHighlightColor(c, eHighlightColor.Red);
                            tipBatBuoc.SetSuperTooltip(c, new SuperTooltipInfo("", "", "Dữ liệu bắt buộc", null, null, eTooltipColor.Lemon, false, false, new Size(0, 0)));
                            isValid = false;
                        }
                        else
                        {
                            hlBatBuoc.SetHighlightColor(c, eHighlightColor.None);
                            tipBatBuoc.SetSuperTooltip(c,null );
                        }
                    }
                }
            }

            //Xử lý cho tCodeList
            if (cRoot is tCodeList)
            {
                tCodeList c = cRoot as tCodeList;
                if (c.Enabled == false || c.Visible == false)
                {
                    //do nothing
                }
                else
                {
                    if (c.BắtBuộc == true)
                    {
                        if (string.IsNullOrEmpty(c.Text.Trim()))
                        {
                            hlBatBuoc.SetHighlightColor(c, eHighlightColor.Red);
                            c.SetToolTip(tipBatBuoc);
                            isValid = false;
                        }
                        else
                        {
                            hlBatBuoc.SetHighlightColor(c, eHighlightColor.None);
                            c.ClearToolTip(tipBatBuoc);
                        }
                    }
                }
            }

            return isValid;
        }
    }
}
