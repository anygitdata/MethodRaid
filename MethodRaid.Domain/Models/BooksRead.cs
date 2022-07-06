using System;

namespace MethodRaid.Domain.Models
{
    public class GetBook
    {
        public int GetBookId { get; set; }
        public int ClientId { get; set; }
        public int BookId { get; set; }


        // Заполнение на текущую дату
        public DateTime DateRel { get; set; }

        // Дата возврата на текДату
        public Nullable<DateTime> DateRet { get; set; }
    }
}
