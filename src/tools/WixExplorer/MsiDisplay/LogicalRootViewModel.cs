using System.Linq;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class LogicalRootViewModel : MsiTreeViewItemViewModel
    {
        public LogicalRootViewModel(MsiInfo msiInfo, bool lazyLoadChildren)
            : base(msiInfo, null, lazyLoadChildren)
        {
        }

        protected override void LoadChildren()
        {
            foreach (string feature in MsiInfo.Features.Keys.OrderBy(key => key))
            {
                FeatureInfo featureInfo = MsiInfo.Features[feature];

                // root features don't have parents
                if (string.IsNullOrEmpty(featureInfo.Feature_Parent))
                {
                    Children.Add(new FeatureViewModel(MsiInfo, featureInfo, this, LazyLoadChildren));
                }
            }

            foreach (string directory in MsiInfo.Directories.Keys.OrderBy(key => key))
            {
                DirectoryInfo directoryInfo = MsiInfo.Directories[directory];
                Children.Add(new DirectoryViewModel(MsiInfo, directoryInfo, this));
            }
        }
    }
}
