
using Library.Core;
using System.Windows.Controls;


namespace Library
{
    /// <summary>
    /// Interaction logic for AddArticleControl.xaml
    /// </summary>
    public partial class AddArticleControl : UserControl
    {
        public AddArticleControl()
        {
            InitializeComponent();
            DataContext = IoC.CreateInstance<AddArticleControlViewModel>();
        }
    }
}
