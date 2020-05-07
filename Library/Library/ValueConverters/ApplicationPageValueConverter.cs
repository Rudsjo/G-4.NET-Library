using Library.Core;
using System;
using System.Globalization;

namespace Library
{
    /// <summary>
    /// Value converter to create a new instance of a page depending on the navigation of the user
    /// </summary>
    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Checks the current page
            switch ((ApplicationPages)value)
            {

                case ApplicationPages.MainPage:
                    return new MainPage();

                case ApplicationPages.BookPage:
                    return new BookPage();

                // Default
                default:
                    return new MainPage();
            }

        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
