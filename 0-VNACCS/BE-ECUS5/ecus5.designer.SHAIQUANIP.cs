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
	/// Generated Class for Table : SHAIQUANIP.
	/// </summary>
	public partial class SHAIQUANIP : TableBase
	{
		public SHAIQUANIP() : base(){}

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
		private string m_Ma_HQ;
		/// <summary>
		/// Ma_HQ.
		/// </summary>
		public string Ma_HQ
		{
			get
			{
				return m_Ma_HQ;
			}
			set
			{
				if ((this.m_Ma_HQ != value))
				{
					this.SendPropertyChanging("Ma_HQ");
					this.m_Ma_HQ = value;
					this.SendPropertyChanged("Ma_HQ");
				}
			}
		}

		private string m_Ten_HQ;
		private bool m_Ten_HQUpdated = false;
		/// <summary>
		/// Ten_HQ.
		/// </summary>
		public string Ten_HQ
		{
			get
			{
				return m_Ten_HQ;
			}
			set
			{
				if ((this.m_Ten_HQ != value))
				{
					this.SendPropertyChanging("Ten_HQ");
					this.m_Ten_HQ = value;
					this.SendPropertyChanged("Ten_HQ");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_Ten_HQUpdated = true;
				}
			}
		}

		private string m_Ten_HQ1;
		private bool m_Ten_HQ1Updated = false;
		/// <summary>
		/// Ten_HQ1.
		/// </summary>
		public string Ten_HQ1
		{
			get
			{
				return m_Ten_HQ1;
			}
			set
			{
				if ((this.m_Ten_HQ1 != value))
				{
					this.SendPropertyChanging("Ten_HQ1");
					this.m_Ten_HQ1 = value;
					this.SendPropertyChanged("Ten_HQ1");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_Ten_HQ1Updated = true;
				}
			}
		}

		private int m_Cap_HQ;
		private bool m_Cap_HQUpdated = false;
		/// <summary>
		/// Cap_HQ.
		/// </summary>
		public int Cap_HQ
		{
			get
			{
				return m_Cap_HQ;
			}
			set
			{
				if ((this.m_Cap_HQ != value))
				{
					this.SendPropertyChanging("Cap_HQ");
					this.m_Cap_HQ = value;
					this.SendPropertyChanged("Cap_HQ");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_Cap_HQUpdated = true;
				}
			}
		}

		private int? m_HienThi;
		private bool m_HienThiUpdated = false;
		/// <summary>
		/// HienThi.
		/// </summary>
		public int? HienThi
		{
			get
			{
				return m_HienThi;
			}
			set
			{
				if ((this.m_HienThi != value))
				{
					this.SendPropertyChanging("HienThi");
					this.m_HienThi = value;
					this.SendPropertyChanged("HienThi");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HienThiUpdated = true;
				}
			}
		}

		private string m_IP;
		private bool m_IPUpdated = false;
		/// <summary>
		/// IP.
		/// </summary>
		public string IP
		{
			get
			{
				return m_IP;
			}
			set
			{
				if ((this.m_IP != value))
				{
					this.SendPropertyChanging("IP");
					this.m_IP = value;
					this.SendPropertyChanged("IP");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_IPUpdated = true;
				}
			}
		}

		private string m_IPCKS;
		private bool m_IPCKSUpdated = false;
		/// <summary>
		/// IPCKS.
		/// </summary>
		public string IPCKS
		{
			get
			{
				return m_IPCKS;
			}
			set
			{
				if ((this.m_IPCKS != value))
				{
					this.SendPropertyChanging("IPCKS");
					this.m_IPCKS = value;
					this.SendPropertyChanged("IPCKS");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_IPCKSUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SHAIQUANIP " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(clsDAL.SelectField("Ma_HQ", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("Ten_HQ", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("Ten_HQ1", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("Cap_HQ", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HienThi", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("IP", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("IPCKS", ProType.OTHER, this.DataManagement));
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
			sbSQL.Append("INSERT INTO SHAIQUANIP (Ma_HQ, Ten_HQ, Ten_HQ1, Cap_HQ, HienThi, IP, IPCKS) VALUES(").Append(clsDAL.IsDBNULL(Ma_HQ, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(Ten_HQ, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(Ten_HQ1, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(Cap_HQ, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(IP, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(IPCKS, ProType.STRING, this.DataManagement)).Append(")");
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SHAIQUANIP SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(m_Ten_HQUpdated ? string.Format(",Ten_HQ = {0}", clsDAL.IsDBNULL(Ten_HQ, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_Ten_HQ1Updated ? string.Format(",Ten_HQ1 = {0}", clsDAL.IsDBNULL(Ten_HQ1, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_Cap_HQUpdated ? string.Format(",Cap_HQ = {0}", clsDAL.IsDBNULL(Cap_HQ, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_HienThiUpdated ? string.Format(",HienThi = {0}", clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_IPUpdated ? string.Format(",IP = {0}", clsDAL.IsDBNULL(IP, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_IPCKSUpdated ? string.Format(",IPCKS = {0}", clsDAL.IsDBNULL(IPCKS, ProType.STRING, this.DataManagement)) : string.Empty);
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
			sbSQL.AppendFormat("Ten_HQ = {0}", clsDAL.IsDBNULL(Ten_HQ, ProType.STRING, this.DataManagement)).AppendFormat(",Ten_HQ1 = {0}", clsDAL.IsDBNULL(Ten_HQ1, ProType.STRING, this.DataManagement)).AppendFormat(",Cap_HQ = {0}", clsDAL.IsDBNULL(Cap_HQ, ProType.OTHER, this.DataManagement)).AppendFormat(",HienThi = {0}", clsDAL.IsDBNULL(HienThi, ProType.OTHER, this.DataManagement)).AppendFormat(",IP = {0}", clsDAL.IsDBNULL(IP, ProType.STRING, this.DataManagement)).AppendFormat(",IPCKS = {0}", clsDAL.IsDBNULL(IPCKS, ProType.STRING, this.DataManagement));
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
			return clsDAL.DeleteString("SHAIQUANIP", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
			sbSQL.AppendFormat("Ma_HQ = {0}", clsDAL.IsDBNULL(Ma_HQ, ProType.STRING, this.DataManagement));
			return sbSQL.ToString();
		}
	}
}