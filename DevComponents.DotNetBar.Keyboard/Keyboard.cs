using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections.ObjectModel;

namespace DevComponents.DotNetBar.Keyboard
{
    // A Keyboard is defined by one or more KeyboardLayout objects.
    // A KeyboardLayout is a layout of certain Key objects.
    // A Key object represents all the data that a virtual key needs (caption, actual key-information to send 
    // to the attached control, hint, bounds, style, etc).

    // Because in this version of the VirtualKeyboard control the user cannot design new layouts for the keyboard, 
    // we will not publicly expose the model classes of the keyboard.

    /// <summary>
    /// Holds information about a single virtual keyboard Key.
    /// </summary>
    public class Key
    {
        private string _Caption;
        
        /// <summary>
        /// Gets the caption on the key.
        /// </summary>
        public string Caption
        {
            get { return _Caption; }
        }


        private string _Info;
        
        /// <summary>
        /// Gets the actual key-information that is sent to the attached control. The format of this information
        /// must confirm to the description for the SendKeys.Send function. For more details, see:
        /// http://msdn.microsoft.com/en-us/library/system.windows.forms.sendkeys.aspx
        /// </summary>
        public string Info
        {
            get { return _Info; }
        }


        private Rectangle _Bounds;

        /// <summary>
        /// Gets the bounds of the key.
        /// </summary>
        public Rectangle Bounds
        {
            get { return _Bounds; }
            internal set { _Bounds = value; }
        }

        
        private KeyStyle _Style;

        /// <summary>
        /// Gets a value representing the way the key looks on the virtual keyboard. A key might look normal, 
        /// lighter, darker or with a pressed effect.
        /// </summary>
        public KeyStyle Style
        {
            get { return _Style; }
        }

        
        private int _ChangeToLayout;

        /// <summary>
        /// Gets the index of the layout to change to, if this key is pressed. A positive value represents a new layout.
        /// KeyboardLayout.NoChange and KeyboardLayout.PreviousLayout constants can also be used with special meaning.
        /// </summary>
        /// <remarks>
        /// If this is a positive integer ( >= 0 ), it represents the index of the layout to switch to when this key 
        /// is pressed. In this case, the keyboard does not send any key information to the attached control (the Info
        /// field is ignored), only changes the layout.
        /// 
        /// If this is KeyboardLayout.NoChange the keyboard will send the info to the attached control.
        /// 
        /// If this is KeyboardLayout.PreviousLayout the keyboard will switch to the previous set layout. The keyboard
        /// maintains internally a stack of all used layouts. If no more layouts in the stakc, the keyboard switches to
        /// the layout with index 0.
        /// </remarks>
        public int ChangeToLayout
        {
            get { return _ChangeToLayout; }
        }


        private int _ChangeToLayoutEx;

        /// <summary>
        /// Gets the index of the layout to change to, when this key is double clicked. A positive value represents a new layout.
        /// KeyboardLayout.NoChange and KeyboardLayout.PreviousLayout constants can also be used with special meaning.
        /// </summary>
        /// <remarks>
        /// If this is a positive integer ( >= 0 ), it represents the index of the layout to switch to when this key 
        /// is pressed. In this case, the keyboard does not send any key information to the attached control (the Info
        /// field is ignored), only changes the layout.
        /// 
        /// If this is KeyboardLayout.NoChange the keyboard will send the info to the attached control.
        /// 
        /// If this is KeyboardLayout.PreviousLayout the keyboard will switch to the previous set layout. The keyboard
        /// maintains internally a stack of all used layouts. If no more layouts in the stakc, the keyboard switches to
        /// the layout with index 0.
        /// </remarks>
        public int ChangeToLayoutEx
        {
            get { return _ChangeToLayoutEx; }
        }

        
        private string _Hint;

        /// <summary>
        /// Gets the text that will be displayed in the upper part of the keyboard, as a hint for what the key does,
        /// usually in combination with a modifier key (e.g. Underline, Undo, etc).
        /// </summary>
        public string Hint
        {
            get { return _Hint; }
        }


