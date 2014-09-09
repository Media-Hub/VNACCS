namespace Naccs.Common.Generator
{
    using Naccs.Common.Function;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    public class ItemInfo
    {
        private XmlDocument correlation;
        private string correlationFileName;
        private DataSet dsItems = new DataSet();
        private HeaderInfoRec HeaderInfo;
        private List<ItemInfoRec> LstItemInfo;
        private List<LastPageInfo> LstLastPageInfo;
        private List<NaccsDataItem> LstNaccsData;
        private DataTable tblCom;
        public static string tblComName = "COM";
        private DataTable tblRan;
        public static string tblRanName = "R01";

        public ItemInfo()
        {
            this.dsItems.DataSetName = "dsJobData";
            this.tblCom = new DataTable();
            this.tblCom.TableName = tblComName;
            this.tblRan = new DataTable();
            this.tblRan.TableName = tblRanName;
            this.LstItemInfo = new List<ItemInfoRec>();
            this.LstNaccsData = new List<NaccsDataItem>();
            this.LstLastPageInfo = new List<LastPageInfo>();
            this.Init();
        }

        private void addNewRowUptoCount(DataTable table, int count)
        {
            while (table.Rows.Count < count)
            {
                DataRow row = table.NewRow();
                table.Rows.Add(row);
            }
        }

        public bool CheckCorrelation(out string[] idArray, out int errorPage, out string msg)
        {
            idArray = null;
            errorPage = -1;
            msg = "";
            if (((this.correlationFileName != null) && (this.correlationFileName != "")) && File.Exists(this.correlationFileName))
            {
                if (this.correlation == null)
                {
                    this.correlation = new XmlDocument();
                    this.correlation.Load(this.correlationFileName);
                }
                foreach (XmlNode node in this.correlation.DocumentElement.ChildNodes)
                {
                    for (int i = 0; i < this.DsItems.Tables.Count; i++)
                    {
                        for (int j = 0; j < this.DsItems.Tables[i].Rows.Count; j++)
                        {
                            if (!this.doCorrelation(this.DsItems.Tables[i].Rows[j], node, out idArray))
                            {
                                if (this.DsItems.Tables[i].TableName == tblComName)
                                {
                                    errorPage = j;
                                }
                                else
                                {
                                    errorPage = j + 1;
                                }
                                msg = node.Attributes["msg"].Value;
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        private bool CheckCorrelationID(string stID)
        {
            if (this.correlation != null)
            {
                foreach (XmlNode node in this.correlation.DocumentElement.ChildNodes)
                {
                    for (int i = 0; i < node.ChildNodes.Count; i++)
                    {
                        if (node.ChildNodes[i].Attributes["id"].Value == stID)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool CheckDataExists(string sTblName, int iPage)
        {
            for (int i = 0; i < this.LstLastPageInfo.Count; i++)
            {
                if ((sTblName == this.LstLastPageInfo[i].Tbl) && (iPage < this.LstLastPageInfo[i].Page))
                {
                    return true;
                }
            }
            return false;
        }

        public void ClearAll()
        {
            if (this.LstLastPageInfo.Count > 0)
            {
                this.LstLastPageInfo.Clear();
            }
            this.ClearCommon();
            for (int i = 0; i < this.DsItems.Tables.Count; i++)
            {
                if (this.DsItems.Tables[i].TableName != tblComName)
                {
                    this.ClearCurrentToEnd(this.DsItems.Tables[i].TableName, 0);
                    this.DsItems.Tables[i].AcceptChanges();
                }
            }
        }

        public void ClearCloseUp(string RepID, int No)
        {
            if (this.LstLastPageInfo.Count > 0)
            {
                for (int j = 0; j < this.LstLastPageInfo.Count; j++)
                {
                    if (this.LstLastPageInfo[j].Tbl == RepID)
                    {
                        this.LstLastPageInfo.Remove(this.LstLastPageInfo[j]);
                    }
                }
            }
            for (int i = 0; i < this.DsItems.Tables[RepID].Columns.Count; i++)
            {
                string columnName = this.DsItems.Tables[RepID].Columns[i].ColumnName;
                int itemRecIdx = this.GetItemRecIdx(columnName);
                if (itemRecIdx > -1)
                {
                    for (int k = No; k < this.DsItems.Tables[RepID].Rows.Count; k++)
                    {
                        if (k == No)
                        {
                            this.DsItems.Tables[RepID].Rows[k][i] = this.LstItemInfo[itemRecIdx].form;
                        }
                        else
                        {
                            this.DsItems.Tables[RepID].Rows[k - 1][i] = this.DsItems.Tables[RepID].Rows[k][i];
                            if (k == (this.DsItems.Tables[RepID].Rows.Count - 1))
                            {
                                this.DsItems.Tables[RepID].Rows[k][i] = this.LstItemInfo[itemRecIdx].form;
                            }
                        }
                    }
                }
            }
            this.DsItems.Tables[RepID].AcceptChanges();
        }

        public void ClearCommon()
        {
            for (int i = 0; i < this.tblCom.Columns.Count; i++)
            {
                this.ClearCommonID(this.tblCom.Columns[i].ColumnName);
            }
            this.tblCom.AcceptChanges();
        }

        public void ClearCommonID(string stID)
        {
            int itemRecIdx = this.GetItemRecIdx(stID);
            if (itemRecIdx > -1)
            {
                this.tblCom.Rows[0][stID] = this.LstItemInfo[itemRecIdx].form;
            }
        }

        public void ClearCurrentToEnd(string RepID, int No)
        {
            if (this.LstLastPageInfo.Count > 0)
            {
                for (int j = 0; j < this.LstLastPageInfo.Count; j++)
                {
                    if (this.LstLastPageInfo[j].Tbl == RepID)
                    {
                        this.LstLastPageInfo.Remove(this.LstLastPageInfo[j]);
                    }
                }
            }
            for (int i = 0; i < this.DsItems.Tables[RepID].Columns.Count; i++)
            {
                string columnName = this.DsItems.Tables[RepID].Columns[i].ColumnName;
                int itemRecIdx = this.GetItemRecIdx(columnName);
                if (itemRecIdx > -1)
                {
                    for (int k = No; k < this.DsItems.Tables[RepID].Rows.Count; k++)
                    {
                        this.DsItems.Tables[RepID].Rows[k][i] = this.LstItemInfo[itemRecIdx].form;
                    }
                }
            }
            this.DsItems.Tables[RepID].AcceptChanges();
        }

        public int CreateDataSet(string FileName)
        {
            XmlReader reader = null;
            try
            {
                if (!File.Exists(FileName))
                {
                    return -2;
                }
                this.Init();
                XmlReaderSettings settings = new XmlReaderSettings {
                    IgnoreComments = true,
                    IgnoreWhitespace = true
                };
                reader = XmlReader.Create(FileName, settings);
                string tblComName = "";
                int count = 1;
                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.EndElement) & (reader.LocalName == "iteminfo"))
                    {
                        this.addNewRowUptoCount(this.dsItems.Tables[tblComName], count);
                        goto Label_03E7;
                    }
                    if ((reader.NodeType == XmlNodeType.Element) & (reader.LocalName == "jobform"))
                    {
                        this.HeaderInfo.jobcode = reader.GetAttribute("jobcode");
                        this.HeaderInfo.jobname = reader.GetAttribute("jobname");
                        this.HeaderInfo.displayname = reader.GetAttribute("display-name");
                    }
                    if ((reader.NodeType == XmlNodeType.Element) & (reader.LocalName == "iteminfo"))
                    {
                        tblComName = Naccs.Common.Generator.ItemInfo.tblComName;
                        count = 1;
                    }
                    else if ((tblComName != "") & (reader.NodeType != XmlNodeType.Whitespace))
                    {
                        if (reader.NodeType == XmlNodeType.EndElement)
                        {
                            this.addNewRowUptoCount(this.dsItems.Tables[tblComName], count);
                            tblComName = Naccs.Common.Generator.ItemInfo.tblComName;
                            count = 1;
                            continue;
                        }
                        if (reader.LocalName == "fixedContainer")
                        {
                            tblComName = reader.GetAttribute("repetition_id");
                            count = int.Parse(reader.GetAttribute("repetition_max"));
                            DataTable table = new DataTable {
                                TableName = tblComName
                            };
                            this.addNewRowUptoCount(table, 1);
                            this.dsItems.Tables.Add(table);
                            continue;
                        }
                        if (reader.LocalName == "ranContainer")
                        {
                            tblComName = tblRanName;
                            count = int.Parse(reader.GetAttribute("repetition_max"));
                            continue;
                        }
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            string attribute = reader.GetAttribute("form");
                            switch (attribute)
                            {
                                case "@D,yyyymmdd":
                                    attribute = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
                                    break;

                                case "@D,ddmmyyyy":
                                    attribute = DateTime.Now.ToString("ddMMyyyy", DateTimeFormatInfo.InvariantInfo);
                                    break;
                            }
                            DataColumn column = new DataColumn(reader.GetAttribute("id"));
                             column.DefaultValue = attribute;
                            this.dsItems.Tables[tblComName].Columns.Add(column);
                            ItemInfoRec item = new ItemInfoRec {
                                id = reader.GetAttribute("id"),
                                name = reader.GetAttribute("name"),
                                input_output = reader.GetAttribute("input_output"),
                                required = reader.GetAttribute("required"),
                                attribute = reader.GetAttribute("attribute"),
                                figure = int.Parse(reader.GetAttribute("figure")),
                                form = attribute,
                                order = long.Parse(reader.GetAttribute("order")),
                                check_attribute = reader.GetAttribute("check_attribute"),
                                check_full = reader.GetAttribute("check_full"),
                                check_date = reader.GetAttribute("check_date"),
                                check_time = reader.GetAttribute("check_time"),
                                choice_keyvalue = reader.GetAttribute("choice_keyvalue"),
                                Out = reader.GetAttribute("out_flg"),
                                In = reader.GetAttribute("in_flg"),
                                Rightpacked = reader.GetAttribute("right_packed"),
                                rep_id = tblComName
                            };
                            this.LstItemInfo.Add(item);
                        }
                    }
                }
            }
            catch (XmlException)
            {
                return -4;
            }
            catch (Exception)
            {
                return -99;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        Label_03E7:
            return 0;
        }

        private DataRow DataRowForm(string RepID)
        {
            DataRow row = this.DsItems.Tables[RepID].NewRow();
            for (int i = 0; i < this.DsItems.Tables[RepID].Columns.Count; i++)
            {
                int itemRecIdx = this.GetItemRecIdx(this.DsItems.Tables[RepID].Columns[i].ColumnName);
                if (itemRecIdx >= 0)
                {
                    row[i] = this.LstItemInfo[itemRecIdx].form;
                }
            }
            return row;
        }

        private int doCheck(int idx, DataRow dataRow, string item_id, out bool dataExist, bool flgCheckReq)
        {
            string s = dataRow[item_id] as string;
            if (s == null)
            {
                s = "";
            }
            else
            {
                s = s.Trim();
            }
            dataExist = s.Length > 0;
            if (((s.Length == 0) && (this.LstItemInfo[idx].required == "M")) && flgCheckReq)
            {
                return -2;
            }
            StrFunc func = StrFunc.CreateInstance();
            if ((this.LstItemInfo[idx].attribute != "W") && !func.ForbiddenCharCheck(s))
            {
                return -3;
            }
            if (((s.Length > 0) && (this.LstItemInfo[idx].check_full == "*")) && (this.LstItemInfo[idx].figure != func.GetByteLength(s)))
            {
                return -4;
            }
            if ((s.Length > 0) && (this.LstItemInfo[idx].check_date == "*"))
            {
                DateTime time;
                if (s.Length != 8)
                {
                    return -6;
                }
                if (!DateTime.TryParseExact(s, "ddMMyyyy", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out time))
                {
                    return -6;
                }
            }
            if ((s.Length > 0) && (this.LstItemInfo[idx].check_time == "*"))
            {
                DateTime time2;
                string format = null;
                if (s.Length == 6)
                {
                    format = "HHmmss";
                }
                else if (s.Length == 4)
                {
                    format = "HHmm";
                }
                else
                {
                    return -7;
                }
                if (!DateTime.TryParseExact(s, format, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out time2))
                {
                    return -7;
                }
            }
            return 0;
        }

        private bool doCorrelation(DataRow datarow, XmlNode checkNode, out string[] idArray)
        {
            idArray = null;
            bool[] flagArray = new bool[checkNode.ChildNodes.Count];
            for (int i = 0; i < checkNode.ChildNodes.Count; i++)
            {
                string columnName = checkNode.ChildNodes[i].Attributes["id"].Value;
                if (datarow.Table.Columns.IndexOf(columnName) < 0)
                {
                    return true;
                }
                string str2 = datarow[columnName] as string;
                flagArray[i] = (str2 != null) && (str2.Trim().Length > 0);
            }
            bool flag = true;
            for (int j = 0; j < flagArray.Length; j++)
            {
                flag = flag && flagArray[j];
            }
            if (flag)
            {
                return true;
            }
            flag = false;
            for (int k = 0; k < flagArray.Length; k++)
            {
                flag = flag || flagArray[k];
            }
            if (!flag)
            {
                return true;
            }
            idArray = new string[1];
            for (int m = 0; m < flagArray.Length; m++)
            {
                if (!flagArray[m])
                {
                    idArray[0] = checkNode.ChildNodes[m].Attributes["id"].Value;
                    break;
                }
            }
            return false;
        }

        public string GetDataList(JobDataStatus jds)
        {
            StringBuilder builder = new StringBuilder();
            string s = "";
            StrFunc func = StrFunc.CreateInstance();
            this.MakeLstNaccsData();
            int lastDataIdx = this.GetLastDataIdx();
            if (lastDataIdx < 0)
            {
                return "";
            }
            for (int i = 0; i <= lastDataIdx; i++)
            {
                if ((this.LstNaccsData[i].In != "0") || (jds != JobDataStatus.jdSend))
                {
                    s = this.LstNaccsData[i].Data;
                    if (this.LstNaccsData[i].Attr.Equals("F"))
                    {
                        s = func.ConvertCommaToPeriod(s);
                    }
                    if (((this.LstNaccsData[i].Attr == "I") || (this.LstNaccsData[i].Attr == "F")) || (this.LstNaccsData[i].Rpacked == "1"))
                    {
                        s = func.FillBlankLeft(s, this.LstNaccsData[i].Figure);
                    }
                    else
                    {
                        s = func.FillBlankRight(s, this.LstNaccsData[i].Figure);
                    }
                    if (builder.Length == 0)
                    {
                        builder.Append(s);
                    }
                    else
                    {
                        builder.Append("\r\n" + s);
                    }
                }
            }
            return builder.ToString();
        }

        public string GetFieldInfo(string stID)
        {
            int itemRecIdx = this.GetItemRecIdx(stID);
            if (itemRecIdx <= -1)
            {
                return null;
            }
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            string id = this.LstItemInfo[itemRecIdx].id;
            string attribute = this.LstItemInfo[itemRecIdx].attribute;
            int figure = this.LstItemInfo[itemRecIdx].figure;
            if ("W".Equals(this.LstItemInfo[itemRecIdx].attribute))
            {
                figure /= 3;
            }
            if (this.LstItemInfo[itemRecIdx].required == "M")
            {
                str = "ON";
            }
            if (this.LstItemInfo[itemRecIdx].check_full == "*")
            {
                str2 = "ON";
            }
            if (this.CheckCorrelationID(id))
            {
                str3 = "ON";
            }
            if (this.LstItemInfo[itemRecIdx].check_date == "*")
            {
                str4 = "ON";
            }
            if (this.LstItemInfo[itemRecIdx].check_time == "*")
            {
                str5 = "ON";
            }
            return ("Item ID\t\t\t" + id + "\n\nAttribute\t\t\t" + attribute + "\n\nDigits\t\t\t" + figure.ToString() + "\n\nMandatory item\t\t" + str + "\n\nCheck equal-length\t\t" + str2 + "\n\nCorrelation check\t\t" + str3 + "\n\nCheck date\t\t\t" + str4 + "\n\nCheck time\t\t\t" + str5);
        }

        public List<string> GetIdData(string stID)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < this.DsItems.Tables.Count; i++)
            {
                for (int j = 0; j < this.DsItems.Tables[i].Columns.Count; j++)
                {
                    if (this.DsItems.Tables[i].Columns[j].ColumnName == stID)
                    {
                        for (int k = 0; k < this.DsItems.Tables[i].Rows.Count; k++)
                        {
                            if (!(this.DsItems.Tables[i].Rows[k][j] is DBNull))
                            {
                                list.Add((string) this.DsItems.Tables[i].Rows[k][j]);
                            }
                        }
                    }
                }
            }
            return list;
        }

        public string GetIdData(string stID, int iNo)
        {
            if (iNo > 0)
            {
                iNo--;
            }
            for (int i = 0; i < this.DsItems.Tables.Count; i++)
            {
                for (int j = 0; j < this.DsItems.Tables[i].Columns.Count; j++)
                {
                    if (this.DsItems.Tables[i].Columns[j].ColumnName == stID)
                    {
                        if (iNo >= this.DsItems.Tables[i].Rows.Count)
                        {
                            return "";
                        }
                        string str = "";
                        if (!(this.DsItems.Tables[i].Rows[iNo][j] is DBNull))
                        {
                            str = (string) this.DsItems.Tables[i].Rows[iNo][j];
                        }
                        return str;
                    }
                }
            }
            return null;
        }

        public ItemInfoRec GetItemRec(string sId)
        {
            int itemRecIdx = this.GetItemRecIdx(sId);
            if (itemRecIdx >= 0)
            {
                return this.LstItemInfo[itemRecIdx];
            }
            return null;
        }

        private int GetItemRecIdx(string sId)
        {
            for (int i = 0; i < this.LstItemInfo.Count; i++)
            {
                if (this.LstItemInfo[i].id == sId)
                {
                    return i;
                }
            }
            return -1;
        }

        private int GetLastDataIdx()
        {
            for (int i = this.LstNaccsData.Count - 1; i >= 0; i--)
            {
                if (this.LstNaccsData[i].Data != "")
                {
                    return i;
                }
            }
            return -1;
        }

        private int GetLastPage(string RepID)
        {
            DataRow row = this.DataRowForm(RepID);
            for (int i = this.DsItems.Tables[RepID].Rows.Count - 1; i > -1; i--)
            {
                bool flag = false;
                for (int j = 0; j < this.DsItems.Tables[RepID].Columns.Count; j++)
                {
                    if (!(this.DsItems.Tables[RepID].Rows[i][j] is DBNull))
                    {
                        string str = (string) this.DsItems.Tables[RepID].Rows[i][j];
                        if (!string.IsNullOrEmpty(str.TrimEnd(new char[0])) && !this.DsItems.Tables[RepID].Rows[i][j].Equals(row[j]))
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                if (flag)
                {
                    return i;
                }
            }
            return -1;
        }

        public string GetRevisionSetData(string RepID, string id, string Data)
        {
            string attribute;
            string str2;
            int figure;
            StrFunc func;
            int itemRecIdx = this.GetItemRecIdx(id);
            if (itemRecIdx > -1)
            {
                attribute = this.LstItemInfo[itemRecIdx].attribute;
                str2 = this.LstItemInfo[itemRecIdx].input_output;
                figure = this.LstItemInfo[itemRecIdx].figure;
            }
            else
            {
                return null;
            }
            if (str2 != "-")
            {
                Data = Data.Trim();
                func = StrFunc.CreateInstance();
                string str4 = attribute;
                if (str4 == null)
                {
                    goto Label_00E3;
                }
                if (!(str4 == "I"))
                {
                    if (str4 == "F")
                    {
                        if (!func.IsNumeric(Data))
                        {
                            return null;
                        }
                    }
                    else if (str4 == "A")
                    {
                        Data = Data.ToUpper();
                        if (!func.ForbiddenCharCheck(Data))
                        {
                            return null;
                        }
                    }
                    else if ((str4 == "W") && !func.ForbiddenConvCheck(Data))
                    {
                        return null;
                    }
                    goto Label_00E3;
                }
                if (func.IsIntegralNum(Data))
                {
                    goto Label_00E3;
                }
            }
            return null;
        Label_00E3:
            if (attribute == "W")
            {
                figure /= 3;
            }
            if (func.GetStringLength(Data) > figure)
            {
                return func.CutStringLength(Data, figure);
            }
            return Data;
        }

        public void GetTsvData(string RepID, int No, string id)
        {
            string text = "";
            for (int i = this.DsItems.Tables[RepID].Columns.IndexOf(id); i < this.DsItems.Tables[RepID].Columns.Count; i++)
            {
                string str2 = "";
                if (!(this.DsItems.Tables[RepID].Rows[No][i] is DBNull))
                {
                    str2 = (string) this.DsItems.Tables[RepID].Rows[No][i];
                }
                if (i == (this.DsItems.Tables[RepID].Columns.Count - 1))
                {
                    text = text + str2 + "\r\n";
                }
                else
                {
                    text = text + str2 + '\t';
                }
            }
            Clipboard.SetText(text);
        }

        private void Init()
        {
            this.dsItems.Tables.Clear();
            this.tblCom.Clear();
            this.tblRan.Clear();
            this.LstItemInfo.Clear();
            this.HeaderInfo.Init();
            this.tblCom.Columns.Clear();
            this.tblRan.Columns.Clear();
            this.addNewRowUptoCount(this.tblCom, 1);
            this.addNewRowUptoCount(this.tblRan, 1);
            this.dsItems.Tables.AddRange(new DataTable[] { this.tblCom, this.tblRan });
        }

        public void InsertRows(string repID, int[] selectRows, int insertLine)
        {
            this.LastPageRemove(repID);
            DataRow row = this.DataRowForm(repID);
            int num = insertLine;
            if (selectRows.Length > 1)
            {
                num = 1;
            }
            int num2 = 0;
            for (int i = 0; i < selectRows.Length; i++)
            {
                if (i == 0)
                {
                    num2 = selectRows[i];
                }
                else if (selectRows[i] == (selectRows[i - 1] + 1))
                {
                    num2++;
                }
                else
                {
                    num2 = selectRows[i] + i;
                }
                for (int j = this.DsItems.Tables[repID].Rows.Count - 1; j >= num2; j--)
                {
                    if ((j + num) < this.DsItems.Tables[repID].Rows.Count)
                    {
                        this.DsItems.Tables[repID].Rows[j + num].ItemArray = this.DsItems.Tables[repID].Rows[j].ItemArray;
                    }
                    this.DsItems.Tables[repID].Rows[j].ItemArray = row.ItemArray;
                }
            }
            this.DsItems.Tables[repID].AcceptChanges();
        }

        private void LastPageRemove(string RepID)
        {
            this.GetLastPage(RepID);
            if (this.LstLastPageInfo.Count > 0)
            {
                for (int i = 0; i < this.LstLastPageInfo.Count; i++)
                {
                    if (this.LstLastPageInfo[i].Tbl == RepID)
                    {
                        this.LstLastPageInfo.Remove(this.LstLastPageInfo[i]);
                    }
                }
            }
        }

        private void MakeLstLastPageInfo()
        {
            for (int i = this.LstNaccsData.Count - 1; i >= 0; i--)
            {
                if (!(this.LstNaccsData[i].Tbl != tblComName) || (this.LstNaccsData[i].Data == ""))
                {
                    continue;
                }
                bool flag = false;
                for (int j = 0; j < this.LstLastPageInfo.Count; j++)
                {
                    if (this.LstLastPageInfo[j].Tbl == this.LstNaccsData[i].Tbl)
                    {
                        flag = true;
                        if (this.LstLastPageInfo[j].Page < this.LstNaccsData[i].Page)
                        {
                            this.LstLastPageInfo[j].Page = this.LstNaccsData[i].Page;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    LastPageInfo item = new LastPageInfo {
                        Tbl = this.LstNaccsData[i].Tbl,
                        Page = this.LstNaccsData[i].Page
                    };
                    this.LstLastPageInfo.Add(item);
                }
            }
        }

        private void MakeLstNaccsData()
        {
            this.LstNaccsData.Clear();
            string id = null;
            long order = 0L;
            string str2 = null;
            string @out = null;
            string @in = null;
            int figure = 0;
            string attribute = null;
            string rightpacked = null;
            for (int i = 0; i < this.DsItems.Tables.Count; i++)
            {
                for (int j = 0; j < this.DsItems.Tables[i].Columns.Count; j++)
                {
                    int itemRecIdx = this.GetItemRecIdx(this.DsItems.Tables[i].Columns[j].ColumnName);
                    if (itemRecIdx > -1)
                    {
                        order = this.LstItemInfo[itemRecIdx].order;
                        id = this.LstItemInfo[itemRecIdx].id;
                        str2 = this.LstItemInfo[itemRecIdx].rep_id;
                        @out = this.LstItemInfo[itemRecIdx].Out;
                        @in = this.LstItemInfo[itemRecIdx].In;
                        figure = this.LstItemInfo[itemRecIdx].figure;
                        attribute = this.LstItemInfo[itemRecIdx].attribute;
                        rightpacked = this.LstItemInfo[itemRecIdx].Rightpacked;
                        for (int k = 0; k < this.DsItems.Tables[i].Rows.Count; k++)
                        {
                            if (k > 0)
                            {
                                order += this.DsItems.Tables[i].Columns.Count;
                            }
                            NaccsDataItem item = new NaccsDataItem {
                                order = order,
                                ID = id
                            };
                            if (this.DsItems.Tables[i].Rows[k][j] is DBNull)
                            {
                                item.Data = "";
                            }
                            else
                            {
                                item.Data = (string) this.DsItems.Tables[i].Rows[k][j];
                            }
                            item.Tbl = str2;
                            item.Page = k;
                            item.Out = @out;
                            item.In = @in;
                            item.Figure = figure;
                            item.Attr = attribute;
                            item.Rpacked = rightpacked;
                            this.LstNaccsData.Add(item);
                        }
                    }
                }
            }
            this.LstNaccsData.Sort(new Comparison<NaccsDataItem>(this.OrderCompare));
        }

        public int OrderCompare(object x, object y)
        {
            NaccsDataItem item = (NaccsDataItem) x;
            NaccsDataItem item2 = (NaccsDataItem) y;
            return item.order.CompareTo(item2.order);
        }

        public bool PasteDecision(string sId)
        {
            int itemRecIdx = this.GetItemRecIdx(sId);
            return (((itemRecIdx > -1) && (this.LstItemInfo[itemRecIdx].input_output == "+")) && (this.LstItemInfo[itemRecIdx].In == "1"));
        }

        public void ReadCorrelation(string FileName)
        {
            this.correlationFileName = FileName;
            if (((this.correlationFileName != null) && (this.correlationFileName != "")) && (File.Exists(this.correlationFileName) && (this.correlation == null)))
            {
                this.correlation = new XmlDocument();
                this.correlation.Load(this.correlationFileName);
            }
        }

        public void RemoveRows(string repID, int[] selectRows)
        {
            this.LastPageRemove(repID);
            DataRow row = this.DataRowForm(repID);
            for (int i = selectRows.Length - 1; i >= 0; i--)
            {
                for (int j = selectRows[i]; j < this.DsItems.Tables[repID].Rows.Count; j++)
                {
                    if (j < (this.DsItems.Tables[repID].Rows.Count - 1))
                    {
                        this.DsItems.Tables[repID].Rows[j].ItemArray = this.DsItems.Tables[repID].Rows[j + 1].ItemArray;
                        this.DsItems.Tables[repID].Rows[j + 1].ItemArray = row.ItemArray;
                    }
                    else
                    {
                        this.DsItems.Tables[repID].Rows[j].ItemArray = row.ItemArray;
                    }
                }
            }
            this.DsItems.Tables[repID].AcceptChanges();
        }

        public int ResetLastPage(string repID, int repCount)
        {
            int lastPage = this.GetLastPage(repID);
            if (lastPage < 0)
            {
                this.LastPageRemove(repID);
                return -1;
            }
            if (repCount > 0)
            {
                int num2 = lastPage % (repCount + 1);
                lastPage -= num2;
            }
            bool flag = false;
            if (this.LstLastPageInfo == null)
            {
                this.LstLastPageInfo = new List<LastPageInfo>();
            }
            for (int i = 0; i < this.LstLastPageInfo.Count; i++)
            {
                if (repID == this.LstLastPageInfo[i].Tbl)
                {
                    this.LstLastPageInfo[i].Page = lastPage;
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                LastPageInfo item = new LastPageInfo {
                    Tbl = repID,
                    Page = lastPage
                };
                this.LstLastPageInfo.Add(item);
            }
            return lastPage;
        }

        public int SearchError(out string id, out int errorPage, bool flgCheckReq)
        {
            return this.SearchError(out id, out errorPage, flgCheckReq, true);
        }

        public int SearchError(out string id, out int errorPage, bool flgCheckReq, bool flgCheckData)
        {
            id = "";
            errorPage = -1;
            bool flag = false;
            for (int i = 0; i < this.DsItems.Tables.Count; i++)
            {
                string str = "";
                int num2 = -1;
                int num3 = -1;
                for (int j = 0; j < this.DsItems.Tables[i].Rows.Count; j++)
                {
                    bool flag2 = false;
                    int num5 = 0;
                    string str2 = "";
                    for (int k = 0; k < this.DsItems.Tables[i].Columns.Count; k++)
                    {
                        DataRow dataRow = null;
                        dataRow = this.DsItems.Tables[i].Rows[j];
                        int itemRecIdx = this.GetItemRecIdx(this.DsItems.Tables[i].Columns[k].Caption);
                        if (itemRecIdx > -1)
                        {
                            string str3 = this.LstItemInfo[itemRecIdx].id;
                            if (this.LstItemInfo[itemRecIdx].In == "1")
                            {
                                bool flag3;
                                int num8 = this.doCheck(itemRecIdx, dataRow, str3, out flag3, flgCheckReq);
                                if ((num8 != 0) && (num5 == 0))
                                {
                                    num5 = num8;
                                    str2 = str3;
                                }
                                flag2 = flag2 || flag3;
                                flag = flag || flag3;
                            }
                        }
                    }
                    if (this.DsItems.Tables[i].TableName == tblRanName)
                    {
                        if (flag2)
                        {
                            if (str != "")
                            {
                                id = str;
                                errorPage = num2;
                                return num3;
                            }
                            if (num5 != 0)
                            {
                                id = str2;
                                errorPage = j + 1;
                                return num5;
                            }
                        }
                        else if ((str == "") && (num5 != 0))
                        {
                            str = str2;
                            num2 = j + 1;
                            num3 = num5;
                        }
                    }
                    else if (num5 != 0)
                    {
                        id = str2;
                        if (this.DsItems.Tables[i].TableName == tblComName)
                        {
                            errorPage = j;
                            return num5;
                        }
                        errorPage = j + 1;
                        return num5;
                    }
                }
            }
            if (flgCheckData && !flag)
            {
                return -1;
            }
            return 0;
        }

        public void SetCheckInfo(string sID, string sDateCheck, string sTimeCheck)
        {
            int itemRecIdx = this.GetItemRecIdx(sID);
            if (itemRecIdx > -1)
            {
                this.LstItemInfo[itemRecIdx].check_date = sDateCheck;
                this.LstItemInfo[itemRecIdx].check_time = sTimeCheck;
            }
        }

        public void SetDataList(List<string> stList, JobDataStatus jds, string tblName)
        {
            this.LstLastPageInfo.Clear();
            StrFunc func = StrFunc.CreateInstance();
            this.MakeLstNaccsData();
            string s = "";
            int num = 0;
            int num2 = 0;
        Label_0021:
            if ((num2 != stList.Count) && (num != this.LstNaccsData.Count))
            {
                if ((tblName != "") && (tblName != this.LstNaccsData[num].Tbl))
                {
                    num++;
                }
                else if (((this.LstNaccsData[num].In == "0") && (jds == JobDataStatus.jdSend)) || ((this.LstNaccsData[num].Out == "0") && (jds == JobDataStatus.jdRecv)))
                {
                    num++;
                }
                else if (stList[num2] == "")
                {
                    num2++;
                    num++;
                }
                else
                {
                    if (((this.LstNaccsData[num].Attr == "I") || (this.LstNaccsData[num].Attr == "F")) || (this.LstNaccsData[num].Rpacked == "1"))
                    {
                        s = stList[num2].TrimStart(null);
                    }
                    else
                    {
                        s = stList[num2].TrimEnd(null);
                    }
                    if ((this.LstNaccsData[num].Attr == "I") && !func.IsIntegralNum(s))
                    {
                        num2++;
                        num++;
                    }
                    else
                    {
                        if (this.LstNaccsData[num].Attr == "F")
                        {
                            s = func.ConvertPeriodToComma(s);
                            if (!func.IsNumeric(s))
                            {
                                num2++;
                                num++;
                                goto Label_0021;
                            }
                        }
                        if (this.LstNaccsData[num].Attr == "A")
                        {
                            s = s.ToUpper();
                            if (!func.ForbiddenCharCheck(s))
                            {
                                num2++;
                                num++;
                                goto Label_0021;
                            }
                        }
                        if ((this.LstNaccsData[num].Attr == "W") && !func.ForbiddenConvCheck(s))
                        {
                            num2++;
                            num++;
                        }
                        else
                        {
                            int figure = this.LstNaccsData[num].Figure;
                            if ("W".Equals(this.LstNaccsData[num].Attr))
                            {
                                figure /= 3;
                            }
                            if (func.GetStringLength(s) > figure)
                            {
                                s = func.CutStringLength(s, figure);
                            }
                            this.LstNaccsData[num].Data = s;
                            num2++;
                            num++;
                        }
                    }
                }
                goto Label_0021;
            }
            DataSet set = this.DsItems.Copy();
            for (int i = 0; i < this.LstNaccsData.Count; i++)
            {
                NaccsDataItem item = this.LstNaccsData[i];
                set.Tables[item.Tbl].Rows[item.Page][item.ID] = item.Data;
            }
            for (int j = 0; j < this.DsItems.Tables.Count; j++)
            {
                for (int k = 0; k < this.DsItems.Tables[j].Rows.Count; k++)
                {
                    this.DsItems.Tables[j].Rows[k].ItemArray = set.Tables[j].Rows[k].ItemArray;
                }
                this.DsItems.Tables[j].AcceptChanges();
            }
            this.MakeLstLastPageInfo();
        }

        public void SetInitString(string sID, string sInit)
        {
            int itemRecIdx = this.GetItemRecIdx(sID);
            if (itemRecIdx > -1)
            {
                if (this.LstItemInfo[itemRecIdx].form != sInit)
                {
                    this.LstItemInfo[itemRecIdx].form = sInit;
                    int index = this.DsItems.Tables[this.LstItemInfo[itemRecIdx].rep_id].Columns.IndexOf(sID);
                    for (int i = 0; i < this.DsItems.Tables[this.LstItemInfo[itemRecIdx].rep_id].Rows.Count; i++)
                    {
                        this.DsItems.Tables[this.LstItemInfo[itemRecIdx].rep_id].Rows[i][index] = sInit;
                    }
                }
                this.DsItems.Tables[this.LstItemInfo[itemRecIdx].rep_id].AcceptChanges();
            }
        }

        public void SetTsvData(string RepID, int No, string id)
        {
            IDataObject dataObject = Clipboard.GetDataObject();
            if (dataObject.GetDataPresent(DataFormats.UnicodeText))
            {
                int count = this.DsItems.Tables[RepID].Rows.Count;
                int num2 = this.DsItems.Tables[RepID].Columns.Count;
                int ordinal = this.DsItems.Tables[RepID].Columns[id].Ordinal;
                int num4 = No;
                StringReader reader = new StringReader((string) dataObject.GetData(DataFormats.UnicodeText));
                while (true)
                {
                    if (num4 == count)
                    {
                        break;
                    }
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    List<string> list = TsvToItems(line);
                    if (list.Count > 0)
                    {
                        int num5 = 0;
                        while (num5 < list.Count)
                        {
                            if (ordinal == num2)
                            {
                                break;
                            }
                            int itemRecIdx = this.GetItemRecIdx(this.DsItems.Tables[RepID].Columns[ordinal].ColumnName);
                            if (itemRecIdx > -1)
                            {
                                if ((this.LstItemInfo[itemRecIdx].input_output == "+") && (this.LstItemInfo[itemRecIdx].In == "1"))
                                {
                                    this.DsItems.Tables[RepID].Rows[num4][ordinal] = this.GetRevisionSetData(RepID, this.DsItems.Tables[RepID].Columns[ordinal].ColumnName, list[num5]);
                                }
                                num5++;
                            }
                            ordinal++;
                        }
                        ordinal = 0;
                    }
                    num4++;
                }
                this.DsItems.Tables[RepID].AcceptChanges();
            }
        }

        private static List<string> TsvToItems(string line)
        {
            List<string> list = new List<string>();
            int startIndex = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '\t')
                {
                    list.Add(line.Substring(startIndex, i - startIndex));
                    startIndex = i + 1;
                }
                if ((i + 1) == line.Length)
                {
                    list.Add(line.Substring(startIndex, (i + 1) - startIndex));
                }
            }
            return list;
        }

        public string DisplayName
        {
            get
            {
                return this.HeaderInfo.displayname;
            }
        }

        public DataSet DsItems
        {
            get
            {
                return this.dsItems;
            }
            set
            {
                this.dsItems = value;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct HeaderInfoRec
        {
            public string jobcode;
            public string jobname;
            public string displayname;
            public void Init()
            {
                this.jobcode = "";
                this.jobname = "";
                this.displayname = "";
            }
        }

        public enum JobDataStatus
        {
            jdNone,
            jdSend,
            jdRecv
        }
    }
}

