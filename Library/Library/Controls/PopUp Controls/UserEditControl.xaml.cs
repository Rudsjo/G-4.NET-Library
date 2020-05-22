using Library.Core;
using System.Windows.Controls;


namespace Library
{
    /// <summary>
    /// Interaction logic for UserEditControl.xaml
    /// </summary>
    public partial class UserEditControl : UserControl
    {
        public UserEditControl()
        {
            InitializeComponent();
            DataContext = IoC.CreateInstance<TableControlViewModel>();
        }
    }
}
