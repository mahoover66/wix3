using System.Linq;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class DirectoryRootViewModel : MsiTreeViewItemViewModel
    {
        public DirectoryRootViewModel(WixPdbInfo WixPdbInfo)
            : base(WixPdbInfo, parent: null, lazyLoadChildren: true)
        {
        }

        protected override void LoadChildren()
        {
            foreach (string directory in WixPdbInfo.Directories.Keys.OrderBy(key => key))
            {
                DirectoryInfo directoryInfo = WixPdbInfo.Directories[directory];
                Children.Add(new DirectoryViewModel(WixPdbInfo, directoryInfo, this));
            }
        }
    }
}
