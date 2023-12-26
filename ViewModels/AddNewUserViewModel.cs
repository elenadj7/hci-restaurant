using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using hci_restaurant.Repositories;
using hci_restaurant.Services;
using MySqlConnector;
using Prism.Events;
using System.Windows;
using System.Windows.Input;
using WPF_LoginForm.ViewModels;

namespace hci_restaurant.ViewModels
{
    public class AddNewUserViewModel : ViewModelBase
    {
        private string username;
        private string password;
        private string name;
        private string surname;
        private string salaryText;
        private int salary;
        private readonly IUserRepository userRepository = new UserRepository();
        private readonly IWindowService windowService = new WindowService();
        private readonly EventAggregator eventAggregator = (EventAggregator)App.EventAggregator;

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        public string SalaryText
        {
            get { return salaryText; }
            set
            {
                salaryText = value;
                OnPropertyChanged(nameof(SalaryText));
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public AddNewUserViewModel()
        {
            AddCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand);
        }

        private bool CanExecuteAddCommand(object parameter)
        {
            bool validData = false;
            if(!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Surname) && !string.IsNullOrEmpty(SalaryText))
            {
                if (int.TryParse(SalaryText, out salary))
                {
                    validData = true;
                }
            }
            
            return validData;
        }

        private void ExecuteAddCommand(object parameter)
        {
            UserModel user = new()
            {
                Username = username,
                Name = name,
                Surname = surname,
                Salary = salary,
                Role = 0
            };

            try
            {
                userRepository.AddUser(user, password);
                eventAggregator.GetEvent<PubSubEvent<UserModel>>().Publish(user);
                windowService.OpenAlertWindow((string)Application.Current.TryFindResource("AddedUser"));
            }
            catch(MySqlException)
            {
                windowService.OpenIncorrectAlertWindow((string)Application.Current.TryFindResource("UsernameExists"));
            }
            
        }

        private void ExecuteCancelCommand(object parameter)
        {
            windowService.Close(this);
        }
    }
}
