using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class MsiInfoViewModel : MsiTreeViewItemViewModel
    {
        public MsiInfoViewModel(MsiInfo msiInfo)
            : base(msiInfo, null, true)
        {
            Details = new ObservableCollection<MsiTreeViewItemDetail>();
            Details.Add(new MsiTreeViewItemDetail("FilePath", MsiInfo.FilePath));
            Details.Add(new MsiTreeViewItemDetail("Title", MsiInfo.Title));
            Details.Add(new MsiTreeViewItemDetail("Subject", MsiInfo.Subject));
            Details.Add(new MsiTreeViewItemDetail("Comments", MsiInfo.Comments));
            Details.Add(new MsiTreeViewItemDetail("Keywords", MsiInfo.Keywords));
            Details.Add(new MsiTreeViewItemDetail("Platform", MsiInfo.Platform));
            Details.Add(new MsiTreeViewItemDetail("PackageCode", MsiInfo.PackageCode));
            Details.Add(new MsiTreeViewItemDetail("Schemna", MsiInfo.Schemna));
            Details.Add(new MsiTreeViewItemDetail("Languages", MsiInfo.Languages));
        }

        public MsiTreeViewItemViewModel ViewModel
        {
            get { return this; }
        }

        public string MsiName
        {
            get { return Path.GetFileName(MsiInfo.FilePath); }
        }

        protected override void LoadChildren()
        {
            Children.Add(new LogicalRootViewModel(MsiInfo, LazyLoadChildren));
            Children.Add(new TableRootViewModel(MsiInfo, LazyLoadChildren));
        }

        public override void Selected(MsiDisplayControl control)
        {
            control.Details.ItemsSource = Details;
            CreateNameValueColumns(control.Details);
        }
    }
}
