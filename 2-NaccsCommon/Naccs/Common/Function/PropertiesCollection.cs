namespace Naccs.Common.Function
{
    using Naccs.Common.Properties;
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Xml;

    public class PropertiesCollection
    {
        public ControlCollection Controls;
        private XmlDocument mPropertyDoc;

        public ArrayList getPropertyList(string controlName, CategoryKind enCategory, BooleanKind enVisible)
        {
            ArrayList list = new ArrayList();
            if (controlName != null)
            {
                for (int i = 0; i < this.Controls[controlName].PropartyCount; i++)
                {
                    if (enCategory != CategoryKind.None)
                    {
                        if (this.Controls[controlName][i].Category == null)
                        {
                            continue;
                        }
                        bool flag = this.Controls[controlName][i].Category == Resources.ResourceManager.GetString("MSG0103");
                        if (((enCategory == CategoryKind.Basic) && flag) || ((enCategory == CategoryKind.Expansion) && !flag))
                        {
                            continue;
                        }
                    }
                    if ((enVisible == BooleanKind.None) || (((enVisible != BooleanKind.False) || !this.Controls[controlName][i].Visible) && ((enVisible != BooleanKind.True) || this.Controls[controlName][i].Visible)))
                    {
                        list.Add(this.Controls[controlName][i].Name);
                    }
                }
            }
            return list;
        }

        public bool isVisible(string controlName, string propartyName)
        {
            if (controlName != null)
            {
                for (int i = 0; i < this.Controls[controlName].PropartyCount; i++)
                {
                    if ((propartyName == null) || ((this.Controls[controlName][i].Name != null) && (this.Controls[controlName][i].Name == propartyName)))
                    {
                        return this.Controls[controlName][i].Visible;
                    }
                }
            }
            return false;
        }

        protected void LoadProperties(string source)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(source);
            XmlNodeList elementsByTagName = document.GetElementsByTagName("Property");
            this.mPropertyDoc = (XmlDocument) document.Clone();
            XmlElement element = this.mPropertyDoc.CreateElement("properties");
            string str = "";
            XmlNode newChild = null;
            foreach (XmlNode node2 in elementsByTagName)
            {
                XmlAttribute namedItem = (XmlAttribute) node2.Attributes.GetNamedItem("control");
                if (str != namedItem.Value)
                {
                    newChild = element.OwnerDocument.CreateElement(namedItem.Value);
                    element.AppendChild(newChild);
                    str = namedItem.Value;
                }
                XmlElement element2 = newChild.OwnerDocument.CreateElement("Property");
                for (int i = 0; i < node2.Attributes.Count; i++)
                {
                    element2.SetAttribute(node2.Attributes[i].Name, node2.Attributes[i].Value);
                }
                newChild.AppendChild(element2);
            }
            this.Controls = new ControlCollection(element.ChildNodes);
        }

        public class AttributeCollection
        {
            private readonly XmlNode mNode;

            internal AttributeCollection(XmlNode node)
            {
                this.mNode = node;
            }

            public string Category
            {
                get
                {
                    if (this.mNode == null)
                    {
                        return null;
                    }
                    XmlAttribute namedItem = (XmlAttribute) this.mNode.Attributes.GetNamedItem("category");
                    return namedItem.Value;
                }
            }

            public string Control
            {
                get
                {
                    if (this.mNode == null)
                    {
                        return null;
                    }
                    XmlAttribute namedItem = (XmlAttribute) this.mNode.Attributes.GetNamedItem("control");
                    return namedItem.Value;
                }
            }

            public bool Localizable
            {
                get
                {
                    if (this.mNode == null)
                    {
                        return false;
                    }
                    XmlAttribute namedItem = (XmlAttribute) this.mNode.Attributes.GetNamedItem("localizable");
                    if (namedItem.Value == null)
                    {
                        return false;
                    }
                    return (namedItem.Value == "True");
                }
            }

            public string Name
            {
                get
                {
                    if (this.mNode == null)
                    {
                        return null;
                    }
                    XmlAttribute namedItem = (XmlAttribute) this.mNode.Attributes.GetNamedItem("name");
                    return namedItem.Value;
                }
            }

            public bool Readonly
            {
                get
                {
                    if (this.mNode == null)
                    {
                        return false;
                    }
                    XmlAttribute namedItem = (XmlAttribute) this.mNode.Attributes.GetNamedItem("readonly");
                    if (namedItem.Value == null)
                    {
                        return false;
                    }
                    return (namedItem.Value == "True");
                }
            }

            public bool Visible
            {
                get
                {
                    if (this.mNode == null)
                    {
                        return false;
                    }
                    XmlAttribute namedItem = (XmlAttribute) this.mNode.Attributes.GetNamedItem("visible");
                    if (namedItem.Value == null)
                    {
                        return false;
                    }
                    return (namedItem.Value == "True");
                }
            }
        }

        public enum BooleanKind
        {
            None,
            True,
            False
        }

        public enum CategoryKind
        {
            None,
            Basic,
            Expansion
        }

        public class ControlCollection
        {
            private readonly XmlNodeList mControlList;

            internal ControlCollection(XmlNodeList list)
            {
                this.mControlList = list;
            }

            public int ControlCount
            {
                get
                {
                    if (this.mControlList == null)
                    {
                        return 0;
                    }
                    return this.mControlList.Count;
                }
            }

            public PropertiesCollection.PropertyCollection this[string controlName]
            {
                get
                {
                    PropertiesCollection.PropertyCollection propertys = null;
                    foreach (XmlElement element in this.mControlList)
                    {
                        if (element.Name == controlName)
                        {
                            propertys = new PropertiesCollection.PropertyCollection(element.GetElementsByTagName("Property"));
                            break;
                        }
                    }
                    if (propertys == null)
                    {
                        propertys = new PropertiesCollection.PropertyCollection(null);
                    }
                    return propertys;
                }
            }
        }

        public class PropertyCollection
        {
            private readonly XmlNodeList mPropertyList;

            internal PropertyCollection(XmlNodeList list)
            {
                this.mPropertyList = list;
            }

            public PropertiesCollection.AttributeCollection this[int index]
            {
                get
                {
                    return new PropertiesCollection.AttributeCollection(this.mPropertyList.Item(index));
                }
            }

            public PropertiesCollection.AttributeCollection this[string name]
            {
                get
                {
                    PropertiesCollection.AttributeCollection attributes = null;
                    for (int i = 0; i < this.PropartyCount; i++)
                    {
                        XmlAttribute namedItem = (XmlAttribute) this.mPropertyList.Item(i).Attributes.GetNamedItem("name");
                        if ((namedItem != null) && (namedItem.Value == name))
                        {
                            attributes = new PropertiesCollection.AttributeCollection(this.mPropertyList.Item(i));
                            break;
                        }
                    }
                    if (attributes == null)
                    {
                        attributes = new PropertiesCollection.AttributeCollection(null);
                    }
                    return attributes;
                }
            }

            public int PropartyCount
            {
                get
                {
                    if (this.mPropertyList == null)
                    {
                        return 0;
                    }
                    return this.mPropertyList.Count;
                }
            }
        }
    }
}

