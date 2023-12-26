using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using hci_restaurant.Repositories;
using hci_restaurant.Services;
using Microsoft.Windows.Themes;
using Prism.Events;
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
    public class ProcurementsViewModel : ViewModelBase
    {
        private ObservableCollection<UserModel> users;
        private ObservableCollection<ProcurementModel> procurements;
        private UserModel selectedUser;
        private string username;
        private string filter;
        private readonly IProcurementRepository procurementRepository = new ProcurementRepository();
        private readonly IUserRepository userRepository = new UserRepository();
        private readonly IWindowService windowService = new WindowService();
        private readonly PubSubEvent<ProcurementModel> addedProcurement = App.EventAggregator.GetEvent<PubSubEvent<ProcurementModel>>();

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
                Procurements = procurementRepository.GetAllByUser(selectedUser.Username);
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

        public ICommand AddProcurementCommand {  get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ShowDetailsCommand { get; set; }
        public ICommand FilterCommand { get; set; }

        public ProcurementsViewModel()
        {
            AddProcurementCommand = new ViewModelCommand(ExecuteAddingNewProcurement);
            DeleteCommand = new ViewModelCommand(ExecuteDelete, CanExecuteDelete);
            ShowDetailsCommand = new ViewModelCommand(ExecuteShowDetails, CanExecuteShowDetails);
            FilterCommand = new ViewModelCommand(ExecuteFilter);
            Users = userRepository.GetAll();

            LoadCurrentUser();
            addedProcurement.Subscribe(OnAddedProcurement);
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
            return parameter is ProcurementModel procurementModel;
        }

        private bool CanExecuteDelete(object parameter)
        {
            return parameter is ProcurementModel procurementModel && SelectedUser.Username == username;
        }

        private void ExecuteFilter(object parameter)
        {
            //TODO
        }
    }
}
