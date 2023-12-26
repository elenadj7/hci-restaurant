using hci_restaurant.Models;
using hci_restaurant.Services;
using hci_restaurant.Views;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using WPF_LoginForm.ViewModels;

namespace hci_restaurant.ViewModels
{
    public class ManagerViewModel : ViewModelBase
    {
        private UserModel user;
        private readonly IWindowService windowService = new WindowService();

        private bool usersSelected = true;
        private bool itemsSelected = false;
        private bool settingsSelected = false;

        private Page currentPage = new UsersPage();

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

        public bool UsersSelected
        {
            get { return usersSelected; }
            set
            {
                usersSelected = value;
                OnPropertyChanged(nameof(UsersSelected));
                if (usersSelected)
                {
                    CurrentPage = new UsersPage();
                }
            }
        }

        public bool ItemsSelected
        {
            get { return itemsSelected; }
            set
            {
                itemsSelected = value;
                OnPropertyChanged(nameof(ItemsSelected));
                if (itemsSelected)
                {
                    CurrentPage = new ItemsPage();
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


        public ManagerViewModel()
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
                ThemeService.CurrentTheme = "Light";

                windowService.OpenLoginWindow();
                windowService.Close(this);
            }
        }
    }
}
