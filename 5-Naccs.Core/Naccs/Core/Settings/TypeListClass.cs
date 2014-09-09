namespace Naccs.Core.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class TypeListClass : List<TypeClass>
    {
        private Dictionary<string, TypeClass> TypeTable = new Dictionary<string, TypeClass>();
        private bool updateFlg;

        public void Add(TypeClass type)
        {
            TypeClass item = (TypeClass) type.Clone();
            base.Add(item);
            try
            {
                this.TypeTable.Add(type.DataType, item);
                this.updateFlg = true;
                type.OnChange += new EventHandler(this.type_OnChange);
                item.OnChange += new EventHandler(this.type_OnChange);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Duplicated key." + exception.Message);
                throw exception;
            }
        }

        public void Clear()
        {
            base.Clear();
            this.TypeTable.Clear();
        }

        public TypeClass getType(string datatype)
        {
            try
            {
                return this.TypeTable[datatype];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Remove(TypeClass type)
        {
            base.Remove(type);
            this.TypeTable.Remove(type.DataType);
            this.updateFlg = true;
        }

        public void RemoveAt(int index)
        {
            TypeClass type = base[index];
            this.Remove(type);
        }

        public void type_OnChange(object sender, EventArgs e)
        {
            this.updateFlg = true;
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

