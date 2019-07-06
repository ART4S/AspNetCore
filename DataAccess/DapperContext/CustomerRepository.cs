using Dapper;
using DapperContext.Queries.Sql.Customers;
using Model.Abstractions;
using Model.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DapperContext
{
    /// <summary>
    /// <inheritdoc cref="ICustomerRepository"/>
    /// </summary>
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IDbConnection connection) : base(connection)
        {
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public List<Customer> GetAllWithDependencies()
        {
            var query = new SelectCustomersWithDependenciesQuery(Table).Body();
            var customers = Connection.Query<Customer>(query).ToList();

            return customers;
        }
    }
}
