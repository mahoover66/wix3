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

        protected IList<string> Colors { get; set; }

        public string FixedColor
        {
            get
            {
                return "red";
            }
        }

        public string GetColumnColor(int columnIndex)
        {
            if (null != Colors)
            {
                if (columnIndex < Colors.Count)
                {
                    return Colors[columnIndex];
                }
            }

            return "white";
        }
    }
}
