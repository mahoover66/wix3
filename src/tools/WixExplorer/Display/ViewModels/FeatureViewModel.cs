using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class FeatureViewModel : MsiTreeViewItemViewModel
    {
        public FeatureViewModel(WixPdbInfo WixPdbInfo, FeatureInfo featureInfo, MsiTreeViewItemViewModel parent)
            : base(WixPdbInfo, parent, lazyLoadChildren: true)
        {
            FeatureInfo = featureInfo;

            Details = new ObservableCollection<MsiTreeViewItemDetail>();
            Details.Add(new MsiTreeViewItemDetail("Feature", FeatureInfo.Feature));
            Details.Add(new MsiTreeViewItemDetail("Feature_Parent", FeatureInfo.Feature_Parent));
            Details.Add(new MsiTreeViewItemDetail("Title", FeatureInfo.Title));
            Details.Add(new MsiTreeViewItemDetail("Description", FeatureInfo.Description));
            Details.Add(new MsiTreeViewItemDetail("Display", FeatureInfo.Display.ToString()));
            Details.Add(new MsiTreeViewItemDetail("Level", FeatureInfo.Level.ToString()));
            Details.Add(new MsiTreeViewItemDetail("Directory_", FeatureInfo.Directory_));
            Details.Add(new MsiTreeViewItemDetail("Attributes", Enum.GetName(typeof(FeatureAttributes), FeatureInfo.Attributes)));
        }

        public FeatureInfo FeatureInfo { get; protected set; }

        public string Feature
        {
            get { return FeatureInfo.Feature; }
        }

        protected override void LoadChildren()
        {
            // first add sub features
            foreach (string feature in WixPdbInfo.Features.Keys.OrderBy(key => key))
            {
                FeatureInfo featureInfo = WixPdbInfo.Features[feature];

                // features with us a a parent
                if (!string.IsNullOrEmpty(featureInfo.Feature_Parent) && featureInfo.Feature_Parent.Equals(FeatureInfo.Feature))
                {
                    Children.Add(new FeatureViewModel(WixPdbInfo, featureInfo, this));
                }
            }

            // now add components
            foreach (FeatureComponentsInfo featureComponentInfo in WixPdbInfo.FeatureComponents.OrderBy(fc => fc.Component_))
            {
                // components with us a a parent
                if (featureComponentInfo.Feature_.Equals(FeatureInfo.Feature))
                {
                    Children.Add(new ComponentViewModel(WixPdbInfo, WixPdbInfo.Components[featureComponentInfo.Component_], this));
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