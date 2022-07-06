using MethodRaid.Domain.ApiDB;
using MethodRaid.Domain.Models;
using MethodRaid.Domain.Models.Tools;
using MethodRaid.InitDB.Models;

namespace MethodRaid.Test.ModelTest
{
    public class Book_Test
    {

        [Fact]
        public void Get_Book_test()
        {
            // arranme
            var context = new DataContext_init();

            var bookId = (API_models.AddBook(context)).BookId;

            // act
            var resProc = DB_Books.Get_Book(bookId);


            // assert
            Assert.IsType<ResProc>(resProc);
            Assert.True(resProc.Result);

            Assert.IsType<Book>(resProc.ResObject);

            Assert.Equal(bookId, ((Book)resProc.ResObject).BookId);
        }


        [Fact]
        public void NewBook_test()
        {
            // arrange
            var context = new DataContext_init();

            var book = new Book
                        {
                            Title="Название",
                            Description="Описание"
                        };

            var count = context.Books.Count();

            // act
            var resBook = DB_Books.AddBook(book);

            var resFind = context.Books.Find(resBook.ResInt);

            // assert
            Assert.IsType<ResProc>(resBook);
            Assert.True(resBook.Result);

            Assert.NotNull(resFind);
            Assert.True(count < context.Books.Count());
        }


        [Fact]
        public void Update_descr_test()
        {
            // arrange
            var context = new DataContext_init();

            API_models.AddBook(context);

            string descr = "Измененное описание книги";

            var book = API_models.AddBook();
            book.Description = descr;

            // act 
            var resProc = DB_Books.UpdDescr(book);

            // assert
            Assert.IsType<ResProc>(resProc);
            Assert.True(resProc.Result);

            var bookFromDB = context.Books.Find(book.BookId);
            Assert.Equal(descr, bookFromDB.Description);

        }

    }
}
