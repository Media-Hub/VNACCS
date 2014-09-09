using System;
using System.Collections;
using System.Drawing;

namespace DevComponents.SuperGrid.TextMarkup
{
	/// <summary>
	/// Represents the serial content layout manager that arranges content blocks in series next to each other.
	/// </summary>
	public class SerialContentLayoutManager:IContentLayout
    {
        #region Events

        /// <summary>
        /// Occurs when X, Y position of next block is calcualted.
        /// </summary>
        public event LayoutManagerPositionEventHandler NextPosition;

        /// <summary>
        /// Occurs before new block is layed out.
        /// </summary>
        public event LayoutManagerLayoutEventHandler BeforeNewBlockLayout;

        #endregion

        #region Private Variables

        private int _BlockSpacing;
		private bool _FitContainerOversize;
        private bool _FitContainer;
        private bool _VerticalFitContainerWidth;
        private bool _HorizontalFitContainerHeight;
		private bool _EvenHeight;
		private bool _MultiLine;
        private bool _RightToLeft;
        private bool _OversizeDistribute;

		private eContentAlignment _ContentAlignment = eContentAlignment.Left;
		private eContentOrientation _ContentOrientation = eContentOrientation.Horizontal;
		private eContentVerticalAlignment _ContentVerticalAlignment = eContentVerticalAlignment.Middle;
        private eContentVerticalAlignment _BlockLineAlignment = eContentVerticalAlignment.Middle;

		#endregion

	    #region IContentLayout Members

		/// <summary>
		/// Performs layout of the content block.
		/// </summary>
		/// <param name="containerBounds">Container bounds to layout content blocks in.</param>
		/// <param name="contentBlocks">Content blocks to layout.</param>
		/// <param name="blockLayout">Block layout manager that resizes the content blocks.</param>
		/// <returns>The bounds of the content blocks within the container bounds.</returns>
        public virtual Rectangle Layout(Rectangle containerBounds, IBlock[] contentBlocks, BlockLayoutManager blockLayout)
		{
		    Rectangle blocksBounds = Rectangle.Empty;
		    Point position = containerBounds.Location;
		    ArrayList lines = new ArrayList();
		    lines.Add(new BlockLineInfo());
		    BlockLineInfo currentLine = lines[0] as BlockLineInfo;
		    bool switchToNewLine = false;
		    int visibleIndex = 0;

		    foreach (IBlock block in contentBlocks)
		    {
		        if (!block.Visible)
		        {
		            block.Bounds = Rectangle.Empty;
		            continue;
		        }

		        if (BeforeNewBlockLayout != null)
		        {
		            LayoutManagerLayoutEventArgs e = new
                        LayoutManagerLayoutEventArgs(block, position, visibleIndex);

		            BeforeNewBlockLayout(this, e);

		            position = e.CurrentPosition;

		            if (e.CancelLayout)
		                continue;
		        }
		        visibleIndex++;

		        Size availableSize = containerBounds.Size;
		        bool isBlockElement = false;
		        bool isNewLineTriggger = false;
		        bool canStartOnNewLine;

		        if (block is IBlockExtended)
		        {
		            IBlockExtended ex = block as IBlockExtended;

		            isBlockElement = ex.IsBlockElement;
		            isNewLineTriggger = ex.IsNewLineAfterElement;
		            canStartOnNewLine = ex.CanStartNewLine;
		        }
		        else
		            canStartOnNewLine = true;

		        if (!isBlockElement)
		        {
		            if (_ContentOrientation == eContentOrientation.Horizontal)
		                availableSize.Width = (containerBounds.Right - position.X);
		            else
		                availableSize.Height = (containerBounds.Bottom - position.Y);
		        }

		        // Resize the content block

		        blockLayout.Layout(block, availableSize);

		        if (_MultiLine && currentLine.Blocks.Count > 0)
		        {
		            if (_ContentOrientation == eContentOrientation.Horizontal &&
		                position.X + block.Bounds.Width > containerBounds.Right && canStartOnNewLine || isBlockElement ||
		                switchToNewLine)
		            {
		                position.X = containerBounds.X;
		                position.Y += (currentLine.LineSize.Height + _BlockSpacing);

		                currentLine = new BlockLineInfo();

		                currentLine.Line = lines.Count;

		                lines.Add(currentLine);
		            }
		            else if (_ContentOrientation == eContentOrientation.Vertical &&
		                     position.Y + block.Bounds.Height > containerBounds.Bottom && canStartOnNewLine || isBlockElement ||
		                     switchToNewLine)
		            {
		                position.Y = containerBounds.Y;
		                position.X += (currentLine.LineSize.Width + _BlockSpacing);

		                currentLine = new BlockLineInfo();

		                currentLine.Line = lines.Count;

		                lines.Add(currentLine);
		            }
		        }

		        if (_ContentOrientation == eContentOrientation.Horizontal)
		        {
		            if (block.Bounds.Height > currentLine.LineSize.Height)
		                currentLine.LineSize.Height = block.Bounds.Height;

		            currentLine.LineSize.Width = position.X + block.Bounds.Width - containerBounds.X;
		        }
		        else if (_ContentOrientation == eContentOrientation.Vertical)
		        {
		            if (block.Bounds.Width > currentLine.LineSize.Width)
		                currentLine.LineSize.Width = block.Bounds.Width;

		            currentLine.LineSize.Height = position.Y + block.Bounds.Height - containerBounds.Y;
		        }

		        currentLine.Blocks.Add(block);

		        if (block.Visible)
                    currentLine.VisibleItemsCount++;

		        block.Bounds = new Rectangle(position, block.Bounds.Size);

		        if (blocksBounds.IsEmpty)
		            blocksBounds = block.Bounds;
		        else
		            blocksBounds = Rectangle.Union(blocksBounds, block.Bounds);

		        switchToNewLine = isBlockElement | isNewLineTriggger;

		        position = GetNextPosition(block, position);
		    }

		    blocksBounds = AlignResizeBlocks(containerBounds, blocksBounds, lines);

		    if (_RightToLeft)
		        blocksBounds = MirrorContent(containerBounds, blocksBounds, contentBlocks);

		    blocksBounds = blockLayout.FinalizeLayout(containerBounds, blocksBounds, lines);

		    return blocksBounds;
		}

