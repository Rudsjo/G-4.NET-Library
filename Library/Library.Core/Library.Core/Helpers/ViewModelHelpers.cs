using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Library.Core
{
    /// <summary>
    /// Helpers class with basic functions to be used through multiple view models
    /// </summary>
    public static class ViewModelHelpers
    {
        /// <summary>
        /// Method to reset all the input properties once an item is added
        /// </summary>
        /// <param name="propertyNames">The names of the properties to reset</param>
        /// <returns></returns>
        public static void ResetInputProperties<T>(params string[] propertyNames)
            where T : class, new()
        {
            // Setting all sent in properties to default values
            foreach(var property in propertyNames)
                IoC.CreateInstance<T>().GetType().GetProperty(property).SetValue(
                    IoC.CreateInstance<T>(), "", null);
        }
    }
}
