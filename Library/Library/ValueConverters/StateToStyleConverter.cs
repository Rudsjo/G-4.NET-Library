using Library.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
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

            // Check for the correct data model
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
