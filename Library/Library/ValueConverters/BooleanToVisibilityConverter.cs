using Library.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;

namespace Library
{
    /// <summary>
    /// Converter that takes a boolean value and returns a <see cref="Visibility"/>
    /// </summary>
    public class BooleanToVisibilityConverter : BaseValueConverter<BooleanToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If no parameter is set...
            if (parameter == null)
                return (bool)value ? Visibility.Visible : Visibility.Hidden;

            // If it's a placeholder
            else if ((string)parameter == "Placeholder")
                return (bool)value ? Visibility.Hidden : Visibility.Visible;

            else if ((string)parameter == "OnlyUser")
                return (bool)value ?
                    IoC.CreateInstance<ApplicationViewModel>().CurrentUser.roleID == 2 || IoC.CreateInstance<ApplicationViewModel>().CurrentUser.roleID == 3 ?
                    Visibility.Hidden : Visibility.Visible : Visibility.Hidden;

            else if ((string)parameter == "RemovedArticles")
                return (bool)value ?
                    IoC.CreateInstance<ApplicationViewModel>().CurrentPage != ApplicationPages.BookPage ? Visibility.Collapsed : Visibility.Visible : Visibility.Collapsed;

            else if ((string)parameter == "BookPageOnly")
                return (ApplicationPages)value == ApplicationPages.BookPage ? Visibility.Visible : Visibility.Collapsed;

            else if((string)parameter == "CheckEditUser")
            {
                return ((int)value == 1 || (int)value == 2) ? 
                    IoC.CreateInstance<ApplicationViewModel>().CurrentUser.roleID == 1 ? Visibility.Visible : Visibility.Hidden : Visibility.Visible;
            }
                

            //Shows the loan button if article is available
            else if ((string)parameter == "LoanArticle")
            {
                var boolVal = (bool)value;

                if (boolVal == true)
                    return Visibility.Visible;

                else
                    return Visibility.Collapsed;
            }

            //Show the reserve button if article is not available
            else if ((string)parameter == "ReserveArticle")
            {
                var boolVal = (bool)value;

                if (boolVal == true)
                    return Visibility.Visible;

                else
                    return Visibility.Collapsed;
            }

            else if ((string)parameter == "Skeleton")
                return (bool)value ? Visibility.Collapsed : Visibility.Visible;

            // If it's a warning text
            else if ((string)parameter == "ErrorText")
                return (bool)value ? Visibility.Visible : Visibility.Collapsed;

            else if ((string)parameter == "UserBlocked")
                return (bool)value ? Visibility.Visible : Visibility.Collapsed;

            else if ((string)parameter == "UserNotBlocked")
                return (bool)value ? Visibility.Collapsed : Visibility.Visible;

            // Else...
            else
                return (bool)value ? Visibility.Hidden : Visibility.Visible;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
