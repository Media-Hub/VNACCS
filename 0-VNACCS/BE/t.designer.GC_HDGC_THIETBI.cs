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
	/// Generated Class for Table : GC_HDGC_THIETBI.
	/// </summary>
	public partial class GC_HDGC_THIETBI : TableBase
	{
		public GC_HDGC_THIETBI() : base(){}

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
		private int m_TB_HDID;
		/// <summary>
		/// TB_HDID.
		/// </summary>
		public int TB_HDID
		{
			get
			{
				return m_TB_HDID;
			}
			set
			{
				if ((this.m_TB_HDID != value))
				{
					this.SendPropertyChanging("TB_HDID");
					this.m_TB_HDID = value;
					this.SendPropertyChanged("TB_HDID");
				}
			}
		}

		private int m_TB_ID;
		/// <summary>
		/// TB_ID.
		/// </summary>
		public int TB_ID
		{
			get
			{
				return m_TB_ID;
			}
			set
			{
				if ((this.m_TB_ID != value))
				{
					this.SendPropertyChanging("TB_ID");
					this.m_TB_ID = value;
					this.SendPropertyChanged("TB_ID");
				}
			}
		}

		private string m_TB_MA;
		private bool m_TB_MAUpdated = false;
		/// <summary>
		/// TB_MA.
		/// </summary>
		public string TB_MA
		{
			get
			{
				return m_TB_MA;
			}
			set
			{
				if ((this.m_TB_MA != value))
				{
					this.SendPropertyChanging("TB_MA");
					this.m_TB_MA = value;
					this.SendPropertyChanged("TB_MA");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TB_MAUpdated = true;
				}
			}
		}

		private string m_TB_TEN;
		private bool m_TB_TENUpdated = false;
		/// <summary>
		/// TB_TEN.
		/// </summary>
		public string TB_TEN
		{
			get
			{
				return m_TB_TEN;
			}
			set
			{
				if ((this.m_TB_TEN != value))
				{
					this.SendPropertyChanging("TB_TEN");
					this.m_TB_TEN = value;
					this.SendPropertyChanged("TB_TEN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TB_TENUpdated = true;
				}
			}
		}

		private string m_TB_DVT;
		private bool m_TB_DVTUpdated = false;
		/// <summary>
		/// TB_DVT.
		/// </summary>
		public string TB_DVT
		{
			get
			{
				return m_TB_DVT;
			}
			set
			{
				if ((this.m_TB_DVT != value))
				{
					this.SendPropertyChanging("TB_DVT");
					this.m_TB_DVT = value;
					this.SendPropertyChanged("TB_DVT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TB_DVTUpdated = true;
				}
			}
		}

		private string m_TB_HS;
		private bool m_TB_HSUpdated = false;
		/// <summary>
		/// TB_HS.
		/// </summary>
		public string TB_HS
		{
			get
			{
				return m_TB_HS;
			}
			set
			{
				if ((this.m_TB_HS != value))
				{
					this.SendPropertyChanging("TB_HS");
					this.m_TB_HS = value;
					this.SendPropertyChanged("TB_HS");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TB_HSUpdated = true;
				}
			}
		}

		private string m_TB_NUOCXX;
		private bool m_TB_NUOCXXUpdated = false;
		/// <summary>
		/// TB_NUOCXX.
		/// </summary>
		public string TB_NUOCXX
		{
			get
			{
				return m_TB_NUOCXX;
			}
			set
			{
				if ((this.m_TB_NUOCXX != value))
				{
					this.SendPropertyChanging("TB_NUOCXX");
					this.m_TB_NUOCXX = value;
					this.SendPropertyChanged("TB_NUOCXX");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TB_NUOCXXUpdated = true;
				}
			}
		}

		private int? m_TB_TINHTRANG = 0;
		private bool m_TB_TINHTRANGUpdated = false;
		/// <summary>
		/// TB_TINHTRANG.
		/// </summary>
		public int? TB_TINHTRANG
		{
			get
			{
				return m_TB_TINHTRANG;
			}
			set
			{
				if ((this.m_TB_TINHTRANG != value))
				{
					this.SendPropertyChanging("TB_TINHTRANG");
					this.m_TB_TINHTRANG = value;
					this.SendPropertyChanged("TB_TINHTRANG");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TB_TINHTRANGUpdated = true;
				}
			}
		}

		private decimal? m_TB_SOLUONG = 0;
		private bool m_TB_SOLUONGUpdated = false;
		/// <summary>
		/// TB_SOLUONG.
		/// </summary>
		public decimal? TB_SOLUONG
		{
			get
			{
				return m_TB_SOLUONG;
			}
			set
			{
				if ((this.m_TB_SOLUONG != value))
				{
					this.SendPropertyChanging("TB_SOLUONG");
					this.m_TB_SOLUONG = value;
					this.SendPropertyChanged("TB_SOLUONG");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TB_SOLUONGUpdated = true;
				}
			}
		}

		private decimal? m_TB_DONGIA = 0;
		private bool m_TB_DONGIAUpdated = false;
		/// <summary>
		/// TB_DONGIA.
		/// </summary>
		public decimal? TB_DONGIA
		{
			get
			{
				return m_TB_DONGIA;
			}
			set
			{
				if ((this.m_TB_DONGIA != value))
				{
					this.SendPropertyChanging("TB_DONGIA");
					this.m_TB_DONGIA = value;
					this.SendPropertyChanged("TB_DONGIA");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TB_DONGIAUpdated = true;
				}
			}
		}

		private string m_TB_MANGTE;
		private bool m_TB_MANGTEUpdated = false;
		/// <summary>
		/// TB_MANGTE.
		/// </summary>
		public string TB_MANGTE
		{
			get
			{
				return m_TB_MANGTE;
			}
			set
			{
				if ((this.m_TB_MANGTE != value))
				{
					this.SendPropertyChanging("TB_MANGTE");
					this.m_TB_MANGTE = value;
					this.SendPropertyChanged("TB_MANGTE");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TB_MANGTEUpdated = true;
				}
			}
		}

		private string m_TB_MABIEUTHUE_XNK;
		private bool m_TB_MABIEUTHUE_XNKUpdated = false;
		/// <summary>
		/// TB_MABIEUTHUE_XNK.
		/// </summary>
		public string TB_MABIEUTHUE_XNK
		{
			get
			{
				return m_TB_MABIEUTHUE_XNK;
			}
			set
			{
				if ((this.m_TB_MABIEUTHUE_XNK != value))
				{
					this.SendPropertyChanging("TB_MABIEUTHUE_XNK");
					this.m_TB_MABIEUTHUE_XNK = value;
					this.SendPropertyChanged("TB_MABIEUTHUE_XNK");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TB_MABIEUTHUE_XNKUpdated = true;
				}
			}
		}

		private string m_TB_MABIEUTHUE_TBDB;
		private bool m_TB_MABIEUTHUE_TBDBUpdated = false;
		/// <summary>
		/// TB_MABIEUTHUE_TBDB.
		/// </summary>
		public string TB_MABIEUTHUE_TBDB
		{
			get
			{
				return m_TB_MABIEUTHUE_TBDB;
			}
			set
			{
				if ((this.m_TB_MABIEUTHUE_TBDB != value))
				{
					this.SendPropertyChanging("TB_MABIEUTHUE_TBDB");
					this.m_TB_MABIEUTHUE_TBDB = value;
					this.SendPropertyChanged("TB_MABIEUTHUE_TBDB");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TB_MABIEUTHUE_TBDBUpdated = true;
				}
			}
		}

		private string m_TB_MABIEUTHUE_MT;
		private bool m_TB_MABIEUTHUE_MTUpdated = false;
		/// <summary>
		/// TB_MABIEUTHUE_MT.
		/// </summary>
		public string TB_MABIEUTHUE_MT
		{
			get
			{
				return m_TB_MABIEUTHUE_MT;
			}
			set
			{
				if ((this.m_TB_MABIEUTHUE_MT != value))
				{
					this.SendPropertyChanging("TB_MABIEUTHUE_MT");
					this.m_TB_MABIEUTHUE_MT = value;
					this.SendPropertyChanged("TB_MABIEUTHUE_MT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TB_MABIEUTHUE_MTUpdated = true;
				}
			}
		}

		private string m_TB_MABIEUTHUE_VAT;
		private bool m_TB_MABIEUTHUE_VATUpdated = false;
		/// <summary>
		/// TB_MABIEUTHUE_VAT.
		/// </summary>
		public string TB_MABIEUTHUE_VAT
		{
			get
			{
				return m_TB_MABIEUTHUE_VAT;
			}
			set
			{
				if ((this.m_TB_MABIEUTHUE_VAT != value))
				{
					this.SendPropertyChanging("TB_MABIEUTHUE_VAT");
					this.m_TB_MABIEUTHUE_VAT = value;
					this.SendPropertyChanged("TB_MABIEUTHUE_VAT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TB_MABIEUTHUE_VATUpdated = true;
				}
			}
		}

		private string m_TB_GHICHU;
		private bool m_TB_GHICHUUpdated = false;
		/// <summary>
		/// TB_GHICHU.
		/// </summary>
		public string TB_GHICHU
		{
			get
			{
				return m_TB_GHICHU;
			}
			set
			{
				if ((this.m_TB_GHICHU != value))
				{
					this.SendPropertyChanging("TB_GHICHU");
					this.m_TB_GHICHU = value;
					this.SendPropertyChanged("TB_GHICHU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TB_GHICHUUpdated = true;
				}
			}
		}

		private string m_TB_TEN_EN;
		private bool m_TB_TEN_ENUpdated = false;
		/// <summary>
		/// TB_TEN_EN.
		/// </summary>
		public string TB_TEN_EN
		{
			get
			{
				return m_TB_TEN_EN;
			}
			set
			{
				if ((this.m_TB_TEN_EN != value))
				{
					this.SendPropertyChanging("TB_TEN_EN");
					this.m_TB_TEN_EN = value;
					this.SendPropertyChanged("TB_TEN_EN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TB_TEN_ENUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM GC_HDGC_THIETBI " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[TB_HDID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TB_ID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TB_MA]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TB_TEN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TB_DVT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TB_HS]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TB_NUOCXX]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TB_TINHTRANG]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TB_SOLUONG]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TB_DONGIA]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TB_MANGTE]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TB_MABIEUTHUE_XNK]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TB_MABIEUTHUE_TBDB]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TB_MABIEUTHUE_MT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TB_MABIEUTHUE_VAT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TB_GHICHU]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TB_TEN_EN]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("TB_HDID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TB_ID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TB_MA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TB_TEN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TB_DVT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TB_HS", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TB_NUOCXX", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TB_TINHTRANG", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TB_SOLUONG", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TB_DONGIA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TB_MANGTE", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TB_MABIEUTHUE_XNK", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TB_MABIEUTHUE_TBDB", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TB_MABIEUTHUE_MT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TB_MABIEUTHUE_VAT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TB_GHICHU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TB_TEN_EN", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO GC_HDGC_THIETBI ([TB_HDID], [TB_ID], [TB_MA], [TB_TEN], [TB_DVT], [TB_HS], [TB_NUOCXX], [TB_TINHTRANG], [TB_SOLUONG], [TB_DONGIA], [TB_MANGTE], [TB_MABIEUTHUE_XNK], [TB_MABIEUTHUE_TBDB], [TB_MABIEUTHUE_MT], [TB_MABIEUTHUE_VAT], [TB_GHICHU], [TB_TEN_EN]) VALUES(").Append("@TB_HDID").Append(",").Append("@TB_ID").Append(",").Append("@TB_MA").Append(",").Append("@TB_TEN").Append(",").Append("@TB_DVT").Append(",").Append("@TB_HS").Append(",").Append("@TB_NUOCXX").Append(",").Append("@TB_TINHTRANG").Append(",").Append("@TB_SOLUONG").Append(",").Append("@TB_DONGIA").Append(",").Append("@TB_MANGTE").Append(",").Append("@TB_MABIEUTHUE_XNK").Append(",").Append("@TB_MABIEUTHUE_TBDB").Append(",").Append("@TB_MABIEUTHUE_MT").Append(",").Append("@TB_MABIEUTHUE_VAT").Append(",").Append("@TB_GHICHU").Append(",").Append("@TB_TEN_EN").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO GC_HDGC_THIETBI (TB_HDID, TB_ID, TB_MA, TB_TEN, TB_DVT, TB_HS, TB_NUOCXX, TB_TINHTRANG, TB_SOLUONG, TB_DONGIA, TB_MANGTE, TB_MABIEUTHUE_XNK, TB_MABIEUTHUE_TBDB, TB_MABIEUTHUE_MT, TB_MABIEUTHUE_VAT, TB_GHICHU, TB_TEN_EN) VALUES(").Append(":TB_HDID").Append(",").Append(":TB_ID").Append(",").Append(":TB_MA").Append(",").Append(":TB_TEN").Append(",").Append(":TB_DVT").Append(",").Append(":TB_HS").Append(",").Append(":TB_NUOCXX").Append(",").Append(":TB_TINHTRANG").Append(",").Append(":TB_SOLUONG").Append(",").Append(":TB_DONGIA").Append(",").Append(":TB_MANGTE").Append(",").Append(":TB_MABIEUTHUE_XNK").Append(",").Append(":TB_MABIEUTHUE_TBDB").Append(",").Append(":TB_MABIEUTHUE_MT").Append(",").Append(":TB_MABIEUTHUE_VAT").Append(",").Append(":TB_GHICHU").Append(",").Append(":TB_TEN_EN").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE GC_HDGC_THIETBI SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_TB_MAUpdated ? string.Format(",[TB_MA] = {0}", "@TB_MA") : string.Empty).Append(m_TB_TENUpdated ? string.Format(",[TB_TEN] = {0}", "@TB_TEN") : string.Empty).Append(m_TB_DVTUpdated ? string.Format(",[TB_DVT] = {0}", "@TB_DVT") : string.Empty).Append(m_TB_HSUpdated ? string.Format(",[TB_HS] = {0}", "@TB_HS") : string.Empty).Append(m_TB_NUOCXXUpdated ? string.Format(",[TB_NUOCXX] = {0}", "@TB_NUOCXX") : string.Empty).Append(m_TB_TINHTRANGUpdated ? string.Format(",[TB_TINHTRANG] = {0}", "@TB_TINHTRANG") : string.Empty).Append(m_TB_SOLUONGUpdated ? string.Format(",[TB_SOLUONG] = {0}", "@TB_SOLUONG") : string.Empty).Append(m_TB_DONGIAUpdated ? string.Format(",[TB_DONGIA] = {0}", "@TB_DONGIA") : string.Empty).Append(m_TB_MANGTEUpdated ? string.Format(",[TB_MANGTE] = {0}", "@TB_MANGTE") : string.Empty).Append(m_TB_MABIEUTHUE_XNKUpdated ? string.Format(",[TB_MABIEUTHUE_XNK] = {0}", "@TB_MABIEUTHUE_XNK") : string.Empty).Append(m_TB_MABIEUTHUE_TBDBUpdated ? string.Format(",[TB_MABIEUTHUE_TBDB] = {0}", "@TB_MABIEUTHUE_TBDB") : string.Empty).Append(m_TB_MABIEUTHUE_MTUpdated ? string.Format(",[TB_MABIEUTHUE_MT] = {0}", "@TB_MABIEUTHUE_MT") : string.Empty).Append(m_TB_MABIEUTHUE_VATUpdated ? string.Format(",[TB_MABIEUTHUE_VAT] = {0}", "@TB_MABIEUTHUE_VAT") : string.Empty).Append(m_TB_GHICHUUpdated ? string.Format(",[TB_GHICHU] = {0}", "@TB_GHICHU") : string.Empty).Append(m_TB_TEN_ENUpdated ? string.Format(",[TB_TEN_EN] = {0}", "@TB_TEN_EN") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_TB_MAUpdated ? string.Format(",TB_MA = {0}", ":TB_MA") : string.Empty).Append(m_TB_TENUpdated ? string.Format(",TB_TEN = {0}", ":TB_TEN") : string.Empty).Append(m_TB_DVTUpdated ? string.Format(",TB_DVT = {0}", ":TB_DVT") : string.Empty).Append(m_TB_HSUpdated ? string.Format(",TB_HS = {0}", ":TB_HS") : string.Empty).Append(m_TB_NUOCXXUpdated ? string.Format(",TB_NUOCXX = {0}", ":TB_NUOCXX") : string.Empty).Append(m_TB_TINHTRANGUpdated ? string.Format(",TB_TINHTRANG = {0}", ":TB_TINHTRANG") : string.Empty).Append(m_TB_SOLUONGUpdated ? string.Format(",TB_SOLUONG = {0}", ":TB_SOLUONG") : string.Empty).Append(m_TB_DONGIAUpdated ? string.Format(",TB_DONGIA = {0}", ":TB_DONGIA") : string.Empty).Append(m_TB_MANGTEUpdated ? string.Format(",TB_MANGTE = {0}", ":TB_MANGTE") : string.Empty).Append(m_TB_MABIEUTHUE_XNKUpdated ? string.Format(",TB_MABIEUTHUE_XNK = {0}", ":TB_MABIEUTHUE_XNK") : string.Empty).Append(m_TB_MABIEUTHUE_TBDBUpdated ? string.Format(",TB_MABIEUTHUE_TBDB = {0}", ":TB_MABIEUTHUE_TBDB") : string.Empty).Append(m_TB_MABIEUTHUE_MTUpdated ? string.Format(",TB_MABIEUTHUE_MT = {0}", ":TB_MABIEUTHUE_MT") : string.Empty).Append(m_TB_MABIEUTHUE_VATUpdated ? string.Format(",TB_MABIEUTHUE_VAT = {0}", ":TB_MABIEUTHUE_VAT") : string.Empty).Append(m_TB_GHICHUUpdated ? string.Format(",TB_GHICHU = {0}", ":TB_GHICHU") : string.Empty).Append(m_TB_TEN_ENUpdated ? string.Format(",TB_TEN_EN = {0}", ":TB_TEN_EN") : string.Empty);
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
				sbSQL.AppendFormat("[TB_MA] = {0}", "@TB_MA").AppendFormat(",[TB_TEN] = {0}", "@TB_TEN").AppendFormat(",[TB_DVT] = {0}", "@TB_DVT").AppendFormat(",[TB_HS] = {0}", "@TB_HS").AppendFormat(",[TB_NUOCXX] = {0}", "@TB_NUOCXX").AppendFormat(",[TB_TINHTRANG] = {0}", "@TB_TINHTRANG").AppendFormat(",[TB_SOLUONG] = {0}", "@TB_SOLUONG").AppendFormat(",[TB_DONGIA] = {0}", "@TB_DONGIA").AppendFormat(",[TB_MANGTE] = {0}", "@TB_MANGTE").AppendFormat(",[TB_MABIEUTHUE_XNK] = {0}", "@TB_MABIEUTHUE_XNK").AppendFormat(",[TB_MABIEUTHUE_TBDB] = {0}", "@TB_MABIEUTHUE_TBDB").AppendFormat(",[TB_MABIEUTHUE_MT] = {0}", "@TB_MABIEUTHUE_MT").AppendFormat(",[TB_MABIEUTHUE_VAT] = {0}", "@TB_MABIEUTHUE_VAT").AppendFormat(",[TB_GHICHU] = {0}", "@TB_GHICHU").AppendFormat(",[TB_TEN_EN] = {0}", "@TB_TEN_EN");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("TB_MA = {0}", ":TB_MA").AppendFormat(",TB_TEN = {0}", ":TB_TEN").AppendFormat(",TB_DVT = {0}", ":TB_DVT").AppendFormat(",TB_HS = {0}", ":TB_HS").AppendFormat(",TB_NUOCXX = {0}", ":TB_NUOCXX").AppendFormat(",TB_TINHTRANG = {0}", ":TB_TINHTRANG").AppendFormat(",TB_SOLUONG = {0}", ":TB_SOLUONG").AppendFormat(",TB_DONGIA = {0}", ":TB_DONGIA").AppendFormat(",TB_MANGTE = {0}", ":TB_MANGTE").AppendFormat(",TB_MABIEUTHUE_XNK = {0}", ":TB_MABIEUTHUE_XNK").AppendFormat(",TB_MABIEUTHUE_TBDB = {0}", ":TB_MABIEUTHUE_TBDB").AppendFormat(",TB_MABIEUTHUE_MT = {0}", ":TB_MABIEUTHUE_MT").AppendFormat(",TB_MABIEUTHUE_VAT = {0}", ":TB_MABIEUTHUE_VAT").AppendFormat(",TB_GHICHU = {0}", ":TB_GHICHU").AppendFormat(",TB_TEN_EN = {0}", ":TB_TEN_EN");
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
			return clsDAL.DeleteString("GC_HDGC_THIETBI", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[TB_HDID] = {0}", "@TB_HDID").AppendFormat(" AND [TB_ID] = {0}", "@TB_ID");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("TB_HDID = {0}", ":TB_HDID").AppendFormat(" AND TB_ID = {0}", ":TB_ID");
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
			paramList.Add(clsDAL.CreateParameter("TB_HDID", "Integer", clsDAL.ToDBParam(TB_HDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_ID", "Integer", clsDAL.ToDBParam(TB_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("TB_MA", "WChar", clsDAL.ToDBParam(TB_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_TEN", "WChar", clsDAL.ToDBParam(TB_TEN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_DVT", "WChar", clsDAL.ToDBParam(TB_DVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_HS", "WChar", clsDAL.ToDBParam(TB_HS, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_NUOCXX", "WChar", clsDAL.ToDBParam(TB_NUOCXX, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_TINHTRANG", "Single", clsDAL.ToDBParam(TB_TINHTRANG, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_SOLUONG", "Numeric", clsDAL.ToDBParam(TB_SOLUONG, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_DONGIA", "Numeric", clsDAL.ToDBParam(TB_DONGIA, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_MANGTE", "WChar", clsDAL.ToDBParam(TB_MANGTE, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_MABIEUTHUE_XNK", "WChar", clsDAL.ToDBParam(TB_MABIEUTHUE_XNK, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_MABIEUTHUE_TBDB", "WChar", clsDAL.ToDBParam(TB_MABIEUTHUE_TBDB, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_MABIEUTHUE_MT", "WChar", clsDAL.ToDBParam(TB_MABIEUTHUE_MT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_MABIEUTHUE_VAT", "WChar", clsDAL.ToDBParam(TB_MABIEUTHUE_VAT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_GHICHU", "WChar", clsDAL.ToDBParam(TB_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_TEN_EN", "WChar", clsDAL.ToDBParam(TB_TEN_EN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_HDID", "Integer", clsDAL.ToDBParam(TB_HDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_ID", "Integer", clsDAL.ToDBParam(TB_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("TB_HDID", "Integer", clsDAL.ToDBParam(TB_HDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_ID", "Integer", clsDAL.ToDBParam(TB_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_MA", "WChar", clsDAL.ToDBParam(TB_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_TEN", "WChar", clsDAL.ToDBParam(TB_TEN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_DVT", "WChar", clsDAL.ToDBParam(TB_DVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_HS", "WChar", clsDAL.ToDBParam(TB_HS, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_NUOCXX", "WChar", clsDAL.ToDBParam(TB_NUOCXX, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_TINHTRANG", "Single", clsDAL.ToDBParam(TB_TINHTRANG, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_SOLUONG", "Numeric", clsDAL.ToDBParam(TB_SOLUONG, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_DONGIA", "Numeric", clsDAL.ToDBParam(TB_DONGIA, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_MANGTE", "WChar", clsDAL.ToDBParam(TB_MANGTE, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_MABIEUTHUE_XNK", "WChar", clsDAL.ToDBParam(TB_MABIEUTHUE_XNK, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_MABIEUTHUE_TBDB", "WChar", clsDAL.ToDBParam(TB_MABIEUTHUE_TBDB, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_MABIEUTHUE_MT", "WChar", clsDAL.ToDBParam(TB_MABIEUTHUE_MT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_MABIEUTHUE_VAT", "WChar", clsDAL.ToDBParam(TB_MABIEUTHUE_VAT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_GHICHU", "WChar", clsDAL.ToDBParam(TB_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TB_TEN_EN", "WChar", clsDAL.ToDBParam(TB_TEN_EN, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}