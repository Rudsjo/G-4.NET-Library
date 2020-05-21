using System;
using System.Windows.Input;

namespace Library.Core
{
    public class RelayParameterizedCommand : ICommand
    {
        #region Private Members

        /// <summary>
        /// The action to run
        /// </summary>
        private Action<object> _action;

        #endregion

        #region Public Events

        /// <summary>
        /// Event that fires when <see cref="CanExecute(object)"/> is changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="action">The action to run</param>
        public RelayParameterizedCommand(Action<object> action)
        {
            _action = action;
        }

        #endregion

        /// <summary>
        /// Checks if the execution of the command is possible
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>Always true</returns>
        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// Execute the given action
        /// </summary>
        /// <param name="parameter">The parameter to send through the action</param>
        public void Execute(object parameter) => _action.Invoke(parameter);
    }
}
