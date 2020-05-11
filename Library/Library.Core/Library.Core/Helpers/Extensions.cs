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
            // Get the property info of the sent in property
            var propertyInfo = listToSort.First().GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            // Get the property info of the IsPlaceholder property
            var propertyInfoOfPlaceholder = listToSort.First().GetType().GetProperty(placeholderPropertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            // Get the ordered list
            listToSort = listToSort.OrderBy(e => propertyInfo.GetValue(e, null)).Where(x => (bool)propertyInfoOfPlaceholder.GetValue(x) != true);

            // Return the list with added placeholders
            return listToSort.FillPlaceHolders().ToList().ToObservableCollection();
        }

        #endregion
    }
}
