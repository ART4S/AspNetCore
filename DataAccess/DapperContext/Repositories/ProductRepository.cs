using Dapper;
using Model.Abstractions;
using Model.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DapperContext.StoredProcedureProviders.Implementations;

namespace DapperContext.Repositories
{
    /// <inheritdoc cref="IProductRepository" />
    public class ProductRepository : BaseRepository<Product, ProductStoredProcedureProvider>, IProductRepository
    {
        public ProductRepository(
            IDbConnection connection,
            ProductStoredProcedureProvider storedProcedureProvider) : base(connection, storedProcedureProvider)
        {
        }

        /// <inheritdoc />
        public override List<Product> GetAll()
        {
            var products = Connection
                .Query<Product>(StoredProcedureProvider.GetAll)
                .ToList();

            return products;
        }

        /// <inheritdoc />
        public List<Product> GetMostCostlyProducts()
        {
            var products = Connection
                .Query<Product>(StoredProcedureProvider.MostCostlyProducts)
                .ToList();

            return products;
        }
    }
}
