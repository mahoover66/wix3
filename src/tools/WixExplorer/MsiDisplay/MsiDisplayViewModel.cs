using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class MsiDisplayViewModel
    {
        public MsiDisplayViewModel()
        {
            WixExplorer = new WixExplorerViewModel();
            Node = new ReadOnlyCollection<WixExplorerViewModel>(
                new WixExplorerViewModel[] { WixExplorer });
        }

        public WixExplorerViewModel WixExplorer { get; protected set; }
        public ReadOnlyCollection<WixExplorerViewModel> Node { get; protected set; }
    }
}
