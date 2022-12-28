using PetProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace PetProject.Infacstructure.Repositories
{
    public class UserRoleReponsitory : GenericRepository<UserRole>
    {
        public UserRoleReponsitory(DbSet<UserRole> dbSet) : base(dbSet) { }

        public override IQueryable<UserRole> Queryable()
        {
            return _dbSet.AsQueryable<UserRole>().Where(p => p.Status == 1);
        }
    }
}
