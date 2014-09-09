using Naccs.Core.Classes;
using Naccs.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace DevComponents.DotNetBar
{
    public partial class frmIDB : DevComponents.DotNetBar.jobForm
    {
        public frmIDB():base("IDB")
        {            
            InitializeComponent();
            progressBarX1.TextVisible = false;
        }

        private void frmIDB_Load(object sender, EventArgs e)
        {
           
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (bwSend.IsBusy == true) return;
            EnableControls(false);
            bwSend.RunWorkerAsync();
        }

        private void EnableControls(bool isEnabled)
        {
            progressBarX1.TextVisible = !isEnabled;
            btnSend.Enabled = isEnabled;
            btnThoat.Enabled = isEnabled;
            this.ControlBox = isEnabled;
        }

        private void XuLyTraVe(ComParameter recvdata)
        {
            bool flag = false;
            bool flag2 = false;
            if (recvdata.DataString.Length < 400)
            {
                if (recvdata.DataString.Equals("LOGON OK"))
                {
                    ////LOGON OK
                    //lblMSG.Text = string.Format("Đăng nhập thành công. Hệ thống thông báo: {0}", recvdata.DataString);
                    
                }
                else//case này Chưa xác định được nội dung là gì
                {
                    //lblMSG.Text = string.Format("Hệ thống thông báo: {0}", recvdata.DataString);
                    //ModalPopupExtender.Show();
                }
            }
            else
            {
                IData data = new NaccsData
                {
                    Header = { DataString = recvdata.DataString.Substring(0, 400) }
                };

                flag = data.Header.Control.EndsWith("P");
                flag2 = (!flag) && (data.Header.DataType == "R");
            }

            try
            {
                if (flag)
                {

                }
                else if (flag2)
                {
                    flag = false;

                    RecvData data2 = DataFactory.CreateRecvData(recvdata.DataString);
                    //HttpErrorDlg dlg = new HttpErrorDlg();
                    //dlg.ShowJobError(this.JobCode(), data2.ResultData);
                    IData resultData = data2.ResultData;
                    int length = 15;
                    if (resultData.Items[0].Length < 15)
                    {
                        length = resultData.Items[0].Length;
                    }
                    string resultCode = resultData.Items[0].Substring(0, length).ToUpper();
                    string jobCodeForReturn = this.jobCode;
                    string messageCode = resultCode.Substring(0, 5);

                    XmlDocument document = new XmlDocument();

                    if (messageCode.IndexOf('A') == 0)//Kiểu lỗi hệ thống thì sử dụng file help kiểu hệ thống
                    {
                        jobCodeForReturn = "SYS";
                        string strSysHelpFile = Path.Combine(Application.StartupPath, string.Format(@"App_LocalResources\Help\gym_err\{0}_err.xml", jobCodeForReturn));
                        document.Load(strSysHelpFile);
                    }
                    else
                    {
                        document.Load(strHelpFile);
                    }                   
                    
                    XmlNode node = document.SelectSingleNode("//response[@code='" + messageCode + "']");

                    string strName = node.Attributes["name"].Value;
                    string strID = node.Attributes["id"].Value;
                    //this.txbMessageCode.Text = messageCode;
                    string strDesc = node.SelectSingleNode("description").InnerText;
                    
                    string strDisposition = node.SelectSingleNode("disposition").InnerText;//cách khắc phục


                    #region Lưu thông điệp trả về
                    THONGDIEP receiveTD = new THONGDIEP();
                    receiveTD.TD_CACHKHACPHUC = strDisposition;
                    receiveTD.TD_COKETTHUC = resultData.Header.EndFlag.Trim();
                    receiveTD.TD_DINHDANG = resultData.Header.Pattern.Trim();
                    receiveTD.TD_LOAITD = resultData.Header.DataType.Trim();
                    receiveTD.TD_MANV = resultData.JobCode.Trim();
                    receiveTD.TD_MATD = resultData.OutCode.Trim();
                    receiveTD.TD_MESSSAGECODE = messageCode;
                    receiveTD.TD_MOTALOI = strDesc;
                    receiveTD.TD_RETURNCODE = resultCode;
                    receiveTD.TD_TENCHITIEU = strName;
                    receiveTD.TD_TENNV = this.jobTitle;
                    receiveTD.TD_THOIGIAN = DateTime.Now;
                    if (((resultData.Header.DataType == "R") || (resultData.Header.DataType == "M")) || (resultData.Header.DataType == "U"))
                    {
                        receiveTD.TD_TIEUDE = resultData.Header.Subject.Remove(0, 0x10).Trim();
                    }
                    else
                    {
                        receiveTD.TD_TIEUDE = resultData.Header.Subject.Trim();
                    }                   
                    receiveTD.TD_TRANGTHAI = (int)TrangThaiThongDiep.Nhan;
                    receiveTD.TD_CONTENT = recvdata.DataString;
                    using (tDBContext mainDB = new tDBContext())
                    {
                        string strMaxID = mainDB.THONGDIEPs.Max("TD_ID");
                        if(string.IsNullOrEmpty(strMaxID))strMaxID="0";
                        receiveTD.TD_ID = Convert.ToInt32(strMaxID) + 1;
                        mainDB.THONGDIEPs.InsertOnSubmit(receiveTD);
                        mainDB.SubmitAllChange();
                    }
                    #endregion
                    
                    this.Show1(strDesc, strDisposition);
                }
            }
            finally
            {

            }
        }

        private void bwSend_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                bwSend.ReportProgress(1, "Tạo thông điệp gửi Hải quan...");
                IData data = this.CreateRequestData();
                string strSendMSG = data.GetDataString();
                bwSend.ReportProgress(30, "Tạo xong thông điệp, đang gửi...");
                ComParameter rParam = this.SendData(strSendMSG);
                bwSend.ReportProgress(60, "Đã gửi thông điệp...");
                //Send được thì log msg
                #region Log send msg
                THONGDIEP sendTD = new THONGDIEP();
                //sendTD.TD_CACHKHACPHUC = "";
                sendTD.TD_COKETTHUC = data.Header.EndFlag.Trim();
                sendTD.TD_DINHDANG = data.Header.Pattern.Trim();
                sendTD.TD_LOAITD = data.Header.DataType.Trim();
                sendTD.TD_MANV = data.JobCode.Trim();
                sendTD.TD_MATD = data.OutCode.Trim();
                //sendTD.TD_MESSSAGECODE = "";
                //sendTD.TD_MOTALOI = "";
                //sendTD.TD_RETURNCODE = "";
                //sendTD.TD_TENCHITIEU = "";
                sendTD.TD_TENNV = this.jobTitle;
                sendTD.TD_THOIGIAN = DateTime.Now;
                if (((data.Header.DataType == "R") || (data.Header.DataType == "M")) || (data.Header.DataType == "U"))
                {
                    sendTD.TD_TIEUDE = data.Header.Subject.Remove(0, 0x10).Trim();
                }
                else
                {
                    sendTD.TD_TIEUDE = data.Header.Subject.Trim();
                }
                sendTD.TD_TRANGTHAI = (int)TrangThaiThongDiep.DaGui;
                sendTD.TD_CONTENT = strSendMSG;
                using (tDBContext mainDB = new tDBContext())
                {
                    string strMaxID = mainDB.THONGDIEPs.Max("TD_ID");
                    if (string.IsNullOrEmpty(strMaxID)) strMaxID = "0";
                    sendTD.TD_ID = Convert.ToInt32(strMaxID) + 1;
                    mainDB.THONGDIEPs.InsertOnSubmit(sendTD);
                    mainDB.SubmitAllChange();
                }
                #endregion
                bwSend.ReportProgress(100, "Đã lưu thông điệp...");

                XuLyTraVe(rParam);
            }
            catch (Exception ex)
            {
                this.Show1(ex.Message, "Liên hệ nhà cung cấp phần mềm. Hotline: 0917 863 688 Mr.Tuyển");
            }
        }

        private void bwSend_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            EnableControls(true);
        }

        private void bwSend_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarX1.Value = e.ProgressPercentage;
            progressBarX1.Text =string.Format("{0}", e.UserState);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
