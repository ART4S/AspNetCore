using Dapper;
using DapperContext.QueryProviders.Abstractions;
using Model.Abstractions;
using Model.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DapperContext.Repositories
{
    /// <inheritdoc cref="ICustomerRepository" />
    public class CustomerRepository : BaseRepository<Customer, ICustomerQueryProvider>, ICustomerRepository
    {
        public CustomerRepository(
            IDbConnection connection,
            ICustomerQueryProvider queryProvider) : base(connection, queryProvider)
        {
        }

        /// <inheritdoc />
        public override List<Customer> GetAll()
        {
            var customersDict = new Dictionary<int, Customer>();

            var customers = Connection.Query<Customer, Order, Customer>(
                QueryProvider.GetAllQuery,
                (customer, order) =>
                {
                    if (!customersDict.TryGetValue(customer.Id, out var customerEntry))
                    {
                        customerEntry = customer;
                        customerEntry.Orders = new List<Order>();
                        customersDict[customer.Id] = customerEntry;
                    }

                    if (order != null)
                    {
                        customerEntry.Orders.Add(order);
                    }

                    return customerEntry;
                })
                .Distinct()
                .ToList();

            return customers;
        }

        /// <inheritdoc />
        public List<Customer> GetOrderedByOrdersCountCustomers()
        {
            var customers = Connection
                .Query<Customer>(QueryProvider.OrderedByOrdersCountCustomersQuery)
                .ToList();

            return customers;
        }
    }
}
