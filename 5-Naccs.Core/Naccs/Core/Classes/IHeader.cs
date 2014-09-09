namespace Naccs.Core.Classes
{
    using System;

    public interface IHeader
    {
        string Control { get; set; }

        string DataInfo { get; set; }

        string DataString { get; set; }

        string DataType { get; set; }

        string DispCode { get; set; }

        string Div { get; set; }

        string EndFlag { get; set; }

        string IndexInfo { get; set; }

        string InputInfo { get; set; }

        string JobCode { get; set; }

        int Length { get; set; }

        string MailAddress { get; set; }

        string MaskedString { get; }

        string OutCode { get; set; }

        string Password { get; set; }

        string Path { get; set; }

        string Pattern { get; set; }

        string RtpInfo { get; set; }

        string SendGuard { get; set; }

        string ServerInfo { get; set; }

        string ServerRecvTime { get; set; }

        string Subject { get; set; }

        string System { get; set; }

        string UserId { get; set; }
    }
}

