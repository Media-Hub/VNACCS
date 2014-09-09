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
	/// Generated Class for Table : SLOAI_CT.
	/// </summary>
	public partial class SLOAI_CT : TableBase
	{
		public SLOAI_CT() : base(){}

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
		private decimal? m_STT;
		private bool m_STTUpdated = false;
		/// <summary>
		/// STT.
		/// </summary>
		public decimal? STT
		{
			get
			{
				return m_STT;
			}
			set
			{
				if ((this.m_STT != value))
				{
					this.SendPropertyChanging("STT");
					this.m_STT = value;
					this.SendPropertyChanged("STT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_STTUpdated = true;
				}
			}
		}

		private string m_MA_LOAI_CT;
		/// <summary>
		/// MA_LOAI_CT.
		/// </summary>
		public string MA_LOAI_CT
		{
			get
			{
				return m_MA_LOAI_CT;
			}
			set
			{
				if ((this.m_MA_LOAI_CT != value))
				{
					this.SendPropertyChanging("MA_LOAI_CT");
					this.m_MA_LOAI_CT = value;
					this.SendPropertyChanged("MA_LOAI_CT");
				}
			}
		}

		private string m_TEN_LOAI_CT;
		private bool m_TEN_LOAI_CTUpdated = false;
		/// <summary>
		/// TEN_LOAI_CT.
		/// </summary>
		public string TEN_LOAI_CT
		{
			get
			{
				return m_TEN_LOAI_CT;
			}
			set
			{
				if ((this.m_TEN_LOAI_CT != value))
				{
					this.SendPropertyChanging("TEN_LOAI_CT");
					this.m_TEN_LOAI_CT = value;
					this.SendPropertyChanged("TEN_LOAI_CT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_LOAI_CTUpdated = true;
				}
			}
		}

		private string m_TEN_LOAI_CT1;
		private bool m_TEN_LOAI_CT1Updated = false;
		/// <summary>
		/// TEN_LOAI_CT1.
		/// </summary>
		public string TEN_LOAI_CT1
		{
			get
			{
				return m_TEN_LOAI_CT1;
			}
			set
			{
				if ((this.m_TEN_LOAI_CT1 != value))
				{
					this.SendPropertyChanging("TEN_LOAI_CT1");
					this.m_TEN_LOAI_CT1 = value;
					this.SendPropertyChanged("TEN_LOAI_CT1");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_LOAI_CT1Updated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SLOAI_CT " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(clsDAL.SelectField("STT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("MA_LOAI_CT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_LOAI_CT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_LOAI_CT1", ProType.OTHER, this.DataManagement));
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
			sbSQL.Append("INSERT INTO SLOAI_CT (STT, MA_LOAI_CT, TEN_LOAI_CT, TEN_LOAI_CT1) VALUES(").Append(clsDAL.IsDBNULL(STT, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(MA_LOAI_CT, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_LOAI_CT, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_LOAI_CT1, ProType.STRING, this.DataManagement)).Append(")");
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SLOAI_CT SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(m_STTUpdated ? string.Format(",STT = {0}", clsDAL.IsDBNULL(STT, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_TEN_LOAI_CTUpdated ? string.Format(",TEN_LOAI_CT = {0}", clsDAL.IsDBNULL(TEN_LOAI_CT, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_LOAI_CT1Updated ? string.Format(",TEN_LOAI_CT1 = {0}", clsDAL.IsDBNULL(TEN_LOAI_CT1, ProType.STRING, this.DataManagement)) : string.Empty);
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
			sbSQL.AppendFormat("STT = {0}", clsDAL.IsDBNULL(STT, ProType.OTHER, this.DataManagement)).AppendFormat(",TEN_LOAI_CT = {0}", clsDAL.IsDBNULL(TEN_LOAI_CT, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_LOAI_CT1 = {0}", clsDAL.IsDBNULL(TEN_LOAI_CT1, ProType.STRING, this.DataManagement));
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
			return clsDAL.DeleteString("SLOAI_CT", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
			sbSQL.AppendFormat("MA_LOAI_CT = {0}", clsDAL.IsDBNULL(MA_LOAI_CT, ProType.STRING, this.DataManagement));
			return sbSQL.ToString();
		}
	}
}