        /// <summary>
        /// Creates a new Key object.
        /// </summary>
        /// <param name="caption">The text displayed on the key.</param>
        /// <param name="info">The information that will be sent to the attached control.</param>
        /// <param name="hint">The text in the upper part of the key, that provides a hint for the user about what the key does.</param>
        /// <param name="style">The way the key looks.</param>
        /// <param name="changeToLayout">The index of the layout to switch to.</param>
        public Key(string caption, string info = null, string hint = "", KeyStyle style = KeyStyle.Normal, int changeToLayout = KeyboardLayout.NoChange, int changeToLayoutEx = KeyboardLayout.NoChange)
        {
            _Caption = caption;
            _Info = info != null ? info : caption;
            _Style = style;
            _Hint = hint;
            _ChangeToLayout = changeToLayout;
            _ChangeToLayoutEx = changeToLayoutEx;
        }

        /// <summary>
        /// Returns a string that represents the current Key.
        /// </summary>
        /// <returns>A string that represents the current Key.</returns>
        public override string ToString()
        {
            return Caption;
        }
    }


    /// <summary>
    /// Specifies the way a key looks, specifically what color it uses from the ColorTable.
    /// </summary>
    public enum KeyStyle
    {
        /// <summary>
        /// Key should be drawn as a normal key colors.
        /// </summary>
        Normal,

        /// <summary>
        /// Key should be drawn with lighter colors.
        /// </summary>
        Light,

        /// <summary>
        /// Key should be drawn with darker colors.
        /// </summary>
        Dark,

        /// <summary>
        /// Key should be drawn with the pressed color.
        /// </summary>
        Pressed,

        /// <summary>
        /// Key should be drawn with the toggled color.
        /// </summary>
        Toggled
    }


    /// <summary>
    /// Represents a layout for a virtual keyboard.
    /// </summary>
    /// The Keys have coordinates in the KeyboardLayout specified in logical layout units. By default for this version
    /// of the control, a normal key is 10 logical units (LUs) width and 10 LUs height. A space between two keys is 1 LU.
    /// When rendering, we assume the whole Layout is stretched over the Keyboard control, so we need to change from 
    /// LUs (ranging from 0 to layout.LogicalWidth) to pixels (ranging from 0 to control.Width).
    /// This allows the layout to be resized to almost any sizes.
    public class KeyboardLayout
    {
        /// <summary>
        /// Indicates that a key should switch the Keyboard to the previous layout.
        /// </summary>
        public const int PreviousLayout = -2;

        /// <summary>
        /// Indicates that a key does not switch to any layout.
        /// </summary>
        public const int NoChange = -1;

        private KeysCollection _Keys = new KeysCollection();

        /// <summary>
        /// Gets the collection of the Keys in this KeyboardLayout.
        /// </summary>
        public KeysCollection Keys
        {
            get { return _Keys; }
        }

        /// <summary>
        /// Gets the logical width of this KeyboardLayout.
        /// </summary>
        public virtual int LogicalWidth
        {
            get
            {
                int w = 0;
                foreach (Key key in Keys)
                {
                    if (key.Bounds.Right > w)
                        w = key.Bounds.Right;
                }

                return w;
            }
        }

        /// <summary>
        /// Gets the logical height of this KeyboardLayout.
        /// </summary>
        public virtual int LogicalHeight
        {
            get
            {
                int h = 0;
                foreach (Key key in Keys)
                {
                    if (key.Bounds.Bottom > h)
                        h = key.Bounds.Bottom;
                }

                return h;
            }
        }

        /// <summary>
        /// Tests if a certain point hits a key on the virtual keyboard.
        /// </summary>
        /// <param name="x">The x-coordinate of the hit point (in logical units of this KeyboardLayout)</param>
        /// <param name="y">The y-coordinate of the hit point (in logical units of this KeyboardLayout)</param>
        /// <returns>Returns the key that was hit, or null if the point didn't hit any keys.</returns>
        public Key KeyHitTest(int x, int y)
        {
            foreach (Key key in Keys)
            {
                if (key.Bounds.Contains(x, y))
                    return key;
            }

            return null;
        }
    }


