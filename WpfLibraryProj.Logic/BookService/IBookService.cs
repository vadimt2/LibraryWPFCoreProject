using System.Collections.Generic;
using WpfLibraryProj.Common;

namespace WpfLibraryProj.Logic.BookService
{
    public interface IBookService
    {
        bool AddBook(Book book);
        void DeleteBook(Book book);
        IEnumerable<AbstractItem> GetBooks();
        Book GetById(Book book);
        Book GetById(int id);
        void UpdateBook(Book book);
    }
}