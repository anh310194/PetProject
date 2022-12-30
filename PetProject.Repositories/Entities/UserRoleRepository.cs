﻿using PetProject.Entities;
using PetProject.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using PetProject.Interfaces.Reponsitories;

namespace PetProject.Repositories.Entities
{
    public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(DbContext dbContext) : base(dbContext) { }

        public override IQueryable<UserRole> Queryable()
        {
            return _dbSet.AsQueryable<UserRole>().Where(p => p.Status == 1);
        }
    }
}
