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
	/// Generated Class for Table : SPHANLOAI.
	/// </summary>
	public partial class SPHANLOAI : TableBase
	{
		public SPHANLOAI() : base(){}

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
		private string m_MA_PLOAI;
		/// <summary>
		/// MA_PLOAI.
		/// </summary>
		public string MA_PLOAI
		{
			get
			{
				return m_MA_PLOAI;
			}
			set
			{
				if ((this.m_MA_PLOAI != value))
				{
					this.SendPropertyChanging("MA_PLOAI");
					this.m_MA_PLOAI = value;
					this.SendPropertyChanged("MA_PLOAI");
				}
			}
		}

		private string m_TEN_PLOAI;
		private bool m_TEN_PLOAIUpdated = false;
		/// <summary>
		/// TEN_PLOAI.
		/// </summary>
		public string TEN_PLOAI
		{
			get
			{
				return m_TEN_PLOAI;
			}
			set
			{
				if ((this.m_TEN_PLOAI != value))
				{
					this.SendPropertyChanging("TEN_PLOAI");
					this.m_TEN_PLOAI = value;
					this.SendPropertyChanged("TEN_PLOAI");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_PLOAIUpdated = true;
				}
			}
		}

		private string m_TEN_PLOAI1;
		private bool m_TEN_PLOAI1Updated = false;
		/// <summary>
		/// TEN_PLOAI1.
		/// </summary>
		public string TEN_PLOAI1
		{
			get
			{
				return m_TEN_PLOAI1;
			}
			set
			{
				if ((this.m_TEN_PLOAI1 != value))
				{
					this.SendPropertyChanging("TEN_PLOAI1");
					this.m_TEN_PLOAI1 = value;
					this.SendPropertyChanged("TEN_PLOAI1");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_PLOAI1Updated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SPHANLOAI " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(clsDAL.SelectField("MA_PLOAI", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_PLOAI", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_PLOAI1", ProType.OTHER, this.DataManagement));
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
			sbSQL.Append("INSERT INTO SPHANLOAI (MA_PLOAI, TEN_PLOAI, TEN_PLOAI1) VALUES(").Append(clsDAL.IsDBNULL(MA_PLOAI, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_PLOAI, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_PLOAI1, ProType.STRING, this.DataManagement)).Append(")");
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SPHANLOAI SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(m_TEN_PLOAIUpdated ? string.Format(",TEN_PLOAI = {0}", clsDAL.IsDBNULL(TEN_PLOAI, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_PLOAI1Updated ? string.Format(",TEN_PLOAI1 = {0}", clsDAL.IsDBNULL(TEN_PLOAI1, ProType.STRING, this.DataManagement)) : string.Empty);
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
			sbSQL.AppendFormat("TEN_PLOAI = {0}", clsDAL.IsDBNULL(TEN_PLOAI, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_PLOAI1 = {0}", clsDAL.IsDBNULL(TEN_PLOAI1, ProType.STRING, this.DataManagement));
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
			return clsDAL.DeleteString("SPHANLOAI", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
			sbSQL.AppendFormat("MA_PLOAI = {0}", clsDAL.IsDBNULL(MA_PLOAI, ProType.STRING, this.DataManagement));
			return sbSQL.ToString();
		}
	}
}