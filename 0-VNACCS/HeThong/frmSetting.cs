using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;

namespace DevComponents.DotNetBar
{
    public partial class frmSetting : DevComponents.DotNetBar.tForm
    {
        public frmSetting()
        {
            InitializeComponent();
            LoadDanhMuc();
        }

        
        private void LoadDanhMuc()
        {
            using (tDBContext mainDB = new tDBContext())
            {
                DataTable sDonVi = mainDB.SDONVIs.GetList("DV_MST,DV_TEN,DV_DIENTHOAI,DV_DIACHI","");
                clDonVi.tCODE.DropDownWidth = 450;
                clDonVi.tCODE.Col1Width = 100;
                clDonVi.tCODE.Col2Width = 340;
                clDonVi.tLIST.DropDownWidth = 450;
                clDonVi.tLIST.Col1Width = 100;
                clDonVi.tLIST.Col2Width = 360;
                clDonVi.ValueMember = "DV_MST";
                clDonVi.DisplayMember = "DV_TEN";
                clDonVi.DropDownColumns = "DV_MST,DV_TEN";
                clDonVi.DropDownColumnsHeaders = "MST\r\nTên Đơn vị";
                clDonVi.DataSource = sDonVi;

                DataTable sHQ = mainDB.SHAIQUANs.GetList("HQ_MA,HQ_TEN","");
                clHaiQuan.tCODE.DropDownWidth = 400;
                clHaiQuan.tCODE.Col2Width = 310;
                clHaiQuan.tLIST.DropDownWidth = 400;
                clHaiQuan.tLIST.Col2Width = 330;
                clHaiQuan.ValueMember = "HQ_MA";
                clHaiQuan.DisplayMember = "HQ_TEN";
                clHaiQuan.DropDownColumns = "HQ_MA,HQ_TEN";
                clHaiQuan.DropDownColumnsHeaders = "Mã HQ\r\nTên Hải quan";
                clHaiQuan.DataSource = sHQ;

                DataTable sBoPhan = mainDB.SBOPHANHAIQUANVNACCSs.GetList("");                
                bsDoiNhap.DataSource = sBoPhan;
                bsDoiXuat.DataSource = sBoPhan.Copy();
            }
        }

        private void frmSetting_Load(object sender, EventArgs e)
        {
            this.SetValues();
        }

        private void SetValues()
        {
            clDonVi.SelectedValue = st.fMaDV;
            txtDiaChi.Text = st.fDiaChiDV;
            txtDienThoai.Text = st.fDienThoaiDV;
            clHaiQuan.SelectedValue = st.fMaHQ;
            clDoiNhap.SelectedValue = st.fMaDoiNhap;
            clDoiXuat.SelectedValue = st.fMaDoiXuat;
            txtIPHQ.Text = st.fIPHQ;

            txtUserID.Text = st.fUserIDVNACCS;
            txtPassword.Text = st.fPasswordVNACCS;
            txtTerminalID.Text = st.fTerminalID;
            txtTerminalAccessKey.Text = st.fTerminalAccessKey;
        }     

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ghiDuLieuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnGhi.PerformClick();
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            GhiDuLieu();
            if (st.myMDIMain.bwCheckStatus.IsBusy == false)
            {                
                st.myMDIMain.bwCheckStatus.RunWorkerAsync();              
            }
        }

