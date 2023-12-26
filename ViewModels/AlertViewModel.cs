using hci_restaurant.Services;
using System.Windows.Input;
using WPF_LoginForm.ViewModels;

namespace hci_restaurant.ViewModels
{
    public class AlertViewModel : ViewModelBase
    {
        private string message;
        private readonly IWindowService windowService = new WindowService();
        private bool isCorrect = true;

        public bool IsCorrect
        {
            get { return isCorrect; }
            set
            {
                isCorrect = value;
                OnPropertyChanged(nameof(IsCorrect));
            }
        }

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

        public AlertViewModel(string message, bool isCorrect)
        {
            CloseCommand = new ViewModelCommand(Close);
            Message = message;
            IsCorrect = isCorrect;
        }

        private void Close(object parameter)
        {
            windowService.Close(this);
        }
    }
}
