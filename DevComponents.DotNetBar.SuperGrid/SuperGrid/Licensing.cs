using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DevComponents.DotNetBar.SuperGrid
{
    internal class NativeFunctions
    {
        #region Licensing
#if !TRIAL
        internal static bool keyValidated = false;
        internal static int keyValidated2 = 0;
        internal static bool ValidateLicenseKey(string key)
        {
            bool ret = false;
            string[] parts = key.Split('-');
            int i = 10;
            foreach (string s in parts)
            {
                if (s == "88405280")
                    i++;
                else if (s == "D06E")
                    i += 10;
                else if (s == "4617")
                    i += 8;
                else if (s == "8810")
                    i += 12;
                else if (s == "64462F60FA93")
                    i += 3;
            }
            if (i == 29)
                return true;
            keyValidated = true;
            return ret;
        }
        internal static bool CheckLicenseKey(string key)
        {
            // F962CEC7-CD8F-4911-A9E9-CAB39962FC1F, 189, 266
            string[] parts = key.Split('-');
            int test = 0;
            for (int i = parts.Length - 1; i >= 0; i--)
            {
                if (parts[i] == "A9E9")
                    test += 11;
                else if (parts[i] == "F962CEC7")
                    test += 12;
                else if (parts[i] == "CAB39962FC1F")
                    test += 2;
                else if (parts[i] == "4911")
                    test += 99;
                else if (parts[i] == "CD8F")
                    test += 65;
            }

            keyValidated2 = test + 77;

            if (test == 23)
                return false;

            return true;
        }
#endif
        #endregion

#if TRIAL
		private static Color m_ColorExpFlag=Color.Empty;
        internal static bool CheckedThrough = false;
		internal static bool ColorExpAlt()
		{
#if NOTIMELIMIT
				return false;
#else
			Color clr=SystemColors.Control;
			Color clr2;
			Color clr3;
			clr2=clr;
			if(clr2.ToArgb()==clr.ToArgb())
			{
				clr3=clr2;
			}
			else
			{
				clr3=clr;
			}

			if(!m_ColorExpFlag.IsEmpty)
			{
				return (m_ColorExpFlag==Color.Black?false:true);
			}
			try
			{
				Microsoft.Win32.RegistryKey key=Microsoft.Win32.Registry.ClassesRoot;
                try
                {
                    key = key.CreateSubKey("CLSID\\{AC49A37B-FD89-4763-AB1B-C6DF6709657F}\\InprocServer32");
                }
                catch (System.UnauthorizedAccessException)
                {
                    key = key.OpenSubKey("CLSID\\{AC49A37B-FD89-4763-AB1B-C6DF6709657F}\\InprocServer32");
                }
				try
				{
					if(key.GetValue("")==null || key.GetValue("").ToString()=="")
					{
						key.SetValue("",DateTime.Today.ToOADate().ToString());
					}
					else
					{
						if(key.GetValue("").ToString()=="windows3.dll")
						{
							m_ColorExpFlag=Color.White;
							key.Close();
							key=null;
							return true;
						}
						DateTime date=DateTime.FromOADate(double.Parse(key.GetValue("").ToString()));
						if(((TimeSpan)DateTime.Today.Subtract(date)).TotalDays>28)
						{
							m_ColorExpFlag=Color.White;
							key.SetValue("","windows4.dll");
							key.Close();
							key=null;
							return true;
						}
						if(((TimeSpan)DateTime.Today.Subtract(date)).TotalDays<0)
						{
							m_ColorExpFlag=Color.White;
							key.SetValue("","windows3.dll");
							key.Close();
							key=null;
							return true;
						}
					}
				}
				finally
				{
					if(key!=null)
						key.Close();
                    CheckedThrough = true;
				}
			}
			catch{}
			m_ColorExpFlag=Color.Black;
			return false;
#endif
		}
#endif
    }
}
