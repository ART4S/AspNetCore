using Model.Entities;
using Npgsql;
using System.Collections.Generic;

namespace DapperContext.Repositories.Npgsql
{
    /// <inheritdoc />
    public class OrderNpgsqlRepository : BaseRepository<Order>
    {
        public OrderNpgsqlRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        /// <inheritdoc />
        public override List<Order> GetAll()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public override Order GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public override void Add(Order entity)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public override void Remove(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
