namespace Naccs.Core.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class JobKeyListClass : List<JobKeyClass>
    {
        private Dictionary<string, JobKeyClass> JobKeyTable = new Dictionary<string, JobKeyClass>();
        private bool updateFlg;

        public void Add(JobKeyClass item)
        {
            JobKeyClass class2 = (JobKeyClass) item.Clone();
            base.Add(class2);
            try
            {
                this.JobKeyTable.Add(item.Kind.ToString(), class2);
                this.updateFlg = true;
                item.OnChange += new EventHandler(this.item_OnChange);
                class2.OnChange += new EventHandler(this.item_OnChange);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Duplicated key." + exception.Message);
                throw exception;
            }
        }

        public void Clear()
        {
            this.updateFlg = base.Count > 0;
            base.Clear();
            this.JobKeyTable.Clear();
        }

        public JobKeyClass getJobKey(string name)
        {
            try
            {
                return this.JobKeyTable[name];
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void item_OnChange(object sender, EventArgs e)
        {
            this.updateFlg = true;
        }

        public void Remove(JobKeyClass item)
        {
            this.JobKeyTable.Remove(item.Kind.ToString());
            base.Remove(item);
            this.updateFlg = true;
        }

        public void RemoveAt(int index)
        {
            JobKeyClass item = base[index];
            this.Remove(item);
        }

        [XmlIgnore]
        public bool UpdateFlg
        {
            get
            {
                return this.updateFlg;
            }
            set
            {
                this.updateFlg = value;
            }
        }
    }
}

