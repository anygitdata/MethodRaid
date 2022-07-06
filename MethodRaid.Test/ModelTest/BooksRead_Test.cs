using MethodRaid.Domain.ApiDB;
using MethodRaid.Domain.Models;
using MethodRaid.Domain.Models.Tools;
using MethodRaid.InitDB.Models;

namespace MethodRaid.Test.ModelTest
{
    public class BooksRead_Test
    {

        [Fact]
        public void Add_BookRel_test()
        {
            // arrange
            var context = new DataContext_init();

            int bookId = context.Books.FirstOrDefault(p=> p.GetBookId == null).BookId;
            int clientId = context.Clients.FirstOrDefault().ClientId;

            // act
            var res = DB_BooksRead.TakeBook(bookId, clientId);


            // assert
            Assert.IsType<ResProc>(res);
            Assert.True(res.Result);

            Assert.True(res.ResInt > 0);


            context = new DataContext_init();

            Assert.True(context.GetBooks.Any(p => p.GetBookId == res.ResInt));
            Assert.NotNull(context.Books.FirstOrDefault(p => p.BookId == bookId).GetBookId);

        }


        [Fact]
        public void BookReturn_test()
        {
            var context = new DataContext_init();

            var book = context.Books.FirstOrDefault(p=> p.GetBookId !=null);
            int bookId = book.BookId;

            // act
            var res = DB_BooksRead.BookReturn(bookId);

            // assert
            Assert.IsType<ResProc>(res);
            Assert.True(res.Result);

            Assert.IsType<Book>(res.ResObject);

            var cont = new DataContext_init();

            var findBook = cont.Books.FirstOrDefault(p => bookId == bookId);
            Assert.Null(findBook.GetBookId);

        }


    }
}