        private void GhiDuLieu()
        {
            #region Lưu mặc định
            //Xử lý lưu mặc định
            using (tDBContext mainDB = new tDBContext())
            {
                var oldValues = mainDB.SMACDINHs.GetListObject(string.Format("MD_USERNAME='{0}'", st.LoginUsername));
                mainDB.SMACDINHs.DeleteAllOnSubmit(oldValues);

                SMACDINH m = new SMACDINH();
                m.MD_USERNAME = st.LoginUsername;
                m.MD_GIATRI_MA = ct.FMADV;
                m.MD_GIATRI = string.Format("{0}", clDonVi.SelectedValue);
                mainDB.SMACDINHs.InsertOnSubmit(m);

                m = new SMACDINH();
                m.MD_USERNAME = st.LoginUsername;
                m.MD_GIATRI_MA = ct.FTENDV;
                m.MD_GIATRI = clDonVi.Text;
                mainDB.SMACDINHs.InsertOnSubmit(m);

                m = new SMACDINH();
                m.MD_USERNAME = st.LoginUsername;
                m.MD_GIATRI_MA = ct.FDIENTHOAIDV;
                m.MD_GIATRI = txtDienThoai.Text;
                mainDB.SMACDINHs.InsertOnSubmit(m);

                m = new SMACDINH();
                m.MD_USERNAME = st.LoginUsername;
                m.MD_GIATRI_MA = ct.FDIACHIDV;
                m.MD_GIATRI = txtDiaChi.Text;
                mainDB.SMACDINHs.InsertOnSubmit(m);

                m = new SMACDINH();
                m.MD_USERNAME = st.LoginUsername;
                m.MD_GIATRI_MA = ct.FMAHQ;
                m.MD_GIATRI = string.Format("{0}", clHaiQuan.SelectedValue);
                mainDB.SMACDINHs.InsertOnSubmit(m);

                m = new SMACDINH();
                m.MD_USERNAME = st.LoginUsername;
                m.MD_GIATRI_MA = ct.FTENHQ;
                m.MD_GIATRI = clHaiQuan.Text;
                mainDB.SMACDINHs.InsertOnSubmit(m);

                m = new SMACDINH();
                m.MD_USERNAME = st.LoginUsername;
                m.MD_GIATRI_MA = ct.FMADOINHAP;
                m.MD_GIATRI = string.Format("{0}", clDoiNhap.SelectedValue);
                mainDB.SMACDINHs.InsertOnSubmit(m);

                m = new SMACDINH();
                m.MD_USERNAME = st.LoginUsername;
                m.MD_GIATRI_MA = ct.FTENDOINHAP;
                m.MD_GIATRI = clDoiNhap.Text;
                mainDB.SMACDINHs.InsertOnSubmit(m);

                m = new SMACDINH();
                m.MD_USERNAME = st.LoginUsername;
                m.MD_GIATRI_MA = ct.FMADOIXUAT;
                m.MD_GIATRI = string.Format("{0}", clDoiXuat.SelectedValue);
                mainDB.SMACDINHs.InsertOnSubmit(m);

                m = new SMACDINH();
                m.MD_USERNAME = st.LoginUsername;
                m.MD_GIATRI_MA = ct.FTENDOIXUAT;
                m.MD_GIATRI = clDoiXuat.Text;
                mainDB.SMACDINHs.InsertOnSubmit(m);

                m = new SMACDINH();
                m.MD_USERNAME = st.LoginUsername;
                m.MD_GIATRI_MA = ct.FUSERIDVNACCS;
                m.MD_GIATRI = txtUserID.Text;
                mainDB.SMACDINHs.InsertOnSubmit(m);

                m = new SMACDINH();
                m.MD_USERNAME = st.LoginUsername;
                m.MD_GIATRI_MA = ct.FPASSWORDVNACCS;
                m.MD_GIATRI = txtPassword.Text;
                mainDB.SMACDINHs.InsertOnSubmit(m);

                m = new SMACDINH();
                m.MD_USERNAME = st.LoginUsername;
                m.MD_GIATRI_MA = ct.FTERMINALID;
                m.MD_GIATRI = txtTerminalID.Text;
                mainDB.SMACDINHs.InsertOnSubmit(m);

                m = new SMACDINH();
                m.MD_USERNAME = st.LoginUsername;
                m.MD_GIATRI_MA = ct.FTERMINALACCESSKEY;
                m.MD_GIATRI = txtTerminalAccessKey.Text;
                mainDB.SMACDINHs.InsertOnSubmit(m);

                //m = new SMACDINH();
                //m.MD_USERNAME = st.LoginUsername;
                //m.MD_GIATRI_MA = ct.FMAHQV4;
                //m.MD_GIATRI = cHQV4.CODE;
                //mainDB.SMACDINHs.InsertOnSubmit(m);

                //m = new SMACDINH();
                //m.MD_USERNAME = st.LoginUsername;
                //m.MD_GIATRI_MA = ct.FTENHQV4;
                //m.MD_GIATRI = cHQV4.TEXT;
                //mainDB.SMACDINHs.InsertOnSubmit(m);

                //m = new SMACDINH();
                //m.MD_USERNAME = st.LoginUsername;
                //m.MD_GIATRI_MA = ct.FIPHQV4;
                //m.MD_GIATRI = txtDiaChiKhaiV4.Text;
                //mainDB.SMACDINHs.InsertOnSubmit(m);

                //m = new SMACDINH();
                //m.MD_USERNAME = st.LoginUsername;
                //m.MD_GIATRI_MA = ct.FTAIKHOANV4;
                //m.MD_GIATRI = txtTaiKhoanKhaiV4.Text;
                //mainDB.SMACDINHs.InsertOnSubmit(m);

                //m = new SMACDINH();
                //m.MD_USERNAME = st.LoginUsername;
                //m.MD_GIATRI_MA = ct.FMATKHAUV4;
                //m.MD_GIATRI = txtMatKhauKhaiV4.Text;
                //mainDB.SMACDINHs.InsertOnSubmit(m);

                mainDB.SubmitAllChange();
                clsAll.GetDefault(st.LoginUsername);
                ToastNotification.Show(this, "Ghi thành công", 3000, eToastPosition.MiddleCenter);                
            }
            #endregion
        }

        private void clHaiQuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clHaiQuan.SelectedValue == null) return;
            if (string.IsNullOrEmpty(clHaiQuan.SelectedValue.ToString())) return;
            bsDoiNhap.Filter = string.Format("BP_MAHQ='{0}'", clHaiQuan.SelectedValue);           
            clDoiNhap.ValueMember = "BP_MABP";
            clDoiNhap.DisplayMember = "BP_TENBP";
            clDoiNhap.DropDownColumns = "BP_MABP,BP_TENBP";
            clDoiNhap.DropDownColumnsHeaders = "Mã\r\nTên bộ phận";            
            clDoiNhap.tCODE.Col2Width = 300;          
            clDoiNhap.tLIST.Col2Width = 300;
            clDoiNhap.DataSource = bsDoiNhap;

            bsDoiXuat.Filter = string.Format("BP_MAHQ='{0}'", clHaiQuan.SelectedValue);
            clDoiXuat.ValueMember = "BP_MABP";
            clDoiXuat.DisplayMember = "BP_TENBP";
            clDoiXuat.DropDownColumns = "BP_MABP,BP_TENBP";
            clDoiXuat.DropDownColumnsHeaders = "Mã\r\nTên bộ phận";  
            clDoiXuat.tCODE.Col3Width = 300;   
            clDoiXuat.tLIST.Col3Width = 300;
            clDoiXuat.DataSource = bsDoiXuat;
        }
    }
}
