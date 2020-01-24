using FileStoringSample.Context;
using FileStoringSample.Model.Abstractions;
using System;
using System.Linq;

namespace FileStoringSample.Repositories.Interfaces
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
