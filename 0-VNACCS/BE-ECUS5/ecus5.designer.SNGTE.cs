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
	/// Generated Class for Table : SNGTE.
	/// </summary>
	public partial class SNGTE : TableBase
	{
		public SNGTE() : base(){}

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
		private string m_MA_NT;
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
				}
			}
		}

		private string m_TEN_NT;
		private bool m_TEN_NTUpdated = false;
		/// <summary>
		/// TEN_NT.
		/// </summary>
		public string TEN_NT
		{
			get
			{
				return m_TEN_NT;
			}
			set
			{
				if ((this.m_TEN_NT != value))
				{
					this.SendPropertyChanging("TEN_NT");
					this.m_TEN_NT = value;
					this.SendPropertyChanged("TEN_NT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_NTUpdated = true;
				}
			}
		}

		private string m_TEN_NT1;
		private bool m_TEN_NT1Updated = false;
		/// <summary>
		/// TEN_NT1.
		/// </summary>
		public string TEN_NT1
		{
			get
			{
				return m_TEN_NT1;
			}
			set
			{
				if ((this.m_TEN_NT1 != value))
				{
					this.SendPropertyChanging("TEN_NT1");
					this.m_TEN_NT1 = value;
					this.SendPropertyChanged("TEN_NT1");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_NT1Updated = true;
				}
			}
		}

		private double? m_TYGIA_VND;
		private bool m_TYGIA_VNDUpdated = false;
		/// <summary>
		/// TYGIA_VND.
		/// </summary>
		public double? TYGIA_VND
		{
			get
			{
				return m_TYGIA_VND;
			}
			set
			{
				if ((this.m_TYGIA_VND != value))
				{
					this.SendPropertyChanging("TYGIA_VND");
					this.m_TYGIA_VND = value;
					this.SendPropertyChanged("TYGIA_VND");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TYGIA_VNDUpdated = true;
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

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SNGTE " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(clsDAL.SelectField("MA_NT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_NT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_NT1", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TYGIA_VND", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_BANG", ProType.OTHER, this.DataManagement));
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
			sbSQL.Append("INSERT INTO SNGTE (MA_NT, TEN_NT, TEN_NT1, TYGIA_VND, TEN_BANG) VALUES(").Append(clsDAL.IsDBNULL(MA_NT, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_NT, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_NT1, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TYGIA_VND, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).Append(")");
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SNGTE SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(m_TEN_NTUpdated ? string.Format(",TEN_NT = {0}", clsDAL.IsDBNULL(TEN_NT, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_NT1Updated ? string.Format(",TEN_NT1 = {0}", clsDAL.IsDBNULL(TEN_NT1, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TYGIA_VNDUpdated ? string.Format(",TYGIA_VND = {0}", clsDAL.IsDBNULL(TYGIA_VND, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_TEN_BANGUpdated ? string.Format(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)) : string.Empty);
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
			sbSQL.AppendFormat("TEN_NT = {0}", clsDAL.IsDBNULL(TEN_NT, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_NT1 = {0}", clsDAL.IsDBNULL(TEN_NT1, ProType.STRING, this.DataManagement)).AppendFormat(",TYGIA_VND = {0}", clsDAL.IsDBNULL(TYGIA_VND, ProType.OTHER, this.DataManagement)).AppendFormat(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement));
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
			return clsDAL.DeleteString("SNGTE", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
			sbSQL.AppendFormat("MA_NT = {0}", clsDAL.IsDBNULL(MA_NT, ProType.STRING, this.DataManagement));
			return sbSQL.ToString();
		}
	}
}