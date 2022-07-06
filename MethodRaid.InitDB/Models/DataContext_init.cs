using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using MethodRaid.Domain;
using MethodRaid.Domain.Models;

namespace MethodRaid.InitDB.Models
{
    public class DataContext_init: DbContext
    {
        public DataContext_init() {}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string conConfig = ApiConfig.Get_SelConnect(); 

            options.UseSqlite($"Data Source={conConfig}");

        }


        public DbSet<Book> Books => Set<Book>();
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<GetBook> GetBooks => Set<GetBook>();

    }
}
