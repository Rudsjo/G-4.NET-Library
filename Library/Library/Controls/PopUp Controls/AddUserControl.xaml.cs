using Library.Core;
using System.Windows.Controls;


namespace Library
{
    /// <summary>
    /// Interaction logic for AddUserControl.xaml
    /// </summary>
    public partial class AddUserControl : UserControl
    {
        public AddUserControl()
        {
            InitializeComponent();
            DataContext = IoC.CreateInstance<AddUserControlViewModel>();
        }
    }
}
