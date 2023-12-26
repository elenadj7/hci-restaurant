using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using hci_restaurant.Repositories;
using hci_restaurant.Services;
using hci_restaurant.Views;
using MySqlConnector;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPF_LoginForm.ViewModels;

namespace hci_restaurant.ViewModels
{
    public class UsersViewModel : ViewModelBase
    {
        private ObservableCollection<UserModel> users = new();
        private readonly IUserRepository userRepository = new UserRepository();
        private readonly IWindowService windowService = new WindowService();
        private readonly PubSubEvent<UserModel> addedUser= App.EventAggregator.GetEvent<PubSubEvent<UserModel>>();
        private readonly PubSubEvent<Tuple<string, int>> modifiedUser = App.EventAggregator.GetEvent<PubSubEvent<Tuple<string, int>>>();
        private string filter;

        public string Filter
        {
            get { return filter; }
            set
            {
                filter = value;
                OnPropertyChanged(nameof(Filter));
            }
        }

        public ObservableCollection<UserModel> Users
        {
            get { return users; }
            set
            {
                users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public ICommand DeleteUserCommand { get; set; }
        public ICommand EditUserCommand { get; set; }
        public ICommand AddNewUserCommand { get; set; }
        public ICommand FilterCommand { get; set; }

        public UsersViewModel()
        {
            Users = LoadAllUsers();
            DeleteUserCommand = new ViewModelCommand(ExecuteDeleting, CanExecuteDelete);
            EditUserCommand = new ViewModelCommand(ExecuteEditing, CanExecuteEdit);
            AddNewUserCommand = new ViewModelCommand(ExecuteAddNewUser);
            FilterCommand = new ViewModelCommand(ExecuteFilter);

            addedUser.Subscribe(OnAddedUser);
            modifiedUser.Subscribe(OnModifiedUser);
        }

        private void OnModifiedUser(Tuple<string, int> user)
        {
            Users.Where(u => u.Username.Equals(user.Item1)).FirstOrDefault().Salary = user.Item2;
        }

        private void OnAddedUser(UserModel user)
        {
            Users.Add(user);
        }

        private void ExecuteFilter(object parameter)
        {
            Users = userRepository.GetAllByFilter(Filter);
        }

        private ObservableCollection<UserModel> LoadAllUsers()
        {
            return userRepository.GetAll();
        }

        private void ExecuteDeleting(object parameter)
        {
            if (parameter is UserModel user)
            {
                windowService.OpenConfirmWindow((string)Application.Current.TryFindResource("DeleteConfirm") + " " + user.Username + "?");
                if (ConfirmViewModel.CanBe)
                {
                    userRepository.DeleteUser(user.Username);
                    users.Remove(user);
                    ConfirmViewModel.CanBe = false;
                    windowService.OpenAlertWindow((string)Application.Current.TryFindResource("Deleted") + " " + user.Username + "!");
                }
            }
        }

        private bool CanExecuteDelete(object parameter)
        {
            return parameter is UserModel;
        }

        private void ExecuteEditing(object parameter)
        {
            if (parameter is UserModel user)
            {
                windowService.OpenUpdateUserWindow(user);
            }
        }

        private bool CanExecuteEdit(object parameter)
        {
            return parameter is UserModel user;
        }

        private void ExecuteAddNewUser(object parameter)
        {
            windowService.OpenAddNewUserWindow();
        }
    }
}
