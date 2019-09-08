using Microsoft.EntityFrameworkCore;

namespace WebFeatures.DataContext.Sql
{
    public class SqlAppContext : AppContext
    {
        public SqlAppContext(DbContextOptions<SqlAppContext> options) : base(options)
        {

        }
    }
}
