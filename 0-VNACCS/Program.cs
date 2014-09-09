using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows.Forms;


namespace DevComponents.DotNetBar
{
    static class Program
    {
        /// <summary>
        /// Thiết lập theo en-US cho toàn bộ ứng dụng
        /// </summary>
        public static CultureInfo CulUS = new CultureInfo("en-US");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;
            CulUS.NumberFormat.NumberDecimalSeparator = ".";
            CulUS.NumberFormat.NumberGroupSeparator = ",";
            CulUS.DateTimeFormat.ShortDatePattern = "MM/dd/yyyy";
            CulUS.DateTimeFormat.LongDatePattern = "MMM dd yyyy";

            #region Kiểm tra một số điều kiện tiên quyết
            string strDBType = clsAll.GetRegValue(ct.DB_TYPE_REGISTER_KEY, DBManagement.Access.ToString());
            switch (strDBType)
            {
                case "Access":
                    string tempAccessDir = clsAll.GetRegValue(ct.ACCESS_DB_PATH_REGISTER_KEY, Application.StartupPath);
                    if (Directory.Exists(tempAccessDir) == false)
                    {
                        MessageBoxEx.Show(string.Format("Thư mục cài đặt dữ liệu access NACCS.VN.mdb là: [{0}] không tồn tại", tempAccessDir));
                        frmCaiDatCSDL f = new frmCaiDatCSDL();
                        DialogResult r = f.ShowDialog();
                        if (r != DialogResult.OK) return;
                    }
                    st.AccessDBDir = tempAccessDir = clsAll.GetRegValue(ct.ACCESS_DB_PATH_REGISTER_KEY, Application.StartupPath);;
                    break;
            }
            #endregion

            #region Giải phóng resources
            clsAll.ExtractResources();
            clsAll.ExtractJobResources();
            #endregion

            #region Xử lý kết nối DB           
            switch(strDBType)
            {
                case "Access":
                    clsDAL.defaultDBMan = DBManagement.Access;
                    break;
                case "SQL":
                    clsDAL.defaultDBMan = DBManagement.SQL;
                    break;
                case "SQLLite":
                    clsDAL.defaultDBMan = DBManagement.SQLLite;
                    break;
                case "Oracle":
                    string orcServerName = clsAll.GetRegValue(ct.ORC_SERVERNAME_REGISTER_KEY, "localhost");
                    string orcPort = clsAll.GetRegValue(ct.ORC_PORT_REGISTER_KEY, "1521");
                    string orcInstenceName = clsAll.GetRegValue(ct.ORC_INSTENCE_NAME_REGISTER_KEY, "orcl");
                    string orcUserName = clsAll.GetRegValue(ct.ORC_USERNAME_REGISTER_KEY, "VNACCS");
                    string orcPass = clsAll.GetRegValue(ct.ORC_PASSWORD_REGISTER_KEY, "1");
                    st.sqlSTRINGORACLE = string.Format(st.sqlSTRINGORACLE, orcServerName, orcPort, orcInstenceName, orcUserName, orcPass);
                    clsDAL.defaultDBMan = DBManagement.Oracle;
                    break;
            };
           //Kết nối thử
            try
            {
                using (tDBContext mainDB = new tDBContext())
                {}
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                frmCaiDatCSDL f = new frmCaiDatCSDL();
                DialogResult r= f.ShowDialog();
                if (r != DialogResult.OK) return;
            }
            #endregion

