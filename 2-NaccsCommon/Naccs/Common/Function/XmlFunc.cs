namespace Naccs.Common.Function
{
    using System;
    using System.Xml;

    public sealed class XmlFunc
    {
        public static XmlNode copyXmlNode(XmlNode tergetNode, XmlNode refNode)
        {
            XmlElement newChild = tergetNode.OwnerDocument.CreateElement(refNode.Name);
            tergetNode.AppendChild(newChild);
            foreach (XmlAttribute attribute in refNode.Attributes)
            {
                newChild.Attributes.Append(tergetNode.OwnerDocument.CreateAttribute(attribute.Name));
                newChild.Attributes.GetNamedItem(attribute.Name).Value = attribute.Value;
            }
            newChild.InnerXml = refNode.InnerXml;
            return newChild;
        }

        public static XmlNode findItemInfo(XmlNode source, string name)
        {
            foreach (XmlNode node in source.ChildNodes)
            {
                if (node.Name == "item")
                {
                    if (name == getStringAttribute(node, "id"))
                    {
                        return node;
                    }
                }
                else
                {
                    XmlNode node2 = findItemInfo(node, name);
                    if (node2 != null)
                    {
                        return node2;
                    }
                }
            }
            return null;
        }

        public static bool getBoolAttribute(XmlNode source, string tag)
        {
            return getBoolAttribute(source, tag, false);
        }

        public static bool getBoolAttribute(XmlNode source, string tag, bool booldef)
        {
            string strA = getStringAttribute(source, tag);
            if (strA.Length > 0)
            {
                if (string.Compare(strA, "True", true) == 0)
                {
                    return true;
                }
                if (string.Compare(strA, "False", true) == 0)
                {
                    return false;
                }
            }
            return booldef;
        }

        public static int getNumericAttribute(XmlNode source, string tag)
        {
            return getNumericAttribute(source, tag, 0);
        }

        public static int getNumericAttribute(XmlNode source, string tag, int intdef)
        {
            string stTarget = getStringAttribute(source, tag);
            if ((stTarget.Length > 0) && Validation.IsNumeric(stTarget))
            {
                return int.Parse(stTarget);
            }
            return intdef;
        }

        public static string getStringAttribute(XmlNode source, string tag)
        {
            return getStringAttribute(source, tag, "");
        }

        public static string getStringAttribute(XmlNode source, string tag, string strdef)
        {
            XmlNode namedItem = source.Attributes.GetNamedItem(tag);
            if (namedItem != null)
            {
                return namedItem.Value;
            }
            return strdef;
        }

        public static void setBoolAttribute(XmlNode source, string tag, bool value)
        {
            string str;
            if (value)
            {
                str = "True";
            }
            else
            {
                str = "False";
            }
            setStringAttribute(source, tag, str);
        }

        public static void setNumericAttribute(XmlNode source, string tag, int value)
        {
            string str = value.ToString();
            setStringAttribute(source, tag, str);
        }

        public static void setStringAttribute(XmlNode source, string tag, string value)
        {
            XmlAttribute namedItem = (XmlAttribute) source.Attributes.GetNamedItem(tag);
            if (namedItem == null)
            {
                namedItem = source.OwnerDocument.CreateAttribute(tag);
                namedItem.Value = value;
                source.Attributes.Append(namedItem);
            }
            else
            {
                namedItem.Value = value;
            }
        }
    }
}

