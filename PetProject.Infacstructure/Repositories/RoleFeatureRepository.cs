using PetProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace PetProject.Infacstructure.Repositories
{
    public class RoleFeatureRepository : GenericRepository<RoleFeature>
    {
        public RoleFeatureRepository(DbSet<RoleFeature> dbSet) : base(dbSet) { }

        public override IQueryable<RoleFeature> Queryable()
        {
            return _dbSet.AsQueryable<RoleFeature>().Where(p => p.Status == 1);
        }
    }
}
