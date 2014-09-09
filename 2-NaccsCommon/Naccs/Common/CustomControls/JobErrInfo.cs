namespace Naccs.Common.CustomControls
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class JobErrInfo
    {
        private StructErr InErrInfo;
        public const int RecCount = 6;
        private StructErr[] ResErrInfo = new StructErr[5];

        public JobErrInfo()
        {
            this.Clear();
        }

        public bool CheckErrInfo(string stErrID, int iErrPage, string stData)
        {
            if (((this.InErrInfo.ErrID == stErrID) && (this.InErrInfo.ErrPage == iErrPage)) && (this.InErrInfo.ErrData == stData))
            {
                return true;
            }
            for (int i = 0; i < this.ResErrInfo.Length; i++)
            {
                if (((this.ResErrInfo[i].ErrID == stErrID) && (this.ResErrInfo[i].ErrPage == iErrPage)) && (this.ResErrInfo[i].ErrData == stData))
                {
                    return true;
                }
            }
            if (stErrID[stErrID.Length - 1] != '_')
            {
                return false;
            }
            string str = iErrPage.ToString();
            StringBuilder builder = new StringBuilder();
            builder.Append(stErrID.ToCharArray());
            builder = builder.Replace('_', '0');
            int num2 = 1;
            for (int j = str.Length - 1; j > -1; j--)
            {
                builder[builder.Length - num2] = str[j];
                num2++;
            }
            return this.CheckErrInfo(builder.ToString(), 0, stData);
        }

        public bool CheckTrimErrInfo(string stErrID, int iErrPage, string stData)
        {
            string trimData = this.GetTrimData(stData);
            if (((this.InErrInfo.ErrID == stErrID) && (this.InErrInfo.ErrPage == iErrPage)) && (this.GetTrimData(this.InErrInfo.ErrData) == trimData))
            {
                return true;
            }
            for (int i = 0; i < this.ResErrInfo.Length; i++)
            {
                if (((this.ResErrInfo[i].ErrID == stErrID) && (this.ResErrInfo[i].ErrPage == iErrPage)) && (this.GetTrimData(this.ResErrInfo[i].ErrData) == trimData))
                {
                    return true;
                }
            }
            if (stErrID[stErrID.Length - 1] != '_')
            {
                return false;
            }
            string str2 = iErrPage.ToString();
            StringBuilder builder = new StringBuilder();
            builder.Append(stErrID.ToCharArray());
            builder = builder.Replace('_', '0');
            int num2 = 1;
            for (int j = str2.Length - 1; j > -1; j--)
            {
                builder[builder.Length - num2] = str2[j];
                num2++;
            }
            return this.CheckTrimErrInfo(builder.ToString(), 0, stData);
        }

        public void Clear()
        {
            for (int i = 0; i < this.ResErrInfo.Length; i++)
            {
                this.ResErrInfo[i].ErrID = "";
                this.ResErrInfo[i].ErrPage = 0;
                this.ResErrInfo[i].ErrData = "";
            }
            this.ClearInputErrorInfo();
        }

        public void ClearInputErrorInfo()
        {
            this.InErrInfo.ErrID = "";
            this.InErrInfo.ErrPage = 0;
            this.InErrInfo.ErrData = "";
        }

        public StructErr GetErrInfo(int idx)
        {
            if (idx == 0)
            {
                return this.InErrInfo;
            }
            if ((1 <= idx) && (idx <= 5))
            {
                return this.ResErrInfo[idx - 1];
            }
            return new StructErr();
        }

        private string GetTrimData(string s)
        {
            return s.TrimEnd(null);
        }

        public void SetErrInfo(string stErrID, int iErrPage, string stData)
        {
            int index = -1;
            for (int i = this.ResErrInfo.Length - 1; i >= 0; i--)
            {
                if ((this.ResErrInfo[i].ErrID == stErrID) && (this.ResErrInfo[i].ErrPage == iErrPage))
                {
                    return;
                }
                if ((this.ResErrInfo[i].ErrID == "") && (this.ResErrInfo[i].ErrPage == 0))
                {
                    index = i;
                }
            }
            if (index > -1)
            {
                this.ResErrInfo[index].ErrID = stErrID;
                this.ResErrInfo[index].ErrPage = iErrPage;
                this.ResErrInfo[index].ErrData = stData;
            }
        }

        public void SetInputErrInfo(string stErrID, int iErrPage, string stData)
        {
            this.InErrInfo.ErrID = stErrID;
            this.InErrInfo.ErrPage = iErrPage;
            this.InErrInfo.ErrData = stData;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct StructErr
        {
            public string ErrID;
            public int ErrPage;
            public string ErrData;
        }
    }
}

