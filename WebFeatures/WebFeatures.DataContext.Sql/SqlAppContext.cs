using Microsoft.EntityFrameworkCore;
using WebFeatures.Common.Time;

namespace WebFeatures.DataContext.Sql
{
    public class SqlAppContext : AppContext
    {
        public SqlAppContext(DbContextOptions<SqlAppContext> options, ITimeProvider timeProvider) : base(options, timeProvider)
        {

        }
    }
}
