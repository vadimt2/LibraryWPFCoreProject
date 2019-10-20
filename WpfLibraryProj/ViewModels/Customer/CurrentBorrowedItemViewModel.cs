using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Windows;
using WpfLibraryProj.Commands;
using WpfLibraryProj.Common;
using WpfLibraryProj.Logic.AbstractItemService;
using WpfLibraryProj.Logic.UserRentalService;
using WpfLibraryProj.Views.LibraryItems.Book;
using WpfLibraryProj.Views.LibraryItems.Journal;
using WpfLibraryProj.Views.Users.Customer;

namespace WpfLibraryProj.ViewModels.Customer
{
    public class CurrentBorrowedItemViewModel : BindableBase //, INavigationAware
    {
        private readonly IRegionManager _regionManager;

        private ObservableCollection<AbstractItem> _abstractItemCollection;

        public ObservableCollection<AbstractItem> AbstractItemCollection
        {
            get => _abstractItemCollection; set
            {
                SetProperty(ref _abstractItemCollection, value);
            }
        }

        private User _user;

        private readonly IUserRentalService _userRentalService;

        private readonly IAbsractItemService _absractItemService;

        public DelegateCommand<AbstractItem> ViewItemCommand { get; private set; }

        public DelagteCommands ReturnItemCommand { get; private set; }

        public CurrentBorrowedItemViewModel(IUserRentalService userRentalService, IRegionManager regionManager, IAbsractItemService absractItemService)
        {
            _absractItemService = absractItemService;

            _userRentalService = userRentalService;

            _regionManager = regionManager;

            ReturnItemCommand = new DelagteCommands(ReturnItem, (res) => { return true; });

            ViewItemCommand = new DelegateCommand<AbstractItem>(ViewItem);

            _user = Application.Current.Properties[NavParameters.User] as User;

            AbstractItemCollection = new ObservableCollection<AbstractItem>();

            UpdateBrrowedItems();

        }

        private void ReturnItem(object pramater)
        {
            var item = pramater as AbstractItem;

            if (item != null && _user != null)
            {
                _absractItemService.ReturnItem(item, _user);

                AbstractItemCollection.Remove(item);
            }
        }

        private void ViewItem(object pramater)
        {
            var item = pramater as AbstractItem;

            if (item != null && _user != null)
            {
                if(item.AbstractItemCategory == AbstractItemCategory.Book)
                {
                  Application.Current.Properties.Add(NavParameters.Book, item);

                    CustomerMainViewModel.CustomerFrame.Navigate(new BookView());
                }

                if (item.AbstractItemCategory == AbstractItemCategory.Journal)
                {
                    Application.Current.Properties.Add(NavParameters.Journal, item);

                    CustomerMainViewModel.CustomerFrame.Navigate(new JournalView());
                }
            }
        }

        private void UpdateBrrowedItems()
        {
            var getAllItemsByUser = _userRentalService.GetCurrentAllItemsUserById(_user.Id);

            foreach (var item in getAllItemsByUser)
            {
                if (item == null) return;

                var getItem = _absractItemService.GetItemById(item.AbstractItemId);

                if (getItem != null)
                    AbstractItemCollection.Add(getItem);
            }
        }

        //bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext)
        //{
        //    return true;
        //}

        //void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext)
        //{
        //    UpdateBrrowedItems();
        //}

        //void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)
        //{
        //    var getUser = navigationContext.Parameters[NavParameters.User];

        //    if (getUser != null)
        //        _user = getUser as User;

        //    UpdateBrrowedItems();
        //}
    }
}