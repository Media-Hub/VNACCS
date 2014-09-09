using System;
using System.Data;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
namespace DevComponents.DotNetBar.thaison
{
	/// <summary>
	/// DB context sử dụng trong using.
	/// </summary>
	public partial class ecus5DBContext : IDisposable
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
		public ecus5DBContext(IDbConnection conn)
		{
			m_Connection = conn;
			m_ConnectionString = conn.ConnectionString;
			Initialization();
		}

		/// <summary>
		/// Hàm khởi tạo.
		/// </summary>
		/// <param name="conString">ConnectionString đến DB</param>
		public ecus5DBContext(string conString)
			: this(conString, clsDAL.defaultDBMan)
		{
		}

		/// <summary>
		/// Hàm khởi tạo.
		/// </summary>
		/// <param name="conString">ConnectionString đến DB</param>
		public ecus5DBContext(string conString, DBManagement DBMan)
			: this(clsConn.getConnection(conString, DBMan))
		{
			_dataManagement = DBMan;
		}

		/// <summary>
		/// Hàm khởi tạo. với connection mặc định
		/// </summary>
		public ecus5DBContext()
			: this(clsDAL.defaultDBMan)
		{
		}

		/// <summary>
		/// Hàm khởi tạo. với connection mặc định
		/// </summary>
		public ecus5DBContext(DBManagement DBMan)
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
		/// Thuộc tính đại diện cho bảng SBIEU_THUE.
		/// </summary>
		public DBTable<SBIEU_THUE> SBIEU_THUEs
		{
			get
			{
				return GetTable<SBIEU_THUE>();
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
		/// Thuộc tính đại diện cho bảng SDIA_DIEM.
		/// </summary>
		public DBTable<SDIA_DIEM> SDIA_DIEMs
		{
			get
			{
				return GetTable<SDIA_DIEM>();
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
		/// Thuộc tính đại diện cho bảng SHAIQUANIP.
		/// </summary>
		public DBTable<SHAIQUANIP> SHAIQUANIPs
		{
			get
			{
				return GetTable<SHAIQUANIP>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SHAIQUAN_SUB.
		/// </summary>
		public DBTable<SHAIQUAN_SUB> SHAIQUAN_SUBs
		{
			get
			{
				return GetTable<SHAIQUAN_SUB>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SHAIQUANIPV4.
		/// </summary>
		public DBTable<SHAIQUANIPV4> SHAIQUANIPV4s
		{
			get
			{
				return GetTable<SHAIQUANIPV4>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SHANGVT.
		/// </summary>
		public DBTable<SHANGVT> SHANGVTs
		{
			get
			{
				return GetTable<SHANGVT>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SLOAI_MA.
		/// </summary>
		public DBTable<SLOAI_MA> SLOAI_MAs
		{
			get
			{
				return GetTable<SLOAI_MA>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SLOAI_KIEN.
		/// </summary>
		public DBTable<SLOAI_KIEN> SLOAI_KIENs
		{
			get
			{
				return GetTable<SLOAI_KIEN>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SLOAI_GP.
		/// </summary>
		public DBTable<SLOAI_GP> SLOAI_GPs
		{
			get
			{
				return GetTable<SLOAI_GP>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SLOAI_CT.
		/// </summary>
		public DBTable<SLOAI_CT> SLOAI_CTs
		{
			get
			{
				return GetTable<SLOAI_CT>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SLOAI_CO.
		/// </summary>
		public DBTable<SLOAI_CO> SLOAI_COs
		{
			get
			{
				return GetTable<SLOAI_CO>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SMA_MIENTHUE.
		/// </summary>
		public DBTable<SMA_MIENTHUE> SMA_MIENTHUEs
		{
			get
			{
				return GetTable<SMA_MIENTHUE>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SMACACLOAI.
		/// </summary>
		public DBTable<SMACACLOAI> SMACACLOAIs
		{
			get
			{
				return GetTable<SMACACLOAI>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SNGTE.
		/// </summary>
		public DBTable<SNGTE> SNGTEs
		{
			get
			{
				return GetTable<SNGTE>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SNGAN_HANG.
		/// </summary>
		public DBTable<SNGAN_HANG> SNGAN_HANGs
		{
			get
			{
				return GetTable<SNGAN_HANG>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SLHINHMD.
		/// </summary>
		public DBTable<SLHINHMD> SLHINHMDs
		{
			get
			{
				return GetTable<SLHINHMD>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SLHINHMD_CONF.
		/// </summary>
		public DBTable<SLHINHMD_CONF> SLHINHMD_CONFs
		{
			get
			{
				return GetTable<SLHINHMD_CONF>();
			}
		}
		/// <summary>
		/// Thuộc tính đại diện cho bảng SNUOC1.
		/// </summary>
		public DBTable<SNUOC1> SNUOC1s
		{
			get
			{
				return GetTable<SNUOC1>();
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
		/// Thuộc tính đại diện cho bảng SPHANLOAI.
		/// </summary>
		public DBTable<SPHANLOAI> SPHANLOAIs
		{
			get
			{
				return GetTable<SPHANLOAI>();
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