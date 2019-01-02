using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Utilities
{
    /// <summary>
    /// Provides assembly information such as version info
    /// </summary>
    public class Assembly
    {
        static System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
        FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

        public String Name
        {
            get
            {
                return this.fvi.FileDescription;
            }
        }
        public String Version
        {
            get
            {
                return this.fvi.FileVersion;
            }
        }
        public String Company
        {
            get
            {
                return this.fvi.CompanyName;
            }
        }
        public String Copyright
        {
            get
            {
                return this.fvi.LegalCopyright;
            }
        }
        public String Description
        {
            get
            {
                return this.fvi.Comments;
            }
        }
        public String Path
        {
            get
            {
                return assembly.Location;
            }
        }
        public String Directory
        {
            get
            {
                return System.IO.Path.GetDirectoryName(assembly.Location);
            }
        }

        public String Guid
        {
            get
            {
                var attribute = (GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
                return attribute.Value;
            }
        }
    }
}
