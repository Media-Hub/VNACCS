using DevComponents.DotNetBar.Controls;
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
    public partial class frmSCUCHQ : DevComponents.DotNetBar.tForm
    {
        DataTable DATA = default(DataTable);
        public frmSCUCHQ()
        {
            InitializeComponent();
            FormatColumns();

            progressBarX1.SendToBack();
            if (bwLoadData.IsBusy == false)
            {
                progressBarX1.BringToFront();
                bwLoadData.RunWorkerAsync();
            }
            
        }

        private void FormatColumns()
        {
            //GridPanel panel = gList.PrimaryGrid;
            //GridColumn NT_TYGIAVND = panel.Columns["NT_TYGIAVND"];
            //GridDoubleInputEditControl NT_TYGIAVND_hienthi =
            //    (GridDoubleInputEditControl)NT_TYGIAVND.RenderControl;
            //NT_TYGIAVND_hienthi.DisplayFormat = "#,##0.##################";
            //GridDoubleInputEditControl NT_TYGIAVND_edit =
            //   (GridDoubleInputEditControl)NT_TYGIAVND.EditControl;
            //NT_TYGIAVND_edit.DisplayFormat = "0.##################";
        }       

        private void frmSCUCHQ_Load(object sender, EventArgs e)
        {
            
        }

        private void bwLoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            using (tDBContext mainDB = new tDBContext())
            {
                this.DATA = mainDB.SCUCHQs.GetList("");
            }
        }

        private void bwLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarX1.SendToBack();
            bsList.DataSource = this.DATA;
            gList.PrimaryGrid.DataSource = bsList;
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            ghiDuLieuToolStripMenuItem.PerformClick();
        }

        private void ghiDuLieuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GhiDuLieu();
            }
            catch (Exception ex)
            {
                clsMx.Show(ex, this.Name);
            }
        }

        private void GhiDuLieu()
        {
            gList.PrimaryGrid.FlushActiveRow();
            if (this.DATA == default(DataTable)) return;
            using (tDBContext mainDB = new tDBContext())
            {
                //Xử lý xóa
                gList.PrimaryGrid.PurgeDeletedRows();
                DataTable delTable = this.DATA.GetChanges(DataRowState.Deleted);
                if (delTable != null && delTable.Rows.Count > 0)
                {
                    delTable = clsAll.DataTableOriginal(delTable);
                    List<SCUCHQ> delObj = clsAll.DataTable2ListObjects<SCUCHQ>(delTable);
                    mainDB.SCUCHQs.DeleteAllOnSubmit(delObj);
                }                

                //Xử lý thêm mới
                DataTable addedTable = this.DATA.GetChanges(DataRowState.Added);
                if (addedTable != null && addedTable.Rows.Count > 0)
                {
                    List<SCUCHQ> addObj = clsAll.DataTable2ListObjects<SCUCHQ>(addedTable);
                    mainDB.SCUCHQs.InsertAllOnSubmit(addObj);
                }

                //Xử lý sửa
                DataTable modifiedTable = this.DATA.GetChanges(DataRowState.Modified);
                if (modifiedTable != null && modifiedTable.Rows.Count > 0)
                {
                    List<SCUCHQ> changeObj = clsAll.DataTable2ListObjects<SCUCHQ>(modifiedTable);
                    mainDB.SCUCHQs.UpdateAllOnSubmit(changeObj);
                }
                
                mainDB.SubmitAllChange();
                this.DATA.AcceptChanges();
            }

            ToastNotification.Show(this, "Ghi thành công");
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBoxX txt = sender as TextBoxX;
                if(txt==null)return;
                bsList.Filter = string.Format("{0} LIKE '%{1}%'",txt.Tag,txt.Text );
            }
            catch { }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void hoaHangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gList.DeleteCurrentRow();
        }

        private void xoaHetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gList.DeleteAllRow();
        }

        private void autoExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gList.ExportExcel();
        }

        private void copyHangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gList.CopyRow();
        }

        private void btnDongBo_Click(object sender, EventArgs e)
        {

        }
    }
}
