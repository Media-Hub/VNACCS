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
	/// Generated Class for Table : SNGAN_HANG.
	/// </summary>
	public partial class SNGAN_HANG : TableBase
	{
		public SNGAN_HANG() : base(){}

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
		private string m_MA_NH;
		/// <summary>
		/// MA_NH.
		/// </summary>
		public string MA_NH
		{
			get
			{
				return m_MA_NH;
			}
			set
			{
				if ((this.m_MA_NH != value))
				{
					this.SendPropertyChanging("MA_NH");
					this.m_MA_NH = value;
					this.SendPropertyChanged("MA_NH");
				}
			}
		}

		private string m_TEN_NH;
		private bool m_TEN_NHUpdated = false;
		/// <summary>
		/// TEN_NH.
		/// </summary>
		public string TEN_NH
		{
			get
			{
				return m_TEN_NH;
			}
			set
			{
				if ((this.m_TEN_NH != value))
				{
					this.SendPropertyChanging("TEN_NH");
					this.m_TEN_NH = value;
					this.SendPropertyChanged("TEN_NH");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_NHUpdated = true;
				}
			}
		}

		private string m_TEN_NH1;
		private bool m_TEN_NH1Updated = false;
		/// <summary>
		/// TEN_NH1.
		/// </summary>
		public string TEN_NH1
		{
			get
			{
				return m_TEN_NH1;
			}
			set
			{
				if ((this.m_TEN_NH1 != value))
				{
					this.SendPropertyChanging("TEN_NH1");
					this.m_TEN_NH1 = value;
					this.SendPropertyChanged("TEN_NH1");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_NH1Updated = true;
				}
			}
		}

		private string m_GHI_CHU;
		private bool m_GHI_CHUUpdated = false;
		/// <summary>
		/// GHI_CHU.
		/// </summary>
		public string GHI_CHU
		{
			get
			{
				return m_GHI_CHU;
			}
			set
			{
				if ((this.m_GHI_CHU != value))
				{
					this.SendPropertyChanging("GHI_CHU");
					this.m_GHI_CHU = value;
					this.SendPropertyChanged("GHI_CHU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_GHI_CHUUpdated = true;
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
			return "SELECT " + Fields + " FROM SNGAN_HANG " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(clsDAL.SelectField("MA_NH", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_NH", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_NH1", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("GHI_CHU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_BANG", ProType.OTHER, this.DataManagement));
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
			sbSQL.Append("INSERT INTO SNGAN_HANG (MA_NH, TEN_NH, TEN_NH1, GHI_CHU, TEN_BANG) VALUES(").Append(clsDAL.IsDBNULL(MA_NH, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_NH, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_NH1, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(GHI_CHU, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).Append(")");
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SNGAN_HANG SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(m_TEN_NHUpdated ? string.Format(",TEN_NH = {0}", clsDAL.IsDBNULL(TEN_NH, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_NH1Updated ? string.Format(",TEN_NH1 = {0}", clsDAL.IsDBNULL(TEN_NH1, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_GHI_CHUUpdated ? string.Format(",GHI_CHU = {0}", clsDAL.IsDBNULL(GHI_CHU, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_BANGUpdated ? string.Format(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)) : string.Empty);
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
			sbSQL.AppendFormat("TEN_NH = {0}", clsDAL.IsDBNULL(TEN_NH, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_NH1 = {0}", clsDAL.IsDBNULL(TEN_NH1, ProType.STRING, this.DataManagement)).AppendFormat(",GHI_CHU = {0}", clsDAL.IsDBNULL(GHI_CHU, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement));
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
			return clsDAL.DeleteString("SNGAN_HANG", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
			sbSQL.AppendFormat("MA_NH = {0}", clsDAL.IsDBNULL(MA_NH, ProType.STRING, this.DataManagement));
			return sbSQL.ToString();
		}
	}
}