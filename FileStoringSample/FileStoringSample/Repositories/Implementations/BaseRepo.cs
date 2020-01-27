using System;
using System.Linq;
using FileStoringSample.Data.Context;
using FileStoringSample.Data.Model.Abstractions;
using FileStoringSample.Data.Repositories.Interfaces;

namespace FileStoringSample.Data.Repositories.Implementations
{
    public class BaseRepo<TEntity, TDataContext> : IRepo<TEntity, TDataContext>
        where TEntity: class, IEntity
        where TDataContext: IDataContext
    {
        protected readonly TDataContext DataContext;

        public BaseRepo(TDataContext dataContext)
        {
            DataContext = dataContext;
        }

        public virtual TEntity GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual void Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
