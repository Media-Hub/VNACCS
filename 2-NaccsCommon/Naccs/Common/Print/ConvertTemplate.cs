namespace Naccs.Common.Print
{
    using Naccs.Common;
    using Naccs.Common.Constant;
    using Naccs.Common.CustomControls;
    using Naccs.Common.Function;
    using Naccs.Common.Properties;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Xml;

    public class ConvertTemplate
    {
        private const int CN_DateWidth = 0x43;
        private const string CN_ERR_Page2 = "Detail parts exceeding the page range cannot be converted.";
        private const string CN_ERR_RanTitle1 = "Title panel of column cannot be deployed in both X and Y directions.";
        private const string CN_ERR_RanTitle2 = "Title panel of column cannot be deployed in Y directions.";
        private const string CN_ERR_RanTitle3 = "Title panel of column cannot be deployed only in X directions.";
        private const string CN_ERR_TabPage1 = "Tab page structure is too complicated to be converted. (Multiple tabs contain other child elements than repetitive part.)";
        private const int CN_Height = 0x13;
        private const string CN_IsGRID = "isGrid";
        private const int CN_PageWidth = 0x4b;
        private const string CN_TabLabelColor = "Window";
        private const int CN_TimeWidth = 0x41;
        private const string CN_TitleText = "Title1";
        private CharsWidth mCharsWidth;
        private PrtProperties mCollection;
        private Font mDefaultFont;
        private string mFontName;
        private bool mPrintFlg;
        private ArrayList mPropertyList;
        private static ConvertTemplate singletonInstance;

        public ConvertTemplate()
        {
            this.initConvertTemplate();
        }

        public ConvertTemplate(bool printFlg)
        {
            this.mPrintFlg = printFlg;
            this.initConvertTemplate();
        }

        public XmlDocument AddItem(string templateFile, XmlNode addNode, int addPage)
        {
            XmlDocument owner = null;
            XmlDocument document2;
            try
            {
                owner = new XmlDocument();
                owner.Load(templateFile);
                XmlNodeList list = owner.SelectNodes("jobform/layout/page");
                for (int i = list.Count; i < addPage; i++)
                {
                    XmlNode node = owner.SelectSingleNode("jobform/layout");
                    node.AppendChild(this.createPageNode(node.OwnerDocument, 0));
                    this.appendPageTitle(owner, node.ChildNodes[i]);
                    list = owner.SelectNodes("jobform/layout/page");
                }
                XmlNode node1 = list[addPage - 1];
                node1.InnerXml = node1.InnerXml + addNode.InnerXml;
                document2 = owner;
            }
            catch (NaccsException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new NaccsException(MessageKind.Error, 0x261, exception.Message);
            }
            return document2;
        }

        private static void adjustLocation(XmlNode destNode, Point offset)
        {
            if (offset.ToString() != "0,0")
            {
                Point location = getLocationAttribute(destNode);
                location.X += offset.X;
                location.Y += offset.Y;
                setLocationAttribute(destNode, location);
            }
        }

        private void appendPageTitle(XmlDocument owner, XmlNode node)
        {
            System.Drawing.Size size = new System.Drawing.Size(SizeDef.LAYOUT_A4_WIDTH, (int) this.mDefaultFont.GetHeight());
            System.Drawing.Size size2 = new System.Drawing.Size(0x4b, (int) this.mDefaultFont.GetHeight());
            System.Drawing.Size size3 = new System.Drawing.Size(0x43, (int) this.mDefaultFont.GetHeight());
            System.Drawing.Size size4 = new System.Drawing.Size(0x41, (int) this.mDefaultFont.GetHeight());
            Point location = new Point(0, 0);
            Point point2 = new Point(SizeDef.LAYOUT_A4_WIDTH - size2.Width, 0);
            Point point3 = new Point((SizeDef.LAYOUT_A4_WIDTH - size3.Width) - size4.Width, (SizeDef.LAYOUT_A4_HEIGHT - size.Height) + 2);
            Point point4 = new Point(SizeDef.LAYOUT_A4_WIDTH - size4.Width, (SizeDef.LAYOUT_A4_HEIGHT - size.Height) + 2);
            if (this.mPrintFlg)
            {
                node.AppendChild(createPageLabelNode(owner, point4, size4, ContentAlignment.BottomRight, "PrintTime"));
                node.AppendChild(createPageLabelNode(owner, point3, size3, ContentAlignment.BottomRight, "PrintDate"));
            }
            node.AppendChild(createPageLabelNode(owner, point2, size2, ContentAlignment.MiddleRight, "PageProgress"));
            node.AppendChild(createPageLabelNode(owner, location, size, ContentAlignment.MiddleCenter, "PageTitle", "Title1"));
        }

        private void assembleLayout(XmlNode sourceCom, XmlNode sourceRan, XmlElement outLayout)
        {
            XmlElement source = null;
            System.Drawing.Size size2;
            System.Drawing.Size size = new System.Drawing.Size(SizeDef.LAYOUT_A4_WIDTH, (int) this.mDefaultFont.GetHeight());
            size2 = new System.Drawing.Size(SizeDef.LAYOUT_A4_WIDTH, SizeDef.LAYOUT_A4_HEIGHT) {
                Height = size.Height - (size.Height * 2)
            };
            Point point = new Point(0, 0);
            Point offset = new Point(0, size.Height);
            Point point3 = new Point(0, 0);
            System.Drawing.Size size3 = new System.Drawing.Size(0, 0);
            int num = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            if ((sourceRan != null) && (sourceRan.ChildNodes.Count > 0))
            {
                source = (XmlElement) sourceRan.SelectSingleNode("ran");
                if (source != null)
                {
                    point3 = getLocationAttribute(source);
                    size3 = getSizeAttribute(source);
                    if (size2.Height < size3.Height)
                    {
                        throw new NaccsException(MessageKind.Error, 0x25e, "Detail parts exceeding the page range cannot be converted.");
                    }
                    num = XmlFunc.getNumericAttribute(source, "RepX");
                    if (num < 1)
                    {
                        num = 1;
                    }
                    if (XmlFunc.getNumericAttribute(source, "RepY") < 1)
                    {
                    }
                    num3 = XmlFunc.getNumericAttribute(source, "SpaceY");
                    num4 = XmlFunc.getNumericAttribute(source, "repetition_max");
                }
            }
            if (sourceCom.ChildNodes.Count > 0)
            {
                XmlNode node = outLayout.OwnerDocument.CreateElement("ComPage");
                node.AppendChild(this.createPageNode(node.OwnerDocument, 1));
                this.appendPageTitle(node.OwnerDocument, node.ChildNodes[0]);
                int num7 = 0;
                int num8 = 0;
                int y = offset.Y;
                XmlNodeList childNodes = sourceCom.ChildNodes;
                foreach (ItemStruct struct2 in this.GetNodeItems(childNodes))
                {
                    XmlNode node2 = childNodes[struct2.NodeIndex];
                    Point location = struct2.Location;
                    System.Drawing.Size size4 = struct2.Size;
                    if (location.Y > ((num7 + size2.Height) - size.Height))
                    {
                        num8++;
                        node.AppendChild(this.createPageNode(node2.OwnerDocument, 1));
                        this.appendPageTitle(node.OwnerDocument, node.ChildNodes[num8]);
                        if (location.Y > (num7 + size2.Height))
                        {
                            num7 += size2.Height;
                        }
                        else
                        {
                            num7 = location.Y;
                        }
                        y = point.Y + size.Height;
                    }
                    if (num8 > 0)
                    {
                        location.Y -= num7;
                        setLocationAttribute(node2, location);
                    }
                    XmlNode newChild = assembleLayoutNode(node2, offset);
                    node.ChildNodes[num8].AppendChild(newChild);
                    if (y < ((location.Y + offset.Y) + size4.Height))
                    {
                        y = (location.Y + offset.Y) + size4.Height;
                    }
                    if ((Application.ProductName.IndexOf("lauber", StringComparison.OrdinalIgnoreCase) > -1) && ((location.Y + size4.Height) > size2.Height))
                    {
                        string str;
                        if (node2.Name == "label")
                        {
                            str = string.Format(Resources.ResourceManager.GetString("COM06"), XmlFunc.getStringAttribute(newChild, "Text"));
                        }
                        else
                        {
                            str = string.Format(Resources.ResourceManager.GetString("COM07"), XmlFunc.getStringAttribute(newChild, "id"), XmlFunc.getStringAttribute(newChild, "name"));
                        }
                        MessageBox.Show(string.Format(Resources.ResourceManager.GetString("COM08"), num8 + 1, str), Resources.ResourceManager.GetString("COM09"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                int num10 = size2.Height - (y + point3.Y);
                int num11 = 0;
                if (size3.Height != 0)
                {
                    int num12 = size3.Height + num3;
                    num11 = (num10 + num3) / num12;
                }
                if (num11 > 0)
                {
                    num5 = num11 * num;
                    num6 = (num4 < num5) ? num4 : num5;
                    Point point5 = new Point(offset.X, y + size.Height);
                    XmlFunc.setNumericAttribute(source, "Count", num6);
                    int num13 = (num6 < num4) ? num11 : intDivCeiling(num6, num);
                    XmlFunc.setNumericAttribute(source, "RepY", num13);
                    foreach (XmlNode node4 in sourceRan.ChildNodes)
                    {
                        node.LastChild.AppendChild(assembleLayoutNode(node4, point5));
                    }
                }
                foreach (XmlNode node5 in node.ChildNodes)
                {
                    XmlNode node6 = node5.Clone();
                    outLayout.AppendChild(node6);
                }
            }
            if (((source != null) && (source.ChildNodes.Count > 0)) && (num5 < num4))
            {
                XmlNode node7 = this.createPageNode(outLayout.OwnerDocument, 0);
                this.appendPageTitle(outLayout.OwnerDocument, node7);
                int num14 = num4 - num5;
                int num15 = 0;
                if (size3.Height != 0)
                {
                    int num16 = size3.Height + num3;
                    num15 = (size2.Height + num3) / num16;
                }
                num6 = (num14 < (num15 * num)) ? num14 : (num15 * num);
                XmlFunc.setNumericAttribute(source, "Offset", num5);
                XmlFunc.setNumericAttribute(source, "Count", num6);
                int num17 = (num6 < num14) ? num15 : intDivCeiling(num6, num);
                XmlFunc.setNumericAttribute(source, "RepY", num17);
                XmlFunc.setNumericAttribute(node7, "MaxOccurs", intDivCeiling(num14, num6));
                foreach (XmlNode node8 in sourceRan.ChildNodes)
                {
                    node7.AppendChild(assembleLayoutNode(node8, offset));
                }
                outLayout.AppendChild(node7);
            }
            if (!this.mPrintFlg && (outLayout.ChildNodes.Count > 3))
            {
                throw new NaccsException(MessageKind.Error, 0x25e, Resources.ResourceManager.GetString("COM12"));
            }
        }

        private static XmlNode assembleLayoutNode(XmlNode source, Point offset)
        {
            XmlNode destNode = source.Clone();
            adjustLocation(destNode, offset);
            return destNode;
        }

        private void convertContainer(XmlNode page, XmlNode source)
        {
            Point point2 = getOffset(source);
            string str = XmlFunc.getStringAttribute(source, "type");
            if (((str == "TabPage") || (str == "GroupBox")) || (str == "Panel"))
            {
                Point point;
                System.Drawing.Size size;
                if (str == "TabPage")
                {
                    point = getLocationAttribute(source.ParentNode);
                    size = getSizeAttribute(source.ParentNode);
                }
                else
                {
                    point = getLocationAttribute(source);
                    size = getSizeAttribute(source);
                }
                point.X += point2.X;
                point.Y += point2.Y;
                bool flag = false;
                switch (str)
                {
                    case "Panel":
                    case "TabPage":
                    {
                        string str2 = XmlFunc.getStringAttribute(source, "BorderStyle").Trim();
                        if (str2.Length != 0)
                        {
                            flag = str2 != BorderStyle.None.ToString();
                        }
                        break;
                    }
                    default:
                        flag = true;
                        break;
                }
                if (flag)
                {
                    XmlNode destNode = page.OwnerDocument.CreateElement("rectangle");
                    this.setAttribute(destNode, source);
                    if (str == "TabPage")
                    {
                        setLocationAttribute(destNode, point);
                        setSizeAttribute(destNode, size);
                    }
                    else
                    {
                        setLocationAttribute(destNode, point);
                    }
                    if (str != "Panel")
                    {
                        string str3 = XmlFunc.getStringAttribute(source, "Text");
                        if (str3.Length != 0)
                        {
                            XmlNode node2 = page.OwnerDocument.CreateElement("label");
                            this.setAttribute(node2, source);
                            point.X += 20;
                            point.Y -= (int) (this.mDefaultFont.GetHeight() / 2f);
                            setLocationAttribute(node2, point);
                            size.Width = this.mCharsWidth.getWidth(str3);
                            size.Height = (int) this.mDefaultFont.GetHeight();
                            setSizeAttribute(node2, size);
                            XmlFunc.setStringAttribute(node2, "BackColor", "Window");
                            page.AppendChild(node2);
                        }
                    }
                    page.AppendChild(destNode);
                }
            }
        }

        public XmlDocument convertDisplayToReport(string templateFile)
        {
            XmlDocument doc = null;
            XmlDocument document2;
            try
            {
                doc = new XmlDocument();
                doc.Load(templateFile);
                XmlElement newChild = this.convertLayout(doc);
                XmlNodeList list = doc.SelectNodes("jobform/layout");
                if (list.Count > 0)
                {
                    list[0].ParentNode.RemoveChild(list[0]);
                }
                XmlNodeList list2 = doc.SelectNodes("jobform");
                if (list2.Count > 0)
                {
                    list2[0].AppendChild(newChild);
                }
                document2 = doc;
            }
            catch (NaccsException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new NaccsException(MessageKind.Error, 0x261, exception.Message);
            }
            return document2;
        }

        private XmlElement convertLayout(XmlDocument doc)
        {
            XmlElement outLayout = null;
            XmlNodeList list = doc.SelectNodes("jobform/layout");
            if (list.Count > 0)
            {
                XmlNode node3;
                XmlAttribute namedItem = (XmlAttribute) list[0].Attributes.GetNamedItem("Font");
                if ((namedItem != null) && (namedItem.Value.Length != 0))
                {
                    this.setDefaultFont(namedItem.Value);
                }
                outLayout = doc.CreateElement("layout");
                XmlNode source = list[0].Clone();
                int height = 0;
                XmlNode node2 = null;
                do
                {
                    node3 = this.separationNode(source, 1);
                    if (node3 == null)
                    {
                        break;
                    }
                    Point location = getLocationAttribute(node3);
                    System.Drawing.Size size = getSizeAttribute(node3);
                    int num2 = XmlFunc.getNumericAttribute(node3, "RepX");
                    int num3 = XmlFunc.getNumericAttribute(node3, "RepY");
                    if ((num3 > 1) && (num2 > 1))
                    {
                        throw new NaccsException(MessageKind.Error, 0x25e, "Title panel of column cannot be deployed in both X and Y directions.");
                    }
                    if (num3 > 1)
                    {
                        throw new NaccsException(MessageKind.Error, 0x25e, "Title panel of column cannot be deployed in Y directions.");
                    }
                    if (num2 > 0)
                    {
                        if (node2 != null)
                        {
                            throw new NaccsException(MessageKind.Error, 0x25e, "Title panel of column cannot be deployed only in X directions.");
                        }
                        node2 = node3;
                        height = size.Height;
                        location.Y = 0;
                        setLocationAttribute(node2, location);
                    }
                }
                while (!(XmlFunc.getStringAttribute(node3, "isGrid") == "True"));
                XmlNode node4 = this.separationNode(source, 0);
                if (node4 != null)
                {
                    Point point2 = getLocationAttribute(node4);
                    point2.Y = height;
                    setLocationAttribute(node4, point2);
                }
                Point offset = new Point(0, 0);
                XmlNode page = this.createPageNode(outLayout.OwnerDocument, 1);
                string font = "";
                foreach (XmlNode node6 in source.ChildNodes)
                {
                    this.convertXmlNode(page, node6, font, offset);
                }
                XmlNode sourceRan = this.createPageNode(outLayout.OwnerDocument, 0);
                if (node2 != null)
                {
                    sourceRan.AppendChild(node2);
                }
                if (node4 != null)
                {
                    XmlNode node8 = this.createRanNode(sourceRan.OwnerDocument, node4);
                    foreach (XmlNode node9 in node4.ChildNodes)
                    {
                        this.convertXmlNode(node8, node9, "", getOffset(node9));
                    }
                    XmlNode newChild = this.createRectNode(node8.OwnerDocument, node4, new Point(0, 0), getSizeAttribute(node4));
                    node8.AppendChild(newChild);
                    sourceRan.AppendChild(node8);
                }
                this.assembleLayout(page, sourceRan, outLayout);
            }
            return outLayout;
        }

        public XmlDocument convertTitle(string templateFile)
        {
            XmlDocument document = null;
            XmlDocument document2;
            try
            {
                document = new XmlDocument();
                document.Load(templateFile);
                XmlNodeList list = document.SelectNodes("jobform/layout/page");
                for (int i = 0; i < list.Count; i++)
                {
                    int num2 = 0;
                    XmlNode source = list[i];
                    while (num2 < source.ChildNodes.Count)
                    {
                        if (source.ChildNodes[num2].Name == "pagelabel")
                        {
                            source.RemoveChild(source.ChildNodes[num2]);
                        }
                        else
                        {
                            num2++;
                        }
                    }
                    Point point = this.getFinalLinePoint(source);
                    int height = (int) this.mDefaultFont.GetHeight();
                    source.PrependChild(createPageLabelNode(source.OwnerDocument, new Point(point.X - 0x41, point.Y), new System.Drawing.Size(0x41, height), ContentAlignment.BottomRight, "PrintTime"));
                    source.PrependChild(createPageLabelNode(source.OwnerDocument, new Point((point.X - 0x43) - 0x41, point.Y), new System.Drawing.Size(0x43, height), ContentAlignment.BottomRight, "PrintDate"));
                    source.PrependChild(createPageLabelNode(source.OwnerDocument, new Point(0, 0), new System.Drawing.Size(point.X, height), ContentAlignment.MiddleCenter, "PageTitle", "Title1"));
                    source.PrependChild(createPageLabelNode(source.OwnerDocument, new Point(point.X - 0x4b, 0), new System.Drawing.Size(0x4b, height), ContentAlignment.MiddleRight, "PageProgress"));
                }
                document2 = document;
            }
            catch (NaccsException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new NaccsException(MessageKind.Error, 0x261, exception.Message);
            }
            return document2;
        }

        private void convertXmlNode(XmlNode page, XmlNode source, string font, Point offset)
        {
            XmlElement destNode = null;
            if (((source.Name == "label") || (source.Name == "ranlabel")) || ((source.Name == "checkbox") || (source.Name == "radio")))
            {
                destNode = page.OwnerDocument.CreateElement(source.Name);
                this.setAttribute(destNode, source);
                if (this.mPrintFlg || !string.IsNullOrEmpty(font))
                {
                    setParentFont(destNode, font);
                }
                adjustLocation(destNode, offset);
                page.AppendChild(destNode);
            }
            else if ((((source.Name == "textbox") || (source.Name == "maskedtextbox")) || ((source.Name == "combobox") || (source.Name == "inputcombobox"))) || ((source.Name == "datetimepicker") || (source.Name == "commatext")))
            {
                destNode = page.OwnerDocument.CreateElement("prttextbox");
                this.setAttribute(destNode, source);
                if (this.mPrintFlg || !string.IsNullOrEmpty(font))
                {
                    setParentFont(destNode, font);
                }
                System.Type textBox = DesignControls.TextBox;
                if (source.Name == "maskedtextbox")
                {
                    textBox = DesignControls.MaskedTextBox;
                    XmlFunc.setStringAttribute(destNode, "Format", getMaskFormat(source));
                }
                else if (source.Name == "datetimepicker")
                {
                    textBox = DesignControls.DataTimePicker;
                    XmlFunc.setStringAttribute(destNode, "Format", getDateTimeFormat(source));
                }
                else if (source.Name == "commatext")
                {
                    textBox = DesignControls.CommaText;
                    XmlFunc.setStringAttribute(destNode, "Format", getCommaFormat(source));
                }
                if (XmlFunc.getStringAttribute(source, "isGrid") != "True")
                {
                    string borderStyle = XmlFunc.getStringAttribute(source, "BorderStyle").Trim();
                    if (borderStyle.Length == 0)
                    {
                        borderStyle = ComponentProperty.getPropertyDefault(textBox, "BorderStyle");
                        if (borderStyle == null)
                        {
                            borderStyle = BorderStyle.None.ToString();
                        }
                    }
                    int num = SizeDef.prtTextMargin(borderStyle);
                    Point location = getLocationAttribute(destNode);
                    location.Y += num;
                    setLocationAttribute(destNode, location);
                    System.Drawing.Size size = getSizeAttribute(destNode);
                    size.Height -= num * 2;
                    setSizeAttribute(destNode, size);
                    XmlFunc.setStringAttribute(destNode, "BorderStyle", BorderStyle.None.ToString());
                }
                adjustLocation(destNode, offset);
                page.AppendChild(destNode);
            }
            else if (source.Name == "div-textbox")
            {
                destNode = page.OwnerDocument.CreateElement("prttextbox");
                this.setAttribute(destNode, source);
                if (this.mPrintFlg || !string.IsNullOrEmpty(font))
                {
                    setParentFont(destNode, font);
                }
                System.Type maskedTextBox = DesignControls.MaskedTextBox;
                int cnt = XmlFunc.getNumericAttribute(source, "L_MaxLength");
                int num3 = XmlFunc.getNumericAttribute(source, "R_MaxLength");
                StrFunc func = StrFunc.CreateInstance();
                string str2 = func.MakeCycleStr(cnt, "_") + " - " + func.MakeCycleStr(num3, "_");
                XmlFunc.setStringAttribute(destNode, "Format", str2);
                string str3 = XmlFunc.getStringAttribute(source, "BorderStyle").Trim();
                if (str3.Length == 0)
                {
                    str3 = BorderStyle.None.ToString();
                }
                int num4 = SizeDef.prtTextMargin(str3) + SizeDef.prtTextMargin(BorderStyle.Fixed3D.ToString());
                Point point2 = getLocationAttribute(destNode);
                point2.Y += num4;
                setLocationAttribute(destNode, point2);
                System.Drawing.Size size2 = getSizeAttribute(destNode);
                size2.Width -= num4 * 2;
                size2.Height -= num4 * 2;
                setSizeAttribute(destNode, size2);
                XmlFunc.setStringAttribute(destNode, "BorderStyle", BorderStyle.None.ToString());
                adjustLocation(destNode, offset);
                page.AppendChild(destNode);
            }
            else if ((source.Name == "container") && isContinueType(source))
            {
                if (XmlFunc.getStringAttribute(source, "type") == "TitlePanel")
                {
                    page.AppendChild(source.Clone());
                }
                else if (!isRepetition(source))
                {
                    if (!this.isConvertContainer(source))
                    {
                        throw new NaccsException(MessageKind.Error, 0x25e, "Tab page structure is too complicated to be converted. (Multiple tabs contain other child elements than repetitive part.)");
                    }
                    if (this.isContainerChilds(source))
                    {
                        string str4 = XmlFunc.getStringAttribute(source, "Font");
                        if (str4.Length == 0)
                        {
                            str4 = font;
                        }
                        foreach (XmlNode node in source.ChildNodes)
                        {
                            this.convertXmlNode(page, node, str4, getOffset(node));
                        }
                        this.convertContainer(page, source);
                    }
                }
            }
        }

        public static void copyAttribute(XmlNode destNode, XmlNode source, string name)
        {
            XmlAttribute namedItem = (XmlAttribute) source.Attributes.GetNamedItem(name);
            if (namedItem != null)
            {
                XmlAttribute node = destNode.OwnerDocument.CreateAttribute(name);
                node.Value = namedItem.Value;
                destNode.Attributes.Append(node);
            }
        }

        public static ConvertTemplate CreateInstance()
        {
            return CreateInstance(false);
        }

        public static ConvertTemplate CreateInstance(bool printFlg)
        {
            if (singletonInstance == null)
            {
                singletonInstance = new ConvertTemplate(printFlg);
            }
            return singletonInstance;
        }

        private static XmlNode createPageLabelNode(XmlDocument doc, Point location, System.Drawing.Size size, ContentAlignment align, string format)
        {
            XmlNode source = doc.CreateElement("pagelabel");
            setLocationAttribute(source, location);
            setSizeAttribute(source, size);
            XmlFunc.setStringAttribute(source, "TextAlign", align.ToString());
            XmlFunc.setStringAttribute(source, "PageFormat", format);
            return source;
        }

        private static XmlNode createPageLabelNode(XmlDocument doc, Point location, System.Drawing.Size size, ContentAlignment align, string format, string text)
        {
            XmlNode source = doc.CreateElement("pagelabel");
            setLocationAttribute(source, location);
            setSizeAttribute(source, size);
            XmlFunc.setStringAttribute(source, "TextAlign", align.ToString());
            XmlFunc.setStringAttribute(source, "PageFormat", format);
            XmlFunc.setStringAttribute(source, "Text", text);
            return source;
        }

        private XmlNode createPageNode(XmlDocument doc, int minOccurs)
        {
            XmlNode source = doc.CreateElement("page");
            XmlFunc.setStringAttribute(source, "PaperSize", "A4");
            XmlFunc.setStringAttribute(source, "PrintMargin", string.Format("{0:d},{0:d},{0:d},{0:d}", new object[] { SizeDef.MARGINS_LEFT, SizeDef.MARGINS_TOP, SizeDef.MARGINS_RIGHT, SizeDef.MARGINS_BOTTOM }));
            XmlFunc.setStringAttribute(source, "LandScape", "False");
            XmlFunc.setNumericAttribute(source, "MinOccurs", minOccurs);
            XmlFunc.setNumericAttribute(source, "MaxOccurs", 1);
            return source;
        }

        private XmlNode createRanNode(XmlDocument doc, XmlNode source)
        {
            XmlNode node = doc.CreateElement("ran");
            this.setAttribute((XmlElement) node, source);
            XmlFunc.setNumericAttribute(node, "Offset", 0);
            XmlFunc.setNumericAttribute(node, "Count", 0);
            return node;
        }

        private XmlNode createRectNode(XmlDocument doc, XmlNode source, Point location, System.Drawing.Size size)
        {
            XmlNode destNode = doc.CreateElement("rectangle");
            this.setAttribute(destNode, source);
            setLocationAttribute(destNode, location);
            setSizeAttribute(destNode, size);
            return destNode;
        }

        private static string getCommaFormat(XmlNode source)
        {
            string str = "";
            if (string.Compare(XmlFunc.getStringAttribute(source, "IsComma").Trim(), "TRUE", true) == 0)
            {
                str = "###,###";
            }
            else
            {
                str = "######";
            }
            return (XmlFunc.getStringAttribute(source, "CurrencyMark").Trim() + str);
        }

        private ArrayList getControlProperties(string name, PropertiesCollection.CategoryKind enCategoryKind)
        {
            ArrayList expansion = null;
            _Property property = new _Property();
            for (int i = 0; i < this.mPropertyList.Count; i++)
            {
                property = (_Property) this.mPropertyList[i];
                if (property.name == name)
                {
                    if (enCategoryKind == PropertiesCollection.CategoryKind.Basic)
                    {
                        return property.Basic;
                    }
                    if (enCategoryKind == PropertiesCollection.CategoryKind.Expansion)
                    {
                        return property.Expansion;
                    }
                }
            }
            property.name = name;
            string controlName = PrtProperties.getControlName(name);
            property.Basic = this.mCollection.getPropertyList(controlName, PropertiesCollection.CategoryKind.Basic, PropertiesCollection.BooleanKind.True);
            property.Expansion = this.mCollection.getPropertyList(controlName, PropertiesCollection.CategoryKind.Expansion, PropertiesCollection.BooleanKind.True);
            this.mPropertyList.Add(property);
            if (enCategoryKind == PropertiesCollection.CategoryKind.Basic)
            {
                return property.Basic;
            }
            if (enCategoryKind == PropertiesCollection.CategoryKind.Expansion)
            {
                expansion = property.Expansion;
            }
            return expansion;
        }

        private static string getDateTimeFormat(XmlNode source)
        {
            string str = "dd/MM/yyyy";
            switch (XmlFunc.getStringAttribute(source, "Format"))
            {
                case "Time":
                    return "HH:mm:ss";

                case "Custom":
                    str = XmlFunc.getStringAttribute(source, "CustomFormat");
                    break;
            }
            return str;
        }

        private Point getFinalLinePoint(XmlNode source)
        {
            string str = XmlFunc.getStringAttribute(source, "PaperSize", "A4");
            if (XmlFunc.getBoolAttribute(source, "LandScape", false))
            {
                switch (str)
                {
                    case "A4":
                        return new Point(SizeDef.LAYOUT_A4_HEIGHT, SizeDef.LAYOUT_A4_WIDTH - 10);

                    case "A3":
                        return new Point(SizeDef.LAYOUT_A3_HEIGHT, SizeDef.LAYOUT_A3_WIDTH - 10);
                }
                return new Point(SizeDef.LAYOUT_A5_HEIGHT, SizeDef.LAYOUT_A5_WIDTH - 10);
            }
            if (str == "A4")
            {
                return new Point(SizeDef.LAYOUT_A4_WIDTH, SizeDef.LAYOUT_A4_HEIGHT - 10);
            }
            if (str == "A3")
            {
                return new Point(SizeDef.LAYOUT_A3_WIDTH, SizeDef.LAYOUT_A3_HEIGHT - 10);
            }
            return new Point(SizeDef.LAYOUT_A5_WIDTH, SizeDef.LAYOUT_A5_HEIGHT - 10);
        }

        private static Point getLocationAttribute(XmlNode source)
        {
            Point component = new Point(0, 0);
            XmlAttribute namedItem = (XmlAttribute) source.Attributes.GetNamedItem("Location");
            if ((namedItem != null) && (namedItem.Value.Length != 0))
            {
                component = (Point) TypeDescriptor.GetConverter(component).ConvertFromInvariantString(namedItem.Value);
            }
            return component;
        }

        private static string getMaskFormat(XmlNode source)
        {
            return XmlFunc.getStringAttribute(source, "Mask");
        }

        private List<ItemStruct> GetNodeItems(XmlNodeList list)
        {
            List<ItemStruct> list2 = new List<ItemStruct>();
            int num = 0;
            foreach (XmlNode node in list)
            {
                ItemStruct item = new ItemStruct {
                    Location = getLocationAttribute(node),
                    Size = getSizeAttribute(node),
                    NodeIndex = num
                };
                list2.Add(item);
                num++;
            }
            list2.Sort(new Comparison<ItemStruct>(this.ItemCompare));
            return list2;
        }

        private static Point getOffset(XmlNode source)
        {
            Point point = new Point(0, 0);
            for (XmlNode node = source.ParentNode; node != null; node = node.ParentNode)
            {
                if (node.Name == "container")
                {
                    XmlAttribute namedItem = (XmlAttribute) node.Attributes.GetNamedItem("type");
                    if (((namedItem != null) && ((namedItem.Value == "GroupBox") || (namedItem.Value == "Panel"))) && isRepetition(node))
                    {
                        return point;
                    }
                }
                Point point2 = getLocationAttribute(node);
                point.X += point2.X;
                point.Y += point2.Y;
            }
            return point;
        }

        private static System.Drawing.Size getSizeAttribute(XmlNode source)
        {
            System.Drawing.Size component = new System.Drawing.Size(0, 0);
            XmlAttribute namedItem = (XmlAttribute) source.Attributes.GetNamedItem("Size");
            if ((namedItem != null) && (namedItem.Value.Length != 0))
            {
                component = (System.Drawing.Size) TypeDescriptor.GetConverter(component).ConvertFromInvariantString(namedItem.Value);
            }
            return component;
        }

        private XmlNode gridToContainer(XmlNode source)
        {
            XmlNode node = source.OwnerDocument.CreateElement("container");
            XmlFunc.setStringAttribute(node, "type", "Panel");
            foreach (XmlAttribute attribute in source.Attributes)
            {
                XmlFunc.setStringAttribute(node, attribute.Name, attribute.Value);
            }
            XmlFunc.setStringAttribute(node, "isGrid", "True");
            XmlFunc.setNumericAttribute(node, "RepX", 1);
            XmlFunc.setNumericAttribute(node, "RepY", 1);
            Point location = getLocationAttribute(source);
            location.Y = 0;
            setLocationAttribute(node, location);
            location.X = 0;
            int width = 0;
            int height = 0x13;
            foreach (XmlNode node2 in source.ChildNodes)
            {
                if (XmlFunc.getBoolAttribute(node2, "Visible", true))
                {
                    XmlNode destNode = node2.OwnerDocument.CreateElement("prttextbox");
                    this.setAttribute(destNode, node2);
                    XmlNode node4 = node2.OwnerDocument.CreateElement("textbox");
                    foreach (XmlAttribute attribute2 in destNode.Attributes)
                    {
                        XmlFunc.setStringAttribute(node4, attribute2.Name, attribute2.Value);
                    }
                    int num3 = XmlFunc.getNumericAttribute(node2, "Width", 100);
                    setSizeAttribute(node4, new System.Drawing.Size(num3, height));
                    setLocationAttribute(node4, location);
                    location.X += num3;
                    width += num3;
                    XmlFunc.setStringAttribute(node4, "BorderStyle", BorderStyle.FixedSingle.ToString());
                    XmlFunc.setStringAttribute(node4, "isGrid", "True");
                    XmlNode node5 = node2.SelectSingleNode("columnstyle");
                    if (node5 != null)
                    {
                        foreach (XmlAttribute attribute3 in node5.Attributes)
                        {
                            if (attribute3.Name == "Alignment")
                            {
                                if (attribute3.Value.Contains(HorizontalAlignment.Center.ToString()))
                                {
                                    XmlFunc.setStringAttribute(node4, "TextAlign", HorizontalAlignment.Center.ToString());
                                }
                                else if (attribute3.Value.Contains(HorizontalAlignment.Right.ToString()))
                                {
                                    XmlFunc.setStringAttribute(node4, "TextAlign", HorizontalAlignment.Right.ToString());
                                }
                                else
                                {
                                    XmlFunc.setStringAttribute(node4, "TextAlign", HorizontalAlignment.Left.ToString());
                                }
                            }
                            else
                            {
                                XmlFunc.setStringAttribute(node4, attribute3.Name, attribute3.Value);
                            }
                        }
                    }
                    node.AppendChild(node4);
                }
            }
            setSizeAttribute(node, new System.Drawing.Size(width, height));
            return node;
        }

        private XmlNode gridToTitlePanel(XmlNode source)
        {
            XmlNode node = source.OwnerDocument.CreateElement("container");
            XmlFunc.setStringAttribute(node, "type", "TitlePanel");
            foreach (XmlAttribute attribute in source.Attributes)
            {
                XmlFunc.setStringAttribute(node, attribute.Name, attribute.Value);
            }
            XmlFunc.setStringAttribute(node, "IsRan", "True");
            XmlFunc.setStringAttribute(node, "isGrid", "True");
            XmlFunc.setNumericAttribute(node, "RepX", 1);
            XmlFunc.setNumericAttribute(node, "RepY", 1);
            Point location = getLocationAttribute(source);
            location.Y = 0;
            setLocationAttribute(node, location);
            location.X = 0;
            int width = 0;
            int height = 0x13;
            foreach (XmlNode node2 in source.ChildNodes)
            {
                if (XmlFunc.getBoolAttribute(node2, "Visible", true))
                {
                    XmlNode destNode = node2.OwnerDocument.CreateElement("label");
                    this.setAttribute(destNode, node2);
                    int num3 = XmlFunc.getNumericAttribute(node2, "Width", 100);
                    setSizeAttribute(destNode, new System.Drawing.Size(num3, height));
                    setLocationAttribute(destNode, location);
                    location.X += num3;
                    width += num3;
                    XmlFunc.setStringAttribute(destNode, "AutoSize", "False");
                    XmlFunc.setStringAttribute(destNode, "Text", XmlFunc.getStringAttribute(node2, "HeaderText"));
                    XmlFunc.setStringAttribute(destNode, "BorderStyle", BorderStyle.Fixed3D.ToString());
                    XmlFunc.setStringAttribute(destNode, "isGrid", "True");
                    XmlNode node4 = node2.SelectSingleNode("columnstyle");
                    if (node4 != null)
                    {
                        foreach (XmlAttribute attribute2 in node4.Attributes)
                        {
                            if (attribute2.Name == "Alignment")
                            {
                                XmlFunc.setStringAttribute(destNode, "TextAlign", attribute2.Value);
                            }
                            else
                            {
                                XmlFunc.setStringAttribute(destNode, attribute2.Name, attribute2.Value);
                            }
                        }
                    }
                    XmlFunc.setStringAttribute(destNode, "BackColor", "Window");
                    node.AppendChild(destNode);
                }
            }
            setSizeAttribute(node, new System.Drawing.Size(width, height));
            return node;
        }

        private void initConvertTemplate()
        {
            this.mFontName = "";
            this.setDefaultFont("Courier New, 9pt");
            this.mCollection = new PrtProperties();
            this.mPropertyList = new ArrayList();
        }

        private static int intDivCeiling(int val1, int val2)
        {
            if ((val1 % val2) <= 0)
            {
                return (val1 / val2);
            }
            return ((val1 / val2) + 1);
        }

        private bool isContainerChilds(XmlNode source)
        {
            if (source.ChildNodes.Count > 0)
            {
                foreach (XmlNode node in source.ChildNodes)
                {
                    if (node.Name == "container")
                    {
                        if (this.isContainerChilds(node))
                        {
                            return true;
                        }
                    }
                    else if (node.Name != "navigator")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool isContinueType(XmlNode source)
        {
            XmlAttribute namedItem = (XmlAttribute) source.Attributes.GetNamedItem("type");
            if (namedItem == null)
            {
                return false;
            }
            if ((!(namedItem.Value == "GroupBox") && !(namedItem.Value == "Panel")) && (!(namedItem.Value == "TabControl") && !(namedItem.Value == "TabPage")))
            {
                return (namedItem.Value == "TitlePanel");
            }
            return true;
        }

        private bool isConvertContainer(XmlNode source)
        {
            if (XmlFunc.getStringAttribute(source, "type") == "TabControl")
            {
                bool flag = false;
                foreach (XmlNode node in source.ChildNodes)
                {
                    if (this.isContainerChilds(node))
                    {
                        if (flag)
                        {
                            return false;
                        }
                        flag = true;
                    }
                }
            }
            return true;
        }

        private static bool isRepetition(XmlNode source)
        {
            XmlAttribute namedItem = (XmlAttribute) source.Attributes.GetNamedItem("repetition_id");
            if ((namedItem != null) && (namedItem.Value.Length != 0))
            {
                if (namedItem.Value.Substring(0, 1).Equals("R"))
                {
                    return true;
                }
                if (namedItem.Value.Substring(0, 1).Equals("C"))
                {
                    return true;
                }
            }
            return false;
        }

        private int ItemCompare(ItemStruct x, ItemStruct y)
        {
            int num = x.Location.Y + x.Size.Height;
            return num.CompareTo((int) (y.Location.Y + y.Size.Height));
        }

        private XmlNode separationNode(XmlNode source, int separation)
        {
            XmlNode node = null;
            XmlNode node2 = source.SelectSingleNode("datagrid");
            if (node2 != null)
            {
                if (separation == 0)
                {
                    node = this.gridToContainer(node2);
                    source.RemoveChild(node2);
                    return node;
                }
                return this.gridToTitlePanel(node2);
            }
            foreach (XmlNode node3 in source.SelectNodes("container"))
            {
                if (isContinueType(node3))
                {
                    bool flag = false;
                    if (separation == 0)
                    {
                        flag = isRepetition(node3);
                    }
                    else
                    {
                        if (separation != 1)
                        {
                            return node;
                        }
                        flag = (XmlFunc.getStringAttribute(node3, "type") == "TitlePanel") && (XmlFunc.getStringAttribute(node3, "IsRan").Trim().ToUpper() == "TRUE");
                    }
                    if (flag)
                    {
                        node = node3;
                        source.RemoveChild(node3);
                        return node;
                    }
                    node = this.separationNode(node3, separation);
                    if (node != null)
                    {
                        return node;
                    }
                }
            }
            return node;
        }

        private void setAttribute(XmlNode destNode, XmlNode source)
        {
            foreach (string str in this.getControlProperties(destNode.Name, PropertiesCollection.CategoryKind.Basic))
            {
                copyAttribute(destNode, source, str);
            }
            foreach (string str2 in this.getControlProperties(destNode.Name, PropertiesCollection.CategoryKind.Expansion))
            {
                copyAttribute(destNode, source, str2);
            }
        }

        private void setDefaultFont(string value)
        {
            if (this.mFontName != value)
            {
                if (this.mDefaultFont != null)
                {
                    this.mDefaultFont.Dispose();
                }
                this.mDefaultFont = new Font("Tahoma", 9f);
                this.mDefaultFont = (Font) TypeDescriptor.GetConverter(this.mDefaultFont).ConvertFromString(value);
                if (this.mCharsWidth != null)
                {
                    this.mCharsWidth.disposeGraphics();
                }
                this.mCharsWidth = new CharsWidth(value);
                this.mFontName = value;
            }
        }

        private static void setLocationAttribute(XmlNode source, Point location)
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

        private static void setParentFont(XmlNode destNode, string value)
        {
            if (XmlFunc.getStringAttribute(destNode, "Font").Length == 0)
            {
                XmlFunc.setStringAttribute(destNode, "Font", value);
            }
        }

        private static void setSizeAttribute(XmlNode source, System.Drawing.Size size)
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

        [StructLayout(LayoutKind.Sequential)]
        private struct _Property
        {
            public string name;
            public ArrayList Basic;
            public ArrayList Expansion;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct ItemStruct
        {
            public Point Location;
            public System.Drawing.Size Size;
            public int NodeIndex;
        }
    }
}

