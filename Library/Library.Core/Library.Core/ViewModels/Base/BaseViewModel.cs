using System.ComponentModel;


namespace Library.Core
{
    /// <summary>
    /// A base view model to hold basic functionality for view models
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Event to trigger when a property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Method to trigger the <see cref="PropertyChanged"/> event
        /// </summary>
        /// <param name="name">The name of the property</param>
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
