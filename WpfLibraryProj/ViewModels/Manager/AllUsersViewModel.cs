using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using WpfLibraryProj.Commands;
using WpfLibraryProj.Common;
using WpfLibraryProj.Logic.UserService;
using WpfLibraryProj.Views.Users.Manager;

namespace WpfLibraryProj.ViewModels.Manager
{
    public class AllUsersViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;

        private ObservableCollection<User> _allUsersCollection;

        public ObservableCollection<User> AllUsersCollection
        {
            get => _allUsersCollection; set
            {
                SetProperty(ref _allUsersCollection, value);
            }
        }

        private readonly IUserService _userItemService;

        private DelagteCommands _editItemCommand;

        public DelagteCommands EditItemCommand
        {
            get => _editItemCommand; set
            {
                SetProperty(ref _editItemCommand, value);
            }
        }

        private DelagteCommands _deleteItemCommand;

        public DelagteCommands DeleteItemCommand
        {
            get => _deleteItemCommand; set
            {
                SetProperty(ref _deleteItemCommand, value);
            }
        }

        public AllUsersViewModel(IUserService userItemService, IRegionManager regionManager)
        {
            _regionManager = regionManager;

            _userItemService = userItemService;

            var allUserItemCollection = _userItemService.GetUsers();

            AllUsersCollection = new ObservableCollection<User>(allUserItemCollection);

            EditItemCommand = new DelagteCommands(EditItem, (res) => { return true; });

            DeleteItemCommand = new DelagteCommands(DeleteItem, (res) => { return true; });

        }

        private void EditItem(object prameter)
        {
            var item = prameter as User;

            Application.Current.Properties.Add(NavParameters.AddUser, item);

            ManagerMainViewModel.ManagerFrame.Navigate(new AddUserView());

                //var navigationParameters = new NavigationParameters();

            //navigationParameters.Add(NavParameters.User, item);

            //_regionManager.RequestNavigate(Regions.ManagerViewRegion, Pages.AddUserView, navigationParameters);

        }

        private void DeleteItem(object prameter)
        {
            var item = prameter as User;

            _userItemService.DeleteUser(item.Id);

            if (item == null) return;

            AllUsersCollection.Remove(item);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            UpdateList();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        private void UpdateList()
        {
            var allUserItemCollection = _userItemService.GetUsers();

            AllUsersCollection = new ObservableCollection<User>(allUserItemCollection);
        }
    }
}

