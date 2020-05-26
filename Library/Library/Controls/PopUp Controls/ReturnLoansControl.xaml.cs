using Library.Core;
using System.Windows.Controls;

namespace Library
{
    /// <summary>
    /// Interaction logic for ReturnLoansControl.xaml
    /// </summary>
    public partial class ReturnLoansControl : UserControl
    {
        public ReturnLoansControl()
        {
            InitializeComponent();
            DataContext = IoC.CreateInstance<MainContentUserControlViewModel>();
        }
    }
}
