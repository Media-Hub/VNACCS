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
	/// Generated Class for Table : GC_HDGC.
	/// </summary>
	public partial class GC_HDGC : TableBase
	{
		public GC_HDGC() : base(){}

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
		private int m_HD_ID;
		/// <summary>
		/// HD_ID.
		/// </summary>
		public int HD_ID
		{
			get
			{
				return m_HD_ID;
			}
			set
			{
				if ((this.m_HD_ID != value))
				{
					this.SendPropertyChanging("HD_ID");
					this.m_HD_ID = value;
					this.SendPropertyChanged("HD_ID");
				}
			}
		}

		private string m_HD_SOHD;
		private bool m_HD_SOHDUpdated = false;
		/// <summary>
		/// HD_SOHD.
		/// </summary>
		public string HD_SOHD
		{
			get
			{
				return m_HD_SOHD;
			}
			set
			{
				if ((this.m_HD_SOHD != value))
				{
					this.SendPropertyChanging("HD_SOHD");
					this.m_HD_SOHD = value;
					this.SendPropertyChanged("HD_SOHD");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HD_SOHDUpdated = true;
				}
			}
		}

		private DateTime? m_HD_NGAYKY;
		private bool m_HD_NGAYKYUpdated = false;
		/// <summary>
		/// HD_NGAYKY.
		/// </summary>
		public DateTime? HD_NGAYKY
		{
			get
			{
				return m_HD_NGAYKY;
			}
			set
			{
				if ((this.m_HD_NGAYKY != value))
				{
					this.SendPropertyChanging("HD_NGAYKY");
					this.m_HD_NGAYKY = value;
					this.SendPropertyChanged("HD_NGAYKY");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HD_NGAYKYUpdated = true;
				}
			}
		}

		private DateTime? m_HD_NGAYHH;
		private bool m_HD_NGAYHHUpdated = false;
		/// <summary>
		/// HD_NGAYHH.
		/// </summary>
		public DateTime? HD_NGAYHH
		{
			get
			{
				return m_HD_NGAYHH;
			}
			set
			{
				if ((this.m_HD_NGAYHH != value))
				{
					this.SendPropertyChanging("HD_NGAYHH");
					this.m_HD_NGAYHH = value;
					this.SendPropertyChanged("HD_NGAYHH");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HD_NGAYHHUpdated = true;
				}
			}
		}

		private string m_HD_MAHQ;
		private bool m_HD_MAHQUpdated = false;
		/// <summary>
		/// HD_MAHQ.
		/// </summary>
		public string HD_MAHQ
		{
			get
			{
				return m_HD_MAHQ;
			}
			set
			{
				if ((this.m_HD_MAHQ != value))
				{
					this.SendPropertyChanging("HD_MAHQ");
					this.m_HD_MAHQ = value;
					this.SendPropertyChanged("HD_MAHQ");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HD_MAHQUpdated = true;
				}
			}
		}

		private string m_HD_MADV;
		private bool m_HD_MADVUpdated = false;
		/// <summary>
		/// HD_MADV.
		/// </summary>
		public string HD_MADV
		{
			get
			{
				return m_HD_MADV;
			}
			set
			{
				if ((this.m_HD_MADV != value))
				{
					this.SendPropertyChanging("HD_MADV");
					this.m_HD_MADV = value;
					this.SendPropertyChanged("HD_MADV");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HD_MADVUpdated = true;
				}
			}
		}

		private string m_HD_TENDV;
		private bool m_HD_TENDVUpdated = false;
		/// <summary>
		/// HD_TENDV.
		/// </summary>
		public string HD_TENDV
		{
			get
			{
				return m_HD_TENDV;
			}
			set
			{
				if ((this.m_HD_TENDV != value))
				{
					this.SendPropertyChanging("HD_TENDV");
					this.m_HD_TENDV = value;
					this.SendPropertyChanged("HD_TENDV");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HD_TENDVUpdated = true;
				}
			}
		}

		private string m_HD_DIACHIDV;
		private bool m_HD_DIACHIDVUpdated = false;
		/// <summary>
		/// HD_DIACHIDV.
		/// </summary>
		public string HD_DIACHIDV
		{
			get
			{
				return m_HD_DIACHIDV;
			}
			set
			{
				if ((this.m_HD_DIACHIDV != value))
				{
					this.SendPropertyChanging("HD_DIACHIDV");
					this.m_HD_DIACHIDV = value;
					this.SendPropertyChanged("HD_DIACHIDV");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HD_DIACHIDVUpdated = true;
				}
			}
		}

		private string m_HD_NUOCTHUE_MA;
		private bool m_HD_NUOCTHUE_MAUpdated = false;
		/// <summary>
		/// HD_NUOCTHUE_MA.
		/// </summary>
		public string HD_NUOCTHUE_MA
		{
			get
			{
				return m_HD_NUOCTHUE_MA;
			}
			set
			{
				if ((this.m_HD_NUOCTHUE_MA != value))
				{
					this.SendPropertyChanging("HD_NUOCTHUE_MA");
					this.m_HD_NUOCTHUE_MA = value;
					this.SendPropertyChanged("HD_NUOCTHUE_MA");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HD_NUOCTHUE_MAUpdated = true;
				}
			}
		}

		private string m_HD_NGTE_MA;
		private bool m_HD_NGTE_MAUpdated = false;
		/// <summary>
		/// HD_NGTE_MA.
		/// </summary>
		public string HD_NGTE_MA
		{
			get
			{
				return m_HD_NGTE_MA;
			}
			set
			{
				if ((this.m_HD_NGTE_MA != value))
				{
					this.SendPropertyChanging("HD_NGTE_MA");
					this.m_HD_NGTE_MA = value;
					this.SendPropertyChanged("HD_NGTE_MA");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HD_NGTE_MAUpdated = true;
				}
			}
		}

		private string m_HD_PTTT_MA;
		private bool m_HD_PTTT_MAUpdated = false;
		/// <summary>
		/// HD_PTTT_MA.
		/// </summary>
		public string HD_PTTT_MA
		{
			get
			{
				return m_HD_PTTT_MA;
			}
			set
			{
				if ((this.m_HD_PTTT_MA != value))
				{
					this.SendPropertyChanging("HD_PTTT_MA");
					this.m_HD_PTTT_MA = value;
					this.SendPropertyChanged("HD_PTTT_MA");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HD_PTTT_MAUpdated = true;
				}
			}
		}

		private decimal? m_HD_TONGTRIGIASANPHAM = 0;
		private bool m_HD_TONGTRIGIASANPHAMUpdated = false;
		/// <summary>
		/// HD_TONGTRIGIASANPHAM.
		/// </summary>
		public decimal? HD_TONGTRIGIASANPHAM
		{
			get
			{
				return m_HD_TONGTRIGIASANPHAM;
			}
			set
			{
				if ((this.m_HD_TONGTRIGIASANPHAM != value))
				{
					this.SendPropertyChanging("HD_TONGTRIGIASANPHAM");
					this.m_HD_TONGTRIGIASANPHAM = value;
					this.SendPropertyChanged("HD_TONGTRIGIASANPHAM");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HD_TONGTRIGIASANPHAMUpdated = true;
				}
			}
		}

		private decimal? m_HD_TONGTRIGIATIENCONG = 0;
		private bool m_HD_TONGTRIGIATIENCONGUpdated = false;
		/// <summary>
		/// HD_TONGTRIGIATIENCONG.
		/// </summary>
		public decimal? HD_TONGTRIGIATIENCONG
		{
			get
			{
				return m_HD_TONGTRIGIATIENCONG;
			}
			set
			{
				if ((this.m_HD_TONGTRIGIATIENCONG != value))
				{
					this.SendPropertyChanging("HD_TONGTRIGIATIENCONG");
					this.m_HD_TONGTRIGIATIENCONG = value;
					this.SendPropertyChanged("HD_TONGTRIGIATIENCONG");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HD_TONGTRIGIATIENCONGUpdated = true;
				}
			}
		}

		private string m_HD_BENTHUE_MA;
		private bool m_HD_BENTHUE_MAUpdated = false;
		/// <summary>
		/// HD_BENTHUE_MA.
		/// </summary>
		public string HD_BENTHUE_MA
		{
			get
			{
				return m_HD_BENTHUE_MA;
			}
			set
			{
				if ((this.m_HD_BENTHUE_MA != value))
				{
					this.SendPropertyChanging("HD_BENTHUE_MA");
					this.m_HD_BENTHUE_MA = value;
					this.SendPropertyChanged("HD_BENTHUE_MA");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HD_BENTHUE_MAUpdated = true;
				}
			}
		}

		private string m_HD_BENTHUE_TEN;
		private bool m_HD_BENTHUE_TENUpdated = false;
		/// <summary>
		/// HD_BENTHUE_TEN.
		/// </summary>
		public string HD_BENTHUE_TEN
		{
			get
			{
				return m_HD_BENTHUE_TEN;
			}
			set
			{
				if ((this.m_HD_BENTHUE_TEN != value))
				{
					this.SendPropertyChanging("HD_BENTHUE_TEN");
					this.m_HD_BENTHUE_TEN = value;
					this.SendPropertyChanged("HD_BENTHUE_TEN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HD_BENTHUE_TENUpdated = true;
				}
			}
		}

		private string m_HD_BENTHUE_DIACHI;
		private bool m_HD_BENTHUE_DIACHIUpdated = false;
		/// <summary>
		/// HD_BENTHUE_DIACHI.
		/// </summary>
		public string HD_BENTHUE_DIACHI
		{
			get
			{
				return m_HD_BENTHUE_DIACHI;
			}
			set
			{
				if ((this.m_HD_BENTHUE_DIACHI != value))
				{
					this.SendPropertyChanging("HD_BENTHUE_DIACHI");
					this.m_HD_BENTHUE_DIACHI = value;
					this.SendPropertyChanged("HD_BENTHUE_DIACHI");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HD_BENTHUE_DIACHIUpdated = true;
				}
			}
		}

		private string m_HD_GHICHU;
		private bool m_HD_GHICHUUpdated = false;
		/// <summary>
		/// HD_GHICHU.
		/// </summary>
		public string HD_GHICHU
		{
			get
			{
				return m_HD_GHICHU;
			}
			set
			{
				if ((this.m_HD_GHICHU != value))
				{
					this.SendPropertyChanging("HD_GHICHU");
					this.m_HD_GHICHU = value;
					this.SendPropertyChanged("HD_GHICHU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HD_GHICHUUpdated = true;
				}
			}
		}

		private string m_HD_SOTN;
		private bool m_HD_SOTNUpdated = false;
		/// <summary>
		/// HD_SOTN.
		/// </summary>
		public string HD_SOTN
		{
			get
			{
				return m_HD_SOTN;
			}
			set
			{
				if ((this.m_HD_SOTN != value))
				{
					this.SendPropertyChanging("HD_SOTN");
					this.m_HD_SOTN = value;
					this.SendPropertyChanged("HD_SOTN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HD_SOTNUpdated = true;
				}
			}
		}

		private DateTime? m_HD_NGAYTN;
		private bool m_HD_NGAYTNUpdated = false;
		/// <summary>
		/// HD_NGAYTN.
		/// </summary>
		public DateTime? HD_NGAYTN
		{
			get
			{
				return m_HD_NGAYTN;
			}
			set
			{
				if ((this.m_HD_NGAYTN != value))
				{
					this.SendPropertyChanging("HD_NGAYTN");
					this.m_HD_NGAYTN = value;
					this.SendPropertyChanged("HD_NGAYTN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HD_NGAYTNUpdated = true;
				}
			}
		}

		private string m_HD_REF;
		private bool m_HD_REFUpdated = false;
		/// <summary>
		/// HD_REF.
		/// </summary>
		public string HD_REF
		{
			get
			{
				return m_HD_REF;
			}
			set
			{
				if ((this.m_HD_REF != value))
				{
					this.SendPropertyChanging("HD_REF");
					this.m_HD_REF = value;
					this.SendPropertyChanged("HD_REF");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HD_REFUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM GC_HDGC " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[HD_ID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HD_SOHD]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HD_NGAYKY]", ProType.DATETIME, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HD_NGAYHH]", ProType.DATETIME, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HD_MAHQ]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HD_MADV]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HD_TENDV]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HD_DIACHIDV]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HD_NUOCTHUE_MA]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HD_NGTE_MA]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HD_PTTT_MA]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HD_TONGTRIGIASANPHAM]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HD_TONGTRIGIATIENCONG]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HD_BENTHUE_MA]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HD_BENTHUE_TEN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HD_BENTHUE_DIACHI]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HD_GHICHU]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HD_SOTN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HD_NGAYTN]", ProType.DATETIME, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HD_REF]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("HD_ID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HD_SOHD", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HD_NGAYKY", ProType.DATETIME, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HD_NGAYHH", ProType.DATETIME, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HD_MAHQ", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HD_MADV", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HD_TENDV", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HD_DIACHIDV", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HD_NUOCTHUE_MA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HD_NGTE_MA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HD_PTTT_MA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HD_TONGTRIGIASANPHAM", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HD_TONGTRIGIATIENCONG", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HD_BENTHUE_MA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HD_BENTHUE_TEN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HD_BENTHUE_DIACHI", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HD_GHICHU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HD_SOTN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HD_NGAYTN", ProType.DATETIME, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HD_REF", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO GC_HDGC ([HD_ID], [HD_SOHD], [HD_NGAYKY], [HD_NGAYHH], [HD_MAHQ], [HD_MADV], [HD_TENDV], [HD_DIACHIDV], [HD_NUOCTHUE_MA], [HD_NGTE_MA], [HD_PTTT_MA], [HD_TONGTRIGIASANPHAM], [HD_TONGTRIGIATIENCONG], [HD_BENTHUE_MA], [HD_BENTHUE_TEN], [HD_BENTHUE_DIACHI], [HD_GHICHU], [HD_SOTN], [HD_NGAYTN], [HD_REF]) VALUES(").Append("@HD_ID").Append(",").Append("@HD_SOHD").Append(",").Append("@HD_NGAYKY").Append(",").Append("@HD_NGAYHH").Append(",").Append("@HD_MAHQ").Append(",").Append("@HD_MADV").Append(",").Append("@HD_TENDV").Append(",").Append("@HD_DIACHIDV").Append(",").Append("@HD_NUOCTHUE_MA").Append(",").Append("@HD_NGTE_MA").Append(",").Append("@HD_PTTT_MA").Append(",").Append("@HD_TONGTRIGIASANPHAM").Append(",").Append("@HD_TONGTRIGIATIENCONG").Append(",").Append("@HD_BENTHUE_MA").Append(",").Append("@HD_BENTHUE_TEN").Append(",").Append("@HD_BENTHUE_DIACHI").Append(",").Append("@HD_GHICHU").Append(",").Append("@HD_SOTN").Append(",").Append("@HD_NGAYTN").Append(",").Append("@HD_REF").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO GC_HDGC (HD_ID, HD_SOHD, HD_NGAYKY, HD_NGAYHH, HD_MAHQ, HD_MADV, HD_TENDV, HD_DIACHIDV, HD_NUOCTHUE_MA, HD_NGTE_MA, HD_PTTT_MA, HD_TONGTRIGIASANPHAM, HD_TONGTRIGIATIENCONG, HD_BENTHUE_MA, HD_BENTHUE_TEN, HD_BENTHUE_DIACHI, HD_GHICHU, HD_SOTN, HD_NGAYTN, HD_REF) VALUES(").Append(":HD_ID").Append(",").Append(":HD_SOHD").Append(",").Append(":HD_NGAYKY").Append(",").Append(":HD_NGAYHH").Append(",").Append(":HD_MAHQ").Append(",").Append(":HD_MADV").Append(",").Append(":HD_TENDV").Append(",").Append(":HD_DIACHIDV").Append(",").Append(":HD_NUOCTHUE_MA").Append(",").Append(":HD_NGTE_MA").Append(",").Append(":HD_PTTT_MA").Append(",").Append(":HD_TONGTRIGIASANPHAM").Append(",").Append(":HD_TONGTRIGIATIENCONG").Append(",").Append(":HD_BENTHUE_MA").Append(",").Append(":HD_BENTHUE_TEN").Append(",").Append(":HD_BENTHUE_DIACHI").Append(",").Append(":HD_GHICHU").Append(",").Append(":HD_SOTN").Append(",").Append(":HD_NGAYTN").Append(",").Append(":HD_REF").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE GC_HDGC SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_HD_SOHDUpdated ? string.Format(",[HD_SOHD] = {0}", "@HD_SOHD") : string.Empty).Append(m_HD_NGAYKYUpdated ? string.Format(",[HD_NGAYKY] = {0}", "@HD_NGAYKY") : string.Empty).Append(m_HD_NGAYHHUpdated ? string.Format(",[HD_NGAYHH] = {0}", "@HD_NGAYHH") : string.Empty).Append(m_HD_MAHQUpdated ? string.Format(",[HD_MAHQ] = {0}", "@HD_MAHQ") : string.Empty).Append(m_HD_MADVUpdated ? string.Format(",[HD_MADV] = {0}", "@HD_MADV") : string.Empty).Append(m_HD_TENDVUpdated ? string.Format(",[HD_TENDV] = {0}", "@HD_TENDV") : string.Empty).Append(m_HD_DIACHIDVUpdated ? string.Format(",[HD_DIACHIDV] = {0}", "@HD_DIACHIDV") : string.Empty).Append(m_HD_NUOCTHUE_MAUpdated ? string.Format(",[HD_NUOCTHUE_MA] = {0}", "@HD_NUOCTHUE_MA") : string.Empty).Append(m_HD_NGTE_MAUpdated ? string.Format(",[HD_NGTE_MA] = {0}", "@HD_NGTE_MA") : string.Empty).Append(m_HD_PTTT_MAUpdated ? string.Format(",[HD_PTTT_MA] = {0}", "@HD_PTTT_MA") : string.Empty).Append(m_HD_TONGTRIGIASANPHAMUpdated ? string.Format(",[HD_TONGTRIGIASANPHAM] = {0}", "@HD_TONGTRIGIASANPHAM") : string.Empty).Append(m_HD_TONGTRIGIATIENCONGUpdated ? string.Format(",[HD_TONGTRIGIATIENCONG] = {0}", "@HD_TONGTRIGIATIENCONG") : string.Empty).Append(m_HD_BENTHUE_MAUpdated ? string.Format(",[HD_BENTHUE_MA] = {0}", "@HD_BENTHUE_MA") : string.Empty).Append(m_HD_BENTHUE_TENUpdated ? string.Format(",[HD_BENTHUE_TEN] = {0}", "@HD_BENTHUE_TEN") : string.Empty).Append(m_HD_BENTHUE_DIACHIUpdated ? string.Format(",[HD_BENTHUE_DIACHI] = {0}", "@HD_BENTHUE_DIACHI") : string.Empty).Append(m_HD_GHICHUUpdated ? string.Format(",[HD_GHICHU] = {0}", "@HD_GHICHU") : string.Empty).Append(m_HD_SOTNUpdated ? string.Format(",[HD_SOTN] = {0}", "@HD_SOTN") : string.Empty).Append(m_HD_NGAYTNUpdated ? string.Format(",[HD_NGAYTN] = {0}", "@HD_NGAYTN") : string.Empty).Append(m_HD_REFUpdated ? string.Format(",[HD_REF] = {0}", "@HD_REF") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_HD_SOHDUpdated ? string.Format(",HD_SOHD = {0}", ":HD_SOHD") : string.Empty).Append(m_HD_NGAYKYUpdated ? string.Format(",HD_NGAYKY = {0}", ":HD_NGAYKY") : string.Empty).Append(m_HD_NGAYHHUpdated ? string.Format(",HD_NGAYHH = {0}", ":HD_NGAYHH") : string.Empty).Append(m_HD_MAHQUpdated ? string.Format(",HD_MAHQ = {0}", ":HD_MAHQ") : string.Empty).Append(m_HD_MADVUpdated ? string.Format(",HD_MADV = {0}", ":HD_MADV") : string.Empty).Append(m_HD_TENDVUpdated ? string.Format(",HD_TENDV = {0}", ":HD_TENDV") : string.Empty).Append(m_HD_DIACHIDVUpdated ? string.Format(",HD_DIACHIDV = {0}", ":HD_DIACHIDV") : string.Empty).Append(m_HD_NUOCTHUE_MAUpdated ? string.Format(",HD_NUOCTHUE_MA = {0}", ":HD_NUOCTHUE_MA") : string.Empty).Append(m_HD_NGTE_MAUpdated ? string.Format(",HD_NGTE_MA = {0}", ":HD_NGTE_MA") : string.Empty).Append(m_HD_PTTT_MAUpdated ? string.Format(",HD_PTTT_MA = {0}", ":HD_PTTT_MA") : string.Empty).Append(m_HD_TONGTRIGIASANPHAMUpdated ? string.Format(",HD_TONGTRIGIASANPHAM = {0}", ":HD_TONGTRIGIASANPHAM") : string.Empty).Append(m_HD_TONGTRIGIATIENCONGUpdated ? string.Format(",HD_TONGTRIGIATIENCONG = {0}", ":HD_TONGTRIGIATIENCONG") : string.Empty).Append(m_HD_BENTHUE_MAUpdated ? string.Format(",HD_BENTHUE_MA = {0}", ":HD_BENTHUE_MA") : string.Empty).Append(m_HD_BENTHUE_TENUpdated ? string.Format(",HD_BENTHUE_TEN = {0}", ":HD_BENTHUE_TEN") : string.Empty).Append(m_HD_BENTHUE_DIACHIUpdated ? string.Format(",HD_BENTHUE_DIACHI = {0}", ":HD_BENTHUE_DIACHI") : string.Empty).Append(m_HD_GHICHUUpdated ? string.Format(",HD_GHICHU = {0}", ":HD_GHICHU") : string.Empty).Append(m_HD_SOTNUpdated ? string.Format(",HD_SOTN = {0}", ":HD_SOTN") : string.Empty).Append(m_HD_NGAYTNUpdated ? string.Format(",HD_NGAYTN = {0}", ":HD_NGAYTN") : string.Empty).Append(m_HD_REFUpdated ? string.Format(",HD_REF = {0}", ":HD_REF") : string.Empty);
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
				sbSQL.AppendFormat("[HD_SOHD] = {0}", "@HD_SOHD").AppendFormat(",[HD_NGAYKY] = {0}", "@HD_NGAYKY").AppendFormat(",[HD_NGAYHH] = {0}", "@HD_NGAYHH").AppendFormat(",[HD_MAHQ] = {0}", "@HD_MAHQ").AppendFormat(",[HD_MADV] = {0}", "@HD_MADV").AppendFormat(",[HD_TENDV] = {0}", "@HD_TENDV").AppendFormat(",[HD_DIACHIDV] = {0}", "@HD_DIACHIDV").AppendFormat(",[HD_NUOCTHUE_MA] = {0}", "@HD_NUOCTHUE_MA").AppendFormat(",[HD_NGTE_MA] = {0}", "@HD_NGTE_MA").AppendFormat(",[HD_PTTT_MA] = {0}", "@HD_PTTT_MA").AppendFormat(",[HD_TONGTRIGIASANPHAM] = {0}", "@HD_TONGTRIGIASANPHAM").AppendFormat(",[HD_TONGTRIGIATIENCONG] = {0}", "@HD_TONGTRIGIATIENCONG").AppendFormat(",[HD_BENTHUE_MA] = {0}", "@HD_BENTHUE_MA").AppendFormat(",[HD_BENTHUE_TEN] = {0}", "@HD_BENTHUE_TEN").AppendFormat(",[HD_BENTHUE_DIACHI] = {0}", "@HD_BENTHUE_DIACHI").AppendFormat(",[HD_GHICHU] = {0}", "@HD_GHICHU").AppendFormat(",[HD_SOTN] = {0}", "@HD_SOTN").AppendFormat(",[HD_NGAYTN] = {0}", "@HD_NGAYTN").AppendFormat(",[HD_REF] = {0}", "@HD_REF");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("HD_SOHD = {0}", ":HD_SOHD").AppendFormat(",HD_NGAYKY = {0}", ":HD_NGAYKY").AppendFormat(",HD_NGAYHH = {0}", ":HD_NGAYHH").AppendFormat(",HD_MAHQ = {0}", ":HD_MAHQ").AppendFormat(",HD_MADV = {0}", ":HD_MADV").AppendFormat(",HD_TENDV = {0}", ":HD_TENDV").AppendFormat(",HD_DIACHIDV = {0}", ":HD_DIACHIDV").AppendFormat(",HD_NUOCTHUE_MA = {0}", ":HD_NUOCTHUE_MA").AppendFormat(",HD_NGTE_MA = {0}", ":HD_NGTE_MA").AppendFormat(",HD_PTTT_MA = {0}", ":HD_PTTT_MA").AppendFormat(",HD_TONGTRIGIASANPHAM = {0}", ":HD_TONGTRIGIASANPHAM").AppendFormat(",HD_TONGTRIGIATIENCONG = {0}", ":HD_TONGTRIGIATIENCONG").AppendFormat(",HD_BENTHUE_MA = {0}", ":HD_BENTHUE_MA").AppendFormat(",HD_BENTHUE_TEN = {0}", ":HD_BENTHUE_TEN").AppendFormat(",HD_BENTHUE_DIACHI = {0}", ":HD_BENTHUE_DIACHI").AppendFormat(",HD_GHICHU = {0}", ":HD_GHICHU").AppendFormat(",HD_SOTN = {0}", ":HD_SOTN").AppendFormat(",HD_NGAYTN = {0}", ":HD_NGAYTN").AppendFormat(",HD_REF = {0}", ":HD_REF");
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
			return clsDAL.DeleteString("GC_HDGC", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[HD_ID] = {0}", "@HD_ID");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("HD_ID = {0}", ":HD_ID");
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
			paramList.Add(clsDAL.CreateParameter("HD_ID", "Integer", clsDAL.ToDBParam(HD_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("HD_SOHD", "WChar", clsDAL.ToDBParam(HD_SOHD, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_NGAYKY", "Date", clsDAL.ToDBParam(HD_NGAYKY, ProType.DATETIME, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_NGAYHH", "Date", clsDAL.ToDBParam(HD_NGAYHH, ProType.DATETIME, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_MAHQ", "WChar", clsDAL.ToDBParam(HD_MAHQ, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_MADV", "WChar", clsDAL.ToDBParam(HD_MADV, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_TENDV", "WChar", clsDAL.ToDBParam(HD_TENDV, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_DIACHIDV", "WChar", clsDAL.ToDBParam(HD_DIACHIDV, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_NUOCTHUE_MA", "WChar", clsDAL.ToDBParam(HD_NUOCTHUE_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_NGTE_MA", "WChar", clsDAL.ToDBParam(HD_NGTE_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_PTTT_MA", "WChar", clsDAL.ToDBParam(HD_PTTT_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_TONGTRIGIASANPHAM", "Numeric", clsDAL.ToDBParam(HD_TONGTRIGIASANPHAM, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_TONGTRIGIATIENCONG", "Numeric", clsDAL.ToDBParam(HD_TONGTRIGIATIENCONG, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_BENTHUE_MA", "WChar", clsDAL.ToDBParam(HD_BENTHUE_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_BENTHUE_TEN", "WChar", clsDAL.ToDBParam(HD_BENTHUE_TEN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_BENTHUE_DIACHI", "WChar", clsDAL.ToDBParam(HD_BENTHUE_DIACHI, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_GHICHU", "WChar", clsDAL.ToDBParam(HD_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_SOTN", "WChar", clsDAL.ToDBParam(HD_SOTN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_NGAYTN", "Date", clsDAL.ToDBParam(HD_NGAYTN, ProType.DATETIME, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_REF", "WChar", clsDAL.ToDBParam(HD_REF, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_ID", "Integer", clsDAL.ToDBParam(HD_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("HD_ID", "Integer", clsDAL.ToDBParam(HD_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_SOHD", "WChar", clsDAL.ToDBParam(HD_SOHD, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_NGAYKY", "Date", clsDAL.ToDBParam(HD_NGAYKY, ProType.DATETIME, this.DataManagement), this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_NGAYHH", "Date", clsDAL.ToDBParam(HD_NGAYHH, ProType.DATETIME, this.DataManagement), this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_MAHQ", "WChar", clsDAL.ToDBParam(HD_MAHQ, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_MADV", "WChar", clsDAL.ToDBParam(HD_MADV, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_TENDV", "WChar", clsDAL.ToDBParam(HD_TENDV, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_DIACHIDV", "WChar", clsDAL.ToDBParam(HD_DIACHIDV, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_NUOCTHUE_MA", "WChar", clsDAL.ToDBParam(HD_NUOCTHUE_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_NGTE_MA", "WChar", clsDAL.ToDBParam(HD_NGTE_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_PTTT_MA", "WChar", clsDAL.ToDBParam(HD_PTTT_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_TONGTRIGIASANPHAM", "Numeric", clsDAL.ToDBParam(HD_TONGTRIGIASANPHAM, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_TONGTRIGIATIENCONG", "Numeric", clsDAL.ToDBParam(HD_TONGTRIGIATIENCONG, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_BENTHUE_MA", "WChar", clsDAL.ToDBParam(HD_BENTHUE_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_BENTHUE_TEN", "WChar", clsDAL.ToDBParam(HD_BENTHUE_TEN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_BENTHUE_DIACHI", "WChar", clsDAL.ToDBParam(HD_BENTHUE_DIACHI, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_GHICHU", "WChar", clsDAL.ToDBParam(HD_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_SOTN", "WChar", clsDAL.ToDBParam(HD_SOTN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_NGAYTN", "Date", clsDAL.ToDBParam(HD_NGAYTN, ProType.DATETIME, this.DataManagement), this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HD_REF", "WChar", clsDAL.ToDBParam(HD_REF, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}