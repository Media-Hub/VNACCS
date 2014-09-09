namespace Naccs.Common.Function
{
    using System;
    using System.Globalization;

    public sealed class Validation
    {
        public static bool IsNumeric(object oTarget)
        {
            return IsNumeric(oTarget.ToString());
        }

        public static bool IsNumeric(string stTarget)
        {
            double num;
            return double.TryParse(stTarget, NumberStyles.Any, (IFormatProvider) null, out num);
        }
    }
}

