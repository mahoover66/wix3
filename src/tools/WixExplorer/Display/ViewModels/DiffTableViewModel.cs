using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;

using Microsoft.Tools.WindowsInstallerXml;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class DiffTableViewModel : MsiTreeViewItemViewModel
    {
        public DiffTableViewModel(WixPdbInfo leftWixPdbInfo, Table leftTable, WixPdbInfo rightWixPdbInfo, Table rightTable, MsiTreeViewItemViewModel parent)
            : base(wixPdbInfo: null, parent: parent, lazyLoadChildren: false)
        {
            LeftWixPdbInfo = leftWixPdbInfo;
            LeftTable = leftTable;
            RightWixPdbInfo = rightWixPdbInfo;
            RightTable = rightTable;
        }

        public WixPdbInfo LeftWixPdbInfo { get; protected set; }
        public Table LeftTable { get; protected set; }
        public WixPdbInfo RightWixPdbInfo { get; protected set; }
        public Table RightTable { get; protected set; }

        public string TableName
        {
            get
            {
                // one of the tables is guarenteed to be non null, and if they
                // are both non null, the names will match.
                if (LeftTable != null)
                {
                    return LeftTable.Name;
                }
                else
                {
                    return RightTable.Name;
                }
            }
        }

        public override void Selected(MsiDisplayControl control)
        {
            DataGrid grid = control.Details;

            grid.Columns.Clear();
        }
    }
}
