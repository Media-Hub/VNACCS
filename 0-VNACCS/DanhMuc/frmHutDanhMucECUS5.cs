using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DevComponents.DotNetBar
{
    public partial class frmHutDanhMucECUS5 : DevComponents.DotNetBar.tForm
    {
        public frmHutDanhMucECUS5()
        {
            InitializeComponent();
            progressBarX1.SendToBack();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void bwSieuNhan_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (e.Argument == null) return;
                string strConn = string.Format(strConnSQL, txtServerName.Text, txtDBName.Text, txtSQLUser.Text, txtSQLPass.Text);
                DBManagement dbType1 = DBManagement.SQL;
                if (rAccess.Checked == true)
                {
                    strConn = string.Format(strConnAccess, txtAccessFile.Text);
                    dbType1 = DBManagement.Access;
                }               

                using (thaison.ecus5DBContext edb = new thaison.ecus5DBContext(strConn, dbType1))
                {
                    using (tDBContext mainDB = new tDBContext())
                    {
                        #region Hút Nước SNUOC
                        if (btnSNUOC.Name.Equals(e.Argument.ToString()))
                        {
                            List<thaison.SNUOC> lnuoc = edb.SNUOCs.GetListObject("");
                            List<SNUOC> myNuoc = new List<SNUOC>();
                            int i=0;
                            foreach (thaison.SNUOC n in lnuoc)
                            {
                                if (mainDB.SNUOCs.Exist(string.Format("NC_MANUOC='{0}'", n.MA_NUOC)) == true) continue;
                                SNUOC nuoc = new SNUOC();
                                nuoc.NC_MANUOC = n.MA_NUOC;
                                nuoc.NC_TENNUOC = n.TEN_NUOC;
                                nuoc.NC_GHICHU = n.TEN_NUOC1_VN;
                                myNuoc.Add(nuoc);
                                btnSNUOC.Text = string.Format("1- Hút Nước ({0})",++i);
                            }
                            mainDB.SNUOCs.InsertAllOnSubmit(myNuoc);
                            mainDB.SubmitAllChange();                        
                            return;
                        }
                        #endregion

                        #region Hút Hải quan SHAIQUAN
                        if (btnSHAIQUAN.Name.Equals(e.Argument.ToString()))
                        {
                            List<thaison.SHAIQUAN> listThaiSonItem = edb.SHAIQUANs.GetListObject("");
                            List<SHAIQUAN> myListItem = new List<SHAIQUAN>();
                            int i = 0;
                            foreach (thaison.SHAIQUAN thaisonItem in listThaiSonItem)
                            {
                                if (mainDB.SHAIQUANs.Exist(string.Format("HQ_MA='{0}'", thaisonItem.Ma_HQ)) == true) continue;
                                SHAIQUAN myItem = new SHAIQUAN();
                                myItem.HQ_MA = thaisonItem.Ma_HQ;
                                myItem.HQ_TEN = thaisonItem.Ten_HQ;
                                myListItem.Add(myItem);
                                btnSHAIQUAN.Text = string.Format("1- Hút Hải quan ({0})", ++i);
                            }
                            mainDB.SHAIQUANs.InsertAllOnSubmit(myListItem);
                            mainDB.SubmitAllChange();
                            return;
                        }
                        #endregion

                        #region Hút bộ phận SBOPHANHAIQUANVNACCS
                        if (btnSBOPHAN.Name.Equals(e.Argument.ToString()))
                        {
                            List<thaison.SHAIQUAN_SUB> listThaiSonItem = edb.SHAIQUAN_SUBs.GetListObject("");
                            List<SBOPHANHAIQUANVNACCS> myListItem = new List<SBOPHANHAIQUANVNACCS>();
                            int i = 0;
                            foreach (thaison.SHAIQUAN_SUB thaisonItem in listThaiSonItem)
                            {
                                if (mainDB.SBOPHANHAIQUANVNACCSs.Exist(string.Format("BP_MAHQ='{0}' AND BP_MABP='{1}'", thaisonItem.MA_HQ, thaisonItem.MA)) == true) continue;
                                SBOPHANHAIQUANVNACCS myItem = new SBOPHANHAIQUANVNACCS();
                                myItem.BP_MAHQ = thaisonItem.MA_HQ;
                                myItem.BP_MABP = thaisonItem.MA;
                                myItem.BP_TENBP = thaisonItem.TEN;
                                myListItem.Add(myItem);
                                btnSBOPHAN.Text = string.Format("1- Hút Bộ phận ({0})", ++i);
                            }
                            mainDB.SBOPHANHAIQUANVNACCSs.InsertAllOnSubmit(myListItem);
                            mainDB.SubmitAllChange();
                            return;
                        }
                        #endregion

                        #region Hút địa điểm lưu kho
                        if (btnSDDLK.Name.Equals(e.Argument.ToString()))
                        {
                            List<thaison.SDIA_DIEM> listThaiSonItem = edb.SDIA_DIEMs.GetListObject("");
                            List<SDDLK> myListItem = new List<SDDLK>();
                            int i = 0;
                            foreach (thaison.SDIA_DIEM thaisonItem in listThaiSonItem)
                            {
                                if (mainDB.SDDLKs.Exist(string.Format("DDLK_MA='{0}'", thaisonItem.MA_DIA_DIEM)) == true) continue;
                                SDDLK myItem = new SDDLK();
                                myItem.DDLK_MA = thaisonItem.MA_DIA_DIEM;
                                myItem.DDLK_TEN = thaisonItem.TEN_DIA_DIEM;//.Replace("'","''");
                                myItem.DDLH_GHICHU = thaisonItem.GHI_CHU;
                                myItem.DDLK_DIABAN = thaisonItem.DIA_BAN;
                                myItem.DDLK_DIACHI = thaisonItem.DIA_CHI;
                                myItem.DDLK_MAHQ = thaisonItem.MA_HQ;
                                //myItem.
                                myListItem.Add(myItem);
                                btnSDDLK.Text = string.Format("1- Hút địa điểm lưu kho ({0})", ++i);
                            }
                            mainDB.SDDLKs.InsertAllOnSubmit(myListItem);
                            mainDB.SubmitAllChange();
                            return;
                        }
                        #endregion

                        #region Hút cửa khẩu trong nước
                        if (btnSCUAKHAU.Name.Equals(e.Argument.ToString()))
                        {
                            List<thaison.SCUAKHAU> listThaiSonItem = edb.SCUAKHAUs.GetListObject("");
                            List<SCUAKHAU> myListItem = new List<SCUAKHAU>();
                            int i = 0;
                            foreach (thaison.SCUAKHAU thaisonItem in listThaiSonItem)
                            {
                                if (mainDB.SCUAKHAUs.Exist(string.Format("CK_MACK='{0}' AND CK_MANUOC='VN'", thaisonItem.Ma_CK)) == true) continue;
                                SCUAKHAU myItem = new SCUAKHAU();
                                myItem.CK_MANUOC = "VN";
                                myItem.CK_MACK = thaisonItem.Ma_CK;
                                myItem.CK_TENCK = thaisonItem.Ten_CK;
                                myItem.CK_TENBANG = thaisonItem.TEN_BANG;
                                myItem.CK_MACKCU = thaisonItem.MA_CU;
                                myItem.CK_TENCKCU = thaisonItem.TEN_CU;
                                myItem.CK_TENCK_VN = thaisonItem.TEN_CK1_VN;//TEN_CK1_VN bọn TS update nhiều hơn TEN_CK_VN
                                myListItem.Add(myItem);
                                btnSCUAKHAU.Text = string.Format("6 - Hút cửa khẩu trong nước ({0})", ++i);
                            }
                            mainDB.SCUAKHAUs.InsertAllOnSubmit(myListItem);
                            mainDB.SubmitAllChange();
                            return;
                        }
                        #endregion

                        #region Hút cửa khẩu nước ngoài
                        if (btnSCUAKHAUNN.Name.Equals(e.Argument.ToString()))
                        {
                            List<thaison.SCUAKHAUNN> listThaiSonItem = edb.SCUAKHAUNNs.GetListObject("");
                            List<SCUAKHAUNN> myListItem = new List<SCUAKHAUNN>();
                            int i = 0;
                            foreach (thaison.SCUAKHAUNN thaisonItem in listThaiSonItem)
                            {
                                if (mainDB.SCUAKHAUNNs.Exist(string.Format("CK_MACK='{0}' AND CK_MANUOC='{1}'", thaisonItem.MA_CK, thaisonItem.MA_NUOC)) == true) continue;
                                SCUAKHAUNN myItem = new SCUAKHAUNN();
                                myItem.CK_MANUOC = thaisonItem.MA_NUOC;
                                myItem.CK_MACK = thaisonItem.MA_CK;
                                myItem.CK_TENCK = thaisonItem.TEN_CK;
                                myItem.CK_TENBANG = thaisonItem.TEN_BANG;
                                myItem.CK_MACKCU = thaisonItem.MA_CU;
                                myItem.CK_TENCKCU = thaisonItem.TEN_CU;
                                myItem.CK_TENCK_VN = thaisonItem.TEN_CK1_VN;//TEN_CK1_VN bọn TS update nhiều hơn TEN_CK_VN
                                myItem.CK_TYPE = thaisonItem.ItemType;
                                myListItem.Add(myItem);
                                btnSCUAKHAUNN.Text = string.Format("7 - Hút cửa khẩu nước ngoài ({0})", ++i);
                            }
                            mainDB.SCUAKHAUNNs.InsertAllOnSubmit(myListItem);
                            mainDB.SubmitAllChange();
                            return;
                        }
                        #endregion

                        #region Hút nguyên tệ
                        if (btnSNGHTE.Name.Equals(e.Argument.ToString()))
                        {
                            List<thaison.SNGTE> listThaiSonItem = edb.SNGTEs.GetListObject("");
                            List<SNGHTE> myListItem = new List<SNGHTE>();
                            int i = 0;
                            foreach (thaison.SNGTE thaisonItem in listThaiSonItem)
                            {
                                if (mainDB.SNGHTEs.Exist(string.Format("NT_MANT='{0}'", thaisonItem.MA_NT)) == true) continue;
                                SNGHTE myItem = new SNGHTE();
                                myItem.NT_MANT = thaisonItem.MA_NT;
                                myItem.NT_TENNT = thaisonItem.TEN_NT;
                                myItem.NT_TENNT_VN = thaisonItem.TEN_NT1;
                                myItem.NT_TYGIAVND =Convert.ToDecimal(thaisonItem.TYGIA_VND);
                                myItem.NT_TENBANG = thaisonItem.TEN_BANG;
                                myListItem.Add(myItem);
                                btnSNGHTE.Text = string.Format("8 - Hút Nguyên tệ ({0})", ++i);
                            }
                            mainDB.SNGHTEs.InsertAllOnSubmit(myListItem);
                            mainDB.SubmitAllChange();
                            return;
                        }
                        #endregion

                        #region Hút nguyên tệ
                        if (btnSDKGH.Name.Equals(e.Argument.ToString()))
                        {
                            List<thaison.SDKGH> listThaiSonItem = edb.SDKGHs.GetListObject("");
                            List<SDKGH> myListItem = new List<SDKGH>();
                            int i = 0;
                            foreach (thaison.SDKGH thaisonItem in listThaiSonItem)
                            {
                                if (mainDB.SDKGHs.Exist(string.Format("DKGH_MA='{0}'", thaisonItem.MA_GH)) == true) continue;
                                SDKGH myItem = new SDKGH();
                                myItem.DKGH_MA = thaisonItem.MA_GH;
                                myItem.DKGH_TEN = thaisonItem.TEN_GH;
                                myItem.DKGH_GHICHU = thaisonItem.GHICHU;                               
                                myListItem.Add(myItem);
                                btnSDKGH.Text = string.Format("9 - Hút điều kiện giao hàng ({0})", ++i);
                            }
                            mainDB.SDKGHs.InsertAllOnSubmit(myListItem);
                            mainDB.SubmitAllChange();
                            return;
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                clsMx.Show(ex, this.Name);
                return;
            }
        }

        private void bwSieuNhan_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarX1.SendToBack();           
            pDongBo.Enabled = true;
        }

        string strConnAccess= "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{0}';Persist Security Info=True;Jet OLEDB:Database Password=thaisonpdf";
        string strConnSQL = "Data Source={0};Initial Catalog={1};User ID={2};pwd={3}";
        private void btnHUT_Click(object sender, EventArgs e)
        {
            //if (rAccess.Checked == false)
            //{
            //    ToastNotification.Show(this, "Chưa hỗ trợ hút từ DB SQL, đề nghị code thêm");
            //    return;
            //}

            if (rAccess.Checked == true)
            {
                if (File.Exists(txtAccessFile.Text) == false)
                {
                    ToastNotification.Show(this, "Không tìm thấy file access ECUSS5VNACCS.mdb, đề nghị kiểm tra lại");
                    return;
                }

                //test connect den db ecus5

                try
                {
                    using (thaison.ecus5DBContext edb = new thaison.ecus5DBContext(string.Format(strConnAccess, txtAccessFile.Text)))
                    {
                        int c = edb.SNUOCs.Count();
                    }
                }
                catch (Exception ex)
                {
                    clsMx.Show(ex, this.Name);
                    return;
                }

                if (bwSieuNhan.IsBusy == true) return;
                progressBarX1.BringToFront();
                bwSieuNhan.RunWorkerAsync((sender as ButtonX).Name);
                pDongBo.Enabled = false;
            }
            else//Hút từ SQL
            {
                try
                {
                    string strConn = string.Format(strConnSQL, txtServerName.Text, txtDBName.Text, txtSQLUser.Text, txtSQLPass.Text);
                    using (thaison.ecus5DBContext edb = new thaison.ecus5DBContext(strConn,DBManagement.SQL))
                    {
                        int c = edb.SNUOCs.Count();
                    }
                }
                catch (Exception ex)
                {
                    clsMx.Show(ex, this.Name);
                    return;
                }

                if (bwSieuNhan.IsBusy == true) return;
                progressBarX1.BringToFront();
                bwSieuNhan.RunWorkerAsync((sender as ButtonX).Name);
                pDongBo.Enabled = false;
            }
        }

        private void pDongBo_Click(object sender, EventArgs e)
        {

        }

      
    }
}
