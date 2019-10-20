using System;
using System.Collections.Generic;
using System.Linq;
using WpfLibraryProj.Common;
using WpfLibraryProj.DAL;
using WpfLibraryProj.Logic.AbstractItemService;

namespace WpfLibraryProj.Logic.BookService
{
    public class BookService : IBookService
    {

        private readonly Repository<Book> _bookRepository;

        public BookService(Repository<Book> bookRepository)
        {

            _bookRepository = bookRepository;
        }

        public IEnumerable<AbstractItem> GetBooks()
        {
            return _bookRepository.GetAll();
        }

        public Book GetById(Book book)
        {
            var getBook = _bookRepository.GetById(book.Id);

            return getBook ?? null;
        }

        public Book GetById(int id)
        {
            var getBook = _bookRepository.GetById(id);

            return getBook ?? null;
        }

        public bool AddBook(Book book)
        {
            book.AbstractItemCategory = AbstractItemCategory.Book;

            book.RentedQuantity = 0;

            book.Id = 0;

            book.ISBN = Guid.NewGuid();

            if (string.IsNullOrWhiteSpace(book.Author) || string.IsNullOrWhiteSpace(book.Description)
                || string.IsNullOrWhiteSpace(book.Title) || book.Quantity == 0 || book.PublishDate == null)
                return false;

            book.Discount((int)book.Disscount);

            _bookRepository.Insert(book);

            var result = _bookRepository.Save();

            return result;
        }

        public void DeleteBook(Book book)
        {
            var getBook = _bookRepository.GetById(book.Id);

            if (getBook == null) return;

            _bookRepository.Delete(getBook.Id);

            _bookRepository.Save();
        }

        public void UpdateBook(Book book)
        {
            var bookInDb = _bookRepository.GetById(book.Id);

            if (bookInDb == null) return;

            bookInDb.Title = book.Title;

            bookInDb.Author = book.Author;

            bookInDb.Description = book.Description;

            bookInDb.Disscount = book.Disscount;

            bookInDb.ISBN = book.ISBN;

            bookInDb.BookCategory = book.BookCategory;

            _bookRepository.Save();
        }
    }
}
