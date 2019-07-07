using Model.Entities;
using Npgsql;
using System;
using System.Collections.Generic;

namespace DapperContext.Repositories.Npgsql
{
    /// <inheritdoc />
    public class ProductNpgsqlRepository : BaseRepository<Product>
    {
        public ProductNpgsqlRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        /// <inheritdoc />
        public override List<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override void Add(Product entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
