using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using hci_restaurant.Repositories;
using hci_restaurant.Services;
using hci_restaurant.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WPF_LoginForm.ViewModels;

namespace hci_restaurant.ViewModels
{
    class UsersViewModel : ViewModelBase
    {
        private ObservableCollection<UserModel> users = new();
        private IUserRepository userRepository = new UserRepository();
        private IWindowService windowService = new WindowService();

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

        public UsersViewModel()
        {
            Users = LoadAllUsers();
            DeleteUserCommand = new ViewModelCommand(ExecuteDeleting, CanExecuteDelete);
            EditUserCommand = new ViewModelCommand(ExecuteEditing, CanExecuteEdit);
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
                    windowService.OpenAlertWindow((string)Application.Current.TryFindResource("DeletedUser") + " " + user.Username + "!");
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
                //TODO
            }
        }

        private bool CanExecuteEdit(object parameter)
        {
            return parameter is UserModel user;
        }
    }
}
