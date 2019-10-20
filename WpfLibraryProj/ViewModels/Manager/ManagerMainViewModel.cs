using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WpfLibraryProj.Commands;
using WpfLibraryProj.Common;
using WpfLibraryProj.Views;
using WpfLibraryProj.Views.LibraryItems.AllCollection;
using WpfLibraryProj.Views.Users.Manager;

namespace WpfLibraryProj.ViewModels.Manager
{
    public class ManagerMainViewModel : BindableBase, INavigationAware
    {
        private static Frame _managerFrame;

        public static Frame ManagerFrame
        {
            get
            {
                if (_managerFrame == null)
                    _managerFrame = new Frame();

                return _managerFrame;
            }
        }

        //private readonly IRegionManager _regionManager;

        private User _user;

        public DelegateCommand SignoutCommand { get; private set; }
        
        public DelegateCommand ViewAllItemsCommand { get; private set; }

        public DelagteCommands AddItemCommand { get; private set; }

        public DelegateCommand MenageAllUsersCommand { get; private set; }

        public DelegateCommand AddUserCommand { get; private set; }

        public ManagerMainViewModel(IRegionManager regionManager)
        {
            //_regionManager = regionManager;

            AddItemCommand = new DelagteCommands(AddItem, CanSubmit);

            ViewAllItemsCommand = new DelegateCommand(ViewAllItems);

            MenageAllUsersCommand = new DelegateCommand(MenageAllUsers);

            AddUserCommand = new DelegateCommand(AddUser);

            SignoutCommand = new DelegateCommand(Signout);

            _user = Application.Current.Properties[NavParameters.User] as User;

            ManagerFrame.Navigate(new ManageAllItemsView());
        }

        private void Signout()
        {
            Application.Current.Properties.Remove(NavParameters.User);

            MainWindowViewModel.MainFrame.Navigate(new MainView());

            //_regionManager.RequestNavigate(Regions.MainRegion, Pages.MainView);
        }

        private void ViewAllItems()
        {
            ManagerFrame.Navigate(new ManageAllItemsView());

            //_regionManager.RequestNavigate(Regions.ManagerViewRegion, Pages.ManageAllItemsView);
        }

        private void MenageAllUsers()
        {
            ManagerFrame.Navigate(new AllUsersView());


            //_regionManager.RequestNavigate(Regions.ManagerViewRegion, Pages.AllUsersView);
        }

        void AddItem(object pramater)
        {
            ManagerFrame.Navigate(new AddItemView());

            //_regionManager.RequestNavigate(Regions.ManagerViewRegion, Pages.AddItemView);
        }
        
        void AddUser()
        {
            ManagerFrame.Navigate(new AddUserView());

            //_regionManager.RequestNavigate(Regions.ManagerViewRegion, Pages.AddUserView);
        }

        bool CanSubmit(object pramater)
        {

            return true;
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
            var getUser = navigationContext.Parameters[NavParameters.User];

            if (getUser != null)
                _user = getUser as User;
        }
    }
}
