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
	/// Generated Class for Table : NACCS_USER.
	/// </summary>
	public partial class NACCS_USER : TableBase
	{
		public NACCS_USER() : base(){}

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
		private string m_N_USERNAME;
		/// <summary>
		/// N_USERNAME.
		/// </summary>
		public string N_USERNAME
		{
			get
			{
				return m_N_USERNAME;
			}
			set
			{
				if ((this.m_N_USERNAME != value))
				{
					this.SendPropertyChanging("N_USERNAME");
					this.m_N_USERNAME = value;
					this.SendPropertyChanged("N_USERNAME");
				}
			}
		}

		private string m_N_PASSWORD;
		private bool m_N_PASSWORDUpdated = false;
		/// <summary>
		/// N_PASSWORD.
		/// </summary>
		public string N_PASSWORD
		{
			get
			{
				return m_N_PASSWORD;
			}
			set
			{
				if ((this.m_N_PASSWORD != value))
				{
					this.SendPropertyChanging("N_PASSWORD");
					this.m_N_PASSWORD = value;
					this.SendPropertyChanged("N_PASSWORD");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_N_PASSWORDUpdated = true;
				}
			}
		}

		private bool m_N_LOCKED = false;
		private bool m_N_LOCKEDUpdated = false;
		/// <summary>
		/// N_LOCKED.
		/// </summary>
		public bool N_LOCKED
		{
			get
			{
				return m_N_LOCKED;
			}
			set
			{
				if ((this.m_N_LOCKED != value))
				{
					this.SendPropertyChanging("N_LOCKED");
					this.m_N_LOCKED = value;
					this.SendPropertyChanged("N_LOCKED");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_N_LOCKEDUpdated = true;
				}
			}
		}

		private DateTime m_N_REGISTER_DATE;
		private bool m_N_REGISTER_DATEUpdated = false;
		/// <summary>
		/// N_REGISTER_DATE.
		/// </summary>
		public DateTime N_REGISTER_DATE
		{
			get
			{
				return m_N_REGISTER_DATE;
			}
			set
			{
				if ((this.m_N_REGISTER_DATE != value))
				{
					this.SendPropertyChanging("N_REGISTER_DATE");
					this.m_N_REGISTER_DATE = value;
					this.SendPropertyChanged("N_REGISTER_DATE");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_N_REGISTER_DATEUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM NACCS_USER " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[N_USERNAME]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[N_PASSWORD]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[N_LOCKED]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[N_REGISTER_DATE]", ProType.DATETIME, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("N_USERNAME", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("N_PASSWORD", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("N_LOCKED", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("N_REGISTER_DATE", ProType.DATETIME, this.DataManagement));
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
				sbSQL.Append("INSERT INTO NACCS_USER ([N_USERNAME], [N_PASSWORD], [N_LOCKED], [N_REGISTER_DATE]) VALUES(").Append("@N_USERNAME").Append(",").Append("@N_PASSWORD").Append(",").Append("@N_LOCKED").Append(",").Append("@N_REGISTER_DATE").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO NACCS_USER (N_USERNAME, N_PASSWORD, N_LOCKED, N_REGISTER_DATE) VALUES(").Append(":N_USERNAME").Append(",").Append(":N_PASSWORD").Append(",").Append(":N_LOCKED").Append(",").Append(":N_REGISTER_DATE").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE NACCS_USER SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_N_PASSWORDUpdated ? string.Format(",[N_PASSWORD] = {0}", "@N_PASSWORD") : string.Empty).Append(m_N_LOCKEDUpdated ? string.Format(",[N_LOCKED] = {0}", "@N_LOCKED") : string.Empty).Append(m_N_REGISTER_DATEUpdated ? string.Format(",[N_REGISTER_DATE] = {0}", "@N_REGISTER_DATE") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_N_PASSWORDUpdated ? string.Format(",N_PASSWORD = {0}", ":N_PASSWORD") : string.Empty).Append(m_N_LOCKEDUpdated ? string.Format(",N_LOCKED = {0}", ":N_LOCKED") : string.Empty).Append(m_N_REGISTER_DATEUpdated ? string.Format(",N_REGISTER_DATE = {0}", ":N_REGISTER_DATE") : string.Empty);
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
				sbSQL.AppendFormat("[N_PASSWORD] = {0}", "@N_PASSWORD").AppendFormat(",[N_LOCKED] = {0}", "@N_LOCKED").AppendFormat(",[N_REGISTER_DATE] = {0}", "@N_REGISTER_DATE");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("N_PASSWORD = {0}", ":N_PASSWORD").AppendFormat(",N_LOCKED = {0}", ":N_LOCKED").AppendFormat(",N_REGISTER_DATE = {0}", ":N_REGISTER_DATE");
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
			return clsDAL.DeleteString("NACCS_USER", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[N_USERNAME] = {0}", "@N_USERNAME");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("N_USERNAME = {0}", ":N_USERNAME");
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
			paramList.Add(clsDAL.CreateParameter("N_USERNAME", "WChar", clsDAL.ToDBParam(N_USERNAME, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("N_PASSWORD", "WChar", clsDAL.ToDBParam(N_PASSWORD, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("N_LOCKED", "Boolean", clsDAL.ToDBParam(N_LOCKED, ProType.BOOL, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("N_REGISTER_DATE", "Date", clsDAL.ToDBParam(N_REGISTER_DATE, ProType.DATETIME, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("N_USERNAME", "WChar", clsDAL.ToDBParam(N_USERNAME, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("N_USERNAME", "WChar", clsDAL.ToDBParam(N_USERNAME, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("N_PASSWORD", "WChar", clsDAL.ToDBParam(N_PASSWORD, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("N_LOCKED", "Boolean", clsDAL.ToDBParam(N_LOCKED, ProType.BOOL, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("N_REGISTER_DATE", "Date", clsDAL.ToDBParam(N_REGISTER_DATE, ProType.DATETIME, this.DataManagement), this.DataManagement));
			return paramList;
		}
	}
}