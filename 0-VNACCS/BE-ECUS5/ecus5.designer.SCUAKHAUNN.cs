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
	/// Generated Class for Table : SCUAKHAUNN.
	/// </summary>
	public partial class SCUAKHAUNN : TableBase
	{
		public SCUAKHAUNN() : base(){}

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
		private string m_MA_CK;
		/// <summary>
		/// MA_CK.
		/// </summary>
		public string MA_CK
		{
			get
			{
				return m_MA_CK;
			}
			set
			{
				if ((this.m_MA_CK != value))
				{
					this.SendPropertyChanging("MA_CK");
					this.m_MA_CK = value;
					this.SendPropertyChanged("MA_CK");
				}
			}
		}

		private string m_TEN_CK;
		private bool m_TEN_CKUpdated = false;
		/// <summary>
		/// TEN_CK.
		/// </summary>
		public string TEN_CK
		{
			get
			{
				return m_TEN_CK;
			}
			set
			{
				if ((this.m_TEN_CK != value))
				{
					this.SendPropertyChanging("TEN_CK");
					this.m_TEN_CK = value;
					this.SendPropertyChanged("TEN_CK");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_CKUpdated = true;
				}
			}
		}

		private string m_TEN_CK1;
		private bool m_TEN_CK1Updated = false;
		/// <summary>
		/// TEN_CK1.
		/// </summary>
		public string TEN_CK1
		{
			get
			{
				return m_TEN_CK1;
			}
			set
			{
				if ((this.m_TEN_CK1 != value))
				{
					this.SendPropertyChanging("TEN_CK1");
					this.m_TEN_CK1 = value;
					this.SendPropertyChanged("TEN_CK1");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_CK1Updated = true;
				}
			}
		}

		private string m_MA_CUC;
		private bool m_MA_CUCUpdated = false;
		/// <summary>
		/// MA_CUC.
		/// </summary>
		public string MA_CUC
		{
			get
			{
				return m_MA_CUC;
			}
			set
			{
				if ((this.m_MA_CUC != value))
				{
					this.SendPropertyChanging("MA_CUC");
					this.m_MA_CUC = value;
					this.SendPropertyChanged("MA_CUC");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_MA_CUCUpdated = true;
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

		private string m_MA_NUOC;
		private bool m_MA_NUOCUpdated = false;
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
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_MA_NUOCUpdated = true;
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

		private string m_TEN_CK_VN;
		private bool m_TEN_CK_VNUpdated = false;
		/// <summary>
		/// TEN_CK_VN.
		/// </summary>
		public string TEN_CK_VN
		{
			get
			{
				return m_TEN_CK_VN;
			}
			set
			{
				if ((this.m_TEN_CK_VN != value))
				{
					this.SendPropertyChanging("TEN_CK_VN");
					this.m_TEN_CK_VN = value;
					this.SendPropertyChanged("TEN_CK_VN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_CK_VNUpdated = true;
				}
			}
		}

		private string m_TEN_CK1_VN;
		private bool m_TEN_CK1_VNUpdated = false;
		/// <summary>
		/// TEN_CK1_VN.
		/// </summary>
		public string TEN_CK1_VN
		{
			get
			{
				return m_TEN_CK1_VN;
			}
			set
			{
				if ((this.m_TEN_CK1_VN != value))
				{
					this.SendPropertyChanging("TEN_CK1_VN");
					this.m_TEN_CK1_VN = value;
					this.SendPropertyChanged("TEN_CK1_VN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_CK1_VNUpdated = true;
				}
			}
		}

		private int? m_ISVNACCS;
		private bool m_ISVNACCSUpdated = false;
		/// <summary>
		/// ISVNACCS.
		/// </summary>
		public int? ISVNACCS
		{
			get
			{
				return m_ISVNACCS;
			}
			set
			{
				if ((this.m_ISVNACCS != value))
				{
					this.SendPropertyChanging("ISVNACCS");
					this.m_ISVNACCS = value;
					this.SendPropertyChanged("ISVNACCS");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_ISVNACCSUpdated = true;
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
			return "SELECT " + Fields + " FROM SCUAKHAUNN " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(clsDAL.SelectField("MA_CK", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_CK", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_CK1", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("MA_CUC", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HienThi", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_BANG", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("MA_NUOC", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("MA_CU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_CU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_CK_VN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_CK1_VN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("ISVNACCS", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("ItemType", ProType.OTHER, this.DataManagement));
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
			sbSQL.Append("INSERT INTO SCUAKHAUNN (MA_CK, TEN_CK, TEN_CK1, MA_CUC, HienThi, TEN_BANG, MA_NUOC, MA_CU, TEN_CU, TEN_CK_VN, TEN_CK1_VN, ISVNACCS, ItemType) VALUES(").Append(clsDAL.IsDBNULL(MA_CK, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_CK, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_CK1, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(MA_CUC, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(MA_NUOC, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(MA_CU, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_CU, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_CK_VN, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_CK1_VN, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(ISVNACCS, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(ItemType, ProType.STRING, this.DataManagement)).Append(")");
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SCUAKHAUNN SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(m_TEN_CKUpdated ? string.Format(",TEN_CK = {0}", clsDAL.IsDBNULL(TEN_CK, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_CK1Updated ? string.Format(",TEN_CK1 = {0}", clsDAL.IsDBNULL(TEN_CK1, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_MA_CUCUpdated ? string.Format(",MA_CUC = {0}", clsDAL.IsDBNULL(MA_CUC, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_HienThiUpdated ? string.Format(",HienThi = {0}", clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_TEN_BANGUpdated ? string.Format(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_MA_NUOCUpdated ? string.Format(",MA_NUOC = {0}", clsDAL.IsDBNULL(MA_NUOC, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_MA_CUUpdated ? string.Format(",MA_CU = {0}", clsDAL.IsDBNULL(MA_CU, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_CUUpdated ? string.Format(",TEN_CU = {0}", clsDAL.IsDBNULL(TEN_CU, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_CK_VNUpdated ? string.Format(",TEN_CK_VN = {0}", clsDAL.IsDBNULL(TEN_CK_VN, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_CK1_VNUpdated ? string.Format(",TEN_CK1_VN = {0}", clsDAL.IsDBNULL(TEN_CK1_VN, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_ISVNACCSUpdated ? string.Format(",ISVNACCS = {0}", clsDAL.IsDBNULL(ISVNACCS, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_ItemTypeUpdated ? string.Format(",ItemType = {0}", clsDAL.IsDBNULL(ItemType, ProType.STRING, this.DataManagement)) : string.Empty);
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
			sbSQL.AppendFormat("TEN_CK = {0}", clsDAL.IsDBNULL(TEN_CK, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_CK1 = {0}", clsDAL.IsDBNULL(TEN_CK1, ProType.STRING, this.DataManagement)).AppendFormat(",MA_CUC = {0}", clsDAL.IsDBNULL(MA_CUC, ProType.STRING, this.DataManagement)).AppendFormat(",HienThi = {0}", clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)).AppendFormat(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).AppendFormat(",MA_NUOC = {0}", clsDAL.IsDBNULL(MA_NUOC, ProType.STRING, this.DataManagement)).AppendFormat(",MA_CU = {0}", clsDAL.IsDBNULL(MA_CU, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_CU = {0}", clsDAL.IsDBNULL(TEN_CU, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_CK_VN = {0}", clsDAL.IsDBNULL(TEN_CK_VN, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_CK1_VN = {0}", clsDAL.IsDBNULL(TEN_CK1_VN, ProType.STRING, this.DataManagement)).AppendFormat(",ISVNACCS = {0}", clsDAL.IsDBNULL(ISVNACCS, ProType.OTHER, this.DataManagement)).AppendFormat(",ItemType = {0}", clsDAL.IsDBNULL(ItemType, ProType.STRING, this.DataManagement));
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
			return clsDAL.DeleteString("SCUAKHAUNN", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
			sbSQL.AppendFormat("MA_CK = {0}", clsDAL.IsDBNULL(MA_CK, ProType.STRING, this.DataManagement));
			return sbSQL.ToString();
		}
	}
}