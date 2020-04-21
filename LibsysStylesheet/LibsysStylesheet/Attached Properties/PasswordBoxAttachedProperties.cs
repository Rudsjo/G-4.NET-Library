using System.Windows;
using System.Windows.Controls;

namespace LibsysStylesheet
{
    /// <summary>
    /// Attached property to monitor changes in a <see cref="PasswordBox"/>
    /// </summary>
    public class MonitorPasswordProperty : BaseAttachedProperty<MonitorPasswordProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Get the caller
            var passwordBox = sender as PasswordBox;

            // Make sure it's a password box
            if (passwordBox == null)
                return;

            // Remove any previous events
            passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;

            // If the caller set MonitorPassword to true...
            if ((bool)e.NewValue)
            {
                // Set the default value
                HasTextProperty.SetValue(passwordBox);

                // Start listening out for password changes
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }

        /// <summary>
        /// Event that fires when the password box 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            HasTextProperty.SetValue((PasswordBox)sender);
        }
    }

    /// <summary>
    /// Attached property to check if a <see cref="PasswordBox"/> has any text
    /// </summary>
    public class HasTextProperty : BaseAttachedProperty<HasTextProperty, bool>
    {
        /// <summary>
        /// Sets the HasText property based on if the caller <see cref="PasswordBox"/> has any text
        /// </summary>
        /// <param name="sender"></param>
        public static void SetValue(DependencyObject sender)
        {
            SetValue(sender, ((PasswordBox)sender).SecurePassword.Length > 0);
        }
    }
}
