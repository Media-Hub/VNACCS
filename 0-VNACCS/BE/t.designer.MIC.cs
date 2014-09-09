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
	/// Generated Class for Table : MIC.
	/// </summary>
	public partial class MIC : TableBase
	{
		public MIC() : base(){}

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

		private string m_ICN;
		private bool m_ICNUpdated = false;
		/// <summary>
		/// Số tờ khai.
		/// </summary>
		public string ICN
		{
			get
			{
				return m_ICN;
			}
			set
			{
				if ((this.m_ICN != value))
				{
					this.SendPropertyChanging("ICN");
					this.m_ICN = value;
					this.SendPropertyChanged("ICN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_ICNUpdated = true;
				}
			}
		}

		private string m_SKB;
		private bool m_SKBUpdated = false;
		/// <summary>
		/// Mã phân loại cá nhân tổ chức.
		/// </summary>
		public string SKB
		{
			get
			{
				return m_SKB;
			}
			set
			{
				if ((this.m_SKB != value))
				{
					this.SendPropertyChanging("SKB");
					this.m_SKB = value;
					this.SendPropertyChanged("SKB");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_SKBUpdated = true;
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

		private string m_IMC;
		private bool m_IMCUpdated = false;
		/// <summary>
		/// Mã người nhập khẩu.
		/// </summary>
		public string IMC
		{
			get
			{
				return m_IMC;
			}
			set
			{
				if ((this.m_IMC != value))
				{
					this.SendPropertyChanging("IMC");
					this.m_IMC = value;
					this.SendPropertyChanged("IMC");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_IMCUpdated = true;
				}
			}
		}

		private string m_IMN;
		private bool m_IMNUpdated = false;
		/// <summary>
		/// Tên người nhập khẩu.
		/// </summary>
		public string IMN
		{
			get
			{
				return m_IMN;
			}
			set
			{
				if ((this.m_IMN != value))
				{
					this.SendPropertyChanging("IMN");
					this.m_IMN = value;
					this.SendPropertyChanged("IMN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_IMNUpdated = true;
				}
			}
		}

		private string m_IMY;
		private bool m_IMYUpdated = false;
		/// <summary>
		/// Mã bưu chính người nhập khẩu.
		/// </summary>
		public string IMY
		{
			get
			{
				return m_IMY;
			}
			set
			{
				if ((this.m_IMY != value))
				{
					this.SendPropertyChanging("IMY");
					this.m_IMY = value;
					this.SendPropertyChanged("IMY");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_IMYUpdated = true;
				}
			}
		}

		private string m_IMA;
		private bool m_IMAUpdated = false;
		/// <summary>
		/// Địa chỉ người nhập khẩu.
		/// </summary>
		public string IMA
		{
			get
			{
				return m_IMA;
			}
			set
			{
				if ((this.m_IMA != value))
				{
					this.SendPropertyChanging("IMA");
					this.m_IMA = value;
					this.SendPropertyChanged("IMA");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_IMAUpdated = true;
				}
			}
		}

		private string m_IMT;
		private bool m_IMTUpdated = false;
		/// <summary>
		/// Điện thoại người nhập khẩu.
		/// </summary>
		public string IMT
		{
			get
			{
				return m_IMT;
			}
			set
			{
				if ((this.m_IMT != value))
				{
					this.SendPropertyChanging("IMT");
					this.m_IMT = value;
					this.SendPropertyChanged("IMT");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_IMTUpdated = true;
				}
			}
		}

		private string m_EPC;
		private bool m_EPCUpdated = false;
		/// <summary>
		/// Mã người xuất khẩu.
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

		private string m_EPY;
		private bool m_EPYUpdated = false;
		/// <summary>
		/// Mã bưu chính người xuất khẩu.
		/// </summary>
		public string EPY
		{
			get
			{
				return m_EPY;
			}
			set
			{
				if ((this.m_EPY != value))
				{
					this.SendPropertyChanging("EPY");
					this.m_EPY = value;
					this.SendPropertyChanged("EPY");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_EPYUpdated = true;
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

		private string m_EP2;
		private bool m_EP2Updated = false;
		/// <summary>
		/// Địa chỉ người xuất khẩu (tên đường,...).
		/// </summary>
		public string EP2
		{
			get
			{
				return m_EP2;
			}
			set
			{
				if ((this.m_EP2 != value))
				{
					this.SendPropertyChanging("EP2");
					this.m_EP2 = value;
					this.SendPropertyChanged("EP2");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_EP2Updated = true;
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

		private string m_HAB;
		private bool m_HABUpdated = false;
		/// <summary>
		/// Số House AWB.
		/// </summary>
		public string HAB
		{
			get
			{
				return m_HAB;
			}
			set
			{
				if ((this.m_HAB != value))
				{
					this.SendPropertyChanging("HAB");
					this.m_HAB = value;
					this.SendPropertyChanged("HAB");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HABUpdated = true;
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
		/// Địa điểm lưu kho dự kiến.
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

		private string m_VSN;
		private bool m_VSNUpdated = false;
		/// <summary>
		/// Tên máy bay trở hàng.
		/// </summary>
		public string VSN
		{
			get
			{
				return m_VSN;
			}
			set
			{
				if ((this.m_VSN != value))
				{
					this.SendPropertyChanging("VSN");
					this.m_VSN = value;
					this.SendPropertyChanged("VSN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_VSNUpdated = true;
				}
			}
		}

		private DateTime? m_ARR;
		private bool m_ARRUpdated = false;
		/// <summary>
		/// Ngày hàng đến.
		/// </summary>
		public DateTime? ARR
		{
			get
			{
				return m_ARR;
			}
			set
			{
				if ((this.m_ARR != value))
				{
					this.SendPropertyChanging("ARR");
					this.m_ARR = value;
					this.SendPropertyChanged("ARR");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_ARRUpdated = true;
				}
			}
		}

		private string m_DST;
		private bool m_DSTUpdated = false;
		/// <summary>
		/// Địa điểm dỡ hàng (cảng dỡ).
		/// </summary>
		public string DST
		{
			get
			{
				return m_DST;
			}
			set
			{
				if ((this.m_DST != value))
				{
					this.SendPropertyChanging("DST");
					this.m_DST = value;
					this.SendPropertyChanged("DST");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DSTUpdated = true;
				}
			}
		}

		private string m_PSC;
		private bool m_PSCUpdated = false;
		/// <summary>
		/// Địa điểm xếp hàng (cảng xếp).
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

		private string m_IP1;
		private bool m_IP1Updated = false;
		/// <summary>
		/// Mã  phân loại giá hóa đơn.
		/// </summary>
		public string IP1
		{
			get
			{
				return m_IP1;
			}
			set
			{
				if ((this.m_IP1 != value))
				{
					this.SendPropertyChanging("IP1");
					this.m_IP1 = value;
					this.SendPropertyChanged("IP1");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_IP1Updated = true;
				}
			}
		}

		private string m_IP2;
		private bool m_IP2Updated = false;
		/// <summary>
		/// Mã điều kiện giá hóa đơn (điều kiện giao hàng).
		/// </summary>
		public string IP2
		{
			get
			{
				return m_IP2;
			}
			set
			{
				if ((this.m_IP2 != value))
				{
					this.SendPropertyChanging("IP2");
					this.m_IP2 = value;
					this.SendPropertyChanged("IP2");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_IP2Updated = true;
				}
			}
		}

		private string m_IP3;
		private bool m_IP3Updated = false;
		/// <summary>
		/// Mã nguyên tệ trên hóa đơn.
		/// </summary>
		public string IP3
		{
			get
			{
				return m_IP3;
			}
			set
			{
				if ((this.m_IP3 != value))
				{
					this.SendPropertyChanging("IP3");
					this.m_IP3 = value;
					this.SendPropertyChanged("IP3");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_IP3Updated = true;
				}
			}
		}

		private decimal? m_IP4 = 0;
		private bool m_IP4Updated = false;
		/// <summary>
		/// Tổng trị giá hóa đơn.
		/// </summary>
		public decimal? IP4
		{
			get
			{
				return m_IP4;
			}
			set
			{
				if ((this.m_IP4 != value))
				{
					this.SendPropertyChanging("IP4");
					this.m_IP4 = value;
					this.SendPropertyChanged("IP4");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_IP4Updated = true;
				}
			}
		}

		private string m_FR1;
		private bool m_FR1Updated = false;
		/// <summary>
		/// Mã phân loại phí vận chuyển.
		/// </summary>
		public string FR1
		{
			get
			{
				return m_FR1;
			}
			set
			{
				if ((this.m_FR1 != value))
				{
					this.SendPropertyChanging("FR1");
					this.m_FR1 = value;
					this.SendPropertyChanged("FR1");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_FR1Updated = true;
				}
			}
		}

		private string m_FR2;
		private bool m_FR2Updated = false;
		/// <summary>
		/// Mã nguyên tệ tiền vận chuyển.
		/// </summary>
		public string FR2
		{
			get
			{
				return m_FR2;
			}
			set
			{
				if ((this.m_FR2 != value))
				{
					this.SendPropertyChanging("FR2");
					this.m_FR2 = value;
					this.SendPropertyChanged("FR2");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_FR2Updated = true;
				}
			}
		}

		private decimal? m_FR3 = 0;
		private bool m_FR3Updated = false;
		/// <summary>
		/// Phí vận chuyển.
		/// </summary>
		public decimal? FR3
		{
			get
			{
				return m_FR3;
			}
			set
			{
				if ((this.m_FR3 != value))
				{
					this.SendPropertyChanging("FR3");
					this.m_FR3 = value;
					this.SendPropertyChanged("FR3");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_FR3Updated = true;
				}
			}
		}

		private string m_IN1;
		private bool m_IN1Updated = false;
		/// <summary>
		/// Mã phân loại phí bảo hiểm.
		/// </summary>
		public string IN1
		{
			get
			{
				return m_IN1;
			}
			set
			{
				if ((this.m_IN1 != value))
				{
					this.SendPropertyChanging("IN1");
					this.m_IN1 = value;
					this.SendPropertyChanged("IN1");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_IN1Updated = true;
				}
			}
		}

		private string m_IN2;
		private bool m_IN2Updated = false;
		/// <summary>
		/// Mã nguyên tệ phí bảo hiểm.
		/// </summary>
		public string IN2
		{
			get
			{
				return m_IN2;
			}
			set
			{
				if ((this.m_IN2 != value))
				{
					this.SendPropertyChanging("IN2");
					this.m_IN2 = value;
					this.SendPropertyChanged("IN2");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_IN2Updated = true;
				}
			}
		}

		private decimal? m_IN3 = 0;
		private bool m_IN3Updated = false;
		/// <summary>
		/// Phí bảo hiểm.
		/// </summary>
		public decimal? IN3
		{
			get
			{
				return m_IN3;
			}
			set
			{
				if ((this.m_IN3 != value))
				{
					this.SendPropertyChanging("IN3");
					this.m_IN3 = value;
					this.SendPropertyChanged("IN3");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_IN3Updated = true;
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

		private string m_OR;
		private bool m_ORUpdated = false;
		/// <summary>
		/// Nước xuất xứ hàng.
		/// </summary>
		public string OR
		{
			get
			{
				return m_OR;
			}
			set
			{
				if ((this.m_OR != value))
				{
					this.SendPropertyChanging("OR");
					this.m_OR = value;
					this.SendPropertyChanged("OR");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_ORUpdated = true;
				}
			}
		}

		private decimal? m_DPR = 0;
		private bool m_DPRUpdated = false;
		/// <summary>
		/// Trị giá tính thuế.
		/// </summary>
		public decimal? DPR
		{
			get
			{
				return m_DPR;
			}
			set
			{
				if ((this.m_DPR != value))
				{
					this.SendPropertyChanging("DPR");
					this.m_DPR = value;
					this.SendPropertyChanged("DPR");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_DPRUpdated = true;
				}
			}
		}

		private string m_NT1;
		private bool m_NT1Updated = false;
		/// <summary>
		/// Phần ghi chú cho hàng hóa.
		/// </summary>
		public string NT1
		{
			get
			{
				return m_NT1;
			}
			set
			{
				if ((this.m_NT1 != value))
				{
					this.SendPropertyChanging("NT1");
					this.m_NT1 = value;
					this.SendPropertyChanged("NT1");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_NT1Updated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM MIC " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
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
				sbSQL.Append(clsDAL.SelectField("[TK_ID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[ICN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[SKB]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[CH]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[CHB]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[IMC]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[IMN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[IMY]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[IMA]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[IMT]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[EPC]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[EPN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[EPY]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[EPA]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[EP2]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[EP3]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[EP4]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[EPO]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HAB]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[MAB]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[NO]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[GW]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[ST]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[VSN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[ARR]", ProType.DATETIME, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DST]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[PSC]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[IP1]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[IP2]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[IP3]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[IP4]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[FR1]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[FR2]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[FR3]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[IN1]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[IN2]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[IN3]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[REF]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[CMN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[OR]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[DPR]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[NT1]", ProType.OTHER, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("TK_ID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("ICN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("SKB", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("CH", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("CHB", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("IMC", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("IMN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("IMY", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("IMA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("IMT", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("EPC", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("EPN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("EPY", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("EPA", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("EP2", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("EP3", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("EP4", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("EPO", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HAB", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("MAB", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NO", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("GW", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("ST", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("VSN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("ARR", ProType.DATETIME, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DST", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("PSC", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("IP1", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("IP2", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("IP3", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("IP4", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("FR1", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("FR2", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("FR3", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("IN1", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("IN2", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("IN3", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("REF", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("CMN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("OR", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("DPR", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("NT1", ProType.OTHER, this.DataManagement));
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
				sbSQL.Append("INSERT INTO MIC ([TK_ID], [ICN], [SKB], [CH], [CHB], [IMC], [IMN], [IMY], [IMA], [IMT], [EPC], [EPN], [EPY], [EPA], [EP2], [EP3], [EP4], [EPO], [HAB], [MAB], [NO], [GW], [ST], [VSN], [ARR], [DST], [PSC], [IP1], [IP2], [IP3], [IP4], [FR1], [FR2], [FR3], [IN1], [IN2], [IN3], [REF], [CMN], [OR], [DPR], [NT1]) VALUES(").Append("@TK_ID").Append(",").Append("@ICN").Append(",").Append("@SKB").Append(",").Append("@CH").Append(",").Append("@CHB").Append(",").Append("@IMC").Append(",").Append("@IMN").Append(",").Append("@IMY").Append(",").Append("@IMA").Append(",").Append("@IMT").Append(",").Append("@EPC").Append(",").Append("@EPN").Append(",").Append("@EPY").Append(",").Append("@EPA").Append(",").Append("@EP2").Append(",").Append("@EP3").Append(",").Append("@EP4").Append(",").Append("@EPO").Append(",").Append("@HAB").Append(",").Append("@MAB").Append(",").Append("@NO").Append(",").Append("@GW").Append(",").Append("@ST").Append(",").Append("@VSN").Append(",").Append("@ARR").Append(",").Append("@DST").Append(",").Append("@PSC").Append(",").Append("@IP1").Append(",").Append("@IP2").Append(",").Append("@IP3").Append(",").Append("@IP4").Append(",").Append("@FR1").Append(",").Append("@FR2").Append(",").Append("@FR3").Append(",").Append("@IN1").Append(",").Append("@IN2").Append(",").Append("@IN3").Append(",").Append("@REF").Append(",").Append("@CMN").Append(",").Append("@OR").Append(",").Append("@DPR").Append(",").Append("@NT1").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO MIC (TK_ID, ICN, SKB, CH, CHB, IMC, IMN, IMY, IMA, IMT, EPC, EPN, EPY, EPA, EP2, EP3, EP4, EPO, HAB, MAB, NO, GW, ST, VSN, ARR, DST, PSC, IP1, IP2, IP3, IP4, FR1, FR2, FR3, IN1, IN2, IN3, REF, CMN, OR, DPR, NT1) VALUES(").Append(":TK_ID").Append(",").Append(":ICN").Append(",").Append(":SKB").Append(",").Append(":CH").Append(",").Append(":CHB").Append(",").Append(":IMC").Append(",").Append(":IMN").Append(",").Append(":IMY").Append(",").Append(":IMA").Append(",").Append(":IMT").Append(",").Append(":EPC").Append(",").Append(":EPN").Append(",").Append(":EPY").Append(",").Append(":EPA").Append(",").Append(":EP2").Append(",").Append(":EP3").Append(",").Append(":EP4").Append(",").Append(":EPO").Append(",").Append(":HAB").Append(",").Append(":MAB").Append(",").Append(":NO").Append(",").Append(":GW").Append(",").Append(":ST").Append(",").Append(":VSN").Append(",").Append(":ARR").Append(",").Append(":DST").Append(",").Append(":PSC").Append(",").Append(":IP1").Append(",").Append(":IP2").Append(",").Append(":IP3").Append(",").Append(":IP4").Append(",").Append(":FR1").Append(",").Append(":FR2").Append(",").Append(":FR3").Append(",").Append(":IN1").Append(",").Append(":IN2").Append(",").Append(":IN3").Append(",").Append(":REF").Append(",").Append(":CMN").Append(",").Append(":OR").Append(",").Append(":DPR").Append(",").Append(":NT1").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE MIC SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
				sbSQL.Append(m_ICNUpdated ? string.Format(",[ICN] = {0}", "@ICN") : string.Empty).Append(m_SKBUpdated ? string.Format(",[SKB] = {0}", "@SKB") : string.Empty).Append(m_CHUpdated ? string.Format(",[CH] = {0}", "@CH") : string.Empty).Append(m_CHBUpdated ? string.Format(",[CHB] = {0}", "@CHB") : string.Empty).Append(m_IMCUpdated ? string.Format(",[IMC] = {0}", "@IMC") : string.Empty).Append(m_IMNUpdated ? string.Format(",[IMN] = {0}", "@IMN") : string.Empty).Append(m_IMYUpdated ? string.Format(",[IMY] = {0}", "@IMY") : string.Empty).Append(m_IMAUpdated ? string.Format(",[IMA] = {0}", "@IMA") : string.Empty).Append(m_IMTUpdated ? string.Format(",[IMT] = {0}", "@IMT") : string.Empty).Append(m_EPCUpdated ? string.Format(",[EPC] = {0}", "@EPC") : string.Empty).Append(m_EPNUpdated ? string.Format(",[EPN] = {0}", "@EPN") : string.Empty).Append(m_EPYUpdated ? string.Format(",[EPY] = {0}", "@EPY") : string.Empty).Append(m_EPAUpdated ? string.Format(",[EPA] = {0}", "@EPA") : string.Empty).Append(m_EP2Updated ? string.Format(",[EP2] = {0}", "@EP2") : string.Empty).Append(m_EP3Updated ? string.Format(",[EP3] = {0}", "@EP3") : string.Empty).Append(m_EP4Updated ? string.Format(",[EP4] = {0}", "@EP4") : string.Empty).Append(m_EPOUpdated ? string.Format(",[EPO] = {0}", "@EPO") : string.Empty).Append(m_HABUpdated ? string.Format(",[HAB] = {0}", "@HAB") : string.Empty).Append(m_MABUpdated ? string.Format(",[MAB] = {0}", "@MAB") : string.Empty).Append(m_NOUpdated ? string.Format(",[NO] = {0}", "@NO") : string.Empty).Append(m_GWUpdated ? string.Format(",[GW] = {0}", "@GW") : string.Empty).Append(m_STUpdated ? string.Format(",[ST] = {0}", "@ST") : string.Empty).Append(m_VSNUpdated ? string.Format(",[VSN] = {0}", "@VSN") : string.Empty).Append(m_ARRUpdated ? string.Format(",[ARR] = {0}", "@ARR") : string.Empty).Append(m_DSTUpdated ? string.Format(",[DST] = {0}", "@DST") : string.Empty).Append(m_PSCUpdated ? string.Format(",[PSC] = {0}", "@PSC") : string.Empty).Append(m_IP1Updated ? string.Format(",[IP1] = {0}", "@IP1") : string.Empty).Append(m_IP2Updated ? string.Format(",[IP2] = {0}", "@IP2") : string.Empty).Append(m_IP3Updated ? string.Format(",[IP3] = {0}", "@IP3") : string.Empty).Append(m_IP4Updated ? string.Format(",[IP4] = {0}", "@IP4") : string.Empty).Append(m_FR1Updated ? string.Format(",[FR1] = {0}", "@FR1") : string.Empty).Append(m_FR2Updated ? string.Format(",[FR2] = {0}", "@FR2") : string.Empty).Append(m_FR3Updated ? string.Format(",[FR3] = {0}", "@FR3") : string.Empty).Append(m_IN1Updated ? string.Format(",[IN1] = {0}", "@IN1") : string.Empty).Append(m_IN2Updated ? string.Format(",[IN2] = {0}", "@IN2") : string.Empty).Append(m_IN3Updated ? string.Format(",[IN3] = {0}", "@IN3") : string.Empty).Append(m_REFUpdated ? string.Format(",[REF] = {0}", "@REF") : string.Empty).Append(m_CMNUpdated ? string.Format(",[CMN] = {0}", "@CMN") : string.Empty).Append(m_ORUpdated ? string.Format(",[OR] = {0}", "@OR") : string.Empty).Append(m_DPRUpdated ? string.Format(",[DPR] = {0}", "@DPR") : string.Empty).Append(m_NT1Updated ? string.Format(",[NT1] = {0}", "@NT1") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_ICNUpdated ? string.Format(",ICN = {0}", ":ICN") : string.Empty).Append(m_SKBUpdated ? string.Format(",SKB = {0}", ":SKB") : string.Empty).Append(m_CHUpdated ? string.Format(",CH = {0}", ":CH") : string.Empty).Append(m_CHBUpdated ? string.Format(",CHB = {0}", ":CHB") : string.Empty).Append(m_IMCUpdated ? string.Format(",IMC = {0}", ":IMC") : string.Empty).Append(m_IMNUpdated ? string.Format(",IMN = {0}", ":IMN") : string.Empty).Append(m_IMYUpdated ? string.Format(",IMY = {0}", ":IMY") : string.Empty).Append(m_IMAUpdated ? string.Format(",IMA = {0}", ":IMA") : string.Empty).Append(m_IMTUpdated ? string.Format(",IMT = {0}", ":IMT") : string.Empty).Append(m_EPCUpdated ? string.Format(",EPC = {0}", ":EPC") : string.Empty).Append(m_EPNUpdated ? string.Format(",EPN = {0}", ":EPN") : string.Empty).Append(m_EPYUpdated ? string.Format(",EPY = {0}", ":EPY") : string.Empty).Append(m_EPAUpdated ? string.Format(",EPA = {0}", ":EPA") : string.Empty).Append(m_EP2Updated ? string.Format(",EP2 = {0}", ":EP2") : string.Empty).Append(m_EP3Updated ? string.Format(",EP3 = {0}", ":EP3") : string.Empty).Append(m_EP4Updated ? string.Format(",EP4 = {0}", ":EP4") : string.Empty).Append(m_EPOUpdated ? string.Format(",EPO = {0}", ":EPO") : string.Empty).Append(m_HABUpdated ? string.Format(",HAB = {0}", ":HAB") : string.Empty).Append(m_MABUpdated ? string.Format(",MAB = {0}", ":MAB") : string.Empty).Append(m_NOUpdated ? string.Format(",NO = {0}", ":NO") : string.Empty).Append(m_GWUpdated ? string.Format(",GW = {0}", ":GW") : string.Empty).Append(m_STUpdated ? string.Format(",ST = {0}", ":ST") : string.Empty).Append(m_VSNUpdated ? string.Format(",VSN = {0}", ":VSN") : string.Empty).Append(m_ARRUpdated ? string.Format(",ARR = {0}", ":ARR") : string.Empty).Append(m_DSTUpdated ? string.Format(",DST = {0}", ":DST") : string.Empty).Append(m_PSCUpdated ? string.Format(",PSC = {0}", ":PSC") : string.Empty).Append(m_IP1Updated ? string.Format(",IP1 = {0}", ":IP1") : string.Empty).Append(m_IP2Updated ? string.Format(",IP2 = {0}", ":IP2") : string.Empty).Append(m_IP3Updated ? string.Format(",IP3 = {0}", ":IP3") : string.Empty).Append(m_IP4Updated ? string.Format(",IP4 = {0}", ":IP4") : string.Empty).Append(m_FR1Updated ? string.Format(",FR1 = {0}", ":FR1") : string.Empty).Append(m_FR2Updated ? string.Format(",FR2 = {0}", ":FR2") : string.Empty).Append(m_FR3Updated ? string.Format(",FR3 = {0}", ":FR3") : string.Empty).Append(m_IN1Updated ? string.Format(",IN1 = {0}", ":IN1") : string.Empty).Append(m_IN2Updated ? string.Format(",IN2 = {0}", ":IN2") : string.Empty).Append(m_IN3Updated ? string.Format(",IN3 = {0}", ":IN3") : string.Empty).Append(m_REFUpdated ? string.Format(",REF = {0}", ":REF") : string.Empty).Append(m_CMNUpdated ? string.Format(",CMN = {0}", ":CMN") : string.Empty).Append(m_ORUpdated ? string.Format(",OR = {0}", ":OR") : string.Empty).Append(m_DPRUpdated ? string.Format(",DPR = {0}", ":DPR") : string.Empty).Append(m_NT1Updated ? string.Format(",NT1 = {0}", ":NT1") : string.Empty);
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
				sbSQL.AppendFormat("[ICN] = {0}", "@ICN").AppendFormat(",[SKB] = {0}", "@SKB").AppendFormat(",[CH] = {0}", "@CH").AppendFormat(",[CHB] = {0}", "@CHB").AppendFormat(",[IMC] = {0}", "@IMC").AppendFormat(",[IMN] = {0}", "@IMN").AppendFormat(",[IMY] = {0}", "@IMY").AppendFormat(",[IMA] = {0}", "@IMA").AppendFormat(",[IMT] = {0}", "@IMT").AppendFormat(",[EPC] = {0}", "@EPC").AppendFormat(",[EPN] = {0}", "@EPN").AppendFormat(",[EPY] = {0}", "@EPY").AppendFormat(",[EPA] = {0}", "@EPA").AppendFormat(",[EP2] = {0}", "@EP2").AppendFormat(",[EP3] = {0}", "@EP3").AppendFormat(",[EP4] = {0}", "@EP4").AppendFormat(",[EPO] = {0}", "@EPO").AppendFormat(",[HAB] = {0}", "@HAB").AppendFormat(",[MAB] = {0}", "@MAB").AppendFormat(",[NO] = {0}", "@NO").AppendFormat(",[GW] = {0}", "@GW").AppendFormat(",[ST] = {0}", "@ST").AppendFormat(",[VSN] = {0}", "@VSN").AppendFormat(",[ARR] = {0}", "@ARR").AppendFormat(",[DST] = {0}", "@DST").AppendFormat(",[PSC] = {0}", "@PSC").AppendFormat(",[IP1] = {0}", "@IP1").AppendFormat(",[IP2] = {0}", "@IP2").AppendFormat(",[IP3] = {0}", "@IP3").AppendFormat(",[IP4] = {0}", "@IP4").AppendFormat(",[FR1] = {0}", "@FR1").AppendFormat(",[FR2] = {0}", "@FR2").AppendFormat(",[FR3] = {0}", "@FR3").AppendFormat(",[IN1] = {0}", "@IN1").AppendFormat(",[IN2] = {0}", "@IN2").AppendFormat(",[IN3] = {0}", "@IN3").AppendFormat(",[REF] = {0}", "@REF").AppendFormat(",[CMN] = {0}", "@CMN").AppendFormat(",[OR] = {0}", "@OR").AppendFormat(",[DPR] = {0}", "@DPR").AppendFormat(",[NT1] = {0}", "@NT1");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("ICN = {0}", ":ICN").AppendFormat(",SKB = {0}", ":SKB").AppendFormat(",CH = {0}", ":CH").AppendFormat(",CHB = {0}", ":CHB").AppendFormat(",IMC = {0}", ":IMC").AppendFormat(",IMN = {0}", ":IMN").AppendFormat(",IMY = {0}", ":IMY").AppendFormat(",IMA = {0}", ":IMA").AppendFormat(",IMT = {0}", ":IMT").AppendFormat(",EPC = {0}", ":EPC").AppendFormat(",EPN = {0}", ":EPN").AppendFormat(",EPY = {0}", ":EPY").AppendFormat(",EPA = {0}", ":EPA").AppendFormat(",EP2 = {0}", ":EP2").AppendFormat(",EP3 = {0}", ":EP3").AppendFormat(",EP4 = {0}", ":EP4").AppendFormat(",EPO = {0}", ":EPO").AppendFormat(",HAB = {0}", ":HAB").AppendFormat(",MAB = {0}", ":MAB").AppendFormat(",NO = {0}", ":NO").AppendFormat(",GW = {0}", ":GW").AppendFormat(",ST = {0}", ":ST").AppendFormat(",VSN = {0}", ":VSN").AppendFormat(",ARR = {0}", ":ARR").AppendFormat(",DST = {0}", ":DST").AppendFormat(",PSC = {0}", ":PSC").AppendFormat(",IP1 = {0}", ":IP1").AppendFormat(",IP2 = {0}", ":IP2").AppendFormat(",IP3 = {0}", ":IP3").AppendFormat(",IP4 = {0}", ":IP4").AppendFormat(",FR1 = {0}", ":FR1").AppendFormat(",FR2 = {0}", ":FR2").AppendFormat(",FR3 = {0}", ":FR3").AppendFormat(",IN1 = {0}", ":IN1").AppendFormat(",IN2 = {0}", ":IN2").AppendFormat(",IN3 = {0}", ":IN3").AppendFormat(",REF = {0}", ":REF").AppendFormat(",CMN = {0}", ":CMN").AppendFormat(",OR = {0}", ":OR").AppendFormat(",DPR = {0}", ":DPR").AppendFormat(",NT1 = {0}", ":NT1");
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
			return clsDAL.DeleteString("MIC", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
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
			paramList.Add(clsDAL.CreateParameter("ICN", "WChar", clsDAL.ToDBParam(ICN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SKB", "WChar", clsDAL.ToDBParam(SKB, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CH", "WChar", clsDAL.ToDBParam(CH, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CHB", "WChar", clsDAL.ToDBParam(CHB, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("IMC", "WChar", clsDAL.ToDBParam(IMC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("IMN", "WChar", clsDAL.ToDBParam(IMN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("IMY", "WChar", clsDAL.ToDBParam(IMY, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("IMA", "WChar", clsDAL.ToDBParam(IMA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("IMT", "WChar", clsDAL.ToDBParam(IMT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EPC", "WChar", clsDAL.ToDBParam(EPC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EPN", "WChar", clsDAL.ToDBParam(EPN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EPY", "WChar", clsDAL.ToDBParam(EPY, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EPA", "WChar", clsDAL.ToDBParam(EPA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EP2", "WChar", clsDAL.ToDBParam(EP2, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EP3", "WChar", clsDAL.ToDBParam(EP3, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EP4", "WChar", clsDAL.ToDBParam(EP4, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EPO", "WChar", clsDAL.ToDBParam(EPO, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HAB", "WChar", clsDAL.ToDBParam(HAB, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("MAB", "WChar", clsDAL.ToDBParam(MAB, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NO", "Numeric", clsDAL.ToDBParam(NO, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("GW", "Numeric", clsDAL.ToDBParam(GW, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("ST", "WChar", clsDAL.ToDBParam(ST, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("VSN", "WChar", clsDAL.ToDBParam(VSN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("ARR", "Date", clsDAL.ToDBParam(ARR, ProType.DATETIME, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DST", "WChar", clsDAL.ToDBParam(DST, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PSC", "WChar", clsDAL.ToDBParam(PSC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("IP1", "WChar", clsDAL.ToDBParam(IP1, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("IP2", "WChar", clsDAL.ToDBParam(IP2, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("IP3", "WChar", clsDAL.ToDBParam(IP3, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("IP4", "Numeric", clsDAL.ToDBParam(IP4, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("FR1", "WChar", clsDAL.ToDBParam(FR1, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("FR2", "WChar", clsDAL.ToDBParam(FR2, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("FR3", "Numeric", clsDAL.ToDBParam(FR3, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("IN1", "WChar", clsDAL.ToDBParam(IN1, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("IN2", "WChar", clsDAL.ToDBParam(IN2, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("IN3", "Numeric", clsDAL.ToDBParam(IN3, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("REF", "WChar", clsDAL.ToDBParam(REF, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CMN", "WChar", clsDAL.ToDBParam(CMN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("OR", "WChar", clsDAL.ToDBParam(OR, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DPR", "Numeric", clsDAL.ToDBParam(DPR, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NT1", "WChar", clsDAL.ToDBParam(NT1, ProType.STRING, this.DataManagement) , this.DataManagement));
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
			paramList.Add(clsDAL.CreateParameter("ICN", "WChar", clsDAL.ToDBParam(ICN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("SKB", "WChar", clsDAL.ToDBParam(SKB, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CH", "WChar", clsDAL.ToDBParam(CH, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CHB", "WChar", clsDAL.ToDBParam(CHB, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("IMC", "WChar", clsDAL.ToDBParam(IMC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("IMN", "WChar", clsDAL.ToDBParam(IMN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("IMY", "WChar", clsDAL.ToDBParam(IMY, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("IMA", "WChar", clsDAL.ToDBParam(IMA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("IMT", "WChar", clsDAL.ToDBParam(IMT, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EPC", "WChar", clsDAL.ToDBParam(EPC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EPN", "WChar", clsDAL.ToDBParam(EPN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EPY", "WChar", clsDAL.ToDBParam(EPY, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EPA", "WChar", clsDAL.ToDBParam(EPA, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EP2", "WChar", clsDAL.ToDBParam(EP2, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EP3", "WChar", clsDAL.ToDBParam(EP3, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EP4", "WChar", clsDAL.ToDBParam(EP4, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("EPO", "WChar", clsDAL.ToDBParam(EPO, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HAB", "WChar", clsDAL.ToDBParam(HAB, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("MAB", "WChar", clsDAL.ToDBParam(MAB, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NO", "Numeric", clsDAL.ToDBParam(NO, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("GW", "Numeric", clsDAL.ToDBParam(GW, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("ST", "WChar", clsDAL.ToDBParam(ST, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("VSN", "WChar", clsDAL.ToDBParam(VSN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("ARR", "Date", clsDAL.ToDBParam(ARR, ProType.DATETIME, this.DataManagement), this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DST", "WChar", clsDAL.ToDBParam(DST, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("PSC", "WChar", clsDAL.ToDBParam(PSC, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("IP1", "WChar", clsDAL.ToDBParam(IP1, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("IP2", "WChar", clsDAL.ToDBParam(IP2, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("IP3", "WChar", clsDAL.ToDBParam(IP3, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("IP4", "Numeric", clsDAL.ToDBParam(IP4, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("FR1", "WChar", clsDAL.ToDBParam(FR1, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("FR2", "WChar", clsDAL.ToDBParam(FR2, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("FR3", "Numeric", clsDAL.ToDBParam(FR3, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("IN1", "WChar", clsDAL.ToDBParam(IN1, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("IN2", "WChar", clsDAL.ToDBParam(IN2, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("IN3", "Numeric", clsDAL.ToDBParam(IN3, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("REF", "WChar", clsDAL.ToDBParam(REF, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("CMN", "WChar", clsDAL.ToDBParam(CMN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("OR", "WChar", clsDAL.ToDBParam(OR, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("DPR", "Numeric", clsDAL.ToDBParam(DPR, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("NT1", "WChar", clsDAL.ToDBParam(NT1, ProType.STRING, this.DataManagement) , this.DataManagement));
			return paramList;
		}
	}
}