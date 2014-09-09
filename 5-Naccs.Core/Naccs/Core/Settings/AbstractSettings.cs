namespace Naccs.Core.Settings
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    public class AbstractSettings
    {
        protected bool updateFlg;

        public AbstractSettings()
        {
            this.Initialize();
        }

        public virtual void Initialize()
        {
            this.resetProperties(this);
        }

        protected void resetProperties(object o)
        {
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(o))
            {
                if (descriptor.CanResetValue(o))
                {
                    descriptor.ResetValue(o);
                }
                this.resetProperties(descriptor.GetValue(o));
            }
        }

        [XmlIgnore]
        public virtual bool UpdateFlg
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

