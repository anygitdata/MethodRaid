// See https://aka.ms/new-console-template for more information

using MethodRaid.Domain.ApiDB;
using MethodRaid.InitDB;
using MethodRaid.InitDB.Models;


Console.WriteLine("Консольное приложение MethodRain.InitDB");

try
{

    Console.WriteLine("Консольное приложение");

    //try
    //{
    //    string res = ApiConfig_initDB.StrSQLConnect();

    //    Console.WriteLine(res);

    //}
    //catch (Exception ex)
    //{
    //    Console.WriteLine(ex.Message);
    //}

    //return;

    var context = new DataContext_init();

    if (context.GetBooks.Any())
        context.GetBooks.RemoveRange(context.GetBooks);

    if (context.Books.Any())
        context.Books.RemoveRange(context.Books);

    if (context.Clients.Any())
        context.RemoveRange(context.Clients);


    var lsBook = SeedData.SeedBooks_fromFile();

    var lsClient = SeedData.SeedClients_fromFile();
    
    context.SaveChanges();


    context.AddRange(lsBook);
    context.AddRange(lsClient);
    context.SaveChanges();

    // Выдача книг читателям
    int bookId = context.Books.FirstOrDefault().BookId;
    int clientId = context.Clients.Skip(2).FirstOrDefault().ClientId;
    DB_BooksRead.TakeBook(bookId, clientId);

    bookId = context.Books.Skip(3).FirstOrDefault().BookId;
    clientId = context.Clients.Skip(1).FirstOrDefault().ClientId;
    DB_BooksRead.TakeBook(bookId, clientId);

    Console.WriteLine($"Загружено книг: {lsBook.Count}");
    Console.WriteLine($"Загружено книентов: {lsClient.Count}");

    int getBook = context.GetBooks.Count();
    Console.WriteLine($"Выдано книг читателям {getBook}");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

