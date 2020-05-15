using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Library.Core
{
    /// <summary>
    /// Base command to run a given action
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Private Members

        /// <summary>
        /// The action to run
        /// </summary>
        private Action _action;

        #endregion

        #region Public Events

        /// <summary>
        /// The event that's fired when the <see cref="CanExecute(object)"/> value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion

        #region Constructor

        /// <summary>
        /// Deafult constructor
        /// </summary>
        /// <param name="action">The action to run</param>
        public RelayCommand(Action action)
        {
            _action = action;
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Checks if the execution of the command is possible
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>Always true</returns>
        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// Execute the given action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter) => _action.Invoke();

        #endregion
    }
}
