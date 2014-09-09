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
	/// Generated Class for Table : SHANGVT.
	/// </summary>
	public partial class SHANGVT : TableBase
	{
		public SHANGVT() : base(){}

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

		private string m_MA_HANG_VT;
		private bool m_MA_HANG_VTUpdated = false;
		/// <summary>
		/// MA_HANG_VT.
		/// </summary>
		public string MA_HANG_VT
		{
			get
			{
				return m_MA_HANG_VT;
			}
			set
			{
				if ((this.m_MA_HANG_VT != value))
				{
					this.SendPropertyChanging("MA_HANG_VT");
					this.m_MA_HANG_VT = value;
					this.SendPropertyChanged("MA_HANG_VT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_MA_HANG_VTUpdated = true;
				}
			}
		}

		private string m_TEN_HANG_VT;
		private bool m_TEN_HANG_VTUpdated = false;
		/// <summary>
		/// TEN_HANG_VT.
		/// </summary>
		public string TEN_HANG_VT
		{
			get
			{
				return m_TEN_HANG_VT;
			}
			set
			{
				if ((this.m_TEN_HANG_VT != value))
				{
					this.SendPropertyChanging("TEN_HANG_VT");
					this.m_TEN_HANG_VT = value;
					this.SendPropertyChanged("TEN_HANG_VT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_HANG_VTUpdated = true;
				}
			}
		}

		private string m_SO_HIEU_PTVT;
		private bool m_SO_HIEU_PTVTUpdated = false;
		/// <summary>
		/// SO_HIEU_PTVT.
		/// </summary>
		public string SO_HIEU_PTVT
		{
			get
			{
				return m_SO_HIEU_PTVT;
			}
			set
			{
				if ((this.m_SO_HIEU_PTVT != value))
				{
					this.SendPropertyChanging("SO_HIEU_PTVT");
					this.m_SO_HIEU_PTVT = value;
					this.SendPropertyChanged("SO_HIEU_PTVT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_SO_HIEU_PTVTUpdated = true;
				}
			}
		}

		private string m_TEN_PTVT;
		private bool m_TEN_PTVTUpdated = false;
		/// <summary>
		/// TEN_PTVT.
		/// </summary>
		public string TEN_PTVT
		{
			get
			{
				return m_TEN_PTVT;
			}
			set
			{
				if ((this.m_TEN_PTVT != value))
				{
					this.SendPropertyChanging("TEN_PTVT");
					this.m_TEN_PTVT = value;
					this.SendPropertyChanged("TEN_PTVT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_PTVTUpdated = true;
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

		private int? m_HienThi;
		private bool m_HienThiUpdated = false;
		/// <summary>
		/// HienThi.
		/// </summary>
		public int? HienThi
		{
			get
			{
				return m_HienThi;
			}
			set
			{
				if ((this.m_HienThi != value))
				{
					this.SendPropertyChanging("HienThi");
					this.m_HienThi = value;
					this.SendPropertyChanged("HienThi");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HienThiUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SHANGVT " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(clsDAL.SelectField("STT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("MA_HANG_VT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_HANG_VT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("SO_HIEU_PTVT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_PTVT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("GHI_CHU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_BANG", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("KEY_FIELD", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HienThi", ProType.OTHER, this.DataManagement));
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
			sbSQL.Append("INSERT INTO SHANGVT (STT, MA_HANG_VT, TEN_HANG_VT, SO_HIEU_PTVT, TEN_PTVT, GHI_CHU, TEN_BANG, KEY_FIELD, HienThi) VALUES(").Append(clsDAL.IsDBNULL(STT, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(MA_HANG_VT, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_HANG_VT, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(SO_HIEU_PTVT, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_PTVT, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(GHI_CHU, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(KEY_FIELD, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)).Append(")");
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SHANGVT SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(m_STTUpdated ? string.Format(",STT = {0}", clsDAL.IsDBNULL(STT, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_MA_HANG_VTUpdated ? string.Format(",MA_HANG_VT = {0}", clsDAL.IsDBNULL(MA_HANG_VT, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_HANG_VTUpdated ? string.Format(",TEN_HANG_VT = {0}", clsDAL.IsDBNULL(TEN_HANG_VT, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_SO_HIEU_PTVTUpdated ? string.Format(",SO_HIEU_PTVT = {0}", clsDAL.IsDBNULL(SO_HIEU_PTVT, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_PTVTUpdated ? string.Format(",TEN_PTVT = {0}", clsDAL.IsDBNULL(TEN_PTVT, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_GHI_CHUUpdated ? string.Format(",GHI_CHU = {0}", clsDAL.IsDBNULL(GHI_CHU, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_BANGUpdated ? string.Format(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_HienThiUpdated ? string.Format(",HienThi = {0}", clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)) : string.Empty);
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
			sbSQL.AppendFormat("STT = {0}", clsDAL.IsDBNULL(STT, ProType.OTHER, this.DataManagement)).AppendFormat(",MA_HANG_VT = {0}", clsDAL.IsDBNULL(MA_HANG_VT, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_HANG_VT = {0}", clsDAL.IsDBNULL(TEN_HANG_VT, ProType.STRING, this.DataManagement)).AppendFormat(",SO_HIEU_PTVT = {0}", clsDAL.IsDBNULL(SO_HIEU_PTVT, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_PTVT = {0}", clsDAL.IsDBNULL(TEN_PTVT, ProType.STRING, this.DataManagement)).AppendFormat(",GHI_CHU = {0}", clsDAL.IsDBNULL(GHI_CHU, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).AppendFormat(",HienThi = {0}", clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement));
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
			return clsDAL.DeleteString("SHANGVT", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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