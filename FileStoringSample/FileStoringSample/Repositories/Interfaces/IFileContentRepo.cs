using System;
using System.IO;
using FileStoringSample.Data.Context;
using FileStoringSample.Data.Model.Entities;

namespace FileStoringSample.Data.Repositories.Interfaces
{
    public interface IFileContentRepo<TDataContext> : IRepo<FileContent, TDataContext>
        where TDataContext : IDataContext
    {
        Stream Read(Guid id);

        Stream Write(Guid id);
    }
}
