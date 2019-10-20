using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using WpfLibraryProj.Common;
using WpfLibraryProj.Logic.BookService;

namespace WpfLibraryProj.ViewModels.Manage.Items
{
    public class BookViewModel : BindableBase, INavigationAware
    {
        private readonly IBookService _bookService;

        private Book _book;

        public Book Book
        {
            get => _book; set
            {
                SetProperty(ref _book, value);
            }
        }

        public BookViewModel(IBookService bookService)
        {
            _bookService = bookService;

            Book = Application.Current.Properties[NavParameters.Book] as Book;          

            Application.Current.Properties.Remove(NavParameters.Book);
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
            var getBook = navigationContext.Parameters[NavParameters.Book];

            var book = getBook as Book;

            _book = book;

        }
    }
}
