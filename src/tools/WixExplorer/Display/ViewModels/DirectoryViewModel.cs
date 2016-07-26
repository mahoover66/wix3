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

            Details = new ObservableCollection<MsiTreeViewItemDetail>();
            Details.Add(new MsiTreeViewItemDetail("Directory", DirectoryInfo.Directory));
            Details.Add(new MsiTreeViewItemDetail("Directory_Parent", DirectoryInfo.Directory_Parent));
            Details.Add(new MsiTreeViewItemDetail("DefaultDir", DirectoryInfo.DefaultDir));
            Details.Add(new MsiTreeViewItemDetail("DirectoryName", DirectoryInfo.DirectoryName));
            Details.Add(new MsiTreeViewItemDetail("FullPath", DirectoryInfo.FullPath));
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