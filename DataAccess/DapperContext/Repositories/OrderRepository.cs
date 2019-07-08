using Dapper;
using DapperContext.QueryProviders.Abstractions;
using Model.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DapperContext.Repositories
{
    /// <inheritdoc />
    public class OrderRepository : BaseRepository<Order, IQueryProvider<Order>>
    {
        public OrderRepository(
            IDbConnection connection,
            IQueryProvider<Order> queryProvider) : base(connection, queryProvider)
        {
        }

        /// <inheritdoc />
        public override List<Order> GetAll()
        {
            var orders = Connection.Query<Order, Customer, Product, Order>(
                    QueryProvider.GetAllQuery,
                    (order, customer, product) =>
                    {
                        order.Product = product;
                        order.Customer = customer;

                        return order;
                    })
                .ToList();

            return orders;
        }
    }
}
