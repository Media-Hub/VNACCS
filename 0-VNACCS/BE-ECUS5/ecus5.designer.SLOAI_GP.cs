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
	/// Generated Class for Table : SLOAI_GP.
	/// </summary>
	public partial class SLOAI_GP : TableBase
	{
		public SLOAI_GP() : base(){}

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
		private string m_Ma_Loai_GP;
		/// <summary>
		/// Ma_Loai_GP.
		/// </summary>
		public string Ma_Loai_GP
		{
			get
			{
				return m_Ma_Loai_GP;
			}
			set
			{
				if ((this.m_Ma_Loai_GP != value))
				{
					this.SendPropertyChanging("Ma_Loai_GP");
					this.m_Ma_Loai_GP = value;
					this.SendPropertyChanged("Ma_Loai_GP");
				}
			}
		}

		private string m_Ten_Loai_GP;
		private bool m_Ten_Loai_GPUpdated = false;
		/// <summary>
		/// Ten_Loai_GP.
		/// </summary>
		public string Ten_Loai_GP
		{
			get
			{
				return m_Ten_Loai_GP;
			}
			set
			{
				if ((this.m_Ten_Loai_GP != value))
				{
					this.SendPropertyChanging("Ten_Loai_GP");
					this.m_Ten_Loai_GP = value;
					this.SendPropertyChanged("Ten_Loai_GP");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_Ten_Loai_GPUpdated = true;
				}
			}
		}

		private string m_Ten_Loai_GP1;
		private bool m_Ten_Loai_GP1Updated = false;
		/// <summary>
		/// Ten_Loai_GP1.
		/// </summary>
		public string Ten_Loai_GP1
		{
			get
			{
				return m_Ten_Loai_GP1;
			}
			set
			{
				if ((this.m_Ten_Loai_GP1 != value))
				{
					this.SendPropertyChanging("Ten_Loai_GP1");
					this.m_Ten_Loai_GP1 = value;
					this.SendPropertyChanged("Ten_Loai_GP1");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_Ten_Loai_GP1Updated = true;
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
			return "SELECT " + Fields + " FROM SLOAI_GP " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(clsDAL.SelectField("Ma_Loai_GP", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("Ten_Loai_GP", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("Ten_Loai_GP1", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_BANG", ProType.OTHER, this.DataManagement));
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
			sbSQL.Append("INSERT INTO SLOAI_GP (Ma_Loai_GP, Ten_Loai_GP, Ten_Loai_GP1, TEN_BANG) VALUES(").Append(clsDAL.IsDBNULL(Ma_Loai_GP, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(Ten_Loai_GP, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(Ten_Loai_GP1, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)).Append(")");
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SLOAI_GP SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(m_Ten_Loai_GPUpdated ? string.Format(",Ten_Loai_GP = {0}", clsDAL.IsDBNULL(Ten_Loai_GP, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_Ten_Loai_GP1Updated ? string.Format(",Ten_Loai_GP1 = {0}", clsDAL.IsDBNULL(Ten_Loai_GP1, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TEN_BANGUpdated ? string.Format(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement)) : string.Empty);
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
			sbSQL.AppendFormat("Ten_Loai_GP = {0}", clsDAL.IsDBNULL(Ten_Loai_GP, ProType.STRING, this.DataManagement)).AppendFormat(",Ten_Loai_GP1 = {0}", clsDAL.IsDBNULL(Ten_Loai_GP1, ProType.STRING, this.DataManagement)).AppendFormat(",TEN_BANG = {0}", clsDAL.IsDBNULL(TEN_BANG, ProType.STRING, this.DataManagement));
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
			return clsDAL.DeleteString("SLOAI_GP", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
			sbSQL.AppendFormat("Ma_Loai_GP = {0}", clsDAL.IsDBNULL(Ma_Loai_GP, ProType.STRING, this.DataManagement));
			return sbSQL.ToString();
		}
	}
}