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
	/// Generated Class for Table : SLOAIKIEN.
	/// </summary>
	public partial class SLOAIKIEN : TableBase
	{
		public SLOAIKIEN() : base(){}

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
		private string m_LK_MALK;
		/// <summary>
		/// LK_MALK.
		/// </summary>
		public string LK_MALK
		{
			get
			{
				return m_LK_MALK;
			}
			set
			{
				if ((this.m_LK_MALK != value))
				{
					this.SendPropertyChanging("LK_MALK");
					this.m_LK_MALK = value;
					this.SendPropertyChanged("LK_MALK");
				}
			}
		}

		private string m_LK_TENLK;
		private bool m_LK_TENLKUpdated = false;
		/// <summary>
		/// LK_TENLK.
		/// </summary>
		public string LK_TENLK
		{
			get
			{
				return m_LK_TENLK;
			}
			set
			{
				if ((this.m_LK_TENLK != value))
				{
					this.SendPropertyChanging("LK_TENLK");
					this.m_LK_TENLK = value;
					this.SendPropertyChanged("LK_TENLK");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_LK_TENLKUpdated = true;
				}
			}
		}

		private string m_LK_MALK_V4;
		private bool m_LK_MALK_V4Updated = false;
		/// <summary>
		/// LK_MALK_V4.
		/// </summary>
		public string LK_MALK_V4
		{
			get
			{
				return m_LK_MALK_V4;
			}
			set
			{
				if ((this.m_LK_MALK_V4 != value))
				{
					this.SendPropertyChanging("LK_MALK_V4");
					this.m_LK_MALK_V4 = value;
					this.SendPropertyChanged("LK_MALK_V4");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_LK_MALK_V4Updated = true;
				}
			}
		}

		private string m_LK_TENLK_V4;
		private bool m_LK_TENLK_V4Updated = false;
		/// <summary>
		/// LK_TENLK_V4.
		/// </summary>
		public string LK_TENLK_V4
		{
			get
			{
				return m_LK_TENLK_V4;
			}
			set
			{
				if ((this.m_LK_TENLK_V4 != value))
				{
					this.SendPropertyChanging("LK_TENLK_V4");
					this.m_LK_TENLK_V4 = value;
					this.SendPropertyChanged("LK_TENLK_V4");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_LK_TENLK_V4Updated = true;
				}
			}
		}

		private string m_LK_GHICHU;
		private bool m_LK_GHICHUUpdated = false;
		/// <summary>
		/// LK_GHICHU.
		/// </summary>
		public string LK_GHICHU
		{
			get
			{
				return m_LK_GHICHU;
			}
			set
			{
				if ((this.m_LK_GHICHU != value))
				{
					this.SendPropertyChanging("LK_GHICHU");
					this.m_LK_GHICHU = value;
					this.SendPropertyChanged("LK_GHICHU");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_LK_GHICHUUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM SLOAIKIEN " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[LK_MALK]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[LK_TENLK]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[LK_MALK_V4]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[LK_TENLK_V4]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[LK_GHICHU]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("LK_MALK", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("LK_TENLK", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("LK_MALK_V4", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("LK_TENLK_V4", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("LK_GHICHU", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO SLOAIKIEN ([LK_MALK], [LK_TENLK], [LK_MALK_V4], [LK_TENLK_V4], [LK_GHICHU]) VALUES(").Append("@LK_MALK").Append(",").Append("@LK_TENLK").Append(",").Append("@LK_MALK_V4").Append(",").Append("@LK_TENLK_V4").Append(",").Append("@LK_GHICHU").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO SLOAIKIEN (LK_MALK, LK_TENLK, LK_MALK_V4, LK_TENLK_V4, LK_GHICHU) VALUES(").Append(":LK_MALK").Append(",").Append(":LK_TENLK").Append(",").Append(":LK_MALK_V4").Append(",").Append(":LK_TENLK_V4").Append(",").Append(":LK_GHICHU").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE SLOAIKIEN SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_LK_TENLKUpdated ? string.Format(",[LK_TENLK] = {0}", "@LK_TENLK") : string.Empty).Append(m_LK_MALK_V4Updated ? string.Format(",[LK_MALK_V4] = {0}", "@LK_MALK_V4") : string.Empty).Append(m_LK_TENLK_V4Updated ? string.Format(",[LK_TENLK_V4] = {0}", "@LK_TENLK_V4") : string.Empty).Append(m_LK_GHICHUUpdated ? string.Format(",[LK_GHICHU] = {0}", "@LK_GHICHU") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_LK_TENLKUpdated ? string.Format(",LK_TENLK = {0}", ":LK_TENLK") : string.Empty).Append(m_LK_MALK_V4Updated ? string.Format(",LK_MALK_V4 = {0}", ":LK_MALK_V4") : string.Empty).Append(m_LK_TENLK_V4Updated ? string.Format(",LK_TENLK_V4 = {0}", ":LK_TENLK_V4") : string.Empty).Append(m_LK_GHICHUUpdated ? string.Format(",LK_GHICHU = {0}", ":LK_GHICHU") : string.Empty);
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
				sbSQL.AppendFormat("[LK_TENLK] = {0}", "@LK_TENLK").AppendFormat(",[LK_MALK_V4] = {0}", "@LK_MALK_V4").AppendFormat(",[LK_TENLK_V4] = {0}", "@LK_TENLK_V4").AppendFormat(",[LK_GHICHU] = {0}", "@LK_GHICHU");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("LK_TENLK = {0}", ":LK_TENLK").AppendFormat(",LK_MALK_V4 = {0}", ":LK_MALK_V4").AppendFormat(",LK_TENLK_V4 = {0}", ":LK_TENLK_V4").AppendFormat(",LK_GHICHU = {0}", ":LK_GHICHU");
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
			return clsDAL.DeleteString("SLOAIKIEN", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[LK_MALK] = {0}", "@LK_MALK");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("LK_MALK = {0}", ":LK_MALK");
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
			paramList.Add(clsDAL.CreateParameter("LK_MALK", "WChar", clsDAL.ToDBParam(LK_MALK, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("LK_TENLK", "WChar", clsDAL.ToDBParam(LK_TENLK, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("LK_MALK_V4", "WChar", clsDAL.ToDBParam(LK_MALK_V4, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("LK_TENLK_V4", "WChar", clsDAL.ToDBParam(LK_TENLK_V4, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("LK_GHICHU", "WChar", clsDAL.ToDBParam(LK_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("LK_MALK", "WChar", clsDAL.ToDBParam(LK_MALK, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("LK_MALK", "WChar", clsDAL.ToDBParam(LK_MALK, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("LK_TENLK", "WChar", clsDAL.ToDBParam(LK_TENLK, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("LK_MALK_V4", "WChar", clsDAL.ToDBParam(LK_MALK_V4, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("LK_TENLK_V4", "WChar", clsDAL.ToDBParam(LK_TENLK_V4, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("LK_GHICHU", "WChar", clsDAL.ToDBParam(LK_GHICHU, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}