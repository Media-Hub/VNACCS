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
	/// Generated Class for Table : SCUCHQ.
	/// </summary>
	public partial class SCUCHQ : TableBase
	{
		public SCUCHQ() : base(){}

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
		private string m_MA_CUC;
		/// <summary>
		/// MA_CUC.
		/// </summary>
		public string MA_CUC
		{
			get
			{
				return m_MA_CUC;
			}
			set
			{
				if ((this.m_MA_CUC != value))
				{
					this.SendPropertyChanging("MA_CUC");
					this.m_MA_CUC = value;
					this.SendPropertyChanged("MA_CUC");
				}
			}
		}

		private string m_TEN_CUC;
		private bool m_TEN_CUCUpdated = false;
		/// <summary>
		/// TEN_CUC.
		/// </summary>
		public string TEN_CUC
		{
			get
			{
				return m_TEN_CUC;
			}
			set
			{
				if ((this.m_TEN_CUC != value))
				{
					this.SendPropertyChanging("TEN_CUC");
					this.m_TEN_CUC = value;
					this.SendPropertyChanged("TEN_CUC");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_CUCUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SCUCHQ " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[MA_CUC]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TEN_CUC]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("MA_CUC", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_CUC", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO SCUCHQ ([MA_CUC], [TEN_CUC]) VALUES(").Append("@MA_CUC").Append(",").Append("@TEN_CUC").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO SCUCHQ (MA_CUC, TEN_CUC) VALUES(").Append(":MA_CUC").Append(",").Append(":TEN_CUC").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SCUCHQ SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_TEN_CUCUpdated ? string.Format(",[TEN_CUC] = {0}", "@TEN_CUC") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_TEN_CUCUpdated ? string.Format(",TEN_CUC = {0}", ":TEN_CUC") : string.Empty);
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
				sbSQL.AppendFormat("[TEN_CUC] = {0}", "@TEN_CUC");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("TEN_CUC = {0}", ":TEN_CUC");
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
			return clsDAL.DeleteString("SCUCHQ", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[MA_CUC] = {0}", "@MA_CUC");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("MA_CUC = {0}", ":MA_CUC");
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
			paramList.Add(clsDAL.CreateParameter("MA_CUC", "WChar", clsDAL.ToDBParam(MA_CUC, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("TEN_CUC", "WChar", clsDAL.ToDBParam(TEN_CUC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("MA_CUC", "WChar", clsDAL.ToDBParam(MA_CUC, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("MA_CUC", "WChar", clsDAL.ToDBParam(MA_CUC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TEN_CUC", "WChar", clsDAL.ToDBParam(TEN_CUC, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}