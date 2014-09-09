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
    public partial class frmGC_HDGC_NGUYENLIEUlist : tForm
    {                                                                                            
        private DataTable DATA = default(DataTable);
        GC_HDGC HDGC = default(GC_HDGC);                                    
                                                                                                
        public frmGC_HDGC_NGUYENLIEUlist(GC_HDGC HDGC)
        {                                                                                       
            InitializeComponent();
            this.HDGC = HDGC;                                                
            proLoad.SendToBack();
            Control.CheckForIllegalCrossThreadCalls = false;                                    
            LoadDanhMuc();
        }                                                                                       
    
        private void LoadDanhMuc()  
        {                           
                                    
        }                           
    
        private void frmGC_HDGC_NGUYENLIEUlist_Load(object sender, EventArgs e)
        {                                                                                       
            if (bwLoadData.IsBusy == false) bwLoadData.RunWorkerAsync();                        
        }                                                                                       
                                                                                                
        private void bwLoadData_DoWork(object sender, DoWorkEventArgs e)                        
        {                                                                                       
            proLoad.BringToFront();                                                             
            using (tDBContext mainDB = new tDBContext())                                        
            {                                                                                   
               this.DATA = mainDB.GC_HDGC_NGUYENLIEUs.GetList("");
               //System.Threading.Thread.Sleep(1000);                                           
            }                                                                                   
        }                                                                                       
                                                                                                
        private void bwLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {                                                                                       
            bsList.DataSource = this.DATA;                                                      
            gList.PrimaryGrid.DataSource = bsList;                                              
            proLoad.SendToBack();                                                               
        }                                                                                       
                                                                                                
        private void phimTat_GhiDuLieu_Click(object sender, EventArgs e)                       
        {                                                                                       
            btnGhiDuLieu.PerformClick();                                                       
        }                                                                                       
                                                                                                
        private void btnGhiDuLieu_Click(object sender, EventArgs e)                            
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
                        List<GC_HDGC_NGUYENLIEU> delObj = clsAll.DataTable2ListObjects<GC_HDGC_NGUYENLIEU>(delTable);
                        mainDB.GC_HDGC_NGUYENLIEUs.DeleteAllOnSubmit(delObj);                    }
            
                    //Xử lý thêm mới
                    DataTable addedTable = this.DATA.GetChanges(DataRowState.Added);
                    if (addedTable != null && addedTable.Rows.Count > 0)
                    {
                        List<GC_HDGC_NGUYENLIEU> addObj = clsAll.DataTable2ListObjects<GC_HDGC_NGUYENLIEU>(addedTable);
                        string strMaxID = mainDB.GC_HDGC_NGUYENLIEUs.Max("NL_ID");
                        if (string.IsNullOrEmpty(strMaxID)) strMaxID = "0";
                        int intMaxID = Convert.ToInt32(strMaxID);
                        foreach (GC_HDGC_NGUYENLIEU n in addObj)
                        {
                            n.NL_HDID = this.HDGC.HD_ID;
                            n.NL_ID = ++intMaxID;
                        }
                        mainDB.GC_HDGC_NGUYENLIEUs.InsertAllOnSubmit(addObj);                    
                    }
            
                    //Xử lý sửa
                    DataTable modifiedTable = this.DATA.GetChanges(DataRowState.Modified);
                    if (modifiedTable != null && modifiedTable.Rows.Count > 0)
                    {
                        List<GC_HDGC_NGUYENLIEU> changeObj = clsAll.DataTable2ListObjects<GC_HDGC_NGUYENLIEU>(modifiedTable);
                        mainDB.GC_HDGC_NGUYENLIEUs.UpdateAllOnSubmit(changeObj);                    }
            
                    mainDB.SubmitAllChange();
                    this.DATA.AcceptChanges();
                }
            
                this.tShow1("Ghi thành công");
            }  
                                                                                                
        private void btnThoat_Click(object sender, EventArgs e)                                 
        {                                                                                       
            this.Close();                                                                       
        }                                                                                       
                                                                                                
        private void gList_CellDoubleClick(object sender, GridCellDoubleClickEventArgs e)       
        {                                                                                       
                                                                 
        }                                                                                       
                                                                                                
        private void gList_RowHeaderClick(object sender, GridRowHeaderClickEventArgs e)         
        {                                                                                       
            if (e.GridRow == null) return;                                                      
                                                                   
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
                                                                                                
                                                                                  
        }                                                                                       
                                                                                                
        private void phimTat_XoaDong_Click(object sender, EventArgs e)                          
        {                                                                                       
            if (bsList.Current == null) return;                                                 
                                                                                                
            gList.PrimaryGrid.FlushActiveRow();                                                 
            gList.DeleteCurrentRow();                                                           
                                                                                                
                                                                                    
        }                                                                                       
                                                                                                
        private void phimTat_XoaHet_Click(object sender, EventArgs e)                           
        {                                                                                       
            if (this.DATA == null) return;                                                      
            if (this.DATA == default(DataTable)) return;                                        
            if (this.DATA.Rows.Count < 1) return;                                               
                                                                                                
            gList.PrimaryGrid.FlushActiveRow();                                                 
            gList.DeleteAllRow();                                                               
                                                                                                
                                                                                   
        }                                                                                       
                                                                                                
        private void phimTat_Excel_Click(object sender, EventArgs e)                            
        {                                                                                       
            if (this.DATA == null) return;                                                      
            if (this.DATA == default(DataTable)) return;                                        
            if (this.DATA.Rows.Count < 1) return;                                               
                                                                                                
            gList.ExportExcel();                                                                
        }                                                                                       
                                                                                                
        private void chuotPhai_GhiDuLieu_Click(object sender, EventArgs e)                     
        {                                                                                       
            phimTat_GhiDuLieu.PerformClick();                                                  
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
                                                                                                
                                                                                                
        public void OnDataChanged()                                                             
        {                                                                                       
            if (bwLoadData.IsBusy == false) bwLoadData.RunWorkerAsync();                        
        }                                                                                       
                                                                                                
        private void btnInAn_Click(object sender, EventArgs e)                                  
        {                                                                                       
                                                                                                
        }                                                                                       
    }                                                                                           
}
