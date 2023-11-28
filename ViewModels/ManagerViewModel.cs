using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using hci_restaurant.Repositories;
using hci_restaurant.Services;
using hci_restaurant.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF_LoginForm.ViewModels;

namespace hci_restaurant.ViewModels
{
    class ManagerViewModel : ViewModelBase
    {
        private UserModel user;
        private IUserRepository userRepository;
        private IWindowService windowService;

        private bool usersSelected = true;
        private bool itemsSelected = false;
        private bool procurementsSelected = false;
        private bool settingsSelected = false;

        public UserModel User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public bool UsersSelected
        {
            get { return usersSelected; }
            set
            {
                usersSelected = value;
                OnPropertyChanged(nameof(UsersSelected));
            }
        }

        public bool ItemsSelected
        {
            get { return itemsSelected; }
            set
            {
                itemsSelected = value;
                OnPropertyChanged(nameof(ItemsSelected));
            }
        }

        public bool ProcurementsSelected
        {
            get { return procurementsSelected; }
            set
            {
                procurementsSelected = value;
                OnPropertyChanged(nameof(ProcurementsSelected));
            }
        }

        public bool SettingsSelected
        {
            get { return settingsSelected; }
            set
            {
                settingsSelected = value;
                OnPropertyChanged(nameof(SettingsSelected));
            }
        }


        public ManagerViewModel()
        {
            userRepository = new UserRepository();
            windowService = new WindowService();
            LogoutCommand = new ViewModelCommand(ExecuteLogoutCommand);
            LoadCurrentUser();
        }

        public ICommand LogoutCommand { get; set; }

        private void LoadCurrentUser()
        {
            ClaimsPrincipal? currentUser = Thread.CurrentPrincipal as ClaimsPrincipal;
            if (currentUser != null && (currentUser?.Identity is ClaimsIdentity identity))
            {
                user = new()
                {
                    Username = identity.FindFirst(ClaimTypes.Name)?.Value,
                    Name = identity.FindFirst("name")?.Value,
                    Surname = identity.FindFirst("surname")?.Value,
                    Salary = int.Parse(identity.FindFirst("salary")?.Value),
                    Role = short.Parse(identity.FindFirst(ClaimTypes.Role)?.Value),
                };
            }
            
        }

        private void ExecuteLogoutCommand(object parameter)
        {
            if (Thread.CurrentPrincipal is ClaimsPrincipal currentUser)
            {
                var claimsIdentity = currentUser.Identity as ClaimsIdentity;
                claimsIdentity?.Claims.ToList().ForEach(c => claimsIdentity.RemoveClaim(c));

                Thread.CurrentPrincipal = null;

                windowService.OpenLoginWindow();
                windowService.Close(this);
            }
        }
    }
}
