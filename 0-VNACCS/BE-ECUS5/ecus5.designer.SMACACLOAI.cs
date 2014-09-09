using System;
using System.Data;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
namespace DevComponents.DotNetBar.thaison
{
	/// <summary>
	/// Generated Class for Table : SMACACLOAI.
	/// </summary>
	public partial class SMACACLOAI : TableBase
	{
		public SMACACLOAI() : base(){}

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
		private decimal? m_STT;
		private bool m_STTUpdated = false;
		/// <summary>
		/// STT.
		/// </summary>
		public decimal? STT
		{
			get
			{
				return m_STT;
			}
			set
			{
				if ((this.m_STT != value))
				{
					this.SendPropertyChanging("STT");
					this.m_STT = value;
					this.SendPropertyChanged("STT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_STTUpdated = true;
				}
			}
		}

		private string m_MA;
		private bool m_MAUpdated = false;
		/// <summary>
		/// MA.
		/// </summary>
		public string MA
		{
			get
			{
				return m_MA;
			}
			set
			{
				if ((this.m_MA != value))
				{
					this.SendPropertyChanging("MA");
					this.m_MA = value;
					this.SendPropertyChanged("MA");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_MAUpdated = true;
				}
			}
		}

		private string m_TEN_MO_TA_E;
		private bool m_TEN_MO_TA_EUpdated = false;
		/// <summary>
		/// TEN_MO_TA_E.
		/// </summary>
		public string TEN_MO_TA_E
		{
			get
			{
				return m_TEN_MO_TA_E;
			}
			set
			{
				if ((this.m_TEN_MO_TA_E != value))
				{
					this.SendPropertyChanging("TEN_MO_TA_E");
					this.m_TEN_MO_TA_E = value;
					this.SendPropertyChanged("TEN_MO_TA_E");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_MO_TA_EUpdated = true;
				}
			}
		}

		private string m_TEN_MO_TA;
		private bool m_TEN_MO_TAUpdated = false;
		/// <summary>
		/// TEN_MO_TA.
		/// </summary>
		public string TEN_MO_TA
		{
			get
			{
				return m_TEN_MO_TA;
			}
			set
			{
				if ((this.m_TEN_MO_TA != value))
				{
					this.SendPropertyChanging("TEN_MO_TA");
					this.m_TEN_MO_TA = value;
					this.SendPropertyChanged("TEN_MO_TA");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_MO_TAUpdated = true;
				}
			}
		}

		private string m_TEN_MO_TA_TCVN;
		private bool m_TEN_MO_TA_TCVNUpdated = false;
		/// <summary>
		/// TEN_MO_TA_TCVN.
		/// </summary>
		public string TEN_MO_TA_TCVN
		{
			get
			{
				return m_TEN_MO_TA_TCVN;
			}
			set
			{
				if ((this.m_TEN_MO_TA_TCVN != value))
				{
					this.SendPropertyChanging("TEN_MO_TA_TCVN");
					this.m_TEN_MO_TA_TCVN = value;
					this.SendPropertyChanged("TEN_MO_TA_TCVN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_MO_TA_TCVNUpdated = true;
				}
			}
		}

		private string m_LOAI_MA;
		private bool m_LOAI_MAUpdated = false;
		/// <summary>
		/// LOAI_MA.
		/// </summary>
		public string LOAI_MA
		{
			get
			{
				return m_LOAI_MA;
			}
			set
			{
				if ((this.m_LOAI_MA != value))
				{
					this.SendPropertyChanging("LOAI_MA");
					this.m_LOAI_MA = value;
					this.SendPropertyChanged("LOAI_MA");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_LOAI_MAUpdated = true;
				}
			}
		}

		private string m_TEN_BANG;
		private bool m_TEN_BANGUpdated = false;
		/// <summary>
		/// TEN_BANG.
		/// </summary>
		public string TEN_BANG
		{
			get
			{
				return m_TEN_BANG;
			}
			set
			{
				if ((this.m_TEN_BANG != value))
				{
					this.SendPropertyChanging("TEN_BANG");
					this.m_TEN_BANG = value;
					this.SendPropertyChanged("TEN_BANG");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_BANGUpdated = true;
				}
			}
		}

		private string m_GHI_CHU;
		private bool m_GHI_CHUUpdated = false;
		/// <summary>
		/// GHI_CHU.
		/// </summary>
		public string GHI_CHU
		{
			get
			{
				return m_GHI_CHU;
			}
			set
			{
				if ((this.m_GHI_CHU != value))
				{
					this.SendPropertyChanging("GHI_CHU");
					this.m_GHI_CHU = value;
					this.SendPropertyChanged("GHI_CHU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_GHI_CHUUpdated = true;
				}
			}
		}

		private int? m_isHienThi;
		private bool m_isHienThiUpdated = false;
		/// <summary>
		/// isHienThi.
		/// </summary>
		public int? isHienThi
		{
			get
			{
				return m_isHienThi;
			}
			set
			{
				if ((this.m_isHienThi != value))
				{
					this.SendPropertyChanging("isHienThi");
					this.m_isHienThi = value;
					this.SendPropertyChanged("isHienThi");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_isHienThiUpdated = true;
				}
			}
		}

		private string m_KEY_FIELD;
		/// <summary>
		/// KEY_FIELD.
		/// </summary>
		public string KEY_FIELD
		{
			get
			{
				return m_KEY_FIELD;
			}
			set
			{
				if ((this.m_KEY_FIELD != value))
				{
					this.SendPropertyChanging("KEY_FIELD");
					this.m_KEY_FIELD = value;
					this.SendPropertyChanged("KEY_FIELD");
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SMACACLOAI " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(clsDAL.SelectField("STT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("MA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_MO_TA_E", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_MO_TA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_MO_TA_TCVN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("LOAI_MA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_BANG", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("GHI_CHU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("isHienThi", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("KEY_FIELD", ProType.OTHER, this.DataManagement));
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
			sbSQL.Append("INSERT INTO SMACACLOAI (STT, MA, TEN_MO_TA_E, TEN_MO_TA, TEN_MO_TA_TCVN, LOAI_MA, TEN_BANG, GHI_CHU, isHienThi, KEY_FIELD) VALUES(").Append(clsDAL.IsDBNULL(STT, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(MA, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_MO_TA_E, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_MO_TA, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_MO_TA_TCVN, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(LOAI_MA, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(GHI_CHU, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(isHienThi, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(KEY_FIELD, ProType.STRING, this.DataManagement)).Append(")");
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SMACACLOAI SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(m_STTUpdated ? string.Format(",STT = {0}", clsDAL.IsDBNULL(STT, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_MAUpdated ? string.Format(",MA = {0}", clsDAL.IsDBNULL(MA, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_MO_TA_EUpdated ? string.Format(",TEN_MO_TA_E = {0}", clsDAL.IsDBNULL(TEN_MO_TA_E, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_MO_TAUpdated ? string.Format(",TEN_MO_TA = {0}", clsDAL.IsDBNULL(TEN_MO_TA, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_MO_TA_TCVNUpdated ? string.Format(",TEN_MO_TA_TCVN = {0}", clsDAL.IsDBNULL(TEN_MO_TA_TCVN, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_LOAI_MAUpdated ? string.Format(",LOAI_MA = {0}", clsDAL.IsDBNULL(LOAI_MA, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_BANGUpdated ? string.Format(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_GHI_CHUUpdated ? string.Format(",GHI_CHU = {0}", clsDAL.IsDBNULL(GHI_CHU, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_isHienThiUpdated ? string.Format(",isHienThi = {0}", clsDAL.IsDBNULL(isHienThi, ProType.OTHER, this.DataManagement)) : string.Empty);
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
			sbSQL.AppendFormat("STT = {0}", clsDAL.IsDBNULL(STT, ProType.OTHER, this.DataManagement)).AppendFormat(",MA = {0}", clsDAL.IsDBNULL(MA, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_MO_TA_E = {0}", clsDAL.IsDBNULL(TEN_MO_TA_E, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_MO_TA = {0}", clsDAL.IsDBNULL(TEN_MO_TA, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_MO_TA_TCVN = {0}", clsDAL.IsDBNULL(TEN_MO_TA_TCVN, ProType.STRING, this.DataManagement)).AppendFormat(",LOAI_MA = {0}", clsDAL.IsDBNULL(LOAI_MA, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).AppendFormat(",GHI_CHU = {0}", clsDAL.IsDBNULL(GHI_CHU, ProType.STRING, this.DataManagement)).AppendFormat(",isHienThi = {0}", clsDAL.IsDBNULL(isHienThi, ProType.OTHER, this.DataManagement));
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
			return clsDAL.DeleteString("SMACACLOAI", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
			sbSQL.AppendFormat("KEY_FIELD = {0}", clsDAL.IsDBNULL(KEY_FIELD, ProType.STRING, this.DataManagement));
			return sbSQL.ToString();
		}
	}
}