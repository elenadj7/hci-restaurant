using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using hci_restaurant.Repositories;
using hci_restaurant.Services;
using Microsoft.Windows.Themes;
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
        private ObservableCollection<ProcurementHasItemModel> items;
        private ObservableCollection<UserModel> users;
        private ObservableCollection<ProcurementModel> procurements;
        private UserModel selectedUser;
        private ProcurementModel selectedProcurement;
        private readonly IProcurementRepository procurementRepository = new ProcurementRepository();
        private readonly IUserRepository userRepository = new UserRepository();
        private readonly IWindowService windowService = new WindowService();
        private string username;

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

        public ProcurementModel SelectedProcurement
        {
            get => selectedProcurement;
            set
            {
                selectedProcurement = value;
                OnPropertyChanged(nameof(SelectedProcurement));
                Items = procurementRepository.GetItemDataByUsernameAndProcurementId(SelectedUser.Username, SelectedProcurement.Id);
            }
        }

        public ObservableCollection<ProcurementHasItemModel> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddProcurementCommand {  get; set; }
        public ICommand DeleteProcurementCommand { get; set; }

        public ProcurementsViewModel()
        {
            EditCommand = new ViewModelCommand(ExecuteEditing, CanExecuteEdit);
            DeleteCommand = new ViewModelCommand(ExecuteDeleting, CanExecuteDelete);
            AddProcurementCommand = new ViewModelCommand(ExecuteAddingNewProcurement);
            DeleteProcurementCommand = new ViewModelCommand(ExecuteDeletingProcurement, CanExecuteDeletingProcurement);
            Users = userRepository.GetAll();
            SelectedUser = Users.ElementAt(0);


            ClaimsPrincipal? currentUser = Thread.CurrentPrincipal as ClaimsPrincipal;
            if (currentUser != null && (currentUser?.Identity is ClaimsIdentity identity))
            {
                username = identity.FindFirst(ClaimTypes.Name)?.Value;
            }
        }

        private void ExecuteDeletingProcurement(object parameter)
        {
            procurementRepository.DeleteProcurement(selectedProcurement.Id);
            windowService.OpenAlertWindow((string)Application.Current.TryFindResource("DeletedProcurement"));
            Procurements.Remove(selectedProcurement);
        }

        private bool CanExecuteDeletingProcurement(object parameter)
        {
            if(SelectedProcurement == null || SelectedUser == null)
            {
                return false;
            }
            else if (!SelectedUser.Username.Equals(username))
            {
                return false;
            }

            return true;
        }

        private void ExecuteAddingNewProcurement(object parameter)
        {
            windowService.OpenAddNewProcurementWindow();
        }

        private void ExecuteDeleting(object parameter)
        {
            if (parameter is ProcurementHasItemModel item)
            {
                 //todo
            }
        }

        private bool CanExecuteDelete(object parameter)
        {
            return parameter is ProcurementHasItemModel && username.Equals(SelectedUser.Username);
        }

        private void ExecuteEditing(object parameter)
        {
            if (parameter is ProcurementHasItemModel item)
            {
                //TODO
            }
        }

        private bool CanExecuteEdit(object parameter)
        {
            return parameter is ProcurementHasItemModel && username.Equals(SelectedUser.Username);
        }
    }
}
