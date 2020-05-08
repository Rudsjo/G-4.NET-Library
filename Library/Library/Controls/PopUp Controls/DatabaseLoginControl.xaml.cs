using Library.Core;
using System.Windows.Controls;


namespace Library
{
    /// <summary>
    /// Interaction logic for DatabaseLoginUserControl.xaml
    /// </summary>
    public partial class DatabaseLoginControl : UserControl
    {
        public DatabaseLoginControl()
        {
            InitializeComponent();
            DataContext = IoC.CreateInstance<DatabaseLoginControlViewModel>();
        }
    }
}
