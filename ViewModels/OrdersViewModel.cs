using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using hci_restaurant.Repositories;
using hci_restaurant.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_LoginForm.ViewModels;

namespace hci_restaurant.ViewModels
{
    public class OrdersViewModel : ViewModelBase
    {
        private ObservableCollection<UserModel> users;
        private ObservableCollection<OrderModel> orders;
        private UserModel selectedUser;
        private string filter;
        private string username;
        private readonly IWindowService windowService = new WindowService();
        private readonly IOrderRepository orderRepository = new OrderRepository();
        private readonly IUserRepository userRepository = new UserRepository();

        public ObservableCollection<UserModel> Users
        {
            get { return users; }
            set
            {
                users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public string Filter
        {
            get { return filter; }
            set
            {
                filter = value;
                OnPropertyChanged(nameof(Filter));
            }
        }

        public UserModel SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
                Orders = orderRepository.GetAllByUsername(selectedUser.Username);
            }
        }

        public ObservableCollection<OrderModel> Orders
        {
            get { return orders; }
            set
            {
                orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

        public ICommand FilterCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ShowDetailsCommand { get; set; }
        public ICommand AddOrderCommand { get; set; }

        public OrdersViewModel()
        {
            ShowDetailsCommand = new ViewModelCommand(ExecuteShowDetails, CanExecuteShowDetails);
            DeleteCommand = new ViewModelCommand(ExecuteDelete, CanExecuteDelete);
            AddOrderCommand = new ViewModelCommand(ExecuteAddOrder);
            Users = userRepository.GetAll();

            LoadCurrentUser();
        }

        private void LoadCurrentUser()
        {
            ClaimsPrincipal? currentUser = Thread.CurrentPrincipal as ClaimsPrincipal;
            if (currentUser != null && (currentUser?.Identity is ClaimsIdentity identity))
            {
                username = identity.FindFirst(ClaimTypes.Name)?.Value;
            }

            SelectedUser = Users.Where(user => user.Username == username).FirstOrDefault();
        }

        private void ExecuteAddOrder(object parameter)
        {
            windowService.OpenAddNewOrderWindow();
        }

        private void ExecuteShowDetails(object parameter)
        {
            if (parameter is OrderModel orderModel)
            {
                windowService.OpenOrderDetailsWindow(SelectedUser.Username, orderModel.Id);
            }
        }

        private bool CanExecuteShowDetails(object parameter)
        {
            return parameter is OrderModel orderModel;
        }

        private void ExecuteDelete(object parameter)
        {
            if (parameter is OrderModel orderModel)
            {
                windowService.OpenConfirmWindow((string)Application.Current.TryFindResource("DeleteConfirm") + " " + orderModel.Id + "?");
                if (ConfirmViewModel.CanBe)
                {
                    orderRepository.DeleteOrder(orderModel.Id);
                    Orders.Remove(orderModel);
                    windowService.OpenAlertWindow((string)Application.Current.TryFindResource("Deleted") + " " + orderModel.Id + "!");
                }
            }
        }

        private bool CanExecuteDelete(object parameter)
        {
            return parameter is OrderModel && username.Equals(SelectedUser.Username);
        }
    }
}
