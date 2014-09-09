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
	/// Generated Class for Table : GC_HDGC_NGUYENLIEU.
	/// </summary>
	public partial class GC_HDGC_NGUYENLIEU : TableBase
	{
		public GC_HDGC_NGUYENLIEU() : base(){}

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
		private int m_NL_HDID;
		/// <summary>
		/// NL_HDID.
		/// </summary>
		public int NL_HDID
		{
			get
			{
				return m_NL_HDID;
			}
			set
			{
				if ((this.m_NL_HDID != value))
				{
					this.SendPropertyChanging("NL_HDID");
					this.m_NL_HDID = value;
					this.SendPropertyChanged("NL_HDID");
				}
			}
		}

		private int m_NL_ID;
		/// <summary>
		/// NL_ID.
		/// </summary>
		public int NL_ID
		{
			get
			{
				return m_NL_ID;
			}
			set
			{
				if ((this.m_NL_ID != value))
				{
					this.SendPropertyChanging("NL_ID");
					this.m_NL_ID = value;
					this.SendPropertyChanged("NL_ID");
				}
			}
		}

		private string m_NL_MA;
		private bool m_NL_MAUpdated = false;
		/// <summary>
		/// NL_MA.
		/// </summary>
		public string NL_MA
		{
			get
			{
				return m_NL_MA;
			}
			set
			{
				if ((this.m_NL_MA != value))
				{
					this.SendPropertyChanging("NL_MA");
					this.m_NL_MA = value;
					this.SendPropertyChanged("NL_MA");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NL_MAUpdated = true;
				}
			}
		}

		private string m_NL_TEN;
		private bool m_NL_TENUpdated = false;
		/// <summary>
		/// NL_TEN.
		/// </summary>
		public string NL_TEN
		{
			get
			{
				return m_NL_TEN;
			}
			set
			{
				if ((this.m_NL_TEN != value))
				{
					this.SendPropertyChanging("NL_TEN");
					this.m_NL_TEN = value;
					this.SendPropertyChanged("NL_TEN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NL_TENUpdated = true;
				}
			}
		}

		private string m_NL_DVT;
		private bool m_NL_DVTUpdated = false;
		/// <summary>
		/// NL_DVT.
		/// </summary>
		public string NL_DVT
		{
			get
			{
				return m_NL_DVT;
			}
			set
			{
				if ((this.m_NL_DVT != value))
				{
					this.SendPropertyChanging("NL_DVT");
					this.m_NL_DVT = value;
					this.SendPropertyChanged("NL_DVT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NL_DVTUpdated = true;
				}
			}
		}

		private string m_NL_HS;
		private bool m_NL_HSUpdated = false;
		/// <summary>
		/// NL_HS.
		/// </summary>
		public string NL_HS
		{
			get
			{
				return m_NL_HS;
			}
			set
			{
				if ((this.m_NL_HS != value))
				{
					this.SendPropertyChanging("NL_HS");
					this.m_NL_HS = value;
					this.SendPropertyChanged("NL_HS");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NL_HSUpdated = true;
				}
			}
		}

		private decimal? m_NL_SOLUONG = 0;
		private bool m_NL_SOLUONGUpdated = false;
		/// <summary>
		/// NL_SOLUONG.
		/// </summary>
		public decimal? NL_SOLUONG
		{
			get
			{
				return m_NL_SOLUONG;
			}
			set
			{
				if ((this.m_NL_SOLUONG != value))
				{
					this.SendPropertyChanging("NL_SOLUONG");
					this.m_NL_SOLUONG = value;
					this.SendPropertyChanged("NL_SOLUONG");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NL_SOLUONGUpdated = true;
				}
			}
		}

		private string m_NL_NGUONNGUYENLIEU = "Nhập khẩu";
		private bool m_NL_NGUONNGUYENLIEUUpdated = false;
		/// <summary>
		/// NL_NGUONNGUYENLIEU.
		/// </summary>
		public string NL_NGUONNGUYENLIEU
		{
			get
			{
				return m_NL_NGUONNGUYENLIEU;
			}
			set
			{
				if ((this.m_NL_NGUONNGUYENLIEU != value))
				{
					this.SendPropertyChanging("NL_NGUONNGUYENLIEU");
					this.m_NL_NGUONNGUYENLIEU = value;
					this.SendPropertyChanged("NL_NGUONNGUYENLIEU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NL_NGUONNGUYENLIEUUpdated = true;
				}
			}
		}

		private decimal? m_NL_DONGIA = 0;
		private bool m_NL_DONGIAUpdated = false;
		/// <summary>
		/// NL_DONGIA.
		/// </summary>
		public decimal? NL_DONGIA
		{
			get
			{
				return m_NL_DONGIA;
			}
			set
			{
				if ((this.m_NL_DONGIA != value))
				{
					this.SendPropertyChanging("NL_DONGIA");
					this.m_NL_DONGIA = value;
					this.SendPropertyChanged("NL_DONGIA");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NL_DONGIAUpdated = true;
				}
			}
		}

		private string m_NL_MABIEUTHUE_XNK;
		private bool m_NL_MABIEUTHUE_XNKUpdated = false;
		/// <summary>
		/// NL_MABIEUTHUE_XNK.
		/// </summary>
		public string NL_MABIEUTHUE_XNK
		{
			get
			{
				return m_NL_MABIEUTHUE_XNK;
			}
			set
			{
				if ((this.m_NL_MABIEUTHUE_XNK != value))
				{
					this.SendPropertyChanging("NL_MABIEUTHUE_XNK");
					this.m_NL_MABIEUTHUE_XNK = value;
					this.SendPropertyChanged("NL_MABIEUTHUE_XNK");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NL_MABIEUTHUE_XNKUpdated = true;
				}
			}
		}

		private string m_NL_MABIEUTHUE_TBDB;
		private bool m_NL_MABIEUTHUE_TBDBUpdated = false;
		/// <summary>
		/// NL_MABIEUTHUE_TBDB.
		/// </summary>
		public string NL_MABIEUTHUE_TBDB
		{
			get
			{
				return m_NL_MABIEUTHUE_TBDB;
			}
			set
			{
				if ((this.m_NL_MABIEUTHUE_TBDB != value))
				{
					this.SendPropertyChanging("NL_MABIEUTHUE_TBDB");
					this.m_NL_MABIEUTHUE_TBDB = value;
					this.SendPropertyChanged("NL_MABIEUTHUE_TBDB");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NL_MABIEUTHUE_TBDBUpdated = true;
				}
			}
		}

		private string m_NL_MABIEUTHUE_MT;
		private bool m_NL_MABIEUTHUE_MTUpdated = false;
		/// <summary>
		/// NL_MABIEUTHUE_MT.
		/// </summary>
		public string NL_MABIEUTHUE_MT
		{
			get
			{
				return m_NL_MABIEUTHUE_MT;
			}
			set
			{
				if ((this.m_NL_MABIEUTHUE_MT != value))
				{
					this.SendPropertyChanging("NL_MABIEUTHUE_MT");
					this.m_NL_MABIEUTHUE_MT = value;
					this.SendPropertyChanged("NL_MABIEUTHUE_MT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NL_MABIEUTHUE_MTUpdated = true;
				}
			}
		}

		private string m_NL_MABIEUTHUE_VAT;
		private bool m_NL_MABIEUTHUE_VATUpdated = false;
		/// <summary>
		/// NL_MABIEUTHUE_VAT.
		/// </summary>
		public string NL_MABIEUTHUE_VAT
		{
			get
			{
				return m_NL_MABIEUTHUE_VAT;
			}
			set
			{
				if ((this.m_NL_MABIEUTHUE_VAT != value))
				{
					this.SendPropertyChanging("NL_MABIEUTHUE_VAT");
					this.m_NL_MABIEUTHUE_VAT = value;
					this.SendPropertyChanged("NL_MABIEUTHUE_VAT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NL_MABIEUTHUE_VATUpdated = true;
				}
			}
		}

		private string m_NL_GHICHU;
		private bool m_NL_GHICHUUpdated = false;
		/// <summary>
		/// NL_GHICHU.
		/// </summary>
		public string NL_GHICHU
		{
			get
			{
				return m_NL_GHICHU;
			}
			set
			{
				if ((this.m_NL_GHICHU != value))
				{
					this.SendPropertyChanging("NL_GHICHU");
					this.m_NL_GHICHU = value;
					this.SendPropertyChanged("NL_GHICHU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NL_GHICHUUpdated = true;
				}
			}
		}

		private string m_NL_TEN_EN;
		private bool m_NL_TEN_ENUpdated = false;
		/// <summary>
		/// NL_TEN_EN.
		/// </summary>
		public string NL_TEN_EN
		{
			get
			{
				return m_NL_TEN_EN;
			}
			set
			{
				if ((this.m_NL_TEN_EN != value))
				{
					this.SendPropertyChanging("NL_TEN_EN");
					this.m_NL_TEN_EN = value;
					this.SendPropertyChanged("NL_TEN_EN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NL_TEN_ENUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM GC_HDGC_NGUYENLIEU " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[NL_HDID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[NL_ID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[NL_MA]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[NL_TEN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[NL_DVT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[NL_HS]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[NL_SOLUONG]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[NL_NGUONNGUYENLIEU]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[NL_DONGIA]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[NL_MABIEUTHUE_XNK]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[NL_MABIEUTHUE_TBDB]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[NL_MABIEUTHUE_MT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[NL_MABIEUTHUE_VAT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[NL_GHICHU]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[NL_TEN_EN]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("NL_HDID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NL_ID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NL_MA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NL_TEN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NL_DVT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NL_HS", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NL_SOLUONG", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NL_NGUONNGUYENLIEU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NL_DONGIA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NL_MABIEUTHUE_XNK", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NL_MABIEUTHUE_TBDB", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NL_MABIEUTHUE_MT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NL_MABIEUTHUE_VAT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NL_GHICHU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NL_TEN_EN", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO GC_HDGC_NGUYENLIEU ([NL_HDID], [NL_ID], [NL_MA], [NL_TEN], [NL_DVT], [NL_HS], [NL_SOLUONG], [NL_NGUONNGUYENLIEU], [NL_DONGIA], [NL_MABIEUTHUE_XNK], [NL_MABIEUTHUE_TBDB], [NL_MABIEUTHUE_MT], [NL_MABIEUTHUE_VAT], [NL_GHICHU], [NL_TEN_EN]) VALUES(").Append("@NL_HDID").Append(",").Append("@NL_ID").Append(",").Append("@NL_MA").Append(",").Append("@NL_TEN").Append(",").Append("@NL_DVT").Append(",").Append("@NL_HS").Append(",").Append("@NL_SOLUONG").Append(",").Append("@NL_NGUONNGUYENLIEU").Append(",").Append("@NL_DONGIA").Append(",").Append("@NL_MABIEUTHUE_XNK").Append(",").Append("@NL_MABIEUTHUE_TBDB").Append(",").Append("@NL_MABIEUTHUE_MT").Append(",").Append("@NL_MABIEUTHUE_VAT").Append(",").Append("@NL_GHICHU").Append(",").Append("@NL_TEN_EN").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO GC_HDGC_NGUYENLIEU (NL_HDID, NL_ID, NL_MA, NL_TEN, NL_DVT, NL_HS, NL_SOLUONG, NL_NGUONNGUYENLIEU, NL_DONGIA, NL_MABIEUTHUE_XNK, NL_MABIEUTHUE_TBDB, NL_MABIEUTHUE_MT, NL_MABIEUTHUE_VAT, NL_GHICHU, NL_TEN_EN) VALUES(").Append(":NL_HDID").Append(",").Append(":NL_ID").Append(",").Append(":NL_MA").Append(",").Append(":NL_TEN").Append(",").Append(":NL_DVT").Append(",").Append(":NL_HS").Append(",").Append(":NL_SOLUONG").Append(",").Append(":NL_NGUONNGUYENLIEU").Append(",").Append(":NL_DONGIA").Append(",").Append(":NL_MABIEUTHUE_XNK").Append(",").Append(":NL_MABIEUTHUE_TBDB").Append(",").Append(":NL_MABIEUTHUE_MT").Append(",").Append(":NL_MABIEUTHUE_VAT").Append(",").Append(":NL_GHICHU").Append(",").Append(":NL_TEN_EN").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE GC_HDGC_NGUYENLIEU SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_NL_MAUpdated ? string.Format(",[NL_MA] = {0}", "@NL_MA") : string.Empty).Append(m_NL_TENUpdated ? string.Format(",[NL_TEN] = {0}", "@NL_TEN") : string.Empty).Append(m_NL_DVTUpdated ? string.Format(",[NL_DVT] = {0}", "@NL_DVT") : string.Empty).Append(m_NL_HSUpdated ? string.Format(",[NL_HS] = {0}", "@NL_HS") : string.Empty).Append(m_NL_SOLUONGUpdated ? string.Format(",[NL_SOLUONG] = {0}", "@NL_SOLUONG") : string.Empty).Append(m_NL_NGUONNGUYENLIEUUpdated ? string.Format(",[NL_NGUONNGUYENLIEU] = {0}", "@NL_NGUONNGUYENLIEU") : string.Empty).Append(m_NL_DONGIAUpdated ? string.Format(",[NL_DONGIA] = {0}", "@NL_DONGIA") : string.Empty).Append(m_NL_MABIEUTHUE_XNKUpdated ? string.Format(",[NL_MABIEUTHUE_XNK] = {0}", "@NL_MABIEUTHUE_XNK") : string.Empty).Append(m_NL_MABIEUTHUE_TBDBUpdated ? string.Format(",[NL_MABIEUTHUE_TBDB] = {0}", "@NL_MABIEUTHUE_TBDB") : string.Empty).Append(m_NL_MABIEUTHUE_MTUpdated ? string.Format(",[NL_MABIEUTHUE_MT] = {0}", "@NL_MABIEUTHUE_MT") : string.Empty).Append(m_NL_MABIEUTHUE_VATUpdated ? string.Format(",[NL_MABIEUTHUE_VAT] = {0}", "@NL_MABIEUTHUE_VAT") : string.Empty).Append(m_NL_GHICHUUpdated ? string.Format(",[NL_GHICHU] = {0}", "@NL_GHICHU") : string.Empty).Append(m_NL_TEN_ENUpdated ? string.Format(",[NL_TEN_EN] = {0}", "@NL_TEN_EN") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_NL_MAUpdated ? string.Format(",NL_MA = {0}", ":NL_MA") : string.Empty).Append(m_NL_TENUpdated ? string.Format(",NL_TEN = {0}", ":NL_TEN") : string.Empty).Append(m_NL_DVTUpdated ? string.Format(",NL_DVT = {0}", ":NL_DVT") : string.Empty).Append(m_NL_HSUpdated ? string.Format(",NL_HS = {0}", ":NL_HS") : string.Empty).Append(m_NL_SOLUONGUpdated ? string.Format(",NL_SOLUONG = {0}", ":NL_SOLUONG") : string.Empty).Append(m_NL_NGUONNGUYENLIEUUpdated ? string.Format(",NL_NGUONNGUYENLIEU = {0}", ":NL_NGUONNGUYENLIEU") : string.Empty).Append(m_NL_DONGIAUpdated ? string.Format(",NL_DONGIA = {0}", ":NL_DONGIA") : string.Empty).Append(m_NL_MABIEUTHUE_XNKUpdated ? string.Format(",NL_MABIEUTHUE_XNK = {0}", ":NL_MABIEUTHUE_XNK") : string.Empty).Append(m_NL_MABIEUTHUE_TBDBUpdated ? string.Format(",NL_MABIEUTHUE_TBDB = {0}", ":NL_MABIEUTHUE_TBDB") : string.Empty).Append(m_NL_MABIEUTHUE_MTUpdated ? string.Format(",NL_MABIEUTHUE_MT = {0}", ":NL_MABIEUTHUE_MT") : string.Empty).Append(m_NL_MABIEUTHUE_VATUpdated ? string.Format(",NL_MABIEUTHUE_VAT = {0}", ":NL_MABIEUTHUE_VAT") : string.Empty).Append(m_NL_GHICHUUpdated ? string.Format(",NL_GHICHU = {0}", ":NL_GHICHU") : string.Empty).Append(m_NL_TEN_ENUpdated ? string.Format(",NL_TEN_EN = {0}", ":NL_TEN_EN") : string.Empty);
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
				sbSQL.AppendFormat("[NL_MA] = {0}", "@NL_MA").AppendFormat(",[NL_TEN] = {0}", "@NL_TEN").AppendFormat(",[NL_DVT] = {0}", "@NL_DVT").AppendFormat(",[NL_HS] = {0}", "@NL_HS").AppendFormat(",[NL_SOLUONG] = {0}", "@NL_SOLUONG").AppendFormat(",[NL_NGUONNGUYENLIEU] = {0}", "@NL_NGUONNGUYENLIEU").AppendFormat(",[NL_DONGIA] = {0}", "@NL_DONGIA").AppendFormat(",[NL_MABIEUTHUE_XNK] = {0}", "@NL_MABIEUTHUE_XNK").AppendFormat(",[NL_MABIEUTHUE_TBDB] = {0}", "@NL_MABIEUTHUE_TBDB").AppendFormat(",[NL_MABIEUTHUE_MT] = {0}", "@NL_MABIEUTHUE_MT").AppendFormat(",[NL_MABIEUTHUE_VAT] = {0}", "@NL_MABIEUTHUE_VAT").AppendFormat(",[NL_GHICHU] = {0}", "@NL_GHICHU").AppendFormat(",[NL_TEN_EN] = {0}", "@NL_TEN_EN");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("NL_MA = {0}", ":NL_MA").AppendFormat(",NL_TEN = {0}", ":NL_TEN").AppendFormat(",NL_DVT = {0}", ":NL_DVT").AppendFormat(",NL_HS = {0}", ":NL_HS").AppendFormat(",NL_SOLUONG = {0}", ":NL_SOLUONG").AppendFormat(",NL_NGUONNGUYENLIEU = {0}", ":NL_NGUONNGUYENLIEU").AppendFormat(",NL_DONGIA = {0}", ":NL_DONGIA").AppendFormat(",NL_MABIEUTHUE_XNK = {0}", ":NL_MABIEUTHUE_XNK").AppendFormat(",NL_MABIEUTHUE_TBDB = {0}", ":NL_MABIEUTHUE_TBDB").AppendFormat(",NL_MABIEUTHUE_MT = {0}", ":NL_MABIEUTHUE_MT").AppendFormat(",NL_MABIEUTHUE_VAT = {0}", ":NL_MABIEUTHUE_VAT").AppendFormat(",NL_GHICHU = {0}", ":NL_GHICHU").AppendFormat(",NL_TEN_EN = {0}", ":NL_TEN_EN");
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
			return clsDAL.DeleteString("GC_HDGC_NGUYENLIEU", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[NL_HDID] = {0}", "@NL_HDID").AppendFormat(" AND [NL_ID] = {0}", "@NL_ID");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("NL_HDID = {0}", ":NL_HDID").AppendFormat(" AND NL_ID = {0}", ":NL_ID");
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
			paramList.Add(clsDAL.CreateParameter("NL_HDID", "Integer", clsDAL.ToDBParam(NL_HDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_ID", "Integer", clsDAL.ToDBParam(NL_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("NL_MA", "WChar", clsDAL.ToDBParam(NL_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_TEN", "WChar", clsDAL.ToDBParam(NL_TEN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_DVT", "WChar", clsDAL.ToDBParam(NL_DVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_HS", "WChar", clsDAL.ToDBParam(NL_HS, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_SOLUONG", "Numeric", clsDAL.ToDBParam(NL_SOLUONG, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_NGUONNGUYENLIEU", "WChar", clsDAL.ToDBParam(NL_NGUONNGUYENLIEU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_DONGIA", "Numeric", clsDAL.ToDBParam(NL_DONGIA, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_MABIEUTHUE_XNK", "WChar", clsDAL.ToDBParam(NL_MABIEUTHUE_XNK, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_MABIEUTHUE_TBDB", "WChar", clsDAL.ToDBParam(NL_MABIEUTHUE_TBDB, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_MABIEUTHUE_MT", "WChar", clsDAL.ToDBParam(NL_MABIEUTHUE_MT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_MABIEUTHUE_VAT", "WChar", clsDAL.ToDBParam(NL_MABIEUTHUE_VAT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_GHICHU", "WChar", clsDAL.ToDBParam(NL_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_TEN_EN", "WChar", clsDAL.ToDBParam(NL_TEN_EN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_HDID", "Integer", clsDAL.ToDBParam(NL_HDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_ID", "Integer", clsDAL.ToDBParam(NL_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("NL_HDID", "Integer", clsDAL.ToDBParam(NL_HDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_ID", "Integer", clsDAL.ToDBParam(NL_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_MA", "WChar", clsDAL.ToDBParam(NL_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_TEN", "WChar", clsDAL.ToDBParam(NL_TEN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_DVT", "WChar", clsDAL.ToDBParam(NL_DVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_HS", "WChar", clsDAL.ToDBParam(NL_HS, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_SOLUONG", "Numeric", clsDAL.ToDBParam(NL_SOLUONG, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_NGUONNGUYENLIEU", "WChar", clsDAL.ToDBParam(NL_NGUONNGUYENLIEU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_DONGIA", "Numeric", clsDAL.ToDBParam(NL_DONGIA, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_MABIEUTHUE_XNK", "WChar", clsDAL.ToDBParam(NL_MABIEUTHUE_XNK, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_MABIEUTHUE_TBDB", "WChar", clsDAL.ToDBParam(NL_MABIEUTHUE_TBDB, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_MABIEUTHUE_MT", "WChar", clsDAL.ToDBParam(NL_MABIEUTHUE_MT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_MABIEUTHUE_VAT", "WChar", clsDAL.ToDBParam(NL_MABIEUTHUE_VAT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_GHICHU", "WChar", clsDAL.ToDBParam(NL_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NL_TEN_EN", "WChar", clsDAL.ToDBParam(NL_TEN_EN, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}