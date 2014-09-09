using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.ComponentModel;

namespace DevComponents.DotNetBar
{
    // Uses Font Awesome - http://fortawesome.github.com/Font-Awesome
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class Symbols
    {
        #region WinApi
        [DllImport("gdi32")]
        static extern IntPtr AddFontMemResourceEx(IntPtr pbFont,
                                                           uint cbFont,
                                                           IntPtr pdv,
                                                           [In] ref uint pcFonts);
        [DllImport("gdi32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool RemoveFontMemResourceEx(IntPtr fh);
        #endregion


        #region Internal Implementation
        private static Dictionary<float, Font> _FontAwesomeCache = new Dictionary<float, Font>(10);
        /// <summary>
        /// Returns FontAwesome at specific size.
        /// </summary>
        /// <param name="fontSize">Font size in points</param>
        /// <returns>Font in desired size.</returns>
        public static Font GetFontAwesome(float fontSize)
        {
            Font font = null;
            EnsureFontLoaded();
            if (fontSize <= 0) return _FontAwesome;
            if(_FontAwesomeCache.TryGetValue(fontSize, out font))
                return font;
            font = new Font(_FontAwesome.FontFamily, fontSize, FontStyle.Regular, GraphicsUnit.Point);
            _FontAwesomeCache.Add(fontSize, font);
            return font;
        }

        private static Font _FontAwesome = null;
        /// <summary>
        /// Gets FontAwesome at default size.
        /// </summary>
        public static Font FontAwesome
        {
            get
            {
                EnsureFontLoaded();
                return _FontAwesome;
            }
        }
        /// <summary>
        /// Returns FontAwesome Family.
        /// </summary>
        public static FontFamily FontAwesomeFamily
        {
            get
            {
                EnsureFontLoaded();
                return _FontAwesome.FontFamily;
            }
        }

        private static PrivateFontCollection _PrivateFontCollection;
        private static GCHandle _FontAwesomeHandle;
        private static IntPtr _FontAwesomePointer;
        private static void EnsureFontLoaded()
        {
            if (_FontAwesome == null)
            {
                _PrivateFontCollection = new PrivateFontCollection();

                byte[] fontAwesomeBuffer = BarFunctions.LoadFont("SystemImages.FontAwesome.ttf");
                _FontAwesomeHandle = GCHandle.Alloc(fontAwesomeBuffer, GCHandleType.Pinned);
                _PrivateFontCollection.AddMemoryFont(_FontAwesomeHandle.AddrOfPinnedObject(), fontAwesomeBuffer.Length);
                uint rsxCnt = 1;
                _FontAwesomePointer = AddFontMemResourceEx(_FontAwesomeHandle.AddrOfPinnedObject(),
                                                     (uint)fontAwesomeBuffer.Length, IntPtr.Zero, ref rsxCnt);
                using (FontFamily ff = _PrivateFontCollection.Families[0])
                {
                    if (ff.IsStyleAvailable(FontStyle.Regular))
                    {
                        _FontAwesome = new Font(ff, _FontAwesomeDefaultSize, FontStyle.Regular, GraphicsUnit.Point);
                        _FontAwesomeCache.Add(_FontAwesomeDefaultSize, _FontAwesome);
                    }
                    else
                    {
                        // Error use default font...
                        _FontAwesome = SystemInformation.MenuFont;
                    }
                }
            }
        }

        private static float _FontAwesomeDefaultSize = 18;
        /// <summary>
        /// Gets the default size for the FontAwesome font size in points.
        /// </summary>
        public static float FontAwesomeDefaultSize
        {
            get { return _FontAwesomeDefaultSize; }
        }
        #endregion
    }
}
