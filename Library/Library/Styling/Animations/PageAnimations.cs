using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Library
{
    /// <summary>
    /// Helpers to animate pages in specific ways
    /// </summary>
    public static class PageAnimations
    {
        /// <summary>
        /// Animation to slide and fade in a page from the right
        /// </summary>
        /// <param name="page">The page to animate</param>
        /// <param name="seconds">The time of the animation</param>
        /// <returns></returns>
        public static async Task SlideAndFadeInFromTheRight(this Page page, float seconds)
        {
            // Creates the storyboard
            var sb = new Storyboard();

            // Add slide from right animation
            sb.AddSlideFromRight(seconds, page.WindowWidth);

            // Add fade in animation
            sb.AddFadeIn(seconds);

            // Start animating
            sb.Begin(page);

            // Make page visible
            page.Visibility = Visibility.Visible;

            // Wait for it to finish
            await Task.Delay((int)(seconds * 1000));
        }

        /// <summary>
        /// Animation to flip and fade out a page to the left
        /// </summary>
        /// <param name="page">The page to animate</param>
        /// <param name="seconds">The time of the animation</param>
        /// <returns></returns>
        public static async Task FlipAndFadeOut(this Page page, float seconds)
        {
            // Creates the storyboard
            var sb = new Storyboard();

            // Add flip out animation on the horizontal line
            sb.AddFlipOutX(seconds, -80);

            // Add flip out animation on the vertical line
            sb.AddFlipOutY(seconds, -80);

            // Add fade out animation
            sb.AddFadeOut(seconds);

            // Start animating
            sb.Begin(page);

            // Make page visible
            page.Visibility = Visibility.Visible;

            // Wait for it to finish
            await Task.Delay((int)(seconds * 1000));
        }


    }
}
