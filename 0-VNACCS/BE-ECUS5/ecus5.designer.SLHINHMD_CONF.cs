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
	/// Generated Class for Table : SLHINHMD_CONF.
	/// </summary>
	public partial class SLHINHMD_CONF : TableBase
	{
		public SLHINHMD_CONF() : base(){}

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
		private string m_Nhom_LH;
		private bool m_Nhom_LHUpdated = false;
		/// <summary>
		/// Nhom_LH.
		/// </summary>
		public string Nhom_LH
		{
			get
			{
				return m_Nhom_LH;
			}
			set
			{
				if ((this.m_Nhom_LH != value))
				{
					this.SendPropertyChanging("Nhom_LH");
					this.m_Nhom_LH = value;
					this.SendPropertyChanged("Nhom_LH");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_Nhom_LHUpdated = true;
				}
			}
		}

		private string m_Ma_LH;
		/// <summary>
		/// Ma_LH.
		/// </summary>
		public string Ma_LH
		{
			get
			{
				return m_Ma_LH;
			}
			set
			{
				if ((this.m_Ma_LH != value))
				{
					this.SendPropertyChanging("Ma_LH");
					this.m_Ma_LH = value;
					this.SendPropertyChanged("Ma_LH");
				}
			}
		}

		private string m_Ten_LH;
		private bool m_Ten_LHUpdated = false;
		/// <summary>
		/// Ten_LH.
		/// </summary>
		public string Ten_LH
		{
			get
			{
				return m_Ten_LH;
			}
			set
			{
				if ((this.m_Ten_LH != value))
				{
					this.SendPropertyChanging("Ten_LH");
					this.m_Ten_LH = value;
					this.SendPropertyChanged("Ten_LH");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_Ten_LHUpdated = true;
				}
			}
		}

		private string m_Ten_LH1;
		private bool m_Ten_LH1Updated = false;
		/// <summary>
		/// Ten_LH1.
		/// </summary>
		public string Ten_LH1
		{
			get
			{
				return m_Ten_LH1;
			}
			set
			{
				if ((this.m_Ten_LH1 != value))
				{
					this.SendPropertyChanging("Ten_LH1");
					this.m_Ten_LH1 = value;
					this.SendPropertyChanged("Ten_LH1");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_Ten_LH1Updated = true;
				}
			}
		}

		private string m_Ten_VT;
		private bool m_Ten_VTUpdated = false;
		/// <summary>
		/// Ten_VT.
		/// </summary>
		public string Ten_VT
		{
			get
			{
				return m_Ten_VT;
			}
			set
			{
				if ((this.m_Ten_VT != value))
				{
					this.SendPropertyChanging("Ten_VT");
					this.m_Ten_VT = value;
					this.SendPropertyChanged("Ten_VT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_Ten_VTUpdated = true;
				}
			}
		}

		private int? m_LH_ND;
		private bool m_LH_NDUpdated = false;
		/// <summary>
		/// LH_ND.
		/// </summary>
		public int? LH_ND
		{
			get
			{
				return m_LH_ND;
			}
			set
			{
				if ((this.m_LH_ND != value))
				{
					this.SendPropertyChanging("LH_ND");
					this.m_LH_ND = value;
					this.SendPropertyChanged("LH_ND");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_LH_NDUpdated = true;
				}
			}
		}

		private int? m_So_TT;
		private bool m_So_TTUpdated = false;
		/// <summary>
		/// So_TT.
		/// </summary>
		public int? So_TT
		{
			get
			{
				return m_So_TT;
			}
			set
			{
				if ((this.m_So_TT != value))
				{
					this.SendPropertyChanging("So_TT");
					this.m_So_TT = value;
					this.SendPropertyChanged("So_TT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_So_TTUpdated = true;
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

		private string m_TEN_LH_VN;
		private bool m_TEN_LH_VNUpdated = false;
		/// <summary>
		/// TEN_LH_VN.
		/// </summary>
		public string TEN_LH_VN
		{
			get
			{
				return m_TEN_LH_VN;
			}
			set
			{
				if ((this.m_TEN_LH_VN != value))
				{
					this.SendPropertyChanging("TEN_LH_VN");
					this.m_TEN_LH_VN = value;
					this.SendPropertyChanged("TEN_LH_VN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_LH_VNUpdated = true;
				}
			}
		}

		private string m_TEN_LH1_VN;
		private bool m_TEN_LH1_VNUpdated = false;
		/// <summary>
		/// TEN_LH1_VN.
		/// </summary>
		public string TEN_LH1_VN
		{
			get
			{
				return m_TEN_LH1_VN;
			}
			set
			{
				if ((this.m_TEN_LH1_VN != value))
				{
					this.SendPropertyChanging("TEN_LH1_VN");
					this.m_TEN_LH1_VN = value;
					this.SendPropertyChanged("TEN_LH1_VN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_LH1_VNUpdated = true;
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
			return "SELECT " + Fields + " FROM SLHINHMD_CONF " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(clsDAL.SelectField("Nhom_LH", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("Ma_LH", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("Ten_LH", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("Ten_LH1", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("Ten_VT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("LH_ND", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("So_TT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("isVNACCS", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_BANG", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("MA_CU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_CU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_LH_VN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_LH1_VN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HienThi", ProType.OTHER, this.DataManagement));
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
			sbSQL.Append("INSERT INTO SLHINHMD_CONF (Nhom_LH, Ma_LH, Ten_LH, Ten_LH1, Ten_VT, LH_ND, So_TT, isVNACCS, TEN_BANG, MA_CU, TEN_CU, TEN_LH_VN, TEN_LH1_VN, HienThi) VALUES(").Append(clsDAL.IsDBNULL(Nhom_LH, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(Ma_LH, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(Ten_LH, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(Ten_LH1, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(Ten_VT, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(LH_ND, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(So_TT, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(isVNACCS, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(MA_CU, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_CU, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_LH_VN, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_LH1_VN, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)).Append(")");
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SLHINHMD_CONF SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(m_Nhom_LHUpdated ? string.Format(",Nhom_LH = {0}", clsDAL.IsDBNULL(Nhom_LH, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_Ten_LHUpdated ? string.Format(",Ten_LH = {0}", clsDAL.IsDBNULL(Ten_LH, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_Ten_LH1Updated ? string.Format(",Ten_LH1 = {0}", clsDAL.IsDBNULL(Ten_LH1, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_Ten_VTUpdated ? string.Format(",Ten_VT = {0}", clsDAL.IsDBNULL(Ten_VT, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_LH_NDUpdated ? string.Format(",LH_ND = {0}", clsDAL.IsDBNULL(LH_ND, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_So_TTUpdated ? string.Format(",So_TT = {0}", clsDAL.IsDBNULL(So_TT, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_isVNACCSUpdated ? string.Format(",isVNACCS = {0}", clsDAL.IsDBNULL(isVNACCS, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_TEN_BANGUpdated ? string.Format(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_MA_CUUpdated ? string.Format(",MA_CU = {0}", clsDAL.IsDBNULL(MA_CU, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_CUUpdated ? string.Format(",TEN_CU = {0}", clsDAL.IsDBNULL(TEN_CU, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_LH_VNUpdated ? string.Format(",TEN_LH_VN = {0}", clsDAL.IsDBNULL(TEN_LH_VN, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_LH1_VNUpdated ? string.Format(",TEN_LH1_VN = {0}", clsDAL.IsDBNULL(TEN_LH1_VN, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_HienThiUpdated ? string.Format(",HienThi = {0}", clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)) : string.Empty);
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
			sbSQL.AppendFormat("Nhom_LH = {0}", clsDAL.IsDBNULL(Nhom_LH, ProType.STRING, this.DataManagement)).AppendFormat(",Ten_LH = {0}", clsDAL.IsDBNULL(Ten_LH, ProType.STRING, this.DataManagement)).AppendFormat(",Ten_LH1 = {0}", clsDAL.IsDBNULL(Ten_LH1, ProType.STRING, this.DataManagement)).AppendFormat(",Ten_VT = {0}", clsDAL.IsDBNULL(Ten_VT, ProType.STRING, this.DataManagement)).AppendFormat(",LH_ND = {0}", clsDAL.IsDBNULL(LH_ND, ProType.OTHER, this.DataManagement)).AppendFormat(",So_TT = {0}", clsDAL.IsDBNULL(So_TT, ProType.OTHER, this.DataManagement)).AppendFormat(",isVNACCS = {0}", clsDAL.IsDBNULL(isVNACCS, ProType.OTHER, this.DataManagement)).AppendFormat(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).AppendFormat(",MA_CU = {0}", clsDAL.IsDBNULL(MA_CU, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_CU = {0}", clsDAL.IsDBNULL(TEN_CU, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_LH_VN = {0}", clsDAL.IsDBNULL(TEN_LH_VN, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_LH1_VN = {0}", clsDAL.IsDBNULL(TEN_LH1_VN, ProType.STRING, this.DataManagement)).AppendFormat(",HienThi = {0}", clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement));
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
			return clsDAL.DeleteString("SLHINHMD_CONF", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
			sbSQL.AppendFormat("Ma_LH = {0}", clsDAL.IsDBNULL(Ma_LH, ProType.STRING, this.DataManagement));
			return sbSQL.ToString();
		}
	}
}