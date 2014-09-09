using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DevComponents.DotNetBar
{
    public partial class frmGC_HDGC : DevComponents.DotNetBar.tForm
    {
        GC_HDGC DATA = default(GC_HDGC);
        public frmGC_HDGC():this(-1)
        { }

        public frmGC_HDGC(int HD_ID)
        {
            InitializeComponent();

            LoadDanhMuc();

            bsDATA.DataSource = typeof(DevComponents.DotNetBar.GC_HDGC);
            clsAll.SetBindingSource(this.CONTENT, this.bsDATA);
            
            DATA = new GC_HDGC();
            bsDATA.DataSource = DATA;

            if (HD_ID > 0 && bwLoadData.IsBusy == false) bwLoadData.RunWorkerAsync(HD_ID);        
        }

        private void LoadDanhMuc()
        {
            //Hải quan
            HD_MAHQ.tCODE.Col1Width = 50;
            HD_MAHQ.tCODE.Col2Width = 300;
            HD_MAHQ.tCODE.Col3Width = 50;
            HD_MAHQ.tCODE.Col4Width = 300;
            HD_MAHQ.tCODE.DropDownWidth = 400;
            HD_MAHQ.tCODE.DropDownHeight = 200;
            HD_MAHQ.tCODE.MoRongKhiForcus = false;
            HD_MAHQ.tLIST.Col1Width = 50;
            HD_MAHQ.tLIST.Col2Width = 300;
            HD_MAHQ.tLIST.Col3Width = 50;
            HD_MAHQ.tLIST.Col4Width = 300;
            HD_MAHQ.tLIST.DropDownWidth = 500;
            HD_MAHQ.tLIST.DropDownHeight = 200;
            HD_MAHQ.tLIST.MoRongKhiForcus = false;
            HD_MAHQ.ValueMember = "HQ_MA";
            HD_MAHQ.DisplayMember = "HQ_TEN";
            HD_MAHQ.DropDownColumns = "HQ_MA,HQ_TEN";
            HD_MAHQ.DropDownColumnsHeaders = "Mã HQ\r\nTên Hải quan";
            HD_MAHQ.DataSource = st.SHAIQUAN;
            
            HD_NGTE_MA.tCODE.Col1Width = 50;
            HD_NGTE_MA.tCODE.Col2Width = 150;
            HD_NGTE_MA.tCODE.DropDownWidth = 250;
            HD_NGTE_MA.tCODE.DropDownHeight = 200;
            HD_NGTE_MA.tLIST.Col1Width = 50;
            HD_NGTE_MA.tLIST.Col2Width = 150;
            HD_NGTE_MA.tLIST.DropDownWidth = 250;
            HD_NGTE_MA.tLIST.DropDownHeight = 200;
            HD_NGTE_MA.ValueMember = "NT_MANT";
            HD_NGTE_MA.DisplayMember = "NT_TENNT";
            HD_NGTE_MA.DropDownColumns = "NT_MANT,NT_TENNT";
            HD_NGTE_MA.DropDownColumnsHeaders = "Mã\r\nTên nguyên tệ";
            HD_NGTE_MA.DataSource = st.SNGTE;

            HD_PTTT_MA.tCODE.Col1Width = 50;
            HD_PTTT_MA.tCODE.Col2Width = 150;
            HD_PTTT_MA.tCODE.DropDownWidth = 250;
            HD_PTTT_MA.tCODE.DropDownHeight = 200;
            HD_PTTT_MA.tLIST.Col1Width = 50;
            HD_PTTT_MA.tLIST.Col2Width = 150;
            HD_PTTT_MA.tLIST.DropDownWidth = 250;
            HD_PTTT_MA.tLIST.DropDownHeight = 200;
            HD_PTTT_MA.DropDownColumns = "PTTT_MA,PTTT_TEN";
            HD_PTTT_MA.DropDownColumnsHeaders = "Mã\r\nTên PTTT";
            HD_PTTT_MA.DisplayMember = "PTTT_TEN";
            HD_PTTT_MA.ValueMember = "PTTT_MA";
            HD_PTTT_MA.DataSource = st.SPTTT;

            HD_NUOCTHUE_MA.tCODE.Col1Width = 40;
            HD_NUOCTHUE_MA.tCODE.Col2Width = 100;
            HD_NUOCTHUE_MA.tCODE.Col3Width = 150;
            HD_NUOCTHUE_MA.tCODE.DropDownWidth = 330;
            HD_NUOCTHUE_MA.tCODE.DropDownHeight = 200;
            HD_NUOCTHUE_MA.tCODE.MoRongKhiForcus = false;
            HD_NUOCTHUE_MA.tLIST.Col1Width = 40;
            HD_NUOCTHUE_MA.tLIST.Col2Width = 100;
            HD_NUOCTHUE_MA.tLIST.Col3Width = 150;
            HD_NUOCTHUE_MA.tLIST.DropDownWidth = 330;
            HD_NUOCTHUE_MA.tLIST.DropDownHeight = 200;
            HD_NUOCTHUE_MA.tLIST.MoRongKhiForcus = false;
            HD_NUOCTHUE_MA.ValueMember = "NC_MANUOC";
            HD_NUOCTHUE_MA.DisplayMember = "NC_TENNUOC";
            HD_NUOCTHUE_MA.DropDownColumns = "NC_MANUOC,NC_TENNUOC,NC_GHICHU";
            HD_NUOCTHUE_MA.DropDownColumnsHeaders = "Mã\r\nTên nước\r\nGhi chú";
            HD_NUOCTHUE_MA.DataSource = st.SNUOC;
        }

       

        private void bwLoadData_DoWork(object sender, DoWorkEventArgs e)
        {            
            using (tDBContext mainDB = new tDBContext())
            {
                this.DATA = mainDB.GC_HDGCs.GetObject(string.Format("HD_ID = {0}", e.Argument));               
            }
        }

        private void bwLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bsDATA.DataSource = DATA;
        }

        private void ghiDuLieuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnGhiDuLieu.PerformClick();
        }

        private void btnGhiDuLieu_Click(object sender, EventArgs e)
        {
            using (tDBContext mainDB = new tDBContext())
            {
                if (DATA.HD_ID < 1)
                {
                    string strMaxID = mainDB.GC_HDGCs.Max("HD_ID");
                    if (string.IsNullOrEmpty(strMaxID) == true) strMaxID = "0";
                    int intMaxID = Convert.ToInt32(strMaxID) + 1;
                    DATA.HD_ID = intMaxID;
                    mainDB.GC_HDGCs.InsertOnSubmit(DATA);
                }
                else
                {
                    mainDB.GC_HDGCs.UpdateOnSubmit(DATA);
                }
                mainDB.SubmitAllChange();
            }
            this.tShow1("Ghi dữ liệu thành công");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNguyenLieu_Click(object sender, EventArgs e)
        {
            frmGC_HDGC_NGUYENLIEUlist f = new frmGC_HDGC_NGUYENLIEUlist(DATA);
            st.myMDIMain.LoadFormF(f, true);
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            frmGC_HDGC_SANPHAMlist f = new frmGC_HDGC_SANPHAMlist(DATA);
            st.myMDIMain.LoadFormF(f, true);
        }

        private void btnThietBi_Click(object sender, EventArgs e)
        {
            frmGC_HDGC_THIETBIlist f = new frmGC_HDGC_THIETBIlist();
            st.myMDIMain.LoadFormF(f, true);
        }

        private void btnHangMau_Click(object sender, EventArgs e)
        {
            frmGC_HDGC_HANGMAUlist f = new frmGC_HDGC_HANGMAUlist();
            st.myMDIMain.LoadFormF(f, true);
        }

        private void btnDinhMuc_Click(object sender, EventArgs e)
        {
            frmGC_HDGC_DINHMUClist f = new frmGC_HDGC_DINHMUClist(DATA);
            st.myMDIMain.LoadFormF(f, true);
        }
    }
}
