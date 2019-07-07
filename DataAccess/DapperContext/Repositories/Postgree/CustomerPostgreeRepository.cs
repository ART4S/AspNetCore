using Model.Entities;
using Npgsql;
using System;
using System.Collections.Generic;

namespace DapperContext.Repositories.Postgree
{
    public class CustomerPostgreeRepository : BaseRepository<Customer>
    {
        public CustomerPostgreeRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        public override List<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Customer GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override void Add(Customer entity)
        {
            throw new NotImplementedException();
        }

        public override void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
