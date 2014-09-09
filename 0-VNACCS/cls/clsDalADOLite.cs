using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Collections.Generic;

namespace DevComponents.DotNetBar
{
    public class clsDalADOLite
    {
        #region Dynamic SQL
        public static DataSet GetDataSet(string TSQL)
        {
            return GetDataSet(TSQL, null);
        }

        public static DataSet GetDataSet(string TSQL, IDbConnection myConn)
        {
            return GetDataSet(TSQL, myConn, null);
        }

        public static DataSet GetDataSet(string TSQL, IDbConnection myConn, IDbTransaction myTrans)
        {
            bool mustClose = false;
            if (myConn == null)
            {
                mustClose = true;
                myConn = clsConn.getConnSQLite();
            }
            DataSet myDS = new DataSet();
            SQLiteCommand myCMD = new SQLiteCommand(TSQL, myConn as SQLiteConnection);
            SQLiteDataAdapter myAD = new SQLiteDataAdapter(myCMD);
            // 
            myCMD.CommandType = CommandType.Text;
            myCMD.CommandTimeout = 180000;//3 phut             
            if (myTrans != null)
                myCMD.Transaction = myTrans as SQLiteTransaction;
            // 
            myAD.Fill(myDS);
            //
            if (mustClose) myConn.Close();
            return myDS;
        }

        public static IDataReader GetDataReader(string TSQL, IDbConnection myConn)
        {
            IDataReader myRD;
            IDbCommand myCMD = myConn.CreateCommand();
            myCMD.Connection = myConn;
            myCMD.CommandText = TSQL;
            // 
            myCMD.CommandType = CommandType.Text;
            myCMD.CommandTimeout = 180000;
            // 
            myRD = myCMD.ExecuteReader();
            // 
            return myRD;
        }

        public static bool CheckExist(string TSQL)
        {
            //Hàm checkexist này còn dùng cho cả đối với trường hợp check duplicate 
            //Nếu là duplicate sẽ có dạng: SELECT COUNT(*) FROM TABLE WHERE ID=@ID AND xID<>@xID 
            //Nếu là exist sẽ có dạng: SELECT COUNT(*) FROM TABLE WHERE ID=@ID 
            return CheckExist(TSQL, null);
        }

        public static bool CheckExist(string TSQL, IDbConnection myConn)
        {
            //Hàm checkexist này còn dùng cho cả đối với trường hợp check duplicate 
            //Nếu là duplicate sẽ có dạng: SELECT COUNT(*) FROM TABLE WHERE ID=@ID AND xID<>@xID 
            //Nếu là exist sẽ có dạng: SELECT COUNT(*) FROM TABLE WHERE ID=@ID 
            return CheckExist(TSQL, myConn, null);
        }

