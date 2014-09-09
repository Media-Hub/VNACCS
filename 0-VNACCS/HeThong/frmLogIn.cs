using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DevComponents.DotNetBar
{
    public partial class frmLogIn : DevComponents.DotNetBar.OfficeForm
    {
        public frmLogIn()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmLogIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (string.IsNullOrEmpty(st.LoginUsername)) Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            

            if (string.IsNullOrEmpty(txtUserName.Text) 
                //|| string.IsNullOrEmpty(txtPassword.Text)
                )
            {
                ToastNotification.Show(this,"Vui lòng nhập đủ email và mật khẩu.");                
                return;
            }

            try
            {
                using (tDBContext mainDB = new tDBContext())
                {
                    int i = mainDB.NACCS_USERs.Count(string.Format("N_USERNAME='{0}'", txtUserName.Text));                  
                    if (i > 0)
                    {
                        //Đăng nhập thành công thì load các giá trị mặc định
                        st.LoginUsername = txtUserName.Text;
                        clsAll.GetDefault(st.LoginUsername);
                        if(st.myMDIMain.bwCheckStatus.IsBusy==false)
                            st.myMDIMain.bwCheckStatus.RunWorkerAsync();
                        this.Close();
                    }
                    else
                    {
                        ToastNotification.Show(this, "Đăng nhập thất bại. Vui lòng kiểm tra lại email và mật khẩu.");                        
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                clsMx.Show(ex, this.Name);
            }
        }
    }
}
