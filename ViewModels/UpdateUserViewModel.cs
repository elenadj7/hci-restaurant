using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using hci_restaurant.Repositories;
using hci_restaurant.Services;
using Prism.Events;
using System;
using System.Windows;
using System.Windows.Input;
using WPF_LoginForm.ViewModels;

namespace hci_restaurant.ViewModels
{
    public class UpdateUserViewModel : ViewModelBase
    {
        private UserModel user;
        private string name;
        private string surname;
        private string salary;
        private readonly IWindowService windowService = new WindowService();
        private readonly IUserRepository userRepository = new UserRepository();
        private readonly EventAggregator eventAggregator = (EventAggregator)App.EventAggregator;

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

        public string Salary
        {
            get { return salary; }
            set
            {
                salary = value;
                OnPropertyChanged(nameof(Salary));
            }
        }

        public ICommand UpdateCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public UpdateUserViewModel()
        {
            UpdateCommand = new ViewModelCommand(ExecuteUpdating);
            CancelCommand = new ViewModelCommand(ExecuteCaneling);
        }

        public UpdateUserViewModel(UserModel user)
        {
            UpdateCommand = new ViewModelCommand(ExecuteUpdating);
            CancelCommand = new ViewModelCommand(ExecuteCaneling);
            this.user = user;
            Name = user.Name;
            Surname = user.Surname;
            Salary = user.Salary.ToString();
        }

        public void ExecuteCaneling(object parameter)
        {
            windowService.Close(this);
        }

        public void ExecuteUpdating(object parameter)
        {
            if (!int.TryParse(Salary, out _))
            {
                windowService.OpenIncorrectAlertWindow((string)Application.Current.TryFindResource("SalaryAlert"));
                return;
            }

            userRepository.UpdateUser(user.Username, name, surname, int.Parse(salary));
            eventAggregator.GetEvent<PubSubEvent<Tuple<string, int>>>().Publish(Tuple.Create(user.Username, int.Parse(salary)));
            windowService.OpenAlertWindow((string)Application.Current.TryFindResource("UpdatedUser"));
        }
    }
}
