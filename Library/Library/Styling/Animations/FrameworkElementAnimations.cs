using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Library
{
    /// <summary>
    /// Helper class to animate framework elements such as side menus
    /// </summary>
    public static class FrameworkElementAnimations
    {
        /// <summary>
        /// Slides an element in from the right
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <param name="keepMargin">Whether to keep the element at the same width during the naimation or not</param>
        /// <returns></returns>
        public static async Task SlideAndFadeInFromLeft(this FrameworkElement element, float seconds = 0.3f, bool keepMargin = true)
        {
            // Create the storyboard
            var sb = new Storyboard();

            // Add a slide from the right animation
            sb.AddSlideFromRight(seconds, element.ActualWidth, keepMargin: keepMargin);

            // Add a fade in animation
            sb.AddFadeIn(seconds);

            // Start animating
            sb.Begin(element);

            // Make the element visible
            element.Visibility = Visibility.Visible;

            // Wait for it to finish
            await Task.Delay((int)(seconds * 1000));
        }

        public static async Task SlideAndFadeOutToLeft(this FrameworkElement element, float seconds = 0.3f, bool keepMargin = true)
        {
            // Create the storyboard
            var sb = new Storyboard();

            // Add slide from the right animation
            sb.AddSlideToLeft(seconds, element.ActualWidth, keepMargin: keepMargin);

            // Add fade out animation
            sb.AddFadeOut(seconds);

            // Start animating
            sb.Begin(element);

            // Make the element visible
            element.Visibility = Visibility.Visible;

            // Wait for it to finish
            await Task.Delay((int)(seconds * 1000));
        }
    }
}
