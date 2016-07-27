using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;

using Microsoft.Tools.WindowsInstallerXml;
using System.Collections.ObjectModel;
using System.Collections;

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

            // at least one of the tables will be defined, and if both are defined, the columns are the same
            ColumnDefinitionCollection columnDefs;
            if (null != LeftTable)
            {
                columnDefs = LeftTable.Definition.Columns;
            }
            else
            {
                columnDefs = RightTable.Definition.Columns;
            }

            for (int i = 0; i < columnDefs.Count; i++)
            {
                ColumnDefinition column = columnDefs[i];

                DataGridTextColumn gridColumn = new DataGridTextColumn();
                gridColumn.Header = column.Name;
                gridColumn.MinWidth = 15;
                gridColumn.Binding = new Binding(string.Format("Fields[{0}].Data", i));
                grid.Columns.Add(gridColumn);
            }

            // now fix the width of the last column
            DataGridColumn lastColumn = grid.Columns[columnDefs.Count - 1];
            lastColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Auto);
            lastColumn.MinWidth = lastColumn.ActualWidth;
            lastColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

            // now generate the data
        }

        protected void DiffRows()
        {
            Details = new ObservableCollection<ItemDetail>();

            IEnumerator enumLeft = LeftTable.Rows.GetEnumerator();
            IEnumerator enumRight = RightTable.Rows.GetEnumerator();

            bool nextLeft = enumLeft.MoveNext();
            bool nextRight = enumRight.MoveNext();

            while (nextLeft || nextRight)
            {
                // we have both items, compare them
                Row left = nextLeft ? (Row)enumLeft.Current : null;
                Row right = nextRight ? (Row)enumRight.Current : null;
                
                // if the left is null, the right is greater
                if (null == left)
                {
                    RowItemDetail rowItemDetail = new RowItemDetail(right, null);
                    Details.Add(rowItemDetail);
                    nextRight = enumRight.MoveNext();
                    continue;
                }

                // this compares the table name then the columns
                int compare = left.CompareTo(right);
                if (compare < 0)
                {
                    // left is smaller, only add it
                    AddChildTable(left, null);
                    nextLeft = enumLeft.MoveNext();
                }
                else if (compare == 0)
                {
                    // they have the same name, use them both
                    AddChildTable(left, right);
                    nextLeft = enumLeft.MoveNext();
                    nextRight = enumRight.MoveNext();
                }
                else
                {
                    // right is smaller, only add it
                    AddChildTable(null, right);
                    nextRight = enumRight.MoveNext();
                }
            }
        }
    }

}
}
