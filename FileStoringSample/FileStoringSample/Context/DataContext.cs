using Microsoft.EntityFrameworkCore;

namespace FileStoringSample.Context
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext()
        {

        }
    }
}
