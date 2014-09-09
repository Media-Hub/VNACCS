namespace Naccs.Net
{
    using System;

    public enum MailCause
    {
        None,
        SmtpDnsException,
        SmtpErrorResponse,
        SmtpException,
        PopDnsException,
        PopErrorResponse,
        PopException,
        Exception,
        Timeout,
        UserCancel,
        NotSupport,
        ProgramError
    }
}

