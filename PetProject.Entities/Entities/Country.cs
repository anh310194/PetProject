﻿using PetProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Entities
{
    public class Country : BaseEntity
    {
        public string? CountryCode { get; set; }
        public string? CountryName { get; set; }
        public IEnumerable<User>? Users { get; set; }
    }
}