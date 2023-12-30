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
    public class ProcurementsViewModel : ViewModelBase
    {
        private ObservableCollection<UserModel> users;
        private ObservableCollection<ProcurementModel> procurements;
        private ObservableCollection<string> months = new();
        private ObservableCollection<string> years = new();
        private UserModel selectedUser;
        private string selectedMonth;
        private string selectedYear;
        private string username;
        private readonly IProcurementRepository procurementRepository = new ProcurementRepository();
        private readonly IUserRepository userRepository = new UserRepository();
        private readonly IWindowService windowService = new WindowService();
        private readonly PubSubEvent<ProcurementModel> addedProcurement = App.EventAggregator.GetEvent<PubSubEvent<ProcurementModel>>();

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
                        Procurements = procurementRepository.GetAllByUser(selectedUser.Username);
                    }
                    else
                    {
                        Procurements = procurementRepository.GetAllByYear(selectedUser.Username, int.Parse(selectedYear));
                    }
                }
                else
                {
                    if (selectedYear.Equals((string)Application.Current.TryFindResource("All")))
                    {
                        Procurements = procurementRepository.GetAllByMonth(selectedUser.Username, int.Parse(selectedMonth));
                    }
                    else
                    {
                        Procurements = procurementRepository.GetAllByYearAndMonth(selectedUser.Username, int.Parse(selectedYear), int.Parse(selectedMonth));
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
                        Procurements = procurementRepository.GetAllByUser(selectedUser.Username);
                    }
                    else
                    {
                        Procurements = procurementRepository.GetAllByMonth(selectedUser.Username, int.Parse(selectedMonth));
                    }
                }
                else
                {
                    if (selectedMonth.Equals((string)Application.Current.TryFindResource("All")))
                    {
                        Procurements = procurementRepository.GetAllByYear(selectedUser.Username, int.Parse(selectedYear));
                    }
                    else
                    {
                        Procurements = procurementRepository.GetAllByYearAndMonth(selectedUser.Username, int.Parse(selectedYear), int.Parse(selectedMonth));
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

        public ObservableCollection<ProcurementModel> Procurements
        {
            get { return procurements; }
            set
            {
                procurements = value;
                OnPropertyChanged(nameof(Procurements));
            }
        }

        public UserModel SelectedUser
        {
            get => selectedUser;
            set
            {   
                selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
                SelectedMonth = Months.ElementAt(0);
                SelectedYear = Months.ElementAt(0);
                Procurements = procurementRepository.GetAllByUser(selectedUser.Username);
            }
        }

        public ICommand AddProcurementCommand {  get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ShowDetailsCommand { get; set; }

        public ProcurementsViewModel()
        {
            AddProcurementCommand = new ViewModelCommand(ExecuteAddingNewProcurement);
            DeleteCommand = new ViewModelCommand(ExecuteDelete, CanExecuteDelete);
            ShowDetailsCommand = new ViewModelCommand(ExecuteShowDetails, CanExecuteShowDetails);
            Users = userRepository.GetAll();

            FillMonthsAndYears();
            LoadCurrentUser();
            addedProcurement.Subscribe(OnAddedProcurement);
        }

        private void FillMonthsAndYears()
        {
            months.Add((string)Application.Current.TryFindResource("All"));
            years.Add((string)Application.Current.TryFindResource("All"));

            for (int i = 1; i <= 12; i++)
            {
                months.Add(i + "");
            }

            for(int i = 2010; i <= 2023; ++i)
            {
                years.Add(i + "");
            }

            selectedMonth = months.ElementAt(0);
            selectedYear = years.ElementAt(0);
        }

        private void OnAddedProcurement(ProcurementModel procurement)
        {
            Procurements.Add(procurement);
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

        private void ExecuteAddingNewProcurement(object parameter)
        {
            windowService.OpenAddNewProcurementWindow();
        }

        private void ExecuteDelete(object parameter)
        {
            if (parameter is ProcurementModel procurementModel)
            {
                windowService.OpenConfirmWindow((string)Application.Current.TryFindResource("DeleteConfirm") + " " + procurementModel.Id + "?");
                if (ConfirmViewModel.CanBe)
                {
                    procurementRepository.DeleteProcurement(procurementModel.Id);
                    Procurements.Remove(procurementModel);
                    ConfirmViewModel.CanBe = false;
                    windowService.OpenAlertWindow((string)Application.Current.TryFindResource("Deleted") + " " + procurementModel.Id + "!");
                }
            }
        }

        private void ExecuteShowDetails(object parameter)
        {
            if(parameter is ProcurementModel procurementModel)
            {
                windowService.OpenProcurementDetailsWindow(SelectedUser.Username, procurementModel.Id);
            }
        }

        private bool CanExecuteShowDetails(object parameter)
        {
            return parameter is ProcurementModel;
        }

        private bool CanExecuteDelete(object parameter)
        {
            return parameter is ProcurementModel && SelectedUser.Username == username;
        }

        private void ExecuteFilter(object parameter)
        {
            //TODO
        }
    }
}
