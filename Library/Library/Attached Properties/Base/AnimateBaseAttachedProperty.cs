using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media.Animation;

namespace Library
{
    /// <summary>
    /// A base attached property class to run any animation method when a boolean value is set to true
    /// and a reverse animation when set to false
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    public abstract class AnimateBaseAttachedProperty<Parent> : BaseAttachedProperty<Parent, bool>
        where Parent : BaseAttachedProperty<Parent, bool>, new()
    {

        #region Public Properties

        /// <summary>
        /// A flag indicating if this is the first time this property has been loaded
        /// </summary>
        public bool FirstLoad { get; set; }

        #endregion

        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            // Make sure the sender is a framework element
            if (!(sender is FrameworkElement element))
                return;

            // Don't fire if value hasn't changed and ignore if it's first load
            if (sender.GetValue(ValueProperty) == value && !FirstLoad)
                return;

            // On first load...
            if(FirstLoad)
            {
                // Create a single seld-unhookable event for the elements Loaded event
                RoutedEventHandler onLoaded = null;
                onLoaded = (ss, ee) =>
                {
                    // Unhook
                    element.Loaded -= onLoaded;

                    // Do desired animation
                    DoAnimation(element, (bool)value);

                    // Indicate that first load is done
                    FirstLoad = false;
                };

                // Hook into the Loaded event of the element
                element.Loaded += onLoaded;
            }

            // If not first load
            else
            {
                // Do any desired animation
                DoAnimation(element, (bool)value);
            }
        }

        /// <summary>
        /// The animation method that is fired when the value changes
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="value">Indicator if the animation should take place or not</param>
        protected virtual void DoAnimation(FrameworkElement element, bool value) { }
    }
}
