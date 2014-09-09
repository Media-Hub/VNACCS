using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DevComponents.DotNetBar
{
    public partial class frmCaiDatCSDL : DevComponents.DotNetBar.tForm
    {
        public frmCaiDatCSDL()
        {
            InitializeComponent();
            string strDBType = clsAll.GetRegValue(ct.DB_TYPE_REGISTER_KEY, DBManagement.Access.ToString());
            if (strDBType.Equals(DBManagement.Access.ToString()))
            {
                rAccess.Checked = true;
            }
            if (strDBType.Equals(DBManagement.SQL.ToString()))
            {
                rSQL.Checked = true;
            }
            if (strDBType.Equals(DBManagement.Oracle.ToString()))
            {
                rOracle.Checked = true;
            }
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            if (rAccess.Checked == true)
            {
                clsAll.SetRegValue(ct.DB_TYPE_REGISTER_KEY, DBManagement.Access.ToString());
                clsAll.SetRegValue(ct.ACCESS_DB_PATH_REGISTER_KEY, txtAccessFolder.Text);
                ToastNotification.Show(this, "Ghi thành công");
                return;
            }

            if (rSQL.Checked == true)
            {
                clsAll.SetRegValue(ct.DB_TYPE_REGISTER_KEY, DBManagement.SQL.ToString());
                clsAll.SetRegValue(ct.SQL_SERVERNAME_REGISTER_KEY, txtSQLServerName.Text);
                clsAll.SetRegValue(ct.SQL_DBNAME_REGISTER_KEY,txtSQLDBName.Text);
                clsAll.SetRegValue(ct.SQL_USERNAME_REGISTER_KEY, txtSQLUsername.Text);
                clsAll.SetRegValue(ct.SQL_PASSWORD_REGISTER_KEY,txtSQLPass.Text);
                ToastNotification.Show(this, "Ghi thành công");
                return;
            }

            if (rOracle.Checked == true)
            {
                string tempStr = string.Format("Data Source=//{0}:{1}/{2};User ID={3};pwd={4}", txtOracleServerName.Text, txtOraclePort.Text, txtOracleInstence.Text, txtOracleUsername.Text, txtOraclePass.Text);                
                //Kết nối thử
                try
                {
                    using (tDBContext mainDB = new tDBContext(tempStr, DBManagement.Oracle))
                    { }
                    st.sqlSTRINGORACLE = tempStr;
                    clsAll.SetRegValue(ct.DB_TYPE_REGISTER_KEY, DBManagement.Oracle.ToString());
                    clsAll.SetRegValue(ct.ORC_SERVERNAME_REGISTER_KEY, txtOracleServerName.Text);
                    clsAll.SetRegValue(ct.ORC_PORT_REGISTER_KEY, txtOraclePort.Text);
                    clsAll.SetRegValue(ct.ORC_INSTENCE_NAME_REGISTER_KEY, txtOracleInstence.Text);
                    clsAll.SetRegValue(ct.ORC_USERNAME_REGISTER_KEY, txtOracleUsername.Text);
                    clsAll.SetRegValue(ct.ORC_PASSWORD_REGISTER_KEY, txtOraclePass.Text);
                                        
                    //ToastNotification.Show(this, "Ghi thành công");
                    //MessageBoxEx.Show("Kết nối thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    string s = ex.Message;                    
                    //ToastNotification.Show(this, "Kết nối không thành công. Nội dung lỗi:\n"+ex.Message);
                    MessageBoxEx.Show("Kết nối không thành công. Nội dung lỗi:\n" + ex.Message, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //this.DialogResult = DialogResult.No;
                }
                
                return;
            }
            
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
    }
}
