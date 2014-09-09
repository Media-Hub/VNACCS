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
	/// Generated Class for Table : SCUCHQ.
	/// </summary>
	public partial class SCUCHQ : TableBase
	{
		public SCUCHQ() : base(){}

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
		private string m_Ma_CUC;
		/// <summary>
		/// Ma_CUC.
		/// </summary>
		public string Ma_CUC
		{
			get
			{
				return m_Ma_CUC;
			}
			set
			{
				if ((this.m_Ma_CUC != value))
				{
					this.SendPropertyChanging("Ma_CUC");
					this.m_Ma_CUC = value;
					this.SendPropertyChanged("Ma_CUC");
				}
			}
		}

		private string m_Ten_CUC;
		private bool m_Ten_CUCUpdated = false;
		/// <summary>
		/// Ten_CUC.
		/// </summary>
		public string Ten_CUC
		{
			get
			{
				return m_Ten_CUC;
			}
			set
			{
				if ((this.m_Ten_CUC != value))
				{
					this.SendPropertyChanging("Ten_CUC");
					this.m_Ten_CUC = value;
					this.SendPropertyChanged("Ten_CUC");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_Ten_CUCUpdated = true;
				}
			}
		}

		private string m_Ten_CUC1;
		private bool m_Ten_CUC1Updated = false;
		/// <summary>
		/// Ten_CUC1.
		/// </summary>
		public string Ten_CUC1
		{
			get
			{
				return m_Ten_CUC1;
			}
			set
			{
				if ((this.m_Ten_CUC1 != value))
				{
					this.SendPropertyChanging("Ten_CUC1");
					this.m_Ten_CUC1 = value;
					this.SendPropertyChanged("Ten_CUC1");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_Ten_CUC1Updated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SCUCHQ " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(clsDAL.SelectField("Ma_CUC", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("Ten_CUC", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("Ten_CUC1", ProType.OTHER, this.DataManagement));
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
			sbSQL.Append("INSERT INTO SCUCHQ (Ma_CUC, Ten_CUC, Ten_CUC1) VALUES(").Append(clsDAL.IsDBNULL(Ma_CUC, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(Ten_CUC, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(Ten_CUC1, ProType.STRING, this.DataManagement)).Append(")");
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SCUCHQ SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(m_Ten_CUCUpdated ? string.Format(",Ten_CUC = {0}", clsDAL.IsDBNULL(Ten_CUC, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_Ten_CUC1Updated ? string.Format(",Ten_CUC1 = {0}", clsDAL.IsDBNULL(Ten_CUC1, ProType.STRING, this.DataManagement)) : string.Empty);
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
			sbSQL.AppendFormat("Ten_CUC = {0}", clsDAL.IsDBNULL(Ten_CUC, ProType.STRING, this.DataManagement)).AppendFormat(",Ten_CUC1 = {0}", clsDAL.IsDBNULL(Ten_CUC1, ProType.STRING, this.DataManagement));
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
			return clsDAL.DeleteString("SCUCHQ", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
			sbSQL.AppendFormat("Ma_CUC = {0}", clsDAL.IsDBNULL(Ma_CUC, ProType.STRING, this.DataManagement));
			return sbSQL.ToString();
		}
	}
}