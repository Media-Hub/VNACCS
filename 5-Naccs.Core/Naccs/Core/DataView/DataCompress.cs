namespace Naccs.Core.DataView
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Runtime.InteropServices;
    using System.Text;

    public class DataCompress
    {
        public List<IndexRec> IndexList;
        private string mDataFile;
        private string mFolder;
        private string mIndexFile;

        public DataCompress(string path, string name)
        {
            this.mFolder = path;
            this.mDataFile = Path.Combine(this.mFolder, name);
            this.mIndexFile = Path.ChangeExtension(this.mDataFile, ".idx");
            this.CreateIndexList();
        }

        public void AddData(string data)
        {
            long offset = this.LastOffset() + this.LastSize();
            long size = this.AppendData(data);
            this.AppendIndex(offset, size);
        }

        private long AppendData(string data)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(data);
            byte[] buffer = this.Compress(bytes, 0, bytes.Length);
            FileStream stream = new FileStream(this.mDataFile, FileMode.Append, FileAccess.Write);
            stream.Write(buffer, 0, buffer.Length);
            stream.Close();
            return (long) buffer.Length;
        }

        private void AppendIndex(long offset, long size)
        {
            IndexRec item = new IndexRec {
                Offset = offset,
                Size = size
            };
            this.IndexList.Add(item);
            StreamWriter writer = new StreamWriter(this.mIndexFile, true);
            string str = string.Format("{0},{1}", offset, size);
            writer.WriteLine(str);
            writer.Close();
        }

        public void Clear()
        {
            this.IndexList.Clear();
            if (File.Exists(this.mIndexFile))
            {
                File.Delete(this.mIndexFile);
            }
            if (File.Exists(this.mDataFile))
            {
                File.Delete(this.mDataFile);
            }
        }

        private byte[] Compress(byte[] buffer, int offset, int size)
        {
            byte[] buffer2;
            MemoryStream stream = new MemoryStream();
            try
            {
                GZipStream stream2 = new GZipStream(stream, CompressionMode.Compress);
                try
                {
                    stream2.Write(buffer, offset, size);
                }
                finally
                {
                    stream2.Close();
                }
            }
            finally
            {
                buffer2 = stream.ToArray();
                stream.Close();
            }
            return buffer2;
        }

        private void CreateIndexList()
        {
            this.IndexList = new List<IndexRec>();
            if (File.Exists(this.mIndexFile))
            {
                string str;
                StreamReader reader = new StreamReader(this.mIndexFile);
                while ((str = reader.ReadLine()) != null)
                {
                    string[] strArray = str.Split(new char[] { ',' });
                    if (strArray.Length > 1)
                    {
                        try
                        {
                            IndexRec item = new IndexRec {
                                Offset = long.Parse(strArray[0].Trim()),
                                Size = long.Parse(strArray[1].Trim())
                            };
                            this.IndexList.Add(item);
                            continue;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                reader.Close();
            }
        }

        private byte[] Decompress(byte[] buffer, long offset, long size)
        {
            byte[] buffer2=new byte[0x3e8];
            MemoryStream stream = new MemoryStream();
            MemoryStream stream2 = new MemoryStream();
            try
            {
                stream.Write(buffer, (int) offset, (int) size);
                stream.Position = 0L;
                GZipStream stream3 = new GZipStream(stream, CompressionMode.Decompress);
                try
                {
                    byte[] buffer3 = new byte[0x3e8];
                    long num = 0L;
                    while (true)
                    {
                        int count = stream3.Read(buffer3, 0, 0x3e8);
                        if (count == 0)
                        {
                            return buffer2;
                        }
                        num += count;
                        stream2.SetLength(num);
                        stream2.Write(buffer3, 0, count);
                    }
                }
                finally
                {
                    stream3.Close();
                }
            }
            finally
            {
                stream.Close();
                buffer2 = stream2.ToArray();
                stream2.Close();
            }
            return buffer2;
        }

        public string GetData(int index)
        {
            byte[] buffer = this.ReadDataFile(this.mDataFile, this.IndexList[index].Offset, this.IndexList[index].Size);
            byte[] bytes = this.Decompress(buffer, 0L, (long) buffer.Length);
            return Encoding.Unicode.GetString(bytes);
        }

        public long LastOffset()
        {
            long offset = 0L;
            if (this.IndexList.Count > 0)
            {
                offset = this.IndexList[this.IndexList.Count - 1].Offset;
            }
            return offset;
        }

        public long LastSize()
        {
            long size = 0L;
            if (this.IndexList.Count > 0)
            {
                size = this.IndexList[this.IndexList.Count - 1].Size;
            }
            return size;
        }

        private byte[] ReadDataFile(string path, long offset, long size)
        {
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] buffer = new byte[size];
            stream.Seek(offset, SeekOrigin.Begin);
            stream.Read(buffer, 0, (int) size);
            stream.Close();
            return buffer;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct IndexRec
        {
            public long Offset;
            public long Size;
        }
    }
}

