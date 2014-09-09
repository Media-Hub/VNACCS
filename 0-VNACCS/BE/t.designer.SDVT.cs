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
	/// Generated Class for Table : SDVT.
	/// </summary>
	public partial class SDVT : TableBase
	{
		public SDVT() : base(){}

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
		private string m_DVT_MADVT;
		/// <summary>
		/// DVT_MADVT.
		/// </summary>
		public string DVT_MADVT
		{
			get
			{
				return m_DVT_MADVT;
			}
			set
			{
				if ((this.m_DVT_MADVT != value))
				{
					this.SendPropertyChanging("DVT_MADVT");
					this.m_DVT_MADVT = value;
					this.SendPropertyChanged("DVT_MADVT");
				}
			}
		}

		private string m_DVT_TENDVT;
		private bool m_DVT_TENDVTUpdated = false;
		/// <summary>
		/// DVT_TENDVT.
		/// </summary>
		public string DVT_TENDVT
		{
			get
			{
				return m_DVT_TENDVT;
			}
			set
			{
				if ((this.m_DVT_TENDVT != value))
				{
					this.SendPropertyChanging("DVT_TENDVT");
					this.m_DVT_TENDVT = value;
					this.SendPropertyChanged("DVT_TENDVT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DVT_TENDVTUpdated = true;
				}
			}
		}

		private string m_DVT_MADVT_V4;
		private bool m_DVT_MADVT_V4Updated = false;
		/// <summary>
		/// DVT_MADVT_V4.
		/// </summary>
		public string DVT_MADVT_V4
		{
			get
			{
				return m_DVT_MADVT_V4;
			}
			set
			{
				if ((this.m_DVT_MADVT_V4 != value))
				{
					this.SendPropertyChanging("DVT_MADVT_V4");
					this.m_DVT_MADVT_V4 = value;
					this.SendPropertyChanged("DVT_MADVT_V4");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DVT_MADVT_V4Updated = true;
				}
			}
		}

		private string m_DVT_TENDVT_V4;
		private bool m_DVT_TENDVT_V4Updated = false;
		/// <summary>
		/// DVT_TENDVT_V4.
		/// </summary>
		public string DVT_TENDVT_V4
		{
			get
			{
				return m_DVT_TENDVT_V4;
			}
			set
			{
				if ((this.m_DVT_TENDVT_V4 != value))
				{
					this.SendPropertyChanging("DVT_TENDVT_V4");
					this.m_DVT_TENDVT_V4 = value;
					this.SendPropertyChanged("DVT_TENDVT_V4");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DVT_TENDVT_V4Updated = true;
				}
			}
		}

		private string m_DVT_GHICHU;
		private bool m_DVT_GHICHUUpdated = false;
		/// <summary>
		/// DVT_GHICHU.
		/// </summary>
		public string DVT_GHICHU
		{
			get
			{
				return m_DVT_GHICHU;
			}
			set
			{
				if ((this.m_DVT_GHICHU != value))
				{
					this.SendPropertyChanging("DVT_GHICHU");
					this.m_DVT_GHICHU = value;
					this.SendPropertyChanged("DVT_GHICHU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DVT_GHICHUUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SDVT " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[DVT_MADVT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DVT_TENDVT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DVT_MADVT_V4]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DVT_TENDVT_V4]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DVT_GHICHU]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("DVT_MADVT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DVT_TENDVT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DVT_MADVT_V4", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DVT_TENDVT_V4", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DVT_GHICHU", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO SDVT ([DVT_MADVT], [DVT_TENDVT], [DVT_MADVT_V4], [DVT_TENDVT_V4], [DVT_GHICHU]) VALUES(").Append("@DVT_MADVT").Append(",").Append("@DVT_TENDVT").Append(",").Append("@DVT_MADVT_V4").Append(",").Append("@DVT_TENDVT_V4").Append(",").Append("@DVT_GHICHU").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO SDVT (DVT_MADVT, DVT_TENDVT, DVT_MADVT_V4, DVT_TENDVT_V4, DVT_GHICHU) VALUES(").Append(":DVT_MADVT").Append(",").Append(":DVT_TENDVT").Append(",").Append(":DVT_MADVT_V4").Append(",").Append(":DVT_TENDVT_V4").Append(",").Append(":DVT_GHICHU").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SDVT SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_DVT_TENDVTUpdated ? string.Format(",[DVT_TENDVT] = {0}", "@DVT_TENDVT") : string.Empty).Append(m_DVT_MADVT_V4Updated ? string.Format(",[DVT_MADVT_V4] = {0}", "@DVT_MADVT_V4") : string.Empty).Append(m_DVT_TENDVT_V4Updated ? string.Format(",[DVT_TENDVT_V4] = {0}", "@DVT_TENDVT_V4") : string.Empty).Append(m_DVT_GHICHUUpdated ? string.Format(",[DVT_GHICHU] = {0}", "@DVT_GHICHU") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_DVT_TENDVTUpdated ? string.Format(",DVT_TENDVT = {0}", ":DVT_TENDVT") : string.Empty).Append(m_DVT_MADVT_V4Updated ? string.Format(",DVT_MADVT_V4 = {0}", ":DVT_MADVT_V4") : string.Empty).Append(m_DVT_TENDVT_V4Updated ? string.Format(",DVT_TENDVT_V4 = {0}", ":DVT_TENDVT_V4") : string.Empty).Append(m_DVT_GHICHUUpdated ? string.Format(",DVT_GHICHU = {0}", ":DVT_GHICHU") : string.Empty);
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
				sbSQL.AppendFormat("[DVT_TENDVT] = {0}", "@DVT_TENDVT").AppendFormat(",[DVT_MADVT_V4] = {0}", "@DVT_MADVT_V4").AppendFormat(",[DVT_TENDVT_V4] = {0}", "@DVT_TENDVT_V4").AppendFormat(",[DVT_GHICHU] = {0}", "@DVT_GHICHU");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("DVT_TENDVT = {0}", ":DVT_TENDVT").AppendFormat(",DVT_MADVT_V4 = {0}", ":DVT_MADVT_V4").AppendFormat(",DVT_TENDVT_V4 = {0}", ":DVT_TENDVT_V4").AppendFormat(",DVT_GHICHU = {0}", ":DVT_GHICHU");
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
			return clsDAL.DeleteString("SDVT", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[DVT_MADVT] = {0}", "@DVT_MADVT");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("DVT_MADVT = {0}", ":DVT_MADVT");
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
			paramList.Add(clsDAL.CreateParameter("DVT_MADVT", "WChar", clsDAL.ToDBParam(DVT_MADVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("DVT_TENDVT", "WChar", clsDAL.ToDBParam(DVT_TENDVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DVT_MADVT_V4", "WChar", clsDAL.ToDBParam(DVT_MADVT_V4, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DVT_TENDVT_V4", "WChar", clsDAL.ToDBParam(DVT_TENDVT_V4, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DVT_GHICHU", "WChar", clsDAL.ToDBParam(DVT_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DVT_MADVT", "WChar", clsDAL.ToDBParam(DVT_MADVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("DVT_MADVT", "WChar", clsDAL.ToDBParam(DVT_MADVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DVT_TENDVT", "WChar", clsDAL.ToDBParam(DVT_TENDVT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DVT_MADVT_V4", "WChar", clsDAL.ToDBParam(DVT_MADVT_V4, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DVT_TENDVT_V4", "WChar", clsDAL.ToDBParam(DVT_TENDVT_V4, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DVT_GHICHU", "WChar", clsDAL.ToDBParam(DVT_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}