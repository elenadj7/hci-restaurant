using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using hci_restaurant.Repositories;
using hci_restaurant.Services;
using hci_restaurant.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_LoginForm.ViewModels;

namespace hci_restaurant.ViewModels
{
    class LoginViewModel : ViewModelBase
    {
        private string username;
        private SecureString password;
        private string errorMessage;

        private IUserRepository repository;
        private IWindowService windowService;

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public SecureString Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            repository = new UserRepository();
            windowService = new WindowService();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        }

        private bool CanExecuteLoginCommand(object parameter)
        {
            bool validData = true;
            if (string.IsNullOrEmpty(Username) || password == null) { validData = false; }
            return validData;
        }

        private void ExecuteLoginCommand(object parameter)
        {
            
            UserModel? user = repository.AuthenticateUser(Username, Password);
            if (user != null)
            {
                List<Claim> claims = new()
                {
                    new(ClaimTypes.Name, user.Username), 
                    new("name", user.Name), 
                    new("surname", user.Surname),
                    new("salary", user.Salary+""), 
                    new(ClaimTypes.Role, user.Role+"")
                };

                ClaimsIdentity identity = new(claims, "CustomAuthType");
                Thread.CurrentPrincipal = new ClaimsPrincipal(identity);

                short role = short.Parse(identity.FindFirst(ClaimTypes.Role)?.Value);
                if(role == 1)
                {
                    windowService.OpenManagerWindow();
                }
                else
                {
                    windowService.OpenMainWindow();
                }

                windowService.Close(this);
            }
            else
            {
                ErrorMessage = "* Invalid username or password";
            }
        }
    }
}
