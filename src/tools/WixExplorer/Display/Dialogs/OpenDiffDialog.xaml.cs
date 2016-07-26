using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class OpenDiffDialog : Window, INotifyPropertyChanged
    {
        public OpenDiffDialog(MsiDisplayViewModel msiDisplayViewModel)
        {
            ViewModel = msiDisplayViewModel;

            InitializeComponent();

            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MsiDisplayViewModel ViewModel { get; protected set; }


        public IList<WixPdbInfo> OpenMSIs
        {
            get
            {
                return ViewModel.Msis;
            }
        }

        protected WixPdbInfo _LeftMsi;

        public WixPdbInfo LeftMsi
        {
            get
            {
                return _LeftMsi;
            }
            set
            {
                if (_LeftMsi != value)
                {
                    _LeftMsi = value;
                    RaisePropertyChanged("LeftMsi");
                }
            }
        }

        protected WixPdbInfo _RightMsi;

        public WixPdbInfo RightMsi
        {
            get
            {
                return _RightMsi;
            }
            set
            {
                if (_RightMsi != value)
                {
                    _RightMsi = value;
                    RaisePropertyChanged("RightMsi");
                }
            }
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            // Dialog box accepted
            DialogResult = true;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Dialog box canceled
            DialogResult = false;
        }

        private void RaisePropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
