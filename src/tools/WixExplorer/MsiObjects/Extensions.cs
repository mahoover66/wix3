using System;
using System.Collections.Generic;
using Microsoft.Tools.WindowsInstallerXml;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public static class Extensions
    {
        public static int FindFieldIndex(this ColumnDefinitionCollection columns, string name)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                if (columns[i].Name.Equals(name, StringComparison.Ordinal))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
