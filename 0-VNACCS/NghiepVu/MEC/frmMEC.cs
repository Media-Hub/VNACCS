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
    public partial class frmMEC : DevComponents.DotNetBar.jobForm
    {
        MEC DATA = default(MEC);
        public frmMEC():this(-1)
        {
        }
        public frmMEC(int TK_ID)
            : base("MEC")
        {            
            InitializeComponent();
            LoadDanhMuc();
            Control.CheckForIllegalCrossThreadCalls = false;
            clsAll.SetBindingSource(this.pThongTinTK, this.bsMEC);
            progressBarX1.TextVisible = false;
            proLoadData.Visible = false;
            if (TK_ID < 1)
            {
                bsMEC.DataSource = new MEC();
            }
            else
            {
                if (bwLoadData.IsBusy == false) bwLoadData.RunWorkerAsync(TK_ID);
            }
        }

        private void LoadDanhMuc()
        {
            //nước
            CGK.tCODE.Col1Width = 40;
            CGK.tCODE.Col2Width = 100;
            CGK.tCODE.Col3Width = 150;
            CGK.tCODE.DropDownWidth = 330;
            CGK.tCODE.DropDownHeight = 200;
            CGK.tCODE.MoRongKhiForcus = false;
            CGK.tLIST.Col1Width = 40;
            CGK.tLIST.Col2Width = 100;
            CGK.tLIST.Col3Width = 150;
            CGK.tLIST.DropDownWidth = 330;
            CGK.tLIST.DropDownHeight = 200;
            CGK.tLIST.MoRongKhiForcus = false;
            CGK.ValueMember = "NC_MANUOC";
            CGK.DisplayMember = "NC_TENNUOC";
            CGK.DropDownColumns = "NC_MANUOC,NC_TENNUOC,NC_GHICHU";
            CGK.DropDownColumnsHeaders = "Mã\r\nTên nước\r\nGhi chú";           
            CGK.DataSource = st.SNUOC;

            ////nước
            //OR.tCODE.Col1Width = 40;
            //OR.tCODE.Col2Width = 100;
            //OR.tCODE.Col3Width = 150;
            //OR.tCODE.DropDownWidth = 330;
            //OR.tCODE.DropDownHeight = 200;
            //OR.tCODE.MoRongKhiForcus = false;
            //OR.tLIST.Col1Width = 40;
            //OR.tLIST.Col2Width = 100;
            //OR.tLIST.Col3Width = 150;
            //OR.tLIST.DropDownWidth = 330;
            //OR.tLIST.DropDownHeight = 200;
            //OR.tLIST.MoRongKhiForcus = false;
            //OR.ValueMember = "NC_MANUOC";
            //OR.DisplayMember = "NC_TENNUOC";
            //OR.DropDownColumns = "NC_MANUOC,NC_TENNUOC,NC_GHICHU";
            //OR.DropDownColumnsHeaders = "Mã\r\nTên nước\r\nGhi chú";
            //OR.DataSource = st.SNUOC.Copy();

            ////Phân loại cá nhân tổ chức
            //SKB.tCODE.Col1Width = 40;
            //SKB.tCODE.Col2Width = 150;
            //SKB.tCODE.DropDownWidth = 220;
            //SKB.tCODE.DropDownHeight = 150;
            //SKB.tCODE.MoRongKhiForcus = false;
            //SKB.tLIST.Col1Width = 40;
            //SKB.tLIST.Col2Width = 150;
            //SKB.tLIST.DropDownWidth = 220;
            //SKB.tLIST.DropDownHeight = 150;
            //SKB.tLIST.MoRongKhiForcus = false;
            //SKB.ValueMember = "MA";
            //SKB.DisplayMember = "TEN";
            //SKB.DropDownColumns = "MA,TEN";
            //SKB.DropDownColumnsHeaders = "Mã\r\nMô tả";
            //SKB.DataSource = st.SCANHANTOCHUC;

            //Hải quan
            CH.tCODE.Col1Width = 50;
            CH.tCODE.Col2Width = 300;
            CH.tCODE.Col3Width = 50;
            CH.tCODE.Col4Width = 300;
            CH.tCODE.DropDownWidth = 400;
            CH.tCODE.DropDownHeight = 200;
            CH.tCODE.MoRongKhiForcus = false;
            CH.tLIST.Col1Width = 50;
            CH.tLIST.Col2Width = 300;
            CH.tLIST.Col3Width = 50;
            CH.tLIST.Col4Width = 300;
            CH.tLIST.DropDownWidth = 500;
            CH.tLIST.DropDownHeight = 200;
            CH.tLIST.MoRongKhiForcus = false;
            CH.ValueMember = "HQ_MA";
            CH.DisplayMember = "HQ_TEN";
            CH.DropDownColumns = "HQ_MA,HQ_TEN";
            CH.DropDownColumnsHeaders = "Mã HQ\r\nTên Hải quan";
            CH.DataSource = st.SHAIQUAN;

            //bộ phận
            CHB.tCODE.Col1Width = 50;
            CHB.tCODE.Col2Width = 200;
            CHB.tCODE.DropDownWidth = 300;
            CHB.tCODE.DropDownHeight = 150;
            CHB.tCODE.MoRongKhiForcus = false;
            CHB.tLIST.Col1Width = 50;
            CHB.tLIST.Col2Width = 200;
            CHB.tLIST.DropDownWidth = 300;
            CHB.tLIST.DropDownHeight = 150;
            CHB.tLIST.MoRongKhiForcus = false;
            CHB.ValueMember = "BP_MABP";
            CHB.DisplayMember = "BP_TENBP";
            CHB.DropDownColumns = "BP_MABP,BP_TENBP";
            CHB.DropDownColumnsHeaders = "Mã\r\nTên bộ phận xử lý tờ khai";
            bsBoPhan.DataSource = st.SBOPHAN;
            CHB.DataSource = bsBoPhan;

            //địa điểm lưu kho
            //ST           
            ST.tCODE.Col1Width = 50;
            ST.tCODE.Col2Width = 200;
            ST.tCODE.Col3Width = 200;
            ST.tCODE.DropDownWidth = 500;
            ST.tCODE.DropDownHeight = 200;
            ST.tCODE.MoRongKhiForcus = false;
            ST.tLIST.Col1Width = 50;
            ST.tLIST.Col2Width = 200;
            ST.tLIST.Col3Width = 200;
            ST.tLIST.DropDownWidth = 500;
            ST.tLIST.DropDownHeight = 200;
            ST.tLIST.MoRongKhiForcus = false;
            ST.ValueMember = "DDLK_MA";
            ST.DisplayMember = "DDLK_TEN";
            ST.DropDownColumns = "DDLK_MA,DDLK_TEN,DDLH_GHICHU";
            ST.DropDownColumnsHeaders = "Mã địa điểm\r\nTên địa điểm\r\nGhi chú (Tiếng Việt)";
            ST.DataSource = st.SDDLK;

            //địa điểm dỡ hàng (cửa khẩu trong nước)
            //DST           
            DSC.tCODE.Col1Width = 50;
            DSC.tCODE.Col2Width = 200;
            DSC.tCODE.Col3Width = 200;
            DSC.tCODE.DropDownWidth = 500;
            DSC.tCODE.DropDownHeight = 200;
            DSC.tCODE.MoRongKhiForcus = false;
            DSC.tLIST.Col1Width = 50;
            DSC.tLIST.Col2Width = 200;
            DSC.tLIST.Col3Width = 200;
            DSC.tLIST.DropDownWidth = 500;
            DSC.tLIST.DropDownHeight = 200;
            DSC.tLIST.MoRongKhiForcus = false;
            DSC.ValueMember = "CK_MACK";
            DSC.DisplayMember = "CK_TENCK";
            DSC.DropDownColumns = "CK_MACK,CK_TENCK,CK_TENCK_VN";
            DSC.DropDownColumnsHeaders = "Mã CK\r\nTên cửa khẩu\r\nGhi chú (Tiếng Việt)";
            DSC.DataSource = st.SCUAKHAU;

            //địa điểm xếp hàng (cửa khẩu nước ngoài
            //PSC
            PSC.tCODE.Col1Width = 50;
            PSC.tCODE.Col2Width = 200;
            PSC.tCODE.Col3Width = 200;
            PSC.tCODE.DropDownWidth = 500;
            PSC.tCODE.DropDownHeight = 200;
            PSC.tCODE.MoRongKhiForcus = false;
            PSC.tLIST.Col1Width = 50;
            PSC.tLIST.Col2Width = 200;
            PSC.tLIST.Col3Width = 200;
            PSC.tLIST.DropDownWidth = 500;
            PSC.tLIST.DropDownHeight = 200;
            PSC.tLIST.MoRongKhiForcus = false;
            PSC.ValueMember = "CK_MACK";
            PSC.DisplayMember = "CK_TENCK";
            PSC.DropDownColumns = "CK_MACK,CK_TENCK,CK_TENCK_VN";
            PSC.DropDownColumnsHeaders = "Mã CK\r\nTên cửa khẩu\r\nGhi chú (Tiếng Việt)";
            PSC.DataSource = st.SCUAKHAUNN;

            ////nguyên tệ
            ////IP3
            //IP3.Col1Width = 50;
            //IP3.Col2Width = 150;            
            //IP3.DropDownWidth = 250;
            //IP3.DropDownHeight = 200;           
            //IP3.ValueMember = "NT_MANT";
            //IP3.DisplayMember = "NT_TENNT";
            //IP3.DropDownColumns = "NT_MANT,NT_TENNT";
            //IP3.DropDownColumnsHeaders = "Mã\r\nTên nguyên tệ";
            //IP3.DataSource = st.SNGTE;

            //FR2.Col1Width = 50;
            //FR2.Col2Width = 150;
            //FR2.DropDownWidth = 250;
            //FR2.DropDownHeight = 200;
            //FR2.ValueMember = "NT_MANT";
            //FR2.DisplayMember = "NT_TENNT";
            //FR2.DropDownColumns = "NT_MANT,NT_TENNT";
            //FR2.DropDownColumnsHeaders = "Mã\r\nTên nguyên tệ";
            //FR2.DataSource = st.SNGTE.Copy();

            //IN2.Col1Width = 50;
            //IN2.Col2Width = 150;
            //IN2.DropDownWidth = 250;
            //IN2.DropDownHeight = 200;
            //IN2.ValueMember = "NT_MANT";
            //IN2.DisplayMember = "NT_TENNT";
            //IN2.DropDownColumns = "NT_MANT,NT_TENNT";
            //IN2.DropDownColumnsHeaders = "Mã\r\nTên nguyên tệ";
            //IN2.DataSource = st.SNGTE.Copy();

            ////Điều kiện giao hàng
            ////IP2
            //IP2.Col1Width = 50;
            //IP2.Col2Width = 150;
            //IP2.Col3Width = 150;
            //IP2.DropDownWidth = 400;
            //IP2.DropDownHeight = 200;
            //IP2.ValueMember = "DKGH_MA";
            //IP2.DisplayMember = "DKGH_MA";
            //IP2.DropDownColumns = "DKGH_MA,DKGH_TEN,DKGH_GHICHU";
            //IP2.DropDownColumnsHeaders = "Mã\r\nTên điều kiện giao hàng\r\nGhi chú";
            //IP2.DataSource = st.SDKGH; 

            ////phân loại giá hóa đơn
            ////IP1
            //IP1.Col1Width = 50;
            //IP1.Col2Width = 150;
            //IP1.DropDownWidth = 250;            
            //IP1.DropDownHeight = 200;
            //IP1.ValueMember = "MA";
            //IP1.DisplayMember = "MA";
            //IP1.DropDownColumns = "MA,TEN";
            //IP1.DropDownColumnsHeaders = "Mã\r\nTên phân loại";
            //IP1.DataSource = st.SPHANLOAIGIAHOADON;

            ////phân loại phí vận chuyển
            ////IP1
            //FR1.Col1Width = 50;
            //FR1.Col2Width = 150;
            //FR1.DropDownWidth = 250;
            //FR1.DropDownHeight = 200;
            //FR1.ValueMember = "MA";
            //FR1.DisplayMember = "MA";
            //FR1.DropDownColumns = "MA,TEN";
            //FR1.DropDownColumnsHeaders = "Mã\r\nTên phân loại";
            //FR1.DataSource = st.SPHANLOAIPHIVANCHUYEN;

            ////phân loại phí bảo hiểm
            ////IP1
            //IN1.Col1Width = 50;
            //IN1.Col2Width = 150;
            //IN1.DropDownWidth = 250;
            //IN1.DropDownHeight = 200;
            //IN1.ValueMember = "MA";
            //IN1.DisplayMember = "MA";
            //IN1.DropDownColumns = "MA,TEN";
            //IN1.DropDownColumnsHeaders = "Mã\r\nTên phân loại";
            //IN1.DataSource = st.SPHANLOAIPHIBAOHIEM;

        }
        private void frmMEC_Load(object sender, EventArgs e)
        {

        }
        private void CH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CH.SelectedValue == null) return;
            if (string.IsNullOrEmpty(string.Format("{0}", CH.SelectedValue))) return;

            bsBoPhan.Filter = string.Format("BP_MAHQ = '{0}' OR BP_MAHQ = ''", CH.SelectedValue);
           
        }

        private void bwLoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            proLoadData.Visible = true;
            using (tDBContext mainDB = new tDBContext())
            {
                this.DATA = mainDB.MECs.GetObject(string.Format("TK_ID = {0}", e.Argument));
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void bwLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bsMEC.DataSource = this.DATA;
            proLoadData.Visible = false;
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
                else if (recvdata.DataString.Equals("Verification GW Error"))
                {
                    this.ShowLoiHeThong(recvdata.DataString);
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
            bwSend.ReportProgress(1,"Tạo thông điệp gửi Hải quan...");
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

        private void btnToKhaiMoi_Click(object sender, EventArgs e)
        {
            bsMEC.DataSource = new MEC();
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            if (this.CheckControls(pThongTinTK)==false)
            {
                ToastNotification.Show(this, "Còn một số chỉ tiêu chưa nhập hoặc chưa đúng, vui lòng kiểm tra lại", eToastPosition.MiddleCenter);
                return;
            }
            if (bsMEC.DataSource == null) return;
            MEC MEC = bsMEC.DataSource as MEC;
            if (MEC == null) return;
            if (MEC.TK_ID < 1)//trường hợp thêm mới
            {
                using (tDBContext mainDB = new tDBContext())
                {
                    string strMax = mainDB.MECs.Max("TK_ID");
                    if (string.IsNullOrEmpty(strMax)) strMax = "0";
                    MEC.TK_ID = Convert.ToInt32(strMax)+1;
                    mainDB.MECs.InsertOnSubmit(MEC);
                    mainDB.SubmitAllChange();
                }
            }
            else//Trường hợp update
            {
                using (tDBContext mainDB = new tDBContext())
                {
                    mainDB.MECs.UpdateOnSubmit(MEC);
                    mainDB.SubmitAllChange();
                }
            }
            ToastNotification.Show(this,"Ghi thành công",eToastPosition.MiddleCenter);
            
            frmMEClist f = this.Tag as frmMEClist;
            if (f == null) return;
            f.OnDataChanged();
        }

        private void frmMEC_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void panelEx2_Scroll(object sender, ScrollEventArgs e)
        {
            this.hlBatBuoc.UpdateHighlights();
        }

        private void OR_Load(object sender, EventArgs e)
        {

        }

        private void ghiDuLieuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnGhi.PerformClick();
        }

        private void buttonX10_Click(object sender, EventArgs e)
        {
           
        }
    }
}
