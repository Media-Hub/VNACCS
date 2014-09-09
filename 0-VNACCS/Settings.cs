using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DevComponents.DotNetBar
{
    public class st
    {
        //Liên quan tới CSDL
        public static string AccessDBDir = Application.StartupPath;
        public static string sqlSTRINGADO = "Data Source=(local);Initial Catalog=NACCS.VN;User ID=sa;pwd=1";
        public static string sqlSTRINGOLE = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\NACCS.VN.mdb;Persist Security Info=True;Jet OLEDB:Database Password=tuyenchim";
        public static string sqlSTRINGLITE = "Data Source= |DataDirectory|NACCS.VN.db;Version=3;New=False;Compress=True;";
        public static string sqlSTRINGORACLE = "Data Source=//{0}:{1}/{2};User ID={3};pwd={4}";

        //Liên quan tới thư mục tạm, excel
        public static string TEMP_DIR = Path.Combine(Application.StartupPath, "@tempVNACCS");

        //Liên quan đến lỗi, thông báo lỗi, tính năng học lỗi
        public static string CurrentErrorFormName = "ALL";
        public static string CurrentErrorMessage = "Lỗi ngoại lệ không xác định";
        public static string CurrentErrorLearned = "0";
        
        //Liên quan tới hệ thống, kỹ thuật lập trình
        public static frmMain myMDIMain;

        //Liên quan tới tài khoản đăng nhập phần mềm này
        public static string LoginUsername = string.Empty;//Tài khoản đăng nhập vào hệ thống này

        //Cổng tiếp nhận VNACCS
        //public static string  uri = "https://intrctv.vnaccs.customs.gov.vn/";
        //

        //he thong test
        public static string fIPHQ = "https://ediconn.vnaccs.customs.gov.vn/";

        //Xác định là khai cho mã đơn vị nào
        public static string fMaDV = string.Empty;
        public static string fTenDV = string.Empty;
        public static string fDiaChiDV = string.Empty;
        public static string fDienThoaiDV = string.Empty;

        //
        public static string fMaHQ = string.Empty;
        public static string fTenHQ = string.Empty;
        public static string fMaDoiNhap = string.Empty;
        public static string fTenDoiNhap = string.Empty;
        public static string fMaDoiXuat = string.Empty;
        public static string fTenDoiXuat = string.Empty;
        //
        public static string fUserIDVNACCS = string.Empty;
        public static string fPasswordVNACCS = string.Empty;
        public static string fTerminalID = string.Empty;
        public static string fTerminalAccessKey = string.Empty;

        //
        public static string fMaHQV4 = string.Empty;
        public static string fTenHQV4 = string.Empty;
        //
        public static string fIPHQV4 = string.Empty;
        public static string fTaiKhoanV4 = string.Empty;
        public static string fMatKhauV4 = string.Empty;

        //Liên quan tới proxy
        public static string fProxyOption = "1";//1-No proxy,   2-Use IE proxy,   3-Manual proxy
        public static string fProxyIP = string.Empty;
        public static string fProxyPort = string.Empty;
        public static string fProxyUser = string.Empty;
        public static string fProxyPass = string.Empty;

        public static DataTable SHAIQUAN = null;
        public static DataTable SBOPHAN = null;
        public static DataTable SNUOC = null;
        public static DataTable SCANHANTOCHUC = null;
        public static DataTable SDDLK = null;
        public static DataTable SCUAKHAU = null;
        public static DataTable SCUAKHAUNN = null;
        public static DataTable SNGTE = null;
        public static DataTable SDKGH = null;
        public static DataTable SPHANLOAIGIAHOADON = null;
        public static DataTable SPHANLOAIPHIVANCHUYEN = null;
        public static DataTable SPHANLOAIPHIBAOHIEM = null;
        public static DataTable SPTTT = null;
        
    }
}
