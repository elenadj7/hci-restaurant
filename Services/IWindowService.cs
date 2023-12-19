using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hci_restaurant.Services
{
    internal interface IWindowService
    {
        void OpenLoginWindow();
        void Close(object viewModel);
        void OpenManagerWindow();
        void OpenMainWindow();
        void OpenAlertWindow(string message);
        void OpenIncorrectAlertWindow(string message);
        void OpenConfirmWindow(string message);
        void OpenAddNewUserWindow();
    }
}
