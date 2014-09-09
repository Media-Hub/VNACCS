using System;
using System.Collections.Generic;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// SelectedElements
    ///</summary>
    public class SelectedElements
    {
        #region Private variables

        private List<SelectedRange> _Ranges;

        #endregion

        ///<summary>
        /// SelectedElements
        ///</summary>
        public SelectedElements()
        {
            _Ranges = new List<SelectedRange>();
        }

        #region Public properties

        #region Count

        ///<summary>
        /// Count
        ///</summary>
        public int Count
        {
            get
            {
                int count = 0;

                foreach (SelectedRange range in _Ranges)
                    count += range.Count;

                return (count);
            }
        }

        #endregion

        #region FirstIndex

        ///<summary>
        /// FirstIndex
        ///</summary>
        public int FirstIndex
        {
            get
            {
                if (_Ranges.Count > 0)
                    return (_Ranges[0].StartIndex);

                return (-1);
            }
        }

        #endregion

        #region LastIndex

        ///<summary>
        /// LastIndex
        ///</summary>
        public int LastIndex
        {
            get
            {
                if (_Ranges.Count > 0)
                    return (_Ranges[_Ranges.Count - 1].EndIndex);

                return (-1);
            }
        }

        #endregion

        #region Ranges

        ///<summary>
        /// Items
        ///</summary>
        public List<SelectedRange> Ranges
        {
            get { return (_Ranges); }
        }

        #endregion

        #endregion

        #region FindRange

        ///<summary>
        /// FindRange
        ///</summary>
        ///<param name="index"></param>
        ///<param name="selRange"></param>
        ///<returns></returns>
        public bool FindRange(int index, out SelectedRange selRange)
        {
            foreach (SelectedRange range in _Ranges)
            {
                if (range.Contains(index) == true)
                {
                    selRange = range;

                    return (true);
                }
            }

            selRange = null;

            return (false);
        }

        #endregion

        #region AddItem

        ///<summary>
        /// AddItem
        ///</summary>
        ///<param name="index"></param>
        ///<returns></returns>
        public void AddItem(int index)
        {
            for (int i = 0; i < _Ranges.Count; i++)
            {
                SelectedRange range = _Ranges[i];

                if (index <= range.EndIndex + 1)
                {
                    AddItem(range, i, index);
                    return;
                }
            }

            _Ranges.Add(new SelectedRange(index, index));

            ValidateRanges();
        }

        private void AddItem(SelectedRange range, int i, int index)
        {
            if (range.StartIndex - 1 == index)
            {
                range.StartIndex--;
            }
            else if (range.EndIndex + 1 == index)
            {
                range.EndIndex++;

                if (i + 1 < _Ranges.Count)
                {
                    if (_Ranges[i + 1].StartIndex == index + 1)
                    {
                        _Ranges[i].EndIndex = _Ranges[i + 1].EndIndex;
                        _Ranges.RemoveAt(i + 1);
                    }
                }
            }
            else if (range.Contains(index) == false)
            {
                SelectedRange selRange = new SelectedRange(index, index);

                _Ranges.Insert(i, selRange);
            }

            ValidateRanges();
        }

        #endregion

        #region AddRange

        ///<summary>
        ///</summary>
        ///<param name="startIndex"></param>
        ///<param name="endIndex"></param>
        public void AddRange(int startIndex, int endIndex)
        {
            for (int i = 0; i < _Ranges.Count; i++)
            {
                SelectedRange range = _Ranges[i];

                if (endIndex == range.StartIndex - 1)
                {
                    range.StartIndex = startIndex;

                    if (i > 0)
                    {
                        if (_Ranges[i - 1].EndIndex + 1 == startIndex)
                        {
                            _Ranges[i - 1].EndIndex = range.EndIndex;
                            _Ranges.RemoveAt(i);
                        }
                    }

                    CoalesceRanges();
                    ValidateRanges();
                    return;
                }

                if (startIndex == range.EndIndex + 1)
                {
                    range.EndIndex = endIndex;

                    if (i + 1 < _Ranges.Count)
                    {
                        if (_Ranges[i + 1].StartIndex - 1 == endIndex)
                        {
                            range.EndIndex = _Ranges[i + 1].EndIndex;
                            _Ranges.RemoveAt(i + 1);
                        }
                    }

                    CoalesceRanges();
                    ValidateRanges();
                    return;
                }
            }

            SelectedRange selRange = null;

            for (int i = 0; i < _Ranges.Count; i++)
            {
                SelectedRange range = _Ranges[i];

                if (range.StartIndex > startIndex)
                {
                    selRange = new SelectedRange(startIndex, endIndex);
                    _Ranges.Insert(i, selRange);
                    break;
                }
            }

            if (selRange == null)
            {
                selRange = new SelectedRange(startIndex, endIndex);
                _Ranges.Add(selRange);
            }

            CoalesceRanges();
        }

        #region CoalesceRanges

        private void CoalesceRanges()
        {
            SelectedRange xRange = _Ranges[0];

            for (int i = 1; i < _Ranges.Count; i++)
            {
                SelectedRange range = _Ranges[i];

                if (range.StartIndex - 1 <= xRange.EndIndex)
                {
                    if (range.EndIndex > xRange.EndIndex)
                        xRange.EndIndex = range.EndIndex;

                    if (range.StartIndex < xRange.StartIndex)
                        xRange.StartIndex = range.StartIndex;

                    range.StartIndex = -1;
                }
                else
                {
                    xRange = range;
                }
            }

            for (int i = _Ranges.Count - 1; i > 0; i--)
            {
                SelectedRange range = _Ranges[i];

                if (range.StartIndex == -1)
                    _Ranges.RemoveAt(i);
            }

            ValidateRanges();
        }

        #endregion

        #endregion

        #region RemoveItem

        ///<summary>
        /// RemoveItem
        ///</summary>
        ///<param name="index"></param>
        public void RemoveItem(int index)
        {
            SelectedRange selRange;

            if (FindRange(index, out selRange) == true)
                RemoveItem(index, selRange);
        }

        ///<summary>
        /// RemoveItem
        ///</summary>
        ///<param name="index"></param>
        ///<param name="range"></param>
        ///<returns></returns>
        public void RemoveItem(int index, SelectedRange range)
        {
            if (range.StartIndex == index)
            {
                range.StartIndex++;

                if (range.StartIndex > range.EndIndex)
                    _Ranges.Remove(range);
            }
            else if (range.EndIndex == index)
            {
                range.EndIndex--;

                if (range.StartIndex > range.EndIndex)
                    _Ranges.Remove(range);
            }
            else if (range.Contains(index) == true)
            {
                int i = _Ranges.IndexOf(range);
                int endIndex = range.EndIndex;

                range.EndIndex = index - 1;

                _Ranges.Insert(i + 1,
                    new SelectedRange(index + 1, endIndex));
            }

            ValidateRanges();
        }

        #endregion

        #region RemoveRange

        ///<summary>
        ///</summary>
        ///<param name="startIndex"></param>
        ///<param name="endIndex"></param>
        public bool RemoveRange(int startIndex, int endIndex)
        {
            bool itemsRemoved = false;

            for (int i = 0; i < _Ranges.Count; i++)
            {
                SelectedRange range = _Ranges[i];

                if (range.EndIndex < startIndex)
                    continue;

                if (range.StartIndex > endIndex)
                    break;

                if (range.EndIndex >= startIndex && range.EndIndex <= endIndex)
                {
                    if (range.StartIndex < startIndex)
                        range.EndIndex = startIndex - 1;
                    else
                        range.StartIndex = -1;

                    itemsRemoved = true;
                }
                else if (range.StartIndex >= startIndex && range.StartIndex <= endIndex)
                {
                    if (range.EndIndex > endIndex)
                        range.StartIndex = endIndex + 1;
                    else
                        range.StartIndex = -1;

                    itemsRemoved = true;
                }
                else
                {
                    int index = range.EndIndex;
                    range.EndIndex = startIndex - 1;

                    _Ranges.Insert(i + 1,
                        new SelectedRange(endIndex + 1, index));

                    itemsRemoved = true;
                    break;
                }
            }

            for (int i = _Ranges.Count - 1; i >= 0; i--)
            {
                SelectedRange range = _Ranges[i];

                if (range.StartIndex == -1)
                    _Ranges.RemoveAt(i);
            }

            ValidateRanges();

            return (itemsRemoved);
        }

        private void ValidateRanges()
        {
            int n = -2;

            for (int i = 0; i < _Ranges.Count; i++)
            {
                SelectedRange range = _Ranges[i];

                if (range.StartIndex - 1 <= n)
                    throw new Exception("Invalid Range StartIndex!");

                if (range.StartIndex > range.EndIndex)
                    throw new Exception("Invalid Range EndIndex!");

                n = range.EndIndex;
            }
        }

        #endregion

        #region Clear

        ///<summary>
        /// Clear
        ///</summary>
        public void Clear()
        {
            _Ranges.Clear();
        }

        #endregion

        #region OffsetIndices

        ///<summary>
        /// OffsetIndices
        ///</summary>
        ///<param name="index"></param>
        ///<param name="count"></param>
        public void OffsetIndices(int index, int count)
        {
            for (int i=0; i<_Ranges.Count; i++)
            {
                SelectedRange range = _Ranges[i];

                if (range.EndIndex >= index)
                {
                    if (range.StartIndex < index)
                    {
                        SelectedRange range2 = new SelectedRange(index, range.EndIndex);

                        range.EndIndex = index - 1;

                        _Ranges.Insert(++i, range2);
                    }
                                       
                    OffsetRanges(i, count);
                    CoalesceRanges();
                    break;
                }
            }

            ValidateRanges();
        }

        private void OffsetRanges(int n, int count)
        {
            for (int i = n; i < _Ranges.Count; i++)
            {
                SelectedRange range = _Ranges[i];

                range.StartIndex += count;
                range.EndIndex += count;
            }
        }

        #endregion

        #region GetNextIndex

        ///<summary>
        /// GetNextIndex
        ///</summary>
        ///<param name="index"></param>
        ///<returns></returns>
        public bool GetNextIndex(ref int index)
        {
            if (Count > 0)
            {
                if (index < LastIndex)
                {
                    index = (index < 0)
                        ? FirstIndex : NextIndex(index);

                    return (true);
                }
            }

            return (false);
        }

        #region NextIndex

        private int NextIndex(int index)
        {
            index++;

            foreach (SelectedRange range in _Ranges)
            {
                if (range.StartIndex >= index)
                    return (range.StartIndex);

                if (range.EndIndex >= index)
                    return (index);
            }

            return (LastIndex);
        }

        #endregion

        #endregion

        #region Copy

        internal SelectedElements Copy()
        {
            SelectedElements copy = new SelectedElements();

            foreach (SelectedRange range in _Ranges)
                copy.AddRange(range.StartIndex, range.EndIndex);

            return (copy);
        }

        #endregion
    }

    #region SelectedRange class

    ///<summary>
    /// SelectedRange
    ///</summary>
    public class SelectedRange
    {
        #region Private variables

        private int _StartIndex;
        private int _EndIndex;

        #endregion

        ///<summary>
        ///</summary>
        ///<param name="startIndex"></param>
        ///<param name="endIndex"></param>
        public SelectedRange(int startIndex, int endIndex)
        {
            _StartIndex = startIndex;
            _EndIndex = endIndex;
        }

        #region Public properties

        #region Count

        ///<summary>
        /// Count
        ///</summary>
        public int Count
        {
            get { return (_EndIndex - _StartIndex + 1); }
        }

        #endregion

        #region EndIndex

        ///<summary>
        /// EndIndex
        ///</summary>
        public int EndIndex
        {
            get { return (_EndIndex); }
            set { _EndIndex = value; }
        }

        #endregion

        #region StartIndex

        ///<summary>
        /// StartIndex
        ///</summary>
        public int StartIndex
        {
            get { return (_StartIndex); }
            set { _StartIndex = value; }
        }

        #endregion

        #endregion

        #region Contains

        ///<summary>
        /// Contains
        ///</summary>
        ///<param name="index"></param>
        ///<returns></returns>
        public bool Contains(int index)
        {
            return (index >= _StartIndex && index <= _EndIndex);
        }

        #endregion
    }

    #endregion
}
