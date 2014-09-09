using System;
using System.Data;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
namespace DevComponents.DotNetBar
{
	/// <summary>
	/// DB context sử dụng trong using.
	/// </summary>
	public partial class tDBContext : IDisposable
	{
		private string m_ConnectionString = default(string);
		private IDbConnection m_Connection = null;
		private Dictionary<Type, object> m_Tables = default(Dictionary<Type,object>);
		private List<object> l_Tables = default(List<object>);
		/// <summary>
		/// Khởi tạo dữ liệu.
		/// </summary>
		private void Initialization()
		{
			m_Tables = new Dictionary<Type, object>();
			l_Tables = new List<object>();
		}

		/// <summary>
		/// Hàm khởi tạo.
		/// </summary>
		/// <param name="conn">Connection đến DB</param>
		public tDBContext(IDbConnection conn)
		{
			m_Connection = conn;
			m_ConnectionString = conn.ConnectionString;
			Initialization();
		}

		/// <summary>
		/// Hàm khởi tạo.
		/// </summary>
		/// <param name="conString">ConnectionString đến DB</param>
		public tDBContext(string conString)
			: this(conString, clsDAL.defaultDBMan)
		{
		}

		/// <summary>
		/// Hàm khởi tạo.
		/// </summary>
		/// <param name="conString">ConnectionString đến DB</param>
		public tDBContext(string conString, DBManagement DBMan)
			: this(clsConn.getConnection(conString, DBMan))
		{
			_dataManagement = DBMan;
		}

		/// <summary>
		/// Hàm khởi tạo. với connection mặc định
		/// </summary>
		public tDBContext()
			: this(clsDAL.defaultDBMan)
		{
		}

		/// <summary>
		/// Hàm khởi tạo. với connection mặc định
		/// </summary>
		public tDBContext(DBManagement DBMan)
			: this(clsConn.getConnection(DBMan))
		{
			_dataManagement = DBMan;
		}

		public void Dispose()
		{
			if (m_Connection != null && m_Connection.State == ConnectionState.Open)
				m_Connection.Close();
			m_Connection = null;
			if (m_Tables!=null)
				m_Tables.Clear();
			m_Tables = null;
		}

		private DBManagement _dataManagement;
		/// <summary>
		/// Hệ quản trị CSDL.
		/// </summary>
		public DBManagement DataManagement { get { return _dataManagement; } set { _dataManagement = value; } }
		/// <summary>
		/// Lấy ra Table kiểu T.
		/// </summary>
		protected DBTable<T> GetTable<T>()
		{
			if (!m_Tables.ContainsKey(typeof(T)))
			{
				DBTable<T> tbl = new DBTable<T>(m_Connection, this.DataManagement);
				l_Tables.Add(tbl);
				m_Tables.Add(typeof(T), tbl);
			}
			return m_Tables[typeof(T)] as DBTable<T>;
		}

		/// <summary>
		/// Lấy ra View kiểu T.
		/// </summary>
		protected DBView<T> GetView<T>()
		{
			if (!m_Tables.ContainsKey(typeof(T)))
			{
				m_Tables.Add(typeof(T), new DBView<T>(m_Connection, this.DataManagement));
			}
			return m_Tables[typeof(T)] as DBView<T>;
		}

