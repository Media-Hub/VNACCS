namespace Naccs.Core.Classes
{
    using Naccs.Core.Properties;
    using Naccs.Core.Settings;
    using Naccs.Net;
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public class DataFactory
    {
        private static Encoding FileEncoding = new UTF8Encoding(false);

        public static IData CreateInstance()
        {
            return new NaccsData();
        }

        private static NaccsData createInstanceFromByteArray(byte[] byteArray, Encoding encoding)
        {
            if ((byteArray == null) || (byteArray.Length < 400))
            {
                return null;
            }
            NaccsData data = new NaccsData();
            data.NaccsHeader.SetBytesData(byteArray, encoding, true);
            if (data.Header.Length > 400)
            {
                int length = data.Header.Length;
                if (length > byteArray.Length)
                {
                    length = byteArray.Length;
                }
                length -= 400;
                data.JobData = NaccsHeader.ByteDataToString(byteArray, 400, length, encoding);
                return data;
            }
            if (data.Header.Length == 0)
            {
                data.JobData = NaccsHeader.ByteDataToString(byteArray, 400, byteArray.Length - 400, encoding);
            }
            return data;
        }

        public static RecvData CreateRecvData(IData data)
        {
            RecvData data2;
            data2.ResultData = null;
            data2.OtherData = null;
            data2.MData = null;
            if (data.Header.DataType == "M")
            {
                data2.MData = data;
                data2.ResultData = (IData) data.Clone();
                if (data2.ResultData.ItemCount > 1)
                {
                    data2.ResultData.Items.RemoveRange(1, data2.ResultData.ItemCount - 1);
                }
                data2.ResultData.Header.DataType = "R";
                data2.ResultData.Header.Div = "000";
                data2.ResultData.Header.EndFlag = " ";
                data2.ResultData.CalcLength();
                data2.OtherData = (IData) data.Clone();
                data2.OtherData.Items.RemoveAt(0);
                data2.OtherData.Header.DataType = "C";
                data2.OtherData.Header.Div = "001";
                data2.OtherData.Header.EndFlag = "E";
                data2.OtherData.CalcLength();
                return data2;
            }
            if (data.Header.DataType == "R")
            {
                data2.ResultData = data;
                return data2;
            }
            data2.OtherData = data;
            return data2;
        }

        public static RecvData CreateRecvData(ComParameter comParameter)
        {
            RecvData data = CreateRecvData(comParameter.DataString);
            if (comParameter.AttachCount > 0)
            {
                if (data.OtherData == null)
                {
                    comParameter.DeleteAttachFolder();
                }
                else
                {
                    PathInfo info = PathInfo.CreateInstance();
                    string path = info.AttachPath + data.OtherData.Header.OutCode.Trim() + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    comParameter.CopyAttachFiles(path);
                    comParameter.DeleteAttachFolder();
                    comParameter.AttachFolder = path;
                }
            }
            setAttachInfo(data.ResultData, comParameter);
            setAttachInfo(data.OtherData, comParameter);
            setAttachInfo(data.MData, comParameter);
            return data;
        }

        public static RecvData CreateRecvData(string data)
        {
            return CreateRecvData(NaccsHeader.GetEncodedBytes(data, NaccsHeader.CalculationEncoding));
        }

        public static RecvData CreateRecvData(byte[] byteArray)
        {
            RecvData data;
            data.ResultData = null;
            data.OtherData = null;
            data.MData = null;
            NaccsData data2 = createInstanceFromByteArray(byteArray, NaccsHeader.CalculationEncoding);
            if (data2 != null)
            {
                if (data2.Header.DataType == "R")
                {
                    data.ResultData = data2;
                }
                else
                {
                    if (data2.Header.DataType == "M")
                    {
                        data.MData = data2;
                        data.ResultData = new NaccsData();
                        data2.NaccsHeader.CopyTo(data.ResultData.Header);
                        data.ResultData.Items.Add(data2.Items[0]);
                        data.ResultData.Header.DataType = "R";
                        data.ResultData.Header.Div = "000";
                        data.ResultData.Header.EndFlag = " ";
                        data.ResultData.CalcLength();
                        data.OtherData = new NaccsData();
                        data2.NaccsHeader.CopyTo(data.OtherData.Header);
                        data.OtherData.Items.AddRange(data2.Items);
                        data.OtherData.Items.RemoveAt(0);
                        data.OtherData.Header.DataType = "C";
                        data.OtherData.Header.Div = "001";
                        data.OtherData.Header.EndFlag = "E";
                        data.OtherData.CalcLength();
                        return data;
                    }
                    data.OtherData = data2;
                }
                int length = 0;
                if (data2.Header.Length > 0)
                {
                    length = byteArray.Length - data2.Header.Length;
                }
                if (length < 400)
                {
                    return data;
                }
                byte[] destinationArray = new byte[length];
                Array.Copy(byteArray, data2.Header.Length, destinationArray, 0, length);
                NaccsData data3 = createInstanceFromByteArray(destinationArray, NaccsHeader.CalculationEncoding);
                if (data3.Header.DataType == "R")
                {
                    data.ResultData = data3;
                    return data;
                }
                data.OtherData = data3;
            }
            return data;
        }

        public static IData LoadFromDataView(string id, string fileName)
        {
            NaccsData data;
            XmlSerializer serializer = new XmlSerializer(typeof(NaccsData));
            using (XmlReader reader = XmlReader.Create(fileName))
            {
                data = (NaccsData) serializer.Deserialize(reader);
            }
            data.ID = id;
            return data;
        }

        public static IData LoadFromEdiFile(string fileName)
        {
            NaccsData data = new NaccsData();
            LoadFromEdiFile(fileName, data);
            return data;
        }

        public static void LoadFromEdiFile(string fileName, IData data)
        {
            byte[] bytes = null;
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(stream, FileEncoding))
                {
                    string s = reader.ReadToEnd();
                    bytes = FileEncoding.GetBytes(s);
                }
            }
            if (FileEncoding.GetString(bytes).IndexOf(Environment.NewLine) != 0x18e)
            {
                throw new Exception(Resources.ResourceManager.GetString("CORE72"));
            }
            NaccsData data2 = data as NaccsData;
            if (data2 == null)
            {
                data2 = new NaccsData();
            }
            (data2.Header as NaccsHeader).SetBytesData(bytes, FileEncoding, true);
            data2.JobData = NaccsHeader.ByteDataToString(bytes, 400, bytes.Length - 400, FileEncoding);
        }

        public static void SaveToDataView(IData data, string fileName)
        {
            XmlSerializer serializer;
            if (typeof(NaccsData).IsInstanceOfType(data))
            {
                serializer = new XmlSerializer(typeof(NaccsData));
            }
            else
            {
                serializer = new XmlSerializer(typeof(IData));
            }
            TextWriter textWriter = new StreamWriter(fileName);
            data.CalcLength();
            serializer.Serialize(textWriter, data);
            textWriter.Close();
        }

        public static void SaveToEdiFile(IData data, string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(stream, FileEncoding))
                {
                    writer.Write(data.GetDataString());
                }
                stream.Close();
            }
        }

        private static void setAttachInfo(IData data, ComParameter comParameter)
        {
            if ((data != null) && (comParameter.AttachCount > 0))
            {
                data.AttachFolder = "";
                if ((data.Header.DataType == "C") || (data.Header.DataType == "M"))
                {
                    data.AttachFolder = comParameter.AttachFolder;
                    data.AttachCount = comParameter.AttachCount;
                }
                data.Status = DataStatus.Recept;
                data.Contained = ContainedType.ReceiveOnly;
            }
            if ((data != null) && (comParameter.Sign == "1"))
            {
                data.Signflg = int.Parse(comParameter.Sign);
                data.SignFileFolder = "";
                PathInfo info = PathInfo.CreateInstance();
                DirectoryInfo info2 = new DirectoryInfo(info.SignFilePath);
                if (!info2.Exists)
                {
                    info2.Create();
                }
                string path = info.SignFilePath + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "chk.txt";
                StreamWriter writer = null;
                try
                {
                    writer = new StreamWriter(path);
                    writer.Write(comParameter.SignFile);
                }
                finally
                {
                    writer.Close();
                }
                data.SignFileFolder = path;
            }
        }
    }
}

