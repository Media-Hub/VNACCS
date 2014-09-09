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
	/// Generated Class for Table : SPTVT.
	/// </summary>
	public partial class SPTVT : TableBase
	{
		public SPTVT() : base(){}

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
		private string m_MA_PTVT;
		/// <summary>
		/// MA_PTVT.
		/// </summary>
		public string MA_PTVT
		{
			get
			{
				return m_MA_PTVT;
			}
			set
			{
				if ((this.m_MA_PTVT != value))
				{
					this.SendPropertyChanging("MA_PTVT");
					this.m_MA_PTVT = value;
					this.SendPropertyChanged("MA_PTVT");
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

		private string m_TEN_PTVT1;
		private bool m_TEN_PTVT1Updated = false;
		/// <summary>
		/// TEN_PTVT1.
		/// </summary>
		public string TEN_PTVT1
		{
			get
			{
				return m_TEN_PTVT1;
			}
			set
			{
				if ((this.m_TEN_PTVT1 != value))
				{
					this.SendPropertyChanging("TEN_PTVT1");
					this.m_TEN_PTVT1 = value;
					this.SendPropertyChanged("TEN_PTVT1");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_PTVT1Updated = true;
				}
			}
		}

		private int? m_SP_SOTK;
		private bool m_SP_SOTKUpdated = false;
		/// <summary>
		/// SP_SOTK.
		/// </summary>
		public int? SP_SOTK
		{
			get
			{
				return m_SP_SOTK;
			}
			set
			{
				if ((this.m_SP_SOTK != value))
				{
					this.SendPropertyChanging("SP_SOTK");
					this.m_SP_SOTK = value;
					this.SendPropertyChanged("SP_SOTK");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_SP_SOTKUpdated = true;
				}
			}
		}

		private string m_SP_MA_LH;
		private bool m_SP_MA_LHUpdated = false;
		/// <summary>
		/// SP_MA_LH.
		/// </summary>
		public string SP_MA_LH
		{
			get
			{
				return m_SP_MA_LH;
			}
			set
			{
				if ((this.m_SP_MA_LH != value))
				{
					this.SendPropertyChanging("SP_MA_LH");
					this.m_SP_MA_LH = value;
					this.SendPropertyChanged("SP_MA_LH");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_SP_MA_LHUpdated = true;
				}
			}
		}

		private string m_SP_MA_HQ;
		private bool m_SP_MA_HQUpdated = false;
		/// <summary>
		/// SP_MA_HQ.
		/// </summary>
		public string SP_MA_HQ
		{
			get
			{
				return m_SP_MA_HQ;
			}
			set
			{
				if ((this.m_SP_MA_HQ != value))
				{
					this.SendPropertyChanging("SP_MA_HQ");
					this.m_SP_MA_HQ = value;
					this.SendPropertyChanged("SP_MA_HQ");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_SP_MA_HQUpdated = true;
				}
			}
		}

		private int? m_SP_NAMDK;
		private bool m_SP_NAMDKUpdated = false;
		/// <summary>
		/// SP_NAMDK.
		/// </summary>
		public int? SP_NAMDK
		{
			get
			{
				return m_SP_NAMDK;
			}
			set
			{
				if ((this.m_SP_NAMDK != value))
				{
					this.SendPropertyChanging("SP_NAMDK");
					this.m_SP_NAMDK = value;
					this.SendPropertyChanged("SP_NAMDK");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_SP_NAMDKUpdated = true;
				}
			}
		}

		private string m_TEN_PTVT_VN;
		private bool m_TEN_PTVT_VNUpdated = false;
		/// <summary>
		/// TEN_PTVT_VN.
		/// </summary>
		public string TEN_PTVT_VN
		{
			get
			{
				return m_TEN_PTVT_VN;
			}
			set
			{
				if ((this.m_TEN_PTVT_VN != value))
				{
					this.SendPropertyChanging("TEN_PTVT_VN");
					this.m_TEN_PTVT_VN = value;
					this.SendPropertyChanged("TEN_PTVT_VN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_PTVT_VNUpdated = true;
				}
			}
		}

		private string m_TEN_PTVT1_VN;
		private bool m_TEN_PTVT1_VNUpdated = false;
		/// <summary>
		/// TEN_PTVT1_VN.
		/// </summary>
		public string TEN_PTVT1_VN
		{
			get
			{
				return m_TEN_PTVT1_VN;
			}
			set
			{
				if ((this.m_TEN_PTVT1_VN != value))
				{
					this.SendPropertyChanging("TEN_PTVT1_VN");
					this.m_TEN_PTVT1_VN = value;
					this.SendPropertyChanged("TEN_PTVT1_VN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_PTVT1_VNUpdated = true;
				}
			}
		}

		private int? m_HIENTHI;
		private bool m_HIENTHIUpdated = false;
		/// <summary>
		/// HIENTHI.
		/// </summary>
		public int? HIENTHI
		{
			get
			{
				return m_HIENTHI;
			}
			set
			{
				if ((this.m_HIENTHI != value))
				{
					this.SendPropertyChanging("HIENTHI");
					this.m_HIENTHI = value;
					this.SendPropertyChanged("HIENTHI");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HIENTHIUpdated = true;
				}
			}
		}

		private int? m_IsVNACCS;
		private bool m_IsVNACCSUpdated = false;
		/// <summary>
		/// IsVNACCS.
		/// </summary>
		public int? IsVNACCS
		{
			get
			{
				return m_IsVNACCS;
			}
			set
			{
				if ((this.m_IsVNACCS != value))
				{
					this.SendPropertyChanging("IsVNACCS");
					this.m_IsVNACCS = value;
					this.SendPropertyChanged("IsVNACCS");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_IsVNACCSUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SPTVT " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(clsDAL.SelectField("MA_PTVT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_PTVT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_PTVT1", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("SP_SOTK", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("SP_MA_LH", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("SP_MA_HQ", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("SP_NAMDK", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_PTVT_VN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_PTVT1_VN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HIENTHI", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("IsVNACCS", ProType.OTHER, this.DataManagement));
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
			sbSQL.Append("INSERT INTO SPTVT (MA_PTVT, TEN_PTVT, TEN_PTVT1, SP_SOTK, SP_MA_LH, SP_MA_HQ, SP_NAMDK, TEN_PTVT_VN, TEN_PTVT1_VN, HIENTHI, IsVNACCS) VALUES(").Append(clsDAL.IsDBNULL(MA_PTVT, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_PTVT, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_PTVT1, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(SP_SOTK, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(SP_MA_LH, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(SP_MA_HQ, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(SP_NAMDK, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_PTVT_VN, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_PTVT1_VN, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(HIENTHI, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(IsVNACCS, ProType.OTHER, this.DataManagement)).Append(")");
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SPTVT SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(m_TEN_PTVTUpdated ? string.Format(",TEN_PTVT = {0}", clsDAL.IsDBNULL(TEN_PTVT, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_PTVT1Updated ? string.Format(",TEN_PTVT1 = {0}", clsDAL.IsDBNULL(TEN_PTVT1, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_SP_SOTKUpdated ? string.Format(",SP_SOTK = {0}", clsDAL.IsDBNULL(SP_SOTK, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_SP_MA_LHUpdated ? string.Format(",SP_MA_LH = {0}", clsDAL.IsDBNULL(SP_MA_LH, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_SP_MA_HQUpdated ? string.Format(",SP_MA_HQ = {0}", clsDAL.IsDBNULL(SP_MA_HQ, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_SP_NAMDKUpdated ? string.Format(",SP_NAMDK = {0}", clsDAL.IsDBNULL(SP_NAMDK, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_TEN_PTVT_VNUpdated ? string.Format(",TEN_PTVT_VN = {0}", clsDAL.IsDBNULL(TEN_PTVT_VN, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_PTVT1_VNUpdated ? string.Format(",TEN_PTVT1_VN = {0}", clsDAL.IsDBNULL(TEN_PTVT1_VN, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_HIENTHIUpdated ? string.Format(",HIENTHI = {0}", clsDAL.IsDBNULL(HIENTHI, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_IsVNACCSUpdated ? string.Format(",IsVNACCS = {0}", clsDAL.IsDBNULL(IsVNACCS, ProType.OTHER, this.DataManagement)) : string.Empty);
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
			sbSQL.AppendFormat("TEN_PTVT = {0}", clsDAL.IsDBNULL(TEN_PTVT, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_PTVT1 = {0}", clsDAL.IsDBNULL(TEN_PTVT1, ProType.STRING, this.DataManagement)).AppendFormat(",SP_SOTK = {0}", clsDAL.IsDBNULL(SP_SOTK, ProType.OTHER, this.DataManagement)).AppendFormat(",SP_MA_LH = {0}", clsDAL.IsDBNULL(SP_MA_LH, ProType.STRING, this.DataManagement)).AppendFormat(",SP_MA_HQ = {0}", clsDAL.IsDBNULL(SP_MA_HQ, ProType.STRING, this.DataManagement)).AppendFormat(",SP_NAMDK = {0}", clsDAL.IsDBNULL(SP_NAMDK, ProType.OTHER, this.DataManagement)).AppendFormat(",TEN_PTVT_VN = {0}", clsDAL.IsDBNULL(TEN_PTVT_VN, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_PTVT1_VN = {0}", clsDAL.IsDBNULL(TEN_PTVT1_VN, ProType.STRING, this.DataManagement)).AppendFormat(",HIENTHI = {0}", clsDAL.IsDBNULL(HIENTHI, ProType.OTHER, this.DataManagement)).AppendFormat(",IsVNACCS = {0}", clsDAL.IsDBNULL(IsVNACCS, ProType.OTHER, this.DataManagement));
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
			return clsDAL.DeleteString("SPTVT", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
			sbSQL.AppendFormat("MA_PTVT = {0}", clsDAL.IsDBNULL(MA_PTVT, ProType.STRING, this.DataManagement));
			return sbSQL.ToString();
		}
	}
}