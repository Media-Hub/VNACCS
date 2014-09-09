namespace Naccs.Core.Settings
{
    using System;
    using System.Runtime.InteropServices;
    using System.Xml.Serialization;

    [StructLayout(LayoutKind.Sequential)]
    public struct PathItem
    {
        [XmlAttribute("type")]
        public Naccs.Core.Settings.PathType PathType;
        [XmlText]
        public string Value;
        public PathItem(Naccs.Core.Settings.PathType type, string value)
        {
            this.PathType = type;
            this.Value = value;
        }
    }
}

