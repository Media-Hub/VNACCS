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
	/// Generated Class for Table : SPTVT.
	/// </summary>
	public partial class SPTVT : TableBase
	{
		public SPTVT() : base(){}

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
		private string m_PTVT_MAPTVT;
		/// <summary>
		/// PTVT_MAPTVT.
		/// </summary>
		public string PTVT_MAPTVT
		{
			get
			{
				return m_PTVT_MAPTVT;
			}
			set
			{
				if ((this.m_PTVT_MAPTVT != value))
				{
					this.SendPropertyChanging("PTVT_MAPTVT");
					this.m_PTVT_MAPTVT = value;
					this.SendPropertyChanged("PTVT_MAPTVT");
				}
			}
		}

		private string m_PTVT_TENPTVT;
		private bool m_PTVT_TENPTVTUpdated = false;
		/// <summary>
		/// PTVT_TENPTVT.
		/// </summary>
		public string PTVT_TENPTVT
		{
			get
			{
				return m_PTVT_TENPTVT;
			}
			set
			{
				if ((this.m_PTVT_TENPTVT != value))
				{
					this.SendPropertyChanging("PTVT_TENPTVT");
					this.m_PTVT_TENPTVT = value;
					this.SendPropertyChanged("PTVT_TENPTVT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_PTVT_TENPTVTUpdated = true;
				}
			}
		}

		private string m_PTVT_GHICHU;
		private bool m_PTVT_GHICHUUpdated = false;
		/// <summary>
		/// PTVT_GHICHU.
		/// </summary>
		public string PTVT_GHICHU
		{
			get
			{
				return m_PTVT_GHICHU;
			}
			set
			{
				if ((this.m_PTVT_GHICHU != value))
				{
					this.SendPropertyChanging("PTVT_GHICHU");
					this.m_PTVT_GHICHU = value;
					this.SendPropertyChanged("PTVT_GHICHU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_PTVT_GHICHUUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SPTVT " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[PTVT_MAPTVT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[PTVT_TENPTVT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[PTVT_GHICHU]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("PTVT_MAPTVT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("PTVT_TENPTVT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("PTVT_GHICHU", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO SPTVT ([PTVT_MAPTVT], [PTVT_TENPTVT], [PTVT_GHICHU]) VALUES(").Append("@PTVT_MAPTVT").Append(",").Append("@PTVT_TENPTVT").Append(",").Append("@PTVT_GHICHU").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO SPTVT (PTVT_MAPTVT, PTVT_TENPTVT, PTVT_GHICHU) VALUES(").Append(":PTVT_MAPTVT").Append(",").Append(":PTVT_TENPTVT").Append(",").Append(":PTVT_GHICHU").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SPTVT SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_PTVT_TENPTVTUpdated ? string.Format(",[PTVT_TENPTVT] = {0}", "@PTVT_TENPTVT") : string.Empty).Append(m_PTVT_GHICHUUpdated ? string.Format(",[PTVT_GHICHU] = {0}", "@PTVT_GHICHU") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_PTVT_TENPTVTUpdated ? string.Format(",PTVT_TENPTVT = {0}", ":PTVT_TENPTVT") : string.Empty).Append(m_PTVT_GHICHUUpdated ? string.Format(",PTVT_GHICHU = {0}", ":PTVT_GHICHU") : string.Empty);
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
				sbSQL.AppendFormat("[PTVT_TENPTVT] = {0}", "@PTVT_TENPTVT").AppendFormat(",[PTVT_GHICHU] = {0}", "@PTVT_GHICHU");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("PTVT_TENPTVT = {0}", ":PTVT_TENPTVT").AppendFormat(",PTVT_GHICHU = {0}", ":PTVT_GHICHU");
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
			return clsDAL.DeleteString("SPTVT", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[PTVT_MAPTVT] = {0}", "@PTVT_MAPTVT");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("PTVT_MAPTVT = {0}", ":PTVT_MAPTVT");
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
			paramList.Add(clsDAL.CreateParameter("PTVT_MAPTVT", "WChar", clsDAL.ToDBParam(PTVT_MAPTVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("PTVT_TENPTVT", "WChar", clsDAL.ToDBParam(PTVT_TENPTVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PTVT_GHICHU", "WChar", clsDAL.ToDBParam(PTVT_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PTVT_MAPTVT", "WChar", clsDAL.ToDBParam(PTVT_MAPTVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("PTVT_MAPTVT", "WChar", clsDAL.ToDBParam(PTVT_MAPTVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PTVT_TENPTVT", "WChar", clsDAL.ToDBParam(PTVT_TENPTVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PTVT_GHICHU", "WChar", clsDAL.ToDBParam(PTVT_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}