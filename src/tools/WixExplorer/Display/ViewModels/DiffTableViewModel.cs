using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
            // generate the data for the table
            GenerateData();

            DataGrid grid = control.Details;
            grid.ItemsSource = Details;
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
                gridColumn.Binding = new Binding(string.Format("Row.Fields[{0}].Data", i));
                grid.Columns.Add(gridColumn);
            }

            // now fix the width of the last column
            DataGridColumn lastColumn = grid.Columns[columnDefs.Count - 1];
            lastColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Auto);
            lastColumn.MinWidth = lastColumn.ActualWidth;
            lastColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void GenerateData()
        {
            int numColumns;
            if (null != LeftTable)
            {
                numColumns = LeftTable.Definition.Columns.Count;
            }
            else
            {
                numColumns = RightTable.Definition.Columns.Count;
            }

            // allocate storage for data
            Details = new ObservableCollection<ItemDetail>();

            // preallocate color lists
            List<string> deletedRowColors = new List<string>(numColumns);
            for (int i = 0; i < numColumns; ++i)
            {
                deletedRowColors.Add("lightRed");
            }
            List<string> addedRowColors = new List<string>(numColumns);
            for (int i = 0; i < numColumns; ++i)
            {
                addedRowColors.Add("lightBule");
            }
            List<string> matchRowColors = new List<string>(numColumns);
            for (int i = 0; i < numColumns; ++i)
            {
                matchRowColors.Add("white");
            }

            bool nextLeft = false;
            Row[] leftRows = null;
            IEnumerator<Row> enumLeft = null;
            if (null != LeftTable)
            {
                leftRows = new Row[LeftTable.Rows.Count];
                LeftTable.Rows.CopyTo(leftRows, 0);
                enumLeft = leftRows.OrderBy(r => r, new RowComparer()).GetEnumerator();
                nextLeft = enumLeft.MoveNext();
            }

            bool nextRight = false;
            Row[] rightRows = null;
            IEnumerator<Row> enumRight = null;
            if (null != RightTable)
            {
                rightRows = new Row[RightTable.Rows.Count];
                RightTable.Rows.CopyTo(rightRows, 0);
                enumRight = rightRows.OrderBy(r => r, new RowComparer()).GetEnumerator();
                nextRight = enumRight.MoveNext();
            }

            while (nextLeft || nextRight)
            {
                // we have both items, compare them
                Row leftRow = nextLeft ? enumLeft.Current : null;
                Row rightRow = nextRight ? enumRight.Current : null;

                // if the left is null, the right is greater
                if (null == leftRow)
                {
                    Details.Add(new RowItemDetail(rightRow, addedRowColors));
                    nextRight = enumRight.MoveNext();
                    continue;
                }

                // this compares the primary keys in the two rows
                int compare = leftRow.CompareTo(rightRow);
                if (compare < 0)
                {
                    // left is smaller, the row was deleted
                    Details.Add(new RowItemDetail(leftRow, deletedRowColors));
                    nextLeft = enumLeft.MoveNext();
                }
                else if (compare == 0)
                {
                    // the primary keys match, does everything match?
                    List<string> leftColors = new List<string>(numColumns);
                    List<string> rightColors = new List<string>(numColumns);
                    bool fieldDifference = false;
                    for (int i = 0; i < numColumns; ++i)
                    {
                        if (0 == leftRow.Fields[i].CompareTo(rightRow.Fields[i]))
                        {
                            // these fields match so the cels get the pale colors
                            leftColors.Add("lightRed");
                            rightColors.Add("lightBlue");
                        }
                        else
                        {
                            // fields are different so the cels get the dark colors
                            leftColors.Add("darkRed");
                            rightColors.Add("darkBlue");
                            fieldDifference = true;
                        }
                    }

                    if (fieldDifference)
                    {
                        // there is some difference, put in both rows with colored backgrounds
                        Details.Add(new RowItemDetail(leftRow, leftColors));
                        Details.Add(new RowItemDetail(rightRow, rightColors));
                    }
                    else
                    {
                        // everything matches, so just put in one row with white background
                        Details.Add(new RowItemDetail(leftRow, matchRowColors));
                    }

                    nextLeft = enumLeft.MoveNext();
                    nextRight = enumRight.MoveNext();
                }
                else
                {
                    // right is smaller, 
                    Details.Add(new RowItemDetail(rightRow, addedRowColors));
                    nextRight = enumRight.MoveNext();
                }
            }
        }
    }

}

