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
        private bool isFinished;
        private bool isVisible = false;

        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }

        public bool IsFinished
        {
            get { return isFinished; }
            set
            {
                isFinished = value;
                OnPropertyChanged(nameof(IsFinished));
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
                Procurements = procurementRepository.GetAllByUser(selectedUser.Username);
                SelectedProcurement = null;
                if(Items != null)
                {
                    Items.Clear();
                }
                IsVisible = false;
            }
        }

        public ProcurementModel SelectedProcurement
        {
            get => selectedProcurement;
            set
            {
                selectedProcurement = value;
                if(selectedProcurement != null)
                {
                    OnPropertyChanged(nameof(SelectedProcurement));
                    Items = procurementRepository.GetItemDataByUsernameAndProcurementId(SelectedUser.Username, SelectedProcurement.Id);
                    IsVisible = true;
                    if (selectedProcurement.IsFinished == 0)
                    {
                        IsFinished = false;
                    }
                    else
                    {
                        IsFinished = true;
                    }
                }
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
        public ICommand UpdateProcurementCommand { get; set; }

        public ProcurementsViewModel()
        {
            EditCommand = new ViewModelCommand(ExecuteEditing, CanExecuteEdit);
            DeleteCommand = new ViewModelCommand(ExecuteDeleting, CanExecuteDelete);
            AddProcurementCommand = new ViewModelCommand(ExecuteAddingNewProcurement);
            DeleteProcurementCommand = new ViewModelCommand(ExecuteDeletingProcurement, CanExecuteDeletingProcurement);
            UpdateProcurementCommand = new ViewModelCommand(ExecuteUpdatingProcurement, CanExecuteUpdatingProcurement);
            Users = userRepository.GetAll();
            SelectedUser = Users.ElementAt(0);


            ClaimsPrincipal? currentUser = Thread.CurrentPrincipal as ClaimsPrincipal;
            if (currentUser != null && (currentUser?.Identity is ClaimsIdentity identity))
            {
                username = identity.FindFirst(ClaimTypes.Name)?.Value;
            }
        }

        private void ExecuteUpdatingProcurement(object parameter)
        {
            procurementRepository.UpdateProcurement(SelectedProcurement.Id);
            SelectedProcurement.IsFinished = 1;
            IsFinished = true;
            windowService.OpenAlertWindow((string)Application.Current.TryFindResource("CompletedProcurement"));
        }

        private bool CanExecuteUpdatingProcurement(object parameter)
        {
            if (SelectedProcurement == null || SelectedUser == null)
            {
                return false;
            }
            else if (!SelectedUser.Username.Equals(username))
            {
                return false;
            }
            else if (SelectedProcurement.IsFinished == 1)
            {
                return false;
            }

            return true;
        }

        private void ExecuteDeletingProcurement(object parameter)
        {
            procurementRepository.DeleteProcurement(selectedProcurement.Id);
            windowService.OpenAlertWindow((string)Application.Current.TryFindResource("DeletedProcurement"));
            Procurements.Remove(selectedProcurement);
            Items.Clear();
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
                procurementRepository.DeleteProcurementHasItem(item.ProcurementId, item.Item.Id);
                Items.Remove(item);

                if(Items.Count > 0)
                {
                    windowService.OpenAlertWindow((string)Application.Current.TryFindResource("Deleted") + " " + item.Item.Name + "!");
                    return;
                }
                
                procurementRepository.DeleteProcurement(item.ProcurementId);
                Procurements.Remove(selectedProcurement);
                windowService.OpenAlertWindow((string)Application.Current.TryFindResource("DeletedProcurement"));
                IsVisible = false;
            }
        }

        private bool CanExecuteDelete(object parameter)
        {
            return parameter is ProcurementHasItemModel && username.Equals(SelectedUser.Username) && !IsFinished;
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
            return parameter is ProcurementHasItemModel && username.Equals(SelectedUser.Username) && !IsFinished;
        }
    }
}
