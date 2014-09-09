using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace DevComponents.DotNetBar.SuperGrid
{
	/// <summary>
    /// Filter Expression evaluator
	/// </summary>
    public class FilterEval
    {
        #region Static data

        static readonly EmptyFilterOp EmptyOp = new EmptyFilterOp();

        static bool _localizedStringsLoaded;

        static private string _exprErrorString = "Expression error";
        static private string _missingParenString = "Missing parenthesis";
        static private string _missingQuoteString = "Missing quote";
        static private string _invalidArgCountString = "Invalid number of arguments";
        static private string _invalidArgString = "Invalid argument";
        static private string _invalidEmptyOpString = "Invalid 'empty' operation";
        static private string _invalidDateTimeOpString = "Invalid DateTime operation";
        static private string _invalidStringOpString = "Invalid string operation";
        static private string _invalidBoolOpString = "Invalid bool operation";
        static private string _invalidNumericOpString = "Invalid numeric operation";
        static private string _invalidEvalString = "Invalid logical evaluation";
        static private string _undefinedFunctionString = "Undefined function";

        #endregion

        #region Const data

        const string Cref = "((\\[[^\\]]*\\])|(\\[\\d+\\]))";

        const string Dref =
            "(([\\w.]+)|" +
            "(\"[^\"\\\\]*(?:\\\\.[^\"\\\\]*)*\")|" +
            "(\'([^\']*)\')|" +
            "(#([^#]*)#))";

        const string Bop = "(([()+\\-*/%,\"\\^])|(<=)|(>=)|(<>)|(=)|(<<)|(>>)|(<)|(>)|(!=)|(\\|\\|)|(&&)|(&)|(\\|))";

        const string Regs = Cref + "|" + Dref + "|" + Bop;

        #endregion

        #region Private data

        private int _TCount;
        private int _PCount;
        private FToken[] _PTokens = new FToken[10];
        private List<FToken> _PfTokens;

        private GridPanel _GridPanel;
        private GridColumn _GridColumn;
        private GridRow _GridRow;

        private string _Source;
        private List<object> _OperandPool;

        private Regex _Regex = new Regex(Regs);
        private MatchCollection _Mc;

        private object _Tag;
        private FilterMatchType _MatchType;

        private bool _Colorize;
        private bool _PostError;

        #endregion

	    ///<summary>
	    /// Expression evaluator constructor
	    ///</summary>
	    ///<param name="gridPanel">Associated GridPanel</param>
	    ///<param name="gridColumn"></param>
	    ///<param name="matchType"></param>
	    ///<param name="source"></param>
	    public FilterEval(GridPanel gridPanel,
            GridColumn gridColumn, FilterMatchType matchType, string source)
        {
            _GridPanel = gridPanel;
            _GridColumn = gridColumn;
            _MatchType = matchType;

            LoadLocalizedStrings();

            Source = source;
        }

        #region Public properties

        #region GridColumn

        ///<summary>
        /// Gets the associated Grid GridColumn, if any
        ///</summary>
        public GridColumn GridColumn
        {
            get { return (_GridColumn); }
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

                Tokenize();
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

        #region internal Properties

        internal bool PostError
        {
            get { return (_PostError); }
        }

        #endregion

        #region Tokenize code

        private void Tokenize()
        {
            if (string.IsNullOrEmpty(_Source) == false)
            {
                _TCount = 0;

                _Mc = _Regex.Matches(_Source);

                _PfTokens = new List<FToken>(_Mc.Count);
                _OperandPool = new List<object>(_Mc.Count);

                Expr();

                FToken t = GetToken();

                if (t != FToken.BadToken)
                    throw new Exception(_exprErrorString);

                foreach (FToken token in _PfTokens)
                {
                    if (token > FToken.Operator)
                        return;
                }

                throw new Exception(_exprErrorString);
            }
            
            if (_PfTokens != null)
                _PfTokens.Clear();
        }

	    #region Expr

        /// <summary>
        /// Handles conditional Or operator
        /// </summary>
        private void Expr()
        {
            Term0();

            FToken t = GetToken();

            while (t == FToken.ConditionalOr)
            {
                List<FToken> pfTokens = _PfTokens;
                _PfTokens = new List<FToken>(_Mc.Count - _TCount);

                Term0();

                pfTokens.Add(FToken.ConditionalOrRpn);
                pfTokens.Add(ProcessObject(_PfTokens));
                pfTokens.Add(t);

                _PfTokens = pfTokens;

                t = GetToken();
            }

            PutToken(t);
        }

        #endregion

        #region Term0

        /// <summary>
        /// Handles Conditional And operator
        /// </summary>
        private void Term0()
        {
            Term1();

            FToken t = GetToken();

            while (t == FToken.ConditionalAnd)
            {
                List<FToken> pfTokens = _PfTokens;
                _PfTokens = new List<FToken>(_Mc.Count - _TCount);

                Term1();

                pfTokens.Add(FToken.ConditionalAndRpn);
                pfTokens.Add(ProcessObject(_PfTokens));
                pfTokens.Add(t);

                _PfTokens = pfTokens;

                t = GetToken();
            }

            PutToken(t);
        }

        #endregion

        #region Term1

        /// <summary>
        /// Handles the Logical Or operator
        /// </summary>
        private void Term1()
        {
            Term2();

            FToken t = GetToken();

            while (t == FToken.LogicalOr)
            {
                Term2();

                _PfTokens.Add(t);

                t = GetToken();
            }

            PutToken(t);
        }

        #endregion

        #region Term2

        /// <summary>
        /// Handles Logical Xor operator
        /// </summary>
        private void Term2()
        {
            Term3();

            FToken t = GetToken();

            while (t == FToken.LogicalXor)
            {
                Term3();

                _PfTokens.Add(t);

                t = GetToken();
            }

            PutToken(t);
        }

        #endregion

        #region Term3

        /// <summary>
        /// Handles the Logical And operator
        /// </summary>
        private void Term3()
        {
            Term4();

            FToken t = GetToken();

            while (t == FToken.LogicalAnd)
            {
                Term4();

                _PfTokens.Add(t);

                t = GetToken();
            }

            PutToken(t);
        }

        #endregion

        #region Term4

        /// <summary>
        /// Handles the Comparison operators
        /// </summary>
        private void Term4()
        {
            int n = GetNegate();

            Term5();

            n += GetNegate();

            FToken t = GetToken();

            while (t == FToken.Equal || t == FToken.NotEqual || t == FToken.NotEqual2 ||
                t == FToken.GreaterThan || t == FToken.GreaterThanOrEqual ||
                t == FToken.LessThan || t == FToken.LessThanOrEqual)
            {
                n += GetNegate();

                Term5();

                if (t == FToken.Equal || t == FToken.NotEqual || t == FToken.NotEqual2)
                    SetMatchString();

                _PfTokens.Add(t);

                t = GetToken();
            }
                
            PutToken(t);

            if (n % 2 == 1)
                _PfTokens.Add(FToken.Negate);
        }

        #endregion

        #region Term5

        /// <summary>
        /// Handles Like, Is, and Between operators
        /// </summary>
        private void Term5()
        {
            Term6();

            int n = GetNegate();

            FToken t = GetToken();

            while (t == FToken.Like || t == FToken.Is || t == FToken.Between)
            {
                if (t == FToken.Is)
                {
                    n += GetNegate();

                    FToken t2 = GetToken();

                    if (t2 == FToken.Like || t2 == FToken.Between)
                        t = t2;
                    else
                        PutToken(t2);
                }

                Term6();

                if (t == FToken.Like)
                {
                    SetMatchString();
                }
                else if (t == FToken.Between)
                {
                    FToken t2 = GetToken();

                    if (t2 != FToken.ConditionalAnd && t2 != FToken.Comma)
                        PutToken(t2);

                    Term6();
                }

                _PfTokens.Add(t);

                t = GetToken();
            }

            PutToken(t);

            if (n % 2 == 1)
                PutToken(FToken.Negate);
        }

        #region SetMatchString

        private void SetMatchString()
        {
            if (_MatchType != FilterMatchType.None)
            {
                FToken lt = _PfTokens[_PfTokens.Count - 1];

                if (lt < FToken.Operator)
                {
                    string s = _OperandPool[(int)lt] as string;

                    if (s != null)
                    {
                        if (_PfTokens.Count <= 2 || _PfTokens[_PfTokens.Count - 2] != FToken.Function)
                        {
                            s = GetMatchString(s);

                            _OperandPool.Add(s);

                            _PfTokens[_PfTokens.Count - 1] =
                                (FToken)(_OperandPool.Count - 1);
                        }
                    }
                }
            }
        }

        #region GetMatchString

        private string GetMatchString(string text)
        {
            string s = text;

            switch (GridPanel.FilterMatchType)
            {
                case FilterMatchType.None:
                    if (GridPanel.FilterIgnoreMatchCase == true)
                        s = s.ToLower();
                    break;

                case FilterMatchType.RegularExpressions:
                    Regex.Match("", s);
                    break;

                case FilterMatchType.Wildcards:
                    s = Wildcard.WildcardToRegex(text);
                    Regex.Match("", s);
                    break;
            }

            return (s);
        }

        #endregion

        #endregion

        #endregion

        #region Term6

        /// <summary>
        /// Handles the Shift Left and Right operators
        /// </summary>
        private void Term6()
        {
            Term7();

            FToken t = GetToken();

            while (t == FToken.ShiftLeft || t == FToken.ShiftRight)
            {
                Term7();

                if (ReduceTerm(t) == false)
                    _PfTokens.Add(t);

                t = GetToken();
            }

            PutToken(t);
        }

        #endregion

        #region Term7

        /// <summary>
        /// Handles +, - operators
        /// </summary>
        private void Term7()
        {
            Term8();

            FToken t = GetToken();

            while (t == FToken.Add || t == FToken.Subtract)
            {
                Term8();

                if (ReduceTerm(t) == false)
                    _PfTokens.Add(t);

                t = GetToken();
            }

            PutToken(t);
        }

        #endregion

        #region Term8

        /// <summary>
        /// Handles %, *, and / operators
        /// </summary>
        private void Term8()
        {
            Factor();

            FToken t = GetToken();

            while (t == FToken.Mod ||
                t == FToken.Multiply || t == FToken.Divide)
            {
                Factor();

                if (ReduceTerm(t) == false)
                    _PfTokens.Add(t);

                t = GetToken();
            }

            PutToken(t);
        }

        #endregion

        #region Factor

        /// <summary>
        /// Handles factor processing
        /// </summary>
        private void Factor()
        {
            FToken t = GetToken();

            int n = 0;

            // Unary operators

            while (t == FToken.Add || t == FToken.Subtract)
            {
                if (t == FToken.Subtract)
                    n++;

                t = GetToken();
            }

            // Operand processing

            if (t == FToken.LParen)
            {
                Expr();

                t = GetToken();

                if (t != FToken.RParen)
                    throw new Exception(_missingParenString);
            }
            else if (t < FToken.Operator)
            {
                if (Function(t) == false)
                    _PfTokens.Add(t);
            }
            else
            {
                if (_GridColumn != null)
                {
                    FToken t2 = ProcessObject(_GridColumn);

                    _PfTokens.Add(t2);
                }

                PutToken(t);
            }

            if (n % 2 == 1)
            {
                if (_PfTokens.Count == 0)
                    throw new Exception(_exprErrorString);

                _PfTokens.Add(FToken.Negate);
            }
        }

        #endregion

        #region Function

        /// <summary>
        /// Handles function parsing
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool Function(FToken e)
        {
            string s = _OperandPool[(int)e] as string;

            if (s == null)
            {
                GridColumn col = _OperandPool[(int)e] as GridColumn;

                if (col != null)
                    s = col.Name;
            }

            if (s != null)
            {
                s = s.ToLower();

                FToken t = GetFunction(s);

                if (t != FToken.BadToken)
                {
                    _PfTokens.Add(t);

                    t = GetToken();

                    if (t != FToken.LParen)
                        throw new Exception(_missingParenString);

                    ParameterList(e);

                    t = GetToken();

                    if (t != FToken.RParen)
                        throw new Exception(_missingParenString);

                    return (true);
                }
            }

            return (false);
        }

        #region GetFunction

        /// <summary>
        /// Determines whether the parsed string
        /// is a one of our function keywords
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private FToken GetFunction(string s)
        {
            FToken t = GetToken();

            if (t == FToken.LParen)
            {
                PutToken(t);

                t = FTokenList.GetStringToken(s, FTokenList.TokenListC);

                return ((t != FToken.BadToken) ? t : FToken.User);
            }

            PutToken(t);

            return (FToken.BadToken);
        }

        #endregion

        #endregion

        #region ParameterList

        /// <summary>
        /// Handles function parameters
        /// </summary>
        private void ParameterList(FToken e)
        {
            int n = 0;

            if (_PfTokens[_PfTokens.Count - 1] == FToken.User)
            {
                _PfTokens.Add(e);
                n++;
            }

            FToken t = GetToken();

            while (t != FToken.RParen && t != FToken.BadToken)
            {
                PutToken(t);

                int count = _PfTokens.Count;

                Expr();

                if (_PfTokens.Count > count)
                    n++;

                t = GetToken();

                if (t != FToken.Comma)
                    break;

                t = GetToken();
            }

            PutToken(t);

            _PfTokens.Add(FToken.Function);
            _PfTokens.Add((FToken)n);
        }

        #endregion

        #region GetToken

        /// <summary>
        /// Gets the next parsed token
        /// </summary>
        /// <returns></returns>
        private FToken GetToken()
        {
            FToken t = FToken.BadToken;

            if (_PCount > 0)
            {
                t = _PTokens[--_PCount];
            }
            else
            {
                if (_TCount < _Mc.Count)
                {
                    string s = _Mc[_TCount].Value;

                    t = FTokenList.GetToken(s);

                    if (t == FToken.BadToken)
                        t = ProcessOperand(s);

                    _TCount++;
                }
            }

            return (t);
        }

        #endregion

        #region ProcessOperand

        private FToken ProcessOperand(string s)
        {
            object o = GetValue(s);

            return (ProcessObject(o));
        }

        #endregion

        #region ProcessObject

        private FToken ProcessObject(object o)
        {
            int index = _OperandPool.IndexOf(o);

            if (index < 0)
            {
                _OperandPool.Add(o);

                index = _OperandPool.Count - 1;
            }

            return ((FToken)(index));
        }

        #endregion

        #region GetValue

        /// <summary>
        /// Gets the 'value' from the given string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private object GetValue(string s)
        {
            s = s.Trim();

            if (s.Length >= 2)
            {
                if (IsValueMarker('"', '"', ref s, false) == true)
                    return (s);

                if (IsValueMarker('\'', '\'', ref s, false) == true)
                    return (s);

                if (IsValueMarker('#', '#', ref s, true) == true)
                    return (GetDateValue(s));

                if (IsValueMarker('[', ']', ref s, true) == true)
                    return (GetColumnValue(s));
            }

            if (s.Length > 0)
            {
                if (s.Equals("\"") == true)
                    throw new Exception(_missingQuoteString);

                GridColumn col = GetColumn(s);

                if (col != null)
                    return (col);

                string l = s.ToLower();

                if (l.Equals("true") == true)
                    return (true);

                if (l.Equals("false") == true)
                    return (false);

                if (l.Equals("null") == true)
                    return (null);
        
                if (l.Equals("empty") == true)
                    return (EmptyOp);

                double d;

                if (double.TryParse(s, out d) == true)
                    return (d);
            }

            return (s);
        }

        #region GetColumn

        private GridColumn GetColumn(string s)
        {
            s = s.ToUpper();

            foreach (GridColumn column in _GridPanel.Columns)
            {
                if (column.Name != null && column.Name.ToUpper().Equals(s))
                {
                    column.FilterExprUsesName = true;

                    return (column);
                }

                if (column.HeaderText != null && column.HeaderText.ToUpper().Equals(s))
                {
                    column.FilterExprUsesName = false;

                    return (column);
                }
            }

            return (null);
        }

	    #endregion

        #region GetColumnValue

        private GridColumn GetColumnValue(string s)
        {
            if (s.Length <= 0 && _GridColumn != null)
                return (_GridColumn);

            int index;
            if (int.TryParse(s, out index) == true)
            {
                if ((uint)index < _GridPanel.Columns.Count)
                    return (_GridPanel.Columns[index]);
            }

            return (GetColumn(s));
        }

        #endregion

        #region GetDateValue

        private object GetDateValue(string s)
        {
            if (s.Length > 0)
            {
                if (Char.IsDigit(s[0]) == true)
                    return (DateTime.Parse(s));

                return ((int)Enum.Parse(typeof(DayOfWeek), s));
            }

            return (null);
        }

        #endregion

        #region IsValueMarker

        private bool IsValueMarker(
            char c1, char c2, ref string s, bool trim)
        {
            if (s[0] == c1 && s[s.Length - 1] == c2)
            {
                s = s.Substring(1, s.Length - 2);

                if (trim == true)
                    s = s.Trim();

                return (true);
            }

            return (false);
        }

        #endregion

        #endregion

        #region PutToken

        /// <summary>
        /// Saves the given token for future use
        /// </summary>
        /// <param name="t"></param>
        private void PutToken(FToken t)
        {
            _PTokens[_PCount++] = t;
        }

        #endregion

        #region GetNegate

        private int GetNegate()
        {
            FToken t = GetToken();

            int n = 0;

            while (t == FToken.Negate)
            {
                if (t == FToken.Negate)
                    n++;

                t = GetToken();
            }

            PutToken(t);

            return (n);
        }

        #endregion

        #region ReduceTerm

        private bool ReduceTerm(FToken t)
        {
            if (_PfTokens.Count >= 2)
            {
                FToken t1 = _PfTokens[_PfTokens.Count - 1];
                FToken t2 = _PfTokens[_PfTokens.Count - 2];

                if (t1 < FToken.Operator && t2 < FToken.Operator)
                {
                    object o1 = _OperandPool[(int) t1];
                    object o2 = _OperandPool[(int) t2];

                    if (o1 != null && o2 != null && o1.GetType() == o2.GetType())
                    {
                        if (o1 is string)
                        {
                            switch (t)
                            {
                                case FToken.Add:
                                    string s = (string) o2 + (string) o1;

                                    _PfTokens.RemoveAt(_PfTokens.Count - 1);
                                    _PfTokens[_PfTokens.Count - 1] = ProcessObject(s);

                                    return (true);

                                default:
                                    return (false);
                            }
                        }

                        if (o1 is int)
                        {
                            double d1 = Convert.ToDouble(o1);
                            double d2 = Convert.ToDouble(o2);

                            double d3;

                            if (ReduceNumeric(t, d1, d2, out d3) == false)
                                return (false);

                            int n = (int) d3;

                            _PfTokens.RemoveAt(_PfTokens.Count - 1);
                            _PfTokens[_PfTokens.Count - 1] = ProcessObject(n);

                            return (true);
                        }

                        if (o1 is double)
                        {
                            double d3;

                            if (ReduceNumeric(t, (double) o1, (double) o2, out d3) == false)
                                return (false);

                            _PfTokens.RemoveAt(_PfTokens.Count - 1);
                            _PfTokens[_PfTokens.Count - 1] = ProcessObject(d3);

                            return (true);
                        }
                    }
                }
            }

            return (false);
        }

        #region ReduceNumeric

        private bool ReduceNumeric(
            FToken t, double d1, double d2, out double d3)
        {
            d3 = 0;

            switch (t)
            {
                case FToken.Add:
                    d3 = d2 + d1;
                    break;

                case FToken.Subtract:
                    d3 = d2 - d1;
                    break;

                case FToken.Divide:
                    d3 = d2 / d1;
                    break;

                case FToken.Multiply:
                    d3 = d2 * d1;
                    break;

                case FToken.ShiftLeft:
                    d3 = (int)d2 << (int)d1;
                    break;

                case FToken.ShiftRight:
                    d3 = (int)d2 >> (int)d1;
                    break;

                case FToken.Mod:
                    d3 = d2 % d1;
                    break;

                default:
                    return (false);
            }

            return (true);
        }

        #endregion

        #endregion

        #endregion

        #region Evaluate

        /// <summary>
        /// Evaluates the previously tokenized code
        /// </summary>
        /// <returns></returns>
        public bool Evaluate(GridRow row)
        {
            _GridRow = row;

            return (EvaluateEx(_PfTokens));
        }

        private bool EvaluateEx(List<FToken> pfTokens)
        {
	        Stack myStack = new Stack();

            for (int i = 0; i < pfTokens.Count; i++)
            {
                if (pfTokens[i] < FToken.Operator)
                    myStack.Push(_OperandPool[(int) pfTokens[i]]);

                else if (pfTokens[i] > FToken.Functions)
                    myStack.Push(pfTokens[i]);

                else if (pfTokens[i] == FToken.Negate)
                    EvalNegate(myStack);

                else if (pfTokens[i] == FToken.ConditionalAndRpn)
                    EvalShortCircuit(myStack, pfTokens[++i], true);

                else if (pfTokens[i] == FToken.ConditionalOrRpn)
                    EvalShortCircuit(myStack, pfTokens[++i], false);

                else if (pfTokens[i] == FToken.Function)
                    EvalFunction(myStack, (int) pfTokens[++i]);

                else
                    EvalOperator(myStack, pfTokens[i]);
            }

            return (EvalResult(myStack));
        }

        #region EvalNegate

        private void EvalNegate(Stack myStack)
        {
            if (myStack.Count < 1)
                throw new Exception(_exprErrorString);

            object value = ProcessValue(myStack.Pop());

            if (value is double)
                myStack.Push(-(double)value);

            else if (value is int)
                myStack.Push(-(int)value);

            else if (value is bool)
                myStack.Push(!(bool)value);

            else if (value == null)
                myStack.Push(0);

            else
                myStack.Push(0);
        }

        #endregion

        #region EvalShortCircuit

        private void EvalShortCircuit(
            Stack myStack, FToken token, bool evres)
        {
            bool res = (bool)myStack.Peek();

            if (res == evres)
            {
                res = EvaluateEx(
                    (List<FToken>)_OperandPool[(int)token]);
            }

            myStack.Push(res);
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

            switch ((FToken)myStack.Pop())
            {
                case FToken.AddDays:    myStack.Push(AddDays(args));    break;
                case FToken.AddHours:   myStack.Push(AddHours(args));   break;
                case FToken.AddMinutes: myStack.Push(AddMinutes(args)); break;
                case FToken.AddMonths:  myStack.Push(AddMonths(args));  break;
                case FToken.AddSeconds: myStack.Push(AddSeconds(args)); break;
                case FToken.AddYears:   myStack.Push(AddYears(args));   break;
                case FToken.Ceiling:    myStack.Push(Ceiling(args));    break;
                case FToken.Convert:    myStack.Push(ConvertTo(args));  break;
                case FToken.Date:       myStack.Push(Date(args));       break;
                case FToken.Day:        myStack.Push(Day(args));        break;
                case FToken.DayOfWeek:  myStack.Push(Dow(args));        break;
                case FToken.DayOfYear:  myStack.Push(Doy(args));        break;
                case FToken.EndOfMonth: myStack.Push(EndOfMonth(args)); break;
                case FToken.FirstOfMonth: myStack.Push(FirstOfMonth(args)); break;
                case FToken.Floor:      myStack.Push(Floor(args));      break;
                case FToken.Hour:       myStack.Push(Hour(args));       break;
                case FToken.IndexOf:    myStack.Push(IndexOf(args));    break;
                case FToken.Left:       myStack.Push(Left(args));       break;
                case FToken.Length:     myStack.Push(Length(args));     break;
                case FToken.LTrim:      myStack.Push(LTrim(args));      break;
                case FToken.Minute:     myStack.Push(Minute(args));     break;
                case FToken.Month:      myStack.Push(Month(args));      break;
                case FToken.Now:        myStack.Push(Now(args));        break;
                case FToken.Raw:        myStack.Push(Raw(args));        break;
                case FToken.Right:      myStack.Push(Right(args));      break;
                case FToken.Round:      myStack.Push(Round(args));      break;
                case FToken.RTrim:      myStack.Push(RTrim(args));      break;
                case FToken.Second:     myStack.Push(Second(args));     break;
                case FToken.Substring:  myStack.Push(Substring(args));  break;
                case FToken.TimeOfDay:  myStack.Push(TimeOfDay(args));  break;
                case FToken.ToLower:    myStack.Push(ToLower(args));    break;
                case FToken.ToString:   myStack.Push(ToString(args));   break;
                case FToken.TotalDays:  myStack.Push(TotalDays(args));  break;
                case FToken.TotalHours: myStack.Push(TotalHours(args)); break;
                case FToken.TotalMinutes: myStack.Push(TotalMinutes(args)); break;
                case FToken.TotalSeconds: myStack.Push(TotalSeconds(args)); break;
                case FToken.TotalYears: myStack.Push(TotalYears(args)); break;
                case FToken.ToUpper:    myStack.Push(ToUpper(args));    break;
                case FToken.Trim:       myStack.Push(Trim(args));       break;
                case FToken.User:       myStack.Push(User(args));       break;
                case FToken.Year:       myStack.Push(Year(args));       break;
            }
        }

        #region ConvertTo

        /// <summary>
        /// Converts the given arg to the
        /// specified .Net System data type.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object ConvertTo(object[] args)
        {
            if (args.Length < 1)
                throw new ExceptionArgCount();

            object value = ProcessValue(args.Length == 1 ? _GridColumn : args[0]);

            string s = args[args.Length - 1] as string;

            if (s == null)
                throw new ExceptionArg();

            if (value == null || value == EmptyOp)
                return (String.Empty);

            switch (s)
            {
                case "System.Boolean":
                    return (System.Convert.ToBoolean(value));

                case "System.Byte":
                    return (System.Convert.ToByte(value));

                case "System.Char":
                    return (System.Convert.ToChar(value));

                case "System.DateTime":
                    return (System.Convert.ToDateTime(value));

                case "System.Decimal":
                    return (System.Convert.ToDecimal(value));

                case "System.Double":
                    return (System.Convert.ToDouble(value));

                case "System.Int16":
                    return (System.Convert.ToInt16(value));

                case "System.Int32":
                    return (System.Convert.ToInt32(value));

                case "System.Int64":
                    return (System.Convert.ToInt64(value));

                case "System.SByte":
                    return (System.Convert.ToSByte(value));

                case "System.Single":
                    return (System.Convert.ToSingle(value));

                case "System.String":
                    return (System.Convert.ToString(value));

                case "System.UInt16":
                    return (System.Convert.ToUInt16(value));

                case "System.UInt32":
                    return (System.Convert.ToUInt32(value));

                case "System.UInt64":
                    return (System.Convert.ToUInt64(value));

                default:
                    throw new ExceptionArg();
            }
        }

        #endregion

        #region DateTime Functions

        #region AddMonths

        /// <summary>
        /// Adds Months to the given DateTime.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object AddMonths(object[] args)
        {
            if (args.Length != 2)
                throw new ExceptionArgCount();

            object value1 = ProcessValue(args[0]);
            object value2 = ProcessValue(args[1]);

            if (value1 is DateTime)
            {
                if (value2 is int)
                    return (((DateTime)value1).AddMonths((int)value2));

                if (value2 is double)
                    return (((DateTime)value1).AddMonths(Convert.ToInt32(value2)));
            }

            if (value1 == null || value1 == EmptyOp)
                return (value1);

            throw new ExceptionArg();
        }

        #endregion

        #region AddDays

        /// <summary>
        /// Adds Days to the given DateTime.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object AddDays(object[] args)
        {
            if (args.Length != 2)
                throw new ExceptionArgCount();

            object value1 = ProcessValue(args[0]);
            object value2 = ProcessValue(args[1]);

            if (value1 is DateTime)
            {
                if (value2 is int)
                    return (((DateTime)value1).AddDays((int)value2));

                if (value2 is double)
                    return (((DateTime)value1).AddDays(Convert.ToInt32(value2)));
            }

            if (value1 == null || value1 == EmptyOp)
                return (value1);

            throw new ExceptionArg();
        }

        #endregion

        #region AddHours

        /// <summary>
        /// Adds Days to the given DateTime.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object AddHours(object[] args)
        {
            if (args.Length != 2)
                throw new ExceptionArgCount();

            object value1 = ProcessValue(args[0]);
            object value2 = ProcessValue(args[1]);

            if (value1 is DateTime)
            {
                if (value2 is int)
                    return (((DateTime)value1).AddHours((int)value2));

                if (value2 is double)
                    return (((DateTime)value1).AddHours(Convert.ToInt32(value2)));
            }

            if (value1 == null || value1 == EmptyOp)
                return (value1);

            throw new ExceptionArg();
        }

        #endregion

        #region AddMinutes

        /// <summary>
        /// Adds Minutes to the given DateTime.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object AddMinutes(object[] args)
        {
            if (args.Length != 2)
                throw new ExceptionArgCount();

            object value1 = ProcessValue(args[0]);
            object value2 = ProcessValue(args[1]);

            if (value1 is DateTime)
            {
                if (value2 is int)
                    return (((DateTime)value1).AddMinutes((int)value2));

                if (value2 is double)
                    return (((DateTime)value1).AddMinutes(Convert.ToInt32(value2)));
            }

            if (value1 == null || value1 == EmptyOp)
                return (value1);

            throw new ExceptionArg();
        }

        #endregion

        #region AddYears

        /// <summary>
        /// Adds Years to the given DateTime.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object AddYears(object[] args)
        {
            if (args.Length != 2)
                throw new ExceptionArgCount();

            object value1 = ProcessValue(args[0]);
            object value2 = ProcessValue(args[1]);

            if (value1 is DateTime)
            {
                if (value2 is int)
                    return (((DateTime)value1).AddYears((int)value2));

                if (value2 is double)
                    return (((DateTime)value1).AddYears(Convert.ToInt32(value2)));
            }
            
            if (value1 == null || value1 == EmptyOp)
                return (value1);

            throw new ExceptionArg();
        }

        #endregion

        #region AddSeconds

        /// <summary>
        /// Adds Seconds to the given DateTime.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object AddSeconds(object[] args)
        {
            if (args.Length != 2)
                throw new ExceptionArgCount();

            object value1 = ProcessValue(args[0]);
            object value2 = ProcessValue(args[1]);

            if (value1 is DateTime)
            {
                if (value2 is int)
                    return (((DateTime)value1).AddSeconds((int)value2));

                if (value2 is double)
                    return (((DateTime)value1).AddSeconds(Convert.ToInt32(value2)));
            }
            if (value1 == null || value1 == EmptyOp)
                return (value1);

            throw new ExceptionArg();
        }

        #endregion

        #region Date

        /// <summary>
        /// Returns the Date portion of the given DateTime.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object Date(object[] args)
        {
            if (args.Length != 0 && args.Length != 1)
                throw new ExceptionArgCount();

            object value = (args.Length == 1)
                ? ProcessValue(args[0]) : DateTime.Now;

            if (value is DateTime)
                return (((DateTime)value).Date);

            if (value == null || value == EmptyOp)
                return (value);

            throw new ExceptionArg();
        }

        #endregion

        #region Day

        /// <summary>
        /// Returns the Day of the given DateTime.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object Day(object[] args)
        {
            if (args.Length != 0 && args.Length != 1)
                throw new ExceptionArgCount();

            object value = (args.Length == 1)
                ? ProcessValue(args[0]) : DateTime.Now;

            if (value is DateTime)
                return (((DateTime)value).Day);

            if (value == null || value == EmptyOp)
                return (value);

            throw new ExceptionArg();
        }

        #endregion

        #region Dow

        /// <summary>
        /// Returns the DayOfWeek of the given DateTime.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object Dow(object[] args)
        {
            if (args.Length != 0 && args.Length != 1)
                throw new ExceptionArgCount();

            object value = (args.Length == 1)
                ? ProcessValue(args[0]) : DateTime.Now;

            if (value is DateTime)
                return ((int)((DateTime)value).DayOfWeek + 1);

            if (value is string)
            {
                string s = (string)value;

                if (s.Length > 1)
                {
                    s = s.Substring(0, 1).ToUpper() + s.Substring(1).ToLower();

                    if (Enum.IsDefined(typeof(DayOfWeek), s))
                        return ((int)Enum.Parse(typeof(DayOfWeek), s) + 1);
                }

                return (null);
            }

            if (value is int || value is double)
                return (Enum.GetName(typeof(DayOfWeek), (double) value - 1));

            if (value == null || value == EmptyOp)
                return (value);

            throw new ExceptionArg();
        }

        #endregion

        #region Doy

        /// <summary>
        /// Returns the DayOfYear of the given DateTime.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object Doy(object[] args)
        {
            if (args.Length != 0 && args.Length != 1)
                throw new ExceptionArgCount();

            object value = (args.Length == 1)
                ? ProcessValue(args[0]) : DateTime.Now;

            if (value is DateTime)
                return (((DateTime)value).DayOfYear);

            if (value == null || value == EmptyOp)
                return (value);

            throw new ExceptionArg();
        }

        #endregion

        #region EndOfMonth

        /// <summary>
        /// Returns the EndOfMonth of the given DateTime.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object EndOfMonth(object[] args)
        {
            if (args.Length != 0 && args.Length != 1)
                throw new ExceptionArgCount();

            object value = (args.Length == 1)
                ? ProcessValue(args[0]) : DateTime.Now;

            if (value is DateTime)
            {
                DateTime dt = (DateTime)value;

                dt = new DateTime(dt.Year, dt.Month, 1);

                return (dt.AddMonths(1).AddDays(-dt.Day));
            }

            if (value == null || value == EmptyOp)
                return (value);

            throw new ExceptionArg();
        }

        #endregion

        #region FirstOfMonth

        /// <summary>
        /// Returns the FirstOfMonth of the given DateTime.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object FirstOfMonth(object[] args)
        {
            if (args.Length != 0 && args.Length != 1)
                throw new ExceptionArgCount();

            object value = (args.Length == 1)
                ? ProcessValue(args[0]) : DateTime.Now;

            if (value is DateTime)
            {
                DateTime dt = (DateTime) value;

                return (dt.AddDays(1 - dt.Day));
            }

            if (value == null || value == EmptyOp)
                return (value);

            throw new ExceptionArg();
        }

        #endregion

        #region Hour

        /// <summary>
        /// Returns the Hour of the given DateTime.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object Hour(object[] args)
        {
            if (args.Length != 0 && args.Length != 1)
                throw new ExceptionArgCount();

            object value = (args.Length == 1)
                ? ProcessValue(args[0]) : DateTime.Now;

            if (value is DateTime)
                return (((DateTime)value).Hour);

            if (value == null || value == EmptyOp)
                return (value);

            throw new ExceptionArg();
        }

        #endregion

        #region Minute

        /// <summary>
        /// Returns the Minute of the given DateTime.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object Minute(object[] args)
        {
            if (args.Length != 0 && args.Length != 1)
                throw new ExceptionArgCount();

            object value = (args.Length == 1)
                ? ProcessValue(args[0]) : DateTime.Now;

            if (value is DateTime)
                return (((DateTime)value).Minute);

            if (value == null || value == EmptyOp)
                return (value);

            throw new ExceptionArg();
        }

        #endregion

        #region Month

        /// <summary>
        /// Returns the Month of the given DateTime.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object Month(object[] args)
        {
            if (args.Length != 0 && args.Length != 1)
                throw new ExceptionArgCount();

            object value = (args.Length == 1)
                ? ProcessValue(args[0]) : DateTime.Now;

            if (value is DateTime)
                return (((DateTime)value).Month);

            if (value == null || value == EmptyOp)
                return (value);

            throw new ExceptionArg();
        }

        #endregion

        #region Now

        /// <summary>
        /// Returns the current DateTime
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static private object Now(object[] args)
        {
            if (args.Length != 0)
                throw new ExceptionArgCount();

            return (DateTime.Now);
        }

        #endregion

        #region Year

        /// <summary>
        /// Returns the Year of the given DateTime.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object Year(object[] args)
        {
            if (args.Length != 0 && args.Length != 1)
                throw new ExceptionArgCount();

            object value = (args.Length == 1)
                ? ProcessValue(args[0]) : DateTime.Now;

            if (value is DateTime)
                return (((DateTime)value).Year);

            if (value == null || value == EmptyOp)
                return (value);

            throw new ExceptionArg();
        }

        #endregion

        #region Second

        /// <summary>
        /// Returns the Minute of the given DateTime.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object Second(object[] args)
        {
            if (args.Length != 0 && args.Length != 1)
                throw new ExceptionArgCount();

            object value = (args.Length == 1)
                ? ProcessValue(args[0]) : DateTime.Now;

            if (value is DateTime)
                return (((DateTime)value).Second);

            if (value == null || value == EmptyOp)
                return (value);

            throw new ExceptionArg();
        }

        #endregion

        #region TimeOfDay

        /// <summary>
        /// Returns the TimeOfDay of the given DateTime.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object TimeOfDay(object[] args)
        {
            if (args.Length != 0 && args.Length != 1)
                throw new ExceptionArgCount();

            object value = (args.Length == 1)
                ? ProcessValue(args[0]) : DateTime.Now;

            if (value is DateTime)
                return (((DateTime)value).TimeOfDay);

            if (value == null || value == EmptyOp)
                return (value);

            throw new ExceptionArg();
        }

        #endregion

        #region TotalDays

        /// <summary>
        /// Returns the TotalDays of the given TimeSpan.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object TotalDays(object[] args)
        {
            if (args.Length != 1)
                throw new ExceptionArgCount();

            object value = ProcessValue(args[0]);

            if (value is TimeSpan)
                return ((TimeSpan)value).TotalDays;

            if (value == null || value == EmptyOp)
                return (value);

            throw new ExceptionArg();
        }

        #endregion

        #region TotalHours

        /// <summary>
        /// Returns the TotalHours of the given TimeSpan.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object TotalHours(object[] args)
        {
            if (args.Length != 1)
                throw new ExceptionArgCount();

            object value = ProcessValue(args[0]);

            if (value is TimeSpan)
                return ((TimeSpan)value).TotalHours;

            if (value == null || value == EmptyOp)
                return (value);

            throw new ExceptionArg();
        }

        #endregion

        #region TotalMinutes

        /// <summary>
        /// Returns the TotalMinutes of the given TimeSpan.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object TotalMinutes(object[] args)
        {
            if (args.Length != 1)
                throw new ExceptionArgCount();

            object value = ProcessValue(args[0]);

            if (value is TimeSpan)
                return ((TimeSpan)value).TotalMinutes;

            if (value == null || value == EmptyOp)
                return (value);

            throw new ExceptionArg();
        }

        #endregion

        #region TotalSeconds

        /// <summary>
        /// Returns the TotalSeconds of the given TimeSpan.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object TotalSeconds(object[] args)
        {
            if (args.Length != 1)
                throw new ExceptionArgCount();

            object value = ProcessValue(args[0]);

            if (value is TimeSpan)
                return ((TimeSpan)value).TotalSeconds;

            if (value == null || value == EmptyOp)
                return (value);

            throw new ExceptionArg();
        }

        #endregion

        #region TotalYears

        /// <summary>
        /// Returns the TotalDays of the given TimeSpan.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object TotalYears(object[] args)
        {
            if (args.Length != 1)
                throw new ExceptionArgCount();

            object value = ProcessValue(args[0]);

            if (value is TimeSpan)
                return ((TimeSpan)value).TotalDays / 365;

            if (value == null || value == EmptyOp)
                return (value);

            throw new ExceptionArg();
        }

        #endregion

        #endregion

        #region Numeric Functions

        #region Ceiling

        /// <summary>
        /// Returns the smallest whole value
        /// greater than or equal to the given value.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object Ceiling(object[] args)
        {
            if (args.Length > 1)
                throw new ExceptionArgCount();

            object value = ProcessValue(args.Length == 0 ? _GridColumn : args[0]);

            if (value is double)
                return (Math.Ceiling((double)value));

            if (value == null || value == EmptyOp)
                return (value);

            throw new ExceptionArg();
        }

        #endregion

        #region Floor

        /// <summary>
        /// Returns the largest whole value
        /// less than or equal to the given value.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object Floor(object[] args)
        {
            if (args.Length > 1)
                throw new ExceptionArgCount();

            object value = ProcessValue(args.Length == 0 ? _GridColumn : args[0]);

            if (value is double)
                return (Math.Floor((double)value));

            if (value == null || value == EmptyOp)
                return (value);

            throw new ExceptionArg();
        }

        #endregion

        #region Round

        /// <summary>
        /// Returns the largest whole value
        /// less than or equal to the given value.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object Round(object[] args)
        {
            if (args.Length > 2)
                throw new ExceptionArgCount();

            object value = ProcessValue(args.Length == 0 ? _GridColumn : args[0]);
            object decimals = args.Length > 1 ? ProcessValue(args[args.Length - 1]) : 0d;

            if (value is double && decimals is double)
                return (Math.Round((double)value, Convert.ToInt32(decimals)));

            if (value == null || value == EmptyOp)
                return (value);

            throw new ExceptionArg();
        }

        #endregion

        #endregion

        #region String Functions

        #region IndexOf

        /// <summary>
        /// Returns the index of the given
        /// string within the alternate specified string.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>-1 if not found</returns>
        private object IndexOf(object[] args)
        {
            if (args.Length < 2 || args.Length > 4)
                throw new ExceptionArgCount();

            object o1 = ProcessValue(args[0]);
            object o2 = ProcessValue(args[1]);

            if (o1 is string && o2 is string)
            {
                string subText = (string)o1;
                string text = (string)o2;

                int start = 0;
                int len = text.Length;

                if (args.Length > 2)
                {
                    object o3 = ProcessValue(args[2]);

                    if (o3 is double == false && o3 is int == false)
                        throw new ExceptionArg();

                    start = Convert.ToInt32(o3);

                    if (args.Length > 3)
                    {
                        object o4 = ProcessValue(args[2]);

                        if (o4 is double == false && o4 is int == false)
                            throw new ExceptionArg();

                        len = Math.Min(len, Convert.ToInt32(o4));
                    }
                }

                if ((uint)start + len > text.Length)
                    return (-1);

                if (len < subText.Length)
                    return (-1);

                return (text.IndexOf(subText, start, len));
            }

            if (o1 == null || o1 == EmptyOp)
                return (-1);

            if (o2 == null || o2 == EmptyOp)
                return (-1);

            throw new ExceptionArg();
        }

        #endregion

        #region Left

        /// <summary>
        /// Returns the left part of the given character
        /// string with the specified number of characters.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object Left(object[] args)
        {
            if (args.Length != 2)
                throw new ExceptionArgCount();

            object text = ProcessValue(args[0]);

            if (text is string)
            {
                string s = (string)text;

                object len = ProcessValue(args[1]);

                if (len is double == false && len is int == false)
                    throw new ExceptionArg();

                int n = Convert.ToInt32(len);

                if (n > s.Length)
                    n = s.Length;

                return (s.Substring(0, n));
            }

            if (text == null || text == EmptyOp)
                return (text);

            throw new ExceptionArg();
        }

        #endregion

        #region Length

        /// <summary>
        /// Returns the length the given string.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object Length(object[] args)
        {
            if (args.Length > 1)
                throw new ExceptionArgCount();

            object value = ProcessValue(args.Length == 0 ? _GridColumn : args[0]);

            if (value is string)
                return ((string)value).Length;

            if (value == null || value == EmptyOp)
                return (value);

            throw new ExceptionArg();
        }

        #endregion

        #region LTrim

        /// <summary>
        /// Removes all leading
        /// whitespace characters from the given string.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object LTrim(object[] args)
        {
            if (args.Length > 1)
                throw new ExceptionArgCount();

            object value = ProcessValue(args.Length == 0 ? _GridColumn : args[0]);

            if (value is string)
                return ((string)value).TrimStart();

            if (value == null || value == EmptyOp)
                return (value);

            throw new ExceptionArg();
        }

        #endregion

        #region Raw

        /// <summary>
        /// Returns the raw unevaluated cell text (if the cell
        /// contains an expression '=123', it is not evaluated as such).
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object Raw(object[] args)
        {
            if (args.Length > 1)
                throw new ExceptionArgCount();

            return (args.Length == 0 ? _GridColumn : ProcessRawValue(args[0]));
        }

        #endregion

        #region Right

        /// <summary>
        /// Returns the right part of the given character
        /// string with the specified number of characters.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object Right(object[] args)
        {
            if (args.Length != 2)
                throw new ExceptionArgCount();

            object text = ProcessValue(args[0]);

            if (text is string)
            {
                string s = (string)text;

                object len = ProcessValue(args[1]);

                if (len is double == false && len is int == false)
                    throw new ExceptionArg();

                int n = Convert.ToInt32(len);

                if (n > s.Length)
                    n = s.Length;

                return (s.Substring(s.Length - n, n));
            }

            if (text == null || text == EmptyOp)
                return (text);

            throw new ExceptionArg();
        }

        #endregion

        #region RTrim

        /// <summary>
        /// Removes all trailing
        /// whitespace characters from the given string.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object RTrim(object[] args)
        {
            if (args.Length > 1)
                throw new ExceptionArgCount();

            object value = ProcessValue(args.Length == 0 ? _GridColumn : args[0]);

            if (value is string)
                return ((string)value).TrimEnd();

            if (value == null || value == EmptyOp)
                return (value);

            throw new ExceptionArg();
        }

        #endregion

        #region Substring

        /// <summary>
        /// Returns a substring of the given string.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object Substring(object[] args)
        {
            if (args.Length != 2 && args.Length != 3)
                throw new ExceptionArgCount();

            object text = ProcessValue(args[0]);

            if (text is string)
            {
                string s = (string)text;

                object index = ProcessValue(args[1]);

                if (index is double == false)
                    throw new ExceptionArg();

                int n = Convert.ToInt32(index);

                if (args.Length == 3)
                {
                    object length = ProcessValue(args[2]);

                    if (length is double == false)
                        throw new ExceptionArg();

                    return (s.Substring(n, Convert.ToInt32(length)));
                }

                return (s.Substring(n));
            }

            if (text == null || text == EmptyOp)
                return (text);

            throw new ExceptionArg();
        }

        #endregion

        #region ToLower

        /// <summary>
        /// Returns a lower cased copy of the given string
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object ToLower(object[] args)
        {
            if (args.Length > 1)
                throw new ExceptionArgCount();

            object value = ProcessValue(args.Length == 0 ? _GridColumn : args[0]);

            if (value is string)
                return ((string)value).ToLower();

            if (value == null || value == EmptyOp)
                return (value);

            throw new ExceptionArg();
        }

        #endregion

        #region ToString

        /// <summary>
        /// Returns the ToString of the given object.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object ToString(object[] args)
        {
            if (args.Length > 1)
                throw new ExceptionArgCount();

            object value = ProcessValue(args.Length == 0 ? _GridColumn : args[0]);

            if (value == null || value == EmptyOp)
                return (value);

            return (value.ToString());
        }

        #endregion

        #region ToUpper

        /// <summary>
        /// Returns an upper cased copy of the given string
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object ToUpper(object[] args)
        {
            if (args.Length > 1)
                throw new ExceptionArgCount();

            object value = ProcessValue(args.Length == 0 ? _GridColumn : args[0]);

            if (value is string)
                return ((string)value).ToUpper();

            if (value == null || value == EmptyOp)
                return (value);

            throw new ExceptionArg();
        }

        #endregion

        #region Trim

        /// <summary>
        /// Removes all leading and trailing
        /// whitespace characters from the given string.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object Trim(object[] args)
        {
            if (args.Length > 1)
                throw new ExceptionArgCount();

            object value = ProcessValue(args.Length == 0 ? _GridColumn : args[0]);

            if (value is string)
                return ((string)value).Trim();

            if (value == null || value == EmptyOp)
                return (value);

            throw new ExceptionArg();
        }

        #endregion

        #endregion

        #region User

        /// <summary>
        /// Calculates the sum of the given set of values
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object User(object[] args)
        {
            object o = null;

            object[] uargs = new object[args.Length];

            for (int i = 0; i < args.Length; i++)
                uargs[i] = ProcessValue(args[i]);

            if (_GridPanel.SuperGrid.DoFilterUserFunctionEvent(
                _GridRow, uargs, ref o) == false)
            {
                string s = uargs[0] + " - " + _undefinedFunctionString;

                throw new Exception(s);
            }

            return (o);
        }

        #endregion

        #endregion

        #region EvalOperator

        private void EvalOperator(Stack myStack, FToken token)
        {
            if (myStack.Count < 2)
                throw new Exception(_exprErrorString);

            _PostError = true;

            object value1 = ProcessValue(myStack.Pop());
            object value2 = ProcessValue(myStack.Pop());

            if (value1 == EmptyOp || value2 == EmptyOp)
                OpEmptyValue(myStack, token, value1, value2);

            else if (value1 is bool || value2 is bool)
                OpBoolValue(myStack, token, value1, value2);

            else if (value1 is DateTime || value2 is DateTime)
                OpDateTimeValue(myStack, token, value1, value2);

            else if (value1 is string || value2 is string)
                OpStringValue(myStack, token, value1, value2);

            else
                OpDoubleValue(myStack, token, value1, value2);
        }

        #region ProcessValue

        /// <summary>
        /// Process the given object value
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private object ProcessValue(object o)
        {
            GridColumn col = o as GridColumn;

            if (col != null)
            {
                if (col.ColumnIndex < _GridRow.Cells.Count)
                {
                    object value = _GridRow.Cells[col.ColumnIndex].Value;

                    if (_GridRow.Cells[col.ColumnIndex].IsValueExpression == true)
                        value = _GridRow.Cells[col.ColumnIndex].GetExpValue((string)value);

                    return (value is DBNull ? null : value);
                }

                return (EmptyOp);
            }

            return (o);
        }

        #endregion

        #region ProcessRawValue

        /// <summary>
        /// Process the given object value
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private object ProcessRawValue(object o)
        {
            GridColumn col = o as GridColumn;

            if (col != null)
            {
                if (col.ColumnIndex < _GridRow.Cells.Count)
                {
                    object value = _GridRow.Cells[col.ColumnIndex].Value;

                    return (value is DBNull ? null : value);
                }

                return (EmptyOp);
            }

            return (o);
        }

        #endregion

        #region OpEmptyValue

        private void OpEmptyValue(
            Stack myStack, FToken op, object o1, object o2)
        {
            switch (op)
            {
                case FToken.Is:
                case FToken.Like:
                case FToken.Equal:
                case FToken.LessThanOrEqual:
                case FToken.GreaterThanOrEqual:
                    myStack.Push((o2 == EmptyOp && o1 == EmptyOp));
                    break;

                case FToken.NotEqual:
                case FToken.NotEqual2:
                    myStack.Push(o2 != o1);
                    break;

                case FToken.LessThan:
                    myStack.Push(o2 == o1 ? false : o2 == EmptyOp);
                    break;
                
                case FToken.GreaterThan:
                    myStack.Push(o2 == o1 ? false : o2 != EmptyOp);
                    break;

                case FToken.LogicalAnd:
                case FToken.LogicalXor:
                case FToken.Add:
                case FToken.Subtract:
                case FToken.Multiply:
                case FToken.Divide:
                case FToken.Mod:
                case FToken.ShiftLeft:
                case FToken.ShiftRight:
                    myStack.Push(EmptyOp);
                    break;

                default:
                    throw new Exception(_invalidEmptyOpString);
            }
        }

        #endregion

        #region OpDateTimeValue

        private void OpDateTimeValue(
            Stack myStack, FToken op, object o1, object o2)
        {
            if (op == FToken.Add)
            {
                OpDateTimeValueAdd(myStack, o1, o2);
            }
            else if (op == FToken.Subtract)
            {
                OpDateTimeValueSubtract(myStack, o1, o2);
            }
            else if ((o1 is DateTime || o1 == null) && (o2 is DateTime || o2 == null))
            {
                DateTime d1 = (DateTime)(o1 ?? DateTime.MinValue);
                DateTime d2 = (DateTime)(o2 ?? DateTime.MinValue);

                switch (op)
                {
                    case FToken.Is:
                    case FToken.Equal:
                        myStack.Push(d2.Equals(d1));
                        break;

                    case FToken.NotEqual:
                    case FToken.NotEqual2:
                        myStack.Push(d2.Equals(d1) == false);
                        break;

                    case FToken.LessThan:
                        myStack.Push(d2.CompareTo(d1) < 0);
                        break;

                    case FToken.LessThanOrEqual:
                        myStack.Push(d2.CompareTo(d1) <= 0);
                        break;

                    case FToken.GreaterThan:
                        myStack.Push(d2.CompareTo(d1) > 0);
                        break;

                    case FToken.GreaterThanOrEqual:
                        myStack.Push(d2.CompareTo(d1) >= 0);
                        break;

                    case FToken.Between:
                        object value = ProcessValue(myStack.Pop());

                        if (value == null || value == EmptyOp)
                        {
                            myStack.Push(false);
                        }
                        else
                        {
                            if (value is DateTime == false)
                                throw new Exception(_invalidDateTimeOpString);

                            DateTime d3 = (DateTime)value;

                            myStack.Push(d3 >= d2 && d3 <= d1);
                        }
                        break;

                    default:
                        throw new Exception(_invalidDateTimeOpString);
                }
            }
            else
            {
                throw new Exception(_invalidDateTimeOpString);
            }
        }

        #region OpDateTimeValueAdd

        private void OpDateTimeValueAdd(Stack myStack, object o1, object o2)
        {
            if (o2 is DateTime)
            {
                DateTime d2 = (DateTime)o2;

                if (o1 is TimeSpan)
                    myStack.Push(d2.Add((TimeSpan)o1));

                else if (o1 is double)
                    myStack.Push(d2.AddDays((double)o1));

                else if (o1 == null)
                    myStack.Push(d2);

                else
                    throw new Exception(_invalidDateTimeOpString);
            }
            else
            {
                throw new Exception(_invalidDateTimeOpString);
            }
        }

        #endregion

        #region OpDateTimeValueSubtract

        private void OpDateTimeValueSubtract(Stack myStack, object o1, object o2)
        {
            if (o2 is DateTime)
            {
                DateTime d2 = (DateTime)o2;

                if (o1 is TimeSpan)
                    myStack.Push(d2.Add(-(TimeSpan)o1));

                else if (o1 is DateTime)
                    myStack.Push(d2 - (DateTime)o1);

                else if (o1 is double)
                    myStack.Push(d2.AddDays(-(double)o1));

                else if (o1 == null)
                    myStack.Push(new TimeSpan(0));

                else
                    throw new Exception(_invalidDateTimeOpString);
            }
            else
            {
                throw new Exception(_invalidDateTimeOpString);
            }
        }

        #endregion

        #endregion

        #region OpStringValue

        private void OpStringValue(
            Stack myStack, FToken op, object o1, object o2)
        {
            string s1 = (o1 != null ? o1.ToString() : "");
            string s2 = (o2 != null ? o2.ToString() : "");

            switch (op)
            {
                case FToken.Add:
                    myStack.Push(s2 + s1);
                    break;

                case FToken.Is:
                case FToken.Equal:
                    if (_MatchType == FilterMatchType.None)
                    {
                        if (GridPanel.FilterIgnoreMatchCase == true)
                        {
                            s1 = s1.ToLower();
                            s2 = s2.ToLower();
                        }

                        myStack.Push(s2.Equals(s1));
                    }
                    else
                    {
                        if (o2 == null)
                        {
                            myStack.Push(o1 == null);
                        }
                        else
                        {
                            _PostError = false;

                            myStack.Push(Regex.IsMatch(s2, s1,
                                 GridPanel.FilterIgnoreMatchCase ? RegexOptions.IgnoreCase : RegexOptions.None));
                        }
                    }
                    break;

                case FToken.NotEqual:
                case FToken.NotEqual2:
                    if (_MatchType == FilterMatchType.None)
                    {
                        if (GridPanel.FilterIgnoreMatchCase == true)
                        {
                            s1 = s1.ToLower();
                            s2 = s2.ToLower();
                        }

                        myStack.Push(s2.Equals(s1) == false);
                    }
                    else
                    {
                        if (o2 == null)
                        {
                            myStack.Push(o1 != null);
                        }
                        else
                        {
                            _PostError = false;

                            myStack.Push(Regex.IsMatch(s2, s1,
                                GridPanel.FilterIgnoreMatchCase ? RegexOptions.IgnoreCase : RegexOptions.None) == false);
                        }
                    }
                    break;

                case FToken.LessThan:
                    myStack.Push(s2.CompareTo(s1) < 0);
                    break;

                case FToken.LessThanOrEqual:
                    myStack.Push(s2.CompareTo(s1) <= 0);
                    break;

                case FToken.GreaterThan:
                    myStack.Push(s2.CompareTo(s1) > 0);
                    break;

                case FToken.GreaterThanOrEqual:
                    myStack.Push(s2.CompareTo(s1) >= 0);
                    break;

                case FToken.Like:
                    if (_MatchType == FilterMatchType.None)
                    {
                        if (GridPanel.FilterIgnoreMatchCase == true)
                        {
                            s1 = s1.ToLower();
                            s2 = s2.ToLower();
                        }

                        myStack.Push(s2.StartsWith(s1));
                    }
                    else
                    {
                        _PostError = false;

                        myStack.Push(Regex.IsMatch(s2, s1,
                            GridPanel.FilterIgnoreMatchCase ? RegexOptions.IgnoreCase : RegexOptions.None));
                    }
                    break;

                case FToken.Between:
                    object value = ProcessValue(myStack.Pop());

                    if (value == null || value == EmptyOp)
                    {
                        myStack.Push(false);
                    }
                    else
                    {
                        string s3 = (string)(value);

                        myStack.Push(s3.CompareTo(s2) >= 0 && s3.CompareTo(s1) <= 0);
                    }
                    break;

                default:
                    throw new Exception(_invalidStringOpString);
            }
        }

	    #endregion

        #region OpBoolValue

        private void OpBoolValue(
            Stack myStack, FToken op, object o1, object o2)
        {
            int b1 = GetBoolInt(o1);
            int b2 = GetBoolInt(o2);

            switch (op)
            {
                case FToken.LogicalAnd:
                    myStack.Push((b2 == 1) && (b1 == 1));
                    break;

                case FToken.LogicalOr:
                    myStack.Push((b2 == 1) || (b1 == 1));
                    break;

                case FToken.ConditionalAnd:
                    myStack.Push((b2 == 1) && (b1 == 1));
                    break;

                case FToken.ConditionalOr:
                    myStack.Push((b2 == 1) || (b1 == 1));
                    break;

                case FToken.LogicalXor:
                    myStack.Push((b2 == 1) ^ (b1 == 1));
                    break;

                case FToken.Is:
                case FToken.Equal:
                case FToken.Like:
                    myStack.Push(b2 == b1);
                    break;

                case FToken.NotEqual:
                case FToken.NotEqual2:
                    myStack.Push(b2 != b1);
                    break;

                default:
                    throw new Exception(_invalidBoolOpString);
            }
        }

	    #region GetBoolInt

        private int GetBoolInt(object o)
        {
            if (o == null)
                return (-1);

            string s = o as string;

            if (s != null)
            {
                s = s.ToLower().Trim();

                switch (s)
                {
                    case "y":
                    case "yes":
                    case "t":
                    case "true":
                    case "on":
                    case "1":
                        return (1);

                    case "n":
                    case "no":
                    case "f":
                    case "false":
                    case "off":
                    case "0":
                        return (0);

                    case "":
                    case "null":
                        return (-1);
                }
            }

            return (Convert.ToBoolean(o) ? 1 : 0);
        }

        #endregion

        #endregion

        #region OpDoubleValue

        private void OpDoubleValue(
            Stack myStack, FToken op, object o1, object o2)
        {
            if (o1 == null && o2 == null)
            {
                switch (op)
                {
                    case FToken.LogicalAnd:
                    case FToken.LogicalXor:
                    case FToken.Add:
                    case FToken.Subtract:
                    case FToken.Multiply:
                    case FToken.Divide:
                    case FToken.Mod:
                    case FToken.ShiftLeft:
                    case FToken.ShiftRight:
                        myStack.Push(0d);
                        break;

                    case FToken.Is:
                    case FToken.Equal:
                    case FToken.LessThanOrEqual:
                    case FToken.GreaterThanOrEqual:
                    case FToken.Like:
                        myStack.Push(true);
                        break;

                    case FToken.NotEqual:
                    case FToken.NotEqual2:
                    case FToken.LessThan:
                    case FToken.GreaterThan:
                    case FToken.ConditionalAnd:
                    case FToken.ConditionalOr:
                        myStack.Push(false);
                        break;

                    default:
                        throw new Exception(_invalidNumericOpString);
                }
            }
            else
            {
                double d1 = Convert.ToDouble(o1);
                double d2 = Convert.ToDouble(o2);

                switch (op)
                {
                    case FToken.LogicalAnd:
                        myStack.Push((double)((int)d2 & (int)d1));
                        break;

                    case FToken.LogicalOr:
                        myStack.Push((double)((int)d2 | (int)d1));
                        break;

                    case FToken.LogicalXor:
                        myStack.Push((double)((int)d2 ^ (int)d1));
                        break;

                    case FToken.Add:
                        myStack.Push(d2 + d1);
                        break;

                    case FToken.Subtract:
                        myStack.Push(d2 - d1);
                        break;

                    case FToken.Multiply:
                        myStack.Push(d2 * d1);
                        break;

                    case FToken.Divide:
                        myStack.Push(d2 / d1);
                        break;

                    case FToken.Mod:
                        myStack.Push(d2 % d1);
                        break;

                    case FToken.ShiftLeft:
                        myStack.Push((double)((int)d2 << (int)d1));
                        break;

                    case FToken.ShiftRight:
                        myStack.Push((double)((int)d2 >> (int)d1));
                        break;

                    case FToken.Equal:
                        myStack.Push(o1 == null | o2 == null ? false : d1 == d2);
                        break;

                    case FToken.Is:
                    case FToken.Like:
                        myStack.Push(d2 == d1);
                        break;

                    case FToken.NotEqual:
                    case FToken.NotEqual2:
                        myStack.Push(o1 == null | o2 == null ? true : d1 != d2);
                        break;

                    case FToken.LessThan:
                        myStack.Push(d2 < d1);
                        break;

                    case FToken.LessThanOrEqual:
                        myStack.Push(d2 <= d1);
                        break;
                    case FToken.GreaterThan:
                        myStack.Push(d2 > d1);
                        break;

                    case FToken.GreaterThanOrEqual:
                        myStack.Push(d2 >= d1);
                        break;

                    case FToken.Between:
                        object value = ProcessValue(myStack.Pop());

                        if (value == null || value == EmptyOp)
                        {
                            myStack.Push(false);
                        }
                        else
                        {
                            double d3 = Convert.ToDouble(value);

                            myStack.Push(d3 >= d2 && d3 <= d1);
                        }
                        break;

                    default:
                        throw new Exception(_invalidNumericOpString);
                }
            }
        }

        #endregion

        #endregion

        #region EvalResult

        private bool EvalResult(Stack myStack)
        {
            if (myStack.Count > 0)
            {
                object o = ProcessValue(myStack.Pop());

                if (o is bool)
                    return ((bool)o);

                throw new Exception(_invalidEvalString);
            }

            return (false);
        }

        #endregion

        #endregion

        #region GetInfix code

        /// <summary>
        /// GetInfix
        /// </summary>
        /// <returns></returns>
        public string GetInfix(bool colorize)
        {
            if (_PfTokens != null)
            {
                _Colorize = colorize;

                string s = GetInfixEx(_PfTokens);

                if (s.Length > 2)
                {
                    IsValueMarker('(', ')', ref s, false);

                    return (s);
                }
            }

            return ("");
        }

	    private string GetInfixEx(List<FToken> pfTokens)
        {
            if (pfTokens.Count > 0)
            {
                Stack myStack = new Stack();

                for (int i = 0; i < pfTokens.Count; i++)
                {
                    if (pfTokens[i] < FToken.Operator)
                    {
                        object o = _OperandPool[(int)pfTokens[i]];

                        myStack.Push((o is string) ? "\"" + o + "\"" : o);
                    }
                    else if (pfTokens[i] > FToken.Functions)
                        myStack.Push(pfTokens[i]);

                    else if (pfTokens[i] == FToken.Negate)
                        InfixNegate(myStack);

                    else if (pfTokens[i] == FToken.ConditionalAndRpn)
                        InfixShortCircuit(myStack, pfTokens[++i]);

                    else if (pfTokens[i] == FToken.ConditionalOrRpn)
                        InfixShortCircuit(myStack, pfTokens[++i]);

                    else if (pfTokens[i] == FToken.Function)
                        InfixFunction(myStack, (int)pfTokens[++i]);

                    else
                        InfixOperator(myStack, pfTokens[i]);
                }

                return (InfixValue(myStack.Pop()));
            }

            return ("");
        }

        #region InfixNegate

        private void InfixNegate(Stack myStack)
        {
            if (myStack.Count < 1)
                throw new Exception(_exprErrorString);

            object o = myStack.Pop();

            if (o is int || o is double)
            {
                myStack.Push("-" + InfixValue(o));
            }
            else
            {
                myStack.Push(o);

                myStack.Push("(" +
                     ColorPart("not ", ExprColorPart.Operator) +
                     InfixValue(myStack.Pop()) + ")");
            }
        }

        #endregion

        #region InfixShortCircuit

        private void InfixShortCircuit(
            Stack myStack, FToken token)
        {
            string s = GetInfixEx((List<FToken>)_OperandPool[(int)token]);

            myStack.Push(s);
        }

        #endregion

        #region InfixFunction

        /// <summary>
        /// Evaluates the current function
        /// </summary>
        /// <param name="myStack"></param>
        /// <param name="count"></param>
        private void InfixFunction(Stack myStack, int count)
        {
            object[] args = new object[count];

            for (int i = count - 1; i >= 0; i--)
                args[i] = myStack.Pop();

            FToken t = (FToken)myStack.Pop();

            string func;
            int index = 0;

            if (t == FToken.User)
            {
                func = args[index++].ToString();

                IsValueMarker('"', '"', ref func, false);
                func = ColorPart(func, ExprColorPart.UserFunction);
            }
            else
            {
                func = Enum.GetName(typeof(FToken), t);
                func = ColorPart(func, ExprColorPart.SysFunction);
            }

            StringBuilder sb = new StringBuilder();

            for (int i=index; i<args.Length; i++)
            {
                object o = args[i];

                sb.Append(InfixValue(o));
                sb.Append(", ");
            }

            if (sb.Length > 0)
                sb.Length -= 2;

            sb.Insert(0, func + "(");
            sb.Append(")");

            myStack.Push(sb.ToString());
        }

        #endregion

        #region InfixOperator

        private void InfixOperator(Stack myStack, FToken token)
        {
            if (myStack.Count < 2)
                throw new Exception(_exprErrorString);

            string o1 = InfixValue(myStack.Pop());
            string o2 = InfixValue(myStack.Pop());

            string op = FTokenList.GetOpTokenText(token);

            string s;

            if (token == FToken.Between)
            {
                string o3 = InfixValue(myStack.Pop());

                s = o3 + ColorPart(" is between ", ExprColorPart.Operator) +
                    o2 + ColorPart(" and ", ExprColorPart.Operator) + o1;
            }
            else if (token == FToken.Like)
            {
                s = o2 + ColorPart(" is " + op + " ", ExprColorPart.Operator) + o1;
            }
            else
            {
                s = o2 + " " + ColorPart(op, ExprColorPart.Operator) + " " + o1;
            }

            myStack.Push("(" + s + ")");
        }

        #region InfixValue

	    /// <summary>
	    /// Process the given object value
	    /// </summary>
	    /// <param name="o"></param>
	    /// <returns></returns>
	    private string InfixValue(object o)
        {
            GridColumn col = o as GridColumn;

            if (col != null)
                return (ColorPart(GetFilterColumnName(col), ExprColorPart.Column));

	        if (o is EmptyFilterOp)
	            return (ColorPart("empty", ExprColorPart.Operator));

	        if (o == null)
                return (ColorPart("null", ExprColorPart.Operator));

	        string s = o.ToString();

            if (s.StartsWith("\"") && s.EndsWith("\""))
                return (ColorPart(s, ExprColorPart.String));

	        return (s);
        }

        #region GetFilterColumnName

        private string GetFilterColumnName(GridColumn gridColumn)
        {
            if (gridColumn.FilterExprUsesName == false &&
                string.IsNullOrEmpty(gridColumn.HeaderText) == false)
            {
                return (gridColumn.HeaderText);
            }
            
            return (gridColumn.Name);
        }

        #endregion

        #endregion

        #endregion

        #region ColorPart

        private string ColorPart(string s, ExprColorPart part)
        {
            if (_Colorize == true)
                s = "#@@#" + (int)part + s + "#@@#0";

            return (s);
        }

        #endregion

        #endregion

        #region LoadLocalizedStrings

        private void LoadLocalizedStrings()
        {
            if (_localizedStringsLoaded == false)
            {
                using (LocalizationManager lm = new LocalizationManager(_GridPanel.SuperGrid))
                {
                    string s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterExprError)) != "")
                        _exprErrorString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterMissingParen)) != "")
                        _missingParenString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterMissingQuote)) != "")
                        _missingQuoteString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterInvalidArgCount)) != "")
                        _invalidArgCountString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterInvalidArg)) != "")
                        _invalidArgString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterInvalidEmptyOp)) != "")
                        _invalidEmptyOpString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterInvalidDateTimeOp)) != "")
                        _invalidDateTimeOpString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterInvalidStringOp)) != "")
                        _invalidStringOpString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterInvalidBoolOp)) != "")
                        _invalidBoolOpString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterInvalidNumericOp)) != "")
                        _invalidNumericOpString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterInvalidEval)) != "")
                        _invalidEvalString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterUndefinedFunction)) != "")
                        _undefinedFunctionString = s;
                }

                _localizedStringsLoaded = true;
            }
        }

        #endregion

        #region FTokenList

        internal class FTokenList
        {
            #region Static data

            internal static TokenPair[] TokenListA = {
                new TokenPair("+", FToken.Add),
                new TokenPair("-", FToken.Subtract),
                new TokenPair("*", FToken.Multiply),
                new TokenPair("/", FToken.Divide),

                new TokenPair("=", FToken.Equal),
                new TokenPair("!=", FToken.NotEqual),
                new TokenPair("<>", FToken.NotEqual2),
                new TokenPair("<", FToken.LessThan),
                new TokenPair("<=", FToken.LessThanOrEqual),
                new TokenPair(">", FToken.GreaterThan),
                new TokenPair(">=", FToken.GreaterThanOrEqual),
                new TokenPair("(", FToken.LParen),
                new TokenPair(")", FToken.RParen),

                new TokenPair("&&", FToken.ConditionalAnd),
                new TokenPair("||", FToken.ConditionalOr),

                new TokenPair("&", FToken.LogicalAnd),
                new TokenPair("|", FToken.LogicalOr),
                new TokenPair("^", FToken.LogicalXor),
                new TokenPair("%", FToken.Mod),
                new TokenPair("<<", FToken.ShiftLeft),
                new TokenPair(">>", FToken.ShiftRight),
                new TokenPair(",", FToken.Comma),
            };

            internal static TokenPair[] TokenListB = {
                new TokenPair("and", FToken.ConditionalAnd),
                new TokenPair("or", FToken.ConditionalOr),
                new TokenPair("like", FToken.Like),
                new TokenPair("is", FToken.Is),
                new TokenPair("not", FToken.Negate),
                new TokenPair("between", FToken.Between),
                new TokenPair("xor", FToken.LogicalXor),
            };

            internal static TokenPair[] TokenListC = {
                new TokenPair("now", FToken.Now),
                new TokenPair("date", FToken.Date),

                new TokenPair("tod", FToken.TimeOfDay),
                new TokenPair("timeofday", FToken.TimeOfDay),

                new TokenPair("dow", FToken.DayOfWeek),
                new TokenPair("dayofweek", FToken.DayOfWeek),

                new TokenPair("doy", FToken.DayOfYear),
                new TokenPair("dayofyear", FToken.DayOfYear),

                new TokenPair("firstofmonth", FToken.FirstOfMonth),
                new TokenPair("endofmonth", FToken.EndOfMonth),

                new TokenPair("year", FToken.Year),
                new TokenPair("years", FToken.Year),
                new TokenPair("month", FToken.Month),
                new TokenPair("months", FToken.Month),
                new TokenPair("day", FToken.Day),
                new TokenPair("days", FToken.Day),
                new TokenPair("hour", FToken.Hour),
                new TokenPair("hours", FToken.Hour),
                new TokenPair("minute", FToken.Minute),
                new TokenPair("minutes", FToken.Minute),
                new TokenPair("second", FToken.Second),
                new TokenPair("seconds", FToken.Second),

                new TokenPair("addyears", FToken.AddYears),
                new TokenPair("addmonths", FToken.AddMonths),
                new TokenPair("adddays", FToken.AddDays),
                new TokenPair("addhours", FToken.AddHours),
                new TokenPair("addminutes", FToken.AddMinutes),
                new TokenPair("addseconds", FToken.AddSeconds),

                new TokenPair("totalyears", FToken.TotalYears),
                new TokenPair("totaldays", FToken.TotalDays),
                new TokenPair("totalhours", FToken.TotalHours),
                new TokenPair("totalminutes", FToken.TotalMinutes),
                new TokenPair("totalseconds", FToken.TotalSeconds),

                new TokenPair("ceiling", FToken.Ceiling),
                new TokenPair("floor", FToken.Floor),
                new TokenPair("round", FToken.Round),

                new TokenPair("length", FToken.Length),

                new TokenPair("raw", FToken.Raw),
                new TokenPair("right", FToken.Right),
                new TokenPair("left", FToken.Left),
                new TokenPair("substring", FToken.Substring),
                new TokenPair("indexof", FToken.IndexOf),

                new TokenPair("convert", FToken.Convert),
                new TokenPair("tostring", FToken.ToString),
                new TokenPair("tolower", FToken.ToLower),
                new TokenPair("toupper", FToken.ToUpper),

                new TokenPair("trim", FToken.Trim),
                new TokenPair("ltrim", FToken.LTrim),
                new TokenPair("rtrim", FToken.RTrim),
            };

            #endregion

            #region GetToken

            public static FToken GetToken(string text)
            {
                foreach (TokenPair tp in TokenListA)
                {
                    if (tp.Text.Equals(text) == true)
                        return (tp.Token);
                }

                return (GetStringToken(text, TokenListB));
            }

            #endregion

            #region GetStringToken

            public static FToken
                GetStringToken(string text, TokenPair[] tokenList)
            {
                string s = text.ToLower();

                foreach (TokenPair tp in tokenList)
                {
                    if (tp.Text.Equals(s) == true)
                        return (tp.Token);
                }

                return (FToken.BadToken);
            }

            #endregion

            #region GetOpTokenText

            public static string GetOpTokenText(FToken token)
            {
                foreach (TokenPair tp in TokenListB)
                {
                    if (tp.Token == token)
                        return (tp.Text);
                }

                foreach (TokenPair tp in TokenListA)
                {
                    if (tp.Token == token)
                        return (tp.Text);
                }

                return ("****");
            }

            #endregion

            #region TokenPair

            internal class TokenPair
            {
                #region Private data

                public readonly string Text;
                public readonly FToken Token;

                #endregion

                public TokenPair(string text, FToken token)
                {
                    Text = text;
                    Token = token;
                }
            }

            #endregion
        }

        #endregion

        #region ExceptionArg/Count

        private class ExceptionArgCount : Exception
        {
            public ExceptionArgCount()
                : base(Name + "(): " + _invalidArgCountString)
            {
            }

            private static string Name
            {
                get { return (new StackTrace().GetFrame(2).GetMethod().Name); }
            }
        }

        private class ExceptionArg : Exception
        {
            public ExceptionArg()
                : base(Name + "(): " + _invalidArgString)
            {
            }

            private static string Name
            {
                get { return (new StackTrace().GetFrame(2).GetMethod().Name); }
            }

        }

        #endregion
    }

    #region FToken

    ///<summary>
    /// Filter Tokens
    ///</summary>
    internal enum FToken
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

        LogicalOr,
        LogicalAnd,
        LogicalXor,

        Function,
        Comma,

        ConditionalOr,
        ConditionalAnd,

        ConditionalOrRpn,
        ConditionalAndRpn,

        Equal,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual,
        NotEqual,
        NotEqual2,

        Like,
        Is,
        Between,

        BadToken,

        // Anything after this is a function

        Functions,

        User,

        Ceiling,
        Floor,
        Round,

        Length,

        Substring,
        IndexOf,
        Left,
        Right,
        Raw,

        Convert,
        ToString,
        ToLower,
        ToUpper,

        Trim,
        LTrim,
        RTrim,

        Now,
        Date,

        TimeOfDay,
        DayOfWeek,
        DayOfYear,

        FirstOfMonth,
        EndOfMonth,

        Year,
        Month,
        Day,
        Hour,
        Minute,
        Second,

        AddYears,
        AddMonths,
        AddDays,
        AddHours,
        AddMinutes,
        AddSeconds,

        TotalYears,
        TotalDays,
        TotalHours,
        TotalMinutes,
        TotalSeconds,
    };

    #endregion

    #region EmptyFilterOp

    ///<summary>
    /// Empty Filter Operand
    ///</summary>
    public class EmptyFilterOp
    {
    }

    #endregion

    #region Wildcard

    internal class Wildcard : Regex
    {
        public Wildcard(string pattern)
            : base(WildcardToRegex(pattern))
        {
        }

        public Wildcard(string pattern, RegexOptions options)
            : base(WildcardToRegex(pattern), options)
        {
        }

        public static string WildcardToRegex(string pattern)
        {
            string s = ("^" + pattern
                              .Replace("*", ".*")
                              .Replace("?", ".")
                              .Replace("(", "\\(")
                              .Replace(")", "\\)")
                              .Replace("]", "]+") + "$");

            return (s);
        }
    }

    #endregion

    #region ExprColorPart

    internal enum ExprColorPart
    {
        Default,
        Dim,

        Column,
        Operator,
        String,
        SysFunction,
        UserFunction,
        Error,

        LastEntry
    }

    #endregion

    #region ExpressionColors

    ///<summary>
    /// Expression syntax Colors
    ///</summary>
    [TypeConverter(typeof(ExpressionColorsConverter))]
    public class ExpressionColors
    {
        #region Private variables

        private Color[] _Colors;

        #endregion

        ///<summary>
        /// Expression syntax Colors
        ///</summary>
        public ExpressionColors()
        {
            _Colors = new Color[(int)ExprColorPart.LastEntry];

            _Colors[(int)ExprColorPart.Default] = Color.Black;
            _Colors[(int)ExprColorPart.Dim] = Color.DimGray;
            _Colors[(int)ExprColorPart.Column] = Color.Green;
            _Colors[(int)ExprColorPart.Operator] = Color.Blue;
            _Colors[(int)ExprColorPart.String] = Color.Crimson;
            _Colors[(int)ExprColorPart.SysFunction] = Color.DarkRed;
            _Colors[(int)ExprColorPart.UserFunction] = Color.Teal;
            _Colors[(int)ExprColorPart.Error] = Color.Crimson;
        }

        #region Public properties

        #region Default

        ///<summary>
        /// Default text color
        ///</summary>
        [DefaultValue(typeof(Color), "Black")]
        public Color Default
        {
            get { return (_Colors[(int)ExprColorPart.Default]); }
            set { _Colors[(int)ExprColorPart.Default] = value; }
        }

        #endregion

        #region Dim

        ///<summary>
        /// Dim text color
        ///</summary>
        [DefaultValue(typeof(Color), "DimGray")]
        public Color Dim
        {
            get { return (_Colors[(int)ExprColorPart.Dim]); }
            set { _Colors[(int)ExprColorPart.Dim] = value; }
        }

        #endregion

        #region Column

        ///<summary>
        /// Column Name text color
        ///</summary>
        [DefaultValue(typeof(Color), "Green")]
        public Color Column
        {
            get { return (_Colors[(int)ExprColorPart.Column]); }
            set { _Colors[(int)ExprColorPart.Column] = value; }
        }

        #endregion

        #region Operator

        ///<summary>
        /// Operator text color
        ///</summary>
        [DefaultValue(typeof(Color), "Blue")]
        public Color Operator
        {
            get { return (_Colors[(int)ExprColorPart.Operator]); }
            set { _Colors[(int)ExprColorPart.Operator] = value; }
        }

        #endregion

        #region String

        ///<summary>
        /// String text color
        ///</summary>
        [DefaultValue(typeof(Color), "Crimson")]
        public Color String
        {
            get { return (_Colors[(int)ExprColorPart.String]); }
            set { _Colors[(int)ExprColorPart.String] = value; }
        }

        #endregion

        #region SysFunction

        ///<summary>
        /// System Function text color
        ///</summary>
        [DefaultValue(typeof(Color), "SaddleBrown")]
        public Color SysFunction
        {
            get { return (_Colors[(int)ExprColorPart.SysFunction]); }
            set { _Colors[(int)ExprColorPart.SysFunction] = value; }
        }

        #endregion

        #region UserFunction

        ///<summary>
        /// User Function text color
        ///</summary>
        [DefaultValue(typeof(Color), "Teal")]
        public Color UserFunction
        {
            get { return (_Colors[(int)ExprColorPart.UserFunction]); }
            set { _Colors[(int)ExprColorPart.UserFunction] = value; }
        }

        #endregion

        #region Error

        ///<summary>
        /// Error text color
        ///</summary>
        [DefaultValue(typeof(Color), "Crimson")]
        public Color Error
        {
            get { return (_Colors[(int)ExprColorPart.Error]); }
            set { _Colors[(int)ExprColorPart.Error] = value; }
        }

        #endregion

        #endregion

        #region Internal properties

        #region Colors

        internal Color[] Colors
        {
            get { return (_Colors); }
        }

        #endregion

        #endregion
    }

    #region ExpressionColorsConverter

    ///<summary>
    /// ExpressionColorsConverter
    ///</summary>
    public class ExpressionColorsConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(
            ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
                return (" ");

            return (base.ConvertTo(context, culture, value, destinationType));
        }
    }

    #endregion

    #endregion
}