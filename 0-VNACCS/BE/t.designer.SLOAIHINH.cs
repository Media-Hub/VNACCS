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
	/// Generated Class for Table : SLOAIHINH.
	/// </summary>
	public partial class SLOAIHINH : TableBase
	{
		public SLOAIHINH() : base(){}

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
		private string m_MA_LH;
		/// <summary>
		/// MA_LH.
		/// </summary>
		public string MA_LH
		{
			get
			{
				return m_MA_LH;
			}
			set
			{
				if ((this.m_MA_LH != value))
				{
					this.SendPropertyChanging("MA_LH");
					this.m_MA_LH = value;
					this.SendPropertyChanged("MA_LH");
				}
			}
		}

		private string m_TEN_LH;
		private bool m_TEN_LHUpdated = false;
		/// <summary>
		/// TEN_LH.
		/// </summary>
		public string TEN_LH
		{
			get
			{
				return m_TEN_LH;
			}
			set
			{
				if ((this.m_TEN_LH != value))
				{
					this.SendPropertyChanging("TEN_LH");
					this.m_TEN_LH = value;
					this.SendPropertyChanged("TEN_LH");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_LHUpdated = true;
				}
			}
		}

		private string m_MA_LH_V4;
		private bool m_MA_LH_V4Updated = false;
		/// <summary>
		/// MA_LH_V4.
		/// </summary>
		public string MA_LH_V4
		{
			get
			{
				return m_MA_LH_V4;
			}
			set
			{
				if ((this.m_MA_LH_V4 != value))
				{
					this.SendPropertyChanging("MA_LH_V4");
					this.m_MA_LH_V4 = value;
					this.SendPropertyChanged("MA_LH_V4");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_MA_LH_V4Updated = true;
				}
			}
		}

		private string m_TEN_LH_V4;
		private bool m_TEN_LH_V4Updated = false;
		/// <summary>
		/// TEN_LH_V4.
		/// </summary>
		public string TEN_LH_V4
		{
			get
			{
				return m_TEN_LH_V4;
			}
			set
			{
				if ((this.m_TEN_LH_V4 != value))
				{
					this.SendPropertyChanging("TEN_LH_V4");
					this.m_TEN_LH_V4 = value;
					this.SendPropertyChanged("TEN_LH_V4");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_TEN_LH_V4Updated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SLOAIHINH " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[MA_LH]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TEN_LH]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[MA_LH_V4]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[TEN_LH_V4]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("MA_LH", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_LH", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("MA_LH_V4", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("TEN_LH_V4", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO SLOAIHINH ([MA_LH], [TEN_LH], [MA_LH_V4], [TEN_LH_V4]) VALUES(").Append("@MA_LH").Append(",").Append("@TEN_LH").Append(",").Append("@MA_LH_V4").Append(",").Append("@TEN_LH_V4").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO SLOAIHINH (MA_LH, TEN_LH, MA_LH_V4, TEN_LH_V4) VALUES(").Append(":MA_LH").Append(",").Append(":TEN_LH").Append(",").Append(":MA_LH_V4").Append(",").Append(":TEN_LH_V4").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SLOAIHINH SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_TEN_LHUpdated ? string.Format(",[TEN_LH] = {0}", "@TEN_LH") : string.Empty).Append(m_MA_LH_V4Updated ? string.Format(",[MA_LH_V4] = {0}", "@MA_LH_V4") : string.Empty).Append(m_TEN_LH_V4Updated ? string.Format(",[TEN_LH_V4] = {0}", "@TEN_LH_V4") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_TEN_LHUpdated ? string.Format(",TEN_LH = {0}", ":TEN_LH") : string.Empty).Append(m_MA_LH_V4Updated ? string.Format(",MA_LH_V4 = {0}", ":MA_LH_V4") : string.Empty).Append(m_TEN_LH_V4Updated ? string.Format(",TEN_LH_V4 = {0}", ":TEN_LH_V4") : string.Empty);
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
				sbSQL.AppendFormat("[TEN_LH] = {0}", "@TEN_LH").AppendFormat(",[MA_LH_V4] = {0}", "@MA_LH_V4").AppendFormat(",[TEN_LH_V4] = {0}", "@TEN_LH_V4");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("TEN_LH = {0}", ":TEN_LH").AppendFormat(",MA_LH_V4 = {0}", ":MA_LH_V4").AppendFormat(",TEN_LH_V4 = {0}", ":TEN_LH_V4");
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
			return clsDAL.DeleteString("SLOAIHINH", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[MA_LH] = {0}", "@MA_LH");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("MA_LH = {0}", ":MA_LH");
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
			paramList.Add(clsDAL.CreateParameter("MA_LH", "WChar", clsDAL.ToDBParam(MA_LH, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("TEN_LH", "WChar", clsDAL.ToDBParam(TEN_LH, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("MA_LH_V4", "WChar", clsDAL.ToDBParam(MA_LH_V4, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TEN_LH_V4", "WChar", clsDAL.ToDBParam(TEN_LH_V4, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("MA_LH", "WChar", clsDAL.ToDBParam(MA_LH, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("MA_LH", "WChar", clsDAL.ToDBParam(MA_LH, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TEN_LH", "WChar", clsDAL.ToDBParam(TEN_LH, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("MA_LH_V4", "WChar", clsDAL.ToDBParam(MA_LH_V4, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TEN_LH_V4", "WChar", clsDAL.ToDBParam(TEN_LH_V4, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}