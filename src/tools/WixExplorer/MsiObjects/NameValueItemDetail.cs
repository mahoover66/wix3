using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    class NameValueItemDetail : ItemDetail
    {
        public NameValueItemDetail(string name, string value, IList<string> colors)
            : base(colors)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; protected set; }
        public string Value { get; protected set; }
    }
}
