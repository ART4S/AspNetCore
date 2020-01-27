using System;

namespace FileStoringSample.Data.Model.Abstractions
{
    public class BaseEntity : IEntity
    {
        public Guid Id { get; set; }
    }
}
