using Library.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;


namespace Library
{
    /// <summary>
    /// Interaction logic for TableControl.xaml
    /// </summary>
    public partial class TableControlSkeleton : UserControl
    {
        public TableControlSkeleton()
        {
            InitializeComponent();

            SkeletonListShell.ItemsSource = SkeletonList;
        }

        /// <summary>
        /// Placeholder list to be shown in the skeleton controls
        /// </summary>
        public IEnumerable<UserViewModel> SkeletonList { get; set; } = new ObservableCollection<UserViewModel>().FillPlaceHolders();
    }
}
