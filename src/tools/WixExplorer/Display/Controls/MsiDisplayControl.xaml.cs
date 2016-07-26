using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    /// <summary>
    /// Interaction logic for MsiDisplayControl.xaml
    /// </summary>
    public partial class MsiDisplayControl : UserControl
    {
        public MsiDisplayControl()
        {
            InitializeComponent();
        }

        private void MsiTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            MsiTreeViewItemViewModel selectedItem = MsiTree.SelectedItem as MsiTreeViewItemViewModel;
            if (selectedItem != null)
            {
                // let the item update
                selectedItem.Selected(this);
            }
            else
            {
                // don't know what is selected, clear the textbox
                Details.ItemsSource = null;
            }
        }
    }
}
