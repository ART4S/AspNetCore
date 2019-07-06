using Dapper;
using Model.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DapperContext.Repositories.Sql
{
    /// <inheritdoc />
    public class CustomerSqlRepository : BaseSqlRepository<Customer>
    {
        public CustomerSqlRepository(SqlConnection connection) : base(connection)
        {
        }

        /// <inheritdoc />
        public override List<Customer> GetAll()
        {
            var query = "SELECT * FROM Customers " +
                        "LEFT JOIN Orders ON Customers.Id = Orders.CustomerId";

            var customersDict = new Dictionary<int, Customer>();

            var customers = Connection.Query<Customer, Order, Customer>(
                query,
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
        public override Customer GetById(int id)
        {
            var query = "SELECT * FROM Customers " +
                        "WHERE Id = @id";

            var entity = Connection.QueryFirstOrDefault<Customer>(query);
            return entity;
        }

        /// <inheritdoc />
        public override void Add(Customer entity)
        {
            var query = "INSERT INTO Customers (Name)" +
                        "VALUES (@Name); " +
                        "SELECT CAST(SCOPE_IDENTITY() as int);";

            var id = Connection.QueryFirst<int>(query, entity);
            entity.Id = id;
        }

        /// <inheritdoc />
        public override void Remove(int id)
        {
            var query = "DELETE FROM Customers " +
                        "WHERE Id = @id";

            Connection.Execute(query, id);
        }
    }
}
