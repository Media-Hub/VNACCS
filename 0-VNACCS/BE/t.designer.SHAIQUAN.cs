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
	/// Generated Class for Table : SHAIQUAN.
	/// </summary>
	public partial class SHAIQUAN : TableBase
	{
		public SHAIQUAN() : base(){}

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
		private string m_HQ_MA;
		/// <summary>
		/// HQ_MA.
		/// </summary>
		public string HQ_MA
		{
			get
			{
				return m_HQ_MA;
			}
			set
			{
				if ((this.m_HQ_MA != value))
				{
					this.SendPropertyChanging("HQ_MA");
					this.m_HQ_MA = value;
					this.SendPropertyChanged("HQ_MA");
				}
			}
		}

		private string m_HQ_TEN;
		private bool m_HQ_TENUpdated = false;
		/// <summary>
		/// HQ_TEN.
		/// </summary>
		public string HQ_TEN
		{
			get
			{
				return m_HQ_TEN;
			}
			set
			{
				if ((this.m_HQ_TEN != value))
				{
					this.SendPropertyChanging("HQ_TEN");
					this.m_HQ_TEN = value;
					this.SendPropertyChanged("HQ_TEN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HQ_TENUpdated = true;
				}
			}
		}

		private string m_HQ_MACU;
		private bool m_HQ_MACUUpdated = false;
		/// <summary>
		/// HQ_MACU.
		/// </summary>
		public string HQ_MACU
		{
			get
			{
				return m_HQ_MACU;
			}
			set
			{
				if ((this.m_HQ_MACU != value))
				{
					this.SendPropertyChanging("HQ_MACU");
					this.m_HQ_MACU = value;
					this.SendPropertyChanged("HQ_MACU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HQ_MACUUpdated = true;
				}
			}
		}

		private string m_HQ_TENCU;
		private bool m_HQ_TENCUUpdated = false;
		/// <summary>
		/// HQ_TENCU.
		/// </summary>
		public string HQ_TENCU
		{
			get
			{
				return m_HQ_TENCU;
			}
			set
			{
				if ((this.m_HQ_TENCU != value))
				{
					this.SendPropertyChanging("HQ_TENCU");
					this.m_HQ_TENCU = value;
					this.SendPropertyChanged("HQ_TENCU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HQ_TENCUUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SHAIQUAN " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[HQ_MA]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HQ_TEN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HQ_MACU]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HQ_TENCU]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("HQ_MA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HQ_TEN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HQ_MACU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HQ_TENCU", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO SHAIQUAN ([HQ_MA], [HQ_TEN], [HQ_MACU], [HQ_TENCU]) VALUES(").Append("@HQ_MA").Append(",").Append("@HQ_TEN").Append(",").Append("@HQ_MACU").Append(",").Append("@HQ_TENCU").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO SHAIQUAN (HQ_MA, HQ_TEN, HQ_MACU, HQ_TENCU) VALUES(").Append(":HQ_MA").Append(",").Append(":HQ_TEN").Append(",").Append(":HQ_MACU").Append(",").Append(":HQ_TENCU").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SHAIQUAN SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_HQ_TENUpdated ? string.Format(",[HQ_TEN] = {0}", "@HQ_TEN") : string.Empty).Append(m_HQ_MACUUpdated ? string.Format(",[HQ_MACU] = {0}", "@HQ_MACU") : string.Empty).Append(m_HQ_TENCUUpdated ? string.Format(",[HQ_TENCU] = {0}", "@HQ_TENCU") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_HQ_TENUpdated ? string.Format(",HQ_TEN = {0}", ":HQ_TEN") : string.Empty).Append(m_HQ_MACUUpdated ? string.Format(",HQ_MACU = {0}", ":HQ_MACU") : string.Empty).Append(m_HQ_TENCUUpdated ? string.Format(",HQ_TENCU = {0}", ":HQ_TENCU") : string.Empty);
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
				sbSQL.AppendFormat("[HQ_TEN] = {0}", "@HQ_TEN").AppendFormat(",[HQ_MACU] = {0}", "@HQ_MACU").AppendFormat(",[HQ_TENCU] = {0}", "@HQ_TENCU");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("HQ_TEN = {0}", ":HQ_TEN").AppendFormat(",HQ_MACU = {0}", ":HQ_MACU").AppendFormat(",HQ_TENCU = {0}", ":HQ_TENCU");
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
			return clsDAL.DeleteString("SHAIQUAN", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[HQ_MA] = {0}", "@HQ_MA");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("HQ_MA = {0}", ":HQ_MA");
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
			paramList.Add(clsDAL.CreateParameter("HQ_MA", "WChar", clsDAL.ToDBParam(HQ_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("HQ_TEN", "WChar", clsDAL.ToDBParam(HQ_TEN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HQ_MACU", "WChar", clsDAL.ToDBParam(HQ_MACU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HQ_TENCU", "WChar", clsDAL.ToDBParam(HQ_TENCU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HQ_MA", "WChar", clsDAL.ToDBParam(HQ_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("HQ_MA", "WChar", clsDAL.ToDBParam(HQ_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HQ_TEN", "WChar", clsDAL.ToDBParam(HQ_TEN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HQ_MACU", "WChar", clsDAL.ToDBParam(HQ_MACU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HQ_TENCU", "WChar", clsDAL.ToDBParam(HQ_TENCU, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}