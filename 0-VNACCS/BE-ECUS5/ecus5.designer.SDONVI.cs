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
	/// Generated Class for Table : SDONVI.
	/// </summary>
	public partial class SDONVI : TableBase
	{
		public SDONVI() : base(){}

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
		private string m_Ma_DV;
		/// <summary>
		/// Ma_DV.
		/// </summary>
		public string Ma_DV
		{
			get
			{
				return m_Ma_DV;
			}
			set
			{
				if ((this.m_Ma_DV != value))
				{
					this.SendPropertyChanging("Ma_DV");
					this.m_Ma_DV = value;
					this.SendPropertyChanged("Ma_DV");
				}
			}
		}

		private string m_Ten_DV;
		private bool m_Ten_DVUpdated = false;
		/// <summary>
		/// Ten_DV.
		/// </summary>
		public string Ten_DV
		{
			get
			{
				return m_Ten_DV;
			}
			set
			{
				if ((this.m_Ten_DV != value))
				{
					this.SendPropertyChanging("Ten_DV");
					this.m_Ten_DV = value;
					this.SendPropertyChanged("Ten_DV");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_Ten_DVUpdated = true;
				}
			}
		}

		private string m_Ten_GD;
		private bool m_Ten_GDUpdated = false;
		/// <summary>
		/// Ten_GD.
		/// </summary>
		public string Ten_GD
		{
			get
			{
				return m_Ten_GD;
			}
			set
			{
				if ((this.m_Ten_GD != value))
				{
					this.SendPropertyChanging("Ten_GD");
					this.m_Ten_GD = value;
					this.SendPropertyChanged("Ten_GD");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_Ten_GDUpdated = true;
				}
			}
		}

		private string m_MaLHDN;
		private bool m_MaLHDNUpdated = false;
		/// <summary>
		/// MaLHDN.
		/// </summary>
		public string MaLHDN
		{
			get
			{
				return m_MaLHDN;
			}
			set
			{
				if ((this.m_MaLHDN != value))
				{
					this.SendPropertyChanging("MaLHDN");
					this.m_MaLHDN = value;
					this.SendPropertyChanged("MaLHDN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_MaLHDNUpdated = true;
				}
			}
		}

		private string m_TWDP;
		private bool m_TWDPUpdated = false;
		/// <summary>
		/// TWDP.
		/// </summary>
		public string TWDP
		{
			get
			{
				return m_TWDP;
			}
			set
			{
				if ((this.m_TWDP != value))
				{
					this.SendPropertyChanging("TWDP");
					this.m_TWDP = value;
					this.SendPropertyChanged("TWDP");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TWDPUpdated = true;
				}
			}
		}

		private string m_NoiQuanLy;
		private bool m_NoiQuanLyUpdated = false;
		/// <summary>
		/// NoiQuanLy.
		/// </summary>
		public string NoiQuanLy
		{
			get
			{
				return m_NoiQuanLy;
			}
			set
			{
				if ((this.m_NoiQuanLy != value))
				{
					this.SendPropertyChanging("NoiQuanLy");
					this.m_NoiQuanLy = value;
					this.SendPropertyChanged("NoiQuanLy");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NoiQuanLyUpdated = true;
				}
			}
		}

		private string m_SoGPKD;
		private bool m_SoGPKDUpdated = false;
		/// <summary>
		/// SoGPKD.
		/// </summary>
		public string SoGPKD
		{
			get
			{
				return m_SoGPKD;
			}
			set
			{
				if ((this.m_SoGPKD != value))
				{
					this.SendPropertyChanging("SoGPKD");
					this.m_SoGPKD = value;
					this.SendPropertyChanged("SoGPKD");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_SoGPKDUpdated = true;
				}
			}
		}

		private DateTime? m_NgayCapGPKD;
		private bool m_NgayCapGPKDUpdated = false;
		/// <summary>
		/// NgayCapGPKD.
		/// </summary>
		public DateTime? NgayCapGPKD
		{
			get
			{
				return m_NgayCapGPKD;
			}
			set
			{
				if ((this.m_NgayCapGPKD != value))
				{
					this.SendPropertyChanging("NgayCapGPKD");
					this.m_NgayCapGPKD = value;
					this.SendPropertyChanged("NgayCapGPKD");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NgayCapGPKDUpdated = true;
				}
			}
		}

		private string m_DiaChi;
		private bool m_DiaChiUpdated = false;
		/// <summary>
		/// DiaChi.
		/// </summary>
		public string DiaChi
		{
			get
			{
				return m_DiaChi;
			}
			set
			{
				if ((this.m_DiaChi != value))
				{
					this.SendPropertyChanging("DiaChi");
					this.m_DiaChi = value;
					this.SendPropertyChanged("DiaChi");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DiaChiUpdated = true;
				}
			}
		}

		private string m_DienThoai;
		private bool m_DienThoaiUpdated = false;
		/// <summary>
		/// DienThoai.
		/// </summary>
		public string DienThoai
		{
			get
			{
				return m_DienThoai;
			}
			set
			{
				if ((this.m_DienThoai != value))
				{
					this.SendPropertyChanging("DienThoai");
					this.m_DienThoai = value;
					this.SendPropertyChanged("DienThoai");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DienThoaiUpdated = true;
				}
			}
		}

		private string m_Fax;
		private bool m_FaxUpdated = false;
		/// <summary>
		/// Fax.
		/// </summary>
		public string Fax
		{
			get
			{
				return m_Fax;
			}
			set
			{
				if ((this.m_Fax != value))
				{
					this.SendPropertyChanging("Fax");
					this.m_Fax = value;
					this.SendPropertyChanged("Fax");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_FaxUpdated = true;
				}
			}
		}

		private string m_GiamDoc;
		private bool m_GiamDocUpdated = false;
		/// <summary>
		/// GiamDoc.
		/// </summary>
		public string GiamDoc
		{
			get
			{
				return m_GiamDoc;
			}
			set
			{
				if ((this.m_GiamDoc != value))
				{
					this.SendPropertyChanging("GiamDoc");
					this.m_GiamDoc = value;
					this.SendPropertyChanged("GiamDoc");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_GiamDocUpdated = true;
				}
			}
		}

		private string m_KeToan;
		private bool m_KeToanUpdated = false;
		/// <summary>
		/// KeToan.
		/// </summary>
		public string KeToan
		{
			get
			{
				return m_KeToan;
			}
			set
			{
				if ((this.m_KeToan != value))
				{
					this.SendPropertyChanging("KeToan");
					this.m_KeToan = value;
					this.SendPropertyChanged("KeToan");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_KeToanUpdated = true;
				}
			}
		}

		private string m_TaiKhoan;
		private bool m_TaiKhoanUpdated = false;
		/// <summary>
		/// TaiKhoan.
		/// </summary>
		public string TaiKhoan
		{
			get
			{
				return m_TaiKhoan;
			}
			set
			{
				if ((this.m_TaiKhoan != value))
				{
					this.SendPropertyChanging("TaiKhoan");
					this.m_TaiKhoan = value;
					this.SendPropertyChanged("TaiKhoan");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TaiKhoanUpdated = true;
				}
			}
		}

		private string m_PPT_GTGT;
		private bool m_PPT_GTGTUpdated = false;
		/// <summary>
		/// PPT_GTGT.
		/// </summary>
		public string PPT_GTGT
		{
			get
			{
				return m_PPT_GTGT;
			}
			set
			{
				if ((this.m_PPT_GTGT != value))
				{
					this.SendPropertyChanging("PPT_GTGT");
					this.m_PPT_GTGT = value;
					this.SendPropertyChanged("PPT_GTGT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_PPT_GTGTUpdated = true;
				}
			}
		}

		private string m_Nhom_CQ;
		private bool m_Nhom_CQUpdated = false;
		/// <summary>
		/// Nhom_CQ.
		/// </summary>
		public string Nhom_CQ
		{
			get
			{
				return m_Nhom_CQ;
			}
			set
			{
				if ((this.m_Nhom_CQ != value))
				{
					this.SendPropertyChanging("Nhom_CQ");
					this.m_Nhom_CQ = value;
					this.SendPropertyChanged("Nhom_CQ");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_Nhom_CQUpdated = true;
				}
			}
		}

		private string m_TT_DV;
		private bool m_TT_DVUpdated = false;
		/// <summary>
		/// TT_DV.
		/// </summary>
		public string TT_DV
		{
			get
			{
				return m_TT_DV;
			}
			set
			{
				if ((this.m_TT_DV != value))
				{
					this.SendPropertyChanging("TT_DV");
					this.m_TT_DV = value;
					this.SendPropertyChanged("TT_DV");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TT_DVUpdated = true;
				}
			}
		}

		private int? m_So_TT;
		private bool m_So_TTUpdated = false;
		/// <summary>
		/// So_TT.
		/// </summary>
		public int? So_TT
		{
			get
			{
				return m_So_TT;
			}
			set
			{
				if ((this.m_So_TT != value))
				{
					this.SendPropertyChanging("So_TT");
					this.m_So_TT = value;
					this.SendPropertyChanged("So_TT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_So_TTUpdated = true;
				}
			}
		}

		private string m_MLNSChuong;
		private bool m_MLNSChuongUpdated = false;
		/// <summary>
		/// MLNSChuong.
		/// </summary>
		public string MLNSChuong
		{
			get
			{
				return m_MLNSChuong;
			}
			set
			{
				if ((this.m_MLNSChuong != value))
				{
					this.SendPropertyChanging("MLNSChuong");
					this.m_MLNSChuong = value;
					this.SendPropertyChanged("MLNSChuong");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_MLNSChuongUpdated = true;
				}
			}
		}

		private string m_sogc;
		private bool m_sogcUpdated = false;
		/// <summary>
		/// sogc.
		/// </summary>
		public string sogc
		{
			get
			{
				return m_sogc;
			}
			set
			{
				if ((this.m_sogc != value))
				{
					this.SendPropertyChanging("sogc");
					this.m_sogc = value;
					this.SendPropertyChanged("sogc");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_sogcUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SDONVI " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(clsDAL.SelectField("Ma_DV", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("Ten_DV", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("Ten_GD", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("MaLHDN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TWDP", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NoiQuanLy", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("SoGPKD", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NgayCapGPKD", ProType.DATETIME, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DiaChi", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DienThoai", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("Fax", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("GiamDoc", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("KeToan", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TaiKhoan", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("PPT_GTGT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("Nhom_CQ", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TT_DV", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("So_TT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("MLNSChuong", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("sogc", ProType.OTHER, this.DataManagement));
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
			sbSQL.Append("INSERT INTO SDONVI (Ma_DV, Ten_DV, Ten_GD, MaLHDN, TWDP, NoiQuanLy, SoGPKD, NgayCapGPKD, DiaChi, DienThoai, Fax, GiamDoc, KeToan, TaiKhoan, PPT_GTGT, Nhom_CQ, TT_DV, So_TT, MLNSChuong, sogc) VALUES(").Append(clsDAL.IsDBNULL(Ma_DV, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(Ten_DV, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(Ten_GD, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(MaLHDN, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TWDP, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(NoiQuanLy, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(SoGPKD, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(NgayCapGPKD, ProType.DATETIME, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(DiaChi, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(DienThoai, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(Fax, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(GiamDoc, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(KeToan, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TaiKhoan, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(PPT_GTGT, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(Nhom_CQ, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(TT_DV, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(So_TT, ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(MLNSChuong, ProType.STRING, this.DataManagement)).Append(",").Append(clsDAL.IsDBNULL(sogc, ProType.STRING, this.DataManagement)).Append(")");
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SDONVI SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append(m_Ten_DVUpdated ? string.Format(",Ten_DV = {0}", clsDAL.IsDBNULL(Ten_DV, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_Ten_GDUpdated ? string.Format(",Ten_GD = {0}", clsDAL.IsDBNULL(Ten_GD, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_MaLHDNUpdated ? string.Format(",MaLHDN = {0}", clsDAL.IsDBNULL(MaLHDN, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TWDPUpdated ? string.Format(",TWDP = {0}", clsDAL.IsDBNULL(TWDP, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_NoiQuanLyUpdated ? string.Format(",NoiQuanLy = {0}", clsDAL.IsDBNULL(NoiQuanLy, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_SoGPKDUpdated ? string.Format(",SoGPKD = {0}", clsDAL.IsDBNULL(SoGPKD, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_NgayCapGPKDUpdated ? string.Format(",NgayCapGPKD = {0}", clsDAL.IsDBNULL(NgayCapGPKD, ProType.DATETIME, this.DataManagement)) : string.Empty).Append(m_DiaChiUpdated ? string.Format(",DiaChi = {0}", clsDAL.IsDBNULL(DiaChi, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_DienThoaiUpdated ? string.Format(",DienThoai = {0}", clsDAL.IsDBNULL(DienThoai, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_FaxUpdated ? string.Format(",Fax = {0}", clsDAL.IsDBNULL(Fax, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_GiamDocUpdated ? string.Format(",GiamDoc = {0}", clsDAL.IsDBNULL(GiamDoc, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_KeToanUpdated ? string.Format(",KeToan = {0}", clsDAL.IsDBNULL(KeToan, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TaiKhoanUpdated ? string.Format(",TaiKhoan = {0}", clsDAL.IsDBNULL(TaiKhoan, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_PPT_GTGTUpdated ? string.Format(",PPT_GTGT = {0}", clsDAL.IsDBNULL(PPT_GTGT, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_Nhom_CQUpdated ? string.Format(",Nhom_CQ = {0}", clsDAL.IsDBNULL(Nhom_CQ, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_TT_DVUpdated ? string.Format(",TT_DV = {0}", clsDAL.IsDBNULL(TT_DV, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_So_TTUpdated ? string.Format(",So_TT = {0}", clsDAL.IsDBNULL(So_TT, ProType.OTHER, this.DataManagement)) : string.Empty).Append(m_MLNSChuongUpdated ? string.Format(",MLNSChuong = {0}", clsDAL.IsDBNULL(MLNSChuong, ProType.STRING, this.DataManagement)) : string.Empty).Append(m_sogcUpdated ? string.Format(",sogc = {0}", clsDAL.IsDBNULL(sogc, ProType.STRING, this.DataManagement)) : string.Empty);
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
			sbSQL.AppendFormat("Ten_DV = {0}", clsDAL.IsDBNULL(Ten_DV, ProType.STRING, this.DataManagement)).AppendFormat(",Ten_GD = {0}", clsDAL.IsDBNULL(Ten_GD, ProType.STRING, this.DataManagement)).AppendFormat(",MaLHDN = {0}", clsDAL.IsDBNULL(MaLHDN, ProType.STRING, this.DataManagement)).AppendFormat(",TWDP = {0}", clsDAL.IsDBNULL(TWDP, ProType.STRING, this.DataManagement)).AppendFormat(",NoiQuanLy = {0}", clsDAL.IsDBNULL(NoiQuanLy, ProType.STRING, this.DataManagement)).AppendFormat(",SoGPKD = {0}", clsDAL.IsDBNULL(SoGPKD, ProType.STRING, this.DataManagement)).AppendFormat(",NgayCapGPKD = {0}", clsDAL.IsDBNULL(NgayCapGPKD, ProType.DATETIME, this.DataManagement)).AppendFormat(",DiaChi = {0}", clsDAL.IsDBNULL(DiaChi, ProType.STRING, this.DataManagement)).AppendFormat(",DienThoai = {0}", clsDAL.IsDBNULL(DienThoai, ProType.STRING, this.DataManagement)).AppendFormat(",Fax = {0}", clsDAL.IsDBNULL(Fax, ProType.STRING, this.DataManagement)).AppendFormat(",GiamDoc = {0}", clsDAL.IsDBNULL(GiamDoc, ProType.STRING, this.DataManagement)).AppendFormat(",KeToan = {0}", clsDAL.IsDBNULL(KeToan, ProType.STRING, this.DataManagement)).AppendFormat(",TaiKhoan = {0}", clsDAL.IsDBNULL(TaiKhoan, ProType.STRING, this.DataManagement)).AppendFormat(",PPT_GTGT = {0}", clsDAL.IsDBNULL(PPT_GTGT, ProType.STRING, this.DataManagement)).AppendFormat(",Nhom_CQ = {0}", clsDAL.IsDBNULL(Nhom_CQ, ProType.STRING, this.DataManagement)).AppendFormat(",TT_DV = {0}", clsDAL.IsDBNULL(TT_DV, ProType.STRING, this.DataManagement)).AppendFormat(",So_TT = {0}", clsDAL.IsDBNULL(So_TT, ProType.OTHER, this.DataManagement)).AppendFormat(",MLNSChuong = {0}", clsDAL.IsDBNULL(MLNSChuong, ProType.STRING, this.DataManagement)).AppendFormat(",sogc = {0}", clsDAL.IsDBNULL(sogc, ProType.STRING, this.DataManagement));
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
			return clsDAL.DeleteString("SDONVI", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
			sbSQL.AppendFormat("Ma_DV = {0}", clsDAL.IsDBNULL(Ma_DV, ProType.STRING, this.DataManagement));
			return sbSQL.ToString();
		}
	}
}