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
	/// Generated Class for Table : SMACDINH.
	/// </summary>
	public partial class SMACDINH : TableBase
	{
		public SMACDINH() : base(){}

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
		private string m_MD_USERNAME;
		/// <summary>
		/// MD_USERNAME.
		/// </summary>
		public string MD_USERNAME
		{
			get
			{
				return m_MD_USERNAME;
			}
			set
			{
				if ((this.m_MD_USERNAME != value))
				{
					this.SendPropertyChanging("MD_USERNAME");
					this.m_MD_USERNAME = value;
					this.SendPropertyChanged("MD_USERNAME");
				}
			}
		}

		private string m_MD_GIATRI_MA;
		/// <summary>
		/// MD_GIATRI_MA.
		/// </summary>
		public string MD_GIATRI_MA
		{
			get
			{
				return m_MD_GIATRI_MA;
			}
			set
			{
				if ((this.m_MD_GIATRI_MA != value))
				{
					this.SendPropertyChanging("MD_GIATRI_MA");
					this.m_MD_GIATRI_MA = value;
					this.SendPropertyChanged("MD_GIATRI_MA");
				}
			}
		}

		private string m_MD_GIATRI;
		private bool m_MD_GIATRIUpdated = false;
		/// <summary>
		/// MD_GIATRI.
		/// </summary>
		public string MD_GIATRI
		{
			get
			{
				return m_MD_GIATRI;
			}
			set
			{
				if ((this.m_MD_GIATRI != value))
				{
					this.SendPropertyChanging("MD_GIATRI");
					this.m_MD_GIATRI = value;
					this.SendPropertyChanged("MD_GIATRI");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_MD_GIATRIUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SMACDINH " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[MD_USERNAME]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[MD_GIATRI_MA]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[MD_GIATRI]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("MD_USERNAME", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("MD_GIATRI_MA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("MD_GIATRI", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO SMACDINH ([MD_USERNAME], [MD_GIATRI_MA], [MD_GIATRI]) VALUES(").Append("@MD_USERNAME").Append(",").Append("@MD_GIATRI_MA").Append(",").Append("@MD_GIATRI").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO SMACDINH (MD_USERNAME, MD_GIATRI_MA, MD_GIATRI) VALUES(").Append(":MD_USERNAME").Append(",").Append(":MD_GIATRI_MA").Append(",").Append(":MD_GIATRI").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SMACDINH SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_MD_GIATRIUpdated ? string.Format(",[MD_GIATRI] = {0}", "@MD_GIATRI") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_MD_GIATRIUpdated ? string.Format(",MD_GIATRI = {0}", ":MD_GIATRI") : string.Empty);
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
				sbSQL.AppendFormat("[MD_GIATRI] = {0}", "@MD_GIATRI");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("MD_GIATRI = {0}", ":MD_GIATRI");
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
			return clsDAL.DeleteString("SMACDINH", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[MD_USERNAME] = {0}", "@MD_USERNAME").AppendFormat(" AND [MD_GIATRI_MA] = {0}", "@MD_GIATRI_MA");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("MD_USERNAME = {0}", ":MD_USERNAME").AppendFormat(" AND MD_GIATRI_MA = {0}", ":MD_GIATRI_MA");
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
			paramList.Add(clsDAL.CreateParameter("MD_USERNAME", "WChar", clsDAL.ToDBParam(MD_USERNAME, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("MD_GIATRI_MA", "WChar", clsDAL.ToDBParam(MD_GIATRI_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("MD_GIATRI", "WChar", clsDAL.ToDBParam(MD_GIATRI, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("MD_USERNAME", "WChar", clsDAL.ToDBParam(MD_USERNAME, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("MD_GIATRI_MA", "WChar", clsDAL.ToDBParam(MD_GIATRI_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("MD_USERNAME", "WChar", clsDAL.ToDBParam(MD_USERNAME, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("MD_GIATRI_MA", "WChar", clsDAL.ToDBParam(MD_GIATRI_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("MD_GIATRI", "WChar", clsDAL.ToDBParam(MD_GIATRI, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}