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
	/// Generated Class for Table : KD_DANHMUCHANG.
	/// </summary>
	public partial class KD_DANHMUCHANG : TableBase
	{
		public KD_DANHMUCHANG() : base(){}

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
		private string m_HAG_MA;
		/// <summary>
		/// HAG_MA.
		/// </summary>
		public string HAG_MA
		{
			get
			{
				return m_HAG_MA;
			}
			set
			{
				if ((this.m_HAG_MA != value))
				{
					this.SendPropertyChanging("HAG_MA");
					this.m_HAG_MA = value;
					this.SendPropertyChanged("HAG_MA");
				}
			}
		}

		private string m_HAG_TEN;
		private bool m_HAG_TENUpdated = false;
		/// <summary>
		/// HAG_TEN.
		/// </summary>
		public string HAG_TEN
		{
			get
			{
				return m_HAG_TEN;
			}
			set
			{
				if ((this.m_HAG_TEN != value))
				{
					this.SendPropertyChanging("HAG_TEN");
					this.m_HAG_TEN = value;
					this.SendPropertyChanged("HAG_TEN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HAG_TENUpdated = true;
				}
			}
		}

		private string m_HAG_TEN_EN;
		private bool m_HAG_TEN_ENUpdated = false;
		/// <summary>
		/// HAG_TEN_EN.
		/// </summary>
		public string HAG_TEN_EN
		{
			get
			{
				return m_HAG_TEN_EN;
			}
			set
			{
				if ((this.m_HAG_TEN_EN != value))
				{
					this.SendPropertyChanging("HAG_TEN_EN");
					this.m_HAG_TEN_EN = value;
					this.SendPropertyChanged("HAG_TEN_EN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HAG_TEN_ENUpdated = true;
				}
			}
		}

		private string m_HAG_DVT;
		private bool m_HAG_DVTUpdated = false;
		/// <summary>
		/// HAG_DVT.
		/// </summary>
		public string HAG_DVT
		{
			get
			{
				return m_HAG_DVT;
			}
			set
			{
				if ((this.m_HAG_DVT != value))
				{
					this.SendPropertyChanging("HAG_DVT");
					this.m_HAG_DVT = value;
					this.SendPropertyChanged("HAG_DVT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HAG_DVTUpdated = true;
				}
			}
		}

		private string m_HAG_HS;
		private bool m_HAG_HSUpdated = false;
		/// <summary>
		/// HAG_HS.
		/// </summary>
		public string HAG_HS
		{
			get
			{
				return m_HAG_HS;
			}
			set
			{
				if ((this.m_HAG_HS != value))
				{
					this.SendPropertyChanging("HAG_HS");
					this.m_HAG_HS = value;
					this.SendPropertyChanged("HAG_HS");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HAG_HSUpdated = true;
				}
			}
		}

		private decimal? m_HAG_DONGIAHOADON = 0;
		private bool m_HAG_DONGIAHOADONUpdated = false;
		/// <summary>
		/// HAG_DONGIAHOADON.
		/// </summary>
		public decimal? HAG_DONGIAHOADON
		{
			get
			{
				return m_HAG_DONGIAHOADON;
			}
			set
			{
				if ((this.m_HAG_DONGIAHOADON != value))
				{
					this.SendPropertyChanging("HAG_DONGIAHOADON");
					this.m_HAG_DONGIAHOADON = value;
					this.SendPropertyChanged("HAG_DONGIAHOADON");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HAG_DONGIAHOADONUpdated = true;
				}
			}
		}

		private string m_HAG_MABIEUTHUE_XNK;
		private bool m_HAG_MABIEUTHUE_XNKUpdated = false;
		/// <summary>
		/// HAG_MABIEUTHUE_XNK.
		/// </summary>
		public string HAG_MABIEUTHUE_XNK
		{
			get
			{
				return m_HAG_MABIEUTHUE_XNK;
			}
			set
			{
				if ((this.m_HAG_MABIEUTHUE_XNK != value))
				{
					this.SendPropertyChanging("HAG_MABIEUTHUE_XNK");
					this.m_HAG_MABIEUTHUE_XNK = value;
					this.SendPropertyChanged("HAG_MABIEUTHUE_XNK");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HAG_MABIEUTHUE_XNKUpdated = true;
				}
			}
		}

		private string m_HAG_MABIEUTHUE_TTDB;
		private bool m_HAG_MABIEUTHUE_TTDBUpdated = false;
		/// <summary>
		/// HAG_MABIEUTHUE_TTDB.
		/// </summary>
		public string HAG_MABIEUTHUE_TTDB
		{
			get
			{
				return m_HAG_MABIEUTHUE_TTDB;
			}
			set
			{
				if ((this.m_HAG_MABIEUTHUE_TTDB != value))
				{
					this.SendPropertyChanging("HAG_MABIEUTHUE_TTDB");
					this.m_HAG_MABIEUTHUE_TTDB = value;
					this.SendPropertyChanged("HAG_MABIEUTHUE_TTDB");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HAG_MABIEUTHUE_TTDBUpdated = true;
				}
			}
		}

		private string m_HAG_MABIEUTHUE_MT;
		private bool m_HAG_MABIEUTHUE_MTUpdated = false;
		/// <summary>
		/// HAG_MABIEUTHUE_MT.
		/// </summary>
		public string HAG_MABIEUTHUE_MT
		{
			get
			{
				return m_HAG_MABIEUTHUE_MT;
			}
			set
			{
				if ((this.m_HAG_MABIEUTHUE_MT != value))
				{
					this.SendPropertyChanging("HAG_MABIEUTHUE_MT");
					this.m_HAG_MABIEUTHUE_MT = value;
					this.SendPropertyChanged("HAG_MABIEUTHUE_MT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HAG_MABIEUTHUE_MTUpdated = true;
				}
			}
		}

		private string m_HAG_MABIEUTHUE_VAT;
		private bool m_HAG_MABIEUTHUE_VATUpdated = false;
		/// <summary>
		/// HAG_MABIEUTHUE_VAT.
		/// </summary>
		public string HAG_MABIEUTHUE_VAT
		{
			get
			{
				return m_HAG_MABIEUTHUE_VAT;
			}
			set
			{
				if ((this.m_HAG_MABIEUTHUE_VAT != value))
				{
					this.SendPropertyChanging("HAG_MABIEUTHUE_VAT");
					this.m_HAG_MABIEUTHUE_VAT = value;
					this.SendPropertyChanged("HAG_MABIEUTHUE_VAT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HAG_MABIEUTHUE_VATUpdated = true;
				}
			}
		}

		private string m_HAG_GHICHU;
		private bool m_HAG_GHICHUUpdated = false;
		/// <summary>
		/// HAG_GHICHU.
		/// </summary>
		public string HAG_GHICHU
		{
			get
			{
				return m_HAG_GHICHU;
			}
			set
			{
				if ((this.m_HAG_GHICHU != value))
				{
					this.SendPropertyChanging("HAG_GHICHU");
					this.m_HAG_GHICHU = value;
					this.SendPropertyChanged("HAG_GHICHU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HAG_GHICHUUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM KD_DANHMUCHANG " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[HAG_MA]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HAG_TEN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HAG_TEN_EN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HAG_DVT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HAG_HS]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HAG_DONGIAHOADON]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HAG_MABIEUTHUE_XNK]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HAG_MABIEUTHUE_TTDB]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HAG_MABIEUTHUE_MT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HAG_MABIEUTHUE_VAT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HAG_GHICHU]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("HAG_MA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HAG_TEN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HAG_TEN_EN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HAG_DVT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HAG_HS", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HAG_DONGIAHOADON", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HAG_MABIEUTHUE_XNK", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HAG_MABIEUTHUE_TTDB", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HAG_MABIEUTHUE_MT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HAG_MABIEUTHUE_VAT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HAG_GHICHU", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO KD_DANHMUCHANG ([HAG_MA], [HAG_TEN], [HAG_TEN_EN], [HAG_DVT], [HAG_HS], [HAG_DONGIAHOADON], [HAG_MABIEUTHUE_XNK], [HAG_MABIEUTHUE_TTDB], [HAG_MABIEUTHUE_MT], [HAG_MABIEUTHUE_VAT], [HAG_GHICHU]) VALUES(").Append("@HAG_MA").Append(",").Append("@HAG_TEN").Append(",").Append("@HAG_TEN_EN").Append(",").Append("@HAG_DVT").Append(",").Append("@HAG_HS").Append(",").Append("@HAG_DONGIAHOADON").Append(",").Append("@HAG_MABIEUTHUE_XNK").Append(",").Append("@HAG_MABIEUTHUE_TTDB").Append(",").Append("@HAG_MABIEUTHUE_MT").Append(",").Append("@HAG_MABIEUTHUE_VAT").Append(",").Append("@HAG_GHICHU").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO KD_DANHMUCHANG (HAG_MA, HAG_TEN, HAG_TEN_EN, HAG_DVT, HAG_HS, HAG_DONGIAHOADON, HAG_MABIEUTHUE_XNK, HAG_MABIEUTHUE_TTDB, HAG_MABIEUTHUE_MT, HAG_MABIEUTHUE_VAT, HAG_GHICHU) VALUES(").Append(":HAG_MA").Append(",").Append(":HAG_TEN").Append(",").Append(":HAG_TEN_EN").Append(",").Append(":HAG_DVT").Append(",").Append(":HAG_HS").Append(",").Append(":HAG_DONGIAHOADON").Append(",").Append(":HAG_MABIEUTHUE_XNK").Append(",").Append(":HAG_MABIEUTHUE_TTDB").Append(",").Append(":HAG_MABIEUTHUE_MT").Append(",").Append(":HAG_MABIEUTHUE_VAT").Append(",").Append(":HAG_GHICHU").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE KD_DANHMUCHANG SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_HAG_TENUpdated ? string.Format(",[HAG_TEN] = {0}", "@HAG_TEN") : string.Empty).Append(m_HAG_TEN_ENUpdated ? string.Format(",[HAG_TEN_EN] = {0}", "@HAG_TEN_EN") : string.Empty).Append(m_HAG_DVTUpdated ? string.Format(",[HAG_DVT] = {0}", "@HAG_DVT") : string.Empty).Append(m_HAG_HSUpdated ? string.Format(",[HAG_HS] = {0}", "@HAG_HS") : string.Empty).Append(m_HAG_DONGIAHOADONUpdated ? string.Format(",[HAG_DONGIAHOADON] = {0}", "@HAG_DONGIAHOADON") : string.Empty).Append(m_HAG_MABIEUTHUE_XNKUpdated ? string.Format(",[HAG_MABIEUTHUE_XNK] = {0}", "@HAG_MABIEUTHUE_XNK") : string.Empty).Append(m_HAG_MABIEUTHUE_TTDBUpdated ? string.Format(",[HAG_MABIEUTHUE_TTDB] = {0}", "@HAG_MABIEUTHUE_TTDB") : string.Empty).Append(m_HAG_MABIEUTHUE_MTUpdated ? string.Format(",[HAG_MABIEUTHUE_MT] = {0}", "@HAG_MABIEUTHUE_MT") : string.Empty).Append(m_HAG_MABIEUTHUE_VATUpdated ? string.Format(",[HAG_MABIEUTHUE_VAT] = {0}", "@HAG_MABIEUTHUE_VAT") : string.Empty).Append(m_HAG_GHICHUUpdated ? string.Format(",[HAG_GHICHU] = {0}", "@HAG_GHICHU") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_HAG_TENUpdated ? string.Format(",HAG_TEN = {0}", ":HAG_TEN") : string.Empty).Append(m_HAG_TEN_ENUpdated ? string.Format(",HAG_TEN_EN = {0}", ":HAG_TEN_EN") : string.Empty).Append(m_HAG_DVTUpdated ? string.Format(",HAG_DVT = {0}", ":HAG_DVT") : string.Empty).Append(m_HAG_HSUpdated ? string.Format(",HAG_HS = {0}", ":HAG_HS") : string.Empty).Append(m_HAG_DONGIAHOADONUpdated ? string.Format(",HAG_DONGIAHOADON = {0}", ":HAG_DONGIAHOADON") : string.Empty).Append(m_HAG_MABIEUTHUE_XNKUpdated ? string.Format(",HAG_MABIEUTHUE_XNK = {0}", ":HAG_MABIEUTHUE_XNK") : string.Empty).Append(m_HAG_MABIEUTHUE_TTDBUpdated ? string.Format(",HAG_MABIEUTHUE_TTDB = {0}", ":HAG_MABIEUTHUE_TTDB") : string.Empty).Append(m_HAG_MABIEUTHUE_MTUpdated ? string.Format(",HAG_MABIEUTHUE_MT = {0}", ":HAG_MABIEUTHUE_MT") : string.Empty).Append(m_HAG_MABIEUTHUE_VATUpdated ? string.Format(",HAG_MABIEUTHUE_VAT = {0}", ":HAG_MABIEUTHUE_VAT") : string.Empty).Append(m_HAG_GHICHUUpdated ? string.Format(",HAG_GHICHU = {0}", ":HAG_GHICHU") : string.Empty);
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
				sbSQL.AppendFormat("[HAG_TEN] = {0}", "@HAG_TEN").AppendFormat(",[HAG_TEN_EN] = {0}", "@HAG_TEN_EN").AppendFormat(",[HAG_DVT] = {0}", "@HAG_DVT").AppendFormat(",[HAG_HS] = {0}", "@HAG_HS").AppendFormat(",[HAG_DONGIAHOADON] = {0}", "@HAG_DONGIAHOADON").AppendFormat(",[HAG_MABIEUTHUE_XNK] = {0}", "@HAG_MABIEUTHUE_XNK").AppendFormat(",[HAG_MABIEUTHUE_TTDB] = {0}", "@HAG_MABIEUTHUE_TTDB").AppendFormat(",[HAG_MABIEUTHUE_MT] = {0}", "@HAG_MABIEUTHUE_MT").AppendFormat(",[HAG_MABIEUTHUE_VAT] = {0}", "@HAG_MABIEUTHUE_VAT").AppendFormat(",[HAG_GHICHU] = {0}", "@HAG_GHICHU");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("HAG_TEN = {0}", ":HAG_TEN").AppendFormat(",HAG_TEN_EN = {0}", ":HAG_TEN_EN").AppendFormat(",HAG_DVT = {0}", ":HAG_DVT").AppendFormat(",HAG_HS = {0}", ":HAG_HS").AppendFormat(",HAG_DONGIAHOADON = {0}", ":HAG_DONGIAHOADON").AppendFormat(",HAG_MABIEUTHUE_XNK = {0}", ":HAG_MABIEUTHUE_XNK").AppendFormat(",HAG_MABIEUTHUE_TTDB = {0}", ":HAG_MABIEUTHUE_TTDB").AppendFormat(",HAG_MABIEUTHUE_MT = {0}", ":HAG_MABIEUTHUE_MT").AppendFormat(",HAG_MABIEUTHUE_VAT = {0}", ":HAG_MABIEUTHUE_VAT").AppendFormat(",HAG_GHICHU = {0}", ":HAG_GHICHU");
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
			return clsDAL.DeleteString("KD_DANHMUCHANG", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[HAG_MA] = {0}", "@HAG_MA");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("HAG_MA = {0}", ":HAG_MA");
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
			paramList.Add(clsDAL.CreateParameter("HAG_MA", "WChar", clsDAL.ToDBParam(HAG_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("HAG_TEN", "WChar", clsDAL.ToDBParam(HAG_TEN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HAG_TEN_EN", "WChar", clsDAL.ToDBParam(HAG_TEN_EN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HAG_DVT", "WChar", clsDAL.ToDBParam(HAG_DVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HAG_HS", "WChar", clsDAL.ToDBParam(HAG_HS, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HAG_DONGIAHOADON", "Numeric", clsDAL.ToDBParam(HAG_DONGIAHOADON, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HAG_MABIEUTHUE_XNK", "WChar", clsDAL.ToDBParam(HAG_MABIEUTHUE_XNK, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HAG_MABIEUTHUE_TTDB", "WChar", clsDAL.ToDBParam(HAG_MABIEUTHUE_TTDB, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HAG_MABIEUTHUE_MT", "WChar", clsDAL.ToDBParam(HAG_MABIEUTHUE_MT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HAG_MABIEUTHUE_VAT", "WChar", clsDAL.ToDBParam(HAG_MABIEUTHUE_VAT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HAG_GHICHU", "WChar", clsDAL.ToDBParam(HAG_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HAG_MA", "WChar", clsDAL.ToDBParam(HAG_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("HAG_MA", "WChar", clsDAL.ToDBParam(HAG_MA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HAG_TEN", "WChar", clsDAL.ToDBParam(HAG_TEN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HAG_TEN_EN", "WChar", clsDAL.ToDBParam(HAG_TEN_EN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HAG_DVT", "WChar", clsDAL.ToDBParam(HAG_DVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HAG_HS", "WChar", clsDAL.ToDBParam(HAG_HS, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HAG_DONGIAHOADON", "Numeric", clsDAL.ToDBParam(HAG_DONGIAHOADON, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HAG_MABIEUTHUE_XNK", "WChar", clsDAL.ToDBParam(HAG_MABIEUTHUE_XNK, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HAG_MABIEUTHUE_TTDB", "WChar", clsDAL.ToDBParam(HAG_MABIEUTHUE_TTDB, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HAG_MABIEUTHUE_MT", "WChar", clsDAL.ToDBParam(HAG_MABIEUTHUE_MT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HAG_MABIEUTHUE_VAT", "WChar", clsDAL.ToDBParam(HAG_MABIEUTHUE_VAT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HAG_GHICHU", "WChar", clsDAL.ToDBParam(HAG_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}