using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid.Style;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// GridImageEditControl
    ///</summary>
    [ToolboxItem(false)]
    public class GridImageEditControl : Control, IGridCellEditControl
    {
        #region Private variables

        private GridCell _Cell;
        private EditorPanel _EditorPanel;
        private Bitmap _EditorCellBitmap;

        private bool _ValueChanged;
        private bool _SuspendUpdate;

        private int _ImageIndex = -1;
        private ImageList _ImageList;
        private string _ImageKey;

        private Image _Image;
        private string _ImageLocation;
        private Image _LocationImage;
        private ImageSizeMode _ImageSizeMode = ImageSizeMode.Normal;

        private Image _StreamImage;
        private MemoryStream _ImageMemoryStream;
        private byte[] _ImageData;

        private StretchBehavior _StretchBehavior = StretchBehavior.Both;

        #endregion

        #region Public properties

        #region Image

        ///<summary>
        /// Image
        ///</summary>
        public Image Image
        {
            get { return (_Image); }
            set { _Image = value; }
        }

        #endregion

        #region ImageData

        ///<summary>
        /// ImageData
        ///</summary>
        public byte[] ImageData
        {
            get { return (_ImageData); }

            set
            {
                _ImageData = value;

                StreamImage = null;
            }
        }

        #endregion

        #region ImageIndex

        ///<summary>
        /// ImageKey
        ///</summary>
        public int ImageIndex
        {
            get { return (_ImageIndex); }
            set { _ImageIndex = value; }
        }

        #endregion

        #region ImageKey

        ///<summary>
        /// ImageKey
        ///</summary>
        public string ImageKey
        {
            get { return (_ImageKey); }
            set { _ImageKey = value; }
        }

        #endregion

        #region ImageList

        ///<summary>
        /// ImageList
        ///</summary>
        public ImageList ImageList
        {
            get { return (_ImageList); }
            set { _ImageList = value; }
        }

        #endregion

        #region ImageLocation

        ///<summary>
        /// ImageLocation
        ///</summary>
        public string ImageLocation
        {
            get { return (_ImageLocation); }

            set
            {
                _ImageLocation = value;

                LocationImage = null;
            }
        }

        #endregion

        #region ImageSizeMode

        ///<summary>
        /// ImageSizeMode
        ///</summary>
        public ImageSizeMode ImageSizeMode
        {
            get { return (_ImageSizeMode); }
            set { _ImageSizeMode = value; }
        }

        #endregion

        #endregion

        #region Private properties

        #region EffectiveImage

        private Image EffectiveImage
        {
            get
            {
                if (_Image != null)
                    return (_Image);

                if (StreamImage != null)
                    return (StreamImage);

                if (_ImageList != null)
                {
                    _ImageList.TransparentColor = Color.Magenta;

                    if ((uint)_ImageIndex < _ImageList.Images.Count)
                        return (_ImageList.Images[_ImageIndex]);

                    if (_ImageKey != null)
                        return (_ImageList.Images[_ImageKey]);
                }

                return (LocationImage);
            }
        }

        #endregion

        #region ImageMemoryStream

        private MemoryStream ImageMemoryStream
        {
            get
            {
                if (_ImageMemoryStream == null)
                    _ImageMemoryStream = new MemoryStream();

                return (_ImageMemoryStream);
            }

            set
            {
                if (_ImageMemoryStream != null)
                    _ImageMemoryStream.Dispose();

                _ImageMemoryStream = value;
            }
        }

        #endregion

        #region LocationImage

        private Image LocationImage
        {
            get { return (_LocationImage); }

            set
            {
                if (_LocationImage != null)
                    _LocationImage.Dispose();
                    
                _LocationImage = value;

                if (string.IsNullOrEmpty(_ImageLocation) == false)
                    _LocationImage = Image.FromFile(_ImageLocation);
            }
        }

        #endregion

        #region StreamImage

        private Image StreamImage
        {
            get
            {
                if (_StreamImage == null)
                {
                    if (_ImageData != null)
                    {
                        MemoryStream ms = ImageMemoryStream;

                        if (ms != null)
                        {
                            ms.SetLength(0);
                            ms.Write(_ImageData, 0, _ImageData.Length);

                            StreamImage = Image.FromStream(ms);
                        }
                    }
                }

                return (_StreamImage);
            }

            set
            {
                if (_StreamImage != null)
                    _StreamImage.Dispose();

                _StreamImage = value;
            }
        }

        #endregion

        #endregion

        #region OnInvalidated

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);

            if (_Cell != null && _SuspendUpdate == false)
                _Cell.InvalidateRender();
        }

        #endregion

        #region OnPaintBackground

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            _Cell.PaintEditorBackground(e, this);
        }

        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            ResetImageInfo();

            ImageMemoryStream = null;

            base.Dispose(disposing);
        }

        #endregion

        #region SetValue

        ///<summary>
        /// SetValue
        ///</summary>
        ///<param name="value"></param>
        public virtual void SetValue(object value)
        {
            GridPanel panel = _Cell.GridPanel;

            if (panel.IsDesignerHosted == false)
            {
                ResetImageInfo();

                if (value == null ||
                    (panel.NullValue == NullValue.DBNull && value == DBNull.Value))
                {
                }
                else if (value is Image)
                {
                    Image = (Image)value;
                }
                else if (value is string)
                {
                    if (_Cell.IsValueExpression == true)
                        value = _Cell.GetExpValue((string)value);

                    if (SetImageKey((string)value) == false)
                    {
                        int index;

                        if (int.TryParse((string)value, out index) == true)
                            SetImageIndex(index);
                        else
                            ImageLocation = (string)value;
                    }
                }
                else if (value is byte[])
                {
                    ImageData = (byte[])value;
                }
                else
                {
                    int index = Convert.ToInt32(value);

                    SetImageIndex(index);
                }
            }
        }

        #region ResetImageInfo

        private void ResetImageInfo()
        {
            Image = null;
            ImageKey = null;
            ImageIndex = -1;
            ImageLocation = null;
            ImageData = null;
        }

        #endregion

        #region SetImageKey

        private bool SetImageKey(string key)
        {
            if (_ImageList != null)
            {
                if (_ImageList.Images.ContainsKey(key))
                {
                    _ImageKey = key;

                    return (true);
                }
            }

            return (false);
        }

        #endregion

        #region SetImageIndex

        private void SetImageIndex(int index)
        {
            if (_ImageList != null && index < _ImageList.Images.Count)
                ImageIndex = index;
        }

        #endregion

        #endregion

        #region IGridCellEditControl Members

        #region Public properties

        #region CanInterrupt

        public bool CanInterrupt
        {
            get { return (true); }
        }

        #endregion

        #region CellEditMode

        public CellEditMode CellEditMode
        {
            get { return (CellEditMode.InPlace); }
        }

        #endregion

        #region EditorCell

        public GridCell EditorCell
        {
            get { return (_Cell); }
            set { _Cell = value; }
        }

        #endregion

        #region EditorCellBitmap

        ///<summary>
        /// EditorCellBitmap
        ///</summary>
        public Bitmap EditorCellBitmap
        {
            get { return (_EditorCellBitmap); }

            set
            {
                if (_EditorCellBitmap != null)
                    _EditorCellBitmap.Dispose();

                _EditorCellBitmap = value;
            }
        }

        #endregion

        #region EditorFormattedValue

        ///<summary>
        /// EditorFormattedValue
        ///</summary>
        public virtual string EditorFormattedValue
        {
            get { return (Text); }
        }

        #endregion

        #region EditorPanel

        public EditorPanel EditorPanel
        {
            get { return (_EditorPanel); }
            set { _EditorPanel = value; }
        }

        #endregion

        #region EditorValue

        public virtual object EditorValue
        {
            get
            {
                if (String.IsNullOrEmpty(ImageLocation) == false)
                    return (ImageLocation);

                if (String.IsNullOrEmpty(_ImageKey) == false)
                    return (_ImageKey);

                return (_ImageIndex);
            }

            set { SetValue(value); }
        }

        #endregion

        #region EditorValueChanged

        public virtual bool EditorValueChanged
        {
            get { return (_ValueChanged); }

            set
            {
                if (_ValueChanged != value)
                {
                    _ValueChanged = value;

                    if (value == true)
                        _Cell.SetEditorDirty(this);
                }
            }
        }

        #endregion

        #region EditorValueType

        ///<summary>
        /// EditorValueType
        ///</summary>
        public virtual Type EditorValueType
        {
            get { return (typeof(string)); }
        }

        #endregion

        #region StretchBehavior

        public virtual StretchBehavior StretchBehavior
        {
            get { return (_StretchBehavior); }
            set { _StretchBehavior = value; }
        }

        #endregion

        #region SuspendUpdate

        public bool SuspendUpdate
        {
            get { return (_SuspendUpdate); }
            set { _SuspendUpdate = value; }
        }

        #endregion

        #region ValueChangeBehavior

        public virtual ValueChangeBehavior ValueChangeBehavior
        {
            get { return (ValueChangeBehavior.InvalidateRender); }
        }

        #endregion

        #endregion

        #region InitializeContext

        ///<summary>
        /// InitializeContext
        ///</summary>
        ///<param name="cell"></param>
        ///<param name="style"></param>
        public virtual void InitializeContext(GridCell cell, CellVisualStyle style)
        {
            _Cell = cell;

            if (style != null)
                Enabled = (_Cell.IsReadOnly == false);

            SetValue(_Cell.Value);

            _ValueChanged = false;
        }

        #endregion

        #region GetProposedSize

        ///<summary>
        /// GetProposedSize
        ///</summary>
        ///<param name="g"></param>
        ///<param name="cell"></param>
        ///<param name="style"></param>
        ///<param name="constraintSize"></param>
        ///<returns></returns>
        public virtual Size GetProposedSize(Graphics g,
            GridCell cell, CellVisualStyle style, Size constraintSize)
        {
            Image image = EffectiveImage;

            Size size = (image != null)
                ? image.Size : new Size(20, 20);

            return (size);
        }

        #endregion

        #region Edit support

        #region BeginEdit

        public virtual bool BeginEdit(bool selectAll)
        {
            return (false);
        }

        #endregion

        #region EndEdit

        public virtual bool EndEdit()
        {
            return (false);
        }

        #endregion

        #region CancelEdit

        public virtual bool CancelEdit()
        {
            return (false);
        }

        #endregion

        #endregion

        #region CellRender

        public virtual void CellRender(Graphics g)
        {
            Image image = EffectiveImage;

            if (image != null)
            {
                Rectangle r = _Cell.GetCellEditBounds(this);

                switch (_ImageSizeMode)
                {
                    case ImageSizeMode.Normal:
                        Rectangle sr = r;
                        sr.Location = Point.Empty;

                        g.DrawImage(image, r, sr, GraphicsUnit.Pixel);
                        break;

                    case ImageSizeMode.CenterImage:
                        RenderImageCentered(g, image, r);
                        break;

                    case ImageSizeMode.StretchImage:
                        g.DrawImage(image, r);
                        break;

                    case ImageSizeMode.Zoom:
                        RenderImageScaled(g, image, r);
                        break;
                }
            }
        }

        #region RenderImageCentered

        private void RenderImageCentered(Graphics g, Image image, Rectangle r)
        {
            Rectangle sr = r;
            sr.Location = Point.Empty;

            if (image.Width > r.Width)
                sr.X += (image.Width - r.Width) / 2;
            else
                r.X += (r.Width - image.Width) / 2;

            if (image.Height > r.Height)
                sr.Y += (image.Height - r.Height) / 2;
            else
                r.Y += (r.Height - image.Height) / 2;

            g.DrawImage(image, r, sr, GraphicsUnit.Pixel);
        }

        #endregion

        #region RenderImageScaled

        private void RenderImageScaled(Graphics g, Image image, Rectangle r)
        {
            SizeF size = new SizeF(image.Width / image.HorizontalResolution,
                image.Height / image.VerticalResolution);

            float scale = Math.Min(r.Width / size.Width, r.Height / size.Height);

            size.Width *= scale;
            size.Height *= scale;

            g.DrawImage(image, r.X + (r.Width - size.Width) / 2,
                r.Y + (r.Height - size.Height) / 2,
                size.Width, size.Height);

        }

        #endregion

        #endregion

        #region Keyboard support

        #region CellKeyDown

        ///<summary>
        /// CellKeyDown
        ///</summary>
        public virtual void CellKeyDown(KeyEventArgs e)
        {
            OnKeyDown(e);
        }

        #endregion

        #region WantsInputKey

        public virtual bool WantsInputKey(Keys key, bool gridWantsKey)
        {
            return (gridWantsKey == false);
        }

        #endregion

        #endregion

        #region Mouse support

        #region OnCellMouseMove

        ///<summary>
        /// OnCellMouseMove
        ///</summary>
        ///<param name="e"></param>
        public virtual void OnCellMouseMove(MouseEventArgs e)
        {
            OnMouseMove(e);
        }

        #endregion

        #region OnCellMouseEnter

        ///<summary>
        /// OnCellMouseEnter
        ///</summary>
        ///<param name="e"></param>
        public virtual void OnCellMouseEnter(EventArgs e)
        {
            OnMouseEnter(e);
        }

        #endregion

        #region OnCellMouseLeave

        ///<summary>
        /// OnCellMouseLeave
        ///</summary>
        ///<param name="e"></param>
        public virtual void OnCellMouseLeave(EventArgs e)
        {
            OnMouseLeave(e);
        }

        #endregion

        #region OnCellMouseUp

        ///<summary>
        /// OnCellMouseUp
        ///</summary>
        ///<param name="e"></param>
        public virtual void OnCellMouseUp(MouseEventArgs e)
        {
            OnMouseUp(e);
        }

        #endregion

        #region OnCellMouseDown

        ///<summary>
        /// OnCellMouseDown
        ///</summary>
        ///<param name="e"></param>
        public virtual void OnCellMouseDown(MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        #endregion

        #endregion

        #endregion  
    }

    #region ImageSizeMode

    ///<summary>
    /// ImageSizeMode
    ///</summary>
    public enum ImageSizeMode
    {
        ///<summary>
        /// The image is placed in the upper-left
        /// corner of the cell and is clipped if needed to fit.
        ///</summary>
        Normal,

        ///<summary>
        /// The image is stretched or shrunk to fit the cell.
        ///</summary>
        StretchImage,

        ///<summary>
        /// The image is displayed in the center of the cell if
        /// the cell is larger than the image. If the image is larger
        /// than the cell, the image is placed in the center of the 
        /// cell and the outside edges are clipped.
        ///</summary>
        CenterImage,

        ///<summary>
        /// The size of the image is increased or decreased to fit the
        /// cell, maintaining the size ratio.
        ///</summary>
        Zoom
    }

    #endregion
}
