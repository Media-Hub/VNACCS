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
	/// Generated Class for Table : SDOITAC.
	/// </summary>
	public partial class SDOITAC : TableBase
	{
		public SDOITAC() : base(){}

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
		private string m_DT_TENDT;
		/// <summary>
		/// DT_TENDT.
		/// </summary>
		public string DT_TENDT
		{
			get
			{
				return m_DT_TENDT;
			}
			set
			{
				if ((this.m_DT_TENDT != value))
				{
					this.SendPropertyChanging("DT_TENDT");
					this.m_DT_TENDT = value;
					this.SendPropertyChanged("DT_TENDT");
				}
			}
		}

		private string m_DT_MADT;
		private bool m_DT_MADTUpdated = false;
		/// <summary>
		/// DT_MADT.
		/// </summary>
		public string DT_MADT
		{
			get
			{
				return m_DT_MADT;
			}
			set
			{
				if ((this.m_DT_MADT != value))
				{
					this.SendPropertyChanging("DT_MADT");
					this.m_DT_MADT = value;
					this.SendPropertyChanged("DT_MADT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DT_MADTUpdated = true;
				}
			}
		}

		private string m_DT_MABUUCHINH;
		private bool m_DT_MABUUCHINHUpdated = false;
		/// <summary>
		/// DT_MABUUCHINH.
		/// </summary>
		public string DT_MABUUCHINH
		{
			get
			{
				return m_DT_MABUUCHINH;
			}
			set
			{
				if ((this.m_DT_MABUUCHINH != value))
				{
					this.SendPropertyChanging("DT_MABUUCHINH");
					this.m_DT_MABUUCHINH = value;
					this.SendPropertyChanged("DT_MABUUCHINH");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DT_MABUUCHINHUpdated = true;
				}
			}
		}

		private string m_DT_SONHA_TENDUONG;
		private bool m_DT_SONHA_TENDUONGUpdated = false;
		/// <summary>
		/// DT_SONHA_TENDUONG.
		/// </summary>
		public string DT_SONHA_TENDUONG
		{
			get
			{
				return m_DT_SONHA_TENDUONG;
			}
			set
			{
				if ((this.m_DT_SONHA_TENDUONG != value))
				{
					this.SendPropertyChanging("DT_SONHA_TENDUONG");
					this.m_DT_SONHA_TENDUONG = value;
					this.SendPropertyChanged("DT_SONHA_TENDUONG");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DT_SONHA_TENDUONGUpdated = true;
				}
			}
		}

		private string m_DT_QUAN_HUYEN;
		private bool m_DT_QUAN_HUYENUpdated = false;
		/// <summary>
		/// DT_QUAN_HUYEN.
		/// </summary>
		public string DT_QUAN_HUYEN
		{
			get
			{
				return m_DT_QUAN_HUYEN;
			}
			set
			{
				if ((this.m_DT_QUAN_HUYEN != value))
				{
					this.SendPropertyChanging("DT_QUAN_HUYEN");
					this.m_DT_QUAN_HUYEN = value;
					this.SendPropertyChanged("DT_QUAN_HUYEN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DT_QUAN_HUYENUpdated = true;
				}
			}
		}

		private string m_DT_TINH_THANHPHO;
		private bool m_DT_TINH_THANHPHOUpdated = false;
		/// <summary>
		/// DT_TINH_THANHPHO.
		/// </summary>
		public string DT_TINH_THANHPHO
		{
			get
			{
				return m_DT_TINH_THANHPHO;
			}
			set
			{
				if ((this.m_DT_TINH_THANHPHO != value))
				{
					this.SendPropertyChanging("DT_TINH_THANHPHO");
					this.m_DT_TINH_THANHPHO = value;
					this.SendPropertyChanged("DT_TINH_THANHPHO");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DT_TINH_THANHPHOUpdated = true;
				}
			}
		}

		private string m_DT_TENNUOC;
		private bool m_DT_TENNUOCUpdated = false;
		/// <summary>
		/// DT_TENNUOC.
		/// </summary>
		public string DT_TENNUOC
		{
			get
			{
				return m_DT_TENNUOC;
			}
			set
			{
				if ((this.m_DT_TENNUOC != value))
				{
					this.SendPropertyChanging("DT_TENNUOC");
					this.m_DT_TENNUOC = value;
					this.SendPropertyChanged("DT_TENNUOC");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DT_TENNUOCUpdated = true;
				}
			}
		}

		private string m_DT_DIENTHOAI;
		private bool m_DT_DIENTHOAIUpdated = false;
		/// <summary>
		/// DT_DIENTHOAI.
		/// </summary>
		public string DT_DIENTHOAI
		{
			get
			{
				return m_DT_DIENTHOAI;
			}
			set
			{
				if ((this.m_DT_DIENTHOAI != value))
				{
					this.SendPropertyChanging("DT_DIENTHOAI");
					this.m_DT_DIENTHOAI = value;
					this.SendPropertyChanged("DT_DIENTHOAI");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DT_DIENTHOAIUpdated = true;
				}
			}
		}

		private string m_DT_FAX;
		private bool m_DT_FAXUpdated = false;
		/// <summary>
		/// DT_FAX.
		/// </summary>
		public string DT_FAX
		{
			get
			{
				return m_DT_FAX;
			}
			set
			{
				if ((this.m_DT_FAX != value))
				{
					this.SendPropertyChanging("DT_FAX");
					this.m_DT_FAX = value;
					this.SendPropertyChanged("DT_FAX");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DT_FAXUpdated = true;
				}
			}
		}

		private string m_DT_EMAIL;
		private bool m_DT_EMAILUpdated = false;
		/// <summary>
		/// DT_EMAIL.
		/// </summary>
		public string DT_EMAIL
		{
			get
			{
				return m_DT_EMAIL;
			}
			set
			{
				if ((this.m_DT_EMAIL != value))
				{
					this.SendPropertyChanging("DT_EMAIL");
					this.m_DT_EMAIL = value;
					this.SendPropertyChanged("DT_EMAIL");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DT_EMAILUpdated = true;
				}
			}
		}

		private string m_DT_GHICHU;
		private bool m_DT_GHICHUUpdated = false;
		/// <summary>
		/// DT_GHICHU.
		/// </summary>
		public string DT_GHICHU
		{
			get
			{
				return m_DT_GHICHU;
			}
			set
			{
				if ((this.m_DT_GHICHU != value))
				{
					this.SendPropertyChanging("DT_GHICHU");
					this.m_DT_GHICHU = value;
					this.SendPropertyChanged("DT_GHICHU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DT_GHICHUUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SDOITAC " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[DT_TENDT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DT_MADT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DT_MABUUCHINH]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DT_SONHA_TENDUONG]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DT_QUAN_HUYEN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DT_TINH_THANHPHO]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DT_TENNUOC]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DT_DIENTHOAI]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DT_FAX]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DT_EMAIL]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DT_GHICHU]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("DT_TENDT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DT_MADT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DT_MABUUCHINH", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DT_SONHA_TENDUONG", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DT_QUAN_HUYEN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DT_TINH_THANHPHO", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DT_TENNUOC", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DT_DIENTHOAI", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DT_FAX", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DT_EMAIL", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DT_GHICHU", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO SDOITAC ([DT_TENDT], [DT_MADT], [DT_MABUUCHINH], [DT_SONHA_TENDUONG], [DT_QUAN_HUYEN], [DT_TINH_THANHPHO], [DT_TENNUOC], [DT_DIENTHOAI], [DT_FAX], [DT_EMAIL], [DT_GHICHU]) VALUES(").Append("@DT_TENDT").Append(",").Append("@DT_MADT").Append(",").Append("@DT_MABUUCHINH").Append(",").Append("@DT_SONHA_TENDUONG").Append(",").Append("@DT_QUAN_HUYEN").Append(",").Append("@DT_TINH_THANHPHO").Append(",").Append("@DT_TENNUOC").Append(",").Append("@DT_DIENTHOAI").Append(",").Append("@DT_FAX").Append(",").Append("@DT_EMAIL").Append(",").Append("@DT_GHICHU").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO SDOITAC (DT_TENDT, DT_MADT, DT_MABUUCHINH, DT_SONHA_TENDUONG, DT_QUAN_HUYEN, DT_TINH_THANHPHO, DT_TENNUOC, DT_DIENTHOAI, DT_FAX, DT_EMAIL, DT_GHICHU) VALUES(").Append(":DT_TENDT").Append(",").Append(":DT_MADT").Append(",").Append(":DT_MABUUCHINH").Append(",").Append(":DT_SONHA_TENDUONG").Append(",").Append(":DT_QUAN_HUYEN").Append(",").Append(":DT_TINH_THANHPHO").Append(",").Append(":DT_TENNUOC").Append(",").Append(":DT_DIENTHOAI").Append(",").Append(":DT_FAX").Append(",").Append(":DT_EMAIL").Append(",").Append(":DT_GHICHU").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SDOITAC SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_DT_MADTUpdated ? string.Format(",[DT_MADT] = {0}", "@DT_MADT") : string.Empty).Append(m_DT_MABUUCHINHUpdated ? string.Format(",[DT_MABUUCHINH] = {0}", "@DT_MABUUCHINH") : string.Empty).Append(m_DT_SONHA_TENDUONGUpdated ? string.Format(",[DT_SONHA_TENDUONG] = {0}", "@DT_SONHA_TENDUONG") : string.Empty).Append(m_DT_QUAN_HUYENUpdated ? string.Format(",[DT_QUAN_HUYEN] = {0}", "@DT_QUAN_HUYEN") : string.Empty).Append(m_DT_TINH_THANHPHOUpdated ? string.Format(",[DT_TINH_THANHPHO] = {0}", "@DT_TINH_THANHPHO") : string.Empty).Append(m_DT_TENNUOCUpdated ? string.Format(",[DT_TENNUOC] = {0}", "@DT_TENNUOC") : string.Empty).Append(m_DT_DIENTHOAIUpdated ? string.Format(",[DT_DIENTHOAI] = {0}", "@DT_DIENTHOAI") : string.Empty).Append(m_DT_FAXUpdated ? string.Format(",[DT_FAX] = {0}", "@DT_FAX") : string.Empty).Append(m_DT_EMAILUpdated ? string.Format(",[DT_EMAIL] = {0}", "@DT_EMAIL") : string.Empty).Append(m_DT_GHICHUUpdated ? string.Format(",[DT_GHICHU] = {0}", "@DT_GHICHU") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_DT_MADTUpdated ? string.Format(",DT_MADT = {0}", ":DT_MADT") : string.Empty).Append(m_DT_MABUUCHINHUpdated ? string.Format(",DT_MABUUCHINH = {0}", ":DT_MABUUCHINH") : string.Empty).Append(m_DT_SONHA_TENDUONGUpdated ? string.Format(",DT_SONHA_TENDUONG = {0}", ":DT_SONHA_TENDUONG") : string.Empty).Append(m_DT_QUAN_HUYENUpdated ? string.Format(",DT_QUAN_HUYEN = {0}", ":DT_QUAN_HUYEN") : string.Empty).Append(m_DT_TINH_THANHPHOUpdated ? string.Format(",DT_TINH_THANHPHO = {0}", ":DT_TINH_THANHPHO") : string.Empty).Append(m_DT_TENNUOCUpdated ? string.Format(",DT_TENNUOC = {0}", ":DT_TENNUOC") : string.Empty).Append(m_DT_DIENTHOAIUpdated ? string.Format(",DT_DIENTHOAI = {0}", ":DT_DIENTHOAI") : string.Empty).Append(m_DT_FAXUpdated ? string.Format(",DT_FAX = {0}", ":DT_FAX") : string.Empty).Append(m_DT_EMAILUpdated ? string.Format(",DT_EMAIL = {0}", ":DT_EMAIL") : string.Empty).Append(m_DT_GHICHUUpdated ? string.Format(",DT_GHICHU = {0}", ":DT_GHICHU") : string.Empty);
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
				sbSQL.AppendFormat("[DT_MADT] = {0}", "@DT_MADT").AppendFormat(",[DT_MABUUCHINH] = {0}", "@DT_MABUUCHINH").AppendFormat(",[DT_SONHA_TENDUONG] = {0}", "@DT_SONHA_TENDUONG").AppendFormat(",[DT_QUAN_HUYEN] = {0}", "@DT_QUAN_HUYEN").AppendFormat(",[DT_TINH_THANHPHO] = {0}", "@DT_TINH_THANHPHO").AppendFormat(",[DT_TENNUOC] = {0}", "@DT_TENNUOC").AppendFormat(",[DT_DIENTHOAI] = {0}", "@DT_DIENTHOAI").AppendFormat(",[DT_FAX] = {0}", "@DT_FAX").AppendFormat(",[DT_EMAIL] = {0}", "@DT_EMAIL").AppendFormat(",[DT_GHICHU] = {0}", "@DT_GHICHU");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("DT_MADT = {0}", ":DT_MADT").AppendFormat(",DT_MABUUCHINH = {0}", ":DT_MABUUCHINH").AppendFormat(",DT_SONHA_TENDUONG = {0}", ":DT_SONHA_TENDUONG").AppendFormat(",DT_QUAN_HUYEN = {0}", ":DT_QUAN_HUYEN").AppendFormat(",DT_TINH_THANHPHO = {0}", ":DT_TINH_THANHPHO").AppendFormat(",DT_TENNUOC = {0}", ":DT_TENNUOC").AppendFormat(",DT_DIENTHOAI = {0}", ":DT_DIENTHOAI").AppendFormat(",DT_FAX = {0}", ":DT_FAX").AppendFormat(",DT_EMAIL = {0}", ":DT_EMAIL").AppendFormat(",DT_GHICHU = {0}", ":DT_GHICHU");
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
			return clsDAL.DeleteString("SDOITAC", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[DT_TENDT] = {0}", "@DT_TENDT");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("DT_TENDT = {0}", ":DT_TENDT");
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
			paramList.Add(clsDAL.CreateParameter("DT_TENDT", "WChar", clsDAL.ToDBParam(DT_TENDT, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("DT_MADT", "WChar", clsDAL.ToDBParam(DT_MADT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DT_MABUUCHINH", "WChar", clsDAL.ToDBParam(DT_MABUUCHINH, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DT_SONHA_TENDUONG", "WChar", clsDAL.ToDBParam(DT_SONHA_TENDUONG, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DT_QUAN_HUYEN", "WChar", clsDAL.ToDBParam(DT_QUAN_HUYEN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DT_TINH_THANHPHO", "WChar", clsDAL.ToDBParam(DT_TINH_THANHPHO, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DT_TENNUOC", "WChar", clsDAL.ToDBParam(DT_TENNUOC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DT_DIENTHOAI", "WChar", clsDAL.ToDBParam(DT_DIENTHOAI, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DT_FAX", "WChar", clsDAL.ToDBParam(DT_FAX, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DT_EMAIL", "WChar", clsDAL.ToDBParam(DT_EMAIL, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DT_GHICHU", "WChar", clsDAL.ToDBParam(DT_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DT_TENDT", "WChar", clsDAL.ToDBParam(DT_TENDT, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("DT_TENDT", "WChar", clsDAL.ToDBParam(DT_TENDT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DT_MADT", "WChar", clsDAL.ToDBParam(DT_MADT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DT_MABUUCHINH", "WChar", clsDAL.ToDBParam(DT_MABUUCHINH, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DT_SONHA_TENDUONG", "WChar", clsDAL.ToDBParam(DT_SONHA_TENDUONG, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DT_QUAN_HUYEN", "WChar", clsDAL.ToDBParam(DT_QUAN_HUYEN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DT_TINH_THANHPHO", "WChar", clsDAL.ToDBParam(DT_TINH_THANHPHO, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DT_TENNUOC", "WChar", clsDAL.ToDBParam(DT_TENNUOC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DT_DIENTHOAI", "WChar", clsDAL.ToDBParam(DT_DIENTHOAI, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DT_FAX", "WChar", clsDAL.ToDBParam(DT_FAX, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DT_EMAIL", "WChar", clsDAL.ToDBParam(DT_EMAIL, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DT_GHICHU", "WChar", clsDAL.ToDBParam(DT_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}