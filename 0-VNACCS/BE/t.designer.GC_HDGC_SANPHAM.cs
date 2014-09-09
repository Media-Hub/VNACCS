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
	/// Generated Class for Table : GC_HDGC_SANPHAM.
	/// </summary>
	public partial class GC_HDGC_SANPHAM : TableBase
	{
		public GC_HDGC_SANPHAM() : base(){}

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
		private int m_SP_HDID;
		/// <summary>
		/// SP_HDID.
		/// </summary>
		public int SP_HDID
		{
			get
			{
				return m_SP_HDID;
			}
			set
			{
				if ((this.m_SP_HDID != value))
				{
					this.SendPropertyChanging("SP_HDID");
					this.m_SP_HDID = value;
					this.SendPropertyChanged("SP_HDID");
				}
			}
		}

		private int m_SP_ID;
		/// <summary>
		/// SP_ID.
		/// </summary>
		public int SP_ID
		{
			get
			{
				return m_SP_ID;
			}
			set
			{
				if ((this.m_SP_ID != value))
				{
					this.SendPropertyChanging("SP_ID");
					this.m_SP_ID = value;
					this.SendPropertyChanged("SP_ID");
				}
			}
		}

		private string m_SP_MA;
		private bool m_SP_MAUpdated = false;
		/// <summary>
		/// SP_MA.
		/// </summary>
		public string SP_MA
		{
			get
			{
				return m_SP_MA;
			}
			set
			{
				if ((this.m_SP_MA != value))
				{
					this.SendPropertyChanging("SP_MA");
					this.m_SP_MA = value;
					this.SendPropertyChanged("SP_MA");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_SP_MAUpdated = true;
				}
			}
		}

		private string m_SP_TEN;
		private bool m_SP_TENUpdated = false;
		/// <summary>
		/// SP_TEN.
		/// </summary>
		public string SP_TEN
		{
			get
			{
				return m_SP_TEN;
			}
			set
			{
				if ((this.m_SP_TEN != value))
				{
					this.SendPropertyChanging("SP_TEN");
					this.m_SP_TEN = value;
					this.SendPropertyChanged("SP_TEN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_SP_TENUpdated = true;
				}
			}
		}

		private string m_SP_DVT;
		private bool m_SP_DVTUpdated = false;
		/// <summary>
		/// SP_DVT.
		/// </summary>
		public string SP_DVT
		{
			get
			{
				return m_SP_DVT;
			}
			set
			{
				if ((this.m_SP_DVT != value))
				{
					this.SendPropertyChanging("SP_DVT");
					this.m_SP_DVT = value;
					this.SendPropertyChanged("SP_DVT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_SP_DVTUpdated = true;
				}
			}
		}

		private string m_SP_HS;
		private bool m_SP_HSUpdated = false;
		/// <summary>
		/// SP_HS.
		/// </summary>
		public string SP_HS
		{
			get
			{
				return m_SP_HS;
			}
			set
			{
				if ((this.m_SP_HS != value))
				{
					this.SendPropertyChanging("SP_HS");
					this.m_SP_HS = value;
					this.SendPropertyChanged("SP_HS");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_SP_HSUpdated = true;
				}
			}
		}

		private string m_SP_LOAISP_MA;
		private bool m_SP_LOAISP_MAUpdated = false;
		/// <summary>
		/// SP_LOAISP_MA.
		/// </summary>
		public string SP_LOAISP_MA
		{
			get
			{
				return m_SP_LOAISP_MA;
			}
			set
			{
				if ((this.m_SP_LOAISP_MA != value))
				{
					this.SendPropertyChanging("SP_LOAISP_MA");
					this.m_SP_LOAISP_MA = value;
					this.SendPropertyChanged("SP_LOAISP_MA");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_SP_LOAISP_MAUpdated = true;
				}
			}
		}

		private decimal? m_SP_DONGIA = 0;
		private bool m_SP_DONGIAUpdated = false;
		/// <summary>
		/// SP_DONGIA.
		/// </summary>
		public decimal? SP_DONGIA
		{
			get
			{
				return m_SP_DONGIA;
			}
			set
			{
				if ((this.m_SP_DONGIA != value))
				{
					this.SendPropertyChanging("SP_DONGIA");
					this.m_SP_DONGIA = value;
					this.SendPropertyChanged("SP_DONGIA");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_SP_DONGIAUpdated = true;
				}
			}
		}

		private string m_SP_MABIEUTHUE_XNK;
		private bool m_SP_MABIEUTHUE_XNKUpdated = false;
		/// <summary>
		/// SP_MABIEUTHUE_XNK.
		/// </summary>
		public string SP_MABIEUTHUE_XNK
		{
			get
			{
				return m_SP_MABIEUTHUE_XNK;
			}
			set
			{
				if ((this.m_SP_MABIEUTHUE_XNK != value))
				{
					this.SendPropertyChanging("SP_MABIEUTHUE_XNK");
					this.m_SP_MABIEUTHUE_XNK = value;
					this.SendPropertyChanged("SP_MABIEUTHUE_XNK");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_SP_MABIEUTHUE_XNKUpdated = true;
				}
			}
		}

		private string m_SP_MABIEUTHUE_TBDB;
		private bool m_SP_MABIEUTHUE_TBDBUpdated = false;
		/// <summary>
		/// SP_MABIEUTHUE_TBDB.
		/// </summary>
		public string SP_MABIEUTHUE_TBDB
		{
			get
			{
				return m_SP_MABIEUTHUE_TBDB;
			}
			set
			{
				if ((this.m_SP_MABIEUTHUE_TBDB != value))
				{
					this.SendPropertyChanging("SP_MABIEUTHUE_TBDB");
					this.m_SP_MABIEUTHUE_TBDB = value;
					this.SendPropertyChanged("SP_MABIEUTHUE_TBDB");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_SP_MABIEUTHUE_TBDBUpdated = true;
				}
			}
		}

		private string m_SP_MABIEUTHUE_MT;
		private bool m_SP_MABIEUTHUE_MTUpdated = false;
		/// <summary>
		/// SP_MABIEUTHUE_MT.
		/// </summary>
		public string SP_MABIEUTHUE_MT
		{
			get
			{
				return m_SP_MABIEUTHUE_MT;
			}
			set
			{
				if ((this.m_SP_MABIEUTHUE_MT != value))
				{
					this.SendPropertyChanging("SP_MABIEUTHUE_MT");
					this.m_SP_MABIEUTHUE_MT = value;
					this.SendPropertyChanged("SP_MABIEUTHUE_MT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_SP_MABIEUTHUE_MTUpdated = true;
				}
			}
		}

		private string m_SP_MABIEUTHUE_VAT;
		private bool m_SP_MABIEUTHUE_VATUpdated = false;
		/// <summary>
		/// SP_MABIEUTHUE_VAT.
		/// </summary>
		public string SP_MABIEUTHUE_VAT
		{
			get
			{
				return m_SP_MABIEUTHUE_VAT;
			}
			set
			{
				if ((this.m_SP_MABIEUTHUE_VAT != value))
				{
					this.SendPropertyChanging("SP_MABIEUTHUE_VAT");
					this.m_SP_MABIEUTHUE_VAT = value;
					this.SendPropertyChanged("SP_MABIEUTHUE_VAT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_SP_MABIEUTHUE_VATUpdated = true;
				}
			}
		}

		private string m_SP_GHICHU;
		private bool m_SP_GHICHUUpdated = false;
		/// <summary>
		/// SP_GHICHU.
		/// </summary>
		public string SP_GHICHU
		{
			get
			{
				return m_SP_GHICHU;
			}
			set
			{
				if ((this.m_SP_GHICHU != value))
				{
					this.SendPropertyChanging("SP_GHICHU");
					this.m_SP_GHICHU = value;
					this.SendPropertyChanged("SP_GHICHU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_SP_GHICHUUpdated = true;
				}
			}
		}

		private string m_SP_TEN_EN;
		private bool m_SP_TEN_ENUpdated = false;
		/// <summary>
		/// SP_TEN_EN.
		/// </summary>
		public string SP_TEN_EN
		{
			get
			{
				return m_SP_TEN_EN;
			}
			set
			{
				if ((this.m_SP_TEN_EN != value))
				{
					this.SendPropertyChanging("SP_TEN_EN");
					this.m_SP_TEN_EN = value;
					this.SendPropertyChanged("SP_TEN_EN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_SP_TEN_ENUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM GC_HDGC_SANPHAM " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[SP_HDID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[SP_ID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[SP_MA]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[SP_TEN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[SP_DVT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[SP_HS]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[SP_LOAISP_MA]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[SP_DONGIA]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[SP_MABIEUTHUE_XNK]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[SP_MABIEUTHUE_TBDB]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[SP_MABIEUTHUE_MT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[SP_MABIEUTHUE_VAT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[SP_GHICHU]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[SP_TEN_EN]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("SP_HDID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("SP_ID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("SP_MA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("SP_TEN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("SP_DVT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("SP_HS", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("SP_LOAISP_MA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("SP_DONGIA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("SP_MABIEUTHUE_XNK", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("SP_MABIEUTHUE_TBDB", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("SP_MABIEUTHUE_MT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("SP_MABIEUTHUE_VAT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("SP_GHICHU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("SP_TEN_EN", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO GC_HDGC_SANPHAM ([SP_HDID], [SP_ID], [SP_MA], [SP_TEN], [SP_DVT], [SP_HS], [SP_LOAISP_MA], [SP_DONGIA], [SP_MABIEUTHUE_XNK], [SP_MABIEUTHUE_TBDB], [SP_MABIEUTHUE_MT], [SP_MABIEUTHUE_VAT], [SP_GHICHU], [SP_TEN_EN]) VALUES(").Append("@SP_HDID").Append(",").Append("@SP_ID").Append(",").Append("@SP_MA").Append(",").Append("@SP_TEN").Append(",").Append("@SP_DVT").Append(",").Append("@SP_HS").Append(",").Append("@SP_LOAISP_MA").Append(",").Append("@SP_DONGIA").Append(",").Append("@SP_MABIEUTHUE_XNK").Append(",").Append("@SP_MABIEUTHUE_TBDB").Append(",").Append("@SP_MABIEUTHUE_MT").Append(",").Append("@SP_MABIEUTHUE_VAT").Append(",").Append("@SP_GHICHU").Append(",").Append("@SP_TEN_EN").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO GC_HDGC_SANPHAM (SP_HDID, SP_ID, SP_MA, SP_TEN, SP_DVT, SP_HS, SP_LOAISP_MA, SP_DONGIA, SP_MABIEUTHUE_XNK, SP_MABIEUTHUE_TBDB, SP_MABIEUTHUE_MT, SP_MABIEUTHUE_VAT, SP_GHICHU, SP_TEN_EN) VALUES(").Append(":SP_HDID").Append(",").Append(":SP_ID").Append(",").Append(":SP_MA").Append(",").Append(":SP_TEN").Append(",").Append(":SP_DVT").Append(",").Append(":SP_HS").Append(",").Append(":SP_LOAISP_MA").Append(",").Append(":SP_DONGIA").Append(",").Append(":SP_MABIEUTHUE_XNK").Append(",").Append(":SP_MABIEUTHUE_TBDB").Append(",").Append(":SP_MABIEUTHUE_MT").Append(",").Append(":SP_MABIEUTHUE_VAT").Append(",").Append(":SP_GHICHU").Append(",").Append(":SP_TEN_EN").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE GC_HDGC_SANPHAM SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_SP_MAUpdated ? string.Format(",[SP_MA] = {0}", "@SP_MA") : string.Empty).Append(m_SP_TENUpdated ? string.Format(",[SP_TEN] = {0}", "@SP_TEN") : string.Empty).Append(m_SP_DVTUpdated ? string.Format(",[SP_DVT] = {0}", "@SP_DVT") : string.Empty).Append(m_SP_HSUpdated ? string.Format(",[SP_HS] = {0}", "@SP_HS") : string.Empty).Append(m_SP_LOAISP_MAUpdated ? string.Format(",[SP_LOAISP_MA] = {0}", "@SP_LOAISP_MA") : string.Empty).Append(m_SP_DONGIAUpdated ? string.Format(",[SP_DONGIA] = {0}", "@SP_DONGIA") : string.Empty).Append(m_SP_MABIEUTHUE_XNKUpdated ? string.Format(",[SP_MABIEUTHUE_XNK] = {0}", "@SP_MABIEUTHUE_XNK") : string.Empty).Append(m_SP_MABIEUTHUE_TBDBUpdated ? string.Format(",[SP_MABIEUTHUE_TBDB] = {0}", "@SP_MABIEUTHUE_TBDB") : string.Empty).Append(m_SP_MABIEUTHUE_MTUpdated ? string.Format(",[SP_MABIEUTHUE_MT] = {0}", "@SP_MABIEUTHUE_MT") : string.Empty).Append(m_SP_MABIEUTHUE_VATUpdated ? string.Format(",[SP_MABIEUTHUE_VAT] = {0}", "@SP_MABIEUTHUE_VAT") : string.Empty).Append(m_SP_GHICHUUpdated ? string.Format(",[SP_GHICHU] = {0}", "@SP_GHICHU") : string.Empty).Append(m_SP_TEN_ENUpdated ? string.Format(",[SP_TEN_EN] = {0}", "@SP_TEN_EN") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_SP_MAUpdated ? string.Format(",SP_MA = {0}", ":SP_MA") : string.Empty).Append(m_SP_TENUpdated ? string.Format(",SP_TEN = {0}", ":SP_TEN") : string.Empty).Append(m_SP_DVTUpdated ? string.Format(",SP_DVT = {0}", ":SP_DVT") : string.Empty).Append(m_SP_HSUpdated ? string.Format(",SP_HS = {0}", ":SP_HS") : string.Empty).Append(m_SP_LOAISP_MAUpdated ? string.Format(",SP_LOAISP_MA = {0}", ":SP_LOAISP_MA") : string.Empty).Append(m_SP_DONGIAUpdated ? string.Format(",SP_DONGIA = {0}", ":SP_DONGIA") : string.Empty).Append(m_SP_MABIEUTHUE_XNKUpdated ? string.Format(",SP_MABIEUTHUE_XNK = {0}", ":SP_MABIEUTHUE_XNK") : string.Empty).Append(m_SP_MABIEUTHUE_TBDBUpdated ? string.Format(",SP_MABIEUTHUE_TBDB = {0}", ":SP_MABIEUTHUE_TBDB") : string.Empty).Append(m_SP_MABIEUTHUE_MTUpdated ? string.Format(",SP_MABIEUTHUE_MT = {0}", ":SP_MABIEUTHUE_MT") : string.Empty).Append(m_SP_MABIEUTHUE_VATUpdated ? string.Format(",SP_MABIEUTHUE_VAT = {0}", ":SP_MABIEUTHUE_VAT") : string.Empty).Append(m_SP_GHICHUUpdated ? string.Format(",SP_GHICHU = {0}", ":SP_GHICHU") : string.Empty).Append(m_SP_TEN_ENUpdated ? string.Format(",SP_TEN_EN = {0}", ":SP_TEN_EN") : string.Empty);
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
				sbSQL.AppendFormat("[SP_MA] = {0}", "@SP_MA").AppendFormat(",[SP_TEN] = {0}", "@SP_TEN").AppendFormat(",[SP_DVT] = {0}", "@SP_DVT").AppendFormat(",[SP_HS] = {0}", "@SP_HS").AppendFormat(",[SP_LOAISP_MA] = {0}", "@SP_LOAISP_MA").AppendFormat(",[SP_DONGIA] = {0}", "@SP_DONGIA").AppendFormat(",[SP_MABIEUTHUE_XNK] = {0}", "@SP_MABIEUTHUE_XNK").AppendFormat(",[SP_MABIEUTHUE_TBDB] = {0}", "@SP_MABIEUTHUE_TBDB").AppendFormat(",[SP_MABIEUTHUE_MT] = {0}", "@SP_MABIEUTHUE_MT").AppendFormat(",[SP_MABIEUTHUE_VAT] = {0}", "@SP_MABIEUTHUE_VAT").AppendFormat(",[SP_GHICHU] = {0}", "@SP_GHICHU").AppendFormat(",[SP_TEN_EN] = {0}", "@SP_TEN_EN");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("SP_MA = {0}", ":SP_MA").AppendFormat(",SP_TEN = {0}", ":SP_TEN").AppendFormat(",SP_DVT = {0}", ":SP_DVT").AppendFormat(",SP_HS = {0}", ":SP_HS").AppendFormat(",SP_LOAISP_MA = {0}", ":SP_LOAISP_MA").AppendFormat(",SP_DONGIA = {0}", ":SP_DONGIA").AppendFormat(",SP_MABIEUTHUE_XNK = {0}", ":SP_MABIEUTHUE_XNK").AppendFormat(",SP_MABIEUTHUE_TBDB = {0}", ":SP_MABIEUTHUE_TBDB").AppendFormat(",SP_MABIEUTHUE_MT = {0}", ":SP_MABIEUTHUE_MT").AppendFormat(",SP_MABIEUTHUE_VAT = {0}", ":SP_MABIEUTHUE_VAT").AppendFormat(",SP_GHICHU = {0}", ":SP_GHICHU").AppendFormat(",SP_TEN_EN = {0}", ":SP_TEN_EN");
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
			return clsDAL.DeleteString("GC_HDGC_SANPHAM", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[SP_HDID] = {0}", "@SP_HDID").AppendFormat(" AND [SP_ID] = {0}", "@SP_ID");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("SP_HDID = {0}", ":SP_HDID").AppendFormat(" AND SP_ID = {0}", ":SP_ID");
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
			paramList.Add(clsDAL.CreateParameter("SP_HDID", "Integer", clsDAL.ToDBParam(SP_HDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_ID", "Integer", clsDAL.ToDBParam(SP_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("SP_MA", "WChar", clsDAL.ToDBParam(SP_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_TEN", "WChar", clsDAL.ToDBParam(SP_TEN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_DVT", "WChar", clsDAL.ToDBParam(SP_DVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_HS", "WChar", clsDAL.ToDBParam(SP_HS, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_LOAISP_MA", "WChar", clsDAL.ToDBParam(SP_LOAISP_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_DONGIA", "Numeric", clsDAL.ToDBParam(SP_DONGIA, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_MABIEUTHUE_XNK", "WChar", clsDAL.ToDBParam(SP_MABIEUTHUE_XNK, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_MABIEUTHUE_TBDB", "WChar", clsDAL.ToDBParam(SP_MABIEUTHUE_TBDB, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_MABIEUTHUE_MT", "WChar", clsDAL.ToDBParam(SP_MABIEUTHUE_MT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_MABIEUTHUE_VAT", "WChar", clsDAL.ToDBParam(SP_MABIEUTHUE_VAT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_GHICHU", "WChar", clsDAL.ToDBParam(SP_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_TEN_EN", "WChar", clsDAL.ToDBParam(SP_TEN_EN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_HDID", "Integer", clsDAL.ToDBParam(SP_HDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_ID", "Integer", clsDAL.ToDBParam(SP_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("SP_HDID", "Integer", clsDAL.ToDBParam(SP_HDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_ID", "Integer", clsDAL.ToDBParam(SP_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_MA", "WChar", clsDAL.ToDBParam(SP_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_TEN", "WChar", clsDAL.ToDBParam(SP_TEN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_DVT", "WChar", clsDAL.ToDBParam(SP_DVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_HS", "WChar", clsDAL.ToDBParam(SP_HS, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_LOAISP_MA", "WChar", clsDAL.ToDBParam(SP_LOAISP_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_DONGIA", "Numeric", clsDAL.ToDBParam(SP_DONGIA, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_MABIEUTHUE_XNK", "WChar", clsDAL.ToDBParam(SP_MABIEUTHUE_XNK, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_MABIEUTHUE_TBDB", "WChar", clsDAL.ToDBParam(SP_MABIEUTHUE_TBDB, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_MABIEUTHUE_MT", "WChar", clsDAL.ToDBParam(SP_MABIEUTHUE_MT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_MABIEUTHUE_VAT", "WChar", clsDAL.ToDBParam(SP_MABIEUTHUE_VAT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_GHICHU", "WChar", clsDAL.ToDBParam(SP_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SP_TEN_EN", "WChar", clsDAL.ToDBParam(SP_TEN_EN, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}