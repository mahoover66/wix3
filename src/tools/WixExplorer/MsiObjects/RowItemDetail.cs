using System;
using Microsoft.Tools.WindowsInstallerXml;
using System.Collections.Generic;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class RowItemDetail : ItemDetail
    {
        public RowItemDetail(Row row, IList<string> colors) 
            : base(colors)
        {
            Row = row;
        }

        public Row Row { get; protected set; }
    }
}
