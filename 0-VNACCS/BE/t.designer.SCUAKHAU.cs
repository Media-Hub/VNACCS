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
	/// Generated Class for Table : SCUAKHAU.
	/// </summary>
	public partial class SCUAKHAU : TableBase
	{
		public SCUAKHAU() : base(){}

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
		private string m_CK_MANUOC;
		/// <summary>
		/// CK_MANUOC.
		/// </summary>
		public string CK_MANUOC
		{
			get
			{
				return m_CK_MANUOC;
			}
			set
			{
				if ((this.m_CK_MANUOC != value))
				{
					this.SendPropertyChanging("CK_MANUOC");
					this.m_CK_MANUOC = value;
					this.SendPropertyChanged("CK_MANUOC");
				}
			}
		}

		private string m_CK_MACK;
		/// <summary>
		/// CK_MACK.
		/// </summary>
		public string CK_MACK
		{
			get
			{
				return m_CK_MACK;
			}
			set
			{
				if ((this.m_CK_MACK != value))
				{
					this.SendPropertyChanging("CK_MACK");
					this.m_CK_MACK = value;
					this.SendPropertyChanged("CK_MACK");
				}
			}
		}

		private string m_CK_TENCK;
		private bool m_CK_TENCKUpdated = false;
		/// <summary>
		/// CK_TENCK.
		/// </summary>
		public string CK_TENCK
		{
			get
			{
				return m_CK_TENCK;
			}
			set
			{
				if ((this.m_CK_TENCK != value))
				{
					this.SendPropertyChanging("CK_TENCK");
					this.m_CK_TENCK = value;
					this.SendPropertyChanged("CK_TENCK");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_CK_TENCKUpdated = true;
				}
			}
		}

		private string m_CK_TENCK_VN;
		private bool m_CK_TENCK_VNUpdated = false;
		/// <summary>
		/// CK_TENCK_VN.
		/// </summary>
		public string CK_TENCK_VN
		{
			get
			{
				return m_CK_TENCK_VN;
			}
			set
			{
				if ((this.m_CK_TENCK_VN != value))
				{
					this.SendPropertyChanging("CK_TENCK_VN");
					this.m_CK_TENCK_VN = value;
					this.SendPropertyChanged("CK_TENCK_VN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_CK_TENCK_VNUpdated = true;
				}
			}
		}

		private string m_CK_MACKCU;
		private bool m_CK_MACKCUUpdated = false;
		/// <summary>
		/// CK_MACKCU.
		/// </summary>
		public string CK_MACKCU
		{
			get
			{
				return m_CK_MACKCU;
			}
			set
			{
				if ((this.m_CK_MACKCU != value))
				{
					this.SendPropertyChanging("CK_MACKCU");
					this.m_CK_MACKCU = value;
					this.SendPropertyChanged("CK_MACKCU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_CK_MACKCUUpdated = true;
				}
			}
		}

		private string m_CK_TENCKCU;
		private bool m_CK_TENCKCUUpdated = false;
		/// <summary>
		/// CK_TENCKCU.
		/// </summary>
		public string CK_TENCKCU
		{
			get
			{
				return m_CK_TENCKCU;
			}
			set
			{
				if ((this.m_CK_TENCKCU != value))
				{
					this.SendPropertyChanging("CK_TENCKCU");
					this.m_CK_TENCKCU = value;
					this.SendPropertyChanged("CK_TENCKCU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_CK_TENCKCUUpdated = true;
				}
			}
		}

		private string m_CK_TENBANG;
		private bool m_CK_TENBANGUpdated = false;
		/// <summary>
		/// CK_TENBANG.
		/// </summary>
		public string CK_TENBANG
		{
			get
			{
				return m_CK_TENBANG;
			}
			set
			{
				if ((this.m_CK_TENBANG != value))
				{
					this.SendPropertyChanging("CK_TENBANG");
					this.m_CK_TENBANG = value;
					this.SendPropertyChanged("CK_TENBANG");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_CK_TENBANGUpdated = true;
				}
			}
		}

		private bool m_CK_HIENTHI = false;
		private bool m_CK_HIENTHIUpdated = false;
		/// <summary>
		/// CK_HIENTHI.
		/// </summary>
		public bool CK_HIENTHI
		{
			get
			{
				return m_CK_HIENTHI;
			}
			set
			{
				if ((this.m_CK_HIENTHI != value))
				{
					this.SendPropertyChanging("CK_HIENTHI");
					this.m_CK_HIENTHI = value;
					this.SendPropertyChanged("CK_HIENTHI");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_CK_HIENTHIUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SCUAKHAU " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[CK_MANUOC]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[CK_MACK]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[CK_TENCK]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[CK_TENCK_VN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[CK_MACKCU]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[CK_TENCKCU]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[CK_TENBANG]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[CK_HIENTHI]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("CK_MANUOC", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("CK_MACK", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("CK_TENCK", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("CK_TENCK_VN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("CK_MACKCU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("CK_TENCKCU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("CK_TENBANG", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("CK_HIENTHI", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO SCUAKHAU ([CK_MANUOC], [CK_MACK], [CK_TENCK], [CK_TENCK_VN], [CK_MACKCU], [CK_TENCKCU], [CK_TENBANG], [CK_HIENTHI]) VALUES(").Append("@CK_MANUOC").Append(",").Append("@CK_MACK").Append(",").Append("@CK_TENCK").Append(",").Append("@CK_TENCK_VN").Append(",").Append("@CK_MACKCU").Append(",").Append("@CK_TENCKCU").Append(",").Append("@CK_TENBANG").Append(",").Append("@CK_HIENTHI").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO SCUAKHAU (CK_MANUOC, CK_MACK, CK_TENCK, CK_TENCK_VN, CK_MACKCU, CK_TENCKCU, CK_TENBANG, CK_HIENTHI) VALUES(").Append(":CK_MANUOC").Append(",").Append(":CK_MACK").Append(",").Append(":CK_TENCK").Append(",").Append(":CK_TENCK_VN").Append(",").Append(":CK_MACKCU").Append(",").Append(":CK_TENCKCU").Append(",").Append(":CK_TENBANG").Append(",").Append(":CK_HIENTHI").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SCUAKHAU SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_CK_TENCKUpdated ? string.Format(",[CK_TENCK] = {0}", "@CK_TENCK") : string.Empty).Append(m_CK_TENCK_VNUpdated ? string.Format(",[CK_TENCK_VN] = {0}", "@CK_TENCK_VN") : string.Empty).Append(m_CK_MACKCUUpdated ? string.Format(",[CK_MACKCU] = {0}", "@CK_MACKCU") : string.Empty).Append(m_CK_TENCKCUUpdated ? string.Format(",[CK_TENCKCU] = {0}", "@CK_TENCKCU") : string.Empty).Append(m_CK_TENBANGUpdated ? string.Format(",[CK_TENBANG] = {0}", "@CK_TENBANG") : string.Empty).Append(m_CK_HIENTHIUpdated ? string.Format(",[CK_HIENTHI] = {0}", "@CK_HIENTHI") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_CK_TENCKUpdated ? string.Format(",CK_TENCK = {0}", ":CK_TENCK") : string.Empty).Append(m_CK_TENCK_VNUpdated ? string.Format(",CK_TENCK_VN = {0}", ":CK_TENCK_VN") : string.Empty).Append(m_CK_MACKCUUpdated ? string.Format(",CK_MACKCU = {0}", ":CK_MACKCU") : string.Empty).Append(m_CK_TENCKCUUpdated ? string.Format(",CK_TENCKCU = {0}", ":CK_TENCKCU") : string.Empty).Append(m_CK_TENBANGUpdated ? string.Format(",CK_TENBANG = {0}", ":CK_TENBANG") : string.Empty).Append(m_CK_HIENTHIUpdated ? string.Format(",CK_HIENTHI = {0}", ":CK_HIENTHI") : string.Empty);
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
				sbSQL.AppendFormat("[CK_TENCK] = {0}", "@CK_TENCK").AppendFormat(",[CK_TENCK_VN] = {0}", "@CK_TENCK_VN").AppendFormat(",[CK_MACKCU] = {0}", "@CK_MACKCU").AppendFormat(",[CK_TENCKCU] = {0}", "@CK_TENCKCU").AppendFormat(",[CK_TENBANG] = {0}", "@CK_TENBANG").AppendFormat(",[CK_HIENTHI] = {0}", "@CK_HIENTHI");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("CK_TENCK = {0}", ":CK_TENCK").AppendFormat(",CK_TENCK_VN = {0}", ":CK_TENCK_VN").AppendFormat(",CK_MACKCU = {0}", ":CK_MACKCU").AppendFormat(",CK_TENCKCU = {0}", ":CK_TENCKCU").AppendFormat(",CK_TENBANG = {0}", ":CK_TENBANG").AppendFormat(",CK_HIENTHI = {0}", ":CK_HIENTHI");
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
			return clsDAL.DeleteString("SCUAKHAU", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[CK_MANUOC] = {0}", "@CK_MANUOC").AppendFormat(" AND [CK_MACK] = {0}", "@CK_MACK");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("CK_MANUOC = {0}", ":CK_MANUOC").AppendFormat(" AND CK_MACK = {0}", ":CK_MACK");
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
			paramList.Add(clsDAL.CreateParameter("CK_MANUOC", "WChar", clsDAL.ToDBParam(CK_MANUOC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CK_MACK", "WChar", clsDAL.ToDBParam(CK_MACK, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("CK_TENCK", "WChar", clsDAL.ToDBParam(CK_TENCK, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CK_TENCK_VN", "WChar", clsDAL.ToDBParam(CK_TENCK_VN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CK_MACKCU", "WChar", clsDAL.ToDBParam(CK_MACKCU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CK_TENCKCU", "WChar", clsDAL.ToDBParam(CK_TENCKCU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CK_TENBANG", "WChar", clsDAL.ToDBParam(CK_TENBANG, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CK_HIENTHI", "Boolean", clsDAL.ToDBParam(CK_HIENTHI, ProType.BOOL, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CK_MANUOC", "WChar", clsDAL.ToDBParam(CK_MANUOC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CK_MACK", "WChar", clsDAL.ToDBParam(CK_MACK, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("CK_MANUOC", "WChar", clsDAL.ToDBParam(CK_MANUOC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CK_MACK", "WChar", clsDAL.ToDBParam(CK_MACK, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CK_TENCK", "WChar", clsDAL.ToDBParam(CK_TENCK, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CK_TENCK_VN", "WChar", clsDAL.ToDBParam(CK_TENCK_VN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CK_MACKCU", "WChar", clsDAL.ToDBParam(CK_MACKCU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CK_TENCKCU", "WChar", clsDAL.ToDBParam(CK_TENCKCU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CK_TENBANG", "WChar", clsDAL.ToDBParam(CK_TENBANG, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CK_HIENTHI", "Boolean", clsDAL.ToDBParam(CK_HIENTHI, ProType.BOOL, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}