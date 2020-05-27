using Library.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Library
{
    public class IntToStringConverter : BaseValueConverter<IntToStringConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Returns an empty string if an int is 0
            if ((int)value == 0 && (string)parameter == null)
                return "";

            // Get the reason id and convert it to string 
            if((string)parameter == "GetReason")
            {
                foreach(var reason in IoC.CreateInstance<ApplicationViewModel>().CurrentReasons)
                {
                    if ((int)value == 0)
                        return "";

                    if (reason.reasonID == (int)value)
                        return reason.reason;
                }
            }

            return value;

        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
