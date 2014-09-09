namespace Naccs.Common.Print
{
    using Naccs.Common;
    using Naccs.Common.Constant;
    using Naccs.Common.Function;
    using Naccs.Common.JobConfig;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    public class PrintXml
    {
        private int contained;
        private int convertMode;
        private System.Drawing.Size headerSize = new System.Drawing.Size(0x4b, 12);
        private string infoUniqueNo = "";
        private bool isTrialMode;
        private AbstractJobConfig jobConf;
        private int mDataMaxRanNo;
        private DateTime mDateTime;
        private XmlDocument mItemXML;
        private int mPrintPageNo;
        private RecordAttribute mRecodeAtb;
        private string mTempFile;
        private DateTime mTimeStamp;
        public const string PAGE_MAX_MARK = "[%mp%]";
        private string serverRecvTime = "";
        private StrFunc SF = StrFunc.CreateInstance();

        private void cangeData(XmlNode node, int pageno, int ranno, List<string> data)
        {
            if (node.Name == "pagelabel")
            {
                node.InnerText = this.getPageLabelStr(node, pageno);
            }
            else if (node.Name == "ranlabel")
            {
                if (XmlFunc.getStringAttribute(node, "PadLeft") == "True")
                {
                    node.InnerText = ranno.ToString().PadLeft(XmlFunc.getNumericAttribute(node, "figure"), '0');
                }
                else
                {
                    node.InnerText = ranno.ToString();
                }
            }
            else if (node.Name == "prttextbox")
            {
                if (XmlFunc.getStringAttribute(node, "Format").Length != 0)
                {
                    string format = XmlFunc.getStringAttribute(node, "Format");
                    string str = "";
                    if (this.isTrialMode)
                    {
                        str = this.getDmyDataValue(node);
                    }
                    else
                    {
                        str = this.getHitData(node, ranno, data);
                    }
                    if (format.Contains("dd/MM/yyyy") || format.Contains("MM/yyyy"))
                    {
                        format = format.Replace("d", "9").Replace("M", "9").Replace("y", "9");
                    }
                    if ((format.Contains("_") || format.Contains("9")) || (format.Contains("A") || format.Contains("C")))
                    {
                        node.InnerText = this.getDivFormConv(str, format);
                    }
                    else if (str.Trim().Length > 0)
                    {
                        str = str.Trim();
                        string str6 = XmlFunc.getStringAttribute(node, "attribute");
                        if ("F".Equals(str6))
                        {
                            str = this.SF.ConvertPeriodToComma(str);
                        }
                        if (format.Contains("#"))
                        {
                            if (this.SF.IsNumeric(str))
                            {
                                if (format.Substring(0, 1) != "#")
                                {
                                    if (format.Contains(","))
                                    {
                                        node.InnerText = this.SF.MakeYenCommaStr(str, format.Substring(0, 1), true);
                                    }
                                    else
                                    {
                                        node.InnerText = this.SF.MakeYenCommaStr(str, format.Substring(0, 1), false);
                                    }
                                }
                                else if (format.Contains(","))
                                {
                                    node.InnerText = this.SF.MakeYenCommaStr(str, "", true);
                                }
                                else
                                {
                                    node.InnerText = this.SF.MakeYenCommaStr(str, "", false);
                                }
                            }
                            else
                            {
                                node.InnerText = this.SF.MakeCycleStr(str.Length, "?");
                            }
                        }
                        else if (this.isTrialMode)
                        {
                            node.InnerText = format;
                        }
                        else if (this.SF.IsNumeric(str))
                        {
                            node.InnerText = this.getTimeValue(str, format);
                        }
                        else
                        {
                            node.InnerText = this.SF.MakeCycleStr(str.Length, "?");
                        }
                    }
                }
                else if (XmlFunc.getStringAttribute(node, "PasswordChar").Length > 0)
                {
                    node.InnerText = this.SF.MakeCycleStr(XmlFunc.getNumericAttribute(node, "figure"), XmlFunc.getStringAttribute(node, "PasswordChar"));
                }
                else if (this.isTrialMode)
                {
                    node.InnerText = this.getDmyDataValue(node);
                }
                else
                {
                    string s = this.getHitData(node, ranno, data);
                    if (s != null)
                    {
                        string str3 = XmlFunc.getStringAttribute(node, "attribute");
                        if ("F".Equals(str3))
                        {
                            s = this.SF.ConvertPeriodToComma(s);
                        }
                        node.InnerText = s;
                    }
                }
            }
            else if ((node.Name == "checkbox") || (node.Name == "radio"))
            {
                if (this.isTrialMode)
                {
                    node.InnerText = "1";
                }
                else
                {
                    string strA = this.getHitData(node, ranno, data).Trim();
                    if (((string.Compare(strA, "TRUE", true) == 0) || (strA == "1")) || (strA == "+"))
                    {
                        node.InnerText = "1";
                    }
                    else
                    {
                        node.InnerText = "0";
                    }
                }
            }
            else if (((node.Name == "barcode") || (node.Name == "hand-ocr")) || (node.Name == "comp-ocr"))
            {
                string str8 = "";
                string str9 = XmlFunc.getStringAttribute(node, "CurrencyMark");
                int length = XmlFunc.getNumericAttribute(node, "figure");
                if (str9.Length > 0)
                {
                    length++;
                }
                if (this.isTrialMode)
                {
                    str8 = this.getDmyDataValue(node);
                }
                else
                {
                    str8 = this.getHitData(node, ranno, data);
                    if (str8 == null)
                    {
                        str8 = "";
                    }
                    str8 = str8.Trim();
                }
                if (str8.Length > 0)
                {
                    str8 = str9 + str8;
                }
                if (str8.Length > length)
                {
                    str8 = str8.Substring(0, length);
                }
                node.InnerText = this.SF.MakeCycleStr(length - str8.Length, " ") + str8;
            }
        }

        private void ChkAndSetFont(XmlNode node)
        {
            if ((node.Name != "gstomp") && (XmlFunc.getStringAttribute(node, "Font").Length == 0))
            {
                node.Attributes.Append(node.OwnerDocument.CreateAttribute("Font"));
                string str = XmlFunc.getStringAttribute(node.ParentNode, "Font");
                if (str.Length > 0)
                {
                    node.Attributes.GetNamedItem("Font").Value = str;
                }
                else if ((node.Name.Equals("prttextbox") || node.Name.Equals("radio")) || node.Name.Equals("checkbox"))
                {
                    node.Attributes.GetNamedItem("Font").Value = "Courier New, 9pt";
                }
                else
                {
                    node.Attributes.GetNamedItem("Font").Value = "Tahoma, 9pt";
                }
            }
        }

        private XmlNode copyXmlNode(XmlNode tergetNode, XmlNode refNode)
        {
            XmlElement newChild = tergetNode.OwnerDocument.CreateElement(refNode.Name);
            tergetNode.AppendChild(newChild);
            foreach (XmlAttribute attribute in refNode.Attributes)
            {
                newChild.Attributes.Append(tergetNode.OwnerDocument.CreateAttribute(attribute.Name));
                newChild.Attributes.GetNamedItem(attribute.Name).Value = attribute.Value;
            }
            return newChild;
        }

        private XmlNode copyXmlNode(XmlNode tergetNode, XmlNode refNode, string tagName)
        {
            XmlElement newChild = tergetNode.OwnerDocument.CreateElement(tagName);
            tergetNode.AppendChild(newChild);
            foreach (XmlAttribute attribute in refNode.Attributes)
            {
                newChild.Attributes.Append(tergetNode.OwnerDocument.CreateAttribute(attribute.Name));
                newChild.Attributes.GetNamedItem(attribute.Name).Value = attribute.Value;
            }
            return newChild;
        }

        private XmlNode createLabelNode(XmlDocument doc, Point location, System.Drawing.Size size, ContentAlignment align, string text)
        {
            XmlNode source = doc.CreateElement("label");
            this.setLocationAttribute(source, location);
            this.setSizeAttribute(source, size);
            XmlFunc.setStringAttribute(source, "TextAlign", align.ToString());
            XmlFunc.setStringAttribute(source, "Text", text);
            XmlFunc.setStringAttribute(source, "Font", "Courier New, 9pt");
            return source;
        }

        public XmlDocument createPrintXml(XmlDocument source, List<string> data)
        {
            return this.createPrintXml(source, null, data);
        }

        public XmlDocument createPrintXml(XmlDocument source, XmlDocument itemxml, List<string> data)
        {
            XmlDocument document2;
            try
            {
                if (itemxml == null)
                {
                    this.mItemXML = source;
                }
                else
                {
                    this.mItemXML = itemxml;
                }
                if (data == null)
                {
                    this.isTrialMode = true;
                }
                else
                {
                    this.isTrialMode = false;
                }
                if (this.mTimeStamp.Ticks == 0L)
                {
                    this.mDateTime = DateTime.Now;
                }
                else
                {
                    this.mDateTime = this.mTimeStamp;
                }
                this.mTempFile = Path.GetFileName(source.BaseURI);
                XmlDocument document = new XmlDocument();
                document.AppendChild(document.CreateXmlDeclaration("1.0", "UTF-8", null));
                XmlElement newChild = document.CreateElement("jobform");
                document.AppendChild(newChild);
                newChild.Attributes.Append(document.CreateAttribute("jobcode"));
                newChild.Attributes.Append(document.CreateAttribute("jobname"));
                newChild.Attributes.GetNamedItem("jobcode").Value = XmlFunc.getStringAttribute(source.SelectSingleNode("jobform"), "jobcode");
                newChild.Attributes.GetNamedItem("jobname").Value = XmlFunc.getStringAttribute(source.SelectSingleNode("jobform"), "jobname");
                this.copyXmlNode(document.SelectSingleNode("jobform"), source.SelectSingleNode("jobform/meta_info"));
                this.copyXmlNode(document.SelectSingleNode("jobform"), source.SelectSingleNode("jobform/layout"));
                XmlElement tergetNode = (XmlElement) document.SelectSingleNode("jobform/layout");
                int ranNo = 0;
                int printPageNo = this.PrintPageNo;
                int num3 = 0;
                this.mDataMaxRanNo = 0;
                if (!this.isTrialMode)
                {
                    XmlNode node = source.SelectSingleNode("jobform/layout/page/ran");
                    if (node != null)
                    {
                        this.mDataMaxRanNo = this.getRanNo(this.mItemXML, XmlFunc.getStringAttribute(node, "repetition_id"), data);
                    }
                }
                foreach (XmlNode node2 in source.SelectNodes("jobform/layout/page"))
                {
                    int num4 = XmlFunc.getNumericAttribute(node2, "MinOccurs", 0);
                    int num5 = XmlFunc.getNumericAttribute(node2, "MaxOccurs", 1);
                    if (this.isTrialMode)
                    {
                        num4 = 1;
                        num5 = 1;
                    }
                    num3 = 0;
                    while (((ranNo < this.mDataMaxRanNo) || (num3 < num4)) && (num3 < num5))
                    {
                        printPageNo++;
                        num3++;
                        XmlNode outpageNode = this.copyXmlNode(tergetNode, node2);
                        if ((this.convertMode > 0) || ((this.jobConf != null) && (this.jobConf.templateSytle != TemplateStyle.Print)))
                        {
                            string infoUniqueNo;
                            if (this.isTrialMode)
                            {
                                infoUniqueNo = this.SF.MakeCycleStr(9, "X") + "E";
                            }
                            else
                            {
                                infoUniqueNo = this.infoUniqueNo;
                            }
                            Point location = this.getFinalLinePoint(node2);
                            outpageNode.AppendChild(this.createLabelNode(outpageNode.OwnerDocument, location, this.headerSize, ContentAlignment.MiddleLeft, infoUniqueNo));
                        }
                        foreach (XmlNode node4 in node2.ChildNodes)
                        {
                            this.ChkAndSetFont(node4);
                            if (node4.Name == "ran")
                            {
                                ranNo += this.openContainer(outpageNode, node4, printPageNo, ranNo, data);
                            }
                            else if (node4.Name == "container")
                            {
                                this.openContainer(outpageNode, node4, printPageNo, ranNo, data);
                            }
                            else
                            {
                                XmlNode node5 = this.copyXmlNode(outpageNode, node4);
                                this.cangeData(node5, printPageNo, 0, data);
                            }
                        }
                    }
                }
                this.PrintPageNo = printPageNo;
                document2 = document;
            }
            catch (Exception exception)
            {
                throw new NaccsException(MessageKind.Error, 0x261, exception.Message);
            }
            return document2;
        }

        private int getDataIdx(XmlDocument iteminfo, string name, int ranno)
        {
            int num = 0;
            string str = name.Trim(new char[] { '_' });
            foreach (XmlNode node2 in iteminfo.SelectSingleNode("jobform/iteminfo").ChildNodes)
            {
                if (node2.Name == "item")
                {
                    if (XmlFunc.getStringAttribute(node2, "id") == str)
                    {
                        if (this.IsContained(node2))
                        {
                            return num;
                        }
                        return -1;
                    }
                    if (this.IsContained(node2))
                    {
                        num++;
                    }
                }
                else
                {
                    int num2 = 0;
                    int num3 = XmlFunc.getNumericAttribute(node2, "repetition_max");
                    int num4 = this.getRanContainedCount(node2.ChildNodes);
                    foreach (XmlNode node3 in node2.ChildNodes)
                    {
                        if (XmlFunc.getStringAttribute(node3, "id").Trim(new char[] { '_' }) == str)
                        {
                            num += num2 + (num4 * ranno);
                            if (this.IsContained(node3))
                            {
                                return num;
                            }
                            return -1;
                        }
                        if (this.IsContained(node3))
                        {
                            num2++;
                        }
                    }
                    num += num3 * num4;
                }
            }
            return -1;
        }

        private string getDivFormConv(string str, string format)
        {
            char ch = ' ';
            format = format.Replace("9", "_");
            format = format.Replace("A", "_");
            format = format.Replace("C", "_");
            format = format.Replace(">", "");
            format = format.Replace("<", "");
            format = format.Replace("|", "");
            int startIndex = 0;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < format.Length; i++)
            {
                if (format.Substring(i, 1) == "_")
                {
                    if (startIndex >= str.Length)
                    {
                        builder.Append(ch);
                    }
                    else
                    {
                        string st = str.Substring(startIndex, 1);
                        if (this.SF.GetByteLength(st) == 1)
                        {
                            builder.Append(st);
                            startIndex++;
                        }
                        else
                        {
                            if (format.Length < (i + 2))
                            {
                                break;
                            }
                            if (format.Substring(i + 1, 1) == "_")
                            {
                                builder.Append(st);
                                startIndex++;
                                i++;
                            }
                            else
                            {
                                builder.Append(ch);
                            }
                        }
                    }
                }
                else
                {
                    builder.Append(format.Substring(i, 1));
                }
            }
            return builder.ToString();
        }

        private string getDmyDataValue(XmlNode node)
        {
            string st = "X";
            string str3 = XmlFunc.getStringAttribute(node, "attribute");
            if (str3 != null)
            {
                if (!(str3 == "I") && !(str3 == "F"))
                {
                    if (str3 == "W")
                    {
                        st = "W";
                    }
                }
                else
                {
                    st = "N";
                }
            }
            StringBuilder builder = new StringBuilder();
            int num = XmlFunc.getNumericAttribute(node, "figure");
            if (st == "W")
            {
                num /= 3;
            }
            if (((st == "N") && XmlFunc.getStringAttribute(node, "Format").Contains("#")) || ((node.Name == "hand-ocr") || (node.Name == "comp-ocr")))
            {
                for (int i = 1; i <= num; i++)
                {
                    string introduced7 = i.ToString();
                    builder.Append(introduced7.Substring(i.ToString().Length - 1));
                }
            }
            else
            {
                string str2 = this.SF.MakeCycleStr(9, st);
                for (int j = 1; j <= (num / 10); j++)
                {
                    builder.Append(str2);
                    string introduced8 = j.ToString();
                    builder.Append(introduced8.Substring(j.ToString().Length - 1));
                }
                builder.Append(this.SF.MakeCycleStr(num - builder.Length, st));
                if (num > 1)
                {
                    builder.Remove(builder.Length - 1, 1);
                    builder.Append("E");
                }
            }
            return builder.ToString();
        }

        private Point getFinalLinePoint(XmlNode source)
        {
            string str = XmlFunc.getStringAttribute(source, "PaperSize", "A4");
            if (XmlFunc.getBoolAttribute(source, "LandScape", false))
            {
                switch (str)
                {
                    case "A4":
                        return new Point(0, SizeDef.LAYOUT_A4_WIDTH - 10);

                    case "A3":
                        return new Point(0, SizeDef.LAYOUT_A3_WIDTH - 10);
                }
                return new Point(0, SizeDef.LAYOUT_A5_WIDTH - 10);
            }
            if (str == "A4")
            {
                return new Point(0, SizeDef.LAYOUT_A4_HEIGHT - 10);
            }
            if (str == "A3")
            {
                return new Point(0, SizeDef.LAYOUT_A3_HEIGHT - 10);
            }
            return new Point(0, SizeDef.LAYOUT_A5_HEIGHT - 10);
        }

        private string getHitData(XmlNode node, int ranno, List<string> data)
        {
            int num;
            string name = XmlFunc.getStringAttribute(node, "id");
            if (ranno == 0)
            {
                num = this.getDataIdx(this.mItemXML, name, 0);
            }
            else
            {
                num = this.getDataIdx(this.mItemXML, name, ranno - 1);
            }
            if ((num < 0) || (data.Count <= num))
            {
                return "";
            }
            string str2 = XmlFunc.getStringAttribute(node, "attribute");
            if ((!(this.getRightPacked(this.mItemXML, name) == "1") && !(str2 == "I")) && !(str2 == "F"))
            {
                return data[num].TrimEnd(new char[0]);
            }
            return data[num].TrimStart(new char[0]);
        }

        private string getPageLabelStr(XmlNode node, int pageno)
        {
            XmlDocument ownerDocument = node.OwnerDocument;
            switch (XmlFunc.getStringAttribute(node, "PageFormat"))
            {
                case "Page":
                    return pageno.ToString();

                case "PageMax":
                    return "[%mp%]";

                case "PageProgress":
                    return (pageno.ToString() + " / [%mp%]");

                case "PageTitle":
                    if (this.jobConf == null)
                    {
                        return XmlFunc.getStringAttribute(node, "Text");
                    }
                    return this.mRecodeAtb.titile1;

                case "PageTitle2":
                    if (this.jobConf == null)
                    {
                        return XmlFunc.getStringAttribute(node, "Text");
                    }
                    return this.mRecodeAtb.titile2;

                case "PrintDate":
                    return string.Format(DateTimeFormatInfo.InvariantInfo, "{0:dd/MM/yyyy}", new object[] { this.mDateTime });

                case "PrintTime":
                    return string.Format(DateTimeFormatInfo.InvariantInfo, "{0:HH:mm:ss}", new object[] { this.mDateTime });

                case "FileName":
                    return this.mTempFile;

                case "JobCode":
                    return XmlFunc.getStringAttribute(ownerDocument.SelectSingleNode("jobform"), "jobcode");

                case "JobName":
                    return XmlFunc.getStringAttribute(ownerDocument.SelectSingleNode("jobform"), "jobname");

                case "ReceiveDateTime":
                    if (!this.isTrialMode)
                    {
                        return this.getTimeValue(this.serverRecvTime, "dd/MM/yyyy HH:mm");
                    }
                    return XmlFunc.getStringAttribute(node, "Text");
            }
            return "";
        }

        private int getRanContainedCount(XmlNodeList nodeList)
        {
            int num = 0;
            foreach (XmlNode node in nodeList)
            {
                if (this.IsContained(node))
                {
                    num++;
                }
            }
            return num;
        }

        private int getRanNo(XmlDocument iteminfo, string name, List<string> datalst)
        {
            string str;
            int num5;
            if (name.Trim().Length == 0)
            {
                str = "R01";
            }
            else
            {
                str = name.Trim();
            }
            XmlNode source = null;
            foreach (XmlNode node3 in iteminfo.SelectSingleNode("jobform/iteminfo").ChildNodes)
            {
                if (str == "R01")
                {
                    if (!(node3.Name == "ranContainer"))
                    {
                        continue;
                    }
                    source = node3;
                    break;
                }
                if ((node3.Name == "fixedContainer") && (XmlFunc.getStringAttribute(node3, "repetition_id") == str))
                {
                    source = node3;
                    break;
                }
            }
            if (source == null)
            {
                return 0;
            }
            int count = source.ChildNodes.Count;
            int num2 = XmlFunc.getNumericAttribute(source, "repetition_max");
            int num3 = this.getDataIdx(iteminfo, XmlFunc.getStringAttribute(source.ChildNodes[0], "id"), 0);
            int num4 = this.getDataIdx(iteminfo, XmlFunc.getStringAttribute(source.ChildNodes[count - 1], "id"), num2 - 1);
            if (datalst.Count <= num4)
            {
                num5 = datalst.Count - 1;
            }
            else
            {
                num5 = num4;
            }
            while ((num5 > 0) && (datalst[num5].Trim().Length == 0))
            {
                num5--;
            }
            if (num5 < num3)
            {
                return 0;
            }
            return (int) Math.Ceiling((double) (((double) ((num5 - num3) + 1)) / ((double) count)));
        }

        private string getRightPacked(XmlDocument iteminfo, string name)
        {
            string str = name.Trim(new char[] { '_' });
            foreach (XmlNode node2 in iteminfo.SelectSingleNode("jobform/iteminfo").ChildNodes)
            {
                if (node2.Name == "item")
                {
                    if (XmlFunc.getStringAttribute(node2, "id") == str)
                    {
                        return XmlFunc.getStringAttribute(node2, "right_packed", "0");
                    }
                }
                else
                {
                    foreach (XmlNode node3 in node2.ChildNodes)
                    {
                        if (XmlFunc.getStringAttribute(node3, "id").Trim(new char[] { '_' }) == str)
                        {
                            return XmlFunc.getStringAttribute(node3, "right_packed", "0");
                        }
                    }
                }
            }
            return "0";
        }

        private string getTimeValue(string timeStr, string format)
        {
            char[] anyOf = new char[] { 'y', 'Y', 'M', 'd', 'D' };
            char[] chArray2 = new char[] { 'h', 'H', 'm', 's', 'S' };
            string s = timeStr.Trim();
            string str2 = format.Trim().Replace('h', 'H');
            if (!this.SF.IsNumeric(s))
            {
                return s;
            }
            if ((str2.IndexOfAny(anyOf) < 0) && (str2.IndexOfAny(chArray2) < 0))
            {
                return s;
            }
            try
            {
                DateTime time2;
                DateTime now = new DateTime();
                now = DateTime.Now;
                int year = 0;
                int month = 0;
                int day = 0;
                int hour = 0;
                int minute = 0;
                int second = 0;
                if (str2.IndexOfAny(anyOf) < 0)
                {
                    if ((s.Length != 4) && (s.Length != 6))
                    {
                        return s;
                    }
                    hour = int.Parse(s.Substring(0, 2));
                    minute = int.Parse(s.Substring(2, 2));
                    if (s.Length == 6)
                    {
                        second = int.Parse(s.Substring(4, 2));
                    }
                    time2 = new DateTime(now.Year, now.Month, now.Day, hour, minute, second);
                }
                else if (str2.IndexOfAny(chArray2) < 0)
                {
                    if (s.Length != 8)
                    {
                        return s;
                    }
                    year = int.Parse(s.Substring(0, 4));
                    month = int.Parse(s.Substring(4, 2));
                    day = int.Parse(s.Substring(6, 2));
                    time2 = new DateTime(year, month, day);
                }
                else
                {
                    if (s.Length != 14)
                    {
                        return s;
                    }
                    year = int.Parse(s.Substring(0, 4));
                    month = int.Parse(s.Substring(4, 2));
                    day = int.Parse(s.Substring(6, 2));
                    hour = int.Parse(s.Substring(8, 2));
                    minute = int.Parse(s.Substring(10, 2));
                    second = int.Parse(s.Substring(12, 2));
                    time2 = new DateTime(year, month, day, hour, minute, second);
                }
                return time2.ToString(str2, new CultureInfo("en-US"));
            }
            catch (Exception)
            {
                return s;
            }
        }

        private bool IsContained(XmlNode node)
        {
            return ((this.contained == 0) || (((this.contained == 1) && (XmlFunc.getNumericAttribute(node, "in_flg") == 1)) || ((this.contained == 2) && (XmlFunc.getNumericAttribute(node, "out_flg") == 1))));
        }

        private int openContainer(XmlNode outpageNode, XmlNode node, int PageNo, int RanNo, List<string> data)
        {
            return this.openContainer(outpageNode, node, PageNo, RanNo, data, new Point(0, 0));
        }

        private int openContainer(XmlNode outpageNode, XmlNode node, int PageNo, int RanNo, List<string> data, Point baseLoc)
        {
            XmlNode node2 = null;
            int num = 0;
            Point component = new Point(0, 0);
            Point point2 = new Point(0, 0);
            component = (Point) TypeDescriptor.GetConverter(component).ConvertFromInvariantString(XmlFunc.getStringAttribute(node, "Location"));
            point2 = (Point) TypeDescriptor.GetConverter(point2).ConvertFromInvariantString(XmlFunc.getStringAttribute(node, "Size"));
            int num2 = XmlFunc.getNumericAttribute(node, "RepX", 1);
            int num3 = XmlFunc.getNumericAttribute(node, "RepY", 1);
            int num4 = XmlFunc.getNumericAttribute(node, "SpaceX", 0);
            int num5 = XmlFunc.getNumericAttribute(node, "SpaceY", 0);
            for (int i = 0; i < num3; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    num++;
                    if (this.isTrialMode || ((RanNo + num) <= this.mDataMaxRanNo))
                    {
                        if (node.Name == "container")
                        {
                            string str = XmlFunc.getStringAttribute(node, "BorderStyle").Trim();
                            if ((str.Length > 0) && (str != BorderStyle.None.ToString()))
                            {
                                Point point3 = new Point(0, 0) {
                                    Y = (component.Y + ((num5 + point2.Y) * i)) + baseLoc.Y,
                                    X = (component.X + ((num4 + point2.X) * j)) + baseLoc.X
                                };
                                node2 = this.copyXmlNode(outpageNode, node, "rectangle");
                                node2.Attributes.GetNamedItem("Location").Value = TypeDescriptor.GetConverter(point3).ConvertToInvariantString(point3);
                                this.cangeData(node2, PageNo, RanNo, data);
                            }
                        }
                        foreach (XmlNode node3 in node.ChildNodes)
                        {
                            Point point4 = new Point(0, 0) {
                                Y = (component.Y + ((num5 + point2.Y) * i)) + baseLoc.Y,
                                X = (component.X + ((num4 + point2.X) * j)) + baseLoc.X
                            };
                            this.ChkAndSetFont(node3);
                            if (node3.Name == "container")
                            {
                                this.openContainer(outpageNode, node3, PageNo, RanNo + num, data, point4);
                            }
                            else
                            {
                                Point point5 = new Point(0, 0);
                                point5 = (Point) TypeDescriptor.GetConverter(point4).ConvertFromInvariantString(XmlFunc.getStringAttribute(node3, "Location"));
                                point4.Y += point5.Y;
                                point4.X += point5.X;
                                node2 = this.copyXmlNode(outpageNode, node3);
                                node2.Attributes.GetNamedItem("Location").Value = TypeDescriptor.GetConverter(point4).ConvertToInvariantString(point4);
                                if (node.Name == "container")
                                {
                                    this.cangeData(node2, PageNo, RanNo, data);
                                }
                                else
                                {
                                    this.cangeData(node2, PageNo, RanNo + num, data);
                                }
                            }
                        }
                    }
                }
            }
            return num;
        }

        private void setLocationAttribute(XmlNode source, Point location)
        {
            XmlAttribute namedItem = (XmlAttribute) source.Attributes.GetNamedItem("Location");
            if (namedItem == null)
            {
                namedItem = source.OwnerDocument.CreateAttribute("Location");
                namedItem.Value = TypeDescriptor.GetConverter(location).ConvertToInvariantString(location);
                source.Attributes.Append(namedItem);
            }
            else
            {
                namedItem.Value = TypeDescriptor.GetConverter(location).ConvertToInvariantString(location);
            }
        }

        private void setSizeAttribute(XmlNode source, System.Drawing.Size size)
        {
            XmlAttribute namedItem = (XmlAttribute) source.Attributes.GetNamedItem("Size");
            if (namedItem == null)
            {
                namedItem = source.OwnerDocument.CreateAttribute("Size");
                namedItem.Value = TypeDescriptor.GetConverter(size).ConvertToInvariantString(size);
                source.Attributes.Append(namedItem);
            }
            else
            {
                namedItem.Value = TypeDescriptor.GetConverter(size).ConvertToInvariantString(size);
            }
        }

        public int Contained
        {
            set
            {
                this.contained = value;
            }
        }

        public int ConvertMode
        {
            set
            {
                this.convertMode = value;
            }
        }

        public string InfoUniqueNo
        {
            set
            {
                this.infoUniqueNo = value;
            }
        }

        public AbstractJobConfig JobConf
        {
            set
            {
                this.jobConf = value;
                if (this.jobConf != null)
                {
                    switch (this.jobConf.jobStyle)
                    {
                        case JobStyle.combi:
                            this.mRecodeAtb = ((NormalConfig) this.jobConf).record;
                            return;

                        case JobStyle.divide:
                            this.mRecodeAtb = ((DivideConfig) this.jobConf).record;
                            return;
                    }
                    this.mRecodeAtb = ((NormalConfig) this.jobConf).record;
                }
            }
        }

        public int PrintPageNo
        {
            get
            {
                return this.mPrintPageNo;
            }
            set
            {
                this.mPrintPageNo = value;
            }
        }

        public string ServerRecvTime
        {
            set
            {
                this.serverRecvTime = value;
            }
        }

        public DateTime TimeStamp
        {
            set
            {
                this.mTimeStamp = value;
            }
        }
    }
}

