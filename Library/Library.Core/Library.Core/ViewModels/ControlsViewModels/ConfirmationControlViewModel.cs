using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.Core
{
    public class ConfirmationControlViewModel : BaseViewModel
    {
        public ICommand Confirm { get; set; }

        public ICommand Abort { get; set; }


        public ConfirmationControlViewModel()
        {
            Confirm = new RelayCommand(async () => await ConfirmCommand());
        }

        private async Task ConfirmCommand()
        {
            IoC.CreateInstance<TableControlViewModel>().WantToDelete = true;
            IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();

            await Task.Delay(1);
        }
    }
}
