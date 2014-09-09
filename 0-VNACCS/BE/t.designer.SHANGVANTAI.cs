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
	/// Generated Class for Table : SHANGVANTAI.
	/// </summary>
	public partial class SHANGVANTAI : TableBase
	{
		public SHANGVANTAI() : base(){}

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
		private string m_HVT_MAHVT;
		/// <summary>
		/// HVT_MAHVT.
		/// </summary>
		public string HVT_MAHVT
		{
			get
			{
				return m_HVT_MAHVT;
			}
			set
			{
				if ((this.m_HVT_MAHVT != value))
				{
					this.SendPropertyChanging("HVT_MAHVT");
					this.m_HVT_MAHVT = value;
					this.SendPropertyChanged("HVT_MAHVT");
				}
			}
		}

		private string m_HVT_TENHVT;
		private bool m_HVT_TENHVTUpdated = false;
		/// <summary>
		/// HVT_TENHVT.
		/// </summary>
		public string HVT_TENHVT
		{
			get
			{
				return m_HVT_TENHVT;
			}
			set
			{
				if ((this.m_HVT_TENHVT != value))
				{
					this.SendPropertyChanging("HVT_TENHVT");
					this.m_HVT_TENHVT = value;
					this.SendPropertyChanged("HVT_TENHVT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HVT_TENHVTUpdated = true;
				}
			}
		}

		private string m_HVT_SOHIEUPTVT;
		private bool m_HVT_SOHIEUPTVTUpdated = false;
		/// <summary>
		/// HVT_SOHIEUPTVT.
		/// </summary>
		public string HVT_SOHIEUPTVT
		{
			get
			{
				return m_HVT_SOHIEUPTVT;
			}
			set
			{
				if ((this.m_HVT_SOHIEUPTVT != value))
				{
					this.SendPropertyChanging("HVT_SOHIEUPTVT");
					this.m_HVT_SOHIEUPTVT = value;
					this.SendPropertyChanged("HVT_SOHIEUPTVT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HVT_SOHIEUPTVTUpdated = true;
				}
			}
		}

		private string m_HVT_TENPTVT;
		private bool m_HVT_TENPTVTUpdated = false;
		/// <summary>
		/// HVT_TENPTVT.
		/// </summary>
		public string HVT_TENPTVT
		{
			get
			{
				return m_HVT_TENPTVT;
			}
			set
			{
				if ((this.m_HVT_TENPTVT != value))
				{
					this.SendPropertyChanging("HVT_TENPTVT");
					this.m_HVT_TENPTVT = value;
					this.SendPropertyChanged("HVT_TENPTVT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HVT_TENPTVTUpdated = true;
				}
			}
		}

		private string m_HVT_GHICHU;
		private bool m_HVT_GHICHUUpdated = false;
		/// <summary>
		/// HVT_GHICHU.
		/// </summary>
		public string HVT_GHICHU
		{
			get
			{
				return m_HVT_GHICHU;
			}
			set
			{
				if ((this.m_HVT_GHICHU != value))
				{
					this.SendPropertyChanging("HVT_GHICHU");
					this.m_HVT_GHICHU = value;
					this.SendPropertyChanged("HVT_GHICHU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HVT_GHICHUUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SHANGVANTAI " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[HVT_MAHVT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HVT_TENHVT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HVT_SOHIEUPTVT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HVT_TENPTVT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HVT_GHICHU]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("HVT_MAHVT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HVT_TENHVT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HVT_SOHIEUPTVT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HVT_TENPTVT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HVT_GHICHU", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO SHANGVANTAI ([HVT_MAHVT], [HVT_TENHVT], [HVT_SOHIEUPTVT], [HVT_TENPTVT], [HVT_GHICHU]) VALUES(").Append("@HVT_MAHVT").Append(",").Append("@HVT_TENHVT").Append(",").Append("@HVT_SOHIEUPTVT").Append(",").Append("@HVT_TENPTVT").Append(",").Append("@HVT_GHICHU").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO SHANGVANTAI (HVT_MAHVT, HVT_TENHVT, HVT_SOHIEUPTVT, HVT_TENPTVT, HVT_GHICHU) VALUES(").Append(":HVT_MAHVT").Append(",").Append(":HVT_TENHVT").Append(",").Append(":HVT_SOHIEUPTVT").Append(",").Append(":HVT_TENPTVT").Append(",").Append(":HVT_GHICHU").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SHANGVANTAI SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_HVT_TENHVTUpdated ? string.Format(",[HVT_TENHVT] = {0}", "@HVT_TENHVT") : string.Empty).Append(m_HVT_SOHIEUPTVTUpdated ? string.Format(",[HVT_SOHIEUPTVT] = {0}", "@HVT_SOHIEUPTVT") : string.Empty).Append(m_HVT_TENPTVTUpdated ? string.Format(",[HVT_TENPTVT] = {0}", "@HVT_TENPTVT") : string.Empty).Append(m_HVT_GHICHUUpdated ? string.Format(",[HVT_GHICHU] = {0}", "@HVT_GHICHU") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_HVT_TENHVTUpdated ? string.Format(",HVT_TENHVT = {0}", ":HVT_TENHVT") : string.Empty).Append(m_HVT_SOHIEUPTVTUpdated ? string.Format(",HVT_SOHIEUPTVT = {0}", ":HVT_SOHIEUPTVT") : string.Empty).Append(m_HVT_TENPTVTUpdated ? string.Format(",HVT_TENPTVT = {0}", ":HVT_TENPTVT") : string.Empty).Append(m_HVT_GHICHUUpdated ? string.Format(",HVT_GHICHU = {0}", ":HVT_GHICHU") : string.Empty);
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
				sbSQL.AppendFormat("[HVT_TENHVT] = {0}", "@HVT_TENHVT").AppendFormat(",[HVT_SOHIEUPTVT] = {0}", "@HVT_SOHIEUPTVT").AppendFormat(",[HVT_TENPTVT] = {0}", "@HVT_TENPTVT").AppendFormat(",[HVT_GHICHU] = {0}", "@HVT_GHICHU");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("HVT_TENHVT = {0}", ":HVT_TENHVT").AppendFormat(",HVT_SOHIEUPTVT = {0}", ":HVT_SOHIEUPTVT").AppendFormat(",HVT_TENPTVT = {0}", ":HVT_TENPTVT").AppendFormat(",HVT_GHICHU = {0}", ":HVT_GHICHU");
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
			return clsDAL.DeleteString("SHANGVANTAI", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[HVT_MAHVT] = {0}", "@HVT_MAHVT");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("HVT_MAHVT = {0}", ":HVT_MAHVT");
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
			paramList.Add(clsDAL.CreateParameter("HVT_MAHVT", "WChar", clsDAL.ToDBParam(HVT_MAHVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("HVT_TENHVT", "WChar", clsDAL.ToDBParam(HVT_TENHVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HVT_SOHIEUPTVT", "WChar", clsDAL.ToDBParam(HVT_SOHIEUPTVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HVT_TENPTVT", "WChar", clsDAL.ToDBParam(HVT_TENPTVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HVT_GHICHU", "WChar", clsDAL.ToDBParam(HVT_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HVT_MAHVT", "WChar", clsDAL.ToDBParam(HVT_MAHVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("HVT_MAHVT", "WChar", clsDAL.ToDBParam(HVT_MAHVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HVT_TENHVT", "WChar", clsDAL.ToDBParam(HVT_TENHVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HVT_SOHIEUPTVT", "WChar", clsDAL.ToDBParam(HVT_SOHIEUPTVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HVT_TENPTVT", "WChar", clsDAL.ToDBParam(HVT_TENPTVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HVT_GHICHU", "WChar", clsDAL.ToDBParam(HVT_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}