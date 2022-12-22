using Microsoft.EntityFrameworkCore;
using PetProject.Entities;
using PetProject.Infacstructure.Database;

namespace PetProject.Infacstructure.Repositories
{
    public class UserRoleReponsitory : GenericRepository<UserRole>
    {
        public UserRoleReponsitory(PetProjectContext dataContext) : base(dataContext) { }

        public override IQueryable<UserRole> Queryable()
        {
            return _dbSet.AsQueryable<UserRole>().Where(p => p.Status == 1);
        }
    }
}
