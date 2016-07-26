using System.Collections.ObjectModel;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class RegistryViewModel : MsiTreeViewItemViewModel
    {
        public RegistryViewModel(WixPdbInfo WixPdbInfo, RegistryInfo registryInfo, MsiTreeViewItemViewModel parent)
            : base(WixPdbInfo, parent, lazyLoadChildren: false)
        {
            RegistryInfo = registryInfo;

            Details = new ObservableCollection<MsiTreeViewItemDetail>();
            Details.Add(new MsiTreeViewItemDetail("Registry", RegistryInfo.Registry));
            Details.Add(new MsiTreeViewItemDetail("Root", RegistryInfo.Root.ToString()));
            Details.Add(new MsiTreeViewItemDetail("Key", RegistryInfo.Key));
            Details.Add(new MsiTreeViewItemDetail("Name", RegistryInfo.Name));
            Details.Add(new MsiTreeViewItemDetail("Value", RegistryInfo.Value));
            Details.Add(new MsiTreeViewItemDetail("Component_", RegistryInfo.Component_));
        }

        public RegistryInfo RegistryInfo { get; protected set; }

        public string Registry
        {
            get { return RegistryInfo.Registry; }
        }

        public override void Selected(MsiDisplayControl control)
        {
            control.Details.ItemsSource = Details;
            CreateNameValueColumns(control.Details);
        }
    }
}