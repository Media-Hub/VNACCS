using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Reflection;
using DevComponents.DotNetBar;
using System.Runtime.InteropServices;
using System.Drawing;

namespace DevComponents.DotNetBar.Design
{
    public class SymbolTypeEditor : System.Drawing.Design.UITypeEditor
    {
        #region Implementation
        private IWindowsFormsEditorService edSvc = null;
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context != null
                && context.Instance != null
                && provider != null)
            {
                edSvc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
                string stringValue = null;
                if (value != null) stringValue = value.ToString();

                if (edSvc != null)
                {
                    ItemPanel itemPanel = new ItemPanel();
                    itemPanel.Font = Symbols.FontAwesome;
                    itemPanel.MultiLine = true;
                    itemPanel.AutoScroll = true;
                    itemPanel.LayoutOrientation = eOrientation.Horizontal;
                    itemPanel.SelectedIndexChanged += new EventHandler(ItemPanelSelectedIndexChanged);
#if !TRIAL
                    itemPanel.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
#endif

                    Dictionary<int, bool> supportedChars = GetFontSupportedChars(itemPanel.Font);
                    for (int i = 0xf000; i <= 0xf200; i++)
                    {
                        if (!supportedChars.ContainsKey(i)) continue;
                        ButtonItem button = new ButtonItem();
                        button.AutoCheckOnClick = true;
                        button.OptionGroup = "sym";
                        button.Text = char.ConvertFromUtf32(i);
                        button.Tooltip = string.Format("{0:x}", i);
                        if (button.Text == stringValue) button.Checked = true;
                        itemPanel.Items.Add(button);
                    }
                    edSvc.DropDownControl(itemPanel);
                    itemPanel.SelectedIndexChanged -= new EventHandler(ItemPanelSelectedIndexChanged);
                    if (itemPanel.SelectedItem != null)
                        return itemPanel.SelectedItem.Text;
                }
            }

            return value;
        }

        void ItemPanelSelectedIndexChanged(object sender, EventArgs e)
        {
            if (edSvc != null)
                edSvc.CloseDropDown();
        }

        /// <summary>
        /// Gets the editor style used by the EditValue method.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that can be used to gain additional context information.</param>
        /// <returns>A UITypeEditorEditStyle value that indicates the style of editor used by EditValue. If the UITypeEditor does not support this method, then GetEditStyle will return None</returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null && context.Instance != null)
            {
                return UITypeEditorEditStyle.DropDown;
            }
            return base.GetEditStyle(context);
        }

        // This method indicates to the design environment that
        // the type editor will paint additional content in the
        // LightShape entry in the PropertyGrid.
        public override bool GetPaintValueSupported(
            ITypeDescriptorContext context)
        {
            return true;
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            string symbol = (string)e.Value;
            Font font = Symbols.GetFontAwesome(12);
            e.Graphics.DrawString(symbol, font, Brushes.Brown, e.Bounds.X, e.Bounds.Y + 2, StringFormat.GenericDefault);

        }
        #endregion

        #region GetFontSupportedChars
        private static readonly IntPtr HGDI_ERROR = new IntPtr(-1);
        private const int UIntSize = 4;
        [DllImport("gdi32.dll")]
        private static extern uint GetFontUnicodeRanges(IntPtr hdc, IntPtr lpgs);
        [DllImport("gdi32.dll")]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);
        private Dictionary<int, bool> GetFontSupportedChars(Font font)
        {
            Dictionary<int, bool> table = new Dictionary<int, bool>();

            IntPtr glyphSetData = IntPtr.Zero;
            IntPtr savedFont = HGDI_ERROR;
            IntPtr hdc = IntPtr.Zero;
            Graphics g = null;

            try 
            {
                g = Graphics.FromHwnd(IntPtr.Zero);
                hdc = g.GetHdc();
                IntPtr hFont = font.ToHfont();
                savedFont = SelectObject(hdc, hFont);
                if (savedFont == HGDI_ERROR)
                    throw new Exception(
                        "Unexpected failure of SelectObject.");
                uint size = GetFontUnicodeRanges(hdc, IntPtr.Zero);
                if (size == 0)
                    throw new Exception(
                        "Unexpected failure of GetFontUnicodeRanges.");
                glyphSetData = Marshal.AllocHGlobal((int)size);
                if (GetFontUnicodeRanges(hdc, glyphSetData) == 0)
                    throw new Exception(
                        "Unexpected failure of GetFontUnicodeRanges.");
                int offset = UIntSize;
                uint flags = (uint)Marshal.ReadInt32(
                    glyphSetData, offset);
                offset += UIntSize;
                uint codePointCount = (uint)Marshal.ReadInt32(
                    glyphSetData, offset);
                offset += UIntSize;
                uint rangeCount = (uint)Marshal.ReadInt32(
                    glyphSetData, offset);
                offset += UIntSize;
                for (uint index = 0; index < rangeCount; index++)
                {
                    ushort first = (ushort)Marshal.ReadInt16(
                        glyphSetData, offset);
                    offset += Marshal.SizeOf(typeof(ushort));
                    ushort count = (ushort)Marshal.ReadInt16(
                        glyphSetData, offset);
                    offset += Marshal.SizeOf(typeof(ushort));
                    for (int i = first; i < first + count; i++)
                    {
                        table.Add(i, true);
                    }
                } 
            }
			finally 
            {
                if (glyphSetData != IntPtr.Zero)
                    Marshal.FreeHGlobal(glyphSetData);
                if (savedFont != HGDI_ERROR)
                    SelectObject(hdc, savedFont);
                if (g != null)
                {
                    if (hdc != IntPtr.Zero)
                        g.ReleaseHdc(hdc);
                    g.Dispose();
                } 
            }
            return table;
        }        
        #endregion
    }
}
