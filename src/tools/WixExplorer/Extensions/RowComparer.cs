using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class RowComparer : IComparer<Row>
    {
        public int Compare(Row x, Row y)
        {
            if (null == x)
            {
                return 1;
            }

            return x.CompareTo(y);
        }
    }
}
