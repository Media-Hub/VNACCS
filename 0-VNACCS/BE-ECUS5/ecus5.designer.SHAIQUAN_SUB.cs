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
	/// Generated Class for Table : SHAIQUAN_SUB.
	/// </summary>
	public partial class SHAIQUAN_SUB : TableBase
	{
		public SHAIQUAN_SUB() : base(){}

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

		private string m_TEN_TCVN;
		private bool m_TEN_TCVNUpdated = false;
		/// <summary>
		/// TEN_TCVN.
		/// </summary>
		public string TEN_TCVN
		{
			get
			{
				return m_TEN_TCVN;
			}
			set
			{
				if ((this.m_TEN_TCVN != value))
				{
					this.SendPropertyChanging("TEN_TCVN");
					this.m_TEN_TCVN = value;
					this.SendPropertyChanged("TEN_TCVN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_TCVNUpdated = true;
				}
			}
		}

		private int? m_IDA;
		private bool m_IDAUpdated = false;
		/// <summary>
		/// IDA.
		/// </summary>
		public int? IDA
		{
			get
			{
				return m_IDA;
			}
			set
			{
				if ((this.m_IDA != value))
				{
					this.SendPropertyChanging("IDA");
					this.m_IDA = value;
					this.SendPropertyChanged("IDA");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_IDAUpdated = true;
				}
			}
		}

		private int? m_EDA;
		private bool m_EDAUpdated = false;
		/// <summary>
		/// EDA.
		/// </summary>
		public int? EDA
		{
			get
			{
				return m_EDA;
			}
			set
			{
				if ((this.m_EDA != value))
				{
					this.SendPropertyChanging("EDA");
					this.m_EDA = value;
					this.SendPropertyChanged("EDA");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_EDAUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SHAIQUAN_SUB " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(clsDAL.SelectField("MA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("MA_HQ", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HienThi", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("isVNACCS", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_BANG", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("KEY_FIELD", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_TCVN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("IDA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("EDA", ProType.OTHER, this.DataManagement));
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
			sbSQL.Append("INSERT INTO SHAIQUAN_SUB (MA, MA_HQ, TEN, HienThi, isVNACCS, TEN_BANG, KEY_FIELD, TEN_TCVN, IDA, EDA) VALUES(").Append(clsDAL.IsDBNULL(MA, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(MA_HQ, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(isVNACCS, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(KEY_FIELD, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_TCVN, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(IDA, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(EDA, ProType.OTHER, this.DataManagement)).Append(")");
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SHAIQUAN_SUB SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(m_MAUpdated ? string.Format(",MA = {0}", clsDAL.IsDBNULL(MA, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_MA_HQUpdated ? string.Format(",MA_HQ = {0}", clsDAL.IsDBNULL(MA_HQ, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TENUpdated ? string.Format(",TEN = {0}", clsDAL.IsDBNULL(TEN, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_HienThiUpdated ? string.Format(",HienThi = {0}", clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_isVNACCSUpdated ? string.Format(",isVNACCS = {0}", clsDAL.IsDBNULL(isVNACCS, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_TEN_BANGUpdated ? string.Format(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_TCVNUpdated ? string.Format(",TEN_TCVN = {0}", clsDAL.IsDBNULL(TEN_TCVN, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_IDAUpdated ? string.Format(",IDA = {0}", clsDAL.IsDBNULL(IDA, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_EDAUpdated ? string.Format(",EDA = {0}", clsDAL.IsDBNULL(EDA, ProType.OTHER, this.DataManagement)) : string.Empty);
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
			sbSQL.AppendFormat("MA = {0}", clsDAL.IsDBNULL(MA, ProType.STRING, this.DataManagement)).AppendFormat(",MA_HQ = {0}", clsDAL.IsDBNULL(MA_HQ, ProType.STRING, this.DataManagement)).AppendFormat(",TEN = {0}", clsDAL.IsDBNULL(TEN, ProType.STRING, this.DataManagement)).AppendFormat(",HienThi = {0}", clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)).AppendFormat(",isVNACCS = {0}", clsDAL.IsDBNULL(isVNACCS, ProType.OTHER, this.DataManagement)).AppendFormat(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_TCVN = {0}", clsDAL.IsDBNULL(TEN_TCVN, ProType.STRING, this.DataManagement)).AppendFormat(",IDA = {0}", clsDAL.IsDBNULL(IDA, ProType.OTHER, this.DataManagement)).AppendFormat(",EDA = {0}", clsDAL.IsDBNULL(EDA, ProType.OTHER, this.DataManagement));
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
			return clsDAL.DeleteString("SHAIQUAN_SUB", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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