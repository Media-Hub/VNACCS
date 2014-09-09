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
	/// Generated Class for Table : SHAIQUAN.
	/// </summary>
	public partial class SHAIQUAN : TableBase
	{
		public SHAIQUAN() : base(){}

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
		private string m_Ma_HQ;
		/// <summary>
		/// Ma_HQ.
		/// </summary>
		public string Ma_HQ
		{
			get
			{
				return m_Ma_HQ;
			}
			set
			{
				if ((this.m_Ma_HQ != value))
				{
					this.SendPropertyChanging("Ma_HQ");
					this.m_Ma_HQ = value;
					this.SendPropertyChanged("Ma_HQ");
				}
			}
		}

		private string m_Ten_HQ;
		private bool m_Ten_HQUpdated = false;
		/// <summary>
		/// Ten_HQ.
		/// </summary>
		public string Ten_HQ
		{
			get
			{
				return m_Ten_HQ;
			}
			set
			{
				if ((this.m_Ten_HQ != value))
				{
					this.SendPropertyChanging("Ten_HQ");
					this.m_Ten_HQ = value;
					this.SendPropertyChanged("Ten_HQ");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_Ten_HQUpdated = true;
				}
			}
		}

		private string m_Ten_HQ1;
		private bool m_Ten_HQ1Updated = false;
		/// <summary>
		/// Ten_HQ1.
		/// </summary>
		public string Ten_HQ1
		{
			get
			{
				return m_Ten_HQ1;
			}
			set
			{
				if ((this.m_Ten_HQ1 != value))
				{
					this.SendPropertyChanging("Ten_HQ1");
					this.m_Ten_HQ1 = value;
					this.SendPropertyChanged("Ten_HQ1");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_Ten_HQ1Updated = true;
				}
			}
		}

		private int m_Cap_HQ;
		private bool m_Cap_HQUpdated = false;
		/// <summary>
		/// Cap_HQ.
		/// </summary>
		public int Cap_HQ
		{
			get
			{
				return m_Cap_HQ;
			}
			set
			{
				if ((this.m_Cap_HQ != value))
				{
					this.SendPropertyChanging("Cap_HQ");
					this.m_Cap_HQ = value;
					this.SendPropertyChanged("Cap_HQ");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_Cap_HQUpdated = true;
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

		private string m_TEN_HQ_VN;
		private bool m_TEN_HQ_VNUpdated = false;
		/// <summary>
		/// TEN_HQ_VN.
		/// </summary>
		public string TEN_HQ_VN
		{
			get
			{
				return m_TEN_HQ_VN;
			}
			set
			{
				if ((this.m_TEN_HQ_VN != value))
				{
					this.SendPropertyChanging("TEN_HQ_VN");
					this.m_TEN_HQ_VN = value;
					this.SendPropertyChanged("TEN_HQ_VN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_HQ_VNUpdated = true;
				}
			}
		}

		private string m_TEN_HQ1_VN;
		private bool m_TEN_HQ1_VNUpdated = false;
		/// <summary>
		/// TEN_HQ1_VN.
		/// </summary>
		public string TEN_HQ1_VN
		{
			get
			{
				return m_TEN_HQ1_VN;
			}
			set
			{
				if ((this.m_TEN_HQ1_VN != value))
				{
					this.SendPropertyChanging("TEN_HQ1_VN");
					this.m_TEN_HQ1_VN = value;
					this.SendPropertyChanged("TEN_HQ1_VN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_HQ1_VNUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SHAIQUAN " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(clsDAL.SelectField("Ma_HQ", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("Ten_HQ", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("Ten_HQ1", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("Cap_HQ", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HienThi", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("isVNACCS", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_BANG", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("MA_CU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_CU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_HQ_VN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_HQ1_VN", ProType.OTHER, this.DataManagement));
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
			sbSQL.Append("INSERT INTO SHAIQUAN (Ma_HQ, Ten_HQ, Ten_HQ1, Cap_HQ, HienThi, isVNACCS, TEN_BANG, MA_CU, TEN_CU, TEN_HQ_VN, TEN_HQ1_VN) VALUES(").Append(clsDAL.IsDBNULL(Ma_HQ, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(Ten_HQ, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(Ten_HQ1, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(Cap_HQ, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(isVNACCS, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(MA_CU, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_CU, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_HQ_VN, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_HQ1_VN, ProType.STRING, this.DataManagement)).Append(")");
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SHAIQUAN SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(m_Ten_HQUpdated ? string.Format(",Ten_HQ = {0}", clsDAL.IsDBNULL(Ten_HQ, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_Ten_HQ1Updated ? string.Format(",Ten_HQ1 = {0}", clsDAL.IsDBNULL(Ten_HQ1, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_Cap_HQUpdated ? string.Format(",Cap_HQ = {0}", clsDAL.IsDBNULL(Cap_HQ, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_HienThiUpdated ? string.Format(",HienThi = {0}", clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_isVNACCSUpdated ? string.Format(",isVNACCS = {0}", clsDAL.IsDBNULL(isVNACCS, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_TEN_BANGUpdated ? string.Format(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_MA_CUUpdated ? string.Format(",MA_CU = {0}", clsDAL.IsDBNULL(MA_CU, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_CUUpdated ? string.Format(",TEN_CU = {0}", clsDAL.IsDBNULL(TEN_CU, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_HQ_VNUpdated ? string.Format(",TEN_HQ_VN = {0}", clsDAL.IsDBNULL(TEN_HQ_VN, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_HQ1_VNUpdated ? string.Format(",TEN_HQ1_VN = {0}", clsDAL.IsDBNULL(TEN_HQ1_VN, ProType.STRING, this.DataManagement)) : string.Empty);
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
			sbSQL.AppendFormat("Ten_HQ = {0}", clsDAL.IsDBNULL(Ten_HQ, ProType.STRING, this.DataManagement)).AppendFormat(",Ten_HQ1 = {0}", clsDAL.IsDBNULL(Ten_HQ1, ProType.STRING, this.DataManagement)).AppendFormat(",Cap_HQ = {0}", clsDAL.IsDBNULL(Cap_HQ, ProType.OTHER, this.DataManagement)).AppendFormat(",HienThi = {0}", clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)).AppendFormat(",isVNACCS = {0}", clsDAL.IsDBNULL(isVNACCS, ProType.OTHER, this.DataManagement)).AppendFormat(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).AppendFormat(",MA_CU = {0}", clsDAL.IsDBNULL(MA_CU, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_CU = {0}", clsDAL.IsDBNULL(TEN_CU, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_HQ_VN = {0}", clsDAL.IsDBNULL(TEN_HQ_VN, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_HQ1_VN = {0}", clsDAL.IsDBNULL(TEN_HQ1_VN, ProType.STRING, this.DataManagement));
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
			return clsDAL.DeleteString("SHAIQUAN", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
			sbSQL.AppendFormat("Ma_HQ = {0}", clsDAL.IsDBNULL(Ma_HQ, ProType.STRING, this.DataManagement));
			return sbSQL.ToString();
		}
	}
}