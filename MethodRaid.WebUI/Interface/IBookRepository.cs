using MethodRaid.Domain.Models;
using System.Collections.Generic;

namespace MethodRaid.WebUI.Interface
{
    public interface IBookRepository
    {
        IEnumerable<Book> Books { get; }
    }
}
