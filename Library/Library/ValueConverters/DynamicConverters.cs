namespace Library
{
    #region Namespaces
    using System;
    using System.Windows;
    using System.Reflection;
    using System.Windows.Data;
    using System.Windows.Media;
    using System.Globalization;
    using System.Windows.Controls;
    using System.Collections.ObjectModel;
    using Microsoft.CSharp.RuntimeBinder;
    #endregion

    public class DynamicListToHeaderTemplateConverter :
                 BaseValueConverter<DynamicListToHeaderTemplateConverter>
    {
        /// <summary>
        /// Converts a dynamic collection into a <see cref="DataTemplate"/> for a <see cref="HeaderedItemsControl"/>.HeaderTemplate
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Create the View
            var Headers = new DataTemplate();

            if (value == null)
                return Headers;

            // Create the main border
            FrameworkElementFactory MainBorder =                new FrameworkElementFactory(typeof(Border));
            MainBorder.SetValue(Border.BackgroundProperty,      new SolidColorBrush(Color.FromRgb(255, 255, 255)));
            MainBorder.SetValue(Border.BorderThicknessProperty, new Thickness(0, 0, 0, 1));
            MainBorder.SetValue(Border.BorderBrushProperty, App.Current.Resources["CustomLightGreyBrush"]);
            MainBorder.SetValue(Border.HeightProperty, 50.0D);

            // Create the stackpanel
            FrameworkElementFactory MainStack = new FrameworkElementFactory(typeof(StackPanel));
            MainStack.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            // Can fail if it is not a collection
            try
            {
                // Get the first item in the collection
                dynamic Item = ((dynamic)value)[0];

                // Get the type
                var Type = Item.GetType();

                // Check if all items in the collection is the same type
                for (int i = 0; i < ((dynamic)value).Count; i++)
                {
                    if (((dynamic)value)[i].GetType() != Type)
                        // If not then throw an exception
                        throw new Exception("Collection is not consistant");
                }

                // Get the properties
                PropertyInfo[] Properties = Type.GetProperties();

                // Set the datatemplates data type
                Headers.DataType = Properties[0].GetType();

                // Add all columns
                for (int i = 0; i < Properties.Length; i++)
                {
                    // Create the header container
                    FrameworkElementFactory Content = new FrameworkElementFactory(typeof(TextBlock));

                    // Set all the values
                    Content.SetValue(TextBlock.TextProperty, Properties[i].Name);
                    Content.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                    Content.SetValue(TextBlock.VerticalAlignmentProperty,   VerticalAlignment.Center);
                    Content.SetValue(TextBlock.TextAlignmentProperty,       TextAlignment.Center);
                    Content.SetValue(TextBlock.FontFamilyProperty, App.Current.Resources[string.Format("PassionOneRegular")]);
                    Content.SetValue(TextBlock.FontSizeProperty,   App.Current.Resources[string.Format("FontSizeSmall")]);
                    Content.SetValue(TextBlock.ForegroundProperty, App.Current.Resources[string.Format("CustomGreyBrush")]);
                    Content.SetValue(TextBlock.WidthProperty, 140.0D);

                    // Append the item to the stackpanel
                    MainStack.AppendChild(Content);
                }
            }
            // Catch the exception
            catch (RuntimeBinderException ex)
            {
                // Print the exception type
                throw new Exception(ex.Message);
            }

            // Append the stackpanel to the main grid
            MainBorder.AppendChild(MainStack);

            // Set the DataTemplates VisualTree to the new border
            Headers.VisualTree = MainBorder;

            // Return the new DataTemplate
            return Headers;
        }

        /// <summary>
        /// Converts it back to a Collection
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Does not need implementation
            throw new NotImplementedException();
        }
    }

    public class DynamicListToItemTemplateConverter : 
                 BaseValueConverter<DynamicListToItemTemplateConverter>
    {
        /// <summary>
        /// Converts a dynamic collection into a <see cref="DataTemplate"/> for a <see cref="HeaderedItemsControl"/>.ItemTemplate
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Create a new DataTemplate
            var Headers = new DataTemplate();

            if (value == null)
                return Headers;

            // Create the main border
            FrameworkElementFactory MainBorder = new FrameworkElementFactory(typeof(Border));
            MainBorder.SetValue(Border.BackgroundProperty, new SolidColorBrush(Color.FromRgb(255, 255, 255)));
            MainBorder.SetValue(Border.BorderBrushProperty, App.Current.Resources["CustomLightGreyBrush"]);
            MainBorder.SetValue(Border.BorderThicknessProperty, new Thickness(0, 0, 0, 1));          
            MainBorder.SetValue(Border.PaddingProperty, new Thickness(0, 20, 0, 20));            

            // Create the stackpanel
            FrameworkElementFactory MainStack = new FrameworkElementFactory(typeof(StackPanel));
            MainStack.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            // Can fail
            try
            {
                // Get all items from the probided value
                var Items = (ObservableCollection<dynamic>)value;

                // Get the item type from the first element
                var CurrentType = Items[0].GetType();

                // Get the properties
                PropertyInfo[] Properties = CurrentType.GetProperties();

                for (int i2 = 0; i2 < Properties.Length; i2++)
                {
                    // Create the scrollViewer
                    FrameworkElementFactory scroll = new FrameworkElementFactory(typeof(ScrollViewer));
                    scroll.SetValue(ScrollViewer.WidthProperty, 140.0D);
                    scroll.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty,
                        ScrollBarVisibility.Auto);
                    // Create the header container
                    FrameworkElementFactory Content = new FrameworkElementFactory(typeof(TextBlock));

                    // Set all properties for the TextBlock
                    Content.SetValue(TextBlock.TextProperty, new Binding(Properties[i2].Name));
                    Content.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                    Content.SetValue(TextBlock.VerticalAlignmentProperty,   VerticalAlignment.Center);
                    Content.SetValue(TextBlock.TextAlignmentProperty,       TextAlignment.Center);
                    Content.SetValue(TextBlock.StyleProperty, App.Current.Resources[string.Format("TableItemText")]);

                    // Append the item to the stackpanel
                    scroll.AppendChild(Content);
                    // Append the ScrollViewer to the Stackpanel
                    MainStack.AppendChild(scroll);
                }
            }
            // Catch the exception
            catch (RuntimeBinderException ex)
            {
                // Print the exception type
                throw new Exception(ex.Message);
            }

            // Append the stackpanel to the main grid
            MainBorder.AppendChild(MainStack);

            // Set the DataTemplates VisualTree to the new border
            Headers.VisualTree = MainBorder;

            // Return the new DataTemplate
            return Headers;
        }

        /// <summary>
        /// Converts it back to a Collection
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Does not need implementation
            throw new NotImplementedException();
        }
    }

}
