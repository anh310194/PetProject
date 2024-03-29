﻿using PetProject.Domain.Entities;
using PetProject.Infacstructure.Context;
using PetProject.Repositories.Common;
using PetProject.Interfaces.Repositories;

namespace PetProject.Infacstructure.Reposibilities
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(PetProjectContext context) : base(context)
        {
        }
    }
}
