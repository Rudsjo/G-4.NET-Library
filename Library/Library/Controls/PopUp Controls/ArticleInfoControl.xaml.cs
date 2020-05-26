using Library.Core;
using System.Windows.Controls;


namespace Library
{
    /// <summary>
    /// Interaction logic for ArticleInfoControl.xaml
    /// </summary>
    public partial class ArticleInfoControl : UserControl
    {
        public ArticleInfoControl()
        {
            InitializeComponent();
            DataContext = IoC.CreateInstance<TableControlViewModel>();
        }
    }
}
