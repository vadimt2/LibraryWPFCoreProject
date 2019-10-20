using Prism.Mvvm;
using System.Collections.ObjectModel;
using WpfLibraryProj.Common;
using WpfLibraryProj.Logic.AbstractItemService;
using WpfLibraryProj.Logic.UserService;
using Prism.Regions;
using WpfLibraryProj.Commands;
using System;
using Prism.Commands;
using System.Windows.Controls;
using WpfLibraryProj.Views.Users.Customer;
using System.Windows;
using WpfLibraryProj.Views;
using WpfLibraryProj.Views.Users.Manager;
using WpfLibraryProj.Views.Users.Employee;

namespace WpfLibraryProj.ViewModels
{
    public class MainViewModel : BindableBase, INavigationAware
    {
        private readonly IAbsractItemService _absractItemService;

        private readonly IUserService _userService;

        public DelegateCommand<PasswordBox> PasswordChangedCommand { get; private set; }

        public DelegateCommand SignupCommand { get; private set; }

        private DelegateCommand _signinCommand;

        public DelegateCommand SigninCommand
        {
            get => _signinCommand; set
            {
                SetProperty(ref _signinCommand, value);
            }
        }

        private readonly IRegionManager _regionManager;

        private ObservableCollection<AbstractItem> _abstractItems;

        public ObservableCollection<AbstractItem> AbstractItemCollection
        {
            get => _abstractItems; set
            {
                SetProperty(ref _abstractItems, value);
            }
        }

        private string _userName;

        private string _password;

        public string UserName
        {
            get => _userName; set
            {
                SetProperty(ref _userName, value);
                SigninCommand = new DelegateCommand(Signin, CanSubmitSignin);
            }
        }

        public string Password
        {
            get => _password; set
            {
                SetProperty(ref _password, value);
                SigninCommand = new DelegateCommand(Signin, CanSubmitSignin);
            }
        }

        public MainViewModel(IAbsractItemService absractItemService, IUserService userService, IRegionManager regionManager)
        {
            _absractItemService = absractItemService;

            _userService = userService;

            var abstractItemCollection = _absractItemService.GetAllItems();

            AbstractItemCollection = new ObservableCollection<AbstractItem>(abstractItemCollection);

            SignupCommand = new DelegateCommand(Signup, CanSubmit);

            SigninCommand = new DelegateCommand(Signin, CanSubmitSignin);

            PasswordChangedCommand = new DelegateCommand<PasswordBox>(ExecutePasswordChangedCommand);

            _regionManager = regionManager;
        }

        private void ExecutePasswordChangedCommand(PasswordBox obj)
        {
            if (obj != null)
                Password = obj.Password;
        }

        private void Signup()
        {
            MainWindowViewModel.MainFrame.Navigate(new SignupView());

            //_regionManager.RequestNavigate(Regions.MainRegion, Pages.SignupView);
        }

        private bool CanSubmit()
        {
            return true;
        }

        private void Signin()
        {

            User user = new User(UserName, Password);

            var getUser = _userService.Signin(user);

            if (getUser == null) return;

            if (getUser.UserRank == UserRank.Customer)
            {
                Application.Current.Properties.Add(NavParameters.User, getUser);

                MainWindowViewModel.MainFrame.Navigate(new CustomerMainView());

                //var navigationParameters = new NavigationParameters();
                //navigationParameters.Add(NavParameters.User, getUser);
                //_regionManager.RequestNavigate(Regions.MainRegion, Pages.CustomerMainView, navigationParameters);
            }


            if (getUser.UserRank == UserRank.LibraryEmployee)
            {
                Application.Current.Properties.Add(NavParameters.User, getUser);

                MainWindowViewModel.MainFrame.Navigate(new EmployeeMainView());

                //var navigationParameters = new NavigationParameters();

                //navigationParameters.Add(NavParameters.User, getUser);

                //_regionManager.RequestNavigate(Regions.MainRegion, Pages.EmployeeMainView, navigationParameters);
            }

            if (getUser.UserRank == UserRank.Manager)
            {
                Application.Current.Properties.Add(NavParameters.User, getUser);

                MainWindowViewModel.MainFrame.Navigate(new ManagerMainView());

                //var navigationParameters = new NavigationParameters();

                //navigationParameters.Add(NavParameters.User, getUser);

                //_regionManager.RequestNavigate(Regions.MainRegion, new Uri(Pages.ManagerMainView, UriKind.RelativeOrAbsolute), navigationParameters);
            }

        }

        private void ClearFields()
        {
            UserName = "";
            Password = "";
        }

        private bool CanSubmitSignin()
        {
            if (string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(UserName)) return false;

            return true;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            ClearFields();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            ClearFields();
        }
    }
}
