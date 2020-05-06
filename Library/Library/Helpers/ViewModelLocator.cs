
using Library.Core;

namespace Library
{
    /// <summary>
    /// Locates view models from the IoC for bindings in XAML files
    /// </summary>
    public class ViewModelLocator
    {

        /// <summary>
        /// Singleton instance of the locator
        /// </summary>
        public static ViewModelLocator Instance { get; private set; } = new ViewModelLocator();

        /// <summary>
        /// The <see cref="ApplicationViewModel"/>
        /// </summary>
        public static ApplicationViewModel ApplicationViewModel => IoC.CreateInstance<ApplicationViewModel>();

    }
}
