using Dapper;
using DapperContext.QueryProviders.Abstractions;
using Model.Abstractions;
using Model.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DapperContext.Repositories
{
    /// <inheritdoc cref="IProductRepository" />
    public class ProductRepository : BaseRepository<Product, IProductQueryProvider>, IProductRepository
    {
        public ProductRepository(
            IDbConnection connection,
            IProductQueryProvider queryProvider) : base(connection, queryProvider)
        {
        }

        /// <inheritdoc />
        public override List<Product> GetAll()
        {
            var products = Connection
                .Query<Product>(QueryProvider.GetAllQuery)
                .ToList();

            return products;
        }
    }
}
