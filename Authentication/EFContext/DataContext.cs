using Microsoft.EntityFrameworkCore;

namespace EFContext
{
    /// <summary>
    /// Контекст доступа к данным приложения
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public 
    }
}
