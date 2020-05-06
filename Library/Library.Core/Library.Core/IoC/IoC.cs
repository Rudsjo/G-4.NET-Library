using System;

namespace Library.Core
{
    /// <summary>
    /// The IoC container to create instances of frequently used classes through the application
    /// </summary>
    public static class IoC
    {

        public static object Instance { get; set; }

        #region Conversion Container

        // EXAMPLE:
        //      public static IClass CreateClass() => new Class();

        #endregion



        #region Helper Methods

        /// <summary>
        /// Method to call for a singleton instance of a class
        /// </summary>
        /// <typeparam name="T">The class to create an instance of</typeparam>
        /// <returns>A new instance of the desired class</returns>
        public static T CreateInstance<T>()
            where T : class, new()
            => Singleton<T>.Instance;

        #endregion
    }

    /// <summary>
    /// Creates a singleton instance of a desired class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Singleton<T>
        where T : class, new()
    {
        /// <summary>
        /// Creates the singelton instance
        /// </summary>
        class SingletonCreator 
        { 
            internal static readonly T Instance = new T();        
        }

        /// <summary>
        /// Property to obtain the desired instance
        /// </summary>
        public static T Instance => SingletonCreator.Instance;

    }
}