    /// <summary>
    /// Represents a layout for a virtual keyboard in which keys are defined in a linear way. This class
    /// allows creation of layouts in which keys can be ordered from left to right and from top to bottom.
    /// </summary>
    /// <remarks>
    /// With the linear layout, the keys are allowed to have any width. The height, however, must be the same.
    /// This layout is build by adding to it spaces, keys and new lines.
    /// A key represents a virtual key AND a space after it.
    /// A space represents a placeholder between two keys and it is used when there is need for a larger space
    /// between two certain keys
    /// A new line represents the fact that a new line of keys should be defined.
    /// </remarks>
    /// <example>
    /// The default virtual keyboard of the VirtualKeyboard control is defined in the following way:
    /// <code>
    /// AddKey("q");
    /// AddKey("w");
    /// AddKey("e");
    /// // ...
    /// AddKey("p");
    /// AddKey("Backspace");
    ///
    /// AddLine(); // Starts a new row of keys.
    /// AddSpace(4); // Creates a larger space at the beginning of the second row.
    ///
    /// AddKey("a");
    /// AddKey("s");
    /// // ...
    /// </code>
    /// </example>
    public class LinearKeyboardLayout : KeyboardLayout
    {
        private int _LastXOffset = 0;
        private int _LastYOffset = 0;

        /// <summary>
        /// The logical size of the space between two keys.
        /// </summary>
        public const int SpaceSize = 1;

        /// <summary>
        /// The logical size of a key.
        /// </summary>
        public const int NormalKeySize = 10;

        /// <summary>
        /// Adds a space to this LinearKeyboardLayout.
        /// </summary>
        /// <param name="width">The size of the space, in logical units.</param>
        public void AddSpace(int width = SpaceSize)
        {
            _LastXOffset += width;
        }


        /// <summary>
        /// Starts a new row of keys in this LinearKeyboardLayout.
        /// </summary>
        public void AddLine()
        {
            _LastXOffset = 0;
            _LastYOffset += NormalKeySize + SpaceSize;
        }


        /// <summary>
        /// Adds a new key to this LinearKeayboardLayout.
        /// </summary>
        /// <param name="caption">The text displayed on the key.</param>
        /// <param name="info">The information that will be sent to the attached control.</param>
        /// <param name="hint">The text in the upper part of the key, that provides a hint for the user about what the key does.</param>
        /// <param name="width">The width of the key, in logical units.</param>
        /// <param name="height">The height of the key, in logical units.</param>
        /// <param name="style">The way the key looks.</param>
        /// <param name="changeToLayout">The index of the layout to switch to.</param>
        /// <param name="changeToLayoutEx">The index of the layout to switch to, when key is double clicked.</param>
        public void AddKey(string caption, string info = null, string hint = "", int width = NormalKeySize, int height = NormalKeySize, KeyStyle style = KeyStyle.Normal, int layout = KeyboardLayout.NoChange, int layoutEx = KeyboardLayout.NoChange)
        {
            Key key = new Key(caption, info, hint, style, layout, layoutEx);

            key.Bounds = new Rectangle(_LastXOffset, _LastYOffset, width, height);
            
            _LastXOffset += width;
            AddSpace();

            Keys.Add(key);
        }
    }


    /// <summary>
    /// Represents a Virtual Keyboard.
    /// </summary>
    /// Each Keyboard has a collection of KeyboardLayout objects. The Keyboard also maintains the current Layout among all its layouts.
    public class Keyboard
    {
        private KeyboardLayoutsCollection _Layouts = new KeyboardLayoutsCollection();

        /// <summary>
        /// Gets the collection of the keyboard layouts in this Keyboard.
        /// </summary>
        public KeyboardLayoutsCollection Layouts
        {
            get { return _Layouts; }
        }

        private int _CurrentLayoutIndex;

        /// <summary>
        /// Gets or sets the current layout index of this Keyboard.
        /// </summary>
        public int CurrentLayoutIndex
        {
            get { return _CurrentLayoutIndex; }
            set
            {
                _LayoutStack.Push(_CurrentLayoutIndex);
                _CurrentLayoutIndex = value;
            }
        }


