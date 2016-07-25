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
                MsiInfo msiInfo = new MsiInfo(dlg.FileName);
                msiInfo.Open();

                // now add it to the display
                ViewModel.WixExplorer.AddMsiInfo(msiInfo);
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
