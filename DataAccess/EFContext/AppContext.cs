using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace EFContext
{
    /// <summary>
    /// Базовый класс контекста доступа к данным
    /// </summary>
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}
