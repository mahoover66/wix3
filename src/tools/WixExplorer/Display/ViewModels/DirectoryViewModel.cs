using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class DirectoryViewModel : MsiTreeViewItemViewModel
    {
        public DirectoryViewModel(WixPdbInfo WixPdbInfo, DirectoryInfo directoryInfo, MsiTreeViewItemViewModel parent)
            : base(WixPdbInfo, parent, lazyLoadChildren: true)
        {
            DirectoryInfo = directoryInfo;

            Details = new ObservableCollection<ItemDetail>();
            Details.Add(new NameValueItemDetail("Directory", DirectoryInfo.Directory, null));
            Details.Add(new NameValueItemDetail("Directory_Parent", DirectoryInfo.Directory_Parent, null));
            Details.Add(new NameValueItemDetail("DefaultDir", DirectoryInfo.DefaultDir, null));
            Details.Add(new NameValueItemDetail("DirectoryName", DirectoryInfo.DirectoryName, null));
            Details.Add(new NameValueItemDetail("FullPath", DirectoryInfo.FullPath, null));
        }

        public DirectoryInfo DirectoryInfo { get; protected set; }

        public string Directory
        {
            get { return DirectoryInfo.Directory; }
        }

        protected override void LoadChildren()
        {
            foreach (ComponentInfo componentInfo in WixPdbInfo.Components.Values.OrderBy(c => c.Component))
            {
                // components installed to this directory
                if (!string.IsNullOrEmpty(componentInfo.Directory_) && componentInfo.Directory_.Equals(DirectoryInfo.Directory))
                {
                    Children.Add(new ComponentViewModel(WixPdbInfo, componentInfo, this));
                }
            }
        }

        public override void Selected(MsiDisplayControl control)
        {
            control.Details.ItemsSource = Details;
            CreateNameValueColumns(control.Details);
        }
    }
}