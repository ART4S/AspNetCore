﻿namespace Entities.Abstractions
{
    /// <inheritdoc />
    public abstract class BaseEntity<TId> : IEntity<TId>
        where TId : struct
    {
        public TId Id { get; set; }
    }
}
