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
	/// Generated Class for Table : SDKGH.
	/// </summary>
	public partial class SDKGH : TableBase
	{
		public SDKGH() : base(){}

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
		private string m_DKGH_MA;
		/// <summary>
		/// DKGH_MA.
		/// </summary>
		public string DKGH_MA
		{
			get
			{
				return m_DKGH_MA;
			}
			set
			{
				if ((this.m_DKGH_MA != value))
				{
					this.SendPropertyChanging("DKGH_MA");
					this.m_DKGH_MA = value;
					this.SendPropertyChanged("DKGH_MA");
				}
			}
		}

		private string m_DKGH_TEN;
		private bool m_DKGH_TENUpdated = false;
		/// <summary>
		/// DKGH_TEN.
		/// </summary>
		public string DKGH_TEN
		{
			get
			{
				return m_DKGH_TEN;
			}
			set
			{
				if ((this.m_DKGH_TEN != value))
				{
					this.SendPropertyChanging("DKGH_TEN");
					this.m_DKGH_TEN = value;
					this.SendPropertyChanged("DKGH_TEN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DKGH_TENUpdated = true;
				}
			}
		}

		private string m_DKGH_GHICHU;
		private bool m_DKGH_GHICHUUpdated = false;
		/// <summary>
		/// DKGH_GHICHU.
		/// </summary>
		public string DKGH_GHICHU
		{
			get
			{
				return m_DKGH_GHICHU;
			}
			set
			{
				if ((this.m_DKGH_GHICHU != value))
				{
					this.SendPropertyChanging("DKGH_GHICHU");
					this.m_DKGH_GHICHU = value;
					this.SendPropertyChanged("DKGH_GHICHU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DKGH_GHICHUUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SDKGH " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[DKGH_MA]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DKGH_TEN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DKGH_GHICHU]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("DKGH_MA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DKGH_TEN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DKGH_GHICHU", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO SDKGH ([DKGH_MA], [DKGH_TEN], [DKGH_GHICHU]) VALUES(").Append("@DKGH_MA").Append(",").Append("@DKGH_TEN").Append(",").Append("@DKGH_GHICHU").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO SDKGH (DKGH_MA, DKGH_TEN, DKGH_GHICHU) VALUES(").Append(":DKGH_MA").Append(",").Append(":DKGH_TEN").Append(",").Append(":DKGH_GHICHU").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SDKGH SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_DKGH_TENUpdated ? string.Format(",[DKGH_TEN] = {0}", "@DKGH_TEN") : string.Empty).Append(m_DKGH_GHICHUUpdated ? string.Format(",[DKGH_GHICHU] = {0}", "@DKGH_GHICHU") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_DKGH_TENUpdated ? string.Format(",DKGH_TEN = {0}", ":DKGH_TEN") : string.Empty).Append(m_DKGH_GHICHUUpdated ? string.Format(",DKGH_GHICHU = {0}", ":DKGH_GHICHU") : string.Empty);
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
				sbSQL.AppendFormat("[DKGH_TEN] = {0}", "@DKGH_TEN").AppendFormat(",[DKGH_GHICHU] = {0}", "@DKGH_GHICHU");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("DKGH_TEN = {0}", ":DKGH_TEN").AppendFormat(",DKGH_GHICHU = {0}", ":DKGH_GHICHU");
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
			return clsDAL.DeleteString("SDKGH", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[DKGH_MA] = {0}", "@DKGH_MA");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("DKGH_MA = {0}", ":DKGH_MA");
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
			paramList.Add(clsDAL.CreateParameter("DKGH_MA", "WChar", clsDAL.ToDBParam(DKGH_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("DKGH_TEN", "WChar", clsDAL.ToDBParam(DKGH_TEN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DKGH_GHICHU", "WChar", clsDAL.ToDBParam(DKGH_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DKGH_MA", "WChar", clsDAL.ToDBParam(DKGH_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("DKGH_MA", "WChar", clsDAL.ToDBParam(DKGH_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DKGH_TEN", "WChar", clsDAL.ToDBParam(DKGH_TEN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DKGH_GHICHU", "WChar", clsDAL.ToDBParam(DKGH_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}