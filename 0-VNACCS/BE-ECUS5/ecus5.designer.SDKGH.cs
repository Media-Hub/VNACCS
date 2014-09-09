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
	/// Generated Class for Table : SDKGH.
	/// </summary>
	public partial class SDKGH : TableBase
	{
		public SDKGH() : base(){}

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
		private string m_MA_GH;
		/// <summary>
		/// MA_GH.
		/// </summary>
		public string MA_GH
		{
			get
			{
				return m_MA_GH;
			}
			set
			{
				if ((this.m_MA_GH != value))
				{
					this.SendPropertyChanging("MA_GH");
					this.m_MA_GH = value;
					this.SendPropertyChanged("MA_GH");
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

		private string m_TEN_GH;
		private bool m_TEN_GHUpdated = false;
		/// <summary>
		/// TEN_GH.
		/// </summary>
		public string TEN_GH
		{
			get
			{
				return m_TEN_GH;
			}
			set
			{
				if ((this.m_TEN_GH != value))
				{
					this.SendPropertyChanging("TEN_GH");
					this.m_TEN_GH = value;
					this.SendPropertyChanged("TEN_GH");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_GHUpdated = true;
				}
			}
		}

		private string m_TEN_GH_TCVN;
		private bool m_TEN_GH_TCVNUpdated = false;
		/// <summary>
		/// TEN_GH_TCVN.
		/// </summary>
		public string TEN_GH_TCVN
		{
			get
			{
				return m_TEN_GH_TCVN;
			}
			set
			{
				if ((this.m_TEN_GH_TCVN != value))
				{
					this.SendPropertyChanging("TEN_GH_TCVN");
					this.m_TEN_GH_TCVN = value;
					this.SendPropertyChanged("TEN_GH_TCVN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_GH_TCVNUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SDKGH " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(clsDAL.SelectField("MA_GH", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("GHICHU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_BANG", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_GH", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_GH_TCVN", ProType.OTHER, this.DataManagement));
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
			sbSQL.Append("INSERT INTO SDKGH (MA_GH, GHICHU, TEN_BANG, TEN_GH, TEN_GH_TCVN) VALUES(").Append(clsDAL.IsDBNULL(MA_GH, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(GHICHU, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_GH, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_GH_TCVN, ProType.STRING, this.DataManagement)).Append(")");
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SDKGH SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(m_GHICHUUpdated ? string.Format(",GHICHU = {0}", clsDAL.IsDBNULL(GHICHU, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_BANGUpdated ? string.Format(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_GHUpdated ? string.Format(",TEN_GH = {0}", clsDAL.IsDBNULL(TEN_GH, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_GH_TCVNUpdated ? string.Format(",TEN_GH_TCVN = {0}", clsDAL.IsDBNULL(TEN_GH_TCVN, ProType.STRING, this.DataManagement)) : string.Empty);
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
			sbSQL.AppendFormat("GHICHU = {0}", clsDAL.IsDBNULL(GHICHU, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_GH = {0}", clsDAL.IsDBNULL(TEN_GH, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_GH_TCVN = {0}", clsDAL.IsDBNULL(TEN_GH_TCVN, ProType.STRING, this.DataManagement));
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
			return clsDAL.DeleteString("SDKGH", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
			sbSQL.AppendFormat("MA_GH = {0}", clsDAL.IsDBNULL(MA_GH, ProType.STRING, this.DataManagement));
			return sbSQL.ToString();
		}
	}
}