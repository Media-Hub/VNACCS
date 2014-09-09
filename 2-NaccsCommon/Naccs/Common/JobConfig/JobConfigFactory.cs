namespace Naccs.Common.JobConfig
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Xml;

    public class JobConfigFactory
    {
        private static string guidePath;
        private static string gymErrPath;
        public static string lastError;
        private static string templatePath;

        public static int createJobConfig(string outcode, out AbstractJobConfig config)
        {
            int num;
            string str4;
            XmlDocument document2;
            config = null;
            string code = outcode.Substring(0, 6);
            if (!int.TryParse(outcode.Substring(6), out num))
            {
                return -10;
            }
            string dir = TemplatePath + @"recv\" + code + @"\";
            int num2 = dirCheck(dir, code, out str4);
            if (num2 != 0)
            {
                code = code.Substring(0, 4);
                dir = TemplatePath + @"recv\" + code + @"\";
                int num3 = dirCheck(dir, code, out str4);
                switch (num3)
                {
                    case 0:
                        goto Label_015D;

                    case -1:
                        return num2;
                }
                return num3;
            }
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(str4);
                XmlAttribute attribute = document.DocumentElement.Attributes["erase"];
                if ((attribute != null) && (attribute.Value.ToUpper() == "TRUE"))
                {
                    code = code.Substring(0, 4);
                    dir = TemplatePath + @"recv\" + code + @"\";
                    int num4 = dirCheck(dir, code, out str4);
                    switch (num4)
                    {
                        case 0:
                            goto Label_015D;

                        case -1:
                            return -8;
                    }
                    return num4;
                }
            }
            catch (XmlException exception)
            {
                lastError = exception.Message + "\t" + exception.StackTrace;
                return -4;
            }
            catch (Exception exception2)
            {
                lastError = exception2.Message + "\t" + exception2.StackTrace;
                return -99;
            }
        Label_015D:
            document2 = new XmlDocument();
            try
            {
                XmlNode node;
                document2.Load(str4);
                XmlAttribute attribute2 = document2.DocumentElement.Attributes["style"];
                if ((attribute2 == null) || (attribute2.Value == "normal"))
                {
                    NormalConfig config2 = new NormalConfig();
                    num2 = config2.setDocument(document2);
                    if (num2 == 0)
                    {
                        config2.setDirectories(dir, GymErrPath, GuidePath);
                        num2 = config2.selectRecord(num, out node);
                    }
                    config = config2;
                    return num2;
                }
                if ((attribute2.Value == "combi") || (attribute2.Value == "combination"))
                {
                    CombiConfig config3 = new CombiConfig();
                    num2 = config3.setDocument(document2);
                    if (num2 == 0)
                    {
                        config3.setDirectories(dir, GymErrPath, GuidePath);
                    }
                    config = config3;
                    return num2;
                }
                if (attribute2.Value == "divide")
                {
                    DivideConfig config4 = new DivideConfig();
                    num2 = config4.setDocument(document2);
                    if (num2 == 0)
                    {
                        config4.setDirectories(dir, GymErrPath, GuidePath);
                        num2 = config4.selectRecord(num, out node);
                    }
                    config = config4;
                    return num2;
                }
                return -6;
            }
            catch (XmlException exception3)
            {
                lastError = exception3.Message + "\t" + exception3.StackTrace;
                return -4;
            }
            catch (Exception exception4)
            {
                lastError = exception4.Message + "\t" + exception4.StackTrace;
                return -99;
            }
            return num2;
        }

        public static int createJobConfig(string jobcode, DateTime dateTime, out AbstractJobConfig config)
        {
            string str2;
            config = null;
            string dir = TemplatePath + @"send\";
            if (jobcode.Contains("."))
            {
                char[] separator = new char[] { '.' };
                string[] strArray = jobcode.Split(separator);
                string str3 = dir;
                dir = str3 + strArray[0] + @"\" + strArray[1] + @"\";
                str2 = strArray[0] + strArray[1];
            }
            else
            {
                dir = dir + jobcode + @"\";
                str2 = jobcode;
            }
            return loadJob(dir, str2, dateTime, out config);
        }

        public static int createJobConfig(string jobcode, string dispcode, DateTime dateTime, out AbstractJobConfig config)
        {
            config = null;
            string dir = TemplatePath + @"send\" + jobcode + @"\" + dispcode + @"\";
            string code = jobcode + dispcode;
            return loadJob(dir, code, dateTime, out config);
        }

        private static int dirCheck(string dir, string code, out string path)
        {
            path = dir + code + ".conf";
            if (!Directory.Exists(dir))
            {
                return -1;
            }
            if (File.Exists(path))
            {
                return 0;
            }
            string[] directories = Directory.GetDirectories(dir);
            if ((directories != null) && (directories.Length > 0))
            {
                return -3;
            }
            return -2;
        }

        private static string getAttribute(XmlNode node, string attrName)
        {
            if (node.Attributes[attrName] != null)
            {
                return node.Attributes[attrName].Value;
            }
            return "";
        }

        private static int loadJob(string dir, string code, DateTime dateTime, out AbstractJobConfig config)
        {
            string str;
            config = null;
            int num = dirCheck(dir, code, out str);
            if (num == 0)
            {
                XmlDocument doc = new XmlDocument();
                try
                {
                    XmlNode node;
                    doc.Load(str);
                    XmlAttribute attribute = doc.DocumentElement.Attributes["style"];
                    if (((attribute == null) || (attribute.Value == "")) || (attribute.Value == "normal"))
                    {
                        NormalConfig config2 = new NormalConfig();
                        num = config2.setDocument(doc);
                        if (num == 0)
                        {
                            config2.setDirectories(dir, GymErrPath, GuidePath);
                            num = config2.selectRecord(dateTime, out node);
                        }
                        config = config2;
                        return num;
                    }
                    if (attribute.Value == "combi")
                    {
                        CombiConfig config3 = new CombiConfig();
                        num = config3.setDocument(doc);
                        if (num == 0)
                        {
                            config3.setDirectories(dir, GymErrPath, GuidePath);
                            for (int i = 0; i < config3.count; i++)
                            {
                                AbstractJobConfig config4;
                                num = createJobConfig(config3.configs[i].code, dateTime, out config4);
                                config3.configs[i].config = config4 as NormalConfig;
                            }
                        }
                        config = config3;
                        return num;
                    }
                    if (attribute.Value == "divide")
                    {
                        DivideConfig config5 = new DivideConfig();
                        num = config5.setDocument(doc);
                        if (num == 0)
                        {
                            config5.setDirectories(dir, GymErrPath, GuidePath);
                            num = config5.selectRecord(dateTime, out node);
                        }
                        config = config5;
                        return num;
                    }
                    return -6;
                }
                catch (XmlException exception)
                {
                    lastError = exception.Message + "\t" + exception.StackTrace;
                    return -4;
                }
                catch (Exception exception2)
                {
                    lastError = exception2.Message + "\t" + exception2.StackTrace;
                    return -99;
                }
            }
            return num;
        }

        public static string GuidePath
        {
            get
            {
                return guidePath;
            }
            set
            {
                guidePath = value;
            }
        }

        public static string GymErrPath
        {
            get
            {
                return gymErrPath;
            }
            set
            {
                gymErrPath = value;
            }
        }

        public static string TemplatePath
        {
            get
            {
                return templatePath;
            }
            set
            {
                if (value.EndsWith(@"\"))
                {
                    templatePath = value;
                }
                else
                {
                    templatePath = value + @"\";
                }
            }
        }
    }
}

