using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DevComponents.DotNetBar.SuperGrid
{
	/// <summary>
    /// Expression evaluator
	/// </summary>
	public class EEval
	{
        #region Private data

        private int _TCount;
        private int _PCount;
        private ETokens[] _PTokens = new ETokens[10];
        private List<ETokens> _PfTokens = new List<ETokens>();

        private GridCell _Cell;
        private GridPanel _GridPanel;
        private List<GridCell> _UsedCells;

        private string _Source;
        private List<string> _StringPool = new List<string>();

        private object _Tag;

        #endregion

	    ///<summary>
	    /// Expression evaluator constructor
	    ///</summary>
	    ///<param name="gridPanel">Associated GridPanel</param>
	    ///<param name="source">'Excel-like' expression (eg. =d4+d5)</param>
	    public EEval(GridPanel gridPanel, string source)
            : this(null, source, new List<GridCell>())
        {
            _GridPanel = gridPanel;
        }

        internal EEval(GridCell cell, string source)
            : this(cell, source, new List<GridCell>())
        {
        }

	    private EEval(GridCell cell, string source, List<GridCell> usedCells)
        {
            _Cell = cell;
            _UsedCells = usedCells;

            if (_Cell != null)
                _GridPanel = cell.GridPanel;

            Source = source;
        }

        #region Public properties

        #region Cell

        ///<summary>
        /// Gets the associated Grid Cell, if any
        ///</summary>
        public GridCell Cell
        {
            get { return (_Cell); }
        }

        #endregion

        #region GridPanel

        ///<summary>
        /// Gets the associated Grid Panel
        ///</summary>
        public GridPanel GridPanel
        {
            get { return (_GridPanel); }
        }

        #endregion

        #region Source

        ///<summary>
        /// Gets or sets the expression source
        ///</summary>
        public string Source
        {
            get { return (_Source); }

            set
            {
                _Source = value;

                Tokenize(value);
            }
        }

        #endregion

        #region Tag

        ///<summary>
        /// Gets or sets user-defined data associated with the object
        ///</summary>
        public object Tag
        {
            get { return (_Tag); }
            set { _Tag = value; }
        }

        #endregion

        #endregion

        #region Tokenize code

        private void Tokenize(string source)
        {
            const string sref = @"([\.]|[a-zA-Z_]+|""[^""]+"")\s*([\.]|\d+)";

            Regex p = new Regex(
                "(" + sref + "\\s*:\\s*" + sref + ")|(" + sref + ")|" +
                "([a-zA-Z0-9.]+)|" +
                "\"([^\"]+)\"|" +
                "([()+\\-*/%,|&\\^])|(<<)|(>>)");

            MatchCollection mc = p.Matches(source);

            Expr(mc);

            ETokens t = GetToken(mc);

            if (t != ETokens.BadToken)
                throw new Exception("Expression error.");
        }

        #region Expr

        /// <summary>
        /// Main expression entry routine
        /// </summary>
        private void Expr(MatchCollection mc)
		{
		    Term1(mc);

			ETokens t = GetToken(mc);

            while (t == ETokens.BitwiseOr)
			{
				Term1(mc);

				_PfTokens.Add(t);

				t = GetToken(mc);
			}

			PutToken(t);
        }

        #endregion

        #region Term1

        /// <summary>
        /// Handles the ^ operator
        /// </summary>
        private void Term1(MatchCollection mc)
        {
            Term2(mc);

            ETokens t = GetToken(mc);

            while (t == ETokens.BitwiseXor)
            {
                Term2(mc);

                _PfTokens.Add(t);

                t = GetToken(mc);
            }

            PutToken(t);
        }

        #endregion

        #region Term2

        /// <summary>
        /// Handles the & operator
        /// </summary>
        private void Term2(MatchCollection mc)
        {
            Term3(mc);

            ETokens t = GetToken(mc);

            while (t == ETokens.BitwiseAnd)
            {
                Term3(mc);

                _PfTokens.Add(t);

                t = GetToken(mc);
            }

            PutToken(t);
        }

        #endregion

        #region Term3

        /// <summary>
        /// Handles the shift left and right operators
        /// </summary>
        private void Term3(MatchCollection mc)
        {
            Term4(mc);

            ETokens t = GetToken(mc);

            while (t == ETokens.ShiftLeft || t == ETokens.ShiftRight)
            {
                Term4(mc);

                _PfTokens.Add(t);

                t = GetToken(mc);
            }

            PutToken(t);
        }

        #endregion

        #region Term4

        /// <summary>
        /// Handles %, *, and / operators
        /// </summary>
        private void Term4(MatchCollection mc)
        {
            Term5(mc);

            ETokens t = GetToken(mc);

            while (t == ETokens.Add || t == ETokens.Subtract)
            {
                Term5(mc);

                _PfTokens.Add(t);

                t = GetToken(mc);
            }

            PutToken(t);
        }

        #endregion

        #region Term5

        /// <summary>
        /// Handles %, *, and / operators
        /// </summary>
        private void Term5(MatchCollection mc)
		{
            Factor(mc);

			ETokens t = GetToken(mc);

            while (t == ETokens.Mod ||
                t == ETokens.Multiply || t == ETokens.Divide)
			{
                Factor(mc);

                _PfTokens.Add(t);

				t = GetToken(mc);
			}

			PutToken(t);
        }

        #endregion

        #region Factor

        /// <summary>
        /// Handles factor processing
        /// </summary>
        private void Factor(MatchCollection mc)
		{
			ETokens t = GetToken(mc);

            int n = 0;

            // Unary operators

            while (t == ETokens.Add || t == ETokens.Subtract)
            {
                if (t == ETokens.Subtract)
                    n++;

                t = GetToken(mc);
            }

            // Operand processing

            if (t < ETokens.Operator)
            {
                if (Function(mc, t) == false)
                    _PfTokens.Add(t);
            }
            else if (t == ETokens.LParen)
            {
                Expr(mc);

                t = GetToken(mc);

                if (t != ETokens.RParen)
                    throw new Exception("Expecting right parenthesis.");
            }
            else
            {
                PutToken(t);
            }

            if (n % 2 == 1)
            {
                if (_PfTokens.Count == 0)
                    throw new Exception("Invalid expression.");

                _PfTokens.Add(ETokens.Negate);
            }
        }

        #endregion

        #region Function

	    /// <summary>
	    /// Handles function parsing
	    /// </summary>
	    /// <param name="mc"></param>
        /// <param name="e"></param>
	    /// <returns></returns>
	    private bool Function(MatchCollection mc, ETokens e)
        {
            string s = _StringPool[(int)e].ToUpper();

            ETokens t = GetFunction(mc, s);

            if (t != ETokens.BadToken)
            {
                _PfTokens.Add(t);

                t = GetToken(mc);

                if (t != ETokens.LParen)
                    throw new Exception("Expecting left parenthesis.");

                ParameterList(mc, e);

                t = GetToken(mc);

                if (t != ETokens.RParen)
                    throw new Exception("Expecting right parenthesis.");

                return (true);
            }

            return (false);
        }

        #region GetFunction

	    /// <summary>
	    /// Determines whether the parsed string
	    /// is a one of our function keywords
	    /// </summary>
	    /// <param name="mc"></param>
	    /// <param name="s"></param>
	    /// <returns></returns>
	    private ETokens GetFunction(MatchCollection mc, string s)
        {
            ETokens t = GetToken(mc);

            if (t == ETokens.LParen)
            {
                PutToken(t);

                switch (s)
                {
                    case "AVG":
                        return (ETokens.Avg);

                    case "CEILING":
                        return (ETokens.Ceiling);

                    case "FLOOR":
                        return (ETokens.Floor);

                    case "MIN":
                        return (ETokens.Min);

                    case "MAX":
                        return (ETokens.Max);

                    case "ROUND":
                        return (ETokens.Round);

                    case "SUM":
                        return (ETokens.Sum);

                    default:
                        return (ETokens.User);
                }
            }

            PutToken(t);

	        return (ETokens.BadToken);
        }

        #endregion

        #endregion

        #region ParameterList

        /// <summary>
        /// Handles function parameters
        /// </summary>
        private void ParameterList(MatchCollection mc, ETokens e)
        {
            int n = 0;

            if (_PfTokens[_PfTokens.Count - 1] == ETokens.User)
            {
                _PfTokens.Add(e);
                n++;
            }

            ETokens t = GetToken(mc);

            while (t != ETokens.RParen && t != ETokens.BadToken)
            {
                PutToken(t);

                int count = _PfTokens.Count;

                Expr(mc);

                if (_PfTokens.Count > count)
                    n++;

                t = GetToken(mc);

                if (t != ETokens.Comma)
                    break;

                t = GetToken(mc);
            }

            PutToken(t);

            _PfTokens.Add(ETokens.Function);
            _PfTokens.Add((ETokens) n);
        }

        #endregion

        #region GetToken

        /// <summary>
        /// Gets the next parsed token
        /// </summary>
        /// <returns></returns>
        private ETokens GetToken(MatchCollection mc)
		{
			ETokens t = ETokens.BadToken;

			if (_PCount > 0)
			{
				t = _PTokens[--_PCount];
			}
			else
			{
				if (_TCount < mc.Count)
				{
                    string s = mc[_TCount].Value;

					switch (s)
					{
                        case "|":
                            t = ETokens.BitwiseOr;
                            break;

                        case "^":
                            t = ETokens.BitwiseXor;
                            break;

                        case "&":
                            t = ETokens.BitwiseAnd;
                            break;

                        case "<<":
                            t = ETokens.ShiftLeft;
                            break;

                        case ">>":
                            t = ETokens.ShiftRight;
                            break;

                        case "+":
							t = ETokens.Add;
							break;

                        case "-":
                            t = ETokens.Subtract;
                            break;

                        case "*":
							t = ETokens.Multiply;
							break;

                        case "/":
							t = ETokens.Divide;
							break;

                        case "%":
                            t = ETokens.Mod;
                            break;

                        case "(":
							t = ETokens.LParen;
							break;

						case ")":
							t = ETokens.RParen;
							break;

                        case ",":
                            t = ETokens.Comma;
                            break;

						default:
                            int index = _StringPool.IndexOf(s);

                            if (index < 0)
                            {
                                _StringPool.Add(s);

                                index = _StringPool.Count - 1;
                            }

					        t = (ETokens)(index);
					        break;
					}

					_TCount++;
				}
			}

			return (t);
        }

        #endregion

        #region PutToken

        /// <summary>
        /// Saves the given token for future use
        /// </summary>
        /// <param name="t"></param>
        private void PutToken(ETokens t)
		{
			_PTokens[_PCount++] = t;
        }

        #endregion

        #endregion

        #region Evaluate

        /// <summary>
        /// Evaluates the previously tokenized code
        /// </summary>
        /// <returns></returns>
        public object Evaluate()
        {
            _UsedCells.Clear();

            if (_Cell != null)
                _UsedCells.Add(_Cell);

            return (EvaluateEx());
        }

        internal object EvaluateEx()
        {
	        Stack myStack = new Stack();

            for (int i = 0; i < _PfTokens.Count; i++)
            {
                if (_PfTokens[i] < ETokens.Operator)
                {
                    myStack.Push(_StringPool[(int)_PfTokens[i]]);
                }
                else if (_PfTokens[i] > ETokens.Functions)
                {
                    myStack.Push(_PfTokens[i]);
                }
                else
                {
                    if (_PfTokens[i] == ETokens.Negate)
                    {
                        object value = ProcessValue(myStack.Pop());

                        if (value is double == false)
                            throw new Exception("Invalid negation value");

                        myStack.Push(-(double)value);
                    }
                    else if (_PfTokens[i] == ETokens.Function)
                    {
                        if (++i == _PfTokens.Count)
                            throw new Exception("Expecting function parameter count");

                        EvalFunction(myStack, (int)_PfTokens[i]);
                    }
                    else
                    {
                        object value1 = ProcessValue(myStack.Pop());
                        object value2 = ProcessValue(myStack.Pop());

                        if (value1 == null || value2 == null)
                        {
                            if (value1 == null && value2 == null)
                                value1 = value2 = 0d;

                            else if (value1 == null)
                            {
                                if (value2 is string)
                                    value1 = "";
                                else
                                    value1 = 0d;
                            }
                            else
                            {
                                if (value1 is string)
                                    value2 = "";
                                else
                                    value2 = 0d;
                            }
                        }

                        if (value1 is string || value2 is string)
                            OpStringValue(myStack, _PfTokens[i], value1.ToString(), value2.ToString());
                        else
                            OpDoubleValue(myStack, _PfTokens[i], (double)value1, (double)value2);
                    }
                }
            }

            if (myStack.Count > 0)
                return (ProcessValue(myStack.Pop()));

            return (0);
        }

        #region OpStringValue

        private void OpStringValue(Stack myStack, ETokens op, string s1, string s2)
        {
            switch (op)
            {
                case ETokens.Add:
                    myStack.Push(s2 + s1);
                    break;

                default:
                    throw new Exception("Invalid string operation.");
            }
        }

        #endregion

        #region OpDoubleValue

        private void OpDoubleValue(
            Stack myStack, ETokens op, double d1, double d2)
        {
            switch (op)
            {
                case ETokens.BitwiseAnd:
                    myStack.Push((double)((int)d2 & (int)d1));
                    break;

                case ETokens.BitwiseOr:
                    myStack.Push((double)((int)d2 | (int)d1));
                    break;

                case ETokens.BitwiseXor:
                    myStack.Push((double)((int)d2 ^ (int)d1));
                    break;

                case ETokens.Add:
                    myStack.Push(d2 + d1);
                    break;

                case ETokens.Subtract:
                    myStack.Push(d2 - d1);
                    break;

                case ETokens.Multiply:
                    myStack.Push(d2 * d1);
                    break;

                case ETokens.Divide:
                    myStack.Push(d2 / d1);
                    break;

                case ETokens.Mod:
                    myStack.Push(d2 % d1);
                    break;

                case ETokens.ShiftLeft:
                    myStack.Push((double)((int)d2 << (int)d1));
                    break;

                case ETokens.ShiftRight:
                    myStack.Push((double)((int)d2 >> (int)d1));
                    break;
            }
        }

        #endregion

        #region EvalFunction

        /// <summary>
        /// Evaluates the current function
        /// </summary>
        /// <param name="myStack"></param>
        /// <param name="count"></param>
        private void EvalFunction(Stack myStack, int count)
        {
            object[] args = new object[count];

            for (int i = count - 1; i >= 0; i--)
                args[i] = myStack.Pop();

            switch ((ETokens)myStack.Pop())
            {
                case ETokens.Avg:
                    myStack.Push(Avg(args));
                    break;

                case ETokens.Ceiling:
                    myStack.Push(Ceiling(args));
                    break;

                case ETokens.Floor:
                    myStack.Push(Floor(args));
                    break;

                case ETokens.Min:
                    myStack.Push(Min(args));
                    break;

                case ETokens.Max:
                    myStack.Push(Max(args));
                    break;

                case ETokens.Round:
                    myStack.Push(Round(args));
                    break;

                case ETokens.Sum:
                    myStack.Push(Sum(args));
                    break;

                case ETokens.User:
                    myStack.Push(User(args));
                    break;
            }
        }

        #region Average

        /// <summary>
        /// Calculates the average of the given set of values
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private double Avg(IEnumerable<object> args)
        {
            double d = 0;
            int count = 1;

            foreach (object o in args)
            {
                object value = ProcessValue(o, ref count);

                if (value is double)
                    d += (double) value;
                else
                    throw new Exception("Can't AVERAGE non-numeric data.");
            }

            return (d / count);
        }

        #endregion

        #region Ceiling

        /// <summary>
        /// Returns the smallest whole value
        /// greater than or equal to the given value.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private double Ceiling(object[] args)
        {
            if (args.Length != 1)
                throw new Exception("Ceiling(): Invalid number of arguments.");

            object value = ProcessValue(args[0]);

            if (value is double == false)
                throw new Exception("Ceiling(): Invalid data type.");

            return (Math.Ceiling((double)value));
        }

        #endregion

        #region Floor

        /// <summary>
        /// Returns the largest whole value
        /// less than or equal to the given value.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private double Floor(object[] args)
        {
            if (args.Length != 1)
                throw new Exception("Floor(): Invalid number of arguments.");

            object value = ProcessValue(args[0]);

            if (value is double == false)
                throw new Exception("Floor(): Invalid data type.");

            return (Math.Floor((double)value));
        }

        #endregion

        #region Minimum

        /// <summary>
        /// Calculates the minimum from the given set of values
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private double Min(ICollection<object> args)
        {
            if (args.Count == 0)
                return (0);

            double d = double.PositiveInfinity;

            foreach (object o in args)
            {
                double n = ProcessMinMaxValue(o, true);

                if (n < d)
                    d = n;
            }

            return (d);
        }

        #endregion

        #region Maximum

        /// <summary>
        /// Calculates the maximum from the given set of values
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private double Max(ICollection<object> args)
        {
            if (args.Count == 0)
                return (0);

            double d = double.NegativeInfinity;

            foreach (object o in args)
            {
                double n = ProcessMinMaxValue(o, false);

                if (n > d)
                    d = n;
            }

            return (d);
        }

        #endregion

        #region Round

        /// <summary>
        /// Returns the largest whole value
        /// less than or equal to the given value.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private double Round(object[] args)
        {
            if (args.Length != 1 && args.Length != 2)
                throw new Exception("Round(): Invalid number of arguments.");

            object value = ProcessValue(args[0]);
            object decimals = args.Length == 2 ? ProcessValue(args[1]) : 0d;

            if (value is double == false)
                throw new Exception("Round(): Invalid value type.");

            if (decimals is double == false)
                throw new Exception("Round(): Invalid decimals type.");

            return (Math.Round((double)value, Convert.ToInt32(decimals)));
        }

        #endregion

        #region Sum

        /// <summary>
        /// Calculates the sum of the given set of values
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private double Sum(IEnumerable<object> args)
        {
            double d = 0;
            int count = 0;

            foreach (object o in args)
            {
                object value = ProcessValue(o, ref count);

                if (value is double)
                    d += (double)value;
                else
                    throw new Exception("Can't SUM non-numeric data.");
            }

            return (d);
        }

        #endregion

        #region User

        /// <summary>
        /// Calculates the sum of the given set of values
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object User(object[] args)
        {
            object d = null;
            int count = 0;

            object[] uargs = new object[args.Length];

            for (int i = 0; i < args.Length; i++)
                uargs[i] = ProcessValue(args[i], ref count);

            _GridPanel.SuperGrid.DoCellUserFunctionEvent(_Cell, uargs, ref d);

            return (d);
        }

        #endregion

        #region ProcessValue

        /// <summary>
        /// Process the given object value
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private object ProcessValue(object o)
        {
            int count = 0;

            return (ProcessValue(o, ref count));
        }

        private object ProcessValue(object o, ref int count)
        {
            count = 1;

            if (o is double)
                return (double)o;

            if (o is string)
            {
                string s = (string)o;

                if (s.Length > 0)
                {
                    if (s[0] == '.' || s[0] == '"' || char.IsLetter(s[0]) == true)
                    {
                        object d = null;

                        List<GridCell> cells = GetCellReferences(s);

                        if (cells != null && cells.Count > 0)
                        {
                            foreach (GridCell cell in cells)
                            {
                                object n;

                                if (ProcessCellValue(cell, out n) == true)
                                {
                                    if (d == null)
                                    {
                                        d = n;
                                    }
                                    else
                                    {
                                        if (n is string || d is string)
                                            d = d + n.ToString();
                                        else
                                            d = (double)d + (double)n;
                                    }

                                    count++;
                                }
                            }
                        }
                        else
                        {
                            return (s);
                        }

                        return (d);
                    }

                    return (GetValue(s));
                }
            }

            return (0d);
        }

        #endregion

        #region ProcessMinMaxValue

        /// <summary>
        /// Processes the given MinMax object value
        /// </summary>
        /// <param name="o"></param>
        /// <param name="min"></param>
        /// <returns></returns>
        private double ProcessMinMaxValue(object o, bool min)
        {
            if (o is double)
                return (double) o;

            if (o is string)
            {
                string s = (string)o;

                if (s.Length > 0)
                {
                    if (s[0] == '.' || s[0] == '"' || char.IsLetter(s[0]) == true)
                    {
                        double d = 0;

                        IEnumerable<GridCell> cells = GetCellReferences(s);

                        bool valueSet = false;

                        foreach (GridCell cell in cells)
                        {
                            object value;

                            if (ProcessCellValue(cell, out value) == true)
                            {
                                if (value is double == false)
                                    throw new Exception("Cannot Min/Max non-numeric values.");

                                double n = (double)value;

                                if (valueSet == false || (min ? n < d : n > d))
                                {
                                    d = n;

                                    valueSet = true;
                                }
                            }
                        }

                        return (d);
                    }
                    
                    return ((double)GetValue(s));
                }
            }

            return (0);
        }

        #endregion

        #region ProcessCellValue

        /// <summary>
        /// Processes the given cell reference value
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool ProcessCellValue(GridCell cell, out object value)
        {
            value = cell.Value ?? "";

            string s = value.ToString();

            if (string.IsNullOrEmpty(s) == false)
            {
                if (s[0] == '=')
                {
                    if (_UsedCells.Contains(cell) == true)
                        throw new Exception("Recursive cell reference");

                    _UsedCells.Add(cell);

                    EEval eval = new EEval(cell, s, _UsedCells);

                    value = eval.EvaluateEx();

                    _UsedCells.Remove(cell);

                    return (true);
                }

                value = GetValue(s);

                return (true);
            }

            return (false);
        }

        #endregion

        #endregion

        #region GetValue

        /// <summary>
        /// Gets the double value from the given string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private object GetValue(string s)
        {
            double d;

            if (double.TryParse(s, out d) == true)
                return (d);

            return (s);
        }

        #endregion

        #endregion

        #region GetCellReferences

        internal List<GridCell> GetCellReferences()
        {
            return (GetCellReferences(_Source));
        }

        private List<GridCell> GetCellReferences(string item)
        {
            const string sref1 = @"(?<col1>[\.]|[a-zA-Z_]+|""[^""]+"")\s*(?<row1>[\.]|\d+)";
            const string sref2 = @"(?<col2>[\.]|[a-zA-Z_]+|""[^""]+"")\s*(?<row2>[\.]|\d+)";

            Regex reg = new Regex(
                "(" + sref1 + ":" + sref2 + ")|" +
                "(" + sref1 + ")");

            MatchCollection mc = reg.Matches(item);

            if (mc.Count > 0)
            {
                List<GridCell> cells = new List<GridCell>();

                for (int i = 0; i < mc.Count; i++)
                    AddCellRange(_GridPanel, cells, mc[i].Groups);

                return (cells);
            }

            return (null);
        }

        #region AddCellRange

        private void AddCellRange(
            GridPanel panel, List<GridCell> cells, GroupCollection groups)
        {
            int row1;
            int col1 = GetRowCol(panel, groups["col1"].Value,
                groups["row1"].Value, out row1);

            int row2 = row1;
            int col2 = col1;

            if (groups["col2"].Success == true)
            {
                col2 = GetRowCol(panel, groups["col2"].Value,
                    groups["row2"].Value, out row2);
            }

            if (col1 >= 0 && col2 >= 0)
                ProcessRange(panel, cells, col1, row1, col2, row2);
        }

        #region GetRowCol

        private int GetRowCol(GridPanel panel,
            string scol, string srow, out int row)
        {
            scol = scol.Replace("\"", "");

            if (_Cell != null)
            {
                if (srow.Equals(".") == true)
                    row = _Cell.GridRow.RowIndex;
                else
                    int.TryParse(srow, out row);

                if (scol.Equals(".") == true)
                    return (_Cell.ColumnIndex);
            }
            else
            {
                int.TryParse(srow, out row);
            }

            int col;
            if (int.TryParse(scol, out col) == true)
                return (col);

            GridColumn column = panel.Columns[scol];

            if (column != null)
                return (column.ColumnIndex);

            return (-1);
        }

        #endregion

        #region ProcessRange

        private void ProcessRange(GridPanel panel,
            ICollection<GridCell> cells, int col1,
            int row1, int col2, int row2)
        {
            for (int i = row1; i <= row2; i++)
            {
                for (int j = col1; j <= col2; j++)
                {
                    GridCell cell = panel.GetCell(i, j);

                    if (cell != null)
                        cells.Add(cell);
                }
            }
        }

	    #endregion

        #endregion

        #endregion
    }

    #region enums

    #region ETokens

    ///<summary>
    /// ETokens
    ///</summary>
    internal enum ETokens
    {
        // Anything before this, is an operand

        Operator = 512,

        // Normal operators

        Negate,

        LParen,
        RParen,

        Multiply,
        Divide,
        Mod,

        Add,
        Subtract,

        ShiftLeft,
        ShiftRight,

        BitwiseAnd,
        BitwiseOr,
        BitwiseXor,

        Function,
        Comma,

        BadToken,

        // Anything after this is a function

        Functions,

        Avg,
        Ceiling,
        Floor,
        Max,
        Min,
        Round,
        Sum,
        User,
    };

    #endregion

    #endregion
}