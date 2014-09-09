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
	/// Generated Class for Table : GC_HDGC_HANGMAU.
	/// </summary>
	public partial class GC_HDGC_HANGMAU : TableBase
	{
		public GC_HDGC_HANGMAU() : base(){}

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
		private int m_HM_HDID;
		/// <summary>
		/// HM_HDID.
		/// </summary>
		public int HM_HDID
		{
			get
			{
				return m_HM_HDID;
			}
			set
			{
				if ((this.m_HM_HDID != value))
				{
					this.SendPropertyChanging("HM_HDID");
					this.m_HM_HDID = value;
					this.SendPropertyChanged("HM_HDID");
				}
			}
		}

		private int m_HM_ID;
		/// <summary>
		/// HM_ID.
		/// </summary>
		public int HM_ID
		{
			get
			{
				return m_HM_ID;
			}
			set
			{
				if ((this.m_HM_ID != value))
				{
					this.SendPropertyChanging("HM_ID");
					this.m_HM_ID = value;
					this.SendPropertyChanged("HM_ID");
				}
			}
		}

		private string m_HM_MA;
		private bool m_HM_MAUpdated = false;
		/// <summary>
		/// HM_MA.
		/// </summary>
		public string HM_MA
		{
			get
			{
				return m_HM_MA;
			}
			set
			{
				if ((this.m_HM_MA != value))
				{
					this.SendPropertyChanging("HM_MA");
					this.m_HM_MA = value;
					this.SendPropertyChanged("HM_MA");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HM_MAUpdated = true;
				}
			}
		}

		private string m_HM_TEN;
		private bool m_HM_TENUpdated = false;
		/// <summary>
		/// HM_TEN.
		/// </summary>
		public string HM_TEN
		{
			get
			{
				return m_HM_TEN;
			}
			set
			{
				if ((this.m_HM_TEN != value))
				{
					this.SendPropertyChanging("HM_TEN");
					this.m_HM_TEN = value;
					this.SendPropertyChanged("HM_TEN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HM_TENUpdated = true;
				}
			}
		}

		private string m_HM_DVT;
		private bool m_HM_DVTUpdated = false;
		/// <summary>
		/// HM_DVT.
		/// </summary>
		public string HM_DVT
		{
			get
			{
				return m_HM_DVT;
			}
			set
			{
				if ((this.m_HM_DVT != value))
				{
					this.SendPropertyChanging("HM_DVT");
					this.m_HM_DVT = value;
					this.SendPropertyChanged("HM_DVT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HM_DVTUpdated = true;
				}
			}
		}

		private string m_HM_HS;
		private bool m_HM_HSUpdated = false;
		/// <summary>
		/// HM_HS.
		/// </summary>
		public string HM_HS
		{
			get
			{
				return m_HM_HS;
			}
			set
			{
				if ((this.m_HM_HS != value))
				{
					this.SendPropertyChanging("HM_HS");
					this.m_HM_HS = value;
					this.SendPropertyChanged("HM_HS");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HM_HSUpdated = true;
				}
			}
		}

		private decimal? m_HM_SOLUONG = 0;
		private bool m_HM_SOLUONGUpdated = false;
		/// <summary>
		/// HM_SOLUONG.
		/// </summary>
		public decimal? HM_SOLUONG
		{
			get
			{
				return m_HM_SOLUONG;
			}
			set
			{
				if ((this.m_HM_SOLUONG != value))
				{
					this.SendPropertyChanging("HM_SOLUONG");
					this.m_HM_SOLUONG = value;
					this.SendPropertyChanged("HM_SOLUONG");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HM_SOLUONGUpdated = true;
				}
			}
		}

		private string m_HM_MABIEUTHUE_XNK;
		private bool m_HM_MABIEUTHUE_XNKUpdated = false;
		/// <summary>
		/// HM_MABIEUTHUE_XNK.
		/// </summary>
		public string HM_MABIEUTHUE_XNK
		{
			get
			{
				return m_HM_MABIEUTHUE_XNK;
			}
			set
			{
				if ((this.m_HM_MABIEUTHUE_XNK != value))
				{
					this.SendPropertyChanging("HM_MABIEUTHUE_XNK");
					this.m_HM_MABIEUTHUE_XNK = value;
					this.SendPropertyChanged("HM_MABIEUTHUE_XNK");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HM_MABIEUTHUE_XNKUpdated = true;
				}
			}
		}

		private string m_HM_MABIEUTHUE_TBDB;
		private bool m_HM_MABIEUTHUE_TBDBUpdated = false;
		/// <summary>
		/// HM_MABIEUTHUE_TBDB.
		/// </summary>
		public string HM_MABIEUTHUE_TBDB
		{
			get
			{
				return m_HM_MABIEUTHUE_TBDB;
			}
			set
			{
				if ((this.m_HM_MABIEUTHUE_TBDB != value))
				{
					this.SendPropertyChanging("HM_MABIEUTHUE_TBDB");
					this.m_HM_MABIEUTHUE_TBDB = value;
					this.SendPropertyChanged("HM_MABIEUTHUE_TBDB");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HM_MABIEUTHUE_TBDBUpdated = true;
				}
			}
		}

		private string m_HM_MABIEUTHUE_MT;
		private bool m_HM_MABIEUTHUE_MTUpdated = false;
		/// <summary>
		/// HM_MABIEUTHUE_MT.
		/// </summary>
		public string HM_MABIEUTHUE_MT
		{
			get
			{
				return m_HM_MABIEUTHUE_MT;
			}
			set
			{
				if ((this.m_HM_MABIEUTHUE_MT != value))
				{
					this.SendPropertyChanging("HM_MABIEUTHUE_MT");
					this.m_HM_MABIEUTHUE_MT = value;
					this.SendPropertyChanged("HM_MABIEUTHUE_MT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HM_MABIEUTHUE_MTUpdated = true;
				}
			}
		}

		private string m_HM_MABIEUTHUE_VAT;
		private bool m_HM_MABIEUTHUE_VATUpdated = false;
		/// <summary>
		/// HM_MABIEUTHUE_VAT.
		/// </summary>
		public string HM_MABIEUTHUE_VAT
		{
			get
			{
				return m_HM_MABIEUTHUE_VAT;
			}
			set
			{
				if ((this.m_HM_MABIEUTHUE_VAT != value))
				{
					this.SendPropertyChanging("HM_MABIEUTHUE_VAT");
					this.m_HM_MABIEUTHUE_VAT = value;
					this.SendPropertyChanged("HM_MABIEUTHUE_VAT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HM_MABIEUTHUE_VATUpdated = true;
				}
			}
		}

		private string m_HM_GHICHU;
		private bool m_HM_GHICHUUpdated = false;
		/// <summary>
		/// HM_GHICHU.
		/// </summary>
		public string HM_GHICHU
		{
			get
			{
				return m_HM_GHICHU;
			}
			set
			{
				if ((this.m_HM_GHICHU != value))
				{
					this.SendPropertyChanging("HM_GHICHU");
					this.m_HM_GHICHU = value;
					this.SendPropertyChanged("HM_GHICHU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HM_GHICHUUpdated = true;
				}
			}
		}

		private string m_HM_TEN_EN;
		private bool m_HM_TEN_ENUpdated = false;
		/// <summary>
		/// HM_TEN_EN.
		/// </summary>
		public string HM_TEN_EN
		{
			get
			{
				return m_HM_TEN_EN;
			}
			set
			{
				if ((this.m_HM_TEN_EN != value))
				{
					this.SendPropertyChanging("HM_TEN_EN");
					this.m_HM_TEN_EN = value;
					this.SendPropertyChanged("HM_TEN_EN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HM_TEN_ENUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM GC_HDGC_HANGMAU " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[HM_HDID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HM_ID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HM_MA]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HM_TEN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HM_DVT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HM_HS]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HM_SOLUONG]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HM_MABIEUTHUE_XNK]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HM_MABIEUTHUE_TBDB]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HM_MABIEUTHUE_MT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HM_MABIEUTHUE_VAT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HM_GHICHU]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HM_TEN_EN]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("HM_HDID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HM_ID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HM_MA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HM_TEN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HM_DVT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HM_HS", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HM_SOLUONG", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HM_MABIEUTHUE_XNK", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HM_MABIEUTHUE_TBDB", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HM_MABIEUTHUE_MT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HM_MABIEUTHUE_VAT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HM_GHICHU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HM_TEN_EN", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO GC_HDGC_HANGMAU ([HM_HDID], [HM_ID], [HM_MA], [HM_TEN], [HM_DVT], [HM_HS], [HM_SOLUONG], [HM_MABIEUTHUE_XNK], [HM_MABIEUTHUE_TBDB], [HM_MABIEUTHUE_MT], [HM_MABIEUTHUE_VAT], [HM_GHICHU], [HM_TEN_EN]) VALUES(").Append("@HM_HDID").Append(",").Append("@HM_ID").Append(",").Append("@HM_MA").Append(",").Append("@HM_TEN").Append(",").Append("@HM_DVT").Append(",").Append("@HM_HS").Append(",").Append("@HM_SOLUONG").Append(",").Append("@HM_MABIEUTHUE_XNK").Append(",").Append("@HM_MABIEUTHUE_TBDB").Append(",").Append("@HM_MABIEUTHUE_MT").Append(",").Append("@HM_MABIEUTHUE_VAT").Append(",").Append("@HM_GHICHU").Append(",").Append("@HM_TEN_EN").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO GC_HDGC_HANGMAU (HM_HDID, HM_ID, HM_MA, HM_TEN, HM_DVT, HM_HS, HM_SOLUONG, HM_MABIEUTHUE_XNK, HM_MABIEUTHUE_TBDB, HM_MABIEUTHUE_MT, HM_MABIEUTHUE_VAT, HM_GHICHU, HM_TEN_EN) VALUES(").Append(":HM_HDID").Append(",").Append(":HM_ID").Append(",").Append(":HM_MA").Append(",").Append(":HM_TEN").Append(",").Append(":HM_DVT").Append(",").Append(":HM_HS").Append(",").Append(":HM_SOLUONG").Append(",").Append(":HM_MABIEUTHUE_XNK").Append(",").Append(":HM_MABIEUTHUE_TBDB").Append(",").Append(":HM_MABIEUTHUE_MT").Append(",").Append(":HM_MABIEUTHUE_VAT").Append(",").Append(":HM_GHICHU").Append(",").Append(":HM_TEN_EN").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE GC_HDGC_HANGMAU SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_HM_MAUpdated ? string.Format(",[HM_MA] = {0}", "@HM_MA") : string.Empty).Append(m_HM_TENUpdated ? string.Format(",[HM_TEN] = {0}", "@HM_TEN") : string.Empty).Append(m_HM_DVTUpdated ? string.Format(",[HM_DVT] = {0}", "@HM_DVT") : string.Empty).Append(m_HM_HSUpdated ? string.Format(",[HM_HS] = {0}", "@HM_HS") : string.Empty).Append(m_HM_SOLUONGUpdated ? string.Format(",[HM_SOLUONG] = {0}", "@HM_SOLUONG") : string.Empty).Append(m_HM_MABIEUTHUE_XNKUpdated ? string.Format(",[HM_MABIEUTHUE_XNK] = {0}", "@HM_MABIEUTHUE_XNK") : string.Empty).Append(m_HM_MABIEUTHUE_TBDBUpdated ? string.Format(",[HM_MABIEUTHUE_TBDB] = {0}", "@HM_MABIEUTHUE_TBDB") : string.Empty).Append(m_HM_MABIEUTHUE_MTUpdated ? string.Format(",[HM_MABIEUTHUE_MT] = {0}", "@HM_MABIEUTHUE_MT") : string.Empty).Append(m_HM_MABIEUTHUE_VATUpdated ? string.Format(",[HM_MABIEUTHUE_VAT] = {0}", "@HM_MABIEUTHUE_VAT") : string.Empty).Append(m_HM_GHICHUUpdated ? string.Format(",[HM_GHICHU] = {0}", "@HM_GHICHU") : string.Empty).Append(m_HM_TEN_ENUpdated ? string.Format(",[HM_TEN_EN] = {0}", "@HM_TEN_EN") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_HM_MAUpdated ? string.Format(",HM_MA = {0}", ":HM_MA") : string.Empty).Append(m_HM_TENUpdated ? string.Format(",HM_TEN = {0}", ":HM_TEN") : string.Empty).Append(m_HM_DVTUpdated ? string.Format(",HM_DVT = {0}", ":HM_DVT") : string.Empty).Append(m_HM_HSUpdated ? string.Format(",HM_HS = {0}", ":HM_HS") : string.Empty).Append(m_HM_SOLUONGUpdated ? string.Format(",HM_SOLUONG = {0}", ":HM_SOLUONG") : string.Empty).Append(m_HM_MABIEUTHUE_XNKUpdated ? string.Format(",HM_MABIEUTHUE_XNK = {0}", ":HM_MABIEUTHUE_XNK") : string.Empty).Append(m_HM_MABIEUTHUE_TBDBUpdated ? string.Format(",HM_MABIEUTHUE_TBDB = {0}", ":HM_MABIEUTHUE_TBDB") : string.Empty).Append(m_HM_MABIEUTHUE_MTUpdated ? string.Format(",HM_MABIEUTHUE_MT = {0}", ":HM_MABIEUTHUE_MT") : string.Empty).Append(m_HM_MABIEUTHUE_VATUpdated ? string.Format(",HM_MABIEUTHUE_VAT = {0}", ":HM_MABIEUTHUE_VAT") : string.Empty).Append(m_HM_GHICHUUpdated ? string.Format(",HM_GHICHU = {0}", ":HM_GHICHU") : string.Empty).Append(m_HM_TEN_ENUpdated ? string.Format(",HM_TEN_EN = {0}", ":HM_TEN_EN") : string.Empty);
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
				sbSQL.AppendFormat("[HM_MA] = {0}", "@HM_MA").AppendFormat(",[HM_TEN] = {0}", "@HM_TEN").AppendFormat(",[HM_DVT] = {0}", "@HM_DVT").AppendFormat(",[HM_HS] = {0}", "@HM_HS").AppendFormat(",[HM_SOLUONG] = {0}", "@HM_SOLUONG").AppendFormat(",[HM_MABIEUTHUE_XNK] = {0}", "@HM_MABIEUTHUE_XNK").AppendFormat(",[HM_MABIEUTHUE_TBDB] = {0}", "@HM_MABIEUTHUE_TBDB").AppendFormat(",[HM_MABIEUTHUE_MT] = {0}", "@HM_MABIEUTHUE_MT").AppendFormat(",[HM_MABIEUTHUE_VAT] = {0}", "@HM_MABIEUTHUE_VAT").AppendFormat(",[HM_GHICHU] = {0}", "@HM_GHICHU").AppendFormat(",[HM_TEN_EN] = {0}", "@HM_TEN_EN");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("HM_MA = {0}", ":HM_MA").AppendFormat(",HM_TEN = {0}", ":HM_TEN").AppendFormat(",HM_DVT = {0}", ":HM_DVT").AppendFormat(",HM_HS = {0}", ":HM_HS").AppendFormat(",HM_SOLUONG = {0}", ":HM_SOLUONG").AppendFormat(",HM_MABIEUTHUE_XNK = {0}", ":HM_MABIEUTHUE_XNK").AppendFormat(",HM_MABIEUTHUE_TBDB = {0}", ":HM_MABIEUTHUE_TBDB").AppendFormat(",HM_MABIEUTHUE_MT = {0}", ":HM_MABIEUTHUE_MT").AppendFormat(",HM_MABIEUTHUE_VAT = {0}", ":HM_MABIEUTHUE_VAT").AppendFormat(",HM_GHICHU = {0}", ":HM_GHICHU").AppendFormat(",HM_TEN_EN = {0}", ":HM_TEN_EN");
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
			return clsDAL.DeleteString("GC_HDGC_HANGMAU", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[HM_HDID] = {0}", "@HM_HDID").AppendFormat(" AND [HM_ID] = {0}", "@HM_ID");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("HM_HDID = {0}", ":HM_HDID").AppendFormat(" AND HM_ID = {0}", ":HM_ID");
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
			paramList.Add(clsDAL.CreateParameter("HM_HDID", "Integer", clsDAL.ToDBParam(HM_HDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HM_ID", "Integer", clsDAL.ToDBParam(HM_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("HM_MA", "WChar", clsDAL.ToDBParam(HM_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HM_TEN", "WChar", clsDAL.ToDBParam(HM_TEN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HM_DVT", "WChar", clsDAL.ToDBParam(HM_DVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HM_HS", "WChar", clsDAL.ToDBParam(HM_HS, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HM_SOLUONG", "Numeric", clsDAL.ToDBParam(HM_SOLUONG, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HM_MABIEUTHUE_XNK", "WChar", clsDAL.ToDBParam(HM_MABIEUTHUE_XNK, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HM_MABIEUTHUE_TBDB", "WChar", clsDAL.ToDBParam(HM_MABIEUTHUE_TBDB, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HM_MABIEUTHUE_MT", "WChar", clsDAL.ToDBParam(HM_MABIEUTHUE_MT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HM_MABIEUTHUE_VAT", "WChar", clsDAL.ToDBParam(HM_MABIEUTHUE_VAT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HM_GHICHU", "WChar", clsDAL.ToDBParam(HM_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HM_TEN_EN", "WChar", clsDAL.ToDBParam(HM_TEN_EN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HM_HDID", "Integer", clsDAL.ToDBParam(HM_HDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HM_ID", "Integer", clsDAL.ToDBParam(HM_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("HM_HDID", "Integer", clsDAL.ToDBParam(HM_HDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HM_ID", "Integer", clsDAL.ToDBParam(HM_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HM_MA", "WChar", clsDAL.ToDBParam(HM_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HM_TEN", "WChar", clsDAL.ToDBParam(HM_TEN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HM_DVT", "WChar", clsDAL.ToDBParam(HM_DVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HM_HS", "WChar", clsDAL.ToDBParam(HM_HS, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HM_SOLUONG", "Numeric", clsDAL.ToDBParam(HM_SOLUONG, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HM_MABIEUTHUE_XNK", "WChar", clsDAL.ToDBParam(HM_MABIEUTHUE_XNK, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HM_MABIEUTHUE_TBDB", "WChar", clsDAL.ToDBParam(HM_MABIEUTHUE_TBDB, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HM_MABIEUTHUE_MT", "WChar", clsDAL.ToDBParam(HM_MABIEUTHUE_MT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HM_MABIEUTHUE_VAT", "WChar", clsDAL.ToDBParam(HM_MABIEUTHUE_VAT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HM_GHICHU", "WChar", clsDAL.ToDBParam(HM_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HM_TEN_EN", "WChar", clsDAL.ToDBParam(HM_TEN_EN, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}