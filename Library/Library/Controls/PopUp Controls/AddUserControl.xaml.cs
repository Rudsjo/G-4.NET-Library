using Library.Core;
using System.Security;
using System.Windows.Controls;


namespace Library
{
    /// <summary>
    /// Interaction logic for AddUserControl.xaml
    /// </summary>
    public partial class AddUserControl : UserControl, IHavePassword
    {
        public AddUserControl()
        {
            InitializeComponent();
            DataContext = IoC.CreateInstance<AddUserControlViewModel>();
        }

        public SecureString SecurePassword => PasswordText.SecurePassword;
    }
}