        public static bool CheckExist(string TSQL, IDbConnection myConn, IDbTransaction myTrans)
        {
            //Hàm checkexist này còn dùng cho cả đối với trường hợp check duplicate 
            //Nếu là duplicate sẽ có dạng: SELECT COUNT(*) FROM TABLE WHERE ID=@ID AND xID<>@xID 
            //Nếu là exist sẽ có dạng: SELECT COUNT(*) FROM TABLE WHERE ID=@ID 
            DataSet myDS = GetDataSet(TSQL, myConn, myTrans);
            if ((int)myDS.Tables[0].Rows[0][0] == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static int AffectData(string TSQL)
        {
            return AffectData(TSQL, null);
        }

        public static int AffectData(string TSQL, IDbConnection myConn)
        {
            return AffectData(TSQL, myConn, null);
        }

        public static int AffectData(string TSQL, IDbConnection myConn, IDbTransaction myTrans)
        {
            return AffectData(TSQL, myConn, myTrans, null);
        }

        public static int AffectData(string TSQL, IDbConnection myConn, IDbTransaction myTrans, List<IDbDataParameter> myParams)
        {
            bool mustClose = false;
            if (myConn == null)
            {
                mustClose = true;
                myConn = clsConn.getConnSQLite();
            }
            SQLiteCommand myCMD = new SQLiteCommand();
            // 
            myCMD.Connection = myConn as SQLiteConnection;
            if (myTrans != null)
                myCMD.Transaction = myTrans as SQLiteTransaction;
            //
            myCMD.CommandType = CommandType.Text;
            myCMD.CommandText = TSQL;
            myCMD.CommandTimeout = 180000;//3 phut 
            //
            if (myParams != null)
                AttachParameters(myCMD, myParams);

            int CMDResult = myCMD.ExecuteNonQuery();
            //
            if (mustClose) myConn.Close();
            return CMDResult;
        }

        public static string GetFValue(string TSQL)
        {
            return GetFValue(TSQL, null);
        }

        public static string GetFValue(string TSQL, IDbConnection myConn)
        {
            return GetFValue(TSQL, myConn, null);
        }

        public static string GetFValue(string TSQL, IDbConnection myConn, IDbTransaction myTrans)
        {
            bool mustClose = false;
            if (myConn == null)
            {
                mustClose = true;
                myConn = clsConn.getConnSQLite();
            }

            DataSet myDS = GetDataSet(TSQL, myConn, myTrans);
            try
            {
                return myDS.Tables[0].Rows[0][0].ToString();
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                if (mustClose) myConn.Close();
            }
        }

        public static DataTable GetDBSchema()
        {
            return GetDBSchema(null);
        }

        public static DataTable GetDBSchema(IDbConnection myConn)
        {
            return GetDBSchema(myConn, null);
        }

        public static DataTable GetDBSchema(IDbConnection myConn, IDbTransaction myTrans)
        {
            bool mustClose = false;
            if (myConn == null)
            {
                mustClose = true;
                myConn = clsConn.getConnSQLite();
            }
            StringBuilder sbSql = new StringBuilder();

            DataSet myDS = GetDataSet("select * from sqlite_master;", myConn, myTrans);
            try
            {
                return myDS.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                if (mustClose) myConn.Close();
            }
        }

        public static DataTable GetTableSchema(string tableName)
        {
            return GetTableSchema(tableName, null);
        }

        public static DataTable GetTableSchema(string tableName, IDbConnection myConn)
        {
            return GetTableSchema(tableName, myConn, null);
        }

        public static DataTable GetTableSchema(string tableName, IDbConnection myConn, IDbTransaction myTrans)
        {
            bool mustClose = false;
            if (myConn == null)
            {
                mustClose = true;
                myConn = clsConn.getConnSQLite();
            }
            StringBuilder sbSql = new StringBuilder();

            DataSet myDS = GetDataSet(string.Format("pragma table_info({0});", tableName), myConn, myTrans);
            try
            {
                return myDS.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                if (mustClose) myConn.Close();
            }
        }
 
        public static BindingSource GetBindBox(string TSQL)
        {
            return GetBindBox(TSQL, null);
        }

        public static BindingSource GetBindBox(string TSQL, IDbConnection myConn)
        {
            bool mustClose = false;
            if (myConn == null)
            {
                mustClose = true;
                myConn = clsConn.getConnSQLite();
            }

            DataSet myDS = GetDataSet(TSQL, myConn as SQLiteConnection);
            if (mustClose) myConn.Close();
            BindingSource myBind = new BindingSource();
            // 
            myBind.DataSource = myDS;
            myBind.DataMember = myDS.Tables[0].ToString();
            myBind.AddNew();
            //Thêm 1 dòng trắng cuối cùng của list 
            // 
            return myBind;
        }

        public static BindingSource GetBindView(string TSQL)
        {
            return GetBindView(TSQL, null);
        }

        public static BindingSource GetBindView(string TSQL, IDbConnection myConn)
        {
            bool mustClose = false;
            if (myConn == null)
            {
                mustClose = true;
                myConn = clsConn.getConnSQLite();
            }

            DataSet myDS = GetDataSet(TSQL, myConn as SQLiteConnection);
            if (mustClose) myConn.Close();
            BindingSource myBind = new BindingSource();
            // 
            myBind.DataSource = myDS;
            myBind.DataMember = myDS.Tables[0].ToString();
            // 
            return myBind;
        }
        #endregion

        #region Parameter
        private static DbType ConvertToDbType(string type)
        {
            switch (type.ToLower())
            {
                case "ansistring":
                    return DbType.AnsiString;
                case "byte":
                case "tinyint":
                case "unsignedtinyint":
                    return DbType.Byte;
                case "boolean":
                case "bit":
                    return DbType.Boolean;
                case "currency":
                case "money":
                case "smallmoney":
                    return DbType.Currency;
                case "date":
                case "smalldatetime":
                case "timestamp":
                    return DbType.Date;
                case "datetime":
                    return DbType.DateTime;
                case "decimal":
                case "numeric":
                    return DbType.Decimal;
                case "double":
                case "float":
                    return DbType.Double;
                case "guid":
                    return DbType.Guid;
                case "int16":
                    return DbType.Int16;
                case "int32":
                case "int":
                    return DbType.Int32;
                case "int64":
                case "bigint":
                    return DbType.Int64;
                case "object":
                case "image":
                case "binary":
                    return DbType.Object;
                case "sbyte":
                    return DbType.SByte;
                case "single":
                case "real":
                    return DbType.Single;
                case "time":
                    return DbType.Time;
                case "uint16":
                    return DbType.UInt16;
                case "uint32":
                    return DbType.UInt32;
                case "uint64":
                    return DbType.UInt64;
                case "varnumeric":
                    return DbType.VarNumeric;
                case "ansistringfixedlength":
                    return DbType.AnsiStringFixedLength;
                case "stringfixedlength":
                    return DbType.StringFixedLength;
                case "xml":
                    return DbType.Xml;
                case "datetime2":
                    return DbType.DateTime2;
                case "datetimeoffset":
                    return DbType.DateTimeOffset;
                case "string":
                case "char":
                case "nchar":
                default:
                    return DbType.String;
            }
        }

        public static DbType GetDbType(string type)
        {
            int intType = 0;
            if (int.TryParse(type, out intType))
                return (DbType)intType;
            else
                return ConvertToDbType(type);
        }
        private static void AttachParameters(IDbCommand command, List<IDbDataParameter> commandParameters)
        {
            if ((command == null)) throw new ArgumentNullException("command");
            if (((commandParameters != null)))
            {
                foreach (SQLiteParameter p in commandParameters)
                {
                    if (((p != null)))
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) && p.Value == null)
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }

        public static IDbDataParameter CreateParameter(string parameterName, string type, object value)
        {
            return CreateParameter(parameterName, type, value, ParameterDirection.Input);
        }

        public static IDbDataParameter CreateParameter(string parameterName, string type, object value, ParameterDirection direction)
        {
            SQLiteParameter param = new SQLiteParameter(parameterName, GetDbType(type));
            param.Value = value;
            param.Direction = direction;
            return param;
        }

        public static IDbDataParameter CreateParameter(string parameterName, string type, int size, object value, ParameterDirection direction)
        {
            IDbDataParameter param = CreateParameter(parameterName, type, value, direction);
            param.Size = size;
            return param;
        }
        #endregion
    }
}
