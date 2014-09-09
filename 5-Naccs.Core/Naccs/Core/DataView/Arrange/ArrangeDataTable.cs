namespace Naccs.Core.DataView.Arrange
{
    using Naccs.Core.Classes;
    using Naccs.Core.DataView;
    using Naccs.Core.Properties;
    using Naccs.Core.Settings;
    using System;
    using System.Data;
    using System.Globalization;
    using System.IO;

    public class ArrangeDataTable
    {
        private const string cnDatasetName = "NewDataSet";
        private const string cnIndexName = "data.xml";
        private const string cnLockFileName = "_LockFile.lok";
        private const string cnStorageFolder = "PastDataView";
        private const string cnTableName = "Naccs";

        public static void BeginLock(string subFolder)
        {
            if (!string.IsNullOrEmpty(subFolder))
            {
                string path = IndexFolderPath(subFolder);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                File.WriteAllText(Path.Combine(path, "_LockFile.lok"), string.Empty);
            }
        }

        public static DataTable CreateDataTable()
        {
            DataSet set = new DataSet("NewDataSet");
            DataTable table = set.Tables.Add("Naccs");
            foreach (string str in ColumnNames)
            {
                DataColumn column = new DataColumn {
                    DataType = Type.GetType("System.String"),
                    ColumnName = str
                };
                table.Columns.Add(column);
            }
            return table;
        }

        public static void DataTableDataAdd(DataTable Table, IData Data, string FName)
        {
            DataRow row = Table.NewRow();
            string str = "";
            if (Data.Status == DataStatus.Recept)
            {
                str = Resources.ResourceManager.GetString("CORE108");
                if (Data.UserFolder.Trim().Length > 0)
                {
                    str = Data.UserFolder.Substring(6);
                }
            }
            else if (Data.Status == DataStatus.Sent)
            {
                str = Resources.ResourceManager.GetString("CORE109");
            }
            row["clFolder"] = str;
            row["clAirSea"] = UDataView.GetASType(Data);
            row["clJobCode"] = Data.JobCode;
            row["clOutCode"] = Data.OutCode;
            row["clInputNo"] = Data.Header.InputInfo;
            row["clJobInfo"] = Data.Header.Subject;
            row["clTimeStamp"] = Data.TimeStamp.ToString("dd/MM/yyyy HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
            row["clFileName"] = FName;
            Table.Rows.Add(row);
        }

        public static void EndLock(string subFolder)
        {
            if (!string.IsNullOrEmpty(subFolder))
            {
                string str = IndexFolderPath(subFolder);
                string path = Path.Combine(str, "_LockFile.lok");
                try
                {
                    File.Delete(path);
                }
                catch
                {
                }
                if (Directory.Exists(str) && (Directory.GetFiles(str, "*.txt", SearchOption.TopDirectoryOnly).Length == 0))
                {
                    try
                    {
                        Directory.Delete(str, true);
                    }
                    catch
                    {
                    }
                }
            }
        }

        public static string ExtractFileDate(string filename)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filename);
            if (fileNameWithoutExtension.Length < 14)
            {
                return string.Empty;
            }
            return fileNameWithoutExtension.Substring(fileNameWithoutExtension.Length - 14);
        }

        public static string IndexFilePath(string subFolder)
        {
            return Path.Combine(Path.Combine(Path.Combine(PathInfo.CreateInstance().CommonPath, "PastDataView"), subFolder), "data.xml");
        }

        public static string IndexFolderPath(string subFolder)
        {
            return Path.Combine(Path.Combine(PathInfo.CreateInstance().CommonPath, "PastDataView"), subFolder);
        }

        public static bool IsLock(string subFolder)
        {
            return File.Exists(Path.Combine(IndexFolderPath(subFolder), "_LockFile.lok"));
        }

        public static void Load(DataTable table, string subFolder)
        {
            string path = "";
            table.BeginLoadData();
            table.Rows.Clear();
            try
            {
                path = IndexFilePath(subFolder);
                if (File.Exists(path))
                {
                    table.DataSet.ReadXml(path);
                }
            }
            finally
            {
                table.EndLoadData();
            }
        }

        public static void Save(DataTable table, string subFolder)
        {
            if (!string.IsNullOrEmpty(subFolder))
            {
                string path = IndexFolderPath(subFolder);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                table.AcceptChanges();
                table.WriteXml(Path.Combine(path, "data.xml"));
            }
        }

        public static string[] ColumnNames
        {
            get
            {
                return Enum.GetNames(typeof(ColumnName));
            }
        }

        public static string LockFileName
        {
            get
            {
                return "_LockFile.lok";
            }
        }

        public static string PastDataViewPath
        {
            get
            {
                return Path.Combine(PathInfo.CreateInstance().CommonPath, "PastDataView");
            }
        }

        private enum ColumnName
        {
            clFolder,
            clAirSea,
            clJobCode,
            clOutCode,
            clInputNo,
            clJobInfo,
            clTimeStamp,
            clFileName
        }
    }
}

