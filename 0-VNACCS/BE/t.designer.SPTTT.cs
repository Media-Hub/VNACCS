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
	/// Generated Class for Table : SPTTT.
	/// </summary>
	public partial class SPTTT : TableBase
	{
		public SPTTT() : base(){}

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
		private string m_PTTT_MA;
		/// <summary>
		/// PTTT_MA.
		/// </summary>
		public string PTTT_MA
		{
			get
			{
				return m_PTTT_MA;
			}
			set
			{
				if ((this.m_PTTT_MA != value))
				{
					this.SendPropertyChanging("PTTT_MA");
					this.m_PTTT_MA = value;
					this.SendPropertyChanged("PTTT_MA");
				}
			}
		}

		private string m_PTTT_TEN;
		private bool m_PTTT_TENUpdated = false;
		/// <summary>
		/// PTTT_TEN.
		/// </summary>
		public string PTTT_TEN
		{
			get
			{
				return m_PTTT_TEN;
			}
			set
			{
				if ((this.m_PTTT_TEN != value))
				{
					this.SendPropertyChanging("PTTT_TEN");
					this.m_PTTT_TEN = value;
					this.SendPropertyChanged("PTTT_TEN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_PTTT_TENUpdated = true;
				}
			}
		}

		private string m_PTTT_GHICHU;
		private bool m_PTTT_GHICHUUpdated = false;
		/// <summary>
		/// PTTT_GHICHU.
		/// </summary>
		public string PTTT_GHICHU
		{
			get
			{
				return m_PTTT_GHICHU;
			}
			set
			{
				if ((this.m_PTTT_GHICHU != value))
				{
					this.SendPropertyChanging("PTTT_GHICHU");
					this.m_PTTT_GHICHU = value;
					this.SendPropertyChanged("PTTT_GHICHU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_PTTT_GHICHUUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SPTTT " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[PTTT_MA]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[PTTT_TEN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[PTTT_GHICHU]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("PTTT_MA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("PTTT_TEN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("PTTT_GHICHU", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO SPTTT ([PTTT_MA], [PTTT_TEN], [PTTT_GHICHU]) VALUES(").Append("@PTTT_MA").Append(",").Append("@PTTT_TEN").Append(",").Append("@PTTT_GHICHU").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO SPTTT (PTTT_MA, PTTT_TEN, PTTT_GHICHU) VALUES(").Append(":PTTT_MA").Append(",").Append(":PTTT_TEN").Append(",").Append(":PTTT_GHICHU").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SPTTT SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_PTTT_TENUpdated ? string.Format(",[PTTT_TEN] = {0}", "@PTTT_TEN") : string.Empty).Append(m_PTTT_GHICHUUpdated ? string.Format(",[PTTT_GHICHU] = {0}", "@PTTT_GHICHU") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_PTTT_TENUpdated ? string.Format(",PTTT_TEN = {0}", ":PTTT_TEN") : string.Empty).Append(m_PTTT_GHICHUUpdated ? string.Format(",PTTT_GHICHU = {0}", ":PTTT_GHICHU") : string.Empty);
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
				sbSQL.AppendFormat("[PTTT_TEN] = {0}", "@PTTT_TEN").AppendFormat(",[PTTT_GHICHU] = {0}", "@PTTT_GHICHU");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("PTTT_TEN = {0}", ":PTTT_TEN").AppendFormat(",PTTT_GHICHU = {0}", ":PTTT_GHICHU");
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
			return clsDAL.DeleteString("SPTTT", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[PTTT_MA] = {0}", "@PTTT_MA");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("PTTT_MA = {0}", ":PTTT_MA");
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
			paramList.Add(clsDAL.CreateParameter("PTTT_MA", "WChar", clsDAL.ToDBParam(PTTT_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("PTTT_TEN", "WChar", clsDAL.ToDBParam(PTTT_TEN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PTTT_GHICHU", "WChar", clsDAL.ToDBParam(PTTT_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PTTT_MA", "WChar", clsDAL.ToDBParam(PTTT_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("PTTT_MA", "WChar", clsDAL.ToDBParam(PTTT_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PTTT_TEN", "WChar", clsDAL.ToDBParam(PTTT_TEN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PTTT_GHICHU", "WChar", clsDAL.ToDBParam(PTTT_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}