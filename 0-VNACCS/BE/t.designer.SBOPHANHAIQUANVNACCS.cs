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
	/// Generated Class for Table : SBOPHANHAIQUANVNACCS.
	/// </summary>
	public partial class SBOPHANHAIQUANVNACCS : TableBase
	{
		public SBOPHANHAIQUANVNACCS() : base(){}

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
		private string m_BP_MAHQ;
		/// <summary>
		/// BP_MAHQ.
		/// </summary>
		public string BP_MAHQ
		{
			get
			{
				return m_BP_MAHQ;
			}
			set
			{
				if ((this.m_BP_MAHQ != value))
				{
					this.SendPropertyChanging("BP_MAHQ");
					this.m_BP_MAHQ = value;
					this.SendPropertyChanged("BP_MAHQ");
				}
			}
		}

		private string m_BP_MABP;
		/// <summary>
		/// BP_MABP.
		/// </summary>
		public string BP_MABP
		{
			get
			{
				return m_BP_MABP;
			}
			set
			{
				if ((this.m_BP_MABP != value))
				{
					this.SendPropertyChanging("BP_MABP");
					this.m_BP_MABP = value;
					this.SendPropertyChanged("BP_MABP");
				}
			}
		}

		private string m_BP_TENBP;
		private bool m_BP_TENBPUpdated = false;
		/// <summary>
		/// BP_TENBP.
		/// </summary>
		public string BP_TENBP
		{
			get
			{
				return m_BP_TENBP;
			}
			set
			{
				if ((this.m_BP_TENBP != value))
				{
					this.SendPropertyChanging("BP_TENBP");
					this.m_BP_TENBP = value;
					this.SendPropertyChanged("BP_TENBP");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_BP_TENBPUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SBOPHANHAIQUANVNACCS " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[BP_MAHQ]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[BP_MABP]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[BP_TENBP]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("BP_MAHQ", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("BP_MABP", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("BP_TENBP", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO SBOPHANHAIQUANVNACCS ([BP_MAHQ], [BP_MABP], [BP_TENBP]) VALUES(").Append("@BP_MAHQ").Append(",").Append("@BP_MABP").Append(",").Append("@BP_TENBP").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO SBOPHANHAIQUANVNACCS (BP_MAHQ, BP_MABP, BP_TENBP) VALUES(").Append(":BP_MAHQ").Append(",").Append(":BP_MABP").Append(",").Append(":BP_TENBP").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SBOPHANHAIQUANVNACCS SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_BP_TENBPUpdated ? string.Format(",[BP_TENBP] = {0}", "@BP_TENBP") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_BP_TENBPUpdated ? string.Format(",BP_TENBP = {0}", ":BP_TENBP") : string.Empty);
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
				sbSQL.AppendFormat("[BP_TENBP] = {0}", "@BP_TENBP");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("BP_TENBP = {0}", ":BP_TENBP");
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
			return clsDAL.DeleteString("SBOPHANHAIQUANVNACCS", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[BP_MAHQ] = {0}", "@BP_MAHQ").AppendFormat(" AND [BP_MABP] = {0}", "@BP_MABP");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("BP_MAHQ = {0}", ":BP_MAHQ").AppendFormat(" AND BP_MABP = {0}", ":BP_MABP");
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
			paramList.Add(clsDAL.CreateParameter("BP_MAHQ", "WChar", clsDAL.ToDBParam(BP_MAHQ, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("BP_MABP", "WChar", clsDAL.ToDBParam(BP_MABP, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("BP_TENBP", "WChar", clsDAL.ToDBParam(BP_TENBP, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("BP_MAHQ", "WChar", clsDAL.ToDBParam(BP_MAHQ, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("BP_MABP", "WChar", clsDAL.ToDBParam(BP_MABP, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("BP_MAHQ", "WChar", clsDAL.ToDBParam(BP_MAHQ, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("BP_MABP", "WChar", clsDAL.ToDBParam(BP_MABP, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("BP_TENBP", "WChar", clsDAL.ToDBParam(BP_TENBP, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}