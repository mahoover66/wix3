using System;
using System.Collections.ObjectModel;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class FileViewModel : MsiTreeViewItemViewModel
    {
        public FileViewModel(MsiInfo msiInfo, FileInfo fileInfo, MsiTreeViewItemViewModel parent)
            : base(msiInfo, parent, false)
        {
            FileInfo = fileInfo;

            Details = new ObservableCollection<MsiTreeViewItemDetail>();
            Details.Add(new MsiTreeViewItemDetail("File", FileInfo.File));
            Details.Add(new MsiTreeViewItemDetail("Component_", FileInfo.Component_));
            Details.Add(new MsiTreeViewItemDetail("FileName", FileInfo.FileName));
            Details.Add(new MsiTreeViewItemDetail("FileSize", FileInfo.FileSize.ToString()));
            Details.Add(new MsiTreeViewItemDetail("Version", FileInfo.Version));
            Details.Add(new MsiTreeViewItemDetail("Language", FileInfo.Language));
            Details.Add(new MsiTreeViewItemDetail("Attributes", Enum.GetName(typeof(FileAttributes), FileInfo.Attributes)));
            Details.Add(new MsiTreeViewItemDetail("Sequence", FileInfo.Sequence.ToString()));
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