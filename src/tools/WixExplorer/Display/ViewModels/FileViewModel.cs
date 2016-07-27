using System;
using System.Collections.ObjectModel;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class FileViewModel : MsiTreeViewItemViewModel
    {
        public FileViewModel(WixPdbInfo WixPdbInfo, FileInfo fileInfo, MsiTreeViewItemViewModel parent)
            : base(WixPdbInfo, parent, lazyLoadChildren: false)
        {
            FileInfo = fileInfo;

            Details = new ObservableCollection<ItemDetail>();
            Details.Add(new NameValueItemDetail("File", FileInfo.File, null));
            Details.Add(new NameValueItemDetail("Component_", FileInfo.Component_, null));
            Details.Add(new NameValueItemDetail("FileName", FileInfo.FileName, null));
            Details.Add(new NameValueItemDetail("FileSize", FileInfo.FileSize.ToString(), null));
            Details.Add(new NameValueItemDetail("Version", FileInfo.Version, null));
            Details.Add(new NameValueItemDetail("Language", FileInfo.Language, null));
            Details.Add(new NameValueItemDetail("Attributes", Enum.GetName(typeof(FileAttributes), FileInfo.Attributes), null));
            Details.Add(new NameValueItemDetail("Sequence", FileInfo.Sequence.ToString(), null));
        }

        public FileInfo FileInfo { get; protected set; }

        public string File
        {
            get { return FileInfo.File; }
        }

        public override void Selected(MsiDisplayControl control)
        {
            control.Details.ItemsSource = Details;
            CreateNameValueColumns(control.Details);
        }
    }
}