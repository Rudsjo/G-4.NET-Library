using Library.Core;
using System;
using System.Globalization;
using System.Windows.Documents;

namespace Library
{
    /// <summary>
    /// Value converter to create a new instance of a page depending on the navigation of the user
    /// </summary>
    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)parameter)
            {
                case "Main":
                    {
                        // Checks the current page
                        switch ((ApplicationPages)value)
                        {

                            case ApplicationPages.MainPage:
                                return new MainPage();

                            case ApplicationPages.BookPage:
                                return new BookPage();

                            case ApplicationPages.EmployeePage:
                                return new EmployeePage();

                            // Default
                            default:
                                return new BackupPage();
                        }
                    }

                case "Table":
                    {
                        switch ((ApplicationPages)value)
                        {
                            case ApplicationPages.BookPage:
                                return new ArticleTableControl();

                            case ApplicationPages.EmployeePage:
                                return new UserTableControl();

                            default:
                                return null;
                        }
                    }

                default:
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
