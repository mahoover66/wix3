using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class MsiTreeViewItemViewModel : INotifyPropertyChanged
    {
        static readonly MsiTreeViewItemViewModel s_dummyChild = new MsiTreeViewItemViewModel();

        public ObservableCollection<ItemDetail> Details { get; protected set; }
        public ObservableCollection<MsiTreeViewItemViewModel> Children { get; protected set; }
        public WixPdbInfo WixPdbInfo { get; protected set; }
        public MsiTreeViewItemViewModel Parent { get; protected set; }
        public bool LazyLoadChildren { get; protected set; }

        private bool m_isExpanded;
        private bool m_isSelected;

        protected MsiTreeViewItemViewModel(WixPdbInfo wixPdbInfo, MsiTreeViewItemViewModel parent, bool lazyLoadChildren)
        {
            WixPdbInfo = wixPdbInfo;
            Parent = parent;
            LazyLoadChildren = lazyLoadChildren;

            Children = new ObservableCollection<MsiTreeViewItemViewModel>();

            if (LazyLoadChildren)
                Children.Add(s_dummyChild);
        }

        // This is used to create the DummyChild instance.
        private MsiTreeViewItemViewModel()
        {
        }

        /// <summary>
        /// Returns true if this object's Children have not yet been populated.
        /// </summary>
        public bool HasDummyChild
        {
            get { return this.Children.Count == 1 && this.Children[0] == s_dummyChild; }
        }

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is expanded.
        /// </summary>
        public bool IsExpanded
        {
            get { return m_isExpanded; }
            set
            {
                if (value != m_isExpanded)
                {
                    m_isExpanded = value;
                    this.OnPropertyChanged("IsExpanded");
                }

                // Expand all the way up to the root.
                if (m_isExpanded && Parent != null)
                    Parent.IsExpanded = true;

                // Lazy load the child items, if necessary.
                if (this.HasDummyChild)
                {
                    this.Children.Remove(s_dummyChild);
                    this.LoadChildren();
                }
            }
        }

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return m_isSelected; }
            set
            {
                if (value != m_isSelected)
                {
                    m_isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }

        /// <summary>
        /// Invoked when the child items need to be loaded on demand.
        /// Subclasses can override this to populate the Children collection.
        /// </summary>
        protected virtual void LoadChildren()
        {
        }

        // called when the item is selected
        public virtual void Selected(MsiDisplayControl control)
        {
            control.Details.ItemsSource = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Helpler method to create two columns in the details pane, name and value
        /// </summary>
        protected void CreateNameValueColumns(DataGrid grid)
        {
            grid.Columns.Clear();

            if (grid.ItemsSource == null)
            {
                return;
            }

            DataGridTextColumn nameColumn = new DataGridTextColumn();
            nameColumn.Header = "Name";
            nameColumn.Binding = new Binding("Name");
            grid.Columns.Add(nameColumn);

            DataGridTextColumn valueColumn = new DataGridTextColumn();
            valueColumn.Header = "Value";
            valueColumn.Binding = new Binding("Value");
            grid.Columns.Add(valueColumn);
        }
    }
}
