using PetProject.Entities;
using PetProject.Infacstructure.Interfaces;

namespace PetProject.Infacstructure.Repositories
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
