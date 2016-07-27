using System.Collections.ObjectModel;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class RegistryViewModel : MsiTreeViewItemViewModel
    {
        public RegistryViewModel(WixPdbInfo WixPdbInfo, RegistryInfo registryInfo, MsiTreeViewItemViewModel parent)
            : base(WixPdbInfo, parent, lazyLoadChildren: false)
        {
            RegistryInfo = registryInfo;

            Details = new ObservableCollection<ItemDetail>();
            Details.Add(new NameValueItemDetail("Registry", RegistryInfo.Registry, null));
            Details.Add(new NameValueItemDetail("Root", RegistryInfo.Root.ToString(), null));
            Details.Add(new NameValueItemDetail("Key", RegistryInfo.Key, null));
            Details.Add(new NameValueItemDetail("Name", RegistryInfo.Name, null));
            Details.Add(new NameValueItemDetail("Value", RegistryInfo.Value, null));
            Details.Add(new NameValueItemDetail("Component_", RegistryInfo.Component_, null));
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