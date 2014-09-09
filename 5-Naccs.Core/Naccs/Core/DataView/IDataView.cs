namespace Naccs.Core.DataView
{
    using Naccs.Core.Classes;
    using Naccs.Core.Settings;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public interface IDataView
    {
        event OnJobFormOpenHandler OnJobFormOpen;

        event OnPrintJobHandler OnPrintJob;

        event OnUpdateDataViewItemsHandler OnUpdateDataViewItems;

        void AllSelect();
        string AppendJobData(IData Data, int Status, bool Flag, bool bkFlag, bool TMFlag);
        void AppendRecord();
        void AppendRecord(IData data, string FolderStr);
        bool CheckFile(IData data);
        void ClearDelete();
        void DataDelete(int DNInt);
        void DataExport();
        void DataImport();
        void DataUndo();
        void DataViewFontChange(int sizeflag);
        bool DataViewListSave(string Path);
        void DataViewNoRepaint(bool ViewFlag);
        void DataViewRepaint(bool SelectFlag);
        void DataViewSaveRepaint();
        void Distribute(IData data);
        void Distribute(bool SelFlag);
        void Distribute(bool SelFlag, IData data);
        void FolderChangeName();
        void FolderCreate();
        void FolderDelete();
        int GetCount(int Status);
        int GetCount(int Status, int SaveDate);
        IData GetData(int key);
        ColumnWidthClass GetDataViewColum();
        string GetDataViewListData();
        IData GetDivData(string DataInf, int Flag, string JCode, string OCode);
        StringList GetFldList();
        int GetKeyID();
        List<string> GetSelectKeyList();
        int GetSendKey(bool flg = true);
        int GetSplitter();
        void JobOpen();
        void OpenStatusChange(string key);
        void PrintStatusChange(string key);
        void RecordDelete(int key);
        void RecvFolderSelect();
        void RecvSearch();
        void SaveAsFile();
        void SaveDateDataDelete(int SaveDate);
        void SaveStatusChange(string key, bool Flag);
        void SendSearch();
        void SendStatusChange(string sendkey);
        void SetDataViewColum(ColumnWidthClass usr_cwc);
        void SetMsgc();
        void SetOpenViewFlag(bool Flag);
        void SetSplitter(int SplInt);
        void SetUserClass(bool DebugFlag, bool ReloadFlag);
        bool sExistenceCheck();
        bool SWResCheck(string OutCode, string DataInf);
        void UpdateCount();
        string UpdateJobData(IData Data);
        void UpdDataViewMainItems(bool ViewRefresh, bool Import, bool Export, bool DemoFlag);
        void ViewCountCheck(bool flag);
        void ViewRefresh();
        void ViewRepair(bool StartFlag);
        void ViewRestore();
        bool ViewRevert(string path);

        DataGridView InternalDataGrid { get; }

        DataSet InternalDataSet { get; }
    }
}

