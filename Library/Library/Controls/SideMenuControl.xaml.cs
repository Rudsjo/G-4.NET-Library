using Library.Core;
using System.Windows;
using System.Windows.Controls;


namespace Library
{
    /// <summary>
    /// Interaction logic for SideMenuControl.xaml
    /// </summary>
    public partial class SideMenuControl : UserControl
    {
        public SideMenuControl()
        {
            InitializeComponent();
            DataContext = IoC.CreateInstance<SideMenuControlViewModel>();
        }
    }
}
