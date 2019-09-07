using Microsoft.EntityFrameworkCore;

namespace DataContext.Sql
{
    public class SqlAppContext : AppContext
    {
        public SqlAppContext(DbContextOptions<SqlAppContext> options) : base(options)
        {

        }
    }
}
