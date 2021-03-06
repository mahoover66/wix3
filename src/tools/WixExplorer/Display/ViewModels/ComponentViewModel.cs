﻿using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class ComponentViewModel : MsiTreeViewItemViewModel
    {
        public ComponentViewModel(WixPdbInfo WixPdbInfo, ComponentInfo componentInfo, MsiTreeViewItemViewModel parent)
            : base(WixPdbInfo, parent, lazyLoadChildren: true)
        {
            ComponentInfo = componentInfo;

            Details = new ObservableCollection<ItemDetail>();
            Details.Add(new NameValueItemDetail("Component", ComponentInfo.Component, null));
            Details.Add(new NameValueItemDetail("ComponentId", ComponentInfo.ComponentId, null));
            Details.Add(new NameValueItemDetail("Directory_", ComponentInfo.Directory_, null));
            Details.Add(new NameValueItemDetail("Attributes", Enum.GetName(typeof(ComponentAttributes), ComponentInfo.Attributes), null));
            Details.Add(new NameValueItemDetail("Condition", ComponentInfo.Condition, null));
            Details.Add(new NameValueItemDetail("KeyPath", ComponentInfo.KeyPath, null));
        }

        public ComponentInfo ComponentInfo { get; protected set; }

        public string Component
        {
            get { return ComponentInfo.Component; }
        }

        protected override void LoadChildren()
        {
            // first add files
            foreach (string file in WixPdbInfo.Files.Keys.OrderBy(key => key))
            {
                FileInfo fileInfo = WixPdbInfo.Files[file];

                // files records with us a a parent
                if (fileInfo.Component_.Equals(ComponentInfo.Component))
                {
                    Children.Add(new FileViewModel(WixPdbInfo, fileInfo, this));
                }
            }

            // now add registry entries
            foreach (string registry in WixPdbInfo.RegistryEntries.Keys.OrderBy(key => key))
            {
                RegistryInfo registryInfo = WixPdbInfo.RegistryEntries[registry];

                // registry records with us a a parent
                if (registryInfo.Component_.Equals(ComponentInfo.Component))
                {
                    Children.Add(new RegistryViewModel(WixPdbInfo, registryInfo, this));
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