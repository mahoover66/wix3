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

            Details = new ObservableCollection<ItemDetail>();
            Details.Add(new NameValueItemDetail("Feature", FeatureInfo.Feature, null));
            Details.Add(new NameValueItemDetail("Feature_Parent", FeatureInfo.Feature_Parent, null));
            Details.Add(new NameValueItemDetail("Title", FeatureInfo.Title, null));
            Details.Add(new NameValueItemDetail("Description", FeatureInfo.Description, null));
            Details.Add(new NameValueItemDetail("Display", FeatureInfo.Display.ToString(), null));
            Details.Add(new NameValueItemDetail("Level", FeatureInfo.Level.ToString(), null));
            Details.Add(new NameValueItemDetail("Directory_", FeatureInfo.Directory_, null));
            Details.Add(new NameValueItemDetail("Attributes", Enum.GetName(typeof(FeatureAttributes), FeatureInfo.Attributes), null));
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