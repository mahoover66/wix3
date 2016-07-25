using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class ComponentViewModel : MsiTreeViewItemViewModel
    {
        public ComponentViewModel(MsiInfo msiInfo, ComponentInfo componentInfo, MsiTreeViewItemViewModel parent, bool lazyLoadChildren)
            : base(msiInfo, parent, lazyLoadChildren)
        {
            ComponentInfo = componentInfo;

            Details = new ObservableCollection<MsiTreeViewItemDetail>();
            Details.Add(new MsiTreeViewItemDetail("Component", ComponentInfo.Component));
            Details.Add(new MsiTreeViewItemDetail("ComponentId", ComponentInfo.ComponentId));
            Details.Add(new MsiTreeViewItemDetail("Directory_", ComponentInfo.Directory_));
            Details.Add(new MsiTreeViewItemDetail("Attributes", Enum.GetName(typeof(ComponentAttributes), ComponentInfo.Attributes)));
            Details.Add(new MsiTreeViewItemDetail("Condition", ComponentInfo.Condition));
            Details.Add(new MsiTreeViewItemDetail("KeyPath", ComponentInfo.KeyPath));
        }

        public ComponentInfo ComponentInfo { get; protected set; }

        public string Component
        {
            get { return ComponentInfo.Component; }
        }

        protected override void LoadChildren()
        {
            // first add files
            foreach (string file in MsiInfo.Files.Keys.OrderBy(key => key))
            {
                FileInfo fileInfo = MsiInfo.Files[file];

                // files with us a a parent
                if (fileInfo.Component_.Equals(ComponentInfo.Component))
                {
                    Children.Add(new FileViewModel(MsiInfo, fileInfo, this));
                }
            }

            // now add registry entries
            foreach (string registry in MsiInfo.RegistryEntries.Keys.OrderBy(key => key))
            {
                RegistryInfo registryInfo = MsiInfo.RegistryEntries[registry];

                // files with us a a parent
                if (registryInfo.Component_.Equals(ComponentInfo.Component))
                {
                    Children.Add(new RegistryViewModel(MsiInfo, registryInfo, this));
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