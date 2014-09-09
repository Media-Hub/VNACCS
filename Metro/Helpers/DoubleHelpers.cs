using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace DevComponents.DotNetBar.Metro.Helpers
{
    internal static class DoubleHelpers
    {
        /// <summary>
        /// Gets whether values are close.
        /// </summary>
        /// <param name="value1">First value.</param>
        /// <param name="value2">Second value</param>
        /// <returns>true if values are close enough</returns>
        public static bool AreClose(double value1, double value2)
        {
            if (value1 == value2)
            {
                return true;
            }
            double num2 = ((Math.Abs(value1) + Math.Abs(value2)) + 10.0) * 2.2204460492503131E-16;
            double num = value1 - value2;
            return ((-num2 < num) && (num2 > num));
        }
        /// <summary>
        /// Gets whether value is zero
        /// </summary>
        /// <param name="value">value to check</param>
        /// <returns>true if value is considered zero</returns>
        public static bool IsZero(double value)
        {
            return (Math.Abs(value) < 2.2204460492503131E-15);
        }

        /// <summary>
        /// Gets whether value is not an number.
        /// </summary>
        /// <param name="value">value to test</param>
        /// <returns>true if value is not an number</returns>
        public static bool IsNaN(double value)
        {
            NanUnion union = new NanUnion();
            union.DoubleValue = value;
            ulong num = union.UintValue & 18442240474082181120L;
            ulong num2 = union.UintValue & ((ulong)0xfffffffffffffL);
            if ((num != 0x7ff0000000000000L) && (num != 18442240474082181120L))
            {
                return false;
            }
            return (num2 != 0L);
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct NanUnion
        {
            // Fields
            [FieldOffset(0)]
            internal double DoubleValue;
            [FieldOffset(0)]
            internal ulong UintValue;
        }
    }
}
