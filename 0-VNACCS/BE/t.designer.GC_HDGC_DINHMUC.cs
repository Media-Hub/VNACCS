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
	/// Generated Class for Table : GC_HDGC_DINHMUC.
	/// </summary>
	public partial class GC_HDGC_DINHMUC : TableBase
	{
		public GC_HDGC_DINHMUC() : base(){}

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
		private int m_DM_HDID;
		/// <summary>
		/// DM_HDID.
		/// </summary>
		public int DM_HDID
		{
			get
			{
				return m_DM_HDID;
			}
			set
			{
				if ((this.m_DM_HDID != value))
				{
					this.SendPropertyChanging("DM_HDID");
					this.m_DM_HDID = value;
					this.SendPropertyChanged("DM_HDID");
				}
			}
		}

		private string m_DM_SPID;
		/// <summary>
		/// DM_SPID.
		/// </summary>
		public string DM_SPID
		{
			get
			{
				return m_DM_SPID;
			}
			set
			{
				if ((this.m_DM_SPID != value))
				{
					this.SendPropertyChanging("DM_SPID");
					this.m_DM_SPID = value;
					this.SendPropertyChanged("DM_SPID");
				}
			}
		}

		private string m_DM_NLID;
		/// <summary>
		/// DM_NLID.
		/// </summary>
		public string DM_NLID
		{
			get
			{
				return m_DM_NLID;
			}
			set
			{
				if ((this.m_DM_NLID != value))
				{
					this.SendPropertyChanging("DM_NLID");
					this.m_DM_NLID = value;
					this.SendPropertyChanged("DM_NLID");
				}
			}
		}

		private decimal? m_DM_DINHMUCSUDUNG = 0;
		private bool m_DM_DINHMUCSUDUNGUpdated = false;
		/// <summary>
		/// DM_DINHMUCSUDUNG.
		/// </summary>
		public decimal? DM_DINHMUCSUDUNG
		{
			get
			{
				return m_DM_DINHMUCSUDUNG;
			}
			set
			{
				if ((this.m_DM_DINHMUCSUDUNG != value))
				{
					this.SendPropertyChanging("DM_DINHMUCSUDUNG");
					this.m_DM_DINHMUCSUDUNG = value;
					this.SendPropertyChanged("DM_DINHMUCSUDUNG");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DM_DINHMUCSUDUNGUpdated = true;
				}
			}
		}

		private decimal? m_DM_TLHH = 0;
		private bool m_DM_TLHHUpdated = false;
		/// <summary>
		/// DM_TLHH.
		/// </summary>
		public decimal? DM_TLHH
		{
			get
			{
				return m_DM_TLHH;
			}
			set
			{
				if ((this.m_DM_TLHH != value))
				{
					this.SendPropertyChanging("DM_TLHH");
					this.m_DM_TLHH = value;
					this.SendPropertyChanged("DM_TLHH");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DM_TLHHUpdated = true;
				}
			}
		}

		private string m_DM_NGUONNGUYENLIEU;
		private bool m_DM_NGUONNGUYENLIEUUpdated = false;
		/// <summary>
		/// DM_NGUONNGUYENLIEU.
		/// </summary>
		public string DM_NGUONNGUYENLIEU
		{
			get
			{
				return m_DM_NGUONNGUYENLIEU;
			}
			set
			{
				if ((this.m_DM_NGUONNGUYENLIEU != value))
				{
					this.SendPropertyChanging("DM_NGUONNGUYENLIEU");
					this.m_DM_NGUONNGUYENLIEU = value;
					this.SendPropertyChanged("DM_NGUONNGUYENLIEU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DM_NGUONNGUYENLIEUUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM GC_HDGC_DINHMUC " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[DM_HDID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DM_SPID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DM_NLID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DM_DINHMUCSUDUNG]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DM_TLHH]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DM_NGUONNGUYENLIEU]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("DM_HDID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DM_SPID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DM_NLID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DM_DINHMUCSUDUNG", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DM_TLHH", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DM_NGUONNGUYENLIEU", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO GC_HDGC_DINHMUC ([DM_HDID], [DM_SPID], [DM_NLID], [DM_DINHMUCSUDUNG], [DM_TLHH], [DM_NGUONNGUYENLIEU]) VALUES(").Append("@DM_HDID").Append(",").Append("@DM_SPID").Append(",").Append("@DM_NLID").Append(",").Append("@DM_DINHMUCSUDUNG").Append(",").Append("@DM_TLHH").Append(",").Append("@DM_NGUONNGUYENLIEU").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO GC_HDGC_DINHMUC (DM_HDID, DM_SPID, DM_NLID, DM_DINHMUCSUDUNG, DM_TLHH, DM_NGUONNGUYENLIEU) VALUES(").Append(":DM_HDID").Append(",").Append(":DM_SPID").Append(",").Append(":DM_NLID").Append(",").Append(":DM_DINHMUCSUDUNG").Append(",").Append(":DM_TLHH").Append(",").Append(":DM_NGUONNGUYENLIEU").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE GC_HDGC_DINHMUC SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_DM_DINHMUCSUDUNGUpdated ? string.Format(",[DM_DINHMUCSUDUNG] = {0}", "@DM_DINHMUCSUDUNG") : string.Empty).Append(m_DM_TLHHUpdated ? string.Format(",[DM_TLHH] = {0}", "@DM_TLHH") : string.Empty).Append(m_DM_NGUONNGUYENLIEUUpdated ? string.Format(",[DM_NGUONNGUYENLIEU] = {0}", "@DM_NGUONNGUYENLIEU") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_DM_DINHMUCSUDUNGUpdated ? string.Format(",DM_DINHMUCSUDUNG = {0}", ":DM_DINHMUCSUDUNG") : string.Empty).Append(m_DM_TLHHUpdated ? string.Format(",DM_TLHH = {0}", ":DM_TLHH") : string.Empty).Append(m_DM_NGUONNGUYENLIEUUpdated ? string.Format(",DM_NGUONNGUYENLIEU = {0}", ":DM_NGUONNGUYENLIEU") : string.Empty);
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
				sbSQL.AppendFormat("[DM_DINHMUCSUDUNG] = {0}", "@DM_DINHMUCSUDUNG").AppendFormat(",[DM_TLHH] = {0}", "@DM_TLHH").AppendFormat(",[DM_NGUONNGUYENLIEU] = {0}", "@DM_NGUONNGUYENLIEU");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("DM_DINHMUCSUDUNG = {0}", ":DM_DINHMUCSUDUNG").AppendFormat(",DM_TLHH = {0}", ":DM_TLHH").AppendFormat(",DM_NGUONNGUYENLIEU = {0}", ":DM_NGUONNGUYENLIEU");
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
			return clsDAL.DeleteString("GC_HDGC_DINHMUC", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[DM_HDID] = {0}", "@DM_HDID").AppendFormat(" AND [DM_SPID] = {0}", "@DM_SPID").AppendFormat(" AND [DM_NLID] = {0}", "@DM_NLID");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("DM_HDID = {0}", ":DM_HDID").AppendFormat(" AND DM_SPID = {0}", ":DM_SPID").AppendFormat(" AND DM_NLID = {0}", ":DM_NLID");
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
			paramList.Add(clsDAL.CreateParameter("DM_HDID", "Integer", clsDAL.ToDBParam(DM_HDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DM_SPID", "WChar", clsDAL.ToDBParam(DM_SPID, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DM_NLID", "WChar", clsDAL.ToDBParam(DM_NLID, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("DM_DINHMUCSUDUNG", "Numeric", clsDAL.ToDBParam(DM_DINHMUCSUDUNG, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DM_TLHH", "Numeric", clsDAL.ToDBParam(DM_TLHH, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DM_NGUONNGUYENLIEU", "WChar", clsDAL.ToDBParam(DM_NGUONNGUYENLIEU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DM_HDID", "Integer", clsDAL.ToDBParam(DM_HDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DM_SPID", "WChar", clsDAL.ToDBParam(DM_SPID, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DM_NLID", "WChar", clsDAL.ToDBParam(DM_NLID, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("DM_HDID", "Integer", clsDAL.ToDBParam(DM_HDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DM_SPID", "WChar", clsDAL.ToDBParam(DM_SPID, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DM_NLID", "WChar", clsDAL.ToDBParam(DM_NLID, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DM_DINHMUCSUDUNG", "Numeric", clsDAL.ToDBParam(DM_DINHMUCSUDUNG, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DM_TLHH", "Numeric", clsDAL.ToDBParam(DM_TLHH, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DM_NGUONNGUYENLIEU", "WChar", clsDAL.ToDBParam(DM_NGUONNGUYENLIEU, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}