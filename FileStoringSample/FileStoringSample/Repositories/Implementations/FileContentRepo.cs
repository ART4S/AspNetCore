using FileStoringSample.Context;
using FileStoringSample.Model.Entities;
using FileStoringSample.Repositories.Interfaces;
using PartialStreams.Database;
using System;
using System.IO;

namespace FileStoringSample.Repositories.Implementations
{
    public class FileContentRepo<TDataContext> : BaseRepo<FileContent, IDataContext>, IFileContentRepo<TDataContext>
        where TDataContext : IDataContext
    {
        private readonly TDataContext _dataContext;

        public FileContentRepo(TDataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public Stream Read(Guid id)
        {
            var content = GetById(id);
            if (content == null)
                throw new ArgumentException($"Content with id={id} doesn't exists in current context");

            return new DbPartialReadingStream(new DbPartialReader(_dataContext, content));
        }

        public Stream Write(Guid id)
        {
            var content = GetById(id);
            if (content == null)
                throw new ArgumentException($"Content with id={id} doesn't exists in current context");

            return new DbPartialReadingStream(new DbPartialReader(_dataContext, content));
        }
    }
}
