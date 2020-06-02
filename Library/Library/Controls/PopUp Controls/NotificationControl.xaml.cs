using Library.Core;
using System.Windows.Controls;


namespace Library
{
    /// <summary>
    /// Interaction logic for NotificationControl.xaml
    /// </summary>
    public partial class NotificationControl : UserControl
    {
        public NotificationControl()
        {
            InitializeComponent();

            DataContext = IoC.CreateInstance<MainContentUserControlViewModel>();
        }
    }
}
