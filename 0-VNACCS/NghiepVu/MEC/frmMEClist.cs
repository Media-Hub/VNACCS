using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DevComponents.DotNetBar
{
    public partial class frmMEClist : tForm
    {
        private DataTable DATA = default(DataTable);

        public frmMEClist()
        {
            InitializeComponent();           
            proLoad.SendToBack();
            Control.CheckForIllegalCrossThreadCalls = false;
        }       

        private void frmMEClist_Load(object sender, EventArgs e)
        {
            if (bwLoadData.IsBusy == false) bwLoadData.RunWorkerAsync();
        }

        private void bwLoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            proLoad.BringToFront();
            using (tDBContext mainDB = new tDBContext())
            {
               this.DATA = mainDB.MECs.GetList("");
               //System.Threading.Thread.Sleep(1000);
            }
        }

        private void bwLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bsList.DataSource = this.DATA;
            gList.PrimaryGrid.DataSource = bsList;
            proLoad.SendToBack();
        }

        private void phimTat_XemChiTiet_Click(object sender, EventArgs e)
        {
            btnXemChiTiet.PerformClick();
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (gList.PrimaryGrid.DataSource == null) return;
            if (gList.PrimaryGrid.SelectedRows == null) return;
            if (gList.PrimaryGrid.GetSelectedRows().Count < 1) return;
            GridElement ele = gList.PrimaryGrid.GetSelectedRows()[0];
            GridRow currentRow = ele as GridRow;
            if (currentRow == null) return;
            GridCell cellID = currentRow["TK_ID"];
            int currentID = -1;
            currentID = Convert.ToInt32(cellID.Value);           
            
            frmMEC f = new frmMEC(currentID);
            if (f.IsDisposed == false) st.myMDIMain.LoadFormF(f, false);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gList_CellDoubleClick(object sender, GridCellDoubleClickEventArgs e)
        {
            btnXemChiTiet.PerformClick();
        }

        private void gList_RowHeaderClick(object sender, GridRowHeaderClickEventArgs e)
        {
            if (e.GridRow == null) return;
            btnXemChiTiet.PerformClick();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            tTextBox txt = sender as tTextBox;
            if(txt==null) return;
            bsList.Filter = string.Format("{0} LIKE '%{1}%'", txt.Tag, txt.Text);
        }

        private void phimTat_CopyDong_Click(object sender, EventArgs e)
        {
            if (bsList.Current == null) return;

            gList.PrimaryGrid.FlushActiveRow();
            gList.CopyRow();

            ThemDuLieu();
        }

        private void phimTat_XoaDong_Click(object sender, EventArgs e)
        {
            if (bsList.Current == null) return;

            gList.PrimaryGrid.FlushActiveRow();
            gList.DeleteCurrentRow();

            XoaDuLieu();
        }       

        private void phimTat_XoaHet_Click(object sender, EventArgs e)
        {
            if (this.DATA == null) return;
            if (this.DATA == default(DataTable)) return;
            if (this.DATA.Rows.Count < 1) return;

            gList.PrimaryGrid.FlushActiveRow();
            gList.DeleteAllRow();

            XoaDuLieu();
        }

        private void XoaDuLieu()
        {
            if (this.DATA == default(DataTable)) return;
            using (tDBContext mainDB = new tDBContext())
            {
                //Xử lý xóa
                gList.PrimaryGrid.PurgeDeletedRows();
                DataTable delTable = this.DATA.GetChanges(DataRowState.Deleted);
                if (delTable != null && delTable.Rows.Count > 0)
                {
                    delTable = clsAll.DataTableOriginal(delTable);
                    List<MEC> delObj = clsAll.DataTable2ListObjects<MEC>(delTable);
                    mainDB.MECs.DeleteAllOnSubmit(delObj);
                }

                mainDB.SubmitAllChange();
                this.DATA.AcceptChanges();
                this.tShow1("Xóa thành công");
            }
        }

        private void ThemDuLieu()
        {
            if (this.DATA == default(DataTable)) return;
            using (tDBContext mainDB = new tDBContext())
            {
                //Xử lý thêm mới
                DataTable addedTable = this.DATA.GetChanges(DataRowState.Added);
                if (addedTable != null && addedTable.Rows.Count > 0)
                {
                    string strMaxKey = mainDB.MECs.Max("TK_ID");
                    if (string.IsNullOrEmpty(strMaxKey) == true) strMaxKey = "0";
                    int intMaxKey = Convert.ToInt32(strMaxKey);
                    foreach (DataRow r in addedTable.Rows)
                    {
                        r["TK_ID"] = ++intMaxKey;
                    }
                    List<MEC> addObj = clsAll.DataTable2ListObjects<MEC>(addedTable);
                    mainDB.MECs.InsertAllOnSubmit(addObj);
                }

                mainDB.SubmitAllChange();
                this.DATA.AcceptChanges();                
            }
        }

        private void phimTat_Excel_Click(object sender, EventArgs e)
        {
            if (this.DATA == null) return;
            if (this.DATA == default(DataTable)) return;
            if (this.DATA.Rows.Count < 1) return;

            gList.ExportExcel();
        }

        private void chuotPhai_XemChiTiet_Click(object sender, EventArgs e)
        {
            phimTat_XemChiTiet.PerformClick();
        }

        private void chuotPhai_CopyDong_Click(object sender, EventArgs e)
        {
            phimTat_CopyDong.PerformClick();
        }

        private void chuotPhai_XoaDong_Click(object sender, EventArgs e)
        {
            phimTat_XoaDong.PerformClick();
        }

        private void chuotPhai_XoaHet_Click(object sender, EventArgs e)
        {
            phimTat_XoaHet.PerformClick();
        }

        private void chuotPhai_XuatExcel_Click(object sender, EventArgs e)
        {
            phimTat_Excel.PerformClick();
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            frmMEC f = new frmMEC();
            f.Tag = this;
            if (f.IsDisposed == false) st.myMDIMain.LoadFormF(f,false);
        }       

        public void OnDataChanged()
        {
            if (bwLoadData.IsBusy == false) bwLoadData.RunWorkerAsync();
        }

        private void btnInAn_Click(object sender, EventArgs e)
        {

        }
    }
}
