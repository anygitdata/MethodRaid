using MethodRaid.Domain.Models;
using MethodRaid.InitDB.Models;

namespace MethodRaid.Test
{
    public class API_models
    {
        public static Client AddClient(DataContext_init context = null, string ClientName = "Новый пользователь")
        {
            if (context is null)
                context = new DataContext_init();

            int id = 0;

            if (context.Clients.Any())
                id = context.Clients.Max(p => p.ClientId);


            id++;

            var client = new Client
            {
                ClientId = id,
                ClientName = ClientName
            };

            context.Add(client);
            context.SaveChanges();

            return client;
        }


        public static Book AddBook(DataContext_init context = null, string title = "Название книги", string descr = "Описание книги")
        {
            if (context is null)
                context = new DataContext_init();

            int id = 0;

            if (context.Books.Any())
                id = context.Books.Max(p => p.BookId);


            id++;

            var book = new Book
            {
                BookId = id,
                Title = title,
                Description = descr

            };

            context.Add(book);
            context.SaveChanges();


            return book;
        }

    }
}
