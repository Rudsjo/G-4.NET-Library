namespace Library.Core
{
    /// <summary>
    /// Required nemspaces
    /// </summary>
    #region Namespaces
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Security;
    using System.Threading.Tasks;
    using static System.Runtime.InteropServices.Marshal;
    #endregion

    /// <summary>
    /// Extension methods for the <see cref="SecureString"/> class.
    /// </summary>
    public static class Extensions
    {
        #region Extensions for the SecureStringClass

        /// <summary>
        /// Reveals the string of the SecureString
        /// </summary>
        /// <param name="_secureString">The secure string.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToUnsecureString(this SecureString _secureString)
        {         
            // Check if the secure string is null
            if (_secureString == null)
                return null;

            // Create a pointer
            IntPtr pointer = IntPtr.Zero;

            // Get the string from the secure string
            try
            {
                // Copy the content from the SecureString into unmanaged memory
                pointer = SecureStringToCoTaskMemUnicode(_secureString);

                // Return the content that the new pointer is pointing to
                return PtrToStringUni(pointer);
            }
            // Finally, clean up at the memory address 
            finally
            {
                // Free the memory
                ZeroFreeGlobalAllocUnicode(pointer);
            }

        }

        #endregion

        #region String extensions

        /// <summary>
        /// Converts to securestring.
        /// </summary>
        /// <param name="UnsecureString">The unsecure string.</param>
        /// <returns></returns>
        public static SecureString ToSecureString(this string UnsecureString)
        {
            // Check if the string is null
            if (String.IsNullOrEmpty(UnsecureString))
                return null;

            // Create a SecureString
            var Result = new SecureString();
            // Append all characters from the string to the SecureString
            foreach (char c in UnsecureString) Result.AppendChar(c);

            // Return the new SecureString
            return Result;
        }

        #endregion

        #region List Extensions

        /// <summary>
        /// Extension method to convert any <see cref="IEnumerable"/> object to an <see cref="ObservableCollection{T}"/>
        /// </summary
        /// <typeparam name="T">The type the list is filled with</typeparam>
        /// <typeparam name="L">The type of list</typeparam>
        /// <param name="listToConvert">The list to convert</param>
        /// <returns></returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> listToConvert)
        {
            // Creating the observable collection to return
            var collectionToReturn = new ObservableCollection<T>();

            // Getting base functionality from the List class to perform the convertion
            var listToConvertAsList = (listToConvert as List<T>);

            // Get all values from the sent in list and send them into the collection
            if(listToConvertAsList != null)
                listToConvertAsList.ForEach((x) =>
                {
                    collectionToReturn.Add(x);
                });

            // Returning
            return collectionToReturn;
        }

        /// <summary>
        /// Extension method to fill a list with placeholder dummy data
        /// </summary>
        /// <typeparam name="T">The type of the list content</typeparam>
        /// <typeparam name="L">The type of the list</typeparam>
        /// <param name="listToFill">The list to fill up</param>
        /// <param name="maxNumberOfItems">The maximum number of items</param>
        /// <returns></returns>
        public static IEnumerable<T> FillPlaceHolders<T>(this IEnumerable<T> listToFill, int maxNumberOfItems = 8, string placeholderPropertyName = "IsPlaceholder")
            where T : new()
        {
            // Creating a nee temporary list
            var tempList = new List<T>();

            // Filling all the existing items
            if(listToFill != null)
                foreach (var item in listToFill)
                    tempList.Add((T)item);

            // Get the type
            var type = typeof(T);

            // Make sure the type has the IsPlaceholder property
            if (type.GetProperty(placeholderPropertyName) == null)
                // If it's not the same list will be returned
                return tempList.ToObservableCollection();


            // Adding remaining dummies
            while (tempList.Count < maxNumberOfItems)
            {
                // Creating a temporary item
                var tempItem = new T();

                // Setting the IsPlaceholder property
                tempItem.GetType().GetProperty(placeholderPropertyName).SetValue(tempItem, true);

                // Adds the item to the list
                tempList.Add(tempItem);

            }

            // Returns the list as an observable collection
            return tempList;
        }

        /// <summary>
        /// Sorting a <see cref="IEnumerable{T}"/> from a given property name
        /// </summary>
        /// <typeparam name="T">The type the list is filled with</typeparam>
        /// <param name="listToSort">The list to sort</param>
        /// <param name="propertyName">The name of the property to sort from</param>
        /// <param name="placeholderPropertyName">The property name to indicate if an object is a placeholder</param>
        /// <returns>A sorted list with placeholders if needed</returns>
        public static IEnumerable<T> SortByPropertyName<T>(this IEnumerable<T> listToSort, string propertyName, string placeholderPropertyName = "IsPlaceholder")
        where T : class, new()
        {
            if (listToSort == null || listToSort.ToList().Count == 0)
                return new ObservableCollection<T>().FillPlaceHolders();

            // Get the property info of the sent in property
            var propertyInfo = listToSort.First().GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            // Get the property info of the IsPlaceholder property
            var propertyInfoOfPlaceholder = listToSort.First().GetType().GetProperty(placeholderPropertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            // Get the ordered list
            listToSort = listToSort.OrderBy(e => propertyInfo.GetValue(e, null)).Where(x => (bool)propertyInfoOfPlaceholder.GetValue(x) != true);

            // Return the list with added placeholders
            return listToSort;
        }

        public static T ToModel<I, T>(this I objectToConvertFrom)
            where T : I, new()
        {
            // Create a new model to return
            var modelToReturn = new T();

            // Get all properties
            var properties = typeof(I).GetProperties();

            // Get and set all the property values
            for(int index = 0; index < properties.Length; index++)
            {
                modelToReturn.GetType().GetProperty(properties[index].Name).SetValue(
                modelToReturn, objectToConvertFrom.GetType().GetProperty(properties[index].Name).GetValue(objectToConvertFrom));
            }

            // Return the model
            return modelToReturn;

        }


        /// <summary>
        /// Takes a list from the database and converts it to a viewModel
        /// </summary>
        /// <typeparam name="I">The type of interface to send in</typeparam>
        /// <typeparam name="T">The type of ViewModel to return</typeparam>
        /// <param name="modelList">List to fill from</param>
        /// <returns>a list of the new viewmodels</returns>
        public static IEnumerable<T> ToModelDataToViewModel<I, T>(this IEnumerable<I> modelList)
            // interface
            where I : class
            // T has to derive from interface
            where T : I, new()
        {
            // Get the properties of sent in type
            var properties = typeof(I).GetProperties();

            // The list to return
            List<T> listToReturn = new List<T>();

            // Goes through every item in the sent in list
            modelList.ToList().ForEach(item =>
            {
                // Creates a new instance of the type to return
                var newItem = new T();

                // For every item in the sent in list
                Parallel.ForEach(modelList, (currentProperty) =>
                {
                    // Check for property names and set the values
                    for (int index = 0; index < properties.Length; index++)
                    {
                        newItem.GetType().GetProperty(properties[index].Name).SetValue(
                        newItem, item.GetType().GetProperty(properties[index].Name).GetValue(item));
                    }
                });

                // Add the item to the returning list
                listToReturn.Add(newItem);

            });

            // Returns the list
            return listToReturn;
        }

        #region Unused method that got replaces by ToModel<I, T>()
        /// <summary>
        /// Method to copy the properties from one model to another
        /// This needs further comments
        /// 
        /// THIS METHOD IS NOT NEEDED AS THE METHOD ( ToModel ) WORKS FINE FOR THE FUNCTION IN TABLECONTROLVIEWMODEL
        /// 
        /// </summary>
        /// <typeparam name="T"> the model to copy from</typeparam>
        /// <typeparam name="TU"> the model to copy to</typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        //public static void CopyPropertiesTo<T, TU>(this T source, TU destination)
        //{
        //    var sourceProperties = typeof(T).GetProperties().Where(x => x.CanRead).ToList();

        //    var destinationProperties = typeof(TU).GetProperties().Where(x => x.CanWrite).ToList();

        //    foreach (var property in sourceProperties)
        //    {
        //        if (destinationProperties.Any(x => x.Name == property.Name))
        //        {
        //            var p = destinationProperties.First(x => x.Name == property.Name);

        //            if (p.CanWrite)
        //            { // check if the property can be set or not.
        //                p.SetValue(destination, property.GetValue(source, null), null);
        //            }
        //        }

        //    }

        //}
        #endregion

        #endregion

    }
}
