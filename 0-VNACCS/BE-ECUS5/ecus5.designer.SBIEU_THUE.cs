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
	/// Generated Class for Table : SBIEU_THUE.
	/// </summary>
	public partial class SBIEU_THUE : TableBase
	{
		public SBIEU_THUE() : base(){}

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
		private string m_MA_BT;
		/// <summary>
		/// MA_BT.
		/// </summary>
		public string MA_BT
		{
			get
			{
				return m_MA_BT;
			}
			set
			{
				if ((this.m_MA_BT != value))
				{
					this.SendPropertyChanging("MA_BT");
					this.m_MA_BT = value;
					this.SendPropertyChanged("MA_BT");
				}
			}
		}

		private string m_HS;
		private bool m_HSUpdated = false;
		/// <summary>
		/// HS.
		/// </summary>
		public string HS
		{
			get
			{
				return m_HS;
			}
			set
			{
				if ((this.m_HS != value))
				{
					this.SendPropertyChanging("HS");
					this.m_HS = value;
					this.SendPropertyChanged("HS");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HSUpdated = true;
				}
			}
		}

		private double? m_TS_TD;
		private bool m_TS_TDUpdated = false;
		/// <summary>
		/// TS_TD.
		/// </summary>
		public double? TS_TD
		{
			get
			{
				return m_TS_TD;
			}
			set
			{
				if ((this.m_TS_TD != value))
				{
					this.SendPropertyChanging("TS_TD");
					this.m_TS_TD = value;
					this.SendPropertyChanged("TS_TD");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TS_TDUpdated = true;
				}
			}
		}

		private double? m_TS;
		private bool m_TSUpdated = false;
		/// <summary>
		/// TS.
		/// </summary>
		public double? TS
		{
			get
			{
				return m_TS;
			}
			set
			{
				if ((this.m_TS != value))
				{
					this.SendPropertyChanging("TS");
					this.m_TS = value;
					this.SendPropertyChanged("TS");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TSUpdated = true;
				}
			}
		}

		private string m_DIEN_GIAI;
		private bool m_DIEN_GIAIUpdated = false;
		/// <summary>
		/// DIEN_GIAI.
		/// </summary>
		public string DIEN_GIAI
		{
			get
			{
				return m_DIEN_GIAI;
			}
			set
			{
				if ((this.m_DIEN_GIAI != value))
				{
					this.SendPropertyChanging("DIEN_GIAI");
					this.m_DIEN_GIAI = value;
					this.SendPropertyChanged("DIEN_GIAI");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DIEN_GIAIUpdated = true;
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

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SBIEU_THUE " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(clsDAL.SelectField("MA_BT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HS", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TS_TD", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TS", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DIEN_GIAI", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("GHI_CHU", ProType.OTHER, this.DataManagement));
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
			sbSQL.Append("INSERT INTO SBIEU_THUE (MA_BT, HS, TS_TD, TS, DIEN_GIAI, GHI_CHU) VALUES(").Append(clsDAL.IsDBNULL(MA_BT, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(HS, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TS_TD, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TS, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(DIEN_GIAI, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(GHI_CHU, ProType.STRING, this.DataManagement)).Append(")");
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SBIEU_THUE SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(m_HSUpdated ? string.Format(",HS = {0}", clsDAL.IsDBNULL(HS, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TS_TDUpdated ? string.Format(",TS_TD = {0}", clsDAL.IsDBNULL(TS_TD, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_TSUpdated ? string.Format(",TS = {0}", clsDAL.IsDBNULL(TS, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_DIEN_GIAIUpdated ? string.Format(",DIEN_GIAI = {0}", clsDAL.IsDBNULL(DIEN_GIAI, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_GHI_CHUUpdated ? string.Format(",GHI_CHU = {0}", clsDAL.IsDBNULL(GHI_CHU, ProType.STRING, this.DataManagement)) : string.Empty);
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
			sbSQL.AppendFormat("HS = {0}", clsDAL.IsDBNULL(HS, ProType.STRING, this.DataManagement)).AppendFormat(",TS_TD = {0}", clsDAL.IsDBNULL(TS_TD, ProType.OTHER, this.DataManagement)).AppendFormat(",TS = {0}", clsDAL.IsDBNULL(TS, ProType.OTHER, this.DataManagement)).AppendFormat(",DIEN_GIAI = {0}", clsDAL.IsDBNULL(DIEN_GIAI, ProType.STRING, this.DataManagement)).AppendFormat(",GHI_CHU = {0}", clsDAL.IsDBNULL(GHI_CHU, ProType.STRING, this.DataManagement));
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
			return clsDAL.DeleteString("SBIEU_THUE", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
			sbSQL.AppendFormat("MA_BT = {0}", clsDAL.IsDBNULL(MA_BT, ProType.STRING, this.DataManagement));
			return sbSQL.ToString();
		}
	}
}