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
	/// Generated Class for Table : GC_HDGC_PK_NOIDUNG_DC.
	/// </summary>
	public partial class GC_HDGC_PK_NOIDUNG_DC : TableBase
	{
		public GC_HDGC_PK_NOIDUNG_DC() : base(){}

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
		private int m_DC_HDID = 0;
		/// <summary>
		/// DC_HDID.
		/// </summary>
		public int DC_HDID
		{
			get
			{
				return m_DC_HDID;
			}
			set
			{
				if ((this.m_DC_HDID != value))
				{
					this.SendPropertyChanging("DC_HDID");
					this.m_DC_HDID = value;
					this.SendPropertyChanged("DC_HDID");
				}
			}
		}

		private int m_DC_PKID = 0;
		/// <summary>
		/// DC_PKID.
		/// </summary>
		public int DC_PKID
		{
			get
			{
				return m_DC_PKID;
			}
			set
			{
				if ((this.m_DC_PKID != value))
				{
					this.SendPropertyChanging("DC_PKID");
					this.m_DC_PKID = value;
					this.SendPropertyChanged("DC_PKID");
				}
			}
		}

		private int m_DC_NDID = 0;
		/// <summary>
		/// DC_NDID.
		/// </summary>
		public int DC_NDID
		{
			get
			{
				return m_DC_NDID;
			}
			set
			{
				if ((this.m_DC_NDID != value))
				{
					this.SendPropertyChanging("DC_NDID");
					this.m_DC_NDID = value;
					this.SendPropertyChanged("DC_NDID");
				}
			}
		}

		private int m_DC_ID = 0;
		/// <summary>
		/// DC_ID.
		/// </summary>
		public int DC_ID
		{
			get
			{
				return m_DC_ID;
			}
			set
			{
				if ((this.m_DC_ID != value))
				{
					this.SendPropertyChanging("DC_ID");
					this.m_DC_ID = value;
					this.SendPropertyChanged("DC_ID");
				}
			}
		}

		private string m_MA_NLSPTB;
		private bool m_MA_NLSPTBUpdated = false;
		/// <summary>
		/// MA_NLSPTB.
		/// </summary>
		public string MA_NLSPTB
		{
			get
			{
				return m_MA_NLSPTB;
			}
			set
			{
				if ((this.m_MA_NLSPTB != value))
				{
					this.SendPropertyChanging("MA_NLSPTB");
					this.m_MA_NLSPTB = value;
					this.SendPropertyChanged("MA_NLSPTB");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_MA_NLSPTBUpdated = true;
				}
			}
		}

		private string m_DC_TRUOC;
		private bool m_DC_TRUOCUpdated = false;
		/// <summary>
		/// DC_TRUOC.
		/// </summary>
		public string DC_TRUOC
		{
			get
			{
				return m_DC_TRUOC;
			}
			set
			{
				if ((this.m_DC_TRUOC != value))
				{
					this.SendPropertyChanging("DC_TRUOC");
					this.m_DC_TRUOC = value;
					this.SendPropertyChanged("DC_TRUOC");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DC_TRUOCUpdated = true;
				}
			}
		}

		private string m_DC_SAU;
		private bool m_DC_SAUUpdated = false;
		/// <summary>
		/// DC_SAU.
		/// </summary>
		public string DC_SAU
		{
			get
			{
				return m_DC_SAU;
			}
			set
			{
				if ((this.m_DC_SAU != value))
				{
					this.SendPropertyChanging("DC_SAU");
					this.m_DC_SAU = value;
					this.SendPropertyChanged("DC_SAU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DC_SAUUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM GC_HDGC_PK_NOIDUNG_DC " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[DC_HDID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DC_PKID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DC_NDID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DC_ID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[MA_NLSPTB]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DC_TRUOC]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DC_SAU]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("DC_HDID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DC_PKID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DC_NDID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DC_ID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("MA_NLSPTB", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DC_TRUOC", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DC_SAU", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO GC_HDGC_PK_NOIDUNG_DC ([DC_HDID], [DC_PKID], [DC_NDID], [DC_ID], [MA_NLSPTB], [DC_TRUOC], [DC_SAU]) VALUES(").Append("@DC_HDID").Append(",").Append("@DC_PKID").Append(",").Append("@DC_NDID").Append(",").Append("@DC_ID").Append(",").Append("@MA_NLSPTB").Append(",").Append("@DC_TRUOC").Append(",").Append("@DC_SAU").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO GC_HDGC_PK_NOIDUNG_DC (DC_HDID, DC_PKID, DC_NDID, DC_ID, MA_NLSPTB, DC_TRUOC, DC_SAU) VALUES(").Append(":DC_HDID").Append(",").Append(":DC_PKID").Append(",").Append(":DC_NDID").Append(",").Append(":DC_ID").Append(",").Append(":MA_NLSPTB").Append(",").Append(":DC_TRUOC").Append(",").Append(":DC_SAU").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE GC_HDGC_PK_NOIDUNG_DC SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_MA_NLSPTBUpdated ? string.Format(",[MA_NLSPTB] = {0}", "@MA_NLSPTB") : string.Empty).Append(m_DC_TRUOCUpdated ? string.Format(",[DC_TRUOC] = {0}", "@DC_TRUOC") : string.Empty).Append(m_DC_SAUUpdated ? string.Format(",[DC_SAU] = {0}", "@DC_SAU") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_MA_NLSPTBUpdated ? string.Format(",MA_NLSPTB = {0}", ":MA_NLSPTB") : string.Empty).Append(m_DC_TRUOCUpdated ? string.Format(",DC_TRUOC = {0}", ":DC_TRUOC") : string.Empty).Append(m_DC_SAUUpdated ? string.Format(",DC_SAU = {0}", ":DC_SAU") : string.Empty);
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
				sbSQL.AppendFormat("[MA_NLSPTB] = {0}", "@MA_NLSPTB").AppendFormat(",[DC_TRUOC] = {0}", "@DC_TRUOC").AppendFormat(",[DC_SAU] = {0}", "@DC_SAU");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("MA_NLSPTB = {0}", ":MA_NLSPTB").AppendFormat(",DC_TRUOC = {0}", ":DC_TRUOC").AppendFormat(",DC_SAU = {0}", ":DC_SAU");
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
			return clsDAL.DeleteString("GC_HDGC_PK_NOIDUNG_DC", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[DC_HDID] = {0}", "@DC_HDID").AppendFormat(" AND [DC_PKID] = {0}", "@DC_PKID").AppendFormat(" AND [DC_NDID] = {0}", "@DC_NDID").AppendFormat(" AND [DC_ID] = {0}", "@DC_ID");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("DC_HDID = {0}", ":DC_HDID").AppendFormat(" AND DC_PKID = {0}", ":DC_PKID").AppendFormat(" AND DC_NDID = {0}", ":DC_NDID").AppendFormat(" AND DC_ID = {0}", ":DC_ID");
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
			paramList.Add(clsDAL.CreateParameter("DC_HDID", "Integer", clsDAL.ToDBParam(DC_HDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DC_PKID", "Integer", clsDAL.ToDBParam(DC_PKID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DC_NDID", "Integer", clsDAL.ToDBParam(DC_NDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DC_ID", "Integer", clsDAL.ToDBParam(DC_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("MA_NLSPTB", "WChar", clsDAL.ToDBParam(MA_NLSPTB, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DC_TRUOC", "WChar", clsDAL.ToDBParam(DC_TRUOC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DC_SAU", "WChar", clsDAL.ToDBParam(DC_SAU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DC_HDID", "Integer", clsDAL.ToDBParam(DC_HDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DC_PKID", "Integer", clsDAL.ToDBParam(DC_PKID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DC_NDID", "Integer", clsDAL.ToDBParam(DC_NDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DC_ID", "Integer", clsDAL.ToDBParam(DC_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("DC_HDID", "Integer", clsDAL.ToDBParam(DC_HDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DC_PKID", "Integer", clsDAL.ToDBParam(DC_PKID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DC_NDID", "Integer", clsDAL.ToDBParam(DC_NDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DC_ID", "Integer", clsDAL.ToDBParam(DC_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("MA_NLSPTB", "WChar", clsDAL.ToDBParam(MA_NLSPTB, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DC_TRUOC", "WChar", clsDAL.ToDBParam(DC_TRUOC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DC_SAU", "WChar", clsDAL.ToDBParam(DC_SAU, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}