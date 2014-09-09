using System;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Collections.Generic;

namespace DevComponents.DotNetBar
{
    public class clsDalORACLE
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
                myConn = clsConn.getConnOracle();
            }
            DataSet myDS = new DataSet();
            OracleCommand myCMD = new OracleCommand(TSQL, myConn as OracleConnection);
            OracleDataAdapter myAD = new OracleDataAdapter(myCMD);
            // 
            myCMD.CommandType = CommandType.Text;
            myCMD.CommandTimeout = 180000;//3 phut             
            if (myTrans != null)
                myCMD.Transaction = myTrans as OracleTransaction;
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
                myConn = clsConn.getConnOracle();
            }
            OracleCommand myCMD = new OracleCommand();
            // 
            myCMD.Connection = myConn as OracleConnection;
            if (myTrans != null)
                myCMD.Transaction = myTrans as OracleTransaction;
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
                myConn = clsConn.getConnOracle();
            }

            DataSet myDS = GetDataSet(TSQL, myConn, myTrans);
            try
            {
                return myDS.Tables[0].Rows[0][0].ToString();
            }
            catch
            {
                return "0";
            }
            finally
            {
                if (mustClose) myConn.Close();
            }
        }

        public static DataTable GetTableSchema(string tableName, string strOwner)
        {
            return GetTableSchema(tableName,strOwner, null);
        }

        public static DataTable GetTableSchema(string tableName,string strOwner, IDbConnection myConn)
        {
            bool mustClose = false;
            if (myConn == null)
            {
                mustClose = true;
                myConn = clsConn.getConnOracle();
            }
            StringBuilder sbSql = new StringBuilder();


            sbSql.AppendLine("SELECT ' ' DESCRIPTION, b.data_default COLUMN_DEFAULT,(CASE WHEN b.data_default IS NULL THEN 0 ELSE 1 END) COLUMN_HASDEFAULT, b.data_length CHARACTER_MAXIMUM_LENGTH,b.nullable IS_NULLABLE, B.TABLE_NAME ,A.COLUMN_NAME PK, (CASE WHEN A.COLUMN_NAME IS NULL THEN 0 ELSE 1 END) ISPK,B.COLUMN_NAME,B.DATA_TYPE,A.POSITION ORDINAL_POSITION FROM   ");
            sbSql.AppendLine("( ");
            sbSql.AppendLine("  SELECT cols.table_name, cols.column_name , cols.position, cons.status, cons.owner ");
            sbSql.AppendLine("  FROM all_constraints cons, all_cons_columns cols ");
            sbSql.AppendLine("  WHERE cons.constraint_type = 'P' ");
            sbSql.AppendLine("  AND cons.constraint_name = cols.constraint_name ");
            sbSql.AppendFormat("  AND cons.owner = '{1}' AND cons.TABLE_NAME='{0}' ", tableName, strOwner);
            sbSql.AppendLine("  ORDER BY cols.table_name, cols.position ");
            sbSql.AppendLine(") A RIGHT OUTER JOIN  ");
            sbSql.AppendFormat("(SELECT * FROM COLS WHERE TABLE_NAME='{0}') B ON B.TABLE_NAME=A.TABLE_NAME AND B.COLUMN_NAME=A.COLUMN_NAME  ", tableName);
                       

            DataSet myDS = GetDataSet(sbSql.ToString(), myConn);
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

        public static DataTable GetSPParams()
        {
            return GetSPParams(null);
        }

        public static DataTable GetSPParams(IDbConnection myConn)
        {
            bool mustClose = false;
            if (myConn == null)
            {
                mustClose = true;
                myConn = clsConn.getConnOracle();
            }
            StringBuilder sbSql = new StringBuilder();

            sbSql.AppendLine("SELECT");
            sbSql.AppendLine("	pa.SPECIFIC_NAME, pa.PARAMETER_NAME, pa.ORDINAL_POSITION, pa.DATA_TYPE, pa.PARAMETER_MODE, pa.CHARACTER_MAXIMUM_LENGTH");
            sbSql.AppendLine("FROM information_schema.parameters pa");
            sbSql.AppendLine("INNER JOIN information_schema.routines  ro ON ro.SPECIFIC_NAME=pa.SPECIFIC_NAME");
            sbSql.AppendLine("WHERE ro.routine_type='PROCEDURE'");
            sbSql.AppendLine("ORDER BY pa.SPECIFIC_NAME, pa.ORDINAL_POSITION");
            
            DataSet myDS = GetDataSet(sbSql.ToString(), myConn);
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

        public static DataTable GetSPSchema(string SPName, List<IDbDataParameter> myParams)
        {
            return GetSPSchema(SPName, null, myParams);
        }

        public static DataTable GetSPSchema(string SPName, IDbConnection myConn, List<IDbDataParameter> myParams)
        {
            bool mustClose = false;
            if (myConn == null)
            {
                mustClose = true;
                myConn = clsConn.getConnOracle();
            }
            IDbTransaction myTrans = myConn.BeginTransaction();
            IDataReader drdr = null;
            try
            {
                AffectData("SET FMTONLY ON;", myConn, myTrans); 
                OracleCommand myCMD = new OracleCommand();
                // 
                myCMD.Connection = myConn as OracleConnection;
                if (myTrans != null)
                    myCMD.Transaction = myTrans as OracleTransaction;
                //
                myCMD.CommandType = CommandType.StoredProcedure;
                myCMD.CommandText = SPName;
                myCMD.CommandTimeout = 180000;//3 phut 
                //
                if (myParams != null)
                    AttachParameters(myCMD, myParams);

                drdr = myCMD.ExecuteReader();
                return drdr.GetSchemaTable();
            }
            catch
            {
                AffectData("SET FMTONLY OFF;", myConn, myTrans);
                return null;
            }
            finally
            {
                if (drdr != null && !drdr.IsClosed)
                    drdr.Close();
                myTrans.Rollback();
                if (mustClose) myConn.Close();
            }
        }
        #endregion

        #region Parameter
        private static OracleType ConvertToOracleType(string type)
        {
            switch (type.ToLower())
            {
                case "integer":
                case "bigint":
                case "int64":
                case "int32":
                    return OracleType.Int32;                
                case "uint32":
                case "uint64":
                    return OracleType.UInt32;
                case "bit":
                case "boolean":
                    return OracleType.Char;
                case "char":
                    return OracleType.Char;
                case "datetime":
                case "date":
                    return OracleType.DateTime;
                case "decimal":
                case "numeric":
                case "varnumeric":
                    return OracleType.Number;
                case "float":
                case "double":
                    return OracleType.Float;
                case "image":
                case "binary":
                case "object":
                    return OracleType.Byte;
                case "int":
                case "int16":
                    return OracleType.Int16;
                case "uint16":
                    return OracleType.UInt16;
                case "money":
                case "currency":
                    return OracleType.Number;
                case "nchar":
                    return OracleType.NChar;
                case "stringfixedlength":
                case "ntext":
                    return OracleType.NVarChar;
                case "nvarchar":
                case "varchar2":
                case "string":
                case "guid":
                    return OracleType.NVarChar;
                case "real":
                case "single":
                    return OracleType.Float;
                case "smalldatetime":
                    return OracleType.DateTime;
                case "smallint":
                    return OracleType.Int16;
                case "smallmoney":
                case "number":
                    return OracleType.Number;
                case "text":
                    return OracleType.NVarChar;
                case "timestamp":
                    return OracleType.Timestamp;
                case "tinyint":
                case "unsignedtinyint":
                case "byte":
                case "sbyte":
                    return OracleType.Int16;
                case "varbinary":
                    return OracleType.Byte;
                case "varchar":
                case "ansistring":
                    return OracleType.NVarChar;
                case "xml":
                    return OracleType.NVarChar;
                case "time":
                    return OracleType.DateTime;
                case "datetime2":
                    return OracleType.DateTime;
                case "datetimeoffset":
                    return OracleType.DateTime;              
                case "clob":
                    return OracleType.Clob;
                case "blob":
                    return OracleType.Blob;
                case "bfile":
                    return OracleType.BFile;
               
                case "variant":
                case "wchar":
                default:
                    return OracleType.NVarChar;
            }
        }

        public static OracleType GetOracleType(string type)
        {            
            int intType = 0;
            if (int.TryParse(type, out intType))
                return (OracleType)intType;
            else
                return ConvertToOracleType(type);
        }

        private static void AttachParameters(IDbCommand command, List<IDbDataParameter> commandParameters)
        {
            if ((command == null)) throw new ArgumentNullException("command");
            if (((commandParameters != null)))
            {
                foreach (OracleParameter p in commandParameters)
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
            OracleParameter param = new OracleParameter(parameterName, GetOracleType(type));
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
