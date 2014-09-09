namespace Naccs.Common.Function
{
    using System;
    using System.Diagnostics;

    public class DosCommand
    {
        public static string doCommand(string commandLine)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = Environment.GetEnvironmentVariable("ComSpec");
                startInfo.RedirectStandardInput = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                startInfo.Arguments = "/c " + commandLine;
                Process process = Process.Start(startInfo);
                string str = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                process.Close();
                return str;
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }
    }
}

