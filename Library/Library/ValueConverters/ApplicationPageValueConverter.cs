﻿using Library.Core;
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
                                {
                                    IoC.CreateInstance<ApplicationViewModel>().PageLoadComplete = false;
                                    return new MainPage();
                                }

                            case ApplicationPages.BookPage:
                                {
                                    IoC.CreateInstance<ApplicationViewModel>().PageLoadComplete = false;
                                    return new BookPage();
                                }

                            case ApplicationPages.EmployeePage:
                                {
                                    IoC.CreateInstance<ApplicationViewModel>().PageLoadComplete = false;
                                    return new EmployeePage();

                                }

                            case ApplicationPages.ReportPage:
                                {
                                    IoC.CreateInstance<ApplicationViewModel>().PageLoadComplete = false;
                                    return new ReportPage();
                                }

                            // Default
                            default:
                                {
                                    IoC.CreateInstance<ApplicationViewModel>().PageLoadComplete = false;
                                    return new BackupPage();

                                }
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
