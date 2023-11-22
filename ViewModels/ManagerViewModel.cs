using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_LoginForm.ViewModels;

namespace hci_restaurant.ViewModels
{
    class ManagerViewModel : ViewModelBase
    {
        private UserModel user;
        private IUserRepository userRepository;
    }
}
