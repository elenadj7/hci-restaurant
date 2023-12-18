using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace hci_restaurant.Models.Repositories
{
    interface IUserRepository
    {
        IEnumerable<UserModel> GetAll();
        public UserModel? AuthenticateUser(string username, SecureString password);
        public string? GetTheme(string username);
        public string? GetLanguage(string username);
        public void UpdateTheme(string username, string theme);
        public void UpdateLanguage(string username, string language);
    }
}
