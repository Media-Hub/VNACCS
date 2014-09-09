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
	/// Generated Class for Table : SDIA_DIEM.
	/// </summary>
	public partial class SDIA_DIEM : TableBase
	{
		public SDIA_DIEM() : base(){}

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
		private string m_MA_DIA_DIEM;
		/// <summary>
		/// MA_DIA_DIEM.
		/// </summary>
		public string MA_DIA_DIEM
		{
			get
			{
				return m_MA_DIA_DIEM;
			}
			set
			{
				if ((this.m_MA_DIA_DIEM != value))
				{
					this.SendPropertyChanging("MA_DIA_DIEM");
					this.m_MA_DIA_DIEM = value;
					this.SendPropertyChanged("MA_DIA_DIEM");
				}
			}
		}

		private string m_TEN_DIA_DIEM;
		private bool m_TEN_DIA_DIEMUpdated = false;
		/// <summary>
		/// TEN_DIA_DIEM.
		/// </summary>
		public string TEN_DIA_DIEM
		{
			get
			{
				return m_TEN_DIA_DIEM;
			}
			set
			{
				if ((this.m_TEN_DIA_DIEM != value))
				{
					this.SendPropertyChanging("TEN_DIA_DIEM");
					this.m_TEN_DIA_DIEM = value;
					this.SendPropertyChanged("TEN_DIA_DIEM");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_DIA_DIEMUpdated = true;
				}
			}
		}

		private string m_TEN_DIA_DIEM_TCVN;
		private bool m_TEN_DIA_DIEM_TCVNUpdated = false;
		/// <summary>
		/// TEN_DIA_DIEM_TCVN.
		/// </summary>
		public string TEN_DIA_DIEM_TCVN
		{
			get
			{
				return m_TEN_DIA_DIEM_TCVN;
			}
			set
			{
				if ((this.m_TEN_DIA_DIEM_TCVN != value))
				{
					this.SendPropertyChanging("TEN_DIA_DIEM_TCVN");
					this.m_TEN_DIA_DIEM_TCVN = value;
					this.SendPropertyChanged("TEN_DIA_DIEM_TCVN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_DIA_DIEM_TCVNUpdated = true;
				}
			}
		}

		private string m_DIA_CHI;
		private bool m_DIA_CHIUpdated = false;
		/// <summary>
		/// DIA_CHI.
		/// </summary>
		public string DIA_CHI
		{
			get
			{
				return m_DIA_CHI;
			}
			set
			{
				if ((this.m_DIA_CHI != value))
				{
					this.SendPropertyChanging("DIA_CHI");
					this.m_DIA_CHI = value;
					this.SendPropertyChanged("DIA_CHI");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DIA_CHIUpdated = true;
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

		private string m_MA_HQ;
		private bool m_MA_HQUpdated = false;
		/// <summary>
		/// MA_HQ.
		/// </summary>
		public string MA_HQ
		{
			get
			{
				return m_MA_HQ;
			}
			set
			{
				if ((this.m_MA_HQ != value))
				{
					this.SendPropertyChanging("MA_HQ");
					this.m_MA_HQ = value;
					this.SendPropertyChanged("MA_HQ");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_MA_HQUpdated = true;
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

		private string m_TEN_DIA_DIEM_VN;
		private bool m_TEN_DIA_DIEM_VNUpdated = false;
		/// <summary>
		/// TEN_DIA_DIEM_VN.
		/// </summary>
		public string TEN_DIA_DIEM_VN
		{
			get
			{
				return m_TEN_DIA_DIEM_VN;
			}
			set
			{
				if ((this.m_TEN_DIA_DIEM_VN != value))
				{
					this.SendPropertyChanging("TEN_DIA_DIEM_VN");
					this.m_TEN_DIA_DIEM_VN = value;
					this.SendPropertyChanged("TEN_DIA_DIEM_VN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_DIA_DIEM_VNUpdated = true;
				}
			}
		}

		private string m_TEN_DIA_DIEM_TCVN_VN;
		private bool m_TEN_DIA_DIEM_TCVN_VNUpdated = false;
		/// <summary>
		/// TEN_DIA_DIEM_TCVN_VN.
		/// </summary>
		public string TEN_DIA_DIEM_TCVN_VN
		{
			get
			{
				return m_TEN_DIA_DIEM_TCVN_VN;
			}
			set
			{
				if ((this.m_TEN_DIA_DIEM_TCVN_VN != value))
				{
					this.SendPropertyChanging("TEN_DIA_DIEM_TCVN_VN");
					this.m_TEN_DIA_DIEM_TCVN_VN = value;
					this.SendPropertyChanged("TEN_DIA_DIEM_TCVN_VN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_DIA_DIEM_TCVN_VNUpdated = true;
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

		private string m_DIA_BAN;
		private bool m_DIA_BANUpdated = false;
		/// <summary>
		/// DIA_BAN.
		/// </summary>
		public string DIA_BAN
		{
			get
			{
				return m_DIA_BAN;
			}
			set
			{
				if ((this.m_DIA_BAN != value))
				{
					this.SendPropertyChanging("DIA_BAN");
					this.m_DIA_BAN = value;
					this.SendPropertyChanged("DIA_BAN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DIA_BANUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SDIA_DIEM " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(clsDAL.SelectField("MA_DIA_DIEM", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_DIA_DIEM", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_DIA_DIEM_TCVN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DIA_CHI", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_BANG", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("MA_HQ", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("GHI_CHU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_DIA_DIEM_VN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_DIA_DIEM_TCVN_VN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HienThi", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DIA_BAN", ProType.OTHER, this.DataManagement));
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
			sbSQL.Append("INSERT INTO SDIA_DIEM (MA_DIA_DIEM, TEN_DIA_DIEM, TEN_DIA_DIEM_TCVN, DIA_CHI, TEN_BANG, MA_HQ, GHI_CHU, TEN_DIA_DIEM_VN, TEN_DIA_DIEM_TCVN_VN, HienThi, DIA_BAN) VALUES(").Append(clsDAL.IsDBNULL(MA_DIA_DIEM, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_DIA_DIEM, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_DIA_DIEM_TCVN, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(DIA_CHI, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(MA_HQ, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(GHI_CHU, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_DIA_DIEM_VN, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_DIA_DIEM_TCVN_VN, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(DIA_BAN, ProType.STRING, this.DataManagement)).Append(")");
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SDIA_DIEM SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(m_TEN_DIA_DIEMUpdated ? string.Format(",TEN_DIA_DIEM = {0}", clsDAL.IsDBNULL(TEN_DIA_DIEM, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_DIA_DIEM_TCVNUpdated ? string.Format(",TEN_DIA_DIEM_TCVN = {0}", clsDAL.IsDBNULL(TEN_DIA_DIEM_TCVN, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_DIA_CHIUpdated ? string.Format(",DIA_CHI = {0}", clsDAL.IsDBNULL(DIA_CHI, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_BANGUpdated ? string.Format(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_MA_HQUpdated ? string.Format(",MA_HQ = {0}", clsDAL.IsDBNULL(MA_HQ, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_GHI_CHUUpdated ? string.Format(",GHI_CHU = {0}", clsDAL.IsDBNULL(GHI_CHU, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_DIA_DIEM_VNUpdated ? string.Format(",TEN_DIA_DIEM_VN = {0}", clsDAL.IsDBNULL(TEN_DIA_DIEM_VN, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_DIA_DIEM_TCVN_VNUpdated ? string.Format(",TEN_DIA_DIEM_TCVN_VN = {0}", clsDAL.IsDBNULL(TEN_DIA_DIEM_TCVN_VN, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_HienThiUpdated ? string.Format(",HienThi = {0}", clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_DIA_BANUpdated ? string.Format(",DIA_BAN = {0}", clsDAL.IsDBNULL(DIA_BAN, ProType.STRING, this.DataManagement)) : string.Empty);
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
			sbSQL.AppendFormat("TEN_DIA_DIEM = {0}", clsDAL.IsDBNULL(TEN_DIA_DIEM, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_DIA_DIEM_TCVN = {0}", clsDAL.IsDBNULL(TEN_DIA_DIEM_TCVN, ProType.STRING, this.DataManagement)).AppendFormat(",DIA_CHI = {0}", clsDAL.IsDBNULL(DIA_CHI, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).AppendFormat(",MA_HQ = {0}", clsDAL.IsDBNULL(MA_HQ, ProType.STRING, this.DataManagement)).AppendFormat(",GHI_CHU = {0}", clsDAL.IsDBNULL(GHI_CHU, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_DIA_DIEM_VN = {0}", clsDAL.IsDBNULL(TEN_DIA_DIEM_VN, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_DIA_DIEM_TCVN_VN = {0}", clsDAL.IsDBNULL(TEN_DIA_DIEM_TCVN_VN, ProType.STRING, this.DataManagement)).AppendFormat(",HienThi = {0}", clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)).AppendFormat(",DIA_BAN = {0}", clsDAL.IsDBNULL(DIA_BAN, ProType.STRING, this.DataManagement));
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
			return clsDAL.DeleteString("SDIA_DIEM", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
			sbSQL.AppendFormat("MA_DIA_DIEM = {0}", clsDAL.IsDBNULL(MA_DIA_DIEM, ProType.STRING, this.DataManagement));
			return sbSQL.ToString();
		}
	}
}