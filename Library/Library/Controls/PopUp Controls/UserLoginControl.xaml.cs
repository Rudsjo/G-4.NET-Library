using Library.Core;
using System.Security;
using System.Windows.Controls;


namespace Library
{
    /// <summary>
    /// Interaction logic for UserLoginControl.xaml
    /// </summary>
    public partial class UserLoginControl : UserControl, IHavePassword
    {
        public UserLoginControl()
        {
            InitializeComponent();
            DataContext = IoC.CreateInstance<UserLoginControlViewModel>();
            UsernameBox.Focus();
        }

        /// <summary>
        /// The secure password for this login page
        /// </summary>
        public SecureString SecurePassword => PasswordText.SecurePassword;
    }
}
