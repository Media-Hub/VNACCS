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
	/// Generated Class for Table : SLOAIDANHMUC.
	/// </summary>
	public partial class SLOAIDANHMUC : TableBase
	{
		public SLOAIDANHMUC() : base(){}

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
		private string m_LDM_MALOAI;
		/// <summary>
		/// LDM_MALOAI.
		/// </summary>
		public string LDM_MALOAI
		{
			get
			{
				return m_LDM_MALOAI;
			}
			set
			{
				if ((this.m_LDM_MALOAI != value))
				{
					this.SendPropertyChanging("LDM_MALOAI");
					this.m_LDM_MALOAI = value;
					this.SendPropertyChanged("LDM_MALOAI");
				}
			}
		}

		private string m_LDM_TENLOAI;
		private bool m_LDM_TENLOAIUpdated = false;
		/// <summary>
		/// LDM_TENLOAI.
		/// </summary>
		public string LDM_TENLOAI
		{
			get
			{
				return m_LDM_TENLOAI;
			}
			set
			{
				if ((this.m_LDM_TENLOAI != value))
				{
					this.SendPropertyChanging("LDM_TENLOAI");
					this.m_LDM_TENLOAI = value;
					this.SendPropertyChanged("LDM_TENLOAI");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_LDM_TENLOAIUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SLOAIDANHMUC " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[LDM_MALOAI]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[LDM_TENLOAI]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("LDM_MALOAI", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("LDM_TENLOAI", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO SLOAIDANHMUC ([LDM_MALOAI], [LDM_TENLOAI]) VALUES(").Append("@LDM_MALOAI").Append(",").Append("@LDM_TENLOAI").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO SLOAIDANHMUC (LDM_MALOAI, LDM_TENLOAI) VALUES(").Append(":LDM_MALOAI").Append(",").Append(":LDM_TENLOAI").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SLOAIDANHMUC SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_LDM_TENLOAIUpdated ? string.Format(",[LDM_TENLOAI] = {0}", "@LDM_TENLOAI") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_LDM_TENLOAIUpdated ? string.Format(",LDM_TENLOAI = {0}", ":LDM_TENLOAI") : string.Empty);
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
				sbSQL.AppendFormat("[LDM_TENLOAI] = {0}", "@LDM_TENLOAI");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("LDM_TENLOAI = {0}", ":LDM_TENLOAI");
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
			return clsDAL.DeleteString("SLOAIDANHMUC", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[LDM_MALOAI] = {0}", "@LDM_MALOAI");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("LDM_MALOAI = {0}", ":LDM_MALOAI");
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
			paramList.Add(clsDAL.CreateParameter("LDM_MALOAI", "WChar", clsDAL.ToDBParam(LDM_MALOAI, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("LDM_TENLOAI", "WChar", clsDAL.ToDBParam(LDM_TENLOAI, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("LDM_MALOAI", "WChar", clsDAL.ToDBParam(LDM_MALOAI, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("LDM_MALOAI", "WChar", clsDAL.ToDBParam(LDM_MALOAI, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("LDM_TENLOAI", "WChar", clsDAL.ToDBParam(LDM_TENLOAI, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}