            #region Load danh mục  (Tạm thời để load danh mục ở đây, sau này tách ra form flash)
            try
            {
                using (tDBContext mainDB = new tDBContext())
                {
                    //Nước
                    DataTable dtNuoc = mainDB.SNUOCs.GetList("");
                    st.SNUOC = clsAll.AddFirstRowEmpty(dtNuoc);

                    //Phân loại cá nhân tổ chức
                    DataTable dtCaNhanToChuc = new DataTable("SCANHANTOCHUC");
                    DataColumn cMa = new DataColumn("MA", typeof(string));
                    dtCaNhanToChuc.Columns.Add(cMa);
                    DataColumn cTen = new DataColumn("TEN", typeof(string));
                    dtCaNhanToChuc.Columns.Add(cTen);
                    DataRow r = dtCaNhanToChuc.NewRow(); r[0] = ""; r[1] = ""; dtCaNhanToChuc.Rows.Add(r);
                    DataRow r1 = dtCaNhanToChuc.NewRow(); r1[0] = "1"; r1[1] = "Cá nhân gửi cá nhân"; dtCaNhanToChuc.Rows.Add(r1);
                    DataRow r2 = dtCaNhanToChuc.NewRow(); r2[0] = "2"; r2[1] = "Tổ chức gửi cá nhân"; dtCaNhanToChuc.Rows.Add(r2);
                    DataRow r3 = dtCaNhanToChuc.NewRow(); r3[0] = "3"; r3[1] = "Cá nhân gửi tổ chức"; dtCaNhanToChuc.Rows.Add(r3);
                    DataRow r4 = dtCaNhanToChuc.NewRow(); r4[0] = "4"; r4[1] = "Tổ chức gửi tổ chức"; dtCaNhanToChuc.Rows.Add(r4);
                    DataRow r5 = dtCaNhanToChuc.NewRow(); r5[0] = "5"; r5[1] = "Khác"; dtCaNhanToChuc.Rows.Add(r5);
                    st.SCANHANTOCHUC = dtCaNhanToChuc;

                    //hải quan
                    DataTable dtHaiQuan = mainDB.SHAIQUANs.GetList("");
                    st.SHAIQUAN = clsAll.AddFirstRowEmpty(dtHaiQuan);

                    //Bộ phận xử lý tờ khai
                    DataTable dtBoPhan = mainDB.SBOPHANHAIQUANVNACCSs.GetList("");
                    st.SBOPHAN = clsAll.AddFirstRowEmpty(dtBoPhan);

                    //Địa điểm lưu kho
                    DataTable dtDiaDiem = mainDB.SDDLKs.GetList("");
                    st.SDDLK = clsAll.AddFirstRowEmpty(dtDiaDiem);

                    //Cửa khẩu trong nước                
                    DataTable dtCK = mainDB.SCUAKHAUs.GetList("");
                    st.SCUAKHAU = clsAll.AddFirstRowEmpty(dtCK);

                    //Cửa khẩu nước ngoài
                    DataTable dtCKNN = mainDB.SCUAKHAUNNs.GetList("");
                    st.SCUAKHAUNN = clsAll.AddFirstRowEmpty(dtCKNN);

                    //Nguyên tệ
                    DataTable dtNguyenTe = mainDB.SNGHTEs.GetList("");
                    st.SNGTE = clsAll.AddFirstRowEmpty(dtNguyenTe);

                    //điều kiện giao hàng
                    DataTable dtDKGH = mainDB.SDKGHs.GetList("");
                    st.SDKGH = clsAll.AddFirstRowEmpty(dtDKGH);

                    //phân loại giá hóa đơn
                    DataTable dtPhanLoaiGiaHoaDon = new DataTable("SPHANLOAIGIAHOADON");
                    DataColumn c1 = new DataColumn("MA", typeof(string));
                    dtPhanLoaiGiaHoaDon.Columns.Add(c1);
                    DataColumn c2 = new DataColumn("TEN", typeof(string));
                    dtPhanLoaiGiaHoaDon.Columns.Add(c2);
                    DataRow rr1 = dtPhanLoaiGiaHoaDon.NewRow();
                    rr1["MA"] = "A"; rr1["TEN"] = "Giá hóa đơn cho hàng hóa phải trả tiền";
                    dtPhanLoaiGiaHoaDon.Rows.Add(rr1);
                    DataRow rr2 = dtPhanLoaiGiaHoaDon.NewRow();
                    rr2["MA"] = "B"; rr2["TEN"] = "Giá hóa đơn cho hàng hóa miễn trả tiền (free of charge)";
                    dtPhanLoaiGiaHoaDon.Rows.Add(rr2);
                    DataRow rr3 = dtPhanLoaiGiaHoaDon.NewRow();
                    rr3["MA"] = "C"; rr3["TEN"] = "Giá hóa đơn cho hàng hóa lẫn cả phải trả tiền và miễn trả tiền";
                    dtPhanLoaiGiaHoaDon.Rows.Add(rr3);
                    DataRow rr4 = dtPhanLoaiGiaHoaDon.NewRow();
                    rr4["MA"] = "D"; rr4["TEN"] = "Các trường hợp khác";
                    dtPhanLoaiGiaHoaDon.Rows.Add(rr4);
                    st.SPHANLOAIGIAHOADON = clsAll.AddFirstRowEmpty(dtPhanLoaiGiaHoaDon);

                    //phân loại phí vận chuyển
                    DataTable dtPhanLoaiPhiVanChuyen = new DataTable("SPHANLOAIPHIVANCHUYEN");
                    DataColumn cc1 = new DataColumn("MA", typeof(string));
                    dtPhanLoaiPhiVanChuyen.Columns.Add(cc1);
                    DataColumn cc2 = new DataColumn("TEN", typeof(string));
                    dtPhanLoaiPhiVanChuyen.Columns.Add(cc2);
                    DataRow ra1 = dtPhanLoaiPhiVanChuyen.NewRow();
                    ra1["MA"] = "A"; ra1["TEN"] = "Trường hợp chứng từ vận tải ghi tổng số tiền cước phí chung cho tất cả hàng trên hóa đơn";
                    dtPhanLoaiPhiVanChuyen.Rows.Add(ra1);
                    DataRow ra2 = dtPhanLoaiPhiVanChuyen.NewRow();
                    ra2["MA"] = "B"; ra2["TEN"] = "Trường hợp Hóa đơn lô hàng có cả hàng tiền và hàng F.O.C hoặc tách riêng phí vận tải của hàng trả tiền và hàng F.O.C trên chứng từ vận tải";
                    dtPhanLoaiPhiVanChuyen.Rows.Add(ra2);
                    DataRow ra3 = dtPhanLoaiPhiVanChuyen.NewRow();
                    ra3["MA"] = "C"; ra3["TEN"] = "Trường hợp tờ khai chỉ nhập khẩu một phần hàng hóa của lô hàng trên chứng từ vận tải";
                    dtPhanLoaiPhiVanChuyen.Rows.Add(ra3);
                    DataRow ra4 = dtPhanLoaiPhiVanChuyen.NewRow();
                    ra4["MA"] = "D"; ra4["TEN"] = "Phân bổ cước phí vận tải theo tỷ lệ trọng lượng, dung tích";
                    dtPhanLoaiPhiVanChuyen.Rows.Add(ra4);
                    DataRow ra5 = dtPhanLoaiPhiVanChuyen.NewRow();
                    ra5["MA"] = "E"; ra5["TEN"] = "Trường hợp trị giá hóa đơn của hàng hóa đã có phí vận tải (ví dụ CIF, C&F, CIP) nhưng cước phí thực tế vượt quá cước phí trên hóa đơn (phát sinh thêm phí vận tải)";
                    dtPhanLoaiPhiVanChuyen.Rows.Add(ra5);
                    DataRow ra6 = dtPhanLoaiPhiVanChuyen.NewRow();
                    ra6["MA"] = "F"; ra6["TEN"] = "Trường hợp có cước vượt cước và chỉ nhập khẩu một phần hàng hóa của lô hàng";
                    dtPhanLoaiPhiVanChuyen.Rows.Add(ra6);
                    st.SPHANLOAIGIAHOADON = clsAll.AddFirstRowEmpty(dtPhanLoaiPhiVanChuyen);

                    //phân loại phí bảo hiểm
                    DataTable dtPhanLoaiPhiBaoHiem = new DataTable("SPHANLOAIPHIVANCHUYEN");
                    DataColumn bh1 = new DataColumn("MA", typeof(string));
                    dtPhanLoaiPhiBaoHiem.Columns.Add(bh1);
                    DataColumn bh2 = new DataColumn("TEN", typeof(string));
                    dtPhanLoaiPhiBaoHiem.Columns.Add(bh2);
                    DataRow rbh1 = dtPhanLoaiPhiBaoHiem.NewRow();
                    rbh1["MA"] = "A"; rbh1["TEN"] = "Bảo hiểm riêng";
                    dtPhanLoaiPhiBaoHiem.Rows.Add(rbh1);
                    DataRow rbh2 = dtPhanLoaiPhiBaoHiem.NewRow();
                    rbh2["MA"] = "B"; rbh2["TEN"] = "Bảo hiểm tổng hợp";
                    dtPhanLoaiPhiBaoHiem.Rows.Add(rbh2);
                    DataRow rbh3 = dtPhanLoaiPhiBaoHiem.NewRow();
                    rbh3["MA"] = "D"; rbh3["TEN"] = "Không bảo hiểm";
                    dtPhanLoaiPhiBaoHiem.Rows.Add(rbh3);
                    st.SPHANLOAIPHIBAOHIEM = clsAll.AddFirstRowEmpty(dtPhanLoaiPhiBaoHiem);

                    //phuong thuc thanh toan
                    DataTable dtPTTT = mainDB.SPTTTs.GetList("");
                    st.SPTTT = clsAll.AddFirstRowEmpty(dtPTTT);

                }
            }
            catch (Exception ex)
            {
                clsMx.Show1(ex, "static void Main()");
                return;
            }
            #endregion

         
#if testcontrol
            Application.Run(new TestControl());
#else
            st.myMDIMain = new frmMain();
            st.myMDIMain.styleManager1.ManagerStyle = eStyle.Office2007Blue;
            Application.Run(st.myMDIMain);
#endif
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            clsMx.Show(e.Exception, "Application_ThreadException");
        }
    }
}
