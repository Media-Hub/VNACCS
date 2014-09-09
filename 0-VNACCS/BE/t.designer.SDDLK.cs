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
	/// Generated Class for Table : SDDLK.
	/// </summary>
	public partial class SDDLK : TableBase
	{
		public SDDLK() : base(){}

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
		private string m_DDLK_MA;
		/// <summary>
		/// DDLK_MA.
		/// </summary>
		public string DDLK_MA
		{
			get
			{
				return m_DDLK_MA;
			}
			set
			{
				if ((this.m_DDLK_MA != value))
				{
					this.SendPropertyChanging("DDLK_MA");
					this.m_DDLK_MA = value;
					this.SendPropertyChanged("DDLK_MA");
				}
			}
		}

		private string m_DDLK_TEN;
		private bool m_DDLK_TENUpdated = false;
		/// <summary>
		/// DDLK_TEN.
		/// </summary>
		public string DDLK_TEN
		{
			get
			{
				return m_DDLK_TEN;
			}
			set
			{
				if ((this.m_DDLK_TEN != value))
				{
					this.SendPropertyChanging("DDLK_TEN");
					this.m_DDLK_TEN = value;
					this.SendPropertyChanged("DDLK_TEN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DDLK_TENUpdated = true;
				}
			}
		}

		private string m_DDLK_DIACHI;
		private bool m_DDLK_DIACHIUpdated = false;
		/// <summary>
		/// DDLK_DIACHI.
		/// </summary>
		public string DDLK_DIACHI
		{
			get
			{
				return m_DDLK_DIACHI;
			}
			set
			{
				if ((this.m_DDLK_DIACHI != value))
				{
					this.SendPropertyChanging("DDLK_DIACHI");
					this.m_DDLK_DIACHI = value;
					this.SendPropertyChanged("DDLK_DIACHI");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DDLK_DIACHIUpdated = true;
				}
			}
		}

		private string m_DDLK_MAHQ;
		private bool m_DDLK_MAHQUpdated = false;
		/// <summary>
		/// DDLK_MAHQ.
		/// </summary>
		public string DDLK_MAHQ
		{
			get
			{
				return m_DDLK_MAHQ;
			}
			set
			{
				if ((this.m_DDLK_MAHQ != value))
				{
					this.SendPropertyChanging("DDLK_MAHQ");
					this.m_DDLK_MAHQ = value;
					this.SendPropertyChanged("DDLK_MAHQ");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DDLK_MAHQUpdated = true;
				}
			}
		}

		private string m_DDLK_DIABAN;
		private bool m_DDLK_DIABANUpdated = false;
		/// <summary>
		/// DDLK_DIABAN.
		/// </summary>
		public string DDLK_DIABAN
		{
			get
			{
				return m_DDLK_DIABAN;
			}
			set
			{
				if ((this.m_DDLK_DIABAN != value))
				{
					this.SendPropertyChanging("DDLK_DIABAN");
					this.m_DDLK_DIABAN = value;
					this.SendPropertyChanged("DDLK_DIABAN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DDLK_DIABANUpdated = true;
				}
			}
		}

		private string m_DDLH_GHICHU;
		private bool m_DDLH_GHICHUUpdated = false;
		/// <summary>
		/// DDLH_GHICHU.
		/// </summary>
		public string DDLH_GHICHU
		{
			get
			{
				return m_DDLH_GHICHU;
			}
			set
			{
				if ((this.m_DDLH_GHICHU != value))
				{
					this.SendPropertyChanging("DDLH_GHICHU");
					this.m_DDLH_GHICHU = value;
					this.SendPropertyChanged("DDLH_GHICHU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DDLH_GHICHUUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SDDLK " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[DDLK_MA]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DDLK_TEN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DDLK_DIACHI]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DDLK_MAHQ]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DDLK_DIABAN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DDLH_GHICHU]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("DDLK_MA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DDLK_TEN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DDLK_DIACHI", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DDLK_MAHQ", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DDLK_DIABAN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DDLH_GHICHU", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO SDDLK ([DDLK_MA], [DDLK_TEN], [DDLK_DIACHI], [DDLK_MAHQ], [DDLK_DIABAN], [DDLH_GHICHU]) VALUES(").Append("@DDLK_MA").Append(",").Append("@DDLK_TEN").Append(",").Append("@DDLK_DIACHI").Append(",").Append("@DDLK_MAHQ").Append(",").Append("@DDLK_DIABAN").Append(",").Append("@DDLH_GHICHU").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO SDDLK (DDLK_MA, DDLK_TEN, DDLK_DIACHI, DDLK_MAHQ, DDLK_DIABAN, DDLH_GHICHU) VALUES(").Append(":DDLK_MA").Append(",").Append(":DDLK_TEN").Append(",").Append(":DDLK_DIACHI").Append(",").Append(":DDLK_MAHQ").Append(",").Append(":DDLK_DIABAN").Append(",").Append(":DDLH_GHICHU").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SDDLK SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_DDLK_TENUpdated ? string.Format(",[DDLK_TEN] = {0}", "@DDLK_TEN") : string.Empty).Append(m_DDLK_DIACHIUpdated ? string.Format(",[DDLK_DIACHI] = {0}", "@DDLK_DIACHI") : string.Empty).Append(m_DDLK_MAHQUpdated ? string.Format(",[DDLK_MAHQ] = {0}", "@DDLK_MAHQ") : string.Empty).Append(m_DDLK_DIABANUpdated ? string.Format(",[DDLK_DIABAN] = {0}", "@DDLK_DIABAN") : string.Empty).Append(m_DDLH_GHICHUUpdated ? string.Format(",[DDLH_GHICHU] = {0}", "@DDLH_GHICHU") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_DDLK_TENUpdated ? string.Format(",DDLK_TEN = {0}", ":DDLK_TEN") : string.Empty).Append(m_DDLK_DIACHIUpdated ? string.Format(",DDLK_DIACHI = {0}", ":DDLK_DIACHI") : string.Empty).Append(m_DDLK_MAHQUpdated ? string.Format(",DDLK_MAHQ = {0}", ":DDLK_MAHQ") : string.Empty).Append(m_DDLK_DIABANUpdated ? string.Format(",DDLK_DIABAN = {0}", ":DDLK_DIABAN") : string.Empty).Append(m_DDLH_GHICHUUpdated ? string.Format(",DDLH_GHICHU = {0}", ":DDLH_GHICHU") : string.Empty);
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
				sbSQL.AppendFormat("[DDLK_TEN] = {0}", "@DDLK_TEN").AppendFormat(",[DDLK_DIACHI] = {0}", "@DDLK_DIACHI").AppendFormat(",[DDLK_MAHQ] = {0}", "@DDLK_MAHQ").AppendFormat(",[DDLK_DIABAN] = {0}", "@DDLK_DIABAN").AppendFormat(",[DDLH_GHICHU] = {0}", "@DDLH_GHICHU");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("DDLK_TEN = {0}", ":DDLK_TEN").AppendFormat(",DDLK_DIACHI = {0}", ":DDLK_DIACHI").AppendFormat(",DDLK_MAHQ = {0}", ":DDLK_MAHQ").AppendFormat(",DDLK_DIABAN = {0}", ":DDLK_DIABAN").AppendFormat(",DDLH_GHICHU = {0}", ":DDLH_GHICHU");
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
			return clsDAL.DeleteString("SDDLK", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[DDLK_MA] = {0}", "@DDLK_MA");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("DDLK_MA = {0}", ":DDLK_MA");
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
			paramList.Add(clsDAL.CreateParameter("DDLK_MA", "WChar", clsDAL.ToDBParam(DDLK_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("DDLK_TEN", "WChar", clsDAL.ToDBParam(DDLK_TEN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DDLK_DIACHI", "WChar", clsDAL.ToDBParam(DDLK_DIACHI, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DDLK_MAHQ", "WChar", clsDAL.ToDBParam(DDLK_MAHQ, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DDLK_DIABAN", "WChar", clsDAL.ToDBParam(DDLK_DIABAN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DDLH_GHICHU", "WChar", clsDAL.ToDBParam(DDLH_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DDLK_MA", "WChar", clsDAL.ToDBParam(DDLK_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("DDLK_MA", "WChar", clsDAL.ToDBParam(DDLK_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DDLK_TEN", "WChar", clsDAL.ToDBParam(DDLK_TEN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DDLK_DIACHI", "WChar", clsDAL.ToDBParam(DDLK_DIACHI, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DDLK_MAHQ", "WChar", clsDAL.ToDBParam(DDLK_MAHQ, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DDLK_DIABAN", "WChar", clsDAL.ToDBParam(DDLK_DIABAN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DDLH_GHICHU", "WChar", clsDAL.ToDBParam(DDLH_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}