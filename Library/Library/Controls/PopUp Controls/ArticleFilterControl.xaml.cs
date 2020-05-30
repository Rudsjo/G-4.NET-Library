using Library.Core;
using System.Windows.Controls;


namespace Library
{
    /// <summary>
    /// Interaction logic for ArticleFilterControl.xaml
    /// </summary>
    public partial class ArticleFilterControl : UserControl
    {
        public ArticleFilterControl()
        {
            InitializeComponent();
            DataContext = IoC.CreateInstance<MainContentUserControlViewModel>();
        }
    }
}
