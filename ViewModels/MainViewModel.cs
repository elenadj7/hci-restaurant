using hci_restaurant.Models;
using hci_restaurant.Services;
using hci_restaurant.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WPF_LoginForm.ViewModels;

namespace hci_restaurant.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private UserModel user;
        private readonly IWindowService windowService = new WindowService();

        private bool ordersSelected = false;
        private bool procurementsSelected = true;
        private bool settingsSelected = false;

        private Page currentPage = new ProcurementsPage();

        public Page CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public UserModel User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public bool OrdersSelected
        {
            get { return ordersSelected; }
            set
            {
                ordersSelected = value;
                OnPropertyChanged(nameof(OrdersSelected));
                if (ordersSelected)
                {
                    //TODO
                }
            }
        }

        public bool ProcurementsSelected
        {
            get { return procurementsSelected; }
            set
            {
                procurementsSelected = value;
                OnPropertyChanged(nameof(ProcurementsSelected));
                if (procurementsSelected)
                {
                    CurrentPage = new ProcurementsPage();
                }
            }
        }

        public bool SettingsSelected
        {
            get { return settingsSelected; }
            set
            {
                settingsSelected = value;
                OnPropertyChanged(nameof(SettingsSelected));
                if (settingsSelected)
                {
                    CurrentPage = new SettingsPage();
                }
            }
        }


        public MainViewModel()
        {
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

                LanguageService.CurrentLanguage = "English";
                ThemeService.CurrentTheme = "Dark";

                windowService.OpenLoginWindow();
                windowService.Close(this);
            }
        }
    }
}
