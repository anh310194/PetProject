using PetProject.Core.Data;
using PetProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Infacstructure.Repositories
{
    public class UserRoleReponsitory : Repository<UserRole>
    {
        public UserRoleReponsitory(IDbContext context) : base(context) { }

        public override IQueryable<UserRole> Queryable()
        {
            return _dbSet.AsQueryable<UserRole>().Where(p => p.Status == 1);
        }
    }
}
