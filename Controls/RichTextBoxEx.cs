using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace DevComponents.DotNetBar.Controls
{
    [ToolboxItem(true), Docking(DockingBehavior.Ask)]
    [Description("Provides rich-text, RTF text editing."), ToolboxBitmap(typeof(RichTextBoxEx), "Controls.RichTextBoxEx.ico"), DefaultEvent("TextChanged"), DefaultProperty("Text")]
    [DefaultBindingProperty("Text")]
    [Designer(typeof(DevComponents.DotNetBar.Design.RichTextBoxExDesigner))]
    public class RichTextBoxEx : ScrollbarControl
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the RichTextBoxEx class.
        /// </summary>
        public RichTextBoxEx()
        {
            RichTextBoxScrollEx rtb = new RichTextBoxScrollEx();
            this.SetStyle(ControlStyles.Selectable, false);
            rtb.AcceptsTabChanged += OnRichTextBoxAcceptsTabChanged;
            rtb.TextChanged += OnRichTextBoxTextChanged;
            rtb.HideSelectionChanged += OnRichTextBoxHideSelectionChanged;
            rtb.ModifiedChanged += OnRichTextBoxModifiedChanged;
            rtb.MultilineChanged += OnRichTextBoxMultilineChanged;
            rtb.ReadOnlyChanged += OnRichTextBoxReadOnlyChanged;
            rtb.GotFocus += OnRichTextBoxGotFocus;
            rtb.LostFocus += OnRichTextBoxLostFocus;
            rtb.KeyDown += OnRichTextBoxKeyDown;
            rtb.KeyUp += OnRichTextBoxKeyUp;
            rtb.KeyPress += OnRichTextBoxKeyPress;
            rtb.PreviewKeyDown += OnRichTextBoxPreviewKeyDown;
            rtb.LinkClicked += OnRichTextBoxLinkClicked;
            rtb.Protected += OnRichTextBoxProtected;
            rtb.SelectionChanged += OnRichTextBoxSelectionChanged;
            rtb.HScroll += OnRichTextBoxHScroll;
            rtb.VScroll += OnRichTextBoxVScroll;
            rtb.Validating += OnRichTextBoxValidating;
            rtb.Validated += OnRichTextBoxValidated;
            RichTextBox = rtb;
            rtb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ScrollOverrideControl = rtb;
        }
        #endregion

        #region Internals

        private RichTextBox _RichTextBox;
        /// <summary>
        /// Gets the reference to internal RichTextBox control.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public RichTextBox RichTextBox
        {
            get { return _RichTextBox; }
            internal set { _RichTextBox = value; }
        }
        private void OnRichTextBoxAcceptsTabChanged(object sender, EventArgs e)
        {
            this.OnAcceptsTabChanged(e);
        }
        /// <summary>
        /// Occurs when the value of the AcceptsTab property changes
        /// </summary>
        [Description("Occurs when the value of the AcceptsTab property changes.")]
        public event EventHandler AcceptsTabChanged;
        /// <summary>
        /// Raises AcceptsTabChanged event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnAcceptsTabChanged(EventArgs e)
        {
            EventHandler handler = AcceptsTabChanged;
            if (handler != null)
                handler(this, e);
        }
        private void OnRichTextBoxTextChanged(object sender, EventArgs e)
        {
            this.OnTextChanged(e);
        }
        private void OnRichTextBoxHideSelectionChanged(object sender, EventArgs e)
        {
            this.OnHideSelectionChanged(e);
        }
        /// <summary>
        /// Occurs when the value of the HideSelection property changes.
        /// </summary>
        [Description("Occurs when the value of the HideSelection property changes.")]
        public event EventHandler HideSelectionChanged;
        /// <summary>
        /// Raises HideSelectionChanged event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnHideSelectionChanged(EventArgs e)
        {
            EventHandler handler = HideSelectionChanged;
            if (handler != null)
                handler(this, e);
        }
        private void OnRichTextBoxModifiedChanged(object sender, EventArgs e)
        {
            this.OnModifiedChanged(e);
        }
        /// <summary>
        /// Occurs when the value of the Modified property changes.
        /// </summary>
        [Description("Occurs when the value of the Modified property changes.")]
        public event EventHandler ModifiedChanged;
        /// <summary>
        /// Raises ModifiedChanged event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnModifiedChanged(EventArgs e)
        {
            EventHandler handler = ModifiedChanged;
            if (handler != null)
                handler(this, e);
        }
        private void OnRichTextBoxMultilineChanged(object sender, EventArgs e)
        {
            this.OnMultilineChanged(e);
        }
        /// <summary>
        /// Occurs when the value of the Multiline property changes.
        /// </summary>
        [Description("Occurs when the value of the Multiline property changes.")]
        public event EventHandler MultilineChanged;
        /// <summary>
        /// Raises MultilineChanged event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnMultilineChanged(EventArgs e)
        {
            EventHandler handler = MultilineChanged;
            if (handler != null)
                handler(this, e);
        }
        private void OnRichTextBoxReadOnlyChanged(object sender, EventArgs e)
        {
            this.OnReadOnlyChanged(e);
        }
        /// <summary>
        /// Occurs when the value of the ReadOnly property changes.
        /// </summary>
        [Description("Occurs when the value of the ReadOnly property changes.")]
        public event EventHandler ReadOnlyChanged;
        /// <summary>
        /// Raises ReadOnlyChanged event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnReadOnlyChanged(EventArgs e)
        {
            EventHandler handler = ReadOnlyChanged;
            if (handler != null)
                handler(this, e);
        }
        private void OnRichTextBoxGotFocus(object sender, EventArgs e)
        {
            this.OnGotFocus(e);
        }
        private void OnRichTextBoxLostFocus(object sender, EventArgs e)
        {
            this.OnLostFocus(e);
        }
        private void OnRichTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            this.OnKeyDown(e);
        }
        private void OnRichTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            this.OnKeyUp(e);
        }
        private void OnRichTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnKeyPress(e);
        }
        private void OnRichTextBoxPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            this.OnPreviewKeyDown(e);
        }
        private void OnRichTextBoxLinkClicked(object sender, LinkClickedEventArgs e)
        {
            this.OnLinkClicked(e);
        }
        /// <summary>
        /// Occurs when a hyperlink in the text is clicked.
        /// </summary>
        [Description("Occurs when a hyperlink in the text is clicked."), Category("Behavior")]
        public event LinkClickedEventHandler LinkClicked;
        /// <summary>
        /// Raises LinkClicked event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnLinkClicked(LinkClickedEventArgs e)
        {
            LinkClickedEventHandler handler = LinkClicked;
            if (handler != null)
                handler(this, e);
        }
        private void OnRichTextBoxProtected(object sender, EventArgs e)
        {
            this.OnProtected(e);
        }
        /// <summary>
        /// Occurs when the user takes an action that would change a protected range of text.
        /// </summary>
        [Category("Behavior"), Description("Occurs when the user takes an action that would change a protected range of text.")]
        public event EventHandler Protected;
        /// <summary>
        /// Raises Protected event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnProtected(EventArgs e)
        {
            EventHandler handler = Protected;
            if (handler != null)
                handler(this, e);
        }
        private void OnRichTextBoxSelectionChanged(object sender, EventArgs e)
        {
            this.OnSelectionChanged(e);
        }
        /// <summary>
        /// Occurs when the current selection has changed.
        /// </summary>
        [Category("Behavior"), Description("Occurs when the current selection has changed.")]
        public event EventHandler SelectionChanged;
        /// <summary>
        /// Raises SelectionChanged event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnSelectionChanged(EventArgs e)
        {
            EventHandler handler = SelectionChanged;
            if (handler != null)
                handler(this, e);
        }
        private void OnRichTextBoxHScroll(object sender, EventArgs e)
        {
            this.OnHScroll(e);
        }
        /// <summary>
        /// Occurs when the horizontal scroll bar is clicked.
        /// </summary>
        [Description("Occurs when the horizontal scroll bar is clicked."), Category("Behavior")]
        public event EventHandler HScroll;
        /// <summary>
        /// Raises HScroll event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnHScroll(EventArgs e)
        {
            EventHandler handler = HScroll;
            if (handler != null)
                handler(this, e);
        }
        private void OnRichTextBoxVScroll(object sender, EventArgs e)
        {
            this.OnVScroll(e);
        }
        /// <summary>
        /// Occurs when the vertical scroll bar is clicked.
        /// </summary>
        [Description("Occurs when the vertical scroll bar is clicked."), Category("Behavior")]
        public event EventHandler VScroll;
        /// <summary>
        /// Raises HScroll event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnVScroll(EventArgs e)
        {
            EventHandler handler = VScroll;
            if (handler != null)
                handler(this, e);
        }
        private void OnRichTextBoxValidating(object sender, CancelEventArgs e)
        {
            this.OnValidating(e);
        }
        private void OnRichTextBoxValidated(object sender, EventArgs e)
        {
            this.OnValidated(e);
        }

        /// <summary>
        /// Gets or sets whether anti-alias smoothing is used while painting. Default value is true.
        /// </summary>
        [DefaultValue(true), Category("Appearance"), Description("Gets or sets whether anti-aliasing is used while painting."), Browsable(false)]
        public override bool AntiAlias
        {
            get { return base.AntiAlias; }
            set { base.AntiAlias = value; }
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            _RichTextBox.ForeColor = this.ForeColor;
            base.OnForeColorChanged(e);
        }

        //protected override void OnBackColorChanged(EventArgs e)
        //{
        //    _RichTextBox.BackColor = this.BackColor;
        //    base.OnBackColorChanged(e);
        //}

        private Color _BackColorRichTextBox = SystemColors.Window;
        /// <summary>
        /// Gets or sets the back color of the RichTextBox.
        /// </summary>
        [Category("Columns"), Description("Indicates back color of RichTextBox.")]
        public Color BackColorRichTextBox
        {
            get { return _BackColorRichTextBox; }
            set { _BackColorRichTextBox = value; _RichTextBox.BackColor = value; }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeBackColorRichTextBox()
        {
            return !_BackColorRichTextBox.Equals(SystemColors.Window);
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetBackColorRichTextBox()
        {
            this.BackColorRichTextBox = SystemColors.Window;
        }
        #endregion

        #region RichTextBox Forwarding
        private BorderStyle _BorderStyle;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BorderStyle BorderStyle
        {
            get { return _BorderStyle; }
            set { _BorderStyle = value; }
        }
        /// <summary>
        /// Appends text to the current text of a text box
        /// </summary>
        public void AppendText(string text)
        {
            _RichTextBox.AppendText(text);
            UpdateScrollBarsDelayed();
        }
        /// <summary>
        /// Determines whether you can paste information from the Clipboard in the specified data format.
        /// </summary>
        /// <param name="clipFormat"></param>
        /// <returns></returns>
        public bool CanPaste(DataFormats.Format clipFormat)
        {
            return _RichTextBox.CanPaste(clipFormat);
        }
        /// <summary>
        /// Clears all text from the text box control.
        /// </summary>
        public void Clear()
        {
            _RichTextBox.Clear();
            UpdateScrollBarsDelayed();
        }
        /// <summary>
        /// Clears information about the most recent operation from the undo buffer of the text box.
        /// </summary>
        public void ClearUndo()
        {
            _RichTextBox.ClearUndo();
            UpdateScrollBarsDelayed();
        }
        /// <summary>
        /// Copies the current selection in the text box to the Clipboard.
        /// </summary>
        public void Copy()
        {
            _RichTextBox.Copy();
        }
        /// <summary>
        /// Moves the current selection in the text box to the Clipboard.
        /// </summary>
        public void Cut()
        {
            _RichTextBox.Cut();
            UpdateScrollBarsDelayed();
        }
        /// <summary>
        /// Specifies that the value of the SelectionLength property is zero so that no characters are selected in the control.
        /// </summary>
        public void DeselectAll()
        {
            _RichTextBox.DeselectAll();
        }
        /// <summary>
        /// Searches the text in a RichTextBox control for a string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int Find(string str)
        {
            return _RichTextBox.Find(str);
        }
        /// <summary>
        /// Searches the text of a RichTextBox control for the first instance of a character from a list of characters.
        /// </summary>
        /// <param name="characterSet"></param>
        /// <returns></returns>
        public int Find(char[] characterSet)
        {
            return _RichTextBox.Find(characterSet);
        }
        /// <summary>
        /// Searches the text in a RichTextBox control for a string with specific options applied to the search.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public int Find(string str, RichTextBoxFinds options)
        {
            return _RichTextBox.Find(str, options);
        }
        /// <summary>
        /// Searches the text of a RichTextBox control, at a specific starting point, for the first instance of a character from a list of characters.
        /// </summary>
        /// <param name="characterSet"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public int Find(char[] characterSet, int start)
        {
            return _RichTextBox.Find(characterSet, start);
        }
        /// <summary>
        /// Searches the text in a RichTextBox control for a string at a specific location within the control and with specific options applied to the search. 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="start"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public int Find(string str, int start, RichTextBoxFinds options)
        {
            return _RichTextBox.Find(str, start, options);
        }
        /// <summary>
        /// Searches a range of text in a RichTextBox control for the first instance of a character from a list of characters.
        /// </summary>
        /// <param name="characterSet"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public int Find(char[] characterSet, int start, int end)
        {
            return _RichTextBox.Find(characterSet, start, end);
        }
        /// <summary>
        /// Searches the text in a RichTextBox control for a string within a range of text within the control and with specific options applied to the search. 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public int Find(string str, int start, int end, RichTextBoxFinds options)
        {
            return _RichTextBox.Find(str, start, end, options);
        }
        public bool Focus()
        {
            return ((this.RichTextBox != null) && this.RichTextBox.Focus());
        }
        /// <summary>
        /// Retrieves the character that is closest to the specified location within the control.
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public int GetCharFromPosition(Point pt)
        {
            return _RichTextBox.GetCharFromPosition(pt);
        }
        /// <summary>
        /// Retrieves the index of the character nearest to the specified location
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public int GetCharIndexFromPosition(Point pt)
        {
            return _RichTextBox.GetCharIndexFromPosition(pt);
        }
        /// <summary>
        /// Retrieves the index of the first character of a given line. (Inherited from TextBoxBase.)
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public int GetFirstCharIndexFromLine(int lineNumber)
        {
            return _RichTextBox.GetFirstCharIndexFromLine(lineNumber);
        }
        /// <summary>
        /// Retrieves the index of the first character of the current line. (Inherited from TextBoxBase.)
        /// </summary>
        /// <returns></returns>
        public int GetFirstCharIndexOfCurrentLine()
        {
            return _RichTextBox.GetFirstCharIndexOfCurrentLine();
        }
        /// <summary>
        /// Retrieves the line number from the specified character position within the text of the RichTextBox control.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetLineFromCharIndex(int index)
        {
            return _RichTextBox.GetLineFromCharIndex(index);
        }
        /// <summary>
        /// Retrieves the location within the control at the specified character index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Point GetPositionFromCharIndex(int index)
        {
            return _RichTextBox.GetPositionFromCharIndex(index);
        }
        /// <summary>
        /// Loads a rich text format (RTF) or standard ASCII text file into the RichTextBox control. 
        /// </summary>
        /// <param name="path"></param>
        public void LoadFile(string path)
        {
            _RichTextBox.LoadFile(path);
            UpdateScrollBarsDelayed();
        }
        /// <summary>
        /// Loads the contents of an existing data stream into the RichTextBox control. 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fileType"></param>
        public void LoadFile(Stream data, RichTextBoxStreamType fileType)
        {
            _RichTextBox.LoadFile(data, fileType);
            UpdateScrollBarsDelayed();
        }
        /// <summary>
        /// Loads a specific type of file into the RichTextBox control. 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileType"></param>
        public void LoadFile(string path, RichTextBoxStreamType fileType)
        {
            _RichTextBox.LoadFile(path, fileType);
            UpdateScrollBarsDelayed();
        }
        /// <summary>
        /// Replaces the current selection in the text box with the contents of the Clipboard.
        /// </summary>
        public void Paste()
        {
            _RichTextBox.Paste();
            UpdateScrollBarsDelayed();
        }
        /// <summary>
        /// Pastes the contents of the Clipboard in the specified Clipboard format. 
        /// </summary>
        /// <param name="clipFormat"></param>
        public void Paste(DataFormats.Format clipFormat)
        {
            _RichTextBox.Paste(clipFormat);
            UpdateScrollBarsDelayed();
        }
        /// <summary>
        /// Reapplies the last operation that was undone in the control. 
        /// </summary>
        public void Redo()
        {
            _RichTextBox.Redo();
            UpdateScrollBarsDelayed();
        }
        /// <summary>
        /// Saves the contents of the RichTextBox to a rich text format (RTF) file. 
        /// </summary>
        /// <param name="path"></param>
        public void SaveFile(string path)
        {
            _RichTextBox.SaveFile(path);
        }
        /// <summary>
        /// Saves the contents of a RichTextBox control to an open data stream. 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fileType"></param>
        public void SaveFile(Stream data, RichTextBoxStreamType fileType)
        {
            _RichTextBox.SaveFile(data, fileType);
        }
        /// <summary>
        /// Saves the contents of the RichTextBox to a specific type of file. 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileType"></param>
        public void SaveFile(string path, RichTextBoxStreamType fileType)
        {
            _RichTextBox.SaveFile(path, fileType);
        }
        /// <summary>
        /// Scrolls the contents of the control to the current caret position.
        /// </summary>
        public void ScrollToCaret()
        {
            _RichTextBox.ScrollToCaret();
        }
        /// <summary>
        /// Activates the control. (Inherited from Control.)
        /// </summary>
        public void Select()
        {
           _RichTextBox.Select();
        }
        /// <summary>
        /// Selects a range of text in the text box.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        public void Select(int start, int length)
        {
            _RichTextBox.Select(start, length);
        }
        /// <summary>
        /// Selects all text in the text box.
        /// </summary>
        public void SelectAll()
        {
            _RichTextBox.SelectAll();
        }
        /// <summary>
        /// Undoes the last edit operation in the text box.
        /// </summary>
        public void Undo()
        {
            _RichTextBox.Undo();
            UpdateScrollBarsDelayed();
        }

        /// <summary>
        /// Gets or sets whether tab characters are accepted as input
        /// </summary>
        [DefaultValue(false), Description("Indicates whether tab characters are accepted as input."), Category("Behavior")]
        public bool AcceptsTab
        {
            get
            {
                return _RichTextBox.AcceptsTab;
            }
            set
            {
                _RichTextBox.AcceptsTab = value;
            }
        }
        [Browsable(false)]
        public override bool AllowDrop
        {
            get
            {
                return base.AllowDrop;
            }
            set
            {
                base.AllowDrop = value;
            }
        }
        /// <summary>
        /// Gets or sets whether automatic word selection is enabled.
        /// </summary>
        [Category("Behavior"), DefaultValue(false), Description("Indicates whether automatic word selection is enabled.")]
        public bool AutoWordSelection
        {
            get
            {
                return _RichTextBox.AutoWordSelection;
            }
            set
            {
                _RichTextBox.AutoWordSelection = value;
            }
        }
        [Bindable(false), Browsable(false)]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }
        /// <summary>
        /// Gets or sets indent for bullets in the control.
        /// </summary>
        [DefaultValue(0), Category("Behavior"), Description("Indicates the indent for bullets in the control."), Localizable(true)]
        public int BulletIndent
        {
            get
            {
                return _RichTextBox.BulletIndent;
            }
            set
            {
                _RichTextBox.BulletIndent = value;
            }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanRedo
        {
            get
            {
                return _RichTextBox.CanRedo;
            }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanUndo
        {
            get
            {
                return _RichTextBox.CanUndo;
            }
        }
        public override ContextMenuStrip ContextMenuStrip
        {
            get
            {
                return base.ContextMenuStrip;
            }
            set
            {
                base.ContextMenuStrip = value;
                _RichTextBox.ContextMenuStrip = value;
            }
        }
        protected override Size DefaultSize
        {
            get
            {
                return new Size(100, 100);
            }
        }
        /// <summary>
        /// Gets or sets whether URLs are automatically formatted as links.
        /// </summary>
        [Category("Behavior"), DefaultValue(true), Description("Indicates whether URLs are automatically formatted as links.")]
        public bool DetectUrls
        {
            get
            {
                return _RichTextBox.DetectUrls;
            }
            set
            {
                _RichTextBox.DetectUrls = value;
            }
        }
        /// <summary>
        /// Gets or sets whether drag/drop of text, pictures and other data is enabled.
        /// </summary>
        [DefaultValue(false), Category("Behavior"), Description("Indicates whether drag/drop of text, pictures and other data is enabled.")]
        public bool EnableAutoDragDrop
        {
            get
            {
                return _RichTextBox.EnableAutoDragDrop;
            }
            set
            {
                _RichTextBox.EnableAutoDragDrop = value;
            }
        }
        [Browsable(false)]
        public override bool Focused
        {
            get
            {
                return _RichTextBox.Focused;
            }
        }
        /// <summary>
        /// Gets or sets whether selection should be hidden when the edit control loses focus.
        /// </summary>
        [Description("Indicates whether selection should be hidden when the edit control loses focus."), Category("Behavior"), DefaultValue(true)]
        public bool HideSelection
        {
            get
            {
                return _RichTextBox.HideSelection;
            }
            set
            {
                _RichTextBox.HideSelection = value;
            }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public RichTextBoxLanguageOptions LanguageOption
        {
            get
            {
                return _RichTextBox.LanguageOption;
            }
            set
            {
                _RichTextBox.LanguageOption = value;
            }
        }
        /// <summary>
        /// Gets or sets lines of text in a multi-line edit, as an array of String values.
        /// </summary>
        [Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version= 2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor)), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), MergableProperty(false), Localizable(true), Category("Appearance"), Description("Indicates lines of text in a multi-line edit, as an array of String values.")]
        public string[] Lines
        {
            get
            {
                return _RichTextBox.Lines;
            }
            set
            {
                _RichTextBox.Lines = value;
            }
        }
        /// <summary>
        /// Gets or sets maximum number of characters that can be entered into the edit control.
        /// </summary>
        [Localizable(true), Category("Behavior"), Description("Indicates maximum number of characters that can be entered into the edit control."), DefaultValue(0x7fffffff)]
        public int MaxLength
        {
            get
            {
                return _RichTextBox.MaxLength;
            }
            set
            {
                _RichTextBox.MaxLength = value;
            }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Modified
        {
            get
            {
                return _RichTextBox.Modified;
            }
        }
        /// <summary>
        /// Gets or sets whether the text in the control can span more than one line.
        /// </summary>
        [RefreshProperties(RefreshProperties.All), Description("Indicates whether the text in the control can span more than one line."), DefaultValue(true), Localizable(true), Category("Behavior")]
        public bool Multiline
        {
            get
            {
                return _RichTextBox.Multiline;
            }
            set
            {
                _RichTextBox.Multiline = value;
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Localizable(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new System.Windows.Forms.Padding Padding
        {
            get
            {
                return base.Padding;
            }
            set
            {
                base.Padding = value;
            }
        }
        /// <summary>
        /// Gets or sets whether the text in the edit control can be changed or not.
        /// </summary>
        [DefaultValue(false), Category("Behavior"), Description("Indicates whether the text in the edit control can be changed or not."), RefreshProperties(RefreshProperties.Repaint)]
        public bool ReadOnly
        {
            get
            {
                return _RichTextBox.ReadOnly;
            }
            set
            {
                _RichTextBox.ReadOnly = value;
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public string RedoActionName
        {
            get
            {
                return _RichTextBox.RedoActionName;
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), DefaultValue(true)]
        public bool RichTextShortcutsEnabled
        {
            get
            {
                return _RichTextBox.RichTextShortcutsEnabled;
            }
            set
            {
                _RichTextBox.RichTextShortcutsEnabled = value;
            }
        }

        /// <summary>
        /// Gets or sets right margin dimensions.
        /// </summary>
        [Description("Indicates the right margin dimensions."), Localizable(true), Category("Behavior"), DefaultValue(0)]
        public int RightMargin
        {
            get
            {
                return _RichTextBox.RightMargin;
            }
            set
            {
                _RichTextBox.RightMargin = value;
            }
        }
        [RefreshProperties(RefreshProperties.All), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public string Rtf
        {
            get
            {
                return _RichTextBox.Rtf;
            }
            set
            {
                _RichTextBox.Rtf = value;
                UpdateScrollBarsDelayed();
            }
        }
        /// <summary>
        /// Gets or sets for multi-line edit control, which scroll bars will be shown.
        /// </summary>
        [Localizable(true), Description("Indicates, for multi-line edit control, which scroll bars will be shown."), DefaultValue(typeof(RichTextBoxScrollBars), "Both"), Category("Appearance")]
        public RichTextBoxScrollBars ScrollBars
        {
            get
            {
                return _RichTextBox.ScrollBars;
            }
            set
            {
                _RichTextBox.ScrollBars = value;
            }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DefaultValue("")]
        public string SelectedRtf
        {
            get
            {
                return _RichTextBox.SelectedRtf;
            }
            set
            {
                _RichTextBox.SelectedRtf = value;
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public string SelectedText
        {
            get
            {
                return _RichTextBox.SelectedText;
            }
            set
            {
                _RichTextBox.SelectedText = value;
            }
        }
        [DefaultValue(typeof(HorizontalAlignment), "Left"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public HorizontalAlignment SelectionAlignment
        {
            get
            {
                return _RichTextBox.SelectionAlignment;
            }
            set
            {
                _RichTextBox.SelectionAlignment = value;
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public Color SelectionBackColor
        {
            get
            {
                return _RichTextBox.SelectionBackColor;
            }
            set
            {
                _RichTextBox.SelectionBackColor = value;
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool SelectionBullet
        {
            get
            {
                return _RichTextBox.SelectionBullet;
            }
            set
            {
                _RichTextBox.SelectionBullet = value;
            }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionCharOffset
        {
            get
            {
                return _RichTextBox.SelectionCharOffset;
            }
            set
            {
                _RichTextBox.SelectionCharOffset = value;
            }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color SelectionColor
        {
            get
            {
                return _RichTextBox.SelectionColor;
            }
            set
            {
                _RichTextBox.SelectionColor = value;
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public Font SelectionFont
        {
            get
            {
                return _RichTextBox.SelectionFont;
            }
            set
            {
                _RichTextBox.SelectionFont = value;
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int SelectionHangingIndent
        {
            get
            {
                return _RichTextBox.SelectionHangingIndent;
            }
            set
            {
                _RichTextBox.SelectionHangingIndent = value;
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int SelectionIndent
        {
            get
            {
                return _RichTextBox.SelectionIndent;
            }
            set
            {
                _RichTextBox.SelectionIndent = value;
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int SelectionLength
        {
            get
            {
                return _RichTextBox.SelectionLength;
            }
            set
            {
                _RichTextBox.SelectionLength = value;
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int SelectionProtected
        {
            get
            {
                return _RichTextBox.SelectionLength;
            }
            set
            {
                _RichTextBox.SelectionLength = value;
            }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionRightIndent
        {
            get
            {
                return _RichTextBox.SelectionRightIndent;
            }
            set
            {
                _RichTextBox.SelectionRightIndent = value;
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int SelectionStart
        {
            get
            {
                return _RichTextBox.SelectionStart;
            }
            set
            {
                _RichTextBox.SelectionStart = value;
            }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int[] SelectionTabs
        {
            get
            {
                return _RichTextBox.SelectionTabs;
            }
            set
            {
                _RichTextBox.SelectionTabs = value;
            }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public RichTextBoxSelectionTypes SelectionType
        {
            get
            {
                return _RichTextBox.SelectionType;
            }
        }
        /// <summary>
        /// Gets or sets whether shortcuts defined for the control are enabled.
        /// </summary>
        [DefaultValue(true), Description("Indicates whether shortcuts defined for the control are enabled."), Category("Behavior")]
        public bool ShortcutsEnabled
        {
            get
            {
                return _RichTextBox.ShortcutsEnabled;
            }
            set
            {
                _RichTextBox.ShortcutsEnabled = value;
            }
        }
        /// <summary>
        /// Gets or sets whether selection margin is visible.
        /// </summary>
        [DefaultValue(false), Category("Behavior"), Description("Indicates whether selection margin is visible.")]
        public bool ShowSelectionMargin
        {
            get
            {
                return _RichTextBox.ShowSelectionMargin;
            }
            set
            {
                _RichTextBox.ShowSelectionMargin = value;
            }
        }
        public new bool TabStop
        {
            get
            {
                return _RichTextBox.TabStop;
            }
            set
            {
                _RichTextBox.TabStop = value;
            }
        }
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=11.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        public override string Text
        {
            get
            {
                return _RichTextBox.Text;
            }
            set
            {
                _RichTextBox.Text = value;
                UpdateScrollBarsDelayed();
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int TextLength
        {
            get
            {
                return _RichTextBox.TextLength;
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public string UndoActionName
        {
            get
            {
                return _RichTextBox.UndoActionName;
            }
        }
        /// <summary>
        /// Gets or sets whether lines are automatically word-wrapped.
        /// </summary>
        [DefaultValue(true), Description("Indicates whether lines are automatically word-wrapped."), Localizable(true), Category("Behavior")]
        public bool WordWrap
        {
            get
            {
                return _RichTextBox.WordWrap;
            }
            set
            {
                _RichTextBox.WordWrap = value;
            }
        }
        /// <summary>
        /// Gets or sets current zoom factor for the control content.
        /// </summary>
        [Localizable(true), DefaultValue((float)1f), Category("Behavior"), Description("Indicates current zoom factor for the control content.")]
        public float ZoomFactor
        {
            get
            {
                return _RichTextBox.ZoomFactor;
            }
            set
            {
                _RichTextBox.ZoomFactor = value;
            }
        }

        public override ContextMenu ContextMenu
        {
            get
            {
                return _RichTextBox.ContextMenu;
            }
            set
            {
                _RichTextBox.ContextMenu = value;
            }
        }

        
        #endregion
    }
}
