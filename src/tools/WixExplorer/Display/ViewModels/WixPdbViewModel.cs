using System.Collections.ObjectModel;
using System.IO;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class WixPdbViewModel : MsiTreeViewItemViewModel
    {
        public WixPdbViewModel(WixPdbInfo WixPdbInfo)
            : base(WixPdbInfo, null, true)
        {
            Details = new ObservableCollection<MsiTreeViewItemDetail>();
            Details.Add(new MsiTreeViewItemDetail("FilePath", WixPdbInfo.FilePath));
            Details.Add(new MsiTreeViewItemDetail("Title", WixPdbInfo.Title));
            Details.Add(new MsiTreeViewItemDetail("Subject", WixPdbInfo.Subject));
            Details.Add(new MsiTreeViewItemDetail("Comments", WixPdbInfo.Comments));
            Details.Add(new MsiTreeViewItemDetail("Keywords", WixPdbInfo.Keywords));
            Details.Add(new MsiTreeViewItemDetail("Platform", WixPdbInfo.Platform));
            Details.Add(new MsiTreeViewItemDetail("PackageCode", WixPdbInfo.PackageCode));
            Details.Add(new MsiTreeViewItemDetail("Schemna", WixPdbInfo.Schemna));
            Details.Add(new MsiTreeViewItemDetail("Languages", WixPdbInfo.Languages));
        }

        public MsiTreeViewItemViewModel ViewModel
        {
            get { return this; }
        }

        public string MsiName
        {
            get { return Path.GetFileName(WixPdbInfo.FilePath); }
        }

        protected override void LoadChildren()
        {
            Children.Add(new LogicalRootViewModel(WixPdbInfo));
            Children.Add(new TableRootViewModel(WixPdbInfo));
        }

        public override void Selected(MsiDisplayControl control)
        {
            control.Details.ItemsSource = Details;
            CreateNameValueColumns(control.Details);
        }
    }
}