	    #endregion

		#region Internals

        private struct SizeExtended
        {
            public int Width;
            public int Height;
            public float WidthReduction;
            public float HeightReduction;
            public bool UseAbsoluteWidth;
        }

		private Rectangle AlignResizeBlocks(Rectangle containerBounds,Rectangle blocksBounds,ArrayList lines)
		{
			Rectangle newBounds=Rectangle.Empty;

			if(containerBounds.IsEmpty || blocksBounds.IsEmpty || ((BlockLineInfo)lines[0]).Blocks.Count==0)
				return newBounds;

            if (_ContentAlignment == eContentAlignment.Left && _ContentVerticalAlignment == eContentVerticalAlignment.Top &&
                !_FitContainer && !_FitContainerOversize && !_EvenHeight && _BlockLineAlignment == eContentVerticalAlignment.Top)
                return blocksBounds;

			Point[] offset=new Point[lines.Count];
            SizeExtended[] sizeOffset = new SizeExtended[lines.Count];
			foreach(BlockLineInfo lineInfo in lines)
			{
                if (_ContentOrientation == eContentOrientation.Horizontal)
                {
                    if (_FitContainer && containerBounds.Width > lineInfo.LineSize.Width ||
                        _FitContainerOversize && lineInfo.LineSize.Width > containerBounds.Width)
                    {
                        if (_OversizeDistribute && containerBounds.Width < lineInfo.LineSize.Width * .75)
                        {
                            sizeOffset[lineInfo.Line].Width = (int)Math.Floor((float)(containerBounds.Width - lineInfo.VisibleItemsCount * _BlockSpacing) / (float)lineInfo.VisibleItemsCount);
                            sizeOffset[lineInfo.Line].UseAbsoluteWidth = true;
                        }
                        else
                            sizeOffset[lineInfo.Line].Width = ((containerBounds.Width - lineInfo.VisibleItemsCount * _BlockSpacing) - lineInfo.LineSize.Width) / lineInfo.VisibleItemsCount;
                        sizeOffset[lineInfo.Line].WidthReduction = (float)(containerBounds.Width - lineInfo.VisibleItemsCount * _BlockSpacing) / (float)lineInfo.LineSize.Width;
                        blocksBounds.Width = containerBounds.Width;
                    }

                    if (_HorizontalFitContainerHeight && containerBounds.Height > blocksBounds.Height)
                        sizeOffset[lineInfo.Line].Height = (containerBounds.Height - lineInfo.LineSize.Height) / lines.Count;
                }
                else
                {
                    if (_FitContainer && containerBounds.Height > lineInfo.LineSize.Height ||
                        _FitContainerOversize && lineInfo.LineSize.Height > containerBounds.Height)
                    {
                        if (_OversizeDistribute && containerBounds.Width < lineInfo.LineSize.Width*.75)
                        {
                            sizeOffset[lineInfo.Line].Height = (int)
                                Math.Floor((float) (containerBounds.Height - lineInfo.VisibleItemsCount*_BlockSpacing)/
                                           (float) lineInfo.VisibleItemsCount);

                            sizeOffset[lineInfo.Line].UseAbsoluteWidth = true;
                        }
                        else
                            sizeOffset[lineInfo.Line].Height = ((containerBounds.Height -
                                                                 lineInfo.VisibleItemsCount*_BlockSpacing) -
                                                                lineInfo.LineSize.Height)/lineInfo.VisibleItemsCount;
                        sizeOffset[lineInfo.Line].HeightReduction =
                            (float) (containerBounds.Height - lineInfo.VisibleItemsCount*_BlockSpacing)/
                            (float) lineInfo.LineSize.Height;
                        blocksBounds.Height = containerBounds.Height;
                    }

                    if (_VerticalFitContainerWidth && containerBounds.Width > blocksBounds.Width)
                        sizeOffset[lineInfo.Line].Width = (containerBounds.Width - lineInfo.LineSize.Width)/lines.Count;
                }


			    if (_ContentOrientation == eContentOrientation.Horizontal && !_FitContainer)
                {
                    if (containerBounds.Width > blocksBounds.Width && _FitContainerOversize || !_FitContainerOversize)
                    {
                        switch (_ContentAlignment)
                        {
                            case eContentAlignment.Right:
                                if (containerBounds.Width > lineInfo.LineSize.Width)
                                    offset[lineInfo.Line].X = containerBounds.Width - lineInfo.LineSize.Width;
                                break;

                            case eContentAlignment.Center:
                                if (containerBounds.Width > lineInfo.LineSize.Width)
                                    offset[lineInfo.Line].X = (containerBounds.Width - lineInfo.LineSize.Width)/2;
                                break;
                        }
                    }
                }

			    if (_ContentOrientation == eContentOrientation.Vertical && !_FitContainer)
                {
                    if (containerBounds.Height > blocksBounds.Height && _FitContainerOversize || !_FitContainerOversize)
                    {
                        switch (_ContentVerticalAlignment)
                        {
                            case eContentVerticalAlignment.Bottom:
                                if (containerBounds.Height > lineInfo.LineSize.Height)
                                    offset[lineInfo.Line].Y = containerBounds.Height - lineInfo.LineSize.Height;
                                break;

                            case eContentVerticalAlignment.Middle:
                                if (containerBounds.Height > lineInfo.LineSize.Height)
                                    offset[lineInfo.Line].Y = (containerBounds.Height - lineInfo.LineSize.Height)/2;
                                break;
                        }
                    }
                }
			}

            if (_VerticalFitContainerWidth && containerBounds.Width > blocksBounds.Width && _ContentOrientation==eContentOrientation.Vertical)
                blocksBounds.Width = containerBounds.Width;

            else if(_HorizontalFitContainerHeight && containerBounds.Height>blocksBounds.Height && _ContentOrientation==eContentOrientation.Horizontal)
                blocksBounds.Height = containerBounds.Height;

			if(_ContentOrientation==eContentOrientation.Horizontal)
			{
				foreach(BlockLineInfo lineInfo in lines)
				{
                    foreach (IBlock block in lineInfo.Blocks)
                    {
                        if (!block.Visible)
                            continue;

                        Rectangle r = block.Bounds;

                        if (_EvenHeight && lineInfo.LineSize.Height > 0)
                            r.Height = lineInfo.LineSize.Height;

                        r.Offset(offset[lineInfo.Line]);

                        if (_ContentVerticalAlignment == eContentVerticalAlignment.Middle)
                        {
                            // Takes care of offset rounding error when both content is vertically centered and blocks in line are centered

                            if (_BlockLineAlignment == eContentVerticalAlignment.Middle)
                                r.Offset(0,
                                         ((containerBounds.Height - blocksBounds.Height) +
                                          (lineInfo.LineSize.Height - r.Height))/2);
                            else
                                r.Offset(0, (containerBounds.Height - blocksBounds.Height)/2);

                            // Line alignment of the block

                            if (_BlockLineAlignment == eContentVerticalAlignment.Bottom)
                                r.Offset(0, lineInfo.LineSize.Height - r.Height);
                        }
                        else if (_ContentVerticalAlignment == eContentVerticalAlignment.Bottom)
                            r.Offset(0, containerBounds.Height - blocksBounds.Height);

                        // To avoid rounding offset errors when dividing this is split see upper part

                        if (_ContentVerticalAlignment != eContentVerticalAlignment.Middle)
                        {
                            // Line alignment of the block

                            if (_BlockLineAlignment == eContentVerticalAlignment.Middle)
                                r.Offset(0, (lineInfo.LineSize.Height - r.Height)/2);

                            else if (_BlockLineAlignment == eContentVerticalAlignment.Bottom)
                                r.Offset(0, lineInfo.LineSize.Height - r.Height);
                        }

                        if (sizeOffset[lineInfo.Line].Width != 0)
                        {
                            if (_OversizeDistribute)
                            {
                                int nw = sizeOffset[lineInfo.Line].UseAbsoluteWidth
                                             ? sizeOffset[lineInfo.Line].Width
                                             : (int) Math.Floor(r.Width*sizeOffset[lineInfo.Line].WidthReduction);

                                offset[lineInfo.Line].X += nw - r.Width;

                                r.Width = nw;
                            }
                            else
                            {
                                r.Width += sizeOffset[lineInfo.Line].Width;
                                offset[lineInfo.Line].X += sizeOffset[lineInfo.Line].Width;
                            }
                        }

                        r.Height += sizeOffset[lineInfo.Line].Height;

                        block.Bounds = r;

                        if (newBounds.IsEmpty)
                            newBounds = block.Bounds;
                        else
                            newBounds = Rectangle.Union(newBounds, block.Bounds);
                    }

				    // Adjust for left-over size adjustment for odd difference
                    // between container width and the total block width

                    if (!_OversizeDistribute && sizeOffset[lineInfo.Line].Width != 0 &&
                        containerBounds.Width - (lineInfo.LineSize.Width + sizeOffset[lineInfo.Line].Width * lineInfo.Blocks.Count) != 0)
                    {
                        Rectangle r = ((IBlock) lineInfo.Blocks[lineInfo.Blocks.Count - 1]).Bounds;

                        r.Width += containerBounds.Width -
                                   (lineInfo.LineSize.Width + sizeOffset[lineInfo.Line].Width*lineInfo.Blocks.Count);

                        ((IBlock) lineInfo.Blocks[lineInfo.Blocks.Count - 1]).Bounds = r;
                    }
				}
			}
			else
			{
				foreach(BlockLineInfo lineInfo in lines)
				{
					foreach(IBlock block in lineInfo.Blocks)
					{
						if(!block.Visible)
							continue;

						Rectangle r=block.Bounds;

						if(_EvenHeight && lineInfo.LineSize.Width>0)
							r.Width=lineInfo.LineSize.Width;

						r.Offset(offset[lineInfo.Line]);

						if(_ContentAlignment==eContentAlignment.Center)
							r.Offset(((containerBounds.Width-blocksBounds.Width)+(lineInfo.LineSize.Width-r.Width))/2,0); //r.Offset((containerBounds.Width-blocksBounds.Width)/2+(lineInfo.LineSize.Width-r.Width)/2,0);
						else if(_ContentAlignment==eContentAlignment.Right)

							r.Offset((containerBounds.Width-blocksBounds.Width)+lineInfo.LineSize.Width-r.Width,0);
						r.Width+=sizeOffset[lineInfo.Line].Width;

						if(sizeOffset[lineInfo.Line].Height!=0)
						{
                            if (_OversizeDistribute)
                            {
                                int nw = sizeOffset[lineInfo.Line].UseAbsoluteWidth
                                    ? sizeOffset[lineInfo.Line].Height : (int)Math.Floor(r.Height * sizeOffset[lineInfo.Line].HeightReduction);
                                
                                offset[lineInfo.Line].Y += nw - r.Height;
                                r.Height = nw;
                            }
                            else
                            {
                                r.Height += sizeOffset[lineInfo.Line].Height;
                                offset[lineInfo.Line].Y += sizeOffset[lineInfo.Line].Height;
                            }
						}

						block.Bounds=r;

						if(newBounds.IsEmpty)
							newBounds=block.Bounds;
						else
							newBounds=Rectangle.Union(newBounds,block.Bounds);
					}

                    if (!_OversizeDistribute && sizeOffset[lineInfo.Line].Height != 0 && containerBounds.Height - (lineInfo.LineSize.Height + sizeOffset[lineInfo.Line].Height * lineInfo.Blocks.Count) != 0)
					{
						Rectangle r=((IBlock)lineInfo.Blocks[lineInfo.Blocks.Count-1]).Bounds;
						r.Height+=containerBounds.Height-(lineInfo.LineSize.Height+sizeOffset[lineInfo.Line].Height*lineInfo.Blocks.Count);
						((IBlock)lineInfo.Blocks[lineInfo.Blocks.Count-1]).Bounds=r;
					}
				}
			}
			return newBounds;
		}

