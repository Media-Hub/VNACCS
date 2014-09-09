using System;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// ConvertValue
    ///</summary>
    static public class ConvertValue
    {
        ///<summary>
        /// ConvertTo
        ///</summary>
        ///<param name="o"></param>
        ///<param name="type"></param>
        ///<returns></returns>
        static public object ConvertTo(object o, Type type)
        {
            if (o == null || o is DBNull)
                return (o);

            if (o.GetType() == type)
                return (o);

            if (type == typeof (decimal))
                return (Convert.ToDecimal(o));

            if (type == typeof (double))
                return (Convert.ToDouble(o));

            if (type == typeof (Int16))
                return (Convert.ToInt16(o));

            if (type == typeof (Int32))
                return (Convert.ToInt32(o));

            if (type == typeof (Int64))
                return (Convert.ToInt64(o));

            if (type == typeof (Single))
                return (Convert.ToSingle(o));

            if (type == typeof (UInt16))
                return (Convert.ToUInt16(o));

            if (type == typeof (UInt32))
                return (Convert.ToUInt32(o));

            if (type == typeof (UInt64))
                return (Convert.ToUInt64(o));

            if (type == typeof(sbyte))
                return (Convert.ToSByte(o));

            if (type == typeof(byte))
                return (Convert.ToByte(o));

            if (type == typeof(bool))
                return (Convert.ToBoolean(o));

            if (type == typeof(char))
                return (Convert.ToChar(o));

            if (type == typeof(string))
                return (Convert.ToString(o));

            if (type == typeof(DateTime))
                return (Convert.ToDateTime(o));

            return (o);
        }
    }
}
