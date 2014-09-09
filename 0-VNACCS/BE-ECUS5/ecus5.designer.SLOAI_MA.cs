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
	/// Generated Class for Table : SLOAI_MA.
	/// </summary>
	public partial class SLOAI_MA : TableBase
	{
		public SLOAI_MA() : base(){}

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
		private string m_LOAI_MA;
		/// <summary>
		/// LOAI_MA.
		/// </summary>
		public string LOAI_MA
		{
			get
			{
				return m_LOAI_MA;
			}
			set
			{
				if ((this.m_LOAI_MA != value))
				{
					this.SendPropertyChanging("LOAI_MA");
					this.m_LOAI_MA = value;
					this.SendPropertyChanged("LOAI_MA");
				}
			}
		}

		private string m_TEN_LOAI_MA;
		private bool m_TEN_LOAI_MAUpdated = false;
		/// <summary>
		/// TEN_LOAI_MA.
		/// </summary>
		public string TEN_LOAI_MA
		{
			get
			{
				return m_TEN_LOAI_MA;
			}
			set
			{
				if ((this.m_TEN_LOAI_MA != value))
				{
					this.SendPropertyChanging("TEN_LOAI_MA");
					this.m_TEN_LOAI_MA = value;
					this.SendPropertyChanged("TEN_LOAI_MA");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_LOAI_MAUpdated = true;
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

		private string m_TEN_LOAI_MA_TCVN;
		private bool m_TEN_LOAI_MA_TCVNUpdated = false;
		/// <summary>
		/// TEN_LOAI_MA_TCVN.
		/// </summary>
		public string TEN_LOAI_MA_TCVN
		{
			get
			{
				return m_TEN_LOAI_MA_TCVN;
			}
			set
			{
				if ((this.m_TEN_LOAI_MA_TCVN != value))
				{
					this.SendPropertyChanging("TEN_LOAI_MA_TCVN");
					this.m_TEN_LOAI_MA_TCVN = value;
					this.SendPropertyChanged("TEN_LOAI_MA_TCVN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_LOAI_MA_TCVNUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SLOAI_MA " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(clsDAL.SelectField("LOAI_MA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_LOAI_MA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_BANG", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HienThi", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_LOAI_MA_TCVN", ProType.OTHER, this.DataManagement));
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
			sbSQL.Append("INSERT INTO SLOAI_MA (LOAI_MA, TEN_LOAI_MA, TEN_BANG, HienThi, TEN_LOAI_MA_TCVN) VALUES(").Append(clsDAL.IsDBNULL(LOAI_MA, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_LOAI_MA, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_LOAI_MA_TCVN, ProType.STRING, this.DataManagement)).Append(")");
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SLOAI_MA SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(m_TEN_LOAI_MAUpdated ? string.Format(",TEN_LOAI_MA = {0}", clsDAL.IsDBNULL(TEN_LOAI_MA, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_BANGUpdated ? string.Format(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_HienThiUpdated ? string.Format(",HienThi = {0}", clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_TEN_LOAI_MA_TCVNUpdated ? string.Format(",TEN_LOAI_MA_TCVN = {0}", clsDAL.IsDBNULL(TEN_LOAI_MA_TCVN, ProType.STRING, this.DataManagement)) : string.Empty);
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
			sbSQL.AppendFormat("TEN_LOAI_MA = {0}", clsDAL.IsDBNULL(TEN_LOAI_MA, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).AppendFormat(",HienThi = {0}", clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)).AppendFormat(",TEN_LOAI_MA_TCVN = {0}", clsDAL.IsDBNULL(TEN_LOAI_MA_TCVN, ProType.STRING, this.DataManagement));
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
			return clsDAL.DeleteString("SLOAI_MA", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
			sbSQL.AppendFormat("LOAI_MA = {0}", clsDAL.IsDBNULL(LOAI_MA, ProType.STRING, this.DataManagement));
			return sbSQL.ToString();
		}
	}
}