		private Point GetNextPosition(IBlock block, Point position)
		{
            if (NextPosition != null)
            {
                LayoutManagerPositionEventArgs e = new LayoutManagerPositionEventArgs();

                e.Block = block;
                e.CurrentPosition = position;

                NextPosition(this, e);

                if (e.Cancel)
                    return e.NextPosition;
            }

			if(_ContentOrientation==eContentOrientation.Horizontal)
				position.X+=block.Bounds.Width+_BlockSpacing;
			else
				position.Y+=block.Bounds.Height+_BlockSpacing;

			return position;
		}

		internal class BlockLineInfo
		{
		    public ArrayList Blocks=new ArrayList();
			public Size LineSize = Size.Empty;
			public int Line;
            public int VisibleItemsCount;
		}

        private Rectangle MirrorContent(Rectangle containerBounds, Rectangle blockBounds, IBlock[] contentBlocks)
        {
            if (blockBounds.Width < containerBounds.Width)
                blockBounds.X = containerBounds.Right - ((blockBounds.X - containerBounds.X) + blockBounds.Width);

            else if (blockBounds.Width > containerBounds.Width)
                containerBounds.Width = blockBounds.Width;

            foreach (IBlock block in contentBlocks)
            {
                if (!block.Visible)
                    continue;

                Rectangle r = block.Bounds;

                block.Bounds = new Rectangle(containerBounds.Right - 
                    ((r.X - containerBounds.X) + r.Width), r.Y, r.Width, r.Height);
            }

            return blockBounds;
        }
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the spacing in pixels between content blocks. Default value is 0.
		/// </summary>
		public virtual int BlockSpacing
		{
			get {return _BlockSpacing;}
			set {_BlockSpacing=value;}
		}

