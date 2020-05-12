using Library.Core;
using System.Windows;

namespace Library
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Custom startup to load the <see cref="IoC"/> immediately before anything else in the application
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // Let the base application do what it needs
            base.OnStartup(e);

            IoC.CreateInstance<ApplicationViewModel>();

            // Set start page
            IoC.CreateInstance<ApplicationViewModel>().GoToPage(ApplicationPages.MainPage);

            // Create a new instance of the main window
            Current.MainWindow = new MainWindow();

            // Show the main window
            Current.MainWindow.Show();
        }

    }
}
