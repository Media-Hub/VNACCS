using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DevComponents.DotNetBar
{
    public class clsMx
    {
        public static void Show(Exception ex,string FormName="ALL")
        { 
            //1. Xử lý ghi ra file log
            clsAll.WriteLog(ex.Message, FormName, ex.Source);

            //2. Kiểm tra xem trong DB lỗi này đã được học chưa
            //   2.1. Nếu chưa được học
            //        2.1.1 Đẩy thông tin exception về máy chủ
            //   2.2. Mếu đã được học
            //        2.2.1. Lấy thông tin xử lý lỗi ra cho người dùng xem

            //3. Hiển thị dialog
            tDialog f = new tDialog();
            if (f.IsDisposed == false)
            {
                f.txtNoiDungLoi.Text =string.Format("Hệ thống phát sinh lỗi ngoại lệ không xác định. Nội dung lỗi:\n{0}", ex.Message);
                st.myMDIMain.LoadFormF(f, true);
            }
        }

        public static void Show1(Exception ex, string FormName = "ALL")
        {
            //1. Xử lý ghi ra file log
            clsAll.WriteLog(ex.Message, FormName, ex.Source);

            //2. Kiểm tra xem trong DB lỗi này đã được học chưa
            //   2.1. Nếu chưa được học
            //        2.1.1 Đẩy thông tin exception về máy chủ
            //   2.2. Mếu đã được học
            //        2.2.1. Lấy thông tin xử lý lỗi ra cho người dùng xem

            //3. Hiển thị dialog           
            string s = string.Format("Hệ thống phát sinh lỗi ngoại lệ không xác định. Nội dung lỗi:\n{0}", ex.Message);
            MessageBoxEx.Show(s, "Báo lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