		/// <summary>
		/// Thuộc tính đại diện cho bảng GC_HDGC_SANPHAM.
		/// </summary>
		public DBTable<GC_HDGC_SANPHAM> GC_HDGC_SANPHAMs
		{
			get
			{
				return GetTable<GC_HDGC_SANPHAM>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng THONGDIEP.
		/// </summary>
		public DBTable<THONGDIEP> THONGDIEPs
		{
			get
			{
				return GetTable<THONGDIEP>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SPTVT.
		/// </summary>
		public DBTable<SPTVT> SPTVTs
		{
			get
			{
				return GetTable<SPTVT>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SPTTT.
		/// </summary>
		public DBTable<SPTTT> SPTTTs
		{
			get
			{
				return GetTable<SPTTT>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SNUOC.
		/// </summary>
		public DBTable<SNUOC> SNUOCs
		{
			get
			{
				return GetTable<SNUOC>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SNGHTE.
		/// </summary>
		public DBTable<SNGHTE> SNGHTEs
		{
			get
			{
				return GetTable<SNGHTE>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SMACDINH.
		/// </summary>
		public DBTable<SMACDINH> SMACDINHs
		{
			get
			{
				return GetTable<SMACDINH>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SLOAIKIEN.
		/// </summary>
		public DBTable<SLOAIKIEN> SLOAIKIENs
		{
			get
			{
				return GetTable<SLOAIKIEN>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SLOAIHINH.
		/// </summary>
		public DBTable<SLOAIHINH> SLOAIHINHs
		{
			get
			{
				return GetTable<SLOAIHINH>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SLOAIDANHMUC.
		/// </summary>
		public DBTable<SLOAIDANHMUC> SLOAIDANHMUCs
		{
			get
			{
				return GetTable<SLOAIDANHMUC>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SHANGVANTAI.
		/// </summary>
		public DBTable<SHANGVANTAI> SHANGVANTAIs
		{
			get
			{
				return GetTable<SHANGVANTAI>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SHAIQUAN.
		/// </summary>
		public DBTable<SHAIQUAN> SHAIQUANs
		{
			get
			{
				return GetTable<SHAIQUAN>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SDVT.
		/// </summary>
		public DBTable<SDVT> SDVTs
		{
			get
			{
				return GetTable<SDVT>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SDONVI.
		/// </summary>
		public DBTable<SDONVI> SDONVIs
		{
			get
			{
				return GetTable<SDONVI>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SDOITAC.
		/// </summary>
		public DBTable<SDOITAC> SDOITACs
		{
			get
			{
				return GetTable<SDOITAC>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SDKGH.
		/// </summary>
		public DBTable<SDKGH> SDKGHs
		{
			get
			{
				return GetTable<SDKGH>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SDDLK.
		/// </summary>
		public DBTable<SDDLK> SDDLKs
		{
			get
			{
				return GetTable<SDDLK>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SDANHMUCCACLOAI.
		/// </summary>
		public DBTable<SDANHMUCCACLOAI> SDANHMUCCACLOAIs
		{
			get
			{
				return GetTable<SDANHMUCCACLOAI>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SCUCHQ.
		/// </summary>
		public DBTable<SCUCHQ> SCUCHQs
		{
			get
			{
				return GetTable<SCUCHQ>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SCUAKHAUNN.
		/// </summary>
		public DBTable<SCUAKHAUNN> SCUAKHAUNNs
		{
			get
			{
				return GetTable<SCUAKHAUNN>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SCUAKHAU.
		/// </summary>
		public DBTable<SCUAKHAU> SCUAKHAUs
		{
			get
			{
				return GetTable<SCUAKHAU>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SBOPHANHAIQUANVNACCS.
		/// </summary>
		public DBTable<SBOPHANHAIQUANVNACCS> SBOPHANHAIQUANVNACCSs
		{
			get
			{
				return GetTable<SBOPHANHAIQUANVNACCS>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng NACCS_USER.
		/// </summary>
		public DBTable<NACCS_USER> NACCS_USERs
		{
			get
			{
				return GetTable<NACCS_USER>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng MIC.
		/// </summary>
		public DBTable<MIC> MICs
		{
			get
			{
				return GetTable<MIC>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng MEC.
		/// </summary>
		public DBTable<MEC> MECs
		{
			get
			{
				return GetTable<MEC>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng KD_DANHMUCHANG.
		/// </summary>
		public DBTable<KD_DANHMUCHANG> KD_DANHMUCHANGs
		{
			get
			{
				return GetTable<KD_DANHMUCHANG>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng GC_HDGC_PK_NOIDUNG_DC.
		/// </summary>
		public DBTable<GC_HDGC_PK_NOIDUNG_DC> GC_HDGC_PK_NOIDUNG_DCs
		{
			get
			{
				return GetTable<GC_HDGC_PK_NOIDUNG_DC>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng GC_HDGC_PK_NOIDUNG.
		/// </summary>
		public DBTable<GC_HDGC_PK_NOIDUNG> GC_HDGC_PK_NOIDUNGs
		{
			get
			{
				return GetTable<GC_HDGC_PK_NOIDUNG>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng GC_HDGC_PK.
		/// </summary>
		public DBTable<GC_HDGC_PK> GC_HDGC_PKs
		{
			get
			{
				return GetTable<GC_HDGC_PK>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng GC_HDGC_NGUYENLIEU.
		/// </summary>
		public DBTable<GC_HDGC_NGUYENLIEU> GC_HDGC_NGUYENLIEUs
		{
			get
			{
				return GetTable<GC_HDGC_NGUYENLIEU>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng GC_HDGC_HANGMAU.
		/// </summary>
		public DBTable<GC_HDGC_HANGMAU> GC_HDGC_HANGMAUs
		{
			get
			{
				return GetTable<GC_HDGC_HANGMAU>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng GC_HDGC_DINHMUC.
		/// </summary>
		public DBTable<GC_HDGC_DINHMUC> GC_HDGC_DINHMUCs
		{
			get
			{
				return GetTable<GC_HDGC_DINHMUC>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng GC_HDGC.
		/// </summary>
		public DBTable<GC_HDGC> GC_HDGCs
		{
			get
			{
				return GetTable<GC_HDGC>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng GC_HDGC_THIETBI.
		/// </summary>
		public DBTable<GC_HDGC_THIETBI> GC_HDGC_THIETBIs
		{
			get
			{
				return GetTable<GC_HDGC_THIETBI>();
			}
		}
		/// <summary>
		/// Cập nhật tất các thay đổi vào DB với transaction.
		/// </summary>
		/// <returns>Số dòng thay đổi</returns>
		public int SubmitAllChange()
		{
			int intReturn = 0;
			if (m_Connection == null) m_Connection = clsConn.getConnection(m_ConnectionString, this.DataManagement);
			if (m_Connection.State != ConnectionState.Open)
				m_Connection.Open();
			IDbTransaction trans = m_Connection.BeginTransaction();
			try
			{
				foreach (object table in l_Tables)
				{
					IDBTable tbl = table as IDBTable;
					if (tbl!=null)
						intReturn += tbl.SubmitAll(trans);
				}
				trans.Commit();
			}
			catch (Exception ex)
			{
				trans.Rollback();
				throw ex;
			}
			finally
			{
				m_Tables.Clear();
				l_Tables.Clear();
				m_Connection.Close();
			}
			return intReturn;
		}
	}
}