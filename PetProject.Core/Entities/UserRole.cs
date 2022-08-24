﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Core.Entities
{
    public class UserRole: BaseEntity
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public byte Status { get; set; }

    }
}
