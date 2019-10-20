using Prism.Commands;
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
using WpfLibraryProj.Views.LibraryItems.AllCollection;

namespace WpfLibraryProj.ViewModels.Manager
{
    public class AddUserViewModel : BindableBase, INavigationAware
    {
        private readonly IUserService _userService;

        private readonly IRegionManager _regionManager;

        public AddUserViewModel(IUserService userService, IRegionManager regionManager)
        {
            _tempUser = Application.Current.Properties[NavParameters.AddUser] as User;

            _userService = userService;

            _regionManager = regionManager;

            AddUserCommand = new DelegateCommand(AddUser);

            ClearFeildsCommand = new DelegateCommand(ClearFeilds);

            UserRankCollection = new ObservableCollection<UserRank>();
            foreach (UserRank category in (UserRank[])Enum.GetValues(typeof(UserRank)))
                UserRankCollection.Add(category);

            if (_tempUser != null)
            {
                User = _tempUser;

                _updateItem = true;

                Application.Current.Properties.Remove(NavParameters.User);
            }
            else
                User = new User();
        }

        private bool _updateItem;

        private User _tempUser;

        public DelegateCommand ClearFeildsCommand { get; private set; }

        private DelegateCommand _addUserCommand;

        public DelegateCommand AddUserCommand
        {
            get => _addUserCommand; set
            {
                SetProperty(ref _addUserCommand, value);
            }
        }

        private ObservableCollection<UserRank> _userRankCollection;

        public ObservableCollection<UserRank> UserRankCollection
        {
            get => _userRankCollection; set
            {
                {
                    SetProperty(ref _userRankCollection, value);
                }
            }
        }

        private User _user;

        public User User
        {
            get => _user; set
            {
                SetProperty(ref _user, value);
            }
        }

       

        public void AddUser()
        {
            if (string.IsNullOrWhiteSpace(User.UserName) || string.IsNullOrWhiteSpace(User.Password))
                 return;

            if (_updateItem)
            {
                User.Id = _tempUser.Id;
                _userService.ManagerUpdateUser(User);
            }
            else
            {
                _userService.ManagerAddUser(User);
            }

            ClearFeilds();

            ManagerMainViewModel.ManagerFrame.Navigate(new ItemHasAddedView());


            //_regionManager.RequestNavigate(Regions.AddItem, Pages.ItemHasAddedView);

        }

        public void ClearFeilds()
        {
            User.UserName = "";

            User.Password = "";

            User.Email = "";

            User.UserRank = UserRank.Customer;

        }

        bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)
        {
            ClearFeilds();

            var getUser = navigationContext.Parameters[NavParameters.User];

            //if (getUser != null)
            //{
            //    var user = getUser as User;

            //    UserName = user.UserName;

            //    Password = user.Password;

            //    Email = user.Email;

            //    UserRank = user.UserRank;

            //    _tempUser = user;

            //    _updateItem = true;

            //}
        }
    }
}
