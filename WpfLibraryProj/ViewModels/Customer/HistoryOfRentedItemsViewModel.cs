using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using WpfLibraryProj.Commands;
using WpfLibraryProj.Common;
using WpfLibraryProj.Logic.AbstractItemService;
using WpfLibraryProj.Logic.UserRentalService;
using WpfLibraryProj.Views.LibraryItems.Book;
using WpfLibraryProj.Views.LibraryItems.Journal;

namespace WpfLibraryProj.ViewModels.Customer
{
    public class HistoryOfRentedItemsViewModel : BindableBase
    {
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

        public DelagteCommands BorrowItemCommand { get; private set; }

        public DelegateCommand<AbstractItem> ViewItemCommand { get; private set; }

        public HistoryOfRentedItemsViewModel(IUserRentalService userRentalService, IAbsractItemService absractItemService)
        {
            _absractItemService = absractItemService;

            _userRentalService = userRentalService;

            BorrowItemCommand = new DelagteCommands(BorrowItem, (res) => { return true; });

            ViewItemCommand = new DelegateCommand<AbstractItem>(ViewItem);

            _user = Application.Current.Properties[NavParameters.User] as User;

            AbstractItemCollection = new ObservableCollection<AbstractItem>();

            UpdateRentedItems();
        }


        private void BorrowItem(object pramater)
        {
            var item = pramater as AbstractItem;

            if (item != null && _user != null)
            {
                _absractItemService.BorrowItem(item, _user);

                AbstractItemCollection.Remove(item);
            }
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

        private void UpdateRentedItems()
        {
            var getAllItemsByUser = _userRentalService.GetCurrentReturendItemsUserById(_user.Id);

            foreach (var item in getAllItemsByUser)
            {
                if (item == null) return;

                var getItem = _absractItemService.GetItemById(item.AbstractItemId);

                if (getItem != null)
                    AbstractItemCollection.Add(getItem);
            }
        }
    }
}
