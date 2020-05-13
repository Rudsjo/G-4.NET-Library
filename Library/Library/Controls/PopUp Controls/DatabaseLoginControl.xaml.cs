using Library.Core;
using System.Security;
using System.Windows.Controls;


namespace Library
{
    /// <summary>
    /// Interaction logic for DatabaseLoginUserControl.xaml
    /// </summary>
    public partial class DatabaseLoginControl : UserControl, IHavePassword
    {
        public DatabaseLoginControl()
        {
            InitializeComponent();
            DataContext = IoC.CreateInstance<DatabaseLoginControlViewModel>();
        }

        public SecureString SecurePassword => PasswordText.SecurePassword;
    }
}
