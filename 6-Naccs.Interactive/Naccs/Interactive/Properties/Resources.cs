namespace Naccs.Interactive.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode]
    internal class Resources
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal Resources()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        internal static string INTR01
        {
            get
            {
                return ResourceManager.GetString("INTR01", resourceCulture);
            }
        }

        internal static string INTR02
        {
            get
            {
                return ResourceManager.GetString("INTR02", resourceCulture);
            }
        }

        internal static string INTR03
        {
            get
            {
                return ResourceManager.GetString("INTR03", resourceCulture);
            }
        }

        internal static string INTR04
        {
            get
            {
                return ResourceManager.GetString("INTR04", resourceCulture);
            }
        }

        internal static string INTR05
        {
            get
            {
                return ResourceManager.GetString("INTR05", resourceCulture);
            }
        }

        internal static string INTR06
        {
            get
            {
                return ResourceManager.GetString("INTR06", resourceCulture);
            }
        }

        internal static string INTR07
        {
            get
            {
                return ResourceManager.GetString("INTR07", resourceCulture);
            }
        }

        internal static string INTR08
        {
            get
            {
                return ResourceManager.GetString("INTR08", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Naccs.Interactive.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }
    }
}

