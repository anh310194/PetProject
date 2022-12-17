using Microsoft.EntityFrameworkCore;
using PetProject.Domain.Entities;
using PetProject.Specification.Common;

namespace PetProject.Specification.Repositories
{
    public class UserRoleReponsitory : GenericRepository<UserRole>
    {
        public UserRoleReponsitory(DbContext context) : base(context) { }

        public override IQueryable<UserRole> Queryable()
        {
            return _dbSet.AsQueryable<UserRole>().Where(p => p.Status == 1);
        }
    }
}