		/// <summary>
		/// Gets or sets whether content blocks are forced to fit the container bounds if they 
		/// occupy more space than it is available by container. Default value is false.
		/// </summary>
		public virtual bool FitContainerOversize
		{
			get {return _FitContainerOversize;}
			set {_FitContainerOversize=value;}
		}

		/// <summary>
		/// Gets or sets whether content blocks are resized to fit the container bound if they
		/// occupy less space than it is available by container. Default value is false.
		/// </summary>
		public virtual bool FitContainer
		{
			get {return _FitContainer;}
			set {_FitContainer=value;}
		}

        /// <summary>
        /// Gets or sets whether content blocks are resized (Width) to fit container bounds if they
        /// occupy less space than the actual container width. Applies to the Vertical orientation only. Default value is false.
        /// </summary>
        public virtual bool VerticalFitContainerWidth
        {
            get { return _VerticalFitContainerWidth; }
            set { _VerticalFitContainerWidth = value; }
        }

        /// <summary>
        /// Gets or sets whether content blocks are resized (Height) to fit container bounds if they
        /// occupy less space than the actual container height. Applies to the Horizontal orientation only. Default value is false.
        /// </summary>
        public virtual bool HorizontalFitContainerHeight
        {
            get { return _HorizontalFitContainerHeight; }
            set { _HorizontalFitContainerHeight = value; }
        }

