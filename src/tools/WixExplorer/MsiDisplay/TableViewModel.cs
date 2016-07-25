using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;

using Microsoft.Tools.WindowsInstallerXml;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class TableViewModel : MsiTreeViewItemViewModel
    {
        public TableViewModel(MsiInfo msiInfo, Table table, MsiTreeViewItemViewModel parent, bool lazyLoadChildren)
            : base(msiInfo, parent, lazyLoadChildren)
        {
            Table = table;
        }

        public Table Table { get; protected set; }

        public string TableName
        {
            get
            {
                return Table.Name;
            }
        }

        public override void Selected(MsiDisplayControl control)
        {
            DataGrid grid = control.Details;

            grid.ItemsSource = Table.Rows;

            grid.Columns.Clear();

            Microsoft.Tools.WindowsInstallerXml.ColumnDefinitionCollection columnDefs = Table.Definition.Columns;
            for (int i = 0; i < columnDefs.Count; i++)
            {
                Microsoft.Tools.WindowsInstallerXml.ColumnDefinition column = columnDefs[i];

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
        }
    }
}
