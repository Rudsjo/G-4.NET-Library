using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Library
{
    public class ButtonWithIcon : Button
    {
        /// <summary>
        /// Dependency property for setting an icon inside of a <see cref="Button"/>
        /// </summary>
        public static readonly DependencyProperty IconContentProperty =
            DependencyProperty.Register("IconContent", typeof(string), typeof(ButtonWithIcon), new PropertyMetadata(default));


        /// <summary>
        /// Getter and setter for the dependency property <see cref="IconContentProperty"/>
        /// </summary>
        public string IconContent
        {
            get { return (string)GetValue(IconContentProperty); }
            set { SetValue(IconContentProperty, value); }
        }



        public ICommand ButtonInButtonCommand
        {
            get { return (ICommand)GetValue(ButtonInButtonProperty); }
            set { SetValue(ButtonInButtonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonInButtonProperty =
            DependencyProperty.Register(nameof(ButtonInButtonCommand), typeof(ICommand), typeof(ButtonWithIcon), new PropertyMetadata(default));


    }

    /// <summary>
    /// Dependency property to set an icon inside of a <see cref="TextBox"/>
    /// </summary>
    public class ContentControlWithIcon : ContentControl
    {
        /// <summary>
        /// Dependency property for setting an icon inside of a textbox
        /// </summary>
        public static readonly DependencyProperty ContentIconProperty =
            DependencyProperty.Register(nameof(ContentIcon), typeof(string), typeof(ContentControlWithIcon), new PropertyMetadata(default));


        /// <summary>
        /// Getter and setter for the dependency property <see cref="IconContentProperty"/>
        /// </summary>
        public string ContentIcon
        {
            get { return (string)GetValue(ContentIconProperty); }
            set { SetValue(ContentIconProperty, value); }
        }

       

        /// <summary>
        /// Dependency property to bind a input value from a textbox located in <see cref="ContentControlWithIcon"/>
        /// </summary>
        public static readonly DependencyProperty UserInputProperty =
            DependencyProperty.Register(nameof(UserInput), typeof(string), typeof(ContentControlWithIcon), new PropertyMetadata(default));

        public string UserInput
        {
            get { return (string)GetValue(UserInputProperty); }
            set { SetValue(UserInputProperty, value); }
        }


    }
}
