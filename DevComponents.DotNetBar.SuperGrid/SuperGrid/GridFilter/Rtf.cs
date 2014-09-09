using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DevComponents.DotNetBar.SuperGrid.SuperGrid
{
    ///<summary>
    /// Constructor
    ///</summary>
    public class Rtf
    {
        #region Private data

        private StringBuilder _SbRtf;   // Running Rtf text buffer
        private StringBuilder _SbText;  // Running plain text buffer

        private int _TextLength;        // Running text length
        private int _ProLength;         // Prolog length (used for Clear())

        private Group _Group;           // RTF Group
        private Stack<Group> _GStack;   // Group Stack

        private bool _Finalized;        // RTF has been finalized
        private bool _KeepText;         // Keep plain text

        #endregion

        #region Class definitions

        private class Group : ICloneable
        {
            public int Font;            // Current font index
            public int FontSize;        // Current font size
            public int ForeColor;       // Current ForeColor index
            public int BackColor;       // Current BackColor index

            public int LeftMargin;      // Left margin
            public int FirstIndent;     // First line indent
            public int LeftIndent;      // Left indent

            public bool Bold;           // Bold
            public bool Italicize;      // Italicize
            public bool SmallCaps;      // Small Caps
            public bool Strikeout;      // Strikeout
            public bool SuperScript;    // Superscript
            public bool Underline;      // Underline

            public bool Paragraph;      // Paragraph

            #region ICloneable Members

            // Routine to support the IClonable Interface

            public object Clone()
            {
                Group gp = new Group();

                gp.Font = Font;
                gp.FontSize = FontSize;
                gp.ForeColor = ForeColor;
                gp.BackColor = BackColor;

                gp.LeftMargin = LeftMargin;
                gp.FirstIndent = FirstIndent;
                gp.LeftIndent = LeftIndent;

                gp.Bold = Bold;
                gp.Italicize = Italicize;
                gp.SmallCaps = SmallCaps;
                gp.Strikeout = Strikeout;
                gp.SuperScript = SuperScript;
                gp.Underline = Underline;

                gp.Paragraph = Paragraph;

                return (gp);
            }

            #endregion
        }

        #endregion

        /// <summary>
        /// This constructor accepts the Rtf font and
        /// color arrays, and initializes the Rtf buffer
        /// </summary>
        /// <param name="sFonts"></param>
        /// <param name="cColors"></param>
        public Rtf(string[] sFonts, Color[] cColors)
        {
            _Group = new Group();
            _GStack = new Stack<Group>();
            _SbRtf = new StringBuilder();

            InitRtf(sFonts, cColors);
        }

        #region InitRtf

        /// <summary>
        /// Initializes our Rtf engine
        /// </summary>
        /// <param name="sFonts"></param>
        /// <param name="cColors"></param>
        private void InitRtf(string[] sFonts, Color[] cColors)
        {
            // Add the RTF prolog, font table, and
            // color table

            _SbRtf.Append("{\\rtf1\\ansi\\deff0{\\fonttbl");

            for (int i = 0; i < sFonts.Length; i++)
                _SbRtf.AppendFormat("{0}\\f{1} {2};{3}", '{', i, sFonts[i], '}');

            _SbRtf.Append("}{\\colortbl;");

            for (int i = 0; i < cColors.Length; i++)
            {
                _SbRtf.AppendFormat("\\red{0}\\green{1}\\blue{2};",
                                    cColors[i].R, cColors[i].G, cColors[i].B);
            }

            _SbRtf.Append("}");

            _ProLength = _SbRtf.Length;
        }

        #endregion

        #region Close

        /// <summary>
        // Closes and finalizes the Rtf document
        /// </summary>
        public void Close()
        {
            if (_Finalized == false)
            {
                // Everything should be popped off the
                // stack by now

                if (_GStack.Count > 0)
                    throw new Exception();

                _SbRtf.Append("}");

                _Finalized = true;
            }
        }

        #endregion

        #region Group support code

        #region BeginGroup

        /// <summary>
        /// This routine begins a new RTF group
        /// </summary>
        /// <param name="fParagraph"></param>
        /// <returns></returns>
        public int BeginGroup(bool fParagraph)
        {
            PushGroup();

            // The paragraph flag is used when we close
            // the group, so save if for later inspection

            _Group.Paragraph = fParagraph;

            _SbRtf.Append("{");

            return (_GStack.Count);
        }

        #endregion

        #region EndGroup

        /// <summary>
        /// This routine ends the current open group
        /// </summary>
        /// <returns></returns>
        public int EndGroup()
        {
            if (_Group.Paragraph == true)
                _SbRtf.Append("\\par");

            _SbRtf.Append("}");

            PopGroup();

            return (_GStack.Count);
        }

        #endregion

        #region PushGroup

        /// <summary>
        // This routine saves all the current group settings
        /// </summary>
        private void PushGroup()
        {
            Group gp = (Group)_Group.Clone();

            _GStack.Push(gp);
        }

        #endregion

        #region PopGroup

        /// <summary>
        /// This routine restores all the current
        /// group settings
        /// </summary>
        private void PopGroup()
        {
            if (_GStack.Count <= 0)
                throw new Exception();

            _Group = _GStack.Pop();
        }

        #endregion

        #region GroupCount

        /// <summary>
        /// Stack group count
        /// </summary>
        public int GroupCount
        {
            get { return (_GStack.Count); }
        }

        #endregion

        #endregion

        #region Font support

        #region FontSize

        /// <summary>
        /// This routine gets or sets the current
        /// group level font size
        /// </summary>
        public int FontSize
        {
            get { return (_Group.FontSize); }

            set
            {
                if (_Group.FontSize != value)
                {
                    // The fontsize setting for an RTF document
                    // is doubled (eg. if you want a 12 point font, then
                    // you must specify it as size 24)

                    _SbRtf.AppendFormat("\\fs{0} ", value * 2);

                    _Group.FontSize = value;
                }
            }
        }

        #endregion

        #region Font

        /// <summary>
        /// This routine gets or sets the current
        /// group level font setting
        /// </summary>
        public int Font
        {
            get { return (_Group.Font); }

            set
            {
                if (_Group.Font != value)
                {
                    _SbRtf.AppendFormat("\\f{0} ", value);

                    _Group.Font = value;
                }
            }
        }

        #endregion

        #region SuperScript

        /// <summary>
        /// This routine gets or sets the current
        /// group level superscript setting
        /// </summary>
        public bool SuperScript
        {
            get { return (_Group.SuperScript); }

            set
            {
                if (_Group.SuperScript != value)
                {
                    _SbRtf.Append(value == true ? "\\super " : "\\super0 ");
                    _Group.SuperScript = value;
                }
            }
        }

        #endregion

        #region SmallCaps

        private int _ScapsFontSize;

        /// <summary>
        /// This routine gets or sets the current
        /// group level smallCaps setting
        /// </summary>
        public bool SmallCaps
        {
            get { return (_Group.SmallCaps); }

            set
            {
                if (_Group.SmallCaps != value)
                {
                    // 'scaps' does not appear to work in the system RichTextBox
                    // so we must simulate it via altering the fontsize a percentage
                    // of the current size

                    if (value == true)
                    {
                        _ScapsFontSize = FontSize;

                        FontSize = (FontSize * 7) / 8;
                    }
                    else
                    {
                        FontSize = _ScapsFontSize;
                    }

                    _Group.SmallCaps = value;
                }
            }
        }

        #endregion

        #region Bold

        /// <summary>
        /// This routine gets or sets the current
        /// group level font Bold setting
        /// </summary>
        public bool Bold
        {
            get { return (_Group.Bold); }

            set
            {
                if (_Group.Bold != value)
                {
                    _SbRtf.Append(value == true ? "\\b " : "\\b0 ");
                    _Group.Bold = value;
                }
            }
        }

        #endregion

        #region Italicize

        /// <summary>
        /// This routine gets or sets the current
        /// group level font Italics setting
        /// </summary>
        public bool Italicize
        {
            get { return (_Group.Italicize); }

            set
            {
                if (_Group.Italicize != value)
                {
                    _SbRtf.Append(value == true ? "\\i " : "\\i0 ");
                    _Group.Italicize = value;
                }
            }
        }

        #endregion

        #region Strikeout

        /// <summary>
        // This routine gets or sets the current
        // group level font Strikeout setting
        /// </summary>
        public bool Strikeout
        {
            get { return (_Group.Strikeout); }

            set
            {
                if (_Group.Strikeout != value)
                {
                    _SbRtf.Append(value == true ? "\\strike " : "\\strike0 ");
                    _Group.Strikeout = value;
                }
            }
        }

        #endregion

        #region Underline

        /// <summary>
        /// This routine gets or sets the current
        /// group level font Underline setting
        /// </summary>
        public bool Underline
        {
            get { return (_Group.Underline); }

            set
            {
                if (_Group.Underline != value)
                {
                    _SbRtf.Append(value == true ? "\\ul " : "\\ul0 ");

                    _Group.Underline = value;
                }
            }
        }

        #endregion

        #endregion

        #region Color support

        #region ForeColor

        /// <summary>
        /// This routine gets or sets the current
        /// group level foreColor value
        /// </summary>
        public int ForeColor
        {
            get { return (_Group.ForeColor); }

            set
            {
                if (_Group.ForeColor != value)
                {
                    _SbRtf.AppendFormat("\\cf{0} ", value + 1);

                    _Group.ForeColor = value;
                }
            }
        }

        #endregion

        #region BackColor

        /// <summary>
        /// This routine gets or sets the current
        /// group level backColor setting
        /// </summary>
        public int BackColor
        {
            get { return (_Group.BackColor); }

            set
            {
                if (_Group.BackColor != value)
                {
                    // "\\cb " does not appear to work, so we work
                    // around this via using the highlight tag

                    _SbRtf.AppendFormat("\\highlight{0} ", value + 1);

                    _Group.BackColor = value;
                }
            }
        }

        #endregion

        #endregion

        #region Margin / indent support

        #region LeftMargin

        /// <summary>
        /// This routine gets or sets the current
        /// group level left margin setting
        /// </summary>
        public int LeftMargin
        {
            get { return (_Group.LeftMargin); }
            set { _Group.LeftMargin = value; }
        }

        #endregion

        #region FirstIndent

        /// <summary>
        /// This routine gets or sets the current
        /// group level first indent setting
        /// </summary>
        public int FirstIndent
        {
            get { return (_Group.FirstIndent); }

            set
            {
                if (_Group.FirstIndent != value)
                {
                    // Set the indent value, taking the current
                    // left margin into account

                    _SbRtf.AppendFormat("\\fi{0} ", _Group.LeftMargin + value);

                    _Group.FirstIndent = value;
                }
            }
        }

        #endregion

        #region LeftIndent

        /// <summary>
        /// This routine gets or sets the current
        /// group level left indent setting
        /// </summary>
        public int LeftIndent
        {
            get { return (_Group.LeftIndent); }

            set
            {
                if (_Group.LeftIndent != value)
                {
                    // Set the indent value, taking the current
                    // left margin into account

                    _SbRtf.AppendFormat("\\li{0} ", _Group.LeftMargin + value);

                    _Group.LeftIndent = value;
                }
            }
        }

        #endregion

        #endregion

        #region Text output support

        #region KeepText

        /// <summary>
        /// This property tells the rtf code whether to
        /// keep a running accumulation of plain text
        /// </summary>
        public bool KeepText
        {
            get { return (_KeepText); }

            set
            {
                if (_KeepText != value)
                {
                    _SbText = (value == true) ?
                        new StringBuilder() : null;

                    _KeepText = value;
                }
            }
        }

        #endregion

        #region Text

        /// <summary>
        /// This read-only property returns the current plain
        /// text - if the 'KeepText' property was set to true
        /// </summary>
        public string Text
        {
            get
            {
                //if (_GStack.Count != 0 || fFinalized == false)
                //    throw new Exception();

                //if (sbText == null)
                //    throw new Exception();

                return (_SbText.ToString());
            }
        }

        #endregion

        #region RtfText

        /// <summary>
        /// This read-only property returns the current
        /// rtf accumulated control text
        /// </summary>
        public string RtfText
        {
            get
            {
                if (_GStack.Count != 0 || _Finalized == false)
                    throw new Exception();

                return (_SbRtf.ToString());
            }
        }

        #endregion

        #region TextLength

        /// <summary>
        /// This property get/sets the current
        /// rtf plain text length
        /// </summary>
        public int TextLength
        {
            get { return (_TextLength); }
            set { _TextLength = value; }
        }

        #endregion

        #region Clear

        /// <summary>
        /// This routine clears the accumulated text
        /// </summary>
        public void Clear()
        {
            _SbRtf.Length = _ProLength;

            if (_SbText != null)
                _SbText.Length = 0;

            _TextLength = 0;
        }

        #endregion

        #region WriteText (char)

        /// <summary>
        /// This routine writes a given char to the rtf buffer
        /// </summary>
        /// <param name="c"></param>
        public void WriteText(char c)
        {
            _SbRtf.Append(c);

            if (_SbText != null)
                _SbText.Append(c);

            _TextLength++;
        }

        #endregion

        #region WriteText (string)

        /// <summary>
        /// This routine writes a given string to the rtf buffer
        /// </summary>
        /// <param name="s"></param>
        public void WriteText(string s)
        {
            if (s.Length > 0)
            {
                _SbRtf.Append(s);

                if (_SbText != null)
                    _SbText.Append(s);

                _TextLength += s.Length;
            }
        }

        #endregion

        #region WriteLine

        /// <summary>
        /// This routine writes a new line to the rtf buffer
        /// </summary>
        public void WriteLine()
        {
            _SbRtf.Append("\\line ");

            if (_SbText != null)
                _SbText.Append('\n');

            _TextLength++;
        }

        #endregion

        #region WriteLine (string)

        /// <summary>
        /// This routine writes a given string followed
        /// by a new line to the rtf buffer
        /// </summary>
        /// <param name="s"></param>
        public void WriteLine(string s)
        {
            WriteText(s);
            WriteLine();
        }

        #endregion

        #region WriteTab

        /// <summary>
        /// This routine writes a tab to the rtf buffer
        /// </summary>
        public void WriteTab()
        {
            _SbRtf.Append("\\tab ");

            if (_SbText != null)
                _SbText.Append('\t');

            _TextLength++;
        }

        #endregion

        #region WriteHex

        /// <summary>
        /// This routine writes hex chars to the rtf buffer
        /// </summary>
        /// <param name="x"></param>
        public void WriteHex(uint x)
        {
            string hex =
                String.Format("{0:x2}", x);

            _SbRtf.AppendFormat("\\'{0}", hex);

            _TextLength++;
        }

        #endregion

        #region CenterAlignText

        /// <summary>
        /// This routine causes the test to be center aligned
        /// </summary>
        public void CenterAlignText()
        {
            _SbRtf.Append("\\qc ");
        }

        #endregion

        #region LeftAlignText

        /// <summary>
        /// This routine causes the test to be left aligned
        /// </summary>
        public void LeftAlignText()
        {
            _SbRtf.Append("\\ql ");
        }

        #endregion

        #endregion
    }
}