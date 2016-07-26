namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class TableRootViewModel : MsiTreeViewItemViewModel
    {
        public TableRootViewModel(WixPdbInfo WixPdbInfo)
            : base(WixPdbInfo, parent: null, lazyLoadChildren: true)
        {
        }

        protected override void LoadChildren()
        {
            foreach (Table table in WixPdbInfo.Tables)
            {
                Children.Add(new TableViewModel(WixPdbInfo, table, this));
            }
        }
    }
}
