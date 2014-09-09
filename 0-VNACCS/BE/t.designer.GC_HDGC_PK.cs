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
	/// Generated Class for Table : GC_HDGC_PK.
	/// </summary>
	public partial class GC_HDGC_PK : TableBase
	{
		public GC_HDGC_PK() : base(){}

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
		private int m_PK_HDID = 0;
		/// <summary>
		/// PK_HDID.
		/// </summary>
		public int PK_HDID
		{
			get
			{
				return m_PK_HDID;
			}
			set
			{
				if ((this.m_PK_HDID != value))
				{
					this.SendPropertyChanging("PK_HDID");
					this.m_PK_HDID = value;
					this.SendPropertyChanged("PK_HDID");
				}
			}
		}

		private int m_PK_ID;
		/// <summary>
		/// PK_ID.
		/// </summary>
		public int PK_ID
		{
			get
			{
				return m_PK_ID;
			}
			set
			{
				if ((this.m_PK_ID != value))
				{
					this.SendPropertyChanging("PK_ID");
					this.m_PK_ID = value;
					this.SendPropertyChanged("PK_ID");
				}
			}
		}

		private string m_PK_SOPK;
		private bool m_PK_SOPKUpdated = false;
		/// <summary>
		/// PK_SOPK.
		/// </summary>
		public string PK_SOPK
		{
			get
			{
				return m_PK_SOPK;
			}
			set
			{
				if ((this.m_PK_SOPK != value))
				{
					this.SendPropertyChanging("PK_SOPK");
					this.m_PK_SOPK = value;
					this.SendPropertyChanged("PK_SOPK");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_PK_SOPKUpdated = true;
				}
			}
		}

		private DateTime? m_PK_NGAYPK;
		private bool m_PK_NGAYPKUpdated = false;
		/// <summary>
		/// PK_NGAYPK.
		/// </summary>
		public DateTime? PK_NGAYPK
		{
			get
			{
				return m_PK_NGAYPK;
			}
			set
			{
				if ((this.m_PK_NGAYPK != value))
				{
					this.SendPropertyChanging("PK_NGAYPK");
					this.m_PK_NGAYPK = value;
					this.SendPropertyChanged("PK_NGAYPK");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_PK_NGAYPKUpdated = true;
				}
			}
		}

		private string m_PK_NGUOIDUYET;
		private bool m_PK_NGUOIDUYETUpdated = false;
		/// <summary>
		/// PK_NGUOIDUYET.
		/// </summary>
		public string PK_NGUOIDUYET
		{
			get
			{
				return m_PK_NGUOIDUYET;
			}
			set
			{
				if ((this.m_PK_NGUOIDUYET != value))
				{
					this.SendPropertyChanging("PK_NGUOIDUYET");
					this.m_PK_NGUOIDUYET = value;
					this.SendPropertyChanged("PK_NGUOIDUYET");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_PK_NGUOIDUYETUpdated = true;
				}
			}
		}

		private string m_PK_VANBAN;
		private bool m_PK_VANBANUpdated = false;
		/// <summary>
		/// PK_VANBAN.
		/// </summary>
		public string PK_VANBAN
		{
			get
			{
				return m_PK_VANBAN;
			}
			set
			{
				if ((this.m_PK_VANBAN != value))
				{
					this.SendPropertyChanging("PK_VANBAN");
					this.m_PK_VANBAN = value;
					this.SendPropertyChanged("PK_VANBAN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_PK_VANBANUpdated = true;
				}
			}
		}

		private int? m_PK_TRANGTHAI = 0;
		private bool m_PK_TRANGTHAIUpdated = false;
		/// <summary>
		/// PK_TRANGTHAI.
		/// </summary>
		public int? PK_TRANGTHAI
		{
			get
			{
				return m_PK_TRANGTHAI;
			}
			set
			{
				if ((this.m_PK_TRANGTHAI != value))
				{
					this.SendPropertyChanging("PK_TRANGTHAI");
					this.m_PK_TRANGTHAI = value;
					this.SendPropertyChanged("PK_TRANGTHAI");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_PK_TRANGTHAIUpdated = true;
				}
			}
		}

		private DateTime? m_PK_NGAYKB;
		private bool m_PK_NGAYKBUpdated = false;
		/// <summary>
		/// PK_NGAYKB.
		/// </summary>
		public DateTime? PK_NGAYKB
		{
			get
			{
				return m_PK_NGAYKB;
			}
			set
			{
				if ((this.m_PK_NGAYKB != value))
				{
					this.SendPropertyChanging("PK_NGAYKB");
					this.m_PK_NGAYKB = value;
					this.SendPropertyChanged("PK_NGAYKB");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_PK_NGAYKBUpdated = true;
				}
			}
		}

		private string m_PK_SOTN;
		private bool m_PK_SOTNUpdated = false;
		/// <summary>
		/// PK_SOTN.
		/// </summary>
		public string PK_SOTN
		{
			get
			{
				return m_PK_SOTN;
			}
			set
			{
				if ((this.m_PK_SOTN != value))
				{
					this.SendPropertyChanging("PK_SOTN");
					this.m_PK_SOTN = value;
					this.SendPropertyChanged("PK_SOTN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_PK_SOTNUpdated = true;
				}
			}
		}

		private DateTime? m_PK_NGAYTN;
		private bool m_PK_NGAYTNUpdated = false;
		/// <summary>
		/// PK_NGAYTN.
		/// </summary>
		public DateTime? PK_NGAYTN
		{
			get
			{
				return m_PK_NGAYTN;
			}
			set
			{
				if ((this.m_PK_NGAYTN != value))
				{
					this.SendPropertyChanging("PK_NGAYTN");
					this.m_PK_NGAYTN = value;
					this.SendPropertyChanged("PK_NGAYTN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_PK_NGAYTNUpdated = true;
				}
			}
		}

		private string m_PK_GHICHU;
		private bool m_PK_GHICHUUpdated = false;
		/// <summary>
		/// PK_GHICHU.
		/// </summary>
		public string PK_GHICHU
		{
			get
			{
				return m_PK_GHICHU;
			}
			set
			{
				if ((this.m_PK_GHICHU != value))
				{
					this.SendPropertyChanging("PK_GHICHU");
					this.m_PK_GHICHU = value;
					this.SendPropertyChanged("PK_GHICHU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_PK_GHICHUUpdated = true;
				}
			}
		}

		private string m_PK_REF;
		private bool m_PK_REFUpdated = false;
		/// <summary>
		/// PK_REF.
		/// </summary>
		public string PK_REF
		{
			get
			{
				return m_PK_REF;
			}
			set
			{
				if ((this.m_PK_REF != value))
				{
					this.SendPropertyChanging("PK_REF");
					this.m_PK_REF = value;
					this.SendPropertyChanged("PK_REF");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_PK_REFUpdated = true;
				}
			}
		}

		private int? m_PK_HUY = 0;
		private bool m_PK_HUYUpdated = false;
		/// <summary>
		/// PK_HUY.
		/// </summary>
		public int? PK_HUY
		{
			get
			{
				return m_PK_HUY;
			}
			set
			{
				if ((this.m_PK_HUY != value))
				{
					this.SendPropertyChanging("PK_HUY");
					this.m_PK_HUY = value;
					this.SendPropertyChanged("PK_HUY");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_PK_HUYUpdated = true;
				}
			}
		}

		private string m_PK_HUY_REF;
		private bool m_PK_HUY_REFUpdated = false;
		/// <summary>
		/// PK_HUY_REF.
		/// </summary>
		public string PK_HUY_REF
		{
			get
			{
				return m_PK_HUY_REF;
			}
			set
			{
				if ((this.m_PK_HUY_REF != value))
				{
					this.SendPropertyChanging("PK_HUY_REF");
					this.m_PK_HUY_REF = value;
					this.SendPropertyChanged("PK_HUY_REF");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_PK_HUY_REFUpdated = true;
				}
			}
		}

		private int? m_PK_HUY_SOTN = 0;
		private bool m_PK_HUY_SOTNUpdated = false;
		/// <summary>
		/// PK_HUY_SOTN.
		/// </summary>
		public int? PK_HUY_SOTN
		{
			get
			{
				return m_PK_HUY_SOTN;
			}
			set
			{
				if ((this.m_PK_HUY_SOTN != value))
				{
					this.SendPropertyChanging("PK_HUY_SOTN");
					this.m_PK_HUY_SOTN = value;
					this.SendPropertyChanged("PK_HUY_SOTN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_PK_HUY_SOTNUpdated = true;
				}
			}
		}

		private int? m_PK_HUY_NAMTN = 0;
		private bool m_PK_HUY_NAMTNUpdated = false;
		/// <summary>
		/// PK_HUY_NAMTN.
		/// </summary>
		public int? PK_HUY_NAMTN
		{
			get
			{
				return m_PK_HUY_NAMTN;
			}
			set
			{
				if ((this.m_PK_HUY_NAMTN != value))
				{
					this.SendPropertyChanging("PK_HUY_NAMTN");
					this.m_PK_HUY_NAMTN = value;
					this.SendPropertyChanged("PK_HUY_NAMTN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_PK_HUY_NAMTNUpdated = true;
				}
			}
		}

		private DateTime? m_PK_HUY_NGAYTN;
		private bool m_PK_HUY_NGAYTNUpdated = false;
		/// <summary>
		/// PK_HUY_NGAYTN.
		/// </summary>
		public DateTime? PK_HUY_NGAYTN
		{
			get
			{
				return m_PK_HUY_NGAYTN;
			}
			set
			{
				if ((this.m_PK_HUY_NGAYTN != value))
				{
					this.SendPropertyChanging("PK_HUY_NGAYTN");
					this.m_PK_HUY_NGAYTN = value;
					this.SendPropertyChanged("PK_HUY_NGAYTN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_PK_HUY_NGAYTNUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM GC_HDGC_PK " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[PK_HDID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[PK_ID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[PK_SOPK]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[PK_NGAYPK]", ProType.DATETIME, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[PK_NGUOIDUYET]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[PK_VANBAN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[PK_TRANGTHAI]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[PK_NGAYKB]", ProType.DATETIME, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[PK_SOTN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[PK_NGAYTN]", ProType.DATETIME, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[PK_GHICHU]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[PK_REF]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[PK_HUY]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[PK_HUY_REF]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[PK_HUY_SOTN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[PK_HUY_NAMTN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[PK_HUY_NGAYTN]", ProType.DATETIME, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("PK_HDID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("PK_ID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("PK_SOPK", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("PK_NGAYPK", ProType.DATETIME, this.DataManagement)).Append(",").Append(clsDAL.SelectField("PK_NGUOIDUYET", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("PK_VANBAN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("PK_TRANGTHAI", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("PK_NGAYKB", ProType.DATETIME, this.DataManagement)).Append(",").Append(clsDAL.SelectField("PK_SOTN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("PK_NGAYTN", ProType.DATETIME, this.DataManagement)).Append(",").Append(clsDAL.SelectField("PK_GHICHU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("PK_REF", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("PK_HUY", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("PK_HUY_REF", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("PK_HUY_SOTN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("PK_HUY_NAMTN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("PK_HUY_NGAYTN", ProType.DATETIME, this.DataManagement));
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
				sbSQL.Append("INSERT INTO GC_HDGC_PK ([PK_HDID], [PK_ID], [PK_SOPK], [PK_NGAYPK], [PK_NGUOIDUYET], [PK_VANBAN], [PK_TRANGTHAI], [PK_NGAYKB], [PK_SOTN], [PK_NGAYTN], [PK_GHICHU], [PK_REF], [PK_HUY], [PK_HUY_REF], [PK_HUY_SOTN], [PK_HUY_NAMTN], [PK_HUY_NGAYTN]) VALUES(").Append("@PK_HDID").Append(",").Append("@PK_ID").Append(",").Append("@PK_SOPK").Append(",").Append("@PK_NGAYPK").Append(",").Append("@PK_NGUOIDUYET").Append(",").Append("@PK_VANBAN").Append(",").Append("@PK_TRANGTHAI").Append(",").Append("@PK_NGAYKB").Append(",").Append("@PK_SOTN").Append(",").Append("@PK_NGAYTN").Append(",").Append("@PK_GHICHU").Append(",").Append("@PK_REF").Append(",").Append("@PK_HUY").Append(",").Append("@PK_HUY_REF").Append(",").Append("@PK_HUY_SOTN").Append(",").Append("@PK_HUY_NAMTN").Append(",").Append("@PK_HUY_NGAYTN").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO GC_HDGC_PK (PK_HDID, PK_ID, PK_SOPK, PK_NGAYPK, PK_NGUOIDUYET, PK_VANBAN, PK_TRANGTHAI, PK_NGAYKB, PK_SOTN, PK_NGAYTN, PK_GHICHU, PK_REF, PK_HUY, PK_HUY_REF, PK_HUY_SOTN, PK_HUY_NAMTN, PK_HUY_NGAYTN) VALUES(").Append(":PK_HDID").Append(",").Append(":PK_ID").Append(",").Append(":PK_SOPK").Append(",").Append(":PK_NGAYPK").Append(",").Append(":PK_NGUOIDUYET").Append(",").Append(":PK_VANBAN").Append(",").Append(":PK_TRANGTHAI").Append(",").Append(":PK_NGAYKB").Append(",").Append(":PK_SOTN").Append(",").Append(":PK_NGAYTN").Append(",").Append(":PK_GHICHU").Append(",").Append(":PK_REF").Append(",").Append(":PK_HUY").Append(",").Append(":PK_HUY_REF").Append(",").Append(":PK_HUY_SOTN").Append(",").Append(":PK_HUY_NAMTN").Append(",").Append(":PK_HUY_NGAYTN").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE GC_HDGC_PK SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_PK_SOPKUpdated ? string.Format(",[PK_SOPK] = {0}", "@PK_SOPK") : string.Empty).Append(m_PK_NGAYPKUpdated ? string.Format(",[PK_NGAYPK] = {0}", "@PK_NGAYPK") : string.Empty).Append(m_PK_NGUOIDUYETUpdated ? string.Format(",[PK_NGUOIDUYET] = {0}", "@PK_NGUOIDUYET") : string.Empty).Append(m_PK_VANBANUpdated ? string.Format(",[PK_VANBAN] = {0}", "@PK_VANBAN") : string.Empty).Append(m_PK_TRANGTHAIUpdated ? string.Format(",[PK_TRANGTHAI] = {0}", "@PK_TRANGTHAI") : string.Empty).Append(m_PK_NGAYKBUpdated ? string.Format(",[PK_NGAYKB] = {0}", "@PK_NGAYKB") : string.Empty).Append(m_PK_SOTNUpdated ? string.Format(",[PK_SOTN] = {0}", "@PK_SOTN") : string.Empty).Append(m_PK_NGAYTNUpdated ? string.Format(",[PK_NGAYTN] = {0}", "@PK_NGAYTN") : string.Empty).Append(m_PK_GHICHUUpdated ? string.Format(",[PK_GHICHU] = {0}", "@PK_GHICHU") : string.Empty).Append(m_PK_REFUpdated ? string.Format(",[PK_REF] = {0}", "@PK_REF") : string.Empty).Append(m_PK_HUYUpdated ? string.Format(",[PK_HUY] = {0}", "@PK_HUY") : string.Empty).Append(m_PK_HUY_REFUpdated ? string.Format(",[PK_HUY_REF] = {0}", "@PK_HUY_REF") : string.Empty).Append(m_PK_HUY_SOTNUpdated ? string.Format(",[PK_HUY_SOTN] = {0}", "@PK_HUY_SOTN") : string.Empty).Append(m_PK_HUY_NAMTNUpdated ? string.Format(",[PK_HUY_NAMTN] = {0}", "@PK_HUY_NAMTN") : string.Empty).Append(m_PK_HUY_NGAYTNUpdated ? string.Format(",[PK_HUY_NGAYTN] = {0}", "@PK_HUY_NGAYTN") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_PK_SOPKUpdated ? string.Format(",PK_SOPK = {0}", ":PK_SOPK") : string.Empty).Append(m_PK_NGAYPKUpdated ? string.Format(",PK_NGAYPK = {0}", ":PK_NGAYPK") : string.Empty).Append(m_PK_NGUOIDUYETUpdated ? string.Format(",PK_NGUOIDUYET = {0}", ":PK_NGUOIDUYET") : string.Empty).Append(m_PK_VANBANUpdated ? string.Format(",PK_VANBAN = {0}", ":PK_VANBAN") : string.Empty).Append(m_PK_TRANGTHAIUpdated ? string.Format(",PK_TRANGTHAI = {0}", ":PK_TRANGTHAI") : string.Empty).Append(m_PK_NGAYKBUpdated ? string.Format(",PK_NGAYKB = {0}", ":PK_NGAYKB") : string.Empty).Append(m_PK_SOTNUpdated ? string.Format(",PK_SOTN = {0}", ":PK_SOTN") : string.Empty).Append(m_PK_NGAYTNUpdated ? string.Format(",PK_NGAYTN = {0}", ":PK_NGAYTN") : string.Empty).Append(m_PK_GHICHUUpdated ? string.Format(",PK_GHICHU = {0}", ":PK_GHICHU") : string.Empty).Append(m_PK_REFUpdated ? string.Format(",PK_REF = {0}", ":PK_REF") : string.Empty).Append(m_PK_HUYUpdated ? string.Format(",PK_HUY = {0}", ":PK_HUY") : string.Empty).Append(m_PK_HUY_REFUpdated ? string.Format(",PK_HUY_REF = {0}", ":PK_HUY_REF") : string.Empty).Append(m_PK_HUY_SOTNUpdated ? string.Format(",PK_HUY_SOTN = {0}", ":PK_HUY_SOTN") : string.Empty).Append(m_PK_HUY_NAMTNUpdated ? string.Format(",PK_HUY_NAMTN = {0}", ":PK_HUY_NAMTN") : string.Empty).Append(m_PK_HUY_NGAYTNUpdated ? string.Format(",PK_HUY_NGAYTN = {0}", ":PK_HUY_NGAYTN") : string.Empty);
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
				sbSQL.AppendFormat("[PK_SOPK] = {0}", "@PK_SOPK").AppendFormat(",[PK_NGAYPK] = {0}", "@PK_NGAYPK").AppendFormat(",[PK_NGUOIDUYET] = {0}", "@PK_NGUOIDUYET").AppendFormat(",[PK_VANBAN] = {0}", "@PK_VANBAN").AppendFormat(",[PK_TRANGTHAI] = {0}", "@PK_TRANGTHAI").AppendFormat(",[PK_NGAYKB] = {0}", "@PK_NGAYKB").AppendFormat(",[PK_SOTN] = {0}", "@PK_SOTN").AppendFormat(",[PK_NGAYTN] = {0}", "@PK_NGAYTN").AppendFormat(",[PK_GHICHU] = {0}", "@PK_GHICHU").AppendFormat(",[PK_REF] = {0}", "@PK_REF").AppendFormat(",[PK_HUY] = {0}", "@PK_HUY").AppendFormat(",[PK_HUY_REF] = {0}", "@PK_HUY_REF").AppendFormat(",[PK_HUY_SOTN] = {0}", "@PK_HUY_SOTN").AppendFormat(",[PK_HUY_NAMTN] = {0}", "@PK_HUY_NAMTN").AppendFormat(",[PK_HUY_NGAYTN] = {0}", "@PK_HUY_NGAYTN");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("PK_SOPK = {0}", ":PK_SOPK").AppendFormat(",PK_NGAYPK = {0}", ":PK_NGAYPK").AppendFormat(",PK_NGUOIDUYET = {0}", ":PK_NGUOIDUYET").AppendFormat(",PK_VANBAN = {0}", ":PK_VANBAN").AppendFormat(",PK_TRANGTHAI = {0}", ":PK_TRANGTHAI").AppendFormat(",PK_NGAYKB = {0}", ":PK_NGAYKB").AppendFormat(",PK_SOTN = {0}", ":PK_SOTN").AppendFormat(",PK_NGAYTN = {0}", ":PK_NGAYTN").AppendFormat(",PK_GHICHU = {0}", ":PK_GHICHU").AppendFormat(",PK_REF = {0}", ":PK_REF").AppendFormat(",PK_HUY = {0}", ":PK_HUY").AppendFormat(",PK_HUY_REF = {0}", ":PK_HUY_REF").AppendFormat(",PK_HUY_SOTN = {0}", ":PK_HUY_SOTN").AppendFormat(",PK_HUY_NAMTN = {0}", ":PK_HUY_NAMTN").AppendFormat(",PK_HUY_NGAYTN = {0}", ":PK_HUY_NGAYTN");
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
			return clsDAL.DeleteString("GC_HDGC_PK", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[PK_HDID] = {0}", "@PK_HDID").AppendFormat(" AND [PK_ID] = {0}", "@PK_ID");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("PK_HDID = {0}", ":PK_HDID").AppendFormat(" AND PK_ID = {0}", ":PK_ID");
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
			paramList.Add(clsDAL.CreateParameter("PK_HDID", "Integer", clsDAL.ToDBParam(PK_HDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_ID", "Integer", clsDAL.ToDBParam(PK_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("PK_SOPK", "WChar", clsDAL.ToDBParam(PK_SOPK, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_NGAYPK", "Date", clsDAL.ToDBParam(PK_NGAYPK, ProType.DATETIME, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_NGUOIDUYET", "WChar", clsDAL.ToDBParam(PK_NGUOIDUYET, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_VANBAN", "WChar", clsDAL.ToDBParam(PK_VANBAN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_TRANGTHAI", "Single", clsDAL.ToDBParam(PK_TRANGTHAI, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_NGAYKB", "Date", clsDAL.ToDBParam(PK_NGAYKB, ProType.DATETIME, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_SOTN", "WChar", clsDAL.ToDBParam(PK_SOTN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_NGAYTN", "Date", clsDAL.ToDBParam(PK_NGAYTN, ProType.DATETIME, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_GHICHU", "WChar", clsDAL.ToDBParam(PK_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_REF", "WChar", clsDAL.ToDBParam(PK_REF, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_HUY", "Single", clsDAL.ToDBParam(PK_HUY, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_HUY_REF", "WChar", clsDAL.ToDBParam(PK_HUY_REF, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_HUY_SOTN", "SmallInt", clsDAL.ToDBParam(PK_HUY_SOTN, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_HUY_NAMTN", "SmallInt", clsDAL.ToDBParam(PK_HUY_NAMTN, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_HUY_NGAYTN", "Date", clsDAL.ToDBParam(PK_HUY_NGAYTN, ProType.DATETIME, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_HDID", "Integer", clsDAL.ToDBParam(PK_HDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_ID", "Integer", clsDAL.ToDBParam(PK_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("PK_HDID", "Integer", clsDAL.ToDBParam(PK_HDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_ID", "Integer", clsDAL.ToDBParam(PK_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_SOPK", "WChar", clsDAL.ToDBParam(PK_SOPK, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_NGAYPK", "Date", clsDAL.ToDBParam(PK_NGAYPK, ProType.DATETIME, this.DataManagement), this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_NGUOIDUYET", "WChar", clsDAL.ToDBParam(PK_NGUOIDUYET, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_VANBAN", "WChar", clsDAL.ToDBParam(PK_VANBAN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_TRANGTHAI", "Single", clsDAL.ToDBParam(PK_TRANGTHAI, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_NGAYKB", "Date", clsDAL.ToDBParam(PK_NGAYKB, ProType.DATETIME, this.DataManagement), this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_SOTN", "WChar", clsDAL.ToDBParam(PK_SOTN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_NGAYTN", "Date", clsDAL.ToDBParam(PK_NGAYTN, ProType.DATETIME, this.DataManagement), this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_GHICHU", "WChar", clsDAL.ToDBParam(PK_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_REF", "WChar", clsDAL.ToDBParam(PK_REF, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_HUY", "Single", clsDAL.ToDBParam(PK_HUY, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_HUY_REF", "WChar", clsDAL.ToDBParam(PK_HUY_REF, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_HUY_SOTN", "SmallInt", clsDAL.ToDBParam(PK_HUY_SOTN, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_HUY_NAMTN", "SmallInt", clsDAL.ToDBParam(PK_HUY_NAMTN, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PK_HUY_NGAYTN", "Date", clsDAL.ToDBParam(PK_HUY_NGAYTN, ProType.DATETIME, this.DataManagement), this.DataManagement));
			return paramList;
		}
	}
}