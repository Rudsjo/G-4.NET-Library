using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Library
{
    /// <summary>
    /// An abstract base value converter to hold basic parts of the value converter, 
    /// including the <see cref="MarkupExtension"/> to make the value converter accessible in XAML
    /// </summary>
    /// <typeparam name="T">The value converter to be implemented</typeparam>
    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter
        where T : class, new()
    {
        #region Private Members

        /// <summary>
        /// A single static instance of this value converter
        /// </summary>
        private static T Converter = null;

        #endregion

        #region Value Converter Methods

        /// <summary>
        /// The method to convert one type to another
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// The method to convert a value back to its source type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        #endregion

        #region Markup Extension Methods

        /// <summary>
        /// Provides a static interface of the value converter to make it accessible in XAML
        /// </summary>
        /// <param name="serviceProvider">The service provider</param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            // Returns the converter and creates a new instance in case it's null
            return Converter ?? (Converter = new T());
        }

        #endregion
    }
}
