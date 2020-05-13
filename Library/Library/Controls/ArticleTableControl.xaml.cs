using Library.Core;
using System.Windows.Controls;


namespace Library
{
    /// <summary>
    /// Interaction logic for TableControl.xaml
    /// </summary>
    public partial class ArticleTableControl : UserControl
    {
        public ArticleTableControl()
        {
            InitializeComponent();
            DataContext = IoC.CreateInstance<TableControlViewModel>();
        }
    }
}
