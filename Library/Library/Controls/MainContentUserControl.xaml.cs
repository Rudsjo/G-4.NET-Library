using Library.Core;
using System.Windows.Controls;


namespace Library
{
    /// <summary>
    /// Interaction logic for MainContentUserControl.xaml
    /// </summary>
    public partial class MainContentUserControl : UserControl
    {
        public MainContentUserControl()
        {
            InitializeComponent();
            DataContext = IoC.CreateInstance<MainContentUserControlViewModel>();
        }
    }
}
