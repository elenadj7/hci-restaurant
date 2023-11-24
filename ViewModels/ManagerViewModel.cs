using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using hci_restaurant.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WPF_LoginForm.ViewModels;

namespace hci_restaurant.ViewModels
{
    class ManagerViewModel : ViewModelBase
    {
        private UserModel user;
        private IUserRepository userRepository;

        public UserModel User
        {
            get { return user; }
            set { user = value;
            OnPropertyChanged(nameof(User));}
        }

        public ManagerViewModel()
        {
            userRepository = new UserRepository();
            LoadCurrentUser();
        }

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
    }
}
