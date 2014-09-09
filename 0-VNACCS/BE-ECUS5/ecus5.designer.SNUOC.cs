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
	/// Generated Class for Table : SNUOC.
	/// </summary>
	public partial class SNUOC : TableBase
	{
		public SNUOC() : base(){}

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
		private string m_MA_NUOC;
		/// <summary>
		/// MA_NUOC.
		/// </summary>
		public string MA_NUOC
		{
			get
			{
				return m_MA_NUOC;
			}
			set
			{
				if ((this.m_MA_NUOC != value))
				{
					this.SendPropertyChanging("MA_NUOC");
					this.m_MA_NUOC = value;
					this.SendPropertyChanged("MA_NUOC");
				}
			}
		}

		private string m_TEN_NUOC;
		private bool m_TEN_NUOCUpdated = false;
		/// <summary>
		/// TEN_NUOC.
		/// </summary>
		public string TEN_NUOC
		{
			get
			{
				return m_TEN_NUOC;
			}
			set
			{
				if ((this.m_TEN_NUOC != value))
				{
					this.SendPropertyChanging("TEN_NUOC");
					this.m_TEN_NUOC = value;
					this.SendPropertyChanged("TEN_NUOC");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_NUOCUpdated = true;
				}
			}
		}

		private string m_TEN_NUOC1;
		private bool m_TEN_NUOC1Updated = false;
		/// <summary>
		/// TEN_NUOC1.
		/// </summary>
		public string TEN_NUOC1
		{
			get
			{
				return m_TEN_NUOC1;
			}
			set
			{
				if ((this.m_TEN_NUOC1 != value))
				{
					this.SendPropertyChanging("TEN_NUOC1");
					this.m_TEN_NUOC1 = value;
					this.SendPropertyChanged("TEN_NUOC1");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_NUOC1Updated = true;
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

		private string m_MA_NT;
		private bool m_MA_NTUpdated = false;
		/// <summary>
		/// MA_NT.
		/// </summary>
		public string MA_NT
		{
			get
			{
				return m_MA_NT;
			}
			set
			{
				if ((this.m_MA_NT != value))
				{
					this.SendPropertyChanging("MA_NT");
					this.m_MA_NT = value;
					this.SendPropertyChanged("MA_NT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_MA_NTUpdated = true;
				}
			}
		}

		private string m_TEN_NUOC_VN;
		private bool m_TEN_NUOC_VNUpdated = false;
		/// <summary>
		/// TEN_NUOC_VN.
		/// </summary>
		public string TEN_NUOC_VN
		{
			get
			{
				return m_TEN_NUOC_VN;
			}
			set
			{
				if ((this.m_TEN_NUOC_VN != value))
				{
					this.SendPropertyChanging("TEN_NUOC_VN");
					this.m_TEN_NUOC_VN = value;
					this.SendPropertyChanged("TEN_NUOC_VN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_NUOC_VNUpdated = true;
				}
			}
		}

		private string m_TEN_NUOC1_VN;
		private bool m_TEN_NUOC1_VNUpdated = false;
		/// <summary>
		/// TEN_NUOC1_VN.
		/// </summary>
		public string TEN_NUOC1_VN
		{
			get
			{
				return m_TEN_NUOC1_VN;
			}
			set
			{
				if ((this.m_TEN_NUOC1_VN != value))
				{
					this.SendPropertyChanging("TEN_NUOC1_VN");
					this.m_TEN_NUOC1_VN = value;
					this.SendPropertyChanged("TEN_NUOC1_VN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_NUOC1_VNUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SNUOC " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(clsDAL.SelectField("MA_NUOC", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_NUOC", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_NUOC1", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_BANG", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HienThi", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("MA_NT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_NUOC_VN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_NUOC1_VN", ProType.OTHER, this.DataManagement));
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
			sbSQL.Append("INSERT INTO SNUOC (MA_NUOC, TEN_NUOC, TEN_NUOC1, TEN_BANG, HienThi, MA_NT, TEN_NUOC_VN, TEN_NUOC1_VN) VALUES(").Append(clsDAL.IsDBNULL(MA_NUOC, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_NUOC, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_NUOC1, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(MA_NT, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_NUOC_VN, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_NUOC1_VN, ProType.STRING, this.DataManagement)).Append(")");
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SNUOC SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(m_TEN_NUOCUpdated ? string.Format(",TEN_NUOC = {0}", clsDAL.IsDBNULL(TEN_NUOC, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_NUOC1Updated ? string.Format(",TEN_NUOC1 = {0}", clsDAL.IsDBNULL(TEN_NUOC1, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_BANGUpdated ? string.Format(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_HienThiUpdated ? string.Format(",HienThi = {0}", clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_MA_NTUpdated ? string.Format(",MA_NT = {0}", clsDAL.IsDBNULL(MA_NT, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_NUOC_VNUpdated ? string.Format(",TEN_NUOC_VN = {0}", clsDAL.IsDBNULL(TEN_NUOC_VN, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_NUOC1_VNUpdated ? string.Format(",TEN_NUOC1_VN = {0}", clsDAL.IsDBNULL(TEN_NUOC1_VN, ProType.STRING, this.DataManagement)) : string.Empty);
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
			sbSQL.AppendFormat("TEN_NUOC = {0}", clsDAL.IsDBNULL(TEN_NUOC, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_NUOC1 = {0}", clsDAL.IsDBNULL(TEN_NUOC1, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).AppendFormat(",HienThi = {0}", clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)).AppendFormat(",MA_NT = {0}", clsDAL.IsDBNULL(MA_NT, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_NUOC_VN = {0}", clsDAL.IsDBNULL(TEN_NUOC_VN, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_NUOC1_VN = {0}", clsDAL.IsDBNULL(TEN_NUOC1_VN, ProType.STRING, this.DataManagement));
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
			return clsDAL.DeleteString("SNUOC", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
			sbSQL.AppendFormat("MA_NUOC = {0}", clsDAL.IsDBNULL(MA_NUOC, ProType.STRING, this.DataManagement));
			return sbSQL.ToString();
		}
	}
}