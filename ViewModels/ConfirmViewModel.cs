using hci_restaurant.Services;
using System.Windows.Input;
using WPF_LoginForm.ViewModels;

namespace hci_restaurant.ViewModels
{
    public class ConfirmViewModel : ViewModelBase
    {
        private string message;
        public static bool CanBe { get; set; }
        private readonly IWindowService windowService = new WindowService();

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ConfirmViewModel()
        {
            ConfirmCommand = new ViewModelCommand(ExecuteConfirmation);
            CancelCommand = new ViewModelCommand(ExecuteCancelation);
        }

        public ConfirmViewModel(string message)
        {
            ConfirmCommand = new ViewModelCommand(ExecuteConfirmation);
            CancelCommand = new ViewModelCommand(ExecuteCancelation);
            Message = message;
        }

        private void ExecuteConfirmation(object parameter)
        {
            CanBe = true;
            Close();
        }

        private void ExecuteCancelation(object parameter)
        {
            CanBe = false;
            Close();
        }

        private void Close()
        {
            windowService.Close(this);
        }
    }
}
