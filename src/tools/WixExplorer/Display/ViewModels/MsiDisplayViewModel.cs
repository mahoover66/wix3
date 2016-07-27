using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class MsiDisplayViewModel
    {
        public MsiDisplayViewModel()
        {
            WixExplorer = new WixExplorerViewModel();
            Node = new ReadOnlyCollection<WixExplorerViewModel>(
                new WixExplorerViewModel[] { WixExplorer });

            Msis = new List<WixPdbInfo>();
        }

        public List<WixPdbInfo> Msis { get; protected set; }

        public WixExplorerViewModel WixExplorer { get; protected set; }
        public ReadOnlyCollection<WixExplorerViewModel> Node { get; protected set; }

        public void AddWixPdbInfo(WixPdbInfo WixPdbInfo)
        {
            Msis.Add(WixPdbInfo);
            WixExplorer.AddWixPdbInfo(WixPdbInfo);
        }

        internal void AddDiffView(DiffRootViewModel diffRootViewModel)
        {
            WixExplorer.Children.Add(diffRootViewModel);
        }
    }
}
