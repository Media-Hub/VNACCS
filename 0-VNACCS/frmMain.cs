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
    public partial class frmMain : DevComponents.DotNetBar.OfficeForm
    {
        public frmMain()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            btnHaiQuan.Text = "N/A";
            btnBoPhan.Text = "N/A";
            lblDoanhNghiep.Text = "N/A";
            lblMayChuInternet.Text = "N/A";
            lblMayChuNACCSVN.Text = "N/A";
            lblMayChuVnaccs.Text = "N/A";

            BindJobsLits();
            cMaNV.ComboBoxEx.KeyDown += ComboBoxEx_KeyDown;
            cMaNV.Focus();            
            cTenNV.ComboBoxEx.KeyDown += ComboBoxEx_KeyDown;
        }

        void ComboBoxEx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            cTenNV_SelectedIndexChanged(null, null);
        }


        private void BindJobsLits()
        {
            XmlDocument docJob = new XmlDocument();
            docJob.Load(Path.Combine(Application.StartupPath, @"App_LocalResources\JobTrees\jobtree.xml"));
            DataTable DATA = new DataTable();
            DataColumn cCode = new DataColumn("CODE");
            DATA.Columns.Add(cCode);
            DataColumn cName = new DataColumn("NAME");
            DATA.Columns.Add(cName);
            DataRow re = DATA.NewRow();
            re[0] = ""; re[1] = "";
            DATA.Rows.Add(re);
            foreach (XmlNode n in docJob.SelectNodes("jobtree/category/item"))
            {
                DataRow i = DATA.NewRow();
                string v = n.Attributes["code"].Value;
                i[0] = v;
                string name = n.Attributes["name"].Value;
                i[1] = name;
                DATA.Rows.Add(i);
            }
            cMaNV.ComboBoxEx.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cMaNV.ComboBoxEx.AutoCompleteSource = AutoCompleteSource.ListItems;
            cMaNV.ComboBoxEx.Col1Width = 70;
            cMaNV.ComboBoxEx.Col2Width = 400;
            cMaNV.ComboBoxEx.ValueMember = "CODE";
            cMaNV.ComboBoxEx.DisplayMember = "CODE";
            cMaNV.ComboBoxEx.DropDownColumns = "CODE,NAME";
            cMaNV.ComboBoxEx.DropDownColumnsHeaders = "Mã NV\r\nTên nghiệp vụ";
            cMaNV.ComboBoxEx.DropDownHeight = 500;
            cMaNV.ComboBoxEx.MoRongKhiForcus = false;
            cMaNV.ComboBoxEx.DataSource = DATA;

            cTenNV.ComboBoxEx.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cTenNV.ComboBoxEx.AutoCompleteSource = AutoCompleteSource.ListItems;
            cTenNV.ComboBoxEx.Col1Width = 70;
            cTenNV.ComboBoxEx.Col2Width = 400;
            cTenNV.ComboBoxEx.ValueMember = "CODE";
            cTenNV.ComboBoxEx.DisplayMember = "NAME";
            cTenNV.ComboBoxEx.DropDownColumns = "CODE,NAME";
            cTenNV.ComboBoxEx.DropDownColumnsHeaders = "Mã NV\r\nTên nghiệp vụ";
            cTenNV.ComboBoxEx.DropDownHeight = 500;
            cTenNV.ComboBoxEx.MoRongKhiForcus = true;
            cTenNV.ComboBoxEx.DataSource = DATA;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (bwLoadLogInForm.IsBusy == false) bwLoadLogInForm.RunWorkerAsync();
        }

        private void btnKetNoiCSDL_Click(object sender, EventArgs e)
        {
            frmCaiDatCSDL f = new frmCaiDatCSDL();
            f.ShowDialog();
        }

        public Form LoadFormF(Form objF, bool Dlg)
        {
            if (objF == null) return null;
            if (objF.IsDisposed == true) return objF;
            if (Dlg)
            {
                if (objF.Owner == null) objF.Owner = this;                
                objF.ShowDialog();
            }
            else
            {
                if (objF != null)
                {
                    if (!objF.IsDisposed)
                    {
                        objF.MdiParent = this;
                        objF.Show();
                    }
                }
            }
            return objF;
        }

        public void SendLogToSerVer()
        { 
        
        }

        private void btnDanhMuc_Click(object sender, EventArgs e)
        {

        }

        private void btnNguyenTe_Click(object sender, EventArgs e)
        {
            frmSNguyenTe f = new frmSNguyenTe();
            if (f.IsDisposed == false) LoadFormF(f,false);
        }

        private void btnDMDonViXNK_Click(object sender, EventArgs e)
        {
            frmSDonVi f = new frmSDonVi();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }
        //tu day
        private void btnDMDoiTac_Click(object sender, EventArgs e)
        {
            frmSDOITAC f = new frmSDOITAC();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }

        private void btnDMDVHaiQuan_Click(object sender, EventArgs e)
        {
            frmSHAIQUAN f = new frmSHAIQUAN();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }

        private void btnDMCANG_Click(object sender, EventArgs e)
        {
            frmSCUAKHAU f = new frmSCUAKHAU();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }

        private void btnDMCangNuocNgoai_Click(object sender, EventArgs e)
        {
            frmSCUAKHAUNN f = new frmSCUAKHAUNN();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }

        private void btnBoPhanXuLyTK_Click(object sender, EventArgs e)
        {
            frmSBOPHANHAIQUANVNACCS f = new frmSBOPHANHAIQUANVNACCS();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }

        private void btnDMNuoc_Click(object sender, EventArgs e)
        {
            frmSNUOC f = new frmSNUOC();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }

        private void btnDMDVT_Click(object sender, EventArgs e)
        {
            frmSDVT f = new frmSDVT();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }

        private void btnDMLoaiKien_Click(object sender, EventArgs e)
        {
            frmSLOAIKIEN f = new frmSLOAIKIEN();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }

        private void btnDMHangVanTai_Click(object sender, EventArgs e)
        {
            frmSHANGVANTAI f = new frmSHANGVANTAI();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }

        private void btnDMDKGH_Click(object sender, EventArgs e)
        {
            frmSDKGH f = new frmSDKGH();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }

        private void btnDMDDLK_Click(object sender, EventArgs e)
        {
            frmSDDLK f = new frmSDDLK();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }

        private void btnDMLoaiHinh_Click(object sender, EventArgs e)
        {
            frmSLOAIHINH f = new frmSLOAIHINH();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }

        private void btnDMPTTT_Click(object sender, EventArgs e)
        {
            frmSPTTT f = new frmSPTTT();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }

        private void btnDMMaBieuThue_Click(object sender, EventArgs e)
        {
            frmSBIEUTHUE f = new frmSBIEUTHUE();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }

        private void btnDMCacLoai_Click(object sender, EventArgs e)
        {
            frmSDANHMUCCACLOAI f = new frmSDANHMUCCACLOAI();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }

        private void btnDMLoaiDanhMuc_Click(object sender, EventArgs e)
        {
            frmSLOAIDANHMUC f = new frmSLOAIDANHMUC();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }

        private void btnDMDongBoECUS5_Click(object sender, EventArgs e)
        {
            frmHutDanhMucECUS5 f = new frmHutDanhMucECUS5();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }

        private void bwLoadLogInForm_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void bwLoadLogInForm_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.InvokeRequired == true)
            {
                delegateLoadLoginForm v = new delegateLoadLoginForm(LoadLoginForm);
                this.Invoke(v);
            }
            else
            {
                LoadLoginForm();
            }
        }

        private delegate void delegateLoadLoginForm();
        private void LoadLoginForm()
        {
         frmLogIn f = new frmLogIn();
            if (f.IsDisposed == false) LoadFormF(f, true);
        }

        private void labelX1_Click(object sender, EventArgs e)
        {

        }

        public void btnCheckStatus_Click(object sender, EventArgs e)
        {            
            btnHaiQuan.Text =string.IsNullOrEmpty(st.fMaHQ)==true?"Chưa cài đặt...": string.Format("{0} - {1}", st.fMaHQ,st.fTenHQ);
            btnHaiQuan.RecalcLayout();
            btnBoPhan.Text = string.IsNullOrEmpty(st.fMaDoiNhap) == true ? "Chưa cài đặt..." : string.Format("{0} - {1}", st.fMaDoiNhap, st.fTenDoiNhap);
            lblDoanhNghiep.Text = string.IsNullOrEmpty(st.fMaDV) == true ? "Chưa cài đặt..." : string.Format("{0} - {1}", st.fMaDV, st.fTenDV);
            metroStatusBar1.RecalcLayout();
            panelEx1.Refresh();

            if (bwCheckStatus.IsBusy == true) return;
            bwCheckStatus.RunWorkerAsync();
        }

        private void bwCheckStatus_DoWork(object sender, DoWorkEventArgs e)
        {
            using (tDBContext mainDB = new tDBContext())
            {
                int countDonVi = mainDB.SDONVIs.Count();
                if (countDonVi < 1)
                {
                    lblDoanhNghiep.Text = "Bạn chưa nhập thông tin đơn vị Xuất Nhập Khẩu";
                    if (bwLoadDonViForm.IsBusy == false) bwLoadDonViForm.RunWorkerAsync();
                    return;
                }
            }

            //Nếu chưa cài đặt thông số thì hiển thị form cài đặt luôn
            if (string.IsNullOrEmpty(st.fMaHQ) == true)
            {
                if (bwLoadSettingForm.IsBusy == false) bwLoadSettingForm.RunWorkerAsync();
            }

            btnHaiQuan.Text = string.IsNullOrEmpty(st.fMaHQ) == true ? "Chưa cài đặt..." : string.Format("{0} - {1}", st.fMaHQ, st.fTenHQ);
            btnHaiQuan.RecalcLayout();
            btnBoPhan.Text = string.IsNullOrEmpty(st.fMaDoiNhap) == true ? "Chưa cài đặt..." : string.Format("{0} - {1}", st.fMaDoiNhap, st.fTenDoiNhap);
            lblDoanhNghiep.Text = string.IsNullOrEmpty(st.fMaDV) == true ? "Chưa cài đặt..." : string.Format("{0} - {1}", st.fMaDV, st.fTenDV);
            metroStatusBar1.RecalcLayout();
            panelEx1.Refresh();
                        
            lblMayChuInternet.Text = "Dò tìm...";
            lblMayChuNACCSVN.Text = "Dò tìm...";
            lblMayChuVnaccs.Text = "Dò tìm...";
            metroStatusBar1.RecalcLayout();
            panelEx1.Refresh();

            lblMayChuInternet.Text = clsService.IsInternetAvailable()==true?"Tốt":"Gián đoạn";
            lblMayChuNACCSVN.Text = "Tốt";
            lblMayChuVnaccs.Text = "Tốt";
            metroStatusBar1.RecalcLayout();
            panelEx1.Refresh();
        }

        private void bwCheckStatus_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnHaiQuan.RecalcLayout();
            btnHaiQuan.Refresh();
            panelEx1.Refresh();            
            if (btnHaiQuan.Text.Equals("Chưa cài đặt..."))
            {                
                btnHaiQuan.PulseSpeed = 24;
                btnHaiQuan.Pulse(500);
            }
            metroStatusBar1.RecalcLayout();
        }

        private void buttonItem132_Click(object sender, EventArgs e)
        {

        }

        private void btnSettingVNACCS_Click(object sender, EventArgs e)
        {
            frmSetting f = new frmSetting();
            if (f.IsDisposed == false) LoadFormF(f, true);
        }

        private void CaiDat_Click(object sender, EventArgs e)
        {
            btnSettingVNACCS.RaiseClick();
        }

        private void bwLoadSettingForm_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void bwLoadSettingForm_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.InvokeRequired == true)
            {
                DelegateLoadSettingForm v = new DelegateLoadSettingForm(LoadSettingForm);
                this.Invoke(v);
            }
            else
            {
                LoadSettingForm();
            }           
        }

        private delegate void DelegateLoadSettingForm();
        private void LoadSettingForm()
        {
            frmSetting f = new frmSetting();
            if (f.IsDisposed == false) LoadFormF(f, true);
        }

        private void cTenNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cTenNV.ComboBoxEx.SelectedValue == null) return;
            if (string.IsNullOrEmpty(cTenNV.ComboBoxEx.SelectedValue.ToString())) return;
            object jobForm = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(string.Format("DevComponents.DotNetBar.frm{0}", cTenNV.ComboBoxEx.SelectedValue));
            if (jobForm == null) return;
            if (((Form)jobForm).IsDisposed == false) LoadFormF((Form)jobForm, false);
        }

        private void btnThongDiep_Click(object sender, EventArgs e)
        {
            frmThongDiep f = new frmThongDiep();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }

        private void btnCloseAllWindow_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (!f.IsDisposed && f.ControlBox == true)
                {
                    f.Close();
                }
            }
        }

        private void btnDanhMucHangNKD_Click(object sender, EventArgs e)
        {
            frmDanhMucHangKD f = new frmDanhMucHangKD();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }

        private void btnDanhMucHangXKD_Click(object sender, EventArgs e)
        {
            frmDanhMucHangKD f = new frmDanhMucHangKD();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }

        private void btnStyle_Click(object sender, EventArgs e)
        {
            ButtonItem btn = sender as ButtonItem;
            if (btn == null) return;
            if (btn.Text.Equals("Office2013")) this.styleManager1.ManagerStyle = eStyle.Metro;
            if (btn.Text.Equals(eStyle.Office2007Black.ToString())) this.styleManager1.ManagerStyle = eStyle.Office2007Black;
            if (btn.Text.Equals(eStyle.Office2007Blue.ToString())) this.styleManager1.ManagerStyle = eStyle.Office2007Blue;
            if (btn.Text.Equals(eStyle.Office2007Silver.ToString())) this.styleManager1.ManagerStyle = eStyle.Office2007Silver;
            if (btn.Text.Equals(eStyle.Office2007VistaGlass.ToString())) this.styleManager1.ManagerStyle = eStyle.Office2007VistaGlass;
            if (btn.Text.Equals(eStyle.Office2010Black.ToString())) this.styleManager1.ManagerStyle = eStyle.Office2010Black;
            if (btn.Text.Equals(eStyle.Office2010Blue.ToString())) this.styleManager1.ManagerStyle = eStyle.Office2010Blue;
            if (btn.Text.Equals(eStyle.Office2010Silver.ToString())) this.styleManager1.ManagerStyle = eStyle.Office2010Silver;            
            if (btn.Text.Equals(eStyle.VisualStudio2010Blue.ToString())) this.styleManager1.ManagerStyle = eStyle.VisualStudio2010Blue;
            if (btn.Text.Equals(eStyle.Windows7Blue.ToString())) this.styleManager1.ManagerStyle = eStyle.Windows7Blue;
        }

        private void btnMIC_Click(object sender, EventArgs e)
        {
            frmMIC f = new frmMIC();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }

        private void btnMIClist_Click(object sender, EventArgs e)
        {
            frmMIClist f = new frmMIClist();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }

        private void btnMEC_Click(object sender, EventArgs e)
        {
            frmMEC f = new frmMEC();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }

        private void btnMEClist_Click(object sender, EventArgs e)
        {
            frmMEClist f = new frmMEClist();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }

        private void bwLoadDonViForm_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void bwLoadDonViForm_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadDonViForm();
        }

        private delegate void DelegateLoadDonViForm();
        private void LoadDonViForm()
        {
            if (this.InvokeRequired == true)
            {
                DelegateLoadDonViForm v = new DelegateLoadDonViForm(LoadDonViForm);
                this.Invoke(v);
            }
            else
            {
                frmSDonVi f = new frmSDonVi();
                if (f.IsDisposed == false) LoadFormF(f, true);
            }
        }

        private void btnHDGC_Click(object sender, EventArgs e)
        {
            frmGC_HDGC f = new frmGC_HDGC();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }

        private void btnDanhSachHDGC_Click(object sender, EventArgs e)
        {
            frmGC_HDGClist f = new frmGC_HDGClist();
            if (f.IsDisposed == false) LoadFormF(f, false);
        }
    }
}
