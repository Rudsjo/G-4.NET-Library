using System.Windows;
using System.Windows.Controls;

namespace Library
{
    public class ButtonWithIcon : Button
    {
        /// <summary>
        /// Dependency property for setting an icon inside of a contentpresenter,
        /// for example a button
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
    }
}
