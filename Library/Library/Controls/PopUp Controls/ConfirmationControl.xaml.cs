using Library.Core;
using System.Windows.Controls;

namespace Library
{
    /// <summary>
    /// Interaction logic for DeleteConfirmationControl.xaml
    /// </summary>
    public partial class ConfirmationControl : UserControl
    {
        public ConfirmationControl()
        {
            InitializeComponent();
            DataContext = IoC.CreateInstance<ConfirmationControlViewModel>();
        }
    }
}
