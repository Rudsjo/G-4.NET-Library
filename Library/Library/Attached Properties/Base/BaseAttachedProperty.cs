using System;
using System.Windows;

namespace Library
{
    /// <summary>
    /// Base Attached property to give basic funtionality to other attached properties 
    /// and ease future creation of attached properties
    /// </summary>
    /// <typeparam name="Parent">The class the attached property should be attached to</typeparam>
    /// <typeparam name="Property">The type of the attached property</typeparam>
    public abstract class BaseAttachedProperty<Parent, Property>
        where Parent : new()
    {

        #region Public Events

        /// <summary>
        /// Event to fire when the value changes
        /// </summary>
        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (sender, e) => { };

        /// <summary>
        /// Event to fore when the value changed, even when the value is the same.
        /// Useful when a user for example refreshes a page and we want to run something even though the value is not changed
        /// </summary>
        public event Action<DependencyObject, object> ValueUpdated = (sender, value) => { };

        #endregion

        #region Public Properties

        /// <summary>
        /// A singleton instance of the parent class
        /// </summary>
        public static Parent Instance { get; private set; } = new Parent();

        #endregion

        #region Attached Property Definitions

        /// <summary>
        /// The attached property for this class
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.RegisterAttached("Value",
                                                typeof(Property),
                                                typeof(BaseAttachedProperty<Parent, Property>),
                                                new UIPropertyMetadata(
                                                    default(Property),
                                                    new PropertyChangedCallback(OnValuePropertyChanged),
                                                    new CoerceValueCallback(OnValuePropertyUpdated)
                                                    ));


        /// <summary>
        /// Gets the attached property
        /// </summary>
        /// <param name="obj">The element to get the property from</param>
        /// <returns></returns>
        public static Property GetValue(DependencyObject obj) => (Property)obj.GetValue(ValueProperty);

        /// <summary>
        /// Sets the attahed property
        /// </summary>
        /// <param name="obj">The element to get the property from</param>
        /// <param name="value">The value to set the property to</param>
        public static void SetValue(DependencyObject obj, Property value) => obj.SetValue(ValueProperty, value);

        /// <summary>
        /// The fallback event when the <see cref="ValueProperty"/> is changed
        /// </summary>
        /// <param name="obj">The UI element that had its property changed</param>
        /// <param name="e">The argument for the event</param>
        private static void OnValuePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            // Call the parent function
            (Instance as BaseAttachedProperty<Parent, Property>)?.OnValueChanged(obj, e);

            // Call event listeners
            (Instance as BaseAttachedProperty<Parent, Property>)?.ValueChanged(obj, e);
        }

        /// <summary>
        /// The callback event when <see cref="ValueProperty"/> is changed, even if it's the same value
        /// </summary>
        /// <param name="obj">The UI element that had its property updated</param>
        /// <param name="value">The argument for the event</param>
        /// <returns></returns>
        private static object OnValuePropertyUpdated(DependencyObject obj, object value)
        {
            // Call the parent function
            (Instance as BaseAttachedProperty<Parent, Property>)?.OnValueUpdated(obj, value);

            // Call the event listeners
            (Instance as BaseAttachedProperty<Parent, Property>)?.ValueUpdated(obj, value);

            // Return the value
            return value;
        }

        #endregion

        #region Event Methods

        /// <summary>
        /// The method that is called when any attached property of this type is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }


        /// <summary>
        /// The method that is called when any attached property pof this type is changed, even if the value is the same
        /// Used when the suer for example refreshes a page and the value is the same as before but you want to run something anyways
        /// </summary>
        /// <param name="sender">The UI element that this property was changed for</param>
        /// <param name="value">The arguments for this event</param>
        public virtual void OnValueUpdated(DependencyObject sender, object value) { }

        #endregion

    }
}
