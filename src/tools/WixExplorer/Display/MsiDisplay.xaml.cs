using System;
using System.Windows;

using Microsoft.Win32;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MsiDisplay : Window
    {
        public MsiDisplay()
        {
            InitializeComponent();

            ViewModel = new MsiDisplayViewModel();
            MsiDisplayControl.DataContext = ViewModel;
        }

        public MsiDisplayViewModel ViewModel { get; protected set; }

        private void menuItemFileOpen_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box 
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "";
            dlg.DefaultExt = ".wixpdb";
            dlg.Filter = "Wix Debuging Info documents (.wixpdb)|*.wixpdb";

            // Show open file dialog box 
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Open document 
                WixPdbInfo WixPdbInfo = new WixPdbInfo(dlg.FileName);
                WixPdbInfo.Open();

                // now add it to the display
                ViewModel.AddWixPdbInfo(WixPdbInfo);
            }
        }

        private void menuItemFileDiff_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Msis.Count < 2)
            {
                MessageBox.Show("You must have at least two MSIs open in order to diff", "Wix Explorer Error", MessageBoxButton.OK);
                return;
            }

            // Configure open file dialog box 
            OpenDiffDialog dlg = new OpenDiffDialog(ViewModel);

            // Show open file dialog box 
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                if (dlg.LeftMsi == null || dlg.RightMsi == null)
                {
                    MessageBox.Show("You must have select two MSIs in order to diff", "Wix Explorer Error", MessageBoxButton.OK);
                    return;
                }

                DiffRootViewModel diffRootViewModel = new DiffRootViewModel(dlg.LeftMsi, dlg.RightMsi);
                ViewModel.AddDiffView(diffRootViewModel);
            }
        }

        private void menuItemFileExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void menuItemHelpAbout_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
