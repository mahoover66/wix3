using System;
using System.Collections.ObjectModel;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class DirectoryViewModel : MsiTreeViewItemViewModel
    {
        public DirectoryViewModel(MsiInfo msiInfo, DirectoryInfo directoryInfo, MsiTreeViewItemViewModel parent)
            : base(msiInfo, parent, false)
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

        public override void Selected(MsiDisplayControl control)
        {
            control.Details.ItemsSource = Details;
            CreateNameValueColumns(control.Details);
        }
    }
}