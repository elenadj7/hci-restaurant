using System.Collections.ObjectModel;
using System.Security;

namespace hci_restaurant.Models.Repositories
{
    interface IUserRepository
    {
        ObservableCollection<UserModel> GetAll();
        UserModel? AuthenticateUser(string username, SecureString password);
        string? GetTheme(string username);
        string? GetLanguage(string username);
        void UpdateTheme(string username, string theme);
        void UpdateLanguage(string username, string language);
        void DeleteUser(string username);
        void AddUser(UserModel user, string password);
        ObservableCollection<UserModel> GetAllByFilter(string filter);
        void UpdateUser(string username, string name, string surname, int salary);
    }
}
