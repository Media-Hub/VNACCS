using System;
using System.Data;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
namespace DevComponents.DotNetBar
{
	/// <summary>
	/// Generated Class for Table : SNGHTE.
	/// </summary>
	public partial class SNGHTE : TableBase
	{
		public SNGHTE() : base(){}

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
		private string m_NT_MANT;
		/// <summary>
		/// NT_MANT.
		/// </summary>
		public string NT_MANT
		{
			get
			{
				return m_NT_MANT;
			}
			set
			{
				if ((this.m_NT_MANT != value))
				{
					this.SendPropertyChanging("NT_MANT");
					this.m_NT_MANT = value;
					this.SendPropertyChanged("NT_MANT");
				}
			}
		}

		private string m_NT_TENNT;
		private bool m_NT_TENNTUpdated = false;
		/// <summary>
		/// NT_TENNT.
		/// </summary>
		public string NT_TENNT
		{
			get
			{
				return m_NT_TENNT;
			}
			set
			{
				if ((this.m_NT_TENNT != value))
				{
					this.SendPropertyChanging("NT_TENNT");
					this.m_NT_TENNT = value;
					this.SendPropertyChanged("NT_TENNT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NT_TENNTUpdated = true;
				}
			}
		}

		private string m_NT_TENNT_VN;
		private bool m_NT_TENNT_VNUpdated = false;
		/// <summary>
		/// NT_TENNT_VN.
		/// </summary>
		public string NT_TENNT_VN
		{
			get
			{
				return m_NT_TENNT_VN;
			}
			set
			{
				if ((this.m_NT_TENNT_VN != value))
				{
					this.SendPropertyChanging("NT_TENNT_VN");
					this.m_NT_TENNT_VN = value;
					this.SendPropertyChanged("NT_TENNT_VN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NT_TENNT_VNUpdated = true;
				}
			}
		}

		private decimal m_NT_TYGIAVND;
		private bool m_NT_TYGIAVNDUpdated = false;
		/// <summary>
		/// NT_TYGIAVND.
		/// </summary>
		public decimal NT_TYGIAVND
		{
			get
			{
				return m_NT_TYGIAVND;
			}
			set
			{
				if ((this.m_NT_TYGIAVND != value))
				{
					this.SendPropertyChanging("NT_TYGIAVND");
					this.m_NT_TYGIAVND = value;
					this.SendPropertyChanged("NT_TYGIAVND");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NT_TYGIAVNDUpdated = true;
				}
			}
		}

		private string m_NT_TENBANG;
		private bool m_NT_TENBANGUpdated = false;
		/// <summary>
		/// NT_TENBANG.
		/// </summary>
		public string NT_TENBANG
		{
			get
			{
				return m_NT_TENBANG;
			}
			set
			{
				if ((this.m_NT_TENBANG != value))
				{
					this.SendPropertyChanging("NT_TENBANG");
					this.m_NT_TENBANG = value;
					this.SendPropertyChanged("NT_TENBANG");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NT_TENBANGUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SNGHTE " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			switch(this.DataManagement)
			{
				case DBManagement.Access:
				case DBManagement.SQL:
				case DBManagement.SQLLite:
				default:
				sbSQL.Append(clsDAL.SelectField("[NT_MANT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[NT_TENNT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[NT_TENNT_VN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[NT_TYGIAVND]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[NT_TENBANG]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("NT_MANT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NT_TENNT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NT_TENNT_VN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NT_TYGIAVND", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NT_TENBANG", ProType.OTHER, this.DataManagement));
				break;
			}
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
			switch(this.DataManagement)
			{
				case DBManagement.Access:
				case DBManagement.SQL:
				case DBManagement.SQLLite:
				default:
				sbSQL.Append("INSERT INTO SNGHTE ([NT_MANT], [NT_TENNT], [NT_TENNT_VN], [NT_TYGIAVND], [NT_TENBANG]) VALUES(").Append("@NT_MANT").Append(",").Append("@NT_TENNT").Append(",").Append("@NT_TENNT_VN").Append(",").Append("@NT_TYGIAVND").Append(",").Append("@NT_TENBANG").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO SNGHTE (NT_MANT, NT_TENNT, NT_TENNT_VN, NT_TYGIAVND, NT_TENBANG) VALUES(").Append(":NT_MANT").Append(",").Append(":NT_TENNT").Append(",").Append(":NT_TENNT_VN").Append(",").Append(":NT_TYGIAVND").Append(",").Append(":NT_TENBANG").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SNGHTE SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			switch(this.DataManagement)
			{
				case DBManagement.Access:
					return UpdateFullStatement(WhereClause);
				case DBManagement.SQL:
				case DBManagement.SQLLite:
				default:
				sbSQL.Append(m_NT_TENNTUpdated ? string.Format(",[NT_TENNT] = {0}", "@NT_TENNT") : string.Empty).Append(m_NT_TENNT_VNUpdated ? string.Format(",[NT_TENNT_VN] = {0}", "@NT_TENNT_VN") : string.Empty).Append(m_NT_TYGIAVNDUpdated ? string.Format(",[NT_TYGIAVND] = {0}", "@NT_TYGIAVND") : string.Empty).Append(m_NT_TENBANGUpdated ? string.Format(",[NT_TENBANG] = {0}", "@NT_TENBANG") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_NT_TENNTUpdated ? string.Format(",NT_TENNT = {0}", ":NT_TENNT") : string.Empty).Append(m_NT_TENNT_VNUpdated ? string.Format(",NT_TENNT_VN = {0}", ":NT_TENNT_VN") : string.Empty).Append(m_NT_TYGIAVNDUpdated ? string.Format(",NT_TYGIAVND = {0}", ":NT_TYGIAVND") : string.Empty).Append(m_NT_TENBANGUpdated ? string.Format(",NT_TENBANG = {0}", ":NT_TENBANG") : string.Empty);
				break;
			}
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
			switch(this.DataManagement)
			{
				case DBManagement.Access:
				case DBManagement.SQL:
				case DBManagement.SQLLite:
				default:
				sbSQL.AppendFormat("[NT_TENNT] = {0}", "@NT_TENNT").AppendFormat(",[NT_TENNT_VN] = {0}", "@NT_TENNT_VN").AppendFormat(",[NT_TYGIAVND] = {0}", "@NT_TYGIAVND").AppendFormat(",[NT_TENBANG] = {0}", "@NT_TENBANG");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("NT_TENNT = {0}", ":NT_TENNT").AppendFormat(",NT_TENNT_VN = {0}", ":NT_TENNT_VN").AppendFormat(",NT_TYGIAVND = {0}", ":NT_TYGIAVND").AppendFormat(",NT_TENBANG = {0}", ":NT_TENBANG");
				break;
			}
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
			return clsDAL.DeleteString("SNGHTE", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
			switch(this.DataManagement)
			{
				case DBManagement.Access:
				case DBManagement.SQL:
				case DBManagement.SQLLite:
				default:
				sbSQL.AppendFormat("[NT_MANT] = {0}", "@NT_MANT");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("NT_MANT = {0}", ":NT_MANT");
				break;
			}
			return sbSQL.ToString();
		}

		/// <summary>
		/// Tạo parameter để Delete dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> DeleteParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("NT_MANT", "WChar", clsDAL.ToDBParam(NT_MANT, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("NT_TENNT", "WChar", clsDAL.ToDBParam(NT_TENNT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NT_TENNT_VN", "WChar", clsDAL.ToDBParam(NT_TENNT_VN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NT_TYGIAVND", "Numeric", clsDAL.ToDBParam(NT_TYGIAVND, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NT_TENBANG", "WChar", clsDAL.ToDBParam(NT_TENBANG, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NT_MANT", "WChar", clsDAL.ToDBParam(NT_MANT, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("NT_MANT", "WChar", clsDAL.ToDBParam(NT_MANT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NT_TENNT", "WChar", clsDAL.ToDBParam(NT_TENNT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NT_TENNT_VN", "WChar", clsDAL.ToDBParam(NT_TENNT_VN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NT_TYGIAVND", "Numeric", clsDAL.ToDBParam(NT_TYGIAVND, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NT_TENBANG", "WChar", clsDAL.ToDBParam(NT_TENBANG, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}