using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DevComponents.DotNetBar
{
    public class tGrid : SuperGridControl
    {
        public tGrid()
        {            
            InitializeComponent();
        }

        bool _autoExcel=true;
        [DefaultValue(true)]
        public bool AutoExcel
        {
            get{return _autoExcel;}
            set{_autoExcel=value;}
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // tGrid
            // 
            this.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.PrimaryGrid.AllowRowHeaderResize = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tGrid_KeyDown);
            this.ResumeLayout(false);

        }

        private void tGrid_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        public void ExportExcel()
        {
            try
            {
                //if (e.KeyCode != Keys.F12) return;
                if (_autoExcel == false) return;
                BindingSource bsTemp = this.PrimaryGrid.DataSource as BindingSource;
                if (bsTemp == null) return;
                DataTable tempDATA = bsTemp.DataSource as DataTable;
                if (tempDATA == null) return;
                DataTable DATA = tempDATA;
                DATA.RejectChanges();

                Excel.Application objExcel;
                try
                {
                    //Tìm instance Excel đang chạy.
                    objExcel = (Excel.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application");
                }
                catch
                {
                    //Không có instance nào của Excel đang chạy.
                    objExcel = new Excel.Application();
                }

                try
                {
                    objExcel.Interactive = false;
                    objExcel.Interactive = true;
                }
                catch
                {
                    //CrossShow2 msg = new CrossShow2(clsMx.Show);
                    //this.Invoke(msg, Qk.Settings.ctrmsgPrintError, eAlertType.CanhBao);
                    return;
                }
                objExcel.Visible = false;
                string tmpFileXSL = Path.Combine(st.TEMP_DIR, Guid.NewGuid().ToString() + ".xls");
                clsAll.ExtractXLS("AutoExcel.xls", tmpFileXSL);
                objExcel.Workbooks.Open(tmpFileXSL);

                object[,] objData = clsAll.DataTable2ArrayObjects(DATA);
                string strRange = string.Format("A{0}:{1}{2}", 1, clsAll.GetExcelColumnLabel(DATA.Columns.Count), DATA.Rows.Count);
                objExcel.Range[strRange].Value = objData;
                objExcel.Visible = true;
                objExcel.ActiveWorkbook.Save();
                //objExcel.Worksheets.PrintPreview();
            }
            catch (Exception ex)
            {
                clsMx.Show(ex, this.Name);
            }
        }

        /// <summary>
        /// Chỉ hỗ trợ khi sử dụng binding source và datatable
        /// </summary>
        public void CopyRow()
        {
            if (this.PrimaryGrid.DataSource == null) return;
            BindingSource bsList = this.PrimaryGrid.DataSource as BindingSource;
            if (bsList == null) return;
            if (bsList.Current == null) return;
            DataTable DATA = bsList.DataSource as DataTable;
            if (DATA == null) return;
            DataRowView rv = bsList.Current as DataRowView;
            if (rv == null) return;
            DataRow r = rv.Row as DataRow;
            if (r == null) return;
            DataRow newR = DATA.NewRow();
            newR.ItemArray = r.ItemArray.Clone() as object[];
            DATA.Rows.InsertAt(newR, DATA.Rows.IndexOf(r) + 1);
        }

        /// <summary>
        /// Chỉ hỗ trợ khi sử dụng binding source và datatable
        /// </summary>
        public void DeleteAllRow()
        {
            if (this.PrimaryGrid.DataSource == null) return;
            BindingSource bsList = this.PrimaryGrid.DataSource as BindingSource;
            if (bsList == null) return;
            try
            {
                while (bsList.Current != null)
                    bsList.RemoveCurrent();
            }
            catch { };
        }

        /// <summary>
        /// Chỉ hỗ trợ khi sử dụng binding source và datatable
        /// </summary>
        public void DeleteCurrentRow()
        {
            if (this.PrimaryGrid.DataSource == null) return;
            BindingSource bsList = this.PrimaryGrid.DataSource as BindingSource;
            if (bsList == null) return;
            try
            {
                bsList.RemoveCurrent();
            }
            catch { };
        }
    }
}
