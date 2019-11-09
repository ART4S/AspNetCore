﻿using System.Collections.Generic;
using AuthenticationExample.Data.Abstractions;

namespace AuthenticationExample.Data.Model
{
    public class Role : BaseEntity
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; }
    }
}