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
	/// Generated Class for Table : SLOAI_KIEN.
	/// </summary>
	public partial class SLOAI_KIEN : TableBase
	{
		public SLOAI_KIEN() : base(){}

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
		private string m_MA_LK;
		/// <summary>
		/// MA_LK.
		/// </summary>
		public string MA_LK
		{
			get
			{
				return m_MA_LK;
			}
			set
			{
				if ((this.m_MA_LK != value))
				{
					this.SendPropertyChanging("MA_LK");
					this.m_MA_LK = value;
					this.SendPropertyChanged("MA_LK");
				}
			}
		}

		private string m_TEN_LK;
		private bool m_TEN_LKUpdated = false;
		/// <summary>
		/// TEN_LK.
		/// </summary>
		public string TEN_LK
		{
			get
			{
				return m_TEN_LK;
			}
			set
			{
				if ((this.m_TEN_LK != value))
				{
					this.SendPropertyChanging("TEN_LK");
					this.m_TEN_LK = value;
					this.SendPropertyChanged("TEN_LK");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_LKUpdated = true;
				}
			}
		}

		private string m_TEN_LK1;
		private bool m_TEN_LK1Updated = false;
		/// <summary>
		/// TEN_LK1.
		/// </summary>
		public string TEN_LK1
		{
			get
			{
				return m_TEN_LK1;
			}
			set
			{
				if ((this.m_TEN_LK1 != value))
				{
					this.SendPropertyChanging("TEN_LK1");
					this.m_TEN_LK1 = value;
					this.SendPropertyChanged("TEN_LK1");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_LK1Updated = true;
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

		private int? m_isVNACCS;
		private bool m_isVNACCSUpdated = false;
		/// <summary>
		/// isVNACCS.
		/// </summary>
		public int? isVNACCS
		{
			get
			{
				return m_isVNACCS;
			}
			set
			{
				if ((this.m_isVNACCS != value))
				{
					this.SendPropertyChanging("isVNACCS");
					this.m_isVNACCS = value;
					this.SendPropertyChanged("isVNACCS");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_isVNACCSUpdated = true;
				}
			}
		}

		private string m_TEN_LK_VN;
		private bool m_TEN_LK_VNUpdated = false;
		/// <summary>
		/// TEN_LK_VN.
		/// </summary>
		public string TEN_LK_VN
		{
			get
			{
				return m_TEN_LK_VN;
			}
			set
			{
				if ((this.m_TEN_LK_VN != value))
				{
					this.SendPropertyChanging("TEN_LK_VN");
					this.m_TEN_LK_VN = value;
					this.SendPropertyChanged("TEN_LK_VN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_LK_VNUpdated = true;
				}
			}
		}

		private string m_TEN_LK1_VN;
		private bool m_TEN_LK1_VNUpdated = false;
		/// <summary>
		/// TEN_LK1_VN.
		/// </summary>
		public string TEN_LK1_VN
		{
			get
			{
				return m_TEN_LK1_VN;
			}
			set
			{
				if ((this.m_TEN_LK1_VN != value))
				{
					this.SendPropertyChanging("TEN_LK1_VN");
					this.m_TEN_LK1_VN = value;
					this.SendPropertyChanged("TEN_LK1_VN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_LK1_VNUpdated = true;
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

		private string m_MA_CU;
		private bool m_MA_CUUpdated = false;
		/// <summary>
		/// MA_CU.
		/// </summary>
		public string MA_CU
		{
			get
			{
				return m_MA_CU;
			}
			set
			{
				if ((this.m_MA_CU != value))
				{
					this.SendPropertyChanging("MA_CU");
					this.m_MA_CU = value;
					this.SendPropertyChanged("MA_CU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_MA_CUUpdated = true;
				}
			}
		}

		private string m_TEN_CU;
		private bool m_TEN_CUUpdated = false;
		/// <summary>
		/// TEN_CU.
		/// </summary>
		public string TEN_CU
		{
			get
			{
				return m_TEN_CU;
			}
			set
			{
				if ((this.m_TEN_CU != value))
				{
					this.SendPropertyChanging("TEN_CU");
					this.m_TEN_CU = value;
					this.SendPropertyChanged("TEN_CU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_CUUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SLOAI_KIEN " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(clsDAL.SelectField("MA_LK", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_LK", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_LK1", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_BANG", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("isVNACCS", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_LK_VN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_LK1_VN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HienThi", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("MA_CU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_CU", ProType.OTHER, this.DataManagement));
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
			sbSQL.Append("INSERT INTO SLOAI_KIEN (MA_LK, TEN_LK, TEN_LK1, TEN_BANG, isVNACCS, TEN_LK_VN, TEN_LK1_VN, HienThi, MA_CU, TEN_CU) VALUES(").Append(clsDAL.IsDBNULL(MA_LK, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_LK, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_LK1, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(isVNACCS, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_LK_VN, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_LK1_VN, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(MA_CU, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_CU, ProType.STRING, this.DataManagement)).Append(")");
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SLOAI_KIEN SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(m_TEN_LKUpdated ? string.Format(",TEN_LK = {0}", clsDAL.IsDBNULL(TEN_LK, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_LK1Updated ? string.Format(",TEN_LK1 = {0}", clsDAL.IsDBNULL(TEN_LK1, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_BANGUpdated ? string.Format(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_isVNACCSUpdated ? string.Format(",isVNACCS = {0}", clsDAL.IsDBNULL(isVNACCS, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_TEN_LK_VNUpdated ? string.Format(",TEN_LK_VN = {0}", clsDAL.IsDBNULL(TEN_LK_VN, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_LK1_VNUpdated ? string.Format(",TEN_LK1_VN = {0}", clsDAL.IsDBNULL(TEN_LK1_VN, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_HienThiUpdated ? string.Format(",HienThi = {0}", clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_MA_CUUpdated ? string.Format(",MA_CU = {0}", clsDAL.IsDBNULL(MA_CU, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_CUUpdated ? string.Format(",TEN_CU = {0}", clsDAL.IsDBNULL(TEN_CU, ProType.STRING, this.DataManagement)) : string.Empty);
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
			sbSQL.AppendFormat("TEN_LK = {0}", clsDAL.IsDBNULL(TEN_LK, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_LK1 = {0}", clsDAL.IsDBNULL(TEN_LK1, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).AppendFormat(",isVNACCS = {0}", clsDAL.IsDBNULL(isVNACCS, ProType.OTHER, this.DataManagement)).AppendFormat(",TEN_LK_VN = {0}", clsDAL.IsDBNULL(TEN_LK_VN, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_LK1_VN = {0}", clsDAL.IsDBNULL(TEN_LK1_VN, ProType.STRING, this.DataManagement)).AppendFormat(",HienThi = {0}", clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)).AppendFormat(",MA_CU = {0}", clsDAL.IsDBNULL(MA_CU, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_CU = {0}", clsDAL.IsDBNULL(TEN_CU, ProType.STRING, this.DataManagement));
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
			return clsDAL.DeleteString("SLOAI_KIEN", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
			sbSQL.AppendFormat("MA_LK = {0}", clsDAL.IsDBNULL(MA_LK, ProType.STRING, this.DataManagement));
			return sbSQL.ToString();
		}
	}
}