using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Windows;
using WpfLibraryProj.Commands;
using WpfLibraryProj.Common;
using WpfLibraryProj.Logic.AbstractItemService;
using WpfLibraryProj.Views.LibraryItems.Book;
using WpfLibraryProj.Views.LibraryItems.Journal;

namespace WpfLibraryProj.ViewModels.Customer
{
    public class ViewAllItemsViewModel : BindableBase, INavigationAware
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

        private readonly IAbsractItemService _absractItemService;

        public DelegateCommand<AbstractItem> ViewItemCommand { get; private set; }

        private DelagteCommands _borrowItemCommand;

        public DelagteCommands BorrowItemCommand
        {
            get => _borrowItemCommand; set
            {
                SetProperty(ref _borrowItemCommand, value);
            }
        }

        private DelagteCommands _returnItemCommand;

        public DelagteCommands ReturnItemCommand
        {
            get => _returnItemCommand; set
            {
                SetProperty(ref _returnItemCommand, value);
            }
        }


        public ViewAllItemsViewModel(IAbsractItemService absractItemService, IRegionManager regionManager)
        {
            _absractItemService = absractItemService;

            _regionManager = regionManager;

            var abstractItemCollection = _absractItemService.GetAllItems();

            AbstractItemCollection = new ObservableCollection<AbstractItem>(abstractItemCollection);

            ViewItemCommand = new DelegateCommand<AbstractItem>(ViewItem);

            BorrowItemCommand = new DelagteCommands(BorrowItem, (res) => { return true; });

            ReturnItemCommand = new DelagteCommands(ReturnItem, (res) => { return true; });

            _user = Application.Current.Properties[NavParameters.User] as User;

        }

        private void ViewItem(object pramater)
        {
            var item = pramater as AbstractItem;

            if (item != null && _user != null)
            {
                if (item.AbstractItemCategory == AbstractItemCategory.Book)
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

        private void BorrowItem(object pramater)
        {
            var item = pramater as AbstractItem;

            if (item != null && _user != null)
                _absractItemService.BorrowItem(item, _user);
            var updatedItems = _absractItemService.GetAllItems();
            AbstractItemCollection = new ObservableCollection<AbstractItem>(updatedItems);
        }

        private void ReturnItem(object pramater)
        {
            var item = pramater as AbstractItem;

            if (item != null && _user != null)
                _absractItemService.ReturnItem(item, _user);
            var updatedItems = _absractItemService.GetAllItems();
            AbstractItemCollection = new ObservableCollection<AbstractItem>(updatedItems);
        }

        bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext)
        {
            var updatedItems = _absractItemService.GetAllItems();
            AbstractItemCollection = new ObservableCollection<AbstractItem>(updatedItems);
        }

        void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)
        {
            var getUser = navigationContext.Parameters[NavParameters.User];

            if (getUser != null)
                _user = getUser as User;

            var updatedItems = _absractItemService.GetAllItems();
            AbstractItemCollection = new ObservableCollection<AbstractItem>(updatedItems);
        }
    }
}
