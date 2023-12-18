using hci_restaurant.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_LoginForm.ViewModels;

namespace hci_restaurant.ViewModels
{
    public class AlertViewModel : ViewModelBase
    {
        private string message;
        private IWindowService windowService = new WindowService();

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public ICommand CloseCommand {  get; set; }
        
        public AlertViewModel()
        {
            CloseCommand = new ViewModelCommand(Close);
        }

        public AlertViewModel(string message)
        {
            CloseCommand = new ViewModelCommand(Close);
            Message = message;
        }

        private void Close(object parameter)
        {
            windowService.Close(this);
        }
    }
}
