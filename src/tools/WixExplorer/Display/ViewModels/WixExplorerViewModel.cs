namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class WixExplorerViewModel : MsiTreeViewItemViewModel
    {
        public WixExplorerViewModel()
            : base(null, null, false)
        {
        }

        public void AddWixPdbInfo(WixPdbInfo WixPdbInfo)
        {
            WixPdbViewModel viewModel = new WixPdbViewModel(WixPdbInfo);
            Children.Add(viewModel);
            viewModel.IsSelected = true;
            this.IsExpanded = true;
        }

        public void AddDiffView(DiffRootViewModel diffRootView)
        {
            Children.Add(diffRootView);
            diffRootView.IsSelected = true;
            this.IsExpanded = true;
        }

        public override void Selected(MsiDisplayControl control)
        {
            control.Details.ItemsSource = null;
            CreateNameValueColumns(control.Details);
        }
    }
}
