﻿namespace AuthenticationExample.Data.Abstractions
{
    public abstract class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}
