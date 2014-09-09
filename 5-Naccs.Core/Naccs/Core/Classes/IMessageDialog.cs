namespace Naccs.Core.Classes
{
    using System;
    using System.Windows.Forms;

    public interface IMessageDialog
    {
        DialogResult ShowJobMessage(string jobCode, string resultCode, string internalCode);
        DialogResult ShowMessage(string messageCode, string internalCode);
        DialogResult ShowMessage(ButtonPatern buttonPatern, MessageKind messageKind, string message);
        DialogResult ShowMessage(string messageCode, string applyStr, string internalCode);
        DialogResult ShowMessage(ButtonPatern buttonPatern, MessageKind messageKind, string message, bool logout, bool autosize);
        DialogResult ShowMessage(ButtonPatern buttonPatern, MessageKind messageKind, string message, bool logout, bool autosize, MessageBoxDefaultButton defaultButton);
    }
}

