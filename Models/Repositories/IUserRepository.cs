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
        UserModel GetUser(string username);
        IEnumerable<UserModel> GetAll();
        public UserModel? AuthenticateUser(string username, SecureString password);
    }
}
