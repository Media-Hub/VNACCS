namespace Naccs.Core.DataView
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate void OnUpdateDataViewItemsHandler(object sender, bool FldCreate, bool FldChange, bool FldDelete, bool DDelete, bool DUndo, bool SSearch, bool RSearch, bool Export);
}

