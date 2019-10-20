using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Timers;
using System.Windows;
using WpfLibraryProj.Common;
using WpfLibraryProj.Logic.BookService;
using WpfLibraryProj.ViewModels.Employee;
using WpfLibraryProj.ViewModels.Manager;
using WpfLibraryProj.Views.LibraryItems.AllCollection;

namespace WpfLibraryProj.ViewModels.Manage.Items
{
    public class AddBookViewModel : BindableBase, INavigationAware
    {
        private readonly IBookService _bookService;

        private readonly IRegionManager _regionManager;

        private Book _book;

        public Book Book
        {
            get => _book; set
            {
                SetProperty(ref _book, value);
            }
        }

        private ObservableCollection<BookCategory> _bookCategories;

        public ObservableCollection<BookCategory> BookCategories
        {
            get => _bookCategories; set
            {
                SetProperty(ref _bookCategories, value);
            }
        }

        public DelegateCommand AddItemCommand { get; set; }

        public DelegateCommand ClearFeildsCommand { get; set; }

        private bool _updateItem;

        private Book _tempBook;

        public AddBookViewModel(IBookService bookService, IRegionManager regionManager)
        {
            _tempBook = Application.Current.Properties[NavParameters.Book] as Book;

            _bookService = bookService;

            _regionManager = regionManager;

            BookCategories = new ObservableCollection<BookCategory>();

            foreach (BookCategory category in (BookCategory[])Enum.GetValues(typeof(BookCategory)))
                BookCategories.Add(category);

            AddItemCommand = new DelegateCommand(AddItem);

            ClearFeildsCommand = new DelegateCommand(ClearFeilds);

            if (_tempBook != null)
            {
                Book = _tempBook;

                _updateItem = true;

                Application.Current.Properties.Remove(NavParameters.Book);
            }
            else
                Book = new Book();
        }

        private void AddItem()
        {
            if (string.IsNullOrWhiteSpace(Book.Title) || string.IsNullOrWhiteSpace(Book.Author) || string.IsNullOrWhiteSpace(Book.Description) ||
            Book.Price <= 0 || Book.Quantity <= 0 || Book.PublishDate == null) return;

            Book.Discount((int)Book.Disscount);

            if (_updateItem)
            {
                Book.Id = _tempBook.Id;
                _bookService.UpdateBook(Book);

                var getUser = Application.Current.Properties[NavParameters.User] as User;

                if (getUser == null) return;

                if (getUser.UserRank == UserRank.Manager)
                    ManagerMainViewModel.ManagerFrame.Navigate(new ItemHasAddedView());

                if (getUser.UserRank == UserRank.Manager)
                    EmployeeMainViewModel.EmployeeFrame.Navigate(new ItemHasAddedView());
            }
            else
                _bookService.AddBook(Book);

            ClearFeilds();


            AddItemViewModel.AddItemFrame.Navigate(new ItemHasAddedView());

            //_regionManager.RequestNavigate(Regions.AddItem, Pages.ItemHasAddedView);

        }



        public void ClearFeilds()
        {
            Book.PublishDate = new DateTime();

            Book.Title = "";

            Book.Author = "";

            Book.Description = "";

            Book.Disscount = 0;

            Book.Quantity = 0;

            Book.Price = 0;

            Book.BookCategory = BookCategory.ArtsAndMusic;

        }

        void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)
        {
            //var getBook = navigationContext.Parameters[NavParameters.Book];

            //if (getBook != null)
            //{
            //    var book = getBook as Book;

            //    PublishDate = book.PublishDate;

            //    Title = book.Title;

            //    Author = book.Author;

            //    Description = book.Description;

            //    Disscount = book.Disscount;

            //    Quantity = book.Quantity;

            //    Price = book.Price;

            //    BookCategory = book.BookCategory;

            //    _tempBook = book;

            //    _updateItem = true;

            //}
        }

        bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