        /// <summary>
        /// Gets the current layout of this Keyboard.
        /// </summary>
        public KeyboardLayout CurrentLayout
        {
            get { return _Layouts[_CurrentLayoutIndex]; }
        }


        private Stack<int> _LayoutStack = new Stack<int>();

        /// <summary>
        /// Switches the Keyboard to the previous layout.
        /// </summary>
        public void ChangeToPreviousLayout()
        {
            if (_LayoutStack.Count > 0)
                _CurrentLayoutIndex = _LayoutStack.Pop();
            else
                _CurrentLayoutIndex = 0;
        }

        /// <summary>
        /// Switches the Keyboard to the first layout and resets the layout stack.
        /// </summary>
        public void ResetLayoutStack()
        {
            _LayoutStack.Clear();
            _CurrentLayoutIndex = 0;
        }


        /// <summary>
        /// Creates the default Keyboard populated with common layouts and functionality.
        /// </summary>
        /// <returns>A new Keyboard instance representing a default keyboard.</returns>
        public static Keyboard CreateDefaultKeyboard()
        {
            Keyboard keyboard = new Keyboard();

            LinearKeyboardLayout kc; // Actually there are 4 layout objects, but for code simplicity this variable is reused for creating each of them.

            #region Normal style configuration (no modifier keys pressed)
            
            kc = new LinearKeyboardLayout();
            keyboard._Layouts.Add(kc);

            kc.AddKey("q");
            kc.AddKey("w");
            kc.AddKey("e");
            kc.AddKey("r");
            kc.AddKey("t");
            kc.AddKey("y");
            kc.AddKey("u");
            kc.AddKey("i");
            kc.AddKey("o");
            kc.AddKey("p");
            kc.AddKey("Backspace", info: "{BACKSPACE}", width: 21);

            kc.AddLine();
            kc.AddSpace(4);

            kc.AddKey("a");
            kc.AddKey("s");
            kc.AddKey("d");
            kc.AddKey("f");
            kc.AddKey("g");
            kc.AddKey("h");
            kc.AddKey("j");
            kc.AddKey("k");
            kc.AddKey("l");
            kc.AddKey("'");
            kc.AddKey("Enter", info: "{ENTER}", width: 17);

            kc.AddLine();

            kc.AddKey("Shift", info: "", style: KeyStyle.Dark, layout: 1);
            kc.AddKey("z");
            kc.AddKey("x");
            kc.AddKey("c");
            kc.AddKey("v");
            kc.AddKey("b");
            kc.AddKey("n");
            kc.AddKey("m");
            kc.AddKey(",");
            kc.AddKey(".");
            kc.AddKey("?");
            kc.AddKey("Shift", info: "", style: KeyStyle.Dark, layout: 1);

            kc.AddLine();

            kc.AddKey("Ctrl", info: "", style: KeyStyle.Dark, layout: 2);
            kc.AddKey("&123", info: "", style: KeyStyle.Dark, layout: 3);
            kc.AddKey(":-)", info: ":-{)}", style: KeyStyle.Dark);
            //kc.AddKey("Alt", info: "%", style: KeyStyle.Dark);
            kc.AddKey(" ", width: 76);
            kc.AddKey("<", info: "{LEFT}", style: KeyStyle.Dark);
            kc.AddKey(">", info: "{RIGHT}", style: KeyStyle.Dark);

            #endregion

            #region Shift modifier pressed

            kc = new LinearKeyboardLayout();
            keyboard._Layouts.Add(kc);

            kc.AddKey("Q", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("W", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("E", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("R", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("T", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("Y", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("U", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("I", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("O", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("P", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("Backspace", info: "{BACKSPACE}", width: 21);

            kc.AddLine();
            kc.AddSpace(4);

            kc.AddKey("A", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("S", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("D", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("F", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("G", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("H", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("J", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("K", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("L", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("\"", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("Enter", info: "{ENTER}", width: 17);

            kc.AddLine();

            kc.AddKey("Shift", info: "", style: KeyStyle.Pressed, layout: 0, layoutEx: 4);
            kc.AddKey("Z", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("X", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("C", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("V", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("B", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("N", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("M", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey(";", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey(":", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("!", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("Shift", info: "", style: KeyStyle.Pressed, layout: 0, layoutEx: 4);

            kc.AddLine();

            kc.AddKey("Ctrl", info: "", style: KeyStyle.Dark, layout: 2);
            kc.AddKey("&123", info: "", style: KeyStyle.Dark, layout: 3);
            kc.AddKey(":-)", info: ":-{)}", style: KeyStyle.Dark, layout: KeyboardLayout.PreviousLayout);
            kc.AddKey(" ", width: 76, layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("<", info: "+{LEFT}", style: KeyStyle.Dark, layout: KeyboardLayout.PreviousLayout);
            kc.AddKey(">", info: "+{RIGHT}", style: KeyStyle.Dark, layout: KeyboardLayout.PreviousLayout);
            
            #endregion

            #region Ctrl modifier pressed
            
            kc = new LinearKeyboardLayout();
            keyboard._Layouts.Add(kc);

            kc.AddKey("q", info: "^q", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("w", info: "^w", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("e", info: "^e", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("r", info: "^r", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("t", info: "^t", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("y", info: "^y", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("u", info: "^u", hint: "Underline", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("i", info: "^i", hint: "Italic", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("o", info: "^o", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("p", info: "^p", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("Backspace", info: "^{BACKSPACE}", width: 21, layout: KeyboardLayout.PreviousLayout);

            kc.AddLine();
            kc.AddSpace(4);

            kc.AddKey("a", info: "^a", hint: "Select all", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("s", info: "^s", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("d", info: "^d", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("f", info: "^f", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("g", info: "^g", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("h", info: "^h", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("j", info: "^j", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("k", info: "^k", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("l", info: "^l", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("'", info: "^'", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("Enter", info: "^{ENTER}", width: 17, layout: KeyboardLayout.PreviousLayout);

            kc.AddLine();

            kc.AddKey("Shift", info: "", layout: 1);
            kc.AddKey("z", info: "^z", hint: "Undo", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("x", info: "^x", hint: "Cut", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("c", info: "^c", hint: "Copy", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("v", info: "^v", hint: "Paste", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("b", info: "^b", hint: "Bold", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("n", info: "^n", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("m", info: "^m", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey(",", info: "^,", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey(".", info: "^.", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("?", info: "^?", layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("Shift", info: "", layout: 1);

            kc.AddLine();

            kc.AddKey("Ctrl", info: "", style: KeyStyle.Pressed, layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("&123", info: "", style: KeyStyle.Dark, layout: 3);
            kc.AddKey(":-)", info: "^:-{)}", style: KeyStyle.Dark, layout: KeyboardLayout.PreviousLayout);
            kc.AddKey(" ", info: "^ ", width: 76, layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("<", info: "^{LEFT}", style: KeyStyle.Dark, layout: KeyboardLayout.PreviousLayout);
            kc.AddKey(">", info: "^{RIGHT}", style: KeyStyle.Dark, layout: KeyboardLayout.PreviousLayout);

            #endregion

            #region Symbols and numbers (&123) modifier pressed

            kc = new LinearKeyboardLayout();
            keyboard._Layouts.Add(kc);

            kc.AddKey("!");
            kc.AddKey("@");
            kc.AddKey("#");
            kc.AddKey("$");
            kc.AddKey("½");
            kc.AddKey("-");
            kc.AddKey("+", info: "{+}");

            kc.AddSpace(5);

            kc.AddKey("1", style: KeyStyle.Light);
            kc.AddKey("2", style: KeyStyle.Light);
            kc.AddKey("3", style: KeyStyle.Light);

            kc.AddSpace(5);

            kc.AddKey("Bcks", info: "{BACKSPACE}", style: KeyStyle.Dark);

            kc.AddLine();

            // second line
            kc.AddKey(";");
            kc.AddKey(":");
            kc.AddKey("\"");
            kc.AddKey("%", info: "{%}");
            kc.AddKey("&");
            kc.AddKey("/");
            kc.AddKey("*");

            kc.AddSpace(5);

            kc.AddKey("4", style: KeyStyle.Light);
            kc.AddKey("5", style: KeyStyle.Light);
            kc.AddKey("6", style: KeyStyle.Light);

            kc.AddSpace(5);

            kc.AddKey("Enter", info: "{ENTER}", style: KeyStyle.Dark);

            kc.AddLine();

            // third line
            kc.AddKey("(", info: "{(}");
            kc.AddKey(")", info: "{)}");
            kc.AddKey("[", info: "{[}");
            kc.AddKey("]", info: "{]}");
            kc.AddKey("_");
            kc.AddKey("\\");
            kc.AddKey("=");

            kc.AddSpace(5);

            kc.AddKey("7", style: KeyStyle.Light);
            kc.AddKey("8", style: KeyStyle.Light);
            kc.AddKey("9", style: KeyStyle.Light);

            kc.AddSpace(5);

            kc.AddKey("Tab", info: "{TAB}", style: KeyStyle.Dark);

            kc.AddLine();

            // forth line
            kc.AddKey("...", style: KeyStyle.Dark, layout: KeyboardLayout.PreviousLayout);
            kc.AddKey("&123", info: "", style: KeyStyle.Pressed, layout: KeyboardLayout.PreviousLayout);
            kc.AddKey(":-)", info: ":-{)}", style: KeyStyle.Dark);
            kc.AddKey("<", info: "{LEFT}", style: KeyStyle.Dark);
            kc.AddKey(">", info: "{RIGHT}", style: KeyStyle.Dark);
            kc.AddKey("Space", info: "^ ", width: 21);
            
            kc.AddSpace(5);

            kc.AddKey("0", style:KeyStyle.Light, width: 21);
            kc.AddKey(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, style: KeyStyle.Dark);

            kc.AddSpace(5);

            kc.AddLine();

            #endregion

            #region Shift modifier toggled

            kc = new LinearKeyboardLayout();
            keyboard._Layouts.Add(kc);

            kc.AddKey("Q");
            kc.AddKey("W");
            kc.AddKey("E");
            kc.AddKey("R");
            kc.AddKey("T");
            kc.AddKey("Y");
            kc.AddKey("U");
            kc.AddKey("I");
            kc.AddKey("O");
            kc.AddKey("P");
            kc.AddKey("Backspace", info: "{BACKSPACE}", width: 21);

            kc.AddLine();
            kc.AddSpace(4);

            kc.AddKey("A");
            kc.AddKey("S");
            kc.AddKey("D");
            kc.AddKey("F");
            kc.AddKey("G");
            kc.AddKey("H");
            kc.AddKey("J");
            kc.AddKey("K");
            kc.AddKey("L");
            kc.AddKey("'");
            kc.AddKey("Enter", info: "{ENTER}", width: 17);

            kc.AddLine();

            kc.AddKey("Shift", info: "", style: KeyStyle.Toggled, layout: 0);
            kc.AddKey("Z");
            kc.AddKey("X");
            kc.AddKey("C");
            kc.AddKey("V");
            kc.AddKey("B");
            kc.AddKey("N");
            kc.AddKey("M");
            kc.AddKey(",");
            kc.AddKey(".");
            kc.AddKey("?");
            kc.AddKey("Shift", info: "", style: KeyStyle.Toggled, layout: 0);

            kc.AddLine();

            kc.AddKey("Ctrl", info: "", style: KeyStyle.Dark, layout: 2);
            kc.AddKey("&123", info: "", style: KeyStyle.Dark, layout: 3);
            kc.AddKey(":-)", info: ":-{)}", style: KeyStyle.Dark);
            kc.AddKey(" ", width: 76);
            kc.AddKey("<", info: "+{LEFT}", style: KeyStyle.Dark);
            kc.AddKey(">", info: "+{RIGHT}", style: KeyStyle.Dark);

            #endregion

            return keyboard;
        }
    }

    /// <summary>
    /// Contains a collection of Key objects.
    /// </summary>
    public class KeysCollection : Collection<Key>
    {

    }

    /// <summary>
    /// Contains a collection of KeyboardLayout objects.
    /// </summary>
    public class KeyboardLayoutsCollection : Collection<KeyboardLayout>
    {

    }
}
