using System.Collections.ObjectModel;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class WixExplorerViewModel : MsiTreeViewItemViewModel
    {
        public WixExplorerViewModel()
            : base(null, null, false)
        {
        }

        public void AddMsiInfo(MsiInfo msiInfo)
        {
            MsiInfoViewModel viewModel = new MsiInfoViewModel(msiInfo);
            Children.Add(viewModel);
            viewModel.IsSelected = true;
            this.IsExpanded = true;
        }

        public override void Selected(MsiDisplayControl control)
        {
            control.Details.ItemsSource = null;
            CreateNameValueColumns(control.Details);
        }
    }
}
