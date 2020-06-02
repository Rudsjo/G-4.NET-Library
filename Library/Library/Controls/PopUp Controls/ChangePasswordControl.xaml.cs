using Library.Core;
using System.Security;
using System.Windows.Controls;


namespace Library
{
    /// <summary>
    /// Interaction logic for ChangePasswordControl.xaml
    /// </summary>
    public partial class ChangePasswordControl : UserControl, IHavePassword, INewPassword
    {
        public ChangePasswordControl()
        {
            InitializeComponent();
            DataContext = IoC.CreateInstance<ChangePasswordControlViewModel>();
        }

        public SecureString SecurePassword => OldPassword.SecurePassword;

        public SecureString SecondPassword => NewPassword.SecurePassword;
    }
}
