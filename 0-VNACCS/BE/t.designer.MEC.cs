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
	/// Generated Class for Table : MEC.
	/// </summary>
	public partial class MEC : TableBase
	{
		public MEC() : base(){}

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
		private int m_TK_ID = 0;
		/// <summary>
		/// TK_ID.
		/// </summary>
		public int TK_ID
		{
			get
			{
				return m_TK_ID;
			}
			set
			{
				if ((this.m_TK_ID != value))
				{
					this.SendPropertyChanging("TK_ID");
					this.m_TK_ID = value;
					this.SendPropertyChanged("TK_ID");
				}
			}
		}

		private string m_ECN;
		private bool m_ECNUpdated = false;
		/// <summary>
		/// Số tờ khai.
		/// </summary>
		public string ECN
		{
			get
			{
				return m_ECN;
			}
			set
			{
				if ((this.m_ECN != value))
				{
					this.SendPropertyChanging("ECN");
					this.m_ECN = value;
					this.SendPropertyChanged("ECN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_ECNUpdated = true;
				}
			}
		}

		private string m_JKN;
		private bool m_JKNUpdated = false;
		/// <summary>
		/// Mã phân loại cá nhân tổ chức (đối với tờ khai xuất thì chưa sử dụng, tạm thời ẩn).
		/// </summary>
		public string JKN
		{
			get
			{
				return m_JKN;
			}
			set
			{
				if ((this.m_JKN != value))
				{
					this.SendPropertyChanging("JKN");
					this.m_JKN = value;
					this.SendPropertyChanged("JKN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_JKNUpdated = true;
				}
			}
		}

		private string m_CH;
		private bool m_CHUpdated = false;
		/// <summary>
		/// Mã Hải quan.
		/// </summary>
		public string CH
		{
			get
			{
				return m_CH;
			}
			set
			{
				if ((this.m_CH != value))
				{
					this.SendPropertyChanging("CH");
					this.m_CH = value;
					this.SendPropertyChanged("CH");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_CHUpdated = true;
				}
			}
		}

		private string m_CHB;
		private bool m_CHBUpdated = false;
		/// <summary>
		/// Mã bộ phận xử lý tờ khai.
		/// </summary>
		public string CHB
		{
			get
			{
				return m_CHB;
			}
			set
			{
				if ((this.m_CHB != value))
				{
					this.SendPropertyChanging("CHB");
					this.m_CHB = value;
					this.SendPropertyChanged("CHB");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_CHBUpdated = true;
				}
			}
		}

		private string m_EPC;
		private bool m_EPCUpdated = false;
		/// <summary>
		/// Nhập mã số thuế của người xuất khẩu..
		/// </summary>
		public string EPC
		{
			get
			{
				return m_EPC;
			}
			set
			{
				if ((this.m_EPC != value))
				{
					this.SendPropertyChanging("EPC");
					this.m_EPC = value;
					this.SendPropertyChanged("EPC");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_EPCUpdated = true;
				}
			}
		}

		private string m_EPN;
		private bool m_EPNUpdated = false;
		/// <summary>
		/// Tên người xuất khẩu.
		/// </summary>
		public string EPN
		{
			get
			{
				return m_EPN;
			}
			set
			{
				if ((this.m_EPN != value))
				{
					this.SendPropertyChanging("EPN");
					this.m_EPN = value;
					this.SendPropertyChanged("EPN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_EPNUpdated = true;
				}
			}
		}

		private string m_EPP;
		private bool m_EPPUpdated = false;
		/// <summary>
		/// Mã bưu chính người xuất khẩu.
		/// </summary>
		public string EPP
		{
			get
			{
				return m_EPP;
			}
			set
			{
				if ((this.m_EPP != value))
				{
					this.SendPropertyChanging("EPP");
					this.m_EPP = value;
					this.SendPropertyChanged("EPP");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_EPPUpdated = true;
				}
			}
		}

		private string m_EPA;
		private bool m_EPAUpdated = false;
		/// <summary>
		/// Địa chỉ người xuất khẩu (số nhà,...).
		/// </summary>
		public string EPA
		{
			get
			{
				return m_EPA;
			}
			set
			{
				if ((this.m_EPA != value))
				{
					this.SendPropertyChanging("EPA");
					this.m_EPA = value;
					this.SendPropertyChanged("EPA");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_EPAUpdated = true;
				}
			}
		}

		private string m_EPT;
		private bool m_EPTUpdated = false;
		/// <summary>
		/// Địa chỉ người xuất khẩu (tên đường,...).
		/// </summary>
		public string EPT
		{
			get
			{
				return m_EPT;
			}
			set
			{
				if ((this.m_EPT != value))
				{
					this.SendPropertyChanging("EPT");
					this.m_EPT = value;
					this.SendPropertyChanged("EPT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_EPTUpdated = true;
				}
			}
		}

		private string m_CGC;
		private bool m_CGCUpdated = false;
		/// <summary>
		/// Mã người nhập khẩu (nếu có).
		/// </summary>
		public string CGC
		{
			get
			{
				return m_CGC;
			}
			set
			{
				if ((this.m_CGC != value))
				{
					this.SendPropertyChanging("CGC");
					this.m_CGC = value;
					this.SendPropertyChanged("CGC");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_CGCUpdated = true;
				}
			}
		}

		private string m_CGN;
		private bool m_CGNUpdated = false;
		/// <summary>
		/// Tên người nhập khẩu.
		/// </summary>
		public string CGN
		{
			get
			{
				return m_CGN;
			}
			set
			{
				if ((this.m_CGN != value))
				{
					this.SendPropertyChanging("CGN");
					this.m_CGN = value;
					this.SendPropertyChanged("CGN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_CGNUpdated = true;
				}
			}
		}

		private string m_CGP;
		private bool m_CGPUpdated = false;
		/// <summary>
		/// Mã bưu chính người nhập khẩu.
		/// </summary>
		public string CGP
		{
			get
			{
				return m_CGP;
			}
			set
			{
				if ((this.m_CGP != value))
				{
					this.SendPropertyChanging("CGP");
					this.m_CGP = value;
					this.SendPropertyChanged("CGP");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_CGPUpdated = true;
				}
			}
		}

		private string m_CGA;
		private bool m_CGAUpdated = false;
		/// <summary>
		/// Địa chỉ người nhập khẩu (số nhà,...).
		/// </summary>
		public string CGA
		{
			get
			{
				return m_CGA;
			}
			set
			{
				if ((this.m_CGA != value))
				{
					this.SendPropertyChanging("CGA");
					this.m_CGA = value;
					this.SendPropertyChanged("CGA");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_CGAUpdated = true;
				}
			}
		}

		private string m_CAT;
		private bool m_CATUpdated = false;
		/// <summary>
		/// Địa chỉ người nhập khẩu (tên đường,...).
		/// </summary>
		public string CAT
		{
			get
			{
				return m_CAT;
			}
			set
			{
				if ((this.m_CAT != value))
				{
					this.SendPropertyChanging("CAT");
					this.m_CAT = value;
					this.SendPropertyChanged("CAT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_CATUpdated = true;
				}
			}
		}

		private string m_CAC;
		private bool m_CACUpdated = false;
		/// <summary>
		/// Địa chỉ người nhập khẩu (thành phố,...).
		/// </summary>
		public string CAC
		{
			get
			{
				return m_CAC;
			}
			set
			{
				if ((this.m_CAC != value))
				{
					this.SendPropertyChanging("CAC");
					this.m_CAC = value;
					this.SendPropertyChanged("CAC");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_CACUpdated = true;
				}
			}
		}

		private string m_EP3;
		private bool m_EP3Updated = false;
		/// <summary>
		/// Địa chỉ người xuất khẩu (thành phố,...).
		/// </summary>
		public string EP3
		{
			get
			{
				return m_EP3;
			}
			set
			{
				if ((this.m_EP3 != value))
				{
					this.SendPropertyChanging("EP3");
					this.m_EP3 = value;
					this.SendPropertyChanged("EP3");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_EP3Updated = true;
				}
			}
		}

		private string m_CAS;
		private bool m_CASUpdated = false;
		/// <summary>
		/// Địa chỉ người nhập khẩu (tên nước,...).
		/// </summary>
		public string CAS
		{
			get
			{
				return m_CAS;
			}
			set
			{
				if ((this.m_CAS != value))
				{
					this.SendPropertyChanging("CAS");
					this.m_CAS = value;
					this.SendPropertyChanged("CAS");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_CASUpdated = true;
				}
			}
		}

		private string m_EP4;
		private bool m_EP4Updated = false;
		/// <summary>
		/// Địa chỉ người xuất khẩu (tên nước,...).
		/// </summary>
		public string EP4
		{
			get
			{
				return m_EP4;
			}
			set
			{
				if ((this.m_EP4 != value))
				{
					this.SendPropertyChanging("EP4");
					this.m_EP4 = value;
					this.SendPropertyChanged("EP4");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_EP4Updated = true;
				}
			}
		}

		private string m_CGK;
		private bool m_CGKUpdated = false;
		/// <summary>
		/// Mã nước người nhập khẩu.
		/// </summary>
		public string CGK
		{
			get
			{
				return m_CGK;
			}
			set
			{
				if ((this.m_CGK != value))
				{
					this.SendPropertyChanging("CGK");
					this.m_CGK = value;
					this.SendPropertyChanged("CGK");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_CGKUpdated = true;
				}
			}
		}

		private string m_EPO;
		private bool m_EPOUpdated = false;
		/// <summary>
		/// Mã nước của người xuất khẩu (mã nước xuất khẩu).
		/// </summary>
		public string EPO
		{
			get
			{
				return m_EPO;
			}
			set
			{
				if ((this.m_EPO != value))
				{
					this.SendPropertyChanging("EPO");
					this.m_EPO = value;
					this.SendPropertyChanged("EPO");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_EPOUpdated = true;
				}
			}
		}

		private string m_AWB;
		private bool m_AWBUpdated = false;
		/// <summary>
		/// Nhập số vận đơn HAWB.
		/// </summary>
		public string AWB
		{
			get
			{
				return m_AWB;
			}
			set
			{
				if ((this.m_AWB != value))
				{
					this.SendPropertyChanging("AWB");
					this.m_AWB = value;
					this.SendPropertyChanged("AWB");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_AWBUpdated = true;
				}
			}
		}

		private string m_MAB;
		private bool m_MABUpdated = false;
		/// <summary>
		/// Số master bill.
		/// </summary>
		public string MAB
		{
			get
			{
				return m_MAB;
			}
			set
			{
				if ((this.m_MAB != value))
				{
					this.SendPropertyChanging("MAB");
					this.m_MAB = value;
					this.SendPropertyChanged("MAB");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_MABUpdated = true;
				}
			}
		}

		private decimal? m_NO = 0;
		private bool m_NOUpdated = false;
		/// <summary>
		/// Số lượng.
		/// </summary>
		public decimal? NO
		{
			get
			{
				return m_NO;
			}
			set
			{
				if ((this.m_NO != value))
				{
					this.SendPropertyChanging("NO");
					this.m_NO = value;
					this.SendPropertyChanged("NO");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NOUpdated = true;
				}
			}
		}

		private decimal? m_GW = 0;
		private bool m_GWUpdated = false;
		/// <summary>
		/// Tổng trọng lượng.
		/// </summary>
		public decimal? GW
		{
			get
			{
				return m_GW;
			}
			set
			{
				if ((this.m_GW != value))
				{
					this.SendPropertyChanging("GW");
					this.m_GW = value;
					this.SendPropertyChanged("GW");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_GWUpdated = true;
				}
			}
		}

		private string m_ST;
		private bool m_STUpdated = false;
		/// <summary>
		/// Mã địa điểm lưu kho dự kiến.
		/// </summary>
		public string ST
		{
			get
			{
				return m_ST;
			}
			set
			{
				if ((this.m_ST != value))
				{
					this.SendPropertyChanging("ST");
					this.m_ST = value;
					this.SendPropertyChanged("ST");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_STUpdated = true;
				}
			}
		}

		private string m_DSC;
		private bool m_DSCUpdated = false;
		/// <summary>
		/// Mã địa điểm nhận hàng cuối cùng.
		/// </summary>
		public string DSC
		{
			get
			{
				return m_DSC;
			}
			set
			{
				if ((this.m_DSC != value))
				{
					this.SendPropertyChanging("DSC");
					this.m_DSC = value;
					this.SendPropertyChanged("DSC");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DSCUpdated = true;
				}
			}
		}

		private string m_PSC;
		private bool m_PSCUpdated = false;
		/// <summary>
		/// Mã địa điểm xếp hàng.
		/// </summary>
		public string PSC
		{
			get
			{
				return m_PSC;
			}
			set
			{
				if ((this.m_PSC != value))
				{
					this.SendPropertyChanging("PSC");
					this.m_PSC = value;
					this.SendPropertyChanged("PSC");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_PSCUpdated = true;
				}
			}
		}

		private string m_FCD;
		private bool m_FCDUpdated = false;
		/// <summary>
		/// Nhập mã đơn vị tiền tệ của hóa đơn (giá FOB) theo chuẩn UN/LOCODE.
		/// </summary>
		public string FCD
		{
			get
			{
				return m_FCD;
			}
			set
			{
				if ((this.m_FCD != value))
				{
					this.SendPropertyChanging("FCD");
					this.m_FCD = value;
					this.SendPropertyChanged("FCD");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_FCDUpdated = true;
				}
			}
		}

		private string m_FKK;
		private bool m_FKKUpdated = false;
		/// <summary>
		/// Nhập giá hóa đơn (giá FOB).
		/// </summary>
		public string FKK
		{
			get
			{
				return m_FKK;
			}
			set
			{
				if ((this.m_FKK != value))
				{
					this.SendPropertyChanging("FKK");
					this.m_FKK = value;
					this.SendPropertyChanged("FKK");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_FKKUpdated = true;
				}
			}
		}

		private string m_SKK;
		private bool m_SKKUpdated = false;
		/// <summary>
		/// Nhập giá khai báo.
		/// </summary>
		public string SKK
		{
			get
			{
				return m_SKK;
			}
			set
			{
				if ((this.m_SKK != value))
				{
					this.SendPropertyChanging("SKK");
					this.m_SKK = value;
					this.SendPropertyChanged("SKK");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_SKKUpdated = true;
				}
			}
		}

		private string m_REF;
		private bool m_REFUpdated = false;
		/// <summary>
		/// Mã hàng (mã quản lý nội bộ của doanh nghiệp).
		/// </summary>
		public string REF
		{
			get
			{
				return m_REF;
			}
			set
			{
				if ((this.m_REF != value))
				{
					this.SendPropertyChanging("REF");
					this.m_REF = value;
					this.SendPropertyChanged("REF");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_REFUpdated = true;
				}
			}
		}

		private string m_CMN;
		private bool m_CMNUpdated = false;
		/// <summary>
		/// Mô tả hàng.
		/// </summary>
		public string CMN
		{
			get
			{
				return m_CMN;
			}
			set
			{
				if ((this.m_CMN != value))
				{
					this.SendPropertyChanging("CMN");
					this.m_CMN = value;
					this.SendPropertyChanged("CMN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_CMNUpdated = true;
				}
			}
		}

		private string m_NT;
		private bool m_NTUpdated = false;
		/// <summary>
		/// Phần ghi chú cho hàng hóa.
		/// </summary>
		public string NT
		{
			get
			{
				return m_NT;
			}
			set
			{
				if ((this.m_NT != value))
				{
					this.SendPropertyChanging("NT");
					this.m_NT = value;
					this.SendPropertyChanged("NT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NTUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM MEC " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[TK_ID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[ECN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[JKN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[CH]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[CHB]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[EPC]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[EPN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[EPP]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[EPA]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[EPT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[CGC]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[CGN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[CGP]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[CGA]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[CAT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[CAC]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[EP3]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[CAS]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[EP4]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[CGK]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[EPO]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[AWB]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[MAB]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[NO]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[GW]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[ST]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DSC]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[PSC]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[FCD]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[FKK]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[SKK]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[REF]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[CMN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[NT]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("TK_ID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("ECN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("JKN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("CH", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("CHB", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("EPC", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("EPN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("EPP", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("EPA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("EPT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("CGC", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("CGN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("CGP", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("CGA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("CAT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("CAC", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("EP3", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("CAS", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("EP4", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("CGK", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("EPO", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("AWB", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("MAB", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NO", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("GW", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("ST", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DSC", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("PSC", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("FCD", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("FKK", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("SKK", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("REF", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("CMN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NT", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO MEC ([TK_ID], [ECN], [JKN], [CH], [CHB], [EPC], [EPN], [EPP], [EPA], [EPT], [CGC], [CGN], [CGP], [CGA], [CAT], [CAC], [EP3], [CAS], [EP4], [CGK], [EPO], [AWB], [MAB], [NO], [GW], [ST], [DSC], [PSC], [FCD], [FKK], [SKK], [REF], [CMN], [NT]) VALUES(").Append("@TK_ID").Append(",").Append("@ECN").Append(",").Append("@JKN").Append(",").Append("@CH").Append(",").Append("@CHB").Append(",").Append("@EPC").Append(",").Append("@EPN").Append(",").Append("@EPP").Append(",").Append("@EPA").Append(",").Append("@EPT").Append(",").Append("@CGC").Append(",").Append("@CGN").Append(",").Append("@CGP").Append(",").Append("@CGA").Append(",").Append("@CAT").Append(",").Append("@CAC").Append(",").Append("@EP3").Append(",").Append("@CAS").Append(",").Append("@EP4").Append(",").Append("@CGK").Append(",").Append("@EPO").Append(",").Append("@AWB").Append(",").Append("@MAB").Append(",").Append("@NO").Append(",").Append("@GW").Append(",").Append("@ST").Append(",").Append("@DSC").Append(",").Append("@PSC").Append(",").Append("@FCD").Append(",").Append("@FKK").Append(",").Append("@SKK").Append(",").Append("@REF").Append(",").Append("@CMN").Append(",").Append("@NT").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO MEC (TK_ID, ECN, JKN, CH, CHB, EPC, EPN, EPP, EPA, EPT, CGC, CGN, CGP, CGA, CAT, CAC, EP3, CAS, EP4, CGK, EPO, AWB, MAB, NO, GW, ST, DSC, PSC, FCD, FKK, SKK, REF, CMN, NT) VALUES(").Append(":TK_ID").Append(",").Append(":ECN").Append(",").Append(":JKN").Append(",").Append(":CH").Append(",").Append(":CHB").Append(",").Append(":EPC").Append(",").Append(":EPN").Append(",").Append(":EPP").Append(",").Append(":EPA").Append(",").Append(":EPT").Append(",").Append(":CGC").Append(",").Append(":CGN").Append(",").Append(":CGP").Append(",").Append(":CGA").Append(",").Append(":CAT").Append(",").Append(":CAC").Append(",").Append(":EP3").Append(",").Append(":CAS").Append(",").Append(":EP4").Append(",").Append(":CGK").Append(",").Append(":EPO").Append(",").Append(":AWB").Append(",").Append(":MAB").Append(",").Append(":NO").Append(",").Append(":GW").Append(",").Append(":ST").Append(",").Append(":DSC").Append(",").Append(":PSC").Append(",").Append(":FCD").Append(",").Append(":FKK").Append(",").Append(":SKK").Append(",").Append(":REF").Append(",").Append(":CMN").Append(",").Append(":NT").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE MEC SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_ECNUpdated ? string.Format(",[ECN] = {0}", "@ECN") : string.Empty).Append(m_JKNUpdated ? string.Format(",[JKN] = {0}", "@JKN") : string.Empty).Append(m_CHUpdated ? string.Format(",[CH] = {0}", "@CH") : string.Empty).Append(m_CHBUpdated ? string.Format(",[CHB] = {0}", "@CHB") : string.Empty).Append(m_EPCUpdated ? string.Format(",[EPC] = {0}", "@EPC") : string.Empty).Append(m_EPNUpdated ? string.Format(",[EPN] = {0}", "@EPN") : string.Empty).Append(m_EPPUpdated ? string.Format(",[EPP] = {0}", "@EPP") : string.Empty).Append(m_EPAUpdated ? string.Format(",[EPA] = {0}", "@EPA") : string.Empty).Append(m_EPTUpdated ? string.Format(",[EPT] = {0}", "@EPT") : string.Empty).Append(m_CGCUpdated ? string.Format(",[CGC] = {0}", "@CGC") : string.Empty).Append(m_CGNUpdated ? string.Format(",[CGN] = {0}", "@CGN") : string.Empty).Append(m_CGPUpdated ? string.Format(",[CGP] = {0}", "@CGP") : string.Empty).Append(m_CGAUpdated ? string.Format(",[CGA] = {0}", "@CGA") : string.Empty).Append(m_CATUpdated ? string.Format(",[CAT] = {0}", "@CAT") : string.Empty).Append(m_CACUpdated ? string.Format(",[CAC] = {0}", "@CAC") : string.Empty).Append(m_EP3Updated ? string.Format(",[EP3] = {0}", "@EP3") : string.Empty).Append(m_CASUpdated ? string.Format(",[CAS] = {0}", "@CAS") : string.Empty).Append(m_EP4Updated ? string.Format(",[EP4] = {0}", "@EP4") : string.Empty).Append(m_CGKUpdated ? string.Format(",[CGK] = {0}", "@CGK") : string.Empty).Append(m_EPOUpdated ? string.Format(",[EPO] = {0}", "@EPO") : string.Empty).Append(m_AWBUpdated ? string.Format(",[AWB] = {0}", "@AWB") : string.Empty).Append(m_MABUpdated ? string.Format(",[MAB] = {0}", "@MAB") : string.Empty).Append(m_NOUpdated ? string.Format(",[NO] = {0}", "@NO") : string.Empty).Append(m_GWUpdated ? string.Format(",[GW] = {0}", "@GW") : string.Empty).Append(m_STUpdated ? string.Format(",[ST] = {0}", "@ST") : string.Empty).Append(m_DSCUpdated ? string.Format(",[DSC] = {0}", "@DSC") : string.Empty).Append(m_PSCUpdated ? string.Format(",[PSC] = {0}", "@PSC") : string.Empty).Append(m_FCDUpdated ? string.Format(",[FCD] = {0}", "@FCD") : string.Empty).Append(m_FKKUpdated ? string.Format(",[FKK] = {0}", "@FKK") : string.Empty).Append(m_SKKUpdated ? string.Format(",[SKK] = {0}", "@SKK") : string.Empty).Append(m_REFUpdated ? string.Format(",[REF] = {0}", "@REF") : string.Empty).Append(m_CMNUpdated ? string.Format(",[CMN] = {0}", "@CMN") : string.Empty).Append(m_NTUpdated ? string.Format(",[NT] = {0}", "@NT") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_ECNUpdated ? string.Format(",ECN = {0}", ":ECN") : string.Empty).Append(m_JKNUpdated ? string.Format(",JKN = {0}", ":JKN") : string.Empty).Append(m_CHUpdated ? string.Format(",CH = {0}", ":CH") : string.Empty).Append(m_CHBUpdated ? string.Format(",CHB = {0}", ":CHB") : string.Empty).Append(m_EPCUpdated ? string.Format(",EPC = {0}", ":EPC") : string.Empty).Append(m_EPNUpdated ? string.Format(",EPN = {0}", ":EPN") : string.Empty).Append(m_EPPUpdated ? string.Format(",EPP = {0}", ":EPP") : string.Empty).Append(m_EPAUpdated ? string.Format(",EPA = {0}", ":EPA") : string.Empty).Append(m_EPTUpdated ? string.Format(",EPT = {0}", ":EPT") : string.Empty).Append(m_CGCUpdated ? string.Format(",CGC = {0}", ":CGC") : string.Empty).Append(m_CGNUpdated ? string.Format(",CGN = {0}", ":CGN") : string.Empty).Append(m_CGPUpdated ? string.Format(",CGP = {0}", ":CGP") : string.Empty).Append(m_CGAUpdated ? string.Format(",CGA = {0}", ":CGA") : string.Empty).Append(m_CATUpdated ? string.Format(",CAT = {0}", ":CAT") : string.Empty).Append(m_CACUpdated ? string.Format(",CAC = {0}", ":CAC") : string.Empty).Append(m_EP3Updated ? string.Format(",EP3 = {0}", ":EP3") : string.Empty).Append(m_CASUpdated ? string.Format(",CAS = {0}", ":CAS") : string.Empty).Append(m_EP4Updated ? string.Format(",EP4 = {0}", ":EP4") : string.Empty).Append(m_CGKUpdated ? string.Format(",CGK = {0}", ":CGK") : string.Empty).Append(m_EPOUpdated ? string.Format(",EPO = {0}", ":EPO") : string.Empty).Append(m_AWBUpdated ? string.Format(",AWB = {0}", ":AWB") : string.Empty).Append(m_MABUpdated ? string.Format(",MAB = {0}", ":MAB") : string.Empty).Append(m_NOUpdated ? string.Format(",NO = {0}", ":NO") : string.Empty).Append(m_GWUpdated ? string.Format(",GW = {0}", ":GW") : string.Empty).Append(m_STUpdated ? string.Format(",ST = {0}", ":ST") : string.Empty).Append(m_DSCUpdated ? string.Format(",DSC = {0}", ":DSC") : string.Empty).Append(m_PSCUpdated ? string.Format(",PSC = {0}", ":PSC") : string.Empty).Append(m_FCDUpdated ? string.Format(",FCD = {0}", ":FCD") : string.Empty).Append(m_FKKUpdated ? string.Format(",FKK = {0}", ":FKK") : string.Empty).Append(m_SKKUpdated ? string.Format(",SKK = {0}", ":SKK") : string.Empty).Append(m_REFUpdated ? string.Format(",REF = {0}", ":REF") : string.Empty).Append(m_CMNUpdated ? string.Format(",CMN = {0}", ":CMN") : string.Empty).Append(m_NTUpdated ? string.Format(",NT = {0}", ":NT") : string.Empty);
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
				sbSQL.AppendFormat("[ECN] = {0}", "@ECN").AppendFormat(",[JKN] = {0}", "@JKN").AppendFormat(",[CH] = {0}", "@CH").AppendFormat(",[CHB] = {0}", "@CHB").AppendFormat(",[EPC] = {0}", "@EPC").AppendFormat(",[EPN] = {0}", "@EPN").AppendFormat(",[EPP] = {0}", "@EPP").AppendFormat(",[EPA] = {0}", "@EPA").AppendFormat(",[EPT] = {0}", "@EPT").AppendFormat(",[CGC] = {0}", "@CGC").AppendFormat(",[CGN] = {0}", "@CGN").AppendFormat(",[CGP] = {0}", "@CGP").AppendFormat(",[CGA] = {0}", "@CGA").AppendFormat(",[CAT] = {0}", "@CAT").AppendFormat(",[CAC] = {0}", "@CAC").AppendFormat(",[EP3] = {0}", "@EP3").AppendFormat(",[CAS] = {0}", "@CAS").AppendFormat(",[EP4] = {0}", "@EP4").AppendFormat(",[CGK] = {0}", "@CGK").AppendFormat(",[EPO] = {0}", "@EPO").AppendFormat(",[AWB] = {0}", "@AWB").AppendFormat(",[MAB] = {0}", "@MAB").AppendFormat(",[NO] = {0}", "@NO").AppendFormat(",[GW] = {0}", "@GW").AppendFormat(",[ST] = {0}", "@ST").AppendFormat(",[DSC] = {0}", "@DSC").AppendFormat(",[PSC] = {0}", "@PSC").AppendFormat(",[FCD] = {0}", "@FCD").AppendFormat(",[FKK] = {0}", "@FKK").AppendFormat(",[SKK] = {0}", "@SKK").AppendFormat(",[REF] = {0}", "@REF").AppendFormat(",[CMN] = {0}", "@CMN").AppendFormat(",[NT] = {0}", "@NT");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("ECN = {0}", ":ECN").AppendFormat(",JKN = {0}", ":JKN").AppendFormat(",CH = {0}", ":CH").AppendFormat(",CHB = {0}", ":CHB").AppendFormat(",EPC = {0}", ":EPC").AppendFormat(",EPN = {0}", ":EPN").AppendFormat(",EPP = {0}", ":EPP").AppendFormat(",EPA = {0}", ":EPA").AppendFormat(",EPT = {0}", ":EPT").AppendFormat(",CGC = {0}", ":CGC").AppendFormat(",CGN = {0}", ":CGN").AppendFormat(",CGP = {0}", ":CGP").AppendFormat(",CGA = {0}", ":CGA").AppendFormat(",CAT = {0}", ":CAT").AppendFormat(",CAC = {0}", ":CAC").AppendFormat(",EP3 = {0}", ":EP3").AppendFormat(",CAS = {0}", ":CAS").AppendFormat(",EP4 = {0}", ":EP4").AppendFormat(",CGK = {0}", ":CGK").AppendFormat(",EPO = {0}", ":EPO").AppendFormat(",AWB = {0}", ":AWB").AppendFormat(",MAB = {0}", ":MAB").AppendFormat(",NO = {0}", ":NO").AppendFormat(",GW = {0}", ":GW").AppendFormat(",ST = {0}", ":ST").AppendFormat(",DSC = {0}", ":DSC").AppendFormat(",PSC = {0}", ":PSC").AppendFormat(",FCD = {0}", ":FCD").AppendFormat(",FKK = {0}", ":FKK").AppendFormat(",SKK = {0}", ":SKK").AppendFormat(",REF = {0}", ":REF").AppendFormat(",CMN = {0}", ":CMN").AppendFormat(",NT = {0}", ":NT");
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
			return clsDAL.DeleteString("MEC", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.AppendFormat("[TK_ID] = {0}", "@TK_ID");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("TK_ID = {0}", ":TK_ID");
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
			paramList.Add(clsDAL.CreateParameter("TK_ID", "Integer", clsDAL.ToDBParam(TK_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("ECN", "WChar", clsDAL.ToDBParam(ECN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("JKN", "WChar", clsDAL.ToDBParam(JKN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CH", "WChar", clsDAL.ToDBParam(CH, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CHB", "WChar", clsDAL.ToDBParam(CHB, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EPC", "WChar", clsDAL.ToDBParam(EPC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EPN", "WChar", clsDAL.ToDBParam(EPN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EPP", "WChar", clsDAL.ToDBParam(EPP, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EPA", "WChar", clsDAL.ToDBParam(EPA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EPT", "WChar", clsDAL.ToDBParam(EPT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CGC", "WChar", clsDAL.ToDBParam(CGC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CGN", "WChar", clsDAL.ToDBParam(CGN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CGP", "WChar", clsDAL.ToDBParam(CGP, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CGA", "WChar", clsDAL.ToDBParam(CGA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CAT", "WChar", clsDAL.ToDBParam(CAT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CAC", "WChar", clsDAL.ToDBParam(CAC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EP3", "WChar", clsDAL.ToDBParam(EP3, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CAS", "WChar", clsDAL.ToDBParam(CAS, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EP4", "WChar", clsDAL.ToDBParam(EP4, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CGK", "WChar", clsDAL.ToDBParam(CGK, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EPO", "WChar", clsDAL.ToDBParam(EPO, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("AWB", "WChar", clsDAL.ToDBParam(AWB, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("MAB", "WChar", clsDAL.ToDBParam(MAB, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NO", "Numeric", clsDAL.ToDBParam(NO, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("GW", "Numeric", clsDAL.ToDBParam(GW, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("ST", "WChar", clsDAL.ToDBParam(ST, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DSC", "WChar", clsDAL.ToDBParam(DSC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PSC", "WChar", clsDAL.ToDBParam(PSC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("FCD", "WChar", clsDAL.ToDBParam(FCD, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("FKK", "WChar", clsDAL.ToDBParam(FKK, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SKK", "WChar", clsDAL.ToDBParam(SKK, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("REF", "WChar", clsDAL.ToDBParam(REF, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CMN", "WChar", clsDAL.ToDBParam(CMN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NT", "WChar", clsDAL.ToDBParam(NT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("TK_ID", "Integer", clsDAL.ToDBParam(TK_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("TK_ID", "Integer", clsDAL.ToDBParam(TK_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("ECN", "WChar", clsDAL.ToDBParam(ECN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("JKN", "WChar", clsDAL.ToDBParam(JKN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CH", "WChar", clsDAL.ToDBParam(CH, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CHB", "WChar", clsDAL.ToDBParam(CHB, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EPC", "WChar", clsDAL.ToDBParam(EPC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EPN", "WChar", clsDAL.ToDBParam(EPN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EPP", "WChar", clsDAL.ToDBParam(EPP, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EPA", "WChar", clsDAL.ToDBParam(EPA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EPT", "WChar", clsDAL.ToDBParam(EPT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CGC", "WChar", clsDAL.ToDBParam(CGC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CGN", "WChar", clsDAL.ToDBParam(CGN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CGP", "WChar", clsDAL.ToDBParam(CGP, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CGA", "WChar", clsDAL.ToDBParam(CGA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CAT", "WChar", clsDAL.ToDBParam(CAT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CAC", "WChar", clsDAL.ToDBParam(CAC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EP3", "WChar", clsDAL.ToDBParam(EP3, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CAS", "WChar", clsDAL.ToDBParam(CAS, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EP4", "WChar", clsDAL.ToDBParam(EP4, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CGK", "WChar", clsDAL.ToDBParam(CGK, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EPO", "WChar", clsDAL.ToDBParam(EPO, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("AWB", "WChar", clsDAL.ToDBParam(AWB, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("MAB", "WChar", clsDAL.ToDBParam(MAB, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NO", "Numeric", clsDAL.ToDBParam(NO, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("GW", "Numeric", clsDAL.ToDBParam(GW, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("ST", "WChar", clsDAL.ToDBParam(ST, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DSC", "WChar", clsDAL.ToDBParam(DSC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PSC", "WChar", clsDAL.ToDBParam(PSC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("FCD", "WChar", clsDAL.ToDBParam(FCD, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("FKK", "WChar", clsDAL.ToDBParam(FKK, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SKK", "WChar", clsDAL.ToDBParam(SKK, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("REF", "WChar", clsDAL.ToDBParam(REF, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CMN", "WChar", clsDAL.ToDBParam(CMN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NT", "WChar", clsDAL.ToDBParam(NT, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}