using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using hci_restaurant.Repositories;
using hci_restaurant.Services;
using System.Security.Claims;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using WPF_LoginForm.ViewModels;

namespace hci_restaurant.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private int selectedIntLanguage = LanguageService.GetIntFromLanguage();
        private int selectedIntTheme = ThemeService.GetIntFromTheme();
        private IUserRepository userRepository = new UserRepository();
        private UserModel user;
        private IWindowService windowService = new WindowService();

        public int SelectedIntTheme
        {
            get { return selectedIntTheme; }
            set
            {
                selectedIntTheme = value;
                OnPropertyChanged(nameof(SelectedIntTheme));
                ExecuteSelectedTheme();
            }
        }

        public int SelectedIntLanguage
        {
            get { return selectedIntLanguage; }
            set
            {
                selectedIntLanguage = value;
                OnPropertyChanged(nameof(SelectedIntLanguage));
                ExecuteSelectedLanguage();
            }
        }

        public ICommand SaveCommand { get; set; }

        public SettingsViewModel()
        {
            SaveCommand = new ViewModelCommand(ExecuteSaveCommand);
            GetUser();
        }

        private void ExecuteSelectedLanguage()
        {
            LanguageService.CurrentLanguage = LanguageService.GetStringFromLanguage(SelectedIntLanguage);
        }

        private void ExecuteSelectedTheme()
        {
            ThemeService.CurrentTheme = ThemeService.GetStringFromTheme(SelectedIntTheme);
        }

        private void ExecuteSaveCommand(object parameter)
        {
            userRepository.UpdateTheme(user.Username, ThemeService.CurrentTheme);
            userRepository.UpdateLanguage(user.Username, LanguageService.CurrentLanguage);
            windowService.OpenAlertWindow((string)Application.Current.TryFindResource("SaveAlert"));
        }

        private void GetUser()
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
    }
}
