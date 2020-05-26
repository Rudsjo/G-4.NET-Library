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

            // Clear the password box when the window is closed
            this.IsVisibleChanged += (sender, e) =>
            {
                PasswordText.Clear();
            };
            UsernameBox.Focus();


            DataContext = IoC.CreateInstance<UserLoginControlViewModel>();
        }

        /// <summary>
        /// The secure password for this login page
        /// </summary>
        public SecureString SecurePassword => PasswordText.SecurePassword;
    }
}
