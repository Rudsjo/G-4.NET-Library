using Library.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace Library
{
    /// <summary>
    /// Converter to convert a state to a background color
    /// <usage>
    /// Used for table header items
    /// </usage>
    /// </summary>
    public class StateToStyleConverter : BaseValueConverter<StateToStyleConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            #region Brushes
            // Set the color of a chosen table
            var sortedBrush = new SolidColorBrush(Color.FromRgb(237, 237, 237));

            // Set the default brush
            var defaultBrush = Brushes.Transparent;

            #endregion

            #region Styles

            // Style for an active navigation button
            var activeStyle = App.Current.Resources[string.Format("ActiveSideMenuButton")];

            // Style for a default navigation button
            var defaultStyle = App.Current.Resources[string.Format("SideMenuButton")];

            #endregion

            // Check the type of the value
            var type = value.GetType();

            // Check what tabel to sort by and change the background of that button
            if (type == typeof(SortableTables))
            {
                // Return the correct background color
                switch ((SortableTables)value)
                {
                    case SortableTables.PersonalNumber:
                        {
                            if ((string)parameter == "PNumber")
                                return sortedBrush;
                            else
                                return defaultBrush;
                        }

                    case SortableTables.FirstName:
                        {
                            if ((string)parameter == "FName")
                                return sortedBrush;
                            else
                                return defaultBrush;
                        }

                    case SortableTables.LastName:
                        {
                            if ((string)parameter == "LName")
                                return sortedBrush;
                            else
                                return defaultBrush;
                        }

                    case SortableTables.LoanedArticles:
                        {
                            if ((string)parameter == "LArticles")
                                return sortedBrush;
                            else
                                return defaultBrush;
                        }

                    case SortableTables.ReservedArticles:
                        {
                            if ((string)parameter == "RArticles")
                                return sortedBrush;
                            else
                                return defaultBrush;
                        }
                    default:
                        return defaultBrush;
                }
            }

            // Returns the correct style for the buttons in the side menu
            else if (type == typeof(ApplicationPages))
            {
                switch ((ApplicationPages)value)
                {
                    // Return the correct style
                    case ApplicationPages.BookPage:
                        {
                            if ((string)parameter == "BookPage")
                                return activeStyle;
                            else
                                return defaultStyle;
                        }

                    case ApplicationPages.EmployeePage:
                        {
                            if ((string)parameter == "EmployeePage")
                                return activeStyle;
                            else
                                return defaultStyle;
                        }

                    case ApplicationPages.CustomerPage:
                        {
                            if ((string)parameter == "CustomerPage")
                                return activeStyle;
                            else
                                return defaultStyle;
                        }
                }
            }

            else if(type == typeof(string))
            {
                string content = (string)value;

                //Sets warninglabel as collapsed if textboxes are empty
                if (content == "")
                    return Visibility.Collapsed;

                //Checks so that the personal Number is ten numbers long and only contains numbers
                if((string)parameter == "PN")
                {
                    if (content.Length == 10 && Regex.IsMatch(content, "^[0-9]*$"))
                        return Visibility.Collapsed;
                    else
                        return Visibility.Visible;
                }

                //Checks so that the First name does not start with whitespace
                if((string)parameter == "FN")
                {
                    if (!content.StartsWith(" "))
                        return Visibility.Collapsed;
                    else
                        return Visibility.Visible;
                }

                //Checks so that the Last name does not start with whitespace
                if ((string)parameter == "LN")
                {
                    if (!content.StartsWith(" "))
                        return Visibility.Collapsed;
                    else
                        return Visibility.Visible;
                }
            }

            return defaultBrush;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
