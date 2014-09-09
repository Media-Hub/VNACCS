using System;
using System.Drawing;
using System.Xml;

//#if AdvTree
//using DevComponents.Tree;
//namespace DevComponents.Tree.TextMarkup
////#elif DOTNETBAR
////namespace DevComponents.DotNetBar.TextMarkup
//#elif SUPERGRID
using DevComponents.DotNetBar.SuperGrid;
namespace DevComponents.SuperGrid.TextMarkup
//#endif
{
    internal class FontElement : FontChangeElement
    {
        #region Private Variables

        private Color _MForeColor = Color.Empty;
        private Color _MOldForeColor = Color.Empty;
        private int _MSize;
        private bool _MRelativeSize;
        private string _MFace = "";
        private string _MSystemColorName = "";

        #endregion

        #region Internal Implementation
        protected override void SetFont(MarkupDrawContext d)
        {
            Font font = d.CurrentFont;
            try
            {
                if (_MFace != "" || _MSize != 0 && _MRelativeSize || _MSize>4 && !_MRelativeSize)
                {
                    if (_MFace != "")
                        d.CurrentFont = new Font(_MFace, ((_MRelativeSize || _MSize == 0)?font.SizeInPoints + _MSize:_MSize), font.Style);
                    else
                        d.CurrentFont = new Font(font.FontFamily, ((_MRelativeSize || _MSize == 0)? font.SizeInPoints + _MSize : _MSize), font.Style);
                }
                else
                    font = null;
            }
            catch
            {
                font = null;
            }

            if (font != null)
                m_OldFont = font;

            if (!d.IgnoreFormattingColors)
            {
                if (!_MForeColor.IsEmpty)
                {
                    _MOldForeColor = d.CurrentForeColor;
                    d.CurrentForeColor = _MForeColor;
                }
                else if (_MSystemColorName != "")
                {
//#if DOTNETBAR
//                    if (Rendering.GlobalManager.Renderer is Rendering.Office2007Renderer)
//                    {
//                        m_OldForeColor = d.CurrentForeColor;
//                        d.CurrentForeColor = ((Rendering.Office2007Renderer)Rendering.GlobalManager.Renderer).ColorTable.Form.Active.CaptionTextExtra;
//                    }
//#endif
                }
            }
        }

        public override void RenderEnd(MarkupDrawContext d)
        {
            RestoreForeColor(d);

            base.RenderEnd(d);
        }

        public override void MeasureEnd(Size availableSize, MarkupDrawContext d)
        {
            RestoreForeColor(d);
            base.MeasureEnd(availableSize, d);
        }

        protected virtual void RestoreForeColor(MarkupDrawContext d)
        {
            if (!_MOldForeColor.IsEmpty)
                d.CurrentForeColor = _MOldForeColor;
            _MOldForeColor = Color.Empty;
        }

        public Color ForeColor
        {
            get { return _MForeColor; }
            set { _MForeColor = value; }
        }

        public int Size
        {
            get { return _MSize; }
            set { _MSize = value; }
        }

        public string Face
        {
            get { return _MFace; }
            set { _MFace = value; }
        }

        private Color GetColorFromName(string name)
        {
            string s = name.ToLower();
            _MSystemColorName = "";
            if (s == "syscaptiontextextra")
            {
                _MSystemColorName = s;
                return Color.Empty;
            }

            return Color.FromName(name);
        }

        public override void ReadAttributes(XmlTextReader reader)
        {
            _MRelativeSize = false;
            for (int i = 0; i < reader.AttributeCount; i++)
            {
                reader.MoveToAttribute(i);
                if (reader.Name.ToLower() == "color")
                {
                    try
                    {
                        string s = reader.Value;
                        if (s.StartsWith("#"))
                        {
                            if (s.Length == 7)
                                _MForeColor = ColorHelpers.GetColor(s.Substring(1));
                        }
                        else
                        {
                            _MForeColor = GetColorFromName(s);
                        }
                    }
                    catch
                    {
                        _MForeColor = Color.Empty;
                    }
                }
                else if (reader.Name.ToLower() == "size")
                {
                    string s = reader.Value;
                    if (s.StartsWith("+"))
                    {
                        try
                        {
                            _MSize = Int32.Parse(s.Substring(1));
                            _MRelativeSize = true;
                        }
                        catch
                        {
                            _MSize = 0;
                        }
                    }
                    else
                    {
                        if (s.StartsWith("-"))
                            _MRelativeSize = true;
                        try
                        {
                            _MSize = Int32.Parse(s);
                        }
                        catch
                        {
                            _MSize = 0;
                        }
                    }
                }
                else if (reader.Name.ToLower() == "face")
                {
                    _MFace = reader.Value;
                }
            }
        }
        #endregion
    }
}
