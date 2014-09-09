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
	/// Generated Class for Table : SNUOC.
	/// </summary>
	public partial class SNUOC : TableBase
	{
		public SNUOC() : base(){}

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
		private string m_NC_MANUOC;
		/// <summary>
		/// NC_MANUOC.
		/// </summary>
		public string NC_MANUOC
		{
			get
			{
				return m_NC_MANUOC;
			}
			set
			{
				if ((this.m_NC_MANUOC != value))
				{
					this.SendPropertyChanging("NC_MANUOC");
					this.m_NC_MANUOC = value;
					this.SendPropertyChanged("NC_MANUOC");
				}
			}
		}

		private string m_NC_TENNUOC;
		private bool m_NC_TENNUOCUpdated = false;
		/// <summary>
		/// NC_TENNUOC.
		/// </summary>
		public string NC_TENNUOC
		{
			get
			{
				return m_NC_TENNUOC;
			}
			set
			{
				if ((this.m_NC_TENNUOC != value))
				{
					this.SendPropertyChanging("NC_TENNUOC");
					this.m_NC_TENNUOC = value;
					this.SendPropertyChanged("NC_TENNUOC");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NC_TENNUOCUpdated = true;
				}
			}
		}

		private string m_NC_GHICHU;
		private bool m_NC_GHICHUUpdated = false;
		/// <summary>
		/// NC_GHICHU.
		/// </summary>
		public string NC_GHICHU
		{
			get
			{
				return m_NC_GHICHU;
			}
			set
			{
				if ((this.m_NC_GHICHU != value))
				{
					this.SendPropertyChanging("NC_GHICHU");
					this.m_NC_GHICHU = value;
					this.SendPropertyChanged("NC_GHICHU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NC_GHICHUUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SNUOC " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[NC_MANUOC]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[NC_TENNUOC]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[NC_GHICHU]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("NC_MANUOC", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NC_TENNUOC", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NC_GHICHU", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO SNUOC ([NC_MANUOC], [NC_TENNUOC], [NC_GHICHU]) VALUES(").Append("@NC_MANUOC").Append(",").Append("@NC_TENNUOC").Append(",").Append("@NC_GHICHU").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO SNUOC (NC_MANUOC, NC_TENNUOC, NC_GHICHU) VALUES(").Append(":NC_MANUOC").Append(",").Append(":NC_TENNUOC").Append(",").Append(":NC_GHICHU").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SNUOC SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_NC_TENNUOCUpdated ? string.Format(",[NC_TENNUOC] = {0}", "@NC_TENNUOC") : string.Empty).Append(m_NC_GHICHUUpdated ? string.Format(",[NC_GHICHU] = {0}", "@NC_GHICHU") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_NC_TENNUOCUpdated ? string.Format(",NC_TENNUOC = {0}", ":NC_TENNUOC") : string.Empty).Append(m_NC_GHICHUUpdated ? string.Format(",NC_GHICHU = {0}", ":NC_GHICHU") : string.Empty);
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
				sbSQL.AppendFormat("[NC_TENNUOC] = {0}", "@NC_TENNUOC").AppendFormat(",[NC_GHICHU] = {0}", "@NC_GHICHU");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("NC_TENNUOC = {0}", ":NC_TENNUOC").AppendFormat(",NC_GHICHU = {0}", ":NC_GHICHU");
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
			return clsDAL.DeleteString("SNUOC", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[NC_MANUOC] = {0}", "@NC_MANUOC");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("NC_MANUOC = {0}", ":NC_MANUOC");
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
			paramList.Add(clsDAL.CreateParameter("NC_MANUOC", "WChar", clsDAL.ToDBParam(NC_MANUOC, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("NC_TENNUOC", "WChar", clsDAL.ToDBParam(NC_TENNUOC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NC_GHICHU", "WChar", clsDAL.ToDBParam(NC_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NC_MANUOC", "WChar", clsDAL.ToDBParam(NC_MANUOC, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("NC_MANUOC", "WChar", clsDAL.ToDBParam(NC_MANUOC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NC_TENNUOC", "WChar", clsDAL.ToDBParam(NC_TENNUOC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NC_GHICHU", "WChar", clsDAL.ToDBParam(NC_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}