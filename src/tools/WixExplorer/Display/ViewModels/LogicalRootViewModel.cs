using System.Linq;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class LogicalRootViewModel : MsiTreeViewItemViewModel
    {
        public LogicalRootViewModel(WixPdbInfo WixPdbInfo)
            : base(WixPdbInfo, null, lazyLoadChildren: true)
        {
        }

        protected override void LoadChildren()
        {
            // first add all the Features
            foreach (string feature in WixPdbInfo.Features.Keys.OrderBy(key => key))
            {
                FeatureInfo featureInfo = WixPdbInfo.Features[feature];

                // root features don't have parents
                if (string.IsNullOrEmpty(featureInfo.Feature_Parent))
                {
                    Children.Add(new FeatureViewModel(WixPdbInfo, featureInfo, this));
                }
            }

            // now add the Directory root node
            Children.Add(new DirectoryRootViewModel(WixPdbInfo));

        }
    }
}
