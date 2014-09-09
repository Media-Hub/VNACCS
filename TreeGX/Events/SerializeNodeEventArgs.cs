using System;

namespace DevComponents.Tree
{
	/// <summary>
	/// Represents arguments for SerializeNode event which allows you to add custom serialization data to definitions saved by control.
	/// </summary>
	public class SerializeNodeEventArgs : EventArgs
	{
		/// <summary>
		/// Gets reference to the node being serialized or de-serialized.
		/// </summary>
		public Node Node = null;

		/// <summary>
		/// Gets reference to instance of XmlElement that item is serialized to or is being de-serialized from. You should not change any data directly on this element.
		/// </summary>
		public System.Xml.XmlElement ItemXmlElement = null;

		/// <summary>
		/// Gets the reference to XmlElement that you can serialize to or de-serialize any custom data from. You can add child elements or set the attributes on
		/// this XmlElement when handling SerializeItem event. When handling DeserializeItem event you can load your data from this element.
		/// </summary>
		public System.Xml.XmlElement CustomXmlElement = null;

		public SerializeNodeEventArgs(Node node, System.Xml.XmlElement itemXmlElement, System.Xml.XmlElement customXmlElement)
		{
			this.Node = node;
			this.ItemXmlElement = itemXmlElement;
			this.CustomXmlElement = customXmlElement;
		}
	}
	
	/// <summary>
	/// Defines delegate for SerializeItem event.
	/// </summary>
	public delegate void SerializeNodeEventHandler(object sender, SerializeNodeEventArgs e);
}
