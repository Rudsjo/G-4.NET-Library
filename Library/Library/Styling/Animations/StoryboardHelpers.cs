using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Library
{
    /// <summary>
    /// Animation helpers for <see cref="Storyboard"/>
    /// </summary>
    public static class StoryboardHelpers
    {
        /// <summary>
        /// Adds a slide from the right animation to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time of the animation</param>
        /// <param name="offset">The distance to the right to start from</param>
        /// <param name="deceleartionRatio">The rate of deceleration</param>
        /// <param name="keepMargin">Whether to keep the element at the same width during the animation</param>
        public static void AddSlideFromRight(this Storyboard storyboard, float seconds, double offset, float deceleartionRatio = 0.9f, bool keepMargin = true)
        {
            // Create the margin animate from right
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(keepMargin ? offset : offset, 0, -offset, 0),
                To = new Thickness(0),
                DecelerationRatio = deceleartionRatio
            };

            // Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // Add this to the storyboard
            storyboard.Children.Add(animation);
        }

        /// <summary>
        /// Adds a flip animation in a vertical angle to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time of the animation</param>
        /// <param name="offset">The distance of the flip</param>
        /// <param name="accelerationRatio">The rate of acceleration</param>
        public static void AddFlipOutX(this Storyboard storyboard, float seconds, double offset, float accelerationRatio = 0.2f)
        {
            // Creates the X-angle animation
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                To = offset,
                AccelerationRatio = accelerationRatio
            };

            // Set the target property name for the animation
            Storyboard.SetTargetProperty(animation, new PropertyPath("RenderTransform.(SkewTransform.AngleY)"));

            // Add the animation to the storyboard
            storyboard.Children.Add(animation);
        }

        /// <summary>
        /// Adds a flip animation in a horizontal angle to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard the animation will be added to</param>
        /// <param name="seconds">The time of the animation</param>
        /// <param name="offset">The distance of the flip</param>
        /// <param name="accelerationRatio">The rate of the acceleration</param>
        public static void AddFlipOutY(this Storyboard storyboard, float seconds, double offset, float accelerationRatio = 0.2f)
        {
            // Creates the X-angle animation
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                To = offset,
                AccelerationRatio = accelerationRatio
            };

            // Set the target property name for the animations
            Storyboard.SetTargetProperty(animation, new PropertyPath("RenderTransform.(SkewTransform.AngleY)"));

            // Add the animation to the storyboard
            storyboard.Children.Add(animation);
        }


        /// <summary>
        /// Adds a fade in animation to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        public static void AddFadeIn(this Storyboard storyboard, float seconds)
        {
            // Creates the animation
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = 0,
                To = 1
            };

            // Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));

            // Add this to the storyboard
            storyboard.Children.Add(animation);

        }

        /// <summary>
        /// Adds a fade out animation to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        public static void AddFadeOut(this Storyboard storyboard, float seconds)
        {

            // Creates the animation
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = 1,
                To = 0
            };

            // Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));

            // Adds this to the storyboard
            storyboard.Children.Add(animation);
        }

        /// <summary>
        /// Adds a slide out animation to the left
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <param name="offset">The distance of the slide</param>
        /// <param name="decelerationRatio">The ratio of deceleration</param>
        /// <param name="keepMargin">Whether to keep the element at the same width during the animation or not</param>
        public static void AddSlideToLeft(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
        {
            // Create the margin animate from right
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(0),
                To = new Thickness(-offset, 0, keepMargin ? offset : 0, 0),
                DecelerationRatio = decelerationRatio
            };

            // Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // Add this to the storyboard
            storyboard.Children.Add(animation);
        }
    }
}
