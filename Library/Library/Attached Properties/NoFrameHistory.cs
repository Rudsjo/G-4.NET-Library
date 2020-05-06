using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Library
{
    /// <summary>
    /// Attached property for creating a <see cref="Frame"/> that never shows navigation and keeps the navigation history empty
    /// </summary>
    public class NoFrameHistory : BaseAttachedProperty<NoFrameHistory, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Get the frame
            var frame = (sender as Frame);

            // Hide navigation bar
            frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;

            // Clear history on navigate
            frame.Navigated += (frameSender, ee) => ((Frame)frameSender).NavigationService.RemoveBackEntry();
        }
    }
}
