﻿using Microsoft.EntityFrameworkCore;
using PetProject.Domain.Entities;
using PetProject.Shared.Interfaces;
using PetProject.Specification.Common;
using PetProject.Specification.Interfaces;

namespace PetProject.Specification.Repositories
{
    public class UserRoleReponsitory : GenericRepository<UserRole>
    {
        public UserRoleReponsitory(IDataContext dataContext) : base(dataContext) { }

        public override IQueryable<UserRole> Queryable()
        {
            return _dbSet.AsQueryable<UserRole>().Where(p => p.Status == 1);
        }
    }
}
