using Dapper;
using Model.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DapperContext.StoredProcedureProviders.Implementations;

namespace DapperContext.Repositories
{
    /// <inheritdoc />
    public class OrderRepository : BaseRepository<Order, OrderStoredProcedureProvider>
    {
        public OrderRepository(
            IDbConnection connection,
            OrderStoredProcedureProvider storedProcedureProvider) : base(connection, storedProcedureProvider)
        {
        }

        /// <inheritdoc />
        public override List<Order> GetAll()
        {
            var orders = Connection.Query<Order, Customer, Product, Order>(
                    StoredProcedureProvider.GetAll,
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
