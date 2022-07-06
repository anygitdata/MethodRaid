using MethodRaid.Domain.Models;
using MethodRaid.Domain.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodRaid.Domain.ApiDB
{
    public class DB_Books
    {
        // ---------------------




        public static ResProc Get_Book(int bookId)
        {
            var res = new ResProc();

            using (var conn = ConnectDB.Get_SQLiteConnection())
            {

                string sql = $"Select BookId, Title, Description, case when getBookId is null then -1 else GetBookId end From Books WHERE BookId = @bookId";

                conn.Open();

                using (var comn = conn.CreateCommand())
                {
                    comn.CommandText = sql;
                    comn.Parameters.AddWithValue("@bookId", bookId);

                    try
                    {
                        var dr = comn.ExecuteReader();

                        Book book = new Book();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                book.BookId = dr.GetInt32(0);
                                book.Title = dr.GetString(1);
                                book.Description = dr.GetString(2);

                                var getBookId = dr.GetInt32(3);

                                if (getBookId > -1)
                                    book.GetBookId = getBookId;
                                
                                break;
                            }

                            dr.Close();
                            dr.Dispose();
                        }

                        res.ResObject = book;
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

        public static ResProc UpdDescr(Book book)
        {
            var res = new ResProc();

            string sql = $"update Books set Description = @descr where BookId = @bookId";


            using (var conn = ConnectDB.Get_SQLiteConnection())
            {
                conn.Open();

                using (var comn = conn.CreateCommand())
                {
                    comn.CommandText = sql;

                    comn.Parameters.AddWithValue("@bookId", book.BookId);
                    comn.Parameters.AddWithValue("@descr", book.Description);

                    try
                    {
                        var dr = comn.ExecuteNonQuery();

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

        public static ResProc AddBook(Book book)
        {
            var res = new ResProc();

            string sql = $"insert into Books ( Title, Description) values (@title, @Descr)";

            using (var conn = ConnectDB.Get_SQLiteConnection())
            {
                conn.Open();

                using (var comn = conn.CreateCommand())
                {
                    comn.CommandText = sql;

                    comn.Parameters.AddWithValue("@title", book.Title);
                    comn.Parameters.AddWithValue("@descr", book.Description);

                    try
                    {
                        var dr = comn.ExecuteNonQuery();


                        res.ResInt = ApiDB.GetMaxID(ETypeModel.book);
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


        public static List<BookExt> Get_ListBookWithClient(string filtr = "All")
        {
            var res = new List<BookExt>();
            var sql = "";

            switch (filtr)
            {
                case "All":
                    sql = "SELECT BookId, 0 clientId, Title, Description, '' ClientName From Books";
                    break;

                case "Free":
                    sql = "SELECT BookId, 0 clientId, Title, Description, '' ClientName From Books WHERE GetBookId is null";
                    break;

                case "Read":
                    sql = @"SELECT b.BookId, cl.ClientId, Title, Description, cl.ClientName 
From Books b 
join GetBooks gb on b.BookId = gb.BookId and gb.DateRet is null
join Clients cl on gb.ClientId = cl.ClientId";
                    break;
            }


            using (var conn = ConnectDB.Get_SQLiteConnection())
            {
                conn.Open();

                using (var comn = conn.CreateCommand())
                {
                    comn.CommandText = sql;

                    var dr = comn.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            res.Add(
                                new BookExt
                                {
                                    BookId = dr.GetInt32(0),
                                    ClientId = dr.GetInt32(1),
                                    Title = dr.GetString(2),
                                    Description = dr.GetString(3),
                                    ClientName = dr.GetString(4)
                                });
                        }
                    }

                    dr.Dispose();
                }
            }

            return res;

        }


        public static List<Book> Get_ListBooks(string filtr="")
        {

            var res = new List<Book>();

            using (var conn = ConnectDB.Get_SQLiteConnection())
            {
                conn.Open();

                using (var comn = conn.CreateCommand())
                {

                    string sql = "SELECT BookId, Title, Description  From Books";
                    string sWhere = "";

                    switch (filtr)
                    {
                        case "All":
                            break;
                        case "Read":
                            sWhere = " where GetBookId is not null";
                            break;
                        case "Free":
                            sWhere = " where GetBookId is null";
                            break;
                    }

                    sql += sWhere;

                    comn.CommandText = sql;

                    var dr = comn.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            res.Add(
                                new Book { BookId = dr.GetInt32(0), Title = dr.GetString(1), Description = dr.GetString(2) });
                        }
                    }

                    dr.Dispose();
                }


                return res;
            }
        }
    }
}
