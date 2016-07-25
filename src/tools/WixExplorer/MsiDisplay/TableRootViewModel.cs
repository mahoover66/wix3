using Microsoft.Tools.WindowsInstallerXml;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class TableRootViewModel : MsiTreeViewItemViewModel
    {
        public TableRootViewModel(MsiInfo msiInfo, bool lazyLoadChildren)
            : base(msiInfo, null, lazyLoadChildren)
        {
        }

        protected override void LoadChildren()
        {
            foreach (Table table in MsiInfo.Tables)
            {
                Children.Add(new TableViewModel(MsiInfo, table, this, LazyLoadChildren));
            }
        }
    }
}
