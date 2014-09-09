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
	/// Generated Class for Table : SLOAI_CO.
	/// </summary>
	public partial class SLOAI_CO : TableBase
	{
		public SLOAI_CO() : base(){}

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
		private string m_Ma_Loai_CO;
		/// <summary>
		/// Ma_Loai_CO.
		/// </summary>
		public string Ma_Loai_CO
		{
			get
			{
				return m_Ma_Loai_CO;
			}
			set
			{
				if ((this.m_Ma_Loai_CO != value))
				{
					this.SendPropertyChanging("Ma_Loai_CO");
					this.m_Ma_Loai_CO = value;
					this.SendPropertyChanged("Ma_Loai_CO");
				}
			}
		}

		private string m_Ten_Loai_CO;
		private bool m_Ten_Loai_COUpdated = false;
		/// <summary>
		/// Ten_Loai_CO.
		/// </summary>
		public string Ten_Loai_CO
		{
			get
			{
				return m_Ten_Loai_CO;
			}
			set
			{
				if ((this.m_Ten_Loai_CO != value))
				{
					this.SendPropertyChanging("Ten_Loai_CO");
					this.m_Ten_Loai_CO = value;
					this.SendPropertyChanged("Ten_Loai_CO");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_Ten_Loai_COUpdated = true;
				}
			}
		}

		private string m_Ma_Loai_CO_MOI;
		private bool m_Ma_Loai_CO_MOIUpdated = false;
		/// <summary>
		/// Ma_Loai_CO_MOI.
		/// </summary>
		public string Ma_Loai_CO_MOI
		{
			get
			{
				return m_Ma_Loai_CO_MOI;
			}
			set
			{
				if ((this.m_Ma_Loai_CO_MOI != value))
				{
					this.SendPropertyChanging("Ma_Loai_CO_MOI");
					this.m_Ma_Loai_CO_MOI = value;
					this.SendPropertyChanged("Ma_Loai_CO_MOI");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_Ma_Loai_CO_MOIUpdated = true;
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

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SLOAI_CO " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(clsDAL.SelectField("Ma_Loai_CO", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("Ten_Loai_CO", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("Ma_Loai_CO_MOI", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("GHI_CHU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("STT", ProType.OTHER, this.DataManagement));
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
			sbSQL.Append("INSERT INTO SLOAI_CO (Ma_Loai_CO, Ten_Loai_CO, Ma_Loai_CO_MOI, GHI_CHU, STT) VALUES(").Append(clsDAL.IsDBNULL(Ma_Loai_CO, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(Ten_Loai_CO, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(Ma_Loai_CO_MOI, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(GHI_CHU, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(STT, ProType.OTHER, this.DataManagement)).Append(")");
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SLOAI_CO SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(m_Ten_Loai_COUpdated ? string.Format(",Ten_Loai_CO = {0}", clsDAL.IsDBNULL(Ten_Loai_CO, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_Ma_Loai_CO_MOIUpdated ? string.Format(",Ma_Loai_CO_MOI = {0}", clsDAL.IsDBNULL(Ma_Loai_CO_MOI, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_GHI_CHUUpdated ? string.Format(",GHI_CHU = {0}", clsDAL.IsDBNULL(GHI_CHU, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_STTUpdated ? string.Format(",STT = {0}", clsDAL.IsDBNULL(STT, ProType.OTHER, this.DataManagement)) : string.Empty);
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
			sbSQL.AppendFormat("Ten_Loai_CO = {0}", clsDAL.IsDBNULL(Ten_Loai_CO, ProType.STRING, this.DataManagement)).AppendFormat(",Ma_Loai_CO_MOI = {0}", clsDAL.IsDBNULL(Ma_Loai_CO_MOI, ProType.STRING, this.DataManagement)).AppendFormat(",GHI_CHU = {0}", clsDAL.IsDBNULL(GHI_CHU, ProType.STRING, this.DataManagement)).AppendFormat(",STT = {0}", clsDAL.IsDBNULL(STT, ProType.OTHER, this.DataManagement));
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
			return clsDAL.DeleteString("SLOAI_CO", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
			sbSQL.AppendFormat("Ma_Loai_CO = {0}", clsDAL.IsDBNULL(Ma_Loai_CO, ProType.STRING, this.DataManagement));
			return sbSQL.ToString();
		}
	}
}