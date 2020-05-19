using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace DynamicCollection
{
    /// <summary>
    /// Converter that makes it possible for a single <see cref="ListView"/> to change it's ItemSource
    /// to another collection that contains a different type.
    /// </summary>
    public class DynamicListToGridViewConverter :
                 MarkupExtension, 
                 IValueConverter
    {
        /// <summary>
        /// Converts a Collection with dynamics to the correct GridView
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Create the View
            var View = new GridView();

            // Can fail if it is not a collection
            try
            {
                // Get the first item in the collection
                dynamic Item = ((dynamic)value)[0];

                // Get the type
                var Type = Item.GetType();

                // Check if all items in the collection is the same type
                for (int i = 0; i < ((dynamic)value).Count; i++)
                    if (((dynamic)value)[i].GetType() != Type)
                        // If not then throw an exception
                        throw new Exception("Collection is not consistant");

                // If not a class, Just show the value
                if (!Type.IsClass)
                    // Create the single column
                    View.Columns.Add(new GridViewColumn()
                    {
                        // Set the header to "Value"
                        Header = "Value"
                    });

                // Get the properties
                PropertyInfo[] Properties = Type.GetProperties();

                // Add all columns
                for (int i = 0; i < Properties.Length; i++)
                {
                    // Add a new column and create the binding
                    View.Columns.Add(new GridViewColumn()
                    {
                        // Create the header, set it to the name of the property
                        Header = Properties[i].Name,

                        // Set the binding path to the name of the property
                        DisplayMemberBinding = new Binding(Properties[i].Name),
                    });
                }
            }
            // Catch the exception
            catch (RuntimeBinderException ex)
            {
                // Print the exception type
                throw new Exception(ex.Message);
            }

            // Return the new View
            return View;
        }

        /// <summary>
        /// Converts it back to a Collection
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Does not need implementation
            throw new NotImplementedException();
        }

        /// <summary>
        /// Provide a new instance of the class to the View
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new DynamicListToGridViewConverter();
        }
    }
}
