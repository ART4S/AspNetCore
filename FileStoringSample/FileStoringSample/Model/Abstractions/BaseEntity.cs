using System;

namespace FileStoringSample.Model.Abstractions
{
    public class BaseEntity : IEntity
    {
        public Guid Id { get; set; }
    }
}
