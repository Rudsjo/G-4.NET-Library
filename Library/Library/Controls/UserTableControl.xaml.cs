using Library.Core;
using System.Windows.Controls;


namespace Library
{
    /// <summary>
    /// Interaction logic for TableControl.xaml
    /// </summary>
    public partial class UserTableControl : UserControl
    {
        public UserTableControl()
        {
            InitializeComponent();
            DataContext = IoC.CreateInstance<TableControlViewModel>();
        }
    }
}
