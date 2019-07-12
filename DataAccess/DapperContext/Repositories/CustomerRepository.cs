using Dapper;
using Model.Abstractions;
using Model.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DapperContext.StoredProcedureProviders.Implementations;

namespace DapperContext.Repositories
{
    /// <inheritdoc cref="ICustomerRepository" />
    public class CustomerRepository : BaseRepository<Customer, CustomersStoredProcedureProvider>, ICustomerRepository
    {
        public CustomerRepository(
            IDbConnection connection,
            CustomersStoredProcedureProvider queryProvider) : base(connection, queryProvider)
        {
        }

        /// <inheritdoc />
        public override List<Customer> GetAll()
        {
            var customersDict = new Dictionary<int, Customer>();

            var customers = Connection.Query<Customer, Order, Customer>(
                StoredProcedureProvider.GetAll,
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
                .Query<Customer>(StoredProcedureProvider.OrderedByOrdersCountCustomers)
                .ToList();

            return customers;
        }
    }
}
