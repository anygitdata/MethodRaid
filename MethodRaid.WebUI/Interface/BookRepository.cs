using MethodRaid.Domain.ApiDB;
using MethodRaid.Domain.Models;
using System.Collections.Generic;

namespace MethodRaid.WebUI.Interface
{
    public class BookRepository: IBookRepository
    {
        public IEnumerable<Book> Books { get; }

        public BookRepository()
        {
            Books = DB_Books.Get_ListBooks();
        }
    }
}