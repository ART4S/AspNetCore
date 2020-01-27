using System;

namespace FileStoringSample.Data.Model.Abstractions
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
