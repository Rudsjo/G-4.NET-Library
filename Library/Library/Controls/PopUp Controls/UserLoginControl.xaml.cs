using Library.Core;
using System.Windows.Controls;


namespace Library
{
    /// <summary>
    /// Interaction logic for UserLoginControl.xaml
    /// </summary>
    public partial class UserLoginControl : UserControl
    {
        public UserLoginControl()
        {
            InitializeComponent();
            DataContext = IoC.CreateInstance<UserLoginControlViewModel>();
        }
    }
}
