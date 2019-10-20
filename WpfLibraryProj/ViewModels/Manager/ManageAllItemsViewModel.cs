using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using WpfLibraryProj.Commands;
using WpfLibraryProj.Common;
using WpfLibraryProj.Logic.AbstractItemService;
using WpfLibraryProj.Views.LibraryItems.Book;
using WpfLibraryProj.Views.LibraryItems.Journal;

namespace WpfLibraryProj.ViewModels.Manager
{
    public class ManageAllItemsViewModel : BindableBase, INavigationAware
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

        private readonly IAbsractItemService _absractItemService;

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

        public ManageAllItemsViewModel(IAbsractItemService absractItemService, IRegionManager regionManager)
        {
            _regionManager = regionManager;

            _absractItemService = absractItemService;

            var abstractItemCollection = _absractItemService.GetAllItems();

            AbstractItemCollection = new ObservableCollection<AbstractItem>(abstractItemCollection);

            EditItemCommand = new DelagteCommands(EditItem, (res) => { return true; });

            DeleteItemCommand = new DelagteCommands(DeleteItem, (res) => { return true; });
        }

        private void EditItem(object prameter)
        {
            var item = prameter as AbstractItem;
            if (item.AbstractItemCategory == AbstractItemCategory.Book)
            {
                var book = prameter as Book;

                if (book == null) return;

                Application.Current.Properties.Add(NavParameters.Book, book);

                ManagerMainViewModel.ManagerFrame.Navigate(new AddBookView());

                //var navigationParameters = new NavigationParameters();

                //navigationParameters.Add(NavParameters.Book, book);

                //_regionManager.RequestNavigate(Regions.ManagerViewRegion, Pages.AddBookView, navigationParameters);
            }

            if (item.AbstractItemCategory == AbstractItemCategory.Journal)
            {
                var journal = prameter as Journal;

                if (journal == null) return;

                Application.Current.Properties.Add(NavParameters.Journal, journal);

                ManagerMainViewModel.ManagerFrame.Navigate(new AddJournalView());

                //var navigationParameters = new NavigationParameters();

                //navigationParameters.Add(NavParameters.Journal, journal);

                //_regionManager.RequestNavigate(Regions.ManagerViewRegion, Pages.AddJournalView, navigationParameters);

            }
        }

        private void DeleteItem(object prameter)
        {
            var item = prameter as AbstractItem;

            _absractItemService.DeleteItem(item.Id);

            AbstractItemCollection.Remove(item);
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
            var abstractItemCollection = _absractItemService.GetAllItems();

            AbstractItemCollection = new ObservableCollection<AbstractItem>(abstractItemCollection);
        }
    }
}
