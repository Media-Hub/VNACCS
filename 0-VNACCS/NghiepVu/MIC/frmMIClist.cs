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
    public partial class frmMIClist : DevComponents.DotNetBar.tForm
    {
        private DataTable DATA = default(DataTable);

        public frmMIClist()
        {
            InitializeComponent();
            progressBarX1.SendToBack();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void frmMIClist_Load(object sender, EventArgs e)
        {
            if (bwLoadData.IsBusy == false) bwLoadData.RunWorkerAsync();
        }

        private void bwLoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            progressBarX1.BringToFront();
            using (tDBContext mainDB = new tDBContext())
            {
               this.DATA = mainDB.MICs.GetList("");
               System.Threading.Thread.Sleep(3000);
            }
        }

        private void bwLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            gList.PrimaryGrid.DataSource = this.DATA;
            progressBarX1.SendToBack();
        }

        private void xemChiTietToolStripMenuItem_Click(object sender, EventArgs e)
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
            int currentID = (int)cellID.Value;
            frmMIC f = new frmMIC(currentID);
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
    }
}
