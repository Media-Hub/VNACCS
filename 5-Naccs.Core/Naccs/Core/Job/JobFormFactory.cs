namespace Naccs.Core.Job
{
    using Naccs.Common.JobConfig;
    using Naccs.Core.Classes;
    using System;

    public class JobFormFactory
    {
        private static int errCode;

        public static CommonJobForm CreateEmptyForm()
        {
            try
            {
                errCode = 0;
                return new JobForm();
            }
            catch
            {
                errCode = -99;
                return null;
            }
        }

        public static CommonJobForm CreateNewForm(string jobCode, string kind, bool flgNew, bool flgJobTitle)
        {
            try
            {
                AbstractJobConfig config;
                CommonJobForm form = null;
                errCode = 0;
                if (kind != "")
                {
                    errCode = JobConfigFactory.createJobConfig(jobCode, kind, DateTime.Now, out config);
                }
                else
                {
                    errCode = JobConfigFactory.createJobConfig(jobCode, DateTime.Now, out config);
                }
                if (errCode >= 0)
                {
                    switch (config.jobStyle)
                    {
                        case JobStyle.normal:
                            form = new JobForm();
                            break;

                        case JobStyle.combi:
                            form = new CombiJobForm();
                            break;

                        case JobStyle.divide:
                            form = new DivideJobForm();
                            break;
                    }
                    errCode = form.CreateNewForm(jobCode, kind, flgNew, flgJobTitle);
                    if (errCode < 0)
                    {
                        return null;
                    }
                }
                return form;
            }
            catch
            {
                errCode = -99;
                return null;
            }
        }

        public static CommonJobForm CreateRecvForm(IData Data, bool flgJobTitle)
        {
            try
            {
                AbstractJobConfig config;
                string outCode = Data.OutCode;
                CommonJobForm form = null;
                errCode = 0;
                errCode = JobConfigFactory.createJobConfig(outCode, out config);
                if (errCode >= 0)
                {
                    switch (config.jobStyle)
                    {
                        case JobStyle.normal:
                            form = new JobForm();
                            break;

                        case JobStyle.combi:
                            form = new CombiJobForm();
                            break;

                        case JobStyle.divide:
                            form = new DivideJobForm();
                            break;
                    }
                    errCode = form.CreateRecvForm(Data, flgJobTitle);
                    if (errCode < 0)
                    {
                        return null;
                    }
                }
                return form;
            }
            catch
            {
                errCode = -99;
                return null;
            }
        }

        public static string GetMsgCode(int errint)
        {
            switch (errint)
            {
                case -12:
                    return "E512";

                case -11:
                    return "E511";

                case -9:
                    return "E509";

                case -8:
                    return "E508";

                case -7:
                    return "E507";

                case -6:
                    return "E506";

                case -5:
                    return "E505";

                case -4:
                    return "E504";

                case -3:
                    return "E503";

                case -2:
                    return "E502";

                case -1:
                    return "E501";
            }
            return "E510";
        }

        public static int ErrorCode
        {
            get
            {
                return errCode;
            }
            set
            {
                errCode = value;
            }
        }
    }
}

