using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using hci_restaurant.Repositories;
using hci_restaurant.Services;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using WPF_LoginForm.ViewModels;

namespace hci_restaurant.ViewModels
{
    public class OrdersViewModel : ViewModelBase
    {
        private ObservableCollection<UserModel> users;
        private ObservableCollection<OrderModel> orders;
        private ObservableCollection<string> months = new();
        private ObservableCollection<string> years = new();
        private UserModel selectedUser;
        private string selectedMonth;
        private string selectedYear;
        private string filter;
        private string username;
        private readonly IWindowService windowService = new WindowService();
        private readonly IOrderRepository orderRepository = new OrderRepository();
        private readonly IUserRepository userRepository = new UserRepository();
        private readonly PubSubEvent<OrderModel> addedOrder = App.EventAggregator.GetEvent<PubSubEvent<OrderModel>>();


        public ObservableCollection<string> Years
        {
            get { return years; }
            set
            {
                years = value;
                OnPropertyChanged(nameof(Years));
            }
        }

        public ObservableCollection<string> Months
        {
            get { return months; }
            set
            {
                months = value;
                OnPropertyChanged(nameof(Months));
            }
        }

        public string SelectedMonth
        {
            get { return selectedMonth; }
            set
            {
                selectedMonth = value;
                OnPropertyChanged(nameof(selectedMonth));
                if (selectedMonth.Equals((string)Application.Current.TryFindResource("All")))
                {
                    if (selectedYear.Equals((string)Application.Current.TryFindResource("All")))
                    {
                        Orders = orderRepository.GetAllByUsername(selectedUser.Username);
                    }
                    else
                    {
                        Orders = orderRepository.GetAllByYear(selectedUser.Username, int.Parse(selectedYear));
                    }
                }
                else
                {
                    if (selectedYear.Equals((string)Application.Current.TryFindResource("All")))
                    {
                        Orders = orderRepository.GetAllByMonth(selectedUser.Username, int.Parse(selectedMonth));
                    }
                    else
                    {
                        Orders = orderRepository.GetAllByYearAndMonth(selectedUser.Username, int.Parse(selectedYear), int.Parse(selectedMonth));
                    }
                }
            }
        }

        public string SelectedYear
        {
            get => selectedYear;
            set
            {
                selectedYear = value;
                OnPropertyChanged(nameof(SelectedYear));
                if (selectedYear.Equals((string)Application.Current.TryFindResource("All")))
                {
                    if (selectedMonth.Equals((string)Application.Current.TryFindResource("All")))
                    {
                        Orders = orderRepository.GetAllByUsername(selectedUser.Username);
                    }
                    else
                    {
                        Orders = orderRepository.GetAllByMonth(selectedUser.Username, int.Parse(selectedMonth));
                    }
                }
                else
                {
                    if (selectedMonth.Equals((string)Application.Current.TryFindResource("All")))
                    {
                        Orders = orderRepository.GetAllByYear(selectedUser.Username, int.Parse(selectedYear));
                    }
                    else
                    {
                        Orders = orderRepository.GetAllByYearAndMonth(selectedUser.Username, int.Parse(selectedYear), int.Parse(selectedMonth));
                    }
                }
            }
        }

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

            FillMonthsAndYears();
            LoadCurrentUser();
            addedOrder.Subscribe(OnAddedOrder);
        }

        private void OnAddedOrder(OrderModel order)
        {
            Orders.Add(order);
        }

        private void FillMonthsAndYears()
        {
            months.Add((string)Application.Current.TryFindResource("All"));
            years.Add((string)Application.Current.TryFindResource("All"));

            for (int i = 1; i <= 12; i++)
            {
                months.Add(i + "");
            }

            for (int i = 2010; i <= 2023; ++i)
            {
                years.Add(i + "");
            }

            selectedMonth = months.ElementAt(0);
            selectedYear = years.ElementAt(0);
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
            return parameter is OrderModel;
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
                    ConfirmViewModel.CanBe = false;
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
