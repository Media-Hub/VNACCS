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
	/// Generated Class for Table : GC_HDGC_PK_NOIDUNG.
	/// </summary>
	public partial class GC_HDGC_PK_NOIDUNG : TableBase
	{
		public GC_HDGC_PK_NOIDUNG() : base(){}

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
		private int m_ND_HDID = 0;
		/// <summary>
		/// ND_HDID.
		/// </summary>
		public int ND_HDID
		{
			get
			{
				return m_ND_HDID;
			}
			set
			{
				if ((this.m_ND_HDID != value))
				{
					this.SendPropertyChanging("ND_HDID");
					this.m_ND_HDID = value;
					this.SendPropertyChanged("ND_HDID");
				}
			}
		}

		private int m_ND_PKID = 0;
		/// <summary>
		/// ND_PKID.
		/// </summary>
		public int ND_PKID
		{
			get
			{
				return m_ND_PKID;
			}
			set
			{
				if ((this.m_ND_PKID != value))
				{
					this.SendPropertyChanging("ND_PKID");
					this.m_ND_PKID = value;
					this.SendPropertyChanged("ND_PKID");
				}
			}
		}

		private int m_ND_ID;
		/// <summary>
		/// ND_ID.
		/// </summary>
		public int ND_ID
		{
			get
			{
				return m_ND_ID;
			}
			set
			{
				if ((this.m_ND_ID != value))
				{
					this.SendPropertyChanging("ND_ID");
					this.m_ND_ID = value;
					this.SendPropertyChanged("ND_ID");
				}
			}
		}

		private string m_ND_LOAIPK_MA;
		private bool m_ND_LOAIPK_MAUpdated = false;
		/// <summary>
		/// ND_LOAIPK_MA.
		/// </summary>
		public string ND_LOAIPK_MA
		{
			get
			{
				return m_ND_LOAIPK_MA;
			}
			set
			{
				if ((this.m_ND_LOAIPK_MA != value))
				{
					this.SendPropertyChanging("ND_LOAIPK_MA");
					this.m_ND_LOAIPK_MA = value;
					this.SendPropertyChanged("ND_LOAIPK_MA");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_ND_LOAIPK_MAUpdated = true;
				}
			}
		}

		private string m_ND_KIEU;
		private bool m_ND_KIEUUpdated = false;
		/// <summary>
		/// Kiểu phụ lục (Bổ sung: BS, Điều chỉnh: DC, Hợp đồng: HD).
		/// </summary>
		public string ND_KIEU
		{
			get
			{
				return m_ND_KIEU;
			}
			set
			{
				if ((this.m_ND_KIEU != value))
				{
					this.SendPropertyChanging("ND_KIEU");
					this.m_ND_KIEU = value;
					this.SendPropertyChanged("ND_KIEU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_ND_KIEUUpdated = true;
				}
			}
		}

		private string m_ND_DOITUONG;
		private bool m_ND_DOITUONGUpdated = false;
		/// <summary>
		/// Loại nội dung (Loại SP: L; Nguyên phụ liệu: N; Sản phẩm: S; Thiết bị: T; Hàng mẫu: H).
		/// </summary>
		public string ND_DOITUONG
		{
			get
			{
				return m_ND_DOITUONG;
			}
			set
			{
				if ((this.m_ND_DOITUONG != value))
				{
					this.SendPropertyChanging("ND_DOITUONG");
					this.m_ND_DOITUONG = value;
					this.SendPropertyChanged("ND_DOITUONG");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_ND_DOITUONGUpdated = true;
				}
			}
		}

		private string m_ND_GHICHU;
		private bool m_ND_GHICHUUpdated = false;
		/// <summary>
		/// ND_GHICHU.
		/// </summary>
		public string ND_GHICHU
		{
			get
			{
				return m_ND_GHICHU;
			}
			set
			{
				if ((this.m_ND_GHICHU != value))
				{
					this.SendPropertyChanging("ND_GHICHU");
					this.m_ND_GHICHU = value;
					this.SendPropertyChanged("ND_GHICHU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_ND_GHICHUUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM GC_HDGC_PK_NOIDUNG " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[ND_HDID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[ND_PKID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[ND_ID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[ND_LOAIPK_MA]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[ND_KIEU]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[ND_DOITUONG]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[ND_GHICHU]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("ND_HDID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("ND_PKID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("ND_ID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("ND_LOAIPK_MA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("ND_KIEU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("ND_DOITUONG", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("ND_GHICHU", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO GC_HDGC_PK_NOIDUNG ([ND_HDID], [ND_PKID], [ND_ID], [ND_LOAIPK_MA], [ND_KIEU], [ND_DOITUONG], [ND_GHICHU]) VALUES(").Append("@ND_HDID").Append(",").Append("@ND_PKID").Append(",").Append("@ND_ID").Append(",").Append("@ND_LOAIPK_MA").Append(",").Append("@ND_KIEU").Append(",").Append("@ND_DOITUONG").Append(",").Append("@ND_GHICHU").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO GC_HDGC_PK_NOIDUNG (ND_HDID, ND_PKID, ND_ID, ND_LOAIPK_MA, ND_KIEU, ND_DOITUONG, ND_GHICHU) VALUES(").Append(":ND_HDID").Append(",").Append(":ND_PKID").Append(",").Append(":ND_ID").Append(",").Append(":ND_LOAIPK_MA").Append(",").Append(":ND_KIEU").Append(",").Append(":ND_DOITUONG").Append(",").Append(":ND_GHICHU").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE GC_HDGC_PK_NOIDUNG SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_ND_LOAIPK_MAUpdated ? string.Format(",[ND_LOAIPK_MA] = {0}", "@ND_LOAIPK_MA") : string.Empty).Append(m_ND_KIEUUpdated ? string.Format(",[ND_KIEU] = {0}", "@ND_KIEU") : string.Empty).Append(m_ND_DOITUONGUpdated ? string.Format(",[ND_DOITUONG] = {0}", "@ND_DOITUONG") : string.Empty).Append(m_ND_GHICHUUpdated ? string.Format(",[ND_GHICHU] = {0}", "@ND_GHICHU") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_ND_LOAIPK_MAUpdated ? string.Format(",ND_LOAIPK_MA = {0}", ":ND_LOAIPK_MA") : string.Empty).Append(m_ND_KIEUUpdated ? string.Format(",ND_KIEU = {0}", ":ND_KIEU") : string.Empty).Append(m_ND_DOITUONGUpdated ? string.Format(",ND_DOITUONG = {0}", ":ND_DOITUONG") : string.Empty).Append(m_ND_GHICHUUpdated ? string.Format(",ND_GHICHU = {0}", ":ND_GHICHU") : string.Empty);
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
				sbSQL.AppendFormat("[ND_LOAIPK_MA] = {0}", "@ND_LOAIPK_MA").AppendFormat(",[ND_KIEU] = {0}", "@ND_KIEU").AppendFormat(",[ND_DOITUONG] = {0}", "@ND_DOITUONG").AppendFormat(",[ND_GHICHU] = {0}", "@ND_GHICHU");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("ND_LOAIPK_MA = {0}", ":ND_LOAIPK_MA").AppendFormat(",ND_KIEU = {0}", ":ND_KIEU").AppendFormat(",ND_DOITUONG = {0}", ":ND_DOITUONG").AppendFormat(",ND_GHICHU = {0}", ":ND_GHICHU");
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
			return clsDAL.DeleteString("GC_HDGC_PK_NOIDUNG", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[ND_HDID] = {0}", "@ND_HDID").AppendFormat(" AND [ND_PKID] = {0}", "@ND_PKID").AppendFormat(" AND [ND_ID] = {0}", "@ND_ID");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("ND_HDID = {0}", ":ND_HDID").AppendFormat(" AND ND_PKID = {0}", ":ND_PKID").AppendFormat(" AND ND_ID = {0}", ":ND_ID");
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
			paramList.Add(clsDAL.CreateParameter("ND_HDID", "Integer", clsDAL.ToDBParam(ND_HDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("ND_PKID", "Integer", clsDAL.ToDBParam(ND_PKID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("ND_ID", "Integer", clsDAL.ToDBParam(ND_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("ND_LOAIPK_MA", "WChar", clsDAL.ToDBParam(ND_LOAIPK_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("ND_KIEU", "WChar", clsDAL.ToDBParam(ND_KIEU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("ND_DOITUONG", "WChar", clsDAL.ToDBParam(ND_DOITUONG, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("ND_GHICHU", "WChar", clsDAL.ToDBParam(ND_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("ND_HDID", "Integer", clsDAL.ToDBParam(ND_HDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("ND_PKID", "Integer", clsDAL.ToDBParam(ND_PKID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("ND_ID", "Integer", clsDAL.ToDBParam(ND_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("ND_HDID", "Integer", clsDAL.ToDBParam(ND_HDID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("ND_PKID", "Integer", clsDAL.ToDBParam(ND_PKID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("ND_ID", "Integer", clsDAL.ToDBParam(ND_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("ND_LOAIPK_MA", "WChar", clsDAL.ToDBParam(ND_LOAIPK_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("ND_KIEU", "WChar", clsDAL.ToDBParam(ND_KIEU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("ND_DOITUONG", "WChar", clsDAL.ToDBParam(ND_DOITUONG, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("ND_GHICHU", "WChar", clsDAL.ToDBParam(ND_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}