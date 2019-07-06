using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    /// <summary>
    /// Контекст доступа к данным приложения
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
    }
}
