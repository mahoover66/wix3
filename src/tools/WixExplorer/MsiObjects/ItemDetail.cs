using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class ItemDetail
    {
        public ItemDetail(IList<string> colors)
        {
            Colors = colors;
        }

        public IList<string> Colors { get; protected set; }
    }
}
