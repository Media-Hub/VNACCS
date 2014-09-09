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
		private string m_DV_MST;
		/// <summary>
		/// DV_MST.
		/// </summary>
		public string DV_MST
		{
			get
			{
				return m_DV_MST;
			}
			set
			{
				if ((this.m_DV_MST != value))
				{
					this.SendPropertyChanging("DV_MST");
					this.m_DV_MST = value;
					this.SendPropertyChanged("DV_MST");
				}
			}
		}

		private string m_DV_TEN;
		private bool m_DV_TENUpdated = false;
		/// <summary>
		/// DV_TEN.
		/// </summary>
		public string DV_TEN
		{
			get
			{
				return m_DV_TEN;
			}
			set
			{
				if ((this.m_DV_TEN != value))
				{
					this.SendPropertyChanging("DV_TEN");
					this.m_DV_TEN = value;
					this.SendPropertyChanged("DV_TEN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DV_TENUpdated = true;
				}
			}
		}

		private string m_DV_DIACHI;
		private bool m_DV_DIACHIUpdated = false;
		/// <summary>
		/// DV_DIACHI.
		/// </summary>
		public string DV_DIACHI
		{
			get
			{
				return m_DV_DIACHI;
			}
			set
			{
				if ((this.m_DV_DIACHI != value))
				{
					this.SendPropertyChanging("DV_DIACHI");
					this.m_DV_DIACHI = value;
					this.SendPropertyChanged("DV_DIACHI");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DV_DIACHIUpdated = true;
				}
			}
		}

		private string m_DV_DIENTHOAI;
		private bool m_DV_DIENTHOAIUpdated = false;
		/// <summary>
		/// DV_DIENTHOAI.
		/// </summary>
		public string DV_DIENTHOAI
		{
			get
			{
				return m_DV_DIENTHOAI;
			}
			set
			{
				if ((this.m_DV_DIENTHOAI != value))
				{
					this.SendPropertyChanging("DV_DIENTHOAI");
					this.m_DV_DIENTHOAI = value;
					this.SendPropertyChanged("DV_DIENTHOAI");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DV_DIENTHOAIUpdated = true;
				}
			}
		}

		private string m_DV_FAX;
		private bool m_DV_FAXUpdated = false;
		/// <summary>
		/// DV_FAX.
		/// </summary>
		public string DV_FAX
		{
			get
			{
				return m_DV_FAX;
			}
			set
			{
				if ((this.m_DV_FAX != value))
				{
					this.SendPropertyChanging("DV_FAX");
					this.m_DV_FAX = value;
					this.SendPropertyChanged("DV_FAX");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DV_FAXUpdated = true;
				}
			}
		}

		private string m_VNACCS_MAHQ;
		private bool m_VNACCS_MAHQUpdated = false;
		/// <summary>
		/// VNACCS_MAHQ.
		/// </summary>
		public string VNACCS_MAHQ
		{
			get
			{
				return m_VNACCS_MAHQ;
			}
			set
			{
				if ((this.m_VNACCS_MAHQ != value))
				{
					this.SendPropertyChanging("VNACCS_MAHQ");
					this.m_VNACCS_MAHQ = value;
					this.SendPropertyChanged("VNACCS_MAHQ");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_VNACCS_MAHQUpdated = true;
				}
			}
		}

		private string m_VNACCS_MABOPHAN;
		private bool m_VNACCS_MABOPHANUpdated = false;
		/// <summary>
		/// VNACCS_MABOPHAN.
		/// </summary>
		public string VNACCS_MABOPHAN
		{
			get
			{
				return m_VNACCS_MABOPHAN;
			}
			set
			{
				if ((this.m_VNACCS_MABOPHAN != value))
				{
					this.SendPropertyChanging("VNACCS_MABOPHAN");
					this.m_VNACCS_MABOPHAN = value;
					this.SendPropertyChanged("VNACCS_MABOPHAN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_VNACCS_MABOPHANUpdated = true;
				}
			}
		}

		private string m_VNACCS_TERMINALID;
		private bool m_VNACCS_TERMINALIDUpdated = false;
		/// <summary>
		/// VNACCS_TERMINALID.
		/// </summary>
		public string VNACCS_TERMINALID
		{
			get
			{
				return m_VNACCS_TERMINALID;
			}
			set
			{
				if ((this.m_VNACCS_TERMINALID != value))
				{
					this.SendPropertyChanging("VNACCS_TERMINALID");
					this.m_VNACCS_TERMINALID = value;
					this.SendPropertyChanged("VNACCS_TERMINALID");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_VNACCS_TERMINALIDUpdated = true;
				}
			}
		}

		private string m_VNACCS_TERMINALACCESSKEY;
		private bool m_VNACCS_TERMINALACCESSKEYUpdated = false;
		/// <summary>
		/// VNACCS_TERMINALACCESSKEY.
		/// </summary>
		public string VNACCS_TERMINALACCESSKEY
		{
			get
			{
				return m_VNACCS_TERMINALACCESSKEY;
			}
			set
			{
				if ((this.m_VNACCS_TERMINALACCESSKEY != value))
				{
					this.SendPropertyChanging("VNACCS_TERMINALACCESSKEY");
					this.m_VNACCS_TERMINALACCESSKEY = value;
					this.SendPropertyChanged("VNACCS_TERMINALACCESSKEY");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_VNACCS_TERMINALACCESSKEYUpdated = true;
				}
			}
		}

		private string m_VNACCS_USERID;
		private bool m_VNACCS_USERIDUpdated = false;
		/// <summary>
		/// VNACCS_USERID.
		/// </summary>
		public string VNACCS_USERID
		{
			get
			{
				return m_VNACCS_USERID;
			}
			set
			{
				if ((this.m_VNACCS_USERID != value))
				{
					this.SendPropertyChanging("VNACCS_USERID");
					this.m_VNACCS_USERID = value;
					this.SendPropertyChanged("VNACCS_USERID");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_VNACCS_USERIDUpdated = true;
				}
			}
		}

		private string m_VNACCS_PASSWORD;
		private bool m_VNACCS_PASSWORDUpdated = false;
		/// <summary>
		/// VNACCS_PASSWORD.
		/// </summary>
		public string VNACCS_PASSWORD
		{
			get
			{
				return m_VNACCS_PASSWORD;
			}
			set
			{
				if ((this.m_VNACCS_PASSWORD != value))
				{
					this.SendPropertyChanging("VNACCS_PASSWORD");
					this.m_VNACCS_PASSWORD = value;
					this.SendPropertyChanged("VNACCS_PASSWORD");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_VNACCS_PASSWORDUpdated = true;
				}
			}
		}

		private string m_V4_MAHQ;
		private bool m_V4_MAHQUpdated = false;
		/// <summary>
		/// V4_MAHQ.
		/// </summary>
		public string V4_MAHQ
		{
			get
			{
				return m_V4_MAHQ;
			}
			set
			{
				if ((this.m_V4_MAHQ != value))
				{
					this.SendPropertyChanging("V4_MAHQ");
					this.m_V4_MAHQ = value;
					this.SendPropertyChanged("V4_MAHQ");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_V4_MAHQUpdated = true;
				}
			}
		}

		private string m_V4_IPHQ;
		private bool m_V4_IPHQUpdated = false;
		/// <summary>
		/// V4_IPHQ.
		/// </summary>
		public string V4_IPHQ
		{
			get
			{
				return m_V4_IPHQ;
			}
			set
			{
				if ((this.m_V4_IPHQ != value))
				{
					this.SendPropertyChanging("V4_IPHQ");
					this.m_V4_IPHQ = value;
					this.SendPropertyChanged("V4_IPHQ");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_V4_IPHQUpdated = true;
				}
			}
		}

		private string m_V4_TAIKHOANKHAI;
		private bool m_V4_TAIKHOANKHAIUpdated = false;
		/// <summary>
		/// V4_TAIKHOANKHAI.
		/// </summary>
		public string V4_TAIKHOANKHAI
		{
			get
			{
				return m_V4_TAIKHOANKHAI;
			}
			set
			{
				if ((this.m_V4_TAIKHOANKHAI != value))
				{
					this.SendPropertyChanging("V4_TAIKHOANKHAI");
					this.m_V4_TAIKHOANKHAI = value;
					this.SendPropertyChanged("V4_TAIKHOANKHAI");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_V4_TAIKHOANKHAIUpdated = true;
				}
			}
		}

		private string m_V4_MATKHAUKHAI;
		private bool m_V4_MATKHAUKHAIUpdated = false;
		/// <summary>
		/// V4_MATKHAUKHAI.
		/// </summary>
		public string V4_MATKHAUKHAI
		{
			get
			{
				return m_V4_MATKHAUKHAI;
			}
			set
			{
				if ((this.m_V4_MATKHAUKHAI != value))
				{
					this.SendPropertyChanging("V4_MATKHAUKHAI");
					this.m_V4_MATKHAUKHAI = value;
					this.SendPropertyChanged("V4_MATKHAUKHAI");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_V4_MATKHAUKHAIUpdated = true;
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
			switch(this.DataManagement)
			{
				case DBManagement.Access:
				case DBManagement.SQL:
				case DBManagement.SQLLite:
				default:
				sbSQL.Append(clsDAL.SelectField("[DV_MST]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DV_TEN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DV_DIACHI]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DV_DIENTHOAI]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DV_FAX]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[VNACCS_MAHQ]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[VNACCS_MABOPHAN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[VNACCS_TERMINALID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[VNACCS_TERMINALACCESSKEY]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[VNACCS_USERID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[VNACCS_PASSWORD]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[V4_MAHQ]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[V4_IPHQ]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[V4_TAIKHOANKHAI]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[V4_MATKHAUKHAI]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("DV_MST", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DV_TEN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DV_DIACHI", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DV_DIENTHOAI", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DV_FAX", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("VNACCS_MAHQ", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("VNACCS_MABOPHAN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("VNACCS_TERMINALID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("VNACCS_TERMINALACCESSKEY", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("VNACCS_USERID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("VNACCS_PASSWORD", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("V4_MAHQ", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("V4_IPHQ", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("V4_TAIKHOANKHAI", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("V4_MATKHAUKHAI", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO SDONVI ([DV_MST], [DV_TEN], [DV_DIACHI], [DV_DIENTHOAI], [DV_FAX], [VNACCS_MAHQ], [VNACCS_MABOPHAN], [VNACCS_TERMINALID], [VNACCS_TERMINALACCESSKEY], [VNACCS_USERID], [VNACCS_PASSWORD], [V4_MAHQ], [V4_IPHQ], [V4_TAIKHOANKHAI], [V4_MATKHAUKHAI]) VALUES(").Append("@DV_MST").Append(",").Append("@DV_TEN").Append(",").Append("@DV_DIACHI").Append(",").Append("@DV_DIENTHOAI").Append(",").Append("@DV_FAX").Append(",").Append("@VNACCS_MAHQ").Append(",").Append("@VNACCS_MABOPHAN").Append(",").Append("@VNACCS_TERMINALID").Append(",").Append("@VNACCS_TERMINALACCESSKEY").Append(",").Append("@VNACCS_USERID").Append(",").Append("@VNACCS_PASSWORD").Append(",").Append("@V4_MAHQ").Append(",").Append("@V4_IPHQ").Append(",").Append("@V4_TAIKHOANKHAI").Append(",").Append("@V4_MATKHAUKHAI").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO SDONVI (DV_MST, DV_TEN, DV_DIACHI, DV_DIENTHOAI, DV_FAX, VNACCS_MAHQ, VNACCS_MABOPHAN, VNACCS_TERMINALID, VNACCS_TERMINALACCESSKEY, VNACCS_USERID, VNACCS_PASSWORD, V4_MAHQ, V4_IPHQ, V4_TAIKHOANKHAI, V4_MATKHAUKHAI) VALUES(").Append(":DV_MST").Append(",").Append(":DV_TEN").Append(",").Append(":DV_DIACHI").Append(",").Append(":DV_DIENTHOAI").Append(",").Append(":DV_FAX").Append(",").Append(":VNACCS_MAHQ").Append(",").Append(":VNACCS_MABOPHAN").Append(",").Append(":VNACCS_TERMINALID").Append(",").Append(":VNACCS_TERMINALACCESSKEY").Append(",").Append(":VNACCS_USERID").Append(",").Append(":VNACCS_PASSWORD").Append(",").Append(":V4_MAHQ").Append(",").Append(":V4_IPHQ").Append(",").Append(":V4_TAIKHOANKHAI").Append(",").Append(":V4_MATKHAUKHAI").Append(")");
				break;
			}
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
			switch(this.DataManagement)
			{
				case DBManagement.Access:
					return UpdateFullStatement(WhereClause);
				case DBManagement.SQL:
				case DBManagement.SQLLite:
				default:
				sbSQL.Append(m_DV_TENUpdated ? string.Format(",[DV_TEN] = {0}", "@DV_TEN") : string.Empty).Append(m_DV_DIACHIUpdated ? string.Format(",[DV_DIACHI] = {0}", "@DV_DIACHI") : string.Empty).Append(m_DV_DIENTHOAIUpdated ? string.Format(",[DV_DIENTHOAI] = {0}", "@DV_DIENTHOAI") : string.Empty).Append(m_DV_FAXUpdated ? string.Format(",[DV_FAX] = {0}", "@DV_FAX") : string.Empty).Append(m_VNACCS_MAHQUpdated ? string.Format(",[VNACCS_MAHQ] = {0}", "@VNACCS_MAHQ") : string.Empty).Append(m_VNACCS_MABOPHANUpdated ? string.Format(",[VNACCS_MABOPHAN] = {0}", "@VNACCS_MABOPHAN") : string.Empty).Append(m_VNACCS_TERMINALIDUpdated ? string.Format(",[VNACCS_TERMINALID] = {0}", "@VNACCS_TERMINALID") : string.Empty).Append(m_VNACCS_TERMINALACCESSKEYUpdated ? string.Format(",[VNACCS_TERMINALACCESSKEY] = {0}", "@VNACCS_TERMINALACCESSKEY") : string.Empty).Append(m_VNACCS_USERIDUpdated ? string.Format(",[VNACCS_USERID] = {0}", "@VNACCS_USERID") : string.Empty).Append(m_VNACCS_PASSWORDUpdated ? string.Format(",[VNACCS_PASSWORD] = {0}", "@VNACCS_PASSWORD") : string.Empty).Append(m_V4_MAHQUpdated ? string.Format(",[V4_MAHQ] = {0}", "@V4_MAHQ") : string.Empty).Append(m_V4_IPHQUpdated ? string.Format(",[V4_IPHQ] = {0}", "@V4_IPHQ") : string.Empty).Append(m_V4_TAIKHOANKHAIUpdated ? string.Format(",[V4_TAIKHOANKHAI] = {0}", "@V4_TAIKHOANKHAI") : string.Empty).Append(m_V4_MATKHAUKHAIUpdated ? string.Format(",[V4_MATKHAUKHAI] = {0}", "@V4_MATKHAUKHAI") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_DV_TENUpdated ? string.Format(",DV_TEN = {0}", ":DV_TEN") : string.Empty).Append(m_DV_DIACHIUpdated ? string.Format(",DV_DIACHI = {0}", ":DV_DIACHI") : string.Empty).Append(m_DV_DIENTHOAIUpdated ? string.Format(",DV_DIENTHOAI = {0}", ":DV_DIENTHOAI") : string.Empty).Append(m_DV_FAXUpdated ? string.Format(",DV_FAX = {0}", ":DV_FAX") : string.Empty).Append(m_VNACCS_MAHQUpdated ? string.Format(",VNACCS_MAHQ = {0}", ":VNACCS_MAHQ") : string.Empty).Append(m_VNACCS_MABOPHANUpdated ? string.Format(",VNACCS_MABOPHAN = {0}", ":VNACCS_MABOPHAN") : string.Empty).Append(m_VNACCS_TERMINALIDUpdated ? string.Format(",VNACCS_TERMINALID = {0}", ":VNACCS_TERMINALID") : string.Empty).Append(m_VNACCS_TERMINALACCESSKEYUpdated ? string.Format(",VNACCS_TERMINALACCESSKEY = {0}", ":VNACCS_TERMINALACCESSKEY") : string.Empty).Append(m_VNACCS_USERIDUpdated ? string.Format(",VNACCS_USERID = {0}", ":VNACCS_USERID") : string.Empty).Append(m_VNACCS_PASSWORDUpdated ? string.Format(",VNACCS_PASSWORD = {0}", ":VNACCS_PASSWORD") : string.Empty).Append(m_V4_MAHQUpdated ? string.Format(",V4_MAHQ = {0}", ":V4_MAHQ") : string.Empty).Append(m_V4_IPHQUpdated ? string.Format(",V4_IPHQ = {0}", ":V4_IPHQ") : string.Empty).Append(m_V4_TAIKHOANKHAIUpdated ? string.Format(",V4_TAIKHOANKHAI = {0}", ":V4_TAIKHOANKHAI") : string.Empty).Append(m_V4_MATKHAUKHAIUpdated ? string.Format(",V4_MATKHAUKHAI = {0}", ":V4_MATKHAUKHAI") : string.Empty);
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
				sbSQL.AppendFormat("[DV_TEN] = {0}", "@DV_TEN").AppendFormat(",[DV_DIACHI] = {0}", "@DV_DIACHI").AppendFormat(",[DV_DIENTHOAI] = {0}", "@DV_DIENTHOAI").AppendFormat(",[DV_FAX] = {0}", "@DV_FAX").AppendFormat(",[VNACCS_MAHQ] = {0}", "@VNACCS_MAHQ").AppendFormat(",[VNACCS_MABOPHAN] = {0}", "@VNACCS_MABOPHAN").AppendFormat(",[VNACCS_TERMINALID] = {0}", "@VNACCS_TERMINALID").AppendFormat(",[VNACCS_TERMINALACCESSKEY] = {0}", "@VNACCS_TERMINALACCESSKEY").AppendFormat(",[VNACCS_USERID] = {0}", "@VNACCS_USERID").AppendFormat(",[VNACCS_PASSWORD] = {0}", "@VNACCS_PASSWORD").AppendFormat(",[V4_MAHQ] = {0}", "@V4_MAHQ").AppendFormat(",[V4_IPHQ] = {0}", "@V4_IPHQ").AppendFormat(",[V4_TAIKHOANKHAI] = {0}", "@V4_TAIKHOANKHAI").AppendFormat(",[V4_MATKHAUKHAI] = {0}", "@V4_MATKHAUKHAI");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("DV_TEN = {0}", ":DV_TEN").AppendFormat(",DV_DIACHI = {0}", ":DV_DIACHI").AppendFormat(",DV_DIENTHOAI = {0}", ":DV_DIENTHOAI").AppendFormat(",DV_FAX = {0}", ":DV_FAX").AppendFormat(",VNACCS_MAHQ = {0}", ":VNACCS_MAHQ").AppendFormat(",VNACCS_MABOPHAN = {0}", ":VNACCS_MABOPHAN").AppendFormat(",VNACCS_TERMINALID = {0}", ":VNACCS_TERMINALID").AppendFormat(",VNACCS_TERMINALACCESSKEY = {0}", ":VNACCS_TERMINALACCESSKEY").AppendFormat(",VNACCS_USERID = {0}", ":VNACCS_USERID").AppendFormat(",VNACCS_PASSWORD = {0}", ":VNACCS_PASSWORD").AppendFormat(",V4_MAHQ = {0}", ":V4_MAHQ").AppendFormat(",V4_IPHQ = {0}", ":V4_IPHQ").AppendFormat(",V4_TAIKHOANKHAI = {0}", ":V4_TAIKHOANKHAI").AppendFormat(",V4_MATKHAUKHAI = {0}", ":V4_MATKHAUKHAI");
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
			switch(this.DataManagement)
			{
				case DBManagement.Access:
				case DBManagement.SQL:
				case DBManagement.SQLLite:
				default:
				sbSQL.AppendFormat("[DV_MST] = {0}", "@DV_MST");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("DV_MST = {0}", ":DV_MST");
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
			paramList.Add(clsDAL.CreateParameter("DV_MST", "WChar", clsDAL.ToDBParam(DV_MST, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("DV_TEN", "WChar", clsDAL.ToDBParam(DV_TEN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DV_DIACHI", "WChar", clsDAL.ToDBParam(DV_DIACHI, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DV_DIENTHOAI", "WChar", clsDAL.ToDBParam(DV_DIENTHOAI, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DV_FAX", "WChar", clsDAL.ToDBParam(DV_FAX, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("VNACCS_MAHQ", "WChar", clsDAL.ToDBParam(VNACCS_MAHQ, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("VNACCS_MABOPHAN", "WChar", clsDAL.ToDBParam(VNACCS_MABOPHAN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("VNACCS_TERMINALID", "WChar", clsDAL.ToDBParam(VNACCS_TERMINALID, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("VNACCS_TERMINALACCESSKEY", "WChar", clsDAL.ToDBParam(VNACCS_TERMINALACCESSKEY, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("VNACCS_USERID", "WChar", clsDAL.ToDBParam(VNACCS_USERID, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("VNACCS_PASSWORD", "WChar", clsDAL.ToDBParam(VNACCS_PASSWORD, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("V4_MAHQ", "WChar", clsDAL.ToDBParam(V4_MAHQ, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("V4_IPHQ", "WChar", clsDAL.ToDBParam(V4_IPHQ, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("V4_TAIKHOANKHAI", "WChar", clsDAL.ToDBParam(V4_TAIKHOANKHAI, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("V4_MATKHAUKHAI", "WChar", clsDAL.ToDBParam(V4_MATKHAUKHAI, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DV_MST", "WChar", clsDAL.ToDBParam(DV_MST, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("DV_MST", "WChar", clsDAL.ToDBParam(DV_MST, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DV_TEN", "WChar", clsDAL.ToDBParam(DV_TEN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DV_DIACHI", "WChar", clsDAL.ToDBParam(DV_DIACHI, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DV_DIENTHOAI", "WChar", clsDAL.ToDBParam(DV_DIENTHOAI, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DV_FAX", "WChar", clsDAL.ToDBParam(DV_FAX, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("VNACCS_MAHQ", "WChar", clsDAL.ToDBParam(VNACCS_MAHQ, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("VNACCS_MABOPHAN", "WChar", clsDAL.ToDBParam(VNACCS_MABOPHAN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("VNACCS_TERMINALID", "WChar", clsDAL.ToDBParam(VNACCS_TERMINALID, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("VNACCS_TERMINALACCESSKEY", "WChar", clsDAL.ToDBParam(VNACCS_TERMINALACCESSKEY, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("VNACCS_USERID", "WChar", clsDAL.ToDBParam(VNACCS_USERID, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("VNACCS_PASSWORD", "WChar", clsDAL.ToDBParam(VNACCS_PASSWORD, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("V4_MAHQ", "WChar", clsDAL.ToDBParam(V4_MAHQ, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("V4_IPHQ", "WChar", clsDAL.ToDBParam(V4_IPHQ, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("V4_TAIKHOANKHAI", "WChar", clsDAL.ToDBParam(V4_TAIKHOANKHAI, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("V4_MATKHAUKHAI", "WChar", clsDAL.ToDBParam(V4_MATKHAUKHAI, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}