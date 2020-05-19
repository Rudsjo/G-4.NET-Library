using Library.Core;
using System.Windows.Controls;


namespace Library
{
    /// <summary>
    /// Interaction logic for PopUpControl.xaml
    /// </summary>
    public partial class PopUpControl : UserControl
    {
        public PopUpControl()
        {
            InitializeComponent();
            DataContext = IoC.CreateInstance<PopUpControlViewModel>();
        }
    }
}
