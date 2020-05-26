using Library.Core;
using System.Windows.Controls;

namespace Library
{
    /// <summary>
    /// Interaction logic for SubPopUpControl.xaml
    /// </summary>
    public partial class SubPopUpControl : UserControl
    {
        public SubPopUpControl()
        {
            InitializeComponent();
            DataContext = IoC.CreateInstance<PopUpControlViewModel>();
        }
    }
}
