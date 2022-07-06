using System;

namespace MethodRaid.Domain.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public Nullable<int> GetBookId { get; set; }
    }


    public class BookExt: Book
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }  
    }

}
