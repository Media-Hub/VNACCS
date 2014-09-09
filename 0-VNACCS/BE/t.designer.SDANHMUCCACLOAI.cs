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
	/// Generated Class for Table : SDANHMUCCACLOAI.
	/// </summary>
	public partial class SDANHMUCCACLOAI : TableBase
	{
		public SDANHMUCCACLOAI() : base(){}

		/// <summary>
		/// Là View hay là Table.
		/// </summary>
		public override bool IsView 
		{
			get
			{
				return false;
			}
		}
		private string m_DMCL_MALOAIDM;
		/// <summary>
		/// DMCL_MALOAIDM.
		/// </summary>
		public string DMCL_MALOAIDM
		{
			get
			{
				return m_DMCL_MALOAIDM;
			}
			set
			{
				if ((this.m_DMCL_MALOAIDM != value))
				{
					this.SendPropertyChanging("DMCL_MALOAIDM");
					this.m_DMCL_MALOAIDM = value;
					this.SendPropertyChanged("DMCL_MALOAIDM");
				}
			}
		}

		private string m_DMCL_MADM;
		/// <summary>
		/// DMCL_MADM.
		/// </summary>
		public string DMCL_MADM
		{
			get
			{
				return m_DMCL_MADM;
			}
			set
			{
				if ((this.m_DMCL_MADM != value))
				{
					this.SendPropertyChanging("DMCL_MADM");
					this.m_DMCL_MADM = value;
					this.SendPropertyChanged("DMCL_MADM");
				}
			}
		}

		private string m_DMCL_TENDM;
		private bool m_DMCL_TENDMUpdated = false;
		/// <summary>
		/// DMCL_TENDM.
		/// </summary>
		public string DMCL_TENDM
		{
			get
			{
				return m_DMCL_TENDM;
			}
			set
			{
				if ((this.m_DMCL_TENDM != value))
				{
					this.SendPropertyChanging("DMCL_TENDM");
					this.m_DMCL_TENDM = value;
					this.SendPropertyChanged("DMCL_TENDM");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DMCL_TENDMUpdated = true;
				}
			}
		}

		private string m_DMCL_TENBANG;
		private bool m_DMCL_TENBANGUpdated = false;
		/// <summary>
		/// DMCL_TENBANG.
		/// </summary>
		public string DMCL_TENBANG
		{
			get
			{
				return m_DMCL_TENBANG;
			}
			set
			{
				if ((this.m_DMCL_TENBANG != value))
				{
					this.SendPropertyChanging("DMCL_TENBANG");
					this.m_DMCL_TENBANG = value;
					this.SendPropertyChanged("DMCL_TENBANG");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DMCL_TENBANGUpdated = true;
				}
			}
		}

		private string m_DMCL_GHICHU;
		private bool m_DMCL_GHICHUUpdated = false;
		/// <summary>
		/// DMCL_GHICHU.
		/// </summary>
		public string DMCL_GHICHU
		{
			get
			{
				return m_DMCL_GHICHU;
			}
			set
			{
				if ((this.m_DMCL_GHICHU != value))
				{
					this.SendPropertyChanging("DMCL_GHICHU");
					this.m_DMCL_GHICHU = value;
					this.SendPropertyChanged("DMCL_GHICHU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DMCL_GHICHUUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SDANHMUCCACLOAI " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			switch(this.DataManagement)
			{
				case DBManagement.Access:
				case DBManagement.SQL:
				case DBManagement.SQLLite:
				default:
				sbSQL.Append(clsDAL.SelectField("[DMCL_MALOAIDM]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DMCL_MADM]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DMCL_TENDM]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DMCL_TENBANG]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DMCL_GHICHU]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("DMCL_MALOAIDM", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DMCL_MADM", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DMCL_TENDM", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DMCL_TENBANG", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DMCL_GHICHU", ProType.OTHER, this.DataManagement));
				break;
			}
			return SelectStatement(sbSQL.ToString(), WhereClause, OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause)
		{
			return SelectStatement(WhereClause, string.Empty);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu theo khóa chính.
		/// </summary>
		public override string SelectStatement()
		{
			return SelectStatement(WhereById());
		}

		/// <summary>
		/// Tạo câu SQL thêm dữ liệu.
		/// </summary>
		public override string InsertStatement()
		{
			StringBuilder sbSQL = new StringBuilder();
			switch(this.DataManagement)
			{
				case DBManagement.Access:
				case DBManagement.SQL:
				case DBManagement.SQLLite:
				default:
				sbSQL.Append("INSERT INTO SDANHMUCCACLOAI ([DMCL_MALOAIDM], [DMCL_MADM], [DMCL_TENDM], [DMCL_TENBANG], [DMCL_GHICHU]) VALUES(").Append("@DMCL_MALOAIDM").Append(",").Append("@DMCL_MADM").Append(",").Append("@DMCL_TENDM").Append(",").Append("@DMCL_TENBANG").Append(",").Append("@DMCL_GHICHU").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO SDANHMUCCACLOAI (DMCL_MALOAIDM, DMCL_MADM, DMCL_TENDM, DMCL_TENBANG, DMCL_GHICHU) VALUES(").Append(":DMCL_MALOAIDM").Append(",").Append(":DMCL_MADM").Append(",").Append(":DMCL_TENDM").Append(",").Append(":DMCL_TENBANG").Append(",").Append(":DMCL_GHICHU").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SDANHMUCCACLOAI SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			switch(this.DataManagement)
			{
				case DBManagement.Access:
					return UpdateFullStatement(WhereClause);
				case DBManagement.SQL:
				case DBManagement.SQLLite:
				default:
				sbSQL.Append(m_DMCL_TENDMUpdated ? string.Format(",[DMCL_TENDM] = {0}", "@DMCL_TENDM") : string.Empty).Append(m_DMCL_TENBANGUpdated ? string.Format(",[DMCL_TENBANG] = {0}", "@DMCL_TENBANG") : string.Empty).Append(m_DMCL_GHICHUUpdated ? string.Format(",[DMCL_GHICHU] = {0}", "@DMCL_GHICHU") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_DMCL_TENDMUpdated ? string.Format(",DMCL_TENDM = {0}", ":DMCL_TENDM") : string.Empty).Append(m_DMCL_TENBANGUpdated ? string.Format(",DMCL_TENBANG = {0}", ":DMCL_TENBANG") : string.Empty).Append(m_DMCL_GHICHUUpdated ? string.Format(",DMCL_GHICHU = {0}", ":DMCL_GHICHU") : string.Empty);
				break;
			}
			if(sbSQL.Length > 0)
				return UpdateStatement(sbSQL.ToString().Substring(1), WhereClause);
			else
				return UpdateFullStatement(WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateFullStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			switch(this.DataManagement)
			{
				case DBManagement.Access:
				case DBManagement.SQL:
				case DBManagement.SQLLite:
				default:
				sbSQL.AppendFormat("[DMCL_TENDM] = {0}", "@DMCL_TENDM").AppendFormat(",[DMCL_TENBANG] = {0}", "@DMCL_TENBANG").AppendFormat(",[DMCL_GHICHU] = {0}", "@DMCL_GHICHU");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("DMCL_TENDM = {0}", ":DMCL_TENDM").AppendFormat(",DMCL_TENBANG = {0}", ":DMCL_TENBANG").AppendFormat(",DMCL_GHICHU = {0}", ":DMCL_GHICHU");
				break;
			}
			return UpdateStatement(sbSQL.ToString(), WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật liêu theo khóa chính.
		/// </summary>
		public override string UpdateStatement()
		{
			return UpdateStatement(WhereById());
		}

		/// <summary>
		/// Tạo câu SQL xóa dữ liêu.
		/// </summary>
		public override string DeleteStatement(string WhereClause)
		{
			return clsDAL.DeleteString("SDANHMUCCACLOAI", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL xóa dữ liệu theo khóa chính.
		/// </summary>
		public override string DeleteStatement()
		{
			 return DeleteStatement(WhereById());
		}

		/// <summary>
		/// Tạo điều kiện tìm kiếm theo khóa chính.
		/// </summary>
		public override string WhereById()
		{
			StringBuilder sbSQL = new StringBuilder();
			switch(this.DataManagement)
			{
				case DBManagement.Access:
				case DBManagement.SQL:
				case DBManagement.SQLLite:
				default:
				sbSQL.AppendFormat("[DMCL_MALOAIDM] = {0}", "@DMCL_MALOAIDM").AppendFormat(" AND [DMCL_MADM] = {0}", "@DMCL_MADM");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("DMCL_MALOAIDM = {0}", ":DMCL_MALOAIDM").AppendFormat(" AND DMCL_MADM = {0}", ":DMCL_MADM");
				break;
			}
			return sbSQL.ToString();
		}

		/// <summary>
		/// Tạo parameter để Delete dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> DeleteParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("DMCL_MALOAIDM", "WChar", clsDAL.ToDBParam(DMCL_MALOAIDM, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DMCL_MADM", "WChar", clsDAL.ToDBParam(DMCL_MADM, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("DMCL_TENDM", "WChar", clsDAL.ToDBParam(DMCL_TENDM, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DMCL_TENBANG", "WChar", clsDAL.ToDBParam(DMCL_TENBANG, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DMCL_GHICHU", "WChar", clsDAL.ToDBParam(DMCL_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DMCL_MALOAIDM", "WChar", clsDAL.ToDBParam(DMCL_MALOAIDM, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DMCL_MADM", "WChar", clsDAL.ToDBParam(DMCL_MADM, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("DMCL_MALOAIDM", "WChar", clsDAL.ToDBParam(DMCL_MALOAIDM, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DMCL_MADM", "WChar", clsDAL.ToDBParam(DMCL_MADM, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DMCL_TENDM", "WChar", clsDAL.ToDBParam(DMCL_TENDM, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DMCL_TENBANG", "WChar", clsDAL.ToDBParam(DMCL_TENBANG, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DMCL_GHICHU", "WChar", clsDAL.ToDBParam(DMCL_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}