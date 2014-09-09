using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// FilterUserData
    ///</summary>
    static public class FilterUserData
    {
        #region Static data

        private static string _filterPath;
        private static List<UserFilterData> _filterData;

        #endregion

        static FilterUserData()
        {
            _filterPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                          "\\DotNetBar\\UserFilterData.xml";
        }

        #region GetFilterData

        ///<summary>
        /// GetFilterData
        ///</summary>
        ///<returns></returns>
        static public List<UserFilterData> GetFilterData(GridPanel gridPanel)
        {
            if (_filterData != null)
                return (_filterData);

            return (LoadFilterData(gridPanel));
        }

        #endregion

        #region LoadFilterData

        ///<summary>
        /// LoadFilterData
        ///</summary>
        ///<returns></returns>
        static public List<UserFilterData> LoadFilterData(GridPanel gridPanel)
        {
            _filterData = new List<UserFilterData>();

            string path = _filterPath;

            if (gridPanel.SuperGrid.DoFilterLoadUserDataEvent(
                gridPanel, ref path, ref _filterData) == false)
            {
                if (File.Exists(path) == true)
                {
                    using (XmlReader reader = XmlReader.Create(path))
                    {
                        UserFilterData fd = null;

                        while (reader.Read())
                        {
                            if (reader.IsStartElement())
                            {
                                switch (reader.Name)
                                {
                                    case "Filter":
                                        fd = new UserFilterData();
                                        _filterData.Add(fd);
                                        break;

                                    case "Name":
                                        if (reader.Read())
                                        {
                                            if (fd != null)
                                                fd.Name = reader.Value.Trim();
                                        }
                                        break;

                                    case "Description":
                                        if (reader.Read())
                                        {
                                            if (fd != null)
                                                fd.Description = reader.Value.Trim();
                                        }
                                        break;

                                    case "Expression":
                                        if (reader.Read())
                                        {
                                            if (fd != null)
                                                fd.Expression = reader.Value.Trim();
                                        }
                                        break;

                                    case "ReferNames":
                                        if (reader.Read())
                                        {
                                            if (fd != null)
                                                fd.ReferNamesString = reader.Value.Trim();
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            return (_filterData);
        }

        #endregion

        #region StoreFilterData

        ///<summary>
        /// StoreFilterData
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="filterData"></param>
        static public void StoreFilterData(GridPanel gridPanel, List<UserFilterData> filterData)
        {
            for (int i = filterData.Count - 1; i >= 0; --i)
            {
                UserFilterData fd = filterData[i];

                if (fd.Expression != null)
                {
                    string expr = fd.Expression.Trim();

                    if (expr.Length <= 0)
                        filterData.RemoveAt(i);
                }
            }

            string path = _filterPath;

            if (gridPanel.SuperGrid.DoFilterStoreUserDataEvent(
                gridPanel, ref path, ref _filterData) == false)
            {
                string dir = Path.GetDirectoryName(path);

                if (dir != null)
                {
                    if (Directory.Exists(dir) == false)
                        Directory.CreateDirectory(dir);

                    using (XmlWriter writer = XmlWriter.Create(path))
                    {
                        writer.WriteStartDocument();
                        writer.WriteStartElement("FilterData");

                        foreach (UserFilterData fd in filterData)
                        {
                            if (fd.Expression != null)
                            {
                                writer.WriteStartElement("Filter");

                                writer.WriteElementString("Name", fd.Name);
                                writer.WriteElementString("Description", fd.Description);
                                writer.WriteElementString("Expression", fd.Expression);

                                string names = fd.ReferNamesString;

                                if (names != string.Empty)
                                    writer.WriteElementString("ReferNames", names);

                                writer.WriteEndElement();
                            }
                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                    }
                }
            }
        }

        #endregion
    }

    ///<summary>
    /// User Filter expression data
    ///</summary>
    public class UserFilterData
    {
        #region Private variables

        private string _Name;
        private string _Description;
        private string _Expression;

        private List<string> _ReferNames = new List<string>();

        #endregion

        #region Public properties

        #region Description

        ///<summary>
        /// Description
        ///</summary>
        public string Description
        {
            get { return (_Description); }
            set { _Description = value; }
        }

        #endregion

        #region Expression

        ///<summary>
        /// Expression
        ///</summary>
        public string Expression
        {
            get { return (_Expression); }
            set { _Expression = value; }
        }

        #endregion

        #region Name

        ///<summary>
        /// Name
        ///</summary>
        public string Name
        {
            get { return (_Name); }
            set { _Name = value; }
        }

        #endregion

        #region ReferNames

        ///<summary>
        /// ReferNames
        ///</summary>
        public List<string> ReferNames
        {
            get { return (_ReferNames); }
        }

        #endregion

        #region ReferNamesString

        ///<summary>
        /// ReferNamesString
        ///</summary>
        public string ReferNamesString
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                foreach (string s in _ReferNames)
                {
                    sb.Append(s);
                    sb.Append(",");
                }

                if (sb.Length > 0)
                    sb.Length--;

                return (sb.ToString());
            }

            set
            {
                _ReferNames.Clear();

                if (value != null)
                {
                    string[] names = value.Split(',');

                    foreach (string s in names)
                        _ReferNames.Add(s);
                }
            }
        }

        #endregion

        #endregion
    }
}

