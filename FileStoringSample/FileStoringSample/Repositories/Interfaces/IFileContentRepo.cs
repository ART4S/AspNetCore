using FileStoringSample.Context;
using FileStoringSample.Model.Entities;
using System;
using System.IO;

namespace FileStoringSample.Repositories.Interfaces
{
    public interface IFileContentRepo<TDataContext> : IRepo<FileContent, TDataContext>
        where TDataContext : IDataContext
    {
        Stream Read(Guid id);

        Stream Write(Guid id);
    }
}
