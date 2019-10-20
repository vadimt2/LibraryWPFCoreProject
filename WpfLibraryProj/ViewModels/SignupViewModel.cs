using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;
using WpfLibraryProj.Commands;
using WpfLibraryProj.Common;
using WpfLibraryProj.Logic.UserService;
using WpfLibraryProj.Views;

namespace WpfLibraryProj.ViewModels
{
    public class SignupViewModel : BindableBase, INavigationAware
    {
        private string _userName;

        public string UserName
        {
            get => _userName; set
            {
                SetProperty(ref _userName, value);
                SignupCommand = new DelagteCommands(Signup, CanSubmit);
            }
        }

        private string _password;

        public string Password
        {
            get => _password; set
            {
                SetProperty(ref _password, value);
                SignupCommand = new DelagteCommands(Signup, CanSubmit);
            }
        }

        private string _email;

        public string Email
        {
            get => _email; set
            {
                SetProperty(ref _email, value);

            }
        }

        private string _message;

        public string Message
        {
            get => _message; set
            {
                SetProperty(ref _message, value);

            }
        }

        private DelagteCommands _signupCommand;

        public DelagteCommands SignupCommand
        {
            get => _signupCommand; set
            {
                SetProperty(ref _signupCommand, value);
            }
        }

        public DelegateCommand GoBackCommand { get; private set; }

        private readonly IUserService _userService;

        private readonly IRegionManager _regionManager;


        public SignupViewModel(IUserService userService, IRegionManager regionManager)
        {
            _userService = userService;

            _regionManager = regionManager;

            SignupCommand = new DelagteCommands(Signup, CanSubmit);

            GoBackCommand = new DelegateCommand(GoBack);
        }


        private void Signup(object pramater)
        {
            User user = new User(UserName, Password, Email);
            var result = _userService.Signup(user);

            if (!result)
                Message = "Please fill up all the feilds or try onther user name";

            Message = "Go to the main menu and login, Thank you.";
        }

        private void GoBack()
        {
            UserName = "";
            Password = "";
            Email = "";
            Message = "";

            MainWindowViewModel.MainFrame.Navigate(new MainView());

            //  _regionManager.RequestNavigate(Regions.MainRegion, Pages.MainView);
        }

        private bool CanSubmit(object pramater)
        {
            if (string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(UserName)) return false;

            return true;
        }

        void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)
        {

        }

        bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext)
        {
            return true; ;
        }

        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
