using System.Collections.ObjectModel;
using System.IO;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class WixPdbViewModel : MsiTreeViewItemViewModel
    {
        public WixPdbViewModel(WixPdbInfo WixPdbInfo)
            : base(WixPdbInfo, null, true)
        {
            Details = new ObservableCollection<ItemDetail>();
            Details.Add(new NameValueItemDetail("FilePath", WixPdbInfo.FilePath, null));
            Details.Add(new NameValueItemDetail("Title", WixPdbInfo.Title, null));
            Details.Add(new NameValueItemDetail("Subject", WixPdbInfo.Subject, null));
            Details.Add(new NameValueItemDetail("Comments", WixPdbInfo.Comments, null));
            Details.Add(new NameValueItemDetail("Keywords", WixPdbInfo.Keywords, null));
            Details.Add(new NameValueItemDetail("Platform", WixPdbInfo.Platform, null));
            Details.Add(new NameValueItemDetail("PackageCode", WixPdbInfo.PackageCode, null));
            Details.Add(new NameValueItemDetail("Schemna", WixPdbInfo.Schemna, null));
            Details.Add(new NameValueItemDetail("Languages", WixPdbInfo.Languages, null));
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
