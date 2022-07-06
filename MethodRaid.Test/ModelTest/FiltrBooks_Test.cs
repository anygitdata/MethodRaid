using MethodRaid.Domain.ApiDB;
using MethodRaid.Domain.Models;
using MethodRaid.InitDB.Models;

namespace MethodRaid.Test.ModelTest
{
    public class FiltrBooks_Test
    {

        [Fact]
        public void FiltrBooks_getAll_test()
        {
            // arranme
            var context = new DataContext_init();

            var count = context.Books.Count();

            // act 
            var res = DB_BooksRead.FiltrBooks( ETypeSelectedBook.all);

            // assert
            Assert.IsType<List<Book>>(res);

            Assert.Equal(count, res.Count);
        }


        [Fact]
        public void FiltrBooks_getRead_test()
        {
            // arranme
            var context = new DataContext_init();

            var count = context.Books.Where(p=> p.GetBookId != null).Count();

            // act 
            var res = DB_BooksRead.FiltrBooks(ETypeSelectedBook.read);

            // assert
            Assert.IsType<List<Book>>(res);

            Assert.Equal(count, res.Count);
        }


        [Fact]
        public void FiltrBooks_getFree_test()
        {
            // arranme
            var context = new DataContext_init();

            var count = context.Books.Where(p => p.GetBookId == null).Count();

            // act 
            var res = DB_BooksRead.FiltrBooks(ETypeSelectedBook.free);

            // assert
            Assert.IsType<List<Book>>(res);

            Assert.Equal(count, res.Count);
        }


    }
}
