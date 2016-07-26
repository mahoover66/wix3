using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class MsiTreeViewItemDetail
    {
        public MsiTreeViewItemDetail(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; protected set; }
        public string Value { get; protected set; }
    }
}
