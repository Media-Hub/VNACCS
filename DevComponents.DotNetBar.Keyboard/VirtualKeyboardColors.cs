using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DevComponents.DotNetBar.Keyboard
{
    /// <summary>
    /// Defines the VirtualKeyboard colors.
    /// </summary>
    /// <remarks>
    /// This class maintains internally a Brush for each Color. These Brush objects are disposed internally when not needed anymore.
    /// A Brush of a certain color is obtained by setting the same Color property to that color.
    /// Maintaining the brushes here helps to have less GDI handles created / destroyed on each drawing of the keyboard.
    /// </remarks>
    public sealed class VirtualKeyboardColorTable : IDisposable
    {
        /// <summary>
        /// Creates a new color table for the VirtualKeyoard.
        /// </summary>
        public VirtualKeyboardColorTable()
        {
            _BackgroundBrush = new SolidBrush(BackgroundColor);
            _KeysBrush = new SolidBrush(KeysColor);
            _LightKeysBrush = new SolidBrush(LightKeysColor);
            _DarkKeysBrush = new SolidBrush(DarkKeysColor);
            _PressedKeysBrush = new SolidBrush(PressedKeysColor);
            _TextBrush = new SolidBrush(TextColor);
            _DownKeysBrush = new SolidBrush(DownKeysColor);
            _DownTextBrush = new SolidBrush(DownTextColor);
            _TopBarTextBrush = new SolidBrush(TopBarTextColor);
            _ToggleTextBrush = new SolidBrush(ToggleTextColor);
        }

        private static void ReCreateBrush(ref Brush brush, Color color)
        {
            if (brush != null)
                brush.Dispose();

            brush = new SolidBrush(color);
        }


        private Color _BackgroundColor = Color.Black;
        /// <summary>
        /// Gets or sets the color used to draw the background of the keyboard.
        /// </summary>
        public Color BackgroundColor
        {
            get { return _BackgroundColor; }
            set { _BackgroundColor = value; ReCreateBrush(ref _BackgroundBrush, _BackgroundColor); }
        }

        private Brush _BackgroundBrush;
        /// <summary>
        /// Gets a brush used for drawing the background of the keyboard.
        /// </summary>
        public Brush BackgroundBrush
        {
            get { return _BackgroundBrush; }
        }
        

        private Color _KeysColor = Color.FromArgb(0x30, 0x2f, 0x37);
        /// <summary>
        /// Gets or sets the color used for drawing normal keys.
        /// </summary>
        public Color KeysColor
        {
            get { return _KeysColor; }
            set { _KeysColor = value; ReCreateBrush(ref _KeysBrush, _KeysColor); }
        }

        private Brush _KeysBrush;
        /// <summary>
        /// Gets a brush used for drawing normal keys. 
        /// </summary>
        public Brush KeysBrush
        {
            get { return _KeysBrush; }
        }
        
        
        private Color _LightKeysColor = Color.FromArgb(0x45, 0x44, 0x4c);
        /// <summary>
        /// Gets or sets the color used for drawing light keys.
        /// </summary>
        public Color LightKeysColor
        {
            get { return _LightKeysColor; }
            set { _LightKeysColor = value; ReCreateBrush(ref _LightKeysBrush, _LightKeysColor); }
        }

        private Brush _LightKeysBrush;
        /// <summary>
        /// Gets a brush used for drawing light keys.
        /// </summary>
        public Brush LightKeysBrush
        {
            get { return _LightKeysBrush; }
        }


        private Color _DarkKeysColor = Color.FromArgb(0x1d, 0x1c, 0x21);
        /// <summary>
        /// Gets or sets the color used for drawing dark keys.
        /// </summary>
        public Color DarkKeysColor
        {
            get { return _DarkKeysColor; }
            set { _DarkKeysColor = value; ReCreateBrush(ref _DarkKeysBrush, _DarkKeysColor); }
        }
        
        private Brush _DarkKeysBrush;
        /// <summary>
        /// Gets a brush used for drawing dark keys.
        /// </summary>
        public Brush DarkKeysBrush
        {
            get { return _DarkKeysBrush; }
        }


        private Color _PressedKeysColor = Color.FromArgb(0x10, 0xa1, 0x51);
        /// <summary>
        /// Gets or sets a brush used for drawing pressed keys.
        /// </summary>
        public Color PressedKeysColor
        {
            get { return _PressedKeysColor; }
            set { _PressedKeysColor = value; ReCreateBrush(ref _PressedKeysBrush, _PressedKeysColor); }
        }

        private Brush _PressedKeysBrush;
        /// <summary>
        /// Gets a brush used for drawing pressed keys.
        /// </summary>
        public Brush PressedKeysBrush
        {
            get { return _PressedKeysBrush; }
        }


        private Color _DownKeysColor = Color.White;
        /// <summary>
        /// Gets or sets a brush used for drawing pressed key when the key is down.
        /// </summary>
        public Color DownKeysColor
        {
            get { return _DownKeysColor; }
            set { _DownKeysColor = value; ReCreateBrush(ref _DownKeysBrush, _DownKeysColor); }
        }

        private Brush _DownKeysBrush;
        /// <summary>
        /// Gets a brush used for drawing pressed key when the key is down.
        /// </summary>
        public Brush DownKeysBrush
        {
            get { return _DownKeysBrush; }
        }


        private Color _TextColor = Color.White;
        /// <summary>
        ///  Gets or sets a color used for drawing the text on the keys.
        /// </summary>
        public Color TextColor
        {
            get { return _TextColor; }
            set { _TextColor = value; ReCreateBrush(ref _TextBrush, _TextColor); }
        }

        private Brush _TextBrush;
        /// <summary>
        /// Gets a brush used for drawing the text on the keys.
        /// </summary>
        public Brush TextBrush
        {
            get { return _TextBrush; }
        }


        private Color _DownTextColor = Color.FromArgb(0x20, 0x20, 0x20);
        /// <summary>
        ///  Gets or sets a color used for drawing the text on a key when the key is down.
        /// </summary>
        public Color DownTextColor
        {
            get { return _DownTextColor; }
            set { _DownTextColor = value; ReCreateBrush(ref _DownTextBrush, _DownTextColor); }
        }

        private Brush _DownTextBrush;
        /// <summary>
        /// Gets a brush used for drawing the text on a key when the key is down.
        /// </summary>
        public Brush DownTextBrush
        {
            get { return _DownTextBrush; }
        }


        private Color _TopBarTextColor = Color.White;
        /// <summary>
        ///  Gets or sets a color used for drawing the text on the top bar.
        /// </summary>
        public Color TopBarTextColor
        {
            get { return _TopBarTextColor; }
            set { _TopBarTextColor = value; ReCreateBrush(ref _TopBarTextBrush, _TopBarTextColor); }
        }

        private Brush _TopBarTextBrush;
        /// <summary>
        /// Gets a brush used for drawing the text on the top bar.
        /// </summary>
        public Brush TopBarTextBrush
        {
            get { return _TopBarTextBrush; }
        }


        private Color _ToggleTextColor = Color.Green;
        /// <summary>
        /// Gets or sets a color used for drawing the text on a key when the key is in its toggled state.
        /// </summary>
        public Color ToggleTextColor
        {
            get { return _ToggleTextColor; }
            set { _ToggleTextColor = value; ReCreateBrush(ref _ToggleTextBrush, _ToggleTextColor); }
        }

        private Brush _ToggleTextBrush;
        /// <summary>
        /// Gets a brush used for drawing the text on a key when the key is in its toggled state.
        /// </summary>
        public Brush ToggleTextBrush
        {
            get { return _ToggleTextBrush; }
        }


        public void Dispose()
        {
            if (_BackgroundBrush != null)
                _BackgroundBrush.Dispose();

            if (_KeysBrush != null)
                _KeysBrush.Dispose();

            if (_LightKeysBrush != null)
                _LightKeysBrush.Dispose();

            if (_DarkKeysBrush != null)
                _DarkKeysBrush.Dispose();

            if (_PressedKeysBrush != null)
                _PressedKeysBrush.Dispose();

            if (_TextBrush != null)
                _TextBrush.Dispose();

            if (_DownTextBrush != null)
                _DownTextBrush.Dispose();

            if (_DownKeysBrush != null)
                _DownKeysBrush.Dispose();

            if (_TopBarTextBrush != null)
                _TopBarTextBrush.Dispose();

            if (_ToggleTextBrush != null)
                _ToggleTextBrush.Dispose();
        }
    }
}
