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
	/// Generated Class for Table : SPTTT.
	/// </summary>
	public partial class SPTTT : TableBase
	{
		public SPTTT() : base(){}

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
		private string m_MA_PTTT;
		/// <summary>
		/// MA_PTTT.
		/// </summary>
		public string MA_PTTT
		{
			get
			{
				return m_MA_PTTT;
			}
			set
			{
				if ((this.m_MA_PTTT != value))
				{
					this.SendPropertyChanging("MA_PTTT");
					this.m_MA_PTTT = value;
					this.SendPropertyChanged("MA_PTTT");
				}
			}
		}

		private string m_GHICHU;
		private bool m_GHICHUUpdated = false;
		/// <summary>
		/// GHICHU.
		/// </summary>
		public string GHICHU
		{
			get
			{
				return m_GHICHU;
			}
			set
			{
				if ((this.m_GHICHU != value))
				{
					this.SendPropertyChanging("GHICHU");
					this.m_GHICHU = value;
					this.SendPropertyChanged("GHICHU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_GHICHUUpdated = true;
				}
			}
		}

		private string m_TEN_PTTT;
		private bool m_TEN_PTTTUpdated = false;
		/// <summary>
		/// TEN_PTTT.
		/// </summary>
		public string TEN_PTTT
		{
			get
			{
				return m_TEN_PTTT;
			}
			set
			{
				if ((this.m_TEN_PTTT != value))
				{
					this.SendPropertyChanging("TEN_PTTT");
					this.m_TEN_PTTT = value;
					this.SendPropertyChanged("TEN_PTTT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_PTTTUpdated = true;
				}
			}
		}

		private string m_TEN_PTTT_TCVN;
		private bool m_TEN_PTTT_TCVNUpdated = false;
		/// <summary>
		/// TEN_PTTT_TCVN.
		/// </summary>
		public string TEN_PTTT_TCVN
		{
			get
			{
				return m_TEN_PTTT_TCVN;
			}
			set
			{
				if ((this.m_TEN_PTTT_TCVN != value))
				{
					this.SendPropertyChanging("TEN_PTTT_TCVN");
					this.m_TEN_PTTT_TCVN = value;
					this.SendPropertyChanged("TEN_PTTT_TCVN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_PTTT_TCVNUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SPTTT " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(clsDAL.SelectField("MA_PTTT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("GHICHU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_PTTT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_PTTT_TCVN", ProType.OTHER, this.DataManagement));
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
			sbSQL.Append("INSERT INTO SPTTT (MA_PTTT, GHICHU, TEN_PTTT, TEN_PTTT_TCVN) VALUES(").Append(clsDAL.IsDBNULL(MA_PTTT, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(GHICHU, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_PTTT, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_PTTT_TCVN, ProType.STRING, this.DataManagement)).Append(")");
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SPTTT SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(m_GHICHUUpdated ? string.Format(",GHICHU = {0}", clsDAL.IsDBNULL(GHICHU, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_PTTTUpdated ? string.Format(",TEN_PTTT = {0}", clsDAL.IsDBNULL(TEN_PTTT, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_PTTT_TCVNUpdated ? string.Format(",TEN_PTTT_TCVN = {0}", clsDAL.IsDBNULL(TEN_PTTT_TCVN, ProType.STRING, this.DataManagement)) : string.Empty);
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
			sbSQL.AppendFormat("GHICHU = {0}", clsDAL.IsDBNULL(GHICHU, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_PTTT = {0}", clsDAL.IsDBNULL(TEN_PTTT, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_PTTT_TCVN = {0}", clsDAL.IsDBNULL(TEN_PTTT_TCVN, ProType.STRING, this.DataManagement));
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
			return clsDAL.DeleteString("SPTTT", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
			sbSQL.AppendFormat("MA_PTTT = {0}", clsDAL.IsDBNULL(MA_PTTT, ProType.STRING, this.DataManagement));
			return sbSQL.ToString();
		}
	}
}