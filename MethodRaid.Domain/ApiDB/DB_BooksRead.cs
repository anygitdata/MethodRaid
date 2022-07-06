using MethodRaid.Domain.Models;
using MethodRaid.Domain.Models.Tools;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MethodRaid.Domain.ApiDB
{
    public class DB_BooksRead
    {

        public static List<Book> FiltrBooks(ETypeSelectedBook typeSelected)
        {
            string sql = "Select BookId, Title, Description, case when getBookId is null then -1 else GetBookId end From Books ";

            var res = new List<Book>();

            switch (typeSelected)
            {
                case ETypeSelectedBook.read:
                    sql += "WHERE GetBookId is not null";
                    break;

                case ETypeSelectedBook.free:
                    sql += "WHERE GetBookId is null";
                    break;
                case ETypeSelectedBook.all:

                    break;

                default:
                    throw new Exception("DB_BooksRead.FiltrBook: Ошибка параметра typeSelected");
            }

            using (var conn = ConnectDB.Get_SQLiteConnection())
            {
                conn.Open();
                using(var comn = conn.CreateCommand())
                {
                    comn.CommandText = sql;

                    var dr = comn.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            res.Add(
                                new Book
                                {
                                    BookId = dr.GetInt32(0),
                                    Title = dr.GetString(1),
                                    Description = dr.GetString(2),
                                    GetBookId = dr.GetInt32(3)
                                }); ;
                        }
                    }

                    dr.Dispose();

                }

            }


            return res; 
        } 


        private static int Book_getBookId(SQLiteConnection conn, int bookId)
        {
            int res = 0;

            using (var comn = conn.CreateCommand())
            {
                comn.CommandText = "select GetBookId From Books WHERE BookId = @bookId;";
                comn.Parameters.AddWithValue("@bookId", bookId);

                var resComn = comn.ExecuteScalar();
                if (resComn != null)
                {
                    res = Convert.ToInt32(resComn);
                }
            }

            return res; 
        }


        public static ResAJAX Web_TakeBook(WebBook book)
        {
            var resProc = TakeBook(book.BookId, book.ClientId);

            var resAjax = new ResAJAX();

            resProc.Fill_intoResAJAX(resAjax);

            return resAjax;
        }


        public static ResProc TakeBook(int bookId, int clientId)
        {
            var res = new ResProc();

            using (var conn = ConnectDB.Get_SQLiteConnection())
            {

                string sql = $@"insert into GetBooks(GetBookId, ClientId, BookId, DateRel) values(@maxId, @clientId, @bookId, @dateRel);
update Books set GetBookId = @maxId where BookId=@bookId";

                DateTime dateRel = DateTime.Now;

                conn.Open();

                int maxId = ApiDB.GetMaxID(ETypeModel.getbook) +1;

                using (var comn = conn.CreateCommand())
                {
                    comn.CommandText = sql;
                    comn.Parameters.AddWithValue("@maxId", maxId);
                    comn.Parameters.AddWithValue("@clientId", clientId);
                    comn.Parameters.AddWithValue("@bookId", bookId);
                    comn.Parameters.AddWithValue("@dateRel", dateRel);

                    try
                    {
                        var dr = comn.ExecuteNonQuery();


                        res.ResInt = maxId;
                        res.Result = true;
                    }
                    catch (Exception ex)
                    {
                        res.Message = ex.Message;
                    }
                }
            }



            return res; 
        }


        public static ResAJAX Web_ReturnBook(WebBook book)
        {
            var resAjax = new ResAJAX();
            var res = BookReturn(book.BookId);

            res.Fill_intoResAJAX(resAjax);

            return resAjax;
        }


        public static ResProc BookReturn(int bookId)
        {
            var res = new ResProc();

            var resBook = DB_Books.Get_Book(bookId);

            if (!resBook.Result)
                throw new Exception($"DB_BooksRead.BookReturn: {resBook.Message}");

            var book = (Book)resBook.ResObject;
            var getBookId = book.GetBookId;
            var dateReturn = DateTime.Now;

            using (var conn = ConnectDB.Get_SQLiteConnection())
            {
                conn.Open();

                string sql = $@"update Books set GetBookId = null where BookId = @bookId;
UPDATE GetBooks set DateRet = @dateReturn where GetBookId = @getBookId";

                using (var comn = conn.CreateCommand())
                {
                    comn.CommandText = sql;

                    comn.Parameters.AddWithValue("@bookId", bookId);
                    comn.Parameters.AddWithValue("@dateReturn", dateReturn);
                    comn.Parameters.AddWithValue("@getBookId", getBookId);

                    comn.ExecuteNonQuery();

                    res.ResObject = book;
                    res.Result = true;

                }

            }


            return res; 
        }

    }
}
