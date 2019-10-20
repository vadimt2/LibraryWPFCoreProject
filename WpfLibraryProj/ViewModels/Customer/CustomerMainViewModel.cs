using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfLibraryProj.Commands;
using WpfLibraryProj.Common;
using WpfLibraryProj.Logic.AbstractItemService;
using WpfLibraryProj.Views;
using WpfLibraryProj.Views.Users.Customer;

namespace WpfLibraryProj.ViewModels.Customer
{
    public class CustomerMainViewModel : BindableBase, INavigationAware
    {
        private static Frame _customerFrame;

        public static Frame CustomerFrame
        {
            get
            {
                if (_customerFrame == null)
                    _customerFrame = new Frame();

                return _customerFrame;
            }
        }

        private User _user;

        public DelegateCommand SignoutCommand { get; private set; }

        public DelegateCommand ViewAllItemsCommand { get; private set; }

        public DelegateCommand CurrentBrrowedCommand { get; private set; }

        public DelegateCommand ReturnedBrrowedCommand { get; private set; }

        private readonly IRegionManager _regionManager;

        public CustomerMainViewModel(IRegionManager regionManager)
        {
             _regionManager = regionManager;

            ViewAllItemsCommand = new DelegateCommand(ViewAllItems);

            CurrentBrrowedCommand = new DelegateCommand(CurrentBrrowedItems);

            ReturnedBrrowedCommand = new DelegateCommand(ReturnedItems);

            SignoutCommand = new DelegateCommand(Signout);

            _user = Application.Current.Properties[NavParameters.User] as User;

            CustomerFrame.Navigate(new ViewAllItemsView());

        }

        private void Signout()
        {
            Application.Current.Properties.Remove(NavParameters.User);

            MainWindowViewModel.MainFrame.Navigate(new MainView());

            //_regionManager.RequestNavigate(Regions.MainRegion, Pages.MainView);
        }

        private void ViewAllItems()
        {
            //var navigationParameters = new NavigationParameters();

            //navigationParameters.Add(NavParameters.User, _user);

            CustomerFrame.Navigate(new ViewAllItemsView());

            //_regionManager.RequestNavigate(Regions.CustomerRegion, Pages.ViewAllItemsView, navigationParameters);
        }

        private void ReturnedItems()
        {
            //var navigationParameters = new NavigationParameters();

            //navigationParameters.Add(NavParameters.User, _user);

            CustomerFrame.Navigate(new HistoryOfRentedItemsView());

            //_regionManager.RequestNavigate(Regions.CustomerRegion, new Uri("CurrentBorrowedItemView", UriKind.Relative), navigationParameters);
        }

        private void CurrentBrrowedItems()
        {
            //var navigationParameters = new NavigationParameters();

            //navigationParameters.Add(NavParameters.User, _user);

            CustomerFrame.Navigate(new CurrentBorrowedItemView());

            //_regionManager.RequestNavigate(Regions.CustomerRegion, new Uri("CurrentBorrowedItemView", UriKind.Relative), navigationParameters);
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

            var navigationParameters = new NavigationParameters();

            navigationParameters.Add(NavParameters.User, _user);

            _regionManager.RequestNavigate(Regions.CustomerRegion, Pages.ViewAllItemsView, navigationParameters);
        }
    }
}
