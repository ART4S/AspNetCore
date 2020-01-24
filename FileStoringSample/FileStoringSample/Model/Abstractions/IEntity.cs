using System;

namespace FileStoringSample.Model.Abstractions
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
