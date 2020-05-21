using Library.Core;
using System.Windows.Controls;


namespace Library
{
    /// <summary>
    /// Interaction logic for ArticleEditControl.xaml
    /// </summary>
    public partial class ArticleEditControl : UserControl
    {
        public ArticleEditControl()
        {
            InitializeComponent();
            DataContext = IoC.CreateInstance<TableControlViewModel>();
        }
    }
}
