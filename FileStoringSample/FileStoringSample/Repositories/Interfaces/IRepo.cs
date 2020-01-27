using System;
using System.Linq;
using FileStoringSample.Data.Context;
using FileStoringSample.Data.Model.Abstractions;

namespace FileStoringSample.Data.Repositories.Interfaces
{
    public interface IRepo<TEntity, TDataContext>
        where TEntity : class, IEntity
        where TDataContext : IDataContext
    {
        TEntity GetById(Guid id);

        IQueryable<TEntity> GetAll();

        void Add(TEntity entity);

        void Remove(TEntity entity);

        int SaveChanges();
    }
}
