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
	/// Generated Class for Table : SMA_MIENTHUE.
	/// </summary>
	public partial class SMA_MIENTHUE : TableBase
	{
		public SMA_MIENTHUE() : base(){}

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
		private int m_ID;
		private bool m_IDUpdated = false;
		/// <summary>
		/// ID.
		/// </summary>
		public int ID
		{
			get
			{
				return m_ID;
			}
			set
			{
				if ((this.m_ID != value))
				{
					this.SendPropertyChanging("ID");
					this.m_ID = value;
					this.SendPropertyChanged("ID");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_IDUpdated = true;
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

		private string m_NGAY_BAN_HANH;
		private bool m_NGAY_BAN_HANHUpdated = false;
		/// <summary>
		/// NGAY_BAN_HANH.
		/// </summary>
		public string NGAY_BAN_HANH
		{
			get
			{
				return m_NGAY_BAN_HANH;
			}
			set
			{
				if ((this.m_NGAY_BAN_HANH != value))
				{
					this.SendPropertyChanging("NGAY_BAN_HANH");
					this.m_NGAY_BAN_HANH = value;
					this.SendPropertyChanged("NGAY_BAN_HANH");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NGAY_BAN_HANHUpdated = true;
				}
			}
		}

		private string m_NGAY_HIEU_LUC;
		private bool m_NGAY_HIEU_LUCUpdated = false;
		/// <summary>
		/// NGAY_HIEU_LUC.
		/// </summary>
		public string NGAY_HIEU_LUC
		{
			get
			{
				return m_NGAY_HIEU_LUC;
			}
			set
			{
				if ((this.m_NGAY_HIEU_LUC != value))
				{
					this.SendPropertyChanging("NGAY_HIEU_LUC");
					this.m_NGAY_HIEU_LUC = value;
					this.SendPropertyChanged("NGAY_HIEU_LUC");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NGAY_HIEU_LUCUpdated = true;
				}
			}
		}

		private string m_NGAY_HH;
		private bool m_NGAY_HHUpdated = false;
		/// <summary>
		/// NGAY_HH.
		/// </summary>
		public string NGAY_HH
		{
			get
			{
				return m_NGAY_HH;
			}
			set
			{
				if ((this.m_NGAY_HH != value))
				{
					this.SendPropertyChanging("NGAY_HH");
					this.m_NGAY_HH = value;
					this.SendPropertyChanged("NGAY_HH");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NGAY_HHUpdated = true;
				}
			}
		}

		private string m_TEN;
		private bool m_TENUpdated = false;
		/// <summary>
		/// TEN.
		/// </summary>
		public string TEN
		{
			get
			{
				return m_TEN;
			}
			set
			{
				if ((this.m_TEN != value))
				{
					this.SendPropertyChanging("TEN");
					this.m_TEN = value;
					this.SendPropertyChanged("TEN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TENUpdated = true;
				}
			}
		}

		private string m_TableID;
		private bool m_TableIDUpdated = false;
		/// <summary>
		/// TableID.
		/// </summary>
		public string TableID
		{
			get
			{
				return m_TableID;
			}
			set
			{
				if ((this.m_TableID != value))
				{
					this.SendPropertyChanging("TableID");
					this.m_TableID = value;
					this.SendPropertyChanged("TableID");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TableIDUpdated = true;
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

		private string m_TEN_VN;
		private bool m_TEN_VNUpdated = false;
		/// <summary>
		/// TEN_VN.
		/// </summary>
		public string TEN_VN
		{
			get
			{
				return m_TEN_VN;
			}
			set
			{
				if ((this.m_TEN_VN != value))
				{
					this.SendPropertyChanging("TEN_VN");
					this.m_TEN_VN = value;
					this.SendPropertyChanged("TEN_VN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_VNUpdated = true;
				}
			}
		}

		private string m_TEN_VN_TCVN;
		private bool m_TEN_VN_TCVNUpdated = false;
		/// <summary>
		/// TEN_VN_TCVN.
		/// </summary>
		public string TEN_VN_TCVN
		{
			get
			{
				return m_TEN_VN_TCVN;
			}
			set
			{
				if ((this.m_TEN_VN_TCVN != value))
				{
					this.SendPropertyChanging("TEN_VN_TCVN");
					this.m_TEN_VN_TCVN = value;
					this.SendPropertyChanged("TEN_VN_TCVN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_VN_TCVNUpdated = true;
				}
			}
		}

		private string m_Type;
		private bool m_TypeUpdated = false;
		/// <summary>
		/// Type.
		/// </summary>
		public string Type
		{
			get
			{
				return m_Type;
			}
			set
			{
				if ((this.m_Type != value))
				{
					this.SendPropertyChanging("Type");
					this.m_Type = value;
					this.SendPropertyChanged("Type");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TypeUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SMA_MIENTHUE " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(clsDAL.SelectField("ID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("MA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NGAY_BAN_HANH", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NGAY_HIEU_LUC", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NGAY_HH", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TableID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("KEY_FIELD", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HienThi", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_VN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_VN_TCVN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("Type", ProType.OTHER, this.DataManagement));
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
			sbSQL.Append("INSERT INTO SMA_MIENTHUE (ID, MA, NGAY_BAN_HANH, NGAY_HIEU_LUC, NGAY_HH, TEN, TableID, KEY_FIELD, HienThi, TEN_VN, TEN_VN_TCVN, Type) VALUES(").Append(clsDAL.IsDBNULL(ID, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(MA, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(NGAY_BAN_HANH, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(NGAY_HIEU_LUC, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(NGAY_HH, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TableID, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(KEY_FIELD, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_VN, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_VN_TCVN, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(Type, ProType.STRING, this.DataManagement)).Append(")");
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SMA_MIENTHUE SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(m_IDUpdated ? string.Format(",ID = {0}", clsDAL.IsDBNULL(ID, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_MAUpdated ? string.Format(",MA = {0}", clsDAL.IsDBNULL(MA, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_NGAY_BAN_HANHUpdated ? string.Format(",NGAY_BAN_HANH = {0}", clsDAL.IsDBNULL(NGAY_BAN_HANH, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_NGAY_HIEU_LUCUpdated ? string.Format(",NGAY_HIEU_LUC = {0}", clsDAL.IsDBNULL(NGAY_HIEU_LUC, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_NGAY_HHUpdated ? string.Format(",NGAY_HH = {0}", clsDAL.IsDBNULL(NGAY_HH, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TENUpdated ? string.Format(",TEN = {0}", clsDAL.IsDBNULL(TEN, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TableIDUpdated ? string.Format(",TableID = {0}", clsDAL.IsDBNULL(TableID, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_HienThiUpdated ? string.Format(",HienThi = {0}", clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_TEN_VNUpdated ? string.Format(",TEN_VN = {0}", clsDAL.IsDBNULL(TEN_VN, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_VN_TCVNUpdated ? string.Format(",TEN_VN_TCVN = {0}", clsDAL.IsDBNULL(TEN_VN_TCVN, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TypeUpdated ? string.Format(",Type = {0}", clsDAL.IsDBNULL(Type, ProType.STRING, this.DataManagement)) : string.Empty);
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
			sbSQL.AppendFormat("ID = {0}", clsDAL.IsDBNULL(ID, ProType.OTHER, this.DataManagement)).AppendFormat(",MA = {0}", clsDAL.IsDBNULL(MA, ProType.STRING, this.DataManagement)).AppendFormat(",NGAY_BAN_HANH = {0}", clsDAL.IsDBNULL(NGAY_BAN_HANH, ProType.STRING, this.DataManagement)).AppendFormat(",NGAY_HIEU_LUC = {0}", clsDAL.IsDBNULL(NGAY_HIEU_LUC, ProType.STRING, this.DataManagement)).AppendFormat(",NGAY_HH = {0}", clsDAL.IsDBNULL(NGAY_HH, ProType.STRING, this.DataManagement)).AppendFormat(",TEN = {0}", clsDAL.IsDBNULL(TEN, ProType.STRING, this.DataManagement)).AppendFormat(",TableID = {0}", clsDAL.IsDBNULL(TableID, ProType.STRING, this.DataManagement)).AppendFormat(",HienThi = {0}", clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)).AppendFormat(",TEN_VN = {0}", clsDAL.IsDBNULL(TEN_VN, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_VN_TCVN = {0}", clsDAL.IsDBNULL(TEN_VN_TCVN, ProType.STRING, this.DataManagement)).AppendFormat(",Type = {0}", clsDAL.IsDBNULL(Type, ProType.STRING, this.DataManagement));
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
			return clsDAL.DeleteString("SMA_MIENTHUE", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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