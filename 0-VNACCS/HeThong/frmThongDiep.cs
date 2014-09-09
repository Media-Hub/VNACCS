using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DevComponents.DotNetBar
{
    public partial class frmThongDiep : DevComponents.DotNetBar.tForm
    {
        DataTable DATA = default(DataTable);
        public frmThongDiep()
        {
            InitializeComponent();
            if (bwLoadData.IsBusy == false) bwLoadData.RunWorkerAsync();
        }

        private void frmThongDiep_Load(object sender, EventArgs e)
        {

        }

        private void bwLoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            using (tDBContext mainDB = new tDBContext())
            {
                DATA = mainDB.THONGDIEPs.GetList("");
            }
        }

        private void bwLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            gList.PrimaryGrid.AutoGenerateColumns = false;
            gList.PrimaryGrid.DataSource = DATA;
        }
    }
}
