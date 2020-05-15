using Library.Core;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Library
{
    /// <summary>
    /// A base page to hold basic functionality for pages
    /// </summary>
    public class BasePage : Page
    {
        #region Public Properties

        /// <summary>
        /// The animation to play when a page is loaded
        /// </summary>
        public PageAnimation PageLoadAnimation { get; set; } = PageAnimation.SlideAndFadeInFromRight;

        /// <summary>
        /// The animation to play when a page is unloaded
        /// </summary>
        public PageAnimation PageUnloadAnimation { get; set; } = PageAnimation.FlipAndFadeOut;

        /// <summary>
        /// The time any slide animation takes to complete
        /// </summary>
        public float SlideSeconds { get; set; } = 1.2f;

        /// <summary>
        /// The time any flip animation takes to complete
        /// </summary>
        public float FlipSeconds { get; set; } = 1.4f;

        /// <summary>
        /// A flag to indicate if the page should animate out on load
        /// </summary>
        public bool ShouldAnimateOut { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public BasePage()
        {
            // Checks if there should be any page animation on load
            if (PageLoadAnimation != PageAnimation.None)
                // If it is, then we hide the page to begin with
                Visibility = Visibility.Collapsed;

            // Listen out for the page loading
            Loaded += BasePage_Loaded;
        }

        #endregion

        #region Animation Load / Unload

        private async void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            // If wew are setup to animate out on load
            if (ShouldAnimateOut)
                // Animate out...
                await AnimateOutAsync();

            // Otherwise...
            else
                // Animate the page in
                await AnimateInAsync();
        }

        /// <summary>
        /// Animates the page in
        /// </summary>
        /// <returns></returns>
        public async Task AnimateInAsync()
        {
            // Make sure an animation should take place
            if (PageLoadAnimation == PageAnimation.None)
                return;

            // Perform the correct animation
            switch (PageLoadAnimation)
            {
                // Slide and fade in from the right
                case PageAnimation.SlideAndFadeInFromRight:
                    {
                        // Start the animation
                        await this.SlideAndFadeInFromTheRight(SlideSeconds);
                        IoC.CreateInstance<ApplicationViewModel>().PageLoadComplete = true;
                        break;
                    }
            }
        }

        public async Task AnimateOutAsync()
        {
            // Make sure an animation should take place
            if (PageUnloadAnimation == PageAnimation.None)
                return;

            // Perform the correct animation
            switch (PageUnloadAnimation)
            {
                // Flip and fade out
                case PageAnimation.FlipAndFadeOut:
                    {
                        // Start the animation
                        await this.FlipAndFadeOut(SlideSeconds);
                        //await this.SlideAndFadeOutToLeft(FlipSeconds);
                        break;
                    }
            }
        }

        #endregion

    }

    /// <summary>
    /// Base page to inherit all functions from <see cref="BasePage"/> and also containing an actual view model
    /// </summary>
    /// <typeparam name="VM"></typeparam>
    public class BasePage<VM> : BasePage
        where VM : BaseViewModel, new()
    {

        #region Private Member

        /// <summary>
        /// The view model associated with this page
        /// </summary>
        private VM _viewModel;

        #endregion

        #region Public Properties

        /// <summary>
        /// The view model associated with this page
        /// </summary>
        public VM ViewModel
        {
            get => _viewModel;
            set
            {
                // If nothing has changed, return
                if (_viewModel == value)
                    return;

                // Update the value if something has changed
                _viewModel = value;

                // Set the data context for this page
                DataContext = _viewModel;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public BasePage() : base()
        {
            // Create a default view model
            ViewModel = new VM();
        }

        #endregion

    }
}
