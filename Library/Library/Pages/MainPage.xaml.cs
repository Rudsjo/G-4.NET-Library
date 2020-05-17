using Library.Core;


namespace Library
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : BasePage<MainPageViewModel>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            tbFirstSearch.Focus();
            DataContext = IoC.CreateInstance<MainPageViewModel>();
        }
    }
}