		/// <summary>
		/// Gets or sets the content orientation. Default value is Horizontal.
		/// </summary>
		public virtual eContentOrientation ContentOrientation
		{
			get {return _ContentOrientation;}
			set {_ContentOrientation=value;}
		}

		/// <summary>
		/// Gets or sets the content vertical alignment. Default value is Middle.
		/// </summary>
		public virtual eContentVerticalAlignment ContentVerticalAlignment
		{
			get {return _ContentVerticalAlignment;}
			set {_ContentVerticalAlignment=value;}
		}

        /// <summary>
        /// Gets or sets the block line vertical alignment. Default value is Middle.
        /// </summary>
        public virtual eContentVerticalAlignment BlockLineAlignment
        {
            get { return _BlockLineAlignment; }
            set { _BlockLineAlignment = value; }
        }

		/// <summary>
		/// Gets or sets the content horizontal alignment. Default value is Left.
		/// </summary>
		public virtual eContentAlignment ContentAlignment
		{
			get {return _ContentAlignment;}
			set {_ContentAlignment=value;}
		}

		/// <summary>
		/// Gets or sets whether all content blocks are resized so they have same height which is height of the tallest content block. Default value is false.
		/// </summary>
		public virtual bool EvenHeight
		{
			get {return _EvenHeight;}
			set {_EvenHeight=value;}
		}

