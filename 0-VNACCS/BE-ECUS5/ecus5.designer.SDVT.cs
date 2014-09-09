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
	/// Generated Class for Table : SDVT.
	/// </summary>
	public partial class SDVT : TableBase
	{
		public SDVT() : base(){}

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
		private string m_MA_DVT;
		/// <summary>
		/// MA_DVT.
		/// </summary>
		public string MA_DVT
		{
			get
			{
				return m_MA_DVT;
			}
			set
			{
				if ((this.m_MA_DVT != value))
				{
					this.SendPropertyChanging("MA_DVT");
					this.m_MA_DVT = value;
					this.SendPropertyChanged("MA_DVT");
				}
			}
		}

		private string m_TEN_DVT;
		private bool m_TEN_DVTUpdated = false;
		/// <summary>
		/// TEN_DVT.
		/// </summary>
		public string TEN_DVT
		{
			get
			{
				return m_TEN_DVT;
			}
			set
			{
				if ((this.m_TEN_DVT != value))
				{
					this.SendPropertyChanging("TEN_DVT");
					this.m_TEN_DVT = value;
					this.SendPropertyChanged("TEN_DVT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_DVTUpdated = true;
				}
			}
		}

		private string m_TEN_DVT1;
		private bool m_TEN_DVT1Updated = false;
		/// <summary>
		/// TEN_DVT1.
		/// </summary>
		public string TEN_DVT1
		{
			get
			{
				return m_TEN_DVT1;
			}
			set
			{
				if ((this.m_TEN_DVT1 != value))
				{
					this.SendPropertyChanging("TEN_DVT1");
					this.m_TEN_DVT1 = value;
					this.SendPropertyChanged("TEN_DVT1");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_DVT1Updated = true;
				}
			}
		}

		private string m_MA_STD;
		private bool m_MA_STDUpdated = false;
		/// <summary>
		/// MA_STD.
		/// </summary>
		public string MA_STD
		{
			get
			{
				return m_MA_STD;
			}
			set
			{
				if ((this.m_MA_STD != value))
				{
					this.SendPropertyChanging("MA_STD");
					this.m_MA_STD = value;
					this.SendPropertyChanged("MA_STD");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_MA_STDUpdated = true;
				}
			}
		}

		private decimal? m_TL_QUYDOI;
		private bool m_TL_QUYDOIUpdated = false;
		/// <summary>
		/// TL_QUYDOI.
		/// </summary>
		public decimal? TL_QUYDOI
		{
			get
			{
				return m_TL_QUYDOI;
			}
			set
			{
				if ((this.m_TL_QUYDOI != value))
				{
					this.SendPropertyChanging("TL_QUYDOI");
					this.m_TL_QUYDOI = value;
					this.SendPropertyChanged("TL_QUYDOI");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TL_QUYDOIUpdated = true;
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

		private string m_TEN_DVT_VN;
		private bool m_TEN_DVT_VNUpdated = false;
		/// <summary>
		/// TEN_DVT_VN.
		/// </summary>
		public string TEN_DVT_VN
		{
			get
			{
				return m_TEN_DVT_VN;
			}
			set
			{
				if ((this.m_TEN_DVT_VN != value))
				{
					this.SendPropertyChanging("TEN_DVT_VN");
					this.m_TEN_DVT_VN = value;
					this.SendPropertyChanged("TEN_DVT_VN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_DVT_VNUpdated = true;
				}
			}
		}

		private string m_TEN_DVT1_VN;
		private bool m_TEN_DVT1_VNUpdated = false;
		/// <summary>
		/// TEN_DVT1_VN.
		/// </summary>
		public string TEN_DVT1_VN
		{
			get
			{
				return m_TEN_DVT1_VN;
			}
			set
			{
				if ((this.m_TEN_DVT1_VN != value))
				{
					this.SendPropertyChanging("TEN_DVT1_VN");
					this.m_TEN_DVT1_VN = value;
					this.SendPropertyChanged("TEN_DVT1_VN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_DVT1_VNUpdated = true;
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

		private string m_ItemType;
		private bool m_ItemTypeUpdated = false;
		/// <summary>
		/// ItemType.
		/// </summary>
		public string ItemType
		{
			get
			{
				return m_ItemType;
			}
			set
			{
				if ((this.m_ItemType != value))
				{
					this.SendPropertyChanging("ItemType");
					this.m_ItemType = value;
					this.SendPropertyChanged("ItemType");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_ItemTypeUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SDVT " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(clsDAL.SelectField("MA_DVT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_DVT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_DVT1", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("MA_STD", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TL_QUYDOI", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_BANG", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("isVNACCS", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("MA_CU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_CU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_DVT_VN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_DVT1_VN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HienThi", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("ItemType", ProType.OTHER, this.DataManagement));
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
			sbSQL.Append("INSERT INTO SDVT (MA_DVT, TEN_DVT, TEN_DVT1, MA_STD, TL_QUYDOI, TEN_BANG, isVNACCS, MA_CU, TEN_CU, TEN_DVT_VN, TEN_DVT1_VN, HienThi, ItemType) VALUES(").Append(clsDAL.IsDBNULL(MA_DVT, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_DVT, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_DVT1, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(MA_STD, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TL_QUYDOI, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(isVNACCS, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(MA_CU, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_CU, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_DVT_VN, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_DVT1_VN, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(ItemType, ProType.STRING, this.DataManagement)).Append(")");
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SDVT SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(m_TEN_DVTUpdated ? string.Format(",TEN_DVT = {0}", clsDAL.IsDBNULL(TEN_DVT, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_DVT1Updated ? string.Format(",TEN_DVT1 = {0}", clsDAL.IsDBNULL(TEN_DVT1, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_MA_STDUpdated ? string.Format(",MA_STD = {0}", clsDAL.IsDBNULL(MA_STD, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TL_QUYDOIUpdated ? string.Format(",TL_QUYDOI = {0}", clsDAL.IsDBNULL(TL_QUYDOI, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_TEN_BANGUpdated ? string.Format(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_isVNACCSUpdated ? string.Format(",isVNACCS = {0}", clsDAL.IsDBNULL(isVNACCS, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_MA_CUUpdated ? string.Format(",MA_CU = {0}", clsDAL.IsDBNULL(MA_CU, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_CUUpdated ? string.Format(",TEN_CU = {0}", clsDAL.IsDBNULL(TEN_CU, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_DVT_VNUpdated ? string.Format(",TEN_DVT_VN = {0}", clsDAL.IsDBNULL(TEN_DVT_VN, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_DVT1_VNUpdated ? string.Format(",TEN_DVT1_VN = {0}", clsDAL.IsDBNULL(TEN_DVT1_VN, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_HienThiUpdated ? string.Format(",HienThi = {0}", clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_ItemTypeUpdated ? string.Format(",ItemType = {0}", clsDAL.IsDBNULL(ItemType, ProType.STRING, this.DataManagement)) : string.Empty);
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
			sbSQL.AppendFormat("TEN_DVT = {0}", clsDAL.IsDBNULL(TEN_DVT, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_DVT1 = {0}", clsDAL.IsDBNULL(TEN_DVT1, ProType.STRING, this.DataManagement)).AppendFormat(",MA_STD = {0}", clsDAL.IsDBNULL(MA_STD, ProType.STRING, this.DataManagement)).AppendFormat(",TL_QUYDOI = {0}", clsDAL.IsDBNULL(TL_QUYDOI, ProType.OTHER, this.DataManagement)).AppendFormat(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).AppendFormat(",isVNACCS = {0}", clsDAL.IsDBNULL(isVNACCS, ProType.OTHER, this.DataManagement)).AppendFormat(",MA_CU = {0}", clsDAL.IsDBNULL(MA_CU, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_CU = {0}", clsDAL.IsDBNULL(TEN_CU, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_DVT_VN = {0}", clsDAL.IsDBNULL(TEN_DVT_VN, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_DVT1_VN = {0}", clsDAL.IsDBNULL(TEN_DVT1_VN, ProType.STRING, this.DataManagement)).AppendFormat(",HienThi = {0}", clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)).AppendFormat(",ItemType = {0}", clsDAL.IsDBNULL(ItemType, ProType.STRING, this.DataManagement));
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
			return clsDAL.DeleteString("SDVT", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
			sbSQL.AppendFormat("MA_DVT = {0}", clsDAL.IsDBNULL(MA_DVT, ProType.STRING, this.DataManagement));
			return sbSQL.ToString();
		}
	}
}