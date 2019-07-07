using Model.Entities;
using Npgsql;
using System;
using System.Collections.Generic;

namespace DapperContext.Repositories.Npgsql
{
    /// <inheritdoc />
    public class CustomerNpgsqlRepository : BaseRepository<Customer>
    {
        public CustomerNpgsqlRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        /// <inheritdoc />
        public override List<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override Customer GetById(int id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override void Add(Customer entity)
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