        /// <summary>
        /// Gets or sets whether oversized blocks are resized based on the percentage reduction instead of based on equal pixel distribution. Default value is false.
        /// </summary>
        public virtual bool OversizeDistribute
        {
            get { return _OversizeDistribute; }
            set { _OversizeDistribute = value; }
        }

		/// <summary>
		/// Gets or sets whether content is wrapped into new line if it exceeds the width of the container.
		/// </summary>
		public bool MultiLine
		{
			get {return _MultiLine;}
			set {_MultiLine=value;}
		}

        /// <summary>
        /// Gets or sets whether layout is right-to-left.
        /// </summary>
        public bool RightToLeft
        {
            get { return _RightToLeft; }
            set { _RightToLeft = value; }
        }

		#endregion
	}

    /// <summary>
    /// Represents event arguments for SerialContentLayoutManager.NextPosition event.
    /// </summary>
    public class LayoutManagerPositionEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the block that is layed out.
        /// </summary>
        public IBlock Block;

        /// <summary>
        /// Gets or sets the current block position.
        /// </summary>
        public Point CurrentPosition = Point.Empty;

        /// <summary>
        /// Gets or sets the calculated next block position.
        /// </summary>
        public Point NextPosition = Point.Empty;

        /// <summary>
        /// Cancels default position calculation.
        /// </summary>
        public bool Cancel;
    }

    /// <summary>
    /// Represents event arguments for the SerialContentLayoutManager layout events.
    /// </summary>
    public class LayoutManagerLayoutEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the reference block object.
        /// </summary>
        public IBlock Block;

        /// <summary>
        /// Gets or sets the position block will assume.
        /// </summary>
        public Point CurrentPosition = Point.Empty;

        /// <summary>
        /// Cancel the layout of the block, applies only to BeforeXXX layout event.
        /// </summary>
        public bool CancelLayout;

        /// <summary>
        /// Gets or sets the visibility index of the block.
        /// </summary>
        public int BlockVisibleIndex;

        /// <summary>
        /// Creates new instance of the class and initializes it with default values.
        /// </summary>
        public LayoutManagerLayoutEventArgs(IBlock block, Point currentPosition, int visibleIndex)
        {
            this.Block = block;
            this.CurrentPosition = currentPosition;
            this.BlockVisibleIndex = visibleIndex;
        }
    }

    /// <summary>
    /// Delegate for SerialContentLayoutManager.NextPosition event.
    /// </summary>
    public delegate void LayoutManagerPositionEventHandler(object sender, LayoutManagerPositionEventArgs e);

    /// <summary>
    /// Delegate for the SerialContentLayoutManager layout events.
    /// </summary>
    public delegate void LayoutManagerLayoutEventHandler(object sender, LayoutManagerLayoutEventArgs e);
    

}
