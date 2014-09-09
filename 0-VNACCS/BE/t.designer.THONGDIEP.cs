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
	/// Generated Class for Table : THONGDIEP.
	/// </summary>
	public partial class THONGDIEP : TableBase
	{
		public THONGDIEP() : base(){}

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
		private int m_TD_ID;
		/// <summary>
		/// TD_ID.
		/// </summary>
		public int TD_ID
		{
			get
			{
				return m_TD_ID;
			}
			set
			{
				if ((this.m_TD_ID != value))
				{
					this.SendPropertyChanging("TD_ID");
					this.m_TD_ID = value;
					this.SendPropertyChanged("TD_ID");
				}
			}
		}

		private string m_TD_MANV;
		private bool m_TD_MANVUpdated = false;
		/// <summary>
		/// Mã nghiệp vụ (IDC,IDB,..).
		/// </summary>
		public string TD_MANV
		{
			get
			{
				return m_TD_MANV;
			}
			set
			{
				if ((this.m_TD_MANV != value))
				{
					this.SendPropertyChanging("TD_MANV");
					this.m_TD_MANV = value;
					this.SendPropertyChanged("TD_MANV");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TD_MANVUpdated = true;
				}
			}
		}

		private string m_TD_TENNV;
		private bool m_TD_TENNVUpdated = false;
		/// <summary>
		/// Tên nghiệp vu (Cho tên vào cho dễ xem).
		/// </summary>
		public string TD_TENNV
		{
			get
			{
				return m_TD_TENNV;
			}
			set
			{
				if ((this.m_TD_TENNV != value))
				{
					this.SendPropertyChanging("TD_TENNV");
					this.m_TD_TENNV = value;
					this.SendPropertyChanged("TD_TENNV");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TD_TENNVUpdated = true;
				}
			}
		}

		private string m_TD_MATD;
		private bool m_TD_MATDUpdated = false;
		/// <summary>
		/// Mã thông điệp đầu ra  (*VIDB,..), Mã thông điệp đầu vào.
		/// </summary>
		public string TD_MATD
		{
			get
			{
				return m_TD_MATD;
			}
			set
			{
				if ((this.m_TD_MATD != value))
				{
					this.SendPropertyChanging("TD_MATD");
					this.m_TD_MATD = value;
					this.SendPropertyChanged("TD_MATD");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TD_MATDUpdated = true;
				}
			}
		}

		private string m_TD_DINHDANG;
		private bool m_TD_DINHDANGUpdated = false;
		/// <summary>
		/// Định dạng (Q).
		/// </summary>
		public string TD_DINHDANG
		{
			get
			{
				return m_TD_DINHDANG;
			}
			set
			{
				if ((this.m_TD_DINHDANG != value))
				{
					this.SendPropertyChanging("TD_DINHDANG");
					this.m_TD_DINHDANG = value;
					this.SendPropertyChanged("TD_DINHDANG");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TD_DINHDANGUpdated = true;
				}
			}
		}

		private string m_TD_RETURNCODE;
		private bool m_TD_RETURNCODEUpdated = false;
		/// <summary>
		/// Mã kết quả xử lý (=returnCode: E3000-ICN-0000).
		/// </summary>
		public string TD_RETURNCODE
		{
			get
			{
				return m_TD_RETURNCODE;
			}
			set
			{
				if ((this.m_TD_RETURNCODE != value))
				{
					this.SendPropertyChanging("TD_RETURNCODE");
					this.m_TD_RETURNCODE = value;
					this.SendPropertyChanged("TD_RETURNCODE");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TD_RETURNCODEUpdated = true;
				}
			}
		}

		private string m_TD_TIEUDE;
		private bool m_TD_TIEUDEUpdated = false;
		/// <summary>
		/// Tiêu đề (là nội dung của message).
		/// </summary>
		public string TD_TIEUDE
		{
			get
			{
				return m_TD_TIEUDE;
			}
			set
			{
				if ((this.m_TD_TIEUDE != value))
				{
					this.SendPropertyChanging("TD_TIEUDE");
					this.m_TD_TIEUDE = value;
					this.SendPropertyChanged("TD_TIEUDE");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TD_TIEUDEUpdated = true;
				}
			}
		}

		private DateTime? m_TD_THOIGIAN;
		private bool m_TD_THOIGIANUpdated = false;
		/// <summary>
		/// Thời gian gửi/nhận.
		/// </summary>
		public DateTime? TD_THOIGIAN
		{
			get
			{
				return m_TD_THOIGIAN;
			}
			set
			{
				if ((this.m_TD_THOIGIAN != value))
				{
					this.SendPropertyChanging("TD_THOIGIAN");
					this.m_TD_THOIGIAN = value;
					this.SendPropertyChanged("TD_THOIGIAN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TD_THOIGIANUpdated = true;
				}
			}
		}

		private string m_TD_LOAITD;
		private bool m_TD_LOAITDUpdated = false;
		/// <summary>
		/// Loại thông điệp (R,..).
		/// </summary>
		public string TD_LOAITD
		{
			get
			{
				return m_TD_LOAITD;
			}
			set
			{
				if ((this.m_TD_LOAITD != value))
				{
					this.SendPropertyChanging("TD_LOAITD");
					this.m_TD_LOAITD = value;
					this.SendPropertyChanged("TD_LOAITD");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TD_LOAITDUpdated = true;
				}
			}
		}

		private string m_TD_COKETTHUC;
		private bool m_TD_COKETTHUCUpdated = false;
		/// <summary>
		/// Cờ kết thúc (E,...).
		/// </summary>
		public string TD_COKETTHUC
		{
			get
			{
				return m_TD_COKETTHUC;
			}
			set
			{
				if ((this.m_TD_COKETTHUC != value))
				{
					this.SendPropertyChanging("TD_COKETTHUC");
					this.m_TD_COKETTHUC = value;
					this.SendPropertyChanged("TD_COKETTHUC");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TD_COKETTHUCUpdated = true;
				}
			}
		}

		private string m_TD_MESSSAGECODE;
		private bool m_TD_MESSSAGECODEUpdated = false;
		/// <summary>
		/// MessageCode (là rút gọn của returnCode-mã thông điệp).
		/// </summary>
		public string TD_MESSSAGECODE
		{
			get
			{
				return m_TD_MESSSAGECODE;
			}
			set
			{
				if ((this.m_TD_MESSSAGECODE != value))
				{
					this.SendPropertyChanging("TD_MESSSAGECODE");
					this.m_TD_MESSSAGECODE = value;
					this.SendPropertyChanged("TD_MESSSAGECODE");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TD_MESSSAGECODEUpdated = true;
				}
			}
		}

		private string m_TD_TENCHITIEU;
		private bool m_TD_TENCHITIEUUpdated = false;
		/// <summary>
		/// TenChiTieu (Tên chỉ tiêu thông tin, ICN,...).
		/// </summary>
		public string TD_TENCHITIEU
		{
			get
			{
				return m_TD_TENCHITIEU;
			}
			set
			{
				if ((this.m_TD_TENCHITIEU != value))
				{
					this.SendPropertyChanging("TD_TENCHITIEU");
					this.m_TD_TENCHITIEU = value;
					this.SendPropertyChanged("TD_TENCHITIEU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TD_TENCHITIEUUpdated = true;
				}
			}
		}

		private string m_TD_MOTALOI;
		private bool m_TD_MOTALOIUpdated = false;
		/// <summary>
		/// MoTaLoi.
		/// </summary>
		public string TD_MOTALOI
		{
			get
			{
				return m_TD_MOTALOI;
			}
			set
			{
				if ((this.m_TD_MOTALOI != value))
				{
					this.SendPropertyChanging("TD_MOTALOI");
					this.m_TD_MOTALOI = value;
					this.SendPropertyChanged("TD_MOTALOI");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TD_MOTALOIUpdated = true;
				}
			}
		}

		private string m_TD_CACHKHACPHUC;
		private bool m_TD_CACHKHACPHUCUpdated = false;
		/// <summary>
		/// CachKhacPhuc.
		/// </summary>
		public string TD_CACHKHACPHUC
		{
			get
			{
				return m_TD_CACHKHACPHUC;
			}
			set
			{
				if ((this.m_TD_CACHKHACPHUC != value))
				{
					this.SendPropertyChanging("TD_CACHKHACPHUC");
					this.m_TD_CACHKHACPHUC = value;
					this.SendPropertyChanged("TD_CACHKHACPHUC");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TD_CACHKHACPHUCUpdated = true;
				}
			}
		}

		private int? m_TD_TRANGTHAI;
		private bool m_TD_TRANGTHAIUpdated = false;
		/// <summary>
		/// Trạng thái (Nhận 1, đang gửi 2, đã gửi 3, thùng rác 4).
		/// </summary>
		public int? TD_TRANGTHAI
		{
			get
			{
				return m_TD_TRANGTHAI;
			}
			set
			{
				if ((this.m_TD_TRANGTHAI != value))
				{
					this.SendPropertyChanging("TD_TRANGTHAI");
					this.m_TD_TRANGTHAI = value;
					this.SendPropertyChanged("TD_TRANGTHAI");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TD_TRANGTHAIUpdated = true;
				}
			}
		}

		private string m_TD_CONTENT;
		private bool m_TD_CONTENTUpdated = false;
		/// <summary>
		/// Chứa toàn bộ message giao dịch.
		/// </summary>
		public string TD_CONTENT
		{
			get
			{
				return m_TD_CONTENT;
			}
			set
			{
				if ((this.m_TD_CONTENT != value))
				{
					this.SendPropertyChanging("TD_CONTENT");
					this.m_TD_CONTENT = value;
					this.SendPropertyChanged("TD_CONTENT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TD_CONTENTUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM THONGDIEP " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[TD_ID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TD_MANV]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TD_TENNV]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TD_MATD]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TD_DINHDANG]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TD_RETURNCODE]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TD_TIEUDE]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TD_THOIGIAN]", ProType.DATETIME, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TD_LOAITD]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TD_COKETTHUC]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TD_MESSSAGECODE]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TD_TENCHITIEU]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TD_MOTALOI]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TD_CACHKHACPHUC]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TD_TRANGTHAI]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TD_CONTENT]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("TD_ID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TD_MANV", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TD_TENNV", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TD_MATD", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TD_DINHDANG", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TD_RETURNCODE", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TD_TIEUDE", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TD_THOIGIAN", ProType.DATETIME, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TD_LOAITD", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TD_COKETTHUC", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TD_MESSSAGECODE", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TD_TENCHITIEU", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TD_MOTALOI", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TD_CACHKHACPHUC", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TD_TRANGTHAI", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TD_CONTENT", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO THONGDIEP ([TD_ID], [TD_MANV], [TD_TENNV], [TD_MATD], [TD_DINHDANG], [TD_RETURNCODE], [TD_TIEUDE], [TD_THOIGIAN], [TD_LOAITD], [TD_COKETTHUC], [TD_MESSSAGECODE], [TD_TENCHITIEU], [TD_MOTALOI], [TD_CACHKHACPHUC], [TD_TRANGTHAI], [TD_CONTENT]) VALUES(").Append("@TD_ID").Append(",").Append("@TD_MANV").Append(",").Append("@TD_TENNV").Append(",").Append("@TD_MATD").Append(",").Append("@TD_DINHDANG").Append(",").Append("@TD_RETURNCODE").Append(",").Append("@TD_TIEUDE").Append(",").Append("@TD_THOIGIAN").Append(",").Append("@TD_LOAITD").Append(",").Append("@TD_COKETTHUC").Append(",").Append("@TD_MESSSAGECODE").Append(",").Append("@TD_TENCHITIEU").Append(",").Append("@TD_MOTALOI").Append(",").Append("@TD_CACHKHACPHUC").Append(",").Append("@TD_TRANGTHAI").Append(",").Append("@TD_CONTENT").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO THONGDIEP (TD_ID, TD_MANV, TD_TENNV, TD_MATD, TD_DINHDANG, TD_RETURNCODE, TD_TIEUDE, TD_THOIGIAN, TD_LOAITD, TD_COKETTHUC, TD_MESSSAGECODE, TD_TENCHITIEU, TD_MOTALOI, TD_CACHKHACPHUC, TD_TRANGTHAI, TD_CONTENT) VALUES(").Append(":TD_ID").Append(",").Append(":TD_MANV").Append(",").Append(":TD_TENNV").Append(",").Append(":TD_MATD").Append(",").Append(":TD_DINHDANG").Append(",").Append(":TD_RETURNCODE").Append(",").Append(":TD_TIEUDE").Append(",").Append(":TD_THOIGIAN").Append(",").Append(":TD_LOAITD").Append(",").Append(":TD_COKETTHUC").Append(",").Append(":TD_MESSSAGECODE").Append(",").Append(":TD_TENCHITIEU").Append(",").Append(":TD_MOTALOI").Append(",").Append(":TD_CACHKHACPHUC").Append(",").Append(":TD_TRANGTHAI").Append(",").Append(":TD_CONTENT").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE THONGDIEP SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_TD_MANVUpdated ? string.Format(",[TD_MANV] = {0}", "@TD_MANV") : string.Empty).Append(m_TD_TENNVUpdated ? string.Format(",[TD_TENNV] = {0}", "@TD_TENNV") : string.Empty).Append(m_TD_MATDUpdated ? string.Format(",[TD_MATD] = {0}", "@TD_MATD") : string.Empty).Append(m_TD_DINHDANGUpdated ? string.Format(",[TD_DINHDANG] = {0}", "@TD_DINHDANG") : string.Empty).Append(m_TD_RETURNCODEUpdated ? string.Format(",[TD_RETURNCODE] = {0}", "@TD_RETURNCODE") : string.Empty).Append(m_TD_TIEUDEUpdated ? string.Format(",[TD_TIEUDE] = {0}", "@TD_TIEUDE") : string.Empty).Append(m_TD_THOIGIANUpdated ? string.Format(",[TD_THOIGIAN] = {0}", "@TD_THOIGIAN") : string.Empty).Append(m_TD_LOAITDUpdated ? string.Format(",[TD_LOAITD] = {0}", "@TD_LOAITD") : string.Empty).Append(m_TD_COKETTHUCUpdated ? string.Format(",[TD_COKETTHUC] = {0}", "@TD_COKETTHUC") : string.Empty).Append(m_TD_MESSSAGECODEUpdated ? string.Format(",[TD_MESSSAGECODE] = {0}", "@TD_MESSSAGECODE") : string.Empty).Append(m_TD_TENCHITIEUUpdated ? string.Format(",[TD_TENCHITIEU] = {0}", "@TD_TENCHITIEU") : string.Empty).Append(m_TD_MOTALOIUpdated ? string.Format(",[TD_MOTALOI] = {0}", "@TD_MOTALOI") : string.Empty).Append(m_TD_CACHKHACPHUCUpdated ? string.Format(",[TD_CACHKHACPHUC] = {0}", "@TD_CACHKHACPHUC") : string.Empty).Append(m_TD_TRANGTHAIUpdated ? string.Format(",[TD_TRANGTHAI] = {0}", "@TD_TRANGTHAI") : string.Empty).Append(m_TD_CONTENTUpdated ? string.Format(",[TD_CONTENT] = {0}", "@TD_CONTENT") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_TD_MANVUpdated ? string.Format(",TD_MANV = {0}", ":TD_MANV") : string.Empty).Append(m_TD_TENNVUpdated ? string.Format(",TD_TENNV = {0}", ":TD_TENNV") : string.Empty).Append(m_TD_MATDUpdated ? string.Format(",TD_MATD = {0}", ":TD_MATD") : string.Empty).Append(m_TD_DINHDANGUpdated ? string.Format(",TD_DINHDANG = {0}", ":TD_DINHDANG") : string.Empty).Append(m_TD_RETURNCODEUpdated ? string.Format(",TD_RETURNCODE = {0}", ":TD_RETURNCODE") : string.Empty).Append(m_TD_TIEUDEUpdated ? string.Format(",TD_TIEUDE = {0}", ":TD_TIEUDE") : string.Empty).Append(m_TD_THOIGIANUpdated ? string.Format(",TD_THOIGIAN = {0}", ":TD_THOIGIAN") : string.Empty).Append(m_TD_LOAITDUpdated ? string.Format(",TD_LOAITD = {0}", ":TD_LOAITD") : string.Empty).Append(m_TD_COKETTHUCUpdated ? string.Format(",TD_COKETTHUC = {0}", ":TD_COKETTHUC") : string.Empty).Append(m_TD_MESSSAGECODEUpdated ? string.Format(",TD_MESSSAGECODE = {0}", ":TD_MESSSAGECODE") : string.Empty).Append(m_TD_TENCHITIEUUpdated ? string.Format(",TD_TENCHITIEU = {0}", ":TD_TENCHITIEU") : string.Empty).Append(m_TD_MOTALOIUpdated ? string.Format(",TD_MOTALOI = {0}", ":TD_MOTALOI") : string.Empty).Append(m_TD_CACHKHACPHUCUpdated ? string.Format(",TD_CACHKHACPHUC = {0}", ":TD_CACHKHACPHUC") : string.Empty).Append(m_TD_TRANGTHAIUpdated ? string.Format(",TD_TRANGTHAI = {0}", ":TD_TRANGTHAI") : string.Empty).Append(m_TD_CONTENTUpdated ? string.Format(",TD_CONTENT = {0}", ":TD_CONTENT") : string.Empty);
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
				sbSQL.AppendFormat("[TD_MANV] = {0}", "@TD_MANV").AppendFormat(",[TD_TENNV] = {0}", "@TD_TENNV").AppendFormat(",[TD_MATD] = {0}", "@TD_MATD").AppendFormat(",[TD_DINHDANG] = {0}", "@TD_DINHDANG").AppendFormat(",[TD_RETURNCODE] = {0}", "@TD_RETURNCODE").AppendFormat(",[TD_TIEUDE] = {0}", "@TD_TIEUDE").AppendFormat(",[TD_THOIGIAN] = {0}", "@TD_THOIGIAN").AppendFormat(",[TD_LOAITD] = {0}", "@TD_LOAITD").AppendFormat(",[TD_COKETTHUC] = {0}", "@TD_COKETTHUC").AppendFormat(",[TD_MESSSAGECODE] = {0}", "@TD_MESSSAGECODE").AppendFormat(",[TD_TENCHITIEU] = {0}", "@TD_TENCHITIEU").AppendFormat(",[TD_MOTALOI] = {0}", "@TD_MOTALOI").AppendFormat(",[TD_CACHKHACPHUC] = {0}", "@TD_CACHKHACPHUC").AppendFormat(",[TD_TRANGTHAI] = {0}", "@TD_TRANGTHAI").AppendFormat(",[TD_CONTENT] = {0}", "@TD_CONTENT");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("TD_MANV = {0}", ":TD_MANV").AppendFormat(",TD_TENNV = {0}", ":TD_TENNV").AppendFormat(",TD_MATD = {0}", ":TD_MATD").AppendFormat(",TD_DINHDANG = {0}", ":TD_DINHDANG").AppendFormat(",TD_RETURNCODE = {0}", ":TD_RETURNCODE").AppendFormat(",TD_TIEUDE = {0}", ":TD_TIEUDE").AppendFormat(",TD_THOIGIAN = {0}", ":TD_THOIGIAN").AppendFormat(",TD_LOAITD = {0}", ":TD_LOAITD").AppendFormat(",TD_COKETTHUC = {0}", ":TD_COKETTHUC").AppendFormat(",TD_MESSSAGECODE = {0}", ":TD_MESSSAGECODE").AppendFormat(",TD_TENCHITIEU = {0}", ":TD_TENCHITIEU").AppendFormat(",TD_MOTALOI = {0}", ":TD_MOTALOI").AppendFormat(",TD_CACHKHACPHUC = {0}", ":TD_CACHKHACPHUC").AppendFormat(",TD_TRANGTHAI = {0}", ":TD_TRANGTHAI").AppendFormat(",TD_CONTENT = {0}", ":TD_CONTENT");
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
			return clsDAL.DeleteString("THONGDIEP", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[TD_ID] = {0}", "@TD_ID");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("TD_ID = {0}", ":TD_ID");
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
			paramList.Add(clsDAL.CreateParameter("TD_ID", "Integer", clsDAL.ToDBParam(TD_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("TD_MANV", "WChar", clsDAL.ToDBParam(TD_MANV, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_TENNV", "WChar", clsDAL.ToDBParam(TD_TENNV, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_MATD", "WChar", clsDAL.ToDBParam(TD_MATD, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_DINHDANG", "WChar", clsDAL.ToDBParam(TD_DINHDANG, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_RETURNCODE", "WChar", clsDAL.ToDBParam(TD_RETURNCODE, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_TIEUDE", "WChar", clsDAL.ToDBParam(TD_TIEUDE, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_THOIGIAN", "Date", clsDAL.ToDBParam(TD_THOIGIAN, ProType.DATETIME, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_LOAITD", "WChar", clsDAL.ToDBParam(TD_LOAITD, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_COKETTHUC", "WChar", clsDAL.ToDBParam(TD_COKETTHUC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_MESSSAGECODE", "WChar", clsDAL.ToDBParam(TD_MESSSAGECODE, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_TENCHITIEU", "WChar", clsDAL.ToDBParam(TD_TENCHITIEU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_MOTALOI", "WChar", clsDAL.ToDBParam(TD_MOTALOI, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_CACHKHACPHUC", "WChar", clsDAL.ToDBParam(TD_CACHKHACPHUC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_TRANGTHAI", "Single", clsDAL.ToDBParam(TD_TRANGTHAI, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_CONTENT", "WChar", clsDAL.ToDBParam(TD_CONTENT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_ID", "Integer", clsDAL.ToDBParam(TD_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("TD_ID", "Integer", clsDAL.ToDBParam(TD_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_MANV", "WChar", clsDAL.ToDBParam(TD_MANV, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_TENNV", "WChar", clsDAL.ToDBParam(TD_TENNV, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_MATD", "WChar", clsDAL.ToDBParam(TD_MATD, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_DINHDANG", "WChar", clsDAL.ToDBParam(TD_DINHDANG, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_RETURNCODE", "WChar", clsDAL.ToDBParam(TD_RETURNCODE, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_TIEUDE", "WChar", clsDAL.ToDBParam(TD_TIEUDE, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_THOIGIAN", "Date", clsDAL.ToDBParam(TD_THOIGIAN, ProType.DATETIME, this.DataManagement), this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_LOAITD", "WChar", clsDAL.ToDBParam(TD_LOAITD, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_COKETTHUC", "WChar", clsDAL.ToDBParam(TD_COKETTHUC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_MESSSAGECODE", "WChar", clsDAL.ToDBParam(TD_MESSSAGECODE, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_TENCHITIEU", "WChar", clsDAL.ToDBParam(TD_TENCHITIEU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_MOTALOI", "WChar", clsDAL.ToDBParam(TD_MOTALOI, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_CACHKHACPHUC", "WChar", clsDAL.ToDBParam(TD_CACHKHACPHUC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_TRANGTHAI", "Single", clsDAL.ToDBParam(TD_TRANGTHAI, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TD_CONTENT", "WChar", clsDAL.ToDBParam(TD_CONTENT, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}